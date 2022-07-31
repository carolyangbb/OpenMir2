using System.Collections;
using SystemModule;

namespace GameSvr
{
    public class TUserfunc : ICommand
    {
        public TUserInfo FInfo = null;
        public TTagMgr FTag = null;
        public TFriendMgr FFriend = null;

        public TUserfunc() : base()
        {
            FInfo = new TUserInfo();
            FTag = null;
            FFriend = null;
        }

        public override void OnCmdChange(ref TCmdMsg Msg)
        {
        }


        public bool IsRealUser()
        {
            bool result = false;
            if (FInfo != null)
            {
                if (FInfo.UserHandle > 0)
                {
                    result = true;
                }
            }
            return result;
        }

        public bool OpenTag()
        {
            bool result = false;
            if (FInfo != null)
            {
                if (FTag != null)
                {
                    CloseTag();
                }
                FTag = new TTagMgr();
                FTag.OnCmdDBRejectList(FInfo);
                result = true;
            }
            return result;
        }

        public void CloseTag()
        {
            if (FTag != null)
            {
                FTag.Free();
                FTag = null;
            }
        }

        public bool OpenFriend()
        {
            bool result;
            result = false;
            if (FInfo != null)
            {
                if (FFriend != null)
                {
                    CloseFriend();
                }
                FFriend = new TFriendMgr();
                FFriend.OnCmdDBList(FInfo);
                result = true;
            }
            return result;
        }

        public void CloseFriend()
        {
            if (FFriend != null)
            {
                FFriend.Free();
                FFriend = null;
            }
        }
    }

    public class TUserMgr : TCmdMgr
    {
        private readonly ArrayList FItems = null;
        private readonly object FHumanCS = null;

        public TUserMgr() : base()
        {
            FItems = new ArrayList();
            FHumanCS = new object();
        }

        ~TUserMgr()
        {
            RemoveAll();
            FItems.Free();
            FHumanCS.Free();
        }

        public bool Add(string UserName_, int Recog_, int ConnState_, int GateIdx_, int UserGateIdx_, int UserHandle_)
        {
            TUserfunc Info = null;
            bool ReUse;
            var result = false;
            if (GetUserFunc(UserName_, ref Info))
            {
                this.ErrMsg("Exist User !:" + UserName_);
                ReUse = true;
            }
            else
            {
                Info = new TUserfunc();
                ReUse = false;
            }
            if (Info != null)
            {
                Info.FInfo.UserName = UserName_;
                Info.FInfo.Recog = Recog_;
                Info.FInfo.ConnState = ConnState_;
                Info.FInfo.GateIdx = GateIdx_;
                Info.FInfo.UserGateIdx = UserGateIdx_;
                Info.FInfo.UserHandle = UserHandle_;
                if (!ReUse)
                {
                    FItems.Add(Info);
                }
                OpenUser(UserName_);
                if (Info.IsRealUser())
                {
                    OnCmdDBOwnList(Info.FInfo);
                }
                result = true;
            }
            return result;
        }

        public TUserfunc Find(string UserName_)
        {
            TUserfunc Item;
            TUserfunc result = null;
            for (var i = 0; i < FItems.Count; i++)
            {
                Item = FItems[i] as TUserfunc;
                if (Item.FInfo.UserName == UserName_)
                {
                    result = Item;
                    return result;
                }
            }
            return result;
        }

        public bool Delete(string UserName_)
        {
            var result = false;
            var Item = Find(UserName_);
            if (Item != null)
            {
                if (Item.IsRealUser())
                {
                    OnCmdDBOwnList(Item.FInfo);
                }
                var i = FItems.IndexOf(Item);
                if (i >= 0)
                {
                    FItems.RemoveAt(i);
                    Item.Free();
                    result = true;
                }
            }
            return result;
        }

        public void OpenUser(string UserName_)
        {
            var Item = Find(UserName_);
            if (Item != null)
            {
                if (Item.FInfo != null)
                {
                    Item.FInfo.OnUserOpen();
                }
                if (Item.FTag != null)
                {
                    Item.FTag.OnUserOpen();
                }
                if (Item.FFriend != null)
                {
                    Item.FFriend.OnUserOpen();
                }
            }
        }

