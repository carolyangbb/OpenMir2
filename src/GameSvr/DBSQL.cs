using System;
using System.Collections;
using System.Data;
using SystemModule;
using SystemModule.Common;

namespace GameSvr
{
    public class TDBSql
    {
        public bool AutoConnectable
        {
            get
            {
                return FAutoConnectable;
            }
            set
            {
                FAutoConnectable = value;
            }
        }
        private readonly IDbConnection FADOConnection = null;
        private readonly IDataReader FADOQuery = null;
        private bool FAutoConnectable = false;
        private readonly StringList FConnFile = null;
        private readonly string FConnInfo = string.Empty;
        private string FFileName = string.Empty;
        private string FServerName = string.Empty;
        private readonly DateTime FLastConnTime;
        private int FLastConnMsec = 0;

        public TDBSql()
        {
            //FADOConnection = new TADOConnection(null);
            //FADOQuery = new TADOQuery(null);
            FConnFile = new StringList();
            FConnInfo = "";
            FLastConnTime = DateTime.Now;
            FLastConnMsec = 0;
            FAutoConnectable = false;
        }

        ~TDBSql()
        {
            DisConnect();
            //FADOConnection.Free();
            //FADOQuery.Free();
            //FConnFile.Free();
        }

        public bool Connect(string ServerName, string FileName)
        {
            bool result;
            result = false;
            FFileName = FileName;
            FServerName = ServerName;
            FConnFile.LoadFromFile(FileName);
            //FConnInfo = FConnFile[ServerName];
            //if (FConnInfo != "")
            //{
            //    FADOConnection.ConnectionString = FConnInfo;
            //    FADOConnection.LoginPrompt = false;
            //    FADOConnection.Connected = true;
            //    result = FADOConnection.Connected;
            //    if (result == true)
            //    {
            //        FADOQuery.Active = false;
            //        FADOQuery.Connection = FADOConnection;
            //        FLastConnTime = DateTime.Now;
            //        svMain.MainOutMessage("SUCCESS DBSQL CONNECTION ");
            //    }
            //}
            //else
            //{
            //    svMain.MainOutMessage(ServerName + " : DBSQL CONNECTION INFO IS NULL!");
            //}
            FLastConnMsec = HUtil32.GetTickCount();
            return result;
        }

        public bool ReConnect()
        {
            bool result;
            result = false;
            if ((FLastConnMsec + 15 * 1000) < HUtil32.GetTickCount())
            {
                DisConnect();
                M2Share.MainOutMessage("[TestCode]Try to reconnect with DBSQL");
                result = Connect(FServerName, FFileName);
                M2Share.MainOutMessage("CAUTION! DBSQL RECONNECTION");
            }
            return result;
        }

        public bool Connected()
        {
            //bool result;
            //result = FADOConnection.Connected;
            return false;
        }

        public void DisConnect()
        {
            //FADOQuery.Active = false;
            //FADOConnection.Connected = false;
        }

        private void LoadItemFromDB(TMarketLoad pItem, IDataReader SqlDB)
        {
            if (SqlDB == null)
            {
                M2Share.MainOutMessage("[Exception] SqlDB = nil");
            }
            /*pItem.Index = SqlDB.FieldByName("FLD_SELLINDEX").AsInteger;
            pItem.SellState = SqlDB.FieldByName("FLD_SELLOK").AsInteger;
            pItem.SellWho = SqlDB.FieldByName("FLD_SELLWHO").AsString.Trim();
            pItem.ItemName = SqlDB.FieldByName("FLD_ITEMNAME").AsString.Trim();
            pItem.SellPrice = SqlDB.FieldByName("FLD_SELLPRICE").AsInteger;
            pItem.Selldate = FormatDateTime("YYMMDDHHNNSS", SqlDB.FieldByName("FLD_SELLDATE").AsDateTime);
            pItem.UserItem.MakeIndex = SqlDB.FieldByName("FLD_MAKEINDEX").AsInteger;
            pItem.UserItem.Index = SqlDB.FieldByName("FLD_INDEX").AsInteger;
            pItem.UserItem.Dura = SqlDB.FieldByName("FLD_DURA").AsInteger;
            pItem.UserItem.DuraMax = SqlDB.FieldByName("FLD_DURAMAX").AsInteger;
            for (k = 0; k <= 13; k++)
            {
                pItem.UserItem.Desc[k] = SqlDB.FieldByName("FLD_DESC" + k.ToString()).AsInteger;
            }
            pItem.UserItem.ColorR = SqlDB.FieldByName("FLD_COLORR").AsInteger;
            pItem.UserItem.ColorG = SqlDB.FieldByName("FLD_COLORG").AsInteger;
            pItem.UserItem.ColorB = SqlDB.FieldByName("FLD_COLORB").AsInteger;
            prefix = SqlDB.FieldByName("FLD_PREFIX").AsString.Trim();
            StrPCopy(pItem.UserItem.Prefix, prefix);*/
        }

