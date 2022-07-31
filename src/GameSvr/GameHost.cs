using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using SystemModule;

namespace GameSvr
{
    public class GameHost
    {
        public void FormCreate()
        {
            string fname;
            System.IO.FileInfo fhandle;
            M2Share.gErrorCount = 0;
            M2Share.ServerIndex = 0;
            M2Share.RunSocket = new TRunSocket();
            M2Share.MainMsg = new ArrayList();
            M2Share.UserLogs = new ArrayList();
            M2Share.UserConLogs = new ArrayList();
            M2Share.UserChatLog = new ArrayList();
            M2Share.GrobalEnvir = new TEnvirList();
            M2Share.ItemMan = new ItemUnitSystem();
            M2Share.MagicMan = new TMagicManager();
            M2Share.NoticeMan = new TNoticeManager();
            M2Share.GuildMan = new TGuildManager();
            M2Share.GuildAgitMan = new TGuildAgitManager();
            M2Share.GuildAgitBoardMan = new TGuildAgitBoardManager();
            M2Share.EventMan = new TEventManager();
            M2Share.UserCastle = new TUserCastle();
            M2Share.boUserCastleInitialized = false;
            M2Share.gFireDragon = new TDragonSystem();
            M2Share.FrontEngine = new TFrontEngine();
            M2Share.UserEngine = new TUserEngine();
            M2Share.UserMgrEngine = new TUserMgrEngine();
            DBSQL.g_DBSQL = new TDBSql();
            SqlEngn.SqlEngine = new TSQLEngine();
            M2Share.DecoItemList = new ArrayList();
            M2Share.MakeItemList = new List<string>();
            M2Share.MakeItemIndexList = new ArrayList();
            M2Share.StartPoints = new List<TSafePoint>();
            M2Share.SafePoints = new List<TSafePoint>();
            M2Share.MultiServerList = new ArrayList();
            M2Share.ShutUpList = new ArrayList();
            M2Share.MiniMapList = new ArrayList();
            M2Share.UnbindItemList = new List<string>();
            M2Share.LineNoticeList = new ArrayList();
            M2Share.LineHelpList = new ArrayList();
            M2Share.QuestDiaryList = new ArrayList();
            M2Share.DefaultNpc = null;
            M2Share.DropItemNoticeList = new ArrayList();
            M2Share.ShopItemList = new List<TShopItem>();
            M2Share.EventItemList = new ArrayList();
            M2Share.EventItemGifeBaseNumber = 0;
            M2Share.csMsgLock = new object();
            M2Share.csTimerLock = new object();
            M2Share.csObjMsgLock = new object();
            M2Share.csSendMsgLock = new object();
            M2Share.csShare = new object();
            M2Share.csDelShare = new object();
            M2Share.csSocLock = new object();
            M2Share.usLock = new object();
            M2Share.usIMLock = new object();
            M2Share.ruLock = new object();
            M2Share.ruSendLock = new object();
            M2Share.ruCloseLock = new object();
            M2Share.socstrLock = new object();
            M2Share.fuLock = new object();
            M2Share.fuOpenLock = new object();
            M2Share.fuCloseLock = new object();
            M2Share.humanLock = new object();
            M2Share.umLock = new object();
            M2Share.RDBSocData = "";
            M2Share.ReadyDBReceive = false;
            M2Share.RunFailCount = 0;
            M2Share.MirUserLoadCount = 0;
            M2Share.MirUserSaveCount = 0;
            M2Share.BoGetGetNeedNotice = false;
            M2Share.FCertify = 0;
            M2Share.FItemNumber = 0;
            M2Share.ServerReady = false;
            M2Share.ServerClosing = false;
            M2Share.BoEnableAbusiveFilter = true;
            M2Share.LottoSuccess = 0;
            M2Share.LottoFail = 0;
            M2Share.Lotto1 = 0;
            M2Share.Lotto2 = 0;
            M2Share.Lotto3 = 0;
            M2Share.Lotto4 = 0;
            M2Share.Lotto5 = 0;
            M2Share.Lotto6 = 0;
            M2Share.CurrentMerchantIndex = 0;
            M2Share.ServerTickDifference = 0;
            //FillChar(svMain.GrobalQuestParams, sizeof(svMain.GrobalQuestParams), '\0');
            //ayear = DateTime.Today.Year;
            //amon = DateTime.Today.Month;
            //aday = DateTime.Today.Day;
            //ahour = DateTime.Now.Hour;
            //amin = DateTime.Now.Minute;
            //asec = DateTime.Now.Second;
            //amsec = DateTime.Now.Millisecond;
            //svMain.ErrorLogFile = ".\\Log\\" + (ayear).ToString() + "-" + (amon).ToString() + "-" + svMain.IntTo_Str(aday) + "." + svMain.IntTo_Str(ahour) + "-" + svMain.IntTo_Str(amin) + ".log";
            fhandle = new FileInfo(M2Share.ErrorLogFile);
            StreamWriter _W_1 = fhandle.CreateText();
            _W_1.Close();
            M2Share.minruncount = 99999;
            M2Share.maxsoctime = 0;
            M2Share.maxusrtime = 0;
            M2Share.maxhumtime = 0;
            M2Share.maxmontime = 0;
            M2Share.curhumrotatetime = 0;
            M2Share.maxhumrotatetime = 0;
            M2Share.humrotatecount = 0;
            M2Share.HumLimitTime = 30;
            M2Share.MonLimitTime = 30;
            M2Share.ZenLimitTime = 5;
            M2Share.NpcLimitTime = 5;
            M2Share.SocLimitTime = 10;
            M2Share.DecLimitTime = 20;
            OutMainMessage("ready to load ini file..");
            fname = ".\\!setup.txt";
            if (File.Exists(fname))
            {
                //ini = new FileStream(fname);
                //if (ini != null)
                //{
                //    svMain.ServerIndex = ini.ReadInteger("Server", "ServerIndex", 0);
                //    svMain.ServerName = ini.ReadString("Server", "ServerName", "");
                //    svMain.ServerNumber = ini.ReadInteger("Server", "ServerNumber", 0);
                //    str = ini.ReadString("Server", "VentureServer", "FALSE");
                //    svMain.BoVentureServer = ((str).ToLower().CompareTo(("TRUE").ToLower()) == 0);
                //    str = ini.ReadString("Server", "TestServer", "FALSE");
                //    svMain.BoTestServer = ((str).ToLower().CompareTo(("TRUE").ToLower()) == 0);
                //    // 努扼捞攫飘 抛胶飘侩
                //    str = ini.ReadString("Server", "ClientTest", "FALSE");
                //    svMain.BoClientTest = ((str).ToLower().CompareTo(("TRUE").ToLower()) == 0);
                //    svMain.TestLevel = ini.ReadInteger("Server", "TestLevel", 1);
                //    svMain.TestGold = ini.ReadInteger("Server", "TestGold", 0);
                //    svMain.TestServerMaxUser = ini.ReadInteger("Server", "TestServerUserLimit", 5000);
                //    str = ini.ReadString("Server", "ServiceMode", "FALSE");
                //    svMain.BoServiceMode = ((str).ToLower().CompareTo(("TRUE").ToLower()) == 0);
                //    str = ini.ReadString("Server", "NonPKServer", "FALSE");
                //    svMain.BoNonPKServer = ((str).ToLower().CompareTo(("TRUE").ToLower()) == 0);
                //    str = ini.ReadString("Server", "ViewHackMessage", "TRUE");
                //    svMain.BoViewHackCode = ((str).ToLower().CompareTo(("TRUE").ToLower()) == 0);
                //    str = ini.ReadString("Server", "ViewAdmissionFailure", "FALSE");
                //    svMain.BoViewAdmissionfail = ((str).ToLower().CompareTo(("TRUE").ToLower()) == 0);
                //    svMain.DefHomeMap = ini.ReadString("Server", "HomeMap", "0");
                //    svMain.DefHomeX = ini.ReadInteger("Server", "HomeX", 289);
                //    svMain.DefHomeY = ini.ReadInteger("Server", "HomeY", 618);
                //    DBSocket.Address = ini.ReadString("Server", "DBAddr", "210.121.143.205");
                //    DBSocket.Port = ini.ReadInteger("Server", "DBPort", 6000);
                //    this.Active = true;
                //    svMain.FItemNumber = ini.ReadInteger("Setup", "ItemNumber", 0);
                //    svMain.HumLimitTime = ini.ReadInteger("Server", "HumLimit", svMain.HumLimitTime);
                //    svMain.MonLimitTime = ini.ReadInteger("Server", "MonLimit", svMain.MonLimitTime);
                //    svMain.ZenLimitTime = ini.ReadInteger("Server", "ZenLimit", svMain.ZenLimitTime);
                //    svMain.NpcLimitTime = ini.ReadInteger("Server", "NpcLimit", svMain.NpcLimitTime);
                //    svMain.SocLimitTime = ini.ReadInteger("Server", "SocLimit", svMain.SocLimitTime);
                //    svMain.DecLimitTime = ini.ReadInteger("Server", "DecLimit", svMain.DecLimitTime);
                //    svMain.SENDBLOCK = ini.ReadInteger("Server", "SendBlock", svMain.SENDBLOCK);
                //    svMain.SENDCHECKBLOCK = ini.ReadInteger("Server", "CheckBlock", svMain.SENDCHECKBLOCK);
                //    svMain.SENDAVAILABLEBLOCK = ini.ReadInteger("Server", "AvailableBlock", svMain.SENDAVAILABLEBLOCK);
                //    svMain.GATELOAD = ini.ReadInteger("Server", "GateLoad", svMain.GATELOAD);
                //    svMain.UserFullCount = ini.ReadInteger("Server", "UserFull", 500);
                //    svMain.ZenFastStep = ini.ReadInteger("Server", "ZenFastStep", 300);
                //    svMain.MsgServerAddress = ini.ReadString("Server", "MsgSrvAddr", "210.121.143.205");
                //    svMain.MsgServerPort = ini.ReadInteger("Server", "MsgSrvPort", 4900);
                //    svMain.LogServerAddress = ini.ReadString("Server", "LogServerAddr", "192.168.0.152");
                //    svMain.LogServerPort = ini.ReadInteger("Server", "LogServerPort", 10000);
                //    svMain.DiscountForNightTime = ini.ReadBool("Server", "DiscountForNightTime", false);
                //    svMain.HalfFeeStart = ini.ReadInteger("Server", "HalfFeeStart", 2);
                //    svMain.HalfFeeEnd = ini.ReadInteger("Server", "HalfFeeEnd", 10);
                //    svMain.ShareBaseDir = ini.ReadString("Share", "BaseDir", "D:\\");
                //    svMain.ShareBaseDirCopy = svMain.ShareBaseDir;
                //    svMain.ShareFileNameNum = 1;
                //    svMain.GuildDir = ini.ReadString("Share", "GuildDir", "D:\\");
                //    svMain.GuildFile = ini.ReadString("Share", "GuildFile", "D:\\");
                //    svMain.GuildBaseDir = Path.GetDirectoryName(svMain.GuildFile) + "\\";
                //    svMain.GuildAgitFile = svMain.GuildBaseDir + "GuildAgitList.txt";
                //    svMain.ShareVentureDir = ini.ReadString("Share", "VentureDir", "D:\\");
                //    svMain.ConLogBaseDir = ini.ReadString("Share", "ConLogDir", "D:\\");
                //    svMain.ChatLogBaseDir = ini.ReadString("Share", "ChatLogDir", "D:\\");
                //    svMain.CastleDir = ini.ReadString("Share", "CastleDir", "D:\\");
                //    svMain.EnvirDir = ini.ReadString("Share", "EnvirDir", ".\\Envir\\");
                //    svMain.MapDir = ini.ReadString("Share", "MapDir", ".\\Map\\");
                //    svMain.ClientFileName1 = ini.ReadString("Setup", "ClientFile1", "");
                //    svMain.ClientFileName2 = ini.ReadString("Setup", "ClientFile2", "");
                //    svMain.ClientFileName3 = ini.ReadString("Setup", "ClientFile3", "");
                //    svMain.__ClothsForMan = ini.ReadString("Names", "ClothsMan", "");
                //    svMain.__ClothsForWoman = ini.ReadString("Names", "ClothsWoman", "");
                //    svMain.__WoodenSword = ini.ReadString("Names", "WoodenSword", "");
                //    svMain.__Candle = ini.ReadString("Names", "Candle", "");
                //    svMain.__BasicDrug = ini.ReadString("Names", "BasicDrug", "");
                //    svMain.__GoldStone = ini.ReadString("Names", "GoldStone", "");
                //    svMain.__SilverStone = ini.ReadString("Names", "SilverStone", "");
                //    svMain.__SteelStone = ini.ReadString("Names", "SteelStone", "");
                //    svMain.__CopperStone = ini.ReadString("Names", "CopperStone", "");
                //    svMain.__BlackStone = ini.ReadString("Names", "BlackStone", "");
                //    svMain.__Gem1Stone = ini.ReadString("Names", "Gem1Stone", "");
                //    svMain.__Gem2Stone = ini.ReadString("Names", "Gem2Stone", "");
                //    svMain.__Gem3Stone = ini.ReadString("Names", "Gem3Stone", "");
                //    svMain.__Gem4Stone = ini.ReadString("Names", "Gem4Stone", "");
                //    svMain.__ZumaMonster1 = ini.ReadString("Names", "Zuma1", "");
                //    svMain.__ZumaMonster2 = ini.ReadString("Names", "Zuma2", "");
                //    svMain.__ZumaMonster3 = ini.ReadString("Names", "Zuma3", "");
                //    svMain.__ZumaMonster4 = ini.ReadString("Names", "Zuma4", "");
                //    svMain.__Bee = ini.ReadString("Names", "Bee", "");
                //    svMain.__Spider = ini.ReadString("Names", "Spider", "");
                //    svMain.__WhiteSkeleton = ini.ReadString("Names", "Skeleton", "");
                //    svMain.__ShinSu = ini.ReadString("Names", "Dragon", "");
                //    svMain.__ShinSu1 = ini.ReadString("Names", "Dragon1", "");
                //    svMain.__AngelMob = ini.ReadString("Names", "Angel", "");
                //    svMain.__CloneMob = ini.ReadString("Names", "Clone", "");
                //    svMain.__WomaHorn = ini.ReadString("Names", "WomaHorn", "");
                //    svMain.BUILDGUILDFEE = ini.ReadInteger("Names", "BuildGuildFee", svMain.BUILDGUILDFEE);
                //    svMain.__ZumaPiece = ini.ReadString("Names", "ZumaPiece", "");
                //    svMain.__GoldenImugi = ini.ReadString("Names", "GoldenImugi", "");
                //    svMain.__WhiteSnake = ini.ReadString("Names", "WhiteSnake", "");
                //    ini.Free();
                //}
                //OutMainMessage("!setup.txt loaded..");
            }
            else
            {
                OutMainMessage("File not found... <!setup.txt>");
            }
            if ((M2Share.__ClothsForMan == "") || (M2Share.__ClothsForWoman == "") || (M2Share.__WoodenSword == "") || (M2Share.__Candle == "") || (M2Share.__BasicDrug == "") || (M2Share.__GoldStone == "") || (M2Share.__SilverStone == "") || (M2Share.__SteelStone == "") || (M2Share.__CopperStone == "") || (M2Share.__BlackStone == "") || (M2Share.__Gem1Stone == "") || (M2Share.__Gem2Stone == "") || (M2Share.__Gem3Stone == "") || (M2Share.__Gem4Stone == "") || (M2Share.__ZumaMonster1 == "") || (M2Share.__ZumaMonster2 == "") || (M2Share.__ZumaMonster3 == "") || (M2Share.__ZumaMonster4 == "") || (M2Share.__Bee == "") || (M2Share.__Spider == "") || (M2Share.__WhiteSkeleton == "") || (M2Share.__ShinSu == "") || (M2Share.__ShinSu1 == "") || (M2Share.__AngelMob == "") || (M2Share.__CloneMob == "") || (M2Share.__WomaHorn == "") || (M2Share.__ZumaPiece == "") || (M2Share.__GoldenImugi == "") || (M2Share.__WhiteSnake == ""))
            {
                OutMainMessage("Check your !setup.txt file. [Names] -> ClothsForMan ...");
            }
            M2Share.LoadConfig();
            // 游戏设置读取
            // 滚傈喊 酒捞袍 牢郸胶 瘤沥
            if (M2Share.KOREANVERSION)
            {
                M2Share.INDEX_CHOCOLATE = 661;
                M2Share.INDEX_CANDY = 666;
                M2Share.INDEX_LOLLIPOP = 667;
                M2Share.INDEX_MIRBOOTS = 642;
            }
            else if (M2Share.ENGLISHVERSION)
            {
                M2Share.INDEX_CHOCOLATE = 1;
                M2Share.INDEX_CANDY = 594;
                M2Share.INDEX_LOLLIPOP = 595;
                M2Share.INDEX_MIRBOOTS = 477;
            }
            else if (M2Share.PHILIPPINEVERSION)
            {
                M2Share.INDEX_CHOCOLATE = 611;
                M2Share.INDEX_CANDY = 556;
                M2Share.INDEX_LOLLIPOP = 557;
                M2Share.INDEX_MIRBOOTS = 1;
            }
            //this.Text = svMain.ServerName + " " + (DateTime.Today).ToString() + " " + DateTime.Now.ToString() + " V" + (Grobal2.VERSION_NUMBER).ToString();
            //svMain.LoadMultiServerTables();
            //LogUDP.Host = svMain.LogServerAddress;
            //LogUDP.Port = svMain.LogServerPort;
            //ConnectTimer.Enabled = true;
            //Application.ThreadException = OnProgramException;
            //svMain.CurrentDBloadingTime = HUtil32.GetTickCount();
            //svMain.serverruntime = HUtil32.GetTickCount();
            //StartTimer.Enabled = true;
            //Timer1.Enabled = true;
        }

