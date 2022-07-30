using System.Collections;
using System.Threading;
using SystemModule;

namespace GameSvr
{
    public class TSqlLoadRecord
    {
        public int loadType;
        public string UserName;
        public object pRcd;
    }

    public class TSQLEngine
    {
        private readonly ArrayList SqlToDBList = null;
        private readonly ArrayList DbToGameList = null;
        private bool FActive = false;
        public object SQLock = null;

        public TSQLEngine()
        {
            SQLock = new object();
            SqlToDBList = new ArrayList();
            DbToGameList = new ArrayList();
            FActive = true;
            SqlEngn.g_UMDEBUG = 0;
        }

        public void Open(bool WantOpen)
        {
            try
            {
                Monitor.Enter(SQLock);
                FActive = WantOpen;
            }
            finally
            {
                Monitor.Exit(SQLock);
            }
        }

        public void ExecuteSaveCommand()
        {
            
        }

        public int ExecuteLoadCommand()
        {
            int result;
            TSqlLoadRecord pload;
            bool bExit;
            TSearchSellItem pSearchInfo;
            TMarketLoad pLoadInfo;
            ArrayList rInfoList;
            int SqlResult;
            TSearchGaBoardList pSearchGaBoardList;
            TGaBoardArticleLoad pArticleLoad;
            long loadtime;
            int loadtype;
            result = 0;
            bExit = false;
            pload = null;
            while (!bExit)
            {
                result = 1;
                // bug result
                try
                {
                    if (!DBSQL.g_DBSQL.Connected())
                    {
                        DBSQL.g_DBSQL.ReConnect();
                        continue;
                    }
                }
                catch
                {
                    svMain.MainOutMessage("[Exception]ExecuteLoadCommand - g_DBSQL.Connected");
                }
                result = 2;
                // bug result
                // 疙飞绢 府胶飘 掘扁... 静饭靛 林狼 ...
                try
                {
                    SQLock.Enter();
                    if (SqlToDBList != null)
                    {
                        if (SqlToDBList.Count > 0)
                        {
                            if (SqlToDBList[0] == null)
                            {
                                svMain.MainOutMessage("SQLToDBList.Items[0] = nil" + " [" + SqlEngn.g_UMDEBUG.ToString() + "]");
                            }
                            pload = (TSqlLoadRecord)SqlToDBList[0];
                            SqlToDBList.RemoveAt(0);
                            if (SqlEngn.g_UMDEBUG == 1000)
                            {
                                svMain.MainOutMessage("SQLToDBList.Delete(0) count:" + SqlToDBList.Count.ToString() + " [" + SqlEngn.g_UMDEBUG.ToString() + "]");
                                SqlEngn.g_UMDEBUG = 6;
                                bExit = false;
                            }
                            else
                            {
                                SqlEngn.g_UMDEBUG = 2;
                                bExit = false;
                            }
                        }
                        else
                        {
                            // {debug code}MainOutMessage('not SQLToDBList.count > 0');
                            SqlEngn.g_UMDEBUG = 3;
                            pload = null;
                            bExit = true;
                        }
                    }
                    else
                    {
                        if (SqlEngn.g_UMDEBUG == 1000)
                        {
                            svMain.MainOutMessage("not SQLToDBList <> nil" + " [" + SqlEngn.g_UMDEBUG.ToString() + "]");
                        }
                        SqlEngn.g_UMDEBUG = 4;
                        pload = null;
                        bExit = true;
                    }
                }
                finally
                {
                    SQLock.Leave();
                }
                result = 3;
                if (pload != null)
                {
                    loadtime = HUtil32.GetTickCount();
                    loadtype = pload.loadType;
                    if ((SqlEngn.g_UMDEBUG > 0) && (SqlEngn.g_UMDEBUG != 2))
                    {
                        svMain.MainOutMessage("[TestCode]ExecuteLoadCommand LoadType : " + loadtype.ToString() + " [" + SqlEngn.g_UMDEBUG.ToString() + "]");
                    }
                    SqlEngn.g_UMDEBUG = 5;
                    result = 30000 + loadtype;
                    switch (pload.loadType)
                    {
                        case SqlEngn.LOADTYPE_REQGETLIST:
                            SqlEngn.g_UMDEBUG = 11;
                            result = 4;
                            pSearchInfo = (TSearchSellItem)pload.pRcd;
                            SqlEngn.g_UMDEBUG = 12;
                            if (pSearchInfo != null)
                            {
                                rInfoList = new ArrayList();
                                SqlEngn.g_UMDEBUG = 21;
                                if (DBSQL.g_DBSQL == null)
                                {
                                    svMain.MainOutMessage("[Exception] g_DBSql = nil");
                                }
                                SqlResult = DBSQL.g_DBSQL.LoadPageUserMarket(pSearchInfo.MarketName, pSearchInfo.Who, pSearchInfo.ItemName, pSearchInfo.ItemType, pSearchInfo.ItemSet, rInfoList);
                                if (rInfoList == null)
                                {
                                    svMain.MainOutMessage("[Exception] rInfoList = nil");
                                }
                                SqlEngn.g_UMDEBUG = 22;
                                pSearchInfo.IsOK = SqlResult;
                                pSearchInfo.pList = rInfoList;
                                SqlEngn.g_UMDEBUG = 23;
                                rInfoList = null;
                                SqlEngn.g_UMDEBUG = 24;
                                pload.loadType = SqlEngn.LOADTYPE_GETLIST;
                                SqlEngn.g_UMDEBUG = 13;
                                AddToGameList(pload);
                                pSearchInfo = null;
                                pload = null;
                                SqlEngn.g_UMDEBUG = 14;
                            }
                            else
                            {
                                if (SqlEngn.g_UMDEBUG > 0)
                                {
                                    svMain.MainOutMessage("[TestCode]ExecuteLoadCommand : pSearchInfo = nil" + loadtype.ToString() + " [" + SqlEngn.g_UMDEBUG.ToString() + "]");
                                }
                                SqlEngn.g_UMDEBUG = 15;
                            }
                            break;
                        case SqlEngn.LOADTYPE_REQBUYITEM:
                            result = 5;
                            SqlEngn.g_UMDEBUG = 25;
                            pLoadInfo = (TMarketLoad)pload.pRcd;
                            if (pLoadInfo != null)
                            {
                                SqlResult = DBSQL.g_DBSQL.BuyOneUserMarket(ref pLoadInfo);
                                pLoadInfo.IsOK = SqlResult;
                                pload.loadType = SqlEngn.LOADTYPE_BUYITEM;
                                AddToGameList(pload);
                                pLoadInfo = null;
                                pload = null;
                            }
                            break;
                        case SqlEngn.LOADTYPE_REQSELLITEM:
                            result = 6;
                            SqlEngn.g_UMDEBUG = 16;
                            pLoadInfo = (TMarketLoad)pload.pRcd;
                            if (pLoadInfo != null)
                            {
                                SqlEngn.g_UMDEBUG = 17;
                                SqlResult = DBSQL.g_DBSQL.AddSellUserMarket(pLoadInfo);
                                pLoadInfo.IsOK = SqlResult;
                                pload.loadType = SqlEngn.LOADTYPE_SELLITEM;
                                AddToGameList(pload);
                                pLoadInfo = null;
                                pload = null;
                                SqlEngn.g_UMDEBUG = 18;
                            }
                            else
                            {
                                SqlEngn.g_UMDEBUG = 19;
                            }
                            break;
                        case SqlEngn.LOADTYPE_REQREADYTOSELL:
                            result = 7;
                            SqlEngn.g_UMDEBUG = 26;
                            pLoadInfo = (TMarketLoad)pload.pRcd;
                            SqlEngn.g_UMDEBUG = 30;
                            if (pLoadInfo != null)
                            {
                                SqlResult = DBSQL.g_DBSQL.ReadyToSell(ref pLoadInfo);
                                pLoadInfo.IsOK = SqlResult;
                                pload.loadType = SqlEngn.LOADTYPE_READYTOSELL;
                                AddToGameList(pload);
                                pLoadInfo = null;
                                pload = null;
                            }
                            SqlEngn.g_UMDEBUG = 31;
                            break;
                        case SqlEngn.LOADTYPE_REQCANCELITEM:
                            result = 8;
                            SqlEngn.g_UMDEBUG = 27;
                            pLoadInfo = (TMarketLoad)pload.pRcd;
                            if (pLoadInfo != null)
                            {
                                SqlResult = DBSQL.g_DBSQL.CancelUserMarket(ref pLoadInfo);
                                pLoadInfo.IsOK = SqlResult;
                                pload.loadType = SqlEngn.LOADTYPE_CANCELITEM;
                                AddToGameList(pload);
                                pLoadInfo = null;
                                pload = null;
                            }
                            break;
                        case SqlEngn.LOADTYPE_REQGETPAYITEM:
                            result = 9;
                            SqlEngn.g_UMDEBUG = 28;
                            pLoadInfo = (TMarketLoad)pload.pRcd;
                            if (pLoadInfo != null)
                            {
                                SqlResult = DBSQL.g_DBSQL.GetPayUserMarket(ref pLoadInfo);
                                pLoadInfo.IsOK = SqlResult;
                                pload.loadType = SqlEngn.LOADTYPE_GETPAYITEM;
                                AddToGameList(pload);
                                pLoadInfo = null;
                                pload = null;
                            }
                            break;
                        case SqlEngn.LOADTYPE_REQCHECKTODB:
                            result = 10;
                            SqlEngn.g_UMDEBUG = 29;
                            pSearchInfo = (TSearchSellItem)pload.pRcd;
                            if (pSearchInfo != null)
                            {
                                switch (pSearchInfo.CheckType)
                                {
                                    case Grobal2.MARKET_CHECKTYPE_SELLOK:
                                        DBSQL.g_DBSQL.ChkAddSellUserMarket(pSearchInfo, true);
                                        break;
                                    case Grobal2.MARKET_CHECKTYPE_SELLFAIL:
                                        DBSQL.g_DBSQL.ChkAddSellUserMarket(pSearchInfo, false);
                                        break;
                                    case Grobal2.MARKET_CHECKTYPE_BUYOK:
                                        DBSQL.g_DBSQL.ChkBuyOneUserMarket(pSearchInfo, true);
                                        break;
                                    case Grobal2.MARKET_CHECKTYPE_BUYFAIL:
                                        DBSQL.g_DBSQL.ChkBuyOneUserMarket(pSearchInfo, false);
                                        break;
                                    case Grobal2.MARKET_CHECKTYPE_CANCELOK:
                                        DBSQL.g_DBSQL.ChkCancelUserMarket(pSearchInfo, true);
                                        break;
                                    case Grobal2.MARKET_CHECKTYPE_CANCELFAIL:
                                        DBSQL.g_DBSQL.ChkCancelUserMarket(pSearchInfo, false);
                                        break;
                                    case Grobal2.MARKET_CHECKTYPE_GETPAYOK:
                                        DBSQL.g_DBSQL.ChkGetPayUserMarket(pSearchInfo, true);
                                        break;
                                    case Grobal2.MARKET_CHECKTYPE_GETPAYFAIL:
                                        DBSQL.g_DBSQL.ChkGetPayUserMarket(pSearchInfo, false);
                                        break;
                                }
                                //FreeMem(pSearchInfo);
                                pSearchInfo = null;
                            }
                            break;
                        case SqlEngn.GABOARD_REQGETLIST:
                            result = 11;
                            pSearchGaBoardList = (TSearchGaBoardList)pload.pRcd;
                            if (pSearchGaBoardList != null)
                            {
                                rInfoList = new ArrayList();
                                SqlResult = DBSQL.g_DBSQL.LoadPageGaBoardList(pSearchGaBoardList.GuildName, pSearchGaBoardList.Kind, rInfoList);
                                pSearchGaBoardList.ArticleList = rInfoList;
                                rInfoList = null;
                                pload.loadType = SqlEngn.GABOARD_GETLIST;
                                AddToGameList(pload);
                                pSearchGaBoardList = null;
                                pload = null;
                            }
                            break;
                        case SqlEngn.GABOARD_REQADDARTICLE:
                            result = 12;
                            pArticleLoad = (TGaBoardArticleLoad)pload.pRcd;
                            pArticleLoad.UserName = pload.UserName;
                            if (pArticleLoad != null)
                            {
                                SqlResult = DBSQL.g_DBSQL.AddGaBoardArticle(pArticleLoad);
                                pload.loadType = SqlEngn.GABOARD_ADDARTICLE;
                                AddToGameList(pload);
                                pArticleLoad = null;
                                pload = null;
                            }
                            break;
                        case SqlEngn.GABOARD_REQDELARTICLE:
                            result = 13;
                            pArticleLoad = (TGaBoardArticleLoad)pload.pRcd;
                            pArticleLoad.UserName = pload.UserName;
                            if (pArticleLoad != null)
                            {
                                SqlResult = DBSQL.g_DBSQL.DelGaBoardArticle(pArticleLoad);
                                pload.loadType = SqlEngn.GABOARD_DELARTICLE;
                                AddToGameList(pload);
                                pArticleLoad = null;
                                pload = null;
                            }
                            break;
                        case SqlEngn.GABOARD_REQEDITARTICLE:
                            result = 14;
                            pArticleLoad = (TGaBoardArticleLoad)pload.pRcd;
                            pArticleLoad.UserName = pload.UserName;
                            if (pArticleLoad != null)
                            {
                                SqlResult = DBSQL.g_DBSQL.EditGaBoardArticle(pArticleLoad);
                                pload.loadType = SqlEngn.GABOARD_EDITARTICLE;
                                AddToGameList(pload);
                                pArticleLoad = null;
                                pload = null;
                            }
                            break;
                        default:
                            result = 170000 + loadtype;
                            if (SqlEngn.g_UMDEBUG > 0)
                            {
                                svMain.MainOutMessage("[TestCode]ExecuteLoadCommand : case else LoadType" + loadtype.ToString() + " [" + SqlEngn.g_UMDEBUG.ToString() + "]");
                            }
                            SqlEngn.g_UMDEBUG = 20;
                            break;
                    }
                    result = 15;
                    if (pload != null)
                    {
                        result = 16;
                        dispose(pload);
                        pload = null;
                    }
                }
            }
            return result;
        }

