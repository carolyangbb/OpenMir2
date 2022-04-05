using System;
using System.Collections;
using System.Collections.Generic;
using SystemModule;

namespace RobotSvr
{
    public class TActor
    {
        public byte m_btTitleIndex = 0;
        public byte m_RushStep = 0;
        public bool m_btAFilter = false;
        public byte m_btPoisonDecHealth = 0;
        public int m_nRecogId = 0;
        public bool n_boState = false;
        public int m_nIPower = 0;
        public int m_nIPowerLvl = 0;
        public long m_nIPowerExp = 0;
        public int m_nTagX = 0;
        public int m_nTagY = 0;
        public int m_nCurrX = 0;
        public int m_nCurrY = 0;
        public byte m_btDir = 0;
        public byte m_btSex = 0;
        public byte m_btRace = 0;
        public byte m_btHair = 0;
        public byte m_btHairEx = 0;
        public byte m_btDress = 0;
        // 衣服类型
        public byte m_btWeapon = 0;
        // 武器类型
        public byte m_btWeaponEffect = 0;
        // 武器特效类型
        public byte m_btHorse = 0;
        public byte m_btEffect = 0;
        public byte m_btAttribute = 0;
        public byte m_btJob = 0;
        public short m_wGloryPoint = 0;
        public short m_wAppearance = 0;
        public byte m_btDeathState = 0;
        public int m_nFeature = 0;
        public int m_nFeatureEx = 0;
        public short m_wAppr = 0;
        public int m_nState = 0;
        public bool m_boDeath = false;
        public bool m_boSkeleton = false;
        public bool m_boItemExplore = false;
        public bool m_boDelActor = false;
        public bool m_boDelActionAfterFinished = false;
        public string m_sDescUserName;
        public string m_sUserName = String.Empty;
        public int m_sUserNameOffSet = 0;
        public string m_sLoyaly;
        public string m_sAutoSayMsg;
        public byte m_btNameColor = 0;
        public int m_nNameColor = 0;
        public TAbility m_Abil = null;
        public TOAbility m_Abils = null;
        public int m_nGold = 0;
        public int m_nGameGold = 0;
        public short m_nGameGird = 0;
        public int m_nGamePoint = 0;
        public short m_nGameDiamd = 0;
        public short m_nHitSpeed = 0;
        public bool m_boVisible = false;
        public bool m_boHoldPlace = false;
        public string[] m_SayingArr;
        public int[] m_SayWidthsArr;
        public long m_dwSayTime = 0;
        public int m_nSayX = 0;
        public int m_nSayY = 0;
        public int m_nSayLineCount = 0;
        public int m_nShiftX = 0;
        public int m_nShiftY = 0;
        public int m_nPx = 0;
        public int m_nHpx = 0;
        public int m_nWpx = 0;
        public int m_nSpx = 0;
        public int m_nPy = 0;
        public int m_nHpy = 0;
        public int m_nWpy = 0;
        public int m_nSpy = 0;
        public int m_nRx = 0;
        public int m_nRy = 0;
        public int m_nWpeX = 0;
        public int m_nWpeY = 0;
        public int m_nDownDrawLevel = 0;
        public int m_nTargetX = 0;
        public int m_nTargetY = 0;
        public int m_nTargetRecog = 0;
        public int m_nHiterCode = 0;
        public int m_nMagicNum = 0;
        public int m_nCurrentEvent = 0;
        public bool m_boDigFragment = false;
        public bool m_boThrow = false;
        public bool m_boHeroLongHit = false;
        public bool m_boHeroLongHit2 = false;
        public bool m_boSmiteHit = false;
        public byte m_boSmiteLongHit = 0;
        public bool m_boSmiteLongHit2 = false;
        public byte m_boSmiteLongHitS2 = 0;
        public bool m_boSmiteWideHit2 = false;
        public byte m_boSmiteWideHitS2 = 0;
        public int m_nBodyOffset = 0;
        public int m_nHairOffset = 0;
        public int m_nHairOffsetEx = 0;
        public int m_nCboHairOffset = 0;
        public int m_nHumWinOffset = 0;
        public int m_nCboHumWinOffSet = 0;
        public int m_nWeaponOffset = 0;
        public bool m_boUseCboLib = false;
        public bool m_boUseMagic = false;
        public bool m_boNewMagic = false;
        public bool m_boHitEffect = false;
        public bool m_boUseEffect = false;
        public int m_nHitEffectNumber = 0;
        public long m_dwWaitMagicRequest = 0;
        public int m_nWaitForRecogId = 0;
        public int m_nWaitForFeature = 0;
        public int m_nWaitForStatus = 0;
        public int m_nCurEffFrame = 0;
        public int m_nSpellFrame = 0;
        public TUseMagicInfo m_CurMagic = null;
        public int m_nGenAniCount = 0;
        public bool m_boOpenHealth = false;
        public bool m_noInstanceOpenHealth = false;
        public long m_dwOpenHealthStart = 0;
        public long m_dwOpenHealthTime = 0;
        public bool m_boGrouped = false;
        public int m_nCurrentAction = 0;
        public bool m_boReverseFrame = false;
        public bool m_boWarMode = false;
        public long m_dwWarModeTime = 0;
        public int m_nChrLight = 0;
        public int m_nMagLight = 0;
        public int m_nRushDir = 0;
        public int m_nXxI = 0;
        public bool m_boLockEndFrame = false;
        public long m_dwLastStruckTime = 0;
        public long m_dwSendQueryUserNameTime = 0;
        public long m_dwDeleteTime = 0;
        public int m_nMagicStruckSound = 0;
        public bool m_boRunSound = false;
        public int m_nFootStepSound = 0;
        public int m_nStruckSound = 0;
        public int m_nStruckWeaponSound = 0;
        public int m_nAppearSound = 0;
        public int m_nNormalSound = 0;
        public int m_nAttackSound = 0;
        public int m_nWeaponSound = 0;
        public int m_nScreamSound = 0;
        public int m_nDieSound = 0;
        public int m_nDie2Sound = 0;
        public int m_nMagicStartSound = 0;
        public int m_nMagicFireSound = 0;
        public int m_nMagicExplosionSound = 0;
        public TMonsterAction m_Action = null;
        public int m_nHeroEnergyType = 0;
        public int m_nHeroEnergy = 0;
        public int m_nMaxHeroEnergy = 0;
        public int m_nBagSize = 0;
        public byte m_btIsHero = 0;
        public long m_dwMsgHint = 0;
        public long m_dwHealthHP = 0;
        public long m_dwHealthMP = 0;
        public long m_dwHealthSP = 0;
        public long m_dwHealthBK = 0;
        public long m_dwAutoTecTick = 0;
        public long m_dwAutoTecHeroTick = 0;
        public long m_dwPracticeTick = 0;
        public bool m_boFisrShopItem = false;
        public bool m_boAttackSlow = false;
        // 腕力不够时慢动作攻击.
        public bool m_boMoveSlow = false;
        // 负重不够时慢动作跑
        public int m_nMoveSlowLevel = 0;
        public ArrayList m_StruckDamage = null;
        // m_StruckDamageTick: LongWord;
        // m_StruckDamageTick2: LongWord;
        public bool m_fHideMode = false;
        public byte m_nTempState = 0;
        protected int m_nStartFrame = 0;
        protected int m_nEndFrame = 0;
        protected int m_nEffectStart = 0;
        protected int m_nEffectFrame = 0;
        protected int m_nEffectEnd = 0;
        protected long m_dwEffectStartTime = 0;
        protected long m_dwEffectFrameTime = 0;
        protected long m_dwFrameTime = 0;
        protected long m_dwStartTime = 0;
        protected int m_nMaxTick = 0;
        protected int m_nCurTick = 0;
        protected int m_nMoveStep = 0;
        protected bool m_boMsgMuch = false;
        protected long m_dwStruckFrameTime = 0;
        protected int m_nCurrentDefFrame = 0;
        protected long m_dwDefFrameTime = 0;
        protected int m_nDefFrameCount = 0;
        protected int m_nSkipTick = 0;
        protected long m_dwSmoothMoveTime = 0;
        protected long m_dwGenAnicountTime = 0;
        protected int m_nOldx = 0;
        protected int m_nOldy = 0;
        protected int m_nOldDir = 0;
        protected int m_nWpord = 0;
        public int m_nCurFocusFrame = 0;
        public long m_dwFocusFrameTick = 0;
        public int m_nCurrentFrame = 0;
        public long m_dwLoadSurfaceTime = 0;
        public int m_nActBeforeX = 0;
        public int m_nActBeforeY = 0;
        public TChrMsg m_ChrMsg = null;
        public IList<string> m_MsgList = null;
        public TChrMsg RealActionMsg = null;
        public ArrayList m_nMoveHpList = null;

