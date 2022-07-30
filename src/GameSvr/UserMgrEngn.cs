using SystemModule;

namespace GameSvr
{
    public class TUserMgrEngine
    {
        private readonly TUserMgr FUserMgr = null;

        public TUserMgrEngine()
        {
            FUserMgr = new TUserMgr();
        }

        public void Execute()
        {
            while (true)
            {
                try
                {
                    FUserMgr.RunMsg();
                }
                catch
                {
                    svMain.MainOutMessage("[UserMgrEngine] raise exception..");
                }
                this.Sleep(1);
                if (this.Terminated)
                {
                    return;
                }
            }
        }

        public void AddUser(string UserName_, int Recog_, int ConnState_, int GateIdx_, int UserGateIdx_, int UserHandle_)
        {
            svMain.umLock.Enter();
            try
            {
                InterSendMsg(TSendTarget.stInterServer, 0, GateIdx_, UserGateIdx_, UserHandle_, UserName_, Recog_, Grobal2.ISM_FUNC_USEROPEN, (ushort)ConnState_, 0, 0, "");
            }
            finally
            {
                svMain.umLock.Leave();
            }
        }

        public void DeleteUser(string UserName_)
        {
            svMain.umLock.Enter();
            try
            {
                InterSendMsg(TSendTarget.stInterServer, 0, 0, 0, 0, UserName_, 0, Grobal2.ISM_FUNC_USERCLOSE, 0, 0, 0, "");
            }
            finally
            {
                svMain.umLock.Leave();
            }
        }

        // ------------------------------------------------------------------------------
        // Internal SendMsg... Don't Use Lock...
        // ------------------------------------------------------------------------------
        public void InterSendMsg(TSendTarget SendTarget, int TargetSvrIdx, int GateIdx, int UserGateIdx, int UserHandle, string UserName, int Recog, ushort Ident, ushort Param, ushort Tag, ushort Series, string Body)
        {
            TUserInfo userinfo;
            if (SendTarget == TSendTarget.stClient)
            {
                if (!InterGetUserInfo(UserName, ref userinfo))
                {
                    svMain.umLock.Enter();
                    try
                    {
                        FUserMgr.ErrMsg("[USERMGR_ENGINE]Not Exist Object " + UserName);
                    }
                    finally
                    {
                        svMain.umLock.Leave();
                    }
                    return;
                }
            }
            svMain.umLock.Enter();
            try
            {
                FUserMgr.SendMsgQueue1(SendTarget, TargetSvrIdx, GateIdx, UserGateIdx, UserHandle, UserName, Recog, Ident, Param, Tag, Series, Body);
            }
            finally
            {
                svMain.umLock.Leave();
            }
        }

        public bool InterGetUserInfo(string UserName_, ref TUserInfo UserInfo_)
        {
            bool result;
            result = FUserMgr.GetUserInfo(UserName_, ref UserInfo_);
            return result;
        }

        public void ExternSendMsg(TSendTarget SendTarget, int TargetSvrIdx, int GateIdx, int UserGateIdx, int UserHandle, string UserName, TDefaultMessage msg, string body)
        {
            svMain.umLock.Enter();
            try
            {
                FUserMgr.SendMsgQueue(SendTarget, TargetSvrIdx, GateIdx, UserGateIdx, UserHandle, UserName, msg, body);
            }
            finally
            {
                svMain.umLock.Leave();
            }
        }

        public void OnDBRead(string data)
        {
            FUserMgr.OnDBRead(data);
        }

        public void OnExternInterMsg(int snum, int Ident, string UserName, string Data)
        {
            svMain.umLock.Enter();
            try
            {
                InterSendMsg(TSendTarget.stInterServer, snum, 0, 0, 0, UserName, 0, (ushort)Ident, 0, 0, 0, Data);
            }
            finally
            {
                svMain.umLock.Leave();
            }
        }
    }
}

