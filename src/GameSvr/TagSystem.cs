using System;
using System.Collections;
using SystemModule;

namespace GameSvr
{

    public class TTagInfo : ICommand
    {
        public string Sender
        {
            get
            {
                return FSender;
            }
            set
            {
                FSender = value;
            }
        }
        
        public string SendDate
        {
            get
            {
                return FSendDate;
            }
            set
            {
                FSendDate = value;
            }
        }
        
        public string Msg
        {
            get
            {
                return FMsg;
            }
            set
            {
                FMsg = value;
            }
        }
        
        public int State
        {
            get
            {
                return FState;
            }
            set
            {
                FState = value;
            }
        }
        
        public bool DBSaved
        {
            get
            {
                return FDBSaved;
            }
            set
            {
                FDBSaved = value;
            }
        }
        
        public bool Client
        {
            get
            {
                return FClient;
            }
            set
            {
                FClient = value;
            }
        }
        
        private string FSender = String.Empty;
        private string FSendDate = String.Empty;
        private string FMsg = String.Empty;
        private int FState = 0;
        private bool FDBSaved = false;
        private bool FClient = false;

        public TTagInfo() : base()
        {
            FSender = "";
            FSendDate = "";
            FMsg = "";
            FState = 0;
            FDBSaved = false;
            FClient = false;
        }

        public string GetMsgList()
        {
            return  FState.ToString() + ":" + FSendDate + ":" + FSender + ":" + FMsg;
        }

    }

    public class TTagMgr : ICommand
    {
        private readonly ArrayList FItems = null;
        private readonly ArrayList FRejecter = null;
        private int FNotReadCount = 0;
        private bool FIsTagListSendAble = false;
        private bool FWantTagListFlag = false;
        private int FWantTagListPage = 0;
        private bool FClientGetList = false;
        private bool FIsRejectListSendAble = false;
        private bool FWantRejectListFlag = false;

        public TTagMgr() : base()
        {
            FItems = new ArrayList();
            FRejecter = new ArrayList();
            FIsTagListSendAble = false;
            FWantTagListFlag = false;
            FWantTagListPage = -1;
            FClientGetList = false;
            FIsRejectListSendAble = false;
            FWantRejectListFlag = false;
            FNotReadCount = 0;
        }
        
        ~TTagMgr()
        {
            RemoveAll();
            FItems.Free();
            FRejecter.Clear();
            FRejecter.Free();
        }

        public void OnUserOpen()
        {
            
        }
        
        public void OnUserClose()
        {
            
        }

        public int GetTagCount()
        {
            return FItems.Count;
        }
        
        public bool IsTagAddAble()
        {
            var result = false;
            if (GetTagCount() < TagSystem.MAX_TAG_COUNT)
            {
                result = true;
            }
            return result;
        }

        // 脚痹 抛弊锅龋 积己
        // ------------------------------------------------------------------------------
        // 率瘤 傈价 朝楼 积己
        // ------------------------------------------------------------------------------
        public string GenerateSendDate()
        {
            string result;
            result = FormatDateTime("yymmddhhnnss", DateTime.Now);
            return result;
        }

        public TTagInfo Find(string SendDate)
        {
            TTagInfo Item;
            TTagInfo result = null;
            for (var i = 0; i < FItems.Count; i++)
            {
                Item = FItems[i] as TTagInfo;
                if (Item.FSendDate == SendDate)
                {
                    result = Item;
                    return result;
                }
            }
            return result;
        }

        public bool Add(TUserInfo UserInfo, string Sender, string SendDate, int State, string Msg)
        {
            var result = false;
            if (IsTagAddAble())
            {
                var Info = new TTagInfo();
                if (Info != null)
                {
                    Info.Sender = Sender;
                    Info.SendDate = SendDate;
                    Info.FState = State;
                    Info.Msg = Msg;
                    FItems.Add(Info);
                    if (Info.State == Grobal2.TAGSTATE_NOTREAD)
                    {
                        FNotReadCount++;
                    }
                    result = true;
                }
            }
            return result;
        }

        // 昏力 : 惑怕甫 TAGSTATE_DELETED 肺 官厕
        // ------------------------------------------------------------------------------
        // 率瘤 昏力
        // ------------------------------------------------------------------------------
        public bool Delete(TUserInfo UserInfo, string SendDate, ref int rState)
        {
            bool result;
            TTagInfo Info;
            result = false;
            // 率瘤沥焊甫 掘绊
            Info = Find(SendDate);
            // FItems.Item[SendDate];
            // 沥焊啊 乐栏搁
            if (Info != null)
            {
                // 昏力 陛瘤 惑怕搁 昏力 陛瘤
                if (Info.State == Grobal2.TAGSTATE_DONTDELETE)
                {
                    rState = Grobal2.TAGSTATE_DONTDELETE;
                    return result;
                }
                else
                {
                    // 昏力 啊瓷窍搁 昏力灯澜栏肺 加己 函版
                    Info.State = Grobal2.TAGSTATE_DELETED;
                    rState = Info.State;
                    result = true;
                    return result;
                }
            }
            // if ( Info <> nil ) ...

            return result;
        }