        public void Execute()
        {
            int buginfo = 0;
            while (true)
            {
                try
                {
                    ExecuteSaveCommand();
                }
                catch
                {
                    svMain.MainOutMessage("EXCEPTION SQLEngine.ExecuteSaveCommand");
                }
                try
                {
                    buginfo = ExecuteLoadCommand();
                }
                catch
                {
                    svMain.MainOutMessage("EXCEPTION SQLEngine.ExecuteLoadCommand" + buginfo.ToString() + " [" + SqlEngn.g_UMDEBUG.ToString() + "]");
                    if (buginfo == 3)
                    {
                        SqlEngn.g_UMDEBUG = 1000;
                    }
                }
                //this.Sleep(1);
                //if (this.Terminated)
                //{
                //    return;
                //}
            }
        }

        private void AddToDBList(TSqlLoadRecord pInfo)
        {
            if (pInfo == null)
            {
                return;
            }
            try
            {
                SQLock.Enter();
                SqlToDBList.Add(pInfo);
            }
            finally
            {
                SQLock.Leave();
            }
        }

        public bool RequestLoadPageUserMarket(TMarKetReqInfo ReqInfo_)
        {
            bool result;
            TSqlLoadRecord pload;
            bool flag;
            result = false;
            try
            {
                SQLock.Enter();
                flag = FActive;
            }
            finally
            {
                SQLock.Leave();
            }
            if (!flag)
            {
                return result;
            }
            pload = new TSqlLoadRecord();
            pload.loadType = SqlEngn.LOADTYPE_REQGETLIST;
            pload.UserName = ReqInfo_.UserName;
            //GetMem(pload.pRcd, sizeof(TSearchSellItem));
            ((TSearchSellItem)pload.pRcd).MarketName = ReqInfo_.MarketName;
            ((TSearchSellItem)pload.pRcd).Who = ReqInfo_.SearchWho;
            ((TSearchSellItem)pload.pRcd).ItemName = ReqInfo_.SearchItem;
            ((TSearchSellItem)pload.pRcd).ItemType = ReqInfo_.ItemType;
            ((TSearchSellItem)pload.pRcd).ItemSet = ReqInfo_.ItemSet;
            ((TSearchSellItem)pload.pRcd).UserMode = ReqInfo_.UserMode;
            if (pload == null)
            {
                return result;
            }
            AddToDBList(pload);
            if (SqlEngn.g_UMDEBUG == 1000)
            {
                svMain.MainOutMessage("RequestLoadPageUserMarket-AddToDBList" + " [" + SqlEngn.g_UMDEBUG.ToString() + "]");
            }
            SqlEngn.g_UMDEBUG = 1;
            result = true;
            return result;
        }

