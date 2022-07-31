using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using SystemModule;

namespace GameSvr
{
    public struct TMonInfo
    {
        public string Name;
        public int Race;
    }

    public struct TSaveRcd
    {
        public string uid;
        public string uname;
        public int certify;
        public int savefail;
        public long savetime;
        public TUserHuman hum;
        public FDBRecord rcd;
    }

    public struct TReadyUserInfo
    {
        public string UserId;
        public string UserName;
        public string UserAddress;
        public bool StartNew;
        public int Certification;
        public int ApprovalMode;
        public int AvailableMode;
        public int ClientVersion;
        public int LoginClientVersion;
        public int ClientCheckSum;
        public int Shandle;
        public short UserGateIndex;
        public int GateIndex;
        public long ReadyStartTime;
        public bool Closed;
    }

    public struct TChangeUserInfo
    {
        public string CommandWho;
        public string UserName;
        public int ChangeGold;
    }

    public struct TUserOpenInfo
    {
        public string Name;
        public FDBRecord rcd;
        public TReadyUserInfo readyinfo;
    }

    public class THolySeizeInfo
    {
        public object[] earr;
        public ArrayList seizelist;
        public long OpenTime;
        public long SeizeTime;
    }

    public class M2Share
    {
        public static string GateClass = "Config";

