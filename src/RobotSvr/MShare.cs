using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using SystemModule;

namespace RobotSvr
{
    public class MShare
    {
#if DEBUG_LOGIN
        public static byte g_fWZLFirst = 7;
#else
        public static byte g_fWZLFirst = 7;
#endif
        public static bool g_boLogon = false;
        public static bool g_fGoAttack = false;
        public static int g_nDragonRageStateIndex = 0;
        public static int AAX = 26 + 14;
        public static int LMX = 30;
        public static int LMY = 26;
        public static int VIEWWIDTH = 8;
        public static byte g_BuildBotTex = 0;
        public static byte g_WinBottomType = 0;
        public static bool g_Windowed = false;
        public static bool g_boAutoPickUp = true;
        public static bool g_boPickUpAll = false;
        public static int g_ptItems_Pos = -1;
        public static int g_ptItems_Type = 0;
        public static Dictionary<string, string> g_ItemsFilter_All = null;
        public static Dictionary<string, string> g_ItemsFilter_All_Def = null;
        public static ArrayList g_ItemsFilter_Dress = null;
        public static ArrayList g_ItemsFilter_Weapon = null;
        public static ArrayList g_ItemsFilter_Headgear = null;
        public static ArrayList g_ItemsFilter_Drug = null;
        public static ArrayList g_ItemsFilter_Other = null;
        public static ArrayList g_xMapDescList = null;
        public static ArrayList g_xCurMapDescList = null;
        public static byte[] g_pWsockAddr = new byte[4 + 1];
        public static string[] g_sRenewBooks = new string[] { "随机传送卷", "地牢逃脱卷", "回城卷", "行会回城卷", "盟重传送石", "比奇传送石", "随机传送石", };
        public static int g_nMagicRange = 8;
        public static int g_TileMapOffSetX = 9;
        public static int g_TileMapOffSetY = 9;
        public static byte g_btMyEnergy = 0;
        public static byte g_btMyLuck = 0;
        public static TItemShine g_tiOKShow = new TItemShine() { idx = 0, tick = 0 };
        public static TItemShine g_tiFailShow = new TItemShine() { idx = 0, tick = 0 };
        public static TItemShine g_tiOKShow2 = new TItemShine() { idx = 0, tick = 0 };
        public static TItemShine g_tiFailShow2 = new TItemShine() { idx = 0, tick = 0 };
        public static TItemShine g_spOKShow2 = new TItemShine() { idx = 0, tick = 0 };
        public static TItemShine g_spFailShow2 = new TItemShine() { idx = 0, tick = 0 };
        public static string g_tiHintStr1 = "";
        public static string g_tiHintStr2 = "";
        public static TMovingItem[] g_TIItems = new TMovingItem[1 + 1];
        public static string g_spHintStr1 = "";
        public static string g_spHintStr2 = "";
        public static TMovingItem[] g_spItems = new TMovingItem[1 + 1];
        public static int g_SkidAD_Count = 0;
        public static int g_SkidAD_Count2 = 0;
        public static string g_lastHeroSel = string.Empty;
        public static byte g_ItemWear = 0;
        public static byte g_ShowSuite = 0;
        public static byte g_ShowSuite2 = 0;
        public static byte g_ShowSuite3 = 0;
        public static byte g_SuiteSpSkill = 0;
        public static int g_SuiteIdx = -1;
        public static byte g_btSellType = 0;
        public static bool g_showgamegoldinfo = false;
        public static bool SSE_AVAILABLE = false;
        public static int g_lWavMaxVol = 68;
        // -100;
        public static long g_uDressEffectTick = 0;
        public static long g_sDressEffectTick = 0;
        public static long g_hDressEffectTick = 0;
        public static int g_uDressEffectIdx = 0;
        public static int g_sDressEffectIdx = 0;
        public static int g_hDressEffectIdx = 0;
        public static long g_uWeaponEffectTick = 0;
        public static long g_sWeaponEffectTick = 0;
        public static long g_hWeaponEffectTick = 0;
        public static int g_uWeaponEffectIdx = 0;
        public static int g_sWeaponEffectIdx = 0;
        public static int g_hWeaponEffectIdx = 0;
        public static int g_ChatWindowLines = 12;
        public static bool g_LoadBeltConfig = false;
        public static bool g_BeltMode = true;
        public static int g_BeltPositionX = 408;
        public static int g_BeltPositionY = 487;
        public static int g_dwActorLimit = 5;
        public static int g_nProcActorIDx = 0;
        public static bool g_boPointFlash = false;
        public static long g_PointFlashTick = 0;
        public static bool g_boHPointFlash = false;
        public static long g_HPointFlashTick = 0;
        public static bool g_NextSeriesSkill = false;
        public static long g_dwSeriesSkillReadyTick = 0;
        public static int g_nCurrentMagic = 888;
        public static int g_nCurrentMagic2 = 888;
        public static long g_SendFireSerieSkillTick = 0;
        public static long g_IPointLessHintTick = 0;
        public static long g_MPLessHintTick = 0;
        public static int g_SeriesSkillStep = 0;
        public static bool g_SeriesSkillFire_100 = false;
        public static bool g_SeriesSkillFire = false;
        public static bool g_SeriesSkillReady = false;
        public static bool g_SeriesSkillReadyFlash = false;
        public static byte[] g_TempSeriesSkillArr;
        public static byte[] g_HTempSeriesSkillArr;
        public static byte[] g_SeriesSkillArr = new byte[3 + 1];
        public static ArrayList g_SeriesSkillSelList = null;
        public static ArrayList g_hSeriesSkillSelList = null;
        public static long g_dwAutoTecTick = 0;
        public static long g_dwAutoTecHeroTick = 0;
        public static long g_dwProcMessagePacket = 0;
        public static long g_ProcNowTick = 0;
        public static bool g_ProcCanFill = true;
        public static long g_ProcOnDrawTick_Effect = 0;
        public static long g_ProcOnDrawTick_Effect2 = 0;
        public static long g_dwImgMgrTick = 0;
        public static int g_nImgMgrIdx = 0;
        public static object ProcMsgCS = null;
        public static object ThreadCS = null;
        public static bool g_bIMGBusy = false;
        public static long g_dwThreadTick = 0;
        public static long g_rtime = 0;
        public static long g_dwLastThreadTick = 0;
        public static bool g_boExchgPoison = false;
        public static bool g_boCheckTakeOffPoison = false;
        public static int g_Angle = 0;
        public static bool g_ShowMiniMapXY = false;
        public static bool g_DrawingMiniMap = false;
        public static bool g_DrawMiniBlend = false;
        public static bool g_boTakeOnPos = true;
        public static bool g_boHeroTakeOnPos = true;
        public static bool g_boQueryDynCode = false;
        public static bool g_boQuerySelChar = false;
        public static bool g_pbRecallHero = false;
        public static int g_dwCheckCount = 0;
        public static int g_nBookPath = 0;
        public static int g_nBookPage = 0;
        public static int g_HillMerchant = 0;
        public static string g_sBookLabel = "";
        public static int g_MaxExpFilter = 2000;
        public static bool g_boDrawLevelRank = false;
        public static string[] g_UnBindItems = { "万年雪霜", "疗伤药", "强效太阳水", "强效金创药", "强效魔法药", "金创药(小量)", "魔法药(小量)", "金创药(中量)", "魔法药(中量)", "地牢逃脱卷", "随机传送卷", "回城卷", "行会回城卷" };
        public static string g_sLogoText = "LegendSoft";
        public static string g_sGoldName = "金币";
        public static string g_sGameGoldName = "元宝";
        public static string g_sGamePointName = "泡点";
        public static string g_sWarriorName = "武士";
        public static string g_sWizardName = "魔法师";
        public static string g_sTaoistName = "道士";
        public static string g_sUnKnowName = "未知";
        public static string g_sMainParam1 = string.Empty;
        public static string g_sMainParam2 = string.Empty;
        public static string g_sMainParam3 = string.Empty;
        public static string g_sMainParam4 = string.Empty;
        public static string g_sMainParam5 = string.Empty;
        public static string g_sMainParam6 = string.Empty;
        public static bool g_boCanDraw = true;
        public static bool g_boInitialize = false;
        public static int g_nInitializePer = 0;
        public static bool g_boQueryExit = false;
        public static string g_FontName = string.Empty;
        public static int g_FontSize = 0;
        public static byte[] g_PowerBlock = { 0x55, 0x8B, 0xEC, 0x83, 0xC4, 0xE8, 0x89, 0x55, 0xF8, 0x89, 0x45, 0xFC, 0xC7, 0x45, 0xEC, 0xE8, 0x03, 0x00, 0x00, 0xC7, 0x45, 0xE8, 0x64, 0x00, 0x00, 0x00, 0xDB, 0x45, 0xEC, 0xDB, 0x45, 0xE8, 0xDE, 0xF9, 0xDB, 0x45, 0xFC, 0xDE, 0xC9, 0xDD, 0x5D, 0xF0, 0x9B, 0x8B, 0x45, 0xF8, 0x8B, 0x00, 0x8B, 0x55, 0xF8, 0x89, 0x02, 0xDD, 0x45, 0xF0, 0x8B, 0xE5, 0x5D, 0xC3, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };
        public static byte[] g_PowerBlock1 = { 0x55, 0x8B, 0xEC, 0x83, 0xC4, 0xE8, 0x89, 0x55, 0xF8, 0x89, 0x45, 0xFC, 0xC7, 0x45, 0xEC, 0x64, 0x00, 0x00, 0x00, 0xC7, 0x45, 0xE8, 0x64, 0x00, 0x00, 0x00, 0xDB, 0x45, 0xEC, 0xDB, 0x45, 0xE8, 0xDE, 0xF9, 0xDB, 0x45, 0xFC, 0xDE, 0xC9, 0xDD, 0x5D, 0xF0, 0x9B, 0x8B, 0x45, 0xF8, 0x8B, 0x00, 0x8B, 0x55, 0xF8, 0x89, 0x02, 0xDD, 0x45, 0xF0, 0x8B, 0xE5, 0x5D, 0xC3, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };
        public static string g_sServerName = string.Empty;
        public static string g_sServerMiniName = string.Empty;
        public static string g_psServerAddr = string.Empty;
        public static int g_pnServerPort = 0;
        public static string g_sSelChrAddr = string.Empty;
        public static int g_nSelChrPort = 0;
        public static string g_sRunServerAddr = string.Empty;
        public static int g_nRunServerPort = 0;
        public static int g_nTopDrawPos = 0;
        public static int g_nLeftDrawPos = 0;
        public static bool g_boSendLogin = false;
        public static bool g_boServerConnected = false;
        public static bool g_SoftClosed = false;
        public static TChrAction g_ChrAction;
        public static TConnectionStep g_ConnectionStep;
        public static bool g_boFullScreen = false;
        public static byte g_btMP3Volume = 70;
        public static byte g_btSoundVolume = 70;
        public static bool g_boBGSound = true;
        public static bool g_boSound = true;
        public static bool g_DlgInitialize = false;
        public static bool g_boFirstActive = true;
        public static bool g_boFirstTime = false;
        public static string g_sMapTitle = string.Empty;
        public static int g_nLastMapMusic = -1;
        public static ArrayList g_SendSayList = null;
        public static int g_SendSayListIdx = 0;
        public static ArrayList g_ServerList = null;
        public static IList<string> g_GroupMembers = null;
        public static ArrayList g_SoundList = null;
        public static ArrayList BGMusicList = null;
        public static long g_DxFontsMgrTick = 0;
        public static TClientMagic[][] g_MagicArr = new TClientMagic[3][] { new TClientMagic[] { }, new TClientMagic[] { }, new TClientMagic[] { } };
        public static ArrayList g_MagicList = null;
        public static ArrayList g_IPMagicList = null;
        public static ArrayList g_HeroMagicList = null;
        public static ArrayList g_HeroIPMagicList = null;
        public static ArrayList[] g_ShopListArr = new ArrayList[5 + 1];
        public static ArrayList g_SaveItemList = null;
        public static ArrayList g_MenuItemList = null;
        public static IList<TDropItem> g_DropedItemList = null;
        public static ArrayList g_ChangeFaceReadyList = null;
        public static IList<TActor> g_FreeActorList = null;
        public static int g_PoisonIndex = 0;
        public static int g_nBonusPoint = 0;
        public static int g_nSaveBonusPoint = 0;
        public static TNakedAbility g_BonusTick = null;
        public static TNakedAbility g_BonusAbil = null;
        public static TNakedAbility g_NakedAbil = null;
        public static TNakedAbility g_BonusAbilChg = null;
        public static string g_sGuildName = string.Empty;
        public static string g_sGuildRankName = string.Empty;
        public static long g_dwLatestJoinAttackTick = 0;
        // 最后魔法攻击时间
        public static long g_dwLastAttackTick = 0;
        // 最后攻击时间(包括物理攻击及魔法攻击)
        public static long g_dwLastMoveTick = 0;
        // 最后移动时间
        public static long g_dwLatestSpellTick = 0;
        // 最后魔法攻击时间
        public static long g_dwLatestFireHitTick = 0;
        // 最后列火攻击时间
        public static long g_dwLatestSLonHitTick = 0;
        // 最后列火攻击时间
        public static long g_dwLatestTwinHitTick = 0;
        public static long g_dwLatestPursueHitTick = 0;
        public static long g_dwLatestRushHitTick = 0;
        public static long g_dwLatestSmiteHitTick = 0;
        public static long g_dwLatestSmiteLongHitTick = 0;
        public static long g_dwLatestSmiteLongHitTick2 = 0;
        public static long g_dwLatestSmiteLongHitTick3 = 0;
        public static long g_dwLatestSmiteWideHitTick = 0;
        public static long g_dwLatestSmiteWideHitTick2 = 0;
        public static long g_dwLatestRushRushTick = 0;
        public static long g_dwMagicDelayTime = 0;
        public static long g_dwMagicPKDelayTime = 0;
        public static int g_nMouseCurrX = 0;
        // 鼠标所在地图位置座标X
        public static int g_nMouseCurrY = 0;
        // 鼠标所在地图位置座标Y
        public static int g_nMouseX = 0;
        // 鼠标所在屏幕位置座标X
        public static int g_nMouseY = 0;
        // 鼠标所在屏幕位置座标Y
        public static int g_nTargetX = 0;
        // 目标座标
        public static int g_nTargetY = 0;
        // 目标座标
        public static TActor g_TargetCret = null;
        public static TActor g_FocusCret = null;
        public static TActor g_MagicTarget = null;
        public static Link g_APQueue = null;
        public static ArrayList g_APPathList = null;
        public static double[,] g_APPass;
        public static TActor g_APTagget = null;
        public static long g_APRunTick = 0;
        public static long g_APRunTick2 = 0;
        public static TDropItem g_AutoPicupItem = null;
        public static int g_nAPStatus = 0;
        public static bool g_boAPAutoMove = false;
        public static bool g_boLongHit = false;
        public static string g_sAPStr = string.Empty;
        public static int g_boAPXPAttack = 0;
        public static long m_dwSpellTick = 0;
        public static long m_dwRecallTick = 0;
        public static long m_dwDoubluSCTick = 0;
        public static long m_dwPoisonTick = 0;
        public static int m_btMagPassTh = 0;
        public static int g_nTagCount = 0;
        public static long m_dwTargetFocusTick = 0;
        public static Dictionary<string, string> g_APPickUpList = null;
        public static Dictionary<string, string> g_APMobList = null;
        public static int g_AttackInvTime = 900;
        public static TActor g_AttackTarget = null;
        public static long g_dwSearchEnemyTick = 0;
        public static bool g_boAllowJointAttack = false;
        // ////////////////////////////////////////
        public static byte g_nAPReLogon = 0;
        public static bool g_nAPrlRecallHero = false;
        public static bool g_nAPSendSelChr = false;
        public static bool g_nAPSendNotice = false;
        public static long g_nAPReLogonWaitTick = 0;
        public static int g_nAPReLogonWaitTime = 10 * 1000;
        public static int g_ApLastSelect = 0;
        public static int g_nOverAPZone = 0;
        public static int g_nOverAPZone2 = 0;
        public static bool g_APGoBack = false;
        public static bool g_APGoBack2 = false;
        public static int g_APStep = -1;
        public static int g_APStep2 = -1;
        public static Point[] g_APMapPath;
        public static Point[] g_APMapPath2;
        public static Point g_APLastPoint;
        public static Point g_APLastPoint2;
        public static bool g_nApMiniMap = false;
        public static long g_dwBlinkTime = 0;
        public static bool g_boViewBlink = false;
        public static bool g_boMapMoving = false;
        public static bool g_boMapMovingWait = false;
        public static bool g_boCheckBadMapMode = false;
        public static bool g_boCheckSpeedHackDisplay = false;
        public static bool g_boViewMiniMap = false;
        // 是否可视小地图 默认为True
        public static int g_nViewMinMapLv = 0;
        // 小地图显示等级
        public static int g_nMiniMapIndex = 0;
        // 小地图索引编号
        public static int g_nMiniMapX = 0;
        // 小图鼠标指向坐标X
        public static int g_nMiniMapY = 0;
        // 小图鼠标指向坐标Y
        // NPC 相关
        public static int g_nCurMerchant = 0;
        // NPC大对话框
        public static int g_nCurMerchantFaceIdx = 0;
        // Development 2019-01-14
        public static int g_nMDlgX = 0;
        public static int g_nMDlgY = 0;
        public static int g_nStallX = 0;
        public static int g_nStallY = 0;
        public static long g_dwChangeGroupModeTick = 0;
        public static long g_dwDealActionTick = 0;
        public static long g_dwQueryMsgTick = 0;
        public static int g_nDupSelection = 0;
        public static bool g_boAllowGroup = false;
        // 人物信息相关
        public static int g_nMySpeedPoint = 0;
        // 敏捷
        public static int g_nMyHitPoint = 0;
        // 准确
        public static int g_nMyAntiPoison = 0;
        // 魔法躲避
        public static int g_nMyPoisonRecover = 0;
        // 中毒恢复
        public static int g_nMyHealthRecover = 0;
        // 体力恢复
        public static int g_nMySpellRecover = 0;
        // 魔法恢复
        public static int g_nMyAntiMagic = 0;
        // 魔法躲避
        public static int g_nMyHungryState = 0;
        // 饥饿状态
        public static int g_nMyIPowerRecover = 0;
        // 中毒恢复
        public static int g_nMyAddDamage = 0;
        public static int g_nMyDecDamage = 0;
        public static int g_nHeroSpeedPoint = 0;
        // 敏捷
        public static int g_nHeroHitPoint = 0;
        // 准确
        public static int g_nHeroAntiPoison = 0;
        // 魔法躲避
        public static int g_nHeroPoisonRecover = 0;
        // 中毒恢复
        public static int g_nHeroHealthRecover = 0;
        // 体力恢复
        public static int g_nHeroSpellRecover = 0;
        // 魔法恢复
        public static int g_nHeroAntiMagic = 0;
        // 魔法躲避
        public static int g_nHeroHungryState = 0;
        // 饥饿状态
        public static int g_nHeroBagSize = 40;
        public static int g_nHeroIPowerRecover = 0;
        public static int g_nHeroAddDamage = 0;
        public static int g_nHeroDecDamage = 0;
        public static ushort g_wAvailIDDay = 0;
        public static ushort g_wAvailIDHour = 0;
        public static ushort g_wAvailIPDay = 0;
        public static ushort g_wAvailIPHour = 0;
        public static THumActor g_MySelf = null;
        public static THumActor g_MyDrawActor = null;
        public static string g_sAttackMode = "";
        public static string sAttackModeOfAll = "[全体攻击模式]";
        public static string sAttackModeOfPeaceful = "[和平攻击模式]";
        public static string sAttackModeOfDear = "[夫妻攻击模式]";
        public static string sAttackModeOfMaster = "[师徒攻击模式]";
        public static string sAttackModeOfGroup = "[编组攻击模式]";
        public static string sAttackModeOfGuild = "[行会攻击模式]";
        public static string sAttackModeOfRedWhite = "[善恶攻击模式]";
        public static int g_RIWhere = 0;
        public static TMovingItem[] g_RefineItems = new TMovingItem[2 + 1];
        public static int g_BuildAcusesStep = 0;
        public static int g_BuildAcusesProc = 0;
        public static long g_BuildAcusesProcTick = 0;
        public static int g_BuildAcusesSuc = -1;
        public static int g_BuildAcusesSucFrame = 0;
        public static long g_BuildAcusesSucFrameTick = 0;
        public static long g_BuildAcusesFrameTick = 0;
        public static int g_BuildAcusesFrame = 0;
        public static int g_BuildAcusesRate = 0;
        public static TMovingItem[] g_BuildAcuses = new TMovingItem[7 + 1];
        public static int g_BAFirstShape = -1;
        public static TClientItem[] g_tui = new TClientItem[13 + 1];
        public static TClientItem[] g_UseItems = new TClientItem[13 + 1];
        public static TItemShine g_detectItemShine = null;
        public static TItemShine[] UserState1Shine = new TItemShine[13 + 1];
        public static TItemShine[] g_UseItemsShine = new TItemShine[13 + 1];
        public static TClientItem[] g_ItemArr = new TClientItem[MAXBAGITEMCL - 1 + 1];
        public static TClientItem[] g_HeroItemArr = new TClientItem[MAXBAGITEMCL - 1 + 1];
        public static TItemShine[] g_ItemArrShine = new TItemShine[MAXBAGITEMCL - 1 + 1];
        public static TItemShine[] g_HeroItemArrShine = new TItemShine[MAXBAGITEMCL - 1 + 1];
        public static TItemShine[] g_StallItemArrShine = new TItemShine[10 - 1 + 1];
        public static TItemShine[] g_uStallItemArrShine = new TItemShine[10 - 1 + 1];
        public static TItemShine[] g_DealItemsShine = new TItemShine[10 - 1 + 1];
        public static TItemShine[] g_DealRemoteItemsShine = new TItemShine[20 - 1 + 1];
        public static TItemShine g_MovingItemShine = null;
        public static bool g_boBagLoaded = false;
        public static bool g_boServerChanging = false;
        public static int g_nCaptureSerial = 0;
        public static int g_nReceiveCount = 0;
        public static int g_nTestSendCount = 0;
        public static int g_nTestReceiveCount = 0;
        public static int g_nSpellCount = 0;
        public static int g_nSpellFailCount = 0;
        public static int g_nFireCount = 0;
        public static int g_nDebugCount = 0;
        public static int g_nDebugCount1 = 0;
        public static int g_nDebugCount2 = 0;
        public static TClientItem g_SellDlgItem = null;
        public static TMovingItem g_TakeBackItemWait = null;
        public static TMovingItem g_SellDlgItemSellWait = null;
        public static TClientItem g_DetectItem = null;
        public static int g_DetectItemMineID = 0;
        public static TClientItem g_DealDlgItem = null;
        public static bool g_boQueryPrice = false;
        public static long g_dwQueryPriceTime = 0;
        public static string g_sSellPriceStr = string.Empty;
        public static TClientItem[] g_DealItems = new TClientItem[9 + 1];
        public static bool g_boYbDealing = false;
        public static TClientItem[] g_YbDealItems = new TClientItem[9 + 1];
        public static TClientItem[] g_DealRemoteItems = new TClientItem[19 + 1];
        public static int g_nDealGold = 0;
        public static int g_nDealRemoteGold = 0;
        public static bool g_boDealEnd = false;
        public static string g_sDealWho = string.Empty;
        public static TClientItem g_MouseItem = null;
        public static TClientItem g_MouseStateItem = null;
        public static TClientItem g_HeroMouseStateItem = null;
        public static TClientItem g_MouseUserStateItem = null;
        public static TClientItem g_HeroMouseItem = null;
        public static bool g_boItemMoving = false;
        public static TMovingItem g_MovingItem = null;
        public static TMovingItem g_OpenBoxItem = null;
        public static TMovingItem g_WaitingUseItem = null;
        public static TMovingItem g_WaitingStallItem = null;
        public static TMovingItem g_WaitingDetectItem = null;
        public static TDropItem g_FocusItem = null;
        public static TDropItem g_FocusItem2 = null;
        public static bool g_boOpenStallSystem = true;
        public static bool g_boAutoLongAttack = true;
        public static bool g_boAutoSay = true;
        public static bool g_boMutiHero = true;
        public static bool g_boSkill_114_MP = false;
        public static bool g_boSkill_68_MP = false;
        public static int g_nDayBright = 0;
        public static int g_nAreaStateValue = 0;
        public static bool g_boNoDarkness = false;
        public static int g_nRunReadyCount = 0;
        public static bool g_boLastViewFog = false;
        public static bool g_boViewFog = false;
        public static bool g_boForceNotViewFog = true;
        public static TClientItem g_EatingItem = null;
        public static long g_dwEatTime = 0;
        public static long g_dwHeroEatTime = 0;
        public static long g_dwDizzyDelayStart = 0;
        public static long g_dwDizzyDelayTime = 0;
        public static bool g_boDoFadeOut = false;
        // 由亮变暗
        public static bool g_boDoFadeIn = false;
        // 由暗变亮
        public static int g_nFadeIndex = 0;
        public static bool g_boDoFastFadeOut = false;
        public static bool g_boAutoDig = false;
        public static bool g_boAutoSit = false;
        // 自动锄矿
        public static bool g_boSelectMyself = false;
        // 鼠标是否指到自己
        // 游戏速度检测相关变量
        public static int g_dwFirstServerTime = 0;
        public static int g_dwFirstClientTime = 0;
        public static int g_nTimeFakeDetectCount = 0;
        public static int g_dwLatestClientTime2 = 0;
        public static int g_dwFirstClientTimerTime = 0;
        // timer 矫埃
        public static long g_dwLatestClientTimerTime = 0;
        public static long g_dwFirstClientGetTime = 0;
        // gettickcount 矫埃
        public static long g_dwLatestClientGetTime = 0;
        public static int g_nTimeFakeDetectSum = 0;
        public static int g_nTimeFakeDetectTimer = 0;
        public static long g_dwLastestClientGetTime = 0;
        // 外挂功能变量开始
        public static long g_dwDropItemFlashTime = 5 * 1000;
        // 地面物品闪时间间隔
        public static int g_nHitTime = 1400;
        // 攻击间隔时间间隔  0820
        public static int g_nItemSpeed = 60;
        public static long g_dwSpellTime = 500;
        // 魔法攻间隔时间
        public static bool g_boHero = true;
        public static bool g_boOpenAutoPlay = true;
        // 死亡颜色  Development 2018-12-29
        public static bool g_boClientCanSet = true;
        public static int g_nEatIteminvTime = 200;
        public static bool g_boCanRunSafeZone = true;
        public static bool g_boCanRunHuman = true;
        public static bool g_boCanRunMon = true;
        public static bool g_boCanRunNpc = true;
        public static bool g_boCanRunAllInWarZone = false;
        public static bool g_boCanStartRun = true;
        // 是否允许免助跑
        public static bool g_boParalyCanRun = false;
        // 麻痹是否可以跑
        public static bool g_boParalyCanWalk = false;
        // 麻痹是否可以走
        public static bool g_boParalyCanHit = false;
        // 麻痹是否可以攻击
        public static bool g_boParalyCanSpell = false;
        // 麻痹是否可以魔法
        public static bool g_boShowRedHPLable = true;
        // 显示血条
        public static bool g_boShowHPNumber = true;
        // 显示血量数字
        public static bool g_boShowJobLevel = true;
        // 显示职业等级
        public static bool g_boDuraAlert = true;
        // 物品持久警告
        public static bool g_boMagicLock = true;
        // 魔法锁定
        public static bool g_boSpeedRate = false;
        public static bool g_boSpeedRateShow = false;
        // g_boAutoPuckUpItem        : Boolean = False;
        public static bool g_boShowHumanInfo = true;
        public static bool g_boShowMonsterInfo = false;
        public static bool g_boShowNpcInfo = false;
        // 外挂功能变量结束
        public static bool g_boQuickPickup = false;
        public static long g_dwAutoPickupTick = 0;
        /// <summary>
        /// 自动捡物品间隔
        /// </summary>
        public static long g_dwAutoPickupTime = 100;
        public static TActor g_MagicLockActor = null;
        public static bool g_boNextTimePowerHit = false;
        public static bool g_boCanLongHit = false;
        public static bool g_boCanWideHit = false;
        public static bool g_boCanCrsHit = false;
        public static bool g_boCanStnHit = false;
        public static bool g_boNextTimeFireHit = false;
        public static bool g_boNextTimeTwinHit = false;
        public static bool g_boNextTimePursueHit = false;
        public static bool g_boNextTimeRushHit = false;
        public static bool g_boNextTimeSmiteHit = false;
        public static bool g_boNextTimeSmiteLongHit = false;
        public static bool g_boNextTimeSmiteLongHit2 = false;
        public static bool g_boNextTimeSmiteLongHit3 = false;
        public static bool g_boNextTimeSmiteWideHit = false;
        public static bool g_boNextTimeSmiteWideHit2 = false;
        public static bool g_boCanSLonHit = false;
        public static bool g_boCanSquHit = false;
        public static Dictionary<string, string> g_ShowItemList = null;
        public static bool g_boDrawTileMap = true;
        public static bool g_boDrawDropItem = true;
        public static int g_nTestX = 71;
        public static int g_nTestY = 212;
        public static int g_nSquHitPoint = 0;
        public static int g_nMaxSquHitPoint = 0;
        public static bool g_boConfigLoaded = false;
        public static byte g_dwCollectExpLv = 0;
        public static bool g_boCollectStateShine = false;
        public static int g_nCollectStateShine = 0;
        public static long g_dwCollectStateShineTick = 0;
        public static long g_dwCollectStateShineTick2 = 0;
        public static long g_dwCollectExp = 0;
        public static long g_dwCollectExpMax = 1;
        public static bool g_boCollectExpShine = false;
        public static int g_boCollectExpShineCount = 0;
        public static long g_dwCollectExpShineTick = 0;
        public static long g_dwCollectIpExp = 0;
        public static long g_dwCollectIpExpMax = 1;
        public static bool g_ReSelChr = false;
        public static bool ShouldUnloadEnglishKeyboardLayout = false;
        public static int[] g_FSResolutionWidth = { 800, 1024, 1280, 1280, 1366, 1440, 1600, 1680, 1920 };// 电脑分辨率宽度
        public static int[] g_FSResolutionHeight = { 600, 768, 800, 1024, 768, 900, 900, 1050, 1080 };// 电脑分辨率高度
        public static byte g_FScreenMode = 0;
        public static int g_FScreenWidth = SCREENWIDTH;
        public static int g_FScreenHeight = SCREENHEIGHT;
        public static bool g_boClientUI = false;
        public const string REG_SETUP_PATH = "SOFTWARE\\Jason\\Mir2\\Setup";
        public const string REG_SETUP_BITDEPTH = "BitDepth";
        public const string REG_SETUP_DISPLAY = "DisplaySize";
        public const string REG_SETUP_WINDOWS = "Window";
        public const string REG_SETUP_MP3VOLUME = "MusicVolume";
        public const string REG_SETUP_SOUNDVOLUME = "SoundVolume";
        public const string REG_SETUP_MP3OPEN = "MusicOpen";
        public const string REG_SETUP_SOUNDOPEN = "SoundOpen";
        public const int MAXX = 40;
        public const int MAXY = 30;
        public const int LONGHEIGHT_IMAGE = 35;
        public const int FLASHBASE = 410;
        public const int SOFFX = 0;
        public const int SOFFY = 0;
        public const int HEALTHBAR_BLACK = 0;
        public const int BARWIDTH = 30;
        public const int BARHEIGHT = 2;
        public const int MAXSYSLINE = 8;
        public const int BOTTOMBOARD = 1;
        public const int AREASTATEICONBASE = 150;
        public const int g_WinBottomRetry = 45;
        public const bool NEWHINTSYS = true;
        public const int NPC_CILCK_INVTIME = 500;
        public const int MAXITEMBOX_WIDTH = 177;
        public const int MAXMAGICLV = 3;
        public const int RUNLOGINCODE = 0;
        public const int CLIENT_VERSION_NUMBER = 120020522;
        public const int STDCLIENT = 0;
        public const int RMCLIENT = 46;
        public const int CLIENTTYPE = RMCLIENT;
        public const int CUSTOMLIBFILE = 0;
        public const int DEBUG = 0;
        public const int LOGICALMAPUNIT = 30;
        // 1108 40;
        public const int HUMWINEFFECTTICK = 200;
        public const int WINLEFT = 100;
        // 窗体左边 图片素材留在左边屏幕内的尺寸为100
        public const int WINTOP = 100;
        // 窗体顶边 图片素材留在顶边屏幕内的尺寸为100
        public const int MINIMAPSIZE = 200;
        // 迷你地图宽度
        public const int DEFAULTCURSOR = 0;
        // 系统默认光标
        public const int IMAGECURSOR = 1;
        // 图形光标
        public const int USECURSOR = DEFAULTCURSOR;
        // 使用什么类型的光标
        public const int MAXBAGITEMCL = 52;
        public const int MAXFONT = 8;
        public const int ENEMYCOLOR = 69;
        public const int HERO_MIIDX_OFFSET = 5000;
        public const int SAVE_MIIDX_OFFSET = HERO_MIIDX_OFFSET + 500;
        public const int STALL_MIIDX_OFFSET = HERO_MIIDX_OFFSET + 500 + 50;
        public const int DETECT_MIIDX_OFFSET = HERO_MIIDX_OFFSET + 500 + 50 + 10 + 1;
        public const int MSGMUCH = 2;
        public static string[] g_sHumAttr = { "金", "木", "水", "火", "土" };
        public static string[] g_DBStateStrArr = { "装", "时", "状", "属", "称", "技", "其" };
        public static string[] g_DBStateStrArrW = { "备", "装", "态", "性", "号", "能", "他" };
        public static string[] g_DBStateStrArrUS = { "装", "时", "称" };
        public static string[] g_DBStateStrArrUSW = { "备", "装", "号" };
        public static string[] g_DBStateStrArr2 = { "状", "技", "经", "连", "其" };
        public static string[] g_DBStateStrArr2W = { "态", "能", "络", "击", "他" };
        public static string[] g_slegend = { "", "传奇神剑", "传奇勋章", "传奇项链", "传奇之冠", "", "传奇护腕", "", "传奇之戒", "", "传奇腰带", "传奇之靴", "", "传奇面巾" };
        public const int MAX_GC_GENERAL = 16;
        public static Rectangle[] g_ptGeneral = new Rectangle[] {
          new Rectangle() {X= 35 + 000, Y= 70 + 23 * 0,  Width= 35 + 000 + 72 + 18, Height= 70 + 23 * 0 + 16} ,
          new Rectangle() {X=35 + 000, Y=70 + 23 * 1,Width= 35 + 000 + 72 + 18,  Height= 70 + 23 * 1 + 16} ,
          new Rectangle() {X=35 + 000, Y=70 + 23 * 2,Width= 35 + 000 + 78 + 18,  Height= 70 + 23 * 2 + 16} ,
          new Rectangle() {X=35 + 000, Y=70 + 23 * 3,Width= 35 + 000 + 96,  Height= 70 + 23 * 3 + 16} ,
          new Rectangle() {X=35 + 120, Y=70 + 23 * 0,Width= 35 + 120 + 72 + 30,  Height= 70 + 23 * 0 + 16} ,
          new Rectangle() {X=35 + 120, Y=70 + 23 * 1,Width= 35 + 120 + 72,  Height= 70 + 23 * 1 + 16} ,
          new Rectangle() {X=35 + 120, Y=70 + 23 * 2,Width= 35 + 120 + 72 + 18, Height=  70 + 23 * 2 + 16} ,
          new Rectangle() {X=35 + 120, Y=70 + 23 * 3,Width= 35 + 120 + 72,  Height= 70 + 23 * 3 + 16} ,
          new Rectangle() {X=35 + 120, Y=70 + 23 * 4,Width= 35 + 120 + 72 + 18,  Height= 70 + 23 * 4 + 16} ,
          new Rectangle() {X=35 + 240, Y=70 + 23 * 0,Width= 35 + 240 + 72,  Height= 70 + 23 * 0 + 16} ,
          new Rectangle() {X=35 + 240, Y=70 + 23 * 1,Width= 35 + 240 + 72,  Height= 70 + 23 * 1 + 16} ,
          new Rectangle() {X=35 + 240, Y=70 + 23 * 2,Width= 35 + 240 + 48,  Height= 70 + 23 * 2 + 16} ,
          new Rectangle() {X=35 + 240, Y=70 + 23 * 3,Width= 35 + 240 + 72,  Height= 70 + 23 * 3 + 16} ,
          new Rectangle() {X=35 + 240, Y=70 + 23 * 4,Width= 35 + 240 + 72,  Height= 70 + 23 * 4 + 16} ,
          new Rectangle() {X=35 + 240, Y=70 + 23 * 5,Width= 35 + 240 + 72,  Height= 70 + 23 * 5 + 16} ,
          new Rectangle() {X=35 + 120, Y=70 + 23 * 5,Width= 35 + 120 + 72,  Height= 70 + 23 * 5 + 16} ,
          new Rectangle() {X=35 + 000, Y=70 + 23 * 5,Width= 35 + 000 + 96,  Height= 70 + 23 * 5 + 16} };
        public static bool[] g_gcGeneral = { true, true, false, true, true, true, false, true, false, true, true, true, true, false, false, true, true };
        public static Color[] g_clGeneral = { System.Drawing.Color.Silver, System.Drawing.Color.Silver, System.Drawing.Color.Silver, System.Drawing.Color.Silver, System.Drawing.Color.Silver, System.Drawing.Color.Silver, System.Drawing.Color.Silver, System.Drawing.Color.Silver, System.Drawing.Color.Silver, System.Drawing.Color.Silver, System.Drawing.Color.Silver, System.Drawing.Color.Silver, System.Drawing.Color.Silver, System.Drawing.Color.Silver, System.Drawing.Color.Silver, System.Drawing.Color.Silver, System.Drawing.Color.Silver };
        public static Color[] g_clGeneralDef = { System.Drawing.Color.Silver, System.Drawing.Color.Silver, System.Drawing.Color.Silver, System.Drawing.Color.Silver, System.Drawing.Color.Silver, System.Drawing.Color.Silver, System.Drawing.Color.Silver, System.Drawing.Color.Silver, System.Drawing.Color.Silver, System.Drawing.Color.Silver, System.Drawing.Color.Silver, System.Drawing.Color.Silver, System.Drawing.Color.Silver, System.Drawing.Color.Silver, System.Drawing.Color.Silver, System.Drawing.Color.Silver, System.Drawing.Color.Silver };
        // ====================================Protect====================================
        public const int MAX_GC_PROTECT = 11;
        public static Rectangle[] g_ptProtect = new Rectangle[]{
           new Rectangle(){X=35 + 000,Y=70 + 24 * 0, Width=35 + 000 + 20,Height=70 + 24 * 0 + 16} ,
           new Rectangle(){X=35 + 000,Y=70 + 24 * 1, Width=35 + 000 + 20,Height=70 + 24 * 1 + 16} ,
           new Rectangle(){X=35 + 000,Y=70 + 24 * 2, Width=35 + 000 + 20,Height=70 + 24 * 2 + 16} ,
           new Rectangle(){X=35 + 000,Y=70 + 24 * 3, Width=35 + 000 + 20,Height=70 + 24 * 3 + 16} ,
           new Rectangle(){X=35 + 000,Y=70 + 24 * 4, Width=35 + 000 + 20,Height=70 + 24 * 4 + 16} ,
           new Rectangle(){X=35 + 000,Y=70 + 24 * 5, Width=35 + 000 + 20,Height=70 + 24 * 5 + 16} ,
           new Rectangle(){X=35 + 000,Y=70 + 24 * 6, Width=35 + 000 + 72,Height=70 + 24 * 6 + 16} ,
           new Rectangle(){X=35 + 180,Y=70 + 24 * 0, Width=35 + 180 + 20,Height=70 + 24 * 0 + 16} ,
           new Rectangle(){X=35 + 180,Y=70 + 24 * 1, Width=35 + 180 + 20,Height=70 + 24 * 1 + 16} ,
           new Rectangle(){X=35 + 180,Y=70 + 24 * 3, Width=35 + 180 + 20,Height=70 + 24 * 3 + 16} ,
           new Rectangle(){X=35 + 180,Y=70 + 24 * 5, Width=35 + 180 + 20,Height=70 + 24 * 5 + 16} ,
           new Rectangle(){X=35 + 180,Y=70 + 24 * 6, Width=35 + 180 + 20,Height=70 + 24 * 6 + 16} };