        public int LoadPageUserMarket(string marketname, string sellwho, string itemname, int itemtype, int itemset, ArrayList sellitemlist)
        {
            int result = Grobal2.UMResult_Fail;
            //if (itemname != "")
            //{
            //    SearchStr = "EXEC UM_LOAD_ITEMNAME \'" + marketname + "\',\'" + itemname + "\'";
            //}
            //else if (sellwho != "")
            //{
            //    SearchStr = "EXEC UM_LOAD_USERNAME \'" + marketname + "\',\'" + sellwho + "\'";
            //}
            //else if (itemset != 0)
            //{
            //    SearchStr = "EXEC UM_LOAD_ITEMSET \'" + marketname + "\'," + itemset.ToString();
            //}
            //else if (itemtype >= 0)
            //{
            //    SearchStr = "EXEC UM_LOAD_ITEMTYPE \'" + marketname + "\'," + itemtype.ToString();
            //}
            //try
            //{
            //    if (FADOQuery.Active)
            //    {
            //        //VisualStyleElement.ToolTip.Close;
            //    }
            //    FADOQuery.SQL.Clear();
            //    FADOQuery.SQL.ADD(SearchStr);
            //    if (!FADOQuery.Active)
            //    {
            //        FADOQuery.Open;
            //    }
            //}
            //catch
            //{
            //    svMain.MainOutMessage("Exception) TFrmSql.LoadPageUserMarket -> Open (" + FADOQuery.SQL.Count.ToString() + ")");
            //    for (i = 0; i < FADOQuery.SQL.Count; i++)
            //    {
            //        svMain.MainOutMessage(" :" + FADOQuery.SQL[i]);
            //    }
            //    result = Grobal2.UMResult_ReadFail;
            //    return result;
            //}
            //try
            //{
            //    FADOQuery.First;
            //    for (i = 0; i < FADOQuery.RecordCount; i++)
            //    {
            //        pSellItem = new TMarketLoad();
            //        LoadItemFromDB(pSellItem, FADOQuery);
            //        sellitemlist.Add(pSellItem);
            //        if (!EOF)
            //        {
            //            FADOQuery.Next;
            //        }
            //    }
            //    if (FADOQuery.Active)
            //    {
            //        //VisualStyleElement.ToolTip.Close;
            //    }
            //    result = Grobal2.UMResult_Success;
            //}
            //catch
            //{
            //    svMain.MainOutMessage("Exception) TFrmSql.LoadPageUserMarket -> LoadItemFromDB (" + FADOQuery.RecordCount.ToString() + ")");
            //    result = Grobal2.UMResult_ReadFail;
            //    if (FADOQuery.Active)
            //    {
            //        //VisualStyleElement.ToolTip.Close;
            //    }
            //}
            return result;
        }

