using System;
using System.Collections;
using System.Windows.Forms;
using SystemModule;

namespace GameSvr
{
    public class TEnvirList : ArrayList
    {
        public int ServerIndex = 0;

        public TEnvirList() : base()
        {
        }

        public void InitEnvirnoments()
        {
            for (var i = 0; i < this.Count; i++)
            {
                (this[i] as TEnvirnoment).ApplyDoors();
            }
        }

        // canattack : FALSE (傍拜阑 给窃, 惑痢殿)
        // gameroom  : TRUE (措访厘, 磷绢档 公秦窃)
        // ghost : TRUE (蜡飞狼笼, 荤恩捞 捧疙烙)
        // norecon : 犁立 啊瓷 咯何
        public TEnvirnoment AddEnvir(string mapname, string newmapname, string title, int serverindex, int needlevel, bool lawful, bool fightzone, bool fight2zone, bool fight3zone, bool fight4zone, bool dark, bool dawn, bool sunny, bool quiz, bool norecon, bool needhole, bool norecall, bool norandommove, bool NoEscapeMove, bool NoTeleportMove, bool nodrug, int minemap, bool nopositionmove, string backmap, Object npc, int setnumber, int setvalue, int autoAttack, int GuildAgit, bool nochat, bool nogroup, bool nothrowitem, bool nodropitem, bool dostall, bool nodeal)
        {
            TEnvirnoment result;
            TEnvirnoment envir;
            int i;
            result = null;
            envir = new TEnvirnoment();
            envir.MapName = mapname;
            envir.NewMapName = newmapname;
            if (newmapname != "")
            {
                envir.UseNewMap = true;
            }
            envir.MapTitle = title;
            envir.Server = serverindex;
            envir.LawFull = lawful;
            envir.FightZone = fightzone;
            envir.Fight2Zone = fight2zone;
            envir.Fight3Zone = fight3zone;
            envir.Fight4Zone = fight4zone;
            envir.Darkness = dark;
            envir.Dawn = dawn;
            // 货寒眠啊
            envir.DayLight = sunny;
            envir.QuizZone = quiz;
            envir.NoReconnect = norecon;
            envir.NeedHole = needhole;
            envir.NoRecall = norecall;
            envir.NoRandomMove = norandommove;
            envir.NoEscapeMove = NoEscapeMove;
            // sonmg
            envir.NoTeleportMove = NoTeleportMove;
            // sonmg
            envir.NoDrug = nodrug;
            envir.MineMap = minemap;
            envir.NoPositionMove = nopositionmove;
            envir.BackMap = backmap;
            envir.MapQuest = npc;
            envir.NeedSetNumber = setnumber;
            envir.NeedSetValue = setvalue;
            envir.AutoAttack = autoAttack;
            envir.GuildAgit = GuildAgit;
            envir.NoChat = nochat;
            envir.NoGroup = nogroup;
            envir.NoThrowItem = nothrowitem;
            envir.NoDropItem = nodropitem;
            envir.BoStall = dostall;
            envir.NoDeal = nodeal;
            for (i = 0; i < svMain.MiniMapList.Count; i++)
            {
                if (svMain.MiniMapList[i].ToLower().CompareTo(mapname.ToLower()) == 0)
                {
                    envir.MiniMap = (int)svMain.MiniMapList.Values[i];
                    break;
                }
            }
            if (GuildAgit > -1)
            {
                if (envir.LoadMap(svMain.MapDir + envir.GetGuildAgitRealMapName() + ".map"))
                {
                    result = envir;
                    this.Add(envir);
                }
                else
                {
                    // envir.ResizeMap (1, 1);
                    MessageBox.Show("file not found..  " + svMain.MapDir + mapname + ".map");
                }
            }
            else
            {
                if (newmapname != "")
                {
                    if (envir.LoadMap(svMain.MapDir + envir.GetGuildAgitRealMapName() + ".map"))
                    {
                        result = envir;
                        this.Add(envir);
                    }
                    else
                    {
                        // envir.ResizeMap (1, 1);
                        MessageBox.Show("file not found..  " + svMain.MapDir + mapname + ".map");
                    }
                }
                else
                {
                    if (envir.LoadMap(svMain.MapDir + mapname + ".map"))
                    {
                        result = envir;
                        this.Add(envir);
                    }
                    else
                    {
                        // envir.ResizeMap (1, 1);
                        MessageBox.Show("file not found..  " + svMain.MapDir + mapname + ".map");
                    }
                }
            }
            return result;
        }

        public bool AddGate(string map, int x, int y, string entermap, int enterx, int entery)
        {
            bool result;
            TEnvirnoment envir;
            TEnvirnoment enter;
            TGateInfo pg;
            result = false;
            envir = GetEnvir(map);
            enter = GetEnvir(entermap);
            if ((envir != null) && (enter != null))
            {
                pg = new TGateInfo();
                pg.GateType = 0;
                pg.EnterEnvir = enter;
                pg.EnterX = enterx;
                pg.EnterY = entery;
                if (null != envir.AddToMap(x, y, Grobal2.OS_GATEOBJECT, pg))
                {
                    result = true;
                }
                else
                {
                    if (pg != null)
                    {
                        dispose(pg);
                    }
                    result = false;
                }
            }
            return result;
        }

        // map 捞抚狼 Envirnoment甫 掘绢咳   (one Map, one Envirnoment)
        public TEnvirnoment GetEnvir(string mapname)
        {
            TEnvirnoment result;
            int i;
            result = null;
            try
            {
                svMain.csShare.Enter();
                for (i = 0; i < this.Count; i++)
                {
                    if ((this[i] as TEnvirnoment).MapName.ToLower().CompareTo(mapname.ToLower()) == 0)
                    {
                        result = this[i] as TEnvirnoment;
                        return result;
                    }
                }
            }
            finally
            {
                svMain.csShare.Leave();
            }
            return result;
        }

        public TEnvirnoment ServerGetEnvir(int server, string mapname)
        {
            TEnvirnoment result;
            int i;
            result = null;
            try
            {
                svMain.csShare.Enter();
                for (i = 0; i < this.Count; i++)
                {
                    if (((this[i] as TEnvirnoment).Server == server) && ((this[i] as TEnvirnoment).MapName.ToLower().CompareTo(mapname.ToLower()) == 0))
                    {
                        result = this[i] as TEnvirnoment;
                        return result;
                    }
                }
            }
            finally
            {
                svMain.csShare.Leave();
            }
            return result;
        }

        public int GetServer(string mapname)
        {
            int result;
            int i;
            result = 0;
            try
            {
                svMain.csShare.Enter();
                for (i = 0; i < this.Count; i++)
                {
                    if ((this[i] as TEnvirnoment).MapName.ToLower().CompareTo(mapname.ToLower()) == 0)
                    {
                        result = (this[i] as TEnvirnoment).Server;
                        return result;
                    }
                }
            }
            finally
            {
                svMain.csShare.Leave();
            }
            return result;
        }

    }
}