        public TActor() : base()
        {
            m_btTitleIndex = 0;
            m_nCurFocusFrame = 0;
            m_nWaitForRecogId = 0;
            m_MsgList = new List<string>();
            m_nRecogId = 0;
            m_wAppr = 0;
            m_btPoisonDecHealth = 0;
            n_boState = false;
            m_btAFilter = false;
            m_nIPower = -1;
            m_nIPowerLvl = 0;
            m_nIPowerExp = 0;
            m_btAttribute = 0;
            m_nGold = 0;
            m_boVisible = true;
            m_boHoldPlace = true;
            m_wGloryPoint = 0;
            m_nCurrentAction = 0;
            m_boReverseFrame = false;
            m_nShiftX = 0;
            m_nShiftY = 0;
            m_nDownDrawLevel = 0;
            m_nCurrentFrame = -1;
            m_nEffectFrame = -1;
            RealActionMsg.Ident = 0;
            m_sUserName = "";
            m_sUserNameOffSet = 0;
            m_sAutoSayMsg = "";
            m_btNameColor = 255;
            m_nNameColor = 255;
            m_dwSendQueryUserNameTime = MShare.GetTickCount();
            m_boWarMode = false;
            m_dwWarModeTime = 0;
            m_boDeath = false;
            m_boSkeleton = false;
            m_boItemExplore = false;
            m_boDelActor = false;
            m_boDelActionAfterFinished = false;
            m_nChrLight = 0;
            m_nMagLight = 0;
            m_boLockEndFrame = false;
            m_dwSmoothMoveTime = 0;
            m_dwGenAnicountTime = 0;
            m_dwDefFrameTime = 0;
            m_dwLoadSurfaceTime = MShare.GetTickCount();
            m_boGrouped = false;
            m_boOpenHealth = false;
            m_noInstanceOpenHealth = false;
            m_CurMagic.ServerMagicCode = 0;
            m_nSpellFrame = Actor.DEFSPELLFRAME;
            m_nNormalSound = -1;
            m_nFootStepSound = -1;
            m_nAttackSound = -1;
            m_nWeaponSound = -1;
            m_nStruckSound = SoundUtil.s_struck_body_longstick;
            m_nStruckWeaponSound = -1;
            m_nScreamSound = -1;
            m_nDieSound = -1;
            m_nDie2Sound = -1;
            m_btIsHero = 0;
            m_boFisrShopItem = true;
            m_boSmiteLongHit = 0;
            m_boSmiteLongHit2 = false;
            m_boSmiteLongHitS2 = 0;
            m_boSmiteWideHit2 = false;
            m_boSmiteWideHitS2 = 0;
            m_boAttackSlow = false;
            m_boMoveSlow = false;
            m_nMoveSlowLevel = 0;
            m_btWeaponEffect = 0;
            m_StruckDamage = new ArrayList();
            m_nMoveHpList = new ArrayList();
            m_nTempState = 1;
            m_fHideMode = false;
        }

        public void SendMsg(int wIdent, int nX, int nY, int ndir, int nFeature, int nState, string sStr, int nSound, int dwDelay = 0)
        {
            TChrMsg Msg;
            Msg = new TChrMsg();
            Msg.Ident = wIdent;
            Msg.X = nX;
            Msg.Y = nY;
            Msg.Dir = ndir;
            Msg.Feature = nFeature;
            Msg.State = nState;
            Msg.Saying = HUtil32.Str_ToInt(sStr, 0);
            Msg.Sound = nSound;
            if (dwDelay > 0)
            {
                Msg.dwDelay = MShare.GetTickCount() + dwDelay;
            }
            else
            {
                Msg.dwDelay = 0;
            }
            m_MsgList.Add(Msg);
        }

        public void UpdateMsg(short wIdent, int nX, int nY, int ndir, int nFeature, int nState, string sStr, int nSound)
        {
            int i;
            TChrMsg Msg;
            m_MsgList.__Lock();
            try
            {
                i = 0;
                while (true)
                {
                    if (i >= m_MsgList.Count)
                    {
                        break;
                    }
                    Msg = m_MsgList[i];
                    if (((this == MShare.g_MySelf) && (Msg.Ident >= 3000) && (Msg.Ident <= 3099)) || (Msg.Ident == wIdent))
                    {
                        Dispose(Msg);
                        m_MsgList.RemoveAt(i);
                        continue;
                    }
                    i++;
                }
            }
            finally
            {
                m_MsgList.UnLock();
            }
            SendMsg(wIdent, nX, nY, ndir, nFeature, nState, sStr, nSound);
        }

        public void CleanUserMsgs()
        {
            int i;
            TChrMsg Msg;
            m_MsgList.__Lock();
            try
            {
                i = 0;
                while (true)
                {
                    if (i >= m_MsgList.Count)
                    {
                        break;
                    }
                    Msg = m_MsgList[i];
                    if ((Msg.Ident >= 3000) && (Msg.Ident <= 3099))
                    {
                        //@ Unsupported function or procedure: 'Dispose'
                        Dispose(Msg);
                        m_MsgList.RemoveAt(i);
                        continue;
                    }
                    i++;
                }
            }
            finally
            {
                m_MsgList.UnLock();
            }
        }