        public bool RequestReadyToSellUserMarket(string UserName, string MarketName, string sellwho)
        {
            TSqlLoadRecord pload;
            bool result = false;
            if (!FActive)
            {
                return result;
            }
            pload = new TSqlLoadRecord();
            pload.loadType = SqlEngn.LOADTYPE_REQREADYTOSELL;
            pload.UserName = UserName;
            ((TMarketLoad)pload.pRcd).MarketName = MarketName;
            ((TMarketLoad)pload.pRcd).SellWho = sellwho;
            if (pload == null)
            {
                return result;
            }
            AddToDBList(pload);
            result = true;
            return result;
        }

        public bool RequestBuyItemUserMarket(string UserName, string MarketName, string BuyWho, int SellIndex)
        {
            bool result = false;
            if (!FActive)
            {
                return result;
            }
            TSqlLoadRecord pload = new TSqlLoadRecord();
            pload.loadType = SqlEngn.LOADTYPE_REQBUYITEM;
            pload.UserName = UserName;
            //GetMem(pload.pRcd, sizeof(TMarketLoad));
            ((TMarketLoad)pload.pRcd).MarketName = MarketName;
            ((TMarketLoad)pload.pRcd).SellWho = BuyWho;
            ((TMarketLoad)pload.pRcd).Index = SellIndex;
            if (pload == null)
            {
                return result;
            }
            AddToDBList(pload);
            result = true;
            return result;
        }

