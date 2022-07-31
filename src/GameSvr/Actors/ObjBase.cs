using System;

namespace GameSvr
{
    public struct TUpgradeProb
    {
        public int[] iValue;
        public int iBase;
    }

    public class TSlaveInfo
    {
        public string SlaveName;
        public int SlaveExp;
        public byte SlaveExpLevel;
        public byte SlaveMakeLevel;
        public int RemainRoyalty;
        public int HP;
        public int MP;
    }

    public class TPkHiterInfo
    {
        public Object hiter;
        public long hittime;
    }
}