        public static TGameConfig g_GameConfig = new TGameConfig()
        {
            boGameAssist = true,
            boWhisperRecord = true,
            boMaketSystem = true,
            boNoFog = false,
            boStallSystem = true,
            boShowHpBar = true,
            boShowHpNumber = true,
            boNoStruck = false,
            boFastMove = false,
            boNoWeight = true
        };
        public static GameHost FrmMain = null;
        public static TRunSocket RunSocket = null;
        public static ObjectManager ObjectMgr;
        public static TFrontEngine FrontEngine = null;
        public static TUserEngine UserEngine = null;
        public static TUserMgrEngine UserMgrEngine = null;
        public static TEnvirList GrobalEnvir = null;
        public static ItemUnitSystem ItemMan = null;
        public static TMagicManager MagicMan = null;
        public static TNoticeManager NoticeMan = null;
        public static TGuildManager GuildMan = null;
        public static TGuildAgitManager GuildAgitMan = null;
        public static TGuildAgitBoardManager GuildAgitBoardMan = null;
        public static int GuildAgitStartNumber = 0;
        public static int GuildAgitMaxNumber = 0;
        public static TEventManager EventMan = null;
        public static TUserCastle UserCastle = null;
        public static TDataMgr LocalDB = null;
        public static bool boUserCastleInitialized = false;
        public static TDragonSystem gFireDragon = null;
        public static ArrayList DecoItemList = null;
        public static IList<string> MakeItemList = null;
        public static ArrayList MakeItemIndexList = null;
        public static IList<TSafePoint> StartPoints = null;
        public static IList<TSafePoint> SafePoints = null;
        public static ArrayList MultiServerList = null;
        public static ArrayList ShutUpList = null;
        public static ArrayList MiniMapList = null;
        public static IList<string> UnbindItemList = null;
        public static ArrayList LineNoticeList = null;
        public static ArrayList LineHelpList = null;
        public static ArrayList QuestDiaryList = null;
        public static TMerchant DefaultNpc = null;
        public static ArrayList DropItemNoticeList = null;
        public static IList<TShopItem> ShopItemList = null;
        public static ArrayList EventItemList = null;
        public static int EventItemGifeBaseNumber = 0;
        public static int[] GrobalQuestParams = new int[9 + 1];
        public static int MirDayTime;
        public static string ErrorLogFile = string.Empty;
        public static int ServerIndex = 0;
        public static string ServerName = string.Empty;
        public static int ServerNumber = 0;
        public static bool BoVentureServer = false;
        public static bool BoTestServer = false;
        public static bool BoClientTest = false;
        public static int TestLevel = 0;
        public static int TestGold = 0;
        public static int TestServerMaxUser = 0;
        public static bool BoServiceMode = false;
        public static bool BoNonPKServer = false;
        public static bool BoViewHackCode = false;
        public static bool BoViewAdmissionfail = false;
        public static bool BoGetGetNeedNotice = false;
        public static long GetGetNoticeTime = 0;
        public static int UserFullCount = 0;
        public static int ZenFastStep = 0;
        public static bool BoSysHasMission = false;
        public static string SysMission_Map = string.Empty;
        public static int SysMission_X = 0;
        public static int SysMission_Y = 0;
        public static int TotalUserCount = 0;
        public static object csMsgLock = null;
        public static object csTimerLock = null;
        public static object csObjMsgLock = null;
        public static object csSendMsgLock = null;
        public static object csShare = null;
        public static object csDelShare = null;
        public static object csSocLock = null;
        public static object usLock = null;
        public static object usIMLock = null;
        public static object ruLock = null;
        public static object ruSendLock = null;
        public static object ruCloseLock = null;
        public static object socstrLock = null;
        public static object fuLock = null;
        public static object fuOpenLock = null;
        public static object fuCloseLock = null;
        public static object humanLock = null;
        public static object umLock = null;
        public static ArrayList MainMsg = null;
        public static ArrayList UserLogs = null;
        public static ArrayList UserConLogs = null;
        public static ArrayList UserChatLog = null;
        public static bool DiscountForNightTime = false;
        public static int HalfFeeStart = 0;
        public static int HalfFeeEnd = 0;
        public static bool ServerReady = false;
        public static bool ServerClosing = false;
        public static int FCertify = 0;
        public static int FItemNumber = 0;
        public static string RDBSocData = string.Empty;
        public static bool ReadyDBReceive = false;
        public static int RunFailCount = 0;
        public static int MirUserLoadCount = 0;
        public static int MirUserSaveCount = 0;
        public static long CurrentDBloadingTime = 0;
        public static bool BoEnableAbusiveFilter = false;
        public static int LottoSuccess = 0;
        public static int LottoFail = 0;
        public static int Lotto1 = 0;
        public static int Lotto2 = 0;
        public static int Lotto3 = 0;
        public static int Lotto4 = 0;
        public static int Lotto5 = 0;
        public static int Lotto6 = 0;
        public static string MsgServerAddress = string.Empty;
        public static int MsgServerPort = 0;
        public static string LogServerAddress = string.Empty;
        public static int LogServerPort = 0;
        public static string ShareBaseDir = string.Empty;
        public static string ShareBaseDirCopy = string.Empty;
        public static string ShareVentureDir = string.Empty;
        public static int ShareFileNameNum = 0;
        public static string ConLogBaseDir = string.Empty;
        public static string ChatLogBaseDir = string.Empty;
        public static string DefHomeMap = string.Empty;
        public static short DefHomeX = 0;
        public static short DefHomeY = 0;
        public static string GuildDir = string.Empty;
        public static string GuildFile = string.Empty;
        public static string GuildBaseDir = string.Empty;
        public static string GuildAgitFile = string.Empty;
        public static string CastleDir = string.Empty;
        public static string EnvirDir = string.Empty;
        public static string MapDir = string.Empty;
        public static int CurrentMonthlyCard = 0;
        public static int TotalTimeCardUsage = 0;
        public static int LastMonthTotalTimeCardUsage = 0;
        public static int GrossTimeCardUsage = 0;
        public static int GrossResetCount = 0;
        public static long serverruntime = 0;
        public static long runstart = 0;
        public static int rcount = 0;
        public static int minruncount = 0;
        public static int curruncount = 0;
        public static int maxsoctime = 0;
        public static int cursoctime = 0;
        public static int maxusrtime = 0;
        public static int curusrcount = 0;
        public static int curhumtime = 0;
        public static int maxhumtime = 0;
        public static int curmontime = 0;
        public static int maxmontime = 0;
        public static long humrotatetime = 0;
        public static int curhumrotatetime = 0;
        public static int maxhumrotatetime = 0;
        public static int humrotatecount = 0;
        public static string LatestGenStr;
        public static string LatestMonStr;
        public static long HumLimitTime = 0;
        public static long MonLimitTime = 0;
        public static long ZenLimitTime = 0;
        public static long NpcLimitTime = 0;
        public static long SocLimitTime = 0;
        public static long DecLimitTime = 0;
        public static string __ClothsForMan = string.Empty;
        public static string __ClothsForWoman = string.Empty;
        public static string __WoodenSword = string.Empty;
        public static string __Candle = string.Empty;
        public static string __BasicDrug = string.Empty;
        public static string __GoldStone = string.Empty;
        public static string __SilverStone = string.Empty;
        public static string __SteelStone = string.Empty;
        public static string __CopperStone = string.Empty;
        public static string __BlackStone = string.Empty;
        public static string __Gem1Stone = string.Empty;
        public static string __Gem2Stone = string.Empty;
        public static string __Gem3Stone = string.Empty;
        public static string __Gem4Stone = string.Empty;
        public static string __ZumaMonster1 = string.Empty;
        public static string __ZumaMonster2 = string.Empty;
        public static string __ZumaMonster3 = string.Empty;
        public static string __ZumaMonster4 = string.Empty;
        public static string __Bee = string.Empty;
        public static string __Spider = string.Empty;
        public static string __WhiteSkeleton = string.Empty;
        public static string __ShinSu = string.Empty;
        public static string __ShinSu1 = string.Empty;
        public static string __AngelMob = string.Empty;
        public static string __CloneMob = string.Empty;
        public static string __WomaHorn = string.Empty;
        public static string __ZumaPiece = string.Empty;
        public static string __GoldenImugi = string.Empty;
        public static string __WhiteSnake = string.Empty;
        public static string ClientFileName1 = string.Empty;
        public static string ClientFileName2 = string.Empty;
        public static string ClientFileName3 = string.Empty;
        public static int ClientCheckSumValue1 = 0;
        public static int ClientCheckSumValue2 = 0;
        public static int ClientCheckSumValue3 = 0;
        public static int gErrorCount = 0;
        public static int CurrentMerchantIndex = 0;
        public static long ServerTickDifference = 0;
        public static int INDEX_CHOCOLATE = 0;
        public static int INDEX_CANDY = 0;
        public static int INDEX_LOLLIPOP = 0;
        public static int INDEX_MIRBOOTS = 0;
        public const bool ENGLISHVERSION = true;
        public const bool PHILIPPINEVERSION = false;
        public const bool CHINAVERSION = false;
        public const bool TAIWANVERSION = false;
        public const bool KOREANVERSION = false;
        public const int SENDBLOCK = 1024;
        public const int SENDCHECKBLOCK = 4096;
        public const int SENDAVAILABLEBLOCK = 7999;
        public const int GATELOAD = 10;
        public const string LINENOTICEFILE = "Notice\\LineNotice.txt";
        public const string LINEHELPFILE = "LineHelp.txt";
        public const int BUILDGUILDFEE = 1000000;
        public static string g_sStartMarryManMsg = "[%n]: %s 与 %d 的婚礼现在开始..";
        public static string g_sStartMarryWoManMsg = "[%n]: %d 与 %s 的婚礼现在开始..";
        public static string g_sStartMarryManAskQuestionMsg = "[%n]: %s 你愿意娶 %d 小姐为妻，并照顾她一生一世吗？";
        public static string g_sStartMarryWoManAskQuestionMsg = "[%n]: %d 你愿意娶 %s 小姐为妻，并照顾她一生一世吗？";
        public static string g_sMarryManAnswerQuestionMsg = "[%s]: 我愿意，%d 小姐我会尽我一生的时间来照顾您，让您过上快乐美满的日子的";
        public static string g_sMarryManAskQuestionMsg = "[%n]: %d 你愿意嫁给 %s 先生为妻，并照顾他一生一世吗？";
        public static string g_sMarryWoManAnswerQuestionMsg = "[%s]: 我愿意，%d 先生我愿意让你来照顾我，保护我";
        public static string g_sMarryWoManGetMarryMsg = "[%n]: 我宣布 %d 先生与 %s 小姐正式成为合法夫妻";
        public static string g_sMarryWoManDenyMsg = "[%s]: %d 你这个好色之徒，谁会愿意嫁给你呀，癞蛤蟆想吃天鹅肉";
        public static string g_sMarryWoManCancelMsg = "[%n]: 真是可惜，二个人这个时候才翻脸，你们培养好感情后再来找我吧";
        public static string g_sfUnMarryManLoginMsg = "你的老婆%d已经强行与你脱离了夫妻关系了";
        public static string g_sfUnMarryWoManLoginMsg = "你的老公%d已经强行与你脱离了夫妻关系了";
        public static string g_sManLoginDearOnlineSelfMsg = "你的老婆%d当前位于%m(%x:%y)";
        public static string g_sManLoginDearOnlineDearMsg = "你的老公%s在:%m(%x:%y)上线了";
        public static string g_sWoManLoginDearOnlineSelfMsg = "你的老公当前位于%m(%x:%y)";
        public static string g_sWoManLoginDearOnlineDearMsg = "你的老婆%s在:%m(%x:%y) 上线了";
        public static string g_sManLoginDearNotOnlineMsg = "你的老婆现在不在线";
        public static string g_sWoManLoginDearNotOnlineMsg = "你的老公现在不在线";
        public static string g_sManLongOutDearOnlineMsg = "你的老公在:%m(%x:%y)下线了";
        public static string g_sWoManLongOutDearOnlineMsg = "你的老婆在:%m(%x:%y)下线了";
        public static string g_sYouAreNotMarryedMsg = "你都没结婚查什么？";
        public static string g_sYourWifeNotOnlineMsg = "你的老婆还没有上线";
        public static string g_sYourHusbandNotOnlineMsg = "你的老公还没有上线";
        public static string g_sYourWifeNowLocateMsg = "你的老婆现在位于:";
        public static string g_sYourHusbandSearchLocateMsg = "你的老公正在找你，他现在位于:";
        public static string g_sYourHusbandNowLocateMsg = "你的老公现在位于:";
        public static string g_sYourWifeSearchLocateMsg = "你的老婆正在找你，他现在位于:";
        public static string g_sfUnMasterLoginMsg = "你的一个徒弟已经背判师门了";
        public static string g_sfUnMasterListLoginMsg = "你的师父%d已经将你逐出师门了";
        public static string g_sMasterListOnlineSelfMsg = "你的师父%d当前位于%m(%x:%y)";
        public static string g_sMasterListOnlineMasterMsg = "你的徒弟%s在:%m(%x:%y)上线了";
        public static string g_sMasterOnlineSelfMsg = "你的徒弟当前位于%m(%x:%y)";
        public static string g_sMasterOnlineMasterListMsg = "你的师父%s在:%m(%x:%y) 上线了";
        public static string g_sMasterLongOutMasterListOnlineMsg = "你的师父在:%m(%x:%y)下线了";
        public static string g_sMasterListLongOutMasterOnlineMsg = "你的徒弟%s在:%m(%x:%y)下线了";
        public static string g_sMasterListNotOnlineMsg = "你的师父现不在线";
        public static string g_sMasterNotOnlineMsg = "你的徒弟现不在线";
        public static string g_sYouAreNotMasterMsg = "你都没师徒关系查什么？";
        public static string g_sYourMasterNotOnlineMsg = "你的师父还没有上线";
        public static string g_sYourMasterListNotOnlineMsg = "你的徒弟还没有上线";
        public static string g_sYourMasterNowLocateMsg = "你的师父现在位于:";
        public static string g_sYourMasterListSearchLocateMsg = "你的徒弟正在找你，他现在位于:";
        public static string g_sYourMasterListNowLocateMsg = "你的徒弟现在位于:";
        public static string g_sYourMasterSearchLocateMsg = "你的师父正在找你，他现在位于:";
        public static string g_sYourMasterListUnMasterOKMsg = "你的徒弟%d已经圆满出师了";
        public static string g_sYouAreUnMasterOKMsg = "你已经出师了";
        public static string g_sUnMasterLoginMsg = "你的一个徒弟已经圆满出师了";
        public static string g_sNPCSayUnMasterOKMsg = "[%n]: 我宣布%d与%s正式脱离师徒关系";
        public static string g_sNPCSayForceUnMasterMsg = "[%n]: 我宣布%s与%d已经正式脱离师徒关系";
        public static bool boSecondCardSystem = false;
        public static int g_nExpErienceLevel = 7;
        public static string BADMANHOMEMAP = "3";
        public static short BADMANSTARTX = 845;
        public static short BADMANSTARTY = 674;
        public static string RECHARGINGMAP = "kaiqu";
        public static bool boSafeZoneStall = false;
        public const int MAXKINGLEVEL = 61;
        public const int MAXLEVEL = 101;
        public static long[] NEEDEXPS = { 100, 200, 300, 400, 600, 900, 1200, 1700, 2500, 6000, 8000, 10000, 15000, 30000, 40000, 50000, 70000, 100000, 120000, 140000, 250000, 300000, 350000, 400000, 500000, 700000, 1000000, 1400000, 1800000, 2000000, 2400000, 2800000, 3200000, 3600000, 4000000, 4800000, 5600000, 8200000, 9000000, 12000000, 16000000, 30000000, 50000000, 80000000, 120000000, 160000000, 200000000, 250000000, 300000000, 350000000, 400000000, 480000000, 560000000, 640000000, 740000000, 840000000, 950000000, 1070000000, 1200000000, 1500000000, 1500000000, 1600000000, 1600000000, 1700000000, 1700000000, 1800000000, 1800000000, 1900000000, 1900000000, 2000000000, 2000000000, 2000000000, 2000000000, 2000000000, 2000000000, 2000000000, 2000000000, 2000000000, 2000000000, 2000000000, 2000000000, 2000000000, 2000000000, 2000000000, 2000000000, 2000000000, 2000000000, 2000000000, 2000000000, 2000000000, 2100000000, 2100000000, 2100000000, 2100000000, 2100000000, 2100000000, 2100000000, 2100000000, 2100000000, 2100000000, 2140000000 };
        public const int ADJ_LEVEL = 20;
        public static TNakedAbility WarriorBonus = new TNakedAbility() { DC = 17, MC = 20, SC = 20, AC = 20, MAC = 20, HP = 1, MP = 3, Hit = 20, Speed = 35 };
        public static TNakedAbility WizzardBonus = new TNakedAbility() { DC = 17, MC = 25, SC = 30, AC = 20, MAC = 15, HP = 2, MP = 1, Hit = 25, Speed = 35 };
        public static TNakedAbility PriestBonus = new TNakedAbility() { DC = 20, MC = 30, SC = 17, AC = 20, MAC = 15, HP = 2, MP = 1, Hit = 30, Speed = 30 };
        public static byte[,,] SpitMap = { { { 0, 0, 1, 0, 0 }, { 0, 0, 1, 0, 0 }, { 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0 } }, { { 0, 0, 0, 0, 1 }, { 0, 0, 0, 1, 0 }, { 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0 } }, { { 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0 }, { 0, 0, 0, 1, 1 }, { 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0 } }, { { 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0 }, { 0, 0, 0, 1, 0 }, { 0, 0, 0, 0, 1 } }, { { 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0 }, { 0, 0, 1, 0, 0 }, { 0, 0, 1, 0, 0 } }, { { 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0 }, { 0, 1, 0, 0, 0 }, { 1, 0, 0, 0, 0 } }, { { 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0 }, { 1, 1, 0, 0, 0 }, { 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0 } }, { { 1, 0, 0, 0, 0 }, { 0, 1, 0, 0, 0 }, { 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0 } } };
        public static byte[,,] CrossMap = { { { 0, 1, 1, 1, 0 }, { 0, 0, 1, 0, 0 }, { 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0 } }, { { 0, 0, 0, 1, 1 }, { 0, 0, 0, 1, 1 }, { 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0 } }, { { 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 1 }, { 0, 0, 0, 1, 1 }, { 0, 0, 0, 0, 1 }, { 0, 0, 0, 0, 0 } }, { { 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0 }, { 0, 0, 0, 1, 1 }, { 0, 0, 0, 1, 1 } }, { { 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0 }, { 0, 0, 1, 0, 0 }, { 0, 1, 1, 1, 0 } }, { { 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0 }, { 1, 1, 0, 0, 0 }, { 1, 1, 0, 0, 0 } }, { { 0, 0, 0, 0, 0 }, { 1, 0, 0, 0, 0 }, { 1, 1, 0, 0, 0 }, { 1, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0 } }, { { 1, 1, 0, 0, 0 }, { 1, 1, 0, 0, 0 }, { 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0 } } };

