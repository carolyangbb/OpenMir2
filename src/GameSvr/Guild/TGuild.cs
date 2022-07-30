using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using SystemModule;
using SystemModule.Common;

namespace GameSvr
{
    public class TGuild
    {
        public string GuildName = String.Empty;
        public ArrayList NoticeList = null;
        public IList<TGuildWarInfo> KillGuilds = null;
        public IList<TGuild> AllyGuilds = null;
        public IList<TGuildRank> MemberList = null;
        public int MatchPoint = 0;
        public bool BoStartGuildFight = false;
        public ArrayList FightMemberList = null;
        public bool AllowAllyGuild = false;
        private long guildsavetime = 0;
        private bool dosave = false;

        public TGuild(string gname) : base()
        {
            GuildName = gname;
            NoticeList = new ArrayList();
            KillGuilds = new List<TGuildWarInfo>();
            AllyGuilds = new List<TGuild>();
            MemberList = new List<TGuildRank>();
            FightMemberList = new ArrayList();
            guildsavetime = 0;
            dosave = false;
            MatchPoint = 0;
            BoStartGuildFight = false;
            AllowAllyGuild = false;
        }

        ~TGuild()
        {
            //NoticeList.Free();
            //KillGuilds.Free();
            //AllyGuilds.Free();
            //FreeMemberList();
            //MemberList.Free();
            //FightMemberList.Free();
            //base.Destroy;
        }

        private void FreeMemberList()
        {
            TGuildRank pgrank;
            if (MemberList != null)
            {
                for (var i = 0; i < MemberList.Count; i++)
                {
                    pgrank = MemberList[i];
                    if (pgrank.MemList != null)
                    {
                        pgrank.MemList.Free();
                    }
                    Dispose(pgrank);
                }
                MemberList.Clear();
            }
        }

        public bool LoadGuild()
        {
            bool result;
            result = LoadGuildFile(GuildName + ".txt");
            return result;
        }

        public bool LoadGuildFile(string flname)
        {
            bool result;
            StringList strlist;
            string str = string.Empty;
            string data = string.Empty;
            string rtstr = string.Empty;
            string rankname = string.Empty;
            int step;
            int rank;
            TGuildRank pgrank;
            TGuildWarInfo pgw;
            TGuild aguild;
            result = false;
            pgrank = null;
            if (File.Exists(svMain.GuildDir + flname))
            {
                FreeMemberList();
                NoticeList.Clear();
                for (var i = 0; i < KillGuilds.Count; i++)
                {
                    Dispose(KillGuilds[i]);
                }
                KillGuilds.Clear();
                AllyGuilds.Clear();
                step = 0;
                rank = 0;
                rankname = "";
                strlist = new StringList();
                strlist.LoadFromFile(svMain.GuildDir + flname);
                for (var i = 0; i < strlist.Count; i++)
                {
                    str = (string)strlist[i];
                    if (str != "")
                    {
                        if (str[0] == ';')
                        {
                            continue;
                        }
                        if (str[0] != '+')
                        {
                            if (str == "门派公告")
                            {
                                step = 1;
                            }
                            if (str == "敌对门派")
                            {
                                step = 2;
                            }
                            if (str == "结盟门派")
                            {
                                step = 3;
                            }
                            if (str == "门派成员")
                            {
                                step = 4;
                            }
                            if (str[0] == '#')
                            {
                                str = str.Substring(2 - 1, str.Length - 1);
                                str = HUtil32.GetValidStr3(str, ref data, new string[] { ",", " " });
                                rank = HUtil32.Str_ToInt(data, 0);
                                rankname = str.Trim();
                                pgrank = null;
                            }
                        }
                        else
                        {
                            str = str.Substring(2 - 1, str.Length - 1);
                            switch (step)
                            {
                                case 1:
                                    NoticeList.Add(str);
                                    break;
                                case 2:
                                    while (str != "")
                                    {
                                        str = HUtil32.GetValidStr3(str, ref data, new string[] { ",", " " });
                                        str = HUtil32.GetValidStr3(str, ref rtstr, new string[] { ",", " " });
                                        if (data != "")
                                        {
                                            pgw = new TGuildWarInfo();
                                            pgw.WarGuild = svMain.GuildMan.GetGuild(data);
                                            if (pgw.WarGuild != null)
                                            {
                                                pgw.WarStartTime  =  HUtil32.GetTickCount();
                                                pgw.WarRemain = HUtil32.Str_ToInt(rtstr.Trim(), 0);
                                                KillGuilds.Add(pgw);
                                            }
                                            else
                                            {
                                                Dispose(pgw);
                                            }
                                        }
                                        else
                                        {
                                            break;
                                        }
                                    }
                                    break;
                                case 3:
                                    while (str != "")
                                    {
                                        str = HUtil32.GetValidStr3(str, ref data, new string[] { ",", " " });
                                        if (data != "")
                                        {
                                            aguild = svMain.GuildMan.GetGuild(data);
                                            if (aguild != null)
                                            {
                                                AllyGuilds.Add(aguild);
                                            }
                                        }
                                        else
                                        {
                                            break;
                                        }
                                    }
                                    break;
                                case 4:
                                    if ((rank > 0) && (rankname != ""))
                                    {
                                        if (pgrank == null)
                                        {
                                            pgrank = new TGuildRank();
                                            pgrank.Rank = rank;
                                            pgrank.RankName = rankname;
                                            pgrank.MemList = new List<TCreature>();
                                            MemberList.Add(pgrank);
                                        }
                                        while (str != "")
                                        {
                                            str = HUtil32.GetValidStr3(str, ref data, new string[] { ",", " " });
                                            if (data != "")
                                            {
                                                pgrank.MemList.Add(data);
                                            }
                                            else
                                            {
                                                break;
                                            }
                                        }
                                    }
                                    break;
                            }
                        }
                    }
                }
                strlist.Free();
                result = true;
            }
            return result;
        }