        public void CheckToDB(string UserName, string Marketname, string SellWho, int MakeIndex_, int SellIndex, int CheckType)
        {
            TSqlLoadRecord pload;
            if (!FActive)
            {
                svMain.MainOutMessage("[TestCode2] TSqlEngine.CheckToDB FActive is FALSE");
            }
            pload = new TSqlLoadRecord();
            pload.loadType = SqlEngn.LOADTYPE_REQCHECKTODB;
            pload.UserName = UserName;
            //GetMem(pload.pRcd, sizeof(TSearchSellItem));
            ((TSearchSellItem)pload.pRcd).CheckType = CheckType;
            ((TSearchSellItem)pload.pRcd).MarketName = Marketname;
            ((TSearchSellItem)pload.pRcd).Who = SellWho;
            ((TSearchSellItem)pload.pRcd).MakeIndex = MakeIndex_;
            ((TSearchSellItem)pload.pRcd).SellIndex = SellIndex;
            if (pload == null)
            {
                return;
            }
            AddToDBList(pload);
        }

        public bool RequestSellItemUserMarket(string UserName, TMarketLoad pselladd)
        {
            bool result = false;
            if (!FActive)
            {
                return result;
            }
            TSqlLoadRecord pload = new TSqlLoadRecord();
            pload.loadType = SqlEngn.LOADTYPE_REQSELLITEM;
            pload.UserName = UserName;
            //GetMem(pload.pRcd, sizeof(TMarketLoad));
            //Move(pselladd, pload.pRcd, sizeof(TMarketLoad));
            if (pload == null)
            {
                return result;
            }
            AddToDBList(pload);
            result = true;
            return result;
        }

