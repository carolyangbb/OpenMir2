using System;
using System.Collections;
using System.Drawing;
using System.IO;
using SystemModule;

namespace GameSvr
{
    public struct TDropItemInfo
    {
        public string Name;
        public int FirstRate;
        public int SecondRate;
        public int Amount;
        public int DropCount;
    }

    public struct TDragonLevelInfo
    {
        public int Level;
        public int DropExp;
        public ArrayList DropItemList;
    }

    public struct TATMapInfo
    {
        public TEnvirnoment Envir;
        public int Mode;
    }

    public class TDragonSystem
    {
        public int Level
        {
            get
            {
                return FLevel;
            }
        }
        public int Exp
        {
            get
            {
                return FExp;
            }
        }
        public int ExpDivider
        {
            get
            {
                return GetExpDivider();
            }
            set
            {
                SetExpDivider(value);
            }
        }
        private string FInitFileName = String.Empty;
        private readonly TDragonLevelInfo[] FLevelInfo;
        private int FLevel = 0;
        private int FExp = 0;
        private long FLastChangeExpTime = 0;
        private long FLastAttackTme = 0;
        private ArrayList FAutoAttackMap = null;
        private string FDropMapName = String.Empty;
        private TEnvirnoment FDopItemEnvir = null;
        private Rectangle FDropItemRect = null;
        private int FExpDivider = 0;

        public TDragonSystem() : base()
        {
            bool RetSuccess;
            InitFirst();
            Initialize(FInitFileName, ref RetSuccess);
            if (RetSuccess == false)
            {
            }
        }

        ~TDragonSystem()
        {
            RemoveAll();
        }

        private void InitFirst()
        {
            int i;
            try
            {
                for (i = 0; i < DragonSystem.DRAGON_MAX_LEVEL; i++)
                {
                    FLevelInfo[i].Level = i + 1;
                    FLevelInfo[i].DropExp = (i + 1) * 10000;
                    FLevelInfo[i].DropItemList = new ArrayList();
                }
                FAutoAttackMap = new ArrayList();
            }
            catch
            {
            }
            FDopItemEnvir = null;
            FDropItemRect.Left = -1;
            FDropItemRect.Top = -1;
            FDropItemRect.Right = -1;
            FDropItemRect.Bottom = -1;
            FLevel = 1;
            FExp = 0;
            FExpDivider = 1;
            FLastChangeExpTime = HUtil32.GetTickCount();
            FLastAttackTme = HUtil32.GetTickCount();
            FInitFileName = DragonSystem.DRAGONITEMFILE;
        }

        private void RemoveAll()
        {
            int i;
            int j;
            try
            {
                for (i = 0; i < DragonSystem.DRAGON_MAX_LEVEL; i++)
                {
                    if (FLevelInfo[i].DropItemList != null)
                    {
                        for (j = FLevelInfo[i].DropItemList.Count - 1; j >= 0; j--)
                        {
                            dispose(FLevelInfo[i].DropItemList[0]);
                            FLevelInfo[i].DropItemList.RemoveAt(0);
                        }
                        FLevelInfo[i].DropItemList.Free();
                        FLevelInfo[i].DropItemList = null;
                    }
                }
            }
            catch
            {
            }
            try
            {
                for (i = 0; i < FAutoAttackMap.Count; i++)
                {
                    dispose(FAutoAttackMap[0]);
                    FAutoAttackMap.RemoveAt(0);
                }
                FAutoAttackMap.Free();
                FAutoAttackMap = null;
            }
            catch
            {
            }
        }