        public void CloserUser(string UserName_)
        {
            var Item = Find(UserName_);
            if (Item != null)
            {
                if (Item.FInfo != null)
                {
                    Item.FInfo.OnUserClose();
                }
                if (Item.FTag != null)
                {
                    Item.FTag.OnUserClose();
                }
                if (Item.FFriend != null)
                {
                    Item.FFriend.OnUserClose();
                }
            }
        }

        private void RemoveAll()
        {
            TUserfunc Item;
            for (var i = 0; i < FItems.Count; i++)
            {
                Item = (TUserfunc)FItems[i];
                Item.Free();
            }
            FItems.Clear();
        }

        public bool GetUserFunc(string UserName_, ref TUserfunc UserFunc_)
        {
            bool result;
            var Item = Find(UserName_);
            if (Item != null)
            {
                UserFunc_ = Item;
                result = true;
            }
            else
            {
                UserFunc_ = null;
                result = false;
            }
            return result;
        }

        public bool GetUserInfo(string UserName_, ref TUserInfo UserInfo_)
        {
            UserInfo_ = null;
            var result = false;
            var Item = Find(UserName_);
            if (Item != null)
            {
                if (Item.FInfo != null)
                {
                    UserInfo_ = Item.FInfo;
                    result = true;
                }
            }
            return result;
        }

        public bool SetConnState(string UserName_, int ConnState_)
        {
            TUserInfo Item = null;
            var result = false;
            if (GetUserInfo(UserName_, ref Item))
            {
                Item.ConnState = ConnState_;
                result = true;
            }
            return result;
        }

        public override void OnCmdChange(ref TCmdMsg Msg)
        {
            TUserfunc Func = null;
            switch (Msg.CmdNum)
            {
                case Grobal2.DBR_FRIEND_WONLIST:
                    OnCmdDBROwnList(Msg);
                    return;
                case Grobal2.DBR_LM_LIST:
                    OnCmdDBRLMList(Msg);
                    return;
                case Grobal2.ISM_FUNC_USEROPEN:
                    if (Add(Msg.UserName, Msg.Msg.Recog, Msg.Msg.Param, Msg.GateIdx, Msg.UserGateIdx, Msg.Userhandle))
                    {
                        if (Msg.Userhandle != 0)
                        {
                            OpenSubSystem(Msg.UserName, TSystemType.stFriend);
                            OpenSubSystem(Msg.UserName, TSystemType.stTag);
                        }
                    }
                    return;
                case Grobal2.ISM_FUNC_USERCLOSE:
                    CloserUser(Msg.UserName);
                    Delete(Msg.UserName);
                    return;
            }
            if (GetUserFunc(Msg.UserName, ref Func))
            {
                Msg.pInfo = Func.FInfo;
                if (Func.FInfo != null)
                {
                    switch (Msg.CmdNum)
                    {
                        case Grobal2.ISM_FRIEND_OPEN:
                        case Grobal2.ISM_FRIEND_CLOSE:
                        case Grobal2.ISM_USER_INFO:
                            Func.FInfo.OnCmdChange(ref Msg);
                            return;
                            break;
                    }
                }
                else
                {
                    return;
                }
                if (Func.FFriend != null)
                {
                    switch (Msg.CmdNum)
                    {
                        case Grobal2.CM_FRIEND_ADD:
                        case Grobal2.CM_FRIEND_DELETE:
                        case Grobal2.CM_FRIEND_EDIT:
                        case Grobal2.CM_FRIEND_LIST:
                        case Grobal2.ISM_FRIEND_INFO:
                        case Grobal2.ISM_FRIEND_DELETE:
                        case Grobal2.ISM_FRIEND_RESULT:
                        case Grobal2.DBR_FRIEND_LIST:
                        case Grobal2.DBR_FRIEND_RESULT:
                            Func.FFriend.OnCmdChange(ref Msg);
                            return;
                            break;
                    }
                }
                if (Func.FTag != null)
                {
                    switch (Msg.CmdNum)
                    {
                        case Grobal2.CM_TAG_ADD:
                        case Grobal2.CM_TAG_ADD_DOUBLE:
                        case Grobal2.CM_TAG_DELETE:
                        case Grobal2.CM_TAG_SETINFO:
                        case Grobal2.CM_TAG_LIST:
                        case Grobal2.CM_TAG_REJECT_LIST:
                        case Grobal2.CM_TAG_REJECT_ADD:
                        case Grobal2.CM_TAG_REJECT_DELETE:
                        case Grobal2.ISM_TAG_SEND:
                        case Grobal2.ISM_TAG_RESULT:
                        case Grobal2.DBR_TAG_LIST:
                        case Grobal2.DBR_TAG_REJECT_LIST:
                        case Grobal2.DBR_TAG_NOTREADCOUNT:
                        case Grobal2.DBR_TAG_RESULT:
                            Func.FTag.OnCmdChange(ref Msg);
                            break;
                    }
                }
            }
        }

