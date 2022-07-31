using System;
using System.Collections;
using System.Collections.Generic;
using SystemModule;

namespace GameSvr
{
    public class TCmdMsg
    {
        public ushort CmdNum;
        public int TagetSvrIdx;
        public int GateIdx;
        public int UserGateIdx;
        public int Userhandle;
        public TSendTarget SendTarget;
        public TDefaultMessage Msg;
        public string UserName;
        public string body;
        public TUserInfo pInfo;
    }

    public class ICommand
    {
        public ICommand() : base()
        {
            
        }

        public virtual void OnCmdChange(ref TCmdMsg Msg)
        {
        }

        public void ErrMsg(string Msg)
        {
            svMain.MainOutMessage(Msg);
        }
    }

    public class TCmdMgr : ICommand
    {
        private readonly ArrayList FItems = null;
        private string FDBBuffer = String.Empty;
        private string FDBBufferBack = String.Empty;
        private readonly IList<string> FDBList = null;
        private object FInterCS = null;
        private object FExternCS = null;

        public TCmdMgr() : base()
        {
            FItems = new ArrayList();
            FDBList = new List<string>();
            FInterCS = new object();
            FExternCS = new object();
        }

        private void RemoveAll()
        {
            int i;
            TCmdMsg pItem;
            for (i = 0; i < FItems.Count; i++)
            {
                pItem = (TCmdMsg)FItems[i];
                if (pItem != null)
                {
                    Dispose(pItem);
                }
            }
            FItems.Clear();
        }

        public virtual void SendMsg(TCmdMsg Msg)
        {
            switch (Msg.SendTarget)
            {
                case TSendTarget.stClient:
                    SendToClient(Msg);
                    break;
                case TSendTarget.stInterServer:
                    SendToInterServer(Msg);
                    break;
                case TSendTarget.stOtherServer:
                    SendToOtherServer(Msg);
                    break;
                case TSendTarget.stDbServer:
                    SendToDBServer(Msg);
                    break;
            }
        }

        private void SendToClient2(TCmdMsg Msg)
        {
            string str;
            str = "[" + Msg.Msg.Ident.ToString() + "]" + "[" + Msg.Msg.Param.ToString() + "]" + "[" + Msg.Msg.Tag.ToString() + "]" + "[" + Msg.Msg.Series.ToString() + "]<" + Msg.body + ">";
            Msg.Msg.Ident = Grobal2.SM_SYSMESSAGE;
            Msg.Msg.Param = HUtil32.MakeWord(219, 255);
            Msg.Msg.Tag = 0;
            Msg.Msg.Series = 1;
            Msg.body = EDcode.EncodeString(str);
            SendToClient(Msg);
        }

        private void SendToClient(TCmdMsg Msg)
        {
            int packetlen;
            TMsgHeader header;
            string pbuf;
            string EncodeBody;
            pbuf = null;
            if (Msg.Userhandle == 0)
            {
                return;
            }
            header.Code = 0xaa55aa55;
            header.SNumber = Msg.Userhandle;
            header.UserGateIndex = (ushort)Msg.UserGateIdx;
            header.Ident = Grobal2.GM_DATA;
            if (Msg.body != "")
            {
                //EncodeBody = EDcode.EncodeString(Msg.body);
                //header.length = sizeof(TDefaultMessage) + EncodeBody.Length + 1;
                //packetlen = sizeof(TMsgHeader) + header.length;
                //GetMem(pbuf, packetlen + 4);
                //Move(packetlen, pbuf, 4);
                //Move(header, pbuf[4], sizeof(TMsgHeader));
                //Move(Msg.Msg, (pbuf[4 + sizeof(TMsgHeader)]), sizeof(TDefaultMessage));
                //Move(EncodeBody[1], (pbuf[4 + sizeof(TMsgHeader) + sizeof(TDefaultMessage)]), EncodeBody.Length + 1);
            }
            else
            {
                //header.length = sizeof(TDefaultMessage);
                //packetlen = sizeof(TMsgHeader) + header.length;
                //GetMem(pbuf, packetlen + 4);
                //Move(packetlen, pbuf, 4);
                //Move(header, pbuf[4], sizeof(TMsgHeader));
                //Move(Msg.Msg, (pbuf[4 + sizeof(TMsgHeader)]), sizeof(TDefaultMessage));
            }
            FInterCS.Enter();
            try
            {
                svMain.RunSocket.SendCmdSocket(Msg.GateIdx, pbuf);
            }
            finally
            {
                FInterCS.Leave();
            }
        }

        private void SendToInterServer(TCmdMsg Msg)
        {
            this.OnCmdChange(ref Msg);
        }

        private void SendToOtherServer(TCmdMsg Msg)
        {
            string str;
            str = Msg.UserName + "/" + Msg.body;
            FInterCS.Enter();
            try
            {
                svMain.UserEngine.SendInterMsg(Msg.CmdNum, svMain.ServerIndex, str);
            }
            finally
            {
                FInterCS.Leave();
            }
            SendMsgQueue(TSendTarget.stInterServer, svMain.ServerIndex, 0, 0, 0, Msg.UserName, Msg.Msg, Msg.body);
        }

        private void SendToDBServer(TCmdMsg Msg)
        {
            string str = EDcode.EncodeMessage(Msg.Msg) + Msg.UserName + "/" + Msg.body;
            FInterCS.Enter();
            try
            {
                svMain.FrontEngine.AddDBData(str);
            }
            finally
            {
                FInterCS.Leave();
            }
        }