        private string DecodeStrInfo(ArrayList StrInfo, ref bool IsSuccess)
        {
            string result;
            int i;
            string str;
            string str1;
            string str2;
            string str3;
            string infostr;
            int CurrentLevel;
            int CurrentExp;
            TDropItemInfo pDropItemInfo;
            int levelCount;
            int expcount;
            int itemcount;
            result = "";
            IsSuccess = false;
            levelCount = 0;
            expcount = 0;
            levelCount = 0;
            itemcount = 0;
            CurrentLevel = 1;
            CurrentExp = 10000;
            try
            {
                for (i = 0; i < StrInfo.Count; i++)
                {
                    str = StrInfo[i].Trim();
                    if ((str != "") && (str[1] != ";"))
                    {
                        infostr = str[1];
                        if (infostr == "!")
                        {
                            str2 = HUtil32.GetValidStr3(str, ref str1, new string[] { " ", "\09" });
                            if (str1.ToLower().CompareTo("!LEVEL".ToLower()) == 0)
                            {
                                CurrentLevel = HUtil32.Str_ToInt(str2.Trim(), 1);
                                if ((CurrentLevel <= 0) && (CurrentLevel > DragonSystem.DRAGON_MAX_LEVEL))
                                {
                                    result = "[" + (i + 1).ToString() + "] " + "ERROR! LevelInfo Worng 1~" + DragonSystem.DRAGON_MAX_LEVEL.ToString() + ":" + str2;
                                    return result;
                                }
                                levelCount++;
                            }
                            else if (str1.ToLower().CompareTo("!EXP".ToLower()) == 0)
                            {
                                CurrentExp = HUtil32.Str_ToInt(str2.Trim(), 0);
                                if (CurrentExp > 0)
                                {
                                    FLevelInfo[CurrentLevel - 1].DropExp = CurrentExp;
                                    expcount++;
                                }
                                else
                                {
                                    result = "[" + (i + 1).ToString() + "] " + "ERROR! ExpInfo Worong Exp < 0:" + str2;
                                    return result;
                                }
                            }
                            else if (str1.ToLower().CompareTo("!DROPMAP".ToLower()) == 0)
                            {
                                FDropMapName = str2.Trim();
                            }
                            else if (str1.ToLower().CompareTo("!DROPAREA".ToLower()) == 0)
                            {
                                str2 = HUtil32.GetValidStr3(str2, ref str3, new string[] { " ", "\09" });
                                FDropItemRect.Left = HUtil32.Str_ToInt(str3, -1);
                                str2 = HUtil32.GetValidStr3(str2, ref str3, new string[] { " ", "\09" });
                                FDropItemRect.Top = HUtil32.Str_ToInt(str3, -1);
                                str2 = HUtil32.GetValidStr3(str2, ref str3, new string[] { " ", "\09" });
                                FDropItemRect.Right = HUtil32.Str_ToInt(str3, -1);
                                str2 = HUtil32.GetValidStr3(str2, ref str3, new string[] { " ", "\09" });
                                FDropItemRect.Bottom = HUtil32.Str_ToInt(str3, -1);
                            }
                            else
                            {
                                result = "[" + (i + 1).ToString() + "] " + "ERROR! Check String :" + str1;
                                return result;
                            }
                        }
                        else
                        {
                            pDropItemInfo = new TDropItemInfo();
                            str2 = HUtil32.GetValidStr3(str,ref str1, new string[] { " ", "\09" });
                            pDropItemInfo.Name = str1.Trim();
                            str2 = HUtil32.GetValidStr3(str2, ref str1, new string[] { " ", "\09" });
                            pDropItemInfo.FirstRate = HUtil32.Str_ToInt(str1, 0);
                            str2 = HUtil32.GetValidStr3(str2, ref str1, new string[] { " ", "\09" });
                            pDropItemInfo.SecondRate = HUtil32.Str_ToInt(str1, 1);
                            str2 = HUtil32.GetValidStr3(str2, ref str1, new string[] { " ", "\09" });
                            pDropItemInfo.Amount = HUtil32.Str_ToInt(str1, 1);
                            str2 = HUtil32.GetValidStr3(str2, ref str1, new string[] { " ", "\09" });
                            pDropItemInfo.DropCount = HUtil32.Str_ToInt(str1, 1);
                            if (pDropItemInfo.FirstRate > pDropItemInfo.SecondRate)
                            {
                                result = "[" + (i + 1).ToString() + "] " + "ERROR! FirstRate > SeconRate ";
                                dispose(pDropItemInfo);
                                return result;
                            }
                            if (pDropItemInfo.DropCount < 1)
                            {
                                result = "[" + (i + 1).ToString() + "] " + "ERROR! DropCount is 0 ";
                                dispose(pDropItemInfo);
                                return result;
                            }
                            FLevelInfo[CurrentLevel - 1].DropItemList.Add(pDropItemInfo);
                            itemcount++;
                        }
                    }
                }
            }
            catch
            {
            }
            IsSuccess = true;
            result = "READ SUCCESS Level:" + levelCount.ToString() + " ,Exp:" + expcount.ToString() + " ,ITEM" + itemcount.ToString() + " DROPMAP: " + FDropMapName + "DROPAREA: " + " X1:" + FDropItemRect.Left.ToString() + "," + " Y1:" + FDropItemRect.Top.ToString() + "," + " X2:" + FDropItemRect.Right.ToString() + "," + " Y2:" + FDropItemRect.Bottom.ToString();
            return result;
        }