        private void OnProgramException(object Sender, Exception E)
        {
            if (M2Share.gErrorCount > 20000)
            {
                M2Share.gErrorCount = 0;
                OutMainMessage(E.Message + DateTime.Now.ToString("hh:nn:ss"));
            }
            M2Share.gErrorCount = M2Share.gErrorCount + 1;
        }

        public void FormDestroy(object Sender)
        {
            //int i;
            //SaveItemNumber();
            //if (svMain.boUserCastleInitialized)
            //{
            //    svMain.UserCastle.SaveAll();
            //}
            //svMain.FrontEngine.Interrupts;
            //svMain.FrontEngine.Free();
            //svMain.UserEngine.Free();
            //svMain.UserMgrEngine.Interrupts;
            //svMain.UserMgrEngine.Free();
            //SqlEngn.SqlEngn.SqlEngine.Interrupts;
            //DBSQL.DBSQL.g_DBSQL.Free();
            //SqlEngn.SqlEngn.SqlEngine.Free();
            //svMain.RunSocket.Free();
            //svMain.MainMsg.Free();
            //svMain.UserLogs.Free();
            //svMain.UserConLogs.Free();
            //svMain.UserChatLog.Free();
            //svMain.GrobalEnvir.Free();
            //svMain.ItemMan.Free();
            //svMain.MagicMan.Free();
            //svMain.NoticeMan.Free();
            //svMain.GuildMan.Free();
            //svMain.GuildAgitMan.Free();
            //svMain.GuildAgitBoardMan.Free();
            //svMain.EventMan.Free();
            //svMain.UserCastle.Free();
            //svMain.gFireDragon.Free();
            //svMain.DecoItemList.Free();
            //svMain.MakeItemList.Free();
            //svMain.MakeItemIndexList.Free();
            //svMain.StartPoints.Free();
            //svMain.SafePoints.Free();
            //svMain.MultiServerList.Free();
            //svMain.EventItemList.Free();
            //svMain.DropItemNoticeList.Free();
            //for (i = 0; i < svMain.ShopItemList.Count; i++)
            //{
            //    this.Dispose(((TShopItem)(svMain.ShopItemList.Items[i])));
            //}
            //svMain.ShopItemList = null;
            //svMain.csMsgLock.Free();
            //svMain.csTimerLock.Free();
            //svMain.csObjMsgLock.Free();
            //svMain.csSendMsgLock.Free();
            //svMain.csShare.Free();
            //svMain.csDelShare.Free();
            //svMain.csSocLock.Free();
        }

