using System.Collections.Generic;
using SystemModule;

namespace GameSvr
{
    public class TMarketItemManager
    {
        public int UserMode
        {
            get
            {
                return FUserMode;
            }
            set
            {
                FUserMode = value;
            }
        }
        public int ItemType
        {
            get
            {
                return FItemType;
            }
            set
            {
                FItemType = value;
            }
        }
        public int LodedPage
        {
            get
            {
                return FLoadedpage;
            }
        }
        public int CurrPage
        {
            get
            {
                return FCurrPage;
            }
            set
            {
                FCurrPage = value;
            }
        }
        private int FState = 0;
        private readonly int FMaxPage = 0;
        private int FCurrPage = 0;
        private int FLoadedpage = 0;
        private IList<TMarketItem> FItems = null;
        private int FSelectedIndex = 0;
        private int FUserMode = 0;
        private int FItemType = 0;
        public TMarKetReqInfo ReqInfo;

        public TMarketItemManager()
        {
            InitFirst();
        }

        ~TMarketItemManager()
        {
            RemoveAll();
        }

        private void RemoveAll()
        {
            TMarketItem pinfo;
            for (var i = FItems.Count - 1; i >= 0; i--)
            {
                pinfo = FItems[i];
                if (pinfo != null)
                {
                    //Dispose(pinfo);
                }
                FItems.RemoveAt(i);
            }
            FItems.Clear();
            FState = MaketSystem.MAKET_STATE_EMPTY;
        }

        private bool CheckIndex(int Index_)
        {
            bool result;
            if ((Index_ >= 0) && (Index_ < FItems.Count))
            {
                result = true;
            }
            else
            {
                result = false;
            }
            return result;
        }

        private void InitFirst()
        {
            FItems = new List<TMarketItem>();
            FSelectedIndex = -1;
            FState = MaketSystem.MAKET_STATE_EMPTY;
            ReqInfo.UserName = "";
            ReqInfo.MarketName = "";
            ReqInfo.SearchWho = "";
            ReqInfo.SearchItem = "";
            ReqInfo.ItemType = 0;
            ReqInfo.ItemSet = 0;
            ReqInfo.UserMode = 0;
        }

        public void Load()
        {
            if (IsEmpty() && (FState == MaketSystem.MAKET_STATE_EMPTY))
            {
                OnMsgReadData();
            }
        }

        public void ReLoad()
        {
            if (!IsEmpty())
            {
                RemoveAll();
            }
            Load();
        }

        public void Add(TMarketItem pInfo_)
        {
            if ((FItems != null) && (pInfo_ != null))
            {
                FItems.Add(pInfo_);
            }
            if ((FItems.Count % MaketSystem.MAKET_ITEMCOUNT_PER_PAGE) == 0)
            {
                FLoadedpage = FItems.Count / MaketSystem.MAKET_ITEMCOUNT_PER_PAGE;
            }
            else
            {
                FLoadedpage = (FItems.Count / MaketSystem.MAKET_ITEMCOUNT_PER_PAGE) + 1;
            }
        }

        public void Delete(int Index_)
        {

        }

        public void Clear()
        {
            RemoveAll();
            FSelectedIndex = -1;
            FState = MaketSystem.MAKET_STATE_EMPTY;
        }

        public bool Select(int Index_)
        {
            bool result;
            result = false;
            if (CheckIndex(Index_))
            {
                FSelectedIndex = Index_;
                result = true;
            }
            return result;
        }

        public bool IsEmpty()
        {
            bool result;
            if (FItems.Count > 0)
            {
                result = false;
            }
            else
            {
                result = true;
            }
            return result;
        }

        public int Count()
        {
            return FItems.Count;
        }

        public int PageCount()
        {
            int result;
            if (FItems.Count % MaketSystem.MAKET_ITEMCOUNT_PER_PAGE == 0)
            {
                result = FItems.Count / MaketSystem.MAKET_ITEMCOUNT_PER_PAGE;
            }
            else
            {
                result = (FItems.Count / MaketSystem.MAKET_ITEMCOUNT_PER_PAGE) + 1;
            }
            return result;
        }

        public TMarketItem GetItem(int Index_, ref bool rSelected)
        {
            TMarketItem result = GetItem(Index_);
            if (result != null)
            {
                if (Index_ == FSelectedIndex)
                {
                    rSelected = true;
                }
                else
                {
                    rSelected = false;
                }
            }
            return result;
        }

        public TMarketItem GetItem(int Index_)
        {
            TMarketItem result = null;
            if (CheckIndex(Index_))
            {
                result = FItems[Index_];
            }
            return result;
        }

        public bool IsExistIndex(int Index_, ref int rMoney_)
        {
            bool result = false;
            TMarketItem pInfo;
            rMoney_ = 0;
            for (var i = 0; i < FItems.Count; i++)
            {
                pInfo = FItems[i];
                if (pInfo != null)
                {
                    if (pInfo.Index == Index_)
                    {
                        result = true;
                        rMoney_ = pInfo.SellPrice;
                        return result;
                    }
                }
            }
            return result;
        }

        public bool IsMyItem(int Index_, string CharName_)
        {
            bool result;
            int i;
            TMarketItem pInfo;
            result = false;
            if (CharName_ == "")
            {
                return result;
            }
            for (i = 0; i < FItems.Count; i++)
            {
                pInfo = FItems[i];
                if (pInfo != null)
                {
                    if (pInfo.Index == Index_)
                    {
                        if (pInfo.SellWho == CharName_)
                        {
                            result = true;
                        }
                        return result;
                    }
                }
            }
            return result;
        }

        // 여러가지 메세지 전송및 수신 -------------------------------------------------
        public void OnMsgReadData()
        {
        }

        public void OnMsgWriteData()
        {
        }
    }
}

namespace GameSvr
{
    public class MaketSystem
    {
        public const int MAKET_ITEMCOUNT_PER_PAGE = 10;
        public const int MAKET_MAX_PAGE = 15;
        public const double MAKET_MAX_ITEM_COUNT = MAKET_ITEMCOUNT_PER_PAGE * MAKET_MAX_PAGE;
        public const int MARKET_CHARGE_MONEY = 1000;
        public const int MARKET_ALLOW_LEVEL = 1;
        // 1 레벨 이상만 된다.
        public const int MARKET_COMMISION = 10;
        // 1000 분의 1 단위로 저장
        public const int MARKET_MAX_TRUST_MONEY = 50000000;
        // 최대금액
        public const int MARKET_MAX_SELL_COUNT = 5;
        // 최대 몇개까지 되나.
        public const int MAKET_STATE_EMPTY = 0;
        public const int MAKET_STATE_LOADING = 1;
        public const int MAKET_STATE_LOADED = 2;
    }
}