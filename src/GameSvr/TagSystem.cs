using System;
using System.Collections;
using SystemModule;

namespace GameSvr
{

    public class TTagInfo : ICommand
    {
        public string Sender
        {
            get
            {
                return FSender;
            }
            set
            {
                FSender = value;
            }
        }
        
        public string SendDate
        {
            get
            {
                return FSendDate;
            }
            set
            {
                FSendDate = value;
            }
        }
        
        public string Msg
        {
            get
            {
                return FMsg;
            }
            set
            {
                FMsg = value;
            }
        }
        
        public int State
        {
            get
            {
                return FState;
            }
            set
            {
                FState = value;
            }
        }
        
        public bool DBSaved
        {
            get
            {
                return FDBSaved;
            }
            set
            {
                FDBSaved = value;
            }
        }
        
        public bool Client
        {
            get
            {
                return FClient;
            }
            set
            {
                FClient = value;
            }
        }
        
        private string FSender = String.Empty;
        public string FSendDate = String.Empty;
        private string FMsg = String.Empty;
        public int FState = 0;
        private bool FDBSaved = false;
        private bool FClient = false;

        public TTagInfo() : base()
        {
            FSender = "";
            FSendDate = "";
            FMsg = "";
            FState = 0;
            FDBSaved = false;
            FClient = false;
        }

        public string GetMsgList()
        {
            return  FState.ToString() + ":" + FSendDate + ":" + FSender + ":" + FMsg;
        }

    }

    public class TTagMgr : ICommand
    {
        private readonly ArrayList FItems = null;
        private readonly ArrayList FRejecter = null;
        private int FNotReadCount = 0;
        private bool FIsTagListSendAble = false;
        private bool FWantTagListFlag = false;
        private int FWantTagListPage = 0;
        private bool FClientGetList = false;
        private bool FIsRejectListSendAble = false;
        private bool FWantRejectListFlag = false;

        public TTagMgr() : base()
        {
            FItems = new ArrayList();
            FRejecter = new ArrayList();
            FIsTagListSendAble = false;
            FWantTagListFlag = false;
            FWantTagListPage = -1;
            FClientGetList = false;
            FIsRejectListSendAble = false;
            FWantRejectListFlag = false;
            FNotReadCount = 0;
        }
        
        ~TTagMgr()
        {
            RemoveAll();
            FItems.Free();
            FRejecter.Clear();
            FRejecter.Free();
        }

        public void OnUserOpen()
        {
            
        }
        
        public void OnUserClose()
        {
            
        }

        public int GetTagCount()
        {
            return FItems.Count;
        }
        
        public bool IsTagAddAble()
        {
            var result = false;
            if (GetTagCount() < TagSystem.MAX_TAG_COUNT)
            {
                result = true;
            }
            return result;
        }

        public string GenerateSendDate()
        {
            return DateTime.Now.ToString("yymmddhhnnss");
        }

        public TTagInfo Find(string SendDate)
        {
            TTagInfo Item;
            TTagInfo result = null;
            for (var i = 0; i < FItems.Count; i++)
            {
                Item = FItems[i] as TTagInfo;
                if (Item.FSendDate == SendDate)
                {
                    result = Item;
                    return result;
                }
            }
            return result;
        }

        public bool Add(TUserInfo UserInfo, string Sender, string SendDate, int State, string Msg)
        {
            var result = false;
            if (IsTagAddAble())
            {
                var Info = new TTagInfo();
                if (Info != null)
                {
                    Info.Sender = Sender;
                    Info.SendDate = SendDate;
                    Info.FState = State;
                    Info.Msg = Msg;
                    FItems.Add(Info);
                    if (Info.State == Grobal2.TAGSTATE_NOTREAD)
                    {
                        FNotReadCount++;
                    }
                    result = true;
                }
            }
            return result;
        }

        public bool Delete(TUserInfo UserInfo, string SendDate, ref int rState)
        {
            bool result = false;
            TTagInfo Info = Find(SendDate);
            if (Info != null)
            {
                if (Info.State == Grobal2.TAGSTATE_DONTDELETE)
                {
                    rState = Grobal2.TAGSTATE_DONTDELETE;
                    return result;
                }
                else
                {
                    Info.State = Grobal2.TAGSTATE_DELETED;
                    rState = Info.State;
                    result = true;
                    return result;
                }
            }
            return result;
        }