        public int AddSellUserMarket(TMarketLoad psellitem)
        {
            int result = Grobal2.UMResult_Fail;
            //FADOQuery.SQL.Clear();
            //FADOQuery.SQL.Add("INSERT INTO TBL_ITEMMARKET (" + "FLD_MARKETNAME," + "FLD_SELLOK," + "FLD_ITEMTYPE," + "FLD_ITEMSET," + "FLD_ITEMNAME," + "FLD_SELLWHO," + "FLD_SELLPRICE," + "FLD_SELLDATE," + "FLD_MAKEINDEX," + "FLD_INDEX," + "FLD_DURA," + "FLD_DURAMAX," + "FLD_DESC0," + "FLD_DESC1," + "FLD_DESC2," + "FLD_DESC3," + "FLD_DESC4," + "FLD_DESC5," + "FLD_DESC6," + "FLD_DESC7," + "FLD_DESC8," + "FLD_DESC9," + "FLD_DESC10," + "FLD_DESC11," + "FLD_DESC12," + "FLD_DESC13," + "FLD_COLORR," + "FLD_COLORG," + "FLD_COLORB," + "FLD_PREFIX" + ")");
            //FADOQuery.SQL.Add(" Values(\'" + psellitem.MarketName + "\'," + Grobal2.MARKET_DBSELLTYPE_READYSELL.ToString() + "," + psellitem.MarketType.ToString() + "," + psellitem.SetType.ToString() + ",\'" + psellitem.ItemName + "\',\'" + psellitem.SellWho + "\'," + psellitem.SellPrice.ToString() + "," + "GETDATE()," + psellitem.UserItem.MakeIndex.ToString() + "," + psellitem.UserItem.Index.ToString() + "," + psellitem.UserItem.Dura.ToString() + "," + psellitem.UserItem.DuraMax.ToString() + "," + psellitem.UserItem.Desc[0].ToString() + "," + psellitem.UserItem.Desc[1].ToString() + "," + psellitem.UserItem.Desc[2].ToString() + "," + psellitem.UserItem.Desc[3].ToString() + "," + psellitem.UserItem.Desc[4].ToString() + "," + psellitem.UserItem.Desc[5].ToString() + "," + psellitem.UserItem.Desc[6].ToString() + "," + psellitem.UserItem.Desc[7].ToString() + "," + psellitem.UserItem.Desc[8].ToString() + "," + psellitem.UserItem.Desc[9].ToString() + "," + psellitem.UserItem.Desc[10].ToString() + "," + psellitem.UserItem.Desc[11].ToString() + "," + psellitem.UserItem.Desc[12].ToString() + "," + psellitem.UserItem.Desc[13].ToString() + "," + psellitem.UserItem.ColorR.ToString() + "," + psellitem.UserItem.ColorG.ToString() + "," + psellitem.UserItem.ColorB.ToString() + ",\'" + psellitem.UserItem.Prefix + "\'" + ")");
            //try
            //{
            //    FADOQuery.ExecSQL;
            //    result = Grobal2.UMResult_Success;
            //}
            //catch
            //{
            //    svMain.MainOutMessage("Exception) TFrmSql.AddSellUserMarket -> ExecSQL");
            //    for (var i = 0; i < FADOQuery.SQL.Count; i++)
            //    {
            //        svMain.MainOutMessage(" :" + FADOQuery.SQL[i]);
            //    }
            //}
            return result;
        }

        public int ReadyToSell(ref TMarketLoad Readyitem)
        {
            int result = Grobal2.UMResult_Fail;
            string SearchStr = "EXEC UM_READYTOSELL_NEW \'" + Readyitem.MarketName + "\',\'" + Readyitem.SellWho + "\'";
            //try
            //{
            //    if (FADOQuery.Active)
            //    {
            //        //VisualStyleElement.ToolTip.Close;
            //    }
            //    FADOQuery.SQL.Clear();
            //    FADOQuery.SQL.ADD(SearchStr);
            //    if (!FADOQuery.Active)
            //    {
            //        FADOQuery.Open;
            //    }
            //}
            //catch
            //{
            //    svMain.MainOutMessage("Exception) TFrmSql.ReadyToSell -> Open");
            //    for (i = 0; i < FADOQuery.SQL.Count; i++)
            //    {
            //        svMain.MainOutMessage(" :" + FADOQuery.SQL[i]);
            //    }
            //    result = Grobal2.UMResult_ReadFail;
            //    return result;
            //}
            //try
            //{
            //    if (FADOQuery.RecordCount >= 0)
            //    {
            //        Readyitem.SellCount = FADOQuery.RecordCount;
            //        result = Grobal2.UMResult_Success;
            //    }
            //    else
            //    {
            //        Readyitem.SellCount = 0;
            //        result = Grobal2.UMResult_Fail;
            //    }
            //    if (FADOQuery.Active)
            //    {
            //        //VisualStyleElement.ToolTip.Close;
            //    }
            //}
            //catch
            //{
            //    svMain.MainOutMessage("Exception) TFrmSql.ReadyToSell -> RecordCount");
            //    Readyitem.SellCount = 0;
            //    result = Grobal2.UMResult_Fail;
            //    if (FADOQuery.Active)
            //    {
            //        //VisualStyleElement.ToolTip.Close;
            //    }
            //}
            return result;
        }

