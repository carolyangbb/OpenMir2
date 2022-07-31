using System;

namespace SystemModule
{

    public class TUseMagicInfo
    {
        public ushort MagicId;
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
        // Abil        : TAbility;
        public byte Abil_Level;
        public ushort Abil_HP;
        public ushort Abil_MP;
        public long Abil_Exp;
        public short[] StatusArr;
        // ���� �ð� ���
        public string HomeMap;
        // 16, ����
        public short HomeX;
        public short HomeY;
        // NeckName    : string[20];
        // SkillArr    : array[0..7] of TSkillInfo;
        public int PKPoint;
        public byte AllowParty;
        public byte FreeGulityCount;
        // �������� Ƚ��
        public byte AttackMode;
        // ���� ���� ���
        public byte IncHealth;
        public byte IncSpell;
        public byte IncHealing;
        public byte FightZoneDie;
        // ���� ����忡�� ���� ī��Ʈ
        public string UserId;
        public byte DBVersion;
        // DB�� �Ϻε����͸� ������ ���� �������� �����ߴ��� ���ߴ��� �˱� ����
        public byte BonusApply;
        // BonusAbil   : TNakedAbility;  //�������� �ø� �ɷ�ġ
        // CurBonusAbil: TNakedAbility;  //���� �ö� �ִ� �ɷ�ġ
        public int BonusPoint;
        // HungryState : longword;
        public long DailyQuest;
        // TO PDS:INSERT Fileld
        public byte HorseRide;
        // TestServerResetCount: byte;  //166
        public short CGHIUseTime;
        public Double BodyLuck;
        // 8
        public bool BoEnableGRecall;
        // DailyQuestNumber: word;
        // DailyQuestGetDate: word;
        public byte[] bytes_1;
        // Reserved    : array[0..23] of char; //216] of char;
        public byte[] QuestOpenIndex;
        // 24,
        public byte[] QuestFinIndex;
        // 24,
        public byte[] Quest;
        // 176
        public byte HorseRace;
        public int SecondsCard;
    } // end THuman

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

    // �̸�2
    public struct TUseMagic
    {
        public TUseMagicInfo[] Magics;
    } 

    // �̸�2
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

    public struct FDBRecord
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

    // �ε��� ���� ������ ���, DB������ ���� �ϴ� ���� �ε����� �����Ѵ�.
    // �ε����� ��ȿ�ϰ� ������� �ʾҴٸ� Rebuild�Ѵ�.
    public struct FDBIndexHeader
    {
        public string[] Title;
        public int IdxCount;
        // �ε��� ��
        public int MaxCount;
        // FDBHeader�� MaxCount�� ��ġ�ؾ� �Ѵ�.
        public int BlankCount;
        public int LastChangeRecordPosition;
        // ���������� ��ģ ���ڵ��� �ε���
        public Double LastChangeDateTime;
    } // end FDBIndexHeader

    public struct FIndexRcd
    {
        public string[] Key;
        // Key�� ũ�� ����
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