        public bool SetInfo(TUserInfo UserInfo, string SendDate, ref int state)
        {
            bool result = false;
            TTagInfo Info = Find(SendDate);
            if (Info != null)
            {
                if ((Info.FState == Grobal2.TAGSTATE_NOTREAD) && (state != Grobal2.TAGSTATE_NOTREAD))
                {
                    if (FNotReadCount > 0)
                    {
                        FNotReadCount -= 1;
                    }
                }
                if (state == Grobal2.TAGSTATE_WANTDELETABLE)
                {
                    state = Grobal2.TAGSTATE_READ;
                }
                Info.FState = state;
                result = true;
            }
            return result;
        }

        public void RemoveInfo(string Date)
        {
            TTagInfo Info = Find(Date);
            if (Info != null)
            {
                int i = FItems.IndexOf(Info);
                if (i >= 0)
                {
                    FItems.RemoveAt(i);
                    Info.Free();
                    return;
                }
            }
        }

        public void RemoveAll()
        {
            TTagInfo Info;
            for (var i = 0; i < FItems.Count; i++)
            {
                Info = (TTagInfo)FItems[i];
                if (Info != null)
                {
                    Info.Free();
                }
            }
            FItems.Clear();
        }

        public int GetRejecterCount()
        {
            return FRejecter.Count;
        }

        public bool IsRejecterAddAble(string Name)
        {
            bool result;
            string Str = String.Empty;
            if ((Name != "") && (false == FindRejecter(Name, Str)) && (GetRejecterCount() < TagSystem.MAX_REJECTER_COUNT))
            {
                result = true;
            }
            else
            {
                result = false;
            }
            return result;
        }

        public bool AddRejecter(string Rejecter)
        {
            bool result = false;
            if (IsRejecterAddAble(Rejecter))
            {
                FRejecter.Add(Rejecter);
                result = true;
            }
            return result;
        }

        public bool DeleteRejecter(string Rejecter)
        {
            string Str = string.Empty;
            bool result = false;
            if (FindRejecter(Rejecter, Str))
            {
                var i = FRejecter.IndexOf(Rejecter);
                if (i >= 0)
                {
                    FRejecter.Remove(i);
                    result = true;
                }
            }
            return result;
        }

        public bool FindRejecter(string Rejecter, string pName)
        {
            pName = "";
            bool result = false;
            for (var i = 0; i < FRejecter.Count; i++)
            {
                if (FRejecter[i] == Rejecter)
                {
                    pName = (string)FRejecter[i];
                    result = true;
                    return result;
                }
            }
            return result;
        }

        public bool IsRejecter(string Rejecter)
        {
            bool result = false;
            for (var i = 0; i < FRejecter.Count; i++)
            {
                if (FRejecter[i] == Rejecter)
                {
                    result = true;
                    return result;
                }
            }
            return result;
        }

        public void RemoveAllRejecter()
        {
            FRejecter.Clear();
        }

        public override void OnCmdChange(ref TCmdMsg Msg)
        {
            switch (Msg.CmdNum)
            {
                case Grobal2.CM_TAG_ADD:
                    OnCmdCMAdd(Msg);
                    break;
                case Grobal2.CM_TAG_ADD_DOUBLE:
                    OnCmdCMAddDouble(Msg);
                    break;
                case Grobal2.CM_TAG_DELETE:
                    OnCmdCMDelete(Msg);
                    break;
                case Grobal2.CM_TAG_SETINFO:
                    OnCmdCMSetInfo(Msg);
                    break;
                case Grobal2.CM_TAG_LIST:
                    OnCmdCMList(Msg);
                    break;
                case Grobal2.CM_TAG_NOTREADCOUNT:
                    OnCmdCMNotReadCount(Msg);
                    break;
                case Grobal2.CM_TAG_REJECT_LIST:
                    OnCmdCMRejectList(Msg);
                    break;
                case Grobal2.CM_TAG_REJECT_ADD:
                    OnCmdCMRejectAdd(Msg);
                    break;
                case Grobal2.CM_TAG_REJECT_DELETE:
                    OnCmdCMRejectDelete(Msg);
                    break;
                case Grobal2.ISM_TAG_SEND:
                    OnCmdISMSend(Msg);
                    break;
                case Grobal2.ISM_TAG_RESULT:
                    OnCmdISMResult(Msg);
                    break;
                case Grobal2.DBR_TAG_LIST:
                    OnCmdDBRList(Msg);
                    break;
                case Grobal2.DBR_TAG_REJECT_LIST:
                    OnCmdDBRRejectList(Msg);
                    break;
                case Grobal2.DBR_TAG_NOTREADCOUNT:
                    OnCmdDBRNotReadCount(Msg);
                    break;
                case Grobal2.DBR_TAG_RESULT:
                    OnCmdDBRResult(Msg);
                    break;
            }
        }

