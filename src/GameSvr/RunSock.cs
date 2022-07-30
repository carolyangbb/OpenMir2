using System;
using System.Collections;
using System.Net.Sockets;
using SystemModule;

namespace GameSvr
{
    public struct TRAddr
    {
        public string[] RemoteAddr;
        public string[] PublicAddr;
    }

    public struct TRunGateInfo
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

    public struct TUserInfo
    {
        public string UserId;
        public string UserName;
        public string UserAddress;
        public int UserHandle;
        public int UserGateIndex;
        public int Certification;
        public int ClientVersion;
        public Object UEngine;
        public Object FEngine;
        public Object UCret;
        public long OpenTime;
        public bool Enabled;
    }

    public struct TRunCmdInfo
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
        private object FCmdCS = null;
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
            TRunCmdInfo pInfo;
            svMain.ruLock.Free();
            svMain.socstrLock.Free();
            RunAddressList.Free();
            if (FCmdList != null)
            {
                for (i = 0; i < FCmdList.Count; i++)
                {
                    pInfo = (TRunCmdInfo)FCmdList[0];
                    if (pInfo != null)
                    {
                        if (pInfo.pbuff != null)
                        {
                            freeMem(pInfo.pbuff);
                        }
                        dispose(pInfo);
                    }
                    FCmdList.RemoveAt(0);
                }
                FCmdList.Free();
            }
            FCmdCS.Free();
            base.Destroy();
        }
        private void LoadRunAddress()
        {
            RunAddressList.LoadFromFile(Units.RunSock.GATEADDRFILE);
            CheckListValid(RunAddressList);
        }

        public bool IsValidGateAddr(string addr)
        {
            bool result;
            int i;
            try
            {
                result = false;
                svMain.ruLock.Enter();
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
                svMain.ruLock.Leave();
            }
            return result;
        }

        private string GetPublicAddr(string raddr)
        {
            string result;
            int i;
            result = raddr;
            for (i = 0; i < MaxPubAddr; i++)
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
            int i;
            string remote;
            if (svMain.ServerReady)
            {
                remote = Socket.RemoteAddress;
                for (i = 0; i < Units.RunSock.MAXGATE; i++)
                {
                    if (!GateArr[i].Connected)
                    {
                        GateArr[i].Connected = true;
                        GateArr[i].Socket = Socket;
                        GateArr[i].PublicAddress = GetPublicAddr(remote);
                        GateArr[i].Status = 1;
                        GateArr[i].UserList = new ArrayList();
                        GateArr[i].ReceiveBuffer = null;
                        GateArr[i].ReceiveLen = 0;
                        GateArr[i].SendBuffers = new ArrayList();
                        GateArr[i].NeedCheck = false;
                        GateArr[i].GateSyncMode = 0;
                        GateArr[i].SendDataCount = 0;
                        GateArr[i].SendDataTime = GetTickCount;
                        svMain.MainOutMessage("Gate " + i.ToString() + " Opened..");
                        break;
                    }
                }
            }
            else
            {
                svMain.MainOutMessage("Not ready " + Socket.RemoteAddress);
                Socket.Close;
            }
        }

        public void Disconnect(Socket Socket)
        {
            CloseGate(Socket);
        }

        public void SocketError(Socket Socket, ref int ErrorCode)
        {
            if (Socket.Connected)
            {
                Socket.Close;
            }
            ErrorCode = 0;
        }

        public void SocketRead(Socket Socket)
        {
            int i;
            int len;
            string p;
            for (i = 0; i < Units.RunSock.MAXGATE; i++)
            {
                if (GateArr[i].Socket == Socket)
                {
                    try
                    {
                        len = Socket.ReceiveLength;
                        GetMem(p, len);
                        Socket.ReceiveBuf(p, len);
                        ExecGateBuffers(i, GateArr[i] as TRunGateInfo, p, len);
                        FreeMem(p);
                    }
                    catch
                    {
                        svMain.MainOutMessage("Exception] SocketRead");
                    }
                    break;
                }
            }
        }

        public void CloseAllGate()
        {
            int i;
            for (i = 0; i < Units.RunSock.MAXGATE; i++)
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
            TUserInfo uinf;
            try
            {
                svMain.ruLock.Enter();
                for (i = 0; i < Units.RunSock.MAXGATE; i++)
                {
                    if (GateArr[i].Socket == Socket)
                    {
                        ulist = GateArr[i].UserList;
                        for (j = 0; j < ulist.Count; j++)
                        {
                            uinf = ulist[j] as TUserInfo;
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
                            FreeMem(GateArr[i].ReceiveBuffer);
                        }
                        GateArr[i].ReceiveBuffer = null;
                        GateArr[i].ReceiveLen = 0;
                        for (j = 0; j < GateArr[i].SendBuffers.Count; j++)
                        {
                            FreeMem(GateArr[i].SendBuffers[j]);
                        }
                        GateArr[i].SendBuffers.Free();
                        GateArr[i].SendBuffers = null;
                        GateArr[i].Connected = false;
                        GateArr[i].Socket = null;
                        svMain.MainOutMessage("Gate " + i.ToString() + " Closed..");
                        break;
                    }
                }
            }
            finally
            {
                svMain.ruLock.Leave();
            }
        }

        // ruLock 안에서 호출되어야함
        // UserList의 index를 리턴함
        private int OpenNewUser(int shandle, int uindex, string addr, ArrayList ulist)
        {
            int result;
            int i;
            TUserInfo uinfo;
            uinfo = new TUserInfo();
            uinfo.UserId = "";
            uinfo.UserName = "";
            uinfo.UserAddress = addr;
            uinfo.UserHandle = shandle;
            uinfo.UserGateIndex = uindex;
            uinfo.Certification = 0;
            uinfo.UEngine = null;
            uinfo.FEngine = null;
            uinfo.UCret = null;
            uinfo.OpenTime = GetTickCount;
            uinfo.Enabled = false;
            for (i = 0; i < ulist.Count; i++)
            {
                if (ulist[i] == null)
                {
                    ulist[i] = uinfo;
                    // 중간에 빠진곳에 넣음
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
            TUserInfo puser;
            if (!(gateindex >= 0 && gateindex <= Units.RunSock.MAXGATE - 1))
            {
                return;
            }
            if (GateArr[gateindex].UserList == null)
            {
                return;
            }
            try
            {
                svMain.ruLock.Enter();
                try
                {
                    for (i = 0; i < GateArr[gateindex].UserList.Count; i++)
                    {
                        if (GateArr[gateindex].UserList[i] == null)
                        {
                            continue;
                        }
                        if ((GateArr[gateindex].UserList[i] as TUserInfo).UserHandle == uhandle)
                        {
                            puser = GateArr[gateindex].UserList[i] as TUserInfo;
                            // Close 조건 다시 생각해야 함.
                            try
                            {
                                if (puser.FEngine != null)
                                {
                                    ((TFrontEngine)puser.FEngine).UserSocketHasClosed(gateindex, puser.UserHandle);
                                }
                            }
                            catch
                            {
                                svMain.MainOutMessage("[RunSock] TRunSocket.CloseUser exception 1");
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
                                svMain.MainOutMessage("[RunSock] TRunSocket.CloseUser exception 2");
                            }
                            try
                            {
                                if (puser.UCret != null)
                                {
                                    if (((TCreature)puser.UCret).BoGhost)
                                    {
                                        // 사용자종료가 아닌, 서버의 강제종료인경우
                                        if (!((TUserHuman)puser.UCret).SoftClosed)
                                        {
                                            // 캐릭터 선택으로 빠진것이 아니면
                                            // 다른 서버에 알린다.
                                            IdSrvClient.FrmIDSoc.SendUserClose(puser.UserId, puser.Certification);
                                        }
                                    }
                                }
                            }
                            catch
                            {
                                svMain.MainOutMessage("[RunSock] TRunSocket.CloseUser exception 3");
                            }
                            try
                            {
                                // 제거.. 제거하지 않는다.
                                // GateArr[gateindex].UserList.Delete (i);
                                Dispose(puser);
                                GateArr[gateindex].UserList[i] = null;
                            }
                            catch
                            {
                                svMain.MainOutMessage("[RunSock] TRunSocket.CloseUser exception 4");
                            }
                            break;
                        }
                    }
                }
                catch
                {
                    svMain.MainOutMessage("[RunSock] TRunSocket.CloseUser exception");
                }
            }
            finally
            {
                svMain.ruLock.Leave();
            }
        }

        public void SendGateLoadTest(int gindex)
        {
            TDefaultMessage def;
            int packetlen;
            TMsgHeader header;
            string pbuf;
            header.Code = (int)0xaa55aa55;
            header.SNumber = 0;
            header.Ident = Grobal2.GM_TEST;
            pbuf = null;
            header.length = 80;
            packetlen = sizeof(TMsgHeader) + header.length;
            GetMem(pbuf, packetlen + 4);
            Move(packetlen, pbuf, 4);
            Move(header, pbuf[4], sizeof(TMsgHeader));
            Move(def, pbuf[4 + sizeof(TMsgHeader)], sizeof(TDefaultMessage));
            SendUserSocket(gindex, pbuf);
        }

        // 동기화 맞춰야함
        public void SendForcedClose(int gindex, int uhandle)
        {
            TDefaultMessage def;
            int packetlen;
            TMsgHeader header;
            string pbuf;
            def = Grobal2.MakeDefaultMsg(Grobal2.SM_OUTOFCONNECTION, 0, 0, 0, 0);
            pbuf = null;
            header.Code = (int)0xaa55aa55;
            header.SNumber = uhandle;
            header.Ident = Grobal2.GM_DATA;
            header.length = sizeof(TDefaultMessage);
            packetlen = sizeof(TMsgHeader) + header.length;
            GetMem(pbuf, packetlen + 4);
            Move(packetlen, pbuf, 4);
            Move(header, pbuf[4], sizeof(TMsgHeader));
            Move(def, pbuf[4 + sizeof(TMsgHeader)], sizeof(TDefaultMessage));
            SendUserSocket(gindex, pbuf);
        }

        public void SendGateCheck(Socket socket, int msg)
        {
            TMsgHeader header;
            if (socket.Connected)
            {
                header.Code = (int)0xaa55aa55;
                header.SNumber = 0;
                header.Ident = (ushort)msg;
                header.length = 0;
                if (socket != null)
                {
                    socket.SendBuf(header, sizeof(TMsgHeader));
                }
            }
        }

        public void SendServerUserIndex(Socket socket, int shandle, int gindex, int index)
        {
            TMsgHeader header;
            if (socket.Connected)
            {
                header.Code = (int)0xaa55aa55;
                header.SNumber = shandle;
                header.UserGateIndex = (ushort)gindex;
                header.Ident = Grobal2.GM_SERVERUSERINDEX;
                header.UserListIndex = (ushort)index;
                header.length = 0;
                if (socket != null)
                {
                    socket.SendBuf(header, sizeof(TMsgHeader));
                }
            }
        }

        // 다른 서버에서 재접속 되었을 경우, 혹은 다른 서버에 이상이 생김..
        public void CloseUserId(string uid, int cert)
        {
            int gi;
            int k;
            TUserInfo pu;
            for (gi = 0; gi < Units.RunSock.MAXGATE; gi++)
            {
                if (GateArr[gi].Connected && (GateArr[gi].Socket != null) && (GateArr[gi].UserList != null))
                {
                    try
                    {
                        svMain.ruCloseLock.Enter();
                        for (k = 0; k < GateArr[gi].UserList.Count; k++)
                        {
                            pu = GateArr[gi].UserList[k] as TUserInfo;
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
                                    // 강제 종료 메세지를 클라이언트에 보낸다.
                                    SendForcedClose(gi, pu.UserHandle);
                                }
                                // GateArr[gi].UserList.Delete (k); 제거하지 않음
                                Dispose(pu);
                                GateArr[gi].UserList[k] = null;
                                break;
                            }
                        }
                    }
                    finally
                    {
                        svMain.ruCloseLock.Leave();
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
                svMain.ruSendLock.Enter();
                if (gindex >= 0 && gindex <= Units.RunSock.MAXGATE - 1)
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
                svMain.ruSendLock.Leave();
            }
            if (!flag)
            {
                try
                {
                    FreeMem(pbuf);
                }
                finally
                {
                    pbuf = null;
                }
            }
        }

        public void UserLoadingOk(int gateindex, int shandle, Object cret)
        {
            int i;
            TUserInfo puser;
            if (gateindex >= 0 && gateindex <= Units.RunSock.MAXGATE - 1)
            {
                if (GateArr[gateindex].UserList == null)
                {
                    return;
                }
                try
                {
                    svMain.ruLock.Enter();
                    for (i = 0; i < GateArr[gateindex].UserList.Count; i++)
                    {
                        puser = GateArr[gateindex].UserList[i];
                        if (puser == null)
                        {
                            continue;
                        }
                        if (puser.UserHandle == shandle)
                        {
                            puser.FEngine = null;
                            puser.UEngine = svMain.UserEngine;
                            puser.UCret = cret;
                            break;
                        }
                    }
                }
                finally
                {
                    svMain.ruLock.Leave();
                }
            }
        }

        public bool DoClientCertification_GetCertification(string body, ref string uid, ref string chrname, ref int certify, ref int clversion, ref int clientchecksum, ref bool startnew)
        {
            bool result;
            string str;
            string scert;
            string sver;
            string start;
            string sxorcert;
            string checksum;
            string sxor2;
            long checkcert;
            long xor1;
            long xor2;
            result = false;
            try
            {
                str = EDcode.DecodeString(body);
                if (str.Length > 2)
                {
                    if ((str[1] == "*") && (str[2] == "*"))
                    {
                        str = str.Substring(3 - 1, str.Length - 2);
                        str = GetValidStr3(str, uid, new string[] { "/" });
                        str = GetValidStr3(str, chrname, new string[] { "/" });
                        str = GetValidStr3(str, scert, new string[] { "/" });
                        str = GetValidStr3(str, sver, new string[] { "/" });
                        str = GetValidStr3(str, sxorcert, new string[] { "/" });
                        str = GetValidStr3(str, checksum, new string[] { "/" });
                        str = GetValidStr3(str, sxor2, new string[] { "/" });
                        start = str;
                        certify = HUtil32.Str_ToInt(scert, 0);
                        checkcert = certify;
                        xor1 = HUtil32.Str_ToInt64(sxorcert, 0);
                        xor2 = HUtil32.Str_ToInt64(sxor2, 0);
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
                svMain.MainOutMessage("[RunSock] TRunSocket.DoClientCertification.GetCertification exception ");
            }
            return result;
        }

        // ruLock 안에서 호출되는 함수 임.
        private void DoClientCertification(int gindex, TUserInfo puser, int shandle, string data)
        {
            string uid;
            string chrname;
            int certify;
            int clversion;
            int loginclientversion;
            int clcheck;
            int bugstep;
            int certmode;
            int availmode;
            bool startnew;
            bugstep = 0;
            try
            {
                if (puser.UserId == "")
                {
                    if (CharCount(data, "!") >= 1)
                    {
                        ArrestStringEx(data, "#", "!", data);
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
                                    // PayMode
                                    svMain.FrontEngine.LoadPlayer(uid, chrname, puser.UserAddress, startnew, certify, certmode, availmode, clversion, loginclientversion, clcheck, shandle, puser.UserGateIndex, gindex);
                                    // CurGateIndex);
                                }
                                catch
                                {
                                    svMain.MainOutMessage("[RunSock] LoadPlay... TRunSocket.DoClientCertification exception");
                                }
                            }
                            else
                            {
                                // 인증 실패
                                bugstep = 2;
                                puser.UserId = "* disable *";
                                puser.Enabled = false;
                                CloseUser(gindex, shandle);
                                // CurGateIndex, shandle);
                                bugstep = 3;
                                // MainOutMessage ('Fail admission: "' + data + '"');
                                svMain.MainOutMessage("Fail admission:1<" + puser.UserAddress + "><" + availmode.ToString() + ">");
                                if (startnew)
                                {
                                    svMain.MainOutMessage("Fail admission:2<" + certmode.ToString() + "><" + uid + "><" + chrname + "><" + certify.ToString() + "><" + clversion.ToString() + "><" + clcheck.ToString() + "><T>");
                                }
                                else
                                {
                                    svMain.MainOutMessage("Fail admission:2<" + certmode.ToString() + "><" + uid + "><" + chrname + "><" + certify.ToString() + "><" + clversion.ToString() + "><" + clcheck.ToString() + "><F>");
                                }
                            }
                        }
                        else
                        {
                            bugstep = 4;
                            puser.UserId = "* disable *";
                            puser.Enabled = false;
                            CloseUser(gindex, shandle);
                            // CurGateIndex, shandle);
                            bugstep = 5;
                            svMain.MainOutMessage("invalid admission: \"" + data + "\"");
                        }
                    }
                }
            }
            catch
            {
                svMain.MainOutMessage("[RunSock] TRunSocket.DoClientCertification exception " + bugstep.ToString());
            }
        }

        private void ExecGateMsg(int gindex, TRunGateInfo CGate, TMsgHeader pheader, string pdata, int len)
        {
            int i;
            int uidx;
            int debug;
            TUserInfo puser;
            debug = 0;
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
                        // 1은 기본값
                        debug = 2;
                        puser = null;
                        // -------------------
                        for (i = 0; i < CGate.UserList.Count; i++)
                        {
                            puser = CGate.UserList[i] as TUserInfo;
                            if (puser != null)
                            {
                                if (puser.UserHandle == pheader.SNumber)
                                {
                                    // 끊어질때 IP Address 비교(sonmg)
                                    // if CompareText(puser.UserAddress, StrPas(pdata)) <> 0 then
                                    // MainOutMessage('[IP Address Not Match] ' + puser.UserId + ' ' + puser.UserName + ' ' + puser.UserAddress + '->' + StrPas(pdata));
                                }
                            }
                        }
                        // -------------------
                        CloseUser(gindex, pheader.SNumber);
                        break;
                    case Grobal2.GM_CHECKCLIENT:
                        debug = 3;
                        CGate.NeedCheck = true;
                        break;
                    case Grobal2.GM_RECEIVE_OK:
                        debug = 4;
                        CGate.GateSyncMode = 0;
                        // Sync GOOD
                        CGate.SendDataCount = 0;
                        break;
                    case Grobal2.GM_DATA:
                        // CGate.SendDataCount - CGate.SendCheckTimeCount;
                        debug = 5;
                        puser = null;
                        if (pheader.UserListIndex >= 1)
                        {
                            uidx = pheader.UserListIndex - 1;
                            if (uidx < CGate.UserList.Count)
                            {
                                // 리스트가 중간에 빠질 수도 있음..
                                puser = CGate.UserList[uidx] as TUserInfo;
                                if (puser != null)
                                {
                                    if (puser.UserHandle != pheader.SNumber)
                                    {
                                        // 재 확인
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
                                if ((CGate.UserList[i] as TUserInfo).UserHandle == pheader.SNumber)
                                {
                                    puser = CGate.UserList[i] as TUserInfo;
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
                                    if (len >= sizeof(TDefaultMessage))
                                    {
                                        if (len == sizeof(TDefaultMessage))
                                        {
                                            svMain.UserEngine.ProcessUserMessage((TUserHuman)puser.UCret, (TDefaultMessage)pdata, null);
                                        }
                                        else
                                        {
                                            svMain.UserEngine.ProcessUserMessage((TUserHuman)puser.UCret, (TDefaultMessage)pdata, pdata[sizeof(TDefaultMessage)]);
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
                svMain.MainOutMessage("[Exception] ExecGateMsg.. " + debug.ToString());
            }
        }

        private void ExecGateBuffers(int gindex, TRunGateInfo CGate, string pBuffer, int buflen)
        {
            int len;
            string pwork;
            string pbody;
            string ptemp;
            TMsgHeader pheader;
            pwork = null;
            len = 0;
            try
            {
                if (pBuffer != null)
                {
                    ReAllocMem(CGate.ReceiveBuffer, CGate.ReceiveLen + buflen);
                    Move(pBuffer, CGate.ReceiveBuffer[CGate.ReceiveLen], buflen);
                }
            }
            catch
            {
                svMain.MainOutMessage("Exception] ExecGateBuffers->pBuffer");
            }
            try
            {
                len = CGate.ReceiveLen + buflen;
                pwork = CGate.ReceiveBuffer;
                // pwork
                while (len >= sizeof(TMsgHeader))
                {
                    pheader = (TMsgHeader)pwork;
                    if (pheader.Code == 0xaa55aa55)
                    {
                        if (len < sizeof(TMsgHeader) + pheader.length)
                        {
                            break;
                        }
                        pbody = pwork[sizeof(TMsgHeader)];
                        // .....pheader, pbody, pheader.Length...
                        ExecGateMsg(gindex, CGate, pheader, pbody, pheader.length);
                        pwork = pwork[sizeof(TMsgHeader) + pheader.length];
                        len = len - (sizeof(TMsgHeader) + pheader.length);
                    }
                    else
                    {
                        pwork = pwork[1];
                        len -= 1;
                    }
                }
            }
            catch
            {
                svMain.MainOutMessage("Exception] ExecGateBuffers->@pwork,ExecGateMsg");
            }
            try
            {
                if (len > 0)
                {
                    // 남았음
                    GetMem(ptemp, len);
                    Move(pwork, ptemp, len);
                    FreeMem(CGate.ReceiveBuffer);
                    CGate.ReceiveBuffer = ptemp;
                    // psrc 나머지만 있음
                    CGate.ReceiveLen = len;
                }
                else
                {
                    FreeMem(CGate.ReceiveBuffer);
                    CGate.ReceiveBuffer = null;
                    CGate.ReceiveLen = 0;
                }
            }
            catch
            {
                svMain.MainOutMessage("Exception] ExecGateBuffers->FreeMem");
            }
        }

        private bool SendGateBuffers(int gindex, TRunGateInfo CGate, ArrayList slist)
        {
            bool result;
            int curn;
            int len;
            int newlen;
            int totlen;
            int sendlen;
            string psend;
            string pnew;
            string pwork;
            long start;
            result = true;
            if (slist.Count == 0)
            {
                return result;
            }
            start = GetTickCount;
            if (CGate.GateSyncMode > 0)
            {
                if (GetTickCount - CGate.WaitTime > 2000)
                {
                    // 타임 아웃
                    CGate.GateSyncMode = 0;
                    CGate.SendDataCount = 0;
                }
                // if CurGate.GateSyncMode >= 2 then begin
                // CurGate.GateSyncMode := 2; //breakpoint 때매
                return result;
                // end;
            }
            // 패킷 최적화
            try
            {
                curn = 0;
                psend = (string)slist[curn];
                // 항상 slist.Count > 0 임.
                while (true)
                {
                    if (curn + 1 >= slist.Count)
                    {
                        break;
                    }
                    pwork = (string)slist[curn + 1];
                    // 바로전 블럭이 SENDBLOCK 보다 작으면 합한다.
                    Move(psend, len, 4);
                    Move(pwork, newlen, 4);
                    if (len + newlen < svMain.SENDBLOCK)
                    {
                        slist.RemoveAt(curn + 1);
                        // 작은 블럭은 모아서 한꺼번에 보낸다.
                        // ReallocMem (psend, 4 + len + newlen);
                        GetMem(pnew, 4 + len + newlen);
                        totlen = len + newlen;
                        Move(totlen, pnew, 4);
                        Move(psend[4], pnew[4], len);
                        Move(pwork[4], pnew[4 + len], newlen);
                        FreeMem(psend);
                        FreeMem(pwork);
                        psend = pnew;
                        slist[curn] = psend;
                    }
                    else
                    {
                        curn++;
                        psend = pwork;
                    }
                }
            }
            catch
            {
                svMain.MainOutMessage("Exception SendGateBuffers(1)..");
            }
            // 보내기
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
                    Move(psend, sendlen, 4);
                    if ((CGate.GateSyncMode == 0) && (sendlen + CGate.SendDataCount >= svMain.SENDCHECKBLOCK))
                    {
                        if ((CGate.SendDataCount == 0) && (sendlen >= svMain.SENDCHECKBLOCK))
                        {
                            // 너무 큰 데이타는 안 보낸다.
                            slist.RemoveAt(0);
                            try
                            {
                                FreeMem(psend);
                            }
                            catch
                            {
                                psend = null;
                            }
                            psend = null;
                        }
                        else
                        {
                            // 채크 신호를 보낸다.
                            // CGate.SendCheckTimeCount := CGate.SendDataCount;
                            SendGateCheck(CGate.Socket, Grobal2.GM_RECEIVE_OK);
                            CGate.GateSyncMode = 1;
                            // SENDAVAILABLEBLOCK까지 보낸 수 있음
                            CGate.WaitTime = GetTickCount;
                        }
                        break;
                    }
                    // if (CurGate.GateSyncMode = 1) and (sendlen + CurGate.SendDataCount >= SENDAVAILABLEBLOCK) then begin
                    // CurGate.GateSyncMode := 2;
                    // break;
                    // end;
                    if (psend == null)
                    {
                        continue;
                    }
                    slist.RemoveAt(0);
                    pwork = psend[4];
                    while (sendlen > 0)
                    {
                        if (sendlen >= svMain.SENDBLOCK)
                        {
                            if (CGate.Socket != null)
                            {
                                if (CGate.Socket.Connected)
                                {
                                    CGate.Socket.SendBuf(pwork, svMain.SENDBLOCK);
                                }
                                CGate.worksendsoccount = CGate.worksendsoccount + 1;
                                CGate.worksendbytes = CGate.worksendbytes + svMain.SENDBLOCK;
                            }
                            CGate.SendDataCount = CGate.SendDataCount + svMain.SENDBLOCK;
                            pwork = pwork[svMain.SENDBLOCK];
                            sendlen = sendlen - svMain.SENDBLOCK;
                        }
                        else
                        {
                            if (CGate.Socket != null)
                            {
                                if (CGate.Socket.Connected)
                                {
                                    CGate.Socket.SendBuf(pwork, sendlen);
                                }
                                CGate.worksendsoccount = CGate.worksendsoccount + 1;
                                CGate.worksendbytes = CGate.worksendbytes + sendlen;
                                CGate.SendDataCount = CGate.SendDataCount + sendlen;
                            }
                            sendlen = 0;
                            break;
                        }
                    }
                    FreeMem(psend);
                    if (GetTickCount - start > svMain.SocLimitTime)
                    {
                        result = false;
                        break;
                    }
                }
            }
            catch
            {
                svMain.MainOutMessage("Exception SendGateBuffers(2)..");
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

        // 다른쓰레드에서 넣어주는것
        public void PatchSendData()
        {
            TRunCmdInfo pInfo;
            int count;
            count = FCmdList.Count;
            pInfo = null;
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
                        dispose(pInfo);
                    }
                    catch
                    {
                        svMain.MainOutMessage("[EXCEPT] PAtchSendDate");
                    }
                    pInfo = null;
                }
            }
        }

        // 데이터 패치
        public void Run()
        {
            int i;
            int k;
            long start;
            TRunGateInfo pgate;
            start = GetTickCount;
            if (svMain.ServerReady)
            {
                try
                {
                    // Cmd 에서 온데이터 패치
                    PatchSendData();
                    // Gate Load Test
                    if (svMain.GATELOAD > 0)
                    {
                        if (GetTickCount - gateloadtesttime >= 100)
                        {
                            gateloadtesttime = GetTickCount;
                            for (i = 0; i < Units.RunSock.MAXGATE; i++)
                            {
                                // 보낼꺼 처리
                                pgate = GateArr[i] as TRunGateInfo;
                                if (pgate.SendBuffers != null)
                                {
                                    for (k = 0; k < svMain.GATELOAD; k++)
                                    {
                                        SendGateLoadTest(i);
                                    }
                                }
                            }
                        }
                    }
                    for (i = 0; i < Units.RunSock.MAXGATE; i++)
                    {
                        // 보낼꺼 처리
                        pgate = GateArr[i] as TRunGateInfo;
                        if (pgate.SendBuffers != null)
                        {
                            // CurGateIndex := i;
                            // CurGate := pgate;
                            pgate.curbuffercount = pgate.SendBuffers.Count;
                            // 현재 보낼 버퍼의 수
                            if (!SendGateBuffers(i, pgate, pgate.SendBuffers))
                            {
                                // 못보낸것이 있음, 시간초과
                                pgate.remainbuffercount = pgate.SendBuffers.Count;
                                // 보내고 남은 버퍼의 수
                                // RunGateIndex := CurGateIndex + 1;
                                // full := TRUE;
                                break;
                            }
                            else
                            {
                                pgate.remainbuffercount = pgate.SendBuffers.Count;
                                // 보내고 남은 버퍼의 수
                            }
                        }
                    }
                    // if not full then RunGateIndex := 0;
                    for (i = 0; i < Units.RunSock.MAXGATE; i++)
                    {
                        if (GateArr[i].Socket != null)
                        {
                            pgate = GateArr[i] as TRunGateInfo;
                            if (GetTickCount - pgate.sendchecktime >= 1000)
                            {
                                pgate.sendchecktime = GetTickCount;
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
                    svMain.MainOutMessage("[RunSock] TRunSocket.Run exception");
                }
            }
            svMain.cursoctime = GetTickCount - start;
            if (svMain.cursoctime > svMain.maxsoctime)
            {
                svMain.maxsoctime = svMain.cursoctime;
            }
        }
    } // end TRunSocket
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