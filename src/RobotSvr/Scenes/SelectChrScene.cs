using System;
using SystemModule;
using SystemModule.Sockets;

namespace RobotSvr
{
    public class SelectChrScene : Scene
    {
        private readonly IClientScoket ClientSocket;
        private int NewIndex = 0;
        public readonly SelChar[] ChrArr;

        public SelectChrScene(RobotClient robotClient) : base(SceneType.stSelectChr, robotClient)
        {
            ChrArr = new SelChar[2];
            ChrArr[0].FreezeState = true;
            ChrArr[0].UserChr = new TUserCharacterInfo();
            ChrArr[1].FreezeState = true;
            ChrArr[1].UserChr = new TUserCharacterInfo();
            NewIndex = 0;
            ClientSocket = new IClientScoket();
            ClientSocket.OnConnected += CSocketConnect;
            ClientSocket.OnDisconnected += CSocketDisconnect;
            ClientSocket.ReceivedDatagram += CSocketRead;
            ClientSocket.OnError += CSocketError;
        }

        public override void OpenScene()
        {
            m_ConnectionStep = TConnectionStep.cnsQueryChr;
            ClientSocket.Connect(MShare.g_sSelChrAddr, MShare.g_nSelChrPort);
            Console.WriteLine("OpenScene");
        }

        public override void CloseScene()
        {
            SetNotifyEvent(CloseSocket, 1000);
        }

        public void SelChrStartClick()
        {
            string chrname = "";
            if (ChrArr[0].Valid && ChrArr[0].Selected)
            {
                chrname = ChrArr[0].UserChr.Name;
            }
            if (ChrArr[1].Valid && ChrArr[1].Selected)
            {
                chrname = ChrArr[1].UserChr.Name;
            }
            if (!string.IsNullOrEmpty(chrname))
            {
                SendSelChr(chrname);
            }
            else
            {
                Console.WriteLine("开始游戏前你应该先创建一个新角色！点击<创建角色>按钮创建一个游戏角色。");
            }
        }

        private void SendSelChr(string chrname)
        {
            robotClient.m_sCharName = chrname;
            ClientPacket msg = Grobal2.MakeDefaultMsg(Grobal2.CM_SELCHR, 0, 0, 0, 0);
            SendSocket(EDcode.EncodeMessage(msg) + EDcode.EncodeString(robotClient.LoginID + "/" + chrname));
            MainOutMessage($"选择角色 {chrname}");
        }

        public void SelChrNewChrClick()
        {
            if (!ChrArr[0].Valid || !ChrArr[1].Valid)
            {
                if (!ChrArr[0].Valid)
                {
                    MakeNewChar(0);
                }
                else
                {
                    MakeNewChar(1);
                }
            }
            else
            {
                Console.WriteLine("一个帐号最多只能创建 2 个游戏角色！");
            }
        }

        public void SelChrEraseChrClick()
        {
            var n = 0;
            if (ChrArr[0].Valid && ChrArr[0].Selected)
            {
                n = 0;
            }
            if (ChrArr[1].Valid && ChrArr[1].Selected)
            {
                n = 1;
            }
            if (ChrArr[n].Valid && (!ChrArr[n].FreezeState) && (ChrArr[n].UserChr.Name != ""))
            {
                SendDelChr(ChrArr[n].UserChr.Name);
            }
        }

        private void SendDelChr(string chrname)
        {
            ClientPacket msg = Grobal2.MakeDefaultMsg(Grobal2.CM_DELCHR, 0, 0, 0, 0);
            SendSocket(EDcode.EncodeMessage(msg) + EDcode.EncodeString(chrname));
        }

        private void ClearChrs()
        {
            ChrArr[0].FreezeState = false;
            ChrArr[1].FreezeState = true;
            ChrArr[0].Selected = true;
            ChrArr[1].Selected = false;
            ChrArr[0].UserChr.Name = "";
            ChrArr[1].UserChr.Name = "";
        }

        private void AddChr(string uname, int job, int hair, int level, int sex)
        {
            int n;
            if (!ChrArr[0].Valid)
            {
                n = 0;
            }
            else if (!ChrArr[1].Valid)
            {
                n = 1;
            }
            else
            {
                return;
            }
            ChrArr[n].UserChr.Name = uname;
            ChrArr[n].UserChr.Job = (byte)job;
            ChrArr[n].UserChr.hair = (byte)hair;
            ChrArr[n].UserChr.Level = (byte)level;
            ChrArr[n].UserChr.Sex = (byte)sex;
            ChrArr[n].Valid = true;
        }

