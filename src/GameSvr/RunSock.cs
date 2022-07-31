using System.Collections;
using System.Net.Sockets;
using SystemModule;

namespace GameSvr
{
    public struct TRAddr
    {
        public string RemoteAddr;
        public string PublicAddr;
    }

    public class TRunGateInfo
    {
        public bool Connected;
        public Socket Socket;
        public string PublicAddress;
        public int Status;
        // 0: disconnected,  1: good,  2: heavy traffic
        public string ReceiveBuffer;
        public int ReceiveLen;
        public ArrayList SendBuffers;
        public ArrayList UserList;
        public bool NeedCheck;
        public int GateSyncMode;
        public long WaitTime;
        public int SendDataCount;
        public long SendDataTime;
        public int curbuffercount;
        public int remainbuffercount;
        public long sendchecktime;
        public int worksendbytes;
        public int sendbytes;
        public int worksendsoccount;
        public int sendsoccount;
    }

    public class TGateUserInfo
    {
        public string UserId;
        public string UserName;
        public string UserAddress;
        public int UserHandle;
        public int UserGateIndex;
        public int Certification;
        public int ClientVersion;
        public object UEngine;
        public object FEngine;
        public object UCret;
        public long OpenTime;
        public bool Enabled;
    }

    public class TRunCmdInfo
    {
        public int idx;
        public string pbuff;
    }

    public class TRunSocket
    {
        private readonly ArrayList RunAddressList = null;
        private readonly int MaxPubAddr = 0;
        private readonly TRAddr[] PubAddrTable;
        private readonly int DecGateIndex = 0;
        private long gateloadtesttime = 0;
        private readonly ArrayList FCmdList = null;
        private readonly object FCmdCS = null;
        public TRunGateInfo[] GateArr;

        public TRunSocket()
        {
            RunAddressList = new ArrayList();
            for (var i = 0; i < RunSock.MAXGATE; i++)
            {
                GateArr[i].Connected = false;
                GateArr[i].Socket = null;
                GateArr[i].NeedCheck = false;
                GateArr[i].curbuffercount = 0;
                GateArr[i].remainbuffercount = 0;
                GateArr[i].sendchecktime = HUtil32.GetTickCount();
                GateArr[i].sendbytes = 0;
                GateArr[i].sendsoccount = 0;
            }
            LoadRunAddress();
            DecGateIndex = 0;
            FCmdList = new ArrayList();
            FCmdCS = new object();
        }

        ~TRunSocket()
        {
            int i;
            M2Share.ruLock.Free();
            M2Share.socstrLock.Free();
            RunAddressList.Free();
            if (FCmdList != null)
            {
                for (i = 0; i < FCmdList.Count; i++)
                {
                    //pInfo = (TRunCmdInfo)FCmdList[0];
                    //if (pInfo != null)
                    //{
                    //    if (pInfo.pbuff != null)
                    //    {
                    //        freeMem(pInfo.pbuff);
                    //    }
                    //    dispose(pInfo);
                    //}
                    //FCmdList.RemoveAt(0);
                }
                FCmdList.Free();
            }
            FCmdCS.Free();
        }

        private void LoadRunAddress()
        {
            //RunAddressList.LoadFromFile(RunSock.GATEADDRFILE);
            //CheckListValid(RunAddressList);
        }

        public bool IsValidGateAddr(string addr)
        {
            bool result;
            int i;
            try
            {
                result = false;
                M2Share.ruLock.Enter();
                for (i = 0; i < RunAddressList.Count; i++)
                {
                    if (RunAddressList[i] == addr)
                    {
                        result = true;
                        break;
                    }
                }
            }
            finally
            {
                M2Share.ruLock.Leave();
            }
            return result;
        }

        private string GetPublicAddr(string raddr)
        {
            string result = raddr;
            for (var i = 0; i < MaxPubAddr; i++)
            {
                if (PubAddrTable[i].RemoteAddr == raddr)
                {
                    result = PubAddrTable[i].PublicAddr;
                    break;
                }
            }
            return result;
        }

        public void Connect(Socket Socket)
        {
            //if (svMain.ServerReady)
            //{
            //    remote = Socket.RemoteAddress;
            //    for (var i = 0; i < RunSock.MAXGATE; i++)
            //    {
            //        if (!GateArr[i].Connected)
            //        {
            //            GateArr[i].Connected = true;
            //            GateArr[i].Socket = Socket;
            //            GateArr[i].PublicAddress = GetPublicAddr(remote);
            //            GateArr[i].Status = 1;
            //            GateArr[i].UserList = new ArrayList();
            //            GateArr[i].ReceiveBuffer = null;
            //            GateArr[i].ReceiveLen = 0;
            //            GateArr[i].SendBuffers = new ArrayList();
            //            GateArr[i].NeedCheck = false;
            //            GateArr[i].GateSyncMode = 0;
            //            GateArr[i].SendDataCount = 0;
            //            GateArr[i].SendDataTime  =  HUtil32.GetTickCount();
            //            svMain.MainOutMessage("Gate " + i.ToString() + " Opened..");
            //            break;
            //        }
            //    }
            //}
            //else
            //{
            //    svMain.MainOutMessage("Not ready " + Socket.RemoteAddress);
            //    //Socket.Close;
            //}
        }

