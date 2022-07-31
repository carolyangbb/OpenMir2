using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.IO;
using SystemModule;
using SystemModule.Common;

// ==============================================================================
// SQLLocal DB :
// 霸烙救俊辑 荤侩登绰 单捞鸥海捞胶甫 佬绢甸捞绰 努贰胶 沥狼
// StdItems    : 酒捞袍  沥焊
// Monster     : 阁胶磐 沥焊
// MonsterItem : 阁胶磐啊 冻备绰 酒捞袍 沥焊
// Magic       : 扁贱 (公傍) 沥焊
// 
// 寇何 拳老   : .\!DBSETUP.TXT    : DB 立加 沥焊
// ALIAS NAME=                 : BDE Alias Setting
// SERVER NAME=                : Name or IP
// DATABASE NAME=              : Resource DataBase Name
// USER NAME=                  : DB Owner's User Name
// PASSWORD=                   : Password
// TABLE_STDITEMS=             : Table Name of StdItems
// TABLE_MONSTER=              : Table Name of Monster
// TABLE_MOBITEM=              : Table Name of Monster's Drop Items
// TABLE_MAGIC=                : Table Name of Magic
// 
// 努贰胶 沥狼
// TDataMgr        =  Base Class of Data Manager ( Don't Use Directly )
// TItemMgr        = class of TDataMgr
// TMonsterMgr     = class of TDataMgr
// TMonsterItemMgr = class of TDataMgr
// TMagicMgr       = class of TDAtaMgr
// 
// 累己磊 : 冠措己 ,2003.2.24
// ===============================================================================
namespace GameSvr
{
    public struct TSQLDetails
    {
        public string Alias;
        public string Server;
        public string Database;
        public string User;
        public string Pass;
    }

    public class TDataMgr
    {
        public bool IsConected => FConnected;
        private readonly StringDictionary FInfos = null;
        private TLoadType FLoadType;
        private readonly Dictionary<string, string> FLinkInfo = null;
        private readonly IDataReader FQuery = null;
        private string FCompareStr = string.Empty;
        private string ConnectionString = null;
        private TSQLDetails SQLDetails;
        private readonly bool FConnected = false;
        private string FTableName = string.Empty;
        public string FTableNameIndex = string.Empty;

        public TDataMgr() : base()
        {
            //FQuery = new TAdoQuery(null);
            //FDataBase = new TADOConnection(null);
            FInfos = new StringDictionary();
            FLinkInfo = new Dictionary<string, string>();
            FConnected = false;
            FTableName = "";
        }