        public static string IntTo_Str(int val)
        {
            string result;
            if (val < 10)
            {
                result = "0" + val.ToString();
            }
            else
            {
                result = val.ToString();
            }
            return result;
        }

        public static int CheckFileCheckSum(string flname)
        {
            int result = 0;
            //NativeInt i;
            //NativeInt handle;
            //NativeInt bsize;
            //NativeInt cval;
            //NativeInt csum;
            //result = 0;
            //if (File.Exists(flname))
            //{
            //    handle = File.Open(flname, (FileMode)FileAccess.Read | FileShare.ReadWrite);
            //    if (handle > 0)
            //    {
            //        bsize = FileSeek(handle, 0, 2);
            //        GetMem(pbuf, (bsize + 3) / 4 * 4);
            //        FillChar(pbuf, (bsize + 3) / 4 * 4, 0);
            //        FileSeek(handle, 0, 0);
            //        FileRead(handle, pbuf, bsize);
            //        handle.Close();
            //        csum = 0;
            //        for (i = 0; i < (bsize + 3) / 4; i++)
            //        {
            //            cval = ((pbuf) as NativeInt);
            //            pbuf = (NativeInt(pbuf) + 4 as string);
            //            csum = csum ^ cval;
            //        }
            //        result = csum;
            //    }
            //}
            return result;
        }