        public void Disconnect(Socket Socket)
        {
            CloseGate(Socket);
        }

        public void SocketError(Socket Socket, ref int ErrorCode)
        {
            if (Socket.Connected)
            {
                //Socket.Close;
            }
            ErrorCode = 0;
        }

        public void SocketRead(Socket Socket)
        {
            int i;
            for (i = 0; i < RunSock.MAXGATE; i++)
            {
                if (GateArr[i].Socket == Socket)
                {
                    try
                    {
                        //len = Socket.ReceiveLength;
                        //GetMem(p, len);
                        //Socket.ReceiveBuf(p, len);
                        //ExecGateBuffers(i, GateArr[i], p, len);
                        //FreeMem(p);
                    }
                    catch
                    {
                        M2Share.MainOutMessage("Exception] SocketRead");
                    }
                    break;
                }
            }
        }

        public void CloseAllGate()
        {
            int i;
            for (i = 0; i < RunSock.MAXGATE; i++)
            {
                if (GateArr[i].Socket != null)
                {
                    CloseGate(GateArr[i].Socket);
                }
            }
        }

        public void CloseGate(Socket Socket)
        {
            int i;
            int j;
            ArrayList ulist;
            TGateUserInfo uinf;
            try
            {
                M2Share.ruLock.Enter();
                for (i = 0; i < RunSock.MAXGATE; i++)
                {
                    if (GateArr[i].Socket == Socket)
                    {
                        ulist = GateArr[i].UserList;
                        for (j = 0; j < ulist.Count; j++)
                        {
                            uinf = ulist[j] as TGateUserInfo;
                            if (uinf == null)
                            {
                                continue;
                            }
                            if (uinf.UCret != null)
                            {
                                ((TUserHuman)uinf.UCret).EmergencyClose = true;
                                if (!((TUserHuman)uinf.UCret).SoftClosed)
                                {
                                    IdSrvClient.FrmIDSoc.SendUserClose(uinf.UserId, uinf.Certification);
                                }
                            }
                            Dispose(uinf);
                            ulist[j] = null;
                        }
                        if (GateArr[i].ReceiveBuffer != null)
                        {
                            // FreeMem(GateArr[i].ReceiveBuffer);
                        }
                        GateArr[i].ReceiveBuffer = null;
                        GateArr[i].ReceiveLen = 0;
                        for (j = 0; j < GateArr[i].SendBuffers.Count; j++)
                        {
                            //FreeMem(GateArr[i].SendBuffers[j]);
                        }
                        GateArr[i].SendBuffers.Free();
                        GateArr[i].SendBuffers = null;
                        GateArr[i].Connected = false;
                        GateArr[i].Socket = null;
                        M2Share.MainOutMessage("Gate " + i.ToString() + " Closed..");
                        break;
                    }
                }
            }
            finally
            {
                M2Share.ruLock.Leave();
            }
        }

        // ruLock 救俊辑 龋免登绢具窃
        // UserList狼 index甫 府畔窃
        private int OpenNewUser(int shandle, int uindex, string addr, ArrayList ulist)
        {
            int result;
            int i;
            TGateUserInfo uinfo;
            uinfo = new TGateUserInfo();
            uinfo.UserId = "";
            uinfo.UserName = "";
            uinfo.UserAddress = addr;
            uinfo.UserHandle = shandle;
            uinfo.UserGateIndex = uindex;
            uinfo.Certification = 0;
            uinfo.UEngine = null;
            uinfo.FEngine = null;
            uinfo.UCret = null;
            uinfo.OpenTime = HUtil32.GetTickCount();
            uinfo.Enabled = false;
            for (i = 0; i < ulist.Count; i++)
            {
                if (ulist[i] == null)
                {
                    ulist[i] = uinfo;
                    // 吝埃俊 狐柳镑俊 持澜
                    result = i;
                    return result;
                }
            }
            ulist.Add(uinfo);
            result = ulist.Count - 1;
            return result;
        }