        public void BackupGuild(string flname)
        {
            StringList strlist = new StringList();
            strlist.Add("门派公告");
            for (var i = 0; i < NoticeList.Count; i++)
            {
                strlist.Add("+" + NoticeList[i]);
            }
            strlist.Add(" ");
            strlist.Add("敌对门派");
            for (var i = 0; i < KillGuilds.Count; i++)
            {
                TGuildWarInfo pgw = KillGuilds[i];
                int remain = (int)(pgw.WarRemain - (HUtil32.GetTickCount() - pgw.WarStartTime));
                if (remain > 0)
                {
                    strlist.Add("+" + KillGuilds[i] + " " + remain.ToString());
                }
            }
            strlist.Add(" ");
            strlist.Add("结盟门派");
            for (var i = 0; i < AllyGuilds.Count; i++)
            {
                strlist.Add("+" + AllyGuilds[i]);
            }
            strlist.Add(" ");
            strlist.Add("门派成员");
            for (var i = 0; i < MemberList.Count; i++)
            {
                TGuildRank pgr = MemberList[i];
                strlist.Add("#" + pgr.Rank.ToString() + " " + pgr.RankName);
                for (var k = 0; k < pgr.MemList.Count; k++)
                {
                    strlist.Add("+" + pgr.MemList[k]);
                }
            }
            try
            {
                strlist.SaveToFile(flname);
            }
            catch
            {
                svMain.MainOutMessage(flname + "Saving error...");
            }
            strlist.Free();
        }

        public void SaveGuild()
        {
            if (svMain.ServerIndex == 0)
            {
                BackupGuild(svMain.GuildDir + GuildName + ".txt");
            }
            else
            {
                BackupGuild(svMain.GuildDir + GuildName + "." + svMain.ServerIndex.ToString());
            }
        }

        public void GuildInfoChange()
        {
            dosave = true;
            guildsavetime  =  HUtil32.GetTickCount();
            SaveGuild();
        }

        public void CheckSave()
        {
            if (dosave)
            {
                if (HUtil32.GetTickCount() - guildsavetime > 30 * 1000)
                {
                    dosave = false;
                    SaveGuild();
                }
            }
        }

        public bool IsMember(string who)
        {
            bool result = false;
            CheckSave();
            if (MemberList != null)
            {
                for (var i = 0; i < MemberList.Count; i++)
                {
                    TGuildRank pgrank = MemberList[i];
                    for (var k = 0; k < pgrank.MemList.Count; k++)
                    {
                        if (pgrank.MemList[k] == who)
                        {
                            result = true;
                            return result;
                        }
                    }
                }
            }
            return result;
        }