        public bool RequestGetPayUserMarket(string UserName, string MarketName, string sellwho, int sellindex)
        {
            bool result = false;
            if (!FActive)
            {
                return result;
            }
            TSqlLoadRecord pload = new TSqlLoadRecord();
            pload.loadType = SqlEngn.LOADTYPE_REQGETPAYITEM;
            pload.UserName = UserName;
            //GetMem(pload.pRcd, sizeof(TMarketLoad));
            ((TMarketLoad)pload.pRcd).MarketName = MarketName;
            ((TMarketLoad)pload.pRcd).SellWho = sellwho;
            ((TMarketLoad)pload.pRcd).Index = sellindex;
            if (pload == null)
            {
                return result;
            }
            AddToDBList(pload);
            result = true;
            return result;
        }

        public bool RequestCancelSellUserMarket(string UserName, string MarketName, string sellwho, int sellindex)
        {
            bool result;
            TSqlLoadRecord pload;
            result = false;
            if (!FActive)
            {
                return result;
            }
            pload = new TSqlLoadRecord();
            pload.loadType = SqlEngn.LOADTYPE_REQCANCELITEM;
            pload.UserName = UserName;
            // 措脚 敬促.
            ((TMarketLoad)pload.pRcd).MarketName = MarketName;
            ((TMarketLoad)pload.pRcd).SellWho = sellwho;
            ((TMarketLoad)pload.pRcd).Index = sellindex;
            // debug code
            if (pload == null)
            {
                return result;
            }
            AddToDBList(pload);
            result = true;
            return result;
        }

        private void AddToGameList(TSqlLoadRecord pInfo)
        {
            if (pInfo == null)
            {
                return;
            }
            try
            {
                SQLock.Enter();
                DbToGameList.Add(pInfo);
            }
            finally
            {
                SQLock.Leave();
            }
        }

        private TSqlLoadRecord GetGameExecuteData()
        {
            TSqlLoadRecord result = null;
            try
            {
                SQLock.Enter();
                if (DbToGameList != null)
                {
                    if (DbToGameList.Count > 0)
                    {
                        result = (TSqlLoadRecord)DbToGameList[0];
                        DbToGameList.RemoveAt(0);
                    }
                }
            }
            finally
            {
                SQLock.Leave();
            }
            return result;
        }