        // 惑怕 函版
        // ------------------------------------------------------------------------------
        // 惑怕 函版
        // 函版夸备 惑怕 : 0 ( 佬瘤臼澜 ) , 1( 佬澜 ) , 2 ( 昏力陛瘤 ) , 3 ( 昏力陛瘤 秦力)
        // ------------------------------------------------------------------------------
        public bool SetInfo(TUserInfo UserInfo, string SendDate, ref int state)
        {
            bool result;
            TTagInfo Info;
            result = false;
            Info = Find(SendDate);
            // FItems.Item[SendDate];
            if (Info != null)
            {
                // 率瘤甫 佬篮版快俊绰 救佬澜 俺荐甫 窍唱 临捞磊
                if ((Info.FState == Grobal2.TAGSTATE_NOTREAD) && (state != Grobal2.TAGSTATE_NOTREAD))
                {
                    if (FNotReadCount > 0)
                    {
                        FNotReadCount -= 1;
                    }
                }
                // 昏力陛瘤 秦力老 版快俊绰 佬澜栏肺 函版茄促.
                if (state == Grobal2.TAGSTATE_WANTDELETABLE)
                {
                    state = Grobal2.TAGSTATE_READ;
                }
                // 胶抛捞飘 函版
                Info.FState = state;
                result = true;
            }
            return result;
        }

        // 瘤沥等 朝楼狼 率瘤 昏力
        // ------------------------------------------------------------------------------
        // 瘤沥等 朝楼狼 率瘤 昏力
        // ------------------------------------------------------------------------------
        public void RemoveInfo(string Date)
        {
            TTagInfo Info;
            int i;
            Info = Find(Date);
            // FItems.Item[Date];
            if (Info != null)
            {
                i = FItems.IndexOf(Info);
                if (i >= 0)
                {
                    FItems.RemoveAt(i);
                    // FItems.Delete( Date );
                    Info.Free();
                    return;
                }
            }
        }

        // 傈眉 昏力
        // ------------------------------------------------------------------------------
        // 郴何 率瘤 皋葛府 傈何 昏力
        // ------------------------------------------------------------------------------
        public void RemoveAll()
        {
            int i;
            TTagInfo Info;
            for (i = 0; i < FItems.Count; i++)
            {
                Info = (TTagInfo)FItems[i];
                if (Info != null)
                {
                    Info.Free();
                }
            }
            FItems.Clear();
        }

        // 芭何磊 俺荐
        // ------------------------------------------------------------------------------
        // 芭何磊 俺荐 掘扁
        // ------------------------------------------------------------------------------
        public int GetRejecterCount()
        {
            int result;
            result = FRejecter.Count;
            return result;
        }

        // 芭何磊甫 殿废且 荐 乐唱 八配
        // ------------------------------------------------------------------------------
        // 芭何磊 眠啊 啊瓷茄啊.
        // ------------------------------------------------------------------------------
        public bool IsRejecterAddAble(string Name)
        {
            bool result;
            string Str;
            // 捞抚捞 乐绊
            // 捞固 甸绢乐瘤 臼绊
            // 弥措 俺荐甫 逞瘤臼酒具窃
            if ((Name != "") && (false == FindRejecter(Name, Str)) && (GetRejecterCount() < TagSystem.MAX_REJECTER_COUNT))
            {
                result = true;
            }
            else
            {
                result = false;
            }
            return result;
        }

        // 芭何磊 眠啊
        // ------------------------------------------------------------------------------
        // 芭何磊 眠啊
        // ------------------------------------------------------------------------------
        public bool AddRejecter(string Rejecter)
        {
            bool result;
            result = false;
            if (IsRejecterAddAble(Rejecter))
            {
                // new (pStr);
                // pStr^ := Rejecter;
                FRejecter.Add(Rejecter);
                // FRejecter.Add ( Rejecter , pStr );
                result = true;
            }
            return result;
        }

        // 芭何磊 昏力
        // ------------------------------------------------------------------------------
        // 芭何磊 昏力
        // ------------------------------------------------------------------------------
        public bool DeleteRejecter(string Rejecter)
        {
            bool result;
            string Str;
            int i;
            result = false;
            if (FindRejecter(Rejecter, Str))
            {
                i = FRejecter.IndexOf(Rejecter);
                if (i >= 0)
                {
                    FRejecter.Remove(i);
                    // FRejecter.Delete ( Rejecter );
                    // Dispose( pStr);
                    result = true;
                }
            }
            return result;
        }

        // 芭何磊 茫扁
        // ------------------------------------------------------------------------------
        // 芭何磊 茫扁
        // ------------------------------------------------------------------------------
        public bool FindRejecter(string Rejecter, string pName)
        {
            bool result;
            int i;
            pName = "";
            result = false;
            // pStr := FRejecter.Item[Rejecter];
            for (i = 0; i < FRejecter.Count; i++)
            {
                // pStr := PString(FRejecter.Items[i]);
                if (FRejecter[i] == Rejecter)
                {
                    pName = (string)FRejecter[i];
                    result = true;
                    return result;
                }
            }
            return result;
        }

