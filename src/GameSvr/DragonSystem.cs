using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using SystemModule;
using SystemModule.Common;

namespace GameSvr
{
    public class TDropItemInfo
    {
        public string Name;
        public int FirstRate;
        public int SecondRate;
        public int Amount;
        public int DropCount;
    }

    public class TDragonLevelInfo
    {
        public int Level;
        public int DropExp;
        public IList<TDropItemInfo> DropItemList;
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
        private string FInitFileName = string.Empty;
        private readonly TDragonLevelInfo[] FLevelInfo;
        private int FLevel = 0;
        private int FExp = 0;
        private long FLastChangeExpTime = 0;
        private long FLastAttackTme = 0;
        private ArrayList FAutoAttackMap = null;
        private string FDropMapName = string.Empty;
        private TEnvirnoment FDopItemEnvir = null;
        private Rectangle FDropItemRect;
        private int FExpDivider = 0;

        public TDragonSystem() : base()
        {
            bool RetSuccess = false;
            FLevelInfo = new TDragonLevelInfo[DragonSystem.DRAGON_MAX_LEVEL];
            InitFirst();
            Initialize(FInitFileName, ref RetSuccess);
            if (RetSuccess == false)
            {
                return;
            }
        }

        ~TDragonSystem()
        {
            RemoveAll();
        }

        private void InitFirst()
        {
            try
            {
                for (var i = 0; i < DragonSystem.DRAGON_MAX_LEVEL; i++)
                {
                    FLevelInfo[i] = new TDragonLevelInfo();
                    FLevelInfo[i].Level = i + 1;
                    FLevelInfo[i].DropExp = (i + 1) * 10000;
                    FLevelInfo[i].DropItemList = new List<TDropItemInfo>();
                }
                FAutoAttackMap = new ArrayList();
            }
            catch
            {
            }
            FDopItemEnvir = null;
            FDropItemRect.X = -1;
            FDropItemRect.Y = -1;
            FDropItemRect.Width = -1;
            FDropItemRect.Height = -1;
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

        private string DecodeStrInfo(StringList StrInfo, ref bool IsSuccess)
        {
            IsSuccess = false;
            string str = string.Empty;
            string str1 = string.Empty;
            string str2 = string.Empty;
            string str3 = string.Empty;
            string infostr = string.Empty;
            TDropItemInfo pDropItemInfo;
            string result = "";
            int levelCount = 0;
            int expcount = 0;
            int itemcount = 0;
            int CurrentLevel = 1;
            int CurrentExp = 10000;
            try
            {
                for (var i = 0; i < StrInfo.Count; i++)
                {
                    str = StrInfo[i].Trim();
                    if ((str != "") && (str[0] != ';'))
                    {
                        //infostr = str[1];
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
                                FDropItemRect.X = HUtil32.Str_ToInt(str3, -1);
                                str2 = HUtil32.GetValidStr3(str2, ref str3, new string[] { " ", "\09" });
                                FDropItemRect.Y = HUtil32.Str_ToInt(str3, -1);
                                str2 = HUtil32.GetValidStr3(str2, ref str3, new string[] { " ", "\09" });
                                FDropItemRect.Width = HUtil32.Str_ToInt(str3, -1);
                                str2 = HUtil32.GetValidStr3(str2, ref str3, new string[] { " ", "\09" });
                                FDropItemRect.Height = HUtil32.Str_ToInt(str3, -1);
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
                            str2 = HUtil32.GetValidStr3(str, ref str1, new string[] { " ", "\09" });
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
            string result = "";
            IsSuccess = false;
            try
            {
                if (!File.Exists(FileName))
                {
                    return "Do not Find FileName:" + FileName;
                }
                StringList fileinfo = new StringList();
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
            if ((changelevel < 1) || (changelevel >= 13))
            {
                return;
            }
            for (var i = 0; i < FLevelInfo[changelevel - 1].DropItemList.Count; i++)
            {
                TDropItemInfo pinfo = FLevelInfo[changelevel - 1].DropItemList[i];
                for (var j = 0; j < pinfo.DropCount; j++)
                {
                    if (new Random(pinfo.SecondRate).Next() < pinfo.FirstRate)
                    {
                        int px = new Random(Math.Abs(FDropItemRect.Right - FDropItemRect.Left) + 1).Next() + FDropItemRect.Left;
                        int slope1 = FDropItemRect.Left + FDropItemRect.Top + 4;
                        int slope2 = FDropItemRect.Right + FDropItemRect.Bottom - 4;
                        int slope3 = FDropItemRect.Top - FDropItemRect.Left - 4;
                        int slope4 = FDropItemRect.Bottom - FDropItemRect.Right + 4;
                        int LowValue = HUtil32._MAX(slope1 - px, px + slope3);
                        int HighValue = HUtil32._MIN(slope2 - px, px + slope4);
                        int py = new Random(HighValue - LowValue + 1).Next() + LowValue;
                        int itemmakeindex = M2Share.UserEngine.MakeItemToMap(FDropMapName, pinfo.Name, pinfo.Amount, px, py);
                        if (itemmakeindex != 0)
                        {
                            M2Share.AddUserLog("15\09" + FDropMapName + "\09" + px.ToString() + "\09" + py.ToString() + "\09" + "EvilMir" + "\09" + pinfo.Name + "\09" + itemmakeindex.ToString() + "\09" + "0" + "\09" + "0");
                        }
                    }
                }
            }
        }

        public void OnAttackTarget(TEnvirnoment Envir_, TCreature user_, int Mode_)
        {
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
                int pwr = 20 * (new System.Random(3).Next() + 1);
                int dam = user_.GetMagStruckDamage(null, pwr);
                user_.StruckDamage(dam, null);
                user_.SendDelayMsg(Grobal2.RM_STRUCK, Grobal2.RM_REFMESSAGE, (short)dam, user_.WAbil.HP, user_.WAbil.MaxHP, 0, "", 200);
            }
        }

        public void OnAutoAttack(TEnvirnoment Envir_, int Mode_)
        {
            TCreature Tempuser;
            ArrayList userlist = new ArrayList();
            int usercount = M2Share.UserEngine.GetAreaAllUsers(Envir_, userlist);
            for (var i = 0; i < userlist.Count; i++)
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
            FDopItemEnvir = M2Share.GrobalEnvir.GetEnvir(FDropMapName);
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
                            M2Share.MainOutMessage("DRAGON LEVELUP LEVEL:" + FLevel.ToString());
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
                M2Share.MainOutMessage("DRAGON RESET LEVEL");
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
                M2Share.MainOutMessage("EXCEPTION DRAGON SYSTEM");
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