        public void ExecuteRun()
        {
            TSqlLoadRecord pLoad;
            TSearchSellItem pSearchInfo;
            TMarketLoad pLoadInfo;
            TUserHuman hum;
            int i;
            TSearchGaBoardList pBoardListInfo;
            TGaBoardArticleLoad pArticleInfo;
            try
            {
                pLoad = GetGameExecuteData();
                if (pLoad != null)
                {
                    switch (pLoad.loadType)
                    {
                        case SqlEngn.LOADTYPE_GETLIST:
                            pSearchInfo = (TSearchSellItem)pLoad.pRcd;
                            if (pSearchInfo != null)
                            {
                                hum = svMain.UserEngine.GetUserHuman(pLoad.UserName);
                                if (hum != null)
                                {
                                    hum.GetMarketData(pSearchInfo);
                                    hum.SendUserMarketList(0);
                                }
                                else
                                {
                                    svMain.MainOutMessage("INFO SQLENGINE DO NOT FIND USER FOR MARKETLIST!");
                                }
                                if (pSearchInfo.pList != null)
                                {
                                    for (i = pSearchInfo.pList.Count - 1; i >= 0; i--)
                                    {
                                        if (pSearchInfo.pList[0] != null)
                                        {
                                            dispose(pSearchInfo.pList[0]);
                                        }
                                        pSearchInfo.pList.RemoveAt(0);
                                    }
                                    pSearchInfo.pList.Free();
                                    pSearchInfo.pList = null;
                                }
                            }
                            break;
                        case SqlEngn.LOADTYPE_SELLITEM:
                            pLoadInfo = (TMarketLoad)pLoad.pRcd;
                            if (pLoadInfo != null)
                            {
                                hum = svMain.UserEngine.GetUserHuman(pLoad.UserName);
                                if (hum != null)
                                {
                                    hum.SellUserMarket(pLoadInfo);
                                }
                                else
                                {
                                    svMain.MainOutMessage("INFO SQLENGINE DO NOT FIND USER FOR SELLITEM!");
                                }
                            }
                            break;
                        case SqlEngn.LOADTYPE_READYTOSELL:
                            pLoadInfo = (TMarketLoad)pLoad.pRcd;
                            if (pLoadInfo != null)
                            {
                                hum = svMain.UserEngine.GetUserHuman(pLoad.UserName);
                                if (hum != null)
                                {
                                    hum.ReadyToSellUserMarket(pLoadInfo);
                                }
                                else
                                {
                                    svMain.MainOutMessage("INFO SQLENGINE DO NOT FIND USER FOR SELLITEM!");
                                }
                            }
                            break;
                        case SqlEngn.LOADTYPE_BUYITEM:
                            pLoadInfo = (TMarketLoad)pLoad.pRcd;
                            if (pLoadInfo != null)
                            {
                                hum = svMain.UserEngine.GetUserHuman(pLoad.UserName);
                                if (hum != null)
                                {
                                    hum.BuyUserMarket(pLoadInfo);
                                }
                                else
                                {
                                    svMain.MainOutMessage("INFO SQLENGINE DO NOT FIND USER FOR SELLITEM!");
                                }
                            }
                            break;
                        case SqlEngn.LOADTYPE_CANCELITEM:
                            pLoadInfo = (TMarketLoad)pLoad.pRcd;
                            if (pLoadInfo != null)
                            {
                                hum = svMain.UserEngine.GetUserHuman(pLoad.UserName);
                                if (hum != null)
                                {
                                    hum.CancelUserMarket(pLoadInfo);
                                }
                                else
                                {
                                    svMain.MainOutMessage("INFO SQLENGINE DO NOT FIND USER FOR CANCEL!");
                                }
                            }
                            break;
                        case SqlEngn.LOADTYPE_GETPAYITEM:
                            pLoadInfo = (TMarketLoad)pLoad.pRcd;
                            if (pLoadInfo != null)
                            {
                                hum = svMain.UserEngine.GetUserHuman(pLoad.UserName);
                                if (hum != null)
                                {
                                    hum.GetPayUserMarket(pLoadInfo);
                                }
                                else
                                {
                                    svMain.MainOutMessage("INFO SQLENGINE DO NOT FIND USER FOR GETPAY!");
                                }
                            }
                            break;
                        case SqlEngn.GABOARD_GETLIST:
                            pBoardListInfo = (TSearchGaBoardList)pLoad.pRcd;
                            if (pBoardListInfo != null)
                            {
                                if (pBoardListInfo.GuildName != "")
                                {
                                    svMain.GuildAgitBoardMan.AddGaBoardList(pBoardListInfo);
                                    hum = svMain.UserEngine.GetUserHuman(pBoardListInfo.UserName);
                                    if (hum != null)
                                    {
                                        hum.CmdReloadGaBoardList(pBoardListInfo.GuildName, 1);
                                    }
                                }
                                if (pBoardListInfo.ArticleList != null)
                                {
                                    for (i = pBoardListInfo.ArticleList.Count - 1; i >= 0; i--)
                                    {
                                        if (pBoardListInfo.ArticleList[0] != null)
                                        {
                                            dispose(pBoardListInfo.ArticleList[0]);
                                        }
                                        pBoardListInfo.ArticleList.RemoveAt(0);
                                    }
                                    pBoardListInfo.ArticleList.Free();
                                    pBoardListInfo.ArticleList = null;
                                }
                            }
                            break;
                        case SqlEngn.GABOARD_ADDARTICLE:
                            pArticleInfo = (TGaBoardArticleLoad)pLoad.pRcd;
                            if (pArticleInfo != null)
                            {
                                // GuildAgitBoardMan.LoadAllGaBoardList( pArticleInfo.UserName );
                            }
                            break;
                        case SqlEngn.GABOARD_DELARTICLE:
                            pArticleInfo = (TGaBoardArticleLoad)pLoad.pRcd;
                            if (pArticleInfo != null)
                            {
                                // GuildAgitBoardMan.LoadAllGaBoardList( pArticleInfo.UserName );
                            }
                            break;
                        case SqlEngn.GABOARD_EDITARTICLE:
                            pArticleInfo = (TGaBoardArticleLoad)pLoad.pRcd;
                            if (pArticleInfo != null)
                            {
                                // GuildAgitBoardMan.LoadAllGaBoardList( pArticleInfo.UserName );
                            }
                            break;
                    }
                    if (pLoad.pRcd != null)
                    {
                        //FreeMem(pLoad.pRcd);
                    }
                    dispose(pLoad);
                    pLoad = null;
                }
            }
            catch
            {
                svMain.MainOutMessage("SQLEngnExcept ExecuteRun!");
            }
        }