        public void OpenSubSystem(string UserName_, TSystemType SystemType)
        {
            TUserfunc userfunc = null;
            if (GetUserFunc(UserName_, ref userfunc))
            {
                switch (SystemType)
                {
                    case TSystemType.stTag:
                        userfunc.OpenTag();
                        break;
                    case TSystemType.stFriend:
                        userfunc.OpenFriend();
                        break;
                }
            }
        }

        public void CloseSubSystem(string Username_, TSystemType SystemType)
        {
            TUserfunc userfunc = null;
            if (GetUserFunc(Username_, ref userfunc))
            {
                switch (SystemType)
                {
                    case TSystemType.stTag:
                        userfunc.CloseTag();
                        break;
                    case TSystemType.stFriend:
                        userfunc.CloseFriend();
                        break;
                }
            }
        }

        public void OnCmdDBOwnList(TUserInfo UserInfo)
        {

        }

        public void OnCmdDBROwnList(TCmdMsg Cmd)
        {
            string Friend = string.Empty;
            string TempStr = string.Empty;
            string MapInfo = string.Empty;
            TUserInfo userinfo = null;
            string BodyStr = HUtil32.GetValidStr3(Cmd.body, ref TempStr, new string[] { "/" });
            var ListCount = HUtil32.Str_ToInt(TempStr, 0);
            var ConnState = 0;
            MapInfo = "";
            if (GetUserInfo(Cmd.UserName, ref userinfo))
            {
                ConnState = userinfo.ConnState;
                MapInfo = userinfo.MapInfo;
            }
            for (var i = 0; i < ListCount; i++)
            {
                BodyStr = HUtil32.GetValidStr3(BodyStr, ref Friend, new string[] { "/" });
                if (Friend != "")
                {
                    if (GetUserInfo(Friend, ref userinfo))
                    {
                        OnSendInfoToOthers(Cmd.UserName, ConnState, MapInfo, Friend);
                    }
                }
            }
        }

        public void OnCmdDBRLMList(TCmdMsg Cmd)
        {
            FHumanCS.Enter();
            try
            {
                M2Share.UserEngine.ExternSendMessage(Cmd.UserName, Grobal2.RM_LM_DBGETLIST, 0, 0, 0, 0, Cmd.body);
            }
            finally
            {
                FHumanCS.Leave();
            }
        }

        public void OnSendInfoToOthers(string UserName, int ConnState, string MapInfo, string LinkedFriend)
        {
            string str = UserName + "/" + ConnState.ToString() + "/" + MapInfo + "/";
            M2Share.UserMgrEngine.InterSendMsg(TSendTarget.stOtherServer, M2Share.ServerIndex, 0, 0, 0, LinkedFriend, 0, Grobal2.ISM_USER_INFO, (short)M2Share.ServerIndex, 0, 0, str);
        }
    }


    public enum TSystemType
    {
        stTag,
        stFriend
    }
}