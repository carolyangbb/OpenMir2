using System;
using System.Collections;
using System.Collections.Generic;
using SystemModule;

namespace GameSvr
{
    public class TMerchant : TNormNpc
    {
        public string MarketName = string.Empty;
        public byte MarketType = 0;
        public int PriceRate = 0;
        public bool NoSeal = false;
        public bool BoCastleManage = false;
        public bool BoHiddenNpc = false;
        public int fSaveToFileCount = 0;
        public int CreateIndex = 0;
        private long checkrefilltime = 0;
        private long checkverifytime = 0;
        public ArrayList DealGoods = null;
        public ArrayList ProductList = null;
        public IList<IList<TUserItem>> GoodsList = null;
        public ArrayList PriceList = null;
        public IList<TUpgradeInfo> UpgradingList = null;

        public TMerchant() : base()
        {
            this.RaceImage = Grobal2.RCC_MERCHANT;
            this.Appearance = 0;
            PriceRate = 100;
            NoSeal = false;
            BoCastleManage = false;
            BoHiddenNpc = false;
            DealGoods = new ArrayList();
            ProductList = new ArrayList();
            GoodsList = new List<IList<TUserItem>>();
            PriceList = new ArrayList();
            UpgradingList = new List<TUpgradeInfo>();
            checkrefilltime = HUtil32.GetTickCount();
            checkverifytime = HUtil32.GetTickCount();
            fSaveToFileCount = 0;
            CreateIndex = 0;
        }

        public void ClearMerchantInfos()
        {
            for (var i = 0; i < ProductList.Count; i++)
            {
                Dispose((TMarketProduct)ProductList[i]);
            }
            ProductList.Clear();
            DealGoods.Clear();
            this.ClearNpcInfos();
        }

        public void LoadMerchantInfos()
        {
            this.NpcBaseDir = LocalDB.MARKETDEFDIR;
            LocalDB.FrmDB.LoadMarketDef(this, LocalDB.MARKETDEFDIR, MarketName + "-" + this.MapName, true);
        }

        public void LoadMarketSavedGoods()
        {
            LocalDB.FrmDB.LoadMarketSavedGoods(this, MarketName + "-" + this.MapName);
            LocalDB.FrmDB.LoadMarketPrices(this, MarketName + "-" + this.MapName);
            LoadUpgradeItemList();
        }

        private IList<TUserItem> GetGoodsList(int gindex)
        {
            IList<TUserItem> result = null;
            IList<TUserItem> l;
            if (gindex > 0)
            {
                try
                {
                    for (var i = 0; i < GoodsList.Count; i++)
                    {
                        l = GoodsList[i];
                        if (l.Count > 0)
                        {
                            if (l[0].Index == gindex)
                            {
                                result = l;
                                break;
                            }
                        }
                    }
                }
                catch
                {
                }
            }
            return result;
        }

        public void PriceUp(int index)
        {
            for (var i = 0; i < PriceList.Count; i++)
            {
                if (((TPricesInfo)PriceList[i]).Index == index)
                {
                    int price = ((TPricesInfo)PriceList[i]).SellPrice;
                    if (price < HUtil32.MathRound(price * 1.1))
                    {
                        price = HUtil32.MathRound(price * 1.1);
                    }
                    else
                    {
                        price = price + 1;
                    }
                    return;
                }
            }
            TStdItem pstd = M2Share.UserEngine.GetStdItem(index);
            if (pstd != null)
            {
                NewPrice(index, HUtil32.MathRound(pstd.Price * 1.1));
            }
        }

        public void PriceDown(int index)
        {
            decimal price;
            for (var i = 0; i < PriceList.Count; i++)
            {
                if (((TPricesInfo)PriceList[i]).Index == index)
                {
                    price = ((TPricesInfo)PriceList[i]).SellPrice;
                    if (price > HUtil32.MathRound(price / 1.1M))
                    {
                        price = HUtil32.MathRound(price / 1.1M);
                    }
                    else
                    {
                        price = price - 1;
                    }
                    price = _MAX(2, (int)price);
                    return;
                }
            }
            TStdItem pstd = M2Share.UserEngine.GetStdItem(index);
            if (pstd != null)
            {
                NewPrice(index, HUtil32.MathRound(pstd.Price * 1.1));
            }
        }

        public void NewPrice(int index, decimal price)
        {
            TPricesInfo pi = new TPricesInfo();
            pi.Index = (short)index;
            pi.SellPrice = (int)price;
            PriceList.Add(pi);
            LocalDB.FrmDB.WriteMarketPrices(this, MarketName + "-" + this.MapName);
        }

        // 拱扒狼 措钎 啊拜
        public int GetPrice(int index)
        {
            int price = -2;
            for (var i = 0; i < PriceList.Count; i++)
            {
                if (((TPricesInfo)PriceList[i]).Index == index)
                {
                    price = ((TPricesInfo)PriceList[i]).SellPrice;
                    break;
                }
            }
            if (price < 0)
            {
                TStdItem pstd = M2Share.UserEngine.GetStdItem(index);
                if ((pstd != null) && IsDealingItem(pstd.StdMode, pstd.Shape))
                {
                    price = pstd.Price;
                }
            }
            return price;
        }

        private int GetGoodsPrice(TUserItem uitem)
        {
            int result;
            int i;
            int price;
            int upg;
            double dam;
            TStdItem pstd;
            price = GetPrice(uitem.Index);
            if (price > 0)
            {
                pstd = M2Share.UserEngine.GetStdItem(uitem.Index);
                if (pstd != null)
                {
                    if ((pstd.OverlapItem < 1) && (pstd.StdMode > 4) && (pstd.DuraMax > 0) && (uitem.DuraMax > 0) && (pstd.StdMode != 8))
                    {
                        if (pstd.StdMode == 40)
                        {
                            if (uitem.Dura <= uitem.DuraMax)
                            {
                                dam = price / 2 / uitem.DuraMax * (uitem.DuraMax - uitem.Dura);
                                price = _MAX(2, HUtil32.MathRound(price - dam));
                            }
                            else
                            {
                                price = price + HUtil32.MathRound((uitem.Dura - uitem.DuraMax) * price / uitem.DuraMax * 2);
                            }
                        }
                        if (pstd.StdMode == 43)
                        {
                            if (uitem.DuraMax < 10000)
                            {
                                uitem.DuraMax = 10000;
                            }
                            if (uitem.Dura <= uitem.DuraMax)
                            {
                                dam = price / 2 / uitem.DuraMax * (uitem.DuraMax - uitem.Dura);
                                price = _MAX(2, HUtil32.MathRound(price - dam));
                            }
                            else
                            {
                                price = price + HUtil32.MathRound((uitem.Dura - uitem.DuraMax) * (price / uitem.DuraMax * 1.3));
                            }
                        }
                        if ((pstd.OverlapItem < 1) && (pstd.StdMode > 4))
                        {
                            upg = 0;
                            for (i = 0; i <= 7; i++)
                            {
                                if ((pstd.StdMode == 5) || (pstd.StdMode == 6))
                                {
                                    if ((i == 4) || (i == 9))
                                    {
                                        continue;
                                    }
                                    if (i == 6)
                                    {
                                        if (uitem.Desc[i] > 10)
                                        {
                                            upg = upg + (uitem.Desc[i] - 10) * 2;
                                        }
                                        continue;
                                    }
                                    upg = upg + uitem.Desc[i];
                                }
                                else
                                {
                                    upg = upg + uitem.Desc[i];
                                }
                            }
                            if (upg > 0)
                            {
                                price = price + price / 5 * upg;
                            }
                            price = HUtil32.MathRound(price / pstd.DuraMax * uitem.DuraMax);
                            dam = price / 2 / uitem.DuraMax * (uitem.DuraMax - uitem.Dura);
                            price = _MAX(2, HUtil32.MathRound(price - dam));
                        }
                    }
                }
            }
            result = price;
            return result;
        }

        private int GetSellPrice(TUserHuman whocret, int price)
        {
            int result;
            int prate;
            if (BoCastleManage)
            {
                if (M2Share.UserCastle.IsOurCastle(whocret.MyGuild))
                {
                    prate = _MAX(60, HUtil32.MathRound(PriceRate * 0.8));
                    result = HUtil32.MathRound(price / 100 * prate);
                }
                else
                {
                    result = HUtil32.MathRound(price / 100 * PriceRate);
                }
            }
            else
            {
                result = HUtil32.MathRound(price / 100 * PriceRate);
            }
            return result;
        }

        private decimal GetBuyPrice(int price)
        {
            return HUtil32.MathRound(price / 2);
        }

        public bool IsDealingItem(int stdmode, int shape)
        {
            bool result;
            int i;
            int _stdmode;
            int _shape;
            string str1 = string.Empty;
            string str2 = string.Empty;
            result = false;
            for (i = 0; i < DealGoods.Count; i++)
            {
                str2 = HUtil32.GetValidStr3((string)DealGoods[i], ref str1, new string[] { ",", " " });
                _stdmode = HUtil32.Str_ToInt(str1, -1);
                _shape = HUtil32.Str_ToInt(str2, -1);
                if (_stdmode == stdmode)
                {
                    if (_shape != -1)
                    {
                        if (_shape == shape)
                        {
                            result = true;
                            break;
                        }
                    }
                    else
                    {
                        result = true;
                        break;
                    }
                }
            }
            return result;
        }