        public virtual void CalcActorFrame()
        {
            m_boUseCboLib = false;
            m_boUseMagic = false;
            m_boNewMagic = false;
            m_nCurrentFrame = -1;
            m_nBodyOffset = Actor.GetOffset(m_wAppearance);
            m_Action = Actor.GetRaceByPM(m_btRace, m_wAppearance);
            if (m_Action == null)
            {
                return;
            }
            switch (m_nCurrentAction)
            {
                case Grobal2.SM_TURN:
                    m_nStartFrame = m_Action.ActStand.start + m_btDir * (m_Action.ActStand.frame + m_Action.ActStand.skip);
                    m_nEndFrame = m_nStartFrame + m_Action.ActStand.frame - 1;
                    m_dwFrameTime = m_Action.ActStand.ftime;
                    m_dwStartTime = MShare.GetTickCount();
                    m_nDefFrameCount = m_Action.ActStand.frame;
                    Shift(m_btDir, 0, 0, 1);
                    if (m_fHideMode)
                    {
                        m_fHideMode = false;
                        m_dwSmoothMoveTime = 0;
                        m_nCurrentAction = 0;
                    }
                    break;
                case Grobal2.SM_WALK:
                case Grobal2.SM_RUSH:
                case Grobal2.SM_RUSHKUNG:
                case Grobal2.SM_BACKSTEP:
                    m_nStartFrame = m_Action.ActWalk.start + m_btDir * (m_Action.ActWalk.frame + m_Action.ActWalk.skip);
                    m_nEndFrame = m_nStartFrame + m_Action.ActWalk.frame - 1;
                    m_dwFrameTime = m_Action.ActWalk.ftime;
                    m_dwStartTime = MShare.GetTickCount();
                    m_nMaxTick = m_Action.ActWalk.usetick;
                    m_nCurTick = 0;
                    m_nMoveStep = 1;
                    if (m_nCurrentAction == Grobal2.SM_BACKSTEP)
                    {
                        Shift(ClFunc.GetBack(m_btDir), m_nMoveStep, 0, m_nEndFrame - m_nStartFrame + 1);
                    }
                    else
                    {
                        Shift(m_btDir, m_nMoveStep, 0, m_nEndFrame - m_nStartFrame + 1);
                    }
                    break;
                case Grobal2.SM_HIT:
                    m_nStartFrame = m_Action.ActAttack.start + m_btDir * (m_Action.ActAttack.frame + m_Action.ActAttack.skip);
                    m_nEndFrame = m_nStartFrame + m_Action.ActAttack.frame - 1;
                    m_dwFrameTime = m_Action.ActAttack.ftime;
                    m_dwStartTime = MShare.GetTickCount();
                    m_dwWarModeTime = MShare.GetTickCount();
                    Shift(m_btDir, 0, 0, 1);
                    break;
                case Grobal2.SM_STRUCK:
                    if (m_btRace != 12)
                    {
                        m_nStartFrame = m_Action.ActStruck.start + m_btDir * (m_Action.ActStruck.frame + m_Action.ActStruck.skip);
                        m_nEndFrame = m_nStartFrame + m_Action.ActStruck.frame - 1;
                        m_dwFrameTime = m_dwStruckFrameTime;
                        m_dwStartTime = MShare.GetTickCount();
                        Shift(m_btDir, 0, 0, 1);
                    }
                    break;
                case Grobal2.SM_DEATH:
                    m_nStartFrame = m_Action.ActDie.start + m_btDir * (m_Action.ActDie.frame + m_Action.ActDie.skip);
                    m_nEndFrame = m_nStartFrame + m_Action.ActDie.frame - 1;
                    m_nStartFrame = m_nEndFrame;
                    m_dwFrameTime = m_Action.ActDie.ftime;
                    m_dwStartTime = MShare.GetTickCount();
                    break;
                case Grobal2.SM_NOWDEATH:
                    m_nStartFrame = m_Action.ActDie.start + m_btDir * (m_Action.ActDie.frame + m_Action.ActDie.skip);
                    m_nEndFrame = m_nStartFrame + m_Action.ActDie.frame - 1;
                    m_dwFrameTime = m_Action.ActDie.ftime;
                    m_dwStartTime = MShare.GetTickCount();
                    break;
                case Grobal2.SM_SKELETON:
                    m_nStartFrame = m_Action.ActDeath.start + m_btDir;
                    m_nEndFrame = m_nStartFrame + m_Action.ActDeath.frame - 1;
                    m_dwFrameTime = m_Action.ActDeath.ftime;
                    m_dwStartTime = MShare.GetTickCount();
                    break;
            }
        }

