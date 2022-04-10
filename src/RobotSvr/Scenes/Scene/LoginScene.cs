using System;
using SystemModule;
using SystemModule.Packet;
using SystemModule.Sockets;

namespace RobotSvr
{
    /// <summary>
    /// 登陆场景
    /// </summary>
    public class LoginScene : SceneBase
    {
        private readonly IClientScoket ClientSocket;
        private readonly TUserEntryAdd _mNewIdRetryAdd = null;

        public LoginScene(RobotClient robotClient) : base(SceneType.stLogin, robotClient)
        {
            ClientSocket = new IClientScoket();
            ClientSocket.OnConnected += CSocketConnect;
            ClientSocket.OnDisconnected += CSocketDisconnect;
            ClientSocket.ReceivedDatagram += CSocketRead;
            ClientSocket.OnError += CSocketError;
        }

        public override void OpenScene()
        {
            ClientSocket.Host = MShare.g_sGameIPaddr;
            ClientSocket.Port = MShare.g_nGamePort;
            SetNotifyEvent(Login, 1000);
        }

        public void Login()
        {
            if (m_ConnectionStep == TConnectionStep.cnsConnect)
            {
                if ((robotClient.m_ConnectionStatus == TConnectionStatus.cns_Failure) && (HUtil32.GetTickCount() > robotClient.m_dwConnectTick))
                {
                    robotClient.m_dwConnectTick = HUtil32.GetTickCount();
                    try
                    {
                        ClientSocket.Connect();
                        robotClient.m_ConnectionStatus = TConnectionStatus.cns_Connect;
                    }
                    catch
                    {
                        robotClient.m_ConnectionStatus = TConnectionStatus.cns_Failure;
                    }
                }
            }
        }

        public override void CloseScene()
        {
            SetNotifyEvent(CloseSocket, 1000);
        }

        public void PassWdFail()
        {
            m_ConnectionStep = TConnectionStep.cnsConnect;
        }

        public override void PlayScene()
        {

        }
        
        /// <summary>
        /// 账号注册
        /// </summary>
        private void NewAccount()
        {
            m_ConnectionStep = TConnectionStep.cnsNewAccount;
            SendNewAccount(robotClient.LoginID, robotClient.LoginPasswd);
        }

        /// <summary>
        /// 账号注册成功
        /// </summary>
        /// <param name="sData"></param>
        public void ClientNewIdSuccess()
        {
            SendLogin(robotClient.LoginID, robotClient.LoginPasswd);
        }

        private void SendNewAccount(string sAccount, string sPassword)
        {
            MainOutMessage("创建帐号");
            m_ConnectionStep = TConnectionStep.cnsNewAccount;
            UserFullEntry ue = new UserFullEntry();
            ue.UserEntry.sAccount = sAccount;
            ue.UserEntry.sPassword = sPassword;
            ue.UserEntry.sUserName = sAccount;
            ue.UserEntry.sSSNo = "650101-1455111";
            ue.UserEntry.sQuiz = sAccount;
            ue.UserEntry.sAnswer = sAccount;
            ue.UserEntry.sPhone = "";
            ue.UserEntry.sEMail = "";
            ue.UserEntryAdd.sQuiz2 = sAccount;
            ue.UserEntryAdd.sAnswer2 = sAccount;
            ue.UserEntryAdd.sBirthDay = "1978/01/01";
            ue.UserEntryAdd.sMobilePhone = "";
            ue.UserEntryAdd.sMemo = "";
            ue.UserEntryAdd.sMemo2 = "";
            var Msg = Grobal2.MakeDefaultMsg(Grobal2.CM_ADDNEWUSER, 0, 0, 0, 0);
            SendSocket(EDcode.EncodeMessage(Msg) + EDcode.EncodeBuffer(ue));
        }

        private void SendLogin(string uid, string passwd)
        {
            MainOutMessage("开始登陆");
            robotClient.LoginID = uid;
            robotClient.LoginPasswd = passwd;
            ClientPacket msg = Grobal2.MakeDefaultMsg(Grobal2.CM_IDPASSWORD, 0, 0, 0, 0);
            SendSocket(EDcode.EncodeMessage(msg) + EDcode.EncodeString(uid + "/" + passwd));
            MShare.g_boSendLogin = true;
        }
        
