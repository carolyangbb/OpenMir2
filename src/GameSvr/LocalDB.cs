using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Windows.Forms;
using SystemModule;
using SystemModule.Common;

namespace GameSvr
{
    public class TFrmDB
    {
        private IDataReader Query = null;

        public TFrmDB()
        {

        }

#if LOADSQL
        public int LoadStdItems()
        {
            int result;
            result =  -1;
            SQLSQLLocalDB.gItemMgr = new TItemMgr();
            if ((SQLSQLLocalDB.gItemMgr.Load(svMain.UserEngine.StdItemList, SQLLocalDB.TLoadType.ltSQL, 0)))
            {
                result = 1;
            }
            else
            {
                result =  -100;
            }
                        SQLSQLLocalDB.gItemMgr.Free();
            return result;
        }

#else
        public int LoadStdItems()
        {
            int result;
            int i;
            int idx;
            TStdItem item;
            TStdItem pitem;
            result = -1;
            Query.SQL.Clear();
            Query.SQL.Add("select * from StdItems");
            try
            {
                //Query.Open;
            }
            finally
            {
                result = -2;
            }
            for (i = 0; i < Query.RecordCount; i++)
            {
                idx = Query.FieldByName("Idx").AsInteger;
                item.Name = Query.FieldByName("NAME").AsString;
                item.StdMode = Query.FieldByName("StdMode").AsInteger;
                item.Shape = Query.FieldByName("Shape").AsInteger;
                item.Weight = Query.FieldByName("Weight").AsInteger;
                item.AniCount = Query.FieldByName("AniCount").AsInteger;
                item.SpecialPwr = Query.FieldByName("Source").AsInteger;
                item.ItemDesc = Query.FieldByName("Reserved").AsInteger;
                item.Looks = Query.FieldByName("Looks").AsInteger;
                item.DuraMax = Query.FieldByName("DuraMax").AsInteger;
                item.AC = HUtil32.MakeWord(Query.FieldByName("Ac").AsInteger, Query.FieldByName("Ac2").AsInteger);
                item.MAC = HUtil32.MakeWord(Query.FieldByName("Mac").AsInteger, Query.FieldByName("MAc2").AsInteger);
                item.DC = HUtil32.MakeWord(Query.FieldByName("Dc").AsInteger, Query.FieldByName("Dc2").AsInteger);
                item.MC = HUtil32.MakeWord(Query.FieldByName("Mc").AsInteger, Query.FieldByName("Mc2").AsInteger);
                item.SC = HUtil32.MakeWord(Query.FieldByName("Sc").AsInteger, Query.FieldByName("Sc2").AsInteger);
                item.Need = Query.FieldByName("Need").AsInteger;
                item.NeedLevel = Query.FieldByName("NeedLevel").AsInteger;
                item.Price = Query.FieldByName("Price").AsInteger;
                if (idx == svMain.UserEngine.StdItemList.Count)
                {
                    // 酒捞袍狼 DB Index客 府胶飘狼 牢郸胶啊 老摹秦具茄促.
                    pitem = new TStdItem();
                    pitem = item;
                    // 捞抚捞 绝绰 酒捞袍篮 荤扼柳 酒捞袍烙...
                    svMain.UserEngine.StdItemList.Add(pitem);
                    result = 1;
                }
                else
                {
                    result = -100;
                    break;
                }
                //this.Next;
            }
            this.Close();
            return result;
        }

#endif
        public int LoadMonItems(string monname, ref ArrayList ilist)
        {
            int i;
            int n;
            int m;
            int cnt;
            string str;
            string iname;
            string data = string.Empty;
            StringList strlist;
            TMonItemInfo pmi;
            int result = 0;
            string flname = svMain.EnvirDir + LocalDB.MONBAGDIR + monname + ".txt";
            if (!File.Exists(flname))
            {
                return result;
            }
            if (ilist != null)
            {
                for (i = 0; i < ilist.Count; i++)
                {
                    this.Dispose((TMonItemInfo)ilist[i]);
                }
                ilist.Clear();
            }
            strlist = new StringList();
            strlist.LoadFromFile(flname);
            for (i = 0; i < strlist.Count; i++)
            {
                str = strlist[i];
                if (str != "")
                {
                    if (str[0] == ';')
                    {
                        continue;
                    }
                    str = HUtil32.GetValidStr3(str, ref data, new string[] { " ", "/", "\09" });
                    n = HUtil32.Str_ToInt(data, -1);
                    str = HUtil32.GetValidStr3(str, ref data, new string[] { " ", "/", "\09" });
                    m = HUtil32.Str_ToInt(data, -1);
                    str = HUtil32.GetValidStrCap(str, ref data, new string[] { " ", "\09" });
                    if (data != "")
                    {
                        if (data[0] == '\"')
                        {
                            HUtil32.ArrestStringEx(data, "\"", "\"", ref data);
                        }
                    }
                    iname = data;
                    str = HUtil32.GetValidStr3(str, ref data, new string[] { " ", "\09" });
                    cnt = HUtil32.Str_ToInt(data, 1);
                    if ((n > 0) && (m > 0) && (iname != ""))
                    {
                        if (ilist == null)
                        {
                            ilist = new ArrayList();
                        }
                        pmi = new TMonItemInfo();
                        pmi.SelPoint = n - 1;
                        pmi.MaxPoint = m;
                        pmi.ItemName = iname;
                        pmi.Count = cnt;
                        ilist.Add(pmi);
                        result++;
                    }
                }
            }
            strlist.Free();
            return result;
        }

#if LOADSQL

        public int LoadMonsters()
        {
            int result;
            TMonsterInfo pMonInfo;
            int i;
            result = 0;
            SQLSQLLocalDB.gMonsterMgr = new TMonsterMgr();
            if (SQLSQLLocalDB.gMonsterMgr.Load(svMain.UserEngine.MonDefList, SQLLocalDB.TLoadType.ltSQL,  -1))
            {
                                SQLSQLLocalDB.gMonsterMgr.Free();
                                svMain.FrmMain.Memo1.Lines.Add("[SQL]LoadMonster Number:" + (svMain.UserEngine.MonDefList.Count).ToString());
                for (i = 0; i < svMain.UserEngine.MonDefList.Count; i ++ )
                {
                    pMonInfo = svMain.UserEngine.MonDefList[i];
                    pMonInfo.ItemList = new ArrayList();
                    LoadMonItems(pMonInfo.Name, ref pMonInfo.ItemList);
                }
                result = 1;
            }
            else
            {
                                SQLSQLLocalDB.gMonsterMgr.Free();
            }
            return result;
        }

#else
        public int LoadMonsters()
        {
            int result;
            int i;
            TMonsterInfo pm;
            result = 0;
            Query.SQL.Clear();
            Query.SQL.Add("select * from Monster");
            try
            {
                //Query.Open;
            }
            finally
            {
                result = -2;
            }
            for (i = 0; i < Query.RecordCount; i++)
            {
                pm = new TMonsterInfo();
                pm.Name = Query.FieldByName("NAME").AsString;
                pm.Race = Query.FieldByName("Race").AsInteger;
                pm.RaceImg = Query.FieldByName("RaceImg").AsInteger;
                pm.Appr = Query.FieldByName("Appr").AsInteger;
                pm.Level = Query.FieldByName("Lvl").AsInteger;
                pm.LifeAttrib = Query.FieldByName("Undead").AsInteger;
                pm.CoolEye = Query.FieldByName("CoolEye").AsInteger;
                pm.Exp = Query.FieldByName("Exp").AsInteger;
                pm.HP = Query.FieldByName("HP").AsInteger;
                pm.MP = Query.FieldByName("MP").AsInteger;
                pm.AC = Query.FieldByName("AC").AsInteger;
                pm.MAC = Query.FieldByName("MAC").AsInteger;
                pm.DC = Query.FieldByName("DC").AsInteger;
                pm.MaxDC = Query.FieldByName("DCMAX").AsInteger;
                pm.MC = Query.FieldByName("MC").AsInteger;
                pm.SC = Query.FieldByName("SC").AsInteger;
                pm.Speed = Query.FieldByName("SPEED").AsInteger;
                pm.Hit = Query.FieldByName("HIT").AsInteger;
                pm.WalkSpeed = Query._MAX(200, Query.FieldByName("WALK_SPD").AsInteger);
                pm.WalkStep = Query._MAX(1, Query.FieldByName("WalkStep").AsInteger);
                pm.WalkWait = Query.FieldByName("WalkWait").AsInteger;
                pm.AttackSpeed = Query.FieldByName("ATTACK_SPD").AsInteger;
                if (pm.WalkSpeed < 200)
                {
                    pm.WalkSpeed = 200;
                }
                if (pm.AttackSpeed < 200)
                {
                    pm.AttackSpeed = 200;
                }
                // newly added by sonmg.
                pm.Tame = Query.FieldByName("TAME").AsInteger;
                pm.AntiPush = Query.FieldByName("ANTIPUSH").AsInteger;
                pm.AntiUndead = Query.FieldByName("ANTIUNDEAD").AsInteger;
                pm.SizeRate = Query.FieldByName("SIZERATE").AsInteger;
                pm.AntiStop = Query.FieldByName("ANTISTOP").AsInteger;
                pm.ItemList = null;
                LoadMonItems(pm.Name, ref pm.ItemList);
                svMain.UserEngine.MonDefList.Add(pm);
                result = 1;
                //this.Next;
            }
            this.Close();
            return result;
        }

#endif
#if LOADSQL
        public int LoadMagic()
        {
            int result;
            result = 0;
            SQLSQLLocalDB.gMagicMgr = new TMagicMgr();
            if (SQLSQLLocalDB.gMagicMgr.Load(svMain.UserEngine.DefMagicList, SQLLocalDB.TLoadType.ltSQL, 1))
            {
                result = 1;
            }
                        SQLSQLLocalDB.gMagicMgr.Free();
            return result;
        }

#else
        public int LoadMagic()
        {
            int result;
            int i;
            TDefMagic pm;
            result = 0;
            Query.SQL.Clear();
            Query.SQL.Add("select * from Magic");
            try
            {
                //Query.Open;
            }
            finally
            {
                result = -2;
            }
            for (i = 0; i < Query.RecordCount; i++)
            {
                pm = new TDefMagic();
                pm.MagicId = Query.FieldByName("MagId").AsInteger;
                pm.MagicName = Query.FieldByName("MagName").AsString;
                pm.EffectType = Query.FieldByName("EffectType").AsInteger;
                pm.Effect = Query.FieldByName("Effect").AsInteger;
                pm.Spell = Query.FieldByName("Spell").AsInteger;
                pm.MinPower = Query.FieldByName("Power").AsInteger;
                pm.MaxPower = Query.FieldByName("MaxPower").AsInteger;
                pm.Job = Query.FieldByName("Job").AsInteger;
                pm.NeedLevel[0] = Query.FieldByName("NeedL1").AsInteger;
                pm.NeedLevel[1] = Query.FieldByName("NeedL2").AsInteger;
                pm.NeedLevel[2] = Query.FieldByName("NeedL3").AsInteger;
                pm.NeedLevel[3] = Query.FieldByName("NeedL3").AsInteger;
                pm.MaxTrain[0] = Query.FieldByName("L1Train").AsInteger;
                pm.MaxTrain[1] = Query.FieldByName("L2Train").AsInteger;
                pm.MaxTrain[2] = Query.FieldByName("L3Train").AsInteger;
                pm.MaxTrain[3] = pm.MaxTrain[2];
                // FieldByName('L2Train').AsInteger;
                pm.MaxTrainLevel = 3;
                // /FieldByName('TrainLevel').AsInteger;
                pm.DelayTime = Query.FieldByName("Delay").AsInteger * 10;
                pm.DefSpell = Query.FieldByName("DefSpell").AsInteger;
                pm.DefMinPower = Query.FieldByName("DefPower").AsInteger;
                pm.DefMaxPower = Query.FieldByName("DefMaxPower").AsInteger;
                pm.Desc = Query.FieldByName("Descr").AsString;
                svMain.UserEngine.DefMagicList.Add(pm);
                result = 1;
                //this.Next;
            }
            this.Close();
            return result;
        }

#endif
        public int LoadZenLists()
        {
            string str;
            string data = string.Empty;
            TZenInfo pz;
            StringList strlist;
            int ztime;
            int result = -1;
            string flname = svMain.EnvirDir + LocalDB.ZENFILE;
            if (File.Exists(flname))
            {
                strlist = new StringList();
                strlist.LoadFromFile(flname);
                for (var i = 0; i < strlist.Count; i++)
                {
                    str = strlist[i];
                    if (str == "")
                    {
                        continue;
                    }
                    if (str[0] == ';')
                    {
                        continue;
                    }
                    pz = new TZenInfo();
                    str = HUtil32.GetValidStr3(str, ref data, new string[] { " ", "\09" });
                    pz.MapName = data.ToUpper();
                    str = HUtil32.GetValidStr3(str, ref data, new string[] { " ", "\09" });
                    pz.X = HUtil32.Str_ToInt(data, 0);
                    str = HUtil32.GetValidStr3(str, ref data, new string[] { " ", "\09" });
                    pz.Y = HUtil32.Str_ToInt(data, 0);
                    str = HUtil32.GetValidStrCap(str, ref data, new string[] { " ", "\09" });
                    if (data != "")
                    {
                        if (data[0] == '\"')
                        {
                            HUtil32.ArrestStringEx(data, "\"", "\"", ref data);
                        }
                    }
                    pz.MonName = data;
                    str = HUtil32.GetValidStr3(str, ref data, new string[] { " ", "\09" });
                    pz.Area = HUtil32.Str_ToInt(data, 0);
                    str = HUtil32.GetValidStr3(str, ref data, new string[] { " ", "\09" });
                    pz.Count = HUtil32.Str_ToInt(data, 0);
                    str = HUtil32.GetValidStr3(str, ref data, new string[] { " ", "\09" });
                    ztime = -1;
                    if (data != "")
                    {
                        ztime = HUtil32.Str_ToInt(data, -1);
                    }
                    pz.MonZenTime = (long)ztime * 60 * 1000;
                    str = HUtil32.GetValidStr3(str, ref data, new string[] { " ", "\09" });
                    pz.SmallZenRate = HUtil32.Str_ToInt(data, 0);
                    str = HUtil32.GetValidStr3(str, ref data, new string[] { " ", "\09" });
                    pz.TX = HUtil32.Str_ToInt(data, 0);
                    str = HUtil32.GetValidStr3(str, ref data, new string[] { " ", "\09" });
                    pz.TY = HUtil32.Str_ToInt(data, 0);
                    str = HUtil32.GetValidStr3(str, ref data, new string[] { " ", "\09" });
                    pz.ZenShoutType = HUtil32.Str_ToInt(data, 0);
                    str = HUtil32.GetValidStr3(str, ref data, new string[] { " ", "\09" });
                    pz.ZenShoutMsg = HUtil32.Str_ToInt(data, 0);
                    if ((pz.MapName != "") && (pz.MonName != "") && (pz.MonZenTime != 0))
                    {
                        if (svMain.GrobalEnvir.ServerGetEnvir(svMain.ServerIndex, pz.MapName) != null)
                        {
                            pz.StartTime = 0;
                            pz.Mons = new ArrayList();
                            svMain.UserEngine.MonList.Add(pz);
                        }
                    }
                }
                pz = new TZenInfo();
                pz.MapName = "";
                pz.MonName = "";
                pz.Mons = new ArrayList();
                svMain.UserEngine.MonList.Add(pz);
                strlist.Free();
                result = 1;
            }
            return result;
        }