        public void ReadyAction(TChrMsg Msg)
        {
            int n;
            TUseMagicInfo UseMagic;
            m_nActBeforeX = m_nCurrX;
            m_nActBeforeY = m_nCurrY;
            if (Msg.Ident == Grobal2.SM_ALIVE)
            {
                MShare.g_boViewFog = MShare.g_boLastViewFog;
                m_boDeath = false;
                m_boSkeleton = false;
                m_boItemExplore = false;
            }
            if (!m_boDeath)
            {
                switch (Msg.Ident)
                {
                    case Grobal2.SM_TURN:
                    case Grobal2.SM_WALK:
                    case Grobal2.SM_BACKSTEP:
                    case Grobal2.SM_RUSH:
                    case Grobal2.SM_RUSHEX:
                    case Grobal2.SM_RUSHKUNG:
                    case Grobal2.SM_RUN:
                    case Grobal2.SM_HORSERUN:
                    case Grobal2.SM_DIGUP:
                    case Grobal2.SM_ALIVE:
                        // m_nFeature := MakeHumanFeature(0, DRESSfeature(Msg.Feature), WEAPONfeature(Msg.Feature), 6 * 10 + 1 * 2 + 1); //Msg.Feature;
                        m_nFeature = Msg.Feature;
                        m_nState = Msg.State;
                        //@ Unsupported function or procedure: 'HUtil32.HiByte'
                        m_RushStep = HUtil32.HiByte(Msg.Dir);
                        if (m_nState & Grobal2.STATE_OPENHEATH != 0)
                        {
                            m_boOpenHealth = true;
                        }
                        else
                        {
                            m_boOpenHealth = false;
                        }
                        break;
                }
                if (MShare.g_MySelf == this)
                {
                    if (Msg.Ident == Grobal2.CM_WALK)
                    {
                        if (!ClMain.g_PlayScene.CanWalk(Msg.X, Msg.Y))
                        {
                            return;
                        }
                    }
                    if (Msg.Ident == Grobal2.CM_RUN)
                    {
                        if (!ClMain.g_PlayScene.CanRun(MShare.g_MySelf.m_nCurrX, MShare.g_MySelf.m_nCurrY, Msg.X, Msg.Y))
                        {
                            return;
                        }
                    }
                    if (Msg.Ident == Grobal2.CM_HORSERUN)
                    {
                        if (!ClMain.g_PlayScene.CanRun(MShare.g_MySelf.m_nCurrX, MShare.g_MySelf.m_nCurrY, Msg.X, Msg.Y))
                        {
                            return;
                        }
                    }
                    switch (Msg.Ident)
                    {
                        case Grobal2.CM_TURN:
                        case Grobal2.CM_WALK:
                        case Grobal2.CM_SITDOWN:
                        case Grobal2.CM_RUN:
                        case Grobal2.CM_HIT:
                        case Grobal2.CM_HEAVYHIT:
                        case Grobal2.CM_BIGHIT:
                        case Grobal2.CM_POWERHIT:
                        case Grobal2.CM_LONGHIT:
                        case Grobal2.CM_WIDEHIT:
                        case Grobal2.CM_CRSHIT:
                            if (Msg.Ident == Grobal2.CM_POWERHIT)
                            {
                                Msg.Saying = ClMain.GetMagicLv(this, 7);
                            }
                            else if (Msg.Ident == Grobal2.CM_LONGHIT)
                            {
                                Msg.Saying = ClMain.GetMagicLv(this, 12);
                            }
                            else if (Msg.Ident == Grobal2.CM_WIDEHIT)
                            {
                                Msg.Saying = ClMain.GetMagicLv(this, 25);
                            }
                            else if (Msg.Ident == Grobal2.CM_PURSUEHIT)
                            {
                                Msg.Saying = ClMain.GetMagicLv(this, 56);
                            }
                            RealActionMsg = Msg;
                            Msg.Ident = Msg.Ident - 3000;
                            break;
                        case Grobal2.CM_HORSERUN:
                            RealActionMsg = Msg;
                            Msg.Ident = Grobal2.SM_HORSERUN;
                            break;
                        case Grobal2.CM_THROW:
                            if (m_nFeature != 0)
                            {
                                m_nTargetX = (Msg.Feature as TActor).m_nCurrX;
                                m_nTargetY = (Msg.Feature as TActor).m_nCurrY;
                                m_nTargetRecog = (Msg.Feature as TActor).m_nRecogId;
                            }
                            RealActionMsg = Msg;
                            Msg.Ident = Grobal2.SM_THROW;
                            break;
                        case Grobal2.CM_FIREHIT:
                            Msg.Saying = ClMain.GetMagicLv(this, 26);
                            RealActionMsg = Msg;
                            Msg.Ident = Grobal2.SM_FIREHIT;
                            break;
                        case Grobal2.CM_SPELL:
                            if (MShare.g_MagicTarget != null)
                            {
                                Msg.Dir = ClFunc.GetFlyDirection(m_nCurrX, m_nCurrY, MShare.g_MagicTarget.m_nCurrX, MShare.g_MagicTarget.m_nCurrY);
                            }
                            RealActionMsg = Msg;
                            UseMagic = (TUseMagicInfo)Msg.Feature;
                            RealActionMsg.Dir = UseMagic.MagicSerial;
                            Msg.Ident = Msg.Ident - 3000;
                            Msg.X = ClMain.GetMagicLv(this, UseMagic.MagicSerial);
                            Msg.Y = m_btPoisonDecHealth;
                            break;
                    }
                    m_nOldx = m_nCurrX;
                    m_nOldy = m_nCurrY;
                    m_nOldDir = m_btDir;
                }
                switch (Msg.Ident)
                {
                    case Grobal2.SM_STRUCK:
                        m_nMagicStruckSound = Msg.X;
                        n = HUtil32.Round(200 - m_Abil.Level * 5);
                        if (n > 80)
                        {
                            m_dwStruckFrameTime = n;
                        }
                        else
                        {
                            m_dwStruckFrameTime = 80;
                        }
                        m_dwLastStruckTime = MShare.GetTickCount();
                        break;
                    case Grobal2.SM_SPELL:
                        m_btDir = Msg.Dir;
                        UseMagic = (TUseMagicInfo)Msg.Feature;
                        if (UseMagic != null)
                        {
                            m_CurMagic = UseMagic;
                            m_CurMagic.ServerMagicCode = -1;
                            m_CurMagic.targx = Msg.X;
                            m_CurMagic.targy = Msg.Y;
                            m_CurMagic.spelllv = Msg.X;
                            m_CurMagic.Poison = Msg.Y;
                            Dispose(UseMagic);
                            if (m_CurMagic.EffectNumber >= 60 && m_CurMagic.EffectNumber <= 66)
                            {
                                MShare.g_SeriesSkillFire = false;
                                MShare.g_SeriesSkillFire_100 = false;
                            }
                        }
                        break;
                    case Grobal2.SM_FIREHIT:
                    case Grobal2.SM_POWERHIT:
                    case Grobal2.SM_LONGHIT:
                    case Grobal2.SM_WIDEHIT:
                    case Grobal2.SM_PURSUEHIT:
                        m_nCurrX = Msg.X;
                        m_nCurrY = Msg.Y;
                        m_btDir = Msg.Dir;
                        m_CurMagic.magfirelv = Msg.Saying;
                        break;
                    default:
                        m_nCurrX = Msg.X;
                        m_nCurrY = Msg.Y;
                        m_btDir = Msg.Dir;
                        break;
                }
                m_nCurrentAction = Msg.Ident;
                CalcActorFrame();
            }
            else if (Msg.Ident == Grobal2.SM_SKELETON)
            {
                m_nCurrentAction = Msg.Ident;
                CalcActorFrame();
                m_boSkeleton = true;
            }
            if ((Msg.Ident == Grobal2.SM_DEATH) || (Msg.Ident == Grobal2.SM_NOWDEATH))
            {
                m_boDeath = true;
                if (HUtil32.HiByte(Msg.Dir) != 0)
                {
                    m_boItemExplore = true;
                }
                ClMain.g_PlayScene.ActorDied(this);
            }
        }

        private bool GetMessage(TChrMsg ChrMsg)
        {
            bool result;
            int i;
            TChrMsg Msg;
            result = false;
            m_MsgList.__Lock();
            try
            {
                i = 0;
                while (m_MsgList.Count > i)
                {
                    Msg = m_MsgList[i];
                    if ((Msg.dwDelay != 0) && (MShare.GetTickCount() < Msg.dwDelay))
                    {
                        i++;
                        continue;
                    }
                    ChrMsg = Msg;
                    Dispose(Msg);
                    m_MsgList.RemoveAt(i);
                    result = true;
                    break;
                }
            }
            finally
            {
                m_MsgList.UnLock();
            }
            return result;
        }

        public void ProcMsg()
        {
            while ((m_nCurrentAction == 0) && GetMessage(m_ChrMsg))
            {
                switch (m_ChrMsg.Ident)
                {
                    case Grobal2.SM_STRUCK:
                        m_nHiterCode = m_ChrMsg.Sound;
                        ReadyAction(m_ChrMsg);
                        break;
                    case Grobal2.SM_DEATH:
                    case Grobal2.SM_NOWDEATH:
                    case Grobal2.SM_SKELETON:
                    case Grobal2.SM_ALIVE:
                    case Grobal2.SM_ACTION_MIN:
                    case Grobal2.SM_ACTION2_MIN:
                    case 3000:
                        ReadyAction(m_ChrMsg);
                        break;
                    case Grobal2.SM_SPACEMOVE_SHOW:
                        m_ChrMsg.Ident = Grobal2.SM_TURN;
                        ReadyAction(m_ChrMsg);
                        break;
                    case Grobal2.SM_SPACEMOVE_SHOW2:
                        m_ChrMsg.Ident = Grobal2.SM_TURN;
                        ReadyAction(m_ChrMsg);
                        break;
                }
            }
        }

