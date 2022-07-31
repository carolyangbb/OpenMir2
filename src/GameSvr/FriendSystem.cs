using System.Collections.Generic;
using SystemModule;

namespace GameSvr
{
    public class TFriendInfo : ICommand
    {

        public string Name
        {
            get
            {
                return FName;
            }
            set
            {
                FName = value;
            }
        }
        public string Ownner
        {
            get
            {
                return FOwnner;
            }
            set
            {
                FOwnner = value;
            }
        }
        public int RegState
        {
            get
            {
                return FRegState;
            }
            set
            {
                FRegState = value;
            }
        }
        public string Desc
        {
            get
            {
                return FDesc;
            }
            set
            {
                FDesc = value;
            }
        }
        private string FOwnner = string.Empty;
        public string FName = string.Empty;
        private int FRegState = 0;
        public string FDesc = string.Empty;

        public TFriendInfo() : base()
        {

        }
    }

    public class TFriendMgr : ICommand
    {
        private readonly IList<TFriendInfo> FItems = null;
        private bool FIsListSendAble = false;
        private bool FWantListFlag = false;

        public TFriendMgr() : base()
        {
            FItems = new List<TFriendInfo>();
            FIsListSendAble = false;
            FWantListFlag = false;
        }

        ~TFriendMgr()
        {
            RemoveAll();
            //FItems.Free();
        }

        public void OnUserOpen()
        {
        }

        public void OnUserClose()
        {
        }

        public TFriendInfo Find(string UserName_)
        {
            TFriendInfo result = null;
            for (var i = 0; i < FItems.Count; i++)
            {
                var Item = FItems[i];
                if (Item.FName == UserName_)
                {
                    result = Item;
                    return result;
                }
            }
            return result;
        }

        public bool Add(TUserInfo UserInfo, string Friend, int RegState, string Desc)
        {
            TFriendInfo Info;
            var result = false;
            if ((Friend != "") && (!IsFriend(Friend)))
            {
                Info = new TFriendInfo();
                if (Info != null)
                {
                    Info.Name = Friend;
                    Info.Ownner = UserInfo.UserName;
                    Info.RegState = RegState;
                    Info.Desc = Desc;
                    FItems.Add(Info);
                    result = true;
                }
                else
                {
                    this.ErrMsg("Nil Pointer When Create -[TFriendMgr.Add]");
                }
            }
            else
            {
                this.ErrMsg("Empty \"Friend\" -[TFriendMgr.Add]");
            }
            return result;
        }

        public bool Delete(TUserInfo UserInfo, string Friend)
        {
            var result = false;
            var Item = Find(Friend);
            if (Item != null)
            {
                var i = FItems.IndexOf(Item);
                if (i >= 0)
                {
                    FItems.RemoveAt(i);
                    //Item.Free();
                    result = true;
                }
            }
            return result;
        }

        public bool SetDesc(TUserInfo UserInfo, string Friend, string Desc)
        {
            var result = false;
            var Item = Find(Friend);
            if (Item != null)
            {
                Item.FDesc = Desc;
                result = true;
            }
            return result;
        }

        public bool IsFriend(string Name)
        {
            var result = false;
            var Item = Find(Name);
            if (Item != null)
            {
                result = true;
            }
            return result;
        }

        private void RemoveAll()
        {
            for (var i = 0; i < FItems.Count; i++)
            {
                var Item = FItems[i];
                if (Item != null)
                {
                    //Item.Free();
                }
            }
            FItems.Clear();
        }

        public void OnMsgInfoToClient(TUserInfo UserInfo, string FriendName, int ConnState, int RegState, string Desc)
        {
            string str;
            str = FriendName + "/" + Desc;
            M2Share.UserMgrEngine.InterSendMsg(TSendTarget.stClient, 0, UserInfo.GateIdx, UserInfo.UserGateIdx, UserInfo.UserHandle, UserInfo.UserName, UserInfo.Recog, Grobal2.SM_FRIEND_INFO, (short)RegState, (short)ConnState, 0, str);
        }