        public int LoadGenMsgLists()
        {
            int result;
            int i;
            string str;
            string flname;
            StringList strlist;
            result = -1;
            flname = svMain.EnvirDir +LocalDB.ZENMSGFILE;
            if (File.Exists(flname))
            {
                strlist = new StringList();
                strlist.LoadFromFile(flname);
                for (i = 0; i < strlist.Count; i++)
                {
                    str = strlist[i];
                    if (str == "")
                    {
                        continue;
                    }
                    if (str[0] == ';')
                    {
                        continue;
                    }
                    svMain.UserEngine.GenMsgList.Add(str);
                }
                strlist.Free();
                result = 1;
            }
            return result;
        }

        public TNormNpc LoadMapFiles_GetMapNpc(string mqfile)
        {
            TNormNpc result;
            TMerchant npc;
            npc = new TMerchant();
            npc.MapName = "0";
            npc.CX = 0;
            npc.CY = 0;
            npc.UserName = mqfile;
            npc.NpcFace = 0;
            npc.Appearance = 0;
            npc.DefineDirectory =LocalDB.MAPQUESTDIR;
            npc.BoInvisible = true;
            npc.BoUseMapFileName = false;
            svMain.UserEngine.NpcList.Add(npc);
            result = npc;
            return result;
        }

        // 
        // 2003/01/14 Mine2 眠啊
        public int LoadMapFiles()
        {
            int result;
            int i;
            int needlevel;
            int xx;
            int yy;
            int ex;
            int ey;
            int svindex;
            int setnum;
            int setval;
            int autoattack;
            int GuildAgit;
            string str = string.Empty;
            string data = string.Empty;
            string tmp = string.Empty;
            string flname = string.Empty;
            string map = string.Empty;
            string newmap = string.Empty;
            string entermap = string.Empty;
            string maptitle = string.Empty;
            string servernum = string.Empty;
            string backmap = string.Empty;
            // minemap,
            bool law;
            bool fight;
            bool fight2;
            bool fight3;
            bool fight4;
            bool dark;
            bool dawn;
            bool sunny;
            bool quiz;
            bool norecon;
            bool needhole;
            bool norecall;
            bool norandommove;
            bool NoEscapeMove;
            bool NoTeleportMove;
            bool nodrug;
            bool nopositionmove;
            bool nochat;
            bool nogroup;
            bool nothrowitem;
            bool nodropitem;
            bool stall;
            bool nodeal;
            int minemap;
            StringList strlist;
            TNormNpc npc;
            string frmcap;
            TEnvirnoment TempEnvir;
            int j;
            int FirstGuildAgit;
            int LastGuildAgit;
            bool boGuildAgitGate;
            frmcap = svMain.FrmMain.Text;
            FirstGuildAgit = -1;
            LastGuildAgit = -1;
            result = -1;
            flname = svMain.EnvirDir +LocalDB.MAPDEFFILE;
            if (!File.Exists(flname))
            {
                return result;
            }
            strlist = new StringList();
            strlist.LoadFromFile(flname);
            if (strlist.Count < 1)
            {
                strlist.Free();
                return result;
            }
            result = 1;
            // 甘阑 刚历 眠啊窃
            for (i = 0; i < strlist.Count; i++)
            {
                str = strlist[i];
                if (str != "")
                {
                    if (str[0] == '[')
                    {
                        needlevel = 1;
                        map = "";
                        law = false;
                        str = HUtil32.ArrestStringEx(str, "[", "]", ref map);
                        maptitle = HUtil32.GetValidStrCap(map, ref map, new string[] { " ", ",", "\09" });
                        newmap = HUtil32.GetValidStr3(map, ref map, new string[] { "~", "|", "\09" }).Trim();
                        // 获取重复利用地图  支持| 与～
                        if (maptitle != "")
                        {
                            if (maptitle[0] == '\"')
                            {
                                HUtil32.ArrestStringEx(maptitle, "\"", "\"", ref maptitle);
                            }
                        }
                        servernum = HUtil32.GetValidStr3(maptitle, ref maptitle, new string[] { " ", ",", "\09" }).Trim();
                        svindex = HUtil32.Str_ToInt(servernum, 0);
                        if (map != "")
                        {
                            law = false;
                            fight = false;
                            fight2 = false;
                            fight3 = false;
                            fight4 = false;
                            dark = false;
                            dawn = false;
                            sunny = false;
                            quiz = false;
                            norecon = false;
                            backmap = "";
                            needlevel = 1;
                            needhole = false;
                            norecall = false;
                            norandommove = false;
                            NoEscapeMove = false;
                            NoTeleportMove = false;
                            nodrug = false;
                            minemap = 0;
                            nopositionmove = false;
                            npc = null;
                            setnum = -1;
                            setval = -1;
                            autoattack = -1;
                            GuildAgit = -1;
                            nochat = false;
                            nogroup = false;
                            nothrowitem = false;
                            nodropitem = false;
                            stall = false;
                            nodeal = false;
                            while (true)
                            {
                                if (str == "")
                                {
                                    break;
                                }
                                str = HUtil32.GetValidStr3(str, ref data, new string[] { " ", ",", "\09" });
                                if (data != "")
                                {
                                    if (data.ToLower().CompareTo("SAFE".ToLower()) == 0)
                                    {
                                        law = true;
                                    }
                                    if (data.ToLower().CompareTo("DARK".ToLower()) == 0)
                                    {
                                        dark = true;
                                    }
                                    if (data.ToLower().CompareTo("DAWN".ToLower()) == 0)
                                    {
                                        dawn = true;
                                    }
                                    // 货寒眠啊
                                    if (data.ToLower().CompareTo("FIGHT".ToLower()) == 0)
                                    {
                                        fight = true;
                                    }
                                    if (data.ToLower().CompareTo("FIGHT2".ToLower()) == 0)
                                    {
                                        fight2 = true;
                                    }
                                    if (data.ToLower().CompareTo("FIGHT3".ToLower()) == 0)
                                    {
                                        fight3 = true;
                                    }
                                    if (data.ToLower().CompareTo("FIGHT4".ToLower()) == 0)
                                    {
                                        fight4 = true;
                                    }
                                    if (data.ToLower().CompareTo("DAY".ToLower()) == 0)
                                    {
                                        sunny = true;
                                    }
                                    if (data.ToLower().CompareTo("QUIZ".ToLower()) == 0)
                                    {
                                        quiz = true;
                                    }
                                    if (HUtil32.CompareLStr(data, "NORECONNECT", 8))
                                    {
                                        norecon = true;
                                        HUtil32.ArrestStringEx(data, "(", ")", ref backmap);
                                        if (backmap == "")
                                        {
                                            result = -11;
                                        }
                                    }
                                    if (HUtil32.CompareLStr(data, "CHECKQUEST", 10))
                                    {
                                        // 茄 俺 炼扒父
                                        HUtil32.ArrestStringEx(data, "(", ")", ref tmp);
                                        npc = LoadMapFiles_GetMapNpc(tmp);
                                    }
                                    if (HUtil32.CompareLStr(data, "NEEDSET_ON", 10))
                                    {
                                        setval = 1;
                                        HUtil32.ArrestStringEx(data, "(", ")", ref tmp);
                                        setnum = HUtil32.Str_ToInt(tmp, -1);
                                    }
                                    if (HUtil32.CompareLStr(data, "NEEDSET_OFF", 10))
                                    {
                                        setval = 0;
                                        HUtil32.ArrestStringEx(data, "(", ")", ref tmp);
                                        setnum = HUtil32.Str_ToInt(tmp, -1);
                                    }
                                    if (HUtil32.CompareLStr(data, "NEEDHOLE", 7))
                                    {
                                        needhole = true;
                                    }
                                    if (HUtil32.CompareLStr(data, "NORECALL", 7))
                                    {
                                        norecall = true;
                                    }
                                    if (HUtil32.CompareLStr(data, "NORANDOMMOVE", 11))
                                    {
                                        norandommove = true;
                                    }
                                    if (HUtil32.CompareLStr(data, "NOESCAPEMOVE", 12))
                                    {
                                        NoEscapeMove = true;
                                    }
                                    // sonmg
                                    if (HUtil32.CompareLStr(data, "NOTELEPORTMOVE", 14))
                                    {
                                        NoTeleportMove = true;
                                    }
                                    // sonmg
                                    if (HUtil32.CompareLStr(data, "NODRUG", 6))
                                    {
                                        nodrug = true;
                                    }
                                    if (HUtil32.CompareLStr(data, "MINE", 4))
                                    {
                                        minemap = 1;
                                    }
                                    if (HUtil32.CompareLStr(data, "MINE2", 5))
                                    {
                                        minemap = 2;
                                    }
                                    if (HUtil32.CompareLStr(data, "MINE3", 5))
                                    {
                                        minemap = 3;
                                    }
                                    if (HUtil32.CompareLStr(data, "NOPOSITIONMOVE", 13))
                                    {
                                        nopositionmove = true;
                                    }
                                    if (HUtil32.CompareLStr(data, "THUNDER", 7))
                                    {
                                        autoattack = 1;
                                    }
                                    if (HUtil32.CompareLStr(data, "FIRE", 4))
                                    {
                                        autoattack = 2;
                                    }
                                    if (HUtil32.CompareLStr(data, "NOMAPXY", 7))
                                    {
                                        autoattack = 3;
                                    }
                                    // (sonmg 2005/03/14)
                                    // 巩颇厘盔(sonmg)
                                    if (HUtil32.CompareLStr(data, "GUILDAGIT", 9))
                                    {
                                        HUtil32.ArrestStringEx(data, "(", ")", ref tmp);
                                        GuildAgit = HUtil32.Str_ToInt(tmp, -1);
                                        // 贸澜 厘盔 锅龋
                                        if (FirstGuildAgit < 0)
                                        {
                                            FirstGuildAgit = GuildAgit;
                                        }
                                        else if (FirstGuildAgit > GuildAgit)
                                        {
                                            FirstGuildAgit = GuildAgit;
                                        }
                                        // 付瘤阜 厘盔 锅龋
                                        if (LastGuildAgit < 0)
                                        {
                                            LastGuildAgit = GuildAgit;
                                        }
                                        else if (LastGuildAgit < GuildAgit)
                                        {
                                            LastGuildAgit = GuildAgit;
                                        }
                                        svMain.GuildAgitStartNumber = FirstGuildAgit;
                                        svMain.GuildAgitMaxNumber = LastGuildAgit;
                                    }
                                    // sonmg 2004/10/12
                                    if (HUtil32.CompareLStr(data, "NOCHAT", 6))
                                    {
                                        nochat = true;
                                    }
                                    if (HUtil32.CompareLStr(data, "NOGROUP", 7))
                                    {
                                        nogroup = true;
                                    }
                                    // sonmg 2005/03/14
                                    if (HUtil32.CompareLStr(data, "NOTHROWITEM", 11))
                                    {
                                        nothrowitem = true;
                                    }
                                    if (HUtil32.CompareLStr(data, "NODROPITEM", 10))
                                    {
                                        nodropitem = true;
                                    }
                                    if (HUtil32.CompareLStr(data, "STALL", 5))
                                    {
                                        stall = true;
                                    }
                                    if (HUtil32.CompareLStr(data, "NODEAL", 5))
                                    {
                                        nodeal = true;
                                    }
                                    if (data[0] == 'L')
                                    {
                                        needlevel = HUtil32.Str_ToInt(data.Substring(2 - 1, data.Length - 1), 1);
                                    }
                                }
                                else
                                {
                                    break;
                                }
                            }
                            if (GuildAgit > -1)
                            {
                                TempEnvir = svMain.GrobalEnvir.AddEnvir(map + GuildAgit.ToString().ToUpper(), newmap, maptitle + GuildAgit.ToString(), svindex, needlevel, law, fight, fight2, fight3, fight4, dark, dawn, sunny, quiz, norecon, needhole, norecall, norandommove, NoEscapeMove, NoTeleportMove, nodrug, minemap, nopositionmove, backmap, npc, setnum, setval, autoattack, GuildAgit, nochat, nogroup, nothrowitem, nodropitem, stall, nodeal);
                                if (TempEnvir == null)
                                {
                                    result = -10;
                                }
                                else
                                {
                                    switch (TempEnvir.AutoAttack)
                                    {
                                        case 1:
                                        case 2:
                                            svMain.gFireDragon.SetAutoAttackMap(TempEnvir, TempEnvir.AutoAttack);
                                            break;
                                    }
                                }
                            }
                            else
                            {
                                TempEnvir = svMain.GrobalEnvir.AddEnvir(map.ToUpper(), newmap, maptitle, svindex, needlevel, law, fight, fight2, fight3, fight4, dark, dawn, sunny, quiz, norecon, needhole, norecall, norandommove, NoEscapeMove, NoTeleportMove, nodrug, minemap, nopositionmove, backmap, npc, setnum, setval, autoattack, GuildAgit, nochat, nogroup, nothrowitem, nodropitem, stall, nodeal);
                                if (TempEnvir == null)
                                {
                                    result = -10;
                                }
                                else
                                {
                                    switch (TempEnvir.AutoAttack)
                                    {
                                        case 1:
                                        case 2:
                                            // 肋 父甸绢脸澜
                                            // 侩矫胶袍俊 磊悼傍拜汲沥阑 窃.
                                            svMain.gFireDragon.SetAutoAttackMap(TempEnvir, TempEnvir.AutoAttack);
                                            break;
                                    }
                                }
                            }
                        }
                        svMain.FrmMain.Text = "Map loading.. " + (i + 1).ToString() + "/" + strlist.Count.ToString();
                        svMain.FrmMain.RefreshForm();
                    }
                }
            }
            svMain.FrmMain.Text = frmcap;
            // 涝备甫 眠啊窃
            for (i = 0; i < strlist.Count; i++)
            {
                boGuildAgitGate = false;
                // 临付促 檬扁拳
                str = strlist[i];
                if (str != "")
                {
                    if ((str[0] == '[') || (str[0] == ';'))
                    {
                        continue;
                    }
                    str = HUtil32.GetValidStr3(str, ref data, new string[] { " ", ",", "\09" });
                    // 巩颇 厘盔 甘 霸捞飘捞搁 Flag甫 眉农窍绊 茄 窜绢 歹 佬绰促.
                    if (data.CompareTo("GUILDAGIT") == 0)
                    {
                        boGuildAgitGate = true;
                        str = HUtil32.GetValidStr3(str, ref data, new string[] { " ", ",", "\09" });
                    }
                    map = data;
                    str = HUtil32.GetValidStr3(str, ref data, new string[] { " ", ",", "\09" });
                    xx = HUtil32.Str_ToInt(data, 0);
                    str = HUtil32.GetValidStr3(str, ref data, new string[] { " ", ",", "\09" });
                    yy = HUtil32.Str_ToInt(data, 0);
                    str = HUtil32.GetValidStr3(str, ref data, new string[] { " ", ",", "-", ">", "\09" });
                    entermap = data;
                    str = HUtil32.GetValidStr3(str, ref data, new string[] { " ", ",", "\09" });
                    ex = HUtil32.Str_ToInt(data, 0);
                    str = HUtil32.GetValidStr3(str, ref data, new string[] { " ", ",", ";", "\09" });
                    ey = HUtil32.Str_ToInt(data, 0);
                    if (boGuildAgitGate)
                    {
                        for (j = FirstGuildAgit; j <= LastGuildAgit; j++)
                        {
                            if (false == svMain.GrobalEnvir.AddGate(map + j.ToString().ToUpper(), xx, yy, entermap + j.ToString(), ex, ey))
                            {
                                svMain.MainOutMessage("NOT ADD GATE :[" + (i + 1).ToString() + "]" + strlist[i]);
                            }
                        }
                    }
                    else
                    {
                        if (false == svMain.GrobalEnvir.AddGate(map.ToUpper(), xx, yy, entermap, ex, ey))
                        {
                            svMain.MainOutMessage("NOT ADD GATE :[" + (i + 1).ToString() + "]" + strlist[i]);
                        }
                    }
                }
            }
            strlist.Free();
            return result;
        }