        public void ProcHurryMsg()
        {
            int n;
            TChrMsg Msg;
            bool fin;
            m_MsgList.__Lock();
            try
            {
                n = 0;
                while (true)
                {
                    if (m_MsgList.Count <= n)
                    {
                        break;
                    }
                    Msg = (TChrMsg)m_MsgList[n];
                    fin = false;
                    switch (Msg.Ident)
                    {
                        case Grobal2.SM_MAGICFIRE:
                            if (m_CurMagic.ServerMagicCode != 0)
                            {
                                m_CurMagic.ServerMagicCode = 255;
                                m_CurMagic.target = Msg.X;
                                if (Msg.Y >= 0 && Msg.Y <= magiceff.Units.magiceff.MAXMAGICTYPE - 1)
                                {
                                    m_CurMagic.EffectType = (TMagicType)Msg.Y;
                                }
                                m_CurMagic.EffectNumber = Msg.Dir % 255;
                                m_CurMagic.targx = Msg.Feature;
                                m_CurMagic.targy = Msg.State;
                                m_CurMagic.magfirelv = Msg.Saying;
                                m_CurMagic.Recusion = true;
                                fin = true;
                            }
                            break;
                        case Grobal2.SM_MAGICFIRE_FAIL:
                            if (m_CurMagic.ServerMagicCode != 0)
                            {
                                m_CurMagic.ServerMagicCode = 0;
                                fin = true;
                            }
                            break;
                    }
                    if (fin)
                    {
                        Dispose((TChrMsg)m_MsgList[n]);
                        m_MsgList.RemoveAt(n);
                    }
                    else
                    {
                        n++;
                    }
                }
            }
            finally
            {
                m_MsgList.UnLock();
            }
        }

        public bool IsIdle()
        {
            return (m_nCurrentAction == 0) && (m_MsgList.Count == 0); ;
        }

        public bool ActionFinished()
        {
            bool result;
            if ((m_nCurrentAction == 0) || (m_nCurrentFrame >= m_nEndFrame))
            {
                result = true;
            }
            else
            {
                result = false;
            }
            return result;
        }

        public int CanWalk()
        {
            int result;
            if (MShare.GetTickCount() - MShare.g_dwLatestSpellTick < MShare.g_dwMagicPKDelayTime)
            {
                result = -1;
            }
            else
            {
                result = 1;
            }
            return result;
        }

        public int CanRun()
        {
            int result = 1;
            if (m_Abil.HP < Actor.RUN_MINHEALTH)
            {
                result = -1;
            }
            return result;
        }

        public bool Strucked()
        {
            bool result = false;
            m_MsgList.__Lock();
            try
            {
                for (var i = 0; i < m_MsgList.Count; i++)
                {
                    if (((TChrMsg)m_MsgList[i]).Ident == Grobal2.SM_STRUCK)
                    {
                        result = true;
                        break;
                    }
                }
            }
            finally
            {
                m_MsgList.UnLock();
            }
            return result;
        }

        // 阴影闪烁修复  直接替换函数
        public void Shift(int dir, int step, int cur, int max)
        {
            int ss;
            int v;
            int funx;
            int funy;
            int unx = Grobal2.UNITX * step;
            int uny = Grobal2.UNITY * step;
            if (cur > max)
            {
                cur = max;
            }
            m_nRx = m_nCurrX;
            m_nRy = m_nCurrY;
            switch (dir)
            {
                case Grobal2.DR_UP:
                    ss = HUtil32.Round((max - cur) / max) * step;
                    m_nRy = m_nCurrY + ss;
                    if (ss == step)
                    {
                        funx = -HUtil32.Round(uny / max * cur);
                        if ((funx % 2) != 0)
                        {
                            funx = funx + 1;
                        }
                        m_nShiftY = funx;
                    }
                    else
                    {
                        funx = HUtil32.Round(uny / max * (max - cur));
                        if ((funx % 2) != 0)
                        {
                            funx = funx + 1;
                        }
                        m_nShiftY = funx;
                    }
                    break;
                case Grobal2.DR_UPRIGHT:
                    if (max >= 6)
                    {
                        v = 2;
                    }
                    else
                    {
                        v = 0;
                    }
                    ss = HUtil32.Round((max - cur + v) / max) * step;
                    m_nRx = m_nCurrX - ss;
                    m_nRy = m_nCurrY + ss;
                    if (ss == step)
                    {
                        funx = HUtil32.Round(unx / max * cur);
                        if ((funx % 2) != 0)
                        {
                            funx = funx + 1;
                        }
                        m_nShiftX = funx;
                        funy = -HUtil32.Round(uny / max * cur);
                        if ((funy % 2) != 0)
                        {
                            funy = funy + 1;
                        }
                        m_nShiftY = funy;
                    }
                    else
                    {
                        funx = -HUtil32.Round(unx / max * (max - cur));
                        if ((funx % 2) != 0)
                        {
                            funx = funx + 1;
                        }
                        m_nShiftX = funx;
                        funy = HUtil32.Round(uny / max * (max - cur));
                        if ((funy % 2) != 0)
                        {
                            funy = funy + 1;
                        }
                        m_nShiftY = funy;
                    }
                    break;
                case Grobal2.DR_RIGHT:
                    ss = HUtil32.Round((max - cur) / max) * step;
                    m_nRx = m_nCurrX - ss;
                    if (ss == step)
                    {
                        m_nShiftX = HUtil32.Round(unx / max * cur);
                    }
                    else
                    {
                        m_nShiftX = -HUtil32.Round(unx / max * (max - cur));
                    }
                    m_nShiftY = 0;
                    break;
                case Grobal2.DR_DOWNRIGHT:
                    if (max >= 6)
                    {
                        v = 2;
                    }
                    else
                    {
                        v = 0;
                    }
                    ss = HUtil32.Round((max - cur - v) / max) * step;
                    m_nRx = m_nCurrX - ss;
                    m_nRy = m_nCurrY - ss;
                    if (ss == step)
                    {
                        funx = HUtil32.Round(unx / max * cur);
                        if ((funx % 2) != 0)
                        {
                            funx = funx + 1;
                        }
                        m_nShiftX = funx;
                        funy = HUtil32.Round(uny / max * cur);
                        if ((funy % 2) != 0)
                        {
                            funy = funy + 1;
                        }
                        m_nShiftY = funy;
                    }
                    else
                    {
                        funx = -HUtil32.Round(unx / max * (max - cur));
                        if ((funx % 2) != 0)
                        {
                            funx = funx + 1;
                        }
                        m_nShiftX = funx;
                        funy = -HUtil32.Round(uny / max * (max - cur));
                        if ((funy % 2) != 0)
                        {
                            funy = funy + 1;
                        }
                        m_nShiftY = funy;
                    }
                    break;
                case Grobal2.DR_DOWN:
                    if (max >= 6)
                    {
                        v = 1;
                    }
                    else
                    {
                        v = 0;
                    }
                    ss = HUtil32.Round((max - cur - v) / max) * step;
                    m_nShiftX = 0;
                    m_nRy = m_nCurrY - ss;
                    if (ss == step)
                    {
                        funy = HUtil32.Round(uny / max * cur);
                        if ((funy % 2) != 0)
                        {
                            funy = funy + 1;
                        }
                        m_nShiftY = funy;
                    }
                    else
                    {
                        funy = -HUtil32.Round(uny / max * (max - cur));
                        if ((funy % 2) != 0)
                        {
                            funy = funy + 1;
                        }
                        m_nShiftY = funy;
                    }
                    break;
                case Grobal2.DR_DOWNLEFT:
                    if (max >= 6)
                    {
                        v = 2;
                    }
                    else
                    {
                        v = 0;
                    }
                    ss = HUtil32.Round((max - cur - v) / max) * step;
                    m_nRx = m_nCurrX + ss;
                    m_nRy = m_nCurrY - ss;
                    if (ss == step)
                    {
                        funx = -HUtil32.Round(unx / max * cur);
                        if ((funx % 2) != 0)
                        {
                            funx = funx + 1;
                        }
                        m_nShiftX = funx;
                        funy = HUtil32.Round(uny / max * cur);
                        if ((funy % 2) != 0)
                        {
                            funy = funy + 1;
                        }
                        m_nShiftY = funy;
                    }
                    else
                    {
                        funx = HUtil32.Round(unx / max * (max - cur));
                        if ((funx % 2) != 0)
                        {
                            funx = funx + 1;
                        }
                        m_nShiftX = funx;
                        funy = -HUtil32.Round(uny / max * (max - cur));
                        if ((funy % 2) != 0)
                        {
                            funy = funy + 1;
                        }
                        m_nShiftY = funy;
                    }
                    break;
                case Grobal2.DR_LEFT:
                    ss = HUtil32.Round((max - cur) / max) * step;
                    m_nRx = m_nCurrX + ss;
                    if (ss == step)
                    {
                        m_nShiftX = -HUtil32.Round(unx / max * cur);
                    }
                    else
                    {
                        m_nShiftX = HUtil32.Round(unx / max * (max - cur));
                    }
                    m_nShiftY = 0;
                    break;
                case Grobal2.DR_UPLEFT:
                    if (max >= 6)
                    {
                        v = 2;
                    }
                    else
                    {
                        v = 0;
                    }
                    ss = HUtil32.Round((max - cur + v) / max) * step;
                    m_nRx = m_nCurrX + ss;
                    m_nRy = m_nCurrY + ss;
                    if (ss == step)
                    {
                        funx = -HUtil32.Round(unx / max * cur);
                        if ((funx % 2) != 0)
                        {
                            funx = funx + 1;
                        }
                        m_nShiftX = funx;
                        funy = -HUtil32.Round(uny / max * cur);
                        if ((funy % 2) != 0)
                        {
                            funy = funy + 1;
                        }
                        m_nShiftY = funy;
                    }
                    else
                    {
                        funx = HUtil32.Round(unx / max * (max - cur));
                        if ((funx % 2) != 0)
                        {
                            funx = funx + 1;
                        }
                        m_nShiftX = funx;
                        funy = HUtil32.Round(uny / max * (max - cur));
                        if ((funy % 2) != 0)
                        {
                            funy = funy + 1;
                        }
                        m_nShiftY = funy;
                    }
                    break;
            }
        }

