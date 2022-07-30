using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Threading;
using System.Windows.Forms;
using SystemModule;

namespace GameSvr
{
    public class TFrmMain
    {
        public void RefreshForm()
        {
            Application.DoEvents();
        }

        public void FormCreate(System.Object Sender, System.EventArgs _e1)
        {
            string fname;
            string str;
            FileStream ini;
            short ayear;
            short amon;
            short aday;
            short ahour;
            short amin;
            short asec;
            short amsec;
            System.IO.FileInfo fhandle;
            svMain.gErrorCount = 0;
            svMain.ServerIndex = 0;
            svMain.RunSocket = new TRunSocket();
            svMain.MainMsg = new ArrayList();
            svMain.UserLogs = new ArrayList();
            svMain.UserConLogs = new ArrayList();
            svMain.UserChatLog = new ArrayList();
            svMain.GrobalEnvir = new TEnvirList();
            svMain.ItemMan = new ItemUnitSystem();
            svMain.MagicMan = new TMagicManager();
            svMain.NoticeMan = new TNoticeManager();
            svMain.GuildMan = new TGuildManager();
            svMain.GuildAgitMan = new TGuildAgitManager();
            svMain.GuildAgitBoardMan = new TGuildAgitBoardManager();
            svMain.EventMan = new TEventManager();
            svMain.UserCastle = new TUserCastle();
            svMain.boUserCastleInitialized = false;
            svMain.gFireDragon = new TDragonSystem();
            svMain.FrontEngine = new TFrontEngine();
            svMain.UserEngine = new TUserEngine();
            svMain.UserMgrEngine = new TUserMgrEngine();
            DBSQL.g_DBSQL = new TDBSql();
            SqlEngn.SqlEngine = new TSQLEngine();
            svMain.DecoItemList = new ArrayList();
            svMain.MakeItemList = new ArrayList();
            svMain.MakeItemIndexList = new ArrayList();
            svMain.StartPoints = new List<TSafePoint>();
            svMain.SafePoints = new List<TSafePoint>();
            svMain.MultiServerList = new ArrayList();
            svMain.ShutUpList = new TQuickList();
            svMain.MiniMapList = new ArrayList();
            svMain.UnbindItemList = new ArrayList();
            svMain.LineNoticeList = new ArrayList();
            svMain.LineHelpList = new ArrayList();
            svMain.QuestDiaryList = new ArrayList();
            svMain.DefaultNpc = null;
            svMain.DropItemNoticeList = new ArrayList();
            svMain.ShopItemList = new TGList();
            svMain.EventItemList = new ArrayList();
            svMain.EventItemGifeBaseNumber = 0;
            svMain.csMsgLock = new object();
            svMain.csTimerLock = new object();
            svMain.csObjMsgLock = new object();
            svMain.csSendMsgLock = new object();
            svMain.csShare = new object();
            svMain.csDelShare = new object();
            svMain.csSocLock = new object();
            svMain.usLock = new object();
            svMain.usIMLock = new object();
            svMain.ruLock = new object();
            svMain.ruSendLock = new object();
            svMain.ruCloseLock = new object();
            svMain.socstrLock = new object();
            svMain.fuLock = new object();
            svMain.fuOpenLock = new object();
            svMain.fuCloseLock = new object();
            svMain.humanLock = new object();
            svMain.umLock = new object();
            svMain.SQLock = new object();
            svMain.RDBSocData = "";
            svMain.ReadyDBReceive = false;
            svMain.RunFailCount = 0;
            svMain.MirUserLoadCount = 0;
            svMain.MirUserSaveCount = 0;
            svMain.BoGetGetNeedNotice = false;
            svMain.FCertify = 0;
            svMain.FItemNumber = 0;
            svMain.ServerReady = false;
            svMain.ServerClosing = false;
            svMain.BoEnableAbusiveFilter = true;
            svMain.LottoSuccess = 0;
            svMain.LottoFail = 0;
            svMain.Lotto1 = 0;
            svMain.Lotto2 = 0;
            svMain.Lotto3 = 0;
            svMain.Lotto4 = 0;
            svMain.Lotto5 = 0;
            svMain.Lotto6 = 0;
            svMain.CurrentMerchantIndex = 0;
            svMain.ServerTickDifference = 0;
            //FillChar(svMain.GrobalQuestParams, sizeof(svMain.GrobalQuestParams), '\0');
            //ayear = DateTime.Today.Year;
            //amon = DateTime.Today.Month;
            //aday = DateTime.Today.Day;
            //ahour = DateTime.Now.Hour;
            //amin = DateTime.Now.Minute;
            //asec = DateTime.Now.Second;
            //amsec = DateTime.Now.Millisecond;
            //svMain.ErrorLogFile = ".\\Log\\" + (ayear).ToString() + "-" + (amon).ToString() + "-" + svMain.IntTo_Str(aday) + "." + svMain.IntTo_Str(ahour) + "-" + svMain.IntTo_Str(amin) + ".log";
            fhandle = new FileInfo(svMain.ErrorLogFile);
            StreamWriter _W_1 = fhandle.CreateText();
            _W_1.Close();
            svMain.minruncount = 99999;
            svMain.maxsoctime = 0;
            svMain.maxusrtime = 0;
            svMain.maxhumtime = 0;
            svMain.maxmontime = 0;
            svMain.curhumrotatetime = 0;
            svMain.maxhumrotatetime = 0;
            svMain.humrotatecount = 0;
            svMain.HumLimitTime = 30;
            svMain.MonLimitTime = 30;
            svMain.ZenLimitTime = 5;
            svMain.NpcLimitTime = 5;
            svMain.SocLimitTime = 10;
            svMain.DecLimitTime = 20;
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
                MessageBox.Show("File not found... <!setup.txt>");
            }
            if ((svMain.__ClothsForMan == "") || (svMain.__ClothsForWoman == "") || (svMain.__WoodenSword == "") || (svMain.__Candle == "") || (svMain.__BasicDrug == "") || (svMain.__GoldStone == "") || (svMain.__SilverStone == "") || (svMain.__SteelStone == "") || (svMain.__CopperStone == "") || (svMain.__BlackStone == "") || (svMain.__Gem1Stone == "") || (svMain.__Gem2Stone == "") || (svMain.__Gem3Stone == "") || (svMain.__Gem4Stone == "") || (svMain.__ZumaMonster1 == "") || (svMain.__ZumaMonster2 == "") || (svMain.__ZumaMonster3 == "") || (svMain.__ZumaMonster4 == "") || (svMain.__Bee == "") || (svMain.__Spider == "") || (svMain.__WhiteSkeleton == "") || (svMain.__ShinSu == "") || (svMain.__ShinSu1 == "") || (svMain.__AngelMob == "") || (svMain.__CloneMob == "") || (svMain.__WomaHorn == "") || (svMain.__ZumaPiece == "") || (svMain.__GoldenImugi == "") || (svMain.__WhiteSnake == ""))
            {
                MessageBox.Show("Check your !setup.txt file. [Names] -> ClothsForMan ...");
            }
            M2Share.LoadConfig();
            // 游戏设置读取
            // 滚傈喊 酒捞袍 牢郸胶 瘤沥
            if (svMain.KOREANVERSION)
            {
                svMain.INDEX_CHOCOLATE = 661;
                svMain.INDEX_CANDY = 666;
                svMain.INDEX_LOLLIPOP = 667;
                svMain.INDEX_MIRBOOTS = 642;
            }
            else if (svMain.ENGLISHVERSION)
            {
                svMain.INDEX_CHOCOLATE = 1;
                svMain.INDEX_CANDY = 594;
                svMain.INDEX_LOLLIPOP = 595;
                svMain.INDEX_MIRBOOTS = 477;
            }
            else if (svMain.PHILIPPINEVERSION)
            {
                svMain.INDEX_CHOCOLATE = 611;
                svMain.INDEX_CANDY = 556;
                svMain.INDEX_LOLLIPOP = 557;
                svMain.INDEX_MIRBOOTS = 1;
            }
            //this.Text = svMain.ServerName + " " + (DateTime.Today).ToString() + " " + DateTime.Now.ToString() + " V" + (Grobal2.VERSION_NUMBER).ToString();
            //svMain.LoadMultiServerTables();
            //LogUDP.Host = svMain.LogServerAddress;
            //LogUDP.Port = svMain.LogServerPort;
            //ConnectTimer.Enabled = true;
            //Application.ThreadException = OnProgramException;
            //svMain.CurrentDBloadingTime = HUtil32.GetTickCount;
            //svMain.serverruntime = HUtil32.GetTickCount;
            //StartTimer.Enabled = true;
            //Timer1.Enabled = true;
        }