        public void CloseUser(int gateindex, int uhandle)
        {
            int i;
            TGateUserInfo puser;
            if (!(gateindex >= 0 && gateindex <= RunSock.MAXGATE - 1))
            {
                return;
            }
            if (GateArr[gateindex].UserList == null)
            {
                return;
            }
            try
            {
                M2Share.ruLock.Enter();
                try
                {
                    for (i = 0; i < GateArr[gateindex].UserList.Count; i++)
                    {
                        if (GateArr[gateindex].UserList[i] == null)
                        {
                            continue;
                        }
                        if ((GateArr[gateindex].UserList[i] as TGateUserInfo).UserHandle == uhandle)
                        {
                            puser = GateArr[gateindex].UserList[i] as TGateUserInfo;
                            // Close 炼扒 促矫 积阿秦具 窃.
                            try
                            {
                                if (puser.FEngine != null)
                                {
                                    ((TFrontEngine)puser.FEngine).UserSocketHasClosed(gateindex, puser.UserHandle);
                                }
                            }
                            catch
                            {
                                M2Share.MainOutMessage("[RunSock] TRunSocket.CloseUser exception 1");
                            }
                            try
                            {
                                if (puser.UCret != null)
                                {
                                    // TUserHuman (puser.UCret).EmergencyClose := TRUE;
                                    ((TUserHuman)puser.UCret).UserSocketClosed = true;
                                }
                            }
                            catch
                            {
                                M2Share.MainOutMessage("[RunSock] TRunSocket.CloseUser exception 2");
                            }
                            try
                            {
                                if (puser.UCret != null)
                                {
                                    if (((TCreature)puser.UCret).BoGhost)
                                    {
                                        // 荤侩磊辆丰啊 酒囱, 辑滚狼 碍力辆丰牢版快
                                        if (!((TUserHuman)puser.UCret).SoftClosed)
                                        {
                                            // 某腐磐 急琶栏肺 狐柳巴捞 酒聪搁
                                            // 促弗 辑滚俊 舅赴促.
                                            IdSrvClient.FrmIDSoc.SendUserClose(puser.UserId, puser.Certification);
                                        }
                                    }
                                }
                            }
                            catch
                            {
                                M2Share.MainOutMessage("[RunSock] TRunSocket.CloseUser exception 3");
                            }
                            try
                            {
                                // 力芭.. 力芭窍瘤 臼绰促.
                                // GateArr[gateindex].UserList.Delete (i);
                                Dispose(puser);
                                GateArr[gateindex].UserList[i] = null;
                            }
                            catch
                            {
                                M2Share.MainOutMessage("[RunSock] TRunSocket.CloseUser exception 4");
                            }
                            break;
                        }
                    }
                }
                catch
                {
                    M2Share.MainOutMessage("[RunSock] TRunSocket.CloseUser exception");
                }
            }
            finally
            {
                M2Share.ruLock.Leave();
            }
        }

        public void SendGateLoadTest(int gindex)
        {
            string pbuf = string.Empty;
            //header.Code = (int)0xaa55aa55;
            //header.SNumber = 0;
            //header.Ident = Grobal2.GM_TEST;
            //pbuf = null;
            //header.length = 80;
            //packetlen = sizeof(TMsgHeader) + header.length;
            //GetMem(pbuf, packetlen + 4);
            //Move(packetlen, pbuf, 4);
            //Move(header, pbuf[4], sizeof(TMsgHeader));
            //Move(def, pbuf[4 + sizeof(TMsgHeader)], sizeof(TDefaultMessage));
            SendUserSocket(gindex, pbuf);
        }

        // 悼扁拳 嘎苗具窃
        public void SendForcedClose(int gindex, int uhandle)
        {
            TDefaultMessage def = Grobal2.MakeDefaultMsg(Grobal2.SM_OUTOFCONNECTION, 0, 0, 0, 0);
            string pbuf = null;
            //header.Code = (int)0xaa55aa55;
            //header.SNumber = uhandle;
            //header.Ident = Grobal2.GM_DATA;
            //header.length = sizeof(TDefaultMessage);
            //packetlen = sizeof(TMsgHeader) + header.length;
            //GetMem(pbuf, packetlen + 4);
            //Move(packetlen, pbuf, 4);
            //Move(header, pbuf[4], sizeof(TMsgHeader));
            //Move(def, pbuf[4 + sizeof(TMsgHeader)], sizeof(TDefaultMessage));
            SendUserSocket(gindex, pbuf);
        }

        public void SendGateCheck(Socket socket, int msg)
        {
            if (socket.Connected)
            {
                //header.Code = (int)0xaa55aa55;
                //header.SNumber = 0;
                //header.Ident = (short)msg;
                //header.length = 0;
                //if (socket != null)
                //{
                //    socket.SendBuf(header, sizeof(TMsgHeader));
                //}
            }
        }