        public virtual void FeatureChanged()
        {
            int haircount;
            switch (m_btRace)
            {
                case 0:
                    m_btHair = Grobal2.HAIRfeature(m_nFeature);
                    m_btDress = Grobal2.DRESSfeature(m_nFeature);
                    if (m_btDress >= 24 && m_btDress <= 27)
                    {
                        m_btDress = 18 + m_btSex;
                    }
                    m_btWeapon = Grobal2.WEAPONfeature(m_nFeature);
                    m_btHorse = Grobal2.Horsefeature(m_nFeatureEx);
                    m_btWeaponEffect = m_btHorse;
                    m_btHorse = 0;
                    m_btEffect = Grobal2.Effectfeature(m_nFeatureEx);
                    m_nBodyOffset = Actor.HUMANFRAME * m_btDress;
                    if (m_btHair >= 10)
                    {
                        m_btHairEx = m_btHair / 10;
                        m_btHair = m_btHair % 10;
                    }
                    else
                    {
                        m_btHairEx = 0;
                    }
                    if (m_btHairEx > 0)
                    {
                        if (m_btHairEx > haircount)
                        {
                            m_btHairEx = haircount;
                        }
                        m_nHairOffsetEx = Actor.HUMANFRAME * ((m_btHairEx - 1) * 2 + m_btSex);
                    }
                    else
                    {
                        m_nHairOffsetEx = -1;
                    }
                    if (m_btHair > haircount - 1)
                    {
                        m_btHair = haircount - 1;
                    }
                    m_btHair = m_btHair * 2;
                    if (m_btHair > 1)
                    {
                        m_nHairOffset = Actor.HUMANFRAME * (m_btHair + m_btSex);
                    }
                    else
                    {
                        m_nHairOffset = -1;
                    }
                    m_nWeaponOffset = Actor.HUMANFRAME * m_btWeapon;
                    if (m_btEffect != 0)
                    {
                        if (m_btEffect == 50)
                        {
                            m_nHumWinOffset = 352;
                        }
                        else
                        {
                            m_nHumWinOffset = (m_btEffect - 1) * Actor.HUMANFRAME;
                        }
                    }
                    break;
                case 50:
                    break;
                default:
                    m_wAppearance = Grobal2.APPRfeature(m_nFeature);
                    m_nBodyOffset = Actor.GetOffset(m_wAppearance);
                    break;
            }
        }

        public virtual int light()
        {
            return m_nChrLight;
        }

        public int CharWidth()
        {
            return 48;
        }

        public int CharHeight()
        {
            return 70;
        }

        public bool CheckSelect(int dx, int dy)
        {
            return false;
        }

        public virtual int GetDefaultFrame(bool wmode)
        {
            int result;
            int cf;
            TMonsterAction pm;
            result = 0;
            pm = Actor.GetRaceByPM(m_btRace, m_wAppearance);
            if (pm == null)
            {
                return result;
            }
            if (m_boDeath)
            {
                if (m_boSkeleton)
                {
                    result = pm.ActDeath.start;
                }
                else
                {
                    result = pm.ActDie.start + m_btDir * (pm.ActDie.frame + pm.ActDie.skip) + (pm.ActDie.frame - 1);
                }
            }
            else
            {
                m_nDefFrameCount = pm.ActStand.frame;
                if (m_nCurrentDefFrame < 0)
                {
                    cf = 0;
                }
                else if (m_nCurrentDefFrame >= pm.ActStand.frame)
                {
                    cf = 0;
                }
                else
                {
                    cf = m_nCurrentDefFrame;
                }
                result = pm.ActStand.start + m_btDir * (pm.ActStand.frame + pm.ActStand.skip) + cf;
            }
            return result;
        }

        public virtual void DefaultMotion()
        {
            m_boReverseFrame = false;
            if (m_boWarMode)
            {
                if (MShare.GetTickCount() - m_dwWarModeTime > 4 * 1000)
                {
                    m_boWarMode = false;
                }
            }
            m_nCurrentFrame = GetDefaultFrame(m_boWarMode);
            Shift(m_btDir, 0, 1, 1);
        }

        public virtual void RunFrameAction(int frame)
        {

        }

        public virtual void ActionEnded()
        {

        }

        public virtual void ReadyNextAction()
        {

        }