        public void OnMsgList(TUserInfo UserInfo, int PageNum)
        {
            int Cnt;
            string TempStr;
            TTagInfo taginfo;
            int listcount = GetTagCount() - 1;
            int startnum;
            int endnum;
            if (PageNum == 0)
            {
                startnum = 0;
                endnum = listcount;
            }
            else
            {
                startnum = (PageNum - 1) * TagSystem.MAX_TAG_PAGE_COUNT;
                endnum = startnum + TagSystem.MAX_TAG_PAGE_COUNT;
            }
            TempStr = "";
            if (startnum <= listcount)
            {
                if (endnum > listcount)
                {
                    endnum = listcount;
                }
                Cnt = 0;
                for (var i = endnum; i >= startnum; i--)
                {
                    taginfo = (TTagInfo)FItems[i];
                    TempStr = TempStr + taginfo.GetMsgList() + "/";
                    Cnt++;
                }
                OnCmdSMList(UserInfo, PageNum, Cnt, TempStr);
                FClientGetList = true;
            }
        }

        public void OnMsgRejectList(TUserInfo UserInfo)
        {
            int i;
            int Cnt;
            string TempStr;
            // 傈价且膊 绝栏搁 秒家
            if (FRejecter.Count == 0)
            {
                return;
            }
            TempStr = "";
            Cnt = 0;
            for (i = 0; i < FRejecter.Count; i++)
            {
                TempStr = TempStr + FRejecter[i] + "/";
                Cnt++;
            }
            OnCmdSMRejectList(UserInfo, Cnt, TempStr);
        }

        public void OnCmdCMAdd(TCmdMsg Cmd)
        {
            TUserInfo recieverinfo = null;
            string reciever = String.Empty;
            string tagmsg = HUtil32.GetValidStr3(Cmd.body, ref reciever, new string[] { "/" });
            string senddate = GenerateSendDate();
            if (svMain.UserMgrEngine.InterGetUserInfo(reciever, ref recieverinfo))
            {
                OnCmdOSMSend(reciever, recieverinfo.ConnState - Grobal2.CONNSTATE_CONNECT_0, Cmd.UserName, senddate, 0, tagmsg);
            }
            OnCmdDBAdd(Cmd.pInfo, reciever, senddate, 0, tagmsg);
        }

        public void OnCmdCMAddDouble(TCmdMsg Cmd)
        {
            string receiver=String.Empty;
            string receiver2=String.Empty;
            string tagmsg=String.Empty;
            string senddate=String.Empty;
            TUserInfo receiverinfo = null;
            TUserInfo receiverinfo2 = null;
            tagmsg  =  HUtil32.GetValidStr3(Cmd.body, ref receiver, new string[] { "/" });
            tagmsg  =  HUtil32.GetValidStr3(tagmsg, ref receiver2, new string[] { "/" });
            senddate = GenerateSendDate();
            if (receiver != Cmd.UserName)
            {
                if (svMain.UserMgrEngine.InterGetUserInfo(receiver, ref receiverinfo))
                {
                    OnCmdOSMSend(receiver, receiverinfo.ConnState - Grobal2.CONNSTATE_CONNECT_0, Cmd.UserName, senddate, 0, tagmsg);
                }
                OnCmdDBAdd(Cmd.pInfo, receiver, senddate, 0, tagmsg);
            }
            if (receiver2 != Cmd.UserName)
            {
                if (receiver2 == "---")
                {
                    return;
                }
                if (svMain.UserMgrEngine.InterGetUserInfo(receiver2, ref receiverinfo2))
                {
                    OnCmdOSMSend(receiver2, receiverinfo2.ConnState - Grobal2.CONNSTATE_CONNECT_0, Cmd.UserName, senddate, 0, tagmsg);
                }
                OnCmdDBAdd(Cmd.pInfo, receiver2, senddate, 0, tagmsg);
            }
        }