        private void OnProgramException(Object Sender, Exception E)
        {
            if (svMain.gErrorCount > 20000)
            {
                svMain.gErrorCount = 0;
                OutMainMessage(E.Message + formatdatetime("hh:nn:ss", DateTime.Now));
            }
            svMain.gErrorCount = svMain.gErrorCount + 1;
        }

        public void FormDestroy(Object Sender)
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
            string fname;
            FileStream ini;
            fname = ".\\!setup.txt";
            ini = new FileStream(fname);
            if (ini != null)
            {
                ini.WriteInteger("Setup", "ItemNumber", svMain.FItemNumber);
                ini.Free();
            }
        }

        private bool LoadClientFileCheckSum()
        {
            bool result;
            OutMainMessage("loading client version information..");
            if (svMain.ClientFileName1 != "")
            {
                svMain.ClientCheckSumValue1 = svMain.CheckFileCheckSum(svMain.ClientFileName1);
            }
            if (svMain.ClientFileName2 != "")
            {
                svMain.ClientCheckSumValue2 = svMain.CheckFileCheckSum(svMain.ClientFileName2);
            }
            if (svMain.ClientFileName3 != "")
            {
                svMain.ClientCheckSumValue3 = svMain.CheckFileCheckSum(svMain.ClientFileName3);
            }
            if ((svMain.ClientCheckSumValue1 == 0) && (svMain.ClientCheckSumValue2 == 0) && (svMain.ClientCheckSumValue3 == 0))
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

        public void StartTimerTimer(System.Object Sender, System.EventArgs _e1)
        {
            int error;
            int checkvalue;
            bool IsSuccess = false;
            int handle;
            int FileDate;
            DateTime DateTime;
            StartTimer.Enabled = false;
            try
            {
                checkvalue = SIZEOFFDB;
                if (sizeof(FDBRecord) != checkvalue)
                {
                    MessageBox.Show("sizeof(THuman) <> SIZEOFTHUMAN");
                    this.Close();
                    return;
                }
                if (!LoadClientFileCheckSum())
                {
                    this.Close();
                    return;
                }
                OutMainMessage("loading StdItem.DB...");
                // 扁夯 单捞鸥甫 肺爹 茄促.
                error = LocalDB.FrmDB.LoadStdItems();
                if (error < 0)
                {
                    MessageBox.Show("StdItems.DB" + " : Failure was occurred while reading this file. code=" + error.ToString());
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
                    MessageBox.Show("MiniMap.txt" + " : Failure was occurred while reading this file. code=" + error.ToString());
                    this.Close();
                    return;
                }
                else
                {
                    OutMainMessage("MiniMap information loaded.");
                }
                // 侩带怜 矫胶袍 肺爹
                OutMainMessage("Loading DragonSystem...");
                OutMainMessage(svMain.gFireDragon.Initialize(svMain.EnvirDir + DragonSystem.DRAGONITEMFILE, ref IsSuccess));
                if (!IsSuccess)
                {
                    OutMainMessage(DragonSystem.DRAGONITEMFILE + "甫 佬绰档吝坷幅啊 惯积窍看澜");
                }
                OutMainMessage("loading MapFiles...");
                error = LocalDB.FrmDB.LoadMapFiles();
                if (error < 0)
                {
                    MessageBox.Show(" : Failure was occurred while reading this map file. code=" + error.ToString());
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
                    MessageBox.Show("Monster.DB" + " : Failure was occurred while reading this file. code=" + error.ToString());
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
                    MessageBox.Show("Magic.DB" + " : Failure was occurred while reading this file. code=" + error.ToString());
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
                    MessageBox.Show("MonGen.txt" + " : Failure was occurred while reading this file. code=" + error.ToString());
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
                    MessageBox.Show("GenMsg.txt" + " : Failure was occurred while reading this file. code=" + error.ToString());
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
                    MessageBox.Show("UnbindList.txt" + " : Failure was occurred while reading this file. code=" + error.ToString());
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
                    MessageBox.Show("DropItemNoticeList.txt" + "Failure was occurred while reading this file. code=" + error.ToString());
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
                    MessageBox.Show("MapQuest.txt" + " : Failure was occurred while reading this file. code=" + error.ToString());
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
                    MessageBox.Show("00Default.txt" + " : Failure was occurred while reading this file. code=" + error.ToString());
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
                    MessageBox.Show("QuestDiary\\*.txt" + " : Failure was occurred while reading this file. code=" + error.ToString());
                    this.Close();
                    return;
                }
                else
                {
                    OutMainMessage("QuestDiary information loaded.");
                }
                if (LoadAbusiveList("!Abuse.txt"))
                {
                    OutMainMessage("!Abuse.txt" + " loaded..");
                }
                if (svMain.LoadLineNotice(svMain.LINENOTICEFILE))
                {
                    OutMainMessage(svMain.LINENOTICEFILE + " loaded..");
                }
                else
                {
                    OutMainMessage(svMain.LINENOTICEFILE + " loading failure !!!!!!!!!");
                }
                if (svMain.LoadLineHelp(svMain.LINEHELPFILE))
                {
                    OutMainMessage(svMain.LINEHELPFILE + " loaded..");
                }
                else
                {
                    OutMainMessage(svMain.LINEHELPFILE + " loading failure !!!!!!!!!");
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
                svMain.GuildMan.LoadGuildList();
                OutMainMessage("GuildList loaded..");
                svMain.GuildAgitMan.LoadGuildAgitList();
                OutMainMessage("GuildAgitList loaded..");
                svMain.GuildAgitBoardMan.LoadAllGaBoardList("");
                OutMainMessage("GuildAgitBoardList loaded..");
                svMain.UserCastle.Initialize();
                svMain.boUserCastleInitialized = true;
                OutMainMessage("Castle initialized..");
                if (svMain.ServerIndex == 0)
                {
                    InterServerMsg.FrmSrvMsg.Initialize();
                }
                else
                {
                    InterMsgClient.FrmMsgClient.Initialize();
                }
                if (DBSQL.g_DBSQL.Connect(svMain.ServerName, ".\\!DBSQL.TXT"))
                {
                    OutMainMessage("DBSQL Connection Success");
                }
                else
                {
                    OutMainMessage("DBSQL Connection Fail [ ERROR!] ");
                }
                if (File.Exists("M2Server.exe"))
                {
                    handle = File.Open("M2Server.exe", (FileMode)FileAccess.Read | FileShare.ReadWrite);
                    if (handle > 0)
                    {
                        FileDate = File.GetCreationTime(handle);
                        DateTime = new DateTime(FileDate);
                        OutMainMessage("FileDateVersion : " + DateTime.ToString());
                        handle.Close();
                    }
                }
                StartServer();
                svMain.ServerReady = true;
                Thread.CurrentThread.Sleep(500);
                ConnectTimer.Enabled = true;
                svMain.runstart = HUtil32.GetTickCount;
                svMain.rcount = 0;
                svMain.humrotatetime = HUtil32.GetTickCount;
                RunTimer.Enabled = true;
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

        public void Timer1Timer(System.Object Sender, System.EventArgs _e1)
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

        public void SaveVariableTimerTimer(System.Object Sender, System.EventArgs _e1)
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
            for (i = 0; i < svMain.GrobalEnvir.Count; i++)
            {
                env = (TEnvirnoment)svMain.GrobalEnvir[i];
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
                svMain.GrobalEnvir.InitEnvirnoments();
                OutMainMessage("GrobalEnvir loaded..");
                MakeStoneMines();
                // 堡籍阑 盲款促.
                OutMainMessage("MakeStoneMines...");
                error = LocalDB.FrmDB.LoadMerchants();
                if (error < 0)
                {
                    MessageBox.Show("merchant.txt" + " : Failure was occurred while reading this file. code=" + error.ToString());
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
                    MessageBox.Show("DecoItem.txt" + " : Failure was occurred while reading this file. code=" + error.ToString());
                    this.Close();
                    return;
                }
                else
                {
                    OutMainMessage("DecoItemList loaded..");
                }
                // 0锅 辑滚俊辑父 佬绢甸烙.
                if (svMain.ServerIndex == 0)
                {
                    // 厘盔操固扁 坷宏璃飘 肺靛(sonmg)
                    // LoadDecoItemList焊促 唱吝俊 角青登绢具 窃.
                    error = svMain.GuildAgitMan.LoadAgitDecoMon();
                    if (error < 0)
                    {
                        MessageBox.Show(svMain.GuildBaseDir + Guild.AGITDECOMONFILE + " : Failure was occurred while reading this file. code=" + error.ToString());
                        this.Close();
                        return;
                    }
                    else
                    {
                        // 厘盔俊 操固扁 坷宏璃飘甫 积己矫挪促.
                        TotalDecoMonCount = svMain.GuildAgitMan.MakeAgitDecoMon();
                        // 厘盔喊 操固扁 坷宏璃飘 俺荐甫 辆钦茄促.
                        svMain.GuildAgitMan.ArrangeEachAgitDecoMonCount();
                        OutMainMessage("AgitDecoMon " + TotalDecoMonCount.ToString() + " loaded...");
                    }
                }
                // ---------------------------------------------
                if (!svMain.BoVentureServer)
                {
                    // 葛氰辑滚俊辑绰 版厚捍捞 绝促.
                    error = LocalDB.FrmDB.LoadGuards();
                    if (error < 0)
                    {
                        MessageBox.Show("Guardlist.txt" + " : Failure was occurred while reading this file. code=" + error.ToString());
                        this.Close();
                        return;
                    }
                }
                error = LocalDB.FrmDB.LoadNpcs();
                if (error < 0)
                {
                    MessageBox.Show("Npc.txt" + " : Failure was occurred while reading this file. code=" + error.ToString());
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
                    MessageBox.Show("MakeItem.txt" + " : Failure was occurred while reading this file. code=" + error.ToString());
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
                    MessageBox.Show("StartPoint.txt" + " : Failure was occurred while reading this file. code=" + error.ToString());
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
                    MessageBox.Show("SafePoint.txt" + " : Failure was occurred while reading this file. code=" + error.ToString());
                    this.Close();
                    return;
                }
                else
                {
                    OutMainMessage("SafePoints loaded..");
                }
                svMain.FrontEngine.Resume();
                OutMainMessage("F-Engine resumed..");
                svMain.UserEngine.Initialize();
                OutMainMessage("U-Engine initialized..");
                svMain.UserMgrEngine.Resume();
                OutMainMessage("UserMgr-Engine resumed..");
                SqlEngn.SqlEngine.Resume();
                OutMainMessage("SQL-Engine resumed..");
            }
            catch
            {
                OutMainMessage("startserver exception..");
            }
        }

        public void ConnectTimerTimer(System.Object Sender, System.EventArgs _e1)
        {
            if (!DBSocket.Active)
            {
                DBSocket.Active = true;
            }
        }

        // --------------- Gate狼 单捞鸥甫 贸府窃 --------------
        public void RunTimerTimer(System.Object Sender, System.EventArgs _e1)
        {
            if (svMain.ServerReady)
            {
                svMain.RunSocket.Run();
                IdSrvClient.FrmIDSoc.DecodeSocStr();
                svMain.UserEngine.ExecuteRun();
                // 困殴惑痢
                SqlEngn.SqlEngine.ExecuteRun();
                svMain.EventMan.Run();
                if (svMain.ServerIndex == 0)
                {
                    // 0锅 辑滚啊 付胶磐啊 等促.
                    InterServerMsg.FrmSrvMsg.Run();
                }
                else
                {
                    InterMsgClient.FrmMsgClient.Run();
                }
            }
            svMain.rcount++;
            if (HUtil32.GetTickCount() - svMain.runstart > 250)
            {
                svMain.runstart = HUtil32.GetTickCount;
                svMain.curruncount = svMain.rcount;
                if (svMain.minruncount > svMain.curruncount)
                {
                    svMain.minruncount = svMain.curruncount;
                }
                svMain.rcount = 0;
            }
        }

        // ------------- Gate Socket 包访 窃荐 ----------------
        public void GateSocketClientConnect(Object Sender, Socket Socket)
        {
            svMain.RunSocket.Connect(Socket);
        }

        public void GateSocketClientDisconnect(Object Sender, Socket Socket)
        {
            svMain.RunSocket.Disconnect(Socket);
        }

        public void GateSocketClientError(Object Sender, Socket Socket, ref int ErrorCode)
        {
            svMain.RunSocket.SocketError(Socket, ref ErrorCode);
        }

        public void GateSocketClientRead(Object Sender, Socket Socket)
        {
            svMain.RunSocket.SocketRead(Socket);
        }

        public void DBSocketConnect(Object Sender, Socket Socket)
        {
        }

        public void DBSocketDisconnect(Object Sender, Socket Socket)
        {
        }

        public void DBSocketError(Object Sender, Socket Socket,  ref int ErrorCode)
        {
            ErrorCode = 0;
            Socket.Close();
        }

        public void DBSocketRead(Object Sender, Socket Socket)
        {
            string data = string.Empty;
            try
            {
                svMain.csSocLock.Enter();
                data = Socket.ReceiveText;
                svMain.RDBSocData = svMain.RDBSocData + data;
                if (!svMain.ReadyDBReceive)
                {
                    svMain.RDBSocData = "";
                }
            }
            finally
            {
                svMain.csSocLock.Leave();
            }
            svMain.UserMgrEngine.OnDBRead(data);
        }

        public void FormCloseQuery(System.Object Sender, System.ComponentModel.CancelEventArgs _e1)
        {
            //if (!svMain.ServerClosing)
            //{
            //    CanClose = false;
            //    if (MessageBox.Show("exit server ? (yes=exit)", Application.ProductName, new object[] { MessageBoxButtons.YesNo | MessageBoxButtons.YesNo | MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            //    {
            //        svMain.ServerClosing = true;
            //        TCloseTimer.Enabled = true;
            //        svMain.RunSocket.CloseAllGate();
            //    }
            //}
        }

        public void TCloseTimerTimer(System.Object Sender, System.EventArgs _e1)
        {
            if ((svMain.UserEngine.GetRealUserCount() == 0) && svMain.FrontEngine.IsFinished())
            {
                this.Close();
            }
        }

        public void Panel1DblClick(System.Object Sender, System.EventArgs _e1)
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

        public void SpeedButton1Click(System.Object Sender, System.EventArgs _e1)
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

        public void SpeedButton2Click(System.Object Sender, System.EventArgs _e1)
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

namespace GameSvr
{
    public class svMain
    {
        public static TFrmMain FrmMain = null;
        public static TRunSocket RunSocket = null;
        public static TFrontEngine FrontEngine = null;
        public static TUserEngine UserEngine = null;
        public static TUserMgrEngine UserMgrEngine = null;
        public static TEnvirList GrobalEnvir = null;
        public static ItemUnitSystem ItemMan = null;
        public static TMagicManager MagicMan = null;
        public static TNoticeManager NoticeMan = null;
        public static TGuildManager GuildMan = null;
        public static TGuildAgitManager GuildAgitMan = null;
        // 巩颇厘盔(sonmg)
        public static TGuildAgitBoardManager GuildAgitBoardMan = null;
        // 厘盔霸矫魄(sonmg)
        public static int GuildAgitStartNumber = 0;
        // 巩颇厘盔 矫累锅龋(MapInfo俊辑 佬绢咳).
        public static int GuildAgitMaxNumber = 0;
        // 巩颇厘盔 弥措俺荐(MapInfo俊辑 佬绢咳).
        public static TEventManager EventMan = null;
        public static TUserCastle UserCastle = null;
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
        public static ArrayList UnbindItemList = null;
        public static ArrayList LineNoticeList = null;
        public static ArrayList LineHelpList = null;
        public static ArrayList QuestDiaryList = null;
        public static TMerchant DefaultNpc = null;
        public static ArrayList DropItemNoticeList = null;
        public static ArrayList ShopItemList = null;
        public static ArrayList EventItemList = null;
        public static int EventItemGifeBaseNumber = 0;
        public static int[] GrobalQuestParams = new int[9 + 1];
        public static string ErrorLogFile = String.Empty;
        // 固福狼 矫埃... 泅角 矫埃狼 2硅 狐抚
        public static int ServerIndex = 0;
        public static string ServerName = String.Empty;
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
        public static string SysMission_Map = String.Empty;
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
        // user engine thread
        public static object usIMLock = null;
        // user engine thread
        public static object ruLock = null;
        // run sock
        public static object ruSendLock = null;
        // run sock
        public static object ruCloseLock = null;
        // run sock
        public static object socstrLock = null;
        public static object fuLock = null;
        // front engine thread
        public static object fuOpenLock = null;
        // front engine thread
        public static object fuCloseLock = null;
        // front engine thread
        public static object humanLock = null;
        // human sendbufer
        public static object umLock = null;
        // User Manager engine thread

        // SQL Engine Thread
        public static ArrayList MainMsg = null;
        public static ArrayList UserLogs = null;
        // 敲贰捞绢狼 青悼 肺弊
        public static ArrayList UserConLogs = null;
        // 立加 肺弊
        public static ArrayList UserChatLog = null;
        // 盲泼 肺弊
        public static bool DiscountForNightTime = false;
        public static int HalfFeeStart = 0;
        // 且牢矫埃 矫累
        public static int HalfFeeEnd = 0;
        // 且牢矫埃 场
        public static bool ServerReady = false;
        // 辑滚啊 荤侩磊甫 罐阑 霖厚啊 登菌绰啊?
        public static bool ServerClosing = false;
        public static int FCertify = 0;
        public static int FItemNumber = 0;
        public static string RDBSocData = String.Empty;
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
        public static string MsgServerAddress = String.Empty;
        public static int MsgServerPort = 0;
        public static string LogServerAddress = String.Empty;
        public static int LogServerPort = 0;
        public static string ShareBaseDir = String.Empty;
        public static string ShareBaseDirCopy = String.Empty;
        public static string ShareVentureDir = String.Empty;
        public static int ShareFileNameNum = 0;
        public static string ConLogBaseDir = String.Empty;
        // 立加 矫埃 肺弊
        public static string ChatLogBaseDir = String.Empty;
        // 立加 矫埃 肺弊
        public static string DefHomeMap = String.Empty;
        // 阿 辑滚付促 怖 乐绢具 窍绰 甘
        public static short DefHomeX = 0;
        public static short DefHomeY = 0;
        public static string GuildDir = String.Empty;
        public static string GuildFile = String.Empty;
        public static string GuildBaseDir = String.Empty;
        public static string GuildAgitFile = String.Empty;
        public static string CastleDir = String.Empty;
        public static string EnvirDir = String.Empty;
        public static string MapDir = String.Empty;
        public static int CurrentMonthlyCard = 0;
        // 岿沥咀 荤侩磊 荐
        public static int TotalTimeCardUsage = 0;
        // 矫埃力 墨靛 荤侩磊狼 荤侩 醚 矫埃 //矫埃
        public static int LastMonthTotalTimeCardUsage = 0;
        // 矫埃
        public static int GrossTimeCardUsage = 0;
        // 矫埃
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
        // 捞抚甸
        public static string __ClothsForMan = String.Empty;
        public static string __ClothsForWoman = String.Empty;
        public static string __WoodenSword = String.Empty;
        public static string __Candle = String.Empty;
        public static string __BasicDrug = String.Empty;
        public static string __GoldStone = String.Empty;
        public static string __SilverStone = String.Empty;
        public static string __SteelStone = String.Empty;
        public static string __CopperStone = String.Empty;
        public static string __BlackStone = String.Empty;
        public static string __Gem1Stone = String.Empty;
        public static string __Gem2Stone = String.Empty;
        public static string __Gem3Stone = String.Empty;
        public static string __Gem4Stone = String.Empty;
        public static string __ZumaMonster1 = String.Empty;
        public static string __ZumaMonster2 = String.Empty;
        public static string __ZumaMonster3 = String.Empty;
        public static string __ZumaMonster4 = String.Empty;
        public static string __Bee = String.Empty;
        public static string __Spider = String.Empty;
        public static string __WhiteSkeleton = String.Empty;
        public static string __ShinSu = String.Empty;
        public static string __ShinSu1 = String.Empty;
        public static string __AngelMob = String.Empty;
        public static string __CloneMob = String.Empty;
        public static string __WomaHorn = String.Empty;
        public static string __ZumaPiece = String.Empty;
        public static string __GoldenImugi = String.Empty;
        public static string __WhiteSnake = String.Empty;
        public static string ClientFileName1 = String.Empty;
        public static string ClientFileName2 = String.Empty;
        public static string ClientFileName3 = String.Empty;
        public static int ClientCheckSumValue1 = 0;
        public static int ClientCheckSumValue2 = 0;
        public static int ClientCheckSumValue3 = 0;
        // 叼滚彪 沥焊
        public static int gErrorCount = 0;
        // 泅犁 积己吝牢 Merchant Index
        public static int CurrentMerchantIndex = 0;
        public static long ServerTickDifference = 0;
        // 酒捞袍 牢郸胶 瘤沥
        public static int INDEX_CHOCOLATE = 0;
        // 檬妮房
        public static int INDEX_CANDY = 0;
        // 荤帕
        public static int INDEX_LOLLIPOP = 0;
        // 阜措荤帕
        public static int INDEX_MIRBOOTS = 0;
        public const bool ENGLISHVERSION = true;
        // FALSE;
        public const bool PHILIPPINEVERSION = false;
        public const bool CHINAVERSION = false;
        public const bool TAIWANVERSION = false;
        public const bool KOREANVERSION = false;
        public const int SENDBLOCK = 1024;
        // 2048;  //霸捞飘客 烹脚窍扁 锭巩俊 喉钒捞 农促.
        public const int SENDCHECKBLOCK = 4096;
        // 2048;      //某农 脚龋甫 焊辰促.
        public const int SENDAVAILABLEBLOCK = 7999;
        // 4096;  //某农 脚龋啊 绝绢档 捞沥档绰 焊辰促.
        public const int GATELOAD = 10;
        // 10KB
        public const string LINENOTICEFILE = "Notice\\LineNotice.txt";
        public const string LINEHELPFILE = "LineHelp.txt";
        public const int BUILDGUILDFEE = 1000000;
        // 玫锋脚青焊
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
            string pbuf;
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
            //    MessageBox.Show("File not found... <!servertable.txt>");
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
                if (((int)UnbindItemList.Values[i]) == shape)
                {
                    result = (string)UnbindItemList[i];
                    break;
                }
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

        // 敲贰捞绢狼 青悼阑 扁废
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

        // 立加 扁废阑 肺弊肺 巢辫
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

        // ------------- DB Socket 包访 窃荐 ----------------
        public static bool DBConnected()
        {
            bool result;
            if (FrmMain.DBSocket.Active)
            {
                result = FrmMain.DBSocket.Socket.Connected;
            }
            else
            {
                result = false;
            }
            return result;
        }
    }
}