        public bool Run_MagicTimeOut()
        {
            bool result;
            if (this == MShare.g_MySelf)
            {
                result = MShare.GetTickCount() - m_dwWaitMagicRequest > 1800;
            }
            else
            {
                result = MShare.GetTickCount() - m_dwWaitMagicRequest > 900;
            }
            if (result)
            {
                m_CurMagic.ServerMagicCode = 0;
            }
            return result;
        }

        public virtual void Run()
        {
            int prv;
            long dwFrameTimetime;
            bool boFly;
            if ((m_nCurrentAction == Grobal2.SM_WALK) || (m_nCurrentAction == Grobal2.SM_BACKSTEP) || (m_nCurrentAction == Grobal2.SM_RUN) || (m_nCurrentAction == Grobal2.SM_HORSERUN) || (m_nCurrentAction == Grobal2.SM_RUSH) || (m_nCurrentAction == Grobal2.SM_RUSHEX) || (m_nCurrentAction == Grobal2.SM_RUSHKUNG))
            {
                return;
            }
            m_boMsgMuch = false;
            if (this != MShare.g_MySelf)
            {
                if (m_MsgList.Count >= 2)
                {
                    m_boMsgMuch = true;
                }
            }
            RunFrameAction(m_nCurrentFrame - m_nStartFrame);
            prv = m_nCurrentFrame;
            if (m_nCurrentAction != 0)
            {
                if ((m_nCurrentFrame < m_nStartFrame) || (m_nCurrentFrame > m_nEndFrame))
                {
                    m_nCurrentFrame = m_nStartFrame;
                }
                if ((this != MShare.g_MySelf) && m_boUseMagic)
                {
                    dwFrameTimetime = HUtil32.Round(m_dwFrameTime / 1.8);
                }
                else if (m_boMsgMuch)
                {
                    dwFrameTimetime = HUtil32.Round(m_dwFrameTime * 2 / 3);
                }
                else
                {
                    dwFrameTimetime = m_dwFrameTime;
                }
                if (MShare.GetTickCount() - m_dwStartTime > dwFrameTimetime)
                {
                    if (m_nCurrentFrame < m_nEndFrame)
                    {
                        if (m_boUseMagic)
                        {
                            if ((m_nCurEffFrame == m_nSpellFrame - 2) || Run_MagicTimeOut())
                            {
                                if ((m_CurMagic.ServerMagicCode >= 0) || Run_MagicTimeOut())
                                {
                                    m_nCurrentFrame++;
                                    m_nCurEffFrame++;
                                    m_dwStartTime = MShare.GetTickCount();
                                }
                            }
                            else
                            {
                                if (m_nCurrentFrame < m_nEndFrame - 1)
                                {
                                    m_nCurrentFrame++;
                                }
                                m_nCurEffFrame++;
                                m_dwStartTime = MShare.GetTickCount();
                            }
                        }
                        else
                        {
                            m_nCurrentFrame++;
                            m_dwStartTime = MShare.GetTickCount();
                        }
                    }
                    else
                    {
                        if (m_boDelActionAfterFinished)
                        {
                            m_boDelActor = true;
                        }
                        if (this == MShare.g_MySelf)
                        {
                            if (ClMain.frmMain.ServerAcceptNextAction())
                            {
                                ActionEnded();
                                m_nCurrentAction = 0;
                                m_boUseMagic = false;
                                if (m_btRace != 50)
                                {
                                    m_boUseEffect = false;
                                    m_boHitEffect = false;
                                }
                            }
                        }
                        else
                        {
                            ActionEnded();
                            m_nCurrentAction = 0;
                            m_boUseMagic = false;
                            if (m_btRace != 50)
                            {
                                m_boUseEffect = false;
                                m_boHitEffect = false;
                            }
                        }
                    }
                    if (m_boUseMagic)
                    {
                        if (m_nCurEffFrame == m_nSpellFrame - 1)
                        {
                            if (m_CurMagic.ServerMagicCode > 0)
                            {
                                ClMain.g_PlayScene.NewMagic(this, m_CurMagic.ServerMagicCode, m_CurMagic.EffectNumber, m_nCurrX, m_nCurrY, m_CurMagic.targx, m_CurMagic.targy, m_CurMagic.target, m_CurMagic.EffectType, m_CurMagic.Recusion, m_CurMagic.anitime, ref boFly, m_CurMagic.magfirelv);
                            }
                            m_CurMagic.ServerMagicCode = 0;
                        }
                    }
                }
                if (new ArrayList(new int[] { 0, 1, 43 }).Contains(m_wAppearance))
                {
                    m_nCurrentDefFrame = -10;
                }
                else
                {
                    m_nCurrentDefFrame = 0;
                }
                m_dwDefFrameTime = MShare.GetTickCount();
            }
            else if ((MShare.GetTickCount() - m_dwSmoothMoveTime) > 200)
            {
                if (MShare.GetTickCount() - m_dwDefFrameTime > 500)
                {
                    m_dwDefFrameTime = MShare.GetTickCount();
                    m_nCurrentDefFrame++;
                    if (m_nCurrentDefFrame >= m_nDefFrameCount)
                    {
                        m_nCurrentDefFrame = 0;
                    }
                }
                DefaultMotion();
            }
            if (prv != m_nCurrentFrame)
            {
                m_dwLoadSurfaceTime = MShare.GetTickCount();
            }
        }