        public int LoadAdminFiles()
        {
            int result = 0;
            string flname = svMain.EnvirDir + LocalDB.ADMINDEFFILE;
            svMain.UserEngine.AdminList.Clear();
            if (File.Exists(flname))
            {
                StringList strlist = new StringList();
                strlist.LoadFromFile(flname);
                for (var i = 0; i < strlist.Count; i++)
                {
                    string str = strlist[i];
                    if (str != "")
                    {
                        if (str[0] != ';')
                        {
                            string temp = string.Empty;
                            if (str[0] == '*')
                            {
                                str = HUtil32.GetValidStrCap(str, ref temp, new string[] { " ", "\09" });
                                svMain.UserEngine.AdminList.Add(str.Trim(), Grobal2.UD_ADMIN as Object);
                            }
                            else
                            {
                                if (str[0] == '1')
                                {
                                    str = HUtil32.GetValidStrCap(str, ref temp, new string[] { " ", "\09" });
                                    svMain.UserEngine.AdminList.Add(str.Trim(), Grobal2.UD_SYSOP as Object);
                                }
                                else if (str[0] == '2')
                                {
                                    str = HUtil32.GetValidStrCap(str, ref temp, new string[] { " ", "\09" });
                                    svMain.UserEngine.AdminList.Add(str.Trim(), Grobal2.UD_OBSERVER as Object);
                                }
                            }
                        }
                    }
                }
                strlist.Free();
                result = 1;
            }
            return result;
        }

        public int LoadChatLogFiles()
        {
            string flname = svMain.EnvirDir + LocalDB.CHATLOGFILE;
            svMain.UserEngine.ChatLogList.Clear();
            if (File.Exists(flname))
            {
                StringList strlist = new StringList();
                strlist.LoadFromFile(flname);
                for (var i = 0; i < strlist.Count; i++)
                {
                    string str = strlist[i];
                    if (str != "")
                    {
                        if (str[1] != ';')
                        {
                            str = strlist[i];
                            svMain.UserEngine.ChatLogList.Add(str.Trim());
                        }
                    }
                }
                strlist.Free();
            }
            return 1;
        }

        public int SaveChatLogFiles()
        {
            string flname = svMain.EnvirDir + LocalDB.CHATLOGFILE;
            svMain.UserEngine.ChatLogList.SaveToFile(flname);
            return 1;
        }

        public int LoadMerchants()
        {
            int result;
            int i;
            string str = string.Empty;
            string marketname = string.Empty;
            string map = string.Empty;
            string xstr = string.Empty;
            string ystr = string.Empty;
            string seller = string.Empty;
            string facestr = string.Empty;
            string apprstr = string.Empty;
            string castlestr = string.Empty;
            StringList strlist;
            TMerchant merchant;
            string flname = svMain.EnvirDir + LocalDB.MERCHANTFILE;
            if (File.Exists(flname))
            {
                strlist = new StringList();
                strlist.LoadFromFile(flname);
                for (i = 0; i < strlist.Count; i++)
                {
                    str = strlist[i].Trim();
                    if (str == "")
                    {
                        continue;
                    }
                    if (str[0] == ';')
                    {
                        continue;
                    }
                    str = HUtil32.GetValidStr3(str, ref marketname, new string[] { " ", "\09" });
                    str = HUtil32.GetValidStr3(str, ref map, new string[] { " ", "\09" });
                    str = HUtil32.GetValidStr3(str, ref xstr, new string[] { " ", "\09" });
                    str = HUtil32.GetValidStr3(str, ref ystr, new string[] { " ", "\09" });
                    str = HUtil32.GetValidStrCap(str, ref seller, new string[] { " ", "\09" });
                    if (seller != "")
                    {
                        if (seller[0] == '\"')
                        {
                            HUtil32.ArrestStringEx(seller, "\"", "\"", ref seller);
                        }
                    }
                    str = HUtil32.GetValidStr3(str, ref facestr, new string[] { " ", "\09" });
                    str = HUtil32.GetValidStr3(str, ref apprstr, new string[] { " ", "\09" });
                    str = HUtil32.GetValidStr3(str, ref castlestr, new string[] { " ", "\09" });
                    if ((marketname != "") && (map != "") && (apprstr != ""))
                    {
                        merchant = new TMerchant();
                        merchant.MarketName = marketname;
                        merchant.MapName = map.ToUpper();
                        merchant.CX = (short)HUtil32.Str_ToInt(xstr, 0);
                        merchant.CY = (short)HUtil32.Str_ToInt(ystr, 0);
                        merchant.UserName = seller;
                        merchant.NpcFace = (byte)HUtil32.Str_ToInt(facestr, 0);
                        merchant.Appearance = (ushort)HUtil32.Str_ToInt(apprstr, 0);
                        if (HUtil32.Str_ToInt(castlestr, 0) != 0)
                        {
                            merchant.BoCastleManage = true;
                        }
                        svMain.UserEngine.MerchantList.Add(merchant);
                    }
                }
                strlist.Free();
            }
            result = 1;
            return result;
        }

        public int ReloadMerchants()
        {
            int result;
            int i;
            int k;
            int xx;
            int yy;
            string str = string.Empty;
            string marketname = string.Empty;
            string map = string.Empty;
            string xstr = string.Empty;
            string ystr = string.Empty;
            string seller = string.Empty;
            string facestr = string.Empty;
            string apprstr = string.Empty;
            string castlestr = string.Empty;
            StringList strlist;
            TMerchant merchant;
            bool newone;
            string flname = svMain.EnvirDir + LocalDB.MERCHANTFILE;
            if (File.Exists(flname))
            {
                for (i = 0; i < svMain.UserEngine.MerchantList.Count; i++)
                {
                    merchant = (TMerchant)svMain.UserEngine.MerchantList[i];
                    merchant.NpcFace = 255;
                }
                strlist = new StringList();
                strlist.LoadFromFile(flname);
                for (i = 0; i < strlist.Count; i++)
                {
                    str = strlist[i].Trim();
                    if (str == "")
                    {
                        continue;
                    }
                    if (str[0] == ';')
                    {
                        continue;
                    }
                    str = HUtil32.GetValidStr3(str, ref marketname, new string[] { " ", "\09" });
                    str = HUtil32.GetValidStr3(str, ref map, new string[] { " ", "\09" });
                    str = HUtil32.GetValidStr3(str, ref xstr, new string[] { " ", "\09" });
                    str = HUtil32.GetValidStr3(str, ref ystr, new string[] { " ", "\09" });
                    str = HUtil32.GetValidStrCap(str, ref seller, new string[] { " ", "\09" });
                    if (seller != "")
                    {
                        if (seller[0] == '\"')
                        {
                            HUtil32.ArrestStringEx(seller, "\"", "\"", ref seller);
                        }
                    }
                    str = HUtil32.GetValidStr3(str, ref facestr, new string[] { " ", "\09" });
                    str = HUtil32.GetValidStr3(str, ref apprstr, new string[] { " ", "\09" });
                    str = HUtil32.GetValidStr3(str, ref castlestr, new string[] { " ", "\09" });
                    if ((marketname != "") && (map != "") && (apprstr != ""))
                    {
                        xx = HUtil32.Str_ToInt(xstr, 0);
                        yy = HUtil32.Str_ToInt(ystr, 0);
                        map = map.ToUpper();
                        newone = true;
                        for (k = 0; k < svMain.UserEngine.MerchantList.Count; k++)
                        {
                            merchant = (TMerchant)svMain.UserEngine.MerchantList[k];
                            if ((map == merchant.MapName) && (xx == merchant.CX) && (yy == merchant.CY))
                            {
                                newone = false;
                                merchant.MarketName = marketname;
                                merchant.UserName = seller;
                                merchant.NpcFace = (byte)HUtil32.Str_ToInt(facestr, 0);
                                merchant.Appearance = (ushort)HUtil32.Str_ToInt(apprstr, 0);
                                if (HUtil32.Str_ToInt(castlestr, 0) != 0)
                                {
                                    merchant.BoCastleManage = true;
                                }
                                break;
                            }
                        }
                        if (newone)
                        {
                            merchant = new TMerchant();
                            merchant.MapName = map;
                            merchant.PEnvir = svMain.GrobalEnvir.GetEnvir(merchant.MapName);
                            if (merchant.PEnvir != null)
                            {
                                merchant.MarketName = marketname;
                                merchant.CX = (short)xx;
                                merchant.CY = (short)yy;
                                merchant.UserName = seller;
                                merchant.NpcFace = (byte)HUtil32.Str_ToInt(facestr, 0);
                                merchant.Appearance = (ushort)HUtil32.Str_ToInt(apprstr, 0);
                                if (HUtil32.Str_ToInt(castlestr, 0) != 0)
                                {
                                    merchant.BoCastleManage = true;
                                }
                                svMain.UserEngine.MerchantList.Add(merchant);
                                merchant.Initialize();
                            }
                            else
                            {
                                merchant.Free();
                            }
                        }
                    }
                }
                for (i = svMain.UserEngine.MerchantList.Count - 1; i >= 0; i--)
                {
                    merchant = (TMerchant)svMain.UserEngine.MerchantList[i];
                    if (merchant.NpcFace == 255)
                    {
                        merchant.BoGhost = true;
                        svMain.UserEngine.MerchantList.RemoveAt(i);
                    }
                }
                strlist.Free();
            }
            result = 1;
            return result;
        }

        public int LoadNpcs()
        {
            int result;
            StringList strlist;
            int i;
            int race;
            string str = string.Empty;
            string flname = string.Empty;
            string nname = string.Empty;
            string racestr = string.Empty;
            string map = string.Empty;
            string xstr = string.Empty;
            string ystr = string.Empty;
            string facestr = string.Empty;
            string body= string.Empty;
            TNormNpc npc;
            result = -1;
            flname = svMain.EnvirDir +LocalDB.NPCLISTFILE;
            if (File.Exists(flname))
            {
                strlist = new StringList();
                strlist.LoadFromFile(flname);
                for (i = 0; i < strlist.Count; i++)
                {
                    str = strlist[i].Trim();
                    if (str == "")
                    {
                        continue;
                    }
                    if (str[0] == ';')
                    {
                        continue;
                    }
                    str = HUtil32.GetValidStrCap(str, ref nname, new string[] { " ", "\09" });
                    if (nname != "")
                    {
                        if (nname[0] == '\"')
                        {
                            HUtil32.ArrestStringEx(nname, "\"", "\"", ref nname);
                        }
                    }
                    str = HUtil32.GetValidStr3(str,ref racestr, new string[] { " ", "\09" });
                    str = HUtil32.GetValidStr3(str,ref map, new string[] { " ", "\09" });
                    str = HUtil32.GetValidStr3(str,ref xstr, new string[] { " ", "\09" });
                    str = HUtil32.GetValidStr3(str,ref ystr, new string[] { " ", "\09" });
                    str = HUtil32.GetValidStr3(str,ref facestr, new string[] { " ", "\09" });
                    str = HUtil32.GetValidStr3(str, ref body, new string[] { " ", "\09" });
                    if ((nname != "") && (map != "") && (body != ""))
                    {
                        race = HUtil32.Str_ToInt(racestr, 0);
                        npc = null;
                        switch (race)
                        {
                            case 0:
                                npc = new TMerchant();
                                break;
                            case 1:
                                npc = new TGuildOfficial();
                                break;
                            case 2:
                                npc = new TCastleManager();
                                break;
                            case 3:
                                npc = new THiddenNpc();
                                break;
                        }
                        if (npc != null)
                        {
                            npc.MapName = map.ToUpper();
                            npc.CX = (short)HUtil32.Str_ToInt(xstr, 0);
                            npc.CY = (short)HUtil32.Str_ToInt(ystr, 0);
                            npc.UserName = nname;
                            npc.NpcFace = (byte)HUtil32.Str_ToInt(facestr, 0);
                            npc.Appearance = (ushort)HUtil32.Str_ToInt(body, 0);
                            svMain.UserEngine.NpcList.Add(npc);
                        }
                    }
                }
                strlist.Free();
            }
            result = 1;
            return result;
        }

        public int ReloadNpcs()
        {
            int result;
            StringList strlist;
            int i;
            int k;
            int race;
            int xx;
            int yy;
            string str = string.Empty;
            string nname = string.Empty;
            string racestr = string.Empty;
            string map = string.Empty;
            string xstr = string.Empty;
            string ystr = string.Empty;
            string facestr = string.Empty;
            string body = string.Empty;
            TNormNpc npc;
            bool newone;
            result = -1;
            string flname = svMain.EnvirDir + LocalDB.NPCLISTFILE;
            if (File.Exists(flname))
            {
                for (i = 0; i < svMain.UserEngine.NpcList.Count; i++)
                {
                    npc = (TNormNpc)svMain.UserEngine.NpcList[i];
                    npc.NpcFace = 255;
                }
                strlist = new StringList();
                strlist.LoadFromFile(flname);
                for (i = 0; i < strlist.Count; i++)
                {
                    str = strlist[i].Trim();
                    if (str == "")
                    {
                        continue;
                    }
                    if (str[0] == ';')
                    {
                        continue;
                    }
                    str = HUtil32.GetValidStrCap(str, ref nname, new string[] { " ", "\09" });
                    if (nname != "")
                    {
                        if (nname[0] == '\"')
                        {
                            HUtil32.ArrestStringEx(nname, "\"", "\"", ref nname);
                        }
                    }
                    str = HUtil32.GetValidStr3(str,ref racestr, new string[] { " ", "\09" });
                    str = HUtil32.GetValidStr3(str,ref map, new string[] { " ", "\09" });
                    str = HUtil32.GetValidStr3(str,ref xstr, new string[] { " ", "\09" });
                    str = HUtil32.GetValidStr3(str,ref ystr, new string[] { " ", "\09" });
                    str = HUtil32.GetValidStr3(str,ref facestr, new string[] { " ", "\09" });
                    str = HUtil32.GetValidStr3(str,ref body, new string[] { " ", "\09" });
                    if ((nname != "") && (map != "") && (body != ""))
                    {
                        xx = HUtil32.Str_ToInt(xstr, 0);
                        yy = HUtil32.Str_ToInt(ystr, 0);
                        map = map.ToUpper();
                        newone = true;
                        for (k = 0; k < svMain.UserEngine.NpcList.Count; k++)
                        {
                            npc = (TNormNpc)svMain.UserEngine.NpcList[k];
                            if ((map == npc.MapName) && (xx == npc.CX) && (yy == npc.CY))
                            {
                                newone = false;
                                npc.UserName = nname;
                                npc.NpcFace = (byte)HUtil32.Str_ToInt(facestr, 0);
                                npc.Appearance = (ushort)HUtil32.Str_ToInt(body, 0);
                                break;
                            }
                        }
                        if (newone)
                        {
                            race = HUtil32.Str_ToInt(racestr, 0);
                            npc = null;
                            switch (race)
                            {
                                case 0:
                                    npc = new TMerchant();
                                    break;
                                case 1:
                                    npc = new TGuildOfficial();
                                    break;
                                case 2:
                                    npc = new TCastleManager();
                                    break;
                                case 3:
                                    npc = new THiddenNpc();
                                    break;
                            }
                            if (npc != null)
                            {
                                npc.MapName = map;
                                npc.PEnvir = svMain.GrobalEnvir.GetEnvir(npc.MapName);
                                if (npc.PEnvir != null)
                                {
                                    npc.CX = (short)xx;
                                    npc.CY = (short)yy;
                                    npc.UserName = nname;
                                    npc.NpcFace = (byte)HUtil32.Str_ToInt(facestr, 0);
                                    npc.Appearance = (ushort)HUtil32.Str_ToInt(body, 0);
                                    svMain.UserEngine.NpcList.Add(npc);
                                    npc.Initialize();
                                }
                                else
                                {
                                    npc.Free();
                                }
                            }
                        }
                    }
                }
                strlist.Free();
                for (i = svMain.UserEngine.NpcList.Count - 1; i >= 0; i--)
                {
                    npc = (TNormNpc)svMain.UserEngine.NpcList[i];
                    if (npc.NpcFace == 255)
                    {
                        npc.BoGhost = true;
                        svMain.UserEngine.NpcList.RemoveAt(i);
                    }
                }
            }
            result = 1;
            return result;
        }

