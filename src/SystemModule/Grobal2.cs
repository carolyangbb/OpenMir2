using System;
using System.Collections;

namespace SystemModule
{
    public class TSafePoint
    {
        public string mapName;
        public short nX;
        public short nY;
    }

    public class TIconInfo
    {
        public ushort wStart;
        public ushort wFrameTime;
        public byte btFrame;
        public byte bo01;
        public int nx = 0;
        public int ny = 0;
    }

    public struct TIconInfoShow
    {
        public long dwCurrentFrame;
        public long dwFrameTick;
    }

    public class TGameConfig
    {
        public bool boGameAssist;
        public bool boWhisperRecord;
        public bool boMaketSystem;
        public bool boNoFog;
        public bool boStallSystem;
        public bool boShowHpBar;
        public bool boShowHpNumber;
        public bool boNoStruck;
        public bool boFastMove;
        public bool boNoWeight;
        public bool boShowFriend;
        public bool boShowRelationship;
        public bool boShowMail;
        public bool boShowRecharging;
        public bool boShowHelp;
        public bool boShowGameShop;
        public bool boGamepath;
    }

    public struct TMsgHeader
    {
        // 霸捞飘客 辑滚 烹脚俊 荤侩
        public uint Code;
        public int SNumber;
        public ushort UserGateIndex;
        public ushort Ident;
        public ushort UserListIndex;
        public ushort temp;
        public int length;
    }

    public class TDefaultMessage
    {
        public int Recog;
        public ushort Ident;
        public ushort Param;
        public ushort Tag;
        public ushort Series;
    }

    // 努扼捞攫飘俊辑 荤侩
    public struct TChrMsg
    {
        public int ident;
        public int x;
        public int y;
        public int dir;
        public int feature;
        public int state;
        public string saying;
        public int sound;
    } // end TChrMsg

    public struct TMessageInfo
    {
        public ushort Ident;
        public ushort wParam;
        public int lParam1;
        public int lParam2;
        public int lParam3;
        public Object sender;
        public Object target;
        public string description;
    }

    public struct TMessageInfoPtr
    {
        public ushort Ident;
        public ushort wParam;
        public int lParam1;
        public int lParam2;
        public int lParam3;
        public Object sender;
        // target  : TObject;
        public long deliverytime;
        // 档馒 矫埃...
        public string descptr;
    } // end TMessageInfoPtr

    public struct TShortMessage
    {
        public ushort Ident;
        public ushort msg;
    } // end TShortMessage

    public struct TMessageBodyW
    {
        public ushort Param1;
        public ushort Param2;
        public ushort Tag1;
        public ushort Tag2;
    } // end TMessageBodyW

    public class TMessageBodyWL : ClientPacket
    {
        public long lParam1;
        public long lParam2;
        public long lTag1;
        public long lTag2;
    }

    public class TCharDesc : ClientPacket
    {
        public int Feature;
        public int Status;
    }

    public struct TPowerClass
    {
        public byte Min;
        public byte Ever;
        public byte Max;
        public byte dummy;
    } // end TPowerClass

    public class TNakedAbility : ClientPacket
    {
        public ushort DC;
        public ushort MC;
        public ushort SC;
        public ushort AC;
        public ushort MAC;
        public ushort HP;
        public ushort MP;
        public ushort Hit;
        public ushort Speed;
        public ushort Reserved;
    }

    public struct TChgAttr
    {
        public byte attr;
        // 函版等 加己 侥喊 1:AC 2:MAC 3:DC 4:MC 5:SC
        public byte min;
        // DC,MC,SC狼 min/max  AC,MAC牢版快 MakeWord(min,max)蔼烙
        public byte max;
    } // end TChgAttr

    public class TStdItem
    {
        public string Name;
        // 酒捞袍 捞抚 (玫窍力老八)
        public byte StdMode;
        public byte Shape;
        // 屈怕喊 捞抚 (枚八)
        public byte Weight;
        // 公霸
        public byte AniCount;
        // 1焊促 农搁 局聪皋捞记 登绰 酒捞袍 (促弗 侩档肺 腹捞 静烙)
        public ushort SpecialPwr;
        // +捞搁 积拱傍拜+瓷仿, -捞搁 攫单靛傍拜+
        // 1~10 碍档
        // -50~-1 攫单靛 瓷仿摹 氢惑
        // -100~-51 攫单靛 瓷仿摹 皑家
        public byte ItemDesc;
        // $01 IDC_UNIDENTIFIED  (酒捞错萍颇捞 救 等 巴, 努扼捞攫飘俊辑父 荤侩凳)
        // $02 IDC_UNABLETAKEOFF (颊俊辑 冻绢瘤瘤 臼澜, 固瘤荐 荤侩 啊瓷)
        // $04 IDC_NEVERTAKEOFF  (颊俊辑 冻绢瘤瘤 臼澜, 固瘤荐 荤侩 阂啊瓷)
        // $08 IDC_DIEANDBREAK   (馒侩酒捞袍俊辑 磷栏搁 柄瘤绰 加己)
        // $10 IDC_NEVERLOSE     (馒侩酒捞袍俊辑 磷绢档 冻绢瘤瘤 臼澜)
        public ushort Looks;
        // 弊覆 锅龋
        public ushort DuraMax;
        public ushort AC;
        // 规绢仿
        public ushort MAC;
        // 付亲仿
        public ushort DC;
        // 单固瘤
        public ushort MC;
        // 贱荤狼 付过 颇况
        public ushort SC;
        // 档荤狼 沥脚仿
        public byte Need;
        // 0:Level, 1:DC, 2:MC, 3:SC
        public byte NeedLevel;
        // 1..60 level value...
        public byte NeedIdentify;
        public int Price;
        // 啊拜
        public int Stock;
        // 焊蜡樊
        public byte AtkSpd;
        // 傍拜加档
        public byte Agility;
        // 刮酶
        public byte Accurate;
        // 沥犬
        public byte MgAvoid;
        // 付过雀乔 -> 付过历亲(sonmg)
        public byte Strong;
        // 碍档
        public byte Undead;
        // 荤磊
        public int HpAdd;
        // 眠啊HP
        public int MpAdd;
        // 眠啊MP
        public int ExpAdd;
        // 眠啊 版氰摹
        public byte EffType1;
        // 瓤苞辆幅1
        public byte EffRate1;
        // 瓤苞犬伏1
        public byte EffValue1;
        // 瓤苞蔼1
        public byte EffType2;
        // 瓤苞辆幅2
        public byte EffRate2;
        // 瓤苞犬伏2
        public byte EffValue2;
        // 瓤苞蔼2
        // --------------------
        // added by sonmg
        public byte Slowdown;
        // 敌拳
        public byte Tox;
        // 吝刀
        public byte ToxAvoid;
        // 吝刀历亲
        public byte UniqueItem;
        // 蜡聪农加己
        // 蜡聪农 --- $01:力访/诀弊饭捞靛 救凳
        // 蜡聪农 --- $02:荐府阂啊
        // 蜡聪农 --- $04:滚府搁荤扼咙(啊规芒俊辑 冻备瘤 臼澜)
        // 蜡聪农 --- $08:背券阂啊(12=4+8 : 背券阂啊,冻崩阂啊)
        public byte OverlapItem;
        // 吝汗倾侩
        public byte light;
        // 蝴阑郴绰 酒捞袍
        // --------------------
        public byte ItemType;
        // 酒捞袍狼 备盒
        public ushort ItemSet;
        // 悸飘 酒捞袍 备盒
        public string Reference;
    } // end TStdItem

    // 固福2
    public class TUserItem
    {
        public int MakeIndex;
        // 辑滚俊辑狼 酒捞袍 牢郸胶(父甸绢 龙锭 牢郸胶 概败咙, 吝汗啊瓷)
        public ushort Index;
        // 钎霖酒捞袍狼 牢郸胶  0:绝澜, 1何磐 矫累窃..
        public ushort Dura;
        public ushort DuraMax;
        // 函版等 郴备己 弥措蔼
        public byte[] Desc;
        // 0..7 酒捞袍 诀弊饭捞靛 惑怕
        // 10 0:诀弊饭捞靛客 惑包 绝澜
        // 1:颇鲍仿 诀弊饭捞靛 酒捞错萍颇捞 救 登菌澜
        // 2:付仿 诀弊饭捞靛 酒捞错萍颇捞 救 登菌澜
        // 3:档仿 诀弊饭捞靛 酒捞错萍颇捞 救 登菌澜
        // 5:傍拜加档 诀弊饭捞靛 酒捞错萍颇捞 救 登菌澜
        // 9:角菩, 器俺咙
        public byte ColorR;
        public byte ColorG;
        public byte ColorB;
        public byte[] Prefix;
    } // end TUserItem

    // 固福2
    public struct TAbility
    {
        public byte Level;
        public byte reserved1;
        public ushort AC;
        // armor class
        public ushort MAC;
        // magic armor class
        public ushort DC;
        // damage class  -> makeword(min/max)
        public ushort MC;
        // magic power class   -> makeword(min/max)
        public ushort SC;
        // sprite energy class    -> makeword(min/max)
        public ushort HP;
        // health point
        public ushort MP;
        // magic point
        public ushort MaxHP;
        // max health point
        public ushort MaxMP;
        // max magic point
        public byte ExpCount;
        public byte ExpMaxCount;
        public int Exp;
        public int MaxExp;
        public ushort Weight;
        public ushort MaxWeight;
        public byte WearWeight;
        public byte MaxWearWeight;
        public byte HandWeight;
        public byte MaxHandWeight;
    }

    // 固福2
    public struct TAddAbility
    {
        // 酒捞袍 馒侩栏肺 疵绢唱绰 瓷仿摹
        public ushort HP;
        public ushort MP;
        public ushort HIT;
        // 沥犬
        public ushort SPEED;
        // 刮酶
        public ushort AC;
        public ushort MAC;
        public ushort DC;
        public ushort MC;
        public ushort SC;
        public ushort AntiPoison;
        // %  // 吝刀历亲
        public ushort PoisonRecover;
        // %
        public ushort HealthRecover;
        // %
        public ushort SpellRecover;
        // %
        public ushort AntiMagic;
        // 付过 雀乔啦 % // => 付过历亲
        public byte Luck;
        // 青款 器牢飘
        public byte UnLuck;
        // 阂青 器牢飘
        public byte WeaponStrong;
        public byte UndeadPower;
        public ushort HitSpeed;
        // added by sonmg
        public byte Slowdown;
        public byte Poison;
    } // end TAddAbility

    public struct TPricesInfo
    {
        public ushort Index;
        public int SellPrice;
    }

    public struct TClientGoods
    {
        public string Name;
        public byte SubMenu;
        public int Price;
        public int Stock;
        // 俺喊酒捞袍牢版快, Item狼 ServerIndex 烙
        // Dura        : word;
        // DuraMax     : word;
        public ushort Grade;
    } // end TClientGoods

    public struct TClientJangwon
    {
        // 厘盔 府胶飘
        public int Num;
        public string GuildName;
        public string CaptaineName1;
        public string CaptaineName2;
        public int SellPrice;
        public string SellState;
    } // end TClientJangwon

    public struct TClientGABoard
    {
        // 厘盔 霸矫魄 府胶飘
        public string WrigteUser;
        public string TitleMsg;
        public int IndexType1;
        public int IndexType2;
        public int IndexType3;
        public int IndexType4;
        public int ReplyCount;
    } // end TClientGABoard

    public struct TClientGADecoration
    {
        // 厘盔 操固扁
        public int Num;
        public string Name;
        public int Price;
        public int ImgIndex;
        public int CaseNum;
    } // end TClientGADecoration

    public class ClientPacket
    { 
        
    }

    public class TClientItem : ClientPacket
    {
        public TStdItem S;
        public int MakeIndex;
        public ushort Dura;
        public ushort DuraMax;
        public byte[] Desc;
    }

    public struct TUserStateInfo
    {
        public int Feature;
        public string UserName;
        public int NameColor;
        public string GuildName;
        // [14]; //荐沥(2004/12/22)
        public string GuildRankName;
        public TClientItem[] UseItems;
        // 8->12
        public bool bExistLover;
        // 楷牢 惑怕(2004/10/27)
        public string LoverName;
    } // end TUserStateInfo

    public struct TDropItem
    {
        // 努扼捞攫飘俊辑 荤侩
        public int Id;
        public ushort X;
        public ushort Y;
        public ushort Looks;
        public long FlashTime;
        // 付瘤阜栏肺 馆娄芭赴 矫埃
        public bool BoFlash;
        public long FlashStepTime;
        public int FlashStep;
        public string Name;
        public bool BoDeco;
        public bool boNonSuch;
        public bool boPickUp;
        public bool boShowName;
    } // end TDropItem

    public struct TDefMagic
    {
        public ushort MagicId;
        public string MagicName;
        // 沫 疵副巴 12->14(努扼捞攫飘客 窃膊 荤侩)
        public byte EffectType;
        public byte Effect;
        public ushort Spell;
        public ushort MinPower;
        public byte[] NeedLevel;
        public int[] MaxTrain;
        public byte MaxTrainLevel;
        // 荐访 饭骇
        public byte Job;
        // 0: 傈荤 1:贱荤  2:档荤   99:葛滴啊瓷
        public int DelayTime;
        // 茄规 金促澜俊 促澜 付过阑 镜 荐 乐绰单 吧府绰 矫埃
        public byte DefSpell;
        public byte DefMinPower;
        public ushort MaxPower;
        public byte DefMaxPower;
        public string Desc;
    } // end TDefMagic

    public class TUserMagic
    {
        public TDefMagic pDef;
        // 馆靛矫 nil捞 酒聪绢具 茄促.
        public ushort MagicId;
        // Magic Index 历厘. 蜡聪农秦具窍哥, 函悼登搁 救凳, 亲惑 0焊促 农促.
        public byte Level;
        public char Key;
        // 荤侩磊啊 瘤沥茄 虐
        public int CurTrain;
    } // end TUserMagic

    public struct TClientMagic
    {
        public Char Key;
        public byte Level;
        public int CurTrain;
        public TDefMagic Def;
    } // end TClientMagic

    // 2003/04/15 模备, 率瘤
    public struct TFriend
    {
        public string CharID;
        public byte Status;
        public string Memo;
    } // end TFriend

    public struct TMail
    {
        public string Sender;
        public string Date;
        public string Mail;
        public byte Status;
    } // end TMail

    public struct TRelationship
    {
        public string CharID;
        public byte Level;
        public byte Sex;
        public byte Status;
        public string Date;
    } // end TRelationship

    public struct TSkillInfo
    {
        public ushort SkillIndex;
        public ushort Reserved;
        public int CurTrain;
    } // end TSkillInfo

    public class TMapItem
    {
        public TUserItem UserItem;
        public string Name;
        public ushort Looks;
        public byte AniCount;
        public byte Reserved;
        public int Count;
        public Object Ownership;
        public long Droptime;
        public Object Droper;
    }

    // 厘盔操固扁 酒捞袍(sonmg)
    public struct TAgitDecoItem
    {
        public string Name;
        public ushort Looks;
        public string MapName;
        public ushort x;
        public ushort y;
        public string Maker;
        public ushort Dura;
    } // end TAgitDecoItem

    public class TVisibleItemInfo
    {
        public byte check;
        public ushort x;
        public ushort y;
        public long Id;
        public string Name;
        public ushort looks;
    } 

    public class TVisibleActor
    {
        public byte check;
        public Object cret;
    } 

    // 甘 俊辑 老绢唱绰 捞亥飘, activate矫难具父 捞亥飘啊 惯积茄促.
    public struct TMapEventInfo
    {
        public byte check;
        public int X;
        public int Y;
        public Object EventObject;
    } // end TMapEventInfo

    public struct TGateInfo
    {
        public byte GateType;
        public Object EnterEnvir;
        public int EnterX;
        public int EnterY;
    }

    public struct TAThing
    {
        public byte Shape;
        public Object AObject;
        public long ATime;
    }

    public class TMapInfo
    {
        /// <summary>
        ///  0: can move  
        ///  1: can't move  
        ///  2: can't move and cant't fly
        /// </summary>
        public byte MoveAttr;
        public bool Door;
        public byte Area;
        public byte Reserved;
        public ArrayList OBJList;
    }