        public int BuyOneUserMarket(ref TMarketLoad Buyitem)
        {
            int result;
            string SearchStr;
            int CheckType;
            int ChangeTYpe;
            int ItemIndex;
            result = Grobal2.UMResult_Fail;
            CheckType = Grobal2.MARKET_DBSELLTYPE_SELL;
            ChangeTYpe = Grobal2.MARKET_DBSELLTYPE_READYBUY;
            ItemIndex = Buyitem.Index;
            SearchStr = "EXEC UM_LOAD_INDEX " + ItemIndex.ToString() + "," + CheckType.ToString() + "," + ChangeTYpe.ToString();
            //FADOQuery.SQL.Clear();
            //FADOQuery.SQL.ADD(SearchStr);
            //try
            //{
            //    if (!FADOQuery.Active)
            //    {
            //        FADOQuery.Open;
            //    }
            //}
            //catch
            //{
            //    svMain.MainOutMessage("Exception) TFrmSql.BuyOnUserMarket -> Open");
            //    for (i = 0; i < FADOQuery.SQL.Count; i++)
            //    {
            //        svMain.MainOutMessage(" :" + FADOQuery.SQL[i]);
            //    }
            //    result = Grobal2.UMResult_ReadFail;
            //    return result;
            //}
            //if (FADOQuery.RecordCount == 1)
            //{
            //    LoadItemFromDB(Buyitem.UserItem, FADOQuery);
            //    result = Grobal2.UMResult_Success;
            //}
            //else
            //{
            //    result = Grobal2.UMResult_Fail;
            //}
            //if (FADOQuery.Active)
            //{
            //    //VisualStyleElement.ToolTip.Close;
            //}
            return result;
        }

        public int CancelUserMarket(ref TMarketLoad Cancelitem)
        {
            int result;
            string SearchStr;
            int CheckType;
            int ChangeTYpe;
            int ItemIndex;
            result = Grobal2.UMResult_Fail;
            CheckType = Grobal2.MARKET_DBSELLTYPE_SELL;
            ChangeTYpe = Grobal2.MARKET_DBSELLTYPE_READYCANCEL;
            ItemIndex = Cancelitem.Index;
            SearchStr = "EXEC UM_LOAD_INDEX " + ItemIndex.ToString() + "," + CheckType.ToString() + "," + ChangeTYpe.ToString();
            //FADOQuery.SQL.Clear();
            //FADOQuery.SQL.ADD(SearchStr);
            //try
            //{
            //    if (!FADOQuery.Active)
            //    {
            //        FADOQuery.Open;
            //    }
            //}
            //catch
            //{
            //    svMain.MainOutMessage("Exception) TFrmSql.BuyOnUserMarket -> Open");
            //    for (i = 0; i < FADOQuery.SQL.Count; i++)
            //    {
            //        svMain.MainOutMessage(" :" + FADOQuery.SQL[i]);
            //    }
            //    result = Grobal2.UMResult_ReadFail;
            //    return result;
            //}
            //if (FADOQuery.RecordCount == 1)
            //{
            //    LoadItemFromDB(Cancelitem.UserItem, FADOQuery);
            //    result = Grobal2.UMResult_Success;
            //}
            //else
            //{
            //    result = Grobal2.UMResult_Fail;
            //}
            //if (FADOQuery.Active)
            //{
            //    //VisualStyleElement.ToolTip.Close;
            //}
            return result;
        }