        public static bool[] g_gcProtect = { false, false, false, false, false, false, false, true, true, true, false, true };
        public static int[] g_gnProtectPercent = { 10, 10, 10, 10, 10, 10, 0, 88, 88, 88, 20, 00 };
        public static int[] g_gnProtectTime = { 4000, 4000, 4000, 4000, 4000, 4000, 4000, 4000, 4000, 1000, 1000, 1000 };
        public static Color[] g_clProtect = { System.Drawing.Color.Silver, System.Drawing.Color.Silver, System.Drawing.Color.Silver, System.Drawing.Color.Silver, System.Drawing.Color.Silver, System.Drawing.Color.Silver, System.Drawing.Color.Silver, System.Drawing.Color.Silver, System.Drawing.Color.Silver, System.Drawing.Color.Silver, System.Drawing.Color.Silver, System.Drawing.Color.Lime };
        // ====================================Tec====================================
        public const int MAX_GC_TEC = 14;
        public static string[] g_HintTec = { "钩选此项将开启刀刀刺杀", "钩选此项将开启智能半月", "钩选此项将自动凝聚烈火剑法", "钩选此项将自动凝聚逐日剑法", "钩选此项将自动开启魔法盾", "钩选此项英雄将自动开启魔法盾", "钩选此项道士将自动使用隐身术", "", "", "钩选此项将自动凝聚雷霆剑法", "钩选此项将自动进行隔位刺杀", "钩选此项将自动凝聚断空斩", "钩选此项英雄将不使用连击打怪\\方便玩家之间进行PK", "钩选此项将自动凝聚开天斩", "钩选此项：施展魔法超过允许距离时，会自动跑近目标并释放魔法" };
        public static string[] g_caTec = { "刀刀刺杀", "智能半月", "自动烈火", "逐日剑法", "自动开盾", "持续开盾(英雄)", "自动隐身", "时间间隔", "", "自动雷霆", "隔位刺杀", "自动断空斩", "英雄连击不打怪", "自动开天斩", "自动调节魔法距离" };
        public static string[] g_sMagics = { "火球术", "治愈术", "大火球", "施毒术", "攻杀剑术", "抗拒火环", "地狱火", "疾光电影", "雷电术", "雷电术", "雷电术", "雷电术", "雷电术", "开天斩", "开天斩" };
        public const int g_gnTecPracticeKey = 0;
        public static bool[] g_gcTec = { true, true, true, true, true, true, false, false, false, false, false, false, false, true, false };
        public static int[] g_gnTecTime = { 0, 0, 0, 0, 0, 0, 0, 0, 4000, 0, 0, 0, 0, 0, 0 };
        public static Color[] g_clTec = { System.Drawing.Color.Silver, System.Drawing.Color.Silver, System.Drawing.Color.Silver, System.Drawing.Color.Silver, System.Drawing.Color.Silver, System.Drawing.Color.Silver, System.Drawing.Color.Silver, System.Drawing.Color.Silver, System.Drawing.Color.Silver, System.Drawing.Color.Silver, System.Drawing.Color.Silver, System.Drawing.Color.Silver, System.Drawing.Color.Silver, System.Drawing.Color.Silver, System.Drawing.Color.Silver };
        // ====================================Assistant====================================
        public const int MAX_GC_ASS = 6;
        public static Rectangle[] g_ptAss = {
  new Rectangle()  {X=35 + 000,Y= 70 + 24 * 0, Width=35 + 000 + 142,Height= 70 + 24 * 0 + 16} ,
  new Rectangle()  {X=35 + 000,Y= 70 + 24 * 1, Width=35 + 000 + 72, Height=70 + 24 * 1 + 16} ,
  new Rectangle()  {X=35 + 000,Y= 70 + 24 * 2, Width=35 + 000 + 72, Height=70 + 24 * 2 + 16} ,
  new Rectangle()  {X=35 + 000,Y= 70 + 24 * 3, Width=35 + 000 + 72, Height=70 + 24 * 3 + 16} ,
  new Rectangle()  {X=35 + 000,Y= 70 + 24 * 4, Width=35 + 000 + 72, Height=70 + 24 * 4 + 16} ,
  new Rectangle()  {X=35 + 000,Y= 70 + 24 * 5, Width=35 + 000 + 120,Height= 70 + 24 * 5 + 16} ,
  new Rectangle()  {X=35 + 000,Y= 70 + 24 * 6, Width=35 + 000 + 120,Height= 70 + 24 * 6 + 16} };