        public string Initialize(string FileName, ref bool IsSuccess)
        {
            string result;
            ArrayList fileinfo;
            result = "";
            IsSuccess = false;
            try
            {
                if (!File.Exists(FileName))
                {
                    result = this.ClassName + "|Do not Find FileName:" + FileName;
                    return result;
                }
                fileinfo = new ArrayList();
                fileinfo.LoadFromFile(FileName);
                result = DecodeStrInfo(fileinfo, ref IsSuccess);
                fileinfo.Free();
            }
            catch
            {
            }
            return result;
        }

        public string reload(ref bool IsSuccess)
        {
            string result;
            RemoveAll();
            InitFirst();
            result = Initialize(FInitFileName, ref IsSuccess);
            return result;
        }

        private int GetNextLevelExp()
        {
            int result;
            if ((FLevel > 0) && (FLevel <= DragonSystem.DRAGON_MAX_LEVEL))
            {
                result = FLevelInfo[FLevel - 1].DropExp / FExpDivider;
            }
            else
            {
                result = 0x7FFFFFFF;
            }
            return result;
        }

        public void OnLevelup(int changelevel)
        {
            OnDropItem(changelevel);
        }

        public void OnDropItem(int changelevel)
        {
            int i;
            int j;
            int px;
            int py;
            TDropItemInfo pinfo;
            int slope1;
            int slope2;
            int slope3;
            int slope4;
            int LowValue;
            int HighValue;
            int itemmakeindex;
            if ((changelevel < 1) || (changelevel >= 13))
            {
                return;
            }
            for (i = 0; i < FLevelInfo[changelevel - 1].DropItemList.Count; i++)
            {
                pinfo = FLevelInfo[changelevel - 1].DropItemList[i] as TDropItemInfo;
                for (j = 0; j < pinfo.DropCount; j++)
                {
                    if (new System.Random(pinfo.SecondRate).Next() < pinfo.FirstRate)
                    {
                        px = new System.Random(Math.Abs(FDropItemRect.Right - FDropItemRect.Left) + 1).Next() + FDropItemRect.Left;
                        slope1 = FDropItemRect.Left + FDropItemRect.Top + 4;
                        slope2 = FDropItemRect.Right + FDropItemRect.Bottom - 4;
                        slope3 = FDropItemRect.Top - FDropItemRect.Left - 4;
                        slope4 = FDropItemRect.Bottom - FDropItemRect.Right + 4;
                        LowValue = _MAX(slope1 - px, px + slope3);
                        HighValue = _MIN(slope2 - px, px + slope4);
                        py = new System.Random(HighValue - LowValue + 1).Next() + LowValue;
                        itemmakeindex = 0;
                        itemmakeindex = svMain.UserEngine.MakeItemToMap(FDropMapName, pinfo.Name, pinfo.Amount, px, py);
                        if (itemmakeindex != 0)
                        {
                            // 冻崩_
                            svMain.AddUserLog("15\09" + FDropMapName + "\09" + px.ToString() + "\09" + py.ToString() + "\09" + "EvilMir" + "\09" + pinfo.Name + "\09" + itemmakeindex.ToString() + "\09" + "0" + "\09" + "0");
                        }
                    }
                }
            }
        }

        public void OnAttackTarget(TEnvirnoment Envir_, TCreature user_, int Mode_)
        {
            int pwr;
            int dam;
            if ((!user_.Death) && (!user_.BoGhost) && (!user_.BoSysopMode) && (!user_.BoSuperviserMode))
            {
                switch (Mode_)
                {
                    case 1:
                        user_.SendRefMsg(Grobal2.RM_NORMALEFFECT, 0, user_.CX, user_.CY, Grobal2.NE_THUNDER, "");
                        break;
                    case 2:
                        user_.SendRefMsg(Grobal2.RM_NORMALEFFECT, 0, user_.CX, user_.CY, Grobal2.NE_FIRE, "");
                        break;
                }
                pwr = 20 * (new System.Random(3).Next() + 1);
                dam = user_.GetMagStruckDamage(null, pwr);
                user_.StruckDamage(dam, null);
                // wparam
                // lparam1
                // lparam2
                // hiter
                user_.SendDelayMsg((TCreature)Grobal2.RM_STRUCK, Grobal2.RM_REFMESSAGE, dam, user_.WAbil.HP, user_.WAbil.MaxHP, (long)null, "", 200);
            }
        }