        // 芭何磊 茫扁
        // ------------------------------------------------------------------------------
        // 芭何磊 茫扁
        // ------------------------------------------------------------------------------
        public bool IsRejecter(string Rejecter)
        {
            bool result;
            int i;
            result = false;
            // pStr := FRejecter.Item[Rejecter];
            for (i = 0; i < FRejecter.Count; i++)
            {
                // pStr := PString(FRejecter.Items[i]);
                if (FRejecter[i] == Rejecter)
                {
                    result = true;
                    return result;
                }
            }
            return result;
        }

        // 芭何磊 傈眉 昏力
        // ------------------------------------------------------------------------------
        // 葛电 芭何磊 皋葛府甫 昏力茄促.
        // ------------------------------------------------------------------------------
        public void RemoveAllRejecter()
        {
            // 
            // for i := 0 to FRejecter.Count - 1 do
            // begin
            // pStr := FRejecter.Items[i];        //FRejecter.GetByIndex(i);
            // if ( pStr <> nil ) then
            // begin
            // Dispose( pStr );
            // pStr := nil;
            // end;
            // end;
            // 
            FRejecter.Clear();
        }

        // 疙飞绢 傈价罐菌阑锭 惯积窍绰 捞亥飘
        // ------------------------------------------------------------------------------
        // 疙飞绢 傈价罐疽阑 版快 惯积窍绰 捞亥飘 坷滚肺靛凳
        // ------------------------------------------------------------------------------
        public override void OnCmdChange(ref TCmdMsg Msg)
        {
            switch (Msg.CmdNum)
            {
                case Grobal2.CM_TAG_ADD:
                    OnCmdCMAdd(Msg);
                    break;
                case Grobal2.CM_TAG_ADD_DOUBLE:
                    OnCmdCMAddDouble(Msg);
                    break;
                case Grobal2.CM_TAG_DELETE:
                    // sonmg
                    OnCmdCMDelete(Msg);
                    break;
                case Grobal2.CM_TAG_SETINFO:
                    OnCmdCMSetInfo(Msg);
                    break;
                case Grobal2.CM_TAG_LIST:
                    OnCmdCMList(Msg);
                    break;
                case Grobal2.CM_TAG_NOTREADCOUNT:
                    OnCmdCMNotReadCount(Msg);
                    break;
                case Grobal2.CM_TAG_REJECT_LIST:
                    OnCmdCMRejectList(Msg);
                    break;
                case Grobal2.CM_TAG_REJECT_ADD:
                    OnCmdCMRejectAdd(Msg);
                    break;
                case Grobal2.CM_TAG_REJECT_DELETE:
                    OnCmdCMRejectDelete(Msg);
                    break;
                case Grobal2.ISM_TAG_SEND:
                    OnCmdISMSend(Msg);
                    break;
                case Grobal2.ISM_TAG_RESULT:
                    OnCmdISMResult(Msg);
                    break;
                case Grobal2.DBR_TAG_LIST:
                    OnCmdDBRList(Msg);
                    break;
                case Grobal2.DBR_TAG_REJECT_LIST:
                    OnCmdDBRRejectList(Msg);
                    break;
                case Grobal2.DBR_TAG_NOTREADCOUNT:
                    OnCmdDBRNotReadCount(Msg);
                    break;
                case Grobal2.DBR_TAG_RESULT:
                    OnCmdDBRResult(Msg);
                    break;
            }
        }

        // 皋技瘤 府胶飘 焊郴扁
        // ------------------------------------------------------------------------------
        // 努扼攫飘俊 率瘤 府胶飘福 傈价茄促.
        // ------------------------------------------------------------------------------
        public void OnMsgList(TUserInfo UserInfo, int PageNum)
        {
            int i;
            int startnum;
            int endnum;
            int listcount;
            int Cnt;
            string TempStr;
            TTagInfo taginfo;
            listcount = GetTagCount() - 1;
            // 其捞瘤 锅龋啊 0 捞搁 傈眉傈价茄促.
            if (PageNum == 0)
            {
                startnum = 0;
                endnum = listcount;
            }
            else
            {
                startnum = (PageNum - 1) * TagSystem.MAX_TAG_PAGE_COUNT;
                endnum = startnum + TagSystem.MAX_TAG_PAGE_COUNT;
            }
            TempStr = "";
            // 傈价 矫累锅龋啊 府胶飘 农扁 救栏肺 甸绢坷绊
            if (startnum <= listcount)
            {
                // 傈价 场锅龋啊 府胶飘 农扁焊促 农搁 场锅龋肺 涵版茄促.
                if (endnum > listcount)
                {
                    endnum = listcount;
                }
                // 矫累锅龋 - 场锅龋鳖瘤狼 郴侩栏肺 府胶飘 备己
                Cnt = 0;
                // for i := startnum to endnum do begin
                for (i = endnum; i >= startnum; i--)
                {
                    // 芭操肺
                    taginfo = (TTagInfo)FItems[i];
                    // FItems.GetByIndex(i);
                    TempStr = TempStr + taginfo.GetMsgList() + "/";
                    Cnt++;
                }
                // 努扼捞攫飘肺 傈价
                OnCmdSMList(UserInfo, PageNum, Cnt, TempStr);
                // 努扼攫飘啊 府胶飘甫 啊瘤绊 乐促
                FClientGetList = true;
            }
        }