        public string MemberLogin(Object who, ref int grank)
        {
            TGuildRank pgrank;
            string result = "";
            if (MemberList != null)
            {
                for (var i = 0; i < MemberList.Count; i++)
                {
                    pgrank = MemberList[i];
                    for (var k = 0; k < pgrank.MemList.Count; k++)
                    {
                        if (pgrank.MemList[k] == ((TCreature)who).UserName)
                        {
                            pgrank.MemList[k] = who;
                            grank = pgrank.Rank;
                            result = pgrank.RankName;
                            ((TUserHuman)who).UserNameChanged();
                            ((TUserHuman)who).SendMsg((TUserHuman)who, Grobal2.RM_CHANGEGUILDNAME, 0, 0, 0, 0, "");
                            return result;
                        }
                    }
                }
            }
            return result;
        }

        public string GetGuildMaster()
        {
            string result;
            result = "";
            if (MemberList != null)
            {
                if (MemberList.Count > 0)
                {
                    if (MemberList[0].MemList.Count > 0)
                    {
                        result = MemberList[0].MemList[0];
                    }
                }
            }
            return result;
        }

        // 肚 促弗 巩林.
        public string GetAnotherGuildMaster()
        {
            string result;
            result = "";
            if (MemberList != null)
            {
                if (MemberList.Count > 0)
                {
                    if (MemberList[0].MemList.Count >= 2)
                    {
                        result = MemberList[0].MemList[1];
                    }
                }
            }
            return result;
        }

        public bool AddGuildMaster(string who)
        {
            bool result;
            TGuildRank pgrank;
            result = false;
            if (MemberList != null)
            {
                if (MemberList.Count == 0)
                {
                    pgrank = new TGuildRank();
                    pgrank.Rank = 1;
                    pgrank.RankName = "门派门主";
                    pgrank.MemList = new List<TCreature>();
                    pgrank.MemList.Add(who);
                    MemberList.Add(pgrank);
                    SaveGuild();
                    result = true;
                }
            }
            return result;
        }

        public bool DelGuildMaster(string who)
        {
            TGuildRank pgrank;
            bool result = false;
            if (MemberList != null)
            {
                if (MemberList.Count == 1)
                {
                    pgrank = MemberList[0];
                    if (pgrank.MemList.Count == 1)
                    {
                        if (pgrank.MemList[0] == who)
                        {
                            BreakGuild();
                            result = true;
                        }
                    }
                }
            }
            return result;
        }

        public void BreakGuild()
        {
            int i;
            int k;
            TGuildRank pgrank;
            TUserHuman hum;
            if (svMain.ServerIndex == 0)
            {
                BackupGuild(svMain.GuildDir + GuildName + "." + HUtil32.GetCurrentTime + ".bak");
            }
            if (MemberList != null)
            {
                for (i = 0; i < MemberList.Count; i++)
                {
                    pgrank = MemberList[i];
                    for (k = 0; k < pgrank.MemList.Count; k++)
                    {
                        hum = (TUserHuman)pgrank.MemList[k];
                        if (hum != null)
                        {
                            hum.MyGuild = null;
                            hum.GuildRankChanged(0, "");
                        }
                    }
                    pgrank.MemList.Free();
                }
                MemberList.Clear();
            }
            if (NoticeList != null)
            {
                NoticeList.Clear();
            }
            if (KillGuilds != null)
            {
                for (i = 0; i < KillGuilds.Count; i++)
                {
                    Dispose(KillGuilds[i]);
                }
                KillGuilds.Clear();
            }
            if (AllyGuilds != null)
            {
                AllyGuilds.Clear();
            }
            SaveGuild();
        }

        public bool AddMember(Object who)
        {
            bool result;
            int i;
            TGuildRank pgrank;
            TGuildRank drank;
            result = false;
            drank = null;
            if (MemberList != null)
            {
                for (i = 0; i < MemberList.Count; i++)
                {
                    pgrank = MemberList[i];
                    if (pgrank.Rank == Guild.DEFRANK)
                    {
                        drank = pgrank;
                        break;
                    }
                }
                if (drank == null)
                {
                    drank = new TGuildRank();
                    drank.Rank = Guild.DEFRANK;
                    drank.RankName = "门派成员";
                    drank.MemList = new List<TCreature>();
                    MemberList.Add(drank);
                }
                drank.MemList.Add(((TCreature)who).UserName, who);
                GuildInfoChange();
                result = true;
            }
            return result;
        }