        public void SendServerUserIndex(Socket socket, int shandle, int gindex, int index)
        {
            if (socket.Connected)
            {
                //header.Code = (int)0xaa55aa55;
                //header.SNumber = shandle;
                //header.UserGateIndex = (short)gindex;
                //header.Ident = Grobal2.GM_SERVERUSERINDEX;
                //header.UserListIndex = (short)index;
                //header.length = 0;
                //if (socket != null)
                //{
                //    socket.SendBuf(header, sizeof(TMsgHeader));
                //}
            }
        }

        // 促弗 辑滚俊辑 犁立加 登菌阑 版快, 趣篮 促弗 辑滚俊 捞惑捞 积辫..
        public void CloseUserId(string uid, int cert)
        {
            int gi;
            int k;
            TGateUserInfo pu;
            for (gi = 0; gi < RunSock.MAXGATE; gi++)
            {
                if (GateArr[gi].Connected && (GateArr[gi].Socket != null) && (GateArr[gi].UserList != null))
                {
                    try
                    {
                        M2Share.ruCloseLock.Enter();
                        for (k = 0; k < GateArr[gi].UserList.Count; k++)
                        {
                            pu = GateArr[gi].UserList[k] as TGateUserInfo;
                            if (pu == null)
                            {
                                continue;
                            }
                            if ((pu.UserId == uid) || (pu.Certification == cert))
                            {
                                if (pu.FEngine != null)
                                {
                                    ((TFrontEngine)pu.FEngine).UserSocketHasClosed(gi, pu.UserHandle);
                                }
                                if (pu.UCret != null)
                                {
                                    ((TUserHuman)pu.UCret).EmergencyClose = true;
                                    ((TUserHuman)pu.UCret).UserSocketClosed = true;
                                    // 碍力 辆丰 皋技瘤甫 努扼捞攫飘俊 焊辰促.
                                    SendForcedClose(gi, pu.UserHandle);
                                }
                                // GateArr[gi].UserList.Delete (k); 力芭窍瘤 臼澜
                                Dispose(pu);
                                GateArr[gi].UserList[k] = null;
                                break;
                            }
                        }
                    }
                    finally
                    {
                        M2Share.ruCloseLock.Leave();
                    }
                }
            }
        }

        // pbuf : [length(4)] + [data]
        public void SendUserSocket(int gindex, string pbuf)
        {
            bool flag;
            flag = false;
            if (pbuf == null)
            {
                return;
            }
            try
            {
                M2Share.ruSendLock.Enter();
                if (gindex >= 0 && gindex <= RunSock.MAXGATE - 1)
                {
                    if (GateArr[gindex].SendBuffers != null)
                    {
                        if (GateArr[gindex].Connected && (GateArr[gindex].Socket != null))
                        {
                            GateArr[gindex].SendBuffers.Add(pbuf);
                            flag = true;
                        }
                    }
                }
            }
            finally
            {
                M2Share.ruSendLock.Leave();
            }
            if (!flag)
            {
                try
                {
                    //FreeMem(pbuf);
                }
                finally
                {
                    pbuf = null;
                }
            }
        }

        public void UserLoadingOk(int gateindex, int shandle, object cret)
        {
            int i;
            TGateUserInfo puser;
            if (gateindex >= 0 && gateindex <= RunSock.MAXGATE - 1)
            {
                if (GateArr[gateindex].UserList == null)
                {
                    return;
                }
                try
                {
                    M2Share.ruLock.Enter();
                    for (i = 0; i < GateArr[gateindex].UserList.Count; i++)
                    {
                        puser = (TGateUserInfo)GateArr[gateindex].UserList[i];
                        if (puser == null)
                        {
                            continue;
                        }
                        if (puser.UserHandle == shandle)
                        {
                            puser.FEngine = null;
                            puser.UEngine = M2Share.UserEngine;
                            puser.UCret = cret;
                            break;
                        }
                    }
                }
                finally
                {
                    M2Share.ruLock.Leave();
                }
            }
        }