        public void OnMsgRejectList(TUserInfo UserInfo)
        {
            int i;
            int Cnt;
            string TempStr;
            // 傈价且膊 绝栏搁 秒家
            if (FRejecter.Count == 0)
            {
                return;
            }
            TempStr = "";
            Cnt = 0;
            for (i = 0; i < FRejecter.Count; i++)
            {
                TempStr = TempStr + FRejecter[i] + "/";
                Cnt++;
            }
            OnCmdSMRejectList(UserInfo, Cnt, TempStr);
        }

        public void OnCmdCMAdd(TCmdMsg Cmd)
        {
            TUserInfo recieverinfo;
            string reciever;
            string tagmsg = HUtil32.GetValidStr3(Cmd.body, ref reciever, new string[] { "/" });
            string senddate = GenerateSendDate();
            if (svMain.UserMgrEngine.InterGetUserInfo(reciever, ref recieverinfo))
            {
                OnCmdOSMSend(reciever, recieverinfo.ConnState - Grobal2.CONNSTATE_CONNECT_0, Cmd.UserName, senddate, 0, tagmsg);
            }
            OnCmdDBAdd(Cmd.pInfo, reciever, senddate, 0, tagmsg);
        }

        public void OnCmdCMAddDouble(TCmdMsg Cmd)
        {
            string receiver;
            string receiver2;
            string tagmsg;
            string senddate;
            TUserInfo receiverinfo;
            TUserInfo receiverinfo2;
            tagmsg  =  HUtil32.GetValidStr3(Cmd.body, ref receiver, new string[] { "/" });
            tagmsg  =  HUtil32.GetValidStr3(tagmsg, ref receiver2, new string[] { "/" });
            senddate = GenerateSendDate();
            if (receiver != Cmd.UserName)
            {
                if (svMain.UserMgrEngine.InterGetUserInfo(receiver, ref receiverinfo))
                {
                    OnCmdOSMSend(receiver, receiverinfo.ConnState - Grobal2.CONNSTATE_CONNECT_0, Cmd.UserName, senddate, 0, tagmsg);
                }
                OnCmdDBAdd(Cmd.pInfo, receiver, senddate, 0, tagmsg);
            }
            // 滴锅掳 荐脚磊俊霸 傈价.
            if (receiver2 != Cmd.UserName)
            {
                if (receiver2 == "---")
                {
                    return;
                }
                if (svMain.UserMgrEngine.InterGetUserInfo(receiver2, ref receiverinfo2))
                {
                    OnCmdOSMSend(receiver2, receiverinfo2.ConnState - Grobal2.CONNSTATE_CONNECT_0, Cmd.UserName, senddate, 0, tagmsg);
                }
                OnCmdDBAdd(Cmd.pInfo, receiver2, senddate, 0, tagmsg);
            }
        }

        public void OnCmdCMDelete(TCmdMsg Cmd)
        {
            string senddate;
            int deletemode;
            int rState;
            TUserInfo userinfo;
            // 疙妨绢 盒籍
            senddate = Cmd.body;
            deletemode = Cmd.Msg.Param;
            userinfo = Cmd.pInfo;
            switch (deletemode)
            {
                case 0:
                    // 1 俺 昏力
                    if (Delete(Cmd.pInfo, senddate, ref rState))
                    {
                        // DB俊 疙飞绢 焊郴扁
                        OnCmdDBDelete(userinfo, senddate);
                        // 努扼捞攫飘俊 疙飞绢 焊郴扁
                        OnCmdSMDelete(userinfo, senddate, rState);
                        // 角力肺 皋葛府俊辑 昏力
                        RemoveInfo(senddate);
                    }
                    break;
                case 1:
                    break;
                    // 佬篮巴 傈何 昏力
            }
        }

        // ------------------------------------------------------------------------------
        // CM_TAG_LIST    : 率瘤 府胶飘 夸备
        // Param    : 其捞瘤 锅龋
        // ------------------------------------------------------------------------------
        public void OnCmdCMList(TCmdMsg Cmd)
        {
            int pagenum;
            pagenum = Cmd.Msg.Param;
            // 傈价啊瓷窍搁
            if (FIsTagListSendAble)
            {
                OnMsgList(Cmd.pInfo, pagenum);
            }
            else
            {
                // 傈价捞 阂啊瓷窍促. DB肺何磐 酒流 府胶飘啊 档馒窍瘤 臼澜
                FWantTagListFlag = true;
                FWantTagListPage = pagenum;
                // 努腐茄鉴埃 佬绢坷霸 函版茄促.
                OnCmdDBList(Cmd.pInfo);
            }
        }