        public void OnCmdCMDelete(TCmdMsg Cmd)
        {
            string senddate = Cmd.body;
            int deletemode = Cmd.Msg.Param;
            TUserInfo userinfo = Cmd.pInfo;
            switch (deletemode)
            {
                case 0:
                    int rState = 0;
                    if (Delete(Cmd.pInfo, senddate, ref rState))
                    {
                        OnCmdDBDelete(userinfo, senddate);
                        OnCmdSMDelete(userinfo, senddate, rState);
                        RemoveInfo(senddate);
                    }
                    break;
                case 1:
                    break;
            }
        }

        public void OnCmdCMList(TCmdMsg Cmd)
        {
            int pagenum = Cmd.Msg.Param;
            if (FIsTagListSendAble)
            {
                OnMsgList(Cmd.pInfo, pagenum);
            }
            else
            {
                FWantTagListFlag = true;
                FWantTagListPage = pagenum;
                // 努腐茄鉴埃 佬绢坷霸 函版茄促.
                OnCmdDBList(Cmd.pInfo);
            }
        }

        public void OnCmdCMSetInfo(TCmdMsg Cmd)
        {
            var senddate = Cmd.body;
            int tagstate = Cmd.Msg.Param;
            var userinfo = Cmd.pInfo;
            if (SetInfo(userinfo, senddate, ref tagstate))
            {
                OnCmdSMInfo(userinfo, senddate, tagstate);
                OnCmdDBInfo(userinfo, senddate, tagstate);
            }
            else
            {
                OnCmdSMResult(userinfo, Grobal2.CM_TAG_SETINFO, Grobal2.CR_DONTUPDATE);
            }
        }

        public void OnCmdCMRejectAdd(TCmdMsg Cmd)
        {
            TUserInfo rejectinfo = null;
            var rejecter = Cmd.body;
            var userinfo = Cmd.pInfo;
            if (!svMain.UserMgrEngine.InterGetUserInfo(rejecter, ref rejectinfo))
            {
                OnCmdSMResult(userinfo, Grobal2.CM_TAG_REJECT_ADD, Grobal2.CR_DONTADD);
                return;
            }
            if (AddRejecter(rejecter))
            {
                OnCmdDBRejectAdd(userinfo, rejecter);
                OnCmdSMRejectAdd(userinfo, rejecter);
            }
            else
            {
                OnCmdSMResult(userinfo, Grobal2.CM_TAG_REJECT_ADD, Grobal2.CR_DONTADD);
            }
        }

        public void OnCmdCMRejectDelete(TCmdMsg Cmd)
        {
            var rejecter = Cmd.body;
            var userinfo = Cmd.pInfo;
            if (DeleteRejecter(rejecter))
            {
                OnCmdDBRejectDelete(userinfo, rejecter);
                OnCmdSMRejectDelete(userinfo, rejecter);
            }
            else
            {
                OnCmdSMResult(userinfo, Grobal2.CM_TAG_REJECT_DELETE, Grobal2.CR_DONTDELETE);
            }
        }

        public void OnCmdCMRejectList(TCmdMsg Cmd)
        {
            TUserInfo userinfo;
            userinfo = Cmd.pInfo;
            if (FIsRejectListSendAble)
            {
                OnMsgRejectList(userinfo);
            }
            else
            {
                FWantRejectListFlag = true;
            }
        }

        public void OnCmdCMNotReadCount(TCmdMsg Cmd)
        {
            var userinfo = Cmd.pInfo;
            if (FIsTagListSendAble)
            {
                OnCmdSMNotReadCount(userinfo, FNotReadCount);
            }
            else
            {
                OnCmdSMResult(userinfo, Grobal2.CM_TAG_NOTREADCOUNT, Grobal2.CR_DBWAIT);
            }
        }

        public void OnCmdSMList(TUserInfo UserInfo, int PageNum, int ListCount, string TagList)
        {
            var str = TagList;
            svMain.UserMgrEngine.InterSendMsg(TSendTarget.stClient, 0, UserInfo.GateIdx, UserInfo.UserGateIdx, UserInfo.UserHandle, UserInfo.UserName, UserInfo.Recog, Grobal2.SM_TAG_LIST, (ushort)PageNum, (ushort)ListCount, 0, str);
        }