        public int GetPayUserMarket(ref TMarketLoad GetPayitem)
        {
            int result;
            string SearchStr;
            int CheckType;
            int ChangeTYpe;
            int ItemIndex;
            result = Grobal2.UMResult_Fail;
            CheckType = Grobal2.MARKET_DBSELLTYPE_BUY;
            ChangeTYpe = Grobal2.MARKET_DBSELLTYPE_READYGETPAY;
            ItemIndex = GetPayitem.Index;
            SearchStr = "EXEC UM_LOAD_INDEX " + ItemIndex.ToString() + "," + CheckType.ToString() + "," + ChangeTYpe.ToString();
            //FADOQuery.SQL.Clear();
            //FADOQuery.SQL.ADD(SearchStr);
            //try
            //{
            //    if (!FADOQuery.Active)
            //    {
            //        FADOQuery.Open;
            //    }
            //}
            //catch
            //{
            //    svMain.MainOutMessage("Exception) TFrmSql.BuyOnUserMarket -> Open");
            //    for (i = 0; i < FADOQuery.SQL.Count; i++)
            //    {
            //        svMain.MainOutMessage(" :" + FADOQuery.SQL[i]);
            //    }
            //    result = Grobal2.UMResult_ReadFail;
            //    return result;
            //}
            //if (FADOQuery.RecordCount == 1)
            //{
            //    LoadItemFromDB(GetPayitem.UserItem, FADOQuery);
            //    result = Grobal2.UMResult_Success;
            //}
            //else
            //{
            //    result = Grobal2.UMResult_Fail;
            //}
            //if (FADOQuery.Active)
            //{
            //    //VisualStyleElement.ToolTip.Close;
            //}
            return result;
        }

        public int ChkAddSellUserMarket(TSearchSellItem pSearchInfo, bool IsSucess)
        {
            int result;
            string SearchStr;
            int CheckType;
            int ChangeTYpe;
            int MakeIndex;
            string sellwho;
            string marketname;
            result = Grobal2.UMResult_Fail;
            CheckType = Grobal2.MARKET_DBSELLTYPE_READYSELL;
            if (IsSucess)
            {
                ChangeTYpe = Grobal2.MARKET_DBSELLTYPE_SELL;
            }
            else
            {
                ChangeTYpe = Grobal2.MARKET_DBSELLTYPE_DELETE;
            }
            MakeIndex = pSearchInfo.MakeIndex;
            sellwho = pSearchInfo.Who;
            marketname = pSearchInfo.MarketName;
            SearchStr = "EXEC UM_CHECK_MAKEINDEX \'" + marketname + "\',\'" + sellwho + "\'," + MakeIndex.ToString() + "," + CheckType.ToString() + "," + ChangeTYpe.ToString();
            //FADOQuery.SQL.Clear();
            //FADOQuery.SQL.ADD(SearchStr);
            //try
            //{
            //    FADOQuery.ExecSql;
            //}
            //catch
            //{
            //    svMain.MainOutMessage("Exception) TFrmSql.ChkAddSellUserMarket -> Open");
            //    for (i = 0; i < FADOQuery.SQL.Count; i++)
            //    {
            //        svMain.MainOutMessage(" :" + FADOQuery.SQL[i]);
            //    }
            //    result = Grobal2.UMResult_ReadFail;
            //    return result;
            //}
            //result = Grobal2.UMResult_Success;
            //if (FADOQuery.Active)
            //{
            //    //VisualStyleElement.ToolTip.Close;
            //}
            return result;
        }

        public int ChkBuyOneUserMarket(TSearchSellItem pSearchInfo, bool IsSucess)
        {
            int result;
            string SearchStr;
            int CheckType;
            int ChangeTYpe;
            int Index;
            string sellwho;
            string marketname;
            result = Grobal2.UMResult_Fail;
            CheckType = Grobal2.MARKET_DBSELLTYPE_READYBUY;
            if (IsSucess)
            {
                ChangeTYpe = Grobal2.MARKET_DBSELLTYPE_BUY;
            }
            else
            {
                ChangeTYpe = Grobal2.MARKET_DBSELLTYPE_SELL;
            }
            Index = pSearchInfo.SellIndex;
            sellwho = pSearchInfo.Who;
            marketname = pSearchInfo.MarketName;
            SearchStr = "EXEC UM_CHECK_INDEX_BUY \'" + marketname + "\',\'" + sellwho + "\'," + Index.ToString() + "," + CheckType.ToString() + "," + ChangeTYpe.ToString();
            //FADOQuery.SQL.Clear();
            //FADOQuery.SQL.ADD(SearchStr);
            //try
            //{
            //    FADOQuery.ExecSql;
            //}
            //catch
            //{
            //    svMain.MainOutMessage("Exception) TFrmSql.BuyOnUserMarket -> Open");
            //    for (i = 0; i < FADOQuery.SQL.Count; i++)
            //    {
            //        svMain.MainOutMessage(" :" + FADOQuery.SQL[i]);
            //    }
            //    result = Grobal2.UMResult_ReadFail;
            //    return result;
            //}
            //result = Grobal2.UMResult_Success;
            //if (FADOQuery.Active)
            //{
            //    //VisualStyleElement.ToolTip.Close;
            //}
            return result;
        }