    public struct TUserEntryInfo
    {
        // 荤侩磊 殿废沥焊, logon傈俊 静烙
        public string LoginId;
        public string Password;
        public string UserName;
        // *
        public string SSNo;
        // * 721109-1476110
        public string Phone;
        // 笼傈拳 锅龋
        public string Quiz;
        // *
        public string Answer;
        // *
        public string EMail;
    } // end TUserEntryInfo

    public struct TUserEntryAddInfo
    {
        // temp     : array[0..14] of byte;
        public string Quiz2;
        // *
        public string Answer2;
        // *
        public string Birthday;
        // * 1972/11/09
        public string MobilePhone;
        // 017-6227-1234
        public string Memo1;
        // *
        public string Memo2;
    } // end TUserEntryAddInfo

    public struct TUserCharacterInfo
    {
        // 啊惑技拌俊 甸绢坷扁 傈俊 荤侩磊俊霸 傈崔登绰
        public string EncName;
        // 纳发磐 沥焊
        public byte Sex;
        public byte Hair;
        public byte Job;
        // 0:傈荤 1: 贱荤 2:档荤
        public byte Level;
        public int Feature;
        public string EncEncName;
    } // end TUserCharacterInfo

    public struct TLoadHuman
    {
        public Char[] UsrId;
        public Char[] ChrName;
        // 13 -> 19
        public Char[] UsrAddr;
        public int CertifyCode;
    } // end TLoadHuman

    public struct TMonsterInfo
    {
        public string Name;
        public byte Race;
        // 辑滚狼 AI 橇肺弊伐
        public byte RaceImg;
        // 努扼捞攫飘 橇贰烙 侥喊
        public ushort Appr;
        // 捞固瘤 锅龋
        public byte Level;
        public byte LifeAttrib;
        public byte CoolEye;
        // 传狼 亮澜, 100% 捞搁 篮脚阑 航, 50%捞搁 篮脚阑 杭 犬伏捞 50%
        public ushort Exp;
        public ushort HP;
        public ushort MP;
        public byte AC;
        public byte MAC;
        public byte DC;
        public byte MaxDC;
        public byte MC;
        public byte SC;
        public byte Speed;
        public byte Hit;
        public ushort WalkSpeed;
        public ushort WalkStep;
        public ushort WalkWait;
        public ushort AttackSpeed;
        // ////////////////////////
        // newly added by sonmg.
        public ushort Tame;
        public ushort AntiPush;
        public ushort AntiUndead;
        public ushort SizeRate;
        public ushort AntiStop;
        // ////////////////////////
        public ArrayList ItemList;
    } // end TMonsterInfo

    public struct TZenInfo
    {
        public string MapName;
        public int X;
        public int Y;
        public string MonName;
        public int MonRace;
        public int Area;
        // 裹困 +area, -area rectangle
        public int Count;
        public long MonZenTime;
        // 剐府技牧靛 窜困
        public long StartTime;
        public ArrayList Mons;
        public int SmallZenRate;
        // 2003/06/20 捞亥飘侩 各 贸府
        public int TX;
        public int TY;
        public int ZenShoutType;
        public int ZenShoutMsg;
    } // end TZenInfo

    public struct TMonItemInfo
    {
        public int SelPoint;
        public int MaxPoint;
        public string ItemName;
        public int Count;
    } // end TMonItemInfo

    public struct TMarketProduct
    {
        public string GoodsName;
        public int Count;
        public int ZenHour;
        // hour
        public long ZenTime;
    } // end TMarketProduct

    // QuestDiary侩
    public struct TQDDinfo
    {
        public int Index;
        public string Title;
        public ArrayList SList;
    } // end TQDDinfo

    // 困殴魄概侩 酒捞袍 --------------------------------------------------------
    public class TMarketItem
    {
        public TClientItem Item;
        // 函版等 瓷仿摹绰 咯扁俊 利侩凳.
        public int UpgCount;
        // 眠啊肺 诀弊饭捞靛 等 俺荐
        public int Index;
        // 魄概锅龋
        public int SellPrice;
        // 魄概 啊拜
        public string SellWho;
        // 魄概磊
        public string Selldate;
        // 魄概朝楼(0312311210 = 2003-12-31 12:10 )
        // 1 = 魄概吝 , 2 = 魄概肯丰
        public ushort SellState;
    } // end TMarketItem

    // 困殴魄概 佬扁侩 ----------------------------------------------------------
    public class TMarketLoad
    {
        public TUserItem UserItem;
        // DB 历厘侩
        public int Index;
        // DB 牢郸胶
        public int MarketType;
        // 盒府等 酒捞袍 辆幅
        public int SetType;
        // 悸飘 酒捞袍 辆幅
        public int SellCount;
        public int SellPrice;
        // 魄概 啊拜
        public string ItemName;
        // 酒捞袍捞抚
        public string MarketName;
        // 魄概磊疙
        public string SellWho;
        // 魄概磊
        public string Selldate;
        // 魄概朝楼(0312311210 = 2003-12-31 12:10 )
        public ushort SellState;
        // 1 = 魄概吝 , 2 = 魄概肯丰
        public int IsOK;
    } // end TMarketLoad

    // 酒捞袍 八祸侩 ------------------------------------------------------------
    public class TSearchSellItem
    {
        public string MarketName;
        // 辑滚捞抚_NPC  捞抚捞 荤侩凳
        public string Who;
        // 酒捞袍 魄概磊 八祸矫 荤侩 ,
        public string ItemName;
        // 酒捞袍 捞抚 八祸矫 荤侩
        public int MakeIndex;
        // 酒捞袍狼 蜡聪农 锅龋
        public int ItemType;
        // 酒捞抛 辆幅 八祸矫 荤侩
        public int ItemSet;
        // 酒捞袍 悸飘 炼雀矫 荤侩
        public int SellIndex;
        // 魄概 牢郸胶 酒捞袍 混锭 , 秒家 , 陛咀雀荐殿俊 荤侩
        public int CheckType;
        // DB 狼 眉农鸥涝
        public int IsOK;
        // 搬苞蔼
        public int UserMode;
        // 1= 酒捞袍 荤扁  , 2= 磊脚狼 酒捞袍 八祸
        public ArrayList pList;
    } // end TSearchSellItem

    // 困殴八荤侩....------------------------------------------------------------
    public struct TMarKetReqInfo
    {
        public string UserName;
        public string MarketName;
        public string SearchWho;
        public string SearchItem;
        public int ItemType;
        public int ItemSet;
        public int UserMode;
    } // end TMarKetReqInfo

    // 厘盔霸矫魄 府胶飘 八祸侩....------------------------------------------------------------
    public struct TSearchGaBoardList
    {
        public int AgitNum;
        // 固荤侩
        public string GuildName;
        public int OrgNum;
        // 固荤侩
        public int SrcNum1;
        // 固荤侩
        public int SrcNum2;
        // 固荤侩
        public int SrcNum3;
        // 固荤侩
        public int Kind;
        public string UserName;
        // 固荤侩
        public ArrayList ArticleList;
    } // end TSearchGaBoardList

    public class TGaBoardArticleLoad
    {
        public int AgitNum;
        public string GuildName;
        public int OrgNum;
        public int SrcNum1;
        public int SrcNum2;
        public int SrcNum3;
        public int Kind;
        public string UserName;
        public Char[] Content;
    }

    public struct TShopItem
    {
        public string sItemName;
        public byte btClass;
        public ushort wLooks;
        // Items.wil
        public int wPrice;
        public ushort wShape1;
        public ushort wShape2;
        public string sExplain;
    }

    // ==========内挂部分
    public struct TCItemRule
    {
        public string Name;
        public byte ntype;
        public bool rare;
        public bool pick;
        public bool Show;
    } // end TCItemRule

    public struct TSetupFiltrate
    {
        public string sItemName;
        public TItemType sItemType;
        public bool boHintMsg;
        public bool boPickup;
        public bool boShowName;
    } // end TSetupFiltrate

    public struct TMasterRanking
    {
        public string sMasterName;
        public int nRanking;
    } // end TMasterRanking

    // 物品过滤
    public enum TItemType
    {
        i_All,
        i_Other,
        i_HPMPDurg,
        i_Dress,
        i_Weapon,
        i_Jewelry
    } // end TItemType

}