        public void OnMsgInfoToServer(TUserInfo UserInfo, string FriendName, int RegState, string Desc)
        {
            string str;
            str = RegState.ToString() + ":" + FriendName + ":" + Desc;
            M2Share.UserMgrEngine.InterSendMsg(TSendTarget.stClient, 0, 0, 0, 0, UserInfo.UserName, UserInfo.Recog, Grobal2.ISM_FRIEND_INFO, 0, 0, 0, str);
        }

        public void OnSendInfoToClient(TUserInfo UserInfo, string Friend)
        {
            var Item = Find(Friend);
            if (Item != null)
            {
                OnMsgInfoToClient(UserInfo, Item.Name, Grobal2.CONNSTATE_DISCONNECT, Item.RegState, Item.Desc);
            }
        }

        public void OnSendListToClient(TUserInfo UserInfo)
        {
            for (var i = 0; i < FItems.Count; i++)
            {
                var Item = FItems[i];
                OnMsgInfoToClient(UserInfo, Item.Name, Grobal2.CONNSTATE_DISCONNECT, Item.RegState, Item.Desc);
            }
        }

        public override void OnCmdChange(ref TCmdMsg Msg)
        {
            switch (Msg.CmdNum)
            {
                case Grobal2.CM_FRIEND_ADD:
                    OnCmdCMAdd(Msg);
                    break;
                case Grobal2.CM_FRIEND_DELETE:
                    OnCmdCMDelete(Msg);
                    break;
                case Grobal2.CM_FRIEND_EDIT:
                    OnCmdCMEdit(Msg);
                    break;
                case Grobal2.CM_FRIEND_LIST:
                    OnCmdCMList(Msg);
                    break;
                case Grobal2.ISM_FRIEND_INFO:
                    OnCmdISMInfo(Msg);
                    break;
                case Grobal2.ISM_FRIEND_DELETE:
                    OnCmdISMDelete(Msg);
                    break;
                case Grobal2.ISM_FRIEND_RESULT:
                    OnCmdISMResult(Msg);
                    break;
                case Grobal2.DBR_FRIEND_LIST:
                    OnCmdDBRList(Msg);
                    break;
                case Grobal2.DBR_FRIEND_RESULT:
                    // DBR_FRIEND_WONLIST  : OnCmdDBROwnList( Msg );
                    OnCmdDBRResult(Msg);
                    break;
            }
        }

        public void OnCmdCMList(TCmdMsg Cmd)
        {
            FWantListFlag = true;
            if (FIsListSendAble)
            {
                OnSendListToClient(Cmd.pInfo);
            }
        }

        public void OnCmdCMAdd(TCmdMsg Cmd)
        {
            TUserInfo userinfo = null;
            var friend = Cmd.body;
            var regstate = Cmd.Msg.Param;
            var forceflag = Cmd.Msg.Tag;
#if DEBUG
            this.ErrMsg("Cmd_CM_Add" + owner + "/" + friend + "/" + (regstate).ToString());
#endif
            if (M2Share.UserMgrEngine.InterGetUserInfo(friend, ref userinfo))
            {
                if (Add(Cmd.pInfo, friend, regstate, ""))
                {
                    OnCmdDBAdd(Cmd.pInfo, friend, regstate, "");
                    OnMsgInfoToClient(Cmd.pInfo, friend, userinfo.ConnState, regstate, "");
                }
            }
            else
            {
                if (forceflag == 1)
                {
                    if (Add(Cmd.pInfo, friend, regstate, ""))
                    {
                        OnMsgInfoToClient(Cmd.pInfo, friend, Grobal2.CONNSTATE_DISCONNECT, regstate, "");
                    }
                }
            }
        }

        public void OnCmdCMDelete(TCmdMsg Cmd)
        {
            var Owner = Cmd.UserName;
            var Friend = Cmd.body;
#if DEBUG
            this.ErrMsg("Cmd_CM_Delete" + Owner + "/" + Friend);
#endif
            if (true == Delete(Cmd.pInfo, Friend))
            {
                OnCmdSMDelete(Cmd.pInfo, Friend);
                OnCmdDBDelete(Cmd.pInfo, Friend);
            }
            else
            {
                OnCmdSMResult(Cmd.pInfo, Cmd.CmdNum, Grobal2.CR_DONTDELETE);
            }
        }