        public int ChkCancelUserMarket(TSearchSellItem pSearchInfo, bool IsSucess)
        {
            string SearchStr;
            int CheckType;
            int ChangeTYpe;
            int Index;
            string sellwho;
            string marketname;
            int result = Grobal2.UMResult_Fail;
            CheckType = Grobal2.MARKET_DBSELLTYPE_READYCANCEL;
            if (IsSucess)
            {
                ChangeTYpe = Grobal2.MARKET_DBSELLTYPE_DELETE;
            }
            else
            {
                ChangeTYpe = Grobal2.MARKET_DBSELLTYPE_SELL;
            }
            Index = pSearchInfo.SellIndex;
            sellwho = pSearchInfo.Who;
            marketname = pSearchInfo.MarketName;
            SearchStr = "EXEC UM_CHECK_INDEX \'" + marketname + "\',\'" + sellwho + "\'," + Index.ToString() + "," + CheckType.ToString() + "," + ChangeTYpe.ToString();
            //FADOQuery.SQL.Clear();
            //FADOQuery.SQL.ADD(SearchStr);
            //try
            //{
            //    FADOQuery.ExecSql;
            //}
            //catch
            //{
            //    svMain.MainOutMessage("Exception) TFrmSql.BuyOnUserMarket -> Open");
            //    for (var i = 0; i < FADOQuery.SQL.Count; i++)
            //    {
            //        svMain.MainOutMessage(" :" + FADOQuery.SQL[i]);
            //    }
            //    result = Grobal2.UMResult_ReadFail;
            //    return result;
            //}
            //result = Grobal2.UMResult_Success;
            //if (FADOQuery.Active)
            //{
            //    //VisualStyleElement.ToolTip.Close;
            //}
            return result;
        }

        public int ChkGetPayUserMarket(TSearchSellItem pSearchInfo, bool IsSucess)
        {
            int ChangeTYpe;
            int result = Grobal2.UMResult_Fail;
            int CheckType = Grobal2.MARKET_DBSELLTYPE_READYGETPAY;
            if (IsSucess)
            {
                ChangeTYpe = Grobal2.MARKET_DBSELLTYPE_DELETE;
            }
            else
            {
                ChangeTYpe = Grobal2.MARKET_DBSELLTYPE_BUY;
            }
            int Index = pSearchInfo.SellIndex;
            string sellwho = pSearchInfo.Who;
            string marketname = pSearchInfo.MarketName;
            string SearchStr = "EXEC UM_CHECK_INDEX \'" + marketname + "\',\'" + sellwho + "\'," + Index.ToString() + "," + CheckType.ToString() + "," + ChangeTYpe.ToString();
            //FADOQuery.SQL.Clear();
            //FADOQuery.SQL.ADD(SearchStr);
            //try
            //{
            //    FADOQuery.ExecSql;
            //}
            //catch
            //{
            //    svMain.MainOutMessage("Exception) TFrmSql.BuyOnUserMarket -> Open");
            //    for (var i = 0; i < FADOQuery.SQL.Count; i++)
            //    {
            //        svMain.MainOutMessage(" :" + FADOQuery.SQL[i]);
            //    }
            //    result = Grobal2.UMResult_ReadFail;
            //    return result;
            //}
            //result = Grobal2.UMResult_Success;
            //if (FADOQuery.Active)
            //{
            //    //VisualStyleElement.ToolTip.Close;
            //}
            return result;
        }

        private void LoadBoardListFromDB(TGaBoardArticleLoad pList, IDataReader SqlDB)
        {
            char[] content = new char[500 + 1];
            /*pList.AgitNum = SqlDB.FieldByName("FLD_AGITNUM").AsInteger;
            pList.GuildName = SqlDB.FieldByName("FLD_GUILDNAME").AsString.Trim();
            pList.OrgNum = SqlDB.FieldByName("FLD_ORGNUM").AsInteger;
            pList.SrcNum1 = SqlDB.FieldByName("FLD_SRCNUM1").AsInteger;
            pList.SrcNum2 = SqlDB.FieldByName("FLD_SRCNUM2").AsInteger;
            pList.SrcNum3 = SqlDB.FieldByName("FLD_SRCNUM3").AsInteger;
            pList.UserName = SqlDB.FieldByName("FLD_USERNAME").AsString.Trim();*/
            //FillChar(pList.Content, sizeof(pList.Content), '\0');
            //StrPLCopy(pList.Content, SqlDB.FieldByName("FLD_CONTENT").AsString.Trim(), sizeof(pList.Content) - 1);
        }