        public static int GetCertifyNumber()
        {
            int result;
            FCertify++;
            if (FCertify > 0x7FFE)
            {
                FCertify = 1;
            }
            result = FCertify;
            return result;
        }

        public static int GetItemServerIndex()
        {
            int result;
            FItemNumber++;
            if (FItemNumber > 0x7FFFFFFE)
            {
                FItemNumber = 1;
            }
            result = FItemNumber;
            return result;
        }

        public static void LoadMultiServerTables()
        {
            //int i;
            //int k;
            //string str;
            //string snum;
            //string saddr;
            //string sport;
            //ArrayList strlist;
            //ArrayList slist;
            //for (i = 0; i < MultiServerList.Count; i++)
            //{
            //    ((MultiServerList[i]) as ArrayList).Free();
            //}
            //MultiServerList.Clear();
            //if (File.Exists("!servertable.txt"))
            //{
            //    strlist = new ArrayList();
            //    strlist.LoadFromFile("!servertable.txt");
            //    for (i = 0; i < strlist.Count; i++)
            //    {
            //        str = strlist[i].Trim();
            //        if (str != "")
            //        {
            //            if (str[1] == ";")
            //            {
            //                continue;
            //            }
            //            str  =  HUtil32.GetValidStr3(str, snum, new string[] { " ", "\09" });
            //            if (str != "")
            //            {
            //                slist = new ArrayList();
            //                for (k = 0; k <= 30; k++)
            //                {
            //                    if (str == "")
            //                    {
            //                        break;
            //                    }
            //                    str  =  HUtil32.GetValidStr3(str, saddr, new string[] { " ", "\09" });
            //                    str  =  HUtil32.GetValidStr3(str, sport, new string[] { " ", "\09" });
            //                    if ((saddr != "") && (sport != ""))
            //                    {
            //                        slist.Add(saddr, ((HUtil32.Str_ToInt(sport, 0)) as Object));
            //                    }
            //                }
            //                MultiServerList.Add(slist);
            //            }
            //        }
            //    }
            //}
            //else
            //{
            //    OutMainMessage("File not found... <!servertable.txt>");
            //}
        }

        public static bool LoadLineNotice(string flname)
        {
            bool result = false;
            if (File.Exists(flname))
            {
                //LineNoticeList.LoadFromFile(flname);
                //CheckListValid(LineNoticeList);
                result = true;
            }
            return result;
        }

        public static bool LoadLineHelp(string flname)
        {
            bool result = false;
            if (File.Exists(flname))
            {
                //LineHelpList.LoadFromFile(flname);
                //CheckListValid(LineHelpList);
                result = true;
            }
            return result;
        }

        public static bool GetMultiServerAddrPort(byte servernum, ref string addr, ref int port)
        {
            ArrayList slist;
            bool result = false;
            if (servernum < MultiServerList.Count)
            {
                slist = MultiServerList[servernum] as ArrayList;
                int n = new System.Random(slist.Count).Next();
                //addr = slist[n];
                //port = ((int)slist.Values[n]);
                result = true;
            }
            else
            {
                MainOutMessage("GetMultiServerAddrPort Fail..:" + servernum.ToString());
            }
            return result;
        }

        public static string GetUnbindItemName(int shape)
        {
            string result = "";
            for (var i = 0; i < UnbindItemList.Count; i++)
            {
                //if (((int)UnbindItemList.Values[i]) == shape)
                //{
                //    result = UnbindItemList[i];
                //    break;
                //}
            }
            return result;
        }

        public static void MainOutMessage(string str)
        {
            try
            {
                csMsgLock.Enter();
                MainMsg.Add(str);
            }
            finally
            {
                csMsgLock.Leave();
            }
        }

        public static void AddUserLog(string str)
        {
            try
            {
                csMsgLock.Enter();
                UserLogs.Add(str);
            }
            finally
            {
                csMsgLock.Leave();
            }
        }

        public static void AddConLog(string str)
        {
            try
            {
                csMsgLock.Enter();
                UserConLogs.Add(str);
            }
            finally
            {
                csMsgLock.Leave();
            }
        }

        public static void AddChatLog(string str)
        {
            try
            {
                csMsgLock.Enter();
                UserChatLog.Add(str);
            }
            finally
            {
                csMsgLock.Leave();
            }
        }

        public static void WriteConLogs(ArrayList slist)
        {
            //short ayear;
            //short amon;
            //short aday;
            //short ahour;
            //short amin;
            //short asec;
            //short amsec;
            //string dirname;
            //string flname;
            //char[] dir256 = new char[255 + 1];
            //System.IO.FileInfo f;
            //int i;
            //if (slist.Count == 0)
            //{
            //    return;
            //}
            //ayear = DateTime.Today.Year;
            //amon = DateTime.Today.Month;
            //aday = DateTime.Today.Day;
            //ahour = DateTime.Now.Hour;
            //amin = DateTime.Now.Minute;
            //asec = DateTime.Now.Second;
            //amsec = DateTime.Now.Millisecond;
            //dirname = ConLogBaseDir + (ayear).ToString() + "-" + IntTo_Str(amon) + "-" + IntTo_Str(aday);
            //if (!File.Exists(dirname))
            //{
            //    StrPCopy(dir256, dirname);
            //    CreateDirectory(dir256, null);
            //}
            //flname = dirname + "\\C-" + (ServerIndex).ToString() + "-" + IntTo_Str(ahour) + "H" + IntTo_Str(amin / 10 * 10) + "M.txt";
            //f = new FileInfo(flname);
            //if (!File.Exists(flname))
            //{
            //    _W_0 = f.CreateText();
            //}
            //else
            //{
            //    _W_0 = f.AppendText();
            //}
            //for (i = 0; i < slist.Count; i++)
            //{
            //    _W_0.WriteLine("1\09" + slist[i] + "\09" + "0");
            //}
            //_W_0.Close();
        }