        public void SaveItemNumber()
        {
            //string fname = ".\\!setup.txt";
            //FileStream ini = new FileStream(fname);
            //if (ini != null)
            //{
            //    ini.WriteInteger("Setup", "ItemNumber", svMain.FItemNumber);
            //    ini.Free();
            //}
        }

        private bool LoadClientFileCheckSum()
        {
            bool result;
            OutMainMessage("loading client version information..");
            if (M2Share.ClientFileName1 != "")
            {
                M2Share.ClientCheckSumValue1 = M2Share.CheckFileCheckSum(M2Share.ClientFileName1);
            }
            if (M2Share.ClientFileName2 != "")
            {
                M2Share.ClientCheckSumValue2 = M2Share.CheckFileCheckSum(M2Share.ClientFileName2);
            }
            if (M2Share.ClientFileName3 != "")
            {
                M2Share.ClientCheckSumValue3 = M2Share.CheckFileCheckSum(M2Share.ClientFileName3);
            }
            if ((M2Share.ClientCheckSumValue1 == 0) && (M2Share.ClientCheckSumValue2 == 0) && (M2Share.ClientCheckSumValue3 == 0))
            {
                OutMainMessage("Loading client version information failed. check !setup.txt -> [setup] -> clientfile1,..");
                result = false;
            }
            else
            {
                OutMainMessage("Ok.");
                result = true;
            }
            return result;
        }