        ~TDataMgr()
        {
            DBDisConnect();
            //FQuery.Free();
            //FDataBase.Free();
            //FInfos.Free();
            //FLinkInfo.Free();
        }

#if !LOADSQL
        private void AddBDEalias(TSQLDetails SQLDetails)
        {
            string sAlias;
            ArrayList slParams;
            ArrayList slaliasList;
            slParams = null;
            slaliasList = null;
            try
            {
                slParams = new ArrayList();
                slaliasList = new ArrayList();
                sAlias = SQLDetails.Alias;
                slParams.Add("SERVER NAME=" + SQLDetails.Server);
                slParams.Add("DATABASE NAME=" + SQLDetails.Database);
                slParams.Add("USER NAME=" + SQLDetails.User);
                slParams.Add("PASSWORD=" + SQLDetails.Pass);
                try
                {
                    //Session.ConfigMode = cmPersistent;
                    //Session.GetAliasNames(slaliasList);
                    //if (slaliasList.IndexOf(sAlias) > -1)
                    //{
                    //    Session.DeleteAlias(sAlias);
                    //    Session.SaveConfigFile;
                    //}
                    //Session.AddAlias(sAlias, "MSSQL", slParams);
                    //Session.SaveConfigFile;
                }
                catch (Exception)
                {
                    //MessageBox.Show(":" + E.Message, Application.ProductName, new object[] { MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            finally
            {
                if (slParams != null)
                {
                    slParams.Free();
                }
                if (slaliasList != null)
                {
                    slaliasList.Free();
                }
            }
        }

#endif

        private bool GetLinkInfo(Dictionary<string, string> LinkInfo)
        {
            bool result = false;
            if (File.Exists(SQLLocalDB.LinkInfoFileName))
            {
                StringList loadList = new StringList();
                loadList.LoadFromFile(SQLLocalDB.LinkInfoFileName);
                for (int i = 0; i < loadList.Count; i++)
                {
                    var line = loadList[i];
                    if (line.StartsWith(";"))
                    {
                        continue;
                    }
                    var val = line.Split('=');
                    if (val.Length == 2)
                    {
                        LinkInfo.Add(val[0], val[1]);
                    }
                }
                result = true;
            }
            return result;
        }

        private bool LoadFromFile<T>(IList<T> DataList)
        {
            return false;
        }

        private bool LoadFromSQL<T>(IList<T> DataList, int TableKind)
        {
            string DataName = string.Empty;
            int DataIndex = 0;
            bool result = false;
            if (FQuery.FieldCount > 0)
            {
                result = true;
                for (var i = 0; i < FQuery.FieldCount; i++)
                {
                    T pItem = (T)OnMakeData(DataName, DataIndex, FQuery);
                    if ((TableKind == 0) && (i != DataIndex))
                    {
                        M2Share.MainOutMessage("CRITICAL ERROR!!! Record Index does not match in StdItem DB " + DataIndex.ToString());
                    }
                    else if ((TableKind == 1) && (i + 1 != DataIndex))
                    {
                        M2Share.MainOutMessage("CRITICAL ERROR!!! Record Index does not match in StdItem DB " + DataIndex.ToString());
                    }
                    if (pItem != null)
                    {
                        DataList.Add(pItem);
                    }
                    //FQuery.Next;
                }
                FInfos.Clear();
                FInfos.Add(FTableName + ":" + FCompareStr + " Load. Count Is " + FQuery.FieldCount, "");
            }
            return result;
        }

        public bool Load<T>(IList<T> DataList, TLoadType LoadType, int TableKind)
        {
            bool result = false;
            FLoadType = LoadType;
            if (DBConnect())
            {
                switch (LoadType)
                {
                    case TLoadType.ltFILE:
                        result = LoadFromFile(DataList);
                        break;
                    case TLoadType.ltSQL:
                        result = LoadFromSQL(DataList, TableKind);
                        break;
                }
            }
            DBDisConnect();
            return result;
        }

        public StringDictionary GetLoadedDataInfos()
        {
            StringDictionary result;
            result = FInfos;
            return result;
        }

        public void SetCompareStr(string CompareStr)
        {
            FCompareStr = CompareStr;
        }

        public virtual void OnGetSelectQuery(string Query, string TableName, string CompareStr)
        {
           
        }

        public virtual object OnMakeData(string pDataName, int pDataIndex, IDataReader Fields)
        {
            return null;
        }

        private bool DBConnect()
        {
            bool result = false;
            if (GetLinkInfo(FLinkInfo))
            {
                if (!FConnected)
                {
                    SQLDetails = new TSQLDetails();
                    SQLDetails.Alias = "MIR_RES";
                    SQLDetails.Server = FLinkInfo["SERVER NAME"];
                    SQLDetails.Database = FLinkInfo["DATABASE NAME"];
                    SQLDetails.User = FLinkInfo["USER NAME"];
                    SQLDetails.Pass = FLinkInfo["PASSWORD"];
                    ConnectionString = "Provider=SQLOLEDB.1;Password=" + SQLDetails.Pass + ";Persist Security Info=True;User ID=" + SQLDetails.User + ";Initial Catalog=" + SQLDetails.Database + ";Data Source=" + SQLDetails.Server;
                    FTableName = FLinkInfo[FTableNameIndex];
                    //FDataBase.Connected = true;
                    //FConnected = FDataBase.State == ConnectionState.Open;
                }
                if (FConnected)
                {
                    //FQuery.Connection = FDataBase;
                    OnGetSelectQuery("", FTableName, FCompareStr);
                    //if (FQuery.SQL.Count > 0)
                    //{
                    //    FQuery.Active = false;
                    //    FQuery.Active = true;
                    //    result = FQuery.Active;
                    //}
                }
            }
            return result;
        }

        private void DBDisConnect()
        {
            // FQuery.Active = false;
            // FDataBase.Connected = false;
        }
    }

    public class TItemMgr : TDataMgr
    {
        public TItemMgr() : base()
        {
            this.FTableNameIndex = "TABLE_STDITEMS";
        }

        public override void OnGetSelectQuery(string Query, string TableName, string CompareStr)
        {
            Query = "SELECT * FROM " + TableName;
        }

        public override object OnMakeData(string pDataName, int pDataIndex, IDataReader Fields)
        {
            TStdItem pitem = new TStdItem();
             pDataIndex = Fields.GetInt32("ID");
             pitem.Name = Fields.GetString("NAME");
             pitem.StdMode = Fields.GetByte("STDMode");
             pitem.Shape = Fields.GetByte("SHAPE");
             pitem.Weight = Fields.GetByte("WEIGHT");
             pitem.AniCount = Fields.GetByte("ANICOUNT");
             pitem.SpecialPwr = Fields.GetInt16("SOURCE");
             pitem.ItemDesc = Fields.GetByte("RESERVED");
             pitem.Looks = Fields.GetInt16("IMGINDEX");
             pitem.DuraMax = Fields.GetInt16("DURAMAX");
             pitem.AC = HUtil32.MakeWord(Fields.GetInt16("AC"), Fields.GetInt16("ACMAX"));
             pitem.MAC = HUtil32.MakeWord(Fields.GetInt16("MAC"), Fields.GetInt16("MACMAX"));
             pitem.DC = HUtil32.MakeWord(Fields.GetInt16("DC"), Fields.GetInt16("DCMAX"));
             pitem.MC = HUtil32.MakeWord(Fields.GetInt16("MC"), Fields.GetInt16("MCMAX"));
             pitem.SC = HUtil32.MakeWord(Fields.GetInt16("SC"), Fields.GetInt16("SCMAX"));
             pitem.Need = Fields.GetByte("NEED");
             pitem.NeedLevel = Fields.GetByte("NEEDLEVEL");
             pitem.NeedIdentify = 0;
             pitem.Price = Fields.GetInt32("PRICE");
             pitem.Stock = Fields.GetInt32("STOCK");
             pitem.AtkSpd = Fields.GetByte("ATKSPD");
             pitem.Agility = Fields.GetByte("AGILITY");
             pitem.Accurate = Fields.GetByte("ACCURATE");
             pitem.MgAvoid = Fields.GetByte("MGAVOID");
             pitem.Strong = Fields.GetByte("STRONG");
             pitem.Undead = Fields.GetByte("UNDEAD");
             pitem.HpAdd = Fields.GetInt32("HPADD");
             pitem.MpAdd = Fields.GetInt32("MPADD");
             pitem.ExpAdd = Fields.GetInt32("EXPADD");
             pitem.EffType1 = Fields.GetByte("EFFTYPE1");
             pitem.EffRate1 = Fields.GetByte("EFFRATE1");
             pitem.EffValue1 = Fields.GetByte("EFFVALUE1");
             pitem.EffType2 = Fields.GetByte("EFFTYPE2");
             pitem.EffRate2 = Fields.GetByte("EFFRATE2");
             pitem.EffValue2 = Fields.GetByte("EFFVALUE2");
             pitem.Slowdown = Fields.GetByte("SLOWDOWN");
             pitem.Tox = Fields.GetByte("TOX");
             pitem.ToxAvoid = Fields.GetByte("TOXAVOID");
             pitem.UniqueItem = Fields.GetByte("UNIQUEITEM");
             pitem.OverlapItem = Fields.GetByte("OVERLAPITEM");
             pitem.light = Fields.GetByte("LIGHT");
             pitem.ItemType = Fields.GetByte("ITEMTYPE");
             pitem.ItemSet = Fields.GetInt16("ITEMSET");
             pitem.Reference = Fields.GetString("REFERENCE");
            pDataName = pitem.Name;
            return pitem;
        }
    }

    public class TMonsterMgr : TDataMgr
    {
        public TMonsterMgr() : base()
        {
            this.FTableNameIndex = "TABLE_MONSTER";
        }

        public override void OnGetSelectQuery(string Query, string TableName, string CompareStr)
        {
            Query = "SELECT * FROM " + TableName;
        }

        public override object OnMakeData(string pDataName, int pDataIndex, IDataReader Fields)
        {
            TMonsterInfo pitem = new TMonsterInfo();
            pDataIndex = Fields.GetInt32("ID");
            pitem.Name = Fields.GetString("NAME");
            pitem.Race = Fields.GetByte("RACE");
            pitem.RaceImg = Fields.GetByte("RACEIMG");
            pitem.Appr = Fields.GetUInt16("IMGINDEX");
            pitem.Level = Fields.GetByte("LV");
            pitem.LifeAttrib = Fields.GetByte("UNDEAD");
            pitem.CoolEye = Fields.GetByte("COOLEYE");
            pitem.Exp = Fields.GetInt16("EXP");
            pitem.HP = Fields.GetInt16("HP");
            pitem.MP = Fields.GetInt16("MP");
            pitem.AC = Fields.GetByte("AC");
            pitem.MAC = Fields.GetByte("MAC");
            pitem.DC = Fields.GetByte("DC");
            pitem.MaxDC = Fields.GetByte("DCMAX");
            pitem.MC = Fields.GetByte("MC");
            pitem.SC = Fields.GetByte("SC");
            pitem.Speed = Fields.GetByte("AGILITY");
            pitem.Hit = Fields.GetByte("ACCURATE");
            pitem.WalkSpeed = (short)HUtil32._MAX(200, Fields.GetInt32("WALK_SPD"));
            pitem.WalkStep = (short)HUtil32._MAX(1, Fields.GetInt32("WALKSTEP"));
            pitem.WalkWait = Fields.GetInt16("WALKWAIT");
            pitem.AttackSpeed = Fields.GetInt16("ATTACK_SPD");
            pitem.Tame = Fields.GetInt16("TAME");
            pitem.AntiPush = Fields.GetInt16("ANTIPUSH");
            pitem.AntiUndead = Fields.GetInt16("ANTIUNDEAD");
            pitem.SizeRate = Fields.GetInt16("SIZERATE");
            pitem.AntiStop = Fields.GetInt16("ANTISTOP");
            if (pitem.WalkSpeed < 200)
            {
                pitem.WalkSpeed = 200;
            }
            if (pitem.AttackSpeed < 200)
            {
                pitem.AttackSpeed = 200;
            }
            pDataName = pitem.Name;
            return pitem;
        }
    }

    public class TMonsterItemMgr : TDataMgr
    {
        public TMonsterItemMgr() : base()
        {
            this.FTableNameIndex = "TABLE_MOBITEM";
        }

        public override void OnGetSelectQuery(string Query, string TableName, string CompareStr)
        {
            Query = "SELECT * FROM " + TableName + " WHERE MOBNAME=\'" + CompareStr + "\'";
        }

        public override object OnMakeData(string pDataName, int pDataIndex, IDataReader Fields)
        {
            TMonItemInfo pitem = new TMonItemInfo();
            pDataIndex = Fields.GetInt32("ID");
            pitem.SelPoint = Fields.GetInt32("SELPOINT") - 1;
            pitem.MaxPoint = Fields.GetInt32("MAXPOINT");
            pitem.ItemName = Fields.GetString("ITEMNAME");
            pitem.Count = Fields.GetInt32("COUNT");
            pDataName = pitem.ItemName;
            return pitem;
        }

    }

    public class TMagicMgr : TDataMgr
    {
        public TMagicMgr() : base()
        {
            this.FTableNameIndex = "TABLE_MAGIC";
        }

        public override void OnGetSelectQuery(string Query, string TableName, string CompareStr)
        {
            Query = "SELECT * FROM " + TableName;
        }

        public override object OnMakeData(string pDataName, int pDataIndex, IDataReader Fields)
        {
            TDefMagic pitem = new TDefMagic();
            pitem.MagicId = Fields.GetInt16("ID");
            pitem.MagicName = Fields.GetString("NAME");
            pitem.EffectType = Fields.GetByte("EFFECTTYPE");
            pitem.Effect = Fields.GetByte("EFFECT");
            pitem.Spell = Fields.GetInt16("SPELL");
            pitem.MinPower = Fields.GetInt16("POWER");
            pitem.MaxPower = Fields.GetInt16("MAXPOWER");
            pitem.Job = Fields.GetByte("JOB");
            pitem.NeedLevel[0] = Fields.GetByte("NEEDL1");
            pitem.NeedLevel[1] = Fields.GetByte("NEEDL2");
            pitem.NeedLevel[2] = Fields.GetByte("NEEDL3");
            pitem.NeedLevel[3] = Fields.GetByte("NEEDL3");
            pitem.MaxTrain[0] = Fields.GetInt32("L1TRAIN");
            pitem.MaxTrain[1] = Fields.GetInt32("L2TRAIN");
            pitem.MaxTrain[2] = Fields.GetInt32("L3TRAIN");
            pitem.MaxTrain[3] = pitem.MaxTrain[2];
            pitem.MaxTrainLevel = 3;
            pitem.DelayTime = Fields.GetInt32("DELAY") * 10;
            pitem.DefSpell = Fields.GetByte("DEFSPELL");
            pitem.DefMinPower = Fields.GetByte("DEFPOWER");
            pitem.DefMaxPower = Fields.GetByte("DEFMAXPOWER");
            pitem.Desc = Fields.GetString("DESCR");
            pDataIndex = pitem.MagicId;
            pDataName = pitem.MagicName;
            return pitem;
        }
    }

    public enum TLoadType
    {
        ltFILE,
        ltSQL
    }
}

namespace GameSvr
{
    public class SQLLocalDB
    {
        public static TItemMgr gItemMgr = null;
        public static TMonsterMgr gMonsterMgr = null;
        public static TMonsterItemMgr gMonsterItemMgr = null;
        public static TMagicMgr gMagicMgr = null;
        public const string LinkInfoFileName = ".\\!DBSETUP.TXT";
    }
}

