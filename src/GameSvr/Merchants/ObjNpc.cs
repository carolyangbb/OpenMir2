using System;
using System.Collections;
using System.IO;
using SystemModule;

namespace GameSvr
{
    public class TUpgradeInfo
    {
        public string UserName;
        public TUserItem uitem;
        public byte updc;
        public byte upsc;
        public byte upmc;
        public byte durapoint;
        public DateTime readydate;
        public long readycount;
    }

    public class TQuestRequire
    {
        public int RandomCount;
        public ushort CheckIndex;
        public byte CheckValue;
    }

    public class TQuestActionInfo
    {
        public int ActIdent;
        public string ActParam;
        public int ActParamVal;
        public string ActTag;
        public int ActTagVal;
        public string ActExtra;
        public int ActExtraVal;
        public string ActParam4;
        public int ActParamVal4;
        public string ActParam5;
        public int ActParamVal5;
        public string ActParam6;
        public int ActParamVal6;
        public string ActParam7;
        public int ActParamVal7;
    }

    public class TQuestConditionInfo
    {
        public int IfIdent;
        public string IfParam;
        public int IfParamVal;
        public string IfTag;
        public int IfTagVal;
    }

    public class TSayingProcedure
    {
        public ArrayList ConditionList;
        public ArrayList ActionList;
        public string Saying;
        public ArrayList ElseActionList;
        public string ElseSaying;
        public ArrayList AvailableCommands;
    }

    public class TSayingRecord
    {
        public string Title;
        public ArrayList Procs;
    }

    public class TQuestRecord
    {
        public bool BoRequire;
        public int LocalNumber;
        public TQuestRequire[] QuestRequireArr;
        public ArrayList SayingList;
    }

    public class ObjNpc
    {
        public const int GUILDWARFEE = 30000;
        public const int CASTLEMAINDOORREPAREGOLD = 1500000;
        public const int CASTLECOREWALLREPAREGOLD = 400000;
        public const int CASTLEARCHEREMPLOYFEE = 250000;
        public const int CASTLEGUARDEMPLOYFEE = 250000;
        public const int UPGRADEWEAPONFEE = 10000;
        public const int MAXREQUIRE = 10;
        public const int MAX_SOURCECNT = 6;
        public const int COND_FAILURE = 0;
        public const int COND_GEMFAIL = 1;
        public const int COND_SUCCESS = 2;
        public const int COND_MINERALFAIL = 3;
        public const int COND_NOMONEY = 4;
        public const int COND_BAGFULL = 5;
        public const int GSG_ERROR = 0;
        public const int GSG_SMALL = 1;
        public const int GSG_MEDIUM = 2;
        public const int GSG_LARGE = 3;
        public const int GSG_GREATLARGE = 4;
        public const int GSG_SUPERIOR = 5;

        public static int GetPP(string str)
        {
            int n;
            int result = -1;
            if (str.Length == 2)
            {
                if (Char.ToUpper(str[0]) == 'P')
                {
                    n = HUtil32.Str_ToInt(str[1].ToString(), -1);
                    if (n >= 0 && n <= 9)
                    {
                        result = n;
                    }
                }
                if (Char.ToUpper(str[0]) == 'G')
                {
                    n = HUtil32.Str_ToInt(str[1].ToString(), -1);
                    if (n >= 0 && n <= 9)
                    {
                        result = 100 + n;
                    }
                }
                if (Char.ToUpper(str[0]) == 'D')
                {
                    n = HUtil32.Str_ToInt(str[1].ToString(), -1);
                    if (n >= 0 && n <= 9)
                    {
                        result = 200 + n;
                    }
                }
                if (Char.ToUpper(str[0]) == 'M')
                {
                    n = HUtil32.Str_ToInt(str[1].ToString(), -1);
                    if (n >= 0 && n <= 9)
                    {
                        result = 300 + n;
                    }
                }
            }
            return result;
        }

        public static void ReadStrings(string flname, ArrayList strlist)
        {
            strlist.Clear();
            System.IO.FileInfo f = new FileInfo(flname);
            StreamReader _R_0 = f.OpenText();
            while (!(_R_0.BaseStream.Position >= _R_0.BaseStream.Length))
            {
                string str = _R_0.ReadLine();
                strlist.Add(str);
            }
            _R_0.Close();
        }

        public static void WriteStrings(string flname, ArrayList strlist)
        {
            System.IO.FileInfo f;
            int i;
            f = new FileInfo(flname);
            StreamWriter _W_0 = f.CreateText();
            for (i = 0; i < strlist.Count; i++)
            {
                _W_0.WriteLine(strlist[i]);
            }
            _W_0.Close();
        }
    }
}