        public static bool[] g_gcAss = { false, false, false, false, false, false, false };
        public static Color[] g_clAss = { System.Drawing.Color.Lime, System.Drawing.Color.Silver, System.Drawing.Color.Silver, System.Drawing.Color.Silver, System.Drawing.Color.Silver, System.Drawing.Color.Silver, System.Drawing.Color.Silver };
        // ====================================HotKey====================================
        public const int MAX_GC_ITEMS = 7;
        public static Rectangle g_ptItemsA = new Rectangle() { X = 25 + 194, Y = 68 + 18 * 7 + 23, Width = 25 + 194 + 80, Height = 68 + 18 * 7 + 16 + 23 };
        public static Rectangle g_ptAutoPickUp = new Rectangle() { X = 25 + 267, Y = 68 + 18 * 7 + 23, Width = 25 + 267 + 80, Height = 68 + 18 * 7 + 16 + 23 };
        public static Color[] g_clItems = { System.Drawing.Color.Silver, System.Drawing.Color.Silver, System.Drawing.Color.Silver, System.Drawing.Color.Silver, System.Drawing.Color.Silver, System.Drawing.Color.Silver, System.Drawing.Color.Silver, System.Drawing.Color.Silver };
        public const int MAX_SERIESSKILL_POINT = 4;
        public static int g_HitSpeedRate = 0;
        public static int g_MagSpeedRate = 0;
        public static int g_MoveSpeedRate = 0;
        public const bool g_boFlashMission = false;
        public const bool g_boNewMission = false;
        // 新任务
        public const long g_dwNewMission = 0;
        public const int WH_KEYBOARD_LL = 13;
        public const int LLKHF_ALTDOWN = 0x20;
        public const string CONFIGFILE = "Config\\%s.ini";
        public const string g_affiche0 = "游戏音效已关闭！";
        public const string g_affiche1 = "健康游戏公告";
        public const string g_affiche2 = "抵制不良游戏 拒绝盗版游戏 注意自我保护 谨防受骗上当 适度游戏益脑";
        public const string g_affiche3 = "沉迷游戏伤身 合理安排时间 享受健康生活 严厉打击赌博 营造和谐环境";
        public const int VK2_SHIFT = 32;
        public const int VK2_CONTROL = 64;
        public const int VK2_ALT = 128;
        public const int VK2_WIN = 256;
        public const int SCREENWIDTH = 800;
        public const int SCREENHEIGHT = 600;
        public static string[] g_levelstring = { "一", "二", "三", "四", "五", "六", "七", "八" };