        public void ClientGetPasswordOK(ClientPacket msg, string sBody)
        {
            MShare.g_wAvailIDDay = HUtil32.LoWord(msg.Recog);
            MShare.g_wAvailIDHour = HUtil32.HiWord(msg.Recog);
            MShare.g_wAvailIPDay = msg.Param;
            MShare.g_wAvailIPHour = msg.Tag;
            if ((MShare.g_wAvailIDHour % 60) > 0)
            {
                MainOutMessage("个人帐户的期限: 剩余 " + (MShare.g_wAvailIDHour / 60).ToString() + " 小时 " + (MShare.g_wAvailIDHour % 60).ToString() + " 分钟.");
            }
            else if (MShare.g_wAvailIDHour > 0)
            {
                MainOutMessage("个人帐户的期限: 剩余 " + MShare.g_wAvailIDHour.ToString() + " 分钟.");
            }
            else
            {
                MainOutMessage("帐号登录成功！");
            }
            string sServerName = string.Empty;
            string sText = EDcode.DeCodeString(sBody);
            HUtil32.GetValidStr3(sText, ref sServerName, new[] { "/" });
            ClientGetSelectServer();
            SendSelectServer(sServerName);
        }
        
        public void ClientGetSelectServer()
        {
            
        }

        private void SendSelectServer(string svname)
        {
            MainOutMessage($"选择服务器：{svname}");
            m_ConnectionStep = TConnectionStep.cnsSelServer;
            var DefMsg = Grobal2.MakeDefaultMsg(Grobal2.CM_SELECTSERVER, 0, 0, 0, 0);
            SendSocket(EDcode.EncodeMessage(DefMsg) + EDcode.EncodeString(svname));
        }

        /// <summary>
        /// 登陆成功
        /// </summary>
        public void ClientGetPasswdSuccess(string body)
        {
            string runaddr = string.Empty;
            string runport = string.Empty;
            string certifystr = string.Empty;
            string Str = EDcode.DeCodeString(body);
            Str = HUtil32.GetValidStr3(Str, ref runaddr, new string[] { "/" });
            Str = HUtil32.GetValidStr3(Str, ref runport, new string[] { "/" });
            Str = HUtil32.GetValidStr3(Str, ref certifystr, new string[] { "/" });
            robotClient.Certification = HUtil32.Str_ToInt(certifystr, 0);
            MShare.g_sSelChrAddr = runaddr;
            MShare.g_nSelChrPort = HUtil32.Str_ToInt(runport, 0);
            m_ConnectionStep = TConnectionStep.cnsQueryChr;
        }

        private void SendSocket(string sendstr)
        {
            if (ClientSocket.IsConnected)
            {
                ClientSocket.SendText($"#1{sendstr}!");
            }
            else
            {
                MainOutMessage($"Socket Close: {ClientSocket.RemoteEndPoint}");
            }
        }

        private void CloseSocket()
        {
            ClientSocket.Disconnect();//断开登录网关链接
            MainOutMessage("主动断开");
        }

        #region Socket Events

        private void CSocketConnect(object sender, DSCClientConnectedEventArgs e)
        {
            MShare.g_boServerConnected = true;
            if (m_ConnectionStep == TConnectionStep.cnsConnect)
            {
                if (robotClient.m_boNewAccount)
                {
                    SetNotifyEvent(NewAccount, 3000);
                }
                else
                {
                    ClientNewIdSuccess();
                }
            }
            if (MShare.g_ConnectionStep == TConnectionStep.cnsLogin)
            {
                robotClient.DScreen.ChangeScene(SceneType.stLogin);
            }
        }

        private void CSocketDisconnect(object sender, DSCClientConnectedEventArgs e)
        {
            MShare.g_boServerConnected = false;
            if (MShare.g_SoftClosed)
            {
                MShare.g_SoftClosed = false;
            }
            else if ((robotClient.DScreen.CurrentScene == robotClient.LoginScene) && !MShare.g_boSendLogin)
            {
                MainOutMessage("游戏连接已关闭...");
            }
        }

        private void CSocketError(object sender, DSCClientErrorEventArgs e)
        {
            switch (e.ErrorCode)
            {
                case System.Net.Sockets.SocketError.ConnectionRefused:
                    Console.WriteLine($"游戏服务器[{ClientSocket.RemoteEndPoint}]拒绝链接...");
                    break;
                case System.Net.Sockets.SocketError.ConnectionReset:
                    Console.WriteLine($"游戏服务器[{ClientSocket.RemoteEndPoint}]关闭连接...");
                    break;
                case System.Net.Sockets.SocketError.TimedOut:
                    Console.WriteLine($"游戏服务器[{ClientSocket.RemoteEndPoint}]链接超时...");
                    break;
            }
        }

        private void CSocketRead(object sender, DSCClientDataInEventArgs e)
        {
            var sData = HUtil32.GetString(e.Buff, 0, e.BuffLen);
            if (!string.IsNullOrEmpty(sData))
            {
                ClientManager.AddPacket(robotClient.SessionId, sData);
            }
        }

        #endregion
    }
}