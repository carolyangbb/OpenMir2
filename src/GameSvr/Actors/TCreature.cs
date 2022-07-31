using System;
using System.Collections;
using System.Collections.Generic;
using SystemModule;

namespace GameSvr
{
    public class TCreature
    {
        public bool BoInFreePKArea
        {
            get
            {
                return FBoInFreePKArea;
            }
            set
            {
                SetBoInFreePKArea(value);
            }
        }
        public int ActorId;
        public string MapName;
        public string UserName;
        public short CX = 0;
        public short CY = 0;
        public byte Dir = 0;
        public byte Sex = 0;
        public byte Hair = 0;
        public byte HairColorR = 0;
        public byte HairColorG = 0;
        public byte HairColorB = 0;
        public byte Job = 0;
        public int Gold = 0;
        public int GameGold = 0;
        public TAbility Abil = null;
        public int CharStatus = 0;
        public ushort[] StatusArr;
        public string HomeMap;
        public short HomeX = 0;
        public short HomeY = 0;
        public string NeckName;
        public int PlayerKillingPoint = 0;
        public bool AllowGroup = false;
        public string GroupRequester = string.Empty;
        public long GroupRequestTime = 0;
        public bool AllowEnterGuild = false;
        public byte FreeGulityCount = 0;
        public byte IncHealth = 0;
        public byte IncSpell = 0;
        public short IncHealing = 0;
        public byte FightZoneDieCount = 0;
        public int DBVersion = 0;
        public byte BonusApply = 0;
        public TNakedAbility BonusAbil = null;
        public TNakedAbility CurBonusAbil = null;
        public int BonusPoint = 0;
        public long HungryState = 0;
        public byte TestServerResetCount = 0;
        public double BodyLuck = 0;
        public int BodyLuckLevel = 0;
        public short CGHIUseTime = 0;
        public bool BoEnableRecall = false;
        public bool BoEnableAgitRecall = false;
        public short DailyQuestNumber = 0;
        public short DailyQuestGetDate = 0;
        public byte[] QuestIndexOpenStates;
        public byte[] QuestIndexFinStates;
        public byte[] QuestStates;
        public int CharStatusEx = 0;
        public int FightExp = 0;
        public TAbility WAbil = null;
        public TAddAbility AddAbil = null;
        public int ViewRange = 0;
        public byte[] StatusValue;
        public long[] StatusTimes;
        public byte[] ExtraAbil;
        public byte[] ExtraAbilFlag;
        public int[] ExtraAbilTimes;
        public ushort Appearance = 0;
        public byte RaceServer = 0;
        public byte RaceImage = 0;
        public byte AccuracyPoint = 0;
        public byte HitPowerPlus = 0;
        public byte HitDouble = 0;
        public long CGHIstart = 0;
        public bool BoCGHIEnable = false;
        public bool BoOldVersionUser_Italy = false;
        public bool BoReadyAdminPassword = false;
        public bool BoReadySuperAdminPassword = false;
        public int PlusFinalDamage = 0;
        public long MeetLoverDelayTime = 0;
        public byte HealthRecover = 0;
        public byte SpellRecover = 0;
        public byte AntiPoison = 0;
        public byte PoisonRecover = 0;
        public byte AntiMagic = 0;
        public int Luck = 0;
        public int PerHealth = 0;
        public int PerHealing = 0;
        public int PerSpell = 0;
        public long IncHealthSpellTime = 0;
        public byte RedPoisonLevel = 0;
        // 弧刀俊 吝刀登菌阑锭狼 碍档(0~256)
        public byte PoisonLevel = 0;
        // 吝刀登菌阑锭 刀狼 碍档 (0..3) (0~256)
        public int PlusPoisonFactor = 0;
        public int AvailableGold = 0;
        public byte SpeedPoint = 0;
        public byte UserDegree = 0;
        public short HitSpeed = 0;
        public byte LifeAttrib = 0;
        public byte CoolEye = 0;
        public TCreature GroupOwner = null;
        public IList<TCreature> GroupMembers = null;
        public bool BoHearWhisper = false;
        public bool BoHearCry = false;
        public bool BoHearGuildMsg = false;
        public bool BoExchangeAvailable = false;
        public ArrayList WhisperBlockList = null;
        public long LatestCryTime = 0;
        public TCreature Master = null;
        public long MasterRoyaltyTime = 0;
        public long SlaveLifeTime = 0;
        public int SlaveExp = 0;
        public byte SlaveExpLevel = 0;
        public byte SlaveMakeLevel = 0;
        public IList<TCreature> SlaveList = null;
        public bool BoSlaveRelax = false;
        public byte HumAttackMode = 0;
        public byte DefNameColor = 0;
        public int Light = 0;
        public bool BoGuildWarArea = false;
        public object Castle = null;
        public bool BoCrimeforCastle = false;
        public long CrimeforCastleTime = 0;
        public bool NeverDie = false;
        public bool HoldPlace = false;
        public bool BoFearFire = false;
        public bool BoAnimal = false;
        public bool BoNoItem = false;
        public bool HideMode = false;
        public bool StickMode = false;
        public bool RushMode = false;
        public bool NoAttackMode = false;
        public bool NoMaster = false;
        public bool BoSkeleton = false;
        public int MeatQuality = 0;
        public int BodyLeathery = 0;
        public bool BoHolySeize = false;
        public long HolySeizeStart = 0;
        public long HolySeizeTime = 0;
        public bool BoCrazyMode = false;
        public bool BoGoodCrazyMode = false;
        public long CrazyModeStart = 0;
        public long CrazyModeTime = 0;
        public bool BoOpenHealth = false;
        public long OpenHealthStart = 0;
        public long OpenHealthTime = 0;
        public bool BoDuplication = false;
        public long DupStartTime = 0;
        public TEnvirnoment PEnvir = null;
        public bool BoGhost = false;
        public long GhostTime = 0;
        public bool Death = false;
        public long DeathTime = 0;
        public byte DeathState = 0;
        public long StruckTime = 0;
        public bool WantRefMsg = false;
        public bool ErrorOnInit = false;
        public bool SpaceMoved = false;
        public bool BoDealing = false;
        public bool BoDealEnding = false;
        public long DealItemChangeTime = 0;
        public TCreature DealCret = null;
        public TGuild MyGuild = null;
        public int GuildRank = 0;
        public string GuildRankName = string.Empty;
        public string LatestNpcCmd = string.Empty;
        public int AttackSkillCount = 0;
        public int AttackSkillPointCount = 0;
        public bool BoHasMission = false;
        public int Mission_X = 0;
        public int Mission_Y = 0;
        public bool BoHumHideMode = false;
        public bool BoStoneMode = false;
        public bool BoViewFixedHide = false;
        public bool BoNextTimeFreeCurseItem = false;
        public bool BoFixedHideMode = false;
        public bool BoSysopMode = false;
        public bool BoSuperviserMode = false;
        public bool BoEcho = false;
        public bool BoTaiwanEventUser = false;
        public string TaiwanEventItemName = string.Empty;
        public bool BoAbilSpaceMove = false;
        public bool BoAbilMakeStone = false;
        public bool BoAbilRevival = false;
        public long LatestRevivalTime = 0;
        public bool BoAddMagicFireball = false;
        public bool BoAddMagicHealing = false;
        public bool BoAbilAngerEnergy = false;
        public bool BoMagicShield = false;
        public bool BoAbilSuperStrength = false;
        public bool BoFastTraining = false;
        public bool BoAbilSearch = false;
        public bool BoAbilSeeHealGauge = false;
        public bool BoAbilMagBubbleDefence = false;
        public byte MagBubbleDefenceLevel = 0;
        public long SearchRate = 0;
        public long SearchTime = 0;
        public long RunTime = 0;
        public long RunNextTick = 0;
        public int HealthTick = 0;
        public int SpellTick = 0;
        public TCreature TargetCret = null;
        public long TargetFocusTime = 0;
        public TCreature LastHiter = null;
        public int LastHiterRace = 0;
        public TCreature SlaveHiter = null;
        public long LastHitTime = 0;
        public TCreature ExpHiter = null;
        public long ExpHitTime = 0;
        public long LatestSpaceMoveTime = 0;
        public long LatestSpaceScrollTime = 0;
        public long LatestSearchWhoTime = 0;
        public long MapMoveTime = 0;
        public bool BoIllegalAttack = false;
        public long IllegalAttackTime = 0;
        public short ManaToHealthPoint = 0;
        public int SuckupEnemyHealthRate = 0;
        public double SuckupEnemyHealth = 0;
        public int RefObjCount = 0;
        public long poisontime = 0;
        public long time4hour = 0;
        public long time10min = 0;
        public long time500ms = 0;
        public long time60sec = 0;
        public long time30sec = 0;
        public long time10sec = 0;
        public long time5sec = 0;
        public long ticksec = 0;
        public bool FAlreadyDisapper = false;
        public long MasterFeature = 0;
        public bool ForceMoveToMaster = false;
        public bool BoDontMove = false;
        public bool BoDisapear = false;
        public bool DontBagItemDrop = false;
        public bool DontBagGoldDrop = false;
        public bool DontUseItemDrop = false;
        public int Tame = 0;
        public int AntiPush = 0;
        public int AntiUndead = 0;
        public int SizeRate = 0;
        public int AntiStop = 0;
        public int PushedCount = 0;
        public bool BoLoseTargetMoment = false;
        public bool BoHighLevelEffect = false;
        public bool BoGuildAgitDealTry = false;
        public int MeltArea = 0;
        public bool bStealth = false;
        public int BodyState = 0;
        public bool LoverPlusAbility = false;
        public string m_sMasterName;
        public bool m_boMaster = false;
        public int m_nMasterCount = 0;
        public int m_nMasterRanking = 0;
        private readonly ArrayList MsgList = null;
        private readonly IList<TCreature> MsgTargetList = null;
        private readonly IList<TVisibleItemInfo> VisibleItems = null;
        private readonly IList<TEvent> VisibleEvents = null;
        private long WatchTime = 0;
        private bool FBoInFreePKArea = false;
        private readonly IList<TPkHiterInfo> PKHiterList = null;
        protected int FindPathRate = 0;
        protected long FindpathTime = 0;
        protected long HitTime = 0;
        protected long WalkTime = 0;
        protected long SearchEnemyTime = 0;
        protected bool AreaStateOrNameChanged = false;
        public IList<TVisibleActor> VisibleActors = null;
        public IList<TUserItem> ItemList = null;
        public IList<TUserItem> DealList = null;
        public int DealGold = 0;
        public bool BoDealSelect = false;
        public IList<TUserMagic> MagicList = null;
        public TUserItem[] UseItems;
        public ArrayList SaveItems = null;
        public int NextWalkTime = 0;
        public int WalkStep = 0;
        public int WalkCurStep = 0;
        public int WalkWaitTime = 0;
        public long WalkWaitCurTime = 0;
        public bool BoWalkWaitMode = false;
        public int NextHitTime = 0;
        public TUserMagic PSwordSkill = null;
        public TUserMagic PPowerHitSkill = null;
        public TUserMagic PLongHitSkill = null;
        public TUserMagic PWideHitSkill = null;
        public TUserMagic PFireHitSkill = null;
        public TUserMagic PCrossHitSkill = null;
        public TUserMagic PTwinHitSkill = null;
        public TUserMagic PStoneHitSkill = null;
        public bool BoAllowPowerHit = false;
        public bool BoAllowLongHit = false;
        public bool BoAllowWideHit = false;
        public bool BoAllowFireHit = false;
        public bool BoAllowCrossHit = false;
        public int BoAllowTwinHit = 0;
        public bool BoAllowStoneHit = false;
        public long LatestFireHitTime = 0;
        public long LatestTwinHitTime = 0;
        public long LatestRushRushTime = 0;
        public long LatestStoneHitTime = 0;

        public TCreature()
        {
            BoGhost = false;
            GhostTime = 0;
            Death = false;
            DeathTime = 0;
            WatchTime = HUtil32.GetTickCount();
            Dir = Grobal2.DR_DOWN;
            RaceServer = Grobal2.RC_ANIMAL;
            RaceImage = 0;
            Hair = 0;
            Job = 0;
            Gold = 0;
            GameGold = 0;
            Appearance = 0;
            HoldPlace = true;
            ViewRange = 5;
            HomeMap = "0";
            NeckName = "";
            UserDegree = 0;
            Light = 0;
            DefNameColor = 255;
            HitPowerPlus = 0;
            HitDouble = 0;
            BodyLuck = 0;
            CGHIUseTime = 0;
            CGHIstart = HUtil32.GetTickCount();
            BoCGHIEnable = false;
            BoOldVersionUser_Italy = false;
            BoReadyAdminPassword = false;
            BoReadySuperAdminPassword = false;
            PlusFinalDamage = 0;
            MeetLoverDelayTime = 0;
            BoFearFire = false;
            BoAbilSeeHealGauge = false;
            BoAllowPowerHit = false;
            BoAllowLongHit = false;
            BoAllowWideHit = false;
            BoAllowFireHit = false;
            BoAllowCrossHit = false;
            BoAllowTwinHit = 0;
            BoAllowStoneHit = false;
            AccuracyPoint = ObjBase.DEFHIT;
            SpeedPoint = ObjBase.DEFSPEED;
            HitSpeed = 0;
            LifeAttrib = Grobal2.LA_CREATURE;
            AntiPoison = 0;
            PoisonRecover = 0;
            HealthRecover = 0;
            SpellRecover = 0;
            AntiMagic = 0;
            Luck = 0;
            IncSpell = 0;
            IncHealth = 0;
            IncHealing = 0;
            PerHealth = 5;
            PerHealing = 5;
            PerSpell = 5;
            IncHealthSpellTime = HUtil32.GetTickCount();
            RedPoisonLevel = 0;
            PoisonLevel = 0;
            PlusPoisonFactor = 0;
            FightZoneDieCount = 0;
            AvailableGold = ObjBase.BAGGOLD;
            CharStatus = 0;
            CharStatusEx = 0;

            /*FillChar(StatusArr, sizeof(short) * Grobal2.STATUSARR_SIZE, '\0');
            FillChar(StatusValue, sizeof(byte) * Grobal2.STATUSARR_SIZE, '\0');
            FillChar(BonusAbil, sizeof(TNakedAbility), '\0');
            FillChar(CurBonusAbil, sizeof(TNakedAbility), '\0');
            FillChar(ExtraAbil, sizeof(byte) * Grobal2.EXTRAABIL_SIZE, '\0');
            FillChar(ExtraAbilFlag, sizeof(byte) * Grobal2.EXTRAABIL_SIZE, '\0');
            FillChar(ExtraAbilTimes, sizeof(long) * Grobal2.EXTRAABIL_SIZE, '\0');*/

            AllowGroup = false;
            GroupRequester = "";
            AllowEnterGuild = false;
            FreeGulityCount = 0;
            HumAttackMode = Grobal2.HAM_ALL;
            FBoInFreePKArea = false;
            BoGuildWarArea = false;
            BoCrimeforCastle = false;
            NeverDie = false;
            BoSkeleton = false;
            RushMode = false;
            BoHolySeize = false;
            BoCrazyMode = false;
            BoGoodCrazyMode = false;
            BoOpenHealth = false;
            BoDuplication = false;
            BoAnimal = false;
            BoNoItem = false;
            BodyLeathery = 50;
            HideMode = false;
            StickMode = false;
            NoAttackMode = false;
            NoMaster = false;
            BoIllegalAttack = false;
            ManaToHealthPoint = 0;
            SuckupEnemyHealthRate = 0;
            SuckupEnemyHealth = 0;
            //FillChar(AddAbil, sizeof(TAddAbility), 0);
            MsgList = new ArrayList();
            MsgTargetList = new List<TCreature>();
            PKHiterList = new List<TPkHiterInfo>();
            VisibleActors = new List<TVisibleActor>();
            VisibleItems = new List<TVisibleItemInfo>();
            VisibleEvents = new List<TEvent>();
            ItemList = new List<TUserItem>();
            DealList = new List<TUserItem>();
            DealGold = 0;
            MagicList = new List<TUserMagic>();
            SaveItems = new ArrayList();
            //FillChar(UseItems, sizeof(TUserItem) * 13, '\0');
            PSwordSkill = null;
            PPowerHitSkill = null;
            PLongHitSkill = null;
            PWideHitSkill = null;
            PFireHitSkill = null;
            PCrossHitSkill = null;
            PTwinHitSkill = null;
            PStoneHitSkill = null;
            GroupOwner = null;
            Castle = null;
            Master = null;
            SlaveExp = 0;
            SlaveExpLevel = 0;
            BoSlaveRelax = false;
            GroupMembers = new List<TCreature>();
            BoHearWhisper = true;
            BoHearCry = true;
            BoHearGuildMsg = true;
            BoExchangeAvailable = true;
            BoEnableRecall = false;
            BoEnableAgitRecall = false;
            DailyQuestNumber = 0;
            DailyQuestGetDate = 0;
            WhisperBlockList = new ArrayList();
            SlaveList = new List<TCreature>();
            //FillChar(QuestStates, sizeof(QuestStates), '\0');
            //FillChar(QuestIndexOpenStates, sizeof(QuestIndexOpenStates), '\0');
            //FillChar(QuestIndexFinStates, sizeof(QuestIndexFinStates), '\0');
            Abil.Level = 1;
            Abil.AC = 0;
            Abil.MAC = 0;
            Abil.DC = MakeWord(1, 4);
            Abil.MC = MakeWord(1, 2);
            Abil.SC = MakeWord(1, 2);
            Abil.MP = 15;
            Abil.HP = 15;
            Abil.MaxHP = 15;
            Abil.MaxMP = 15;
            Abil.Exp = 0;
            Abil.MaxExp = 50;
            Abil.Weight = 0;
            Abil.MaxWeight = 100;
            WantRefMsg = false;
            BoDealing = false;
            BoDealEnding = false;
            DealCret = null;
            MyGuild = null;
            GuildRank = 0;
            GuildRankName = "";
            LatestNpcCmd = "";
            BoHasMission = false;
            BoHumHideMode = false;
            BoStoneMode = false;
            BoViewFixedHide = false;
            BoNextTimeFreeCurseItem = false;
            BoFixedHideMode = false;
            BoSysopMode = false;
            BoSuperviserMode = false;
            BoEcho = true;
            BoTaiwanEventUser = false;
            RunTime = (int)(GetCurrentTime + new System.Random(1500).Next());
            RunNextTick = 250;
            SearchRate = 2000 + ((long)new System.Random(2000).Next());
            SearchTime = HUtil32.GetTickCount();
            time4hour = HUtil32.GetTickCount();
            time10min = HUtil32.GetTickCount();
            time500ms = HUtil32.GetTickCount();
            poisontime = HUtil32.GetTickCount();
            time60sec = HUtil32.GetTickCount();
            time30sec = HUtil32.GetTickCount();
            time10sec = HUtil32.GetTickCount();
            time5sec = HUtil32.GetTickCount();
            ticksec = HUtil32.GetTickCount();
            LatestCryTime = 0;
            LatestSpaceMoveTime = 0;
            LatestSpaceScrollTime = 0;
            LatestSearchWhoTime = 0;
            MapMoveTime = HUtil32.GetTickCount();
            SlaveLifeTime = 0;
            NextWalkTime = 1400;
            NextHitTime = 3000;
            WalkCurStep = 0;
            WalkWaitCurTime = HUtil32.GetTickCount();
            BoWalkWaitMode = false;
            HealthTick = 0;
            SpellTick = 0;
            TargetCret = null;
            LastHiter = null;
            LastHiterRace = -1;
            SlaveHiter = null;
            ExpHiter = null;
            RefObjCount = 0;
            FAlreadyDisapper = false;
            ForceMoveToMaster = false;
            BoDontMove = false;
            BoDisapear = false;
            DontBagItemDrop = false;
            DontBagGoldDrop = false;
            DontUseItemDrop = false;
            MasterFeature = 0;
            bStealth = false;
            BodyState = 0;
            LoverPlusAbility = false;
            BoLoseTargetMoment = false;
            PushedCount = 0;
            BoHighLevelEffect = true;
            BoGuildAgitDealTry = false;
            MeltArea = 2;
            m_sMasterName = "";
            m_boMaster = false;
            m_nMasterCount = 0;
            m_nMasterRanking = 0;
        }

        private void SetBoInFreePKArea(bool flag)
        {
            if (FBoInFreePKArea != flag)
            {
                FBoInFreePKArea = flag;
                AreaStateOrNameChanged = true;
            }
        }

        public long GetNextHitTime()
        {
            long result;
            if (StatusArr[Grobal2.POISON_SLOW] > 0)
            {
                result = NextHitTime + NextHitTime / 2;
            }
            else
            {
                result = NextHitTime;
            }
            return result;
        }

        public long GetNextWalkTime()
        {
            long result;
            if (StatusArr[Grobal2.POISON_SLOW] > 0)
            {
                result = NextWalkTime + NextWalkTime / 2;
            }
            else
            {
                result = NextWalkTime;
            }
            return result;
        }

        // 框流老荐 乐唱 眉农窃
        public bool IsMoveAble()
        {
            bool result;
            // 甘惑俊 乐绊
            // 磷瘤 臼绊
            // 惑怕 捞惑捞 酒聪绊
            if (!BoGhost && !Death && (StatusArr[Grobal2.POISON_STONE] == 0) && (StatusArr[Grobal2.POISON_ICE] == 0) && (StatusArr[Grobal2.POISON_STUN] == 0) && (StatusArr[Grobal2.POISON_DONTMOVE] == 0))
            {
                result = true;
            }
            else
            {
                result = false;
            }
            return result;
        }

        public void ChangeItemWithLevel(ref TClientItem citem, int lv)
        {
            if ((citem.S.Shape == ObjBase.DRESS_SHAPE_WING) && ((citem.S.StdMode == ObjBase.DRESS_STDMODE_MAN) || (citem.S.StdMode == ObjBase.DRESS_STDMODE_WOMAN)))
            {
                if (lv >= 20)
                {
                    if (lv < 30)
                    {
                        // 20 ~ 29
                        // 扁夯蔼栏肺 汲沥
                    }
                    else if (lv < 40)// 30 ~ 39
                    {
                        citem.S.DC = (short)(citem.S.DC + MakeWord(0, 1));
                        citem.S.MC = (short)(citem.S.MC + MakeWord(0, 2));
                        citem.S.SC = (short)(citem.S.SC + MakeWord(0, 2));
                        citem.S.AC = (short)(citem.S.AC + MakeWord(2, 3));
                        citem.S.MAC = (short)(citem.S.MAC + MakeWord(0, 2));
                    }
                    else if (lv < 50) // 40 ~ 49
                    {
                        citem.S.DC = (short)(citem.S.DC + MakeWord(0, 3));
                        citem.S.MC = (short)(citem.S.MC + MakeWord(0, 4));
                        citem.S.SC = (short)(citem.S.SC + MakeWord(0, 4));
                        citem.S.AC = (short)(citem.S.AC + MakeWord(5, 5));
                        citem.S.MAC = (short)(citem.S.MAC + MakeWord(1, 2));
                    }
                    else
                    {
                        // 50~
                        citem.S.DC = (short)(citem.S.DC + MakeWord(0, 5));
                        citem.S.MC = (short)(citem.S.MC + MakeWord(0, 6));
                        citem.S.SC = (short)(citem.S.SC + MakeWord(0, 6));
                        citem.S.AC = (short)(citem.S.AC + MakeWord(9, 7));
                        citem.S.MAC = (short)(citem.S.MAC + MakeWord(2, 4));
                    }
                }
            }
        }

        public void ChangeItemByJob(ref TClientItem citem, int lv)
        {
            if ((citem.S.StdMode == 22) && (citem.S.Shape == ObjBase.DRAGON_RING_SHAPE))
            {
                switch (Job)
                {
                    case 0:
                        citem.S.DC = MakeWord(LoByte(citem.S.DC), _MIN(255, HiByte(citem.S.DC) + 4));
                        citem.S.MC = 0;
                        citem.S.SC = 0;
                        break;
                    case 1:
                        citem.S.DC = 0;
                        citem.S.SC = 0;
                        break;
                    case 2:
                        citem.S.MC = 0;
                        break;
                }
            }
            else if ((citem.S.StdMode == 26) && (citem.S.Shape == ObjBase.DRAGON_BRACELET_SHAPE))
            {
                switch (Job)
                {
                    case 0:
                        citem.S.DC = MakeWord(LoByte(citem.S.DC) + 1, _MIN(255, HiByte(citem.S.DC) + 2));
                        citem.S.MC = 0;
                        citem.S.SC = 0;
                        citem.S.AC = MakeWord(LoByte(citem.S.AC), _MIN(255, HiByte(citem.S.AC) + 1));
                        break;
                    case 1:
                        citem.S.DC = 0;
                        citem.S.SC = 0;
                        citem.S.AC = MakeWord(LoByte(citem.S.AC), _MIN(255, HiByte(citem.S.AC) + 1));
                        break;
                    case 2:
                        citem.S.MC = 0;
                        break;
                }
            }
            else if ((citem.S.StdMode == 19) && (citem.S.Shape == ObjBase.DRAGON_NECKLACE_SHAPE))
            {
                switch (Job)
                {
                    case 0:
                        citem.S.MC = 0;
                        citem.S.SC = 0;
                        break;
                    case 1:
                        citem.S.DC = 0;
                        citem.S.SC = 0;
                        break;
                    case 2:
                        citem.S.DC = 0;
                        citem.S.MC = 0;
                        break;
                }
            }
            else if (((citem.S.StdMode == 10) || (citem.S.StdMode == 11)) && (citem.S.Shape == ObjBase.DRAGON_DRESS_SHAPE))
            {
                switch (Job)
                {
                    case 0:
                        citem.S.MC = 0;
                        citem.S.SC = 0;
                        break;
                    case 1:
                        citem.S.DC = 0;
                        citem.S.SC = 0;
                        break;
                    case 2:
                        citem.S.DC = 0;
                        citem.S.MC = 0;
                        break;
                }
            }
            else if ((citem.S.StdMode == 15) && (citem.S.Shape == ObjBase.DRAGON_HELMET_SHAPE))
            {
                switch (Job)
                {
                    case 0:
                        citem.S.MC = 0;
                        citem.S.SC = 0;
                        break;
                    case 1:
                        citem.S.DC = 0;
                        citem.S.SC = 0;
                        break;
                    case 2:
                        citem.S.DC = 0;
                        citem.S.MC = 0;
                        break;
                }
            }
            else if (((citem.S.StdMode == 5) || (citem.S.StdMode == 6)) && (citem.S.Shape == ObjBase.DRAGON_WEAPON_SHAPE))
            {
                switch (Job)
                {
                    case 0:
                        citem.S.DC = MakeWord(LoByte(citem.S.DC) + 1, _MIN(255, HiByte(citem.S.DC) + 28));
                        citem.S.MC = 0;
                        citem.S.SC = 0;
                        citem.S.AC = MakeWord(LoByte(citem.S.AC) - 2, HiByte(citem.S.AC));
                        break;
                    case 1:
                        citem.S.SC = 0;
                        if (HiByte(citem.S.MAC) > 12)
                        {
                            citem.S.MAC = MakeWord(LoByte(citem.S.MAC), HiByte(citem.S.MAC) - 12);
                        }
                        else
                        {
                            citem.S.MAC = MakeWord(LoByte(citem.S.MAC), 0);
                        }
                        break;
                    case 2:
                        citem.S.DC = MakeWord(LoByte(citem.S.DC) + 2, _MIN(255, HiByte(citem.S.DC) + 10));
                        citem.S.MC = 0;
                        citem.S.AC = MakeWord(LoByte(citem.S.AC) - 2, HiByte(citem.S.AC));
                        break;
                }
            }
            else if (citem.S.StdMode == 53)
            {
                if (citem.S.Shape == ObjBase.LOLLIPOP_SHAPE)
                {
                    switch (Job)
                    {
                        case 0:
                            // 傈荤
                            citem.S.DC = MakeWord(LoByte(citem.S.DC), _MIN(255, HiByte(citem.S.DC) + 2));
                            // 窍靛内爹
                            citem.S.MC = 0;
                            citem.S.SC = 0;
                            break;
                        case 1:
                            // 贱荤
                            citem.S.DC = 0;
                            citem.S.MC = MakeWord(LoByte(citem.S.MC), _MIN(255, HiByte(citem.S.MC) + 2));
                            // 窍靛内爹
                            citem.S.SC = 0;
                            break;
                        case 2:
                            // 档荤
                            citem.S.DC = 0;
                            citem.S.MC = 0;
                            citem.S.SC = MakeWord(LoByte(citem.S.SC), _MIN(255, HiByte(citem.S.SC) + 2));
                            break;
                            // 窍靛内爹
                    }
                    // case
                }
                else if ((citem.S.Shape == ObjBase.GOLDMEDAL_SHAPE) || (citem.S.Shape == ObjBase.SILVERMEDAL_SHAPE) || (citem.S.Shape == ObjBase.BRONZEMEDAL_SHAPE))
                {
                    switch (Job)
                    {
                        case 0:
                            citem.S.DC = MakeWord(LoByte(citem.S.DC), _MIN(255, HiByte(citem.S.DC)));
                            citem.S.MC = 0;
                            citem.S.SC = 0;
                            break;
                        case 1:
                            citem.S.DC = 0;
                            citem.S.MC = MakeWord(LoByte(citem.S.MC), _MIN(255, HiByte(citem.S.MC)));
                            citem.S.SC = 0;
                            break;
                        case 2:
                            citem.S.DC = 0;
                            citem.S.MC = 0;
                            citem.S.SC = MakeWord(LoByte(citem.S.SC), _MIN(255, HiByte(citem.S.SC)));
                            break;
                    }
                }
                // 2004-06-29 脚痹癌渴(颇炔玫付狼) 流诀喊 瓷仿摹
                if (((citem.S.StdMode == 10) || (citem.S.StdMode == 11)) && (citem.S.Shape == ObjBase.DRESS_SHAPE_PBKING))
                {
                    switch (Job)
                    {
                        case 0:
                            // 傈荤
                            citem.S.DC = MakeWord(LoByte(citem.S.DC), _MIN(255, HiByte(citem.S.DC) + 2));
                            // 窍靛内爹
                            citem.S.MC = 0;
                            citem.S.SC = 0;
                            citem.S.AC = MakeWord(LoByte(citem.S.AC) + 2, _MIN(255, HiByte(citem.S.AC) + 4));
                            // 窍靛内爹
                            // citem.S.MAC := 0;
                            citem.S.MpAdd = citem.S.MpAdd + 30;
                            break;
                        case 1:
                            citem.S.DC = 0;
                            citem.S.SC = 0;
                            citem.S.MAC = MakeWord(LoByte(citem.S.MAC) + 1, _MIN(255, HiByte(citem.S.MAC) + 2));
                            citem.S.HpAdd = citem.S.HpAdd + 30;
                            break;
                        case 2:
                            citem.S.DC = MakeWord(LoByte(citem.S.DC) + 1, _MIN(255, HiByte(citem.S.DC)));
                            citem.S.MC = 0;
                            citem.S.AC = MakeWord(LoByte(citem.S.AC) + 1, _MIN(255, HiByte(citem.S.AC)));
                            citem.S.MAC = MakeWord(LoByte(citem.S.MAC) + 1, _MIN(255, HiByte(citem.S.MAC)));
                            citem.S.HpAdd = citem.S.HpAdd + 20;
                            citem.S.MpAdd = citem.S.MpAdd + 10;
                            break;
                    }
                }
            }
        }

        public bool CheckUnbindItem(string itemname)
        {
            bool result = false;
            for (var i = 0; i < M2Share.UnbindItemList.Count; i++)
            {
                if (itemname.ToLower().CompareTo(M2Share.UnbindItemList[i].ToLower()) == 0)
                {
                    result = true;
                    break;
                }
            }
            return result;
        }

        public void DeleteItemFromBag(TStdItem psDel, TUserItem puDel)
        {
            TStdItem ps;
            TUserItem pu;
            TUserHuman hum;
            for (var i = 0; i < ItemList.Count; i++)
            {
                if (ItemList[i].MakeIndex == puDel.MakeIndex)
                {
                    ps = M2Share.UserEngine.GetStdItem(ItemList[i].Index);
                    pu = ItemList[i];
                    if (ps.OverlapItem >= 1)
                    {
                        if (pu.Dura > 0)
                        {
                            pu.Dura = (short)(pu.Dura - 1);
                            if (pu.Dura <= 0)
                            {
                                if (RaceServer == Grobal2.RC_USERHUMAN)
                                {
                                    hum = this as TUserHuman;
                                    hum.SendDelItem(ItemList[i]);
                                }
                                Dispose(ItemList[i]);
                                ItemList.RemoveAt(i);
                            }
                            else
                            {
                                SendMsg(this, Grobal2.RM_COUNTERITEMCHANGE, 0, pu.MakeIndex, pu.Dura, 0, ps.Name);
                            }
                        }
                        else
                        {
                            if (RaceServer == Grobal2.RC_USERHUMAN)
                            {
                                hum = this as TUserHuman;
                                hum.SendDelItem(ItemList[i]);
                            }
                            Dispose(ItemList[i]);
                            ItemList.RemoveAt(i);
                        }
                    }
                    else
                    {
                        if (RaceServer == Grobal2.RC_USERHUMAN)
                        {
                            hum = this as TUserHuman;
                            hum.SendDelItem(ItemList[i]);
                        }
                        Dispose(ItemList[i]);
                        ItemList.RemoveAt(i);
                    }
                    break;
                }
            }
            WeightChanged();
        }

        public int FindItemToBindFromBag(int count, string itemname, ref ArrayList dellist)
        {
            int i;
            int j;
            TUserItem pu;
            TStdItem pstd;
            int itemcount;
            int delcount;
            string strItemName = string.Empty;
            int result = -1;
            dellist = null;
            if (itemname != "")
            {
                if (CheckUnbindItem(itemname) == false)
                {
                    return result;
                }
            }
            try
            {
                for (i = 0; i < M2Share.UnbindItemList.Count; i++)
                {
                    if (itemname != "")
                    {
                        if (itemname.ToLower().CompareTo(M2Share.UnbindItemList[i].ToLower()) != 0)
                        {
                            continue;
                        }
                    }
                    itemcount = 0;
                    for (j = 0; j < ItemList.Count; j++)
                    {
                        pstd = M2Share.UserEngine.GetStdItem(ItemList[j].Index);
                        if (pstd != null)
                        {
                            if (pstd.Name.ToLower().CompareTo(M2Share.UnbindItemList[i].ToLower()) == 0)
                            {
                                itemcount++;
                            }
                        }
                    }
                    if (itemcount >= count)
                    {
                        strItemName = M2Share.UnbindItemList[i];
                        // result = (int)svMain.UnbindItemList.Values[i];
                        break;
                    }
                }
                if (result >= 0)
                {
                    delcount = 0;
                    for (i = 0; i < ItemList.Count; i++)
                    {
                        pu = ItemList[i];
                        if (M2Share.UserEngine.GetStdItemName(pu.Index).ToLower().CompareTo(strItemName.ToLower()) == 0)
                        {
                            if (RaceServer == Grobal2.RC_USERHUMAN)
                            {
                                if (dellist == null)
                                {
                                    dellist = new ArrayList();
                                }
                                // dellist.Add(svMain.UserEngine.GetStdItemName(pu.Index), pu.MakeIndex as Object);
                            }
                            delcount++;
                            if (delcount >= count)
                            {
                                break;
                            }
                        }
                    }
                }
            }
            catch
            {
                M2Share.MainOutMessage("[Exception] TUserHuman.FindItemToBindFromBag");
            }
            return result;
        }

        public bool GuildAgitInvitationItemSet(TUserItem pu)
        {
            int AgitNum;
            bool result = false;
            string gname = GetGuildNameHereAgit();
            TGuildAgit guildagit = M2Share.GuildAgitMan.GetGuildAgit(gname);
            if (guildagit == null)
            {
                return result;
            }
            if (UserDegree < Grobal2.UD_ADMIN)
            {
                if (MyGuild == null)
                {
                    return result;
                }
                TGuildAgit myguildagit = M2Share.GuildAgitMan.GetGuildAgit(MyGuild.GuildName);
                if (myguildagit == null)
                {
                    return result;
                }
                if ((guildagit.GuildAgitNumber <= 0) || (myguildagit.GuildAgitNumber <= 0))
                {
                    return result;
                }
                if (guildagit.GuildAgitNumber != myguildagit.GuildAgitNumber)
                {
                    return result;
                }
            }
            AgitNum = guildagit.GuildAgitNumber;
            DateTime nowdate = DateTime.Now;
            var ayear = nowdate.Year;
            var amon = nowdate.Month;
            var aday = nowdate.Day;
            var ahour = nowdate.Hour;
            var amin = nowdate.Minute;
            var asec = nowdate.Second;
            var amsec = nowdate.Millisecond;
            pu.Dura = (short)AgitNum;
            pu.DuraMax = (short)ayear;
            pu.Desc[0] = (byte)amon;
            pu.Desc[1] = (byte)aday;
            pu.Desc[2] = (byte)ahour;
            result = true;
            return result;
        }

        public bool GuildAgitDecoItemSet(TUserItem pu, int Number)
        {
            pu.Dura = (short)Number;
            return true;
        }

        // 檬措厘 蜡瓤扁埃 眉农.
        public bool GuildAgitInvitationTimeOutCheck(TUserItem pu)
        {
            DateTime nowdate;
            short cYear;
            short cMon;
            short cDay;
            short cHour;
            bool result = false;
            try
            {
                nowdate = DateTime.Now;
                cYear = pu.DuraMax;
                cMon = MakeWord(pu.Desc[0], 0);
                cDay = MakeWord(pu.Desc[1], 0);
                cHour = MakeWord(pu.Desc[2], 0);
                if ((cMon == 0) || (cDay == 0))
                {
                    return result;
                }
                //exdate = Convert.ToInt64(new DateTime(cYear, cMon, cDay));
                //extime = new DateTime(0, 0, 0, cHour, cMin, cSec, cMSec);
                //exdatetime = exdate + extime + 1;
                //if (nowdate <= exdatetime)
                //{
                //    result = true;
                //}
            }
            catch
            {
                M2Share.MainOutMessage("[Exception]TCreature.GuildAgitInvitationTimeOutCheck");
            }
            return result;
        }

        public void DecRefObjCount()
        {

        }

        public string ItemOptionToStr(byte[] optiondata)
        {
            string result;
            int i;
            string rtstr;
            rtstr = "";
            try
            {
                for (i = 0; i <= 13; i++)
                {
                    rtstr = rtstr + optiondata[i].ToString();
                }
            }
            catch
            {
                M2Share.MainOutMessage("DO NOT MAKE STRING ITEMOPTION");
            }
            result = rtstr;
            return result;
        }

        public string UpgradeResultToStr(int iSum, string strOpt, int iBefore, int iAfter, double fProb, int iJewelStdMode)
        {
            string rtstr = string.Empty;
            string strJewelType = string.Empty;
            if (iJewelStdMode == 60)
            {
                strJewelType = "宝石";
            }
            else if (iJewelStdMode == 61)
            {
                strJewelType = "水晶球";
            }
            try
            {
                rtstr = iSum.ToString() + "," + strJewelType + "," + strOpt + "," + iBefore.ToString() + "," + iAfter.ToString() + "," + Convert.ToString(fProb);
            }
            catch
            {
                M2Share.MainOutMessage("[Exception!] TCreature.UpgradeResultToStr Cannot Make Log String");
            }
            return rtstr;
        }

        public void SendFastMsg(TCreature sender, short Ident, short wparam, long lParam1, long lParam2, long lParam3, string str)
        {
            TMessageInfoPtr pmsg;
            string ansistr;
            try
            {
                M2Share.csObjMsgLock.Enter();
                if (!BoGhost)
                {
                    pmsg = new TMessageInfoPtr();
                    pmsg.Ident = Ident;
                    pmsg.wParam = wparam;
                    pmsg.lParam1 = (int)lParam1;
                    pmsg.lParam2 = (int)lParam2;
                    pmsg.lParam3 = (int)lParam3;
                    pmsg.sender = sender;
                    ansistr = str;
                    if (ansistr != "")
                    {
                        try
                        {
                            //GetMem(pmsg.descptr, ansistr.Length + 1);
                            //Move(ansistr[1], pmsg.descptr, ansistr.Length + 1);
                        }
                        catch
                        {
                            pmsg.descptr = null;
                        }
                    }
                    else
                    {
                        pmsg.descptr = null;
                    }
                    MsgList.Insert(0, pmsg);
                }
            }
            finally
            {
                M2Share.csObjMsgLock.Leave();
            }
        }

        public void SendMsg(TCreature sender, short Ident, short wparam, long lParam1, long lParam2, long lParam3, string str)
        {
            TMessageInfoPtr pmsg;
            string ansistr;
            try
            {
                M2Share.csObjMsgLock.Enter();
                if (!BoGhost)
                {
                    pmsg = new TMessageInfoPtr();
                    pmsg.Ident = Ident;
                    pmsg.wParam = wparam;
                    pmsg.lParam1 = (int)lParam1;
                    pmsg.lParam2 = (int)lParam2;
                    pmsg.lParam3 = (int)lParam3;
                    pmsg.deliverytime = 0;
                    pmsg.sender = sender;
                    ansistr = str;
                    if (ansistr != "")
                    {
                        try
                        {
                            //GetMem(pmsg.descptr, ansistr.Length + 1);
                            //Move(ansistr[1], pmsg.descptr, ansistr.Length + 1);
                        }
                        catch
                        {
                            pmsg.descptr = null;
                        }
                    }
                    else
                    {
                        pmsg.descptr = null;
                    }
                    MsgList.Add(pmsg);
                }
            }
            finally
            {
                M2Share.csObjMsgLock.Leave();
            }
        }

        public void SendDelayMsg(int sender, short Ident, short wparam, long lParam1, long lParam2, long lParam3, string str, int delay)
        {
            var sener = M2Share.ObjectMgr.Get(sender);
            SendDelayMsg(sener, Ident, wparam, lParam1, lParam2, lParam3, str, delay);
        }

        public void SendDelayMsg(TCreature sender, short Ident, short wparam, long lParam1, long lParam2, long lParam3, string str, int delay)
        {
            TMessageInfoPtr pmsg;
            string ansistr;
            try
            {
                M2Share.csObjMsgLock.Enter();
                if (!BoGhost)
                {
                    pmsg = new TMessageInfoPtr();
                    pmsg.Ident = Ident;
                    pmsg.wParam = wparam;
                    pmsg.lParam1 = (int)lParam1;
                    pmsg.lParam2 = (int)lParam2;
                    pmsg.lParam3 = (int)lParam3;
                    pmsg.deliverytime = GetTickCount + delay;
                    pmsg.sender = sender;
                    ansistr = str;
                    if (ansistr != "")
                    {
                        try
                        {
                            //GetMem(pmsg.descptr, ansistr.Length + 1);
                            //Move(ansistr[1], pmsg.descptr, ansistr.Length + 1);
                        }
                        catch
                        {
                            pmsg.descptr = null;
                        }
                    }
                    else
                    {
                        pmsg.descptr = null;
                    }
                    MsgList.Add(pmsg);
                }
            }
            finally
            {
                M2Share.csObjMsgLock.Leave();
            }
        }

        public void UpdateDelayMsg(TCreature sender, short Ident, short wparam, long lParam1, long lParam2, long lParam3, string str, int delay)
        {
            int i;
            TMessageInfoPtr pmsg;
            M2Share.csObjMsgLock.Enter();
            try
            {
                i = 0;
                while (true)
                {
                    if (i >= MsgList.Count)
                    {
                        break;
                    }
                    if (((TMessageInfoPtr)MsgList[i]).Ident == Ident)
                    {
                        pmsg = (TMessageInfoPtr)MsgList[i];
                        MsgList.RemoveAt(i);
                        if (pmsg.descptr != null)
                        {
                            //FreeMem(pmsg.descptr);
                        }
                        Dispose(pmsg);
                    }
                    else
                    {
                        i++;
                    }
                }
            }
            finally
            {
                M2Share.csObjMsgLock.Leave();
            }
            SendDelayMsg(sender, Ident, wparam, lParam1, lParam2, lParam3, str, delay);
        }

        // ms
        public void UpdateDelayMsgCheckParam1(TCreature sender, short Ident, short wparam, long lParam1, long lParam2, long lParam3, string str, int delay)
        {
            // ms
            int i;
            TMessageInfoPtr pmsg;
            M2Share.csObjMsgLock.Enter();
            try
            {
                i = 0;
                while (true)
                {
                    if (i >= MsgList.Count)
                    {
                        break;
                    }
                    if ((((TMessageInfoPtr)MsgList[i]).Ident == Ident) && (((TMessageInfoPtr)MsgList[i]).lParam1 == lParam1))
                    {
                        pmsg = (TMessageInfoPtr)MsgList[i];
                        MsgList.RemoveAt(i);
                        if (pmsg.descptr != null)
                        {
                            //FreeMem(pmsg.descptr);
                        }
                        Dispose(pmsg);
                    }
                    else
                    {
                        i++;
                    }
                }
            }
            finally
            {
                M2Share.csObjMsgLock.Leave();
            }
            SendDelayMsg(sender, Ident, wparam, lParam1, lParam2, lParam3, str, delay);
        }

        // ms
        public void UpdateMsg(TCreature sender, short Ident, short wparam, long lParam1, long lParam2, long lParam3, string str)
        {
            int i;
            TMessageInfoPtr pmsg;
            M2Share.csObjMsgLock.Enter();
            try
            {
                i = 0;
                while (true)
                {
                    if (i >= MsgList.Count)
                    {
                        break;
                    }
                    if (((TMessageInfoPtr)MsgList[i]).Ident == Ident)
                    {
                        pmsg = (TMessageInfoPtr)MsgList[i];
                        MsgList.RemoveAt(i);
                        if (pmsg.descptr != null)
                        {
                            //FreeMem(pmsg.descptr);
                        }
                        Dispose(pmsg);
                    }
                    else
                    {
                        i++;
                    }
                }
            }
            finally
            {
                M2Share.csObjMsgLock.Leave();
            }
            SendMsg(sender, Ident, wparam, lParam1, lParam2, lParam3, str);
        }

        public bool GetMsg(ref TMessageInfo msg)
        {
            TMessageInfoPtr pmsg;
            int n;
            bool result = false;
            try
            {
                M2Share.csObjMsgLock.Enter();
                n = 0;
                msg.Ident = 0;
                while (MsgList.Count > n)
                {
                    pmsg = (TMessageInfoPtr)MsgList[n];
                    if (pmsg.deliverytime != 0)
                    {
                        if (HUtil32.GetTickCount() < pmsg.deliverytime)
                        {
                            n++;
                            continue;
                        }
                    }
                    MsgList.RemoveAt(n);
                    msg.Ident = pmsg.Ident;
                    msg.wParam = pmsg.wParam;
                    msg.lParam1 = pmsg.lParam1;
                    msg.lParam2 = pmsg.lParam2;
                    msg.lParam3 = pmsg.lParam3;
                    msg.sender = pmsg.sender;
                    if (pmsg.descptr != null)
                    {
                        msg.description = pmsg.descptr;
                        //FreeMem(pmsg.descptr);
                    }
                    else
                    {
                        msg.description = "";
                    }
                    Dispose(pmsg);
                    result = true;
                    break;
                }
            }
            finally
            {
                M2Share.csObjMsgLock.Leave();
            }
            return result;
        }

        public bool GetMapCreatures(TEnvirnoment penv, int x, int y, int area, ArrayList rlist)
        {
            bool result;
            int i;
            int j;
            int k;
            int stx;
            int sty;
            int enx;
            int eny;
            TCreature cret;
            TMapInfo pm = null;
            bool inrange;
            result = false;
            if (rlist == null)
            {
                return result;
            }
            try
            {
                stx = x - area;
                enx = x + area;
                sty = y - area;
                eny = y + area;
                for (i = stx; i <= enx; i++)
                {
                    for (j = sty; j <= eny; j++)
                    {
                        inrange = PEnvir.GetMapXY(i, j, ref pm);
                        if (inrange)
                        {
                            if (pm.OBJList != null)
                            {
                                for (k = pm.OBJList.Count - 1; k >= 0; k--)
                                {
                                    // creature//
                                    if (pm.OBJList[k] != null)
                                    {
                                        if (((TAThing)pm.OBJList[k]).Shape == Grobal2.OS_MOVINGOBJECT)
                                        {
                                            cret = ((TAThing)pm.OBJList[k]).AObject as TCreature;
                                            if (cret != null)
                                            {
                                                if (!cret.BoGhost)
                                                {
                                                    rlist.Add(cret);
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch
            {
                M2Share.MainOutMessage("[TCreature] GetMapCreatures exception");
            }
            result = true;
            return result;
        }

        // 措阿急 规氢狼 甘俊辑 积拱眉 掘绢郴扁.
        public bool GetObliqueMapCreatures(TEnvirnoment penv, int x, int y, int area, int dir, ArrayList rlist)
        {
            bool result;
            int i;
            int j;
            int k;
            int stx;
            int sty;
            int enx;
            int eny;
            TCreature cret;
            TMapInfo pm = null;
            bool inrange;
            result = false;
            if (rlist == null)
            {
                return result;
            }
            try
            {
                switch (dir)
                {
                    case 1:
                        stx = x - area - area;
                        enx = x + area;
                        sty = y - area;
                        eny = y + area + area;
                        break;
                    case 3:
                        stx = x - area - area;
                        enx = x + area;
                        sty = y - area - area;
                        eny = y + area;
                        break;
                    case 5:
                        stx = x - area;
                        enx = x + area + area;
                        sty = y - area - area;
                        eny = y + area;
                        break;
                    case 7:
                        stx = x - area;
                        enx = x + area + area;
                        sty = y - area;
                        eny = y + area + area;
                        break;
                    default:
                        return result;
                        break;
                }
                for (i = stx; i <= enx; i++)
                {
                    for (j = sty; j <= eny; j++)
                    {
                        if ((new ArrayList(new int[] { 3, 7 }).Contains(dir) && (Math.Abs(x - i - (y - j)) <= area)) || (new ArrayList(new int[] { 1, 5 }).Contains(dir) && (Math.Abs(x - i + (y - j)) <= area)))
                        {
                            inrange = PEnvir.GetMapXY(i, j, ref pm);
                            if (inrange)
                            {
                                if (pm.OBJList != null)
                                {
                                    for (k = pm.OBJList.Count - 1; k >= 0; k--)
                                    {
                                        if (pm.OBJList[k] != null)
                                        {
                                            if (((TAThing)pm.OBJList[k]).Shape == Grobal2.OS_MOVINGOBJECT)
                                            {
                                                cret = ((TAThing)pm.OBJList[k]).AObject as TCreature;
                                                if (cret != null)
                                                {
                                                    if (!cret.BoGhost)
                                                    {
                                                        rlist.Add(cret);
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch
            {
                M2Share.MainOutMessage("[TCreature] GetObliqueMapCreatures exception");
            }
            result = true;
            return result;
        }

        public void SendRefMsg(short msg, short wparam, int lParam1, int lParam2, int lParam3, string str)
        {
            int i;
            int j;
            int k;
            int stx;
            int sty;
            int enx;
            int eny;
            TCreature cret;
            TMapInfo pm = null;
            bool inrange;
            byte objshape;
            if (BoSuperviserMode || HideMode)
            {
                return;
            }
            objshape = 0;
            if ((HUtil32.GetTickCount() - WatchTime >= 500) || (MsgTargetList.Count == 0))
            {
                WatchTime = HUtil32.GetTickCount();
                MsgTargetList.Clear();
                stx = CX - 12;
                enx = CX + 12;
                sty = CY - 12;
                eny = CY + 12;
                for (i = stx; i <= enx; i++)
                {
                    for (j = sty; j <= eny; j++)
                    {
                        inrange = PEnvir.GetMapXY(i, j, ref pm);
                        if (inrange)
                        {
                            if (pm.OBJList != null)
                            {
                                for (k = pm.OBJList.Count - 1; k >= 0; k--)
                                {
                                    if (pm.OBJList[k] != null)
                                    {
                                        try
                                        {
                                            objshape = ((TAThing)pm.OBJList[k]).Shape;
                                        }
                                        catch
                                        {
                                            M2Share.MainOutMessage("[Exception] Memory Check Error - SendRefMsg");
                                            continue;
                                        }
                                        if (objshape == Grobal2.OS_MOVINGOBJECT)
                                        {
                                            if (HUtil32.GetTickCount() - ((TAThing)pm.OBJList[k]).ATime >= 5 * 60 * 1000)
                                            {
                                                try
                                                {
                                                    Dispose((TAThing)pm.OBJList[k]);
                                                }
                                                catch
                                                {
                                                    M2Share.MainOutMessage("[Exception] Dispose Error - SendRefMsg");
                                                }
                                                pm.OBJList.RemoveAt(k);
                                                if (pm.OBJList.Count <= 0)
                                                {
                                                    pm.OBJList.Free();
                                                    pm.OBJList = null;
                                                    break;
                                                }
                                            }
                                            else
                                            {
                                                try
                                                {
                                                    cret = ((TAThing)pm.OBJList[k]).AObject as TCreature;
                                                    if (cret != null)
                                                    {
                                                        if (!cret.BoGhost)
                                                        {
                                                            if (cret.RaceServer == Grobal2.RC_USERHUMAN)
                                                            {
                                                                cret.SendMsg(this, msg, wparam, lParam1, lParam2, lParam3, str);
                                                                MsgTargetList.Add(cret);
                                                            }
                                                            else
                                                            {
                                                                if (cret.WantRefMsg)
                                                                {
                                                                    if ((msg == Grobal2.RM_STRUCK) || (msg == Grobal2.RM_HEAR) || (msg == Grobal2.RM_DEATH))
                                                                    {
                                                                        cret.SendMsg(this, msg, wparam, lParam1, lParam2, lParam3, str);
                                                                        MsgTargetList.Add(cret);
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                                catch
                                                {
                                                    pm.OBJList.RemoveAt(k);
                                                    if (pm.OBJList.Count <= 0)
                                                    {
                                                        pm.OBJList.Free();
                                                        pm.OBJList = null;
                                                    }
                                                    M2Share.MainOutMessage("[Exception] TCreatre.SendRefMsg");
                                                    break;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                if (MsgTargetList.Count > 0)
                {
                    for (i = 0; i < MsgTargetList.Count; i++)
                    {
                        cret = MsgTargetList[i];
                        try
                        {
                            if (!cret.BoGhost)
                            {
                                if ((cret.MapName == this.MapName) && (Math.Abs(cret.CX - this.CX) <= 11) && (Math.Abs(cret.CY - this.CY) <= 11))
                                {
                                    if (cret.RaceServer == Grobal2.RC_USERHUMAN)
                                    {
                                        cret.SendMsg(this, msg, wparam, lParam1, lParam2, lParam3, str);
                                    }
                                    else
                                    {
                                        if (cret.WantRefMsg && ((msg == Grobal2.RM_STRUCK) || (msg == Grobal2.RM_HEAR) || (msg == Grobal2.RM_DEATH)))
                                        {
                                            cret.SendMsg(this, msg, wparam, lParam1, lParam2, lParam3, str);
                                        }
                                    }
                                }
                            }
                        }
                        catch
                        {
                            M2Share.MainOutMessage("[Exception] TCreatre.SendRefMsg : Target Wrong :" + this.UserName);
                            MsgTargetList.RemoveAt(i);
                            break;
                        }
                    }
                }
            }
        }

        public void UpdateVisibleGay(TCreature cret)
        {
            TVisibleActor va;
            bool flag = false;
            try
            {
                for (var i = 0; i < VisibleActors.Count; i++)
                {
                    if (cret == (VisibleActors[i].cret as TCreature))
                    {
                        VisibleActors[i].check = 1;
                        flag = true;
                        break;
                    }
                }
            }
            catch
            {
                M2Share.MainOutMessage("[TCreature] UpdateVisibleGay exception");
            }
            try
            {
                if (!flag)
                {
                    va = new TVisibleActor();
                    va.check = 2;
                    va.cret = cret;
                    VisibleActors.Add(va);
                    if ((cret.RaceServer != Grobal2.RC_USERHUMAN) && (!cret.Death))
                    {
                        cret.RefObjCount++;
                    }
                }
            }
            catch
            {
                M2Share.MainOutMessage("[TCreature] UpdateVisibleGay-2 exception");
            }
        }

        public void UpdateVisibleItems(short xx, short yy, TMapItem pmi)
        {
            TVisibleItemInfo pvitem;
            bool flag = false;
            for (var i = 0; i < VisibleItems.Count; i++)
            {
                pvitem = VisibleItems[i];
                if (pvitem.Id == pmi.ItemId)
                {
                    pvitem.check = 1;
                    flag = true;
                    break;
                }
            }
            if (!flag)
            {
                pvitem = new TVisibleItemInfo();
                pvitem.check = 2;
                pvitem.x = xx;
                pvitem.y = yy;
                pvitem.Id = pmi.ItemId;
                pvitem.Name = pmi.Name;
                pvitem.looks = pmi.Looks;
                VisibleItems.Add(pvitem);
            }
        }

        public void UpdateVisibleEvents(int xx, int yy, TEvent mevent)
        {
            TEvent __event;
            bool flag = false;
            for (var i = 0; i < VisibleEvents.Count; i++)
            {
                __event = VisibleEvents[i];
                if (__event == mevent)
                {
                    __event.Check = 1;
                    flag = true;
                    break;
                }
            }
            if (!flag)
            {
                mevent.Check = 2;
                mevent.X = xx;
                mevent.Y = yy;
                VisibleEvents.Add(mevent);
            }
        }

        public void SearchViewRange()
        {
            int stx;
            int enx;
            int sty;
            int eny;
            int i;
            int j;
            int k;
            int down;
            TMapInfo pm = null;
            TVisibleItemInfo pvi;
            TVisibleActor pva;
            TMapItem pmapitem;
            TEvent __event;
            TCreature cret;
            bool inrange;
            string uname = string.Empty;
            int hmcount;
            bool hmcheck;
            byte ObjShape;
            int pvacheck;
            TStdItem ps;
            down = 0;
            ObjShape = 0;
            if (PEnvir == null)
            {
                M2Share.MainOutMessage("nil PEnvir");
                return;
            }
            stx = CX - ViewRange;
            enx = CX + ViewRange;
            sty = CY - ViewRange;
            eny = CY + ViewRange;
            if (stx < 0)
            {
                stx = 0;
            }
            if (enx > PEnvir.MapWidth - 1)
            {
                enx = PEnvir.MapWidth - 1;
            }
            if (sty < 0)
            {
                sty = 0;
            }
            if (eny > PEnvir.MapHeight - 1)
            {
                eny = PEnvir.MapHeight - 1;
            }
            hmcount = 0;
            hmcheck = false;
            try
            {
                for (i = 0; i < VisibleItems.Count; i++)
                {
                    VisibleItems[i].check = 0;
                }
                for (i = 0; i < VisibleEvents.Count; i++)
                {
                    VisibleEvents[i].Check = 0;
                }
                if (!hmcheck)
                {
                    for (i = 0; i < VisibleActors.Count; i++)
                    {
                        VisibleActors[i].check = 0;
                    }
                }
            }
            catch
            {
                M2Share.MainOutMessage("ObjBase SearchViewRange 0");
                KickException();
            }
            try
            {
                for (i = stx; i <= enx; i++)
                {
                    for (j = sty; j <= eny; j++)
                    {
                        inrange = PEnvir.GetMapXY(i, j, ref pm);
                        if (inrange)
                        {
                            if (pm.OBJList != null)
                            {
                                down = 1;
                                k = 0;
                                while (true)
                                {
                                    if (k >= pm.OBJList.Count)
                                    {
                                        break;
                                    }
                                    // -1 do begin //downto 0 do begin
                                    if (pm.OBJList[k] != null)
                                    {
                                        // Check Object wrong Memory 2003-09-15 PDS
                                        try
                                        {
                                            // 皋葛府俊辑 俊矾啊 乐栏搁 劳剂记 吧府备
                                            ObjShape = ((TAThing)pm.OBJList[k]).Shape;
                                        }
                                        catch
                                        {
                                            // 坷宏璃飘俊辑 哗滚府磊.
                                            M2Share.MainOutMessage("DELOBJ-WRONG MEMORY:" + MapName + "," + CX.ToString() + "," + CY.ToString());
                                            pm.OBJList.RemoveAt(k);
                                            continue;
                                        }
                                        // creature
                                        if (ObjShape == Grobal2.OS_MOVINGOBJECT)
                                        {
                                            // 儡惑 八荤窍咯 瘤款促.
                                            // 2003/01/22 矫埃 5盒俊辑 10盒栏肺 函版...NPC 濒冠烙 规瘤
                                            if (HUtil32.GetTickCount() - ((TAThing)pm.OBJList[k]).ATime >= 10 * 60 * 1000)
                                            {
                                                try
                                                {
                                                    Dispose((TAThing)pm.OBJList[k]);
                                                }
                                                finally
                                                {
                                                    pm.OBJList.RemoveAt(k);
                                                }
                                                down = 2;
                                                if (pm.OBJList.Count <= 0)
                                                {
                                                    down = 3;
                                                    pm.OBJList.Free();
                                                    pm.OBJList = null;
                                                    break;
                                                }
                                                continue;
                                            }
                                            cret = ((TAThing)pm.OBJList[k]).AObject as TCreature;
                                            down = 4;
                                            if ((cret != null) && (!cret.BoGhost) && (!cret.HideMode) && (!cret.BoSuperviserMode))
                                            {
                                                down = 5;
                                                // 阁胶磐绰 力寇 矫挪促.
                                                // 各捞 酒聪芭唱
                                                // 林牢捞 乐芭唱
                                                // 气林惑怕芭唱
                                                // 蚌霸固模惑怕芭唱
                                                // 皋技瘤啊 鞘夸窃
                                                // 林牢乐绰 各篮 促 夯促.(荤恩贸烦 埃林)
                                                // 荤恩篮 促 夯促
                                                // 2004/04/21 犬措矫具 八荤吝俊绰 眠啊窍瘤 臼绰促
                                                if ((RaceServer < Grobal2.RC_ANIMAL) || (Master != null) || BoCrazyMode || BoGoodCrazyMode || WantRefMsg || ((cret.Master != null) && (Math.Abs(cret.CX - CX) <= 3) && (Math.Abs(cret.CY - CY) <= 3)) || (cret.RaceServer == Grobal2.RC_USERHUMAN) && !hmcheck)
                                                {
                                                    UpdateVisibleGay(cret);
                                                }
                                                if (cret.RaceServer == Grobal2.RC_USERHUMAN)
                                                {
                                                    hmcount++;
                                                }
                                            }
                                        }
                                        if (RaceServer == Grobal2.RC_USERHUMAN)
                                        {
                                            // item
                                            down = 6;
                                            if (((TAThing)pm.OBJList[k]).Shape == Grobal2.OS_ITEMOBJECT)
                                            {
                                                down = 7;
                                                if (HUtil32.GetTickCount() - ((TAThing)pm.OBJList[k]).ATime > 60 * 60 * 1000)
                                                {
                                                    // 厘盔操固扁 酒捞袍篮 扒靛府瘤 臼绰促.
                                                    pmapitem = (TMapItem)((TAThing)pm.OBJList[k]).AObject;
                                                    ps = M2Share.UserEngine.GetStdItem(pmapitem.UserItem.Index);
                                                    if (ps != null)
                                                    {
                                                        if ((ps.StdMode == ObjBase.STDMODE_OF_DECOITEM) && (ps.Shape == ObjBase.SHAPE_OF_DECOITEM))
                                                        {
                                                            if (pmapitem != null)
                                                            {
                                                                UpdateVisibleItems((short)i, (short)j, pmapitem);
                                                                // 促澜 酒捞袍栏肺 逞绢皑...
                                                                k++;
                                                                continue;
                                                            }
                                                        }
                                                    }
                                                    // 滚赴瘤 1矫埃捞 瘤抄扒 绝矩促. -PDS 肋给瞪 啊瓷己 乐澜
                                                    // Dispose (PTMapItem (PTAThing (pm.ObjList[k]).AObject));
                                                    Dispose((TAThing)pm.OBJList[k]);
                                                    pm.OBJList.RemoveAt(k);
                                                    down = 8;
                                                    if (pm.OBJList.Count <= 0)
                                                    {
                                                        down = 9;
                                                        pm.OBJList.Free();
                                                        pm.OBJList = null;
                                                        break;
                                                    }
                                                    continue;
                                                }
                                                else
                                                {
                                                    down = 10;
                                                    pmapitem = (TMapItem)((TAThing)pm.OBJList[k]).AObject;
                                                    if (pmapitem != null)
                                                    {
                                                        UpdateVisibleItems((short)i, (short)j, pmapitem);
                                                        if ((pmapitem.Ownership != null) || (pmapitem.Droper != null))
                                                        {
                                                            if (HUtil32.GetTickCount() - pmapitem.Droptime > ObjBase.ANTI_MUKJA_DELAY)
                                                            {
                                                                pmapitem.Ownership = null;
                                                                pmapitem.Droper = null;
                                                            }
                                                            else
                                                            {
                                                                // {林狼} 冈磊 焊龋 矫埃捞 5盒(磷篮 某腐 free 蜡抗矫埃)阑 檬苞窍搁
                                                                // 捞 何盒俊辑 滚弊啊 惯积茄促.
                                                                if (pmapitem.Ownership != null)
                                                                {
                                                                    if ((pmapitem.Ownership as TCreature).BoGhost)
                                                                    {
                                                                        pmapitem.Ownership = null;
                                                                    }
                                                                }
                                                                if (pmapitem.Droper != null)
                                                                {
                                                                    if ((pmapitem.Droper as TCreature).BoGhost)
                                                                    {
                                                                        pmapitem.Droper = null;
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                            // event
                                            down = 11;
                                            if (((TAThing)pm.OBJList[k]).Shape == Grobal2.OS_EVENTOBJECT)
                                            {
                                                __event = (TEvent)((TAThing)pm.OBJList[k]).AObject;
                                                if (__event.Visible)
                                                {
                                                    UpdateVisibleEvents(i, j, __event);
                                                }
                                            }
                                        }
                                    }
                                    k++;
                                }
                            }
                        }
                    }
                }
            }
            catch
            {
                M2Share.MainOutMessage(UserName + " " + MapName + "," + CX.ToString() + "," + CY.ToString() + " SearchViewRange 1-" + down.ToString());
                KickException();
            }
            try
            {
                i = 0;
                while (true)
                {
                    if (i >= VisibleActors.Count)
                    {
                        break;
                    }
                    pva = VisibleActors[i];
                    try
                    {
                        // 皋葛府 眉农 2003-09-23 PDS
                        pvacheck = pva.check;
                    }
                    catch
                    {
                        VisibleActors.RemoveAt(i);
                        M2Share.MainOutMessage("DELOBJ-WRONG2 MEMORY:" + MapName + "," + CX.ToString() + "," + CY.ToString());
                        continue;
                    }
                    if (pva.check == 0)
                    {
                        if (RaceServer == Grobal2.RC_USERHUMAN)
                        {
                            cret = pva.cret as TCreature;
                            if (cret != null)
                            {
                                if (!cret.HideMode)
                                {
                                    // HideMode牢 巴篮 RM_DIGDOWN 皋技瘤甫 焊辰促....
                                    SendMsg(cret, Grobal2.RM_DISAPPEAR, 0, 0, 0, 0, "");
                                }
                                // 2003/03/18
                                // 2003/04/01 茄锅 劝己拳等 各狼 版快 SearchViewRange 郴俊辑绰 皑家矫虐瘤 臼绰促
                                // 2003/04/21
                                // cret.DecRefObjCount;
                                // SendMsg (cret, RM_DECREFOBJCOUNT, 0, 0, 0, 0, '');
                            }
                        }
                        VisibleActors.RemoveAt(i);
                        Dispose(pva);
                        continue;
                    }
                    else
                    {
                        if (RaceServer == Grobal2.RC_USERHUMAN)
                        {
                            if (pva.check == 2)
                            {
                                // new enterance creature
                                cret = pva.cret as TCreature;
                                if (cret != this)
                                {
                                    if (cret.Death)
                                    {
                                        if (cret.BoSkeleton)
                                        {
                                            SendMsg(cret, Grobal2.RM_SKELETON, cret.Dir, cret.CX, cret.CY, 0, "");
                                        }
                                        else
                                        {
                                            SendMsg(cret, Grobal2.RM_DEATH, cret.Dir, cret.CX, cret.CY, 0, "");
                                        }
                                    }
                                    else
                                    {
                                        uname = cret.GetUserName();
                                        SendMsg(cret, Grobal2.RM_TURN, cret.Dir, cret.CX, cret.CY, 0, uname);
                                        // 贸澜焊绰 某腐牢 版快
                                        // 厚岿玫林 惑怕 函拳(sonmg 2005/08/19)
                                        if (cret.RaceServer == Grobal2.RC_FOXBEAD)
                                        {
                                            SendMsg(cret, Grobal2.RM_FOXSTATE, cret.Dir, cret.CX, cret.CY, cret.BodyState, uname);
                                        }
                                    }
                                    // SendMsg (cret, RM_USERNAME, 0, 0, 0, 0, cret.UserName);
                                }
                            }
                        }
                    }
                    i++;
                }
            }
            catch
            {
                M2Share.MainOutMessage(MapName + "," + CX.ToString() + "," + CY.ToString() + " SearchViewRange 2");
                KickException();
            }
            try
            {
                if (RaceServer == Grobal2.RC_USERHUMAN)
                {
                    // 荤侩磊 茄抛父 傈崔
                    i = 0;
                    while (true)
                    {
                        if (i >= VisibleItems.Count)
                        {
                            break;
                        }
                        if (VisibleItems[i].check == 0)
                        {
                            // 荤扼咙
                            pvi = VisibleItems[i];
                            SendMsg(this, Grobal2.RM_ITEMHIDE, 0, pvi.Id, pvi.x, pvi.y, "");
                            VisibleItems.RemoveAt(i);
                            Dispose(pvi);
                        }
                        else
                        {
                            if (VisibleItems[i].check == 2)
                            {
                                // new visible item
                                pvi = VisibleItems[i];
                                SendMsg(this, Grobal2.RM_ITEMSHOW, pvi.looks, pvi.Id, pvi.x, pvi.y, pvi.Name);
                            }
                            i++;
                        }
                    }
                    i = 0;
                    while (true)
                    {
                        try
                        {
                            if (i >= VisibleEvents.Count)
                            {
                                break;
                            }
                            try
                            {
                                __event = VisibleEvents[i];
                            }
                            catch
                            {
                                VisibleEvents.RemoveAt(i);
                                if (VisibleEvents.Count > 0)
                                {
                                    continue;
                                }
                                break;
                            }
                            if (__event != null)
                            {
                                if (__event.Check == 0)
                                {
                                    SendMsg(this, Grobal2.RM_HIDEEVENT, 0, __event.EventId, __event.X, __event.Y, "");
                                    VisibleEvents.RemoveAt(i);
                                    if (VisibleEvents.Count > 0)
                                    {
                                        continue;
                                    }
                                }
                                else if (__event.Check == 1)
                                {
                                    __event.Check = 0;
                                }
                                else
                                {
                                    if (__event.Check == 2)
                                    {
                                        SendMsg(this, Grobal2.RM_SHOWEVENT, (short)__event.EventType, __event.EventId, HUtil32.MakeLong(__event.X, __event.EventParam), __event.Y, "");
                                    }
                                }
                            }
                        }
                        catch
                        {
                            break;
                        }
                        i++;
                    }
                }
            }
            catch
            {
                M2Share.MainOutMessage(MapName + "," + CX.ToString() + "," + CY.ToString() + " SearchViewRange 3");
                KickException();
            }
        }

        public int Feature()
        {
            int result;
            result = GetRelFeature(null);
            return result;
        }

        public int GetRelFeature(TCreature who)
        {
            int result;
            byte dress;
            byte weapon;
            byte face;
            TStdItem ps;
            if (RaceServer == Grobal2.RC_USERHUMAN)
            {
                dress = 0;
                if (UseItems[Grobal2.U_DRESS].Index > 0)
                {
                    ps = M2Share.UserEngine.GetStdItem(UseItems[Grobal2.U_DRESS].Index);
                    if (ps != null)
                    {
                        dress = (byte)(ps.Shape * 2);
                    }
                }
                dress = (byte)(dress + Sex);
                weapon = 0;
                if (UseItems[Grobal2.U_WEAPON].Index > 0)
                {
                    ps = M2Share.UserEngine.GetStdItem(UseItems[Grobal2.U_WEAPON].Index);
                    if (ps != null)
                    {
                        weapon = (byte)(ps.Shape * 2);
                    }
                }
                weapon = (byte)(weapon + Sex);
                face = (byte)(Hair * 2 + Sex);
                result = (int)Grobal2.MakeFeature(0, dress, weapon, face);
            }
            else
            {
                if (RaceServer == Grobal2.RC_CLONE)
                {
                    result = (int)MasterFeature;
                }
                else
                {
                    result = (int)Grobal2.MakeFeatureAp(RaceImage, DeathState, Appearance);
                }
            }
            return result;
        }

        // 惑措规俊 蝶扼辑 葛嚼捞 崔扼 焊老 荐 乐澜
        public int GetCharStatus()
        {
            int result;
            int i;
            int s;
            s = 0;
            for (i = 0; i < Grobal2.STATUSARR_SIZE; i++)
            {
                if (StatusArr[i] > 0)
                {
                    s = (int)((long)s | (0x80000000 >> i));
                }
            }
            result = s | (CharStatusEx & 0x0000FFFF);
            // sonmg 荐沥(2004/03/29)

            return result;
        }

        public void InitValues()
        {
            // 瓷仿摹
            WAbil = Abil;
        }

        public virtual void Initialize()
        {
            int i;
            int n;
            InitValues();
            // 付过 八荤
            for (i = 0; i < MagicList.Count; i++)
            {
                n = MagicList[i].Level;
                if (!(n >= 0 && n <= 3))
                {
                    MagicList[i].Level = 0;
                }
            }
            // 甘俊 殿厘
            ErrorOnInit = true;
            // 般魔倾侩
            if (PEnvir.CanWalk(CX, CY, true))
            {
                if (Appear())
                {
                    ErrorOnInit = false;
                }
            }
            CharStatus = GetCharStatus();
            AddBodyLuck(0);
        }

        public virtual void Finalize()
        {

        }

        public int GetMasterRace()
        {
            int result = -1;
            if (Master != null)
            {
                result = Master.RaceServer;
            }
            return result;
        }

        public void FeatureChanged()
        {
            SendRefMsg(Grobal2.RM_FEATURECHANGED, 0, Feature(), 0, 0, "");
        }

        public void CharStatusChanged()
        {
            SendRefMsg(Grobal2.RM_CHARSTATUSCHANGED, HitSpeed, CharStatus, 0, 0, "");
        }

        public bool Appear()
        {
            bool result;
            object outofrange;
            outofrange = PEnvir.AddToMap(CX, CY, Grobal2.OS_MOVINGOBJECT, this);
            if (outofrange == null)
            {
                result = false;
            }
            else
            {
                result = true;
            }
            if (!HideMode)
            {
                SendRefMsg(Grobal2.RM_TURN, Dir, CX, CY, 0, "");
            }
            return result;
        }

        public bool Disappear(int num)
        {
            bool result;
            if (FAlreadyDisapper)
            {
                return true;
            }
            int rtn = PEnvir.DeleteFromMap(CX, CY, Grobal2.OS_MOVINGOBJECT, this);
            if (rtn != 1)
            {
                M2Share.MainOutMessage("DeleteFromMapError[" + rtn.ToString() + "]" + PEnvir.MapName + "," + CX.ToString() + "," + CY.ToString() + ":" + num.ToString());
                result = false;
            }
            else
            {
                SendRefMsg(Grobal2.RM_DISAPPEAR, 0, 0, 0, 0, "");
                result = true;
            }
            return result;
        }

        public void KickException()
        {
            TUserHuman hum;
            if (RaceServer == Grobal2.RC_USERHUMAN)
            {
                MapName = HomeMap;
                CX = HomeX;
                CY = HomeY;
                hum = this as TUserHuman;
                hum.EmergencyClose = true;
            }
            else
            {
                Death = true;
                DeathTime = HUtil32.GetTickCount();
                MakeGhost(3);
            }
        }

        public bool Walk(int msg)
        {
            TMapInfo pm = null;
            TAThing pat;
            TGateInfo pgate;
            bool inrange;
            TUserHuman hum;
            TEvent __event;
            int down = 0;
            bool result = true;
            try
            {
                inrange = PEnvir.GetMapXY(CX, CY, ref pm);
                pgate = null;
                __event = null;
                if (inrange)
                {
                    down = 1;
                    if (pm.OBJList != null)
                    {
                        down = 2;
                        for (var i = 0; i < pm.OBJList.Count; i++)
                        {
                            down = 3;
                            pat = (TAThing)pm.OBJList[i];
                            if (pat.Shape == Grobal2.OS_GATEOBJECT)
                            {
                                down = 4;
                                pgate = (TGateInfo)pat.AObject;
                            }
                            if (pat.Shape == Grobal2.OS_EVENTOBJECT)
                            {
                                down = 5;
                                if (((TEvent)pat.AObject).OwnCret != null)
                                {
                                    __event = (TEvent)pat.AObject;
                                }
                                continue;
                            }
                            if (pat.Shape == Grobal2.OS_MAPEVENT)
                            {
                                // ???
                            }
                            if (pat.Shape == Grobal2.OS_DOOR)
                            {
                            }
                            if (pat.Shape == Grobal2.OS_ROON)
                            {
                                // proon := PTRoonInfo (pat.AObject);
                            }
                        }
                    }
                }
                down = 10;
                if (__event != null)
                {
                    down = 11;
                    if (__event.OwnCret.IsProperTarget(this))
                    {
                        down = 12;
                        SendMsg(__event.OwnCret, Grobal2.RM_MAGSTRUCK_MINE, 0, __event.Damage, 0, 0, "");
                    }
                }
                down = 20;
                if (result && (pgate != null))
                {
                    down = 21;
                    if (RaceServer == Grobal2.RC_USERHUMAN)
                    {
                        if (PEnvir.AroundDoorOpened(CX, CY))
                        {
                            if (((TEnvirnoment)pgate.EnterEnvir).NeedHole)
                            {
                                if (M2Share.EventMan.FindEvent(PEnvir, CX, CY, Grobal2.ET_DIGOUTZOMBI) == null)
                                {
                                    // goto needholefinish; 
                                }
                            }
                            if (M2Share.ServerIndex == ((TEnvirnoment)pgate.EnterEnvir).Server)
                            {
                                if (!EnterAnotherMap((TEnvirnoment)pgate.EnterEnvir, pgate.EnterX, pgate.EnterY))
                                {
                                    result = false;
                                }
                            }
                            else
                            {
                                hum = this as TUserHuman;
                                if (HUtil32.GetTickCount() - hum.LatestDropTime > 1000)
                                {
                                    if (Disappear(1) == true)
                                    {
                                        SpaceMoved = true;
                                        hum = this as TUserHuman;
                                        hum.ChangeMapName = ((TEnvirnoment)pgate.EnterEnvir).MapName;
                                        hum.ChangeCX = pgate.EnterX;
                                        hum.ChangeCY = pgate.EnterY;
                                        hum.BoChangeServer = true;
                                        hum.ChangeToServerNumber = ((TEnvirnoment)pgate.EnterEnvir).Server;
                                        hum.EmergencyClose = true;
                                        hum.SoftClosed = true;
                                        hum.FAlreadyDisapper = true;
                                    }
                                    else
                                    {
                                        result = false;
                                    }
                                }
                                else
                                {
                                    result = false;
                                }
                            }
                            // needholefinish:
                        }
                    }
                    else
                    {
                        result = false;
                    }
                }
                else
                {
                    down = 22;
                    if (result)
                    {
                        SendRefMsg((short)msg, Dir, CX, CY, 0, "");
                    }
                }
            }
            catch
            {
                M2Share.MainOutMessage("[TCreature] Walk exception " + MapName + " " + CX.ToString() + ":" + CY.ToString() + ">" + down.ToString());
            }
            return result;
        }

        public bool EnterAnotherMap(TEnvirnoment enterenvir, short enterx, short entery)
        {
            bool result;
            int i;
            short oldx;
            short oldy;
            TMapInfo pm = null;
            TEnvirnoment oldpenvir;
            result = false;
            if (enterenvir == null)
            {
                M2Share.MainOutMessage("ERROR! EnterAnotherMap Enviroment is NIL");
                return result;
            }
            try
            {
                if (Abil.Level < enterenvir.NeedLevel)
                {
                    return result;
                }
                if (enterenvir.MapQuest != null)
                {
                    ((TMerchant)enterenvir.MapQuest).UserCall(this);
                }
                if (enterenvir.NeedSetNumber >= 0)
                {
                    if (GetQuestMark(enterenvir.NeedSetNumber) != enterenvir.NeedSetValue)
                    {
                        return result;
                    }
                }
                if (!enterenvir.GetMapXY(enterx, entery, ref pm))
                {
                    return result;
                }
                if (enterenvir == M2Share.UserCastle.CorePEnvir)
                {
                    // 荤合己狼 郴己牢 版快
                    if (RaceServer == Grobal2.RC_USERHUMAN)
                    {
                        if (!M2Share.UserCastle.CanEnteranceCoreCastle(CX, CY, this as TUserHuman))
                        {
                            return result;
                        }
                    }
                    // 甸绢哎 荐 绝澜.
                }
                oldpenvir = PEnvir;
                oldx = CX;
                oldy = CY;
                // 2) 瘤陛 甘俊辑 栋巢, 函荐 檬扁拳
                // 父距 荤扼瘤瘤 臼绰促搁 弊成 唱埃促.
                // if Disappear(2) = false then   Exit;
                Disappear(2);
                try
                {
                    MsgTargetList.Clear();
                }
                catch
                {
                    M2Share.MainOutMessage("[Exception] MsgTargetList.Clear");
                }
                try
                {
                    for (i = 0; i < VisibleItems.Count; i++)
                    {
                        Dispose(VisibleItems[i]);
                    }
                }
                catch
                {
                    M2Share.MainOutMessage("[Exception] VisbleItems Dispose(..)");
                }
                try
                {
                    VisibleItems.Clear();
                }
                catch
                {
                    M2Share.MainOutMessage("[Exception] VisbleItems.Clear");
                }
                try
                {
                    VisibleEvents.Clear();
                }
                catch
                {
                    M2Share.MainOutMessage("[Exception] VisbleEvents.Clear");
                }
                try
                {
                    i = 0;
                    while (true)
                    {
                        if (i >= VisibleActors.Count)
                        {
                            break;
                        }
                        Dispose(VisibleActors[i]);
                        VisibleActors.RemoveAt(i);
                    }
                }
                catch
                {
                    M2Share.MainOutMessage("[Exception] VisbleActors Dispose(..)");
                }
                try
                {
                    VisibleActors.Clear();
                }
                catch
                {
                    M2Share.MainOutMessage("[Exception] VisbleActors.Clear");
                }
                SendMsg(this, Grobal2.RM_CLEAROBJECTS, 0, 0, 0, 0, "");
                PEnvir = enterenvir;
                MapName = enterenvir.MapName;
                CX = enterx;
                CY = entery;
                SendMsg(this, Grobal2.RM_CHANGEMAP, 0, 0, 0, 0, enterenvir.GetGuildAgitRealMapName());
                if (Appear())
                {
                    MapMoveTime = HUtil32.GetTickCount();
                    SpaceMoved = true;
                    result = true;
                }
                else
                {
                    MapName = oldpenvir.MapName;
                    PEnvir = oldpenvir;
                    CX = oldx;
                    CY = oldy;
                    if (null == PEnvir.AddToMap(CX, CY, Grobal2.OS_MOVINGOBJECT, this))
                    {
                        M2Share.MainOutMessage("ERROR NOT ADDTOMAP EnterAnotherMap:" + MapName + "," + CX.ToString() + "," + CY.ToString());
                    }
                }
                if (PEnvir.Fight3Zone && (PEnvir.Fight3Zone != oldpenvir.Fight3Zone))
                {
                    UserNameChanged();
                }
            }
            catch
            {
                M2Share.MainOutMessage("[TCreature] EnterAnotherMap exception");
            }
            return result;
        }

        public void Turn(byte dir)
        {
            this.Dir = dir;
            SendRefMsg(Grobal2.RM_TURN, dir, CX, CY, 0, "");
        }

        public virtual void Say(string saystr)
        {
            //   SendRefMsg(Grobal2.RM_HEAR, 0, System.Drawing.Color.Black, System.Drawing.Color.White, 0, UserName + ": " + saystr);
        }

        public void SysMsg(string str, int mode)
        {
            if (RaceServer != Grobal2.RC_USERHUMAN)
            {
                return;
            }
            switch (mode)
            {
                case 1:
                    SendMsg(this, Grobal2.RM_SYSMESSAGE2, 0, 0, 0, 0, str);
                    break;
                case 2:
                    SendMsg(this, Grobal2.RM_SYSMSG_BLUE, 0, 0, 0, 0, str);
                    break;
                case 3:
                    SendMsg(this, Grobal2.RM_SYSMESSAGE3, 0, 0, 0, 0, str);
                    break;
                case 4:
                    SendMsg(this, Grobal2.RM_SYSMSG_REMARK, 0, 0, 0, 0, str);
                    break;
                case 5:
                    SendMsg(this, Grobal2.RM_SYSMSG_PINK, 0, 0, 0, 0, str);
                    break;
                case 6:
                    SendMsg(this, Grobal2.RM_SYSMSG_GREEN, 0, 0, 0, 0, str);
                    break;
                default:
                    SendMsg(this, Grobal2.RM_SYSMESSAGE, 0, 0, 0, 0, str);
                    break;
            }
        }

        public void BoxMsg(string str, int mode)
        {
            if (RaceServer != Grobal2.RC_USERHUMAN)
            {
                M2Share.MainOutMessage("TCreature.BoxMsg : not Human");
                return;
            }
            SendMsg(this, Grobal2.RM_MENU_OK, 0, this.ActorId, 0, 0, str);
        }

        public void GroupMsg(string str)
        {
            int i;
            if (GroupOwner != null)
            {
                for (i = 0; i < GroupOwner.GroupMembers.Count; i++)
                {
                    GroupOwner.GroupMembers[i].SendMsg(this, Grobal2.RM_GROUPMESSAGE, 0, 0, 0, 0, "-" + str);
                }
            }
        }

        public void NilMsg(string str)
        {
            SendMsg(null, Grobal2.RM_HEAR, 0, 0, 0, 0, str);
        }

        public void MakeGhost(int num)
        {
            // 肯傈洒 磷澜, 荤扼龙 抗沥
            BoGhost = true;
            GhostTime = HUtil32.GetTickCount();
            if (Disappear(3) == false)
            {
                M2Share.MainOutMessage("Not MakeGhost: " + this.UserName + "," + this.MapName + "," + this.CX.ToString() + "," + this.CY.ToString() + ":" + num.ToString());
            }
            else
            {
                this.FAlreadyDisapper = true;
            }
        }

        public void ApplyMeatQuality()
        {
            // 悼拱(荤娇)牢 版快 绊扁前龙..
            int i;
            TStdItem pstd;
            for (i = 0; i < ItemList.Count; i++)
            {
                pstd = M2Share.UserEngine.GetStdItem(ItemList[i].Index);
                if (pstd != null)
                {
                    if (pstd.StdMode == 40)
                    {
                        // 绊扁耽绢府牢 版快
                        ItemList[i].Dura = (short)MeatQuality;
                    }
                }
            }
        }

        // 措惑捞 阁胶磐俊霸父 荤侩窃
        public bool TakeCretBagItems(TCreature target)
        {
            bool result;
            TUserHuman hum;
            TStdItem ps;
            bool IsAddNew;
            string countstr;
            result = false;
            countstr = "";
            while (true)
            {
                if (target.ItemList.Count <= 0)
                {
                    break;
                }
                ps = M2Share.UserEngine.GetStdItem(target.ItemList[0].Index);
                // pu.Index);   // gadget: 墨款飘酒捞袍
                if (ps != null)
                {
                    IsAddNew = true;
                    if (ps.OverlapItem >= 1)
                    {
                        if (target.ItemList[0].Dura > 0)
                        {
                            countstr = "(" + target.ItemList[0].Dura.ToString() + ")";
                            // 肺弊甫 困茄 酒捞袍 俺荐(sonmg 2005/10/27)
                            if (UserCounterItemAdd(ps.StdMode, ps.Looks, target.ItemList[0].Dura, ps.Name, false))
                            {
                                IsAddNew = false;
                                // 肺弊巢辫
                                if (!M2Share.IsCheapStuff(ps.StdMode))
                                {
                                    // 凛扁_
                                    // 戒绢辑 掘扁
                                    M2Share.AddUserLog("4\09" + MapName + "\09" + CX.ToString() + "\09" + CY.ToString() + "\09" + UserName + "\09" + ps.Name + "\09" + target.ItemList[0].MakeIndex.ToString() + "\09" + "0\09" + "0" + countstr);
                                    // 俺荐肺弊(sonmg 2005/01/07)
                                }
                                target.ItemList.RemoveAt(0);
                                result = true;
                            }
                        }
                        else
                        {
                            target.ItemList[0].Dura = 1;
                            // sonmg
                        }
                    }
                    if (IsAddNew)
                    {
                        if (AddItem(target.ItemList[0]))
                        {
                            if (RaceServer == Grobal2.RC_USERHUMAN)
                            {
                                if (this is TUserHuman)
                                {
                                    hum = this as TUserHuman;
                                    hum.SendAddItem(target.ItemList[0]);
                                    // 肺弊巢辫
                                    if (!M2Share.IsCheapStuff(ps.StdMode))
                                    {
                                        // 凛扁_
                                        // 戒绢辑 掘扁
                                        M2Share.AddUserLog("4\09" + MapName + "\09" + CX.ToString() + "\09" + CY.ToString() + "\09" + UserName + "\09" + ps.Name + "\09" + target.ItemList[0].MakeIndex.ToString() + "\09" + "0\09" + "0" + countstr);
                                        // 俺荐肺弊(sonmg 2005/01/07)
                                    }
                                    result = true;
                                }
                            }
                            target.ItemList.RemoveAt(0);
                        }
                        else
                        {
                            break;
                        }
                    }
                }
                else
                {
                    break;
                }
            }
            return result;
        }

        // 荤扼咙..
        // 磷绢辑 啊规俊 酒捞袍阑 汝覆.. 傈何 汝赴促.
        // itemownershiop : 酒捞袍阑 冈阑 荐 乐绰 荤恩, 阁胶磐俊辑 汝啡阑 版快俊父 利侩等促.
        // 阁胶磐狼 酒捞袍阑 菌绰瘤 犬牢窍绰 侩档肺 荤侩等促.
        public void ScatterBagItems(object itemownership)
        {
            int i;
            int dropwide;
            int drcount;
            int icount;
            TUserItem pu;
            TUserItem newpu;
            TStdItem pstd;
            ArrayList dellist;
            bool boDropall;
            dellist = null;
            if (DontBagItemDrop)
            {
                DontBagItemDrop = false;
                return;
            }
            boDropall = true;
            if (RaceServer == Grobal2.RC_USERHUMAN)
            {
                dropwide = 2;
                if (PKLevel() < 2)
                {
                    boDropall = false;
                }
                // 荤恩篮 1/3犬伏肺 冻焙促.
                // 弧盎捞绰 促 冻焙促.
            }
            else
            {
                dropwide = 3;
            }
            try
            {
                for (i = ItemList.Count - 1; i >= 0; i--)
                {
                    pu = ItemList[i];
                    pstd = M2Share.UserEngine.GetStdItem(pu.Index);
                    if (pstd == null)
                    {
                        continue;
                    }
                    // 磷菌阑锭 惑泅林赣聪绰 救冻备霸...(sonmg 2004/08/09)
                    if ((pstd.StdMode == ObjBase.STDMODE_OF_DECOITEM) && (pstd.Shape == ObjBase.SHAPE_OF_DECOITEM))
                    {
                        continue;
                    }
                    if ((RaceServer == Grobal2.RC_USERHUMAN) && ((pstd.UniqueItem & 0x04) != 0))
                    {
                        continue;
                    }
                    if (BoTaiwanEventUser)
                    {
                        if (pstd.StdMode == ObjBase.TAIWANEVENTITEM)
                        {
                            if (DropItemDown(ItemList[i], dropwide, true, itemownership, this, 0))
                            {
                                if (RaceServer == Grobal2.RC_USERHUMAN)
                                {
                                    if (dellist == null)
                                    {
                                        dellist = new ArrayList();
                                    }
                                    // dellist.Add(svMain.UserEngine.GetStdItemName(pu.Index), pu.MakeIndex as Object);
                                }
                                Dispose(ItemList[i]);
                                ItemList.RemoveAt(i);
                            }
                        }
                    }
                    else
                    {
                        if ((M2Share.PHILIPPINEVERSION && (new System.Random(6).Next() == 0)) || (!M2Share.PHILIPPINEVERSION && (new System.Random(3).Next() == 0)) || boDropall)
                        {
                            if (pstd.OverlapItem >= 1)
                            {
                                icount = pu.Dura;
                                drcount = _MAX(1, new System.Random(icount / 2).Next());
                                icount = _MAX(0, icount - drcount);
                                if (drcount > 0)
                                {
                                    newpu = new TUserItem();
                                    if (M2Share.UserEngine.CopyToUserItemFromName(pstd.Name, ref newpu))
                                    {
                                        newpu.Dura = (short)drcount;
                                        if (DropItemDown(newpu, dropwide, true, itemownership, this, 0))
                                        {
                                            pu.Dura = (short)icount;
                                            if (pu.Dura <= 0)
                                            {
                                                pu = ItemList[i];
                                                if (RaceServer == Grobal2.RC_USERHUMAN)
                                                {
                                                    if (dellist == null)
                                                    {
                                                        dellist = new ArrayList();
                                                    }
                                                    //   dellist.Add(svMain.UserEngine.GetStdItemName(pu.Index), pu.MakeIndex as Object);
                                                }
                                                Dispose(ItemList[i]);
                                                ItemList.RemoveAt(i);
                                            }
                                            else
                                            {
                                                SendMsg(this, Grobal2.RM_COUNTERITEMCHANGE, 0, pu.MakeIndex, pu.Dura, 0, pstd.Name);
                                            }
                                        }
                                    }
                                    if (newpu != null)
                                    {
                                        Dispose(newpu);
                                    }
                                }
                            }
                            else
                            {
                                pu = ItemList[i];
                                if (RaceServer != Grobal2.RC_USERHUMAN)
                                {
                                    if (pstd.StdMode == 43)
                                    {
                                        pu.Dura = GetPurity();
                                    }
                                }
                                if (DropItemDown(pu, dropwide, true, itemownership, this, 0))
                                {
                                    if (RaceServer == Grobal2.RC_USERHUMAN)
                                    {
                                        if (dellist == null)
                                        {
                                            dellist = new ArrayList();
                                        }
                                        // dellist.Add(svMain.UserEngine.GetStdItemName(pu.Index), pu.MakeIndex as Object);
                                    }
                                    Dispose(ItemList[i]);
                                    ItemList.RemoveAt(i);
                                }
                            }
                        }
                    }
                }
                if (dellist != null)
                {
                    //  SendMsg(this, Grobal2.RM_DELITEMS, 0, (int)dellist, 0, 0, "");
                }
            }
            catch
            {
                M2Share.MainOutMessage("[Exception] TCreature.ScatterBagItems");
            }
        }

        public void DropEventItems()
        {
            int dropwide;
            TUserItem pu;
            TStdItem pstd;
            ArrayList dellist;
            dellist = null;
            dropwide = 3;
            try
            {
                for (var i = ItemList.Count - 1; i >= 0; i--)
                {
                    pstd = M2Share.UserEngine.GetStdItem(ItemList[i].Index);
                    if (pstd != null)
                    {
                        if (pstd.StdMode == ObjBase.TAIWANEVENTITEM)
                        {
                            if (DropItemDown(ItemList[i], dropwide, true, null, this, 0))
                            {
                                pu = ItemList[i];
                                if (RaceServer == Grobal2.RC_USERHUMAN)
                                {
                                    if (dellist == null)
                                    {
                                        dellist = new ArrayList();
                                    }
                                    // dellist.Add(svMain.UserEngine.GetStdItemName(pu.Index), pu.MakeIndex as Object);
                                }
                                Dispose(ItemList[i]);
                                ItemList.RemoveAt(i);
                            }
                        }
                    }
                }
                if (dellist != null)
                {
                    // SendMsg(this, Grobal2.RM_DELITEMS, 0, (int)dellist, 0, 0, "");
                }
            }
            catch
            {
                M2Share.MainOutMessage("[Exception] TCreature.DropEventItems");
            }
        }

        public void ScatterGolds(object itemownership)
        {
            const int dropmax = 2000;
            int i;
            int ngold;
            if (DontBagGoldDrop)
            {
                DontBagGoldDrop = false;
                return;
            }
            if (Gold > 0)
            {
                for (i = 0; i <= 16; i++)
                {
                    if (Gold > dropmax)
                    {
                        ngold = dropmax;
                        // Gold := Gold - dropmax;
                        DecGold(dropmax);
                    }
                    else
                    {
                        ngold = Gold;
                        Gold = 0;
                    }
                    if (ngold > 0)
                    {
                        if (!DropGoldDown(ngold, true, itemownership, this))
                        {
                            // Gold := Gold + ngold;
                            IncGold(ngold);
                            break;
                        }
                    }
                    else
                    {
                        break;
                    }
                }
                GoldChanged();
            }
        }

        public void DropUseItems(object itemownership, bool DieFromMob)
        {
            int i;
            int ran;
            ArrayList dellist;
            TStdItem ps;
            dellist = null;
            try
            {
                if (DontUseItemDrop)
                {
                    DontUseItemDrop = false;
                    return;
                }
                if ((RaceServer == Grobal2.RC_USERHUMAN) && !BoOldVersionUser_Italy)
                {
                    if (M2Share.KOREANVERSION || M2Share.PHILIPPINEVERSION)
                    {
                        ps = M2Share.UserEngine.GetStdItem(UseItems[Grobal2.U_CHARM].Index);
                        if (ps != null)
                        {
                            if ((UseItems[Grobal2.U_CHARM].Index == M2Share.INDEX_CHOCOLATE) || ((ps.StdMode == 53) && (ps.Shape == ObjBase.SHAPE_OF_LUCKYLADLE)) || (UseItems[Grobal2.U_CHARM].Index == M2Share.INDEX_CANDY) || (UseItems[Grobal2.U_CHARM].Index == M2Share.INDEX_LOLLIPOP))
                            {
                                if (DieFromMob)
                                {
                                    if (dellist == null)
                                    {
                                        dellist = new ArrayList();
                                    }
                                    // dellist.Add(svMain.UserEngine.GetStdItemName(UseItems[Grobal2.U_CHARM].Index), UseItems[Grobal2.U_CHARM].MakeIndex as Object);
                                    M2Share.AddUserLog("16\09" + MapName + "\09" + CX.ToString() + "\09" + CY.ToString() + "\09" + UserName + "\09" + ps.Name + "\09" + UseItems[Grobal2.U_CHARM].MakeIndex.ToString() + "\09" + HUtil32.BoolToIntStr(RaceServer == Grobal2.RC_USERHUMAN).ToString() + "\09" + "0");
                                    UseItems[Grobal2.U_CHARM].Index = 0;
                                }
                                DontBagItemDrop = true;
                                if (dellist != null)
                                {
                                    // SendMsg(this, Grobal2.RM_DELITEMS, 0, (int)dellist, 0, 0, "");
                                }
                                return;
                            }
                        }
                    }
                    for (i = 0; i <= 12; i++)
                    {
                        if ((i == Grobal2.U_CHARM) && !DieFromMob)
                        {
                            continue;
                        }
                        ps = M2Share.UserEngine.GetStdItem(UseItems[i].Index);
                        if (ps != null)
                        {
                            if ((ps.ItemDesc & Grobal2.IDC_DIEANDBREAK) != 0)
                            {
                                if (!DieFromMob)
                                {
                                    continue;
                                }
                                if (dellist == null)
                                {
                                    dellist = new ArrayList();
                                }
                                //  dellist.Add(svMain.UserEngine.GetStdItemName(UseItems[i].Index), UseItems[i].MakeIndex as Object);
                                M2Share.AddUserLog("16\09" + MapName + "\09" + CX.ToString() + "\09" + CY.ToString() + "\09" + UserName + "\09" + ps.Name + "\09" + UseItems[i].MakeIndex.ToString() + "\09" + HUtil32.BoolToIntStr(RaceServer == Grobal2.RC_USERHUMAN).ToString() + "\09" + "0");
                                UseItems[i].Index = 0;
                            }
                        }
                    }
                }
                if (PKLevel() >= 3)
                {
                    ran = 15;
                }
                else
                {
                    ran = 30;
                }
                for (i = 0; i <= 12; i++)
                {
                    if (new System.Random(ran).Next() == 0)
                    {
                        if ((i == Grobal2.U_WEAPON) && (PKLevel() < 3))
                        {
                            if (new System.Random(2).Next() == 0)
                            {
                                continue;
                            }
                        }
                        if (DropItemDown(UseItems[i], 2, true, itemownership, this, 1))
                        {
                            ps = M2Share.UserEngine.GetStdItem(UseItems[i].Index);
                            if (ps != null)
                            {
                                if ((ps.ItemDesc & Grobal2.IDC_NEVERLOSE) == 0)
                                {
                                    if (RaceServer == Grobal2.RC_USERHUMAN)
                                    {
                                        if (dellist == null)
                                        {
                                            dellist = new ArrayList();
                                        }
                                        // dellist.Add(svMain.UserEngine.GetStdItemName(UseItems[i].Index), UseItems[i].MakeIndex as Object);
                                    }
                                    UseItems[i].Index = 0;
                                }
                            }
                        }
                    }
                }
                if (dellist != null)
                {
                    // SendMsg(this, Grobal2.RM_DELITEMS, 0, (int)dellist, 0, 0, "");
                }
            }
            catch
            {
                M2Share.MainOutMessage("[Exception] TCreature.DropUseItems");
            }
        }

        public char Die_BoolToChar(bool flag)
        {
            char result;
            if (flag)
            {
                result = 'T';
            }
            else
            {
                result = 'F';
            }
            return result;
        }

        public virtual void Die()
        {
            int i;
            int exp;
            bool guildwarkill;
            string str;
            TCreature ehiter;
            TCreature cret;
            bool boBadKill;
            bool bogroupcall;
            TMerchant questnpc;
            TStdItem ps;
            bool KingMobLogFlag;
            TUserHuman hum;
            string strFZNumber;
            int svidx = 0;
            string lovername;
            if (NeverDie)
            {
                return;
            }
            Death = true;
            DeathTime = HUtil32.GetTickCount();
            ClearPkHiterList();
            if (Master != null)
            {
                ExpHiter = null;
                LastHiter = null;
            }
            IncSpell = 0;
            IncHealth = 0;
            IncHealing = 0;
            try
            {
                // 各阑 磷牢 版快.  版氰摹甫 掘绰促.(阁胶磐啊 磷阑 锭)
                if ((RaceServer != Grobal2.RC_USERHUMAN) && (LastHiter != null))
                {
                    // 付瘤阜 锭赴磊啊 荤恩捞绢具 窃.
                    if (ExpHiter != null)
                    {
                        // 版氰摹甫 冈绰 荤恩.. 刚历 锭府扁 矫累茄 荤恩
                        if (ExpHiter.RaceServer == Grobal2.RC_USERHUMAN)
                        {
                            // 弥措 眉仿 父怒, 惑措狼 饭骇俊 厚肥秦辑 版氰摹甫 掘绰促.
                            exp = ExpHiter.CalcGetExp(this.Abil.Level, this.FightExp);
                            if (!M2Share.BoVentureServer)
                            {
                                ExpHiter.GainExp(exp);
                            }
                            else
                            {
                                // 葛氰辑滚俊辑绰 痢荐啊 棵扼埃促.
                            }
                            // 甘涅胶飘啊 乐绰瘤
                            if (PEnvir.HasMapQuest())
                            {
                                if (ExpHiter.GroupOwner != null)
                                {
                                    // 弊缝阑 窍绊 乐栏搁 弊缝盔俊霸 度 鞍捞 利侩等促.
                                    for (i = 0; i < ExpHiter.GroupOwner.GroupMembers.Count; i++)
                                    {
                                        cret = ExpHiter.GroupOwner.GroupMembers[i];
                                        if (!cret.Death && (ExpHiter.PEnvir == cret.PEnvir) && (Math.Abs(ExpHiter.CX - cret.CX) <= 12) && (Math.Abs(ExpHiter.CY - cret.CY) <= 12))
                                        {
                                            if (cret == ExpHiter)
                                            {
                                                bogroupcall = false;
                                            }
                                            else
                                            {
                                                bogroupcall = true;
                                            }
                                            // 磷篮 阁胶磐 捞抚
                                            questnpc = (TMerchant)PEnvir.GetMapQuest(cret, this.UserName, "", bogroupcall);
                                            if (questnpc != null)
                                            {
                                                questnpc.UserCall(cret);
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    // 弊缝阑 救窍绊 乐栏搁 夯牢 父
                                    questnpc = (TMerchant)PEnvir.GetMapQuest(ExpHiter, UserName, "", false);
                                    if (questnpc != null)
                                    {
                                        questnpc.UserCall(ExpHiter);
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (ExpHiter.Master != null)
                            {
                                // 锭赴仇捞 家券各
                                // 何窍档 版氰摹甫 冈澜
                                ExpHiter.GainSlaveExp(this.Abil.Level);
                                // 惑措狼 饭骇父怒 版氰阑 冈澜
                                // 林牢捞 版氰摹甫 冈绰促.
                                exp = ExpHiter.Master.CalcGetExp(this.Abil.Level, this.FightExp);
                                if (!M2Share.BoVentureServer)
                                {
                                    ExpHiter.Master.GainExp(exp);
                                }
                                else
                                {
                                }
                                if (PEnvir.HasMapQuest())
                                {
                                    if (ExpHiter.Master.GroupOwner != null)
                                    {
                                        for (i = 0; i < ExpHiter.Master.GroupOwner.GroupMembers.Count; i++)
                                        {
                                            cret = ExpHiter.Master.GroupOwner.GroupMembers[i];
                                            if (!cret.Death && (ExpHiter.Master.PEnvir == cret.PEnvir) && (Math.Abs(ExpHiter.Master.CX - cret.CX) <= 12) && (Math.Abs(ExpHiter.Master.CY - cret.CY) <= 12))
                                            {
                                                if (cret == ExpHiter.Master)
                                                {
                                                    bogroupcall = false;
                                                }
                                                else
                                                {
                                                    bogroupcall = true;
                                                }
                                                questnpc = (TMerchant)PEnvir.GetMapQuest(cret, this.UserName, "", bogroupcall);
                                                if (questnpc != null)
                                                {
                                                    questnpc.UserCall(cret);
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        questnpc = (TMerchant)PEnvir.GetMapQuest(ExpHiter.Master, UserName, "", false);
                                        if (questnpc != null)
                                        {
                                            questnpc.UserCall(ExpHiter.Master);
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else if (LastHiter.RaceServer == Grobal2.RC_USERHUMAN)
                    {
                        exp = LastHiter.CalcGetExp(this.Abil.Level, this.FightExp);
                        if (!M2Share.BoVentureServer)
                        {
                            LastHiter.GainExp(exp);
                        }
                        else
                        {
                        }
                    }
                }
            }
            catch
            {
                M2Share.MainOutMessage("[Exception] TCreature.Die 1");
            }
            try
            {
                boBadKill = false;
                if ((!M2Share.BoVentureServer) && (!PEnvir.FightZone) && (!PEnvir.Fight2Zone) && (!PEnvir.Fight3Zone) && (!PEnvir.Fight4Zone))
                {
                    if ((RaceServer == Grobal2.RC_USERHUMAN) && (LastHiter != null) && (PKLevel() < 2))
                    {
                        if (LastHiter.RaceServer == Grobal2.RC_USERHUMAN)
                        {
                            boBadKill = true;
                            if (BoTaiwanEventUser)
                            {
                                boBadKill = false;
                            }
                        }
                        if (LastHiter.Master != null)
                        {
                            if (LastHiter.Master.RaceServer == Grobal2.RC_USERHUMAN)
                            {
                                LastHiter = LastHiter.Master;
                                boBadKill = true;
                            }
                        }
                    }
                }
                if (boBadKill && (LastHiter != null))
                {
                    guildwarkill = false;
                    if ((MyGuild != null) && (LastHiter.MyGuild != null))
                    {
                        if (GetGuildRelation(this, LastHiter) == 2)
                        {
                            guildwarkill = true;
                        }
                    }
                    if (M2Share.UserCastle.BoCastleUnderAttack)
                    {
                        if (BoInFreePKArea || M2Share.UserCastle.IsCastleWarArea(PEnvir, CX, CY))
                        {
                            guildwarkill = true;
                        }
                    }
                    if (!guildwarkill)
                    {
                        if (!LastHiter.IsGoodKilling(this))
                        {
                            LastHiter.IncPKPoint(100);
                            LastHiter.SysMsg(UserName + "你犯了谋杀罪", 0);
                            SysMsg("[你现在被" + LastHiter.UserName + "杀害了]", 0);
                            if ((this as TUserHuman).fLover != null)
                            {
                                lovername = (this as TUserHuman).fLover.GetLoverName();
                                if (lovername != "")
                                {
                                    hum = M2Share.UserEngine.GetUserHuman(lovername);
                                    if (hum != null)
                                    {
                                        hum.SysMsg("[你的情侣被" + LastHiter.UserName + "杀害了]", 0);
                                    }
                                    else
                                    {
                                        if (M2Share.UserEngine.FindOtherServerUser(lovername, ref svidx))
                                        {
                                            M2Share.UserEngine.SendInterMsg(Grobal2.ISM_LM_KILLED_MSG, svidx, lovername + "/" + "[你的情侣被" + LastHiter.UserName + "杀害了]");
                                        }
                                    }
                                }
                            }
                            LastHiter.AddBodyLuck(-500);
                            if (PKLevel() < 1)
                            {
                                if (new System.Random(5).Next() == 0)
                                {
                                    if (LastHiter.MakeWeaponUnlock())
                                    {
                                        ps = M2Share.UserEngine.GetStdItem(LastHiter.UseItems[Grobal2.U_WEAPON].Index);
                                        if (ps != null)
                                        {
                                            M2Share.AddUserLog("43\09" + LastHiter.MapName + "\09" + LastHiter.CX.ToString() + "\09" + LastHiter.CY.ToString() + "\09" + LastHiter.UserName + "\09" + ps.Name + "\09" + LastHiter.UseItems[Grobal2.U_WEAPON].MakeIndex.ToString() + "\09" + "1\09" + "-1");
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            LastHiter.SysMsg("[您会受到正当防卫的规则保护]", 1);
                            SysMsg("[你被" + LastHiter.UserName + "杀害了 - 这是正当防卫]", 1);
                        }
                    }
                }
            }
            catch
            {
                M2Share.MainOutMessage("[Exception] TCreature.Die 2");
            }
            try
            {
                if ((!PEnvir.FightZone) && (!PEnvir.Fight3Zone) && !BoAnimal && (!PEnvir.LawFull))
                {
                    if (RaceServer != Grobal2.RC_USERHUMAN)
                    {
                        ehiter = ExpHiter;
                        if (ExpHiter != null)
                        {
                            if (ExpHiter.Master != null)
                            {
                                ehiter = ExpHiter.Master;
                            }
                        }
                    }
                    else
                    {
                        ehiter = LastHiter;
                        if (LastHiter != null)
                        {
                            if (LastHiter.Master != null)
                            {
                                ehiter = LastHiter.Master;
                            }
                        }
                    }
                    if (RaceServer != Grobal2.RC_USERHUMAN)
                    {
                        DropUseItems(ehiter, false);
                        if ((Master == null) && !BoNoItem)
                        {
                            ScatterBagItems(ehiter);
                        }
                        if ((RaceServer >= Grobal2.RC_ANIMAL) && (Master == null) && !BoNoItem)
                        {
                            ScatterGolds(ehiter);
                        }
                    }
                    else
                    {
                        if ((!PEnvir.Fight2Zone) && (!PEnvir.NoDropItem))
                        {
                            if (!(PEnvir.Fight4Zone && (ehiter != null) && (ehiter.RaceServer == Grobal2.RC_USERHUMAN)))
                            {
                                if (ehiter != null)
                                {
                                    if ((ehiter.RaceServer != Grobal2.RC_USERHUMAN) && (!ehiter.BoHasMission))
                                    {
                                        DropUseItems(null, true);
                                    }
                                }
                                else
                                {
                                    DropUseItems(null, false);
                                }
                                FeatureChanged();
                                if ((ehiter != null) && ehiter.BoHasMission)
                                {
                                }
                                else
                                {
                                    ScatterBagItems(null);
                                }
                                AddBodyLuck(-Abil.Level * 5);
                            }
                        }
                    }
                }
                if (PEnvir.Fight3Zone)
                {
                    FightZoneDieCount++;
                    if (MyGuild != null)
                    {
                        MyGuild.TeamFightWhoDead(UserName);
                    }
                    if (LastHiter != null)
                    {
                        if ((LastHiter.MyGuild != null) && (MyGuild != null))
                        {
                            LastHiter.MyGuild.TeamFightWhoWinPoint(LastHiter.UserName, 100);
                            str = LastHiter.MyGuild.GuildName + ":" + LastHiter.MyGuild.MatchPoint.ToString() + "  " + MyGuild.GuildName + ":" + MyGuild.MatchPoint.ToString();
                            M2Share.UserEngine.CryCry(Grobal2.RM_CRY, PEnvir, CX, CY, 10000, "- " + str);
                        }
                    }
                }
                if (RaceServer == Grobal2.RC_CLONE)
                {
                    SendRefMsg(Grobal2.RM_LOOPNORMALEFFECT, (short)this.ActorId, 0, 0, Grobal2.NE_CLONEHIDE, "");
                }
                if (RaceServer == Grobal2.RC_USERHUMAN)
                {
                    if (LastHiter != null)
                    {
                        if (LastHiter.RaceServer == Grobal2.RC_USERHUMAN)
                        {
                            str = LastHiter.UserName;
                        }
                        else if (LastHiter.BoHasMission)
                        {
                            str = "*" + LastHiter.UserName;
                        }
                        else
                        {
                            if (LastHiter.Master != null)
                            {
                                str = LastHiter.Master.UserName;
                            }
                            else
                            {
                                str = "#" + LastHiter.UserName;
                            }
                        }
                    }
                    else
                    {
                        str = "######";
                    }
                    strFZNumber = "";
                    if (PEnvir.FightZone)
                    {
                        strFZNumber = "1";
                    }
                    else if (PEnvir.Fight2Zone)
                    {
                        strFZNumber = "2";
                    }
                    else if (PEnvir.Fight3Zone)
                    {
                        strFZNumber = "3";
                    }
                    else if (PEnvir.Fight4Zone)
                    {
                        strFZNumber = "4";
                    }
                    else
                    {
                        strFZNumber = "F";
                    }
                    if (LastHiter != null)
                    {
                        if (LastHiter.IsGoodKilling(this))
                        {
                            strFZNumber = strFZNumber + "-R";
                        }
                        else if (this.PKLevel() >= 2)
                        {
                            strFZNumber = strFZNumber + "-R";
                        }
                    }
                    M2Share.AddUserLog("19\09" + MapName + "\09" + CX.ToString() + "\09" + CY.ToString() + "\09" + UserName + "\09" + "FZ-" + strFZNumber + "\09" + "0\09" + "1\09" + str);
                }
                else
                {
                    KingMobLogFlag = false;
                    if (Abil.Level >= 60)
                    {
                        KingMobLogFlag = true;
                    }
                    if (KingMobLogFlag)
                    {
                        if (LastHiter != null)
                        {
                            if (LastHiter.RaceServer == Grobal2.RC_USERHUMAN)
                            {
                                str = LastHiter.UserName;
                            }
                            else
                            {
                                str = "#" + LastHiter.UserName;
                            }
                        }
                        else
                        {
                            str = "######";
                        }
                        M2Share.AddUserLog("42\09" + MapName + "\09" + CX.ToString() + "\09" + CY.ToString() + "\09" + UserName + "\09" + "FZ-M" + "\09" + "0\09" + "0\09" + str);
                    }
                }
                SendRefMsg(Grobal2.RM_DEATH, Dir, CX, CY, 1, "");
                Master = null;
            }
            catch
            {
                M2Share.MainOutMessage("[Exception] TCreature.Die 3");
            }
        }

        public void Alive()
        {
            if (Abil.HP == 0)
            {
                Abil.HP = 1;
            }
            Death = false;
            SendRefMsg(Grobal2.RM_LOOPNORMALEFFECT, (short)this.ActorId, 0, 0, Grobal2.NE_RELIVE, "");
            SendRefMsg(Grobal2.RM_ALIVE, Dir, CX, CY, 0, "");
            SendRefMsg(Grobal2.RM_CHANGELIGHT, 0, 0, 0, 0, "");
        }

        public void SetLastHiter(TCreature hiter)
        {
            LastHiter = hiter;
            if (LastHiter != null)
            {
                if (LastHiter.Master != null)
                {
                    LastHiterRace = LastHiter.Master.RaceServer;
                }
                else
                {
                    LastHiterRace = LastHiter.RaceServer;
                }
            }
            LastHitTime = HUtil32.GetTickCount();
            if (ExpHiter == null)
            {
                ExpHiter = hiter;
                ExpHitTime = HUtil32.GetTickCount();
            }
            else if (ExpHiter == hiter)
            {
                ExpHitTime = HUtil32.GetTickCount();
            }
        }

        public void AddPkHiter(TCreature hiter)
        {
            if ((PKLevel() < 2) && (hiter.PKLevel() < 2))
            {
                if ((!PEnvir.FightZone) && (!PEnvir.Fight2Zone) && (!PEnvir.Fight3Zone) && (!PEnvir.Fight4Zone))
                {
                    if (!BoIllegalAttack)
                    {
                        hiter.IllegalAttackTime = HUtil32.GetTickCount();
                        if (!hiter.BoIllegalAttack)
                        {
                            hiter.BoIllegalAttack = true;
                            hiter.ChangeNameColor();
                        }
                    }
                }
            }
        }

        public void CheckTimeOutPkHiterList()
        {
            long DuringIllegalTime = 60 * 1000;
            if (BoIllegalAttack)
            {
                if (HUtil32.GetTickCount() - IllegalAttackTime > DuringIllegalTime)
                {
                    BoIllegalAttack = false;
                    ChangeNameColor();
                }
            }
        }

        public void ClearPkHiterList()
        {
            for (var i = 0; i < PKHiterList.Count; i++)
            {
                Dispose(PKHiterList[i]);
            }
            PKHiterList.Clear();
        }

        public bool IsGoodKilling(TCreature target)
        {
            bool result = false;
            if (target.BoIllegalAttack)
            {
                result = true;
            }
            return result;
        }

        public void SetAllowLongHit(bool boallow)
        {
            BoAllowLongHit = boallow;
            if (BoAllowLongHit)
            {
                SysMsg("使用刺杀剑术", 1);
            }
            else
            {
                SysMsg("不使用刺杀剑术", 1);
            }
        }

        public void SetAllowWideHit(bool boallow)
        {
            BoAllowWideHit = boallow;
            if (BoAllowWideHit)
            {
                SysMsg("使用半月剑法。", 1);
            }
            else
            {
                SysMsg("不使用半月剑法。", 1);
            }
        }

        public void SetAllowCrossHit(bool boallow)
        {
            BoAllowCrossHit = boallow;
            if (BoAllowCrossHit)
            {
                SysMsg("使用狂风斩。", 1);
            }
            else
            {
                SysMsg("不使用狂风斩。", 1);
            }
        }

        public bool SetAllowFireHit()
        {
            bool result = false;
            if (HUtil32.GetTickCount() - LatestFireHitTime > 10 * 1000)
            {
                LatestFireHitTime = HUtil32.GetTickCount();
                BoAllowFireHit = true;
                SysMsg("您的武器因精神火焰而炙热。", 1);
                result = true;
            }
            else
            {
                SysMsg("凝聚内力失败。", 0);
            }
            return result;
        }

        public bool SetAllowTwinHit()
        {
            bool result = false;
            if (HUtil32.GetTickCount() - LatestTwinHitTime > 1000)
            {
                LatestTwinHitTime = HUtil32.GetTickCount();
                BoAllowTwinHit = 1;
                result = true;
            }
            return result;
        }

        public void IncHealthSpell(int hp, int mp)
        {
            if ((hp >= 0) && (mp >= 0))
            {
                if (WAbil.HP + hp < WAbil.MaxHP)
                {
                    WAbil.HP = (short)(WAbil.HP + hp);
                }
                else
                {
                    WAbil.HP = WAbil.MaxHP;
                }
                if (WAbil.MP + mp < WAbil.MaxMP)
                {
                    WAbil.MP = (short)(WAbil.MP + mp);
                }
                else
                {
                    WAbil.MP = WAbil.MaxMP;
                }
                HealthSpellChanged();
            }
        }

        public void RandomSpaceMove(string mname, int mtype)
        {
            short nx = 0;
            short ny = 0;
            int egdey;
            TEnvirnoment oldenvir = PEnvir;
            TEnvirnoment nenvir = M2Share.GrobalEnvir.GetEnvir(mname);
            if (nenvir != null)
            {
                if (nenvir.MapHeight < 150)
                {
                    if (nenvir.MapHeight < 30)
                    {
                        egdey = 2;
                    }
                    else
                    {
                        egdey = 20;
                    }
                }
                else
                {
                    egdey = 50;
                }
                nx = (short)(egdey + new System.Random(nenvir.MapWidth - egdey - 1).Next());
                ny = (short)(egdey + new System.Random(nenvir.MapHeight - egdey - 1).Next());
                SpaceMove(mname, nx, ny, mtype);
            }
        }

        public void RandomSpaceMoveInRange(int mtype, int InRange, int OutRange)
        {
            int ran;
            short signX;
            short signY;
            short nx = 0;
            short ny = 0;
            signX = 1;
            signY = 1;
            if (PEnvir != null)
            {
                ran = new System.Random(100).Next();
                ran = ran % 4;
                switch (ran)
                {
                    case 0:
                        signX = 1;
                        signY = 1;
                        break;
                    case 1:
                        signX = -1;
                        signY = 1;
                        break;
                    case 2:
                        signX = -1;
                        signY = -1;
                        break;
                    case 3:
                        signX = 1;
                        signY = -1;
                        break;
                }
                nx = (short)(CX + signX * (InRange + new System.Random(OutRange - InRange).Next() + 1));
                if (nx >= PEnvir.MapWidth)
                {
                    nx = (short)(PEnvir.MapWidth - 1);
                }
                else if (nx < 0)
                {
                    nx = 0;
                }
                ny = (short)(CY + signY * (InRange + new System.Random(OutRange - InRange).Next() + 1));
                if (ny >= PEnvir.MapHeight)
                {
                    ny = (short)(PEnvir.MapHeight - 1);
                }
                else if (ny < 0)
                {
                    ny = 0;
                }
                SpaceMove(PEnvir.MapName, nx, ny, mtype);
            }
        }

        public bool SpaceMove_RandomEnvXY(TEnvirnoment env, ref short nnx, ref short nny)
        {
            short step;
            int edge;
            bool result = false;
            if (env.MapWidth < 80)
            {
                step = 3;
            }
            else
            {
                step = 10;
            }
            if (env.MapHeight < 150)
            {
                if (env.MapHeight < 50)
                {
                    edge = 2;
                }
                else
                {
                    edge = 15;
                }
            }
            else
            {
                edge = 50;
            }
            for (var i = 0; i <= 200; i++)
            {
                if (env.CanWalk(nnx, nny, true))
                {
                    result = true;
                    break;
                }
                else
                {
                    if (nnx < env.MapWidth - edge - 1)
                    {
                        nnx += step;
                    }
                    else
                    {
                        nnx = (short)new System.Random(env.MapWidth).Next();
                        if (nny < env.MapHeight - edge - 1)
                        {
                            nny += step;
                        }
                        else
                        {
                            nny = (short)new System.Random(env.MapHeight).Next();
                        }
                    }
                }
            }
            return result;
        }

        public void SpaceMove(string mname, short nx, short ny, int mtype)
        {
            int i;
            short oldx;
            short oldy;
            TEnvirnoment nenvir;
            TEnvirnoment oldenvir;
            bool success;
            if (RaceServer == Grobal2.RC_USERHUMAN)
            {
                if ((this as TUserHuman).StallMgr.OnSale)
                {
                    SysMsg("[摆摊状态] 不能瞬间移动功能！", 0);
                    return;
                }
            }
            nenvir = M2Share.GrobalEnvir.GetEnvir(mname);
            if (nenvir != null)
            {
                if (M2Share.ServerIndex == nenvir.Server)
                {
                    oldenvir = PEnvir;
                    oldx = CX;
                    oldy = CY;
                    success = false;
                    Disappear(6);
                    MsgTargetList.Clear();
                    for (i = 0; i < VisibleItems.Count; i++)
                    {
                        Dispose(VisibleItems[i]);
                    }
                    VisibleItems.Clear();
                    i = 0;
                    while (true)
                    {
                        if (i >= VisibleActors.Count)
                        {
                            break;
                        }
                        Dispose(VisibleActors[i]);
                        VisibleActors.RemoveAt(i);
                    }
                    VisibleActors.Clear();
                    PEnvir = nenvir;
                    MapName = nenvir.MapName;
                    CX = nx;// 2 + Random(PEnvir.MapWidth-4);
                    CY = ny;// 2 + Random(PEnvir.MapHeight-4);
                    if (SpaceMove_RandomEnvXY(PEnvir, ref CX, ref CY))
                    {
                        if (null != PEnvir.AddToMap(CX, CY, Grobal2.OS_MOVINGOBJECT, this))
                        {
                            SendMsg(this, Grobal2.RM_CLEAROBJECTS, 0, 0, 0, 0, "");
                            SendMsg(this, Grobal2.RM_CHANGEMAP, 0, 0, 0, 0, nenvir.GetGuildAgitRealMapName());
                            switch (mtype)
                            {
                                case 0:
                                    SendRefMsg(Grobal2.RM_SPACEMOVE_SHOW, Dir, CX, CY, 0, "");
                                    break;
                                case 1:
                                    SendRefMsg(Grobal2.RM_SPACEMOVE_SHOW2, Dir, CX, CY, 0, "");
                                    break;
                                default:
                                    SendRefMsg(Grobal2.RM_SPACEMOVE_SHOW_NO, Dir, CX, CY, 0, "");
                                    break;
                            }
                            MapMoveTime = HUtil32.GetTickCount();
                            SpaceMoved = true;
                            success = true;
                        }
                    }
                    if (!success)
                    {
                        PEnvir = oldenvir;
                        CX = oldx;
                        CY = oldy;
                        if (null == PEnvir.AddToMap(CX, CY, Grobal2.OS_MOVINGOBJECT, this))
                        {
                            M2Share.MainOutMessage("Error DeleteFromMap(2):" + PEnvir.MapName + "," + CX.ToString() + "," + CY.ToString());
                        }
                    }
                }
                else
                {
                    if (SpaceMove_RandomEnvXY(nenvir, ref nx, ref ny))
                    {
                        if (RaceServer == Grobal2.RC_USERHUMAN)
                        {
                            if (Disappear(4) == true)
                            {
                                SpaceMoved = true;
                                TUserHuman hum = this as TUserHuman;
                                hum.ChangeMapName = nenvir.MapName;
                                hum.ChangeCX = nx;
                                hum.ChangeCY = ny;
                                hum.BoChangeServer = true;
                                hum.ChangeToServerNumber = nenvir.Server;
                                hum.EmergencyClose = true;
                                hum.SoftClosed = true;
                                hum.FAlreadyDisapper = true;
                            }
                        }
                        else
                        {
                            KickException();
                        }
                    }
                }
            }
        }

        public void UserSpaceMove(string mname, string xstr, string ystr)
        {
            short xx;
            short yy;
            TEnvirnoment oldenvir;
            TUserHuman hum;
            oldenvir = PEnvir;
            if (mname == "")
            {
                mname = MapName;
            }
            if ((xstr != "") && (ystr != ""))
            {
                xx = (short)HUtil32.Str_ToInt(xstr, 0);
                yy = (short)HUtil32.Str_ToInt(ystr, 0);
                SpaceMove(mname, xx, yy, 0);
            }
            else
            {
                RandomSpaceMove(mname, 0);
            }
            if (oldenvir != PEnvir)
            {
                if (RaceServer == Grobal2.RC_USERHUMAN)
                {
                    hum = this as TUserHuman;
                    hum.BoTimeRecall = false;
                    hum.BoTimeRecallGroup = false;
                }
            }
        }

        public bool UseScroll(int Shape)
        {
            bool result;
            TUserHuman hum;
            TGuildAgit guildagit;
            bool AgitFlag;
            result = false;
            switch (Shape)
            {
                case 1:
                    if (!BoTaiwanEventUser)
                    {
                        if (!(PEnvir.NoEscapeMove || PEnvir.NoTeleportMove))
                        {
                            SendRefMsg(Grobal2.RM_SPACEMOVE_HIDE, 0, 0, 0, 0, "");
                            UserSpaceMove(HomeMap, "", "");
                            result = true;
                        }
                    }
                    else
                    {
                        SysMsg("无法使用。", 0);
                    }
                    break;
                case 2:
                    if (!BoTaiwanEventUser)
                    {
                        if (!(PEnvir.NoRandomMove || PEnvir.NoTeleportMove))
                        {
                            if (M2Share.UserCastle.BoCastleUnderAttack && (PEnvir == M2Share.UserCastle.CorePEnvir))
                            {
                                if (HUtil32.GetTickCount() - LatestSpaceScrollTime > 10 * 1000)
                                {
                                    LatestSpaceScrollTime = HUtil32.GetTickCount();
                                    SendRefMsg(Grobal2.RM_SPACEMOVE_HIDE, 0, 0, 0, 0, "");
                                    UserSpaceMove(MapName, "", "");
                                    result = true;
                                }
                                else
                                {
                                    SysMsg((10 - (HUtil32.GetTickCount() - LatestSpaceScrollTime) / 1000).ToString() + "秒后才可以使用。", 0);
                                    result = false;
                                }
                            }
                            else
                            {
                                SendRefMsg(Grobal2.RM_SPACEMOVE_HIDE, 0, 0, 0, 0, "");
                                UserSpaceMove(MapName, "", "");
                                result = true;
                            }
                        }
                    }
                    else
                    {
                        SysMsg("无法使用。", 0);
                    }
                    break;
                case 3:
                    if (!BoTaiwanEventUser)
                    {
                        SendRefMsg(Grobal2.RM_SPACEMOVE_HIDE, 0, 0, 0, 0, "");
                        if (PKLevel() < 2)
                        {
                            UserSpaceMove(HomeMap, HomeX.ToString(), HomeY.ToString());
                        }
                        else
                        {
                            UserSpaceMove(M2Share.BADMANHOMEMAP, M2Share.BADMANSTARTX.ToString(), M2Share.BADMANSTARTY.ToString());
                        }
                        result = true;
                    }
                    else
                    {
                        SysMsg("你不能使用它。", 0);
                    }
                    break;
                case 4:
                    if (MakeWeaponGoodLock())
                    {
                        result = true;
                    }
                    break;
                case 5:
                    if (!BoTaiwanEventUser)
                    {
                        if (MyGuild != null)
                        {
                            if (!BoInFreePKArea)
                            {
                                if (M2Share.UserCastle.IsOurCastle(MyGuild))
                                {
                                    UserSpaceMove(M2Share.UserCastle.CastleMap, M2Share.UserCastle.GetCastleStartX().ToString(), M2Share.UserCastle.GetCastleStartY().ToString());
                                }
                                else
                                {
                                    SysMsg("它没有效果。", 0);
                                }
                                result = true;
                            }
                            else
                            {
                                SysMsg("你不能在这里使用它。", 0);
                            }
                        }
                    }
                    else
                    {
                        SysMsg("你不能使用它。", 0);
                    }
                    break;
                case 6:
                    AgitFlag = false;
                    if (!BoTaiwanEventUser)
                    {
                        if (RaceServer == Grobal2.RC_USERHUMAN)
                        {
                            hum = this as TUserHuman;
                            if (hum.MyGuild != null)
                            {
                                guildagit = M2Share.GuildAgitMan.GetGuildAgit(hum.MyGuild.GuildName);
                                if (guildagit != null)
                                {
                                    if (guildagit.GuildAgitNumber > -1)
                                    {
                                        hum.CmdGuildAgitFreeMove(guildagit.GuildAgitNumber);
                                        AgitFlag = true;
                                        result = true;
                                    }
                                }
                            }
                            if (AgitFlag == false)
                            {
                                SendRefMsg(Grobal2.RM_SPACEMOVE_HIDE, 0, 0, 0, 0, "");
                                if (PKLevel() < 2)
                                {
                                    UserSpaceMove(HomeMap, HomeX.ToString(), HomeY.ToString());
                                }
                                else
                                {
                                    UserSpaceMove(M2Share.BADMANHOMEMAP, M2Share.BADMANSTARTX.ToString(), M2Share.BADMANSTARTY.ToString());
                                }
                                result = true;
                            }
                        }
                    }
                    else
                    {
                        SysMsg("你不能使用它。", 0);
                    }
                    break;
                case 9:
                    if (RepaireWeaponNormaly())
                    {
                        result = true;
                    }
                    break;
                case 10:
                    if (RepaireWeaponPerfect())
                    {
                        result = true;
                    }
                    break;
                case 11:
                    if (UseLotto())
                    {
                        result = true;
                    }
                    break;
            }
            return result;
        }

        public bool MakeWeaponGoodLock()
        {
            bool result;
            int difficulty;
            bool flag;
            TStdItem pstd;
            int Delta;
            int Old;
            TUserHuman hum;
            Delta = 0;
            if (UseItems[Grobal2.U_WEAPON].Desc[4] > 0)
            {
                Old = -UseItems[Grobal2.U_WEAPON].Desc[4];
            }
            else
            {
                Old = UseItems[Grobal2.U_WEAPON].Desc[3];
            }
            result = false;
            if (UseItems[Grobal2.U_WEAPON].Index != 0)
            {
                difficulty = 0;
                pstd = M2Share.UserEngine.GetStdItem(UseItems[Grobal2.U_WEAPON].Index);
                if (pstd != null)
                {
                    difficulty = Math.Abs(HiByte(pstd.DC) - LoByte(pstd.DC)) / 5;
                }
                else
                {
                    return result;
                }
                if (new System.Random(20).Next() == 1)
                {
                    if (MakeWeaponUnlock())
                    {
                        Delta = -1;
                    }
                }
                else
                {
                    flag = false;
                    if (UseItems[Grobal2.U_WEAPON].Desc[4] > 0)
                    {
                        UseItems[Grobal2.U_WEAPON].Desc[4] = (byte)(UseItems[Grobal2.U_WEAPON].Desc[4] - 1);
                        SysMsg("你的武器得到了祝福。", 1);
                        Delta = 1;
                        flag = true;
                    }
                    else
                    {
                        if (UseItems[Grobal2.U_WEAPON].Desc[3] < 1)
                        {
                            UseItems[Grobal2.U_WEAPON].Desc[3] = (byte)(UseItems[Grobal2.U_WEAPON].Desc[3] + 1);
                            SysMsg("你的武器得到了祝福。", 1);
                            Delta = 1;
                            flag = true;
                        }
                        else
                        {
                            if ((UseItems[Grobal2.U_WEAPON].Desc[3] < 3) && (new System.Random(6 + difficulty).Next() == 1))
                            {
                                UseItems[Grobal2.U_WEAPON].Desc[3] = (byte)(UseItems[Grobal2.U_WEAPON].Desc[3] + 1);
                                SysMsg("你的武器得到了祝福。", 1);
                                Delta = 1;
                                flag = true;
                            }
                            else
                            {
                                if ((UseItems[Grobal2.U_WEAPON].Desc[3] < 7) && (new System.Random(30 + difficulty * 5).Next() == 1))
                                {
                                    UseItems[Grobal2.U_WEAPON].Desc[3] = (byte)(UseItems[Grobal2.U_WEAPON].Desc[3] + 1);
                                    SysMsg("你的武器得到了祝福。", 1);
                                    Delta = 1;
                                    flag = true;
                                }
                            }
                        }
                    }
                    if (RaceServer == Grobal2.RC_USERHUMAN)
                    {
                        RecalcAbilitys();
                        hum = this as TUserHuman;
                        hum.SendUpdateItem(UseItems[Grobal2.U_WEAPON]);
                        SendMsg(this, Grobal2.RM_ABILITY, 0, 0, 0, 0, "");
                        SendMsg(this, Grobal2.RM_SUBABILITY, 0, 0, 0, 0, "");
                    }
                    if (!flag)
                    {
                        SysMsg("它没有效果。", 1);
                    }
                }
                M2Share.AddUserLog("29\09" + MapName + "\09" + CX.ToString() + "\09" + CY.ToString() + "\09" + UserName + "\09" + pstd.Name + "\09" + UseItems[Grobal2.U_WEAPON].MakeIndex.ToString() + "\09" + "0" + "\09" + Delta.ToString() + "(" + Old.ToString() + "->" + (Old + Delta).ToString() + ")");
                result = true;
            }
            return result;
        }

        public bool RepaireWeaponNormaly()
        {
            int repair;
            bool result = false;
            if (UseItems[Grobal2.U_WEAPON].Index > 0)
            {
                TStdItem ps = M2Share.UserEngine.GetStdItem(UseItems[Grobal2.U_WEAPON].Index);
                string WeaponName = string.Empty;
                if (ps != null)
                {
                    if (ps.UniqueItem == 3)
                    {
                        SysMsg("这种武器无法修理。", 0);
                        result = false;
                        return result;
                    }
                    WeaponName = ps.Name;
                }
                repair = _MIN(5000, _MAX(0, UseItems[Grobal2.U_WEAPON].DuraMax - UseItems[Grobal2.U_WEAPON].Dura));
                if (repair > 0)
                {
                    UseItems[Grobal2.U_WEAPON].DuraMax = (short)_MAX(0, UseItems[Grobal2.U_WEAPON].DuraMax - (repair / 30));
                    UseItems[Grobal2.U_WEAPON].Dura = (short)_MIN(UseItems[Grobal2.U_WEAPON].Dura + repair, UseItems[Grobal2.U_WEAPON].DuraMax);
                    SendMsg(this, Grobal2.RM_DURACHANGE, Grobal2.U_WEAPON, UseItems[Grobal2.U_WEAPON].Dura, UseItems[Grobal2.U_WEAPON].DuraMax, 0, "");
                    RecalcAbilitys();
                    SendMsg(this, Grobal2.RM_ABILITY, 0, 0, 0, 0, "");
                    SendMsg(this, Grobal2.RM_SUBABILITY, 0, 0, 0, 0, "");
                    SysMsg("\'" + WeaponName + "\'" + "修理完成。", 1);
                    result = true;
                }
                else
                {
                    SysMsg("你不需要修理这个物品。", 0);
                }
            }
            return result;
        }

        public bool RepaireWeaponPerfect()
        {
            bool result = false;
            if (UseItems[Grobal2.U_WEAPON].Index > 0)
            {
                TStdItem ps = M2Share.UserEngine.GetStdItem(UseItems[Grobal2.U_WEAPON].Index);
                if (ps != null)
                {
                    if (ps.UniqueItem == 3)
                    {
                        SysMsg("这种武器无法修理。", 0);
                        result = false;
                        return result;
                    }
                }
                UseItems[Grobal2.U_WEAPON].Dura = UseItems[Grobal2.U_WEAPON].DuraMax;
                SendMsg(this, Grobal2.RM_DURACHANGE, Grobal2.U_WEAPON, UseItems[Grobal2.U_WEAPON].Dura, UseItems[Grobal2.U_WEAPON].DuraMax, 0, "");
                RecalcAbilitys();
                SendMsg(this, Grobal2.RM_ABILITY, 0, 0, 0, 0, "");
                SendMsg(this, Grobal2.RM_SUBABILITY, 0, 0, 0, 0, "");
                SysMsg("武器修理完成。", 1);
                result = true;
            }
            return result;
        }

        public bool RepairItemNormaly(TStdItem psSeed, TUserItem puSeed)
        {
            bool result = false;
            if (psSeed != null)
            {
                if (psSeed.UniqueItem == 3)
                {
                    SysMsg("这种武器无法修理。", 0);
                    result = false;
                    return result;
                }
                int repair = _MIN(5000, _MAX(0, puSeed.DuraMax - puSeed.Dura));
                if (repair > 0)
                {
                    puSeed.DuraMax = (short)_MAX(0, puSeed.DuraMax - (repair / 30));
                    puSeed.Dura = (short)_MIN(puSeed.Dura + repair, puSeed.DuraMax);
                    if (RaceServer == Grobal2.RC_USERHUMAN)
                    {
                        TUserHuman hum = this as TUserHuman;
                        hum.SendUpdateItem(puSeed);
                    }
                    SysMsg("\'" + psSeed.Name + "\'" + "修理完成。", 1);
                    result = true;
                }
                else
                {
                    SysMsg("你不需要修理这个物品。", 0);
                }
            }
            return result;
        }

        public bool UseLotto()
        {
            bool result;
            int ngold;
            int grade;
            ngold = 0;
            grade = 0;
            switch (new System.Random(30000).Next())
            {
                // 500*25000 = 12,500,000 (11,500,000)
                // Modify the A .. B: 0 .. 4999
                case 0:
                    // 500 * 5000 = 2,500,000
                    if (M2Share.LottoSuccess < M2Share.LottoFail)
                    {
                        ngold = 500;
                        grade = 6;
                        M2Share.Lotto6++;
                    }
                    break;
                // Modify the A .. B: 14000 .. 15999
                case 14000:
                    // 1000 * 2000 = 2,000,000
                    if (M2Share.LottoSuccess < M2Share.LottoFail)
                    {
                        ngold = 1000;
                        grade = 5;
                        M2Share.Lotto5++;
                    }
                    break;
                // Modify the A .. B: 16000 .. 16149
                case 16000:
                    // 10000 * 200 = 2,000,000
                    if (M2Share.LottoSuccess < M2Share.LottoFail)
                    {
                        ngold = 10000;
                        grade = 4;
                        M2Share.Lotto4++;
                    }
                    break;
                // Modify the A .. B: 16150 .. 16169
                case 16150:
                    // 100000 * 20 = 2,000,000
                    if (M2Share.LottoSuccess < M2Share.LottoFail)
                    {
                        ngold = 100000;
                        grade = 3;
                        M2Share.Lotto3++;
                    }
                    break;
                // Modify the A .. B: 16170 .. 16179
                case 16170:
                    // 200000 * 10 = 2,000,000
                    if (M2Share.LottoSuccess < M2Share.LottoFail)
                    {
                        ngold = 200000;
                        grade = 2;
                        M2Share.Lotto2++;
                    }
                    break;
                case 18000:
                    // 1000000 (1殿)
                    if (M2Share.LottoSuccess < M2Share.LottoFail)
                    {
                        ngold = 1000000;
                        grade = 1;
                        M2Share.Lotto1++;
                    }
                    break;
            }
            if (ngold > 0)
            {
                M2Share.LottoSuccess = M2Share.LottoSuccess + ngold;
                switch (grade)
                {
                    case 1:
                        SysMsg("祝贺，你能赢得1等奖。", 1);
                        break;
                    case 2:
                        SysMsg("祝贺，你能赢得2等奖。", 1);
                        break;
                    case 3:
                        SysMsg("祝贺，你能赢得3等奖。", 1);
                        break;
                    case 4:
                        SysMsg("祝贺，你能赢得4等奖。", 1);
                        break;
                    case 5:
                        SysMsg("祝贺，你能赢得5等奖。", 1);
                        break;
                    case 6:
                        SysMsg("祝贺，你能赢得6等奖。", 1);
                        break;
                    case 7:
                        SysMsg("祝贺，你能赢得7等奖。", 1);
                        break;
                    case 8:
                        SysMsg("祝贺，你能赢得8等奖。", 1);
                        break;
                }
                if (IncGold(ngold))
                {
                    GoldChanged();
                }
                else
                {
                    DropGoldDown(ngold, true, null, null);
                }
            }
            else
            {
                M2Share.LottoFail = M2Share.LottoFail + 500;
                SysMsg("没有中奖...", 0);
            }
            result = true;
            return result;
        }

        public void MakeHolySeize(int htime)
        {
            BoHolySeize = true;
            HolySeizeStart = HUtil32.GetTickCount();
            HolySeizeTime = htime;
            ChangeNameColor();
        }

        public void BreakHolySeize()
        {
            BoHolySeize = false;
            ChangeNameColor();
        }

        public void MakeCrazyMode(int csec)
        {
            BoCrazyMode = true;
            CrazyModeStart = HUtil32.GetTickCount();
            CrazyModeTime = csec * 1000;
            ChangeNameColor();
        }

        public void MakeGoodCrazyMode(int csec)
        {
            BoGoodCrazyMode = true;
            CrazyModeStart = HUtil32.GetTickCount();
            CrazyModeTime = csec * 1000;
            ChangeNameColor();
        }

        public void BreakCrazyMode()
        {
            if (BoCrazyMode || BoGoodCrazyMode)
            {
                BoCrazyMode = false;
                BoGoodCrazyMode = false;
                ChangeNameColor();
            }
        }

        public void MakeOpenHealth()
        {
            BoOpenHealth = true;
            CharStatusEx = CharStatusEx | Grobal2.STATE_OPENHEATH;
            CharStatus = GetCharStatus();
            // lparam1
            // lparam2
            SendRefMsg(Grobal2.RM_OPENHEALTH, 0, WAbil.HP, WAbil.MaxHP, 0, "");
        }

        public void BreakOpenHealth()
        {
            if (BoOpenHealth)
            {
                BoOpenHealth = false;
                CharStatusEx = CharStatusEx ^ Grobal2.STATE_OPENHEATH;
                CharStatus = GetCharStatus();
                SendRefMsg(Grobal2.RM_CLOSEHEALTH, 0, 0, 0, 0, "");
            }
        }

        public int GetHitStruckDamage(TCreature hiter, int damage)
        {
            int armor = LoByte(WAbil.AC) + new System.Random(HiByte(WAbil.AC) - LoByte(WAbil.AC) + 1).Next();
            damage = _MAX(0, damage - armor);
            if ((LifeAttrib == Grobal2.LA_UNDEAD) && (hiter != null))
            {
                damage = damage + hiter.AddAbil.UndeadPower;
            }
            if (damage > 0)
            {
                if (BoAbilMagBubbleDefence)
                {
                    damage = HUtil32.MathRound(damage / 100 * (MagBubbleDefenceLevel + 2) * 8);
                    DamageBubbleDefence();
                }
            }
            return damage;
        }

        public int GetMagStruckDamage(TCreature hiter, int damage)
        {
            int result;
            // 郴 付亲仿阑 皑救窍咯 单固瘤 拌魂
            int armor;
            // armor := LoByte(WAbil.MAC) + Random(ShortInt(HiByte(WAbil.MAC)-LoByte(WAbil.MAC)) + 1);
            armor = LoByte(WAbil.MAC) + new System.Random(HiByte(WAbil.MAC) - LoByte(WAbil.MAC) + 1).Next();
            damage = _MAX(0, damage - armor);
            if ((LifeAttrib == Grobal2.LA_UNDEAD) && (hiter != null))
            {
                damage = damage + hiter.AddAbil.UndeadPower;
            }
            if (damage > 0)
            {
                if (BoAbilMagBubbleDefence)
                {
                    damage = HUtil32.MathRound(damage / 100 * (MagBubbleDefenceLevel + 2) * 8);
                    DamageBubbleDefence();
                }
            }
            result = damage;
            return result;
        }

        // 郴 付亲仿阑 皑救窍咯 单固瘤 拌魂
        public void StruckDamage(int damage, TCreature hiter = null)
        {
            int i;
            int wdam;
            int adura;
            int old;
            bool bocalc;
            TStdItem ps;
            int realdam;
            if (damage > 0)
            {
                if (hiter != null)
                {
                    SetLastHiter(hiter);
                }
                wdam = new System.Random(10).Next() + 5;
                if (StatusArr[Grobal2.POISON_DAMAGEARMOR] > 0)
                {
                    wdam = HUtil32.MathRound(wdam * ((10 + RedPoisonLevel) / 10));
                    damage = HUtil32.MathRound(damage * ((10 + RedPoisonLevel) / 10));
                }
                if (StatusArr[Grobal2.POISON_STUN] > 0)
                {
                    damage = HUtil32.MathRound(damage * 1.2M);
                }
                bocalc = false;
                if ((UseItems[Grobal2.U_DRESS].Index > 0) && (UseItems[Grobal2.U_DRESS].Dura > 0))
                {
                    adura = UseItems[Grobal2.U_DRESS].Dura;
                    old = HUtil32.MathRound(adura / 1000);
                    adura = adura - wdam;
                    if (adura <= 0)
                    {
                        SysMsg(M2Share.UserEngine.GetStdItemName(UseItems[Grobal2.U_DRESS].Index) + "道具因持久值耗尽而消失。", 3);
                        UseItems[Grobal2.U_DRESS].Dura = 0;
                        SendMsg(this, Grobal2.RM_DURACHANGE, Grobal2.U_DRESS, UseItems[Grobal2.U_DRESS].Dura, UseItems[Grobal2.U_DRESS].DuraMax, 0, "");
                        bocalc = true;
                    }
                    else
                    {
                        UseItems[Grobal2.U_DRESS].Dura = (short)adura;
                    }
                    if (old != HUtil32.MathRound(adura / 1000))
                    {
                        SendMsg(this, Grobal2.RM_DURACHANGE, Grobal2.U_DRESS, adura, UseItems[Grobal2.U_DRESS].DuraMax, 0, "");
                    }
                }
                for (i = 1; i <= 11; i++)
                {
                    if ((UseItems[i].Index > 0) && (UseItems[i].Dura > 0) && (new System.Random(8).Next() == 0) && (i != Grobal2.U_BUJUK))
                    {
                        if (i == Grobal2.U_ARMRINGL)
                        {
                            ps = M2Share.UserEngine.GetStdItem(UseItems[i].Index);
                            if (ps != null)
                            {
                                if (ps.StdMode == 25)
                                {
                                    continue;
                                }
                            }
                        }
                        adura = UseItems[i].Dura;
                        old = HUtil32.MathRound(adura / 1000);
                        adura = adura - wdam;
                        if (adura <= 0)
                        {
                            SysMsg(M2Share.UserEngine.GetStdItemName(UseItems[i].Index) + "道具因持久值耗尽而消失。", 3);
                            UseItems[i].Dura = 0;
                            SendMsg(this, Grobal2.RM_DURACHANGE, (short)i, UseItems[i].Dura, UseItems[i].DuraMax, 0, "");
                            bocalc = true;
                        }
                        else
                        {
                            UseItems[i].Dura = (short)adura;
                        }
                        if (old != HUtil32.MathRound(adura / 1000))
                        {
                            SendMsg(this, Grobal2.RM_DURACHANGE, (short)i, adura, UseItems[i].DuraMax, 0, "");
                        }
                    }
                }
                if (bocalc)
                {
                    RecalcAbilitys();
                    SendMsg(this, Grobal2.RM_ABILITY, 0, 0, 0, 0, "");
                    SendMsg(this, Grobal2.RM_SUBABILITY, 0, 0, 0, 0, "");
                }
                try
                {
                    if (RaceServer == Grobal2.RC_CLONE)
                    {
                        if (!Death && !BoGhost && (Master != null) && (Master.RaceServer == Grobal2.RC_USERHUMAN) && (!Master.BoGhost) && (!Master.Death) && (Master.WAbil.MP > 0))
                        {
                            if (Master.WAbil.MP >= (damage / 5))
                            {
                                Master.WAbil.MP = (short)(Master.WAbil.MP - (damage / 5));
                            }
                            else
                            {
                                Master.WAbil.MP = 0;
                            }
                            Master.HealthSpellChanged();
                        }
                    }
                    // 牢埃捞 酒匆锭 胶沛捞 登绊 嘎栏搁 钱赴促.
                    if ((RaceServer != Grobal2.RC_USERHUMAN) && (StatusArr[Grobal2.POISON_DONTMOVE] > 1))
                    {
                        StatusArr[Grobal2.POISON_DONTMOVE] = 1;
                    }
                }
                catch
                {
                    M2Share.MainOutMessage("EXCEPTION CLON HP CACULATE");
                }
                realdam = DamageHealth(damage, 0);
            }
        }

        public int DamageHealth(int damage, int minimum)
        {
            int result;
            int spdam;
            result = 0;
            if (BoMagicShield && (damage > 0) && (WAbil.MP > 0))
            {
                spdam = HUtil32.MathRound(damage * 1.5);
                if (WAbil.MP >= spdam)
                {
                    WAbil.MP = (short)(WAbil.MP - spdam);
                    spdam = 0;
                }
                else
                {
                    spdam = spdam - WAbil.MP;
                    WAbil.MP = 0;
                }
                damage = HUtil32.MathRound(spdam / 1.5);
                HealthSpellChanged();
            }
            if (damage > 0)
            {
                if (WAbil.HP - damage > 0)
                {
                    result = damage;
                    WAbil.HP = (short)(WAbil.HP - damage);
                }
                else
                {
                    result = WAbil.HP - minimum;
                    WAbil.HP = (short)_MAX(minimum, 0);
                    // 2004/07/14 sonmg
                }
            }
            else
            {
                if (WAbil.HP - damage < WAbil.MaxHP)
                {
                    result = damage;
                    WAbil.HP = (short)(WAbil.HP - damage);
                }
                else
                {
                    result = WAbil.HP - WAbil.MaxHP;
                    WAbil.HP = WAbil.MaxHP;
                }
            }
            return result;
        }

        // val : (+) dec spell
        // (-) inc spell
        public void DamageSpell(int val)
        {
            if (val > 0)
            {
                if (WAbil.MP - val > 0)
                {
                    WAbil.MP = (short)(WAbil.MP - val);
                }
                else
                {
                    WAbil.MP = 0;
                }
            }
            else
            {
                if (WAbil.MP - val < WAbil.MaxMP)
                {
                    WAbil.MP = (short)(WAbil.MP - val);
                }
                else
                {
                    WAbil.MP = WAbil.MaxMP;
                }
            }
        }

        // 郴 扁霖栏肺 棱篮 各狼 版氰摹甫 拌魂
        public int CalcGetExp(int targlevel, int targhp)
        {
            int result;
            if (Abil.Level < (targlevel + 10))
            {
                result = targhp;
            }
            else
            {
                result = targhp - HUtil32.MathRound(targhp / 15 * (Abil.Level - (targlevel + 10)));
            }
            if (result <= 0)
            {
                result = 1;
            }
            return result;
        }

        // 
        // <弊缝盔 荐俊 蝶弗 版氰摹 刘啊摹>
        // 疙荐   : 2    3    4    5    6    7    8    9  10   11
        // 刘啊摹 : 1.3, 1.4, 1.5, 1.6, 1.7, 1.8, 1.9, 2, 2.1, 2.2
        // 
        // <荐侥>
        // 版氰摹 X 弊缝盔荐俊蝶弗 版氰摹刘啊盒 X 磊脚狼 饭骇 / 弊缝盔 傈眉饭骇狼 钦
        // 
        // <汲疙>
        // 弊缝盔 傈眉狼 饭骇狼 钦 吝 磊脚捞 瞒瘤窍绰 饭骇父怒 刘啊等 版氰摹甫 唱床啊咙
        public void GainExp(long exp)
        {
            int i;
            int n;
            int sumlv;
            long dexp;
            long iexp;
            TCreature cret;
            double[] bonus = { 1, 1.2, 1.3, 1.4, 1.5, 1.6, 1.7, 1.8, 1.9, 2, 2.1, 2.2 };
            try
            {
                if (GroupOwner != null)
                {
                    sumlv = 0;
                    n = 0;
                    for (i = 0; i < GroupOwner.GroupMembers.Count; i++)
                    {
                        cret = GroupOwner.GroupMembers[i];
                        if (!cret.Death && (PEnvir == cret.PEnvir) && (Math.Abs(CX - cret.CX) <= 12) && (Math.Abs(CY - cret.CY) <= 12))
                        {
                            sumlv = sumlv + cret.Abil.Level;
                            n++;
                        }
                    }
                    if ((sumlv > 0) && (n > 1))
                    {
                        dexp = 0;
                        if (n >= 0 && n <= ObjBase.GROUPMAX)
                        {
                            dexp = HUtil32.MathRound(exp * bonus[n]);
                        }
                        for (i = 0; i < GroupOwner.GroupMembers.Count; i++)
                        {
                            cret = GroupOwner.GroupMembers[i];
                            if (!cret.Death && (PEnvir == cret.PEnvir) && (Math.Abs(CX - cret.CX) <= 12) && (Math.Abs(CY - cret.CY) <= 12))
                            {
                                iexp = HUtil32.MathRound(dexp / (decimal)(sumlv * cret.Abil.Level));
                                if (iexp > exp)
                                {
                                    iexp = exp;
                                }
                                cret.WinExp(iexp);
                            }
                        }
                    }
                    else
                    {
                        WinExp(exp);
                    }
                }
                else
                {
                    WinExp(exp);
                }
            }
            catch
            {
                M2Share.MainOutMessage("[Exception] TCreature.GainExp");
            }
        }

        public int GainSlaveExp_NextExp()
        {
            int result;
            int[] slaveupexp = { 0, 0, 50, 100, 200, 300, 600 };
            int more;
            if (SlaveExpLevel >= 0 && SlaveExpLevel <= 6)
            {
                more = slaveupexp[SlaveExpLevel];
            }
            else
            {
                more = 0;
            }
            result = 100 + (Abil.Level * 15) + more;
            // + more
            // + (Abil.MaxHP div 100) * 30;  //眉仿俊 蝶扼

            return result;
        }

        // 阁胶磐档 何窍牢 版快 版氰摹甫 冈澜
        public void GainSlaveExp(int exp)
        {
            // 岿飞 肚绰 盒脚老 版快俊绰 版氰摹甫 冈瘤 臼绰促.
            if ((RaceServer == Grobal2.RC_CLONE) || (RaceServer == Grobal2.RC_ANGEL))
            {
                return;
            }
            SlaveExp = SlaveExp + exp;
            if (SlaveExp > GainSlaveExp_NextExp())
            {
                SlaveExp = SlaveExp - GainSlaveExp_NextExp();
                if (SlaveExpLevel < (SlaveMakeLevel * 2 + 1))
                {
                    SlaveExpLevel++;
                    RecalcAbilitys();
                    // ApplySlaveLevelAbilitys;
                    ChangeNameColor();
                }
            }
        }

        // 何窍牢版快档 版氰摹啊 阶烙
        // [林狼] 府悸 惑怕俊辑 器牢飘甫 棵赴促.
        public void ApplySlaveLevelAbilitys()
        {
            int chp;
            if ((RaceServer == Grobal2.RC_ANGEL) || (RaceServer == Grobal2.RC_CLONE))
            {
                return;
            }
            chp = 0;
            // 归榜
            if ((RaceServer == Grobal2.RC_WHITESKELETON) || (RaceServer == Grobal2.RC_ELFMON) || (RaceServer == Grobal2.RC_ELFWARRIORMON))
            {
                WAbil.DC = MakeWord(LoByte(WAbil.DC), HiByte(Abil.DC));
                WAbil.DC = MakeWord(LoByte(WAbil.DC), HUtil32.MathRound(HiByte(WAbil.DC) + (3 * (0.3 + SlaveExpLevel * 0.1) * SlaveExpLevel)));
                chp = chp + HUtil32.MathRound(Abil.MaxHP * (0.3 + SlaveExpLevel * 0.1)) * SlaveExpLevel;
                chp = Abil.MaxHP + chp;
                if (SlaveExpLevel > 0)
                {
                    WAbil.MaxHP = (short)chp;
                    // _MIN(Round(Abil.MaxHP + 60 * SlaveExpLevel), chp);
                    // Round(Abil.MaxHP * SlaveExpLevel * 1.2))
                }
                else
                {
                    WAbil.MaxHP = Abil.MaxHP;
                }
                // 2003/03/15 脚痹公傍 眠啊
                WAbil.DC = MakeWord(LoByte(WAbil.DC), HiByte(WAbil.DC) + ExtraAbil[Grobal2.EABIL_DCUP]);
            }
            else
            {
                // 贱荤啊 部脚各
                if (Master != null)
                {
                    // 贱荤啊 部脚芭绰 Master 啊 nil 捞 酒丛
                    chp = Abil.MaxHP;
                    WAbil.DC = MakeWord(LoByte(WAbil.DC), HiByte(Abil.DC));
                    WAbil.DC = MakeWord(LoByte(WAbil.DC), HUtil32.MathRound(HiByte(WAbil.DC) + (2 * SlaveExpLevel)));
                    chp = chp + HUtil32.MathRound(Abil.MaxHP * 0.15) * SlaveExpLevel;
                    WAbil.MaxHP = (short)_MIN(HUtil32.MathRound(Abil.MaxHP + 60 * SlaveExpLevel), chp);
                    WAbil.MAC = 0;
                    // 抛捞怪各篮 付过俊 距窃
                    // Round(Abil.MaxHP * SlaveExpLevel * 1.2))
                }
            }
            AccuracyPoint = 15;
            // 沥犬档,.. (抛捞怪各,家券各篮 沥犬捞 15肺 绊沥)

        }

        // 版氰摹甫 掘澜, 饭骇诀 眉农
        public void WinExp(long exp)
        {
            int ExpRate;
            long exptotal;
            // 版氰摹 坷幅 巩力
            if (exp >= 60000)
            {
                exp = 60000;
            }
            exptotal = exp;
            // 版氰摹 利侩
            ExpRate = 100;
            switch (ExpRate)
            {
                case 100:
                    Abil.Exp = (int)(Abil.Exp + exp);
                    SendMsg(this, Grobal2.RM_WINEXP, 0, exp, 0, 0, "");
                    exptotal = exp;
                    break;
                case 120:
                    Abil.Exp = (int)(Abil.Exp + exp + (exp / 5));
                    SendMsg(this, Grobal2.RM_WINEXP, 0, exp + (exp / 5), 0, 0, "");
                    exptotal = exp + (exp / 5);
                    break;
                case 130:
                    Abil.Exp = (int)(Abil.Exp + exp + (exp / 3));
                    SendMsg(this, Grobal2.RM_WINEXP, 0, exp + (exp / 3), 0, 0, "");
                    exptotal = exp + (exp / 3);
                    break;
                case 150:
                    Abil.Exp = (int)(Abil.Exp + exp + (exp / 2));
                    SendMsg(this, Grobal2.RM_WINEXP, 0, exp + (exp / 2), 0, 0, "");
                    exptotal = exp + (exp / 2);
                    break;
                case 200:
                    Abil.Exp = (int)(Abil.Exp + exp + exp);
                    SendMsg(this, Grobal2.RM_WINEXP, 0, exp + exp, 0, 0, "");
                    exptotal = exp + exp;
                    break;
                case 300:
                    Abil.Exp = (int)(Abil.Exp + exp + exp + exp);
                    SendMsg(this, Grobal2.RM_WINEXP, 0, exp + exp + exp, 0, 0, "");
                    exptotal = exp + exp + exp;
                    break;
            }
            // 版氰摹 冈阑 锭付促 青款摹 坷抚
            AddBodyLuck(exp * 0.002);
            if (Abil.Exp >= Abil.MaxExp)
            {
                Abil.Exp = Abil.Exp - Abil.MaxExp;
                Abil.Level++;
                HasLevelUp(Abil.Level - 1);
                AddBodyLuck(100);
                // 肪诀锭付促 青款 蔼捞 坷弗促.
                // 肪诀_ +
                M2Share.AddUserLog("12\09" + MapName + "\09" + Abil.Level.ToString() + "\09" + Abil.Exp.ToString() + "\09" + UserName + "\09" + "0\09" + "0\09" + "1\09" + "0");
                // 肪诀窍搁 眉仿捞 父顶曼,  9/25老何磐 利侩
                IncHealthSpell(2000, 2000);
            }
        }

        public void HasLevelUp(int prevlevel)
        {
            Abil.MaxExp = (int)GetNextLevelExp(Abil.Level);
            // 促澜 饭骇阑 棵府绰单 鞘夸茄 版氰摹
            // if prevlevel <> 0 then begin
            RecalcLevelAbilitys();
            // 饭骇俊 蝶弗 瓷仿摹甫 拌魂茄促.
            // end else
            // RecalcLevelAbilitys_old;
#if FOR_ABIL_POINT
            // 4/16老 何磐 利侩
            if (prevlevel + 1 == Abil.Level)
            {
                BonusPoint = BonusPoint + M2Share.GetBonusPoint(Job, Abil.Level);
                // 肪诀俊 蝶弗 焊呈胶
                SendMsg(this, Grobal2.RM_ADJUST_BONUS, 0, 0, 0, 0, "");
            }
            else
            {
                if (prevlevel != Abil.Level)
                {
                    // 焊呈胶 器牢飘甫 贸澜何磐 促矫 拌魂茄促.
                    BonusPoint = M2Share.GetLevelBonusSum(Job, Abil.Level);
                                        FillChar(BonusAbil, sizeof(TNakedAbility), '\0');
                                        FillChar(CurBonusAbil, sizeof(TNakedAbility), '\0');
                    // if prevlevel <> 0 then begin
                    RecalcLevelAbilitys();
                    // 饭骇俊 蝶弗 瓷仿摹甫 拌魂茄促.
                    // end else begin
                    // RecalcLevelAbilitys_old;
                    // BonusPoint := 0;
                    // end;
                    SendMsg(this, Grobal2.RM_ADJUST_BONUS, 0, 0, 0, 0, "");
                }
            }
#endif
            RecalcAbilitys();
            SendRefMsg(Grobal2.RM_LOOPNORMALEFFECT, (short)this.ActorId, 0, 0, Grobal2.NE_LEVELUP, "");
            SendMsg(this, Grobal2.RM_LEVELUP, 0, Abil.Exp, 0, 0, "");
            if (M2Share.DefaultNpc != null)
            {
                M2Share.DefaultNpc.NpcSayTitle(this, "@_UPLVLEVENT");
            }
            // 眉氰魄 荤侩磊绰 眉氰魄 饭骇捞惑 棵副 荐 绝促.(sonmg 2005/03/17)
            if (RaceServer == Grobal2.RC_USERHUMAN)
            {
                if ((this as TUserHuman).ApprovalMode == 1)
                {
                    if (Abil.Level > ObjBase.EXPERIENCELEVEL)
                    {
                        SysMsg("The trial mode can be used up to level " + ObjBase.EXPERIENCELEVEL.ToString(), 0);
                        SysMsg("connection was terminated.", 0);
                        (this as TUserHuman).EmergencyClose = true;
                    }
                }
            }
        }

        public long GetNextLevelExp(int lv)
        {
            long result;
            // 惑荐肺 沥窍扁肺 窃
            if (lv >= 1 && lv <= M2Share.MAXLEVEL)
            {
                result = M2Share.NEEDEXPS[lv];
            }
            else
            {
                result = 0x7FFFFFFF;
            }
            // 荐沥 (sonmg 2005/05/13)

            return result;
        }

        public void ChangeLevel(int level)
        {
            if (level >= 1 && level <= 40)
            {
                Abil.Level = (byte)level;
            }
        }

        public bool InSafeZone()
        {
            string map;
            int sx = 0;
            int sy = 0;
            bool result = PEnvir.LawFull;
            if (!result)
            {
                result = (PEnvir.MapName == M2Share.BADMANHOMEMAP) && (Math.Abs(CX - M2Share.BADMANSTARTX) <= 10) && (Math.Abs(CY - M2Share.BADMANSTARTY) <= 10);
                if (!result)
                {
                    for (var i = 0; i < M2Share.StartPoints.Count; i++)
                    {
                        map = M2Share.StartPoints[i].mapName;
                        sx = M2Share.StartPoints[i].nX;
                        sy = M2Share.StartPoints[i].nY;
                        if ((map == PEnvir.MapName) && (Math.Abs(CX - sx) <= 10) && (Math.Abs(CY - sy) <= 10))
                        {
                            result = true;
                            break;
                        }
                    }
                    for (var i = 0; i < M2Share.SafePoints.Count; i++)
                    {
                        map = M2Share.SafePoints[i].mapName;
                        sx = M2Share.SafePoints[i].nX;
                        sy = M2Share.SafePoints[i].nY;
                        if ((map == PEnvir.MapName) && (Math.Abs(CX - sx) <= 10) && (Math.Abs(CY - sy) <= 10))
                        {
                            result = true;
                            break;
                        }
                    }
                }
            }
            return result;
        }

        public bool InGuildWarSafeZone()
        {
            string map = string.Empty;
            int sx = 0;
            int sy = 0;
            bool result = PEnvir.LawFull;
            if (!result)
            {
                if (!result)
                {
                    for (var i = 0; i < M2Share.StartPoints.Count; i++)
                    {
                        //map = svMain.StartPoints[i];
                        //sx = HUtil32.Loword((int)svMain.StartPoints.Values[i]);
                        //sy = Hiword((int)svMain.StartPoints.Values[i]);
                        if ((map == PEnvir.MapName) && (Math.Abs(CX - sx) <= 60) && (Math.Abs(CY - sy) <= 60))
                        {
                            result = true;
                            break;
                        }
                    }
                }
            }
            return result;
        }

        public int PKLevel()
        {
            return PlayerKillingPoint / 100;
        }

        public void UserNameChanged()
        {
            SendRefMsg(Grobal2.RM_USERNAME, 0, 0, 0, 0, GetUserName());
        }

        public void ChangeNameColor()
        {
            SendRefMsg(Grobal2.RM_CHANGENAMECOLOR, 0, 0, 0, 0, "");
        }

        public byte MyColor()
        {
            byte result = DefNameColor;
            if (PKLevel() == 1)
            {
                result = 251;
            }
            if (PKLevel() >= 2)
            {
                result = 249;
            }
            return result;
        }

        // self啊 cret阑 好阑锭 cret狼 祸阑 府畔茄促(sonmg 2005/11/29)
        public byte GetThisCharColor(TCreature cret)
        {
            byte result;
            byte[] SlaveColors = { 255, 254, 147, 154, 229, 168, 180, 252 };
            int relat;
            bool CheckAllyGuild;
            result = cret.MyColor();
            if (cret.RaceServer == Grobal2.RC_USERHUMAN)
            {
                // 荤恩
                if (PKLevel() < 2)
                {
                    // 闰嫡捞=0(肚绰 畴珐捞=1) 牢 版快
                    if (cret.BoIllegalAttack)
                    {
                        result = 47;
                    }
                    // 哎祸拌凯
                    relat = GetGuildRelation(this, cret);
                    switch (relat)
                    {
                        case 1:
                        case 3:
                            result = 180;
                            break;
                        case 2:
                            // 仟弗拌凯 (快府祈)
                            result = 69;
                            break;
                            // 林炔祸拌凯
                    }
                    if (cret.PEnvir.Fight3Zone)
                    {
                        // 巩颇 措访厘 救俊 乐澜
                        if (MyGuild == cret.MyGuild)
                        {
                            // 鞍篮祈
                            // 仟弗拌凯
                            result = 180;
                        }
                        else
                        {
                            // 促弗 祈
                            result = 69;
                        }
                        // 林炔祸拌凯
                    }
                }
                // 傍己傈 包访 祸
                if (M2Share.UserCastle.BoCastleUnderAttack)
                {
                    // 傍己傈 吝牢 版快
                    if (BoInFreePKArea && cret.BoInFreePKArea)
                    {
                        // 橇府乔纳捞粮(傈里磐)俊 乐澜, 傍己 瘤开俊 乐澜
                        result = 221;
                        // 利档酒聪绊 快府祈档 酒聪搁 踌祸栏肺 焊牢促.
                        BoGuildWarArea = true;
                        // 傍己傈 瘤开
                        CheckAllyGuild = false;
                        if ((M2Share.UserCastle.OwnerGuild != null) && (MyGuild != null))
                        {
                            CheckAllyGuild = M2Share.UserCastle.OwnerGuild.IsAllyGuild(MyGuild);
                        }
                        else
                        {
                            CheckAllyGuild = false;
                        }
                        // 2003/06/12 荐己 巩颇 挥父 酒聪扼 荐己巩颇 楷钦 巩颇牢版快档 秦寸登档废 荐沥
                        // if UserCastle.IsOurCastle (TGuild(MyGuild)) then begin
                        if (M2Share.UserCastle.IsOurCastle(MyGuild) || CheckAllyGuild)
                        {
                            // 己阑 瘤虐绰 涝厘
                            if ((MyGuild == cret.MyGuild) || MyGuild.IsAllyGuild(cret.MyGuild))
                            {
                                // 快府巩颇,悼竿巩颇
                                // 仟弗拌凯 (快府祈)
                                result = 180;
                            }
                            else if (M2Share.UserCastle.IsRushCastleGuild(cret.MyGuild))
                            {
                                result = 69;
                            }
                            // 傍拜窍绊 乐绰 巩颇, 利
                        }
                        else
                        {
                            // 己阑 傍拜窍绰 涝厘(傍己 肚绰 傍己悼竿)
                            if (M2Share.UserCastle.IsRushCastleGuild(MyGuild))
                            {
                                // 快府 巩颇啊 傍拜窍绊 乐澜
                                if ((MyGuild == cret.MyGuild) || MyGuild.IsAllyGuild(cret.MyGuild))
                                {
                                    // 快府 巩颇盔 烙, 悼竿 巩颇盔
                                    // 仟弗拌凯 (快府祈)
                                    result = 180;
                                }
                                else
                                {
                                    if (M2Share.UserCastle.IsCastleMember(cret as TUserHuman))
                                    {
                                        result = 69;
                                    }
                                    // 己阑 瞒瘤茄 巩颇绰 利栏肺 焊牢促.
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                // 阁胶磐
                try
                {
                    if (cret.RaceServer == Grobal2.RC_CLONE)
                    {
                        if ((cret.Master != null) && (cret.Master.RaceServer == Grobal2.RC_USERHUMAN))
                        {
                            result = cret.Master.MyColor();
                        }
                    }
                    else if (cret.RaceServer == Grobal2.RC_NPC)
                    {
                        // 增加NPC名字颜色
                        result = 222;
                    }
                    else
                    {
                        if (cret.SlaveExpLevel >= 0 && cret.SlaveExpLevel <= 7)
                        {
                            result = SlaveColors[cret.SlaveExpLevel];
                        }
                        if (cret.BoCrazyMode)
                        {
                            result = 249;
                        }
                        // red 气林惑怕
                        if (cret.BoGoodCrazyMode)
                        {
                            result = 253;
                        }
                        // violet 蚌霸固模惑怕 (祸彬炼沥)
                        if (cret.BoHolySeize)
                        {
                            result = 125;
                        }
                    }
                }
                catch
                {
                    M2Share.MainOutMessage("EXCEPT CHARCOLOR");
                }
            }
            return result;
        }

        // 0: 惑包包拌绝澜
        // 1: 快府 巩颇
        // 2: 利措 包拌
        // 3: 悼竿包拌
        public int GetGuildRelation(TCreature onecret, TCreature twocret)
        {
            int result;
            result = 0;
            BoGuildWarArea = false;
            if ((onecret.MyGuild != null) && (twocret.MyGuild != null))
            {
                if (onecret.InGuildWarSafeZone() || twocret.InGuildWarSafeZone())
                {
                    result = 0;
                    // 巩颇傈 陛瘤 备开
                }
                else
                {
                    if (onecret.MyGuild.KillGuilds.Count > 0)
                    {
                        BoGuildWarArea = true;
                        if (onecret.MyGuild.IsHostileGuild(twocret.MyGuild) && twocret.MyGuild.IsHostileGuild(onecret.MyGuild))
                        {
                            result = 2;
                            // 69;  //林炔祸拌凯, 利
                        }
                        if (onecret.MyGuild == twocret.MyGuild)
                        {
                            result = 1;
                            // 180; //仟弗拌凯 (快府祈)
                        }
                        if (onecret.MyGuild.IsAllyGuild(twocret.MyGuild) && twocret.MyGuild.IsAllyGuild(onecret.MyGuild))
                        {
                            result = 3;
                        }
                        // 悼竿包拌
                    }
                }
            }
            return result;
        }

        public bool IsGuildMaster()
        {
            bool result;
            if ((MyGuild != null) && (GuildRank == 1))
            {
                result = true;
            }
            else
            {
                result = false;
            }
            return result;
        }

        public bool IsMyGuildMaster()
        {
            bool result;
            TGuildAgit guildagit;
            result = false;
            if ((MyGuild != null) && (GuildRank == 1))
            {
                // 促弗 巩林捞搁 FALSE.
                // 巩林狼 厘盔 锅龋客 泅犁 乐绰 甘狼 厘盔锅龋啊 老摹 秦具窃.
                guildagit = M2Share.GuildAgitMan.GetGuildAgit(MyGuild.GuildName);
                if (guildagit != null)
                {
                    if ((guildagit.GuildAgitNumber > -1) && (guildagit.GuildAgitNumber == PEnvir.GuildAgit))
                    {
                        result = true;
                    }
                }
            }
            return result;
        }

        // sonmg(2004/04/08)
        public string GetGuildNameHereAgit()
        {
            string result;
            result = "";
            // 泅犁 乐绰 甘狼 厘盔 巩颇甫 掘绰促.
            if (PEnvir.GuildAgit > -1)
            {
                result = M2Share.GuildAgitMan.GetGuildNameFromAgitNum(PEnvir.GuildAgit);
            }
            // 
            // if MyGuild <> nil then begin
            // // 啊涝等 巩颇客 泅犁 乐绰 甘狼 厘盔狼 巩颇啊 老摹 秦具窃.
            // guildagit := GuildAgitMan.GetGuildAgit( TGuild(MyGuild).GuildName );
            // if guildagit <> nil then begin
            // if (guildagit.GuildAgitNumber > -1 ) and (guildagit.GuildAgitNumber = PEnvir.GuildAgit) then begin
            // Result := TRUE;
            // end;
            // end;
            // end;

            return result;
        }

        // 泅犁 厘盔狼 巩颇疙阑 掘绢咳.
        public string GetGuildMasterNameHereAgit()
        {
            string result;
            result = "";
            // 泅犁 乐绰 甘狼 厘盔 巩颇甫 掘绰促.
            if (PEnvir.GuildAgit > -1)
            {
                result = M2Share.GuildAgitMan.GetGuildMasterNameFromAgitNum(PEnvir.GuildAgit);
            }
            return result;
        }

        // 泅犁 厘盔狼 巩林疙阑 掘绢咳.
        public void IncPKPoint(int point)
        {
            int old;
            old = PKLevel();
            // if old >= 2 then point := point * 2; //啊吝贸国
            // PkPoint 100父栏肺 力茄(sonmg 2005/04/14)
            PlayerKillingPoint = _MIN(1000000, PlayerKillingPoint + point);
            if ((old != PKLevel()) && (PKLevel() <= 2))
            {
                ChangeNameColor();
            }
        }

        public void DecPKPoint(int point)
        {
            int old;
            old = PKLevel();
            PlayerKillingPoint = PlayerKillingPoint - point;
            if (PlayerKillingPoint < 0)
            {
                PlayerKillingPoint = 0;
            }
            if ((old != PKLevel()) && (old > 0) && (old <= 2))
            {
                ChangeNameColor();
            }
        }

        public string GetPKTimeMin()
        {
            string result;
            string hourstr;
            // 
            // //撇赴 内靛... 2甫 蚌秦具 窃.
            // if PlayerKillingPoint  < (60 * 24) then
            // result := '24矫埃捞郴'
            // else if PlayerKillingPoint  < ( 60 * 24  * 7 )then
            // result := '1~7 老捞郴'
            // else if PlayerKillingPoint  < ( 60 * 24  * 14) then
            // result := '8~14 老捞郴'
            // else if PlayerKillingPoint  < ( 60 * 24  * 30) then
            // result := '15~30 老捞郴'
            // else result := '茄崔捞惑'
            hourstr = "小时";
            // PkPoint 1痢 皑家登绰单 2盒 家夸.
            result = HUtil32.MathRound(PlayerKillingPoint * 2 / 60).ToString() + hourstr;
            return result;
        }

        public void AddBodyLuck(double r)
        {
            int n;
            if ((r > 0) && (BodyLuck < 5 * ObjBase.BODYLUCKUNIT))
            {
                BodyLuck = BodyLuck + r;
            }
            if ((r < 0) && (BodyLuck > -(5 * ObjBase.BODYLUCKUNIT)))
            {
                BodyLuck = BodyLuck + r;
            }
            n = (int)Convert.ToInt64(BodyLuck / ObjBase.BODYLUCKUNIT);
            if (n > 5)
            {
                n = 5;
            }
            if (n < -10)
            {
                n = -10;
            }
            BodyLuckLevel = n;
        }

        public bool IncGold(int igold)
        {
            bool result;
            result = false;
            if (igold < 0)
            {
                return result;
            }
            // (sonmg 2005/06/22)
            if ((long)Gold + igold <= AvailableGold)
            {
                Gold = Gold + igold;
                result = true;
            }
            return result;
        }

        public bool DecGold(int igold)
        {
            bool result;
            result = false;
            if (igold < 0)
            {
                return result;
            }
            if ((long)Gold - igold >= 0)
            {
                Gold = Gold - igold;
                result = true;
            }
            return result;
        }

        public bool IncGameGold(int igold)
        {
            bool result;
            result = false;
            if (igold < 0)
            {
                return result;
            }
            // (sonmg 2005/06/22)
            if ((long)GameGold + igold <= AvailableGold)
            {
                GameGold = GameGold + igold;
                result = true;
            }
            return result;
        }

        public bool DecGameGold(int igold)
        {
            bool result;
            result = false;
            if (igold < 0)
            {
                return result;
            }
            if ((long)GameGold - igold >= 0)
            {
                GameGold = GameGold - igold;
                result = true;
            }
            return result;
        }

        public int CalcBagWeight()
        {
            int result;
            int i;
            int w;
            int temp;
            TStdItem ps;
            w = 0;
            for (i = 0; i < ItemList.Count; i++)
            {
                ps = M2Share.UserEngine.GetStdItem(ItemList[i].Index);
                if (ps != null)
                {
                    if (ps.OverlapItem == 1)
                    {
                        temp = ItemList[i].Dura;
                        w = w + (temp / 10);
                        // 墨款飘 酒捞袍(1)篮 10俺寸 公霸 1
                    }
                    else if (ps.OverlapItem >= 2)
                    {
                        temp = ItemList[i].Dura;
                        // 墨款飘 酒捞袍(2捞惑)篮 1俺寸 公霸 1
                        w = w + temp * ps.Weight;
                    }
                    else
                    {
                        w = w + ps.Weight;
                    }
                }
            }
            result = w;
            return result;
        }

        public int CalcWearWeightEx(int windex)
        {
            int result;
            int i;
            int w;
            TStdItem ps;
            w = 0;
            // 2003/03/15 酒捞袍 牢亥配府 犬厘
            for (i = 0; i <= 12; i++)
            {
                // 8->12
                if ((windex == -1) || (i != windex) && (i != Grobal2.U_WEAPON) && (i != Grobal2.U_RIGHTHAND))
                {
                    ps = M2Share.UserEngine.GetStdItem(UseItems[i].Index);
                    if (ps != null)
                    {
                        w = w + ps.Weight;
                    }
                }
            }
            result = w;
            return result;
        }

        // windex甫 力寇茄 馒侩酒捞袍狼 公霸
#if FOR_ABIL_POINT
        // 4/16老何磐 利侩
        // 磊脚狼 饭骇俊 嘎绰 瓷仿摹
        // 荤侩救窃.
        public void RecalcLevelAbilitys()
        {
            // 焊呈胶 器牢飘 利侩矫
            int n;
            int mlevel;
            // 焊呈胶 器牢飘俊 措茄 瓷仿摹 炼沥
            // 泅犁, 荤侩窍瘤 臼澜
            if (Abil.Level > M2Share.ADJ_LEVEL)
            {
                mlevel = M2Share.ADJ_LEVEL;
            }
            else
            {
                mlevel = Abil.Level;
            }
            switch(Job)
            {
                case 0:
                    // 傈荤
                    Abil.MaxWeight = 50 + HUtil32.MathRound((Abil.Level / 3) * Abil.Level);
                                        Abil.MaxWearWeight = _MIN(255, 15 + HUtil32.MathRound((Abil.Level / 20) * Abil.Level));
                    // 2003/02/11 弥措公霸 255肺 力茄
                    if (((12 + HUtil32.MathRound((Abil.Level / 13) * Abil.Level)) > 255))
                    {
                        Abil.MaxHandWeight = 255;
                    }
                    else
                    {
                        Abil.MaxHandWeight = 12 + HUtil32.MathRound((Abil.Level / 13) * Abil.Level);
                    }
                    Abil.MaxHP = ObjBase.DEFHP + HUtil32.MathRound((mlevel / 4 + 4) * mlevel);
                    Abil.MaxMP = ObjBase.DEFMP + mlevel * 2;
                                                                                Abil.DC = MakeWord(_MAX(mlevel / 7 - 1, 1), _MAX(1, mlevel / 5));
                    Abil.SC = 0;
                    Abil.MC = 0;
                                        Abil.AC = MakeWord(0, mlevel / 7);
                    Abil.MAC = 0;
                    break;
                case 1:
                    // 贱荤牢版快
                    // 4.2
                    Abil.MaxWeight = 50 + HUtil32.MathRound((Abil.Level / 5) * Abil.Level);
                    Abil.MaxWearWeight = 15 + HUtil32.MathRound((Abil.Level / 100) * Abil.Level);
                    Abil.MaxHandWeight = 12 + HUtil32.MathRound((Abil.Level / 90) * Abil.Level);
                    // 18/30
                    Abil.MaxHP = ObjBase.DEFHP + HUtil32.MathRound((mlevel / 15 + 1.8) * mlevel);
                    Abil.MaxMP = ObjBase.DEFMP + HUtil32.MathRound((mlevel / 5 + 2) * 2.2 * mlevel);
                    n = mlevel / 7;
                                                                                Abil.DC = MakeWord(_MAX(n - 1, 0), _MAX(1, n));
                                                                                Abil.MC = MakeWord(_MAX(n - 1, 0), _MAX(1, n));
                    Abil.SC = 0;
                    Abil.AC = 0;
                    Abil.MAC = 0;
                    break;
                case 2:
                    // 档荤牢 版快
                    // 3.5
                    Abil.MaxWeight = 50 + HUtil32.MathRound((Abil.Level / 4) * Abil.Level);
                    Abil.MaxWearWeight = 15 + HUtil32.MathRound((Abil.Level / 50) * Abil.Level);
                    Abil.MaxHandWeight = 12 + HUtil32.MathRound((Abil.Level / 42) * Abil.Level);
                    // 13
                    Abil.MaxHP = ObjBase.DEFHP + HUtil32.MathRound((mlevel / 6 + 2.5) * mlevel);
                    Abil.MaxMP = ObjBase.DEFMP + HUtil32.MathRound((mlevel / 8) * 2.2 * mlevel);
                    n = mlevel / 7;
                                                                                Abil.DC = MakeWord(_MAX(n - 1, 0), _MAX(1, n));
                    Abil.MC = 0;
                                                                                Abil.SC = MakeWord(_MAX(n - 1, 0), _MAX(1, n));
                    Abil.AC = 0;
                    n = HUtil32.MathRound(mlevel / 6);
                                        Abil.MAC = MakeWord(n / 2, n + 1);
                    break;
            }
            Abil.MaxHP = Abil.MaxHP + BonusAbil.HP;
            Abil.MaxMP = Abil.MaxMP + BonusAbil.MP;
                                                                        Abil.DC = MakeWord(LoByte(Abil.DC) + LoByte(BonusAbil.DC), HiByte(Abil.DC) + HiByte(BonusAbil.DC));
                                                                        Abil.SC = MakeWord(LoByte(Abil.SC) + LoByte(BonusAbil.SC), HiByte(Abil.SC) + HiByte(BonusAbil.SC));
                                                                        Abil.MC = MakeWord(LoByte(Abil.MC) + LoByte(BonusAbil.MC), HiByte(Abil.MC) + HiByte(BonusAbil.MC));
                                                                        Abil.AC = MakeWord(LoByte(Abil.AC) + LoByte(BonusAbil.AC), HiByte(Abil.AC) + HiByte(BonusAbil.AC));
                                                                        Abil.MAC = MakeWord(LoByte(Abil.MAC) + LoByte(BonusAbil.MAC), HiByte(Abil.MAC) + HiByte(BonusAbil.MAC));
            if (Abil.HP > Abil.MaxHP)
            {
                Abil.HP = Abil.MaxHP;
            }
            if (Abil.MP > Abil.MaxMP)
            {
                Abil.MP = Abil.MaxMP;
            }
        }

#else

        public void RecalcLevelAbilitys()
        {
            int n;
            switch (Job)
            {
                case 0:
                    Abil.MaxHP = (short)(14 + HUtil32.MathRound((Abil.Level / 4 + 4.5 + (Abil.Level / 20)) * Abil.Level));
                    Abil.MaxMP = (short)(11 + HUtil32.MathRound(Abil.Level * 3.5));
                    Abil.MaxWeight = (ushort)(50 + HUtil32.MathRound(Abil.Level / 3 * Abil.Level));
                    Abil.MaxWearWeight = (byte)_MIN(255, 15 + HUtil32.MathRound(Abil.Level / 20 * Abil.Level));
                    if ((12 + HUtil32.MathRound(Abil.Level / 13 * Abil.Level)) > 255)
                    {
                        Abil.MaxHandWeight = 255;
                    }
                    else
                    {
                        Abil.MaxHandWeight = (byte)(12 + HUtil32.MathRound(Abil.Level / 13 * Abil.Level));
                    }
                    Abil.DC = MakeWord(_MAX(Abil.Level / 5 - 1, 1), _MAX(1, Abil.Level / 5));
                    Abil.SC = 0;
                    Abil.MC = 0;
                    Abil.AC = MakeWord(0, Abil.Level / 7);
                    Abil.MAC = 0;
                    break;
                case 1:
                    Abil.MaxHP = (short)(14 + HUtil32.MathRound((Abil.Level / 15 + 1.8) * Abil.Level));
                    Abil.MaxMP = (short)(13 + HUtil32.MathRound((Abil.Level / 5 + 2) * 2.2 * Abil.Level));
                    Abil.MaxWeight = (ushort)(50 + HUtil32.MathRound(Abil.Level / 5 * Abil.Level));
                    Abil.MaxWearWeight = (byte)(15 + HUtil32.MathRound(Abil.Level / 100 * Abil.Level));
                    Abil.MaxHandWeight = (byte)(12 + HUtil32.MathRound(Abil.Level / 90 * Abil.Level));
                    n = Abil.Level / 7;
                    Abil.DC = MakeWord(_MAX(n - 1, 0), _MAX(1, n));
                    Abil.MC = MakeWord(_MAX(n - 1, 0), _MAX(1, n));
                    Abil.SC = 0;
                    Abil.AC = 0;
                    Abil.MAC = 0;
                    break;
                case 2:
                    Abil.MaxHP = (short)(14 + HUtil32.MathRound((Abil.Level / 6 + 2.5) * Abil.Level));
                    Abil.MaxMP = (short)(13 + HUtil32.MathRound(Abil.Level / 8 * 2.2 * Abil.Level));
                    Abil.MaxWeight = (ushort)(50 + HUtil32.MathRound(Abil.Level / 4 * Abil.Level));
                    Abil.MaxWearWeight = (byte)(15 + HUtil32.MathRound(Abil.Level / 50 * Abil.Level));
                    Abil.MaxHandWeight = (byte)(12 + HUtil32.MathRound(Abil.Level / 42 * Abil.Level));
                    n = Abil.Level / 7;
                    Abil.DC = MakeWord(_MAX(n - 1, 0), _MAX(1, n));
                    Abil.MC = 0;
                    Abil.SC = MakeWord(_MAX(n - 1, 0), _MAX(1, n));
                    Abil.AC = 0;
                    n = HUtil32.MathRound(Abil.Level / 6);
                    Abil.MAC = MakeWord(n / 2, n + 1);
                    break;
            }
            if (Abil.HP > Abil.MaxHP)
            {
                Abil.HP = Abil.MaxHP;
            }
            if (Abil.MP > Abil.MaxMP)
            {
                Abil.MP = Abil.MaxMP;
            }
        }

#endif

        public void RecalcHitSpeed()
        {
            int i;
            TUserMagic pum;
            AccuracyPoint = (byte)(ObjBase.DEFHIT + BonusAbil.Hit);
            HitPowerPlus = 0;
            HitDouble = 0;
            switch (Job)
            {
                case 2:
                    SpeedPoint = (byte)(ObjBase.DEFSPEED + BonusAbil.Speed + 3);
                    break;
                default:
                    SpeedPoint = (byte)(ObjBase.DEFSPEED + BonusAbil.Speed);
                    break;
            }
            PSwordSkill = null;
            PPowerHitSkill = null;
            PLongHitSkill = null;
            PWideHitSkill = null;
            PFireHitSkill = null;
            // 2003/03/15 脚痹公傍
            PCrossHitSkill = null;
            PTwinHitSkill = null;
            PStoneHitSkill = null;
            for (i = 0; i < MagicList.Count; i++)
            {
                pum = MagicList[i];
                switch (pum.MagicId)
                {
                    case 3:
                        // 寇荐八过 (傈荤 扁檬 八过)
                        PSwordSkill = pum;
                        // 公傍 昏力矫 林狼秦具 茄促.
                        if (pum.Level > 0)
                        {
                            AccuracyPoint = (byte)(AccuracyPoint + HUtil32.MathRound(9 / 3 * pum.Level));
                        }
                        break;
                    case 7:
                        // 抗档八过 (傈荤狼 傍拜 八过)
                        PPowerHitSkill = pum;
                        if (pum.Level > 0)
                        {
                            AccuracyPoint = (byte)(AccuracyPoint + HUtil32.MathRound(3 / 3 * pum.Level));
                        }
                        HitPowerPlus = (byte)(5 + pum.Level);
                        // 颇况 5, 6, 7, 8
                        AttackSkillCount = 7 - PPowerHitSkill.Level;
                        AttackSkillPointCount = new System.Random(AttackSkillCount).Next();
                        break;
                    case 12:
                        // 绢八贱
                        PLongHitSkill = pum;
                        break;
                    case 25:
                        // 馆岿八过
                        PWideHitSkill = pum;
                        break;
                    case 26:
                        // 堪拳搬
                        PFireHitSkill = pum;
                        HitDouble = (byte)(4 + pum.Level * 4);
                        break;
                    case 34:
                        // +40% ~ +160%
                        // 2003/03/15 脚痹公傍
                        // 堡浅曼
                        HitPowerPlus = (byte)(5 + pum.Level);
                        // 颇况 5, 6, 7, 8
                        PCrossHitSkill = pum;
                        break;
                    case 38:
                        // 街锋曼
                        HitPowerPlus = pum.Level;
                        // 颇况 0, 1, 2, 3
                        PTwinHitSkill = pum;
                        break;
                    case 43:
                        // 荤磊饶
                        HitPowerPlus = pum.Level;
                        // 颇况 0, 1, 2, 3
                        PStoneHitSkill = pum;
                        break;
                    case 4:
                        // 老堡八过 (档荤 扁檬八过)
                        PSwordSkill = pum;
                        // 公傍 昏力矫 林狼秦具 茄促.
                        if (pum.Level > 0)
                        {
                            AccuracyPoint = (byte)(AccuracyPoint + HUtil32.MathRound(8 / 3 * pum.Level));
                        }
                        break;
                }
            }
        }

        public void AddMagicWithItem(int magic)
        {
            // 酒捞袍阑 馒侩秦辑 掘绰 付过
            TDefMagic pdm;
            TUserMagic pum;
            TUserHuman hum;
            pdm = null;
            if (magic == ObjBase.AM_FIREBALL)
            {
                // 拳堪厘
                pdm = M2Share.UserEngine.GetDefMagic("火球术");
            }
            if (magic == ObjBase.AM_HEALING)
            {
                pdm = M2Share.UserEngine.GetDefMagic("治愈术");
            }
            if (pdm != null)
            {
                if (!IsMyMagic(pdm.MagicId))
                {
                    pum = new TUserMagic();
                    pum.pDef = pdm;
                    pum.MagicId = pdm.MagicId;
                    pum.Key = '\0';
                    pum.Level = 1;
                    pum.CurTrain = 0;
                    MagicList.Add(pum);
                    // 付过阑 货肺 硅框..
                    if (RaceServer == Grobal2.RC_USERHUMAN)
                    {
                        hum = this as TUserHuman;
                        hum.SendAddMagic(pum);
                        // 付过 眠啊甫 努扼捞攫飘俊 舅覆
                    }
                }
            }
        }

        public void DelMagicWithItem_DelMagicByName(string mname)
        {
            int i;
            TUserHuman hum;
            if (this.RaceServer == Grobal2.RC_USERHUMAN)
            {
                // PDS
                for (i = MagicList.Count - 1; i >= 0; i--)
                {
                    if (MagicList[i].pDef.MagicName == mname)
                    {
                        hum = this as TUserHuman;
                        hum.SendDelMagic(MagicList[i]);
                        Dispose(MagicList[i]);
                        MagicList.RemoveAt(i);
                        break;
                    }
                }
            }
        }

        public void DelMagicWithItem(int magic)
        {
            if (RaceServer != Grobal2.RC_USERHUMAN)
            {
                return;
            }
            if (magic == ObjBase.AM_FIREBALL)
            {
                if (Job != 1)
                {
                    // 贱荤啊 酒聪搁
                    DelMagicWithItem_DelMagicByName("火球术");
                }
            }
            if (magic == ObjBase.AM_HEALING)
            {
                if (Job != 2)
                {
                    // 档荤啊 酒聪搁
                    DelMagicWithItem_DelMagicByName("治愈术");
                }
            }
        }

        // 犁积狼馆瘤狼 郴备仿捞 粹绰促.
        public void ItemDamageRevivalRing()
        {
            int i;
            int idura;
            int olddura;
            TStdItem pstd;
            TUserHuman hum;
            for (i = 0; i <= 12; i++)
            {
                if (UseItems[i].Index > 0)
                {
                    pstd = M2Share.UserEngine.GetStdItem(UseItems[i].Index);
                    if (pstd != null)
                    {
                        if ((i == Grobal2.U_RINGR) || (i == Grobal2.U_RINGL))
                        {
                            if (pstd.Shape == ObjBase.RING_REVIVAL_ITEM)
                            {
                                idura = UseItems[i].Dura;
                                olddura = HUtil32.MathRound(idura / 1000);
                                idura = idura - 1000;
                                if (idura <= 0)
                                {
                                    idura = 0;
                                    UseItems[i].Dura = (short)idura;
                                    if (RaceServer == Grobal2.RC_USERHUMAN)
                                    {
                                        hum = this as TUserHuman;
                                        hum.SendDelItem(UseItems[i]);
                                        hum.SysMsg(pstd.Name + "is destroyed.", 0);
                                    }
                                    M2Share.AddUserLog("16\09" + MapName + "\09" + CX.ToString() + "\09" + CY.ToString() + "\09" + UserName + "\09" + pstd.Name + "\09" + UseItems[i].MakeIndex.ToString() + "\09" + HUtil32.BoolToIntStr(RaceServer == Grobal2.RC_USERHUMAN).ToString() + "\09" + "0");
                                    UseItems[i].Index = 0;
                                    RecalcAbilitys();
                                }
                                else
                                {
                                    UseItems[i].Dura = (short)idura;
                                    M2Share.AddUserLog("11\09" + MapName + "\09" + CX.ToString() + "\09" + CY.ToString() + "\09" + UserName + "\09" + pstd.Name + "\09" + UseItems[i].MakeIndex.ToString() + "\09" + HUtil32.BoolToIntStr(RaceServer == Grobal2.RC_USERHUMAN).ToString() + "\09" + "1");
                                }
                                if (olddura != HUtil32.MathRound(idura / 1000))
                                {
                                    SendMsg(this, Grobal2.RM_DURACHANGE, (short)i, idura, UseItems[i].DuraMax, 0, "");
                                }
                                break;
                            }
                        }
                    }
                }
            }
        }

        public virtual void RecalcAbilitys()
        {
            int i;
            int oldlight;
            bool[] cghi = new bool[3 + 1];
            TStdItem pstd;
            TAbility temp;
            bool fastmoveflag;
            bool oldhmode;
            bool mh_ring;
            bool mh_bracelet;
            bool mh_necklace;
            bool sh_ring;
            bool sh_bracelet;
            bool sh_necklace;
            bool hp_ring;
            bool hp_bracelet;
            bool mp_ring;
            bool mp_bracelet;
            bool hpmp_ring;
            bool hpmp_bracelet;
            bool hpp_necklace;
            bool hpp_bracelet;
            bool hpp_ring;
            bool cho_weapon;
            bool cho_necklace;
            bool cho_ring;
            bool cho_helmet;
            bool cho_bracelet;
            bool pset_necklace;
            bool pset_bracelet;
            bool pset_ring;
            bool hset_necklace;
            bool hset_bracelet;
            bool hset_ring;
            bool yset_necklace;
            bool yset_bracelet;
            bool yset_ring;
            bool dset_wingdress;
            bool boneset_weapon;
            bool boneset_helmet;
            bool boneset_dress;
            bool bugset_necklace;
            bool bugset_ring;
            bool bugset_bracelet;
            bool ptset_belt;
            bool ptset_boots;
            bool ptset_necklace;
            bool ptset_bracelet;
            bool ptset_ring;
            bool ksset_belt;
            bool ksset_boots;
            bool ksset_necklace;
            bool ksset_bracelet;
            bool ksset_ring;
            bool rubyset_belt;
            bool rubyset_boots;
            bool rubyset_necklace;
            bool rubyset_bracelet;
            bool rubyset_ring;
            bool strong_ptset_belt;
            bool strong_ptset_boots;
            bool strong_ptset_necklace;
            bool strong_ptset_bracelet;
            bool strong_ptset_ring;
            bool strong_ksset_belt;
            bool strong_ksset_boots;
            bool strong_ksset_necklace;
            bool strong_ksset_bracelet;
            bool strong_ksset_ring;
            bool strong_rubyset_belt;
            bool strong_rubyset_boots;
            bool strong_rubyset_necklace;
            bool strong_rubyset_bracelet;
            bool strong_rubyset_ring;
            bool dragonset_ring_left;
            bool dragonset_ring_right;
            bool dragonset_bracelet_left;
            bool dragonset_bracelet_right;
            bool dragonset_necklace;
            bool dragonset_dress;
            bool dragonset_helmet;
            bool dragonset_weapon;
            bool dragonset_boots;
            bool dragonset_belt;
            AddAbil = new TAddAbility();
            temp = WAbil;
            WAbil = Abil;
            WAbil.HP = temp.HP;
            WAbil.MP = temp.MP;
            WAbil.Weight = 0;
            WAbil.WearWeight = 0;
            WAbil.HandWeight = 0;
            AntiPoison = 0;
            PoisonRecover = 0;
            HealthRecover = 0;
            SpellRecover = 0;
            AntiMagic = 1;
            Luck = 0;
            HitSpeed = 0;
            oldhmode = BoHumHideMode;
            BoHumHideMode = false;
            BoAbilSpaceMove = false;
            BoAbilMakeStone = false;
            BoAbilRevival = false;
            BoAddMagicFireball = false;
            BoAddMagicHealing = false;
            BoAbilAngerEnergy = false;
            BoMagicShield = false;
            BoAbilSuperStrength = false;
            BoFastTraining = false;
            BoAbilSearch = false;
            ManaToHealthPoint = 0;
            mh_ring = false;
            mh_bracelet = false;
            mh_necklace = false;
            SuckupEnemyHealthRate = 0;
            SuckupEnemyHealth = 0;
            sh_ring = false;
            sh_bracelet = false;
            sh_necklace = false;
            hp_ring = false;
            hp_bracelet = false;
            mp_ring = false;
            mp_bracelet = false;
            hpmp_ring = false;
            hpmp_bracelet = false;
            hpp_necklace = false;
            hpp_bracelet = false;
            hpp_ring = false;
            cho_weapon = false;
            cho_necklace = false;
            cho_ring = false;
            cho_helmet = false;
            cho_bracelet = false;
            BoOldVersionUser_Italy = false;
            pset_necklace = false;
            pset_bracelet = false;
            pset_ring = false;
            hset_necklace = false;
            hset_bracelet = false;
            hset_ring = false;
            yset_necklace = false;
            yset_bracelet = false;
            yset_ring = false;
            boneset_weapon = false;
            boneset_helmet = false;
            boneset_dress = false;
            bugset_necklace = false;
            bugset_ring = false;
            bugset_bracelet = false;
            ptset_belt = false;
            ptset_boots = false;
            ptset_necklace = false;
            ptset_bracelet = false;
            ptset_ring = false;
            ksset_belt = false;
            ksset_boots = false;
            ksset_necklace = false;
            ksset_bracelet = false;
            ksset_ring = false;
            rubyset_belt = false;
            rubyset_boots = false;
            rubyset_necklace = false;
            rubyset_bracelet = false;
            rubyset_ring = false;
            strong_ptset_belt = false;
            strong_ptset_boots = false;
            strong_ptset_necklace = false;
            strong_ptset_bracelet = false;
            strong_ptset_ring = false;
            strong_ksset_belt = false;
            strong_ksset_boots = false;
            strong_ksset_necklace = false;
            strong_ksset_bracelet = false;
            strong_ksset_ring = false;
            strong_rubyset_belt = false;
            strong_rubyset_boots = false;
            strong_rubyset_necklace = false;
            strong_rubyset_bracelet = false;
            strong_rubyset_ring = false;
            dragonset_ring_left = false;
            dragonset_ring_right = false;
            dragonset_bracelet_left = false;
            dragonset_bracelet_right = false;
            dragonset_necklace = false;
            dragonset_dress = false;
            dragonset_helmet = false;
            dragonset_weapon = false;
            dragonset_boots = false;
            dragonset_belt = false;
            BoCGHIEnable = false;
            cghi[0] = false;
            cghi[1] = false;
            cghi[2] = false;
            cghi[3] = false;
            dset_wingdress = false;
            PlusFinalDamage = 0;
            LoverPlusAbility = false;
            if (RaceServer == Grobal2.RC_USERHUMAN)
            {
                for (i = 0; i <= 12; i++)
                {
                    if (UseItems[i].Index > 0)
                    {
                        if (UseItems[i].Dura == 0)
                        {
                            pstd = M2Share.UserEngine.GetStdItem(UseItems[i].Index);
                            if (pstd != null)
                            {
                                if ((i == Grobal2.U_WEAPON) || (i == Grobal2.U_RIGHTHAND))
                                {
                                    WAbil.HandWeight = (byte)(WAbil.HandWeight + pstd.Weight);
                                }
                                else
                                {
                                    WAbil.WearWeight = (byte)(WAbil.WearWeight + pstd.Weight);
                                }
                            }
                            continue;
                        }
                        ApplyItemParameters(UseItems[i], ref AddAbil);
                        ApplyItemParametersEx(UseItems[i], ref WAbil);
                        pstd = M2Share.UserEngine.GetStdItem(UseItems[i].Index);
                        if (pstd != null)
                        {
                            if ((i == Grobal2.U_WEAPON) || (i == Grobal2.U_RIGHTHAND))
                            {
                                WAbil.HandWeight = (byte)(WAbil.HandWeight + pstd.Weight);
                            }
                            else
                            {
                                WAbil.WearWeight = (byte)(WAbil.WearWeight + pstd.Weight);
                            }
                            if ((i == Grobal2.U_WEAPON) || (i == Grobal2.U_ARMRINGL) || (i == Grobal2.U_ARMRINGR))
                            {
                                if ((pstd.SpecialPwr <= -1) && (pstd.SpecialPwr >= -50))
                                {
                                    AddAbil.UndeadPower = (byte)(AddAbil.UndeadPower + (-pstd.SpecialPwr));
                                }
                                if ((pstd.SpecialPwr <= -51) && (pstd.SpecialPwr >= -100))
                                {
                                    AddAbil.UndeadPower = (byte)(AddAbil.UndeadPower + (pstd.SpecialPwr + 50));
                                }
                                if (pstd.Shape == ObjBase.CCHO_WEAPON)
                                {
                                    cho_weapon = true;
                                }
                                if ((pstd.Shape == ObjBase.BONESET_WEAPON_SHAPE) && (pstd.StdMode == 6))
                                {
                                    boneset_weapon = true;
                                }
                                if (pstd.Shape == ObjBase.DRAGON_WEAPON_SHAPE)
                                {
                                    dragonset_weapon = true;
                                }
                            }
                            if (i == Grobal2.U_NECKLACE)
                            {
                                if (pstd.Shape == ObjBase.NECTLACE_FASTTRAINING_ITEM)
                                {
                                    BoFastTraining = true;
                                }
                                if (pstd.Shape == ObjBase.NECTLACE_SEARCH_ITEM)
                                {
                                    BoAbilSearch = true;
                                }
                                if (pstd.Shape == ObjBase.NECKLACE_GI_ITEM)
                                {
                                    cghi[1] = true;
                                }
                                if (pstd.Shape == ObjBase.NECKLACE_OF_MANATOHEALTH)
                                {
                                    mh_necklace = true;
                                    ManaToHealthPoint = (short)(ManaToHealthPoint + pstd.AniCount);
                                }
                                if (pstd.Shape == ObjBase.NECKLACE_OF_SUCKHEALTH)
                                {
                                    sh_necklace = true;
                                    SuckupEnemyHealthRate = SuckupEnemyHealthRate + pstd.AniCount;
                                }
                                if (pstd.Shape == ObjBase.NECKLACE_OF_HPPUP)
                                {
                                    hpp_necklace = true;
                                }
                                if (pstd.Shape == ObjBase.CCHO_NECKLACE)
                                {
                                    cho_necklace = true;
                                }
                                if (pstd.Shape == ObjBase.PSET_NECKLACE_SHAPE)
                                {
                                    pset_necklace = true;
                                }
                                if (pstd.Shape == ObjBase.HSET_NECKLACE_SHAPE)
                                {
                                    hset_necklace = true;
                                }
                                if (pstd.Shape == ObjBase.YSET_NECKLACE_SHAPE)
                                {
                                    yset_necklace = true;
                                }
                                if (pstd.Shape == ObjBase.BUGSET_NECKLACE_SHAPE)
                                {
                                    bugset_necklace = true;
                                }
                                if (pstd.Shape == ObjBase.PTSET_NECKLACE_SHAPE)
                                {
                                    ptset_necklace = true;
                                }
                                if (pstd.Shape == ObjBase.KSSET_NECKLACE_SHAPE)
                                {
                                    ksset_necklace = true;
                                }
                                if (pstd.Shape == ObjBase.RUBYSET_NECKLACE_SHAPE)
                                {
                                    rubyset_necklace = true;
                                }
                                if (pstd.Shape == ObjBase.STRONG_PTSET_NECKLACE_SHAPE)
                                {
                                    strong_ptset_necklace = true;
                                }
                                if (pstd.Shape == ObjBase.STRONG_KSSET_NECKLACE_SHAPE)
                                {
                                    strong_ksset_necklace = true;
                                }
                                if (pstd.Shape == ObjBase.STRONG_RUBYSET_NECKLACE_SHAPE)
                                {
                                    strong_rubyset_necklace = true;
                                }
                                if (pstd.Shape == ObjBase.DRAGON_NECKLACE_SHAPE)
                                {
                                    dragonset_necklace = true;
                                }
                            }
                            if ((i == Grobal2.U_RINGR) || (i == Grobal2.U_RINGL))
                            {
                                if (pstd.Shape == ObjBase.RING_TRANSPARENT_ITEM)
                                {
                                    StatusArr[Grobal2.STATE_TRANSPARENT] = 60000;
                                    BoHumHideMode = true;
                                }
                                if (pstd.Shape == ObjBase.RING_SPACEMOVE_ITEM)
                                {
                                    BoAbilSpaceMove = true;
                                }
                                if (pstd.Shape == ObjBase.RING_MAKESTONE_ITEM)
                                {
                                    BoAbilMakeStone = true;
                                }
                                if (pstd.Shape == ObjBase.RING_REVIVAL_ITEM)
                                {
                                    BoAbilRevival = true;
                                }
                                if (pstd.Shape == ObjBase.RING_FIREBALL_ITEM)
                                {
                                    BoAddMagicFireball = true;
                                }
                                if (pstd.Shape == ObjBase.RING_HEALING_ITEM)
                                {
                                    BoAddMagicHealing = true;
                                }
                                if (pstd.Shape == ObjBase.RING_ANGERENERGY_ITEM)
                                {
                                    BoAbilAngerEnergy = true;
                                }
                                if (pstd.Shape == ObjBase.RING_MAGICSHIELD_ITEM)
                                {
                                    BoMagicShield = true;
                                }
                                if (pstd.Shape == ObjBase.RING_SUPERSTRENGTH_ITEM)
                                {
                                    BoAbilSuperStrength = true;
                                }
                                if (pstd.Shape == ObjBase.RING_CHUN_ITEM)
                                {
                                    // 玫瘤钦老 (玫)
                                    cghi[0] = true;
                                }
                                if (pstd.Shape == ObjBase.RING_OF_MANATOHEALTH)
                                {
                                    // 付仿 -> 眉仿
                                    mh_ring = true;
                                    ManaToHealthPoint = (short)(ManaToHealthPoint + pstd.AniCount);
                                }
                                if (pstd.Shape == ObjBase.RING_OF_SUCKHEALTH)
                                {
                                    // 惑措 眉仿 软荐
                                    sh_ring = true;
                                    SuckupEnemyHealthRate = SuckupEnemyHealthRate + pstd.AniCount;
                                }
                                // 2003/01/15 技飘 酒捞袍 眠啊...技符悸, 踌秒悸, 档何悸
                                if (pstd.Shape == ObjBase.RING_OF_HPUP)
                                {
                                    // HP刘啊
                                    hp_ring = true;
                                }
                                if (pstd.Shape == ObjBase.RING_OF_MPUP)
                                {
                                    // MP刘啊
                                    mp_ring = true;
                                }
                                if (pstd.Shape == ObjBase.RING_OF_HPMPUP)
                                {
                                    // HP/MP 刘啊
                                    hpmp_ring = true;
                                }
                                if (pstd.Shape == ObjBase.RING_OH_HPPUP)
                                {
                                    // HP PERCENT 刘啊
                                    hpp_ring = true;
                                }
                                if (pstd.Shape == ObjBase.CCHO_RING)
                                {
                                    cho_ring = true;
                                }
                                // 2003/03/04 技飘 酒捞袍 眠啊...颇尖悸, 券付籍悸, 康飞苛悸
                                if (pstd.Shape == ObjBase.PSET_RING_SHAPE)
                                {
                                    pset_ring = true;
                                }
                                if (pstd.Shape == ObjBase.HSET_RING_SHAPE)
                                {
                                    hset_ring = true;
                                }
                                if (pstd.Shape == ObjBase.YSET_RING_SHAPE)
                                {
                                    yset_ring = true;
                                }
                                // 2003/11/19 技飘 酒捞袍 眠啊(sonmg)
                                if (pstd.Shape == ObjBase.BUGSET_RING_SHAPE)
                                {
                                    bugset_ring = true;
                                }
                                if (pstd.Shape == ObjBase.PTSET_RING_SHAPE)
                                {
                                    ptset_ring = true;
                                }
                                if (pstd.Shape == ObjBase.KSSET_RING_SHAPE)
                                {
                                    ksset_ring = true;
                                }
                                if (pstd.Shape == ObjBase.RUBYSET_RING_SHAPE)
                                {
                                    rubyset_ring = true;
                                }
                                if (pstd.Shape == ObjBase.STRONG_PTSET_RING_SHAPE)
                                {
                                    strong_ptset_ring = true;
                                }
                                if (pstd.Shape == ObjBase.STRONG_KSSET_RING_SHAPE)
                                {
                                    strong_ksset_ring = true;
                                }
                                if (pstd.Shape == ObjBase.STRONG_RUBYSET_RING_SHAPE)
                                {
                                    strong_rubyset_ring = true;
                                }
                                // 2004/01/09 侩 技飘 酒捞袍 眠啊(sonmg)
                                if (pstd.Shape == ObjBase.DRAGON_RING_SHAPE)
                                {
                                    if (i == Grobal2.U_RINGL)
                                    {
                                        dragonset_ring_left = true;
                                    }
                                    if (i == Grobal2.U_RINGR)
                                    {
                                        dragonset_ring_right = true;
                                    }
                                }
                            }
                            // 迫骂
                            if ((i == Grobal2.U_ARMRINGL) || (i == Grobal2.U_ARMRINGR))
                            {
                                if (pstd.Shape == ObjBase.ARMRING_HAP_ITEM)
                                {
                                    // 玫瘤钦老 (钦)
                                    cghi[2] = true;
                                }
                                if (pstd.Shape == ObjBase.BRACELET_OF_MANATOHEALTH)
                                {
                                    // 付仿 -> 眉仿
                                    mh_bracelet = true;
                                    ManaToHealthPoint = (short)(ManaToHealthPoint + pstd.AniCount);
                                }
                                if (pstd.Shape == ObjBase.BRACELET_OF_SUCKHEALTH)
                                {
                                    // 惑措 眉仿 软荐
                                    sh_bracelet = true;
                                    SuckupEnemyHealthRate = SuckupEnemyHealthRate + pstd.AniCount;
                                }
                                // 2003/01/15 技飘 酒捞袍 眠啊...技符悸, 踌秒悸, 档何悸
                                if (pstd.Shape == ObjBase.BRACELET_OF_HPUP)
                                {
                                    // HP刘啊
                                    hp_bracelet = true;
                                }
                                if (pstd.Shape == ObjBase.BRACELET_OF_MPUP)
                                {
                                    // MP刘啊
                                    mp_bracelet = true;
                                }
                                if (pstd.Shape == ObjBase.BRACELET_OF_HPMPUP)
                                {
                                    // HP/MP刘啊
                                    hpmp_bracelet = true;
                                }
                                if (pstd.Shape == ObjBase.BRACELET_OF_HPPUP)
                                {
                                    // HP PERCENT 刘啊
                                    hpp_bracelet = true;
                                }
                                if (pstd.Shape == ObjBase.CCHO_BRACELET)
                                {
                                    cho_bracelet = true;
                                }
                                // 2003/03/04 技飘 酒捞袍 眠啊...颇尖悸, 券付籍悸, 康飞苛悸
                                if (pstd.Shape == ObjBase.PSET_BRACELET_SHAPE)
                                {
                                    pset_bracelet = true;
                                }
                                if (pstd.Shape == ObjBase.HSET_BRACELET_SHAPE)
                                {
                                    hset_bracelet = true;
                                }
                                if (pstd.Shape == ObjBase.YSET_BRACELET_SHAPE)
                                {
                                    yset_bracelet = true;
                                }
                                // 2003/11/19 技飘 酒捞袍 眠啊(sonmg)
                                if (pstd.Shape == ObjBase.BUGSET_BRACELET_SHAPE)
                                {
                                    bugset_bracelet = true;
                                }
                                if (pstd.Shape == ObjBase.PTSET_BRACELET_SHAPE)
                                {
                                    ptset_bracelet = true;
                                }
                                if (pstd.Shape == ObjBase.KSSET_BRACELET_SHAPE)
                                {
                                    ksset_bracelet = true;
                                }
                                if (pstd.Shape == ObjBase.RUBYSET_BRACELET_SHAPE)
                                {
                                    rubyset_bracelet = true;
                                }
                                if (pstd.Shape == ObjBase.STRONG_PTSET_BRACELET_SHAPE)
                                {
                                    strong_ptset_bracelet = true;
                                }
                                if (pstd.Shape == ObjBase.STRONG_KSSET_BRACELET_SHAPE)
                                {
                                    strong_ksset_bracelet = true;
                                }
                                if (pstd.Shape == ObjBase.STRONG_RUBYSET_BRACELET_SHAPE)
                                {
                                    strong_rubyset_bracelet = true;
                                }
                                // 2004/01/09 侩 技飘 酒捞袍 眠啊(sonmg)
                                if (pstd.Shape == ObjBase.DRAGON_BRACELET_SHAPE)
                                {
                                    if (i == Grobal2.U_ARMRINGL)
                                    {
                                        dragonset_bracelet_left = true;
                                    }
                                    if (i == Grobal2.U_ARMRINGR)
                                    {
                                        dragonset_bracelet_right = true;
                                    }
                                }
                            }
                            // 捧备
                            if (i == Grobal2.U_HELMET)
                            {
                                if (pstd.Shape == ObjBase.HELMET_IL_ITEM)
                                {
                                    cghi[3] = true;
                                }
                                if (pstd.Shape == ObjBase.CCHO_HELMET)
                                {
                                    cho_helmet = true;
                                }
                                // 2003/11/19 技飘 酒捞袍 眠啊(sonmg)
                                if (pstd.Shape == ObjBase.BONESET_HELMET_SHAPE)
                                {
                                    boneset_helmet = true;
                                }
                                // 2004/01/09 侩 技飘 酒捞袍 眠啊(sonmg)
                                if (pstd.Shape == ObjBase.DRAGON_HELMET_SHAPE)
                                {
                                    dragonset_helmet = true;
                                }
                            }
                            // 渴
                            if (i == Grobal2.U_DRESS)
                            {
                                if (pstd.Shape == ObjBase.DRESS_SHAPE_WING)
                                {
                                    dset_wingdress = true;
                                }
                                // 2003/11/19 技飘 酒捞袍 眠啊(sonmg)
                                // 喊档 备盒 鞘夸
                                if ((pstd.Shape == ObjBase.BONESET_DRESS_SHAPE) && ((pstd.Name.ToUpper() == "龙骨甲(男)") || (pstd.Name.ToUpper() == "龙骨甲(女)")))
                                {
                                    boneset_dress = true;
                                }
                                // 2004/01/09 侩 技飘 酒捞袍 眠啊(sonmg)
                                if (pstd.Shape == ObjBase.DRAGON_DRESS_SHAPE)
                                {
                                    dragonset_dress = true;
                                }
                            }
                            // 骇飘(sonmg)
                            if (i == Grobal2.U_BELT)
                            {
                                // 2003/11/19 技飘 酒捞袍 眠啊(sonmg)
                                if (pstd.Shape == ObjBase.PTSET_BELT_SHAPE)
                                {
                                    ptset_belt = true;
                                }
                                if (pstd.Shape == ObjBase.KSSET_BELT_SHAPE)
                                {
                                    ksset_belt = true;
                                }
                                if (pstd.Shape == ObjBase.RUBYSET_BELT_SHAPE)
                                {
                                    rubyset_belt = true;
                                }
                                if (pstd.Shape == ObjBase.STRONG_PTSET_BELT_SHAPE)
                                {
                                    strong_ptset_belt = true;
                                }
                                if (pstd.Shape == ObjBase.STRONG_KSSET_BELT_SHAPE)
                                {
                                    strong_ksset_belt = true;
                                }
                                if (pstd.Shape == ObjBase.STRONG_RUBYSET_BELT_SHAPE)
                                {
                                    strong_rubyset_belt = true;
                                }
                                // 2004/01/09 侩 技飘 酒捞袍 眠啊(sonmg)
                                if (pstd.Shape == ObjBase.DRAGON_BELT_SHAPE)
                                {
                                    dragonset_belt = true;
                                }
                            }
                            // 脚惯(sonmg)
                            if (i == Grobal2.U_BOOTS)
                            {
                                // 2003/11/19 技飘 酒捞袍 眠啊(sonmg)
                                if (pstd.Shape == ObjBase.PTSET_BOOTS_SHAPE)
                                {
                                    ptset_boots = true;
                                }
                                if (pstd.Shape == ObjBase.KSSET_BOOTS_SHAPE)
                                {
                                    ksset_boots = true;
                                }
                                if (pstd.Shape == ObjBase.RUBYSET_BOOTS_SHAPE)
                                {
                                    rubyset_boots = true;
                                }
                                if (pstd.Shape == ObjBase.STRONG_PTSET_BOOTS_SHAPE)
                                {
                                    strong_ptset_boots = true;
                                }
                                if (pstd.Shape == ObjBase.STRONG_KSSET_BOOTS_SHAPE)
                                {
                                    strong_ksset_boots = true;
                                }
                                if (pstd.Shape == ObjBase.STRONG_RUBYSET_BOOTS_SHAPE)
                                {
                                    strong_rubyset_boots = true;
                                }
                                if (pstd.Shape == ObjBase.DRAGON_BOOTS_SHAPE)
                                {
                                    dragonset_boots = true;
                                }
                            }
                            if (i == Grobal2.U_CHARM)
                            {
                                if ((pstd.StdMode == 53) && (pstd.Shape == ObjBase.SHAPE_OF_LUCKYLADLE))
                                {
                                    AddAbil.Luck = (byte)_MIN(255, AddAbil.Luck + 1);
                                }
                            }
                        }
                    }
                }
                if (cghi[0] && cghi[1] && cghi[2] && cghi[3])
                {
                    BoCGHIEnable = true;
                }
                if (mh_necklace && mh_bracelet && mh_ring)
                {
                    ManaToHealthPoint = (short)(ManaToHealthPoint + 50);
                }
                if (sh_necklace && sh_bracelet && sh_ring)
                {
                    AddAbil.HIT = (byte)(AddAbil.HIT + 2);
                }
                if (hp_bracelet && hp_ring)
                {
                    AddAbil.HP = (byte)(AddAbil.HP + 50);
                }
                if (mp_bracelet && mp_ring)
                {
                    AddAbil.MP = (byte)(AddAbil.MP + 50);
                }
                if (hpmp_bracelet && hpmp_ring)
                {
                    AddAbil.HP = (byte)(AddAbil.HP + 30);
                    AddAbil.MP = (byte)(AddAbil.MP + 30);
                }
                if (hpp_necklace && hpp_bracelet && hpp_ring)
                {
                    AddAbil.HP = (byte)(AddAbil.HP + (WAbil.MaxHP * 30 / 100));
                    AddAbil.AC = (short)(AddAbil.AC + HUtil32.MakeWord(2, 2));
                }
                if (cho_weapon && cho_necklace && cho_ring && cho_helmet && cho_bracelet)
                {
                    AddAbil.HitSpeed = (byte)(AddAbil.HitSpeed + 4);
                    AddAbil.DC = (byte)(AddAbil.DC + MakeWord(2, 5));
                    BoOldVersionUser_Italy = true;
                }
                if (pset_bracelet && pset_ring)
                {
                    AddAbil.HitSpeed = (byte)(AddAbil.HitSpeed + 2);
                    if (pset_necklace)
                    {
                        AddAbil.DC = (byte)(AddAbil.DC + MakeWord(1, 3));
                    }
                }
                if (hset_bracelet && hset_ring)
                {
                    WAbil.MaxWeight = (byte)(WAbil.MaxWeight + 20);
                    WAbil.MaxWearWeight = (byte)_MIN(255, WAbil.MaxWearWeight + 5);
                    if (hset_necklace)
                    {
                        AddAbil.MC = (byte)(AddAbil.MC + MakeWord(1, 2));
                    }
                }
                if (yset_bracelet && yset_ring)
                {
                    AddAbil.UndeadPower = (byte)(AddAbil.UndeadPower + 3);
                    if (yset_necklace)
                    {
                        AddAbil.SC = (byte)(AddAbil.SC + MakeWord(1, 2));
                    }
                }
                if (boneset_weapon && boneset_helmet && boneset_dress)
                {
                    AddAbil.AC = (byte)(AddAbil.AC + MakeWord(0, 2));
                    AddAbil.MC = (byte)(AddAbil.MC + MakeWord(0, 1));
                    AddAbil.SC = (byte)(AddAbil.SC + MakeWord(0, 1));
                }
                if (bugset_necklace && bugset_ring && bugset_bracelet)
                {
                    AddAbil.DC = (byte)(AddAbil.DC + MakeWord(0, 1));
                    AddAbil.MC = (byte)(AddAbil.MC + MakeWord(0, 1));
                    AddAbil.SC = (byte)(AddAbil.SC + MakeWord(0, 1));
                    AddAbil.AntiMagic = (byte)(AddAbil.AntiMagic + 1);
                    AddAbil.AntiPoison = (byte)(AddAbil.AntiPoison + 1);
                }
                if (ptset_belt && ptset_boots && ptset_necklace && ptset_bracelet && ptset_ring)
                {
                    AddAbil.DC = (byte)(AddAbil.DC + MakeWord(0, 2));
                    AddAbil.AC = (byte)(AddAbil.AC + MakeWord(0, 2));
                    WAbil.MaxHandWeight = (byte)_MIN(255, WAbil.MaxHandWeight + 1);
                    WAbil.MaxWearWeight = (byte)_MIN(255, WAbil.MaxWearWeight + 2);
                }
                if (ksset_belt && ksset_boots && ksset_necklace && ksset_bracelet && ksset_ring)
                {
                    AddAbil.SC = (byte)(AddAbil.SC + MakeWord(0, 2));
                    AddAbil.AC = (byte)(AddAbil.AC + MakeWord(0, 1));
                    AddAbil.MAC = (byte)(AddAbil.MAC + MakeWord(0, 1));
                    AddAbil.SPEED = (byte)(AddAbil.SPEED + 1);
                    WAbil.MaxHandWeight = (byte)_MIN(255, WAbil.MaxHandWeight + 1);
                    WAbil.MaxWearWeight = (byte)_MIN(255, WAbil.MaxWearWeight + 2);
                }
                if (rubyset_belt && rubyset_boots && rubyset_necklace && rubyset_bracelet && rubyset_ring)
                {
                    AddAbil.MC = (byte)(AddAbil.MC + MakeWord(0, 2));
                    AddAbil.MAC = (byte)(AddAbil.MAC + MakeWord(0, 2));
                    WAbil.MaxHandWeight = (byte)_MIN(255, WAbil.MaxHandWeight + 1);
                    WAbil.MaxWearWeight = (byte)_MIN(255, WAbil.MaxWearWeight + 2);
                }
                if (strong_ptset_belt && strong_ptset_boots && strong_ptset_necklace && strong_ptset_bracelet && strong_ptset_ring)
                {
                    AddAbil.DC = (byte)(AddAbil.DC + MakeWord(0, 3));
                    AddAbil.HP = (byte)(AddAbil.HP + 30);
                    AddAbil.HitSpeed = (byte)(AddAbil.HitSpeed + 2);
                    WAbil.MaxWearWeight = (byte)_MIN(255, WAbil.MaxWearWeight + 2);
                }
                if (strong_ksset_belt && strong_ksset_boots && strong_ksset_necklace && strong_ksset_bracelet && strong_ksset_ring)
                {
                    AddAbil.SC = (byte)(AddAbil.SC + MakeWord(0, 2));
                    AddAbil.HP = (byte)(AddAbil.HP + 15);
                    AddAbil.MP = (byte)(AddAbil.MP + 20);
                    AddAbil.UndeadPower = (byte)(AddAbil.UndeadPower + 1);
                    AddAbil.HIT = (byte)(AddAbil.HIT + 1);
                    AddAbil.SPEED = (byte)(AddAbil.SPEED + 1);
                    WAbil.MaxWearWeight = (byte)_MIN(255, WAbil.MaxWearWeight + 2);
                }
                if (strong_rubyset_belt && strong_rubyset_boots && strong_rubyset_necklace && strong_rubyset_bracelet && strong_rubyset_ring)
                {
                    AddAbil.MC = (byte)(AddAbil.MC + MakeWord(0, 2));
                    AddAbil.MP = (byte)(AddAbil.MP + 40);
                    AddAbil.SPEED = (byte)(AddAbil.SPEED + 2);
                    WAbil.MaxWearWeight = (byte)_MIN(255, WAbil.MaxWearWeight + 2);
                }
                if (dragonset_ring_left && dragonset_ring_right && dragonset_bracelet_left && dragonset_bracelet_right && dragonset_necklace && dragonset_dress && dragonset_helmet && dragonset_weapon && dragonset_boots && dragonset_belt)
                {
                    AddAbil.AC = MakeWord(LoByte(AddAbil.AC) + 1, _MIN(255, HiByte(AddAbil.AC) + 4));
                    AddAbil.MAC = MakeWord(LoByte(AddAbil.MAC) + 1, _MIN(255, HiByte(AddAbil.MAC) + 4));
                    AddAbil.Luck = (byte)_MIN(255, AddAbil.Luck + 2);
                    AddAbil.HitSpeed = (byte)(AddAbil.HitSpeed + 2);
                    AddAbil.AntiMagic = (byte)(AddAbil.AntiMagic + 6);
                    AddAbil.AntiPoison = (byte)(AddAbil.AntiPoison + 6);
                    WAbil.MaxHandWeight = (byte)_MIN(255, WAbil.MaxHandWeight + 34);
                    WAbil.MaxWearWeight = (byte)_MIN(255, WAbil.MaxWearWeight + 27);
                    WAbil.MaxWeight = (byte)(WAbil.MaxWeight + 120);
                    WAbil.MaxHP = (byte)(WAbil.MaxHP + 70);
                    WAbil.MaxMP = (byte)(WAbil.MaxMP + 80);
                    AddAbil.SPEED = (byte)(AddAbil.SPEED + 1);
                    AddAbil.DC = MakeWord(LoByte(AddAbil.DC) + 1, _MIN(255, HiByte(AddAbil.DC) + 4));
                    AddAbil.MC = MakeWord(LoByte(AddAbil.MC) + 1, _MIN(255, HiByte(AddAbil.MC) + 3));
                    AddAbil.SC = MakeWord(LoByte(AddAbil.SC) + 1, _MIN(255, HiByte(AddAbil.SC) + 3));
                }
                else
                {
                    if (dragonset_dress && dragonset_helmet && dragonset_weapon && dragonset_boots && dragonset_belt)
                    {
                        WAbil.MaxHandWeight = (byte)_MIN(255, WAbil.MaxHandWeight + 34);
                        WAbil.MaxWeight = (byte)(WAbil.MaxWeight + 50);
                        AddAbil.SPEED = (byte)(AddAbil.SPEED + 1);
                        AddAbil.DC = MakeWord(LoByte(AddAbil.DC) + 1, _MIN(255, HiByte(AddAbil.DC) + 4));
                        AddAbil.MC = MakeWord(LoByte(AddAbil.MC) + 1, _MIN(255, HiByte(AddAbil.MC) + 3));
                        AddAbil.SC = MakeWord(LoByte(AddAbil.SC) + 1, _MIN(255, HiByte(AddAbil.SC) + 3));
                    }
                    else if (dragonset_dress && dragonset_boots && dragonset_belt)
                    {
                        WAbil.MaxHandWeight = (byte)_MIN(255, WAbil.MaxHandWeight + 17);
                        WAbil.MaxWeight = (byte)(WAbil.MaxWeight + 30);
                        AddAbil.DC = MakeWord(LoByte(AddAbil.DC), _MIN(255, HiByte(AddAbil.DC) + 1));
                        AddAbil.MC = MakeWord(LoByte(AddAbil.MC), _MIN(255, HiByte(AddAbil.MC) + 1));
                        AddAbil.SC = MakeWord(LoByte(AddAbil.SC), _MIN(255, HiByte(AddAbil.SC) + 1));
                    }
                    else if (dragonset_dress && dragonset_helmet && dragonset_weapon)
                    {
                        AddAbil.DC = MakeWord(LoByte(AddAbil.DC), _MIN(255, HiByte(AddAbil.DC) + 2));
                        AddAbil.MC = MakeWord(LoByte(AddAbil.MC), _MIN(255, HiByte(AddAbil.MC) + 1));
                        AddAbil.SC = MakeWord(LoByte(AddAbil.SC), _MIN(255, HiByte(AddAbil.SC) + 1));
                        AddAbil.SPEED = (byte)(AddAbil.SPEED + 1);
                    }
                    if (dragonset_ring_left && dragonset_ring_right && dragonset_bracelet_left && dragonset_bracelet_right && dragonset_necklace)
                    {
                        WAbil.MaxWearWeight = (byte)_MIN(255, WAbil.MaxWearWeight + 27);
                        WAbil.MaxWeight = (byte)(WAbil.MaxWeight + 50);
                        AddAbil.AC = MakeWord(LoByte(AddAbil.AC) + 1, _MIN(255, HiByte(AddAbil.AC) + 3));
                        AddAbil.MAC = MakeWord(LoByte(AddAbil.MAC) + 1, _MIN(255, HiByte(AddAbil.MAC) + 3));
                    }
                    else if ((dragonset_ring_left || dragonset_ring_right) && dragonset_bracelet_left && dragonset_bracelet_right && dragonset_necklace)
                    {
                        WAbil.MaxWearWeight = (byte)_MIN(255, WAbil.MaxWearWeight + 17);
                        WAbil.MaxWeight = (byte)(WAbil.MaxWeight + 30);
                        AddAbil.AC = MakeWord(LoByte(AddAbil.AC) + 1, _MIN(255, HiByte(AddAbil.AC) + 1));
                        AddAbil.MAC = MakeWord(LoByte(AddAbil.MAC) + 1, _MIN(255, HiByte(AddAbil.MAC) + 1));
                    }
                    else if (dragonset_ring_left && dragonset_ring_right && (dragonset_bracelet_left || dragonset_bracelet_right) && dragonset_necklace)
                    {
                        WAbil.MaxWearWeight = (byte)_MIN(255, WAbil.MaxWearWeight + 17);
                        WAbil.MaxWeight = (byte)(WAbil.MaxWeight + 30);
                        AddAbil.AC = MakeWord(LoByte(AddAbil.AC), _MIN(255, HiByte(AddAbil.AC) + 2));
                        AddAbil.MAC = MakeWord(LoByte(AddAbil.MAC), _MIN(255, HiByte(AddAbil.MAC) + 2));
                    }
                    else if ((dragonset_ring_left || dragonset_ring_right) && (dragonset_bracelet_left || dragonset_bracelet_right) && dragonset_necklace)
                    {
                        WAbil.MaxWearWeight = (byte)_MIN(255, WAbil.MaxWearWeight + 17);
                        WAbil.MaxWeight = (byte)(WAbil.MaxWeight + 30);
                        AddAbil.AC = MakeWord(LoByte(AddAbil.AC), _MIN(255, HiByte(AddAbil.AC) + 1));
                        AddAbil.MAC = MakeWord(LoByte(AddAbil.MAC), _MIN(255, HiByte(AddAbil.MAC) + 1));
                    }
                    else
                    {
                        if (dragonset_bracelet_left && dragonset_bracelet_right)
                        {
                            AddAbil.AC = MakeWord(LoByte(AddAbil.AC) + 1, _MIN(255, HiByte(AddAbil.AC)));
                            AddAbil.MAC = MakeWord(LoByte(AddAbil.MAC) + 1, _MIN(255, HiByte(AddAbil.MAC)));
                        }
                        if (dragonset_ring_left && dragonset_ring_right)
                        {
                            AddAbil.AC = MakeWord(LoByte(AddAbil.AC), _MIN(255, HiByte(AddAbil.AC) + 1));
                            AddAbil.MAC = MakeWord(LoByte(AddAbil.MAC), _MIN(255, HiByte(AddAbil.MAC) + 1));
                        }
                    }
                }
                if (dset_wingdress && (Abil.Level >= 20))
                {
                    if (Abil.Level < 30)
                    {
                        // 扁夯蔼栏肺 汲沥
                    }
                    else if (Abil.Level < 40)
                    {
                        AddAbil.DC = (byte)(AddAbil.DC + MakeWord(0, 1));
                        AddAbil.MC = (byte)(AddAbil.MC + MakeWord(0, 2));
                        AddAbil.SC = (byte)(AddAbil.SC + MakeWord(0, 2));
                        AddAbil.AC = (byte)(AddAbil.AC + MakeWord(2, 3));
                        AddAbil.MAC = (byte)(AddAbil.MAC + MakeWord(0, 2));
                    }
                    else if (Abil.Level < 50)
                    {
                        AddAbil.DC = (byte)(AddAbil.DC + MakeWord(0, 3));
                        AddAbil.MC = (byte)(AddAbil.MC + MakeWord(0, 4));
                        AddAbil.SC = (byte)(AddAbil.SC + MakeWord(0, 4));
                        AddAbil.AC = (byte)(AddAbil.AC + MakeWord(5, 5));
                        AddAbil.MAC = (byte)(AddAbil.MAC + MakeWord(1, 2));
                    }
                    else
                    {
                        AddAbil.DC = (byte)(AddAbil.DC + MakeWord(0, 5));
                        AddAbil.MC = (byte)(AddAbil.MC + MakeWord(0, 6));
                        AddAbil.SC = (byte)(AddAbil.SC + MakeWord(0, 6));
                        AddAbil.AC = (byte)(AddAbil.AC + MakeWord(9, 7));
                        AddAbil.MAC = (byte)(AddAbil.MAC + MakeWord(2, 4));
                    }
                }
                WAbil.Weight = (short)CalcBagWeight();
            }
            if (BoFixedHideMode && (StatusArr[Grobal2.STATE_TRANSPARENT] > 0))
            {
                BoHumHideMode = true;
            }
            if (BoHumHideMode)
            {
                if (!oldhmode)
                {
                    CharStatus = GetCharStatus();
                    CharStatusChanged();
                }
            }
            else
            {
                if (oldhmode)
                {
                    StatusArr[Grobal2.STATE_TRANSPARENT] = 0;
                    CharStatus = GetCharStatus();
                    CharStatusChanged();
                }
            }
            RecalcHitSpeed();
            if (AddAbil.HitSpeed >= 0)
            {
                AddAbil.HitSpeed = (short)((short)(int)AddAbil.HitSpeed / 2);
            }
            else
            {
                AddAbil.HitSpeed = (short)((short)(AddAbil.HitSpeed - 1) / 2);
            }
            AddAbil.HitSpeed = (short)_MIN(15, AddAbil.HitSpeed);
            oldlight = Light;
            Light = GetMyLight();
            if (oldlight != Light)
            {
                SendRefMsg(Grobal2.RM_CHANGELIGHT, 0, 0, 0, 0, "");
            }
            SpeedPoint = (byte)(SpeedPoint + AddAbil.SPEED);
            AccuracyPoint = (byte)(AccuracyPoint + AddAbil.HIT);
            AntiPoison = (byte)(AntiPoison + AddAbil.AntiPoison);
            PoisonRecover = (byte)(PoisonRecover + AddAbil.PoisonRecover);
            HealthRecover = (byte)(HealthRecover + AddAbil.HealthRecover);
            SpellRecover = (byte)(SpellRecover + AddAbil.SpellRecover);
            AntiMagic = (byte)(AntiMagic + AddAbil.AntiMagic);
            Luck = Luck + AddAbil.Luck;
            Luck = Luck - AddAbil.UnLuck;
            HitSpeed = AddAbil.HitSpeed;
            WAbil.MaxHP = (short)(Abil.MaxHP + AddAbil.HP);
            WAbil.MaxMP = (short)(Abil.MaxMP + AddAbil.MP);
            WAbil.AC = MakeWord(_MIN(255, LoByte(AddAbil.AC) + LoByte(Abil.AC)), _MIN(255, HiByte(AddAbil.AC) + HiByte(Abil.AC)));
            WAbil.MAC = MakeWord(_MIN(255, LoByte(AddAbil.MAC) + LoByte(Abil.MAC)), _MIN(255, HiByte(AddAbil.MAC) + HiByte(Abil.MAC)));
            WAbil.DC = MakeWord(_MIN(255, LoByte(AddAbil.DC) + LoByte(Abil.DC)), _MIN(255, HiByte(AddAbil.DC) + HiByte(Abil.DC)));
            WAbil.MC = MakeWord(_MIN(255, LoByte(AddAbil.MC) + LoByte(Abil.MC)), _MIN(255, HiByte(AddAbil.MC) + HiByte(Abil.MC)));
            WAbil.SC = MakeWord(_MIN(255, LoByte(AddAbil.SC) + LoByte(Abil.SC)), _MIN(255, HiByte(AddAbil.SC) + HiByte(Abil.SC)));
            if (StatusArr[Grobal2.STATE_DEFENCEUP] > 0)
            {
                WAbil.AC = MakeWord(LoByte(WAbil.AC), _MIN(255, HiByte(WAbil.AC) + (Abil.Level / 7) + StatusValue[Grobal2.STATE_DEFENCEUP]));
            }
            if (StatusArr[Grobal2.STATE_MAGDEFENCEUP] > 0)
            {
                WAbil.MAC = MakeWord(LoByte(WAbil.MAC), _MIN(255, HiByte(WAbil.MAC) + (Abil.Level / 7) + StatusValue[Grobal2.STATE_MAGDEFENCEUP]));
            }
            if (ExtraAbil[Grobal2.EABIL_DCUP] > 0)
            {
                WAbil.DC = MakeWord(LoByte(WAbil.DC), HiByte(WAbil.DC) + ExtraAbil[Grobal2.EABIL_DCUP]);
            }
            if (ExtraAbil[Grobal2.EABIL_MCUP] > 0)
            {
                WAbil.MC = MakeWord(LoByte(WAbil.MC), HiByte(WAbil.MC) + ExtraAbil[Grobal2.EABIL_MCUP]);
            }
            if (ExtraAbil[Grobal2.EABIL_SCUP] > 0)
            {
                WAbil.SC = MakeWord(LoByte(WAbil.SC), HiByte(WAbil.SC) + ExtraAbil[Grobal2.EABIL_SCUP]);
            }
            if (ExtraAbil[Grobal2.EABIL_HITSPEEDUP] > 0)
            {
                HitSpeed = (short)(HitSpeed + ExtraAbil[Grobal2.EABIL_HITSPEEDUP]);
            }
            if (ExtraAbil[Grobal2.EABIL_HPUP] > 0)
            {
                WAbil.MaxHP = (short)(WAbil.MaxHP + ExtraAbil[Grobal2.EABIL_HPUP]);
            }
            if (ExtraAbil[Grobal2.EABIL_MPUP] > 0)
            {
                WAbil.MaxMP = (short)(WAbil.MaxMP + ExtraAbil[Grobal2.EABIL_MPUP]);
            }
            if (ExtraAbil[Grobal2.EABIL_PWRRATE] > 0)
            {
                WAbil.DC = MakeWord(LoByte(WAbil.DC) * ExtraAbil[Grobal2.EABIL_PWRRATE] / 100, HiByte(WAbil.DC) * ExtraAbil[Grobal2.EABIL_PWRRATE] / 100);
                WAbil.MC = MakeWord(LoByte(WAbil.MC) * ExtraAbil[Grobal2.EABIL_PWRRATE] / 100, HiByte(WAbil.MC) * ExtraAbil[Grobal2.EABIL_PWRRATE] / 100);
                WAbil.SC = MakeWord(LoByte(WAbil.SC) * ExtraAbil[Grobal2.EABIL_PWRRATE] / 100, HiByte(WAbil.SC) * ExtraAbil[Grobal2.EABIL_PWRRATE] / 100);
            }
            if (BoAddMagicFireball)
            {
                AddMagicWithItem(ObjBase.AM_FIREBALL);
            }
            else
            {
                DelMagicWithItem(ObjBase.AM_FIREBALL);
            }
            if (BoAddMagicHealing)
            {
                AddMagicWithItem(ObjBase.AM_HEALING);
            }
            else
            {
                DelMagicWithItem(ObjBase.AM_HEALING);
            }
            if (BoAbilSuperStrength)
            {
                WAbil.MaxWeight = (ushort)(WAbil.MaxWeight * 2);
                WAbil.MaxWearWeight = (byte)_MIN(255, WAbil.MaxWearWeight * 2);
                if (WAbil.MaxHandWeight * 2 > 255)
                {
                    WAbil.MaxHandWeight = 255;
                }
                else
                {
                    WAbil.MaxHandWeight = (byte)(WAbil.MaxHandWeight * 2);
                }
            }
            if (ManaToHealthPoint > 0)
            {
                if (ManaToHealthPoint >= WAbil.MaxMP)
                {
                    ManaToHealthPoint = (short)(WAbil.MaxMP - 1);
                }
                WAbil.MaxMP = (short)(WAbil.MaxMP - ManaToHealthPoint);
                WAbil.MaxHP = (short)(WAbil.MaxHP + ManaToHealthPoint);
                if ((RaceServer == Grobal2.RC_USERHUMAN) && (WAbil.HP > WAbil.MaxHP))
                {
                    WAbil.HP = WAbil.MaxHP;
                }
            }
            if ((RaceServer == Grobal2.RC_USERHUMAN) && (WAbil.HP > WAbil.MaxHP) && !mh_necklace && !mh_bracelet && !mh_ring)
            {
                WAbil.HP = WAbil.MaxHP;
            }
            if ((RaceServer == Grobal2.RC_USERHUMAN) && (WAbil.MP > WAbil.MaxMP))
            {
                WAbil.MP = WAbil.MaxMP;
            }
            if (RaceServer == Grobal2.RC_USERHUMAN)
            {
                fastmoveflag = false;
                if ((UseItems[Grobal2.U_BOOTS].Dura > 0) && (UseItems[Grobal2.U_BOOTS].Index == M2Share.INDEX_MIRBOOTS))
                {
                    fastmoveflag = true;
                }
                if (fastmoveflag)
                {
                    StatusArr[Grobal2.STATE_FASTMOVE] = 60000;
                }
                else
                {
                    StatusArr[Grobal2.STATE_FASTMOVE] = 0;
                }
                if (Abil.Level >= ObjBase.EFFECTIVE_HIGHLEVEL)
                {
                    if (BoHighLevelEffect)
                    {
                        StatusArr[Grobal2.STATE_50LEVELEFFECT] = 60000;
                    }
                    else
                    {
                        StatusArr[Grobal2.STATE_50LEVELEFFECT] = 0;
                    }
                }
                else
                {
                    StatusArr[Grobal2.STATE_50LEVELEFFECT] = 0;
                }
                CharStatus = GetCharStatus();
                CharStatusChanged();
            }
            if (RaceServer == Grobal2.RC_USERHUMAN)
            {
                UpdateMsg(this, Grobal2.RM_CHARSTATUSCHANGED, HitSpeed, CharStatus, 0, 0, "");
            }
            if (RaceServer >= Grobal2.RC_ANIMAL)
            {
                ApplySlaveLevelAbilitys();
            }
        }

        public bool IsGroupGenderDiffernt(TCreature cret)
        {
            bool result;
            TCreature hum1;
            TCreature hum2;
            result = false;
            if ((cret != null) && (cret.GroupMembers.Count == 2))
            {
                hum1 = cret.GroupMembers[0];
                hum2 = cret.GroupMembers[1];
                if ((hum1 != null) && (hum2 != null) && (hum1.RaceServer == Grobal2.RC_USERHUMAN) && (hum2.RaceServer == Grobal2.RC_USERHUMAN))
                {
                    if (hum1.Sex != hum2.Sex)
                    {
                        result = true;
                    }
                }
            }
            return result;
        }

        public void ApplyItemParameters(TUserItem uitem, ref TAddAbility aabil)
        {
            TStdItem std;
            TStdItem ps = M2Share.UserEngine.GetStdItem(uitem.Index);
            if (ps != null)
            {
                std = ps;
                M2Share.ItemMan.GetUpgradeStdItem(uitem, ref std);
                ApplyItemParametersByJob(uitem, ref std);
                switch (ps.StdMode)
                {
                    case 5:
                    case 6:
                        aabil.HIT = (byte)(aabil.HIT + HiByte(std.AC));
                        aabil.HitSpeed = (byte)(aabil.HitSpeed + M2Share.ItemMan.RealAttackSpeed(HiByte(std.MAC)));
                        aabil.Luck = (byte)(aabil.Luck + LoByte(std.AC));
                        aabil.UnLuck = (byte)(aabil.UnLuck + LoByte(std.MAC));
                        aabil.Slowdown = (byte)(aabil.Slowdown + std.Slowdown);
                        aabil.Poison = (byte)(aabil.Poison + std.Tox);
                        if (std.SpecialPwr >= 1 && std.SpecialPwr <= 10)
                        {
                            aabil.WeaponStrong = (byte)std.SpecialPwr;
                        }
                        break;
                    case 10:
                    case 11:
                        aabil.AC = MakeWord(LoByte(aabil.AC) + LoByte(std.AC), HiByte(aabil.AC) + HiByte(std.AC));
                        aabil.MAC = MakeWord(LoByte(aabil.MAC) + LoByte(std.MAC), HiByte(aabil.MAC) + HiByte(std.MAC));
                        aabil.SPEED = (short)(aabil.SPEED + std.Agility);
                        aabil.AntiMagic = (ushort)(aabil.AntiMagic + std.MgAvoid);
                        aabil.AntiPoison = (short)(aabil.AntiPoison + std.ToxAvoid);
                        aabil.HP = (short)(aabil.HP + std.HpAdd);
                        aabil.MP = (short)(aabil.MP + std.MpAdd);
                        if (std.EffType1 > 0)
                        {
                            switch (std.EffType1)
                            {
                                case Grobal2.EFFTYPE_HP_MP_ADD:
                                    if (aabil.HealthRecover + std.EffRate1 > 65000)
                                    {
                                        aabil.HealthRecover = 65000;
                                    }
                                    else
                                    {
                                        aabil.HealthRecover = (ushort)(aabil.HealthRecover + std.EffRate1);
                                    }
                                    if (aabil.SpellRecover + std.EffValue1 > 65000)
                                    {
                                        aabil.SpellRecover = 65000;
                                    }
                                    else
                                    {
                                        aabil.SpellRecover = (ushort)(aabil.SpellRecover + std.EffValue1);
                                    }
                                    break;
                            }
                        }
                        if (std.EffType2 > 0)
                        {
                            switch (std.EffType2)
                            {
                                case Grobal2.EFFTYPE_HP_MP_ADD:
                                    if (aabil.HealthRecover + std.EffRate2 > 65000)
                                    {
                                        aabil.HealthRecover = 65000;
                                    }
                                    else
                                    {
                                        aabil.HealthRecover = (ushort)(aabil.HealthRecover + std.EffRate2);
                                    }
                                    if (aabil.SpellRecover + std.EffValue2 > 65000)
                                    {
                                        aabil.SpellRecover = 65000;
                                    }
                                    else
                                    {
                                        aabil.SpellRecover = (ushort)(aabil.SpellRecover + std.EffValue2);
                                    }
                                    break;
                            }
                        }
                        if (std.EffType1 == Grobal2.EFFTYPE_LUCK_ADD)
                        {
                            if (aabil.Luck + std.EffValue1 > 255)
                            {
                                aabil.Luck = 255;
                            }
                            else
                            {
                                aabil.Luck = (byte)(aabil.Luck + std.EffValue1);
                            }
                        }
                        else if (std.EffType2 == Grobal2.EFFTYPE_LUCK_ADD)
                        {
                            if (aabil.Luck + std.EffValue2 > 255)
                            {
                                aabil.Luck = 255;
                            }
                            else
                            {
                                aabil.Luck = (byte)(aabil.Luck + std.EffValue2);
                            }
                        }
                        break;
                    case 15:
                        aabil.AC = MakeWord(LoByte(aabil.AC) + LoByte(std.AC), HiByte(aabil.AC) + HiByte(std.AC));
                        aabil.MAC = MakeWord(LoByte(aabil.MAC) + LoByte(std.MAC), HiByte(aabil.MAC) + HiByte(std.MAC));
                        aabil.HIT = (short)(aabil.HIT + std.Accurate);
                        aabil.AntiMagic = (ushort)(aabil.AntiMagic + std.MgAvoid);
                        aabil.AntiPoison = (short)(aabil.AntiPoison + std.ToxAvoid);
                        break;
                    case 19:
                        aabil.AntiMagic = (ushort)(aabil.AntiMagic + HiByte(std.AC));
                        aabil.UnLuck = (byte)(aabil.UnLuck + LoByte(std.MAC));
                        aabil.Luck = (byte)(aabil.Luck + HiByte(std.MAC));
                        aabil.HitSpeed = (short)(aabil.HitSpeed + std.AtkSpd);
                        aabil.HIT = (short)(aabil.HIT + std.Accurate);
                        aabil.Slowdown = (byte)(aabil.Slowdown + std.Slowdown);
                        aabil.Poison = (byte)(aabil.Poison + std.Tox);
                        break;
                    case 20:
                        aabil.HIT = (short)(aabil.HIT + HiByte(std.AC));
                        aabil.SPEED = (short)(aabil.SPEED + HiByte(std.MAC));
                        aabil.HitSpeed = (short)(aabil.HitSpeed + std.AtkSpd);
                        aabil.AntiMagic = (ushort)(aabil.AntiMagic + std.MgAvoid);
                        aabil.Slowdown = (byte)(aabil.Slowdown + std.Slowdown);
                        aabil.Poison = (byte)(aabil.Poison + std.Tox);
                        break;
                    case 21:
                        aabil.HealthRecover = (ushort)(aabil.HealthRecover + HiByte(std.AC));
                        aabil.SpellRecover = (ushort)(aabil.SpellRecover + HiByte(std.MAC));
                        aabil.HitSpeed = (short)(aabil.HitSpeed + LoByte(std.AC));
                        aabil.HitSpeed = (short)(aabil.HitSpeed - LoByte(std.MAC));
                        aabil.HitSpeed = (short)(aabil.HitSpeed + std.AtkSpd);
                        aabil.HIT = (short)(aabil.HIT + std.Accurate);
                        aabil.AntiMagic = (ushort)(aabil.AntiMagic + std.MgAvoid);
                        aabil.Slowdown = (byte)(aabil.Slowdown + std.Slowdown);
                        aabil.Poison = (byte)(aabil.Poison + std.Tox);
                        break;
                    case 22:
                        aabil.AC = MakeWord(LoByte(aabil.AC) + LoByte(std.AC), HiByte(aabil.AC) + HiByte(std.AC));
                        aabil.MAC = MakeWord(LoByte(aabil.MAC) + LoByte(std.MAC), HiByte(aabil.MAC) + HiByte(std.MAC));
                        aabil.HitSpeed = (short)(aabil.HitSpeed + std.AtkSpd);
                        aabil.Slowdown = (byte)(aabil.Slowdown + std.Slowdown);
                        aabil.Poison = (byte)(aabil.Poison + std.Tox);
                        aabil.HIT = (short)(aabil.HIT + std.Accurate);
                        aabil.HP = (short)(aabil.HP + std.HpAdd);
                        break;
                    case 23:
                        aabil.AntiPoison = (short)(aabil.AntiPoison + HiByte(std.AC));
                        aabil.PoisonRecover = (ushort)(aabil.PoisonRecover + HiByte(std.MAC));
                        aabil.HitSpeed = (short)(aabil.HitSpeed + LoByte(std.AC));
                        aabil.HitSpeed = (short)(aabil.HitSpeed - LoByte(std.MAC));
                        aabil.HitSpeed = (short)(aabil.HitSpeed + std.AtkSpd);
                        aabil.Slowdown = (byte)(aabil.Slowdown + std.Slowdown);
                        aabil.Poison = (byte)(aabil.Poison + std.Tox);
                        break;
                    case 24:
                    case 26:
                        if (std.SpecialPwr >= 1 && std.SpecialPwr <= 10)
                        {
                            aabil.WeaponStrong = (byte)std.SpecialPwr;
                        }
                        switch (ps.StdMode)
                        {
                            case 24:
                                aabil.HIT = (short)(aabil.HIT + HiByte(std.AC));
                                aabil.SPEED = (short)(aabil.SPEED + HiByte(std.MAC));
                                break;
                            case 26:
                                aabil.AC = MakeWord(LoByte(aabil.AC) + LoByte(std.AC), HiByte(aabil.AC) + HiByte(std.AC));
                                aabil.MAC = MakeWord(LoByte(aabil.MAC) + LoByte(std.MAC), HiByte(aabil.MAC) + HiByte(std.MAC));
                                aabil.HIT = (short)(aabil.HIT + std.Accurate);
                                aabil.SPEED = (short)(aabil.SPEED + std.Agility);
                                aabil.MP = (short)(aabil.MP + std.MpAdd);
                                break;
                        }
                        break;
                    case 52:
                        aabil.AC = MakeWord(LoByte(aabil.AC) + LoByte(std.AC), HiByte(aabil.AC) + HiByte(std.AC));
                        aabil.MAC = MakeWord(LoByte(aabil.MAC) + LoByte(std.MAC), HiByte(aabil.MAC) + HiByte(std.MAC));
                        aabil.SPEED = (short)(aabil.SPEED + std.Agility);
                        break;
                    case 54:
                        aabil.AC = MakeWord(LoByte(aabil.AC) + LoByte(std.AC), HiByte(aabil.AC) + HiByte(std.AC));
                        aabil.MAC = MakeWord(LoByte(aabil.MAC) + LoByte(std.MAC), HiByte(aabil.MAC) + HiByte(std.MAC));
                        aabil.HIT = (short)(aabil.HIT + std.Accurate);
                        aabil.SPEED = (short)(aabil.SPEED + std.Agility);
                        aabil.AntiPoison = (short)(aabil.AntiPoison + std.ToxAvoid);
                        break;
                    case 53:
                        aabil.HP = (short)(aabil.HP + std.HpAdd);
                        aabil.MP = (short)(aabil.MP + std.MpAdd);
                        break;
                    default:
                        aabil.AC = MakeWord(LoByte(aabil.AC) + LoByte(std.AC), HiByte(aabil.AC) + HiByte(std.AC));
                        aabil.MAC = MakeWord(LoByte(aabil.MAC) + LoByte(std.MAC), HiByte(aabil.MAC) + HiByte(std.MAC));
                        break;
                }
                aabil.DC = MakeWord(LoByte(aabil.DC) + LoByte(std.DC), _MIN(255, HiByte(aabil.DC) + HiByte(std.DC)));
                aabil.MC = MakeWord(LoByte(aabil.MC) + LoByte(std.MC), _MIN(255, HiByte(aabil.MC) + HiByte(std.MC)));
                aabil.SC = MakeWord(LoByte(aabil.SC) + LoByte(std.SC), _MIN(255, HiByte(aabil.SC) + HiByte(std.SC)));
            }
        }

        public void ApplyItemParametersByJob(TUserItem uitem, ref TStdItem std)
        {
            TStdItem ps;
            ps = M2Share.UserEngine.GetStdItem(uitem.Index);
            if (ps != null)
            {
                if ((ps.StdMode == 22) && (ps.Shape == ObjBase.DRAGON_RING_SHAPE))
                {
                    switch (Job)
                    {
                        case 0:
                            std.DC = MakeWord(LoByte(std.DC), _MIN(255, HiByte(std.DC) + 4));
                            std.MC = 0;
                            std.SC = 0;
                            break;
                        case 1:
                            std.DC = 0;
                            std.SC = 0;
                            break;
                        case 2:
                            std.MC = 0;
                            break;
                    }
                }
                else if ((ps.StdMode == 26) && (ps.Shape == ObjBase.DRAGON_BRACELET_SHAPE))
                {
                    switch (Job)
                    {
                        case 0:
                            // 傈荤
                            std.DC = MakeWord(LoByte(std.DC) + 1, _MIN(255, HiByte(std.DC) + 2));
                            // 窍靛内爹
                            std.MC = 0;
                            std.SC = 0;
                            std.AC = MakeWord(LoByte(std.AC), _MIN(255, HiByte(std.AC) + 1));
                            break;
                        case 1:
                            // 窍靛内爹
                            // 贱荤
                            std.DC = 0;
                            // std.MC := 0;
                            std.SC = 0;
                            std.AC = MakeWord(LoByte(std.AC), _MIN(255, HiByte(std.AC) + 1));
                            break;
                        case 2:
                            // 窍靛内爹
                            // 档荤
                            // std.DC := 0;
                            std.MC = 0;
                            break;
                            // std.SC := 0;
                            // std.AC := 0;
                    }
                    // case
                    // --------------
                    // 格吧捞
                }
                else if ((ps.StdMode == 19) && (ps.Shape == ObjBase.DRAGON_NECKLACE_SHAPE))
                {
                    switch (Job)
                    {
                        case 0:
                            // 傈荤
                            // std.DC := 0;
                            std.MC = 0;
                            std.SC = 0;
                            break;
                        case 1:
                            // 贱荤
                            std.DC = 0;
                            // std.MC := 0;
                            std.SC = 0;
                            break;
                        case 2:
                            // 档荤
                            std.DC = 0;
                            std.MC = 0;
                            break;
                            // std.SC := 0;
                    }
                    // case
                    // --------------
                    // 渴
                }
                else if (((ps.StdMode == 10) || (ps.StdMode == 11)) && (ps.Shape == ObjBase.DRAGON_DRESS_SHAPE))
                {
                    switch (Job)
                    {
                        case 0:
                            // 傈荤
                            // std.DC := 0;
                            std.MC = 0;
                            std.SC = 0;
                            break;
                        case 1:
                            // 贱荤
                            std.DC = 0;
                            // std.MC := 0;
                            std.SC = 0;
                            break;
                        case 2:
                            // 档荤
                            std.DC = 0;
                            std.MC = 0;
                            break;
                            // std.SC := 0;
                    }
                    // case
                    // --------------
                    // 捧备
                }
                else if ((ps.StdMode == 15) && (ps.Shape == ObjBase.DRAGON_HELMET_SHAPE))
                {
                    switch (Job)
                    {
                        case 0:
                            // 傈荤
                            // std.DC := 0;
                            std.MC = 0;
                            std.SC = 0;
                            break;
                        case 1:
                            // 贱荤
                            std.DC = 0;
                            // std.MC := 0;
                            std.SC = 0;
                            break;
                        case 2:
                            // 档荤
                            std.DC = 0;
                            std.MC = 0;
                            break;
                            // std.SC := 0;
                    }
                    // case
                    // --------------
                    // 公扁
                }
                else if (((ps.StdMode == 5) || (ps.StdMode == 6)) && (ps.Shape == ObjBase.DRAGON_WEAPON_SHAPE))
                {
                    switch (Job)
                    {
                        case 0:
                            std.DC = MakeWord(LoByte(std.DC) + 1, _MIN(255, HiByte(std.DC) + 28));
                            std.MC = 0;
                            std.SC = 0;
                            std.AC = MakeWord(LoByte(std.AC) - 2, HiByte(std.AC));
                            break;
                        case 1:
                            std.SC = 0;
                            if (HiByte(std.MAC) > 12)
                            {
                                std.MAC = MakeWord(LoByte(std.MAC), HiByte(std.MAC) - 12);
                            }
                            else
                            {
                                std.MAC = MakeWord(LoByte(std.MAC), 0);
                            }
                            break;
                        case 2:
                            std.DC = MakeWord(LoByte(std.DC) + 2, _MIN(255, HiByte(std.DC) + 10));
                            std.MC = 0;
                            std.AC = MakeWord(LoByte(std.AC) - 2, HiByte(std.AC));
                            break;
                    }
                }
                else if (ps.StdMode == 53)
                {
                    if (ps.Shape == ObjBase.LOLLIPOP_SHAPE)
                    {
                        switch (Job)
                        {
                            case 0:
                                std.DC = MakeWord(LoByte(std.DC), _MIN(255, HiByte(std.DC) + 2));
                                std.MC = 0;
                                std.SC = 0;
                                break;
                            case 1:
                                std.DC = 0;
                                std.MC = MakeWord(LoByte(std.MC), _MIN(255, HiByte(std.MC) + 2));
                                std.SC = 0;
                                break;
                            case 2:
                                std.DC = 0;
                                std.MC = 0;
                                std.SC = MakeWord(LoByte(std.SC), _MIN(255, HiByte(std.SC) + 2));
                                break;
                        }
                    }
                    else if ((std.Shape == ObjBase.GOLDMEDAL_SHAPE) || (std.Shape == ObjBase.SILVERMEDAL_SHAPE) || (std.Shape == ObjBase.BRONZEMEDAL_SHAPE))
                    {
                        switch (Job)
                        {
                            case 0:
                                std.DC = MakeWord(LoByte(std.DC), _MIN(255, HiByte(std.DC)));
                                std.MC = 0;
                                std.SC = 0;
                                break;
                            case 1:
                                std.DC = 0;
                                std.MC = MakeWord(LoByte(std.MC), _MIN(255, HiByte(std.MC)));
                                std.SC = 0;
                                break;
                            case 2:
                                std.DC = 0;
                                std.MC = 0;
                                std.SC = MakeWord(LoByte(std.SC), _MIN(255, HiByte(std.SC)));
                                break;
                        }
                    }
                    // if series
                    // ////////////////////////////////////
                    // 2004-06-29 脚痹癌渴(颇炔玫付狼) 流诀喊 瓷仿摹
                    if (((ps.StdMode == 10) || (ps.StdMode == 11)) && (ps.Shape == ObjBase.DRESS_SHAPE_PBKING))
                    {
                        switch (Job)
                        {
                            case 0:
                                std.DC = MakeWord(LoByte(std.DC), _MIN(255, HiByte(std.DC) + 2));
                                std.MC = 0;
                                std.SC = 0;
                                std.AC = MakeWord(LoByte(std.AC) + 2, _MIN(255, HiByte(std.AC) + 4));
                                std.MpAdd = std.MpAdd + 30;
                                break;
                            case 1:
                                std.DC = 0;
                                std.SC = 0;
                                std.MAC = MakeWord(LoByte(std.MAC) + 1, _MIN(255, HiByte(std.MAC) + 2));
                                std.HpAdd = std.HpAdd + 30;
                                break;
                            case 2:
                                std.DC = MakeWord(LoByte(std.DC) + 1, _MIN(255, HiByte(std.DC)));
                                std.MC = 0;
                                std.AC = MakeWord(LoByte(std.AC) + 1, _MIN(255, HiByte(std.AC)));
                                std.MAC = MakeWord(LoByte(std.MAC) + 1, _MIN(255, HiByte(std.MAC)));
                                std.HpAdd = std.HpAdd + 20;
                                std.MpAdd = std.MpAdd + 10;
                                break;
                        }
                    }
                }
            }
        }

        // 2003/03/15 酒捞袍 牢亥配府 犬厘
        // 2003/03/15 酒捞袍 牢亥配府 犬厘
        public void ApplyItemParametersEx(TUserItem uitem, ref TAbility AWabil)
        {
            TStdItem ps;
            TStdItem std;
            ps = M2Share.UserEngine.GetStdItem(uitem.Index);
            if (ps != null)
            {
                std = ps;
                M2Share.ItemMan.GetUpgradeStdItem(uitem, ref std);
                switch (ps.StdMode)
                {
                    case 52:
                        // 公扁狼 诀弊饭捞靛等 瓷仿摹甫 掘绢柯促.
                        // 脚惯幅
                        if (std.EffType1 > 0)
                        {
                            switch (std.EffType1)
                            {
                                case Grobal2.EFFTYPE_TWOHAND_WEHIGHT_ADD:
                                    if (AWabil.MaxHandWeight + std.EffValue1 > 255)
                                    {
                                        AWabil.MaxHandWeight = 255;
                                    }
                                    else
                                    {
                                        AWabil.MaxHandWeight = (byte)(AWabil.MaxHandWeight + std.EffValue1);
                                    }
                                    break;
                                case Grobal2.EFFTYPE_EQUIP_WHEIGHT_ADD:
                                    // Overflow 规瘤甫 困秦 荐沥(sonmg)
                                    if (AWabil.MaxWearWeight + std.EffValue1 > 255)
                                    {
                                        AWabil.MaxWearWeight = 255;
                                    }
                                    else
                                    {
                                        AWabil.MaxWearWeight = (byte)(AWabil.MaxWearWeight + std.EffValue1);
                                    }
                                    break;
                            }
                        }
                        if (std.EffType2 > 0)
                        {
                            switch (std.EffType2)
                            {
                                case Grobal2.EFFTYPE_TWOHAND_WEHIGHT_ADD:
                                    if (AWabil.MaxHandWeight + std.EffValue2 > 255)
                                    {
                                        AWabil.MaxHandWeight = 255;
                                    }
                                    else
                                    {
                                        AWabil.MaxHandWeight = (byte)(AWabil.MaxHandWeight + std.EffValue2);
                                    }
                                    break;
                                case Grobal2.EFFTYPE_EQUIP_WHEIGHT_ADD:
                                    // Overflow 规瘤甫 困秦 荐沥(sonmg)
                                    if (AWabil.MaxWearWeight + std.EffValue2 > 255)
                                    {
                                        AWabil.MaxWearWeight = 255;
                                    }
                                    else
                                    {
                                        AWabil.MaxWearWeight = (byte)(AWabil.MaxWearWeight + std.EffValue2);
                                    }
                                    break;
                            }
                        }
                        break;
                    case 54:
                        if (std.EffType1 > 0)
                        {
                            switch (std.EffType1)
                            {
                                case Grobal2.EFFTYPE_BAG_WHIGHT_ADD:
                                    if (AWabil.MaxWeight + std.EffValue1 > 65000)
                                    {
                                        AWabil.MaxWeight = 65000;
                                    }
                                    else
                                    {
                                        AWabil.MaxWeight = (ushort)(AWabil.MaxWeight + std.EffValue1);
                                    }
                                    break;
                            }
                        }
                        if (std.EffType2 > 0)
                        {
                            switch (std.EffType2)
                            {
                                case Grobal2.EFFTYPE_BAG_WHIGHT_ADD:
                                    if (AWabil.MaxWeight + std.EffValue2 > 65000)
                                    {
                                        AWabil.MaxWeight = 65000;
                                    }
                                    else
                                    {
                                        AWabil.MaxWeight = (ushort)(AWabil.MaxWeight + std.EffValue2);
                                    }
                                    break;
                            }
                        }
                        break;
                }
            }
        }

        public bool MakeWeaponUnlock()
        {
            TUserHuman hum;
            bool result = false;
            if (UseItems[Grobal2.U_WEAPON].Index != 0)
            {
                if (UseItems[Grobal2.U_WEAPON].Desc[3] > 0)
                {
                    UseItems[Grobal2.U_WEAPON].Desc[3] = (byte)(UseItems[Grobal2.U_WEAPON].Desc[3] - 1);
                    SysMsg("您的武器被诅咒了。", 0);
                    result = true;
                }
                else
                {
                    if (UseItems[Grobal2.U_WEAPON].Desc[4] < 10)
                    {
                        UseItems[Grobal2.U_WEAPON].Desc[4] = (byte)(UseItems[Grobal2.U_WEAPON].Desc[4] + 1);
                        SysMsg("您的武器被诅咒了。", 0);
                        result = true;
                    }
                }
                if (RaceServer == Grobal2.RC_USERHUMAN)
                {
                    RecalcAbilitys();
                    hum = this as TUserHuman;
                    hum.SendUpdateItem(UseItems[Grobal2.U_WEAPON]);
                    SendMsg(this, Grobal2.RM_ABILITY, 0, 0, 0, 0, "");
                    SendMsg(this, Grobal2.RM_SUBABILITY, 0, 0, 0, 0, "");
                }
            }
            return result;
        }

        public void TrainSkill(TUserMagic pum, int train)
        {
            if (BoFastTraining)
            {
                train = train * 3;
            }
            pum.CurTrain = pum.CurTrain + train;
        }

        public TAbility GetMyAbility()
        {
            TAbility result;
            result = Abil;
            result.HP = (short)(AddAbil.HP + Abil.HP);
            result.MP = (short)(AddAbil.MP + Abil.MP);
            result.AC = MakeWord(_MIN(255, LoByte(AddAbil.AC) + LoByte(Abil.AC)), _MIN(255, HiByte(AddAbil.AC) + HiByte(Abil.AC)));
            result.MAC = MakeWord(_MIN(255, LoByte(AddAbil.MAC) + LoByte(Abil.MAC)), _MIN(255, HiByte(AddAbil.MAC) + HiByte(Abil.MAC)));
            result.DC = MakeWord(_MIN(255, LoByte(AddAbil.DC) + LoByte(Abil.DC)), _MIN(255, HiByte(AddAbil.DC) + HiByte(Abil.DC)));
            result.MC = MakeWord(_MIN(255, LoByte(AddAbil.MC) + LoByte(Abil.MC)), _MIN(255, HiByte(AddAbil.MC) + HiByte(Abil.MC)));
            result.SC = MakeWord(_MIN(255, LoByte(AddAbil.SC) + LoByte(Abil.SC)), _MIN(255, HiByte(AddAbil.SC) + HiByte(Abil.SC)));
            return result;
        }

        public int GetMyLight_CheckLightValue()
        {
            int result;
            TStdItem ps;
            TUserHuman hum;
            int CurrentLight = 0;
            if (RaceServer == Grobal2.RC_USERHUMAN)
            {
                hum = this as TUserHuman;
                if (hum != null)
                {
                    if (BoHighLevelEffect)
                    {
                        if (Abil.Level >= ObjBase.EFFECTIVE_HIGHLEVEL)
                        {
                            CurrentLight = 1;
                        }
                    }
                }
            }
            for (var i = Grobal2.U_DRESS; i <= Grobal2.U_CHARM; i++)
            {
                if ((UseItems[i].Index > 0) && (UseItems[i].Dura > 0))
                {
                    ps = M2Share.UserEngine.GetStdItem(UseItems[i].Index);
                    if (ps != null)
                    {
                        if (CurrentLight < ps.light)
                        {
                            CurrentLight = ps.light;
                        }
                    }
                }
            }
            result = CurrentLight;
            return result;
        }

        public int GetMyLight()
        {
            int result;
            if (BoTaiwanEventUser)
            {
                result = 4;
            }
            else
            {
                result = GetMyLight_CheckLightValue();
            }
            return result;
        }

        public string GetUserName()
        {
            string result = string.Empty;
            if (RaceServer != Grobal2.RC_USERHUMAN)
            {
                HUtil32.GetValidStrNoVal(UserName, ref result);
                if (Master != null)
                {
                    if (!Master.BoSuperviserMode)
                    {
                        if (RaceServer == Grobal2.RC_CLONE)
                        {
                            if (Master.RaceServer == Grobal2.RC_USERHUMAN)
                            {
                                result = Master.UserName;
                            }
                        }
                        else
                        {
                            result = result + "(" + Master.UserName + ")";
                        }
                    }
                }
            }
            else
            {
                result = UserName;
                if (m_sMasterName != "")
                {
                    if (m_boMaster)
                    {
                        // Result := Result + '\' + Format('%s的师父', [m_sMasterName])
                    }
                    else
                    {
                        switch (m_nMasterRanking)
                        {
                            case 1:
                                result = result + "\\" + string.Format("%s的大徒弟", new string[] { m_sMasterName });
                                break;
                            case 2:
                                result = result + "\\" + string.Format("%s的二徒弟", new string[] { m_sMasterName });
                                break;
                            case 3:
                                result = result + "\\" + string.Format("%s的三徒弟", new string[] { m_sMasterName });
                                break;
                            case 4:
                                result = result + "\\" + string.Format("%s的四徒弟", new string[] { m_sMasterName });
                                break;
                            case 5:
                                result = result + "\\" + string.Format("%s的五徒弟", new string[] { m_sMasterName });
                                break;
                        }
                    }
                }
                if (BoTaiwanEventUser)
                {
                    // 措父 酒捞袍 捞亥飘
                    result = result + "(" + TaiwanEventItemName + ")";
                }
                if (MyGuild != null)
                {
                    if (M2Share.UserCastle.IsOurCastle(MyGuild))
                    {
                        // 快府巩颇啊 荤合己阑 痢飞
                        // 努扼捞攫飘俊辑绰 馆措肺 捞抚捞 盖 唱吝俊 结柳促.
                        result = result + "\\" + MyGuild.GuildName + "(" + M2Share.UserCastle.CastleName + ")";
                    }
                    else
                    {
                        // if UserCastle.BoCastleUnderAttack then
                        // if (BoInFreePKArea) or (UserCastle.IsCastleWarArea (PEnvir, CX, CY)) then
                        result = result + "\\" + MyGuild.GuildName;
                    }
                }
                if ((this as TUserHuman).fLover.GetLoverName() != "")
                {
                    if ((this as TUserHuman).Sex == 1)
                    {
                        result = result + "\\[" + (this as TUserHuman).fLover.GetLoverName() + " 的妻子]";
                    }
                    else
                    {
                        result = result + "\\[" + (this as TUserHuman).fLover.GetLoverName() + " 的丈夫]";
                    }
                }
            }
            return result;
        }

        public int GetHungryState()
        {
            int result;
            result = (int)(HungryState / 1000);
            if (result > 4)
            {
                result = 4;
            }
            // 0, 1, 2, 3, 4

            return result;
        }

        public int GetQuestMark(int idx)
        {
            int result;
            int dcount;
            int mcount;
            result = 0;
            idx = idx - 1;
            if (idx >= 0)
            {
                dcount = idx / 8;
                mcount = idx % 8;
                if (dcount >= 0 && dcount <= Grobal2.MAXQUESTBYTE - 1)
                {
                    if ((QuestStates[dcount] & (0x80 >> mcount)) != 0)
                    {
                        result = 1;
                    }
                    else
                    {
                        result = 0;
                    }
                }
            }
            return result;
        }

        public void SetQuestMark(int idx, int value)
        {
            int dcount;
            int mcount;
            byte val;
            idx = idx - 1;
            if (idx >= 0)
            {
                dcount = idx / 8;
                mcount = idx % 8;
                if (dcount >= 0 && dcount <= Grobal2.MAXQUESTBYTE - 1)
                {
                    val = QuestStates[dcount];
                    if (value == 0)
                    {
                        QuestStates[dcount] = (byte)(val & (~(0x80 >> mcount)));
                    }
                    else
                    {
                        QuestStates[dcount] = (byte)(val | (0x80 >> mcount));
                    }
                }
            }
        }

        public int GetQuestOpenIndexMark(int idx)
        {
            int dcount;
            int mcount;
            int result = 0;
            idx = idx - 1;
            if (idx >= 0)
            {
                dcount = idx / 8;
                mcount = idx % 8;
                if (dcount >= 0 && dcount <= Grobal2.MAXQUESTINDEXBYTE - 1)
                {
                    if ((QuestIndexOpenStates[dcount] & (0x80 >> mcount)) != 0)
                    {
                        result = 1;
                    }
                    else
                    {
                        result = 0;
                    }
                }
            }
            return result;
        }

        // 0 or not zero
        public void SetQuestOpenIndexMark(int idx, int value)
        {
            int dcount;
            int mcount;
            byte val;
            idx = idx - 1;
            if (idx >= 0)
            {
                dcount = idx / 8;
                mcount = idx % 8;
                if (dcount >= 0 && dcount <= Grobal2.MAXQUESTINDEXBYTE - 1)
                {
                    val = QuestIndexOpenStates[dcount];
                    if (value == 0)
                    {
                        QuestIndexOpenStates[dcount] = (byte)(val & (~(0x80 >> mcount)));
                    }
                    else
                    {
                        QuestIndexOpenStates[dcount] = (byte)(val | (0x80 >> mcount));
                    }
                }
            }
        }

        public int GetQuestFinIndexMark(int idx)
        {
            int result;
            int dcount;
            int mcount;
            result = 0;
            idx = idx - 1;
            if (idx >= 0)
            {
                dcount = idx / 8;
                mcount = idx % 8;
                if (dcount >= 0 && dcount <= Grobal2.MAXQUESTINDEXBYTE - 1)
                {
                    if ((QuestIndexFinStates[dcount] & (0x80 >> mcount)) != 0)
                    {
                        result = 1;
                    }
                    else
                    {
                        result = 0;
                    }
                }
            }
            return result;
        }

        // 0 or not zero
        public void SetQuestFinIndexMark(int idx, int value)
        {
            int dcount;
            int mcount;
            byte val;
            idx = idx - 1;
            if (idx >= 0)
            {
                dcount = idx / 8;
                mcount = idx % 8;
                if (dcount >= 0 && dcount <= Grobal2.MAXQUESTINDEXBYTE - 1)
                {
                    val = QuestIndexFinStates[dcount];
                    if (value == 0)
                    {
                        QuestIndexFinStates[dcount] = (byte)(val & (~(0x80 >> mcount)));
                    }
                    else
                    {
                        QuestIndexFinStates[dcount] = (byte)(val | (0x80 >> mcount));
                    }
                }
            }
        }

        public void DoDamageWeapon(int wdam)
        {
            if ((UseItems[Grobal2.U_WEAPON].Index > 0) && (UseItems[Grobal2.U_WEAPON].Dura > 0))
            {
                int idura = UseItems[Grobal2.U_WEAPON].Dura;
                int olddura = HUtil32.MathRound(idura / 1000);
                idura = idura - wdam;
                if (idura <= 0)
                {
                    idura = 0;
                    SysMsg(M2Share.UserEngine.GetStdItemName(UseItems[Grobal2.U_WEAPON].Index) + "道具因持久值耗尽而消失", 0);
                    UseItems[Grobal2.U_WEAPON].Dura = 0;
                    RecalcAbilitys();
                    SendMsg(this, Grobal2.RM_DURACHANGE, Grobal2.U_WEAPON, UseItems[Grobal2.U_WEAPON].Dura, UseItems[Grobal2.U_WEAPON].DuraMax, 0, "");
                    SendMsg(this, Grobal2.RM_ABILITY, 0, 0, 0, 0, "");
                    SendMsg(this, Grobal2.RM_SUBABILITY, 0, 0, 0, 0, "");
                }
                else
                {
                    UseItems[Grobal2.U_WEAPON].Dura = (short)idura;
                }
                if (olddura != HUtil32.MathRound(idura / 1000))
                {
                    SendMsg(this, Grobal2.RM_DURACHANGE, Grobal2.U_WEAPON, UseItems[Grobal2.U_WEAPON].Dura, UseItems[Grobal2.U_WEAPON].DuraMax, 0, "");
                }
            }
        }

        public int GetAttackPower(int damage, int ranval)
        {
            int result;
            if (ranval < 0)
            {
                ranval = 0;
            }
            if (Luck > 0)
            {
                if (new System.Random(10 - _MIN(9, Luck)).Next() == 0)
                {
                    // 青款牢版快
                    result = damage + ranval;
                }
                else
                {
                    result = damage + new System.Random(ranval + 1).Next();
                }
            }
            else
            {
                result = damage + new System.Random(ranval + 1).Next();
                if (Luck < 0)
                {
                    if (new System.Random(_MAX(0, 10 - _MAX(0, -Luck))).Next() == 0)
                    {
                        result = damage;
                    }
                }
            }
            return result;
        }

        public bool _Attack_DirectAttack(TCreature target, int damage)
        {
            bool result = false;
            if ((RaceServer == Grobal2.RC_USERHUMAN) && (target.RaceServer == Grobal2.RC_USERHUMAN) && (target.InSafeZone() || InSafeZone()))
            {
                return result;
            }
            if (IsProperTarget(target))
            {
                if (new System.Random(target.SpeedPoint).Next() < AccuracyPoint)
                {
                    target.StruckDamage(damage, this);
                    target.SendDelayMsg(Grobal2.RM_STRUCK, Grobal2.RM_REFMESSAGE, (short)damage, target.WAbil.HP, target.WAbil.MaxHP, this.ActorId, "", 500);
                    if (target.RaceServer != Grobal2.RC_USERHUMAN)
                    {
                        target.SendMsg(target, Grobal2.RM_STRUCK, (short)damage, target.WAbil.HP, target.WAbil.MaxHP, this.ActorId, "");
                    }
                    result = true;
                }
            }
            return result;
        }

        // 荤磊饶狼 俺喊傍拜
        public bool _Attack_DirectStoneAttack(TCreature target, int damage)
        {
            bool result;
            result = false;
            if (IsProperTarget(target))
            {
                // 嘎绰瘤 搬沥
                // 单固瘤啊 乐绢 具 登绊
                // 磊脚狼 饭骇焊促 4窜拌 固父父 登绊
                // 空鞭阁胶磐俊霸绰 烹窍瘤 臼澜
                if ((damage > 0) && (target.Abil.Level < this.Abil.Level + 4) && (target.Abil.Level < 60))
                {
                    target.MakePoison(Grobal2.POISON_DONTMOVE, damage, 0);
                    // 付厚
                    result = true;
                }
            }
            return result;
        }

        // 倒 傍拜
        public TCreature _Attack_StoneAttack(int damage)
        {
            TCreature result;
            int i;
            int j;
            int xx;
            int yy;
            TCreature target;
            result = null;
            for (i = -2; i <= 2; i++)
            {
                for (j = -2; j <= 2; j++)
                {
                    xx = CX + i;
                    yy = CY + j;
                    target = PEnvir.GetCreature(xx, yy, true) as TCreature;
                    if ((damage > 0) && (target != null))
                    {
                        if (target.RaceServer != Grobal2.RC_USERHUMAN)
                        {
                            if (_Attack_DirectStoneAttack(target, damage))
                            {
                                result = target;
                            }
                        }
                    }
                }
            }
            return result;
        }

        public bool _Attack_SwordLongAttack(int damage)
        {
            short xx = 0;
            short yy = 0;
            TCreature target;
            bool result = false;
            if (M2Share.GetNextPosition(PEnvir, CX, CY, Dir, 2, ref xx, ref yy))
            {
                target = PEnvir.GetCreature(xx, yy, true) as TCreature;
                if ((damage > 0) && (target != null))
                {
                    if (IsProperTarget(target))
                    {
                        result = _Attack_DirectAttack(target, damage);
                        SelectTarget(target);
                    }
                }
            }
            return result;
        }

        public bool _Attack_SwordWideAttack(int damage)
        {
            int[] valarr = { 7, 1, 2 };
            short xx = 0;
            short yy = 0;
            TCreature target;
            bool result = false;
            for (var i = 0; i <= 2; i++)
            {
                int ndir = (Dir + valarr[i]) % 8;
                if (M2Share.GetNextPosition(PEnvir, CX, CY, (byte)ndir, 1, ref xx, ref yy))
                {
                    target = PEnvir.GetCreature(xx, yy, true) as TCreature;
                    if ((damage > 0) && (target != null))
                    {
                        if (IsProperTarget(target))
                        {
                            result = _Attack_DirectAttack(target, damage);
                            SelectTarget(target);
                        }
                    }
                }
            }
            return result;
        }

        public bool _Attack_SwordCrossAttack(int damage)
        {
            bool result = false;
            int[] valarr = { 7, 1, 2, 3, 4, 5, 6 };
            short xx = 0;
            short yy = 0;
            TCreature target;
            for (var i = 0; i <= 6; i++)
            {
                byte ndir = (byte)((Dir + valarr[i]) % 8);
                if (M2Share.GetNextPosition(PEnvir, CX, CY, ndir, 1, ref xx, ref yy))
                {
                    target = PEnvir.GetCreature(xx, yy, true) as TCreature;
                    if ((damage > 0) && (target != null))
                    {
                        if (IsProperTarget(target))
                        {
                            if (target.RaceServer != Grobal2.RC_USERHUMAN)
                            {
                                result = _Attack_DirectAttack(target, damage);
                            }
                            else
                            {
                                result = _Attack_DirectAttack(target, HUtil32.MathRound(damage * 0.8));
                            }
                            SelectTarget(target);
                        }
                    }
                }
            }
            return result;
        }

        public bool _Attack(short hitmode, TCreature targ)
        {
            bool result;
            int dam;
            int seconddam;
            int weapondamage;
            int n;
            bool addplus;
            int Gap;
            int MoC;
            int Dur;
            result = false;
            try
            {
                addplus = false;
                weapondamage = 0;
                dam = 0;
                seconddam = 0;
                if (targ != null)
                {
                    dam = GetAttackPower(HUtil32.LoByte(WAbil.DC), HiByte(WAbil.DC) - HUtil32.LoByte(WAbil.DC));
                    if ((hitmode == Grobal2.HM_POWERHIT) && BoAllowPowerHit)
                    {
                        BoAllowPowerHit = false;
                        dam = dam + HitPowerPlus;
                        addplus = true;
                    }
                    if ((hitmode == Grobal2.HM_FIREHIT) && BoAllowFireHit)
                    {
                        BoAllowFireHit = false;
                        dam = dam + HUtil32.MathRound(dam / 100 * HitDouble * 10);
                        addplus = true;
                    }
                    if (IsProperTarget(targ))
                    {
                        if ((targ.Abil.Level < 60) && (targ.StatusArr[Grobal2.POISON_SLOW] == 0) && (AddAbil.Slowdown > 0) && (new System.Random(20).Next() <= AddAbil.Slowdown) && (new System.Random(50).Next() > targ.AntiMagic))
                        {
                            // 100->50
                            MoC = 1;
                            Gap = targ.Abil.Level - Abil.Level;
                            if (Gap > 10)
                            {
                                Gap = 10;
                            }
                            if (Gap < -10)
                            {
                                Gap = -10;
                            }
                            if (targ.RaceServer == Grobal2.RC_USERHUMAN)
                            {
                                MoC = 2;
                            }
                            if (new System.Random(100).Next() < (20 + (AddAbil.Slowdown - Gap) / MoC))
                            {
                                Dur = (900 * AddAbil.Slowdown + 3300) / 1000;
                                targ.MakePoison(Grobal2.POISON_SLOW, Dur + 1, 1);
                            }
                            // ...吝刀魄沥...
                        }
                        else if ((targ.Abil.Level < 60) && (AddAbil.Poison > 0) && (new System.Random(20).Next() <= AddAbil.Poison) && (6 >= new System.Random(7 + targ.AntiPoison).Next()))
                        {
                            MoC = 1;
                            Gap = targ.Abil.Level - Abil.Level;
                            if (Gap > 10)
                            {
                                Gap = 10;
                            }
                            if (Gap < -10)
                            {
                                Gap = -10;
                            }
                            if (targ.RaceServer == Grobal2.RC_USERHUMAN)
                            {
                                MoC = 2;
                            }
                            if (new System.Random(100).Next() < (20 + (AddAbil.Poison - Gap) / MoC))
                            {
                                // wparam
                                // pwr(time)
                                targ.SendDelayMsg(this, Grobal2.RM_MAKEPOISON, Grobal2.POISON_DECHEALTH, 5, this.ActorId, AddAbil.Poison, "", 1000);
                            }
                        }
                    }
                }
                else
                {
                    dam = GetAttackPower(HUtil32.LoByte(WAbil.DC), HiByte(WAbil.DC) - HUtil32.LoByte(WAbil.DC));
                    // 八过栏肺 氢惑等 颇况
                    if ((hitmode == Grobal2.HM_POWERHIT) && BoAllowPowerHit)
                    {
                        BoAllowPowerHit = false;
                        dam = dam + HitPowerPlus;
                        addplus = true;
                    }
                }
                // 变 芭府 傍拜 (绢八)
                if (hitmode == Grobal2.HM_LONGHIT)
                {
                    seconddam = 0;
                    if (RaceServer == Grobal2.RC_USERHUMAN)
                    {
                        if (PLongHitSkill != null)
                        {
                            seconddam = HUtil32.MathRound(dam / (PLongHitSkill.pDef.MaxTrainLevel + 2) * (PLongHitSkill.Level + 2));
                        }
                    }
                    else
                    {
                        seconddam = dam;
                    }
                    if (seconddam > 0)
                    {
                        _Attack_SwordLongAttack(seconddam);
                    }
                }
                // 林函 傍拜  (馆岿)
                if (hitmode == Grobal2.HM_WIDEHIT)
                {
                    seconddam = 0;
                    if (RaceServer == Grobal2.RC_USERHUMAN)
                    {
                        if (PWideHitSkill != null)
                        {
                            seconddam = HUtil32.MathRound(dam / (PWideHitSkill.pDef.MaxTrainLevel + 10) * (PWideHitSkill.Level + 2));
                        }
                    }
                    else
                    {
                        seconddam = dam;
                    }
                    if (seconddam > 0)
                    {
                        _Attack_SwordWideAttack(seconddam);
                    }
                }
                // 农肺胶 傍拜 -> 堡浅曼
                if (hitmode == Grobal2.HM_CROSSHIT)
                {
                    seconddam = 0;
                    if (RaceServer == Grobal2.RC_USERHUMAN)
                    {
                        if (PCrossHitSkill != null)
                        {
                            seconddam = HUtil32.MathRound(dam / (PCrossHitSkill.pDef.MaxTrainLevel + 11) * (PCrossHitSkill.Level + 3));
                        }
                    }
                    else
                    {
                        seconddam = dam;
                    }
                    if (seconddam > 0)
                    {
                        _Attack_SwordCrossAttack(seconddam);
                    }
                }
                // 街锋曼
                if ((hitmode == Grobal2.HM_TWINHIT) && (targ != null))
                {
                    dam = dam + HitPowerPlus;
                    seconddam = dam;
                    _Attack_DirectAttack(targ, seconddam);
                    // 惑怕捞惑...胶畔魄沥
                    if ((targ.Abil.Level < 60) && (targ.StatusArr[Grobal2.POISON_STUN] == 0) && (new System.Random(50).Next() > targ.AntiMagic))
                    {
                        // 100->50
                        MoC = 1;
                        // Gap := targ.Abil.Level - Abil.Level;
                        // if Gap > 10 then Gap := 10;
                        // if Gap <-10 then Gap :=-10;
                        if (targ.RaceServer == Grobal2.RC_USERHUMAN)
                        {
                            MoC = 2;
                        }
                        if (((MoC == 1) && (new System.Random(100).Next() < 5 * (PTwinHitSkill.Level + 1))) || ((MoC == 2) && (new System.Random(100).Next() < 2 * (PTwinHitSkill.Level + 1))))
                        {
                            Dur = HUtil32.MathRound(1.5 + 0.8 * PTwinHitSkill.Level);
                            targ.MakePoison(Grobal2.POISON_STUN, Dur, 1);
                        }
                    }
                    if (BoAllowTwinHit == 1)
                    {
                        // 街锋曼 秦力..
                        // {$IFDEF KOREA} SysMsg ('街锋曼捞 矫傈登菌嚼聪促.', 1);
                        // {$ELSE}        SysMsg ('TwinDrakeBlade is used.', 1);   //捞怕府螟 夸没栏肺 昏力(2004/09/22)
                        // {$ENDIF}
                        BoAllowTwinHit = 2;
                    }
                }
                try
                {
                    // 林困阁胶磐 倒凳  -> 荤磊饶
                    if (hitmode == Grobal2.HM_STONEHIT)
                    {
                        seconddam = 0;
                        if ((RaceServer == Grobal2.RC_USERHUMAN) && (PStoneHitSkill != null))
                        {
                            switch (PStoneHitSkill.Level)
                            {
                                case 0:
                                    // 阁胶磐 倒登绰 矫埃 汲沥
                                    seconddam = 5;
                                    break;
                                case 1:
                                    seconddam = 6;
                                    break;
                                case 2:
                                    seconddam = 7;
                                    break;
                                case 3:
                                    seconddam = 8;
                                    break;
                            }
                            targ = _Attack_StoneAttack(seconddam);
                            if (targ != null)
                            {
                                dam = 0;
                            }
                        }
                    }
                }
                catch
                {
                    M2Share.MainOutMessage("EXCEPTION STONEHIT");
                }
                if (targ == null)
                {
                    // 绢八, 馆岿 磊悼荐访阑 阜扁 困秦辑
                    return result;
                }
                // 绢八,馆岿 殿篮 targ客 惑包绝捞 _Attack救栏肺 甸绢柯促.
                if (IsProperTarget(targ))
                {
                    if (AccuracyPoint > new System.Random(targ.SpeedPoint).Next())
                    {
                    }
                    else
                    {
                        dam = 0;
                    }
                }
                else
                {
                    dam = 0;
                }
                if (dam > 0)
                {
                    dam = targ.GetHitStruckDamage(this, dam);
                    weapondamage = new System.Random(5).Next() + 2 - AddAbil.WeaponStrong;
                }
                if ((dam > 0) || (hitmode == Grobal2.HM_STONEHIT))
                {
                    if (hitmode != Grobal2.HM_STONEHIT)
                    {
                        targ.StruckDamage(dam, this);
                        targ.SendDelayMsg(Grobal2.RM_STRUCK, Grobal2.RM_REFMESSAGE, (short)dam, targ.WAbil.HP, targ.WAbil.MaxHP, this.ActorId, "", 200);
                        if (BoAbilMakeStone)
                        {
                            if (new System.Random(5 + targ.AntiPoison).Next() == 0)
                            {
                                targ.MakePoison(Grobal2.POISON_STONE, 5, 0);
                            }
                        }
                        if (SuckupEnemyHealthRate > 0)
                        {
                            SuckupEnemyHealth = SuckupEnemyHealth + (dam / 100 * SuckupEnemyHealthRate);
                            if (SuckupEnemyHealth >= 2)
                            {
                                n = (int)Convert.ToInt64(SuckupEnemyHealth);
                                SuckupEnemyHealth = SuckupEnemyHealth - n;
                                DamageHealth(-n, 0);
                            }
                        }
                    }
                    if ((PSwordSkill != null) && (targ.RaceServer >= Grobal2.RC_ANIMAL))
                    {
                        if (PSwordSkill.Level < 3)
                        {
                            if (Abil.Level >= PSwordSkill.pDef.NeedLevel[PSwordSkill.Level])
                            {
                                TrainSkill(PSwordSkill, 1 + new System.Random(3).Next());
                                if (!CheckMagicLevelup(PSwordSkill))
                                {
                                    SendDelayMsg(this, Grobal2.RM_MAGIC_LVEXP, 0, PSwordSkill.pDef.MagicId, PSwordSkill.Level, PSwordSkill.CurTrain, "", 3000);
                                }
                            }
                        }
                    }
                    // 八贱 氢惑 2 (抗档八过)
                    if (addplus)
                    {
                        if ((PPowerHitSkill != null) && (targ.RaceServer >= Grobal2.RC_ANIMAL))
                        {
                            if (PPowerHitSkill.Level < 3)
                            {
                                if (Abil.Level >= PPowerHitSkill.pDef.NeedLevel[PPowerHitSkill.Level])
                                {
                                    TrainSkill(PPowerHitSkill, 1 + new System.Random(3).Next());
                                    if (!CheckMagicLevelup(PPowerHitSkill))
                                    {
                                        SendDelayMsg(this, Grobal2.RM_MAGIC_LVEXP, 0, PPowerHitSkill.pDef.MagicId, PPowerHitSkill.Level, PPowerHitSkill.CurTrain, "", 3000);
                                    }
                                }
                            }
                        }
                    }
                    // 八贱 氢惑 3 (绢八贱)
                    if ((hitmode == Grobal2.HM_LONGHIT) && (PLongHitSkill != null) && (targ.RaceServer >= Grobal2.RC_ANIMAL))
                    {
                        if (PLongHitSkill.Level < 3)
                        {
                            if (Abil.Level >= PLongHitSkill.pDef.NeedLevel[PLongHitSkill.Level])
                            {
                                TrainSkill(PLongHitSkill, 1);
                                if (!CheckMagicLevelup(PLongHitSkill))
                                {
                                    UpdateDelayMsgCheckParam1(this, Grobal2.RM_MAGIC_LVEXP, 0, PLongHitSkill.pDef.MagicId, PLongHitSkill.Level, PLongHitSkill.CurTrain, "", 3000);
                                }
                            }
                        }
                    }
                    // 八贱 氢惑 4 (馆岿八过)
                    if ((hitmode == Grobal2.HM_WIDEHIT) && (PWideHitSkill != null) && (targ.RaceServer >= Grobal2.RC_ANIMAL))
                    {
                        if (PWideHitSkill.Level < 3)
                        {
                            if (Abil.Level >= PWideHitSkill.pDef.NeedLevel[PWideHitSkill.Level])
                            {
                                TrainSkill(PWideHitSkill, 1);
                                if (!CheckMagicLevelup(PWideHitSkill))
                                {
                                    UpdateDelayMsgCheckParam1(this, Grobal2.RM_MAGIC_LVEXP, 0, PWideHitSkill.pDef.MagicId, PWideHitSkill.Level, PWideHitSkill.CurTrain, "", 3000);
                                }
                            }
                        }
                    }
                    // 八贱 氢惑 5 (堪拳搬)
                    if ((hitmode == Grobal2.HM_FIREHIT) && (PFireHitSkill != null) && (targ.RaceServer >= Grobal2.RC_ANIMAL))
                    {
                        if (PFireHitSkill.Level < 3)
                        {
                            if (Abil.Level >= PFireHitSkill.pDef.NeedLevel[PFireHitSkill.Level])
                            {
                                TrainSkill(PFireHitSkill, 1);
                                if (!CheckMagicLevelup(PFireHitSkill))
                                {
                                    UpdateDelayMsgCheckParam1(this, Grobal2.RM_MAGIC_LVEXP, 0, PFireHitSkill.pDef.MagicId, PFireHitSkill.Level, PFireHitSkill.CurTrain, "", 3000);
                                }
                            }
                        }
                    }
                    // 2003/03/15 脚痹公傍
                    // 八贱 氢惑 6 (堡浅曼)
                    if ((hitmode == Grobal2.HM_CROSSHIT) && (PCrossHitSkill != null) && (targ.RaceServer >= Grobal2.RC_ANIMAL))
                    {
                        if (PCrossHitSkill.Level < 3)
                        {
                            if (Abil.Level >= PCrossHitSkill.pDef.NeedLevel[PCrossHitSkill.Level])
                            {
                                TrainSkill(PCrossHitSkill, 1);
                                if (!CheckMagicLevelup(PCrossHitSkill))
                                {
                                    UpdateDelayMsgCheckParam1(this, Grobal2.RM_MAGIC_LVEXP, 0, PCrossHitSkill.pDef.MagicId, PCrossHitSkill.Level, PCrossHitSkill.CurTrain, "", 3000);
                                }
                            }
                        }
                    }
                    // 八贱 氢惑 7 (街锋曼)
                    if ((hitmode == Grobal2.HM_TWINHIT) && (PTwinHitSkill != null) && (targ.RaceServer >= Grobal2.RC_ANIMAL))
                    {
                        if (PTwinHitSkill.Level < 3)
                        {
                            if (Abil.Level >= PTwinHitSkill.pDef.NeedLevel[PTwinHitSkill.Level])
                            {
                                TrainSkill(PTwinHitSkill, 1);
                                if (!CheckMagicLevelup(PTwinHitSkill))
                                {
                                    UpdateDelayMsgCheckParam1(this, Grobal2.RM_MAGIC_LVEXP, 0, PTwinHitSkill.pDef.MagicId, PTwinHitSkill.Level, PTwinHitSkill.CurTrain, "", 3000);
                                }
                            }
                        }
                    }
                    // 八贱氢惑 8 (荤磊饶)
                    if ((hitmode == Grobal2.HM_STONEHIT) && (PStoneHitSkill != null))
                    {
                        if (PStoneHitSkill.Level < 3)
                        {
                            if (Abil.Level >= PStoneHitSkill.pDef.NeedLevel[PStoneHitSkill.Level])
                            {
                                TrainSkill(PStoneHitSkill, 1);
                                if (!CheckMagicLevelup(PStoneHitSkill))
                                {
                                    UpdateDelayMsgCheckParam1(this, Grobal2.RM_MAGIC_LVEXP, 0, PStoneHitSkill.pDef.MagicId, PStoneHitSkill.Level, PStoneHitSkill.CurTrain, "", 3000);
                                }
                            }
                        }
                    }
                    // 嘎酒具 己傍
                    result = true;
                }
                if (weapondamage > 0)
                {
                    if (UseItems[Grobal2.U_WEAPON].Index > 0)
                    {
                        // 公扁甫 瞒绊 乐栏搁
                        DoDamageWeapon(weapondamage);
                    }
                }
                // 阁胶磐茄抛绰 流立傈崔秦具 窃..
                if (targ.RaceServer != Grobal2.RC_USERHUMAN)
                {
                    targ.SendMsg(targ, Grobal2.RM_STRUCK, (short)dam, targ.WAbil.HP, targ.WAbil.MaxHP, this.ActorId, "");
                }
            }
            catch
            {
                M2Share.MainOutMessage("[Exception] TCreature._Attack");
            }
            return result;
        }

        public void HitHit_IdentifyWeapon(ref TUserItem ui)
        {
            if (ui.Desc[0] + ui.Desc[1] + ui.Desc[2] < 20)
            {
                switch (ui.Desc[10])
                {
                    // Modify the A .. B: 10 .. 13
                    case 10:
                        ui.Desc[0] = (byte)(ui.Desc[0] + (ui.Desc[10] - 9));
                        break;
                    // Modify the A .. B: 20 .. 23
                    case 20:
                        ui.Desc[1] = (byte)(ui.Desc[1] + (ui.Desc[10] - 19));
                        break;
                    // Modify the A .. B: 30 .. 33
                    case 30:
                        ui.Desc[2] = (byte)(ui.Desc[2] + (ui.Desc[10] - 29));
                        break;
                    case 1:
                        ui.Index = 0;
                        break;
                }
            }
            else
            {
                ui.Index = 0;
            }
            ui.Desc[10] = 0;
        }

        public void HitHit_CheckWeaponUpgradeResult()
        {
            TUserItem oldweapon;
            TUserHuman hum;
            if (UseItems[Grobal2.U_WEAPON].Desc[10] != 0)
            {
                oldweapon = UseItems[Grobal2.U_WEAPON];
                HitHit_IdentifyWeapon(ref UseItems[Grobal2.U_WEAPON]);
                if (UseItems[Grobal2.U_WEAPON].Index == 0)
                {
                    SysMsg("您的武器已经碎裂。", 0);
                    hum = this as TUserHuman;
                    hum.SendDelItem(oldweapon);
                    SendRefMsg(Grobal2.RM_BREAKWEAPON, 0, 0, 0, 0, "");
                    M2Share.AddUserLog("21\09" + MapName + "\09" + CX.ToString() + "\09" + CY.ToString() + "\09" + UserName + "\09" + M2Share.UserEngine.GetStdItemName(oldweapon.Index) + "\09" + oldweapon.MakeIndex.ToString() + "\09" + "1\09" + ItemOptionToStr(oldweapon.Desc));
                    FeatureChanged();
                }
                else
                {
                    SysMsg("您的武器已经升级了。", 1);
                    hum = this as TUserHuman;
                    hum.SendUpdateItem(UseItems[Grobal2.U_WEAPON]);
                    M2Share.AddUserLog("20\09" + MapName + "\09" + CX.ToString() + "\09" + CY.ToString() + "\09" + UserName + "\09" + M2Share.UserEngine.GetStdItemName(UseItems[Grobal2.U_WEAPON].Index) + "\09" + UseItems[Grobal2.U_WEAPON].MakeIndex.ToString() + "\09" + "1\09" + ItemOptionToStr(UseItems[Grobal2.U_WEAPON].Desc));
                    RecalcAbilitys();
                    SendMsg(this, Grobal2.RM_ABILITY, 0, 0, 0, 0, "");
                    SendMsg(this, Grobal2.RM_SUBABILITY, 0, 0, 0, 0, "");
                }
            }
        }

        public int HitHit_GetSWSpell(TUserMagic pum)
        {
            int result;
            result = HUtil32.MathRound(pum.pDef.Spell / (pum.pDef.MaxTrainLevel + 1) * (pum.Level + 1));
            return result;
        }

        public void HitHit(TCreature target, short hitmode, short dir)
        {
            int msg;
            TCreature targ;
            bool bopower;
            bool bofire;
            if (hitmode == Grobal2.HM_WIDEHIT)
            {
                if (PWideHitSkill != null)
                {
                    if (WAbil.MP > 0)
                    {
                        DamageSpell(HitHit_GetSWSpell(PWideHitSkill) + PWideHitSkill.pDef.DefSpell);
                        HealthSpellChanged();
                    }
                    else
                    {
                        hitmode = Grobal2.RM_HIT;
                    }
                    // 付仿绝澜...
                }
            }
            // 2003/03/15 脚痹公傍
            if (hitmode == Grobal2.HM_CROSSHIT)
            {
                if (PCrossHitSkill != null)
                {
                    if (WAbil.MP > 0)
                    {
                        DamageSpell(HitHit_GetSWSpell(PCrossHitSkill) + PCrossHitSkill.pDef.DefSpell);
                        HealthSpellChanged();
                    }
                    else
                    {
                        hitmode = Grobal2.RM_HIT;
                    }
                    // 付仿绝澜...
                }
            }
            if (hitmode == Grobal2.HM_TWINHIT)
            {
                if (PTwinHitSkill != null)
                {
                    if (WAbil.MP > 0)
                    {
                        DamageSpell(HitHit_GetSWSpell(PTwinHitSkill) + PTwinHitSkill.pDef.DefSpell);
                        HealthSpellChanged();
                    }
                    else
                    {
                        hitmode = Grobal2.RM_HIT;
                    }
                    // 付仿绝澜...
                }
            }
            // 规氢栏肺 模促.
            this.Dir = (byte)dir;
            if (target == null)
            {
                targ = GetFrontCret();
            }
            else
            {
                targ = target;
            }
            if (targ != null)
            {
                if (UseItems[Grobal2.U_WEAPON].Index != 0)
                {
                    // 力访捞 场抄 公扁狼 抛胶飘(己傍咯何)
                    HitHit_CheckWeaponUpgradeResult();
                }
            }
            bopower = BoAllowPowerHit;
            bofire = BoAllowFireHit;
            // _attack 俊辑 秦力 凳
            if (_Attack(hitmode, targ))
            {
                SelectTarget(targ);
            }
            msg = Grobal2.RM_HIT;
            if (RaceServer == Grobal2.RC_USERHUMAN)
            {
                msg = Grobal2.RM_HIT;
                switch (hitmode)
                {
                    case Grobal2.HM_HIT:
                        msg = Grobal2.RM_HIT;
                        break;
                    case Grobal2.HM_HEAVYHIT:
                        msg = Grobal2.RM_HEAVYHIT;
                        break;
                    case Grobal2.HM_BIGHIT:
                        msg = Grobal2.RM_BIGHIT;
                        break;
                    case Grobal2.HM_POWERHIT:
                        if (bopower)
                        {
                            msg = Grobal2.RM_POWERHIT;
                        }
                        break;
                    case Grobal2.HM_LONGHIT:
                        if (PLongHitSkill != null)
                        {
                            msg = Grobal2.RM_LONGHIT;
                        }
                        break;
                    case Grobal2.HM_WIDEHIT:
                        if (PWideHitSkill != null)
                        {
                            msg = Grobal2.RM_WIDEHIT;
                        }
                        break;
                    case Grobal2.HM_FIREHIT:
                        if (bofire)
                        {
                            msg = Grobal2.RM_FIREHIT;
                        }
                        break;
                    case Grobal2.HM_CROSSHIT:
                        // 2003/03/15 脚痹公傍
                        if (PCrossHitSkill != null)
                        {
                            msg = Grobal2.RM_CROSSHIT;
                        }
                        break;
                    case Grobal2.HM_TWINHIT:
                        if (PTwinHitSkill != null)
                        {
                            msg = Grobal2.RM_TWINHIT;
                        }
                        break;
                }
            }
            // SendRefMsg (msg, self.Dir, CX, CY, 0, '');
            HitMotion(msg, this.Dir, CX, CY);
        }

        public void HitMotion(int hitmsg, byte hitdir, int x, int y)
        {
            SendRefMsg((short)hitmsg, hitdir, x, y, 0, "");
        }

        public void HitHit2(TCreature target, int hitpwr, int magpwr, bool all)
        {
            HitHitEx2(target, Grobal2.RM_HIT, hitpwr, magpwr, all);
        }

        public void HitHitEx2(TCreature target, int rmmsg, int hitpwr, int magpwr, bool all)
        {
            int i;
            int dam;
            ArrayList list;
            TCreature cret;
            this.Dir = M2Share.GetNextDirection(CX, CY, target.CX, target.CY);
            list = new ArrayList();
            PEnvir.GetAllCreature(target.CX, target.CY, true, list);
            for (i = 0; i < list.Count; i++)
            {
                cret = list[i] as TCreature;
                if (IsProperTarget(cret))
                {
                    dam = 0;
                    dam = dam + cret.GetHitStruckDamage(this, hitpwr);
                    dam = dam + cret.GetMagStruckDamage(this, magpwr);
                    if (dam > 0)
                    {
                        cret.StruckDamage(dam, this);
                        cret.SendDelayMsg(Grobal2.RM_STRUCK, Grobal2.RM_REFMESSAGE, (short)dam, cret.WAbil.HP, cret.WAbil.MaxHP, this.ActorId, "", 200);
                    }
                }
            }
            list.Free();
            SendRefMsg((short)rmmsg, this.Dir, CX, CY, 0, "");
        }

        // Result: 角力肺 剐赴 沫
        public int CharPushed(int ndir, int pushcount)
        {
            int result;
            int i;
            short nx = 0;
            short ny = 0;
            int olddir;
            short oldx;
            short oldy;
            bool flag;
            result = 0;
            olddir = Dir;
            oldx = CX;
            oldy = CY;
            Dir = (byte)ndir;
            flag = false;
            if (RaceServer == Grobal2.RC_USERHUMAN)
            {
                if ((this as TUserHuman).StallMgr.OnSale)
                {
                    return result;
                }
            }
            for (i = 0; i < pushcount; i++)
            {
                M2Share.GetFrontPosition(this, ref nx, ref ny);
                if (PEnvir.CanWalk(nx, ny, false))
                {
                    if (PEnvir.MoveToMovingObject(CX, CY, this, nx, ny, false) > 0)
                    {
                        CX = nx;
                        CY = ny;
                        SendRefMsg(Grobal2.RM_PUSH, (short)M2Share.GetBack(ndir), CX, CY, 0, "");
                        result++;
                        if (RaceServer >= Grobal2.RC_ANIMAL)
                        {
                            WalkTime = WalkTime + 800;
                        }
                        flag = true;
                    }
                    else
                    {
                        break;
                    }
                }
                else
                {
                    break;
                }
            }
            if (flag)
            {
                Dir = (byte)M2Share.GetBack(ndir);
            }
            return result;
        }

        public bool CharRushRush_CanPush(TCreature cret, int rushlevel)
        {
            bool result = false;
            if ((Abil.Level > cret.Abil.Level) && (!cret.StickMode))
            {
                int levelgap = Abil.Level - cret.Abil.Level;
                if (new System.Random(20).Next() < 6 + rushlevel * 3 + levelgap)
                {
                    if (IsProperTarget(cret))
                    {
                        result = true;
                    }
                }
            }
            return result;
        }

        public bool CharRushRush(byte ndir, int rushlevel, bool isHumanSkill)
        {
            bool result;
            int i;
            short nx = 0;
            short ny = 0;
            int damage;
            int damagelevel;
            int mydamagelevel;
            TCreature cret;
            TCreature cret2;
            TCreature attackcret;
            bool crash;
            result = false;
            crash = true;
            Dir = ndir;
            attackcret = null;
            damagelevel = rushlevel + 1;
            mydamagelevel = damagelevel;
            cret = GetFrontCret();
            if (RaceServer == Grobal2.RC_USERHUMAN)
            {
                if ((this as TUserHuman).StallMgr.OnSale)
                {
                    return result;
                }
            }
            if (cret != null)
            {
                for (i = 0; i <= _MAX(2, rushlevel + 1); i++)
                {
                    cret = GetFrontCret();
                    if (cret != null)
                    {
                        mydamagelevel = 0;
                        if (CharRushRush_CanPush(cret, rushlevel))
                        {
                            if (rushlevel >= 3)
                            {
                                if (M2Share.GetNextPosition(PEnvir, CX, CY, Dir, 2, ref nx, ref ny))
                                {
                                    cret2 = PEnvir.GetCreature(nx, ny, true) as TCreature;
                                    if (cret2 != null)
                                    {
                                        if (CharRushRush_CanPush(cret2, rushlevel))
                                        {
                                            cret2.CharPushed(Dir, 1);
                                            cret2.PushedCount++;
                                        }
                                    }
                                }
                            }
                            attackcret = cret;
                            if (cret.CharPushed(Dir, 1) == 1)
                            {
                                cret.PushedCount++;
                                M2Share.GetFrontPosition(this, ref nx, ref ny);
                                if (PEnvir.MoveToMovingObject(CX, CY, this, nx, ny, false) > 0)
                                {
                                    CX = nx;
                                    CY = ny;
                                    SendRefMsg(Grobal2.RM_RUSH, ndir, CX, CY, 0, "");
                                    crash = false;
                                    result = true;
                                }
                                damagelevel -= 1;
                            }
                            else
                            {
                                break;
                            }
                        }
                        else
                        {
                            break;
                        }
                    }
                }
            }
            else
            {
                crash = false;
                for (i = 0; i <= _MAX(2, rushlevel + 1); i++)
                {
                    M2Share.GetFrontPosition(this, ref nx, ref ny);
                    if (PEnvir.MoveToMovingObject(CX, CY, this, nx, ny, false) > 0)
                    {
                        CX = nx;
                        CY = ny;
                        SendRefMsg(Grobal2.RM_RUSH, ndir, CX, CY, 0, "");
                        mydamagelevel -= 1;
                    }
                    else
                    {
                        if (PEnvir.CanWalk(nx, ny, true))
                        {
                            mydamagelevel = 0;
                        }
                        else
                        {
                            crash = true;
                        }
                        break;
                    }
                }
            }
            if ((attackcret != null) && isHumanSkill)
            {
                if (damagelevel < 0)
                {
                    damagelevel = 0;
                }
                damage = (1 + damagelevel) * 4 + new System.Random((1 + damagelevel) * 5).Next();
                damage = attackcret.GetHitStruckDamage(this, damage);
                attackcret.StruckDamage(damage);
                attackcret.SendRefMsg(Grobal2.RM_STRUCK, (short)damage, attackcret.WAbil.HP, attackcret.WAbil.MaxHP, this.ActorId, "");
                if (attackcret.RaceServer != Grobal2.RC_USERHUMAN)
                {
                    attackcret.SendMsg(attackcret, Grobal2.RM_STRUCK, (short)damage, attackcret.WAbil.HP, attackcret.WAbil.MaxHP, this.ActorId, "");
                }
            }
            if (crash)
            {
                M2Share.GetFrontPosition(this, ref nx, ref ny);
                SendRefMsg(Grobal2.RM_RUSHKUNG, Dir, nx, ny, 0, "");
                if (isHumanSkill)
                {
                    SysMsg("缺乏冲撞力量。", 0);
                }
            }
            if ((mydamagelevel > 0) && isHumanSkill)
            {
                if (damagelevel < 0)
                {
                    damagelevel = 0;
                }
                damage = (1 + damagelevel) * 5 + new System.Random((1 + damagelevel) * 5).Next();
                damage = GetHitStruckDamage(this, damage);
                StruckDamage(damage);
                if (crash && (LastHiter != null))
                {
                    LastHiter = null;
                }
                SendRefMsg(Grobal2.RM_STRUCK, (short)damage, WAbil.HP, WAbil.MaxHP, 0, "");
            }
            return result;
        }

        public bool CharDrawingRush_CanPush(TCreature cret, int rushlevel)
        {
            bool result;
            int levelgap;
            result = false;
            if ((Abil.Level > cret.Abil.Level) && (!cret.StickMode))
            {
                levelgap = Abil.Level - cret.Abil.Level;
                if (new System.Random(20).Next() < 6 + rushlevel * 3 + levelgap)
                {
                    if (IsProperTarget(cret))
                    {
                        result = true;
                    }
                }
            }
            return result;
        }

        public bool CharDrawingRush(int ndir, int rushlevel, bool isHumanSkill)
        {
            bool result;
            int i;
            short nx = 0;
            short ny = 0;
            int damage;
            int damagelevel;
            int mydamagelevel;
            TCreature cret;
            TCreature attackcret;
            bool crash;
            result = false;
            crash = true;
            Dir = (byte)ndir;
            attackcret = null;
            damagelevel = rushlevel + 1;
            mydamagelevel = damagelevel;
            cret = GetFrontCret();
            if (cret != null)
            {

            }
            else
            {
                crash = false;
                for (i = 0; i <= _MAX(2, rushlevel + 1); i++)
                {
                    M2Share.GetFrontPosition(this, ref nx, ref ny);
                    if (PEnvir.MoveToMovingObject(CX, CY, this, nx, ny, false) > 0)
                    {
                        CX = nx;
                        CY = ny;
                        SendRefMsg(Grobal2.RM_RUSH, (short)ndir, CX, CY, 0, "");
                        mydamagelevel -= 1;
                    }
                    else
                    {
                        if (PEnvir.CanWalk(nx, ny, true))
                        {
                            mydamagelevel = 0;
                        }
                        else
                        {
                            crash = true;
                        }
                        break;
                    }
                }
            }
            if ((attackcret != null) && isHumanSkill)
            {
                if (damagelevel < 0)
                {
                    damagelevel = 0;
                }
                damage = (1 + damagelevel) * 4 + new System.Random((1 + damagelevel) * 5).Next();
                damage = attackcret.GetHitStruckDamage(this, damage);
                attackcret.StruckDamage(damage);
                attackcret.SendRefMsg(Grobal2.RM_STRUCK, (short)damage, attackcret.WAbil.HP, attackcret.WAbil.MaxHP, this.ActorId, "");
                if (attackcret.RaceServer != Grobal2.RC_USERHUMAN)
                {
                    attackcret.SendMsg(attackcret, Grobal2.RM_STRUCK, (short)damage, attackcret.WAbil.HP, attackcret.WAbil.MaxHP, this.ActorId, "");
                }
            }
            if (crash)
            {
                M2Share.GetFrontPosition(this, ref nx, ref ny);
                SendRefMsg(Grobal2.RM_RUSHKUNG, Dir, nx, ny, 0, "");
                if (isHumanSkill)
                {
                    SysMsg("缺乏冲撞力量。", 0);
                }
            }
            if ((mydamagelevel > 0) && isHumanSkill)
            {
                if (damagelevel < 0)
                {
                    damagelevel = 0;
                }
                damage = (1 + damagelevel) * 5 + new System.Random((1 + damagelevel) * 5).Next();
                damage = GetHitStruckDamage(this, damage);
                StruckDamage(damage);
                if (crash && (LastHiter != null))
                {
                    LastHiter = null;
                }
                // wparam
                // lparam1
                // lparam2
                // hiter
                SendRefMsg(Grobal2.RM_STRUCK, (short)damage, WAbil.HP, WAbil.MaxHP, 0, "");
            }
            return result;
        }

        // 缠绢寸辫
        public int SiegeCount()
        {
            int result;
            int i;
            TCreature cret;
            result = 0;
            for (i = 0; i < VisibleActors.Count; i++)
            {
                cret = VisibleActors[i].cret as TCreature;
                if (!cret.Death)
                {
                    if ((Math.Abs(CX - cret.CX) <= 1) && (Math.Abs(CY - cret.CY) <= 1))
                    {
                        result++;
                    }
                }
            }
            return result;
        }

        // 割 付府茄抛 笛矾 阶咯廉 乐绰瘤
        public int SiegeLockCount()
        {
            int result;
            int i;
            int j;
            int n;
            n = 0;
            for (i = -1; i <= 1; i++)
            {
                for (j = -1; j <= 1; j++)
                {
                    if ((!PEnvir.CanWalk(CX + i, CY + j, false)) && (!((i == 0) && (j == 0))))
                    {
                        n++;
                    }
                }
            }
            result = n;
            return result;
        }

        // 备籍俊 爱躯绰瘤?
        // 刀栏肺 傍拜窃.
        public bool MakePoison(int poison, int sec, int poisonlv)
        {
            bool result;
            int old;
            result = false;
            // 刀俊 吝刀登瘤 臼阑 炼扒捞 乐绰啊 八荤..
            sec = sec - PoisonRecover;
            if (sec <= 0)
            {
                return result;
            }
            if (poison >= 0 && poison <= Grobal2.STATUSARR_SIZE - 1)
            {
                old = CharStatus;
                if (StatusArr[poison] > 0)
                {
                    if (sec > StatusArr[poison])
                    {
                        StatusArr[poison] = (ushort)sec;
                    }
                }
                else
                {
                    StatusArr[poison] = (ushort)sec;
                }
                StatusTimes[poison] = HUtil32.GetTickCount();
                CharStatus = GetCharStatus();
                if (poison == Grobal2.POISON_DAMAGEARMOR)
                {
                    RedPoisonLevel = (byte)_MIN(poisonlv, 256);
                    // 刀俊 措茄 眠啊 单固瘤 加己 眠啊(sonmg 2005/10/28) 弧刀 2.0
                    if (PlusPoisonFactor != 0)
                    {
                        RedPoisonLevel = (byte)(RedPoisonLevel * (PlusPoisonFactor / 100));
                    }
                }
                PoisonLevel = (byte)_MIN(poisonlv, 256);
                // 刀俊 措茄 眠啊 单固瘤 加己 眠啊(sonmg 2005/10/28) 踌刀 2硅
                if (PlusPoisonFactor != 0)
                {
                    PoisonLevel = (byte)(PoisonLevel * (PlusPoisonFactor / 100));
                }
                if (old != CharStatus)
                {
                    CharStatusChanged();
                }
                if (RaceServer == Grobal2.RC_USERHUMAN)
                {
                    SysMsg("您中毒了。", 0);
                }
                result = true;
            }
            return result;
        }

        public void ClearPoison(int poison)
        {
            int old;
            if (poison >= 0 && poison <= Grobal2.STATUSARR_SIZE - 1)
            {
                old = CharStatus;
                if (StatusArr[poison] > 0)
                {
                    StatusArr[poison] = 0;
                }
                CharStatus = GetCharStatus();
                if (old != CharStatus)
                {
                    CharStatusChanged();
                }
            }
        }

        public TCreature GetFrontCret()
        {
            short fx = 0;
            short fy = 0;
            TCreature result = null;
            if (M2Share.GetFrontPosition(this, ref fx, ref fy))
            {
                result = PEnvir.GetCreature(fx, fy, true) as TCreature;
            }
            return result;
        }

        public TCreature GetBackCret()
        {
            short fx = 0;
            short fy = 0;
            TCreature result = null;
            if (M2Share.GetBackPosition(this, ref fx, ref fy))
            {
                result = PEnvir.GetCreature(fx, fy, true) as TCreature;
            }
            return result;
        }

        public bool CretInNearXY(TCreature tagcret, int xx, int yy)
        {
            TMapInfo pm = null;
            bool result = false;
            for (var i = xx - 1; i <= xx + 1; i++)
            {
                for (var j = yy - 1; j <= yy + 1; j++)
                {
                    bool inrange = PEnvir.GetMapXY(i, j, ref pm);
                    if (inrange)
                    {
                        if (pm.OBJList != null)
                        {
                            for (var k = 0; k < pm.OBJList.Count; k++)
                            {
                                if (((TAThing)pm.OBJList[k]).Shape == Grobal2.OS_MOVINGOBJECT)
                                {
                                    TCreature cret = ((TAThing)pm.OBJList[k]).AObject as TCreature;
                                    if (cret != null)
                                    {
                                        if ((!cret.BoGhost) && (cret == tagcret))
                                        {
                                            result = true;
                                            return result;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return result;
        }

        public TCreature MakeSlave(string sname, int slevel, int max_slave, int royaltysec)
        {
            short nx = 0;
            short ny = 0;
            TCreature mon;
            int AddPlus;
            TCreature result = null;
            try
            {
                AddPlus = 0;
                if ((GetExistSlave(M2Share.__AngelMob) != null) || (sname == M2Share.__AngelMob))
                {
                    AddPlus = 1;
                }
                else
                {
                    AddPlus = 0;
                }
                if (SlaveList.Count < (max_slave + AddPlus))
                {
                    M2Share.GetFrontPosition(this, ref nx, ref ny);
                    mon = M2Share.UserEngine.AddCreatureSysop(PEnvir.MapName, nx, ny, sname);
                    if (mon != null)
                    {
                        mon.Master = this;
                        mon.MasterRoyaltyTime = GetTickCount + (long)royaltysec * 1000;
                        mon.SlaveMakeLevel = (byte)slevel;
                        mon.SlaveExpLevel = (byte)slevel;
                        mon.MasterFeature = GetRelFeature(this);
                        mon.RecalcAbilitys();
                        if (mon.WAbil.HP < mon.WAbil.MaxHP)
                        {
                            mon.WAbil.HP = (short)(mon.WAbil.HP + (mon.WAbil.MaxHP - mon.WAbil.HP) / 2);
                        }
                        mon.ChangeNameColor();
                        SlaveList.Add(mon);
                        result = mon;
                    }
                }
            }
            catch
            {
                M2Share.MainOutMessage("EXCEPT MAKESLAVE");
            }
            return result;
        }

        // 2003/06/12 浇饭捞宏 菩摹
        // 2003/06/12 浇饭捞宏 菩摹
        public void ClearAllSlaves()
        {
            int i;
            for (i = 0; i < SlaveList.Count; i++)
            {
                if (!SlaveList[i].Death)
                {
                    SlaveList[i].BoDisapear = true;
                    SlaveList[i].MakeGhost(4);
                    // 何窍甸阑 肯傈洒 绝矩促. 林肺 辑滚捞悼窍绰 版快 荤侩
                }
            }
        }

        public void KillAllSlaves()
        {
            int i;
            for (i = 0; i < SlaveList.Count; i++)
            {
                if (!SlaveList[i].Death)
                {
                    SlaveList[i].WAbil.HP = 0;
                    // Die
                }
            }
        }

        public bool ExistAttackSlaves()
        {
            bool result;
            int i;
            TCreature cret;
            // 傍拜 鸥百捞 乐绰 家券荐啊 乐栏搁 TRUE 绝栏搁 FALSE...
            result = false;
            for (i = 0; i < SlaveList.Count; i++)
            {
                cret = SlaveList[i];
                if (!cret.Death)
                {
                    if (cret.TargetCret != null)
                    {
                        if (cret.TargetCret.RaceServer == Grobal2.RC_USERHUMAN)
                        {
                            result = true;
                            break;
                        }
                    }
                }
            }
            return result;
        }

        public TCreature GetExistSlave(string MonName_)
        {
            TCreature result = null;
            TCreature TempCret;
            try
            {
                for (var i = 0; i < SlaveList.Count; i++)
                {
                    TempCret = SlaveList[i];
                    if ((TempCret != null) && (!TempCret.Death) && (!TempCret.BoDisapear) && (!TempCret.BoGhost) && (TempCret.UserName.ToLower().CompareTo(MonName_.ToLower()) == 0))
                    {
                        result = TempCret;
                        return result;
                    }
                }
            }
            catch
            {
                M2Share.MainOutMessage("EXCEPTION GETExistSlave");
            }
            return result;
        }

        public bool EnableRecallMob(TCreature TargetMob, int SkillLevel)
        {
            bool result;
            int i;
            int KingSlaveCount;
            int AddPlus;
            result = false;
            KingSlaveCount = 0;
            AddPlus = 0;
            if ((!TargetMob.NoMaster) && (TargetMob.LifeAttrib == Grobal2.LA_CREATURE) && (TargetMob.RaceServer != Grobal2.RC_CLONE) && (TargetMob.RaceServer != Grobal2.RC_ANGEL) && (TargetMob.Abil.Level < M2Share.MAXKINGLEVEL - 1))
            {
                // 蜡历 52饭骇 巩力 荐沥(sonmg 2004/09/08)
                // 各狼 饭骇捞 50捞惑牢 各篮 茄 付府父 部角 荐 乐促.(滴 付府 捞惑篮 犬伏利侩)
                if (TargetMob.Abil.Level >= 50)
                {
                    for (i = 0; i < SlaveList.Count; i++)
                    {
                        if (SlaveList[i].Abil.Level >= 50)
                        {
                            KingSlaveCount++;
                        }
                    }
                    // 泅犁 家券各 吝俊 饭骇捞 50捞惑牢 各狼 荐付促 1/3究 犬伏 皑家.
                    if (new System.Random(3 * KingSlaveCount).Next() > 0)
                    {
                        result = false;
                        return result;
                    }
                }
                // 券康茄龋绰 1付府父 家券等促.
                if (TargetMob.RaceServer == Grobal2.RC_GHOST_TIGER)
                {
                    if (SlaveList.Count > 0)
                    {
                        result = false;
                        return result;
                    }
                }
                else
                {
                    // 券康茄龋绰 促弗逞捞 部寂廉 乐栏搁... 部角荐 绝促.
                    if (SlaveList.Count == 1)
                    {
                        if (SlaveList[0].RaceServer == Grobal2.RC_GHOST_TIGER)
                        {
                            result = false;
                            return result;
                        }
                    }
                    // 老馆利栏肺 部角荐 乐绰 阁胶磐 荐 眉农
                    if (SlaveList.Count >= (2 + SkillLevel + AddPlus))
                    {
                        result = false;
                        return result;
                    }
                }
                result = true;
            }
            return result;
        }

        public bool IsGroupMember(TCreature cret)
        {
            bool result = false;
            if (GroupOwner != null)
            {
                for (var i = 0; i < GroupOwner.GroupMembers.Count; i++)
                {
                    if (GroupOwner.GroupMembers[i] == cret)
                    {
                        result = true;
                        break;
                    }
                }
            }
            return result;
        }

        public bool CheckGroupValid()
        {
            bool result = true;
            if (GroupMembers.Count <= 1)
            {
                GroupMsg("您的队伍被解散了。");
                GroupMembers.Clear();
                GroupOwner = null;
                result = false;
            }
            return result;
        }

        public void DelGroupMember(TCreature who)
        {
            TCreature cret;
            TUserHuman hum;
            if (GroupOwner != who)
            {
                for (var i = 0; i < GroupMembers.Count; i++)
                {
                    cret = GroupMembers[i];
                    if (cret == who)
                    {
                        who.LeaveGroup();
                        GroupMembers.RemoveAt(i);
                        break;
                    }
                }
                if (this.RaceServer == Grobal2.RC_USERHUMAN)
                {
                    hum = this as TUserHuman;
                    if (!CheckGroupValid())
                    {
                        hum.SendMsg(this, Grobal2.RM_GROUPCANCEL, 0, 0, 0, 0, "");
                    }
                    hum.RefreshGroupMembers();
                }
            }
            else
            {
                for (var i = GroupMembers.Count - 1; i >= 0; i--)
                {
                    hum = GroupMembers[i] as TUserHuman;
                    if ((hum != null) && (hum.RaceServer == Grobal2.RC_USERHUMAN))
                    {
                        hum.SendMsg(this, Grobal2.RM_GROUPCANCEL, 0, 0, 0, 0, "");
                        hum.LeaveGroup();
                        GroupMembers.RemoveAt(i);
                    }
                }
                if (this.RaceServer == Grobal2.RC_USERHUMAN)
                {
                    hum = this as TUserHuman;
                    GroupMsg("你的队伍被解散了。");
                    GroupMembers.Clear();
                    GroupOwner = null;
                    hum.SendMsg(this, Grobal2.RM_GROUPCANCEL, 0, 0, 0, 0, "");
                    hum.RefreshGroupMembers();
                }
            }
        }

        public void EnterGroup(TCreature gowner)
        {
            GroupOwner = gowner;
            GroupMsg(UserName + "加入队伍。");
        }

        public void LeaveGroup()
        {
            GroupMsg(UserName + "退出队伍。");
            SendMsg(this, Grobal2.RM_GROUPCANCEL, 0, 0, 0, 0, "");
            GroupOwner = null;
        }

        public void DenyGroup()
        {
            if (GroupOwner != null)
            {
                if (GroupOwner != this)
                {
                    // 呕硼
                    GroupOwner.DelGroupMember(this);
                    AllowGroup = false;
                }
                else
                {
                    // 救凳
                    SysMsg("如果您想退出，请使用组队的删除键。", 0);
                }
            }
            else
            {
                AllowGroup = false;
            }
        }

        // ----------------------------------------------------------------
        public bool TargetInAttackRange(TCreature target, ref byte targdir)
        {
            bool result;
            result = false;
            if ((target.CX >= (this.CX - 1)) && (target.CX <= (this.CX + 1)) && (target.CY >= (this.CY - 1)) && (target.CY <= (this.CY + 1)) && !((target.CX == this.CX) && (target.CY == this.CY)))
            {
                result = true;
                while (true)
                {
                    if ((target.CX == (this.CX - 1)) && (target.CY == this.CY))
                    {
                        targdir = Grobal2.DR_LEFT;
                        break;
                    }
                    if ((target.CX == (this.CX + 1)) && (target.CY == this.CY))
                    {
                        targdir = Grobal2.DR_RIGHT;
                        break;
                    }
                    if ((target.CX == this.CX) && (target.CY == (this.CY - 1)))
                    {
                        targdir = Grobal2.DR_UP;
                        break;
                    }
                    if ((target.CX == this.CX) && (target.CY == (this.CY + 1)))
                    {
                        targdir = Grobal2.DR_DOWN;
                        break;
                    }
                    if ((target.CX == this.CX - 1) && (target.CY == this.CY - 1))
                    {
                        targdir = Grobal2.DR_UPLEFT;
                        break;
                    }
                    if ((target.CX == this.CX + 1) && (target.CY == this.CY - 1))
                    {
                        targdir = Grobal2.DR_UPRIGHT;
                        break;
                    }
                    if ((target.CX == this.CX - 1) && (target.CY == this.CY + 1))
                    {
                        targdir = Grobal2.DR_DOWNLEFT;
                        break;
                    }
                    if ((target.CX == this.CX + 1) && (target.CY == this.CY + 1))
                    {
                        targdir = Grobal2.DR_DOWNRIGHT;
                        break;
                    }
                    targdir = 0;
                    // 抗寇,
                    break;
                }
            }
            return result;
        }

        public bool TargetInSpitRange(TCreature target, ref byte targdir)
        {
            bool result;
            int nx = 0;
            int ny = 0;
            result = false;
            if ((Math.Abs(target.CX - CX) <= 2) && (Math.Abs(target.CY - CY) <= 2))
            {
                nx = target.CX - CX;
                ny = target.CY - CY;
                if ((Math.Abs(nx) <= 1) && (Math.Abs(ny) <= 1))
                {
                    TargetInAttackRange(target, ref targdir);
                    result = true;
                }
                else
                {
                    nx = nx + 2;
                    ny = ny + 2;
                    if (nx >= 0 && nx <= 4 && ny >= 0 && ny <= 4)
                    {
                        targdir = M2Share.GetNextDirection(CX, CY, target.CX, target.CY);
                        if (M2Share.SpitMap[targdir, ny, nx] == 1)
                        {
                            result = true;
                        }
                    }
                }
            }
            return result;
        }

        public bool TargetInCrossRange(TCreature target, ref byte targdir)
        {
            bool result;
            int nx = 0;
            int ny = 0;
            result = false;
            if ((Math.Abs(target.CX - CX) <= 2) && (Math.Abs(target.CY - CY) <= 2))
            {
                nx = target.CX - CX;
                ny = target.CY - CY;
                if ((Math.Abs(nx) <= 1) && (Math.Abs(ny) <= 1))
                {
                    TargetInAttackRange(target, ref targdir);
                    result = true;
                }
                else
                {
                    nx = nx + 2;
                    ny = ny + 2;
                    if (nx >= 0 && nx <= 4 && ny >= 0 && ny <= 4)
                    {
                        targdir = M2Share.GetNextDirection(CX, CY, target.CX, target.CY);
                        if (M2Share.CrossMap[targdir, ny, nx] == 1)
                        {
                            result = true;
                        }
                    }
                }
            }
            return result;
        }

        public bool WalkTo(byte dir, bool allowdup)
        {
            bool result;
            short prx = 0;
            short pry = 0;
            short nwx = 0;
            short nwy = 0;
            short masx = 0;
            short masy = 0;
            TEnvirnoment oldpenvir;
            bool flag;
            int down;
            result = false;
            down = 0;
            if (BoHolySeize)
            {
                return result;
            }
            try
            {
                down = 1;
                prx = CX;
                pry = CY;
                oldpenvir = PEnvir;
                this.Dir = dir;
                nwx = 0;
                nwy = 0;
                switch (dir)
                {
                    case Grobal2.DR_UP:
                        nwx = CX;
                        nwy = (short)(CY - 1);
                        break;
                    case Grobal2.DR_DOWN:
                        nwx = CX;
                        nwy = (short)(CY + 1);
                        break;
                    case Grobal2.DR_LEFT:
                        nwx = (short)(CX - 1);
                        nwy = CY;
                        break;
                    case Grobal2.DR_RIGHT:
                        nwx = (short)(CX + 1);
                        nwy = CY;
                        break;
                    case Grobal2.DR_UPLEFT:
                        nwx = (short)(CX - 1);
                        nwy = (short)(CY - 1);
                        break;
                    case Grobal2.DR_UPRIGHT:
                        nwx = (short)(CX + 1);
                        nwy = (short)(CY - 1);
                        break;
                    case Grobal2.DR_DOWNLEFT:
                        nwx = (short)(CX - 1);
                        nwy = (short)(CY + 1);
                        break;
                    case Grobal2.DR_DOWNRIGHT:
                        nwx = (short)(CX + 1);
                        nwy = (short)(CY + 1);
                        break;
                }
                down = 2;
                if ((nwx >= 0) && (nwx <= PEnvir.MapWidth - 1) && (nwy >= 0) && (nwy <= PEnvir.MapHeight - 1))
                {
                    down = 3;
                    flag = true;
                    if (BoFearFire)
                    {
                        down = 4;
                        if (!PEnvir.CanSafeWalk(nwx, nwy))
                        {
                            flag = false;
                        }
                    }
                    if (Master != null)
                    {
                        down = 5;
                        M2Share.GetNextPosition(Master.PEnvir, Master.CX, Master.CY, Master.Dir, 1, ref masx, ref masy);
                        if ((nwx == masx) && (nwy == masy))
                        {
                            flag = false;
                        }
                    }
                    if (flag)
                    {
                        down = 6;
                        if (PEnvir.MoveToMovingObject(CX, CY, this, nwx, nwy, allowdup) > 0)
                        {
                            CX = nwx;
                            CY = nwy;
                        }
                    }
                }
                if ((prx != CX) || (pry != CY))
                {
                    if (Walk(Grobal2.RM_WALK))
                    {
                        down = 7;
                        if (BoFixedHideMode)
                        {
                            if (BoHumHideMode)
                            {
                                StatusArr[Grobal2.STATE_TRANSPARENT] = 1;
                            }
                        }
                        result = true;
                    }
                    else
                    {
                        down = 8;
                        if (1 == PEnvir.DeleteFromMap(CX, CY, Grobal2.OS_MOVINGOBJECT, this))
                        {
                            PEnvir = oldpenvir;
                            CX = prx;
                            CY = pry;
                            if (null == PEnvir.AddToMap(CX, CY, Grobal2.OS_MOVINGOBJECT, this))
                            {
                                M2Share.MainOutMessage("NOT ADDTOMAP WorkTo:" + PEnvir.MapName + "," + CX.ToString() + "," + CY.ToString());
                            }
                        }
                    }
                }
            }
            catch
            {
                M2Share.MainOutMessage("[Exception] TCreatre.WalkTo:" + this.UserName + "," + down.ToString() + ":" + CX.ToString() + "," + CY.ToString() + "," + dir.ToString());
            }
            return result;
        }

        public bool RunTo(int dir, bool allowdup)
        {
            short prx;
            short pry;
            bool result = false;
            try
            {
                prx = CX;
                pry = CY;
                this.Dir = (byte)dir;
                switch (dir)
                {
                    case Grobal2.DR_UP:
                        if (CY > 1)
                        {
                            if (PEnvir.CanWalk(CX, CY - 1, allowdup) && PEnvir.CanWalk(CX, CY - 2, allowdup))
                            {
                                if (PEnvir.MoveToMovingObject(CX, CY, this, CX, CY - 2, true) > 0)
                                {
                                    CY = (short)(CY - 2);
                                }
                            }
                        }
                        break;
                    case Grobal2.DR_DOWN:
                        if (CY < PEnvir.MapHeight - 2)
                        {
                            if (PEnvir.CanWalk(CX, CY + 1, allowdup) && PEnvir.CanWalk(CX, CY + 2, allowdup))
                            {
                                if (PEnvir.MoveToMovingObject(CX, CY, this, CX, CY + 2, true) > 0)
                                {
                                    CY = (short)(CY + 2);
                                }
                            }
                        }
                        break;
                    case Grobal2.DR_LEFT:
                        if (CX > 1)
                        {
                            if (PEnvir.CanWalk(CX - 1, CY, allowdup) && PEnvir.CanWalk(CX - 2, CY, allowdup))
                            {
                                if (PEnvir.MoveToMovingObject(CX, CY, this, CX - 2, CY, true) > 0)
                                {
                                    CX = (short)(CX - 2);
                                }
                            }
                        }
                        break;
                    case Grobal2.DR_RIGHT:
                        if (CX < PEnvir.MapWidth - 2)
                        {
                            if (PEnvir.CanWalk(CX + 1, CY, allowdup) && PEnvir.CanWalk(CX + 2, CY, allowdup))
                            {
                                if (PEnvir.MoveToMovingObject(CX, CY, this, CX + 2, CY, true) > 0)
                                {
                                    CX = (short)(CX + 2);
                                }
                            }
                        }
                        break;
                    case Grobal2.DR_UPLEFT:
                        if ((CX > 1) && (CY > 1))
                        {
                            if (PEnvir.CanWalk(CX - 1, CY - 1, allowdup) && PEnvir.CanWalk(CX - 2, CY - 2, allowdup))
                            {
                                if (PEnvir.MoveToMovingObject(CX, CY, this, CX - 2, CY - 2, true) > 0)
                                {
                                    CX = (short)(CX - 2);
                                    CY = (short)(CY - 2);
                                }
                            }
                        }
                        break;
                    case Grobal2.DR_UPRIGHT:
                        if ((CX < PEnvir.MapWidth - 2) && (CY > 1))
                        {
                            if (PEnvir.CanWalk(CX + 1, CY - 1, allowdup) && PEnvir.CanWalk(CX + 2, CY - 2, allowdup))
                            {
                                if (PEnvir.MoveToMovingObject(CX, CY, this, CX + 2, CY - 2, true) > 0)
                                {
                                    CX = (short)(CX + 2);
                                    CY = (short)(CY - 2);
                                }
                            }
                        }
                        break;
                    case Grobal2.DR_DOWNLEFT:
                        if ((CX > 1) && (CY < PEnvir.MapHeight - 2))
                        {
                            if (PEnvir.CanWalk(CX - 1, CY + 1, allowdup) && PEnvir.CanWalk(CX - 2, CY + 2, allowdup))
                            {
                                if (PEnvir.MoveToMovingObject(CX, CY, this, CX - 2, CY + 2, true) > 0)
                                {
                                    CX = (short)(CX - 2);
                                    CY = (short)(CY + 2);
                                }
                            }
                        }
                        break;
                    case Grobal2.DR_DOWNRIGHT:
                        if ((CX < PEnvir.MapWidth - 2) && (CY < PEnvir.MapHeight - 2))
                        {
                            if (PEnvir.CanWalk(CX + 1, CY + 1, allowdup) && PEnvir.CanWalk(CX + 2, CY + 2, allowdup))
                            {
                                if (PEnvir.MoveToMovingObject(CX, CY, this, CX + 2, CY + 2, true) > 0)
                                {
                                    CX = (short)(CX + 2);
                                    CY = (short)(CY + 2);
                                }
                            }
                        }
                        break;
                }
                if ((prx != CX) || (pry != CY))
                {
                    if (Walk(Grobal2.RM_RUN))
                    {
                        result = true;
                    }
                    else
                    {
                        CX = prx;
                        CY = pry;
                        if (PEnvir.MoveToMovingObject(prx, pry, this, CX, CY, true) <= 0)
                        {
                            M2Share.MainOutMessage("ERROR DO NOT MOVINGOBJECT BACK :" + PEnvir.MapName + ":" + CX.ToString() + ":" + CY.ToString());
                        }
                    }
                }
            }
            catch
            {
                M2Share.MainOutMessage("[Exception] TCreature.RunTo");
            }
            return result;
        }

        public bool IsEnoughBag()
        {
            bool result;
            if (ItemList.Count < Grobal2.MAXBAGITEM)
            {
                result = true;
            }
            else
            {
                result = false;
            }
            return result;
        }

        public void WeightChanged()
        {
            // CalcWearWeightEx (-1) +
            WAbil.Weight = (short)CalcBagWeight();
            UpdateMsg(this, Grobal2.RM_WEIGHTCHANGED, 0, 0, 0, 0, "");
        }

        public void GoldChanged()
        {
            if (RaceServer == Grobal2.RC_USERHUMAN)
            {
                UpdateMsg(this, Grobal2.RM_GOLDCHANGED, 0, 0, 0, 0, "");
            }
        }

        public void GameGoldChanged()
        {
            if (RaceServer == Grobal2.RC_USERHUMAN)
            {
                UpdateMsg(this, Grobal2.RM_GAMEGOLDCHANGED, 0, 0, 0, 0, "");
            }
        }

        public void HealthSpellChanged()
        {
            if (RaceServer == Grobal2.RC_USERHUMAN)
            {
                UpdateMsg(this, Grobal2.RM_HEALTHSPELLCHANGED, 0, 0, 0, 0, "");
            }
            if (BoOpenHealth)
            {
                SendRefMsg(Grobal2.RM_HEALTHSPELLCHANGED, 0, 0, 0, 0, "");
            }
        }

        public bool IsAddWeightAvailable(int addweight)
        {
            bool result;
            if (WAbil.Weight + addweight <= WAbil.MaxWeight)
            {
                result = true;
            }
            else
            {
                result = false;
            }
            return result;
        }

        public TUserItem FindItemName(string iname)
        {
            TUserItem result;
            int i;
            result = null;
            for (i = 0; i < ItemList.Count; i++)
            {
                if (M2Share.UserEngine.GetStdItemName(ItemList[i].Index).ToLower().CompareTo(iname.ToLower()) == 0)
                {
                    result = ItemList[i];
                    break;
                }
            }
            return result;
        }

        public TUserItem FindItemNameEx(string iname, ref int count, ref int durasum, ref int duratop)
        {
            TUserItem result;
            int i;
            TStdItem ps;
            TUserItem pu;
            result = null;
            durasum = 0;
            duratop = 0;
            count = 0;
            ps = null;
            pu = null;
            for (i = 0; i < ItemList.Count; i++)
            {
                pu = ItemList[i];
                if (pu != null)
                {
                    ps = M2Share.UserEngine.GetStdItem(pu.Index);
                    if (ps != null)
                    {
                        if (ps.Name.ToLower().CompareTo(iname.ToLower()) == 0)
                        {
                            if (ps.OverlapItem >= 1)
                            {
                                count = pu.Dura;
                                result = pu;
                            }
                            else
                            {
                                // -----------------------------------------------------------
                                // 何利捞搁 俺荐甫 眉农秦辑 葛磊福搁 促澜 酒捞袍栏肺 逞绢皑.
                                if (ps.Name == M2Share.GetUnbindItemName(ObjBase.SHAPE_AMULET_BUNCH))
                                {
                                    if (pu.Dura < pu.DuraMax)
                                    {
                                        continue;
                                    }
                                }
                                // -----------------------------------------------------------
                                if (pu.Dura > duratop)
                                {
                                    duratop = pu.Dura;
                                    result = pu;
                                }
                                durasum = durasum + pu.Dura;
                                if (result == null)
                                {
                                    result = pu;
                                }
                                count++;
                            }
                        }
                    }
                }
            }
            return result;
        }

        public bool FindItemEventGrade(int grade, int count)
        {
            bool result;
            int i;
            TStdItem ps;
            TUserItem pu;
            int existcount;
            result = false;
            ps = null;
            pu = null;
            existcount = 0;
            for (i = 0; i < ItemList.Count; i++)
            {
                pu = ItemList[i];
                if (pu != null)
                {
                    ps = M2Share.UserEngine.GetStdItem(pu.Index);
                    if (ps != null)
                    {
                        if (ps.EffType2 == Grobal2.EFFTYPE2_EVENT_GRADE)
                        {
                            if (ps.EffValue2 == grade)
                            {
                                existcount++;
                            }
                        }
                    }
                }
            }
            if (existcount >= count)
            {
                result = true;
            }
            return result;
        }

        public TUserItem FindItemWear(string iname, ref int count)
        {
            TUserItem result;
            int i;
            result = null;
            count = 0;
            for (i = 0; i <= 8; i++)
            {
                if (M2Share.UserEngine.GetStdItemName(UseItems[i].Index).ToLower().CompareTo(iname.ToLower()) == 0)
                {
                    result = UseItems[i];
                    count++;
                }
            }
            return result;
        }

        public bool CanAddItem()
        {
            bool result;
            result = false;
            if (ItemList.Count < Grobal2.MAXBAGITEM)
            {
                result = true;
            }
            return result;
        }

        // pu绰 货肺 new秦辑 疵巴(蝶肺 new 窍瘤 臼澜)
        public bool AddItem(TUserItem pu)
        {
            bool result;
            result = false;
            if (ItemList.Count < Grobal2.MAXBAGITEM)
            {
                ItemList.Add(pu);
                WeightChanged();
                result = true;
            }
            return result;
        }

        // 角菩沁阑 版快档 蜡狼秦具 窃.
        public bool DelItem(int svindex, string iname)
        {
            bool result;
            int i;
            result = false;
            for (i = 0; i < ItemList.Count; i++)
            {
                if (ItemList[i].MakeIndex == svindex)
                {
                    if (M2Share.UserEngine.GetStdItemName(ItemList[i].Index).ToLower().CompareTo(iname.ToLower()) == 0)
                    {
                        Dispose(ItemList[i]);
                        ItemList.RemoveAt(i);
                        result = true;
                        break;
                    }
                }
            }
            if (result)
            {
                WeightChanged();
            }
            return result;
        }

        public bool DelItemIndex(int bagindex)
        {
            bool result;
            result = false;
            if ((bagindex >= 0) && (bagindex < ItemList.Count))
            {
                Dispose(ItemList[bagindex]);
                ItemList.RemoveAt(bagindex);
            }
            return result;
        }

        public bool DeletePItemAndSend(TUserItem pcheckitem)
        {
            bool result;
            int i;
            TUserHuman hum;
            result = false;
            for (i = 0; i < ItemList.Count; i++)
            {
                if (ItemList[i] == pcheckitem)
                {
                    if (RaceServer == Grobal2.RC_USERHUMAN)
                    {
                        hum = this as TUserHuman;
                        hum.SendDelItem(ItemList[i]);
                    }
                    Dispose(ItemList[i]);
                    ItemList.RemoveAt(i);
                    result = true;
                    return result;
                }
            }
            for (i = 0; i <= 8; i++)
            {
                if (UseItems[i] == pcheckitem)
                {
                    if (RaceServer == Grobal2.RC_USERHUMAN)
                    {
                        hum = this as TUserHuman;
                        hum.SendDelItem(UseItems[i]);
                    }
                    UseItems[i].Index = 0;
                    result = true;
                }
            }
            return result;
        }

        public bool DeletePItemAndSendWithFlag(TUserItem pcheckitem, short wBreakdown)
        {
            bool result;
            int i;
            TUserHuman hum;
            result = false;
            for (i = 0; i < ItemList.Count; i++)
            {
                if (ItemList[i] == pcheckitem)
                {
                    if (RaceServer == Grobal2.RC_USERHUMAN)
                    {
                        hum = this as TUserHuman;
                        hum.SendDelItemWithFlag(ItemList[i], wBreakdown);
                    }
                    Dispose(ItemList[i]);
                    ItemList.RemoveAt(i);
                    result = true;
                    return result;
                }
            }
            for (i = 0; i <= 8; i++)
            {
                if (UseItems[i] == pcheckitem)
                {
                    if (RaceServer == Grobal2.RC_USERHUMAN)
                    {
                        hum = this as TUserHuman;
                        hum.SendDelItemWithFlag(UseItems[i], wBreakdown);
                    }
                    UseItems[i].Index = 0;
                    result = true;
                }
            }
            return result;
        }

        public bool CanTakeOn(int index, TStdItem ps)
        {
            bool result;
            // 己喊苞 饭骇, 流诀俊 嘎绰瘤 八荤
            result = false;
            if (ps.StdMode == 10)
            {
                // 巢磊 渴
                if (Sex != 0)
                {
                    // 巢磊啊 酒聪搁
                    SysMsg("男性服饰。", 0);
                    return result;
                }
            }
            if (ps.StdMode == 11)
            {
                // 咯磊渴
                if (Sex != 1)
                {
                    SysMsg("女性服饰。", 0);
                    return result;
                }
            }
            // 公霸 八荤  index:馒侩且镑
            if ((index == Grobal2.U_WEAPON) || (index == Grobal2.U_RIGHTHAND))
            {
                if (ps.Weight > WAbil.MaxHandWeight)
                {
                    // 甸 荐 乐绰 公扁 公霸 檬苞
                    SysMsg("过重。", 0);
                    return result;
                }
            }
            else
            {
                if (ps.Weight + CalcWearWeightEx(index) > WAbil.MaxWearWeight)
                {
                    // 涝绊 乐绰 酒捞袍狼 公霸 檬苞
                    SysMsg("过重。", 0);
                    return result;
                }
            }
            switch (ps.Need)
            {
                case 0:
                    // 饭骇 八荤
                    if (Abil.Level >= ps.NeedLevel)
                    {
                        result = true;
                    }
                    break;
                case 1:
                    // DC
                    if (HiByte(WAbil.DC) >= ps.NeedLevel)
                    {
                        result = true;
                    }
                    break;
                case 2:
                    // MC
                    if (HiByte(WAbil.MC) >= ps.NeedLevel)
                    {
                        result = true;
                    }
                    break;
                case 3:
                    // SC
                    if (HiByte(WAbil.SC) >= ps.NeedLevel)
                    {
                        result = true;
                    }
                    break;
            }
            if (!result)
            {
                SysMsg("不适合您使用。", 0);
            }
            return result;
        }

        public bool GetDropPosition(int x, int y, int wide, ref int dx, ref int dy)
        {
            bool result;
            int i;
            int j;
            int k;
            int dropcount = 0;
            int icount;
            int ssx;
            int ssy;
            icount = 999;
            result = false;
            ssx = dx;
            ssy = dy;
            for (k = 1; k <= wide; k++)
            {
                for (j = -k; j <= k; j++)
                {
                    for (i = -k; i <= k; i++)
                    {
                        dx = x + i;
                        dy = y + j;
                        if (PEnvir.GetItemEx(dx, dy, ref dropcount) == null)
                        {
                            if (PEnvir.BoCanGetItem)
                            {
                                result = true;
                                break;
                            }
                        }
                        else
                        {
                            if (PEnvir.BoCanGetItem)
                            {
                                if (icount > dropcount)
                                {
                                    icount = dropcount;
                                    ssx = dx;
                                    ssy = dy;
                                }
                            }
                        }
                    }
                    if (result)
                    {
                        break;
                    }
                }
                if (result)
                {
                    break;
                }
            }
            if (!result)
            {
                // 酒聪搁 磊扁 磊府...
                if (icount < 8)
                {
                    dx = ssx;
                    dy = ssy;
                }
                else
                {
                    dx = x;
                    // - wide + Random(wide*2+1);
                    dy = y;
                    // - wide + Random(wide*2+1);
                }
            }
            return result;
        }

        public bool GetRecallPosition(short x, short y, int wide, ref short dx, ref short dy)
        {
            int i;
            int j;
            int k;
            bool result = false;
            if (PEnvir.GetCreature(x, y, true) == null)
            {
                result = true;
                dx = x;
                dy = y;
            }
            if (!result)
            {
                for (k = 1; k <= wide; k++)
                {
                    for (j = -k; j <= k; j++)
                    {
                        for (i = -k; i <= k; i++)
                        {
                            dx = (short)(x + i);
                            dy = (short)(y + j);
                            if (PEnvir.GetCreature(dx, dy, true) == null)
                            {
                                result = true;
                                break;
                            }
                        }
                        if (result)
                        {
                            break;
                        }
                    }
                    if (result)
                    {
                        break;
                    }
                }
            }
            if (!result)
            {
                dx = x;
                dy = y;
            }
            return result;
        }

        public bool DropItemDown(TUserItem ui, int scatterrange, bool diedrop, object ownership, object droper, int IsDropFromBag)
        {
            int dx = 0;
            int dy = 0;
            int idura;
            int temp;
            TMapItem pmi;
            TMapItem pr;
            string logcap;
            TAgitDecoItem decoitem = null;
            string pricestr = string.Empty;
            string ShowName = string.Empty;
            bool result = false;
            string countstr = "";
            TStdItem ps = M2Share.UserEngine.GetStdItem(ui.Index);
            if (ps != null)
            {
                if (ps.StdMode == 40)
                {
                    idura = ui.Dura;
                    idura = idura - 2000;
                    if (idura < 0)
                    {
                        idura = 0;
                    }
                    ui.Dura = (short)idura;
                }
                pmi = new TMapItem();
                pmi.UserItem = ui;
                if (ps.OverlapItem >= 1)
                {
                    temp = ui.Dura;
                    if (temp > 1)
                    {
                        countstr = "(" + temp.ToString() + ")";
                        pmi.Name = ps.Name + countstr;
                    }
                    else
                    {
                        pmi.Name = ps.Name;
                    }
                }
                else
                {
                    pmi.Name = ps.Name;
                }
                pmi.Looks = ps.Looks;
                if (ps.StdMode == 45)
                {
                    pmi.Looks = HUtil32.GetRandomLook(pmi.Looks, ps.Shape);
                }
                pmi.AniCount = ps.AniCount;
                pmi.Reserved = 0;
                pmi.Count = 1;
                pmi.Ownership = ownership;
                pmi.Droptime = HUtil32.GetTickCount();
                pmi.Droper = droper;
                GetDropPosition(CX, CY, scatterrange, ref dx, ref dy);
                if ((ps.StdMode == ObjBase.STDMODE_OF_DECOITEM) && (ps.Shape == ObjBase.SHAPE_OF_DECOITEM))
                {
                    if (ui.Dura <= M2Share.DecoItemList.Count)
                    {
                        dx = CX;
                        dy = CY;
                        pmi.Name = M2Share.GuildAgitMan.GetDecoItemName(ui.Dura, ref pricestr) + "[" + HUtil32.MathRound(ui.DuraMax / 1000).ToString() + "]" + "/" + "1";
                        pmi.Looks = ui.Dura;
                        pmi.AniCount = ps.AniCount;
                        pmi.Reserved = 0;
                        pmi.Count = 1;
                        pmi.Ownership = droper;
                        pmi.Droptime = HUtil32.GetTickCount();
                        pmi.Droper = droper;
                        decoitem = new TAgitDecoItem();
                        decoitem.Name = M2Share.GuildAgitMan.GetDecoItemName(ui.Dura, ref pricestr);
                        decoitem.Looks = ui.Dura;
                        decoitem.MapName = PEnvir.MapName;
                        decoitem.x = (short)dx;
                        decoitem.y = (short)dy;
                        decoitem.Maker = UserName;
                        decoitem.Dura = ui.DuraMax;
                    }
                    else
                    {
                        M2Share.MainOutMessage("[DropItemDown] DecoItemList Error...");
                    }
                }
                else
                {
                    pmi.Name = pmi.Name + "/" + "0";
                }
                pr = (TMapItem)PEnvir.AddToMap(dx, dy, Grobal2.OS_ITEMOBJECT, pmi);
                if (pr == pmi)
                {
                    if ((ps.StdMode == ObjBase.STDMODE_OF_DECOITEM) && (ps.Shape == ObjBase.SHAPE_OF_DECOITEM))
                    {
                        if (M2Share.GuildAgitMan.AddAgitDecoMon(decoitem))
                        {
                            M2Share.GuildAgitMan.IncAgitDecoMonCount(GetGuildNameHereAgit());
                            M2Share.GuildAgitMan.SaveAgitDecoMonList();
                        }
                        else
                        {
                            M2Share.MainOutMessage("[ErrorMsg]TCreature.DropItemDown : AddAgitDecoMon Failure!!!");
                        }
                    }
                    SendRefMsg(Grobal2.RM_ITEMSHOW, pmi.Looks, pmi.ItemId, dx, dy, pmi.Name);
                    if (diedrop)
                    {
                        logcap = "15\09";
                    }
                    else
                    {
                        logcap = "7\09";
                        (this as TUserHuman).LatestDropTime = HUtil32.GetTickCount();
                    }
                    HUtil32.GetValidStrNoVal(UserName, ref ShowName);
                    if ((RaceServer != Grobal2.RC_USERHUMAN) && (M2Share.DropItemNoticeList.IndexOf(M2Share.UserEngine.GetStdItemName(ui.Index)) >= 0))
                    {
                        M2Share.UserEngine.SysMsgAll(PEnvir.MapTitle + "的 " + ShowName + " 死亡掉落: " + M2Share.UserEngine.GetStdItemName(ui.Index));// 物品掉落提醒
                    }
                    if (!M2Share.IsCheapStuff(ps.StdMode))
                    {
                        M2Share.AddUserLog(logcap + MapName + "\09" + CX.ToString() + "\09" + CY.ToString() + "\09" + UserName + "\09" + M2Share.UserEngine.GetStdItemName(ui.Index) + "\09" + ui.MakeIndex.ToString() + "\09" + HUtil32.BoolToIntStr(RaceServer == Grobal2.RC_USERHUMAN).ToString() + "\09" + IsDropFromBag.ToString() + countstr);
                    }
                    result = true;
                }
                else
                {
                    Dispose(pmi);
                }
            }
            return result;
        }

        public bool DropGoldDown(int goldcount, bool diedrop, object ownership, object droper)
        {
            int dx = 0;
            int dy = 0;
            TMapItem pmi;
            TMapItem pr;
            string logcap;
            bool result = false;
            pmi = new TMapItem();
            pmi.Name = Envir.NAME_OF_GOLD;
            pmi.Count = goldcount;
            pmi.Looks = HUtil32.GetGoldLooks(goldcount);
            pmi.Ownership = ownership;
            pmi.Droptime = HUtil32.GetTickCount();
            pmi.Droper = droper;
            GetDropPosition(CX, CY, 3, ref dx, ref dy);
            pr = (TMapItem)PEnvir.AddToMap(dx, dy, Grobal2.OS_ITEMOBJECT, pmi);
            if (pr != null)
            {
                if (pr != pmi)
                {
                    Dispose(pmi);
                    pmi = pr;
                }
                SendRefMsg(Grobal2.RM_ITEMSHOW, pmi.Looks, pmi.ItemId, dx, dy, Envir.NAME_OF_GOLD + "/" + "0");
                if (RaceServer == Grobal2.RC_USERHUMAN)
                {
                    if (diedrop)
                    {
                        logcap = "15\09";
                    }
                    else
                    {
                        logcap = "7\09";
                        (this as TUserHuman).LatestDropTime = HUtil32.GetTickCount();
                    }
                    M2Share.AddUserLog(logcap + MapName + "\09" + CX.ToString() + "\09" + CY.ToString() + "\09" + UserName + "\09" + Envir.NAME_OF_GOLD + "\09" + goldcount.ToString() + "\09" + HUtil32.BoolToIntStr(RaceServer == Grobal2.RC_USERHUMAN).ToString() + "\09" + "0");
                }
                result = true;
            }
            else
            {
                Dispose(pmi);
            }
            return result;
        }

        public bool UserDropItem(string itmname, int itemindex)
        {
            TUserItem pu;
            TStdItem pstd;
            TUserHuman hum;
            string gname;
            bool result = false;
            if (PEnvir.NoThrowItem)
            {
                return result;
            }
            if (itmname.IndexOf(" ") >= 0)
            {
                HUtil32.GetValidStr3(itmname, ref itmname, new string[] { " " });
            }
            if (HUtil32.GetTickCount() - DealItemChangeTime > 3000)
            {
                for (var i = 0; i < ItemList.Count; i++)
                {
                    pu = ItemList[i];
                    pstd = M2Share.UserEngine.GetStdItem(pu.Index);
                    if (pstd == null)
                    {
                        continue;
                    }
                    if (pstd.StdMode != ObjBase.TAIWANEVENTITEM)
                    {
                        if (pu.MakeIndex == itemindex)
                        {
                            if (M2Share.UserEngine.GetStdItemName(pu.Index).ToLower().CompareTo(itmname.ToLower()) == 0)
                            {
                                if (RaceServer == Grobal2.RC_USERHUMAN)
                                {
                                    if ((this as TUserHuman).IsOnSaleItem(pu.MakeIndex))
                                    {
                                        return result;
                                    }
                                }
                                if ((pstd.UniqueItem & 0x04) != 0)
                                {
                                    Dispose(ItemList[i]);
                                    ItemList.RemoveAt(i);
                                    result = true;
                                    break;
                                }
                                if ((pstd.StdMode == ObjBase.STDMODE_OF_DECOITEM) && (pstd.Shape == ObjBase.SHAPE_OF_DECOITEM))
                                {
                                    if (RaceServer == Grobal2.RC_USERHUMAN)
                                    {
                                        hum = this as TUserHuman;
                                        gname = hum.GetGuildNameHereAgit();
                                        if (gname != "")
                                        {
                                            if (MyGuild != null)
                                            {
                                                if (MyGuild.GuildName != gname)
                                                {
                                                    hum.SysMsg("你只能在你的门派庄园上使用。", 0);
                                                    break;
                                                }
                                                if (!M2Share.GuildAgitMan.IsAvailableDecoMonCount(gname))
                                                {
                                                    hum.SysMsg("你不能安置更多的装饰物。", 0);
                                                    break;
                                                }
                                                if (!M2Share.GuildAgitMan.IsMatchDecoItemInOutdoor(pu.Dura, MapName))
                                                {
                                                    hum.SysMsg("你不能安置在这里。", 0);
                                                    break;
                                                }
                                                // GuildAgitMan.IncAgitDecoMonCount( GetGuildNameHereAgit );
                                            }
                                        }
                                        else
                                        {
                                            hum.SysMsg("你只能在你的门派庄园上使用。", 0);
                                            break;
                                        }
                                    }
                                }
                                if (DropItemDown(pu, 1, false, null, this, 0))
                                {
                                    Dispose(ItemList[i]);
                                    ItemList.RemoveAt(i);
                                    result = true;
                                    break;
                                }
                            }
                        }
                    }
                }
                if (result)
                {
                    WeightChanged();
                }
            }
            return result;
        }

        public bool UserDropGold(int dropgold)
        {
            bool result;
            result = false;
            // 酒捞袍阑 滚副 荐 绝澜(sonmg 2005/03/14)
            if (PEnvir.NoThrowItem)
            {
                return result;
            }
            if ((dropgold > 0) && (dropgold <= Gold))
            {
                // Gold := Gold - dropgold;
                DecGold(dropgold);
                if (!DropGoldDown(dropgold, false, null, this))
                {
                    // Gold := Gold + dropgold;
                    IncGold(dropgold);
                }
                GoldChanged();
                result = true;
            }
            return result;
        }

        // 墨款飘 酒捞袍
        // 墨款飘 酒捞袍
        public bool UserDropCountItem(string itmname, int dropidx, int dropcnt)
        {
            int i;
            int remain;
            int t;
            TUserItem pu;
            TUserItem newpu;
            TStdItem ps;
            bool result = false;
            if (PEnvir.NoThrowItem)
            {
                return result;
            }
            if (itmname.IndexOf(" ") >= 0)
            {
                HUtil32.GetValidStr3(itmname, ref itmname, new string[] { " " });
            }
            if (HUtil32.GetTickCount() - DealItemChangeTime > 3000)
            {
                for (i = 0; i < ItemList.Count; i++)
                {
                    pu = ItemList[i];
                    if (pu.MakeIndex == dropidx)
                    {
                        ps = M2Share.UserEngine.GetStdItem(pu.Index);
                        if (ps != null)
                        {
                            if (ps.OverlapItem == 0)
                            {
                                continue;
                            }
                            // if (ps.UniqueItem and $04) <> 0 then continue; //UNIQUEITEM 鞘靛啊 00000100(2柳荐)甫 器窃窍搁 冻奔 荐 绝绰 酒捞袍(sonmg 2005/03/14)
                            if (ps.Name.ToLower().CompareTo(itmname.ToLower()) == 0)
                            {
                                t = pu.Dura;
                                if (dropcnt > t)
                                {
                                    dropcnt = pu.Dura;
                                }
                                remain = t - dropcnt;
                                if (dropcnt > 0)
                                {
                                    if (remain > 0)
                                    {
                                        newpu = new TUserItem();
                                        if (M2Share.UserEngine.CopyToUserItemFromName(itmname, ref newpu))
                                        {
                                            newpu.Dura = (short)dropcnt;
                                            // UNIQUEITEM 鞘靛啊 00000100(2柳荐)甫 器窃窍搁 滚府搁 荤扼瘤绰 酒捞袍(sonmg 2005/03/14)
                                            if ((ps.UniqueItem & 0x04) != 0)
                                            {
                                                pu.Dura = (short)remain;
                                                // 荐樊
                                                SendMsg(this, Grobal2.RM_COUNTERITEMCHANGE, 0, pu.MakeIndex, remain, 0, itmname);
                                                WeightChanged();
                                                Dispose(newpu);
                                                break;
                                            }
                                            if (DropItemDown(newpu, 1, false, null, this, 0))
                                            {
                                                pu.Dura = (short)remain;
                                                // 荐樊
                                                SendMsg(this, Grobal2.RM_COUNTERITEMCHANGE, 0, pu.MakeIndex, remain, 0, itmname);
                                                WeightChanged();
                                                Dispose(newpu);
                                                break;
                                            }
                                            else
                                            {
                                                Dispose(newpu);
                                                break;
                                            }
                                        }
                                        else
                                        {
                                            Dispose(newpu);
                                            break;
                                        }
                                    }
                                    else
                                    {
                                        // UNIQUEITEM 鞘靛啊 00000100(2柳荐)甫 器窃窍搁 滚府搁 荤扼瘤绰 酒捞袍(sonmg 2005/03/14)
                                        if ((ps.UniqueItem & 0x04) != 0)
                                        {
                                            Dispose(ItemList[i]);
                                            ItemList.RemoveAt(i);
                                            WeightChanged();
                                            result = true;
                                            break;
                                        }
                                        if (DropItemDown(pu, 1, false, null, this, 0))
                                        {
                                            Dispose(ItemList[i]);
                                            ItemList.RemoveAt(i);
                                            WeightChanged();
                                            result = true;
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return result;
        }

        public bool UserCounterItemAdd(int StdMode, int Looks, int Cnt, string iName, bool bEnforce, int ExceptMakeIndex = 0)
        {
            TUserItem pu;
            TStdItem ps;
            int idxMinimum;
            short countMinimum;
            bool result = false;
            idxMinimum = -1;
            countMinimum = 0;
            for (var i = 0; i < ItemList.Count; i++)
            {
                ps = M2Share.UserEngine.GetStdItem(ItemList[i].Index);
                if (ps == null)
                {
                    continue;
                }
                if (ps.OverlapItem == 0)
                {
                    continue;
                }
                if ((ps.StdMode == StdMode) && (ps.Looks == Looks) && (ps.OverlapItem >= 1))
                {
                    if (ps.Name.ToLower().CompareTo(iName.ToLower()) == 0)
                    {
                        pu = ItemList[i];
                        if ((ExceptMakeIndex != -1) && (pu.MakeIndex == ExceptMakeIndex))
                        {
                            continue;
                        }
                        if (idxMinimum == -1)
                        {
                            countMinimum = pu.Dura;
                            idxMinimum = i;
                        }
                        else
                        {
                            if (countMinimum > pu.Dura)
                            {
                                countMinimum = pu.Dura;
                                idxMinimum = i;
                            }
                        }
                    }
                }
            }
            if (idxMinimum < 0)
            {
                return result;
            }
            ps = M2Share.UserEngine.GetStdItem(ItemList[idxMinimum].Index);
            if (ps == null)
            {
                return result;
            }
            pu = ItemList[idxMinimum];
            if ((bEnforce == false) && (pu.Dura + Cnt > Grobal2.MAX_OVERLAPITEM))
            {
                return result;
            }
            if (pu.Dura + Cnt > ObjBase.MAX_OVERFLOW)
            {
                return result;
            }
            pu.Dura = (short)_MIN(ObjBase.MAX_OVERFLOW, pu.Dura + Cnt);
            if (RaceServer == Grobal2.RC_USERHUMAN)
            {
                SendMsg(this, Grobal2.RM_COUNTERITEMCHANGE, 0, pu.MakeIndex, pu.Dura, 1, ps.Name);
            }
            result = true;
            return result;
        }

        // 芭贰 格废 -> 墨款飘 酒捞袍 俺荐 眠啊
        public int UserCounterDealItemAdd(int StdMode, int Looks, int Cnt, string iName)
        {
            const int FAIL = 0;
            const int SUCCESS = 1;
            const int OVERFLOW = 2;
            const int OVERCOUNT = 3;
            TUserItem pu;
            TStdItem ps;
            int result = FAIL;
            for (var i = 0; i < DealList.Count; i++)
            {
                ps = M2Share.UserEngine.GetStdItem(DealList[i].Index);
                if (ps == null)
                {
                    continue;
                }
                if (ps.OverlapItem == 0)
                {
                    continue;
                }
                if ((ps.StdMode == StdMode) && (ps.Looks == Looks) && (ps.OverlapItem >= 1))
                {
                    if (ps.Name.ToLower().CompareTo(iName.ToLower()) == 0)
                    {
                        pu = DealList[i];
                        if (pu.Dura + Cnt > Grobal2.MAX_OVERLAPITEM)
                        {
                            result = OVERCOUNT;
                            return result;
                        }
                        if (pu.Dura + Cnt > ObjBase.MAX_OVERFLOW)
                        {
                            result = OVERFLOW;
                            return result;
                        }
                        pu.Dura = (short)_MIN(ObjBase.MAX_OVERFLOW, pu.Dura + Cnt);
                        if (RaceServer == Grobal2.RC_USERHUMAN)
                        {
                            SendMsg(this, Grobal2.RM_COUNTERITEMCHANGE, 0, pu.MakeIndex, pu.Dura, 0, ps.Name);
                        }
                        result = SUCCESS;
                        break;
                    }
                }
            }
            return result;
        }

        public bool PickUp_canpickup(object ownership)
        {
            bool result;
            if ((ownership == null) || (ownership == this))
            {
                result = true;
            }
            else
            {
                result = false;
            }
            return result;
        }

        public bool PickUp_cangrouppickup(object ownership)
        {
            TCreature cret;
            bool result = false;
            if (GroupOwner != null)
            {
                for (var i = 0; i < GroupOwner.GroupMembers.Count; i++)
                {
                    cret = GroupOwner.GroupMembers[i];
                    if (cret == ownership)
                    {
                        result = true;
                        break;
                    }
                }
            }
            return result;
        }

        public bool PickUp()
        {
            bool result;
            int wg;
            TUserItem pu;
            TMapItem pmi;
            TStdItem ps;
            TUserHuman hum;
            TMerchant questnpc;
            string dropername;
            bool flag;
            string countstr;
            result = false;
            wg = 0;
            ps = null;
            countstr = "";
            if (BoDealing || (this as TUserHuman).StallMgr.OnSale)
            {
                return result;
            }
            hum = null;
            pmi = PEnvir.GetItem(CX, CY);
            if (pmi != null)
            {
                if (HUtil32.GetTickCount() - pmi.Droptime > ObjBase.ANTI_MUKJA_DELAY)
                {
                    pmi.Ownership = null;
                }
                if (PickUp_canpickup(pmi.Ownership) || PickUp_cangrouppickup(pmi.Ownership))
                {
                    if (pmi.Name.ToLower().CompareTo(Envir.NAME_OF_GOLD.ToLower()) == 0)
                    {
                        if (PEnvir.DeleteFromMap(CX, CY, Grobal2.OS_ITEMOBJECT, pmi) == 1)
                        {
                            if (IncGold(pmi.Count))
                            {
                                SendRefMsg(Grobal2.RM_ITEMHIDE, 0, pmi.ItemId, CX, CY, "");
                                M2Share.AddUserLog("4\09" + MapName + "\09" + CX.ToString() + "\09" + CY.ToString() + "\09" + UserName + "\09" + Envir.NAME_OF_GOLD + "\09" + pmi.Count.ToString() + "\09" + "1\09" + "0");
                                GoldChanged();
                                Dispose(pmi);
                            }
                            else
                            {
                                PEnvir.AddToMap(CX, CY, Grobal2.OS_ITEMOBJECT, pmi);
                            }
                        }
                    }
                    else
                    {
                        ps = M2Share.UserEngine.GetStdItem(pmi.UserItem.Index);
                    }
                    if (ps != null)
                    {
                        if (ps.OverlapItem >= 1)
                        {
                            countstr = "(" + pmi.UserItem.Dura.ToString() + ")";
                            if (PEnvir.DeleteFromMap(CX, CY, Grobal2.OS_ITEMOBJECT, pmi) == 1)
                            {
                                if (UserCounterItemAdd(ps.StdMode, ps.Looks, pmi.UserItem.Dura, ps.Name, false))
                                {
                                    SendMsg(this, Grobal2.RM_ITEMHIDE, 0, pmi.ItemId, CX, CY, "");
                                    WeightChanged();
                                    Dispose(pmi);
                                    return result;
                                }
                                else
                                {
                                    PEnvir.AddToMap(CX, CY, Grobal2.OS_ITEMOBJECT, pmi);
                                }
                            }
                        }
                        if (IsEnoughBag())
                        {
                            flag = true;
                            if ((ps.StdMode == ObjBase.STDMODE_OF_DECOITEM) && (ps.Shape == ObjBase.SHAPE_OF_DECOITEM))
                            {
                                if (((pmi.Ownership == null) && IsMyGuildMaster()) || ((pmi.Ownership != null) && (pmi.Ownership == this)))
                                {
                                    if (M2Share.GuildAgitMan.DeleteAgitDecoMon(PEnvir.MapName, CX, CY))
                                    {
                                        M2Share.GuildAgitMan.SaveAgitDecoMonList();
                                        flag = true;
                                    }
                                    else
                                    {
                                        flag = false;
                                    }
                                }
                                else
                                {
                                    flag = false;
                                }
                            }
                            if (flag)
                            {
                                if (PEnvir.DeleteFromMap(CX, CY, Grobal2.OS_ITEMOBJECT, pmi) == 1)
                                {
                                    pu = new TUserItem();
                                    pu = pmi.UserItem;
                                    ps = M2Share.UserEngine.GetStdItem(pu.Index);
                                    if (ps != null)
                                    {
                                        if (ps.OverlapItem == 1)
                                        {
                                            wg = pmi.UserItem.Dura / 10;
                                        }
                                        else if (ps.OverlapItem >= 2)
                                        {
                                            wg = ps.Weight * pmi.UserItem.Dura;
                                        }
                                        else
                                        {
                                            wg = ps.Weight;
                                        }
                                    }
                                    if (ps != null)
                                    {
                                        SendMsg(this, Grobal2.RM_ITEMHIDE, 0, pmi.ItemId, CX, CY, "");
                                        AddItem(pu);
                                        if (PEnvir.HasMapQuest())
                                        {
                                            dropername = "";
                                            if (pmi.Droper != null)
                                            {
                                                dropername = (pmi.Droper as TCreature).UserName;
                                            }
                                            questnpc = (TMerchant)PEnvir.GetMapQuest(this, dropername, ps.Name, false);
                                            if (questnpc != null)
                                            {
                                                questnpc.UserCall(this);
                                            }
                                        }
                                        if (!M2Share.IsCheapStuff(ps.StdMode))
                                        {
                                            M2Share.AddUserLog("4\09" + MapName + "\09" + CX.ToString() + "\09" + CY.ToString() + "\09" + UserName + "\09" + M2Share.UserEngine.GetStdItemName(pu.Index) + "\09" + pu.MakeIndex.ToString() + "\09" + "1\09" + "0" + countstr);
                                        }
                                        if (RaceServer == Grobal2.RC_USERHUMAN)
                                        {
                                            if (this is TUserHuman)
                                            {
                                                hum = this as TUserHuman;
                                                hum.SendAddItem(pu);
                                            }
                                        }
                                        if (ps.StdMode == ObjBase.TAIWANEVENTITEM)
                                        {
                                            if (hum != null)
                                            {
                                                hum.TaiwanEventItemName = ps.Name;
                                                hum.BoTaiwanEventUser = true;
                                                StatusArr[Grobal2.STATE_BLUECHAR] = 60000;
                                                CharStatus = GetCharStatus();
                                                CharStatusChanged();
                                                Light = GetMyLight();
                                                SendRefMsg(Grobal2.RM_CHANGELIGHT, 0, 0, 0, 0, "");
                                                UserNameChanged();
                                            }
                                        }
                                        Dispose(pmi);
                                        result = true;
                                    }
                                    else
                                    {
                                        Dispose(pu);
                                        PEnvir.AddToMap(CX, CY, Grobal2.OS_ITEMOBJECT, pmi);
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {
                    SysMsg("你在一定时间内无法捡拾。", 0);
                }
            }
            return result;
        }

        public bool EatItem(TStdItem std, TUserItem pu)
        {
            bool boneedrecalc;
            TUserHuman hum;
            TUserHuman humlover;
            bool flag;
            TStdItem pstd = null;
            bool result = false;
            if (PEnvir.NoDrug)
            {
                SysMsg("在这里您无法使用。", 0);
                return result;
            }
            switch (std.StdMode)
            {
                case 0:
                    // 矫距幅
                    switch (std.Shape)
                    {
                        case ObjBase.FASTFILL_ITEM:
                            IncHealthSpell(std.AC, std.MAC);
                            IncHealthSpell(WAbil.MaxHP * std.DC / 100, WAbil.MaxMP * std.MC / 100);
                            result = true;
                            break;
                        case ObjBase.FREE_UNKNOWN_ITEM:
                            BoNextTimeFreeCurseItem = true;
                            result = true;
                            break;
                        default:
                            if ((IncHealth + std.AC < 1000) && (std.AC > 0))
                            {
                                IncHealth = (byte)(IncHealth + std.AC);
                            }
                            if ((IncSpell + std.MAC < 1000) && (std.MAC > 0))
                            {
                                IncSpell = (byte)(IncSpell + std.MAC);
                            }
                            result = true;
                            break;
                    }
                    break;
                case 1:
                    result = true;
                    break;
                case 2:
                    switch (std.Shape)
                    {
                        case ObjBase.SHAPE_BUNCH_OF_FLOWERS:
                            SendRefMsg(Grobal2.RM_LOOPNORMALEFFECT, (short)this.ActorId, 10000, 0, Grobal2.NE_FLOWERSEFFECT, "");
                            break;
                    }
                    result = true;
                    break;
                case 3:
                    switch (std.Shape)
                    {
                        case ObjBase.INSTANTABILUP_DRUG:
                            boneedrecalc = false;
                            if (LoByte(std.DC) > 0)
                            {
                                ExtraAbil[Grobal2.EABIL_DCUP] = (byte)_MAX(ExtraAbil[Grobal2.EABIL_DCUP], LoByte(std.DC));
                                ExtraAbilFlag[Grobal2.EABIL_DCUP] = 0;
                                ExtraAbilTimes[Grobal2.EABIL_DCUP] = _MAX(ExtraAbilTimes[Grobal2.EABIL_DCUP], HUtil32.GetTickCount() + HiByte(std.DC) * 60 * 1000 + HiByte(std.MAC) * 1000);
                                SysMsg("攻击力瞬间提高" + (HiByte(std.DC) + HiByte(std.MAC) / 60).ToString() + "分" + (HiByte(std.MAC) % 60).ToString() + "秒。", 1);
                                boneedrecalc = true;
                            }
                            if (LoByte(std.MC) > 0)
                            {
                                ExtraAbil[Grobal2.EABIL_MCUP] = (byte)_MAX(ExtraAbil[Grobal2.EABIL_MCUP], LoByte(std.MC));
                                ExtraAbilFlag[Grobal2.EABIL_MCUP] = 0;
                                ExtraAbilTimes[Grobal2.EABIL_MCUP] = _MAX(ExtraAbilTimes[Grobal2.EABIL_MCUP], HUtil32.GetTickCount() + HiByte(std.DC) * 60 * 1000 + HiByte(std.MAC) * 1000);
                                SysMsg("魔法力瞬间提高" + (HiByte(std.DC) + HiByte(std.MAC) / 60).ToString() + "分" + (HiByte(std.MAC) % 60).ToString() + "sec.", 1);
                                boneedrecalc = true;
                            }
                            if (LoByte(std.SC) > 0)
                            {
                                ExtraAbil[Grobal2.EABIL_SCUP] = (byte)_MAX(ExtraAbil[Grobal2.EABIL_SCUP], LoByte(std.SC));
                                ExtraAbilFlag[Grobal2.EABIL_SCUP] = 0;
                                ExtraAbilTimes[Grobal2.EABIL_SCUP] = _MAX(ExtraAbilTimes[Grobal2.EABIL_SCUP], HUtil32.GetTickCount() + HiByte(std.DC) * 60 * 1000 + HiByte(std.MAC) * 1000);
                                SysMsg("精神力瞬间提高" + (HiByte(std.DC) + HiByte(std.MAC) / 60).ToString() + "分" + (HiByte(std.MAC) % 60).ToString() + "秒。", 1);
                                boneedrecalc = true;
                            }
                            if (HiByte(std.AC) > 0)
                            {
                                ExtraAbil[Grobal2.EABIL_HITSPEEDUP] = (byte)_MAX(ExtraAbil[Grobal2.EABIL_HITSPEEDUP], HiByte(std.AC));
                                ExtraAbilFlag[Grobal2.EABIL_HITSPEEDUP] = 0;
                                ExtraAbilTimes[Grobal2.EABIL_HITSPEEDUP] = _MAX(ExtraAbilTimes[Grobal2.EABIL_HITSPEEDUP], HUtil32.GetTickCount() + HiByte(std.DC) * 60 * 1000 + HiByte(std.MAC) * 1000);
                                SysMsg("敏捷度瞬间提高" + (HiByte(std.DC) + HiByte(std.MAC) / 60).ToString() + "分" + (HiByte(std.MAC) % 60).ToString() + "秒。", 1);
                                boneedrecalc = true;
                            }
                            if (LoByte(std.AC) > 0)
                            {
                                ExtraAbil[Grobal2.EABIL_HPUP] = (byte)_MAX(ExtraAbil[Grobal2.EABIL_HPUP], LoByte(std.AC));
                                ExtraAbilFlag[Grobal2.EABIL_HPUP] = 0;
                                ExtraAbilTimes[Grobal2.EABIL_HPUP] = _MAX(ExtraAbilTimes[Grobal2.EABIL_HPUP], HUtil32.GetTickCount() + HiByte(std.DC) * 60 * 1000 + HiByte(std.MAC) * 1000);
                                SysMsg("体力值瞬间提高" + (HiByte(std.DC) + HiByte(std.MAC) / 60).ToString() + "分" + (HiByte(std.MAC) % 60).ToString() + "秒。", 1);
                                boneedrecalc = true;
                            }
                            if (LoByte(std.MAC) > 0)
                            {
                                ExtraAbil[Grobal2.EABIL_MPUP] = (byte)_MAX(ExtraAbil[Grobal2.EABIL_MPUP], LoByte(std.MAC));
                                ExtraAbilFlag[Grobal2.EABIL_MPUP] = 0;
                                ExtraAbilTimes[Grobal2.EABIL_MPUP] = _MAX(ExtraAbilTimes[Grobal2.EABIL_MPUP], HUtil32.GetTickCount() + HiByte(std.DC) * 60 * 1000 + HiByte(std.MAC) * 1000);
                                SysMsg("魔法值瞬间提高" + (HiByte(std.DC) + HiByte(std.MAC) / 60).ToString() + "分" + (HiByte(std.MAC) % 60).ToString() + "秒。", 1);
                                boneedrecalc = true;
                            }
                            if (boneedrecalc)
                            {
                                RecalcAbilitys();
                                SendMsg(this, Grobal2.RM_ABILITY, 0, 0, 0, 0, "");
                            }
                            result = true;
                            break;
                        case ObjBase.INSTANT_EXP_DRUG:
                            if (LoByte(std.MAC) > 0)
                            {
                                if (new System.Random(100).Next() < LoByte(std.DC))
                                {
                                    WinExp(LoByte(std.MAC) * 2 * 100);
                                }
                                else
                                {
                                    if (LoByte(std.MAC) >= LoByte(std.AC))
                                    {
                                        WinExp((new System.Random(LoByte(std.MAC) - LoByte(std.AC)).Next() + LoByte(std.AC)) * 100);
                                    }
                                    else
                                    {
                                        WinExp((new System.Random(LoByte(std.AC) - LoByte(std.MAC)).Next() + LoByte(std.MAC)) * 100);
                                    }
                                }
                            }
                            else
                            {
                                WinExp(LoByte(std.AC) * 100);
                            }
                            result = true;
                            break;
                        case ObjBase.SHAPE_COUPLE_ALIVE_STONE:
                            flag = false;
                            for (var i = 0; i <= 12; i++)
                            {
                                pstd = M2Share.UserEngine.GetStdItem(UseItems[i].Index);
                                if (pstd != null)
                                {
                                    if ((pstd.StdMode == 22) && (pstd.Shape == ObjBase.SHAPE_ADV_COUPLERING))
                                    {
                                        flag = true;
                                        break;
                                    }
                                }
                            }
                            if (flag)
                            {
                                flag = false;
                                hum = this as TUserHuman;
                                if (hum != null)
                                {
                                    if (HUtil32.Str_ToInt(hum.fLover.GetLoverDays(), 0) >= 365)
                                    {
                                        flag = true;
                                    }
                                }
                            }
                            if (flag)
                            {
                                flag = false;
                                hum = this as TUserHuman;
                                if (hum != null)
                                {
                                    humlover = M2Share.UserEngine.GetUserHuman(hum.fLover.GetLoverName());
                                    if (humlover != null)
                                    {
                                        if ((Math.Abs(hum.CX - humlover.CX) <= 1) && (Math.Abs(hum.CY - humlover.CY) <= 1))
                                        {
                                            if (humlover.Death)
                                            {
                                                humlover.WAbil.HP = (short)(humlover.WAbil.MaxHP / 10);
                                                humlover.Alive();
                                                hum.WAbil.HP = (short)(hum.WAbil.HP / 10);
                                                hum.WAbil.MP = (short)(hum.WAbil.MP / 10);
                                                result = true;
                                            }
                                        }
                                    }
                                }
                            }
                            break;
                        default:
                            result = UseScroll(std.Shape);
                            break;
                    }
                    break;
                case 8:
                    switch (std.Shape)
                    {
                        case ObjBase.SHAPE_OF_INVITATION:
                            if (RaceServer == Grobal2.RC_USERHUMAN)
                            {
                                hum = (TUserHuman)this;
                                if (hum.GuildAgitInvitationTimeOutCheck(pu))
                                {
                                    hum.CmdGuildAgitFreeMove(pu.Dura);
                                }
                                else
                                {
                                    hum.SysMsg("这个物品已过期。", 0);
                                }
                                result = true;
                            }
                            break;
                        case ObjBase.SHAPE_OF_TELEPORTTAG:
                            UserSpaceMove(std.Reference, std.HpAdd.ToString(), std.MpAdd.ToString());
                            result = true;
                            break;
                        case ObjBase.SHAPE_OF_GIFTBOX:
                            if (IsEnoughBag())
                            {
                                GetGiftFromBox();
                                result = true;
                            }
                            break;
                        case ObjBase.SHAPE_OF_EASTEREGG:
                            if (IsEnoughBag())
                            {
                                GetGiftFromEasterEgg();
                                result = true;
                            }
                            break;
                        case ObjBase.SHAPE_OF_EGG:
                            if (IsEnoughBag())
                            {
                                GetGiftFromEgg();
                                result = true;
                            }
                            break;
                    }
                    break;
            }
            return result;
        }

        public bool IsMyMagic(int magid)
        {
            bool result;
            int i;
            result = false;
            for (i = 0; i < MagicList.Count; i++)
            {
                if (MagicList[i].MagicId == magid)
                {
                    result = true;
                    break;
                }
            }
            return result;
        }

        public bool ReadBook(TStdItem std)
        {
            TUserMagic pum;
            TUserHuman hum;
            bool result = false;
            TDefMagic pdm = M2Share.UserEngine.GetDefMagic(std.Name);
            if (pdm != null)
            {
                if (!IsMyMagic(pdm.MagicId))
                {
                    if (((pdm.Job == 99) || (pdm.Job == Job)) && (Abil.Level >= pdm.NeedLevel[0]))
                    {
                        pum = new TUserMagic();
                        pum.pDef = pdm;
                        pum.MagicId = pdm.MagicId;
                        pum.Key = '\0';
                        pum.Level = 0;
                        pum.CurTrain = 0;
                        MagicList.Add(pum);
                        RecalcAbilitys();
                        if (RaceServer == Grobal2.RC_USERHUMAN)
                        {
                            hum = this as TUserHuman;
                            hum.SendAddMagic(pum);
                        }
                        result = true;
                    }
                }
            }
            return result;
        }

        public int GetSpellPoint(TUserMagic pum)
        {
            return HUtil32.MathRound(pum.pDef.Spell / (pum.pDef.MaxTrainLevel + 1) * (pum.Level + 1)) + pum.pDef.DefSpell;
        }

        public bool DoSpell(TUserMagic pum, short xx, short yy, TCreature target)
        {
            bool result = false;
            if (M2Share.MagicMan.IsSwordSkill(pum.MagicId))
            {
                return result;
            }
            int spell = GetSpellPoint(pum);
            if (spell > 0)
            {
                if (WAbil.MP >= spell)
                {
                    DamageSpell(spell);
                    if (pum.MagicId != 42)
                    {
                        HealthSpellChanged();
                    }
                }
                else
                {
                    return result;
                }
            }
            result = M2Share.MagicMan.SpellNow(this, pum, xx, yy, target, spell);
            if (pum.MagicId == 42)
            {
                HealthSpellChanged();
            }
            return result;
        }

        public int MagPassThroughMagic(short sx, short sy, short tx, short ty, byte ndir, int magpwr, bool undeadattack)
        {
            int result;
            int i;
            int tcount;
            int acpwr;
            TCreature cret;
            tcount = 0;
            for (i = 0; i <= 12; i++)
            {
                cret = PEnvir.GetCreature(sx, sy, true) as TCreature;
                if (cret != null)
                {
                    if (IsProperTarget(cret))
                    {
                        if (cret.AntiMagic <= new System.Random(50).Next())
                        {
                            if (undeadattack)
                            {
                                acpwr = HUtil32.MathRound(magpwr * 1.5);
                            }
                            else
                            {
                                acpwr = magpwr;
                            }
                            cret.SendDelayMsg(this, Grobal2.RM_MAGSTRUCK, 0, acpwr, 0, 0, "", 600);
                            tcount++;
                        }
                    }
                }
                if (!((Math.Abs(sx - tx) <= 0) && (Math.Abs(sy - ty) <= 0)))
                {
                    ndir = M2Share.GetNextDirection(sx, sy, tx, ty);
                    if (!M2Share.GetNextPosition(PEnvir, sx, sy, ndir, 1, ref sx, ref sy))
                    {
                        break;
                    }
                }
                else
                {
                    break;
                }
            }
            result = tcount;
            return result;
        }

        public bool MagCanHitTarget(short sx, short sy, TCreature target)
        {
            byte ndir;
            bool result = false;
            if (target != null)
            {
                int olddis = Math.Abs(sx - target.CX) + Math.Abs(sy - target.CY);
                for (var i = 0; i <= 12; i++)
                {
                    ndir = M2Share.GetNextDirection(sx, sy, target.CX, target.CY);
                    if (!M2Share.GetNextPosition(PEnvir, sx, sy, ndir, 1, ref sx, ref sy))
                    {
                        break;
                    }
                    if (!PEnvir.CanFireFly(sx, sy))
                    {
                        break;
                    }
                    if ((sx == target.CX) && (sy == target.CY))
                    {
                        result = true;
                        break;
                    }
                    int dis = Math.Abs(sx - target.CX) + Math.Abs(sy - target.CY);
                    if (dis > olddis)
                    {
                        result = true;
                        break;
                    }
                    dis = olddis;
                }
            }
            return result;
        }

        public bool MagDefenceUp(int sec, int value)
        {
            bool result = false;
            if (StatusArr[Grobal2.STATE_DEFENCEUP] > 0)
            {
                if (sec > StatusArr[Grobal2.STATE_DEFENCEUP])
                {
                    StatusArr[Grobal2.STATE_DEFENCEUP] = (ushort)sec;
                    result = true;
                }
            }
            else
            {
                StatusArr[Grobal2.STATE_DEFENCEUP] = (ushort)sec;
                result = true;
            }
            StatusTimes[Grobal2.STATE_DEFENCEUP] = HUtil32.GetTickCount();
            StatusValue[Grobal2.STATE_DEFENCEUP] = (byte)_MIN(255, value);
            SysMsg("防御力上升" + sec.ToString() + "秒。", 1);
            RecalcAbilitys();
            SendMsg(this, Grobal2.RM_ABILITY, 0, 0, 0, 0, "");
            return result;
        }

        public bool MagMagDefenceUp(int sec, int value)
        {
            bool result = false;
            if (StatusArr[Grobal2.STATE_MAGDEFENCEUP] > 0)
            {
                if (sec > StatusArr[Grobal2.STATE_MAGDEFENCEUP])
                {
                    StatusArr[Grobal2.STATE_MAGDEFENCEUP] = (ushort)sec;
                    result = true;
                }
            }
            else
            {
                StatusArr[Grobal2.STATE_MAGDEFENCEUP] = (ushort)sec;
                result = true;
            }
            StatusTimes[Grobal2.STATE_MAGDEFENCEUP] = HUtil32.GetTickCount();
            StatusValue[Grobal2.STATE_MAGDEFENCEUP] = (byte)_MIN(255, value);
            SysMsg("魔法防御力上升" + sec.ToString() + "秒。", 1);
            RecalcAbilitys();
            SendMsg(this, Grobal2.RM_ABILITY, 0, 0, 0, 0, "");
            return result;
        }

        public bool MagBubbleDefenceUp(int mlevel, int sec)
        {
            int old;
            bool result = false;
            if (StatusArr[Grobal2.STATE_BUBBLEDEFENCEUP] == 0)
            {
                old = CharStatus;
                StatusArr[Grobal2.STATE_BUBBLEDEFENCEUP] = (ushort)sec;
                StatusTimes[Grobal2.STATE_BUBBLEDEFENCEUP] = HUtil32.GetTickCount();
                CharStatus = GetCharStatus();
                if (old != CharStatus)
                {
                    CharStatusChanged();
                }
                BoAbilMagBubbleDefence = true;
                MagBubbleDefenceLevel = (byte)mlevel;
                result = true;
            }
            return result;
        }

        public void DamageBubbleDefence()
        {
            if (StatusArr[Grobal2.STATE_BUBBLEDEFENCEUP] > 0)
            {
                if (StatusArr[Grobal2.STATE_BUBBLEDEFENCEUP] > 3)
                {
                    StatusArr[Grobal2.STATE_BUBBLEDEFENCEUP] = (ushort)(StatusArr[Grobal2.STATE_BUBBLEDEFENCEUP] - 3);
                }
                else
                {
                    StatusArr[Grobal2.STATE_BUBBLEDEFENCEUP] = 1;
                }
            }
        }

        public int MagMakeDefenceArea(int xx, int yy, int range, int sec, bool BoMag)
        {
            int result;
            int i;
            int j;
            int k;
            int stx;
            int sty;
            int enx;
            int eny;
            int tcount;
            TMapInfo pm = null;
            bool inrange;
            TCreature cret;
            tcount = 0;
            stx = xx - range;
            enx = xx + range;
            sty = yy - range;
            eny = yy + range;
            for (i = stx; i <= enx; i++)
            {
                for (j = sty; j <= eny; j++)
                {
                    inrange = PEnvir.GetMapXY(i, j, ref pm);
                    if (inrange)
                    {
                        if (pm.OBJList != null)
                        {
                            for (k = 0; k < pm.OBJList.Count; k++)
                            {
                                if (((TAThing)pm.OBJList[k]).Shape == Grobal2.OS_MOVINGOBJECT)
                                {
                                    cret = ((TAThing)pm.OBJList[k]).AObject as TCreature;
                                    if (cret != null)
                                    {
                                        if (!cret.BoGhost)
                                        {
                                            if (IsProperFriend(cret))
                                            {
                                                if (!BoMag)
                                                {
                                                    cret.MagDefenceUp(sec, (LoByte(WAbil.SC) / 9) + new System.Random(HiByte(WAbil.SC) / 9).Next());
                                                }
                                                else
                                                {
                                                    cret.MagMagDefenceUp(sec, (LoByte(WAbil.SC) / 9) + new System.Random(HiByte(WAbil.SC) / 9).Next());
                                                }
                                                tcount++;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            result = tcount;
            return result;
        }

        public int MagMakeCurseArea(int xx, int yy, int range, int sec, int pwr, int skilllevel, bool BoMag)
        {
            int result;
            int i;
            int j;
            int k;
            int stx;
            int sty;
            int enx;
            int eny;
            int tcount;
            TMapInfo pm = null;
            bool inrange;
            TCreature cret;
            bool isNormalAttack;
            bool isAttack;
            int targetsec;
            tcount = 0;
            stx = xx - range;
            enx = xx + range;
            sty = yy - range;
            eny = yy + range;
            for (i = stx; i <= enx; i++)
            {
                for (j = sty; j <= eny; j++)
                {
                    inrange = PEnvir.GetMapXY(i, j, ref pm);
                    if (inrange)
                    {
                        if (pm.OBJList != null)
                        {
                            for (k = 0; k < pm.OBJList.Count; k++)
                            {
                                if (((TAThing)pm.OBJList[k]).Shape == Grobal2.OS_MOVINGOBJECT)
                                {
                                    cret = ((TAThing)pm.OBJList[k]).AObject as TCreature;
                                    if (cret != null)
                                    {
                                        if ((!cret.BoGhost) && (!cret.Death))
                                        {
                                            if (!BoMag)
                                            {
                                                if (IsProperTarget(cret))
                                                {
                                                    targetsec = (sec / 6) - cret.PoisonRecover;
                                                    isAttack = false;
                                                    if (new System.Random(90 + (cret.AntiPoison * 2)).Next() < (14 + (skilllevel + 1) * 8))
                                                    {
                                                        isAttack = true;
                                                    }
                                                    if (isAttack && (targetsec > 0))
                                                    {
                                                        cret.SendDelayMsg(this, Grobal2.RM_CURSE, (short)targetsec, pwr, 0, 0, "", 1200);
                                                        cret.SendDelayMsg(cret, Grobal2.RM_STRUCK, 1, WAbil.HP, WAbil.MP, this.ActorId, "", 1200);
                                                        tcount++;
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                if (IsProperTarget(cret))
                                                {
                                                    // 犬伏拌魂窍备
                                                    isNormalAttack = true;
                                                    targetsec = sec;
                                                    if (cret.RaceServer == Grobal2.RC_USERHUMAN)
                                                    {
                                                        isNormalAttack = false;
                                                        // 吝刀 雀汗
                                                        targetsec = (sec / 6) - cret.PoisonRecover;
                                                    }
                                                    else
                                                    {
                                                        if (cret.Abil.Level >= 60)
                                                        {
                                                            isNormalAttack = false;
                                                            targetsec = sec / 4;
                                                        }
                                                    }
                                                    // 老馆各犬伏: Random(70)<14+(skill_level+1)*5+lvgap
                                                    // 60饭骇捞惑&蜡历犬伏: Random(40)<14+(skill_level+1)*5+lvgap
                                                    isAttack = false;
                                                    // AntiPoison := ;
                                                    if (isNormalAttack)
                                                    {
                                                        if (new System.Random(80).Next() < (14 + (skilllevel + 1) * 3 + (Abil.Level - cret.Abil.Level)))
                                                        {
                                                            isAttack = true;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        if (new System.Random(90 + (cret.AntiPoison * 2)).Next() < (14 + (skilllevel + 1) * 3 + (Abil.Level - cret.Abil.Level)))
                                                        {
                                                            isAttack = true;
                                                        }
                                                    }
                                                    if (isAttack && (targetsec > 0))
                                                    {
                                                        cret.SendDelayMsg(this, Grobal2.RM_CURSE, (short)targetsec, pwr, 0, 0, "", 1200);
                                                        cret.SendDelayMsg(cret, Grobal2.RM_STRUCK, 1, WAbil.HP, WAbil.MP, this.ActorId, "", 1200);
                                                        // cret.MagCurse (sec , pwr);
                                                        tcount++;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            result = tcount;
            return result;
        }

        // 2003/03/15 脚痹公傍 眠啊
        public bool MagDcUp(int sec, int pwr)
        {
            bool result;
            int i;
            int UpDC;
            TCreature cret;
            UpDC = pwr;
            ExtraAbil[Grobal2.EABIL_DCUP] = (byte)_MIN(255, _MAX(ExtraAbil[Grobal2.EABIL_DCUP], UpDC));
            ExtraAbilFlag[Grobal2.EABIL_DCUP] = 0;
            ExtraAbilTimes[Grobal2.EABIL_DCUP] = _MAX(ExtraAbilTimes[Grobal2.EABIL_DCUP], HUtil32.GetTickCount() + (sec * 1000));
            SysMsg("攻击力提高" + ((ExtraAbilTimes[Grobal2.EABIL_DCUP] - GetTickCount) / 1000 / 60).ToString() + "分 " + ((ExtraAbilTimes[Grobal2.EABIL_DCUP] - GetTickCount) / 1000 % 60).ToString() + "秒。", 1);
            RecalcAbilitys();
            SendMsg(this, Grobal2.RM_ABILITY, 0, 0, 0, 0, "");
            if (SlaveList.Count >= 1)
            {
                for (i = 0; i < SlaveList.Count; i++)
                {
                    cret = SlaveList[i];
                    if (cret != null)
                    {
                        cret.ExtraAbil[Grobal2.EABIL_DCUP] = (byte)_MIN(255, _MAX(cret.ExtraAbil[Grobal2.EABIL_DCUP], UpDC));
                        cret.ExtraAbilFlag[Grobal2.EABIL_DCUP] = 0;
                        cret.ExtraAbilTimes[Grobal2.EABIL_DCUP] = _MAX(cret.ExtraAbilTimes[Grobal2.EABIL_DCUP], (int)(HUtil32.GetTickCount() + ((long)sec * 1000)));
                        // 檬窜困
                        cret.ExtraAbil[Grobal2.EABIL_MCUP] = (byte)_MIN(255, _MAX(cret.ExtraAbil[Grobal2.EABIL_MCUP], UpDC));
                        cret.ExtraAbilFlag[Grobal2.EABIL_MCUP] = 0;
                        cret.ExtraAbilTimes[Grobal2.EABIL_MCUP] = _MAX(cret.ExtraAbilTimes[Grobal2.EABIL_MCUP], (int)(HUtil32.GetTickCount() + ((long)sec * 1000)));
                        // 檬窜困
                        cret.RecalcAbilitys();
                    }
                }
            }
            result = true;
            return result;
        }

        public void MagCurse(int sec, int pwrrate)
        {
            MakePoison(Grobal2.POISON_SLOW, sec, 1);
            if (ExtraAbilTimes[Grobal2.EABIL_PWRRATE] < (HUtil32.GetTickCount() + ((long)sec * 1000)))
            {
                ExtraAbil[Grobal2.EABIL_PWRRATE] = (byte)pwrrate;
                ExtraAbilTimes[Grobal2.EABIL_PWRRATE] = (int)(HUtil32.GetTickCount() + ((long)sec * 1000));
                // 檬窜困
                if (pwrrate < 100)
                {
                    SysMsg("攻击能力" + (100 - pwrrate).ToString() + "%减少" + sec.ToString() + "秒。", 1);
                }
                else
                {
                    SysMsg("攻击能力" + (pwrrate - 100).ToString() + "%增加" + sec.ToString() + "秒。", 1);
                }
                RecalcAbilitys();
                SendMsg(this, Grobal2.RM_ABILITY, 0, 0, 0, 0, "");
            }
        }

        public bool CheckMagicLevelup(TUserMagic pum)
        {
            bool result;
            int lv;
            result = false;
            if (pum.Level >= 0 && pum.Level <= 3 && (pum.Level <= pum.pDef.MaxTrainLevel))
            {
                lv = pum.Level;
            }
            else
            {
                lv = 0;
            }
            if (pum.Level < pum.pDef.MaxTrainLevel)
            {
                if (pum.CurTrain >= pum.pDef.MaxTrain[lv])
                {
                    if (pum.Level < pum.pDef.MaxTrainLevel)
                    {
                        // 饭骇 棵妨具窃.
                        pum.CurTrain = pum.CurTrain - pum.pDef.MaxTrain[lv];
                        pum.Level = (byte)(pum.Level + 1);
                        UpdateDelayMsgCheckParam1(this, Grobal2.RM_MAGIC_LVEXP, 0, pum.pDef.MagicId, pum.Level, pum.CurTrain, "", 800);
                        CheckMagicSpecialAbility(pum);
                    }
                    else
                    {
                        // 促 棵啡澜
                        pum.CurTrain = pum.pDef.MaxTrain[lv];
                    }
                    result = true;
                }
            }
            return result;
        }

        public void CheckMagicSpecialAbility(TUserMagic pum)
        {
            if (pum.pDef.MagicId == 28)
            {
                // 沤扁颇楷
                if (pum.Level >= 2)
                {
                    // 沤扁颇楷 2窜拌 捞惑 荐访沁阑 版快
                    BoAbilSeeHealGauge = true;
                }
            }
        }

        public int GetDailyQuest()
        {
            int result;
            short ayear = (short)DateTime.Today.Year;
            short amon = (short)DateTime.Today.Month;
            short aday = (short)DateTime.Today.Day;
            short calcdate = (short)(amon * 31 + aday);
            if ((DailyQuestNumber == 0) || (DailyQuestGetDate != calcdate))
            {
                result = 0;
            }
            else
            {
                result = DailyQuestNumber;
            }
            return result;
        }

        public void SetDailyQuest(int qnumber)
        {
            short ayear = (short)DateTime.Today.Year;
            short amon = (short)DateTime.Today.Month;
            short aday = (short)DateTime.Today.Day;
            DailyQuestNumber = (short)qnumber;
            DailyQuestGetDate = (short)(amon * 31 + aday);
        }

        public virtual void RunMsg(TMessageInfo msg)
        {
            int n = 0;
            int v1 = 0;
            int magx = 0;
            int magy = 0;
            int magpwr = 0;
            int range = 0;
            TCreature hiter;
            TCreature target;
            int plusdamage;
            plusdamage = 0;
            switch (msg.Ident)
            {
                case Grobal2.RM_REFMESSAGE:
                    SendRefMsg((short)msg.sender, msg.wParam, msg.lParam1, msg.lParam2, msg.lParam3, msg.description);
                    if (((int)msg.sender) == Grobal2.RM_STRUCK)
                    {
                        if (RaceServer != Grobal2.RC_USERHUMAN)
                        {
                            SendMsg(this, (short)(int)msg.sender, msg.wParam, msg.lParam1, msg.lParam2, msg.lParam3, msg.description);
                        }
                    }
                    break;
                case Grobal2.RM_DELAYMAGIC:
                    magpwr = msg.wParam;
                    //magx = HUtil32.Loword(msg.lParam1);
                    //magy = HUtil32.Hiword(msg.lParam1);
                    range = msg.lParam2;
                    target = M2Share.ObjectMgr.Get(msg.lParam3);
                    if (target != null)
                    {
                        if ((target.RaceServer == Grobal2.RC_FIREDRAGON) || (target.RaceServer == Grobal2.RC_DRAGONBODY))
                        {
                            if ((Math.Abs(this.CX - target.CX) <= 8) && (Math.Abs(this.CY - target.CY) <= 8))
                            {
                                target.SendMsg(this, Grobal2.RM_DRAGON_EXP, 0, new System.Random(3).Next() + 1, 0, 0, "");
                            }
                        }
                        n = target.GetMagStruckDamage(this, magpwr);
                        if (n > 0)
                        {
                            if (target.RaceServer >= Grobal2.RC_ANIMAL)
                            {
                                magpwr = HUtil32.MathRound(magpwr * 1.2);
                            }
                            if ((Math.Abs(magx - target.CX) <= range) && (Math.Abs(magy - target.CY) <= range))
                            {
                                target.SendMsg(this, Grobal2.RM_MAGSTRUCK, 0, magpwr, 0, 0, "");
                            }
                        }
                    }
                    break;
                case Grobal2.RM_MAGSTRUCK:
                case Grobal2.RM_MAGSTRUCK_MINE:
                    hiter = msg.sender as TCreature;
                    if (hiter != null)
                    {
                        plusdamage = hiter.PlusFinalDamage;
                    }
                    else
                    {
                        plusdamage = 0;
                    }
                    if ((msg.Ident == Grobal2.RM_MAGSTRUCK) && (RaceServer >= Grobal2.RC_ANIMAL) && !RushMode)
                    {
                        if (Abil.Level < M2Share.MAXKINGLEVEL - 1)
                        {
                            WalkTime = WalkTime + 800 + new System.Random(1000).Next();
                        }
                    }
                    v1 = GetMagStruckDamage(null, msg.lParam1);
                    if (v1 > 0)
                    {
                        target = msg.sender as TCreature;
                        SelectTarget(target);
                        StruckDamage(v1 + plusdamage);
                        HealthSpellChanged();
                        SendRefMsg(Grobal2.RM_STRUCK_MAG, (short)(v1 + plusdamage), WAbil.HP, WAbil.MP, (int)msg.sender, "");
                        if (RaceServer != Grobal2.RC_USERHUMAN)
                        {
                            if (BoAnimal)
                            {
                                MeatQuality = MeatQuality - v1 * 1000;
                            }
                            SendMsg(this, Grobal2.RM_STRUCK, (short)(v1 + plusdamage), WAbil.HP, WAbil.MP, (int)msg.sender, "");
                        }
                    }
                    break;
                case Grobal2.RM_MAGHEALING:
                    if (IncHealing + msg.lParam1 < 300)
                    {
                        if (RaceServer == Grobal2.RC_USERHUMAN)
                        {
                            IncHealing = (byte)(IncHealing + msg.lParam1);
                            PerHealing = 5;
                        }
                        else
                        {
                            IncHealing = (byte)(IncHealing + msg.lParam1);
                            PerHealing = 5;
                        }
                    }
                    else
                    {
                        IncHealing = 300;
                    }
                    break;
                case Grobal2.RM_MAKEPOISON:
                    hiter = M2Share.ObjectMgr.Get(msg.lParam2);
                    if (hiter != null)
                    {
                        if (IsProperTarget(hiter))
                        {
                            SelectTarget(hiter);
                            if (Abil.Level < 60)
                            {
                                SetLastHiter(hiter);
                            }
                        }
                        if ((RaceServer == Grobal2.RC_USERHUMAN) && (hiter.RaceServer == Grobal2.RC_USERHUMAN))
                        {
                            AddPkHiter(hiter);
                            SetLastHiter(hiter);
                        }
                        else if (Master != null)
                        {
                            if (Master != hiter)
                            {
                                AddPkHiter(hiter);
                                SetLastHiter(hiter);
                            }
                        }
                        MakePoison(msg.wParam, msg.lParam1, msg.lParam3);
                    }
                    else
                    {
                        MakePoison(msg.wParam, msg.lParam1, msg.lParam3);
                    }
                    break;
                case Grobal2.RM_DOOPENHEALTH:
                    MakeOpenHealth();
                    break;
                case Grobal2.RM_TRANSPARENT:
                    M2Share.MagicMan.MagMakePrivateTransparent(this, msg.lParam1);
                    break;
                case Grobal2.RM_RANDOMSPACEMOVE:
                    RandomSpaceMove(msg.description, msg.wParam);
                    break;
                case Grobal2.RM_DECREFOBJCOUNT:
                    DecRefObjCount();
                    break;
                case Grobal2.RM_DRAGON_EXP:
                    if (M2Share.gFireDragon != null)
                    {
                        M2Share.gFireDragon.ChangeExp(msg.lParam1);
                    }
                    break;
                case Grobal2.RM_CURSE:
                    MagCurse(msg.wParam, msg.lParam1);
                    break;
            }
        }

        public void UseLamp()
        {
            int old;
            int dura;
            TUserHuman hum;
            try
            {
                if (UseItems[Grobal2.U_RIGHTHAND].Index > 0)
                {
                    old = HUtil32.MathRound(UseItems[Grobal2.U_RIGHTHAND].Dura / 1000);
                    dura = UseItems[Grobal2.U_RIGHTHAND].Dura - 2;
                    if (dura <= 0)
                    {
                        UseItems[Grobal2.U_RIGHTHAND].Dura = 0;
                        if (RaceServer == Grobal2.RC_USERHUMAN)
                        {
                            hum = this as TUserHuman;
                            hum.SendDelItem(UseItems[Grobal2.U_RIGHTHAND]);
                        }
                        UseItems[Grobal2.U_RIGHTHAND].Index = 0;
                        Light = GetMyLight();
                        SendRefMsg(Grobal2.RM_CHANGELIGHT, 0, 0, 0, 0, "");
                        SendMsg(this, Grobal2.RM_LAMPCHANGEDURA, 0, UseItems[Grobal2.U_RIGHTHAND].Dura, 0, 0, "");
                        RecalcAbilitys();
                        SendMsg(this, Grobal2.RM_ABILITY, 0, 0, 0, 0, "");
                    }
                    else
                    {
                        UseItems[Grobal2.U_RIGHTHAND].Dura = (short)dura;
                    }
                    if (old != HUtil32.MathRound(dura / 1000))
                    {
                        SendMsg(this, Grobal2.RM_LAMPCHANGEDURA, 0, UseItems[Grobal2.U_RIGHTHAND].Dura, 0, 0, "");
                    }
                }
            }
            catch
            {
                M2Share.MainOutMessage("[Exception] TCreature.UseLamp");
            }
        }

        public virtual void Run()
        {
            TMessageInfo msg = null;
            int i;
            int n;
            int hp;
            int mp;
            int plus;
            int waittime;
            long inchstime;
            TCreature cret;
            bool chg;
            bool needrecalc;
            int test;
            int identbackup;
            bool bcheckDeath;
            long DuringIllegalTime;
            TUserHuman hum;
            test = 0;
            identbackup = 0;
            try
            {
                while (GetMsg(ref msg))
                {
                    identbackup = msg.Ident;
                    RunMsg(msg);
                }
            }
            catch
            {
                M2Share.MainOutMessage("[Exception] TCreature.Run 0 : " + identbackup.ToString() + ":" + msg.Ident.ToString());
            }
            try
            {
                test = 1;
                if (NeverDie)
                {
                    WAbil.HP = WAbil.MaxHP;
                    WAbil.MP = WAbil.MaxMP;
                }
                test = 2;
                n = (int)((HUtil32.GetTickCount() - ticksec) / 20);
                ticksec = HUtil32.GetTickCount();
                HealthTick += n;
                SpellTick += n;
                test = 4;
                if (!Death)
                {
                    if (WAbil.HP < WAbil.MaxHP)
                    {
                        if (HealthTick >= ObjBase.HEALTHFILLTICK)
                        {
                            plus = WAbil.MaxHP / 75 + 1;
                            plus = plus + (plus * HealthRecover / 10);
                            if (WAbil.HP + plus < WAbil.MaxHP)
                            {
                                WAbil.HP = (short)(WAbil.HP + plus);
                            }
                            else
                            {
                                WAbil.HP = WAbil.MaxHP;
                            }
                            HealthSpellChanged();
                        }
                    }
                    test = 5;
                    if (WAbil.MP < WAbil.MaxMP)
                    {
                        if (SpellTick >= ObjBase.SPELLFILLTICK)
                        {
                            plus = WAbil.MaxMP / 18 + 1;
                            plus = plus + (plus * SpellRecover / 10);
                            if (WAbil.MP + plus < WAbil.MaxMP)
                            {
                                WAbil.MP = (short)(WAbil.MP + plus);
                            }
                            else
                            {
                                WAbil.MP = WAbil.MaxMP;
                            }
                            HealthSpellChanged();
                        }
                    }
                    test = 6;
                    if (WAbil.HP == 0)
                    {
                        if (BoAbilRevival)
                        {
                            if (HUtil32.GetTickCount() - LatestRevivalTime > 60 * 1000)
                            {
                                LatestRevivalTime = HUtil32.GetTickCount();
                                ItemDamageRevivalRing();
                                WAbil.HP = WAbil.MaxHP;
                                HealthSpellChanged();
                                SysMsg("戒指的力量令您复活了。", 1);
                            }
                        }
                        if (WAbil.HP == 0)
                        {
                            Die();
                        }
                    }
                    if (HealthTick >= ObjBase.HEALTHFILLTICK)
                    {
                        HealthTick = 0;
                    }
                    if (SpellTick >= ObjBase.SPELLFILLTICK)
                    {
                        SpellTick = 0;
                    }
                }
                else
                {
                    test = 7;
                    if (HUtil32.GetTickCount() - DeathTime > 3 * 60 * 1000)
                    {
                        MakeGhost(5);
                    }
                }
            }
            catch
            {
                M2Share.MainOutMessage("[Exception] TCreature.Run 1 > " + test.ToString());
            }
            try
            {
                if (!Death && ((IncSpell > 0) || (IncHealth > 0) || (IncHealing > 0)))
                {
                    inchstime = 600 - _MIN(400, Abil.Level * 10);
                    if ((HUtil32.GetTickCount() - IncHealthSpellTime >= inchstime) && !Death)
                    {
                        n = _MIN(200, (int)(HUtil32.GetTickCount() - IncHealthSpellTime - inchstime));
                        IncHealthSpellTime = GetTickCount + n;
                        if ((IncHealth > 0) || (IncSpell > 0) || (PerHealing > 0))
                        {
                            if (PerHealth <= 0)
                            {
                                PerHealth = 1;
                            }
                            if (PerSpell <= 0)
                            {
                                PerSpell = 1;
                            }
                            if (PerHealing <= 0)
                            {
                                PerHealing = 1;
                            }
                            if (IncHealth < PerHealth)
                            {
                                hp = IncHealth;
                                IncHealth = 0;
                            }
                            else
                            {
                                hp = PerHealth;
                                IncHealth = (byte)(IncHealth - PerHealth);
                            }
                            if (IncSpell < PerSpell)
                            {
                                mp = IncSpell;
                                IncSpell = 0;
                            }
                            else
                            {
                                mp = PerSpell;
                                IncSpell = (byte)(IncSpell - PerSpell);
                            }
                            if (IncHealing < PerHealing)
                            {
                                hp = hp + IncHealing;
                                IncHealing = 0;
                            }
                            else
                            {
                                hp = hp + PerHealing;
                                IncHealing = (byte)(IncHealing - PerHealing);
                            }
                            PerHealth = 5 + (Abil.Level / 10);
                            PerSpell = 5 + (Abil.Level / 10);
                            PerHealing = 5;
                            IncHealthSpell(hp, mp);
                            if (WAbil.HP == WAbil.MaxHP)
                            {
                                IncHealth = 0;
                                IncHealing = 0;
                            }
                            if (WAbil.MP == WAbil.MaxMP)
                            {
                                IncSpell = 0;
                            }
                        }
                    }
                }
                else
                {
                    IncHealthSpellTime = HUtil32.GetTickCount();
                }
                if (HealthTick < -ObjBase.HEALTHFILLTICK)
                {
                    if (WAbil.HP > 1)
                    {
                        WAbil.HP = (short)(WAbil.HP - 1);
                        HealthTick = HealthTick + ObjBase.HEALTHFILLTICK;
                        HealthSpellChanged();
                    }
                }
            }
            catch
            {
                M2Share.MainOutMessage("[Exception] TCreature.Run 2");
            }
            test = 0;
            try
            {
                if (TargetCret != null)
                {
                    if ((HUtil32.GetTickCount() - TargetFocusTime > 30 * 1000) || TargetCret.Death || TargetCret.BoGhost || (Math.Abs(TargetCret.CX - CX) > 15) || (Math.Abs(TargetCret.CY - CY) > 15))
                    {
                        TargetCret = null;
                    }
                }
                test = 1;
                if (LastHiter != null)
                {
                    if (RaceServer != Grobal2.RC_USERHUMAN)
                    {
                        DuringIllegalTime = 30 * 1000;
                    }
                    else
                    {
                        DuringIllegalTime = 60 * 1000;
                    }
                    if (HUtil32.GetTickCount() - LastHitTime > DuringIllegalTime)
                    {
                        LastHiter = null;
                    }
                    else if (LastHiter.Death || LastHiter.BoGhost)
                    {
                        LastHiter = null;
                    }
                }
                test = 2;
                if (ExpHiter != null)
                {
                    if ((HUtil32.GetTickCount() - ExpHitTime > 6 * 1000) || ExpHiter.Death || ExpHiter.BoGoodCrazyMode || ExpHiter.BoGhost)
                    {
                        ExpHiter = null;
                    }
                }
                test = 3;
                if (Master != null)
                {
                    BoNoItem = true;
                    test = 4;
                    waittime = 1000;
                    if (Master.RaceServer == Grobal2.RC_USERHUMAN)
                    {
                        if ((Master as TUserHuman).BoChangeServer)
                        {
                            waittime = 15 * 1000;
                        }
                    }
                    test = 5;
                    if ((Master.Death && (HUtil32.GetTickCount() > 1000 + Master.DeathTime)) || (Master.BoGhost && (HUtil32.GetTickCount() > waittime + Master.GhostTime)))
                    {
                        WAbil.HP = 0;
                    }
                }
                for (i = SlaveList.Count - 1; i >= 0; i--)
                {
                    try
                    {
                        bcheckDeath = SlaveList[i].Death;
                    }
                    catch
                    {
                        SlaveList.RemoveAt(i);
                        M2Share.MainOutMessage("MEMORY CHECK ERROR! TCreature.Run 2:6");
                        continue;
                    }
                    if ((SlaveList[i] == null) || SlaveList[i].Death || SlaveList[i].BoGhost || (SlaveList[i].Master != this))
                    {
                        SlaveList.RemoveAt(i);
                    }
                }
                test = 7;
                if (BoHolySeize)
                {
                    if (HUtil32.GetTickCount() - HolySeizeStart > HolySeizeTime)
                    {
                        BreakHolySeize();
                    }
                }
                test = 8;
                if (BoCrazyMode || BoGoodCrazyMode)
                {
                    if (HUtil32.GetTickCount() - CrazyModeStart > CrazyModeTime)
                    {
                        BreakCrazyMode();
                    }
                }
                test = 9;
                if (BoOpenHealth)
                {
                    if (HUtil32.GetTickCount() - OpenHealthStart > OpenHealthTime)
                    {
                        BreakOpenHealth();
                    }
                }
            }
            catch
            {
                M2Share.MainOutMessage("[Exception] TCreature.Run 3:" + test.ToString());
            }
            try
            {
                if (HUtil32.GetTickCount() - time10min > 2 * 1000 * 60)
                {
                    time10min = HUtil32.GetTickCount();
                    if (PlayerKillingPoint > 0)
                    {
                        DecPKPoint(1);
                    }
                }
                if (HUtil32.GetTickCount() - time500ms > 1000)
                {
                    time500ms = time500ms + 1000;
                    if (RaceServer == Grobal2.RC_USERHUMAN)
                    {
                        UseLamp();
                    }
                }
                if (HUtil32.GetTickCount() - time5sec > 5 * 1000)
                {
                    time5sec = HUtil32.GetTickCount();
                    if (RaceServer == Grobal2.RC_USERHUMAN)
                    {
                        CheckTimeOutPkHiterList();
                    }
                }
                if (HUtil32.GetTickCount() - time10sec > 10 * 1000)
                {
                    time10sec = HUtil32.GetTickCount();
                    if (Master != null)
                    {
                        if (HUtil32.GetTickCount() > MasterRoyaltyTime)
                        {
                            for (i = Master.SlaveList.Count - 1; i >= 0; i--)
                            {
                                if (Master.SlaveList[i] == this)
                                {
                                    Master.SlaveList.RemoveAt(i);
                                    break;
                                }
                            }
                            Master = null;
                            WAbil.HP = (short)(WAbil.HP / 10);
                            UserNameChanged();
                        }
                        if (SlaveLifeTime != 0)
                        {
                            if (HUtil32.GetTickCount() - SlaveLifeTime > 12 * 60 * 60 * 1000)
                            {
                                WAbil.HP = 0;
                                BoDisapear = true;
                            }
                        }
                    }
                }
                if ((RaceServer == Grobal2.RC_USERHUMAN) && (Abil.Level > M2Share.g_nExpErienceLevel) && M2Share.boSecondCardSystem)
                {
                    if (HUtil32.GetTickCount() - time60sec > 60 * 1000)
                    {
                        time60sec = HUtil32.GetTickCount();
                        hum = this as TUserHuman;
                        if ((hum.SecondsCard <= 0) && (PEnvir.MapName != M2Share.RECHARGINGMAP))
                        {
                            hum.SecondsCard = 0;
                            SysMsg("[账户信息]当前账户充值时间已到期", 3);
                            if (M2Share.GrobalEnvir.GetEnvir(M2Share.RECHARGINGMAP) != null)
                            {
                                SendRefMsg(Grobal2.RM_SPACEMOVE_HIDE, 0, 0, 0, 0, "");
                                RandomSpaceMove(M2Share.RECHARGINGMAP, 0);// 时间到期飞到指定地图
                            }
                        }
                        else
                        {
                            if (hum.SecondsCard > 0)
                            {
                                hum.SecondsCard -= 60;
                                if (hum.SecondsCard <= (60 * 5))
                                {
                                    SysMsg(string.Format("[账户信息]当前账户充值时间剩余{0}分钟", hum.SecondsCard / 60), 3);
                                }
                            }
                        }
                    }
                }
                if (HUtil32.GetTickCount() - time30sec > 30 * 1000)
                {
                    time30sec = HUtil32.GetTickCount();
                    if (GroupOwner != null)
                    {
                        if (GroupOwner.BoGhost)
                        {
                            GroupOwner = null;
                        }
                    }
                    if (GroupOwner == this)
                    {
                        for (i = GroupMembers.Count - 1; i >= 0; i--)
                        {
                            cret = GroupOwner.GroupMembers[i];
                            if (cret.Death || cret.BoGhost)
                            {
                                GroupMembers.RemoveAt(i);
                            }
                        }
                    }
                    if (DealCret != null)
                    {
                        if (DealCret.BoGhost)
                        {
                            DealCret = null;
                        }
                    }
                    PEnvir.VerifyMapTime(CX, CY, this);
                }
            }
            catch
            {
                M2Share.MainOutMessage("[Exception] TCreature.Run 4");
            }
            try
            {
                // 惑怕... 矫埃捞 促 登菌绰瘤 犬牢窃.
                chg = false;
                needrecalc = false;
                for (i = 0; i < Grobal2.STATUSARR_SIZE; i++)
                {
                    if (StatusArr[i] > 0)
                    {
                        if (StatusArr[i] < 60000)
                        {
                            if (HUtil32.GetTickCount() - StatusTimes[i] > 1000)
                            {
                                StatusArr[i] = (ushort)(StatusArr[i] - 1);
                                StatusTimes[i] = StatusTimes[i] + 1000;
                                if (StatusArr[i] == 0)
                                {
                                    chg = true;
                                    switch (i)
                                    {
                                        case Grobal2.STATE_DEFENCEUP:
                                            needrecalc = true;
                                            SysMsg("防御力回复正常。", 1);
                                            break;
                                        case Grobal2.STATE_MAGDEFENCEUP:
                                            needrecalc = true;
                                            SysMsg("魔法防御力回复正常。", 1);
                                            break;
                                        case Grobal2.STATE_TRANSPARENT:
                                            BoHumHideMode = false;
                                            break;
                                        case Grobal2.STATE_BUBBLEDEFENCEUP:
                                            BoAbilMagBubbleDefence = false;
                                            break;
                                        default:
                                            break;
                                    }
                                }
                                else if (StatusArr[i] == 10)
                                {
                                    switch (i)
                                    {
                                        case Grobal2.STATE_DEFENCEUP:
                                            // 10檬傈 皋矫瘤(sonmg 2005/02/23)
                                            SysMsg("防御力" + StatusArr[i].ToString() + "秒后恢复正常。", 1);
                                            break;
                                        case Grobal2.STATE_MAGDEFENCEUP:
                                            SysMsg("魔法防御力" + StatusArr[i].ToString() + "秒后恢复正常。", 1);
                                            break;
                                    }
                                }
                            }
                        }
                    }
                }
                for (i = 0; i < Grobal2.EXTRAABIL_SIZE; i++)
                {
                    if (ExtraAbil[i] > 0)
                    {
                        if (HUtil32.GetTickCount() > ExtraAbilTimes[i])
                        {
                            ExtraAbil[i] = 0;
                            ExtraAbilFlag[i] = 0;
                            needrecalc = true;
                            switch (i)
                            {
                                case Grobal2.EABIL_DCUP:
                                    SysMsg("攻击力恢复正常。", 1);
                                    break;
                                case Grobal2.EABIL_MCUP:
                                    SysMsg("魔法力恢复正常。", 1);
                                    break;
                                case Grobal2.EABIL_SCUP:
                                    SysMsg("精神力恢复正常。", 1);
                                    break;
                                case Grobal2.EABIL_HITSPEEDUP:
                                    SysMsg("攻击速度恢复正常。", 1);
                                    break;
                                case Grobal2.EABIL_HPUP:
                                    SysMsg("体力值恢复正常。", 1);
                                    break;
                                case Grobal2.EABIL_MPUP:
                                    SysMsg("魔法值恢复正常。", 1);
                                    break;
                                case Grobal2.EABIL_PWRRATE:
                                    SysMsg("攻击能力恢复正常。", 1);
                                    break;
                            }
                        }
                        else if ((ExtraAbilFlag[i] == 0) && (HUtil32.GetTickCount() > ExtraAbilTimes[i] - 10000))
                        {
                            ExtraAbilFlag[i] = 1;
                            switch (i)
                            {
                                case Grobal2.EABIL_DCUP:
                                    SysMsg("攻击力10秒后恢复正常。", 1);
                                    break;
                                case Grobal2.EABIL_MCUP:
                                    SysMsg("魔法力10秒后恢复正常。", 1);
                                    break;
                                case Grobal2.EABIL_SCUP:
                                    SysMsg("精神力10秒后恢复正常。", 1);
                                    break;
                                case Grobal2.EABIL_HITSPEEDUP:
                                    SysMsg("攻击速度10秒后恢复正常。", 1);
                                    break;
                                case Grobal2.EABIL_HPUP:
                                    SysMsg("体力值10秒后恢复正常。", 1);
                                    break;
                                case Grobal2.EABIL_MPUP:
                                    SysMsg("魔法值10秒后恢复正常。", 1);
                                    break;
                            }
                        }
                    }
                }

                if (chg)
                {
                    CharStatus = GetCharStatus();
                    CharStatusChanged();
                }
                if (needrecalc)
                {
                    RecalcAbilitys();
                    SendMsg(this, Grobal2.RM_ABILITY, 0, 0, 0, 0, "");
                }
            }
            catch
            {
                M2Share.MainOutMessage("[Exception] TCreature.Run 5");
            }
            try
            {
                // 惑怕... 吝刀(眉仿捞 皑家)
                if (HUtil32.GetTickCount() - poisontime > 2500)
                {
                    poisontime = HUtil32.GetTickCount();
                    if (StatusArr[Grobal2.POISON_DECHEALTH] > 0)
                    {
                        if (BoAnimal)
                        {
                            // 绊扁啊 唱坷绰 悼拱牢 版快..
                            MeatQuality = MeatQuality - 1000;
                            // 绊扁龙捞 摹疙利栏肺 唱狐柳促.
                        }
                        // 踌刀 吧赴 逞捞 荤恩捞绊 朝 锭赴 逞捞 荤恩牢单 眉农且 荐 绝阑 锭...
                        // 眉仿 1 捞窍肺 别捞瘤 臼霸(磷瘤 臼霸) sonmg 2004/07/14
                        if (RaceServer == Grobal2.RC_USERHUMAN)
                        {
                            if ((LastHiter == null) && (LastHiterRace == Grobal2.RC_USERHUMAN))
                            {
                                // 付瘤阜 锭赴 逞捞 穿焙瘤 葛福瘤父 荤恩老 锭绰 磷瘤 臼绰促.
                                DamageHealth(1 + PoisonLevel, 1);
                                // 1 + Random(3));
                            }
                            else
                            {
                                // 付瘤阜 锭赴 逞捞 荤恩捞 酒匆 锭...
                                DamageHealth(1 + PoisonLevel, 0);
                                // 1 + Random(3));
                            }
                        }
                        else
                        {
                            // 阁胶磐啊 吝刀登菌阑 锭...
                            DamageHealth(1 + PoisonLevel, 0);
                            // 1 + Random(3));
                        }
                        HealthTick = 0;
                        // 眉仿 雀汗 救凳
                        SpellTick = 0;
                        // 付仿 雀汗 救凳
                        HealthSpellChanged();
                        // if RaceServer = RC_USERHUMAN then //荤恩捞搁 刀栏肺 磷篮扒 PK捞肺 牢沥 救窃
                        // LastHiter := nil;
                    }
                }
            }
            catch
            {
                M2Share.MainOutMessage("[Exception] TCreature.Run 6");
            }
        }

        public bool CheckAttackRule2(TCreature target)
        {
            bool result;
            result = true;
            if (target == null)
            {
                return result;
            }
            if (InSafeZone() || target.InSafeZone())
            {
                result = false;
            }
            if (!target.BoInFreePKArea)
            {
                // 傍己傈 瘤开俊辑绰 力寇 等促.
                if ((PKLevel() >= 2) && (Abil.Level > 10))
                {
                    // 绊乏 弧盎捞甸
                    if ((target.Abil.Level <= 10) && (target.PKLevel() < 2))
                    {
                        // 历肪 馒茄 檬焊甫 傍拜 给茄促.
                        result = false;
                    }
                }
                if ((Abil.Level <= 10) && (PKLevel() < 2))
                {
                    if ((target.PKLevel() >= 2) && (target.Abil.Level > 10))
                    {
                        result = false;
                    }
                }
            }
            if ((HUtil32.GetTickCount() - MapMoveTime < 3000) || (HUtil32.GetTickCount() - target.MapMoveTime < 3000))
            {
                result = false;
            }
            return result;
        }

        public bool _IsProperTarget_GetNonPKServerRule(TCreature target, bool rslt)
        {
            bool result = rslt;
            if (target.RaceServer == Grobal2.RC_USERHUMAN)
            {
                if ((!PEnvir.FightZone) && (!PEnvir.Fight2Zone) && (!PEnvir.Fight3Zone) && (!PEnvir.Fight4Zone) && (target.RaceServer == Grobal2.RC_USERHUMAN))
                {
                    result = false;
                }
                if (M2Share.UserCastle.BoCastleUnderAttack)
                {
                    if (BoInFreePKArea || M2Share.UserCastle.IsCastleWarArea(PEnvir, CX, CY))
                    {
                        result = true;
                    }
                }
                if ((MyGuild != null) && (target.MyGuild != null))
                {
                    if (GetGuildRelation(this, target) == 2)
                    {
                        // 巩傈(巩颇傈)吝烙
                        result = true;
                    }
                }
            }
            return result;
        }

        public bool _IsProperTarget(TCreature target)
        {
            bool result;
            result = false;
            if (target == null)
            {
                return result;
            }
            if (target == this)
            {
                return result;
            }
            if (RaceServer >= Grobal2.RC_ANIMAL)
            {
                // 磊脚捞 悼拱
                if (Master != null)
                {
                    // 林牢捞 乐绰 各
                    // if (target.RaceServer >= RC_ANIMAL) and (target.Master = nil) then
                    // Result := TRUE;
                    if ((Master.LastHiter == target) || (Master.ExpHiter == target) || (Master.TargetCret == target))
                    {
                        result = true;
                    }
                    if (target.TargetCret != null)
                    {
                        // 林牢阑 傍拜
                        // 悼丰甫 傍拜, 荤恩牢版快 力寇
                        if ((target.TargetCret == Master) || (target.TargetCret.Master == Master) && (target.RaceServer != 0))
                        {
                            result = true;
                        }
                    }
                    if ((target.TargetCret == this) && (target.RaceServer >= Grobal2.RC_ANIMAL))
                    {
                        // 各捞搁辑 磊脚阑 傍拜窍绰 磊
                        result = true;
                    }
                    if (target.Master != null)
                    {
                        // 惑措啊 家券各
                        if ((target.Master == Master.LastHiter) || (target.Master == Master.TargetCret))
                        {
                            result = true;
                        }
                    }
                    if (target.Master == Master)
                    {
                        result = false;
                    }
                    // 林牢捞 鞍栏搁 傍拜救窃
                    if (target.BoHolySeize)
                    {
                        result = false;
                    }
                    // 搬拌俊 吧妨 乐栏搁 傍拜救窃
                    if (Master.BoSlaveRelax)
                    {
                        result = false;
                    }
                    if (target.BoGoodCrazyMode)
                    {
                        result = false;
                    }
                    // 惑措啊 蚌霸固模惑怕搁 傍拜救窃(sonmg)
                    if (target.RaceServer == Grobal2.RC_USERHUMAN)
                    {
                        // 惑措啊 荤恩牢 版快
                        // 惑措 肚绰 磊脚捞 救傈瘤措俊 乐绰 版快 sonmg(2004/10/04)
                        if (InSafeZone() || target.InSafeZone())
                        {
                            // 惑措啊 救傈瘤措俊 乐绰 版快
                            result = false;
                        }
                    }
                    // 鸥百苞 甘捞 促福搁 傍拜且 荐 绝促.(sonmg 2005/01/21 -> 2005/03/31犁荐沥)
                    if (MapName != target.MapName)
                    {
                        result = false;
                    }
                    BreakCrazyMode();
                    // 林牢乐绰 各..
                }
                else
                {
                    // 老馆 各
                    if (target.RaceServer == Grobal2.RC_USERHUMAN)
                    {
                        result = true;
                    }
                    if ((RaceServer > Grobal2.RC_PEACENPC) && (RaceServer < Grobal2.RC_ANIMAL))
                    {
                        // 傍拜仿阑 啊柳 NPC绰 酒公唱 傍拜茄促.
                        result = true;
                    }
                    if (target.Master != null)
                    {
                        result = true;
                    }
                }
                if (BoCrazyMode)
                {
                    // 固魔, 酒公唱 傍拜, 利 救啊覆... (家券各俊霸档 烹茄促.)
                    result = true;
                }
                if (BoGoodCrazyMode)
                {
                    // 蚌霸固模惑怕, 荤恩苞 家券各篮 傍拜 救窍绊 促弗 阁胶磐父 傍拜茄促.
                    if ((target.RaceServer == Grobal2.RC_USERHUMAN) || (target.Master != null))
                    {
                        result = false;
                    }
                    else
                    {
                        result = true;
                    }
                }
            }
            else
            {
                // npc趣篮 荤恩牢版快
                if (RaceServer == Grobal2.RC_USERHUMAN)
                {
                    switch (HumAttackMode)
                    {
                        case Grobal2.HAM_ALL:
                            if (!((target.RaceServer >= Grobal2.RC_NPC) && (target.RaceServer <= Grobal2.RC_PEACENPC)))
                            {
                                result = true;
                            }
                            if (M2Share.BoNonPKServer)
                            {
                                result = _IsProperTarget_GetNonPKServerRule(target, result);
                            }
                            break;
                        case Grobal2.HAM_PEACE:
                            if (target.RaceServer >= Grobal2.RC_ANIMAL)
                            {
                                result = true;
                            }
                            break;
                        case Grobal2.HAM_GROUP:
                            if (!((target.RaceServer >= Grobal2.RC_NPC) && (target.RaceServer <= Grobal2.RC_PEACENPC)))
                            {
                                result = true;
                            }
                            if (target.RaceServer == Grobal2.RC_USERHUMAN)
                            {
                                if (IsGroupMember(target))
                                {
                                    result = false;
                                }
                            }
                            if (M2Share.BoNonPKServer)
                            {
                                result = _IsProperTarget_GetNonPKServerRule(target, result);
                            }
                            break;
                        case Grobal2.HAM_GUILD:
                            if (!((target.RaceServer >= Grobal2.RC_NPC) && (target.RaceServer <= Grobal2.RC_PEACENPC)))
                            {
                                result = true;
                            }
                            if (target.RaceServer == Grobal2.RC_USERHUMAN)
                            {
                                if (MyGuild != null)
                                {
                                    if (MyGuild.IsMember(target.UserName))
                                    {
                                        result = false;
                                    }
                                    if (BoGuildWarArea && (target.MyGuild != null))
                                    {
                                        // 巩颇傈,傍己傈 瘤开俊 乐澜
                                        if (MyGuild.IsAllyGuild(target.MyGuild))
                                        {
                                            result = false;
                                        }
                                    }
                                }
                            }
                            if (M2Share.BoNonPKServer)
                            {
                                result = _IsProperTarget_GetNonPKServerRule(target, result);
                            }
                            break;
                        case Grobal2.HAM_PKATTACK:
                            if (!((target.RaceServer >= Grobal2.RC_NPC) && (target.RaceServer <= Grobal2.RC_PEACENPC)))
                            {
                                result = true;
                            }
                            if (target.RaceServer == Grobal2.RC_USERHUMAN)
                            {
                                if (this.PKLevel() >= 2)
                                {
                                    // 傍拜窍绰 磊啊 弧盎捞
                                    if (target.PKLevel() < 2)
                                    {
                                        result = true;
                                    }
                                    else
                                    {
                                        result = false;
                                    }
                                }
                                else
                                {
                                    // 傍拜窍绰 磊啊 闰嫡捞
                                    if (target.PKLevel() >= 2)
                                    {
                                        result = true;
                                    }
                                    else
                                    {
                                        result = false;
                                    }
                                }
                            }
                            if (M2Share.BoNonPKServer)
                            {
                                result = _IsProperTarget_GetNonPKServerRule(target, result);
                            }
                            break;
                    }
                }
                else
                {
                    result = true;
                }
            }
            if (target.BoSysopMode || target.BoStoneMode || target.HideMode)
            {
                result = false;
            }
            return result;
        }

        public virtual bool IsProperTarget(TCreature target)
        {
            bool result = false;
            if (target == null)
            {
                return result;
            }
            result = _IsProperTarget(target);
            if (result)
            {
                if ((RaceServer == Grobal2.RC_USERHUMAN) && (target.RaceServer == Grobal2.RC_USERHUMAN))
                {
                    result = CheckAttackRule2(target);
                    // 瘤开 蝶扼辑 PK 咯何
                    if (target.BoTaiwanEventUser)
                    {
                        // 捞亥飘 酒捞袍阑 啊瘤绊 乐绰 蜡历绰 傍拜捞 凳
                        result = true;
                    }
                }
            }
            if ((target != null) && (RaceServer == Grobal2.RC_USERHUMAN))
            {
                // 唱绰 荤恩
                if ((target.Master != null) && (target.RaceServer != Grobal2.RC_USERHUMAN))
                {
                    // 林牢捞 乐绰 阁胶磐
                    // 措惑捞 阁胶磐
                    if (target.Master == this)
                    {
                        // 郴 何窍
                        if (HumAttackMode != Grobal2.HAM_ALL)
                        {
                            // 葛滴 傍拜 老锭父 何窍啊 傍拜凳
                            result = false;
                        }
                    }
                    else
                    {
                        // 促弗 捞狼 何窍
                        result = _IsProperTarget(target.Master);
                        if (InSafeZone() || target.InSafeZone())
                        {
                            result = false;
                        }
                    }
                }
            }
            return result;
        }

        public bool IsProperFriend_IsFriend(TCreature cret)
        {
            bool result;
            result = false;
            if (cret.RaceServer == Grobal2.RC_USERHUMAN)
            {
                switch (HumAttackMode)
                {
                    case Grobal2.HAM_ALL:
                        // 措惑捞 荤恩牢 版快父
                        // 傍拜屈怕 汲沥俊 蝶扼 促抚
                        result = true;
                        break;
                    case Grobal2.HAM_PEACE:
                        result = true;
                        break;
                    case Grobal2.HAM_GROUP:
                        if (cret == this)
                        {
                            result = true;
                        }
                        if (IsGroupMember(cret))
                        {
                            result = true;
                        }
                        break;
                    case Grobal2.HAM_GUILD:
                        if (cret == this)
                        {
                            result = true;
                        }
                        if (MyGuild != null)
                        {
                            if (MyGuild.IsMember(cret.UserName))
                            {
                                result = true;
                            }
                            if (BoGuildWarArea && (cret.MyGuild != null))
                            {
                                // 巩颇傈,傍己傈 瘤开俊 乐澜
                                if (MyGuild.IsAllyGuild(cret.MyGuild))
                                {
                                    result = true;
                                }
                            }
                        }
                        break;
                    case Grobal2.HAM_PKATTACK:
                        if (cret == this)
                        {
                            result = true;
                        }
                        if (PKLevel() >= 2)
                        {
                            // 郴啊 弧盎捞
                            if (cret.PKLevel() >= 2)
                            {
                                result = true;
                            }
                        }
                        else
                        {
                            // 郴啊 闰嫡捞
                            if (cret.PKLevel() < 2)
                            {
                                result = true;
                            }
                        }
                        break;
                }
            }
            return result;
        }

        public bool IsProperFriend(TCreature target)
        {
            bool result;
            result = false;
            if (target == null)
            {
                return result;
            }
            if (RaceServer >= Grobal2.RC_ANIMAL)
            {
                // 磊脚捞 悼拱
                if (target.RaceServer >= Grobal2.RC_ANIMAL)
                {
                    result = true;
                }
                if (target.Master != null)
                {
                    // 家券各篮 鳃,殿捞 救等促.
                    result = false;
                }
            }
            else
            {
                // npc趣篮 荤恩牢版快
                if (RaceServer == Grobal2.RC_USERHUMAN)
                {
                    // 傍拜屈怕 汲沥俊 蝶扼 促抚
                    result = IsProperFriend_IsFriend(target);
                    if (target.RaceServer >= Grobal2.RC_ANIMAL)
                    {
                        if (target.Master == this)
                        {
                            // 磊扁 何窍牢 版快.
                            result = true;
                        }
                        else if (target.Master != null)
                        {
                            result = IsProperFriend_IsFriend(target.Master);
                        }
                    }
                }
                else
                {
                    result = true;
                }
            }
            return result;
        }

        public void SelectTarget(TCreature target)
        {
            TargetCret = target;
            TargetFocusTime = HUtil32.GetTickCount();
        }

        public virtual void LoseTarget()
        {
            TargetCret = null;
        }

        public short GetPurity()
        {
            short result;
            if (RaceServer == Grobal2.RC_USERHUMAN)
            {
                if (UseItems[Grobal2.U_WEAPON].Dura == 0)
                {
                    result = (short)(1000 + new System.Random(5000).Next());
                }
                else
                {
                    result = (short)(3000 + new System.Random(13000).Next());
                    if (new System.Random(20).Next() == 0)
                    {
                        result = (short)(result + new System.Random(10000).Next());
                    }
                }
            }
            else
            {
                result = (short)(3000 + new System.Random(11000).Next());
                if (new System.Random(20).Next() == 0)
                {
                    result = (short)(result + new System.Random(10000).Next());
                }
            }
            return result;
        }

        // 急拱惑磊
        // 急拱惑磊
        // 函版(2005 农府胶付胶 捞亥飘)
        public void GetGiftFromBox()
        {
            TUserItem pi;
            TUserHuman hum;
            if (RaceServer != Grobal2.RC_USERHUMAN)
            {
                return;
            }
            hum = this as TUserHuman;
            if (hum == null)
            {
                return;
            }
            if (ItemList.Count < Grobal2.MAXBAGITEM)
            {
                switch (new System.Random(300000).Next())
                {
                    case 1:
                        pi = new TUserItem();
                        if (M2Share.UserEngine.CopyToUserItemFromName("勇敢之球", ref pi))
                        {
                            ItemList.Add(pi);
                            WeightChanged();
                            hum.SendAddItem(pi);
                        }
                        else
                        {
                            Dispose(pi);
                        }
                        break;
                    case 2:
                        pi = new TUserItem();
                        if (M2Share.UserEngine.CopyToUserItemFromName("恶魔之球", ref pi))
                        {
                            ItemList.Add(pi);
                            WeightChanged();
                            hum.SendAddItem(pi);
                        }
                        else
                        {
                            Dispose(pi);
                        }
                        break;
                    case 3:
                        pi = new TUserItem();
                        if (M2Share.UserEngine.CopyToUserItemFromName("神圣之球", ref pi))
                        {
                            ItemList.Add(pi);
                            WeightChanged();
                            hum.SendAddItem(pi);
                        }
                        else
                        {
                            Dispose(pi);
                        }
                        break;
                    // Modify the A .. B: 4 .. 7
                    case 4:
                        pi = new TUserItem();
                        if (M2Share.UserEngine.CopyToUserItemFromName("强风宝石", ref pi))
                        {
                            ItemList.Add(pi);
                            WeightChanged();
                            hum.SendAddItem(pi);
                        }
                        else
                        {
                            Dispose(pi);
                        }
                        break;
                    // Modify the A .. B: 8 .. 12
                    case 8:
                        pi = new TUserItem();
                        if (M2Share.UserEngine.CopyToUserItemFromName("闪避宝石", ref pi))
                        {
                            ItemList.Add(pi);
                            WeightChanged();
                            hum.SendAddItem(pi);
                        }
                        else
                        {
                            Dispose(pi);
                        }
                        break;
                    // Modify the A .. B: 13 .. 17
                    case 13:
                        pi = new TUserItem();
                        if (M2Share.UserEngine.CopyToUserItemFromName("敏捷宝石", ref pi))
                        {
                            ItemList.Add(pi);
                            WeightChanged();
                            hum.SendAddItem(pi);
                        }
                        else
                        {
                            Dispose(pi);
                        }
                        break;
                    // Modify the A .. B: 18 .. 22
                    case 18:
                        pi = new TUserItem();
                        if (M2Share.UserEngine.CopyToUserItemFromName("毒物宝石", ref pi))
                        {
                            ItemList.Add(pi);
                            WeightChanged();
                            hum.SendAddItem(pi);
                        }
                        else
                        {
                            Dispose(pi);
                        }
                        break;
                    // Modify the A .. B: 23 .. 27
                    case 23:
                        pi = new TUserItem();
                        if (M2Share.UserEngine.CopyToUserItemFromName("寒冷宝石", ref pi))
                        {
                            ItemList.Add(pi);
                            WeightChanged();
                            hum.SendAddItem(pi);
                        }
                        else
                        {
                            Dispose(pi);
                        }
                        break;
                    // Modify the A .. B: 28 .. 32
                    case 28:
                        pi = new TUserItem();
                        if (M2Share.UserEngine.CopyToUserItemFromName("觉醒宝石", ref pi))
                        {
                            ItemList.Add(pi);
                            WeightChanged();
                            hum.SendAddItem(pi);
                        }
                        else
                        {
                            Dispose(pi);
                        }
                        break;
                    // Modify the A .. B: 33 .. 37
                    case 33:
                        pi = new TUserItem();
                        if (M2Share.UserEngine.CopyToUserItemFromName("持久宝石", ref pi))
                        {
                            ItemList.Add(pi);
                            WeightChanged();
                            hum.SendAddItem(pi);
                        }
                        else
                        {
                            Dispose(pi);
                        }
                        break;
                    // Modify the A .. B: 38 .. 46
                    case 38:
                        pi = new TUserItem();
                        if (M2Share.UserEngine.CopyToUserItemFromName("勇敢宝石", ref pi))
                        {
                            ItemList.Add(pi);
                            WeightChanged();
                            hum.SendAddItem(pi);
                        }
                        else
                        {
                            Dispose(pi);
                        }
                        break;
                    // Modify the A .. B: 47 .. 56
                    case 47:
                        pi = new TUserItem();
                        if (M2Share.UserEngine.CopyToUserItemFromName("恶魔宝石", ref pi))
                        {
                            ItemList.Add(pi);
                            WeightChanged();
                            hum.SendAddItem(pi);
                        }
                        else
                        {
                            Dispose(pi);
                        }
                        break;
                    // Modify the A .. B: 57 .. 66
                    case 57:
                        pi = new TUserItem();
                        if (M2Share.UserEngine.CopyToUserItemFromName("神圣宝石", ref pi))
                        {
                            ItemList.Add(pi);
                            WeightChanged();
                            hum.SendAddItem(pi);
                        }
                        else
                        {
                            Dispose(pi);
                        }
                        break;
                    // Modify the A .. B: 67 .. 76
                    case 67:
                        pi = new TUserItem();
                        if (M2Share.UserEngine.CopyToUserItemFromName("保护宝石", ref pi))
                        {
                            ItemList.Add(pi);
                            WeightChanged();
                            hum.SendAddItem(pi);
                        }
                        else
                        {
                            Dispose(pi);
                        }
                        break;
                    // Modify the A .. B: 77 .. 86
                    case 77:
                        pi = new TUserItem();
                        if (M2Share.UserEngine.CopyToUserItemFromName("驱魔宝石", ref pi))
                        {
                            ItemList.Add(pi);
                            WeightChanged();
                            hum.SendAddItem(pi);
                        }
                        else
                        {
                            Dispose(pi);
                        }
                        break;
                    // Modify the A .. B: 87 .. 96
                    case 87:
                        pi = new TUserItem();
                        if (M2Share.UserEngine.CopyToUserItemFromName("坚硬宝石", ref pi))
                        {
                            ItemList.Add(pi);
                            WeightChanged();
                            hum.SendAddItem(pi);
                        }
                        else
                        {
                            Dispose(pi);
                        }
                        break;
                    // Modify the A .. B: 97 .. 6095
                    case 97:
                        pi = new TUserItem();
                        if (M2Share.UserEngine.CopyToUserItemFromName("祝福油", ref pi))
                        {
                            ItemList.Add(pi);
                            WeightChanged();
                            hum.SendAddItem(pi);
                        }
                        else
                        {
                            Dispose(pi);
                        }
                        break;
                    // Modify the A .. B: 6096 .. 10594
                    case 6096:
                        pi = new TUserItem();
                        if (M2Share.UserEngine.CopyToUserItemFromName("白金", ref pi))
                        {
                            ItemList.Add(pi);
                            WeightChanged();
                            hum.SendAddItem(pi);
                        }
                        else
                        {
                            Dispose(pi);
                        }
                        break;
                    // Modify the A .. B: 10595 .. 15094
                    case 10595:
                        pi = new TUserItem();
                        if (M2Share.UserEngine.CopyToUserItemFromName("金矿", ref pi))
                        {
                            ItemList.Add(pi);
                            WeightChanged();
                            hum.SendAddItem(pi);
                        }
                        else
                        {
                            Dispose(pi);
                        }
                        break;
                    // Modify the A .. B: 15095 .. 18094
                    case 15095:
                        pi = new TUserItem();
                        if (M2Share.UserEngine.CopyToUserItemFromName("生命力石(大)", ref pi))
                        {
                            ItemList.Add(pi);
                            WeightChanged();
                            hum.SendAddItem(pi);
                        }
                        else
                        {
                            Dispose(pi);
                        }
                        break;
                    // Modify the A .. B: 18095 .. 21094
                    case 18095:
                        pi = new TUserItem();
                        if (M2Share.UserEngine.CopyToUserItemFromName("魔法力石(大)", ref pi))
                        {
                            ItemList.Add(pi);
                            WeightChanged();
                            hum.SendAddItem(pi);
                        }
                        else
                        {
                            Dispose(pi);
                        }
                        break;
                    // Modify the A .. B: 21095 .. 24094
                    case 21095:
                        pi = new TUserItem();
                        if (M2Share.UserEngine.CopyToUserItemFromName("能量石(大)", ref pi))
                        {
                            ItemList.Add(pi);
                            WeightChanged();
                            hum.SendAddItem(pi);
                        }
                        else
                        {
                            Dispose(pi);
                        }
                        break;
                    // Modify the A .. B: 24095 .. 27094
                    case 24095:
                        pi = new TUserItem();
                        if (M2Share.UserEngine.CopyToUserItemFromName("攻击石(大)", ref pi))
                        {
                            ItemList.Add(pi);
                            WeightChanged();
                            hum.SendAddItem(pi);
                        }
                        else
                        {
                            Dispose(pi);
                        }
                        break;
                    // Modify the A .. B: 27095 .. 30094
                    case 27095:
                        pi = new TUserItem();
                        if (M2Share.UserEngine.CopyToUserItemFromName("魔法石(大)", ref pi))
                        {
                            ItemList.Add(pi);
                            WeightChanged();
                            hum.SendAddItem(pi);
                        }
                        else
                        {
                            Dispose(pi);
                        }
                        break;
                    // Modify the A .. B: 30095 .. 33094
                    case 30095:
                        pi = new TUserItem();
                        if (M2Share.UserEngine.CopyToUserItemFromName("精神石(大)", ref pi))
                        {
                            ItemList.Add(pi);
                            WeightChanged();
                            hum.SendAddItem(pi);
                        }
                        else
                        {
                            Dispose(pi);
                        }
                        break;
                    // Modify the A .. B: 33095 .. 37593
                    case 33095:
                        pi = new TUserItem();
                        if (M2Share.UserEngine.CopyToUserItemFromName("阎罗手套", ref pi))
                        {
                            ItemList.Add(pi);
                            WeightChanged();
                            hum.SendAddItem(pi);
                        }
                        else
                        {
                            Dispose(pi);
                        }
                        break;
                    // Modify the A .. B: 37594 .. 41313
                    case 37594:
                        pi = new TUserItem();
                        if (M2Share.UserEngine.CopyToUserItemFromName("青铜手套", ref pi))
                        {
                            ItemList.Add(pi);
                            WeightChanged();
                            hum.SendAddItem(pi);
                        }
                        else
                        {
                            Dispose(pi);
                        }
                        break;
                    // Modify the A .. B: 41314 .. 45093
                    case 41314:
                        pi = new TUserItem();
                        if (M2Share.UserEngine.CopyToUserItemFromName("魔法手镯", ref pi))
                        {
                            ItemList.Add(pi);
                            WeightChanged();
                            hum.SendAddItem(pi);
                        }
                        else
                        {
                            Dispose(pi);
                        }
                        break;
                    // Modify the A .. B: 45094 .. 48843
                    case 45094:
                        pi = new TUserItem();
                        if (M2Share.UserEngine.CopyToUserItemFromName("黑铁腰带", ref pi))
                        {
                            ItemList.Add(pi);
                            WeightChanged();
                            hum.SendAddItem(pi);
                        }
                        else
                        {
                            Dispose(pi);
                        }
                        break;
                    // Modify the A .. B: 48844 .. 52593
                    case 48844:
                        pi = new TUserItem();
                        if (M2Share.UserEngine.CopyToUserItemFromName("道德戒指", ref pi))
                        {
                            ItemList.Add(pi);
                            WeightChanged();
                            hum.SendAddItem(pi);
                        }
                        else
                        {
                            Dispose(pi);
                        }
                        break;
                    // Modify the A .. B: 52594 .. 56343
                    case 52594:
                        pi = new TUserItem();
                        if (M2Share.UserEngine.CopyToUserItemFromName("魅力戒指", ref pi))
                        {
                            ItemList.Add(pi);
                            WeightChanged();
                            hum.SendAddItem(pi);
                        }
                        else
                        {
                            Dispose(pi);
                        }
                        break;
                    // Modify the A .. B: 56344 .. 60093
                    case 56344:
                        pi = new TUserItem();
                        if (M2Share.UserEngine.CopyToUserItemFromName("珊瑚戒指", ref pi))
                        {
                            ItemList.Add(pi);
                            WeightChanged();
                            hum.SendAddItem(pi);
                        }
                        else
                        {
                            Dispose(pi);
                        }
                        break;
                    // Modify the A .. B: 60094 .. 63843
                    case 60094:
                        pi = new TUserItem();
                        if (M2Share.UserEngine.CopyToUserItemFromName("放逐戒指", ref pi))
                        {
                            ItemList.Add(pi);
                            WeightChanged();
                            hum.SendAddItem(pi);
                        }
                        else
                        {
                            Dispose(pi);
                        }
                        break;
                    // Modify the A .. B: 63844 .. 67593
                    case 63844:
                        pi = new TUserItem();
                        if (M2Share.UserEngine.CopyToUserItemFromName("蓝翡翠项链", ref pi))
                        {
                            ItemList.Add(pi);
                            WeightChanged();
                            hum.SendAddItem(pi);
                        }
                        else
                        {
                            Dispose(pi);
                        }
                        break;
                    // Modify the A .. B: 67594 .. 71343
                    case 67594:
                        pi = new TUserItem();
                        if (M2Share.UserEngine.CopyToUserItemFromName("放大镜", ref pi))
                        {
                            ItemList.Add(pi);
                            WeightChanged();
                            hum.SendAddItem(pi);
                        }
                        else
                        {
                            Dispose(pi);
                        }
                        break;
                    // Modify the A .. B: 71344 .. 75093
                    case 71344:
                        pi = new TUserItem();
                        if (M2Share.UserEngine.CopyToUserItemFromName("竹笛", ref pi))
                        {
                            ItemList.Add(pi);
                            WeightChanged();
                            hum.SendAddItem(pi);
                        }
                        else
                        {
                            Dispose(pi);
                        }
                        break;
                    // Modify the A .. B: 75094 .. 88593
                    case 75094:
                        pi = new TUserItem();
                        if (M2Share.UserEngine.CopyToUserItemFromName("特级金创药", ref pi))
                        {
                            ItemList.Add(pi);
                            WeightChanged();
                            hum.SendAddItem(pi);
                        }
                        else
                        {
                            Dispose(pi);
                        }
                        break;
                    // Modify the A .. B: 88594 .. 102093
                    case 88594:
                        pi = new TUserItem();
                        if (M2Share.UserEngine.CopyToUserItemFromName("特级魔法药", ref pi))
                        {
                            ItemList.Add(pi);
                            WeightChanged();
                            hum.SendAddItem(pi);
                        }
                        else
                        {
                            Dispose(pi);
                        }
                        break;
                    // Modify the A .. B: 102094 .. 120093
                    case 102094:
                        pi = new TUserItem();
                        if (M2Share.UserEngine.CopyToUserItemFromName("绳子", ref pi))
                        {
                            ItemList.Add(pi);
                            WeightChanged();
                            hum.SendAddItem(pi);
                        }
                        else
                        {
                            Dispose(pi);
                        }
                        break;
                    // Modify the A .. B: 120094 .. 142593
                    case 120094:
                        pi = new TUserItem();
                        if (M2Share.UserEngine.CopyToUserItemFromName("超级金创药", ref pi))
                        {
                            ItemList.Add(pi);
                            WeightChanged();
                            hum.SendAddItem(pi);
                        }
                        else
                        {
                            Dispose(pi);
                        }
                        break;
                    // Modify the A .. B: 142594 .. 165093
                    case 142594:
                        pi = new TUserItem();
                        if (M2Share.UserEngine.CopyToUserItemFromName("超级魔法药", ref pi))
                        {
                            ItemList.Add(pi);
                            WeightChanged();
                            hum.SendAddItem(pi);
                        }
                        else
                        {
                            Dispose(pi);
                        }
                        break;
                    // Modify the A .. B: 165094 .. 171843
                    case 165094:
                        pi = new TUserItem();
                        if (M2Share.UserEngine.CopyToUserItemFromName("战神油", ref pi))
                        {
                            ItemList.Add(pi);
                            WeightChanged();
                            hum.SendAddItem(pi);
                        }
                        else
                        {
                            Dispose(pi);
                        }
                        break;
                    // Modify the A .. B: 171844 .. 181968
                    case 171844:
                        pi = new TUserItem();
                        if (M2Share.UserEngine.CopyToUserItemFromName("金创药(特大)", ref pi))
                        {
                            ItemList.Add(pi);
                            WeightChanged();
                            hum.SendAddItem(pi);
                        }
                        else
                        {
                            Dispose(pi);
                        }
                        break;
                    // Modify the A .. B: 181969 .. 192093
                    case 181969:
                        pi = new TUserItem();
                        if (M2Share.UserEngine.CopyToUserItemFromName("魔法药(特大)", ref pi))
                        {
                            ItemList.Add(pi);
                            WeightChanged();
                            hum.SendAddItem(pi);
                        }
                        else
                        {
                            Dispose(pi);
                        }
                        break;
                    // Modify the A .. B: 192094 .. 205593
                    case 192094:
                        pi = new TUserItem();
                        if (M2Share.UserEngine.CopyToUserItemFromName("金创药(大)", ref pi))
                        {
                            ItemList.Add(pi);
                            WeightChanged();
                            hum.SendAddItem(pi);
                        }
                        else
                        {
                            Dispose(pi);
                        }
                        break;
                    // Modify the A .. B: 205594 .. 219093
                    case 205594:
                        pi = new TUserItem();
                        if (M2Share.UserEngine.CopyToUserItemFromName("魔法药(大)", ref pi))
                        {
                            ItemList.Add(pi);
                            WeightChanged();
                            hum.SendAddItem(pi);
                        }
                        else
                        {
                            Dispose(pi);
                        }
                        break;
                    // Modify the A .. B: 219094 .. 246093
                    case 219094:
                        pi = new TUserItem();
                        if (M2Share.UserEngine.CopyToUserItemFromName("太阳水(中)", ref pi))
                        {
                            ItemList.Add(pi);
                            WeightChanged();
                            hum.SendAddItem(pi);
                        }
                        else
                        {
                            Dispose(pi);
                        }
                        break;
                    // Modify the A .. B: 246094 .. 249093
                    case 246094:
                        pi = new TUserItem();
                        if (M2Share.UserEngine.CopyToUserItemFromName("彩票", ref pi))
                        {
                            ItemList.Add(pi);
                            WeightChanged();
                            hum.SendAddItem(pi);
                        }
                        else
                        {
                            Dispose(pi);
                        }
                        break;
                    default:
                        // 唱赣瘤
                        pi = new TUserItem();
                        if (M2Share.UserEngine.CopyToUserItemFromName("太阳水", ref pi))
                        {
                            ItemList.Add(pi);
                            WeightChanged();
                            hum.SendAddItem(pi);
                        }
                        else
                        {
                            Dispose(pi);
                        }
                        break;
                }
            }
        }

        // 何劝例 崔翱
        // 何劝例 崔翱
        public void GetGiftFromEasterEgg()
        {
            TUserItem pi;
            TUserHuman hum;
            if (RaceServer != Grobal2.RC_USERHUMAN)
            {
                return;
            }
            hum = this as TUserHuman;
            if (hum == null)
            {
                return;
            }
            if (ItemList.Count < Grobal2.MAXBAGITEM)
            {
                switch (new System.Random(300000).Next())
                {
                    // Modify the A .. B: 1 .. 2
                    case 1:
                        pi = new TUserItem();
                        if (M2Share.UserEngine.CopyToUserItemFromName("DragonSlayer", ref pi))
                        {
                            ItemList.Add(pi);
                            WeightChanged();
                            hum.SendAddItem(pi);
                        }
                        else
                        {
                            Dispose(pi);
                        }
                        break;
                    // Modify the A .. B: 3 .. 4
                    case 3:
                        pi = new TUserItem();
                        if (M2Share.UserEngine.CopyToUserItemFromName("SoulSabre", ref pi))
                        {
                            ItemList.Add(pi);
                            WeightChanged();
                            hum.SendAddItem(pi);
                        }
                        else
                        {
                            Dispose(pi);
                        }
                        break;
                    // Modify the A .. B: 5 .. 6
                    case 5:
                        pi = new TUserItem();
                        if (M2Share.UserEngine.CopyToUserItemFromName("DragonStaff", ref pi))
                        {
                            ItemList.Add(pi);
                            WeightChanged();
                            hum.SendAddItem(pi);
                        }
                        else
                        {
                            Dispose(pi);
                        }
                        break;
                    // Modify the A .. B: 7 .. 31
                    case 7:
                        pi = new TUserItem();
                        if (M2Share.UserEngine.CopyToUserItemFromName("Mirroring", ref pi))
                        {
                            ItemList.Add(pi);
                            WeightChanged();
                            hum.SendAddItem(pi);
                        }
                        else
                        {
                            Dispose(pi);
                        }
                        break;
                    // Modify the A .. B: 32 .. 56
                    case 32:
                        pi = new TUserItem();
                        if (M2Share.UserEngine.CopyToUserItemFromName("FlameField", ref pi))
                        {
                            ItemList.Add(pi);
                            WeightChanged();
                            hum.SendAddItem(pi);
                        }
                        else
                        {
                            Dispose(pi);
                        }
                        break;
                    // Modify the A .. B: 57 .. 81
                    case 57:
                        pi = new TUserItem();
                        if (M2Share.UserEngine.CopyToUserItemFromName("Curse", ref pi))
                        {
                            ItemList.Add(pi);
                            WeightChanged();
                            hum.SendAddItem(pi);
                        }
                        else
                        {
                            Dispose(pi);
                        }
                        break;
                    // Modify the A .. B: 82 .. 106
                    case 82:
                        pi = new TUserItem();
                        if (M2Share.UserEngine.CopyToUserItemFromName("BladeAvalanche", ref pi))
                        {
                            ItemList.Add(pi);
                            WeightChanged();
                            hum.SendAddItem(pi);
                        }
                        else
                        {
                            Dispose(pi);
                        }
                        break;
                    // Modify the A .. B: 107 .. 131
                    case 107:
                        pi = new TUserItem();
                        if (M2Share.UserEngine.CopyToUserItemFromName("LionRoar", ref pi))
                        {
                            ItemList.Add(pi);
                            WeightChanged();
                            hum.SendAddItem(pi);
                        }
                        else
                        {
                            Dispose(pi);
                        }
                        break;
                    // Modify the A .. B: 132 .. 156
                    case 132:
                        pi = new TUserItem();
                        if (M2Share.UserEngine.CopyToUserItemFromName("SummonHolyDeva", ref pi))
                        {
                            ItemList.Add(pi);
                            WeightChanged();
                            hum.SendAddItem(pi);
                        }
                        else
                        {
                            Dispose(pi);
                        }
                        break;
                    // Modify the A .. B: 157 .. 191
                    case 157:
                        pi = new TUserItem();
                        if (M2Share.UserEngine.CopyToUserItemFromName("Vampirism", ref pi))
                        {
                            ItemList.Add(pi);
                            WeightChanged();
                            hum.SendAddItem(pi);
                        }
                        else
                        {
                            Dispose(pi);
                        }
                        break;
                    // Modify the A .. B: 192 .. 226
                    case 192:
                        pi = new TUserItem();
                        if (M2Share.UserEngine.CopyToUserItemFromName("Entrapment", ref pi))
                        {
                            ItemList.Add(pi);
                            WeightChanged();
                            hum.SendAddItem(pi);
                        }
                        else
                        {
                            Dispose(pi);
                        }
                        break;
                    // Modify the A .. B: 227 .. 256
                    case 227:
                        pi = new TUserItem();
                        if (M2Share.UserEngine.CopyToUserItemFromName("Hallucination", ref pi))
                        {
                            ItemList.Add(pi);
                            WeightChanged();
                            hum.SendAddItem(pi);
                        }
                        else
                        {
                            Dispose(pi);
                        }
                        break;
                    // Modify the A .. B: 257 .. 306
                    case 257:
                        pi = new TUserItem();
                        if (M2Share.UserEngine.CopyToUserItemFromName("BraveryOrb", ref pi))
                        {
                            ItemList.Add(pi);
                            WeightChanged();
                            hum.SendAddItem(pi);
                        }
                        else
                        {
                            Dispose(pi);
                        }
                        break;
                    // Modify the A .. B: 307 .. 356
                    case 307:
                        pi = new TUserItem();
                        if (M2Share.UserEngine.CopyToUserItemFromName("DemonicOrb", ref pi))
                        {
                            ItemList.Add(pi);
                            WeightChanged();
                            hum.SendAddItem(pi);
                        }
                        else
                        {
                            Dispose(pi);
                        }
                        break;
                    // Modify the A .. B: 357 .. 406
                    case 357:
                        pi = new TUserItem();
                        if (M2Share.UserEngine.CopyToUserItemFromName("HolyOrb", ref pi))
                        {
                            ItemList.Add(pi);
                            WeightChanged();
                            hum.SendAddItem(pi);
                        }
                        else
                        {
                            Dispose(pi);
                        }
                        break;
                    // Modify the A .. B: 407 .. 456
                    case 407:
                        pi = new TUserItem();
                        if (M2Share.UserEngine.CopyToUserItemFromName("OmaSpiritRing", ref pi))
                        {
                            ItemList.Add(pi);
                            WeightChanged();
                            hum.SendAddItem(pi);
                        }
                        else
                        {
                            Dispose(pi);
                        }
                        break;
                    // Modify the A .. B: 457 .. 506
                    case 457:
                        pi = new TUserItem();
                        if (M2Share.UserEngine.CopyToUserItemFromName("NobleRing", ref pi))
                        {
                            ItemList.Add(pi);
                            WeightChanged();
                            hum.SendAddItem(pi);
                        }
                        else
                        {
                            Dispose(pi);
                        }
                        break;
                    // Modify the A .. B: 507 .. 556
                    case 507:
                        pi = new TUserItem();
                        if (M2Share.UserEngine.CopyToUserItemFromName("SoulRing", ref pi))
                        {
                            ItemList.Add(pi);
                            WeightChanged();
                            hum.SendAddItem(pi);
                        }
                        else
                        {
                            Dispose(pi);
                        }
                        break;
                    // Modify the A .. B: 557 .. 656
                    case 557:
                        pi = new TUserItem();
                        if (M2Share.UserEngine.CopyToUserItemFromName("HolyMCStone", ref pi))
                        {
                            ItemList.Add(pi);
                            WeightChanged();
                            hum.SendAddItem(pi);
                        }
                        else
                        {
                            Dispose(pi);
                        }
                        break;
                    // Modify the A .. B: 657 .. 756
                    case 657:
                        pi = new TUserItem();
                        if (M2Share.UserEngine.CopyToUserItemFromName("HolyDCStone)", ref pi))
                        {
                            ItemList.Add(pi);
                            WeightChanged();
                            hum.SendAddItem(pi);
                        }
                        else
                        {
                            Dispose(pi);
                        }
                        break;
                    // Modify the A .. B: 757 .. 856
                    case 757:
                        pi = new TUserItem();
                        if (M2Share.UserEngine.CopyToUserItemFromName("HolyTaoStone", ref pi))
                        {
                            ItemList.Add(pi);
                            WeightChanged();
                            hum.SendAddItem(pi);
                        }
                        else
                        {
                            Dispose(pi);
                        }
                        break;
                    // Modify the A .. B: 857 .. 956
                    case 857:
                        pi = new TUserItem();
                        if (M2Share.UserEngine.CopyToUserItemFromName("RecallNecklace", ref pi))
                        {
                            ItemList.Add(pi);
                            WeightChanged();
                            hum.SendAddItem(pi);
                        }
                        else
                        {
                            Dispose(pi);
                        }
                        break;
                    // Modify the A .. B: 957 .. 1056
                    case 957:
                        pi = new TUserItem();
                        if (M2Share.UserEngine.CopyToUserItemFromName("RecallRing", ref pi))
                        {
                            ItemList.Add(pi);
                            WeightChanged();
                            hum.SendAddItem(pi);
                        }
                        else
                        {
                            Dispose(pi);
                        }
                        break;
                    // Modify the A .. B: 1057 .. 1156
                    case 1057:
                        pi = new TUserItem();
                        if (M2Share.UserEngine.CopyToUserItemFromName("RecallHelmet", ref pi))
                        {
                            ItemList.Add(pi);
                            WeightChanged();
                            hum.SendAddItem(pi);
                        }
                        else
                        {
                            Dispose(pi);
                        }
                        break;
                    // Modify the A .. B: 1157 .. 1256
                    case 1157:
                        pi = new TUserItem();
                        if (M2Share.UserEngine.CopyToUserItemFromName("RecallBracelet", ref pi))
                        {
                            ItemList.Add(pi);
                            WeightChanged();
                            hum.SendAddItem(pi);
                        }
                        else
                        {
                            Dispose(pi);
                        }
                        break;
                    // Modify the A .. B: 1257 .. 1406
                    case 1257:
                        pi = new TUserItem();
                        if (M2Share.UserEngine.CopyToUserItemFromName("GaleGem", ref pi))
                        {
                            ItemList.Add(pi);
                            WeightChanged();
                            hum.SendAddItem(pi);
                        }
                        else
                        {
                            Dispose(pi);
                        }
                        break;
                    // Modify the A .. B: 1407 .. 1556
                    case 1407:
                        pi = new TUserItem();
                        if (M2Share.UserEngine.CopyToUserItemFromName("EvasionGem", ref pi))
                        {
                            ItemList.Add(pi);
                            WeightChanged();
                            hum.SendAddItem(pi);
                        }
                        else
                        {
                            Dispose(pi);
                        }
                        break;
                    // Modify the A .. B: 1557 .. 1706
                    case 1557:
                        pi = new TUserItem();
                        if (M2Share.UserEngine.CopyToUserItemFromName("SharpnessGem", ref pi))
                        {
                            ItemList.Add(pi);
                            WeightChanged();
                            hum.SendAddItem(pi);
                        }
                        else
                        {
                            Dispose(pi);
                        }
                        break;
                    // Modify the A .. B: 1707 .. 1856
                    case 1707:
                        pi = new TUserItem();
                        if (M2Share.UserEngine.CopyToUserItemFromName("PoisonGem", ref pi))
                        {
                            ItemList.Add(pi);
                            WeightChanged();
                            hum.SendAddItem(pi);
                        }
                        else
                        {
                            Dispose(pi);
                        }
                        break;
                    // Modify the A .. B: 1857 .. 2006
                    case 1857:
                        pi = new TUserItem();
                        if (M2Share.UserEngine.CopyToUserItemFromName("ColdnessGem", ref pi))
                        {
                            ItemList.Add(pi);
                            WeightChanged();
                            hum.SendAddItem(pi);
                        }
                        else
                        {
                            Dispose(pi);
                        }
                        break;
                    // Modify the A .. B: 2007 .. 2156
                    case 2007:
                        pi = new TUserItem();
                        if (M2Share.UserEngine.CopyToUserItemFromName("AwakeningGem", ref pi))
                        {
                            ItemList.Add(pi);
                            WeightChanged();
                            hum.SendAddItem(pi);
                        }
                        else
                        {
                            Dispose(pi);
                        }
                        break;
                    // Modify the A .. B: 2157 .. 2306
                    case 2157:
                        pi = new TUserItem();
                        if (M2Share.UserEngine.CopyToUserItemFromName("EnduranceGem", ref pi))
                        {
                            ItemList.Add(pi);
                            WeightChanged();
                            hum.SendAddItem(pi);
                        }
                        else
                        {
                            Dispose(pi);
                        }
                        break;
                    // Modify the A .. B: 2307 .. 2406
                    case 2307:
                        pi = new TUserItem();
                        if (M2Share.UserEngine.CopyToUserItemFromName("VioletRing", ref pi))
                        {
                            ItemList.Add(pi);
                            WeightChanged();
                            hum.SendAddItem(pi);
                        }
                        else
                        {
                            Dispose(pi);
                        }
                        break;
                    // Modify the A .. B: 2407 .. 2506
                    case 2407:
                        pi = new TUserItem();
                        if (M2Share.UserEngine.CopyToUserItemFromName("DragonRing", ref pi))
                        {
                            ItemList.Add(pi);
                            WeightChanged();
                            hum.SendAddItem(pi);
                        }
                        else
                        {
                            Dispose(pi);
                        }
                        break;
                    // Modify the A .. B: 2507 .. 2606
                    case 2507:
                        pi = new TUserItem();
                        if (M2Share.UserEngine.CopyToUserItemFromName("TitanRing", ref pi))
                        {
                            ItemList.Add(pi);
                            WeightChanged();
                            hum.SendAddItem(pi);
                        }
                        else
                        {
                            Dispose(pi);
                        }
                        break;
                    // Modify the A .. B: 2607 .. 2806
                    case 2607:
                        pi = new TUserItem();
                        if (M2Share.UserEngine.CopyToUserItemFromName("HeroNecklace", ref pi))
                        {
                            ItemList.Add(pi);
                            WeightChanged();
                            hum.SendAddItem(pi);
                        }
                        else
                        {
                            Dispose(pi);
                        }
                        break;
                    // Modify the A .. B: 2807 .. 3006
                    case 2807:
                        pi = new TUserItem();
                        if (M2Share.UserEngine.CopyToUserItemFromName("AdamantineNeck", ref pi))
                        {
                            ItemList.Add(pi);
                            WeightChanged();
                            hum.SendAddItem(pi);
                        }
                        else
                        {
                            Dispose(pi);
                        }
                        break;
                    // Modify the A .. B: 3007 .. 3206
                    case 3007:
                        pi = new TUserItem();
                        if (M2Share.UserEngine.CopyToUserItemFromName("RequiemNecklac", ref pi))
                        {
                            ItemList.Add(pi);
                            WeightChanged();
                            hum.SendAddItem(pi);
                        }
                        else
                        {
                            Dispose(pi);
                        }
                        break;
                    // Modify the A .. B: 3207 .. 3456
                    case 3207:
                        pi = new TUserItem();
                        if (M2Share.UserEngine.CopyToUserItemFromName("BraveryGem", ref pi))
                        {
                            ItemList.Add(pi);
                            WeightChanged();
                            hum.SendAddItem(pi);
                        }
                        else
                        {
                            Dispose(pi);
                        }
                        break;
                    // Modify the A .. B: 3457 .. 3706
                    case 3457:
                        pi = new TUserItem();
                        if (M2Share.UserEngine.CopyToUserItemFromName("DemonicGem", ref pi))
                        {
                            ItemList.Add(pi);
                            WeightChanged();
                            hum.SendAddItem(pi);
                        }
                        else
                        {
                            Dispose(pi);
                        }
                        break;
                    // Modify the A .. B: 3707 .. 3956
                    case 3707:
                        pi = new TUserItem();
                        if (M2Share.UserEngine.CopyToUserItemFromName("HolyGem", ref pi))
                        {
                            ItemList.Add(pi);
                            WeightChanged();
                            hum.SendAddItem(pi);
                        }
                        else
                        {
                            Dispose(pi);
                        }
                        break;
                    // Modify the A .. B: 3957 .. 4206
                    case 3957:
                        pi = new TUserItem();
                        if (M2Share.UserEngine.CopyToUserItemFromName("ProtectionGem", ref pi))
                        {
                            ItemList.Add(pi);
                            WeightChanged();
                            hum.SendAddItem(pi);
                        }
                        else
                        {
                            Dispose(pi);
                        }
                        break;
                    // Modify the A .. B: 4207 .. 4456
                    case 4207:
                        pi = new TUserItem();
                        if (M2Share.UserEngine.CopyToUserItemFromName("ExorcismGem", ref pi))
                        {
                            ItemList.Add(pi);
                            WeightChanged();
                            hum.SendAddItem(pi);
                        }
                        else
                        {
                            Dispose(pi);
                        }
                        break;
                    // Modify the A .. B: 4457 .. 4706
                    case 4457:
                        pi = new TUserItem();
                        if (M2Share.UserEngine.CopyToUserItemFromName("HardnessGem", ref pi))
                        {
                            ItemList.Add(pi);
                            WeightChanged();
                            hum.SendAddItem(pi);
                        }
                        else
                        {
                            Dispose(pi);
                        }
                        break;
                    // Modify the A .. B: 4707 .. 4956
                    case 4707:
                        pi = new TUserItem();
                        if (M2Share.UserEngine.CopyToUserItemFromName("BaekTaGlove", ref pi))
                        {
                            ItemList.Add(pi);
                            WeightChanged();
                            hum.SendAddItem(pi);
                        }
                        else
                        {
                            Dispose(pi);
                        }
                        break;
                    // Modify the A .. B: 4957 .. 5206
                    case 4957:
                        pi = new TUserItem();
                        if (M2Share.UserEngine.CopyToUserItemFromName("HolyTaoWheel", ref pi))
                        {
                            ItemList.Add(pi);
                            WeightChanged();
                            hum.SendAddItem(pi);
                        }
                        else
                        {
                            Dispose(pi);
                        }
                        break;
                    // Modify the A .. B: 5207 .. 5506
                    case 5207:
                        pi = new TUserItem();
                        if (M2Share.UserEngine.CopyToUserItemFromName("SpiritReformer", ref pi))
                        {
                            ItemList.Add(pi);
                            WeightChanged();
                            hum.SendAddItem(pi);
                        }
                        else
                        {
                            Dispose(pi);
                        }
                        break;
                    // Modify the A .. B: 5507 .. 13006
                    case 5507:
                        pi = new TUserItem();
                        if (M2Share.UserEngine.CopyToUserItemFromName("BenedictionOil", ref pi))
                        {
                            ItemList.Add(pi);
                            WeightChanged();
                            hum.SendAddItem(pi);
                        }
                        else
                        {
                            Dispose(pi);
                        }
                        break;
                    // Modify the A .. B: 13007 .. 13506
                    case 13007:
                        pi = new TUserItem();
                        if (M2Share.UserEngine.CopyToUserItemFromName("HealthStoneXL", ref pi))
                        {
                            ItemList.Add(pi);
                            WeightChanged();
                            hum.SendAddItem(pi);
                        }
                        else
                        {
                            Dispose(pi);
                        }
                        break;
                    // Modify the A .. B: 13507 .. 14006
                    case 13507:
                        pi = new TUserItem();
                        if (M2Share.UserEngine.CopyToUserItemFromName("MagicStoneXL", ref pi))
                        {
                            ItemList.Add(pi);
                            WeightChanged();
                            hum.SendAddItem(pi);
                        }
                        else
                        {
                            Dispose(pi);
                        }
                        break;
                    // Modify the A .. B: 14007 .. 14506
                    case 14007:
                        pi = new TUserItem();
                        if (M2Share.UserEngine.CopyToUserItemFromName("PowerStoneXL", ref pi))
                        {
                            ItemList.Add(pi);
                            WeightChanged();
                            hum.SendAddItem(pi);
                        }
                        else
                        {
                            Dispose(pi);
                        }
                        break;
                    // Modify the A .. B: 14507 .. 15006
                    case 14507:
                        pi = new TUserItem();
                        if (M2Share.UserEngine.CopyToUserItemFromName("DCStone(XL)", ref pi))
                        {
                            ItemList.Add(pi);
                            WeightChanged();
                            hum.SendAddItem(pi);
                        }
                        else
                        {
                            Dispose(pi);
                        }
                        break;
                    // Modify the A .. B: 15007 .. 15506
                    case 15007:
                        pi = new TUserItem();
                        if (M2Share.UserEngine.CopyToUserItemFromName("MCStone(XL)", ref pi))
                        {
                            ItemList.Add(pi);
                            WeightChanged();
                            hum.SendAddItem(pi);
                        }
                        else
                        {
                            Dispose(pi);
                        }
                        break;
                    // Modify the A .. B: 15507 .. 16006
                    case 15507:
                        pi = new TUserItem();
                        if (M2Share.UserEngine.CopyToUserItemFromName("TaoStone(XL)", ref pi))
                        {
                            ItemList.Add(pi);
                            WeightChanged();
                            hum.SendAddItem(pi);
                        }
                        else
                        {
                            Dispose(pi);
                        }
                        break;
                    // Modify the A .. B: 16007 .. 18756
                    case 16007:
                        pi = new TUserItem();
                        if (M2Share.UserEngine.CopyToUserItemFromName("BlackIronBrace", ref pi))
                        {
                            ItemList.Add(pi);
                            WeightChanged();
                            hum.SendAddItem(pi);
                        }
                        else
                        {
                            Dispose(pi);
                        }
                        break;
                    // Modify the A .. B: 18757 .. 21506
                    case 18757:
                        pi = new TUserItem();
                        if (M2Share.UserEngine.CopyToUserItemFromName("MoralRing", ref pi))
                        {
                            ItemList.Add(pi);
                            WeightChanged();
                            hum.SendAddItem(pi);
                        }
                        else
                        {
                            Dispose(pi);
                        }
                        break;
                    // Modify the A .. B: 21507 .. 24256
                    case 21507:
                        pi = new TUserItem();
                        if (M2Share.UserEngine.CopyToUserItemFromName("CharmRing", ref pi))
                        {
                            ItemList.Add(pi);
                            WeightChanged();
                            hum.SendAddItem(pi);
                        }
                        else
                        {
                            Dispose(pi);
                        }
                        break;
                    // Modify the A .. B: 24257 .. 27006
                    case 24257:
                        pi = new TUserItem();
                        if (M2Share.UserEngine.CopyToUserItemFromName("CoralRing", ref pi))
                        {
                            ItemList.Add(pi);
                            WeightChanged();
                            hum.SendAddItem(pi);
                        }
                        else
                        {
                            Dispose(pi);
                        }
                        break;
                    // Modify the A .. B: 27007 .. 29756
                    case 27007:
                        pi = new TUserItem();
                        if (M2Share.UserEngine.CopyToUserItemFromName("ExpelRing", ref pi))
                        {
                            ItemList.Add(pi);
                            WeightChanged();
                            hum.SendAddItem(pi);
                        }
                        else
                        {
                            Dispose(pi);
                        }
                        break;
                    // Modify the A .. B: 29757 .. 32406
                    case 29757:
                        pi = new TUserItem();
                        if (M2Share.UserEngine.CopyToUserItemFromName("BlueJadeNeckl", ref pi))
                        {
                            ItemList.Add(pi);
                            WeightChanged();
                            hum.SendAddItem(pi);
                        }
                        else
                        {
                            Dispose(pi);
                        }
                        break;
                    // Modify the A .. B: 32407 .. 35006
                    case 32407:
                        pi = new TUserItem();
                        if (M2Share.UserEngine.CopyToUserItemFromName("ConvexLens", ref pi))
                        {
                            ItemList.Add(pi);
                            WeightChanged();
                            hum.SendAddItem(pi);
                        }
                        else
                        {
                            Dispose(pi);
                        }
                        break;
                    // Modify the A .. B: 35007 .. 37756
                    case 35007:
                        pi = new TUserItem();
                        if (M2Share.UserEngine.CopyToUserItemFromName("BambooPipe", ref pi))
                        {
                            ItemList.Add(pi);
                            WeightChanged();
                            hum.SendAddItem(pi);
                        }
                        else
                        {
                            Dispose(pi);
                        }
                        break;
                    // Modify the A .. B: 37757 .. 47756
                    case 37757:
                        pi = new TUserItem();
                        if (M2Share.UserEngine.CopyToUserItemFromName("(HP)DrugBundXL", ref pi))
                        {
                            ItemList.Add(pi);
                            WeightChanged();
                            hum.SendAddItem(pi);
                        }
                        else
                        {
                            Dispose(pi);
                        }
                        break;
                    // Modify the A .. B: 47757 .. 57756
                    case 47757:
                        pi = new TUserItem();
                        if (M2Share.UserEngine.CopyToUserItemFromName("(MP)DrugBundXL", ref pi))
                        {
                            ItemList.Add(pi);
                            WeightChanged();
                            hum.SendAddItem(pi);
                        }
                        else
                        {
                            Dispose(pi);
                        }
                        break;
                    // Modify the A .. B: 57757 .. 69256
                    case 57757:
                        pi = new TUserItem();
                        if (M2Share.UserEngine.CopyToUserItemFromName("(HP)DrugBundle", ref pi))
                        {
                            ItemList.Add(pi);
                            WeightChanged();
                            hum.SendAddItem(pi);
                        }
                        else
                        {
                            Dispose(pi);
                        }
                        break;
                    // Modify the A .. B: 69257 .. 80756
                    case 69257:
                        pi = new TUserItem();
                        if (M2Share.UserEngine.CopyToUserItemFromName("(MP)DrugBundle", ref pi))
                        {
                            ItemList.Add(pi);
                            WeightChanged();
                            hum.SendAddItem(pi);
                        }
                        else
                        {
                            Dispose(pi);
                        }
                        break;
                    // Modify the A .. B: 80757 .. 87395
                    case 80757:
                        pi = new TUserItem();
                        if (M2Share.UserEngine.CopyToUserItemFromName("WarGodOil", ref pi))
                        {
                            ItemList.Add(pi);
                            WeightChanged();
                            hum.SendAddItem(pi);
                        }
                        else
                        {
                            Dispose(pi);
                        }
                        break;
                    // Modify the A .. B: 87396 .. 97520
                    case 87396:
                        pi = new TUserItem();
                        if (M2Share.UserEngine.CopyToUserItemFromName("(HP)DrugXL", ref pi))
                        {
                            ItemList.Add(pi);
                            WeightChanged();
                            hum.SendAddItem(pi);
                        }
                        else
                        {
                            Dispose(pi);
                        }
                        break;
                    // Modify the A .. B: 97521 .. 107645
                    case 97521:
                        pi = new TUserItem();
                        if (M2Share.UserEngine.CopyToUserItemFromName("(MP)DrugXL", ref pi))
                        {
                            ItemList.Add(pi);
                            WeightChanged();
                            hum.SendAddItem(pi);
                        }
                        else
                        {
                            Dispose(pi);
                        }
                        break;
                    // Modify the A .. B: 107646 .. 119145
                    case 107646:
                        pi = new TUserItem();
                        if (M2Share.UserEngine.CopyToUserItemFromName("(HP)DrugLarge", ref pi))
                        {
                            ItemList.Add(pi);
                            WeightChanged();
                            hum.SendAddItem(pi);
                        }
                        else
                        {
                            Dispose(pi);
                        }
                        break;
                    // Modify the A .. B: 119146 .. 130645
                    case 119146:
                        pi = new TUserItem();
                        if (M2Share.UserEngine.CopyToUserItemFromName("(MP)DrugLarge", ref pi))
                        {
                            ItemList.Add(pi);
                            WeightChanged();
                            hum.SendAddItem(pi);
                        }
                        else
                        {
                            Dispose(pi);
                        }
                        break;
                    // Modify the A .. B: 130646 .. 148145
                    case 130646:
                        pi = new TUserItem();
                        if (M2Share.UserEngine.CopyToUserItemFromName("SunPotion(M)", ref pi))
                        {
                            ItemList.Add(pi);
                            WeightChanged();
                            hum.SendAddItem(pi);
                        }
                        else
                        {
                            Dispose(pi);
                        }
                        break;
                    // Modify the A .. B: 148146 .. 171145
                    case 148146:
                        pi = new TUserItem();
                        if (M2Share.UserEngine.CopyToUserItemFromName("Lotteryticket", ref pi))
                        {
                            ItemList.Add(pi);
                            WeightChanged();
                            hum.SendAddItem(pi);
                        }
                        else
                        {
                            Dispose(pi);
                        }
                        break;
                    default:
                        pi = new TUserItem();
                        if (M2Share.UserEngine.CopyToUserItemFromName("SunPotion", ref pi))
                        {
                            ItemList.Add(pi);
                            WeightChanged();
                            hum.SendAddItem(pi);
                        }
                        else
                        {
                            Dispose(pi);
                        }
                        break;
                }
            }
            // 
            // if Itemlist.Count < MAXBAGITEM then begin
            // case Random(300000) of
            // 1..5:
            // begin
            // new (pi);
            // if UserEngine.CopyToUserItemFromName ('DragonSlayer', pi^) then begin
            // ItemList.Add (pi);
            // WeightChanged;
            // hum.SendAddItem (pi^);
            // end else
            // Dispose (pi);
            // end;
            // 6..10:
            // begin
            // new (pi);
            // if UserEngine.CopyToUserItemFromName ('SoulSabre', pi^) then begin
            // ItemList.Add (pi);
            // WeightChanged;
            // hum.SendAddItem (pi^);
            // end else
            // Dispose (pi);
            // end;
            // 11..15:
            // begin
            // new (pi);
            // if UserEngine.CopyToUserItemFromName ('DragonStaff', pi^) then begin
            // ItemList.Add (pi);
            // WeightChanged;
            // hum.SendAddItem (pi^);
            // end else
            // Dispose (pi);
            // end;
            // 16	..	65:
            // begin
            // new (pi);
            // if UserEngine.CopyToUserItemFromName ('BraveryOrb', pi^) then begin
            // ItemList.Add (pi);
            // WeightChanged;
            // hum.SendAddItem (pi^);
            // end else
            // Dispose (pi);
            // end;
            // 66	..	115:
            // begin
            // new (pi);
            // if UserEngine.CopyToUserItemFromName ('DemonicOrb', pi^) then begin
            // ItemList.Add (pi);
            // WeightChanged;
            // hum.SendAddItem (pi^);
            // end else
            // Dispose (pi);
            // end;
            // 116	..	165	:
            // begin
            // new (pi);
            // if UserEngine.CopyToUserItemFromName ('HolyOrb', pi^) then begin
            // ItemList.Add (pi);
            // WeightChanged;
            // hum.SendAddItem (pi^);
            // end else
            // Dispose (pi);
            // end;
            // 166	..	215	:
            // begin
            // new (pi);
            // if UserEngine.CopyToUserItemFromName ('OmaSpiritRing', pi^) then begin
            // ItemList.Add (pi);
            // WeightChanged;
            // hum.SendAddItem (pi^);
            // end else
            // Dispose (pi);
            // end;
            // 216	..	265	:
            // begin
            // new (pi);
            // if UserEngine.CopyToUserItemFromName ('NobleRing', pi^) then begin
            // ItemList.Add (pi);
            // WeightChanged;
            // hum.SendAddItem (pi^);
            // end else
            // Dispose (pi);
            // end;
            // 266	..	315	:
            // begin
            // new (pi);
            // if UserEngine.CopyToUserItemFromName ('SoulRing', pi^) then begin
            // ItemList.Add (pi);
            // WeightChanged;
            // hum.SendAddItem (pi^);
            // end else
            // Dispose (pi);
            // end;
            // 316	..	515	:
            // begin
            // new (pi);
            // if UserEngine.CopyToUserItemFromName ('RecallNecklace', pi^) then begin
            // ItemList.Add (pi);
            // WeightChanged;
            // hum.SendAddItem (pi^);
            // end else
            // Dispose (pi);
            // end;
            // 516	..	715	:
            // begin
            // new (pi);
            // if UserEngine.CopyToUserItemFromName ('RecallRing', pi^) then begin
            // ItemList.Add (pi);
            // WeightChanged;
            // hum.SendAddItem (pi^);
            // end else
            // Dispose (pi);
            // end;
            // 716	..	915	:
            // begin
            // new (pi);
            // if UserEngine.CopyToUserItemFromName ('RecallHelmet', pi^) then begin
            // ItemList.Add (pi);
            // WeightChanged;
            // hum.SendAddItem (pi^);
            // end else
            // Dispose (pi);
            // end;
            // 916	..	1115	:
            // begin
            // new (pi);
            // if UserEngine.CopyToUserItemFromName ('RecallBracelet', pi^) then begin
            // ItemList.Add (pi);
            // WeightChanged;
            // hum.SendAddItem (pi^);
            // end else
            // Dispose (pi);
            // end;
            // 1116	..	1315	:
            // begin
            // new (pi);
            // if UserEngine.CopyToUserItemFromName ('GaleGem', pi^) then begin
            // ItemList.Add (pi);
            // WeightChanged;
            // hum.SendAddItem (pi^);
            // end else
            // Dispose (pi);
            // end;
            // 1316	..	1515	:
            // begin
            // new (pi);
            // if UserEngine.CopyToUserItemFromName ('EvasionGem', pi^) then begin
            // ItemList.Add (pi);
            // WeightChanged;
            // hum.SendAddItem (pi^);
            // end else
            // Dispose (pi);
            // end;
            // 1516	..	1715	:
            // 
            // begin
            // new (pi);
            // if UserEngine.CopyToUserItemFromName ('SharpnessGem', pi^) then begin
            // ItemList.Add (pi);
            // WeightChanged;
            // hum.SendAddItem (pi^);
            // end else
            // Dispose (pi);
            // end;
            // 1716	..	1915	:
            // 
            // begin
            // new (pi);
            // if UserEngine.CopyToUserItemFromName ('PoisonGem', pi^) then begin
            // ItemList.Add (pi);
            // WeightChanged;
            // hum.SendAddItem (pi^);
            // end else
            // Dispose (pi);
            // end;
            // 1916	..	2115	:
            // 
            // begin
            // new (pi);
            // if UserEngine.CopyToUserItemFromName ('ColdnessGem', pi^) then begin
            // ItemList.Add (pi);
            // WeightChanged;
            // hum.SendAddItem (pi^);
            // end else
            // Dispose (pi);
            // end;
            // 2116	..	2315	:
            // 
            // begin
            // new (pi);
            // if UserEngine.CopyToUserItemFromName ('AwakeningGem', pi^) then begin
            // ItemList.Add (pi);
            // WeightChanged;
            // hum.SendAddItem (pi^);
            // end else
            // Dispose (pi);
            // end;
            // 2316	..	2515	:
            // 
            // begin
            // new (pi);
            // if UserEngine.CopyToUserItemFromName ('EnduranceGem)', pi^) then begin
            // ItemList.Add (pi);
            // WeightChanged;
            // hum.SendAddItem (pi^);
            // end else
            // Dispose (pi);
            // end;
            // 2516	..	2715	:
            // 
            // begin
            // new (pi);
            // if UserEngine.CopyToUserItemFromName ('VioletRing', pi^) then begin
            // ItemList.Add (pi);
            // WeightChanged;
            // hum.SendAddItem (pi^);
            // end else
            // Dispose (pi);
            // end;
            // 2716	..	2915	:
            // 
            // begin
            // new (pi);
            // if UserEngine.CopyToUserItemFromName ('DragonRing', pi^) then begin
            // ItemList.Add (pi);
            // WeightChanged;
            // hum.SendAddItem (pi^);
            // end else
            // Dispose (pi);
            // end;
            // 2916	..	3115	:
            // 
            // begin
            // new (pi);
            // if UserEngine.CopyToUserItemFromName ('TitanRing', pi^) then begin
            // ItemList.Add (pi);
            // WeightChanged;
            // hum.SendAddItem (pi^);
            // end else
            // Dispose (pi);
            // end;
            // 3116	..	3615	:
            // 
            // begin
            // new (pi);
            // if UserEngine.CopyToUserItemFromName ('HeroNecklace', pi^) then begin
            // ItemList.Add (pi);
            // WeightChanged;
            // hum.SendAddItem (pi^);
            // end else
            // Dispose (pi);
            // end;
            // 3616	..	4115	:
            // 
            // begin
            // new (pi);
            // if UserEngine.CopyToUserItemFromName ('AdamantineNeck', pi^) then begin
            // ItemList.Add (pi);
            // WeightChanged;
            // hum.SendAddItem (pi^);
            // end else
            // Dispose (pi);
            // end;
            // 4116	..	4615	:
            // 
            // begin
            // new (pi);
            // if UserEngine.CopyToUserItemFromName ('RequiemNecklac', pi^) then begin
            // ItemList.Add (pi);
            // WeightChanged;
            // hum.SendAddItem (pi^);
            // end else
            // Dispose (pi);
            // end;
            // 4616	..	5115	:
            // 
            // begin
            // new (pi);
            // if UserEngine.CopyToUserItemFromName ('BraveryGem', pi^) then begin
            // ItemList.Add (pi);
            // WeightChanged;
            // hum.SendAddItem (pi^);
            // end else
            // Dispose (pi);
            // end;
            // 5116	..	5615	:
            // 
            // begin
            // new (pi);
            // if UserEngine.CopyToUserItemFromName ('DemonicGem', pi^) then begin
            // ItemList.Add (pi);
            // WeightChanged;
            // hum.SendAddItem (pi^);
            // end else
            // Dispose (pi);
            // end;
            // 5616	..	6115	:
            // 
            // begin
            // new (pi);
            // if UserEngine.CopyToUserItemFromName ('HolyGem', pi^) then begin
            // ItemList.Add (pi);
            // WeightChanged;
            // hum.SendAddItem (pi^);
            // end else
            // Dispose (pi);
            // end;
            // 6116	..	6615	:
            // 
            // begin
            // new (pi);
            // if UserEngine.CopyToUserItemFromName ('ProtectionGem', pi^) then begin
            // ItemList.Add (pi);
            // WeightChanged;
            // hum.SendAddItem (pi^);
            // end else
            // Dispose (pi);
            // end;
            // 6616	..	7115	:
            // 
            // begin
            // new (pi);
            // if UserEngine.CopyToUserItemFromName ('ExorcismGem', pi^) then begin
            // ItemList.Add (pi);
            // WeightChanged;
            // hum.SendAddItem (pi^);
            // end else
            // Dispose (pi);
            // end;
            // 7116	..	7615	:
            // 
            // begin
            // new (pi);
            // if UserEngine.CopyToUserItemFromName ('HardnessGem', pi^) then begin
            // ItemList.Add (pi);
            // WeightChanged;
            // hum.SendAddItem (pi^);
            // end else
            // Dispose (pi);
            // end;
            // 7616	..	8115	:
            // 
            // begin
            // new (pi);
            // if UserEngine.CopyToUserItemFromName ('BaekTaGlove', pi^) then begin
            // ItemList.Add (pi);
            // WeightChanged;
            // hum.SendAddItem (pi^);
            // end else
            // Dispose (pi);
            // end;
            // 8116	..	8615	:
            // 
            // begin
            // new (pi);
            // if UserEngine.CopyToUserItemFromName ('HolyTaoWheel', pi^) then begin
            // ItemList.Add (pi);
            // WeightChanged;
            // hum.SendAddItem (pi^);
            // end else
            // Dispose (pi);
            // end;
            // 8616	..	9115	:
            // 
            // begin
            // new (pi);
            // if UserEngine.CopyToUserItemFromName ('SpiritReformer', pi^) then begin
            // ItemList.Add (pi);
            // WeightChanged;
            // hum.SendAddItem (pi^);
            // end else
            // Dispose (pi);
            // end;
            // 9116	..	16615	:
            // 
            // begin
            // new (pi);
            // if UserEngine.CopyToUserItemFromName ('BenedictionOil', pi^) then begin
            // ItemList.Add (pi);
            // WeightChanged;
            // hum.SendAddItem (pi^);
            // end else
            // Dispose (pi);
            // end;
            // 16616	..	18615	:
            // 
            // begin
            // new (pi);
            // if UserEngine.CopyToUserItemFromName ('HealthStone(L)', pi^) then begin
            // ItemList.Add (pi);
            // WeightChanged;
            // hum.SendAddItem (pi^);
            // end else
            // Dispose (pi);
            // end;
            // 18616	..	20615	:
            // 
            // begin
            // new (pi);
            // if UserEngine.CopyToUserItemFromName ('MagicStone(L)', pi^) then begin
            // ItemList.Add (pi);
            // WeightChanged;
            // hum.SendAddItem (pi^);
            // end else
            // Dispose (pi);
            // end;
            // 20616	..	22615	:
            // 
            // begin
            // new (pi);
            // if UserEngine.CopyToUserItemFromName ('PowerStone(L)', pi^) then begin
            // ItemList.Add (pi);
            // WeightChanged;
            // hum.SendAddItem (pi^);
            // end else
            // Dispose (pi);
            // end;
            // 22616	..	24615	:
            // 
            // begin
            // new (pi);
            // if UserEngine.CopyToUserItemFromName ('DCStone(L)', pi^) then begin
            // ItemList.Add (pi);
            // WeightChanged;
            // hum.SendAddItem (pi^);
            // end else
            // Dispose (pi);
            // end;
            // 24616	..	26615	:
            // 
            // begin
            // new (pi);
            // if UserEngine.CopyToUserItemFromName ('MCStone(L)', pi^) then begin
            // ItemList.Add (pi);
            // WeightChanged;
            // hum.SendAddItem (pi^);
            // end else
            // Dispose (pi);
            // end;
            // 26616	..	28615	:
            // 
            // begin
            // new (pi);
            // if UserEngine.CopyToUserItemFromName ('TaoStone(L)', pi^) then begin
            // ItemList.Add (pi);
            // WeightChanged;
            // hum.SendAddItem (pi^);
            // end else
            // Dispose (pi);
            // end;
            // 28616	..	31615	:
            // 
            // begin
            // new (pi);
            // if UserEngine.CopyToUserItemFromName ('BlackIronBrace', pi^) then begin
            // ItemList.Add (pi);
            // WeightChanged;
            // hum.SendAddItem (pi^);
            // end else
            // Dispose (pi);
            // end;
            // 31616	..	34615	:
            // 
            // begin
            // new (pi);
            // if UserEngine.CopyToUserItemFromName ('MoralRing', pi^) then begin
            // ItemList.Add (pi);
            // WeightChanged;
            // hum.SendAddItem (pi^);
            // end else
            // Dispose (pi);
            // end;
            // 34616	..	37615	:
            // 
            // begin
            // new (pi);
            // if UserEngine.CopyToUserItemFromName ('CharmRing', pi^) then begin
            // ItemList.Add (pi);
            // WeightChanged;
            // hum.SendAddItem (pi^);
            // end else
            // Dispose (pi);
            // end;
            // 37616	..	40615	:
            // 
            // begin
            // new (pi);
            // if UserEngine.CopyToUserItemFromName ('CoralRing', pi^) then begin
            // ItemList.Add (pi);
            // WeightChanged;
            // hum.SendAddItem (pi^);
            // end else
            // Dispose (pi);
            // end;
            // 40616	..	43615	:
            // 
            // begin
            // new (pi);
            // if UserEngine.CopyToUserItemFromName ('ExpelRing', pi^) then begin
            // ItemList.Add (pi);
            // WeightChanged;
            // hum.SendAddItem (pi^);
            // end else
            // Dispose (pi);
            // end;
            // 43616	..	46615	:
            // 
            // begin
            // new (pi);
            // if UserEngine.CopyToUserItemFromName ('BlueJadeNeckl', pi^) then begin
            // ItemList.Add (pi);
            // WeightChanged;
            // hum.SendAddItem (pi^);
            // end else
            // Dispose (pi);
            // end;
            // 46616	..	49615	:
            // 
            // begin
            // new (pi);
            // if UserEngine.CopyToUserItemFromName ('ConvexLens', pi^) then begin
            // ItemList.Add (pi);
            // WeightChanged;
            // hum.SendAddItem (pi^);
            // end else
            // Dispose (pi);
            // end;
            // 49616	..	52615	:
            // 
            // begin
            // new (pi);
            // if UserEngine.CopyToUserItemFromName ('BambooPipe', pi^) then begin
            // ItemList.Add (pi);
            // WeightChanged;
            // hum.SendAddItem (pi^);
            // end else
            // Dispose (pi);
            // end;
            // 52616	..	67615	:
            // 
            // begin
            // new (pi);
            // if UserEngine.CopyToUserItemFromName ('(HP)DrugBundXL', pi^) then begin
            // ItemList.Add (pi);
            // WeightChanged;
            // hum.SendAddItem (pi^);
            // end else
            // Dispose (pi);
            // end;
            // 67616	..	82615	:
            // 
            // begin
            // new (pi);
            // if UserEngine.CopyToUserItemFromName ('(MP)DrugBundXL', pi^) then begin
            // ItemList.Add (pi);
            // WeightChanged;
            // hum.SendAddItem (pi^);
            // end else
            // Dispose (pi);
            // end;
            // 82616	..	100115	:
            // 
            // begin
            // new (pi);
            // if UserEngine.CopyToUserItemFromName ('(HP)DrugBundle', pi^) then begin
            // ItemList.Add (pi);
            // WeightChanged;
            // hum.SendAddItem (pi^);
            // end else
            // Dispose (pi);
            // end;
            // 100116	..	117615	:
            // 
            // begin
            // new (pi);
            // if UserEngine.CopyToUserItemFromName ('(MP)DrugBundle', pi^) then begin
            // ItemList.Add (pi);
            // WeightChanged;
            // hum.SendAddItem (pi^);
            // end else
            // Dispose (pi);
            // end;
            // 117616	..	124365	:
            // 
            // begin
            // new (pi);
            // if UserEngine.CopyToUserItemFromName ('WarGodOil', pi^) then begin
            // ItemList.Add (pi);
            // WeightChanged;
            // hum.SendAddItem (pi^);
            // end else
            // Dispose (pi);
            // end;
            // 124366	..	141865	:
            // 
            // begin
            // new (pi);
            // if UserEngine.CopyToUserItemFromName ('(HP)DrugXL', pi^) then begin
            // ItemList.Add (pi);
            // WeightChanged;
            // hum.SendAddItem (pi^);
            // end else
            // Dispose (pi);
            // end;
            // 141866	..	159365	:
            // 
            // begin
            // new (pi);
            // if UserEngine.CopyToUserItemFromName ('(MP)DrugXL', pi^) then begin
            // ItemList.Add (pi);
            // WeightChanged;
            // hum.SendAddItem (pi^);
            // end else
            // Dispose (pi);
            // end;
            // 159366	..	179365	:
            // 
            // begin
            // new (pi);
            // if UserEngine.CopyToUserItemFromName ('(HP)DrugLarge', pi^) then begin
            // ItemList.Add (pi);
            // WeightChanged;
            // hum.SendAddItem (pi^);
            // end else
            // Dispose (pi);
            // end;
            // 179366	..	199365	:
            // 
            // begin
            // new (pi);
            // if UserEngine.CopyToUserItemFromName ('(MP)DrugLarge', pi^) then begin
            // ItemList.Add (pi);
            // WeightChanged;
            // hum.SendAddItem (pi^);
            // end else
            // Dispose (pi);
            // end;
            // 199366	..	224365	:
            // 
            // begin
            // new (pi);
            // if UserEngine.CopyToUserItemFromName ('SunPotion(M)', pi^) then begin
            // ItemList.Add (pi);
            // WeightChanged;
            // hum.SendAddItem (pi^);
            // end else
            // Dispose (pi);
            // end;
            // 224366	..	259365	:
            // 
            // begin
            // new (pi);
            // if UserEngine.CopyToUserItemFromName ('Lotteryticket', pi^) then begin
            // ItemList.Add (pi);
            // WeightChanged;
            // hum.SendAddItem (pi^);
            // end else
            // Dispose (pi);
            // end;
            // else
            // begin
            // new (pi);
            // if UserEngine.CopyToUserItemFromName ('SunPotion', pi^) then begin
            // ItemList.Add (pi);
            // WeightChanged;
            // hum.SendAddItem (pi^);
            // end else
            // Dispose (pi);
            // end;
            // end;
            // end;
            // 
            // if Itemlist.Count < MAXBAGITEM then begin
            // case Random(300000) of
            // 1..25:
            // begin
            // new (pi);
            // if UserEngine.CopyToUserItemFromName ('BraveryOrb', pi^) then begin
            // ItemList.Add (pi);
            // WeightChanged;
            // hum.SendAddItem (pi^);
            // end else
            // Dispose (pi);
            // end;
            // 26..50:
            // begin
            // new (pi);
            // if UserEngine.CopyToUserItemFromName ('DemonicOrb', pi^) then begin
            // ItemList.Add (pi);
            // WeightChanged;
            // hum.SendAddItem (pi^);
            // end else
            // Dispose (pi);
            // end;
            // 51..75:
            // begin
            // new (pi);
            // if UserEngine.CopyToUserItemFromName ('HolyOrb', pi^) then begin
            // ItemList.Add (pi);
            // WeightChanged;
            // hum.SendAddItem (pi^);
            // end else
            // Dispose (pi);
            // end;
            // 76..100:
            // begin
            // new (pi);
            // if UserEngine.CopyToUserItemFromName ('OmaSpiritRing', pi^) then begin
            // ItemList.Add (pi);
            // WeightChanged;
            // hum.SendAddItem (pi^);
            // end else
            // Dispose (pi);
            // end;
            // 101..125:
            // begin
            // new (pi);
            // if UserEngine.CopyToUserItemFromName ('NobleRIng', pi^) then begin
            // ItemList.Add (pi);
            // WeightChanged;
            // hum.SendAddItem (pi^);
            // end else
            // Dispose (pi);
            // end;
            // 126..150:
            // begin
            // new (pi);
            // if UserEngine.CopyToUserItemFromName ('SoulRing', pi^) then begin
            // ItemList.Add (pi);
            // WeightChanged;
            // hum.SendAddItem (pi^);
            // end else
            // Dispose (pi);
            // end;
            // 151..225:
            // begin
            // new (pi);
            // if UserEngine.CopyToUserItemFromName ('GaleGem', pi^) then begin
            // ItemList.Add (pi);
            // WeightChanged;
            // hum.SendAddItem (pi^);
            // end else
            // Dispose (pi);
            // end;
            // 226..325:
            // begin
            // new (pi);
            // if UserEngine.CopyToUserItemFromName ('EvasionGem', pi^) then begin
            // ItemList.Add (pi);
            // WeightChanged;
            // hum.SendAddItem (pi^);
            // end else
            // Dispose (pi);
            // end;
            // 326..425:
            // begin
            // new (pi);
            // if UserEngine.CopyToUserItemFromName ('SharpnessGem', pi^) then begin
            // ItemList.Add (pi);
            // WeightChanged;
            // hum.SendAddItem (pi^);
            // end else
            // Dispose (pi);
            // end;
            // 426..525:
            // begin
            // new (pi);
            // if UserEngine.CopyToUserItemFromName ('PoisonGem', pi^) then begin
            // ItemList.Add (pi);
            // WeightChanged;
            // hum.SendAddItem (pi^);
            // end else
            // Dispose (pi);
            // end;
            // 526..625:
            // begin
            // new (pi);
            // if UserEngine.CopyToUserItemFromName ('ColdnessGem', pi^) then begin
            // ItemList.Add (pi);
            // WeightChanged;
            // hum.SendAddItem (pi^);
            // end else
            // Dispose (pi);
            // end;
            // 626..725:
            // begin
            // new (pi);
            // if UserEngine.CopyToUserItemFromName ('AwakeningGem', pi^) then begin
            // ItemList.Add (pi);
            // WeightChanged;
            // hum.SendAddItem (pi^);
            // end else
            // Dispose (pi);
            // end;
            // 726..825:
            // begin
            // new (pi);
            // if UserEngine.CopyToUserItemFromName ('EnduranceGem', pi^) then begin
            // ItemList.Add (pi);
            // WeightChanged;
            // hum.SendAddItem (pi^);
            // end else
            // Dispose (pi);
            // end;
            // 826..925:
            // begin
            // new (pi);
            // if UserEngine.CopyToUserItemFromName ('VioletRing', pi^) then begin
            // ItemList.Add (pi);
            // WeightChanged;
            // hum.SendAddItem (pi^);
            // end else
            // Dispose (pi);
            // end;
            // 926..1025:
            // begin
            // new (pi);
            // if UserEngine.CopyToUserItemFromName ('DrangonRingm', pi^) then begin
            // ItemList.Add (pi);
            // WeightChanged;
            // hum.SendAddItem (pi^);
            // end else
            // Dispose (pi);
            // end;
            // 1026..1125:
            // begin
            // new (pi);
            // if UserEngine.CopyToUserItemFromName ('TitanRing', pi^) then begin
            // ItemList.Add (pi);
            // WeightChanged;
            // hum.SendAddItem (pi^);
            // end else
            // Dispose (pi);
            // end;
            // 1126..1325:
            // begin
            // new (pi);
            // if UserEngine.CopyToUserItemFromName ('HeroNecklace', pi^) then begin
            // ItemList.Add (pi);
            // WeightChanged;
            // hum.SendAddItem (pi^);
            // end else
            // Dispose (pi);
            // end;
            // 1326..1525:
            // begin
            // new (pi);
            // if UserEngine.CopyToUserItemFromName ('AdamantineNeck', pi^) then begin
            // ItemList.Add (pi);
            // WeightChanged;
            // hum.SendAddItem (pi^);
            // end else
            // Dispose (pi);
            // end;
            // 1526..1725:
            // begin
            // new (pi);
            // if UserEngine.CopyToUserItemFromName ('RequiemNecklac', pi^) then begin
            // ItemList.Add (pi);
            // WeightChanged;
            // hum.SendAddItem (pi^);
            // end else
            // Dispose (pi);
            // end;
            // 1726..1925:
            // begin
            // new (pi);
            // if UserEngine.CopyToUserItemFromName ('BraveryGem)', pi^) then begin
            // ItemList.Add (pi);
            // WeightChanged;
            // hum.SendAddItem (pi^);
            // end else
            // Dispose (pi);
            // end;
            // 1926..2125:
            // begin
            // new (pi);
            // if UserEngine.CopyToUserItemFromName ('DemonicGem', pi^) then begin
            // ItemList.Add (pi);
            // WeightChanged;
            // hum.SendAddItem (pi^);
            // end else
            // Dispose (pi);
            // end;
            // 2126..2325:
            // begin
            // new (pi);
            // if UserEngine.CopyToUserItemFromName ('HolyGem', pi^) then begin
            // ItemList.Add (pi);
            // WeightChanged;
            // hum.SendAddItem (pi^);
            // end else
            // Dispose (pi);
            // end;
            // 2326..2525:
            // begin
            // new (pi);
            // if UserEngine.CopyToUserItemFromName ('ProtectionGem', pi^) then begin
            // ItemList.Add (pi);
            // WeightChanged;
            // hum.SendAddItem (pi^);
            // end else
            // Dispose (pi);
            // end;
            // 2526..2725:
            // begin
            // new (pi);
            // if UserEngine.CopyToUserItemFromName ('ExorcismGem', pi^) then begin
            // ItemList.Add (pi);
            // WeightChanged;
            // hum.SendAddItem (pi^);
            // end else
            // Dispose (pi);
            // end;
            // 2726..2925:
            // begin
            // new (pi);
            // if UserEngine.CopyToUserItemFromName ('HardnessGem', pi^) then begin
            // ItemList.Add (pi);
            // WeightChanged;
            // hum.SendAddItem (pi^);
            // end else
            // Dispose (pi);
            // end;
            // 2926..3175:
            // begin
            // new (pi);
            // if UserEngine.CopyToUserItemFromName ('BaekTaGlove', pi^) then begin
            // ItemList.Add (pi);
            // WeightChanged;
            // hum.SendAddItem (pi^);
            // end else
            // Dispose (pi);
            // end;
            // 3176..3425:
            // begin
            // new (pi);
            // if UserEngine.CopyToUserItemFromName ('HolyTaoWheel', pi^) then begin
            // ItemList.Add (pi);
            // WeightChanged;
            // hum.SendAddItem (pi^);
            // end else
            // Dispose (pi);
            // end;
            // 3426..3675:
            // begin
            // new (pi);
            // if UserEngine.CopyToUserItemFromName ('SpiritReformer', pi^) then begin
            // ItemList.Add (pi);
            // WeightChanged;
            // hum.SendAddItem (pi^);
            // end else
            // Dispose (pi);
            // end;
            // 3676..12675:
            // begin
            // new (pi);
            // if UserEngine.CopyToUserItemFromName ('BenedictionOil', pi^) then begin
            // ItemList.Add (pi);
            // WeightChanged;
            // hum.SendAddItem (pi^);
            // end else
            // Dispose (pi);
            // end;
            // 12676..13675:
            // begin
            // new (pi);
            // if UserEngine.CopyToUserItemFromName ('HealthStone(L)', pi^) then begin
            // ItemList.Add (pi);
            // WeightChanged;
            // hum.SendAddItem (pi^);
            // end else
            // Dispose (pi);
            // end;
            // 13676..14675:
            // begin
            // new (pi);
            // if UserEngine.CopyToUserItemFromName ('MagicStone(L)', pi^) then begin
            // ItemList.Add (pi);
            // WeightChanged;
            // hum.SendAddItem (pi^);
            // end else
            // Dispose (pi);
            // end;
            // 14676..15675:
            // begin
            // new (pi);
            // if UserEngine.CopyToUserItemFromName ('PowerStone(L)', pi^) then begin
            // ItemList.Add (pi);
            // WeightChanged;
            // hum.SendAddItem (pi^);
            // end else
            // Dispose (pi);
            // end;
            // 15676..16675:
            // begin
            // new (pi);
            // if UserEngine.CopyToUserItemFromName ('DCStone(L)', pi^) then begin
            // ItemList.Add (pi);
            // WeightChanged;
            // hum.SendAddItem (pi^);
            // end else
            // Dispose (pi);
            // end;
            // 16676..17675:
            // begin
            // new (pi);
            // if UserEngine.CopyToUserItemFromName ('MCStone(L)', pi^) then begin
            // ItemList.Add (pi);
            // WeightChanged;
            // hum.SendAddItem (pi^);
            // end else
            // Dispose (pi);
            // end;
            // 17676..18675:
            // begin
            // new (pi);
            // if UserEngine.CopyToUserItemFromName ('TaoStone(L)', pi^) then begin
            // ItemList.Add (pi);
            // WeightChanged;
            // hum.SendAddItem (pi^);
            // end else
            // Dispose (pi);
            // end;
            // 18676..22425:
            // begin
            // new (pi);
            // if UserEngine.CopyToUserItemFromName ('BlackIronBrace', pi^) then begin
            // ItemList.Add (pi);
            // WeightChanged;
            // hum.SendAddItem (pi^);
            // end else
            // Dispose (pi);
            // end;
            // 22426..26175:
            // begin
            // new (pi);
            // if UserEngine.CopyToUserItemFromName ('MoralRing', pi^) then begin
            // ItemList.Add (pi);
            // WeightChanged;
            // hum.SendAddItem (pi^);
            // end else
            // Dispose (pi);
            // end;
            // 26176..29925:
            // begin
            // new (pi);
            // if UserEngine.CopyToUserItemFromName ('CharmRing', pi^) then begin
            // ItemList.Add (pi);
            // WeightChanged;
            // hum.SendAddItem (pi^);
            // end else
            // Dispose (pi);
            // end;
            // 29926..33675:
            // begin
            // new (pi);
            // if UserEngine.CopyToUserItemFromName ('CoralRing', pi^) then begin
            // ItemList.Add (pi);
            // WeightChanged;
            // hum.SendAddItem (pi^);
            // end else
            // Dispose (pi);
            // end;
            // 33676..37425:
            // begin
            // new (pi);
            // if UserEngine.CopyToUserItemFromName ('ExpelRing', pi^) then begin
            // ItemList.Add (pi);
            // WeightChanged;
            // hum.SendAddItem (pi^);
            // end else
            // Dispose (pi);
            // end;
            // 37426..41175:
            // begin
            // new (pi);
            // if UserEngine.CopyToUserItemFromName ('BlueJadeNeckl', pi^) then begin
            // ItemList.Add (pi);
            // WeightChanged;
            // hum.SendAddItem (pi^);
            // end else
            // Dispose (pi);
            // end;
            // 41176..44925:
            // begin
            // new (pi);
            // if UserEngine.CopyToUserItemFromName ('ConvexLens', pi^) then begin
            // ItemList.Add (pi);
            // WeightChanged;
            // hum.SendAddItem (pi^);
            // end else
            // Dispose (pi);
            // end;
            // 44926..48675:
            // begin
            // new (pi);
            // if UserEngine.CopyToUserItemFromName ('BambooPipe', pi^) then begin
            // ItemList.Add (pi);
            // WeightChanged;
            // hum.SendAddItem (pi^);
            // end else
            // Dispose (pi);
            // end;
            // 48676..62175:
            // begin
            // new (pi);
            // if UserEngine.CopyToUserItemFromName ('(HP)DrugBundXL', pi^) then begin
            // ItemList.Add (pi);
            // WeightChanged;
            // hum.SendAddItem (pi^);
            // end else
            // Dispose (pi);
            // end;
            // 62176..75675:
            // begin
            // new (pi);
            // if UserEngine.CopyToUserItemFromName ('(MP)DrugBundXL', pi^) then begin
            // ItemList.Add (pi);
            // WeightChanged;
            // hum.SendAddItem (pi^);
            // end else
            // Dispose (pi);
            // end;
            // 75676..98175:
            // begin
            // new (pi);
            // if UserEngine.CopyToUserItemFromName ('(HP)DrugBundle', pi^) then begin
            // ItemList.Add (pi);
            // WeightChanged;
            // hum.SendAddItem (pi^);
            // end else
            // Dispose (pi);
            // end;
            // 98176..120675:
            // begin
            // new (pi);
            // if UserEngine.CopyToUserItemFromName ('(MP)DrugBundle', pi^) then begin
            // ItemList.Add (pi);
            // WeightChanged;
            // hum.SendAddItem (pi^);
            // end else
            // Dispose (pi);
            // end;
            // 120676..127425:
            // begin
            // new (pi);
            // if UserEngine.CopyToUserItemFromName ('WarGodOil', pi^) then begin
            // ItemList.Add (pi);
            // WeightChanged;
            // hum.SendAddItem (pi^);
            // end else
            // Dispose (pi);
            // end;
            // 127426..137550:
            // begin
            // new (pi);
            // if UserEngine.CopyToUserItemFromName ('(HP)DrugXL', pi^) then begin
            // ItemList.Add (pi);
            // WeightChanged;
            // hum.SendAddItem (pi^);
            // end else
            // Dispose (pi);
            // end;
            // 137551..147675:
            // begin
            // new (pi);
            // if UserEngine.CopyToUserItemFromName ('(MP)DrugXL', pi^) then begin
            // ItemList.Add (pi);
            // WeightChanged;
            // hum.SendAddItem (pi^);
            // end else
            // Dispose (pi);
            // end;
            // 147676..161175:
            // begin
            // new (pi);
            // if UserEngine.CopyToUserItemFromName ('(HP)DrugLarge', pi^) then begin
            // ItemList.Add (pi);
            // WeightChanged;
            // hum.SendAddItem (pi^);
            // end else
            // Dispose (pi);
            // end;
            // 161176..174675:
            // begin
            // new (pi);
            // if UserEngine.CopyToUserItemFromName ('(MP)DrugLarge', pi^) then begin
            // ItemList.Add (pi);
            // WeightChanged;
            // hum.SendAddItem (pi^);
            // end else
            // Dispose (pi);
            // end;
            // 174676..204675:
            // begin
            // new (pi);
            // if UserEngine.CopyToUserItemFromName ('SunPotion(M)', pi^) then begin
            // ItemList.Add (pi);
            // WeightChanged;
            // hum.SendAddItem (pi^);
            // end else
            // Dispose (pi);
            // end;
            // 204676..234675:
            // begin
            // new (pi);
            // if UserEngine.CopyToUserItemFromName ('Lotteryticket', pi^) then begin
            // ItemList.Add (pi);
            // WeightChanged;
            // hum.SendAddItem (pi^);
            // end else
            // Dispose (pi);
            // end;
            // else
            // begin
            // new (pi);
            // if UserEngine.CopyToUserItemFromName ('SunPotion', pi^) then begin
            // ItemList.Add (pi);
            // WeightChanged;
            // hum.SendAddItem (pi^);
            // end else
            // Dispose (pi);
            // end;
            // end;
            // end;

        }

        // 2006何劝例 崔翱
        // 2006何劝例 崔翱
        public void GetGiftFromEgg()
        {
            TUserItem pi;
            TUserHuman hum;
            if (RaceServer != Grobal2.RC_USERHUMAN)
            {
                return;
            }
            hum = this as TUserHuman;
            if (hum == null)
            {
                return;
            }
            if (ItemList.Count < Grobal2.MAXBAGITEM)
            {
                switch (new System.Random(4).Next())
                {
                    case 1:
                        pi = new TUserItem();
                        if (M2Share.UserEngine.CopyToUserItemFromName("HolyWater", ref pi))
                        {
                            ItemList.Add(pi);
                            WeightChanged();
                            hum.SendAddItem(pi);
                        }
                        else
                        {
                            Dispose(pi);
                        }
                        break;
                    default:
                        pi = new TUserItem();
                        if (M2Share.UserEngine.CopyToUserItemFromName("BloodBone", ref pi))
                        {
                            ItemList.Add(pi);
                            WeightChanged();
                            hum.SendAddItem(pi);
                        }
                        else
                        {
                            Dispose(pi);
                        }
                        break;
                }
            }
        }

        public void SendGameConfig()
        {
            if (RaceServer == Grobal2.RC_USERHUMAN)
            {
                string str = EDcode.EncodeBuffer(M2Share.g_GameConfig);
                SendMsg(this, Grobal2.RM_GAMECONFIG, 0, 0, 0, 0, str);
            }
        }

        public long GetTickCount => System.Environment.TickCount;

        public long GetCurrentTime => Environment.TickCount;


        public byte LoByte(short val)
        {
            return HUtil32.LoByte(val);
        }

        public byte HiByte(short val)
        {
            return HUtil32.HiByte(val);
        }

        public int _MIN(int miniVal, int maxVal)
        {
            return HUtil32._MIN(miniVal, maxVal);
        }

        public int _MAX(int miniVal, int maxVal)
        {
            return HUtil32._MAX(miniVal, maxVal);
        }

        public void Dispose(object obj)
        {
            if (obj != null)
            {
                obj = null;
            }
        }

        public short MakeWord(int miniVal, int maxVal)
        {
            return HUtil32.MakeWord(miniVal, maxVal);
        }
    }
}