        // 得到地图文件名称自定义路径
        public static string GetMapDirAndName(string sFileName)
        {
            string result;
            if (File.Exists(WMFile.MAPDIRNAME + sFileName + ".map"))
            {
                result = WMFile.MAPDIRNAME + sFileName + ".map";
            }
            else
            {
                result = WMFile.OLDMAPDIRNAME + sFileName + ".map";
            }
            return result;
        }

        public static void ShowMsg(string Str)
        {
            //  ClMain.DScreen.AddChatBoardString(Str, System.Drawing.Color.White, System.Drawing.Color.Black);
        }

        public static void LoadMapDesc()
        {
            //int i;
            //string szFileName;
            //string szLine;
            //ArrayList xsl;
            //string szMapTitle = String.Empty;
            //string szPointX = String.Empty;
            //string szPointY = String.Empty;
            //string szPlaceName = String.Empty;
            //string szColor = String.Empty;
            //string szFullMap = String.Empty;
            //int nPointX;
            //int nPointY;
            //int nFullMap;
            //Color nColor;
            //TMapDescInfo pMapDescInfo;
            //szFileName = ".\\data\\MapDesc2.dat";
            //if (File.Exists(szFileName))
            //{
            //    xsl = new ArrayList();
            //    xsl.LoadFromFile(szFileName);
            //    for (i = 0; i < xsl.Count; i++)
            //    {
            //        szLine = xsl[i];
            //        if ((szLine == "") || (szLine[0] == ';'))
            //        {
            //            continue;
            //        }
            //        szLine = HGEGUI.GetValidStr3(szLine, ref szMapTitle, new string[] { "," });
            //        szLine = HGEGUI.GetValidStr3(szLine, ref szPointX, new string[] { "," });
            //        szLine = HGEGUI.GetValidStr3(szLine, ref szPointY, new string[] { "," });
            //        szLine = HGEGUI.GetValidStr3(szLine, ref szPlaceName, new string[] { "," });
            //        szLine = HGEGUI.GetValidStr3(szLine, ref szColor, new string[] { "," });
            //        szLine = HGEGUI.GetValidStr3(szLine, ref szFullMap, new string[] { "," });
            //        nPointX = HUtil32.Str_ToInt(szPointX, -1);
            //        nPointY = HUtil32.Str_ToInt(szPointY, -1);
            //        nColor = Convert.ToInt32(szColor);
            //        nFullMap = HUtil32.Str_ToInt(szFullMap, -1);
            //        if ((szPlaceName != "") && (szMapTitle != "") && (nPointX >= 0) && (nPointY >= 0) && (nFullMap >= 0))
            //        {
            //            pMapDescInfo = new TMapDescInfo();
            //            pMapDescInfo.szMapTitle = szMapTitle;
            //            pMapDescInfo.szPlaceName = szPlaceName;
            //            pMapDescInfo.nPointX = nPointX;
            //            pMapDescInfo.nPointY = nPointY;
            //            pMapDescInfo.nColor = nColor;
            //            pMapDescInfo.nFullMap = nFullMap;
            //            g_xMapDescList.Add(szMapTitle, pMapDescInfo as Object);
            //        }
            //    }
            //}
        }

        public static int GetTickCount()
        {
            return HUtil32.GetTickCount(); ;
        }

        // stdcall;
        public static bool IsDetectItem(int idx)
        {
            return idx == DETECT_MIIDX_OFFSET;
        }

        public static bool IsBagItem(int idx)
        {
            return idx >= 6 && idx <= Grobal2.MAXBAGITEM - 1;
        }

        public static bool IsEquItem(int idx)
        {
            if (idx < 0)
            {
                int sel = -(idx + 1);
                return sel >= 0 && sel <= 13;
            }
            return false;
        }

