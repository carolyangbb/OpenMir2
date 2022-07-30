using System;
using System.Collections;
using System.IO;
using System.Windows.Forms;
using SystemModule;

namespace GameSvr
{
    public struct TDefenseUnit
    {
        public int X;
        public int Y;
        public string UnitName;
        public bool BoDoorOpen;
        // TCastleDoor 牢 版快
        public int HP;
        public TCreature UnitObj;
    } // end TDefenseUnit

    public struct TAttackerInfo
    {
        public DateTime AttackDate;
        public string GuildName;
        public TGuild Guild;
    } // end TAttackerInfo

    public class TUserCastle
    {
        public TEnvirnoment CastlePEnvir = null;
        public TEnvirnoment CorePEnvir = null;
        public TEnvirnoment BasementEnvir = null;
        public TDoorCore CoreCastlePDoorCore = null;
        public string CastleMapName = String.Empty;
        public string CastleName = String.Empty;
        public string OwnerGuildName = String.Empty;
        public TGuild OwnerGuild = null;
        public string CastleMap = String.Empty;
        public int CastleStartX = 0;
        public int CastleStartY = 0;
        public DateTime LatestOwnerChangeDateTime;
        // 付瘤阜栏肺 己狼 林牢捞 官诧 矫埃
        public DateTime LatestWarDateTime;
        // 付瘤阜栏肺 傍己傈捞 矫累等 矫埃
        public bool BoCastleWarChecked = false;
        public bool BoCastleUnderAttack = false;
        // 傍己傈 吝牢瘤
        public bool BoCastleWarTimeOut10min = false;
        public int BoCastleWarTimeOutRemainMinute = 0;
        public long CastleAttackStarted = 0;
        public long SaveCastleGoldTime = 0;
        // 傍己傈狼 傍拜磊俊 包访
        public ArrayList AttackerList = null;
        // 傍拜巩颇 府胶飘
        public ArrayList RushGuildList = null;
        // 傍己傈阑 窍绊 乐绰 巩颇
        public TDefenseUnit MainDoor = null;
        // TCastleDoor;  //己巩
        public TDefenseUnit LeftWall = null;
        // TWallStructure;
        public TDefenseUnit CenterWall = null;
        // TWallStructure;
        public TDefenseUnit RightWall = null;
        // TWallStructure;
        public TDefenseUnit[] Guards;
        public TDefenseUnit[] Archers;
        public DateTime IncomeToday;
        // 坷疵 技陛阑 叭扁 矫累茄 老
        public int TotalGold = 0;
        // 傈眉 技陛栏肺 叭腮 捣(己狼 磊陛), 1000父盔捞惑 笛 荐 绝促.
        public int TodayIncome = 0;
        // 陛老 技陛栏肺 叭腮 捣, 10父盔阑 逞阑 荐 绝促.
        // 荤合己狼 历厘篮 荤合己捞 乐绰 辑滚俊辑父 历厘等绊
        // 促弗 辑滚俊辑绰 佬扁父 茄促.
        //Constructor  Create()
        public TUserCastle()
        {
            OwnerGuild = null;
            CastleMap = "3";
            CastleStartX = 644;
            CastleStartY = 290;
            CastleName = "沙巴克";
            CastlePEnvir = null;
            CoreCastlePDoorCore = null;
            BoCastleWarChecked = false;
            // 概老 20矫俊 眉农 咯何
            BoCastleUnderAttack = false;
            BoCastleWarTimeOut10min = false;
            BoCastleWarTimeOutRemainMinute = 0;
            AttackerList = new ArrayList();
            RushGuildList = new ArrayList();
            SaveCastleGoldTime = GetTickCount;
        }
        //@ Destructor  Destroy()
        ~TUserCastle()
        {
            AttackerList.Free();
            RushGuildList.Free();
            base.Destroy();
        }
        public void Initialize()
        {
            int i;
            TDoorInfo pd;
            LoadFromFile(Castle.CASTLEFILENAME);
            LoadAttackerList();
            // 傍己傈捞 利侩登绰 辑滚俊父 利侩, (荤合己狼 辑滚俊辑父)
            if (svMain.ServerIndex != svMain.GrobalEnvir.GetServer(CastleMapName))
            {
                return;
            }
            CorePEnvir = svMain.GrobalEnvir.GetEnvir(Castle.CASTLECOREMAP);
            if (CorePEnvir == null)
            {
                MessageBox.Show(Castle.CASTLECOREMAP + " No map found. ( No inner wall map of wall conquest war )");
            }
            BasementEnvir = svMain.GrobalEnvir.GetEnvir(Castle.CASTLEBASEMAP);
            if (CorePEnvir == null)
            {
                MessageBox.Show(Castle.CASTLEBASEMAP + " - map not found !!");
            }
            // (*** 傍己傈捞 利侩登搁
            CastlePEnvir = svMain.GrobalEnvir.GetEnvir(CastleMapName);
            if (CastlePEnvir != null)
            {
                MainDoor.UnitObj = svMain.UserEngine.AddCreatureSysop(CastleMapName, MainDoor.X, MainDoor.Y, MainDoor.UnitName);
                if (MainDoor.UnitObj != null)
                {
                    MainDoor.UnitObj.WAbil.HP = (ushort)MainDoor.HP;
                    ((TGuardUnit)MainDoor.UnitObj).Castle = this;
                    if (MainDoor.BoDoorOpen)
                    {
                        ((TCastleDoor)MainDoor.UnitObj).OpenDoor();
                    }
                }
                else
                {
                    MessageBox.Show("[Error] UserCastle.Initialize MainDoor.UnitObj = nil");
                }
                LeftWall.UnitObj = svMain.UserEngine.AddCreatureSysop(CastleMapName, LeftWall.X, LeftWall.Y, LeftWall.UnitName);
                if (LeftWall.UnitObj != null)
                {
                    LeftWall.UnitObj.WAbil.HP = (ushort)LeftWall.HP;
                    ((TGuardUnit)LeftWall.UnitObj).Castle = this;
                }
                else
                {
                    MessageBox.Show("[Error] UserCastle.Initialize LeftWall.UnitObj = nil");
                }
                CenterWall.UnitObj = svMain.UserEngine.AddCreatureSysop(CastleMapName, CenterWall.X, CenterWall.Y, CenterWall.UnitName);
                if (CenterWall.UnitObj != null)
                {
                    CenterWall.UnitObj.WAbil.HP = (ushort)CenterWall.HP;
                    ((TGuardUnit)CenterWall.UnitObj).Castle = this;
                }
                else
                {
                    MessageBox.Show("[Error] UserCastle.Initialize CenterWall.UnitObj = nil");
                }
                RightWall.UnitObj = svMain.UserEngine.AddCreatureSysop(CastleMapName, RightWall.X, RightWall.Y, RightWall.UnitName);
                if (RightWall.UnitObj != null)
                {
                    RightWall.UnitObj.WAbil.HP = (ushort)RightWall.HP;
                    ((TGuardUnit)RightWall.UnitObj).Castle = this;
                }
                else
                {
                    MessageBox.Show("[Error] UserCastle.Initialize RightWall.UnitObj = nil");
                }
                for (i = 0; i < Castle.MAXARCHER; i++)
                {
                    if (Archers[i].HP > 0)
                    {
                        Archers[i].UnitObj = svMain.UserEngine.AddCreatureSysop(CastleMapName, Archers[i].X, Archers[i].Y, Archers[i].UnitName);
                        if (Archers[i].UnitObj != null)
                        {
                            ((TGuardUnit)Archers[i].UnitObj).Castle = this;
                            Archers[i].UnitObj.WAbil.HP = (ushort)Archers[i].HP;
                            ((TGuardUnit)Archers[i].UnitObj).OriginX = Archers[i].X;
                            ((TGuardUnit)Archers[i].UnitObj).OriginY = Archers[i].Y;
                            ((TGuardUnit)Archers[i].UnitObj).OriginDir = 3;
                        }
                        else
                        {
                            MessageBox.Show("[Error] UserCastle.Initialize Archer -> UnitObj = nil");
                        }
                    }
                }
                for (i = 0; i < Castle.MAXGUARD; i++)
                {
                    if (Guards[i].HP > 0)
                    {
                        Guards[i].UnitObj = svMain.UserEngine.AddCreatureSysop(CastleMapName, Guards[i].X, Guards[i].Y, Guards[i].UnitName);
                        if (Guards[i].UnitObj != null)
                        {
                            Guards[i].UnitObj.WAbil.HP = (ushort)Guards[i].HP;
                            // TGuardUnit(UnitObj).OriginX := X;
                            // TGuardUnit(UnitObj).OriginY := Y;
                            // TGuardUnit(UnitObj).OriginDir := 3;
                        }
                        else
                        {
                            MessageBox.Show("[Error] UserCastle.Initialize Archer -> UnitObj = nil");
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("<Critical Error> UserCastle : [Defense]->CastleMap is invalid value");
            }
            // 荤合己狼 郴己巩
            TEnvirList _wvar1 = svMain.GrobalEnvir;
            for (i = 0; i < CastlePEnvir.DoorList.Count; i++)
            {
                pd = CastlePEnvir.DoorList[i];
                if ((Math.Abs(pd.DoorX - Castle.COREDOORX) <= 3) && (Math.Abs(pd.DoorY - Castle.COREDOORY) <= 3))
                {
                    CoreCastlePDoorCore = pd.PCore;
                }
            }
            // *)

        }

        // 10檬俊 茄锅
        public void SaveAll()
        {
            SaveToFile(Castle.CASTLEFILENAME);
            // 傍己傈 辑滚俊辑父 历厘凳

        }

        // AttackerList
        private void SaveAttackerList()
        {
            int i;
            ArrayList strlist;
            string flname;
            flname = svMain.CastleDir + Castle.CASTLEATTACERS;
            strlist = new ArrayList();
            for (i = 0; i < AttackerList.Count; i++)
            {
                strlist.Add((AttackerList[i] as TAttackerInfo).GuildName + "       \"" + (AttackerList[i] as TAttackerInfo).AttackDate.ToString() + "\"");
            }
            try
            {
                strlist.SaveToFile(flname);
            }
            catch
            {
                svMain.MainOutMessage(flname + "保存错误...");
            }
            strlist.Free();
        }

        // AttackerList
        public void LoadAttackerList()
        {
            int i;
            ArrayList strlist;
            TAttackerInfo pattack;
            TGuild aguild;
            string flname;
            string gname;
            string adate;
            flname = svMain.CastleDir + Castle.CASTLEATTACERS;
            if (!File.Exists(flname))
            {
                return;
            }
            strlist = new ArrayList();
            try
            {
                strlist.LoadFromFile(flname);
                for (i = 0; i < AttackerList.Count; i++)
                {
                    Dispose(AttackerList[i] as TAttackerInfo);
                }
                AttackerList.Clear();
                for (i = 0; i < strlist.Count; i++)
                {
                    adate = GetValidStr3(strlist[i], gname, new string[] { " ", "\09" });
                    aguild = svMain.GuildMan.GetGuild(gname);
                    if (aguild != null)
                    {
                        pattack = new TAttackerInfo();
                        ArrestStringEx(adate, "\"", "\"", adate);
                        try
                        {
                            pattack.AttackDate = Convert.ToDateTime(adate);
                        }
                        catch
                        {
                            pattack.AttackDate = DateTime.Today;
                        }
                        pattack.GuildName = gname;
                        pattack.Guild = aguild;
                        AttackerList.Add(pattack);
                    }
                }
            }
            catch
            {
                svMain.MainOutMessage(flname + " 读取失败...");
            }
            strlist.Free();
        }

        private void SaveToFile(string flname)
        {
            int i;
            FileStream ini;
            // 荤合己捞 乐绰 辑滚俊辑父 历厘阑 茄促.
            if (svMain.ServerIndex == svMain.GrobalEnvir.GetServer(CastleMapName))
            {
                ini = new FileStream(svMain.CastleDir + flname);
                if (ini != null)
                {
                    ini.WriteString("setup", "CastleName", CastleName);
                    ini.WriteString("setup", "OwnGuild", OwnerGuildName);
                    ini.WriteDateTime("setup", "ChangeDate", LatestOwnerChangeDateTime);
                    ini.WriteDateTime("setup", "WarDate", LatestWarDateTime);
                    ini.WriteDateTime("setup", "IncomeToday", IncomeToday);
                    ini.WriteInteger("setup", "TotalGold", TotalGold);
                    ini.WriteInteger("setup", "TodayIncome", TodayIncome);
                    // 己巩, 己寒...
                    if (MainDoor.UnitObj != null)
                    {
                        ini.WriteBool("defense", "MainDoorOpen", ((TCastleDoor)MainDoor.UnitObj).BoOpenState);
                        ini.WriteInteger("defense", "MainDoorHP", ((TCastleDoor)MainDoor.UnitObj).WAbil.HP);
                    }
                    if (LeftWall.UnitObj != null)
                    {
                        ini.WriteInteger("defense", "LeftWallHP", ((TCastleDoor)LeftWall.UnitObj).WAbil.HP);
                    }
                    if (CenterWall.UnitObj != null)
                    {
                        ini.WriteInteger("defense", "CenterWallHP", ((TCastleDoor)CenterWall.UnitObj).WAbil.HP);
                    }
                    if (RightWall.UnitObj != null)
                    {
                        ini.WriteInteger("defense", "RightWallHP", ((TCastleDoor)RightWall.UnitObj).WAbil.HP);
                    }
                    for (i = 0; i < Castle.MAXARCHER; i++)
                    {
                        ini.WriteInteger("defense", "Archer_" + (i + 1).ToString() + "_X", Archers[i].X);
                        ini.WriteInteger("defense", "Archer_" + (i + 1).ToString() + "_Y", Archers[i].Y);
                        if (Archers[i].UnitObj != null)
                        {
                            ini.WriteInteger("defense", "Archer_" + (i + 1).ToString() + "_HP", ((TArcherGuard)Archers[i].UnitObj).WAbil.HP);
                        }
                        else
                        {
                            ini.WriteInteger("defense", "Archer_" + (i + 1).ToString() + "_HP", 0);
                        }
                    }
                    for (i = 0; i < Castle.MAXGUARD; i++)
                    {
                        ini.WriteInteger("defense", "Guard_" + (i + 1).ToString() + "_X", Guards[i].X);
                        ini.WriteInteger("defense", "Guard_" + (i + 1).ToString() + "_Y", Guards[i].Y);
                        if (Guards[i].UnitObj != null)
                        {
                            ini.WriteInteger("defense", "Guard_" + (i + 1).ToString() + "_HP", ((TGuardUnit)Guards[i].UnitObj).WAbil.HP);
                        }
                        else
                        {
                            ini.WriteInteger("defense", "Guard_" + (i + 1).ToString() + "_HP", Guards[i].HP);
                        }
                    }
                    ini.Free();
                }
            }
        }

        private void LoadFromFile(string flname)
        {
            int i;
            FileStream ini;
            ini = new FileStream(svMain.CastleDir + flname);
            if (ini != null)
            {
                CastleName = ini.ReadString("setup", "CastleName", "SabukWall");
                OwnerGuildName = ini.ReadString("setup", "OwnGuild", "");
                LatestOwnerChangeDateTime = ini.ReadDateTime("setup", "ChangeDate", DateTime.Now);
                LatestWarDateTime = ini.ReadDateTime("setup", "WarDate", DateTime.Now);
                IncomeToday = ini.ReadDateTime("setup", "IncomeToday", DateTime.Now);
                TotalGold = ini.ReadInteger("setup", "TotalGold", 0);
                TodayIncome = ini.ReadInteger("setup", "TodayIncome", 0);
                CastleMapName = ini.ReadString("defense", "CastleMap", "3");
                MainDoor.X = ini.ReadInteger("defense", "MainDoorX", 0);
                MainDoor.Y = ini.ReadInteger("defense", "MainDoorY", 0);
                MainDoor.UnitName = ini.ReadString("defense", "MainDoorName", "");
                MainDoor.BoDoorOpen = ini.ReadBool("defense", "MainDoorOpen", true);
                MainDoor.HP = ini.ReadInteger("defense", "MainDoorHP", 2000);
                MainDoor.UnitObj = null;
                LeftWall.X = ini.ReadInteger("defense", "LeftWallX", 0);
                LeftWall.Y = ini.ReadInteger("defense", "LeftWallY", 0);
                LeftWall.UnitName = ini.ReadString("defense", "LeftWallName", "");
                LeftWall.HP = ini.ReadInteger("defense", "LeftWallHP", 2000);
                LeftWall.UnitObj = null;
                CenterWall.X = ini.ReadInteger("defense", "CenterWallX", 0);
                CenterWall.Y = ini.ReadInteger("defense", "CenterWallY", 0);
                CenterWall.UnitName = ini.ReadString("defense", "CenterWallName", "");
                CenterWall.HP = ini.ReadInteger("defense", "CenterWallHP", 2000);
                CenterWall.UnitObj = null;
                RightWall.X = ini.ReadInteger("defense", "RightWallX", 0);
                RightWall.Y = ini.ReadInteger("defense", "RightWallY", 0);
                RightWall.UnitName = ini.ReadString("defense", "RightWallName", "");
                RightWall.HP = ini.ReadInteger("defense", "RightWallHP", 2000);
                RightWall.UnitObj = null;
                for (i = 0; i < Castle.MAXARCHER; i++)
                {
                    Archers[i].X = ini.ReadInteger("defense", "Archer_" + (i + 1).ToString() + "_X", 0);
                    Archers[i].Y = ini.ReadInteger("defense", "Archer_" + (i + 1).ToString() + "_Y", 0);
                    Archers[i].HP = ini.ReadInteger("defense", "Archer_" + (i + 1).ToString() + "_HP", 0);
                    Archers[i].UnitName = "Archer";
                    Archers[i].UnitObj = null;
                }
                for (i = 0; i < Castle.MAXGUARD; i++)
                {
                    Guards[i].X = ini.ReadInteger("defense", "Guard_" + (i + 1).ToString() + "_X", 0);
                    Guards[i].Y = ini.ReadInteger("defense", "Guard_" + (i + 1).ToString() + "_Y", 0);
                    Guards[i].HP = ini.ReadInteger("defense", "Guard_" + (i + 1).ToString() + "_HP", 0);
                    Guards[i].UnitName = "Guard";
                    Guards[i].UnitObj = null;
                }
                ini.Free();
            }
            OwnerGuild = svMain.GuildMan.GetGuild(OwnerGuildName);
        }

        public void Run()
        {
            int i;
            string str;
            string strRemainMinutes;
            long RemainMinutes;
            if (svMain.ServerIndex != svMain.GrobalEnvir.GetServer(CastleMapName))
            {
                return;
            }
           var ayear = DateTime.Today.Year;
           var amon = DateTime.Today.Month;
           var aday = DateTime.Today.Day;
           var ayear2 = IncomeToday.Year;
           var amon2 = IncomeToday.Month;
           var aday2 = IncomeToday.Day;
            // 促澜朝肺 逞绢啊搁 坷疵狼 荐劳篮 檬扁拳 矫糯
            if ((ayear != ayear2) || (amon != amon2) || (aday != aday2))
            {
                TodayIncome = 0;
                IncomeToday = DateTime.Now;
                BoCastleWarChecked = false;
            }
            // 概老 坷饶 8矫付促 傍己傈 矫累阑 犬牢茄促.
            if (!BoCastleWarChecked && !BoCastleUnderAttack)
            {
                var ahour = DateTime.Now.Hour;
                var amin = DateTime.Now.Minute;
                var asec = DateTime.Now.Second;
                var amsec = DateTime.Now.Millisecond;
                // if amin = 0 then begin  //概 矫 沥阿俊
                if (ahour == 20)
                {
                    // 坷饶8矫
                    BoCastleWarChecked = true;
                    // 茄锅父 八荤窃
                    RushGuildList.Clear();
                    // 傍拜磊 府胶飘甫 八荤
                    for (i = AttackerList.Count - 1; i >= 0; i--)
                    {
                        ayear2 = (AttackerList[i] as TAttackerInfo).AttackDate.Year;
                        amon2 = (AttackerList[i] as TAttackerInfo).AttackDate.Month;
                        aday2 = (AttackerList[i] as TAttackerInfo).AttackDate.Day;
                        if ((ayear == ayear2) && (amon == amon2) && (aday == aday2))
                        {
                            // 傍己傈 矫累
                            BoCastleUnderAttack = true;
                            BoCastleWarTimeOut10min = false;
                            LatestWarDateTime = DateTime.Now;
                            CastleAttackStarted = GetTickCount;
                            RushGuildList.Add((AttackerList[i] as TAttackerInfo).Guild);
                            Dispose(AttackerList[i] as TAttackerInfo);
                            // 皋葛府秦力
                            AttackerList.RemoveAt(i);
                        }
                    }
                    // 傍己傈狼 矫累阑 舅赴促.
                    if (BoCastleUnderAttack)
                    {
                        RushGuildList.Add(OwnerGuild);
                        // 规绢巩档 磊悼栏肺 傍拜巩栏肺 甸绢皑
                        StartCastleWar();
                        SaveAttackerList();
                        svMain.UserEngine.SendInterMsg(Grobal2.ISM_RELOADCASTLEINFO, svMain.ServerIndex, "");
                        // 傈辑滚狼 傈澜栏肺 傍瘤啊 唱埃促.
                        str = "[Sabuk wall conquest war started.]";
                        svMain.UserEngine.SysMsgAll(str);
                        svMain.UserEngine.SendInterMsg(Grobal2.ISM_SYSOPMSG, svMain.ServerIndex, str);
                        ActivateMainDoor(true);
                        // 磊悼栏肺 己巩捞 摧塞
                    }
                }
            }
            // 磷篮 版厚绰 猾促.
            for (i = 0; i < Castle.MAXGUARD; i++)
            {
                if (Guards[i].UnitObj != null)
                {
                    if (Guards[i].UnitObj.BoGhost)
                    {
                        Guards[i].UnitObj = null;
                    }
                }
            }
            for (i = 0; i < Castle.MAXARCHER; i++)
            {
                if (Archers[i].UnitObj != null)
                {
                    if (Archers[i].UnitObj.BoGhost)
                    {
                        Archers[i].UnitObj = null;
                    }
                }
            }
            // 傍己傈 吝牢 版快, 傍己傈 矫累饶 3矫埃捞 瘤唱搁 傍己傈捞 辆丰等促.
            if (BoCastleUnderAttack)
            {
                LeftWall.UnitObj.BoStoneMode = false;
                CenterWall.UnitObj.BoStoneMode = false;
                RightWall.UnitObj.BoStoneMode = false;
                if (!BoCastleWarTimeOut10min)
                {
                    if (GetTickCount - CastleAttackStarted > (3 * 60 * 60 * 1000 - (10 * 60 * 1000)))
                    {
                        // 10盒傈
                        BoCastleWarTimeOut10min = true;
                        BoCastleWarTimeOutRemainMinute = 10;
                        // 傈辑滚狼 傈澜栏肺 傍瘤啊 唱埃促.
                        str = "[离沙巴克攻城战结束还有十分钟]";
                        svMain.UserEngine.SysMsgAll(str);
                        svMain.UserEngine.SendInterMsg(Grobal2.ISM_SYSOPMSG, svMain.ServerIndex, str);
                    }
                }
                else if (BoCastleWarTimeOutRemainMinute > 0)
                {
                    strRemainMinutes = "";
                    RemainMinutes = (3 * 60 * 60 * 1000) - (GetTickCount - CastleAttackStarted);
                    if ((RemainMinutes > 9 * 60 * 1000 - 5 * 1000) && (RemainMinutes < 9 * 60 * 1000 + 5 * 1000))
                    {
                        strRemainMinutes = "9";
                        BoCastleWarTimeOutRemainMinute = 9;
                    }
                    else if ((RemainMinutes > 8 * 60 * 1000 - 5 * 1000) && (RemainMinutes < 8 * 60 * 1000 + 5 * 1000))
                    {
                        strRemainMinutes = "8";
                        BoCastleWarTimeOutRemainMinute = 8;
                    }
                    else if ((RemainMinutes > 7 * 60 * 1000 - 5 * 1000) && (RemainMinutes < 7 * 60 * 1000 + 5 * 1000))
                    {
                        strRemainMinutes = "7";
                        BoCastleWarTimeOutRemainMinute = 7;
                    }
                    else if ((RemainMinutes > 6 * 60 * 1000 - 5 * 1000) && (RemainMinutes < 6 * 60 * 1000 + 5 * 1000))
                    {
                        strRemainMinutes = "6";
                        BoCastleWarTimeOutRemainMinute = 6;
                    }
                    else if ((RemainMinutes > 5 * 60 * 1000 - 5 * 1000) && (RemainMinutes < 5 * 60 * 1000 + 5 * 1000))
                    {
                        strRemainMinutes = "5";
                        BoCastleWarTimeOutRemainMinute = 5;
                    }
                    else if ((RemainMinutes > 4 * 60 * 1000 - 5 * 1000) && (RemainMinutes < 4 * 60 * 1000 + 5 * 1000))
                    {
                        strRemainMinutes = "4";
                        BoCastleWarTimeOutRemainMinute = 4;
                    }
                    else if ((RemainMinutes > 3 * 60 * 1000 - 5 * 1000) && (RemainMinutes < 3 * 60 * 1000 + 5 * 1000))
                    {
                        strRemainMinutes = "3";
                        BoCastleWarTimeOutRemainMinute = 3;
                    }
                    else if ((RemainMinutes > 2 * 60 * 1000 - 5 * 1000) && (RemainMinutes < 2 * 60 * 1000 + 5 * 1000))
                    {
                        strRemainMinutes = "2";
                        BoCastleWarTimeOutRemainMinute = 2;
                    }
                    else if ((RemainMinutes > 1 * 60 * 1000 - 5 * 1000) && (RemainMinutes < 1 * 60 * 1000 + 5 * 1000))
                    {
                        strRemainMinutes = "1";
                        BoCastleWarTimeOutRemainMinute = 1;
                    }
                    else if (RemainMinutes <= 1 * 60 * 1000 - 5 * 1000)
                    {
                        strRemainMinutes = "";
                        BoCastleWarTimeOutRemainMinute = 0;
                    }
                    else
                    {
                        strRemainMinutes = "";
                    }
                    if (strRemainMinutes != "")
                    {
                        str = "[离沙巴克攻城战结还剩下" + strRemainMinutes + "分钟]";
                        // debug code
                        svMain.MainOutMessage(str);
                        // sonmg test
                        svMain.UserEngine.SysMsgAll(str);
                        svMain.UserEngine.SendInterMsg(Grobal2.ISM_SYSOPMSG, svMain.ServerIndex, str);
                    }
                }
                if (GetTickCount - CastleAttackStarted > 3 * 60 * 60 * 1000)
                {
                    // 鸥烙酒眶等 版快, 傍己傈篮 场巢.
                    FinishCastleWar();
                }
            }
            else
            {
                LeftWall.UnitObj.BoStoneMode = true;
                CenterWall.UnitObj.BoStoneMode = true;
                RightWall.UnitObj.BoStoneMode = true;
            }
        }

        public string GetCastleStartMap()
        {
            string result;
            result = CastleMapName;
            return result;
        }

        public short GetCastleStartX()
        {
            return (short)(CastleStartX - 4 + new System.Random(9).Next());
        }

        public short GetCastleStartY()
        {
            return (short)(CastleStartY - 4 + new System.Random(9).Next());
        }

        public bool CanEnteranceCoreCastle(int xx, int yy, TUserHuman hum)
        {
            bool result;
            result = IsOurCastle((TGuild)hum.MyGuild);
            if (!result)
            {
                if (LeftWall.UnitObj != null)
                {
                    if (LeftWall.UnitObj.Death)
                    {
                        if ((LeftWall.UnitObj.CX == xx) && (LeftWall.UnitObj.CY == yy))
                        {
                            result = true;
                        }
                    }
                }
                if (CenterWall.UnitObj != null)
                {
                    if (CenterWall.UnitObj.Death)
                    {
                        if ((CenterWall.UnitObj.CX == xx) && (CenterWall.UnitObj.CY == yy))
                        {
                            result = true;
                        }
                    }
                }
                if (RightWall.UnitObj != null)
                {
                    if (RightWall.UnitObj.Death)
                    {
                        if ((RightWall.UnitObj.CX == xx) && (RightWall.UnitObj.CY == yy))
                        {
                            result = true;
                        }
                    }
                }
            }
            return result;
        }

        public bool IsOurCastle(TGuild g)
        {
            bool result;
            if (g == null)
            {
                result = false;
                return result;
            }
            result = (svMain.UserCastle.OwnerGuild == g) && (svMain.UserCastle.OwnerGuild != null);
            return result;
        }

        public bool IsCastleMember(TUserHuman hum)
        {
            bool result;
            int i;
            result = false;
            if (hum.MyGuild == null)
            {
                return result;
            }
            if (IsOurCastle((TGuild)hum.MyGuild))
            {
                result = true;
                return result;
            }
            for (i = 0; i < ((TGuild)hum.MyGuild).AllyGuilds.Count; i++)
            {
                if (IsOurCastle((TGuild)((TGuild)hum.MyGuild).AllyGuilds.Values[i]))
                {
                    result = true;
                    return result;
                }
            }
            return result;
        }

        public void ActivateDefeseUnits(bool active)
        {
            if (MainDoor.UnitObj != null)
            {
                MainDoor.UnitObj.HideMode = active;
            }
            if (LeftWall.UnitObj != null)
            {
                LeftWall.UnitObj.HideMode = active;
            }
            if (CenterWall.UnitObj != null)
            {
                CenterWall.UnitObj.HideMode = active;
            }
            if (RightWall.UnitObj != null)
            {
                RightWall.UnitObj.HideMode = active;
            }
        }

        public void ActivateMainDoor(bool active)
        {
            // MainDoor.UnitObj.HideMode := active;
            if (MainDoor.UnitObj != null)
            {
                if (!MainDoor.UnitObj.Death)
                {
                    if (active)
                    {
                        if (((TCastleDoor)MainDoor.UnitObj).BoOpenState)
                        {
                            ((TCastleDoor)MainDoor.UnitObj).CloseDoor();
                        }
                    }
                    else
                    {
                        if (!((TCastleDoor)MainDoor.UnitObj).BoOpenState)
                        {
                            ((TCastleDoor)MainDoor.UnitObj).OpenDoor();
                        }
                    }
                }
            }
        }

        // 荤合己救狼 惑痢俊辑 拱扒阑 荤绊 迫 锭 付促 技陛捞 嘿绰促.
        public void PayTax(int goodsprice)
        {
            int tax;
            // 2003/07/15 荤合 技陛 惑氢 炼例 0.05 -> 0.10
            tax = HUtil32.MathRound(goodsprice * 0.1);
            // 技陛篮 5%肺 炼沥   0.05
            if (TodayIncome + tax <= Castle.TODAYGOLD)
            {
                TodayIncome = TodayIncome + tax;
            }
            else
            {
                if (TodayIncome >= Castle.TODAYGOLD)
                {
                    tax = 0;
                }
                else
                {
                    tax = Castle.TODAYGOLD - TodayIncome;
                    TodayIncome = Castle.TODAYGOLD;
                }
            }
            if (tax > 0)
            {
                if ((long)TotalGold + tax <= Castle.CASTLEMAXGOLD)
                {
                    TotalGold = TotalGold + tax;
                }
                else
                {
                    TotalGold = Castle.CASTLEMAXGOLD;
                }
            }
            if (GetTickCount - SaveCastleGoldTime > 10 * 60 * 1000)
            {
                SaveCastleGoldTime = GetTickCount;
                // 己捣逞_
                // '陛傈'
                svMain.AddUserLog("23\09" + "0\09" + "0\09" + "0\09" + "Autosaving\09" + Envir.NAME_OF_GOLD + "\09" + TotalGold.ToString() + "\09" + "0\09" + "0");
            }
        }

        // 巩林啊 己狼 捣阑 猾促.
        // -1: 巩林啊 酒丛
        // -2: 弊父怒 捣捞 绝澜
        // -3: 茫绰捞啊 捣阑 歹 捞惑 甸 荐 绝澜
        // 1 : 己傍
        public int GetBackCastleGold(TUserHuman hum, int howmuch)
        {
            int result;
            result = -1;
            if ((hum.MyGuild == svMain.UserCastle.OwnerGuild) && (hum.GuildRank == 1))
            {
                if (howmuch <= TotalGold)
                {
                    if (hum.Gold + howmuch <= hum.AvailableGold)
                    {
                        TotalGold = TotalGold - howmuch;
                        hum.IncGold(howmuch);
                        // 肺弊巢辫
                        // 己捣画_
                        // '陛傈'
                        svMain.AddUserLog("22\09" + hum.MapName + "\09" + hum.CX.ToString() + "\09" + hum.CY.ToString() + "\09" + hum.UserName + "\09" + Envir.NAME_OF_GOLD + "\09" + howmuch.ToString() + "\09" + "1\09" + "0");
                        hum.GoldChanged();
                        result = 1;
                    }
                    else
                    {
                        result = -3;
                    }
                }
                else
                {
                    result = -2;
                }
            }
            return result;
        }

        // 巩林啊 己狼 捣阑 猾促.
        // 巩林啊 己俊 捣阑 持澜
        // -1: 巩林啊 酒丛
        // -2: 弊父怒 捣捞 绝澜
        // -3: 茫绰捞啊 捣阑 歹 捞惑 甸 荐 绝澜
        public int TakeInCastleGold(TUserHuman hum, int howmuch)
        {
            int result;
            result = -1;
            if ((hum.MyGuild == svMain.UserCastle.OwnerGuild) && (hum.GuildRank == 1))
            {
                if (howmuch <= hum.Gold)
                {
                    if ((long)howmuch + TotalGold <= Castle.CASTLEMAXGOLD)
                    {
                        hum.DecGold(howmuch);
                        TotalGold = TotalGold + howmuch;
                        // 肺弊巢辫
                        // 己捣逞_
                        // '陛傈'
                        svMain.AddUserLog("23\09" + hum.MapName + "\09" + hum.CX.ToString() + "\09" + hum.CY.ToString() + "\09" + hum.UserName + "\09" + Envir.NAME_OF_GOLD + "\09" + howmuch.ToString() + "\09" + "0\09" + "0");
                        hum.GoldChanged();
                        result = 1;
                    }
                    else
                    {
                        result = -3;
                    }
                }
                else
                {
                    result = -2;
                }
            }
            return result;
        }

        // 巩林啊 己俊 捣阑 持澜
        // 己巩阑 绊模促.
        public bool RepairCastleDoor()
        {
            bool result;
            result = false;
            if ((MainDoor.UnitObj != null) && !BoCastleUnderAttack)
            {
                if (MainDoor.UnitObj.WAbil.HP < MainDoor.UnitObj.WAbil.MaxHP)
                {
                    if (!MainDoor.UnitObj.Death)
                    {
                        // 付瘤阜阑 嘎篮 10盒捞 瘤唱搁 绊磨 荐 乐促.
                        if (GetTickCount - ((TCastleDoor)MainDoor.UnitObj).StruckTime > 1 * 60 * 1000)
                        {
                            MainDoor.UnitObj.WAbil.HP = MainDoor.UnitObj.WAbil.MaxHP;
                            ((TCastleDoor)MainDoor.UnitObj).RepairStructure();
                            // 货肺款 葛嚼阑 焊辰促.
                            result = true;
                        }
                    }
                    else
                    {
                        // 肯颇等 版快俊绰 1矫埃 饶俊 绊磨 荐 乐澜
                        if (GetTickCount - ((TCastleDoor)MainDoor.UnitObj).BrokenTime > 1 * 60 * 1000)
                        {
                            MainDoor.UnitObj.WAbil.HP = MainDoor.UnitObj.WAbil.MaxHP;
                            MainDoor.UnitObj.Death = false;
                            ((TCastleDoor)MainDoor.UnitObj).BoOpenState = false;
                            ((TCastleDoor)MainDoor.UnitObj).RepairStructure();
                            // 货肺款 葛嚼阑 焊辰促.
                            result = true;
                        }
                    }
                }
            }
            return result;
        }

        // 己寒阑 绊模促.
        public int RepairCoreCastleWall(int wallnum)
        {
            int result;
            TWallStructure wall;
            result = 0;
            switch (wallnum)
            {
                case 1:
                    wall = (TWallStructure)LeftWall.UnitObj;
                    break;
                case 2:
                    wall = (TWallStructure)CenterWall.UnitObj;
                    break;
                case 3:
                    wall = (TWallStructure)RightWall.UnitObj;
                    break;
                default:
                    return result;
                    break;
            }
            if ((wall != null) && !BoCastleUnderAttack)
            {
                if (wall.WAbil.HP < wall.WAbil.MaxHP)
                {
                    if (!wall.Death)
                    {
                        // 付瘤阜阑 嘎篮 10盒捞 瘤唱搁 绊磨 荐 乐促.
                        if (GetTickCount - wall.StruckTime > 1 * 60 * 1000)
                        {
                            wall.WAbil.HP = wall.WAbil.MaxHP;
                            wall.RepairStructure();
                            // 货肺款 葛嚼阑 焊辰促.
                            result = 1;
                        }
                    }
                    else
                    {
                        // 肯颇等 版快俊绰 1矫埃 饶俊 绊磨 荐 乐澜
                        if (GetTickCount - wall.BrokenTime > 1 * 60 * 1000)
                        {
                            wall.WAbil.HP = wall.WAbil.MaxHP;
                            wall.Death = false;
                            wall.RepairStructure();
                            // 货肺款 葛嚼阑 焊辰促.
                            result = 1;
                        }
                    }
                }
            }
            return result;
        }

        // 傍己傈 脚没 包访
        // 傍己傈 傍拜磊俊 包访
        public bool IsAttackGuild(TGuild aguild)
        {
            bool result;
            int i;
            result = false;
            for (i = 0; i < AttackerList.Count; i++)
            {
                if (aguild == (AttackerList[i] as TAttackerInfo).Guild)
                {
                    result = true;
                    break;
                }
            }
            return result;
        }

        // 傍己傈阑 瘤陛 脚没且 荐 乐绰瘤 咯何..
        public bool ProposeCastleWar(TGuild aguild)
        {
            bool result;
            TAttackerInfo pattack;
            result = false;
            if (!IsAttackGuild(aguild))
            {
                // 吝汗脚没篮 救凳
                pattack = new TAttackerInfo();
                pattack.AttackDate = CalcDay(DateTime.Today, 1 + 3);
                // 3老 饶
                pattack.GuildName = aguild.GuildName;
                pattack.Guild = aguild;
                AttackerList.Add(pattack);
                SaveAttackerList();
                svMain.UserEngine.SendInterMsg(Grobal2.ISM_RELOADCASTLEINFO, svMain.ServerIndex, "");
                result = true;
            }
            return result;
        }

        public string GetNextWarDateTimeStr()
        {
            string result;
            short ayear;
            short amon;
            short aday;
            result = "";
            if (AttackerList.Count > 0)
            {
                if (svMain.ENGLISHVERSION)
                {
                    // 寇惫绢 滚傈
                    result = (AttackerList[0] as TAttackerInfo).AttackDate.ToString();
                }
                else if (svMain.PHILIPPINEVERSION)
                {
                    // 鞘府巧 滚傈
                    result = (AttackerList[0] as TAttackerInfo).AttackDate.ToString();
                }
                else
                {
                    ayear = (AttackerList[0] as TAttackerInfo).AttackDate.Year;
                    amon = (AttackerList[0] as TAttackerInfo).AttackDate.Month;
                    aday = (AttackerList[0] as TAttackerInfo).AttackDate.Day;
                    // 2003/04/01 滚弊 荐沥
                    result = ayear.ToString() + "年" + amon.ToString() + "月" + aday.ToString() + "日";
                }
            }
            return result;
        }

        public string GetListOfWars()
        {
            string result;
            int i;
            int len;
            short y;
            short m;
            short d;
            short ayear;
            short amon;
            short aday;
            string str;
            result = "";
            ayear = 0;
            amon = 0;
            aday = 0;
            len = 0;
            for (i = 0; i < AttackerList.Count; i++)
            {
                y = (AttackerList[i] as TAttackerInfo).AttackDate.Year;
                m = (AttackerList[i] as TAttackerInfo).AttackDate.Month;
                d = (AttackerList[i] as TAttackerInfo).AttackDate.Day;
                if ((y != ayear) || (m != amon) || (d != aday))
                {
                    ayear = y;
                    amon = m;
                    aday = d;
                    if (result != "")
                    {
                        result = result + "\\";
                    }
                    if (svMain.ENGLISHVERSION)
                    {
                        // 寇惫绢 滚傈
                        result = result + (AttackerList[i] as TAttackerInfo).AttackDate.ToString() + "\\";
                    }
                    else if (svMain.PHILIPPINEVERSION)
                    {
                        // 鞘府巧 滚傈
                        result = result + (AttackerList[i] as TAttackerInfo).AttackDate.ToString() + "\\";
                    }
                    else
                    {
                        result = result + ayear.ToString() + "年" + amon.ToString() + "月" + aday.ToString() + "日\\";
                    }
                    len = 0;
                }
                if (len > 40)
                {
                    result = result + "\\";
                    len = 0;
                }
                str = "\"" + (AttackerList[i] as TAttackerInfo).GuildName + "\" ";
                len = len + str.Length;
                result = result + str;
            }
            return result;
        }

        public void StartCastleWar()
        {
            int i;
            ArrayList ulist;
            TUserHuman hum;
            ulist = new ArrayList();
            svMain.UserEngine.GetAreaUsers(CastlePEnvir, CastleStartX, CastleStartY, 100, ulist);
            for (i = 0; i < ulist.Count; i++)
            {
                hum = (TUserHuman)ulist[i];
                hum.UserNameChanged();
                // ChangeNameColor;
            }
            ulist.Free();
        }

        // 傍己傈 吝
        // 傍己傈 吝俊 乐澜..
        public bool IsCastleWarArea(TEnvirnoment penvir, int x, int y)
        {
            bool result;
            result = false;
            if (penvir == CastlePEnvir)
            {
                if ((Math.Abs(CastleStartX - x) < 100) && (Math.Abs(CastleStartY - y) < 100))
                {
                    result = true;
                }
            }
            if ((penvir == CorePEnvir) || (penvir == BasementEnvir))
            {
                result = true;
            }
            return result;
        }

        public bool IsRushCastleGuild(TGuild aguild)
        {
            bool result;
            int i;
            int j;
            result = false;
            if (aguild == null)
            {
                return result;
            }
            for (i = 0; i < RushGuildList.Count; i++)
            {
                if (RushGuildList[i] == aguild)
                {
                    result = true;
                    break;
                }
                for (j = 0; j < aguild.AllyGuilds.Count; j++)
                {
                    if (RushGuildList[i] == aguild.AllyGuilds.Values[j])
                    {
                        result = true;
                        return result;
                    }
                }
            }
            return result;
        }

        public int GetRushGuildCount()
        {
            int result;
            result = RushGuildList.Count;
            return result;
        }

        // 郴己俊辑 促弗 利阑 葛滴 郴卵篮 版快俊 铰府 炼扒捞 等促.
        public bool CheckCastleWarWinCondition(TGuild aguild)
        {
            bool result;
            int i;
            ArrayList ulist;
            bool flag;
            result = false;
            flag = false;
            if (GetTickCount - CastleAttackStarted > 10 * 60 * 1000)
            {
                // 傍己 矫累 10盒捞 瘤唱具 痢飞捞 啊瓷
                ulist = new ArrayList();
                svMain.UserEngine.GetAreaUsers(CorePEnvir, 0, 0, 1000, ulist);
                flag = true;
                for (i = 0; i < ulist.Count; i++)
                {
                    if ((!((TCreature)ulist[i]).Death) && (((TCreature)ulist[i]).MyGuild != aguild))
                    {
                        flag = false;
                        // 快府巩颇 捞寇狼 巩颇啊 尝绢 乐澜
                        break;
                    }
                }
                ulist.Free();
            }
            result = flag;
            return result;
        }

        public void ChangeCastleOwner(TGuild guild)
        {
            TGuild oldguild;
            string str;
            oldguild = OwnerGuild;
            OwnerGuild = guild;
            OwnerGuildName = guild.GuildName;
            LatestOwnerChangeDateTime = DateTime.Now;
            SaveToFile(Castle.CASTLEFILENAME);
            if (oldguild != null)
            {
                oldguild.MemberNameChanged();
            }
            if (OwnerGuild != null)
            {
                OwnerGuild.MemberNameChanged();
            }
            str = "(*)沙巴克已被\"" + OwnerGuildName + "\"占领！！";
            svMain.UserEngine.SysMsgAll(str);
            svMain.UserEngine.SendInterMsg(Grobal2.ISM_SYSOPMSG, svMain.ServerIndex, str);
            svMain.MainOutMessage("[沙巴克]" + OwnerGuildName + "占领");
        }

        public void FinishCastleWar()
        {
            int i;
            ArrayList ulist;
            TUserHuman hum;
            string str;
            BoCastleUnderAttack = false;
            RushGuildList.Clear();
            ulist = new ArrayList();
            svMain.UserEngine.GetAreaUsers(CastlePEnvir, CastleStartX, CastleStartY, 100, ulist);
            for (i = 0; i < ulist.Count; i++)
            {
                hum = (TUserHuman)ulist[i];
                hum.BoInFreePKArea = false;
                // hum.SendAreaState;
                // hum.UserNameChanged; //ChangeNameColor;
                if (hum.MyGuild != OwnerGuild)
                {
                    hum.RandomSpaceMove(hum.HomeMap, 0);
                }
            }
            ulist.Free();
            str = "[沙巴克攻城战已经结束]";
            svMain.UserEngine.SysMsgAll(str);
            svMain.UserEngine.SendInterMsg(Grobal2.ISM_SYSOPMSG, svMain.ServerIndex, str);
        }

    } // end TUserCastle

}

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
}