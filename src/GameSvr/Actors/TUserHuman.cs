using System;
using System.Collections;
using SystemModule;

namespace GameSvr
{
    public class TUserHuman : TAnimal
    {
        public TIconInfo[] m_IconInfo;
        private TDefaultMessage Def = null;
        private readonly ArrayList SendBuffers = null;
        private string LatestSayStr = String.Empty;
        // 档硅 陛瘤
        private int BombSayCount = 0;
        private long BombSayTime = 0;
        private bool BoShutUpMouse = false;
        private long ShutUpMouseTime = 0;
        private long operatetime = 0;
        private long operatetime_30sec = 0;
        private long operatetime_sec = 0;
        private long operatetime_500m = 0;
        private bool boGuildwarsafezone = false;
        private long NpcClickTime = 0;
        private readonly long FirstClientTime = 0;
        private readonly long FirstServerTime = 0;
        // 困殴惑痢包访 单捞磐.
        private readonly TMarketItemManager FUserMarket = null;
        private TCreature FMarketNpc = null;
        private bool BoFlagUserMarket = false;
        // 困殴 扁瓷阑 茄波锅俊 咯矾 锅 夸没 且 荐 绝档废 Flag汲沥(sonmg 2005/01/31)
        private bool FlagReadyToSellCheck = false;
        // 困殴 啊瓷茄瘤 眉农沁阑 锭 Flag汲沥(sonmg 2005/08/10)
        public readonly TStallMgr StallMgr = null;
        private long m_dwBuyShopItemTick = 0;
        private readonly byte[] m_btGetShopItem;
        public string UserId;
        public string UserAddress = String.Empty;
        public int UserHandle = 0;
        public ushort UserGateIndex = 0;
        public long LastSocTime = 0;
        public int GateIndex = 0;
        public int ClientVersion = 0;
        public int LoginClientVersion = 0;
        public int ClientCheckSum = 0;
        public DateTime LoginDateTime;
        public long LoginTime = 0;
        public long ServerShiftTime = 0;
        public bool ReadyRun = false;
        public int Certification = 0;
        public int ApprovalMode = 0;
        public int AvailableMode = 0;
        public long UserConnectTime = 0;
        public int ChangeToServerNumber = 0;
        public bool EmergencyClose = false;
        public bool UserSocketClosed = false;
        public bool UserRequestClose = false;
        public bool SoftClosed = false;
        public bool BoSaveOk = false;
        public ArrayList PrevServerSlaves = null;
        public string TempStr = String.Empty;
        public bool BoChangeServerOK = false;
        public bool BoChangeServer = false;
        public int WriteChangeServerInfoCount = 0;
        public string ChangeMapName = String.Empty;
        public int ChangeCX = 0;
        public int ChangeCY = 0;
        public bool BoChangeServerNeedDelay = false;
        public long ChangeServerDelayTime = 0;
        public long HumStruckTime = 0;
        public int ClientMsgCount = 0;
        public int ClientSpeedHackDetect = 0;
        public long LatestSpellTime = 0;
        public int LatestSpellDelay = 0;
        public long LatestHitTime = 0;
        public long LatestWalkTime = 0;
        public long LatestDropTime = 0;
        // 付瘤阜栏肺 酒捞袍 肚绰 陛傈阑 冻焙 矫埃.(辑滚 发 酒捞袍 汗荤 包访 sonmg)
        public int HitTimeOverCount = 0;
        public int HitTimeOverSum = 0;
        public int SpellTimeOverCount = 0;
        public int WalkTimeOverCount = 0;
        public int WalkTimeOverSum = 0;
        public int SpeedHackTimerOverCount = 0;
        public bool MustRandomMove = false;
        // 犁立且锭 甘俊辑 酒公镑俊辑唱 冻绢瘤霸..
        public object CurQuest = null;
        // PTQuestRecord;
        public TCreature CurQuestNpc = null;
        public object CurSayProc = null;
        public int[] QuestParams;
        public int[] DiceParams;
        public bool BoTimeRecall = false;
        // 矫埃捞 登搁 盔困摹肺 倒酒坷咳
        public bool BoTimeRecallGroup = false;
        // 矫埃捞 登搁 弊缝 傈眉啊 盔困摹肺 倒酒咳
        public long TimeRecallEnd = 0;
        public string TimeRecallMap = String.Empty;
        public int TimeRecallX = 0;
        public int TimeRecallY = 0;
        public byte PriviousCheckCode = 0;
        public int CrackWanrningLevel = 0;
        public long LastSaveTime = 0;
        public int Bright = 0;
        // 甘狼 灌扁,
        public bool FirstTimeConnection = false;
        // 某腐磐甫 贸澜 父甸绢辑 立加窃,
        public bool BoSendNotice = false;
        public bool LoginSign = false;
        public bool BoServerShifted = false;
        public bool BoAccountExpired = false;
        public long LineNoticeTime = 0;
        public int LineNoticeNumber = 0;
        public int NotReadTag = 0;
        public TRelationShipMgr fLover = null;
        public long FExpireTime = 0;
        public int FExpireCount = 0;
        public int SecondsCard = 0;
        public bool m_boStartMarry = false;
        public bool m_boStartUnMarry = false;
        public TCreature m_PoseBaseObject = null;
        public bool m_boStartMaster = false;
        public bool m_boStartUnMaster = false;
        public TMasterRanking[] m_MasterRanking;

        public TUserHuman() : base()
        {
            this.RaceServer = Grobal2.RC_USERHUMAN;
            EmergencyClose = false;
            BoChangeServer = false;
            SoftClosed = false;
            UserRequestClose = false;
            UserSocketClosed = false;
            ReadyRun = false;
            // 肺爹,檬扁拳,.. 肯丰登搁 ReadyRun篮 TRUE啊 等促.
            PriviousCheckCode = 0;
            CrackWanrningLevel = 0;
            // 菩哦 duplication鞍篮 厘抄阑 摹绰瘤 咯何..
            // 2003-08-08 :PDS
            // 荤恩捞 隔副锭 措厚 历厘矫埃阑 5盒埃拜栏肺 罚待 炼沥茄促.
            // 贸澜立加茄 荤恩篮 15盒鳖瘤 历厘鸥烙捞 疵绢朝荐 乐促. 弊饶俊绰 10盒俊 茄锅究 历厘
            LastSaveTime = GetTickCount + new System.Random(5 * 60 * 1000).Next();
            this.WantRefMsg = true;
            BoSaveOk = false;
            MustRandomMove = false;
            CurQuest = null;
            CurSayProc = null;
            BoTimeRecall = false;
            BoTimeRecallGroup = false;
            TimeRecallMap = "";
            TimeRecallX = 0;
            TimeRecallY = 0;
            this.RunTime = (int)GetCurrentTime;
            this.RunNextTick = 250;
            this.SearchRate = 1000;
            this.SearchTime  =  HUtil32.GetTickCount();
            this.ViewRange = 12;
            FirstTimeConnection = false;
            LoginSign = false;
            BoServerShifted = false;
            BoAccountExpired = false;
            BoSendNotice = false;
            operatetime  =  HUtil32.GetTickCount();
            operatetime_sec  =  HUtil32.GetTickCount();
            operatetime_500m  =  HUtil32.GetTickCount();
            boGuildwarsafezone = false;
            NpcClickTime  =  HUtil32.GetTickCount();
            ClientMsgCount = 0;
            ClientSpeedHackDetect = 0;
            LatestSpellTime  =  HUtil32.GetTickCount();
            LatestSpellDelay = 0;
            LatestHitTime  =  HUtil32.GetTickCount();
            LatestWalkTime  =  HUtil32.GetTickCount();
            LatestDropTime  =  HUtil32.GetTickCount();
            HitTimeOverCount = 0;
            HitTimeOverSum = 0;
            SpellTimeOverCount = 0;
            WalkTimeOverCount = 0;
            WalkTimeOverSum = 0;
            SpeedHackTimerOverCount = 0;
            SendBuffers = new ArrayList();
            // 2003/06/12 浇饭捞宏 菩摹
            PrevServerSlaves = new ArrayList();
            // 辑滚 捞悼窍搁辑 颗败促聪绰 何窍
            LatestSayStr = "";
            BombSayCount = 0;
            BombSayTime  =  HUtil32.GetTickCount();
            BoShutUpMouse = false;
            ShutUpMouseTime  =  HUtil32.GetTickCount();
            LoginDateTime = DateTime.Now;
            LoginTime  =  HUtil32.GetTickCount();
            ServerShiftTime  =  HUtil32.GetTickCount();
            FirstClientTime = 0;
            FirstServerTime = 0;
            BoChangeServer = false;
            BoChangeServerNeedDelay = false;
            WriteChangeServerInfoCount = 0;
            LineNoticeTime  =  HUtil32.GetTickCount();
            LineNoticeNumber = 0;
            NotReadTag = 0;
            fLover = new TRelationShipMgr();
            FExpireTime = 0;
            FExpireCount = 0;
            FUserMarket = new TMarketItemManager();
            FMarketNpc = null;
            BoFlagUserMarket = false;
            FlagReadyToSellCheck = false;
            StallMgr = new TStallMgr();
            //FillChar(m_btGetShopItem, 6, '\0');
            SecondsCard = 0;
            //FillChar(m_IconInfo[0], sizeof(Grobal2.TIconInfo), '\0');
        }

        public int GetUserMassCount()
        {
            int result;
            result = svMain.UserEngine.GetAreaUserCount(this.PEnvir, this.CX, this.CY, 10);
            return result;
        }

        public void ResetCharForRevival()
        {
            //FillChar(this.StatusArr, sizeof(short) * Grobal2.STATUSARR_SIZE, '\0');
            //FillChar(this.StatusValue, sizeof(byte) * Grobal2.STATUSARR_SIZE, '\0');
        }

        // 磷篮 版快 惑怕 府悸
        public void Clear_5_9_bugitems()
        {
            int i;
            for (i = this.ItemList.Count - 1; i >= 0; i--)
            {
                if (this.ItemList[i].Index >= 164)
                {
                    Dispose(this.ItemList[i]);
                    this.ItemList.RemoveAt(i);
                }
            }
            for (i = this.SaveItems.Count - 1; i >= 0; i--)
            {
                if (((TUserItem)this.SaveItems[i]).Index >= 164)
                {
                    Dispose((TUserItem)this.SaveItems[i]);
                    this.SaveItems.RemoveAt(i);
                }
            }
        }

        public void Reset_6_28_bugitems()
        {
            int i;
            TStdItem ps;
            for (i = 0; i <= 12; i++)
            {
                if (this.UseItems[i].DuraMax == 0)
                {
                    ps = svMain.UserEngine.GetStdItem(this.UseItems[i].Index);
                    if (ps != null)
                    {
                        this.UseItems[i].DuraMax = ps.DuraMax;
                    }
                }
            }
            for (i = this.ItemList.Count - 1; i >= 0; i--)
            {
                if (this.ItemList[i].DuraMax == 0)
                {
                    ps = svMain.UserEngine.GetStdItem(this.ItemList[i].Index);
                    if (ps != null)
                    {
                        this.ItemList[i].DuraMax = ps.DuraMax;
                    }
                }
            }
            for (i = this.SaveItems.Count - 1; i >= 0; i--)
            {
                if (((TUserItem)this.SaveItems[i]).Index >= 164)
                {
                    ps = svMain.UserEngine.GetStdItem(((TUserItem)this.SaveItems[i]).Index);
                    if (ps != null)
                    {
                        ((TUserItem)this.SaveItems[i]).DuraMax = ps.DuraMax;
                    }
                    // Dispose (PTUserItem(SaveItems[i]));
                    // SaveItems.Delete (i);
                }
            }
        }

        public override void Initialize()
        {
            int i;
            int k;
            int sidx;
            string iname;
            TUserItem pi;
            TStdItem ps;
            TSlaveInfo plsave;
            if (svMain.BoTestServer)
            {
                if (this.Abil.Level < svMain.TestLevel)
                {
                    this.Abil.Level = (byte)svMain.TestLevel;
                }
                if (this.Gold < svMain.TestGold)
                {
                    this.Gold = svMain.TestGold;
                }
            }
            if (svMain.BoTestServer || svMain.BoServiceMode)
            {
                ApprovalMode = 3;
            }
            this.MapMoveTime  =  HUtil32.GetTickCount();
            LoginDateTime = DateTime.Now;
            LoginTime  =  HUtil32.GetTickCount();
            ServerShiftTime  =  HUtil32.GetTickCount();
            base.Initialize();
            for (i = this.ItemList.Count - 1; i >= 0; i--)
            {
                if (svMain.UserEngine.GetStdItemName(this.ItemList[i].Index) == "")
                {
                    Dispose(this.ItemList[i]);
                    this.ItemList.RemoveAt(i);
                    continue;
                }
            }
            for (i = this.ItemList.Count - 1; i >= 0; i--)
            {
                ps = svMain.UserEngine.GetStdItem(this.ItemList[i].Index);
                if ((ps != null) && (ps.OverlapItem >= 1) && (this.ItemList[i].Dura == 0))
                {
                    Dispose(this.ItemList[i]);
                    this.ItemList.RemoveAt(i);
                    continue;
                }
            }
            for (i = this.SaveItems.Count - 1; i >= 0; i--)
            {
                ps = svMain.UserEngine.GetStdItem(((TUserItem)this.SaveItems[i]).Index);
                if ((ps != null) && (ps.OverlapItem >= 1) && (((TUserItem)this.SaveItems[i]).Dura == 0))
                {
                    Dispose((TUserItem)this.SaveItems[i]);
                    this.SaveItems.RemoveAt(i);
                    continue;
                }
            }
            for (i = 0; i < Grobal2.STATUSARR_SIZE; i++)
            {
                if (this.StatusArr[i] > 0)
                {
                    this.StatusTimes[i]  =  HUtil32.GetTickCount();
                }
            }
            this.CharStatus = this.GetCharStatus();
            for (i = 0; i <= 12; i++)
            {
                // 8->12
                if (this.UseItems[i].Index > 0)
                {
                    ps = svMain.UserEngine.GetStdItem(this.UseItems[i].Index);
                    if (ps != null)
                    {
                        if (!M2Share.IsTakeOnAvailable(i, ps))
                        {
                            // (抗)漠俊 渴捞 坷瘤 给窍霸 八荤
                            pi = new TUserItem();
                            pi = this.UseItems[i];
                            this.AddItem(pi);
                            this.UseItems[i].Index = 0;
                        }
                    }
                    else
                    {
                        this.UseItems[i].Index = 0;
                    }
                }
            }
            for (i = 0; i < this.ItemList.Count; i++)
            {
                // 捞亥飘 酒捞袍 八荤
                ps = svMain.UserEngine.GetStdItem(this.ItemList[i].Index);
                if (ps != null)
                {
                    if (!BoServerShifted)
                    {
                        // 盖 立加
                        if (ps.StdMode == ObjBase.TAIWANEVENTITEM)
                        {
                            // 措父 捞亥飘 酒捞袍篮 磷栏搁 冻备扁 锭巩俊 立加矫俊 甸 荐 绝促.
                            Dispose(this.ItemList[i]);
                            this.ItemList.RemoveAt(i);
                            continue;
                        }
                    }
                    else
                    {
                        // 辑滚 捞悼栏肺 立加
                        if (ps.StdMode == ObjBase.TAIWANEVENTITEM)
                        {
                            this.TaiwanEventItemName = ps.Name;
                            this.BoTaiwanEventUser = true;
                            this.StatusArr[Grobal2.STATE_BLUECHAR] = 60000;
                            // 鸥烙 酒眶 绝澜;
                            this.Light = this.GetMyLight();
                            this.SendRefMsg(Grobal2.RM_CHANGELIGHT, 0, 0, 0, 0, "");
                            this.CharStatus = this.GetCharStatus();
                        }
                    }
                }
            }
            // 啊规俊 吝汗等 酒捞叼狼 酒捞袍捞 乐绰瘤 八荤.
            // 肋 给 等 酒捞袍, 瘤况廉具 且 酒捞袍 昏力
            for (i = this.ItemList.Count - 1; i >= 0; i--)
            {
                iname = svMain.UserEngine.GetStdItemName(this.ItemList[i].Index);
                sidx = this.ItemList[i].MakeIndex;
                for (k = i - 1; k >= 0; k--)
                {
                    pi = this.ItemList[k];
                    if ((iname == svMain.UserEngine.GetStdItemName(pi.Index)) && (sidx == pi.MakeIndex))
                    {
                        Dispose(pi);
                        this.ItemList.RemoveAt(k);
                        break;
                    }
                }
            }
            this.SendMsg(this, Grobal2.RM_LOGON, 0, 0, 0, 0, "");
#if FOR_ABIL_POINT
            // 4/16老何磐 利侩
            // 焊呈胶 器牢飘啊 乐栏搁
            if (this.BonusPoint > 0)
            {
                this.SendMsg(this, Grobal2.RM_ADJUST_BONUS, 0, 0, 0, 0, "");
            }
#endif
            // 呈公 腹捞 剐笼登绢 乐栏搁 促弗 镑栏肺 捞悼 矫挪促.
            // 2004/04/22 眉氰魄 饭骇 函版
            if (this.Abil.Level <= ObjBase.EXPERIENCELEVEL)
            {
                // 7
                if (GetUserMassCount() >= 80)
                {
                    // 老窜 裹困 荐沥.(sonmg 2004/06/23)
                    // RandomSpaceMove (PEnvir.MapName, 0);
                    this.RandomSpaceMoveInRange(0, 15, 30);
                }
            }
            // 巩颇 措访 捞亥飘 规俊辑 混酒 抄 版快
            if (MustRandomMove)
            {
                this.RandomSpaceMove(this.PEnvir.MapName, 0);
            }
            this.UserDegree = (byte)svMain.UserEngine.GetMyDegree(this.UserName);
            CheckHomePos();
            // 乔纳捞绰 乔纳捞 顶俊辑 矫累
            // 付过 八荤,..  付过狼 漂荐 瓷仿 八荤
            for (i = 0; i < this.MagicList.Count; i++)
            {
                this.CheckMagicSpecialAbility((TUserMagic)this.MagicList[i]);
            }
            // 贸澜 矫累且锭, 格八 1俺 乞刮汗 茄国究, 捣 0傈..
            if (FirstTimeConnection)
            {
                pi = new TUserItem();
                if (svMain.UserEngine.CopyToUserItemFromName(svMain.__Candle, ref pi))
                {
                    this.ItemList.Add(pi);
                }
                else
                {
                    Dispose(pi);
                }
                pi = new TUserItem();
                if (svMain.UserEngine.CopyToUserItemFromName(svMain.__BasicDrug, ref pi))
                {
                    this.ItemList.Add(pi);
                }
                else
                {
                    Dispose(pi);
                }
                pi = new TUserItem();
                if (svMain.UserEngine.CopyToUserItemFromName(svMain.__WoodenSword, ref pi))
                {
                    this.ItemList.Add(pi);
                }
                else
                {
                    Dispose(pi);
                }
                if (this.Sex == 0)
                {
                    // 巢磊;
                    pi = new TUserItem();
                    if (svMain.UserEngine.CopyToUserItemFromName(svMain.__ClothsForMan, ref pi))
                    {
                        this.ItemList.Add(pi);
                    }
                    else
                    {
                        Dispose(pi);
                    }
                }
                else
                {
                    pi = new TUserItem();
                    if (svMain.UserEngine.CopyToUserItemFromName(svMain.__ClothsForWoman, ref pi))
                    {
                        this.ItemList.Add(pi);
                    }
                    else
                    {
                        Dispose(pi);
                    }
                }
            }
            this.RecalcLevelAbilitys();
            this.RecalcAbilitys();
            // 肚 龋免 秦具 窃..
            this.Abil.MaxExp = (int)this.GetNextLevelExp(this.Abil.Level);
            // TO PDS;
            this.WAbil.MaxExp = this.Abil.MaxExp;
            if (this.FreeGulityCount == 0)
            {
                this.PlayerKillingPoint = 0;
                this.FreeGulityCount++;
            }
            // 啊规俊 捣绰 MAXGOLD傈 鳖瘤 甸 荐 乐促.
            if (this.Gold > ObjBase.BAGGOLD * 2)
            {
                this.Gold = ObjBase.BAGGOLD * 2;
            }
            if (!BoServerShifted)
            {
                if ((ClientVersion < Grobal2.VERSION_NUMBER) || (ClientVersion != LoginClientVersion) || ((ClientCheckSum != svMain.ClientCheckSumValue1) && (ClientCheckSum != svMain.ClientCheckSumValue2) && (ClientCheckSum != svMain.ClientCheckSumValue3)))
                {
                    this.SysMsg("客户端版本号错误请重新下载。", 0);
                    this.SysMsg("官方网站(http://www.lom2.com)", 0);
#if !DEBUG
                    if (!svMain.BoClientTest)
                    {
                        this.SysMsg("连接已中断.", 0);
                        EmergencyClose = true;
                        return;
                    }
#endif
                }
                switch (this.HumAttackMode)
                {
                    case Grobal2.HAM_ALL:
                        this.SysMsg("[攻击模式: 全体攻击]", 1);
                        break;
                    case Grobal2.HAM_PEACE:
                        this.SysMsg("[攻击模式: 和平攻击]", 1);
                        break;
                    case Grobal2.HAM_GROUP:
                        this.SysMsg("[攻击模式: 组队攻击]", 1);
                        break;
                    case Grobal2.HAM_GUILD:
                        this.SysMsg("[攻击模式: 行会攻击]", 1);
                        break;
                    case Grobal2.HAM_PKATTACK:
                        this.SysMsg("[攻击模式: 善恶攻击]", 1);
                        break;
                }
                this.SysMsg("更改攻击模式快捷键: CTRL-H", 1);
                this.SendMsg(this, Grobal2.RM_ATTACKMODE, 0, 0, 0, 0, "");
                if (SecondsCard > 0)
                {
                    this.SysMsg(Format("[账户信息]当前账户充值时间剩余%d秒", new int[] { SecondsCard }), 3);
                }
                else
                {
                    this.SysMsg("[账户信息]当前账户充值时间剩余0秒", 3);
                }
                if (svMain.BoTestServer)
                {
                    this.SysMsg("本服务器是测试服务器，关于服务器的操作规则请查看我们的网站。", 1);
                    // 牢盔 力茄
                    if (svMain.UserEngine.GetUserCount() > svMain.TestServerMaxUser)
                    {
                        if (this.UserDegree < Grobal2.UD_OBSERVER)
                        {
                            this.SysMsg("服务器人数已达上限。", 0);
                            this.SysMsg("连接中断。", 0);
                            EmergencyClose = true;
                        }
                    }
                }
                if (ApprovalMode == 1)
                {
                    // 眉氰魄 荤侩磊, 抛胶飘 辑滚绰 傍楼
                    // 2004/04/22 眉氰贰闺 7->20, 10父傈 -> 500父傈
                    if (!BoServerShifted)
                    {
                        this.SysMsg("你现在处于体验中, 你可以在" + ObjBase.EXPERIENCELEVEL.ToString() + "以前使用，但是会限制你的一些功能。", 1);
                    }
                    this.AvailableGold = 5000000;
                    // 100000;  //眉氰魄 荤侩磊绰 甸 荐 乐绰 捣捞 力茄凳
                    if (this.Abil.Level > ObjBase.EXPERIENCELEVEL)
                    {
                        // 眉氰魄栏肺 立加捞 救凳
                        this.SysMsg("体验模式可以使用到" + ObjBase.EXPERIENCELEVEL.ToString() + " 级。", 0);
                        this.SysMsg("连接中断，请到本游戏网站查看收费相关信息", 0);
                        EmergencyClose = true;
                    }
                }
                // 2003/03/18 抛胶飘 辑滚 牢盔 力茄
                // if ApprovalMode > 3 then begin //公丰荤侩磊, 20老 茄沥 荤侩磊
                if (ApprovalMode == 3)
                {
                    // 公丰荤侩磊, 20老 茄沥 荤侩磊
                    if (!BoServerShifted)
                    {
                        this.SysMsg("现在是免费试玩期间。", 1);
                    }
                }
                if (svMain.BoVentureServer)
                {
                    // 葛氰辑滚
                    this.SysMsg("欢迎来到冒险服务器。", 1);
                }
            }
            Bright = svMain.MirDayTime;
            this.SendMsg(this, Grobal2.RM_ABILITY, 0, 0, 0, 0, "");
            this.SendMsg(this, Grobal2.RM_SUBABILITY, 0, 0, 0, 0, "");
            this.SendMsg(this, Grobal2.RM_DAYCHANGING, 0, 0, 0, 0, "");
            this.SendMsg(this, Grobal2.RM_SENDUSEITEMS, 0, 0, 0, 0, "");
            this.SendMsg(this, Grobal2.RM_SENDMYMAGIC, 0, 0, 0, 0, "");
            // 巩颇俊 啊涝登绢 乐绰瘤..
            this.MyGuild = svMain.GuildMan.GetGuildFromMemberName(this.UserName);
            if (this.MyGuild != null)
            {
                // 辨靛俊 啊涝登绢 乐绰 版快
                this.GuildRankName = ((TGuild)this.MyGuild).MemberLogin(this, ref this.GuildRank);
                // SendMsg (self, RM_CHANGEGUILDNAME, 0, 0, 0, 0, '');
                for (i = 0; i < ((TGuild)this.MyGuild).KillGuilds.Count; i++)
                {
                    this.SysMsg(((TGuild)this.MyGuild).KillGuilds[i] + " is on guild war with your guild.", 1);
                }
                // 辑滚 捞悼捞 酒囱 货肺 立加沁阑 锭父 利侩.
                if (!BoServerShifted)
                {
                    // 巩林/巩颇盔捞 立加沁阑 锭 厘盔捞 楷眉登绢 乐促搁 瘤抄扁埃/巢篮扁埃阑 舅妨淋.
                    if (svMain.GuildAgitMan.IsDelayed(((TGuild)this.MyGuild).GuildName))
                    {
                        CmdGuildAgitRemainTime();
                    }
                    // 立加沁阑 锭 巩颇皋矫瘤(sonmg 2005/08/05)
                    ((TGuild)this.MyGuild).GuildMsg("(!)" + this.UserName + " has connected.", this.UserName);
                    svMain.UserEngine.SendInterMsg(Grobal2.ISM_GUILDMSG, svMain.ServerIndex, ((TGuild)this.MyGuild).GuildName + "/" + "(!)" + this.UserName + " has connected.");
                }
            }
            if (!BoServerShifted)
            {
                CmdGuildAgitExpulsionMyself();
                SendDecoItemList();
            }
            if (this.PLongHitSkill != null)
            {
                if (!this.BoAllowLongHit)
                {
                    this.BoAllowLongHit = true;
                    SendSocket(null, "+LNG");
                }
            }
            if (this.PEnvir.NoReconnect && !BoServerShifted)
            {
                this.RandomSpaceMove(this.PEnvir.BackMap, 0);
            }
            if (PrevServerSlaves.Count > 0)
            {
                for (i = 0; i < PrevServerSlaves.Count; i++)
                {
                    plsave = PrevServerSlaves[i] as TSlaveInfo;
                    RmMakeSlaveProc(plsave);
                    Dispose(plsave);
                }
                PrevServerSlaves.Clear();
            }
            if (!BoServerShifted)
            {
                this.SendMsg(this, Grobal2.RM_DOSTARTUPQUEST, 0, 0, 0, 0, "");
            }
            if (NotReadTag > 0)
            {
                this.SendMsg(this, Grobal2.RM_TAG_ALARM, 0, NotReadTag, 0, 0, "");
                NotReadTag = 0;
            }
            this.SendMsg(this, Grobal2.RM_LM_DBMATLIST, 0, 0, 0, 0, "");
            this.SendMsg(this, Grobal2.RM_LM_DBWANTLIST, 0, 0, 0, 0, "");
            CheckMaster();
        }

        public override void Finalize()
        {
            try
            {
                if (ReadyRun)
                {
                    this.Disappear(5);
                }
            }
            catch
            {
            }
            if (this.BoFixedHideMode)
            {
                if (this.BoHumHideMode)
                {
                    this.StatusArr[Grobal2.STATE_TRANSPARENT] = 0;
                }
            }
            if (this.BoTaiwanEventUser)
            {
                this.StatusArr[Grobal2.STATE_BLUECHAR] = 0;
            }
            try
            {
                if (this.GroupOwner != null)
                {
                    this.GroupOwner.DelGroupMember(this);
                }
                else
                {
                    this.DelGroupMember(this);
                }
            }
            catch
            {
            }
            if (this.MyGuild != null)
            {
                ((TGuild)this.MyGuild).MemberLogout(this);
            }
            WriteConLog();
            base.Finalize();
        }

        public void WriteConLog()
        {
            long contime;
            if ((ApprovalMode == 2) || svMain.BoTestServer)
            {
                contime = (HUtil32.GetTickCount() - LoginTime) / 1000;
            }
            else
            {
                contime = 0;
            }
            svMain.AddConLog(UserAddress + "\09" + UserId + "\09" + this.UserName + "\09" + contime.ToString() + "\09" + FormatDateTime("yyyy-mm-dd hh:mm:ss", LoginDateTime) + "\09" + FormatDateTime("yyyy-mm-dd hh:mm:ss", DateTime.Now) + "\09" + AvailableMode.ToString());
        }

        public void SendSocket(TDefaultMessage pmsg, string body)
        {
            int packetlen;
            TMsgHeader header;
            string pbuf;
            string ansibody;
            pbuf = null;
            try
            {
                header.Code = 0xaa55aa55;
                header.SNumber = UserHandle;
                header.UserGateIndex = UserGateIndex;
                header.Ident = Grobal2.GM_DATA;
                ansibody = body;
                if (pmsg != null)
                {
                    if (ansibody != "")
                    {
                        //header.length = sizeof(TDefaultMessage) + ansibody.Length + 1;
                        //packetlen = sizeof(TMsgHeader) + header.length;
                        //GetMem(pbuf, packetlen + 4);
                        //Move(packetlen, pbuf, 4);
                        //Move(header, pbuf[4], sizeof(TMsgHeader));
                        //Move(pmsg, (pbuf[4 + sizeof(TMsgHeader)]), sizeof(TDefaultMessage));
                        //Move(ansibody[1], (pbuf[4 + sizeof(TMsgHeader) + sizeof(TDefaultMessage)]), ansibody.Length + 1);
                    }
                    else
                    {
                        //header.length = sizeof(TDefaultMessage);
                        //packetlen = sizeof(TMsgHeader) + header.length;
                        //GetMem(pbuf, packetlen + 4);
                        //Move(packetlen, pbuf, 4);
                        //Move(header, pbuf[4], sizeof(TMsgHeader));
                        //Move(pmsg, (pbuf[4 + sizeof(TMsgHeader)]), sizeof(TDefaultMessage));
                    }
                }
                else
                {
                    if (ansibody != "")
                    {
                        //header.length = -(ansibody.Length + 1);
                        //packetlen = sizeof(TMsgHeader) + Math.Abs(header.length);
                        //GetMem(pbuf, packetlen + 4);
                        //Move(packetlen, pbuf, 4);
                        //Move(header, pbuf[4], sizeof(TMsgHeader));
                        //Move(ansibody[1], (pbuf[4 + sizeof(TMsgHeader)]), ansibody.Length + 1);
                    }
                }
                svMain.humanLock.Enter();
                try
                {
                    svMain.RunSocket.SendUserSocket(GateIndex, pbuf);
                }
                finally
                {
                    svMain.humanLock.Leave();
                }
            }
            catch
            {
                svMain.MainOutMessage("Exception SendSocket..");
            }
        }

        public void SendDefMessage(int msg, int recog, int param, int tag, int series, string addstr)
        {
            Def = Grobal2.MakeDefaultMsg((ushort)msg, recog, param, tag, series);
            if (addstr != "")
            {
                SendSocket(Def, EDcode.EncodeString(addstr));
            }
            else
            {
                SendSocket(Def, "");
            }
        }

        public void GuildRankChanged(int rank, string rname)
        {
            this.GuildRank = rank;
            this.GuildRankName = rname;
            this.SendMsg(this, Grobal2.RM_CHANGEGUILDNAME, 0, 0, 0, 0, "");
        }

        // ----------------------------------------------
        private bool TurnXY(int x, int y, int dir)
        {
            bool result;
            result = false;
            if ((x == this.CX) && (y == this.CY))
            {
                this.Dir = (byte)dir;
                if (this.Walk(Grobal2.RM_TURN))
                {
                    result = true;
                }
            }
            return result;
        }

        private bool WalkXY(int x, int y)
        {
            byte ndir;
            short oldx;
            short oldy;
            bool allowdup;
            bool result = false;
            if (HUtil32.GetTickCount() - LatestWalkTime < 600)
            {
                WalkTimeOverCount++;
                WalkTimeOverSum++;
            }
            else
            {
                WalkTimeOverCount = 0;
                if (WalkTimeOverSum > 0)
                {
                    WalkTimeOverSum -= 1;
                }
            }
            LatestWalkTime  =  HUtil32.GetTickCount();
            if ((WalkTimeOverCount < 4) && (WalkTimeOverSum < 6))
            {
                this.SpaceMoved = false;
                oldx = this.CX;
                oldy = this.CY;
                ndir = M2Share.GetNextDirection(this.CX, this.CY, x, y);
                allowdup = true;
                if (this.WalkTo(ndir, allowdup))
                {
                    // 般媚瘤瘤 臼霸 窃.
                    if (this.SpaceMoved || (this.CX == x) && (this.CY == y))
                    {
                        result = true;
                    }
                    this.HealthTick -= 10;
                    // 20
                }
                else
                {
                    // 叭扁 角菩
                    WalkTimeOverCount = 0;
                    WalkTimeOverSum = 0;
                }
            }
            else
            {
                SpeedHackTimerOverCount++;
                if (SpeedHackTimerOverCount > 8)
                {
                    EmergencyClose = true;
                }
                if (svMain.BoViewHackCode)
                {
                    svMain.MainOutMessage("[11002-Walk] " + this.UserName + " " + DateTime.Now.ToString());
                }
            }
            return result;
        }

        private bool RunXY(int x, int y)
        {
            bool result;
            byte ndir;
            bool allowdup;
            result = false;
            if (HUtil32.GetTickCount() - LatestWalkTime < 600)
            {
                WalkTimeOverCount++;
                WalkTimeOverSum++;
            }
            else
            {
                WalkTimeOverCount = 0;
                if (WalkTimeOverSum > 0)
                {
                    WalkTimeOverSum -= 1;
                }
            }
            // dis := GetTickCount - LatestWalkTime;
            // MainOutMessage (IntToStr(dis));
            LatestWalkTime  =  HUtil32.GetTickCount();
            if ((WalkTimeOverCount < 4) && (WalkTimeOverSum < 6))
            {
                this.SpaceMoved = false;
                ndir = M2Share.GetNextDirection(this.CX, this.CY, x, y);
                allowdup = true;
                // 乞惑矫俊绰 钝锭 般磨 荐 乐澜
                if (svMain.UserCastle.BoCastleUnderAttack)
                {
                    // 傍己傈 吝牢 版快
                    if (this.BoInFreePKArea)
                    {
                        // 橇府乔纳捞粮(傈里磐)俊 乐澜, 傍己 瘤开俊 乐澜
                        allowdup = false;
                    }
                    // 傍己傈 瘤开俊辑绰 般磨 荐 绝澜
                }
                if (this.RunTo(ndir, allowdup))
                {
                    if (this.BoFixedHideMode)
                    {
                        // 绊沥 篮脚贱..
                        if (this.BoHumHideMode)
                        {
                            // 捞悼茄版快俊绰 篮脚贱捞 钱赴促.
                            this.StatusArr[Grobal2.STATE_TRANSPARENT] = 1;
                        }
                    }
                    if (this.SpaceMoved || (this.CX == x) && (this.CY == y))
                    {
                        result = true;
                    }
                    this.HealthTick -= 60;
                    // 150
                    this.SpellTick -= 10;
                    this.SpellTick = _MAX(0, this.SpellTick);
                    this.PerHealth -= 1;
                    this.PerSpell -= 1;
                }
                else
                {
                    WalkTimeOverCount = 0;
                    WalkTimeOverSum = 0;
                }
            }
            else
            {
                SpeedHackTimerOverCount++;
                if (SpeedHackTimerOverCount > 8)
                {
                    EmergencyClose = true;
                }
                if (svMain.BoViewHackCode)
                {
                    svMain.MainOutMessage("[11002-Run] " + this.UserName + " " + DateTime.Now.ToString());
                }
            }
            return result;
        }

        private void GetRandomMineral()
        {
            TUserItem pi;
            if (this.ItemList.Count < Grobal2.MAXBAGITEM)
            {
                switch (new System.Random(120).Next())
                {
                    // Modify the A .. B: 1 .. 2
                    case 1:
                        // 陛堡籍
                        pi = new TUserItem();
                        if (svMain.UserEngine.CopyToUserItemFromName(svMain.__GoldStone, ref pi))
                        {
                            // 堡籍狼 鉴档 利侩....
                            pi.Dura = this.GetPurity();
                            this.ItemList.Add(pi);
                            this.WeightChanged();
                            SendAddItem(pi);
                        }
                        else
                        {
                            Dispose(pi);
                        }
                        break;
                    // Modify the A .. B: 3 .. 20
                    case 3:
                        // 篮堡籍
                        pi = new TUserItem();
                        if (svMain.UserEngine.CopyToUserItemFromName(svMain.__SilverStone, ref pi))
                        {
                            // 堡籍狼 鉴档 利侩....
                            pi.Dura = this.GetPurity();
                            this.ItemList.Add(pi);
                            this.WeightChanged();
                            SendAddItem(pi);
                        }
                        else
                        {
                            Dispose(pi);
                        }
                        break;
                    // Modify the A .. B: 21 .. 45
                    case 21:
                        // 枚堡籍
                        pi = new TUserItem();
                        if (svMain.UserEngine.CopyToUserItemFromName(svMain.__SteelStone, ref pi))
                        {
                            // 堡籍狼 鉴档 利侩....
                            pi.Dura = this.GetPurity();
                            this.ItemList.Add(pi);
                            this.WeightChanged();
                            SendAddItem(pi);
                        }
                        else
                        {
                            Dispose(pi);
                        }
                        break;
                    // Modify the A .. B: 46 .. 56
                    case 46:
                        // 孺枚
                        pi = new TUserItem();
                        if (svMain.UserEngine.CopyToUserItemFromName(svMain.__BlackStone, ref pi))
                        {
                            // 堡籍狼 鉴档 利侩....
                            pi.Dura = this.GetPurity();
                            this.ItemList.Add(pi);
                            this.WeightChanged();
                            SendAddItem(pi);
                        }
                        else
                        {
                            Dispose(pi);
                        }
                        break;
                    default:
                        // 悼堡籍
                        pi = new TUserItem();
                        if (svMain.UserEngine.CopyToUserItemFromName(svMain.__CopperStone, ref pi))
                        {
                            // 堡籍狼 鉴档 利侩....
                            pi.Dura = this.GetPurity();
                            this.ItemList.Add(pi);
                            this.WeightChanged();
                            SendAddItem(pi);
                        }
                        else
                        {
                            Dispose(pi);
                        }
                        break;
                }
            }
        }

        private void GetRandomGems()
        {
            TUserItem pi;
            if (this.ItemList.Count < Grobal2.MAXBAGITEM)
            {
                switch (new System.Random(120).Next())
                {
                    // Modify the A .. B: 1 .. 2
                    case 1:
                        // 归陛
                        pi = new TUserItem();
                        if (svMain.UserEngine.CopyToUserItemFromName(svMain.__Gem1Stone, ref pi))
                        {
                            // 堡籍狼 鉴档 利侩....
                            pi.Dura = this.GetPurity();
                            this.ItemList.Add(pi);
                            this.WeightChanged();
                            SendAddItem(pi);
                        }
                        else
                        {
                            Dispose(pi);
                        }
                        break;
                    // Modify the A .. B: 3 .. 20
                    case 3:
                        // 楷苛
                        pi = new TUserItem();
                        if (svMain.UserEngine.CopyToUserItemFromName(svMain.__Gem2Stone, ref pi))
                        {
                            // 堡籍狼 鉴档 利侩....
                            pi.Dura = this.GetPurity();
                            this.ItemList.Add(pi);
                            this.WeightChanged();
                            SendAddItem(pi);
                        }
                        else
                        {
                            Dispose(pi);
                        }
                        break;
                    // Modify the A .. B: 21 .. 45
                    case 21:
                        // 全苛籍
                        pi = new TUserItem();
                        if (svMain.UserEngine.CopyToUserItemFromName(svMain.__Gem4Stone, ref pi))
                        {
                            // 堡籍狼 鉴档 利侩....
                            pi.Dura = this.GetPurity();
                            this.ItemList.Add(pi);
                            this.WeightChanged();
                            SendAddItem(pi);
                        }
                        else
                        {
                            Dispose(pi);
                        }
                        break;
                    default:
                        // 磊荐沥
                        pi = new TUserItem();
                        if (svMain.UserEngine.CopyToUserItemFromName(svMain.__Gem3Stone, ref pi))
                        {
                            // 堡籍狼 鉴档 利侩....
                            pi.Dura = this.GetPurity();
                            this.ItemList.Add(pi);
                            this.WeightChanged();
                            SendAddItem(pi);
                        }
                        else
                        {
                            Dispose(pi);
                        }
                        break;
                }
            }
        }

        // 货肺 眠啊等 MINE3 加己俊辑 唱坷绰 堡籍甸...(2004/11/03)
        private void GetRandomMineral3()
        {
            TUserItem pi;
            if (this.ItemList.Count < Grobal2.MAXBAGITEM)
            {
                switch (new System.Random(240).Next())
                {
                    // 1..4: //陛堡籍
                    // Modify the A .. B: 1 .. 6
                    case 1:
                        // 陛堡籍
                        pi = new TUserItem();
                        if (svMain.UserEngine.CopyToUserItemFromName(svMain.__GoldStone, ref pi))
                        {
                            // 堡籍狼 鉴档 利侩....
                            pi.Dura = this.GetPurity();
                            this.ItemList.Add(pi);
                            this.WeightChanged();
                            SendAddItem(pi);
                        }
                        else
                        {
                            Dispose(pi);
                        }
                        break;
                    // 5..22: //篮堡籍
                    // Modify the A .. B: 7 .. 30
                    case 7:
                        // 篮堡籍
                        pi = new TUserItem();
                        if (svMain.UserEngine.CopyToUserItemFromName(svMain.__SilverStone, ref pi))
                        {
                            // 堡籍狼 鉴档 利侩....
                            pi.Dura = this.GetPurity();
                            this.ItemList.Add(pi);
                            this.WeightChanged();
                            SendAddItem(pi);
                        }
                        else
                        {
                            Dispose(pi);
                        }
                        break;
                    // 23..47: //枚堡籍
                    // Modify the A .. B: 31 .. 66
                    case 31:
                        // 枚堡籍
                        pi = new TUserItem();
                        if (svMain.UserEngine.CopyToUserItemFromName(svMain.__SteelStone, ref pi))
                        {
                            // 堡籍狼 鉴档 利侩....
                            pi.Dura = this.GetPurity();
                            this.ItemList.Add(pi);
                            this.WeightChanged();
                            SendAddItem(pi);
                        }
                        else
                        {
                            Dispose(pi);
                        }
                        break;
                    // 48..67: //孺枚
                    // Modify the A .. B: 67 .. 91
                    case 67:
                        // 孺枚
                        pi = new TUserItem();
                        if (svMain.UserEngine.CopyToUserItemFromName(svMain.__BlackStone, ref pi))
                        {
                            // 堡籍狼 鉴档 利侩....
                            pi.Dura = this.GetPurity();
                            this.ItemList.Add(pi);
                            this.WeightChanged();
                            SendAddItem(pi);
                        }
                        else
                        {
                            Dispose(pi);
                        }
                        break;
                    // 68..140: //悼堡籍
                    // Modify the A .. B: 92 .. 131
                    case 92:
                        // 悼堡籍
                        pi = new TUserItem();
                        if (svMain.UserEngine.CopyToUserItemFromName(svMain.__CopperStone, ref pi))
                        {
                            // 堡籍狼 鉴档 利侩....
                            pi.Dura = this.GetPurity();
                            this.ItemList.Add(pi);
                            this.WeightChanged();
                            SendAddItem(pi);
                        }
                        else
                        {
                            Dispose(pi);
                        }
                        break;
                    // 141..144: //归陛
                    // Modify the A .. B: 132 .. 137
                    case 132:
                        // 归陛
                        pi = new TUserItem();
                        if (svMain.UserEngine.CopyToUserItemFromName(svMain.__Gem1Stone, ref pi))
                        {
                            // 堡籍狼 鉴档 利侩....
                            pi.Dura = this.GetPurity();
                            this.ItemList.Add(pi);
                            this.WeightChanged();
                            SendAddItem(pi);
                        }
                        else
                        {
                            Dispose(pi);
                        }
                        break;
                    // 145..162: //楷苛
                    // Modify the A .. B: 138 .. 161
                    case 138:
                        // 楷苛
                        pi = new TUserItem();
                        if (svMain.UserEngine.CopyToUserItemFromName(svMain.__Gem2Stone, ref pi))
                        {
                            // 堡籍狼 鉴档 利侩....
                            pi.Dura = this.GetPurity();
                            this.ItemList.Add(pi);
                            this.WeightChanged();
                            SendAddItem(pi);
                        }
                        else
                        {
                            Dispose(pi);
                        }
                        break;
                    // 163..187: //全苛籍
                    // Modify the A .. B: 162 .. 197
                    case 162:
                        // 全苛籍
                        pi = new TUserItem();
                        if (svMain.UserEngine.CopyToUserItemFromName(svMain.__Gem4Stone, ref pi))
                        {
                            // 堡籍狼 鉴档 利侩....
                            pi.Dura = this.GetPurity();
                            this.ItemList.Add(pi);
                            this.WeightChanged();
                            SendAddItem(pi);
                        }
                        else
                        {
                            Dispose(pi);
                        }
                        break;
                    default:
                        // 磊荐沥
                        pi = new TUserItem();
                        if (svMain.UserEngine.CopyToUserItemFromName(svMain.__Gem3Stone, ref pi))
                        {
                            // 堡籍狼 鉴档 利侩....
                            pi.Dura = this.GetPurity();
                            this.ItemList.Add(pi);
                            this.WeightChanged();
                            SendAddItem(pi);
                        }
                        else
                        {
                            Dispose(pi);
                        }
                        break;
                }
            }
        }

        // 堡籍阑 某促.
        private bool DigUpMine(int x, int y)
        {
            bool result;
            TEvent __event;
            TEvent ev2;
            string desc;
            result = false;
            desc = "";
            __event = (TEvent)this.PEnvir.GetEvent(x, y);
            if (__event != null)
            {
                if ((__event.EventType == Grobal2.ET_MINE) || (__event.EventType == Grobal2.ET_MINE2) || (__event.EventType == Grobal2.ET_MINE3))
                {
                    if (((TStoneMineEvent)__event).MineCount > 0)
                    {
                        ((TStoneMineEvent)__event).MineCount = ((TStoneMineEvent)__event).MineCount - 1;
                        if (new System.Random(4).Next() == 0)
                        {
                            // 某扁 己傍
                            ev2 = (TEvent)this.PEnvir.GetEvent(this.CX, this.CY);
                            if (ev2 == null)
                            {
                                ev2 = new TPileStones(this.PEnvir, this.CX, this.CY, Grobal2.ET_PILESTONES, 5 * 60 * 1000, true);
                                svMain.EventMan.AddEvent(ev2);
                            }
                            else
                            {
                                if (ev2.EventType == Grobal2.ET_PILESTONES)
                                {
                                    ((TPileStones)ev2).EnlargePile();
                                }
                            }
                            if (new System.Random(12).Next() == 0)
                            {
                                // 堡籍 某扁 己傍
                                if (__event.EventType == Grobal2.ET_MINE)
                                {
                                    GetRandomMineral();
                                }
                                else if (__event.EventType == Grobal2.ET_MINE2)
                                {
                                    GetRandomGems();
                                }
                                else
                                {
                                    GetRandomMineral3();
                                }
                            }
                            desc = "1";
                            this.DoDamageWeapon(5 + new System.Random(15).Next());
                            result = true;
                        }
                    }
                    else
                    {
                        if (HUtil32.GetTickCount() - ((TStoneMineEvent)__event).RefillTime > 10 * 60 * 1000)
                        {
                            // 10盒
                            ((TStoneMineEvent)__event).Refill();
                        }
                    }
                }
            }
            this.SendRefMsg(Grobal2.RM_HEAVYHIT, this.Dir, this.CX, this.CY, 0, desc);
            return result;
        }
      
        private bool HitXY(int hitid, int x, int y, byte dir)
        {
            short fx = 0;
            short fy = 0;
            TStdItem pstd;
            bool result = false;
            if ((HUtil32.GetTickCount() - LatestHitTime) < ((long)900 - (this.HitSpeed * 60)))
            {
                HitTimeOverCount++;
                HitTimeOverSum++;
            }
            else
            {
                HitTimeOverCount = 0;
                if (HitTimeOverSum > 0)
                {
                    HitTimeOverSum -= 1;
                }
            }
            if ((HitTimeOverCount < 4) && (HitTimeOverSum < 6))
            {
                if (!this.Death)
                {
                    if ((x == this.CX) && (y == this.CY))
                    {
                        result = true;
                        LatestHitTime  =  HUtil32.GetTickCount();
                        if ((hitid == Grobal2.CM_HEAVYHIT) && (this.UseItems[Grobal2.U_WEAPON].Index > 0) && M2Share.GetFrontPosition(this, ref fx, ref fy))
                        {
                            if (!this.PEnvir.CanWalk(fx, fy, false))
                            {
                                pstd = svMain.UserEngine.GetStdItem(this.UseItems[Grobal2.U_WEAPON].Index);
                                if (pstd != null)
                                {
                                    if (pstd.Shape == 19)
                                    {
                                        if (DigUpMine(fx, fy))
                                        {
                                            SendSocket(null, "=DIG");
                                        }
                                        this.HealthTick -= 30;
                                        this.SpellTick -= 50;
                                        this.SpellTick = _MAX(0, this.SpellTick);
                                        this.PerHealth -= 2;
                                        this.PerSpell -= 2;
                                        return result;
                                    }
                                }
                            }
                        }
                        if (hitid == Grobal2.CM_HIT)
                        {
                            // inherited
                            this.HitHit(null, Grobal2.HM_HIT, dir);
                        }
                        if (hitid == Grobal2.CM_HEAVYHIT)
                        {
                            // inherited
                            this.HitHit(null, Grobal2.HM_HEAVYHIT, dir);
                        }
                        if (hitid == Grobal2.CM_BIGHIT)
                        {
                            // inherited
                            this.HitHit(null, Grobal2.HM_BIGHIT, dir);
                        }
                        if (hitid == Grobal2.CM_POWERHIT)
                        {
                            // inherited
                            this.HitHit(null, Grobal2.HM_POWERHIT, dir);
                        }
                        if (hitid == Grobal2.CM_LONGHIT)
                        {
                            // inherited
                            this.HitHit(null, Grobal2.HM_LONGHIT, dir);
                        }
                        if (hitid == Grobal2.CM_WIDEHIT)
                        {
                            // inherited
                            this.HitHit(null, Grobal2.HM_WIDEHIT, dir);
                        }
                        if (hitid == Grobal2.CM_FIREHIT)
                        {
                            // inherited
                            this.HitHit(null, Grobal2.HM_FIREHIT, dir);
                        }
                        // 2003/03/15 脚痹公傍
                        if (hitid == Grobal2.CM_CROSSHIT)
                        {
                            // inherited
                            this.HitHit(null, Grobal2.HM_CROSSHIT, dir);
                        }
                        if (hitid == Grobal2.CM_TWINHIT)
                        {
                            // inherited
                            this.HitHit(null, Grobal2.HM_TWINHIT, dir);
                        }
                        // Power Hit阑 磨荐 乐绰 八过阑 劳躯绊, 八(公扁)阑 甸绊 乐绰 版快
                        // 唱吝俊 八苞, 档尝(5, 6)甫 备盒窍咯 八过阑 父电促.
                        if ((this.PPowerHitSkill != null) && (this.UseItems[Grobal2.U_WEAPON].Index > 0))
                        {
                            this.AttackSkillCount -= 1;
                            if (this.AttackSkillPointCount == this.AttackSkillCount)
                            {
                                this.BoAllowPowerHit = true;
                                SendSocket(null, "+PWR");
                                // 努扼捞攫飘俊 促澜锅俊 powerhit阑 锭府档肺 窃
                            }
                            if (this.AttackSkillCount <= 0)
                            {
                                this.AttackSkillCount = 7 - this.PPowerHitSkill.Level;
                                this.AttackSkillPointCount = new System.Random(this.AttackSkillCount).Next();
                            }
                        }
                        this.HealthTick -= 30;
                        // 100
                        this.SpellTick -= 100;
                        this.SpellTick = _MAX(0, this.SpellTick);
                        this.PerHealth -= 2;
                        this.PerSpell -= 2;
                    }
                    else
                    {
                        result = false;
                    }
                }
            }
            else
            {
                LatestHitTime  =  HUtil32.GetTickCount();
                SpeedHackTimerOverCount++;
                if (SpeedHackTimerOverCount > 8)
                {
                    EmergencyClose = true;
                }
                if (svMain.BoViewHackCode)
                {
                    svMain.MainOutMessage("[11000-Hit] " + this.UserName + " " + DateTime.Now.ToString());
                }
                // SysMsg ('recorded as user of hacking program .', 0);
                // SysMsg ('Please be noted your account can be seizied .', 0);
                // SysMsg ('CODE=11000 Please contact to game master(support@legendofmir.net)', 0);
                // EmergencyClose := TRUE;
            }
            return result;
        }

        private TUserMagic GetMagic(int mid)
        {
            TUserMagic result;
            int i;
            result = null;
            for (i = 0; i < this.MagicList.Count; i++)
            {
                if (((TUserMagic)this.MagicList[i]).pDef.MagicId == mid)
                {
                    result = (TUserMagic)this.MagicList[i];
                    break;
                }
            }
            return result;
        }

        private bool SpellXY(int magid, int targetx, int targety, int targcret)
        {
            bool result;
            int ndir;
            int spell;
            TCreature targ;
            TUserMagic pum;
            result = false;
            // MainOutMessage ('Delay ' + InttoStr(HUtil32.GetTickCount() - LatestSpellTime) + ' ' + IntToStr(LatestSpellDelay));
            if ((this.StatusArr[Grobal2.POISON_STONE] != 0) || (this.StatusArr[Grobal2.POISON_STUN] != 0) || StallMgr.OnSale || (this.StatusArr[Grobal2.POISON_ICE] != 0))
            {
                // 付厚等 惑怕牢 版快
                return result;
            }
            if (HUtil32.GetTickCount() - LatestSpellTime > LatestSpellDelay)
            {
                SpellTimeOverCount = 0;
            }
            else
            {
                SpellTimeOverCount++;
            }
            if (SpellTimeOverCount < 2)
            {
                // magid肺 magnum甫 掘绢咳.
                pum = null;
                this.SpellTick -= 450;
                this.SpellTick = _MAX(0, this.SpellTick);
                pum = GetMagic(magid);
                if (pum != null)
                {
                    if (svMain.MagicMan.IsSwordSkill(pum.MagicId))
                    {
                        // pum.pDef.DelayTime + 200  //八过 掉饭捞
                        LatestSpellDelay = 0;
                    }
                    else
                    {
                        LatestSpellDelay = pum.pDef.DelayTime + 800;
                    }
                    // 付过 掉饭捞
                    LatestSpellTime  =  HUtil32.GetTickCount();
                    switch (pum.MagicId)
                    {
                        case Grobal2.SWD_LONGHIT:
                            // 付瘤阜栏肺 付过阑 敬 矫埃捞 捞饶肺 何磐 付过 掉饭捞
                            // 捞饶俊 甸绢柯 付过父阑 倾侩茄促.
                            // 绢八贱
                            if (this.PLongHitSkill != null)
                            {
                                if (!this.BoAllowLongHit)
                                {
                                    this.SetAllowLongHit(true);
                                    SendSocket(null, "+LNG");
                                    // 盔芭府 傍拜阑 窍霸 茄促.
                                }
                                else
                                {
                                    this.SetAllowLongHit(false);
                                    SendSocket(null, "+ULNG");
                                    // 盔芭府 傍拜阑 救窍霸 茄促.
                                }
                            }
                            result = true;
                            break;
                        case Grobal2.SWD_WIDEHIT:
                            // 馆岿八过
                            if (this.PWideHitSkill != null)
                            {
                                if (!this.BoAllowWideHit)
                                {
                                    if (this.BoAllowCrossHit)
                                    {
                                        this.SetAllowCrossHit(false);
                                        SendSocket(null, "+UCRS");
                                        // 堡浅曼 荤侩救窃
                                    }
                                    this.SetAllowWideHit(true);
                                    SendSocket(null, "+WID");
                                    // 馆岿八过 荤侩
                                }
                                else
                                {
                                    this.SetAllowWideHit(false);
                                    SendSocket(null, "+UWID");
                                    // 馆岿八过 荤侩救窃
                                }
                            }
                            result = true;
                            break;
                        case Grobal2.SWD_FIREHIT:
                            // 堪拳搬
                            if (this.PFireHitSkill != null)
                            {
                                if (this.SetAllowFireHit())
                                {
                                    spell = this.GetSpellPoint(pum);
                                    if (this.WAbil.MP >= spell)
                                    {
                                        if (spell > 0)
                                        {
                                            this.DamageSpell(spell);
                                            this.HealthSpellChanged();
                                        }
                                        SendSocket(null, "+FIR");
                                    }
                                    else
                                    {
                                    }
                                }
                                result = true;
                            }
                            break;
                        case Grobal2.SWD_RUSHRUSH:
                            // 公怕焊
                            result = true;
                            if (HUtil32.GetTickCount() - this.LatestRushRushTime > 3000)
                            {
                                this.LatestRushRushTime  =  HUtil32.GetTickCount();
                                this.Dir = (byte)targetx;
                                // 规氢 傈券
                                // if GetTickCount - LatestRushRushTime >= 3000
                                spell = this.GetSpellPoint(pum);
                                if (spell > 0)
                                {
                                    if (this.WAbil.MP >= spell)
                                    {
                                        this.DamageSpell(spell);
                                        this.HealthSpellChanged();
                                    }
                                    else
                                    {
                                        return result;
                                    }
                                    // 付仿葛磊恩
                                }
                                if (this.CharRushRush(this.Dir, pum.Level, true))
                                {
                                    if (pum.Level < 3)
                                    {
                                        if (this.Abil.Level >= pum.pDef.NeedLevel[pum.Level])
                                        {
                                            // 荐访饭骇俊 档崔茄 版快
                                            this.TrainSkill(pum, 1 + new System.Random(3).Next());
                                            if (!this.CheckMagicLevelup(pum))
                                            {
                                                this.SendDelayMsg(this, Grobal2.RM_MAGIC_LVEXP, 0, pum.pDef.MagicId, pum.Level, pum.CurTrain, "", 1000);
                                            }
                                        }
                                    }
                                }
                            }
                            break;
                        case Grobal2.SWD_CROSSHIT:
                            // 2003/03/15 脚痹公傍
                            // 堡浅曼
                            if (this.PCrossHitSkill != null)
                            {
                                if (!this.BoAllowCrossHit)
                                {
                                    if (this.BoAllowWideHit)
                                    {
                                        this.SetAllowWideHit(false);
                                        SendSocket(null, "+UWID");
                                        // 馆岿八过 荤侩救窃
                                    }
                                    this.SetAllowCrossHit(true);
                                    SendSocket(null, "+CRS");
                                    // 堡浅曼 荤侩
                                }
                                else
                                {
                                    this.SetAllowCrossHit(false);
                                    SendSocket(null, "+UCRS");
                                    // 堡浅曼 荤侩救窃
                                }
                            }
                            result = true;
                            break;
                        case Grobal2.SWD_TWINHIT:
                            // 街锋曼
                            if (this.PTwinHitSkill != null)
                            {
                                if (this.SetAllowTwinHit())
                                {
                                    spell = this.GetSpellPoint(pum);
                                    if (this.WAbil.MP >= spell)
                                    {
                                        if (spell > 0)
                                        {
                                            this.DamageSpell(spell);
                                            this.HealthSpellChanged();
                                        }
                                        SendSocket(null, "+TWN");
                                    }
                                    else
                                    {
                                    }
                                }
                                result = true;
                            }
                            break;
                        default:
                            ndir = M2Share.GetNextDirection(this.CX, this.CY, targetx, targety);
                            this.Dir = (byte)ndir;
                            targ = null;
                            if (this.CretInNearXY(targcret as TCreature, targetx, targety))
                            {
                                targ = targcret as TCreature;
                                targetx = targ.CX;
                                targety = targ.CY;
                            }
                            if (!this.DoSpell(pum, targetx, targety, targ))
                            {
                                this.SendRefMsg(Grobal2.RM_MAGICFIRE_FAIL, 0, 0, 0, 0, "");
                            }
                            result = true;
                            break;
                    }
                }
            }
            else
            {
                pum = GetMagic(magid);
                if (pum != null)
                {
                    if (svMain.MagicMan.IsSwordSkill(pum.MagicId))
                    {
                        SpellTimeOverCount = 0;
                        return result;
                        // 八过虐..
                    }
                }
                LatestSpellTime  =  HUtil32.GetTickCount();
                SpeedHackTimerOverCount++;
                if (SpeedHackTimerOverCount > 8)
                {
                    EmergencyClose = true;
                }
                if (svMain.BoViewHackCode)
                {
                    svMain.MainOutMessage("[11001-Mag] " + this.UserName + " " + DateTime.Now.ToString());
                }
                // SysMsg ('recorded as user of hacking program .', 0);
                // SysMsg ('Please be noted your account can be seizied .', 0);
                // MakePoison (POISON_DECHEALTH, 30, 1);
                // MakePoison (POISON_STONE, 5, 0); //吝刀俊 吧府霸 窃
                // SysMsg ('CODE=11001 Please contact to game master.(support@legendofmir.net)', 0);
                // EmergencyClose := TRUE;
            }
            return result;
        }

        private bool SitdownXY(int x, int y, int dir)
        {
            bool result;
            this.SendRefMsg(Grobal2.RM_SITDOWN, 0, 0, 0, 0, "");
            result = true;
            return result;
        }

        // 款康磊 疙飞绢
        // ----------------------------------------------------------
        // 款康磊 疙飞绢...
        public void ChangeSkillLevel(string magname, byte lv)
        {
            int i;
            lv = (byte)_MIN(3, lv);
            for (i = this.MagicList.Count - 1; i >= 0; i--)
            {
                if (((TUserMagic)this.MagicList[i]).pDef.MagicName.ToLower().CompareTo(magname.ToLower()) == 0)
                {
                    ((TUserMagic)this.MagicList[i]).Level = lv;
                    this.SendMsg(this, Grobal2.RM_MAGIC_LVEXP, 0, ((TUserMagic)this.MagicList[i]).pDef.MagicId, ((TUserMagic)this.MagicList[i]).Level, ((TUserMagic)this.MagicList[i]).CurTrain, "");
                    this.SysMsg(magname + " training level" + lv.ToString() + ", was changed ", 1);
                }
            }
        }

        public void CmdMakeFullSkill(string magname, byte lv)
        {
            ChangeSkillLevel(magname, lv);
        }

        public void CmdMakeOtherChangeSkillLevel(string who, string magname, byte lv)
        {
            TUserHuman hum;
            hum = svMain.UserEngine.GetUserHuman(who);
            if (hum != null)
            {
                hum.ChangeSkillLevel(magname, lv);
            }
            else
            {
                this.SysMsg(who + "  is not found.", 0);
            }
        }

        public bool CmdDeletePKPoint(string whostr)
        {
            bool result;
            TUserHuman hum;
            result = false;
            hum = svMain.UserEngine.GetUserHuman(whostr);
            if (hum != null)
            {
                hum.PlayerKillingPoint = 0;
                // 搁了
                hum.ChangeNameColor();
                this.SysMsg(whostr + " : PK point = 0.", 1);
                result = true;
            }
            else
            {
                this.SysMsg(whostr + "  is not found.", 0);
            }
            return result;
        }

        public void CmdSendPKPoint(string whostr, int value)
        {
            TUserHuman hum;
            hum = svMain.UserEngine.GetUserHuman(whostr);
            if (hum != null)
            {
                if (value > 0)
                {
                    hum.PlayerKillingPoint = value;
                }
                this.SysMsg(whostr + " PK point = " + hum.PlayerKillingPoint.ToString(), 1);
            }
            else
            {
                this.SysMsg(whostr + "  is not found.", 0);
            }
        }

        public void CmdChangeJob(string jobname)
        {
            if (jobname.ToLower().CompareTo("战士".ToLower()) == 0)
            {
                this.Job = 0;
            }
            if (jobname.ToLower().CompareTo("魔法师".ToLower()) == 0)
            {
                this.Job = 1;
            }
            if (jobname.ToLower().CompareTo("道士".ToLower()) == 0)
            {
                this.Job = 2;
            }
        }

        public void CmdChangeSex()
        {
            if (this.Sex == 0)
            {
                this.Sex = 1;
            }
            else
            {
                this.Sex = 0;
            }
        }

        public void CmdCallMakeMonster(string monname, string param)
        {
            short nx =0;
            short ny =0;
            if (param != "")
            {
                if (param[0] == '0')
                {
                    return;
                }
            }
            int count = _MIN(100, HUtil32.Str_ToInt(param, 1));
            M2Share.GetFrontPosition(this, ref nx, ref ny);
            for (var i = 0; i < count; i++)
            {
                svMain.UserEngine.AddCreatureSysop(this.MapName, nx, ny, monname);
            }
        }

        public void CmdCallMakeSlaveMonster(string monname, string param, byte makelv, byte explv)
        {
            short nx =0;
            short ny =0;
            int i;
            int count;
            TCreature cret;
            count = HUtil32.Str_ToInt(param, 1);
            if (!(makelv >= 0 && makelv <= 7))
            {
                makelv = 0;
            }
            if (!(explv >= 0 && explv <= 7))
            {
                explv = 0;
            }
            for (i = 0; i < count; i++)
            {
                if (this.SlaveList.Count < 20)
                {
                    M2Share.GetFrontPosition(this, ref nx, ref ny);
                    cret = svMain.UserEngine.AddCreatureSysop(this.MapName, nx, ny, monname);
                    if (cret != null)
                    {
                        cret.Master = this;
                        cret.MasterRoyaltyTime = GetTickCount + 10 * 24 * 60 * 60 * 1000;
                        cret.SlaveMakeLevel = makelv;
                        cret.SlaveExpLevel = explv;
                        cret.MasterFeature = this.GetRelFeature(this);
                        cret.UserNameChanged();
                        cret.RecalcAbilitys();
                        this.SlaveList.Add(cret);
                    }
                }
            }
        }

        public void CmdMissionSetting(string xstr, string ystr)
        {
            int xx;
            int yy;
            if (xstr == "")
            {
                svMain.BoSysHasMission = false;
                this.SysMsg("mission cancelled", 1);
            }
            else
            {
                xx = HUtil32.Str_ToInt(xstr, 0);
                yy = HUtil32.Str_ToInt(ystr, 0);
                svMain.BoSysHasMission = true;
                svMain.SysMission_Map = this.MapName;
                svMain.SysMission_X = xx;
                svMain.SysMission_Y = yy;
                this.SysMsg("Mission: attack target" + this.MapName + " " + xx.ToString() + ":" + yy.ToString(), 1);
            }
        }

        public void CmdCallMakeMonsterXY(string xstr, string ystr, string monname, string countstr)
        {
            int i;
            int count;
            int xx;
            int yy;
            TEnvirnoment penv;
            TCreature cret;
            if (!svMain.BoSysHasMission)
            {
                this.SysMsg("Mission not specified", 0);
                return;
            }
            count = _MIN(500, HUtil32.Str_ToInt(countstr, 0));
            xx = HUtil32.Str_ToInt(xstr, 0);
            yy = HUtil32.Str_ToInt(ystr, 0);
            penv = svMain.GrobalEnvir.GetEnvir(svMain.SysMission_Map);
            if ((penv != null) && (count > 0) && (xx > 0) && (yy > 0))
            {
                for (i = 0; i < count; i++)
                {
                    cret = svMain.UserEngine.AddCreatureSysop(svMain.SysMission_Map, xx, yy, monname);
                    if ((cret != null) && svMain.BoSysHasMission)
                    {
                        cret.BoHasMission = true;
                        cret.Mission_X = svMain.SysMission_X;
                        cret.Mission_Y = svMain.SysMission_Y;
                    }
                }
                this.SysMsg(svMain.SysMission_Map + " " + xx.ToString() + ":" + yy.ToString() + " => " + monname + " " + count.ToString() + " heads", 1);
            }
            else
            {
                this.SysMsg("command error: X Y MobName Number", 0);
            }
        }

        public void CmdMakeItem(string itmname, int count)
        {
            int i;
            TUserItem pu;
            TStdItem pstd;
            int Num;
            // 茄锅俊 父甸 荐 乐绰 酒捞袍 俺荐 力茄.
            if (count > Grobal2.MAX_OVERLAPITEM)
            {
                return;
            }
            for (i = 0; i < count; i++)
            {
                if (this.ItemList.Count >= Grobal2.MAXBAGITEM)
                {
                    break;
                }
                pu = new TUserItem();
                if (svMain.UserEngine.CopyToUserItemFromName(itmname, ref pu))
                {
                    pstd = svMain.UserEngine.GetStdItem(pu.Index);
                    if (pstd != null)
                    {
                        if (pstd.Price >= 15000)
                        {
                            // 啊拜捞 15000盔 捞惑篮 superadmin父 父甸 荐 乐促.
                            if (!svMain.BoTestServer && (this.UserDegree < Grobal2.UD_SUPERADMIN))
                            {
                                Dispose(pu);
                                return;
                            }
                        }
                        // pu.Dura := Round((pu.Dura / 100) * (100 + Random(100)));
                        if (new System.Random(10).Next() == 0)
                        {
                            svMain.UserEngine.RandomUpgradeItem(pu);
                        }
                        // 固瘤 矫府令 酒捞袍牢 版快
                        if (new ArrayList(new int[] { 15, 19, 20, 21, 22, 23, 24, 26, 52, 53, 54 }).Contains(pstd.StdMode))
                        {
                            if ((pstd.Shape == ObjBase.RING_OF_UNKNOWN) || (pstd.Shape == ObjBase.BRACELET_OF_UNKNOWN) || (pstd.Shape == ObjBase.HELMET_OF_UNKNOWN))
                            {
                                svMain.UserEngine.RandomSetUnknownItem(pu);
                            }
                        }
                        // 檬措厘阑 父靛绰 版快(sonmg)
                        if (pstd.StdMode == 8)
                        {
                            if (pstd.Shape == ObjBase.SHAPE_OF_INVITATION)
                            {
                                if (this.GuildAgitInvitationItemSet(pu) == false)
                                {
                                    this.SysMsg("在你的门派庄园你可以得到一个邀请函。", 0);
                                    Dispose(pu);
                                    return;
                                }
                            }
                        }
                        // 惑泅林赣聪(DecoItem)甫 父靛绰 版快(sonmg)
                        if ((pstd.StdMode == ObjBase.STDMODE_OF_DECOITEM) && (pstd.Shape == ObjBase.SHAPE_OF_DECOITEM))
                        {
                            // 烙矫肺 汲沥.
                            Num = count;
                            // Random(16);   //烙矫
                            if (this.GuildAgitDecoItemSet(pu, Num) == false)
                            {
                                // SysMsg('殿废等 厘盔俊辑父 酒捞袍阑 掘阑 荐 乐嚼聪促.', 0);
                                Dispose(pu);
                                return;
                            }
                        }
                        // gadget:墨款飘酒捞袍
                        if (pstd.OverlapItem >= 1)
                        {
                            pu.Dura = (ushort)count;
                            this.ItemList.Add(pu);
                            SendAddItem(pu);
                        }
                        else
                        {
                            // 堡籍 鉴档 炼例
                            if (pstd.StdMode == 43)
                            {
                                pu.Dura = this.GetPurity();
                            }
                            this.ItemList.Add(pu);
                            SendAddItem(pu);
                        }
                        if (this.BoEcho)
                        {
                            svMain.MainOutMessage("MakeItem] " + this.UserName + " : " + itmname + " " + pu.MakeIndex.ToString());
                            // 肺弊巢辫
                            // 款父_
                            svMain.AddUserLog("5\09" + this.MapName + "\09" + this.CX.ToString() + "\09" + this.CY.ToString() + "\09" + this.UserName + "\09" + svMain.UserEngine.GetStdItemName(pu.Index) + "\09" + pu.MakeIndex.ToString() + "\09" + "1\09" + "0");
                        }
                        // 墨款飘酒捞袍篮 1锅父 父电促.
                        if (pstd.OverlapItem >= 1)
                        {
                            break;
                        }
                        // gadget:墨款飘酒捞袍
                        // 惑泅林赣聪档 1锅父 父电促.
                        if ((pstd.StdMode == ObjBase.STDMODE_OF_DECOITEM) && (pstd.Shape == ObjBase.SHAPE_OF_DECOITEM))
                        {
                            break;
                        }
                    }
                }
                else
                {
                    Dispose(pu);
                    break;
                }
            }
            this.WeightChanged();
        }

        public void CmdRefineWeapon(int dc, int mc, int sc, int acc)
        {
            if (dc + mc + sc > 10)
            {
                return;
            }
            if (this.UseItems[Grobal2.U_WEAPON].Index > 0)
            {
                this.UseItems[Grobal2.U_WEAPON].Desc[0] = (byte)dc;
                this.UseItems[Grobal2.U_WEAPON].Desc[1] = (byte)mc;
                this.UseItems[Grobal2.U_WEAPON].Desc[2] = (byte)sc;
                this.UseItems[Grobal2.U_WEAPON].Desc[5] = (byte)acc;
                SendUpdateItem(this.UseItems[Grobal2.U_WEAPON]);
                this.RecalcAbilitys();
                this.SendMsg(this, Grobal2.RM_ABILITY, 0, 0, 0, 0, "");
                this.SendMsg(this, Grobal2.RM_SUBABILITY, 0, 0, 0, 0, "");
            }
        }

        public void CmdDeleteUserGold(string whostr, string goldstr)
        {
            TUserHuman hum;
            int igold;
            int svidx=0;
            hum = svMain.UserEngine.GetUserHuman(whostr);
            igold = HUtil32.Str_ToInt(goldstr, 0);
            if (igold <= 0)
            {
                return;
            }
            if (hum != null)
            {
                if (hum.Gold > igold)
                {
                    // hum.Gold := hum.Gold - igold
                    hum.DecGold(igold);
                }
                else
                {
                    igold = hum.Gold;
                    // 角力肺 荤扼柳剧
                    hum.Gold = 0;
                }
                hum.GoldChanged();
                this.SysMsg(whostr + " Gold of " + igold.ToString() + " Gold was deleted.", 1);
                // 肺弊巢辫
                // 捣昏_
                // '陛傈'
                svMain.AddUserLog("13\09" + this.MapName + "\09" + this.CX.ToString() + "\09" + this.CY.ToString() + "\09" + this.UserName + "\09" + Envir.NAME_OF_GOLD + "\09" + igold.ToString() + "\09" + "1\09" + whostr);
            }
            else
            {
                if (svMain.UserEngine.FindOtherServerUser(whostr, ref svidx))
                {
                    this.SysMsg(whostr + " is " + svidx.ToString() + " no., he(she) is connected to that server.", 1);
                }
                else
                {
                    svMain.FrontEngine.ChangeUserInfos(this.UserName, whostr, -igold);
                }
                // SysMsg (whostr + ' Gold of ' + IntToStr(igold) + ' gold was deleted. (excecute command of  DelGold)', 1);
            }
        }

        public void CmdAddUserGold(string whostr, string goldstr)
        {
            TUserHuman hum;
            int igold;
            int svidx=0;
            hum = svMain.UserEngine.GetUserHuman(whostr);
            igold = HUtil32.Str_ToInt(goldstr, 0);
            if (igold <= 0)
            {
                return;
            }
            if (hum != null)
            {
                if (hum.Gold + igold < this.AvailableGold)
                {
                    // hum.Gold := hum.Gold + igold
                    hum.IncGold(igold);
                }
                else
                {
                    igold = this.AvailableGold - hum.Gold;
                    // 角力肺 荤扼柳剧
                    hum.Gold = this.AvailableGold;
                }
                hum.GoldChanged();
                this.SysMsg(whostr + "增加" + igold.ToString() + "金币。", 1);
                // 肺弊巢辫
                // 捣眠_
                // '陛傈'
                svMain.AddUserLog("14\09" + this.MapName + "\09" + this.CX.ToString() + "\09" + this.CY.ToString() + "\09" + this.UserName + "\09" + Envir.NAME_OF_GOLD + "\09" + igold.ToString() + "\09" + "1\09" + whostr);
            }
            else
            {
                if (svMain.UserEngine.FindOtherServerUser(whostr, ref svidx))
                {
                    this.SysMsg(whostr + " is " + svidx.ToString() + " no., he(she) is connected to that server.", 1);
                }
                else
                {
                    svMain.FrontEngine.ChangeUserInfos(this.UserName, whostr, igold);
                }
                // SysMsg ('  is not found.', 0);
            }
        }

        public void RCmdUserChangeGoldOk(string whostr, int igold)
        {
            string cmdstr;
            string msgstr;
            if (igold > 0)
            {
                cmdstr = "14\09";
                // 捣眠_;
                msgstr = "Added";
            }
            else
            {
                cmdstr = "13\09";
                // 捣昏_;
                msgstr = "Deleted";
                igold = -igold;
            }
            this.SysMsg(whostr + " Gold of " + igold.ToString() + "Gold " + msgstr, 1);
            // 肺弊 巢辫
            // '陛傈'
            svMain.AddUserLog(cmdstr + this.MapName + "\09" + this.CX.ToString() + "\09" + this.CY.ToString() + "\09" + this.UserName + "\09" + Envir.NAME_OF_GOLD + "\09" + igold.ToString() + "\09" + "1\09" + whostr);
        }

        public void CmdFreeSpaceMove(string map, string xstr, string ystr)
        {
            int x;
            int y;
            TEnvirnoment pev;
            pev = svMain.GrobalEnvir.GetEnvir(map);
            if (pev != null)
            {
                x = HUtil32.Str_ToInt(xstr, 0);
                y = HUtil32.Str_ToInt(ystr, 0);
                if (pev.CanWalk(x, y, true))
                {
                    this.SpaceMove(map, (short)x, (short)y, 0);
                }
                else
                {
                    this.SysMsg("失败", 0);
                }
            }
            else
            {
                this.SysMsg("失败", 0);
            }
        }

        public void CmdCharSpaceMove(string CharName_)
        {
            TUserHuman hum;
            int svidx=0;
            hum = svMain.UserEngine.GetUserHuman(CharName_);
            if (hum != null)
            {
                this.SpaceMove(hum.PEnvir.MapName, hum.CX, (short)(hum.CY + 1), 0);
            }
            else
            {
                if (svMain.UserEngine.FindOtherServerUser(CharName_, ref svidx))
                {
                    svMain.UserEngine.SendInterMsg(Grobal2.ISM_REQUEST_RECALL, svidx, CharName_ + "/" + this.UserName);
                }
                else
                {
                    this.SysMsg("无法找到" + CharName_, 0);
                }
            }
        }

        public bool CmdLoverCharSpaceMove(string CharName_)
        {
            bool result;
            TUserHuman hum;
            int svidx=0;
            result = false;
            hum = svMain.UserEngine.GetUserHuman(CharName_);
            if (hum != null)
            {
                // 楷牢捞 NoRecall 瘤开俊 乐栏搁 哎 荐 绝澜.
                if (!hum.PEnvir.NoRecall)
                {
                    this.SpaceMove(hum.PEnvir.MapName, hum.CX, (short)(hum.CY + 1), 0);
                    result = true;
                }
            }
            else
            {
                if (svMain.UserEngine.FindOtherServerUser(CharName_, ref svidx))
                {
                    svMain.UserEngine.SendInterMsg(Grobal2.ISM_REQUEST_LOVERRECALL, svidx, CharName_ + "/" + this.UserName);
                }
                else
                {
                    this.SysMsg("无法找到" + CharName_, 0);
                }
            }
            return result;
        }

        public void CmdBreakLoverRelation()
        {
            int svidx=0;
            int ReqType;
            string OtherName;
            TUserHuman hum;
            string strPayment;
            // 困磊丰 尘 捣捞 乐绰瘤 犬牢
            if (this.Gold < ObjBase.COMPENSATORY_PAYMENT_ONEWAY)
            {
                strPayment = (ObjBase.COMPENSATORY_PAYMENT_ONEWAY / 10000).ToString();
                this.BoxMsg("要打破情侣关系，你需要" + strPayment + "0,000金币。", 0);
                return;
            }
            if (fLover == null)
            {
                return;
            }
            OtherName = fLover.GetLoverName;
            if (OtherName == "")
            {
                return;
            }
            ReqType = Grobal2.RsState_Lover;
            // 楷牢 老规 秦力(2004/12/13)
            if (RelationShipDeleteOther(ReqType, OtherName))
            {
                // 困磊丰 瘤阂
                if (this.Gold >= ObjBase.COMPENSATORY_PAYMENT_ONEWAY)
                {
                    this.DecGold(ObjBase.COMPENSATORY_PAYMENT_ONEWAY);
                    this.GoldChanged();
                }
                // 惑怕 函版(敌拳)
                this.MakePoison(Grobal2.POISON_SLOW, 3, 1);
                // HP, MP 函版(10%)
                this.WAbil.HP = (ushort)_MAX(1, this.WAbil.HP / 10);
                this.WAbil.MP = (ushort)_MAX(1, this.WAbil.MP / 10);
                // 面拜 皋矫瘤
                this.SysMsg("情侣关系破裂了，将造成自身的冲击。", 0);
                hum = svMain.UserEngine.GetUserHuman(OtherName);
                if (hum != null)
                {
                    hum.RelationShipDeleteOther(ReqType, this.UserName);
                    // 肺弊巢辫
                    // 楷牢_
                    // 老规秦力:1
                    svMain.AddUserLog("47\09" + this.MapName + "\09" + this.CX.ToString() + "\09" + this.CY.ToString() + "\09" + this.UserName + "\09" + "0\09" + "0\09" + "1\09" + OtherName);
                    // /////////////////////////////////////////////////////////
                    // 老规利牢 秦力牢 版快俊绰 惑措俊霸 面拜捞 傈秦瘤瘤 臼澜.
                    // /////////////////////////////////////////////////////////
                }
                else
                {
                    if (svMain.UserEngine.FindOtherServerUser(OtherName, ref svidx))
                    {
                        svMain.UserEngine.SendInterMsg(Grobal2.ISM_LM_DELETE, svidx, OtherName + "/" + this.UserName + "/" + ReqType.ToString());
                    }
                }
            }
            else
            {
                SendDefMessage(Grobal2.SM_LM_RESULT, ReqType, Grobal2.RsError_DontDelete, 0, 0, OtherName);
            }
        }

        public void CmdStealth()
        {
            this.bStealth = !this.bStealth;
            if (this.bStealth)
            {
                this.SysMsg("隐身开启", 0);
            }
            else
            {
                this.SysMsg("隐身关闭", 0);
            }
        }

        public void CmdCharMove(string CharName_, string MapName)
        {
            TUserHuman hum;
            hum = svMain.UserEngine.GetUserHuman(CharName_);
            if (hum != null)
            {
                if (svMain.GrobalEnvir.GetEnvir(MapName) != null)
                {
                    hum.SendRefMsg(Grobal2.RM_SPACEMOVE_HIDE, 0, 0, 0, 0, "");
                    hum.RandomSpaceMove(MapName, 0);
                    // 公累困 傍埃捞悼
                }
            }
            else
            {
                this.SysMsg("Cannot Find " + CharName_, 0);
            }
        }

        public void CmdRushAttack()
        {
            this.CharRushRush(this.Dir, 3, true);
        }

        public void CmdManLevelChange(string man, int level)
        {
            int oldlv;
            TUserHuman hum;
            hum = svMain.UserEngine.GetUserHuman(man);
            if (hum != null)
            {
                svMain.MainOutMessage("ChgLv] " + man + " : " + hum.Abil.Level.ToString() + " -> " + level.ToString() + " by " + this.UserName);
                oldlv = hum.Abil.Level;
                hum.ChangeLevel(level);
                hum.HasLevelUp(oldlv);
                // 肺弊甫 巢变促
                // 鸥饭_
                svMain.AddUserLog("17\09" + man + "\09" + oldlv.ToString() + "\09" + level.ToString() + "\09" + this.UserName + "\09" + "0\09" + "0\09" + "1\09" + "0");
                this.SysMsg("[AdjustLevel] " + man + " " + oldlv.ToString() + "->" + level.ToString(), 1);
            }
        }

        public void CmdManExpChange(string man, int exp)
        {
            TUserHuman hum;
            int oldexp;
            hum = svMain.UserEngine.GetUserHuman(man);
            if (hum != null)
            {
                svMain.MainOutMessage("ChgExp] " + man + " : " + hum.Abil.Exp.ToString() + " -> " + exp.ToString() + " by " + this.UserName);
                oldexp = hum.Abil.Exp;
                hum.Abil.Exp = exp;
                // ChangeLevel (level);
                hum.HasLevelUp(this.Abil.Level);
                // 肺弊甫 巢变促
                // 鸥版_
                svMain.AddUserLog("18\09" + man + "\09" + oldexp.ToString() + "\09" + exp.ToString() + "\09" + this.UserName + "\09" + "0\09" + "0\09" + "1\09" + "0");
                this.SysMsg("[AdjustExpriencePoint] " + man + " " + exp.ToString(), 1);
            }
        }

        public void CmdEraseItem(string itmname, string countstr)
        {
            int i;
            int k;
            TUserItem pu;
            int count = HUtil32.Str_ToInt(countstr, 1);
            for (k = 1; k <= count; k++)
            {
                for (i = 0; i < this.ItemList.Count; i++)
                {
                    pu = this.ItemList[i];
                    if (svMain.UserEngine.GetStdItemName(pu.Index).ToLower().CompareTo(itmname.ToLower()) == 0)
                    {
                        svMain.AddUserLog("6\09" + this.MapName + "\09" + this.CX.ToString() + "\09" + this.CY.ToString() + "\09" + this.UserName + "\09" + svMain.UserEngine.GetStdItemName(pu.Index) + "\09" + pu.MakeIndex.ToString() + "\09" + "1\09" + "0");
                        SendDelItem(pu);
                        Dispose(pu);
                        this.ItemList.RemoveAt(i);
                        break;
                    }
                }
            }
        }

        public void CmdRecallMan(string man, string map)
        {
            short nx = 0;
            short ny = 0;
            short dx = 0;
            short dy = 0;
            int svidx = 0;
            TUserHuman hum = svMain.UserEngine.GetUserHuman(man);
            if (hum != null)
            {
                if (map != "")
                {
                    if (hum.MapName != map)
                    {
                        return;
                    }
                }
                if (M2Share.GetFrontPosition(this, ref nx, ref ny))
                {
                    if (this.GetRecallPosition(nx, ny, 3, ref dx, ref dy))
                    {
                        hum.SendRefMsg(Grobal2.RM_SPACEMOVE_HIDE, 0, 0, 0, 0, "");
                        hum.UserSpaceMove(this.MapName, dx.ToString(), dy.ToString());
                    }
                }
                else
                {
                    this.SysMsg("Recall failed.", 0);
                }
            }
            else
            {
                if (svMain.UserEngine.FindOtherServerUser(man, ref svidx))
                {
                    if (M2Share.GetFrontPosition(this, ref nx, ref ny))
                    {
                        if (this.GetRecallPosition(nx, ny, 3, ref dx, ref dy))
                        {
                            svMain.UserEngine.SendInterMsg(Grobal2.ISM_RECALL, svidx, man + "/" + dx.ToString() + "/" + dy.ToString() + "/" + this.MapName);
                        }
                    }
                    else
                    {
                        this.SysMsg("Recall failed.", 0);
                    }
                }
                else
                {
                    this.SysMsg(man + "无法找到。", 0);
                }
            }
        }

        // 瘤沥茄 甘俊辑 公累困肺 10疙狼 蜡历甫 磊扁磊脚捞 乐绰 困摹肺 家券窃.
        public void CmdRecallMap(string MapFrom)
        {
            TUserHuman hum;
            short nx=0;
            short ny=0;
            short dx=0;
            short dy =0;
            int k;
            ArrayList list;
            TEnvirnoment envir;
            if (MapFrom == "")
            {
                return;
            }
            envir = svMain.GrobalEnvir.GetEnvir(MapFrom);
            if (envir != null)
            {
                list = new ArrayList();
                svMain.UserEngine.GetAreaUsers(envir, 0, 0, 1000, list);
                for (k = 0; k < list.Count; k++)
                {
                    hum = list[k] as TUserHuman;
                    if (hum != null)
                    {
                        if (M2Share.GetFrontPosition(this, ref nx, ref ny))
                        {
                            if (this.GetRecallPosition(nx, ny, 3, ref dx, ref dy))
                            {
                                hum.SendRefMsg(Grobal2.RM_SPACEMOVE_HIDE, 0, 0, 0, 0, "");
                                hum.UserSpaceMove(this.MapName, dx.ToString(), dy.ToString());
                            }
                        }
                    }
                    if (k >= (10 - 1))
                    {
                        break;
                    }
                }
                list.Free();
            }
        }

        public void GuildMasterRecallMan(string man, bool bPersonal)
        {
            TUserHuman hum;
            short nx = 0;
            short ny = 0;
            short dx = 0;
            short dy = 0;
            int svidx = 0;
            TGuild guild;
            if (man == "")
            {
                return;
            }
            guild = svMain.GuildMan.GetGuildFromMemberName(man);
            if (this.MyGuild == guild)
            {
                hum = svMain.UserEngine.GetUserHuman(man);
                if (hum != null)
                {
                    if (hum.BoEnableAgitRecall)
                    {
                        if (M2Share.GetFrontPosition(this, ref nx, ref ny))
                        {
                            if (this.GetRecallPosition(nx, ny, 3, ref dx, ref dy))
                            {
                                hum.SendRefMsg(Grobal2.RM_SPACEMOVE_HIDE, 0, 0, 0, 0, "");
                                hum.UserSpaceMove(this.MapName, dx.ToString(), dy.ToString());
                            }
                        }
                        else
                        {
                            this.SysMsg("Recall failed.", 0);
                        }
                    }
                    else
                    {
                        if (bPersonal)
                        {
                            this.SysMsg(man + " is now rejecting guild master\'s Recall.", 0);
                        }
                    }
                }
                else
                {
                    if (M2Share.GetFrontPosition(this, ref nx, ref ny))
                    {
                        if (this.GetRecallPosition(nx, ny, 3, ref dx, ref dy))
                        {
                            if (svMain.UserEngine.FindOtherServerUser(man, ref svidx))
                            {
                                svMain.UserEngine.SendInterMsg(Grobal2.ISM_GUILDMEMBER_RECALL, svidx, man + "/" + dx.ToString() + "/" + dy.ToString() + "/" + this.MapName);
                            }
                            else if (bPersonal)
                            {
                                this.SysMsg(man + " is not found.", 0);
                            }
                        }
                    }
                }
            }
            else
            {
                this.SysMsg("The man is not member of your guild.", 0);
            }
        }

        public void CmdReconnection(string saddr, string sport)
        {
            if ((saddr != "") && (sport != ""))
            {
                this.SendMsg(this, Grobal2.RM_RECONNECT, 0, 0, 0, 0, saddr + "/" + sport);
            }
        }

        public void CmdReloadGuild(string gname)
        {
            TGuild g;
            if (svMain.ServerIndex == 0)
            {
                g = svMain.GuildMan.GetGuild(gname);
                if (g != null)
                {
                    g.LoadGuild();
                    this.SysMsg(gname + " : Update has been done in Guild ", 0);
                    svMain.UserEngine.SendInterMsg(Grobal2.ISM_RELOADGUILD, svMain.ServerIndex, gname);
                }
            }
            else
            {
                this.SysMsg("这个命令只能在主服务器上使用。", 0);
            }
        }

        public void CmdReloadGuildAll(string gname)
        {
            svMain.GuildMan.ClearGuildList();
            svMain.GuildMan.LoadGuildList();
            this.SysMsg("Read all information of Guild.", 1);
        }

        public void CmdReloadGuildAgit()
        {
            svMain.GuildAgitMan.ClearGuildAgitList();
            svMain.GuildAgitMan.LoadGuildAgitList();
            svMain.UserEngine.SendInterMsg(Grobal2.ISM_RELOADGUILDAGIT, svMain.ServerIndex, "");
            svMain.GuildAgitBoardMan.LoadAllGaBoardList("");
            this.SysMsg("Reloaded the GuildAgitList.", 1);
        }

        public void CmdKickUser(string uname)
        {
            TUserHuman hum;
            hum = svMain.UserEngine.GetUserHuman(uname);
            if (hum != null)
            {
                hum.UserRequestClose = true;
            }
        }

        public void CmdTingUser(string uname)
        {
            TUserHuman hum;
            hum = svMain.UserEngine.GetUserHuman(uname);
            if (hum != null)
            {
                // hum.UserRequestClose := TRUE;
                hum.RandomSpaceMove(hum.HomeMap, 0);
            }
            else
            {
                this.SysMsg(uname + "无法找到。", 0);
            }
        }

        public void CmdTingRangeUser(string uname, string rangestr)
        {
            int i;
            int range;
            TUserHuman hum;
            ArrayList ulist;
            hum = svMain.UserEngine.GetUserHuman(uname);
            range = _MIN(HUtil32.Str_ToInt(rangestr, 2), 10);
            if (hum != null)
            {
                ulist = new ArrayList();
                svMain.UserEngine.GetAreaUsers(hum.PEnvir, hum.CX, hum.CY, range, ulist);
                for (i = 0; i < ulist.Count; i++)
                {
                    hum = ulist[i] as TUserHuman;
                    hum.RandomSpaceMove(hum.HomeMap, 0);
                }
                ulist.Free();
            }
            else
            {
                this.SysMsg(uname + "无法找到。", 0);
            }
        }

        // 傍烹 疙飞绢
        public void CmdEraseMagic(string magname)
        {
            int i;
            for (i = this.MagicList.Count - 1; i >= 0; i--)
            {
                if (((TUserMagic)this.MagicList[i]).pDef.MagicName.ToLower().CompareTo(magname.ToLower()) == 0)
                {
                    SendDelMagic((TUserMagic)this.MagicList[i]);
                    Dispose((TUserMagic)this.MagicList[i]);
                    this.MagicList.RemoveAt(i);
                    break;
                }
            }
            this.RecalcAbilitys();
        }

        public void CmdThisManEraseMagic(string whostr, string magname)
        {
            TUserHuman hum;
            hum = svMain.UserEngine.GetUserHuman(whostr);
            if (hum != null)
            {
                hum.CmdEraseMagic(magname);
            }
            else
            {
                this.SysMsg(whostr + "无法找到。", 0);
            }
        }

        public bool GuildDeclareWar(string gname)
        {
            bool result;
            TGuild guild;
            TGuildWarInfo pgw;
            bool flag;
            int BackResult;
            string kindstr;
            long currenttime;
            result = false;
            pgw = null;
            BackResult = 0;
            kindstr = "";
            if (this.IsGuildMaster())
            {
                // 巩林父 荤侩且 荐 乐绰 疙飞
                if (svMain.ServerIndex != 0)
                {
                    this.SysMsg("此命令在此服务器上不可用。", 0);
                    return result;
                }
                guild = svMain.GuildMan.GetGuild(gname);
                if (guild != null)
                {
                    flag = false;
                    // 磊脚狼 巩颇客绰 巩颇傈阑 且 荐 绝澜(sonmg 2005/08/17)
                    if (guild == ((TGuild)this.MyGuild))
                    {
                        this.SysMsg("失败，这是你自己的门派。", 0);
                        return result;
                    }
                    currenttime  =  HUtil32.GetTickCount();
                    // 巩颇埃 矫埃 烹老
                    pgw = ((TGuild)this.MyGuild).DeclareGuildWar(guild, currenttime, ref BackResult);
                    if (BackResult == 0)
                    {
                        return result;
                    }
                    if (pgw != null)
                    {
                        if (guild.DeclareGuildWar((TGuild)this.MyGuild, currenttime, ref BackResult) == null)
                        {
                            pgw.WarStartTime = 0;
                            // 鸥烙酒眶
                        }
                        else
                        {
                            if (BackResult > 0)
                            {
                                flag = true;
                                if (BackResult == 1)
                                {
                                    kindstr = "Req.GWar";
                                }
                                else if (BackResult == 2)
                                {
                                    kindstr = "Ext.GWar";
                                }
                                else
                                {
                                    kindstr = "GWar";
                                }
                                // 肺弊巢辫
                                // 巩傈_   //LastLogNumber
                                svMain.AddUserLog("49\09" + this.MapName + "\09" + this.CX.ToString() + "\09" + this.CY.ToString() + "\09" + this.UserName + "\09" + kindstr + "\09" + "0" + "\09" + "1\09" + kindstr);
                            }
                        }
                    }
                    if (flag)
                    {
                        // 己傍
                        svMain.UserEngine.SendInterMsg(Grobal2.ISM_RELOADGUILD, svMain.ServerIndex, ((TGuild)this.MyGuild).GuildName);
                        svMain.UserEngine.SendInterMsg(Grobal2.ISM_RELOADGUILD, svMain.ServerIndex, gname);
                        result = true;
                    }
                }
                else
                {
                    this.SysMsg(gname + "是不是存在的门派。", 0);
                }
            }
            else
            {
                this.SysMsg("只有门派门主才能使用。", 0);
            }
            return result;
        }

        public void CmdCreateGuild(string gname, string mastername)
        {
            TUserHuman hum;
            bool flag;
            if (svMain.ServerIndex != 0)
            {
                this.SysMsg("这个命令只能在主服务器上使用。", 0);
                return;
            }
            flag = false;
            hum = svMain.UserEngine.GetUserHuman(mastername);
            if (hum != null)
            {
                if (svMain.GuildMan.GetGuildFromMemberName(mastername) == null)
                {
                    if (svMain.GuildMan.AddGuild(gname, mastername))
                    {
                        svMain.UserEngine.SendInterMsg(Grobal2.ISM_ADDGUILD, svMain.ServerIndex, gname + "/" + mastername);
                        this.SysMsg("增加门派 " + gname + " " + "领袖" + "：" + mastername, 0);
                        flag = true;
                    }
                }
                // 巩颇沥焊甫 促矫 佬绰促.
                hum.MyGuild = svMain.GuildMan.GetGuildFromMemberName(hum.UserName);
                if (hum.MyGuild != null)
                {
                    // 辨靛俊 啊涝登绢 乐绰 版快
                    hum.GuildRankName = ((TGuild)hum.MyGuild).MemberLogin(this, ref hum.GuildRank);
                    // SendMsg (self, RM_CHANGEGUILDNAME, 0, 0, 0, 0, '');
                }
            }
            if (!flag)
            {
                this.SysMsg("新门派创建失败。", 0);
            }
        }

        public void CmdDeleteGuild(string gname)
        {
            if (svMain.ServerIndex != 0)
            {
                this.SysMsg("这个命令只能在主服务器上使用。", 0);
                return;
            }
            // 厘盔 馆券 饶 巩颇昏力.
            // GuildAgitMan.DelGuildAgit( gname );
            if (svMain.GuildMan.DelGuild(gname))
            {
                svMain.UserEngine.SendInterMsg(Grobal2.ISM_DELGUILD, svMain.ServerIndex, gname);
                this.SysMsg("删除门派" + gname, 0);
            }
            else
            {
                this.SysMsg("门派删除失败。", 0);
            }
        }

        // 巩颇 措傈狼 痢荐甫 舅妨霖促.
        public void CmdGetGuildMatchPoint(string gname)
        {
            TGuild guild;
            guild = svMain.GuildMan.GetGuild(gname);
            if (guild != null)
            {
                this.SysMsg(gname + "\'点数：" + guild.MatchPoint.ToString(), 1);
            }
            else
            {
                this.SysMsg(gname + "不是有效的门派名字。", 0);
            }
        }

        // 巩颇措傈阑 困秦辑 函荐甫 檬扁拳
        public void CmdStartGuildMatch()
        {
            int i;
            int k;
            ArrayList ulist;
            ArrayList glist;
            TUserHuman hum;
            bool flag;
            string str;
            if (this.PEnvir.Fight3Zone)
            {
                ulist = new ArrayList();
                glist = new ArrayList();
                svMain.UserEngine.GetAreaUsers(this.PEnvir, this.CX, this.CY, 1000, ulist);
                // 泅甘狼 葛电 荤恩
                for (i = 0; i < ulist.Count; i++)
                {
                    hum = ulist[i] as TUserHuman;
                    if (!hum.BoSuperviserMode && !hum.BoSysopMode)
                    {
                        // 款康磊葛靛肺 乐绰 荤恩篮 痢荐俊霸 力寇
                        hum.FightZoneDieCount = 0;
                        // 磷篮 墨款飘 檬扁拳
                        if (hum.MyGuild != null)
                        {
                            flag = false;
                            for (k = 0; k < glist.Count; k++)
                            {
                                if (glist[k] == hum.MyGuild)
                                {
                                    flag = true;
                                    break;
                                }
                            }
                            if (!flag)
                            {
                                glist.Add(hum.MyGuild);
                            }
                        }
                    }
                }
                this.SysMsg("Guild war started .", 1);
                svMain.UserEngine.CryCry(Grobal2.RM_CRY, this.PEnvir, this.CX, this.CY, 10000, " -门派战争开始。");
                str = "";
                for (i = 0; i < glist.Count; i++)
                {
                    ((TGuild)glist[i]).TeamFightStart();
                    // 巩颇措傈函荐檬扁拳, 痢荐, 糕滚
                    for (k = 0; k < ulist.Count; k++)
                    {
                        hum = ulist[k] as TUserHuman;
                        if (hum.MyGuild == glist[i])
                        {
                            ((TGuild)glist[i]).TeamFightAdd(hum.UserName);
                            // 巩颇措傈糕滚 磊悼 眠啊
                        }
                    }
                    str = str + ((TGuild)glist[i]).GuildName + " ";
                }
                svMain.UserEngine.CryCry(Grobal2.RM_CRY, this.PEnvir, this.CX, this.CY, 10000, " -在门派战中的门派：" + str);
                ulist.Free();
                glist.Free();
            }
            else
            {
                this.SysMsg("此命令无法在这个地图上使用。", 0);
            }
        }

        // 巩颇措傈 辆丰(场)
        public void CmdEndGuildMatch()
        {
            int i;
            int k;
            ArrayList ulist;
            ArrayList glist;
            TUserHuman hum;
            bool flag;
            if (this.PEnvir.Fight3Zone)
            {
                ulist = new ArrayList();
                glist = new ArrayList();
                svMain.UserEngine.GetAreaUsers(this.PEnvir, this.CX, this.CY, 1000, ulist);
                // 泅甘狼 葛电 荤恩
                for (i = 0; i < ulist.Count; i++)
                {
                    hum = ulist[i] as TUserHuman;
                    if (!hum.BoSuperviserMode && !hum.BoSysopMode)
                    {
                        // 款康磊葛靛肺 乐绰 荤恩篮 痢荐俊霸 力寇
                        if (hum.MyGuild != null)
                        {
                            flag = false;
                            for (k = 0; k < glist.Count; k++)
                            {
                                if (glist[k] == hum.MyGuild)
                                {
                                    flag = true;
                                    break;
                                }
                            }
                            if (!flag)
                            {
                                glist.Add(hum.MyGuild);
                            }
                        }
                    }
                }
                for (i = 0; i < glist.Count; i++)
                {
                    ((TGuild)glist[i]).TeamFightEnd();
                    svMain.UserEngine.CryCry(Grobal2.RM_CRY, this.PEnvir, this.CX, this.CY, 10000, " -" + ((TGuild)glist[i]).GuildName + "门派战争结束。");
                }
                ulist.Free();
                glist.Free();
            }
        }

        public void CmdAnnounceGuildMembersMatchPoint(string gname)
        {
            int i;
            int n;
            TGuild guild;
            if (this.PEnvir.Fight3Zone)
            {
                guild = svMain.GuildMan.GetGuild(gname);
                if (guild != null)
                {
                    svMain.UserEngine.CryCry(Grobal2.RM_CRY, this.PEnvir, this.CX, this.CY, 10000, " -" + gname + "门派战争点数公布");
                    for (i = 0; i < guild.FightMemberList.Count; i++)
                    {
                        n = (int)guild.FightMemberList.Values[i];
                        // HUtil32.HiWord: 掘篮痢荐
                        svMain.UserEngine.CryCry(Grobal2.RM_CRY, this.PEnvir, this.CX, this.CY, 10000, " -" + guild.FightMemberList[i] + "：" + HUtil32.HiWord(n).ToString() + " 点 / " + HUtil32.LoWord(n).ToString() + " 死亡");
                        // HUtil32.LoWord: 磷篮冉荐
                    }
                    svMain.UserEngine.CryCry(Grobal2.RM_CRY, this.PEnvir, this.CX, this.CY, 10000, " -[" + guild.GuildName + "] " + guild.MatchPoint.ToString());
                    svMain.UserEngine.CryCry(Grobal2.RM_CRY, this.PEnvir, this.CX, this.CY, 10000, "------------------------------------");
                }
            }
            else
            {
                this.SysMsg("此命令无法在这个地图上使用。", 0);
            }
        }

        // 鞍篮 甘俊 乐绰 葛电 某腐狼 府胶飘甫 犬牢茄促.
        public void CmdViewAllCharacterList(string mapname)
        {
            ArrayList userlist;
            int i;
            int listcount;
            TCreature TempUser;
            TEnvirnoment env;
            env = null;
            if (mapname != "")
            {
                env = svMain.GrobalEnvir.GetEnvir(mapname);
            }
            else
            {
                env = this.PEnvir;
            }
            userlist = new ArrayList();
            svMain.UserEngine.GetAreaAllUsers(env, userlist);
            listcount = 0;
            for (i = 0; i < userlist.Count; i++)
            {
                TempUser = userlist[i] as TCreature;
                // 唱甫 力寇茄 荤恩狼 某腐疙阑 免仿茄促.
                if (TempUser.RaceServer == Grobal2.RC_USERHUMAN)
                {
                    if (this.UserName != TempUser.UserName)
                    {
                        this.NilMsg(TempUser.UserName);
                        listcount++;
                    }
                }
            }
            this.NilMsg("**Human( " + listcount.ToString() + " )**");
            userlist.Clear();
            userlist.Free();
        }

        public string GetLevelInfoString(TCreature cret)
        {
            string result;
            // 2003/03/04 眠啊何盒
            result = cret.UserName + " Map" + cret.MapName + " X" + cret.CX.ToString() + " Y" + cret.CY.ToString() + " Lv" + cret.Abil.Level.ToString() + " Exp" + cret.Abil.Exp.ToString() + " HP" + cret.WAbil.HP.ToString() + "/" + cret.WAbil.MaxHP.ToString() + " MP" + cret.WAbil.MP.ToString() + "/" + cret.WAbil.MaxMP.ToString() + " DC" + Lobyte(cret.WAbil.DC).ToString() + "-" + HiByte(cret.WAbil.DC).ToString() + " MC" + Lobyte(cret.WAbil.MC).ToString() + "-" + HiByte(cret.WAbil.MC).ToString() + " SC" + Lobyte(cret.WAbil.SC).ToString() + "-" + HiByte(cret.WAbil.SC).ToString() + " AC" + Lobyte(cret.WAbil.AC).ToString() + "-" + HiByte(cret.WAbil.AC).ToString() + " MAC" + Lobyte(cret.WAbil.MAC).ToString() + "-" + HiByte(cret.WAbil.MAC).ToString() + " Hit" + cret.AccuracyPoint.ToString() + " Spd" + cret.SpeedPoint.ToString() + " HitSpeed" + cret.HitSpeed.ToString() + " Holy" + cret.AddAbil.UndeadPower.ToString();
            return result;
        }

        public void CmdSendUserLevelInfos(string whostr)
        {
            TUserHuman hum;
            hum = svMain.UserEngine.GetUserHuman(whostr);
            if (hum != null)
            {
                this.SysMsg(GetLevelInfoString(hum), 1);
            }
            else
            {
                this.SysMsg(whostr + "无法找到。", 0);
            }
        }

        public void CmdSendMonsterLevelInfos()
        {
            int i;
            ArrayList list;
            TCreature cret;
            list = new ArrayList();
            this.PEnvir.GetCreatureInRange(this.CX, this.CY, 2, true, list);
            for (i = 0; i < list.Count; i++)
            {
                cret = list[i] as TCreature;
                this.SysMsg(GetLevelInfoString(cret), 1);
            }
            list.Free();
        }

        // //////////////////////////////////////////////////
        // 空各狼 沥焊甫 焊郴绰 疙飞. sonmg(2004/02/06)
        // //////////////////////////////////////////////////
        public void CmdSendKingMonsterInfos(string monname)
        {
            TCreature cret;
            int ix;
            int iy;
            try
            {
                // 泅犁 甘 傈眉 谅钎甫 八祸窍咯 阿 谅钎俊 乐绰 霉(GetCreature) 阁胶磐 吝俊
                // 饭骇捞 60 捞惑牢 阁胶磐俊 措茄 沥焊甫 焊晨.
                for (ix = 0; ix < this.PEnvir.MapWidth; ix++)
                {
                    for (iy = 0; iy < this.PEnvir.MapHeight; iy++)
                    {
                        cret = this.PEnvir.GetCreature(ix, iy, true) as TCreature;
                        if (cret != null)
                        {
                            if (monname == "")
                            {
                                if ((cret.Abil.Level >= 60) && (cret.RaceServer != Grobal2.RC_BAMTREE) && (cret.RaceServer != Grobal2.RC_PBMSTONE1) && (cret.RaceServer != Grobal2.RC_PBMSTONE2) && (cret.RaceServer > Grobal2.RC_ANIMAL))
                                {
                                    this.SysMsg(GetLevelInfoString(cret), 1);
                                }
                            }
                            else
                            {
                                if (cret.UserName == monname)
                                {
                                    this.SysMsg(GetLevelInfoString(cret), 1);
                                }
                            }
                        }
                    }
                }
            }
            catch
            {
                svMain.MainOutMessage("[Exception]TUserHuman.CmdSendKingMonsterInfos");
            }
        }

        // sonmg(2004/02/06)
        public void CmdChangeUserCastleOwner(string gldname, bool pass)
        {
            TGuild guild;
            guild = svMain.GuildMan.GetGuild(gldname);
            if (guild != null)
            {
                // 肺弊巢辫
                // 荤合_ +
                svMain.AddUserLog("27\09" + svMain.UserCastle.OwnerGuildName + "\09" + "0\09" + "0\09" + gldname + "\09" + this.UserName + "\09" + "0\09" + "1\09" + "0");
                svMain.UserCastle.ChangeCastleOwner(guild);
                if (pass)
                {
                    svMain.UserEngine.SendInterMsg(Grobal2.ISM_CHANGECASTLEOWNER, svMain.ServerIndex, gldname);
                }
                this.SysMsg("狮北城所属门派修改为：" + gldname, 1);
            }
            else
            {
                this.SysMsg(gldname + "无法找到。", 0);
            }
        }

        public void CmdReloadNpc(string cmdstr)
        {
            int i;
            int n;
            ArrayList list;
            if (cmdstr.ToLower().CompareTo("all".ToLower()) == 0)
            {
                LocalDB.FrmDB.ReloadNpcs();
                // 眠啊等 npc, 昏力等 npc 利侩
                LocalDB.FrmDB.ReloadMerchants();
                n = 0;
                for (i = 0; i < svMain.UserEngine.NpcList.Count; i++)
                {
                    ((TNormNpc)svMain.UserEngine.NpcList[i]).ClearNpcInfos();
                    ((TNormNpc)svMain.UserEngine.NpcList[i]).LoadNpcInfos();
                    n++;
                }
                for (i = 0; i < svMain.UserEngine.MerchantList.Count; i++)
                {
                    ((TMerchant)svMain.UserEngine.MerchantList[i]).ClearMerchantInfos();
                    ((TMerchant)svMain.UserEngine.MerchantList[i]).LoadMerchantInfos();
                    n++;
                }
                this.SysMsg("Reload npc information is successful : " + n.ToString(), 1);
            }
            else
            {
                list = new ArrayList();
                svMain.UserEngine.GetNpcXY(this.PEnvir, this.CX, this.CY, 9, list);
                // 拳搁俊 焊捞绰 npc
                for (i = 0; i < list.Count; i++)
                {
                    ((TNormNpc)list[i]).ClearNpcInfos();
                    ((TNormNpc)list[i]).LoadNpcInfos();
                    this.SysMsg(((TNormNpc)list[i]).UserName + " is reloaded", 1);
                }
                list.Clear();
                svMain.UserEngine.GetMerchantXY(this.PEnvir, this.CX, this.CY, 9, list);
                // 拳搁俊 焊捞绰 npc
                for (i = 0; i < list.Count; i++)
                {
                    ((TMerchant)list[i]).ClearMerchantInfos();
                    ((TMerchant)list[i]).LoadMerchantInfos();
                    this.SysMsg(((TNormNpc)list[i]).UserName + " is reloaded", 1);
                }
                list.Free();
            }
        }

        public void CmdReloadDefaultNpc()
        {
            svMain.DefaultNpc.ClearNpcInfos();
            svMain.DefaultNpc.LoadNpcInfos();
            this.SysMsg("DefaultNpc reloaded.", 1);
        }

        public void CmdOpenCloseUserCastleMainDoor(string cmdstr)
        {
            if (this.IsGuildMaster() && (this.MyGuild == svMain.UserCastle.OwnerGuild))
            {
                if (cmdstr.ToLower().CompareTo("开启".ToLower()) == 0)
                {
                }
                if (cmdstr.ToLower().CompareTo("关闭".ToLower()) == 0)
                {
                }
            }
            else
            {
                this.SysMsg("只有狮北城城主才能使用这个命令。", 0);
            }
        }

        // pass : true(促弗 辑滚俊 傈崔窃, 林狼)
        public void CmdAddShutUpList(string whostr, string minstr, bool pass)
        {
            int idx;
            int amin;
            amin = HUtil32.Str_ToInt(minstr, 5);
            if (whostr != "")
            {
                idx = svMain.ShutUpList.FFind(whostr);
                if (idx >= 0)
                {
                    svMain.ShutUpList.Objects[idx] = (((int)svMain.ShutUpList.Objects[idx]) + amin * 60 * 1000) as Object;
                }
                else
                {
                    svMain.ShutUpList.QAddObject(whostr, (GetCurrentTime + (amin * 60 * 1000)) as Object);
                }
                if (pass)
                {
                    // 促弗 辑滚俊 傈崔且 巴牢瘤
                    svMain.UserEngine.SendInterMsg(Grobal2.ISM_CHATPROHIBITION, svMain.ServerIndex, whostr + "/" + amin.ToString());
                }
                this.SysMsg(whostr + "禁止聊天+" + amin.ToString() + "分钟。", 1);
            }
            else
            {
                this.SysMsg(whostr + "无法被找到。", 0);
            }
        }

        public void CmdDelShutUpList(string whostr, bool pass)
        {
            TUserHuman hum;
            int idx;
            idx = svMain.ShutUpList.FFind(whostr);
            if (idx >= 0)
            {
                svMain.ShutUpList.Delete(idx);
                hum = svMain.UserEngine.GetUserHuman(whostr);
                if (hum != null)
                {
                    hum.SysMsg("Released from chatting", 1);
                }
                if (pass)
                {
                    // 促弗 辑滚俊 傈崔 咯何
                    svMain.UserEngine.SendInterMsg(Grobal2.ISM_CHATPROHIBITIONCANCEL, svMain.ServerIndex, whostr);
                }
                this.SysMsg(whostr + " " + "", 1);
            }
            else
            {
                this.SysMsg(whostr + " 无法被找到。", 0);
            }
        }

        public void CmdSendShutUpList()
        {
            int i;
            for (i = 0; i < svMain.ShutUpList.Count; i++)
            {
                this.SysMsg(svMain.ShutUpList[i] + " " + ((((int)svMain.ShutUpList.Objects[i]) - GetCurrentTime) / 60000).ToString() + "Min", 1);
            }
        }

        public void CmdOneKillMob()
        {
            TCreature cret;
            cret = this.GetFrontCret();
            if ((cret != null) && (cret.RaceServer >= Grobal2.RC_ANIMAL))
            {
                cret.Die();
            }
        }

        public void CmdAgitDecoMonCount(int agitnum)
        {
            int count;
            count = 0;
            count = svMain.GuildAgitMan.GetAgitDecoMonCount(agitnum);
            this.SysMsg(agitnum.ToString() + "门派庄园的装饰物共有：" + count.ToString() + "个。", 0);
        }

        public void CmdAgitDecoMonCountHere()
        {
            int agitnum;
            int count;
            agitnum = 0;
            count = 0;
            agitnum = svMain.GuildAgitMan.GetGuildAgitNumFromMapName(this.MapName);
            if (agitnum > 0)
            {
                count = svMain.GuildAgitMan.GetAgitDecoMonCount(agitnum);
                this.BoxMsg("门派庄园共有" + count.ToString() + "个装饰物。", 0);
            }
        }

        // 叼滚彪 疙飞绢
        public void CmdUserMarketDebug(string strParam)
        {
            // 咯扁俊 叼滚彪 内靛甫 眠啊窍绞矫坷(sonmg)

        }

        // ////////////////////////
        // 款康磊 疙飞绢 added by sonmg.2003/10/02
        // //////////////////////
        // SEED 酒捞袍 眉农.
        public int CheckSeedItem(TStdItem psSeed, TStdItem psJewelry)
        {
            int result;
            // ///////////////////////////////////////////////////////////////////////////
            // 官蠢龙侩前 肚绰 焕噶摹.
            if (psJewelry.StdMode == 61)
            {
                if (psJewelry.Shape == ObjBase.SHAPE_OF_NEEDLE)
                {
                    // 渴, 捧备, 脚惯, 倾府鹅
                    if (new ArrayList(new int[] { 10, 11, 15, 52, 54 }).Contains(psSeed.StdMode))
                    {
                        result = 11;
                    }
                    else
                    {
                        result = 10;
                    }
                    return result;
                }
                else if (psJewelry.Shape == ObjBase.SHAPE_OF_HAMMER)
                {
                    // 格吧捞, 馆瘤, 迫骂
                    if (new ArrayList(new int[] { 19, 20, 21, 22, 23, 24, 26 }).Contains(psSeed.StdMode))
                    {
                        result = 11;
                    }
                    else
                    {
                        result = 10;
                    }
                    return result;
                }
            }
            else if (psJewelry.StdMode == 7)
            {
                if (psJewelry.Shape == ObjBase.SHAPE_OF_CORD)
                {
                    // 弓澜 啊瓷 酒捞袍.
                    if (this.CheckUnbindItem(psSeed.Name))
                    {
                        result = 21;
                    }
                    else
                    {
                        result = 20;
                    }
                    return result;
                }
            }
            // ///////////////////////////////////////////////////////////////////////////
            // 公扁,渴,捧备,格吧捞,馆瘤,迫骂, 脚惯,骇飘.
            if (new ArrayList(new int[] { 5, 6, 10, 11, 15, 19, 20, 21, 22, 23, 24, 26, 52, 54 }).Contains(psSeed.StdMode))
            {
                result = 2;
            }
            else
            {
                result = 0;
            }
            // 蜡聪农 酒捞袍 眉农
            if (psSeed.UniqueItem == 1)
            {
                result = 3;
                return result;
            }
            switch (psSeed.StdMode)
            {
                case 5:
                case 6:
                    // 公扁
                    if ((psJewelry.AC > 0) || (psJewelry.MAC > 0) || (psJewelry.Accurate > 0) || (psJewelry.Agility > 0) || (psJewelry.MgAvoid > 0) || (psJewelry.ToxAvoid > 0))
                    {
                        result = 1;
                    }
                    break;
                case 10:
                case 11:
                    // 渴
                    if ((psJewelry.DC > 0) || (psJewelry.MC > 0) || (psJewelry.SC > 0) || (psJewelry.Accurate > 0) || (psJewelry.AtkSpd > 0) || (psJewelry.Slowdown > 0) || (psJewelry.Tox > 0))
                    {
                        result = 1;
                    }
                    break;
                case 15:
                    // 捧备
                    if ((psJewelry.DC > 0) || (psJewelry.MC > 0) || (psJewelry.SC > 0) || (psJewelry.Agility > 0) || (psJewelry.AtkSpd > 0) || (psJewelry.Slowdown > 0) || (psJewelry.Tox > 0))
                    {
                        result = 1;
                    }
                    break;
                case 19:
                case 20:
                case 21:
                    // 格吧捞
                    if ((psJewelry.AC > 0) || (psJewelry.MAC > 0) || (psJewelry.Agility > 0) || (psJewelry.ToxAvoid > 0))
                    {
                        result = 1;
                    }
                    break;
                case 22:
                    // 馆瘤
                    if ((psJewelry.Accurate > 0) || (psJewelry.Agility > 0) || (psJewelry.MgAvoid > 0) || (psJewelry.ToxAvoid > 0))
                    {
                        result = 1;
                    }
                    break;
                case 23:
                    // 馆瘤23
                    // 漂沥 馆瘤俊 规绢,付亲阑 画(sonmg)
                    if ((psJewelry.AC > 0) || (psJewelry.MAC > 0) || (psJewelry.Accurate > 0) || (psJewelry.Agility > 0) || (psJewelry.MgAvoid > 0) || (psJewelry.ToxAvoid > 0))
                    {
                        result = 1;
                    }
                    break;
                case 24:
                    // 迫骂24
                    // 漂沥 迫骂俊 规绢,付亲阑 画(sonmg)
                    if ((psJewelry.AC > 0) || (psJewelry.MAC > 0) || (psJewelry.AtkSpd > 0) || (psJewelry.MgAvoid > 0) || (psJewelry.Slowdown > 0) || (psJewelry.Tox > 0) || (psJewelry.ToxAvoid > 0))
                    {
                        result = 1;
                    }
                    break;
                case 26:
                    // 迫骂26
                    if ((psJewelry.AtkSpd > 0) || (psJewelry.MgAvoid > 0) || (psJewelry.Slowdown > 0) || (psJewelry.Tox > 0) || (psJewelry.ToxAvoid > 0))
                    {
                        result = 1;
                    }
                    break;
                case 52:
                    // 脚惯
                    if ((psJewelry.DC > 0) || (psJewelry.MC > 0) || (psJewelry.SC > 0) || (psJewelry.Accurate > 0) || (psJewelry.AtkSpd > 0) || (psJewelry.MgAvoid > 0) || (psJewelry.Slowdown > 0) || (psJewelry.Tox > 0) || (psJewelry.ToxAvoid > 0))
                    {
                        result = 1;
                    }
                    break;
                case 54:
                    // 骇飘
                    if ((psJewelry.DC > 0) || (psJewelry.MC > 0) || (psJewelry.SC > 0) || (psJewelry.AtkSpd > 0) || (psJewelry.MgAvoid > 0) || (psJewelry.Slowdown > 0) || (psJewelry.Tox > 0))
                    {
                        result = 1;
                    }
                    break;
                default:
                    result = 0;
                    break;
            }
            return result;
        }

        // 焊苛幅 酒捞袍 眉农.
        public bool CheckJewelryItem(int iStdMode)
        {
            bool result;
            // 焊苛,脚林,畴馋.
            if (new ArrayList(new int[] { 7, 60, 61 }).Contains(iStdMode))
            {
                result = true;
            }
            else
            {
                result = false;
            }
            return result;
        }

        // ///////////////////////
        // 扁粮 加己蔼狼 钦.
        public int SumOfOptions(TUserItem puSeedItem, TStdItem psSeedItem)
        {
            int result;
            result = 0;
            switch (psSeedItem.StdMode)
            {
                case 5:
                case 6:
                    // 公扁
                    result = puSeedItem.Desc[0] + puSeedItem.Desc[1] + puSeedItem.Desc[2] + puSeedItem.Desc[5] + puSeedItem.Desc[12] + puSeedItem.Desc[13];
                    // 傍加 钦魂(公扁).
                    result = result + _MAX(0, svMain.ItemMan.RealAttackSpeed(puSeedItem.Desc[6]));
                    break;
                case 10:
                case 11:
                case 15:
                    // 渴, 捧备
                    result = puSeedItem.Desc[0] + puSeedItem.Desc[1] + puSeedItem.Desc[11] + puSeedItem.Desc[12] + puSeedItem.Desc[13];
                    break;
                case 19:
                    // 格吧捞19
                    result = puSeedItem.Desc[0] + puSeedItem.Desc[2] + puSeedItem.Desc[3] + puSeedItem.Desc[4] + puSeedItem.Desc[11] + puSeedItem.Desc[12] + puSeedItem.Desc[13];
                    // 傍加 钦魂.
                    if (puSeedItem.Desc[9] > 0)
                    {
                        result = result + puSeedItem.Desc[9];
                    }
                    break;
                case 20:
                    // 格吧捞
                    result = puSeedItem.Desc[0] + puSeedItem.Desc[1] + puSeedItem.Desc[2] + puSeedItem.Desc[3] + puSeedItem.Desc[4] + puSeedItem.Desc[11] + puSeedItem.Desc[12] + puSeedItem.Desc[13];
                    // 傍加 钦魂.
                    if (puSeedItem.Desc[9] > 0)
                    {
                        result = result + puSeedItem.Desc[9];
                    }
                    break;
                case 21:
                    // 格吧捞
                    result = puSeedItem.Desc[2] + puSeedItem.Desc[3] + puSeedItem.Desc[4] + puSeedItem.Desc[7] + puSeedItem.Desc[11] + puSeedItem.Desc[12] + puSeedItem.Desc[13];
                    // 傍加 钦魂.
                    if (puSeedItem.Desc[9] > 0)
                    {
                        result = result + puSeedItem.Desc[9];
                    }
                    break;
                case 22:
                    // 馆瘤
                    result = puSeedItem.Desc[0] + puSeedItem.Desc[1] + puSeedItem.Desc[2] + puSeedItem.Desc[3] + puSeedItem.Desc[4] + puSeedItem.Desc[12] + puSeedItem.Desc[13];
                    // 傍加 钦魂.
                    if (puSeedItem.Desc[9] > 0)
                    {
                        result = result + puSeedItem.Desc[9];
                    }
                    break;
                case 23:
                    // 馆瘤23
                    result = puSeedItem.Desc[0] + puSeedItem.Desc[2] + puSeedItem.Desc[3] + puSeedItem.Desc[4] + puSeedItem.Desc[12] + puSeedItem.Desc[13];
                    // 傍加 钦魂.
                    if (puSeedItem.Desc[9] > 0)
                    {
                        result = result + puSeedItem.Desc[9];
                    }
                    break;
                case 24:
                    // 迫骂24
                    result = puSeedItem.Desc[0] + puSeedItem.Desc[1] + puSeedItem.Desc[2] + puSeedItem.Desc[3] + puSeedItem.Desc[4];
                    break;
                case 26:
                    // 迫骂
                    result = puSeedItem.Desc[0] + puSeedItem.Desc[1] + puSeedItem.Desc[2] + puSeedItem.Desc[3] + puSeedItem.Desc[4] + puSeedItem.Desc[11] + puSeedItem.Desc[12];
                    break;
                case 52:
                    // 脚惯
                    result = puSeedItem.Desc[0] + puSeedItem.Desc[1] + puSeedItem.Desc[3];
                    break;
                case 54:
                    // 骇飘
                    result = puSeedItem.Desc[0] + puSeedItem.Desc[1] + puSeedItem.Desc[2] + puSeedItem.Desc[3] + puSeedItem.Desc[13];
                    break;
            }
            // 郴备仿 钦魂.   //窜拌啊 1000 -> 2000 栏肺 刘啊 2003-11-7 PDS
            result = result + _MAX(0, HUtil32.MathRound((puSeedItem.DuraMax - psSeedItem.DuraMax) / 2000));
            return result;
        }

        public void CalcUpgradeProbability_InitProbability(ref TUpgradeProb[] UpProb)
        {
            UpProb[0].iBase = 10000;
            UpProb[0].iValue[0] = 5000;
            UpProb[0].iValue[1] = 5000;
            UpProb[0].iValue[2] = 5000;
            UpProb[0].iValue[3] = UpProb[0].iValue[0] * 2;
            UpProb[0].iValue[4] = UpProb[0].iValue[1] * 2;
            UpProb[0].iValue[5] = UpProb[0].iValue[2] * 2;
            UpProb[1].iBase = 10000;
            UpProb[1].iValue[0] = 4500;
            UpProb[1].iValue[1] = 3000;
            UpProb[1].iValue[2] = 4000;
            UpProb[1].iValue[3] = UpProb[1].iValue[0] * 2;
            UpProb[1].iValue[4] = UpProb[1].iValue[1] * 2;
            UpProb[1].iValue[5] = UpProb[1].iValue[2] * 2;
            UpProb[2].iBase = 10000;
            UpProb[2].iValue[0] = 4000;
            UpProb[2].iValue[1] = 1000;
            UpProb[2].iValue[2] = 3000;
            UpProb[2].iValue[3] = UpProb[2].iValue[0] * 2;
            UpProb[2].iValue[4] = UpProb[2].iValue[1] * 2;
            UpProb[2].iValue[5] = UpProb[2].iValue[2] * 2;
            UpProb[3].iBase = 10000;
            UpProb[3].iValue[0] = 3500;
            UpProb[3].iValue[1] = 500;
            UpProb[3].iValue[2] = 1000;
            UpProb[3].iValue[3] = UpProb[3].iValue[0] * 2;
            UpProb[3].iValue[4] = UpProb[3].iValue[1] * 2;
            UpProb[3].iValue[5] = UpProb[3].iValue[2] * 2;
            UpProb[4].iBase = 10000;
            UpProb[4].iValue[0] = 3000;
            UpProb[4].iValue[1] = 100;
            UpProb[4].iValue[2] = 500;
            UpProb[4].iValue[3] = UpProb[4].iValue[0] * 2;
            UpProb[4].iValue[4] = UpProb[4].iValue[1] * 2;
            UpProb[4].iValue[5] = UpProb[4].iValue[2] * 2;
            UpProb[5].iBase = 10000;
            UpProb[5].iValue[0] = 1500;
            UpProb[5].iValue[1] = 25;
            UpProb[5].iValue[2] = 100;
            UpProb[5].iValue[3] = UpProb[5].iValue[0] * 2;
            UpProb[5].iValue[4] = UpProb[5].iValue[1] * 2;
            UpProb[5].iValue[5] = UpProb[5].iValue[2] * 2;
            UpProb[6].iBase = 10000;
            UpProb[6].iValue[0] = 400;
            UpProb[6].iValue[1] = 5;
            UpProb[6].iValue[2] = 25;
            UpProb[6].iValue[3] = UpProb[6].iValue[0] * 2;
            UpProb[6].iValue[4] = UpProb[6].iValue[1] * 2;
            UpProb[6].iValue[5] = UpProb[6].iValue[2] * 2;
            UpProb[7].iBase = 10000;
            UpProb[7].iValue[0] = 100;
            UpProb[7].iValue[1] = 5;
            UpProb[7].iValue[2] = 5;
            UpProb[7].iValue[3] = UpProb[7].iValue[0] * 2;
            UpProb[7].iValue[4] = UpProb[7].iValue[1] * 2;
            UpProb[7].iValue[5] = UpProb[7].iValue[2] * 2;
            UpProb[8].iBase = 10000;
            UpProb[8].iValue[0] = 25;
            UpProb[8].iValue[1] = 5;
            UpProb[8].iValue[2] = 5;
            UpProb[8].iValue[3] = UpProb[8].iValue[0] * 2;
            UpProb[8].iValue[4] = UpProb[8].iValue[1] * 2;
            UpProb[8].iValue[5] = UpProb[8].iValue[2] * 2;
            UpProb[9].iBase = 10000;
            UpProb[9].iValue[0] = 5;
            UpProb[9].iValue[1] = 5;
            UpProb[9].iValue[2] = 5;
            UpProb[9].iValue[3] = UpProb[9].iValue[0] * 2;
            UpProb[9].iValue[4] = UpProb[9].iValue[1] * 2;
            UpProb[9].iValue[5] = UpProb[9].iValue[2] * 2;
            UpProb[10].iBase = 10000;
            UpProb[10].iValue[0] = 0;
            UpProb[10].iValue[1] = 0;
            UpProb[10].iValue[2] = 0;
            UpProb[10].iValue[3] = 0;
            UpProb[10].iValue[4] = 0;
            UpProb[10].iValue[5] = 0;
        }

        // /////////////////////////////////
        // 犬伏 拌魂 棺 搬苞 府畔 窃荐.
        public int CalcUpgradeProbability(TUserItem puSeedItem, TUserItem puJewelryItem, TStdItem psSeedItem, TStdItem psJewelryItem, int iExecCount, ref int iRetSum, ref double fRetProb)
        {
            TUpgradeProb[] UpProb = new TUpgradeProb[10 + 1];
            int testSucceed;
            int testNoChange;
            int testFail;
            int result = 2;
            int iSum = _MIN(10, _MAX(0, SumOfOptions(puSeedItem, psSeedItem)));
            iRetSum = iSum;
            CalcUpgradeProbability_InitProbability(ref UpProb);
            testSucceed = 0;
            testNoChange = 0;
            testFail = 0;
            int iSucceed = 0;
            int iFail = 0;
            int iRandom = 0;
            if (iExecCount < 1)
            {
                iExecCount = 1;
            }
            for (var i = 0; i < iExecCount; i++)
            {
                iRandom = new System.Random(UpProb[iSum].iBase).Next();
                if (psJewelryItem.StdMode == 60)
                {
                    if (new ArrayList(new int[] { 5, 6 }).Contains(psSeedItem.StdMode))
                    {
                        // 公扁
                        iSucceed = _MIN(UpProb[iSum].iBase, HUtil32.MathRound(UpProb[iSum].iValue[0] * Math.Abs((29 + this.BodyLuckLevel + (Lobyte(psSeedItem.AC) + puSeedItem.Desc[3] - Lobyte(psSeedItem.MAC) - puSeedItem.Desc[4]) / 2) / 30)));
                    }
                    else if (new ArrayList(new int[] { 10, 11 }).Contains(psSeedItem.StdMode))
                    {
                        // 渴
                        iSucceed = _MIN(UpProb[iSum].iBase, HUtil32.MathRound(UpProb[iSum].iValue[0] * Math.Abs((29 + this.BodyLuckLevel) / 30)));
                    }
                    else if (new ArrayList(new int[] { 24, 26, 52 }).Contains(psSeedItem.StdMode))
                    {
                        // 迫骂,脚惯
                        iSucceed = _MIN(UpProb[iSum].iBase, HUtil32.MathRound(UpProb[iSum].iValue[1] * Math.Abs((29 + this.BodyLuckLevel) / 30)));
                    }
                    else if (psSeedItem.StdMode == 19)
                    {
                        // 格吧捞19
                        iSucceed = _MIN(UpProb[iSum].iBase, HUtil32.MathRound(UpProb[iSum].iValue[2] * Math.Abs((29 + this.BodyLuckLevel) / 30)));
                    }
                    else
                    {
                        // 扁鸥
                        iSucceed = _MIN(UpProb[iSum].iBase, HUtil32.MathRound(UpProb[iSum].iValue[2] * Math.Abs((29 + this.BodyLuckLevel) / 30)));
                    }
                    // 傍加 犬伏 蝶肺 利侩.(sonmg 2003/12/22)
                    if (psJewelryItem.Shape == 9)
                    {
                        iSucceed = iSucceed * 60 / 100;
                    }
                    iFail = HUtil32.MathRound((UpProb[iSum].iBase - iSucceed) * 0.7);
                    if ((iRandom >= 0) && (iRandom < iSucceed))
                    {
                        // 己傍
                        result = 2;
                    }
                    else if ((iRandom >= iSucceed) && (iRandom < iSucceed + iFail))
                    {
                        // 阂函
                        result = 1;
                    }
                    else
                    {
                        result = 0;
                    }
                    // 颇颊
                }
                else if (psJewelryItem.StdMode == 61)
                {
                    if (new ArrayList(new int[] { 5, 6 }).Contains(psSeedItem.StdMode))
                    {
                        iSucceed = _MIN(UpProb[iSum].iBase, HUtil32.MathRound(UpProb[iSum].iValue[3] * Math.Abs((29 + this.BodyLuckLevel + (Lobyte(psSeedItem.AC) + puSeedItem.Desc[3] - Lobyte(psSeedItem.MAC) - puSeedItem.Desc[4]) / 2) / 30)));
                    }
                    else if (new ArrayList(new int[] { 10, 11 }).Contains(psSeedItem.StdMode))
                    {
                        iSucceed = _MIN(UpProb[iSum].iBase, HUtil32.MathRound(UpProb[iSum].iValue[3] * Math.Abs((29 + this.BodyLuckLevel) / 30)));
                    }
                    else if (new ArrayList(new int[] { 24, 26, 52 }).Contains(psSeedItem.StdMode))
                    {
                        iSucceed = _MIN(UpProb[iSum].iBase, HUtil32.MathRound(UpProb[iSum].iValue[4] * Math.Abs((29 + this.BodyLuckLevel) / 30)));
                    }
                    else if (psSeedItem.StdMode == 19)
                    {
                        iSucceed = _MIN(UpProb[iSum].iBase, HUtil32.MathRound(UpProb[iSum].iValue[5] * Math.Abs((29 + this.BodyLuckLevel) / 30)));
                    }
                    else
                    {
                        iSucceed = _MIN(UpProb[iSum].iBase, HUtil32.MathRound(UpProb[iSum].iValue[5] * Math.Abs((29 + this.BodyLuckLevel) / 30)));
                    }
                    if (psJewelryItem.Shape == 9)
                    {
                        iSucceed = iSucceed * 60 / 100;
                    }
                    iFail = HUtil32.MathRound(0.7 * (UpProb[iSum].iBase - iSucceed));
                    if ((iRandom >= 0) && (iRandom < iSucceed))
                    {
                        result = 2;
                    }
                    else
                    {
                        result = 1;
                    }
                }
                fRetProb = iSucceed / UpProb[iSum].iBase;
                if (result == 2)
                {
                    testSucceed++;
                }
                else if (result == 1)
                {
                    testNoChange++;
                }
                else if (result == 0)
                {
                    testFail++;
                }
            }
            if (iExecCount > 1)
            {
                this.SysMsg("Prob. Test(" + iExecCount.ToString() + ")times=>" + "Success:" + testSucceed.ToString() + ", NoChange:" + testNoChange.ToString() + ", Destruct:" + testFail.ToString(), 0);
            }
#if DEBUG
            if (psJewelryItem.StdMode == 60)
            {
                this.SysMsg("Sum Opt.:" + (iSum).ToString() + ", Prob.Success:" + (iSucceed).ToString() + ", Prob.NoChange:" + (iFail).ToString() + ", Prob.Destroy:" + (UpProb[iSum].iBase - iSucceed - iFail).ToString() + ", iRandom:" + (iRandom).ToString(), 0);
            }
            else if (psJewelryItem.StdMode == 61)
            {
                this.SysMsg("Sum Opt.:" + (iSum).ToString() + ", Prob.Success:" + (iSucceed).ToString() + ", Prob.NoChange:" + (UpProb[iSum].iBase - iSucceed).ToString() + ", iRandom:" + (iRandom).ToString(), 0);
            }
#endif
            return result;
        }

        // /////////////////////////////////////////////////////////////
        // added by sonmg...
        public void CmdUpgradeItem(string seedname, string jewelryname, int seedindex, int jewelryindex, int ExecCount)
        {
            int iResult;
            int i;
            int j;
            int k;
            int iVal;
            TUserItem puSeed;
            TUserItem puJewelry;
            TStdItem psSeed;
            TStdItem psJewelry;
            string strResult;
            string strEtc;
            int iSumOfOption;
            double fProbability;
            int iBeforeValue;
            int iAfterValue;
            int iShape;
            ArrayList dellist;
            puSeed = null;
            puJewelry = null;
            psSeed = null;
            psJewelry = null;
            iSumOfOption = 0;
            fProbability = 0;
            iBeforeValue = 0;
            iAfterValue = 0;
            try
            {
                if (seedname == "")
                {
                    return;
                }
                if (jewelryname == "")
                {
                    return;
                }
                // /////////////////////////////////////////////////
                // 焊苛幅 八荤
                // 款康磊 疙飞阑 困茄 内靛(款康磊 疙飞老锭绰 Index蔼捞 0栏肺 甸绢柯促).
                if (jewelryindex == 0)
                {
                    for (i = 0; i < this.ItemList.Count; i++)
                    {
                        if (svMain.UserEngine.GetStdItemIndex(jewelryname) == this.ItemList[i].Index)
                        {
                            psJewelry = svMain.UserEngine.GetStdItem(this.ItemList[i].Index);
                            jewelryindex = this.ItemList[i].MakeIndex;
                            puJewelry = this.ItemList[i];
                            break;
                        }
                    }
                    if (i == this.ItemList.Count)
                    {
                        return;
                    }
                }
                else
                {
                    // 款康磊啊 酒囱 沥惑利牢 版快.
                    for (i = 0; i < this.ItemList.Count; i++)
                    {
                        if (jewelryindex == this.ItemList[i].MakeIndex)
                        {
                            psJewelry = svMain.UserEngine.GetStdItem(this.ItemList[i].Index);
                            puJewelry = this.ItemList[i];
                            break;
                        }
                    }
                    if (i == this.ItemList.Count)
                    {
                        return;
                    }
                }
                // /////////////////////////////////////////////////
                // /////////////////////////////////////////////////
                // SEED 八荤
                // 款康磊 疙飞阑 困茄 内靛(款康磊 疙飞老锭绰 Index蔼捞 0栏肺 甸绢柯促).
                if (seedindex == 0)
                {
                    for (i = 0; i < this.ItemList.Count; i++)
                    {
                        if (svMain.UserEngine.GetStdItemIndex(seedname) == this.ItemList[i].Index)
                        {
                            psSeed = svMain.UserEngine.GetStdItem(this.ItemList[i].Index);
                            seedindex = this.ItemList[i].MakeIndex;
                            puSeed = this.ItemList[i];
                            break;
                        }
                    }
                    if (i == this.ItemList.Count)
                    {
                        return;
                    }
                }
                else
                {
                    // 款康磊啊 酒囱 沥惑利牢 版快.
                    for (i = 0; i < this.ItemList.Count; i++)
                    {
                        if (seedindex == this.ItemList[i].MakeIndex)
                        {
                            psSeed = svMain.UserEngine.GetStdItem(this.ItemList[i].Index);
                            puSeed = this.ItemList[i];
                            break;
                        }
                    }
                    if (i == this.ItemList.Count)
                    {
                        return;
                    }
                }
                // /////////////////////////////////////////////////
                if (puSeed.Index > 0)
                {
                    if (CheckJewelryItem(psJewelry.StdMode))
                    {
                        iVal = CheckSeedItem(psSeed, psJewelry);
                        if (iVal == 2)
                        {
                            iResult = CalcUpgradeProbability(puSeed, puJewelry, psSeed, psJewelry, ExecCount, ref iSumOfOption, ref fProbability);
                            if ((iResult <= 2) && (iResult >= 0))
                            {
 
                            }
                            iBeforeValue = GetTotalValueOfOption(puSeed, psSeed, psJewelry, ref strResult, ref strEtc);
                            switch (iResult)
                            {
                                case 2:
                                    if (DoUpgradeItem(puSeed, psSeed, psJewelry) == 0)
                                    {
                                        this.SysMsg("不能升级这个属性。", 0);
                                        return;
                                    }
                                    iAfterValue = GetTotalValueOfOption(puSeed, psSeed, psJewelry, ref strResult, ref strEtc);
                                    this.DeletePItemAndSend(puJewelry);
                                    this.SysMsg(strResult + strEtc + " upgraded to " + seedname, 2);
                                    svMain.AddUserLog("31\09" + this.MapName + "\09" + this.CX.ToString() + "\09" + this.CY.ToString() + "\09" + this.UserName + "\09" + seedname + "\09" + seedindex.ToString() + "\09" + "2\09" + this.UpgradeResultToStr(iSumOfOption, strResult, iBeforeValue, iAfterValue, fProbability, psJewelry.StdMode));
                                    SendDefMessage(Grobal2.SM_UPGRADEITEM_RESULT, seedindex, iResult, 0, 0, seedname);
                                    break;
                                case 1:
                                    iAfterValue = GetTotalValueOfOption(puSeed, psSeed, psJewelry, ref strResult, ref strEtc);
                                    this.DeletePItemAndSend(puJewelry);
                                    this.SysMsg(seedname + "不能升级。", 1);
                                    svMain.AddUserLog("31\09" + this.MapName + "\09" + this.CX.ToString() + "\09" + this.CY.ToString() + "\09" + this.UserName + "\09" + seedname + "\09" + seedindex.ToString() + "\09" + "1\09" + this.UpgradeResultToStr(iSumOfOption, strResult, iBeforeValue, iAfterValue, fProbability, psJewelry.StdMode));
                                    SendDefMessage(Grobal2.SM_UPGRADEITEM_RESULT, seedindex, iResult, 0, 0, seedname);
                                    break;
                                case 0:
                                    iAfterValue = GetTotalValueOfOption(puSeed, psSeed, psJewelry, ref strResult, ref strEtc);
                                    this.DeletePItemAndSend(puJewelry);
                                    this.DeletePItemAndSendWithFlag(puSeed, (short)true);
                                    this.SysMsg("物品(" + seedname + ")被销毁。", 0);
                                    svMain.AddUserLog("31\09" + this.MapName + "\09" + this.CX.ToString() + "\09" + this.CY.ToString() + "\09" + this.UserName + "\09" + seedname + "\09" + seedindex.ToString() + "\09" + "0\09" + this.UpgradeResultToStr(iSumOfOption, strResult, iBeforeValue, iAfterValue, fProbability, psJewelry.StdMode));
                                    SendDefMessage(Grobal2.SM_UPGRADEITEM_RESULT, seedindex, iResult, 0, 0, seedname);
                                    break;
                                default:
                                    svMain.MainOutMessage("[UpgradeItem] " + this.UserName + " DoUpgradeItem : Result value is abnormal.");
                                    break;
                            }
                        }
                        else if (iVal == 1)
                        {
                            this.SysMsg("这个物品的属性不能升级。", 0);
                        }
                        else if (iVal == 3)
                        {
                            // 官蠢龙侩前, 焕噶摹.
                            this.SysMsg("特殊物品不能升级。", 0);
                        }
                        else if (iVal == 11)
                        {
                            if (this.RepairItemNormaly(psSeed, puSeed))
                            {
                                // 荐府侩前 酒捞袍 昏力.
                                this.DeletePItemAndSend(puJewelry);
                            }
                        }
                        else if (iVal == 10)
                        {
                            // 畴馋(2004/05/03 sonmg)
                            this.SysMsg("这个物品不能被修复。", 0);
                        }
                        else if (iVal == 21)
                        {
                            iShape = this.FindItemToBindFromBag(6, psSeed.Name, ref dellist);
                            if (iShape >= 0)
                            {
                                // 弓澜 酒捞袍 积己.
                                if (BindPotionUnit(iShape, 6) == true)
                                {
                                    // 畴馋 窍唱 昏力.
                                    this.DeleteItemFromBag(psJewelry, puJewelry);
                                    // 昏力格废俊 眠啊等 官牢靛 酒捞袍甸 昏力.
                                    if (dellist != null)
                                    {
                                        for (j = 0; j < dellist.Count; j++)
                                        {
                                            for (k = 0; k < this.ItemList.Count; k++)
                                            {
                                                if (this.ItemList[k].MakeIndex == ((int)dellist.Values[j]))
                                                {
                                                    Dispose(this.ItemList[k]);
                                                    this.ItemList.RemoveAt(k);
                                                    break;
                                                }
                                            }
                                        }
                                        this.SendMsg(this, Grobal2.RM_DELITEMS, 0, (int)dellist, 0, 0, "");
                                        // dellist绰 rm_delitem俊辑 free 矫难具 茄促.
                                        this.WeightChanged();
                                    }
                                }
                                else
                                {
                                    this.SysMsg("这个物品不能被合成。", 0);
                                }
                            }
                        }
                        else if (iVal == 20)
                        {
                            this.SysMsg("这个物品不能被合成。", 0);
                        }
                        else
                        {
                            this.SysMsg(seedname + "：无法关联到这个物品。", 0);
                        }
                    }
                    // else SysMsg (jewelryname + ' : 捞 酒捞袍栏肺绰 诀弊饭捞靛且 荐 绝嚼聪促.', 0);
                }
            }
            catch
            {
                svMain.MainOutMessage("[Exception] TUserHuman.CmdUpgradeItem");
            }
        }

        // 诀弊饭捞靛侩 郴何窃荐(sonmg)
        // 牢郸胶肺 扁夯蔼+诀弊饭捞靛等蔼阑 府畔窍绰 窃荐
        private int GetTotalValueOfOption(TUserItem pu, TStdItem pstd, TStdItem psJewelry, ref string strResult, ref string strEtc)
        {
            int result;
            int iBaseValue;
            int iUpgradeValue;
            int iOptionIndex;
            result = 0;
            iBaseValue = 0;
            iUpgradeValue = 0;
            iOptionIndex = 0;
            // //////////////////////////////////////////////
            // 诀弊饭捞靛 可记 犬牢
            // 搬苞 巩磊凯 府畔.
            // 诀弊饭捞靛 且 荐 乐绰 可记篮 茄锅俊 窍唱(盖 贸澜 唱坷绰 可记阑 急琶茄促.)
            if (psJewelry.DC > 0)
            {
                iOptionIndex = 100;
                strResult = "攻击力";
                strEtc = " " + psJewelry.DC.ToString();
            }
            else if (psJewelry.MC > 0)
            {
                iOptionIndex = 101;
                strResult = "魔法力";
                strEtc = " " + psJewelry.MC.ToString();
            }
            else if (psJewelry.SC > 0)
            {
                iOptionIndex = 102;
                strResult = "精神力";
                strEtc = " " + psJewelry.SC.ToString();
            }
            else if (psJewelry.AC > 0)
            {
                iOptionIndex = 103;
                strResult = "防御";
                strEtc = " " + psJewelry.AC.ToString();
            }
            else if (psJewelry.MAC > 0)
            {
                iOptionIndex = 104;
                strResult = "魔防";
                strEtc = " " + psJewelry.MAC.ToString();
            }
            else if (psJewelry.DuraMax > 0)
            {
                iOptionIndex = 105;
                strResult = "持久";
                strEtc = " " + HUtil32.MathRound(psJewelry.DuraMax / 1000).ToString();
            }
            else if (psJewelry.Accurate > 0)
            {
                iOptionIndex = 106;
                strResult = "准确";
                strEtc = " " + psJewelry.Accurate.ToString();
            }
            else if (psJewelry.Agility > 0)
            {
                iOptionIndex = 107;
                strResult = "敏捷";
                strEtc = " " + psJewelry.Agility.ToString();
            }
            else if (psJewelry.AtkSpd > 0)
            {
                iOptionIndex = 108;
                strResult = "攻击速度";
                strEtc = " " + psJewelry.AtkSpd.ToString();
            }
            else if (psJewelry.Slowdown > 0)
            {
                iOptionIndex = 109;
                strResult = "减速";
                strEtc = " " + psJewelry.Slowdown.ToString();
            }
            else if (psJewelry.Tox > 0)
            {
                iOptionIndex = 110;
                strResult = "中毒";
                strEtc = " " + psJewelry.Tox.ToString();
            }
            else if (psJewelry.MgAvoid > 0)
            {
                iOptionIndex = 111;
                strResult = "魔法抗性";
                strEtc = " " + psJewelry.MgAvoid.ToString();
            }
            else if (psJewelry.ToxAvoid > 0)
            {
                iOptionIndex = 112;
                strResult = "中毒抗性";
                strEtc = " " + psJewelry.ToxAvoid.ToString();
            }
            switch (pstd.StdMode)
            {
                case 5:
                case 6:
                    // //////////////////////////////////////////////
                    // iIndex蔼狼 狼固
                    // 100:颇鲍, 101:付过, 102:档仿, 103:规绢, 104:付亲, 105:郴备
                    // 106:沥犬, 107:刮酶, 108:傍加, 109:敌拳, 110:吝刀, 111:付历亲, 112:吝历亲
                    // /////////////////////////////////////////////////////
                    // 公扁
                    switch (iOptionIndex)
                    {
                        case 100:
                            // 颇鲍
                            iBaseValue = HiByte(pstd.DC);
                            iUpgradeValue = pu.Desc[0];
                            result = iBaseValue + iUpgradeValue;
                            break;
                        case 101:
                            // 付过
                            iBaseValue = HiByte(pstd.MC);
                            iUpgradeValue = pu.Desc[1];
                            result = iBaseValue + iUpgradeValue;
                            break;
                        case 102:
                            // 档仿
                            iBaseValue = HiByte(pstd.SC);
                            iUpgradeValue = pu.Desc[2];
                            result = iBaseValue + iUpgradeValue;
                            break;
                        case 103:
                            // 规绢
                            break;
                        case 104:
                            // 付亲
                            break;
                        case 105:
                            // 郴备
                            iBaseValue = HUtil32.MathRound(pstd.DuraMax / 1000);
                            iUpgradeValue = HUtil32.MathRound(pu.DuraMax / 1000);
                            result = iUpgradeValue;
                            break;
                        case 106:
                            // 郴备
                            // 沥犬
                            iBaseValue = HiByte(pstd.AC);
                            iUpgradeValue = pu.Desc[5];
                            result = iBaseValue + iUpgradeValue;
                            break;
                        case 107:
                            // 刮酶
                            break;
                        case 108:
                            // 傍加
                            iBaseValue = svMain.ItemMan.RealAttackSpeed(HiByte(pstd.MAC));
                            iUpgradeValue = svMain.ItemMan.RealAttackSpeed(pu.Desc[6]);
                            // 
                            // //傍加捞 10焊促 累阑 锭甫 困茄 贸府.
                            // if HiByte(pstd.MAC) > 10 then begin
                            // iBaseValue := HiByte(pstd.MAC) - 10;
                            // iUpgradeValue := pu.Desc[6];
                            // end else begin
                            // iBaseValue := - HiByte(pstd.MAC);
                            // iUpgradeValue := pu.Desc[6];
                            // end;
                            result = iBaseValue + iUpgradeValue;
                            break;
                        case 109:
                            // 敌拳
                            iBaseValue = pstd.Slowdown;
                            iUpgradeValue = pu.Desc[12];
                            result = iBaseValue + iUpgradeValue;
                            break;
                        case 110:
                            // 吝刀
                            iBaseValue = pstd.Tox;
                            iUpgradeValue = pu.Desc[13];
                            result = iBaseValue + iUpgradeValue;
                            break;
                        case 111:
                            // 付历亲
                            break;
                        case 112:
                            // 吝历亲
                            break;
                    }
                    break;
                case 10:
                case 11:
                case 15:
                    // case iOptionIndex of
                    // /////////////////////////////////////////////////////
                    // 渴, 捧备
                    switch (iOptionIndex)
                    {
                        case 100:
                            // 颇鲍
                            iBaseValue = HiByte(pstd.DC);
                            iUpgradeValue = pu.Desc[2];
                            result = iBaseValue + iUpgradeValue;
                            break;
                        case 101:
                            // 付过
                            iBaseValue = HiByte(pstd.MC);
                            iUpgradeValue = pu.Desc[3];
                            result = iBaseValue + iUpgradeValue;
                            break;
                        case 102:
                            // 档仿
                            iBaseValue = HiByte(pstd.SC);
                            iUpgradeValue = pu.Desc[4];
                            result = iBaseValue + iUpgradeValue;
                            break;
                        case 103:
                            // 规绢
                            iBaseValue = HiByte(pstd.AC);
                            iUpgradeValue = pu.Desc[0];
                            result = iBaseValue + iUpgradeValue;
                            break;
                        case 104:
                            // 付亲
                            iBaseValue = HiByte(pstd.MAC);
                            iUpgradeValue = pu.Desc[1];
                            result = iBaseValue + iUpgradeValue;
                            break;
                        case 105:
                            // 郴备
                            iBaseValue = HUtil32.MathRound(pstd.DuraMax / 1000);
                            iUpgradeValue = HUtil32.MathRound(pu.DuraMax / 1000);
                            result = iUpgradeValue;
                            break;
                        case 106:
                            // 郴备
                            // 沥犬
                            // 捧备俊父 秦寸
                            iBaseValue = pstd.Accurate;
                            iUpgradeValue = pu.Desc[11];
                            result = iBaseValue + iUpgradeValue;
                            break;
                        case 107:
                            // 刮酶
                            // 渴俊父 秦寸
                            iBaseValue = pstd.Agility;
                            iUpgradeValue = pu.Desc[11];
                            result = iBaseValue + iUpgradeValue;
                            break;
                        case 108:
                            // 傍加
                            break;
                        case 109:
                            // 敌拳
                            break;
                        case 110:
                            // 吝刀
                            break;
                        case 111:
                            // 付历亲
                            iBaseValue = pstd.MgAvoid;
                            iUpgradeValue = pu.Desc[12];
                            result = iBaseValue + iUpgradeValue;
                            break;
                        case 112:
                            // 吝历亲
                            iBaseValue = pstd.ToxAvoid;
                            iUpgradeValue = pu.Desc[13];
                            result = iBaseValue + iUpgradeValue;
                            break;
                    }
                    break;
                case 19:
                    // case iOptionIndex of
                    // /////////////////////////////////////////////////////
                    // 格吧捞19
                    switch (iOptionIndex)
                    {
                        case 100:
                            // 颇鲍
                            iBaseValue = HiByte(pstd.DC);
                            iUpgradeValue = pu.Desc[2];
                            result = iBaseValue + iUpgradeValue;
                            break;
                        case 101:
                            // 付过
                            iBaseValue = HiByte(pstd.MC);
                            iUpgradeValue = pu.Desc[3];
                            result = iBaseValue + iUpgradeValue;
                            break;
                        case 102:
                            // 档仿
                            iBaseValue = HiByte(pstd.SC);
                            iUpgradeValue = pu.Desc[4];
                            result = iBaseValue + iUpgradeValue;
                            break;
                        case 103:
                            // 规绢
                            break;
                        case 104:
                            // 付亲
                            break;
                        case 105:
                            // 郴备
                            iBaseValue = HUtil32.MathRound(pstd.DuraMax / 1000);
                            iUpgradeValue = HUtil32.MathRound(pu.DuraMax / 1000);
                            result = iUpgradeValue;
                            break;
                        case 106:
                            // 郴备
                            // 沥犬
                            iBaseValue = pstd.Accurate;
                            iUpgradeValue = pu.Desc[11];
                            result = iBaseValue + iUpgradeValue;
                            break;
                        case 107:
                            // 刮酶
                            break;
                        case 108:
                            // 傍加
                            iBaseValue = pstd.AtkSpd;
                            iUpgradeValue = pu.Desc[9];
                            result = iBaseValue + iUpgradeValue;
                            break;
                        case 109:
                            // 敌拳
                            iBaseValue = pstd.Slowdown;
                            iUpgradeValue = pu.Desc[12];
                            result = iBaseValue + iUpgradeValue;
                            break;
                        case 110:
                            // 吝刀
                            iBaseValue = pstd.Tox;
                            iUpgradeValue = pu.Desc[13];
                            result = iBaseValue + iUpgradeValue;
                            break;
                        case 111:
                            // 付历亲
                            iBaseValue = HiByte(pstd.AC);
                            iUpgradeValue = pu.Desc[0];
                            result = iBaseValue + iUpgradeValue;
                            break;
                        case 112:
                            // 吝历亲
                            break;
                    }
                    break;
                case 20:
                    // case iOptionIndex of
                    // /////////////////////////////////////////////////////
                    // 格吧捞
                    switch (iOptionIndex)
                    {
                        case 100:
                            // 颇鲍
                            iBaseValue = HiByte(pstd.DC);
                            iUpgradeValue = pu.Desc[2];
                            result = iBaseValue + iUpgradeValue;
                            break;
                        case 101:
                            // 付过
                            iBaseValue = HiByte(pstd.MC);
                            iUpgradeValue = pu.Desc[3];
                            result = iBaseValue + iUpgradeValue;
                            break;
                        case 102:
                            // 档仿
                            iBaseValue = HiByte(pstd.SC);
                            iUpgradeValue = pu.Desc[4];
                            result = iBaseValue + iUpgradeValue;
                            break;
                        case 103:
                            // 规绢
                            break;
                        case 104:
                            // 付亲
                            break;
                        case 105:
                            // 郴备
                            iBaseValue = HUtil32.MathRound(pstd.DuraMax / 1000);
                            iUpgradeValue = HUtil32.MathRound(pu.DuraMax / 1000);
                            result = iUpgradeValue;
                            break;
                        case 106:
                            // 郴备
                            // 沥犬
                            iBaseValue = HiByte(pstd.AC);
                            iUpgradeValue = pu.Desc[0];
                            result = iBaseValue + iUpgradeValue;
                            break;
                        case 107:
                            // 刮酶
                            iBaseValue = HiByte(pstd.MAC);
                            iUpgradeValue = pu.Desc[1];
                            result = iBaseValue + iUpgradeValue;
                            break;
                        case 108:
                            // 傍加
                            iBaseValue = pstd.AtkSpd;
                            iUpgradeValue = pu.Desc[9];
                            result = iBaseValue + iUpgradeValue;
                            break;
                        case 109:
                            // 敌拳
                            iBaseValue = pstd.Slowdown;
                            iUpgradeValue = pu.Desc[12];
                            result = iBaseValue + iUpgradeValue;
                            break;
                        case 110:
                            // 吝刀
                            iBaseValue = pstd.Tox;
                            iUpgradeValue = pu.Desc[13];
                            result = iBaseValue + iUpgradeValue;
                            break;
                        case 111:
                            // 付历亲
                            iBaseValue = pstd.MgAvoid;
                            iUpgradeValue = pu.Desc[11];
                            result = iBaseValue + iUpgradeValue;
                            break;
                        case 112:
                            // 吝历亲
                            break;
                    }
                    break;
                case 21:
                    // case iOptionIndex of
                    // /////////////////////////////////////////////////////
                    // 格吧捞
                    switch (iOptionIndex)
                    {
                        case 100:
                            // 颇鲍
                            iBaseValue = HiByte(pstd.DC);
                            iUpgradeValue = pu.Desc[2];
                            result = iBaseValue + iUpgradeValue;
                            break;
                        case 101:
                            // 付过
                            iBaseValue = HiByte(pstd.MC);
                            iUpgradeValue = pu.Desc[3];
                            result = iBaseValue + iUpgradeValue;
                            break;
                        case 102:
                            // 档仿
                            iBaseValue = HiByte(pstd.SC);
                            iUpgradeValue = pu.Desc[4];
                            result = iBaseValue + iUpgradeValue;
                            break;
                        case 103:
                            // 规绢
                            break;
                        case 104:
                            // 付亲
                            break;
                        case 105:
                            // 郴备
                            iBaseValue = HUtil32.MathRound(pstd.DuraMax / 1000);
                            iUpgradeValue = HUtil32.MathRound(pu.DuraMax / 1000);
                            result = iUpgradeValue;
                            break;
                        case 106:
                            // 郴备
                            // 沥犬
                            iBaseValue = pstd.Accurate;
                            iUpgradeValue = pu.Desc[11];
                            result = iBaseValue + iUpgradeValue;
                            break;
                        case 107:
                            // 刮酶
                            break;
                        case 108:
                            // 傍加
                            iBaseValue = Lobyte(pstd.AC) - Lobyte(pstd.MAC) + pstd.AtkSpd;
                            iUpgradeValue = pu.Desc[9];
                            result = iBaseValue + iUpgradeValue;
                            break;
                        case 109:
                            // 敌拳
                            iBaseValue = pstd.Slowdown;
                            iUpgradeValue = pu.Desc[12];
                            result = iBaseValue + iUpgradeValue;
                            break;
                        case 110:
                            // 吝刀
                            iBaseValue = pstd.Tox;
                            iUpgradeValue = pu.Desc[13];
                            result = iBaseValue + iUpgradeValue;
                            break;
                        case 111:
                            // 付历亲
                            iBaseValue = pstd.MgAvoid;
                            iUpgradeValue = pu.Desc[7];
                            result = iBaseValue + iUpgradeValue;
                            break;
                        case 112:
                            // 吝历亲
                            break;
                    }
                    break;
                case 22:
                    // case iOptionIndex of
                    // /////////////////////////////////////////////////////
                    // 馆瘤
                    switch (iOptionIndex)
                    {
                        case 100:
                            // 颇鲍
                            iBaseValue = HiByte(pstd.DC);
                            iUpgradeValue = pu.Desc[2];
                            result = iBaseValue + iUpgradeValue;
                            break;
                        case 101:
                            // 付过
                            iBaseValue = HiByte(pstd.MC);
                            iUpgradeValue = pu.Desc[3];
                            result = iBaseValue + iUpgradeValue;
                            break;
                        case 102:
                            // 档仿
                            iBaseValue = HiByte(pstd.SC);
                            iUpgradeValue = pu.Desc[4];
                            result = iBaseValue + iUpgradeValue;
                            break;
                        case 103:
                            // 规绢
                            iBaseValue = HiByte(pstd.AC);
                            iUpgradeValue = pu.Desc[0];
                            result = iBaseValue + iUpgradeValue;
                            break;
                        case 104:
                            // 付亲
                            iBaseValue = HiByte(pstd.MAC);
                            iUpgradeValue = pu.Desc[1];
                            result = iBaseValue + iUpgradeValue;
                            break;
                        case 105:
                            // 郴备
                            iBaseValue = HUtil32.MathRound(pstd.DuraMax / 1000);
                            iUpgradeValue = HUtil32.MathRound(pu.DuraMax / 1000);
                            result = iUpgradeValue;
                            break;
                        case 106:
                            // 郴备
                            // 沥犬
                            break;
                        case 107:
                            // 刮酶
                            break;
                        case 108:
                            // 傍加
                            iBaseValue = pstd.AtkSpd;
                            iUpgradeValue = pu.Desc[9];
                            result = iBaseValue + iUpgradeValue;
                            break;
                        case 109:
                            // 敌拳
                            iBaseValue = pstd.Slowdown;
                            iUpgradeValue = pu.Desc[12];
                            result = iBaseValue + iUpgradeValue;
                            break;
                        case 110:
                            // 吝刀
                            iBaseValue = pstd.Tox;
                            iUpgradeValue = pu.Desc[13];
                            result = iBaseValue + iUpgradeValue;
                            break;
                        case 111:
                            // 付历亲
                            break;
                        case 112:
                            // 吝历亲
                            break;
                    }
                    break;
                case 23:
                    // case iOptionIndex of
                    // /////////////////////////////////////////////////////
                    // 馆瘤23
                    switch (iOptionIndex)
                    {
                        case 100:
                            // 颇鲍
                            iBaseValue = HiByte(pstd.DC);
                            iUpgradeValue = pu.Desc[2];
                            result = iBaseValue + iUpgradeValue;
                            break;
                        case 101:
                            // 付过
                            iBaseValue = HiByte(pstd.MC);
                            iUpgradeValue = pu.Desc[3];
                            result = iBaseValue + iUpgradeValue;
                            break;
                        case 102:
                            // 档仿
                            iBaseValue = HiByte(pstd.SC);
                            iUpgradeValue = pu.Desc[4];
                            result = iBaseValue + iUpgradeValue;
                            break;
                        case 103:
                            // 规绢
                            break;
                        case 104:
                            // 付亲
                            break;
                        case 105:
                            // 郴备
                            iBaseValue = HUtil32.MathRound(pstd.DuraMax / 1000);
                            iUpgradeValue = HUtil32.MathRound(pu.DuraMax / 1000);
                            result = iUpgradeValue;
                            break;
                        case 106:
                            // 郴备
                            // 沥犬
                            break;
                        case 107:
                            // 刮酶
                            break;
                        case 108:
                            // 傍加
                            iBaseValue = Lobyte(pstd.AC) - Lobyte(pstd.MAC) + pstd.AtkSpd;
                            iUpgradeValue = pu.Desc[9];
                            result = iBaseValue + iUpgradeValue;
                            break;
                        case 109:
                            // 敌拳
                            iBaseValue = pstd.Slowdown;
                            iUpgradeValue = pu.Desc[12];
                            result = iBaseValue + iUpgradeValue;
                            break;
                        case 110:
                            // 吝刀
                            iBaseValue = pstd.Tox;
                            iUpgradeValue = pu.Desc[13];
                            result = iBaseValue + iUpgradeValue;
                            break;
                        case 111:
                            // 付历亲
                            break;
                        case 112:
                            // 吝历亲
                            break;
                    }
                    break;
                case 24:
                    // case iOptionIndex of
                    // /////////////////////////////////////////////////////
                    // 迫骂24
                    switch (iOptionIndex)
                    {
                        case 100:
                            // 颇鲍
                            iBaseValue = HiByte(pstd.DC);
                            iUpgradeValue = pu.Desc[2];
                            result = iBaseValue + iUpgradeValue;
                            break;
                        case 101:
                            // 付过
                            iBaseValue = HiByte(pstd.MC);
                            iUpgradeValue = pu.Desc[3];
                            result = iBaseValue + iUpgradeValue;
                            break;
                        case 102:
                            // 档仿
                            iBaseValue = HiByte(pstd.SC);
                            iUpgradeValue = pu.Desc[4];
                            result = iBaseValue + iUpgradeValue;
                            break;
                        case 103:
                            // 规绢
                            break;
                        case 104:
                            // 付亲
                            break;
                        case 105:
                            // 郴备
                            iBaseValue = HUtil32.MathRound(pstd.DuraMax / 1000);
                            iUpgradeValue = HUtil32.MathRound(pu.DuraMax / 1000);
                            result = iUpgradeValue;
                            break;
                        case 106:
                            // 郴备
                            // 沥犬
                            iBaseValue = HiByte(pstd.AC);
                            iUpgradeValue = pu.Desc[0];
                            result = iBaseValue + iUpgradeValue;
                            break;
                        case 107:
                            // 刮酶
                            iBaseValue = HiByte(pstd.MAC);
                            iUpgradeValue = pu.Desc[1];
                            result = iBaseValue + iUpgradeValue;
                            break;
                        case 108:
                            // 傍加
                            break;
                        case 109:
                            // 敌拳
                            break;
                        case 110:
                            // 吝刀
                            break;
                        case 111:
                            // 付历亲
                            break;
                        case 112:
                            // 吝历亲
                            break;
                    }
                    break;
                case 26:
                    // case iOptionIndex of
                    // /////////////////////////////////////////////////////
                    // 迫骂
                    switch (iOptionIndex)
                    {
                        case 100:
                            // 颇鲍
                            iBaseValue = HiByte(pstd.DC);
                            iUpgradeValue = pu.Desc[2];
                            result = iBaseValue + iUpgradeValue;
                            break;
                        case 101:
                            // 付过
                            iBaseValue = HiByte(pstd.MC);
                            iUpgradeValue = pu.Desc[3];
                            result = iBaseValue + iUpgradeValue;
                            break;
                        case 102:
                            // 档仿
                            iBaseValue = HiByte(pstd.SC);
                            iUpgradeValue = pu.Desc[4];
                            result = iBaseValue + iUpgradeValue;
                            break;
                        case 103:
                            // 规绢
                            iBaseValue = HiByte(pstd.AC);
                            iUpgradeValue = pu.Desc[0];
                            result = iBaseValue + iUpgradeValue;
                            break;
                        case 104:
                            // 付亲
                            iBaseValue = HiByte(pstd.MAC);
                            iUpgradeValue = pu.Desc[1];
                            result = iBaseValue + iUpgradeValue;
                            break;
                        case 105:
                            // 郴备
                            iBaseValue = HUtil32.MathRound(pstd.DuraMax / 1000);
                            iUpgradeValue = HUtil32.MathRound(pu.DuraMax / 1000);
                            result = iUpgradeValue;
                            break;
                        case 106:
                            // 郴备
                            // 沥犬
                            iBaseValue = pstd.Accurate;
                            iUpgradeValue = pu.Desc[11];
                            result = iBaseValue + iUpgradeValue;
                            break;
                        case 107:
                            // 刮酶
                            iBaseValue = pstd.Agility;
                            iUpgradeValue = pu.Desc[12];
                            result = iBaseValue + iUpgradeValue;
                            break;
                        case 108:
                            // 傍加
                            break;
                        case 109:
                            // 敌拳
                            break;
                        case 110:
                            // 吝刀
                            break;
                        case 111:
                            // 付历亲
                            break;
                        case 112:
                            // 吝历亲
                            break;
                    }
                    break;
                case 52:
                    // case iOptionIndex of
                    // /////////////////////////////////////////////////////
                    // 脚惯
                    switch (iOptionIndex)
                    {
                        case 100:
                            // 颇鲍
                            break;
                        case 101:
                            // 付过
                            break;
                        case 102:
                            // 档仿
                            break;
                        case 103:
                            // 规绢
                            iBaseValue = HiByte(pstd.AC);
                            iUpgradeValue = pu.Desc[0];
                            result = iBaseValue + iUpgradeValue;
                            break;
                        case 104:
                            // 付亲
                            iBaseValue = HiByte(pstd.MAC);
                            iUpgradeValue = pu.Desc[1];
                            result = iBaseValue + iUpgradeValue;
                            break;
                        case 105:
                            // 郴备
                            iBaseValue = HUtil32.MathRound(pstd.DuraMax / 1000);
                            iUpgradeValue = HUtil32.MathRound(pu.DuraMax / 1000);
                            result = iUpgradeValue;
                            break;
                        case 106:
                            // 郴备
                            // 沥犬
                            break;
                        case 107:
                            // 刮酶
                            iBaseValue = pstd.Agility;
                            iUpgradeValue = pu.Desc[3];
                            result = iBaseValue + iUpgradeValue;
                            break;
                        case 108:
                            // 傍加
                            break;
                        case 109:
                            // 敌拳
                            break;
                        case 110:
                            // 吝刀
                            break;
                        case 111:
                            // 付历亲
                            break;
                        case 112:
                            // 吝历亲
                            break;
                    }
                    break;
                case 54:
                    // case iOptionIndex of
                    // /////////////////////////////////////////////////////
                    // 骇飘
                    switch (iOptionIndex)
                    {
                        case 100:
                            // 颇鲍
                            break;
                        case 101:
                            // 付过
                            break;
                        case 102:
                            // 档仿
                            break;
                        case 103:
                            // 规绢
                            iBaseValue = HiByte(pstd.AC);
                            iUpgradeValue = pu.Desc[0];
                            result = iBaseValue + iUpgradeValue;
                            break;
                        case 104:
                            // 付亲
                            iBaseValue = HiByte(pstd.MAC);
                            iUpgradeValue = pu.Desc[1];
                            result = iBaseValue + iUpgradeValue;
                            break;
                        case 105:
                            // 郴备
                            iBaseValue = HUtil32.MathRound(pstd.DuraMax / 1000);
                            iUpgradeValue = HUtil32.MathRound(pu.DuraMax / 1000);
                            result = iUpgradeValue;
                            break;
                        case 106:
                            // 郴备
                            // 沥犬
                            iBaseValue = pstd.Accurate;
                            iUpgradeValue = pu.Desc[2];
                            result = iBaseValue + iUpgradeValue;
                            break;
                        case 107:
                            // 刮酶
                            iBaseValue = pstd.Agility;
                            iUpgradeValue = pu.Desc[3];
                            result = iBaseValue + iUpgradeValue;
                            break;
                        case 108:
                            // 傍加
                            break;
                        case 109:
                            // 敌拳
                            break;
                        case 110:
                            // 吝刀
                            break;
                        case 111:
                            // 付历亲
                            break;
                        case 112:
                            // 吝历亲
                            iBaseValue = pstd.ToxAvoid;
                            iUpgradeValue = pu.Desc[13];
                            result = iBaseValue + iUpgradeValue;
                            break;
                    }
                    break;
                    // case iOptionIndex of
            }
            return result;
        }

        public void CmdMakeAllJewelryItem(int nSelect)
        {
            if (nSelect == 0)
            {
                CmdMakeItem("BraveryGem", 1);
                CmdMakeItem("MagicGem", 1);
                CmdMakeItem("SoulGem", 1);
                CmdMakeItem("ProtectionGem", 1);
                CmdMakeItem("EvilSlayerGem", 1);
                CmdMakeItem("DurabilityGem", 1);
                CmdMakeItem("AccuracyGem", 1);
                CmdMakeItem("AgilityGem", 1);
                CmdMakeItem("StormGem", 1);
                CmdMakeItem("FreezingGem", 1);
                CmdMakeItem("PoisonGem", 1);
                CmdMakeItem("DisillusionGem", 1);
                CmdMakeItem("EnduranceGem", 1);
            }
            else
            {
                CmdMakeItem("BraveryOrb", 1);
                CmdMakeItem("MagicOrb", 1);
                CmdMakeItem("SoulOrb", 1);
                CmdMakeItem("ProtectionOrb", 1);
                CmdMakeItem("EvilSlayerOrb", 1);
                CmdMakeItem("DurabilityOrb", 1);
                CmdMakeItem("AccuracyOrb", 1);
                CmdMakeItem("AgilityOrb", 1);
                CmdMakeItem("StormOrb", 1);
                CmdMakeItem("FreezingOrb", 1);
                CmdMakeItem("PoisonOrb", 1);
                CmdMakeItem("DisillusionOrb", 1);
                CmdMakeItem("EnduranceOrb", 1);
            }
        }

        // 2003/09/15 盲泼肺弊 包访 疙飞绢 眠啊
        // /////////////////////////////////////////////////////////////
        // 2003/09/15 眉泼肺弊 疙飞 眠啊
        public void CmdAddChatLogList(string whostr, bool pass)
        {
            int idx;
            bool bExist;
            if (whostr != "")
            {
                bExist = svMain.UserEngine.FindChatLogList(whostr, ref idx);
                if (!bExist)
                {
                    svMain.UserEngine.ChatLogList.Add(whostr);
                    if (pass)
                    {
                        LocalDB.FrmDB.SaveChatLogFiles();
                        svMain.UserEngine.SendInterMsg(Grobal2.ISM_RELOADCHATLOG, svMain.ServerIndex, "");
                    }
                    this.SysMsg(whostr + "添加到说话日志列表中", 1);
                }
                else
                {
                    this.SysMsg(whostr + "已经在说话日志列表中", 0);
                }
            }
            else
            {
                this.SysMsg(whostr + "无法被找到。", 0);
            }
        }

        public void CmdDelChatLogList(string whostr, bool pass)
        {
            int idx;
            bool bExist;
            bExist = svMain.UserEngine.FindChatLogList(whostr, ref idx);
            if (bExist)
            {
                svMain.UserEngine.ChatLogList.Remove(idx);
                if (pass)
                {
                    LocalDB.FrmDB.SaveChatLogFiles();
                    svMain.UserEngine.SendInterMsg(Grobal2.ISM_RELOADCHATLOG, svMain.ServerIndex, "");
                }
                this.SysMsg(whostr + "从说话日志列表中删除。", 1);
            }
            else
            {
                this.SysMsg(whostr + "无法被找到。", 0);
            }
        }

        public void CmdSendChatLogList()
        {
            int i;
            for (i = 0; i < svMain.UserEngine.ChatLogList.Count; i++)
            {
                this.SysMsg((i + 1).ToString() + "=" + svMain.UserEngine.ChatLogList[i], 1);
            }
        }

        // 巩颇厘盔 包访 款康磊 疙飞绢(sonmg)
        // -------巩颇厘盔 款康磊 疙飞绢---------------------------------------------
        public void CmdGuildAgitRegistration()
        {
            int agitnumber;
            if (svMain.ServerIndex != 0)
            {
                this.SysMsg("这个命令不能的在这个服务器上使用。", 0);
                return;
            }
            if (this.IsGuildMaster())
            {
                // 巩林捞搁
                if (((TGuild)this.MyGuild).GetTotalMemberCount() <= ObjBase.MINAGITMEMBER)
                {
                    this.BoxMsg("门派不能少于" + ObjBase.MINAGITMEMBER.ToString() + "个成员。", 0);
                    return;
                }
                // 措咯窍妨绰 巩林啊 厘盔 备涝脚没阑 茄 惑怕牢瘤 八荤
                if (svMain.GuildAgitMan.IsExistInForSaleGuild(((TGuild)this.MyGuild).GuildName))
                {
                    this.BoxMsg("你已经要求购买了。", 0);
                    return;
                }
                if (this.Gold >= Guild.GUILDAGITREGFEE)
                {
                    agitnumber = svMain.GuildAgitMan.AddGuildAgit(((TGuild)this.MyGuild).GuildName, ((TGuild)this.MyGuild).GetGuildMaster(), ((TGuild)this.MyGuild).GetAnotherGuildMaster());
                    if (agitnumber > -1)
                    {
                        this.DecGold(Guild.GUILDAGITREGFEE);
                        this.GoldChanged();
                        // 肺弊巢辫
                        // 厘措咯_
                        svMain.AddUserLog("37\09" + this.MapName + "\09" + this.CX.ToString() + "\09" + this.CY.ToString() + "\09" + this.UserName + "\09" + ((TGuild)this.MyGuild).GuildName + "\09" + agitnumber.ToString() + "\09" + "1\09" + Guild.GUILDAGITREGFEE.ToString());
                        // 厘盔霸矫魄 府肺靛.
                        svMain.GuildAgitBoardMan.LoadAllGaBoardList("");
                        svMain.UserEngine.SendInterMsg(Grobal2.ISM_RELOADGUILDAGIT, svMain.ServerIndex, "");
                        this.BoxMsg("您已经租用了门派庄园。", 1);
                    }
                    else
                    {
                        this.BoxMsg("您不能租用门派庄园。", 0);
                    }
                }
                else
                {
                    this.BoxMsg("缺少金币。", 0);
                }
            }
            else
            {
                this.BoxMsg("仅门派门主才可以使用这个命令。", 0);
            }
        }

        public void CmdGuildAgitAutoMove()
        {
            string MapName;
            TGuildAgit guildagit;
            if (this.MyGuild == null)
            {
                this.BoxMsg("您不能移动到门派庄园。", 0);
                return;
            }
            if (((TGuild)this.MyGuild).GuildName == "")
            {
                this.BoxMsg("您不能移动到门派庄园。", 0);
                return;
            }
            guildagit = svMain.GuildAgitMan.GetGuildAgit(((TGuild)this.MyGuild).GuildName);
            if (guildagit != null)
            {
                MapName = svMain.GuildAgitMan.GuildAgitMapName[0] + guildagit.GuildAgitNumber.ToString();
                if (svMain.GrobalEnvir.GetEnvir(MapName) != null)
                {
                    this.SendRefMsg(Grobal2.RM_SPACEMOVE_HIDE, 0, 0, 0, 0, "");
                    // RandomSpaceMove (MapName, 0); //公累困 傍埃捞悼
                    // SpaceMove (MapName, GuildAgitMan.EntranceX, GuildAgitMan.EntranceY, 0); //傍埃捞悼
                    this.UserSpaceMove(MapName, svMain.GuildAgitMan.EntranceX.ToString(), svMain.GuildAgitMan.EntranceY.ToString());
                    // 傍埃捞悼
                    this.SysMsg("你已进入门派庄园。", 1);
                }
            }
            else
            {
                this.BoxMsg("不能使用。", 0);
            }
        }

        public void CmdGuildAgitDelete()
        {
            if (svMain.ServerIndex != 0)
            {
                this.SysMsg("这个命令不能的在这个服务器上使用。", 0);
                return;
            }
            if (this.IsMyGuildMaster())
            {
                // 巩林捞搁
                if (svMain.GuildAgitMan.DelGuildAgit(((TGuild)this.MyGuild).GuildName))
                {
                    // 厘盔霸矫魄 府肺靛.
                    svMain.GuildAgitBoardMan.LoadAllGaBoardList("");
                    svMain.UserEngine.SendInterMsg(Grobal2.ISM_RELOADGUILDAGIT, svMain.ServerIndex, "");
                    this.SysMsg("您退租门派庄园。", 1);
                }
                else
                {
                    this.SysMsg("您不能退租门派庄园。", 0);
                }
            }
            else
            {
                this.SysMsg("仅门派门主才可以使用这个命令。", 0);
            }
        }

        public void CmdGuildAgitExtendTime(int count)
        {
            int agitnumber;
            if (svMain.ServerIndex != 0)
            {
                this.SysMsg("这个命令不能的在这个服务器上使用。", 0);
                return;
            }
            if (this.IsMyGuildMaster())
            {
                // 巩林捞搁
                if (this.Gold >= Guild.GUILDAGITEXTENDFEE)
                {
                    agitnumber = svMain.GuildAgitMan.ExtendTime(count, ((TGuild)this.MyGuild).GuildName);
                    if (agitnumber > -1)
                    {
                        this.DecGold(Guild.GUILDAGITEXTENDFEE);
                        this.GoldChanged();
                        // 肺弊巢辫
                        // 厘楷厘_
                        svMain.AddUserLog("38\09" + this.MapName + "\09" + this.CX.ToString() + "\09" + this.CY.ToString() + "\09" + this.UserName + "\09" + ((TGuild)this.MyGuild).GuildName + "\09" + agitnumber.ToString() + "\09" + "1\09" + Guild.GUILDAGITEXTENDFEE.ToString());
                        svMain.UserEngine.SendInterMsg(Grobal2.ISM_RELOADGUILDAGIT, svMain.ServerIndex, "");
                        this.BoxMsg("领土租用期限已被延长。", 1);
                    }
                    else
                    {
                        this.BoxMsg("庄园租用期限不能被延长。", 0);
                    }
                }
                else
                {
                    this.BoxMsg("缺少金币。", 0);
                }
            }
            else
            {
                this.BoxMsg("仅门派门主才可以使用这个命令。", 0);
            }
        }

        public void CmdGuildAgitRemainTime()
        {
            DateTime RemainDateTime;
            DateTime BaseDate;
            DateTime DiffDate;
            int RemainDay;
            short Year;
            short Month;
            short Day;
            short Hour;
            short Min;
            short Sec;
            short MSec;
            if (this.MyGuild == null)
            {
                this.BoxMsg("您不能检查它。", 0);
                return;
            }
            if (((TGuild)this.MyGuild).GuildName == "")
            {
                this.BoxMsg("您不能检查它。", 0);
                return;
            }
            RemainDateTime = svMain.GuildAgitMan.GetRemainDateTime(((TGuild)this.MyGuild).GuildName);
            // 俊矾 皋矫瘤.
            // {$IFNDEF UNDEF_DEBUG}   //sonmg
            // if RemainDateTime < -GUILDAGIT_DAYUNIT then begin
            // {$ELSE}
            if (RemainDateTime < -(Guild.GUILDAGIT_DAYUNIT / 60 / 24))
            {
                // {$ENDIF}
                // 皋矫瘤 免仿.
                this.BoxMsg("不能使用。", 0);
                return;
            }
            // 措咯 扁埃捞 瘤车阑 版快(楷眉) 皋矫瘤.
            if (RemainDateTime <= 0)
            {
                // {$IFNDEF UNDEF_DEBUG}   //sonmg
                // 巢篮 朝楼肺 函券.
                // RemainDateTime := GUILDAGIT_DAYUNIT + RemainDateTime;
                // // 瘤抄 朝楼狼 家荐痢 捞窍甫 滚覆
                // RemainDateTime := Trunc( RemainDateTime );
                // 皋矫瘤 免仿.
                // BoxMsg( '庄园租用期限将到期。\ \你的门派庄园将于' + FloatToStr( RemainDateTime ) + '天后到期。', 0 );
                // {$ELSE}
                // 巢篮 朝楼肺 函券.
                RemainDateTime = (Guild.GUILDAGIT_DAYUNIT / 60 / 24) + RemainDateTime;
                // 瘤抄 朝楼狼 家荐痢 捞窍甫 滚覆
                RemainDateTime = Convert.ToInt64(RemainDateTime * 60 * 24);
                // 皋矫瘤 免仿.
                this.BoxMsg("庄园租用期限将到期。\\ \\你的门派庄园将于" + Convert.ToString(RemainDateTime) + "分钟后到期。", 0);
                // {$ENDIF}
            }
            else
            {
                // 朝楼 盒秦.
                Year = (short)RemainDateTime.Year;
                Month = (short)RemainDateTime.Month;
                Day = (short)RemainDateTime.Day;
                Hour = (short)RemainDateTime.Hour;
                Min = (short)RemainDateTime.Minute;
                Sec = (short)RemainDateTime.Second;
                MSec = (short)RemainDateTime.Millisecond;
                // 1899斥 12岿 31老 扁霖.
                BaseDate = new DateTime(1899, 12, 31);
                // 巢篮 老荐 拌魂.
                DiffDate = RemainDateTime - BaseDate + 1;
                RemainDay = (int)Convert.ToInt64(DiffDate);
                // 皋矫瘤 免仿.
                this.BoxMsg("门派庄园租用时间于 < " + RemainDay.ToString() + "天" + Hour.ToString() + "小时" + Min.ToString() + "分 > " + "后到期。", 1);
            }
        }

        public void CmdGuildAgitRecall(string man, bool WholeRecall)
        {
            int i;
            int k;
            int n;
            TGuildRank pgrank;
            if (this.IsMyGuildMaster())
            {
                // 巩林捞搁
                // 傈眉家券.
                if (WholeRecall)
                {
                    n = (int)((HUtil32.GetTickCount() - this.CGHIstart) / 1000);
                    this.CGHIstart = this.CGHIstart + ((long)n * 1000);
                    if (this.CGHIUseTime > n)
                    {
                        this.CGHIUseTime = (ushort)(this.CGHIUseTime - n);
                    }
                    else
                    {
                        this.CGHIUseTime = 0;
                    }
                    if (this.CGHIUseTime == 0)
                    {
                        if (this.MyGuild != null)
                        {
                            if (((TGuild)this.MyGuild).MemberList != null)
                            {
                                for (i = 0; i < ((TGuild)this.MyGuild).MemberList.Count; i++)
                                {
                                    pgrank = (TGuildRank)((TGuild)this.MyGuild).MemberList[i];
                                    for (k = 0; k < pgrank.MemList.Count; k++)
                                    {
                                        if (pgrank.MemList.Values[k] == this)
                                        {
                                            continue;
                                        }
                                        // 磊扁 磊脚篮 逞绢皑.
                                        GuildMasterRecallMan((string)pgrank.MemList[k], false);
                                    }
                                    this.CGHIstart  =  HUtil32.GetTickCount();
                                    this.CGHIUseTime = 3 * 60;
                                }
                            }
                        }
                    }
                    else
                    {
                        this.SysMsg("你需要在" + this.CGHIUseTime.ToString() + "秒后才能使用门派成员召回功能。", 0);
                    }
                    // 俺牢家券.
                }
                else
                {
                    GuildMasterRecallMan(man, true);
                }
            }
            else
            {
                this.BoxMsg("仅门派门主才可以使用这个命令。", 0);
            }
        }

        // 檬措鼻 捞悼.
        public void CmdGuildAgitFreeMove(int AgitNum)
        {
            string MapName;
            TGuildAgit guildagit;
            string gname;
            if (AgitNum <= 0)
            {
                return;
            }
            gname = svMain.GuildAgitMan.GetGuildNameFromAgitNum(AgitNum);
            guildagit = svMain.GuildAgitMan.GetGuildAgit(gname);
            if (guildagit != null)
            {
                MapName = svMain.GuildAgitMan.GuildAgitMapName[0] + guildagit.GuildAgitNumber.ToString();
                if (svMain.GrobalEnvir.GetEnvir(MapName) != null)
                {
                    this.SendRefMsg(Grobal2.RM_SPACEMOVE_HIDE, 0, 0, 0, 0, "");
                    // RandomSpaceMove (MapName, 0); //公累困 傍埃捞悼
                    // SpaceMove (MapName, GuildAgitMan.EntranceX, GuildAgitMan.EntranceY, 0); //傍埃捞悼
                    this.UserSpaceMove(MapName, svMain.GuildAgitMan.EntranceX.ToString(), svMain.GuildAgitMan.EntranceY.ToString());
                    // 傍埃捞悼
                    this.SysMsg("你已进入门派庄园。", 1);
                }
            }
        }

        // 巩颇厘盔 魄概包访
        public void CmdGuildAgitSale(string StrForSaleGold)
        {
            TGuildAgit guildagit;
            int salegold;
            if (svMain.ServerIndex != 0)
            {
                this.SysMsg("这个命令不能的在这个服务器上使用。", 0);
                return;
            }
            if (this.IsMyGuildMaster())
            {
                // 巩林捞搁
                if (StrForSaleGold.Length > 10)
                {
                    this.SysMsg("你没有输入出售金额或输入金额没有超出限制。", 0);
                    return;
                }
                salegold = HUtil32.Str_ToInt(StrForSaleGold, 0);
                if (salegold <= 0)
                {
                    this.SysMsg("你没有输入出售金额或输入金额没有超出限制。", 0);
                    return;
                }
                // 磊扁 磊脚狼 巩颇 厘盔阑 茫绰促.
                guildagit = svMain.GuildAgitMan.GetGuildAgit(((TGuild)this.MyGuild).GuildName);
                if (guildagit != null)
                {
                    if (guildagit.GetCurrentDelayStatus() <= 0)
                    {
                        this.BoxMsg("庄园租用期限将到期，因此您不能出售门派庄园。", 0);
                        return;
                    }
                    // 魄概吝捞 酒聪搁
                    if (!guildagit.IsForSale())
                    {
                        // 芭贰啊 己荤登瘤 臼疽栏搁
                        if (!guildagit.IsSoldOut())
                        {
                            // 敲贰弊 眉农
                            guildagit.ForSaleFlag = 1;
                            // 魄概 陛咀 殿废
                            guildagit.ForSaleMoney = salegold;
                            // 肺弊巢辫
                            // 厘魄概_
                            svMain.AddUserLog("39\09" + this.MapName + "\09" + this.CX.ToString() + "\09" + this.CY.ToString() + "\09" + this.UserName + "\09" + ((TGuild)this.MyGuild).GuildName + "\09" + guildagit.GuildAgitNumber.ToString() + "\09" + "1\09" + guildagit.ForSaleMoney.ToString());
                            // 巩颇 厘盔 府胶飘 历厘.
                            svMain.GuildAgitMan.SaveGuildAgitList(false);
                            svMain.UserEngine.SendInterMsg(Grobal2.ISM_RELOADGUILDAGIT, svMain.ServerIndex, "");
                            // 皋矫瘤 免仿.
                            this.BoxMsg(M2Share.GetGoldStr(salegold) + "金币的价格登记出售门派庄园。", 1);
                        }
                        else
                        {
                            this.BoxMsg("交易已完成，因此它不能再被出售。", 0);
                        }
                    }
                    else
                    {
                        this.BoxMsg("您的门派庄园当前正在销售中。", 0);
                    }
                }
                else
                {
                    this.BoxMsg("不能使用。", 0);
                }
            }
            else
            {
                this.BoxMsg("仅门派门主才可以使用这个命令。", 0);
            }
        }

        public void CmdGuildAgitSaleCancel()
        {
            TGuildAgit guildagit;
            if (svMain.ServerIndex != 0)
            {
                this.SysMsg("这个命令不能的在这个服务器上使用。", 0);
                return;
            }
            if (this.IsMyGuildMaster())
            {
                // 巩林捞搁
                // 磊扁 磊脚狼 巩颇 厘盔阑 茫绰促.
                guildagit = svMain.GuildAgitMan.GetGuildAgit(((TGuild)this.MyGuild).GuildName);
                if (guildagit != null)
                {
                    // 芭贰啊 己荤等 饶俊绰 魄概秒家甫 且 荐 绝促.
                    if (!guildagit.IsSoldOut())
                    {
                        // 魄概吝捞搁
                        if (guildagit.IsForSale())
                        {
                            // 魄概秒家茄促.
                            guildagit.ResetForSaleFields();
                            // 肺弊巢辫
                            // 厘秒家_(厘盔魄概秒家)
                            svMain.AddUserLog("40\09" + this.MapName + "\09" + this.CX.ToString() + "\09" + this.CY.ToString() + "\09" + this.UserName + "\09" + ((TGuild)this.MyGuild).GuildName + "\09" + guildagit.GuildAgitNumber.ToString() + "\09" + "1\09" + "0");
                            // 巩颇 厘盔 府胶飘 历厘.
                            svMain.GuildAgitMan.SaveGuildAgitList(false);
                            svMain.UserEngine.SendInterMsg(Grobal2.ISM_RELOADGUILDAGIT, svMain.ServerIndex, "");
                            this.BoxMsg("您取消了门派庄园的销售。", 1);
                        }
                        else
                        {
                            this.BoxMsg("您的门派庄园不在销售中。", 0);
                        }
                    }
                    else
                    {
                        this.BoxMsg("交易已完成，您不能取消销售。", 0);
                    }
                }
                else
                {
                    this.BoxMsg("不能使用。", 0);
                }
            }
            else
            {
                this.BoxMsg("仅门派门主才可以使用这个命令。", 0);
            }
        }

        public void CmdGuildAgitBuy(int page)
        {
            const int ONEPAGELINE = 10;
            ArrayList salelist;
            int i;
            int count;
            int startline;
            int endline;
            string data = string.Empty;
            // 厘盔狼 傈眉 府胶飘甫 焊郴 淋.
            salelist = null;
            data = "";
            count = 0;
            // 厘盔 魄概 格废阑 掘绢柯促.
            svMain.GuildAgitMan.GetGuildAgitSaleList(ref salelist);
            // 厘盔 格废捞 绝阑 锭
            if (salelist == null)
            {
                this.BoxMsg("这是没有登记的门派庄园。", 0);
                return;
            }
            // 其捞瘤啊 沥惑捞搁
            if (page > 0)
            {
                // 矫累临
                startline = ONEPAGELINE * (page - 1);
                // 付瘤阜临 : 格废狼 扼牢 荐啊 茄 其捞瘤狼 弥措 扼牢焊促 农瘤 臼霸
                endline = _MIN(salelist.Count, ONEPAGELINE * page);
                // 矫累临何磐 付瘤阜临鳖瘤 焊晨
                for (i = startline; i < endline; i++)
                {
                    data = data + salelist[i] + "/";
                    count++;
                }
                if (count > 0)
                {
                    // 府胶飘甫 焊晨
                    this.SendMsg(this, Grobal2.RM_GUILDAGITLIST, 0, page, count, 0, data);
                }
            }
            // 格废阑 皋葛府俊辑 秦力矫挪促.
            salelist.Free();
        }

        public void CmdTryGuildAgitTrade()
        {
            if (svMain.ServerIndex != 0)
            {
                this.SysMsg("这个命令不能的在这个服务器上使用。", 0);
                return;
            }
            this.SendMsg(this, Grobal2.RM_GUILDAGITDEALTRY, 0, this.ActorId, 0, 0, "");
        }

        // 巩颇厘盔 磊扁 磊脚 眠规(sonmg)
        public void CmdGuildAgitExpulsionMyself()
        {
            TGuildAgit guildagit;
            // 厘盔 郴俊 乐绰 荤恩父 眠规.
            if ((this.PEnvir.GetGuildAgitRealMapName() == svMain.GuildAgitMan.GuildAgitMapName[0]) || (this.PEnvir.GetGuildAgitRealMapName() == svMain.GuildAgitMan.GuildAgitMapName[1]) || (this.PEnvir.GetGuildAgitRealMapName() == svMain.GuildAgitMan.GuildAgitMapName[2]) || (this.PEnvir.GetGuildAgitRealMapName() == svMain.GuildAgitMan.GuildAgitMapName[3]))
            {
                // 巩颇啊 绝阑 版快
                if ((this.MyGuild == null) || (((TGuild)this.MyGuild).GuildName == ""))
                {
                    // 磊扁 磊脚阑 碍力 眠规
                    this.SendRefMsg(Grobal2.RM_SPACEMOVE_HIDE, 0, 0, 0, 0, "");
                    // UserSpaceMove (GuildAgitMan.ReturnMapName, IntToStr(GuildAgitMan.ReturnX), IntToStr(GuildAgitMan.ReturnY));
                    this.UserSpaceMove(this.HomeMap, this.HomeX.ToString(), this.HomeY.ToString());
                    return;
                }
                guildagit = svMain.GuildAgitMan.GetGuildAgit(((TGuild)this.MyGuild).GuildName);
                // 厘盔捞 绝阑 版快
                if (guildagit == null)
                {
                    if (HUtil32.CompareLStr(this.PEnvir.MapName, svMain.GuildAgitMan.GuildAgitMapName[0], 3) || HUtil32.CompareLStr(this.PEnvir.MapName, svMain.GuildAgitMan.GuildAgitMapName[1], 3) || HUtil32.CompareLStr(this.PEnvir.MapName, svMain.GuildAgitMan.GuildAgitMapName[2], 3) || HUtil32.CompareLStr(this.PEnvir.MapName, svMain.GuildAgitMan.GuildAgitMapName[3], 3))
                    {
                        // 磊扁 磊脚阑 碍力 眠规
                        this.SendRefMsg(Grobal2.RM_SPACEMOVE_HIDE, 0, 0, 0, 0, "");
                        // UserSpaceMove (GuildAgitMan.ReturnMapName, IntToStr(GuildAgitMan.ReturnX), IntToStr(GuildAgitMan.ReturnY));
                        this.UserSpaceMove(this.HomeMap, this.HomeX.ToString(), this.HomeY.ToString());
                    }
                }
                else
                {
                    // 厘盔捞 乐阑 版快(sonmg 2005/02/23)
                    // 巢狼 厘盔捞芭唱 措咯 扁埃捞 父丰登菌栏搁 眠规
                    if ((guildagit.GuildAgitNumber != this.PEnvir.GuildAgit) || guildagit.IsExpired())
                    {
                        // 磊扁 磊脚阑 碍力 眠规
                        this.SendRefMsg(Grobal2.RM_SPACEMOVE_HIDE, 0, 0, 0, 0, "");
                        // UserSpaceMove (GuildAgitMan.ReturnMapName, IntToStr(GuildAgitMan.ReturnX), IntToStr(GuildAgitMan.ReturnY));
                        this.UserSpaceMove(this.HomeMap, this.HomeX.ToString(), this.HomeY.ToString());
                    }
                }
            }
        }

        // 厘盔 扁何陛
        public void CmdGuildAgitDonate(string goldstr)
        {
            int GoldDonate;
            string gname;
            TGuildAgit guildagit;
            gname = this.GetGuildNameHereAgit();
            if (gname == "")
            {
                this.BoxMsg("不能支付。", 0);
                return;
            }
            guildagit = svMain.GuildAgitMan.GetGuildAgit(gname);
            if (guildagit != null)
            {
                GoldDonate = HUtil32.Str_ToInt(goldstr, 0);
                if (this.Gold < GoldDonate)
                {
                    this.BoxMsg("缺少金币。", 0);
                    return;
                }
                if (guildagit.GuildAgitTotalGold + GoldDonate > Guild.GUILDAGITMAXGOLD)
                {
                    this.BoxMsg("超出捐赠限制。\\\\捐赠的总数不能超出" + M2Share.GetGoldStr(Guild.GUILDAGITMAXGOLD) + "金币。", 0);
                    return;
                }
                // 啊规芒俊辑 陛傈阑 皑家矫糯.
                if (this.DecGold(GoldDonate))
                {
                    this.GoldChanged();
                    // 厘盔 扁何陛俊 捣阑 眠啊窃.
                    guildagit.GuildAgitTotalGold = guildagit.GuildAgitTotalGold + GoldDonate;
                    this.BoxMsg("捐赠" + M2Share.GetGoldStr(GoldDonate) + "金币。\\\\你平均捐赠" + M2Share.GetGoldStr(guildagit.GuildAgitTotalGold) + "金币。", 0);
                    // 肺弊巢辫
                    // 扁何_
                    // '陛傈'
                    svMain.AddUserLog("46\09" + this.MapName + "\09" + this.CX.ToString() + "\09" + this.CY.ToString() + "\09" + this.UserName + "\09" + Envir.NAME_OF_GOLD + "\09" + GoldDonate.ToString() + "\09" + "0\09" + "0");
                    // 巩颇 厘盔 府胶飘 历厘.
                    svMain.GuildAgitMan.SaveGuildAgitList(false);
                }
                else
                {
                    this.BoxMsg("不能支付。", 0);
                }
            }
            else
            {
                this.BoxMsg("不能支付。", 0);
            }
        }

        public void CmdGuildAgitViewDonation()
        {
            string gname;
            TGuildAgit guildagit;
            gname = this.GetGuildNameHereAgit();
            if (gname == "")
            {
                this.BoxMsg("不能查寻。", 0);
                return;
            }
            guildagit = svMain.GuildAgitMan.GetGuildAgit(gname);
            if (guildagit != null)
            {
                this.BoxMsg("当前剩余捐赠的数量是" + M2Share.GetGoldStr(guildagit.GuildAgitTotalGold) + "金币。", 0);
            }
        }

        public int GetGuildAgitDonation()
        {
            int result;
            string gname;
            TGuildAgit guildagit;
            result = 0;
            gname = this.GetGuildNameHereAgit();
            if (gname == "")
            {
                return result;
            }
            guildagit = svMain.GuildAgitMan.GetGuildAgit(gname);
            if (guildagit != null)
            {
                result = guildagit.GuildAgitTotalGold;
            }
            return result;
        }

        public bool DecGuildAgitDonation(int igold)
        {
            bool result;
            string gname;
            TGuildAgit guildagit;
            result = false;
            gname = this.GetGuildNameHereAgit();
            if (gname == "")
            {
                return result;
            }
            guildagit = svMain.GuildAgitMan.GetGuildAgit(gname);
            if (guildagit != null)
            {
                if (guildagit.GuildAgitTotalGold < igold)
                {
                    return result;
                }
                guildagit.GuildAgitTotalGold = guildagit.GuildAgitTotalGold - igold;
                result = true;
            }
            return result;
        }

        // 厘盔 颇老 滚傈
        public void CmdGetGuildAgitFileVersion()
        {
            this.SysMsg("Version=" + svMain.GuildAgitMan.GuildAgitFileVersion.ToString(), 0);
        }

        public void SendAddItem(TUserItem ui)
        {
            TClientItem citem = null;
            TStdItem ps;
            TStdItem std;
            int opt;
            ps = svMain.UserEngine.GetStdItem(ui.Index);
            if (ps != null)
            {
                std = ps;
                opt = svMain.ItemMan.GetUpgradeStdItem(ui, ref std);
                //Move(std, citem.S, sizeof(TStdItem));
                citem.MakeIndex = ui.MakeIndex;
                citem.Dura = ui.Dura;
                citem.DuraMax = ui.DuraMax;
                //FillChar(citem.Desc, sizeof(citem.Desc), '\0');
                //Move(ui.Desc, citem.Desc, sizeof(ui.Desc));
                if (std.StdMode == 50)
                {
                    citem.S.Name = citem.S.Name + " #" + ui.Dura.ToString();
                }
                if (new ArrayList(new int[] { 15, 19, 20, 21, 22, 23, 24, 26, 52, 53, 54 }).Contains(std.StdMode))
                {
                    if (ui.Desc[8] == 0)
                    {
                        citem.S.Shape = 0;
                    }
                    else
                    {
                        citem.S.Shape = ObjBase.RING_OF_UNKNOWN;
                    }
                }
                Def = Grobal2.MakeDefaultMsg(Grobal2.SM_ADDITEM, this.ActorId, 0, 0, 1);
                SendSocket(Def, EDcode.EncodeBuffer(citem));
            }
        }

        public void SendUpdateItem(TUserItem ui)
        {
            TClientItem citem;
            TStdItem std;
            int opt;
            TStdItem ps = svMain.UserEngine.GetStdItem(ui.Index);
            if (ps != null)
            {
                std = ps;
                opt = svMain.ItemMan.GetUpgradeStdItem(ui, ref std);
                //Move(std, citem.S, sizeof(TStdItem));
                citem.MakeIndex = ui.MakeIndex;
                citem.Dura = ui.Dura;
                citem.DuraMax = ui.DuraMax;
                //FillChar(citem.Desc, sizeof(citem.Desc), '\0');
                //Move(ui.Desc, citem.Desc, sizeof(ui.Desc));
                if (std.StdMode == 50)
                {
                    citem.S.Name = citem.S.Name + " #" + ui.Dura.ToString();
                }
                this.ChangeItemByJob(ref citem, this.Abil.Level);
                Def = Grobal2.MakeDefaultMsg(Grobal2.SM_UPDATEITEM, this.ActorId, 0, 0, 1);
                SendSocket(Def, EDcode.EncodeBuffer(citem));
            }
        }

        public void SendUpdateItemWithLevel(TUserItem ui, int lv)
        {
            TClientItem citem = null;
            TStdItem ps;
            TStdItem std;
            int opt;
            ps = svMain.UserEngine.GetStdItem(ui.Index);
            if (ps != null)
            {
                std = ps;
                opt = svMain.ItemMan.GetUpgradeStdItem(ui, ref std);
                //Move(std, citem.S, sizeof(TStdItem));
                citem.MakeIndex = ui.MakeIndex;
                citem.Dura = ui.Dura;
                citem.DuraMax = ui.DuraMax;
                //FillChar(citem.Desc, sizeof(citem.Desc), '\0');
                //Move(ui.Desc, citem.Desc, sizeof(ui.Desc));
                this.ChangeItemWithLevel(ref citem, lv);
                Def = Grobal2.MakeDefaultMsg(Grobal2.SM_UPDATEITEM, this.ActorId, 0, 0, 1);
                SendSocket(Def, EDcode.EncodeBuffer(citem));
            }
        }

        // 侩酒捞袍(sonmg)
        // 侩酒捞袍 馒侩且锭 流诀喊肺 朝妨林绰 酒捞袍 沥焊(sonmg)
        public void SendUpdateItemByJob(TUserItem ui, int lv)
        {
            TClientItem citem=null;
            TStdItem ps;
            TStdItem std;
            int opt;
            ps = svMain.UserEngine.GetStdItem(ui.Index);
            if (ps != null)
            {
                std = ps;
                opt = svMain.ItemMan.GetUpgradeStdItem(ui, ref std);
                //Move(std, citem.S, sizeof(TStdItem));
                citem.MakeIndex = ui.MakeIndex;
                citem.Dura = ui.Dura;
                citem.DuraMax = ui.DuraMax;
                //FillChar(citem.Desc, sizeof(citem.Desc), '\0');
                //Move(ui.Desc, citem.Desc, sizeof(ui.Desc));
                this.ChangeItemByJob(ref citem, lv);
                Def = Grobal2.MakeDefaultMsg(Grobal2.SM_UPDATEITEM, this.ActorId, 0, 0, 1);
                SendSocket(Def, EDcode.EncodeBuffer(citem));
            }
        }

        public void SendDelItem(TUserItem ui)
        {
            TClientItem citem=null;
            TStdItem ps;
            TStdItem std;
            int opt;
            ps = svMain.UserEngine.GetStdItem(ui.Index);
            if (ps != null)
            {
                std = ps;
                opt = svMain.ItemMan.GetUpgradeStdItem(ui, ref std);
                //Move(std, citem.S, sizeof(TStdItem));
                citem.Dura = ui.Dura;
                citem.DuraMax = ui.DuraMax;
                citem.MakeIndex = ui.MakeIndex;
                //FillChar(citem.Desc, sizeof(citem.Desc), '\0');
                //Move(ui.Desc, citem.Desc, sizeof(ui.Desc));
                if (std.StdMode == 50)
                {
                    citem.S.Name = citem.S.Name + " #" + ui.Dura.ToString();
                }
                Def = Grobal2.MakeDefaultMsg(Grobal2.SM_DELITEM, this.ActorId, 0, 0, 1);
                SendSocket(Def, EDcode.EncodeBuffer(citem));
            }
        }

        public void SendDelItemWithFlag(TUserItem ui, short wBreakdown)
        {
            TClientItem citem=null;
            TStdItem ps;
            TStdItem std;
            int opt;
            ps = svMain.UserEngine.GetStdItem(ui.Index);
            if (ps != null)
            {
                std = ps;
                opt = svMain.ItemMan.GetUpgradeStdItem(ui, ref std);
                //Move(std, citem.S, sizeof(TStdItem));
                citem.Dura = ui.Dura;
                citem.DuraMax = ui.DuraMax;
                citem.MakeIndex = ui.MakeIndex;
                //FillChar(citem.Desc, sizeof(citem.Desc), '\0');
                //Move(ui.Desc, citem.Desc, sizeof(ui.Desc));
                if (std.StdMode == 50)
                {
                    citem.S.Name = citem.S.Name + " #" + ui.Dura.ToString();
                }
                Def = Grobal2.MakeDefaultMsg(Grobal2.SM_DELITEM, this.ActorId, 0, wBreakdown, 1);
                SendSocket(Def, EDcode.EncodeBuffer(citem));
            }
        }

        public void SendDelItems(ArrayList ilist)
        {
            int i;
            string data = string.Empty;
            data = "";
            for (i = 0; i < ilist.Count; i++)
            {
                data = data + ilist[i] + "/" + ((int)ilist.Values[i]).ToString() + "/";
            }
            Def = Grobal2.MakeDefaultMsg(Grobal2.SM_DELITEMS, 0, 0, 0, ilist.Count);
            SendSocket(Def, EDcode.EncodeString(data));
        }

        public void SendBagItems()
        {
            int i;
            TUserItem pu;
            TClientItem citem=null;
            TStdItem ps;
            TStdItem std;
            string data = string.Empty;
            int opt;
            data = "";
            for (i = 0; i < this.ItemList.Count; i++)
            {
                pu = this.ItemList[i];
                ps = svMain.UserEngine.GetStdItem(pu.Index);
                if (ps != null)
                {
                    std = ps;
                    opt = svMain.ItemMan.GetUpgradeStdItem(pu, ref std);
                    //Move(std, citem.S, sizeof(TStdItem));
                    citem.Dura = pu.Dura;
                    citem.DuraMax = pu.DuraMax;
                    citem.MakeIndex = pu.MakeIndex;
                    //FillChar(citem.Desc, sizeof(citem.Desc), '\0');
                    //Move(pu.Desc, citem.Desc, sizeof(pu.Desc));
                    citem.S.NeedIdentify = 0;
                    if (std.StdMode == 50)
                    {
                        // 惑前鼻
                        citem.S.Name = citem.S.Name + " #" + pu.Dura.ToString();
                    }
                    data = data + EDcode.EncodeBuffer(citem) + "/";
                }
            }
            if (data != "")
            {
                // 荐樊
                Def = Grobal2.MakeDefaultMsg(Grobal2.SM_BAGITEMS, this.ActorId, 0, 0, this.ItemList.Count);
                SendSocket(Def, data);
            }
        }

        public void SendUseItems()
        {
            TClientItem citem=null;
            TStdItem ps;
            string data = String.Empty;
            for (var i = 0; i <= 12; i++)
            {
                // 8->12
                if (this.UseItems[i].Index > 0)
                {
                    ps = svMain.UserEngine.GetStdItem(this.UseItems[i].Index);
                    if (ps != null)
                    {
                        //std = ps;
                        //opt = svMain.ItemMan.GetUpgradeStdItem(this.UseItems[i], ref std);
                        //Move(std, citem.S, sizeof(TStdItem));
                        //citem.Dura = this.UseItems[i].Dura;
                        //citem.DuraMax = this.UseItems[i].DuraMax;
                        //citem.MakeIndex = this.UseItems[i].MakeIndex;
                        //FillChar(citem.Desc, sizeof(citem.Desc), '\0');
                        //Move(this.UseItems[i].Desc, citem.Desc, sizeof(this.UseItems[i].Desc));
                    }
                }
                // 玫狼公豪老 版快俊绰 瓷仿摹啊 官诧促.
                if (i == Grobal2.U_DRESS)
                {
                    this.ChangeItemWithLevel(ref citem, this.Abil.Level);
                }
                // 侩酒捞袍老 版快 瓷仿摹啊 官诧促.
                this.ChangeItemByJob(ref citem, this.Abil.Level);
                data = data + i.ToString() + "/" + EDcode.EncodeBuffer(citem) + "/";
            }
            if (data != "")
            {
                Def = Grobal2.MakeDefaultMsg(Grobal2.SM_SENDUSEITEMS, 0, 0, 0, 0);
                SendSocket(Def, data);
            }
        }

        // ----------------------------------------------------------
        // Magic
        public void SendAddMagic(TUserMagic pum)
        {
            TClientMagic cmag;
            cmag.Key = pum.Key;
            cmag.Level = pum.Level;
            cmag.CurTrain = pum.CurTrain;
            cmag.Def = pum.pDef;
            Def = Grobal2.MakeDefaultMsg(Grobal2.SM_ADDMAGIC, 0, 0, 0, 1);
            SendSocket(Def, EDcode.EncodeBuffer(cmag, sizeof(TClientMagic)));
        }

        public void SendDelMagic(TUserMagic pum)
        {
            Def = Grobal2.MakeDefaultMsg(Grobal2.SM_DELMAGIC, pum.MagicId, 0, 0, 1);
            SendSocket(Def, "");
        }

        public void SendMyMagics()
        {
            int i;
            int mdelay;
            string data = string.Empty;
            TUserMagic pum;
            TClientMagic cmag;
            data = "";
            mdelay = 0;
            for (i = 0; i < this.MagicList.Count; i++)
            {
                pum = (TUserMagic)this.MagicList[i];
                cmag.Key = pum.Key;
                cmag.Level = pum.Level;
                cmag.CurTrain = pum.CurTrain;
                cmag.Def = pum.pDef;
                mdelay = mdelay + pum.pDef.DelayTime;
                data = data + EDcode.EncodeBuffer(cmag, sizeof(TClientMagic)) + "/";
            }
            Def = Grobal2.MakeDefaultMsg(Grobal2.SM_SENDMYMAGIC, mdelay ^ 0x773F1A34 ^ 0x4BBC2255, 0, 0, this.MagicList.Count);
            SendSocket(Def, data);
        }

        // ----------------------------------------------------------
        public void LoverWhisper(string whostr, string saystr)
        {
            TUserHuman hum;
            int svidx=0;
            hum = svMain.UserEngine.GetUserHuman(whostr);
            if (hum != null)
            {
                // 胶炮胶 葛靛老版快俊绰 绝绰巴贸烦 加捞磊
                if (hum.bStealth)
                {
                    this.SysMsg("无法被找到。" + whostr, 0);
                    return;
                }
                if (!hum.ReadyRun)
                {
                    this.SysMsg(whostr + "无法接收讯息。", 0);
                    return;
                }
                if (!hum.BoHearWhisper || hum.IsBlockWhisper(this.UserName))
                {
                    this.SysMsg(whostr + "拒绝密语。", 0);
                    return;
                }
                // hum.SendMsg (self, RM_LM_WHISPER, 0, 0, 0, 0, '⒔' + UserName + '=> ' + saystr);
                hum.SendMsg(this, Grobal2.RM_LM_WHISPER, 0, 0, 0, 0, ":)" + this.UserName + "=> " + saystr);
            }
            else
            {
                if (svMain.UserEngine.FindOtherServerUser(whostr, ref svidx))
                {
                    // UserEngine.SendInterMsg (ISM_LM_WHISPER, svidx, whostr + '/' + '⒔' + UserName + '=> ' + saystr);
                    svMain.UserEngine.SendInterMsg(Grobal2.ISM_LM_WHISPER, svidx, whostr + "/" + ":)" + this.UserName + "=> " + saystr);
                }
                else
                {
                    this.SysMsg("无法被找到。" + whostr, 0);
                }
            }
        }

        // **
        public void Whisper(string whostr, string saystr)
        {
            TUserHuman hum;
            int svidx=0;
            hum = svMain.UserEngine.GetUserHuman(whostr);
            if (hum != null)
            {
                // 胶炮胶 葛靛老版快俊绰 绝绰巴贸烦 加捞磊
                if (hum.bStealth)
                {
                    this.SysMsg(whostr + "无法被找到。", 0);
                    return;
                }
                if (!hum.ReadyRun)
                {
                    this.SysMsg(whostr + "无法接收讯息。", 0);
                    return;
                }
                if (!hum.BoHearWhisper || hum.IsBlockWhisper(this.UserName))
                {
                    this.SysMsg(whostr + "拒接密语。", 0);
                    return;
                }
                // 款康磊 肚绰 皑矫磊 葛靛捞搁 喊档 贸府...
                if (this.BoSuperviserMode || this.BoSysopMode)
                {
                    hum.SendMsg(this, Grobal2.RM_GMWHISPER, 0, 0, 0, 0, this.UserName + "=> " + saystr);
                }
                else
                {
                    hum.SendMsg(this, Grobal2.RM_WHISPER, 0, 0, 0, 0, this.UserName + "=> " + saystr);
                }
            }
            else
            {
                if (svMain.UserEngine.FindOtherServerUser(whostr, ref svidx))
                {
                    // 款康磊 肚绰 皑矫磊 葛靛捞搁 喊档 贸府...
                    if (this.BoSuperviserMode || this.BoSysopMode)
                    {
                        svMain.UserEngine.SendInterMsg(Grobal2.ISM_GMWHISPER, svidx, whostr + "/" + this.UserName + "=> " + saystr);
                    }
                    else
                    {
                        svMain.UserEngine.SendInterMsg(Grobal2.ISM_WHISPER, svidx, whostr + "/" + this.UserName + "=> " + saystr);
                    }
                }
                else
                {
                    this.SysMsg(whostr + "无法被找到。", 0);
                }
            }
        }

        public void WhisperRe(string saystr, bool IsGM)
        {
            string sendwho = string.Empty;
            HUtil32.GetValidStr3(saystr, ref sendwho, new string[] { " ", "=", ">" });
            if (this.BoHearWhisper && (!IsBlockWhisper(sendwho)))
            {
                if (IsGM)
                {
                    this.SendMsg(this, Grobal2.RM_GMWHISPER, 0, 0, 0, 0, saystr);
                }
                else
                {
                    this.SendMsg(this, Grobal2.RM_WHISPER, 0, 0, 0, 0, saystr);
                }
            }
        }

        public void LoverWhisperRe(string saystr)
        {
            string sendwho = string.Empty;
            HUtil32.GetValidStr3(saystr, ref sendwho, new string[] { " ", "=", ">" });
            if (this.BoHearWhisper && (!IsBlockWhisper(sendwho)))
            {
                this.SendMsg(this, Grobal2.RM_LM_WHISPER, 0, 0, 0, 0, saystr);
            }
        }

        public void BlockWhisper(string whostr)
        {
            int i;
            for (i = 0; i < this.WhisperBlockList.Count; i++)
            {
                if (whostr.ToLower().CompareTo(this.WhisperBlockList[i].ToLower()) == 0)
                {
                    this.WhisperBlockList.Remove(i);
                    this.SysMsg("[release from ban whisper: " + whostr + "]", 1);
                    return;
                }
            }
            this.WhisperBlockList.Add(whostr);
            this.SysMsg("[ban whisper: " + whostr + "]", 0);
        }

        public bool IsBlockWhisper(string whostr)
        {
            bool result;
            int i;
            result = false;
            for (i = 0; i < this.WhisperBlockList.Count; i++)
            {
                if (whostr.ToLower().CompareTo(this.WhisperBlockList[i].ToLower()) == 0)
                {
                    result = true;
                    break;
                }
            }
            return result;
        }

        // 巩颇呕硼
        public void GuildSecession()
        {
            if ((this.MyGuild != null) && (this.GuildRank > 1))
            {
                // 巩林绰 救凳
                if (((TGuild)this.MyGuild).IsMember(this.UserName))
                {
                    if (((TGuild)this.MyGuild).DelMember(this.UserName))
                    {
                        // //////////////////////////////////
                        // 巩颇傈 吝俊绰 巩颇呕硼且 荐 绝澜.(sonmg)
                        if (this.LastHiter != null)
                        {
                            if (this.LastHiter.MyGuild != null)
                            {
                                // 笛促 巩颇俊 啊涝等 惑怕俊辑
                                if (this.GetGuildRelation(this, this.LastHiter) == 2)
                                {
                                    // 巩傈(巩颇傈)吝烙
                                    this.SysMsg("You cannot secede now.", 0);
                                    return;
                                }
                            }
                        }
                        // //////////////////////////////////
                        svMain.UserEngine.SendInterMsg(Grobal2.ISM_RELOADGUILD, svMain.ServerIndex, ((TGuild)this.MyGuild).GuildName);
                        this.MyGuild = null;
                        GuildRankChanged(0, "");
                        this.SysMsg("退出。", 1);
                        this.ChangeNameColor();
                        // 捞抚祸 诀单捞飘(sonmg 2004/12/29)
                    }
                }
            }
            else
            {
                this.SysMsg("取消。", 0);
            }
        }

        public void CmdSendTestQuestDiary(int unitnum)
        {
            int i;
            int k;
            string str;
            ArrayList list;
            TQDDinfo pqdd;
            if (unitnum == 0)
            {
                for (i = 0; i < svMain.QuestDiaryList.Count; i++)
                {
                    list = svMain.QuestDiaryList[i] as ArrayList;
                    if (list != null)
                    {
                        if (list.Count > 0)
                        {
                            if (this.GetQuestOpenIndexMark(i + 1) == 1)
                            {
                                str = " (开始)";
                            }
                            else
                            {
                                str = " (准备)";
                            }
                            if (this.GetQuestFinIndexMark(i + 1) == 1)
                            {
                                str = str + " (完成)";
                            }
                            else
                            {
                                str = str + " (进行)";
                            }
                            this.SysMsg("[" + ((TQDDinfo)list[0]).Index.ToString() + "] " + ((TQDDinfo)list[0]).Title + str, 1);
                        }
                    }
                }
            }
            else
            {
                unitnum = unitnum - 1;
                // 蜡粗阑 唱鸥尘 锭绰 1捞 0烙
                if (unitnum < svMain.QuestDiaryList.Count)
                {
                    list = svMain.QuestDiaryList[unitnum] as ArrayList;
                    if (list != null)
                    {
                        for (i = 0; i < list.Count; i++)
                        {
                            pqdd = (TQDDinfo)list[i];
                            if (this.GetQuestMark(pqdd.Index) == 1)
                            {
                                str = " (完成)";
                            }
                            else
                            {
                                str = " (未完成)";
                            }
                            this.SysMsg("[" + pqdd.Index.ToString() + "] " + pqdd.Title + str, 2);
                            for (k = 0; k < pqdd.SList.Count; k++)
                            {
                                this.SysMsg((string)pqdd.SList[k], 1);
                            }
                        }
                    }
                }
            }
        }

        public override void Say(string saystr)
        {
            string str;
            string cmd = String.Empty;
            string param1 = String.Empty;
            string param2 = String.Empty;
            string param3 = String.Empty;
            string param4 = String.Empty;
            string param5 = String.Empty;
            string param6 = String.Empty;
            string param7 = String.Empty;
            TUserHuman hum;
            TStdItem pstd;
            int i;
            int idx;
            int n;
            bool boshutup;
            bool flag;
            if (saystr == "")
            {
                return;
            }
            for (i = 0; i < svMain.UserEngine.ChatLogList.Count; i++)
            {
                if (this.UserName == svMain.UserEngine.ChatLogList[i])
                {
                    svMain.AddChatLog("28\09" + this.MapName + "\09" + this.CX.ToString() + "\09" + this.CY.ToString() + "\09" + this.UserName + "\09" + saystr + "\09" + DateTime.Now.ToString() + "\09" + "1\09" + "0");
                    break;
                }
            }
            if (saystr[0] == '@')
            {
                str = saystr.Substring(2 - 1, saystr.Length - 1);
                str = HUtil32.GetValidStr3(str,ref cmd, new string[] { " ", ",", ":" });
                str = HUtil32.GetValidStr3(str, ref param1, new string[] { " ", ",", ":" });
                if (str != "")
                {
                    str = HUtil32.GetValidStr3(str, ref param2, new string[] { " ", ",", ":" });
                }
                if (str != "")
                {
                    str = HUtil32.GetValidStr3(str, ref param3, new string[] { " ", ",", ":" });
                }
                if (str != "")
                {
                    str = HUtil32.GetValidStr3(str, ref param4, new string[] { " ", ",", ":" });
                }
                if (str != "")
                {
                    str = HUtil32.GetValidStr3(str, ref param5, new string[] { " ", ",", ":" });
                }
                if (str != "")
                {
                    str = HUtil32.GetValidStr3(str, ref param6, new string[] { " ", ",", ":" });
                }
                if (str != "")
                {
                    str = HUtil32.GetValidStr3(str, ref param7, new string[] { " ", ",", ":" });
                }
                if ((cmd.ToLower().CompareTo("RejectWhisper".ToLower()) == 0) || (cmd.ToLower().CompareTo("拒绝密语".ToLower()) == 0))
                {
                    this.BoHearWhisper = !this.BoHearWhisper;
                    if (this.BoHearWhisper)
                    {
                        this.SysMsg("[允许密语]", 1);
                    }
                    else
                    {
                        this.SysMsg("[拒绝密语]", 1);
                    }
                    return;
                }
                if ((cmd.ToLower().CompareTo("AllowWhisper".ToLower()) == 0) || (cmd.ToLower().CompareTo("允许密语".ToLower()) == 0))
                {
                    this.BoHearWhisper = true;
                    this.SysMsg("[允许密语]", 1);
                    return;
                }
                if (cmd.ToLower().CompareTo("拒绝".ToLower()) == 0)
                {
                    if (param1 != "")
                    {
                        BlockWhisper(param1);
                    }
                    if (param2 != "")
                    {
                        BlockWhisper(param2);
                    }
                    if (param3 != "")
                    {
                        BlockWhisper(param3);
                    }
                    return;
                }
                if ((cmd.ToLower().CompareTo("拒绝喊话".ToLower()) == 0) || (cmd.ToLower().CompareTo("允许喊话".ToLower()) == 0))
                {
                    this.BoHearCry = !this.BoHearCry;
                    if (this.BoHearCry)
                    {
                        this.SysMsg("[允许喊话]", 1);
                    }
                    else
                    {
                        this.SysMsg("[拒绝喊话]", 1);
                    }
                    return;
                }
                if (cmd.ToLower().CompareTo("拒绝交易".ToLower()) == 0)
                {
                    this.BoExchangeAvailable = !this.BoExchangeAvailable;
                    if (this.BoExchangeAvailable)
                    {
                        this.SysMsg("[允许交易]", 1);
                    }
                    else
                    {
                        this.SysMsg("[拒绝交易]", 1);
                    }
                    return;
                }
                if (cmd.ToLower().CompareTo("加入门派".ToLower()) == 0)
                {
                    this.AllowEnterGuild = !this.AllowEnterGuild;
                    if (this.AllowEnterGuild)
                    {
                        this.SysMsg("[允许加入成员]", 1);
                    }
                    else
                    {
                        this.SysMsg("[拒绝加入成员]", 1);
                    }
                    return;
                }
                if (cmd.ToLower().CompareTo("允许结盟".ToLower()) == 0)
                {
                    if (this.IsGuildMaster())
                    {
                        ((TGuild)this.MyGuild).AllowAllyGuild = !((TGuild)this.MyGuild).AllowAllyGuild;
                        if (((TGuild)this.MyGuild).AllowAllyGuild)
                        {
                            this.SysMsg("[允许结盟]", 1);
                        }
                        else
                        {
                            this.SysMsg("[拒绝结盟]", 1);
                        }
                    }
                    return;
                }
                if ((cmd.ToLower().CompareTo("Alliance".ToLower()) == 0) || (cmd.ToLower().CompareTo("联盟".ToLower()) == 0))
                {
                    if (this.IsGuildMaster())
                    {
                        ServerGetGuildMakeAlly();
                    }
                    return;
                }
                if ((cmd.ToLower().CompareTo("CancleAlliance".ToLower()) == 0) || (cmd.ToLower().CompareTo("取消联盟".ToLower()) == 0))
                {
                    if (this.IsGuildMaster())
                    {
                        ServerGetGuildBreakAlly(param1);
                    }
                    return;
                }
                if (cmd.ToLower().CompareTo("退出门派".ToLower()) == 0)
                {
                    GuildSecession();
                    return;
                }
                if ((cmd.ToLower().CompareTo("允许门派聊天".ToLower()) == 0) || (cmd.ToLower().CompareTo("拒绝门派聊天".ToLower()) == 0))
                {
                    this.BoHearGuildMsg = !this.BoHearGuildMsg;
                    if (this.BoHearGuildMsg)
                    {
                        this.SysMsg("许门派内全体成员喊话", 1);
                    }
                    else
                    {
                        this.SysMsg("拒绝门派内全体成员喊话", 1);
                    }
                    return;
                }
                if ((cmd.ToUpper() == "H") || (cmd.ToUpper() == "帮助"))
                {
                    // for i:=0 to LineHelpList.Count-1 do
                    // SysMsg (LineHelpList[i], 1);
                    if (svMain.DefaultNpc != null)
                    {
                        svMain.DefaultNpc.NpcSayTitle(this, "@_HELP");
                    }
                    return;
                }
                // 50饭骇 瓤苞 钎矫/见辫(sonmg 2004/03/12)
                if (cmd.ToLower().CompareTo("Energy".ToLower()) == 0)
                {
                    if (this.Abil.Level >= ObjBase.EFFECTIVE_HIGHLEVEL)
                    {
                        this.BoHighLevelEffect = !this.BoHighLevelEffect;
                        if (this.BoHighLevelEffect)
                        {
                            this.RecalcAbilitys();
                            this.SysMsg("Show Energy.", 1);
                        }
                        else
                        {
                            this.RecalcAbilitys();
                            this.SysMsg("Hide Energy.", 1);
                        }
                    }
                    return;
                }
                // 涅胶飘 老瘤 抛胶飘
                if (cmd.ToLower().CompareTo("日志".ToLower()) == 0)
                {
                    CmdSendTestQuestDiary(HUtil32.Str_ToInt(param1, 0));
                    return;
                }
                if (cmd.ToLower().CompareTo("AttackMode".ToLower()) == 0)
                {
                    // 傍拜规侥阑 官槽促.
                    if (this.HumAttackMode < Grobal2.HAM_MAXCOUNT - 1)
                    {
                        this.HumAttackMode++;
                    }
                    else
                    {
                        this.HumAttackMode = 0;
                    }
                    switch (this.HumAttackMode)
                    {
                        case Grobal2.HAM_ALL:
                            this.SysMsg("[自由攻击模式]", 1);
                            break;
                        case Grobal2.HAM_PEACE:
                            this.SysMsg("[和平攻击模式]", 1);
                            break;
                        case Grobal2.HAM_GROUP:
                            this.SysMsg("[组队攻击模式]", 1);
                            break;
                        case Grobal2.HAM_GUILD:
                            this.SysMsg("[行会攻击模式]", 1);
                            break;
                        case Grobal2.HAM_PKATTACK:
                            this.SysMsg("[善恶对决模式]", 1);
                            break;
                    }
                    this.SendMsg(this, Grobal2.RM_ATTACKMODE, 0, 0, 0, 0, "");
                    return;
                }
                if (cmd.ToLower().CompareTo("Rest".ToLower()) == 0)
                {
                    // 傍拜 or 绒侥
                    if (this.SlaveList.Count > 0)
                    {
                        this.BoSlaveRelax = !this.BoSlaveRelax;
                        if (this.BoSlaveRelax)
                        {
                            this.SysMsg("下属行动：休息", 1);
                        }
                        else
                        {
                            this.SysMsg("下属行动：攻击", 1);
                        }
                    }
                    return;
                }
                if (HUtil32.Str_ToInt(cmd, 0) == ObjBase.GET_A_CMD)
                {
                    this.SendMsg(this, Grobal2.RM_NEXTTIME_PASSWORD, 0, 0, 0, 0, "");
                    this.SysMsg("请输入密码。", 1);
                    this.BoReadyAdminPassword = true;
                    return;
                }
                if (this.UserDegree >= Grobal2.UD_SYSOP)
                {
                    if (cmd.ToLower().CompareTo("gsa".ToLower()) == 0)
                    {
                        this.SendMsg(this, Grobal2.RM_NEXTTIME_PASSWORD, 0, 0, 0, 0, "");
                        this.SysMsg("请输入密码。", 1);
                        this.BoReadySuperAdminPassword = true;
                        return;
                    }
                    if (HUtil32.Str_ToInt(cmd, 0) == ObjBase.GET_SA_CMD)
                    {
                        this.SendMsg(this, Grobal2.RM_NEXTTIME_PASSWORD, 0, 0, 0, 0, "");
                        this.SysMsg("请输入密码。", 1);
                        this.BoReadySuperAdminPassword = true;
                        return;
                    }
                }
                if ((this.MyGuild == svMain.UserCastle.OwnerGuild) && (this.MyGuild != null))
                {
                    if (cmd.ToLower().CompareTo("狮北城城门".ToLower()) == 0)
                    {
                        CmdOpenCloseUserCastleMainDoor(param1);
                        // 摧塞,凯覆
                        return;
                    }
                }
                // 鉴埃捞悼 馆瘤甫 尝绊 乐栏搁... 混酒乐栏搁
                if (this.BoAbilSpaceMove && (this.WAbil.HP > 0))
                {
                    if (!this.BoTaiwanEventUser)
                    {
                        if (!this.PEnvir.NoPositionMove)
                        {
                            if (cmd.ToLower().CompareTo("Move".ToLower()) == 0)
                            {
                                if (HUtil32.GetTickCount() - this.LatestSpaceMoveTime > 10 * 1000)
                                {
                                    this.LatestSpaceMoveTime  =  HUtil32.GetTickCount();
                                    this.SendRefMsg(Grobal2.RM_SPACEMOVE_HIDE, 0, 0, 0, 0, "");
                                    this.UserSpaceMove("", param1, param2);
                                }
                                else
                                {
                                    this.SysMsg((10 - (HUtil32.GetTickCount() - this.LatestSpaceMoveTime) / 1000).ToString() + "秒后才能使用此命令。", 0);
                                }
                                return;
                            }
                        }
                        else
                        {
                            // 鉴埃捞悼馆瘤 荤侩 阂啊瓷 瘤开
                            this.SysMsg("在这里您无法使用。", 0);
                            return;
                        }
                    }
                    else
                    {
                        this.SysMsg("你无法使用它。", 0);
                    }
                }
                // 沤祸狼格吧捞甫 尝绊 乐栏搁
                if (this.BoAbilSearch || (this.UserDegree >= Grobal2.UD_SYSOP))
                {
                    if (cmd.ToLower().CompareTo("Searching".ToLower()) == 0)
                    {
                        if ((HUtil32.GetTickCount() - this.LatestSearchWhoTime > 10 * 1000) || (this.UserDegree >= Grobal2.UD_SYSOP))
                        {
                            this.LatestSearchWhoTime  =  HUtil32.GetTickCount();
                            hum = svMain.UserEngine.GetUserHuman(param1);
                            if (hum != null)
                            {
                                if (hum.PEnvir == this.PEnvir)
                                {
                                    this.SysMsg(param1 + "在" + hum.CX.ToString() + " " + hum.CY.ToString() + "：他（她）位于该地点。", 1);
                                }
                                else
                                {
                                    this.SysMsg(param1 + "此人在其他的位置。", 1);
                                }
                            }
                            else
                            {
                                this.SysMsg(param1 + "此人无法查询。", 1);
                            }
                        }
                        else
                        {
                            this.SysMsg((10 - (HUtil32.GetTickCount() - this.LatestSearchWhoTime) / 1000).ToString() + "秒后才能使用此命令。", 0);
                        }
                        return;
                    }
                }
                // 玫瘤钦老
                if ((cmd.ToLower().CompareTo("拒绝天地合一".ToLower()) == 0) || (cmd.ToLower().CompareTo("允许天地合一".ToLower()) == 0))
                {
                    this.BoEnableRecall = !this.BoEnableRecall;
                    if (this.BoEnableRecall)
                    {
                        this.SysMsg("[允许天地合一]", 1);
                    }
                    else
                    {
                        this.SysMsg("[拒绝天地合一]", 1);
                    }
                }
                // 巩颇厘盔 家券
                if ((cmd.ToLower().CompareTo("拒绝门派天地合一".ToLower()) == 0) || (cmd.ToLower().CompareTo("允许门派天地合一".ToLower()) == 0))
                {
                    this.BoEnableAgitRecall = !this.BoEnableAgitRecall;
                    if (this.BoEnableAgitRecall)
                    {
                        this.SysMsg("[允许门派天地合一]", 1);
                    }
                    else
                    {
                        this.SysMsg("[拒绝门派天地合一]", 1);
                    }
                }
                if (this.BoCGHIEnable || (this.UserDegree >= Grobal2.UD_SYSOP))
                {
                    if (cmd.ToLower().CompareTo("天地合一".ToLower()) == 0)
                    {
                        if (!this.PEnvir.NoRecall)
                        {
                            n = (int)((HUtil32.GetTickCount() - this.CGHIstart) / 1000);
                            this.CGHIstart = this.CGHIstart + ((long)n * 1000);
                            if (this.CGHIUseTime > n)
                            {
                                this.CGHIUseTime = (ushort)(this.CGHIUseTime - n);
                            }
                            else
                            {
                                this.CGHIUseTime = 0;
                            }
                            if (this.CGHIUseTime == 0)
                            {
                                if (this.GroupOwner == this)
                                {
                                    // 磊脚捞 弊缝炉
                                    for (i = 1; i < this.GroupMembers.Count; i++)
                                    {
                                        // 磊脚 哗绊
                                        if ((this.GroupOwner.GroupMembers.Values[i] as TUserHuman).BoEnableRecall)
                                        {
                                            CmdRecallMan(this.GroupMembers[i], "");
                                        }
                                        else
                                        {
                                            this.SysMsg(this.GroupMembers[i] + "拒绝天地合一", 0);
                                        }
                                    }
                                    this.CGHIstart  =  HUtil32.GetTickCount();
                                    this.CGHIUseTime = 3 * 60;
                                }
                            }
                            else
                            {
                                this.SysMsg("天地合一" + this.CGHIUseTime.ToString() + "秒后可以使用。", 0);
                            }
                        }
                        else
                        {
                            this.SysMsg("你不能在这里使用它。", 0);
                        }
                    }
                }
                // 楷牢 父巢
                if ((cmd.ToLower().CompareTo("MeetCouple".ToLower()) == 0) || (cmd.ToLower().CompareTo("MeetLover".ToLower()) == 0) || (cmd.ToLower().CompareTo("情侣".ToLower()) == 0))
                {
                    flag = false;
                    if (fLover != null)
                    {
                        // 父抄瘤 100老 捞惑 登绢具 荤侩 啊瓷
                        if (fLover.GetLoverName != "")
                        {
                            if (HUtil32.Str_ToInt(fLover.GetLoverDays, 0) >= 100)
                            {
                                // 目敲馆瘤 馒侩 眉农
                                for (i = 0; i <= Grobal2.U_CHARM; i++)
                                {
                                    pstd = svMain.UserEngine.GetStdItem(this.UseItems[i].Index);
                                    if (pstd != null)
                                    {
                                        if ((pstd.StdMode == 22) && (pstd.Shape == ObjBase.SHAPE_COUPLERING))
                                        {
                                            if (HUtil32.GetTickCount() - this.MeetLoverDelayTime > 20 * 60 * 1000)
                                            {
                                                flag = true;
                                                // 酒捞袍苞 朝楼 炼扒捞 嘎澜.
                                                if (CmdLoverCharSpaceMove(fLover.GetLoverName))
                                                {
                                                    this.MeetLoverDelayTime  =  HUtil32.GetTickCount();
                                                }
                                            }
                                            else
                                            {
                                                flag = true;
                                                // 固馒侩 皋矫瘤甫 免仿窍瘤 臼澜.
                                                this.SysMsg("你不能在这里使用，请稍后再试。", 0);
                                            }
                                            break;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                flag = true;
                                // 固馒侩 皋矫瘤甫 免仿窍瘤 臼澜.
                            }
                        }
                        else
                        {
                            flag = true;
                            // 固馒侩 皋矫瘤甫 免仿窍瘤 臼澜.
                        }
                    }
                    if (!flag)
                    {
                        flag = false;
                        this.SysMsg("你必须穿戴一个情侣戒指。", 0);
                    }
                    return;
                }
                if (this.UserDegree >= Grobal2.UD_OBSERVER)
                {
                    if (saystr.Length > 2)
                    {
                        if (saystr[0] == '!')
                        {
                            str = saystr.Substring(2 - 1, saystr.Length - 2);
                            svMain.UserEngine.SysMsgAll("(*)" + str);
                            svMain.UserEngine.SendInterMsg(Grobal2.ISM_SYSOPMSG, svMain.ServerIndex, "(*)" + str);
                            return;
                        }
                        if (saystr[0] == '$')
                        {
                            str = saystr.Substring(2 - 1, saystr.Length - 2);
                            svMain.UserEngine.SysMsgAll("(!)" + str);
                            return;
                        }
                        if (saystr[0] == '#')
                        {
                            str = saystr.Substring(2 - 1, saystr.Length - 2);
                            svMain.UserEngine.CryCry(Grobal2.RM_SYSMESSAGE, this.PEnvir, this.CX, this.CY, 10000, "(#)" + str);
                            return;
                        }
                    }
                }
                if (this.UserDegree >= Grobal2.UD_SYSOP)
                {
                    if (cmd.ToLower().CompareTo("Move".ToLower()) == 0)
                    {
                        if (svMain.GrobalEnvir.GetEnvir(param1) != null)
                        {
                            this.SendRefMsg(Grobal2.RM_SPACEMOVE_HIDE, 0, 0, 0, 0, "");
                            this.RandomSpaceMove(param1, 0);
                        }
                        return;
                    }
                    if ((cmd.ToLower().CompareTo("PositionMove".ToLower()) == 0) || (cmd.ToLower().CompareTo("PMove".ToLower()) == 0))
                    {
                        CmdFreeSpaceMove(param1, param2, param3);
                        return;
                    }
                    if (cmd.ToLower().CompareTo("Stealth".ToLower()) == 0)
                    {
                        CmdStealth();
                        return;
                    }
                    if (cmd.ToLower().CompareTo("CharMove".ToLower()) == 0)
                    {
                        CmdCharMove(param1, param2);
                        return;
                    }
                    if (cmd.ToLower().CompareTo("Goto".ToLower()) == 0)
                    {
                        CmdCharSpaceMove(param1);
                        return;
                    }
                    if (cmd.ToLower().CompareTo("DragonExp".ToLower()) == 0)
                    {
                        if (param1 == "")
                        {
                            this.SysMsg("Dragon Exp= " + svMain.gFireDragon.Exp.ToString(), 0);
                            this.SysMsg("Dragon Level= " + svMain.gFireDragon.Level.ToString(), 0);
                            this.SysMsg("Dragon ExpDvider= " + svMain.gFireDragon.ExpDivider.ToString(), 0);
                        }
                        else
                        {
                            svMain.gFireDragon.ExpDivider = HUtil32.Str_ToInt(param1, 1);
                            this.SysMsg("Set Dragon Exp Divider = " + svMain.gFireDragon.ExpDivider.ToString(), 0);
                        }
                        return;
                    }
                    if (cmd.ToLower().CompareTo("Info".ToLower()) == 0)
                    {
                        CmdSendUserLevelInfos(param1);
                        return;
                    }
                    if ((cmd.ToLower().CompareTo("MobLevel".ToLower()) == 0) || (cmd.ToLower().CompareTo("MobLevel".ToLower()) == 0))
                    {
                        CmdSendMonsterLevelInfos();
                        return;
                    }
                    if ((cmd.ToLower().CompareTo("KingMob".ToLower()) == 0) || (cmd.ToLower().CompareTo("KingMob".ToLower()) == 0))
                    {
                        CmdSendKingMonsterInfos(param1);
                        return;
                    }
                    if ((cmd.ToLower().CompareTo("MobCount".ToLower()) == 0) || (cmd.ToLower().CompareTo("MobCount".ToLower()) == 0))
                    {
                        this.SysMsg(param1 + "no.of Mob=" + svMain.UserEngine.GetMapMons(svMain.GrobalEnvir.GetEnvir(param1), null).ToString(), 1);
                        return;
                    }
                    if (cmd.ToLower().CompareTo("Human".ToLower()) == 0)
                    {
                        if (param1 == "")
                        {
                            param1 = this.PEnvir.MapName;
                        }
                        this.SysMsg(param1 + "No.of human=" + svMain.UserEngine.GetHumCount(param1).ToString(), 1);
                    }
                    if ((cmd.ToLower().CompareTo("Map".ToLower()) == 0) || (cmd.ToLower().CompareTo("地图".ToLower()) == 0))
                    {
                        this.SysMsg("Map: " + this.MapName, 0);
                        return;
                    }
                    if ((cmd.ToLower().CompareTo("Kick".ToLower()) == 0) || (cmd.ToLower().CompareTo("Kick".ToLower()) == 0))
                    {
                        CmdKickUser(param1);
                        return;
                    }
                    if ((cmd.ToLower().CompareTo("Ting".ToLower()) == 0) || (cmd.ToLower().CompareTo("Ting".ToLower()) == 0))
                    {
                        CmdTingUser(param1);
                        return;
                    }
                    if ((cmd.ToLower().CompareTo("SuperTing".ToLower()) == 0) || (cmd.ToLower().CompareTo("SuperTing".ToLower()) == 0))
                    {
                        CmdTingRangeUser(param1, param2);
                        return;
                    }
                    if ((cmd.ToLower().CompareTo("Shutup".ToLower()) == 0) || (cmd.ToLower().CompareTo("Shutup".ToLower()) == 0))
                    {
                        CmdAddShutUpList(param1, param2, true);
                        return;
                    }
                    if ((cmd.ToLower().CompareTo("ReleaseShutup".ToLower()) == 0) || (cmd.ToLower().CompareTo("ReleaseShutup".ToLower()) == 0))
                    {
                        CmdDelShutUpList(param1, true);
                        return;
                    }
                    if ((cmd.ToLower().CompareTo("ShutupList".ToLower()) == 0) || (cmd.ToLower().CompareTo("ShutupList".ToLower()) == 0))
                    {
                        CmdSendShutUpList();
                        return;
                    }
                    // 2003/08/28 盲泼肺弊
                    if ((cmd.ToLower().CompareTo("ReloadChatLog".ToLower()) == 0) || (cmd.ToLower().CompareTo("ReloadChatLog".ToLower()) == 0))
                    {
                        LocalDB.FrmDB.LoadChatLogFiles();
                        svMain.UserEngine.SendInterMsg(Grobal2.ISM_RELOADCHATLOG, svMain.ServerIndex, "");
                        this.SysMsg(cmd + " is reloaded to whole server.", 1);
                        return;
                    }
                    // 2003/09/15 盲泼肺弊 眠啊/昏力
                    if ((cmd.ToLower().CompareTo("AddChatLog".ToLower()) == 0) || (cmd.ToLower().CompareTo("AddChatLog".ToLower()) == 0))
                    {
                        CmdAddChatLogList(param1, true);
                        return;
                    }
                    if ((cmd.ToLower().CompareTo("ReleaseChatLog".ToLower()) == 0) || (cmd.ToLower().CompareTo("ReleaseChatLog".ToLower()) == 0))
                    {
                        CmdDelChatLogList(param1, true);
                        return;
                    }
                    if ((cmd.ToLower().CompareTo("ChatLogList".ToLower()) == 0) || (cmd.ToLower().CompareTo("ChatLogList".ToLower()) == 0))
                    {
                        CmdSendChatLogList();
                        return;
                    }
                    if ((cmd.ToLower().CompareTo("GameMaster".ToLower()) == 0) || (cmd.ToLower().CompareTo("管理员".ToLower()) == 0))
                    {
                        this.BoSysopMode = !this.BoSysopMode;
                        if (this.BoSysopMode)
                        {
                            this.SysMsg("Game master mode", 1);
                        }
                        else
                        {
                            this.SysMsg("Release  game master mode ", 1);
                        }
                        return;
                    }
                    if ((cmd.ToLower().CompareTo("Observer".ToLower()) == 0) || (cmd.ToLower().CompareTo("Ob".ToLower()) == 0) || (cmd.ToLower().CompareTo("观察者".ToLower()) == 0))
                    {
                        this.BoSuperviserMode = !this.BoSuperviserMode;
                        if (this.BoSuperviserMode)
                        {
                            this.SysMsg("Observer mode", 1);
                        }
                        else
                        {
                            this.SysMsg("Release  observer mode", 1);
                        }
                        return;
                    }
                    if ((cmd.ToLower().CompareTo("Superman".ToLower()) == 0) || (cmd.ToLower().CompareTo("无敌".ToLower()) == 0))
                    {
                        this.NeverDie = !this.NeverDie;
                        if (this.NeverDie)
                        {
                            this.SysMsg("Invincible Mode", 1);
                        }
                        else
                        {
                            this.SysMsg("Normal mode", 1);
                        }
                        return;
                    }
                    if ((cmd.ToLower().CompareTo("Level".ToLower()) == 0) || (cmd.ToLower().CompareTo("修改等级".ToLower()) == 0))
                    {
                        this.Abil.Level = (byte)_MIN(40, HUtil32.Str_ToInt(param1, 1));
                        this.HasLevelUp(1);
                        return;
                    }
                    if (cmd.ToLower().CompareTo("SabukWallGold".ToLower()) == 0)
                    {
                        this.SysMsg("SabukWall Fund:" + svMain.UserCastle.TotalGold.ToString() + ",  Todays income:" + svMain.UserCastle.TodayIncome.ToString(), 1);
                        return;
                    }
                    if ((cmd.ToLower().CompareTo("Recall".ToLower()) == 0) || (cmd.ToLower().CompareTo("Recall".ToLower()) == 0))
                    {
                        CmdRecallMan(param1, "");
                        return;
                    }
                    // 漂沥 甘俊 乐绰 荤恩甸阑 磊脚狼 菊栏肺 家券茄促(牢盔荐绰 窃荐郴 绊沥).
                    if ((cmd.ToLower().CompareTo("RecallMap".ToLower()) == 0) || (cmd.ToLower().CompareTo("RecallMap".ToLower()) == 0))
                    {
                        CmdRecallMap(param1);
                        return;
                    }
                    if (cmd.ToLower().CompareTo("flag".ToLower()) == 0)
                    {
                        hum = svMain.UserEngine.GetUserHuman(param1);
                        if (hum != null)
                        {
                            idx = HUtil32.Str_ToInt(param2, 0);
                            if (hum.GetQuestMark(idx) == 1)
                            {
                                this.SysMsg(hum.UserName + ":  [" + idx.ToString() + "] = ON", 1);
                            }
                            else
                            {
                                this.SysMsg(hum.UserName + ":  [" + idx.ToString() + "] = OFF", 1);
                            }
                        }
                        else
                        {
                            this.SysMsg("@flag user_name number_of_flag", 0);
                        }
                    }
                    if (cmd.ToLower().CompareTo("showopen".ToLower()) == 0)
                    {
                        hum = svMain.UserEngine.GetUserHuman(param1);
                        if (hum != null)
                        {
                            idx = HUtil32.Str_ToInt(param2, 0);
                            if (hum.GetQuestOpenIndexMark(idx) == 1)
                            {
                                this.SysMsg(hum.UserName + ":  [" + idx.ToString() + "] = ON", 1);
                            }
                            else
                            {
                                this.SysMsg(hum.UserName + ":  [" + idx.ToString() + "] = OFF", 1);
                            }
                        }
                        else
                        {
                            this.SysMsg("@showopen user_name number_of_unit", 0);
                        }
                    }
                    if (cmd.ToLower().CompareTo("showunit".ToLower()) == 0)
                    {
                        hum = svMain.UserEngine.GetUserHuman(param1);
                        if (hum != null)
                        {
                            idx = HUtil32.Str_ToInt(param2, 0);
                            if (hum.GetQuestFinIndexMark(idx) == 1)
                            {
                                this.SysMsg(hum.UserName + ":  [" + idx.ToString() + "] = ON", 1);
                            }
                            else
                            {
                                this.SysMsg(hum.UserName + ":  [" + idx.ToString() + "] = OFF", 1);
                            }
                        }
                        else
                        {
                            this.SysMsg("@showunit user_name number_of_unit", 0);
                        }
                    }
                    // 款康磊 妇措风 模备殿废
                    if ((cmd.ToLower().CompareTo("addfriend".ToLower()) == 0) || (cmd.ToLower().CompareTo("增加好友".ToLower()) == 0))
                    {
                        if (param1 != "")
                        {
                            this.SendMsg(this, Grobal2.CM_FRIEND_ADD, 0, Grobal2.RT_FRIENDS, 1, 0, param1);
                        }
                    }
                    // 疙飞绢 眠啊
                    if ((cmd.ToLower().CompareTo("safezone".ToLower()) == 0) || (cmd.ToLower().CompareTo("safezone".ToLower()) == 0))
                    {
                        if (this.InSafeZone())
                        {
                            this.SysMsg("Safe Zone.", 2);
                        }
                        else
                        {
                            this.SysMsg("Not Safe Zone.", 0);
                        }
                    }
                }
                // 绢靛刮 : AdminList俊 '*' 殿鞭
                if (this.UserDegree >= Grobal2.UD_ADMIN)
                {
                    if (cmd.ToLower().CompareTo("attack".ToLower()) == 0)
                    {
                        hum = svMain.UserEngine.GetUserHuman(param1);
                        if (hum != null)
                        {
                            this.SelectTarget(hum);
                        }
                        return;
                    }
                    if (cmd.ToLower().CompareTo("Mob".ToLower()) == 0)
                    {
                        CmdCallMakeMonster(param1, param2);
                        return;
                    }
                    if (cmd.ToLower().CompareTo("RecallMob".ToLower()) == 0)
                    {
                        CmdCallMakeSlaveMonster(param1, param2, (byte)HUtil32.Str_ToInt(param3, 0), (byte)HUtil32.Str_ToInt(param4, 0));
                        return;
                    }
                    if (cmd.ToLower().CompareTo("LuckyPoint".ToLower()) == 0)
                    {
                        hum = svMain.UserEngine.GetUserHuman(param1);
                        if (hum != null)
                        {
                            this.SysMsg(param1 + ": BodyLuck= " + hum.BodyLuckLevel.ToString() + "/" + Convert.ToString(hum.BodyLuck) + " Luck = " + hum.Luck.ToString(), 1);
                        }
                        return;
                    }
                    if (cmd.ToLower().CompareTo("Lottery ticket".ToLower()) == 0)
                    {
                        this.SysMsg("won a prize " + svMain.LottoSuccess.ToString() + ", " + "Nothing " + svMain.LottoFail.ToString() + ", " + "1st prize " + svMain.Lotto1.ToString() + ", " + "2nd prize " + svMain.Lotto2.ToString() + ", " + "3rd prize " + svMain.Lotto3.ToString() + ", " + "4th prize " + svMain.Lotto4.ToString() + ", " + "5th prize " + svMain.Lotto5.ToString() + ", " + "6th prize " + svMain.Lotto6.ToString(), 1);
                        return;
                    }
                    if (cmd.ToLower().CompareTo("ReloadGuild".ToLower()) == 0)
                    {
                        CmdReloadGuild(param1);
                        return;
                    }
                    if (cmd.ToLower().CompareTo("ReloadLineNotice".ToLower()) == 0)
                    {
                        if (svMain.LoadLineNotice(svMain.LINENOTICEFILE))
                        {
                            this.SysMsg(svMain.LINENOTICEFILE + " file is reloaded...", 1);
                        }
                        return;
                    }
                    if (cmd.ToLower().CompareTo("ReadAbuseInformation".ToLower()) == 0)
                    {
                        LoadAbusiveList("!Abuse.txt");
                        this.SysMsg("reread abuse language information ", 1);
                        return;
                    }
                    if (cmd.ToLower().CompareTo("Back".ToLower()) == 0)
                    {
                        this.CharPushed(M2Share.GetBack(this.Dir), 1);
                        return;
                    }
                    if (cmd.ToLower().CompareTo("EnergyWave".ToLower()) == 0)
                    {
                        CmdRushAttack();
                        return;
                    }
                    if (cmd.ToLower().CompareTo("FreePenalty".ToLower()) == 0)
                    {
                        CmdDeletePKPoint(param1);
                        return;
                    }
                    if (cmd.ToLower().CompareTo("PKpoint".ToLower()) == 0)
                    {
                        CmdSendPKPoint(param1, HUtil32.Str_ToInt(param2, 0));
                        return;
                    }
                    if (cmd.ToLower().CompareTo("PKPointIncreased".ToLower()) == 0)
                    {
                        this.IncPKPoint(100);
                        return;
                    }
                    if (cmd.ToLower().CompareTo("ChangeLuck".ToLower()) == 0)
                    {
                        this.BodyLuck = Str_ToFloat(param1);
                        this.AddBodyLuck(0);
                        return;
                    }
                    if (cmd.ToLower().CompareTo("Hunger".ToLower()) == 0)
                    {
                        this.HungryState = HUtil32.Str_ToInt(param1, 0);
                        this.SendMsg(this, Grobal2.RM_MYSTATUS, 0, 0, 0, 0, "");
                        return;
                    }
                    if (cmd == "hair")
                    {
                        this.Hair = (byte)HUtil32.Str_ToInt(param1, 0);
                        this.FeatureChanged();
                        return;
                    }
                    if (cmd.ToLower().CompareTo("Training".ToLower()) == 0)
                    {
                        CmdMakeFullSkill(param1, (byte)HUtil32.Str_ToInt(param2, 1));
                        return;
                    }
                    if (cmd.ToLower().CompareTo("DeleteSkill".ToLower()) == 0)
                    {
                        CmdEraseMagic(param1);
                        return;
                    }
                    if (cmd.ToLower().CompareTo("ChangeJob".ToLower()) == 0)
                    {
                        CmdChangeJob(param1);
                        this.SysMsg(cmd, 1);
                        this.HasLevelUp(1);
                        // 瓷仿摹啊 函版登霸 窍妨备 窃..
                        return;
                    }
                    if (cmd.ToLower().CompareTo("ChangeGender".ToLower()) == 0)
                    {
                        CmdChangeSex();
                        this.SysMsg(cmd, 1);
                        return;
                    }
                    if (cmd.ToLower().CompareTo("NameColor".ToLower()) == 0)
                    {
                        this.DefNameColor = (byte)HUtil32.Str_ToInt(param1, 255);
                        this.ChangeNameColor();
                        return;
                    }
                    if (cmd.ToLower().CompareTo("Mission".ToLower()) == 0)
                    {
                        CmdMissionSetting(param1, param2);
                    }
                    if (cmd.ToLower().CompareTo("MobPlace".ToLower()) == 0)
                    {
                        // x
                        // y
                        // 各捞抚
                        // 付府荐
                        CmdCallMakeMonsterXY(param1, param2, param3, param4);
                        return;
                    }
                    if ((cmd.ToLower().CompareTo("Transparency".ToLower()) == 0) || (cmd.ToLower().CompareTo("tp".ToLower()) == 0))
                    {
                        this.BoHumHideMode = !this.BoHumHideMode;
                        if (this.BoHumHideMode)
                        {
                            this.StatusArr[Grobal2.STATE_TRANSPARENT] = 60 * 60;
                        }
                        else
                        {
                            this.StatusArr[Grobal2.STATE_TRANSPARENT] = 0;
                        }
                        this.CharStatus = this.GetCharStatus();
                        this.CharStatusChanged();
                        return;
                    }
                    if (cmd.ToLower().CompareTo("DeleteItem".ToLower()) == 0)
                    {
                        CmdEraseItem(param1, param2);
                        return;
                    }
                    if (cmd.ToLower().CompareTo("LevelAdjust0".ToLower()) == 0)
                    {
                        this.Abil.Level = (byte)_MIN(40, HUtil32.Str_ToInt(param1, 1));
                        this.HasLevelUp(0);
                        return;
                    }
                    if (cmd.ToLower().CompareTo("Nullifingquest".ToLower()) == 0)
                    {
                        //FillChar(this.QuestStates, sizeof(this.QuestStates), '\0');
                        return;
                    }
                    if (cmd.ToLower().CompareTo("setflag".ToLower()) == 0)
                    {
                        hum = svMain.UserEngine.GetUserHuman(param1);
                        if (hum != null)
                        {
                            idx = HUtil32.Str_ToInt(param2, 0);
                            n = HUtil32.Str_ToInt(param3, 0);
                            hum.SetQuestMark(idx, n);
                            if (hum.GetQuestMark(idx) == 1)
                            {
                                this.SysMsg(hum.UserName + ":  [" + idx.ToString() + "] = ON", 1);
                            }
                            else
                            {
                                this.SysMsg(hum.UserName + ":  [" + idx.ToString() + "] = OFF", 1);
                            }
                        }
                        else
                        {
                            this.SysMsg("@setflag user_name number_of_flag set_value", 0);
                        }
                    }
                    if (cmd.ToLower().CompareTo("setopen".ToLower()) == 0)
                    {
                        hum = svMain.UserEngine.GetUserHuman(param1);
                        if (hum != null)
                        {
                            idx = HUtil32.Str_ToInt(param2, 0);
                            n = HUtil32.Str_ToInt(param3, 0);
                            hum.SetQuestOpenIndexMark(idx, n);
                            if (hum.GetQuestOpenIndexMark(idx) == 1)
                            {
                                this.SysMsg(hum.UserName + ":  unit open [" + idx.ToString() + "] = ON", 1);
                            }
                            else
                            {
                                this.SysMsg(hum.UserName + ":  unit open [" + idx.ToString() + "] = OFF", 1);
                            }
                        }
                        else
                        {
                            this.SysMsg("@setopen user_name number_of_unit set_value", 0);
                        }
                    }
                    if (cmd.ToLower().CompareTo("setunit".ToLower()) == 0)
                    {
                        hum = svMain.UserEngine.GetUserHuman(param1);
                        if (hum != null)
                        {
                            idx = HUtil32.Str_ToInt(param2, 0);
                            n = HUtil32.Str_ToInt(param3, 0);
                            hum.SetQuestFinIndexMark(idx, n);
                            if (hum.GetQuestFinIndexMark(idx) == 1)
                            {
                                this.SysMsg(hum.UserName + ":  unit set [" + idx.ToString() + "] = ON", 1);
                            }
                            else
                            {
                                this.SysMsg(hum.UserName + ":  unit set [" + idx.ToString() + "] = OFF", 1);
                            }
                        }
                        else
                        {
                            this.SysMsg("@setunit user_name number_of_unit set_value", 0);
                        }
                    }
                    if (cmd.ToLower().CompareTo("Reconnection".ToLower()) == 0)
                    {
                        CmdReconnection(param1, param2);
                        // addr, port
                    }
                    // 荤合己 包访 疙飞绢
                    // if CompareText(cmd, 'Wallconquestwarmode') = 0 then begin
                    // UserCastle.BoCastleWarMode := not UserCastle.BoCastleWarMode;
                    // if UserCastle.BoCastleWarMode then SysMsg ('test mode change for wall conquest war', 1)
                    // else Sysmsg ('test mode cancel for wall conquest war', 1);
                    // UserCastle.ActivateDefeseUnits (UserCastle.BoCastleWarMode);
                    // exit;
                    // end;
                    if (cmd.ToLower().CompareTo("DisableFilter".ToLower()) == 0)
                    {
                        svMain.BoEnableAbusiveFilter = !svMain.BoEnableAbusiveFilter;
                        if (svMain.BoEnableAbusiveFilter)
                        {
                            this.SysMsg("[able filter for abuse language]", 1);
                        }
                        else
                        {
                            this.SysMsg("[Disable filter for abuse language]", 1);
                        }
                    }
                    if (cmd == "CHGUSERFULL")
                    {
                        svMain.UserFullCount = _MAX(250, HUtil32.Str_ToInt(param1, 0));
                        this.SysMsg("USERFULL " + svMain.UserFullCount.ToString(), 1);
                        return;
                    }
                    if (cmd == "CHGZENFASTSTEP")
                    {
                        svMain.ZenFastStep = _MAX(100, HUtil32.Str_ToInt(param1, 0));
                        this.SysMsg("ZENFASTSTEP " + svMain.ZenFastStep.ToString(), 1);
                        return;
                    }
                    if (HUtil32.Str_ToInt(cmd, 0) == ObjBase.GET_INFO_PASSWD)
                    {
                        this.SysMsg("current monthly " + svMain.CurrentMonthlyCard.ToString(), 1);
                        this.SysMsg("total timeusage " + svMain.TotalTimeCardUsage.ToString(), 1);
                        this.SysMsg("last mon totalu " + svMain.LastMonthTotalTimeCardUsage.ToString(), 1);
                        this.SysMsg("gross total cnt " + svMain.GrossTimeCardUsage.ToString(), 1);
                        this.SysMsg("gross reset cnt " + svMain.GrossResetCount.ToString(), 1);
                        return;
                    }
                    if (HUtil32.Str_ToInt(cmd, 0) == ObjBase.CHG_ECHO_PASSWD)
                    {
                        this.BoEcho = !this.BoEcho;
                        if (this.BoEcho)
                        {
                            this.SysMsg("Echo on", 1);
                        }
                        else
                        {
                            this.SysMsg("Echo off", 1);
                        }
                        svMain.MainOutMessage("...... ");
                        // adminlog
                    }
                    if (!this.BoEcho)
                    {
                        if (HUtil32.Str_ToInt(cmd, 0) == ObjBase.KIL_SERVER_PASSWD)
                        {
                            // kill server
                            svMain.MainOutMessage("  ");
                            // adminlog
                            if (new System.Random(4).Next() == 0)
                            {
                                svMain.BoGetGetNeedNotice = true;
                                svMain.GetGetNoticeTime = GetTickCount + new System.Random(60 * 60 * 1000).Next();
                                this.SysMsg("timer set up...", 0);
                                svMain.MainOutMessage("   ");
                                // adminlog
                            }
                        }
                    }
                    // 巩颇 措傈 包访 疙飞绢
                    if (cmd.ToLower().CompareTo("ContestPoint".ToLower()) == 0)
                    {
                        CmdGetGuildMatchPoint(param1);
                        return;
                    }
                    if (cmd.ToLower().CompareTo("StartContest".ToLower()) == 0)
                    {
                        // 措访 傈侩甘俊辑父 荤侩且 荐 乐促.
                        CmdStartGuildMatch();
                        return;
                    }
                    if (cmd.ToLower().CompareTo("EndContest".ToLower()) == 0)
                    {
                        // 措访 傈侩甘俊辑父 荤侩且 荐 乐促.
                        CmdEndGuildMatch();
                        return;
                    }
                    if (cmd.ToLower().CompareTo("Announcement".ToLower()) == 0)
                    {
                        CmdAnnounceGuildMembersMatchPoint(param1);
                    }
                    // O/X 柠令 规 疙飞绢 (荤侩磊绰 寇摹扁甫 且 荐 绝促.)
                    if (cmd.ToLower().CompareTo("OXQuizRoom".ToLower()) == 0)
                    {
                    }
                    // ////////
                    // 酱欺绢靛刮 : AdminList俊 '*' 殿鞭(抛胶飘 辑滚 肚绰 菩胶况靛 己傍 饶)
                    if ((this.UserDegree >= Grobal2.UD_SUPERADMIN) || svMain.BoTestServer)
                    {
                        if (cmd.ToLower().CompareTo("Make".ToLower()) == 0)
                        {
                            CmdMakeItem(param1, HUtil32.Str_ToInt(param2, 1));
                            return;
                        }
                        if (cmd.ToLower().CompareTo("DelGold".ToLower()) == 0)
                        {
                            CmdDeleteUserGold(param1, param2);
                            return;
                        }
                        if (cmd.ToLower().CompareTo("AddGold".ToLower()) == 0)
                        {
                            CmdAddUserGold(param1, param2);
                            return;
                        }
                        if (cmd == "Test_GOLD_Change")
                        {
                            if (this.BoEcho)
                            {
                                svMain.MainOutMessage("[MakeGold] " + this.UserName + " " + param1);
                            }
                            this.Gold = _MIN(ObjBase.BAGGOLD, HUtil32.Str_ToInt(param1, 0));
                            this.GoldChanged();
                            return;
                        }
                        if (cmd.ToLower().CompareTo("WeaponRefinery".ToLower()) == 0)
                        {
                            CmdRefineWeapon(HUtil32.Str_ToInt(param1, 0), HUtil32.Str_ToInt(param2, 0), HUtil32.Str_ToInt(param3, 0), HUtil32.Str_ToInt(param4, 0));
                            if (this.BoEcho)
                            {
                                svMain.MainOutMessage("[Refine] " + this.UserName + " " + param1 + " " + param2 + " " + param3 + " " + param4);
                            }
                            return;
                        }
                        if (cmd.ToLower().CompareTo("ReloadAdmin".ToLower()) == 0)
                        {
                            LocalDB.FrmDB.LoadAdminFiles();
                            svMain.UserEngine.SendInterMsg(Grobal2.ISM_RELOADADMIN, svMain.ServerIndex, "");
                            this.SysMsg(cmd + " It applied to all servers", 1);
                            return;
                        }
                        if (cmd.ToLower().CompareTo("MarketOpen".ToLower()) == 0)
                        {
                            SqlEngn.SqlEngine.Open(true);
                            svMain.UserEngine.SendInterMsg(Grobal2.ISM_MARKETOPEN, svMain.ServerIndex, "");
                            this.SysMsg(cmd + " Commission merchant system is opened.", 1);
                            return;
                        }
                        if (cmd.ToLower().CompareTo("MarketClose".ToLower()) == 0)
                        {
                            SqlEngn.SqlEngine.Open(false);
                            svMain.UserEngine.SendInterMsg(Grobal2.ISM_MARKETCLOSE, svMain.ServerIndex, "");
                            this.SysMsg(cmd + " Commission merchant system is closed.", 1);
                            return;
                        }
                        if (cmd.ToLower().CompareTo("ReloadNpc".ToLower()) == 0)
                        {
                            // 磊脚狼 林搁俊 乐绰 npc 沥焊甫 府肺靛 矫挪促.
                            CmdReloadNpc(param1);
                            return;
                        }
                        if (cmd.ToLower().CompareTo("ReloadDefaultNpc".ToLower()) == 0)
                        {
                            CmdReloadDefaultNpc();
                            return;
                        }
                        if (cmd.ToLower().CompareTo("ReloadMonItems".ToLower()) == 0)
                        {
                            svMain.UserEngine.ReloadAllMonsterItems();
                            this.SysMsg("monsters item information is all reloaded.", 1);
                            return;
                        }
                        if (cmd.ToLower().CompareTo("ReloadDiary".ToLower()) == 0)
                        {
                            if (LocalDB.FrmDB.LoadQuestDiary() < 0)
                            {
                                this.SysMsg("QuestDiarys reload failure...", 0);
                            }
                            else
                            {
                                this.SysMsg("QuestDiarys reload successful", 1);
                            }
                            return;
                        }
                        if (cmd.ToLower().CompareTo("AdjustLevel".ToLower()) == 0)
                        {
                            CmdManLevelChange(param1, HUtil32.Str_ToInt(param2, 1));
                            return;
                        }
                        if (cmd.ToLower().CompareTo("AdjustExp".ToLower()) == 0)
                        {
                            CmdManExpChange(param1, HUtil32.Str_ToInt(param2, 1));
                            return;
                        }
                        if (cmd.ToLower().CompareTo("AddGuild".ToLower()) == 0)
                        {
                            CmdCreateGuild(param1, param2);
                            return;
                        }
                        if (cmd.ToLower().CompareTo("DelGuild".ToLower()) == 0)
                        {
                            CmdDeleteGuild(param1);
                            return;
                        }
                        if (cmd.ToLower().CompareTo("ChangeSabukLord".ToLower()) == 0)
                        {
                            CmdChangeUserCastleOwner(param1, true);
                            return;
                        }
                        if (cmd.ToLower().CompareTo("ForcedWallconquestWar".ToLower()) == 0)
                        {
                            svMain.UserCastle.BoCastleUnderAttack = !svMain.UserCastle.BoCastleUnderAttack;
                            if (svMain.UserCastle.BoCastleUnderAttack)
                            {
                                svMain.UserCastle.CastleAttackStarted  =  HUtil32.GetTickCount();
                                svMain.UserCastle.StartCastleWar();
                            }
                            else
                            {
                                svMain.UserCastle.FinishCastleWar();
                            }
                            return;
                        }
                        if (cmd.ToLower().CompareTo("AddToItemEvent".ToLower()) == 0)
                        {
                            if (param1 != "")
                            {
                                svMain.EventItemList.Add(param1, (svMain.EventItemGifeBaseNumber + svMain.EventItemList.Count) as Object);
                                this.SysMsg("AddToItemEvent " + param1, 1);
                            }
                            if (param2 != "")
                            {
                                svMain.EventItemList.Add(param2, (svMain.EventItemGifeBaseNumber + svMain.EventItemList.Count) as Object);
                                this.SysMsg("AddToItemEvent " + param2, 1);
                            }
                            if (param3 != "")
                            {
                                svMain.EventItemList.Add(param3, (svMain.EventItemGifeBaseNumber + svMain.EventItemList.Count) as Object);
                                this.SysMsg("AddToItemEvent " + param3, 1);
                            }
                            if (param4 != "")
                            {
                                svMain.EventItemList.Add(param4, (svMain.EventItemGifeBaseNumber + svMain.EventItemList.Count) as Object);
                                this.SysMsg("AddToItemEvent " + param4, 1);
                            }
                            if (param5 != "")
                            {
                                svMain.EventItemList.Add(param5, (svMain.EventItemGifeBaseNumber + svMain.EventItemList.Count) as Object);
                                this.SysMsg("AddToItemEvent " + param5, 1);
                            }
                            return;
                        }
                        if (cmd.ToLower().CompareTo("AddToItemEventAsPieces".ToLower()) == 0)
                        {
                            n = HUtil32.Str_ToInt(param2, 1);
                            for (i = 1; i <= n; i++)
                            {
                                svMain.EventItemList.Add(param1, (svMain.EventItemGifeBaseNumber + svMain.EventItemList.Count) as Object);
                                this.SysMsg("AddToItemEvent " + param1, 1);
                            }
                            return;
                        }
                        if (cmd.ToLower().CompareTo("ItemEventList".ToLower()) == 0)
                        {
                            this.SysMsg("[Item event list]", 1);
                            for (i = 0; i < svMain.EventItemList.Count; i++)
                            {
                                this.SysMsg(svMain.EventItemList[i] + " " + ((int)svMain.EventItemList.Values[i]).ToString(), 1);
                            }
                            return;
                        }
                        if (cmd.ToLower().CompareTo("StartingGiftNo".ToLower()) == 0)
                        {
                            svMain.EventItemGifeBaseNumber = HUtil32.Str_ToInt(param1, 0);
                            this.SysMsg("Starting no. of gift certificate " + svMain.EventItemGifeBaseNumber.ToString(), 1);
                            return;
                        }
                        if (cmd.ToLower().CompareTo("DeleteAllItemEven".ToLower()) == 0)
                        {
                            svMain.EventItemList.Clear();
                            this.SysMsg("DeleteAllItemOfItemEvent", 1);
                            return;
                        }
                        if (cmd.ToLower().CompareTo("StartItemEvent".ToLower()) == 0)
                        {
                            svMain.UserEngine.BoUniqueItemEvent = !svMain.UserEngine.BoUniqueItemEvent;
                            if (svMain.UserEngine.BoUniqueItemEvent)
                            {
                                this.SysMsg("start of item event", 1);
                            }
                            else
                            {
                                this.SysMsg("end of item event", 1);
                            }
                            return;
                        }
                        if (cmd.ToLower().CompareTo("ItemEventTerm".ToLower()) == 0)
                        {
                            svMain.UserEngine.UniqueItemEventInterval = HUtil32.Str_ToInt(param1, 30) * 60 * 1000;
                            this.SysMsg("term of item event = " + HUtil32.Str_ToInt(param1, 30).ToString() + "Min", 1);
                            return;
                        }
                        if (cmd.ToLower().CompareTo("AdjustTestLevel".ToLower()) == 0)
                        {
                            this.Abil.Level = (byte)_MIN(M2Share.MAXLEVEL - 1, HUtil32.Str_ToInt(param1, 1));
                            // 50
                            this.HasLevelUp(1);
                            return;
                        }
                        if (cmd.ToLower().CompareTo("OPTraining".ToLower()) == 0)
                        {
                            CmdMakeOtherChangeSkillLevel(param1, param2, (byte)HUtil32.Str_ToInt(param3, 1));
                            return;
                        }
                        if (cmd.ToLower().CompareTo("OPDeleteSkill".ToLower()) == 0)
                        {
                            CmdThisManEraseMagic(param1, param2);
                        }
                        if (cmd.ToLower().CompareTo("ChangeWeaponDura".ToLower()) == 0)
                        {
                            n = _MIN(65, _MAX(0, HUtil32.Str_ToInt(param1, 0)));
                            if ((this.UseItems[Grobal2.U_WEAPON].Index != 0) && (n > 0))
                            {
                                this.UseItems[Grobal2.U_WEAPON].DuraMax = (ushort)(n * 1000);
                                this.SendMsg(this, Grobal2.RM_DURACHANGE, Grobal2.U_WEAPON, this.UseItems[Grobal2.U_WEAPON].Dura, this.UseItems[Grobal2.U_WEAPON].DuraMax, 0, "");
                            }
                            return;
                        }
                        // /////////////////////
                        // added by sonmg...
                        if (cmd.ToLower().CompareTo("Upgrade".ToLower()) == 0)
                        {
                            CmdUpgradeItem(param1, param2, 0, 0, HUtil32.Str_ToInt(param3, 0));
                            return;
                        }
                        if (cmd.ToLower().CompareTo("allgem".ToLower()) == 0)
                        {
                            CmdMakeAllJewelryItem(0);
                            return;
                        }
                        if (cmd.ToLower().CompareTo("allorb".ToLower()) == 0)
                        {
                            CmdMakeAllJewelryItem(1);
                            return;
                        }
                        if (cmd.ToLower().CompareTo("ReloadMakeItemList".ToLower()) == 0)
                        {
                            // 力炼 犁丰 格废阑 府肺靛 矫挪促.
                            LocalDB.FrmDB.LoadMakeItemList();
                            svMain.UserEngine.SendInterMsg(Grobal2.ISM_RELOADMAKEITEMLIST, svMain.ServerIndex, "");
                            this.SysMsg(cmd + " is reloaded to whole server.", 1);
                            return;
                        }
                        // /////////////////////
                        if ((cmd.ToLower().CompareTo("AgitDecoMonCount".ToLower()) == 0) || (cmd.ToLower().CompareTo("AgitDecoMonCount".ToLower()) == 0))
                        {
                            CmdAgitDecoMonCount(HUtil32.Str_ToInt(param1, 1));
                            return;
                        }
                        if ((cmd.ToLower().CompareTo("AgitDecoMonCountHere".ToLower()) == 0) || (cmd.ToLower().CompareTo("AgitDecoMonCountHere".ToLower()) == 0))
                        {
                            CmdAgitDecoMonCountHere();
                            return;
                        }
                    }
                    // 
                    // if CompareText (cmd, 'ReloadGuildAll') = 0 then begin
                    // CmdReloadGuildAll (param1);
                    // exit;
                    // end;
                    if (cmd.ToLower().CompareTo("ReloadGuildAgit".ToLower()) == 0)
                    {
                        CmdReloadGuildAgit();
                        return;
                    }
                    // 菊俊 乐绰 各阑 茄规俊 磷烙.
                    if (svMain.BoTestServer && (cmd.ToLower().CompareTo("OneKill".ToLower()) == 0))
                    {
                        CmdOneKillMob();
                        return;
                    }
                }
                return;
            }
            else
            {
                // NoChat 甘加己 眠啊(sonmg 2004/10/12)
                if (this.PEnvir.NoChat)
                {
                    this.SysMsg("你不能在这个地图上聊天。", 0);
                    return;
                }
                // 档硅 规瘤 风凭
                if ((saystr.Trim() == LatestSayStr) && (HUtil32.GetTickCount() - BombSayTime < 3000))
                {
                    BombSayCount++;
                    if (BombSayCount >= 2)
                    {
                        BoShutUpMouse = true;
                        ShutUpMouseTime = GetTickCount + 60 * 1000;
                        this.SysMsg("[由于您重复发出相同内容，一分钟内将被禁止交谈。]", 0);
                    }
                }
                else
                {
                    // 绊加 盲泼 规瘤 风凭(sonmg 2006/02/06)
                    if (HUtil32.GetTickCount() - BombSayTime < 2000)
                    {
                        BombSayCount++;
                        if (BombSayCount >= 5)
                        {
                            BoShutUpMouse = true;
                            ShutUpMouseTime = GetTickCount + 30 * 1000;
                            this.SysMsg("[由于您重复发出相同内容，30秒内将被禁止交谈。]", 0);
                        }
                        LatestSayStr = saystr.Trim();
                        BombSayTime  =  HUtil32.GetTickCount();
                    }
                    else
                    {
                        LatestSayStr = saystr.Trim();
                        BombSayTime  =  HUtil32.GetTickCount();
                        BombSayCount = 0;
                    }
                }
                // 档硅肺 盲泼 陛瘤甫 秦力
                if (HUtil32.GetTickCount() > ShutUpMouseTime)
                {
                    BoShutUpMouse = false;
                }
                boshutup = BoShutUpMouse;
                // 款康磊俊 狼秦 盲泼陛瘤 凳
                if (svMain.ShutUpList.FFind(this.UserName) >= 0)
                {
                    boshutup = true;
                }
                if (!boshutup)
                {
                    if (saystr[1] == "/")
                    {
                        str = saystr.Substring(2 - 1, saystr.Length - 1);
                        if (this.UserDegree >= Grobal2.UD_SYSOP)
                        {
                            if (str.ToLower().CompareTo("who ".ToLower()) == 0)
                            {
                                this.NilMsg("在线人数：" + svMain.UserEngine.GetUserCount().ToString());
                                return;
                            }
                            if (this.UserDegree >= Grobal2.UD_SUPERADMIN)
                            {
                                if (str.ToLower().CompareTo("total ".ToLower()) == 0)
                                {
                                    this.NilMsg("所有服务器的在线人数：" + svMain.TotalUserCount.ToString());
                                    return;
                                }
                            }
                        }
                        str = HUtil32.GetValidStr3(str, ref param1, new string[] { " " });
                        Whisper(param1, str);
                        return;
                    }
                    if (saystr[0] == '!')
                    {
                        if (saystr.Length >= 2)
                        {
                            if (saystr[1] == '!')
                            {
                                str = saystr.Substring(3 - 1, saystr.Length - 2);
                                this.GroupMsg(this.UserName + ": " + str);
                                return;
                            }
                            if ((saystr[1] == '~') || (saystr[1] == '&'))
                            {
                                if (this.MyGuild != null)
                                {
                                    str = saystr.Substring(3 - 1, saystr.Length - 2);
                                    ((TGuild)this.MyGuild).GuildMsg(this.UserName + ":" + str);
                                    svMain.UserEngine.SendInterMsg(Grobal2.ISM_GUILDMSG, svMain.ServerIndex, ((TGuild)this.MyGuild).GuildName + "/" + this.UserName + ":" + str);
                                }
                                return;
                            }
                        }
                        if (!this.PEnvir.QuizZone)
                        {
                            if (HUtil32.GetTickCount() - this.LatestCryTime > 10 * 1000)
                            {
                                if (this.Abil.Level <= 7)
                                {
                                    this.SysMsg("喊话只有级别高于8级的玩家才能使用。", 0);
                                }
                                else
                                {
                                    if (this.IsMyGuildMaster())
                                    {
                                        str = saystr.Substring(2 - 1, saystr.Length - 1);
                                        svMain.UserEngine.GuildAgitCry(Grobal2.RM_CRY, this.PEnvir, this.CX, this.CY, 50, "(!)" + this.UserName + ":" + str);
                                    }
                                    else
                                    {
                                        this.LatestCryTime  =  HUtil32.GetTickCount();
                                        str = saystr.Substring(2 - 1, saystr.Length - 1);
                                        svMain.UserEngine.CryCry(Grobal2.RM_CRY, this.PEnvir, this.CX, this.CY, 50, "(!)" + this.UserName + ":" + str);
                                    }
                                }
                            }
                            else
                            {
                                this.SysMsg((10 - ((HUtil32.GetTickCount() - this.LatestCryTime) / 1000)).ToString() + "秒以后才能再次使用喊话。", 0);
                            }
                        }
                        else
                        {
                            this.SysMsg("你不能使用。", 0);
                        }
                        return;
                    }
                    else
                    {
                        if (HUtil32.CompareLStr(saystr, ":)", 2))
                        {
                            if (fLover.GetLoverName != "")
                            {
                                str = saystr.Substring(3 - 1, saystr.Length - 2);
                                LoverWhisper(fLover.GetLoverName, str);
                                return;
                            }
                        }
                    }
                    base.Say(saystr);
                }
                else
                {
                    this.SysMsg("禁止聊天。", 0);
                }
            }
        }

        public void ThinkEtc()
        {
            if (Bright != svMain.MirDayTime)
            {
                Bright = svMain.MirDayTime;
                this.SendMsg(this, Grobal2.RM_DAYCHANGING, 0, 0, 0, 0, "");
            }
        }

        public void ReadySave()
        {
            this.Abil.HP = this.WAbil.HP;
            BrokeDeal();
        }

        // ----------------------------------------------
        public void SendLogon()
        {
            TMessageBodyWL wl;
            Def = Grobal2.MakeDefaultMsg(Grobal2.SM_LOGON, this.ActorId, this.CX, this.CY, MakeWord(this.Dir, this.Light));
            wl.lParam1 = this.Feature();
            wl.lParam2 = this.CharStatus;
            if (this.AllowGroup)
            {
                wl.lTag1 = HUtil32.MakeLong(MakeWord(1, 0), 0);
            }
            else
            {
                wl.lTag1 = 0;
            }
            wl.lTag2 = 0;
            SendSocket(Def, EDcode.EncodeBuffer(wl));
        }

        public void SendAreaState()
        {
            int n;
            n = 0;
            if (this.PEnvir.FightZone)
            {
                n = n | Grobal2.AREA_FIGHT;
            }
            if (this.PEnvir.Fight2Zone)
            {
                n = n | Grobal2.AREA_FIGHT;
            }
            // sonmg (2004/12/23)
            if (this.PEnvir.LawFull)
            {
                n = n | Grobal2.AREA_SAFE;
            }
            if (this.BoInFreePKArea)
            {
                n = n | Grobal2.AREA_FREEPK;
            }
            SendDefMessage(Grobal2.SM_AREASTATE, n, 0, 0, 0, "");
        }

        public void DoStartupQuestNow()
        {
            // if StartupQuestNpc <> nil then begin
            // TMerchant (StartupQuestNpc).UserCall (self);
            // end;
            if (svMain.DefaultNpc != null)
            {
                svMain.DefaultNpc.NpcSayTitle(this, "@_UserLogin");
            }
        }

        public void Operate()
        {
            TMessageInfo msg = null;
            TCharDesc cdesc;
            TMessageBodyWL wl;
            TMessageBodyW mbw;
            TShortMessage smsg;
            string str;
            string strupgrade;
            short ahour;
            short amin;
            short asec;
            short amsec;
            int i;
            int n;
            bool flag;
            TStdItem ps;
            TCreature Cret;
            int identbackup;
            TDefaultMessage DefMsg;
            TUserHuman hum;
            string lovername;
            int svidx=0;
            TStallInfo StallInfo;
            try
            {
                if (this.BoDealing)
                {
                    // 寒焊绊 芭贰秦辑 捣汗荤登绰 滚弊甫 绊魔
                    if ((this.GetFrontCret() != this.DealCret) || (this.DealCret == this) || (this.DealCret == null))
                    {
                        BrokeDeal();
                    }
                }
                // 拌沥矫埃 父丰俊 狼茄 矫埃眉农棺 皋技瘤傈价 2003-01-17 : PDS
                CheckExpiredTime();
                if (BoAccountExpired)
                {
                    this.SysMsg("您的帐号已到期。", 0);
                    this.SysMsg("连接中断。", 0);
                    svMain.MainOutMessage("[AccountExpired] " + this.UserName + " (" + AvailableMode.ToString() + ")");
                    EmergencyClose = true;
                    BoAccountExpired = false;
                    // 皋技瘤绰 茄锅 父
                }
                if (this.BoAllowFireHit)
                {
                    // 堪拳搬 秦力..
                    if (HUtil32.GetTickCount() - this.LatestFireHitTime > 20 * 1000)
                    {
                        this.BoAllowFireHit = false;
                        this.SysMsg("精神火焰消失。", 0);
                        SendSocket(null, "+UFIR");
                        if (svMain.BoGetGetNeedNotice)
                        {
                            // ///////////////////
                            if (HUtil32.GetTickCount() - svMain.GetGetNoticeTime > 2 * 60 * 60 * 1000)
                            {
                                GetGetNotices();
                            }
                        }
                    }
                }
                if (this.BoAllowTwinHit == 2)
                {
                    // 街锋曼 秦力..
                    this.BoAllowTwinHit = 0;
                    // SysMsg ('街锋曼捞 秦力登菌嚼聪促.', 0);
                    SendSocket(null, "+UTWN");
                }
                if (BoTimeRecallGroup)
                {
                    if (HUtil32.GetTickCount() > TimeRecallEnd)
                    {
                        BoTimeRecall = false;
                        BoTimeRecallGroup = false;
                        this.SpaceMove(TimeRecallMap, (short)TimeRecallX, (short)TimeRecallY, 0);
                    }
                }
                else if (BoTimeRecall)
                {
                    if (HUtil32.GetTickCount() > TimeRecallEnd)
                    {
                        BoTimeRecall = false;
                        this.SpaceMove(TimeRecallMap, (short)TimeRecallX, (short)TimeRecallY, 0);
                    }
                }
                if (HUtil32.GetTickCount() - operatetime_30sec > 20 * 1000)
                {
                    operatetime_30sec  =  HUtil32.GetTickCount();
                    if (this.BoTaiwanEventUser)
                    {
                        // 林函 荤恩甸俊霸 磊脚狼 困摹甫 舅赴促.
                        svMain.UserEngine.CryCry(Grobal2.RM_CRY, this.PEnvir, this.CX, this.CY, 1000, this.UserName + " is " + this.CX.ToString() + " " + this.CY.ToString() + " (" + this.TaiwanEventItemName + ")");
                    }
                }
                if (HUtil32.GetTickCount() - operatetime > 3000)
                {
                    operatetime  =  HUtil32.GetTickCount();
                    // 胶菩靛琴(speedhack) 八荤
                    // /SendDefMessage (SM_TIMECHECK_MSG, GetTickCount, 0, 0, 0, '');
                    CheckHomePos();
                    // 促弗 某腐苞 般媚脸绰瘤甫 八荤茄促.
                    n = this.PEnvir.GetDupCount(this.CX, this.CY);
                    if (n >= 2)
                    {
                        if (!this.BoDuplication)
                        {
                            this.BoDuplication = true;
                            this.DupStartTime  =  HUtil32.GetTickCount();
                        }
                    }
                    else
                    {
                        this.BoDuplication = false;
                    }
                    if ((n >= 3) && (HUtil32.GetTickCount() - this.DupStartTime > 3000) || (n == 2) && (HUtil32.GetTickCount() - this.DupStartTime > 10000))
                    {
                        if (HUtil32.GetTickCount() - this.DupStartTime < 20000)
                        {
                            this.CharPushed(new System.Random(8).Next(), 1);
                        }
                        // else
                        // RandomSpaceMove (PEnvir.MapName, 0);
                    }
                }
                // 傍己傈 吝牢 版快
                if (svMain.UserCastle.BoCastleUnderAttack)
                {
                    // 傍己傈 瘤开郴俊辑绰 橇府乔纳捞 瘤开
                    this.BoInFreePKArea = svMain.UserCastle.IsCastleWarArea(this.PEnvir, this.CX, this.CY);
                }
                if (HUtil32.GetTickCount() - operatetime_sec > 1000)
                {
                    operatetime_sec  =  HUtil32.GetTickCount();
                    // 立加 肺弊甫 巢辫
                    // 且牢 矫埃狼 版拌俊绰 肺弊甫 巢辫.
                    ahour = (short)DateTime.Now.Hour;
                    amin = (short)DateTime.Now.Minute;
                    asec = (short)DateTime.Now.Second;
                    amsec = (short)DateTime.Now.Millisecond;
                    // 且牢 矫埃 矫累 趣篮 场
                    if (svMain.DiscountForNightTime)
                    {
                        if (((ahour == svMain.HalfFeeStart) || (ahour == svMain.HalfFeeEnd)) && (amin == 0) && (asec <= 30))
                        {
                            // 且牢 矫埃捞 矫累登绰 锭
                            if (HUtil32.GetTickCount() - LoginTime > 60 * 1000)
                            {
                                // 且牢矫埃矫累锭 扁废阑 窍瘤 臼篮 版快
                                // 且牢 矫埃 捞傈俊 立加茄 版快烙
                                WriteConLog();
                                LoginTime  =  HUtil32.GetTickCount();
                                LoginDateTime = DateTime.Now;
                            }
                        }
                    }
                    // 巩颇傈栏肺 瘤开俊 蝶扼辑 捞抚捞 祸彬捞 函版瞪 版快啊 乐澜
                    if (this.MyGuild != null)
                    {
                        if (((TGuild)this.MyGuild).GuildName != "")
                        {
                            // 2004/04/28(sonmg)
                            if (((TGuild)this.MyGuild).KillGuilds.Count > 0)
                            {
                                // 巩颇傈 吝烙
                                flag = this.InGuildWarSafeZone();
                                if (boGuildwarsafezone != flag)
                                {
                                    boGuildwarsafezone = flag;
                                    // 瘤开俊 蝶扼辑 捞抚祸捞 函版凳
                                    this.ChangeNameColor();
                                }
                            }
                        }
                    }
                    // 傍己傈 吝牢 版快
                    if (svMain.UserCastle.BoCastleUnderAttack)
                    {
                        // 荤合己狼 郴己阑 痢飞窍搁 己阑 瞒瘤窍霸 等促.
                        if (this.PEnvir == svMain.UserCastle.CorePEnvir)
                        {
                            // 郴己救俊 乐绰 版快
                            if ((this.MyGuild != null) && !svMain.UserCastle.IsCastleMember(this))
                            {
                                // 己阑 傍拜窍绰 巩颇啊 痢飞茄 版快
                                if (svMain.UserCastle.IsRushCastleGuild((TGuild)this.MyGuild))
                                {
                                    // 傍己傈阑 脚没茄 巩颇盔捞 郴己 救俊 乐澜
                                    if (svMain.UserCastle.CheckCastleWarWinCondition((TGuild)this.MyGuild))
                                    {
                                        // 郴己 痢飞 己傍
                                        svMain.UserCastle.ChangeCastleOwner((TGuild)this.MyGuild);
                                        // 促弗 辑滚俊 舅覆
                                        svMain.UserEngine.SendInterMsg(Grobal2.ISM_CHANGECASTLEOWNER, svMain.ServerIndex, ((TGuild)this.MyGuild).GuildName);
                                        // 傍己傈篮 辆丰凳, 铰府巩 捞寇俊 葛电 荤恩篮 促弗 镑栏肺 朝扼皑
                                        if (svMain.UserCastle.GetRushGuildCount() <= 1)
                                        {
                                            svMain.UserCastle.FinishCastleWar();
                                        }
                                        // 傍拜磊啊 2巩颇 捞惑捞搁 3矫埃捞 场唱具 辆丰凳
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        this.BoInFreePKArea = false;
                    }
                    if (this.AreaStateOrNameChanged)
                    {
                        this.AreaStateOrNameChanged = false;
                        SendAreaState();
                        this.UserNameChanged();
                    }
                    // 20003/02/11 弊缝盔 困摹 傈崔
                    if (this.GroupOwner != null)
                    {
                        for (i = 0; i < this.GroupOwner.GroupMembers.Count; i++)
                        {
                            Cret = this.GroupOwner.GroupMembers.Values[i] as TCreature;
                            // if (cret <> self) and (cret.MapName = MapName) then
                            if (Cret.MapName == this.MapName)
                            {
                                Cret.SendMsg(this, Grobal2.RM_GROUPPOS, this.Dir, this.CX, this.CY, this.RaceServer, "");
                                Cret.SendMsg(this, Grobal2.RM_HEALTHSPELLCHANGED, 0, 0, 0, 0, "");
                            }
                        }
                    }
                    if (this.SlaveList.Count >= 1)
                    {
                        for (i = 0; i < this.SlaveList.Count; i++)
                        {
                            Cret = this.SlaveList[i] as TCreature;
                            if ((Cret != null) && (Cret.MapName == this.MapName))
                            {
                                this.SendMsg(Cret, Grobal2.RM_GROUPPOS, Cret.Dir, Cret.CX, Cret.CY, Cret.RaceServer, "");
                                this.SendMsg(Cret, Grobal2.RM_HEALTHSPELLCHANGED, 0, 0, 0, 0, "");
                                // cret.SendMsg(self, RM_HEALTHSPELLCHANGED, 0, 0, 0, 0, '');
                            }
                        }
                    }
                }
                if (HUtil32.GetTickCount() - operatetime_500m >= 500)
                {
                    operatetime_500m  =  HUtil32.GetTickCount();
                    // 措父 捞亥飘 包访
                    if (this.BoTaiwanEventUser)
                    {
                        // 捞亥飘 秦力.....
                        flag = false;
                        for (i = 0; i < this.ItemList.Count; i++)
                        {
                            ps = svMain.UserEngine.GetStdItem(this.ItemList[i].Index);
                            if (ps != null)
                            {
                                if (ps.StdMode == ObjBase.TAIWANEVENTITEM)
                                {
                                    // 措父 捞亥飘, 捞亥飘 酒捞袍阑 林栏搁 钎矫巢
                                    flag = true;
                                }
                            }
                        }
                        if (!flag)
                        {
                            // 捞亥飘 秦力.....
                            this.TaiwanEventItemName = "";
                            this.BoTaiwanEventUser = false;
                            // 某腐狼 祸彬阑 官槽促.
                            this.StatusArr[Grobal2.STATE_BLUECHAR] = 1;
                            // 鸥烙 酒眶
                            this.Light = this.GetMyLight();
                            this.SendRefMsg(Grobal2.RM_CHANGELIGHT, 0, 0, 0, 0, "");
                            this.CharStatus = this.GetCharStatus();
                            this.CharStatusChanged();
                            this.UserNameChanged();
                        }
                    }
                }
                // (*if GetTickCount - ClientMsgTime > 1000 * 2 then begin
                // r := ClientMsgCount / (HUtil32.GetTickCount() - ClientMsgTime) * 1000;
                // //SysMsg (FloatToStr(r), 0);
                // ClientMsgTime : =  HUtil32.GetTickCount();
                // if r >= 1.8 then begin
                // Inc (ClientSpeedHackDetect);
                // if ClientSpeedHackDetect >= 3 then begin
                // MainOutMessage ('[using hacking program] ' + UserName);
                // SysMsg ('=====================================================', 0);
                // SysMsg ('recorded as user of hacking program .', 0);
                // SysMsg ('Please be noted that you may have sanction  as like account seizure.', 0);
                // SysMsg ('Connection was terminated by force.', 0);
                // SysMsg ('=====================================================', 0);
                // UserSocketClosed := TRUE;
                // end;
                // end else
                // ClientSpeedHackDetect := 0;
                // ClientMsgCount := 0;
                // end; *)
            }
            catch
            {
                svMain.MainOutMessage("[Exception] TUserHuman.Operate 1");
            }
            identbackup = 0;
            try
            {
                while (this.GetMsg(ref msg))
                {
                    identbackup = msg.Ident;
                    switch (msg.Ident)
                    {
                        case Grobal2.CM_CLIENT_CHECKTIME:
                            //todo 检查客户端时间
                            break;
                        case Grobal2.CM_TURN:
                            // x
                            // y
                            // dir
                            if (this.Death || !TurnXY(msg.lParam1, msg.lParam2, msg.wParam))
                            {
                                SendSocket(null, "+FAIL/" + GetTickCount.ToString());
                            }
                            else
                            {
                                SendSocket(null, "+GOOD/" + GetTickCount.ToString());
                            }
                            break;
                        case Grobal2.CM_WALK:
                            // x
                            // y
                            if (this.Death || !WalkXY(msg.lParam1, msg.lParam2))
                            {
                                SendSocket(null, "+FAIL/" + GetTickCount.ToString());
                            }
                            else
                            {
                                SendSocket(null, "+GOOD/" + GetTickCount.ToString());
                                ClientMsgCount++;
                            }
                            break;
                        case Grobal2.CM_RUN:
                            // x
                            // y
                            if (this.Death || !RunXY(msg.lParam1, msg.lParam2))
                            {
                                SendSocket(null, "+FAIL/" + GetTickCount.ToString());
                            }
                            else
                            {
                                SendSocket(null, "+GOOD/" + GetTickCount.ToString());
                                ClientMsgCount++;
                            }
                            break;
                        case Grobal2.CM_HIT:
                        case Grobal2.CM_HEAVYHIT:
                        case Grobal2.CM_BIGHIT:
                        case Grobal2.CM_POWERHIT:
                        case Grobal2.CM_LONGHIT:
                        case Grobal2.CM_WIDEHIT:
                        case Grobal2.CM_CROSSHIT:
                        case Grobal2.CM_TWINHIT:
                        case Grobal2.CM_FIREHIT:
                            // 2003/03/15 脚痹公傍
                            if (!this.Death)
                            {
                                // X
                                // Y
                                // DIR
                                if (HitXY(msg.Ident, msg.lParam1, msg.lParam2, (byte)msg.wParam))
                                {
                                    // wParam = 规氢
                                    // SendSocket (nil, '+GOOD/' + IntToStr(HUtil32.GetTickCount()));
                                    SendSocket(null, "+GOOD/" + GetTickCount.ToString() + "/" + this.HitSpeed.ToString());
                                    // 秦欧砒眉农(sonmg)
                                    ClientMsgCount++;
                                }
                                else
                                {
                                    SendSocket(null, "+FAIL/" + GetTickCount.ToString());
                                }
                            }
                            else
                            {
                                SendSocket(null, "+FAIL/" + GetTickCount.ToString());
                            }
                            break;
                        case Grobal2.CM_THROW:
                            if (!this.Death)
                            {
                                // if HitXY (Ident, lparam1{X}, lparam2{Y}, wParam{DIR}) then begin  //wParam = 规氢
                                SendSocket(null, "+GOOD/" + GetTickCount.ToString());
                                // Inc (ClientMsgCount);
                                // end else
                                // SendSocket (nil, '+FAIL/' + IntToStr(HUtil32.GetTickCount()));
                            }
                            break;
                        case Grobal2.CM_SPELL:
                            if (!this.Death)
                            {
                                // magid
                                // targetx
                                // targety
                                // target cret
                                if (SpellXY(msg.wParam, msg.lParam1, msg.lParam2, msg.lParam3))
                                {
                                    SendSocket(null, "+GOOD/" + GetTickCount.ToString());
                                    ClientMsgCount++;
                                }
                                else
                                {
                                    // 龋去籍 滚弊肺 牢秦 昏力(sonmg 2005/10/06) 抛胶飘 夸噶(何累侩)
                                    SendSocket(null, "+FAIL/" + GetTickCount.ToString());
                                }
                            }
                            else
                            {
                                SendSocket(null, "+FAIL/" + GetTickCount.ToString());
                            }
                            break;
                        case Grobal2.CM_SITDOWN:
                            if (!this.Death)
                            {
                                // x
                                // y
                                // dir
                                SitdownXY(msg.lParam1, msg.lParam2, msg.wParam);
                                SendSocket(null, "+GOOD/" + GetTickCount.ToString());
                            }
                            else
                            {
                                SendSocket(null, "+FAIL/" + GetTickCount.ToString());
                            }
                            break;
                        case Grobal2.CM_SAY:
                            if (msg.description != "")
                            {
                                Say(msg.description);
                            }
                            break;
                        case Grobal2.CM_DROPITEM:
                            if (this.UserDropItem(msg.description, msg.lParam1))
                            {
                                SendDefMessage(Grobal2.SM_DROPITEM_SUCCESS, msg.lParam1, 0, 0, 0, msg.description);
                            }
                            else
                            {
                                SendDefMessage(Grobal2.SM_DROPITEM_FAIL, msg.lParam1, 0, 0, 0, msg.description);
                            }
                            break;
                        case Grobal2.CM_DROPCOUNTITEM:
                            // 墨款飘 酒捞袍
                            if (msg.lParam2 > 0)
                            {
                                if (this.UserDropCountItem(msg.description, msg.lParam1, msg.lParam2))
                                {
                                    SendDefMessage(Grobal2.SM_DROPITEM_SUCCESS, msg.lParam1, 0, 0, 0, msg.description);
                                }
                            }
                            break;
                        case Grobal2.CM_PICKUP:
                            if ((this.CX == msg.lParam2) && (this.CY == msg.lParam3))
                            {
                                this.PickUp();
                            }
                            break;
                        case Grobal2.CM_QUERYUSERNAME:
                            GetQueryUserName(msg.lParam1 as TCreature, msg.lParam2, msg.lParam3);
                            break;
                        case Grobal2.CM_QUERYBAGITEMS:
                            SendBagItems();
                            break;
                        case Grobal2.CM_OPENDOOR:
                            ServerGetOpenDoor(msg.lParam2, msg.lParam3);
                            break;
                        case Grobal2.CM_TAKEONITEM:
                            ServerGetTakeOnItem((byte)msg.lParam2, msg.lParam1, msg.description);
                            break;
                        case Grobal2.CM_TAKEOFFITEM:
                            ServerGetTakeOffItem((byte)msg.lParam2, msg.lParam1, msg.description);
                            break;
                        case Grobal2.CM_EXCHGTAKEONITEM:
                            break;
                        case Grobal2.CM_EAT:
                            ServerGetEatItem(msg.lParam1, msg.description);
                            break;
                        case Grobal2.CM_BUTCH:
                            ServerGetButch(msg.lParam1 as TCreature, msg.lParam2, msg.lParam3, msg.wParam);
                            break;
                        case Grobal2.CM_MAGICKEYCHANGE:
                            ServerGetMagicKeyChange(msg.lParam1, msg.lParam2);
                            break;
                        case Grobal2.CM_SOFTCLOSE:
                            SoftClosed = true;
                            UserSocketClosed = true;
                            break;
                        case Grobal2.CM_CANCLOSE:
                            if (this.ExistAttackSlaves())
                            {
                                this.SendMsg(this, Grobal2.RM_CANCLOSE_FAIL, 0, 0, 0, 0, "");
                            }
                            else
                            {
                                this.SendMsg(this, Grobal2.RM_CANCLOSE_OK, 0, 0, 0, 0, "");
                            }
                            break;
                        case Grobal2.CM_CLICKNPC:
                            // NPC,惑牢阑 努腐窃.
                            ServerGetClickNpc(msg.lParam1);
                            break;
                        case Grobal2.CM_MERCHANTDLGSELECT:
                            ServerGetMerchantDlgSelect(msg.lParam1, msg.description);
                            break;
                        case Grobal2.CM_MERCHANTQUERYSELLPRICE:
                            ServerGetMerchantQuerySellPrice(msg.lParam1, HUtil32.MakeLong(msg.lParam2, msg.lParam3), msg.description);
                            break;
                        case Grobal2.CM_MERCHANTQUERYREPAIRCOST:
                            ServerGetMerchantQueryRepairPrice(msg.lParam1, HUtil32.MakeLong(msg.lParam2, msg.lParam3), msg.description);
                            break;
                        case Grobal2.CM_USERSELLITEM:
                            // 墨款飘 酒捞袍
                            ServerGetUserSellItem(msg.lParam1, HUtil32.MakeLong(msg.lParam2, msg.lParam3), msg.wParam, msg.description);
                            break;
                        case Grobal2.CM_USERREPAIRITEM:
                            ServerGetUserRepairItem(msg.lParam1, HUtil32.MakeLong(msg.lParam2, msg.lParam3), msg.description);
                            break;
                        case Grobal2.CM_USERSTORAGEITEM:
                            ServerGetUserStorageItem(msg.lParam1, HUtil32.MakeLong(msg.lParam2, msg.lParam3), Math.Abs(msg.wParam), msg.description);
                            break;
                        case Grobal2.CM_USERGETDETAILITEM:
                            ServerGetUserMenuBuy(msg.Ident, msg.lParam1, 0, msg.lParam2, msg.description);
                            break;
                        case Grobal2.CM_USERBUYITEM:
                            ServerGetUserMenuBuy(msg.Ident, msg.lParam1, HUtil32.MakeLong(msg.lParam2, msg.lParam3), msg.wParam, msg.description);
                            break;
                        case Grobal2.CM_DROPGOLD:
                            if (msg.lParam1 > 0)
                            {
                                this.UserDropGold(msg.lParam1);
                            }
                            break;
                        case Grobal2.CM_TEST:
                            SendDefMessage(Grobal2.SM_TEST, 0, 0, 0, 0, "");
                            break;
                        case Grobal2.CM_GROUPMODE:
                            if (msg.lParam2 == 0)
                            {
                                this.DenyGroup();
                            }
                            else
                            {
                                this.AllowGroup = true;
                            }
                            if (this.AllowGroup)
                            {
                                SendDefMessage(Grobal2.SM_GROUPMODECHANGED, 0, 1, 0, 0, "");
                            }
                            else
                            {
                                SendDefMessage(Grobal2.SM_GROUPMODECHANGED, 0, 0, 0, 0, "");
                            }
                            break;
                        case Grobal2.CM_UPGRADEITEM:
                            try
                            {
                                strupgrade = HUtil32.GetValidStr3(msg.description, ref msg.description, new string[] { "/" });
                                CmdUpgradeItem(msg.description, strupgrade, msg.lParam1, HUtil32.MakeLong(msg.lParam2, msg.lParam3), 0);
                            }
                            catch
                            {
                                svMain.MainOutMessage("UPGRADE ERROR");
                            }
                            break;
                        case Grobal2.CM_CREATEGROUP:
                            ServerGetCreateGroup(msg.description.Trim());
                            break;
                        case Grobal2.CM_CREATEGROUPREQ_OK:
                            ServerGetCreateGroupRequestOk(msg.description.Trim());
                            break;
                        case Grobal2.CM_CREATEGROUPREQ_FAIL:
                            ServerGetCreateGroupRequestFail();
                            break;
                        case Grobal2.CM_ADDGROUPMEMBER:
                            ServerGetAddGroupMember(msg.description.Trim());
                            break;
                        case Grobal2.CM_ADDGROUPMEMBERREQ_OK:
                            ServerGetAddGroupMemberRequestOk(msg.description.Trim());
                            break;
                        case Grobal2.CM_ADDGROUPMEMBERREQ_FAIL:
                            ServerGetAddGroupMemberRequestFail();
                            break;
                        case Grobal2.CM_DELGROUPMEMBER:
                            ServerGetDelGroupMember(msg.description.Trim());
                            break;
                        case Grobal2.RM_GUILDAGITDEALTRY:
                        case Grobal2.CM_DEALTRY:
                            // 厘盔芭贰矫档(sonmg)
                            ServerGetDealTry(msg.description.Trim());
                            break;
                        case Grobal2.CM_DEALADDITEM:
                            ServerGetDealAddItem(msg.lParam1, msg.wParam, msg.description);
                            break;
                        case Grobal2.CM_DEALDELITEM:
                            ServerGetDealDelItem(msg.lParam1, msg.description);
                            break;
                        case Grobal2.CM_DEALCANCEL:
                            ServerGetDealCancel();
                            break;
                        case Grobal2.CM_DEALCHGGOLD:
                            ServerGetDealChangeGold(msg.lParam1);
                            break;
                        case Grobal2.CM_DEALEND:
                            ServerGetDealEnd();
                            break;
                        case Grobal2.CM_USERTAKEBACKSTORAGEITEM:
                            ServerGetTakeBackStorageItem(msg.lParam1, HUtil32.MakeLong(msg.lParam2, msg.lParam3), msg.wParam, msg.description);
                            break;
                        case Grobal2.CM_WANTMINIMAP:
                            ServerGetWantMiniMap();
                            break;
                        case Grobal2.CM_USERMAKEDRUGITEM:
                            ServerGetMakeDrug(msg.lParam1, msg.description);
                            break;
                        case Grobal2.CM_USERMAKEITEMSEL:
                            // 酒捞袍 力炼
                            ServerGetMakeItemSel(msg.lParam1, msg.description);
                            break;
                        case Grobal2.CM_USERMAKEITEM:
                            ServerGetMakeItem(msg.lParam1, msg.description);
                            break;
                        case Grobal2.CM_ITEMSUMCOUNT:
                            // 墨款飘 酒捞袍 烹钦.
                            ServerGetSumCountItem(msg.lParam1, HUtil32.MakeLong(msg.lParam2, msg.lParam3), msg.description);
                            break;
                        case Grobal2.CM_QUERYUSERSTATE:
                            // cret
                            // x
                            // y
                            ServerGetQueryUserState(msg.lParam1 as TCreature, msg.lParam2, msg.lParam3);
                            break;
                        case Grobal2.CM_OPENGUILDDLG:
                            ServerGetOpenGuildDlg();
                            break;
                        case Grobal2.CM_GUILDHOME:
                            ServerGetGuildHome();
                            break;
                        case Grobal2.CM_GUILDMEMBERLIST:
                            ServerGetGuildMemberList();
                            break;
                        case Grobal2.CM_GUILDADDMEMBER:
                            ServerGetGuildAddMember(msg.description);
                            break;
                        case Grobal2.CM_GUILDDELMEMBER:
                            ServerGetGuildDelMember(msg.description);
                            break;
                        case Grobal2.CM_GUILDUPDATENOTICE:
                            ServerGetGuildUpdateNotice(msg.description);
                            break;
                        case Grobal2.CM_GUILDUPDATERANKINFO:
                            ServerGetGuildUpdateRanks(msg.description);
                            break;
                        case Grobal2.CM_GUILDMAKEALLY:
                            ServerGetGuildMakeAlly();
                            break;
                        case Grobal2.CM_GUILDBREAKALLY:
                            // 惑措祈 巩林客 付林焊绊
                            ServerGetGuildBreakAlly(msg.description);
                            break;
                        case Grobal2.CM_SPEEDHACKUSER:
                            svMain.MainOutMessage("[Using hacking program(client)] <" + msg.lParam1.ToString() + "> " + this.UserName);
                            break;
                        case Grobal2.CM_ADJUST_BONUS:
                            // speedhack 蜡历 肺弊甫 巢变促.
                            ServerGetAdjustBonus(msg.lParam1, msg.description);
                            break;
                        case Grobal2.CM_FRIEND_ADD:
                            DefMsg = new TDefaultMessage();
                            DefMsg.Recog = (int)msg.sender;
                            DefMsg.Ident = msg.Ident;
                            DefMsg.Param = (ushort)msg.lParam1;
                            DefMsg.Tag = (ushort)msg.lParam2;
                            DefMsg.Series = (ushort)msg.lParam3;
                            svMain.UserMgrEngine.ExternSendMsg(TSendTarget.stInterServer, svMain.ServerIndex, GateIndex, UserGateIndex, UserHandle, this.UserName, DefMsg, msg.description);
                            break;
                        case Grobal2.CM_LM_REQUEST:
                            // 楷牢荤力
                            ServerGetRelationRequest(msg.lParam1, msg.lParam2);
                            break;
                        case Grobal2.CM_LM_OPTION:
                            ServerGetRelationOptionChange(msg.lParam1, msg.lParam2);
                            break;
                        case Grobal2.CM_LM_DELETE:
                            ServerGetRelationDelete(msg.lParam1, msg.description);
                            break;
                        case Grobal2.CM_LM_DELETE_REQ_OK:
                            ServerGetRelationDeleteRequestOk(msg.lParam1, msg.description);
                            break;
                        case Grobal2.CM_LM_DELETE_REQ_FAIL:
                            ServerGetRelationDeleteRequestFail(msg.lParam1, msg.description);
                            break;
                        case Grobal2.CM_MARKET_LIST:
                            // 困殴魄概 UserMarket
                            ServerGetMarketList(msg.lParam1 as TCreature, msg.lParam2, msg.description);
                            break;
                        case Grobal2.CM_MARKET_SELL:
                            ServerGetMarketSell(msg.lParam1 as TCreature, msg.wParam, HUtil32.MakeLong(msg.lParam2, msg.lParam3), msg.description);
                            break;
                        case Grobal2.CM_MARKET_BUY:
                            ServerGetMarketBuy(msg.lParam1 as TCreature, HUtil32.MakeLong(msg.lParam2, msg.lParam3));
                            break;
                        case Grobal2.CM_MARKET_CANCEL:
                            ServerGetMarketCancel(msg.lParam1 as TCreature, HUtil32.MakeLong(msg.lParam2, msg.lParam3));
                            break;
                        case Grobal2.CM_MARKET_GETPAY:
                            ServerGetMarketGetPay(msg.lParam1 as TCreature, HUtil32.MakeLong(msg.lParam2, msg.lParam3));
                            break;
                        case Grobal2.CM_MARKET_CLOSE:
                            ServerGetMarketClose();
                            break;
                        case Grobal2.CM_GUILDAGITLIST:
                            ServerGetGuildAgitList(msg.lParam1);
                            break;
                        case Grobal2.CM_GUILDAGIT_TAG_ADD:
                            if (ServerGetGuildAgitTag(msg.sender as TCreature, msg.description))
                            {
                                DefMsg = new TDefaultMessage();
                                DefMsg.Recog = (int)msg.sender;
                                DefMsg.Ident = Grobal2.CM_TAG_ADD_DOUBLE;
                                DefMsg.Param = (ushort)msg.lParam1;
                                DefMsg.Tag = (ushort)msg.lParam2;
                                DefMsg.Series = (ushort)msg.lParam3;
                                svMain.UserMgrEngine.ExternSendMsg(TSendTarget.stInterServer, svMain.ServerIndex, GateIndex, UserGateIndex, UserHandle, this.UserName, DefMsg, msg.description);
                            }
                            break;
                        case Grobal2.CM_GABOARD_LIST:
                            ServerGetGaBoardList(msg.lParam1);
                            break;
                        case Grobal2.CM_GABOARD_READ:
                            ServerGetGaBoardRead(msg.description);
                            break;
                        case Grobal2.CM_GABOARD_ADD:
                            ServerGetGaBoardAdd(msg.lParam1, msg.lParam2, msg.description);
                            break;
                        case Grobal2.CM_GABOARD_DEL:
                            ServerGetGaBoardDel(msg.lParam2, msg.description);
                            break;
                        case Grobal2.CM_GABOARD_EDIT:
                            ServerGetGaBoardEdit(msg.lParam2, msg.description);
                            break;
                        case Grobal2.CM_GABOARD_NOTICE_CHECK:
                            ServerGetGaBoardNoticeCheck();
                            break;
                        case Grobal2.CM_DECOITEM_BUY:
                            ServerGetDecoItemBuy(msg.Ident, msg.lParam1, HUtil32.MakeLong(msg.lParam2, msg.lParam3), msg.description);
                            break;
                        case Grobal2.CM_UPDATESTALLITEM:
                            // 摆摊
                            if (M2Share.g_GameConfig.boStallSystem)
                            {
                                ClientUpdateStallItem(msg.description, msg.wParam > 0);
                            }
                            break;
                        case Grobal2.CM_OPENSTALL:
                            if (M2Share.g_GameConfig.boStallSystem)
                            {
                                ClientStallOnOpening(msg.description, msg.wParam);
                            }
                            break;
                        case Grobal2.CM_GETSHOPITEM:
                            GetSaleItemListEx(msg);
                            break;
                        case Grobal2.CM_BUYSHOPITEM:
                            BuySaleItemListEx(msg);
                            break;
                        case Grobal2.CM_SHOPPRESEND:
                            PresendItem(msg);
                            break;
                        case Grobal2.RM_STALLSTATUS:
                            if (M2Share.g_GameConfig.boStallSystem)
                            {
                                StallInfo = new TStallInfo();
                                if ((msg.sender as TUserHuman) != this)
                                {
                                    Def = Grobal2.MakeDefaultMsg(Grobal2.SM_OPENSTALL, (int)msg.sender, msg.lParam1, msg.lParam2, msg.lParam3);
                                    if (msg.wParam > 0)
                                    {
                                        StallInfo.Open = (msg.sender as TUserHuman).StallMgr.OnSale;// 摆摊
                                        StallInfo.Looks = (msg.sender as TUserHuman).StallMgr.StallType;
                                        StallInfo.Name = (msg.sender as TUserHuman).StallMgr.mBlock.StallName;
                                    }
                                    else
                                    {
                                        StallInfo.Open = false;// 收摊
                                        StallInfo.Looks = (msg.sender as TUserHuman).StallMgr.StallType;
                                    }
                                    SendSocket(Def, EDcode.EncodeBuffer(StallInfo));
                                }
                            }
                            break;
                        case Grobal2.RM_LM_DBWANTLIST:
                            ServerSetRelationDBWantList(msg.description);
                            break;
                        case Grobal2.RM_LM_DBADD:
                            ServerSetRelationDBAdd(msg.description);
                            break;
                        case Grobal2.RM_LM_DBEDIT:
                            ServerSetRelationDBEdit(msg.description);
                            break;
                        case Grobal2.RM_LM_DBDELETE:
                            ServerSetRelationDBDelete(msg.description);
                            break;
                        case Grobal2.RM_LM_DBGETLIST:
                            ServerGetRelationDBGetList(msg.description);
                            break;
                        case Grobal2.RM_LM_LOGOUT:
                            ServerGetLoverLogout();
                            break;
                        case Grobal2.RM_MA_DBADD:
                            ServerSetMasterDBAdd(msg.description);
                            break;
                        case Grobal2.RM_MA_DBEDIT:
                            ServerSetMasterDBEdit(msg.description);
                            break;
                        case Grobal2.RM_MA_DBDELETE:
                            ServerSetMasterDBDelete(msg.description);
                            break;
                        case Grobal2.RM_LM_DBMATLIST:
                            ServerSetMasterDBWantList(msg.description);
                            break;
                        case Grobal2.RM_LM_REFMATLIST:
                            CheckMaster();
                            this.UserNameChanged();
                            break;
                        case Grobal2.RM_MAKE_SLAVE:
                            // -------------------------------------------------------------
                            // 辑滚俊辑 辑滚肺 焊郴绰 皋技瘤, 瘤楷 贸府 版快
                            if (msg.lParam1 != 0)
                            {
                                RmMakeSlaveProc(msg.lParam1 as TSlaveInfo);
                                Dispose(msg.lParam1 as TSlaveInfo);
                            }
                            break;
                        case Grobal2.RM_TAG_ALARM:
                            // -------------------------------------------------------------
                            // 辑滚俊辑 焊郴绰 皋技瘤 贸府
                            // 率瘤 吭澜 舅覆
                            SendDefMessage(Grobal2.SM_TAG_ALARM, 0, msg.lParam1, 0, 0, "");
                            break;
                        case Grobal2.RM_LOGON:
                            if (this.PEnvir.Darkness)
                            {
                                // 1
                                n = ObjBase.BRIGHT_NIGHT;
                            }
                            else if (this.PEnvir.Dawn)
                            {
                                // 2  //货寒眠啊
                                n = ObjBase.BRIGHT_DAWN;
                            }
                            else
                            {
                                switch (Bright)
                                {
                                    case 1:
                                        n = ObjBase.BRIGHT_DAY;
                                        break;
                                    case 3:
                                        // 0;  //撤
                                        n = ObjBase.BRIGHT_NIGHT;
                                        break;
                                    default:
                                        // 1;  //广
                                        n = ObjBase.BRIGHT_DAWN;
                                        break;
                                        // 2;  //货寒,历翅
                                }
                            }

                            if (this.PEnvir.DayLight)
                            {
                                n = 0;
                            }
                            if ((n > 256) || (this.PEnvir.AutoAttack > 256))
                            {
                                svMain.MainOutMessage("[Caution!] Over size of BYTE in TUserHuman.Operate(RM_LOGON)");
                            }
                            Def = Grobal2.MakeDefaultMsg(Grobal2.SM_NEWMAP, this.ActorId, this.CX, this.CY, MakeWord(Lobyte((ushort)n), Lobyte((ushort)this.PEnvir.AutoAttack)));
                            SendSocket(Def, EDcode.EncodeString(this.PEnvir.GetGuildAgitRealMapName()));
                            SendLogon();
                            GetQueryUserName(this, this.CX, this.CY);
                            SendAreaState();// 发送游戏设置
                            this.SendGameConfig();
                            SendDefMessage(Grobal2.SM_MAPDESCRIPTION, 0, 0, 0, 0, this.PEnvir.MapTitle);
                            Def = Grobal2.MakeDefaultMsg(Grobal2.SM_CHECK_CLIENTVALID, svMain.ClientCheckSumValue1, HUtil32.LoWord(svMain.ClientCheckSumValue2), HUtil32.HiWord(svMain.ClientCheckSumValue2), 0);
                            smsg.Ident = HUtil32.LoWord(svMain.ClientCheckSumValue3);
                            smsg.msg = HUtil32.HiWord(svMain.ClientCheckSumValue3);
                            SendSocket(Def, EDcode.EncodeBuffer(smsg));
                            break;
                        case Grobal2.RM_CHANGEMAP:
                            if (this.PEnvir.NoGroup)
                            {
                                try
                                {
                                    // 郴啊 弊缝炉捞 酒聪搁...
                                    if (this.GroupOwner != null)
                                    {
                                        this.GroupOwner.DelGroupMember(this);
                                    }
                                    else
                                    {
                                        // 郴啊 弊缝 炉捞搁...
                                        this.DelGroupMember(this);
                                    }
                                }
                                catch
                                {
                                }
                            }
                            if (this.PEnvir.Darkness)
                            {
                                // 1
                                n = ObjBase.BRIGHT_NIGHT;
                            }
                            else if (this.PEnvir.Dawn)
                            {
                                // 2  //货寒眠啊
                                n = ObjBase.BRIGHT_DAWN;
                            }
                            else
                            {
                                switch (Bright)
                                {
                                    case 1:
                                        n = ObjBase.BRIGHT_DAY;
                                        break;
                                    case 3:
                                        // 0;  //撤
                                        n = ObjBase.BRIGHT_NIGHT;
                                        break;
                                    default:
                                        // 1;  //广
                                        n = ObjBase.BRIGHT_DAWN;
                                        break;
                                        // 2;  //货寒,历翅
                                }
                            }

                            if (this.PEnvir.DayLight)
                            {
                                n = 0;
                            }
                            // 俊矾 钎矫
                            if ((n > 256) || (this.PEnvir.AutoAttack > 256))
                            {
                                svMain.MainOutMessage("[Caution!] Over size of BYTE in TUserHuman.Operate(RM_CHANGEMAP)");
                            }
                            SendDefMessage(Grobal2.SM_CHANGEMAP, this.ActorId, this.CX, this.CY, MakeWord(Lobyte((ushort)n), Lobyte((ushort)this.PEnvir.AutoAttack)), msg.description);
                            // 瘤开 惑怕 钎矫
                            SendAreaState();
                            SendDefMessage(Grobal2.SM_MAPDESCRIPTION, 0, 0, 0, 0, this.PEnvir.MapTitle);
                            break;
                        case Grobal2.RM_DAYCHANGING:
                            if (this.PEnvir.Darkness)
                            {
                                // 1
                                n = ObjBase.BRIGHT_NIGHT;
                            }
                            else if (this.PEnvir.Dawn)
                            {
                                // 2  //货寒眠啊
                                n = ObjBase.BRIGHT_DAWN;
                            }
                            else
                            {
                                switch (Bright)
                                {
                                    case 1:
                                        n = ObjBase.BRIGHT_DAY;
                                        break;
                                    case 3:
                                        // 0;  //撤
                                        n = ObjBase.BRIGHT_NIGHT;
                                        break;
                                    default:
                                        // 1;  //广
                                        n = ObjBase.BRIGHT_DAWN;
                                        break;
                                        // 2;  //货寒,历翅
                                }
                            }

                            if (this.PEnvir.DayLight)
                            {
                                n = 0;
                            }
                            Def = Grobal2.MakeDefaultMsg(Grobal2.SM_DAYCHANGING, 0, Bright, n, 0);
                            SendSocket(Def, "");
                            break;
                        case Grobal2.RM_ABILITY:
                            Def = Grobal2.MakeDefaultMsg(Grobal2.SM_ABILITY, this.Gold, this.Job, HUtil32.LoWord(this.GameGold), HUtil32.HiWord(this.GameGold));
                            SendSocket(Def, EDcode.EncodeBuffer(this.WAbil));
                            break;
                        case Grobal2.RM_SUBABILITY:
                            SendDefMessage(Grobal2.SM_SUBABILITY, HUtil32.MakeLong(MakeWord(this.AntiMagic, 0), 0), MakeWord(this.AccuracyPoint, this.SpeedPoint), MakeWord(this.AntiPoison, this.PoisonRecover), MakeWord(this.HealthRecover, this.SpellRecover), "");
                            break;
                        case Grobal2.RM_MYSTATUS:
                            SendDefMessage(Grobal2.SM_MYSTATUS, 0, this.GetHungryState(), 0, 0, "");
                            break;
                        case Grobal2.RM_ADJUST_BONUS:
                            // 硅绊悄 殿..
                            ServerSendAdjustBonus();
                            break;
                        case Grobal2.RM_HEALTHSPELLCHANGED:
                            Def = Grobal2.MakeDefaultMsg(Grobal2.SM_HEALTHSPELLCHANGED, (int)msg.sender, (msg.sender as TCreature).WAbil.HP, (msg.sender as TCreature).WAbil.MP, (msg.sender as TCreature).WAbil.MaxHP);
                            SendSocket(Def, "");
                            break;
                        case Grobal2.RM_MOVEFAIL:
                            Def = Grobal2.MakeDefaultMsg(Grobal2.SM_MOVEFAIL, this.ActorId, this.CX, this.CY, this.Dir);
                            cdesc.Feature = this.Feature();
                            cdesc.Status = this.CharStatus;
                            SendSocket(Def, EDcode.EncodeBuffer(cdesc));
                            break;
                        case Grobal2.RM_TURN:
                        case Grobal2.RM_PUSH:
                        case Grobal2.RM_RUSH:
                        case Grobal2.RM_RUSHKUNG:
                            if ((msg.sender != this) || (msg.Ident == Grobal2.RM_PUSH) || (msg.Ident == Grobal2.RM_RUSH) || (msg.Ident == Grobal2.RM_RUSHKUNG))
                            {
                                switch (msg.Ident)
                                {
                                    case Grobal2.RM_PUSH:
                                        // msg.wParam : 规氢
                                        // x
                                        // y
                                        // dir
                                        Def = Grobal2.MakeDefaultMsg(Grobal2.SM_BACKSTEP, (int)msg.sender, msg.lParam1, msg.lParam2, MakeWord(msg.wParam, (msg.sender as TCreature).Light));
                                        break;
                                    case Grobal2.RM_RUSH:
                                        // x
                                        // y
                                        // dir
                                        Def = Grobal2.MakeDefaultMsg(Grobal2.SM_RUSH, (int)msg.sender, msg.lParam1, msg.lParam2, MakeWord(msg.wParam, (msg.sender as TCreature).Light));
                                        break;
                                    case Grobal2.RM_RUSHKUNG:
                                        // x
                                        // y
                                        // dir
                                        Def = Grobal2.MakeDefaultMsg(Grobal2.SM_RUSHKUNG, (int)msg.sender, msg.lParam1, msg.lParam2, MakeWord(msg.wParam, (msg.sender as TCreature).Light));
                                        break;
                                    default:
                                        // x
                                        // y
                                        // dir
                                        Def = Grobal2.MakeDefaultMsg(Grobal2.SM_TURN, (int)msg.sender, msg.lParam1, msg.lParam2, MakeWord(msg.wParam, (msg.sender as TCreature).Light));
                                        break;
                                }
                                cdesc.Feature = (msg.sender as TCreature).GetRelFeature(this);
                                cdesc.Status = (msg.sender as TCreature).CharStatus;
                                str = EDcode.EncodeBuffer(cdesc);
                                n = this.GetThisCharColor(msg.sender as TCreature);
                                if (msg.description != "")
                                {
                                    // 某腐 捞抚
                                    // 捞抚祸彬
                                    str = str + EDcode.EncodeString(msg.description + "/" + n.ToString());
                                }
                                SendSocket(Def, str);
                                if (msg.Ident == Grobal2.RM_TURN)
                                {
                                    if ((msg.sender as TCreature).RaceServer == Grobal2.RC_USERHUMAN)
                                    {
                                        if ((msg.sender as TUserHuman).StallMgr.OnSale)
                                        {
                                            this.SendMsg(msg.sender as TCreature, Grobal2.RM_STALLSTATUS, (ushort)(msg.sender as TUserHuman).StallMgr.mBlock.ItemCount, (msg.sender as TCreature).CX, (msg.sender as TCreature).CY, (msg.sender as TCreature).Dir, "");
                                        }
                                        SendDefMessage(Grobal2.SM_REFICONINFO, (int)msg.sender, 0, 0, 0, EDcode.EncodeBuffer((msg.sender as TUserHuman).m_IconInfo[0], sizeof(Grobal2.TIconInfo)));
                                    }
                                }
                            }
                            break;
                        case Grobal2.RM_FOXSTATE:
                            if (msg.sender != this)
                            {
                                // x
                                // y
                                // dir
                                // bodystate
                                Def = Grobal2.MakeDefaultMsg(Grobal2.SM_FOXSTATE, (int)msg.sender, msg.lParam1, msg.lParam2, MakeWord(msg.wParam, msg.lParam3));
                                cdesc.Feature = (msg.sender as TCreature).GetRelFeature(this);
                                cdesc.Status = (msg.sender as TCreature).CharStatus;
                                str = EDcode.EncodeBuffer(cdesc);
                                n = this.GetThisCharColor(msg.sender as TCreature);
                                if (msg.description != "")
                                {
                                    // 某腐 捞抚
                                    // 捞抚祸彬
                                    str = str + EDcode.EncodeString(msg.description + "/" + n.ToString());
                                }
                                SendSocket(Def, str);
                            }
                            break;
                        case Grobal2.RM_WALK:
                            if (msg.sender != this)
                            {
                                // x
                                // y
                                // dir
                                Def = Grobal2.MakeDefaultMsg(Grobal2.SM_WALK, (int)msg.sender, msg.lParam1, msg.lParam2, MakeWord(msg.wParam, (msg.sender as TCreature).Light));
                                cdesc.Feature = (msg.sender as TCreature).GetRelFeature(this);
                                cdesc.Status = (msg.sender as TCreature).CharStatus;
                                SendSocket(Def, EDcode.EncodeBuffer(cdesc));
                            }
                            break;
                        case Grobal2.RM_RUN:
                            if (msg.sender != this)
                            {
                                // x
                                // y
                                // dir
                                Def = Grobal2.MakeDefaultMsg(Grobal2.SM_RUN, (int)msg.sender, msg.lParam1, msg.lParam2, MakeWord(msg.wParam, (msg.sender as TCreature).Light));
                                cdesc.Feature = (msg.sender as TCreature).GetRelFeature(this);
                                cdesc.Status = (msg.sender as TCreature).CharStatus;
                                SendSocket(Def, EDcode.EncodeBuffer(cdesc));
                            }
                            break;
                        case Grobal2.RM_BUTCH:
                            if (msg.sender != this)
                            {
                                // x
                                // y
                                // Dir
                                Def = Grobal2.MakeDefaultMsg(Grobal2.SM_BUTCH, (int)msg.sender, msg.lParam1, msg.lParam2, msg.wParam);
                                SendSocket(Def, "");
                            }
                            break;
                        case Grobal2.RM_HIT:
                            if (msg.sender != this)
                            {
                                // x
                                // y
                                // Dir
                                Def = Grobal2.MakeDefaultMsg(Grobal2.SM_HIT, (int)msg.sender, msg.lParam1, msg.lParam2, msg.wParam);
                                SendSocket(Def, "");
                            }
                            break;
                        case Grobal2.RM_POWERHIT:
                            if (msg.sender != this)
                            {
                                // x
                                // y
                                // Dir
                                Def = Grobal2.MakeDefaultMsg(Grobal2.SM_POWERHIT, (int)msg.sender, msg.lParam1, msg.lParam2, msg.wParam);
                                SendSocket(Def, "");
                            }
                            break;
                        case Grobal2.RM_LONGHIT:
                            if (msg.sender != this)
                            {
                                // x
                                // y
                                // Dir
                                Def = Grobal2.MakeDefaultMsg(Grobal2.SM_LONGHIT, (int)msg.sender, msg.lParam1, msg.lParam2, msg.wParam);
                                SendSocket(Def, "");
                            }
                            break;
                        case Grobal2.RM_WIDEHIT:
                            if (msg.sender != this)
                            {
                                // x
                                // y
                                // Dir
                                Def = Grobal2.MakeDefaultMsg(Grobal2.SM_WIDEHIT, (int)msg.sender, msg.lParam1, msg.lParam2, msg.wParam);
                                SendSocket(Def, "");
                            }
                            break;
                        case Grobal2.RM_CROSSHIT:
                            if (msg.sender != this)
                            {
                                // x
                                // y
                                // Dir
                                Def = Grobal2.MakeDefaultMsg(Grobal2.SM_CROSSHIT, (int)msg.sender, msg.lParam1, msg.lParam2, msg.wParam);
                                SendSocket(Def, "");
                            }
                            break;
                        case Grobal2.RM_TWINHIT:
                            if (msg.sender != this)
                            {
                                // x
                                // y
                                // Dir
                                Def = Grobal2.MakeDefaultMsg(Grobal2.SM_TWINHIT, (int)msg.sender, msg.lParam1, msg.lParam2, msg.wParam);
                                SendSocket(Def, "");
                            }
                            break;
                        case Grobal2.RM_HEAVYHIT:
                            if (msg.sender != this)
                            {
                                // x
                                // y
                                // Dir
                                Def = Grobal2.MakeDefaultMsg(Grobal2.SM_HEAVYHIT, (int)msg.sender, msg.lParam1, msg.lParam2, msg.wParam);
                                SendSocket(Def, msg.description);
                            }
                            break;
                        case Grobal2.RM_BIGHIT:
                            if (msg.sender != this)
                            {
                                // x
                                // y
                                // Dir
                                Def = Grobal2.MakeDefaultMsg(Grobal2.SM_BIGHIT, (int)msg.sender, msg.lParam1, msg.lParam2, msg.wParam);
                                SendSocket(Def, "");
                            }
                            break;
                        case Grobal2.RM_FIREHIT:
                            if (msg.sender != this)
                            {
                                // x
                                // y
                                // Dir
                                Def = Grobal2.MakeDefaultMsg(Grobal2.SM_FIREHIT, (int)msg.sender, msg.lParam1, msg.lParam2, msg.wParam);
                                SendSocket(Def, "");
                            }
                            break;
                        case Grobal2.RM_WINDCUT:
                            // if msg.Sender <> self then begin
                            // x
                            // y
                            // Dir
                            Def = Grobal2.MakeDefaultMsg(Grobal2.SM_WINDCUT, (int)msg.sender, msg.lParam1, msg.lParam2, msg.wParam);
                            SendSocket(Def, "");
                            break;
                        case Grobal2.RM_PULLMON:
                            // end;
                            // 脚痹公傍(2004/06/23)
                            if (msg.sender != this)
                            {
                                // x
                                // y
                                // Dir
                                Def = Grobal2.MakeDefaultMsg(Grobal2.SM_PULLMON, (int)msg.sender, msg.lParam1, msg.lParam2, msg.wParam);
                                SendSocket(Def, "");
                            }
                            break;
                        case Grobal2.RM_SUCKBLOOD:
                            if (msg.sender != this)
                            {
                                // x
                                // y
                                // Dir
                                Def = Grobal2.MakeDefaultMsg(Grobal2.SM_SUCKBLOOD, (int)msg.sender, msg.lParam1, msg.lParam2, msg.wParam);
                                SendSocket(Def, "");
                            }
                            break;
                        case Grobal2.RM_SPELL:
                            if (msg.sender != this)
                            {
                                Def = Grobal2.MakeDefaultMsg(Grobal2.SM_SPELL, (int)msg.sender, msg.lParam1, msg.lParam2, msg.wParam);
                                SendSocket(Def, msg.lParam3.ToString());
                            }
                            break;
                        case Grobal2.RM_MAGICFIRE:
                            Def = Grobal2.MakeDefaultMsg(Grobal2.SM_MAGICFIRE, (int)msg.sender, HUtil32.LoWord(msg.lParam2), HUtil32.HiWord(msg.lParam2), msg.lParam1);
                            SendSocket(Def, EDcode.EncodeBuffer(msg.lParam3, sizeof(int)));
                            break;
                        case Grobal2.RM_MAGICFIRE_FAIL:
                            SendDefMessage(Grobal2.SM_MAGICFIRE_FAIL, (int)msg.sender, 0, 0, 0, "");
                            break;
                        case Grobal2.RM_STRUCK:
                        case Grobal2.RM_STRUCK_MAG:
                            if (msg.wParam > 0)
                            {
                                if (msg.sender == this)
                                {
                                    if ((msg.lParam3 as TCreature) != null)
                                    {
                                        if ((msg.lParam3 as TCreature).RaceServer == Grobal2.RC_USERHUMAN)
                                        {
                                            this.AddPkHiter(msg.lParam3 as TCreature);
                                        }
                                        this.SetLastHiter(msg.lParam3 as TCreature);
                                    }
                                    if (this.PKLevel() >= 2)
                                    {
                                        HumStruckTime  =  HUtil32.GetTickCount();
                                    }
                                    if (svMain.UserCastle.IsOurCastle((TGuild)this.MyGuild))
                                    {
                                        if (msg.lParam3 != 0)
                                        {
                                            (msg.lParam3 as TCreature).BoCrimeforCastle = true;
                                            (msg.lParam3 as TCreature).CrimeforCastleTime  =  HUtil32.GetTickCount();
                                        }
                                    }
                                    this.HealthTick = 0;
                                    this.SpellTick = 0;
                                    this.PerHealth -= 1;
                                    this.PerSpell -= 1;
                                }
                                if (msg.sender != null)
                                {
                                    Def = Grobal2.MakeDefaultMsg(Grobal2.SM_STRUCK, (int)msg.sender, (msg.sender as TCreature).WAbil.HP, (msg.sender as TCreature).WAbil.MaxHP, msg.wParam);
                                    wl.lParam1 = (msg.sender as TCreature).GetRelFeature(this);
                                    wl.lParam2 = (msg.sender as TCreature).CharStatus;
                                    wl.lTag1 = msg.lParam3;
                                    // 锭赴仇
                                }
                                if (msg.Ident == Grobal2.RM_STRUCK_MAG)
                                {
                                    // 付过栏肺 嘎绰 荤款靛 瓤苞
                                    wl.lTag2 = 1;
                                }
                                else
                                {
                                    wl.lTag2 = 0;
                                }
                                SendSocket(Def, EDcode.EncodeBuffer(wl));
                            }
                            break;
                        case Grobal2.RM_DEATH:
                            if ((msg.sender as TCreature).RaceServer != Grobal2.RC_CLONE)
                            {
                                if (msg.lParam3 == 1)
                                {
                                    // x
                                    // y
                                    // Dir
                                    Def = Grobal2.MakeDefaultMsg(Grobal2.SM_NOWDEATH, (int)msg.sender, msg.lParam1, msg.lParam2, msg.wParam);
                                }
                                else
                                {
                                    // x
                                    // y
                                    // Dir
                                    Def = Grobal2.MakeDefaultMsg(Grobal2.SM_DEATH, (int)msg.sender, msg.lParam1, msg.lParam2, msg.wParam);
                                }
                                cdesc.Feature = (msg.sender as TCreature).GetRelFeature(this);
                                cdesc.Status = (msg.sender as TCreature).CharStatus;
                                SendSocket(Def, EDcode.EncodeBuffer(cdesc));
                            }
                            break;
                        case Grobal2.RM_SKELETON:
                            // x
                            // y
                            // Dir
                            Def = Grobal2.MakeDefaultMsg(Grobal2.SM_SKELETON, (int)msg.sender, msg.lParam1, msg.lParam2, msg.wParam);
                            cdesc.Feature = (msg.sender as TCreature).GetRelFeature(this);
                            cdesc.Status = (msg.sender as TCreature).CharStatus;
                            SendSocket(Def, EDcode.EncodeBuffer(cdesc));
                            break;
                        case Grobal2.RM_ALIVE:
                            // x
                            // y
                            // Dir
                            Def = Grobal2.MakeDefaultMsg(Grobal2.SM_ALIVE, (int)msg.sender, msg.lParam1, msg.lParam2, msg.wParam);
                            cdesc.Feature = (msg.sender as TCreature).GetRelFeature(this);
                            cdesc.Status = (msg.sender as TCreature).CharStatus;
                            SendSocket(Def, EDcode.EncodeBuffer(cdesc));
                            break;
                        case Grobal2.RM_CHANGEFACE:
                            // msg.lparam1 函脚傈
                            // msg.lparam2 函脚饶
                            if ((msg.lParam1 != 0) && (msg.lParam2 != 0))
                            {
                                Def = Grobal2.MakeDefaultMsg(Grobal2.SM_CHANGEFACE, msg.lParam1, HUtil32.LoWord(msg.lParam2), HUtil32.HiWord(msg.lParam2), 0);
                                cdesc.Feature = (msg.lParam2 as TCreature).GetRelFeature(this);
                                cdesc.Status = (msg.lParam2 as TCreature).CharStatus;
                                SendSocket(Def, EDcode.EncodeBuffer(cdesc));
                            }
                            break;
                        case Grobal2.RM_RECONNECT:
                            SoftClosed = true;
                            // 犁立阑 困秦辑 立加辆丰窃.
                            SendDefMessage(Grobal2.SM_RECONNECT, 0, 0, 0, 0, msg.description);
                            break;
                        case Grobal2.RM_SPACEMOVE_SHOW:
                        case Grobal2.RM_SPACEMOVE_SHOW_NO:
                        case Grobal2.RM_SPACEMOVE_SHOW2:
                            // msg.wParam : 规氢
                            if (msg.Ident == Grobal2.RM_SPACEMOVE_SHOW)
                            {
                                // x
                                // y
                                // dir
                                Def = Grobal2.MakeDefaultMsg(Grobal2.SM_SPACEMOVE_SHOW, (int)msg.sender, msg.lParam1, msg.lParam2, MakeWord(msg.wParam, (msg.sender as TCreature).Light));
                            }
                            else if (msg.Ident == Grobal2.RM_SPACEMOVE_SHOW2)
                            {
                                // x
                                // y
                                // dir
                                Def = Grobal2.MakeDefaultMsg(Grobal2.SM_SPACEMOVE_SHOW2, (int)msg.sender, msg.lParam1, msg.lParam2, MakeWord(msg.wParam, (msg.sender as TCreature).Light));
                            }
                            else
                            {
                                // x
                                // y
                                // dir
                                Def = Grobal2.MakeDefaultMsg(Grobal2.SM_SPACEMOVE_SHOW_NO, (int)msg.sender, msg.lParam1, msg.lParam2, MakeWord(msg.wParam, (msg.sender as TCreature).Light));
                            }
                            cdesc.Feature = (msg.sender as TCreature).GetRelFeature(this);
                            cdesc.Status = (msg.sender as TCreature).CharStatus;
                            str = EDcode.EncodeBuffer(cdesc);
                            n = this.GetThisCharColor(msg.sender as TCreature);
                            if (msg.description != "")
                            {
                                str = str + EDcode.EncodeString(msg.description + "/" + n.ToString());
                            }
                            // 某腐 捞抚 + 捞抚祸彬
                            SendSocket(Def, str);
                            break;
                        case Grobal2.RM_SPACEMOVE_HIDE:
                        case Grobal2.RM_SPACEMOVE_HIDE2:
                            if (msg.Ident == Grobal2.RM_SPACEMOVE_HIDE)
                            {
                                Def = Grobal2.MakeDefaultMsg(Grobal2.SM_SPACEMOVE_HIDE, (int)msg.sender, 0, 0, 0);
                            }
                            else
                            {
                                Def = Grobal2.MakeDefaultMsg(Grobal2.SM_SPACEMOVE_HIDE2, (int)msg.sender, 0, 0, 0);
                            }
                            SendSocket(Def, "");
                            break;
                        case Grobal2.RM_DISAPPEAR:
                            Def = Grobal2.MakeDefaultMsg(Grobal2.SM_DISAPPEAR, (int)msg.sender, 0, 0, 0);
                            SendSocket(Def, "");
                            break;
                        case Grobal2.RM_DIGUP:
                            // dir
                            Def = Grobal2.MakeDefaultMsg(Grobal2.SM_DIGUP, (int)msg.sender, msg.lParam1, msg.lParam2, MakeWord(msg.wParam, (msg.sender as TCreature).Light));
                            wl.lParam1 = (msg.sender as TCreature).GetRelFeature(this);
                            wl.lParam2 = (msg.sender as TCreature).CharStatus;
                            wl.lTag1 = msg.lParam3;
                            // 捞亥飘
                            wl.lTag1 = 0;
                            str = EDcode.EncodeBuffer(wl);
                            SendSocket(Def, str);
                            break;
                        case Grobal2.RM_DIGDOWN:
                            Def = Grobal2.MakeDefaultMsg(Grobal2.SM_DIGDOWN, (int)msg.sender, msg.lParam1, msg.lParam2, msg.wParam);
                            SendSocket(Def, "");
                            break;
                        case Grobal2.RM_SHOWEVENT:
                            smsg.Ident = HUtil32.HiWord(msg.lParam2);
                            // EventParam
                            smsg.msg = 0;
                            Def = Grobal2.MakeDefaultMsg(Grobal2.SM_SHOWEVENT, msg.lParam1, msg.wParam, HUtil32.LoWord(msg.lParam2), msg.lParam3);
                            str = EDcode.EncodeBuffer(smsg);
                            SendSocket(Def, str);
                            break;
                        case Grobal2.RM_HIDEEVENT:
                            SendDefMessage(Grobal2.SM_HIDEEVENT, msg.lParam1, msg.wParam, msg.lParam2, msg.lParam3, "");
                            break;
                        case Grobal2.RM_FLYAXE:
                            if (msg.lParam3 != 0)
                            {
                                mbw.Param1 = (msg.lParam3 as TCreature).CX;
                                mbw.Param2 = (msg.lParam3 as TCreature).CY;
                                mbw.Tag1 = HUtil32.LoWord(msg.lParam3);
                                mbw.Tag2 = HUtil32.HiWord(msg.lParam3);
                                // Dir
                                Def = Grobal2.MakeDefaultMsg(Grobal2.SM_FLYAXE, (int)msg.sender, msg.lParam1, msg.lParam2, msg.wParam);
                                str = EDcode.EncodeBuffer(mbw, sizeof(TMessageBodyW));
                                SendSocket(Def, str);
                            }
                            break;
                        case Grobal2.RM_LIGHTING:
                            if (msg.lParam3 != 0)
                            {
                                wl.lParam1 = (msg.lParam3 as TCreature).CX;
                                wl.lParam2 = (msg.lParam3 as TCreature).CY;
                            }
                            wl.lTag1 = msg.lParam3;
                            wl.lTag2 = msg.wParam;
                            // 付过 锅龋
                            Def = Grobal2.MakeDefaultMsg(Grobal2.SM_LIGHTING, (int)msg.sender, msg.lParam1, msg.lParam2, (msg.sender as TCreature).Dir);
                            str = EDcode.EncodeBuffer(wl);
                            SendSocket(Def, str);
                            break;
                        case Grobal2.RM_LIGHTING_1:
                            if (msg.lParam3 != 0)
                            {
                                wl.lParam1 = (msg.lParam3 as TCreature).CX;
                                wl.lParam2 = (msg.lParam3 as TCreature).CY;
                            }
                            wl.lTag1 = msg.lParam3;
                            wl.lTag2 = msg.wParam;
                            // 付过 锅龋
                            Def = Grobal2.MakeDefaultMsg(Grobal2.SM_LIGHTING_1, (int)msg.sender, msg.lParam1, msg.lParam2, (msg.sender as TCreature).Dir);
                            str = EDcode.EncodeBuffer(wl);
                            SendSocket(Def, str);
                            break;
                        case Grobal2.RM_LIGHTING_2:
                            if (msg.lParam3 != 0)
                            {
                                wl.lParam1 = (msg.lParam3 as TCreature).CX;
                                wl.lParam2 = (msg.lParam3 as TCreature).CY;
                            }
                            wl.lTag1 = msg.lParam3;
                            wl.lTag2 = msg.wParam;
                            // 付过 锅龋
                            Def = Grobal2.MakeDefaultMsg(Grobal2.SM_LIGHTING_2, (int)msg.sender, msg.lParam1, msg.lParam2, (msg.sender as TCreature).Dir);
                            str = EDcode.EncodeBuffer(wl);
                            SendSocket(Def, str);
                            break;
                        case Grobal2.RM_DRAGON_FIRE1:
                            if (msg.lParam3 != 0)
                            {
                                wl.lParam1 = (msg.lParam3 as TCreature).CX;
                                wl.lParam2 = (msg.lParam3 as TCreature).CY;
                            }
                            wl.lTag1 = msg.lParam3;
                            wl.lTag2 = msg.wParam;
                            // 付过 锅龋
                            Def = Grobal2.MakeDefaultMsg(Grobal2.SM_DRAGON_FIRE1, (int)msg.sender, msg.lParam1, msg.lParam2, (msg.sender as TCreature).Dir);
                            str = EDcode.EncodeBuffer(wl);
                            SendSocket(Def, str);
                            break;
                        case Grobal2.RM_DRAGON_FIRE2:
                            if (msg.lParam3 != 0)
                            {
                                wl.lParam1 = (msg.lParam3 as TCreature).CX;
                                wl.lParam2 = (msg.lParam3 as TCreature).CY;
                            }
                            wl.lTag1 = msg.lParam3;
                            wl.lTag2 = msg.wParam;
                            // 付过 锅龋
                            Def = Grobal2.MakeDefaultMsg(Grobal2.SM_DRAGON_FIRE2, (int)msg.sender, msg.lParam1, msg.lParam2, (msg.sender as TCreature).Dir);
                            str = EDcode.EncodeBuffer(wl);
                            SendSocket(Def, str);
                            break;
                        case Grobal2.RM_DRAGON_FIRE3:
                            if (msg.lParam3 != 0)
                            {
                                wl.lParam1 = (msg.lParam3 as TCreature).CX;
                                wl.lParam2 = (msg.lParam3 as TCreature).CY;
                            }
                            wl.lTag1 = msg.lParam3;
                            wl.lTag2 = msg.wParam;
                            // 付过 锅龋
                            Def = Grobal2.MakeDefaultMsg(Grobal2.SM_DRAGON_FIRE3, (int)msg.sender, msg.lParam1, msg.lParam2, (msg.sender as TCreature).Dir);
                            str = EDcode.EncodeBuffer(wl);
                            SendSocket(Def, str);
                            break;
                        case Grobal2.RM_NORMALEFFECT:
                            // recog
                            // xx
                            // yy
                            // 瓤苞 辆幅
                            SendDefMessage(Grobal2.SM_NORMALEFFECT, (int)msg.sender, msg.lParam1, msg.lParam2, msg.lParam3, "");
                            break;
                        case Grobal2.RM_LOOPNORMALEFFECT:
                            // recog
                            // 矫埃(檬)
                            // 荤侩救窃.
                            // 瓤苞 辆幅
                            SendDefMessage(Grobal2.SM_LOOPNORMALEFFECT, (int)msg.sender, msg.lParam1, msg.lParam2, msg.lParam3, "");
                            break;
                        case Grobal2.RM_OPENHEALTH:
                            SendDefMessage(Grobal2.SM_OPENHEALTH, (int)msg.sender, (msg.sender as TCreature).WAbil.HP, (msg.sender as TCreature).WAbil.MaxHP, 0, "");
                            break;
                        case Grobal2.RM_CLOSEHEALTH:
                            SendDefMessage(Grobal2.SM_CLOSEHEALTH, (int)msg.sender, 0, 0, 0, "");
                            break;
                        case Grobal2.RM_INSTANCEHEALGUAGE:
                            SendDefMessage(Grobal2.SM_INSTANCEHEALGUAGE, (int)msg.sender, (msg.sender as TCreature).WAbil.HP, (msg.sender as TCreature).WAbil.MaxHP, 0, "");
                            break;
                        case Grobal2.RM_BREAKWEAPON:
                            SendDefMessage(Grobal2.SM_BREAKWEAPON, (int)msg.sender, 0, 0, 0, "");
                            break;
                        case Grobal2.RM_GROUPPOS:
                            Def = Grobal2.MakeDefaultMsg(Grobal2.SM_GROUPPOS, (int)msg.sender, msg.lParam1, msg.lParam2, msg.lParam3);
                            SendSocket(Def, "");
                            break;
                        case Grobal2.RM_CHANGENAMECOLOR:
                            n = this.GetThisCharColor(msg.sender as TCreature);
                            SendDefMessage(Grobal2.SM_CHANGENAMECOLOR, (int)msg.sender, n, 0, 0, "");
                            break;
                        case Grobal2.RM_USERNAME:
                            Def = Grobal2.MakeDefaultMsg(Grobal2.SM_USERNAME, (int)msg.sender, this.GetThisCharColor(msg.sender as TCreature), 0, 0);
                            SendSocket(Def, EDcode.EncodeString(msg.description));
                            break;
                        case Grobal2.RM_WINEXP:
                            Def = Grobal2.MakeDefaultMsg(Grobal2.SM_WINEXP, this.Abil.Exp, msg.lParam1, 0, 0);
                            SendSocket(Def, "");
                            break;
                        case Grobal2.RM_LEVELUP:
                            Def = Grobal2.MakeDefaultMsg(Grobal2.SM_LEVELUP, this.Abil.Exp, this.Abil.Level, 0, 0);
                            SendSocket(Def, "");
                            Def = Grobal2.MakeDefaultMsg(Grobal2.SM_ABILITY, this.Gold, this.Job, HUtil32.LoWord(this.GameGold), HUtil32.HiWord(this.GameGold));
                            SendSocket(Def, EDcode.EncodeBuffer(this.WAbil));
                            SendDefMessage(Grobal2.SM_SUBABILITY, HUtil32.MakeLong(MakeWord(this.AntiMagic, 0), 0), MakeWord(this.AccuracyPoint, this.SpeedPoint), MakeWord(this.AntiPoison, this.PoisonRecover), MakeWord(this.HealthRecover, this.SpellRecover), "");
                            break;
                        case Grobal2.RM_HEAR:
                        case Grobal2.RM_CRY:
                        case Grobal2.RM_WHISPER:
                        case Grobal2.RM_GMWHISPER:
                        case Grobal2.RM_LM_WHISPER:
                        case Grobal2.RM_SYSMESSAGE:
                        case Grobal2.RM_SYSMESSAGE2:
                        case Grobal2.RM_SYSMESSAGE3:
                        case Grobal2.RM_SYSMSG_BLUE:
                        case Grobal2.RM_SYSMSG_PINK:
                        case Grobal2.RM_SYSMSG_GREEN:
                        case Grobal2.RM_SYSMSG_REMARK:
                        case Grobal2.RM_GROUPMESSAGE:
                        case Grobal2.RM_GUILDMESSAGE:
                        case Grobal2.RM_MERCHANTSAY:
                            switch (msg.Ident)
                            {
                                case Grobal2.RM_HEAR:
                                    Def = Grobal2.MakeDefaultMsg(Grobal2.SM_HEAR, (int)msg.sender, MakeWord(0, 255), 0, 1);
                                    break;
                                case Grobal2.RM_CRY:
                                    Def = Grobal2.MakeDefaultMsg(Grobal2.SM_HEAR, (int)msg.sender, MakeWord(0, 151), 0, 1);
                                    break;
                                case Grobal2.RM_WHISPER:
                                    Def = Grobal2.MakeDefaultMsg(Grobal2.SM_WHISPER, (int)msg.sender, MakeWord(252, 255), 0, 1);
                                    break;
                                case Grobal2.RM_GMWHISPER:
                                    Def = Grobal2.MakeDefaultMsg(Grobal2.SM_WHISPER, (int)msg.sender, MakeWord(249, 255), 0, 1);
                                    break;
                                case Grobal2.RM_LM_WHISPER:
                                    // 70
                                    Def = Grobal2.MakeDefaultMsg(Grobal2.SM_WHISPER, (int)msg.sender, MakeWord(253, 255), 0, 1);
                                    break;
                                case Grobal2.RM_SYSMESSAGE:
                                    Def = Grobal2.MakeDefaultMsg(Grobal2.SM_SYSMESSAGE, (int)msg.sender, MakeWord(255, 56), 0, 1);
                                    break;
                                case Grobal2.RM_SYSMESSAGE2:
                                    Def = Grobal2.MakeDefaultMsg(Grobal2.SM_SYSMESSAGE, (int)msg.sender, MakeWord(219, 255), 0, 1);
                                    break;
                                case Grobal2.RM_SYSMESSAGE3:
                                    Def = Grobal2.MakeDefaultMsg(Grobal2.SM_SYSMESSAGE, (int)msg.sender, MakeWord(56, 255), 0, 1);
                                    break;
                                case Grobal2.RM_SYSMSG_BLUE:
                                    Def = Grobal2.MakeDefaultMsg(Grobal2.SM_SYSMESSAGE, (int)msg.sender, MakeWord(255, 252), 0, 1);
                                    break;
                                case Grobal2.RM_SYSMSG_PINK:
                                    Def = Grobal2.MakeDefaultMsg(Grobal2.SM_SYSMESSAGE, (int)msg.sender, MakeWord(255, 253), 0, 1);
                                    break;
                                case Grobal2.RM_SYSMSG_GREEN:
                                    // 楷牢绵窍皋矫瘤
                                    Def = Grobal2.MakeDefaultMsg(Grobal2.SM_SYSMESSAGE, (int)msg.sender, MakeWord(255, 220), 0, 1);
                                    break;
                                case Grobal2.RM_SYSMSG_REMARK:
                                    Def = Grobal2.MakeDefaultMsg(Grobal2.SM_SYSMSG_REMARK, (int)msg.sender, MakeWord(219, 255), 0, 1);
                                    break;
                                case Grobal2.RM_GROUPMESSAGE:
                                    Def = Grobal2.MakeDefaultMsg(Grobal2.SM_SYSMESSAGE, (int)msg.sender, MakeWord(196, 255), 0, 1);
                                    break;
                                case Grobal2.RM_GUILDMESSAGE:
                                    // RM_GUILDMESSAGE: Def := MakeDefaultMsg (SM_GUILDMESSAGE, integer(msg.sender), MakeWord(212, 255), 0, 1);
                                    Def = Grobal2.MakeDefaultMsg(Grobal2.SM_GUILDMESSAGE, (int)msg.sender, MakeWord(211, 255), 0, 1);
                                    break;
                                case Grobal2.RM_MERCHANTSAY:
                                    Def = Grobal2.MakeDefaultMsg(Grobal2.SM_MERCHANTSAY, (int)msg.sender, 0, 0, 1);
                                    break;
                            }
                            str = EDcode.EncodeString(msg.description);
                            SendSocket(Def, str);
                            break;
                        case Grobal2.RM_MERCHANTDLGCLOSE:
                            SendDefMessage(Grobal2.SM_MERCHANTDLGCLOSE, msg.lParam1, msg.lParam2, 0, 0, "");
                            break;
                        case Grobal2.RM_SENDGOODSLIST:
                            // merchant id
                            // count
                            SendDefMessage(Grobal2.SM_SENDGOODSLIST, msg.lParam1, msg.lParam2, 0, 0, msg.description);
                            break;
                        case Grobal2.RM_SENDUSERSELL:
                            // merchant id
                            // count
                            SendDefMessage(Grobal2.SM_SENDUSERSELL, msg.lParam1, msg.lParam2, 0, 0, "");
                            break;
                        case Grobal2.RM_SENDUSERREPAIR:
                        case Grobal2.RM_SENDUSERSPECIALREPAIR:
                            // merchant id
                            // count
                            SendDefMessage(Grobal2.SM_SENDUSERREPAIR, msg.lParam1, msg.lParam2, 0, 0, "");
                            break;
                        case Grobal2.RM_SENDUSERSTORAGEITEM:
                            // merchant id
                            // count
                            SendDefMessage(Grobal2.SM_SENDUSERSTORAGEITEM, msg.lParam1, msg.lParam2, 0, 0, "");
                            break;
                        case Grobal2.RM_SENDUSERSTORAGEITEMLIST:
                            ServerSendStorageItemList(msg.lParam1);
                            break;
                        case Grobal2.RM_SENDUSERMAKEDRUGITEMLIST:
                            // merchant id
                            // count
                            SendDefMessage(Grobal2.SM_SENDUSERMAKEDRUGITEMLIST, msg.lParam1, msg.lParam2, 0, 0, msg.description);
                            break;
                        case Grobal2.RM_SENDBUYPRICE:
                            // buy price
                            SendDefMessage(Grobal2.SM_SENDBUYPRICE, msg.lParam1, 0, 0, 0, "");
                            break;
                        case Grobal2.RM_USERSELLITEM_OK:
                            // chg gold
                            SendDefMessage(Grobal2.SM_USERSELLITEM_OK, msg.lParam1, 0, 0, 0, "");
                            break;
                        case Grobal2.RM_USERSELLITEM_FAIL:
                            SendDefMessage(Grobal2.SM_USERSELLITEM_FAIL, msg.lParam1, 0, 0, 0, "");
                            break;
                        case Grobal2.RM_USERSELLCOUNTITEM_OK:
                            // 墨款飘酒捞袍
                            // gadget : 墨款飘酒捞袍
                            // chg gold
                            SendDefMessage(Grobal2.SM_USERSELLCOUNTITEM_OK, msg.lParam1, msg.lParam2, msg.lParam3, 0, "");
                            break;
                        case Grobal2.RM_USERSELLCOUNTITEM_FAIL:
                            // gadget : 墨款飘酒捞袍
                            SendDefMessage(Grobal2.SM_USERSELLCOUNTITEM_FAIL, msg.lParam1, 0, 0, 0, "");
                            break;
                        case Grobal2.RM_SENDUSERMAKEITEMLIST:
                            // 酒捞袍 力炼
                            // merchant id
                            // count
                            SendDefMessage(Grobal2.SM_SENDUSERMAKEITEMLIST, msg.lParam1, msg.lParam2, 0, 0, msg.description);
                            break;
                        case Grobal2.RM_BUYITEM_SUCCESS:
                            // chg gold
                            SendDefMessage(Grobal2.SM_BUYITEM_SUCCESS, msg.lParam1, HUtil32.LoWord(msg.lParam2), HUtil32.HiWord(msg.lParam2), 0, "");
                            break;
                        case Grobal2.RM_BUYITEM_FAIL:
                            // error code
                            SendDefMessage(Grobal2.SM_BUYITEM_FAIL, msg.lParam1, 0, 0, 0, "");
                            break;
                        case Grobal2.RM_MAKEDRUG_SUCCESS:
                            // chg gold
                            SendDefMessage(Grobal2.SM_MAKEDRUG_SUCCESS, msg.lParam1, 0, 0, 0, "");
                            break;
                        case Grobal2.RM_MAKEDRUG_FAIL:
                            // chg gold
                            SendDefMessage(Grobal2.SM_MAKEDRUG_FAIL, msg.lParam1, 0, 0, 0, "");
                            break;
                        case Grobal2.RM_SENDDETAILGOODSLIST:
                            // merchant id
                            // count
                            // menuindex
                            SendDefMessage(Grobal2.SM_SENDDETAILGOODSLIST, msg.lParam1, msg.lParam2, msg.lParam3, 0, msg.description);
                            break;
                        case Grobal2.RM_USERREPAIRITEM_OK:
                            // cost
                            // dura
                            // maxdura
                            SendDefMessage(Grobal2.SM_USERREPAIRITEM_OK, msg.lParam1, msg.lParam2, msg.lParam3, 0, "");
                            break;
                        case Grobal2.RM_USERREPAIRITEM_FAIL:
                            // cost
                            SendDefMessage(Grobal2.SM_USERREPAIRITEM_FAIL, msg.lParam1, 0, 0, 0, "");
                            break;
                        case Grobal2.RM_SENDREPAIRCOST:
                            // cost
                            SendDefMessage(Grobal2.SM_SENDREPAIRCOST, msg.lParam1, 0, 0, 0, "");
                            break;
                        case Grobal2.RM_MARKET_LIST:
                            // 困殴魄概
                            SendDefMessage(Grobal2.SM_MARKET_LIST, msg.lParam1, msg.lParam2, msg.lParam3, 0, msg.description);
                            break;
                        case Grobal2.RM_MARKET_RESULT:
                            SendDefMessage(Grobal2.SM_MARKET_RESULT, msg.lParam1, msg.lParam2, msg.lParam3, 0, "");
                            break;
                        case Grobal2.RM_ITEMSHOW:
                            // pointer
                            // x
                            // y
                            // looks
                            SendDefMessage(Grobal2.SM_ITEMSHOW, msg.lParam1, msg.lParam2, msg.lParam3, msg.wParam, msg.description);
                            break;
                        case Grobal2.RM_ITEMHIDE:
                            // pointer
                            // x
                            // y
                            SendDefMessage(Grobal2.SM_ITEMHIDE, msg.lParam1, msg.lParam2, msg.lParam3, 0, "");
                            break;
                        case Grobal2.RM_DELITEMS:
                            if (msg.lParam1 != 0)
                            {
                                SendDelItems(msg.lParam1 as ArrayList);
                                (msg.lParam1 as ArrayList).Free();
                                // 咯扁辑 Free秦具 窃...
                            }
                            break;
                        case Grobal2.RM_GUILDAGITLIST:
                            // 巩颇 厘盔
                            // page
                            // count
                            SendDefMessage(Grobal2.SM_GUILDAGITLIST, msg.lParam1, msg.lParam2, 0, 0, msg.description);
                            break;
                        case Grobal2.RM_GABOARD_LIST:
                            // 厘盔霸矫魄
                            // page
                            // count
                            // allpage
                            SendDefMessage(Grobal2.SM_GABOARD_LIST, msg.lParam1, msg.lParam2, msg.lParam3, 0, msg.description);
                            break;
                        case Grobal2.RM_GABOARD_NOTICE_OK:
                            // 厘盔霸矫魄
                            SendDefMessage(Grobal2.SM_GABOARD_NOTICE_OK, msg.lParam1, msg.lParam2, msg.lParam3, 0, msg.description);
                            break;
                        case Grobal2.RM_GABOARD_NOTICE_FAIL:
                            // 厘盔霸矫魄
                            SendDefMessage(Grobal2.SM_GABOARD_NOTICE_FAIL, msg.lParam1, msg.lParam2, msg.lParam3, 0, msg.description);
                            break;
                        case Grobal2.RM_DECOITEM_LIST:
                            // --------------------------------
                            // 厘盔操固扁
                            // merchant id
                            // count
                            SendDefMessage(Grobal2.SM_DECOITEM_LIST, msg.lParam1, msg.lParam2, 0, 0, msg.description);
                            break;
                        case Grobal2.RM_DECOITEM_LISTSHOW:
                            // merchant id
                            // count
                            SendDefMessage(Grobal2.SM_DECOITEM_LISTSHOW, msg.lParam1, msg.lParam2, 0, 0, msg.description);
                            break;
                        case Grobal2.RM_CANCLOSE_OK:
                            // --------------------------------
                            SendDefMessage(Grobal2.SM_CANCLOSE_OK, msg.lParam1, 0, 0, 0, msg.description);
                            break;
                        case Grobal2.RM_CANCLOSE_FAIL:
                            SendDefMessage(Grobal2.SM_CANCLOSE_FAIL, msg.lParam1, 0, 0, 0, msg.description);
                            break;
                        case Grobal2.RM_OPENDOOR_OK:
                            SendDefMessage(Grobal2.SM_OPENDOOR_OK, 0, msg.lParam1, msg.lParam2, 0, "");
                            break;
                        case Grobal2.RM_CLOSEDOOR:
                            SendDefMessage(Grobal2.SM_CLOSEDOOR, 0, msg.lParam1, msg.lParam2, 0, "");
                            break;
                        case Grobal2.RM_SENDUSEITEMS:
                            SendUseItems();
                            break;
                        case Grobal2.RM_SENDMYMAGIC:
                            SendMyMagics();
                            break;
                        case Grobal2.RM_WEIGHTCHANGED:
                            SendDefMessage(Grobal2.SM_WEIGHTCHANGED, this.WAbil.Weight, this.WAbil.WearWeight, this.WAbil.HandWeight, (this.WAbil.Weight + this.WAbil.WearWeight + this.WAbil.HandWeight) ^ 0x3A5F ^ 0x1F35 ^ 0xaa21, "");
                            break;
                        case Grobal2.RM_GOLDCHANGED:
                            SendDefMessage(Grobal2.SM_GOLDCHANGED, this.Gold, 0, 0, 0, "");
                            break;
                        case Grobal2.RM_GAMEGOLDCHANGED:
                            SendDefMessage(Grobal2.SM_GAMEGOLDCHANGED, this.GameGold, 0, 0, 0, "");
                            break;
                        case Grobal2.RM_FEATURECHANGED:
                            SendDefMessage(Grobal2.SM_FEATURECHANGED, (int)msg.sender, HUtil32.LoWord(msg.lParam1), HUtil32.HiWord(msg.lParam1), 0, "");
                            break;
                        case Grobal2.RM_CHARSTATUSCHANGED:
                            SendDefMessage(Grobal2.SM_CHARSTATUSCHANGED, (int)msg.sender, HUtil32.LoWord(msg.lParam1), HUtil32.HiWord(msg.lParam1), msg.wParam, "");
                            break;
                        case Grobal2.RM_CLEAROBJECTS:
                            SendDefMessage(Grobal2.SM_CLEAROBJECTS, 0, 0, 0, 0, "");
                            break;
                        case Grobal2.RM_MAGIC_LVEXP:
                            // lv
                            SendDefMessage(Grobal2.SM_MAGIC_LVEXP, msg.lParam1, msg.lParam2, HUtil32.LoWord(msg.lParam3), HUtil32.HiWord(msg.lParam3), "");
                            break;
                        case Grobal2.RM_SOUND:
                            SendDefMessage(Grobal2.SM_SOUND, 0, msg.lParam1, 0, 0, "");
                            break;
                        case Grobal2.RM_DURACHANGE:
                            SendDefMessage(Grobal2.SM_DURACHANGE, msg.lParam1, msg.wParam, HUtil32.LoWord(msg.lParam2), HUtil32.HiWord(msg.lParam2), "");
                            break;
                        case Grobal2.RM_CHANGELIGHT:
                            // RM_ITEMDURACHANGE:
                            // begin
                            // SendDefMessage (SM_ITEMDURACHANGE, msg.lparam1, msg.lparam2{dura}, msg.lparam3{duramax}, 0, '');
                            // end;
                            SendDefMessage(Grobal2.SM_CHANGELIGHT, (int)msg.sender, (msg.sender as TCreature).Light, 0, 0, "");
                            break;
                        case Grobal2.RM_LAMPCHANGEDURA:
                            SendDefMessage(Grobal2.SM_LAMPCHANGEDURA, msg.lParam1, 0, 0, 0, "");
                            break;
                        case Grobal2.RM_COUNTERITEMCHANGE:
                            // 墨款飘 酒捞袍
                            ServerSendItemCountChanged(msg.lParam1, msg.lParam2, msg.lParam3, msg.description);
                            break;
                        case Grobal2.RM_GROUPCANCEL:
                            SendDefMessage(Grobal2.SM_GROUPCANCEL, 0, 0, 0, 0, "");
                            break;
                        case Grobal2.RM_CHANGEGUILDNAME:
                            SendChangeGuildName();
                            break;
                        case Grobal2.RM_BUILDGUILD_OK:
                            SendDefMessage(Grobal2.SM_BUILDGUILD_OK, 0, 0, 0, 0, "");
                            break;
                        case Grobal2.RM_BUILDGUILD_FAIL:
                            SendDefMessage(Grobal2.SM_BUILDGUILD_FAIL, msg.lParam1, 0, 0, 0, "");
                            break;
                        case Grobal2.RM_DONATE_OK:
                            SendDefMessage(Grobal2.SM_DONATE_OK, msg.lParam1, 0, 0, 0, "");
                            break;
                        case Grobal2.RM_DONATE_FAIL:
                            SendDefMessage(Grobal2.SM_DONATE_FAIL, msg.lParam1, 0, 0, 0, "");
                            break;
                        case Grobal2.RM_MENU_OK:
                            SendDefMessage(Grobal2.SM_MENU_OK, msg.lParam1, 0, 0, 0, msg.description);
                            break;
                        case Grobal2.RM_NEXTTIME_PASSWORD:
                            SendDefMessage(Grobal2.SM_NEXTTIME_PASSWORD, 0, 0, 0, 0, "");
                            break;
                        case Grobal2.RM_DOSTARTUPQUEST:
                            DoStartupQuestNow();
                            break;
                        case Grobal2.RM_PLAYDICE:
                            wl.lParam1 = msg.lParam1;
                            wl.lParam2 = msg.lParam2;
                            wl.lTag1 = msg.lParam3;
                            Def = Grobal2.MakeDefaultMsg(Grobal2.SM_PLAYDICE, (int)msg.sender, msg.wParam, 0, 0);
                            SendSocket(Def, EDcode.EncodeBuffer(wl) + EDcode.EncodeString(msg.description));
                            break;
                        case Grobal2.RM_PLAYROCK:
                            wl.lParam1 = msg.lParam1;
                            wl.lParam2 = msg.lParam2;
                            wl.lTag1 = msg.lParam3;
                            Def = Grobal2.MakeDefaultMsg(Grobal2.SM_PLAYROCK, (int)msg.sender, msg.wParam, 0, 0);
                            SendSocket(Def, EDcode.EncodeBuffer(wl) + EDcode.EncodeString(msg.description));
                            break;
                        case Grobal2.RM_ATTACKMODE:
                            Def = Grobal2.MakeDefaultMsg(Grobal2.SM_ATTACKMODE, (int)msg.sender, (msg.sender as TCreature).HumAttackMode, 0, 0);
                            SendSocket(Def, "");
                            break;
                        case Grobal2.RM_GAMECONFIG:
                            Def = Grobal2.MakeDefaultMsg(Grobal2.SM_GAMECONFIG, msg.lParam1, msg.lParam2, msg.lParam3, msg.wParam);
                            SendSocket(Def, msg.description);
                            break;
                        case Grobal2.RM_REFICONINFO:
                            SendDefMessage(Grobal2.SM_REFICONINFO, msg.lParam1, 0, 0, 0, msg.description);
                            break;
                        default:
                            base.RunMsg(msg);
                            break;
                    }
                    if (EmergencyClose || UserRequestClose || UserSocketClosed)
                    {
                        if (!BoChangeServer)
                        {
                            this.KillAllSlaves();
                            if (svMain.DefaultNpc != null)
                            {
                                svMain.DefaultNpc.NpcSayTitle(this, "@_UserLogout");
                            }
                            lovername = fLover.GetLoverName;
                            hum = svMain.UserEngine.GetUserHuman(lovername);
                            if (hum != null)
                            {
                                hum.SendMsg(hum, Grobal2.RM_LM_LOGOUT, 0, 0, 0, 0, "");
                            }
                            else
                            {
                                if (svMain.UserEngine.FindOtherServerUser(lovername, ref svidx))
                                {
                                    svMain.UserEngine.SendInterMsg(Grobal2.ISM_LM_LOGOUT, svidx, this.UserName + "/" + lovername);
                                }
                            }
                            this.DropEventItems();
                        }
                        this.MakeGhost(6);
                        if (BoChangeServer)
                        {
                            this.MapName = ChangeMapName;
                            this.CX = (short)ChangeCX;
                            this.CY = (short)ChangeCY;
                        }
                        if (UserRequestClose)
                        {
                            SendDefMessage(Grobal2.SM_OUTOFCONNECTION, 0, 0, 0, 0, "");
                        }
                        if (!SoftClosed && UserSocketClosed)
                        {
                            IdSrvClient.FrmIDSoc.SendUserClose(UserId, Certification);
                        }
                        return;
                    }
                }
            }
            catch
            {
                svMain.MainOutMessage("[Exception] Operate 2 #" + this.UserName + " Identback:" + identbackup.ToString() + " Ident:" + msg.Ident.ToString() + " Sender:" + ((int)msg.sender).ToString() + " wP:" + msg.wParam.ToString() + " lP1:" + msg.lParam1.ToString() + " lP2:" + msg.lParam2.ToString() + " lP3:" + msg.lParam3.ToString());
            }
            base.Run();
        }

        private void SendLoginNotice()
        {
            int i;
            ArrayList strlist;
            string data = string.Empty;
            strlist = new ArrayList();
            svMain.NoticeMan.GetNoticList("Notice", strlist);
            data = "";
            for (i = 0; i < strlist.Count; i++)
            {
                data = data + strlist[i] + " ";
            }
            strlist.Free();
            SendDefMessage(Grobal2.SM_SENDNOTICE, 0, 0, 0, 0, data);
        }

        public void RunNotice()
        {
            TMessageInfo msg;
            if (EmergencyClose || UserRequestClose || UserSocketClosed)
            {
                if (UserRequestClose)
                {
                    SendDefMessage(Grobal2.SM_OUTOFCONNECTION, 0, 0, 0, 0, "");
                }
                this.MakeGhost(7);
                return;
            }
            try
            {
                // 肺弊牢 傈俊 傍瘤荤亲阑 焊辰促.
                if (!BoSendNotice)
                {
                    SendLoginNotice();
                    BoSendNotice = true;
                }
                else
                {
                    while (this.GetMsg(ref msg))
                    {
                        switch (msg.Ident)
                        {
                            case Grobal2.CM_LOGINNOTICEOK:
                                ServerGetNoticeOk();
                                break;
                        }
                    }
                }
            }
            catch
            {
                svMain.MainOutMessage("[Exception] TUserHuman.RunNotice");
            }
        }

        public void GetGetNotices()
        {
            if (svMain.BoGetGetNeedNotice)
            {
                GetGetNotices();
            }
        }

        private void ServerGetNoticeOk()
        {
            LoginSign = true;
        }

        public int GetStartX()
        {
            int result;
            result = this.HomeX - 2 + new System.Random(3).Next();
            return result;
        }

        public int GetStartY()
        {
            int result;
            result = this.HomeY - 2 + new System.Random(3).Next();
            return result;
        }

        public void CheckHomePos()
        {
            // 矫累窍绰 付阑阑 官曹瘤 搬沥
            int i;
            for (i = 0; i < svMain.StartPoints.Count; i++)
            {
                if (this.PEnvir.MapName == svMain.StartPoints[i])
                {
                    if ((Math.Abs(this.CX - HUtil32.LoWord((int)svMain.StartPoints.Values[i])) < 50) && (Math.Abs(this.CY - HUtil32.HiWord((int)svMain.StartPoints.Values[i])) < 50))
                    {
                        this.HomeMap = svMain.StartPoints[i];
                        this.HomeX = HUtil32.LoWord((int)svMain.StartPoints.Values[i]);
                        this.HomeY = HUtil32.HiWord((int)svMain.StartPoints.Values[i]);
                    }
                }
            }
            if (this.PKLevel() >= 2)
            {
                // 弧盎捞绰 弧盎捞 付阑肺
                this.HomeMap = M2Share.BADMANHOMEMAP;
                this.HomeX = (short)M2Share.BADMANSTARTX;
                this.HomeY = (short)M2Share.BADMANSTARTY;
            }
        }

        // -------------------- 努扼捞攫飘狼 皋技瘤甫 贸府窃 ---------------------
        private void GetQueryUserName(TCreature target, int x, int y)
        {
            string uname = string.Empty;
            int tagcolor;
            if (this.CretInNearXY(target, x, y))
            {
                tagcolor = this.GetThisCharColor(target);
                Def = Grobal2.MakeDefaultMsg(Grobal2.SM_USERNAME, target.ActorId, tagcolor, 0, 0);
                uname = target.GetUserName();
                SendSocket(Def, EDcode.EncodeString(uname));
            }
            else
            {
                SendDefMessage(Grobal2.SM_GHOST, target.ActorId, x, y, 0, "");
            }
        }

        // 努扼捞攫飘俊 焊呈胶 器牢飘甫 炼沥窍扼绊 脚龋甫 焊辰促.
        private void ServerSendAdjustBonus()
        {
            string str;
            TNakedAbility na;
            Def = Grobal2.MakeDefaultMsg(Grobal2.SM_ADJUST_BONUS, BonusPoint, 0, 0, 0);
            str = "";
            na = this.BonusAbil;
            switch (this.Job)
            {
                case 0:
                    str = EDcode.EncodeBuffer(M2Share.WarriorBonus) + "/" + EDcode.EncodeBuffer(this.CurBonusAbil) + "/" + EDcode.EncodeBuffer(na);
                    break;
                case 1:
                    str = EDcode.EncodeBuffer(M2Share.WizzardBonus) + "/" + EDcode.EncodeBuffer(this.CurBonusAbil) + "/" + EDcode.EncodeBuffer(na);
                    break;
                case 2:
                    str = EDcode.EncodeBuffer(M2Share.PriestBonus) + "/" + EDcode.EncodeBuffer(this.CurBonusAbil) + "/" + EDcode.EncodeBuffer(na);
                    break;
            }
            SendSocket(Def, str);
        }

        private void ServerGetOpenDoor(int dx, int dy)
        {
            TDoorInfo pd;
            if (this.PEnvir == svMain.UserCastle.CastlePEnvir)
            {
                pd = this.PEnvir.FindDoor(dx, dy);
                if (svMain.UserCastle.CoreCastlePDoorCore == pd.PCore)
                {
                    // 荤合己狼 郴己巩
                    if (this.RaceServer == Grobal2.RC_USERHUMAN)
                    {
                        if (!svMain.UserCastle.CanEnteranceCoreCastle(this.CX, this.CY, this))
                        {
                            return;
                        }
                    }
                    // 甸绢哎 荐 绝澜.
                }
            }
            svMain.UserEngine.OpenDoor(this.PEnvir, dx, dy);
        }

        private void ServerGetTakeOnItem(byte where, int svindex, string itmname)
        {
            int i;
            int bagindex;
            int ecount;
            TStdItem ps;
            TStdItem ps2;
            TStdItem std;
            TUserItem targpu;
            TUserItem pu;
            ps = null;
            targpu = null;
            bagindex = -1;
            for (i = 0; i < this.ItemList.Count; i++)
            {
                if (this.ItemList[i].MakeIndex == svindex)
                {
                    ps = svMain.UserEngine.GetStdItem(this.ItemList[i].Index);
                    if (ps != null)
                    {
                        if (ps.Name.ToLower().CompareTo(itmname.ToLower()) == 0)
                        {
                            bagindex = i;
                            targpu = this.ItemList[i];
                            break;
                        }
                    }
                }
            }
            ecount = 0;
            if ((ps != null) && (targpu != null))
            {
                if (M2Share.IsTakeOnAvailable(where, ps))
                {
                    // 馒侩且 荐 乐绰 官弗 酒捞袍牢啊?
                    std = ps;
                    svMain.ItemMan.GetUpgradeStdItem(targpu, ref std);
                    // 公扁狼 诀弊饭捞靛等 瓷仿摹甫 掘绢柯促.
                    if (this.CanTakeOn(where, std))
                    {
                        // 郴啊 瓷仿捞 登绰啊?
                        pu = null;
                        if (this.UseItems[where].Index > 0)
                        {
                            // 捞固 馒侩窍绊 乐澜.
                            // 哈瘤 给窍绰 酒捞袍捞 酒囱版快 (固瘤荐肺 哈阑 荐 乐澜)
                            ps2 = svMain.UserEngine.GetStdItem(this.UseItems[where].Index);
                            // 2003/03/15 酒捞袍 牢亥配府 犬厘
                            if (new ArrayList(new int[] { 15, 19, 20, 21, 22, 23, 24, 26, 52, 53, 54 }).Contains(ps2.StdMode))
                            {
                                if (!this.BoNextTimeFreeCurseItem && (this.UseItems[where].Desc[7] != 0))
                                {
                                    // 哈阑 荐 绝绰 酒捞袍
                                    this.SysMsg("您的武器无法拿下来。", 0);
                                    ecount = -4;
                                    goto finish; //@ Unsupport goto 
                                }
                            }
                            if (!this.BoNextTimeFreeCurseItem && (ps2.ItemDesc & Grobal2.IDC_UNABLETAKEOFF != 0))
                            {
                                // 哈阑 荐 绝绰 酒捞袍
                                this.SysMsg("您的武器无法拿下来。", 0);
                                ecount = -4;
                                goto finish; //@ Unsupport goto 
                            }
                            // 例措肺 哈瘤 给窍绰 酒捞袍
                            if (ps2.ItemDesc & Grobal2.IDC_NEVERTAKEOFF != 0)
                            {
                                this.SysMsg("您的武器无法拿下来。", 0);
                                ecount = -4;
                                goto finish; //@ Unsupport goto 
                            }
                            pu = new TUserItem();
                            pu = this.UseItems[where];
                        }
                        // 固瘤狼 加己阑 啊瘤绊 乐绰 酒捞袍牢 版快 茄锅 馒侩窍搁 钱覆
                        // 2003/03/15 酒捞袍 牢亥配府 犬厘
                        if (new ArrayList(new int[] { 15, 19, 20, 21, 22, 23, 24, 26, 52, 53, 54 }).Contains(ps.StdMode))
                        {
                            if (targpu.Desc[8] != 0)
                            {
                                targpu.Desc[8] = 0;
                            }
                            // 固瘤加己 钱覆;
                        }
                        this.UseItems[where] = targpu;
                        this.DelItemIndex(bagindex);
                        // DelItem (svindex, itmname);
                        if (pu != null)
                        {
                            this.AddItem(pu);
                            // 啊规俊 眠啊登绰 酒捞袍 焊辰促.(乐栏搁)
                            SendAddItem(pu);
                        }
                        try
                        {
                            this.RecalcAbilitys();
                            // 瓷仿摹 犁炼沥 茄促.
                        }
                        catch
                        {
                            svMain.MainOutMessage("ItemTakeOn Ability Error ");
                        }
                        this.SendMsg(this, Grobal2.RM_ABILITY, 0, 0, 0, 0, "");
                        this.SendMsg(this, Grobal2.RM_SUBABILITY, 0, 0, 0, 0, "");
                        SendDefMessage(Grobal2.SM_TAKEON_OK, this.Feature(), 0, 0, 0, "");
                        // 馒侩 己傍 焊辰促. 函版等 葛嚼阑 焊辰促.
                        this.FeatureChanged();
                        // 朝俺渴老版快俊绰 饭骇俊 蝶弗 瓷仿摹甫 官层霖促.(sonmg 荐沥 2004/04/02)
                        if ((ps.StdMode == ObjBase.DRESS_STDMODE_MAN) || (ps.StdMode == ObjBase.DRESS_STDMODE_WOMAN))
                        {
                            if (ps.Shape == ObjBase.DRESS_SHAPE_WING)
                            {
                                SendUpdateItemWithLevel(this.UseItems[where], this.Abil.Level);
                            }
                            else if (ps.Shape == ObjBase.DRAGON_DRESS_SHAPE)
                            {
                                SendUpdateItemByJob(this.UseItems[where], this.Abil.Level);
                            }
                            else if (ps.Shape == ObjBase.DRESS_SHAPE_PBKING)
                            {
                                SendUpdateItemByJob(this.UseItems[where], this.Abil.Level);
                            }
                        }
                        else
                        {
                            SendUpdateItem(this.UseItems[where]);
                        }
                        ecount = 1;
                    }
                    else
                    {
                        ecount = -1;
                    }
                }
                else
                {
                    ecount = -2;
                }
            }
        finish:
            if (ecount <= 0)
            {
                SendDefMessage(Grobal2.SM_TAKEON_FAIL, ecount, 0, 0, 0, "");
            }
        }

        private void ServerGetTakeOffItem(byte where, int svindex, string itmname)
        {
            int ecount;
            TStdItem ps;
            TUserItem pu;
            ecount = 0;
            // 2003/03/15 酒捞袍 牢亥配府 犬厘
            if ((!this.BoDealing) && where >= 0 && where <= 12)
            {
                // 背券吝俊绰 酒捞袍阑 给 哈绰促. 8->12
                if (this.UseItems[where].Index > 0)
                {
                    // 馒侩窍绊 乐绢具 哈阑 荐 乐澜.
                    if (this.UseItems[where].MakeIndex == svindex)
                    {
                        ps = svMain.UserEngine.GetStdItem(this.UseItems[where].Index);
                        // 哈瘤 给窍绰 酒捞袍捞 酒囱版快
                        // 2003/03/15 酒捞袍 牢亥配府 犬厘
                        if (new ArrayList(new int[] { 15, 19, 20, 21, 22, 23, 24, 26, 52, 53, 54 }).Contains(ps.StdMode))
                        {
                            if (!this.BoNextTimeFreeCurseItem && (this.UseItems[where].Desc[7] != 0))
                            {
                                // 哈阑 荐 绝绰 酒捞袍
                                this.SysMsg("您的武器无法拿下来。", 0);
                                ecount = -4;
                                goto finish; //@ Unsupport goto 
                            }
                        }
                        if (!this.BoNextTimeFreeCurseItem && (ps.ItemDesc & Grobal2.IDC_UNABLETAKEOFF != 0))
                        {
                            // 哈阑 荐 绝绰 酒捞袍
                            this.SysMsg("您的武器无法拿下来。", 0);
                            ecount = -4;
                            goto finish; //@ Unsupport goto 
                        }
                        // 例措肺 哈瘤 给窍绰 酒捞袍
                        if (ps.ItemDesc & Grobal2.IDC_NEVERTAKEOFF != 0)
                        {
                            this.SysMsg("您的武器无法拿下来。", 0);
                            ecount = -4;
                            goto finish; //@ Unsupport goto 
                        }
                        if (ps.Name.ToLower().CompareTo(itmname.ToLower()) == 0)
                        {
                            pu = new TUserItem();
                            pu = this.UseItems[where];
                            if (this.AddItem(pu))
                            {
                                // 啊规俊 眠啊登绰 酒捞袍 焊辰促.
                                this.UseItems[where].Index = 0;
                                // 瘤框..
                                SendDefMessage(Grobal2.SM_TAKEOFF_OK, this.Feature(), 0, 0, 0, "");
                                SendAddItem(pu);
                                this.RecalcAbilitys();
                                // 瓷仿摹 犁炼沥 茄促.
                                this.SendMsg(this, Grobal2.RM_ABILITY, 0, 0, 0, 0, "");
                                this.SendMsg(this, Grobal2.RM_SUBABILITY, 0, 0, 0, 0, "");
                                this.FeatureChanged();
                            }
                            else
                            {
                                Dispose(pu);
                                ecount = -3;
                            }
                        }
                    }
                }
                else
                {
                    ecount = -2;
                }
            }
            else
            {
                ecount = -1;
            }
        finish:
            if (ecount <= 0)
            {
                SendDefMessage(Grobal2.SM_TAKEOFF_FAIL, ecount, 0, 0, 0, "");
            }
        }

        // ------------------------------
        private bool BindPotionUnit(int iShape, int iCount)
        {
            bool result;
            string strItemName;
            TUserHuman hum;
            TUserItem pui;
            result = false;
            // 何利篮 弓阑 荐 绝促.(sonmg)
            if (iShape == ObjBase.SHAPE_AMULET_BUNCH)
            {
                return result;
            }
            // 何利弓澜
            try
            {
                strItemName = svMain.UserEngine.GetStdItemNameByShape(31, iShape);
                // 弓澜酒捞袍 StdMode, Shape
                pui = new TUserItem();
                if (svMain.UserEngine.CopyToUserItemFromName(strItemName, ref pui))
                {
                    this.ItemList.Add(pui);
                    if (this.RaceServer == Grobal2.RC_USERHUMAN)
                    {
                        hum = this;
                        hum.SendAddItem(pui);
                    }
                    result = true;
                }
                else
                {
                    Dispose(pui);
                }
            }
            catch
            {
            }
            return result;
        }

        public bool ServerGetEatItem_UnbindPotionUnit(string itmname, int count)
        {
            bool result;
            int i;
            TUserHuman hum;
            TUserItem pui;
            result = false;
            for (i = 0; i < count; i++)
            {
                pui = new TUserItem();
                if (svMain.UserEngine.CopyToUserItemFromName(itmname, ref pui))
                {
                    this.ItemList.Add(pui);
                    if (this.RaceServer == Grobal2.RC_USERHUMAN)
                    {
                        hum = this;
                        hum.SendAddItem(pui);
                    }
                }
                else
                {
                    Dispose(pui);
                }
            }
            result = true;
            return result;
        }

        private void ServerGetEatItem(int svindex, string itmname)
        {
            int i;
            int j;
            bool flag;
            TStdItem ps;
            TUserItem ui;
            TUserItem pu;
            ArrayList dellist;
            dellist = null;
            flag = false;
            if (!this.Death)
            {
                for (i = 0; i < this.ItemList.Count; i++)
                {
                    if (this.ItemList[i].MakeIndex == svindex)
                    {
                        // if UserEngine.GetStdItemName (PTUserItem(ItemList[i]).Index) = itmname then begin
                        ps = svMain.UserEngine.GetStdItem(this.ItemList[i].Index);
                        ui = this.ItemList[i];
                        pu = this.ItemList[i];
                        switch (ps.StdMode)
                        {
                            case 0:
                            case 1:
                            case 2:
                            case 3:
                                // 矫距, 绊扁幅, 澜侥, 胶农费
                                if (this.EatItem(ps, this.ItemList[i]))
                                {
                                    Dispose(this.ItemList[i]);
                                    this.ItemList.RemoveAt(i);
                                    flag = true;
                                    if ((ps.StdMode == 3) && (ps.Shape != 2))
                                    {
                                    }
                                }
                                break;
                            case 4:
                                // 氓
                                if (this.ReadBook(ps))
                                {
                                    Dispose(this.ItemList[i]);
                                    this.ItemList.RemoveAt(i);
                                    flag = true;
                                    // 绢八贱
                                    if (this.PLongHitSkill != null)
                                    {
                                        if (!this.BoAllowLongHit)
                                        {
                                            this.SetAllowLongHit(true);
                                            SendSocket(null, "+LNG");
                                            // 盔芭府 傍拜阑 窍霸 茄促.
                                        }
                                    }
                                    // 馆岿八过
                                    if (this.PWideHitSkill != null)
                                    {
                                        if (!this.BoAllowWideHit)
                                        {
                                            this.SetAllowWideHit(true);
                                            SendSocket(null, "+WID");
                                        }
                                    }
                                    // 2003/03/15 脚痹公傍
                                    // 堡浅曼
                                    if (this.PCrossHitSkill != null)
                                    {
                                        if (!this.BoAllowCrossHit)
                                        {
                                            this.SetAllowCrossHit(true);
                                            SendSocket(null, "+CRS");
                                        }
                                    }
                                }
                                break;
                            case 8:
                                // 冈绰(荤侩) 酒捞袍, 骇飘芒俊 馒侩且 荐 绝澜 (檬措厘) (2004/05/06)
                                if (ps.Shape == ObjBase.SHAPE_OF_INVITATION)
                                {
                                    if (this.EatItem(ps, this.ItemList[i]))
                                    {
                                        Dispose(this.ItemList[i]);
                                        this.ItemList.RemoveAt(i);
                                        flag = true;
                                        if ((ps.StdMode == 3) && (ps.Shape != 2))
                                        {
                                        }
                                    }
                                }
                                else if (this.EatItem(ps, this.ItemList[i]))
                                {
                                    Dispose(this.ItemList[i]);
                                    this.ItemList.RemoveAt(i);
                                    flag = true;
                                }
                                break;
                            case 31:
                                if (this.ItemList.Count + 6 - 1 <= Grobal2.MAXBAGITEM)
                                {
                                    Dispose(this.ItemList[i]);
                                    this.ItemList.RemoveAt(i);
                                    ServerGetEatItem_UnbindPotionUnit(svMain.GetUnbindItemName(ps.Shape), 6);
                                    flag = true;
                                }
                                break;
                        }
                        break;
                        // end;
                    }
                }
            }
            if (flag)
            {
                // 昏力格废俊 眠啊等 官牢靛 酒捞袍甸 昏力.
                if (dellist != null)
                {
                    for (j = 0; j < dellist.Count; j++)
                    {
                        for (i = 0; i < this.ItemList.Count; i++)
                        {
                            pu = this.ItemList[i];
                            if (pu.MakeIndex == ((int)dellist.Values[j]))
                            {
                                Dispose(pu);
                                this.ItemList.RemoveAt(i);
                                break;
                            }
                        }
                    }
                    this.SendMsg(this, Grobal2.RM_DELITEMS, 0, (int)dellist, 0, 0, "");
                    // dellist绰 rm_delitem俊辑 free 矫难具 茄促.
                }
                this.WeightChanged();
                SendDefMessage(Grobal2.SM_EAT_OK, 0, 0, 0, 0, "");
                // 拱扒阑 荤侩窍咯 绝绢咙
                // 荤侩_ +
                svMain.AddUserLog("11\09" + this.MapName + "\09" + this.CX.ToString() + "\09" + this.CY.ToString() + "\09" + this.UserName + "\09" + svMain.UserEngine.GetStdItemName(ui.Index) + "\09" + ui.MakeIndex.ToString() + "\09" + BoolToInt(this.RaceServer == Grobal2.RC_USERHUMAN).ToString() + "\09" + "0");
            }
            else
            {
                SendDefMessage(Grobal2.SM_EAT_FAIL, 0, 0, 0, 0, "");
            }
        }

        private void ServerGetButch(TCreature animal, int x, int y, int ndir)
        {
            int n;
            int m;
            Object cret;
            cret = null;
            if ((Math.Abs(x - this.CX) <= 2) && (Math.Abs(y - this.CY) <= 2))
            {
                // 滴 沫 糠鳖瘤 戒 荐 乐澜
                if (this.PEnvir.IsValidFrontCreature(x, y, 2, ref cret))
                {
                    // (sonmg 2004/12/28)
                    if (cret != null)
                    {
                        animal = cret as TCreature;
                        // (sonmg 2004/12/28)
                        if (animal.Death && (!animal.BoSkeleton) && animal.BoAnimal)
                        {
                            // 磊脚狼 档绵 扁贱俊 蝶扼辑 档绵 器牢飘啊 促福霸 利侩等促.
                            // 扁贱捞 绝绰 版快, 5-20 荤捞捞哥, 绊扁狼 龙档 10-20究 冻绢柳促.
                            n = 5 + new System.Random(16).Next();
                            m = 100 + new System.Random(201).Next();
                            animal.BodyLeathery = animal.BodyLeathery - n;
                            animal.MeatQuality = animal.MeatQuality - m;
                            // 漠龙阑 且 荐废 绊扁龙篮 炼陛究 冻绢咙
                            if (animal.MeatQuality < 0)
                            {
                                animal.MeatQuality = 0;
                            }
                            if (animal.BodyLeathery <= 0)
                            {
                                if ((animal.RaceServer >= Grobal2.RC_ANIMAL) && (animal.RaceServer < Grobal2.RC_MONSTER))
                                {
                                    // 荤娇鞍捞 绊扁甫林绰 巴父, 秦榜肺 函窃
                                    animal.BoSkeleton = true;
                                    animal.ApplyMeatQuality();
                                    animal.SendRefMsg(Grobal2.RM_SKELETON, animal.Dir, animal.CX, animal.CY, 0, "");
                                }
                                if (!this.TakeCretBagItems(animal))
                                {
                                    this.SysMsg("没有获得任何东西。", 0);
                                }
                                animal.BodyLeathery = 50;
                                // 皋技瘤啊 楷加栏肺 唱坷绰 巴阑 阜澜.
                            }
                            this.DeathTime  =  HUtil32.GetTickCount();
                            // 档绵窍绊 乐绰档吝俊 绊扁绰 荤扼瘤瘤 臼澜.
                        }
                    }
                }
                this.Dir = (byte)ndir;
            }
            this.SendRefMsg(Grobal2.RM_BUTCH, this.Dir, this.CX, this.CY, 0, "");
        }

        private void ServerGetMagicKeyChange(int magid, int key)
        {
            int i;
            for (i = 0; i < this.MagicList.Count; i++)
            {
                if (((TUserMagic)this.MagicList[i]).pDef.MagicId == magid)
                {
                    ((TUserMagic)this.MagicList[i]).Key = AnsiChar(key);
                    break;
                }
            }
        }

        private void ServerGetClickNpc(int clickid)
        {
            int i;
            TCreature npc;
            TUserHuman who;
            TVisibleActor pva;
            if (this.BoDealing)
            {
                return;
            }
            // 背券吝俊绰 npc甫 努腐且 荐 绝促.
            // 呈公 狐弗 NPC努腐阑 阜绰促.
            // ms
            if (HUtil32.GetTickCount() - NpcClickTime < 1000)
            {
                return;
            }
            NpcClickTime  =  HUtil32.GetTickCount();
            // NPC殿, 惑牢甸 八荤
            npc = svMain.UserEngine.GetMerchant(clickid);
            if (npc == null)
            {
                npc = svMain.UserEngine.GetNpc(clickid);
            }
            if (npc != null)
            {
                if ((npc.PEnvir == this.PEnvir) && (Math.Abs(npc.CX - this.CX) <= 15) && (Math.Abs(npc.CY - this.CY) <= 15))
                {
                    ((TNormNpc)npc).UserCall(this);
                }
                return;
            }
            who = null;
            for (i = 0; i < this.VisibleActors.Count; i++)
            {
                pva = this.VisibleActors[i];
                pva = this.VisibleActors[i];
                if (pva.cret == (clickid as Object))
                {
                    who = pva.cret as TUserHuman;
                    break;
                }
            }
            if ((who != null) && (who != this))
            {
                if ((who.PEnvir == this.PEnvir) && (Math.Abs(who.CX - this.CX) <= 15) && (Math.Abs(who.CY - this.CY) <= 15))
                {
                    if ((who.RaceServer == Grobal2.RC_USERHUMAN) && who.StallMgr.OnSale)
                    {
                        if (who.Death || who.BoGhost)
                        {
                            return;
                        }
                        who.SendStallItems(this);
                    }
                }
            }
        }

        private bool CretInNearStallXY(int xx, int yy)
        {
            bool result;
            int i;
            int j;
            TCreature target;
            result = true;
            for (i = xx - 2; i <= yy + 2; i++)
            {
                for (j = yy - 2; j <= yy + 2; j++)
                {
                    target = null;
                    if (this.PEnvir.GetMovingObject(i, j, true) == this)
                    {
                        continue;
                    }
                    if (!this.PEnvir.CanWalk(i, j, false))
                    {
                        result = false;
                        return result;
                    }
                    target = (TCreature)this.PEnvir.GetMovingObject(i, j, true);
                    if (target != null)
                    {
                        if ((Math.Abs(xx - target.CX) < 2) || (Math.Abs(yy - target.CY) < 2))
                        {
                            result = false;
                            return result;
                        }
                    }
                }
            }
            return result;
        }

        private bool GetMapCanShop(TEnvirnoment Envir, int nX, int nY, int nRage)
        {
            bool result;
            int III;
            int x;
            int y;
            int nCount;
            int nStartX;
            int nStartY;
            int nEndX;
            int nEndY;
            TMapInfo MapCellInfo;
            TAThing OSObject;
            TCreature BaseObject;
            result = false;
            nCount = 0;
            try
            {
                nStartX = nX - nRage;
                nEndX = nX + nRage;
                nStartY = nY - nRage;
                nEndY = nY + nRage;
                for (x = nStartX; x <= nEndX; x++)
                {
                    if (nCount > 1)
                    {
                        break;
                    }
                    for (y = nStartY; y <= nEndY; y++)
                    {
                        if (nCount > 1)
                        {
                            break;
                        }
                        if (this.PEnvir.GetMapXY(x, y, ref MapCellInfo) && (MapCellInfo.OBJList != null))
                        {
                            if (MapCellInfo.OBJList.Count > 0)
                            {
                                for (III = 0; III < MapCellInfo.OBJList.Count; III++)
                                {
                                    if (nCount > 1)
                                    {
                                        break;
                                    }
                                    OSObject = (TAThing)MapCellInfo.OBJList[III];
                                    if (OSObject != null)
                                    {
                                        if (OSObject.Shape == Grobal2.OS_MOVINGOBJECT)
                                        {
                                            BaseObject = OSObject.AObject as TCreature;
                                            if (BaseObject != null)
                                            {
                                                if ((!BaseObject.Death) && (!BaseObject.BoGhost))
                                                {
                                                    nCount++;
                                                }
                                            }
                                        }
                                    }
                                }
                                // for
                            }
                        }
                    }
                }
            }
            catch
            {
                svMain.MainOutMessage("TUserHuman::GetMapCanShop");
            }
            if (nCount > 1)
            {
                result = true;
            }
            return result;
        }

        private void GetSaleItemListEx(TMessageInfo pmsg)
        {
            string sSENDMSG;
            string sSENDMSGSP;
            int i;
            int nCount;
            int nCountsp;
            TShopItem ShopItem;
            TShopItem pShopItem;
            sSENDMSG = "";
            sSENDMSGSP = "";
            nCount = 0;
            nCountsp = 0;
            svMain.ShopItemList.Lock;
            try
            {
                for (i = 0; i < svMain.ShopItemList.Count; i++)
                {
                    pShopItem = svMain.ShopItemList.Items[i];
                    if (pShopItem.btClass == 5)
                    {
                        ShopItem = pShopItem;
                        sSENDMSGSP = sSENDMSGSP + EDcode.EncodeBuffer(ShopItem, sizeof(TShopItem)) + "/";
                        nCountsp++;
                        if (nCountsp >= 5)
                        {
                            break;
                        }
                    }
                    else if (pmsg.lParam1 == pShopItem.btClass)
                    {
                        ShopItem = pShopItem;
                        sSENDMSG = sSENDMSG + EDcode.EncodeBuffer(ShopItem, sizeof(TShopItem)) + "/";
                        nCount++;
                        if (nCount >= 255)
                        {
                            // 修正商铺页数支持255
                            break;
                        }
                    }
                }
            }
            finally
            {
                svMain.ShopItemList.UnLock;
            }
            if (pmsg.lParam1 >= 0 && pmsg.lParam1 <= 4 && (m_btGetShopItem[pmsg.lParam1] == 0))
            {
                m_btGetShopItem[pmsg.lParam1] = 1;
                // g_Config.btSellType
                Def = Grobal2.MakeDefaultMsg(Grobal2.SM_SPECOFFERITEM, (int)pmsg.sender, 0, 0, 0);
                if (sSENDMSG != "")
                {
                    SendSocket(Def, sSENDMSG);
                }
            }
            if (m_btGetShopItem[5] == 0)
            {
                m_btGetShopItem[5] = 1;
                // g_Config.btSellType
                Def = Grobal2.MakeDefaultMsg(Grobal2.SM_OFFERITEM, (int)pmsg.sender, 0, 0, 0);
                if (sSENDMSGSP != "")
                {
                    SendSocket(Def, sSENDMSGSP);
                }
            }
        }

        private void BuySaleItemListEx(TMessageInfo pmsg)
        {
            string sItemName;
            int nRetCode;
            TStdItem pStdItem;
            TUserItem pUserItem;
            TShopItem pShopItem;
            if (this.BoDealing || (HUtil32.GetTickCount() - this.DealItemChangeTime <= 1000))
            {
                this.SysMsg("[交易状态] 不能使用商铺功能！", 0);
                return;
            }
            // if g_Config.btSellType = 0 then begin
            if (HUtil32.GetTickCount() - m_dwBuyShopItemTick >= 500)
            {
                m_dwBuyShopItemTick = GetTickCount();
                if (this.GameGold > 0)
                {
                    sItemName = EDcode.DecodeString(pmsg.description);
                    if (sItemName != "")
                    {
                        pShopItem = svMain.UserEngine.GetShopItemByName(sItemName);
                        if (pShopItem != null)
                        {
                            pStdItem = svMain.UserEngine.GetStdItemFromName(pShopItem.sItemName);
                            if (pStdItem != null)
                            {
                                if ((pShopItem.wPrice >= 0) && (this.GameGold >= pShopItem.wPrice))
                                {
                                    // ///////////////////
                                    if (pStdItem.OverlapItem >= 1)
                                    {
                                        if (this.UserCounterItemAdd(pStdItem.StdMode, pStdItem.Looks, 1, pStdItem.Name, false))
                                        {
                                            this.GameGold -= pShopItem.wPrice;
                                            this.GameGoldChanged();
                                            return;
                                        }
                                        else
                                        {
                                            goto lab; //@ Unsupport goto 
                                        }
                                    }
                                    else
                                    {
                                    lab:
                                        if (this.IsEnoughBag())
                                        {
                                            pUserItem = new TUserItem();
                                            if (svMain.UserEngine.CopyToUserItemFromName(sItemName, ref pUserItem) && AddToBagItem(pUserItem))
                                            {
                                                this.GameGold -= pShopItem.wPrice;
                                                // SendAddItem(pUserItem^);
                                                this.GameGoldChanged();
                                            }
                                            else
                                            {
                                                Dispose(pUserItem);
                                            }
                                            return;
                                        }
                                        else
                                        {
                                            nRetCode = -4;
                                        }
                                    }
                                }
                                else
                                {
                                    nRetCode = -3;
                                }
                            }
                            else
                            {
                                nRetCode = -1;
                            }
                        }
                        else
                        {
                            nRetCode = -5;
                        }
                    }
                    else
                    {
                        nRetCode = 0;
                    }
                }
                else
                {
                    nRetCode = -2;
                }
            }
            else
            {
                nRetCode = -6;
            }
            SendDefMessage(Grobal2.SM_BUGITEMFAIL, nRetCode, 0, 0, 0, "");
        }

        private void PresendItem(TMessageInfo pmsg)
        {
            TUserHuman PlayObject;
            string s;
            string sWho;
            string sItemName;
            int nRetCode;
            TStdItem pStdItem;
            TUserItem pUserItem;
            TShopItem pShopItem;
            if (HUtil32.GetTickCount() - m_dwBuyShopItemTick >= 500)
            {
                m_dwBuyShopItemTick = GetTickCount();
                if (this.GameGold > 0)
                {
                    s = EDcode.DecodeString(pmsg.description);
                    sItemName  =  HUtil32.GetValidStr3(s, ref sWho, new string[] { "/" });
                    if (sWho.ToLower().CompareTo(this.UserName.ToLower()) != 0)
                    {
                        PlayObject = svMain.UserEngine.GetUserHuman(sWho);
                        if (PlayObject != null)
                        {
                            if (!PlayObject.BoExchangeAvailable)
                            {
                                PlayObject.SysMsg(string.Format("%s向你赠送%s未成功,你必须先允许交易才能接受赠送", new object[] { this.UserName, sItemName }), 1);
                                this.SysMsg(string.Format("%s 当前不允许交易,对方必须允许交易后你才能再进行赠送", new object[] { PlayObject.UserName }), 0);
                                return;
                            }
                            if (sItemName != "")
                            {
                                pShopItem = svMain.UserEngine.GetShopItemByName(sItemName);
                                if (pShopItem != null)
                                {
                                    pStdItem = svMain.UserEngine.GetStdItemFromName(pShopItem.sItemName);
                                    if (pStdItem != null)
                                    {
                                        if ((pShopItem.wPrice >= 0) && (this.GameGold >= pShopItem.wPrice))
                                        {
                                            if (pStdItem.OverlapItem >= 1)
                                            {
                                                if (PlayObject.UserCounterItemAdd(pStdItem.StdMode, pStdItem.Looks, 1, pStdItem.Name, false))
                                                {
                                                    this.GameGold -= pShopItem.wPrice;
                                                    this.GameGoldChanged();
                                                    PlayObject.SysMsg(Format("%s向你赠送了%s，请注意查收", new string[] { this.UserName, sItemName }), 4);
                                                    nRetCode = 1;
                                                }
                                                else
                                                {
                                                    goto lab; 
                                                }
                                            }
                                            else
                                            {
                                            lab:
                                                if (PlayObject.IsEnoughBag())
                                                {
                                                    pUserItem = new TUserItem();
                                                    if (svMain.UserEngine.CopyToUserItemFromName(sItemName, ref pUserItem) && PlayObject.AddToBagItem(pUserItem))
                                                    {
                                                        this.GameGold -= pShopItem.wPrice;
                                                        // PlayObject.SendAddItem(pUserItem^);
                                                        this.GameGoldChanged();
                                                        PlayObject.SysMsg(Format("%s向你赠送了%s，请注意查收", new string[] { this.UserName, sItemName }), 4);
                                                        nRetCode = 1;
                                                    }
                                                    else
                                                    {
                                                        Dispose(pUserItem);
                                                        nRetCode = -1;
                                                    }
                                                }
                                                else
                                                {
                                                    nRetCode = -4;
                                                }
                                            }
                                        }
                                        else
                                        {
                                            nRetCode = -3;
                                        }
                                    }
                                    else
                                    {
                                        nRetCode = -1;
                                    }
                                }
                                else
                                {
                                    nRetCode = -5;
                                }
                            }
                            else
                            {
                                nRetCode = 0;
                            }
                        }
                        else
                        {
                            nRetCode = -7;
                        }
                    }
                    else
                    {
                        nRetCode = -8;
                    }
                }
                else
                {
                    nRetCode = -2;
                }
                // end else begin
                // if m_nGold > 0 then begin
                // s := EDcode.DecodeString(pmsg.sMsg);
                // sItemName : =  HUtil32.GetValidStr3(s, sWho, ['/']);
                // if CompareText(sWho, m_sCharName) <> 0 then begin
                // PlayObject := UserEngine.GetPlayObject(sWho);
                // if PlayObject <> nil then begin
                // if not PlayObject.m_boAllowDeal then begin
                // PlayObject.SysMsg(Format('%s向你赠送%s未成功,你必须先允许交易才能接受赠送', [m_sCharName, sItemName]), c_Blue, t_Hint);
                // SysMsg(Format('%s 当前不允许交易,对方必须允许交易后你才能再进行赠送', [PlayObject.m_sCharName]), c_Red, t_Hint);
                // Exit;
                // end;
                // if sItemName <> '' then begin
                // pShopItem := GetShopItemByName(sItemName);
                // if pShopItem <> nil then begin
                // pStdItem := UserEngine.GetStdItem(pShopItem^.sItemName);
                // if pStdItem <> nil then begin
                // if (pShopItem^.wPrice >= 0) and (m_nGold >= pShopItem^.wPrice) then begin
                // /////////////////////
                // if pStdItem.Overlap >= 1 then begin
                // if PlayObject.UserCounterItemAdd(pStdItem.StdMode, pStdItem.Looks, 1, pStdItem.Name, False) then begin
                // Dec(m_nGold, pShopItem^.wPrice);
                // GoldChanged();
                // if pStdItem.NeedIdentify = 1 then
                // AddGameDataLogAPI('9' + #9 +
                // m_sMapName + #9 +
                // IntToStr(m_nCurrX) + #9 +
                // IntToStr(m_nCurrY) + #9 +
                // m_sCharName + #9 +
                // pStdItem.Name + #9 +
                // '9999' + #9 +
                // '1' + #9 +
                // '商铺赠送给:' + PlayObject.m_sCharName + '-' + IntToStr(pShopItem^.wPrice) + '金币');
                // PlayObject.SysMsg(Format('%s向你赠送了%s，请注意查收', [m_sCharName, sItemName]), c_Purple, t_Hint);
                // nRetCode := 1;
                // end else begin
                // goto lab2;
                // end;
                // end else begin
                // lab2:
                // if PlayObject.IsEnoughBag then begin
                // New(pUserItem);
                // if UserEngine.CopyToUserItemFromName(sItemName, pUserItem) and PlayObject.AddItemToBag(pUserItem) then begin
                // Dec(m_nGold, pShopItem^.wPrice);
                // PlayObject.SendAddItem(pUserItem);
                // GoldChanged();
                // if pStdItem.NeedIdentify = 1 then
                // AddGameDataLogAPI('9' + #9 +
                // m_sMapName + #9 +
                // IntToStr(m_nCurrX) + #9 +
                // IntToStr(m_nCurrY) + #9 +
                // m_sCharName + #9 +
                // pStdItem.Name + #9 +
                // IntToStr(pUserItem.MakeIndex) + #9 +
                // '1' + #9 +
                // '商铺赠送给:' + PlayObject.m_sCharName + '-' + IntToStr(pShopItem^.wPrice) + '金币');
                // PlayObject.SysMsg(Format('%s向你赠送了%s，请注意查收', [m_sCharName, sItemName]), c_Purple, t_Hint);
                // if g_Config.nShopItemBind = 1 then
                // BindItemCharName(pUserItem^.wIndex, pUserItem^.MakeIndex, PlayObject.m_sCharName)
                // else if g_Config.nShopItemBind = 2 then
                // BindItemAccount(pUserItem^.wIndex, pUserItem^.MakeIndex, PlayObject.m_sUserID);
                // nRetCode := 1;
                // end else begin
                // Dispose(pUserItem);
                // nRetCode := -1;
                // end;
                // end else
                // nRetCode := -4;
                // end;
                // end else
                // nRetCode := -3;
                // end else
                // nRetCode := -1;
                // end else
                // nRetCode := -5;
                // end else
                // nRetCode := 0;
                // end else
                // nRetCode := -7;
                // end else
                // nRetCode := -8;
                // end else
                // nRetCode := -2;
                // end;
            }
            else
            {
                nRetCode = -6;
            }
            // g_Config.btSellType
            // g_Config.btSellType
            // g_Config.btSellType
            SendDefMessage(Grobal2.SM_PRESENDITEMFAIL, nRetCode, 0, 0, 0, "");
        }

        private void ServerGetMerchantDlgSelect(int npcid, string clickstr)
        {
            TNormNpc npc;
            // 呈公 狐弗 NPC努腐阑 阜绰促.
            // ms
            if (HUtil32.GetTickCount() - NpcClickTime < 200)
            {
                return;
            }
            NpcClickTime  =  HUtil32.GetTickCount();
            npc = (TNormNpc)svMain.UserEngine.GetMerchant(npcid);
            if (npc == null)
            {
                npc = (TNormNpc)svMain.UserEngine.GetNpc(npcid);
            }
            if (npc == null)
            {
                npc = (TNormNpc)svMain.UserEngine.GetDefaultNpc(npcid);
            }
            if (npc != null)
            {
                // npc.BoInvisible => 甘 涅胶飘牢 版快
                if ((npc.PEnvir == this.PEnvir) && (Math.Abs(npc.CX - this.CX) <= 15) && (Math.Abs(npc.CY - this.CY) <= 15) || npc.BoInvisible)
                {
                    npc.UserSelect(this, clickstr);
                }
            }
        }

        private void ServerGetMerchantQuerySellPrice(int npcid, int itemindex, string itemname)
        {
            int i;
            TCreature npc;
            TUserItem pu;
            pu = null;
            // 郴 啊规狼 酒捞袍俊辑 itemindex狼 酒捞袍阑 茫绰促.
            for (i = 0; i < this.ItemList.Count; i++)
            {
                if (this.ItemList[i].MakeIndex == itemindex)
                {
                    if (svMain.UserEngine.GetStdItemName(this.ItemList[i].Index).ToLower().CompareTo(itemname.ToLower()) == 0)
                    {
                        pu = this.ItemList[i];
                        break;
                    }
                }
            }
            if (pu != null)
            {
                npc = svMain.UserEngine.GetMerchant(npcid);
                if (npc != null)
                {
                    if ((npc.PEnvir == this.PEnvir) && (Math.Abs(npc.CX - this.CX) <= 15) && (Math.Abs(npc.CY - this.CY) <= 15))
                    {
                        ((TMerchant)npc).QueryPrice(this, pu);
                    }
                }
            }
        }

        private void ServerGetMerchantQueryRepairPrice(int npcid, int itemindex, string itemname)
        {
            int i;
            TCreature npc;
            TUserItem pu;
            pu = null;
            // 郴 啊规狼 酒捞袍俊辑 itemindex狼 酒捞袍阑 茫绰促.
            for (i = 0; i < this.ItemList.Count; i++)
            {
                if (this.ItemList[i].MakeIndex == itemindex)
                {
                    if (svMain.UserEngine.GetStdItemName(this.ItemList[i].Index).ToLower().CompareTo(itemname.ToLower()) == 0)
                    {
                        pu = this.ItemList[i];
                        break;
                    }
                }
            }
            if (pu != null)
            {
                npc = svMain.UserEngine.GetMerchant(npcid);
                if (npc != null)
                {
                    if ((npc.PEnvir == this.PEnvir) && (Math.Abs(npc.CX - this.CX) <= 15) && (Math.Abs(npc.CY - this.CY) <= 15))
                    {
                        ((TMerchant)npc).QueryRepairCost(this, pu);
                    }
                }
            }
        }

        private void ServerGetUserSellItem(int npcid, int itemindex, int sellcnt, string itemname)
        {
            int i;
            int temp;
            TCreature npc;
            TUserItem pu;
            TStdItem pstd;
            pu = null;
            // 郴 啊规狼 酒捞袍俊辑 itemindex狼 酒捞袍阑 茫绰促.
            for (i = 0; i < this.ItemList.Count; i++)
            {
                if (this.ItemList[i].MakeIndex == itemindex)
                {
                    if (svMain.UserEngine.GetStdItemName(this.ItemList[i].Index).ToLower().CompareTo(itemname.ToLower()) == 0)
                    {
                        pu = this.ItemList[i];
                        npc = svMain.UserEngine.GetMerchant(npcid);
                        pstd = svMain.UserEngine.GetStdItem(pu.Index);
                        if ((npc != null) && (pu != null) && (pstd != null))
                        {
                            if ((npc.PEnvir == this.PEnvir) && (Math.Abs(npc.CX - this.CX) <= 15) && (Math.Abs(npc.CY - this.CY) <= 15))
                            {
                                if (pstd != null)
                                {
                                    if (pstd.StdMode != ObjBase.TAIWANEVENTITEM)
                                    {
                                        // 措父 捞亥飘侩 酒捞袍篮 迫 荐 绝促
                                        if (pstd.OverlapItem >= 1)
                                        {
                                            // gadget : 墨款飘酒捞袍
                                            temp = pu.Dura;
                                            if ((sellcnt > 0) && (temp >= sellcnt))
                                            {
                                                if (((TMerchant)npc).UserCountSellItem(this, pu, sellcnt))
                                                {
                                                    if (temp - sellcnt <= 0)
                                                    {
                                                        Dispose(this.ItemList[i]);
                                                        this.ItemList.RemoveAt(i);
                                                    }
                                                    else
                                                    {
                                                        this.ItemList[i].Dura = (ushort)(temp - sellcnt);
                                                    }
                                                }
                                                this.WeightChanged();
                                            }
                                        }
                                        else
                                        {
                                            if (((TMerchant)npc).UserSellItem(this, pu))
                                            {
                                                // 魄概茄 酒捞袍阑 绝矩促.
                                                Dispose(this.ItemList[i]);
                                                this.ItemList.RemoveAt(i);
                                                this.WeightChanged();
                                            }
                                            // else
                                            // SendMsg (self, RM_USERSELLITEM_FAIL, 0, 0, 0, 0, '');
                                        }
                                    }
                                }
                            }
                        }
                        break;
                    }
                }
            }
        }

        private void ServerGetUserRepairItem(int npcid, int itemindex, string itemname)
        {
            int i;
            TCreature npc;
            TUserItem pu;
            pu = null;
            // 郴 啊规狼 酒捞袍俊辑 itemindex狼 酒捞袍阑 茫绰促.
            for (i = 0; i < this.ItemList.Count; i++)
            {
                if (this.ItemList[i].MakeIndex == itemindex)
                {
                    if (svMain.UserEngine.GetStdItemName(this.ItemList[i].Index).ToLower().CompareTo(itemname.ToLower()) == 0)
                    {
                        pu = this.ItemList[i];
                        npc = svMain.UserEngine.GetMerchant(npcid);
                        if ((npc != null) && (pu != null))
                        {
                            if ((npc.PEnvir == this.PEnvir) && (Math.Abs(npc.CX - this.CX) <= 15) && (Math.Abs(npc.CY - this.CY) <= 15))
                            {
                                // 荐府茄促.
                                if (((TMerchant)npc).UserRepairItem(this, pu))
                                {
                                }
                            }
                        }
                        break;
                    }
                }
            }
        }

        private void ServerSendStorageItemList(int npcid)
        {
            int i;
            int j;
            string data = string.Empty;
            TUserItem pu;
            TStdItem ps;
            TStdItem std;
            TClientItem citem=null;
            int page;
            int startcount;
            int endcount;
            int maxcount;
            data = "";
            maxcount = this.SaveItems.Count;
            page = maxcount / 50;
            for (j = 0; j <= page; j++)
            {
                startcount = j * 50;
                endcount = startcount + 50;
                if (endcount > maxcount)
                {
                    endcount = maxcount;
                }
                data = "";
                for (i = startcount; i < endcount; i++)
                {
                    pu = (TUserItem)this.SaveItems[i];
                    ps = svMain.UserEngine.GetStdItem(pu.Index);
                    if (ps != null)
                    {
                        std = ps;
                        svMain.ItemMan.GetUpgradeStdItem(pu, ref std);
                        //FillChar(citem.Desc, sizeof(citem.Desc), '\0');
                        Move(pu.Desc, citem.Desc, sizeof(pu.Desc));
                        citem.S = std;
                        citem.Dura = pu.Dura;
                        citem.DuraMax = pu.DuraMax;
                        citem.MakeIndex = pu.MakeIndex;
                        data = data + EDcode.EncodeBuffer(citem) + "/";
                    }
                }
                // , SaveItems.Count
                // 荐樊
                Def = Grobal2.MakeDefaultMsg(Grobal2.SM_SAVEITEMLIST, npcid, 0, j, page);
                SendSocket(Def, data);
            }
        }

        public int ServerGetUserStorageItem_SaveCountItemAdd(TUserItem uitem, int cnt, ref TUserItem bak_ui)
        {
            int result;
            // gadget
            int i;
            short total;
            TStdItem ps;
            TStdItem ps2;
            result = 0;
            ps = svMain.UserEngine.GetStdItem(uitem.Index);
            if (ps != null)
            {
                for (i = 0; i < this.SaveItems.Count; i++)
                {
                    ps2 = svMain.UserEngine.GetStdItem(((TUserItem)this.SaveItems[i]).Index);
                    if (ps2 != null)
                    {
                        if ((ps.StdMode == ps2.StdMode) && (ps.Looks == ps2.Looks) && (ps2.OverlapItem >= 1))
                        {
                            if (ps.Name.ToLower().CompareTo(ps2.Name.ToLower()) == 0)
                            {
                                if (((TUserItem)this.SaveItems[i]).Dura + cnt <= 1000)
                                {
                                    bak_ui.Index = ((TUserItem)this.SaveItems[i]).Index;
                                    // bug fix (sonmg 2005/01/07)
                                    bak_ui.MakeIndex = ((TUserItem)this.SaveItems[i]).MakeIndex;
                                    // bug fix (sonmg 2005/01/07)
                                    total = (short)(((TUserItem)this.SaveItems[i]).Dura + cnt);
                                    ((TUserItem)this.SaveItems[i]).Dura = (ushort)total;
                                    result = 1;
                                    break;
                                }
                                else
                                {
                                    result = 2;
                                    // 墨款飘 酒捞袍 俺荐 力茄俊 吧覆
                                }
                            }
                        }
                    }
                }
            }
            return result;
        }

        private void ServerGetUserStorageItem(int npcid, int itemindex, int count, string itemname)
        {
            int i;
            int remain;
            TCreature npc;
            TUserItem pu;
            TUserItem newpu;
            TUserItem bak_ui;
            TStdItem pstd;
            bool flag;
            int iRetVal;
            string countstr;
            pu = null;
            newpu = null;
            remain = 0;
            countstr = "";
            // 郴 啊规狼 酒捞袍俊辑 itemindex狼 酒捞袍阑 茫绰促.
            flag = false;
            if (itemname.IndexOf(" ") >= 0)
            {
               HUtil32.GetValidStr3(itemname, ref itemname, new string[] { " " });
            }
            if (ApprovalMode != 1)
            {
                for (i = 0; i < this.ItemList.Count; i++)
                {
                    if (this.ItemList[i].MakeIndex == itemindex)
                    {
                        if (svMain.UserEngine.GetStdItemName(this.ItemList[i].Index).ToLower().CompareTo(itemname.ToLower()) == 0)
                        {
                            pu = this.ItemList[i];
                            npc = svMain.UserEngine.GetMerchant(npcid);
                            pstd = svMain.UserEngine.GetStdItem(pu.Index);
                            if ((npc != null) && (pu != null) && (pstd != null))
                            {
                                if ((npc.PEnvir == this.PEnvir) && (Math.Abs(npc.CX - this.CX) <= 15) && (Math.Abs(npc.CY - this.CY) <= 15))
                                {
                                    if (pstd.StdMode != ObjBase.TAIWANEVENTITEM)
                                    {
                                        if (pstd.OverlapItem >= 1)
                                        {
                                            if (count > 1000)
                                            {
                                                break;
                                            }
                                            if (count <= 0)
                                            {
                                                count = 1;
                                            }
                                            if (count > pu.Dura)
                                            {
                                                count = pu.Dura;
                                            }
                                            remain = pu.Dura - count;
                                            countstr = "(" + count.ToString() + ")";
                                            // 肺弊甫 困茄 酒捞袍 俺荐(sonmg 2005/01/07)
                                            iRetVal = ServerGetUserStorageItem_SaveCountItemAdd(pu, count, ref bak_ui);
                                            if (iRetVal == 1)
                                            {
                                                pu.Dura = (ushort)remain;
                                                flag = true;
                                            }
                                            else if (iRetVal == 2)
                                            {
                                                break;
                                            }
                                            else
                                            {
                                                // 货肺款 酒捞袍 眠啊
                                                if (this.SaveItems.Count < ObjBase.MAXSAVELIMIT)
                                                {
                                                    newpu = new TUserItem();
                                                    if (svMain.UserEngine.CopyToUserItemFromName(itemname, ref newpu))
                                                    {
                                                        newpu.Dura = (ushort)count;
                                                        this.SaveItems.Add(newpu);
                                                        pu.Dura = (ushort)remain;
                                                        flag = true;
                                                    }
                                                    else
                                                    {
                                                        Dispose(newpu);
                                                    }
                                                }
                                            }
                                            if (pu.Dura == 0)
                                            {
                                                Dispose(this.ItemList[i]);
                                                // memory leak
                                                this.ItemList.RemoveAt(i);
                                                pu = newpu;
                                                // bug fix (sonmg 2005/01/07)
                                            }
                                        }
                                        else
                                        {
                                            if (this.SaveItems.Count < ObjBase.MAXSAVELIMIT)
                                            {
                                                this.SaveItems.Add(pu);
                                                // 焊包
                                                this.ItemList.RemoveAt(i);
                                                flag = true;
                                            }
                                        }
                                        if (flag)
                                        {
                                            this.WeightChanged();
                                            SendDefMessage(Grobal2.SM_STORAGE_OK, 0, remain, count, 0, "");
                                            if (pu != null)
                                            {
                                                // 肺弊巢辫
                                                // 焊包_ +
                                                svMain.AddUserLog("1\09" + this.MapName + "\09" + this.CX.ToString() + "\09" + this.CY.ToString() + "\09" + this.UserName + "\09" + svMain.UserEngine.GetStdItemName(pu.Index) + "\09" + pu.MakeIndex.ToString() + "\09" + "1\09" + "0" + countstr);
                                                // 俺荐肺弊(sonmg 2005/01/07)
                                            }
                                            else
                                            {
                                                // 肺弊巢辫
                                                // 焊包_ +
                                                svMain.AddUserLog("1\09" + this.MapName + "\09" + this.CX.ToString() + "\09" + this.CY.ToString() + "\09" + this.UserName + "\09" + svMain.UserEngine.GetStdItemName(bak_ui.Index) + "\09" + bak_ui.MakeIndex.ToString() + "\09" + "1\09" + "0" + countstr);
                                                // 俺荐肺弊(sonmg 2005/01/07)
                                            }
                                        }
                                        else
                                        {
                                            // 歹 捞惑 焊包 给窃
                                            SendDefMessage(Grobal2.SM_STORAGE_FULL, 0, 0, 0, 0, "");
                                        }
                                        flag = true;
                                    }
                                }
                            }
                            break;
                        }
                    }
                }
            }
            else
            {
                this.SysMsg("体验模式中您不能使用仓库服务。", 0);
            }
            if (!flag)
            {
                SendDefMessage(Grobal2.SM_STORAGE_FAIL, 0, 0, 0, 0, "");
            }
        }

        private void ServerGetTakeBackStorageItem(int npcid, int itemserverindex, int TakeBackCnt, string iname)
        {
            int I;
            bool flag;
            TUserItem pu;
            TUserItem newpu;
            TUserItem bak_ui;
            TCreature npc;
            TStdItem ps;
            int remain;
            int CheckWeight;
            string countstr;
            remain = 0;
            countstr = "";
            flag = false;
            if (ApprovalMode != 1)
            {
                // 眉氰葛靛绰 拱扒阑 给 茫绰促.
                for (I = 0; I < this.SaveItems.Count; I++)
                {
                    if (((TUserItem)this.SaveItems[I]).MakeIndex == itemserverindex)
                    {
                        if (svMain.UserEngine.GetStdItemName(((TUserItem)this.SaveItems[I]).Index).ToLower().CompareTo(iname.ToLower()) == 0)
                        {
                            pu = (TUserItem)this.SaveItems[I];
                            npc = svMain.UserEngine.GetMerchant(npcid);
                            if ((npc != null) && (pu != null))
                            {
                                ps = svMain.UserEngine.GetStdItem(pu.Index);
                                if (ps != null)
                                {
                                    // 墨款飘酒捞袍
                                    if (ps.OverlapItem == 1)
                                    {
                                        CheckWeight = ps.Weight + ps.Weight * (TakeBackCnt / 10);
                                    }
                                    else if (ps.OverlapItem >= 2)
                                    {
                                        CheckWeight = ps.Weight * TakeBackCnt;
                                    }
                                    else
                                    {
                                        CheckWeight = ps.Weight;
                                    }
                                    if (this.IsAddWeightAvailable(CheckWeight))
                                    {
                                        if ((npc.PEnvir == this.PEnvir) && (Math.Abs(npc.CX - this.CX) <= 15) && (Math.Abs(npc.CY - this.CY) <= 15))
                                        {
                                            // 该变拱扒阑 茫绰促.
                                            if (ps == null)
                                            {
                                                break;
                                            }
                                            if (ps.OverlapItem >= 1)
                                            {
                                                // gadget:墨款飘酒捞袍
                                                if (TakeBackCnt <= 0)
                                                {
                                                    TakeBackCnt = 1;
                                                }
                                                if (TakeBackCnt > pu.Dura)
                                                {
                                                    TakeBackCnt = pu.Dura;
                                                }
                                                remain = pu.Dura - TakeBackCnt;
                                                countstr = "(" + TakeBackCnt.ToString() + ")";
                                                // 肺弊甫 困茄 酒捞袍 俺荐(sonmg 2005/01/07)
                                                if (this.UserCounterItemAdd(ps.StdMode, ps.Looks, TakeBackCnt, ps.Name, false))
                                                {
                                                    if (remain > 0)
                                                    {
                                                        // memory leak
                                                        pu.Dura = (ushort)remain;
                                                    }
                                                    else
                                                    {
                                                        bak_ui.Index = pu.Index;
                                                        // bug fix (sonmg 2005/01/07)
                                                        bak_ui.MakeIndex = pu.MakeIndex;
                                                        // bug fix (sonmg 2005/01/07)
                                                        Dispose((TUserItem)this.SaveItems[I]);
                                                        this.SaveItems.RemoveAt(I);
                                                        pu = null;
                                                    }
                                                }
                                                else
                                                {
                                                    newpu = new TUserItem();
                                                    if (svMain.UserEngine.CopyToUserItemFromName(iname, ref newpu))
                                                    {
                                                        newpu.Dura = (ushort)TakeBackCnt;
                                                        if (this.AddItem(newpu))
                                                        {
                                                            SendAddItem(newpu);
                                                            if (remain > 0)
                                                            {
                                                                // memory leak
                                                                pu.Dura = (ushort)remain;
                                                            }
                                                            else
                                                            {
                                                                if (newpu != null)
                                                                {
                                                                    pu = newpu;
                                                                }
                                                                // bug fix (sonmg 2005/01/07)
                                                                Dispose((TUserItem)this.SaveItems[I]);
                                                                this.SaveItems.RemoveAt(I);
                                                            }
                                                        }
                                                        else
                                                        {
                                                            Dispose(newpu);
                                                            // Memory Leak sonmg
                                                            SendDefMessage(Grobal2.SM_TAKEBACKSTORAGEITEM_FULLBAG, 0, 0, 0, 0, "");
                                                            // 啊规 菜 谩澜
                                                            break;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        Dispose(newpu);
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                if (this.AddItem(pu))
                                                {
                                                    // 啊规栏肺 颗扁绊
                                                    SendAddItem(pu);
                                                    // 努扼捞攫飘俊 焊晨
                                                    this.SaveItems.RemoveAt(I);
                                                }
                                                else
                                                {
                                                    SendDefMessage(Grobal2.SM_TAKEBACKSTORAGEITEM_FULLBAG, 0, 0, 0, 0, "");
                                                    // 啊规 菜 谩澜
                                                    break;
                                                }
                                            }
                                            SendDefMessage(Grobal2.SM_TAKEBACKSTORAGEITEM_OK, itemserverindex, remain, TakeBackCnt, 0, "");
                                            if (pu != null)
                                            {
                                                // 肺弊巢辫
                                                // 茫扁_ +
                                                svMain.AddUserLog("0\09" + this.MapName + "\09" + this.CX.ToString() + "\09" + this.CY.ToString() + "\09" + this.UserName + "\09" + svMain.UserEngine.GetStdItemName(pu.Index) + "\09" + pu.MakeIndex.ToString() + "\09" + "1\09" + "0" + countstr);
                                            }
                                            else
                                            {
                                                // 肺弊巢辫
                                                // 茫扁_ +
                                                svMain.AddUserLog("0\09" + this.MapName + "\09" + this.CX.ToString() + "\09" + this.CY.ToString() + "\09" + this.UserName + "\09" + svMain.UserEngine.GetStdItemName(bak_ui.Index) + "\09" + bak_ui.MakeIndex.ToString() + "\09" + "1\09" + "0" + countstr);
                                            }
                                            flag = true;
                                        }
                                    }
                                    else
                                    {
                                        this.SysMsg("无法携带更多东西。", 0);
                                    }
                                    this.WeightChanged();
                                }
                            }
                            break;
                        }
                    }
                }
            }
            else
            {
                this.SysMsg("体验模式中您不能使用仓库服务。", 0);
            }
            if (!flag)
            {
                SendDefMessage(Grobal2.SM_TAKEBACKSTORAGEITEM_FAIL, 0, 0, 0, 0, "");
            }
            // 啊规 菜 谩澜

        }

        private void ServerGetUserMenuBuy(int msg, int npcid, int MakeIndex, int menuindex, string itemname)
        {
            int i;
            int ii;
            int nPrice;
            int nPriceType;
            TCreature npc;
            TUserHuman who;
            TVisibleActor pva;
            long maxGold;
            TUserItem UserItem;
            TStallInfo StallInfo;
            TStdItem pstd;
            if (this.BoDealing || StallMgr.OnSale || this.Death)
            {
                return;
            }
            // 背券吝俊绰 拱扒阑 混 荐 绝促.
            if (npcid == 0)
            {
                return;
            }
            // if TObject(npcid) = m_LastNPC then begin
            // goto labNpc;
            // end;
            who = null;
            for (i = 0; i < this.VisibleActors.Count; i++)
            {
                pva = this.VisibleActors[i];
                if (pva.cret == (npcid as Object))
                {
                    who = pva.cret as TUserHuman;
                    break;
                }
            }
            if ((who != null) && (who.RaceServer == Grobal2.RC_USERHUMAN) && who.StallMgr.OnSale)
            {
                if ((who == this) || who.Death || who.BoGhost || (Math.Abs(who.CX - this.CX) > 15) || (Math.Abs(who.CY - this.CY) > 15))
                {
                    return;
                }
                if (msg == Grobal2.CM_USERBUYITEM)
                {
                    if (MakeIndex == 0)
                    {
                        return;
                    }
                    nPrice = -1;
                    for (i = who.StallMgr.mBlock.Items.GetLowerBound(0); i <= who.StallMgr.mBlock.Items.GetUpperBound(0); i++)
                    {
                        if (who.StallMgr.mBlock.Items[i].S.Name == "")
                        {
                            continue;
                        }
                        if (who.StallMgr.mBlock.Items[i].MakeIndex == MakeIndex)
                        {
                            nPrice = who.StallMgr.mBlock.Items[i].S.Price;
                            nPriceType = who.StallMgr.mBlock.Items[i].S.NeedIdentify;
                            break;
                        }
                    }
                    if (nPrice == -1)
                    {
                        SendDefMessage(Grobal2.SM_BUYSTALLITEM, -1, 0, 0, 0, "");
                        return;
                    }
                    switch (nPriceType)
                    {
                        case 4:
                            maxGold = (long)who.Gold + nPrice;
                            // g_Config.nHumanMaxGold
                            if (maxGold > 50000000)
                            {
                                SendDefMessage(Grobal2.SM_BUYSTALLITEM, -2, 0, 0, 0, who.UserName);
                                return;
                            }
                            if (this.Gold < nPrice)
                            {
                                SendDefMessage(Grobal2.SM_BUYSTALLITEM, -3, 0, 0, 0, who.StallMgr.mBlock.Items[i].S.Name);
                                return;
                            }
                            break;
                        case 5:
                            maxGold = (long)who.GameGold + nPrice;
                            if (maxGold > Int32.MaxValue)
                            {
                                SendDefMessage(Grobal2.SM_BUYSTALLITEM, -4, 0, 0, 0, who.UserName);
                                return;
                            }
                            if (this.GameGold < nPrice)
                            {
                                SendDefMessage(Grobal2.SM_BUYSTALLITEM, -5, 0, 0, 0, who.StallMgr.mBlock.Items[i].S.Name);
                                return;
                            }
                            break;
                        default:
                            return;
                            break;
                    }
                    for (i = 0; i < who.ItemList.Count; i++)
                    {
                        UserItem = who.ItemList[i];
                        if (UserItem.MakeIndex == MakeIndex)
                        {
                            pstd = svMain.UserEngine.GetStdItem(UserItem.Index);
                            if (pstd == null)
                            {
                                SendDefMessage(Grobal2.SM_BUYSTALLITEM, -6, 0, 0, 0, "");
                                return;
                            }
                            if (pstd.Name.ToLower().CompareTo(itemname.ToLower()) != 0)
                            {
                                SendDefMessage(Grobal2.SM_BUYSTALLITEM, -6, 0, 0, 0, "");
                                return;
                            }
                            if (this.IsEnoughBag() && this.IsAddWeightAvailable(svMain.UserEngine.GetStdItemWeight(UserItem.Index, UserItem.Dura)))
                            {
                                if (nPriceType == 4)
                                {
                                    this.DecGold(nPrice);
                                    this.GoldChanged();
                                    // 捣昏_
                                    // '陛傈'
                                    svMain.AddUserLog("13\09" + this.MapName + "\09" + this.CX.ToString() + "\09" + this.CY.ToString() + "\09" + this.UserName + "\09" + Envir.NAME_OF_GOLD + "\09" + nPrice.ToString() + "\09" + "1\09" + who.UserName);
                                    // g_Config.nHumanMaxGold
                                    who.Gold = _MIN(50000000, who.Gold + nPrice);
                                    who.GoldChanged();
                                    svMain.AddUserLog("13\09" + who.MapName + "\09" + who.CX.ToString() + "\09" + who.CY.ToString() + "\09" + who.UserName + "\09" + Envir.NAME_OF_GOLD + "\09" + nPrice.ToString() + "\09" + "1" + "\09" + this.UserName);
                                }
                                else
                                {
                                    this.GameGold -= nPrice;
                                    this.GameGoldChanged();
                                    // 捣昏_
                                    // '陛傈'
                                    svMain.AddUserLog("13\09" + this.MapName + "\09" + this.CX.ToString() + "\09" + this.CY.ToString() + "\09" + this.UserName + "\09" + Envir.NAME_OF_MONEY + "\09" + nPrice.ToString() + "\09" + "1\09" + who.UserName);
                                    who.GameGold = _MIN(Int32.MaxValue, who.GameGold + nPrice);
                                    who.GameGoldChanged();
                                    svMain.AddUserLog("13\09" + who.MapName + "\09" + who.CX.ToString() + "\09" + who.CY.ToString() + "\09" + who.UserName + "\09" + Envir.NAME_OF_MONEY + "\09" + nPrice.ToString() + "\09" + "1" + "\09" + this.UserName);
                                }
                                if (pstd.NeedIdentify == 1)
                                {
                                    // AddGameDataLogAPI('9' + #9 +
                                    // m_sMapName + #9 +
                                    // IntToStr(CX) + #9 +
                                    // IntToStr(CY) + #9 +
                                    // m_sCharName + #9 +
                                    // pstd.Name + #9 +
                                    // IntToStr(UserItem.MakeIndex) + #9 +
                                    // '1' + #9 +
                                    // who.m_sCharName + ' (' + IntToStr(nPrice) + g_DealGoldType[nPriceType] + ')');
                                    // AddGameDataLogAPI('10' + #9 +
                                    // who.m_sMapName + #9 +
                                    // IntToStr(who.CX) + #9 +
                                    // IntToStr(who.CY) + #9 +
                                    // who.m_sCharName + #9 +
                                    // pstd.Name + #9 +
                                    // IntToStr(UserItem.MakeIndex) + #9 +
                                    // '1' + #9 +
                                    // m_sCharName + ' (' + IntToStr(nPrice) + g_DealGoldType[nPriceType] + ')');
                                }
                                this.AddItem(UserItem);
                                SendAddItem(UserItem);
                                // m_dwSaveRcdTick := 0;    //0720
                                this.WeightChanged();
                                who.SendDelItem(UserItem);
                                who.ItemList.RemoveAt(i);
                                who.WeightChanged();
                                who.LastSaveTime = 0;
                                // 0720
                                who.SysMsg(Format("%s花费%d%s购买了你的%s", new object[] { this.UserName, nPrice, ObjBase.g_DealGoldType[nPriceType], pstd.Name }), 2);
                                this.SysMsg(Format("你花费%d%s购买了%s的%s", new object[] { nPrice, ObjBase.g_DealGoldType[nPriceType], who.UserName, pstd.Name }), 2);
                                for (ii = who.StallMgr.mBlock.Items.GetLowerBound(0); ii <= who.StallMgr.mBlock.Items.GetUpperBound(0); ii++)
                                {
                                    if (who.StallMgr.mBlock.Items[ii].MakeIndex == MakeIndex)
                                    {
                                        who.StallMgr.mBlock.Items[ii].MakeIndex = 0;
                                        who.StallMgr.mBlock.Items[ii].S.Name = "";
                                        break;
                                    }
                                }
                                who.StallMgr.mBlock.ItemCount -= 1;
                                if (who.StallMgr.mBlock.ItemCount <= 0)
                                {
                                    who.StallMgr.OnSale = false;
                                    who.StallMgr.mBlock.StallName = "";
                                    who.StallMgr.mBlock.ItemCount = 0;
                                    StallInfo = new TStallInfo();
                                    //FillChar(who.StallMgr.mBlock.Items, sizeof(who.StallMgr.mBlock.Items), '\0');
                                    StallInfo.Open = false;
                                    StallInfo.Name = who.StallMgr.mBlock.StallName;
                                    StallInfo.Looks = who.StallMgr.StallType;
                                    who.Def = Grobal2.MakeDefaultMsg(Grobal2.SM_OPENSTALL, who.ActorId, who.CX, who.CY, who.Dir);
                                    who.SendSocket(who.Def, EDcode.EncodeBuffer(StallInfo));
                                    who.SendRefMsg(Grobal2.RM_STALLSTATUS, 0, who.CX, who.CY, who.Dir, "");
                                    if (svMain.DefaultNpc != null)
                                    {
                                        svMain.DefaultNpc.NpcSayTitle(this, "@StoreClosed");
                                    }
                                    return;
                                }
                                else
                                {
                                    // StallInfo.open := PlayObject.m_StallMgr.OnSale;
                                    // StallInfo.Looks := PlayObject.m_StallMgr.StallType;
                                    // StallInfo.Name := PlayObject.m_StallMgr.mBlock.StallName;
                                    // m_DefMsg := MakeDefaultMsg(SM_OPENStall, Integer(PlayObject), PlayObject.m_nCurrX, PlayObject.m_nCurrY, PlayObject.m_btDirection);
                                    // SendSocket(@m_DefMsg, EDcode.EncodeBuffer(@StallInfo));
                                    who.SendStallItems(this);
                                }
                            }
                            else
                            {
                                SendDefMessage(Grobal2.SM_BUYSTALLITEM, -7, 0, 0, 0, "");
                                return;
                            }
                            break;
                        }
                    }
                    return;
                }
                else
                {
                    return;
                }
                return;
            }
            npc = svMain.UserEngine.GetMerchant(npcid);
            if (npc != null)
            {
                if ((npc.PEnvir == this.PEnvir) && (Math.Abs(npc.CX - this.CX) <= 15) && (Math.Abs(npc.CY - this.CY) <= 15))
                {
                    if (msg == Grobal2.CM_USERBUYITEM)
                    {
                        if (menuindex > 0)
                        {
                            ((TMerchant)npc).UserBuyItem(this, itemname, MakeIndex, menuindex);
                        }
                        else
                        {
                            ((TMerchant)npc).UserBuyItem(this, itemname, MakeIndex, 1);
                        }
                    }
                    if (msg == Grobal2.CM_USERGETDETAILITEM)
                    {
                        ((TMerchant)npc).UserWantDetailItems(this, itemname, menuindex);
                    }
                }
            }
        }

        private void ServerGetMakeDrug(int npcid, string itemname)
        {
            TCreature npc;
            npc = svMain.UserEngine.GetMerchant(npcid);
            if (npc != null)
            {
                if ((npc.PEnvir == this.PEnvir) && (Math.Abs(npc.CX - this.CX) <= 15) && (Math.Abs(npc.CY - this.CY) <= 15))
                {
                    ((TMerchant)npc).UserMakeNewItem(this, itemname);
                }
            }
        }

        private void ServerGetMakeItemSel(int npcid, string itemname)
        {
            TCreature npc;
            npc = svMain.UserEngine.GetMerchant(npcid);
            if (npc != null)
            {
                if ((npc.PEnvir == this.PEnvir) && (Math.Abs(npc.CX - this.CX) <= 15) && (Math.Abs(npc.CY - this.CY) <= 15))
                {
                    ((TMerchant)npc).SayMakeItemMaterials(this, itemname);
                }
            }
        }

        private void ServerGetMakeItem(int npcid, string itemname)
        {
            TCreature npc;
            npc = svMain.UserEngine.GetMerchant(npcid);
            if (npc != null)
            {
                if ((npc.PEnvir == this.PEnvir) && (Math.Abs(npc.CX - this.CX) <= 15) && (Math.Abs(npc.CY - this.CY) <= 15))
                {
                    ((TMerchant)npc).UserManufactureItem(this, itemname);
                }
            }
        }

        public void RefreshGroupMembers()
        {
            int i;
            string data = string.Empty;
            TCreature cret;
            TUserHuman hum;
            data = "";
            for (i = 0; i < this.GroupMembers.Count; i++)
            {
                cret = this.GroupMembers.Values[i] as TCreature;
                data = data + cret.UserName + "/";
            }
            for (i = 0; i < this.GroupMembers.Count; i++)
            {
                hum = this.GroupMembers.Values[i] as TUserHuman;
                if (hum.RaceServer == Grobal2.RC_USERHUMAN)
                {
                    hum.SendDefMessage(Grobal2.SM_GROUPMEMBERS, 0, 0, 0, 0, data);
                }
                else
                {
                    svMain.MainOutMessage("ERROR NOT HUMAN RefreshGroupMember");
                }
            }
        }

        // 弊缝 父甸扁
        private void ServerGetCreateGroup(string withwho)
        {
            TUserHuman who;
            who = svMain.UserEngine.GetUserHuman(withwho);
            if (this.GroupOwner != null)
            {
                SendDefMessage(Grobal2.SM_CREATEGROUP_FAIL, -1, 0, 0, 0, "");
                return;
            }
            if ((who == null) || (who == this))
            {
                SendDefMessage(Grobal2.SM_CREATEGROUP_FAIL, -2, 0, 0, 0, "");
                return;
            }
            // 困摹 函版(2004/11/18)
            // 弊缝 救登绰 甘 加己 眠啊(sonmg 2004/10/13)
            if (this.PEnvir.NoGroup)
            {
                this.SysMsg("你所在的地图禁止组队。", 0);
                return;
            }
            // 弊缝 救登绰 甘 加己 眠啊(sonmg 2004/10/13)
            if (who.PEnvir.NoGroup)
            {
                this.SysMsg("你所在的地图禁止加入队伍。", 0);
                return;
            }
            // 2003/07/23
            if ((this.LoginSign == false) || (who.LoginSign == false))
            {
                SendDefMessage(Grobal2.SM_CREATEGROUP_FAIL, -2, 0, 0, 0, "");
                return;
            }
            if (who.GroupOwner != null)
            {
                SendDefMessage(Grobal2.SM_CREATEGROUP_FAIL, -3, 0, 0, 0, "");
                return;
            }
            if (!who.AllowGroup)
            {
                SendDefMessage(Grobal2.SM_CREATEGROUP_FAIL, -4, 0, 0, 0, "");
                return;
            }
            // //////////////////////////////////////////////////////////////
            // 40檬 捞惑 版苞窍搁 弊缝 夸没磊客 矫埃阑 檬扁拳 矫挪促.(sonmg)
            if ((HUtil32.GetTickCount() < who.GroupRequestTime) || (HUtil32.GetTickCount() - who.GroupRequestTime > 40 * 1000))
            {
                who.GroupRequester = "";
            }
            // 弊缝 夸没阑 罐绊 乐绰 吝(sonmg)
            if (who.GroupRequester != "")
            {
                SendDefMessage(Grobal2.SM_CREATEGROUP_FAIL, -5, 0, 0, 0, "");
                return;
            }
            // 弊缝 夸没磊 扁废
            who.GroupRequester = this.UserName;
            // 弊缝 夸没 罐篮 矫埃 扁废
            who.GroupRequestTime = GetCurrentTime;
            who.SendDefMessage(Grobal2.SM_CREATEGROUPREQ, 0, 0, 0, 0, this.UserName);
            return;
        }

        private void ServerGetCreateGroupRequestOk(string withwho)
        {
            TUserHuman who;
            who = svMain.UserEngine.GetUserHuman(withwho);
            if (who == null)
            {
                return;
            }
            if (who.GroupOwner != null)
            {
                who.SendDefMessage(Grobal2.SM_CREATEGROUP_FAIL, -1, 0, 0, 0, "");
                // 弊缝 夸没磊 秦力
                this.GroupRequester = "";
                return;
            }
            if ((who == null) || (who == this))
            {
                who.SendDefMessage(Grobal2.SM_CREATEGROUP_FAIL, -2, 0, 0, 0, "");
                // 弊缝 夸没磊 秦力
                this.GroupRequester = "";
                return;
            }
            // 困摹 函版(2004/11/18)
            // 弊缝 救登绰 甘 加己 眠啊(sonmg 2004/10/13)
            if (who.PEnvir.NoGroup)
            {
                who.SysMsg("你所在的地图禁止组队。", 0);
                // 弊缝 夸没磊 秦力
                this.GroupRequester = "";
                return;
            }
            // 弊缝 救登绰 甘 加己 眠啊(sonmg 2004/10/13)
            if (this.PEnvir.NoGroup)
            {
                who.SysMsg("你所在的地图禁止加入队伍。", 0);
                // 弊缝 夸没磊 秦力
                this.GroupRequester = "";
                return;
            }
            // 2003/07/23
            if ((who.LoginSign == false) || (this.LoginSign == false))
            {
                who.SendDefMessage(Grobal2.SM_CREATEGROUP_FAIL, -2, 0, 0, 0, "");
                // 弊缝 夸没磊 秦力
                this.GroupRequester = "";
                return;
            }
            if (who.GroupOwner != null)
            {
                who.SendDefMessage(Grobal2.SM_CREATEGROUP_FAIL, -3, 0, 0, 0, "");
                // 弊缝 夸没磊 秦力
                this.GroupRequester = "";
                return;
            }
            if (!this.AllowGroup)
            {
                who.SendDefMessage(Grobal2.SM_CREATEGROUP_FAIL, -4, 0, 0, 0, "");
                // 弊缝 夸没磊 秦力
                this.GroupRequester = "";
                return;
            }
            // //////////////////////////////////////////////////////////////
            // 颇扼固磐肺 柯 弊缝 夸没磊啊 历厘等 夸没磊客 促福搁 角菩(sonmg)
            if ((this.GroupRequester == "") || (withwho != this.GroupRequester))
            {
                who.SendDefMessage(Grobal2.SM_CREATEGROUP_FAIL, -2, 0, 0, 0, "");
                // 弊缝 夸没磊 秦力
                this.GroupRequester = "";
                return;
            }
            // 弊缝 夸没磊 秦力
            this.GroupRequester = "";
            // //////////////////////////////////////////////////////////////
            who.GroupMembers.Clear();
            who.GroupMembers.Add(withwho, who);
            who.GroupMembers.Add(this.UserName, this);
            who.EnterGroup(who);
            this.EnterGroup(who);
            who.AllowGroup = true;
            who.SendDefMessage(Grobal2.SM_CREATEGROUP_OK, 0, 0, 0, 0, "");
            who.RefreshGroupMembers();
        }

        private void ServerGetCreateGroupRequestFail()
        {
            TUserHuman who;
            who = svMain.UserEngine.GetUserHuman(this.GroupRequester);
            // 弊缝 夸没磊 秦力
            this.GroupRequester = "";
            if (who == null)
            {
                return;
            }
            // 弊缝 芭何 皋矫瘤
            who.SysMsg(this.UserName + "拒绝加入队伍。", 3);
        }

        // 弊缝俊 曼啊
        private void ServerGetAddGroupMember(string withwho)
        {
            TUserHuman who;
            int i;
            who = svMain.UserEngine.GetUserHuman(withwho);
            if (this.GroupOwner != this)
            {
                SendDefMessage(Grobal2.SM_GROUPADDMEM_FAIL, -1, 0, 0, 0, "");
                return;
            }
            if (this.GroupMembers.Count >= ObjBase.GROUPMAX)
            {
                SendDefMessage(Grobal2.SM_GROUPADDMEM_FAIL, -5, 0, 0, 0, "");
                // full
                return;
            }
            if ((who == null) || (who == this))
            {
                SendDefMessage(Grobal2.SM_GROUPADDMEM_FAIL, -2, 0, 0, 0, "");
                return;
            }
            // 2003/07/23
            if ((this.LoginSign == false) || (who.LoginSign == false))
            {
                SendDefMessage(Grobal2.SM_GROUPADDMEM_FAIL, -2, 0, 0, 0, "");
                return;
            }
            // 2003/05/02 弊缝 吝汗 滚弊 菩摹
            for (i = 0; i < this.GroupMembers.Count; i++)
            {
                // 滚弊菩摹
                // PDS -- Nil Check
                if (this.GroupMembers.Values[i] == null)
                {
                    svMain.MainOutMessage("ERROR: GROUP MEMBER IS NIL");
                }
                else
                {
                    if ((this.GroupMembers.Values[i] as TCreature).UserName.ToLower().CompareTo(who.UserName.ToLower()) == 0)
                    {
                        SendDefMessage(Grobal2.SM_GROUPADDMEM_FAIL, -3, 0, 0, 0, "");
                        return;
                    }
                }
            }
            if ((who.GroupOwner != null) || (who.LoginSign == false))
            {
                SendDefMessage(Grobal2.SM_GROUPADDMEM_FAIL, -3, 0, 0, 0, "");
                return;
            }
            if (!who.AllowGroup)
            {
                SendDefMessage(Grobal2.SM_GROUPADDMEM_FAIL, -4, 0, 0, 0, "");
                return;
            }
            if ((HUtil32.GetTickCount() < who.GroupRequestTime) || (HUtil32.GetTickCount() - who.GroupRequestTime > 40 * 1000))
            {
                who.GroupRequester = "";
            }
            if (who.GroupRequester != "")
            {
                SendDefMessage(Grobal2.SM_CREATEGROUP_FAIL, -5, 0, 0, 0, "");
                return;
            }
            who.GroupRequester = this.UserName;
            who.GroupRequestTime = GetCurrentTime;
            who.SendDefMessage(Grobal2.SM_ADDGROUPMEMBERREQ, 0, 0, 0, 0, this.UserName);
            return;
        }

        private void ServerGetAddGroupMemberRequestOk(string withwho)
        {
            TUserHuman who;
            int i;
            who = svMain.UserEngine.GetUserHuman(withwho);
            if (who == null)
            {
                return;
            }
            if (who.GroupOwner != who)
            {
                who.SendDefMessage(Grobal2.SM_GROUPADDMEM_FAIL, -1, 0, 0, 0, "");
                this.GroupRequester = "";
                return;
            }
            if (who.GroupMembers.Count >= ObjBase.GROUPMAX)
            {
                who.SendDefMessage(Grobal2.SM_GROUPADDMEM_FAIL, -5, 0, 0, 0, "");
                this.GroupRequester = "";
                return;
            }
            if ((who == null) || (who == this))
            {
                who.SendDefMessage(Grobal2.SM_GROUPADDMEM_FAIL, -2, 0, 0, 0, "");
                this.GroupRequester = "";
                return;
            }
            if ((who.LoginSign == false) || (this.LoginSign == false))
            {
                who.SendDefMessage(Grobal2.SM_GROUPADDMEM_FAIL, -2, 0, 0, 0, "");
                this.GroupRequester = "";
                return;
            }
            for (i = 0; i < who.GroupMembers.Count; i++)
            {
                if (who.GroupMembers.Values[i] == null)
                {
                    svMain.MainOutMessage("ERROR: GROUP MEMBER IS NIL");
                }
                else
                {
                    if ((who.GroupMembers.Values[i] as TCreature).UserName.ToLower().CompareTo(this.UserName.ToLower()) == 0)
                    {
                        who.SendDefMessage(Grobal2.SM_GROUPADDMEM_FAIL, -3, 0, 0, 0, "");
                        this.GroupRequester = "";
                        return;
                    }
                }
            }
            if ((this.GroupOwner != null) || (LoginSign == false))
            {
                who.SendDefMessage(Grobal2.SM_GROUPADDMEM_FAIL, -3, 0, 0, 0, "");
                this.GroupRequester = "";
                return;
            }
            if (!this.AllowGroup)
            {
                who.SendDefMessage(Grobal2.SM_GROUPADDMEM_FAIL, -4, 0, 0, 0, "");
                this.GroupRequester = "";
                return;
            }
            if ((this.GroupRequester == "") || (withwho != this.GroupRequester))
            {
                who.SendDefMessage(Grobal2.SM_CREATEGROUP_FAIL, -2, 0, 0, 0, "");
                this.GroupRequester = "";
                return;
            }
            this.GroupRequester = "";
            // //////////////////////////////////////////////////////////////
            who.GroupMembers.Add(this.UserName, this);
            this.EnterGroup(who);
            who.SendDefMessage(Grobal2.SM_GROUPADDMEM_OK, 0, 0, 0, 0, "");
            who.RefreshGroupMembers();
        }

        private void ServerGetAddGroupMemberRequestFail()
        {
            TUserHuman who;
            who = svMain.UserEngine.GetUserHuman(this.GroupRequester);
            // 弊缝 夸没磊 秦力
            this.GroupRequester = "";
            if (who == null)
            {
                return;
            }
            // 弊缝 芭何 皋矫瘤
            who.SysMsg(this.UserName + "拒绝加入队伍。", 3);
        }

        private void ServerGetDelGroupMember(string withwho)
        {
            TUserHuman who;
            who = svMain.UserEngine.GetUserHuman(withwho);
            if (this.GroupOwner != this)
            {
                SendDefMessage(Grobal2.SM_GROUPDELMEM_FAIL, -1, 0, 0, 0, "");
                return;
            }
            if (who == null)
            {
                SendDefMessage(Grobal2.SM_GROUPDELMEM_FAIL, -2, 0, 0, 0, "");
                return;
            }
            if (!this.IsGroupMember(who))
            {
                SendDefMessage(Grobal2.SM_GROUPDELMEM_FAIL, -3, 0, 0, 0, "");
                return;
            }
            this.DelGroupMember(who);
            SendDefMessage(Grobal2.SM_GROUPDELMEM_OK, 0, 0, 0, 0, withwho);
        }

        private void ServerGetDealTry(string withwho)
        {
            TCreature cret;
            if (StallMgr.OnSale)
            {
                this.SysMsg("摆摊状态不能交易！", 0);
                return;
            }
            if ((this.PEnvir != null) && this.PEnvir.NoDeal)
            {
                this.SendMsg(svMain.DefaultNpc, Grobal2.RM_MENU_OK, 0, this.ActorId, 0, 0, "[提示]：本地图禁止交易");
                return;
            }
            if (this.BoDealing)
            {
                return;
            }
            // 捞固 芭贰吝
            cret = this.GetFrontCret();
            if ((cret != null) && (cret != this) && (!cret.BoGhost) && (!cret.Death))
            {
                // 菊俊 穿啊 乐绢具窍绊
                if ((cret as TUserHuman).StallMgr.OnSale)
                {
                    this.SysMsg("对方处于摆摊状态，不能进行交易！", 0);
                    return;
                }
                if ((cret.GetFrontCret() == this) && (!cret.BoDealing))
                {
                    // 付林焊绊 乐绢具窍绊, 捞固 芭贰吝捞搁 救凳.
                    if (cret.RaceServer == Grobal2.RC_USERHUMAN)
                    {
                        if (cret.BoExchangeAvailable)
                        {
                            if (this.BoGuildAgitDealTry)
                            {
                                // 厘盔 芭贰 矫档(sonmg)
                                if (this.IsGuildMaster())
                                {
                                    // 巩林捞搁
                                    if (cret.IsGuildMaster())
                                    {
                                        // 惑措规 厘盔 芭贰 敲贰弊 眉农(sonmg)
                                        cret.BoGuildAgitDealTry = true;
                                        // 皋矫瘤 冠胶甫 剁款促.
                                        cret.BoxMsg(this.UserName + "和您开始门派庄园交易了。", 0);
                                        this.BoxMsg(cret.UserName + "和您开始门派庄园交易了。", 0);
                                        StartDeal(cret as TUserHuman);
                                        (cret as TUserHuman).StartDeal(this);
                                    }
                                    else
                                    {
                                        cret.SysMsg("仅门派门主才可以使用这个命令。", 0);
                                        this.SysMsg("这个人不是门派门主。", 0);
                                        this.BoGuildAgitDealTry = false;
                                        // 厘盔芭贰秒家.
                                    }
                                }
                                else
                                {
                                    this.SysMsg("仅门派门主才可以使用这个命令。", 0);
                                    cret.SysMsg("这个人不是门派门主。", 0);
                                    this.BoGuildAgitDealTry = false;
                                    // 厘盔芭贰秒家.
                                }
                            }
                            else
                            {
                                // 背券 促捞倔肺弊 焊辰促.
                                cret.SysMsg(this.UserName + "和您开始交易。", 1);
                                this.SysMsg(cret.UserName + "和您开始交易。", 1);
                                StartDeal(cret as TUserHuman);
                                (cret as TUserHuman).StartDeal(this);
                            }
                        }
                        else
                        {
                            this.SysMsg("对方拒绝和您交易。", 1);
                        }
                    }
                }
                else
                {
                    SendDefMessage(Grobal2.SM_DEALTRY_FAIL, 0, 0, 0, 0, "");
                }
            }
            else
            {
                SendDefMessage(Grobal2.SM_DEALTRY_FAIL, 0, 0, 0, 0, "");
            }
        }

        public void ResetDeal()
        {
            int i;
            TStdItem ps;
            if (this.DealList.Count > 0)
            {
                for (i = this.DealList.Count - 1; i >= 0; i--)
                {
                    ps = svMain.UserEngine.GetStdItem(this.DealList[i].Index);
                    if (ps == null)
                    {
                        continue;
                    }
                    // sonmg 眠啊
                    if (ps.OverlapItem <= 0)
                    {
                        this.ItemList.Add(this.DealList[i]);
                    }
                    // 弊贰肺 困摹 捞悼
                }
                this.DealList.Clear();
            }
            // Gold := Gold + DealGold;
            this.IncGold(this.DealGold);
            this.DealGold = 0;
            this.BoDealSelect = false;
        }

        public void StartDeal(TUserHuman who)
        {
            this.BoDealing = true;
            this.DealCret = who;
            ResetDeal();
            if (this.BoGuildAgitDealTry)
            {
                SendDefMessage(Grobal2.SM_GUILDAGITDEALMENU, 0, 0, 0, 0, who.UserName);
            }
            else
            {
                SendDefMessage(Grobal2.SM_DEALMENU, 0, 0, 0, 0, who.UserName);
            }
            this.DealItemChangeTime  =  HUtil32.GetTickCount();

        }

        public void BrokeDeal()
        {
            TUserItem pu;
            TStdItem ps;
            int i;
            if (this.BoDealEnding)
            {
                return;
            }
            if (this.BoDealing)
            {
                this.BoDealing = false;
                SendDefMessage(Grobal2.SM_DEALCANCEL, 0, 0, 0, 0, "");
                if (this.DealList.Count > 0)
                {
                    for (i = this.DealList.Count - 1; i >= 0; i--)
                    {
                        pu = this.DealList[i];
                        ps = svMain.UserEngine.GetStdItem(pu.Index);
                        if (ps == null)
                        {
                            continue;
                        }
                        if (ps.OverlapItem >= 1)
                        {
                            if (this.UserCounterItemAdd(ps.StdMode, ps.Looks, pu.Dura, ps.Name, true))
                            {

                            }
                            else
                            {
                                this.ItemList.Add(this.DealList[i]);
                                SendAddItem(this.DealList[i]);
                            }
                        }
                    }
                }
                if (this.DealCret != null)
                {
                    (this.DealCret as TUserHuman).DealCret = null;
                    if (this.DealCret != null)
                    {
                        (this.DealCret as TUserHuman).BrokeDeal();
                    }
                }
                this.DealCret = null;
                ResetDeal();
                this.SysMsg("交易取消。", 1);
                this.DealItemChangeTime  =  HUtil32.GetTickCount();
                this.BoGuildAgitDealTry = false;
                // 厘盔芭贰秒家.
            }
        }

        public void ServerGetDealCancel()
        {
            BrokeDeal();
        }

        public void AddDealItem(TUserItem uitem, int remain)
        {
            TClientItem citem=null;
            TStdItem ps;
            TStdItem std;
            SendDefMessage(Grobal2.SM_DEALADDITEM_OK, uitem.MakeIndex, remain, 0, 0, "");
            if (this.DealCret != null)
            {
                ps = svMain.UserEngine.GetStdItem(uitem.Index);
                if (ps != null)
                {
                    std = ps;
                    //FillChar(citem.Desc, sizeof(citem.Desc), '\0');
                    //Move(uitem.Desc, citem.Desc, sizeof(uitem.Desc));
                    svMain.ItemMan.GetUpgradeStdItem(uitem, ref std);
                    citem.S = std;
                    citem.MakeIndex = uitem.MakeIndex;
                    citem.Dura = uitem.Dura;
                    citem.DuraMax = uitem.DuraMax;
                }
                Def = Grobal2.MakeDefaultMsg(Grobal2.SM_DEALREMOTEADDITEM, this.ActorId, 0, 0, 1);
                (this.DealCret as TUserHuman).SendSocket(Def, EDcode.EncodeBuffer(citem));
                (this.DealCret as TUserHuman).DealItemChangeTime  =  HUtil32.GetTickCount();
                this.DealItemChangeTime  =  HUtil32.GetTickCount();
            }
        }

        public void DelDealItem(TUserItem uitem)
        {
            TClientItem citem=null;
            TStdItem ps;
            SendDefMessage(Grobal2.SM_DEALDELITEM_OK, 0, 0, 0, 0, "");
            if (this.DealCret != null)
            {
                ps = svMain.UserEngine.GetStdItem(uitem.Index);
                if (ps != null)
                {
                    citem.S = ps;
                    // citem.UpgradeOpt := 0;
                    //FillChar(citem.Desc, sizeof(citem.Desc), '\0');
                    //Move(uitem.Desc, citem.Desc, sizeof(uitem.Desc));
                    citem.MakeIndex = uitem.MakeIndex;
                    citem.Dura = uitem.Dura;
                    citem.DuraMax = uitem.DuraMax;
                }
                Def = Grobal2.MakeDefaultMsg(Grobal2.SM_DEALREMOTEDELITEM, this.ActorId, 0, 0, 1);
                (this.DealCret as TUserHuman).SendSocket(Def, EDcode.EncodeBuffer(citem));
                (this.DealCret as TUserHuman).DealItemChangeTime  =  HUtil32.GetTickCount();
                this.DealItemChangeTime  =  HUtil32.GetTickCount();
            }
        }

        // 墨款飘 酒捞袍.
        public void AddDealCounterItem(TUserItem uitem, int remain)
        {
            int i;
            TUserItem puAdd;
            TStdItem ps;
            TStdItem psAdd;
            puAdd = null;
            psAdd = null;
            if (this.DealCret != null)
            {
                psAdd = svMain.UserEngine.GetStdItem(uitem.Index);
                if (psAdd != null)
                {
                    for (i = 0; i < this.DealList.Count; i++)
                    {
                        ps = svMain.UserEngine.GetStdItem(this.DealList[i].Index);
                        if (ps == null)
                        {
                            continue;
                        }
                        if (ps.OverlapItem == 0)
                        {
                            continue;
                        }
                        if ((ps.StdMode == psAdd.StdMode) && (ps.Looks == psAdd.Looks) && (ps.OverlapItem >= 1))
                        {
                            if (ps.Name.ToLower().CompareTo(psAdd.Name.ToLower()) == 0)
                            {
                                puAdd = this.DealList[i];
                                break;
                            }
                        }
                    }
                    if (puAdd != null)
                    {
                        Def = Grobal2.MakeDefaultMsg(Grobal2.SM_COUNTERITEMCHANGE, puAdd.MakeIndex, puAdd.Dura, 0, 0);
                        (this.DealCret as TUserHuman).SendSocket(Def, EDcode.EncodeString(psAdd.Name));
                        (this.DealCret as TUserHuman).DealItemChangeTime  =  HUtil32.GetTickCount();
                        this.DealItemChangeTime  =  HUtil32.GetTickCount();
                    }
                }
            }
        }

        public void DelDealCounterItem(TUserItem uitem)
        {
            TStdItem ps;
            if (this.DealCret != null)
            {
                ps = svMain.UserEngine.GetStdItem(uitem.Index);
                if (ps != null)
                {
                    Def = Grobal2.MakeDefaultMsg(Grobal2.SM_COUNTERITEMCHANGE, uitem.MakeIndex, uitem.Dura, 0, 0);
                    (this.DealCret as TUserHuman).SendSocket(Def, EDcode.EncodeString(ps.Name));
                    (this.DealCret as TUserHuman).DealItemChangeTime  =  HUtil32.GetTickCount();
                    this.DealItemChangeTime  =  HUtil32.GetTickCount();
                }
            }
        }

        public bool IsReservedMakingSlave()
        {
            bool result;
            result = false;
            // 2003/06/12 浇饭捞宏 菩摹
            if (PrevServerSlaves.Count > 0)
            {
                // 家券 秦具且 何窍啊 乐澜
                result = true;
            }
            // 
            // for i:=0 to MsgList.Count-1 do begin
            // pmsg := MsgList[i];
            // if pmsg.Ident = RM_MAKE_SLAVE then begin
            // Result := TRUE;
            // break;
            // end;
            // end;
            // 

            return result;
        }

        private void ServerGetDealAddItem(int iidx, int count, string iname)
        {
            int i;
            int iOldCount;
            bool flag;
            TStdItem pstd;
            TStdItem psDeal;
            TUserItem newpu;
            int iRet;
            iRet = 0;
            if (this.DealCret != null)
            {
                if (iname.IndexOf(" ") >= 0)
                {
                    HUtil32.GetValidStr3(iname, ref iname, new string[] { " " });
                }
                flag = false;
                if (!this.DealCret.BoDealSelect)
                {
                    for (i = 0; i < this.ItemList.Count; i++)
                    {
                        pstd = svMain.UserEngine.GetStdItem(this.ItemList[i].Index);
                        if (pstd != null)
                        {
                            if ((pstd.UniqueItem & 0x08) != 0)
                            {
                                continue;
                            }
                            if (pstd.StdMode != ObjBase.TAIWANEVENTITEM)
                            {
                                if (this.ItemList[i].MakeIndex == iidx)
                                {
                                    if (svMain.UserEngine.GetStdItemName(this.ItemList[i].Index).ToLower().CompareTo(iname.ToLower()) == 0)
                                    {
                                        if (this.DealList.Count < ObjBase.MAXDEALITEM)
                                        {
                                            // 墨款飘 酒捞袍.
                                            if (pstd.OverlapItem >= 1)
                                            {
                                                if ((count > 0) && (count <= Grobal2.MAX_OVERLAPITEM))
                                                {
                                                    psDeal = svMain.UserEngine.GetStdItem(this.ItemList[i].Index);
                                                    iOldCount = this.ItemList[i].Dura;
                                                    iRet = this.UserCounterDealItemAdd(psDeal.StdMode, psDeal.Looks, count, psDeal.Name);
                                                    if (iRet == 1)
                                                    {
                                                        // Success
                                                        // Deal芒俊 秦寸 墨款飘 酒捞袍捞 乐栏搁...
                                                        // 老何 眠啊
                                                        if (iOldCount - count > 0)
                                                        {
                                                            // 啊规 芒俊 乐绰 酒捞袍狼 Count甫 皑家矫挪促.
                                                            this.ItemList[i].Dura = (ushort)(iOldCount - count);
                                                            AddDealCounterItem(this.ItemList[i], 0);
                                                            // 傈何 眠啊
                                                        }
                                                        else if (iOldCount - count == 0)
                                                        {
                                                            AddDealCounterItem(this.ItemList[i], 0);
                                                            // 啊规 芒俊 乐绰 酒捞袍 昏力
                                                            this.ItemList.RemoveAt(i);
                                                        }
                                                        else
                                                        {
                                                        }
                                                        flag = true;
                                                        this.CalcBagWeight();
                                                        break;
                                                        // MAX_OVERFLOW甫 逞菌阑 锭
                                                    }
                                                    else if (iRet == 2)
                                                    {
                                                        // Overflow
                                                        flag = false;
                                                        break;
                                                        // 弥措 酒捞袍 俺荐甫 逞菌阑 锭
                                                    }
                                                    else if (iRet == 3)
                                                    {
                                                        // OverCount
                                                        flag = false;
                                                        this.SysMsg(Grobal2.MAX_OVERLAPITEM.ToString() + "个是最大数量。", 0);
                                                        break;
                                                    }
                                                    else
                                                    {
                                                        // DealList俊 秦寸 墨款飘 酒捞袍捞 绝栏搁...
                                                        // 贸澜 老何 眠啊
                                                        if (iOldCount - count > 0)
                                                        {
                                                            newpu = new TUserItem();
                                                            if (svMain.UserEngine.CopyToUserItemFromName(iname, ref newpu))
                                                            {
                                                                newpu.Dura = (ushort)count;
                                                                this.DealList.Add(newpu);
                                                                this.ItemList[i].Dura = (ushort)(iOldCount - count);
                                                                AddDealItem(newpu, iOldCount - count);
                                                            }
                                                            else
                                                            {
                                                                Dispose(newpu);
                                                            }
                                                            // 贸澜 傈何 眠啊
                                                        }
                                                        else if (iOldCount - count == 0)
                                                        {
                                                            this.DealList.Add(this.ItemList[i]);
                                                            AddDealItem(this.ItemList[i], 0);
                                                            this.ItemList.RemoveAt(i);
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    // count啊 MAX_OVERLAPITEM焊促 农搁 皋矫瘤 焊晨.
                                                    if (count > Grobal2.MAX_OVERLAPITEM)
                                                    {
                                                        this.SysMsg(Grobal2.MAX_OVERLAPITEM.ToString() + "个是最大数量。", 0);
                                                    }
                                                    // count啊 0 捞窍捞搁 皋矫瘤甫 焊捞瘤 臼绊 弊成 狐廉唱皑.
                                                    break;
                                                }
                                                // 老馆 酒捞袍.
                                            }
                                            else
                                            {
                                                this.DealList.Add(this.ItemList[i]);
                                                AddDealItem(this.ItemList[i], 0);
                                                this.ItemList.RemoveAt(i);
                                            }
                                            flag = true;
                                            this.CalcBagWeight();
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                if (!flag)
                {
                    SendDefMessage(Grobal2.SM_DEALADDITEM_FAIL, 0, 0, 0, 0, "");
                }
            }
        }

        private void ServerGetDealDelItem(int iidx, string iname)
        {
            int i;
            bool flag;
            TUserItem pu;
            TStdItem ps;
            // 背券 惑措啊 菊俊 乐绰瘤, 绝栏搁 芭贰 秒家
            if (this.DealCret != null)
            {
                if (iname.IndexOf(" ") >= 0)
                {
                    HUtil32.GetValidStr3(iname, ref iname, new string[] { " " });
                }
                flag = false;
                if (!this.DealCret.BoDealSelect)
                {
                    for (i = 0; i < this.DealList.Count; i++)
                    {
                        pu = this.DealList[i];
                        if (pu.MakeIndex == iidx)
                        {
                            if (svMain.UserEngine.GetStdItemName(pu.Index).ToLower().CompareTo(iname.ToLower()) == 0)
                            {
                                ps = svMain.UserEngine.GetStdItem(pu.Index);
                                if (ps != null)
                                {
                                    // 墨款飘 酒捞袍.
                                    if (ps.OverlapItem >= 1)
                                    {
                                        // 鞍篮 辆幅狼 弥家 俺荐 酒捞袍俊 钦魂.
                                        // 甸绊 乐绰 酒捞袍篮 弥家 俺荐 酒捞袍 八荤俊辑 力寇.
                                        if (this.UserCounterItemAdd(ps.StdMode, ps.Looks, pu.Dura, ps.Name, true, pu.MakeIndex))
                                        {
                                            DelDealItem(pu);
                                            this.DealList.RemoveAt(i);
                                            flag = true;
                                            break;
                                        }
                                        else
                                        {
                                            this.ItemList.Add(this.DealList[i]);
                                            SendAddItem(this.DealList[i]);
                                            DelDealItem(pu);
                                            this.DealList.RemoveAt(i);
                                            flag = true;
                                            break;
                                        }
                                    }
                                    else
                                    {
                                        this.ItemList.Add(this.DealList[i]);
                                        DelDealItem(pu);
                                        this.DealList.RemoveAt(i);
                                        flag = true;
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }
                if (!flag)
                {
                    SendDefMessage(Grobal2.SM_DEALDELITEM_FAIL, 0, 0, 0, 0, "");
                }
            }
        }

        private void ServerGetDealChangeGold(int dgold)
        {
            bool flag;
            // sonmg 2005/06/22
            if (!this.BoDealing)
            {
                return;
            }
            if (dgold < 0)
            {
                SendDefMessage(Grobal2.SM_DEALCHGGOLD_FAIL, this.DealGold, HUtil32.LoWord(this.Gold), HUtil32.HiWord(this.Gold), 0, "");
                return;
            }
            if (this.DealGold > 0)
            {
                this.BoxMsg("你不能修改交易金币数量。", 1);
                return;
            }
            flag = false;
            if ((this.GetFrontCret() == this.DealCret) && (this.DealCret != null) && (this.UserName != this.DealCret.UserName))
            {
                if (!this.DealCret.BoDealSelect)
                {
                    // 惑措规捞 急琶 肯丰
                    if (this.Gold + this.DealGold >= dgold)
                    {
                        // self.Gold := (self.Gold + DealGold) - dgold;
                        this.IncGold(this.DealGold);
                        this.DecGold(dgold);
                        this.DealGold = dgold;
                        SendDefMessage(Grobal2.SM_DEALCHGGOLD_OK, this.DealGold, HUtil32.LoWord(this.Gold), HUtil32.HiWord(this.Gold), 0, "");
                        if (this.DealCret != null)
                        {
                            (this.DealCret as TUserHuman).SendDefMessage(Grobal2.SM_DEALREMOTECHGGOLD, this.DealGold, 0, 0, 0, "");
                            (this.DealCret as TUserHuman).DealItemChangeTime  =  HUtil32.GetTickCount();
                        }
                        flag = true;
                        this.DealItemChangeTime  =  HUtil32.GetTickCount();
                    }
                }
            }
            if (!flag)
            {
                SendDefMessage(Grobal2.SM_DEALCHGGOLD_FAIL, this.DealGold, HUtil32.LoWord(this.Gold), HUtil32.HiWord(this.Gold), 0, "");
            }
        }

        private void ServerGetDealEnd()
        {
            int i;
            TUserItem pu;
            TStdItem ps;
            bool flag;
            int agitnumber;
            this.BoDealEnding = true;
            this.BoDealSelect = true;
            // 背券 滚瓢阑 穿抚
            if (this.BoDealing && (this.DealCret != null))
            {
                if ((this.DealCret == null) || StallMgr.OnSale || (this.DealCret as TUserHuman).StallMgr.OnSale)
                {
                    return;
                }
                if ((HUtil32.GetTickCount() - this.DealItemChangeTime < 1000) || (HUtil32.GetTickCount() - this.DealCret.DealItemChangeTime < 1000))
                {
                    // 芭贰 流傈 1檬捞傈俊 拱扒狼 捞悼捞 乐菌澜.
                    this.SysMsg("您太快按下交易键了。", 0);
                    BrokeDeal();
                    // 芭贰啊 秒家
                    this.BoDealEnding = false;
                    return;
                }
                if (this.DealCret.BoDealSelect)
                {
                    // 笛促 穿抚, 背券 矫累..
                    flag = true;
                    // 郴啊 背券前阑 罐阑 父怒 啊规俊 冯捞 乐绰瘤 八荤..
                    if (Grobal2.MAXBAGITEM - this.ItemList.Count < (this.DealCret as TUserHuman).DealList.Count)
                    {
                        flag = false;
                    }
                    if (this.AvailableGold - this.Gold < (this.DealCret as TUserHuman).DealGold)
                    {
                        flag = false;
                    }
                    // 惑措啊 背券前阑 罐阑 父怒 啊规俊 冯捞 乐绰瘤 八荤..
                    if (Grobal2.MAXBAGITEM - (this.DealCret as TUserHuman).ItemList.Count < this.DealList.Count)
                    {
                        flag = false;
                    }
                    if ((this.DealCret as TUserHuman).AvailableGold - (this.DealCret as TUserHuman).Gold < this.DealGold)
                    {
                        flag = false;
                    }
                    if (flag)
                    {
                        // 厘盔 芭贰.
                        if (this.BoGuildAgitDealTry)
                        {
                            agitnumber = ExecuteGuildAgitTrade();
                            if (agitnumber > -1)
                            {
                                // 肺弊巢辫
                                // 厘芭贰_
                                svMain.AddUserLog("41\09" + this.MapName + "\09" + this.CX.ToString() + "\09" + this.CY.ToString() + "\09" + this.UserName + "\09" + ((TGuild)this.MyGuild).GuildName + "\09" + agitnumber.ToString() + "\09" + "1\09" + this.DealCret.UserName);
                                // TGuild(DealCret.MyGuild).GuildName);
                                svMain.UserEngine.SendInterMsg(Grobal2.ISM_RELOADGUILDAGIT, svMain.ServerIndex, "");
                                this.BoGuildAgitDealTry = false;
                                // 厘盔芭贰肯丰.
                            }
                            else
                            {
                                BrokeDeal();
                                // 芭贰啊 秒家
                                this.BoDealEnding = false;
                                return;
                            }
                        }
                        // 惑措啊 芭贰吝牢瘤 八荤(sonmg 2006/03/06)
                        if (this.DealCret.BoDealing)
                        {
                            // 郴 背券前阑 惑措俊霸 淋.
                            for (i = 0; i < this.DealList.Count; i++)
                            {
                                pu = this.DealList[i];
                                (this.DealCret as TUserHuman).AddItem(pu);
                                (this.DealCret as TUserHuman).SendAddItem(pu);
                                ps = svMain.UserEngine.GetStdItem(pu.Index);
                                if (ps != null)
                                {
                                    // 肺弊巢辫
                                    if (!M2Share.IsCheapStuff(ps.StdMode))
                                    {
                                        // 背券_ +
                                        svMain.AddUserLog("8\09" + this.MapName + "\09" + this.CX.ToString() + "\09" + this.CY.ToString() + "\09" + this.UserName + "\09" + svMain.UserEngine.GetStdItemName(pu.Index) + "\09" + pu.MakeIndex.ToString() + "\09" + "1\09" + this.DealCret.UserName);
                                    }
                                }
                            }
                            if (this.DealGold > 0)
                            {
                                // DealCret.Gold := DealCret.Gold + DealGold;
                                this.DealCret.IncGold(this.DealGold);
                                this.DealCret.GoldChanged();
                                // 肺弊巢辫
                                // 背券_ +
                                // '陛傈'
                                svMain.AddUserLog("8\09" + this.MapName + "\09" + this.CX.ToString() + "\09" + this.CY.ToString() + "\09" + this.UserName + "\09" + Envir.NAME_OF_GOLD + "\09" + this.DealGold.ToString() + "\09" + "1\09" + this.DealCret.UserName);
                            }
                            // 惑措狼 背券前阑 爱绰促.
                            for (i = 0; i < this.DealCret.DealList.Count; i++)
                            {
                                pu = this.DealCret.DealList[i];
                                this.AddItem(pu);
                                SendAddItem(pu);
                                ps = svMain.UserEngine.GetStdItem(pu.Index);
                                if (ps != null)
                                {
                                    // 肺弊巢辫
                                    if (!M2Share.IsCheapStuff(ps.StdMode))
                                    {
                                        // 背券_ +
                                        svMain.AddUserLog("8\09" + this.DealCret.MapName + "\09" + this.DealCret.CX.ToString() + "\09" + this.DealCret.CY.ToString() + "\09" + this.DealCret.UserName + "\09" + svMain.UserEngine.GetStdItemName(pu.Index) + "\09" + pu.MakeIndex.ToString() + "\09" + "1\09" + this.UserName);
                                    }
                                }
                            }
                            if (this.DealCret.DealGold > 0)
                            {
                                // Gold := Gold + DealCret.DealGold;
                                this.IncGold(this.DealCret.DealGold);
                                this.GoldChanged();
                                // 肺弊巢辫
                                // 背券_ +
                                // '陛傈'
                                svMain.AddUserLog("8\09" + this.DealCret.MapName + "\09" + this.DealCret.CX.ToString() + "\09" + this.DealCret.CY.ToString() + "\09" + this.DealCret.UserName + "\09" + Envir.NAME_OF_GOLD + "\09" + this.DealCret.DealGold.ToString() + "\09" + "1\09" + this.UserName);
                            }
                        }
                        TUserHuman _wvar1 = this.DealCret as TUserHuman;
                        _wvar1.SendDefMessage(Grobal2.SM_DEALSUCCESS, 0, 0, 0, 0, "");
                        _wvar1.SysMsg("交易已经完成。", 1);
                        _wvar1.DealCret = null;
                        _wvar1.BoDealing = false;
                        _wvar1.DealList.Clear();
                        _wvar1.DealGold = 0;
                        _wvar1.WeightChanged();
                        // 公霸 函悼 馆康(2004/08/30)
                        SendDefMessage(Grobal2.SM_DEALSUCCESS, 0, 0, 0, 0, "");
                        this.SysMsg("交易已经完成。", 1);
                        this.DealCret = null;
                        this.BoDealing = false;
                        this.DealList.Clear();
                        this.DealGold = 0;
                        this.WeightChanged();
                        // 公霸 函悼 馆康(2004/08/30)
                    }
                    else
                    {
                        BrokeDeal();
                        // 芭贰啊 秒家
                    }
                }
                else
                {
                    this.SysMsg("请等待对方确认交易。", 1);
                    this.DealCret.SysMsg("对方请求确认交易，确定完成交易然后按[成交]按钮。", 1);
                }
            }
            this.BoDealEnding = false;
        }

        private void ServerGetWantMiniMap()
        {
            int mini;
            mini = this.PEnvir.MiniMap;
            if (mini > 0)
            {
                SendDefMessage(Grobal2.SM_READMINIMAP_OK, 0, mini, 0, 0, "");
            }
            else
            {
                SendDefMessage(Grobal2.SM_READMINIMAP_FAIL, 0, 0, 0, 0, "");
            }
        }

        private void SendChangeGuildName()
        {
            if (this.MyGuild != null)
            {
                SendDefMessage(Grobal2.SM_CHANGEGUILDNAME, 0, 0, 0, 0, ((TGuild)this.MyGuild).GuildName + "/" + this.GuildRankName);
            }
            else
            {
                SendDefMessage(Grobal2.SM_CHANGEGUILDNAME, 0, 0, 0, 0, "");
            }
        }

        private void ServerGetQueryUserState(TCreature who, int xx, int yy)
        {
            int i;
            TUserStateInfo ustate;
            TStdItem ps;
            TStdItem std;
            TClientItem citem=null;
            TCreature backupWho;
            int opt;
            try
            {
                if (this.CretInNearXY(who, xx, yy))
                {
                    backupWho = who;
                    // 盒脚
                    if ((who.RaceServer == Grobal2.RC_CLONE) && (who.Master != null))
                    {
                        if (who.Master.RaceServer == Grobal2.RC_USERHUMAN)
                        {
                            backupWho = who.Master;
                        }
                        else
                        {
                            svMain.MainOutMessage("ERROR WANT STATE NOT HUMAN");
                            return;
                        }
                    }
                    //FillChar(ustate, sizeof(TUserStateInfo), '\0');
                    ustate.Feature = backupWho.GetRelFeature(this);
                    ustate.UserName = backupWho.UserName;
                    ustate.NameColor = this.GetThisCharColor(backupWho);
                    if (backupWho.MyGuild != null)
                    {
                        ustate.GuildName = ((TGuild)backupWho.MyGuild).GuildName;
                    }
                    ustate.GuildRankName = backupWho.GuildRankName;
                    ustate.bExistLover = (bool)(backupWho as TUserHuman).fLover.GetLoverCount;
                    // 楷牢 惑怕
                    ustate.LoverName = (backupWho as TUserHuman).fLover.GetLoverName;
                    // 楷牢 捞抚
                    // 2003/03/15 酒捞袍 牢亥配府 犬厘
                    for (i = 0; i <= 12; i++)
                    {
                        // 8->12
                        if (backupWho.UseItems[i].Index > 0)
                        {
                            ps = svMain.UserEngine.GetStdItem(backupWho.UseItems[i].Index);
                            if (ps != null)
                            {
                                std = ps;
                                opt = svMain.ItemMan.GetUpgradeStdItem(backupWho.UseItems[i], ref std);
                                //Move(std, citem.S, sizeof(TStdItem));
                                citem.MakeIndex = backupWho.UseItems[i].MakeIndex;
                                citem.Dura = backupWho.UseItems[i].Dura;
                                citem.DuraMax = backupWho.UseItems[i].DuraMax;
                                // citem.UpgradeOpt := opt;
                                // FillChar(citem.Desc, sizeof(citem.Desc), '\0');
                                //Move(backupWho.UseItems[i].Desc, citem.Desc, sizeof(backupWho.UseItems[i].Desc));
                                // 玫狼公豪
                                if (i == Grobal2.U_DRESS)
                                {
                                    backupWho.ChangeItemWithLevel(ref citem, backupWho.Abil.Level);
                                }
                                // 侩酒捞袍老 版快 瓷仿摹啊 官诧促.
                                backupWho.ChangeItemByJob(ref citem, backupWho.Abil.Level);
                                ustate.UseItems[i] = citem;
                            }
                        }
                    }
                    Def = Grobal2.MakeDefaultMsg(Grobal2.SM_SENDUSERSTATE, 0, 0, 0, 1);
                    SendSocket(Def, EDcode.EncodeBuffer(ustate, sizeof(TUserStateInfo)));
                }
            }
            catch
            {
                svMain.MainOutMessage("EXCEPT : ServerQueryUserState");
            }
        }

        private void ServerGetOpenGuildDlg()
        {
            int i;
            string data = string.Empty;
            if (this.MyGuild != null)
            {
                data = ((TGuild)this.MyGuild).GuildName + '\r';
                data = data + " \r";
                // 巩颇标富 颇老 捞抚
                if (this.GuildRank == 1)
                {
                    // 巩林
                    data = data + "1\r";
                }
                else
                {
                    data = data + "0\r";
                }
                // 老馆
                // NoticeList
                data = data + "<Notice>\r";
                if (((TGuild)this.MyGuild).NoticeList != null)
                {
                    for (i = 0; i < ((TGuild)this.MyGuild).NoticeList.Count; i++)
                    {
                        if (data.Length > 5000)
                        {
                            break;
                        }
                        data = data + ((TGuild)this.MyGuild).NoticeList[i] + '\r';
                    }
                }
                // KillGuilds
                data = data + "<KillGuilds>\r";
                if (((TGuild)this.MyGuild).KillGuilds != null)
                {
                    for (i = 0; i < ((TGuild)this.MyGuild).KillGuilds.Count; i++)
                    {
                        if (data.Length > 5000)
                        {
                            break;
                        }
                        data = data + ((TGuild)this.MyGuild).KillGuilds[i] + '\r';
                    }
                }
                // AllyGuilds
                data = data + "<AllyGuilds>\r";
                if (((TGuild)this.MyGuild).AllyGuilds != null)
                {
                    for (i = 0; i < ((TGuild)this.MyGuild).AllyGuilds.Count; i++)
                    {
                        if (data.Length > 5000)
                        {
                            break;
                        }
                        data = data + ((TGuild)this.MyGuild).AllyGuilds[i] + '\r';
                    }
                }
                Def = Grobal2.MakeDefaultMsg(Grobal2.SM_OPENGUILDDLG, 0, 0, 0, 1);
                SendSocket(Def, EDcode.EncodeString(data));
            }
            else
            {
                SendDefMessage(Grobal2.SM_OPENGUILDDLG_FAIL, 0, 0, 0, 0, "");
            }
        }

        private void ServerGetGuildHome()
        {
            ServerGetOpenGuildDlg();
        }

        private void ServerGetGuildMemberList()
        {
            int i;
            int k;
            string data = string.Empty;
            TGuildRank pgrank;
            if (this.MyGuild != null)
            {
                data = "";
                if (((TGuild)this.MyGuild).MemberList != null)
                {
                    for (i = 0; i < ((TGuild)this.MyGuild).MemberList.Count; i++)
                    {
                        pgrank = (TGuildRank)((TGuild)this.MyGuild).MemberList[i];
                        data = data + "#" + pgrank.Rank.ToString() + "/*" + pgrank.RankName + "/";
                        for (k = 0; k < pgrank.MemList.Count; k++)
                        {
                            if (data.Length > 5000)
                            {
                                break;
                            }
                            data = data + pgrank.MemList[k] + "/";
                        }
                    }
                }
                Def = Grobal2.MakeDefaultMsg(Grobal2.SM_SENDGUILDMEMBERLIST, 0, 0, 0, 1);
                SendSocket(Def, EDcode.EncodeString(data));
            }
        }

        private void ServerGetGuildAddMember(string who)
        {
            int error;
            TUserHuman hum;
            error = 1;
            // 巩林父 荤侩啊瓷
            if (this.IsGuildMaster())
            {
                // 巩林父 啊瓷
                hum = svMain.UserEngine.GetUserHuman(who);
                if (hum != null)
                {
                    if (hum.GetFrontCret() == this)
                    {
                        if (hum.AllowEnterGuild)
                        {
                            if (!((TGuild)this.MyGuild).IsMember(who))
                            {
                                // 啊涝巩颇 绝阑 锭
                                // 牢盔 力茄
                                if ((hum.MyGuild == null) && (((TGuild)this.MyGuild).MemberList.Count < ObjBase.MAXGUILDMEMBER))
                                {
                                    ((TGuild)this.MyGuild).AddMember(hum);
                                    svMain.UserEngine.SendInterMsg(Grobal2.ISM_RELOADGUILD, svMain.ServerIndex, ((TGuild)this.MyGuild).GuildName);
                                    // 货 甘滚甫 巩颇俊 啊涝矫糯
                                    hum.MyGuild = this.MyGuild;
                                    hum.GuildRankName = ((TGuild)this.MyGuild).MemberLogin(hum, ref hum.GuildRank);
                                    // hum.SendMsg (self, RM_CHANGEGUILDNAME, 0, 0, 0, 0, '');
                                    this.ChangeNameColor();
                                    // 捞抚祸 诀单捞飘(sonmg 2004/12/29)
                                    error = 0;
                                }
                                else
                                {
                                    error = 4;
                                }
                                // 促弗 巩颇俊 啊涝登绢 乐澜.
                            }
                            else
                            {
                                error = 3;
                            }
                            // 捞固 啊涝登绢 乐澜.
                        }
                        else
                        {
                            error = 5;
                            // 惑措规捞 巩颇 啊涝阑 倾侩救窃.
                            hum.SysMsg("您拒绝加入门派。 [允许命令为@加入门派]", 0);
                        }
                    }
                    else
                    {
                        error = 2;
                    }
                }
                else
                {
                    error = 2;
                }
                // 立加窍绊 付林焊绊 乐绢具 窃.
            }
            if (error == 0)
            {
                SendDefMessage(Grobal2.SM_GUILDADDMEMBER_OK, 0, 0, 0, 0, "");
            }
            else
            {
                SendDefMessage(Grobal2.SM_GUILDADDMEMBER_FAIL, error, 0, 0, 0, "");
            }
        }

        private void ServerGetGuildDelMember(string who)
        {
            int error;
            TUserHuman hum;
            string gname;
            error = 1;
            // 巩林父 荤侩啊瓷
            if (this.IsGuildMaster())
            {
                // 巩林父 啊瓷
                if (((TGuild)this.MyGuild).IsMember(who))
                {
                    if (this.UserName != who)
                    {
                        if (((TGuild)this.MyGuild).DelMember(who))
                        {
                            hum = svMain.UserEngine.GetUserHuman(who);
                            if (hum != null)
                            {
                                hum.MyGuild = null;
                                hum.GuildRankChanged(0, "");
                                hum.UserNameChanged();
                            }
                            svMain.UserEngine.SendInterMsg(Grobal2.ISM_RELOADGUILD, svMain.ServerIndex, ((TGuild)this.MyGuild).GuildName);
                            error = 0;
                        }
                        else
                        {
                            error = 4;
                        }
                        // 且 荐 绝澜.
                    }
                    else
                    {
                        error = 3;
                        // 巩林 夯牢篮 呕硼 救凳.
                        // 巩林夯牢捞 呕硼窍妨搁 巩盔捞 酒公档 绝绰惑怕俊辑 磊脚阑 哗搁凳, 巩颇档 柄咙
                        gname = ((TGuild)this.MyGuild).GuildName;
                        if (((TGuild)this.MyGuild).DelGuildMaster(who))
                        {
                            // 厘盔 馆券 饶 巩颇昏力.
                            // GuildAgitMan.DelGuildAgit( gname );
                            svMain.GuildMan.DelGuild(gname);
                            // 巩颇啊 荤扼柳促.
                            svMain.UserEngine.SendInterMsg(Grobal2.ISM_DELGUILD, svMain.ServerIndex, gname);
                            // UserEngine.SendInterMsg (ISM_RELOADGUILD, ServerIndex, TGuild(MyGuild).GuildName);
                            this.MyGuild = null;
                            GuildRankChanged(0, "");
                            this.UserNameChanged();
                            this.SysMsg("\"" + gname + "\"门派已经被取消了。", 0);
                            error = 0;
                        }
                    }
                }
                else
                {
                    error = 2;
                }
                // 巩盔捞 酒丛
            }
            if (error == 0)
            {
                SendDefMessage(Grobal2.SM_GUILDDELMEMBER_OK, 0, 0, 0, 0, "");
            }
            else
            {
                SendDefMessage(Grobal2.SM_GUILDDELMEMBER_FAIL, error, 0, 0, 0, "");
            }
        }

        private void ServerGetGuildUpdateNotice(string body)
        {
            string data = string.Empty;
            if (this.MyGuild == null)
            {
                return;
            }
            if (this.GuildRank != 1)
            {
                return;
            }
            // 巩颇狼 巩林父 函版 啊瓷
            ((TGuild)this.MyGuild).NoticeList.Clear();
            while (true)
            {
                if (body == "")
                {
                    break;
                }
                body  =  HUtil32.GetValidStr3(body, ref data, new char[] { '\r' });
                ((TGuild)this.MyGuild).NoticeList.Add(data);
            }
            ((TGuild)this.MyGuild).SaveGuild();
            svMain.UserEngine.SendInterMsg(Grobal2.ISM_RELOADGUILD, svMain.ServerIndex, ((TGuild)this.MyGuild).GuildName);
            ServerGetOpenGuildDlg();
        }

        private void ServerGetGuildUpdateRanks(string body)
        {
            int error;
            if (this.MyGuild == null)
            {
                return;
            }
            if (this.GuildRank != 1)
            {
                return;
            }
            // 巩颇狼 巩林父 函版 啊瓷
            error = ((TGuild)this.MyGuild).UpdateGuildRankStr(body);
            if (error == 0)
            {
                svMain.UserEngine.SendInterMsg(Grobal2.ISM_RELOADGUILD, svMain.ServerIndex, ((TGuild)this.MyGuild).GuildName);
                ServerGetGuildMemberList();
            }
            else if (error <= -2)
            {
                SendDefMessage(Grobal2.SM_GUILDRANKUPDATE_FAIL, error, 0, 0, 0, "");
            }
            // -1: 捞傈苞 鞍澜.. 贸府窍瘤 臼绰促.

        }

        // 惑措祈 巩林客 付林焊绊
        private void ServerGetGuildMakeAlly()
        {
            int error;
            TUserHuman hum;
            error = -1;
            // 巩林父 荤侩啊瓷
            hum = this.GetFrontCret() as TUserHuman;
            if (hum != null)
            {
                if (hum.RaceServer == Grobal2.RC_USERHUMAN)
                {
                    if (hum.GetFrontCret() == this)
                    {
                        if (((TGuild)hum.MyGuild).AllowAllyGuild)
                        {
                            if (this.IsGuildMaster() && hum.IsGuildMaster())
                            {
                                if (((TGuild)this.MyGuild).CanAlly((TGuild)hum.MyGuild) && ((TGuild)hum.MyGuild).CanAlly((TGuild)this.MyGuild))
                                {
                                    ((TGuild)this.MyGuild).MakeAllyGuild((TGuild)hum.MyGuild);
                                    ((TGuild)hum.MyGuild).MakeAllyGuild((TGuild)this.MyGuild);
                                    ((TGuild)this.MyGuild).GuildMsg(((TGuild)hum.MyGuild).GuildName + "门派已经和您的门派结盟完成。");
                                    ((TGuild)hum.MyGuild).GuildMsg(((TGuild)this.MyGuild).GuildName + "门派已经和您的门派结盟完成。");
                                    ((TGuild)this.MyGuild).MemberNameChanged();
                                    ((TGuild)hum.MyGuild).MemberNameChanged();
                                    svMain.UserEngine.SendInterMsg(Grobal2.ISM_RELOADGUILD, svMain.ServerIndex, ((TGuild)this.MyGuild).GuildName);
                                    svMain.UserEngine.SendInterMsg(Grobal2.ISM_RELOADGUILD, svMain.ServerIndex, ((TGuild)hum.MyGuild).GuildName);
                                    error = 0;
                                }
                                else
                                {
                                    error = -2;
                                }
                            }
                            else
                            {
                                error = -3;
                            }
                        }
                        else
                        {
                            error = -4;
                        }
                    }
                }
            }
            if (error == 0)
            {
                SendDefMessage(Grobal2.SM_GUILDMAKEALLY_OK, 0, 0, 0, 0, "");
            }
            else
            {
                SendDefMessage(Grobal2.SM_GUILDMAKEALLY_FAIL, error, 0, 0, 0, "");
            }
        }

        private void ServerGetGuildBreakAlly(string gname)
        {
            TGuild aguild;
            int error;
            error = -1;
            if (this.IsGuildMaster())
            {
                aguild = svMain.GuildMan.GetGuild(gname);
                if (aguild != null)
                {
                    if (((TGuild)this.MyGuild).IsAllyGuild(aguild))
                    {
                        ((TGuild)this.MyGuild).BreakAlly(aguild);
                        aguild.BreakAlly((TGuild)this.MyGuild);
                        ((TGuild)this.MyGuild).GuildMsg(aguild.GuildName + "门派已经和您的门派解除结盟完成。");
                        aguild.GuildMsg(((TGuild)this.MyGuild).GuildName + "门派解除了与您的门派的结盟。");
                        ((TGuild)this.MyGuild).MemberNameChanged();
                        aguild.MemberNameChanged();
                        // 促弗 辑滚俊 利侩
                        svMain.UserEngine.SendInterMsg(Grobal2.ISM_RELOADGUILD, svMain.ServerIndex, ((TGuild)this.MyGuild).GuildName);
                        svMain.UserEngine.SendInterMsg(Grobal2.ISM_RELOADGUILD, svMain.ServerIndex, aguild.GuildName);
                        error = 0;
                    }
                    else
                    {
                        error = -2;
                    }
                    // 悼竿吝 酒丛
                }
                else
                {
                    error = -3;
                }
                // 弊繁 巩颇 绝澜
            }
            if (error == 0)
            {
                // 己傍
                SendDefMessage(Grobal2.SM_GUILDBREAKALLY_OK, 0, 0, 0, 0, "");
            }
            else
            {
                SendDefMessage(Grobal2.SM_GUILDBREAKALLY_FAIL, error, 0, 0, 0, "");
            }
        }

        public ushort ServerGetAdjustBonus_CalcLoHi(short abil, short point)
        {
            short result;
            int i;
            int lo;
            int hi;
            lo = Lobyte((ushort)abil);
            hi = HiByte((ushort)abil);
            for (i = 1; i <= point; i++)
            {
                if (lo + 1 < hi)
                {
                    lo++;
                }
                else
                {
                    hi++;
                }
            }
            result = (short)MakeWord(lo, hi);
            return (ushort)result;
        }

        private void ServerGetAdjustBonus(int remainbonus, string bodystr)
        {
#if FOR_ABIL_POINT
            // 4/16老何磐 利侩
            if ((remainbonus >= 0) && (remainbonus < this.BonusPoint))
            {
                                DecodeBuffer(bodystr, cha);
                // 八刘...
                sum = cha.DC + cha.MC + cha.SC + cha.AC + cha.MAC + cha.HP + cha.MP + cha.Hit + cha.Speed;
                ptk = null;
                switch(this.Job)
                {
                    case 0:
                        ptk = M2Share.WarriorBonus;
                        break;
                    case 1:
                        ptk = M2Share.WizzardBonus;
                        break;
                    case 2:
                        ptk = M2Share.PriestBonus;
                        break;
                }
                if ((ptk != null) && (sum == (this.BonusPoint - remainbonus)))
                {
                    this.BonusPoint = remainbonus;
                    this.CurBonusAbil.DC = this.CurBonusAbil.DC + cha.DC;
                    this.CurBonusAbil.MC = this.CurBonusAbil.MC + cha.MC;
                    this.CurBonusAbil.SC = this.CurBonusAbil.SC + cha.SC;
                    this.CurBonusAbil.AC = this.CurBonusAbil.AC + cha.AC;
                    this.CurBonusAbil.MAC = this.CurBonusAbil.MAC + cha.MAC;
                    this.CurBonusAbil.HP = this.CurBonusAbil.HP + cha.HP;
                    this.CurBonusAbil.MP = this.CurBonusAbil.MP + cha.MP;
                    this.CurBonusAbil.Hit = this.CurBonusAbil.Hit + cha.Hit;
                    this.CurBonusAbil.Speed = this.CurBonusAbil.Speed + cha.Speed;
                    this.BonusAbil.DC = ServerGetAdjustBonus_CalcLoHi(this.BonusAbil.DC, this.CurBonusAbil.DC / ptk.DC);
                    this.CurBonusAbil.DC = this.CurBonusAbil.DC % ptk.DC;
                    this.BonusAbil.MC = ServerGetAdjustBonus_CalcLoHi(this.BonusAbil.MC, this.CurBonusAbil.MC / ptk.MC);
                    this.CurBonusAbil.MC = this.CurBonusAbil.MC % ptk.MC;
                    this.BonusAbil.SC = ServerGetAdjustBonus_CalcLoHi(this.BonusAbil.SC, this.CurBonusAbil.SC / ptk.SC);
                    this.CurBonusAbil.SC = this.CurBonusAbil.SC % ptk.SC;
                                                            this.BonusAbil.AC = MakeWord(0, HiByte(this.BonusAbil.AC) + this.CurBonusAbil.AC / ptk.AC);
                    // CalcLoHi (BonusAbil.AC, CurBonusAbil.AC div ptk.AC);
                    this.CurBonusAbil.AC = this.CurBonusAbil.AC % ptk.AC;
                                                            this.BonusAbil.MAC = MakeWord(0, HiByte(this.BonusAbil.MAC) + this.CurBonusAbil.MAC / ptk.MAC);
                    // CalcLoHi (BonusAbil.MAC, CurBonusAbil.MAC div ptk.MAC);
                    this.CurBonusAbil.MAC = this.CurBonusAbil.MAC % ptk.MAC;
                    this.BonusAbil.HP = this.BonusAbil.HP + this.CurBonusAbil.HP / ptk.HP;
                    this.CurBonusAbil.HP = this.CurBonusAbil.HP % ptk.HP;
                    this.BonusAbil.MP = this.BonusAbil.MP + this.CurBonusAbil.MP / ptk.MP;
                    this.CurBonusAbil.MP = this.CurBonusAbil.MP % ptk.MP;
                    this.BonusAbil.Hit = this.BonusAbil.Hit + this.CurBonusAbil.Hit / ptk.Hit;
                    this.CurBonusAbil.Hit = this.CurBonusAbil.Hit % ptk.Hit;
                    this.BonusAbil.Speed = this.BonusAbil.Speed + this.CurBonusAbil.Speed / ptk.Speed;
                    this.CurBonusAbil.Speed = this.CurBonusAbil.Speed % ptk.Speed;
                    this.RecalcLevelAbilitys();
                    this.RecalcAbilitys();
                    this.SendMsg(this, Grobal2.RM_ABILITY, 0, 0, 0, 0, "");
                    this.SendMsg(this, Grobal2.RM_SUBABILITY, 0, 0, 0, 0, "");
                }
                ServerSendAdjustBonus();
            // 焊呈胶 器牢飘甫 促矫 焊郴霖促.
            }
#endif

        }

        private void RmMakeSlaveProc(TSlaveInfo pslave)
        {
            TCreature cret;
            int maxcount;
            if (this.Job == 2)
            {
                // 档荤牢版快
                maxcount = 1;
            }
            else
            {
                maxcount = 5;
            }
            cret = this.MakeSlave(pslave.SlaveName, pslave.SlaveMakeLevel, maxcount, pslave.RemainRoyalty);
            if (cret != null)
            {
                cret.SlaveExp = pslave.SlaveExp;
                cret.SlaveExpLevel = pslave.SlaveExpLevel;
                cret.WAbil.HP = (ushort)pslave.HP;
                cret.WAbil.MP = (ushort)pslave.MP;
                if (cret.NextWalkTime > 1500 - (pslave.SlaveMakeLevel * 200))
                {
                    cret.NextWalkTime = 1500 - (pslave.SlaveMakeLevel * 200);
                }
                if (cret.NextHitTime > 2000 - (pslave.SlaveMakeLevel * 200))
                {
                    cret.NextHitTime = 2000 - (pslave.SlaveMakeLevel * 200);
                }
                cret.RecalcAbilitys();
            }
        }

        // RelationShip ....
        // 楷牢荤力 可记函版
        private void ServerGetRelationOptionChange(int OptionType, int Enable)
        {
            switch (OptionType)
            {
                case 1:
                    // 楷牢老 版快俊
                    // 捞傈狼 惑怕狼 馆傈阑 茄促.
                    if (1 == this.fLover.GetEnable(Grobal2.RsState_Lover))
                    {
                        this.fLover.SetEnable(Grobal2.RsState_Lover, 0);
                    }
                    else
                    {
                        this.fLover.SetEnable(Grobal2.RsState_Lover, 1);
                    }
                    SendDefMessage(Grobal2.SM_LM_OPTION, 0, OptionType, this.fLover.GetEnable(Grobal2.RsState_Lover), 0, "");
                    break;
            }
        }

        // 楷牢荤力 包拌 夸没
        private void ServerGetRelationRequest(int ReqType, int ReqSeq)
        {
            TCreature cert;
            TUserHuman Target;
            int ListCount;
            string msgstr;
            string Date;
            string str;
            int CheckResult;
            // 菊俊 乐绰 惑措甫 掘绰促.
            cert = this.GetFrontCret();
            // 鸥百捞 绝芭唱 , 付林焊绊 乐瘤 臼芭唱 , 牢埃捞 酒聪搁 唱埃促.
            if ((cert == null) || (cert.GetFrontCret() != this) || (cert.RaceServer != Grobal2.RC_USERHUMAN))
            {
                this.BoxMsg("你必须面对对方。", 0);
                return;
            }
            // human 栏肺 鸥涝 官厕
            Target = cert as TUserHuman;
            switch (ReqType)
            {
                case Grobal2.RsState_Lover:
                    // 炼扒眉农
                    // 楷牢狼 版快 炼扒 眉农
                    // 磊脚狼 饭骇眉农
                    if (this.WAbil.Level < 22)
                    {
                        SendDefMessage(Grobal2.SM_LM_RESULT, 0, ReqType, Grobal2.RsError_LessLevelMe, 0, "");
                        return;
                    }
                    // 惑措规狼 饭骇 眉农
                    if (Target.WAbil.Level < 22)
                    {
                        SendDefMessage(Grobal2.SM_LM_RESULT, 0, ReqType, Grobal2.RsError_LessLevelOther, 0, Target.UserName);
                        return;
                    }
                    // 惑措规苞狼 己喊 眉农
                    if (this.Sex == Target.Sex)
                    {
                        SendDefMessage(Grobal2.SM_LM_RESULT, 0, ReqType, Grobal2.RsError_EqualSex, 0, "");
                        return;
                    }
                    // 背力啊瓷 炼扒 眉农 (眉农 鉴辑 炼沥)
                    CheckResult = this.fLover.GetEnableJoin(ReqType);
                    if (CheckResult == 1)
                    {
                        // 背力啊瓷 惑怕啊 酒丛
                        SendDefMessage(Grobal2.SM_LM_RESULT, 0, ReqType, Grobal2.RsError_RejectMe, 0, "");
                        return;
                    }
                    else if (CheckResult == 2)
                    {
                        // 捞固 背力吝牢 荤恩捞 乐澜
                        SendDefMessage(Grobal2.SM_LM_RESULT, 0, ReqType, Grobal2.RsError_FullUser, 0, this.UserName);
                        return;
                    }
                    else if (CheckResult != 0)
                    {
                        return;
                    }
                    // 唱狼 背力啊瓷 惑怕 眉农
                    if (!this.fLover.EnableJoinLover)
                    {
                        SendDefMessage(Grobal2.SM_LM_RESULT, 0, ReqType, Grobal2.RsError_RejectMe, 0, "");
                        return;
                    }
                    // 惑措规狼 背力啊瓷惑怕 眉农
                    if (!Target.fLover.EnableJoinLover)
                    {
                        SendDefMessage(Grobal2.SM_LM_RESULT, 0, ReqType, Grobal2.RsError_RejectOther, 0, Target.UserName);
                        return;
                    }
                    break;
                default:
                    // 楷牢捞 酒匆版快狼 炼扒眉农
                    return;
                    break;
            }
            switch (ReqSeq)
            {
                case Grobal2.RsReq_None:
                    break;
                case Grobal2.RsReq_WantToJoinOther:
                    // 曼咯 矫啮胶 函拳 ...
                    // 扁夯惑怕
                    // 穿备俊霸 曼啊脚没阑 窃
                    if (!this.fLover.GetEnableJoinReq(ReqType))
                    {
                        // 郴啊 惑措规俊霸 曼咯且荐 绝绰 惑怕捞促.
                        SendDefMessage(Grobal2.SM_LM_RESULT, 0, ReqType, Grobal2.RsError_DontJoin, 0, Target.UserName);
                    }
                    else
                    {
                        // 曼咯啊瓷茄瘤 八配
                        CheckResult = Target.fLover.GetEnableJoin(ReqType);
                        if (CheckResult == 1)
                        {
                            // 背力啊瓷 惑怕啊 酒丛
                            SendDefMessage(Grobal2.SM_LM_RESULT, 0, ReqType, Grobal2.RsError_RejectOther, 0, Target.UserName);
                        }
                        else if (CheckResult == 2)
                        {
                            // 捞固 背力吝牢 荤恩捞 乐澜
                            SendDefMessage(Grobal2.SM_LM_RESULT, 0, ReqType, Grobal2.RsError_FullUser, 0, Target.UserName);
                        }
                        else if (CheckResult == 0)
                        {
                            // 惑措规捞 促弗 夸备甫 罐绊 乐绰惑怕啊 酒聪搁
                            if (Target.fLover.ReqSequence == Grobal2.RsReq_None)
                            {
                                // 穿焙啊 脚没沁促绊 舅覆
                                Target.SendDefMessage(Grobal2.SM_LM_REQUEST, 0, ReqType, Grobal2.RsReq_WhoWantJoin, 0, this.UserName);
                                // 唱绰 措翠阑 扁促府绰 惑怕捞绊
                                this.fLover.ReqSequence = Grobal2.RsReq_WaitAnser;
                                // 惑措规篮 措翠阑 秦拎具 窍绰 惑怕
                                Target.fLover.ReqSequence = Grobal2.RsReq_WhoWantJoin;
                            }
                            else
                            {
                                // 惑措规捞 泅犁 促弗 览翠 惑怕捞促.
                                SendDefMessage(Grobal2.SM_LM_RESULT, 0, ReqType, Grobal2.RsError_DontJoin, 0, Target.UserName);
                            }
                        }
                        // if not Target.fLover.GetEnableJoin
                    }
                    break;
                case Grobal2.RsReq_AloowJoin:
                    // if not Self.fLover.GetEnableJoinReq
                    // 曼啊甫 倾遏窃
                    if (Target.fLover.ReqSequence == Grobal2.RsReq_WaitAnser)
                    {
                        // 鸥百阑 殿废茄促.
                        Date = "";
                        this.fLover.ReqSequence = Grobal2.RsReq_None;
                        this.fLover.Add(this.UserName, Target.UserName, ReqType, Target.WAbil.Level, Target.Sex, Date, "");
                        msgstr = this.fLover.GetListmsg(ReqType, ListCount);
                        SendDefMessage(Grobal2.SM_LM_LIST, 0, ListCount, 0, 0, msgstr);
                        SendDefMessage(Grobal2.SM_LM_RESULT, 0, ReqType, Grobal2.RsError_SuccessJoin, 0, Target.UserName);
                        // 磊脚阑 鸥百俊 殿废茄促.
                        Target.fLover.ReqSequence = Grobal2.RsReq_None;
                        Target.fLover.Add(Target.UserName, this.UserName, ReqType, this.WAbil.Level, this.Sex, Date, "");
                        msgstr = Target.fLover.GetListmsg(ReqType, ListCount);
                        Target.SendDefMessage(Grobal2.SM_LM_LIST, 0, ListCount, 0, 0, msgstr);
                        Target.SendDefMessage(Grobal2.SM_LM_RESULT, 0, ReqType, Grobal2.RsError_SuccessJoined, 0, this.UserName);
                        // DB 俊 历厘茄促. 茄疙父 历厘窍搁 唱赣瘤荤恩篮 舅酒辑 历厘等促.
                        this.SendMsg(this, Grobal2.RM_LM_DBADD, 0, 0, 0, 0, Target.UserName + ":" + ReqType.ToString() + ":" + Date + "/");
                        // 林函俊 寇摹扁
                        str = "恭喜！\"" + this.UserName + "\"与\"" + Target.UserName + "\"成为情侣。";
                        // UserEngine.CryCry (RM_SYSMSG_PINK, PEnvir, CX, CY, 300, '⒔' + str);
                        svMain.UserEngine.CryCry(Grobal2.RM_SYSMSG_PINK, this.PEnvir, this.CX, this.CY, 300, ":)" + str);
                        // 肺弊巢辫
                        // 楷牢_
                        // 肝澜:0
                        svMain.AddUserLog("47\09" + this.MapName + "\09" + this.CX.ToString() + "\09" + this.CY.ToString() + "\09" + this.UserName + "\09" + "0\09" + "0\09" + "0\09" + Target.UserName);
                        this.UserNameChanged();
                        Target.UserNameChanged();
                    }
                    else
                    {
                        // 惑措规捞 泅犁 促弗 览翠 惑怕捞促. 蝶扼辑 秒家矫挪促.
                        this.fLover.ReqSequence = Grobal2.RsReq_None;
                        SendDefMessage(Grobal2.SM_LM_RESULT, 0, ReqType, Grobal2.RsError_CancelJoin, 0, Target.UserName);
                        Target.fLover.ReqSequence = Grobal2.RsReq_None;
                        Target.SendDefMessage(Grobal2.SM_LM_RESULT, 0, ReqType, Grobal2.RsError_CancelJoin, 0, this.UserName);
                    }
                    break;
                case Grobal2.RsReq_DenyJoin:
                    // 曼啊甫 芭例窃
                    this.fLover.ReqSequence = Grobal2.RsReq_None;
                    SendDefMessage(Grobal2.SM_LM_RESULT, 0, ReqType, Grobal2.RsError_CancelJoin, 0, Target.UserName);
                    Target.fLover.ReqSequence = Grobal2.RsReq_None;
                    Target.SendDefMessage(Grobal2.SM_LM_RESULT, 0, ReqType, Grobal2.RsError_DenyJoin, 0, this.UserName);
                    break;
                case Grobal2.RsReq_Cancel:
                    // 秒家
                    this.fLover.ReqSequence = Grobal2.RsReq_None;
                    SendDefMessage(Grobal2.SM_LM_RESULT, 0, ReqType, Grobal2.RsError_CancelJoin, 0, Target.UserName);
                    Target.fLover.ReqSequence = Grobal2.RsReq_None;
                    Target.SendDefMessage(Grobal2.SM_LM_RESULT, 0, ReqType, Grobal2.RsError_CancelJoin, 0, this.UserName);
                    break;
            }
        }

        // 楷牢 荤力 昏力
        private void ServerGetRelationDelete(int ReqType, string OtherName)
        {
            int svidx=0;
            TUserHuman hum;
            TCreature cert;
            string strPayment;
            // 楷牢 包拌老 版快...
            if (ReqType == Grobal2.RsState_Lover)
            {
                // 菊俊 乐绰 惑措甫 掘绰促.
                cert = this.GetFrontCret();
                // 鸥百捞 绝芭唱 , 付林焊绊 乐瘤 臼芭唱 , 牢埃捞 酒聪搁 唱埃促.
                if ((cert == null) || (cert.GetFrontCret() != this) || (cert.RaceServer != Grobal2.RC_USERHUMAN))
                {
                    this.BoxMsg("要打破情侣关系，你必须面对对方。", 0);
                    return;
                }
                // human 栏肺 鸥涝 官厕
                hum = cert as TUserHuman;
                if (hum == null)
                {
                    return;
                }
                // 惑措规捞 唱狼 楷牢牢瘤 眉农
                if (!((hum.fLover.GetLoverName == this.UserName) && (fLover.GetLoverName == hum.UserName)))
                {
                    this.BoxMsg("这不是你的情侣。", 0);
                    return;
                }
                // 困磊丰 尘 捣捞 乐绰瘤 犬牢
                if (this.Gold < ObjBase.COMPENSATORY_PAYMENT)
                {
                    strPayment = (ObjBase.COMPENSATORY_PAYMENT / 10000).ToString();
                    this.BoxMsg("要打破情侣关系，你需要" + strPayment + "0,000金币。", 0);
                    return;
                }
                // -------------------
                // 楷牢 秦力 犬牢
                hum.SendDefMessage(Grobal2.SM_LM_DELETE_REQ, 0, 0, 0, 0, this.UserName);
                // ///////////////////
                return;
                // -------------------
            }
            if (RelationShipDeleteOther(ReqType, OtherName))
            {
                hum = svMain.UserEngine.GetUserHuman(OtherName);
                if (hum != null)
                {
                    hum.RelationShipDeleteOther(ReqType, this.UserName);
                }
                else
                {
                    if (svMain.UserEngine.FindOtherServerUser(OtherName, ref svidx))
                    {
                        svMain.UserEngine.SendInterMsg(Grobal2.ISM_LM_DELETE, svidx, OtherName + "/" + this.UserName + "/" + ReqType.ToString());
                    }
                }
            }
            else
            {
                SendDefMessage(Grobal2.SM_LM_RESULT, ReqType, Grobal2.RsError_DontDelete, 0, 0, OtherName);
            }
        }

        private void ServerGetRelationDeleteRequestOk(int ReqType, string OtherName)
        {
            int svidx=0;
            TUserHuman hum;
            TCreature cert;
            string strPayment;
            // 楷牢 包拌老 版快...
            if (ReqType == Grobal2.RsState_Lover)
            {
                // 菊俊 乐绰 惑措甫 掘绰促.
                cert = this.GetFrontCret();
                // 鸥百捞 绝芭唱 , 付林焊绊 乐瘤 臼芭唱 , 牢埃捞 酒聪搁 唱埃促.
                if ((cert == null) || (cert.GetFrontCret() != this) || (cert.RaceServer != Grobal2.RC_USERHUMAN))
                {
                    this.BoxMsg("要打破情侣关系，你必须面对对方。", 0);
                    return;
                }
                // human 栏肺 鸥涝 官厕
                hum = cert as TUserHuman;
                if (hum == null)
                {
                    return;
                }
                // 惑措规捞 唱狼 楷牢牢瘤 眉农
                if (!((hum.fLover.GetLoverName == this.UserName) && (fLover.GetLoverName == hum.UserName)))
                {
                    this.BoxMsg("这不是你的情侣。", 0);
                    return;
                }
                // 困磊丰 尘 捣捞 乐绰瘤 犬牢
                if (this.Gold < ObjBase.COMPENSATORY_PAYMENT)
                {
                    strPayment = (ObjBase.COMPENSATORY_PAYMENT / 10000).ToString();
                    this.BoxMsg("要打破情侣关系，你需要" + strPayment + "0,000金币。", 0);
                    return;
                }
            }
            if (RelationShipDeleteOther(ReqType, OtherName))
            {
                if (ReqType == Grobal2.RsState_Lover)
                {
                    if (this.Gold >= ObjBase.COMPENSATORY_PAYMENT)
                    {
                        this.DecGold(ObjBase.COMPENSATORY_PAYMENT);
                        this.GoldChanged();
                    }
                    this.MakePoison(Grobal2.POISON_SLOW, 3, 1);
                    this.WAbil.HP = (ushort)_MAX(1, this.WAbil.HP / 2);
                    this.WAbil.MP = (ushort)_MAX(1, this.WAbil.MP / 2);
                    this.SysMsg("情侣关系破裂了，将造成自身的冲击。", 0);
                    svMain.AddUserLog("47\09" + this.MapName + "\09" + this.CX.ToString() + "\09" + this.CY.ToString() + "\09" + this.UserName + "\09" + "0\09" + "0\09" + "2\09" + OtherName);
                }
                this.UserNameChanged();
                hum = svMain.UserEngine.GetUserHuman(OtherName);
                if (hum != null)
                {
                    if (hum.RelationShipDeleteOther(ReqType, this.UserName))
                    {
                        if (ReqType == Grobal2.RsState_Lover)
                        {
                            if (hum.Gold >= ObjBase.COMPENSATORY_PAYMENT)
                            {
                                hum.DecGold(ObjBase.COMPENSATORY_PAYMENT);
                                hum.GoldChanged();
                            }
                            hum.MakePoison(Grobal2.POISON_SLOW, 3, 1);
                            hum.WAbil.HP = (ushort)_MAX(1, hum.WAbil.HP / 2);
                            hum.WAbil.MP = (ushort)_MAX(1, hum.WAbil.MP / 2);
                            hum.SysMsg("情侣关系破裂了，将造成自身的冲击。", 0);
                            svMain.AddUserLog("47\09" + hum.MapName + "\09" + hum.CX.ToString() + "\09" + hum.CY.ToString() + "\09" + hum.UserName + "\09" + "0\09" + "0\09" + "2\09" + this.UserName);
                        }
                    }
                    hum.UserNameChanged();
                }
                else
                {
                    if (svMain.UserEngine.FindOtherServerUser(OtherName, ref svidx))
                    {
                        svMain.UserEngine.SendInterMsg(Grobal2.ISM_LM_DELETE, svidx, OtherName + "/" + this.UserName + "/" + ReqType.ToString());
                    }
                }
            }
            else
            {
                SendDefMessage(Grobal2.SM_LM_RESULT, ReqType, Grobal2.RsError_DontDelete, 0, 0, OtherName);
            }
        }

        private void ServerGetRelationDeleteRequestFail(int ReqType, string OtherName)
        {
            TUserHuman hum;
            hum = svMain.UserEngine.GetUserHuman(OtherName);
            if (hum == null)
            {
                return;
            }
            // 背力 芭何 皋矫瘤
            hum.SysMsg(this.UserName + "拒绝解除情侣关系。", 3);
        }

        // 辑滚捞悼栏肺 RM_MAKE_SLAVE啊 抗距登绢 乐绰瘤 咯何
        // RelationShip
        // 惑措规捞 磊脚阑 包拌秦力 矫淖阑 版快
        public bool RelationShipDeleteOther(int ReqType, string OtherName)
        {
            bool result;
            result = false;
            if (fLover.Find(OtherName))
            {
                fLover.Delete(OtherName);
                SendDefMessage(Grobal2.SM_LM_DELETE, 0, ReqType, 0, 0, OtherName);
                SendDefMessage(Grobal2.SM_LM_RESULT, 0, ReqType, Grobal2.RsError_SuccessDelete, 0, OtherName);
                // DB俊辑 昏力茄促.
                this.SendMsg(this, Grobal2.RM_LM_DBDELETE, 0, 0, 0, 0, OtherName + "/");
                result = true;
            }
            return result;
        }

        private void ServerSetRelationDBWantList(string body)
        {
            TDefaultMessage msg= new TDefaultMessage();
            msg.Recog = 0;
            msg.Ident = Grobal2.DB_LM_LIST;
            msg.Param = 0;
            msg.Tag = 0;
            msg.Series = 0;
            svMain.UserMgrEngine.ExternSendMsg(TSendTarget.stDbServer, 0, 0, 0, 0, this.UserName, msg, body);
        }

        private void ServerSetRelationDBAdd(string body)
        {
            TDefaultMessage msg= new TDefaultMessage();
            msg.Recog = 0;
            msg.Ident = Grobal2.DB_LM_ADD;
            msg.Param = 0;
            msg.Tag = 0;
            msg.Series = 0;
            svMain.UserMgrEngine.ExternSendMsg(TSendTarget.stDbServer, 0, 0, 0, 0, this.UserName, msg, body);
        }

        private void ServerSetRelationDBEdit(string body)
        {
            TDefaultMessage msg= new TDefaultMessage();
            msg.Recog = 0;
            msg.Ident = Grobal2.DB_LM_EDIT;
            msg.Param = 0;
            msg.Tag = 0;
            msg.Series = 0;
            svMain.UserMgrEngine.ExternSendMsg(TSendTarget.stDbServer, 0, 0, 0, 0, this.UserName, msg, body);
        }

        private void ServerSetRelationDBDelete(string body)
        {
            TDefaultMessage msg= new TDefaultMessage();
            msg.Recog = 0;
            msg.Ident = Grobal2.DB_LM_DELETE;
            msg.Param = 0;
            msg.Tag = 0;
            msg.Series = 0;
            svMain.UserMgrEngine.ExternSendMsg(TSendTarget.stDbServer, 0, 0, 0, 0, this.UserName, msg, body);
        }

        // 包拌矫胶袍殿俊辑 甘沥焊甫 舅酒柯促.
        private string GetCharMapInfo(string charname)
        {
            string result;
            TUserHuman userinfo;
            result = "";
            userinfo = null;
            if (charname == "")
            {
                return result;
            }
            userinfo = svMain.UserEngine.GetUserHuman(charname);
            if ((userinfo != null) && (userinfo.PEnvir != null))
            {
                result = userinfo.PEnvir.MapTitle;
            }
            return result;
        }

        // 包拌矫胶袍俊辑 府胶飘甫 DB 俊辑 掘绢吭阑版快
        private void ServerGetRelationDBGetList(string body)
        {
            int count;
            int i;
            string msgstr = String.Empty;
            int ListCnt;
            string str = String.Empty;
            string datastr = String.Empty;
            string tempstr = String.Empty;
            string _Name = String.Empty;
            int _State;
            int _Msg;
            string _Date = String.Empty;
            int _Level;
            int _Sex;
            TUserHuman hum;
            int svidx=0;
            string lovername = String.Empty;
            str = HUtil32.GetValidStr3(body, ref datastr, new string[] { "/" });
            count = HUtil32.Str_ToInt(datastr, 0);
            _State = 0;
            for (i = 0; i < count; i++)
            {
                str = HUtil32.GetValidStr3(str, ref datastr, new string[] { "/" });
                if (datastr != "")
                {
                    datastr = HUtil32.GetValidStr3(datastr, ref _Name, new string[] { ":" });
                    datastr = HUtil32.GetValidStr3(datastr, ref tempstr, new string[] { ":" });
                    _State = HUtil32.Str_ToInt(tempstr, 0);
                    datastr = HUtil32.GetValidStr3(datastr, ref tempstr, new string[] { ":" });
                    _Msg = HUtil32.Str_ToInt(tempstr, 0);
                    datastr = HUtil32.GetValidStr3(datastr, ref _Date, new string[] { ":" });
                    datastr = HUtil32.GetValidStr3(datastr, ref tempstr, new string[] { ":" });
                    _Level = HUtil32.Str_ToInt(tempstr, 0);
                    _Sex = HUtil32.Str_ToInt(datastr, 0);
                    switch (_State)
                    {
                        case Grobal2.RsState_Lover:
                            fLover.Add(this.UserName, _Name, _State, _Level, _Sex, _Date, GetCharMapInfo(_Name));
                            break;
                        case Grobal2.RsState_LoverEnd:
                            SendDefMessage(Grobal2.SM_LM_RESULT, 0, Grobal2.RsState_Lover, Grobal2.RsError_SuccessDelete, 0, _Name);
                            this.SendMsg(this, Grobal2.RM_LM_DBDELETE, 0, 0, 0, 0, _Name + "/");
                            break;
                        case 0:// 徒弟消息
                            this.m_sMasterName = _Name.Substring(0, _Name.Length - 1);
                            this.m_boMaster = (bool)_State;
                            this.m_nMasterRanking = _Msg;
                            break;
                        case 1:// 师傅消息
                            this.m_boMaster = (bool)_State;
                            if ((i == 0) && (_State == 1))
                            {
                                //FillChar(m_MasterRanking, sizeof(m_MasterRanking), '\0');
                            }
                            m_MasterRanking[_Level - 1].sMasterName = _Name.Substring(0, _Name.Length - 1);
                            m_MasterRanking[_Level - 1].nRanking = _Level;
                            this.m_nMasterCount = count;
                            break;
                    }
                }
            }
            msgstr = this.fLover.GetListmsg(_State, ListCnt);
            if (ListCnt > 0)
            {
                SendDefMessage(Grobal2.SM_LM_LIST, 0, ListCnt, 0, 0, msgstr);
            }
            if (!BoServerShifted)
            {
                lovername = fLover.GetLoverName;
                hum = svMain.UserEngine.GetUserHuman(lovername);
                if (hum != null)
                {
                    if (hum.fLover.GetLoverName != "")
                    {
                        hum.SysMsg(this.UserName + "在" + GetCharMapInfo(this.UserName) + "上线了。", 6);
                        this.SysMsg(hum.UserName + "目前在" + GetCharMapInfo(hum.UserName) + "。", 6);
                    }
                }
                else
                {
                    if (svMain.UserEngine.FindOtherServerUser(lovername, ref svidx))
                    {
                        svMain.UserEngine.SendInterMsg(Grobal2.ISM_LM_LOGIN, svidx, this.UserName + "/" + lovername + "/" + GetCharMapInfo(this.UserName));
                    }
                }
            }
            CheckMaster();
            GetQueryUserName(this, this.CX, this.CY);
        }

        private void ServerGetLoverLogout()
        {
            if (fLover == null)
            {
                return;
            }
            this.SysMsg(fLover.GetLoverName + "已经退出游戏。", 5);
        }

        private void ServerSetMasterDBAdd(string body)
        {
            TDefaultMessage msg = new TDefaultMessage();
            msg.Recog = 0;
            msg.Ident = Grobal2.DB_MA_ADD;
            msg.Param = 0;
            msg.Tag = 0;
            msg.Series = 0;
            svMain.UserMgrEngine.ExternSendMsg(TSendTarget.stDbServer, 0, 0, 0, 0, this.UserName + "1", msg, body);
        }

        private void ServerSetMasterDBEdit(string body)
        {
            TDefaultMessage msg= new TDefaultMessage();
            msg.Recog = 0;
            msg.Ident = Grobal2.DB_MA_EDIT;
            msg.Param = 0;
            msg.Tag = 0;
            msg.Series = 0;
            svMain.UserMgrEngine.ExternSendMsg(TSendTarget.stDbServer, 0, 0, 0, 0, this.UserName + "1", msg, body);
        }

        private void ServerSetMasterDBDelete(string body)
        {
            TDefaultMessage msg= new TDefaultMessage();
            msg.Recog = 0;
            msg.Ident = Grobal2.DB_MA_DELETE;
            msg.Param = 0;
            msg.Tag = 0;
            msg.Series = 0;
            svMain.UserMgrEngine.ExternSendMsg(TSendTarget.stDbServer, 0, 0, 0, 0, this.UserName + "1", msg, body);
        }

        private void ServerSetMasterDBWantList(string body)
        {
            TDefaultMessage msg= new TDefaultMessage();
            msg.Recog = 0;
            msg.Ident = Grobal2.DB_MA_LIST;
            msg.Param = 0;
            msg.Tag = 0;
            msg.Series = 0;
            svMain.UserMgrEngine.ExternSendMsg(TSendTarget.stDbServer, 0, 0, 0, 0, this.UserName + "1", msg, body);
        }

        // 墨款飘 酒捞袍
        private void ServerSendItemCountChanged(int mindex, int icount, int increase, string itmname)
        {
            if (icount <= 0)
            {
                svMain.MainOutMessage("[Caution!] icount <= 0 in TUserHuman.ServerSendItemCountChanged");
            }
            Def = Grobal2.MakeDefaultMsg(Grobal2.SM_COUNTERITEMCHANGE, mindex, icount, increase, 0);
            SendSocket(Def, EDcode.EncodeString(itmname));
        }

        // added by sonmg.
        public int DoUpgradeItem(TUserItem puSeed, TStdItem psSeed, TStdItem psJewelry)
        {
            int result;
            result = 1;
            switch (psSeed.StdMode)
            {
                case 5:
                case 6:
                    puSeed.Desc[0] = (byte)(puSeed.Desc[0] + psJewelry.DC);
                    puSeed.Desc[1] = (byte)(puSeed.Desc[1] + psJewelry.MC);
                    puSeed.Desc[2] = (byte)(puSeed.Desc[2] + psJewelry.SC);
                    if (psJewelry.AtkSpd > 0)
                    {
                        puSeed.Desc[6] = svMain.ItemMan.UpgradeAttackSpeed(puSeed.Desc[6], psJewelry.AtkSpd);
                        puSeed.Desc[6] = (byte)_MIN(15 + 10, puSeed.Desc[6]);
                    }
                    puSeed.Desc[12] = (byte)(puSeed.Desc[12] + psJewelry.Slowdown);
                    puSeed.Desc[13] = (byte)(puSeed.Desc[13] + psJewelry.Tox);
                    break;
                case 10:
                case 11:
                    puSeed.Desc[0] =  (byte)(puSeed.Desc[0] + psJewelry.AC);
                    puSeed.Desc[1] =  (byte)(puSeed.Desc[1] + psJewelry.MAC);
                    puSeed.Desc[11] = (byte)( puSeed.Desc[11] + psJewelry.Agility);
                    puSeed.Desc[12] = (byte)( puSeed.Desc[12] + psJewelry.MgAvoid);
                    puSeed.Desc[13] = (byte)(puSeed.Desc[13] + psJewelry.ToxAvoid);
                    break;
                case 15:
                    puSeed.Desc[0] = (byte)(puSeed.Desc[0] + psJewelry.AC);
                    puSeed.Desc[1] = (byte)(puSeed.Desc[1] + psJewelry.MAC);
                    puSeed.Desc[11] = (byte)(puSeed.Desc[11] + psJewelry.Accurate);
                    puSeed.Desc[12] = (byte)(puSeed.Desc[12] + psJewelry.MgAvoid);
                    puSeed.Desc[13] = (byte)(puSeed.Desc[13] + psJewelry.ToxAvoid);
                    break;
                case 19:
                    puSeed.Desc[2] =  (byte)(puSeed.Desc[2] + psJewelry.DC);
                    puSeed.Desc[3] =  (byte)(puSeed.Desc[3] + psJewelry.MC);
                    puSeed.Desc[4] = (byte)(puSeed.Desc[4] + psJewelry.SC);
                    puSeed.Desc[11] = (byte)(puSeed.Desc[11] + psJewelry.Accurate);
                    puSeed.Desc[0] = (byte)(puSeed.Desc[0] + psJewelry.MgAvoid);
                    if (psJewelry.AtkSpd > 0)
                    {
                        puSeed.Desc[9] = (byte)(puSeed.Desc[9] + psJewelry.AtkSpd);
                        puSeed.Desc[9] = (byte)_MIN(15, puSeed.Desc[9]);
                    }
                    puSeed.Desc[12] = (byte)(puSeed.Desc[12] + psJewelry.Slowdown);
                    puSeed.Desc[13] = (byte)(puSeed.Desc[13] + psJewelry.Tox);
                    break;
                case 20:
                    puSeed.Desc[2] = (byte)(puSeed.Desc[2] + psJewelry.DC);
                    puSeed.Desc[3] = (byte)(puSeed.Desc[3] + psJewelry.MC);
                    puSeed.Desc[4] = (byte)(puSeed.Desc[4] + psJewelry.SC);
                    puSeed.Desc[0] = (byte)(puSeed.Desc[0] + psJewelry.Accurate);
                    puSeed.Desc[1] = (byte)(puSeed.Desc[1] + psJewelry.Agility);
                    puSeed.Desc[11] = (byte)(puSeed.Desc[11] + psJewelry.MgAvoid);
                    if (psJewelry.AtkSpd > 0)
                    {
                        puSeed.Desc[9] = (byte)(puSeed.Desc[9] + psJewelry.AtkSpd);
                        puSeed.Desc[9] = (byte)_MIN(15, puSeed.Desc[9]);
                    }
                    puSeed.Desc[12] = (byte)(puSeed.Desc[12] + psJewelry.Slowdown);
                    puSeed.Desc[13] = (byte)(puSeed.Desc[13] + psJewelry.Tox);
                    break;
                case 21:
                    puSeed.Desc[2] =  (byte)(puSeed.Desc[2] + psJewelry.DC);
                    puSeed.Desc[3] =  (byte)(puSeed.Desc[3] + psJewelry.MC);
                    puSeed.Desc[4] =  (byte)(puSeed.Desc[4] + psJewelry.SC);
                    puSeed.Desc[7] = (byte)(puSeed.Desc[7] + psJewelry.MgAvoid);
                    puSeed.Desc[11] = (byte)(puSeed.Desc[11] + psJewelry.Accurate);
                    if (psJewelry.AtkSpd > 0)
                    {
                        puSeed.Desc[9] = (byte)(puSeed.Desc[9] + psJewelry.AtkSpd);
                        puSeed.Desc[9] = (byte)_MIN(15, puSeed.Desc[9]);
                    }
                    puSeed.Desc[12] = (byte)(puSeed.Desc[12] + psJewelry.Slowdown);
                    puSeed.Desc[13] = (byte)(puSeed.Desc[13] + psJewelry.Tox);
                    break;
                case 22:
                    puSeed.Desc[0] = (byte)(puSeed.Desc[0] + psJewelry.AC);
                    puSeed.Desc[1] = (byte)(puSeed.Desc[1] + psJewelry.MAC);
                    puSeed.Desc[2] = (byte)(puSeed.Desc[2] + psJewelry.DC);
                    puSeed.Desc[3] = (byte)(puSeed.Desc[3] + psJewelry.MC);
                    puSeed.Desc[4] = (byte)(puSeed.Desc[4] + psJewelry.SC);
                    if (psJewelry.AtkSpd > 0)
                    {
                        puSeed.Desc[9] = (byte)(puSeed.Desc[9] + psJewelry.AtkSpd);
                        puSeed.Desc[9] = (byte)_MIN(15, puSeed.Desc[9]);
                    }
                    puSeed.Desc[12] = (byte)( puSeed.Desc[12] + psJewelry.Slowdown);
                    puSeed.Desc[13] = (byte)(puSeed.Desc[13] + psJewelry.Tox);
                    break;
                case 23:
                    puSeed.Desc[2] =  (byte)(puSeed.Desc[2] + psJewelry.DC);
                    puSeed.Desc[3] =  (byte)(puSeed.Desc[3] + psJewelry.MC);
                    puSeed.Desc[4] = (byte)(puSeed.Desc[4] + psJewelry.SC);
                    if (psJewelry.AtkSpd > 0)
                    {
                        puSeed.Desc[9] = (byte)(puSeed.Desc[9] + psJewelry.AtkSpd);
                        puSeed.Desc[9] = (byte)_MIN(15, puSeed.Desc[9]);
                    }
                    puSeed.Desc[12] = (byte)(puSeed.Desc[12] + psJewelry.Slowdown);
                    puSeed.Desc[13] = (byte)(puSeed.Desc[13] + psJewelry.Tox);
                    break;
                case 24:
                    puSeed.Desc[0] =  (byte)(puSeed.Desc[0] + psJewelry.Accurate);
                    puSeed.Desc[1] =  (byte)(puSeed.Desc[1] + psJewelry.Agility);
                    puSeed.Desc[2] =  (byte)(puSeed.Desc[2] + psJewelry.DC);
                    puSeed.Desc[3] =  (byte)(puSeed.Desc[3] + psJewelry.MC);
                    puSeed.Desc[4] = (byte)(puSeed.Desc[4] + psJewelry.SC);
                    break;
                case 26:
                    puSeed.Desc[0] =  (byte)(puSeed.Desc[0] + psJewelry.AC);
                    puSeed.Desc[1] =  (byte)(puSeed.Desc[1] + psJewelry.MAC);
                    puSeed.Desc[2] =  (byte)(puSeed.Desc[2] + psJewelry.DC);
                    puSeed.Desc[3] =  (byte)(puSeed.Desc[3] + psJewelry.MC);
                    puSeed.Desc[4] = (byte)(puSeed.Desc[4] + psJewelry.SC);
                    puSeed.Desc[11] = (byte)(puSeed.Desc[11] + psJewelry.Accurate);
                    puSeed.Desc[12] = (byte)(puSeed.Desc[12] + psJewelry.Agility);
                    break;
                case 52:
                    puSeed.Desc[0] = (byte)(puSeed.Desc[0] + psJewelry.AC);
                    puSeed.Desc[1] = (byte)(puSeed.Desc[1] + psJewelry.MAC);
                    puSeed.Desc[3] = (byte)(puSeed.Desc[3] + psJewelry.Agility);
                    break;
                case 54:
                    puSeed.Desc[0] =  (byte)(puSeed.Desc[0] + psJewelry.AC);
                    puSeed.Desc[1] =  (byte)(puSeed.Desc[1] + psJewelry.MAC);
                    puSeed.Desc[2] =  (byte)(puSeed.Desc[2] + psJewelry.Accurate);
                    puSeed.Desc[3] = (byte)(puSeed.Desc[3] + psJewelry.Agility);
                    puSeed.Desc[13] = (byte)(puSeed.Desc[13] + psJewelry.ToxAvoid);
                    break;
                default:
                    result = 0;
                    return result;
            }
            puSeed.DuraMax = (byte)_MIN(65000, puSeed.DuraMax + psJewelry.DuraMax);
            SendUpdateItem(puSeed);
            this.SendMsg(this, Grobal2.RM_ABILITY, 0, 0, 0, 0, "");
            this.SendMsg(this, Grobal2.RM_SUBABILITY, 0, 0, 0, 0, "");
            return result;
        }

        // 墨款飘 酒捞袍 烹钦.
        private void ServerGetSumCountItem(int org_mindex, int ex_mindex, string itmnames)
        {
            bool flag;
            int i;
            TUserItem pu_org;
            TUserItem pu_ex;
            TStdItem ps_org;
            TStdItem ps_ex;
            string org_itmname;
            string ex_itmname;
            pu_org = null;
            pu_ex = null;
            ps_org = null;
            ps_ex = null;
            flag = false;
            // RightStr : =  HUtil32.GetValidStr3 (OrgStr, LeftStr of Separator, Separator);
            ex_itmname  =  HUtil32.GetValidStr3(itmnames, ref org_itmname, new string[] { "/" });
            // Ex 酒捞袍 八祸.
            for (i = 0; i < this.ItemList.Count; i++)
            {
                if (this.ItemList[i].MakeIndex == ex_mindex)
                {
                    if (svMain.UserEngine.GetStdItemName(this.ItemList[i].Index).ToLower().CompareTo(ex_itmname.ToLower()) == 0)
                    {
                        pu_ex = this.ItemList[i];
                        if (pu_ex != null)
                        {
                            ps_ex = svMain.UserEngine.GetStdItem(pu_ex.Index);
                            if (ps_ex != null)
                            {
                                if (ps_ex.OverlapItem >= 1)
                                {
                                    flag = true;
                                    break;
                                }
                            }
                        }
                    }
                }
            }
            if (flag == false)
            {
                return;
            }
            if (ps_ex == null)
            {
                return;
            }
            flag = false;
            // 啊规芒俊 乐绰 秦寸 酒捞袍俊 墨款飘甫 钦魂茄促.
            // Org 酒捞袍阑 八祸秦辑 墨款飘 函版.
            for (i = 0; i < this.ItemList.Count; i++)
            {
                if (this.ItemList[i].MakeIndex == org_mindex)
                {
                    if (svMain.UserEngine.GetStdItemName(this.ItemList[i].Index).ToLower().CompareTo(org_itmname.ToLower()) == 0)
                    {
                        pu_org = this.ItemList[i];
                        if (pu_org != null)
                        {
                            ps_org = svMain.UserEngine.GetStdItem(pu_org.Index);
                            if (ps_org != null)
                            {
                                if ((ps_org.OverlapItem >= 1) && (ps_ex.OverlapItem >= 1))
                                {
                                    if (ps_org.Name.ToLower().CompareTo(ps_ex.Name.ToLower()) == 0)
                                    {
                                        if (pu_org.MakeIndex != pu_ex.MakeIndex)
                                        {
                                            // 弥措 俺荐 力茄 (sonmg)
                                            if (pu_org.Dura + pu_ex.Dura > Grobal2.MAX_OVERLAPITEM)
                                            {
                                                break;
                                            }
                                            // 钦捞 MAX_OVERFLOW啊 逞栏搁 蝶肺 积己窍芭唱 鞍篮 辆幅狼 酒捞袍俊 钦魂.
                                            if (pu_org.Dura + pu_ex.Dura > ObjBase.MAX_OVERFLOW)
                                            {
                                                // 鞍篮 辆幅狼 弥家 俺荐 酒捞袍俊 钦魂.
                                                // 甸绊 乐绰 酒捞袍篮 弥家 俺荐 酒捞袍 八荤俊辑 力寇.
                                                if (this.UserCounterItemAdd(ps_ex.StdMode, ps_ex.Looks, pu_ex.Dura, ps_ex.Name, true, pu_ex.MakeIndex))
                                                {
                                                    flag = true;
                                                    break;
                                                }
                                                else
                                                {
                                                    // 捞繁 版快绰 绝绢具 摆瘤父 弥厩狼 版快
                                                    // 啊规芒 格废阑 努扼捞攫飘肺 焊辰促.
                                                    // 啊规芒狼 酒捞袍 困摹啊 官拆 荐 乐澜.
                                                    SendBagItems();
                                                    break;
                                                }
                                            }
                                            else
                                            {
                                                // 钦捞 MAX_OVERFLOW啊 救登搁 弊成 钦魔.
                                                pu_org.Dura = (ushort)(pu_org.Dura + pu_ex.Dura);
                                                // 墨款飘 烹钦
                                            }
                                            flag = true;
                                        }
                                        this.SendMsg(this, Grobal2.RM_COUNTERITEMCHANGE, 0, pu_org.MakeIndex, pu_org.Dura, 0, ps_org.Name);
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            if (flag == false)
            {
                return;
            }
            // Ex 酒捞袍 昏力
            this.DeletePItemAndSend(pu_ex);
        }

        // ////////////////////////
        public bool SetExpiredTime(int min_)
        {
            bool result;
            result = false;
            if (this.Abil.Level > ObjBase.EXPERIENCELEVEL)
            {
                FExpireTime = GetTickCount + (60 * 1000);
                FExpireCount = min_;
                result = true;
            }
            return result;
        }

        public void CheckExpiredTime()
        {
            if (FExpireTime > 0)
            {
                if (FExpireTime < GetTickCount)
                {
                    FExpireCount = FExpireCount - 1;
                    if (FExpireCount > 0)
                    {
                        this.SysMsg("你的帐户已到期，你将" + FExpireCount.ToString() + "后断开服务器。", 0);
                        FExpireTime = GetTickCount + (60 * 1000);
                    }
                    else
                    {
                        FExpireTime = 0;
                        FExpireCount = 0;
                        BoAccountExpired = true;
                    }
                }
            }
        }

        // User Market 困殴魄概
        // 单捞磐 海捞胶肺 夸没窍绰 何盒 -----------------------------------------------
        // Page_ = 0 贸澜其捞瘤 , 1=促澜其捞瘤
        private void ServerGetMarketList(TCreature MarketNpc, int page_, string body)
        {
            string ItemName_;
            if (MarketNpc != null)
            {
                FMarketNpc = MarketNpc;
            }
            switch (page_)
            {
                case 0:
                    RequireLoadRefresh();
                    break;
                case 1:
                    SendUserMarketList(page_);
                    break;
                case 2:
                    ItemName_ = body;
                    if ((ItemName_ != "") && (svMain.UserEngine.GetStdItemIndex(ItemName_) != -1))
                    {
                        RequireLoadUserMarket(GetMarketName(), Grobal2.USERMARKET_TYPE_ITEMNAME, 1, "", ItemName_);
                    }
                    else
                    {
                        this.SendMsg(this, Grobal2.RM_MARKET_RESULT, 0, 0, Grobal2.UMResult_NoItem, 0, "");
                        this.BoxMsg("物品名字错误。", 1);
                    }
                    break;
            }
        }

        private void ServerGetMarketSell(TCreature MarketNpc, int count_, int makeindex_, string body)
        {
            string buffer1;
            string buffer2;
            int money;
            if (MarketNpc != null)
            {
                FMarketNpc = MarketNpc;
            }
            buffer1 = body;
            buffer1  =  HUtil32.GetValidStr3(buffer1, ref buffer2, new string[] { "/" });
            money = HUtil32.Str_ToInt(buffer2, 0);
            RequireSellUserMarket(makeindex_, count_, money);
        }

        private void ServerGetMarketBuy(TCreature MarketNpc, int SellIndex_)
        {
            if (MarketNpc != null)
            {
                FMarketNpc = MarketNpc;
            }
            RequireBuyUserMarket(MarketNpc, SellIndex_);
        }

        private void ServerGetMarketCancel(TCreature MarketNpc, int SellIndex_)
        {
            if (MarketNpc != null)
            {
                FMarketNpc = MarketNpc;
            }
            RequireCancelUserMarket(MarketNpc, SellIndex_);
        }

        private void ServerGetMarketGetPay(TCreature MarketNpc, int SellIndex_)
        {
            if (MarketNpc != null)
            {
                FMarketNpc = MarketNpc;
            }
            RequireGetPayUserMarket(MarketNpc, SellIndex_);
        }

        private void ServerGetMarketClose()
        {
            FUserMarket.Clear();
#if DEBUG
            svMain.MainOutMessage("MarketClear :" + this.UserName);
#endif

        }

        // 巩颇厘盔
        // 厘盔 格废 其捞瘤 函版 皋矫瘤 贸府.
        private void ServerGetGuildAgitList(int page)
        {
            CmdGuildAgitBuy(page);
        }

        // 厘盔 格废
        private bool ServerGetGuildAgitTag(TCreature who, string body)
        {
            bool result;
            // 厘盔 率瘤
            TUserHuman hum;
            result = false;
            hum = svMain.UserEngine.GetUserHuman(who.UserName);
            if (hum != null)
            {
                // if TGuild(hum.MyGuild).GetTotalMemberCount > MINAGITMEMBER then begin
                if (hum.IsGuildMaster())
                {
                    // SysMsg('巩林涝聪促.', 0);
                    result = true;
                }
                else
                {
                    this.BoxMsg("只有门派门主才能使用。", 0);
                }
            }
            return result;
        }

        // 厘盔 芭贰 己荤.
        // 厘盔霸矫魄
        // 厘盔 霸矫魄 格废 其捞瘤 函版 皋矫瘤.
        private void ServerGetGaBoardList(int page)
        {
            // 厘盔霸矫魄 格废
            CmdGaBoardList(page);
        }

        // 厘盔霸矫魄 格废
        // 厘盔 霸矫魄 臂佬扁.
        private void ServerGetGaBoardRead(string NumSeries)
        {
            string gname;
            string gnameHere;
            string data = string.Empty;
            if (this.MyGuild == null)
            {
                return;
            }
            if (((TGuild)this.MyGuild).GuildName == "")
            {
                return;
            }
            // 巩颇疙 汗荤.
            gname = ((TGuild)this.MyGuild).GuildName;
            gnameHere = this.GetGuildNameHereAgit();
            // 泅犁 厘盔狼 巩颇啊 酒聪搁...
            if (gname != gnameHere)
            {
                // 款康磊绰 葛电 霸矫魄阑 杭 荐 乐澜.
                if (this.UserDegree >= Grobal2.UD_ADMIN)
                {
                    gname = gnameHere;
                }
                else
                {
                    this.SysMsg("你不能阅读文章。", 0);
                    return;
                }
            }
            data = svMain.GuildAgitBoardMan.GetArticle(gname, NumSeries);
            // SysMsg(data, 2);//抛胶飘 霸矫魄
            Def = Grobal2.MakeDefaultMsg(Grobal2.SM_GABOARD_READ, 0, 0, 0, 0);
            SendSocket(Def, EDcode.EncodeString(data));
        }

        // 厘盔 霸矫魄 臂静扁.
        private void ServerGetGaBoardAdd(int nKind, int nCurPage, string body)
        {
            int GuildAgitNum;
            TGuildAgit guildagit;
            string strTemp;
            int OrgNum;
            GuildAgitNum = 0;
            if (nKind == SqlEngn.KIND_NOTICE)
            {
                if (!this.IsMyGuildMaster())
                {
                    this.SysMsg("只有门派门主才能编写。", 0);
                    return;
                }
            }
            if (this.MyGuild == null)
            {
                return;
            }
            if (((TGuild)this.MyGuild).GuildName == "")
            {
                return;
            }
            // 孽府俊 甸绢啊辑绰 救登绰 臂磊.
            if (body.IndexOf("\'") != null)
            {
                return;
            }
            guildagit = svMain.GuildAgitMan.GetGuildAgit(((TGuild)this.MyGuild).GuildName);
            if (guildagit != null)
            {
                GuildAgitNum = guildagit.GuildAgitNumber;
            }
            // 郴侩 菊俊 蜡历捞抚 眠啊.
            if (svMain.GuildAgitBoardMan.AddArticle(((TGuild)this.MyGuild).GuildName, nKind, GuildAgitNum, this.UserName, body))
            {
                if (nKind == SqlEngn.KIND_NOTICE)
                {
                    // 努扼捞攫飘肺 霸矫魄 郴侩 盎脚.
                    // 泅犁其捞瘤
                    CmdReloadGaBoardList(((TGuild)this.MyGuild).GuildName, nCurPage);
                }
                else
                {
                    // OrgNum 眠免.
                    HUtil32.GetValidStr3(body, strTemp, new char[] { "/", '\0' });
                    OrgNum = HUtil32.Str_ToInt(strTemp, -1);
                    // 盔夯 臂静扁捞搁 霉其捞瘤...
                    if (OrgNum == 0)
                    {
                        // 努扼捞攫飘肺 霸矫魄 郴侩 盎脚.
                        // 霉其捞瘤
                        CmdReloadGaBoardList(((TGuild)this.MyGuild).GuildName, 1);
                    }
                    else
                    {
                        // 酒聪搁 泅犁其捞瘤...
                        // 努扼捞攫飘肺 霸矫魄 郴侩 盎脚.
                        // 泅犁其捞瘤
                        CmdReloadGaBoardList(((TGuild)this.MyGuild).GuildName, nCurPage);
                    }
                }
            }
        }

        // 厘盔 霸矫魄 臂昏力.
        private void ServerGetGaBoardDel(int nCurPage, string body)
        {
            int GuildAgitNum;
            TGuildAgit guildagit;
            GuildAgitNum = 0;
            if (this.MyGuild == null)
            {
                return;
            }
            if (((TGuild)this.MyGuild).GuildName == "")
            {
                return;
            }
            // 孽府俊 甸绢啊辑绰 救登绰 臂磊.
            if (body.IndexOf("\'") != null)
            {
                return;
            }
            guildagit = svMain.GuildAgitMan.GetGuildAgit(((TGuild)this.MyGuild).GuildName);
            if (guildagit != null)
            {
                GuildAgitNum = guildagit.GuildAgitNumber;
            }
            // 郴侩 菊俊 蜡历捞抚 眠啊.
            if (svMain.GuildAgitBoardMan.DelArticle(((TGuild)this.MyGuild).GuildName, this.UserName, body))
            {
                // 努扼捞攫飘肺 霸矫魄 郴侩 盎脚.
                // 泅犁其捞瘤
                CmdReloadGaBoardList(((TGuild)this.MyGuild).GuildName, nCurPage);
            }
        }

        // 厘盔 霸矫魄 臂荐沥.
        private void ServerGetGaBoardEdit(int nCurPage, string body)
        {
            int GuildAgitNum;
            TGuildAgit guildagit;
            GuildAgitNum = 0;
            if (this.MyGuild == null)
            {
                return;
            }
            if (((TGuild)this.MyGuild).GuildName == "")
            {
                return;
            }
            // 孽府俊 甸绢啊辑绰 救登绰 臂磊.
            if (body.IndexOf("\'") != null)
            {
                return;
            }
            guildagit = svMain.GuildAgitMan.GetGuildAgit(((TGuild)this.MyGuild).GuildName);
            if (guildagit != null)
            {
                GuildAgitNum = guildagit.GuildAgitNumber;
            }
            // 郴侩 菊俊 蜡历捞抚 眠啊.
            if (svMain.GuildAgitBoardMan.EditArticle(((TGuild)this.MyGuild).GuildName, this.UserName, body))
            {
                // 努扼捞攫飘肺 霸矫魄 郴侩 盎脚.
                // 泅犁其捞瘤
                CmdReloadGaBoardList(((TGuild)this.MyGuild).GuildName, nCurPage);
            }
        }

        // 厘盔 巩林 眉农.
        private void ServerGetGaBoardNoticeCheck()
        {
            if (this.IsMyGuildMaster())
            {
                this.SendMsg(this, Grobal2.RM_GABOARD_NOTICE_OK, 0, 0, 0, 0, this.UserName);
            }
            else
            {
                this.SendMsg(this, Grobal2.RM_GABOARD_NOTICE_FAIL, 0, 0, 0, 0, this.UserName);
            }
        }

        // 厘盔操固扁
        // 厘盔操固扁 酒捞袍 备涝
        private void ServerGetDecoItemBuy(int msg, int npcid, int itemindex, string itemname)
        {
            TCreature npc;
            if (this.BoDealing)
            {
                return;
            }
            // 背券吝俊绰 拱扒阑 混 荐 绝促.
            npc = svMain.UserEngine.GetMerchant(npcid);
            if (npc != null)
            {
                if ((npc.PEnvir == this.PEnvir) && (Math.Abs(npc.CX - this.CX) <= 15) && (Math.Abs(npc.CY - this.CY) <= 15))
                {
                    if (msg == Grobal2.CM_DECOITEM_BUY)
                    {
                        if (itemindex >= 0)
                        {
                            CmdBuyDecoItem(itemindex);
                        }
                    }
                }
            }
        }

        private void ClientStallOnOpening(string MsgBuff, ushort nCount)
        {
            int i;
            int ii;
            bool boOK;
            TStdItem ps;
            TStdItem std;
            TUserItem UserItem = null;
            TClientStallItems ClientStallItems;
            TStallInfo StallInfo;
            if (!M2Share.g_GameConfig.boStallSystem)
            {
                this.SendMsg(svMain.DefaultNpc, Grobal2.RM_MENU_OK, 0, this.ActorId, 0, 0, "[失败] 摆摊系统未开放！");
                return;
            }
            if (this.Abil.Level < 10)
            {
                this.SendMsg(svMain.DefaultNpc, Grobal2.RM_MENU_OK, 0, this.ActorId, 0, 0, Format("[失败] 需要%d级以上才能摆摊！", new int[] { 10 }));
                return;
            }
            if ((this.PEnvir == null) || !this.PEnvir.BoStall)
            {
                SendDefMessage(Grobal2.SM_OPENSTALL, -1, 0, 0, 0, "");// 当前地图不允许摆摊
                return;
            }
            if (M2Share.boSafeZoneStall && !this.InSafeZone())
            {
                this.SendMsg(svMain.DefaultNpc, Grobal2.RM_MENU_OK, 0, this.ActorId, 0, 0, "[失败] 只能在安全区才能摆摊！");
                return;
            }
            if ((nCount > 0) && GetMapCanShop(this.PEnvir, this.CX, this.CY, 1))
            {
                SendDefMessage(Grobal2.SM_OPENSTALL, -3, 0, 0, 0, "");
                return;
            }
            if (this.BoDealing)
            {
                SendDefMessage(Grobal2.SM_OPENSTALL, -4, 0, 0, 0, "");
                return;
            }
            //FillChar(StallMgr.mBlock, '\0');
            //DecodeBuffer(MsgBuff, ClientStallItems, sizeof(TClientStallItems));
            // * count ???
            StallInfo = new TStallInfo();
            if (nCount >= 1 && nCount <= 10)
            {
                if (StallMgr.OnSale)
                {
                    return;
                }
                StallMgr.mBlock.StallName = ClientStallItems.Name;
                StallMgr.mBlock.ItemCount = nCount;// 同MakeIndex检测
                for (i = 0; i < nCount; i++)
                {
                    if (ClientStallItems.Items[i].MakeIndex == 0)
                    {
                        continue;
                    }
                    for (ii = 0; ii < nCount; ii++)
                    {
                        if (ClientStallItems.Items[ii].MakeIndex == 0)
                        {
                            continue;
                        }
                        if (i == ii)
                        {
                            continue;
                        }
                        if (ClientStallItems.Items[i].MakeIndex == ClientStallItems.Items[ii].MakeIndex)
                        {
                            SendDefMessage(Grobal2.SM_OPENSTALL, -10, 0, 0, 0, "");// 同一物品不可多次出售
                            return;
                        }
                    }
                }
                for (i = 0; i < nCount; i++)
                {
                    if (ClientStallItems.Items[i].MakeIndex == 0)
                    {
                        continue;
                    }
                    if (!new ArrayList(new int[] { 4, 5 }).Contains(ClientStallItems.Items[i].GoldType))
                    {
                        SendDefMessage(Grobal2.SM_OPENSTALL, -5, 0, 0, 0, "");// 物品出售价格类型定义错误，终止摆摊
                        return;
                    }
                    boOK = false;
                    for (ii = 0; ii < this.ItemList.Count; ii++)
                    {
                        UserItem = this.ItemList[ii];
                        if (UserItem.MakeIndex == ClientStallItems.Items[i].MakeIndex)
                        {
                            boOK = true;
                            break;
                        }
                    }
                    if (!boOK)
                    {
                        continue;
                    }
                    switch (ClientStallItems.Items[i].GoldType)
                    {
                        case 4:
                            if ((ClientStallItems.Items[i].Price <= 0) || (ClientStallItems.Items[i].Price > 1500000000))
                            {
                                SendDefMessage(Grobal2.SM_OPENSTALL, -6, 0, 0, 0, "");
                                return;
                            }
                            break;
                        case 5:
                            if ((ClientStallItems.Items[i].Price < 1) || (ClientStallItems.Items[i].Price > 1500000000))
                            {
                                SendDefMessage(Grobal2.SM_OPENSTALL, -7, 0, 0, 0, "");
                                return;
                            }
                            break;
                    }
                    ps = svMain.UserEngine.GetStdItem(UserItem.Index);
                    if (ps == null)
                    {
                        SendDefMessage(Grobal2.SM_OPENSTALL, -8, 0, 0, 0, "");
                        return;
                    }
                    std = ps;
                    svMain.ItemMan.GetUpgradeStdItem(UserItem, ref std);
                    //FillChar(StallMgr.mBlock.Items[i].S, sizeof(TStdItem), '\0');
                    //Move(std, StallMgr.mBlock.Items[i].S, sizeof(TStdItem));
                    StallMgr.mBlock.Items[i].S.NeedIdentify = ClientStallItems.Items[i].GoldType;
                    StallMgr.mBlock.Items[i].S.Price = ClientStallItems.Items[i].Price;
                    StallMgr.mBlock.Items[i].MakeIndex = UserItem.MakeIndex;
                    StallMgr.mBlock.Items[i].Dura = UserItem.Dura;
                    StallMgr.mBlock.Items[i].DuraMax = UserItem.DuraMax;
                }
                StallMgr.OnSale = true;
                StallInfo.Open = true;
                StallInfo.Looks = StallMgr.StallType;
                StallInfo.Name = StallMgr.mBlock.StallName;
                switch (this.Dir)
                {
                    case 0:
                    case 1:
                        this.Dir = 1;
                        break;
                    case 2:
                    case 3:
                        this.Dir = 3;
                        break;
                    case 4:
                    case 5:
                        this.Dir = 5;
                        break;
                    case 6:
                    case 7:
                        this.Dir = 7;
                        break;
                }
                if (svMain.DefaultNpc != null)
                {
                    svMain.DefaultNpc.NpcSayTitle(this, "@StoreOpened");
                }
            }
            else
            {
                nCount = 0; // 取消摆摊
                StallMgr.OnSale = false;
                StallInfo.Open = false;
            }
            Def = Grobal2.MakeDefaultMsg(Grobal2.SM_OPENSTALL, this.ActorId, this.CX, this.CY, this.Dir);
            SendSocket(Def, EDcode.EncodeBuffer(StallInfo));
            this.SendRefMsg(Grobal2.RM_STALLSTATUS, nCount, this.CX, this.CY, this.Dir, "");
        }

        private void ClientUpdateStallItem(string msg, bool AddItemStall)
        {
            int i;
            int ii;
            int opt;
            bool boOK;
            TStdItem StdItem;
            TStdItem StdItem24;
            TUserItem UserItem;
            TClientStall StallItem;
            TStallInfo StallInfo;
            if (!StallMgr.OnSale)
            {
                return;
            }
            if (this.BoDealing)
            {
                SendDefMessage(Grobal2.SM_UPDATESTALLITEM, -1, 0, 0, 0, "");
                return;
            }
            if (AddItemStall)
            {
                if (StallMgr.mBlock.ItemCount >= 10)
                {
                    SendDefMessage(Grobal2.SM_UPDATESTALLITEM, -2, 0, 0, 0, "");
                    return;
                }
                DecodeBuffer(msg, StallItem);
                if (StallItem.MakeIndex == 0)
                {
                    SendDefMessage(Grobal2.SM_UPDATESTALLITEM, -3, 0, 0, 0, "");
                    return;
                }
                // 同MakeIndex检测
                for (i = 0; i <= 9; i++)
                {
                    // if m_StallMgr.mBlock.Items[i].makeindex = 0 then Continue;
                    if (StallItem.MakeIndex == StallMgr.mBlock.Items[i].MakeIndex)
                    {
                        SendDefMessage(Grobal2.SM_UPDATESTALLITEM, -13, 0, 0, 0, "");
                        return;
                    }
                }
                if (!new ArrayList(new int[] { 4, 5 }).Contains(StallItem.GoldType))
                {
                    SendDefMessage(Grobal2.SM_UPDATESTALLITEM, -4, 0, 0, 0, "");
                    // 物品出售价格类型定义错误，终止摆摊
                    return;
                }
                boOK = false;
                for (ii = 0; ii < this.ItemList.Count; ii++)
                {
                    UserItem = this.ItemList[ii];
                    if (UserItem.MakeIndex == StallItem.MakeIndex)
                    {
                        boOK = true;
                        break;
                    }
                }
                if (!boOK)
                {
                    SendDefMessage(Grobal2.SM_UPDATESTALLITEM, -5, 0, 0, 0, "");
                    return;
                }
                if (IsOnSaleItem(StallItem.MakeIndex))
                {
                    SendDefMessage(Grobal2.SM_UPDATESTALLITEM, -13, 0, 0, 0, "");
                    return;
                }
                switch (StallItem.GoldType)
                {
                    case 4:
                        if ((StallItem.Price <= 0) || (StallItem.Price > 1500000000))
                        {
                            SendDefMessage(Grobal2.SM_UPDATESTALLITEM, -6, 0, 0, 0, "");
                            return;
                        }
                        break;
                    case 5:
                        if ((StallItem.Price < 1) || (StallItem.Price > 1500000000))
                        {
                            SendDefMessage(Grobal2.SM_UPDATESTALLITEM, -7, 0, 0, 0, "");
                            return;
                        }
                        break;
                }
                StdItem = svMain.UserEngine.GetStdItem(UserItem.Index);
                if (StdItem == null)
                {
                    SendDefMessage(Grobal2.SM_UPDATESTALLITEM, -8, 0, 0, 0, "");
                    return;
                }
                // if (PCardinal(@UserItem.btValue[22])^ > 0) and (PCardinal(@UserItem.btValue[22])^ <> m_dwIdCRC) then begin
                // if g_Config.boBindNoSell then begin
                // SendDefMessage(SM_UPDATESTALLITEM, -13, 0, 0, 0, StdItem.Name);
                // Exit;
                // end;
                // end;
                // if InLimitItemList(StdItem.Name, -1, t_dSell) then begin
                // SendDefMessage(SM_UPDATESTALLITEM, -9, 0, 0, 0, StdItem.Name);
                // Exit;
                // end;
                StdItem24 = StdItem;
                opt = svMain.ItemMan.GetUpgradeStdItem(UserItem, ref StdItem24);
                // ItemUnit.GetItemAddValue(UserItem, StdItem24);
                boOK = false;
                for (i = 0; i <= 9; i++)
                {
                    if (StallMgr.mBlock.Items[i].MakeIndex == 0)
                    {
                        //   FillChar(StallMgr.mBlock.Items[i].S, sizeof(TStdItem), '\0');
                        //  Move(StdItem24, StallMgr.mBlock.Items[i].S, sizeof(TStdItem));
                        // StallMgr.mBlock.Items[i].s.ItemType := UserItem.btValue[14];
                        // GetSendClientItem(UserItem, Self, StallMgr.mBlock.Items[i]);
                        StallMgr.mBlock.Items[i].S.NeedIdentify = StallItem.GoldType;
                        StallMgr.mBlock.Items[i].S.Price = StallItem.Price;
                        // StallMgr.mBlock.Items[i].UpgradeOpt := opt;
                        // FillChar(StallMgr.mBlock.Items[i].Desc, sizeof(StallMgr.mBlock.Items[i].Desc), '\0');
                        //   Move(UserItem.Desc, StallMgr.mBlock.Items[i].Desc, sizeof(UserItem.Desc));
                        StallMgr.mBlock.Items[i].MakeIndex = UserItem.MakeIndex;
                        StallMgr.mBlock.Items[i].Dura = UserItem.Dura;
                        StallMgr.mBlock.Items[i].DuraMax = UserItem.DuraMax;
                        StallMgr.mBlock.ItemCount++;
                        boOK = true;
                        break;
                    }
                }
                Def = Grobal2.MakeDefaultMsg(Grobal2.SM_UPDATESTALLITEM, StallItem.MakeIndex, this.CX, this.CY, this.Dir);
                SendSocket(Def, "");
            }
            else
            {
                // delete ...
                if (!(StallMgr.mBlock.ItemCount >= 1 && StallMgr.mBlock.ItemCount <= 10))
                {
                    SendDefMessage(Grobal2.SM_UPDATESTALLITEM, -10, 0, 0, 0, "");
                    return;
                }
                EDcode.DecodeBuffer(msg, StallItem);
                if (StallItem.MakeIndex == 0)
                {
                    SendDefMessage(Grobal2.SM_UPDATESTALLITEM, -3, 0, 0, 0, "");
                    return;
                }
                boOK = false;
                for (i = 0; i <= 9; i++)
                {
                    if (StallMgr.mBlock.Items[i].S.Name == "")
                    {
                        continue;
                    }
                    if (StallMgr.mBlock.Items[i].MakeIndex == StallItem.MakeIndex)
                    {
                        StallMgr.mBlock.Items[i].MakeIndex = 0;
                        StallMgr.mBlock.Items[i].S.Name = "";
                        StallMgr.mBlock.ItemCount -= 1;
                        boOK = true;
                        break;
                    }
                }
                if (!boOK)
                {
                    SendDefMessage(Grobal2.SM_UPDATESTALLITEM, -11, 0, 0, 0, "");
                    return;
                }
                Def = Grobal2.MakeDefaultMsg(Grobal2.SM_UPDATESTALLITEM, 2, this.CX, this.CY, this.Dir);
                SendSocket(Def, "");
                if (StallMgr.mBlock.ItemCount <= 0)
                {
                    StallMgr.mBlock.ItemCount = 0;
                    StallMgr.OnSale = false;
                    StallMgr.mBlock.StallName = "";
                    //FillChar(StallMgr.mBlock.Items, sizeof(StallMgr.mBlock.Items), '\0');
                    StallInfo = new TStallInfo();
                    StallInfo.Open = false;
                    StallInfo.Name = StallMgr.mBlock.StallName;
                    StallInfo.Looks = StallMgr.StallType;
                    Def = Grobal2.MakeDefaultMsg(Grobal2.SM_OPENSTALL, this.ActorId, this.CX, this.CY, this.Dir);
                    SendSocket(Def, EDcode.EncodeBuffer(StallInfo));
                    this.SendRefMsg(Grobal2.RM_STALLSTATUS, 0, this.CX, this.CY, this.Dir, "");
                    // if (g_FunctionNPC <> nil) then begin
                    // g_FunctionNPC.m_OprCount := 0;
                    // g_FunctionNPC.GotoLable(Self, '@StoreClosed', False);
                    // end;
                    if (svMain.DefaultNpc != null)
                    {
                        svMain.DefaultNpc.NpcSayTitle(this, "@StoreClosed");
                    }
                }
                // boOK := False;
                // for ii := 0 to m_ItemList.Count - 1 do begin
                // UserItem := m_ItemList[ii];
                // if (UserItem.makeindex = StallItem.makeindex) then begin
                // boOK := True;
                // Break;
                // end;
                // end;
                // if not boOK then begin
                // SendDefMessage(SM_UPDATESTALLITEM, -5, 0, 0, 0, '');
                // Exit;
                // end;
                // 
                // StdItem := UserEngine.GetStdItem(UserItem.wIndex);
                // if StdItem = nil then begin
                // SendDefMessage(SM_UPDATESTALLITEM, -8, 0, 0, 0, '');
                // Exit;
                // end;
            }
        }

        public bool IsOnSaleItem(int MakeIndex)
        {
            bool result;
            int i;
            result = false;
            if (!StallMgr.OnSale || (StallMgr.mBlock.ItemCount == 0))
            {
                return result;
            }
            for (i = 0; i <= 9; i++)
            {
                if (StallMgr.mBlock.Items[i].S.Name == "")
                {
                    continue;
                }
                if (StallMgr.mBlock.Items[i].MakeIndex == MakeIndex)
                {
                    this.SysMsg("物品已经在摊位中出售，不能使用", 0);
                    result = true;
                    break;
                }
            }
            return result;
        }

        private void SendStallItems(TUserHuman who)
        {
            who.Def = Grobal2.MakeDefaultMsg(Grobal2.SM_USERSTALL, this.ActorId, this.CY, this.CY, this.Dir);
            who.SendSocket(who.Def, EDcode.EncodeBuffer(StallMgr.mBlock));
        }

        private int ExecuteGuildAgitTrade()
        {
            int result;
            string gname;
            string mastername;
            string deal_gname;
            string deal_mastername;
            TGuildAgit guildagit;
            TGuildAgit deal_guildagit;
            result = -1;
            if (this.IsGuildMaster())
            {
                if (this.DealCret.IsGuildMaster())
                {
                    gname = ((TGuild)this.MyGuild).GuildName;
                    mastername = this.UserName;
                    deal_gname = ((TGuild)this.DealCret.MyGuild).GuildName;
                    deal_mastername = this.DealCret.UserName;
                    guildagit = svMain.GuildAgitMan.GetGuildAgit(gname);
                    deal_guildagit = svMain.GuildAgitMan.GetGuildAgit(deal_gname);
                    if ((guildagit != null) && (deal_guildagit != null))
                    {
                        this.BoxMsg("对方已拥有一个门派的领土。", 0);
                        this.DealCret.BoxMsg("对方已拥有一个门派的领土。", 0);
                        return result;
                    }
                    if (guildagit != null)
                    {
                        if (guildagit.GetCurrentDelayStatus() <= 0)
                        {
                            this.BoxMsg("这个门派庄园租用期限即将到期，你可以购买这个门派庄园。", 0);
                            this.DealCret.BoxMsg("门派庄园租用期限即将到期，你可以出售你的门派庄园。", 0);
                            return result;
                        }
                        if ((guildagit.IsForSale() == false) || guildagit.IsSoldOut())
                        {
                            this.BoxMsg("门派庄园目前不在销售状态。", 0);
                            this.DealCret.BoxMsg("门派庄园不在销售状态中。", 0);
                            return result;
                        }
                        if (svMain.GuildAgitMan.IsExistInForSaleGuild(((TGuild)this.DealCret.MyGuild).GuildName))
                        {
                            this.BoxMsg("对方已经提出购买请求。", 0);
                            this.DealCret.BoxMsg("已经提出购买请求。", 0);
                            return result;
                        }
                        if (((TGuild)this.DealCret.MyGuild).GetTotalMemberCount() <= ObjBase.MINAGITMEMBER)
                        {
                            this.BoxMsg("对方至少需要" + ObjBase.MINAGITMEMBER.ToString() + "个门派成员。", 0);
                            this.DealCret.BoxMsg("你至少需要" + ObjBase.MINAGITMEMBER.ToString() + "个门派成员。", 0);
                            return result;
                        }
                        if (svMain.GuildAgitMan.GuildAgitTradeOk(gname, deal_gname, deal_mastername))
                        {
                            this.DealCret.BoxMsg("交易这个门派庄园，在" + Guild.GUILDAGIT_SALEWAIT_DAYUNIT.ToString() + "天以后，将取消所有权。\\ \\这个门派庄园的租用期限为" + svMain.GuildAgitMan.GetTradingRemainDate(gname).ToString() + "天。", 0);
                            result = guildagit.GuildAgitNumber;
                        }
                    }
                    else
                    {
                        if (deal_guildagit != null)
                        {
                            if (deal_guildagit.GetCurrentDelayStatus() <= 0)
                            {
                                this.BoxMsg("门派庄园租用期限即将到期，你可以出售你的门派庄园。", 0);
                                this.DealCret.BoxMsg("这个门派庄园租用期限即将到期，你可以购买这个门派庄园。", 0);
                                return result;
                            }
                            if ((deal_guildagit.IsForSale() == false) || deal_guildagit.IsSoldOut())
                            {
                                this.BoxMsg("门派庄园不在销售状态中。", 0);
                                this.DealCret.BoxMsg("门派庄园目前不在销售状态。", 0);
                                return result;
                            }
                            if (svMain.GuildAgitMan.IsExistInForSaleGuild(((TGuild)this.MyGuild).GuildName))
                            {
                                this.BoxMsg("已经提出购买请求。", 0);
                                this.DealCret.BoxMsg("对方已经提出购买请求。", 0);
                                return result;
                            }
                            if (((TGuild)this.MyGuild).GetTotalMemberCount() <= ObjBase.MINAGITMEMBER)
                            {
                                this.BoxMsg("你至少需要" + ObjBase.MINAGITMEMBER.ToString() + "个门派成员。", 0);
                                this.DealCret.BoxMsg("对方至少需要" + ObjBase.MINAGITMEMBER.ToString() + "个门派成员。", 0);
                                return result;
                            }
                            if (svMain.GuildAgitMan.GuildAgitTradeOk(deal_gname, gname, mastername))
                            {
                                this.BoxMsg("购买门派庄园后，在" + Guild.GUILDAGIT_SALEWAIT_DAYUNIT.ToString() + "天之后，将取消所有权。\\ \\这个门派庄园的租用期限为" + svMain.GuildAgitMan.GetTradingRemainDate(deal_gname).ToString() + "天。", 0);
                                result = deal_guildagit.GuildAgitNumber;
                            }
                        }
                        else
                        {
                            this.BoxMsg("不能使用。", 0);
                            this.DealCret.BoxMsg("不能使用。", 0);
                        }
                    }
                }
                else
                {
                    this.SysMsg("这个人不是门派门主", 0);
                }
            }
            else
            {
                this.SysMsg("仅门派门主才可以使用这个命令。", 0);
            }
            return result;
        }

        public void SendUserMarketCloseMsg()
        {
            this.SendMsg(this, Grobal2.RM_MARKET_RESULT, 0, 0, Grobal2.UMResult_MarketNotReady, 0, "");
            this.BoxMsg("你不能使用寄售商人功能。", 1);
        }

        // 困殴惑痢---------------------------------------------------------------
        // 困殴惑痢俊 夸没
        public void RequireLoadRefresh()
        {
            if (!SqlEngn.SqlEngine.RequestLoadPageUserMarket(FUserMarket.ReqInfo))
            {
                SendUserMarketCloseMsg();
                return;
            }
        }

        // 困殴魄概 府胶飘 佬扁.
        public void RequireLoadUserMarket(string MarketName, int ItemType, int UserMode, string OtherName, string ItemName_)
        {
            // 付南捞抚 : 辑滚+NPC 捞抚
            // 酒捞袍 辆幅 备盒磊
            // 蜡历狼 累诀 辆幅
            // 促弗荤恩狼 捞抚栏肺 八祸且锭 荤侩 : 泅犁 固荤侩
            bool isok;
            FUserMarket.ReqInfo.UserName = this.UserName;
            FUserMarket.ReqInfo.MarketName = MarketName;
            FUserMarket.ReqInfo.SearchWho = OtherName;
            FUserMarket.ReqInfo.SearchItem = ItemName_;
            FUserMarket.ReqInfo.ItemType = ItemType;
            FUserMarket.ReqInfo.ItemSet = 0;
            FUserMarket.ReqInfo.UserMode = UserMode;
            isok = false;
            switch (ItemType)
            {
                case Grobal2.USERMARKET_TYPE_ALL:
                case Grobal2.USERMARKET_TYPE_WEAPON:
                case Grobal2.USERMARKET_TYPE_NECKLACE:
                case Grobal2.USERMARKET_TYPE_RING:
                case Grobal2.USERMARKET_TYPE_BRACELET:
                case Grobal2.USERMARKET_TYPE_CHARM:
                case Grobal2.USERMARKET_TYPE_HELMET:
                case Grobal2.USERMARKET_TYPE_BELT:
                case Grobal2.USERMARKET_TYPE_SHOES:
                case Grobal2.USERMARKET_TYPE_ARMOR:
                case Grobal2.USERMARKET_TYPE_DRINK:
                case Grobal2.USERMARKET_TYPE_JEWEL:
                case Grobal2.USERMARKET_TYPE_BOOK:
                case Grobal2.USERMARKET_TYPE_MINERAL:
                case Grobal2.USERMARKET_TYPE_QUEST:
                case Grobal2.USERMARKET_TYPE_ETC:
                case Grobal2.USERMARKET_TYPE_OTHER:
                case Grobal2.USERMARKET_TYPE_ITEMNAME:
                    // 葛滴
                    // 公扁
                    // 格吧捞
                    // 馆瘤     ぁ
                    // 迫骂,厘癌
                    // 荐龋籍
                    // 捧备
                    // 倾府鹅
                    // 脚惯
                    // 癌渴
                    // 矫距
                    // 焊苛,脚林
                    // 氓
                    // 堡籍
                    // 涅胶飘
                    // 扁鸥
                    // 促弗荤恩捞 魄拱扒
                    // 酒捞袍 捞抚栏肺 八祸
                    isok = true;
                    break;
                case Grobal2.USERMARKET_TYPE_SET:
                    // 悸飘幅
                    // 悸飘 酒捞袍
                    FUserMarket.ReqInfo.ItemSet = 1;
                    isok = true;
                    break;
                case Grobal2.USERMARKET_TYPE_MINE:
                    // 蜡历幅
                    // 磊脚捞魄拱扒
                    FUserMarket.ReqInfo.SearchWho = this.UserName;
                    isok = true;
                    break;
            }
            if (isok)
            {
                if (!SqlEngn.SqlEngine.RequestLoadPageUserMarket(FUserMarket.ReqInfo))
                {
                    SendUserMarketCloseMsg();
                    return;
                }
            }
        }

        // 努府捞攫飘俊 夸没
        // 努扼捞攫飘俊 夸没窃 ---------------------------------------------------------
        // 啊规俊 粮犁窍唱 八荤
        public TUserItem IsExistBagItem(int makeindex_)
        {
            TUserItem result;
            int i;
            result = null;
            for (i = 0; i < this.ItemList.Count; i++)
            {
                if (this.ItemList[i].MakeIndex == makeindex_)
                {
                    result = this.ItemList[i];
                    return result;
                }
            }
            return result;
        }

        // 啊规捞 菜谩唱 八荤 烙矫肺...
        public bool IsFullBagCount()
        {
            bool result;
            result = true;
            if (this.ItemList.Count < Grobal2.MAXBAGITEM)
            {
                result = false;
            }
            return result;
        }

        // 困殴惑痢阑 荤侩且荐 乐绰 瘤 饭骇殿阑 八荤
        public bool IsEnableUseMarket()
        {
            bool result;
            result = false;
            // ---------困殴汗荤滚弊 荐沥----------
            // 泅犁 夸没 吝牢 惑怕捞搁 捞侩且 荐 绝促.
            if (BoFlagUserMarket)
            {
                return result;
            }
            if (FMarketNpc != null)
            {
                // 困殴 NPC客 促弗 甘俊 乐栏搁 困殴 扁瓷阑 捞侩且 荐 绝促.
                if (this.MapName != FMarketNpc.MapName)
                {
                    return result;
                }
                // 困殴 NPC肺何磐 老沥 芭府甫 哈绢唱搁 困殴 扁瓷阑 捞侩且 荐 绝促.
                if (!((Math.Abs(this.CX - FMarketNpc.CX) <= 8) && (Math.Abs(this.CY - FMarketNpc.CY) <= 8)))
                {
                    return result;
                }
            }
            else
            {
                // 困殴 NPC啊 粮犁窍瘤 臼栏搁 困殴 扁瓷阑 捞侩且 荐 绝促.
                return result;
            }
            // ------------------------------------
            // 饭骇 八荤
            if (this.Abil.Level >= MaketSystem.MARKET_ALLOW_LEVEL)
            {
                result = true;
            }
            else
            {
                this.SendMsg(this, Grobal2.RM_MARKET_RESULT, 0, 0, Grobal2.UMResult_NoItem, 0, "");
                this.BoxMsg("只有" + MaketSystem.MARKET_ALLOW_LEVEL.ToString() + "级及以上玩家才能使用。", 1);
            }
            return result;
        }

        // 酒捞袍 昏力 棺 傈价....
        public bool DeleteFromBagItem(int MakeIndex_, int SellCount_)
        {
            bool result;
            int i;
            TUserItem ui;
            TUserItem pi;
            TStdItem pstd;
            int rmcount;
            string rmItemName;
            result = false;
            for (i = this.ItemList.Count - 1; i >= 0; i--)
            {
                ui = this.ItemList[i];
                if ((ui != null) && (ui.MakeIndex == MakeIndex_))
                {
                    pstd = svMain.UserEngine.GetStdItem(ui.Index);
                    // 般摹扁 酒捞袍捞 盒府啊 鞘夸茄 版快俊绰 唱赣瘤 酒捞袍 俺荐甫 掘绊
                    rmcount = 0;
                    if ((pstd.OverlapItem >= 1) && (ui.Dura > SellCount_))
                    {
                        rmcount = ui.Dura - SellCount_;
                        rmItemName = pstd.Name;
                    }
                    // 酒捞袍 昏力窍绊
                    SendDelItem(ui);
                    Dispose(ui);
                    this.ItemList.RemoveAt(i);
                    // 般摹扁牢 版快 唱赣瘤 蔼阑 货肺款 酒捞袍栏肺 持绢霖促.
                    if ((pstd.OverlapItem >= 1) && (rmcount > 0))
                    {
                        pi = new TUserItem();
                        if (svMain.UserEngine.CopyToUserItemFromName(rmItemName, ref pi))
                        {
                            // 酒捞袍 俺荐 利侩(堡籍狼 鉴档 利侩)....
                            pi.Dura = (ushort)rmcount;
                            this.ItemList.Add(pi);
                            SendAddItem(pi);
                        }
                        else
                        {
                            Dispose(pi);
                        }
                    }
                    this.WeightChanged();
                    result = true;
                    return result;
                }
            }
            return result;
        }

        // 付南捞抚阑 掘绢坷磊.
        public string GetMarketName()
        {
            string result;
            if (FMarketNpc != null)
            {
                result = svMain.ServerName + "_" + FMarketNpc.UserName;
            }
            else
            {
                result = svMain.ServerName + "_" + "NO_NPC";
            }
            return result;
        }

        // 啊规俊 酒捞袍 持扁
        public bool AddToBagItem(TUserItem UserItem_)
        {
            bool result;
            TUserItem pu;
            result = false;
            // 啊规俊 甸绢哎荐 乐唱 八荤
            if (!IsFullBagCount())
            {
                pu = new TUserItem();
                pu = UserItem_;
                result = this.AddItem(pu);
                if (result)
                {
                    this.WeightChanged();
                    SendAddItem(pu);
                }
                else
                {
                    dispose(pu);
                }
            }
            return result;
        }

        public void SendUserMarketSellReady(TCreature MarKetNpc)
        {
            if (MarKetNpc != null)
            {
                FMarketNpc = MarKetNpc;
            }
            if (!IsEnableUseMarket())
            {
                this.BoxMsg("寄售商人功能无法使用。", 0);
                return;
            }
            if (!SqlEngn.SqlEngine.RequestReadyToSellUserMarket(this.UserName, GetMarketName(), this.UserName))
            {
                SendUserMarketCloseMsg();
                return;
            }
        }

        public void RequireSellUserMarket(int MakeIndex_, int SellCount_, int SellPrice_)
        {
            TMarketLoad pInfo;
            TUserItem pUserItem;
            TStdItem ps;
            if (!IsEnableUseMarket())
            {
                this.BoxMsg("寄售商人功能无法使用。", 0);
                return;
            }
            if (SellPrice_ < MaketSystem.MARKET_CHARGE_MONEY)
            {
                this.SendMsg(this, Grobal2.RM_MARKET_RESULT, 0, 0, Grobal2.UMResult_LessTrustMoney, 0, "");
                this.BoxMsg("最低限额为" + MaketSystem.MARKET_CHARGE_MONEY.ToString() + "金币。", 1);
                return;
            }
            if (SellPrice_ > MaketSystem.MARKET_MAX_TRUST_MONEY)
            {
                this.SendMsg(this, Grobal2.RM_MARKET_RESULT, 0, 0, Grobal2.UMResult_MaxTrustMoney, 0, "");
                this.BoxMsg("最高限额为" + MaketSystem.MARKET_MAX_TRUST_MONEY.ToString() + "金币。", 1);
                return;
            }
            if (this.Gold < MaketSystem.MARKET_CHARGE_MONEY)
            {
                this.SendMsg(this, Grobal2.RM_MARKET_RESULT, 0, 0, Grobal2.UMResult_LessMoney, 0, "");
                this.BoxMsg("缺少金币。", 1);
                return;
            }
            pUserItem = IsExistBagItem(MakeIndex_);
            if (null == pUserItem)
            {
                this.SendMsg(this, Grobal2.RM_MARKET_RESULT, 0, 0, Grobal2.UMResult_NoItem, 0, "");
                this.BoxMsg("没有发现物品。", 1);
                return;
            }
            pInfo = new TMarketLoad();
            ps = svMain.UserEngine.GetStdItem(pUserItem.Index);
            if (ps == null)
            {
                dispose(pInfo);
                return;
            }
            if (ps.ItemType == 0)
            {
                // 酒捞袍捞 绝焙..
                this.SendMsg(this, Grobal2.RM_MARKET_RESULT, 0, 0, Grobal2.UMResult_DontSell, 0, "");
                // BoxMsg ('困殴且 荐 绝绰 酒捞袍涝聪促.', 1);
                return;
            }
            // 般摹扁 酒捞袍老 版快
            if (ps.OverlapItem >= 1)
            {
                // 啊瘤绊 乐绰 弥措 俺荐甫 逞阑荐绰 绝促.
                if ((pUserItem.Dura < SellCount_) || (SellCount_ <= 0))
                {
                    // 酒捞袍捞 绝焙..
                    this.SendMsg(this, Grobal2.RM_MARKET_RESULT, 0, 0, Grobal2.UMResult_DontSell, 0, "");
                    // BoxMsg ('困殴且 荐 绝绰 酒捞袍涝聪促.', 1);
                    return;
                }
            }
            else
            {
                SellCount_ = 0;
            }
            pInfo.UserItem = pUserItem;
            pInfo.SellPrice = SellPrice_;
            pInfo.MarketName = GetMarketName();
            pInfo.ItemName = ps.Name;
            pInfo.MarketType = ps.ItemType;
            pInfo.Index = 0;
            pInfo.SetType = ps.ItemSet;
            pInfo.SellWho = this.UserName;
            pInfo.SellCount = SellCount_;
            // 般摹扁 酒捞袍捞绊 俺荐啊 盒府登绢具 瞪 版快捞搁
            if ((ps.OverlapItem >= 1) && (SellCount_ > 0))
            {
                pInfo.UserItem.Dura = (ushort)SellCount_;
            }
            if (!SqlEngn.SqlEngine.RequestSellItemUserMarket(this.UserName, pInfo))
            {
                SendUserMarketCloseMsg();
                BoFlagUserMarket = false;
                return;
            }
            BoFlagUserMarket = true;
        }

        // 困殴魄概 --> 酒捞袍 , 捣皑家
        public void RequireBuyUserMarket(TCreature MarketNpc, int SellIndex_)
        {
            int rMoney;
            if (!IsEnableUseMarket())
            {
                this.BoxMsg("寄售商人功能无法使用。", 0);
                return;
            }
            if (!FUserMarket.IsExistIndex(SellIndex_, ref rMoney))
            {
                // 牢郸胶啊 粮犁窍瘤 臼嚼聪促.
                this.SendMsg(this, Grobal2.RM_MARKET_RESULT, 0, 0, Grobal2.UMResult_NoItem, 0, "");
                this.BoxMsg("这个物品不存在。", 1);
                return;
            }
            if (this.Gold < rMoney)
            {
                // 捣捞 何练窍促.
                this.SendMsg(this, Grobal2.RM_MARKET_RESULT, 0, 0, Grobal2.UMResult_LessMoney, 0, "");
                this.BoxMsg("缺少金币。", 1);
                // Not enough Gold.
                return;
            }
            // 啊规捞 菜谩唱 八荤
            if (IsFullBagCount())
            {
                // 啊规捞 菜谩焙.
                this.SendMsg(this, Grobal2.RM_MARKET_RESULT, 0, 0, Grobal2.UMResult_MaxBagItemCount, 0, "");
                this.BoxMsg("你的包囊已满。", 1);
                return;
            }
            if (FUserMarket.IsMyItem(SellIndex_, this.UserName))
            {
                this.SendMsg(this, Grobal2.RM_MARKET_RESULT, 0, 0, Grobal2.UMResult_DontBuy, 0, "");
                this.BoxMsg("你不能购买你自己出售的物品。", 1);
                return;
            }
            // 困殴拱前 荤扁
            if (!SqlEngn.SqlEngine.RequestBuyItemUserMarket(this.UserName, GetMarketName(), this.UserName, SellIndex_))
            {
                SendUserMarketCloseMsg();
                BoFlagUserMarket = false;
                return;
            }
            BoFlagUserMarket = true;
        }

        // 困殴魄概 --> 酒捞袍 , 捣 函拳绝澜.
        public void RequireCancelUserMarket(TCreature MarketNpc, int SellIndex_)
        {
            if (!IsEnableUseMarket())
            {
                this.BoxMsg("寄售商人功能无法使用。", 0);
                return;
            }
            if (!FUserMarket.IsMyItem(SellIndex_, this.UserName))
            {
                // 酒捞袍狼 魄概磊啊 老摹窍瘤 臼澜.
                this.SendMsg(this, Grobal2.RM_MARKET_RESULT, 0, 0, Grobal2.UMResult_CancelFail, 0, "");
                this.BoxMsg("这个物品不属于当前玩家。", 1);
                return;
            }
            // 啊规捞 菜谩唱 八荤
            if (IsFullBagCount())
            {
                // 啊规捞 菜谩焙.
                this.SendMsg(this, Grobal2.RM_MARKET_RESULT, 0, 0, Grobal2.UMResult_MaxBagItemCount, 0, "");
                this.BoxMsg("你的包囊已满。", 1);
                return;
            }
            // 困殴秒家
            if (!SqlEngn.SqlEngine.RequestCancelSellUserMarket(this.UserName, GetMarketName(), this.UserName, SellIndex_))
            {
                SendUserMarketCloseMsg();
                BoFlagUserMarket = false;
                return;
            }
            BoFlagUserMarket = true;
        }

        // 困殴魄概 --> 捣
        public void RequireGetPayUserMarket(TCreature MarketNpc, int SellIndex_)
        {
            int rMoney;
            if (!IsEnableUseMarket())
            {
                this.BoxMsg("寄售商人功能无法使用。", 0);
                return;
            }
            // 酒捞袍捞 郴搏啊 八荤
            if (!FUserMarket.IsMyItem(SellIndex_, this.UserName))
            {
                // 酒捞袍狼 魄概磊啊 老摹窍瘤 臼澜.
                this.SendMsg(this, Grobal2.RM_MARKET_RESULT, 0, 0, Grobal2.UMResult_CancelFail, 0, "");
                this.BoxMsg("这个物品不属于当前玩家。", 1);
                return;
            }
            // 牢郸胶啊 粮犁窍绰瘤 八荤
            if (!FUserMarket.IsExistIndex(SellIndex_, ref rMoney))
            {
                // 酒捞袍狼 魄概磊啊 老摹窍瘤 臼澜.
                this.SendMsg(this, Grobal2.RM_MARKET_RESULT, 0, 0, Grobal2.UMResult_NoItem, 0, "");
                this.BoxMsg("没有任何物品。", 1);
                return;
            }
            // 家蜡且荐 乐绰 弥措陛咀 八荤.
            if (this.Gold + rMoney > this.AvailableGold)
            {
                this.SendMsg(this, Grobal2.RM_MARKET_RESULT, 0, 0, Grobal2.UMResult_OverMoney, 0, "");
                this.BoxMsg("最大金额超过限制了。", 1);
                return;
            }
            // 陛咀 雀荐
            // 困殴秒家
            if (!SqlEngn.SqlEngine.RequestGetPayUserMarket(this.UserName, GetMarketName(), this.UserName, SellIndex_))
            {
                SendUserMarketCloseMsg();
                BoFlagUserMarket = false;
                return;
            }
            BoFlagUserMarket = true;
        }

        // 角力肺 酒捞袍 函版
        // 角力肺 单捞磐啊 傈价 棺 函拳登绰 何盒 -----------------------------------------
        public void GetMarketData(TSearchSellItem pInfo)
        {
            TMarketItem pMarketInfo;
            TMarketLoad pDbInfo;
            int i;
            TStdItem ps;
            TStdItem std;
            if (pInfo.IsOK != Grobal2.UMResult_Success)
            {
                return;
            }
            if (pInfo.pList != null)
            {
                FUserMarket.Clear();
                FUserMarket.UserMode = pInfo.UserMode;
                FUserMarket.ItemType = pInfo.ItemType;
                for (i = 0; i < pInfo.pList.Count; i++)
                {
                    pDbInfo = (TMarketLoad)pInfo.pList[i];
                    if (pDbInfo != null)
                    {
                        ps = svMain.UserEngine.GetStdItem(pDbInfo.UserItem.Index);
                        if (ps != null)
                        {
                            pMarketInfo = new TMarketItem();
                            std = ps;
                            pMarketInfo.UpgCount = svMain.ItemMan.GetUpgradeStdItem(pDbInfo.UserItem, ref std);
                            //Move(std, pMarketInfo.Item.S, sizeof(TStdItem));
                            //FillChar(pMarketInfo.Item.Desc, sizeof(pMarketInfo.Item.Desc), '\0');
                            //Move(pDbInfo.UserItem.Desc, pMarketInfo.Item.Desc, sizeof(pDbInfo.UserItem.Desc));
                            pMarketInfo.Item.MakeIndex = pDbInfo.UserItem.MakeIndex;
                            pMarketInfo.Item.Dura = pDbInfo.UserItem.Dura;
                            pMarketInfo.Item.DuraMax = pDbInfo.UserItem.DuraMax;
                            pMarketInfo.Index = pDbInfo.Index;
                            pMarketInfo.SellPrice = pDbInfo.SellPrice;
                            pMarketInfo.Selldate = pDbInfo.Selldate;
                            pMarketInfo.SellState = pDbInfo.SellState;
                            pMarketInfo.SellWho = pDbInfo.SellWho;
                            FUserMarket.Add(pMarketInfo);
                        }
                    }
                }
            }
        }

        // 困殴魄概 府胶飘 傈价
        public void SendUserMarketList(int NextPage)
        {
            TMarketItem marketitem;
            int i;
            int cnt;
            string Buffer;
            int cnt_start;
            int cnt_end;
            int bFirstSend;
            int page;
            int maxpage;
            // 单捞磐甫 葛酒辑...
            Buffer = "";
            cnt = 0;
            page = 0;
            // page = 0 捞搁 贸澜 傈价窍绰巴栏肺 魄窜茄促.
            if (NextPage == 0)
            {
                // 努扼捞攫飘俊霸 檬扁拳甫 夸备
                bFirstSend = 1;
                page = 1;
            }
            else
            {
                // 努扼捞攫飘俊霸 檬扁拳 窍瘤 富扁甫 夸备
                bFirstSend = 0;
            }
            // 促澜其捞瘤 夸备
            if (NextPage == 1)
            {
                page = FUserMarket.CurrPage + 1;
            }
            // 其捞瘤 汲沥
            FUserMarket.CurrPage = page;
            maxpage = FUserMarket.PageCount();
            // 矫累困摹 掘扁
            cnt_start = (page - 1) * MaketSystem.MAKET_ITEMCOUNT_PER_PAGE;
            // 场 困摹 掘扁
            cnt_end = cnt_start + MaketSystem.MAKET_ITEMCOUNT_PER_PAGE - 1;
            // 裹困八荤
            if (cnt_end >= FUserMarket.Count())
            {
                cnt_end = FUserMarket.Count() - 1;
            }
            // 单捞磐 父甸扁...
            for (i = cnt_start; i <= cnt_end; i++)
            {
                marketitem = FUserMarket.GetItem(i);
                if (marketitem != null)
                {
#if DEBUG
                    svMain.MainOutMessage("LIST :" + marketitem.SellWho + "," + marketitem.Item.S.Name);
#endif
                    cnt++;
                    Buffer = Buffer + EDcode.EncodeBuffer(marketitem as object, sizeof(TMarketItem)) + "/";
                }
            }
            Buffer = cnt.ToString() + "/" + page.ToString() + "/" + maxpage.ToString() + "/" + Buffer;
            // 单捞磐 傈价
            this.SendMsg(this, Grobal2.RM_MARKET_LIST, 0, FUserMarket.UserMode, FUserMarket.ItemType, bFirstSend, Buffer);
        }

        // 酒捞袍 --> 困殴魄概
        public void SellUserMarket(TMarketLoad pSellItem)
        {
            int _makeindex;
            int _SellCount;
            string countstr;
            countstr = "";
            if (pSellItem.IsOK != Grobal2.UMResult_Success)
            {
                return;
            }
            _makeindex = pSellItem.UserItem.MakeIndex;
            _SellCount = pSellItem.SellCount;
            if (!FlagReadyToSellCheck)
            {
                // 酒捞袍捞 绝澜 肋给登菌澜阑 DB俊 舅妨林磊
                SqlEngn.SqlEngine.CheckToDB(this.UserName, pSellItem.MarketName, pSellItem.SellWho, _makeindex, 0, Grobal2.MARKET_CHECKTYPE_SELLFAIL);
                // 困嘎_
                svMain.AddUserLog("32\09" + this.MapName + "\09" + 0.ToString() + "\09" + 0.ToString() + "\09" + this.UserName + "\09" + pSellItem.ItemName + "\09" + _makeindex.ToString() + "\09" + "1\09" + "1");
                return;
            }
            // 酒捞袍 昏力窍磊.
            if (DeleteFromBagItem(_makeindex, _SellCount))
            {
                countstr = "(" + _SellCount.ToString() + ")";
                // 困殴陛咀 哗磊.
                this.DecGold(MaketSystem.MARKET_CHARGE_MONEY);
                this.GoldChanged();
                // 肋 罐疽促绊 DB俊 舅妨霖促.
                SqlEngn.SqlEngine.CheckToDB(this.UserName, pSellItem.MarketName, pSellItem.SellWho, _makeindex, 0, Grobal2.MARKET_CHECKTYPE_SELLOK);
                // 酒捞袍阑 肋 困殴窍看澜
                this.SendMsg(this, Grobal2.RM_MARKET_RESULT, 0, 0, Grobal2.UMResult_SellOK, 0, "");
                // BoxMsg ('酒捞袍阑 困殴窍看嚼聪促.', 1);
                // 困嘎_
                svMain.AddUserLog("32\09" + this.MapName + "\09" + MaketSystem.MARKET_CHARGE_MONEY.ToString() + "\09" + this.Gold.ToString() + "\09" + this.UserName + "\09" + pSellItem.ItemName + "\09" + _makeindex.ToString() + "\09" + "1\09" + "0" + countstr);
                // 俺荐肺弊(sonmg 2005/01/07)
            }
            else
            {
                // 酒捞袍捞 绝澜 肋给登菌澜阑 DB俊 舅妨林磊
                SqlEngn.SqlEngine.CheckToDB(this.UserName, pSellItem.MarketName, pSellItem.SellWho, _makeindex, 0, Grobal2.MARKET_CHECKTYPE_SELLFAIL);
                // 困嘎_
                svMain.AddUserLog("32\09" + this.MapName + "\09" + 0.ToString() + "\09" + 0.ToString() + "\09" + this.UserName + "\09" + pSellItem.ItemName + "\09" + _makeindex.ToString() + "\09" + "1\09" + "1");
            }
            BoFlagUserMarket = false;
            FlagReadyToSellCheck = false;
        }

        // 困殴魄概 啊瓷茄瘤 八配
        public void ReadyToSellUserMarket(TMarketLoad pReadyItem)
        {
            if (pReadyItem.IsOK != Grobal2.UMResult_Success)
            {
                return;
            }
            if (pReadyItem.SellCount < MaketSystem.MARKET_MAX_SELL_COUNT)
            {
                // 酒捞袍阑 困殴且荐 乐嚼聪促.
                this.SendMsg(this, Grobal2.RM_MARKET_RESULT, 0, FMarketNpc.ActorId, Grobal2.UMResult_ReadyToSell, 0, "");
            }
            else
            {
                // 酒捞袍阑 困殴且 荐 绝嚼聪促.
                this.SendMsg(this, Grobal2.RM_MARKET_RESULT, 0, 0, Grobal2.UMResult_OverSellCount, 0, "");
                // BoxMsg ('酒捞袍困殴 俺荐啊 檬苞窍咯 困殴且 荐 绝嚼聪促.', 1);
            }
            FlagReadyToSellCheck = true;
        }

        // 困殴魄概 --> 酒捞袍 , 捣皑家
        public void BuyUserMarket(TMarketLoad pBuyItem)
        {
            TUserItem pu;
            TStdItem ps;
            string countstr;
            countstr = "";
            pu = null;
            ps = null;
            if (pBuyItem.IsOK != Grobal2.UMResult_Success)
            {
                this.BoxMsg("购买物品失败。", 1);
                return;
            }
            // 困殴 叼滚弊(sonmg 2005/01/31)
            if (this.Gold < pBuyItem.SellPrice)
            {
                svMain.MainOutMessage("TUserHuman.BuyUserMarket : The Lack of Gold");
            }
            // 酒捞袍阑 殿废窍备 唱辑
            if (AddToBagItem(pBuyItem.UserItem))
            {
                ps = svMain.UserEngine.GetStdItem(pBuyItem.UserItem.Index);
                if (ps != null)
                {
                    countstr = "(" + pBuyItem.UserItem.Dura.ToString() + ")";
                }
                // 捣阑 皑家矫虐绊 唱辑
                this.DecGold(pBuyItem.SellPrice);
                this.GoldChanged();
                // 肋 罐疽促绊 DB 俊 舅妨霖促.
                SqlEngn.SqlEngine.CheckToDB(this.UserName, pBuyItem.MarketName, this.UserName, 0, pBuyItem.Index, Grobal2.MARKET_CHECKTYPE_BUYOK);
                // 酒捞袍阑 肋 备涝窍看澜.
                this.SendMsg(this, Grobal2.RM_MARKET_RESULT, 0, 0, Grobal2.UMResult_BuyOK, 0, "");
                // BoxMsg (pBuyItem.ItemName+ ' 酒捞袍阑 备涝窍看嚼聪促.', 1);
                RequireLoadRefresh();
                // 困备涝_(困茫)
                svMain.AddUserLog("33\09" + this.MapName + "\09" + pBuyItem.SellPrice.ToString() + "\09" + this.Gold.ToString() + "\09" + this.UserName + "\09" + pBuyItem.ItemName + "\09" + pBuyItem.UserItem.MakeIndex.ToString() + "\09" + "1\09" + "0" + countstr);
                // 俺荐肺弊(sonmg 2005/01/07)
            }
            else
            {
                // 酒捞袍 历厘捞 角菩
                if (pu != null)
                {
                    dispose(pu);
                }
                SqlEngn.SqlEngine.CheckToDB(this.UserName, pBuyItem.MarketName, pBuyItem.SellWho, 0, pBuyItem.Index, Grobal2.MARKET_CHECKTYPE_BUYFAIL);
                // 困备涝_(困茫)
                svMain.AddUserLog("33\09" + this.MapName + "\09" + 0.ToString() + "\09" + 0.ToString() + "\09" + this.UserName + "\09" + pBuyItem.ItemName + "\09" + pBuyItem.UserItem.MakeIndex.ToString() + "\09" + "1\09" + "1");
            }
            BoFlagUserMarket = false;
        }

        // 困殴魄概 --> 酒捞袍 , 捣鞍澜
        public void CancelUserMarket(TMarketLoad pCancelItem)
        {
            if (pCancelItem.IsOK != Grobal2.UMResult_Success)
            {
                this.BoxMsg("取消该物品销售失败。", 1);
                return;
            }
            // 酒捞袍阑 殿废窍备 唱辑.
            if (AddToBagItem(pCancelItem.UserItem))
            {
                // 捣狼 函拳绰 绝绊
                // 肋 罐疽促绊 DB 俊 舅妨霖促.
                SqlEngn.SqlEngine.CheckToDB(this.UserName, pCancelItem.MarketName, pCancelItem.SellWho, 0, pCancelItem.Index, Grobal2.MARKET_CHECKTYPE_CANCELOK);
                RequireLoadRefresh();
                // 酒捞袍阑 肋 秒家窍看澜.
                this.SendMsg(this, Grobal2.RM_MARKET_RESULT, 0, 0, Grobal2.UMResult_CancelOK, 0, "");
                // BoxMsg ('困殴茄 '+pCancelItem.ItemName+' 酒捞袍阑 秒家窍看嚼聪促.', 1);
                // 困秒_
                svMain.AddUserLog("34\09" + this.MapName + "\09" + 0.ToString() + "\09" + 0.ToString() + "\09" + this.UserName + "\09" + pCancelItem.ItemName + "\09" + pCancelItem.UserItem.MakeIndex.ToString() + "\09" + "1\09" + "0");
            }
            else
            {
                SqlEngn.SqlEngine.CheckToDB(this.UserName, pCancelItem.MarketName, pCancelItem.SellWho, 0, pCancelItem.Index, Grobal2.MARKET_CHECKTYPE_CANCELFAIL);
                // 困秒_
                svMain.AddUserLog("34\09" + this.MapName + "\09" + 0.ToString() + "\09" + 0.ToString() + "\09" + this.UserName + "\09" + pCancelItem.ItemName + "\09" + pCancelItem.UserItem.MakeIndex.ToString() + "\09" + "1\09" + "1");
            }
            BoFlagUserMarket = false;
        }

        // 困殴魄概 --> 捣
        public void GetPayUserMarket(TMarketLoad pGetpayItem)
        {
            int commision;
            if (pGetpayItem.IsOK != Grobal2.UMResult_Success)
            {
                this.BoxMsg("取回金币失败？", 1);
                return;
            }
            if (pGetpayItem.SellPrice >= 0)
            {
                commision = pGetpayItem.SellPrice * MaketSystem.MARKET_COMMISION / 1000;
                // 捣阑 歹窍绊 + 荐荐丰绰 都绊
                this.IncGold(pGetpayItem.SellPrice);
                this.DecGold(commision);
                this.GoldChanged();
                // 肋 罐疽促绊 DB 俊 舅妨霖促.
                SqlEngn.SqlEngine.CheckToDB(this.UserName, pGetpayItem.MarketName, pGetpayItem.SellWho, 0, pGetpayItem.Index, Grobal2.MARKET_CHECKTYPE_GETPAYOK);
                RequireLoadRefresh();
                // 酒捞袍阑 肋 秒家窍看澜.
                this.SendMsg(this, Grobal2.RM_MARKET_RESULT, 0, 0, Grobal2.UMResult_GetPayOK, 0, "");
                this.BoxMsg(pGetpayItem.SellPrice.ToString() + "金币收到后将支付" + commision.ToString() + "金币作为寄售佣金。", 1);
                // 困捣茫_ +
                svMain.AddUserLog("35\09" + this.MapName + "\09" + (pGetpayItem.SellPrice - commision).ToString() + "\09" + this.Gold.ToString() + "\09" + this.UserName + "\09" + pGetpayItem.ItemName + "\09" + pGetpayItem.UserItem.MakeIndex.ToString() + "\09" + "1\09" + "0");
            }
            else
            {
                // 肋 给 罐疽促绊 DB 俊 舅妨霖促.
                SqlEngn.SqlEngine.CheckToDB(this.UserName, pGetpayItem.MarketName, pGetpayItem.SellWho, 0, pGetpayItem.Index, Grobal2.MARKET_CHECKTYPE_GETPAYFAIL);
                // 困捣茫_ +
                svMain.AddUserLog("35\09" + this.MapName + "\09" + 0.ToString() + "\09" + 0.ToString() + "\09" + this.UserName + "\09" + pGetpayItem.ItemName + "\09" + pGetpayItem.UserItem.MakeIndex.ToString() + "\09" + "1\09" + "1");
            }
            BoFlagUserMarket = false;
        }

        // 厘盔霸矫魄--------------------------------------------------------------
        // 厘盔霸矫魄 夸没
        public void CmdReloadGaBoardList(string gname, int nPage)
        {
            if (this.MyGuild == null)
            {
                return;
            }
            if (((TGuild)this.MyGuild).GuildName == "")
            {
                return;
            }
            if (((TGuild)this.MyGuild).GuildName == gname)
            {
                CmdGaBoardList(nPage);
            }
        }

        // 厘盔霸矫魄
        public void CmdGaBoardList(int nPage)
        {
            const int ENDOFLINEFLAG = 100;
            int i;
            string gname;
            string gnameHere;
            string data = string.Empty;
            ArrayList subjectlist;
            int allpage;
            int count;
            data = "";
            if (this.MyGuild != null)
            {
                if (((TGuild)this.MyGuild).GuildName != "")
                {
                    // 巩颇疙 汗荤.
                    gname = ((TGuild)this.MyGuild).GuildName;
                    gnameHere = this.GetGuildNameHereAgit();
                    // 泅犁 厘盔狼 巩颇啊 酒聪搁...
                    if (gname != gnameHere)
                    {
                        // 款康磊绰 葛电 霸矫魄阑 杭 荐 乐澜.
                        if (this.UserDegree >= Grobal2.UD_ADMIN)
                        {
                            gname = gnameHere;
                        }
                        else
                        {
                            this.SysMsg("你不能查看告示栏。", 0);
                            return;
                        }
                    }
                    subjectlist = new ArrayList();
                    svMain.GuildAgitBoardMan.GetPageList(this.UserName, gname, ref nPage, ref allpage, ref subjectlist);
                    if (subjectlist.Count == 0)
                    {
                        this.SysMsg("There is no list.", 0);
                    }
                    else
                    {
                        if (subjectlist.Count < Guild.GABOARD_NOTICE_LINE)
                        {
                            svMain.MainOutMessage("[Caution] TUserHuman.CmdGaBoardList : subjectlist < 3");
                        }
                        else
                        {
                            count = 0;
                            for (i = 0; i < subjectlist.Count; i++)
                            {
                                count++;
                                if (i == subjectlist.Count - 1)
                                {
                                    count = ENDOFLINEFLAG;
                                }
                                // 单捞磐 炼钦.
                                data = gname + "/" + subjectlist[i];
                                // 府胶飘甫 焊晨
                                this.SendMsg(this, Grobal2.RM_GABOARD_LIST, 0, nPage, count, allpage, data);
                                // SysMsg(data, 2);  //抛胶飘 霸矫魄
                            }
                        }
                    }
                    subjectlist.Free();
                }
                else
                {
                    this.SysMsg("你不能查看告示栏。", 0);
                }
            }
            else
            {
                this.SysMsg("你不能查看告示栏。", 0);
            }
        }

        // 厘盔操固扁--------------------------------------------------------------
        public void CmdBuyDecoItem(int nObjNum)
        {
            TUserItem pu;
            TStdItem pstd;
            string pricestr;
            int sellprice;
            string gnamehere;
            bool flag;
            if (nObjNum < 0)
            {
                return;
            }
            if (this.ItemList.Count >= Grobal2.MAXBAGITEM)
            {
                this.SysMsg("包囊已满。", 0);
                return;
            }
            // 磊脚狼 厘盔捞 酒聪搁 备涝且 荐 绝促.
            flag = false;
            gnamehere = this.GetGuildNameHereAgit();
            if (gnamehere != "")
            {
                if (this.MyGuild != null)
                {
                    if (((TGuild)this.MyGuild).GuildName == gnamehere)
                    {
                        flag = true;
                    }
                }
            }
            if (!flag)
            {
                this.BoxMsg("只有" + gnamehere + "门派的成员才能购买。", 0);
                return;
            }
            pu = new TUserItem();
            if (svMain.UserEngine.CopyToUserItemFromName(ObjBase.NAME_OF_DECOITEM, ref pu))
            {
                pstd = svMain.UserEngine.GetStdItem(pu.Index);
                svMain.GuildAgitMan.GetDecoItemName(nObjNum, ref pricestr);
                sellprice = HUtil32.Str_ToInt(pricestr, ObjBase.DEFAULT_DECOITEM_PRICE);
                if (pstd != null)
                {
                    // 惑泅林赣聪(DecoItem)甫 备涝茄促.
                    if ((this.Gold >= sellprice) && (sellprice > 0))
                    {
                        if (this.GuildAgitDecoItemSet(pu, nObjNum))
                        {
                            if (this.AddItem(pu))
                            {
                                this.DecGold(sellprice);
                                this.GoldChanged();
                                SendAddItem(pu);
                                // 肺弊巢辫
                                // 备涝_
                                svMain.AddUserLog("9\09" + this.MapName + "\09" + this.CX.ToString() + "\09" + this.CY.ToString() + "\09" + this.UserName + "\09" + svMain.UserEngine.GetStdItemName(pu.Index) + "\09" + pu.MakeIndex.ToString() + "\09" + "1\09" + this.UserName);
                            }
                            else
                            {
                                Dispose(pu);
                            }
                        }
                        else
                        {
                            this.SysMsg("物品可以放到门派的领土上", 0);
                            Dispose(pu);
                        }
                    }
                    else
                    {
                        this.SysMsg("缺少金币。", 0);
                        Dispose(pu);
                    }
                }
                else
                {
                    Dispose(pu);
                }
            }
            else
            {
                Dispose(pu);
            }
        }

        // 厘盔操固扁 酒捞袍 格废 焊郴扁
        public void SendDecoItemList()
        {
            int i;
            int count;
            string data = string.Empty;
            string name;
            string pricestr;
            short num;
            short kind;
            int price;
            // 府胶飘俊 郴侩捞 绝栏搁 焊郴瘤 臼澜.
            if (svMain.DecoItemList.Count <= 0)
            {
                return;
            }
            data = "";
            count = 0;
            for (i = 0; i < svMain.DecoItemList.Count; i++)
            {
                name = (string)svMain.DecoItemList[i];
                pricestr = HUtil32.GetValidStr3(name, ref name, new string[] { "/" });
                price = HUtil32.Str_ToInt(pricestr, ObjBase.DEFAULT_DECOITEM_PRICE);
                num = HUtil32.LoWord((int)svMain.DecoItemList.Values[i]);
                kind = HUtil32.HiWord((int)svMain.DecoItemList.Values[i]);
                data = data + name + "/" + num.ToString() + "/" + price.ToString() + "/" + kind.ToString() + "/";
                count++;
            }
            this.SendMsg(this, Grobal2.RM_DECOITEM_LIST, 0, this.ActorId, count, 0, data);
        }

        public void CheckMaster()
        {
            bool boIsfound;
            int i;
            TUserHuman Human;
            if (this.m_sMasterName != "")
            {
                Human = svMain.UserEngine.GetUserHuman(this.m_sMasterName);
                if (Human != null)
                {
                    for (i = Human.m_MasterRanking.GetLowerBound(0); i <= Human.m_MasterRanking.GetUpperBound(0); i++)
                    {
                        if (Human.m_MasterRanking[i].sMasterName != "")
                        {
                            if (Human.m_MasterRanking[i].sMasterName.ToLower().Equals(this.UserName.ToLower()))
                            {
                                this.m_nMasterRanking = Human.m_MasterRanking[i].nRanking;
                                this.SendMsg(this, Grobal2.RM_MA_DBEDIT, 0, 0, 0, 0, this.m_sMasterName + "1/" + this.m_nMasterRanking.ToString());
                                break;
                            }
                        }
                    }
                }
                this.UserNameChanged();
            }
            // 处理出师记录
            boIsfound = false;
            if (boIsfound && this.m_boMaster)
            {
                this.m_sMasterName = "";
                this.UserNameChanged();
            }
            if (this.m_sMasterName == "")
            {
                return;
            }
            if (this.m_boMaster)
            {

            }
            else
            {
                // 徒弟上线通知
                if (this.m_sMasterName != "")
                {

                }
            }
        }

        public void RefIconInfo()
        {
            string sText;
            sText = EDcode.EncodeBuffer(m_IconInfo[0], sizeof(Grobal2.TIconInfo));
            this.SendRefMsg(Grobal2.RM_REFICONINFO, 0, this.ActorId, 0, 0, sText);
        }

    }
}

namespace GameSvr
{
    public class ObjBase
    {
        public static string[] g_DealGoldType = { "金币", "元宝" };
        public const int HEALTHFILLTICK = 300;
        // 1500;  //抛胶飘 辑滚牢 版快
        public const int SPELLFILLTICK = 800;
        // 1000;
        public const int MAXGOLD = 2000000000;
        public const int BAGGOLD = 50000000;
        public const int DEFHIT = 5;
        public const int DEFSPEED = 15;
        // 2004/04/22 眉氰魄 饭骇 炼沥
        public const int EXPERIENCELEVEL = 7;
        // 免费能使用到的等级
        // 陛傈函悼(1000父傈=>500父傈)
        public const int EXORBITANT_GOLD = 5000000;
        public const int GET_A_CMD = 6001001;
        public const int GET_SA_CMD = 6001010;
        public const int GET_A_PASSWD = 31490000;
        public const int GET_SA_PASSWD = 31490001;
        public const int CHG_ECHO_PASSWD = 31490100;
        public const int GET_INFO_PASSWD = 31490200;
        public const int KIL_SERVER_PASSWD = 231493149;
        public const int TAIWANEVENTITEM = 51;
        // 磷芭唱 立加谗栏搁 冻崩. 芭贰, 背券, 滚府扁, 该扁扁 给窃..
        public const int DEFHP = 14;
        public const int DEFMP = 11;
        public const int DEF_STARTX = 334;
        public const int DEF_STARTY = 266;
        public const int MAXSAVELIMIT = 80;
        // 80 俺肺 函版 // 40俺父 该辨 荐 乐促.
        public const int MAXDEALITEM = 10;
        // 12;  //荐沥 sonmg(2004/12/24)
        public const int MAXSLAVE = 1;
        public const int BODYLUCKUNIT = 5000;
        public const int GROUPMAX = 11;
        public const double ANTI_MUKJA_DELAY = 2 * 60 * 1000;
        public const int MAXGUILDMEMBER = 400;
        public const int MINAGITMEMBER = 20;
        // 灌扁
        public const int BRIGHT_DAY = 0;
        public const int BRIGHT_NIGHT = 1;
        public const int BRIGHT_DAWN = 2;
        public const int RING_TRANSPARENT_ITEM = 111;
        public const int RING_SPACEMOVE_ITEM = 112;
        public const int RING_MAKESTONE_ITEM = 113;
        public const int RING_REVIVAL_ITEM = 114;
        public const int RING_FIREBALL_ITEM = 115;
        public const int RING_HEALING_ITEM = 116;
        public const int RING_ANGERENERGY_ITEM = 117;
        public const int RING_MAGICSHIELD_ITEM = 118;
        public const int RING_SUPERSTRENGTH_ITEM = 119;
        public const int NECTLACE_FASTTRAINING_ITEM = 120;
        public const int NECTLACE_SEARCH_ITEM = 121;
        public const int RING_CHUN_ITEM = 122;
        public const int NECKLACE_GI_ITEM = 123;
        public const int ARMRING_HAP_ITEM = 124;
        public const int HELMET_IL_ITEM = 125;
        public const int RING_OF_UNKNOWN = 130;
        public const int BRACELET_OF_UNKNOWN = 131;
        public const int HELMET_OF_UNKNOWN = 132;
        public const int RING_OF_MANATOHEALTH = 133;
        // 付仿阑 眉仿栏肺 傈券
        public const int BRACELET_OF_MANATOHEALTH = 134;
        public const int NECKLACE_OF_MANATOHEALTH = 135;
        public const int RING_OF_SUCKHEALTH = 136;
        // 眉仿 软荐 酒捞袍
        public const int BRACELET_OF_SUCKHEALTH = 137;
        public const int NECKLACE_OF_SUCKHEALTH = 138;
        // 2003/01/15 技飘 酒捞袍 眠啊...技符悸, 踌秒悸, 档何悸
        public const int RING_OF_HPUP = 140;
        // HP, MP, HP/MP 惑铰 悸飘 酒捞袍
        public const int BRACELET_OF_HPUP = 141;
        public const int RING_OF_MPUP = 142;
        public const int BRACELET_OF_MPUP = 143;
        public const int RING_OF_HPMPUP = 144;
        public const int BRACELET_OF_HPMPUP = 145;
        // 2003/02/11 技飘 酒捞袍 眠啊...坷泅悸, 檬去悸
        public const int NECKLACE_OF_HPPUP = 146;
        public const int BRACELET_OF_HPPUP = 147;
        public const int RING_OH_HPPUP = 148;
        public const int CCHO_WEAPON = 23;
        public const int CCHO_NECKLACE = 149;
        public const int CCHO_RING = 150;
        public const int CCHO_HELMET = 151;
        public const int CCHO_BRACELET = 152;
        // 2003/03/04 技飘 酒捞袍 眠啊...颇尖悸, 券付籍悸, 康飞苛悸
        public const int PSET_NECKLACE_SHAPE = 153;
        public const int PSET_BRACELET_SHAPE = 154;
        public const int PSET_RING_SHAPE = 155;
        public const int HSET_NECKLACE_SHAPE = 156;
        public const int HSET_BRACELET_SHAPE = 157;
        public const int HSET_RING_SHAPE = 158;
        public const int YSET_NECKLACE_SHAPE = 159;
        public const int YSET_BRACELET_SHAPE = 160;
        public const int YSET_RING_SHAPE = 161;
        // 2003/11/17 力炼 傈侩 技飘 酒捞袍 眠啊(sonmg)
        // 焕促蓖悸,国饭悸,归陛悸,楷苛悸,全苛悸,碍拳归陛悸,碍拳楷苛悸,碍拳全苛悸.
        // 焕促蓖 技飘(Bone)
        public const int BONESET_WEAPON_SHAPE = 4;
        public const int BONESET_HELMET_SHAPE = 162;
        public const int BONESET_DRESS_SHAPE = 2;
        // 国饭 技飘(Bug)
        public const int BUGSET_NECKLACE_SHAPE = 163;
        public const int BUGSET_RING_SHAPE = 164;
        public const int BUGSET_BRACELET_SHAPE = 165;
        // 归陛 技飘(Platinum)
        public const int PTSET_BELT_SHAPE = 166;
        public const int PTSET_BOOTS_SHAPE = 167;
        public const int PTSET_NECKLACE_SHAPE = 168;
        public const int PTSET_BRACELET_SHAPE = 169;
        public const int PTSET_RING_SHAPE = 170;
        // 楷苛 技飘(Kidney Stone)
        public const int KSSET_BELT_SHAPE = 176;
        public const int KSSET_BOOTS_SHAPE = 177;
        public const int KSSET_NECKLACE_SHAPE = 178;
        public const int KSSET_BRACELET_SHAPE = 179;
        public const int KSSET_RING_SHAPE = 180;
        // 全苛 技飘(Ruby)
        public const int RUBYSET_BELT_SHAPE = 171;
        public const int RUBYSET_BOOTS_SHAPE = 172;
        public const int RUBYSET_NECKLACE_SHAPE = 173;
        public const int RUBYSET_BRACELET_SHAPE = 174;
        public const int RUBYSET_RING_SHAPE = 175;
        // 碍拳归陛 技飘
        public const int STRONG_PTSET_BELT_SHAPE = 181;
        public const int STRONG_PTSET_BOOTS_SHAPE = 182;
        public const int STRONG_PTSET_NECKLACE_SHAPE = 183;
        public const int STRONG_PTSET_BRACELET_SHAPE = 184;
        public const int STRONG_PTSET_RING_SHAPE = 185;
        // 碍拳楷苛 技飘
        public const int STRONG_KSSET_BELT_SHAPE = 191;
        public const int STRONG_KSSET_BOOTS_SHAPE = 192;
        public const int STRONG_KSSET_NECKLACE_SHAPE = 193;
        public const int STRONG_KSSET_BRACELET_SHAPE = 194;
        public const int STRONG_KSSET_RING_SHAPE = 195;
        // 碍拳全苛 技飘
        public const int STRONG_RUBYSET_BELT_SHAPE = 186;
        public const int STRONG_RUBYSET_BOOTS_SHAPE = 187;
        public const int STRONG_RUBYSET_NECKLACE_SHAPE = 188;
        public const int STRONG_RUBYSET_BRACELET_SHAPE = 189;
        public const int STRONG_RUBYSET_RING_SHAPE = 190;
        // 2003-10-01 玫狼公豪 酒捞袍溅捞橇
        public const int DRESS_SHAPE_WING = 9;
        // 2004-06-29 脚痹癌渴(颇炔玫付狼) 嘉捞橇
        public const int DRESS_SHAPE_PBKING = 11;
        // 癌渴狼 StdMode
        public const int DRESS_STDMODE_MAN = 10;
        public const int DRESS_STDMODE_WOMAN = 11;
        // 2004/01/08 侩酒捞袍 酒捞袍 Shape (sonmg)
        // 2004/01/09 侩 技飘 酒捞袍 眠啊(sonmg)
        public const int DRAGON_RING_SHAPE = 198;
        public const int DRAGON_BRACELET_SHAPE = 199;
        public const int DRAGON_NECKLACE_SHAPE = 200;
        public const int DRAGON_DRESS_SHAPE = 10;
        public const int DRAGON_HELMET_SHAPE = 201;
        public const int DRAGON_WEAPON_SHAPE = 37;
        public const int DRAGON_BOOTS_SHAPE = 203;
        public const int DRAGON_BELT_SHAPE = 204;
        // 2004/03/05 阜措荤帕 - 拳捞飘单捞 捞亥飘 眠啊(sonmg)
        public const int LOLLIPOP_SHAPE = 1;
        // 2004/08/16 陛皋崔,篮皋崔,悼皋崔 - 酒抛匙 棵覆侨 捞亥飘 眠啊(sonmg)
        public const int GOLDMEDAL_SHAPE = 2;
        public const int SILVERMEDAL_SHAPE = 3;
        public const int BRONZEMEDAL_SHAPE = 4;
        // 汗炼府 (sonmg 2005/02/02)
        public const int SHAPE_OF_LUCKYLADLE = 5;
        // 公扁狼 StdMode(sonmg)
        public const int WEAPON_STDMODE1 = 5;
        public const int WEAPON_STDMODE2 = 6;
        public const int FASTFILL_ITEM = 1;
        // 拱距狼 shape 盒幅 锅龋
        public const int FREE_UNKNOWN_ITEM = 2;
        // stdmode = 2 (澜侥幅)
        public const int SHAPE_BUNCH_OF_FLOWERS = 1;
        // 采促惯
        // stdmode = 3 (傈辑幅)
        public const int INSTANTABILUP_DRUG = 12;
        public const int INSTANT_EXP_DRUG = 13;
        // 冈栏搁 版氰摹啊 惑铰茄促. (AC * 100 父怒 版氰摹 曼)
        // 楷牢
        public const int SHAPE_COUPLE_ALIVE_STONE = 7;
        // 楷牢何劝籍
        public const int SHAPE_ADV_COUPLERING = 205;
        // 绊鞭目敲馆瘤
        public const int SHAPE_COUPLERING = 206;
        // 目敲馆瘤
        // stdmode = 7, 8
        public const int SHAPE_OF_CORD = 0;
        // 畴馋 Shape (sonmg)
        public const int SHAPE_OF_INVITATION = 0;
        // 檬措厘 Shape (sonmg)
        public const int SHAPE_OF_TELEPORTTAG = 1;
        // 付菩 Shape (sonmg - ResStdItems俊 甘捞抚鞘靛 眠啊)
        public const int SHAPE_OF_GIFTBOX = 2;
        // 急拱惑磊 Shape (sonmg)
        public const int SHAPE_OF_EASTEREGG = 3;
        // 何劝例 崔翱 Shape (sonmg)
        public const int SHAPE_OF_OLDBOX = 4;
        // 嘲篮彼娄 Shape (sonmg)
        public const int SHAPE_OF_EGG = 5;
        // 2006何劝例 崔翱 Shape (sonmg)
        // 厘盔操固扁 stdmode = 9
        public const int STDMODE_OF_DECOITEM = 9;
        // 惑泅林赣聪 StdMode (sonmg)
        public const int SHAPE_OF_DECOITEM = 1;
        // 惑泅林赣聪 Shape (sonmg)
        public const string NAME_OF_DECOITEM = "梦想囊";
        public const int DEFAULT_DECOITEM_PRICE = 10000;
        // 官蠢龙侩前, 焕噶摹(sonmg) + 畴馋(2004/05/03)
        public const int SHAPE_OF_NEEDLE = 20;
        public const int SHAPE_OF_HAMMER = 21;
        public const int SHAPE_AMULET_BUNCH = 111;
        // 何利弓澜
        public const int AM_FIREBALL = 1;
        public const int AM_HEALING = 2;
        // 50饭骇 瘤盔
        public const int EFFECTIVE_HIGHLEVEL = 50;
        // 烙矫 抛胶飘 内靛(sonmg)
        // MAX_OVERLAPITEM = 100;  // 昏力 夸噶(Global2俊 乐澜)
        // 墨款飘 酒捞袍 Overflow 力茄蔼(65000栏肺 秦具窃).
        public const int MAX_OVERFLOW = 65000;
        public const int COMPENSATORY_PAYMENT = 100000;
        public const int COMPENSATORY_PAYMENT_ONEWAY = 300000;
    } 
}