        public static bool IsStorageItem(int idx)
        {
            return (idx >= SAVE_MIIDX_OFFSET) && (idx < SAVE_MIIDX_OFFSET + 46);
        }

        public static bool IsStallItem(int idx)
        {
            return (idx >= STALL_MIIDX_OFFSET) && (idx < STALL_MIIDX_OFFSET + 10);
        }

        public static void ResetSeriesSkillVar()
        {
            g_nCurrentMagic = 888;
            g_nCurrentMagic2 = 888;
            g_SeriesSkillStep = 0;
            g_SeriesSkillFire = false;
            g_SeriesSkillFire_100 = false;
            g_SeriesSkillReady = false;
            g_NextSeriesSkill = false;
            //FillChar(g_VenationInfos);   
            //FillChar(g_TempSeriesSkillArr);   
            //FillChar(g_HTempSeriesSkillArr); 
            //FillChar(g_SeriesSkillArr);      
        }

        public static int GetSeriesSkillIcon(int id)
        {
            int result;
            result = -1;
            switch (id)
            {
                case 100:
                    result = 950;
                    break;
                case 101:
                    result = 952;
                    break;
                case 102:
                    result = 956;
                    break;
                case 103:
                    result = 954;
                    break;
                case 104:
                    result = 942;
                    break;
                case 105:
                    result = 946;
                    break;
                case 106:
                    result = 940;
                    break;
                case 107:
                    result = 944;
                    break;
                case 108:
                    result = 934;
                    break;
                case 109:
                    result = 936;
                    break;
                case 110:
                    result = 932;
                    break;
                case 111:
                    result = 930;
                    break;
                case 112:
                    result = 944;
                    break;
            }
            return result;
        }

        public static void CheckSpeedCount(int Count)
        {
            g_dwCheckCount++;
            if (g_dwCheckCount > Count)
            {
                g_dwCheckCount = 0;
            }
        }

        public static bool IsPersentHP(int nMin, int nMax)
        {
            bool result = false;
            if (nMax != 0)
            {
                result = HUtil32.Round(nMin / nMax * 100) < g_gnProtectPercent[0];
            }
            return result;
        }

        public static bool IsPersentMP(int nMin, int nMax)
        {
            bool result = false;
            if (nMax != 0)
            {
                result = HUtil32.Round(nMin / nMax * 100) < g_gnProtectPercent[1];
            }
            return result;
        }

        public static bool IsPersentSpc(int nMin, int nMax)
        {
            bool result = false;
            if (nMax != 0)
            {
                result = HUtil32.Round(nMin / nMax * 100) < g_gnProtectPercent[3];
            }
            return result;
        }

        public static bool IsPersentBook(int nMin, int nMax)
        {
            bool result = false;
            if (nMax != 0)
            {
                result = HUtil32.Round(nMin / nMax * 100) < g_gnProtectPercent[5];
            }
            return result;
        }

        public static string GetJobName(int nJob)
        {
            string result = "";
            switch (nJob)
            {
                case 0:
                    result = g_sWarriorName;
                    break;
                case 1:
                    result = g_sWizardName;
                    break;
                case 2:
                    result = g_sTaoistName;
                    break;
                default:
                    result = g_sUnKnowName;
                    break;
            }
            return result;
        }

        public static string GetItemType(TItemType ItemType)
        {
            string result = string.Empty;
            switch (ItemType)
            {
                case TItemType.i_HPDurg:
                    result = "金创药";
                    break;
                case TItemType.i_MPDurg:
                    result = "魔法药";
                    break;
                case TItemType.i_HPMPDurg:
                    result = "高级药";
                    break;
                case TItemType.i_OtherDurg:
                    result = "其它药品";
                    break;
                case TItemType.i_Weapon:
                    result = "武器";
                    break;
                case TItemType.i_Dress:
                    result = "衣服";
                    break;
                case TItemType.i_Helmet:
                    result = "头盔";
                    break;
                case TItemType.i_Necklace:
                    result = "项链";
                    break;
                case TItemType.i_Armring:
                    result = "手镯";
                    break;
                case TItemType.i_Ring:
                    result = "戒指";
                    break;
                case TItemType.i_Belt:
                    result = "腰带";
                    break;
                case TItemType.i_Boots:
                    result = "鞋子";
                    break;
                case TItemType.i_Charm:
                    result = "宝石";
                    break;
                case TItemType.i_Book:
                    result = "技能书";
                    break;
                case TItemType.i_PosionDurg:
                    result = "毒药";
                    break;
                case TItemType.i_UseItem:
                    result = "消耗品";
                    break;
                case TItemType.i_Scroll:
                    result = "卷类";
                    break;
                case TItemType.i_Stone:
                    result = "矿石";
                    break;
                case TItemType.i_Gold:
                    result = "金币";
                    break;
                case TItemType.i_Other:
                    result = "其它";
                    break;
            }
            return result;
        }

        public static bool GetItemShowFilter(string sItemName)
        {
            return g_ShowItemList.ContainsKey(sItemName);
        }

        public static void LoadUserConfig(string sUserName)
        {
            //FileStream ini;
            //FileStream ini2;
            //string sFileName;
            //ArrayList Strings;
            //int i;
            //int no;
            //string sn;
            //string so;
            //sFileName = ".\\Config\\" + g_sServerName + "." + sUserName + ".Set";
            //ini = new FileStream(sFileName);
            //// base
            //g_gcGeneral[0] = ini.ReadBool("Basic", "ShowActorName", g_gcGeneral[0]);
            //g_gcGeneral[1] = ini.ReadBool("Basic", "DuraWarning", g_gcGeneral[1]);
            //g_gcGeneral[2] = ini.ReadBool("Basic", "AutoAttack", g_gcGeneral[2]);
            //g_gcGeneral[3] = ini.ReadBool("Basic", "ShowExpFilter", g_gcGeneral[3]);
            //g_MaxExpFilter = ini.ReadInteger("Basic", "ShowExpFilterMax", g_MaxExpFilter);
            //g_gcGeneral[4] = ini.ReadBool("Basic", "ShowDropItems", g_gcGeneral[4]);
            //g_gcGeneral[5] = ini.ReadBool("Basic", "ShowDropItemsFilter", g_gcGeneral[5]);
            //g_gcGeneral[6] = ini.ReadBool("Basic", "ShowHumanWing", g_gcGeneral[6]);
            //g_boAutoPickUp = ini.ReadBool("Basic", "AutoPickUp", g_boAutoPickUp);
            //g_gcGeneral[7] = ini.ReadBool("Basic", "AutoPickUpFilter", g_gcGeneral[7]);
            //g_boPickUpAll = ini.ReadBool("Basic", "PickUpAllItem", g_boPickUpAll);
            //g_gcGeneral[8] = ini.ReadBool("Basic", "HideDeathBody", g_gcGeneral[8]);
            //g_gcGeneral[9] = ini.ReadBool("Basic", "AutoFixItem", g_gcGeneral[9]);
            //g_gcGeneral[10] = ini.ReadBool("Basic", "ShakeScreen", g_gcGeneral[10]);
            //g_gcGeneral[13] = ini.ReadBool("Basic", "StruckShow", g_gcGeneral[13]);
            //g_gcGeneral[15] = ini.ReadBool("Basic", "HideStruck", g_gcGeneral[15]);
            //g_gcGeneral[14] = ini.ReadBool("Basic", "CompareItems", g_gcGeneral[14]);
            //ini2 = new FileStream(".\\lscfg.ini");
            //g_gcGeneral[11] = ini2.ReadBool("Setup", "EffectSound", g_gcGeneral[11]);
            //g_gcGeneral[12] = ini2.ReadBool("Setup", "EffectBKGSound", g_gcGeneral[12]);
            //g_lWavMaxVol = ini2.ReadInteger("Setup", "EffectSoundLevel", g_lWavMaxVol);
            //ini2.Free;
            //g_HitSpeedRate = ini.ReadInteger("Basic", "HitSpeedRate", g_HitSpeedRate);
            //g_MagSpeedRate = ini.ReadInteger("Basic", "MagSpeedRate", g_MagSpeedRate);
            //g_MoveSpeedRate = ini.ReadInteger("Basic", "MoveSpeedRate", g_MoveSpeedRate);
            //// Protect
            //g_gcProtect[0] = ini.ReadBool("Protect", "RenewHPIsAuto", g_gcProtect[0]);
            //g_gcProtect[1] = ini.ReadBool("Protect", "RenewMPIsAuto", g_gcProtect[1]);
            //g_gcProtect[3] = ini.ReadBool("Protect", "RenewSpecialIsAuto", g_gcProtect[3]);
            //g_gcProtect[5] = ini.ReadBool("Protect", "RenewBookIsAuto", g_gcProtect[5]);
            //g_gcProtect[7] = ini.ReadBool("Protect", "HeroRenewHPIsAuto", g_gcProtect[7]);
            //g_gcProtect[8] = ini.ReadBool("Protect", "HeroRenewMPIsAuto", g_gcProtect[8]);
            //g_gcProtect[9] = ini.ReadBool("Protect", "HeroRenewSpecialIsAuto", g_gcProtect[9]);
            //g_gcProtect[10] = ini.ReadBool("Protect", "HeroSidestep", g_gcProtect[10]);
            //g_gcProtect[11] = ini.ReadBool("Protect", "RenewSpecialIsAuto_MP", g_gcProtect[11]);
            //g_gnProtectTime[0] = ini.ReadInteger("Protect", "RenewHPTime", g_gnProtectTime[0]);
            //g_gnProtectTime[1] = ini.ReadInteger("Protect", "RenewMPTime", g_gnProtectTime[1]);
            //g_gnProtectTime[3] = ini.ReadInteger("Protect", "RenewSpecialTime", g_gnProtectTime[3]);
            //g_gnProtectTime[5] = ini.ReadInteger("Protect", "RenewBookTime", g_gnProtectTime[5]);
            //g_gnProtectTime[7] = ini.ReadInteger("Protect", "HeroRenewHPTime", g_gnProtectTime[7]);
            //g_gnProtectTime[8] = ini.ReadInteger("Protect", "HeroRenewMPTime", g_gnProtectTime[8]);
            //g_gnProtectTime[9] = ini.ReadInteger("Protect", "HeroRenewSpecialTime", g_gnProtectTime[9]);
            //g_gnProtectPercent[0] = ini.ReadInteger("Protect", "RenewHPPercent", g_gnProtectPercent[0]);
            //g_gnProtectPercent[1] = ini.ReadInteger("Protect", "RenewMPPercent", g_gnProtectPercent[1]);
            //g_gnProtectPercent[3] = ini.ReadInteger("Protect", "RenewSpecialPercent", g_gnProtectPercent[3]);
            //g_gnProtectPercent[7] = ini.ReadInteger("Protect", "HeroRenewHPPercent", g_gnProtectPercent[7]);
            //g_gnProtectPercent[8] = ini.ReadInteger("Protect", "HeroRenewMPPercent", g_gnProtectPercent[8]);
            //g_gnProtectPercent[9] = ini.ReadInteger("Protect", "HeroRenewSpecialPercent", g_gnProtectPercent[9]);
            //g_gnProtectPercent[10] = ini.ReadInteger("Protect", "HeroPerSidestep", g_gnProtectPercent[10]);
            //g_gnProtectPercent[5] = ini.ReadInteger("Protect", "RenewBookPercent", g_gnProtectPercent[5]);
            //g_gnProtectPercent[6] = ini.ReadInteger("Protect", "RenewBookNowBookIndex", g_gnProtectPercent[6]);
            //ClMain.frmMain.SendClientMessage(Grobal2.CM_HEROSIDESTEP, HUtil32.MakeLong(((int)g_gcProtect[10]), g_gnProtectPercent[10]), 0, 0, 0);
            //g_gcTec[0] = ini.ReadBool("Tec", "SmartLongHit", g_gcTec[0]);
            //g_gcTec[10] = ini.ReadBool("Tec", "SmartLongHit2", g_gcTec[10]);
            //g_gcTec[11] = ini.ReadBool("Tec", "SmartSLongHit", g_gcTec[11]);
            //g_gcTec[1] = ini.ReadBool("Tec", "SmartWideHit", g_gcTec[1]);
            //g_gcTec[2] = ini.ReadBool("Tec", "SmartFireHit", g_gcTec[2]);
            //g_gcTec[3] = ini.ReadBool("Tec", "SmartPureHit", g_gcTec[3]);
            //g_gcTec[4] = ini.ReadBool("Tec", "SmartShield", g_gcTec[4]);
            //g_gcTec[5] = ini.ReadBool("Tec", "SmartShieldHero", g_gcTec[5]);
            //g_gcTec[6] = ini.ReadBool("Tec", "SmartTransparence", g_gcTec[6]);
            //g_gcTec[9] = ini.ReadBool("Tec", "SmartThunderHit", g_gcTec[9]);
            //g_gcTec[7] = ini.ReadBool("AutoPractice", "PracticeIsAuto", g_gcTec[7]);
            //g_gnTecTime[8] = ini.ReadInteger("AutoPractice", "PracticeTime", g_gnTecTime[8]);
            //g_gnTecPracticeKey = ini.ReadInteger("AutoPractice", "PracticeKey", g_gnTecPracticeKey);
            //g_gcTec[12] = ini.ReadBool("Tec", "HeroSeriesSkillFilter", g_gcTec[12]);
            //g_gcTec[13] = ini.ReadBool("Tec", "SLongHit", g_gcTec[13]);
            //g_gcTec[14] = ini.ReadBool("Tec", "SmartGoMagic", g_gcTec[14]);
            //ClMain.frmMain.SendClientMessage(Grobal2.CM_HEROSERIESSKILLCONFIG, HUtil32.MakeLong(((int)g_gcTec[12]), 0), 0, 0, 0);
            //g_gcHotkey[0] = ini.ReadBool("Hotkey", "UseHotkey", g_gcHotkey[0]);
            //FrmDlg.DEHeroCallHero.SetOfHotKey(ini.ReadInteger("Hotkey", "HeroCallHero", 0));
            //FrmDlg.DEHeroSetAttackState.SetOfHotKey(ini.ReadInteger("Hotkey", "HeroSetAttackState", 0));
            //FrmDlg.DEHeroSetGuard.SetOfHotKey(ini.ReadInteger("Hotkey", "HeroSetGuard", 0));
            //FrmDlg.DEHeroSetTarget.SetOfHotKey(ini.ReadInteger("Hotkey", "HeroSetTarget", 0));
            //FrmDlg.DEHeroUnionHit.SetOfHotKey(ini.ReadInteger("Hotkey", "HeroUnionHit", 0));
            //FrmDlg.DESwitchAttackMode.SetOfHotKey(ini.ReadInteger("Hotkey", "SwitchAttackMode", 0));
            //FrmDlg.DESwitchMiniMap.SetOfHotKey(ini.ReadInteger("Hotkey", "SwitchMiniMap", 0));
            //FrmDlg.DxEditSSkill.SetOfHotKey(ini.ReadInteger("Hotkey", "SerieSkill", 0));
            //g_ShowItemList.LoadFromFile(".\\Data\\Filter.dat");
            //// ============================================================================
            //// g_gcAss[0] := ini.ReadBool('Ass', '0', g_gcAss[0]);
            //g_gcAss[1] = ini.ReadBool("Ass", "1", g_gcAss[1]);
            //g_gcAss[2] = ini.ReadBool("Ass", "2", g_gcAss[2]);
            //g_gcAss[3] = ini.ReadBool("Ass", "3", g_gcAss[3]);
            //g_gcAss[4] = ini.ReadBool("Ass", "4", g_gcAss[4]);
            //g_gcAss[5] = ini.ReadBool("Ass", "5", g_gcAss[5]);
            //g_gcAss[6] = ini.ReadBool("Ass", "6", g_gcAss[6]);
            //g_APPickUpList.Clear();
            //g_APMobList.Clear();
            //Strings = new ArrayList();
            //if (g_gcAss[5])
            //{
            //    sFileName = ".\\Config\\" + g_sServerName + "." + sUserName + ".ItemFilterEx.txt";
            //    if (File.Exists(sFileName))
            //    {
            //        Strings.LoadFromFile(sFileName);
            //    }
            //    else
            //    {
            //        Strings.SaveToFile(sFileName);
            //    }
            //    for (i = 0; i < Strings.Count; i++)
            //    {
            //        if ((Strings[i] == "") || (Strings[i][1] == ";"))
            //        {
            //            continue;
            //        }
            //        so = HGEGUI.Units.HGEGUI.GetValidStr3(Strings[i], ref sn, new string[] { ",", " ", "\09" });
            //        no = ((int)so != "");
            //        g_APPickUpList.Add(sn, ((no) as Object));
            //    }
            //}
            //if (g_gcAss[6])
            //{
            //    sFileName = ".\\Config\\" + g_sServerName + "." + sUserName + ".MonFilter.txt";
            //    if (File.Exists(sFileName))
            //    {
            //        Strings.LoadFromFile(sFileName);
            //    }
            //    else
            //    {
            //        Strings.SaveToFile(sFileName);
            //    }
            //    for (i = 0; i < Strings.Count; i++)
            //    {
            //        if ((Strings[i] == "") || (Strings[i][1] == ";"))
            //        {
            //            continue;
            //        }
            //        // , nil
            //        g_APMobList.Add(Strings[i]);
            //    }
            //}
        }