        public void StartTimerTimer(object Sender, System.EventArgs _e1)
        {
            int error;
            bool IsSuccess = false;
            //StartTimer.Enabled = false;
            try
            {
                if (!LoadClientFileCheckSum())
                {
                    this.Close();
                    return;
                }
                OutMainMessage("loading StdItem.DB...");
                error = LocalDB.FrmDB.LoadStdItems();
                if (error < 0)
                {
                    OutMainMessage("StdItems.DB" + " : Failure was occurred while reading this file. code=" + error.ToString());
                    this.Close();
                    return;
                }
                else
                {
                    OutMainMessage("StdItem.DB loaded.");
                }
                OutMainMessage("loading MiniMap.txt...");
                error = LocalDB.FrmDB.LoadMiniMapInfos();
                if (error < 0)
                {
                    OutMainMessage("MiniMap.txt" + " : Failure was occurred while reading this file. code=" + error.ToString());
                    this.Close();
                    return;
                }
                else
                {
                    OutMainMessage("MiniMap information loaded.");
                }
                OutMainMessage("Loading DragonSystem...");
                OutMainMessage(M2Share.gFireDragon.Initialize(M2Share.EnvirDir + DragonSystem.DRAGONITEMFILE, ref IsSuccess));
                if (!IsSuccess)
                {
                    OutMainMessage(DragonSystem.DRAGONITEMFILE + "甫 佬绰档吝坷幅啊 惯积窍看澜");
                }
                OutMainMessage("loading MapFiles...");
                error = LocalDB.FrmDB.LoadMapFiles();
                if (error < 0)
                {
                    OutMainMessage(" : Failure was occurred while reading this map file. code=" + error.ToString());
                    this.Close();
                    return;
                }
                else
                {
                    OutMainMessage("Mapfile loaded.");
                }
                OutMainMessage("loading Monster.DB...");
                error = LocalDB.FrmDB.LoadMonsters();
                if (error <= 0)
                {
                    OutMainMessage("Monster.DB" + " : Failure was occurred while reading this file. code=" + error.ToString());
                    this.Close();
                    return;
                }
                else
                {
                    OutMainMessage("Monster.DB loaded.");
                }
                OutMainMessage("loading Magic.DB...");
                error = LocalDB.FrmDB.LoadMagic();
                if (error <= 0)
                {
                    OutMainMessage("Magic.DB" + " : Failure was occurred while reading this file. code=" + error.ToString());
                    this.Close();
                    return;
                }
                else
                {
                    OutMainMessage("Magic.DB loaded.");
                }
                OutMainMessage("loading MonGen.txt...");
                error = LocalDB.FrmDB.LoadZenLists();
                if (error <= 0)
                {
                    OutMainMessage("MonGen.txt" + " : Failure was occurred while reading this file. code=" + error.ToString());
                    this.Close();
                    return;
                }
                else
                {
                    OutMainMessage("MonGen.txt loaded.");
                }
                // 2003/06/20 捞亥飘各 哩 皋技瘤 殿废
                OutMainMessage("loading GenMsg.txt...");
                error = LocalDB.FrmDB.LoadGenMsgLists();
                if (error <= 0)
                {
                    OutMainMessage("GenMsg.txt" + " : Failure was occurred while reading this file. code=" + error.ToString());
                    this.Close();
                    return;
                }
                else
                {
                    OutMainMessage("GenMsg.txt loaded.");
                }
                OutMainMessage("loading UnbindList.txt...");
                error = LocalDB.FrmDB.LoadUnbindItemLists();
                if (error < 0)
                {
                    OutMainMessage("UnbindList.txt" + " : Failure was occurred while reading this file. code=" + error.ToString());
                    this.Close();
                    return;
                }
                else
                {
                    OutMainMessage("UnbindList information loaded.");
                }
                OutMainMessage("loading DropItemNoticeList.txt...");
                error = LocalDB.FrmDB.LoadDropItemNoticeList();
                if (error < 0)
                {
                    OutMainMessage("DropItemNoticeList.txt" + "Failure was occurred while reading this file. code=" + error.ToString());
                    this.Close();
                    return;
                }
                else
                {
                    OutMainMessage("DropItemNoticeList information loaded.");
                }
                LocalDB.FrmDB.LoadShopItemList();
                OutMainMessage("loading ShopItemList.txt...");
                OutMainMessage("loading MapQuest.txt...");
                error = LocalDB.FrmDB.LoadMapQuestInfos();
                if (error < 0)
                {
                    OutMainMessage("MapQuest.txt" + " : Failure was occurred while reading this file. code=" + error.ToString());
                    this.Close();
                    return;
                }
                else
                {
                    OutMainMessage("MapQuest information loaded.");
                }
                OutMainMessage("loading 00Default.txt...");
                error = LocalDB.FrmDB.LoadDefaultNpc();
                if (error < 0)
                {
                    OutMainMessage("00Default.txt" + " : Failure was occurred while reading this file. code=" + error.ToString());
                    this.Close();
                    return;
                }
                else
                {
                    OutMainMessage("DefaultNpc information loaded..");
                }
                OutMainMessage("loading QuestDiary\\*.txt...");
                error = LocalDB.FrmDB.LoadQuestDiary();
                if (error < 0)
                {
                    OutMainMessage("QuestDiary\\*.txt" + " : Failure was occurred while reading this file. code=" + error.ToString());
                    this.Close();
                    return;
                }
                else
                {
                    OutMainMessage("QuestDiary information loaded.");
                }
                //if (LoadAbusiveList("!Abuse.txt"))
                //{
                //    OutMainMessage("!Abuse.txt" + " loaded..");
                //}
                if (M2Share.LoadLineNotice(M2Share.LINENOTICEFILE))
                {
                    OutMainMessage(M2Share.LINENOTICEFILE + " loaded..");
                }
                else
                {
                    OutMainMessage(M2Share.LINENOTICEFILE + " loading failure !!!!!!!!!");
                }
                if (M2Share.LoadLineHelp(M2Share.LINEHELPFILE))
                {
                    OutMainMessage(M2Share.LINEHELPFILE + " loaded..");
                }
                else
                {
                    OutMainMessage(M2Share.LINEHELPFILE + " loading failure !!!!!!!!!");
                }
                if (LocalDB.FrmDB.LoadAdminFiles() > 0)
                {
                    OutMainMessage("AdminList loaded..");
                }
                else
                {
                    OutMainMessage("AdminList loading failure !!!!!!!!!");
                }
                LocalDB.FrmDB.LoadChatLogFiles();
                OutMainMessage("Chatting Log List loaded..");
                M2Share.GuildMan.LoadGuildList();
                OutMainMessage("GuildList loaded..");
                M2Share.GuildAgitMan.LoadGuildAgitList();
                OutMainMessage("GuildAgitList loaded..");
                M2Share.GuildAgitBoardMan.LoadAllGaBoardList("");
                OutMainMessage("GuildAgitBoardList loaded..");
                M2Share.UserCastle.Initialize();
                M2Share.boUserCastleInitialized = true;
                OutMainMessage("Castle initialized..");
                if (M2Share.ServerIndex == 0)
                {
                    InterServerMsg.FrmSrvMsg.Initialize();
                }
                else
                {
                    InterMsgClient.FrmMsgClient.Initialize();
                }
                if (DBSQL.g_DBSQL.Connect(M2Share.ServerName, ".\\!DBSQL.TXT"))
                {
                    OutMainMessage("DBSQL Connection Success");
                }
                else
                {
                    OutMainMessage("DBSQL Connection Fail [ ERROR!] ");
                }
                if (File.Exists("M2Server.exe"))
                {
                    //handle = File.Open("M2Server.exe", (FileMode)FileAccess.Read | FileShare.ReadWrite);
                    //if (handle > 0)
                    //{
                    //    FileDate = File.GetCreationTime(handle);
                    //    DateTime = new DateTime(FileDate);
                    //    OutMainMessage("FileDateVersion : " + DateTime.ToString());
                    //    handle.Close();
                    //}
                }
                StartServer();
                M2Share.ServerReady = true;
                //Thread.CurrentThread.Sleep(500);
                //ConnectTimer.Enabled = true;
                M2Share.runstart = HUtil32.GetTickCount();
                M2Share.rcount = 0;
                M2Share.humrotatetime = HUtil32.GetTickCount();
                //RunTimer.Enabled = true;
            }
            catch
            {
                OutMainMessage("starttimer exception...");
            }
        }