        public static void WriteChatLogs(ArrayList slist)
        {
            //short ayear;
            //short amon;
            //short aday;
            //short ahour;
            //short amin;
            //short asec;
            //short amsec;
            //string dirname;
            //string flname;
            //char[] dir256 = new char[255 + 1];
            //System.IO.FileInfo f;
            //int i;
            //if (slist.Count == 0)
            //{
            //    return;
            //}
            //ayear = DateTime.Today.Year;
            //amon = DateTime.Today.Month;
            //aday = DateTime.Today.Day;
            //ahour = DateTime.Now.Hour;
            //amin = DateTime.Now.Minute;
            //asec = DateTime.Now.Second;
            //amsec = DateTime.Now.Millisecond;
            //dirname = ChatLogBaseDir + (ayear).ToString() + "-" + IntTo_Str(amon) + "-" + IntTo_Str(aday);
            //if (!File.Exists(dirname))
            //{
            //    StrPCopy(dir256, dirname);
            //    CreateDirectory(dir256, null);
            //}
            //flname = dirname + "\\C-" + IntTo_Str(ahour) + "H" + "M.txt";
            //f = new FileInfo(flname);
            //if (!File.Exists(flname))
            //{
            //    _W_0 = f.CreateText();
            //}
            //else
            //{
            //    _W_0 = f.AppendText();
            //}
            //for (i = 0; i < slist.Count; i++)
            //{
            //    _W_0.WriteLine((ServerIndex).ToString() + "\09" + slist[i] + "\09" + "0");
            //}
            //_W_0.Close();
        }

        public static bool DBConnected()
        {
            bool result = false;
            //if (FrmMain.DBSocket.Active)
            //{
            //    result = FrmMain.DBSocket.Socket.Connected;
            //}
            //else
            //{
            //    result = false;
            //}
            return result;
        }

        public int GetBonusPoint_adjlowlv(int lv)
        {
            int result;
            if (lv <= 25)
            {
                result = HUtil32.MathRound(26 - lv);
            }
            else
            {
                result = 0;
            }
            return result;
        }

        public static int GetBonusPoint(int job, int lv)
        {
            int result;
            result = 0;
            switch (job)
            {
                case 0:
                    if (lv >= ADJ_LEVEL + 1)
                    {
                        result = HUtil32.MathRound(20 + lv / 10 * 5);
                    }
                    else
                    {
                        result = 0;
                    }
                    break;
                case 1:
                    if (lv >= ADJ_LEVEL + 1)
                    {
                        result = HUtil32.MathRound(27 + lv / 10 * 8);
                    }
                    else
                    {
                        result = 0;
                    }
                    break;
                case 2:
                    if (lv >= ADJ_LEVEL + 1)
                    {
                        result = HUtil32.MathRound(28 + lv / 10 * 9);
                    }
                    else
                    {
                        result = 0;
                    }
                    break;
            }
            return result;
        }

        public static int GetLevelBonusSum(int job, int lv)
        {
            int result = 0;
            for (var i = 2; i <= lv; i++)
            {
                result = result + GetBonusPoint(job, i);
            }
            return result;
        }

        public static int GetBack(int dir)
        {
            int result;
            result = Grobal2.DR_UP;
            switch (dir)
            {
                case Grobal2.DR_UP:
                    result = Grobal2.DR_DOWN;
                    break;
                case Grobal2.DR_DOWN:
                    result = Grobal2.DR_UP;
                    break;
                case Grobal2.DR_LEFT:
                    result = Grobal2.DR_RIGHT;
                    break;
                case Grobal2.DR_RIGHT:
                    result = Grobal2.DR_LEFT;
                    break;
                case Grobal2.DR_UPLEFT:
                    result = Grobal2.DR_DOWNRIGHT;
                    break;
                case Grobal2.DR_UPRIGHT:
                    result = Grobal2.DR_DOWNLEFT;
                    break;
                case Grobal2.DR_DOWNLEFT:
                    result = Grobal2.DR_UPRIGHT;
                    break;
                case Grobal2.DR_DOWNRIGHT:
                    result = Grobal2.DR_UPLEFT;
                    break;
            }
            return result;
        }

        public static bool GetFrontPosition(TCreature cret, ref short newx, ref short newy)
        {
            bool result;
            TEnvirnoment penv;
            penv = cret.PEnvir;
            newx = cret.CX;
            newy = cret.CY;
            switch (cret.Dir)
            {
                case Grobal2.DR_UP:
                    if (newy > 0)
                    {
                        newy = (short)(newy - 1);
                    }
                    break;
                case Grobal2.DR_DOWN:
                    if (newy < penv.MapHeight - 1)
                    {
                        newy = (short)(newy + 1);
                    }
                    break;
                case Grobal2.DR_LEFT:
                    if (newx > 0)
                    {
                        newx = (short)(newx - 1);
                    }
                    break;
                case Grobal2.DR_RIGHT:
                    if (newx < penv.MapWidth - 1)
                    {
                        newx = (short)(newx + 1);
                    }
                    break;
                case Grobal2.DR_UPLEFT:
                    if ((newx > 0) && (newy > 0))
                    {
                        newx = (short)(newx - 1);
                        newy = (short)(newy - 1);
                    }
                    break;
                case Grobal2.DR_UPRIGHT:
                    if ((newx > 0) && (newy < penv.MapHeight - 1))
                    {
                        newx = (short)(newx + 1);
                        newy = (short)(newy - 1);
                    }
                    break;
                case Grobal2.DR_DOWNLEFT:
                    if ((newx < penv.MapWidth - 1) && (newy > 0))
                    {
                        newx = (short)(newx - 1);
                        newy = (short)(newy + 1);
                    }
                    break;
                case Grobal2.DR_DOWNRIGHT:
                    if ((newx < penv.MapWidth - 1) && (newy < penv.MapHeight - 1))
                    {
                        newx = (short)(newx + 1);
                        newy = (short)(newy + 1);
                    }
                    break;
            }
            result = true;
            return result;
        }

        public static bool GetBackPosition(TCreature cret, ref short newx, ref short newy)
        {
            bool result;
            TEnvirnoment penv;
            penv = cret.PEnvir;
            newx = cret.CX;
            newy = cret.CY;
            switch (cret.Dir)
            {
                case Grobal2.DR_UP:
                    if (newy < penv.MapHeight - 1)
                    {
                        newy = (short)(newy + 1);
                    }
                    break;
                case Grobal2.DR_DOWN:
                    if (newy > 0)
                    {
                        newy = (short)(newy - 1);
                    }
                    break;
                case Grobal2.DR_LEFT:
                    if (newx < penv.MapWidth - 1)
                    {
                        newx = (short)(newx + 1);
                    }
                    break;
                case Grobal2.DR_RIGHT:
                    if (newx > 0)
                    {
                        newx = (short)(newx - 1);
                    }
                    break;
                case Grobal2.DR_UPLEFT:
                    if ((newx < penv.MapWidth - 1) && (newy < penv.MapHeight - 1))
                    {
                        newx = (short)(newx + 1);
                        newy = (short)(newy + 1);
                    }
                    break;
                case Grobal2.DR_UPRIGHT:
                    if ((newx < penv.MapWidth - 1) && (newy > 0))
                    {
                        newx = (short)(newx - 1);
                        newy = (short)(newy + 1);
                    }
                    break;
                case Grobal2.DR_DOWNLEFT:
                    if ((newx > 0) && (newy < penv.MapHeight - 1))
                    {
                        newx = (short)(newx + 1);
                        newy = (short)(newy - 1);
                    }
                    break;
                case Grobal2.DR_DOWNRIGHT:
                    if ((newx > 0) && (newy > 0))
                    {
                        newx = (short)(newx - 1);
                        newy = (short)(newy - 1);
                    }
                    break;
            }
            result = true;
            return result;
        }

