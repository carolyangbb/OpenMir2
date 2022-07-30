using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using SystemModule;

namespace GameSvr
{
    public class TGuild
    {
        public string GuildName = String.Empty;
        public ArrayList NoticeList = null;
        public ArrayList KillGuilds = null;
        public IList<TGuild> AllyGuilds = null;
        public ArrayList MemberList = null;
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
            KillGuilds = new ArrayList();
            AllyGuilds = new List<TGuild>();
            MemberList = new ArrayList();
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
            int i;
            TGuildRank pgrank;
            if (MemberList != null)
            {
                for (i = 0; i < MemberList.Count; i++)
                {
                    pgrank = MemberList[i] as TGuildRank;
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
            int i;
            ArrayList strlist;
            string str;
            string data = string.Empty;
            string rtstr;
            string rankname;
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
                for (i = 0; i < KillGuilds.Count; i++)
                {
                    Dispose(KillGuilds.Values[i] as TGuildWarInfo);
                }
                KillGuilds.Clear();
                AllyGuilds.Clear();
                step = 0;
                rank = 0;
                rankname = "";
                strlist = new ArrayList();
                strlist.LoadFromFile(svMain.GuildDir + flname);
                for (i = 0; i < strlist.Count; i++)
                {
                    str = (string)strlist[i];
                    if (str != "")
                    {
                        if (str[1] == ";")
                        {
                            continue;
                        }
                        if (str[1] != "+")
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
                            if (str[1] == "#")
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
                                                KillGuilds.Add(pgw.WarGuild.GuildName, pgw as Object);
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
                                                AllyGuilds.Add(data, aguild);
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
                                            pgrank.MemList = new ArrayList();
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
            int i;
            int k;
            int remain;
            ArrayList strlist;
            TGuildRank pgr;
            TGuildWarInfo pgw;
            strlist = new ArrayList();
            strlist.Add("门派公告");
            for (i = 0; i < NoticeList.Count; i++)
            {
                strlist.Add("+" + NoticeList[i]);
            }
            strlist.Add(" ");
            strlist.Add("敌对门派");
            for (i = 0; i < KillGuilds.Count; i++)
            {
                pgw = KillGuilds.Values[i] as TGuildWarInfo;
                remain = pgw.WarRemain - (HUtil32.GetTickCount() - pgw.WarStartTime);
                if (remain > 0)
                {
                    strlist.Add("+" + KillGuilds[i] + " " + remain.ToString());
                }
            }
            strlist.Add(" ");
            strlist.Add("结盟门派");
            for (i = 0; i < AllyGuilds.Count; i++)
            {
                strlist.Add("+" + AllyGuilds[i]);
            }
            strlist.Add(" ");
            strlist.Add("门派成员");
            for (i = 0; i < MemberList.Count; i++)
            {
                pgr = MemberList[i] as TGuildRank;
                strlist.Add("#" + pgr.Rank.ToString() + " " + pgr.RankName);
                for (k = 0; k < pgr.MemList.Count; k++)
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
                // 咯扁辑 矫埃登搁 历厘窃.
                if (HUtil32.GetTickCount() - guildsavetime > 30 * 1000)
                {
                    // 函版等瘤 30檬
                    dosave = false;
                    SaveGuild();
                }
            }
        }

        public bool IsMember(string who)
        {
            bool result;
            int i;
            int k;
            TGuildRank pgrank;
            result = false;
            CheckSave();
            if (MemberList != null)
            {
                for (i = 0; i < MemberList.Count; i++)
                {
                    pgrank = MemberList[i] as TGuildRank;
                    for (k = 0; k < pgrank.MemList.Count; k++)
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
            string result;
            int i;
            int k;
            TGuildRank pgrank;
            result = "";
            if (MemberList != null)
            {
                for (i = 0; i < MemberList.Count; i++)
                {
                    pgrank = MemberList[i] as TGuildRank;
                    for (k = 0; k < pgrank.MemList.Count; k++)
                    {
                        if (pgrank.MemList[k] == ((TCreature)who).UserName)
                        {
                            pgrank.MemList.Values[k] = who;
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
                    if ((MemberList[0] as TGuildRank).MemList.Count > 0)
                    {
                        result = (MemberList[0] as TGuildRank).MemList[0];
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
                    if ((MemberList[0] as TGuildRank).MemList.Count >= 2)
                    {
                        result = (MemberList[0] as TGuildRank).MemList[1];
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
                    pgrank.MemList = new ArrayList();
                    pgrank.MemList.Add(who);
                    MemberList.Add(pgrank);
                    SaveGuild();
                    result = true;
                }
            }
            return result;
        }

        // 贸澜俊 巩颇 积己瞪锭父 荤侩
        public bool DelGuildMaster(string who)
        {
            bool result;
            TGuildRank pgrank;
            result = false;
            // 巩林 去磊 巢疽阑 版快, 巩林 呕硼搁 巩颇 柄咙..
            if (MemberList != null)
            {
                if (MemberList.Count == 1)
                {
                    // 葛电 流氓阑 促 力芭茄 惑怕
                    pgrank = MemberList[0] as TGuildRank;
                    if (pgrank.MemList.Count == 1)
                    {
                        if (pgrank.MemList[0] == who)
                        {
                            BreakGuild();
                            // 巩颇 柄咙... 葛电巩盔 漏覆
                            result = true;
                        }
                    }
                }
            }
            return result;
        }

        // 付瘤阜 巩林唱啊搁 巩颇 柄咙
        public void BreakGuild()
        {
            // 巩颇 荤扼咙, //辨靛俊 肺弊牢茄 荤恩甸阑 巩颇俊辑 猾促.
            int i;
            int k;
            TGuildRank pgrank;
            TUserHuman hum;
            if (svMain.ServerIndex == 0)
            {
                BackupGuild(svMain.GuildDir + GuildName + "." + GetCurrentTime.ToString() + ".bak");
            }
            if (MemberList != null)
            {
                for (i = 0; i < MemberList.Count; i++)
                {
                    pgrank = MemberList[i] as TGuildRank;
                    for (k = 0; k < pgrank.MemList.Count; k++)
                    {
                        hum = (TUserHuman)pgrank.MemList.Values[k];
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
                    Dispose(KillGuilds.Values[i] as TGuildWarInfo);
                }
                KillGuilds.Clear();
            }
            if (AllyGuilds != null)
            {
                AllyGuilds.Clear();
            }
            SaveGuild();
        }

        // 碍力肺 巩颇啊 绝绢咙. (林狼)
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
                    pgrank = MemberList[i] as TGuildRank;
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
                    drank.MemList = new ArrayList();
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
                    pgrank = MemberList[i] as TGuildRank;
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
                    pgrank = MemberList[i] as TGuildRank;
                    for (k = 0; k < pgrank.MemList.Count; k++)
                    {
                        if (pgrank.MemList.Values[k] == who)
                        {
                            if (((TCreature)who).MyGuild != null)
                            {
                                if ((((TCreature)who).MyGuild as TGuild).KillGuilds != null)
                                {
                                    for (j = 0; j < KillGuilds.Count; j++)
                                    {
                                        if ((KillGuilds.Values[j] as TGuildWarInfo).WarGuild != null)
                                        {
                                            (KillGuilds.Values[j] as TGuildWarInfo).WarGuild.CheckSave();
                                        }
                                    }
                                }
                            }
                            pgrank.MemList.Values[k] = null;
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
                    prank = MemberList[i] as TGuildRank;
                    for (k = 0; k < prank.MemList.Count; k++)
                    {
                        if (prank.MemList.Values[k] != null)
                        {
                            hum = (TUserHuman)prank.MemList.Values[k];
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
                    prank = MemberList[i] as TGuildRank;
                    for (k = 0; k < prank.MemList.Count; k++)
                    {
                        if (prank.MemList.Values[k] != null)
                        {
                            hum = (TUserHuman)prank.MemList.Values[k];
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

        public void UpdateGuildRankStr_FreeMembs(ref ArrayList memb)
        {
            int i;
            for (i = 0; i < memb.Count; i++)
            {
                (memb[i] as TGuildRank).MemList.Free();
                Dispose(memb[i] as TGuildRank);
            }
            memb.Free();
        }

        public int UpdateGuildRankStr(string rankstr)
        {
            int result;
            ArrayList nmembs;
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
            // 颇教 -> pgr狼 府胶飘肺 父电促.
            nmembs = new ArrayList();
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
                    if (data[1] == "#")
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
                        // 流氓捞 厚绢乐绰 版快甫 阜绰促.(sonmg 2004/08/16)
                        if (pgr.RankName == "")
                        {
                            result = -7;
                            return result;
                        }
                        pgr.MemList = new ArrayList();
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
            // 捞傈 沥焊客 沥犬捞 鞍栏搁 歹 捞惑 贸府 救窃.
            if (MemberList != null)
            {
                if (MemberList.Count == nmembs.Count)
                {
                    flag = true;
                    for (i = 0; i < MemberList.Count; i++)
                    {
                        pgr = MemberList[i] as TGuildRank;
                        pgr2 = nmembs[i] as TGuildRank;
                        if ((pgr.Rank == pgr2.Rank) && (pgr.RankName == pgr2.RankName) && (pgr.MemList.Count == pgr2.MemList.Count))
                        {
                            for (k = 0; k < pgr.MemList.Count; k++)
                            {
                                if (pgr.MemList[k] != pgr2.MemList[k])
                                {
                                    flag = false;
                                    // 促抚
                                    break;
                                }
                            }
                        }
                        else
                        {
                            flag = false;
                            // 促抚
                            break;
                        }
                    }
                    if (flag)
                    {
                        result = -1;
                        // 捞傈苞 沥犬洒 度 鞍澜.
                        UpdateGuildRankStr_FreeMembs(ref nmembs);
                        return result;
                    }
                }
            }
            // 巩林啊 狐廉乐栏搁救凳.
            result = -2;
            // 巩林啊 狐廉 乐澜.
            if (nmembs.Count > 0)
            {
                if ((nmembs[0] as TGuildRank).Rank == 1)
                {
                    if ((nmembs[0] as TGuildRank).RankName != "")
                    {
                        result = 0;
                        // 沥惑
                    }
                    else
                    {
                        result = -3;
                    }
                    // 巩林狼 疙莫捞 绝澜.
                }
            }
            // 货肺 巩林啊 函版登绰 版快, 货肺 函版等 荤恩捞 立加窍绊 乐绢具 茄促.
            // 巩林狼 荐绰 2疙阑 檬苞 给窃
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
                    // 利绢档 茄疙狼 巩林啊 立加窍绊 乐绢具 茄促.
                }
                else
                {
                    result = -4;
                }
                // 巩林 2疙 檬苞 给窃
            }
            // 葛电 巩盔捞 眠啊,昏力 登绢 乐瘤 臼酒具 茄促.
            if (result == 0)
            {
                oldcount = 0;
                newcount = 0;
                // 货肺函版等 府胶飘客 捞傈府胶飘客 厚背窃
                for (i = 0; i < MemberList.Count; i++)
                {
                    pgr = MemberList[i] as TGuildRank;
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
                            // 巩盔府胶飘啊 老摹窍瘤 臼澜.
                            break;
                        }
                    }
                    if (!flag)
                    {
                        break;
                    }
                }
                // 促矫茄锅 芭操肺 厚背茄促.
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
                            pgr2 = MemberList[k] as TGuildRank;
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
                            // 巩盔府胶飘啊 老摹窍瘤 臼澜.
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
                        // 巩盔府胶飘啊 老摹窍瘤 臼澜 .
                    }
                }
            }
            // 流氓捞 吝汗登瘤 臼疽绰瘤 八荤.
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
                            // 流氓 吝汗
                            break;
                        }
                    }
                    if (result != 0)
                    {
                        break;
                    }
                }
            }
            // 函版 -> 立加窍绊 乐绰 葛电 巩盔俊霸 流氓苞 流氓疙阑 朝妨霖促.
            if (result == 0)
            {
                UpdateGuildRankStr_FreeMembs(ref MemberList);
                // 肯傈洒 Free矫糯
                MemberList = nmembs;
                for (i = 0; i < MemberList.Count; i++)
                {
                    pgr = MemberList[i] as TGuildRank;
                    for (k = 0; k < pgr.MemList.Count; k++)
                    {
                        hum = svMain.UserEngine.GetUserHuman((string)pgr.MemList[k]);
                        if (hum != null)
                        {
                            pgr.MemList.Values[k] = hum;
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
                if ((KillGuilds.Values[i] as TGuildWarInfo).WarGuild == aguild)
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
                        if ((KillGuilds.Values[i] as TGuildWarInfo).WarGuild == aguild)
                        {
                            pgw = KillGuilds.Values[i] as TGuildWarInfo;
                            if (pgw != null)
                            {
                                // 12矫埃 力茄
                                if (pgw.WarRemain < 12 * timeunit)
                                {
                                    // 巩颇傈 楷厘 荐沥(sonmg 2005/08/05)
                                    pgw.WarRemain = pgw.WarStartTime + pgw.WarRemain - currenttime + 3 * timeunit;
                                    pgw.WarStartTime = currenttime;
                                    GuildMsg("***" + aguild.GuildName + "门派战争将持续三个小时。");
                                    svMain.UserEngine.SendInterMsg(Grobal2.ISM_GUILDMSG, svMain.ServerIndex, GuildName + "/" + "***" + aguild.GuildName + "苞(客)狼 巩颇傈捞 3矫埃 楷厘登菌嚼聪促.(巢篮 矫埃 : 距 " + (pgw.WarRemain / timeunit).ToString() + "矫埃)");
                                    svMain.UserEngine.SendInterMsg(Grobal2.ISM_GUILDWAR, svMain.ServerIndex, GuildName + "/" + aguild.GuildName + "/" + pgw.WarStartTime.ToString() + "/" + pgw.WarRemain.ToString());
                                    backresult = 2;
                                    // 楷厘
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
                        KillGuilds.Add(aguild.GuildName, pgw as Object);
                        GuildMsg("***" + aguild.GuildName + "门派战争开始(三个小时)。");
                        svMain.UserEngine.SendInterMsg(Grobal2.ISM_GUILDMSG, svMain.ServerIndex, GuildName + "/" + "***" + aguild.GuildName + "苞(客) 巩颇傈捞 矫累登菌嚼聪促.(3矫埃)");
                        svMain.UserEngine.SendInterMsg(Grobal2.ISM_GUILDWAR, svMain.ServerIndex, GuildName + "/" + aguild.GuildName + "/" + pgw.WarStartTime.ToString() + "/" + pgw.WarRemain.ToString());
                        backresult = 1;
                        // 矫累
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

        // 惑措规 巩颇客 巩颇接阑 扒促.
        // 3矫埃悼救 蜡瓤, 酒公锭唱 且 荐 乐促.
        public bool IsAllyGuild(TGuild aguild)
        {
            bool result;
            int i;
            // 悼竿巩颇牢瘤...
            result = false;
            if (aguild == null)
            {
                return result;
            }
            for (i = 0; i < AllyGuilds.Count; i++)
            {
                if (AllyGuilds.Values[i] == aguild)
                {
                    result = true;
                    return result;
                }
            }
            return result;
        }

        // 傍己巩颇狼 悼竿巩颇牢瘤 犬牢 货肺 眠啊 (sonmg 2005/11/29)
        public bool IsRushAllyGuild(TGuild aguild)
        {
            bool result;
            int i;
            int j;
            // 傍己巩颇狼 悼竿巩颇牢瘤...
            result = false;
            if (aguild == null)
            {
                return result;
            }
            for (i = 0; i < AllyGuilds.Count; i++)
            {
                if (AllyGuilds.Values[i] == aguild)
                {
                    for (j = 0; j < svMain.UserCastle.RushGuildList.Count; j++)
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
            bool result;
            int i;
            result = false;
            for (i = 0; i < AllyGuilds.Count; i++)
            {
                if (AllyGuilds.Values[i] == aguild)
                {
                    // 吝汗八荤
                    return result;
                }
            }
            AllyGuilds.Add(aguild.GuildName, aguild);
            SaveGuild();
            return result;
        }

        // 惑措规 巩颇客 悼竿阑 搬己茄促.
        // 巩林尝府 辑肺 付林焊哥 且 荐 乐促.
        public bool CanAlly(TGuild aguild)
        {
            bool result;
            int i;
            result = false;
            for (i = 0; i < KillGuilds.Count; i++)
            {
                if ((KillGuilds.Values[i] as TGuildWarInfo).WarGuild == aguild)
                {
                    // 傈里吝捞搁 悼竿阑 给茄促.
                    return result;
                }
            }
            result = true;
            return result;
        }

        public bool BreakAlly(TGuild aguild)
        {
            bool result;
            int i;
            result = false;
            for (i = 0; i < AllyGuilds.Count; i++)
            {
                if ((AllyGuilds.Values[i] as TGuild) == aguild)
                {
                    // 悼竿吝捞搁
                    AllyGuilds.Remove(i);
                    result = true;
                    break;
                }
            }
            SaveGuild();
            return result;
        }

        // 巩颇 措访 包访 窃荐
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
            int i;
            int n;
            if (BoStartGuildFight)
            {
                // 巩颇傈 吝俊父 痢荐啊 棵扼埃促.
                MatchPoint = MatchPoint + point;
                for (i = 0; i < FightMemberList.Count; i++)
                {
                    if (FightMemberList[i] == whostr)
                    {
                        n = (int)FightMemberList.Values[i];
                        // Loword: dead count, Hiword: win point
                        FightMemberList.Values[i] = HUtil32.MakeLong(Loword(n), Hiword(n) + point) as Object;
                    }
                }
            }
        }

        public void TeamFightWhoDead(string whostr)
        {
            int i;
            int n;
            if (BoStartGuildFight)
            {
                // 巩颇傈 吝俊父 扁废等促.
                for (i = 0; i < FightMemberList.Count; i++)
                {
                    if (FightMemberList[i] == whostr)
                    {
                        n = (int)FightMemberList.Values[i];
                        // Loword: dead count, Hiword: win point
                        FightMemberList.Values[i] = HUtil32.MakeLong(Loword(n) + 1, Hiword(n)) as Object;
                    }
                }
            }
        }

        public int GetTotalMemberCount()
        {
            int result;
            int i;
            result = 0;
            // 巩林甫 器窃茄 葛电 巩颇盔狼 荐甫 舅妨淋.
            if (MemberList != null)
            {
                for (i = 0; i < MemberList.Count; i++)
                {
                    result = result + (MemberList[i] as TGuildRank).MemList.Count;
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