        public void RefillGoods_RefillNow(ref IList<TUserItem> list, string itemname, int fcount)
        {
            if (list == null)
            {
                list = new List<TUserItem>();
                GoodsList.Add(list);
            }
            for (var i = 0; i < fcount; i++)
            {
                TUserItem pu = new TUserItem();
                if (M2Share.UserEngine.CopyToUserItemFromName(itemname, ref pu))
                {
                    TStdItem ps = M2Share.UserEngine.GetStdItem(pu.Index);
                    if (ps != null)
                    {
                        if (ps.OverlapItem >= 1)
                        {
                            pu.Dura = 1;
                        }
                        list.Insert(0, pu);
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
        }

        public void RefillGoods_WasteNow(ref IList<TUserItem> list, int wcount)
        {
            for (var i = list.Count - 1; i >= 0; i--)
            {
                if (wcount <= 0)
                {
                    break;
                }
                try
                {
                    Dispose(list[i]);
                }
                finally
                {
                    list.RemoveAt(i);
                }
                wcount -= 1;
            }
        }

        private void RefillGoods()
        {
            int i;
            int j;
            int k;
            int stock;
            int gindex;
            TMarketProduct pp;
            IList<TUserItem> list;
            IList<TUserItem> l;
            bool flag;
            int step;
            bool ItemChanged;
            ItemChanged = false;
            i = 0;
            step = 0;
            try
            {
                step = 0;
                for (i = 0; i < ProductList.Count; i++)
                {
                    step = 1;
                    pp = (TMarketProduct)ProductList[i];
                    if (HUtil32.GetTickCount() - pp.ZenTime > ((long)pp.ZenHour) * 60 * 1000)
                    {
                        step = 3;
                        pp.ZenTime = HUtil32.GetTickCount();
                        gindex = M2Share.UserEngine.GetStdItemIndex(pp.GoodsName);
                        if (gindex >= 0)
                        {
                            step = 4;
                            list = null;
                            list = GetGoodsList(gindex);
                            stock = 0;
                            if (list != null)
                            {
                                stock = list.Count;
                            }
                            if (stock < pp.Count)
                            {
                                step = 5;
                                PriceUp(gindex);
                                RefillGoods_RefillNow(ref list, pp.GoodsName, pp.Count - stock);
                                ItemChanged = true;
                                LocalDB.FrmDB.WriteMarketSavedGoods(this, MarketName + "-" + this.MapName);
                                LocalDB.FrmDB.WriteMarketPrices(this, MarketName + "-" + this.MapName);
                                step = 6;
                            }
                            if (stock > pp.Count)
                            {
                                step = 7;
                                RefillGoods_WasteNow(ref list, stock - pp.Count);
                                ItemChanged = true;
                                LocalDB.FrmDB.WriteMarketSavedGoods(this, MarketName + "-" + this.MapName);
                                LocalDB.FrmDB.WriteMarketPrices(this, MarketName + "-" + this.MapName);
                                step = 8;
                            }
                        }
                    }
                }
                if (ItemChanged)
                {
                    if (fSaveToFileCount >= 10)
                    {
                        LocalDB.FrmDB.WriteMarketSavedGoods(this, MarketName + "-" + this.MapName);
                        LocalDB.FrmDB.WriteMarketPrices(this, MarketName + "-" + this.MapName);
                        fSaveToFileCount = 0;
                    }
                    else
                    {
                        fSaveToFileCount++;
                    }
                }
                for (j = 0; j < GoodsList.Count; j++)
                {
                    step = 9;
                    l = GoodsList[j];
                    step = 10;
                    if (l.Count > 1000)
                    {
                        flag = false;
                        for (k = 0; k < ProductList.Count; k++)
                        {
                            step = 11;
                            pp = (TMarketProduct)ProductList[k];
                            gindex = M2Share.UserEngine.GetStdItemIndex(pp.GoodsName);
                            if (l[0].Index == gindex)
                            {
                                step = 12;
                                flag = true;
                                break;
                            }
                        }
                        step = 13;
                        if (!flag)
                        {
                            RefillGoods_WasteNow(ref l, l.Count - 1000);
                        }
                        else
                        {
                            RefillGoods_WasteNow(ref l, l.Count - 5000);
                        }
                    }
                }
            }
            catch
            {
                M2Share.MainOutMessage("Merchant RefillGoods Exception..Step=(" + step.ToString() + ")");
            }
        }

        public override void CheckNpcSayCommand(TUserHuman hum, ref string source, string tag)
        {
            base.CheckNpcSayCommand(hum, ref source, tag);
            if (tag == "$PRICERATE")
            {
                source = this.ChangeNpcSayTag(source, "<$PRICERATE>", PriceRate.ToString());
            }
            if (tag == "$UPGRADEWEAPONFEE")
            {
                source = this.ChangeNpcSayTag(source, "<$UPGRADEWEAPONFEE>", ObjNpc.UPGRADEWEAPONFEE.ToString());
            }
            if (tag == "$USERWEAPON")
            {
                if (hum.UseItems[Grobal2.U_WEAPON].Index != 0)
                {
                    source = this.ChangeNpcSayTag(source, "<$USERWEAPON>", M2Share.UserEngine.GetStdItemName(hum.UseItems[Grobal2.U_WEAPON].Index));
                }
                else
                {
                    source = this.ChangeNpcSayTag(source, "<$USERWEAPON>", "Weapon");
                }
            }
        }

        public override void UserCall(TCreature caller)
        {
            this.NpcSayTitle(caller, "@main");
        }

        private void SaveUpgradeItemList()
        {
            try
            {
                LocalDB.FrmDB.WriteMarketUpgradeInfos(this.UserName, UpgradingList);
            }
            catch
            {
                M2Share.MainOutMessage("Failure in saving upgradinglist - " + this.UserName);
            }
        }

        private void LoadUpgradeItemList()
        {
            for (var i = 0; i < UpgradingList.Count; i++)
            {
                Dispose(UpgradingList[i]);
            }
            UpgradingList.Clear();
            try
            {
                LocalDB.FrmDB.LoadMarketUpgradeInfos(this.UserName, UpgradingList);
            }
            catch
            {
                M2Share.MainOutMessage("Failure in loading upgradinglist - " + this.UserName);
            }
        }

        private void VerifyUpgradeList()
        {
            TUpgradeInfo pup = null;
            double realdate = 0;
            int old = 0;
            for (var i = UpgradingList.Count - 1; i >= 0; i--)
            {
                pup = UpgradingList[i];
                if (pup != null)
                {
                    //realdate = ((double)DateTime.Today) - ((double)pup.readydate);
                    try
                    {
                        old = HUtil32.MathRound(realdate);
                    }
                    catch (Exception)
                    {
                        old = 0;
                    }
                    if (old >= 8)
                    {
                        Dispose(pup);
                        UpgradingList.RemoveAt(i);
                    }
                }
                else
                {
                    M2Share.MainOutMessage("pup Is Nil... ");
                }
            }
        }

        public void UserSelectUpgradeWeapon_PrepareWeaponUpgrade(IList<TUserItem> ilist, ref byte adc, ref byte asc, ref byte amc, ref byte dura)
        {
            int i;
            int k;
            int d;
            int s;
            int m;
            int dctop;
            int dcsec;
            int sctop;
            int scsec;
            int mctop;
            int mcsec;
            int durasum;
            int duracount;
            TStdItem ps;
            ArrayList dellist;
            ArrayList sumlist;
            TStdItem std;
            dctop = 0;
            dcsec = 0;
            sctop = 0;
            scsec = 0;
            mctop = 0;
            mcsec = 0;
            durasum = 0;
            duracount = 0;
            dellist = null;
            sumlist = new ArrayList();
            for (i = ilist.Count - 1; i >= 0; i--)
            {
                if (M2Share.UserEngine.GetStdItemName(ilist[i].Index) == M2Share.__BlackStone)
                {
                    sumlist.Add(HUtil32.MathRound(ilist[i].Dura / 1000));
                    if (dellist == null)
                    {
                        dellist = new ArrayList();
                    }
                    //dellist.Add(svMain.__BlackStone, ilist[i].MakeIndex as Object);
                    Dispose(ilist[i]);
                    ilist.RemoveAt(i);
                }
                else
                {
                    if (M2Share.IsUpgradeWeaponStuff(ilist[i].Index))
                    {
                        ps = M2Share.UserEngine.GetStdItem(ilist[i].Index);
                        if (ps != null)
                        {
                            std = ps;
                            M2Share.ItemMan.GetUpgradeStdItem(ilist[i], ref std);
                            d = 0;
                            s = 0;
                            m = 0;
                            switch (std.StdMode)
                            {
                                case 19:
                                case 20:
                                case 21:
                                    // 格吧捞
                                    d = LoByte(std.DC) + HiByte(std.DC);
                                    s = LoByte(std.SC) + HiByte(std.SC);
                                    m = LoByte(std.MC) + HiByte(std.MC);
                                    break;
                                case 22:
                                case 23:
                                    // 馆瘤
                                    d = LoByte(std.DC) + HiByte(std.DC);
                                    s = LoByte(std.SC) + HiByte(std.SC);
                                    m = LoByte(std.MC) + HiByte(std.MC);
                                    break;
                                case 24:
                                case 26:
                                    // 迫骂
                                    d = LoByte(std.DC) + HiByte(std.DC) + 1;
                                    s = LoByte(std.SC) + HiByte(std.SC) + 1;
                                    m = LoByte(std.MC) + HiByte(std.MC) + 1;
                                    break;
                            }
                            if (dctop < d)
                            {
                                dcsec = dctop;
                                dctop = d;
                            }
                            else if (dcsec < d)
                            {
                                dcsec = d;
                            }
                            if (sctop < s)
                            {
                                scsec = sctop;
                                sctop = s;
                            }
                            else if (scsec < s)
                            {
                                scsec = s;
                            }
                            if (mctop < m)
                            {
                                mcsec = mctop;
                                mctop = m;
                            }
                            else if (mcsec < m)
                            {
                                mcsec = m;
                            }
                            if (dellist == null)
                            {
                                dellist = new ArrayList();
                            }
                            //dellist.Add(ps.Name, ilist[i].MakeIndex as Object);
                            //svMain.AddUserLog("26\09" + hum.MapName + "\09" + hum.CX.ToString() + "\09" + hum.CY.ToString() + "\09" + hum.UserName + "\09" + svMain.UserEngine.GetStdItemName(ilist[i].Index) + "\09" + ilist[i].MakeIndex.ToString() + "\09" + "1\09" + this.ItemOptionToStr(ilist[i].Desc));
                            Dispose(ilist[i]);
                            ilist.RemoveAt(i);
                        }
                    }
                }
            }
            for (i = 0; i < sumlist.Count; i++)
            {
                for (k = sumlist.Count - 1; k > i; k--)
                {
                    if (((int)sumlist[k]) > ((int)sumlist[k - 1]))
                    {
                        // sumlist.Exchange(k, k - 1);
                    }
                }
            }
            for (i = 0; i < sumlist.Count; i++)
            {
                durasum = durasum + ((int)sumlist[i]);
                duracount++;
                if (duracount >= 5)
                {
                    break;
                }
            }
            dura = (byte)HUtil32.MathRound(Convert.ToDouble(_MIN(5, duracount) + durasum / duracount / 5 * _MIN(5, duracount)));
            adc = (byte)(dctop + dctop / 5 + dcsec / 3);
            asc = (byte)(sctop + sctop / 5 + scsec / 3);
            amc = (byte)(mctop + mctop / 5 + mcsec / 3);
            if (dellist != null)
            {
                //  hum.SendMsg(hum, Grobal2.RM_DELITEMS, 0, (int)dellist, 0, 0, "");
            }
            if (sumlist != null)
            {
                sumlist.Free();
            }
        }

        public void UserSelectUpgradeWeapon(TUserHuman hum)
        {
            int i;
            bool flag;
            TUpgradeInfo pup;
            TStdItem pstd;
            flag = false;
            // 甸绊 乐绰 公扁狼 诀弊饭捞靛甫 该变促.
            for (i = 0; i < UpgradingList.Count; i++)
            {
                if (hum.UserName == UpgradingList[i].UserName)
                {
                    this.NpcSayTitle(hum, "~@upgradenow_ing");
                    return;
                }
            }
            if (hum.UseItems[Grobal2.U_WEAPON].Index != 0)
            {
                // --------------------------------------
                // 蜡聪农酒捞袍篮 力访 给该扁霸...
                pstd = M2Share.UserEngine.GetStdItem(hum.UseItems[Grobal2.U_WEAPON].Index);
                if (pstd != null)
                {
                    if (pstd.UniqueItem == 1)
                    {
                        hum.BoxMsg("稀有物品不能被升级。", 0);
                        return;
                    }
                }
                // --------------------------------------
                if (hum.Gold >= ObjNpc.UPGRADEWEAPONFEE)
                {
                    // 捣捞 乐绰瘤
                    if (hum.FindItemName(M2Share.__BlackStone) != null)
                    {
                        // 孺枚阑 啊瘤绊 乐绰瘤
                        hum.DecGold(ObjNpc.UPGRADEWEAPONFEE);
                        if (BoCastleManage)
                        {
                            // 5%狼 技陛捞 叭腮促.
                            M2Share.UserCastle.PayTax(ObjNpc.UPGRADEWEAPONFEE);
                        }
                        hum.GoldChanged();
                        // 啊规俊 乐绰 酒捞袍阑 根顶 持绰促.
                        pup = new TUpgradeInfo();
                        pup.UserName = hum.UserName;
                        pup.uitem = hum.UseItems[Grobal2.U_WEAPON];
                        // 肺弊巢辫
                        // 诀嘎_ +
                        M2Share.AddUserLog("25\09" + hum.MapName + "\09" + hum.CX.ToString() + "\09" + hum.CY.ToString() + "\09" + hum.UserName + "\09" + M2Share.UserEngine.GetStdItemName(hum.UseItems[Grobal2.U_WEAPON].Index) + "\09" + hum.UseItems[Grobal2.U_WEAPON].MakeIndex.ToString() + "\09" + "1\09" + this.ItemOptionToStr(this.UseItems[Grobal2.U_WEAPON].Desc));
                        hum.SendDelItem(hum.UseItems[Grobal2.U_WEAPON]);
                        // 努扼捞攫飘俊 绝绢柳芭 焊晨
                        hum.UseItems[Grobal2.U_WEAPON].Index = 0;
                        hum.RecalcAbilitys();
                        hum.FeatureChanged();
                        hum.SendMsg(hum, Grobal2.RM_ABILITY, 0, 0, 0, 0, "");
                        // hum.SendMsg (hum, RM_SUBABILITY, 0, 0, 0, 0, '');
                        UserSelectUpgradeWeapon_PrepareWeaponUpgrade(hum.ItemList, ref pup.updc, ref pup.upsc, ref pup.upmc, ref pup.durapoint);
                        pup.readydate = DateTime.Now;
                        pup.readycount = HUtil32.GetTickCount();
                        UpgradingList.Add(pup);
                        SaveUpgradeItemList();
                        flag = true;
                    }
                }
            }
            if (flag)
            {
                this.NpcSayTitle(hum, "~@upgradenow_ok");
            }
            else
            {
                this.NpcSayTitle(hum, "~@upgradenow_fail");
            }
        }

        public void UserSelectGetBackUpgrade(TUserHuman hum)
        {
            int i;
            int per;
            int state;
            int rand;
            TUpgradeInfo pup;
            TUserItem pu;
            state = 0;
            pup = null;
            if (hum.CanAddItem())
            {
                for (i = 0; i < UpgradingList.Count; i++)
                {
                    if (hum.UserName == UpgradingList[i].UserName)
                    {
                        state = 1;
                        if ((HUtil32.GetTickCount() - UpgradingList[i].readycount > 60 * 60 * 1000) || (hum.UserDegree >= Grobal2.UD_ADMIN))
                        {
                            pup = UpgradingList[i];
                            UpgradingList.RemoveAt(i);
                            SaveUpgradeItemList();
                            state = 2;
                            break;
                        }
                    }
                }
                if (pup != null)
                {
                    switch (pup.durapoint)
                    {
                        // 郴备 搬沥
                        // Modify the A .. B: 0 .. 8
                        case 0:
                            // n := _MAX(3000, pup.uitem.DuraMax div 2);
                            if (pup.uitem.DuraMax > 3000)
                            {
                                pup.uitem.DuraMax = (short)(pup.uitem.DuraMax - 3000);
                            }
                            else
                            {
                                pup.uitem.DuraMax = (short)(pup.uitem.DuraMax / 2);
                            }
                            if (pup.uitem.Dura > pup.uitem.DuraMax)
                            {
                                pup.uitem.Dura = pup.uitem.DuraMax;
                            }
                            break;
                        // Modify the A .. B: 9 .. 15
                        case 9:
                            if (new System.Random(pup.durapoint).Next() < 6)
                            {
                                pup.uitem.DuraMax = (short)_MAX(0, pup.uitem.DuraMax - 1000);
                            }
                            break;
                        // DURAMAX荐沥
                        // 16..19
                        // Modify the A .. B: 18 .. 255
                        case 18:
                            switch (new System.Random(pup.durapoint - 18).Next())
                            {
                                // Modify the A .. B: 1 .. 4
                                case 1:
                                    pup.uitem.DuraMax = (short)(pup.uitem.DuraMax + 1000);
                                    break;
                                // Modify the A .. B: 5 .. 7
                                case 5:
                                    pup.uitem.DuraMax = (short)(pup.uitem.DuraMax + 2000);
                                    break;
                                // Modify the A .. B: 8 .. 255
                                case 8:
                                    pup.uitem.DuraMax = (short)(pup.uitem.DuraMax + 4000);
                                    break;
                            }
                            break;
                    }
                    if ((pup.updc == pup.upmc) && (pup.upmc == pup.upsc))
                    {
                        rand = new System.Random(3).Next();
                    }
                    else
                    {
                        rand = -1;
                    }
                    // 瓷仿摹
                    if ((pup.updc >= pup.upmc) && (pup.updc >= pup.upsc) || (rand == 0))
                    {
                        // 颇鲍诀
                        // 公扁狼 青款档 包访 乐澜
                        // 青款
                        per = _MIN(85, 10 + _MIN(11, pup.updc) * 7 + pup.uitem.Desc[3] - pup.uitem.Desc[4] + hum.BodyLuckLevel);
                        if (new System.Random(100).Next() < per)
                        {
                            pup.uitem.Desc[10] = 10;
                            if ((per > 63) && (new System.Random(30).Next() == 0))
                            {
                                pup.uitem.Desc[10] = 11;
                            }
                            if ((per > 79) && (new System.Random(200).Next() == 0))
                            {
                                pup.uitem.Desc[10] = 12;
                            }
                        }
                        else
                        {
                            pup.uitem.Desc[10] = 1;
                        }
                    }
                    if ((pup.upmc >= pup.updc) && (pup.upmc >= pup.upsc) || (rand == 1))
                    {
                        // 付仿诀
                        // 公扁狼 青款档 包访 乐澜
                        per = _MIN(85, 10 + _MIN(11, pup.upmc) * 7 + pup.uitem.Desc[3] - pup.uitem.Desc[4] + hum.BodyLuckLevel);
                        if (new System.Random(100).Next() < per)
                        {
                            pup.uitem.Desc[10] = 20;
                            if ((per > 63) && (new System.Random(30).Next() == 0))
                            {
                                pup.uitem.Desc[10] = 21;
                            }
                            if ((per > 79) && (new System.Random(200).Next() == 0))
                            {
                                pup.uitem.Desc[10] = 22;
                            }
                        }
                        else
                        {
                            pup.uitem.Desc[10] = 1;
                        }
                    }
                    if ((pup.upsc >= pup.upmc) && (pup.upsc >= pup.updc) || (rand == 2))
                    {
                        // 档仿诀
                        // 公扁狼 青款档 包访 乐澜
                        per = _MIN(85, 10 + _MIN(11, pup.upsc) * 7 + pup.uitem.Desc[3] - pup.uitem.Desc[4] + hum.BodyLuckLevel);
                        if (new System.Random(100).Next() < per)
                        {
                            pup.uitem.Desc[10] = 30;
                            if ((per > 63) && (new System.Random(30).Next() == 0))
                            {
                                pup.uitem.Desc[10] = 31;
                            }
                            if ((per > 79) && (new System.Random(200).Next() == 0))
                            {
                                pup.uitem.Desc[10] = 32;
                            }
                        }
                        else
                        {
                            pup.uitem.Desc[10] = 1;
                        }
                    }
                    pu = new TUserItem();
                    pu = pup.uitem;
                    Dispose(pup);
                    // 肺弊巢辫
                    // 诀茫_ +
                    M2Share.AddUserLog("24\09" + hum.MapName + "\09" + hum.CX.ToString() + "\09" + hum.CY.ToString() + "\09" + hum.UserName + "\09" + M2Share.UserEngine.GetStdItemName(pu.Index) + "\09" + pu.MakeIndex.ToString() + "\09" + "1\09" + this.ItemOptionToStr(pu.Desc));
                    hum.AddItem(pu);
                    hum.SendAddItem(pu);
                }
                switch (state)
                {
                    case 2:
                        this.NpcSayTitle(hum, "~@getbackupgnow_ok");
                        break;
                    case 1:
                        // 肯己
                        this.NpcSayTitle(hum, "~@getbackupgnow_ing");
                        break;
                    case 0:
                        // 累诀吝
                        this.NpcSayTitle(hum, "~@getbackupgnow_fail");
                        break;
                }
            }
            else
            {
#if KOREA
                hum.SysMsg("歹 捞惑 甸 荐 绝嚼聪促.", 0);
#else
                hum.SysMsg("You cannot carry any more.", 0);
#endif
                this.NpcSayTitle(hum, "@exit");
            }
        }

        public void SendGoodsEntry(TCreature who, int ltop)
        {
            TClientGoods cg;
            IList<TUserItem> goods;
            TStdItem pstd;
            TUserItem pu;
            string data = "";
            int count = 0;
            for (var i = ltop; i < GoodsList.Count; i++)
            {
                goods = GoodsList[i];
                pu = goods[0];
                pstd = M2Share.UserEngine.GetStdItem(pu.Index);
                if (pstd != null)
                {
                    cg.Name = pstd.Name;
                    cg.Price = GetSellPrice((TUserHuman)who, GetPrice(pu.Index));
                    cg.Stock = goods.Count;
                    if ((pstd.StdMode <= 4) || (pstd.StdMode == 42) || (pstd.StdMode == 31))
                    {
                        cg.SubMenu = 0;
                    }
                    else
                    {
                        cg.SubMenu = 1;
                    }
                    if (pstd.OverlapItem >= 1)
                    {
                        cg.SubMenu = 2;
                    }
                    data = data + cg.Name + "/" + cg.SubMenu.ToString() + "/" + cg.Price.ToString() + "/" + cg.Stock.ToString() + "/";
                    count++;
                }
            }
            who.SendMsg(this, Grobal2.RM_SENDGOODSLIST, 0, this.ActorId, count, 0, data);
        }

        public void SendSellGoods(TCreature who)
        {
            who.SendMsg(this, Grobal2.RM_SENDUSERSELL, 0, this.ActorId, 0, 0, "");
        }

        public void SendRepairGoods(TCreature who)
        {
            who.SendMsg(this, Grobal2.RM_SENDUSERREPAIR, 0, this.ActorId, 0, 0, "");
        }

        public void SendSpecialRepairGoods(TCreature who)
        {
            // 漂荐荐府窍扁 皋春
            string str;
            // if specialrepair > 0 then begin
            str = "你这家伙！你太幸运了...我正好有所需材料可做特殊修补。\\" + "但价格嘛...是通常的三倍。\\ \\ " + " <返回/@main> ";
            str = HUtil32.ReplaceChar(str, '\\', (char)0xa);
            this.NpcSay(who, str);
            who.SendMsg(this, Grobal2.RM_SENDUSERSPECIALREPAIR, 0, this.ActorId, 0, 0, "");
            // end else begin
            // {$IFDEF KOREA}
            // NpcSay (who, '对不起，我们用来做特殊修补的材料已经没了，\ ' +
            // '等候一段时间后，您可以再得到这种材料。\ ' +
            // ' \但是你需要再等一会儿。<返回/@main> ');
            // {$ELSE}
            // NpcSay (who, 'Sorry, but we ran out of material for special repairs\' +
            // 'Sorry but we have no materials for repairs, Please wait for a moment\' +
            // ' \ <back/@main>');
            // {$ENDIF}
            // whocret.LatestNpcCmd := '@repair';
            // end;

        }

        // 漂荐荐府窍扁 皋春
        public void SendStorageItemMenu(TCreature who)
        {
            who.SendMsg(this, Grobal2.RM_SENDUSERSTORAGEITEM, 0, this.ActorId, 0, 0, "");
        }

        public void SendStorageItemList(TCreature who)
        {
            who.SendMsg(this, Grobal2.RM_SENDUSERSTORAGEITEMLIST, 0, this.ActorId, 0, 0, "");
        }

        public void SendMakeDrugItemList(TCreature who)
        {
            const int MAKEPRICE = 100;
            int i;
            int j;
            string data = string.Empty;
            TClientGoods cg;
            TUserItem pu;
            TStdItem pstd;
            ArrayList L;
            string sMakeItemName = string.Empty;
            string sMakePrice;
            data = "";
            for (i = 0; i < GoodsList.Count; i++)
            {
                L = (ArrayList)GoodsList[i];
                pu = (TUserItem)L[0];
                pstd = M2Share.UserEngine.GetStdItem(pu.Index);
                if (pstd != null)
                {
                    cg.Name = pstd.Name;
                    cg.Price = MAKEPRICE;
                    // GetSellPrice (GetPrice (pu.Index));//距父靛绰 厚侩
                    for (j = 0; j < M2Share.MakeItemList.Count; j++)
                    {
                        sMakePrice = HUtil32.GetValidStr3(M2Share.MakeItemList[j], ref sMakeItemName, new string[] { ":" });
                        if (cg.Name == sMakeItemName)
                        {
                            cg.Price = HUtil32.Str_ToInt(sMakePrice, 0);
                            break;
                        }
                    }
                    cg.Stock = 1;
                    // l.Count;
                    cg.SubMenu = 0;
                    // 矫距,澜侥,档备幅...
                    data = data + cg.Name + "/" + cg.SubMenu.ToString() + "/" + cg.Price.ToString() + "/" + cg.Stock.ToString() + "/";
                }
            }
            if (data != "")
            {
                who.SendMsg(this, Grobal2.RM_SENDUSERMAKEDRUGITEMLIST, 0, this.ActorId, 0, 0, data);
            }
        }

        // /////////////////////////////////////////////////////
        public void SendMakeFoodList(TCreature who)
        {
            const int MAKEPRICE = 100;
            int i;
            int j;
            string data = string.Empty;
            TClientGoods cg;
            TUserItem pu;
            TStdItem pstd;
            ArrayList L;
            string sMakeItemName = string.Empty;
            string sMakePrice;
            data = "";
            for (i = 0; i < GoodsList.Count; i++)
            {
                // if i >= 12 then // MAKE FOOD
                // break;
                if (i >= HUtil32.Str_ToInt((string)M2Share.MakeItemIndexList[1], 0) - HUtil32.Str_ToInt((string)M2Share.MakeItemIndexList[0], 0))
                {
                    // MAKE FOOD
                    break;
                }
                L = (ArrayList)GoodsList[i];
                pu = (TUserItem)L[0];
                pstd = M2Share.UserEngine.GetStdItem(pu.Index);
                if (pstd != null)
                {
                    cg.Name = pstd.Name;
                    cg.Price = MAKEPRICE;
                    // GetSellPrice (GetPrice (pu.Index));//距父靛绰 厚侩
                    for (j = 0; j < M2Share.MakeItemList.Count; j++)
                    {
                        sMakePrice = HUtil32.GetValidStr3(M2Share.MakeItemList[j], ref sMakeItemName, new string[] { ":" });
                        if (cg.Name == sMakeItemName)
                        {
                            cg.Price = HUtil32.Str_ToInt(sMakePrice, 0);
                            break;
                        }
                    }
                    cg.Stock = 1;
                    // l.Count;
                    cg.SubMenu = 0;
                    // 矫距,澜侥,档备幅...
                    data = data + cg.Name + "/" + cg.SubMenu.ToString() + "/" + cg.Price.ToString() + "/" + cg.Stock.ToString() + "/";
                }
            }
            if (data != "")
            {
                // 葛靛
                who.SendMsg(this, Grobal2.RM_SENDUSERMAKEITEMLIST, 0, this.ActorId, 1, 0, data);
            }
        }

        // /////////////////////////////////////////////////////
        public void SendMakePotionList(TCreature who)
        {
            const int MAKEPRICE = 100;
            int i;
            int j;
            string data = string.Empty;
            TClientGoods cg;
            TUserItem pu;
            TStdItem pstd;
            ArrayList L;
            string sMakeItemName = string.Empty;
            string sMakePrice;
            data = "";
            for (i = HUtil32.Str_ToInt((string)M2Share.MakeItemIndexList[1], 0) - HUtil32.Str_ToInt((string)M2Share.MakeItemIndexList[0], 0); i < GoodsList.Count; i++)
            {
                // if i >= 16 then // MAKE POTION
                // break;
                if (i >= HUtil32.Str_ToInt((string)M2Share.MakeItemIndexList[2], 0) - HUtil32.Str_ToInt((string)M2Share.MakeItemIndexList[0], 0))
                {
                    // MAKE FOOD
                    break;
                }
                L = (ArrayList)GoodsList[i];
                pu = (TUserItem)L[0];
                pstd = M2Share.UserEngine.GetStdItem(pu.Index);
                if (pstd != null)
                {
                    cg.Name = pstd.Name;
                    cg.Price = MAKEPRICE;
                    // GetSellPrice (GetPrice (pu.Index));//距父靛绰 厚侩
                    for (j = 0; j < M2Share.MakeItemList.Count; j++)
                    {
                        sMakePrice = HUtil32.GetValidStr3(M2Share.MakeItemList[j], ref sMakeItemName, new string[] { ":" });
                        if (cg.Name == sMakeItemName)
                        {
                            cg.Price = HUtil32.Str_ToInt(sMakePrice, 0);
                            break;
                        }
                    }
                    cg.Stock = 1;
                    // l.Count;
                    cg.SubMenu = 0;
                    // 矫距,澜侥,档备幅...
                    data = data + cg.Name + "/" + cg.SubMenu.ToString() + "/" + cg.Price.ToString() + "/" + cg.Stock.ToString() + "/";
                }
            }
            if (data != "")
            {
                // 葛靛
                who.SendMsg(this, Grobal2.RM_SENDUSERMAKEITEMLIST, 0, this.ActorId, 2, 0, data);
            }
        }

        // /////////////////////////////////////////////////////
        public void SendMakeGemList(TCreature who)
        {
            const int MAKEPRICE = 100;
            int i;
            int j;
            string data = string.Empty;
            TClientGoods cg;
            TUserItem pu;
            TStdItem pstd;
            ArrayList L;
            string sMakeItemName = string.Empty;
            string sMakePrice;
            data = "";
            for (i = HUtil32.Str_ToInt((string)M2Share.MakeItemIndexList[2], 0) - HUtil32.Str_ToInt((string)M2Share.MakeItemIndexList[0], 0); i < GoodsList.Count; i++)
            {
                // if i >= 29 then // MAKE GEM
                // break;
                if (i >= HUtil32.Str_ToInt((string)M2Share.MakeItemIndexList[3], 0) - HUtil32.Str_ToInt((string)M2Share.MakeItemIndexList[0], 0))
                {
                    // MAKE FOOD
                    break;
                }
                L = (ArrayList)GoodsList[i];
                pu = (TUserItem)L[0];
                pstd = M2Share.UserEngine.GetStdItem(pu.Index);
                if (pstd != null)
                {
                    cg.Name = pstd.Name;
                    cg.Price = MAKEPRICE;
                    // GetSellPrice (GetPrice (pu.Index));//距父靛绰 厚侩
                    for (j = 0; j < M2Share.MakeItemList.Count; j++)
                    {
                        sMakePrice = HUtil32.GetValidStr3(M2Share.MakeItemList[j], ref sMakeItemName, new string[] { ":" });
                        if (cg.Name == sMakeItemName)
                        {
                            cg.Price = HUtil32.Str_ToInt(sMakePrice, 0);
                            break;
                        }
                    }
                    cg.Stock = 1;
                    // l.Count;
                    cg.SubMenu = 0;
                    // 矫距,澜侥,档备幅...
                    data = data + cg.Name + "/" + cg.SubMenu.ToString() + "/" + cg.Price.ToString() + "/" + cg.Stock.ToString() + "/";
                }
            }
            if (data != "")
            {
                who.SendMsg(this, Grobal2.RM_SENDUSERMAKEITEMLIST, 0, this.ActorId, 3, 0, data);
            }
        }

        // /////////////////////////////////////////////////////
        public void SendMakeItemList(TCreature who)
        {
            const int MAKEPRICE = 100;
            int i;
            int j;
            string data = string.Empty;
            TClientGoods cg;
            TUserItem pu;
            TStdItem pstd;
            ArrayList L;
            string sMakeItemName = string.Empty;
            string sMakePrice;
            data = "";
            for (i = HUtil32.Str_ToInt((string)M2Share.MakeItemIndexList[3], 0) - HUtil32.Str_ToInt((string)M2Share.MakeItemIndexList[0], 0); i < GoodsList.Count; i++)
            {
                if (i >= HUtil32.Str_ToInt((string)M2Share.MakeItemIndexList[4], 0) - HUtil32.Str_ToInt((string)M2Share.MakeItemIndexList[0], 0))
                {
                    break;
                }
                L = (ArrayList)GoodsList[i];
                pu = (TUserItem)L[0];
                pstd = M2Share.UserEngine.GetStdItem(pu.Index);
                if (pstd != null)
                {
                    cg.Name = pstd.Name;
                    cg.Price = MAKEPRICE;
                    for (j = 0; j < M2Share.MakeItemList.Count; j++)
                    {
                        sMakePrice = HUtil32.GetValidStr3(M2Share.MakeItemList[j], ref sMakeItemName, new string[] { ":" });
                        if (cg.Name == sMakeItemName)
                        {
                            cg.Price = HUtil32.Str_ToInt(sMakePrice, 0);
                            break;
                        }
                    }
                    cg.Stock = 1;
                    cg.SubMenu = 0;
                    data = data + cg.Name + "/" + cg.SubMenu.ToString() + "/" + cg.Price.ToString() + "/" + cg.Stock.ToString() + "/";
                }
            }
            if (data != "")
            {
                who.SendMsg(this, Grobal2.RM_SENDUSERMAKEITEMLIST, 0, this.ActorId, 4, 0, data);
            }
        }

        // /////////////////////////////////////////////////////
        public void SendMakeStuffList(TCreature who)
        {
            const int MAKEPRICE = 100;
            int i;
            int j;
            string data = string.Empty;
            TClientGoods cg;
            TUserItem pu;
            TStdItem pstd;
            ArrayList L;
            string sMakeItemName = string.Empty;
            string sMakePrice;
            data = "";
            for (i = HUtil32.Str_ToInt((string)M2Share.MakeItemIndexList[4], 0) - HUtil32.Str_ToInt((string)M2Share.MakeItemIndexList[0], 0); i < GoodsList.Count; i++)
            {
                if (i >= HUtil32.Str_ToInt((string)M2Share.MakeItemIndexList[5], 0) - HUtil32.Str_ToInt((string)M2Share.MakeItemIndexList[0], 0))
                {
                    // MAKE STUFF
                    break;
                }
                L = (ArrayList)GoodsList[i];
                pu = (TUserItem)L[0];
                pstd = M2Share.UserEngine.GetStdItem(pu.Index);
                if (pstd != null)
                {
                    cg.Name = pstd.Name;
                    cg.Price = MAKEPRICE;
                    // GetSellPrice (GetPrice (pu.Index));//距父靛绰 厚侩
                    for (j = 0; j < M2Share.MakeItemList.Count; j++)
                    {
                        sMakePrice = HUtil32.GetValidStr3(M2Share.MakeItemList[j], ref sMakeItemName, new string[] { ":" });
                        if (cg.Name == sMakeItemName)
                        {
                            cg.Price = HUtil32.Str_ToInt(sMakePrice, 0);
                            break;
                        }
                    }
                    cg.Stock = 1;
                    // l.Count;
                    cg.SubMenu = 0;
                    // 矫距,澜侥,档备幅...
                    data = data + cg.Name + "/" + cg.SubMenu.ToString() + "/" + cg.Price.ToString() + "/" + cg.Stock.ToString() + "/";
                }
            }
            if (data != "")
            {
                // 葛靛
                who.SendMsg(this, Grobal2.RM_SENDUSERMAKEITEMLIST, 0, this.ActorId, 5, 0, data);
            }
        }

        // /////////////////////////////////////////////////////
        public void SendMakeEtcList(TCreature who)
        {
            const int MAKEPRICE = 100;
            int i;
            int j;
            string data = string.Empty;
            TClientGoods cg;
            TUserItem pu;
            TStdItem pstd;
            ArrayList L;
            string sMakeItemName = string.Empty;
            string sMakePrice;
            data = "";
            for (i = HUtil32.Str_ToInt((string)M2Share.MakeItemIndexList[5], 0) - HUtil32.Str_ToInt((string)M2Share.MakeItemIndexList[0], 0); i < GoodsList.Count; i++)
            {
                L = (ArrayList)GoodsList[i];
                pu = (TUserItem)L[0];
                pstd = M2Share.UserEngine.GetStdItem(pu.Index);
                if (pstd != null)
                {
                    cg.Name = pstd.Name;
                    cg.Price = MAKEPRICE;
                    // GetSellPrice (GetPrice (pu.Index));//距父靛绰 厚侩
                    for (j = 0; j < M2Share.MakeItemList.Count; j++)
                    {
                        sMakePrice = HUtil32.GetValidStr3(M2Share.MakeItemList[j], ref sMakeItemName, new string[] { ":" });
                        if (cg.Name == sMakeItemName)
                        {
                            cg.Price = HUtil32.Str_ToInt(sMakePrice, 0);
                            break;
                        }
                    }
                    cg.Stock = 1;
                    // l.Count;
                    cg.SubMenu = 0;
                    // 矫距,澜侥,档备幅...
                    data = data + cg.Name + "/" + cg.SubMenu.ToString() + "/" + cg.Price.ToString() + "/" + cg.Stock.ToString() + "/";
                }
            }
            if (data != "")
            {
                // 葛靛
                who.SendMsg(this, Grobal2.RM_SENDUSERMAKEITEMLIST, 0, this.ActorId, 6, 0, data);
            }
        }

        // ------------------------------------------------------------------------
        // /////////////////////////////////////////////////////////////
        public override void UserSelect(TCreature whocret, string selstr)
        {
            string sel = string.Empty;
            string body = string.Empty;
            try
            {
                // 荤合己救俊 乐绰 惑痢篮 傍己傈 吝俊绰 拱扒阑 迫瘤 臼绰促.
                if ((BoCastleManage && M2Share.UserCastle.BoCastleUnderAttack) || whocret.Death)
                {
                }
                else
                {
                    body = HUtil32.GetValidStr3(selstr, ref sel, new char[] { '\r' });
                    if (sel != "")
                    {
                        if (sel[0] == '@')
                        {
                            while (true)
                            {
                                whocret.LatestNpcCmd = sel;
                                if (this.CanSpecialRepair)
                                {
                                    if (sel.ToLower().CompareTo("@s_repair".ToLower()) == 0)
                                    {
                                        SendSpecialRepairGoods(whocret);
                                        break;
                                    }
                                }
                                if (this.CanTotalRepair)
                                {
                                    if (sel.ToLower().CompareTo("@t_repair".ToLower()) == 0)
                                    {
                                        SendSpecialRepairGoods(whocret);
                                        break;
                                    }
                                }
                                this.NpcSayTitle(whocret, sel);
                                if (this.CanBuy)
                                {
                                    if (sel.ToLower().CompareTo("@buy".ToLower()) == 0)
                                    {
                                        SendGoodsEntry(whocret, 0);
                                        break;
                                    }
                                }
                                if (this.CanSell)
                                {
                                    if (sel.ToLower().CompareTo("@sell".ToLower()) == 0)
                                    {
                                        SendSellGoods(whocret);
                                        break;
                                    }
                                }
                                if (this.CanRepair)
                                {
                                    if (sel.ToLower().CompareTo("@repair".ToLower()) == 0)
                                    {
                                        SendRepairGoods(whocret);
                                        break;
                                    }
                                }
                                if (this.CanMakeDrug)
                                {
                                    if (sel.ToLower().CompareTo("@makedrug".ToLower()) == 0)
                                    {
                                        SendMakeDrugItemList(whocret);
                                        break;
                                    }
                                }
                                if (sel.ToLower().CompareTo("@prices".ToLower()) == 0)
                                {
                                    break;
                                }
                                if (this.CanStorage)
                                {
                                    if (sel.ToLower().CompareTo("@storage".ToLower()) == 0)
                                    {
                                        SendStorageItemMenu(whocret);
                                        break;
                                    }
                                }
                                if (this.CanGetBack)
                                {
                                    if (sel.ToLower().CompareTo("@getback".ToLower()) == 0)
                                    {
                                        SendStorageItemList(whocret);
                                        break;
                                    }
                                }
                                if (this.CanUpgrade)
                                {
                                    if (sel.ToLower().CompareTo("@upgradenow".ToLower()) == 0)
                                    {
                                        UserSelectUpgradeWeapon((TUserHuman)whocret);
                                        break;
                                    }
                                    if (sel.ToLower().CompareTo("@getbackupgnow".ToLower()) == 0)
                                    {
                                        UserSelectGetBackUpgrade((TUserHuman)whocret);
                                        break;
                                    }
                                }
                                if (this.CanMakeItem)
                                {
                                    if (sel.ToLower().CompareTo("@makefood".ToLower()) == 0)
                                    {
                                        SendMakeFoodList(whocret);
                                        break;
                                    }
                                    if (sel.ToLower().CompareTo("@makepotion".ToLower()) == 0)
                                    {
                                        SendMakePotionList(whocret);
                                        break;
                                    }
                                    if (sel.ToLower().CompareTo("@makegem".ToLower()) == 0)
                                    {
                                        SendMakeGemList(whocret);
                                        break;
                                    }
                                    if (sel.ToLower().CompareTo("@makeitem".ToLower()) == 0)
                                    {
                                        SendMakeItemList(whocret);
                                        break;
                                    }
                                    if (sel.ToLower().CompareTo("@makestuff".ToLower()) == 0)
                                    {
                                        SendMakeStuffList(whocret);
                                        break;
                                    }
                                    if (sel.ToLower().CompareTo("@makeetc".ToLower()) == 0)
                                    {
                                        SendMakeEtcList(whocret);
                                        break;
                                    }
                                }
                                if (this.CanItemMarket && (whocret != null) && (whocret.RaceServer == Grobal2.RC_USERHUMAN))
                                {
                                    if (sel.ToLower().CompareTo("@market_0".ToLower()) == 0)
                                    {
                                        SendUserMarket((TUserHuman)whocret, Grobal2.USERMARKET_TYPE_ALL, Grobal2.USERMARKET_MODE_BUY);
                                        break;
                                    }
                                    if (sel.ToLower().CompareTo("@market_1".ToLower()) == 0)
                                    {
                                        SendUserMarket((TUserHuman)whocret, Grobal2.USERMARKET_TYPE_WEAPON, Grobal2.USERMARKET_MODE_BUY);
                                        break;
                                    }
                                    if (sel.ToLower().CompareTo("@market_2".ToLower()) == 0)
                                    {
                                        SendUserMarket((TUserHuman)whocret, Grobal2.USERMARKET_TYPE_NECKLACE, Grobal2.USERMARKET_MODE_BUY);
                                        break;
                                    }
                                    if (sel.ToLower().CompareTo("@market_3".ToLower()) == 0)
                                    {
                                        SendUserMarket((TUserHuman)whocret, Grobal2.USERMARKET_TYPE_RING, Grobal2.USERMARKET_MODE_BUY);
                                        break;
                                    }
                                    if (sel.ToLower().CompareTo("@market_4".ToLower()) == 0)
                                    {
                                        SendUserMarket((TUserHuman)whocret, Grobal2.USERMARKET_TYPE_BRACELET, Grobal2.USERMARKET_MODE_BUY);
                                        break;
                                    }
                                    if (sel.ToLower().CompareTo("@market_5".ToLower()) == 0)
                                    {
                                        SendUserMarket((TUserHuman)whocret, Grobal2.USERMARKET_TYPE_CHARM, Grobal2.USERMARKET_MODE_BUY);
                                        break;
                                    }
                                    if (sel.ToLower().CompareTo("@market_6".ToLower()) == 0)
                                    {
                                        SendUserMarket((TUserHuman)whocret, Grobal2.USERMARKET_TYPE_HELMET, Grobal2.USERMARKET_MODE_BUY);
                                        break;
                                    }
                                    if (sel.ToLower().CompareTo("@market_7".ToLower()) == 0)
                                    {
                                        SendUserMarket((TUserHuman)whocret, Grobal2.USERMARKET_TYPE_BELT, Grobal2.USERMARKET_MODE_BUY);
                                        break;
                                    }
                                    if (sel.ToLower().CompareTo("@market_8".ToLower()) == 0)
                                    {
                                        SendUserMarket((TUserHuman)whocret, Grobal2.USERMARKET_TYPE_SHOES, Grobal2.USERMARKET_MODE_BUY);
                                        break;
                                    }
                                    if (sel.ToLower().CompareTo("@market_9".ToLower()) == 0)
                                    {
                                        SendUserMarket((TUserHuman)whocret, Grobal2.USERMARKET_TYPE_ARMOR, Grobal2.USERMARKET_MODE_BUY);
                                        break;
                                    }
                                    if (sel.ToLower().CompareTo("@market_10".ToLower()) == 0)
                                    {
                                        SendUserMarket((TUserHuman)whocret, Grobal2.USERMARKET_TYPE_DRINK, Grobal2.USERMARKET_MODE_BUY);
                                        break;
                                    }
                                    if (sel.ToLower().CompareTo("@market_11".ToLower()) == 0)
                                    {
                                        SendUserMarket((TUserHuman)whocret, Grobal2.USERMARKET_TYPE_JEWEL, Grobal2.USERMARKET_MODE_BUY);
                                        break;
                                    }
                                    if (sel.ToLower().CompareTo("@market_12".ToLower()) == 0)
                                    {
                                        SendUserMarket((TUserHuman)whocret, Grobal2.USERMARKET_TYPE_BOOK, Grobal2.USERMARKET_MODE_BUY);
                                        break;
                                    }
                                    if (sel.ToLower().CompareTo("@market_13".ToLower()) == 0)
                                    {
                                        SendUserMarket((TUserHuman)whocret, Grobal2.USERMARKET_TYPE_MINERAL, Grobal2.USERMARKET_MODE_BUY);
                                        break;
                                    }
                                    if (sel.ToLower().CompareTo("@market_14".ToLower()) == 0)
                                    {
                                        SendUserMarket((TUserHuman)whocret, Grobal2.USERMARKET_TYPE_QUEST, Grobal2.USERMARKET_MODE_BUY);
                                        break;
                                    }
                                    if (sel.ToLower().CompareTo("@market_15".ToLower()) == 0)
                                    {
                                        SendUserMarket((TUserHuman)whocret, Grobal2.USERMARKET_TYPE_ETC, Grobal2.USERMARKET_MODE_BUY);
                                        break;
                                    }
                                    if (sel.ToLower().CompareTo("@market_100".ToLower()) == 0)
                                    {
                                        SendUserMarket((TUserHuman)whocret, Grobal2.USERMARKET_TYPE_SET, Grobal2.USERMARKET_MODE_BUY);
                                        break;
                                    }
                                    if (sel.ToLower().CompareTo("@market_200".ToLower()) == 0)
                                    {
                                        SendUserMarket((TUserHuman)whocret, Grobal2.USERMARKET_TYPE_MINE, Grobal2.USERMARKET_MODE_INQUIRY);
                                        break;
                                    }
                                    if (sel.ToLower().CompareTo("@market_sell".ToLower()) == 0)
                                    {
                                        SendUserMarket((TUserHuman)whocret, Grobal2.USERMARKET_TYPE_ALL, Grobal2.USERMARKET_MODE_SELL);
                                        break;
                                    }
                                }
                                // 巩颇 厘盔
                                // (ServerIndex = 0) and
                                if (this.CanAgitUsage && (whocret != null))
                                {
                                    if (sel.ToLower().CompareTo("@agitreg".ToLower()) == 0)
                                    {
                                        ((TUserHuman)whocret).CmdGuildAgitRegistration();
                                    }
                                    if (sel.ToLower().CompareTo("@agitmove".ToLower()) == 0)
                                    {
                                        ((TUserHuman)whocret).CmdGuildAgitAutoMove();
                                    }
                                    if (sel.ToLower().CompareTo("@agitbuy".ToLower()) == 0)
                                    {
                                        ((TUserHuman)whocret).CmdGuildAgitBuy(1);
                                    }
                                    if (sel.ToLower().CompareTo("@agittrade".ToLower()) == 0)
                                    {
                                        ((TUserHuman)whocret).BoGuildAgitDealTry = true;
                                        ((TUserHuman)whocret).CmdTryGuildAgitTrade();
                                    }
                                }
                                if (this.CanAgitManage && (whocret != null))
                                {
                                    if (sel.ToLower().CompareTo("@agitextend".ToLower()) == 0)
                                    {
                                        ((TUserHuman)whocret).CmdGuildAgitExtendTime(1);
                                    }
                                    if (sel.ToLower().CompareTo("@agitremain".ToLower()) == 0)
                                    {
                                        ((TUserHuman)whocret).CmdGuildAgitRemainTime();
                                    }
                                    if (sel.ToLower().CompareTo("@@agitonerecall".ToLower()) == 0)
                                    {
                                        ((TUserHuman)whocret).CmdGuildAgitRecall(body, false);
                                    }
                                    if (sel.ToLower().CompareTo("@agitrecall".ToLower()) == 0)
                                    {
                                        ((TUserHuman)whocret).CmdGuildAgitRecall("", true);
                                    }
                                    if (sel.ToLower().CompareTo("@@agitforsale".ToLower()) == 0)
                                    {
                                        ((TUserHuman)whocret).CmdGuildAgitSale(body);
                                    }
                                    if (sel.ToLower().CompareTo("@agitforsalecancel".ToLower()) == 0)
                                    {
                                        ((TUserHuman)whocret).CmdGuildAgitSaleCancel();
                                    }
                                    if (sel.ToLower().CompareTo("@gaboardlist".ToLower()) == 0)
                                    {
                                        ((TUserHuman)whocret).CmdGaBoardList(1);
                                    }
                                    if (sel.ToLower().CompareTo("@@guildagitdonate".ToLower()) == 0)
                                    {
                                        ((TUserHuman)whocret).CmdGuildAgitDonate(body);
                                    }
                                    if (sel.ToLower().CompareTo("@viewdonation".ToLower()) == 0)
                                    {
                                        ((TUserHuman)whocret).CmdGuildAgitViewDonation();
                                    }
                                }
                                if (this.CanBuyDecoItem && (whocret != null))
                                {
                                    if (sel.ToLower().CompareTo("@ga_decoitem_buy".ToLower()) == 0)
                                    {
                                        SendDecoItemListShow(whocret);
                                    }
                                    if (sel.ToLower().CompareTo("@ga_decomon_count".ToLower()) == 0)
                                    {
                                        ((TUserHuman)whocret).CmdAgitDecoMonCountHere();
                                    }
                                }
                                if (sel.ToLower().CompareTo("@exit".ToLower()) == 0)
                                {
                                    whocret.SendMsg(this, Grobal2.RM_MERCHANTDLGCLOSE, 0, this.ActorId, 0, 0, "");
                                    break;
                                }
                                break;
                            }
                        }
                    }
                }
            }
            catch
            {
                M2Share.MainOutMessage("[Exception] TMerchant.UserSelect... ");
            }
        }

        public void SayMakeItemMaterials(TCreature whocret, string selstr)
        {
            string rmsg = "@";
            rmsg = rmsg + selstr;
            this.NpcSayTitle(whocret, rmsg);
        }

        public void QueryPrice(TCreature whocret, TUserItem uitem)
        {
            int buyprice;
            buyprice = (int)GetBuyPrice(GetGoodsPrice(uitem));
            // 备涝 啊拜阑 舅妨淋
            if (buyprice >= 0)
            {
                whocret.SendMsg(this, Grobal2.RM_SENDBUYPRICE, 0, buyprice, 0, 0, "");
            }
            else
            {
                whocret.SendMsg(this, Grobal2.RM_SENDBUYPRICE, 0, 0, 0, 0, "");
            }
            // 绝澜..

        }

        public bool AddGoods(TUserItem uitem)
        {
            bool result;
            TUserItem pu;
            IList<TUserItem> list;
            TStdItem pstd;
            if (uitem.DuraMax > 0)
            {
                // 郴备己捞 0牢巴篮 颊角 贸府茄促. (静贰扁 规瘤)
                list = GetGoodsList(uitem.Index);
                if (list == null)
                {
                    list = new List<TUserItem>();
                    GoodsList.Add(list);
                }
                pu = new TUserItem();
                // 2003/06/12 荤侩磊啊 迫篮 拱扒狼 郴备己篮 弥措郴备肺 荐沥窍咯
                // 窖 啊拜俊 登混荐 绝档废 荐沥
                pstd = M2Share.UserEngine.GetStdItem(uitem.Index);
                if (pstd != null)
                {
                    // 棱惑牢狼榷阂,刀啊风狼 郴备甫 弥措肺 荐沥窍瘤 臼绰促(sonmg 2004/07/16)
                    // or (pstd.StdMode = 25)
                    if ((pstd.StdMode == 0) || (pstd.StdMode == 31) || ((pstd.StdMode == 3) && ((pstd.Shape == 1) || (pstd.Shape == 2) || (pstd.Shape == 3) || (pstd.Shape == 5) || (pstd.Shape == 9))) || ((pstd.StdMode == 30) && (pstd.Shape == 0)))
                    {
                        uitem.Dura = uitem.DuraMax;
                    }
                }
                pu = uitem;
                list.Insert(0, pu);
            }
            result = true;
            return result;
        }

        public bool UserSellItem_CanSell(TUserItem pu)
        {
            bool result;
            TStdItem pstd;
            result = true;
            pstd = M2Share.UserEngine.GetStdItem(pu.Index);
            if (pstd != null)
            {
                if ((pstd.StdMode == 25) || (pstd.StdMode == 30))
                {
                    if (pu.Dura < 4000)
                    {
                        result = false;
                    }
                }
                else if (pstd.StdMode == 8)
                {
                    // 檬措厘篮 迫 荐 绝促.
                    result = false;
                }
            }
            return result;
        }

        public bool UserSellItem(TCreature whocret, TUserItem uitem)
        {
            bool result;
            int buyprice;
            TStdItem pstd;
            result = false;
            buyprice = (int)GetBuyPrice(GetGoodsPrice(uitem));
            // 拱扒 备涝 啊拜
            if ((buyprice >= 0) && !NoSeal && UserSellItem_CanSell(uitem))
            {
                // 荤侩磊啊 拱扒阑 迫澜. 惑前 备涝档 救窃
                if (whocret.IncGold(buyprice))
                {
                    // 荤合己救狼 惑痢牢 版快
                    if (BoCastleManage)
                    {
                        // 5%狼 技陛捞 叭腮促.
                        M2Share.UserCastle.PayTax(buyprice);
                    }
                    whocret.SendMsg(this, Grobal2.RM_USERSELLITEM_OK, 0, whocret.Gold, 0, 0, "");
                    // 惑前俊 眠啊
                    AddGoods(uitem);
                    // 肺弊巢辫
                    pstd = M2Share.UserEngine.GetStdItem(uitem.Index);
                    if ((pstd != null) && (!M2Share.IsCheapStuff(pstd.StdMode)))
                    {
                        // 魄概_ +
                        M2Share.AddUserLog("10\09" + whocret.MapName + "\09" + whocret.CX.ToString() + "\09" + whocret.CY.ToString() + "\09" + whocret.UserName + "\09" + M2Share.UserEngine.GetStdItemName(uitem.Index) + "\09" + uitem.MakeIndex.ToString() + "\09" + "1\09" + this.UserName);
                    }
                    result = true;
                }
                else
                {
                    // 捣捞 呈公 腹澜.
                    whocret.SendMsg(this, Grobal2.RM_USERSELLITEM_FAIL, 0, 0, 1, 0, "");
                }
            }
            else
            {
                // 秒鞭 救窃
                whocret.SendMsg(this, Grobal2.RM_USERSELLITEM_FAIL, 0, 0, 0, 0, "");
            }
            return result;
        }

        // 墨款飘 酒捞袍
        public bool UserCountSellItem(TCreature whocret, TUserItem uitem, int sellcnt)
        {
            bool result;
            int remain;
            int buyprice;
            TStdItem pstd;
            result = false;
            buyprice = -1;
            pstd = M2Share.UserEngine.GetStdItem(uitem.Index);
            if (pstd != null)
            {
                if (IsDealingItem(pstd.StdMode, pstd.Shape))
                {
                    buyprice = (int)(GetBuyPrice(GetGoodsPrice(uitem)) * sellcnt);
                }
                // 拱扒 备涝 啊拜
            }
            remain = uitem.Dura - sellcnt;
            if ((buyprice >= 0) && !NoSeal && (remain >= 0))
            {
                // 荤侩磊啊 拱扒阑 迫澜. 惑前 备涝档 救窃
                if (whocret.IncGold(buyprice))
                {
                    // 荤合己救狼 惑痢牢 版快
                    if (BoCastleManage)
                    {
                        // 5%狼 技陛捞 叭腮促.
                        M2Share.UserCastle.PayTax(buyprice);
                    }
                    whocret.SendMsg(this, Grobal2.RM_USERSELLCOUNTITEM_OK, 0, whocret.Gold, remain, sellcnt, "");
                    // 惑前俊 眠啊
                    // AddGoods (uitem);
                    // 肺弊巢辫
                    // 魄概_ +
                    M2Share.AddUserLog("10\09" + whocret.MapName + "\09" + whocret.CX.ToString() + "\09" + whocret.CY.ToString() + "\09" + whocret.UserName + "\09" + M2Share.UserEngine.GetStdItemName(uitem.Index) + "\09" + uitem.MakeIndex.ToString() + "\09" + "1\09" + this.UserName);
                    result = true;
                }
                else
                {
                    // 捣捞 呈公 腹澜.
                    whocret.SendMsg(this, Grobal2.RM_USERSELLCOUNTITEM_FAIL, 0, 0, 0, 0, "");
                }
            }
            else
            {
                // 秒鞭 救窃
                whocret.SendMsg(this, Grobal2.RM_USERSELLCOUNTITEM_FAIL, 0, 0, 0, 0, "");
            }
            return result;
        }

        public void QueryRepairCost(TCreature whocret, TUserItem uitem)
        {
            int price;
            int cost;
            price = GetSellPrice((TUserHuman)whocret, GetGoodsPrice(uitem));
            // 魄概啊拜栏肺 券魂窃.
            if (price > 0)
            {
                if ((whocret.LatestNpcCmd == "@s_repair") || (whocret.LatestNpcCmd == "@t_repair"))
                {
                    // 漂荐荐府
                    price = price * 3;
                    // if specialrepair > 0 then
                    // else whocret.LatestNpcCmd := '@fail_s_repair';     //漂荐荐府 犁丰 何练..
                }
                if (uitem.DuraMax > 0)
                {
                    // DURAMAX荐沥
                    cost = HUtil32.MathRound(price / 3 / uitem.DuraMax * _MAX(0, uitem.DuraMax - uitem.Dura));
                }
                else
                {
                    cost = 0;
                }
                // price;
                whocret.SendMsg(this, Grobal2.RM_SENDREPAIRCOST, 0, cost, 0, 0, "");
            }
            else
            {
                whocret.SendMsg(this, Grobal2.RM_SENDREPAIRCOST, 0, -1, 0, 0, "");
            }
            // 绝澜..

        }

        public bool UserRepairItem(TCreature whocret, TUserItem puitem)
        {
            bool result;
            int price;
            int cost;
            TStdItem pstd;
            int repair_type;
            string str;
            result = false;
            repair_type = 0;
            if (whocret.LatestNpcCmd == "@fail_s_repair")
            {
                // 漂荐荐府 给窃.
                str = "对不起，我们刚用完了特殊修补的材料...\\ " + " \\ \\<返回/@main> ";
                str = HUtil32.ReplaceChar(str, '\\', (char)0xa);
                this.NpcSay(whocret, str);
                whocret.SendMsg(this, Grobal2.RM_USERREPAIRITEM_FAIL, 0, 0, 0, 0, "");
                return result;
            }
            pstd = M2Share.UserEngine.GetStdItem(puitem.Index);
            if (pstd == null)
            {
                return result;
            }
            price = GetSellPrice((TUserHuman)whocret, GetGoodsPrice(puitem));
            if (this.CanSpecialRepair && (whocret.LatestNpcCmd == "@s_repair"))
            {
                // 漂荐荐府
                price = price * 3;
                if ((pstd.StdMode != 5) && (pstd.StdMode != 6))
                {
                    M2Share.MainOutMessage("Special Repair(X): " + whocret.UserName + " - " + pstd.Name);
                    whocret.SendMsg(this, Grobal2.RM_USERREPAIRITEM_FAIL, 0, 0, 0, 0, "");
                    return result;
                    // gadget:公扁啊 酒聪搁 漂荐荐府 绝澜.
                }
                else
                {
                    M2Share.MainOutMessage("Repair: " + whocret.UserName + "(" + whocret.MapName + ":" + whocret.CX.ToString() + "," + whocret.CY.ToString() + ")" + " - " + pstd.Name);
                }
            }
            if (this.CanTotalRepair && (whocret.LatestNpcCmd == "@t_repair"))
            {
                // 例措荐府
                price = price * 3;
                switch (pstd.StdMode)
                {
                    case 5:
                    case 6:
                    case 10:
                    case 11:
                    case 15:
                    case 19:
                    case 20:
                    case 21:
                    case 22:
                    case 23:
                    case 24:
                    case 26:
                    case 52:
                    case 54:
                        // 例措荐府 捞亥飘 2003-06-26
                        M2Share.MainOutMessage("Perfect Repair: " + whocret.UserName + "(" + whocret.MapName + ":" + whocret.CX.ToString() + "," + whocret.CY.ToString() + ")" + " - " + pstd.Name);
                        break;
                    default:
                        M2Share.MainOutMessage("Perfect Repair(X): " + whocret.UserName + " - " + pstd.Name);
                        whocret.SendMsg(this, Grobal2.RM_USERREPAIRITEM_FAIL, 0, 0, 0, 0, "");
                        return result;
                        break;
                        // pds:捞亥飘 例措荐府
                }
            }
            // 蜡聪农酒捞袍 鞘靛啊 3捞搁 荐府阂啊.
            // or -> and (sonmg's bug 2003/12/03)
            if ((price > 0) && (pstd.StdMode != 43) && (pstd.UniqueItem != 3))
            {
                // 秒鞭窍瘤 臼绰 巴篮 荐府 救凳
                if (puitem.DuraMax > 0)
                {
                    // DURAMAX荐沥
                    cost = HUtil32.MathRound(price / 3 / puitem.DuraMax * _MAX(0, puitem.DuraMax - puitem.Dura));
                }
                else
                {
                    cost = 0;
                }
                // price;
                if ((cost > 0) && whocret.DecGold(cost))
                {
                    // 荤合己救狼 惑痢牢 版快
                    if (BoCastleManage)
                    {
                        // 5%狼 技陛捞 叭腮促.
                        M2Share.UserCastle.PayTax(cost);
                    }
                    if ((this.CanSpecialRepair && (whocret.LatestNpcCmd == "@s_repair")) || (this.CanTotalRepair && (whocret.LatestNpcCmd == "@t_repair")))
                    {
                        // 漂荐荐府
                        // Dec (specialrepair);
                        // 漂荐荐府绰 郴备啊 距秦瘤瘤 臼澜
                        // puitem.DuraMax := puitem.DuraMax - _MAX(0, puitem.DuraMax-puitem.Dura) div 100;  //DURAMAX荐沥
                        puitem.Dura = puitem.DuraMax;
                        whocret.SendMsg(this, Grobal2.RM_USERREPAIRITEM_OK, 0, whocret.Gold, puitem.Dura, puitem.DuraMax, "");
                        str = "肯寒窍霸 荐府登菌匙...\\请好好使用它。\\ \\<返回/@main> ";
                        str = HUtil32.ReplaceChar(str, '\\', (char)0xa);
                        this.NpcSay(whocret, str);
                        repair_type = 2;
                    }
                    else
                    {
                        // 老馆 荐府, 郴备己捞 腹捞 距秦咙
                        puitem.DuraMax = (short)(puitem.DuraMax - _MAX(0, puitem.DuraMax - puitem.Dura) / 30);
                        // DURAMAX荐沥
                        puitem.Dura = puitem.DuraMax;
                        whocret.SendMsg(this, Grobal2.RM_USERREPAIRITEM_OK, 0, whocret.Gold, puitem.Dura, puitem.DuraMax, "");
                        this.NpcSayTitle(whocret, "~@repair");
                        repair_type = 1;
                    }
                    result = true;
                    // 荐府 肺弊 巢辫
                    // 荐府_ +
                    M2Share.AddUserLog("36\09" + whocret.MapName + "\09" + cost.ToString() + "\09" + whocret.Gold.ToString() + "\09" + whocret.UserName + "\09" + puitem.DuraMax.ToString() + "\09" + puitem.MakeIndex.ToString() + "\09" + repair_type.ToString() + "\09" + "0");
                }
                else
                {
                    // 捣捞 绝澜
                    whocret.SendMsg(this, Grobal2.RM_USERREPAIRITEM_FAIL, 0, 0, 0, 0, "");
                }
            }
            else
            {
                whocret.SendMsg(this, Grobal2.RM_USERREPAIRITEM_FAIL, 0, 0, 0, 0, "");
            }
            return result;
        }

        public void UserBuyItem(TUserHuman whocret, string itmname, int serverindex, int BuyCount)
        {
            int i;
            int k;
            int sellprice;
            int rcode;
            ArrayList list;
            TStdItem pstd;
            TUserItem pu;
            bool done;
            int CheckWeight;
            bool InviteResult;
            done = false;
            InviteResult = true;
            rcode = 1;
            // 惑前捞 促 迫啡嚼聪促.
            for (i = 0; i < GoodsList.Count; i++)
            {
                if (done)
                {
                    break;
                }
                if (NoSeal)
                {
                    break;
                }
                // 拱扒阑 救颇绰 啊霸
                list = (ArrayList)GoodsList[i];
                pu = (TUserItem)list[0];
                pstd = M2Share.UserEngine.GetStdItem(pu.Index);
                if (pstd != null)
                {
                    // 墨款飘酒捞袍
                    if (pstd.OverlapItem == 1)
                    {
                        CheckWeight = pstd.Weight + pstd.Weight * (BuyCount / 10);
                    }
                    else if (pstd.OverlapItem >= 2)
                    {
                        CheckWeight = pstd.Weight * BuyCount;
                    }
                    else
                    {
                        CheckWeight = pstd.Weight;
                    }
                    if (whocret.IsAddWeightAvailable(CheckWeight))
                    {
                        if (pstd.Name == itmname)
                        {
                            for (k = 0; k < list.Count; k++)
                            {
                                pu = (TUserItem)list[k];
                                if ((pstd.StdMode <= 4) || (pstd.StdMode == 42) || (pstd.StdMode == 31) || (pu.MakeIndex == serverindex) || (pstd.OverlapItem >= 1))
                                {
                                    sellprice = GetSellPrice(whocret, GetGoodsPrice(pu)) * BuyCount;
                                    if ((whocret.Gold >= sellprice) && (sellprice > 0))
                                    {
                                        if (pstd.OverlapItem >= 1)
                                        {
                                            pu.Dura = (short)_MIN(1000, BuyCount);
                                        }
                                        if (pstd.OverlapItem >= 1)
                                        {
                                            if (whocret.UserCounterItemAdd(pstd.StdMode, pstd.Looks, BuyCount, pstd.Name, false))
                                            {
                                                whocret.DecGold(sellprice);
                                                list.RemoveAt(k);
                                                if (list.Count == 0)
                                                {
                                                    list.Free();
                                                    GoodsList.RemoveAt(i);
                                                }
                                                whocret.WeightChanged();
                                                rcode = 0;
                                                done = true;
                                                break;
                                            }
                                        }
                                        InviteResult = true;
                                        if ((pstd.StdMode == 8) && (pstd.Shape == ObjBase.SHAPE_OF_INVITATION))
                                        {
                                            InviteResult = whocret.GuildAgitInvitationItemSet(pu);
                                            if (!InviteResult)
                                            {
                                                whocret.SysMsg("在你的门派庄园你可以得到一个邀请函。", 0);
                                            }
                                        }
                                        if (InviteResult)
                                        {
                                            if (whocret.AddItem(pu))
                                            {
                                                whocret.DecGold(sellprice);
                                                if (BoCastleManage)
                                                {
                                                    M2Share.UserCastle.PayTax(sellprice);
                                                }
                                                whocret.SendAddItem(pu);
                                                M2Share.AddUserLog("9\09" + whocret.MapName + "\09" + whocret.CX.ToString() + "\09" + whocret.CY.ToString() + "\09" + whocret.UserName + "\09" + M2Share.UserEngine.GetStdItemName(pu.Index) + "\09" + pu.MakeIndex.ToString() + "\09" + "1\09" + this.UserName);
                                                list.RemoveAt(k);
                                                if (list.Count == 0)
                                                {
                                                    list.Free();
                                                    GoodsList.RemoveAt(i);
                                                }
                                                rcode = 0;
                                            }
                                            else
                                            {
                                                rcode = 2;
                                            }
                                        }
                                        else
                                        {
                                            return;
                                        }
                                    }
                                    else
                                    {
                                        rcode = 3;
                                    }
                                    done = true;
                                    break;
                                }
                            }
                        }
                    }
                    else
                    {
                        rcode = 2;
                    }
                }
            }
            if (rcode == 0)
            {
                whocret.SendMsg(this, Grobal2.RM_BUYITEM_SUCCESS, 0, whocret.Gold, serverindex, 0, "");
            }
            else
            {
                whocret.SendMsg(this, Grobal2.RM_BUYITEM_FAIL, 0, rcode, 0, 0, "");
            }
        }

        public void UserWantDetailItems(TCreature whocret, string itmname, int menuindex)
        {
            int i;
            int k;
            int count;
            string data = string.Empty;
            ArrayList list;
            TStdItem pstd;
            TStdItem std;
            TUserItem pu;
            TClientItem citem = null;
            count = 0;
            for (i = 0; i < GoodsList.Count; i++)
            {
                list = (ArrayList)GoodsList[i];
                pu = (TUserItem)list[0];
                pstd = M2Share.UserEngine.GetStdItem(pu.Index);
                if (pstd != null)
                {
                    if (pstd.Name == itmname)
                    {
                        if (menuindex > list.Count - 1)
                        {
                            menuindex = _MAX(0, list.Count - 10);
                        }
                        for (k = menuindex; k < list.Count; k++)
                        {
                            pu = (TUserItem)list[k];
                            // citem.S := pstd^;
                            std = pstd;
                            M2Share.ItemMan.GetUpgradeStdItem(pu, ref std);
                            //Move(std, citem.S, sizeof(TStdItem));
                            //FillChar(citem.Desc, sizeof(citem.Desc), '\0');
                            citem.Dura = pu.Dura;
                            citem.DuraMax = (short)GetSellPrice((TUserHuman)whocret, GetGoodsPrice(pu));
                            citem.MakeIndex = pu.MakeIndex;
                            //FillChar(citem.Desc, sizeof(citem.Desc), '\0');
                            //Move(pu.Desc, citem.Desc, sizeof(pu.Desc));
                            data = data + EDcode.EncodeBuffer(citem) + "/";
                            count++;
                            if (count >= 10)
                            {
                                break;
                            }
                        }
                        break;
                    }
                }
            }
            whocret.SendMsg(this, Grobal2.RM_SENDDETAILGOODSLIST, 0, this.ActorId, count, menuindex, data);
        }

        public int UserMakeNewItem_CheckCondition(TUserHuman hum, string itemname, ref int iPrice)
        {
            int result;
            List<string> list;
            int condition;
            condition = ObjNpc.COND_FAILURE;
            list = M2Share.GetMakeItemCondition(itemname, ref iPrice);
            if (hum.Gold < iPrice)
            {
                result = ObjNpc.COND_NOMONEY;
                return result;
            }
            //if (list != null)
            //{
            //    condition = ObjNpc.COND_SUCCESS;
            //    for (k = 0; k < list.Count; k++)
            //    {
            //        sourcename = list[k];
            //        sourcecount = (int)list.Values[k];
            //        for (i = 0; i < hum.ItemList.Count; i++)
            //        {
            //            pu = hum.ItemList[i];
            //            if (sourcename == svMain.UserEngine.GetStdItemName(pu.Index))
            //            {
            //                ps = svMain.UserEngine.GetStdItem(pu.Index);
            //                if (ps != null)
            //                {
            //                    if (ps.OverlapItem >= 1)
            //                    {
            //                        sourcecount = sourcecount - _MIN(pu.Dura, sourcecount);
            //                    }
            //                    else
            //                    {
            //                        sourcecount -= 1;
            //                    }
            //                }
            //            }
            //        }
            //        if (sourcecount > 0)
            //        {
            //            condition = ObjNpc.COND_FAILURE;
            //            break;
            //        }
            //    }
            //    if (condition == ObjNpc.COND_SUCCESS)
            //    {
            //        dellist = null;
            //        for (k = 0; k < list.Count; k++)
            //        {
            //            sourcename = list[k];
            //            sourcecount = (int)list.Values[k];
            //            for (i = hum.ItemList.Count - 1; i >= 0; i--)
            //            {
            //                pu = hum.ItemList[i];
            //                if (sourcecount > 0)
            //                {
            //                    if (sourcename == svMain.UserEngine.GetStdItemName(pu.Index))
            //                    {
            //                        ps = svMain.UserEngine.GetStdItem(pu.Index);
            //                        if (ps != null)
            //                        {
            //                            if (ps.OverlapItem >= 1)
            //                            {
            //                                if (pu.Dura < ((int)list.Values[k]))
            //                                {
            //                                    pu.Dura = 0;
            //                                }
            //                                else
            //                                {
            //                                    pu.Dura = pu.Dura - ((int)list.Values[k]);
            //                                }
            //                                if (pu.Dura > 0)
            //                                {
            //                                    hum.SendMsg(this, Grobal2.RM_COUNTERITEMCHANGE, 0, pu.MakeIndex, pu.Dura, 0, ps.Name);
            //                                    continue;
            //                                }
            //                            }
            //                            if (dellist == null)
            //                            {
            //                                dellist = new ArrayList();
            //                            }
            //                            dellist.Add(sourcename, hum.ItemList[i].MakeIndex as Object);
            //                            Dispose(hum.ItemList[i]);
            //                            hum.ItemList.RemoveAt(i);
            //                            sourcecount -= 1;
            //                        }
            //                    }
            //                }
            //                else
            //                {
            //                    break;
            //                }
            //            }
            //        }
            //        if (dellist != null)
            //        {
            //            hum.SendMsg(this, Grobal2.RM_DELITEMS, 0, (int)dellist, 0, 0, "");
            //        }
            //    }
            //}
            result = condition;
            return result;
        }

        public void UserMakeNewItem(TUserHuman whocret, string itmname)
        {
            const int MAKEPRICE = 100;
            int i;
            int rcode;
            bool done;
            ArrayList list;
            TUserItem pu;
            TUserItem newpu;
            TStdItem pstd;
            int iMakePrice;
            int iCheckResult;
            iMakePrice = MAKEPRICE;
            done = false;
            rcode = 1;
            for (i = 0; i < GoodsList.Count; i++)
            {
                if (done)
                {
                    break;
                }
                list = (ArrayList)GoodsList[i];
                pu = (TUserItem)list[0];
                pstd = M2Share.UserEngine.GetStdItem(pu.Index);
                if (pstd != null)
                {
                    if (pstd.Name == itmname)
                    {
                        // 酒捞袍 父靛绰 厚侩档 窃膊 眉农茄促.
                        iCheckResult = UserMakeNewItem_CheckCondition(whocret, itmname, ref iMakePrice);
                        if (iCheckResult != ObjNpc.COND_NOMONEY)
                        {
                            if (iCheckResult == ObjNpc.COND_SUCCESS)
                            {
                                newpu = new TUserItem();
                                M2Share.UserEngine.CopyToUserItemFromName(itmname, ref newpu);
                                if (whocret.AddItem(newpu))
                                {
                                    // whocret.Gold := whocret.Gold - iMakePrice;
                                    whocret.DecGold(iMakePrice);
                                    whocret.SendAddItem(newpu);
                                    // 父甸扁 己傍...
                                    // 肺弊巢辫
                                    // 力累_
                                    M2Share.AddUserLog("2\09" + whocret.MapName + "\09" + whocret.CX.ToString() + "\09" + whocret.CY.ToString() + "\09" + whocret.UserName + "\09" + M2Share.UserEngine.GetStdItemName(newpu.Index) + "\09" + newpu.MakeIndex.ToString() + "\09" + "1\09" + this.UserName);
                                    rcode = 0;
                                }
                                else
                                {
                                    Dispose(newpu);
                                    rcode = 2;
                                }
                            }
                            else
                            {
                                rcode = 4;
                            }
                        }
                        else
                        {
                            rcode = 3;
                        }
                    }
                }
            }
            if (rcode == 0)
            {
                whocret.SendMsg(this, Grobal2.RM_MAKEDRUG_SUCCESS, 0, whocret.Gold, 0, 0, "");
            }
            else
            {
                whocret.SendMsg(this, Grobal2.RM_MAKEDRUG_FAIL, 0, rcode, 0, 0, "");
            }
        }

        // 酒捞袍 力炼 橇肺矫廉.
        public void UserManufactureItem(TUserHuman whocret, string itmname)
        {
            const int MAKEPRICE = 100;
            int i;
            int j;
            int rcode;
            bool done;
            ArrayList list;
            TUserItem pu;
            TUserItem newpu;
            TStdItem pstd;
            string sMakeItemName = string.Empty;
            string[] sItemMakeIndex = new string[ObjNpc.MAX_SOURCECNT + 1];
            string[] sItemName = new string[ObjNpc.MAX_SOURCECNT + 1];
            string[] sItemCount = new string[ObjNpc.MAX_SOURCECNT + 1];
            int iCheckResult = 0;
            int iMakePrice = 0;
            int iMakeCount = 0;
            iMakePrice = MAKEPRICE;
            try
            {
                itmname = HUtil32.GetValidStr3(itmname, ref sMakeItemName, new string[] { "/" });
                for (i = 1; i <= ObjNpc.MAX_SOURCECNT; i++)
                {
                    itmname = HUtil32.GetValidStr3(itmname, ref sItemMakeIndex[i], new string[] { ":" });
                    itmname = HUtil32.GetValidStr3(itmname, ref sItemName[i], new string[] { ":" });
                    itmname = HUtil32.GetValidStr3(itmname, ref sItemCount[i], new string[] { "/" });
                }
                // /////////////////////////////////////////
#if DEBUG
                // sonmg
                whocret.SysMsg(sMakeItemName, 0);
                for (i = 1; i <= ObjNpc.MAX_SOURCECNT; i ++ )
                {
                    whocret.SysMsg(sItemMakeIndex[i] + sItemName[i] + sItemCount[i], 0);
                }
#endif
                // /////////////////////////////////////////
                done = false;
                rcode = 1;
                for (i = 0; i < GoodsList.Count; i++)
                {
                    if (done)
                    {
                        break;
                    }
                    list = (ArrayList)GoodsList[i];
                    pu = (TUserItem)list[0];
                    pstd = M2Share.UserEngine.GetStdItem(pu.Index);
                    if (pstd != null)
                    {
                        if (pstd.Name == sMakeItemName)
                        {
                            // 酒捞袍 父靛绰 厚侩档 窃膊 眉农茄促.
                            iCheckResult = CheckMakeItemCondition(whocret, sMakeItemName, sItemMakeIndex, sItemName, sItemCount, ref iMakePrice, ref iMakeCount);
                            if (iCheckResult != ObjNpc.COND_NOMONEY)
                            {
                                if (iCheckResult == ObjNpc.COND_SUCCESS)
                                {
                                    for (j = 0; j < iMakeCount; j++)
                                    {
                                        newpu = new TUserItem();
                                        M2Share.UserEngine.CopyToUserItemFromName(sMakeItemName, ref newpu);
                                        if (whocret.AddItem(newpu))
                                        {
                                            // whocret.Gold := whocret.Gold - iMakePrice;
                                            whocret.DecGold(iMakePrice);
                                            whocret.SendAddItem(newpu);
                                            // 父甸扁 己傍...
                                            // 力炼 己傍 肺弊
                                            // 
                                            // MainOutMessage( '[Manufacture] ' + whocret.UserName + ' ' + UserEngine.GetStdItemName (newpu.Index) + '(' + IntToStr(newpu.MakeIndex) + ') '
                                            // + '=> 昏力等 犁丰:' + sItemName[1] + ', ' + sItemName[2]
                                            // + ', ' + sItemName[3] + ', ' + sItemName[4]
                                            // + ', ' + sItemName[5] + ', ' + sItemName[6] );
                                            // 肺弊巢辫
                                            // 力累_
                                            M2Share.AddUserLog("2\09" + whocret.MapName + "\09" + whocret.CX.ToString() + "\09" + whocret.CY.ToString() + "\09" + whocret.UserName + "\09" + M2Share.UserEngine.GetStdItemName(newpu.Index) + "\09" + newpu.MakeIndex.ToString() + "\09" + "1\09" + this.UserName);
                                            rcode = 0;
                                        }
                                        else
                                        {
                                            Dispose(newpu);
                                            rcode = 2;
                                        }
                                    }
                                }
                                else if (iCheckResult == ObjNpc.COND_GEMFAIL)
                                {
                                    // 焊苛 力炼 角菩矫俊档 捣篮 狐廉 唱埃促.
                                    // whocret.Gold := whocret.Gold - iMakePrice;
                                    whocret.DecGold(iMakePrice);
                                    whocret.GoldChanged();
                                    // 肺弊巢辫
                                    // 力累_角菩
                                    M2Share.AddUserLog("2\09" + whocret.MapName + "\09" + whocret.CX.ToString() + "\09" + whocret.CY.ToString() + "\09" + whocret.UserName + "\09" + "FAIL\09" + "0\09" + "1\09" + this.UserName);
                                    rcode = 5;
                                }
                                else if (iCheckResult == ObjNpc.COND_MINERALFAIL)
                                {
                                    rcode = 6;
                                }
                                else if (iCheckResult == ObjNpc.COND_BAGFULL)
                                {
                                    rcode = 7;
                                }
                                else
                                {
                                    rcode = 4;
                                }
                            }
                            else
                            {
                                rcode = 3;
                            }
                        }
                    }
                }
                if (rcode == 0)
                {
                    whocret.SendMsg(this, Grobal2.RM_MAKEDRUG_SUCCESS, 0, whocret.Gold, 0, 0, "");
                }
                else
                {
                    whocret.SendMsg(this, Grobal2.RM_MAKEDRUG_FAIL, 0, rcode, 0, 0, "");
                }
            }
            catch
            {
                M2Share.MainOutMessage("[Exception] TMerchant.UserManufactureItem");
            }
        }

        // //////////////////////////////////////////////////////////
        // 荐龋籍狼 殿鞭阑 掘绢郴绰 窃荐.
        public int GetGradeOfGuardStoneByName(string strGuardStone)
        {
            int result = ObjNpc.GSG_ERROR;
            if (HUtil32.CompareBackLStr(strGuardStone, "(小)", 3) == true)
            {
                result = ObjNpc.GSG_SMALL;
            }
            else if (HUtil32.CompareBackLStr(strGuardStone, "(中)", 3) == true)
            {
                result = ObjNpc.GSG_MEDIUM;
            }
            else if (HUtil32.CompareBackLStr(strGuardStone, "(大)", 3) == true)
            {
                result = ObjNpc.GSG_LARGE;
            }
            else if ((HUtil32.CompareBackLStr(strGuardStone, "(特)", 4) == true) || (HUtil32.CompareBackLStr(strGuardStone, "特大", 2) == true))
            {
                result = ObjNpc.GSG_GREATLARGE;
            }
            else if (HUtil32.CompareBackLStr(strGuardStone, "石头", 5) == true)
            {
                result = ObjNpc.GSG_SUPERIOR;
            }
            else
            {
                result = ObjNpc.GSG_ERROR;
            }
            return result;
        }

        public int CheckMakeItemCondition(TUserHuman hum, string itemname, string[] sItemMakeIndex, string[] sItemName, string[] sItemCount, ref int iPrice, ref int iMakeCount)
        {
            int result;
            IList<string> list;
            int k;
            int i;
            int j;
            int icnt = 0;
            int sourcecount = 0;
            int counteritmcount = 0;
            int itemp = 0;
            int sourcemindex = 0;
            string sourcename;
            int condition;
            ArrayList dellist;
            TUserItem pu;
            TStdItem ps;
            int iGuardStoneGrade;
            int iProbability;
            double fTemporary;
            int iRequiredGuardStoneGrade;
            int iSumOutfitAbil;
            int iOutfitGrade;
            string[] sNewName = new string[ObjNpc.MAX_SOURCECNT - 1 + 1];
            string[] sNewCount = new string[ObjNpc.MAX_SOURCECNT - 1 + 1];
            string[] sNewMIndex = new string[ObjNpc.MAX_SOURCECNT - 1 + 1];
            int[] iListDoubleCount = new int[ObjNpc.MAX_SOURCECNT - 1 + 1];
            int checkcount;
            bool bCheckMIndex;
            string strPendant;
            string strGuardStone;
            string strGuardStone15;
            string strGuardStoneXLHigher;
            string delitemname;
            strPendant = "";
            strGuardStone = "";
            strGuardStone15 = "";
            strGuardStoneXLHigher = "";
            strPendant = "<吊坠>";
            strGuardStone = "<守护石>";
            strGuardStone15 = "<守护石15>";
            strGuardStoneXLHigher = "<守护石(特大)很高>";
            iProbability = 0;
            fTemporary = 0;
            condition = ObjNpc.COND_FAILURE;
            iRequiredGuardStoneGrade = 0;
            iOutfitGrade = 0;
            iSumOutfitAbil = 0;
            iGuardStoneGrade = ObjNpc.GSG_ERROR;
            list = M2Share.GetMakeItemCondition(itemname, ref iPrice);
            if (hum.CanAddItem() == false)
            {
                result = ObjNpc.COND_BAGFULL;
                hum.SysMsg("你的包袱已满。", 0);
                return result;
            }
            if (list != null)
            {
                if (list.Count > ObjNpc.MAX_SOURCECNT)
                {
                    M2Share.MainOutMessage("[Caution!] list.Count Overflow in TMerchant.UserManufactureItem");
                }
                condition = ObjNpc.COND_SUCCESS;
                for (j = 0; j < list.Count; j++)
                {
                    if (list[j].ToUpper() == strGuardStone)
                    {
                        iRequiredGuardStoneGrade = 1;
                    }
                    else if (list[j].ToUpper() == strGuardStoneXLHigher)
                    {
                        iRequiredGuardStoneGrade = 2;
                    }
                    else if (list[j].ToUpper() == strGuardStone15)
                    {
                        iRequiredGuardStoneGrade = 3;
                    }
                }
                for (k = 0; k < ObjNpc.MAX_SOURCECNT; k++)
                {
                    sourcemindex = HUtil32.Str_ToInt(sItemMakeIndex[k], 0);
                    sourcename = sItemName[k];
                    sourcecount = HUtil32.Str_ToInt(sItemCount[k], 0);
                    for (i = 0; i < hum.ItemList.Count; i++)
                    {
                        pu = hum.ItemList[i];
                        if (sItemName[k] == M2Share.UserEngine.GetStdItemName(pu.Index))
                        {
                            ps = M2Share.UserEngine.GetStdItem(pu.Index);
                            if (ps != null)
                            {
                                if (ps.OverlapItem >= 1)
                                {
                                    if (pu.Dura < sourcecount)
                                    {
                                        sourcecount = sourcecount - pu.Dura;
                                    }
                                    else
                                    {
                                        itemp = sourcecount;
                                        sourcecount = _MAX(0, itemp - pu.Dura);
                                    }
                                    if (sourcecount <= 0)
                                    {
                                        for (j = 0; j < list.Count; j++)
                                        {
                                            if (list[j] == sourcename)
                                            {
                                                sNewMIndex[j] = sItemMakeIndex[k];
                                                sNewName[j] = sourcename;
                                                sNewCount[j] = sItemCount[k];
                                            }
                                        }
                                        break;
                                    }
                                }
                                else
                                {
                                    if (sourcemindex == pu.MakeIndex)
                                    {
                                        for (j = 0; j < list.Count; j++)
                                        {
                                            if (list[j] == sourcename)
                                            {
                                                if (sNewName[j] == sourcename)
                                                {
                                                    sNewCount[j] = (HUtil32.Str_ToInt(sNewCount[j], 0) + 1).ToString();
                                                }
                                                else
                                                {
                                                    sNewCount[j] = sItemCount[k];
                                                    sNewMIndex[j] = sItemMakeIndex[k];
                                                }
                                                sNewName[j] = sourcename;
                                            }
                                        }
                                        if (new ArrayList(new int[] { 19, 20, 21, 22, 23, 24, 26 }).Contains(ps.StdMode))
                                        {
                                            for (j = 0; j < list.Count; j++)
                                            {
                                                if (list[j].ToUpper() == strPendant)
                                                {
                                                    sNewMIndex[j] = sItemMakeIndex[k];
                                                    sNewName[j] = sourcename;
                                                    sNewCount[j] = sItemCount[k];
                                                    iSumOutfitAbil = HiByte(ps.DC) + HiByte(ps.MC) + HiByte(ps.SC);
                                                    if (new ArrayList(new int[] { 22, 23 }).Contains(ps.StdMode))
                                                    {
                                                        if (iSumOutfitAbil <= 3)
                                                        {
                                                            iOutfitGrade = 0;
                                                        }
                                                        else if (iSumOutfitAbil == 4)
                                                        {
                                                            iOutfitGrade = 1;
                                                        }
                                                        else
                                                        {
                                                            iOutfitGrade = 2;
                                                        }
                                                    }
                                                    else if (new ArrayList(new int[] { 24, 26 }).Contains(ps.StdMode))
                                                    {
                                                        if (HiByte(ps.DC) > 0)
                                                        {
                                                            if (iSumOutfitAbil == 1)
                                                            {
                                                                iOutfitGrade = 0;
                                                            }
                                                            else if (iSumOutfitAbil == 2)
                                                            {
                                                                iOutfitGrade = 1;
                                                            }
                                                            else
                                                            {
                                                                iOutfitGrade = 2;
                                                            }
                                                        }
                                                        else
                                                        {
                                                            if (iSumOutfitAbil == 1)
                                                            {
                                                                iOutfitGrade = 0;
                                                            }
                                                            else if (new ArrayList(new int[] { 2, 3 }).Contains(iSumOutfitAbil))
                                                            {
                                                                iOutfitGrade = 1;
                                                            }
                                                            else
                                                            {
                                                                iOutfitGrade = 2;
                                                            }
                                                        }
                                                    }
                                                    else
                                                    {
                                                        // 格吧捞
                                                        if (iSumOutfitAbil <= 3)
                                                        {
                                                            // 啊焙
                                                            iOutfitGrade = 0;
                                                        }
                                                        else if (new ArrayList(new int[] { 4, 5 }).Contains(iSumOutfitAbil))
                                                        {
                                                            // 唱焙
                                                            iOutfitGrade = 1;
                                                        }
                                                        else
                                                        {
                                                            iOutfitGrade = 2;
                                                        }
                                                        // 促焙
                                                    }
                                                }
                                            }
                                        }
                                        if (ps.StdMode == 53)
                                        {
                                            // 荐龋籍 殿鞭阑 掘绢辰促.
                                            iGuardStoneGrade = GetGradeOfGuardStoneByName(sourcename);
                                            for (j = 0; j < list.Count; j++)
                                            {
                                                if (iGuardStoneGrade < ObjNpc.GSG_GREATLARGE)
                                                {
                                                    if ((list[j].ToUpper() == strGuardStone) || (list[j].ToUpper() == strGuardStone15))
                                                    {
                                                        sNewMIndex[j] = sItemMakeIndex[k];
                                                        sNewName[j] = sourcename;
                                                        sNewCount[j] = sItemCount[k];
                                                    }
                                                }
                                                else if (iGuardStoneGrade >= ObjNpc.GSG_GREATLARGE)
                                                {
                                                    if ((list[j].ToUpper() == strGuardStone) || (list[j].ToUpper() == strGuardStone15) || (list[j].ToUpper() == strGuardStoneXLHigher))
                                                    {
                                                        sNewMIndex[j] = sItemMakeIndex[k];
                                                        sNewName[j] = sourcename;
                                                        sNewCount[j] = sItemCount[k];
                                                    }
                                                }
                                                else
                                                {
                                                    // 荐龋籍 捞抚捞 捞惑窍促搁 Error : 犬牢秦 毫具窃.
                                                    M2Share.MainOutMessage("[Caution!] TMerchant.UserManufactureItem iGuardStoneGrade = GSG_ERROR");
                                                }
                                            }
                                        }
                                        // ------ 堡籍 鉴档 八荤 ------//
                                        if (ps.StdMode == 43)
                                        {
                                            // 堡籍
                                            if (iRequiredGuardStoneGrade == 1)
                                            {
                                                // 鸥涝 A
                                                if (pu.Dura < 11500)
                                                {
                                                    // 鉴档 12
                                                    condition = ObjNpc.COND_MINERALFAIL;
                                                }
                                            }
                                            else if (iRequiredGuardStoneGrade == 2)
                                            {
                                                // 鸥涝 B
                                                if (pu.Dura < 14500)
                                                {
                                                    // 鉴档 15
                                                    condition = ObjNpc.COND_MINERALFAIL;
                                                }
                                            }
                                            else if (iRequiredGuardStoneGrade == 3)
                                            {
                                                // 鸥涝 C
                                                if (pu.Dura < 14500)
                                                {
                                                    // 鉴档 15
                                                    condition = ObjNpc.COND_MINERALFAIL;
                                                }
                                            }
                                        }
                                        sourcecount -= 1;
                                        // 肮荐 皑家..
                                    }
                                }
                            }
                            // if ps <> nil then
                        }
                    }
                    if (sourcecount > 0)
                    {
                        condition = ObjNpc.COND_FAILURE;
                        // 肮荐 固崔捞搁 炼扒 救嘎澜埃林.
                        break;
                    }
                }
#if DEBUG
                for (k = 0; k < list.Count; k ++ )
                {
                    hum.SysMsg(sNewMIndex[k] + " " + sNewName[k] + " " + sNewCount[k], 2);
                }
#endif
                if ((condition == ObjNpc.COND_SUCCESS) || (condition == ObjNpc.COND_MINERALFAIL))
                {
                    checkcount = list.Count;
                    for (k = 0; k < list.Count; k++)
                    {
                        sourcename = sNewName[k];
                        sourcecount = HUtil32.Str_ToInt(sNewCount[k], 0);
                        //if ((sourcename == list[k]) && (sourcecount >= ((int)list[k])))
                        //{
                        //    iListDoubleCount[k] = sourcecount / ((int)list[k]);
                        //    checkcount -= 1;
                        //}
                        //else if (((list[k].ToUpper() == strPendant) || (list[k].ToUpper() == strGuardStone) || (list[k].ToUpper() == strGuardStone15) || (list[k].ToUpper() == strGuardStoneXLHigher)) && (sourcecount >= ((int)list.Values[k])))
                        //{
                        //    iListDoubleCount[k] = sourcecount / ((int)list[k]);
                        //    checkcount -= 1;
                        //}
                    }
                    if (checkcount > 0)
                    {
                        condition = ObjNpc.COND_FAILURE;
                    }
                }
                if (condition == ObjNpc.COND_SUCCESS)
                {
                    iMakeCount = iListDoubleCount[0];
                    for (k = 0; k < list.Count; k++)
                    {
                        if (iMakeCount > iListDoubleCount[k])
                        {
                            iMakeCount = iListDoubleCount[k];
                        }
                        // hum.SysMsg(IntToStr(iListDoubleCount[k]), 1);
                    }
                    if (hum.Gold < iPrice * iMakeCount)
                    {
                        result = ObjNpc.COND_NOMONEY;
                        return result;
                    }
                    if (hum.ItemList.Count + iMakeCount > Grobal2.MAXBAGITEM)
                    {
                        result = ObjNpc.COND_BAGFULL;
#if KOREA
                        hum.SysMsg("啊规捞 啊垫 瞒辑 力炼甫 且 荐 绝嚼聪促.", 0);
#else
                        hum.SysMsg("Your bag is full.", 0);
#endif
                        return result;
                    }
                    dellist = null;
                    for (j = 0; j < iMakeCount; j++)
                    {
                        for (k = 0; k < list.Count; k++)
                        {
                            sourcemindex = HUtil32.Str_ToInt(sNewMIndex[k], 0);
                            sourcename = sNewName[k];
                            //sourcecount = (int)list.Values[k];
                            //counteritmcount = (int)list.Values[k];
                            for (i = hum.ItemList.Count - 1; i >= 0; i--)
                            {
                                pu = hum.ItemList[i];
                                if (sourcecount > 0)
                                {
                                    if (sourcename == M2Share.UserEngine.GetStdItemName(pu.Index))
                                    {
                                        ps = M2Share.UserEngine.GetStdItem(pu.Index);
                                        if (ps != null)
                                        {
                                            if (ps.OverlapItem >= 1)
                                            {
                                                if (pu.Dura < counteritmcount)
                                                {
                                                    counteritmcount = counteritmcount - pu.Dura;
                                                    pu.Dura = 0;
                                                }
                                                else
                                                {
                                                    itemp = counteritmcount;
                                                    counteritmcount = _MAX(0, itemp - pu.Dura);
                                                    pu.Dura = (short)(pu.Dura - itemp);
                                                }
                                                if (pu.Dura > 0)
                                                {
                                                    hum.SendMsg(this, Grobal2.RM_COUNTERITEMCHANGE, 0, pu.MakeIndex, pu.Dura, 0, ps.Name);
                                                    continue;
                                                }
                                            }
                                            else
                                            {
                                                if (pu.MakeIndex != HUtil32.Str_ToInt(sNewMIndex[k], 0))
                                                {
                                                    bCheckMIndex = false;
                                                    for (icnt = 0; icnt < ObjNpc.MAX_SOURCECNT; icnt++)
                                                    {
                                                        if (pu.MakeIndex == HUtil32.Str_ToInt(sItemMakeIndex[icnt], 0))
                                                        {
                                                            bCheckMIndex = true;
                                                            break;
                                                        }
                                                    }
                                                    if (bCheckMIndex == false)
                                                    {
                                                        continue;
                                                    }
                                                }
                                            }
                                            if (dellist == null)
                                            {
                                                dellist = new ArrayList();
                                            }
                                            delitemname = M2Share.UserEngine.GetStdItemName(pu.Index);
                                            //dellist.Add(delitemname, hum.ItemList[i].MakeIndex as Object);
                                            M2Share.AddUserLog("44\09" + hum.MapName + "\09" + hum.CX.ToString() + "\09" + hum.CY.ToString() + "\09" + hum.UserName + "\09" + delitemname + "\09" + hum.ItemList[i].MakeIndex.ToString() + "\09" + "1\09" + this.UserName);
                                            Dispose(hum.ItemList[i]);
                                            hum.ItemList.RemoveAt(i);
                                            sourcecount -= 1;
                                        }
                                    }
                                }
                                else
                                {
                                    break;
                                }
                            }
                        }
                    }
                    if (dellist != null)
                    {
                        //hum.SendMsg(this, Grobal2.RM_DELITEMS, 0, (int)dellist, 0, 0, "");
                    }
                    if (iRequiredGuardStoneGrade > 0)
                    {
                        fTemporary = (hum.BodyLuck - hum.PlayerKillingPoint) / 250;
                        if (iRequiredGuardStoneGrade == 1)
                        {
                            iProbability = 50;
                        }
                        else if (iRequiredGuardStoneGrade == 2)
                        {
                            iProbability = 50;
                        }
                        else if (iRequiredGuardStoneGrade == 3)
                        {
                            iProbability = 50;
                        }
                        if (fTemporary >= 100)
                        {
                            iProbability = iProbability + 5;
                        }
                        else if ((fTemporary < 100) && (fTemporary >= 50))
                        {
                            iProbability = iProbability + 3;
                        }
                        switch (iGuardStoneGrade)
                        {
                            case ObjNpc.GSG_SMALL:
                                iProbability = iProbability + 5;
                                break;
                            case ObjNpc.GSG_MEDIUM:
                                iProbability = iProbability + 10;
                                break;
                            case ObjNpc.GSG_LARGE:
                                iProbability = iProbability + 15;
                                break;
                            case ObjNpc.GSG_GREATLARGE:
                                iProbability = iProbability + 30;
                                break;
                            case ObjNpc.GSG_SUPERIOR:
                                iProbability = iProbability + 50;
                                break;
                        }
                    }
                    if ((iRequiredGuardStoneGrade == 1) || (iRequiredGuardStoneGrade == 3))
                    {
                        if (iOutfitGrade == 0)
                        {
                            iProbability = iProbability + 10;
                        }
                        else if (iOutfitGrade == 1)
                        {
                            iProbability = iProbability + 20;
                        }
                        else if (iOutfitGrade == 2)
                        {
                            iProbability = iProbability + 40;
                        }
#if DEBUG
                        // test
                        hum.SysMsg("BodyLuck:" + Convert.ToString(hum.BodyLuck) + " - PKPoint:" + Convert.ToString(hum.PlayerKillingPoint) + " = " + Convert.ToString(fTemporary) + ", iProbability:" + (iProbability).ToString() + ", DC/MC/SC SUM :" + (iSumOutfitAbil).ToString(), 0);
#endif
                        if (new System.Random(100).Next() < iProbability)
                        {
                            condition = ObjNpc.COND_SUCCESS;
                        }
                        else
                        {
                            condition = ObjNpc.COND_GEMFAIL;
                        }
                    }
                    else if (iRequiredGuardStoneGrade == 2)
                    {
#if DEBUG
                        // test
                        hum.SysMsg("BodyLuck:" + Convert.ToString(hum.BodyLuck) + " - PKPoint:" + Convert.ToString(hum.PlayerKillingPoint) + " = " + Convert.ToString(fTemporary) + ", iProbability:" + (iProbability).ToString(), 0);
#endif
                        if (new System.Random(100).Next() < iProbability)
                        {
                            condition = ObjNpc.COND_SUCCESS;
                        }
                        else
                        {
                            condition = ObjNpc.COND_GEMFAIL;
                        }
                    }
                }
            }
            if (condition == ObjNpc.COND_SUCCESS)
            {
                M2Share.MainOutMessage("[Manufacture Success] " + hum.UserName + " " + itemname + "(" + iMakeCount.ToString() + ")" + " " + "=> Deleted Items:" + sNewName[0] + ", " + sNewName[1] + ", " + sNewName[2] + ", " + sNewName[3] + ", " + sNewName[4] + ", " + sNewName[5] + " " + "BodyLuck:" + Convert.ToString(hum.BodyLuck) + " - PKPoint:" + Convert.ToString(hum.PlayerKillingPoint) + " / 250 = " + Convert.ToString(fTemporary) + ", Prob.Manufacture Gem:" + iProbability.ToString());
            }
            result = condition;
            return result;
        }

        public void SendUserMarket(TUserHuman hum, int ItemType, int UserMode)
        {
            switch (UserMode)
            {
                case Grobal2.USERMARKET_MODE_BUY:
                case Grobal2.USERMARKET_MODE_INQUIRY:
                    hum.RequireLoadUserMarket(M2Share.ServerName + "_" + this.UserName, ItemType, UserMode, "", "");
                    break;
                case Grobal2.USERMARKET_MODE_SELL:
                    hum.SendUserMarketSellReady(this);
                    break;
            }
        }

        public override void RunMsg(TMessageInfo msg)
        {
            base.RunMsg(msg);
        }

        public override void Run()
        {
            int flag;
            long dwCurrentTick;
            long dwDelayTick;
            flag = 0;
            try
            {
                dwCurrentTick = HUtil32.GetTickCount();
                dwDelayTick = CreateIndex * 500;
                if (dwCurrentTick < dwDelayTick)
                {
                    dwDelayTick = 0;
                }
                if (dwCurrentTick - checkrefilltime > 5 * 60 * 1000 + dwDelayTick)
                {
                    checkrefilltime = dwCurrentTick - dwDelayTick;
                    flag = 1;
                    RefillGoods();
                    flag = 2;
                }
                if (dwCurrentTick - checkverifytime > 601 * 1000)
                {
                    checkverifytime = dwCurrentTick;
                    flag = 3;
                    VerifyUpgradeList();
                    flag = 4;
                }
                if (new System.Random(50).Next() == 0)
                {
                    this.Turn((byte)new System.Random(8).Next());
                }
                else if (new System.Random(80).Next() == 0)
                {
                    this.SendRefMsg(Grobal2.RM_HIT, this.Dir, this.CX, this.CY, 0, "");
                }
                if (BoCastleManage && M2Share.UserCastle.BoCastleUnderAttack)
                {
                    flag = 5;
                    if (!this.HideMode)
                    {
                        this.SendRefMsg(Grobal2.RM_DISAPPEAR, 0, 0, 0, 0, "");
                        this.HideMode = true;
                    }
                    flag = 6;
                }
                else
                {
                    if (!BoHiddenNpc)
                    {
                        if (this.HideMode)
                        {
                            this.HideMode = false;
                            this.SendRefMsg(Grobal2.RM_HIT, this.Dir, this.CX, this.CY, 0, "");
                        }
                    }
                }
            }
            catch
            {
                M2Share.MainOutMessage("[Exception] Merchant.Run (" + flag.ToString() + ") " + MarketName + "-" + this.MapName);
            }
            base.Run();
        }

        public void SendDecoItemListShow(TCreature who)
        {
            string data = string.Empty;
            int count = 0;
            who.SendMsg(this, Grobal2.RM_DECOITEM_LISTSHOW, 0, this.ActorId, count, 0, data);
        }
    }
}