        public static bool GetNextPosition(TEnvirnoment penv, short sx, short sy, byte dir, int dis, ref short newx, ref short newy)
        {
            bool result;
            newx = sx;
            newy = sy;
            switch (dir)
            {
                case Grobal2.DR_UP:
                    if (newy > (dis - 1))
                    {
                        newy = (short)(newy - dis);
                    }
                    break;
                case Grobal2.DR_DOWN:
                    if (newy < penv.MapHeight - dis)
                    {
                        newy = (short)(newy + dis);
                    }
                    break;
                case Grobal2.DR_LEFT:
                    if (newx > (dis - 1))
                    {
                        newx = (short)(newx - dis);
                    }
                    break;
                case Grobal2.DR_RIGHT:
                    if (newx < penv.MapWidth - dis)
                    {
                        newx = (short)(newx + dis);
                    }
                    break;
                case Grobal2.DR_UPLEFT:
                    if ((newx > dis - 1) && (newy > dis - 1))
                    {
                        newx = (short)(newx - dis);
                        newy = (short)(newy - dis);
                    }
                    break;
                case Grobal2.DR_UPRIGHT:
                    if ((newx > dis - 1) && (newy < penv.MapHeight - dis))
                    {
                        newx = (short)(newx + dis);
                        newy = (short)(newy - dis);
                    }
                    break;
                case Grobal2.DR_DOWNLEFT:
                    if ((newx < penv.MapWidth - dis) && (newy > dis - 1))
                    {
                        newx = (short)(newx - dis);
                        newy = (short)(newy + dis);
                    }
                    break;
                case Grobal2.DR_DOWNRIGHT:
                    if ((newx < penv.MapWidth - dis) && (newy < penv.MapHeight - dis))
                    {
                        newx = (short)(newx + dis);
                        newy = (short)(newy + dis);
                    }
                    break;
            }
            if ((sx == newx) && (sy == newy))
            {
                result = false;
            }
            else
            {
                result = true;
            }
            return result;
        }

        public static byte GetNextDirection(short sx, short sy, short dx, short dy)
        {
            short flagx;
            short flagy;
            byte result = Grobal2.DR_DOWN;
            if (sx < dx)
            {
                flagx = 1;
            }
            else if (sx == dx)
            {
                flagx = 0;
            }
            else
            {
                flagx = -1;
            }
            if (Math.Abs(sy - dy) > 2)
            {
                if ((sx >= dx - 1) && (sx <= dx + 1))
                {
                    flagx = 0;
                }
            }
            if (sy < dy)
            {
                flagy = 1;
            }
            else if (sy == dy)
            {
                flagy = 0;
            }
            else
            {
                flagy = -1;
            }
            if (Math.Abs(sx - dx) > 2)
            {
                if ((sy > dy - 1) && (sy <= dy + 1))
                {
                    flagy = 0;
                }
            }
            if ((flagx == 0) && (flagy == -1))
            {
                result = Grobal2.DR_UP;
            }
            if ((flagx == 1) && (flagy == -1))
            {
                result = Grobal2.DR_UPRIGHT;
            }
            if ((flagx == 1) && (flagy == 0))
            {
                result = Grobal2.DR_RIGHT;
            }
            if ((flagx == 1) && (flagy == 1))
            {
                result = Grobal2.DR_DOWNRIGHT;
            }
            if ((flagx == 0) && (flagy == 1))
            {
                result = Grobal2.DR_DOWN;
            }
            if ((flagx == -1) && (flagy == 1))
            {
                result = Grobal2.DR_DOWNLEFT;
            }
            if ((flagx == -1) && (flagy == 0))
            {
                result = Grobal2.DR_LEFT;
            }
            if ((flagx == -1) && (flagy == -1))
            {
                result = Grobal2.DR_UPLEFT;
            }
            return result;
        }

        public static byte GetNextDirectionNew(int sx, int sy, int dx, int dy)
        {
            byte result;
            int flagx;
            int flagy;
            result = Grobal2.DR_DOWN;
            if (sx < dx)
            {
                flagx = 1;
            }
            else if (sx == dx)
            {
                flagx = 0;
            }
            else
            {
                flagx = -1;
            }
            if (Math.Abs(sy - dy) > 2)
            {
                if ((sx >= dx - 1) && (sx <= dx + 1))
                {
                    flagx = 0;
                }
            }
            if (sy < dy)
            {
                flagy = 1;
            }
            else if (sy == dy)
            {
                flagy = 0;
            }
            else
            {
                flagy = -1;
            }
            if (Math.Abs(sx - dx) > 2)
            {
                if ((sy >= dy - 1) && (sy <= dy + 1))
                {
                    flagy = 0;
                }
            }
            if ((flagx == 0) && (flagy == -1))
            {
                result = Grobal2.DR_UP;
            }
            if ((flagx == 1) && (flagy == -1))
            {
                result = Grobal2.DR_UPRIGHT;
            }
            if ((flagx == 1) && (flagy == 0))
            {
                result = Grobal2.DR_RIGHT;
            }
            if ((flagx == 1) && (flagy == 1))
            {
                result = Grobal2.DR_DOWNRIGHT;
            }
            if ((flagx == 0) && (flagy == 1))
            {
                result = Grobal2.DR_DOWN;
            }
            if ((flagx == -1) && (flagy == 1))
            {
                result = Grobal2.DR_DOWNLEFT;
            }
            if ((flagx == -1) && (flagy == 0))
            {
                result = Grobal2.DR_LEFT;
            }
            if ((flagx == -1) && (flagy == -1))
            {
                result = Grobal2.DR_UPLEFT;
            }
            return result;
        }

        public static short GetHpMpRate(TCreature cret)
        {
            short result;
            byte hrate;
            byte srate;
            if ((cret.Abil.MaxHP != 0) && (cret.Abil.MaxMP != 0))
            {
                result = HUtil32.MakeWord(HUtil32.MathRound(cret.Abil.HP / cret.Abil.MaxHP * 100), HUtil32.MathRound(cret.Abil.MP / cret.Abil.MaxMP * 100));
            }
            else
            {
                if (cret.Abil.MaxHP == 0)
                {
                    hrate = 0;
                }
                else
                {
                    hrate = (byte)HUtil32.MathRound(cret.Abil.HP / cret.Abil.MaxHP * 100);
                }
                if (cret.Abil.MaxMP == 0)
                {
                    srate = 0;
                }
                else
                {
                    srate = (byte)HUtil32.MathRound(cret.Abil.MP / cret.Abil.MaxMP * 100);
                }
                result = HUtil32.MakeWord(hrate, srate);
            }
            return result;
        }

