using SystemModule;

namespace GameSvr
{
    public class TUserInfo : ICommand
    {
        public string UserName
        {
            get
            {
                return FUserName;
            }
            set
            {
                FUserName = value;
            }
        }
        public int ConnState
        {
            get
            {
                return FConnState;
            }
            set
            {
                FConnState = value;
            }
        }
        public int GateIdx
        {
            get
            {
                return FGateIdx;
            }
            set
            {
                FGateIdx = value;
            }
        }
        public int UserGateIdx
        {
            get
            {
                return FUserGateIdx;
            }
            set
            {
                FUserGateIdx = value;
            }
        }
        public int UserHandle
        {
            get
            {
                return FUserHandle;
            }
            set
            {
                FUserHandle = value;
            }
        }
        public int Recog
        {
            get
            {
                return FRecog;
            }
            set
            {
                FRecog = value;
            }
        }
        public string MapInfo
        {
            get
            {
                return FMapInfo;
            }
            set
            {
                FMapInfo = value;
            }
        }
        private string FUserName = string.Empty;
        private int FConnState = 0;
        private int FGateIdx = 0;
        private int FUserGateIdx = 0;
        private int FUserHandle = 0;
        private int FRecog = 0;
        private string FMapInfo = string.Empty;

        public TUserInfo() : base()
        {
            FUserName = "";
            FConnState = Grobal2.CONNSTATE_UNKNOWN;
            FGateIdx = 0;
            FUserHandle = 0;
            FRecog = 0;
            FMapInfo = "";
        }

        public void OnUserOpen()
        {

        }

        public void OnUserClose()
        {

        }

        public override void OnCmdChange(ref TCmdMsg Cmd)
        {
            switch (Cmd.CmdNum)
            {
                case Grobal2.ISM_FRIEND_OPEN:
                    OnCmdISMFriendOpen(Cmd);
                    break;
                case Grobal2.ISM_FRIEND_CLOSE:
                    OnCmdISMFriendClose(Cmd);
                    break;
                case Grobal2.ISM_USER_INFO:
                    OnCmdISMUserInfo(Cmd);
                    break;
            }
        }

        public void OnCmdISMFriendOpen(TCmdMsg Cmd)
        {

        }

        public void OnCmdISMFriendClose(TCmdMsg Cmd)
        {

        }


        public void OnCmdISMUserInfo(TCmdMsg Cmd)
        {
            string UserName = string.Empty;
            string ConnState = string.Empty;
            string MapInfo = string.Empty;
            string Str;
            Str = HUtil32.GetValidStr3(Cmd.body, ref UserName, new string[] { "/" });
            Str = HUtil32.GetValidStr3(Str, ref ConnState, new string[] { "/" });
            Str = HUtil32.GetValidStr3(Str, ref MapInfo, new string[] { "/" });
            TUserInfo UserInfo = Cmd.pInfo;
            Str = UserName + "/" + MapInfo;
            M2Share.UserMgrEngine.InterSendMsg(TSendTarget.stClient, 0, UserInfo.GateIdx, UserInfo.UserGateIdx, UserInfo.UserHandle, UserInfo.UserName, UserInfo.Recog, Grobal2.SM_USER_INFO, 0, 0, 0, Str);
        }
    }
}