        public void OnCmdCMEdit(TCmdMsg Cmd)
        {
            var Friend = string.Empty;
            var Desc = string.Empty;
            var Owner = Cmd.UserName;
            Desc = HUtil32.GetValidStr3(Cmd.body, ref Friend, new string[] { "/" });
#if DEBUG
            this.ErrMsg("Cmd_CM_SerDesc" + Friend + "/" + Desc);
#endif
            if (true == SetDesc(Cmd.pInfo, Friend, Desc))
            {
                OnSendInfoToClient(Cmd.pInfo, Friend);
                OnCmdDBEdit(Cmd.pInfo, Friend, Desc);
            }
        }

        public void OnCmdSMInfo(TUserInfo UserInfo, string FriendName, short RegState, short Conn, string Desc)
        {
            string str;
            str = Desc;
            M2Share.UserMgrEngine.InterSendMsg(TSendTarget.stClient, 0, UserInfo.GateIdx, UserInfo.UserGateIdx, UserInfo.UserHandle, UserInfo.UserName, UserInfo.Recog, Grobal2.SM_FRIEND_INFO, RegState, Conn, 0, str);
        }

        public void OnCmdSMDelete(TUserInfo UserInfo, string FriendName)
        {
            string str;
            str = FriendName;
            M2Share.UserMgrEngine.InterSendMsg(TSendTarget.stClient, 0, UserInfo.GateIdx, UserInfo.UserGateIdx, UserInfo.UserHandle, UserInfo.UserName, UserInfo.Recog, Grobal2.SM_FRIEND_DELETE, 0, 0, 0, str);
        }

        public void OnCmdSMResult(TUserInfo UserInfo, short CmdNum, short Value)
        {
            M2Share.UserMgrEngine.InterSendMsg(TSendTarget.stClient, 0, UserInfo.GateIdx, UserInfo.UserGateIdx, UserInfo.UserHandle, UserInfo.UserName, UserInfo.Recog, Grobal2.SM_FRIEND_RESULT, CmdNum, Value, 0, "");
        }

        public void OnCmdISMInfo(TCmdMsg Cmd)
        {
            var Friend = string.Empty;
            var RegState = string.Empty;
            var TempStr = HUtil32.GetValidStr3(Cmd.body, ref RegState, new string[] { ":" });
            var Desc = HUtil32.GetValidStr3(TempStr, ref Friend, new string[] { ":" });
            if (!Add(Cmd.pInfo, Friend, HUtil32.Str_ToInt(RegState, 0), Desc))
            {
                this.ErrMsg("OnCmdISMInfo Dont Add Friend :" + Cmd.body);
            }
        }

        public void OnCmdISMDelete(TCmdMsg Cmd)
        {
            string Friend;
            Friend = Cmd.body;
            if (!Delete(Cmd.pInfo, Friend))
            {
                this.ErrMsg("OnCmdISMInfo Dont Delete Friend :" + Cmd.body);
            }
        }

        public void OnCmdISMResult(TCmdMsg Cmd)
        {
            this.ErrMsg("OnCmdISMRsult :" + Cmd.body);
        }

        public void OnCmdOSMInfo(string UserName, short SvrIndex, string FriendName, int RegState, int Conn, string Desc)
        {
            string str = RegState.ToString() + ":" + Conn.ToString() + ":" + FriendName;
            M2Share.UserMgrEngine.InterSendMsg(TSendTarget.stOtherServer, 0, 0, 0, 0, UserName, 0, Grobal2.ISM_FRIEND_INFO, SvrIndex, 0, 0, str);
        }

        public void OnCmdOSMDelete(string UserName, short SvrIndex, string FriendName)
        {
            string str = FriendName;
            M2Share.UserMgrEngine.InterSendMsg(TSendTarget.stOtherServer, 0, 0, 0, 0, UserName, 0, Grobal2.ISM_FRIEND_DELETE, SvrIndex, 0, 0, str);
        }