        public void OutMainMessage(string message)
        {
            Console.WriteLine(message);
        }

        public void Close()
        {

        }

        public void Timer1Timer(object Sender, System.EventArgs _e1)
        {
            //            int i;
            //            int runsec;
            //            System.IO.FileInfo fhandle;
            //            bool fail;
            //            double r;
            //            short ahour;
            //            short amin;
            //            short asec;
            //            string checkstr;
            //            string str;
            //            string sendb;
            //            TRunGateInfo pgate;
            //            int down;
            //            down = 1;
            //            try
            //            {
            //                svMain.csTimerLock.Enter();
            //                if (Memo1.Lines.Count > 500)
            //                {
            //                    Memo1.Lines.Clear();
            //                }
            //                fail = true;
            //                if (svMain.MainMsg.Count > 0)
            //                {
            //                    try
            //                    {
            //                        if (!File.Exists(svMain.ErrorLogFile))
            //                        {
            //                            fhandle = new FileInfo(svMain.ErrorLogFile);
            //                            _W_1 = fhandle.CreateText();
            //                            fail = false;
            //                        }
            //                        else
            //                        {
            //                            fhandle = new FileInfo(svMain.ErrorLogFile);
            //                            _W_1 = fhandle.AppendText();
            //                            fail = false;
            //                        }
            //                    }
            //                    catch
            //                    {
            //                        OutMainMessage("Error on writing ErrorLog.");
            //                    }
            //                }
            //                for (i = 0; i < svMain.MainMsg.Count; i++)
            //                {
            //                    OutMainMessage(svMain.MainMsg[i]);
            //                    if (!fail)
            //                    {
            //                        _W_1.WriteLine(svMain.MainMsg[i]);
            //                    }
            //                }
            //                svMain.MainMsg.Clear();
            //                if (!fail)
            //                {
            //                    _W_1.Close();
            //                }
            //                for (i = 0; i < svMain.UserLogs.Count; i++)
            //                {
            //                    try
            //                    {
            //                        str = "1\09" + (svMain.ServerNumber).ToString() + "\09" + (svMain.ServerIndex).ToString() + "\09" + svMain.UserLogs[i];
            //                        LogUDP.Send(str);
            //                    }
            //                    catch
            //                    {
            //                        continue;
            //                    }
            //                }
            //                svMain.UserLogs.Clear();
            //                if (svMain.UserConLogs.Count > 0)
            //                {
            //                    try
            //                    {
            //                        svMain.WriteConLogs(svMain.UserConLogs);
            //                    }
            //                    catch
            //                    {
            //                        OutMainMessage("ERROR_CONLOG_FAIL");
            //                    }
            //                    svMain.UserConLogs.Clear();
            //                }
            //                if (svMain.UserChatLog.Count > 0)
            //                {
            //                    try
            //                    {
            //                        svMain.WriteChatLogs(svMain.UserChatLog);
            //                    }
            //                    catch
            //                    {
            //                        OutMainMessage("ERROR_CHATLOG_FAIL");
            //                    }
            //                    svMain.UserChatLog.Clear();
            //                }
            //            }
            //            finally
            //            {
            //                svMain.csTimerLock.Leave();
            //            }
            //            try
            //            {
            //                down = 2;
            //                if (svMain.ServerIndex == 0)
            //                {
            //                    checkstr = "[M]";
            //                }
            //                else if (InterMsgClient.FrmMsgClient.MsgClient.Socket.Connected)
            //                {
            //                    checkstr = "[S]";
            //                }
            //                else
            //                {
            //                    checkstr = "[ ]";
            //                }
            //                checkstr = checkstr + (svMain.gErrorCount).ToString();
            //#if DEBUG
            //                // sonmg
            //                checkstr = checkstr + " DEBUG";
            //#endif
            //                down = 3;
            //                runsec = (HUtil32.GetTickCount() - svMain.serverruntime) / 1000;
            //                ahour = runsec / 3600;
            //                amin = (runsec % 3600) / 60;
            //                asec = runsec % 60;
            //                LbRunTime.Text = "[" + (ahour).ToString() + ":" + (amin).ToString() + ":" + (asec).ToString() + "]" + checkstr;
            //                down = 4;
            //                // IntToStr(UserEngine.MonRunCount) + '/' +
            //                LbUserCount.Text = "(" + (svMain.UserEngine.MonCount).ToString() + ")   " + (svMain.UserEngine.GetRealUserCount()).ToString() + "/" + (svMain.UserEngine.GetUserCount()).ToString() + "/" + (svMain.UserEngine.FreeUserCount).ToString();
            //                Label1.Text = "Run" + (svMain.curruncount).ToString() + "/" + (svMain.minruncount).ToString() + " " + "Soc" + (svMain.cursoctime).ToString() + "/" + (svMain.maxsoctime).ToString() + " " + "Usr" + (svMain.curusrcount).ToString() + "/" + (svMain.maxusrtime).ToString() + ".";
            //                Label2.Text = "Hum" + (svMain.curhumtime).ToString() + "/" + (svMain.maxhumtime).ToString() + " " + "Mon" + (svMain.curmontime).ToString() + "/" + (svMain.maxmontime).ToString() + " " + "UsrRot" + (svMain.curhumrotatetime).ToString() + "/" + (svMain.maxhumrotatetime).ToString() + "(" + (svMain.humrotatecount).ToString() + ")";
            //                Label5.Text = svMain.LatestGenStr + " - " + svMain.LatestMonStr + "    ";
            //                down = 5;
            //                r = GetTickCount / (1000 * 60 * 60 * 24);
            //                if (r >= 36)
            //                {
            //                    LbTimeCount.Font.Color = System.Drawing.Color.Red;
            //                }
            //                LbTimeCount.Text = FloatToString(r) + "Day";
            //                down = 6;
            //                str = "";
            //                TRunSocket _wvar1 = svMain.RunSocket;
            //                for (i = 0; i < RunSock.MAXGATE; i++)
            //                {
            //                    pgate = _wvar1.GateArr[i];
            //                    if (pgate != null)
            //                    {
            //                        if (pgate.Socket != null)
            //                        {
            //                            if (pgate.sendbytes < 1024)
            //                            {
            //                                sendb = (pgate.sendbytes).ToString() + "b ";
            //                            }
            //                            else
            //                            {
            //                                sendb = (pgate.sendbytes / 1024).ToString() + "kb ";
            //                            }
            //                            str = str + "[G" + (i + 1).ToString() + ": " + (pgate.curbuffercount).ToString() + "/" + (pgate.remainbuffercount).ToString() + " " + sendb + (pgate.sendsoccount).ToString() + "] ";
            //                        }
            //                    }
            //                }
            //                Label3.Text = str;
            //                down = 7;
            //                svMain.minruncount++;
            //                svMain.maxsoctime -= 1;
            //                svMain.maxusrtime -= 1;
            //                svMain.maxhumtime -= 1;
            //                svMain.maxmontime -= 1;
            //                svMain.maxhumrotatetime -= 1;
            //            }
            //            catch
            //            {
            //                OutMainMessage("Exception Timer1Timer :" + (down).ToString());
            //            }
        }

