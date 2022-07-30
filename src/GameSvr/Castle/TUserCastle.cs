using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using SystemModule;
using SystemModule.Common;

namespace GameSvr
{
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
        public DateTime LatestWarDateTime;
        public bool BoCastleWarChecked = false;
        public bool BoCastleUnderAttack = false;
        public bool BoCastleWarTimeOut10min = false;
        public int BoCastleWarTimeOutRemainMinute = 0;
        public long CastleAttackStarted = 0;
        public long SaveCastleGoldTime = 0;
        public IList<TAttackerInfo> AttackerList = null;
        public ArrayList RushGuildList = null;
        public TDefenseUnit MainDoor = null;
        public TDefenseUnit LeftWall = null;
        public TDefenseUnit CenterWall = null;
        public TDefenseUnit RightWall = null;
        public TDefenseUnit[] Guards;
        public TDefenseUnit[] Archers;
        public DateTime IncomeToday;
        public int TotalGold = 0;
        public int TodayIncome = 0;

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
            BoCastleUnderAttack = false;
            BoCastleWarTimeOut10min = false;
            BoCastleWarTimeOutRemainMinute = 0;
            AttackerList = new List<TAttackerInfo>();
            RushGuildList = new ArrayList();
            SaveCastleGoldTime = HUtil32.GetTickCount();
        }

        ~TUserCastle()
        {
            AttackerList.Free();
            RushGuildList.Free();
        }
        
        public void Initialize()
        {
            int i;
            TDoorInfo pd;
            LoadFromFile(CastleDef.CASTLEFILENAME);
            LoadAttackerList();
            if (svMain.ServerIndex != svMain.GrobalEnvir.GetServer(CastleMapName))
            {
                return;
            }
            CorePEnvir = svMain.GrobalEnvir.GetEnvir(CastleDef.CASTLECOREMAP);
            if (CorePEnvir == null)
            {
                OutMainMessage(CastleDef.CASTLECOREMAP + " No map found. ( No inner wall map of wall conquest war )");
            }
            BasementEnvir = svMain.GrobalEnvir.GetEnvir(CastleDef.CASTLEBASEMAP);
            if (CorePEnvir == null)
            {
                OutMainMessage(CastleDef.CASTLEBASEMAP + " - map not found !!");
            }
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
                    OutMainMessage("[Error] UserCastle.Initialize MainDoor.UnitObj = nil");
                }
                LeftWall.UnitObj = svMain.UserEngine.AddCreatureSysop(CastleMapName, LeftWall.X, LeftWall.Y, LeftWall.UnitName);
                if (LeftWall.UnitObj != null)
                {
                    LeftWall.UnitObj.WAbil.HP = (ushort)LeftWall.HP;
                    ((TGuardUnit)LeftWall.UnitObj).Castle = this;
                }
                else
                {
                    OutMainMessage("[Error] UserCastle.Initialize LeftWall.UnitObj = nil");
                }
                CenterWall.UnitObj = svMain.UserEngine.AddCreatureSysop(CastleMapName, CenterWall.X, CenterWall.Y, CenterWall.UnitName);
                if (CenterWall.UnitObj != null)
                {
                    CenterWall.UnitObj.WAbil.HP = (ushort)CenterWall.HP;
                    ((TGuardUnit)CenterWall.UnitObj).Castle = this;
                }
                else
                {
                    OutMainMessage("[Error] UserCastle.Initialize CenterWall.UnitObj = nil");
                }
                RightWall.UnitObj = svMain.UserEngine.AddCreatureSysop(CastleMapName, RightWall.X, RightWall.Y, RightWall.UnitName);
                if (RightWall.UnitObj != null)
                {
                    RightWall.UnitObj.WAbil.HP = (ushort)RightWall.HP;
                    ((TGuardUnit)RightWall.UnitObj).Castle = this;
                }
                else
                {
                    OutMainMessage("[Error] UserCastle.Initialize RightWall.UnitObj = nil");
                }
                for (i = 0; i < CastleDef.MAXARCHER; i++)
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
                            OutMainMessage("[Error] UserCastle.Initialize Archer -> UnitObj = nil");
                        }
                    }
                }
                for (i = 0; i < CastleDef.MAXGUARD; i++)
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
                            OutMainMessage("[Error] UserCastle.Initialize Archer -> UnitObj = nil");
                        }
                    }
                }
            }
            else
            {
                OutMainMessage("<Critical Error> UserCastle : [Defense]->CastleMap is invalid value");
            }
            TEnvirList _wvar1 = svMain.GrobalEnvir;
            for (i = 0; i < CastlePEnvir.DoorList.Count; i++)
            {
                pd = CastlePEnvir.DoorList[i];
                if ((Math.Abs(pd.DoorX - CastleDef.COREDOORX) <= 3) && (Math.Abs(pd.DoorY - CastleDef.COREDOORY) <= 3))
                {
                    CoreCastlePDoorCore = pd.PCore;
                }
            }
        }

        public void OutMainMessage(string message)
        {
            Console.WriteLine(message);
        }

        public void SaveAll()
        {
            SaveToFile(CastleDef.CASTLEFILENAME);
        }

        private void SaveAttackerList()
        {
            var flname = svMain.CastleDir + CastleDef.CASTLEATTACERS;
            StringList strlist = new StringList();
            for (var i = 0; i < AttackerList.Count; i++)
            {
                strlist.Add(AttackerList[i].GuildName + "       \"" + AttackerList[i].AttackDate.ToString() + "\"");
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

        public void LoadAttackerList()
        {
            StringList strlist;
            TAttackerInfo pattack;
            TGuild aguild;
            string gname = string.Empty;
            string adate = string.Empty;
            string flname = svMain.CastleDir + CastleDef.CASTLEATTACERS;
            if (!File.Exists(flname))
            {
                return;
            }
            strlist = new StringList();
            try
            {
                strlist.LoadFromFile(flname);
                for (var i = 0; i < AttackerList.Count; i++)
                {
                    Dispose(AttackerList[i]);
                }
                AttackerList.Clear();
                for (var i = 0; i < strlist.Count; i++)
                {
                    adate = HUtil32.GetValidStr3(strlist[i], ref gname, new string[] { " ", "\09" });
                    aguild = svMain.GuildMan.GetGuild(gname);
                    if (aguild != null)
                    {
                        pattack = new TAttackerInfo();
                        HUtil32.ArrestStringEx(adate, "\"", "\"", ref adate);
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
          /*  FileStream ini;
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
                    for (var i = 0; i < Castle.MAXARCHER; i++)
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
                    for (var i = 0; i < Castle.MAXGUARD; i++)
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
            }*/
        }

        private void LoadFromFile(string flname)
        {
            /*int i;
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
            OwnerGuild = svMain.GuildMan.GetGuild(OwnerGuildName);*/
        }

        public void Run()
        {
            int i;
            string str = string.Empty;
            string strRemainMinutes = string.Empty;
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
            if (!BoCastleWarChecked && !BoCastleUnderAttack)
            {
                var ahour = DateTime.Now.Hour;
                var amin = DateTime.Now.Minute;
                var asec = DateTime.Now.Second;
                var amsec = DateTime.Now.Millisecond;
                if (ahour == 20)
                {
                    BoCastleWarChecked = true;
                    RushGuildList.Clear();
                    for (i = AttackerList.Count - 1; i >= 0; i--)
                    {
                        ayear2 = AttackerList[i].AttackDate.Year;
                        amon2 = AttackerList[i].AttackDate.Month;
                        aday2 = AttackerList[i].AttackDate.Day;
                        if ((ayear == ayear2) && (amon == amon2) && (aday == aday2))
                        {
                            BoCastleUnderAttack = true;
                            BoCastleWarTimeOut10min = false;
                            LatestWarDateTime = DateTime.Now;
                            CastleAttackStarted = HUtil32.GetTickCount();
                            RushGuildList.Add(AttackerList[i].Guild);
                            Dispose(AttackerList[i]);
                            AttackerList.RemoveAt(i);
                        }
                    }
                    if (BoCastleUnderAttack)
                    {
                        RushGuildList.Add(OwnerGuild);
                        StartCastleWar();
                        SaveAttackerList();
                        svMain.UserEngine.SendInterMsg(Grobal2.ISM_RELOADCASTLEINFO, svMain.ServerIndex, "");
                        str = "[Sabuk wall conquest war started.]";
                        svMain.UserEngine.SysMsgAll(str);
                        svMain.UserEngine.SendInterMsg(Grobal2.ISM_SYSOPMSG, svMain.ServerIndex, str);
                        ActivateMainDoor(true);
                    }
                }
            }
            for (i = 0; i < CastleDef.MAXGUARD; i++)
            {
                if (Guards[i].UnitObj != null)
                {
                    if (Guards[i].UnitObj.BoGhost)
                    {
                        Guards[i].UnitObj = null;
                    }
                }
            }
            for (i = 0; i < CastleDef.MAXARCHER; i++)
            {
                if (Archers[i].UnitObj != null)
                {
                    if (Archers[i].UnitObj.BoGhost)
                    {
                        Archers[i].UnitObj = null;
                    }
                }
            }
            if (BoCastleUnderAttack)
            {
                LeftWall.UnitObj.BoStoneMode = false;
                CenterWall.UnitObj.BoStoneMode = false;
                RightWall.UnitObj.BoStoneMode = false;
                if (!BoCastleWarTimeOut10min)
                {
                    if (HUtil32.GetTickCount() - CastleAttackStarted > (3 * 60 * 60 * 1000 - (10 * 60 * 1000)))
                    {
                        BoCastleWarTimeOut10min = true;
                        BoCastleWarTimeOutRemainMinute = 10;
                        str = "[离沙巴克攻城战结束还有十分钟]";
                        svMain.UserEngine.SysMsgAll(str);
                        svMain.UserEngine.SendInterMsg(Grobal2.ISM_SYSOPMSG, svMain.ServerIndex, str);
                    }
                }
                else if (BoCastleWarTimeOutRemainMinute > 0)
                {
                    strRemainMinutes = "";
                    RemainMinutes = (3 * 60 * 60 * 1000) - (HUtil32.GetTickCount() - CastleAttackStarted);
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
                        svMain.MainOutMessage(str);
                        svMain.UserEngine.SysMsgAll(str);
                        svMain.UserEngine.SendInterMsg(Grobal2.ISM_SYSOPMSG, svMain.ServerIndex, str);
                    }
                }
                if (HUtil32.GetTickCount() - CastleAttackStarted > 3 * 60 * 60 * 1000)
                {
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
            return CastleMapName;
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
            bool result = IsOurCastle(hum.MyGuild);
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
            if (IsOurCastle(hum.MyGuild))
            {
                result = true;
                return result;
            }
            for (i = 0; i < hum.MyGuild.AllyGuilds.Count; i++)
            {
                if (IsOurCastle(hum.MyGuild.AllyGuilds[i]))
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
            if (TodayIncome + tax <= CastleDef.TODAYGOLD)
            {
                TodayIncome = TodayIncome + tax;
            }
            else
            {
                if (TodayIncome >= CastleDef.TODAYGOLD)
                {
                    tax = 0;
                }
                else
                {
                    tax = CastleDef.TODAYGOLD - TodayIncome;
                    TodayIncome = CastleDef.TODAYGOLD;
                }
            }
            if (tax > 0)
            {
                if ((long)TotalGold + tax <= CastleDef.CASTLEMAXGOLD)
                {
                    TotalGold = TotalGold + tax;
                }
                else
                {
                    TotalGold = CastleDef.CASTLEMAXGOLD;
                }
            }
            if (HUtil32.GetTickCount() - SaveCastleGoldTime > 10 * 60 * 1000)
            {
                SaveCastleGoldTime = HUtil32.GetTickCount();
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
                    if ((long)howmuch + TotalGold <= CastleDef.CASTLEMAXGOLD)
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

        public bool RepairCastleDoor()
        {
            bool result = false;
            if ((MainDoor.UnitObj != null) && !BoCastleUnderAttack)
            {
                if (MainDoor.UnitObj.WAbil.HP < MainDoor.UnitObj.WAbil.MaxHP)
                {
                    if (!MainDoor.UnitObj.Death)
                    {
                        if (HUtil32.GetTickCount() - ((TCastleDoor)MainDoor.UnitObj).StruckTime > 1 * 60 * 1000)
                        {
                            MainDoor.UnitObj.WAbil.HP = MainDoor.UnitObj.WAbil.MaxHP;
                            ((TCastleDoor)MainDoor.UnitObj).RepairStructure();
                            result = true;
                        }
                    }
                    else
                    {
                        if (HUtil32.GetTickCount() - ((TCastleDoor)MainDoor.UnitObj).BrokenTime > 1 * 60 * 1000)
                        {
                            MainDoor.UnitObj.WAbil.HP = MainDoor.UnitObj.WAbil.MaxHP;
                            MainDoor.UnitObj.Death = false;
                            ((TCastleDoor)MainDoor.UnitObj).BoOpenState = false;
                            ((TCastleDoor)MainDoor.UnitObj).RepairStructure();
                            result = true;
                        }
                    }
                }
            }
            return result;
        }

        public int RepairCoreCastleWall(int wallnum)
        {
            TWallStructure wall;
            int result = 0;
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
            }
            if ((wall != null) && !BoCastleUnderAttack)
            {
                if (wall.WAbil.HP < wall.WAbil.MaxHP)
                {
                    if (!wall.Death)
                    {
                        if (HUtil32.GetTickCount() - wall.StruckTime > 1 * 60 * 1000)
                        {
                            wall.WAbil.HP = wall.WAbil.MaxHP;
                            wall.RepairStructure();
                            result = 1;
                        }
                    }
                    else
                    {
                        if (HUtil32.GetTickCount() - wall.BrokenTime > 1 * 60 * 1000)
                        {
                            wall.WAbil.HP = wall.WAbil.MaxHP;
                            wall.Death = false;
                            wall.RepairStructure();
                            result = 1;
                        }
                    }
                }
            }
            return result;
        }

        public bool IsAttackGuild(TGuild aguild)
        {
            bool result = false;
            for (var i = 0; i < AttackerList.Count; i++)
            {
                if (aguild == AttackerList[i].Guild)
                {
                    result = true;
                    break;
                }
            }
            return result;
        }

        public bool ProposeCastleWar(TGuild aguild)
        {
            TAttackerInfo pattack;
            bool result = false;
            if (!IsAttackGuild(aguild))
            {
                pattack = new TAttackerInfo();
                //pattack.AttackDate = CalcDay(DateTime.Today, 1 + 3);
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
            string result = "";
            if (AttackerList.Count > 0)
            {
                if (svMain.ENGLISHVERSION)
                {
                    result = AttackerList[0].AttackDate.ToString();
                }
                else if (svMain.PHILIPPINEVERSION)
                {
                    result = AttackerList[0].AttackDate.ToString();
                }
                else
                {
                    var ayear = AttackerList[0].AttackDate.Year;
                    var amon = AttackerList[0].AttackDate.Month;
                    var aday = AttackerList[0].AttackDate.Day;
                    result = ayear.ToString() + "年" + amon.ToString() + "月" + aday.ToString() + "日";
                }
            }
            return result;
        }

        public string GetListOfWars()
        {
            string str = string.Empty;
            string result = "";
            var ayear = 0;
            var amon = 0;
            var aday = 0;
            int len = 0;
            for (var i = 0; i < AttackerList.Count; i++)
            {
                var y = AttackerList[i].AttackDate.Year;
                var m = AttackerList[i].AttackDate.Month;
                var d = AttackerList[i].AttackDate.Day;
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
                        result = result + AttackerList[i].AttackDate.ToString() + "\\";
                    }
                    else if (svMain.PHILIPPINEVERSION)
                    {
                        result = result + AttackerList[i].AttackDate.ToString() + "\\";
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
                str = "\"" + AttackerList[i].GuildName + "\" ";
                len = len + str.Length;
                result = result + str;
            }
            return result;
        }

        public void StartCastleWar()
        {
            ArrayList ulist = new ArrayList();
            svMain.UserEngine.GetAreaUsers(CastlePEnvir, CastleStartX, CastleStartY, 100, ulist);
            for (var i = 0; i < ulist.Count; i++)
            {
                TUserHuman hum = (TUserHuman)ulist[i];
                hum.UserNameChanged();
            }
            ulist.Free();
        }

        public bool IsCastleWarArea(TEnvirnoment penvir, int x, int y)
        {
            bool result = false;
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
            bool result = false;
            if (aguild == null)
            {
                return result;
            }
            for (var i = 0; i < RushGuildList.Count; i++)
            {
                if (RushGuildList[i] == aguild)
                {
                    result = true;
                    break;
                }
                for (var j = 0; j < aguild.AllyGuilds.Count; j++)
                {
                    if (RushGuildList[i] == aguild.AllyGuilds[j])
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
            return RushGuildList.Count;
        }

        public bool CheckCastleWarWinCondition(TGuild aguild)
        {
            bool result = false;
            bool flag = false;
            if (HUtil32.GetTickCount() - CastleAttackStarted > 10 * 60 * 1000)
            {
                ArrayList ulist = new ArrayList();
                svMain.UserEngine.GetAreaUsers(CorePEnvir, 0, 0, 1000, ulist);
                flag = true;
                for (var i = 0; i < ulist.Count; i++)
                {
                    if ((!((TCreature)ulist[i]).Death) && (((TCreature)ulist[i]).MyGuild != aguild))
                    {
                        flag = false;
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
            TGuild oldguild = OwnerGuild;
            OwnerGuild = guild;
            OwnerGuildName = guild.GuildName;
            LatestOwnerChangeDateTime = DateTime.Now;
            SaveToFile(CastleDef.CASTLEFILENAME);
            if (oldguild != null)
            {
                oldguild.MemberNameChanged();
            }
            if (OwnerGuild != null)
            {
                OwnerGuild.MemberNameChanged();
            }
            string str = "(*)沙巴克已被\"" + OwnerGuildName + "\"占领！！";
            svMain.UserEngine.SysMsgAll(str);
            svMain.UserEngine.SendInterMsg(Grobal2.ISM_SYSOPMSG, svMain.ServerIndex, str);
            svMain.MainOutMessage("[沙巴克]" + OwnerGuildName + "占领");
        }

        public void FinishCastleWar()
        {
            BoCastleUnderAttack = false;
            RushGuildList.Clear();
            ArrayList ulist = new ArrayList();
            svMain.UserEngine.GetAreaUsers(CastlePEnvir, CastleStartX, CastleStartY, 100, ulist);
            for (var i = 0; i < ulist.Count; i++)
            {
                TUserHuman hum = (TUserHuman)ulist[i];
                hum.BoInFreePKArea = false;
                if (hum.MyGuild != OwnerGuild)
                {
                    hum.RandomSpaceMove(hum.HomeMap, 0);
                }
            }
            ulist.Free();
            string str = "[沙巴克攻城战已经结束]";
            svMain.UserEngine.SysMsgAll(str);
            svMain.UserEngine.SendInterMsg(Grobal2.ISM_SYSOPMSG, svMain.ServerIndex, str);
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