        public void OnCmdSMInfo(TUserInfo UserInfo, string SendDate, int State)
        {
            var str = SendDate;
            svMain.UserMgrEngine.InterSendMsg(TSendTarget.stClient, 0, UserInfo.GateIdx, UserInfo.UserGateIdx, UserInfo.UserHandle, UserInfo.UserName, UserInfo.Recog, Grobal2.SM_TAG_INFO, (ushort)State, 0, 0, str);
        }

        public void OnCmdSMAdd(TUserInfo UserInfo, string Sender, string SendDate, int State, string SendMsg)
        {
            var str = State.ToString() + ":" + SendDate + ":" + Sender + ":" + SendMsg;
            svMain.UserMgrEngine.InterSendMsg(TSendTarget.stClient, 0, UserInfo.GateIdx, UserInfo.UserGateIdx, UserInfo.UserHandle, UserInfo.UserName, UserInfo.Recog, Grobal2.SM_TAG_LIST, 0, 1, 0, str);
        }
        
        public void OnCmdSMDelete(TUserInfo UserInfo, string SendDate, int State)
        {
            var str = SendDate;
            svMain.UserMgrEngine.InterSendMsg(TSendTarget.stClient, 0, UserInfo.GateIdx, UserInfo.UserGateIdx, UserInfo.UserHandle, UserInfo.UserName, UserInfo.Recog, Grobal2.SM_TAG_INFO, (ushort)State, 0, 0, str);
        }
        
        public void OnCmdSMRejectList(TUserInfo UserInfo, int ListCount, string RejectList)
        {
            var str = RejectList;
            svMain.UserMgrEngine.InterSendMsg(TSendTarget.stClient, 0, UserInfo.GateIdx, UserInfo.UserGateIdx, UserInfo.UserHandle, UserInfo.UserName, UserInfo.Recog, Grobal2.SM_TAG_REJECT_LIST, (ushort)ListCount, 0, 0, str);
        }
        
        public void OnCmdSMRejectAdd(TUserInfo UserInfo, string Rejecter)
        {
            var str = Rejecter;
            svMain.UserMgrEngine.InterSendMsg(TSendTarget.stClient, 0, UserInfo.GateIdx, UserInfo.UserGateIdx, UserInfo.UserHandle, UserInfo.UserName, UserInfo.Recog, Grobal2.SM_TAG_REJECT_ADD, 0, 0, 0, str);
        }
        
        public void OnCmdSMRejectDelete(TUserInfo UserInfo, string Rejecter)
        {
            var str = Rejecter;
            svMain.UserMgrEngine.InterSendMsg(TSendTarget.stClient, 0, UserInfo.GateIdx, UserInfo.UserGateIdx, UserInfo.UserHandle, UserInfo.UserName, UserInfo.Recog, Grobal2.SM_TAG_REJECT_DELETE, 0, 0, 0, str);
        }
        
        public void OnCmdSMNotReadCount(TUserInfo UserInfo, int NotReadCount)
        {
            svMain.UserMgrEngine.InterSendMsg(TSendTarget.stClient, 0, UserInfo.GateIdx, UserInfo.UserGateIdx, UserInfo.UserHandle, UserInfo.UserName, UserInfo.Recog, Grobal2.SM_TAG_ALARM, (ushort)NotReadCount, 0, 0, "");
        }

        public void OnCmdSMResult(TUserInfo UserInfo, short CmdNum, short ResultValue)
        {
            svMain.UserMgrEngine.InterSendMsg(TSendTarget.stClient, 0, UserInfo.GateIdx, UserInfo.UserGateIdx, UserInfo.UserHandle, UserInfo.UserName, UserInfo.Recog, Grobal2.SM_TAG_RESULT, (ushort)CmdNum, (ushort)ResultValue, 0, "");
        }
        
        public void OnCmdISMSend(TCmdMsg Cmd)
        {
            string sender=String.Empty;
            string senddate=String.Empty;
            string statestr=String.Empty;
            string sendmsg=String.Empty;
            string tempstr=String.Empty;
            tempstr = HUtil32.GetValidStr3(Cmd.body, ref statestr, new string[] { ":" });
            tempstr = HUtil32.GetValidStr3(tempstr, ref senddate, new string[] { ":" });
            sendmsg = HUtil32.GetValidStr3(tempstr, ref sender, new string[] { ":" });
            var userinfo = Cmd.pInfo;
            var state = HUtil32.Str_ToInt(statestr, 0);
            if (!IsRejecter(sender))
            {
                if (Add(userinfo, sender, senddate, state, sendmsg))
                {
                    if (FClientGetList)
                    {
                        OnCmdSMAdd(userinfo, sender, senddate, state, sendmsg);
                    }
                    OnCmdSMNotReadCount(userinfo, FNotReadCount);
                }
            }
        }
        