        public bool RequestLoadGuildAgitBoard(string UserName, string gname)
        {
            TSqlLoadRecord pload;
            var result = false;
            if (!FActive)
            {
                return result;
            }
            pload = new TSqlLoadRecord();
            pload.loadType = SqlEngn.GABOARD_REQGETLIST;
            pload.UserName = UserName;
            //GetMem(pload.pRcd, sizeof(TSearchGaBoardList));
            //((TSearchGaBoardList)(pload.pRcd)).AgitNum = 0;
            //((TSearchGaBoardList)(pload.pRcd)).GuildName = gname;
            //((TSearchGaBoardList)(pload.pRcd)).OrgNum = -1;
            //((TSearchGaBoardList)(pload.pRcd)).SrcNum1 = -1;
            //((TSearchGaBoardList)(pload.pRcd)).SrcNum2 = -1;
            //((TSearchGaBoardList)(pload.pRcd)).SrcNum3 = -1;
            //((TSearchGaBoardList)(pload.pRcd)).Kind = SqlEngn.KIND_GENERAL;
            //((TSearchGaBoardList)(pload.pRcd)).UserName = UserName;
            if (pload == null)
            {
                return result;
            }
            AddToDBList(pload);
            result = true;
            return result;
        }

        public bool RequestGuildAgitBoardAddArticle(string gname, int OrgNum, int SrcNum1, int SrcNum2, int SrcNum3, int nKind, int AgitNum, string uname, string data)
        {
            TSqlLoadRecord pload;
            bool result = false;
            if (!FActive)
            {
                return result;
            }
            pload = new TSqlLoadRecord();
            pload.loadType = SqlEngn.GABOARD_REQADDARTICLE;
            pload.UserName = uname;
            //GetMem(pload.pRcd, sizeof(TGaBoardArticleLoad));
            ((TGaBoardArticleLoad)pload.pRcd).AgitNum = AgitNum;
            ((TGaBoardArticleLoad)pload.pRcd).GuildName = gname;
            ((TGaBoardArticleLoad)pload.pRcd).OrgNum = OrgNum;
            ((TGaBoardArticleLoad)pload.pRcd).SrcNum1 = SrcNum1;
            ((TGaBoardArticleLoad)pload.pRcd).SrcNum2 = SrcNum2;
            ((TGaBoardArticleLoad)pload.pRcd).SrcNum3 = SrcNum3;
            ((TGaBoardArticleLoad)pload.pRcd).Kind = nKind;
            ((TGaBoardArticleLoad)pload.pRcd).UserName = uname;
            //FillChar(((TGaBoardArticleLoad)(pload.pRcd)).Content, sizeof((TGaBoardArticleLoad)(pload.pRcd)).Content, '\0');
            //StrPLCopy(((TGaBoardArticleLoad)(pload.pRcd)).Content, data, sizeof((TGaBoardArticleLoad)(pload.pRcd)).Content - 1);
            if (pload == null)
            {
                return result;
            }
            AddToDBList(pload);
            return true;
        }