        public bool DoClientCertification_GetCertification(string body, ref string uid, ref string chrname, ref int certify, ref int clversion, ref int clientchecksum, ref bool startnew)
        {
            bool result;
            string str = string.Empty;
            string scert = string.Empty;
            string sver = string.Empty;
            string start = string.Empty;
            string sxorcert = string.Empty;
            string checksum = string.Empty;
            string sxor2 = string.Empty;
            long checkcert;
            long xor1;
            long xor2;
            result = false;
            try
            {
                str = EDcode.DecodeString(body);
                if (str.Length > 2)
                {
                    if ((str[0] == '*') && (str[1] == '*'))
                    {
                        str = str.Substring(3 - 1, str.Length - 2);
                        str = HUtil32.GetValidStr3(str, ref uid, new string[] { "/" });
                        str = HUtil32.GetValidStr3(str, ref chrname, new string[] { "/" });
                        str = HUtil32.GetValidStr3(str, ref scert, new string[] { "/" });
                        str = HUtil32.GetValidStr3(str, ref sver, new string[] { "/" });
                        str = HUtil32.GetValidStr3(str, ref sxorcert, new string[] { "/" });
                        str = HUtil32.GetValidStr3(str, ref checksum, new string[] { "/" });
                        str = HUtil32.GetValidStr3(str, ref sxor2, new string[] { "/" });
                        start = str;
                        certify = HUtil32.Str_ToInt(scert, 0);
                        checkcert = certify;
                        xor1 = HUtil32.Str_ToInt(sxorcert, 0);
                        xor2 = HUtil32.Str_ToInt(sxor2, 0);
                        if (start == "0")
                        {
                            startnew = true;
                        }
                        else
                        {
                            startnew = false;
                        }
                        if ((uid != "") && (chrname != "") && (checkcert >= 2) && (checkcert == (xor1 ^ 0xF2E44FFF)) && (checkcert == (xor2 ^ 0xa4a5b277)))
                        {
                            clversion = HUtil32.Str_ToInt(sver, 0);
                            clientchecksum = HUtil32.Str_ToInt(checksum, 0);
                            result = true;
                        }
                    }
                }
            }
            catch
            {
                M2Share.MainOutMessage("[RunSock] TRunSocket.DoClientCertification.GetCertification exception ");
            }
            return result;
        }

        // ruLock 救俊辑 龋免登绰 窃荐 烙.
        private void DoClientCertification(int gindex, TGateUserInfo puser, int shandle, string data)
        {
            string uid = string.Empty;
            string chrname = string.Empty;
            int certify = 0;
            int clversion = 0;
            int loginclientversion = 0;
            int clcheck = 0;
            int bugstep = 0;
            int certmode = 0;
            int availmode = 0;
            bool startnew = false;
            bugstep = 0;
            try
            {
                if (puser.UserId == "")
                {
                    if (HUtil32.CharCount(data, '!') >= 1)
                    {
                        HUtil32.ArrestStringEx(data, "#", "!", ref data);
                        data = data.Substring(2 - 1, data.Length - 1);
                        bugstep = 1;
                        if (DoClientCertification_GetCertification(data, ref uid, ref chrname, ref certify, ref clversion, ref clcheck, ref startnew))
                        {
                            certmode = IdSrvClient.FrmIDSoc.GetAdmission(uid, puser.UserAddress, certify, ref availmode, ref loginclientversion);
                            if (certmode > 0)
                            {
                                puser.Enabled = true;
                                puser.UserId = uid.Trim();
                                puser.UserName = chrname.Trim();
                                puser.Certification = certify;
                                puser.ClientVersion = clversion;
                                try
                                {
                                    M2Share.FrontEngine.LoadPlayer(uid, chrname, puser.UserAddress, startnew, certify, certmode, availmode, clversion, loginclientversion, clcheck, shandle, puser.UserGateIndex, gindex);
                                }
                                catch
                                {
                                    M2Share.MainOutMessage("[RunSock] LoadPlay... TRunSocket.DoClientCertification exception");
                                }
                            }
                            else
                            {
                                bugstep = 2;
                                puser.UserId = "* disable *";
                                puser.Enabled = false;
                                CloseUser(gindex, shandle);
                                bugstep = 3;
                                M2Share.MainOutMessage("Fail admission:1<" + puser.UserAddress + "><" + availmode.ToString() + ">");
                                if (startnew)
                                {
                                    M2Share.MainOutMessage("Fail admission:2<" + certmode.ToString() + "><" + uid + "><" + chrname + "><" + certify.ToString() + "><" + clversion.ToString() + "><" + clcheck.ToString() + "><T>");
                                }
                                else
                                {
                                    M2Share.MainOutMessage("Fail admission:2<" + certmode.ToString() + "><" + uid + "><" + chrname + "><" + certify.ToString() + "><" + clversion.ToString() + "><" + clcheck.ToString() + "><F>");
                                }
                            }
                        }
                        else
                        {
                            bugstep = 4;
                            puser.UserId = "* disable *";
                            puser.Enabled = false;
                            CloseUser(gindex, shandle);
                            bugstep = 5;
                            M2Share.MainOutMessage("invalid admission: \"" + data + "\"");
                        }
                    }
                }
            }
            catch
            {
                M2Share.MainOutMessage("[RunSock] TRunSocket.DoClientCertification exception " + bugstep.ToString());
            }
        }

