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
                            // debug code
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
                // bug result
                if (pload != null)
                {
                    loadtime = GetTickCount;
                    loadtype = pload.loadType;
                    if ((SqlEngn.g_UMDEBUG > 0) && (SqlEngn.g_UMDEBUG != 2))
                    {
                        // debug code
                        svMain.MainOutMessage("[TestCode]ExecuteLoadCommand LoadType : " + loadtype.ToString() + " [" + SqlEngn.g_UMDEBUG.ToString() + "]");
                    }
                    SqlEngn.g_UMDEBUG = 5;
                    result = 30000 + loadtype;
                    switch (pload.loadType)
                    {
                        case SqlEngn.LOADTYPE_REQGETLIST:
                            // extended bug result
                            // 肺靛 鸥涝俊 狼秦 备喊凳
                            SqlEngn.g_UMDEBUG = 11;
                            result = 4;
                            // bug result
                            // 器牢磐 蔼阑 掘绢坷磊..
                            pSearchInfo = (TSearchSellItem)pload.pRcd;
                            SqlEngn.g_UMDEBUG = 12;
                            if (pSearchInfo != null)
                            {
                                // 府胶撇 窍唱 父电促澜俊.
                                rInfoList = new ArrayList();
                                SqlEngn.g_UMDEBUG = 21;
                                if (DBSQL.g_DBSQL == null)
                                {
                                    svMain.MainOutMessage("[Exception] g_DBSql = nil");
                                }
                                // 蔼阑 佬绢坷磊. 流立 SQL  俊辑 佬绢坷绰何盒(捞 何盒俊辑 坷幅唱绰 巴 鞍澜)
                                SqlResult = DBSQL.g_DBSQL.LoadPageUserMarket(pSearchInfo.MarketName, pSearchInfo.Who, pSearchInfo.ItemName, pSearchInfo.ItemType, pSearchInfo.ItemSet, rInfoList);
                                if (rInfoList == null)
                                {
                                    svMain.MainOutMessage("[Exception] rInfoList = nil");
                                }
                                SqlEngn.g_UMDEBUG = 22;
                                // 府胶飘 掘篮蔼阑 逞败林绊
                                pSearchInfo.IsOK = SqlResult;
                                pSearchInfo.pList = rInfoList;
                                SqlEngn.g_UMDEBUG = 23;
                                // 府胶飘俊 秦寸窍绰 器牢飘篮 绝俊林绊
                                rInfoList = null;
                                SqlEngn.g_UMDEBUG = 24;
                                // 混娄 鸥涝父 官操绊..
                                pload.loadType = SqlEngn.LOADTYPE_GETLIST;
                                SqlEngn.g_UMDEBUG = 13;
                                // 霸烙率俊辑 荤侩且荐 乐霸 殿废茄饶俊
                                AddToGameList(pload);
                                // 佬篮蔼篮 绝局霖促.
                                pSearchInfo = null;
                                pload = null;
                                SqlEngn.g_UMDEBUG = 14;
                            }
                            else
                            {
                                if (SqlEngn.g_UMDEBUG > 0)
                                {
                                    // debug code
                                    svMain.MainOutMessage("[TestCode]ExecuteLoadCommand : pSearchInfo = nil" + loadtype.ToString() + " [" + SqlEngn.g_UMDEBUG.ToString() + "]");
                                }
                                SqlEngn.g_UMDEBUG = 15;
                            }
                            break;
                        case SqlEngn.LOADTYPE_REQBUYITEM:
                            result = 5;
                            // bug result
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
                            // bug result
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
                            // bug result
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
                            // bug result
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
                            // bug result
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
                            // bug result
                            SqlEngn.g_UMDEBUG = 29;
                            pSearchInfo = (TSearchSellItem)pload.pRcd;
                            if (pSearchInfo != null)
                            {
                                switch (pSearchInfo.CheckType)
                                {
                                    case Grobal2.MARKET_CHECKTYPE_SELLOK:
                                        // 困殴 沥惑
                                        DBSQL.g_DBSQL.ChkAddSellUserMarket(pSearchInfo, true);
                                        break;
                                    case Grobal2.MARKET_CHECKTYPE_SELLFAIL:
                                        // 困殴 角菩
                                        DBSQL.g_DBSQL.ChkAddSellUserMarket(pSearchInfo, false);
                                        break;
                                    case Grobal2.MARKET_CHECKTYPE_BUYOK:
                                        // 备涝 沥惑
                                        DBSQL.g_DBSQL.ChkBuyOneUserMarket(pSearchInfo, true);
                                        break;
                                    case Grobal2.MARKET_CHECKTYPE_BUYFAIL:
                                        // 备涝 角菩
                                        DBSQL.g_DBSQL.ChkBuyOneUserMarket(pSearchInfo, false);
                                        break;
                                    case Grobal2.MARKET_CHECKTYPE_CANCELOK:
                                        // 秒家 沥惑
                                        DBSQL.g_DBSQL.ChkCancelUserMarket(pSearchInfo, true);
                                        break;
                                    case Grobal2.MARKET_CHECKTYPE_CANCELFAIL:
                                        // 秒家 角菩
                                        DBSQL.g_DBSQL.ChkCancelUserMarket(pSearchInfo, false);
                                        break;
                                    case Grobal2.MARKET_CHECKTYPE_GETPAYOK:
                                        // 捣 雀荐 沥惑
                                        DBSQL.g_DBSQL.ChkGetPayUserMarket(pSearchInfo, true);
                                        break;
                                    case Grobal2.MARKET_CHECKTYPE_GETPAYFAIL:
                                        // 捣 雀荐 角菩
                                        DBSQL.g_DBSQL.ChkGetPayUserMarket(pSearchInfo, false);
                                        break;
                                }
                                FreeMem(pSearchInfo);
                                pSearchInfo = null;
                            }
                            break;
                        case SqlEngn.GABOARD_REQGETLIST:
                            // ------------------------------------------
                            // 厘盔霸矫魄 格废...
                            result = 11;
                            // bug result
                            // 器牢磐 蔼阑 掘绢坷磊..
                            pSearchGaBoardList = (TSearchGaBoardList)pload.pRcd;
                            if (pSearchGaBoardList != null)
                            {
                                // 府胶飘撇 窍唱 父电促澜俊.
                                rInfoList = new ArrayList();
                                // 蔼阑 佬绢坷磊. 流立 SQL俊辑 佬绢坷绰何盒
                                SqlResult = DBSQL.g_DBSQL.LoadPageGaBoardList(pSearchGaBoardList.GuildName, pSearchGaBoardList.Kind, rInfoList);
                                // 府胶飘 掘篮蔼阑 逞败林绊
                                pSearchGaBoardList.ArticleList = rInfoList;
                                // 府胶飘俊 秦寸窍绰 器牢磐绰 绝局林绊
                                rInfoList = null;
                                // 混娄 鸥涝父 官操绊..
                                pload.loadType = SqlEngn.GABOARD_GETLIST;
                                // 霸烙率俊辑 荤侩且荐 乐霸 殿废茄饶俊
                                AddToGameList(pload);
                                // 佬篮 蔼篮 绝局霖促.
                                pSearchGaBoardList = null;
                                pload = null;
                            }
                            break;
                        case SqlEngn.GABOARD_REQADDARTICLE:
                            result = 12;
                            // bug result
                            // 器牢磐 蔼阑 掘绢坷磊..
                            pArticleLoad = (TGaBoardArticleLoad)pload.pRcd;
                            // 蜡历捞抚 汗荤.
                            pArticleLoad.UserName = pload.UserName;
                            if (pArticleLoad != null)
                            {
                                // 蔼阑 佬绢坷磊. 流立 SQL俊辑 佬绢坷绰何盒
                                SqlResult = DBSQL.g_DBSQL.AddGaBoardArticle(pArticleLoad);
                                // 混娄 鸥涝父 官操绊..
                                pload.loadType = SqlEngn.GABOARD_ADDARTICLE;
                                // 霸烙率俊辑 荤侩且荐 乐霸 殿废茄饶俊
                                AddToGameList(pload);
                                // 佬篮 蔼篮 绝局霖促.
                                pArticleLoad = null;
                                pload = null;
                            }
                            break;
                        case SqlEngn.GABOARD_REQDELARTICLE:
                            result = 13;
                            // bug result
                            // 器牢磐 蔼阑 掘绢坷磊..
                            pArticleLoad = (TGaBoardArticleLoad)pload.pRcd;
                            // 蜡历捞抚 汗荤.
                            pArticleLoad.UserName = pload.UserName;
                            if (pArticleLoad != null)
                            {
                                // 蔼阑 佬绢坷磊. 流立 SQL俊辑 佬绢坷绰何盒
                                SqlResult = DBSQL.g_DBSQL.DelGaBoardArticle(pArticleLoad);
                                // 混娄 鸥涝父 官操绊..
                                pload.loadType = SqlEngn.GABOARD_DELARTICLE;
                                // 霸烙率俊辑 荤侩且荐 乐霸 殿废茄饶俊
                                AddToGameList(pload);
                                // 佬篮 蔼篮 绝局霖促.
                                pArticleLoad = null;
                                pload = null;
                            }
                            break;
                        case SqlEngn.GABOARD_REQEDITARTICLE:
                            result = 14;
                            // bug result
                            // 器牢磐 蔼阑 掘绢坷磊..
                            pArticleLoad = (TGaBoardArticleLoad)pload.pRcd;
                            // 蜡历捞抚 汗荤.
                            pArticleLoad.UserName = pload.UserName;
                            if (pArticleLoad != null)
                            {
                                // 蔼阑 佬绢坷磊. 流立 SQL俊辑 佬绢坷绰何盒
                                SqlResult = DBSQL.g_DBSQL.EditGaBoardArticle(pArticleLoad);
                                // 混娄 鸥涝父 官操绊..
                                pload.loadType = SqlEngn.GABOARD_EDITARTICLE;
                                // 霸烙率俊辑 荤侩且荐 乐霸 殿废茄饶俊
                                AddToGameList(pload);
                                // 佬篮 蔼篮 绝局霖促.
                                pArticleLoad = null;
                                pload = null;
                            }
                            break;
                        default:
                            result = 170000 + loadtype;
                            // extended bug result
                            if (SqlEngn.g_UMDEBUG > 0)
                            {
                                // debug code
                                svMain.MainOutMessage("[TestCode]ExecuteLoadCommand : case else LoadType" + loadtype.ToString() + " [" + SqlEngn.g_UMDEBUG.ToString() + "]");
                            }
                            SqlEngn.g_UMDEBUG = 20;
                            break;
                            // ------------------------------------------
                    }
                    // case
                    result = 15;
                    // bug result
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
            int buginfo;
            buginfo = 0;
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
                this.Sleep(1);
                // 何窍巩力肺 1->50栏肺 荐沥(sonmg 2004/06/15)->叼矫 汗盔(2004/07/08)
                if (this.Terminated)
                {
                    return;
                }
            }
        }

        // GAME SERVER ==> DB 单捞磐 傈价 ==============================================
        // 沥焊夸没目膏飘 殿废
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
            // 佬扁 饭内靛 积己
            pload = new TSqlLoadRecord();
            pload.loadType = SqlEngn.LOADTYPE_REQGETLIST;
            pload.UserName = ReqInfo_.UserName;
            //GetMem(pload.pRcd, sizeof(TSearchSellItem));
            // 佬绰 辆幅 积己
            ((TSearchSellItem)pload.pRcd).MarketName = ReqInfo_.MarketName;
            ((TSearchSellItem)pload.pRcd).Who = ReqInfo_.SearchWho;
            ((TSearchSellItem)pload.pRcd).ItemName = ReqInfo_.SearchItem;
            ((TSearchSellItem)pload.pRcd).ItemType = ReqInfo_.ItemType;
            ((TSearchSellItem)pload.pRcd).ItemSet = ReqInfo_.ItemSet;
            ((TSearchSellItem)pload.pRcd).UserMode = ReqInfo_.UserMode;
            // debug code
            if (pload == null)
            {
                return result;
            }
            AddToDBList(pload);
            if (SqlEngn.g_UMDEBUG == 1000)
            {
                // debug code
                svMain.MainOutMessage("RequestLoadPageUserMarket-AddToDBList" + " [" + SqlEngn.g_UMDEBUG.ToString() + "]");
            }
            SqlEngn.g_UMDEBUG = 1;
            result = true;
            return result;
        }

        // 酒捞袍 困殴 啊瓷茄瘤 八荤
        // 郴啊 魄概棵赴 酒捞袍阑 秒家矫挪促.
        public bool RequestReadyToSellUserMarket(string UserName, string MarketName, string sellwho)
        {
            bool result;
            TSqlLoadRecord pload;
            result = false;
            if (!FActive)
            {
                return result;
            }
            pload = new TSqlLoadRecord();
            pload.loadType = SqlEngn.LOADTYPE_REQREADYTOSELL;
            pload.UserName = UserName;
            //GetMem(pload.pRcd, sizeof(TMarketLoad));
            // 措脚 敬促.
            ((TMarketLoad)pload.pRcd).MarketName = MarketName;
            ((TMarketLoad)pload.pRcd).SellWho = sellwho;
            // debug code
            if (pload == null)
            {
                return result;
            }
            AddToDBList(pload);
            result = true;
            return result;
        }

        // 酒捞袍 荤扁 夸没
        // 酒捞袍 荤扁甫 夸没茄促.
        public bool RequestBuyItemUserMarket(string UserName, string MarketName, string BuyWho, int SellIndex)
        {
            TSqlLoadRecord pload;
            bool result = false;
            if (!FActive)
            {
                return result;
            }
            // 佬扁 饭内靛 积己
            pload = new TSqlLoadRecord();
            pload.loadType = SqlEngn.LOADTYPE_REQBUYITEM;
            pload.UserName = UserName;
            GetMem(pload.pRcd, sizeof(TMarketLoad));
            // 荤绊磊窍绰 酒捞袍 沥焊殿废
            ((TMarketLoad)pload.pRcd).MarketName = MarketName;
            ((TMarketLoad)pload.pRcd).SellWho = BuyWho;
            ((TMarketLoad)pload.pRcd).Index = SellIndex;
            // debug code
            if (pload == null)
            {
                return result;
            }
            // 殿废
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
            bool result;
            TSqlLoadRecord pload;
            result = false;
            if (!FActive)
            {
                return result;
            }
            pload = new TSqlLoadRecord();
            pload.loadType = SqlEngn.LOADTYPE_REQSELLITEM;
            pload.UserName = UserName;
            GetMem(pload.pRcd, sizeof(TMarketLoad));
            Move(pselladd, pload.pRcd, sizeof(TMarketLoad));
            if (pload == null)
            {
                return result;
            }
            AddToDBList(pload);
            result = true;
            return result;
        }

        // 酒捞袍 昏力
        // 酒捞袍 昏力
        public bool RequestGetPayUserMarket(string UserName, string MarketName, string sellwho, int sellindex)
        {
            bool result;
            TSqlLoadRecord pload;
            result = false;
            if (!FActive)
            {
                return result;
            }
            pload = new TSqlLoadRecord();
            pload.loadType = SqlEngn.LOADTYPE_REQGETPAYITEM;
            pload.UserName = UserName;
            GetMem(pload.pRcd, sizeof(TMarketLoad));
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

        // 殿废等 酒捞袍 秒家
        // 郴啊 魄概棵赴 酒捞袍阑 秒家矫挪促.
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
            GetMem(pload.pRcd, sizeof(TMarketLoad));
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

        // DB --> GAME SERVER  单捞磐 傈价 =============================================
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

        // 霸烙率俊辑 单捞磐甫 啊廉促 静绰 何盒
        // 捞率 酒贰何磐绰 霸烙率俊辑 荤侩窍绰 何盒捞骨肺 静饭靛啊 盒府等促 林狼!=======
        // 霸烙率俊辑 单捞磐甫 佬绢辑 贸府秦具登绰 何盒...
        private TSqlLoadRecord GetGameExecuteData()
        {
            TSqlLoadRecord result;
            result = null;
            // 疙飞绢 府胶飘 掘扁... 静饭靛 林狼 ...
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

        // 霸烙率俊辑 角青窍绰 风凭
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
                // 茄锅俊 窍唱父 角青窍档废窍磊.. 1msec 鸥捞赣俊 拱妨辑 荤侩窍霸 等促.
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
                                // 蜡历啊 乐栏搁
                                if (hum != null)
                                {
                                    hum.GetMarketData(pSearchInfo);
                                    hum.SendUserMarketList(0);
                                }
                                else
                                {
                                    // 蜡历啊 绝促.. 府胶飘 傈价企扁..
                                    svMain.MainOutMessage("INFO SQLENGINE DO NOT FIND USER FOR MARKETLIST!");
                                }
                                // 皋葛府 秦力..
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
                                    // 蜡历啊 绝促.. 府胶飘 傈价企扁..
                                    svMain.MainOutMessage("INFO SQLENGINE DO NOT FIND USER FOR SELLITEM!");
                                    // 单捞磐海捞胶 率 郴侩 秒家傈价
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
                                    // 蜡历啊 绝促.. 府胶飘 傈价企扁..
                                    svMain.MainOutMessage("INFO SQLENGINE DO NOT FIND USER FOR SELLITEM!");
                                    // 单捞磐海捞胶 率 郴侩 秒家傈价
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
                                    // 蜡历啊 绝促.. 府胶飘 傈价企扁..
                                    svMain.MainOutMessage("INFO SQLENGINE DO NOT FIND USER FOR SELLITEM!");
                                    // 单捞磐海捞胶 率 郴侩 秒家傈价
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
                                    // 蜡历啊 绝促.. 府胶飘 傈价企扁..
                                    svMain.MainOutMessage("INFO SQLENGINE DO NOT FIND USER FOR CANCEL!");
                                    // 单捞磐海捞胶 率 郴侩 秒家傈价
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
                                    // 蜡历啊 绝促.. 府胶飘 傈价企扁..
                                    svMain.MainOutMessage("INFO SQLENGINE DO NOT FIND USER FOR GETPAY!");
                                    // 单捞磐海捞胶 率 郴侩 秒家傈价
                                }
                            }
                            break;
                        case SqlEngn.GABOARD_GETLIST:
                            // ------------------------------------------
                            // 厘盔霸矫魄 格废...
                            pBoardListInfo = (TSearchGaBoardList)pLoad.pRcd;
                            if (pBoardListInfo != null)
                            {
                                if (pBoardListInfo.GuildName != "")
                                {
                                    // 厘盔霸矫魄 府胶飘甫 佬澜.
                                    svMain.GuildAgitBoardMan.AddGaBoardList(pBoardListInfo);
                                    // 蜡历俊霸 Refresh矫糯.
                                    hum = svMain.UserEngine.GetUserHuman(pBoardListInfo.UserName);
                                    if (hum != null)
                                    {
                                        hum.CmdReloadGaBoardList(pBoardListInfo.GuildName, 1);
                                    }
                                }
                                // 皋葛府 秦力..
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
                                // 快急 DB俊辑 肺靛茄促...
                                // GuildAgitBoardMan.LoadAllGaBoardList( pArticleInfo.UserName );
                            }
                            break;
                        case SqlEngn.GABOARD_DELARTICLE:
                            pArticleInfo = (TGaBoardArticleLoad)pLoad.pRcd;
                            if (pArticleInfo != null)
                            {
                                // 快急 DB俊辑 肺靛茄促...
                                // GuildAgitBoardMan.LoadAllGaBoardList( pArticleInfo.UserName );
                            }
                            break;
                        case SqlEngn.GABOARD_EDITARTICLE:
                            pArticleInfo = (TGaBoardArticleLoad)pLoad.pRcd;
                            if (pArticleInfo != null)
                            {
                                // 快急 DB俊辑 肺靛茄促...
                                // GuildAgitBoardMan.LoadAllGaBoardList( pArticleInfo.UserName );
                            }
                            break;
                            // ------------------------------------------
                    }
                    // 皋葛府 秦力.. pRcd
                    if (pLoad.pRcd != null)
                    {
                        FreeMem(pLoad.pRcd);
                    }
                    // 皋葛府 秦力
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
            bool result;
            TSqlLoadRecord pload;
            result = false;
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
            result = true;
            return result;
        }

        public bool RequestGuildAgitBoardDelArticle(string gname, int OrgNum, int SrcNum1, int SrcNum2, int SrcNum3, string uname)
        {
            bool result;
            TSqlLoadRecord pload;
            result = false;
            if (!FActive)
            {
                return result;
            }
            // 佬扁 饭内靛 积己
            pload = new TSqlLoadRecord();
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
            bool result;
            TSqlLoadRecord pload;
            result = false;
            if (!FActive)
            {
                return result;
            }
            // 佬扁 饭内靛 积己
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
    }
}