        public bool RequestGuildAgitBoardDelArticle(string gname, int OrgNum, int SrcNum1, int SrcNum2, int SrcNum3, string uname)
        {
            var result = false;
            if (!FActive)
            {
                return result;
            }
            var pload = new TSqlLoadRecord();
            pload.loadType = SqlEngn.GABOARD_REQDELARTICLE;
            pload.UserName = uname;
            //GetMem(pload.pRcd, sizeof(TGaBoardArticleLoad));
            // 佬绰 辆幅 积己
            ((TGaBoardArticleLoad)pload.pRcd).AgitNum = 0;
            ((TGaBoardArticleLoad)pload.pRcd).GuildName = gname;
            ((TGaBoardArticleLoad)pload.pRcd).OrgNum = OrgNum;
            ((TGaBoardArticleLoad)pload.pRcd).SrcNum1 = SrcNum1;
            ((TGaBoardArticleLoad)pload.pRcd).SrcNum2 = SrcNum2;
            ((TGaBoardArticleLoad)pload.pRcd).SrcNum3 = SrcNum3;
            ((TGaBoardArticleLoad)pload.pRcd).Kind = SqlEngn.KIND_ERROR;
            ((TGaBoardArticleLoad)pload.pRcd).UserName = uname;
            //FillChar(((TGaBoardArticleLoad)(pload.pRcd)).Content, sizeof((TGaBoardArticleLoad)(pload.pRcd)).Content, '\0');
            // debug code
            if (pload == null)
            {
                return result;
            }
            AddToDBList(pload);
            result = true;
            return result;
        }

        public bool RequestGuildAgitBoardEditArticle(string gname, int OrgNum, int SrcNum1, int SrcNum2, int SrcNum3, string uname, string data)
        {
            TSqlLoadRecord pload;
            var result = false;
            if (!FActive)
            {
                return result;
            }
            pload = new TSqlLoadRecord();
            pload.loadType = SqlEngn.GABOARD_REQEDITARTICLE;
            pload.UserName = uname;
            //GetMem(pload.pRcd, sizeof(TGaBoardArticleLoad));
            ((TGaBoardArticleLoad)pload.pRcd).AgitNum = 0;
            ((TGaBoardArticleLoad)pload.pRcd).GuildName = gname;
            ((TGaBoardArticleLoad)pload.pRcd).OrgNum = OrgNum;
            ((TGaBoardArticleLoad)pload.pRcd).SrcNum1 = SrcNum1;
            ((TGaBoardArticleLoad)pload.pRcd).SrcNum2 = SrcNum2;
            ((TGaBoardArticleLoad)pload.pRcd).SrcNum3 = SrcNum3;
            ((TGaBoardArticleLoad)pload.pRcd).Kind = SqlEngn.KIND_ERROR;
            ((TGaBoardArticleLoad)pload.pRcd).UserName = uname;
            //FillChar(((TGaBoardArticleLoad)(pload.pRcd)).Content, sizeof((TGaBoardArticleLoad)(pload.pRcd)).Content, '\0');
            //StrPLCopy(((TGaBoardArticleLoad)(pload.pRcd)).Content, data, sizeof((TGaBoardArticleLoad)(pload.pRcd)).Content - 1);
            if (pload == null)
            {
                return result;
            }
            AddToDBList(pload);
            result = true;
            return result;
        }

        public void dispose(object obj)
        { 
            
        }
    }
}

namespace GameSvr
{
    public class SqlEngn
    {
        public static TSQLEngine SqlEngine = null;
        public static int g_UMDEBUG = 0;
        public const int LOADTYPE_REQGETLIST = 100;
        public const int LOADTYPE_REQBUYITEM = 101;
        public const int LOADTYPE_REQSELLITEM = 102;
        public const int LOADTYPE_REQGETPAYITEM = 103;
        public const int LOADTYPE_REQCANCELITEM = 104;
        public const int LOADTYPE_REQREADYTOSELL = 105;
        public const int LOADTYPE_REQCHECKTODB = 106;
        public const int LOADTYPE_GETLIST = 200;
        public const int LOADTYPE_BUYITEM = 201;
        public const int LOADTYPE_SELLITEM = 202;
        public const int LOADTYPE_GETPAYITEM = 203;
        public const int LOADTYPE_CANCELITEM = 204;
        public const int LOADTYPE_READYTOSELL = 205;
        public const int KIND_NOTICE = 0;
        public const int KIND_GENERAL = 1;
        public const int KIND_ERROR = 255;
        public const int GABOARD_REQGETLIST = 500;
        public const int GABOARD_REQADDARTICLE = 501;
        public const int GABOARD_REQDELARTICLE = 502;
        public const int GABOARD_REQEDITARTICLE = 503;
        public const int GABOARD_GETLIST = 600;
        public const int GABOARD_ADDARTICLE = 601;
        public const int GABOARD_DELARTICLE = 602;
        public const int GABOARD_EDITARTICLE = 603;
    }
}

