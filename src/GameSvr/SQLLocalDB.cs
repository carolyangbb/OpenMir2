using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.IO;
using SystemModule;
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
        private readonly Dictionary<object, string> FLinkInfo = null;
        private IDataReader FQuery = null;
        private string FCompareStr = String.Empty;
        private IDbConnection FDataBase = null;
        private bool FConnected = false;
        private string FTableName = String.Empty;
        public string FTableNameIndex = String.Empty;

        public TDataMgr() : base()
        {
            //FQuery = new TAdoQuery(null);
            //FDataBase = new TADOConnection(null);
            FInfos = new StringDictionary();
            FLinkInfo = new Dictionary<object, string>();
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

        private bool GetLinkInfo(Dictionary<object, string> LinkInfo)
        {
            bool result = false;
            if (File.Exists(SQLLocalDB.LinkInfoFileName))
            {
                //LinkInfo.LoadFromFile(SQLLocalDB.LinkInfoFileName);
                result = true;
            }
            return result;
        }

        private bool LoadFromFile(ArrayList DataList)
        {
            return false;
        }

        private bool LoadFromSQL(ArrayList DataList, int TableKind)
        {
            string DataName = string.Empty;
            int DataIndex = 0;
            bool result = false;
            if (FQuery.FieldCount > 0)
            {
                result = true;
                object pItem = null;
                for (var i = 0; i < FQuery.FieldCount; i++)
                {
                    pItem = OnMakeData(DataName, DataIndex, FQuery);
                    if ((TableKind == 0) && (i != DataIndex))
                    {
                        svMain.MainOutMessage("CRITICAL ERROR!!! Record Index does not match in StdItem DB " + DataIndex.ToString());
                    }
                    else if ((TableKind == 1) && (i + 1 != DataIndex))
                    {
                        svMain.MainOutMessage("CRITICAL ERROR!!! Record Index does not match in StdItem DB " + DataIndex.ToString());
                    }
                    if (pItem != null)
                    {
                        DataList.Add(pItem);
                        pItem = null;
                    }
                    //FQuery.Next;
                }
                FInfos.Clear();
                FInfos.Add(FTableName + ":" + FCompareStr + " Load. Count Is " + FQuery.FieldCount, "");
            }
            return result;
        }

        public bool Load(ArrayList DataList, TLoadType LoadType, int TableKind)
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

        public virtual void OnGetSelectQuery(StringDictionary Query, string TableName, string CompareStr)
        {
            Query.Clear();
        }

        public virtual object OnMakeData(string pDataName, int pDataIndex, IDataReader Fields)
        {
            return null;
        }

        private bool DBConnect()
        {
            TSQLDetails SQLDetails;
            bool result = false;
            if (GetLinkInfo(FLinkInfo))
            {
                //if (!FConnected)
                //{
                //    FDataBase.LoginPrompt = false;
                //    SQLDetails.Alias = "MIR_RES";
                //    SQLDetails.Server = FLinkInfo["SERVER NAME"];
                //    SQLDetails.Database = FLinkInfo["DATABASE NAME"];
                //    SQLDetails.User = FLinkInfo["USER NAME"];
                //    SQLDetails.Pass = FLinkInfo["PASSWORD"];
                //    FDataBase.ConnectionString = "Provider=SQLOLEDB.1;Password=" + SQLDetails.Pass + ";Persist Security Info=True;User ID=" + SQLDetails.User + ";Initial Catalog=" + SQLDetails.Database + ";Data Source=" + SQLDetails.Server;
                //    FTableName = FLinkInfo[FTableNameIndex];
                //    FDataBase.Connected = true;
                //    FConnected = FDataBase.State == ConnectionState.Open;
                //}
                //if (FConnected)
                //{
                //    FQuery.Connection = FDataBase;
                //    OnGetSelectQuery(FQuery.SQL, FTableName, FCompareStr);
                //    if (FQuery.SQL.Count > 0)
                //    {
                //        FQuery.Active = false;
                //        FQuery.Active = true;
                //        result = FQuery.Active;
                //    }
                //}
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

        public override void OnGetSelectQuery(StringDictionary Query, string TableName, string CompareStr)
        {
            Query.Clear();
            //Query.Add("SELECT * FROM " + TableName);
        }

        public override object OnMakeData(string pDataName, int pDataIndex,IDataReader  Fields)
        {
            TStdItem pitem = new TStdItem();
           /* pDataIndex = Fields.FieldByName("ID").AsInteger;
            pitem.Name = Fields.FieldByName("NAME").AsString;
            pitem.StdMode = Fields.FieldByName("STDMode").AsInteger;
            pitem.Shape = Fields.FieldByName("SHAPE").AsInteger;
            pitem.Weight = Fields.FieldByName("WEIGHT").AsInteger;
            pitem.AniCount = Fields.FieldByName("ANICOUNT").AsInteger;
            pitem.SpecialPwr = Fields.FieldByName("SOURCE").AsInteger;
            pitem.ItemDesc = Fields.FieldByName("RESERVED").AsInteger;
            pitem.Looks = Fields.FieldByName("IMGINDEX").AsInteger;
            pitem.DuraMax = Fields.FieldByName("DURAMAX").AsInteger;
            pitem.AC = HUtil32.MakeWord(Fields.FieldByName("AC").AsInteger, Fields.FieldByName("ACMAX").AsInteger);
            pitem.MAC = HUtil32.MakeWord(Fields.FieldByName("MAC").AsInteger, Fields.FieldByName("MACMAX").AsInteger);
            pitem.DC = HUtil32.MakeWord(Fields.FieldByName("DC").AsInteger, Fields.FieldByName("DCMAX").AsInteger);
            pitem.MC = HUtil32.MakeWord(Fields.FieldByName("MC").AsInteger, Fields.FieldByName("MCMAX").AsInteger);
            pitem.SC = HUtil32.MakeWord(Fields.FieldByName("SC").AsInteger, Fields.FieldByName("SCMAX").AsInteger);
            pitem.Need = Fields.FieldByName("NEED").AsInteger;
            pitem.NeedLevel = Fields.FieldByName("NEEDLEVEL").AsInteger;
            pitem.NeedIdentify = 0;
            pitem.Price = Fields.FieldByName("PRICE").AsInteger;
            pitem.Stock = Fields.FieldByName("STOCK").AsInteger;
            pitem.AtkSpd = Fields.FieldByName("ATKSPD").AsInteger;
            pitem.Agility = Fields.FieldByName("AGILITY").AsInteger;
            pitem.Accurate = Fields.FieldByName("ACCURATE").AsInteger;
            pitem.MgAvoid = Fields.FieldByName("MGAVOID").AsInteger;
            pitem.Strong = Fields.FieldByName("STRONG").AsInteger;
            pitem.Undead = Fields.FieldByName("UNDEAD").AsInteger;
            pitem.HpAdd = Fields.FieldByName("HPADD").AsInteger;
            pitem.MpAdd = Fields.FieldByName("MPADD").AsInteger;
            pitem.ExpAdd = Fields.FieldByName("EXPADD").AsInteger;
            pitem.EffType1 = Fields.FieldByName("EFFTYPE1").AsInteger;
            pitem.EffRate1 = Fields.FieldByName("EFFRATE1").AsInteger;
            pitem.EffValue1 = Fields.FieldByName("EFFVALUE1").AsInteger;
            pitem.EffType2 = Fields.FieldByName("EFFTYPE2").AsInteger;
            pitem.EffRate2 = Fields.FieldByName("EFFRATE2").AsInteger;
            pitem.EffValue2 = Fields.FieldByName("EFFVALUE2").AsInteger;
            pitem.Slowdown = Fields.FieldByName("SLOWDOWN").AsInteger;
            pitem.Tox = Fields.FieldByName("TOX").AsInteger;
            pitem.ToxAvoid = Fields.FieldByName("TOXAVOID").AsInteger;
            pitem.UniqueItem = Fields.FieldByName("UNIQUEITEM").AsInteger;
            pitem.OverlapItem = Fields.FieldByName("OVERLAPITEM").AsInteger;
            pitem.light = Fields.FieldByName("LIGHT").AsInteger;
            pitem.ItemType = Fields.FieldByName("ITEMTYPE").AsInteger;
            pitem.ItemSet = Fields.FieldByName("ITEMSET").AsInteger;
            pitem.Reference = Fields.FieldByName("REFERENCE").AsString;*/
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

        public override void OnGetSelectQuery(StringDictionary Query, string TableName, string CompareStr)
        {
            Query.Clear();
            //Query.Add("SELECT * FROM " + TableName);
        }

        public override object OnMakeData(string pDataName, int pDataIndex, IDataReader Fields)
        {
            TMonsterInfo pitem = new TMonsterInfo();
            /*pDataIndex = Fields.FieldByName("ID").AsInteger;
            pitem.Name = Fields.FieldByName("NAME").AsString;
            pitem.Race = Fields.FieldByName("RACE").AsInteger;
            pitem.RaceImg = Fields.FieldByName("RACEIMG").AsInteger;
            pitem.Appr = Fields.FieldByName("IMGINDEX").AsInteger;
            pitem.Level = Fields.FieldByName("LV").AsInteger;
            pitem.LifeAttrib = Fields.FieldByName("UNDEAD").AsInteger;
            pitem.CoolEye = Fields.FieldByName("COOLEYE").AsInteger;
            pitem.Exp = Fields.FieldByName("EXP").AsInteger;
            pitem.HP = Fields.FieldByName("HP").AsInteger;
            pitem.MP = Fields.FieldByName("MP").AsInteger;
            pitem.AC = Fields.FieldByName("AC").AsInteger;
            pitem.MAC = Fields.FieldByName("MAC").AsInteger;
            pitem.DC = Fields.FieldByName("DC").AsInteger;
            pitem.MaxDC = Fields.FieldByName("DCMAX").AsInteger;
            pitem.MC = Fields.FieldByName("MC").AsInteger;
            pitem.SC = Fields.FieldByName("SC").AsInteger;
            pitem.Speed = Fields.FieldByName("AGILITY").AsInteger;
            pitem.Hit = Fields.FieldByName("ACCURATE").AsInteger;
            pitem.WalkSpeed = Fields._MAX(200, Fields.FieldByName("WALK_SPD").AsInteger);
            pitem.WalkStep = Fields._MAX(1, Fields.FieldByName("WALKSTEP").AsInteger);
            pitem.WalkWait = Fields.FieldByName("WALKWAIT").AsInteger;
            pitem.AttackSpeed = Fields.FieldByName("ATTACK_SPD").AsInteger;
            pitem.Tame = Fields.FieldByName("TAME").AsInteger;
            pitem.AntiPush = Fields.FieldByName("ANTIPUSH").AsInteger;
            pitem.AntiUndead = Fields.FieldByName("ANTIUNDEAD").AsInteger;
            pitem.SizeRate = Fields.FieldByName("SIZERATE").AsInteger;
            pitem.AntiStop = Fields.FieldByName("ANTISTOP").AsInteger;*/
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

        public override void OnGetSelectQuery(StringDictionary Query, string TableName, string CompareStr)
        {
            Query.Clear();
            //Query.Add("SELECT * FROM " + TableName + " WHERE MOBNAME=\'" + CompareStr + "\'");
        }

        public override object OnMakeData(string pDataName, int pDataIndex, IDataReader Fields)
        {
            TMonItemInfo pitem = new TMonItemInfo();
            /*pDataIndex = Fields.FieldByName("ID").AsInteger;
            pitem.SelPoint = Fields.FieldByName("SELPOINT").AsInteger - 1;
            pitem.MaxPoint = Fields.FieldByName("MAXPOINT").AsInteger;
            pitem.ItemName = Fields.FieldByName("ITEMNAME").AsString;
            pitem.Count = Fields.FieldByName("COUNT").AsInteger;*/
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

        public override void OnGetSelectQuery(StringDictionary Query, string TableName, string CompareStr)
        {
            Query.Clear();
            //Query.Add("SELECT * FROM " + TableName);
        }

        public override object OnMakeData(string pDataName, int pDataIndex, IDataReader Fields)
        {
            TDefMagic pitem = new TDefMagic();
            /*pitem.MagicId = Fields.FieldByName("ID").AsInteger;
            pitem.MagicName = Fields.FieldByName("NAME").AsString;
            pitem.EffectType = Fields.FieldByName("EFFECTTYPE").AsInteger;
            pitem.Effect = Fields.FieldByName("EFFECT").AsInteger;
            pitem.Spell = Fields.FieldByName("SPELL").AsInteger;
            pitem.MinPower = Fields.FieldByName("POWER").AsInteger;
            pitem.MaxPower = Fields.FieldByName("MAXPOWER").AsInteger;
            pitem.Job = Fields.FieldByName("JOB").AsInteger;
            pitem.NeedLevel[0] = Fields.FieldByName("NEEDL1").AsInteger;
            pitem.NeedLevel[1] = Fields.FieldByName("NEEDL2").AsInteger;
            pitem.NeedLevel[2] = Fields.FieldByName("NEEDL3").AsInteger;
            pitem.NeedLevel[3] = Fields.FieldByName("NEEDL3").AsInteger;
            pitem.MaxTrain[0] = Fields.FieldByName("L1TRAIN").AsInteger;
            pitem.MaxTrain[1] = Fields.FieldByName("L2TRAIN").AsInteger;
            pitem.MaxTrain[2] = Fields.FieldByName("L3TRAIN").AsInteger;
            pitem.MaxTrain[3] = pitem.MaxTrain[2];
            pitem.MaxTrainLevel = 3;
            pitem.DelayTime = Fields.FieldByName("DELAY").AsInteger * 10;
            pitem.DefSpell = Fields.FieldByName("DEFSPELL").AsInteger;
            pitem.DefMinPower = Fields.FieldByName("DEFPOWER").AsInteger;
            pitem.DefMaxPower = Fields.FieldByName("DEFMAXPOWER").AsInteger;
            pitem.Desc = Fields.FieldByName("DESCR").AsString;*/
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