        private void MakeNewChar(int index)
        {
            NewIndex = index;
            ChrArr[NewIndex].Valid = true;
            ChrArr[NewIndex].FreezeState = false;
        }

        public override void PlayScene()
        {
            //if (MShare.g_boOpenAutoPlay && (MShare.g_nAPReLogon == 2))
            //{
            //    if (MShare.GetTickCount() - MShare.g_nAPReLogonWaitTick > MShare.g_nAPReLogonWaitTime)
            //    {
            //        MShare.g_nAPReLogonWaitTick = MShare.GetTickCount();
            //        MShare.g_nAPReLogon = 3;
            //        SendSelChr(robotClient.m_sCharName);
            //    }
            //}
        }

        public void ClientGetReceiveChrs(string body)
        {
            string uname = string.Empty;
            string sjob = string.Empty;
            string shair = string.Empty;
            string slevel = string.Empty;
            string ssex = string.Empty;
            if (MShare.g_boOpenAutoPlay && (MShare.g_nAPReLogon == 1))
            {
                MShare.g_nAPReLogon = 2;
                MShare.g_nAPReLogonWaitTick = MShare.GetTickCount();
                MShare.g_nAPReLogonWaitTime = 5000 + RandomNumber.GetInstance().Random(10) * 1000;
            }
            ClearChrs();
            string Str = EDcode.DeCodeString(body);
            int select = 0;
            int nChrCount = 0;
            for (var i = 0; i <= 1; i++)
            {
                Str = HUtil32.GetValidStr3(Str, ref uname, new string[] { "/" });
                Str = HUtil32.GetValidStr3(Str, ref sjob, new string[] { "/" });
                Str = HUtil32.GetValidStr3(Str, ref shair, new string[] { "/" });
                Str = HUtil32.GetValidStr3(Str, ref slevel, new string[] { "/" });
                Str = HUtil32.GetValidStr3(Str, ref ssex, new string[] { "/" });
                if ((uname != "") && (slevel != "") && (ssex != ""))
                {
                    if (uname[0] == '*')
                    {
                        select = i;
                        uname = uname.Substring(1, uname.Length - 1);
                    }
                    AddChr(uname, HUtil32.Str_ToInt(sjob, 0), HUtil32.Str_ToInt(shair, 0), HUtil32.Str_ToInt(slevel, 0), HUtil32.Str_ToInt(ssex, 0));
                    nChrCount++;
                }
                if (select == 0)
                {
                    this.ChrArr[0].FreezeState = false;
                    this.ChrArr[0].Selected = true;
                    this.ChrArr[1].FreezeState = true;
                    this.ChrArr[1].Selected = false;
                }
                else
                {
                    this.ChrArr[0].FreezeState = true;
                    this.ChrArr[0].Selected = false;
                    this.ChrArr[1].FreezeState = false;
                    this.ChrArr[1].Selected = true;
                }
            }
            if (nChrCount > 0)
            {
                SendSelChr(ChrArr[select].UserChr.Name);
            }
            else
            {
                SetNotifyEvent(NewChr, 3000);
            }
        }

        private void NewChr()
        {
            m_ConnectionStep = TConnectionStep.cnsNewChr;
            SelectChrCreateNewChr(robotClient.m_sCharName);
        }

        private void SelectChrCreateNewChr(string sCharName)
        {
            byte sHair = 0;
            switch (RandomNumber.GetInstance().Random(1))
            {
                case 0:
                    sHair = 2;
                    break;
                case 1:
                    switch (new Random(1).Next())
                    {
                        case 0:
                            sHair = 1;
                            break;
                        case 1:
                            sHair = 3;
                            break;
                    }
                    break;
            }
            byte sJob = (byte)RandomNumber.GetInstance().Random(2);
            byte sSex = (byte)RandomNumber.GetInstance().Random(1);
            SendNewChr(robotClient.LoginID, sCharName, sHair, sJob, sSex);
            MainOutMessage($"创建角色 {sCharName}");
        }

