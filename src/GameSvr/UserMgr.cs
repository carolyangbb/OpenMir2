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

        // For FriendMgr...
        // ------------------------------------------------------------------------------
        // 模备 矫胶袍 凯扁
        // ------------------------------------------------------------------------------
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
                // Loading Datas...
                FFriend.OnCmdDBList(FInfo);
                result = true;
            }
            return result;
        }

        // ------------------------------------------------------------------------------
        // 模备 矫胶袍 摧扁
        // ------------------------------------------------------------------------------
        public void CloseFriend()
        {
            if (FFriend != null)
            {
                FFriend.Free();
                FFriend = null;
            }
        }

    } // end TUserfunc

    // TUserMgr Class Declarations ---------------------------------------------
    // 咯矾 矫胶袍阑 蜡历捞抚阑 啊瘤绊 秦矫 府胶飘肺 包府窃
    // -------------------------------------------------------------------------
    public class TUserMgr : TCmdMgr
    {
        private readonly ArrayList FItems = null;
        // TElHashList;
        private object FHumanCS = null;
        // //////////////////////////////////////////////////////////////////////////////
        // TUserMgr
        // //////////////////////////////////////////////////////////////////////////////
        //Constructor  Create()
        public TUserMgr() : base()
        {
            // TO DO Initialize
            FItems = new ArrayList();
            // TElHashList.Create;
            FHumanCS = new object();
        }
        //@ Destructor  Destroy()
        ~TUserMgr()
        {
            RemoveAll();
            FItems.Free();
            FHumanCS.Free();
            // base.Destroy();
        }
        // 蜡历 眠啊
        // ------------------------------------------------------------------------------
        // ADD Info To hash List
        // ------------------------------------------------------------------------------
        public bool Add(string UserName_, int Recog_, int ConnState_, int GateIdx_, int UserGateIdx_, int UserHandle_)
        {
            bool result;
            TUserfunc Info;
            bool ReUse;
            result = false;
            // 鞍篮 捞抚狼 荤侩磊啊 乐唱焊绊
            if (GetUserFunc(UserName_, ref Info))
            {
                this.ErrMsg("Exist User !:" + UserName_);
                ReUse = true;
            }
            else
            {
                // 皋葛府 积己
                Info = new TUserfunc();
                ReUse = false;
            }
            if (Info != null)
            {
                // 单捞磐甫 货肺 盎脚窍磊
                Info.FInfo.UserName = UserName_;
                Info.FInfo.Recog = Recog_;
                Info.FInfo.ConnState = ConnState_;
                Info.FInfo.GateIdx = GateIdx_;
                Info.FInfo.UserGateIdx = UserGateIdx_;
                Info.FInfo.UserHandle = UserHandle_;
                // ToTest
                // ErrMsg( UserName_ +':'+ IntTostr( Recog_ ) + ':'+IntToStr( UserHandle_ ));
                // 皋葛府 犁荤侩老 版快俊绰 弊成 逞绢啊磊
                if (!ReUse)
                {
                    FItems.Add(Info);
                }
                // FItems.Add ( UserName_ , Info );
                // 郴何 辑宏 矫胶袍阑 楷促....
                OpenUser(UserName_);
                // 模备 包访 家蜡磊 府胶飘 何福磊
                // UserHandle_ = 0 捞搁 促弗辑滚俊辑 立加茄 荤恩烙
                if (Info.IsRealUser())
                {
                    OnCmdDBOwnList(Info.FInfo);
                }
                result = true;
            }
            return result;
        }

        // 蜡历捞抚
        // Hum 狼 皋葛府 锅龋
        // 立加 惑怕
        // 霸捞飘 锅龋
        // 蜡历 霸捞飘 锅龋
        // 蜡历 勤甸
        public TUserfunc Find(string UserName_)
        {
            TUserfunc result;
            TUserfunc Item;
            int i;
            result = null;
            for (i = 0; i < FItems.Count; i++)
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

        // 蜡历 昏力
        // ------------------------------------------------------------------------------
        // Delete Info From hash List
        // ------------------------------------------------------------------------------
        public bool Delete(string UserName_)
        {
            bool result;
            TUserfunc Item;
            int i;
            result = false;
            Item = Find(UserName_);
            // FItems.Item[ UserName_ ];
            if (Item != null)
            {
                // 模备 包访 家蜡磊 府胶飘 何福磊
                if (Item.IsRealUser())
                {
                    OnCmdDBOwnList(Item.FInfo);
                }
                i = FItems.IndexOf(Item);
                if (i >= 0)
                {
                    // FItems.Delete( UserName_ );
                    FItems.RemoveAt(i);
                    Item.Free();
                    result = true;
                }
            }
            return result;
        }

        // ------------------------------------------------------------------------------
        // Open Sub System and Send Info To Others
        // ------------------------------------------------------------------------------
        public void OpenUser(string UserName_)
        {
            TUserfunc Item;
            Item = Find(UserName_);
            // FItems.Item[ UserName_ ];
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

        // 辑宏矫胶袍阑 角青啊瓷窍霸
        // ------------------------------------------------------------------------------
        // CLose Sub System and Send Info To Others
        // ------------------------------------------------------------------------------
        public void CloserUser(string UserName_)
        {
            TUserfunc Item;
            Item = Find(UserName_);
            // FItems.Item[ UserName_ ];
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

        // 郴何 蜡历 傈何 昏力
        // ------------------------------------------------------------------------------
        // Delete All Info From hash List
        // ------------------------------------------------------------------------------
        private void RemoveAll()
        {
            int i;
            TUserfunc Item;
            // TO DO Free Mem
            for (i = 0; i < FItems.Count; i++)
            {
                Item = (TUserfunc)FItems[i];
                Item.Free();
            }
            FItems.Clear();
        }

        // ------------------------------------------------------------------------------
        // Find And Get Userfunc From hash List
        // ------------------------------------------------------------------------------
        public bool GetUserFunc(string UserName_, ref TUserfunc UserFunc_)
        {
            bool result;
            TUserfunc Item;
            Item = Find(UserName_);
            // FItems.Item[ UserName_ ];
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

        // 辑宏矫胶袍阑 角青阂啊瓷窍霸
        // ------------------------------------------------------------------------------
        // Find And Get UserInfo From hash List
        // ------------------------------------------------------------------------------
        public bool GetUserInfo(string UserName_, ref TUserInfo UserInfo_)
        {
            bool result;
            TUserfunc Item;
            UserInfo_ = null;
            result = false;
            Item = Find(UserName_);
            // FItems.Item[ UserName_ ];
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

        // For UserInfo...
        // 立加 沥焊 函版
        // ------------------------------------------------------------------------------
        // Change Info's ConnState
        // ------------------------------------------------------------------------------
        public bool SetConnState(string UserName_, int ConnState_)
        {
            bool result;
            TUserInfo Item;
            result = false;
            if (GetUserInfo(UserName_, ref Item))
            {
                Item.ConnState = ConnState_;
                result = true;
            }
            return result;
        }

        // ------------------------------------------------------------------------------
        // The Event Call When Command is Changed
        // ------------------------------------------------------------------------------
        public override void OnCmdChange(ref TCmdMsg Msg)
        {
            TUserfunc Func;
            switch (Msg.CmdNum)
            {
                case Grobal2.DBR_FRIEND_WONLIST:
                    // UserMgr 俊辑 贸府且 疙飞绢
                    OnCmdDBROwnList(Msg);
                    return;
                    break;
                case Grobal2.DBR_LM_LIST:
                    OnCmdDBRLMList(Msg);
                    return;
                    break;
                case Grobal2.ISM_FUNC_USEROPEN:
                    // 蜡历甫 眠啊茄促.
                    if (Add(Msg.UserName, Msg.Msg.Recog, Msg.Msg.Param, Msg.GateIdx, Msg.UserGateIdx, Msg.Userhandle))
                    {
                        // UserHandle_ = 0 捞搁 促弗辑滚俊辑 立加茄 荤恩捞促
                        // 辑宏 矫胶袍篮 凯瘤 臼绰促.
                        if (Msg.Userhandle != 0)
                        {
                            OpenSubSystem(Msg.UserName, TSystemType.stFriend);
                            OpenSubSystem(Msg.UserName, TSystemType.stTag);
                        }
                    }
                    return;
                    break;
                case Grobal2.ISM_FUNC_USERCLOSE:
                    CloserUser(Msg.UserName);
                    Delete(Msg.UserName);
                    return;
                    break;
            }
            if (GetUserFunc(Msg.UserName, ref Func))
            {
                // TO TEST: 绢恫 疙飞绢啊 吭绰瘤 焊咯霖促.
                // ErrMsg( Format('%d,%d,%d,%d,%d,%s',
                // [Msg.Msg.Recog , Msg.Msg.Ident ,Msg.Msg.Param ,Msg.Msg.Tag ,Msg.Msg.Series, Msg.Body ]));
                Msg.pInfo = Func.FInfo;
                // Friend System -----------------------------------
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
                // UserInfo 啊绝栏搁 救凳
                // Friend System -----------------------------------
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
                // Tag System --------------------------------------
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
                            return;
                            break;
                    }
                }
            }
            // if GetUserFunc...

        }

        // 率瘤 肚绰 模备 矫胶袍殿阑 积己
        // ------------------------------------------------------------------------------
        // 郴何 矫胶袍吝 模备客 率瘤甫 货肺 父电促.
        // ------------------------------------------------------------------------------
        public void OpenSubSystem(string UserName_, TSystemType SystemType)
        {
            TUserfunc userfunc;
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

        // 率瘤 肚绰 模备 矫胶袍殿阑 昏力
        // ------------------------------------------------------------------------------
        // 郴何矫胶袍吝 模备客 率瘤甫 昏力茄促.
        // ------------------------------------------------------------------------------
        public void CloseSubSystem(string Username_, TSystemType SystemType)
        {
            TUserfunc userfunc;
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

        // DB 率栏肺 疙飞 傈价
        // ------------------------------------------------------------------------------
        // DB_FRIEND_OWNLIST  :  DB俊 模备肺 殿废等 荤恩甸 府胶飘 夸没
        // Params  : 绝澜
        // ------------------------------------------------------------------------------
        public void OnCmdDBOwnList(TUserInfo UserInfo)
        {
            // 家蜡磊 府胶飘 傈价 陛瘤 2003-07-01
            // 
            // UserMgrEngine.InterSendMsg(   stDBServer ,ServerIndex,0,0,0,
            // UserInfo.UserName ,0, DB_FRIEND_OWNLIST ,
            // 0,0,0,'' );

        }

        // DB 率俊辑 流立 傈价凳
        // ------------------------------------------------------------------------------
        // DBR_FRIEND_OWNLIST  :  DB俊辑 焊郴绰 模备肺 殿废茄荤恩 府胶飘
        // Params  : 模备肺 殿废茄荤恩 府胶飘
        // ------------------------------------------------------------------------------
        public void OnCmdDBROwnList(TCmdMsg Cmd)
        {
            int ListCount;
            string Friend;
            int i;
            int ConnState;
            string TempStr;
            string BodyStr;
            string MapInfo;
            TUserInfo userinfo;
            // TO DO : 府胶飘肺 User甫  殿废茄 荤恩甸俊霸 立加沁澜阑 傈价
            BodyStr = GetValidStr3(Cmd.body, TempStr, new string[] { "/" });
            // 府胶飘 俺荐 掘扁
            ListCount = HUtil32.Str_ToInt(TempStr, 0);
            // 目池记 惑怕甫 茫绰促.
            ConnState = 0;
            MapInfo = "";
            if (GetUserInfo(Cmd.UserName, ref userinfo))
            {
                ConnState = userinfo.ConnState;
                MapInfo = userinfo.MapInfo;
            }
            for (i = 0; i < ListCount; i++)
            {
                // 盒府磊肺 俺牢夸家甫 盒府茄促.
                BodyStr = GetValidStr3(BodyStr, Friend, new string[] { "/" });
                if (Friend != "")
                {
                    // 辑滚俊 乐绰 荤恩甸俊霸父 焊辰促
                    if (GetUserInfo(Friend, ref userinfo))
                    {
                        OnSendInfoToOthers(Cmd.UserName, ConnState, MapInfo, Friend);
                    }
                }
            }
        }

        // ------------------------------------------------------------------------------
        // 包拌府胶飘甫 佬菌促.
        // ------------------------------------------------------------------------------
        public void OnCmdDBRLMList(TCmdMsg Cmd)
        {
            FHumanCS.Enter();
            try
            {
                svMain.UserEngine.ExternSendMessage(Cmd.UserName, Grobal2.RM_LM_DBGETLIST, 0, 0, 0, 0, Cmd.body);
            }
            finally
            {
                FHumanCS.Leave();
            }
        }

        // ------------------------------------------------------------------------------
        // 磊脚阑 模备殿废茄 蜡历俊霸 立加登菌澜阑 舅赴促.
        // ------------------------------------------------------------------------------
        public void OnSendInfoToOthers(string UserName, int ConnState, string MapInfo, string LinkedFriend)
        {
            string str;
            str = UserName + "/" + ConnState.ToString() + "/" + MapInfo + "/";
            svMain.UserMgrEngine.InterSendMsg(TSendTarget.stOtherServer, svMain.ServerIndex, 0, 0, 0, LinkedFriend, 0, Grobal2.ISM_USER_INFO, (ushort)svMain.ServerIndex, 0, 0, str);
            // 
            // // 模备 沥焊甫 掘磊
            // if g_UserMgr.GetUserInfo( LinkedFriend  , FriendInfo) then
            // begin
            // 
            // // 立加等 荤恩父 舅酒郴辑
            // if FriendInfo.ConnState >= CONNSTATE_CONNECT_0 then
            // begin
            // // 模备皋聪历救狼 模备沥焊甫 掘磊.
            // ItemInfo := FItems.Item[LinkedFriend];
            // if ( ItemInfo <> nil ) then
            // begin
            // 
            // // 磊脚狼 辑滚客 鞍篮 辑滚俊乐绰荤恩捞搁
            // if FriendInfo.ConnState = ( ServerIndex + CONNSTATE_CONNECT_0) then
            // begin
            // // TO DO : 努扼捞攫飘俊霸 目池记 沥焊甫 焊晨
            // OnCmdSMUserInfo( F
            // end
            // else
            // begin
            // // TO DO : 促弗辑滚肺 目匙记 沥焊甫 焊晨
            // 
            // end;
            // 
            // end;// if ItemInfo <> nil .../
            // end; // if FriendInfo.ConnState...
            // 
            // end

        }

    } // end TUserMgr

    // ElHashList ,
    // Sub System 's Type
    public enum TSystemType
    {
        stTag,
        stFriend
    } // end TSystemType

}