namespace SystemModule
{
    public class Grobal2
    {
        public const int DEFBLOCKSIZE = 16;
        // ei侩
        // 扁粮 固福2
        public const int MAXBAGITEM = 46;
        public const int MAXHORSEBAG = 30;
        public const int MAXUSERMAGIC = 20;
        public const int MAXSAVEITEM = 100;
        public const int MAXQUESTINDEXBYTE = 24;
        // To PDS:13;  //100;
        public const int MAXQUESTBYTE = 176;
        // TO PDS:100; //13;
        // 努扼捞攫飘俊辑 静烙
        public const int LOGICALMAPUNIT = 40;
        public const int UNITX = 48;
        public const int UNITY = 32;
        public const int HALFX = 24;
        public const int HALFY = 16;
        public const int OS_MOVINGOBJECT = 1;
        public const int OS_ITEMOBJECT = 2;
        public const int OS_EVENTOBJECT = 3;
        public const int OS_GATEOBJECT = 4;
        public const int OS_SWITCHOBJECT = 5;
        public const int OS_MAPEVENT = 6;
        public const int OS_DOOR = 7;
        public const int OS_ROON = 8;
        // StatusArr Size 瘤沥(sonmg 2004/03/19)
        public const int STATUSARR_SIZE = 16;
        public const int EXTRAABIL_SIZE = 7;
        // 2003/07/15 惑怕捞惑 眠啊
        public const int POISON_DECHEALTH = 0;
        // $80000000
        public const int POISON_DAMAGEARMOR = 1;
        // $40000000
        public const int POISON_ICE = 2;
        // $20000000
        public const int POISON_STUN = 3;
        // $10000000
        public const int POISON_SLOW = 4;
        // $08000000
        public const int POISON_STONE = 5;
        // $04000000
        public const int POISON_DONTMOVE = 6;
        // $02000000
        public const int STATE_BLUECHAR = 2;
        public const int STATE_FASTMOVE = 7;
        // $01000000
        public const int STATE_TRANSPARENT = 8;
        // $00800000
        public const int STATE_DEFENCEUP = 9;
        // $00400000
        public const int STATE_MAGDEFENCEUP = 10;
        // $00200000
        public const int STATE_BUBBLEDEFENCEUP = 11;
        // $00100000
        // 2004/03/19 某腐磐 瓤苞 眠啊(sonmg)
        public const int STATE_50LEVELEFFECT = 12;
        // $00080000
        public const int EABIL_DCUP = 0;
        // 鉴埃利栏肺 颇鲍仿阑 棵覆 (老沥 矫埃)
        public const int EABIL_MCUP = 1;
        public const int EABIL_SCUP = 2;
        public const int EABIL_HITSPEEDUP = 3;
        public const int EABIL_HPUP = 4;
        public const int EABIL_MPUP = 5;
        public const int EABIL_PWRRATE = 6;
        // 傍拜仿 饭捞飘 炼沥
        // ItemDesc 狼 加己
        public const int IDC_UNIDENTIFIED = 0x01;
        // 瓷仿 犬牢 救凳
        public const int IDC_UNABLETAKEOFF = 0x02;
        // 颊俊辑 冻绢瘤瘤 臼澜, 固瘤荐 荤侩栏肺 冻绢咙
        public const int IDC_NEVERTAKEOFF = 0x04;
        // 颊俊辑 例措肺 冻绢瘤瘤 臼澜
        public const int IDC_DIEANDBREAK = 0x08;
        // 磷栏搁 柄咙
        public const int IDC_NEVERLOSE = 0x10;
        // 磷绢档 酪绢滚府瘤 臼澜
        public const int STATE_STONE_MODE = 0x00000001;
        // 籍惑阁胶磐狼 葛嚼(籍惑栏肺 乐澜)
        public const int STATE_OPENHEATH = 0x00000002;
        // 眉仿 傍俺惑怕
        public const int HAM_ALL = 0;
        // 葛滴 傍拜
        public const int HAM_PEACE = 1;
        // 乞拳葛靛, 阁胶磐父 傍拜
        public const int HAM_GROUP = 2;
        // 弊缝盔 捞寇 酒公唱 傍拜
        public const int HAM_GUILD = 3;
        // 辨靛盔 捞寇 酒公唱 傍拜
        public const int HAM_PKATTACK = 4;
        // 弧盎捞 措 闰嫡捞
        public const int HAM_MAXCOUNT = 5;
        public const int AREA_FIGHT = 0x01;
        public const int AREA_SAFE = 0x02;
        public const int AREA_FREEPK = 0x04;
        public const int HM_HIT = 0;
        public const int HM_HEAVYHIT = 1;
        public const int HM_BIGHIT = 2;
        public const int HM_POWERHIT = 3;
        public const int HM_LONGHIT = 4;
        public const int HM_WIDEHIT = 5;
        // 2003/03/15 脚痹公傍
        public const int HM_CROSSHIT = 6;
        // 4 镑 嘎澜 -> 8镑 嘎澜
        public const int HM_FIREHIT = 7;
        public const int HM_TWINHIT = 8;
        // 2锅 傍拜
        public const int HM_STONEHIT = 9;
        // 4 镑 嘎澜 -> 8镑 嘎澜
        // ----------------------------
        // SM_??    辑滚 -> 努扼捞攫飘肺
        // 1 ~ 2000
        public const int SM_TEST = 1;
        // 儒抚力绢 疙飞
        public const int SM_STOPACTIONS = 2;
        // 葛电 某腐磐/付过狼 悼累阑 肛冕促.
        // 促弗 甘俊 甸绢埃 版快,
        // 青悼俊 包访 疙飞
        public const int SM_ACTION_MIN = 5;
        public const int SM_THROW = 5;
        public const int SM_RUSH = 6;
        // 菊栏肺 傈柳
        public const int SM_RUSHKUNG = 7;
        // 菊栏肺 傈柳角菩
        public const int SM_FIREHIT = 8;
        // 堪拳搬
        public const int SM_BACKSTEP = 9;
        // 缔吧澜龙,
        public const int SM_TURN = 10;
        public const int SM_WALK = 11;
        public const int SM_SITDOWN = 12;
        public const int SM_RUN = 13;
        public const int SM_HIT = 14;
        public const int SM_HEAVYHIT = 15;
        public const int SM_BIGHIT = 16;
        public const int SM_SPELL = 17;
        public const int SM_POWERHIT = 18;
        public const int SM_LONGHIT = 19;
        // 歹 技霸 锭覆
        public const int SM_DIGUP = 20;
        // 顶颇绊 唱坷促.
        public const int SM_DIGDOWN = 21;
        // 顶颇绊 甸绢啊 见促.
        public const int SM_FLYAXE = 22;
        public const int SM_LIGHTING = 23;
        // 付过 荤侩
        public const int SM_WIDEHIT = 24;
        public const int SM_ACTION_MAX = 25;
        // 2003/03/15 脚痹公傍
        public const int SM_CROSSHIT = 35;
        // 堡浅曼, 林函8鸥老 傍拜
        public const int SM_TWINHIT = 36;
        // 街锋曼, 狐福霸 2锅 傍拜
        public const int SM_STONEHIT = 37;
        // 荤磊饶, 林函8鸥老 倒肺父惦
        public const int SM_WINDCUT = 38;
        // 傍颇级,  菊鸥老 9俺 傍拜
        public const int SM_DRAGONFIRE = 39;
        // 玫锋扁堪(拳锋扁堪)  磊脚林函 鸥老 25俺 傍拜
        public const int SM_CURSE = 40;
        // 历林贱
        // 2004/06/22 脚痹公傍(器铰八, 软趋贱, 竿救贱)
        public const int SM_PULLMON = 41;
        // 器铰八, 缠绢寸辫
        public const int SM_SUCKBLOOD = 42;
        // 软趋贱, 乔甫 弧酒甸烙
        public const int SM_BLINDMON = 43;
        // 竿救贱, 利狼 矫具甫 啊覆
        // FireDragon ------------------------ by Leekg...2003/11/27
        public const int MAGIC_DUN_THUNDER = 70;
        // 侩带怜 锅俺  // FireDragon
        public const int MAGIC_DUN_FIRE1 = 71;
        // 侩带怜 侩鞠 耽绢府
        public const int MAGIC_DUN_FIRE2 = 72;
        // 侩带怜 侩鞠 烙棋飘
        public const int MAGIC_DRAGONFIRE = 73;
        // 侩阂傍拜 磐咙
        public const int MAGIC_FIREBURN = 74;
        // 侩籍惑傍拜 磐咙 鸥坷抚
        public const int MAGIC_SERPENT_1 = 75;
        // 捞公扁 戈玫拳
        public const int MAGIC_JW_EFFECT1 = 76;
        // 厘盔 烙棋飘 1
        public const int MAGIC_FOX_THUNDER = 78;
        // 贱荤厚岿咯快 碍拜
        public const int MAGIC_FOX_FIRE1 = 79;
        // 贱荤厚岿咯快 拳堪
        public const int SM_DRAGON_LIGHTING = 80;
        public const int SM_DRAGON_FIRE1 = 81;
        public const int SM_DRAGON_FIRE2 = 82;
        public const int SM_DRAGON_FIRE3 = 83;
        public const int SM_DRAGON_STRUCK = 85;
        public const int SM_DRAGON_DROPITEM = 86;
        public const int SM_LIGHTING_1 = 87;
        // 付过_1:捞公扁 戈玫拳
        public const int SM_LIGHTING_2 = 88;
        public const int MAGIC_FOX_FIRE2 = 90;
        // 档荤厚岿咯快 气混拌
        public const int MAGIC_FOX_CURSE = 91;
        // 档荤厚岿咯快 历林贱
        public const int MAGIC_SOULBALL_ATT1 = 93;
        // 厚岿玫林 傈扁 傍拜(辟立裹困)
        public const int MAGIC_SOULBALL_ATT2 = 94;
        // 厚岿玫林 傈扁 傍拜(盔芭府)
        public const int MAGIC_SOULBALL_ATT3_1 = 95;
        // 厚岿玫林 傈扁 傍拜(鞘荤扁) 5啊瘤 烙棋飘
        public const int MAGIC_SOULBALL_ATT3_2 = 96;
        public const int MAGIC_SOULBALL_ATT3_3 = 97;
        public const int MAGIC_SOULBALL_ATT3_4 = 98;
        public const int MAGIC_SOULBALL_ATT3_5 = 99;
        public const int MAGIC_SIDESTONE_ATT1 = 100;
        // 龋去扁籍 傈扁 傍拜
        public const int SM_ACTION2_MIN = 1000;
        // SM_READYFIREHIT         = 1000;  //努扼捞攫飘俊辑父 静烙, 堪拳搬 霖厚
        public const int SM_ACTION2_MAX = 1099;
        public const int SM_DIE = 26;
        // 荤扼 咙
        public const int SM_ALIVE = 27;
        public const int SM_MOVEFAIL = 28;
        public const int SM_HIDE = 29;
        public const int SM_DISAPPEAR = 30;
        public const int SM_STRUCK = 31;
        public const int SM_DEATH = 32;
        public const int SM_SKELETON = 33;
        public const int SM_NOWDEATH = 34;
        public const int SM_HEAR = 40;
        public const int SM_FEATURECHANGED = 41;
        public const int SM_USERNAME = 42;
        public const int SM_WINEXP = 44;
        public const int SM_LEVELUP = 45;
        public const int SM_DAYCHANGING = 46;
        public const int SM_LOGON = 50;
        public const int SM_NEWMAP = 51;
        public const int SM_ABILITY = 52;
        public const int SM_HEALTHSPELLCHANGED = 53;
        public const int SM_MAPDESCRIPTION = 54;
        public const int SM_SYSMESSAGE = 100;
        public const int SM_GROUPMESSAGE = 101;
        public const int SM_CRY = 102;
        public const int SM_WHISPER = 103;
        public const int SM_GUILDMESSAGE = 104;
        public const int SM_SYSMSG_REMARK = 105;
        // ITEM ?
        public const int SM_ADDITEM = 200;
        // 酒捞袍阑 货肺 掘澜  Series(荐樊)
        public const int SM_BAGITEMS = 201;
        // 啊规狼 葛电 酒捞袍
        public const int SM_DELITEM = 202;
        // 粹酒辑 绝绢瘤绰 殿狼 捞蜡肺 绝绢咙
        public const int SM_UPDATEITEM = 203;
        // 酒捞袍狼 荤剧捞 函窃
        // Magic
        public const int SM_ADDMAGIC = 210;
        public const int SM_SENDMYMAGIC = 211;
        public const int SM_DELMAGIC = 212;
        public const int SM_USERSTALL = 218;
        public const int SM_OPENSTALL = 219;
        public const int SM_BUYSTALLITEM = 220;
        public const int SM_UPDATESTALLITEM = 221;
        public const int SM_VERSION_AVAILABLE = 500;
        public const int SM_VERSION_FAIL = 501;
        public const int SM_PASSWD_SUCCESS = 502;
        public const int SM_PASSWD_FAIL = 503;
        public const int SM_NEWID_SUCCESS = 504;
        // 货酒捞叼 肋 父甸绢 脸澜
        public const int SM_NEWID_FAIL = 505;
        // 货酒捞叼 父甸扁 角菩
        public const int SM_CHGPASSWD_SUCCESS = 506;
        public const int SM_CHGPASSWD_FAIL = 507;
        public const int SM_QUERYCHR = 520;
        // 某腐府胶飘
        public const int SM_NEWCHR_SUCCESS = 521;
        public const int SM_NEWCHR_FAIL = 522;
        public const int SM_DELCHR_SUCCESS = 523;
        public const int SM_DELCHR_FAIL = 524;
        public const int SM_STARTPLAY = 525;
        public const int SM_STARTFAIL = 526;
        public const int SM_QUERYCHR_FAIL = 527;
        public const int SM_OUTOFCONNECTION = 528;
        // 楷搬 秦力凳
        public const int SM_PASSOK_SELECTSERVER = 529;
        public const int SM_SELECTSERVER_OK = 530;
        public const int SM_NEEDUPDATE_ACCOUNT = 531;
        // 拌沥狼 沥焊甫 促矫 涝仿窍扁 官恩 芒..
        public const int SM_UPDATEID_SUCCESS = 532;
        public const int SM_UPDATEID_FAIL = 533;
        public const int SM_PASSOK_WRONGSSN = 534;
        public const int SM_NOT_IN_SERVICE = 535;
        public const int SM_SEND_PUBLICKEY = 536;
        public const int SM_FOXSTATE = 537;
        // 厚岿玫林 惑怕
        public const int SM_DROPITEM_SUCCESS = 600;
        // 酒捞袍 滚府扁 己傍
        public const int SM_DROPITEM_FAIL = 601;
        public const int SM_ITEMSHOW = 610;
        public const int SM_ITEMHIDE = 611;
        public const int SM_OPENDOOR_OK = 612;
        public const int SM_OPENDOOR_LOCK = 613;
        public const int SM_CLOSEDOOR = 614;
        public const int SM_TAKEON_OK = 615;
        // 馒侩 己傍, + New Feature
        public const int SM_TAKEON_FAIL = 616;
        // 馒侩 角菩
        public const int SM_EXCHGTAKEON_OK = 617;
        // 馒侩酒捞袍 背券 己傍
        public const int SM_EXCHGTAKEON_FAIL = 618;
        // 馒侩酒捞袍 背券 角菩
        public const int SM_TAKEOFF_OK = 619;
        // 哈扁 己傍, + New Feature
        public const int SM_TAKEOFF_FAIL = 620;
        public const int SM_SENDUSEITEMS = 621;
        // 馒侩 酒捞袍 葛滴 焊晨
        public const int SM_WEIGHTCHANGED = 622;
        public const int SM_CLEAROBJECTS = 633;
        public const int SM_CHANGEMAP = 634;
        public const int SM_EAT_OK = 635;
        public const int SM_EAT_FAIL = 636;
        public const int SM_BUTCH = 637;
        public const int SM_MAGICFIRE = 638;
        // 付过 惯荤凳  CM_SPELL -> SM_SPELL + SM_MAGICFIRE
        public const int SM_MAGICFIRE_FAIL = 639;
        public const int SM_MAGIC_LVEXP = 640;
        public const int SM_SOUND = 641;
        public const int SM_DURACHANGE = 642;
        public const int SM_MERCHANTSAY = 643;
        public const int SM_MERCHANTDLGCLOSE = 644;
        public const int SM_SENDGOODSLIST = 645;
        public const int SM_SENDUSERSELL = 646;
        public const int SM_SENDBUYPRICE = 647;
        public const int SM_USERSELLITEM_OK = 648;
        public const int SM_USERSELLITEM_FAIL = 649;
        public const int SM_BUYITEM_SUCCESS = 650;
        public const int SM_BUYITEM_FAIL = 651;
        public const int SM_SENDDETAILGOODSLIST = 652;
        public const int SM_GOLDCHANGED = 653;
        public const int SM_CHANGELIGHT = 654;
        public const int SM_LAMPCHANGEDURA = 655;
        public const int SM_CHANGENAMECOLOR = 656;
        public const int SM_CHARSTATUSCHANGED = 657;
        public const int SM_SENDNOTICE = 658;
        public const int SM_GROUPMODECHANGED = 659;
        public const int SM_CREATEGROUP_OK = 660;
        public const int SM_CREATEGROUP_FAIL = 661;
        public const int SM_GROUPADDMEM_OK = 662;
        public const int SM_GROUPDELMEM_OK = 663;
        public const int SM_GROUPADDMEM_FAIL = 664;
        public const int SM_GROUPDELMEM_FAIL = 665;
        public const int SM_GROUPCANCEL = 666;
        public const int SM_GROUPMEMBERS = 667;
        public const int SM_SENDUSERREPAIR = 668;
        public const int SM_USERREPAIRITEM_OK = 669;
        public const int SM_USERREPAIRITEM_FAIL = 670;
        public const int SM_SENDREPAIRCOST = 671;
        public const int SM_DEALMENU = 673;
        public const int SM_DEALTRY_FAIL = 674;
        public const int SM_DEALADDITEM_OK = 675;
        public const int SM_DEALADDITEM_FAIL = 676;
        public const int SM_DEALDELITEM_OK = 677;
        public const int SM_DEALDELITEM_FAIL = 678;
        // SM_DEALREMOTEADDITEM_OK = 679;
        // SM_DEALREMOTEDELITEM_OK = 680;
        public const int SM_DEALCANCEL = 681;
        // 档吝俊 芭贰 秒家凳
        public const int SM_DEALREMOTEADDITEM = 682;
        // 惑措规捞 背券 酒捞袍阑 眠啊
        public const int SM_DEALREMOTEDELITEM = 683;
        // 惑措规捞 背券 酒捞袍阑 画
        public const int SM_DEALCHGGOLD_OK = 684;
        public const int SM_DEALCHGGOLD_FAIL = 685;
        public const int SM_DEALREMOTECHGGOLD = 686;
        public const int SM_DEALSUCCESS = 687;
        public const int SM_SENDUSERSTORAGEITEM = 700;
        public const int SM_STORAGE_OK = 701;
        public const int SM_STORAGE_FULL = 702;
        // 歹 焊包 给 窃.
        public const int SM_STORAGE_FAIL = 703;
        // 焊包 俊矾
        public const int SM_SAVEITEMLIST = 704;
        public const int SM_TAKEBACKSTORAGEITEM_OK = 705;
        public const int SM_TAKEBACKSTORAGEITEM_FAIL = 706;
        public const int SM_TAKEBACKSTORAGEITEM_FULLBAG = 707;
        public const int SM_AREASTATE = 708;
        // 救傈,措访,老馆..
        public const int SM_DELITEMS = 709;
        public const int SM_READMINIMAP_OK = 710;
        public const int SM_READMINIMAP_FAIL = 711;
        public const int SM_SENDUSERMAKEDRUGITEMLIST = 712;
        public const int SM_MAKEDRUG_SUCCESS = 713;
        public const int SM_MAKEDRUG_FAIL = 714;
        public const int SM_ALLOWPOWERHIT = 715;
        public const int SM_NORMALEFFECT = 716;
        // 扁夯 瓤苞
        // 酒捞袍 力炼
        public const int SM_SENDUSERMAKEITEMLIST = 717;
        public const int SM_CHANGEGUILDNAME = 750;
        // 辨靛狼 捞抚 趣狼 辨靛郴狼 流氓捞抚捞 函版
        public const int SM_SENDUSERSTATE = 751;
        public const int SM_SUBABILITY = 752;
        public const int SM_OPENGUILDDLG = 753;
        public const int SM_OPENGUILDDLG_FAIL = 754;
        public const int SM_SENDGUILDHOME = 755;
        public const int SM_SENDGUILDMEMBERLIST = 756;
        public const int SM_GUILDADDMEMBER_OK = 757;
        public const int SM_GUILDADDMEMBER_FAIL = 758;
        public const int SM_GUILDDELMEMBER_OK = 759;
        public const int SM_GUILDDELMEMBER_FAIL = 760;
        public const int SM_GUILDRANKUPDATE_FAIL = 761;
        public const int SM_BUILDGUILD_OK = 762;
        public const int SM_BUILDGUILD_FAIL = 763;
        public const int SM_DONATE_FAIL = 764;
        public const int SM_DONATE_OK = 765;
        public const int SM_MYSTATUS = 766;
        public const int SM_MENU_OK = 767;
        // description栏肺 皋技瘤 傈崔
        public const int SM_GUILDMAKEALLY_OK = 768;
        public const int SM_GUILDMAKEALLY_FAIL = 769;
        public const int SM_GUILDBREAKALLY_OK = 770;
        public const int SM_GUILDBREAKALLY_FAIL = 771;
        public const int SM_DLGMSG = 772;
        public const int SM_SPACEMOVE_HIDE = 800;
        // 鉴埃捞悼 荤扼咙
        public const int SM_SPACEMOVE_SHOW = 801;
        // 唱鸥巢
        public const int SM_RECONNECT = 802;
        public const int SM_GHOST = 803;
        // 拳搁俊 唱鸥抄 儡惑烙
        public const int SM_SHOWEVENT = 804;
        public const int SM_HIDEEVENT = 805;
        public const int SM_SPACEMOVE_HIDE2 = 806;
        // 鉴埃捞悼 荤扼咙
        public const int SM_SPACEMOVE_SHOW2 = 807;
        // 唱鸥巢
        public const int SM_SPACEMOVE_SHOW_NO = 808;
        // 唱鸥巢(捞棋飘 绝澜)
        public const int SM_TIMECHECK_MSG = 810;
        // 努扼捞攫飘俊辑 矫埃
        public const int SM_ADJUST_BONUS = 811;
        // 焊呈胶 器牢飘甫 炼沥窍扼.
        // Frined System -------------
        public const int SM_FRIEND_DELETE = 812;
        // 模备 昏力
        public const int SM_FRIEND_INFO = 813;
        // 模备 眠啊 棺 沥焊函版
        public const int SM_FRIEND_RESULT = 814;
        // 模备包访 搬苞蔼 傈价
        // Tag System ----------------
        public const int SM_TAG_ALARM = 815;
        // 率瘤吭澜 舅覆
        public const int SM_TAG_LIST = 816;
        // 率瘤府胶飘
        public const int SM_TAG_INFO = 817;
        // 率瘤沥焊 函版
        public const int SM_TAG_REJECT_LIST = 818;
        // 芭何磊 府胶飘
        public const int SM_TAG_REJECT_ADD = 819;
        // 芭何磊 眠啊
        public const int SM_TAG_REJECT_DELETE = 820;
        // 芭何磊 昏力
        public const int SM_TAG_RESULT = 821;
        // 率瘤包访 搬苞蔼 傈价
        // User System ---------------
        public const int SM_USER_INFO = 822;
        // 蜡历狼 立加惑怕棺 甘沥焊傈价
        // RelationShip --------------
        public const int SM_LM_LIST = 823;
        // 包拌 府胶飘
        public const int SM_LM_OPTION = 824;
        // 包拌 可记
        public const int SM_LM_REQUEST = 825;
        // 包拌 汲沥 夸备
        public const int SM_LM_DELETE = 826;
        // 包拌 昏力
        public const int SM_LM_RESULT = 827;
        // 包拌 搬苞蔼 傈价
        // 困殴魄概 ---------------------
        public const int SM_MARKET_LIST = 828;
        // 困殴府胶飘傈价
        public const int SM_MARKET_RESULT = 829;
        // 困殴搬苞  傈价
        // 巩颇厘盔 ---------------------
        public const int SM_GUILDAGITLIST = 830;
        // 厘盔 魄概 格废
        public const int SM_GUILDAGITDEALMENU = 831;
        // 厘盔芭贰
        // 厘盔霸矫魄
        public const int SM_GABOARD_LIST = 832;
        // 厘盔霸矫魄 府胶飘
        public const int SM_GABOARD_READ = 833;
        // 厘盔霸矫魄 臂佬扁
        public const int SM_GABOARD_NOTICE_OK = 834;
        // 厘盔霸矫魄 傍瘤荤亲 静扁 OK
        public const int SM_GABOARD_NOTICE_FAIL = 835;
        // 厘盔霸矫魄 傍瘤荤亲 静扁 FAIL
        // 厘盔操固扁
        public const int SM_DECOITEM_LIST = 836;
        // 厘盔操固扁 酒捞袍 府胶飘
        public const int SM_DECOITEM_LISTSHOW = 837;
        // 厘盔操固扁 酒捞袍 府胶飘
        // 弊缝 搬己 犬牢
        public const int SM_CREATEGROUPREQ = 838;
        // 弊缝 搬己 犬牢
        public const int SM_ADDGROUPMEMBERREQ = 839;
        // 弊缝 搬己 犬牢
        // RelationShip (cont.)--------------
        public const int SM_LM_DELETE_REQ = 840;
        // 包拌 昏力 犬牢
        public const int SM_MAA_LIST = 850;
        // 师徒数据
        // 1000 ~ 1099  咀记栏肺 抗距
        public const int SM_OPENHEALTH = 1100;
        // 眉仿捞 惑措规俊 焊烙
        public const int SM_CLOSEHEALTH = 1101;
        // 眉仿捞 惑措规俊霸 焊捞瘤 臼澜
        public const int SM_BREAKWEAPON = 1102;
        public const int SM_INSTANCEHEALGUAGE = 1103;
        public const int SM_CHANGEFACE = 1104;
        // 函脚...
        public const int SM_NEXTTIME_PASSWORD = 1105;
        // 促澜锅俊绰 厚剐锅龋 涝仿 葛靛捞促.
        public const int SM_CHECK_CLIENTVALID = 1106;
        // 努扼捞攫飘狼 荐沥 咯何 犬牢
        public const int SM_LOOPNORMALEFFECT = 1107;
        // 风橇 烙棋飘 瓤苞
        public const int SM_LOOPSCREENEFFECT = 1108;
        // 拳搁 捞棋飘
        public const int SM_PLAYDICE = 1200;
        public const int SM_PLAYROCK = 1201;
        public const int SM_ATTACKMODE = 1202;
        // 2003/02/11 弊缝盔 困摹 沥焊
        public const int SM_GROUPPOS = 1312;
        // UpgradeItem_Result ---------------- by sonmg...2003/10/02
        public const int SM_UPGRADEITEM_RESULT = 1300;
        // 般摹扁
        public const int SM_COUNTERITEMCHANGE = 1301;
        public const int SM_USERSELLCOUNTITEM_OK = 1302;
        public const int SM_USERSELLCOUNTITEM_FAIL = 1303;
        public const int SM_CANCLOSE_OK = 1304;
        public const int SM_CANCLOSE_FAIL = 1305;
        public const int SM_GAMECONFIG = 1306;
        public const int SM_GAMEGOLDCHANGED = 1310;
        public const int SM_SPECOFFERITEM = 1320;
        public const int SM_OFFERITEM = 1321;
        public const int SM_BUGITEMFAIL = 1322;
        public const int SM_PRESENDITEMFAIL = 1323;
        public const int SM_REFICONINFO = 1324;
        // CM_??   努扼捞攫飘 -> 辑滚肺
        // 2000 ~ 4000
        public const int CM_PROTOCOL = 2000;
        public const int CM_IDPASSWORD = 2001;
        public const int CM_ADDNEWUSER = 2002;
        public const int CM_CHANGEPASSWORD = 2003;
        public const int CM_UPDATEUSER = 2004;
        // ----------------------------
        public const int CM_QUERYCHR = 100;
        public const int CM_NEWCHR = 101;
        public const int CM_DELCHR = 102;
        public const int CM_SELCHR = 103;
        public const int CM_SELECTSERVER = 104;
        // 辑滚甫 急琶 (+ 辑滚捞抚)
        // 3000 - 3099 努扼捞攫飘 捞悼 皋技瘤档 抗距
        // 辑滚俊辑 捞悼 皋技瘤档 0..99 荤捞 捞绢具 茄促.
        public const int CM_THROW = 3005;
        public const int CM_TURN = 3010;
        // CM_TURN - 3000 = SM_TURN 痹蘑阑 馆靛矫 瘤难具 窃
        public const int CM_WALK = 3011;
        public const int CM_SITDOWN = 3012;
        public const int CM_RUN = 3013;
        public const int CM_HIT = 3014;
        public const int CM_HEAVYHIT = 3015;
        public const int CM_BIGHIT = 3016;
        public const int CM_SPELL = 3017;
        public const int CM_POWERHIT = 3018;
        // 歹 技霸 锭覆
        public const int CM_LONGHIT = 3019;
        // 歹 技霸 锭覆
        public const int CM_WIDEHIT = 3024;
        public const int CM_FIREHIT = 3025;
        public const int CM_SAY = 3030;
        // 2003/03/15 脚痹公傍
        public const int CM_CROSSHIT = 3035;
        public const int CM_TWINHIT = 3036;
        public const int CM_QUERYUSERNAME = 80;
        // QUERY 矫府令 疙飞绢
        public const int CM_QUERYBAGITEMS = 81;
        public const int CM_QUERYUSERSTATE = 82;
        // 鸥牢狼 惑怕 焊扁
        public const int CM_DROPITEM = 1000;
        public const int CM_PICKUP = 1001;
        public const int CM_OPENDOOR = 1002;
        public const int CM_TAKEONITEM = 1003;
        // 汗厘阑 馒侩
        public const int CM_TAKEOFFITEM = 1004;
        // 汗厘阑 哈绰促
        public const int CM_EXCHGTAKEONITEM = 1005;
        // 馒侩茄 酒捞袍阑 谅快甫 官槽促.(馆瘤,迫骂)
        public const int CM_EAT = 1006;
        // 冈促, 付矫促
        public const int CM_BUTCH = 1007;
        // 档氟窍促
        public const int CM_MAGICKEYCHANGE = 1008;
        public const int CM_SOFTCLOSE = 1009;
        public const int CM_CLICKNPC = 1010;
        public const int CM_MERCHANTDLGSELECT = 1011;
        public const int CM_MERCHANTQUERYSELLPRICE = 1012;
        public const int CM_USERSELLITEM = 1013;
        // 酒捞袍 迫扁
        public const int CM_USERBUYITEM = 1014;
        public const int CM_USERGETDETAILITEM = 1015;
        public const int CM_DROPGOLD = 1016;
        public const int CM_TEST = 1017;
        // 抛胶飘
        public const int CM_LOGINNOTICEOK = 1018;
        public const int CM_GROUPMODE = 1019;
        public const int CM_CREATEGROUP = 1020;
        public const int CM_ADDGROUPMEMBER = 1021;
        public const int CM_DELGROUPMEMBER = 1022;
        public const int CM_USERREPAIRITEM = 1023;
        public const int CM_MERCHANTQUERYREPAIRCOST = 1024;
        public const int CM_DEALTRY = 1025;
        public const int CM_DEALADDITEM = 1026;
        public const int CM_DEALDELITEM = 1027;
        public const int CM_DEALCANCEL = 1028;
        public const int CM_DEALCHGGOLD = 1029;
        // 背券窍绰 捣捞 函版凳
        public const int CM_DEALEND = 1030;
        public const int CM_USERSTORAGEITEM = 1031;
        public const int CM_USERTAKEBACKSTORAGEITEM = 1032;
        public const int CM_WANTMINIMAP = 1033;
        public const int CM_USERMAKEDRUGITEM = 1034;
        public const int CM_OPENGUILDDLG = 1035;
        public const int CM_GUILDHOME = 1036;
        public const int CM_GUILDMEMBERLIST = 1037;
        public const int CM_GUILDADDMEMBER = 1038;
        public const int CM_GUILDDELMEMBER = 1039;
        public const int CM_GUILDUPDATENOTICE = 1040;
        public const int CM_GUILDUPDATERANKINFO = 1041;
        public const int CM_SPEEDHACKUSER = 1042;
        public const int CM_ADJUST_BONUS = 1043;
        public const int CM_GUILDMAKEALLY = 1044;
        public const int CM_GUILDBREAKALLY = 1045;
        // Frined System---------------
        public const int CM_FRIEND_ADD = 1046;
        // 模备眠啊
        public const int CM_FRIEND_DELETE = 1047;
        // 模备昏力
        public const int CM_FRIEND_EDIT = 1048;
        // 模备汲疙 函版
        public const int CM_FRIEND_LIST = 1049;
        // 模备 府胶飘 夸没
        // Tag System -----------------
        public const int CM_TAG_ADD = 1050;
        // 率瘤 眠啊
        public const int CM_TAG_DELETE = 1051;
        // 率瘤 昏力
        public const int CM_TAG_SETINFO = 1052;
        // 率瘤 惑怕 函版
        public const int CM_TAG_LIST = 1053;
        // 率瘤 府胶飘 夸没
        public const int CM_TAG_NOTREADCOUNT = 1054;
        // 佬瘤臼篮 率瘤 俺荐 夸没
        public const int CM_TAG_REJECT_LIST = 1055;
        // 芭何磊 府胶飘
        public const int CM_TAG_REJECT_ADD = 1056;
        // 芭何磊 眠啊
        public const int CM_TAG_REJECT_DELETE = 1057;
        // 芭何磊 昏力
        // Relationship ---------------
        public const int CM_LM_OPTION = 1058;
        // 包拌 劝己 / 厚劝己
        public const int CM_LM_REQUEST = 1059;
        // 包拌 殿废 夸没
        public const int CM_LM_Add = 1060;
        // 包拌 眠啊 ( 郴何利栏肺 静烙 )
        public const int CM_LM_EDIT = 1061;
        // 包拌 荐沥
        public const int CM_LM_DELETE = 1062;
        // 包拌 颇扁
        // UpgradeItem ---------------- by sonmg...2003/10/02
        public const int CM_UPGRADEITEM = 1063;
        // 酒捞袍 诀弊饭捞靛 夸没
        // 墨款飘 酒捞袍
        public const int CM_DROPCOUNTITEM = 1064;
        // 般摹扁 酒捞袍 冻绢哆覆
        // 酒捞袍 力炼
        public const int CM_USERMAKEITEMSEL = 1065;
        public const int CM_USERMAKEITEM = 1066;
        public const int CM_ITEMSUMCOUNT = 1067;
        // 困殴魄概 -------------------
        public const int CM_MARKET_LIST = 1068;
        // 困殴魄概 饭胶飘 夸没
        public const int CM_MARKET_SELL = 1069;
        // 困殴魄概 蜡历 -> NPC
        public const int CM_MARKET_BUY = 1070;
        // 困殴荤扁 NPC -> 蜡历
        public const int CM_MARKET_CANCEL = 1071;
        // 困殴秒家 NPC -> 蜡历
        public const int CM_MARKET_GETPAY = 1072;
        // 困殴陛雀荐 NPC -> 蜡历
        public const int CM_MARKET_CLOSE = 1073;
        // 困殴惑痢 捞侩 场
        // 厘盔 魄概 格废
        public const int CM_GUILDAGITLIST = 1074;
        public const int CM_GUILDAGIT_TAG_ADD = 1075;
        // 厘盔 率瘤 焊郴扁
        // 厘盔霸矫魄
        public const int CM_GABOARD_LIST = 1076;
        // 厘盔霸矫魄 府胶飘
        public const int CM_GABOARD_ADD = 1077;
        // 厘盔霸矫魄 臂静扁
        public const int CM_GABOARD_READ = 1078;
        // 厘盔霸矫魄 臂佬扁
        public const int CM_GABOARD_EDIT = 1079;
        // 厘盔霸矫魄 臂荐沥
        public const int CM_GABOARD_DEL = 1080;
        // 厘盔霸矫魄 臂昏力
        public const int CM_GABOARD_NOTICE_CHECK = 1081;
        // 厘盔霸矫魄 傍瘤荤亲 静扁 眉农
        public const int CM_TAG_ADD_DOUBLE = 1082;
        // 滴疙 悼矫 率瘤 眠啊
        // 厘盔操固扁 -------------------
        public const int CM_DECOITEM_BUY = 1083;
        // 厘盔操固扁 酒捞袍 备涝
        // 弊缝 搬己 犬牢
        public const int CM_CREATEGROUPREQ_OK = 1084;
        // 弊缝 搬己 犬牢
        public const int CM_CREATEGROUPREQ_FAIL = 1085;
        // 弊缝 搬己 犬牢
        public const int CM_ADDGROUPMEMBERREQ_OK = 1086;
        // 弊缝 搬己 犬牢
        public const int CM_ADDGROUPMEMBERREQ_FAIL = 1087;
        // 弊缝 搬己 犬牢
        // Relationship (cont.)---------------
        public const int CM_LM_DELETE_REQ_OK = 1088;
        // 包拌 颇扁 OK
        public const int CM_LM_DELETE_REQ_FAIL = 1089;
        // 包拌 颇扁 FAIL
        public const int CM_CLIENT_CHECKTIME = 1100;
        public const int CM_CANCLOSE = 1101;
        public const int CM_OPENSTALL = 1110;
        public const int CM_UPDATESTALLITEM = 1111;
        public const int CM_BUYSHOPITEM = 1112;
        public const int CM_SHOPPRESEND = 1113;
        public const int CM_GETSHOPITEM = 1114;
        // ----------------------------
        public const int RM_TURN = 10001;
        public const int RM_WALK = 10002;
        public const int RM_RUN = 10003;
        public const int RM_HIT = 10004;
        public const int RM_HEAVYHIT = 10005;
        public const int RM_BIGHIT = 10006;
        public const int RM_SPELL = 10007;
        public const int RM_POWERHIT = 10008;
        public const int RM_SITDOWN = 10009;
        public const int RM_MOVEFAIL = 10010;
        public const int RM_LONGHIT = 10011;
        public const int RM_WIDEHIT = 10012;
        public const int RM_PUSH = 10013;
        public const int RM_FIREHIT = 10014;
        public const int RM_RUSH = 10015;
        public const int RM_RUSHKUNG = 10016;
        // 2003/03/15 脚痹公傍
        public const int RM_CROSSHIT = 10017;
        public const int RM_TWINHIT = 10019;
        public const int RM_DECREFOBJCOUNT = 10018;
        public const int RM_STRUCK = 10020;
        public const int RM_DEATH = 10021;
        public const int RM_DISAPPEAR = 10022;
        // RM_HIDE                 = 10023;
        public const int RM_SKELETON = 10024;
        public const int RM_MAGSTRUCK = 10025;
        // 眉仿捞 捞 矫痢俊辑 粹绰促.
        public const int RM_MAGHEALING = 10026;
        // 鳃傅
        public const int RM_STRUCK_MAG = 10027;
        // 付过栏肺 嘎澜
        public const int RM_MAGSTRUCK_MINE = 10028;
        // 瘤汾揚澜
        public const int RM_STONEHIT = 10029;
        public const int RM_HEAR = 10030;
        public const int RM_WHISPER = 10031;
        public const int RM_CRY = 10032;
        public const int RM_WINDCUT = 10040;
        // 傍颇级
        public const int RM_DRAGONFIRE = 10041;
        // 玫锋扁堪(->拳锋扁堪)
        public const int RM_CURSE = 10042;
        // 历林贱
        public const int RM_LOGON = 10050;
        public const int RM_ABILITY = 10051;
        public const int RM_HEALTHSPELLCHANGED = 10052;
        public const int RM_DAYCHANGING = 10053;
        public const int RM_USERNAME = 10043;
        public const int RM_WINEXP = 10044;
        public const int RM_LEVELUP = 10045;
        public const int RM_CHANGENAMECOLOR = 10046;
        // 2004/06/22 脚痹公傍(器铰八, 软趋贱, 竿救贱)
        public const int RM_PULLMON = 10047;
        // 器铰八, 缠绢寸辫
        public const int RM_SUCKBLOOD = 10048;
        // 软趋贱, 乔甫 弧酒甸烙
        public const int RM_BLINDMON = 10049;
        // 竿救贱, 利狼 矫具甫 啊覆
        public const int RM_GMWHISPER = 10055;
        // 款康磊 葛靛老 锭 庇富(2004/11/18)
        public const int RM_LM_WHISPER = 10056;
        // 楷牢 庇加富
        public const int RM_FOXSTATE = 10057;
        // 厚岿玫林 惑怕
        public const int RM_SYSMESSAGE = 10100;
        public const int RM_REFMESSAGE = 10101;
        public const int RM_GROUPMESSAGE = 10102;
        public const int RM_SYSMESSAGE2 = 10103;
        public const int RM_GUILDMESSAGE = 10104;
        public const int RM_SYSMSG_BLUE = 10105;
        public const int RM_SYSMESSAGE3 = 10106;
        public const int RM_SYSMSG_REMARK = 10107;
        public const int RM_SYSMSG_PINK = 10108;
        public const int RM_SYSMSG_GREEN = 10109;
        public const int RM_ITEMSHOW = 10110;
        public const int RM_ITEMHIDE = 10111;
        public const int RM_OPENDOOR_OK = 10112;
        public const int RM_CLOSEDOOR = 10113;
        public const int RM_SENDUSEITEMS = 10114;
        public const int RM_WEIGHTCHANGED = 10115;
        public const int RM_FEATURECHANGED = 10116;
        public const int RM_CLEAROBJECTS = 10117;
        public const int RM_CHANGEMAP = 10118;
        public const int RM_BUTCH = 10119;
        public const int RM_MAGICFIRE = 10120;
        public const int RM_MAGICFIRE_FAIL = 10121;
        public const int RM_SENDMYMAGIC = 10122;
        public const int RM_MAGIC_LVEXP = 10123;
        public const int RM_SOUND = 10124;
        public const int RM_DURACHANGE = 10125;
        public const int RM_MERCHANTSAY = 10126;
        public const int RM_MERCHANTDLGCLOSE = 10127;
        public const int RM_SENDGOODSLIST = 10128;
        public const int RM_SENDUSERSELL = 10129;
        public const int RM_SENDBUYPRICE = 10130;
        // 惑痢俊辑 荤侩磊狼 酒捞袍阑 荤绰 啊拜
        public const int RM_USERSELLITEM_OK = 10131;
        public const int RM_USERSELLITEM_FAIL = 10132;
        public const int RM_BUYITEM_SUCCESS = 10133;
        public const int RM_BUYITEM_FAIL = 10134;
        public const int RM_SENDDETAILGOODSLIST = 10135;
        public const int RM_GOLDCHANGED = 10136;
        public const int RM_CHANGELIGHT = 10137;
        public const int RM_LAMPCHANGEDURA = 10138;
        public const int RM_CHARSTATUSCHANGED = 10139;
        public const int RM_GROUPCANCEL = 10140;
        public const int RM_SENDUSERREPAIR = 10141;
        public const int RM_SENDREPAIRCOST = 10142;
        public const int RM_USERREPAIRITEM_OK = 10143;
        public const int RM_USERREPAIRITEM_FAIL = 10144;
        // RM_ITEMDURACHANGE       = 10145;
        public const int RM_SENDUSERSTORAGEITEM = 10146;
        public const int RM_SENDUSERSTORAGEITEMLIST = 10147;
        public const int RM_DELITEMS = 10148;
        // 酒捞袍 佬绢 滚覆, 努扼捞攫抛俊 舅覆.
        public const int RM_SENDUSERMAKEDRUGITEMLIST = 10149;
        public const int RM_MAKEDRUG_SUCCESS = 10150;
        public const int RM_MAKEDRUG_FAIL = 10151;
        public const int RM_SENDUSERSPECIALREPAIR = 10152;
        public const int RM_ALIVE = 10153;
        public const int RM_DELAYMAGIC = 10154;
        public const int RM_RANDOMSPACEMOVE = 10155;
        // 酒捞袍 力炼
        public const int RM_SENDUSERMAKEITEMLIST = 10156;
        public const int RM_DIGUP = 10200;
        public const int RM_DIGDOWN = 10201;
        public const int RM_FLYAXE = 10202;
        public const int RM_ALLOWPOWERHIT = 10203;
        public const int RM_LIGHTING = 10204;
        public const int RM_NORMALEFFECT = 10205;
        // 扁夯 瓤苞
        public const int RM_DRAGON_FIRE1 = 10206;
        public const int RM_DRAGON_FIRE2 = 10207;
        public const int RM_DRAGON_FIRE3 = 10208;
        public const int RM_LIGHTING_1 = 10209;
        public const int RM_LIGHTING_2 = 10210;
        public const int RM_MAKEPOISON = 10300;
        public const int RM_CHANGEGUILDNAME = 10301;
        // 辨靛狼 捞抚, 辨靛郴 流氓捞抚 函版
        public const int RM_SUBABILITY = 10302;
        public const int RM_BUILDGUILD_OK = 10303;
        public const int RM_BUILDGUILD_FAIL = 10304;
        public const int RM_DONATE_FAIL = 10305;
        public const int RM_DONATE_OK = 10306;
        public const int RM_MYSTATUS = 10307;
        public const int RM_TRANSPARENT = 10308;
        public const int RM_MENU_OK = 10309;
        public const int RM_SPACEMOVE_HIDE = 10330;
        public const int RM_SPACEMOVE_SHOW = 10331;
        public const int RM_RECONNECT = 10332;
        public const int RM_HIDEEVENT = 10333;
        public const int RM_SHOWEVENT = 10334;
        public const int RM_SPACEMOVE_HIDE2 = 10335;
        public const int RM_SPACEMOVE_SHOW2 = 10336;
        public const int RM_ZEN_BEE = 10337;
        // 厚阜盔面捞 厚阜面阑 父甸绢 辰促.
        public const int RM_DELAYATTACK = 10338;
        // 鸥拜 矫痢阑 嘎眠扁 困秦辑
        public const int RM_SPACEMOVE_SHOW_NO = 10339;
        // 捞棋飘 绝捞 唱鸥巢
        public const int RM_ADJUST_BONUS = 10400;
        // 焊呈胶 器牢飘甫 炼沥窍扼.
        public const int RM_MAKE_SLAVE = 10401;
        // 辑滚捞悼栏肺 何窍啊 蝶扼柯促.
        public const int RM_OPENHEALTH = 10410;
        // 眉仿捞 惑措规俊 焊烙
        public const int RM_CLOSEHEALTH = 10411;
        // 眉仿捞 惑措规俊霸 焊捞瘤 臼澜
        public const int RM_DOOPENHEALTH = 10412;
        public const int RM_BREAKWEAPON = 10413;
        // 公扁啊 柄咙, 局固皋捞记 瓤苞
        public const int RM_INSTANCEHEALGUAGE = 10414;
        public const int RM_CHANGEFACE = 10415;
        // 函脚...
        public const int RM_NEXTTIME_PASSWORD = 10416;
        // 促澜 茄锅篮 厚剐锅龋涝仿 葛靛
        public const int RM_DOSTARTUPQUEST = 10417;
        public const int RM_TAG_ALARM = 10418;
        // 郴何利栏肺 率瘤吭澜舅覆
        public const int RM_LM_DBWANTLIST = 10420;
        // 楷牢荤力 府胶飘盔窃
        public const int RM_LM_DBADD = 10421;
        // 楷牢荤力 府胶飘盔窃
        public const int RM_LM_DBEDIT = 10422;
        // 楷牢荤力 府胶飘盔窃
        public const int RM_LM_DBDELETE = 10423;
        // 楷牢荤力 府胶飘盔窃
        public const int RM_LM_DBGETLIST = 10424;
        // 楷牢荤力 府胶飘掘澜
        public const int RM_LM_LOGOUT = 10425;
        // 楷牢 辆丰甫 舅妨淋
        public const int RM_MA_DBADD = 10426;
        // 添加师徒
        public const int RM_MA_DBEDIT = 10427;
        // 添加师徒
        public const int RM_MA_DBDELETE = 10428;
        // 删除师徒
        public const int RM_LM_DBMATLIST = 10429;
        // 获取师徒信息
        public const int RM_LM_REFMATLIST = 10433;
        // 刷新信息
        public const int RM_DRAGON_EXP = 10430;
        // 侩矫胶袍俊 版氰摹 霖促.
        public const int RM_LOOPNORMALEFFECT = 10431;
        // 风橇 烙棋飘 瓤苞
        public const int RM_LOOPSCREENEFFECT = 10432;
        // 拳搁 捞棋飘
        public const int RM_PLAYDICE = 10500;
        public const int RM_PLAYROCK = 10501;
        // 2003/02/11 弊缝盔 困摹 沥焊
        public const int RM_GROUPPOS = 11008;
        // 墨款飘 酒捞袍
        public const int RM_COUNTERITEMCHANGE = 11011;
        public const int RM_USERSELLCOUNTITEM_OK = 11012;
        public const int RM_USERSELLCOUNTITEM_FAIL = 11013;
        // 酒捞袍 力炼
        public const int RM_SENDUSERMAKEFOODLIST = 11014;
        // 酒捞袍 困殴魄概
        public const int RM_MARKET_LIST = 11015;
        public const int RM_MARKET_RESULT = 11016;
        // 厘盔 魄概 格废
        public const int RM_GUILDAGITLIST = 11017;
        public const int RM_GUILDAGITDEALTRY = 11018;
        // 厘盔霸矫魄
        public const int RM_GABOARD_LIST = 11019;
        // 厘盔霸矫魄 府胶飘
        public const int RM_GABOARD_NOTICE_OK = 11020;
        // 厘盔霸矫魄 傍瘤荤亲 静扁 OK
        public const int RM_GABOARD_NOTICE_FAIL = 11021;
        // 厘盔霸矫魄 傍瘤荤亲 静扁 FAIL
        // 厘盔操固扁
        public const int RM_DECOITEM_LIST = 11022;
        // 厘盔操固扁 酒捞袍 府胶飘
        public const int RM_DECOITEM_LISTSHOW = 11023;
        // 厘盔操固扁 酒捞袍 府胶飘芒 剁快扁
        public const int RM_CANCLOSE_OK = 11024;
        public const int RM_CANCLOSE_FAIL = 11025;
        public const int RM_STALLSTATUS = 11026;
        public const int RM_GAMEGOLDCHANGED = 11027;
        public const int RM_ATTACKMODE = 11028;
        public const int RM_GAMECONFIG = 11029;
        public const int RM_REFICONINFO = 11030;
        // ----------------------------
        // 辑滚埃 皋技瘤辑滚甫 芭摹瘤 臼篮 皋技隆
        public const int ISM_PASSWDSUCCESS = 100;
        // 菩胶况靛 烹苞, Certification+ID
        public const int ISM_CANCELADMISSION = 101;
        // Certification 铰牢秒家..
        public const int ISM_USERCLOSED = 102;
        // 荤侩磊 立加 谗澜
        public const int ISM_USERCOUNT = 103;
        // 捞 辑滚狼 荤侩磊 荐
        public const int ISM_TOTALUSERCOUNT = 104;
        public const int ISM_SHIFTVENTURESERVER = 110;
        public const int ISM_ACCOUNTEXPIRED = 111;
        public const int ISM_GAMETIMEOFTIMECARDUSER = 112;
        public const int ISM_USAGEINFORMATION = 113;
        public const int ISM_FUNC_USEROPEN = 114;
        public const int ISM_FUNC_USERCLOSE = 115;
        public const int ISM_CHECKTIMEACCOUNT = 116;
        public const int ISM_REQUEST_PUBLICKEY = 117;
        public const int ISM_SEND_PUBLICKEY = 118;
        public const int ISM_PREMIUMCHECK = 119;
        // ----------------------------
        public const int ISM_USERSERVERCHANGE = 200;
        public const int ISM_USERLOGON = 201;
        public const int ISM_USERLOGOUT = 202;
        public const int ISM_WHISPER = 203;
        public const int ISM_SYSOPMSG = 204;
        public const int ISM_ADDGUILD = 205;
        public const int ISM_DELGUILD = 206;
        public const int ISM_RELOADGUILD = 207;
        public const int ISM_GUILDMSG = 208;
        public const int ISM_CHATPROHIBITION = 209;
        // 盲陛
        public const int ISM_CHATPROHIBITIONCANCEL = 210;
        // 盲陛秦力
        public const int ISM_CHANGECASTLEOWNER = 211;
        // 荤合己 林牢 函版
        public const int ISM_RELOADCASTLEINFO = 212;
        // 荤合己沥焊啊 函版凳
        public const int ISM_RELOADADMIN = 213;
        // Friend System -------------
        public const int ISM_FRIEND_INFO = 214;
        // 模备沥焊 眠啊
        public const int ISM_FRIEND_DELETE = 215;
        // 模备 昏力
        public const int ISM_FRIEND_OPEN = 216;
        // 模备 矫胶袍 凯扁
        public const int ISM_FRIEND_CLOSE = 217;
        // 模备 矫胶袍 摧扁
        public const int ISM_FRIEND_RESULT = 218;
        // 搬苞蔼 傈价
        // Tag System ----------------
        public const int ISM_TAG_SEND = 219;
        // 率瘤 傈价
        public const int ISM_TAG_RESULT = 220;
        // 搬苞蔼 傈价
        // User System --------------
        public const int ISM_USER_INFO = 221;
        // 蜡历狼 立加惑怕 傈价
        // 2003/06/12 浇饭捞宏 菩摹
        public const int ISM_CHANGESERVERRECIEVEOK = 222;
        // 2003/08/28 盲泼肺弊
        public const int ISM_RELOADCHATLOG = 223;
        // 困殴魄概 凯绊 摧澜
        public const int ISM_MARKETOPEN = 224;
        public const int ISM_MARKETCLOSE = 225;
        // relationship --------------
        // ISM_LM_INFO             = 224;   // 包拌 沥焊 傈价
        // ISM_LM_LEVELINFO        = 225;   // 包拌 饭骇沥焊 傈价
        public const int ISM_LM_DELETE = 226;
        // 力炼 犁丰 格废 ------------(sonmg)
        public const int ISM_RELOADMAKEITEMLIST = 227;
        // 力炼 犁丰 格废 府肺靛
        // 巩盔家券 ------------(sonmg)
        public const int ISM_GUILDMEMBER_RECALL = 228;
        // 巩盔家券
        public const int ISM_RELOADGUILDAGIT = 229;
        // 巩颇厘盔沥焊 府肺靛.
        // 楷牢
        public const int ISM_LM_WHISPER = 230;
        public const int ISM_GMWHISPER = 231;
        // 款康磊 庇富
        // 楷牢(sonmg 2005/04/04)
        public const int ISM_LM_LOGIN = 232;
        // 楷牢 肺弊牢
        public const int ISM_LM_LOGOUT = 233;
        // 楷牢 肺弊酒眶
        public const int ISM_REQUEST_RECALL = 234;
        // 家券 夸没
        public const int ISM_RECALL = 235;
        // 辑滚埃 家券
        public const int ISM_LM_LOGIN_REPLY = 236;
        // 肺弊牢 沁阑 锭 楷牢狼 困摹沥焊
        public const int ISM_LM_KILLED_MSG = 237;
        // 楷牢 混秦 皋矫瘤
        public const int ISM_REQUEST_LOVERRECALL = 238;
        // 楷牢 家券 夸没
        public const int ISM_GUILDWAR = 239;
        // 巩颇傈 脚没楷厘
        // ----------------------------
        public const int DB_LOADHUMANRCD = 100;
        public const int DB_SAVEHUMANRCD = 101;
        public const int DB_SAVEANDCHANGE = 102;
        public const int DB_IDPASSWD = 103;
        public const int DB_NEWUSERID = 104;
        public const int DB_CHANGEPASSWD = 105;
        public const int DB_QUERYCHR = 106;
        public const int DB_NEWCHR = 107;
        public const int DB_GETOTHERNAMES = 108;
        public const int DB_ISVALIDUSER = 111;
        public const int DB_DELCHR = 112;
        public const int DB_ISVALIDUSERWITHID = 113;
        public const int DB_CONNECTIONOPEN = 114;
        public const int DB_CONNECTIONCLOSE = 115;
        public const int DB_SAVELOGO = 116;
        public const int DB_GETACCOUNT = 117;
        public const int DB_SAVESPECFEE = 118;
        public const int DB_SAVELOGO2 = 119;
        public const int DB_GETSERVER = 120;
        public const int DB_CHANGESERVER = 121;
        public const int DB_LOGINCLOSEUSER = 122;
        public const int DB_RUNCLOSEUSER = 123;
        public const int DB_UPDATEUSERINFO = 124;
        // Friend System -------------
        public const int DB_FRIEND_LIST = 125;
        // 模备 府胶飘 夸备
        public const int DB_FRIEND_ADD = 126;
        // 模备 眠啊
        public const int DB_FRIEND_DELETE = 127;
        // 模备 昏力
        public const int DB_FRIEND_OWNLIST = 128;
        // 模备肺 殿废茄 荤恩 府胶飘 夸备
        public const int DB_FRIEND_EDIT = 129;
        // 模备 汲疙 荐沥
        // Tag System ----------------
        public const int DB_TAG_ADD = 130;
        // 率瘤 眠啊
        public const int DB_TAG_DELETE = 131;
        // 率瘤 昏力
        public const int DB_TAG_DELETEALL = 132;
        // 率瘤 傈何 昏力 ( 啊瓷茄巴父 )
        public const int DB_TAG_LIST = 133;
        // 率瘤 府胶飘 眠啊
        public const int DB_TAG_SETINFO = 134;
        // 盟瘤 惑怕 函版
        public const int DB_TAG_REJECT_ADD = 135;
        // 芭何磊 眠啊
        public const int DB_TAG_REJECT_DELETE = 136;
        // 芭何磊 昏力
        public const int DB_TAG_REJECT_LIST = 137;
        // 芭何磊 府胶飘 夸没
        public const int DB_TAG_NOTREADCOUNT = 138;
        // 佬瘤臼篮 率瘤 俺荐 夸没
        // RelationShip --------------
        public const int DB_LM_LIST = 139;
        // 包拌磊 府胶飘 夸备
        public const int DB_LM_ADD = 140;
        // 包拌磊 眠啊
        public const int DB_LM_EDIT = 141;
        // 包拌磊 汲沥 函版
        public const int DB_LM_DELETE = 142;
        // 包拌磊 昏力
        public const int DB_MA_LIST = 143;
        public const int DB_MA_ADD = 144;
        public const int DB_MA_EDIT = 145;
        public const int DB_MA_DELETE = 146;
        public const int DBR_LOADHUMANRCD = 1100;
        public const int DBR_SAVEHUMANRCD = 1101;
        public const int DBR_IDPASSWD = 1103;
        public const int DBR_NEWUSERID = 1104;
        public const int DBR_CHANGEPASSWD = 1105;
        public const int DBR_QUERYCHR = 1106;
        public const int DBR_NEWCHR = 1107;
        public const int DBR_GETOTHERNAMES = 1108;
        public const int DBR_ISVALIDUSER = 1111;
        public const int DBR_DELCHR = 1112;
        public const int DBR_ISVALIDUSERWITHID = 1113;
        public const int DBR_GETACCOUNT = 1117;
        public const int DBR_GETSERVER = 1200;
        public const int DBR_CHANGESERVER = 1201;
        public const int DBR_UPDATEUSERINFO = 1202;
        // Friend System ---------------
        public const int DBR_FRIEND_LIST = 1203;
        // 模备 府胶飘 傈价
        public const int DBR_FRIEND_WONLIST = 1204;
        // 模备肺 殿废茄 荤恩 傈价
        public const int DBR_FRIEND_RESULT = 1205;
        // 疙飞绢俊 措茄 搬苞蔼
        // Tag System ------------------
        public const int DBR_TAG_LIST = 1206;
        // 率瘤 府胶飘 傈价
        public const int DBR_TAG_REJECT_LIST = 1207;
        // 芭何磊 府胶飘 傈价
        public const int DBR_TAG_NOTREADCOUNT = 1208;
        // 佬瘤臼篮 率瘤 货荐 傈价
        public const int DBR_TAG_RESULT = 1209;
        // 戈飞俊 措茄 搬苞蔼
        // RelationShip ---------------
        public const int DBR_LM_LIST = 1210;
        // 包拌 府胶飘 掘绢坷扁
        public const int DBR_LM_RESULT = 1211;
        // 疙飞绢俊 措茄 搬苞蔼
        public const int DBR_FAIL = 2000;
        public const int DBR_NONE = 2000;
        // ----------------------------
        public const int MSM_LOGIN = 1;
        public const int MSM_GETUSERKEY = 100;
        public const int MSM_SELECTUSERKEY = 101;
        public const int MSM_GETGROUPKEY = 102;
        public const int MSM_SELECTGROUPKEY = 103;
        public const int MSM_UPDATEFEERCD = 120;
        public const int MSM_DELETEFEERCD = 121;
        public const int MSM_ADDFEERCD = 122;
        public const int MSM_GETTIMEOUTLIST = 123;
        public const int MCM_PASSWDSUCCESS = 10;
        public const int MCM_PASSWDFAIL = 11;
        public const int MCM_IDONUSE = 12;
        public const int MCM_GETFEERCD = 1000;
        public const int MCM_ADDFEERCD = 1001;
        public const int MCM_ENDTIMEOUT = 1002;
        public const int MCM_ONUSETIMEOUT = 1003;
        // 霸捞飘客 辑滚客狼 烹脚
        public const int GM_OPEN = 1;
        public const int GM_CLOSE = 2;
        public const int GM_CHECKSERVER = 3;
        // 辑滚俊辑 盲农 脚龋甫 焊晨
        public const int GM_CHECKCLIENT = 4;
        // 努扼捞攫飘俊辑 盲农 脚龋甫 焊晨
        public const int GM_DATA = 5;
        public const int GM_SERVERUSERINDEX = 6;
        public const int GM_RECEIVE_OK = 7;
        public const int GM_SENDPUBLICKEY = 8;
        public const int GM_TEST = 20;
        // ----------------------------
        // 辆练
        public const int RC_USERHUMAN = 0;
        // 版氰摹甫 掘阑 荐 绝澜
        public const int RC_NPC = 10;
        public const int RC_DOORGUARD = 11;
        // 巩瘤扁 版厚捍
        public const int RC_PEACENPC = 15;
        public const int RC_ARCHERPOLICE = 20;
        public const int RC_ANIMAL = 50;
        public const int RC_HEN = 51;
        // 催
        public const int RC_DEER = 52;
        // 荤娇...
        public const int RC_WOLF = 53;
        // 戳措
        public const int RC_RUNAWAYHEN = 54;
        // 崔酒唱绰 催
        public const int RC_TRAINER = 55;
        // 荐访炼背
        public const int RC_MONSTER = 80;
        // 厚急各
        public const int RC_OMA = 81;
        public const int RC_SPITSPIDER = 82;
        public const int RC_SLOWMONSTER = 83;
        public const int RC_SCORPION = 84;
        // 傈哎
        public const int RC_KILLINGHERB = 85;
        // 侥牢檬
        public const int RC_SKELETON = 86;
        // 秦榜
        public const int RC_DUALAXESKELETON = 87;
        // 街档尝秦榜
        public const int RC_HEAVYAXESKELETON = 88;
        // 奴档尝秦榜
        public const int RC_KNIGHTSKELETON = 89;
        // 秦榜傈荤
        public const int RC_BIGKUDEKI = 90;
        public const int RC_MAGCOWFACEMON = 91;
        public const int RC_COWFACEKINGMON = 92;
        public const int RC_THORNDARK = 93;
        public const int RC_LIGHTINGZOMBI = 94;
        public const int RC_DIGOUTZOMBI = 95;
        public const int RC_ZILKINZOMBI = 96;
        public const int RC_COWMON = 97;
        // 快搁蓖
        public const int RC_WHITESKELETON = 100;
        // 归榜
        public const int RC_SCULTUREMON = 101;
        // 籍惑阁胶磐
        public const int RC_SCULKING = 102;
        // 林付空
        public const int RC_BEEQUEEN = 103;
        // 国烹
        public const int RC_ARCHERMON = 104;
        // 付泵荤, 秦榜泵荐
        public const int RC_GASMOTH = 105;
        public const int RC_DUNG = 106;
        // 嫡, 啊胶
        public const int RC_CENTIPEDEKING = 107;
        // 瘤匙空
        public const int RC_BLACKPIG = 108;
        // 孺捣
        public const int RC_CASTLEDOOR = 110;
        // 荤合己巩, 己寒,..
        public const int RC_WALL = 111;
        // 荤合己巩, 己寒,..
        public const int RC_ARCHERGUARD = 112;
        // 泵荐版厚
        public const int RC_ELFMON = 113;
        public const int RC_ELFWARRIORMON = 114;
        public const int RC_BIGHEARTMON = 115;
        // 趋芭牢 空 奴 缴厘
        public const int RC_SPIDERHOUSEMON = 116;
        // 气救芭固
        public const int RC_EXPLOSIONSPIDER = 117;
        // 气林
        public const int RC_HIGHRISKSPIDER = 118;
        // 芭措 芭固
        public const int RC_BIGPOISIONSPIDER = 119;
        // 芭措 刀芭固
        public const int RC_SOCCERBALL = 120;
        // 绵备傍
        public const int RC_BAMTREE = 121;
        public const int RC_SCULKING_2 = 122;
        // 娄劈 林付空
        public const int RC_BLACKSNAKEKING = 123;
        // 孺荤空
        public const int RC_NOBLEPIGKING = 124;
        // 蓖捣空
        public const int RC_FEATHERKINGOFKING = 125;
        // 孺玫付空
        // 2003/02/11 眠啊 各
        public const int RC_SKELETONKING = 126;
        // 秦榜馆空
        public const int RC_TOXICGHOST = 127;
        // 何侥蓖
        public const int RC_SKELETONSOLDIER = 128;
        // 秦榜捍凉
        // 2003/03/04 眠啊 各
        public const int RC_BANYAGUARD = 129;
        // 馆具谅荤/馆具快荤
        public const int RC_DEADCOWKING = 130;
        // 荤快玫空
        // 2003/07/15 眠啊 各
        public const int RC_PBOMA1 = 131;
        // 朝俺坷付
        public const int RC_PBOMA2 = 132;
        // 艰苟摹惑鞭坷付
        public const int RC_PBOMA3 = 133;
        // 根嫡捞惑鞭坷付
        public const int RC_PBOMA4 = 134;
        // 漠窍鞭坷付
        public const int RC_PBOMA5 = 135;
        // 档尝窍鞭坷付
        public const int RC_PBOMA6 = 136;
        // 劝窍鞭坷付
        public const int RC_PBGUARD = 137;
        // 苞芭厚玫 芒版厚
        public const int RC_PBMSTONE1 = 138;
        // 付拌籍1
        public const int RC_PBMSTONE2 = 139;
        // 付拌籍2
        public const int RC_PBKING = 140;
        // 坷付颇玫炔(颇炔付脚)
        public const int RC_MINE = 141;
        // 瘤汾
        public const int RC_ANGEL = 142;
        // 岿飞(玫赤)
        public const int RC_CLONE = 143;
        // 盒脚
        public const int RC_FIREDRAGON = 144;
        // 颇玫付锋 (拳锋)
        public const int RC_DRAGONBODY = 145;
        // 拳锋个
        public const int RC_DRAGONSTATUE = 146;
        // 侩籍惑
        public const int RC_EYE_PROG = 147;
        // 汲牢措面
        public const int RC_STON_SPIDER = 148;
        // 脚籍刀付林
        public const int RC_GHOST_TIGER = 149;
        // 券康茄龋
        public const int RC_JUMA_THUNDER = 150;
        // 林付拜汾厘
        public const int RC_GOLDENIMUGI = 151;
        // 炔陛捞公扁
        public const int RC_MONSTERBOX = 152;
        // 阁胶磐冠胶
        public const int RC_STICKBLOCK = 153;
        // 龋去籍
        public const int RC_FOXWARRIOR = 154;
        // 厚岿咯快(傈荤) 厚岿孺龋
        public const int RC_FOXWIZARD = 155;
        // 厚岿咯快(贱荤) 厚岿利龋
        public const int RC_FOXTAOIST = 156;
        // 厚岿咯快(档荤) 厚岿家龋
        public const int RC_PUSHEDMON = 157;
        // 龋扁楷
        public const int RC_PUSHEDMON2 = 158;
        // 龋扁苛
        public const int RC_FOXPILLAR = 159;
        // 龋去扁籍
        public const int RC_FOXBEAD = 160;
        // 厚岿玫林
        public const int RC_ARCHERMASTER = 161;
        // 泵荐龋困捍(2005/08)
        // 2005/11/01
        public const int RC_SUPEROMA = 181;
        // 荐欺坷付
        public const int RC_TOGETHEROMA = 182;
        // 苟摹搁 碍秦瘤绰 坷付
        // 努扼捞攫飘 辆练...
        public const int RCC_HUMAN = 0;
        public const int RCC_GUARD = 12;
        public const int RCC_GUARD2 = 24;
        public const int RCC_MERCHANT = 50;
        public const int RCC_FIREDRAGON = 83;
        // 颇玫付锋 (拳锋)
        public const int LA_CREATURE = 0;
        public const int LA_UNDEAD = 1;
        public const int MP_CANMOVE = 0;
        public const int MP_WALL = 1;
        public const int MP_HIGHWALL = 2;
        public const int DR_UP = 0;
        public const int DR_UPRIGHT = 1;
        public const int DR_RIGHT = 2;
        public const int DR_DOWNRIGHT = 3;
        public const int DR_DOWN = 4;
        public const int DR_DOWNLEFT = 5;
        public const int DR_LEFT = 6;
        public const int DR_UPLEFT = 7;
        public const int U_DRESS = 0;
        public const int U_WEAPON = 1;
        public const int U_RIGHTHAND = 2;
        public const int U_NECKLACE = 3;
        public const int U_HELMET = 4;
        public const int U_ARMRINGL = 5;
        public const int U_ARMRINGR = 6;
        public const int U_RINGL = 7;
        public const int U_RINGR = 8;
        // 2003/03/15 酒捞袍 牢亥配府 犬厘
        public const int U_BUJUK = 9;
        public const int U_BELT = 10;
        public const int U_BOOTS = 11;
        public const int U_CHARM = 12;
        public const int UD_USER = 0;
        public const int UD_USER2 = 1;
        public const int UD_OBSERVER = 2;
        // '2' 殿鞭
        public const int UD_ASSISTANT = 4;
        // 'A' 殿鞭(observer殿鞭苞 sysop殿鞭 荤捞俊 眠啊)
        public const int UD_SYSOP = 6;
        // '1' 殿鞭
        public const int UD_ADMIN = 8;
        // '*' 殿鞭
        public const int UD_SUPERADMIN = 10;
        // '*' 殿鞭(抛胶飘 辑滚 肚绰 菩胶况靛 己傍 饶)
        public const int ET_DIGOUTZOMBI = 1;
        // 粱厚啊 顶颇绊 唱柯 如利
        public const int ET_MINE = 2;
        // 堡籍捞 概厘登绢 乐澜
        public const int ET_PILESTONES = 3;
        // 倒公歹扁
        public const int ET_HOLYCURTAIN = 4;
        // 搬拌
        public const int ET_FIRE = 5;
        public const int ET_SCULPEICE = 6;
        // 林付空狼 倒柄柳 炼阿
        public const int ET_HEARTPALP = 7;
        // 趋芭牢 空(缴厘)规狼 盟荐 傍拜
        public const int ET_MINE2 = 8;
        // 焊籍捞 概厘登绢 乐澜
        public const int ET_JUMAPEICE = 9;
        // 林付拜汾厘 兤柳 炼阿
        public const int ET_MINE3 = 10;
        // 捞亥飘侩 堡籍 棺 焊籍捞 概厘登绢 乐澜(2004/11/03)
        public const int NE_HEARTPALP = 1;
        // 扁夯 瓤苞 矫府令, 1锅 盟荐傍拜
        public const int NE_CLONESHOW = 2;
        // 盒脚唱鸥巢
        public const int NE_CLONEHIDE = 3;
        // 盒脚荤扼咙
        public const int NE_THUNDER = 4;
        // 侩带怜 锅俺
        public const int NE_FIRE = 5;
        // 侩带怜 侩鞠
        public const int NE_DRAGONFIRE = 6;
        // 侩阂傍拜 磐咙
        public const int NE_FIREBURN = 7;
        // 侩籍惑傍拜 磐咙 鸥坷抚
        public const int NE_FIRECIRCLE = 8;
        // 拳锋扁堪
        // 2004/06/22 脚痹公傍 捞棋飘.
        public const int NE_MONCAPTURE = 9;
        // 器铰八-器裙 捞棋飘
        public const int NE_BLOODSUCK = 10;
        // 软趋贱-软涝 捞棋飘
        public const int NE_BLINDEFFECT = 11;
        // 竿救贱 捞棋飘
        public const int NE_FLOWERSEFFECT = 12;
        // 采蕾 捞棋飘
        public const int NE_LEVELUP = 13;
        // 饭骇诀 捞棋飘
        public const int NE_RELIVE = 14;
        // 何劝 捞棋飘
        public const int NE_POISONFOG = 15;
        // 捞公扁 刀救俺 烙棋飘
        public const int NE_SN_MOVEHIDE = 16;
        // 捞公扁 况橇 荤扼瘤绰烙棋飘
        public const int NE_SN_MOVESHOW = 17;
        // 捞公扁 况橇 唱鸥唱绰烙棋飘
        public const int NE_SN_RELIVE = 18;
        // 捞公扁 何劝 烙棋飘
        public const int NE_BIGFORCE = 19;
        // 公必柳扁 烙棋飘
        public const int NE_JW_EFFECT1 = 20;
        // 厘盔 捞棋飘
        public const int NE_FOX_MOVEHIDE = 21;
        // 贱荤厚岿咯快 鉴埃捞悼 烙棋飘
        public const int NE_FOX_FIRE = 22;
        // 贱荤厚岿咯快 拳堪 风橇 烙棋飘
        public const int NE_FOX_MOVESHOW = 23;
        // 贱荤厚岿咯快 唱鸥唱绰 烙棋飘
        public const int NE_SOULSTONE_HIT = 24;
        // 龋去籍 傍拜 烙棋飘
        public const int NE_KINGSTONE_RECALL_1 = 25;
        // 厚岿玫林 家券 厚岿玫林俊霸 谎妨淋
        public const int NE_KINGSTONE_RECALL_2 = 26;
        // 厚岿玫林 家券 某腐俊霸 谎妨淋
        public const int NE_SIDESTONE_PULL = 27;
        // 龋去扁籍 寸扁扁
        public const int NE_HAPPYBIRTHDAY = 28;
        // 橇府固决 积老 烙棋飘
        public const int SWD_LONGHIT = 12;
        // 绢八贱
        public const int SWD_WIDEHIT = 25;
        // 馆岿八过
        public const int SWD_FIREHIT = 26;
        // 堪拳搬
        public const int SWD_RUSHRUSH = 27;
        // 公怕焊
        // 2003/03/15 脚痹公傍
        public const int SWD_CROSSHIT = 34;
        // 堡浅曼
        public const int SWD_TWINHIT = 38;
        // 街锋曼
        public const int SWD_STONEHIT = 43;
        // 荤磊饶
        // 涅胶飘 包访
        // IF
        public const int QI_CHECK = 1;
        // 101捞惑
        public const int QI_RANDOM = 2;
        public const int QI_GENDER = 3;
        // MAN or WOMAN
        public const int QI_DAYTIME = 4;
        // SUNRAISE DAY SUNSET NIGHT
        public const int QI_CHECKOPENUNIT = 5;
        // 蜡粗眉农
        public const int QI_CHECKUNIT = 6;
        // 蜡粗眉农
        public const int QI_CHECKLEVEL = 7;
        public const int QI_CHECKJOB = 8;
        // Warrior, Wizard, Taoist
        public const int QI_CHECKITEM = 20;
        public const int QI_CHECKITEMW = 21;
        public const int QI_CHECKGOLD = 22;
        public const int QI_ISTAKEITEM = 23;
        // 规陛傈俊 罐篮 酒捞袍捞 公均牢瘤 八荤
        public const int QI_CHECKDURA = 24;
        // 酒捞袍狼 酒捞袍狼 乞闭 郴备(dura / 1000) 八荤
        // 咯矾俺 乐绰 版快 弥绊 郴备甫 八荤
        public const int QI_CHECKDURAEVA = 25;
        public const int QI_DAYOFWEEK = 26;
        // 夸老 八荤
        public const int QI_TIMEHOUR = 27;
        // 矫埃窜困 八荤(0..23)
        public const int QI_TIMEMIN = 28;
        // 盒 八荤
        public const int QI_CHECKPKPOINT = 29;
        public const int QI_CHECKLUCKYPOINT = 30;
        public const int QI_CHECKMON_MAP = 31;
        // 泅犁 甘俊 各捞 乐绰瘤
        public const int QI_CHECKMON_AREA = 32;
        // 漂沥 瘤开俊 各捞 乐绰瘤
        public const int QI_CHECKHUM = 33;
        public const int QI_CHECKBAGGAGE = 34;
        // 荤侩磊俊霸 临 荐 乐绰瘤?
        // 6-11
        public const int QI_CHECKNAMELIST = 35;
        public const int QI_CHECKANDDELETENAMELIST = 36;
        public const int QI_CHECKANDDELETEIDLIST = 37;
        // *dq
        public const int QI_IFGETDAILYQUEST = 40;
        // 坷疵 涅胶飘甫 罐疽绰瘤 八荤, 蜡瓤扁埃 八荤 器窃
        public const int QI_CHECKDAILYQUEST = 41;
        // 漂沥 锅龋狼 涅胶飘甫 荐青吝牢瘤 八荤, 蜡瓤扁埃 八荤 器窃
        public const int QI_RANDOMEX = 42;
        // 颇扼皋鸥  5 100   5%烙...
        public const int QI_CHECKMON_NORECALLMOB_MAP = 43;
        // 泅犁 甘俊 乐绰 各 荐(家券各 力寇)
        public const int QI_CHECKBAGREMAIN = 44;
        // 蜡历 啊规狼 傍埃捞 N俺 巢酒 乐绰瘤
        public const int QI_CHECKGRADEITEM = 50;
        public const int QI_EQUALVAR = 51;
        // EQUALV D1 P1  //D1捞 P1苞 鞍篮瘤
        public const int QI_EQUAL = 135;
        // EQUAL P1 10   //P1捞 10牢瘤
        public const int QI_LARGE = 136;
        // LARGE P1 10   //P1捞 10焊促 奴瘤
        public const int QI_SMALL = 137;
        // SMALL P1 10   //P1捞 10焊促 累篮瘤 八荤
        public const int QI_ISGROUPOWNER = 138;
        // 弊缝 家蜡林牢瘤 酒囱瘤 八荤
        public const int QI_ISEXPUSER = 139;
        // 眉氰魄 荤侩磊牢瘤 八荤
        public const int QI_CHECKLOVERFLAG = 140;
        // 楷牢狼 敲贰弊啊 TRUE牢瘤 八荤(楷牢沥焊甫 茫阑 荐 绝栏搁 FALSE 府畔)
        public const int QI_CHECKLOVERRANGE = 141;
        // 楷牢捞 老沥 裹困 救俊 乐绰瘤
        public const int QI_CHECKLOVERDAY = 142;
        // 楷牢苞狼 背力老捞 老沥老 捞惑 登绰瘤
        // 厘盔扁何陛
        public const int QI_CHECKDONATION = 146;
        // 泅犁 扁何陛 儡咀 眉农
        public const int QI_ISGUILDMASTER = 147;
        // Guildmaster牢瘤 眉农
        public const int QI_CHECKWEAPONBADLUCK = 148;
        // 公扁狼 历林 眉农
        public const int QI_CHECKPREMIUMGRADE = 149;
        // 橇府固决 殿鞭 眉农
        public const int QI_CHECKCHILDMOB = 150;
        // 家券吝牢 阁胶磐 捞抚栏肺 眉农(CHECKRECALLMOB)
        public const int QI_CHECKGROUPJOBBALANCE = 151;
        // 弊缝俊 傈荤, 贱荤, 档荤 荐啊 鞍篮瘤 眉农
        public const int QI_CHECKRANGEONELOVER = 152;
        // 裹困郴俊 楷牢牢 荤恩捞 乐绰瘤 眉农
        public const int QI_ISNEWHUMAN = 153;
        public const int QI_ISSYSOP = 154;
        public const int QI_ISADMIN = 155;
        public const int QI_CHECKGAMEGOLD = 156;
        public const int QI_CHECKMARRY = 157;
        public const int QI_CHECKPOSEMARRY = 158;
        public const int QI_CHECKPOSEGENDER = 159;
        public const int QI_CHECKPOSEDIR = 160;
        public const int QI_CHECKPOSELEVEL = 161;
        public const int QI_CHECKMASTER = 162;
        public const int QI_CHECKISMASTER = 163;
        public const int QI_CHECKPOSEMASTER = 164;
        public const int QI_HAVEMASTER = 165;
        public const int QI_CHECKMASTERCOUNT = 166;
        // Action
        public const int QA_SET = 1;
        // 101捞惑
        public const int QA_TAKE = 2;
        // 酒捞袍阑 罐促
        public const int QA_GIVE = 3;
        public const int QA_TAKEW = 4;
        // 馒侩窍绊 乐绰 酒捞袍阑 罐促
        public const int QA_CLOSE = 5;
        // 措拳芒阑 摧澜
        public const int QA_RESET = 6;
        public const int QA_OPENUNIT = 7;
        public const int QA_SETUNIT = 8;
        // 蜡粗悸  1..100
        public const int QA_RESETUNIT = 9;
        // 蜡粗府悸   1..100
        public const int QA_BREAK = 10;
        public const int QA_TIMERECALL = 11;
        // 瘤沥等 矫埃捞 瘤唱搁 泅犁 厘家肺 家券 等促.
        public const int QA_PARAM1 = 12;
        public const int QA_PARAM2 = 13;
        public const int QA_PARAM3 = 14;
        public const int QA_PARAM4 = 15;
        public const int QA_MAPMOVE = 20;
        public const int QA_MAPRANDOM = 21;
        public const int QA_TAKECHECKITEM = 22;
        // CHECK亲格俊辑 八荤等 酒捞袍阑 罐绰促.
        public const int QA_MONGEN = 23;
        // 阁胶磐甫 哩矫糯
        public const int QA_MONCLEAR = 24;
        // 阁胶磐甫 葛滴 力芭 矫挪促
        public const int QA_MOV = 25;
        public const int QA_INC = 26;
        public const int QA_DEC = 27;
        public const int QA_SUM = 28;
        // SUM P1 P2 //P9 = P1 + P2
        public const int QA_BREAKTIMERECALL = 29;
        public const int QA_TIMERECALLGROUP = 30;
        // 瘤沥等 矫埃捞 瘤唱搁 弊缝 傈眉啊 泅犁 厘家肺 家券 等促.
        public const int QA_CLOSENOINVEN = 31;
        // 措拳芒阑 摧澜(牢亥芒篮 扒靛府瘤 臼澜)
        public const int QA_MOVRANDOM = 50;
        // MOVR
        public const int QA_EXCHANGEMAP = 51;
        // EXCHANGEMAP R001  //R001俊 乐绰 茄 荤恩苞 磊府甫 官槽促.
        public const int QA_RECALLMAP = 52;
        // RECALLMAP R001  //R001俊 乐绰 荤恩甸阑 葛滴 家券 茄促.
        public const int QA_ADDBATCH = 53;
        public const int QA_BATCHDELAY = 54;
        public const int QA_BATCHMOVE = 55;
        public const int QA_PLAYDICE = 56;
        // PLAYDICE 2 @diceresult //2俺狼 林荤困甫 奔赴促. 弊饶 @diceresult 技记栏肺 埃促
        // 6-11
        public const int QA_ADDNAMELIST = 57;
        public const int QA_DELETENAMELIST = 58;
        public const int QA_PLAYROCK = 59;
        // PLAYDICE 2 @diceresult //2俺狼 林荤困甫 奔赴促. 弊饶 @diceresult 技记栏肺 埃促
        // *dq
        public const int QA_RANDOMSETDAILYQUEST = 60;
        // 颇扼皋磐,  弥家, 弥措  抗) 401 450  401俊辑 450锅鳖瘤 罚待栏肺 汲沥
        public const int QA_SETDAILYQUEST = 61;
        public const int QA_GIVEEXP = 63;
        // 版氰摹 林扁(捞亥飘 辆丰饶 扁瓷 昏力)
        public const int QA_TAKEGRADEITEM = 70;
        public const int QA_GOTOQUEST = 100;
        public const int QA_ENDQUEST = 101;
        public const int QA_GOTO = 102;
        public const int QA_SOUND = 103;
        public const int QA_CHANGEGENDER = 104;
        public const int QA_KICK = 105;
        public const int QA_MOVEALLMAP = 106;
        // 泅犁 甘 蜡历甸阑 葛滴 漂沥 甘栏肺 捞悼矫糯.
        public const int QA_MOVEALLMAPGROUP = 107;
        // 弊缝 糕滚甸 吝俊 泅犁 甘俊 乐绰 糕滚甸父 漂沥 甘栏肺 捞悼矫糯.
        public const int QA_RECALLMAPGROUP = 108;
        // 弊缝 糕滚甸 吝俊 漂沥 甘俊 乐绰 糕滚甸父 泅犁 甘栏肺 捞悼矫糯.
        public const int QA_WEAPONUPGRADE = 109;
        // 甸绊 乐绰 公扁俊 可记阑 嘿牢促.
        public const int QA_SETALLINMAP = 110;
        // 泅犁 甘俊 乐绰 葛电 蜡历甸狼 敲贰弊甫 SET茄促.
        public const int QA_INCPKPOINT = 111;
        // PK Point甫 刘啊矫挪促.
        public const int QA_DECPKPOINT = 112;
        // PK Point甫 皑家矫挪促.
        // 楷牢
        public const int QA_MOVETOLOVER = 113;
        // 楷牢菊栏肺 捞悼茄促.
        public const int QA_BREAKLOVER = 114;
        // 楷牢包拌甫 老规利栏肺 秦力矫挪促.
        public const int QA_SOUNDALL = 115;
        // 林函荤恩俊霸 荤款靛甫 甸妨淋
        public const int QA_DECWEAPONBADLUCK = 117;
        // 历林啊 嘿篮 公扁狼 历林甫 1 皑家 矫挪促.
        // 厘盔扁何陛
        public const int QA_DECDONATION = 118;
        // 扁何陛 儡咀阑 皑家 矫挪促.
        public const int QA_SHOWEFFECT = 119;
        // 厘盔捞棋飘甫 焊咯霖促.
        public const int QA_MONGENAROUND = 120;
        // 某腐狼 林困俊 阁胶磐甫 哩 矫挪促.
        public const int QA_RECALLMOB = 121;
        // 何窍 阁胶磐 家券
        public const int QA_SETLOVERFLAG = 122;
        // 楷牢狼 敲贰弊甫 SET茄促.
        public const int QA_GUILDSECESSION = 123;
        // 巩颇呕硼
        public const int QA_GIVETOLOVER = 124;
        // 楷牢俊霸 酒捞袍 林扁
        public const int QA_INCMEMORIALCOUNT = 125;
        // NPC喊 墨款飘 刘啊
        public const int QA_DECMEMORIALCOUNT = 126;
        // NPC喊 墨款飘 皑家
        public const int QA_SAVEMEMORIALCOUNT = 127;
        // NPC喊 墨款飘 颇老 历厘
        public const int QA_SECONDSCARD = 128;
        public const int QA_MARRY = 129;
        public const int QA_UNMARRY = 130;
        public const int QA_MASTER = 131;
        public const int QA_UNMASTER = 132;
        public const int QA_SETHUMICON = 133;
        public const int VERSION_NUMBER = 20120221;
        public const int VERSION_NUMBER20050501 = 20050501;
        public const int VERSION_NUMBER_20030805 = 20030805;
        public const int VERSION_NUMBER_20030715 = 20030715;
        public const int VERSION_NUMBER_20030527 = 20030527;
        public const int VERSION_NUMBER_20030403 = 20030403;
        public const int VERSION_NUMBER_030328 = 20030328;
        public const int VERSION_NUMBER_030317 = 20030317;
        public const int VERSION_NUMBER_030211 = 20030211;
        public const int VERSION_NUMBER_030122 = 20030122;
        public const int VERSION_NUMBER_020819 = 20020819;
        public const int VERSION_NUMBER_0522 = 20020522;
        public const int VERSION_NUMBER_02_0403 = 20020403;
        public const int VERSION_NUMBER_01_1006 = 20011006;
        public const int VERSION_NUMBER_0925 = 20010925;
        public const int VERSION_NUMBER_0704 = 20010704;
        // VERSION_NUMBER_0522 = 20010522;
        public const int VERSION_NUMBER_0419 = 20010419;
        public const int VERSION_NUMBER_0407 = 20010407;
        public const int VERSION_NUMBER_0305 = 20010305;
        public const int VERSION_NUMBER_0216 = 20010216;
        public const int BUFFERSIZE = 10000;
        // 酒捞袍狼 函拳蔼 沥狼
        public const int EFFTYPE_TWOHAND_WEHIGHT_ADD = 1;
        public const int EFFTYPE_EQUIP_WHEIGHT_ADD = 2;
        public const int EFFTYPE_LUCK_ADD = 3;
        public const int EFFTYPE_BAG_WHIGHT_ADD = 4;
        public const int EFFTYPE_HP_MP_ADD = 5;
        public const int EFFTYPE2_EVENT_GRADE = 6;
        // Comand Result Defines... PDS:2003-03-31 ---------------------------------
        public const int CR_SUCCESS = 0;
        // 己傍
        public const int CR_FAIL = 1;
        // 角菩
        public const int CR_DONTFINDUSER = 2;
        // 蜡历甫 茫阑 荐 绝澜
        public const int CR_DONTADD = 3;
        // 眠啊且 荐 绝澜
        public const int CR_DONTDELETE = 4;
        // 昏力且 荐 绝澜
        public const int CR_DONTUPDATE = 5;
        // 函版且 荐 绝澜
        public const int CR_DONTACCESS = 6;
        // 角青 阂啊瓷
        public const int CR_LISTISMAX = 7;
        // 府胶飘狼 弥措摹捞骨肺 阂啊瓷
        public const int CR_LISTISMIN = 8;
        // 府胶飘狼 弥家摹捞骨肺 阂啊瓷
        public const int CR_DBWAIT = 9;
        // DB俊辑 扁促府绊 乐绰吝
        // 立加惑怕  PDS:2003-03-31 ------------------------------------------------
        public const int CONNSTATE_UNKNOWN = 0;
        // 舅荐 绝澜
        public const int CONNSTATE_DISCONNECT = 1;
        // 厚立加 惑怕
        public const int CONNSTATE_NOUSE1 = 2;
        // 荤侩救窃
        public const int CONNSTATE_NOUSE2 = 3;
        // 荤侩救窃
        public const int CONNSTATE_CONNECT_0 = 4;
        // 0锅辑滚俊 立加窃
        public const int CONNSTATE_CONNECT_1 = 5;
        // 1锅辑滚俊 立加窃
        public const int CONNSTATE_CONNECT_2 = 6;
        // 2锅辑滚俊 立加窃
        public const int CONNSTATE_CONNECT_3 = 7;
        // 3锅辑滚俊 立加窃 : 抗厚肺父惦
        // 包拌盒幅  2003/04/15 模备, 率瘤
        public const int RT_FRIENDS = 1;
        // 模备
        public const int RT_LOVERS = 2;
        // 楷牢
        public const int RT_MASTER = 3;
        // 荤何
        public const int RT_DISCIPLE = 4;
        // 力磊
        public const int RT_BLACKLIST = 8;
        // 厩楷
        // 率瘤惑怕  PDS:2003-03-31 ------------------------------------------------
        public const int TAGSTATE_NOTREAD = 0;
        // 佬瘤臼澜
        public const int TAGSTATE_READ = 1;
        // 佬澜
        public const int TAGSTATE_DONTDELETE = 2;
        // 昏力陛瘤
        public const int TAGSTATE_DELETED = 3;
        // 昏力凳
        // 率瘤惑怕 函版俊辑 静烙
        public const int TAGSTATE_WANTDELETABLE = 3;
        // 昏力啊瓷窍霸 函版
        // Relationship Request Sequences...
        public const int RsReq_None = 0;
        // 扁夯惑怕
        public const int RsReq_WantToJoinOther = 1;
        // 穿备俊霸 曼啊脚没阑 窃
        public const int RsReq_WaitAnser = 2;
        // 览翠阑 扁促覆
        public const int RsReq_WhoWantJoin = 3;
        // 穿焙啊 曼啊甫 盔窃
        public const int RsReq_AloowJoin = 4;
        // 曼啊甫 倾遏窃
        public const int RsReq_DenyJoin = 5;
        // 曼啊甫 芭例窃
        public const int RsReq_Cancel = 6;
        // 秒家
        public const double RaReq_CancelTime = 30 * 1000;
        // 磊悼 秒家 矫埃 30檬 msec
        public const double MAX_WAITTIME = 60 * 1000;
        // 弥措 扁促府绰 矫埃
        // Relationship State Define...
        public const int RsState_None = 0;
        // 扁夯惑怕
        public const int RsState_Lover = 10;
        // 楷牢
        public const int RsState_LoverEnd = 11;
        // 楷牢呕硼
        public const int RsState_Married = 20;
        // 搬去
        public const int RsState_MarriedEnd = 21;
        // 搬去呕硼
        public const int RsState_Master = 30;
        // 师傅
        public const int RsState_MasterEnd = 31;
        // 荤何呕硼
        public const int RsState_Pupil = 40;
        // 徒弟
        public const int RsState_PupilEnd = 41;
        // 力磊呕硼
        public const int RsState_TempPupil = 50;
        // 烙矫力磊
        public const int RsState_TempPupilEnd = 51;
        // 烙矫力磊呕硼
        // RelationShip Error Code...
        public const int RsError_SuccessJoin = 1;
        // 曼啊俊 己傍窍看促 ( 曼啊茄荤恩率)
        public const int RsError_SuccessJoined = 2;
        // 曼啊俊 己傍登绢脸促 ( 曼啊等 荤恩率)
        public const int RsError_DontJoin = 3;
        // 曼啊且荐 绝促
        public const int RsError_DontLeave = 4;
        // 栋朝荐 绝促.
        public const int RsError_RejectMe = 5;
        // 芭何惑怕捞促
        public const int RsError_RejectOther = 6;
        // 芭何惑怕捞促
        public const int RsError_LessLevelMe = 7;
        // 唱狼饭骇捞 撤促
        public const int RsError_LessLevelOther = 8;
        // 惑措规狼饭骇捞 撤促
        public const int RsError_EqualSex = 9;
        // 己喊捞 鞍促
        public const int RsError_FullUser = 10;
        // 曼咯牢盔捞 啊垫谩促
        public const int RsError_CancelJoin = 11;
        // 曼啊秒家
        public const int RsError_DenyJoin = 12;
        // 曼啊甫 芭例窃
        public const int RsError_DontDelete = 13;
        // 呕硼矫懦荐 绝促.
        public const int RsError_SuccessDelete = 14;
        // 呕硼矫淖澜
        public const int RsError_NotRelationShip = 15;
        // 背力惑怕啊 酒聪促.
        // 般摹扁
        public const int MAX_OVERLAPITEM = 1000;
        // 困殴惑痢 魄概辆幅
        // 俺喊酒捞袍幅
        public const int USERMARKET_TYPE_ALL = 0;
        // 葛滴
        public const int USERMARKET_TYPE_WEAPON = 1;
        // 公扁
        public const int USERMARKET_TYPE_NECKLACE = 2;
        // 格吧捞
        public const int USERMARKET_TYPE_RING = 3;
        // 馆瘤
        public const int USERMARKET_TYPE_BRACELET = 4;
        // 迫骂,厘癌
        public const int USERMARKET_TYPE_CHARM = 5;
        // 荐龋籍
        public const int USERMARKET_TYPE_HELMET = 6;
        // 捧备
        public const int USERMARKET_TYPE_BELT = 7;
        // 倾府鹅
        public const int USERMARKET_TYPE_SHOES = 8;
        // 脚惯
        public const int USERMARKET_TYPE_ARMOR = 9;
        // 癌渴
        public const int USERMARKET_TYPE_DRINK = 10;
        // 矫距
        public const int USERMARKET_TYPE_JEWEL = 11;
        // 焊苛,脚林
        public const int USERMARKET_TYPE_BOOK = 12;
        // 氓
        public const int USERMARKET_TYPE_MINERAL = 13;
        // 堡籍
        public const int USERMARKET_TYPE_QUEST = 14;
        // 涅胶飘酒捞袍
        public const int USERMARKET_TYPE_ETC = 15;
        // 扁鸥
        public const int USERMARKET_TYPE_ITEMNAME = 16;
        // 酒捞袍捞抚
        // 悸飘幅
        public const int USERMARKET_TYPE_SET = 100;
        // 悸飘 酒捞袍
        // 蜡历幅
        public const int USERMARKET_TYPE_MINE = 200;
        // 磊脚捞魄拱扒
        public const int USERMARKET_TYPE_OTHER = 300;
        // 促弗荤恩捞 魄拱扒
        public const int USERMARKET_MODE_NULL = 0;
        // 檬扁蔼
        public const int USERMARKET_MODE_BUY = 1;
        // 荤绰葛靛
        public const int USERMARKET_MODE_INQUIRY = 2;
        // 炼雀葛靛
        public const int USERMARKET_MODE_SELL = 3;
        // 魄概葛靛
        public const int MARKET_CHECKTYPE_SELLOK = 1;
        // 困殴 沥惑
        public const int MARKET_CHECKTYPE_SELLFAIL = 2;
        // 困殴 角菩
        public const int MARKET_CHECKTYPE_BUYOK = 3;
        // 备涝 沥惑
        public const int MARKET_CHECKTYPE_BUYFAIL = 4;
        // 备涝 角菩
        public const int MARKET_CHECKTYPE_CANCELOK = 5;
        // 秒家 沥惑
        public const int MARKET_CHECKTYPE_CANCELFAIL = 6;
        // 秒家 角菩
        public const int MARKET_CHECKTYPE_GETPAYOK = 7;
        // 捣 雀荐 沥惑
        public const int MARKET_CHECKTYPE_GETPAYFAIL = 8;
        // 捣 雀荐 角菩
        public const int MARKET_DBSELLTYPE_SELL = 1;
        // 魄概吝
        public const int MARKET_DBSELLTYPE_BUY = 2;
        // 或澜
        public const int MARKET_DBSELLTYPE_CANCEL = 3;
        // 秒家
        public const int MARKET_DBSELLTYPE_GETPAY = 4;
        // 陛咀雀荐
        public const int MARKET_DBSELLTYPE_READYSELL = 11;
        // 烙矫 魄概吝
        public const int MARKET_DBSELLTYPE_READYBUY = 12;
        // 烙矫 荤绰吝
        public const int MARKET_DBSELLTYPE_READYCANCEL = 13;
        // 烙矫 秒家吝
        public const int MARKET_DBSELLTYPE_READYGETPAY = 14;
        // 烙矫 雀荐吝
        public const int MARKET_DBSELLTYPE_DELETE = 20;
        // 昏力
        // 困殴惑痢 府畔蔼
        public const int UMResult_Success = 0;
        // 己傍
        public const int UMResult_Fail = 1;
        // 角菩
        public const int UMResult_ReadFail = 2;
        // 佬扁 角菩
        public const int UMResult_WriteFail = 3;
        // 历厘 角菩
        public const int UMResult_ReadyToSell = 4;
        // 魄概啊瓷
        public const int UMResult_OverSellCount = 5;
        // 魄概 酒捞袍 俺荐 檬苞
        public const int UMResult_LessMoney = 6;
        // 陛傈何练
        public const int UMResult_LessLevel = 7;
        // 饭骇何练
        public const int UMResult_MaxBagItemCount = 8;
        // 啊规俊 酒捞袍菜曼
        public const int UMResult_NoItem = 9;
        // 酒捞袍捞 绝澜
        public const int UMResult_DontSell = 10;
        // 魄概阂啊
        public const int UMResult_DontBuy = 11;
        // 备涝阂啊
        public const int UMResult_DontGetMoney = 12;
        // 陛咀雀荐 阂啊
        public const int UMResult_MarketNotReady = 13;
        // 困殴矫胶袍 磊眉啊 阂啊瓷
        public const int UMResult_LessTrustMoney = 14;
        // 困殴陛咀捞 何练 1000 傈 焊促绰 目具凳
        public const int UMResult_MaxTrustMoney = 15;
        // 困殴陛咀捞 呈公 怒
        public const int UMResult_CancelFail = 16;
        // 困殴秒家 角菩
        public const int UMResult_OverMoney = 17;
        // 家蜡陛咀 弥措摹啊 逞绢皑
        public const int UMResult_SellOK = 18;
        // 魄概啊 肋夌澜
        public const int UMResult_BuyOK = 19;
        // 备涝捞 肋夌澜
        public const int UMResult_CancelOK = 20;
        // 魄概秒家啊 肋夌澜
        public const int UMResult_GetPayOK = 21;
        // 魄概陛 雀荐啊 肋夌澜
        // 啊拜弥措摹
        public const int MAX_MARKETPRICE = 50000000;