namespace GameSvr
{
    public class SqlEngn
    {
        public static TSQLEngine SqlEngine = null;
        public static int g_UMDEBUG = 0;
        // GAME --> DB
        public const int LOADTYPE_REQGETLIST = 100;
        // 酒捞袍 府胶飘甫 夸没茄促.
        public const int LOADTYPE_REQBUYITEM = 101;
        // 酒捞袍 荤扁甫 夸没茄促.
        public const int LOADTYPE_REQSELLITEM = 102;
        // 酒捞袍 殿废
        public const int LOADTYPE_REQGETPAYITEM = 103;
        // 悼雀荐
        public const int LOADTYPE_REQCANCELITEM = 104;
        // 郴啊 殿废茄 酒捞袍 秒家
        public const int LOADTYPE_REQREADYTOSELL = 105;
        // 困殴啊瓷茄瘤 舅酒焊绰巴
        public const int LOADTYPE_REQCHECKTODB = 106;
        // 酒捞袍 沥惑 荐飞 咯何历厘.
        // DB --> GAME
        public const int LOADTYPE_GETLIST = 200;
        // 酒捞袍 府胶飘 掘澜
        public const int LOADTYPE_BUYITEM = 201;
        // 酒捞袍阑 魂促.
        public const int LOADTYPE_SELLITEM = 202;
        // 酒捞袍阑 殿废
        public const int LOADTYPE_GETPAYITEM = 203;
        // 悼雀荐
        public const int LOADTYPE_CANCELITEM = 204;
        // 郴啊 殿废茄 酒捞袍 秒家
        public const int LOADTYPE_READYTOSELL = 205;
        // 困殴啊瓷茄瘤 舅酒夯促.
        // --------厘盔霸矫魄(sonmg)--------
        public const int KIND_NOTICE = 0;
        public const int KIND_GENERAL = 1;
        public const int KIND_ERROR = 255;
        // GAME --> DB
        public const int GABOARD_REQGETLIST = 500;
        // 厘盔霸矫魄 府胶飘 夸没.
        public const int GABOARD_REQADDARTICLE = 501;
        // 厘盔霸矫魄 臂静扁 夸没.
        public const int GABOARD_REQDELARTICLE = 502;
        // 厘盔霸矫魄 臂昏力 夸没.
        public const int GABOARD_REQEDITARTICLE = 503;
        // 厘盔霸矫魄 臂荐沥 夸没.
        // DB --> GAME
        public const int GABOARD_GETLIST = 600;
        // 厘盔霸矫魄 府胶飘 掘澜.
        public const int GABOARD_ADDARTICLE = 601;
        public const int GABOARD_DELARTICLE = 602;
        public const int GABOARD_EDITARTICLE = 603;
    }
}