        public void OnAutoAttack(TEnvirnoment Envir_, int Mode_)
        {
            ArrayList userlist;
            int usercount;
            int i;
            TCreature Tempuser;
            userlist = new ArrayList();
            usercount = svMain.UserEngine.GetAreaAllUsers(Envir_, userlist);
            for (i = 0; i < userlist.Count; i++)
            {
                Tempuser = (TCreature)userlist[i];
                if (Tempuser.RaceServer == Grobal2.RC_USERHUMAN)
                {
                    if (new System.Random(2).Next() == 0)
                    {
                        OnAttackTarget(Envir_, Tempuser, Mode_);
                    }
                }
            }
            userlist.Clear();
            userlist.Free();
        }

        public void OnMapAutoAttack()
        {
            int i;
            TATMapInfo pmapinfo;
            for (i = 0; i < FAutoAttackMap.Count; i++)
            {
                pmapinfo = (TATMapInfo)FAutoAttackMap[i];
                OnAutoAttack(pmapinfo.Envir, pmapinfo.Mode);
            }
        }

        public void SetAutoAttackMap(TEnvirnoment Envir_, int Mode_)
        {
            TATMapInfo pmapinfo;
            if (FAutoAttackMap != null)
            {
                pmapinfo = new TATMapInfo();
                pmapinfo.Envir = Envir_;
                pmapinfo.Mode = Mode_;
                FAutoAttackMap.Add(pmapinfo);
            }
        }

        public void SetItemDropMap(string MapName, Rectangle Area_)
        {
            FDopItemEnvir = svMain.GrobalEnvir.GetEnvir(FDropMapName);
            FDropItemRect = Area_;
        }

        public void ChangeExp(int exp)
        {
            FLastChangeExpTime = HUtil32.GetTickCount();
            if (FLevel < DragonSystem.DRAGON_MAX_LEVEL)
            {
                if (exp > 0)
                {
                    if (FExp < GetNextLevelExp())
                    {
                        FExp = FExp + exp;
                        if (FExp >= GetNextLevelExp())
                        {
                            FLevel = FLevel + 1;
                            FExp = 0;
                            OnLevelup(FLevel);
                            svMain.MainOutMessage("DRAGON LEVELUP LEVEL:" + FLevel.ToString());
                        }
                    }
                }
            }
        }

        private void SetExpDivider(int divvalue)
        {
            if ((divvalue <= 1000) && (divvalue >= 1))
            {
                FExpDivider = divvalue;
            }
        }

        private int GetExpDivider()
        {
            int result;
            result = FExpDivider;
            return result;
        }

        private void ResetLevel()
        {
            if ((FLevel != 1) || (FExp != 0))
            {
                FLevel = 1;
                FExp = 0;
                svMain.MainOutMessage("DRAGON RESET LEVEL");
            }
        }

        public void Run()
        {
            try
            {
                if (HUtil32.GetTickCount() - FLastChangeExpTime > DragonSystem.DRAGON_RESETTIME)
                {
                    FLastChangeExpTime = HUtil32.GetTickCount();
                    ResetLevel();
                }
                if (HUtil32.GetTickCount() - FLastAttackTme > DragonSystem.MAP_ATTACK_TIME)
                {
                    FLastAttackTme = HUtil32.GetTickCount();
                    OnMapAutoAttack();
                }
            }
            catch
            {
                svMain.MainOutMessage("EXCEPTION DRAGON SYSTEM");
            }
        }

        public void dispose(object obj)
        {
            if (obj != null)
            {
                obj = null;
            }
        }
    }
}

namespace GameSvr
{
    public class DragonSystem
    {
        public const int DRAGON_MAX_LEVEL = 13;
        public const double DRAGON_RESETTIME = 15 * 60 * 1000;
        public const double MAP_ATTACK_TIME = 10 * 1000;
        public const string DRAGONITEMFILE = "DragonItem.txt";
    }
}