        public void OnCmdOSMResult(string UserName, short SvrIndex, short CmdNum, short ResultValue)
        {
            string str;
            str = CmdNum.ToString() + ":" + ResultValue.ToString();
            M2Share.UserMgrEngine.InterSendMsg(TSendTarget.stOtherServer, 0, 0, 0, 0, UserName, 0, Grobal2.ISM_FRIEND_RESULT, SvrIndex, 0, 0, str);
        }

        public void OnCmdDBList(TUserInfo UserInfo)
        {
            M2Share.UserMgrEngine.InterSendMsg(TSendTarget.stDbServer, M2Share.ServerIndex, 0, 0, 0, UserInfo.UserName, 0, Grobal2.DB_FRIEND_LIST, 0, 0, 0, "");
        }

        public void OnCmdDBAdd(TUserInfo UserInfo, string Friend, short RegState, string Desc)
        {
            string str = RegState.ToString() + ":" + Friend + ":" + Desc + "/";
            M2Share.UserMgrEngine.InterSendMsg(TSendTarget.stDbServer, M2Share.ServerIndex, 0, 0, 0, UserInfo.UserName, 0, Grobal2.DB_FRIEND_ADD, RegState, 0, 0, str);
        }

        public void OnCmdDBDelete(TUserInfo UserInfo, string Friend)
        {
            string str = Friend + "/";
            M2Share.UserMgrEngine.InterSendMsg(TSendTarget.stDbServer, M2Share.ServerIndex, 0, 0, 0, UserInfo.UserName, 0, Grobal2.DB_FRIEND_DELETE, 0, 0, 0, str);
        }

        public void OnCmdDBEdit(TUserInfo UserInfo, string Friend, string Desc)
        {
            string str = Friend + ":" + Desc + "/";
            M2Share.UserMgrEngine.InterSendMsg(TSendTarget.stDbServer, M2Share.ServerIndex, 0, 0, 0, UserInfo.UserName, 0, Grobal2.DB_FRIEND_EDIT, 0, 0, 0, str);
        }

        public void OnCmdDBRList(TCmdMsg Cmd)
        {
            var Friend = string.Empty;
            var RegState = string.Empty;
            var Desc = string.Empty;
            var TempStr = string.Empty;
            var BodyStr = HUtil32.GetValidStr3(Cmd.body, ref TempStr, new string[] { "/" });
            var ListCount = HUtil32.Str_ToInt(TempStr, 0);
            for (var i = 0; i < ListCount; i++)
            {
                BodyStr = HUtil32.GetValidStr3(BodyStr, ref TempStr, new string[] { "/" });
                if (TempStr != "")
                {
                    TempStr = HUtil32.GetValidStr3(TempStr, ref RegState, new string[] { ":" });
                    Desc = HUtil32.GetValidStr3(TempStr, ref Friend, new string[] { ":" });
                    Add(Cmd.pInfo, Friend, HUtil32.Str_ToInt(RegState, 0), Desc);
                }
            }
            FIsListSendAble = true;
            if (FWantListFlag)
            {
                OnSendListToClient(Cmd.pInfo);
            }
        }

        private void OnCmdDBRResult(TCmdMsg Cmd)
        {
            var ErrCode = string.Empty;
            this.ErrMsg("CmdDBRResult[Friend] :" + Cmd.body);
            var CmdNum = HUtil32.GetValidStr3(Cmd.body, ref ErrCode, new string[] { "/" });
            switch (HUtil32.Str_ToInt(CmdNum, 0))
            {
                case Grobal2.DB_FRIEND_LIST:
                    break;
                case Grobal2.DB_FRIEND_ADD:
                    break;
                case Grobal2.DB_FRIEND_DELETE:
                    break;
                case Grobal2.DB_FRIEND_OWNLIST:
                    break;
                case Grobal2.DB_FRIEND_EDIT:
                    break;
            }
        }
    }
}