        public void OnCmdISMResult(TCmdMsg Cmd)
        {
        }
        
        public void OnCmdOSMSend(string UserName, int SvrIndex, string Sender, string SendDate, int State, string SendMsg)
        {
            var str = State.ToString() + ":" + SendDate + ":" + Sender + ":" + SendMsg;
            svMain.UserMgrEngine.InterSendMsg(TSendTarget.stOtherServer, 0, 0, 0, 0, UserName, 0, Grobal2.ISM_TAG_SEND, (ushort)SvrIndex, 0, 0, str);
        }
        
        public void OnCmdOSMResult(string UserName, int SvrIndex, short CmdNum, short ResultValue)
        {
            var str = CmdNum.ToString() + ":" + ResultValue.ToString();
            svMain.UserMgrEngine.InterSendMsg(TSendTarget.stOtherServer, 0, 0, 0, 0, UserName, 0, Grobal2.ISM_TAG_RESULT, (ushort)SvrIndex, 0, 0, str);
        }

        public void OnCmdDBRList(TCmdMsg Cmd)
        {
            string tagcountstr = String.Empty;
            string sender = String.Empty;
            string senddate = String.Empty;
            string statestr = String.Empty;
            string sendmsg = String.Empty;
            string tagstr = String.Empty;
            string tempstr = HUtil32.GetValidStr3(Cmd.body, ref tagcountstr, new string[] { "/" });
            int tagcount = HUtil32.Str_ToInt(tagcountstr, 0);
            TUserInfo userinfo = Cmd.pInfo;
            RemoveAll();
            for (var i = 0; i < tagcount; i++)
            {
                tempstr = HUtil32.GetValidStr3(tempstr, ref tagstr, new string[] { "/" });
                tagstr = HUtil32.GetValidStr3(tagstr, ref statestr, new string[] { ":" });
                tagstr = HUtil32.GetValidStr3(tagstr, ref senddate, new string[] { ":" });
                sendmsg = HUtil32.GetValidStr3(tagstr, ref sender, new string[] { ":" });
                if (!Add(userinfo, sender, senddate, HUtil32.Str_ToInt(statestr, 0), sendmsg))
                {
                    // MainOutMessage('Tag didn''t Added...');
                }
            }
            FIsTagListSendAble = true;
            if (FWantTagListFlag)
            {
                OnMsgList(userinfo, FWantTagListPage);
                FWantTagListFlag = false;
            }
        }
        
        public void OnCmdDBRRejectList(TCmdMsg Cmd)
        {
            string rejecter=String.Empty;
            string rejectcountstr=String.Empty;
            string  tempstr  =  HUtil32.GetValidStr3(Cmd.body, ref rejectcountstr, new string[] { "/" });
            int   rejectcount = HUtil32.Str_ToInt(rejectcountstr, 0);
            TUserInfo    userinfo = Cmd.pInfo;
            for (var i = 0; i < rejectcount; i++)
            {
                tempstr  =  HUtil32.GetValidStr3(tempstr, ref rejecter, new string[] { "/" });
                if (!AddRejecter(rejecter))
                {
                    // 眠啊救登绰 捞蜡甫 钎矫窍磊
                }
            }
            FIsRejectListSendAble = true;
            if (FWantRejectListFlag)
            {
                OnMsgRejectList(userinfo);
                FWantRejectListFlag = false;
            }
        }
        
        public void OnCmdDBRNotReadCount(TCmdMsg Cmd)
        {
            var notreadcountstr = Cmd.body;
            var userinfo = Cmd.pInfo;
            OnCmdSMNotReadCount(userinfo, HUtil32.Str_ToInt(notreadcountstr, 0));
        }
        