        private void ExecGateMsg(int gindex, TRunGateInfo CGate, TMsgHeader pheader, string pdata, int len)
        {
            int i;
            int uidx;
            TGateUserInfo puser;
            int debug = 0;
            try
            {
                switch (pheader.Ident)
                {
                    case Grobal2.GM_OPEN:
                        debug = 1;
                        uidx = OpenNewUser(pheader.SNumber, pheader.UserGateIndex, pdata, CGate.UserList);
                        SendServerUserIndex(CGate.Socket, pheader.SNumber, pheader.UserGateIndex, uidx + 1);
                        break;
                    case Grobal2.GM_CLOSE:
                        debug = 2;
                        puser = null;
                        for (i = 0; i < CGate.UserList.Count; i++)
                        {
                            puser = CGate.UserList[i] as TGateUserInfo;
                            if (puser != null)
                            {
                                if (puser.UserHandle == pheader.SNumber)
                                {

                                }
                            }
                        }
                        CloseUser(gindex, pheader.SNumber);
                        break;
                    case Grobal2.GM_CHECKCLIENT:
                        debug = 3;
                        CGate.NeedCheck = true;
                        break;
                    case Grobal2.GM_RECEIVE_OK:
                        debug = 4;
                        CGate.GateSyncMode = 0;
                        CGate.SendDataCount = 0;
                        break;
                    case Grobal2.GM_DATA:
                        debug = 5;
                        puser = null;
                        if (pheader.UserListIndex >= 1)
                        {
                            uidx = pheader.UserListIndex - 1;
                            if (uidx < CGate.UserList.Count)
                            {
                                puser = CGate.UserList[uidx] as TGateUserInfo;
                                if (puser != null)
                                {
                                    if (puser.UserHandle != pheader.SNumber)
                                    {
                                        puser = null;
                                    }
                                }
                            }
                        }
                        if (puser == null)
                        {
                            for (i = 0; i < CGate.UserList.Count; i++)
                            {
                                if (CGate.UserList[i] == null)
                                {
                                    continue;
                                }
                                if ((CGate.UserList[i] as TGateUserInfo).UserHandle == pheader.SNumber)
                                {
                                    puser = CGate.UserList[i] as TGateUserInfo;
                                    break;
                                }
                            }
                        }
                        debug = 6;
                        if (puser != null)
                        {
                            if ((puser.UCret != null) && (puser.UEngine != null))
                            {
                                if (puser.Enabled)
                                {
                                    if (len >= TDefaultMessage.PacketSize)
                                    {
                                        if (len == TDefaultMessage.PacketSize)
                                        {
                                            // svMain.UserEngine.ProcessUserMessage((TUserHuman)puser.UCret, (TDefaultMessage)pdata, null);
                                        }
                                        else
                                        {
                                            // svMain.UserEngine.ProcessUserMessage((TUserHuman)puser.UCret, (TDefaultMessage)pdata, pdata[TDefaultMessage.PacketSize]);
                                        }
                                    }
                                }
                            }
                            else
                            {
                                DoClientCertification(gindex, puser, pheader.SNumber, pdata);
                            }
                        }
                        break;
                }
            }
            catch
            {
                M2Share.MainOutMessage("[Exception] ExecGateMsg.. " + debug.ToString());
            }
        }

        private void ExecGateBuffers(int gindex, TRunGateInfo CGate, string pBuffer, int buflen)
        {
            int len;
            string pwork;
            pwork = null;
            len = 0;
            try
            {
                if (pBuffer != null)
                {
                    //ReAllocMem(CGate.ReceiveBuffer, CGate.ReceiveLen + buflen);
                    //Move(pBuffer, CGate.ReceiveBuffer[CGate.ReceiveLen], buflen);
                }
            }
            catch
            {
                M2Share.MainOutMessage("Exception] ExecGateBuffers->pBuffer");
            }
            try
            {
                len = CGate.ReceiveLen + buflen;
                pwork = CGate.ReceiveBuffer;
                //while (len >= sizeof(TMsgHeader))
                //{
                //    pheader = (TMsgHeader)pwork;
                //    if (pheader.Code == 0xaa55aa55)
                //    {
                //        if (len < sizeof(TMsgHeader) + pheader.length)
                //        {
                //            break;
                //        }
                //        pbody = pwork[sizeof(TMsgHeader)];
                //        ExecGateMsg(gindex, CGate, pheader, pbody, pheader.length);
                //        pwork = pwork[sizeof(TMsgHeader) + pheader.length];
                //        len = len - (sizeof(TMsgHeader) + pheader.length);
                //    }
                //    else
                //    {
                //        pwork = pwork[1];
                //        len -= 1;
                //    }
                //}
            }
            catch
            {
                M2Share.MainOutMessage("Exception] ExecGateBuffers->@pwork,ExecGateMsg");
            }
            try
            {
                //if (len > 0)
                //{
                //    GetMem(ptemp, len);
                //    Move(pwork, ptemp, len);
                //    FreeMem(CGate.ReceiveBuffer);
                //    CGate.ReceiveBuffer = ptemp;
                //    CGate.ReceiveLen = len;
                //}
                //else
                //{
                //    FreeMem(CGate.ReceiveBuffer);
                //    CGate.ReceiveBuffer = null;
                //    CGate.ReceiveLen = 0;
                //}
            }
            catch
            {
                M2Share.MainOutMessage("Exception] ExecGateBuffers->FreeMem");
            }
        }