        public void ClientGetStartPlay(string body)
        {
            MainOutMessage("准备进入游戏");
            string addr = string.Empty;
            string Str = EDcode.DeCodeString(body);
            string sport = HUtil32.GetValidStr3(Str, ref addr, new string[] { "/" });
            MShare.g_nRunServerPort = HUtil32.Str_ToInt(sport, 0);
            MShare.g_sRunServerAddr = addr;
            MShare.g_ConnectionStep = TConnectionStep.cnsPlay;
        }

        private void CloseSocket()
        {
            ClientSocket.Disconnect();
        }

        public void ClientGetReconnect(string body)
        {
            //string addr = string.Empty;
            //string sport = string.Empty;
            //string Str = EDcode.DeCodeString(body);
            //sport = HUtil32.GetValidStr3(Str, ref addr, new string[] { "/" });
            //MShare.g_boServerChanging = true;
            //MShare.g_ConnectionStep = TConnectionStep.cnsPlay;
            //CloseSocket();//断开游戏网关链接
            //ClientSocket.Host = addr;
            //ClientSocket.Port = HUtil32.Str_ToInt(sport, 0);
            //ClientSocket.Connect();
            //robotClient.SocStr = string.Empty;
            //robotClient.BufferStr = string.Empty;
        }

        private void SendNewChr(string uid, string uname, byte shair, byte sjob, byte ssex)
        {
            var msg = Grobal2.MakeDefaultMsg(Grobal2.CM_NEWCHR, 0, 0, 0, 0);
            SendSocket(EDcode.EncodeMessage(msg) + EDcode.EncodeString(uid + "/" + uname + "/" + shair + "/" + sjob + "/" + ssex));
        }

        public void SendQueryChr()
        {
            m_ConnectionStep = TConnectionStep.cnsQueryChr;
            var DefMsg = Grobal2.MakeDefaultMsg(Grobal2.CM_QUERYCHR, 0, 0, 0, 0);
            SendSocket(EDcode.EncodeMessage(DefMsg) + EDcode.EncodeString(robotClient.LoginID + "/" + robotClient.Certification));
            MainOutMessage("查询角色.");
        }

        private void SendSocket(string sendstr)
        {
            if (ClientSocket.IsConnected)
            {
                ClientSocket.SendText($"#1{sendstr}!");
            }
            else
            {
                MainOutMessage($"Socket Close {ClientSocket.Host}:{ClientSocket.Port}");
            }
        }

        #region Socket Events

        private void CSocketConnect(object sender, DSCClientConnectedEventArgs e)
        {
            Console.WriteLine("123123");
            MShare.g_boServerConnected = true;
            if (m_ConnectionStep == TConnectionStep.cnsQueryChr)
            {
                SetNotifyEvent(SendQueryChr, 1000);
                m_ConnectionStep = TConnectionStep.cnsSelChr;
            }
            robotClient.SocStr = string.Empty;
            robotClient.BufferStr = "";
            MainOutMessage($"连接角色网关:[{MShare.g_sSelChrAddr}:{MShare.g_nSelChrPort}]");
        }

        private void CSocketDisconnect(object sender, DSCClientConnectedEventArgs e)
        {
            MShare.g_boServerConnected = false;
            if (MShare.g_SoftClosed)
            {
                MShare.g_SoftClosed = false;
            }
            ClientManager.DelClient(robotClient.SessionId);
        }

        private void CSocketError(object sender, DSCClientErrorEventArgs e)
        {
            switch (e.ErrorCode)
            {
                case System.Net.Sockets.SocketError.ConnectionRefused:
                    Console.WriteLine($"游戏服务器[{ClientSocket.RemoteEndPoint}拒绝链接...");
                    break;
                case System.Net.Sockets.SocketError.ConnectionReset:
                    Console.WriteLine($"游戏服务器[{ClientSocket.RemoteEndPoint}关闭连接...");
                    break;
                case System.Net.Sockets.SocketError.TimedOut:
                    Console.WriteLine($"游戏服务器[{ClientSocket.RemoteEndPoint}链接超时...");
                    break;
            }
            ClientManager.DelClient(robotClient.SessionId);
        }

        private void CSocketRead(object sender, DSCClientDataInEventArgs e)
        {
            string sData = HUtil32.GetString(e.Buff, 0, e.BuffLen);
            if (!string.IsNullOrEmpty(sData))
            {
                robotClient.SocStr += sData;
                ClientManager.AddPacket(robotClient.SessionId, sData);
            }
        }

        #endregion

    }
}