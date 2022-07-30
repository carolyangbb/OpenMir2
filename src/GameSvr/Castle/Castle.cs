using System;

namespace GameSvr
{
    public class Castle
    {
        public const string CASTLEFILENAME = "Sabuk.txt";
        public const string CASTLEATTACERS = "AttackSabukWall.txt";
        public const int CASTLEMAXGOLD = 100000000;
        public const int TODAYGOLD = 5000000;
        public const string CASTLECOREMAP = "0150";
        public const string CASTLEBASEMAP = "D701";
        public const int COREDOORX = 631;
        public const int COREDOORY = 274;
        public const int MAXARCHER = 12;
        public const int MAXGUARD = 4;
    }

    public class TDefenseUnit
    {
        public int X;
        public int Y;
        public string UnitName = string.Empty;
        public bool BoDoorOpen;
        public int HP;
        public TCreature UnitObj;
    }

    public class TAttackerInfo
    {
        public DateTime AttackDate;
        public string GuildName = string.Empty;
        public TGuild Guild;
    }
}
