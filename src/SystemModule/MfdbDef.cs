using System;

namespace SystemModule
{

    public class TUseMagicInfo
    {
        public short MagicId;
        public byte Level;
        public Char Key;
        public int Curtrain;
    }

    public struct THuman
    {
        public string UserName;
        public string MapName;
        public short CX;
        public short CY;
        public byte Dir;
        public byte Hair;
        public byte HairColorR;
        public byte HairColorG;
        public byte HairColorB;
        public byte Sex;
        public byte Job;
        public int Gold;
        public int GameGold;
        public byte Abil_Level;
        public short Abil_HP;
        public short Abil_MP;
        public int Abil_Exp;
        public short[] StatusArr;
        public string HomeMap;
        public short HomeX;
        public short HomeY;
        public int PKPoint;
        public byte AllowParty;
        public byte FreeGulityCount;
        public byte AttackMode;
        public byte IncHealth;
        public byte IncSpell;
        public byte IncHealing;
        public byte FightZoneDie;
        public string UserId;
        public byte DBVersion;
        public byte BonusApply;
        public int BonusPoint;
        public long DailyQuest;
        public byte HorseRide;
        public short CGHIUseTime;
        public Double BodyLuck;
        public bool BoEnableGRecall;
        public byte[] bytes_1;
        public byte[] QuestOpenIndex;
        public byte[] QuestFinIndex;
        public byte[] Quest;
        public byte HorseRace;
        public int SecondsCard;
    }

    public struct TBagItem
    {
        public TUserItem uDress;
        public TUserItem uWeapon;
        public TUserItem uRightHand;
        public TUserItem uHelmet;
        public TUserItem uNecklace;
        public TUserItem uArmRingL;
        public TUserItem uArmRingR;
        public TUserItem uRingL;
        public TUserItem uRingR;
        public TUserItem uBujuk;
        public TUserItem uBelt;
        public TUserItem uBoots;
        public TUserItem uCharm;
        public TUserItem[] Bags;
    }

    // 미르2
    public struct TUseMagic
    {
        public TUseMagicInfo[] Magics;
    } 

    // 미르2
    public struct TSaveItem
    {
        public TUserItem[] Items;
    }

    public struct TMirDBBlockData
    {
        public THuman DBHuman;
        public TBagItem DBBagItem;
        public TUseMagic DBUseMagic;
        public TSaveItem DBSaveItem;
    }

    public struct FDBHeader
    {
        public string[] Title;
        public int LastChangeRecordPosition;
        public Double LastChangeDateTime;
        public int MaxCount;
        public int BlockSize;
        public int Reserved;
        public Double UpdateDateTime;
    }

    public class FDBRecord
    {
        public bool Deleted;
        public Double UpdateDateTime;
        public Char[] Key;
        public TMirDBBlockData Block;
    }

    public struct FDBTemp
    {
        public bool Deleted;
        // delete mark
        public Double UpdateDateTime;
        // TDateTime;
        public string[] Key;
    } // end FDBTemp

    public struct FDBRecordInfo
    {
        public bool Deleted;
        // delete mark
        public Double UpdateDateTime;
        // TDateTime;
        public string[] Key;
        public int Reserved;
    } // end FDBRecordInfo

    // 인덱스 저정 파일의 헤더, DB서버를 종료 하는 순간 인덱스를 저장한다.
    // 인덱스가 유효하게 저장되지 않았다면 Rebuild한다.
    public struct FDBIndexHeader
    {
        public string[] Title;
        public int IdxCount;
        // 인덱스 수
        public int MaxCount;
        // FDBHeader의 MaxCount와 일치해야 한다.
        public int BlankCount;
        public int LastChangeRecordPosition;
        // 마지막으로 고친 레코드의 인덱스
        public Double LastChangeDateTime;
    } // end FDBIndexHeader

    public struct FIndexRcd
    {
        public string[] Key;
        // Key의 크기 주의
        public int Position;
    } 
}

namespace MfdbDef.Units
{
    public class MfdbDef
    {
        public static object CSDBLock = null;
        public static int CurLoading = 0;
        public static int ValidLoading = 0;
        public static int DeleteCount = 0;
        public static int MaxLoading = 0;
        public static bool MirLoaded = false;
        public const string IDXTITLE = "legend of mir database index file 2001/7";
        public const int SIZEOFTHUMAN = 456;
        public const double SIZEOFFDB = 4937 + 8 + 40 * 50 + 4 + 4;
        public const int SIZEOFEIFDB = 4937;

        public void initialization()
        {
            CSDBLock = new object();
        }
    }
}