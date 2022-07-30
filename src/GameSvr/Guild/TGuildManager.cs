using System.Collections;
using System.IO;
using SystemModule;

namespace GameSvr
{
    public class TGuildManager
    {
        public ArrayList GuildList = null;

        public TGuildManager() : base()
        {
            GuildList = new ArrayList();
        }

        ~TGuildManager()
        {
            GuildList.Free();
            base.Destroy();
        }

        public void ClearGuildList()
        {
            int i;
            for (i = 0; i < GuildList.Count; i++)
            {
                (GuildList[i] as TGuild).Free();
            }
            GuildList.Clear();
        }

        public void LoadGuildList()
        {
            ArrayList strlist;
            int i;
            string gname;
            TGuild guild;
            if (File.Exists(svMain.GuildFile))
            {
                strlist = new ArrayList();
                strlist.LoadFromFile(svMain.GuildFile);
                for (i = 0; i < strlist.Count; i++)
                {
                    gname = strlist[i].Trim();
                    if (gname != "")
                    {
                        guild = new TGuild(gname);
                        GuildList.Add(guild);
                    }
                }
                strlist.Free();
                for (i = 0; i < GuildList.Count; i++)
                {
                    if (!(GuildList[i] as TGuild).LoadGuild())
                    {
                        svMain.MainOutMessage((GuildList[i] as TGuild).GuildName + " Information reading failure.");
                    }
                }
                svMain.MainOutMessage("Guild information " + GuildList.Count.ToString() + " read .");
            }
            else
            {
                svMain.MainOutMessage("no guild file loaded...");
            }
        }

        public void SaveGuildList()
        {
            ArrayList strlist;
            int i;
            if (svMain.ServerIndex == 0)
            {
                // 付胶磐 辑滚父 历厘阑 茄促.
                strlist = new ArrayList();
                for (i = 0; i < GuildList.Count; i++)
                {
                    strlist.Add((GuildList[i] as TGuild).GuildName);
                }
                try
                {
                    strlist.SaveToFile(svMain.GuildFile);
                }
                catch
                {
                    svMain.MainOutMessage(svMain.GuildFile + "Saving error...");
                }
                strlist.Free();
            }
        }

        public TGuild GetGuild(string gname)
        {
            TGuild result;
            int i;
            result = null;
            for (i = 0; i < GuildList.Count; i++)
            {
                if ((GuildList[i] as TGuild).GuildName.ToUpper() == gname.ToUpper())
                {
                    result = GuildList[i] as TGuild;
                    break;
                }
            }
            return result;
        }

        public TGuild GetGuildFromMemberName(string who)
        {
            TGuild result;
            int i;
            result = null;
            for (i = 0; i < GuildList.Count; i++)
            {
                if ((GuildList[i] as TGuild).IsMember(who))
                {
                    result = GuildList[i] as TGuild;
                    break;
                }
            }
            return result;
        }

        public bool AddGuild(string gname, string mastername)
        {
            bool result;
            TGuild guild;
            result = false;
            if (IsValidFileName(gname))
            {
                if (GetGuild(gname) == null)
                {
                    guild = new TGuild(gname);
                    guild.AddGuildMaster(mastername);
                    GuildList.Add(guild);
                    SaveGuildList();
                    result = true;
                }
            }
            return result;
        }

        public bool DelGuild(string gname)
        {
            bool result;
            int i;
            result = false;
            for (i = 0; i < GuildList.Count; i++)
            {
                if ((GuildList[i] as TGuild).GuildName == gname)
                {
                    if ((GuildList[i] as TGuild).MemberList.Count <= 1)
                    {
                        // 厘盔 馆券 饶 巩颇昏力.
                        svMain.GuildAgitMan.DelGuildAgit(gname);
                        (GuildList[i] as TGuild).BreakGuild();
                        // Guild绰 老何矾 皋葛府甫 秦力窍瘤 臼澜
                        // TGuild(GuildList[i]).Free();  //Free救茄 版快 皋葛府 穿利捞 积变促...
                        GuildList.RemoveAt(i);
                        SaveGuildList();
                        result = true;
                    }
                    break;
                }
            }
            return result;
        }

        public void CheckGuildWarTimeOut()
        {
            int i;
            int k;
            TGuild g;
            TGuildWarInfo pgw;
            bool changed;
            for (i = 0; i < GuildList.Count; i++)
            {
                g = GuildList[i] as TGuild;
                changed = false;
                for (k = g.KillGuilds.Count - 1; k >= 0; k--)
                {
                    pgw = g.KillGuilds.Values[k] as TGuildWarInfo;
                    if (HUtil32.GetTickCount() - pgw.WarStartTime > pgw.WarRemain)
                    {
                        g.CanceledGuildWar(pgw.WarGuild);
                        g.KillGuilds.Remove(k);
                        Dispose(pgw);
                        changed = true;
                    }
                }
                if (changed)
                {
                    g.GuildInfoChange();
                }
            }
        }
    }
}

