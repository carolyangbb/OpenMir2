using System.Collections.Generic;

namespace GameSvr
{
    public class Guild
    {
        public const int DEFRANK = 99;
        public const int GUILDAGIT_DAYUNIT = 7;
        public const int GUILDAGIT_SALEWAIT_DAYUNIT = 1;
        public const int MAXGUILDAGITCOUNT = 100;
        public const int GABOARD_NOTICE_LINE = 3;
        public const int GABOARD_COUNT_PER_PAGE = 10;
        public const int GABOARD_MAX_ARTICLE_COUNT = 73;
        public const string AGITDECOMONFILE = "AgitDecoMon.txt";
        public const int MAXCOUNT_DECOMON_PER_AGIT = 50;
        public const int GUILDAGITMAXGOLD = 100000000;
        public const int GUILDAGITREGFEE = 10000000;
        public const int GUILDAGITEXTENDFEE = 1000000;
    }

    public class TGuildRank
    {
        public int Rank;
        public string RankName;
        public IList<TCreature> MemList;
    }

    public class TGuildWarInfo
    {
        public TGuild WarGuild;
        public long WarStartTime;
        public long WarRemain;
    }
}