        public void OnCmdDBRResult(TCmdMsg Cmd)
        {
            string ErrCode = String.Empty;
            this.ErrMsg("CmdDBRResult[Tag] :" + Cmd.body);
            string CmdNum  =  HUtil32.GetValidStr3(Cmd.body, ref ErrCode, new string[] { "/" });
            switch (HUtil32.Str_ToInt(CmdNum, 0))
            {
                case Grobal2.DB_TAG_ADD:
                    break;
                case Grobal2.DB_TAG_DELETE:
                    break;
                case Grobal2.DB_TAG_DELETEALL:
                    break;
                case Grobal2.DB_TAG_LIST:
                    break;
                case Grobal2.DB_TAG_SETINFO:
                    break;
                case Grobal2.DB_TAG_REJECT_ADD:
                    break;
                case Grobal2.DB_TAG_REJECT_DELETE:
                    break;
                case Grobal2.DB_TAG_REJECT_LIST:
                    break;
                case Grobal2.DB_TAG_NOTREADCOUNT:
                    break;
            }
        }

        public void OnCmdDBAdd(TUserInfo UserInfo, string Reciever, string SendDate, int State, string SendMsg)
        {
            string str = State.ToString() + ":" + SendDate + ":" + Reciever + ":" + SendMsg + "/";
            svMain.UserMgrEngine.InterSendMsg(TSendTarget.stDbServer, svMain.ServerIndex, 0, 0, 0, UserInfo.UserName, 0, Grobal2.DB_TAG_ADD, 0, 0, 0, str);
        }

        public void OnCmdDBInfo(TUserInfo UserInfo, string SendDate, int State)
        {
            string str = State.ToString() + ":" + SendDate + "/";
            svMain.UserMgrEngine.InterSendMsg(TSendTarget.stDbServer, svMain.ServerIndex, 0, 0, 0, UserInfo.UserName, 0, Grobal2.DB_TAG_SETINFO, 0, 0, 0, str);
        }
        
        public void OnCmdDBDelete(TUserInfo UserInfo, string SendDate)
        {
            string str = SendDate + "/";
            svMain.UserMgrEngine.InterSendMsg(TSendTarget.stDbServer, svMain.ServerIndex, 0, 0, 0, UserInfo.UserName, 0, Grobal2.DB_TAG_DELETE, 0, 0, 0, str);
        }
        
        public void OnCmdDBDeleteAll(TUserInfo UserInfo)
        {
            svMain.UserMgrEngine.InterSendMsg(TSendTarget.stDbServer, svMain.ServerIndex, 0, 0, 0, UserInfo.UserName, 0, Grobal2.DB_TAG_DELETEALL, 0, 0, 0, "");
        }
        
        public void OnCmdDBList(TUserInfo UserInfo)
        {
            svMain.UserMgrEngine.InterSendMsg(TSendTarget.stDbServer, svMain.ServerIndex, 0, 0, 0, UserInfo.UserName, 0, Grobal2.DB_TAG_LIST, 0, 0, 0, "");
        }
        
        public void OnCmdDBRejectAdd(TUserInfo UserInfo, string Rejecter)
        {
            string str = Rejecter + "/";
            svMain.UserMgrEngine.InterSendMsg(TSendTarget.stDbServer, svMain.ServerIndex, 0, 0, 0, UserInfo.UserName, 0, Grobal2.DB_TAG_REJECT_ADD, 0, 0, 0, str);
        }

        public void OnCmdDBRejectDelete(TUserInfo UserInfo, string Rejecter)
        {
            string str = Rejecter + "/";
            svMain.UserMgrEngine.InterSendMsg(TSendTarget.stDbServer, svMain.ServerIndex, 0, 0, 0, UserInfo.UserName, 0, Grobal2.DB_TAG_REJECT_DELETE, 0, 0, 0, str);
        }

        public void OnCmdDBRejectList(TUserInfo UserInfo)
        {
            svMain.UserMgrEngine.InterSendMsg(TSendTarget.stDbServer, svMain.ServerIndex, 0, 0, 0, UserInfo.UserName, 0, Grobal2.DB_TAG_REJECT_LIST, 0, 0, 0, "");
        }

        public void OnCmdDBNotReadCount(TUserInfo UserInfo)
        {
            svMain.UserMgrEngine.InterSendMsg(TSendTarget.stDbServer, svMain.ServerIndex, 0, 0, 0, UserInfo.UserName, 0, Grobal2.DB_TAG_NOTREADCOUNT, 0, 0, 0, "");
        }

    }
}

namespace GameSvr
{
    public class TagSystem
    {
        public const int MAX_TAG_COUNT = 30;
        public const int MAX_TAG_PAGE_COUNT = 10;
        public const int MAX_REJECTER_COUNT = 20;
    }
}