        public int LoadPageGaBoardList(string gname, int nKind, ArrayList BoardList)
        {
            string SearchStr;
            int result = Grobal2.UMResult_Fail;
            if (gname == "")
            {
                return result;
            }
            SearchStr = "EXEC GABOARD_LOAD \'" + gname + "\'," + DBSQL.KIND_NOTICE.ToString();
            //FADOQuery.SQL.Clear();
            //FADOQuery.SQL.ADD(SearchStr);
            //try
            //{
            //    if (!FADOQuery.Active)
            //    {
            //        FADOQuery.Open;
            //    }
            //}
            //catch
            //{
            //    svMain.MainOutMessage("Exception) TDBSql.LoadPageGaBoardList -> Open");
            //    for (var i = 0; i < FADOQuery.SQL.Count; i++)
            //    {
            //        svMain.MainOutMessage(" :" + FADOQuery.SQL[i]);
            //    }
            //    return Grobal2.UMResult_ReadFail;
            //}
            //FADOQuery.First;
            //if (FADOQuery.RecordCount <= DBSQL.GABOARD_NOTICE_LINE)
            //{
            //    for (var i = 0; i < FADOQuery.RecordCount; i++)
            //    {
            //        pArticle = new TGaBoardArticleLoad();
            //        LoadBoardListFromDB(pArticle, FADOQuery);
            //        BoardList.Add(pArticle);
            //        if (!EOF)
            //        {
            //            FADOQuery.Next;
            //        }
            //    }
            //    for (var i = FADOQuery.RecordCount; i < DBSQL.GABOARD_NOTICE_LINE; i++)
            //    {
            //        pArticle = new TGaBoardArticleLoad();
            //        pArticle.AgitNum = 0;
            //        pArticle.OrgNum = 0;
            //        pArticle.SrcNum1 = 0;
            //        pArticle.SrcNum2 = 0;
            //        pArticle.SrcNum3 = 0;
            //        pArticle.Kind = DBSQL.KIND_NOTICE;
            //        pArticle.UserName = "巩林";
            //        pArticle.Content = "巩林 傍瘤荤亲捞 厚绢 乐嚼聪促.";
            //        BoardList.Add(pArticle);
            //    }
            //}
            //else
            //{
            //    for (var i = 0; i < DBSQL.GABOARD_NOTICE_LINE; i++)
            //    {
            //        pArticle = new TGaBoardArticleLoad();
            //        LoadBoardListFromDB(pArticle, FADOQuery);
            //        BoardList.Add(pArticle);
            //        if (!EOF)
            //        {
            //            FADOQuery.Next;
            //        }
            //    }
            //}
            //if (FADOQuery.Active)
            //{
            //    //VisualStyleElement.ToolTip.Close;
            //}
            //SearchStr = "EXEC GABOARD_LOAD \'" + gname + "\'," + nKind.ToString();
            //FADOQuery.SQL.Clear();
            //FADOQuery.SQL.ADD(SearchStr);
            //try
            //{
            //    if (!FADOQuery.Active)
            //    {
            //        FADOQuery.Open;
            //    }
            //}
            //catch
            //{
            //    svMain.MainOutMessage("Exception) TDBSql.LoadPageGaBoardList -> Open");
            //    for (var i = 0; i < FADOQuery.SQL.Count; i++)
            //    {
            //        svMain.MainOutMessage(" :" + FADOQuery.SQL[i]);
            //    }
            //    return Grobal2.UMResult_ReadFail;
            //}
            //FADOQuery.First;
            //for (var i = 0; i < FADOQuery.RecordCount; i++)
            //{
            //    pArticle = new TGaBoardArticleLoad();
            //    LoadBoardListFromDB(pArticle, FADOQuery);
            //    BoardList.Add(pArticle);
            //    if (!EOF)
            //    {
            //        FADOQuery.Next;
            //    }
            //}
            //if (FADOQuery.Active)
            //{
            //    //VisualStyleElement.ToolTip.Close;
            //}
            return Grobal2.UMResult_Success;
        }

