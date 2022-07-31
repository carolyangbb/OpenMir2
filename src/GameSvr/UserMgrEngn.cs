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
                    M2Share.MainOutMessage("[UserMgrEngine] raise exception..");
                }
                //this.Sleep(1);
                //if (this.Terminated)
                //{
                //    return;
                //}
            }
        }

        public void AddUser(string UserName_, int Recog_, int ConnState_, int GateIdx_, int UserGateIdx_, int UserHandle_)
        {
            M2Share.umLock.Enter();
            try
            {
                InterSendMsg(TSendTarget.stInterServer, 0, GateIdx_, UserGateIdx_, UserHandle_, UserName_, Recog_, Grobal2.ISM_FUNC_USEROPEN, (short)ConnState_, 0, 0, "");
            }
            finally
            {
                M2Share.umLock.Leave();
            }
        }

        public void DeleteUser(string UserName_)
        {
            M2Share.umLock.Enter();
            try
            {
                InterSendMsg(TSendTarget.stInterServer, 0, 0, 0, 0, UserName_, 0, Grobal2.ISM_FUNC_USERCLOSE, 0, 0, 0, "");
            }
            finally
            {
                M2Share.umLock.Leave();
            }
        }

        // ------------------------------------------------------------------------------
        // Internal SendMsg... Don't Use Lock...
        // ------------------------------------------------------------------------------
        public void InterSendMsg(TSendTarget SendTarget, int TargetSvrIdx, int GateIdx, int UserGateIdx, int UserHandle, string UserName, int Recog, short Ident, short Param, short Tag, short Series, string Body)
        {
            TUserInfo userinfo = null;
            if (SendTarget == TSendTarget.stClient)
            {
                if (!InterGetUserInfo(UserName, ref userinfo))
                {
                    M2Share.umLock.Enter();
                    try
                    {
                        FUserMgr.ErrMsg("[USERMGR_ENGINE]Not Exist Object " + UserName);
                    }
                    finally
                    {
                        M2Share.umLock.Leave();
                    }
                    return;
                }
            }
            M2Share.umLock.Enter();
            try
            {
                FUserMgr.SendMsgQueue1(SendTarget, TargetSvrIdx, GateIdx, UserGateIdx, UserHandle, UserName, Recog, Ident, Param, Tag, Series, Body);
            }
            finally
            {
                M2Share.umLock.Leave();
            }
        }

        public bool InterGetUserInfo(string UserName_, ref TUserInfo UserInfo_)
        {
            return FUserMgr.GetUserInfo(UserName_, ref UserInfo_);
        }

        public void ExternSendMsg(TSendTarget SendTarget, int TargetSvrIdx, int GateIdx, int UserGateIdx, int UserHandle, string UserName, TDefaultMessage msg, string body)
        {
            M2Share.umLock.Enter();
            try
            {
                FUserMgr.SendMsgQueue(SendTarget, TargetSvrIdx, GateIdx, UserGateIdx, UserHandle, UserName, msg, body);
            }
            finally
            {
                M2Share.umLock.Leave();
            }
        }

        public void OnDBRead(string data)
        {
            FUserMgr.OnDBRead(data);
        }

        public void OnExternInterMsg(int snum, int Ident, string UserName, string Data)
        {
            M2Share.umLock.Enter();
            try
            {
                InterSendMsg(TSendTarget.stInterServer, snum, 0, 0, 0, UserName, 0, (short)Ident, 0, 0, 0, Data);
            }
            finally
            {
                M2Share.umLock.Leave();
            }
        }
    }
}