        // ------------------------------------------------------------------------------
        // CM_TAG_EDIT    : 佬篮惑怕 函版 棺 昏力 陛瘤 秦力
        // Param    : 率瘤朝楼 ,  率瘤 惑怕 函版 锅龋
        // ------------------------------------------------------------------------------
        public void OnCmdCMSetInfo(TCmdMsg Cmd)
        {
            int tagstate;
            string senddate;
            TUserInfo userinfo;
            senddate = Cmd.body;
            tagstate = Cmd.Msg.Param;
            userinfo = Cmd.pInfo;
            if (SetInfo(userinfo, senddate, ref tagstate))
            {
                // 努扼捞攫飘俊 傈价
                OnCmdSMInfo(userinfo, senddate, tagstate);
                // DB 俊 傈价
                OnCmdDBInfo(userinfo, senddate, tagstate);
            }
            else
            {
                // 坷幅 傈价
                OnCmdSMResult(userinfo, Grobal2.CM_TAG_SETINFO, Grobal2.CR_DONTUPDATE);
            }
        }

        // ------------------------------------------------------------------------------
        // CM_TAG_REjECT_ADD   : 芭何磊 眠啊
        // Param    : 芭何磊捞抚
        // ------------------------------------------------------------------------------
        public void OnCmdCMRejectAdd(TCmdMsg Cmd)
        {
            string rejecter;
            TUserInfo userinfo;
            TUserInfo rejectinfo;
            rejecter = Cmd.body;
            userinfo = Cmd.pInfo;
            // 柯扼牢俊 乐绰荤恩父 芭何磊肺 眠啊且 荐 乐促.
            if (!svMain.UserMgrEngine.InterGetUserInfo(rejecter, ref rejectinfo))
            {
                OnCmdSMResult(userinfo, Grobal2.CM_TAG_REJECT_ADD, Grobal2.CR_DONTADD);
                return;
            }
            // 芭何磊 眠啊
            if (AddRejecter(rejecter))
            {
                OnCmdDBRejectAdd(userinfo, rejecter);
                OnCmdSMRejectAdd(userinfo, rejecter);
            }
            else
            {
                OnCmdSMResult(userinfo, Grobal2.CM_TAG_REJECT_ADD, Grobal2.CR_DONTADD);
            }
        }

        // ------------------------------------------------------------------------------
        // CM_TAG_REJECT_DELETE    : 芭何磊 昏力
        // Param    : 昏力且 芭何磊 捞抚
        // ------------------------------------------------------------------------------
        public void OnCmdCMRejectDelete(TCmdMsg Cmd)
        {
            string rejecter;
            TUserInfo userinfo;
            rejecter = Cmd.body;
            userinfo = Cmd.pInfo;
            if (DeleteRejecter(rejecter))
            {
                OnCmdDBRejectDelete(userinfo, rejecter);
                OnCmdSMRejectDelete(userinfo, rejecter);
            }
            else
            {
                OnCmdSMResult(userinfo, Grobal2.CM_TAG_REJECT_DELETE, Grobal2.CR_DONTDELETE);
            }
        }

        // ------------------------------------------------------------------------------
        // CM_TAG_REJECT_LIST    : 芭何磊 府胶飘 夸备
        // Param    : 绝澜
        // ------------------------------------------------------------------------------
        public void OnCmdCMRejectList(TCmdMsg Cmd)
        {
            TUserInfo userinfo;
            userinfo = Cmd.pInfo;
            if (FIsRejectListSendAble)
            {
                OnMsgRejectList(userinfo);
            }
            else
            {
                FWantRejectListFlag = true;
                // OnCmdSMResult(userinfo,CM_TAG_REJECT_LIST , CR_DBWAIT);
            }
        }

        // ------------------------------------------------------------------------------
        // CM_TAG_NOTREADCOUNT    : 佬瘤臼篮 率瘤 俺荐 夸备
        // Param    : 绝澜
        // ------------------------------------------------------------------------------
        public void OnCmdCMNotReadCount(TCmdMsg Cmd)
        {
            TUserInfo userinfo;
            userinfo = Cmd.pInfo;
            if (FIsTagListSendAble)
            {
                OnCmdSMNotReadCount(userinfo, FNotReadCount);
            }
            else
            {
                OnCmdSMResult(userinfo, Grobal2.CM_TAG_NOTREADCOUNT, Grobal2.CR_DBWAIT);
            }
        }

        // 努扼捞攫飘肺 焊郴绰 疙飞绢甸 ........................................
        // 府胶飘 傈价
        // ==============================================================================
        // 努扼捞攫飘肺 焊郴绰 疙飞绢甸
        // ==============================================================================
        // ------------------------------------------------------------------------------
        // SM_TAG_LIST    : 率瘤 府胶飘 傈价
        // Param    : 率瘤 俺荐 , 率瘤 沥焊甸狼 府胶飘
        // ------------------------------------------------------------------------------
        public void OnCmdSMList(TUserInfo UserInfo, int PageNum, int ListCount, string TagList)
        {
            string str;
            str = TagList;
            svMain.UserMgrEngine.InterSendMsg(TSendTarget.stClient, 0, UserInfo.GateIdx, UserInfo.UserGateIdx, UserInfo.UserHandle, UserInfo.UserName, UserInfo.Recog, Grobal2.SM_TAG_LIST, (ushort)PageNum, (ushort)ListCount, 0, str);
        }

