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
            if (GetTagCount() < Units.TagSystem.MAX_TAG_COUNT)
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
            if ((Name != "") && (false == FindRejecter(Name, Str)) && (GetRejecterCount() < Units.TagSystem.MAX_REJECTER_COUNT))
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
                startnum = (PageNum - 1) * Units.TagSystem.MAX_TAG_PAGE_COUNT;
                endnum = startnum + Units.TagSystem.MAX_TAG_PAGE_COUNT;
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

        // 努扼捞攫飘俊辑 坷绰 疙飞绢甸 ........................................
        // //////////////////////////////////////////////////////////////////////////////
        // 努扼捞攫飘俊辑 坷绰 疙飞绢甸
        // //////////////////////////////////////////////////////////////////////////////
        // ------------------------------------------------------------------------------
        // CM_TAG_ADD : 率瘤 眠啊
        // 荐脚磊 / 率瘤郴侩
        // ------------------------------------------------------------------------------
        public void OnCmdCMAdd(TCmdMsg Cmd)
        {
            string reciever;
            string tagmsg;
            string senddate;
            TUserInfo recieverinfo;
            // 疙妨绢 盒籍
            tagmsg = GetValidStr3(Cmd.body, reciever, new string[] { "/" });
            senddate = GenerateSendDate();
            // 立加秦 乐栏搁 立加磊俊霸 舅妨霖促.
            if (svMain.UserMgrEngine.InterGetUserInfo(reciever, ref recieverinfo))
            {
                // 郴寇何 辑滚肺 傈价
                OnCmdOSMSend(reciever, recieverinfo.ConnState - Grobal2.CONNSTATE_CONNECT_0, Cmd.UserName, senddate, 0, tagmsg);
            }
            // DB 俊 历厘窍磊
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
            // 疙飞绢 盒籍
            tagmsg = GetValidStr3(Cmd.body, receiver, new string[] { "/" });
            tagmsg = GetValidStr3(tagmsg, receiver2, new string[] { "/" });
            // 焊郴绰 矫埃 扁废.
            senddate = GenerateSendDate();
            // ///////////////////////////////
            // 霉锅掳 荐脚磊俊霸 傈价.
            // 焊郴绰 荤恩苞 罐绰 荤恩捞 鞍栏搁 焊郴瘤 臼澜.(sonmg : 2004/05/03)
            if (receiver != Cmd.UserName)
            {
                // 立加秦 乐栏搁 立加磊俊霸 舅妨霖促.
                if (svMain.UserMgrEngine.InterGetUserInfo(receiver, ref receiverinfo))
                {
                    // 郴寇何 辑滚肺 傈价
                    OnCmdOSMSend(receiver, receiverinfo.ConnState - Grobal2.CONNSTATE_CONNECT_0, Cmd.UserName, senddate, 0, tagmsg);
                }
                // 率瘤 傈价 皋矫瘤 免仿
                // 捞芭 捞犯纳 静搁 动户牢单.. 唱吝俊 绊摹磊 静饭靛 窃何肺 静绊 乐澜
                // TagLock.Enter();
                // try
                // 
                // hum := UserEngine.GetUserHuman( Cmd.UserName );
                // if hum <> nil then begin
                // hum.SysMsg(receiver + '丛俊霸 率瘤甫 傈价沁嚼聪促.', 0);
                // end;
                // 
                // finally
                // TagLock.Leave();
                // end;
                // DB 俊 历厘窍磊
                OnCmdDBAdd(Cmd.pInfo, receiver, senddate, 0, tagmsg);
            }
            // ///////////////////////////////
            // 滴锅掳 荐脚磊俊霸 傈价.
            if (receiver2 != Cmd.UserName)
            {
                if (receiver2 == "---")
                {
                    return;
                }
                // 立加秦 乐栏搁 立加磊俊霸 舅妨霖促.
                if (svMain.UserMgrEngine.InterGetUserInfo(receiver2, ref receiverinfo2))
                {
                    // 郴寇何 辑滚肺 傈价
                    OnCmdOSMSend(receiver2, receiverinfo2.ConnState - Grobal2.CONNSTATE_CONNECT_0, Cmd.UserName, senddate, 0, tagmsg);
                }
                // 率瘤 傈价 皋矫瘤 免仿
                // 捞芭 捞犯纳 静搁 动户牢单.. 唱吝俊 绊摹磊 静饭靛 窃何肺 静绊 乐澜
                // TagLock.Enter();
                // try
                // 
                // hum := UserEngine.GetUserHuman( Cmd.UserName );
                // if hum <> nil then begin
                // hum.SysMsg(receiver2 + '丛俊霸 率瘤甫 傈价沁嚼聪促.', 0);
                // end;
                // 
                // finally
                // TagLock.Leave();
                // end;
                // DB 俊 历厘窍磊
                OnCmdDBAdd(Cmd.pInfo, receiver2, senddate, 0, tagmsg);
            }
        }

        // sonmg
        // ------------------------------------------------------------------------------
        // CM_TAG_DELETE    : 率瘤 昏力
        // Param    : 率瘤朝楼
        // ------------------------------------------------------------------------------
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
            svMain.UserMgrEngine.InterSendMsg(TSendTarget.stClient, 0, UserInfo.GateIdx, UserInfo.UserGateIdx, UserInfo.UserHandle, UserInfo.UserName, UserInfo.Recog, Grobal2.SM_TAG_LIST, PageNum, ListCount, 0, str);
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
            svMain.UserMgrEngine.InterSendMsg(TSendTarget.stClient, 0, UserInfo.GateIdx, UserInfo.UserGateIdx, UserInfo.UserHandle, UserInfo.UserName, UserInfo.Recog, Grobal2.SM_TAG_INFO, State, 0, 0, str);
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

        // 率瘤 昏力 傈价
        // ------------------------------------------------------------------------------
        // 率瘤绰 角力肺 皋葛府俊辑 昏力凳栏肺父 钎矫登绊 DB俊绰 角力肺 昏力矫糯
        // 蝶扼辑 唱吝俊 蜡历啊 犁 立加窍搁 荤扼瘤霸凳
        // +----------------------------------------------------------------------------
        // SM_TAG_DELETE    : 率瘤 昏力 沥焊 傈价
        // Param    : 率瘤朝楼 , 惑怕沥焊 ( 昏力凳 栏肺 函版 )
        // ------------------------------------------------------------------------------
        public void OnCmdSMDelete(TUserInfo UserInfo, string SendDate, int State)
        {
            string str;
            // 惑怕: 朝楼:傈价磊:"郴侩"
            str = SendDate;
            // pagenum = 0 , sendnum = 1;
            svMain.UserMgrEngine.InterSendMsg(TSendTarget.stClient, 0, UserInfo.GateIdx, UserInfo.UserGateIdx, UserInfo.UserHandle, UserInfo.UserName, UserInfo.Recog, Grobal2.SM_TAG_INFO, State, 0, 0, str);
        }

        // 芭何磊 府胶飘 傈价
        // ------------------------------------------------------------------------------
        // SM_TAG_REJECT_LIST   : 芭何磊 府胶飘 傈价
        // Param    : 芭何磊 俺荐 , 芭何磊 府胶飘
        // ------------------------------------------------------------------------------
        public void OnCmdSMRejectList(TUserInfo UserInfo, int ListCount, string RejectList)
        {
            string str;
            str = RejectList;
            svMain.UserMgrEngine.InterSendMsg(TSendTarget.stClient, 0, UserInfo.GateIdx, UserInfo.UserGateIdx, UserInfo.UserHandle, UserInfo.UserName, UserInfo.Recog, Grobal2.SM_TAG_REJECT_LIST, ListCount, 0, 0, str);
        }

        // 芭何磊 眠啊 傈价
        // ------------------------------------------------------------------------------
        // SM_TAG_REJECT_ADD   : 芭何磊 眠啊 傈价
        // Param    : 芭何磊疙
        // ------------------------------------------------------------------------------
        public void OnCmdSMRejectAdd(TUserInfo UserInfo, string Rejecter)
        {
            string str;
            str = Rejecter;
            svMain.UserMgrEngine.InterSendMsg(TSendTarget.stClient, 0, UserInfo.GateIdx, UserInfo.UserGateIdx, UserInfo.UserHandle, UserInfo.UserName, UserInfo.Recog, Grobal2.SM_TAG_REJECT_ADD, 0, 0, 0, str);
        }

        // 芭何磊 昏力 傈价
        // ------------------------------------------------------------------------------
        // SM_TAG_REJECT_DELETE   : 芭何磊 昏力 傈价
        // Param    : 芭何磊疙
        // ------------------------------------------------------------------------------
        public void OnCmdSMRejectDelete(TUserInfo UserInfo, string Rejecter)
        {
            string str;
            str = Rejecter;
            svMain.UserMgrEngine.InterSendMsg(TSendTarget.stClient, 0, UserInfo.GateIdx, UserInfo.UserGateIdx, UserInfo.UserHandle, UserInfo.UserName, UserInfo.Recog, Grobal2.SM_TAG_REJECT_DELETE, 0, 0, 0, str);
        }

        // 率瘤 佬瘤 臼篮 俺荐 傈价
        // ------------------------------------------------------------------------------
        // SM_TAG_NOTREADCOUNT   : 佬瘤臼篮 率瘤 俺荐 傈价
        // Param    : 佬瘤臼篮 率瘤 俺荐
        // ------------------------------------------------------------------------------
        public void OnCmdSMNotReadCount(TUserInfo UserInfo, int NotReadCount)
        {
            svMain.UserMgrEngine.InterSendMsg(TSendTarget.stClient, 0, UserInfo.GateIdx, UserInfo.UserGateIdx, UserInfo.UserHandle, UserInfo.UserName, UserInfo.Recog, Grobal2.SM_TAG_ALARM, NotReadCount, 0, 0, "");
        }

        // 努扼攫飘 疙飞绢俊 措茄 搬苞蔼
        // ------------------------------------------------------------------------------
        // SM_TAG_RESULT   : 努扼捞攫飘 疙飞绢俊 措茄 搬苞蔼
        // Param    : 傈价等 疙飞绢 , 搬苞蔼
        // ------------------------------------------------------------------------------
        public void OnCmdSMResult(TUserInfo UserInfo, short CmdNum, short ResultValue)
        {
            svMain.UserMgrEngine.InterSendMsg(TSendTarget.stClient, 0, UserInfo.GateIdx, UserInfo.UserGateIdx, UserInfo.UserHandle, UserInfo.UserName, UserInfo.Recog, Grobal2.SM_TAG_RESULT, CmdNum, ResultValue, 0, "");
        }

        // 辑滚埃俊 傈价罐绰 疙飞绢甸 ..........................................
        // 辑滚啊 率瘤 傈价罐澜
        // //////////////////////////////////////////////////////////////////////////////
        // 辑滚埃俊 傈价罐绰 疙飞绢甸
        // //////////////////////////////////////////////////////////////////////////////
        // ------------------------------------------------------------------------------
        // ISM_TAG_SEND   : 辑滚埃俊 率瘤 傈价罐澜
        // Param    : 率瘤惑怕 , 朝楼 , 傈价磊 , 郴侩
        // ------------------------------------------------------------------------------
        public void OnCmdISMSend(TCmdMsg Cmd)
        {
            string sender;
            string senddate;
            string statestr;
            int state;
            string sendmsg;
            string tempstr;
            TUserInfo userinfo;
            // 惑怕:朝楼:傈价茄纳腐疙:"郴侩"
            tempstr = GetValidStr3(Cmd.body, statestr, new string[] { ":" });
            tempstr = GetValidStr3(tempstr, senddate, new string[] { ":" });
            sendmsg = GetValidStr3(tempstr, sender, new string[] { ":" });
            userinfo = Cmd.pInfo;
            state = HUtil32.Str_ToInt(statestr, 0);
            // 芭何磊啊 酒聪搁
            if (!IsRejecter(sender))
            {
                // 率瘤甫 眠啊茄促.
                if (Add(userinfo, sender, senddate, state, sendmsg))
                {
                    // 农扼捞攫飘啊 府胶飘甫 傈价罐疽促搁 率瘤沥焊 傈价
                    if (FClientGetList)
                    {
                        OnCmdSMAdd(userinfo, sender, senddate, state, sendmsg);
                    }
                    // 率瘤吭澜 舅覆 傈价
                    OnCmdSMNotReadCount(userinfo, FNotReadCount);
                }
            }
        }

        // 辑滚扒 疙飞俊 措茄 搬苞蔼罐澜
        // ------------------------------------------------------------------------------
        // ISM_TAG_RESULT   : 辑滚埃俊 搬苞 傈价 罐澜
        // Param    : 傈价疙飞绢 , 搬苞蔼
        // ------------------------------------------------------------------------------
        public void OnCmdISMResult(TCmdMsg Cmd)
        {
        }

        // 辑滚埃俊 傈价窍绰 疙飞绢甸 ..........................................
        // 辑滚埃俊 率扁 傈价
        // //////////////////////////////////////////////////////////////////////////////
        // 辑滚埃俊 傈价窍绰 疙飞绢甸
        // //////////////////////////////////////////////////////////////////////////////
        // ------------------------------------------------------------------------------
        // ISM_TAG_SEND   : 辑滚埃俊 率瘤 傈价窃
        // Param    : 率瘤惑怕 , 朝楼 , 傈价磊 , 郴侩
        // ------------------------------------------------------------------------------
        public void OnCmdOSMSend(string UserName, int SvrIndex, string Sender, string SendDate, int State, string SendMsg)
        {
            string str;
            // 惑怕:朝楼:傈价茄纳腐疙:"郴侩"
            str = State.ToString() + ":" + SendDate + ":" + Sender + ":" + SendMsg;
            svMain.UserMgrEngine.InterSendMsg(TSendTarget.stOtherServer, 0, 0, 0, 0, UserName, 0, Grobal2.ISM_TAG_SEND, (ushort)SvrIndex, 0, 0, str);
        }

        // 辑滚埃 疙飞俊 措茄 搬侞 傈价
        // ------------------------------------------------------------------------------
        // ISM_TAG_RESULT   : 辑滚埃俊 搬苞 傈价窃
        // Param    : 傈价疙飞绢 , 搬苞蔼
        // ------------------------------------------------------------------------------
        public void OnCmdOSMResult(string UserName, int SvrIndex, short CmdNum, short ResultValue)
        {
            string str;
            str = CmdNum.ToString() + ":" + ResultValue.ToString();
            svMain.UserMgrEngine.InterSendMsg(TSendTarget.stOtherServer, 0, 0, 0, 0, UserName, 0, Grobal2.ISM_TAG_RESULT, (ushort)SvrIndex, 0, 0, str);
        }

        // DB 肺何磐 坷绰 疙飞绢甸 .............................................
        // 率瘤 府胶飘 罐澜
        // //////////////////////////////////////////////////////////////////////////////
        // DB 肺何磐 坷绰 疙飞绢甸
        // //////////////////////////////////////////////////////////////////////////////
        // ------------------------------------------------------------------------------
        // DBR_TAG_LIST   : DB肺何磐 率瘤 府胶飘 罐澜
        // Param    : 俺荐 , 府胶飘
        // ------------------------------------------------------------------------------
        public void OnCmdDBRList(TCmdMsg Cmd)
        {
            string tagcountstr;
            string sender;
            string senddate;
            string statestr;
            string sendmsg;
            string tempstr;
            string tagstr;
            int tagcount;
            int i;
            TUserInfo userinfo;
            tempstr = GetValidStr3(Cmd.body, tagcountstr, new string[] { "/" });
            tagcount = HUtil32.Str_ToInt(tagcountstr, 0);
            userinfo = Cmd.pInfo;
            // 啊瘤绊乐绰 葛电 府胶飘撇 瘤款促.
            RemoveAll();
            for (i = 0; i < tagcount; i++)
            {
                // 率瘤俊 包访等逞阑 啊廉柯促.
                tempstr = GetValidStr3(tempstr, tagstr, new string[] { "/" });
                // 率瘤 牢磊甫 盒府茄促.
                tagstr = GetValidStr3(tagstr, statestr, new string[] { ":" });
                tagstr = GetValidStr3(tagstr, senddate, new string[] { ":" });
                sendmsg = GetValidStr3(tagstr, sender, new string[] { ":" });
                // 率瘤 眠啊
                if (!Add(userinfo, sender, senddate, HUtil32.Str_ToInt(statestr, 0), sendmsg))
                {
                    // 眠啊救登绰 捞蜡甫 钎矫窍磊
                    // MainOutMessage('Tag didn''t Added...');
                }
            }
            // 府胶飘 霖厚啊 登菌促
            FIsTagListSendAble = true;
            // 率瘤吭澜 舅覆 傈价 2003-08-21 : 皋牢俊辑 舅妨霖促.. 荐沥凳
            // OnCmdSMNotReadCount( userinfo , FNotReadCount );
            // 努扼捞攫飘啊 府胶飘甫 盔窃 林磊
            if (FWantTagListFlag)
            {
                // 努扼捞攫飘俊霸 府胶撇 焊辰饶
                OnMsgList(userinfo, FWantTagListPage);
                // 努扼捞攫飘俊霸 焊陈澜阑 悸泼
                FWantTagListFlag = false;
            }
        }

        // 芭何磊 府胶飘 罐澜
        // ------------------------------------------------------------------------------
        // DBR_TAG_REJECT_LIST   : DB肺何磐 芭何磊 府胶飘 罐澜
        // Param    : 俺荐 , 府胶飘
        // ------------------------------------------------------------------------------
        public void OnCmdDBRRejectList(TCmdMsg Cmd)
        {
            string tempstr;
            string rejecter;
            string rejectcountstr;
            int rejectcount;
            int i;
            TUserInfo userinfo;
            tempstr = GetValidStr3(Cmd.body, rejectcountstr, new string[] { "/" });
            rejectcount = HUtil32.Str_ToInt(rejectcountstr, 0);
            userinfo = Cmd.pInfo;
            for (i = 0; i < rejectcount; i++)
            {
                // 率瘤俊 包访等逞阑 啊廉柯促.
                tempstr = GetValidStr3(tempstr, rejecter, new string[] { "/" });
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

        // 佬瘤臼篮 率瘤 俺荐 罐澜
        // ------------------------------------------------------------------------------
        // DBR_TAG_NOTREADCOUNT   : DB肺何磐 佬瘤臼篮 率瘤 俺荐 罐澜
        // Param    : 俺荐
        // ------------------------------------------------------------------------------
        public void OnCmdDBRNotReadCount(TCmdMsg Cmd)
        {
            string notreadcountstr;
            TUserInfo userinfo;
            notreadcountstr = Cmd.body;
            userinfo = Cmd.pInfo;
            OnCmdSMNotReadCount(userinfo, HUtil32.Str_ToInt(notreadcountstr, 0));
        }

        // 搬苞蔼 罐澜
        // ------------------------------------------------------------------------------
        // DBR_TAG_RESULT   : DB肺何磐 搬苞蔼 罐澜
        // Param    : 傈价茄 疙飞绢 搬苞蔼
        // ------------------------------------------------------------------------------
        public void OnCmdDBRResult(TCmdMsg Cmd)
        {
            string CmdNum;
            string ErrCode;
            // TO TEST:
            this.ErrMsg("CmdDBRResult[Tag] :" + Cmd.body);
            CmdNum = GetValidStr3(Cmd.body, ErrCode, new string[] { "/" });
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

        // DB 肺 焊郴绰 疙飞绢甸 ................................................
        // DB 俊 率瘤 眠啊
        // //////////////////////////////////////////////////////////////////////////////
        // DB 肺 焊郴绰 疙飞绢甸
        // //////////////////////////////////////////////////////////////////////////////
        // ------------------------------------------------------------------------------
        // DB_TAG_ADD   : DB俊 率瘤 眠啊 傈价
        // Param    : 惑怕 , 朝楼 , 荐脚磊 , 郴侩
        // ------------------------------------------------------------------------------
        public void OnCmdDBAdd(TUserInfo UserInfo, string Reciever, string SendDate, int State, string SendMsg)
        {
            string str;
            str = State.ToString() + ":" + SendDate + ":" + Reciever + ":" + SendMsg + "/";
            svMain.UserMgrEngine.InterSendMsg(TSendTarget.stDbServer, svMain.ServerIndex, 0, 0, 0, UserInfo.UserName, 0, Grobal2.DB_TAG_ADD, 0, 0, 0, str);
        }

        // DB 俊 率瘤 惑怕 函版
        // ------------------------------------------------------------------------------
        // DB_TAG_INFO   : DB俊 率瘤 惑怕 函版 傈价
        // Param    : 朝楼 , 惑怕
        // ------------------------------------------------------------------------------
        public void OnCmdDBInfo(TUserInfo UserInfo, string SendDate, int State)
        {
            string str;
            str = State.ToString() + ":" + SendDate + "/";
            svMain.UserMgrEngine.InterSendMsg(TSendTarget.stDbServer, svMain.ServerIndex, 0, 0, 0, UserInfo.UserName, 0, Grobal2.DB_TAG_SETINFO, 0, 0, 0, str);
        }

        // DB俊  率瘤 昏力
        // ------------------------------------------------------------------------------
        // DB_TAG_DELETE   : DB俊 率瘤 昏力 傈价
        // Param    : 朝楼
        // ------------------------------------------------------------------------------
        public void OnCmdDBDelete(TUserInfo UserInfo, string SendDate)
        {
            string str;
            str = SendDate + "/";
            svMain.UserMgrEngine.InterSendMsg(TSendTarget.stDbServer, svMain.ServerIndex, 0, 0, 0, UserInfo.UserName, 0, Grobal2.DB_TAG_DELETE, 0, 0, 0, str);
        }

        // DB俊 佬篮 率瘤 傈何 昏力
        // ------------------------------------------------------------------------------
        // DB_TAG_DELETEALL   : DB俊 率瘤 傈何昏力 傈价 ( 佬瘤臼篮巴苞 昏力陛瘤等巴篮 力寇)
        // Param    : 绝澜
        // ------------------------------------------------------------------------------
        public void OnCmdDBDeleteAll(TUserInfo UserInfo)
        {
            svMain.UserMgrEngine.InterSendMsg(TSendTarget.stDbServer, svMain.ServerIndex, 0, 0, 0, UserInfo.UserName, 0, Grobal2.DB_TAG_DELETEALL, 0, 0, 0, "");
        }

        // DB俊 率瘤 府胶飘 夸没
        // ------------------------------------------------------------------------------
        // DB_TAG_LIST   : DB俊 率瘤府胶飘 夸没
        // Param    : 绝澜
        // ------------------------------------------------------------------------------
        public void OnCmdDBList(TUserInfo UserInfo)
        {
            svMain.UserMgrEngine.InterSendMsg(TSendTarget.stDbServer, svMain.ServerIndex, 0, 0, 0, UserInfo.UserName, 0, Grobal2.DB_TAG_LIST, 0, 0, 0, "");
        }

        // DB俊 芭何磊 眠啊
        // ------------------------------------------------------------------------------
        // DB_TAG_REJECT_ADD   : DB俊 芭何磊 府胶飘 眠啊
        // Param    : 芭何磊疙
        // ------------------------------------------------------------------------------
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