        public void SaveVariableTimerTimer(object Sender, System.EventArgs _e1)
        {
            SaveItemNumber();
        }

        private void MakeStoneMines()
        {
            int i;
            int x;
            int y;
            TStoneMineEvent ev;
            TEnvirnoment env;
            for (i = 0; i < M2Share.GrobalEnvir.Count; i++)
            {
                env = (TEnvirnoment)M2Share.GrobalEnvir[i];
                if (env.MineMap > 0)
                {
                    for (x = 0; x < env.MapWidth; x++)
                    {
                        for (y = 0; y < env.MapHeight; y++)
                        {
                            if (env.MineMap == 1)
                            {
                                ev = new TStoneMineEvent(env, x, y, Grobal2.ET_MINE);
                            }
                            else if (env.MineMap == 2)
                            {
                                ev = new TStoneMineEvent(env, x, y, Grobal2.ET_MINE2);
                            }
                            else
                            {
                                ev = new TStoneMineEvent(env, x, y, Grobal2.ET_MINE3);
                            }
                            if ((ev != null) && ev.IsAddToMap == false)
                            {
                                ev.Free();
                                ev = null;
                            }
                        }
                    }
                }
            }
        }

        private void StartServer()
        {
            int error;
            int TotalDecoMonCount;
            try
            {
                IdSrvClient.FrmIDSoc.Initialize();
                OutMainMessage("IDSoc Initialized..");
                M2Share.GrobalEnvir.InitEnvirnoments();
                OutMainMessage("GrobalEnvir loaded..");
                MakeStoneMines();
                // 堡籍阑 盲款促.
                OutMainMessage("MakeStoneMines...");
                error = LocalDB.FrmDB.LoadMerchants();
                if (error < 0)
                {
                    OutMainMessage("merchant.txt" + " : Failure was occurred while reading this file. code=" + error.ToString());
                    this.Close();
                    return;
                }
                else
                {
                    OutMainMessage("merchant loaded...");
                }
                // ---------------------------------------------
                // 厘盔操固扁 酒捞袍 府胶飘 肺靛(sonmg)
                // LoadAgitDecoMon焊促 刚历 角青登绢具 窃.
                error = LocalDB.FrmDB.LoadDecoItemList();
                if (error < 0)
                {
                    OutMainMessage("DecoItem.txt" + " : Failure was occurred while reading this file. code=" + error.ToString());
                    this.Close();
                    return;
                }
                else
                {
                    OutMainMessage("DecoItemList loaded..");
                }
                // 0锅 辑滚俊辑父 佬绢甸烙.
                if (M2Share.ServerIndex == 0)
                {
                    // 厘盔操固扁 坷宏璃飘 肺靛(sonmg)
                    // LoadDecoItemList焊促 唱吝俊 角青登绢具 窃.
                    error = M2Share.GuildAgitMan.LoadAgitDecoMon();
                    if (error < 0)
                    {
                        OutMainMessage(M2Share.GuildBaseDir + Guild.AGITDECOMONFILE + " : Failure was occurred while reading this file. code=" + error.ToString());
                        this.Close();
                        return;
                    }
                    else
                    {
                        // 厘盔俊 操固扁 坷宏璃飘甫 积己矫挪促.
                        TotalDecoMonCount = M2Share.GuildAgitMan.MakeAgitDecoMon();
                        // 厘盔喊 操固扁 坷宏璃飘 俺荐甫 辆钦茄促.
                        M2Share.GuildAgitMan.ArrangeEachAgitDecoMonCount();
                        OutMainMessage("AgitDecoMon " + TotalDecoMonCount.ToString() + " loaded...");
                    }
                }
                // ---------------------------------------------
                if (!M2Share.BoVentureServer)
                {
                    // 葛氰辑滚俊辑绰 版厚捍捞 绝促.
                    error = LocalDB.FrmDB.LoadGuards();
                    if (error < 0)
                    {
                        OutMainMessage("Guardlist.txt" + " : Failure was occurred while reading this file. code=" + error.ToString());
                        this.Close();
                        return;
                    }
                }
                error = LocalDB.FrmDB.LoadNpcs();
                if (error < 0)
                {
                    OutMainMessage("Npc.txt" + " : Failure was occurred while reading this file. code=" + error.ToString());
                    this.Close();
                    return;
                }
                else
                {
                    OutMainMessage("Npc loaded..");
                }
                error = LocalDB.FrmDB.LoadMakeItemList();
                if (error < 0)
                {
                    OutMainMessage("MakeItem.txt" + " : Failure was occurred while reading this file. code=" + error.ToString());
                    this.Close();
                    return;
                }
                else
                {
                    OutMainMessage("MakeItem loaded..");
                }
                error = LocalDB.FrmDB.LoadStartPoints();
                if (error < 0)
                {
                    OutMainMessage("StartPoint.txt" + " : Failure was occurred while reading this file. code=" + error.ToString());
                    this.Close();
                    return;
                }
                else
                {
                    OutMainMessage("StartPoints loaded..");
                }
                error = LocalDB.FrmDB.LoadSafePoints();
                if (error < 0)
                {
                    OutMainMessage("SafePoint.txt" + " : Failure was occurred while reading this file. code=" + error.ToString());
                    this.Close();
                    return;
                }
                else
                {
                    OutMainMessage("SafePoints loaded..");
                }
                // svMain.FrontEngine.Resume();
                OutMainMessage("F-Engine resumed..");
                M2Share.UserEngine.Initialize();
                OutMainMessage("U-Engine initialized..");
                // svMain.UserMgrEngine.Resume();
                OutMainMessage("UserMgr-Engine resumed..");
                // SqlEngn.SqlEngine.Resume();
                OutMainMessage("SQL-Engine resumed..");
            }
            catch
            {
                OutMainMessage("startserver exception..");
            }
        }