        public int LoadGuards()
        {
            StringList strlist;
            string str = string.Empty;
            string mname = string.Empty;
            string map = string.Empty;
            string xstr = string.Empty;
            string ystr = string.Empty;
            string dirstr = string.Empty;
            TCreature cret;
            int result = -1;
            string flname = svMain.EnvirDir + LocalDB.GUARDLISTFILE;
            if (File.Exists(flname))
            {
                strlist = new StringList();
                strlist.LoadFromFile(flname);
                for (var i = 0; i < strlist.Count; i++)
                {
                    str = strlist[i];
                    if (str == "")
                    {
                        continue;
                    }
                    if (str[0] == ';')
                    {
                        continue;
                    }
                    str = HUtil32.GetValidStrCap(str, ref mname, new string[] { " " });
                    if (mname != "")
                    {
                        if (mname[0] == '\"')
                        {
                            HUtil32.ArrestStringEx(mname, "\"", "\"", ref mname);
                        }
                    }
                    str = HUtil32.GetValidStr3(str,ref map, new string[] { " " });
                    str = HUtil32.GetValidStr3(str,ref xstr, new string[] { " ", "," });
                    str = HUtil32.GetValidStr3(str,ref ystr, new string[] { " ", ",", ":" });
                    str = HUtil32.GetValidStr3(str,ref dirstr, new string[] { " ", ":" });
                    if ((mname != "") && (map != "") && (dirstr != ""))
                    {
                        cret = svMain.UserEngine.AddCreatureSysop(map, HUtil32.Str_ToInt(xstr, 0), HUtil32.Str_ToInt(ystr, 0), mname);
                        if (cret != null)
                        {
                            cret.Dir = (byte)HUtil32.Str_ToInt(dirstr, 0);
                        }
                    }
                }
                strlist.Free();
                result = 1;
            }
            return result;
        }

        public int LoadMakeItemList()
        {
            StringList strlist;
            int count;
            string str = string.Empty;
            string itemname = string.Empty;
            string makeitemname = string.Empty;
            ArrayList slist;
            int result = -1;
            svMain.MakeItemList.Clear();
            svMain.MakeItemIndexList.Clear();
            var flname = svMain.EnvirDir + LocalDB.MAKEITEMFILE;
            if (File.Exists(flname))
            {
                strlist = new StringList();
                strlist.LoadFromFile(flname);
                slist = null;
                for (var i = 0; i < strlist.Count; i++)
                {
                    str = strlist[i].Trim();
                    if (str != "")
                    {
                        if (str[0] == ';')
                        {
                            continue;
                        }
                        if (str[0] == '[')
                        {
                            if (slist != null)
                            {
                                svMain.MakeItemList.Add(makeitemname, slist as Object);
                            }
                            slist = new ArrayList();
                            HUtil32.ArrestStringEx(str, "[", "]", ref makeitemname);
                        }
                        else if (str[0] == '-')
                        {
                            svMain.MakeItemIndexList.Add(svMain.MakeItemList.Count.ToString(), null);
                        }
                        else
                        {
                            if (slist != null)
                            {
                                str = HUtil32.GetValidStr3(str, ref itemname, new string[] { " ", "\09" });
                                if (itemname.Length > 20)
                                {
                                    svMain.MainOutMessage("MAKEITEMLIST NAME > 20" + itemname);
                                }
                                count = HUtil32.Str_ToInt(str.Trim(), 1);
                                slist.Add(itemname, count as Object);
                            }
                        }
                    }
                }
                if (slist != null)
                {
                    svMain.MakeItemList.Add(makeitemname, slist as Object);
                }
                strlist.Free();
                result = 1;
            }
            return result;
        }

        public int LoadStartPoints()
        {
            string str;
            string smap = string.Empty;
            string xstr = string.Empty;
            string ystr = string.Empty;
            StringList strlist;
            int result = 0;
            string flname = svMain.EnvirDir + LocalDB.STARTPOINTFILE;
            if (File.Exists(flname))
            {
                strlist = new StringList();
                strlist.LoadFromFile(flname);
                for (var i = 0; i < strlist.Count; i++)
                {
                    str = strlist[i].Trim();
                    if (str != "")
                    {
                        str = HUtil32.GetValidStr3(str, ref smap, new string[] { " ", "\09" });
                        str = HUtil32.GetValidStr3(str, ref xstr, new string[] { " ", "\09" });
                        str = HUtil32.GetValidStr3(str, ref ystr, new string[] { " ", "\09" });
                        if ((smap != "") && (xstr != "") && (ystr != ""))
                        {
                            svMain.StartPoints.Add(smap, HUtil32.MakeLong(HUtil32.Str_ToInt(xstr, 0), HUtil32.Str_ToInt(ystr, 0)) as Object);
                            result = 1;
                        }
                    }
                }
                strlist.Free();
            }
            return result;
        }

        public int LoadSafePoints()
        {
            string str;
            string smap = string.Empty;
            string xstr = string.Empty;
            string ystr = string.Empty;
            StringList strlist;
            int result = 0;
            string flname = svMain.EnvirDir + LocalDB.SAFEPOINTFILE;
            if (File.Exists(flname))
            {
                strlist = new StringList();
                strlist.LoadFromFile(flname);
                for (var i = 0; i < strlist.Count; i++)
                {
                    str = strlist[i].Trim();
                    if (str != "")
                    {
                        str = HUtil32.GetValidStr3(str, ref smap, new string[] { " ", "\09" });
                        str = HUtil32.GetValidStr3(str, ref xstr, new string[] { " ", "\09" });
                        str = HUtil32.GetValidStr3(str, ref ystr, new string[] { " ", "\09" });
                        if ((smap != "") && (xstr != "") && (ystr != ""))
                        {
                            svMain.SafePoints.Add(new TSafePoint()
                            {
                                mapName = smap,
                                nX = (short)HUtil32.Str_ToInt(xstr, 0),
                                nY = (short)HUtil32.Str_ToInt(ystr, 0)
                            });
                            result = 1;
                        }
                    }
                }
                strlist.Free();
            }
            return result;
        }

        public int LoadDecoItemList()
        {
            int i;
            string str = string.Empty;
            string Num = string.Empty;
            string Name = string.Empty;
            string Kind = string.Empty;
            string Price = string.Empty;
            StringList strlist;
            int result = -1;
            var flname = svMain.EnvirDir + LocalDB.DECOITEMFILE;
            if (File.Exists(flname))
            {
                strlist = new StringList();
                strlist.LoadFromFile(flname);
                for (i = 0; i < strlist.Count; i++)
                {
                    str = strlist[i].Trim();
                    if (str != "")
                    {
                        if (str[0] == ';')
                        {
                            continue;
                        }
                        str = HUtil32.GetValidStr3(str, ref Num, new string[] { " ", "-", "\09" });
                        str = HUtil32.GetValidStr3(str, ref Name, new string[] { " ", "-", "\09" });
                        str = HUtil32.GetValidStr3(str, ref Kind, new string[] { " ", "-", "\09" });
                        str = HUtil32.GetValidStr3(str, ref Price, new string[] { " ", "-", "\09" });
                        if ((Num != "") && (Name != "") && (Kind != ""))
                        {
                            if (Price == "")
                            {
                                Price = ObjBase.DEFAULT_DECOITEM_PRICE.ToString();
                            }
                            svMain.DecoItemList.Add(Name + "/" + Price, HUtil32.MakeLong(HUtil32.Str_ToInt(Num, 0), HUtil32.Str_ToInt(Kind, 0)) as Object);
                            result = 1;
                        }
                    }
                }
                strlist.Free();
            }
            return result;
        }

        public int LoadMiniMapInfos()
        {
            int index;
            string str = string.Empty;
            string smap = string.Empty;
            string idxstr = string.Empty;
            StringList strlist;
            int result = 0;
            string flname = svMain.EnvirDir + LocalDB.MINIMAPFILE;
            if (File.Exists(flname))
            {
                strlist = new StringList();
                strlist.LoadFromFile(flname);
                for (var i = 0; i < strlist.Count; i++)
                {
                    str = strlist[i];
                    if (str != "")
                    {
                        if (str[0] != ';')
                        {
                            str = HUtil32.GetValidStr3(str, ref smap, new string[] { " ", "\09" });
                            str = HUtil32.GetValidStr3(str, ref idxstr, new string[] { " ", "\09" });
                            index = HUtil32.Str_ToInt(idxstr, 0);
                            if (index > 0)
                            {
                                svMain.MiniMapList.Add(smap, index as Object);
                            }
                        }
                    }
                }
                strlist.Free();
            }
            return result;
        }

        public int LoadUnbindItemLists()
        {
            int shape;
            string str;
            string shapestr = string.Empty;
            string itmname = string.Empty;
            StringList strlist;
            int result = 0;
            string flname = svMain.EnvirDir + LocalDB.UNBINDFILE;
            if (File.Exists(flname))
            {
                strlist = new StringList();
                strlist.LoadFromFile(flname);
                for (var i = 0; i < strlist.Count; i++)
                {
                    str = strlist[i];
                    if (str != "")
                    {
                        if (str[0] != ';')
                        {
                            str = HUtil32.GetValidStr3(str, ref shapestr, new string[] { " ", "\09" });
                            str = HUtil32.GetValidStrCap(str, ref itmname, new string[] { " ", "\09" });
                            if (itmname != "")
                            {
                                if (itmname[0] == '\"')
                                {
                                    HUtil32.ArrestStringEx(itmname, "\"", "\"", ref itmname);
                                }
                            }
                            shape = HUtil32.Str_ToInt(shapestr, 0);
                            if (shape > 0)
                            {
                                svMain.UnbindItemList.Add(itmname, shape as Object);
                            }
                            else
                            {
                                result = -i;
                                break;
                            }
                        }
                    }
                }
                strlist.Free();
            }
            return result;
        }

        public int LoadMapQuestInfos()
        {
            int result;
            int i;
            string str = string.Empty;
            string flname = string.Empty;
            string mapstr = string.Empty;
            string constr1 = string.Empty;
            string constr2 = string.Empty;
            string monname = string.Empty;
            string iname = string.Empty;
            string qfile = string.Empty;
            string gflag = string.Empty;
            string str1 = string.Empty;
            int set1;
            int val1;
            StringList strlist;
            TEnvirnoment envir;
            bool enablegroup;
            result = 1;
            flname = svMain.EnvirDir +LocalDB.MAPQUESTFILE;
            if (File.Exists(flname))
            {
                strlist = new StringList();
                strlist.LoadFromFile(flname);
                for (i = 0; i < strlist.Count; i++)
                {
                    str = strlist[i];
                    if (str != "")
                    {
                        if (str[0] != ';')
                        {
                            str = HUtil32.GetValidStr3(str, ref mapstr, new string[] { " ", "\09" });
                            str = HUtil32.GetValidStr3(str, ref constr1, new string[] { " ", "\09" });
                            str = HUtil32.GetValidStr3(str, ref constr2, new string[] { " ", "\09" });
                            str = HUtil32.GetValidStrCap(str, ref monname, new string[] { " ", "\09" });
                            if (monname != "")
                            {
                                if (monname[0] == '\"')
                                {
                                    HUtil32.ArrestStringEx(monname, "\"", "\"", ref monname);
                                }
                            }
                            str = HUtil32.GetValidStrCap(str, ref iname, new string[] { " ", "\09" });
                            if (iname != "")
                            {
                                if (iname[0] == '\"')
                                {
                                    HUtil32.ArrestStringEx(iname, "\"", "\"", ref iname);
                                }
                            }
                            str = HUtil32.GetValidStr3(str, ref qfile, new string[] { " ", "\09" });
                            str = HUtil32.GetValidStr3(str, ref gflag, new string[] { " ", "\09" });
                            if ((mapstr != "") && (monname != "") && (qfile != ""))
                            {
                                envir = svMain.GrobalEnvir.GetEnvir(mapstr);
                                if (envir != null)
                                {
                                    HUtil32.ArrestStringEx(constr1, "[", "]", ref str1);
                                    set1 = HUtil32.Str_ToInt(str1, -1);
                                    val1 = HUtil32.Str_ToInt(constr2, 0);
                                    if (HUtil32.CompareLStr(gflag, "GROUP", 2))
                                    {
                                        enablegroup = true;
                                    }
                                    else
                                    {
                                        enablegroup = false;
                                    }
                                    if (!envir.AddMapQuest(set1, val1, monname, iname, qfile, enablegroup))
                                    {
                                        result = -i;
                                        break;
                                    }
                                }
                                else
                                {
                                    result = -i;
                                    break;
                                }
                            }
                            else
                            {
                                result = -i;
                                break;
                            }
                        }
                    }
                }
                strlist.Free();
            }
            return result;
        }

        public int LoadDefaultNpc()
        {
            int result = 1;
            if (!Directory.Exists(svMain.EnvirDir +LocalDB.MARKETDEFDIR))
            {
                Directory.CreateDirectory(svMain.EnvirDir +LocalDB.MARKETDEFDIR);
            }
            if (File.Exists(svMain.EnvirDir +LocalDB.MARKETDEFDIR +LocalDB.DEFAULTNPCFILE + ".txt"))
            {
                TMerchant npc = new TMerchant();
                npc.MapName = "0";
                npc.CX = 0;
                npc.CY = 0;
                npc.UserName =LocalDB.DEFAULTNPCFILE;
                npc.NpcFace = 0;
                npc.Appearance = 0;
                npc.DefineDirectory =LocalDB.MARKETDEFDIR;
                npc.BoInvisible = true;
                npc.BoUseMapFileName = false;
                svMain.DefaultNpc = npc;
            }
            else
            {
                result = -1;
            }
            return result;
        }

        public string LoadQuestDiary_XXStr(int n)
        {
            string result;
            if (n >= 1000)
            {
                result = n.ToString();
            }
            else if (n >= 100)
            {
                result = "0" + n.ToString();
            }
            else
            {
                result = "00" + n.ToString();
            }
            return result;
        }