        private bool SendGateBuffers(int gindex, TRunGateInfo CGate, ArrayList slist)
        {
            bool result;
            int curn;
            int sendlen = 0;
            string psend;
            long start;
            result = true;
            if (slist.Count == 0)
            {
                return result;
            }
            start = HUtil32.GetTickCount();
            if (CGate.GateSyncMode > 0)
            {
                if (HUtil32.GetTickCount() - CGate.WaitTime > 2000)
                {
                    // 鸥烙 酒眶
                    CGate.GateSyncMode = 0;
                    CGate.SendDataCount = 0;
                }
                return result;
            }
            try
            {
                curn = 0;
                psend = (string)slist[curn];
                while (true)
                {
                    if (curn + 1 >= slist.Count)
                    {
                        break;
                    }
                    //pwork = (string)slist[curn + 1];
                    //Move(psend, len, 4);
                    //Move(pwork, newlen, 4);
                    //if (len + newlen < svMain.SENDBLOCK)
                    //{
                    //    slist.RemoveAt(curn + 1);
                    //    GetMem(pnew, 4 + len + newlen);
                    //    totlen = len + newlen;
                    //    Move(totlen, pnew, 4);
                    //    Move(psend[4], pnew[4], len);
                    //    Move(pwork[4], pnew[4 + len], newlen);
                    //    FreeMem(psend);
                    //    FreeMem(pwork);
                    //    psend = pnew;
                    //    slist[curn] = psend;
                    //}
                    //else
                    //{
                    //    curn++;
                    //    psend = pwork;
                    //}
                }
            }
            catch
            {
                M2Share.MainOutMessage("Exception SendGateBuffers(1)..");
            }
            try
            {
                while (slist.Count > 0)
                {
                    psend = (string)slist[0];
                    if (psend == null)
                    {
                        slist.RemoveAt(0);
                        continue;
                    }
                    //Move(psend, sendlen, 4);
                    if ((CGate.GateSyncMode == 0) && (sendlen + CGate.SendDataCount >= M2Share.SENDCHECKBLOCK))
                    {
                        if ((CGate.SendDataCount == 0) && (sendlen >= M2Share.SENDCHECKBLOCK))
                        {
                            slist.RemoveAt(0);
                            try
                            {
                                //FreeMem(psend);
                            }
                            catch
                            {
                                psend = null;
                            }
                            psend = null;
                        }
                        else
                        {
                            SendGateCheck(CGate.Socket, Grobal2.GM_RECEIVE_OK);
                            CGate.GateSyncMode = 1;
                            CGate.WaitTime = HUtil32.GetTickCount();
                        }
                        break;
                    }
                    if (psend == null)
                    {
                        continue;
                    }
                    slist.RemoveAt(0);
                    //pwork = psend[4];
                    //while (sendlen > 0)
                    //{
                    //    if (sendlen >= svMain.SENDBLOCK)
                    //    {
                    //        if (CGate.Socket != null)
                    //        {
                    //            if (CGate.Socket.Connected)
                    //            {
                    //                CGate.Socket.SendBuf(pwork, svMain.SENDBLOCK);
                    //            }
                    //            CGate.worksendsoccount = CGate.worksendsoccount + 1;
                    //            CGate.worksendbytes = CGate.worksendbytes + svMain.SENDBLOCK;
                    //        }
                    //        CGate.SendDataCount = CGate.SendDataCount + svMain.SENDBLOCK;
                    //        pwork = pwork[svMain.SENDBLOCK];
                    //        sendlen = sendlen - svMain.SENDBLOCK;
                    //    }
                    //    else
                    //    {
                    //        if (CGate.Socket != null)
                    //        {
                    //            if (CGate.Socket.Connected)
                    //            {
                    //                CGate.Socket.SendBuf(pwork, sendlen);
                    //            }
                    //            CGate.worksendsoccount = CGate.worksendsoccount + 1;
                    //            CGate.worksendbytes = CGate.worksendbytes + sendlen;
                    //            CGate.SendDataCount = CGate.SendDataCount + sendlen;
                    //        }
                    //        sendlen = 0;
                    //        break;
                    //    }
                    //}
                    //FreeMem(psend);
                    if (HUtil32.GetTickCount() - start > M2Share.SocLimitTime)
                    {
                        result = false;
                        break;
                    }
                }
            }
            catch
            {
                M2Share.MainOutMessage("Exception SendGateBuffers(2)..");
            }
            return result;
        }