        public int AddGaBoardArticle(TGaBoardArticleLoad pArticleLoad)
        {
            int result = Grobal2.UMResult_Fail;
            //FADOQuery.SQL.Clear();
            //FADOQuery.SQL.Add("INSERT INTO TBL_GABOARD Values(" + pArticleLoad.AgitNum.ToString() + ",\'" + pArticleLoad.GuildName + "\'," + pArticleLoad.OrgNum.ToString() + "," + pArticleLoad.SrcNum1.ToString() + "," + pArticleLoad.SrcNum2.ToString() + "," + pArticleLoad.SrcNum3.ToString() + "," + pArticleLoad.Kind.ToString() + ",\'" + pArticleLoad.UserName + "\',\'" + pArticleLoad.Content + "\'" + ")");
            //try
            //{
            //    FADOQuery.ExecSQL;
            //    result = Grobal2.UMResult_Success;
            //}
            //catch
            //{
            //    svMain.MainOutMessage("Exception) TDBSql.AddGaBoardArticle -> ExecSQL");
            //    for (var i = 0; i < FADOQuery.SQL.Count; i++)
            //    {
            //        svMain.MainOutMessage(" :" + FADOQuery.SQL[i]);
            //    }
            //}
            return result;
        }

        public int DelGaBoardArticle(TGaBoardArticleLoad pArticleLoad)
        {
            int result;
            string SearchStr;
            result = Grobal2.UMResult_Fail;
            if (pArticleLoad == null)
            {
                return result;
            }
            if (pArticleLoad.GuildName == "")
            {
                return result;
            }
            if (pArticleLoad.UserName == "")
            {
                return result;
            }
            SearchStr = "EXEC GABOARD_DEL \'" + pArticleLoad.GuildName + "\'," + pArticleLoad.OrgNum.ToString() + "," + pArticleLoad.SrcNum1.ToString() + "," + pArticleLoad.SrcNum2.ToString() + "," + pArticleLoad.SrcNum3.ToString();
            //FADOQuery.SQL.Clear();
            //FADOQuery.SQL.ADD(SearchStr);
            //try
            //{
            //    FADOQuery.ExecSQL;
            //    result = Grobal2.UMResult_Success;
            //}
            //catch
            //{
            //    svMain.MainOutMessage("Exception) TDBSql.DelGaBoardArticle -> ExecSQL");
            //    for (i = 0; i < FADOQuery.SQL.Count; i++)
            //    {
            //        svMain.MainOutMessage(" :" + FADOQuery.SQL[i]);
            //    }
            //}
            return result;
        }

        public int EditGaBoardArticle(TGaBoardArticleLoad pArticleLoad)
        {
            int result = Grobal2.UMResult_Fail;
            //FADOQuery.SQL.Clear();
            //FADOQuery.SQL.Add("UPDATE TBL_GABOARD SET FLD_CONTENT = \'" + pArticleLoad.Content + "\' WHERE " + "FLD_GUILDNAME = \'" + pArticleLoad.GuildName + "\' AND " + "FLD_ORGNUM = " + pArticleLoad.OrgNum.ToString() + " AND " + "FLD_SRCNUM1 = " + pArticleLoad.SrcNum1.ToString() + " AND " + "FLD_SRCNUM2 = " + pArticleLoad.SrcNum2.ToString() + " AND " + "FLD_SRCNUM3 = " + pArticleLoad.SrcNum3.ToString());
            //try
            //{
            //    FADOQuery.ExecSQL;
            //    result = Grobal2.UMResult_Success;
            //}
            //catch
            //{
            //    svMain.MainOutMessage("Exception) TDBSql.EditGaBoardArticle -> ExecSQL");
            //    for (var i = 0; i < FADOQuery.SQL.Count; i++)
            //    {
            //        svMain.MainOutMessage(" :" + FADOQuery.SQL[i]);
            //    }
            //}
            return result;
        }
    }
}

namespace GameSvr
{
    public class DBSQL
    {
        public static TDBSql g_DBSQL = null;
        public const int GABOARD_NOTICE_LINE = 3;
        public const int KIND_NOTICE = 0;
    } // end DBSQL

}