        public int LoadQuestDiary()
        {
            string flname;
            string title = String.Empty;
            string str = String.Empty;
            string numstr = String.Empty;
            StringList strlist;
            ArrayList diarylist;
            TQDDinfo pqdd;
            bool bobegin;
            int result = 1;
            for (var n = 0; n < svMain.QuestDiaryList.Count; n++)
            {
                diarylist = svMain.QuestDiaryList[n] as ArrayList;
                for (var i = 0; i < diarylist.Count; i++)
                {
                    pqdd = (TQDDinfo)diarylist[i];
                    pqdd.SList.Free();
                    this.Dispose(pqdd);
                }
                diarylist.Free();
            }
            svMain.QuestDiaryList.Clear();
            bobegin = false;
            for (var n = 1; n <= Grobal2.MAXQUESTINDEXBYTE * 8; n++)
            {
                diarylist = null;
                flname = svMain.EnvirDir +LocalDB.QUESTDIARYDIR + LoadQuestDiary_XXStr(n) + ".txt";
                if (File.Exists(flname))
                {
                    title = "";
                    pqdd = null;
                    strlist = new StringList();
                    strlist.LoadFromFile(flname);
                    for (var i = 0; i < strlist.Count; i++)
                    {
                        str = strlist[i];
                        if (str != "")
                        {
                            if (str[0] != ';')
                            {
                                if ((str[0] == '[') && (str.Length > 2))
                                {
                                    if (title == "")
                                    {
                                        HUtil32.ArrestStringEx(str, "[", "]", ref title);
                                        diarylist = new ArrayList();
                                        pqdd = new TQDDinfo();
                                        pqdd.Index = n;
                                        // 扁夯
                                        pqdd.Title = title;
                                        pqdd.SList = new ArrayList();
                                        diarylist.Add(pqdd);
                                        bobegin = true;
                                        continue;
                                    }
                                    if (str[1] != '@')
                                    {
                                        str = HUtil32.GetValidStr3(str, ref numstr, new string[] { " ", "\09" });
                                        HUtil32.ArrestStringEx(numstr, "[", "]", ref numstr);
                                        pqdd = new TQDDinfo();
                                        pqdd.Index = HUtil32.Str_ToInt(numstr, 0);
                                        pqdd.Title = str;
                                        pqdd.SList = new ArrayList();
                                        diarylist.Add(pqdd);
                                        bobegin = true;
                                    }
                                    else
                                    {
                                        bobegin = false;
                                    }
                                }
                                else
                                {
                                    if (bobegin)
                                    {
                                        pqdd.SList.Add(str);
                                    }
                                }
                            }
                        }
                    }
                    strlist.Free();
                }
                if (diarylist != null)
                {
                    svMain.QuestDiaryList.Add(diarylist);
                }
                else
                {
                    svMain.QuestDiaryList.Add(null);
                }
            }
            return result;
        }

        public int LoadDropItemNoticeList()
        {
            string str;
            int result = 0;
            string flname = svMain.EnvirDir + LocalDB.DROPITEMFILE;
            svMain.DropItemNoticeList.Clear();
            if (File.Exists(flname))
            {
                StringList strlist = new StringList();
                strlist.LoadFromFile(flname);
                for (var i = 0; i < strlist.Count; i++)
                {
                    str = strlist[i].Trim();
                    if (str != "")
                    {
                        svMain.DropItemNoticeList.Add(str);
                        result = 1;
                    }
                }
                strlist.Free();
            }
            return result;
        }

        public void LoadShopItemList_UnInitShopItemList()
        {
            int i;
            svMain.ShopItemList.Enter();
            try
            {
                for (i = 0; i < svMain.ShopItemList.Count; i++)
                {
                    this.Dispose((TShopItem)svMain.ShopItemList.Items[i]);
                }
                svMain.ShopItemList.Clear();
            }
            finally
            {
                svMain.ShopItemList.Leave();
            }
        }

        public void LoadShopItemList()
        {
            int i;
            int nPrice;
            string sFileName;
            StringList LoadList;
            string sLineText = string.Empty;
            string sItemClass = string.Empty;
            string sItemName = string.Empty;
            string s1 = string.Empty;
            string s2 = string.Empty;
            string s3 = string.Empty;
            string sItemPrice = string.Empty;
            string sItemDes = string.Empty;
            TStdItem pStdItem;
            TShopItem pShopItem;
            sFileName = svMain.EnvirDir + LocalDB.SHOPITEMFILE;
            if (!File.Exists(sFileName))
            {
                LoadList = new StringList();
                LoadList.Add(";引擎插件商铺配置文件");
                LoadList.Add(";物品类别\09物品名称\09Item.wil序号\09出售价格\09Effect.wil序号\09图片数量\09描述");
                LoadList.SaveToFile(sFileName);
                LoadList.Free();
                return;
            }
            LoadShopItemList_UnInitShopItemList();
            LoadList = new StringList();
            LoadList.LoadFromFile(sFileName);
            svMain.ShopItemList.Enter();
            try
            {
                for (i = 0; i < LoadList.Count; i++)
                {
                    sLineText = LoadList[i];
                    if ((sLineText != "") && (sLineText[0] != ';'))
                    {
                        sLineText = HUtil32.GetValidStr3(sLineText, ref sItemClass, new string[] { "\09" });
                        sLineText = HUtil32.GetValidStr3(sLineText, ref sItemName, new string[] { "\09" });
                        sLineText = HUtil32.GetValidStr3(sLineText, ref s1, new string[] { "\09" });
                        sLineText = HUtil32.GetValidStr3(sLineText, ref sItemPrice, new string[] { "\09" });
                        sLineText = HUtil32.GetValidStr3(sLineText, ref s2, new string[] { "\09" });
                        sLineText = HUtil32.GetValidStr3(sLineText, ref s3, new string[] { "\09" });
                        sLineText = HUtil32.GetValidStr3(sLineText, ref sItemDes, new string[] { "\09" });
                        nPrice = HUtil32.Str_ToInt(sItemPrice, 2000000000);
                        if ((sItemName != "") && (nPrice > 0) && (sItemDes != ""))
                        {
                            pStdItem = svMain.UserEngine.GetStdItemFromName(sItemName);
                            if (pStdItem != null)
                            {
                                pShopItem = new TShopItem();
                                pShopItem.wPrice = nPrice;
                                pShopItem.sExplain = sItemDes;
                                pShopItem.sItemName = sItemName;
                                pShopItem.btClass = (byte)HUtil32.Str_ToInt(sItemClass, 5);
                                pShopItem.wLooks = (ushort)HUtil32.Str_ToInt(s1, 0);
                                pShopItem.wShape1 = (ushort)HUtil32.Str_ToInt(s2, 0);
                                pShopItem.wShape2 = (ushort)HUtil32.Str_ToInt(s3, 0);
                                svMain.ShopItemList.Add(pShopItem);
                            }
                        }
                    }
                }
            }
            finally
            {
                svMain.ShopItemList.Leave();
            }
            LoadList.Free();
        }

        public void LoadMarketDef_AddAvailableCommands(string npcsaying, ArrayList cmdlist)
        {
            string capture = string.Empty;
            string str = npcsaying;
            while (true)
            {
                if (str == "")
                {
                    break;
                }
                str = HUtil32.ArrestStringEx(str, "@", ">", ref capture);
                if (capture != "")
                {
                    cmdlist.Add("@" + capture);
                }
                else
                {
                    break;
                }
            }
        }

        public TQuestRecord LoadMarketDef_NewQuest()
        {
            TQuestRecord result;
            TQuestRecord pq;
            pq = new TQuestRecord();
            pq.BoRequire = false;
            //FillChar(pq.QuestRequireArr, sizeof(TQuestRequire) * ObjNpc.MAXREQUIRE, '\0');
            reqidx = 0;
            pq.SayingList = new ArrayList();
            npc.Sayings.Add(pq);
            result = pq;
            return result;
        }

        public bool LoadMarketDef_DecodeConditionStr(string srcstr, TQuestConditionInfo pqc)
        {
            string cmdstr = string.Empty;
            string paramstr = string.Empty;
            string tagstr = string.Empty;
            bool result = false;
            srcstr = HUtil32.GetValidStrCap(srcstr, ref cmdstr, new string[] { " ", "\09" });
            srcstr = HUtil32.GetValidStrCap(srcstr, ref paramstr, new string[] { " ", "\09" });
            srcstr = HUtil32.GetValidStrCap(srcstr, ref tagstr, new string[] { " ", "\09" });
            cmdstr = cmdstr.ToUpper();
            int ident = 0;
            if (cmdstr.ToUpper() == "CHECK")
            {
                ident = Grobal2.QI_CHECK;
                HUtil32.ArrestStringEx(paramstr, "[", "]", ref paramstr);
                if (!HUtil32.IsStringNumber(paramstr))
                {
                    ident = 0;
                }
                if (!HUtil32.IsStringNumber(tagstr))
                {
                    ident = 0;
                }
            }
            if (cmdstr.ToUpper() == "CHECKLOVERFLAG")
            {
                ident = Grobal2.QI_CHECKLOVERFLAG;
                HUtil32.ArrestStringEx(paramstr, "[", "]", ref paramstr);
                if (!HUtil32.IsStringNumber(paramstr))
                {
                    ident = 0;
                }
                if (!HUtil32.IsStringNumber(tagstr))
                {
                    ident = 0;
                }
            }
            if (cmdstr.ToUpper() == "CHECKOPEN")
            {
                ident = Grobal2.QI_CHECKOPENUNIT;
                HUtil32.ArrestStringEx(paramstr, "[", "]", ref paramstr);
                if (!HUtil32.IsStringNumber(paramstr))
                {
                    ident = 0;
                }
                if (!HUtil32.IsStringNumber(tagstr))
                {
                    ident = 0;
                }
            }
            if (cmdstr.ToUpper() == "CHECKUNIT")
            {
                ident = Grobal2.QI_CHECKUNIT;
                HUtil32.ArrestStringEx(paramstr, "[", "]", ref paramstr);
                if (!HUtil32.IsStringNumber(paramstr))
                {
                    ident = 0;
                }
                if (!HUtil32.IsStringNumber(tagstr))
                {
                    ident = 0;
                }
            }
            if (cmdstr.ToUpper() == "RANDOM")
            {
                ident = Grobal2.QI_RANDOM;
            }
            if (cmdstr.ToUpper() == "GENDER")
            {
                ident = Grobal2.QI_GENDER;
            }
            if (cmdstr.ToUpper() == "DAYTIME")
            {
                ident = Grobal2.QI_DAYTIME;
            }
            if (cmdstr.ToUpper() == "CHECKLEVEL")
            {
                ident = Grobal2.QI_CHECKLEVEL;
            }
            if (cmdstr.ToUpper() == "CHECKJOB")
            {
                ident = Grobal2.QI_CHECKJOB;
            }
            if (cmdstr.ToUpper() == "CHECKITEM")
            {
                ident = Grobal2.QI_CHECKITEM;
            }
            if (cmdstr.ToUpper() == "CHECKITEMW")
            {
                ident = Grobal2.QI_CHECKITEMW;
            }
            if (cmdstr.ToUpper() == "CHECKGOLD")
            {
                ident = Grobal2.QI_CHECKGOLD;
            }
            if (cmdstr.ToUpper() == "ISTAKEITEM")
            {
                ident = Grobal2.QI_ISTAKEITEM;
            }
            if (cmdstr.ToUpper() == "CHECKDURA")
            {
                ident = Grobal2.QI_CHECKDURA;
            }
            if (cmdstr.ToUpper() == "CHECKDURAEVA")
            {
                ident = Grobal2.QI_CHECKDURAEVA;
            }
            if (cmdstr.ToUpper() == "DAYOFWEEK")
            {
                ident = Grobal2.QI_DAYOFWEEK;
            }
            if (cmdstr.ToUpper() == "HOUR")
            {
                ident = Grobal2.QI_TIMEHOUR;
            }
            if (cmdstr.ToUpper() == "MIN")
            {
                ident = Grobal2.QI_TIMEMIN;
            }
            if (cmdstr.ToUpper() == "CHECKPKPOINT")
            {
                ident = Grobal2.QI_CHECKPKPOINT;
            }
            if (cmdstr.ToUpper() == "CHECKLUCKYPOINT")
            {
                ident = Grobal2.QI_CHECKLUCKYPOINT;
            }
            if (cmdstr.ToUpper() == "CHECKMONMAP")
            {
                ident = Grobal2.QI_CHECKMON_MAP;
            }
            if (cmdstr.ToUpper() == "CHECKMONMAPNORECALL")
            {
                ident = Grobal2.QI_CHECKMON_NORECALLMOB_MAP;
            }
            if (cmdstr.ToUpper() == "CHECKMONAREA")
            {
                ident = Grobal2.QI_CHECKMON_AREA;
            }
            if (cmdstr.ToUpper() == "CHECKHUM")
            {
                ident = Grobal2.QI_CHECKHUM;
            }
            if (cmdstr.ToUpper() == "CHECKBAGGAGE")
            {
                ident = Grobal2.QI_CHECKBAGGAGE;
            }
            // 6-11
            if (cmdstr.ToUpper() == "CHECKNAMELIST")
            {
                ident = Grobal2.QI_CHECKNAMELIST;
            }
            if (cmdstr.ToUpper() == "CHECK_DELETE_NAMELIST")
            {
                ident = Grobal2.QI_CHECKANDDELETENAMELIST;
            }
            if (cmdstr.ToUpper() == "CHECK_DELETE_IDLIST")
            {
                ident = Grobal2.QI_CHECKANDDELETEIDLIST;
            }
            // *dq
            if (cmdstr.ToUpper() == "IFGETDAILYQUEST")
            {
                ident = Grobal2.QI_IFGETDAILYQUEST;
            }
            if (cmdstr.ToUpper() == "RANDOMEX")
            {
                ident = Grobal2.QI_RANDOMEX;
            }
            if (cmdstr.ToUpper() == "CHECKDAILYQUEST")
            {
                ident = Grobal2.QI_CHECKDAILYQUEST;
            }
            if (cmdstr.ToUpper() == "CHECKGRADEITEM")
            {
                ident = Grobal2.QI_CHECKGRADEITEM;
            }
            if (cmdstr.ToUpper() == "CHECKBAGREMAIN")
            {
                ident = Grobal2.QI_CHECKBAGREMAIN;
            }
            if (cmdstr.ToUpper() == "EQUALVAR")
            {
                ident = Grobal2.QI_EQUALVAR;
            }
            if (cmdstr.ToUpper() == "EQUAL")
            {
                ident = Grobal2.QI_EQUAL;
            }
            if (cmdstr.ToUpper() == "LARGE")
            {
                ident = Grobal2.QI_LARGE;
            }
            if (cmdstr.ToUpper() == "SMALL")
            {
                ident = Grobal2.QI_SMALL;
            }
            // sonmg(2004/08/25)
            if (cmdstr.ToUpper() == "ISGROUPOWNER")
            {
                ident = Grobal2.QI_ISGROUPOWNER;
            }
            if (cmdstr.ToUpper() == "ISEXPUSER")
            {
                ident = Grobal2.QI_ISEXPUSER;
            }
            if (cmdstr.ToUpper() == "CHECKLOVERFLAG")
            {
                ident = Grobal2.QI_CHECKLOVERFLAG;
            }
            if (cmdstr.ToUpper() == "CHECKLOVERRANGE")
            {
                ident = Grobal2.QI_CHECKLOVERRANGE;
            }
            if (cmdstr.ToUpper() == "CHECKLOVERDAY")
            {
                ident = Grobal2.QI_CHECKLOVERDAY;
            }
            // 厘盔扁何陛
            if (cmdstr.ToUpper() == "CHECKDONATION")
            {
                ident = Grobal2.QI_CHECKDONATION;
            }
            if (cmdstr.ToUpper() == "ISGUILDMASTER")
            {
                ident = Grobal2.QI_ISGUILDMASTER;
            }
            if (cmdstr.ToUpper() == "CHECKWEAPONBADLUCK")
            {
                ident = Grobal2.QI_CHECKWEAPONBADLUCK;
            }
            if (cmdstr.ToUpper() == "CHECKPREMIUMGRADE")
            {
                ident = Grobal2.QI_CHECKPREMIUMGRADE;
            }
            if (cmdstr.ToUpper() == "CHECKCHILDMOB")
            {
                ident = Grobal2.QI_CHECKCHILDMOB;
            }
            if (cmdstr.ToUpper() == "CHECKGROUPJOBBALANCE")
            {
                ident = Grobal2.QI_CHECKGROUPJOBBALANCE;
            }
            if (cmdstr.ToUpper() == "CHECKRANGEONELOVER")
            {
                ident = Grobal2.QI_CHECKRANGEONELOVER;
            }
            if (cmdstr.ToUpper() == "ISNEWHUMAN")
            {
                ident = Grobal2.QI_ISNEWHUMAN;
            }
            if (cmdstr.ToUpper() == "ISSYSOP")
            {
                ident = Grobal2.QI_ISSYSOP;
            }
            if (cmdstr.ToUpper() == "ISADMIN")
            {
                ident = Grobal2.QI_ISADMIN;
            }
            if (cmdstr.ToUpper() == "CHECKGAMEGOLD")
            {
                ident = Grobal2.QI_CHECKGAMEGOLD;
            }
            if (cmdstr.ToUpper() == "CHECKMARRY")
            {
                ident = Grobal2.QI_CHECKMARRY;
            }
            if (cmdstr.ToUpper() == "CHECKPOSEMARRY")
            {
                ident = Grobal2.QI_CHECKPOSEMARRY;
            }
            if (cmdstr.ToUpper() == "CHECKPOSEGENDER")
            {
                ident = Grobal2.QI_CHECKPOSEGENDER;
            }
            if (cmdstr.ToUpper() == "CHECKPOSEDIR")
            {
                ident = Grobal2.QI_CHECKPOSEDIR;
            }
            if (cmdstr.ToUpper() == "CHECKPOSELEVEL")
            {
                ident = Grobal2.QI_CHECKPOSELEVEL;
            }
            if (cmdstr.ToUpper() == "CHECKMASTER")
            {
                ident = Grobal2.QI_CHECKMASTER;
            }
            if (cmdstr.ToUpper() == "CHECKISMASTER")
            {
                ident = Grobal2.QI_CHECKISMASTER;
            }
            if (cmdstr.ToUpper() == "CHECKPOSEMASTER")
            {
                ident = Grobal2.QI_CHECKPOSEMASTER;
            }
            if (cmdstr.ToUpper() == "HAVEMASTER")
            {
                ident = Grobal2.QI_HAVEMASTER;
            }
            if (cmdstr.ToUpper() == "CHECKMASTERCOUNT")
            {
                ident = Grobal2.QI_CHECKMASTERCOUNT;
            }
            if (ident > 0)
            {
                pqc.IfIdent = ident;
                if (paramstr != "")
                {
                    if (paramstr[0] == '\"')
                    {
                        HUtil32.ArrestStringEx(paramstr, "\"", "\"", ref paramstr);
                    }
                }
                pqc.IfParam = paramstr;
                if (tagstr != "")
                {
                    if (tagstr[0] == '\"')
                    {
                        HUtil32.ArrestStringEx(tagstr, "\"", "\"", ref tagstr);
                    }
                }
                pqc.IfTag = tagstr;
                if (HUtil32.IsStringNumber(paramstr))
                {
                    pqc.IfParamVal = HUtil32.Str_ToInt(paramstr, 0);
                }
                if (HUtil32.IsStringNumber(tagstr))
                {
                    pqc.IfTagVal = HUtil32.Str_ToInt(tagstr, 0);
                }
                result = true;
            }
            return result;
        }