        public void ConnectTimerTimer(object Sender, System.EventArgs _e1)
        {
            //if (!DBSocket.Active)
            //{
            //    DBSocket.Active = true;
            //}
        }

        public void RunTimerTimer(object Sender, System.EventArgs _e1)
        {
            if (M2Share.ServerReady)
            {
                M2Share.RunSocket.Run();
                IdSrvClient.FrmIDSoc.DecodeSocStr();
                M2Share.UserEngine.ExecuteRun();
                SqlEngn.SqlEngine.ExecuteRun();
                M2Share.EventMan.Run();
                if (M2Share.ServerIndex == 0)
                {
                    InterServerMsg.FrmSrvMsg.Run();
                }
                else
                {
                    InterMsgClient.FrmMsgClient.Run();
                }
            }
            M2Share.rcount++;
            if (HUtil32.GetTickCount() - M2Share.runstart > 250)
            {
                M2Share.runstart = HUtil32.GetTickCount();
                M2Share.curruncount = M2Share.rcount;
                if (M2Share.minruncount > M2Share.curruncount)
                {
                    M2Share.minruncount = M2Share.curruncount;
                }
                M2Share.rcount = 0;
            }
        }

        public void GateSocketClientConnect(object Sender, Socket Socket)
        {
            M2Share.RunSocket.Connect(Socket);
        }