        // 率瘤 惑怕 傈价
        // ------------------------------------------------------------------------------
        // SM_TAG_INFO    : 率瘤 惑怕 沥焊 傈价
        // Param    : 率瘤朝楼 , 惑怕沥焊
        // ------------------------------------------------------------------------------
        public void OnCmdSMInfo(TUserInfo UserInfo, string SendDate, int State)
        {
            string str;
            str = SendDate;
            svMain.UserMgrEngine.InterSendMsg(TSendTarget.stClient, 0, UserInfo.GateIdx, UserInfo.UserGateIdx, UserInfo.UserHandle, UserInfo.UserName, UserInfo.Recog, Grobal2.SM_TAG_INFO, (ushort)State, 0, 0, str);
        }

        // 率瘤眠啊 傈价
        // ------------------------------------------------------------------------------
        // SM_TAG_ADD    : 率瘤 眠啊 傈价
        // Param    : 惑怕 : 朝楼 : 傈价磊 : 郴侩
        // ------------------------------------------------------------------------------
        public void OnCmdSMAdd(TUserInfo UserInfo, string Sender, string SendDate, int State, string SendMsg)
        {
            string str;
            // 惑怕: 朝楼:傈价磊:"郴侩"
            str = State.ToString() + ":" + SendDate + ":" + Sender + ":" + SendMsg;
            // pagenum = 0 , sendnum = 1;
            svMain.UserMgrEngine.InterSendMsg(TSendTarget.stClient, 0, UserInfo.GateIdx, UserInfo.UserGateIdx, UserInfo.UserHandle, UserInfo.UserName, UserInfo.Recog, Grobal2.SM_TAG_LIST, 0, 1, 0, str);
        }
        
        public void OnCmdSMDelete(TUserInfo UserInfo, string SendDate, int State)
        {
            var str = SendDate;
            svMain.UserMgrEngine.InterSendMsg(TSendTarget.stClient, 0, UserInfo.GateIdx, UserInfo.UserGateIdx, UserInfo.UserHandle, UserInfo.UserName, UserInfo.Recog, Grobal2.SM_TAG_INFO, (ushort)State, 0, 0, str);
        }
        
        public void OnCmdSMRejectList(TUserInfo UserInfo, int ListCount, string RejectList)
        {
            var str = RejectList;
            svMain.UserMgrEngine.InterSendMsg(TSendTarget.stClient, 0, UserInfo.GateIdx, UserInfo.UserGateIdx, UserInfo.UserHandle, UserInfo.UserName, UserInfo.Recog, Grobal2.SM_TAG_REJECT_LIST, (ushort)ListCount, 0, 0, str);
        }
        
        public void OnCmdSMRejectAdd(TUserInfo UserInfo, string Rejecter)
        {
            var str = Rejecter;
            svMain.UserMgrEngine.InterSendMsg(TSendTarget.stClient, 0, UserInfo.GateIdx, UserInfo.UserGateIdx, UserInfo.UserHandle, UserInfo.UserName, UserInfo.Recog, Grobal2.SM_TAG_REJECT_ADD, 0, 0, 0, str);
        }
        
        public void OnCmdSMRejectDelete(TUserInfo UserInfo, string Rejecter)
        {
            var str = Rejecter;
            svMain.UserMgrEngine.InterSendMsg(TSendTarget.stClient, 0, UserInfo.GateIdx, UserInfo.UserGateIdx, UserInfo.UserHandle, UserInfo.UserName, UserInfo.Recog, Grobal2.SM_TAG_REJECT_DELETE, 0, 0, 0, str);
        }
        
        public void OnCmdSMNotReadCount(TUserInfo UserInfo, int NotReadCount)
        {
            svMain.UserMgrEngine.InterSendMsg(TSendTarget.stClient, 0, UserInfo.GateIdx, UserInfo.UserGateIdx, UserInfo.UserHandle, UserInfo.UserName, UserInfo.Recog, Grobal2.SM_TAG_ALARM, (ushort)NotReadCount, 0, 0, "");
        }

        public void OnCmdSMResult(TUserInfo UserInfo, short CmdNum, short ResultValue)
        {
            svMain.UserMgrEngine.InterSendMsg(TSendTarget.stClient, 0, UserInfo.GateIdx, UserInfo.UserGateIdx, UserInfo.UserHandle, UserInfo.UserName, UserInfo.Recog, Grobal2.SM_TAG_RESULT, (ushort)CmdNum, (ushort)ResultValue, 0, "");
        }
        
        public void OnCmdISMSend(TCmdMsg Cmd)
        {
            string sender=String.Empty;
            string senddate=String.Empty;
            string statestr=String.Empty;
            int state = 0;
            string sendmsg=String.Empty;
            string tempstr=String.Empty;
            TUserInfo userinfo;
            tempstr = HUtil32.GetValidStr3(Cmd.body, ref statestr, new string[] { ":" });
            tempstr = HUtil32.GetValidStr3(tempstr, ref senddate, new string[] { ":" });
            sendmsg = HUtil32.GetValidStr3(tempstr, ref sender, new string[] { ":" });
            userinfo = Cmd.pInfo;
            state = HUtil32.Str_ToInt(statestr, 0);
            if (!IsRejecter(sender))
            {
                if (Add(userinfo, sender, senddate, state, sendmsg))
                {
                    if (FClientGetList)
                    {
                        OnCmdSMAdd(userinfo, sender, senddate, state, sendmsg);
                    }
                    OnCmdSMNotReadCount(userinfo, FNotReadCount);
                }
            }
        }
        