        public static bool IsTakeOnAvailable(int useindex, TStdItem pstd)
        {
            bool result = false;
            if (pstd == null)
            {
                return result;
            }
            switch (useindex)
            {
                case Grobal2.U_DRESS:
                    if (pstd.StdMode >= 10 && pstd.StdMode <= 11)
                    {
                        result = true;
                    }
                    break;
                case Grobal2.U_WEAPON:
                    if ((pstd.StdMode == 5) || (pstd.StdMode == 6))
                    {
                        result = true;
                    }
                    break;
                case Grobal2.U_RIGHTHAND:
                    if (pstd.StdMode == 30)
                    {
                        result = true;
                    }
                    break;
                case Grobal2.U_NECKLACE:
                    if ((pstd.StdMode == 19) || (pstd.StdMode == 20) || (pstd.StdMode == 21))
                    {
                        result = true;
                    }
                    break;
                case Grobal2.U_HELMET:
                    if (pstd.StdMode == 15)
                    {
                        result = true;
                    }
                    break;
                case Grobal2.U_RINGL:
                case Grobal2.U_RINGR:
                    if ((pstd.StdMode == 22) || (pstd.StdMode == 23))
                    {
                        result = true;
                    }
                    break;
                case Grobal2.U_ARMRINGR:
                    if ((pstd.StdMode == 24) || (pstd.StdMode == 26))
                    {
                        result = true;
                    }
                    break;
                case Grobal2.U_ARMRINGL:
                    if ((pstd.StdMode == 24) || (pstd.StdMode == 25) || (pstd.StdMode == 26))
                    {
                        result = true;
                    }
                    break;
                case Grobal2.U_BUJUK:
                    if (pstd.StdMode == 25)
                    {
                        result = true;
                    }
                    break;
                case Grobal2.U_BELT:
                    if (pstd.StdMode == 54)
                    {
                        result = true;
                    }
                    break;
                case Grobal2.U_BOOTS:
                    if (pstd.StdMode == 52)
                    {
                        result = true;
                    }
                    break;
                case Grobal2.U_CHARM:
                    if (pstd.StdMode == 53)
                    {
                        result = true;
                    }
                    break;
            }
            return result;
        }

        public static bool IsDCItem(int uindex)
        {
            bool result;
            TStdItem pstd;
            pstd = M2Share.UserEngine.GetStdItem(uindex);
            if (new ArrayList(new int[] { 5, 6, 10, 11, 15, 19, 20, 21, 22, 23, 24, 26, 52, 53, 54 }).Contains(pstd.StdMode))
            {
                result = true;
            }
            else
            {
                result = false;
            }
            return result;
        }

        public static bool IsUpgradeWeaponStuff(int uindex)
        {
            TStdItem pstd = M2Share.UserEngine.GetStdItem(uindex);
            bool result;
            if (new ArrayList(new int[] { 19, 20, 21, 22, 23, 24, 26 }).Contains(pstd.StdMode))
            {
                result = true;
            }
            else
            {
                result = false;
            }
            return result;
        }

        public static List<string> GetMakeItemCondition(string itemname, ref int iPrice)
        {
            string sMakeItemName = string.Empty;
            string sMakeItemPrice = string.Empty;
            List<string> result = null;
            for (var i = 0; i < M2Share.MakeItemList.Count; i++)
            {
                sMakeItemPrice = HUtil32.GetValidStr3(M2Share.MakeItemList[i], ref sMakeItemName, new string[] { ":" });
                if (sMakeItemName == itemname)
                {
                    //result = svMain.MakeItemList[i];
                    iPrice = HUtil32.Str_ToInt(sMakeItemPrice, 0);
                    break;
                }
            }
            return result;
        }

        public static byte GetTurnDir(int dir, int rotatecount)
        {
            return (byte)((dir + rotatecount) % 8);
        }

        public static bool IsCheapStuff(int stdmode)
        {
            bool result;
            if (stdmode >= 0 && stdmode <= 2)
            {
                result = true;
            }
            else
            {
                result = false;
            }
            return result;
        }

        public static string GetGoldStr(int gold)
        {
            string result;
            int i;
            int n;
            string str;
            str = gold.ToString();
            n = 0;
            result = "";
            for (i = str.Length; i >= 1; i--)
            {
                if (n == 3)
                {
                    result = str[i] + "," + result;
                    n = 1;
                }
                else
                {
                    result = str[i] + result;
                    n++;
                }
            }
            return result;
        }

        public static string GetStrGoldStr(string strgold)
        {
            int n = 0;
            string result = "";
            for (var i = strgold.Length; i >= 1; i--)
            {
                if (n == 3)
                {
                    result = strgold[i] + "," + result;
                    n = 1;
                }
                else
                {
                    result = strgold[i] + result;
                    n++;
                }
            }
            return result;
        }