        public bool DelMember(string who)
        {
            bool result;
            int i;
            int k;
            TGuildRank pgrank;
            bool done;
            result = false;
            done = false;
            if (MemberList != null)
            {
                for (i = 0; i < MemberList.Count; i++)
                {
                    pgrank = MemberList[i];
                    for (k = 0; k < pgrank.MemList.Count; k++)
                    {
                        if (pgrank.MemList[k] == who)
                        {
                            pgrank.MemList.Remove(k);
                            done = true;
                            break;
                        }
                    }
                    if (done)
                    {
                        break;
                    }
                }
            }
            if (done)
            {
                GuildInfoChange();
            }
            result = done;
            return result;
        }

        public void MemberLogout(Object who)
        {
            int i;
            int k;
            int j;
            TGuildRank pgrank;
            if (this == null)
            {
                return;
            }
            CheckSave();
            if (MemberList != null)
            {
                for (i = 0; i < MemberList.Count; i++)
                {
                    pgrank = MemberList[i];
                    for (k = 0; k < pgrank.MemList.Count; k++)
                    {
                        if (pgrank.MemList[k] == who)
                        {
                            if (((TCreature)who).MyGuild != null)
                            {
                                if (((TCreature)who).MyGuild.KillGuilds != null)
                                {
                                    for (j = 0; j < KillGuilds.Count; j++)
                                    {
                                        if (KillGuilds[j].WarGuild != null)
                                        {
                                            KillGuilds[j].WarGuild.CheckSave();
                                        }
                                    }
                                }
                            }
                            pgrank.MemList[k] = null;
                            return;
                        }
                    }
                }
            }
        }

        public void MemberNameChanged()
        {
            int i;
            int k;
            TGuildRank prank;
            TUserHuman hum;
            if (MemberList != null)
            {
                for (i = 0; i < MemberList.Count; i++)
                {
                    prank = MemberList[i];
                    for (k = 0; k < prank.MemList.Count; k++)
                    {
                        if (prank.MemList[k] != null)
                        {
                            hum = (TUserHuman)prank.MemList[k];
                            hum.UserNameChanged();
                        }
                    }
                }
            }
        }