        public bool LoadMarketDef_DecodeActionStr(string srcstr, TQuestActionInfo pqa)
        {
            string cmdstr = string.Empty;
            string paramstr = string.Empty;
            string tagstr = string.Empty;
            string extrastr = string.Empty;
            string extrastr4 = string.Empty;
            string extrastr5 = string.Empty;
            string extrastr6 = string.Empty;
            string extrastr7 = string.Empty;
            bool result = false;
            srcstr = HUtil32.GetValidStrCap(srcstr, ref cmdstr, new string[] { " ", "\09" });
            srcstr = HUtil32.GetValidStrCap(srcstr, ref paramstr, new string[] { " ", "\09" });
            srcstr = HUtil32.GetValidStrCap(srcstr, ref tagstr, new string[] { " ", "\09" });
            srcstr = HUtil32.GetValidStrCap(srcstr, ref extrastr, new string[] { " ", "\09" });
            srcstr = HUtil32.GetValidStrCap(srcstr, ref extrastr4, new string[] { " ", "\09" });
            srcstr = HUtil32.GetValidStrCap(srcstr, ref extrastr5, new string[] { " ", "\09" });
            srcstr = HUtil32.GetValidStrCap(srcstr, ref extrastr6, new string[] { " ", "\09" });
            srcstr = HUtil32.GetValidStrCap(srcstr, ref extrastr7, new string[] { " ", "\09" });
            cmdstr = cmdstr.ToUpper();
            int ident = 0;
            if (cmdstr.ToUpper() == "SET")
            {
                ident = Grobal2.QA_SET;
                HUtil32.ArrestStringEx(paramstr, "[", "]", ref paramstr);
                if (!HUtil32.IsStringNumber(paramstr))
                {
                    ident = 0;
                }
                if (!HUtil32.IsStringNumber(tagstr))
                {
                    ident = 0;
                }
            }
            if (cmdstr.ToUpper() == "SETLOVERFLAG")
            {
                ident = Grobal2.QA_SETLOVERFLAG;
                HUtil32.ArrestStringEx(paramstr, "[", "]", ref paramstr);
                if (!HUtil32.IsStringNumber(paramstr))
                {
                    ident = 0;
                }
                if (!HUtil32.IsStringNumber(tagstr))
                {
                    ident = 0;
                }
            }
            if (cmdstr.ToUpper() == "RESET")
            {
                ident = Grobal2.QA_RESET;
                HUtil32.ArrestStringEx(paramstr, "[", "]", ref paramstr);
            }
            if (cmdstr.ToUpper() == "SETOPEN")
            {
                ident = Grobal2.QA_OPENUNIT;
                HUtil32.ArrestStringEx(paramstr, "[", "]", ref paramstr);
                if (!HUtil32.IsStringNumber(paramstr))
                {
                    ident = 0;
                }
                if (!HUtil32.IsStringNumber(tagstr))
                {
                    ident = 0;
                }
            }
            if (cmdstr.ToUpper() == "SETUNIT")
            {
                ident = Grobal2.QA_SETUNIT;
                HUtil32.ArrestStringEx(paramstr, "[", "]", ref paramstr);
                if (!HUtil32.IsStringNumber(paramstr))
                {
                    ident = 0;
                }
                if (!HUtil32.IsStringNumber(tagstr))
                {
                    ident = 0;
                }
            }
            if (cmdstr.ToUpper() == "RESETUNIT")
            {
                ident = Grobal2.QA_RESETUNIT;
                HUtil32.ArrestStringEx(paramstr, "[", "]", ref paramstr);
            }
            if (cmdstr.ToUpper() == "TAKE")
            {
                ident = Grobal2.QA_TAKE;
            }
            if (cmdstr.ToUpper() == "GIVE")
            {
                ident = Grobal2.QA_GIVE;
            }
            if (cmdstr.ToUpper() == "TAKEW")
            {
                ident = Grobal2.QA_TAKEW;
            }
            if (cmdstr.ToUpper() == "CLOSE")
            {
                ident = Grobal2.QA_CLOSE;
            }
            if (cmdstr.ToUpper() == "CLOSENOINVEN")
            {
                ident = Grobal2.QA_CLOSENOINVEN;
            }
            if (cmdstr.ToUpper() == "MAPMOVE")
            {
                ident = Grobal2.QA_MAPMOVE;
            }
            if (cmdstr.ToUpper() == "MAP")
            {
                ident = Grobal2.QA_MAPRANDOM;
            }
            if (cmdstr.ToUpper() == "BREAK")
            {
                ident = Grobal2.QA_BREAK;
            }
            if (cmdstr.ToUpper() == "TIMERECALL")
            {
                ident = Grobal2.QA_TIMERECALL;
            }
            if (cmdstr.ToUpper() == "TIMERECALLGROUP")
            {
                ident = Grobal2.QA_TIMERECALLGROUP;
            }
            if (cmdstr.ToUpper() == "BREAKTIMERECALL")
            {
                ident = Grobal2.QA_BREAKTIMERECALL;
            }
            if (cmdstr.ToUpper() == "PARAM1")
            {
                ident = Grobal2.QA_PARAM1;
            }
            if (cmdstr.ToUpper() == "PARAM2")
            {
                ident = Grobal2.QA_PARAM2;
            }
            if (cmdstr.ToUpper() == "PARAM3")
            {
                ident = Grobal2.QA_PARAM3;
            }
            if (cmdstr.ToUpper() == "PARAM4")
            {
                ident = Grobal2.QA_PARAM4;
            }
            if (cmdstr.ToUpper() == "TAKECHECKITEM")
            {
                ident = Grobal2.QA_TAKECHECKITEM;
            }
            if (cmdstr.ToUpper() == "MONGEN")
            {
                ident = Grobal2.QA_MONGEN;
            }
            if (cmdstr.ToUpper() == "MONCLEAR")
            {
                ident = Grobal2.QA_MONCLEAR;
            }
            if (cmdstr.ToUpper() == "MOV")
            {
                ident = Grobal2.QA_MOV;
            }
            if (cmdstr.ToUpper() == "INC")
            {
                ident = Grobal2.QA_INC;
            }
            if (cmdstr.ToUpper() == "DEC")
            {
                ident = Grobal2.QA_DEC;
            }
            if (cmdstr.ToUpper() == "SUM")
            {
                ident = Grobal2.QA_SUM;
            }
            if (cmdstr.ToUpper() == "MOVR")
            {
                ident = Grobal2.QA_MOVRANDOM;
            }
            if (cmdstr.ToUpper() == "EXCHANGEMAP")
            {
                ident = Grobal2.QA_EXCHANGEMAP;
            }
            if (cmdstr.ToUpper() == "RECALLMAP")
            {
                ident = Grobal2.QA_RECALLMAP;
            }
            if (cmdstr.ToUpper() == "ADDBATCH")
            {
                ident = Grobal2.QA_ADDBATCH;
            }
            if (cmdstr.ToUpper() == "BATCHDELAY")
            {
                ident = Grobal2.QA_BATCHDELAY;
            }
            if (cmdstr.ToUpper() == "BATCHMOVE")
            {
                ident = Grobal2.QA_BATCHMOVE;
            }
            if (cmdstr.ToUpper() == "PLAYDICE")
            {
                ident = Grobal2.QA_PLAYDICE;
            }
            if (cmdstr.ToUpper() == "PLAYROCK")
            {
                ident = Grobal2.QA_PLAYROCK;
            }
            // 6-11
            if (cmdstr.ToUpper() == "ADDNAMELIST")
            {
                ident = Grobal2.QA_ADDNAMELIST;
            }
            if (cmdstr.ToUpper() == "DELNAMELIST")
            {
                ident = Grobal2.QA_DELETENAMELIST;
            }
            // *DQ
            if (cmdstr.ToUpper() == "RANDOMSETDAILYQUEST")
            {
                ident = Grobal2.QA_RANDOMSETDAILYQUEST;
            }
            if (cmdstr.ToUpper() == "SETDAILYQUEST")
            {
                ident = Grobal2.QA_SETDAILYQUEST;
            }
            if (cmdstr.ToUpper() == "TAKEGRADEITEM")
            {
                ident = Grobal2.QA_TAKEGRADEITEM;
            }
            if (cmdstr.ToUpper() == "GOQUEST")
            {
                ident = Grobal2.QA_GOTOQUEST;
            }
            if (cmdstr.ToUpper() == "ENDQUEST")
            {
                ident = Grobal2.QA_ENDQUEST;
            }
            if (cmdstr.ToUpper() == "GOTO")
            {
                ident = Grobal2.QA_GOTO;
            }
            if (cmdstr.ToUpper() == "SOUND")
            {
                ident = Grobal2.QA_SOUND;
            }
            if (cmdstr.ToUpper() == "SOUNDALL")
            {
                ident = Grobal2.QA_SOUNDALL;
            }
            if (cmdstr.ToUpper() == "CHANGEGENDER")
            {
                ident = Grobal2.QA_CHANGEGENDER;
            }
            if (cmdstr.ToUpper() == "KICK")
            {
                ident = Grobal2.QA_KICK;
            }
            if (cmdstr.ToUpper() == "MOVEALLMAP")
            {
                ident = Grobal2.QA_MOVEALLMAP;
            }
            if (cmdstr.ToUpper() == "MOVEALLMAPGROUP")
            {
                ident = Grobal2.QA_MOVEALLMAPGROUP;
            }
            if (cmdstr.ToUpper() == "RECALLMAPGROUP")
            {
                ident = Grobal2.QA_RECALLMAPGROUP;
            }
            if (cmdstr.ToUpper() == "WEAPONUPGRADE")
            {
                ident = Grobal2.QA_WEAPONUPGRADE;
            }
            if (cmdstr.ToUpper() == "SETALLINMAP")
            {
                ident = Grobal2.QA_SETALLINMAP;
                HUtil32.ArrestStringEx(paramstr, "[", "]", ref paramstr);
                if (!HUtil32.IsStringNumber(paramstr))
                {
                    ident = 0;
                }
                if (!HUtil32.IsStringNumber(tagstr))
                {
                    ident = 0;
                }
            }
            if (cmdstr.ToUpper() == "INCPKPOINT")
            {
                ident = Grobal2.QA_INCPKPOINT;
            }
            if (cmdstr.ToUpper() == "DECPKPOINT")
            {
                ident = Grobal2.QA_DECPKPOINT;
            }
            if (cmdstr.ToUpper() == "MOVETOLOVER")
            {
                ident = Grobal2.QA_MOVETOLOVER;
            }
            if (cmdstr.ToUpper() == "BREAKLOVER")
            {
                ident = Grobal2.QA_BREAKLOVER;
            }
            // 厘盔扁何陛
            if (cmdstr.ToUpper() == "DECDONATION")
            {
                ident = Grobal2.QA_DECDONATION;
            }
            if (cmdstr.ToUpper() == "SHOWEFFECT")
            {
                ident = Grobal2.QA_SHOWEFFECT;
            }
            if (cmdstr.ToUpper() == "MONGENAROUND")
            {
                ident = Grobal2.QA_MONGENAROUND;
            }
            if (cmdstr.ToUpper() == "RECALLMOB")
            {
                ident = Grobal2.QA_RECALLMOB;
            }
            if (cmdstr.ToUpper() == "SETLOVERFLAG")
            {
                ident = Grobal2.QA_SETLOVERFLAG;
            }
            if (cmdstr.ToUpper() == "GUILDSECESSION")
            {
                ident = Grobal2.QA_GUILDSECESSION;
            }
            if (cmdstr.ToUpper() == "GIVETOLOVER")
            {
                ident = Grobal2.QA_GIVETOLOVER;
            }
            if (cmdstr.ToUpper() == "INCMEMORIALCOUNT")
            {
                ident = Grobal2.QA_INCMEMORIALCOUNT;
            }
            if (cmdstr.ToUpper() == "DECMEMORIALCOUNT")
            {
                ident = Grobal2.QA_DECMEMORIALCOUNT;
            }
            if (cmdstr.ToUpper() == "SAVEMEMORIALCOUNT")
            {
                ident = Grobal2.QA_SAVEMEMORIALCOUNT;
            }
            if (cmdstr.ToUpper() == "SECONDSCARD")
            {
                ident = Grobal2.QA_SECONDSCARD;
            }
            if (cmdstr.ToUpper() == "MARRY")
            {
                ident = Grobal2.QA_MARRY;
            }
            if (cmdstr.ToUpper() == "UNMARRY")
            {
                ident = Grobal2.QA_UNMARRY;
            }
            if (cmdstr.ToUpper() == "MASTER")
            {
                ident = Grobal2.QA_MASTER;
            }
            if (cmdstr.ToUpper() == "UNMASTER")
            {
                ident = Grobal2.QA_UNMASTER;
            }
            if (cmdstr.ToUpper() == "SETHUMICON")
            {
                ident = Grobal2.QA_SETHUMICON;
            }
            if (ident > 0)
            {
                pqa.ActIdent = ident;
                if (paramstr != "")
                {
                    if (paramstr[0] == '\"')
                    {
                        HUtil32.ArrestStringEx(paramstr, "\"", "\"", ref paramstr);
                    }
                }
                pqa.ActParam = paramstr;
                if (tagstr != "")
                {
                    if (tagstr[0] == '\"')
                    {
                        HUtil32.ArrestStringEx(tagstr, "\"", "\"", ref tagstr);
                    }
                }
                pqa.ActTag = tagstr;
                pqa.ActExtra = extrastr;
                pqa.ActParam4 = extrastr4;
                pqa.ActParam5 = extrastr5;
                pqa.ActParam6 = extrastr6;
                pqa.ActParam7 = extrastr7;
                if (HUtil32.IsStringNumber(paramstr))
                {
                    pqa.ActParamVal = HUtil32.Str_ToInt(paramstr, 0);
                }
                if (HUtil32.IsStringNumber(tagstr))
                {
                    pqa.ActTagVal = HUtil32.Str_ToInt(tagstr, 1);
                }
                if (HUtil32.IsStringNumber(extrastr))
                {
                    pqa.ActExtraVal = HUtil32.Str_ToInt(extrastr, 1);
                }
                if (HUtil32.IsStringNumber(extrastr4))
                {
                    pqa.ActParamVal4 = HUtil32.Str_ToInt(extrastr4, 0);
                }
                if (HUtil32.IsStringNumber(extrastr5))
                {
                    pqa.ActParamVal5 = HUtil32.Str_ToInt(extrastr5, 0);
                }
                if (HUtil32.IsStringNumber(extrastr6))
                {
                    pqa.ActParamVal6 = HUtil32.Str_ToInt(extrastr6, 0);
                }
                if (HUtil32.IsStringNumber(extrastr7))
                {
                    pqa.ActParamVal7 = HUtil32.Str_ToInt(extrastr7, 0);
                }
                result = true;
            }
            return result;
        }