        public static void LoadConfig()
        {
            //FileStream Conf;
            //string sConfigFileName;
            //int nInteger;
            //string sString;
            //sConfigFileName = ".\\Config.ini";
            //Conf = new FileStream(sConfigFileName);
            //nInteger = Conf.ReadInteger(GateClass, "WhisperRecord", -1);// 游戏私聊
            //if (nInteger == -1)
            //{
            //    Conf.WriteBool(GateClass, "WhisperRecord", g_GameConfig.boWhisperRecord);
            //}
            //nInteger = Conf.ReadInteger(GateClass, "NoFog", -1);
            //if (nInteger == -1)
            //{
            //    Conf.WriteBool(GateClass, "NoFog", g_GameConfig.boNoFog);
            //}
            //nInteger = Conf.ReadInteger(GateClass, "StallSystem", -1);
            //if (nInteger == -1)
            //{
            //    Conf.WriteBool(GateClass, "StallSystem", g_GameConfig.boStallSystem);
            //}
            //nInteger = Conf.ReadInteger(GateClass, "SafeZoneStall", -1);
            //if (nInteger == -1)
            //{
            //    Conf.WriteBool(GateClass, "SafeZoneStall", boSafeZoneStall);
            //}
            //nInteger = Conf.ReadInteger(GateClass, "ShowHpBar", -1);
            //if (nInteger == -1)
            //{
            //    Conf.WriteBool(GateClass, "ShowHpBar", g_GameConfig.boShowHpBar);
            //}
            //nInteger = Conf.ReadInteger(GateClass, "ShowHpNumber", -1);
            //if (nInteger == -1)
            //{
            //    Conf.WriteBool(GateClass, "ShowHpNumber", g_GameConfig.boShowHpNumber);
            //}
            //nInteger = Conf.ReadInteger(GateClass, "NoStruck", -1);
            //if (nInteger == -1)
            //{
            //    Conf.WriteBool(GateClass, "NoStruck", g_GameConfig.boNoStruck);
            //}
            //nInteger = Conf.ReadInteger(GateClass, "FastMove", -1);
            //if (nInteger == -1)
            //{
            //    Conf.WriteBool(GateClass, "FastMove", g_GameConfig.boFastMove);
            //}
            //nInteger = Conf.ReadInteger(GateClass, "ShowFriend", -1);
            //if (nInteger == -1)
            //{
            //    Conf.WriteBool(GateClass, "ShowFriend", g_GameConfig.boShowFriend);
            //}
            //nInteger = Conf.ReadInteger(GateClass, "ShowRelationship", -1);
            //if (nInteger == -1)
            //{
            //    Conf.WriteBool(GateClass, "ShowRelationship", g_GameConfig.boShowRelationship);
            //}
            //nInteger = Conf.ReadInteger(GateClass, "ShowMail", -1);
            //if (nInteger == -1)
            //{
            //    Conf.WriteBool(GateClass, "ShowMail", g_GameConfig.boShowMail);
            //}
            //nInteger = Conf.ReadInteger(GateClass, "ShowRecharging", -1);
            //if (nInteger == -1)
            //{
            //    Conf.WriteBool(GateClass, "ShowRecharging", g_GameConfig.boShowRecharging);
            //}
            //nInteger = Conf.ReadInteger(GateClass, "ShowHelp", -1);
            //if (nInteger == -1)
            //{
            //    Conf.WriteBool(GateClass, "ShowHelp", g_GameConfig.boShowHelp);
            //}
            //nInteger = Conf.ReadInteger(GateClass, "SecondCardSystem", -1);
            //if (nInteger == -1)
            //{
            //    Conf.WriteBool(GateClass, "SecondCardSystem", boSecondCardSystem);
            //}
            //nInteger = Conf.ReadInteger(GateClass, "ExpErienceLevel", -1);
            //if (nInteger == -1)
            //{
            //    Conf.WriteInteger(GateClass, "ExpErienceLevel", g_nExpErienceLevel);
            //}
            //sString = Conf.ReadString(GateClass, "BadManHomeMap", "");
            //if (sString == "")
            //{
            //    Conf.WriteString(GateClass, "BadManHomeMap", "3");
            //}
            //nInteger = Conf.ReadInteger(GateClass, "BadManStartX", -1);
            //if (nInteger == -1)
            //{
            //    Conf.WriteInteger(GateClass, "BadManStartX", BADMANSTARTX);
            //}
            //nInteger = Conf.ReadInteger(GateClass, "BadManStartY", -1);
            //if (nInteger == -1)
            //{
            //    Conf.WriteInteger(GateClass, "BadManStartY", BADMANSTARTY);
            //}
            //sString = Conf.ReadString(GateClass, "RECHARGINGMAP", "");
            //// 充值地图
            //if (sString == "")
            //{
            //    Conf.WriteString(GateClass, "RECHARGINGMAP", "kaiqu");
            //}
            //g_GameConfig.boWhisperRecord = Conf.ReadBool(GateClass, "WhisperRecord", g_GameConfig.boWhisperRecord);
            //// 游戏私聊
            //g_GameConfig.boNoFog = Conf.ReadBool(GateClass, "NoFog", g_GameConfig.boNoFog);
            //g_GameConfig.boStallSystem = Conf.ReadBool(GateClass, "StallSystem", g_GameConfig.boStallSystem);
            //boSafeZoneStall = Conf.ReadBool(GateClass, "SafeZoneStall", boSafeZoneStall);
            //g_GameConfig.boShowHpBar = Conf.ReadBool(GateClass, "ShowHpBar", g_GameConfig.boShowHpBar);
            //g_GameConfig.boShowHpNumber = Conf.ReadBool(GateClass, "ShowHpNumber", g_GameConfig.boShowHpNumber);
            //g_GameConfig.boNoStruck = Conf.ReadBool(GateClass, "NoStruck", g_GameConfig.boNoStruck);
            //g_GameConfig.boFastMove = Conf.ReadBool(GateClass, "FastMove", g_GameConfig.boFastMove);
            //g_GameConfig.boShowFriend = Conf.ReadBool(GateClass, "ShowFriend", g_GameConfig.boShowFriend);
            //g_GameConfig.boShowRelationship = Conf.ReadBool(GateClass, "ShowRelationship", g_GameConfig.boShowRelationship);
            //g_GameConfig.boShowMail = Conf.ReadBool(GateClass, "ShowMail", g_GameConfig.boShowMail);
            //g_GameConfig.boShowRecharging = Conf.ReadBool(GateClass, "ShowRecharging", g_GameConfig.boShowRecharging);
            //g_GameConfig.boShowHelp = Conf.ReadBool(GateClass, "ShowHelp", g_GameConfig.boShowHelp);
            //boSecondCardSystem = Conf.ReadBool(GateClass, "SecondCardSystem", boSecondCardSystem);
            //g_nExpErienceLevel = Conf.ReadInteger(GateClass, "ExpErienceLevel", g_nExpErienceLevel);
            //BADMANHOMEMAP = Conf.ReadString(GateClass, "BadManHomeMap", BADMANHOMEMAP);
            //BADMANSTARTX = Conf.ReadInteger(GateClass, "BadManStartX", BADMANSTARTX);
            //BADMANSTARTY = Conf.ReadInteger(GateClass, "BadManStartY", BADMANSTARTY);
            //g_GameConfig.boGamepath = Conf.ReadBool(GateClass, "Gamepath", g_GameConfig.boGamepath);
            //RECHARGINGMAP = Conf.ReadString(GateClass, "RECHARGINGMAP", RECHARGINGMAP);// 充值
            //Conf.Free();
        }

        public static void SaveConfig()
        {
            //FileStream Conf;
            //string sConfigFileName;
            //sConfigFileName = ".\\Config.ini";
            //Conf = new FileStream(sConfigFileName);
            //Conf.WriteBool(GateClass, "WhisperRecord", g_GameConfig.boWhisperRecord);
            //// 游戏私聊
            //Conf.WriteBool(GateClass, "NoFog", g_GameConfig.boNoFog);
            //Conf.WriteBool(GateClass, "StallSystem", g_GameConfig.boStallSystem);
            //Conf.WriteBool(GateClass, "SafeZoneStall", boSafeZoneStall);
            //Conf.WriteBool(GateClass, "ShowHpBar", g_GameConfig.boShowHpBar);
            //Conf.WriteBool(GateClass, "ShowHpNumber", g_GameConfig.boShowHpNumber);
            //Conf.WriteBool(GateClass, "NoStruck", g_GameConfig.boNoStruck);
            //Conf.WriteBool(GateClass, "FastMove", g_GameConfig.boFastMove);
            //Conf.WriteBool(GateClass, "ShowFriend", g_GameConfig.boShowFriend);
            //Conf.WriteBool(GateClass, "ShowRelationship", g_GameConfig.boShowRelationship);
            //Conf.WriteBool(GateClass, "ShowMail", g_GameConfig.boShowMail);
            //Conf.WriteBool(GateClass, "ShowRecharging", g_GameConfig.boShowRecharging);
            //Conf.WriteBool(GateClass, "ShowHelp", g_GameConfig.boShowHelp);
            //Conf.WriteBool(GateClass, "SecondCardSystem", boSecondCardSystem);
            //Conf.WriteInteger(GateClass, "ExpErienceLevel", g_nExpErienceLevel);
            //Conf.WriteString(GateClass, "BadManHomeMap", BADMANHOMEMAP);
            //Conf.WriteInteger(GateClass, "BadManStartX", BADMANSTARTX);
            //Conf.WriteInteger(GateClass, "BadManStartY", BADMANSTARTY);
            //Conf.WriteBool(GateClass, "Gamepath", g_GameConfig.boGamepath);
            //Conf.WriteString(GateClass, "RECHARGINGMAP", RECHARGINGMAP);// 充值地图
            //Conf.Free();
        }
    }
}