        public void OnCmdISMResult(TCmdMsg Cmd)
        {
        }
        
        public void OnCmdOSMSend(string UserName, int SvrIndex, string Sender, string SendDate, int State, string SendMsg)
        {
            var str = State.ToString() + ":" + SendDate + ":" + Sender + ":" + SendMsg;
            svMain.UserMgrEngine.InterSendMsg(TSendTarget.stOtherServer, 0, 0, 0, 0, UserName, 0, Grobal2.ISM_TAG_SEND, (ushort)SvrIndex, 0, 0, str);
        }
        
        public void OnCmdOSMResult(string UserName, int SvrIndex, short CmdNum, short ResultValue)
        {
            string str;
            str = CmdNum.ToString() + ":" + ResultValue.ToString();
            svMain.UserMgrEngine.InterSendMsg(TSendTarget.stOtherServer, 0, 0, 0, 0, UserName, 0, Grobal2.ISM_TAG_RESULT, (ushort)SvrIndex, 0, 0, str);
        }
        
        public void OnCmdDBRList(TCmdMsg Cmd)
        {
            string tagcountstr=String.Empty;
            string sender=String.Empty;
            string senddate=String.Empty;
            string statestr=String.Empty;
            string sendmsg=String.Empty;
            string tempstr=String.Empty;
            string tagstr=String.Empty;
            int tagcount = 0;
            TUserInfo userinfo;
            tempstr = HUtil32.GetValidStr3(Cmd.body,ref tagcountstr, new string[] { "/" });
            tagcount = HUtil32.Str_ToInt(tagcountstr, 0);
            userinfo = Cmd.pInfo;
            RemoveAll();
            for (var i = 0; i < tagcount; i++)
            {
                tempstr = HUtil32.GetValidStr3(tempstr,ref  tagstr, new string[] { "/" });
                tagstr = HUtil32.GetValidStr3(tagstr,ref  statestr, new string[] { ":" });
                tagstr = HUtil32.GetValidStr3(tagstr,ref  senddate, new string[] { ":" });
                sendmsg = HUtil32.GetValidStr3(tagstr,ref  sender, new string[] { ":" });
                if (!Add(userinfo, sender, senddate, HUtil32.Str_ToInt(statestr, 0), sendmsg))
                {
                    // MainOutMessage('Tag didn''t Added...');
                }
            }
            FIsTagListSendAble = true;
            if (FWantTagListFlag)
            {
                OnMsgList(userinfo, FWantTagListPage);
                FWantTagListFlag = false;
            }
        }
        
        public void OnCmdDBRRejectList(TCmdMsg Cmd)
        {
            string tempstr=String.Empty;
            string rejecter=String.Empty;
            string rejectcountstr=String.Empty;
            int rejectcount = 0;
            int i;
            TUserInfo userinfo;
            tempstr  =  HUtil32.GetValidStr3(Cmd.body, ref rejectcountstr, new string[] { "/" });
            rejectcount = HUtil32.Str_ToInt(rejectcountstr, 0);
            userinfo = Cmd.pInfo;
            for (i = 0; i < rejectcount; i++)
            {
                // 率瘤俊 包访等逞阑 啊廉柯促.
                tempstr  =  HUtil32.GetValidStr3(tempstr, ref rejecter, new string[] { "/" });
                if (!AddRejecter(rejecter))
                {
                    // 眠啊救登绰 捞蜡甫 钎矫窍磊
                }
            }
            // 芭何磊 府胶飘 霖厚啊 登菌澜邓
            FIsRejectListSendAble = true;
            // 努扼捞攫飘啊 芭何磊 府胶飘甫 盔窃
            if (FWantRejectListFlag)
            {
                OnMsgRejectList(userinfo);
                FWantRejectListFlag = false;
            }
        }
        
        public void OnCmdDBRNotReadCount(TCmdMsg Cmd)
        {
            var notreadcountstr = Cmd.body;
            var userinfo = Cmd.pInfo;
            OnCmdSMNotReadCount(userinfo, HUtil32.Str_ToInt(notreadcountstr, 0));
        }
        
        public void OnCmdDBRResult(TCmdMsg Cmd)
        {
            string ErrCode = String.Empty;
            this.ErrMsg("CmdDBRResult[Tag] :" + Cmd.body);
            string CmdNum  =  HUtil32.GetValidStr3(Cmd.body, ref ErrCode, new string[] { "/" });
            switch (HUtil32.Str_ToInt(CmdNum, 0))
            {
                case Grobal2.DB_TAG_ADD:
                    break;
                case Grobal2.DB_TAG_DELETE:
                    break;
                case Grobal2.DB_TAG_DELETEALL:
                    break;
                case Grobal2.DB_TAG_LIST:
                    break;
                case Grobal2.DB_TAG_SETINFO:
                    break;
                case Grobal2.DB_TAG_REJECT_ADD:
                    break;
                case Grobal2.DB_TAG_REJECT_DELETE:
                    break;
                case Grobal2.DB_TAG_REJECT_LIST:
                    break;
                case Grobal2.DB_TAG_NOTREADCOUNT:
                    break;
            }
        }