        public int LoadMarketDef_ApplyCallProcedure(StringList srclist)
        {
            string str;
            string str2;
            string data = string.Empty;
            string flname2;
            int calls = 0;
            for (var i = 0; i < srclist.Count; i++)
            {
                str = srclist[i].Trim();
                if (str != "")
                {
                    if (str[0] == '#')
                    {
                        if (HUtil32.CompareLStr(str, "#CALL", 5))
                        {
                            str = HUtil32.ArrestStringEx(str, "[", "]", ref data);
                            flname2 = data.Trim();
                            str2 = str.Trim();
                            if (LocalDB.CutAndAddFromFile(svMain.EnvirDir +LocalDB.QUESTDIARYDIR + flname2, str2, srclist))
                            {
                                srclist[i] = "#ACT";
                                srclist.Insert(i + 1, "goto " + str2);
                            }
                            else
                            {
                                svMain.MainOutMessage("script error, load fail: " + flname2 + " - " + str2);
                            }
                            calls++;
                        }
                    }
                }
            }
            return calls;
        }

        public void LoadMarketDef_AssortDefines(StringList srclist, IList<TDefineInfo> rlist, ref string homestr)
        {
            string str;
            string str2;
            string data = string.Empty;
            string defname = string.Empty;
            string defcontents = string.Empty;
            string newflname = string.Empty;
            StringList nlist;
            TDefineInfo pdef;
            for (var i = 0; i < srclist.Count; i++)
            {
                str = srclist[i].Trim();
                if (str != "")
                {
                    if (str[0] == '#')
                    {
                        if (HUtil32.CompareLStr(str, "#SETHOME", 8))
                        {
                            str2 = HUtil32.GetValidStr3(str, ref data, new string[] { " ", "\09" });
                            homestr = str2.Trim();
                            srclist[i] = "";
                        }
                        if (HUtil32.CompareLStr(str, "#DEFINE", 7))
                        {
                            str = HUtil32.GetValidStr3(str, ref data, new string[] { " ", "\09" });
                            str = HUtil32.GetValidStr3(str, ref defname, new string[] { " ", "\09" });
                            str = HUtil32.GetValidStr3(str, ref defcontents, new string[] { " ", "\09" });
                            pdef = new TDefineInfo();
                            pdef.defname = defname.ToUpper();
                            pdef.defvalue = defcontents;
                            rlist.Add(pdef);
                            srclist[i] = "";
                        }
                        if (HUtil32.CompareLStr(str, "#INCLUDE", 8))
                        {
                            str2 = HUtil32.GetValidStr3(str, ref data, new string[] { " ", "\09" });
                            newflname = str2.Trim();
                            newflname = svMain.EnvirDir +LocalDB.QUESTDEFINEDIR + newflname;
                            if (File.Exists(newflname))
                            {
                                nlist = new StringList();
                                nlist.LoadFromFile(newflname);
                                LoadMarketDef_AssortDefines(nlist, rlist, ref homestr);
                                nlist.Free();
                            }
                            else
                            {
                                svMain.MainOutMessage("script error, load fail: " + newflname);
                            }
                            srclist[i] = "";
                        }
                    }
                }
            }
        }