        public static byte RACEfeature(int feature)
        {
            byte result =  HUtil32.LoByte(HUtil32.LoWord(feature));
            return result;
        }

        public static byte WEAPONfeature(int feature)
        {
            byte result = HUtil32.HiByte(HUtil32.LoWord(feature));
            return result;
        }

        public static byte HAIRfeature(int feature)
        {
            byte result = HUtil32.LoByte(HUtil32.HiWord(feature));
            return result;
        }

        public static byte DRESSfeature(int feature)
        {
            byte result = HUtil32.HiByte(HUtil32.HiWord(feature));
            return result;
        }

        public static ushort APPRfeature(int feature)
        {
            ushort result = HUtil32.HiWord(feature);
            return result;
        }

        public static long MakeFeature(byte race, byte dress, byte weapon, byte face)
        {
            long result;
            result = HUtil32.MakeLong(HUtil32.MakeWord(race, weapon), HUtil32.MakeWord(face, dress));
            return result;
        }

        public static long MakeFeatureAp(byte race, byte state, ushort appear)
        {
            return HUtil32.MakeLong(HUtil32.MakeWord(race, state), appear);
        }

        public static TDefaultMessage MakeDefaultMsg(ushort msg, int soul, int wparam, int atag, int nseries)
        {
            TDefaultMessage result = new TDefaultMessage();
            result.Ident = msg;
            result.Recog = soul;
            result.Param = (ushort)wparam;
            result.Tag = (ushort)atag;
            result.Series = (ushort)nseries;
            return result;
        }

        public static int UpInt(double r)
        {
            int result;
            if (r > Convert.ToInt32(r))
            {
                result = Convert.ToInt32(r) + 1;
            }
            else
            {
                result = Convert.ToInt32(r);
            }
            return result;
        }

    }
}