        public void OnCmdDBAdd(TUserInfo UserInfo, string Reciever, string SendDate, int State, string SendMsg)
        {
            string str;
            str = State.ToString() + ":" + SendDate + ":" + Reciever + ":" + SendMsg + "/";
            svMain.UserMgrEngine.InterSendMsg(TSendTarget.stDbServer, svMain.ServerIndex, 0, 0, 0, UserInfo.UserName, 0, Grobal2.DB_TAG_ADD, 0, 0, 0, str);
        }

        public void OnCmdDBInfo(TUserInfo UserInfo, string SendDate, int State)
        {
            string str;
            str = State.ToString() + ":" + SendDate + "/";
            svMain.UserMgrEngine.InterSendMsg(TSendTarget.stDbServer, svMain.ServerIndex, 0, 0, 0, UserInfo.UserName, 0, Grobal2.DB_TAG_SETINFO, 0, 0, 0, str);
        }
        
        public void OnCmdDBDelete(TUserInfo UserInfo, string SendDate)
        {
            string str;
            str = SendDate + "/";
            svMain.UserMgrEngine.InterSendMsg(TSendTarget.stDbServer, svMain.ServerIndex, 0, 0, 0, UserInfo.UserName, 0, Grobal2.DB_TAG_DELETE, 0, 0, 0, str);
        }
        
        public void OnCmdDBDeleteAll(TUserInfo UserInfo)
        {
            svMain.UserMgrEngine.InterSendMsg(TSendTarget.stDbServer, svMain.ServerIndex, 0, 0, 0, UserInfo.UserName, 0, Grobal2.DB_TAG_DELETEALL, 0, 0, 0, "");
        }
        
        public void OnCmdDBList(TUserInfo UserInfo)
        {
            svMain.UserMgrEngine.InterSendMsg(TSendTarget.stDbServer, svMain.ServerIndex, 0, 0, 0, UserInfo.UserName, 0, Grobal2.DB_TAG_LIST, 0, 0, 0, "");
        }
        
        public void OnCmdDBRejectAdd(TUserInfo UserInfo, string Rejecter)
        {
            string str;
            str = Rejecter + "/";
            svMain.UserMgrEngine.InterSendMsg(TSendTarget.stDbServer, svMain.ServerIndex, 0, 0, 0, UserInfo.UserName, 0, Grobal2.DB_TAG_REJECT_ADD, 0, 0, 0, str);
        }

        // DB俊 芭何磊 昏力
        // ------------------------------------------------------------------------------
        // DB_TAG_REJECT_DELETE   : DB俊 芭何磊 府胶飘 昏力
        // Param    : 芭何磊疙
        // ------------------------------------------------------------------------------
        public void OnCmdDBRejectDelete(TUserInfo UserInfo, string Rejecter)
        {
            string str;
            str = Rejecter + "/";
            svMain.UserMgrEngine.InterSendMsg(TSendTarget.stDbServer, svMain.ServerIndex, 0, 0, 0, UserInfo.UserName, 0, Grobal2.DB_TAG_REJECT_DELETE, 0, 0, 0, str);
        }

        // DB俊 芭何磊 府胶飘 夸没
        // ------------------------------------------------------------------------------
        // DB_TAG_REJECT_LIST   : DB俊 芭何磊 府胶飘 傈价 夸没
        // Param    : 绝澜
        // ------------------------------------------------------------------------------
        public void OnCmdDBRejectList(TUserInfo UserInfo)
        {
            svMain.UserMgrEngine.InterSendMsg(TSendTarget.stDbServer, svMain.ServerIndex, 0, 0, 0, UserInfo.UserName, 0, Grobal2.DB_TAG_REJECT_LIST, 0, 0, 0, "");
        }

        // 佬瘤臼篮 率瘤俺荐 夸没
        // ------------------------------------------------------------------------------
        // DB_TAG_NOTREDCOUNT   : DB俊 佬瘤臼篮 皋技瘤 荐磊 夸没
        // Param    : 绝澜
        // ------------------------------------------------------------------------------
        public void OnCmdDBNotReadCount(TUserInfo UserInfo)
        {
            svMain.UserMgrEngine.InterSendMsg(TSendTarget.stDbServer, svMain.ServerIndex, 0, 0, 0, UserInfo.UserName, 0, Grobal2.DB_TAG_NOTREADCOUNT, 0, 0, 0, "");
        }

    } // end TTagMgr

}

namespace GameSvr
{
    public class TagSystem
    {
        // ElHashList ,
        public const int MAX_TAG_COUNT = 30;
        // 弥措 率瘤 俺荐
        public const int MAX_TAG_PAGE_COUNT = 10;
        // 其捞瘤寸 率瘤 俺荐
        public const int MAX_REJECTER_COUNT = 20;
    } // end TagSystem

}