        public void GuildMsg(string str, string nameexcept = "")
        {
            int i;
            int k;
            TGuildRank prank;
            TUserHuman hum;
            if (MemberList != null)
            {
                for (i = 0; i < MemberList.Count; i++)
                {
                    prank = MemberList[i];
                    for (k = 0; k < prank.MemList.Count; k++)
                    {
                        if (prank.MemList[k] != null)
                        {
                            hum = (TUserHuman)prank.MemList[k];
                            if (hum != null)
                            {
                                if (HUtil32.CompareLStr(str, "***", 3))
                                {
                                    hum.ChangeNameColor();
                                }
                                if (hum.UserName != nameexcept)
                                {
                                    if (hum.BoHearGuildMsg)
                                    {
                                        hum.SendMsg(hum, Grobal2.RM_GUILDMESSAGE, 0, 0, 0, 0, str);
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        public void UpdateGuildRankStr_FreeMembs(ref IList<TGuildRank> memb)
        {
            for (var i = 0; i < memb.Count; i++)
            {
                memb[i].MemList.Free();
                Dispose(memb[i]);
            }
            memb.Free();
        }

        public int UpdateGuildRankStr(string rankstr)
        {
            int result;
            IList<TGuildRank> nmembs;
            TGuildRank pgr;
            TGuildRank pgr2;
            int i;
            int j;
            int k;
            int m;
            int oldcount;
            int newcount;
            string data = string.Empty;
            string s1 = string.Empty;
            string s2 = string.Empty;
            string mname = string.Empty;
            bool flag;
            TUserHuman hum;
            result = -1;
            nmembs = new List<TGuildRank>();
            pgr = null;
            while (true)
            {
                if (rankstr == "")
                {
                    break;
                }
                rankstr = HUtil32.GetValidStr3(rankstr, ref data, new char[] { '\r' });
                data = data.Trim();
                if (data != "")
                {
                    if (data[0] == '#')
                    {
                        data = data.Substring(2 - 1, data.Length - 1);
                        data = HUtil32.GetValidStr3(data, ref s1, new string[] { " ", "<" });
                        data = HUtil32.GetValidStr3(data, ref s2, new string[] { "<", ">" });
                        if (pgr != null)
                        {
                            nmembs.Add(pgr);
                        }
                        pgr = new TGuildRank();
                        pgr.Rank = HUtil32.Str_ToInt(s1, 99);
                        pgr.RankName = s2.Trim();
                        if (pgr.RankName == "")
                        {
                            result = -7;
                            return result;
                        }
                        pgr.MemList = new List<TCreature>();
                    }
                    else
                    {
                        if (pgr != null)
                        {
                            for (i = 0; i <= 9; i++)
                            {
                                if (data == "")
                                {
                                    break;
                                }
                                data = HUtil32.GetValidStr3(data, ref mname, new string[] { " ", "," });
                                if (mname != "")
                                {
                                    pgr.MemList.Add(mname);
                                }
                            }
                        }
                    }
                }
            }
            if (pgr != null)
            {
                nmembs.Add(pgr);
            }
            if (MemberList != null)
            {
                if (MemberList.Count == nmembs.Count)
                {
                    flag = true;
                    for (i = 0; i < MemberList.Count; i++)
                    {
                        pgr = MemberList[i];
                        pgr2 = nmembs[i] as TGuildRank;
                        if ((pgr.Rank == pgr2.Rank) && (pgr.RankName == pgr2.RankName) && (pgr.MemList.Count == pgr2.MemList.Count))
                        {
                            for (k = 0; k < pgr.MemList.Count; k++)
                            {
                                if (pgr.MemList[k] != pgr2.MemList[k])
                                {
                                    flag = false;
                                    break;
                                }
                            }
                        }
                        else
                        {
                            flag = false;
                            break;
                        }
                    }
                    if (flag)
                    {
                        result = -1;
                        UpdateGuildRankStr_FreeMembs(ref nmembs);
                        return result;
                    }
                }
            }
            result = -2;
            if (nmembs.Count > 0)
            {
                if ((nmembs[0] as TGuildRank).Rank == 1)
                {
                    if ((nmembs[0] as TGuildRank).RankName != "")
                    {
                        result = 0;
                    }
                    else
                    {
                        result = -3;
                    }
                }
            }
            if (result == 0)
            {
                pgr = nmembs[0] as TGuildRank;
                if (pgr.MemList.Count <= 2)
                {
                    m = pgr.MemList.Count;
                    for (i = 0; i < pgr.MemList.Count; i++)
                    {
                        if (svMain.UserEngine.GetUserHuman((string)pgr.MemList[i]) == null)
                        {
                            m -= 1;
                            break;
                        }
                    }
                    if (m <= 0)
                    {
                        result = -5;
                    }
                }
                else
                {
                    result = -4;
                }
            }
            if (result == 0)
            {
                oldcount = 0;
                newcount = 0;
                for (i = 0; i < MemberList.Count; i++)
                {
                    pgr = MemberList[i];
                    flag = true;
                    for (j = 0; j < pgr.MemList.Count; j++)
                    {
                        flag = false;
                        mname = (string)pgr.MemList[j];
                        oldcount++;
                        for (k = 0; k < nmembs.Count; k++)
                        {
                            pgr2 = nmembs[k] as TGuildRank;
                            for (m = 0; m < pgr2.MemList.Count; m++)
                            {
                                if (mname == pgr2.MemList[m])
                                {
                                    flag = true;
                                    break;
                                }
                            }
                            if (flag)
                            {
                                break;
                            }
                        }
                        if (!flag)
                        {
                            result = -6;
                            break;
                        }
                    }
                    if (!flag)
                    {
                        break;
                    }
                }
                for (i = 0; i < nmembs.Count; i++)
                {
                    pgr = nmembs[i] as TGuildRank;
                    flag = true;
                    for (j = 0; j < pgr.MemList.Count; j++)
                    {
                        flag = false;
                        mname = (string)pgr.MemList[j];
                        newcount++;
                        for (k = 0; k < MemberList.Count; k++)
                        {
                            pgr2 = MemberList[k];
                            for (m = 0; m < pgr2.MemList.Count; m++)
                            {
                                if (mname == pgr2.MemList[m])
                                {
                                    flag = true;
                                    break;
                                }
                            }
                            if (flag)
                            {
                                break;
                            }
                        }
                        if (!flag)
                        {
                            result = -6;
                            break;
                        }
                    }
                    if (!flag)
                    {
                        break;
                    }
                }
                if (result == 0)
                {
                    if (oldcount != newcount)
                    {
                        result = -6;
                    }
                }
            }
            if (result == 0)
            {
                for (i = 0; i < nmembs.Count; i++)
                {
                    m = (nmembs[i] as TGuildRank).Rank;
                    for (k = i + 1; k < nmembs.Count; k++)
                    {
                        if ((m == (nmembs[k] as TGuildRank).Rank) || (m <= 0) || (m > 99))
                        {
                            result = -7;
                            break;
                        }
                    }
                    if (result != 0)
                    {
                        break;
                    }
                }
            }
            if (result == 0)
            {
                UpdateGuildRankStr_FreeMembs(ref MemberList);
                MemberList = nmembs;
                for (i = 0; i < MemberList.Count; i++)
                {
                    pgr = MemberList[i];
                    for (k = 0; k < pgr.MemList.Count; k++)
                    {
                        hum = svMain.UserEngine.GetUserHuman((string)pgr.MemList[k]);
                        if (hum != null)
                        {
                            pgr.MemList[k] = hum;
                            hum.GuildRankChanged(pgr.Rank, pgr.RankName);
                        }
                    }
                }
                GuildInfoChange();
            }
            else
            {
                UpdateGuildRankStr_FreeMembs(ref nmembs);
            }
            return result;
        }

        public bool IsHostileGuild(TGuild aguild)
        {
            bool result;
            int i;
            // 利措巩颇牢瘤...
            result = false;
            for (i = 0; i < KillGuilds.Count; i++)
            {
                if (KillGuilds[i].WarGuild == aguild)
                {
                    result = true;
                    break;
                }
            }
            return result;
        }

        public TGuildWarInfo DeclareGuildWar(TGuild aguild, long currenttime, ref int backresult)
        {
            TGuildWarInfo result;
            int i;
            TGuildWarInfo pgw;
            long timeunit;
            result = null;
            backresult = 0;
            timeunit = 60 * 60 * 1000;
            if (aguild != null)
            {
                if (!IsAllyGuild(aguild))
                {
                    pgw = null;
                    for (i = 0; i < KillGuilds.Count; i++)
                    {
                        if (KillGuilds[i].WarGuild == aguild)
                        {
                            pgw = KillGuilds[i];
                            if (pgw != null)
                            {
                                if (pgw.WarRemain < 12 * timeunit)
                                {
                                    pgw.WarRemain = pgw.WarStartTime + pgw.WarRemain - currenttime + 3 * timeunit;
                                    pgw.WarStartTime = currenttime;
                                    GuildMsg("***" + aguild.GuildName + "门派战争将持续三个小时。");
                                    svMain.UserEngine.SendInterMsg(Grobal2.ISM_GUILDMSG, svMain.ServerIndex, GuildName + "/" + "***" + aguild.GuildName + "苞(客)狼 巩颇傈捞 3矫埃 楷厘登菌嚼聪促.(巢篮 矫埃 : 距 " + (pgw.WarRemain / timeunit).ToString() + "矫埃)");
                                    svMain.UserEngine.SendInterMsg(Grobal2.ISM_GUILDWAR, svMain.ServerIndex, GuildName + "/" + aguild.GuildName + "/" + pgw.WarStartTime.ToString() + "/" + pgw.WarRemain.ToString());
                                    backresult = 2;
                                    break;
                                }
                            }
                        }
                    }
                    if (pgw == null)
                    {
                        pgw = new TGuildWarInfo();
                        pgw.WarGuild = aguild;
                        pgw.WarStartTime = currenttime;
                        pgw.WarRemain = 3 * timeunit;
                        KillGuilds.Add(aguild.GuildName, pgw);
                        GuildMsg("***" + aguild.GuildName + "门派战争开始(三个小时)。");
                        svMain.UserEngine.SendInterMsg(Grobal2.ISM_GUILDMSG, svMain.ServerIndex, GuildName + "/" + "***" + aguild.GuildName + "苞(客) 巩颇傈捞 矫累登菌嚼聪促.(3矫埃)");
                        svMain.UserEngine.SendInterMsg(Grobal2.ISM_GUILDWAR, svMain.ServerIndex, GuildName + "/" + aguild.GuildName + "/" + pgw.WarStartTime.ToString() + "/" + pgw.WarRemain.ToString());
                        backresult = 1;
                    }
                    result = pgw;
                }
            }
            MemberNameChanged();
            GuildInfoChange();
            return result;
        }

        public void CanceledGuildWar(TGuild aguild)
        {
            GuildMsg("***" + aguild.GuildName + "门派战争结束。");
        }

        public bool IsAllyGuild(TGuild aguild)
        {
            bool result = false;
            if (aguild == null)
            {
                return result;
            }
            for (var i = 0; i < AllyGuilds.Count; i++)
            {
                if (AllyGuilds[i] == aguild)
                {
                    result = true;
                    return result;
                }
            }
            return result;
        }

        public bool IsRushAllyGuild(TGuild aguild)
        {
            bool result = false;
            if (aguild == null)
            {
                return result;
            }
            for (var i = 0; i < AllyGuilds.Count; i++)
            {
                if (AllyGuilds[i] == aguild)
                {
                    for (var j = 0; j < svMain.UserCastle.RushGuildList.Count; j++)
                    {
                        if (svMain.UserCastle.RushGuildList[j] == aguild)
                        {
                            result = true;
                            break;
                        }
                    }
                    result = true;
                    return result;
                }
            }
            return result;
        }

        public bool MakeAllyGuild(TGuild aguild)
        {
            bool result = false;
            for (var i = 0; i < AllyGuilds.Count; i++)
            {
                if (AllyGuilds[i] == aguild)
                {
                    return result;
                }
            }
            AllyGuilds.Add(aguild);
            SaveGuild();
            return result;
        }

        public bool CanAlly(TGuild aguild)
        {
            bool result = false;
            for (var i = 0; i < KillGuilds.Count; i++)
            {
                if (KillGuilds[i].WarGuild == aguild)
                {
                    return result;
                }
            }
            result = true;
            return result;
        }

        public bool BreakAlly(TGuild aguild)
        {
            bool result = false;
            for (var i = 0; i < AllyGuilds.Count; i++)
            {
                if (AllyGuilds[i] == aguild)
                {
                    AllyGuilds.RemoveAt(i);
                    result = true;
                    break;
                }
            }
            SaveGuild();
            return result;
        }

        public void TeamFightStart()
        {
            MatchPoint = 0;
            BoStartGuildFight = true;
            FightMemberList.Clear();
        }

        public void TeamFightEnd()
        {
            BoStartGuildFight = false;
        }

        public void TeamFightAdd(string whostr)
        {
            FightMemberList.Add(whostr);
        }

        public void TeamFightWhoWinPoint(string whostr, int point)
        {
            if (BoStartGuildFight)
            {
                MatchPoint = MatchPoint + point;
                for (var i = 0; i < FightMemberList.Count; i++)
                {
                    if (FightMemberList[i] == whostr)
                    {
                        var n = (int)FightMemberList[i];
                        FightMemberList[i] = HUtil32.MakeLong(HUtil32.LoWord(n), HUtil32.HiWord(n) + point) as Object;
                    }
                }
            }
        }

        public void TeamFightWhoDead(string whostr)
        {
            if (BoStartGuildFight)
            {
                for (var i = 0; i < FightMemberList.Count; i++)
                {
                    if (FightMemberList[i] == whostr)
                    {
                        var n = (int)FightMemberList[i];
                        FightMemberList[i] = HUtil32.MakeLong(HUtil32.LoWord(n) + 1, HUtil32.HiWord(n)) as Object;
                    }
                }
            }
        }

        public int GetTotalMemberCount()
        {
            int result = 0;
            if (MemberList != null)
            {
                for (var i = 0; i < MemberList.Count; i++)
                {
                    result = result + MemberList[i].MemList.Count;
                }
            }
            return result;
        }

        public void Dispose(object obj)
        {
            if (obj != null)
            {
                obj = null;
            }
        }
    }
}