        public bool Move()
        {
            bool result;
            int prv;
            int curstep;
            int maxstep;
            bool fastmove;
            bool normmove;
            result = false;
            fastmove = false;
            normmove = false;
            if (m_nCurrentAction == Grobal2.SM_BACKSTEP)
            {
                fastmove = true;
            }
            if ((m_nCurrentAction == Grobal2.SM_RUSH) || (m_nCurrentAction == Grobal2.SM_RUSHEX) || (m_nCurrentAction == Grobal2.SM_RUSHKUNG))
            {
                normmove = true;
            }
            if (!fastmove && !normmove)
            {
                m_boMoveSlow = false;
                m_boAttackSlow = false;
                m_nMoveSlowLevel = 0;
                if (m_nState & 0x10000000 != 0)
                {
                    m_nMoveSlowLevel = 1;
                    m_boMoveSlow = true;
                }
                if (m_boMoveSlow && (m_nSkipTick < m_nMoveSlowLevel))
                {
                    m_nSkipTick++;
                    return result;
                }
                else
                {
                    m_nSkipTick = 0;
                }
                if ((m_btRace == 0) && (this == MShare.g_MySelf))
                {
                    if (new ArrayList(new int[] { 5, 9, 11, 13 }).Contains(m_nCurrentAction))
                    {
                        switch (m_nCurrentFrame - m_nStartFrame)
                        {
                            case 1:
                                break;
                            case 4:
                                break;
                        }
                    }
                }
            }
            result = false;
            m_boMsgMuch = (this != MShare.g_MySelf) && (m_MsgList.Count >= 2);
            prv = m_nCurrentFrame;
            if ((m_nCurrentAction == Grobal2.SM_WALK) || (m_nCurrentAction == Grobal2.SM_RUN) || (m_nCurrentAction == Grobal2.SM_HORSERUN) || (m_nCurrentAction == Grobal2.SM_RUSH) || (m_nCurrentAction == Grobal2.SM_RUSHKUNG))
            {
                if ((m_nCurrentFrame < m_nStartFrame) || (m_nCurrentFrame > m_nEndFrame))
                {
                    m_nCurrentFrame = m_nStartFrame - 1;
                }
                if (m_nCurrentFrame < m_nEndFrame)
                {
                    m_nCurrentFrame++;
                    if (m_boMsgMuch && !normmove)
                    {
                        if (m_nCurrentFrame < m_nEndFrame)
                        {
                            m_nCurrentFrame++;
                        }
                    }
                    curstep = m_nCurrentFrame - m_nStartFrame + 1;
                    maxstep = m_nEndFrame - m_nStartFrame + 1;
                    Shift(m_btDir, m_nMoveStep, curstep, maxstep);
                }
                if (m_nCurrentFrame >= m_nEndFrame)
                {
                    if (this == MShare.g_MySelf)
                    {
                        if (ClMain.frmMain.ServerAcceptNextAction())
                        {
                            m_nCurrentAction = 0;
                            m_boLockEndFrame = true;
                            m_dwSmoothMoveTime = MShare.GetTickCount();
                            m_boUseCboLib = false;
                        }
                    }
                    else
                    {
                        m_nCurrentAction = 0;
                        m_boLockEndFrame = true;
                        m_dwSmoothMoveTime = MShare.GetTickCount();
                        m_boUseCboLib = false;
                    }
                }
                if ((m_nCurrentAction == Grobal2.SM_RUSH) || (m_nCurrentAction == Grobal2.SM_RUSHEX))
                {
                    if (this == MShare.g_MySelf)
                    {
                        MShare.g_dwDizzyDelayStart = MShare.GetTickCount();
                        MShare.g_dwDizzyDelayTime = 300;
                    }
                }
                if (m_nCurrentAction == Grobal2.SM_RUSHKUNG)
                {
                    if (m_nCurrentFrame >= m_nEndFrame - 3)
                    {
                        m_nCurrX = m_nActBeforeX;
                        m_nCurrY = m_nActBeforeY;
                        m_nRx = m_nCurrX;
                        m_nRy = m_nCurrY;
                        m_nCurrentAction = 0;
                        m_boUseCboLib = false;
                        m_boLockEndFrame = true;
                    }
                }
                result = true;
            }
            if (m_nCurrentAction == Grobal2.SM_RUSHEX)
            {
                if ((m_nCurrentFrame < m_nStartFrame) || (m_nCurrentFrame > m_nEndFrame))
                {
                    m_nCurrentFrame = m_nStartFrame - 1;
                }
                if (m_nCurrentFrame < m_nEndFrame)
                {
                    m_nCurrentFrame++;
                    if (m_boMsgMuch && !normmove)
                    {
                        if (m_nCurrentFrame < m_nEndFrame)
                        {
                            m_nCurrentFrame++;
                        }
                    }
                    curstep = m_nCurrentFrame - m_nStartFrame + 1;
                    maxstep = m_nEndFrame - m_nStartFrame + 1;
                    Shift(m_btDir, m_nMoveStep, curstep, maxstep);
                    ReadyNextAction();
                    ClMain.frmMain.LastHitTick = MShare.GetTickCount();
                }
                else if (m_nCurrentFrame >= m_nEndFrame)
                {
                    if (this == MShare.g_MySelf)
                    {
                        ActionEnded();
                        m_nCurrentAction = 0;
                        m_boLockEndFrame = true;
                        m_dwSmoothMoveTime = MShare.GetTickCount() - 200;
                        m_boUseCboLib = false;
                    }
                    else
                    {
                        ActionEnded();
                        m_nCurrentAction = 0;
                        m_boLockEndFrame = true;
                        m_dwSmoothMoveTime = MShare.GetTickCount() - 200;
                        m_boUseCboLib = false;
                    }
                    ClMain.frmMain.LastHitTick = 0;
                }
                if (this == MShare.g_MySelf)
                {
                    MShare.g_dwDizzyDelayStart = MShare.GetTickCount();
                    MShare.g_dwDizzyDelayTime = 300;
                }
                result = true;
            }
            if (m_nCurrentAction == Grobal2.SM_BACKSTEP)
            {
                if ((m_nCurrentFrame > m_nEndFrame) || (m_nCurrentFrame < m_nStartFrame))
                {
                    m_nCurrentFrame = m_nEndFrame + 1;
                }
                if (m_nCurrentFrame > m_nStartFrame)
                {
                    m_nCurrentFrame -= 1;
                    if (m_boMsgMuch || fastmove)
                    {
                        if (m_nCurrentFrame > m_nStartFrame)
                        {
                            m_nCurrentFrame -= 1;
                        }
                    }
                    curstep = m_nEndFrame - m_nCurrentFrame + 1;
                    maxstep = m_nEndFrame - m_nStartFrame + 1;
                    Shift(ClFunc.GetBack(m_btDir), m_nMoveStep, curstep, maxstep);
                }
                if (m_nCurrentFrame <= m_nStartFrame)
                {
                    if (this == MShare.g_MySelf)
                    {
                        m_nCurrentAction = 0;
                        m_boLockEndFrame = true;
                        m_dwSmoothMoveTime = MShare.GetTickCount();
                        MShare.g_dwDizzyDelayStart = MShare.GetTickCount();
                        MShare.g_dwDizzyDelayTime = 1000;
                    }
                    else
                    {
                        m_nCurrentAction = 0;
                        m_boLockEndFrame = true;
                        m_dwSmoothMoveTime = MShare.GetTickCount();
                    }
                }
                result = true;
            }
            if (prv != m_nCurrentFrame)
            {
                m_dwLoadSurfaceTime = MShare.GetTickCount();
                LoadSurface();
            }
            return result;
        }

        public void MoveFail()
        {
            m_nCurrentAction = 0;
            m_boLockEndFrame = true;
            MShare.g_MySelf.m_nCurrX = m_nOldx;
            MShare.g_MySelf.m_nCurrY = m_nOldy;
            MShare.g_MySelf.m_btDir = m_nOldDir;
            CleanUserMsgs();
        }

        public bool CanCancelAction()
        {
            bool result;
            result = false;
            if (m_nCurrentAction == Grobal2.SM_HIT)
            {
                if (!m_boUseEffect)
                {
                    result = true;
                }
            }
            return result;
        }

        public void CancelAction()
        {
            m_nCurrentAction = 0;
            m_boLockEndFrame = true;
        }

        public void CleanCharMapSetting(int X, int Y)
        {
            MShare.g_MySelf.m_nCurrX = X;
            MShare.g_MySelf.m_nCurrY = Y;
            MShare.g_MySelf.m_nRx = X;
            MShare.g_MySelf.m_nRy = Y;
            m_nOldx = X;
            m_nOldy = Y;
            m_nCurrentAction = 0;
            m_nCurrentFrame = -1;
            CleanUserMsgs();
        }

        public void StruckShowDamage(string Str)
        {
            int idx = 0;
            //m_StruckDamage.Add(Str, idx);
        }

        public void GetMoveHPShow(int nCount)
        {

        }

        public void Say(string Str)
        {

        }
    }
}