        public void GateSocketClientDisconnect(object Sender, Socket Socket)
        {
            M2Share.RunSocket.Disconnect(Socket);
        }

        public void GateSocketClientError(object Sender, Socket Socket, ref int ErrorCode)
        {
            M2Share.RunSocket.SocketError(Socket, ref ErrorCode);
        }

        public void GateSocketClientRead(object Sender, Socket Socket)
        {
            M2Share.RunSocket.SocketRead(Socket);
        }

        public void DBSocketConnect(object Sender, Socket Socket)
        {
        }

        public void DBSocketDisconnect(object Sender, Socket Socket)
        {
        }

        public void DBSocketError(object Sender, Socket Socket, ref int ErrorCode)
        {
            ErrorCode = 0;
            Socket.Close();
        }

        public void DBSocketRead(object Sender, Socket Socket)
        {
            //string data = string.Empty;
            //try
            //{
            //    svMain.csSocLock.Enter();
            //    data = Socket.ReceiveText;
            //    svMain.RDBSocData = svMain.RDBSocData + data;
            //    if (!svMain.ReadyDBReceive)
            //    {
            //        svMain.RDBSocData = "";
            //    }
            //}
            //finally
            //{
            //    svMain.csSocLock.Leave();
            //}
            //svMain.UserMgrEngine.OnDBRead(data);
        }

        public void FormCloseQuery(object Sender, System.ComponentModel.CancelEventArgs _e1)
        {
            //if (!svMain.ServerClosing)
            //{
            //    CanClose = false;
            //    if (OutMainMessage("exit server ? (yes=exit)", Application.ProductName, new object[] { MessageBoxButtons.YesNo | MessageBoxButtons.YesNo | MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            //    {
            //        svMain.ServerClosing = true;
            //        TCloseTimer.Enabled = true;
            //        svMain.RunSocket.CloseAllGate();
            //    }
            //}
        }

        public void TCloseTimerTimer(object Sender, System.EventArgs _e1)
        {
            if ((M2Share.UserEngine.GetRealUserCount() == 0) && M2Share.FrontEngine.IsFinished())
            {
                this.Close();
            }
        }

        public void Panel1DblClick(object Sender, System.EventArgs _e1)
        {
            //FileStream ini;
            //string fname;
            //string bostr;
            //if (FSrvValue.FSrvValue.FrmServerValue.Execute())
            //{
            //    fname = ".\\!setup.txt";
            //    ini = new FileStream(fname);
            //    if (ini != null)
            //    {
            //        ini.WriteInteger("Server", "HumLimit", svMain.HumLimitTime);
            //        ini.WriteInteger("Server", "MonLimit", svMain.MonLimitTime);
            //        ini.WriteInteger("Server", "ZenLimit", svMain.ZenLimitTime);
            //        ini.WriteInteger("Server", "SocLimit", svMain.SocLimitTime);
            //        ini.WriteInteger("Server", "DecLimit", svMain.DecLimitTime);
            //        ini.WriteInteger("Server", "NpcLimit", svMain.NpcLimitTime);
            //        ini.WriteInteger("Server", "SendBlock", svMain.SENDBLOCK);
            //        ini.WriteInteger("Server", "CheckBlock", svMain.SENDCHECKBLOCK);
            //        ini.WriteInteger("Server", "GateLoad", svMain.GATELOAD);
            //        if (svMain.BoViewHackCode)
            //        {
            //            bostr = "TRUE";
            //        }
            //        else
            //        {
            //            bostr = "FALSE";
            //        }
            //        ini.WriteString("Server", "ViewHackMessage", bostr);
            //        if (svMain.BoViewAdmissionfail)
            //        {
            //            bostr = "TRUE";
            //        }
            //        else
            //        {
            //            bostr = "FALSE";
            //        }
            //        ini.WriteString("Server", "ViewAdmissionFailure", bostr);
            //    }
            //}
        }

        public void SpeedButton1Click(object Sender, System.EventArgs _e1)
        {
            //FileStream ini;
            //string fname;
            //IdSrvClient.FrmIDSoc.Timer1Timer(this);
            //TFrmMsgClient _wvar1 = InterMsgClient.FrmMsgClient;
            //if (svMain.ServerIndex != 0)
            //{
            //    if (!_wvar1.MsgClient.Socket.Connected)
            //    {
            //        _wvar1.MsgClient.Active = true;
            //    }
            //}
            //fname = ".\\!setup.txt";
            //if (File.Exists(fname))
            //{
            //    ini = new FileStream(fname);
            //    if (ini != null)
            //    {
            //        svMain.LogServerAddress = ini.ReadString("Server", "LogServerAddr", "192.168.0.152");
            //        svMain.LogServerPort = ini.ReadInteger("Server", "LogServerPort", 10000);
            //        svMain.ClientFileName1 = ini.ReadString("Setup", "ClientFile1", "");
            //        svMain.ClientFileName2 = ini.ReadString("Setup", "ClientFile2", "");
            //        svMain.ClientFileName3 = ini.ReadString("Setup", "ClientFile3", "");
            //    }
            //    ini.Free();
            //}
            //LogUDP.Host = svMain.LogServerAddress;
            //LogUDP.Port = svMain.LogServerPort;
            //svMain.LoadMultiServerTables();
            //IdSrvClient.FrmIDSoc.LoadShareIPList();
            //LoadClientFileCheckSum();
        }

        public void SpeedButton2Click(object Sender, System.EventArgs _e1)
        {
            //FGameSet.FGameSet.FrmGameConfig.Top = this.Top + 20;
            //FGameSet.FGameSet.FrmGameConfig.Left = this.Left;
            //if (FGameSet.FGameSet.FrmGameConfig.Execute())
            //{
            //    M2Share.SaveConfig();
            //}
        }
    }
}