        public static void LoadItemDesc()
        {
            //if (File.Exists(fItemDesc))
            //{
            //    temp = new ArrayList();
            //    temp.LoadFromFile(fItemDesc);
            //    for (i = 0; i < temp.Count; i++)
            //    {
            //        if (temp[i] == "")
            //        {
            //            continue;
            //        }
            //        desc = HGEGUI.Units.HGEGUI.GetValidStr3(temp[i], ref Name, new string[] { "=" });
            //        desc = desc.Replace("\\", "");
            //        ps = new string();
            //        ps = desc;
            //        if ((Name != "") && (desc != ""))
            //        {
            //            // g_ItemDesc.Put(name, TObject(ps));
            //            g_ItemDesc.Add(Name, ps as object);
            //        }
            //    }
            //    temp.Free;
            //}
        }

        public static int GetLevelColor(byte iLevel)
        {
            int result;
            switch (iLevel)
            {
                case 0:
                    result = 0x00FFFFFF;
                    break;
                case 1:
                    result = 0x004AD663;
                    break;
                case 2:
                    result = 0x00E9A000;
                    break;
                case 3:
                    result = 0x00FF35B1;
                    break;
                case 4:
                    result = 0x000061EB;
                    break;
                case 5:
                    result = 0x005CF4FF;
                    break;
                case 15:
                    result = Color.Gray.ToArgb();
                    break;
                default:
                    result = 0x005CF4FF;
                    break;
            }
            return result;
        }

        public static void LoadItemFilter()
        {
            //int i;
            //int n;
            //string s;
            //string s0;
            //string s1;
            //string s2;
            //string s3;
            //string s4;
            //string fn;
            //ArrayList ls;
            //TCItemRule p;
            //TCItemRule p2;
            //fn = ".\\Data\\lsDefaultItemFilter.txt";
            //if (File.Exists(fn))
            //{
            //    ls = new ArrayList();
            //    ls.LoadFromFile(fn);
            //    for (i = 0; i < ls.Count; i++)
            //    {
            //        s = ls[i];
            //        if (s == "")
            //        {
            //            continue;
            //        }
            //        s = HGEGUI.Units.HGEGUI.GetValidStr3(s, ref s0, new string[] { "," });
            //        s = HGEGUI.Units.HGEGUI.GetValidStr3(s, ref s1, new string[] { "," });
            //        s = HGEGUI.Units.HGEGUI.GetValidStr3(s, ref s2, new string[] { "," });
            //        s = HGEGUI.Units.HGEGUI.GetValidStr3(s, ref s3, new string[] { "," });
            //        s = HGEGUI.Units.HGEGUI.GetValidStr3(s, ref s4, new string[] { "," });
            //        p = new TCItemRule();
            //        p.Name = s0;
            //        p.rare = s2 == "1";
            //        p.pick = s3 == "1";
            //        p.Show = s4 == "1";
            //        g_ItemsFilter_All.Put(s0, p as object);
            //        p2 = new TCItemRule();
            //        p2 = p;
            //        g_ItemsFilter_All_Def.Put(s0, p2 as object);
            //        n = Convert.ToInt32(s1);
            //        switch (n)
            //        {
            //            case 0:
            //                g_ItemsFilter_Dress.Add(s0, p as object);
            //                break;
            //            case 1:
            //                g_ItemsFilter_Weapon.Add(s0, p as object);
            //                break;
            //            case 2:
            //                g_ItemsFilter_Headgear.Add(s0, p as object);
            //                break;
            //            case 3:
            //                g_ItemsFilter_Drug.Add(s0, p as object);
            //                break;
            //            default:
            //                g_ItemsFilter_Other.Add(s0, p as object);
            //                break;
            //                // 服装
            //        }
            //    }
            //    ls.Free;
            //}
        }

        public static void LoadItemFilter2()
        {
            //int i;
            //string s;
            //string s0;
            //string s2;
            //string s3;
            //string s4;
            //string fn;
            //ArrayList ls;
            //TCItemRule p;
            //TCItemRule p2;
            //bool b2;
            //bool b3;
            //bool b4;
            //fn = ".\\Config\\" + g_sServerName + "." + ClMain.frmMain.m_sCharName + ".ItemFilter.txt";
            //// DScreen.AddChatBoardString(fn, clWhite, clBlue);
            //if (File.Exists(fn))
            //{
            //    // DScreen.AddChatBoardString('1', clWhite, clBlue);
            //    ls = new ArrayList();
            //    ls.LoadFromFile(fn);
            //    for (i = 0; i < ls.Count; i++)
            //    {
            //        s = ls[i];
            //        if (s == "")
            //        {
            //            continue;
            //        }
            //        s = HGEGUI.Units.HGEGUI.GetValidStr3(s, ref s0, new string[] { "," });
            //        s = HGEGUI.Units.HGEGUI.GetValidStr3(s, ref s2, new string[] { "," });
            //        s = HGEGUI.Units.HGEGUI.GetValidStr3(s, ref s3, new string[] { "," });
            //        s = HGEGUI.Units.HGEGUI.GetValidStr3(s, ref s4, new string[] { "," });
            //        p = ((TCItemRule)(g_ItemsFilter_All_Def.GetValues(s0)));
            //        if (p != null)
            //        {
            //            // DScreen.AddChatBoardString('2', clWhite, clBlue);
            //            b2 = s2 == "1";
            //            b3 = s3 == "1";
            //            b4 = s4 == "1";
            //            if ((b2 != p.rare) || (b3 != p.pick) || (b4 != p.Show))
            //            {
            //                // DScreen.AddChatBoardString('3', clWhite, clBlue);
            //                p2 = ((TCItemRule)(g_ItemsFilter_All.GetValues(s0)));
            //                if (p2 != null)
            //                {
            //                    // DScreen.AddChatBoardString('4', clWhite, clBlue);
            //                    p2.rare = b2;
            //                    p2.pick = b3;
            //                    p2.Show = b4;
            //                }
            //            }
            //        }
            //    }
            //    ls.Free;
            //}
        }

        public static void SaveItemFilter()
        {
            // 退出增量保存
            //int i;
            //ArrayList ls;
            //TCItemRule p;
            //TCItemRule p2;
            //string fn;
            //fn = ".\\Config\\" + g_sServerName + "." + ClMain.frmMain.m_sCharName + ".ItemFilter.txt";
            //ls = new ArrayList();
            //for (i = 0; i < g_ItemsFilter_All.Count; i++)
            //{
            //    p = ((TCItemRule)(g_ItemsFilter_All.GetValues(g_ItemsFilter_All.Keys[i])));
            //    p2 = ((TCItemRule)(g_ItemsFilter_All_Def.GetValues(g_ItemsFilter_All_Def.Keys[i])));
            //    if (p.Name == p2.Name)
            //    {
            //        if ((p.rare != p2.rare) || (p.pick != p2.pick) || (p.Show != p2.Show))
            //        {
            //            ls.Add(string.Format("%s,%d,%d,%d", new byte[] { p.Name, ((byte)p.rare), ((byte)p.pick), ((byte)p.Show) }));
            //        }
            //    }
            //}
            //if (ls.Count > 0)
            //{
            //    ls.SaveToFile(fn);
            //}
            //ls.Free;
        }

        public static int GetItemWhere(TClientItem clientItem)
        {
            int result;
            result = -1;
            if (clientItem.Item.Name == "")
            {
                return result;
            }
            switch (clientItem.Item.StdMode)
            {
                case 10:
                case 11:
                    result = Grobal2.U_DRESS;
                    break;
                case 5:
                case 6:
                    result = Grobal2.U_WEAPON;
                    break;
                case 30:
                    result = Grobal2.U_RIGHTHAND;
                    break;
                case 19:
                case 20:
                case 21:
                    result = Grobal2.U_NECKLACE;
                    break;
                case 15:
                    result = Grobal2.U_HELMET;
                    break;
                case 16:
                    break;
                case 24:
                case 26:
                    result = Grobal2.U_ARMRINGL;
                    break;
                case 22:
                case 23:
                    result = Grobal2.U_RINGL;
                    break;
                case 25:
                    result = Grobal2.U_BUJUK;
                    break;
                case 27:
                    result = Grobal2.U_BELT;
                    break;
                case 28:
                    result = Grobal2.U_BOOTS;
                    break;
                case 7:
                case 29:
                    result = Grobal2.U_CHARM;
                    break;
            }
            return result;
        }