        public void SendCmdSocket(int gindex, string pbuf)
        {
            TRunCmdInfo PInfo;
            FCmdCS.Enter();
            try
            {
                PInfo = new TRunCmdInfo();
                PInfo.idx = gindex;
                PInfo.pbuff = pbuf;
                FCmdList.Add(PInfo);
            }
            finally
            {
                FCmdCS.Leave();
            }
        }

        public void PatchSendData()
        {
            int count = FCmdList.Count;
            TRunCmdInfo pInfo = null;
            while (count > 0)
            {
                FCmdCS.Enter();
                try
                {
                    pInfo = (TRunCmdInfo)FCmdList[0];
                    FCmdList.RemoveAt(0);
                }
                finally
                {
                    FCmdCS.Leave();
                }
                count = count - 1;
                if (pInfo != null)
                {
                    try
                    {
                        SendUserSocket(pInfo.idx, pInfo.pbuff);
                        //dispose(pInfo);
                    }
                    catch
                    {
                        M2Share.MainOutMessage("[EXCEPT] PAtchSendDate");
                    }
                    pInfo = null;
                }
            }
        }

        public void Run()
        {
            int i;
            int k;
            long start;
            TRunGateInfo pgate;
            start = HUtil32.GetTickCount();
            if (M2Share.ServerReady)
            {
                try
                {
                    PatchSendData();
                    if (M2Share.GATELOAD > 0)
                    {
                        if (HUtil32.GetTickCount() - gateloadtesttime >= 100)
                        {
                            gateloadtesttime = HUtil32.GetTickCount();
                            for (i = 0; i < RunSock.MAXGATE; i++)
                            {
                                pgate = GateArr[i];
                                if (pgate.SendBuffers != null)
                                {
                                    for (k = 0; k < M2Share.GATELOAD; k++)
                                    {
                                        SendGateLoadTest(i);
                                    }
                                }
                            }
                        }
                    }
                    for (i = 0; i < RunSock.MAXGATE; i++)
                    {
                        // 焊尘波 贸府
                        pgate = GateArr[i];
                        if (pgate.SendBuffers != null)
                        {
                            pgate.curbuffercount = pgate.SendBuffers.Count;
                            if (!SendGateBuffers(i, pgate, pgate.SendBuffers))
                            {
                                pgate.remainbuffercount = pgate.SendBuffers.Count;
                                break;
                            }
                            else
                            {
                                pgate.remainbuffercount = pgate.SendBuffers.Count;
                            }
                        }
                    }
                    for (i = 0; i < RunSock.MAXGATE; i++)
                    {
                        if (GateArr[i].Socket != null)
                        {
                            pgate = GateArr[i];
                            if (HUtil32.GetTickCount() - pgate.sendchecktime >= 1000)
                            {
                                pgate.sendchecktime = HUtil32.GetTickCount();
                                pgate.sendbytes = pgate.worksendbytes;
                                pgate.sendsoccount = pgate.worksendsoccount;
                                pgate.worksendbytes = 0;
                                pgate.worksendsoccount = 0;
                            }
                            if (GateArr[i].NeedCheck)
                            {
                                GateArr[i].NeedCheck = false;
                                SendGateCheck(GateArr[i].Socket, Grobal2.GM_CHECKSERVER);
                            }
                        }
                    }
                }
                catch
                {
                    M2Share.MainOutMessage("[RunSock] TRunSocket.Run exception");
                }
            }
            M2Share.cursoctime = (int)(HUtil32.GetTickCount() - start);
            if (M2Share.cursoctime > M2Share.maxsoctime)
            {
                M2Share.maxsoctime = M2Share.cursoctime;
            }
        }

        public void Dispose(object obj)
        {
            if (obj != null)
            {
                obj = null;
            }
        }
    }
}

namespace GameSvr
{
    public class RunSock
    {
        public const int SERVERBASEPORT = 5000;
        public const int MAXGATE = 20;
        public const string GATEADDRFILE = ".\\!runaddr.txt";
        // ADDRTABLEFILE = '.\!addrtable.txt';
        public const int MAX_PUBADDR = 30;
    } // end RunSock
}