        public void SendMsgQueue(TSendTarget SendTarget, int TargetSvrIdx, int GateIdx, int UserGateIdx, int UserHandle, string UserName, TDefaultMessage msg, string body)
        {
            TCmdMsg pItem;
            pItem = new TCmdMsg();
            pItem.CmdNum = msg.Ident;
            pItem.SendTarget = SendTarget;
            pItem.TagetSvrIdx = TargetSvrIdx;
            pItem.GateIdx = GateIdx;
            pItem.UserGateIdx = UserGateIdx;
            pItem.Userhandle = UserHandle;
            pItem.UserName = UserName;
            pItem.Msg = msg;
            pItem.body = body;
            pItem.pInfo = null;
            FItems.Add(pItem);
        }

        public void SendMsgQueue1(TSendTarget SendTarget, int TargetSvrIdx, int GateIdx, int UserGateIdx, int UserHandle, string UserName, int Recog, ushort Ident, ushort Param, ushort Tag, ushort Series, string Body)
        {
            TCmdMsg pItem;
            pItem = new TCmdMsg();
            pItem.Msg.Recog = Recog;
            pItem.Msg.Ident = Ident;
            pItem.Msg.Param = Param;
            pItem.Msg.Tag = Tag;
            pItem.Msg.Series = Series;
            pItem.SendTarget = SendTarget;
            pItem.CmdNum = Ident;
            pItem.TagetSvrIdx = TargetSvrIdx;
            pItem.GateIdx = GateIdx;
            pItem.UserGateIdx = UserGateIdx;
            pItem.Userhandle = UserHandle;
            pItem.UserName = UserName;
            pItem.body = Body;
            pItem.pInfo = null;
            FItems.Add(pItem);
        }

        public void RunMsg()
        {
            TCmdMsg pInfo;
            int TempCmdNum = 0;
            try
            {
                PatchDBBuffer();
            }
            catch (Exception E)
            {
                this.ErrMsg("[Exception] PatchFBBuffer : " + E.Message);
            }
            int Count = FItems.Count;
            for (var i = 0; i < Count; i++)
            {
                FInterCS.Enter();
                try
                {
                    pInfo = null;
                    pInfo = (TCmdMsg)FItems[0];
                    FItems.RemoveAt(0);
                }
                finally
                {
                    FInterCS.Leave();
                }
                if (pInfo != null)
                {
                    try
                    {
                        TempCmdNum = pInfo.CmdNum;
                        SendMsg(pInfo);
                    }
                    catch (Exception E)
                    {
                        this.ErrMsg("FT_EXCEPTION:[" + TempCmdNum.ToString() + "]:" + E.Message);
                    }
                    pInfo = null;
                }
            }
        }

        public void OnDBRead(string ReadBuffer)
        {
            try
            {
                FExternCS.Enter();
                FDBBuffer = FDBBuffer + ReadBuffer;
            }
            finally
            {
                FExternCS.Leave();
            }
        }

        private void DivideBuffer()
        {
            int EndPosition;
            int DataLength;
            string TempData;
            try
            {
                FExternCS.Enter();
                FDBBufferBack = FDBBufferBack + FDBBuffer;
                FDBBuffer = "";
            }
            finally
            {
                FExternCS.Leave();
            }
            while (true)
            {
                EndPosition = FDBBufferBack.IndexOf("!");
                if (EndPosition > 0)
                {
                    TempData = FDBBufferBack;
                    DataLength = TempData.Length;
                    FDBBufferBack.Remove(1, EndPosition);
                    TempData.Remove(EndPosition + 1, DataLength);
                    FDBList.Add(TempData);
                }
                else
                {
                    break;
                }
            }
        }

        private void PatchDBBuffer()
        {
            string Data = string.Empty;
            string certify = string.Empty;
            string head = string.Empty;
            string Body = string.Empty;
            string rmsg = string.Empty;
            string username = string.Empty;
            string sendcmdnum = string.Empty;
            DivideBuffer();
            int ListCount = FDBList.Count;
            if (ListCount == 0)
            {
                return;
            }
            for (var i = 0; i < ListCount; i++)
            {
                string Str = FDBList[0];
                FDBList.RemoveAt(0);
                if (Str != "")
                {
                    Data = "";
                    Str = HUtil32.ArrestStringEx(Str, "#", "!", ref Data);
                    if (Data != "")
                    {
                        Data = HUtil32.GetValidStr3(Data, ref certify, new string[] { "/" });
                        int len = Data.Length;
                        if (HUtil32.Str_ToInt(certify, 0) == 0)
                        {
                            if (len != Grobal2.DEFBLOCKSIZE)
                            {
                                head = Data.Substring(0, Grobal2.DEFBLOCKSIZE);
                                Body = Data.Substring(Grobal2.DEFBLOCKSIZE + 2 - 1, Data.Length - Grobal2.DEFBLOCKSIZE - 7);
                                TDefaultMessage msg = EDcode.DecodeMessage(head);
                                rmsg = EDcode.DecodeString(Body);
                                rmsg = HUtil32.GetValidStr3(rmsg, ref username, new string[] { "/" });
                                rmsg = HUtil32.GetValidStr3(rmsg, ref sendcmdnum, new string[] { "/" });
                                if (username != "")
                                {
                                    SendMsgQueue(TSendTarget.stInterServer, 0, 0, 0, 0, username, msg, rmsg);
                                }
                            }
                        }
                    }
                }
            }
        }
    }

    public enum TSendTarget
    {
        stClient,
        stInterServer,
        stOtherServer,
        stDbServer,
        stFunc
    }
}

    namespace GameSvr
{
    public class CmdMgr
    {
        public static bool g_DbUse = false;
    }
}