        public static bool GetSecretAbil(TClientItem CurrMouseItem)
        {
            bool result;
            result = false;
            if (!new ArrayList(new int[] { 5, 6, 10, 15, 26 }).Contains(CurrMous.Item.Item.StdMode))
            {
                return result;
            }
            return result;
        }

        public static void InitClientItems()
        {
            //FillChar(g_MagicArr);            
            //FillChar(g_TakeBackItemWait);           
            //FillChar(g_UseItems);           
            //FillChar(g_ItemArr);           
            //FillChar(g_RefineItems);     
            //FillChar(g_BuildAcuses);     
            //FillChar(g_DetectItem);    
            //FillChar(g_TIItems);        
            //FillChar(g_spItems);     
            //FillChar(g_ItemArr);        
            //FillChar(g_HeroItemArr);  
            //FillChar(g_DealItems);       
            //FillChar(g_YbDealItems);         
            //FillChar(g_DealRemoteItems);
        }

        public static byte GetTIHintString1(int idx, TClientItem ci, string iname)
        {
            byte result;
            result = 0;
            g_tiHintStr1 = "";
            //switch (idx)
            //{
            //    case 0:
            //        g_tiHintStr1 = "我收藏天下的奇珍异宝，走南闯北几十年了，各种神器见过不少，把你要鉴定的装备放在桌子上吧！";
            //        FrmDlg.DBTIbtn1.btnState = tdisable;
            //        FrmDlg.DBTIbtn1.Caption = "普通鉴定";
            //        FrmDlg.DBTIbtn2.btnState = tdisable;
            //        FrmDlg.DBTIbtn2.Caption = "高级鉴定";
            //        break;
            //    case 1:
            //        if ((ci == null) || (ci.s.Name == ""))
            //        {
            //            return result;
            //        }
            //        if (ci.Item.Eva.EvaTimesMax == 0)
            //        {
            //            g_tiHintStr1 = "标志了不可鉴定的物品我是鉴定不了的，你换一个吧。";
            //            FrmDlg.DBTIbtn1.btnState = tdisable;
            //            FrmDlg.DBTIbtn1.Caption = "普通鉴定";
            //            FrmDlg.DBTIbtn2.btnState = tdisable;
            //            FrmDlg.DBTIbtn2.Caption = "高级鉴定";
            //            return result;
            //        }
            //        if (ci.Item.Eva.EvaTimes < ci.Item.Eva.EvaTimesMax)
            //        {
            //            if (FrmDlg.DWTI.tag == 1)
            //            {
            //                switch (ci.Item.Eva.EvaTimes)
            //                {
            //                    case 0:
            //                        g_tiHintStr1 = "第一次鉴定我需要一个一级鉴定卷轴，你快去收集一个吧！";
            //                        FrmDlg.DBTIbtn1.btnState = tnor;
            //                        FrmDlg.DBTIbtn1.Caption = "普通一鉴";
            //                        FrmDlg.DBTIbtn2.btnState = tnor;
            //                        FrmDlg.DBTIbtn2.Caption = "高级一鉴";
            //                        break;
            //                    case 1:
            //                        g_tiHintStr1 = "第二次鉴定我需要一个二级鉴定卷轴，你快去收集一个吧！";
            //                        FrmDlg.DBTIbtn1.btnState = tnor;
            //                        FrmDlg.DBTIbtn1.Caption = "普通二鉴";
            //                        FrmDlg.DBTIbtn2.btnState = tnor;
            //                        FrmDlg.DBTIbtn2.Caption = "高级二鉴";
            //                        break;
            //                    case 2:
            //                        g_tiHintStr1 = "第三次鉴定我需要一个三级鉴定卷轴，你快去收集一个吧！";
            //                        FrmDlg.DBTIbtn1.btnState = tnor;
            //                        FrmDlg.DBTIbtn1.Caption = "普通三鉴";
            //                        FrmDlg.DBTIbtn2.btnState = tnor;
            //                        FrmDlg.DBTIbtn2.Caption = "高级三鉴";
            //                        break;
            //                    default:
            //                        g_tiHintStr1 = "我需要一个三级鉴定卷轴来鉴定你这个装备。";
            //                        FrmDlg.DBTIbtn1.btnState = tnor;
            //                        FrmDlg.DBTIbtn1.Caption = "普通三鉴";
            //                        FrmDlg.DBTIbtn2.btnState = tnor;
            //                        FrmDlg.DBTIbtn2.Caption = "高级三鉴";
            //                        break;
            //                }
            //            }
            //            else if (FrmDlg.DWTI.tag == 2)
            //            {
            //                FrmDlg.DBTIbtn1.btnState = tnor;
            //                FrmDlg.DBTIbtn1.Caption = "更换";
            //            }
            //            result = ci.Item.Eva.EvaTimes;
            //        }
            //        else
            //        {
            //            g_tiHintStr1 = string.Format("你的这件%s已经不能再鉴定了。", new string[] { ci.s.Name });
            //            FrmDlg.DBTIbtn1.btnState = tdisable;
            //            FrmDlg.DBTIbtn1.Caption = "普通鉴定";
            //            FrmDlg.DBTIbtn2.btnState = tdisable;
            //            FrmDlg.DBTIbtn2.Caption = "高级鉴定";
            //        }
            //        break;
            //    case 2:
            //        g_tiHintStr1 = string.Format("借助卷轴的力量，我已经帮你发现了你这%s的潜能。", new string[] { iname });
            //        break;
            //    case 3:
            //        g_tiHintStr1 = string.Format("借助卷轴的力量，我已经帮你发现了你这%s的神秘潜能。", new string[] { iname });
            //        break;
            //    case 4:
            //        g_tiHintStr1 = string.Format("这%s虽然没能发现更大的潜能，但是他拥有感应其他宝物存在的特殊能力。", new string[] { iname });
            //        break;
            //    case 5:
            //        g_tiHintStr1 = string.Format("我并没能从你的这个%s上发现更多的潜能。你不要沮丧，我会给你额外的补偿。", new string[] { iname });
            //        break;
            //    case 6:
            //        g_tiHintStr1 = string.Format("我并没能从你的这个%s上发现更多的潜能。", new string[] { iname });
            //        break;
            //    case 7:
            //        g_tiHintStr1 = string.Format("我并没能从你的这个%s上发现更多的潜能。你的宝物已经不可鉴定。", new string[] { iname });
            //        break;
            //    case 8:
            //        g_tiHintStr1 = "你缺少宝物或者卷轴。";
            //        break;
            //    case 9:
            //        g_tiHintStr1 = string.Format("恭喜你的宝物被鉴定为主宰装备，你获得了%s。", new string[] { iname });
            //        break;
            //    case 10:
            //        g_tiHintStr1 = "待鉴物品错误或不存在！";
            //        break;
            //    case 11:
            //        g_tiHintStr1 = string.Format("你的这件%s不可以鉴定！", new string[] { iname });
            //        break;
            //    case 12:
            //        FrmDlg.DBTIbtn1.btnState = tdisable;
            //        FrmDlg.DBTIbtn2.btnState = tdisable;
            //        g_tiHintStr1 = string.Format("以我目前的能力，%s只能先鉴定到这里了。", new string[] { iname });
            //        break;
            //    case 30:
            //        g_tiHintStr1 = "鉴定卷轴错误或不存在！";
            //        break;
            //    case 31:
            //        g_tiHintStr1 = string.Format("我需要一个%s级鉴定卷轴，你的卷轴不符合要求！", new string[] { iname });
            //        break;
            //    case 32:
            //        g_tiHintStr1 = string.Format("高级鉴定失败，你的%s消失了！", new string[] { iname });
            //        break;
            //    case 33:
            //        g_tiHintStr1 = string.Format("服务器没有%s的数据，高级鉴定失败！", new string[] { iname });
            //        break;
            //}
            return result;
        }

        public static byte GetTIHintString1(int idx)
        {
            return GetTIHintString1(idx, null);
        }

        public static byte GetTIHintString1(int idx, TClientItem ci)
        {
            return GetTIHintString1(idx, ci, "");
        }

        public static byte GetTIHintString2(int idx, TClientItem ci, string iname)
        {
            byte result;
            result = 0;
            /*g_tiHintStr1 = "";
            switch (idx)
            {
                case 0:
                    g_tiHintStr1 = "如果你不喜欢已经鉴定过了的宝物，你可以把他给我，我平素最爱收藏各种宝物，我会给你一个一模一样的没鉴定过的装备作为补偿。";
                    FrmDlg.DBTIbtn1.btnState = tdisable;
                    break;
                case 1:
                    g_tiHintStr1 = string.Format("这个%s，看上去不错，我这里正好有没有鉴定过的各种%s你可以挑一把，要换的话，你要给我一个幸运符。", new string[] { ci.s.Name, ci.s.Name });
                    FrmDlg.DBTIbtn1.btnState = tnor;
                    break;
                case 2:
                    g_tiHintStr1 = string.Format("我已经给了你一把没鉴定过的%s，跟你原来的%s没鉴定过之前是一模一样的！", new string[] { iname, iname });
                    break;
                case 3:
                    g_tiHintStr1 = "缺少宝物或材料。";
                    break;
                case 4:
                    g_tiHintStr1 = string.Format("你的这件%s并没有鉴定过。", new string[] { iname });
                    break;
                case 5:
                    g_tiHintStr1 = "材料不符合，请放入幸运符。";
                    break;
                case 6:
                    g_tiHintStr1 = "该物品框只能放鉴定过的宝物，你的东西不符合，我已经将它放回你的包裹了。";
                    break;
                case 7:
                    g_tiHintStr1 = "该物品框只能放幸运符，你的东西不符合，我已经将它放回你的包裹了。";
                    break;
                case 8:
                    g_tiHintStr1 = "宝物更换失败。";
                    break;
            }*/
            return result;
        }

        public static byte GetTIHintString2(int idx)
        {
            return GetTIHintString2(idx, null);
        }

        public static byte GetTIHintString2(int idx, TClientItem ci)
        {
            return GetTIHintString2(idx, ci, "");
        }

        public static byte GetSpHintString1(int idx, TClientItem ci, string iname)
        {
            byte result;
            result = 0;
            /*g_spHintStr1 = "";
            switch (idx)
            {
                case 0:
                    g_spHintStr1 = "你可以跟别人购买神秘卷轴，也可以自己制作神秘卷轴来解读宝物的神秘属性。";
                    break;
                case 1:
                    g_spHintStr1 = "这次解读不幸失败，解读幸运值、神秘卷轴" + "的等级和熟练度过低可能导致解读失败，不" + "要失望，再接再厉吧。";
                    break;
                case 2:
                    g_spHintStr1 = "找不到鉴定物品或卷轴";
                    break;
                case 3:
                    FrmDlg.DBSP.btnState = tdisable;
                    g_spHintStr1 = "没有可鉴定的神秘属性";
                    break;
                case 4:
                    g_spHintStr1 = "装备不符合神秘解读要求";
                    break;
                case 5:
                    g_spHintStr1 = "卷轴类型不符合";
                    break;
                case 6:
                    g_spHintStr1 = "卷轴等级不符合";
                    break;
                case 7:
                    g_spHintStr1 = "借助神秘卷轴的帮助，已经帮你解读出了一个神秘属性";
                    break;
                case 10:
                    g_spHintStr1 = "神秘卷轴制作成功。";
                    break;
                case 11:
                    g_spHintStr1 = "这次制作不幸的失败了，可能是因为你的神" + "秘解读技能等级还不够高，或者是你制作的" + "卷轴等级太高了";
                    break;
                case 12:
                    g_spHintStr1 = "找不到羊皮卷。";
                    break;
                case 13:
                    g_spHintStr1 = "请放入羊皮卷。";
                    break;
                case 14:
                    g_spHintStr1 = "精力值不够。";
                    break;
                case 15:
                    g_spHintStr1 = "没有解读技能，制作失败。";
                    break;
            }*/
            return result;
        }

        public static byte GetSpHintString1(int idx)
        {
            return GetSpHintString1(idx, null);
        }

        public static byte GetSpHintString1(int idx, TClientItem ci)
        {
            return GetSpHintString1(idx, ci, "");
        }

        public static byte GetSpHintString2(int idx, TClientItem ci, string iname)
        {
            byte result;
            result = 0;
            g_spHintStr1 = "";
            switch (idx)
            {
                case 0:
                    g_spHintStr1 = "你可以把你对鉴宝的心得还有你的鉴定经验写在神秘卷轴上，这样的话，就可以帮助更多人解读神秘属性。";
                    break;
            }
            return result;
        }

        public static byte GetSpHintString2(int idx)
        {
            return GetSpHintString2(idx, null);
        }

        public static byte GetSpHintString2(int idx, TClientItem ci)
        {
            return GetSpHintString2(idx, ci, "");
        }