        public int LoadMarketDef(TNormNpc npc, string basedir, string marketname, bool bomarket)
        {
            int i;
            int j;
            int k;
            int n;
            int stdmode;
            int rate;
            int reqidx;
            string flname = string.Empty;
            string str = string.Empty;
            string str2 = string.Empty;
            string data = string.Empty;
            string idxstr = string.Empty;
            string valstr = string.Empty;
            string itmname = string.Empty;
            string scount = string.Empty;
            string shour = string.Empty;
            string taghomestr = string.Empty;
            string src1 = string.Empty;
            string src2 = string.Empty;
            StringList strlist;
            IList<TDefineInfo> deflist;
            TMarketProduct pp;
            int step;
            int questnumber;
            string stepstr;
            TSayingRecord psay;
            TSayingProcedure psayproc;
            TQuestRecord pquest;
            TQuestConditionInfo pqcon;
            TQuestActionInfo pqact;
            TDefineInfo pdef;
            bool bobegin;
            result = -1;
            step = 0;
            questnumber = 0;
            stdmode = 0;
            flname = svMain.EnvirDir + basedir + marketname + ".txt";
            if (File.Exists(flname))
            {
                strlist = new StringList();
                try
                {
                    strlist.LoadFromFile(flname);
                }
                catch
                {
                    strlist.Free();
                    return result;
                }
                for (i = 0; i <= 100; i++)
                {
                    if (LoadMarketDef_ApplyCallProcedure(strlist) == 0)
                    {
                        break;
                    }
                }
                deflist = new List<TDefineInfo>();
                LoadMarketDef_AssortDefines(strlist, deflist, ref taghomestr);
                pdef = new TDefineInfo();
                pdef.defname = "@HOME";
                if (taghomestr == "")
                {
                    taghomestr = "@main";
                }
                pdef.defvalue = taghomestr;
                deflist.Add(pdef);
                bobegin = false;
                for (i = 0; i < strlist.Count; i++)
                {
                    str = strlist[i].Trim();
                    if (str != "")
                    {
                        if (str[0] == '[')
                        {
                            bobegin = false;
                            continue;
                        }
                        if (str[0] == '#')
                        {
                            if (HUtil32.CompareLStr(str, "#IF", 3) || HUtil32.CompareLStr(str, "#ACT", 4) || HUtil32.CompareLStr(str, "#ELSEACT", 8))
                            {
                                bobegin = true;
                                continue;
                            }
                        }
                        if (bobegin)
                        {
                            for (k = 0; k < deflist.Count; k++)
                            {
                                pdef = deflist[k];
                                for (j = 0; j <= 9; j++)
                                {
                                    n = str.ToUpper().IndexOf(pdef.defname);
                                    if (n > 0)
                                    {
                                        src1 = str.Substring(1 - 1, n - 1);
                                        src2 = str.Substring(n + pdef.defname.Length - 1, 256);
                                        str = src1 + pdef.defvalue + src2;
                                        strlist[i] = str;
                                    }
                                    else
                                    {
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }
                for (i = 0; i < deflist.Count; i++)
                {
                    this.Dispose(deflist[i]);
                }
                deflist.Free();
                pquest = null;
                psay = null;
                psayproc = null;
                reqidx = 0;
                for (i = 0; i < strlist.Count; i++)
                {
                    str = strlist[i].Trim();
                    if (str != "")
                    {
                        if (str[0] == ';')
                        {
                            continue;
                        }
                        if (str[0] == '/')
                        {
                            continue;
                        }
                        // 惑痢狼 扁夯 沥焊,  拱啊 秒鞭前 殿
                        if ((step == 0) && bomarket)
                        {
                            if (str[0] == '%')
                            {
                                str = str.Substring(2 - 1, str.Length - 1);
                                rate = HUtil32.Str_ToInt(str, -1);
                                if (rate >= 55)
                                {
                                    ((TMerchant)npc).PriceRate = rate;
                                }
                                continue;
                            }
                            if (str[0] == '+')
                            {
                                str = str.Substring(2 - 1, str.Length - 1);
                                if (stdmode >= 0)
                                {
                                    ((TMerchant)npc).DealGoods.Add(str);
                                }
                                continue;
                            }
                        }
                        if (str[0] == '{')
                        {
                            if (HUtil32.CompareLStr(str, "{Quest", 6))
                            {
                                if (pquest != null)
                                {
                                }
                                str2 = HUtil32.GetValidStr3(str, ref data, new string[] { " ", "}", "\09" });
                                HUtil32.GetValidStr3(str2, ref data, new string[] { " ", "}", "\09" });
                                questnumber = HUtil32.Str_ToInt(data, 0);
                                pquest = LoadMarketDef_NewQuest();
                                pquest.LocalNumber = questnumber;
                                questnumber++;
                                psay = null;
                                step = 1;
                            }
                            if (HUtil32.CompareLStr(str, "{~Quest", 6))
                            {
                                continue;
                            }
                        }
                        if ((step == 1) && (pquest != null))
                        {
                            if (str[0] == '#')
                            {
                                str2 = HUtil32.GetValidStr3(str, ref data, new string[] { "=", " ", "\09" });
                                valstr = str2.Trim();
                                pquest.BoRequire = true;
                                if (HUtil32.CompareLStr(str, "#IF", 3))
                                {
                                    HUtil32.ArrestStringEx(str, "[", "]", ref idxstr);
                                    pquest.QuestRequireArr[reqidx].CheckIndex = (ushort)HUtil32.Str_ToInt(idxstr, 0);
                                    HUtil32.GetValidStr3(str2, ref valstr, new string[] { "=", " ", "\09" });
                                    n = HUtil32.Str_ToInt(valstr, 0);
                                    if (n != 0)
                                    {
                                        n = 1;
                                    }
                                    pquest.QuestRequireArr[reqidx].CheckValue = (byte)n;
                                }
                                if (HUtil32.CompareLStr(str, "#RAND", 5))
                                {
                                    pquest.QuestRequireArr[reqidx].RandomCount = HUtil32.Str_ToInt(valstr, 0);
                                }
                                continue;
                            }
                        }
                        if (str[0] == '[')
                        {
                            step = 10;
                            if (pquest == null)
                            {
                                pquest = LoadMarketDef_NewQuest();
                                pquest.LocalNumber = questnumber;
                            }
                            if (psayproc != null)
                            {
                                LoadMarketDef_AddAvailableCommands(psayproc.Saying, psayproc.AvailableCommands);
                                LoadMarketDef_AddAvailableCommands(psayproc.ElseSaying, psayproc.AvailableCommands);
                            }
                            if (str.ToLower().CompareTo("[Goods]".ToLower()) == 0)
                            {
                                step = 20;
                                ((TMerchant)npc).CreateIndex = svMain.CurrentMerchantIndex;
                                svMain.CurrentMerchantIndex++;
                                continue;
                            }
                            HUtil32.ArrestStringEx(str, "[", "]", ref stepstr);
                            psay = new TSayingRecord();
                            psay.Procs = new ArrayList();
                            psay.Title = stepstr;
                            psayproc = new TSayingProcedure();
                            psay.Procs.Add(psayproc);
                            psayproc.ConditionList = new ArrayList();
                            psayproc.ActionList = new ArrayList();
                            psayproc.Saying = "";
                            psayproc.ElseActionList = new ArrayList();
                            psayproc.ElseSaying = "";
                            psayproc.AvailableCommands = new ArrayList();
                            pquest.SayingList.Add(psay);
                            continue;
                        }
                        if ((pquest != null) && (psay != null))
                        {
                            if ((step >= 10) && (step < 20))
                            {
                                if (str[0] == '#')
                                {
                                    if (str.ToLower().CompareTo("#IF".ToLower()) == 0)
                                    {
                                        if ((psayproc.ConditionList.Count > 0) || (psayproc.Saying != ""))
                                        {
                                            psayproc = new TSayingProcedure();
                                            psay.Procs.Add(psayproc);
                                            psayproc.ConditionList = new ArrayList();
                                            psayproc.ActionList = new ArrayList();
                                            psayproc.Saying = "";
                                            psayproc.ElseActionList = new ArrayList();
                                            psayproc.ElseSaying = "";
                                            psayproc.AvailableCommands = new ArrayList();
                                        }
                                        step = 11;
                                    }
                                    if (str.ToLower().CompareTo("#ACT".ToLower()) == 0)
                                    {
                                        step = 12;
                                    }
                                    if (str.ToLower().CompareTo("#SAY".ToLower()) == 0)
                                    {
                                        step = 10;
                                    }
                                    if (str.ToLower().CompareTo("#ELSEACT".ToLower()) == 0)
                                    {
                                        step = 13;
                                    }
                                    if (str.ToLower().CompareTo("#ELSESAY".ToLower()) == 0)
                                    {
                                        step = 14;
                                    }
                                    continue;
                                }
                            }
                            if ((step == 10) && (psayproc != null))
                            {
                                psayproc.Saying = psayproc.Saying + ReplaceNewLine(str);
                                if (!svMain.TAIWANVERSION)
                                {
                                    psayproc.Saying = ReplaceChar(psayproc.Saying, "\\", (char)0xa);
                                }
                                ((TMerchant)npc).ActivateNpcUtilitys(psayproc.Saying);
                            }
                            if (step == 11)
                            {
                                pqcon = new TQuestConditionInfo();
                                if (LoadMarketDef_DecodeConditionStr(str.Trim(), pqcon))
                                {
                                    psayproc.ConditionList.Add(pqcon);
                                }
                                else
                                {
                                    this.Dispose(pqcon);
                                    svMain.MainOutMessage("script error: " + str + " line:" + i.ToString() + " : " + flname);
                                }
                            }
                            if (step == 12)
                            {
                                pqact = new TQuestActionInfo();
                                if (LoadMarketDef_DecodeActionStr(str.Trim(), pqact))
                                {
                                    psayproc.ActionList.Add(pqact);
                                }
                                else
                                {
                                    this.Dispose(pqact);
                                    svMain.MainOutMessage("script error: " + str + " line:" + i.ToString() + " : " + flname);
                                }
                            }
                            if (step == 13)
                            {
                                pqact = new TQuestActionInfo();
                                if (LoadMarketDef_DecodeActionStr(str.Trim(), pqact))
                                {
                                    psayproc.ElseActionList.Add(pqact);
                                }
                                else
                                {
                                    this.Dispose(pqact);
                                    svMain.MainOutMessage("script error: " + str + " line:" + i.ToString() + " : " + flname);
                                }
                            }
                            if (step == 14)
                            {
                                psayproc.ElseSaying = psayproc.ElseSaying + ReplaceNewLine(str);
                                if (!svMain.TAIWANVERSION)
                                {
                                    psayproc.ElseSaying = ReplaceChar(psayproc.ElseSaying, "\\", (char)0xa);
                                }
                                ((TMerchant)npc).ActivateNpcUtilitys(psayproc.ElseSaying);
                            }
                        }
                        if ((step == 20) && bomarket)
                        {
                            str = HUtil32.GetValidStrCap(str, ref itmname, new string[] { " ", "\09" });
                            str = HUtil32.GetValidStrCap(str, ref scount, new string[] { " ", "\09" });
                            str = HUtil32.GetValidStrCap(str, ref shour, new string[] { " ", "\09" });
                            if ((itmname != "") && (shour != ""))
                            {
                                pp = new TMarketProduct();
                                if (itmname != "")
                                {
                                    if (itmname[0] == '\"')
                                    {
                                        HUtil32.ArrestStringEx(itmname, "\"", "\"", ref itmname);
                                    }
                                }
                                if (itmname.Length > 20)
                                {
                                    svMain.MainOutMessage("ITEM NAME > 20:" + itmname);
                                }
                                pp.GoodsName = itmname;
                                pp.Count = _MIN(5000, HUtil32.Str_ToInt(scount, 1));
                                pp.ZenHour = HUtil32.Str_ToInt(shour, 1);
                                pp.ZenTime = GetTickCount - ((long)pp.ZenHour) * 60 * 60 * 1000;
                                ((TMerchant)npc).ProductList.Add(pp);
                            }
                        }
                    }
                }
                strlist.Free();
            }
            else
            {
                svMain.MainOutMessage("File open failure : " + flname);
            }
            return 1;
        }

        public int LoadNpcDef(TNormNpc npc, string basedir, string npcname)
        {
            if (basedir == "")
            {
                basedir =LocalDB.NPCDEFDIR;
            }
            return LoadMarketDef(npc, basedir, npcname, false);
        }

        public int LoadMarketSavedGoods(TMerchant merchant, string marketname)
        {
            int result;
            int i;
            int rbyte;
            string flname;
            TGoodsHeader header;
            int fhandle;
            TUserItem pu;
            ArrayList list;
            result = -1;
            flname =LocalDB.MARKETSAVEDDIR + marketname + ".sav";
            //fhandle = File.Open(flname, (FileMode)FileAccess.Read | FileShare.ReadWrite);
            //list = null;
            //if (fhandle > 0)
            //{
            //    rbyte = FileRead(fhandle, header, sizeof(TGoodsHeader));
            //    if (rbyte == sizeof(TGoodsHeader))
            //    {
            //        for (i = 0; i < header.RecordCount; i++)
            //        {
            //            pu = new TUserItem();
            //            rbyte = FileRead(fhandle, pu, sizeof(TUserItem));
            //            if (rbyte == sizeof(TUserItem))
            //            {
            //                // 肋给等 单捞鸥甫 滚覆
            //                if (list == null)
            //                {
            //                    list = new ArrayList();
            //                    list.Add(pu);
            //                }
            //                else
            //                {
            //                    if (((TUserItem)(list[0])).Index == pu.Index)
            //                    {
            //                        list.Add(pu);
            //                    }
            //                    else
            //                    {
            //                        merchant.GoodsList.Add(list);
            //                        // 惑前 府胶飘俊 眠啊
            //                        list = new ArrayList();
            //                        list.Add(pu);
            //                    }
            //                }
            //            }
            //            else
            //            {
            //                if (pu != null)
            //                {
            //                    this.Dispose(pu);
            //                }
            //                // Memory Leak sonmg
            //                break;
            //            }
            //        }
            //    }
            //    if (list != null)
            //    {
            //        merchant.GoodsList.Add(list);
            //    }
            //    // 惑前 府胶飘俊 眠啊
            //    fhandle.Close();
            //    result = 1;
            //}
            return result;
        }

        public int WriteMarketSavedGoods(TMerchant merchant, string marketname)
        {
            int result;
            int i;
            int k;
            string flname;
            TGoodsHeader header;
            int fhandle;
            ArrayList list;
            result = -1;
            flname =LocalDB.MARKETSAVEDDIR + marketname + ".sav";
            //if (File.Exists(flname))
            //{
            //    fhandle = File.Open(flname, (FileMode)FileAccess.Write | FileShare.ReadWrite);
            //}
            //else
            //{
            //    fhandle = File.Create(flname);
            //}
            //if (fhandle > 0)
            //{
            //    FillChar(header, sizeof(TGoodsHeader), '\0');
            //    header.RecordCount = 0;
            //    for (i = 0; i < merchant.GoodsList.Count; i++)
            //    {
            //        list = ((merchant.GoodsList[i]) as ArrayList);
            //        header.RecordCount = header.RecordCount + list.Count;
            //    }
            //    FileWrite(fhandle, header, sizeof(TGoodsHeader));
            //    for (i = 0; i < merchant.GoodsList.Count; i++)
            //    {
            //        list = ((merchant.GoodsList[i]) as ArrayList);
            //        for (k = 0; k < list.Count; k++)
            //        {
            //            FileWrite(fhandle, ((TUserItem)(list[k])), sizeof(TUserItem));
            //        }
            //    }
            //    fhandle.Close();
            //    result = 1;
            //}
            return result;
        }

        public int LoadMarketPrices(TMerchant merchant, string marketname)
        {
            int result;
            int fhandle;
            int i;
            int rbyte;
            string flname;
            TPricesInfo ppi;
            TGoodsHeader header;
            result = -1;
            flname =LocalDB.MARKETPRICESDIR + marketname + ".prc";
            //fhandle = File.Open(flname, (FileMode)FileAccess.Read | FileShare.ReadWrite);
            //if (fhandle > 0)
            //{
            //    rbyte = FileRead(fhandle, header, sizeof(TGoodsHeader));
            //    if (rbyte == sizeof(TGoodsHeader))
            //    {
            //        for (i = 0; i < header.RecordCount; i++)
            //        {
            //            ppi = new TPricesInfo();
            //            rbyte = FileRead(fhandle, ppi, sizeof(TPricesInfo));
            //            if (rbyte == sizeof(TPricesInfo))
            //            {
            //                // 肋给等 单捞鸥甫 滚覆
            //                merchant.PriceList.Add(ppi);
            //            }
            //            else
            //            {
            //                break;
            //            }
            //        }
            //    }
            //    fhandle.Close();
            //    result = 1;
            //}
            return result;
        }

        public int WriteMarketPrices(TMerchant merchant, string marketname)
        {
            int result;
            int fhandle;
            int i;
            string flname;
            TGoodsHeader header;
            result = -1;
            flname =LocalDB.MARKETPRICESDIR + marketname + ".prc";
            //if (File.Exists(flname))
            //{
            //    fhandle = File.Open(flname, (FileMode)FileAccess.Write | FileShare.ReadWrite);
            //}
            //else
            //{
            //    fhandle = File.Create(flname);
            //}
            //if (fhandle > 0)
            //{
            //    FillChar(header, sizeof(TGoodsHeader), '\0');
            //    header.RecordCount = merchant.PriceList.Count;
            //    FileWrite(fhandle, header, sizeof(TGoodsHeader));
            //    for (i = 0; i < merchant.PriceList.Count; i++)
            //    {
            //        FileWrite(fhandle, ((TPricesInfo)(merchant.PriceList[i])), sizeof(TPricesInfo));
            //    }
            //    fhandle.Close();
            //    result = 1;
            //}
            return result;
        }

        public int LoadMarketUpgradeInfos(string marketname, IList<TUpgradeInfo> upglist)
        {
            int result;
            int fhandle;
            int i;
            int count;
            int rbyte;
            string flname;
            TUpgradeInfo pup;
            TUpgradeInfo up;
            result = -1;
            flname =LocalDB.MARKETUPGRADEDIR + marketname + ".upg";
            //if (File.Exists(flname))
            //{
            //    fhandle = File.Open(flname, (FileMode)FileAccess.Read | FileShare.ReadWrite);
            //    if (fhandle > 0)
            //    {
            //        FileRead(fhandle, count, sizeof(int));
            //        for (i = 0; i < count; i++)
            //        {
            //            rbyte = FileRead(fhandle, up, sizeof(TUpgradeInfo));
            //            if (rbyte == sizeof(TUpgradeInfo))
            //            {
            //                pup = new TUpgradeInfo();
            //                pup = up;
            //                pup.readycount = 0;
            //                upglist.Add(pup);
            //            }
            //            else
            //            {
            //                break;
            //            }
            //        }
            //        result = 1;
            //        fhandle.Close();
            //    }
            //}
            return result;
        }

        public int WriteMarketUpgradeInfos(string marketname, IList<TUpgradeInfo> upglist)
        {
            int result;
            int fhandle;
            int i;
            int count;
            string flname;
            result = -1;
            flname =LocalDB.MARKETUPGRADEDIR + marketname + ".upg";
            //if (File.Exists(flname))
            //{
            //    fhandle = File.Open(flname, (FileMode)FileAccess.Write | FileShare.ReadWrite);
            //}
            //else
            //{
            //    fhandle = File.Create(flname);
            //}
            //if (fhandle > 0)
            //{
            //    count = upglist.Count;
            //    FileWrite(fhandle, count, sizeof(int));
            //    for (i = 0; i < upglist.Count; i++)
            //    {
            //        FileWrite(fhandle, ((TUpgradeInfo)(upglist[i])), sizeof(TUpgradeInfo));
            //    }
            //    fhandle.Close();
            //    result = 1;
            //}
            return result;
        }

        public int LoadMemorialCount(TNormNpc merchant, string marketname)
        {
            int result;
            int rbyte;
            string flname;
            char[] header = new char[19 + 1];
            string content;
            long headercount;
            int fhandle;
            result = -1;
            //FillChar(header, sizeof(header), '\0');
            //flname =LocalDB.MARKETSAVEDDIR + marketname +LocalDB.MEMORIALCOUNT_EXT;
            //fhandle = File.Open(flname, (FileMode)FileAccess.Read | FileShare.ReadWrite);
            //try
            //{
            //    if (fhandle > 0)
            //    {
            //        rbyte = FileRead(fhandle, header, sizeof(header));
            //        if (rbyte > 0)
            //        {
            //            HUtil32.GetValidStr3(header, content, new char[] { " ", '\r', '\n', "\09", '\0' });
            //            headercount = Convert.ToInt32(content);
            //            merchant.MemorialCount = headercount;
            //        }
            //        fhandle.Close();
            //        result = 1;
            //    }
            //}
            //catch
            //{
            //    fhandle.Close();
            //    result = -1;
            //}
            return result;
        }

        public int WriteMemorialCount(TNormNpc merchant, string marketname)
        {
            int result;
            string flname;
            char[] header = new char[19 + 1];
            int fhandle;
            string str;
            result = -1;
            //FillChar(header, sizeof(header), '\0');
            //flname =LocalDB.MARKETSAVEDDIR + marketname +LocalDB.MEMORIALCOUNT_EXT;
            //if (File.Exists(flname))
            //{
            //    fhandle = File.Open(flname, (FileMode)FileAccess.Write | FileShare.ReadWrite);
            //}
            //else
            //{
            //    fhandle = File.Create(flname);
            //}
            //try
            //{
            //    if (fhandle > 0)
            //    {
            //        str = (merchant.MemorialCount).ToString();
            //        StrPCopy(header, str);
            //        FileWrite(fhandle, header, sizeof(header));
            //        fhandle.Close();
            //        result = 1;
            //    }
            //}
            //catch
            //{
            //    fhandle.Close();
            //    result = -1;
            //}
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

    public struct TGoodsHeader
    {
        public int RecordCount;
        public int[] dummy;
    } 

    public struct TDefineInfo
    {
        public string defname;
        public string defvalue;
    }
}

namespace GameSvr
{
    public class LocalDB
    {
        public static TFrmDB FrmDB = null;
        public const string ZENFILE = "MonGen.txt";
        public const string ZENMSGFILE = "GenMsg.txt";
        public const string MAPDEFFILE = "MapInfo.txt";
        public const string MONBAGDIR = "MonItems\\";
        public const string ADMINDEFFILE = "AdminList.txt";
        public const string CHATLOGFILE = "ChatLog.txt";
        public const string MERCHANTFILE = "Merchant.txt";
        public const string MARKETDEFDIR = "Market_Def\\";
        public const string MARKETSAVEDDIR = ".\\Envir\\Market_Saved\\";
        public const string MARKETPRICESDIR = ".\\Envir\\Market_Prices\\";
        public const string MARKETUPGRADEDIR = ".\\Envir\\Market_Upg\\";
        public const string GUARDLISTFILE = "GuardList.txt";
        public const string MAKEITEMFILE = "MakeItem.txt";
        public const string NPCLISTFILE = "Npcs.txt";
        public const string NPCDEFDIR = "Npc_def\\";
        public const string STARTPOINTFILE = "StartPoint.txt";
        public const string SAFEPOINTFILE = "SafePoint.txt";
        public const string DECOITEMFILE = "DecoItem.txt";
        public const string MINIMAPFILE = "MiniMap.txt";
        public const string UNBINDFILE = "UnbindList.txt";
        public const string MAPQUESTFILE = "MapQuest.txt";
        public const string MAPQUESTDIR = "MapQuest_def\\";
        public const string QUESTDIARYDIR = "QuestDiary\\";
        public const string QUESTDEFINEDIR = "Defines\\";
        public const string STARTUPDIR = "Startup\\";
        public const string DEFAULTNPCFILE = "00Default";
        public const string DROPITEMFILE = "DropItemNotice.txt";
        public const string SHOPITEMFILE = "ShopItemList.txt";
        public const string MEMORIALCOUNT_EXT = ".cnt";

        public static bool CutAndAddFromFile(string flname, string tagstr, ArrayList list)
        {
            string str;
            bool result = false;
            if (File.Exists(flname))
            {
                StringList strlist = new StringList();
                strlist.LoadFromFile(flname);
                tagstr = "[" + tagstr + "]";
                bool bobegin = false;
                for (var i = 0; i < strlist.Count; i++)
                {
                    str = strlist[i].Trim();
                    if (str != "")
                    {
                        if (!bobegin)
                        {
                            if (str[0] == '[')
                            {
                                if (str.ToLower().CompareTo(tagstr.ToLower()) == 0)
                                {
                                    bobegin = true;
                                    list.Add(str);
                                }
                            }
                        }
                        else
                        {
                            if (str[0] == '{')
                            {
                                continue;
                            }
                            if (str[0] == '}')
                            {
                                bobegin = false;
                                result = true;
                                break;
                            }
                            list.Add(str);
                        }
                    }
                }
                strlist.Free();
            }
            return result;
        }
    } 
}