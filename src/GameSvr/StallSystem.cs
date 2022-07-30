using SystemModule;

namespace GameSvr
{
    public struct TClientStall
    {
        public int MakeIndex;
        public int Price;
        public byte GoldType;
    } 

    public struct TClientStallItems
    {
        public string Name;
        public TClientStall[] Items;
    }

    public class TClientStallInfo : ClientPacket
    {
        public int ItemCount;
        public string StallName;
        public TClientItem[] Items;
    }

    public class TStallInfo : ClientPacket
    {
        public bool Open;
        public ushort Looks;
        public string Name;
    }

    public class TStallMgr
    {
        public ushort StallType = 0;
        public bool OnSale = false;
        public TClientStallInfo mBlock = null;
        public bool DoShop = false;
        public int uSelIdx = 0;
        public TClientStallInfo uBlock = null;
        public int CurActor = 0;

        public TStallMgr()
        {
            StallType = 0;
            CurActor = 0;
            OnSale = false;
            DoShop = false;
            uSelIdx = -1;
            //FillChar(mBlock, sizeof(mBlock), 0);
            //FillChar(uBlock, sizeof(uBlock), 0);
        }
    }
}

namespace GameSvr
{
    public class StallSystem
    {
        public const int MAX_STALL_ITEM_COUNT = 10;
    }
}