        public static void AutoPutOntiBooks()
        {
            //if ((g_TIItems[0].Item.Item.Name != "") && (g_TIItems[0].Item.Item.Eva.EvaTimesMax > 0) && (g_TIItems[0].Item.Item.Eva.EvaTimes < g_TIItems[0].Item.Item.Eva.EvaTimesMax) && ((g_TIItems[1].Item.Item.Name == "") || (g_TIItems[1].Item.Item.StdMode != 56) || !(g_TIItems[1].Item.Item.Shape >= 1 && g_TIItems[1].Item.Item.Shape <= 3) || (g_TIItems[1].Item.Item.Shape != g_TIItems[0].Item.Item.Eva.EvaTimes + 1)))
            //{
            //    for (i = MAXBAGITEMCL - 1; i >= 6; i--)
            //    {
            //        if ((g_ItemArr[i].Item.Name != "") && (g_ItemArr[i].Item.StdMode == 56) && (g_ItemArr[i].Item.Shape == g_TIItems[0].Item.Eva.EvaTimes + 1))
            //        {
            //            if (g_TIItems[1].Item.Item.Name != "")
            //            {
            //                cu = g_TIItems[1].Item;
            //                g_TIItems[1].Item = g_ItemArr[i];
            //                g_TIItems[1].Index = i;
            //                g_ItemArr[i] = cu;
            //            }
            //            else
            //            {
            //                g_TIItems[1].Item = g_ItemArr[i];
            //                g_TIItems[1].Index = i;
            //                g_ItemArr[i].Item.Name = "";
            //            }
            //            break;
            //        }
            //    }
            //}
        }

        public static void AutoPutOntiSecretBooks()
        {
            //if (FrmDlg.DWSP.Visible && (FrmDlg.DWSP.tag == 1) && (g_spItems[0].Item.Item.Name != "") && (g_spItems[0].Item.Item.Eva.EvaTimesMax > 0) && ((g_spItems[1].Item.Item.Name == "") || (g_spItems[1].Item.Item.StdMode != 56) || (g_spItems[1].Item.                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                             s.Shape != 0)))
            //{
            //    for (i = MAXBAGITEMCL - 1; i >= 6; i--)
            //    {
            //        if ((g_ItemArr[i].Item.Name != "") && (g_ItemArr[i].Item.StdMode == 56) && (g_ItemArr[i].Item.Shape == 0))
            //        {
            //            if (g_spItems[1].Item.Item.Name != "")
            //            {
            //                cu = g_spItems[1].Item;
            //                g_spItems[1].Item = g_ItemArr[i];
            //                g_spItems[1].Index = i;
            //                g_ItemArr[i] = cu;
            //            }
            //            else
            //            {
            //                g_spItems[1].Item = g_ItemArr[i];
            //                g_spItems[1].Index = i;
            //                g_ItemArr[i].Item.Name = "";
            //            }
            //            break;
            //        }
            //    }
            //}
        }

        public static void AutoPutOntiCharms()
        {
            //if ((g_TIItems[0].Item.Item.Name != "") && (g_TIItems[0].Item.Item.Eva.EvaTimesMax > 0) && (g_TIItems[0].Item.Item.Eva.EvaTimes > 0) && ((g_TIItems[1].Item.Item.Name == "") || (g_TIItems[1].Item.Item.StdMode != 41) || (g_TIItems[1].Item.Item.Shape != 30)))
            //{
            //    for (i = MAXBAGITEMCL - 1; i >= 6; i--)
            //    {
            //        if ((g_ItemArr[i].Item.Name != "") && (g_ItemArr[i].Item.StdMode == 41) && (g_ItemArr[i].Item.Shape == 30))
            //        {
            //            if (g_TIItems[1].Item.Item.Name != "")
            //            {
            //                cu = g_TIItems[1].Item;
            //                g_TIItems[1].Item = g_ItemArr[i];
            //                g_TIItems[1].Index = i;
            //                g_ItemArr[i] = cu;
            //            }
            //            else
            //            {
            //                g_TIItems[1].Item = g_ItemArr[i];
            //                g_TIItems[1].Index = i;
            //                g_ItemArr[i].Item.Name = "";
            //            }
            //            break;
            //        }
            //    }
            //}
        }

        public static bool GetSuiteAbil(int idx, int Shape, ref byte[] sa)
        {
            bool result = false;
            //FillChar(sa);           
            //switch (idx)
            //{
            //    case 1:
            //        result = true;
            //        for (i = TtSuiteAbil.GetLowerBound(0); i <= TtSuiteAbil.GetUpperBound(0); i++ )
            //        {
            //            if ((g_UseItems[i].s.Name != "") && ((g_UseItems[i].s.Shape == Shape) || (g_UseItems[i].s.AniCount == Shape)))
            //            {
            //                sa[i] = 1;
            //            }
            //        }
            //        break;
            //    case 2:
            //        result = true;
            //        for (i = Grobal2.byte.GetLowerBound(0); i <= Grobal2.byte.GetUpperBound(0); i++ )
            //        {
            //            if ((g_HeroUseItems[i].s.Name != "") && ((g_HeroUseItems[i].s.Shape == Shape) || (g_HeroUseItems[i].s.AniCount == Shape)))
            //            {
            //                sa[i] = 1;
            //            }
            //        }
            //        break;
            //    case 3:
            //        result = true;
            //        for (i = Grobal2.byte.GetLowerBound(0); i <= Grobal2.byte.GetUpperBound(0); i++ )
            //        {
            //            if ((UserState1.UseItems[i].s.Name != "") && ((UserState1.UseItems[i].s.Shape == Shape) || (UserState1.UseItems[i].s.AniCount == Shape)))
            //            {
            //                sa[i] = 1;
            //            }
            //        }
            //        break;
            //}
            return result;
        }

        public static void InitScreenConfig()
        {
            // 屏幕顶部滚动公告-范围
            //g_SkidAD_Rect.Left = 5;
            //g_SkidAD_Rect.Top = 7;
            //g_SkidAD_Rect.Right = SCREENWIDTH - 5;
            //g_SkidAD_Rect.Bottom = 7 + 20;
            //g_SkidAD_Rect2.Left = 183;
            //g_SkidAD_Rect2.Top = 6;
            //g_SkidAD_Rect2.Right = SCREENWIDTH - 208;
            //g_SkidAD_Rect2.Bottom = 6 + 20;
            //G_RC_SQUENGINER.Left = 78;
            //G_RC_SQUENGINER.Top = 90;
            //G_RC_SQUENGINER.Right = G_RC_SQUENGINER.Left + 16;
            //G_RC_SQUENGINER.Bottom = G_RC_SQUENGINER.Top + 95;
            //G_RC_IMEMODE.Left = SCREENWIDTH - 270 - 65;
            //G_RC_IMEMODE.Top = 105;
            //G_RC_IMEMODE.Right = G_RC_IMEMODE.Left + 60;
            //G_RC_IMEMODE.Bottom = G_RC_IMEMODE.Top + 9;
        }

        public static bool IsInMyRange(TActor Act)
        {
            bool result;
            result = false;
            if ((Act == null) || (g_MySelf == null))
            {
                return result;
            }
            if ((Math.Abs(Act.m_nCurrX - g_MySelf.m_nCurrX) <= (g_TileMapOffSetX - 2)) && (Math.Abs(Act.m_nCurrY - g_MySelf.m_nCurrY) <= (g_TileMapOffSetY - 1)))
            {
                result = true;
            }
            return result;
        }

        public static bool IsItemInMyRange(int X, int Y)
        {
            bool result;
            result = false;
            if (g_MySelf == null)
            {
                return result;
            }
            if ((Math.Abs(X - g_MySelf.m_nCurrX) <= HUtil32._MIN(24, g_TileMapOffSetX + 9)) && (Math.Abs(Y - g_MySelf.m_nCurrY) <= HUtil32._MIN(24, g_TileMapOffSetY + 10)))
            {
                result = true;
            }
            return result;
        }

        public void initialization()
        {
            //g_APPass = new double();
            //g_dwThreadTick = new long();
            //g_dwThreadTick = 0;
            //g_pbRecallHero = new bool();
            //g_pbRecallHero = false;
            //InitializeCriticalSection(ProcMsgCS);
            //InitializeCriticalSection(ThreadCS);
            //g_APPickUpList = new THStringList();
            //g_APMobList = new THStringList();
            //g_ItemsFilter_All = new object();
            //g_ItemsFilter_All_Def = new object();
            //g_ItemsFilter_Dress = new object();
            //g_ItemsFilter_Weapon = new object();
            //g_ItemsFilter_Headgear = new object();
            //g_ItemsFilter_Drug = new object();
            //g_ItemsFilter_Other = new object();
            //g_SuiteItemsList = new object();
            //g_TitlesList = new object();
            //g_xMapDescList = new object();
            //g_xCurMapDescList = new object();
        }

        public void finalization()
        {
            //Dispose(g_APPass);
            //DeleteCriticalSection(ProcMsgCS);
            //DeleteCriticalSection(ThreadCS);
            //g_APPickUpList.Free;
            //g_APMobList.Free;
            //g_ItemsFilter_All.Free;
            //g_ItemsFilter_All_Def.Free;
            //g_ItemsFilter_Dress.Free;
            //g_ItemsFilter_Weapon.Free;
            //g_ItemsFilter_Headgear.Free;
            //g_ItemsFilter_Drug.Free;
            //g_ItemsFilter_Other.Free;
            //g_SuiteItemsList.Free;
            //g_xMapDescList.Free;
            //g_xCurMapDescList.Free;
        }

    }

    public struct TVaInfo
    {
        public string cap;
        public Rectangle[] pt1;
        public Rectangle[] pt2;
        public string[] str1;
        public string[] Hint;
    } // end TVaInfo

    public class TFindNode
    {
        public int X;
        public int Y;
    } // end TFindNode

    public class Tree
    {
        public int H;
        public int X;
        public int Y;
        public byte Dir;
        public Tree Father;
    }

    public class Link
    {
        public Tree Node;
        public int F;
        public Link Next;
    }

    public struct TVirusSign
    {
        public int Offset;
        public string CodeSign;
    }

    public class TMovingItem
    {
        public int Index;
        public TClientItem Item;
    }

    public struct TCleintBox
    {
        public int Index;
        public TClientItem Item;
    }

    public enum MagicType
    {
        mtReady,
        mtFly,
        mtExplosion,
        mtFlyAxe,
        mtFireWind,
        mtFireGun,
        mtLightingThunder,
        mtThunder,
        mtExploBujauk,
        mtBujaukGroundEffect,
        mtKyulKai,
        mtFlyArrow,
        mtFlyBug,
        mtGroundEffect,
        mtThuderEx,
        mtFireBall,
        mtFlyBolt,
        mtRedThunder,
        mtRedGroundThunder,
        mtLava,
        mtSpurt,
        mtFlyStick,
        mtFlyStick2
    }

    public struct TShowItem
    {
        public string sItemName;
        public TItemType ItemType;
        public bool boAutoPickup;
        public bool boShowName;
        public int nFColor;
        public int nBColor;
    } // end TShowItem

    public struct TMapDescInfo
    {
        public string szMapTitle;
        public string szPlaceName;
        public int nPointX;
        public int nPointY;
        public Color nColor;
        public int nFullMap;
    } // end TMapDescInfo

    public class TItemShine
    {
        public int idx;
        public long tick;
    }

    public struct TSeriesSkill
    {
        public byte wMagid;
        public byte nStep;
        public bool bSpell;
    } // end TSeriesSkill

    public struct TTempSeriesSkillA
    {
        public TClientMagic pm;
        public bool bo;
    } // end TTempSeriesSkillA

    public enum TTimerCommand
    {
        tcSoftClose,
        tcReSelConnect,
        tcFastQueryChr,
        tcQueryItemPrice
    } // end TTimerCommand

    public enum TChrAction
    {
        caWalk,
        caRun,
        caHorseRun,
        caHit,
        caSpell,
        caSitdown
    } // end TChrAction

    public enum TConnectionStep
    {
        cnsIntro,
        cnsLogin,
        cnsSelChr,
        cnsReSelChr,
        cnsPlay
    } // end TConnectionStep

    public enum TItemType
    {
        i_HPDurg,
        i_MPDurg,
        i_HPMPDurg,
        i_OtherDurg,
        i_Weapon,
        i_Dress,
        i_Helmet,
        i_Necklace,
        i_Armring,
        i_Ring,
        i_Belt,
        i_Boots,
        i_Charm,
        i_Book,
        i_PosionDurg,
        i_UseItem,
        i_Scroll,
        i_Stone,
        i_Gold,
        i_Other
    }

    public class TChrMsg
    {
        public int Ident;
        public int X;
        public int Y;
        public int Dir;
        public int State;
        public int Feature;
        public int Saying;
        public int Sound;
        public int dwDelay;
    }

    public class TDropItem
    {
        public int X;
        public int Y;
        public int id;
        public int looks;
        public string Name;
        public int Width;
        public int Height;
        public int FlashTime;
        public int FlashStepTime;
        public int FlashStep;
        public bool BoFlash;
        public bool boNonSuch;
        public bool boPickUp;
        public bool boShowName;
    }
}