using System.Collections;
using SystemModule;

namespace RobotSvr;

public class THumActor : TActor
{
    private readonly bool m_boHideWeapon = false;
    private bool m_boSSkill;
    private bool m_boWeaponEffect;
    public long m_dwFrameTick;
    private long m_dwWeaponpEffectTime;
    private int m_nCurBubbleStruck;
    private int m_nCurWeaponEffect;
    public int m_nFrame;
    public ArrayList m_SlaveObject;
    public TStallMgr m_StallMgr;

    public THumActor(RobotClient robotClient) : base(robotClient)
    {
        m_SlaveObject = new ArrayList();
        m_boWeaponEffect = false;
        m_boSSkill = false;
        m_dwFrameTime = 150;
        m_dwFrameTick = MShare.GetTickCount();
        m_nFrame = 0;
        m_nHumWinOffset = 0;
        m_nCboHumWinOffSet = 0;
    }

    protected void CalcActorWinFrame()
    {
        if (m_btEffect == 50)
            m_nCboHumWinOffSet = 352;
        else if (m_btEffect != 0) m_nCboHumWinOffSet = (m_btEffect - 1) * 2000;
    }

    public override void ActionEnded()
    {
        if (MShare.g_SeriesSkillFire)
        {
            if (MShare.g_MagicLockActor == null || MShare.g_MagicLockActor.m_boDeath)
            {
                MShare.g_SeriesSkillFire = false;
                MShare.g_SeriesSkillStep = 0;
            }

            if (m_boUseMagic && this == MShare.g_MySelf && MShare.g_MagicLockActor != null &&
                !MShare.g_MagicLockActor.m_boDeath && MShare.g_nCurrentMagic <= 3)
                if (m_nCurrentFrame - m_nStartFrame >= m_nSpellFrame - 1)
                {
                    if (MShare.g_MagicArr[0][MShare.g_SeriesSkillArr[MShare.g_nCurrentMagic]] != null)
                    {
                        // ClMain.frmMain.UseMagic(MShare.g_nMouseX, MShare.g_nMouseY, MShare.g_MagicArr[0][MShare.g_SeriesSkillArr[MShare.g_nCurrentMagic]], false, true);
                    }

                    MShare.g_nCurrentMagic++;
                    if (MShare.g_nCurrentMagic > HUtil32._MIN(3, MShare.g_SeriesSkillStep))
                    {
                        MShare.g_SeriesSkillFire = false;
                        MShare.g_SeriesSkillStep = 0;
                    }
                }
        }
    }

    public override void ReadyNextAction()
    {
        if (m_boUseCboLib && m_boHitEffect && this == MShare.g_MySelf && MShare.g_nCurrentMagic2 < 4)
            if (m_nCurrentFrame - m_nStartFrame == 2)
            {
                if (MShare.g_MagicArr[0][MShare.g_SeriesSkillArr[MShare.g_nCurrentMagic2]] != null)
                {
                    // ClMain.frmMain.UseMagic(MShare.g_nMouseX, MShare.g_nMouseY, MShare.g_MagicArr[0][MShare.g_SeriesSkillArr[MShare.g_nCurrentMagic2]], false, true);
                }

                MShare.g_nCurrentMagic2++;
                if (MShare.g_nCurrentMagic2 > HUtil32._MIN(4, MShare.g_SeriesSkillStep))
                    MShare.g_SeriesSkillFire = false;
            }
    }

    public override void CalcActorFrame()
    {
        m_boUseMagic = false;
        m_boNewMagic = false;
        m_boUseCboLib = false;
        m_boHitEffect = false;
        m_nHitEffectNumber = 0;
        m_nCurrentFrame = -1;
        //this.m_btHair = Grobal2.HAIRfeature(this.m_nFeature);
        //this.m_btDress = Grobal2.DRESSfeature(this.m_nFeature);
        //if (this.m_btDress >= 24 && this.m_btDress <= 27)
        //{
        //    this.m_btDress = 18 + this.m_btSex;
        //}
        //this.m_btWeapon = Grobal2.WEAPONfeature(this.m_nFeature);
        //this.m_btHorse = Grobal2.Horsefeature(this.m_nFeatureEx);
        //this.m_btWeaponEffect = this.m_btHorse;
        //this.m_btHorse = 0;
        //this.m_btEffect = Grobal2.Effectfeature(this.m_nFeatureEx);
        //this.m_nBodyOffset = Actor.HUMANFRAME * this.m_btDress;
        //this.m_nCboHairOffset = -1;
        //if (this.m_btHair >= 10)
        //{
        //    this.m_btHairEx = this.m_btHair / 10;
        //    this.m_btHair = this.m_btHair % 10;
        //}
        //else
        //{
        //    this.m_btHairEx = 0;
        //}
        //if (this.m_btHairEx == 0)
        //{
        //    if (this.m_btHair > haircount - 1)
        //    {
        //        this.m_btHair = haircount - 1;
        //    }
        //    nHairEx = (this.m_btHair - this.m_btSex) >> 1 + 1;
        //    if (nHairEx > haircount)
        //    {
        //        nHairEx = haircount;
        //    }
        //    this.m_nCboHairOffset = 2000 * ((nHairEx - 1) * 2 + this.m_btSex);
        //}
        //else if (this.m_btHairEx > 0)
        //{
        //    if (this.m_btHairEx > haircount)
        //    {
        //        this.m_btHairEx = haircount;
        //    }
        //    this.m_nHairOffsetEx = Actor.HUMANFRAME * ((this.m_btHairEx - 1) * 2 + this.m_btSex);
        //    nHairEx = (this.m_btHairEx - 1) * 2 + this.m_btSex;
        //    if (nHairEx > haircount)
        //    {
        //        nHairEx = haircount;
        //    }
        //    this.m_nCboHairOffset = 2000 * nHairEx;
        //}
        //else
        //{
        //    this.m_nHairOffsetEx = -1;
        //}
        //if (this.m_btHair > haircount - 1)
        //{
        //    this.m_btHair = haircount - 1;
        //}
        //this.m_btHair = this.m_btHair * 2;
        //if (this.m_btHair > 1)
        //{
        //    this.m_nHairOffset = Actor.HUMANFRAME * (this.m_btHair + this.m_btSex);
        //}
        //else
        //{
        //    this.m_nHairOffset = -1;
        //}
        //this.m_nWeaponOffset = Actor.HUMANFRAME * this.m_btWeapon;
        //if (this.m_btEffect == 50)
        //{
        //    this.m_nHumWinOffset = 352;
        //}
        //else if (this.m_btEffect != 0)
        //{
        //    this.m_nHumWinOffset = (this.m_btEffect - 1) * Actor.HUMANFRAME;
        //}
        switch (m_nCurrentAction)
        {
            case Grobal2.SM_TURN:
                m_nStartFrame = THumAction.HA.ActStand.start +
                                m_btDir * (THumAction.HA.ActStand.frame + THumAction.HA.ActStand.skip);
                m_nEndFrame = m_nStartFrame + THumAction.HA.ActStand.frame - 1;
                m_dwFrameTime = THumAction.HA.ActStand.ftime;
                m_dwStartTime = MShare.GetTickCount();
                m_nDefFrameCount = THumAction.HA.ActStand.frame;
                Shift(m_btDir, 0, 0, m_nEndFrame - m_nStartFrame + 1);
                if (m_fHideMode)
                {
                    m_fHideMode = false;
                    m_dwSmoothMoveTime = 0;
                    m_nCurrentAction = 0;
                }

                break;
            case Grobal2.SM_WALK:
            case Grobal2.SM_BACKSTEP:
                m_nStartFrame = THumAction.HA.ActWalk.start +
                                m_btDir * (THumAction.HA.ActWalk.frame + THumAction.HA.ActWalk.skip);
                m_nEndFrame = m_nStartFrame + THumAction.HA.ActWalk.frame - 1;
                m_dwFrameTime = THumAction.HA.ActWalk.ftime;
                m_dwStartTime = MShare.GetTickCount();
                m_nMaxTick = THumAction.HA.ActWalk.usetick;
                m_nCurTick = 0;
                m_nMoveStep = 1;
                if (m_nCurrentAction == Grobal2.SM_BACKSTEP)
                    Shift(ClFunc.GetBack(m_btDir), m_nMoveStep, 0, m_nEndFrame - m_nStartFrame + 1);
                else
                    Shift(m_btDir, m_nMoveStep, 0, m_nEndFrame - m_nStartFrame + 1);
                break;
            case Grobal2.SM_RUSH:
                if (m_nRushDir == 0)
                {
                    m_nRushDir = 1;
                    m_nStartFrame = THumAction.HA.ActRushLeft.start +
                                    m_btDir * (THumAction.HA.ActRushLeft.frame + THumAction.HA.ActRushLeft.skip);
                    m_nEndFrame = m_nStartFrame + THumAction.HA.ActRushLeft.frame - 1;
                    m_dwFrameTime = THumAction.HA.ActRushLeft.ftime;
                    m_dwStartTime = MShare.GetTickCount();
                    m_nMaxTick = THumAction.HA.ActRushLeft.usetick;
                    m_nCurTick = 0;
                    m_nMoveStep = 1;
                    Shift(m_btDir, 1, 0, m_nEndFrame - m_nStartFrame + 1);
                }
                else
                {
                    m_nRushDir = 0;
                    m_nStartFrame = THumAction.HA.ActRushRight.start +
                                    m_btDir * (THumAction.HA.ActRushRight.frame + THumAction.HA.ActRushRight.skip);
                    m_nEndFrame = m_nStartFrame + THumAction.HA.ActRushRight.frame - 1;
                    m_dwFrameTime = THumAction.HA.ActRushRight.ftime;
                    m_dwStartTime = MShare.GetTickCount();
                    m_nMaxTick = THumAction.HA.ActRushRight.usetick;
                    m_nCurTick = 0;
                    m_nMoveStep = 1;
                    Shift(m_btDir, 1, 0, m_nEndFrame - m_nStartFrame + 1);
                }

                break;
            case Grobal2.SM_RUSHKUNG:
                m_nStartFrame = THumAction.HA.ActRun.start +
                                m_btDir * (THumAction.HA.ActRun.frame + THumAction.HA.ActRun.skip);
                m_nEndFrame = m_nStartFrame + THumAction.HA.ActRun.frame - 1;
                m_dwFrameTime = THumAction.HA.ActRun.ftime;
                m_dwStartTime = MShare.GetTickCount();
                m_nMaxTick = THumAction.HA.ActRun.usetick;
                m_nCurTick = 0;
                m_nMoveStep = 1;
                Shift(m_btDir, m_nMoveStep, 0, m_nEndFrame - m_nStartFrame + 1);
                break;
            case Grobal2.SM_SITDOWN:
                m_nStartFrame = THumAction.HA.ActSitdown.start +
                                m_btDir * (THumAction.HA.ActSitdown.frame + THumAction.HA.ActSitdown.skip);
                m_nEndFrame = m_nStartFrame + THumAction.HA.ActSitdown.frame - 1;
                m_dwFrameTime = THumAction.HA.ActSitdown.ftime;
                m_dwStartTime = MShare.GetTickCount();
                break;
            case Grobal2.SM_RUN:
                m_nStartFrame = THumAction.HA.ActRun.start +
                                m_btDir * (THumAction.HA.ActRun.frame + THumAction.HA.ActRun.skip);
                m_nEndFrame = m_nStartFrame + THumAction.HA.ActRun.frame - 1;
                m_dwFrameTime = THumAction.HA.ActRun.ftime;
                m_dwStartTime = MShare.GetTickCount();
                m_nMaxTick = THumAction.HA.ActRun.usetick;
                m_nCurTick = 0;
                if (m_nCurrentAction == Grobal2.SM_RUN)
                    m_nMoveStep = 2;
                else
                    m_nMoveStep = 1;
                Shift(m_btDir, m_nMoveStep, 0, m_nEndFrame - m_nStartFrame + 1);
                break;
            case Grobal2.SM_HORSERUN:
                m_nStartFrame = THumAction.HA.ActRun.start +
                                m_btDir * (THumAction.HA.ActRun.frame + THumAction.HA.ActRun.skip);
                m_nEndFrame = m_nStartFrame + THumAction.HA.ActRun.frame - 1;
                m_dwFrameTime = THumAction.HA.ActRun.ftime;
                m_dwStartTime = MShare.GetTickCount();
                m_nMaxTick = THumAction.HA.ActRun.usetick;
                m_nCurTick = 0;
                if (m_nCurrentAction == Grobal2.SM_HORSERUN)
                    m_nMoveStep = 3;
                else
                    m_nMoveStep = 1;
                Shift(m_btDir, m_nMoveStep, 0, m_nEndFrame - m_nStartFrame + 1);
                break;
            case Grobal2.SM_THROW:
                m_nStartFrame = THumAction.HA.ActHit.start +
                                m_btDir * (THumAction.HA.ActHit.frame + THumAction.HA.ActHit.skip);
                m_nEndFrame = m_nStartFrame + THumAction.HA.ActHit.frame - 1;
                m_dwFrameTime = THumAction.HA.ActHit.ftime;
                m_dwStartTime = MShare.GetTickCount();
                m_boWarMode = true;
                m_dwWarModeTime = MShare.GetTickCount();
                m_boThrow = true;
                Shift(m_btDir, 0, 0, 1);
                break;
            case Grobal2.SM_HIT:
            case Grobal2.SM_POWERHIT:
            case Grobal2.SM_LONGHIT:
            case Grobal2.SM_WIDEHIT:
            case Grobal2.SM_FIREHIT:
            case Grobal2.SM_CRSHIT:
                m_nStartFrame = THumAction.HA.ActHit.start + m_btDir * (THumAction.HA.ActHit.frame + THumAction.HA.ActHit.skip);
                m_nEndFrame = m_nStartFrame + THumAction.HA.ActHit.frame - 1;
                m_dwFrameTime = THumAction.HA.ActHit.ftime;
                m_dwStartTime = MShare.GetTickCount();
                m_boWarMode = true;
                m_dwWarModeTime = MShare.GetTickCount();
                if (m_nCurrentAction == Grobal2.SM_POWERHIT)
                {
                    m_boHitEffect = true;
                    m_nMagLight = 2;
                    m_nHitEffectNumber = 1;
                    //switch (m_CurMagic.magfirelv / 4)
                    //{
                    //    case 1:
                    //        m_nHitEffectNumber += 101;
                    //        break;
                    //    case 2:
                    //        m_nHitEffectNumber += 201;
                    //        break;
                    //    case 3:
                    //        m_nHitEffectNumber += 301;
                    //        break;
                    //}
                }

                if (m_nCurrentAction == Grobal2.SM_LONGHIT)
                {
                    m_boHitEffect = true;
                    m_nMagLight = 2;
                    m_nHitEffectNumber = 2;
                    //switch (m_CurMagic.magfirelv / 4)
                    //{
                    //    case 1:
                    //        m_nHitEffectNumber += 101;
                    //        break;
                    //    case 2:
                    //        m_nHitEffectNumber += 201;
                    //        break;
                    //    case 3:
                    //        m_nHitEffectNumber += 301;
                    //        break;
                    //}
                }

                if (m_nCurrentAction == Grobal2.SM_WIDEHIT)
                {
                    m_boHitEffect = true;
                    m_nMagLight = 2;
                    m_nHitEffectNumber = 3;
                    //switch (m_CurMagic.magfirelv / 4)
                    //{
                    //    case 1:
                    //        m_nHitEffectNumber += 101;
                    //        break;
                    //    case 2:
                    //        m_nHitEffectNumber += 201;
                    //        break;
                    //    case 3:
                    //        m_nHitEffectNumber += 301;
                    //        break;
                    //}
                }

                if (m_nCurrentAction == Grobal2.SM_FIREHIT)
                {
                    m_boHitEffect = true;
                    m_nMagLight = 2;
                    m_nHitEffectNumber = 4;
                    //switch (m_CurMagic.magfirelv / 4)
                    //{
                    //    case 1:
                    //        m_nHitEffectNumber += 101;
                    //        break;
                    //    case 2:
                    //        m_nHitEffectNumber += 201;
                    //        break;
                    //    case 3:
                    //        m_nHitEffectNumber += 301;
                    //        break;
                    //}
                }

                if (m_nCurrentAction == Grobal2.SM_CRSHIT)
                {
                    m_boHitEffect = true;
                    m_nMagLight = 2;
                    m_nHitEffectNumber = 5;
                }

                Shift(m_btDir, 0, 0, 1);
                break;
            case Grobal2.SM_HEAVYHIT:
                m_nStartFrame = THumAction.HA.ActHeavyHit.start +
                                m_btDir * (THumAction.HA.ActHeavyHit.frame + THumAction.HA.ActHeavyHit.skip);
                m_nEndFrame = m_nStartFrame + THumAction.HA.ActHeavyHit.frame - 1;
                m_dwFrameTime = THumAction.HA.ActHeavyHit.ftime;
                m_dwStartTime = MShare.GetTickCount();
                m_boWarMode = true;
                m_dwWarModeTime = MShare.GetTickCount();
                Shift(m_btDir, 0, 0, 1);
                break;
            case Grobal2.SM_BIGHIT:
                m_nStartFrame = THumAction.HA.ActBigHit.start +
                                m_btDir * (THumAction.HA.ActBigHit.frame + THumAction.HA.ActBigHit.skip);
                m_nEndFrame = m_nStartFrame + THumAction.HA.ActBigHit.frame - 1;
                m_dwFrameTime = THumAction.HA.ActBigHit.ftime;
                m_dwStartTime = MShare.GetTickCount();
                m_boWarMode = true;
                m_dwWarModeTime = MShare.GetTickCount();
                Shift(m_btDir, 0, 0, 1);
                break;
            case Grobal2.SM_SPELL:
                if (m_CurMagic.EffectNumber >= 104 && m_CurMagic.EffectNumber <= 114)
                {
                    // DScreen.AddChatBoardString(format('EffectNumber=%d m_nEndFrame=%d', [m_CurMagic.EffectNumber, 1]), clWhite, clRed);
                    CalcActorWinFrame();
                    switch (m_CurMagic.EffectNumber)
                    {
                        case 104:
                            m_nSpellFrame = THumAction.HA.ActMagic_104.frame;
                            m_nStartFrame = THumAction.HA.ActMagic_104.start + m_btDir *
                                (THumAction.HA.ActMagic_104.frame + THumAction.HA.ActMagic_104.skip);
                            m_nEndFrame = m_nStartFrame + THumAction.HA.ActMagic_104.frame - 1;
                            m_dwFrameTime = THumAction.HA.ActMagic_104.ftime;
                            m_dwStartTime = MShare.GetTickCount();
                            m_boSSkill = true;
                            break;
                        case 105:
                            m_nSpellFrame = THumAction.HA.ActMagic_105.frame;
                            m_nStartFrame = THumAction.HA.ActMagic_105.start + m_btDir *
                                (THumAction.HA.ActMagic_105.frame + THumAction.HA.ActMagic_105.skip);
                            m_nEndFrame = m_nStartFrame + THumAction.HA.ActMagic_105.frame - 1;
                            m_dwFrameTime = THumAction.HA.ActMagic_105.ftime;
                            m_dwStartTime = MShare.GetTickCount();
                            m_boSSkill = true;
                            m_boNewMagic = true;
                            break;
                        case 106:
                            m_nSpellFrame = THumAction.HA.ActMagic_106.frame;
                            m_nStartFrame = THumAction.HA.ActMagic_106.start + m_btDir *
                                (THumAction.HA.ActMagic_106.frame + THumAction.HA.ActMagic_106.skip);
                            m_nEndFrame = m_nStartFrame + THumAction.HA.ActMagic_106.frame - 1;
                            m_dwFrameTime = THumAction.HA.ActMagic_106.ftime;
                            m_dwStartTime = MShare.GetTickCount();
                            m_boSSkill = true;
                            m_boNewMagic = true;
                            break;
                        case 107:
                            m_nSpellFrame = THumAction.HA.ActMagic_107.frame;
                            m_nStartFrame = THumAction.HA.ActMagic_107.start + m_btDir *
                                (THumAction.HA.ActMagic_107.frame + THumAction.HA.ActMagic_107.skip);
                            m_nEndFrame = m_nStartFrame + THumAction.HA.ActMagic_107.frame - 1;
                            m_dwFrameTime = THumAction.HA.ActMagic_107.ftime;
                            m_dwStartTime = MShare.GetTickCount();
                            m_boSSkill = true;
                            m_boNewMagic = true;
                            break;
                        case 108:
                            m_nSpellFrame = THumAction.HA.ActMagic_108.frame;
                            m_nStartFrame = THumAction.HA.ActMagic_108.start + m_btDir *
                                (THumAction.HA.ActMagic_108.frame + THumAction.HA.ActMagic_108.skip);
                            m_nEndFrame = m_nStartFrame + THumAction.HA.ActMagic_108.frame - 1;
                            m_dwFrameTime = THumAction.HA.ActMagic_108.ftime;
                            m_dwStartTime = MShare.GetTickCount();
                            m_boSSkill = true;
                            break;
                        case 109:
                            m_nSpellFrame = THumAction.HA.ActMagic_109.frame;
                            m_nStartFrame = THumAction.HA.ActMagic_109.start + m_btDir *
                                (THumAction.HA.ActMagic_109.frame + THumAction.HA.ActMagic_109.skip);
                            m_nEndFrame = m_nStartFrame + THumAction.HA.ActMagic_109.frame - 1;
                            m_dwFrameTime = THumAction.HA.ActMagic_109.ftime;
                            m_dwStartTime = MShare.GetTickCount();
                            m_boSSkill = true;
                            m_boNewMagic = true;
                            break;
                        case 110:
                            m_nSpellFrame = THumAction.HA.ActMagic_110.frame;
                            m_nStartFrame = THumAction.HA.ActMagic_110.start + m_btDir *
                                (THumAction.HA.ActMagic_110.frame + THumAction.HA.ActMagic_110.skip);
                            m_nEndFrame = m_nStartFrame + THumAction.HA.ActMagic_110.frame - 1;
                            m_dwFrameTime = THumAction.HA.ActMagic_110.ftime;
                            m_dwStartTime = MShare.GetTickCount();
                            m_boSSkill = true;
                            m_boNewMagic = true;
                            break;
                        case 111:
                            m_nSpellFrame = THumAction.HA.ActMagic_111.frame;
                            m_nStartFrame = THumAction.HA.ActMagic_111.start + m_btDir *
                                (THumAction.HA.ActMagic_111.frame + THumAction.HA.ActMagic_111.skip);
                            m_nEndFrame = m_nStartFrame + THumAction.HA.ActMagic_111.frame - 1;
                            m_dwFrameTime = THumAction.HA.ActMagic_111.ftime;
                            m_dwStartTime = MShare.GetTickCount();
                            m_boNewMagic = true;
                            break;
                        case 112:
                            m_nSpellFrame = THumAction.HA.ActMagic_112.frame;
                            m_nStartFrame = THumAction.HA.ActMagic_112.start + m_btDir *
                                (THumAction.HA.ActMagic_112.frame + THumAction.HA.ActMagic_112.skip);
                            m_nEndFrame = m_nStartFrame + THumAction.HA.ActMagic_112.frame - 1;
                            m_dwFrameTime = THumAction.HA.ActMagic_112.ftime;
                            m_dwStartTime = MShare.GetTickCount();
                            m_boSSkill = true;
                            break;
                        case 113:
                            m_nSpellFrame = THumAction.HA.ActMagic_113.frame;
                            m_nStartFrame = THumAction.HA.ActMagic_113.start + m_btDir *
                                (THumAction.HA.ActMagic_113.frame + THumAction.HA.ActMagic_113.skip);
                            m_nEndFrame = m_nStartFrame + THumAction.HA.ActMagic_113.frame - 1;
                            m_dwFrameTime = THumAction.HA.ActMagic_113.ftime;
                            m_dwStartTime = MShare.GetTickCount();
                            m_boSSkill = true;
                            break;
                        case 114:
                            m_nSpellFrame = THumAction.HA.ActMagic_114.frame;
                            m_nStartFrame = THumAction.HA.ActMagic_114.start + m_btDir *
                                (THumAction.HA.ActMagic_114.frame + THumAction.HA.ActMagic_114.skip);
                            m_nEndFrame = m_nStartFrame + THumAction.HA.ActMagic_114.frame - 1;
                            m_dwFrameTime = THumAction.HA.ActMagic_114.ftime;
                            m_dwStartTime = MShare.GetTickCount();
                            m_boSSkill = true;
                            break;
                    }

                    m_nCurEffFrame = 0;
                    m_boUseMagic = true;
                    m_boUseCboLib = true;
                }
                else
                {
                    m_nStartFrame = THumAction.HA.ActSpell.start +
                                    m_btDir * (THumAction.HA.ActSpell.frame + THumAction.HA.ActSpell.skip);
                    m_nEndFrame = m_nStartFrame + THumAction.HA.ActSpell.frame - 1;
                    m_dwFrameTime = THumAction.HA.ActSpell.ftime;
                    m_dwStartTime = MShare.GetTickCount();
                    m_nCurEffFrame = 0;
                    m_boUseMagic = true;
                    // DScreen.AddChatBoardString(format('EffectNumber=%d m_nEndFrame=%d', [m_CurMagic.EffectNumber, 1]), clWhite, clRed);
                    m_nSpellFrame = Actor.DEFSPELLFRAME;
                    switch (m_CurMagic.EffectNumber)
                    {
                        case 10:
                            // 灵魂火符
                            m_nMagLight = 2;
                            //if (m_CurMagic.spelllv > MShare.MAXMAGICLV) 
                            //    m_nSpellFrame = 10;
                            break;
                        case 15:
                            //if (m_CurMagic.spelllv > 3)
                            //{
                            //    m_nMagLight = 2;
                            //    m_nSpellFrame = 10;
                            //}
                            break;
                        case 22: // 地狱雷光
                            m_nMagLight = 4;
                            m_nSpellFrame = 10;
                            break;
                        case 26: // 心灵启示
                            m_nMagLight = 2;
                            break;
                        case 34: // 灭天火
                            m_nMagLight = 2;
                            //if (m_CurMagic.spelllv > MShare.MAXMAGICLV) m_nSpellFrame = 10;
                            break;
                        case 35: // 无极真气
                            m_nMagLight = 2;
                            break;
                        case 43: // 狮子吼
                            m_nMagLight = 3;
                            break;
                        case 121:
                            m_nMagLight = 3;
                            m_nSpellFrame = 10;
                            break;
                        case 120:
                            m_nMagLight = 3;
                            m_nSpellFrame = 12;
                            break;
                        case 122:
                            m_nMagLight = 3;
                            m_nSpellFrame = 8;
                            break;
                        case 116:
                        case 117:
                            // 狮子吼
                            m_nMagLight = 3;
                            m_nSpellFrame = 10;
                            break;
                        case 124:
                            m_nMagLight = 4;
                            // FSpellFrame := 40;
                            break;
                        case 127:
                            m_nMagLight = 3;
                            break;
                        default:
                            m_nMagLight = 2;
                            m_nSpellFrame = Actor.DEFSPELLFRAME;
                            break;
                    }
                }

                m_dwWaitMagicRequest = MShare.GetTickCount();
                m_boWarMode = true;
                m_dwWarModeTime = MShare.GetTickCount();
                Shift(m_btDir, 0, 0, 1);
                break;
            case Grobal2.SM_STRUCK:
                m_nStartFrame = THumAction.HA.ActStruck.start +
                                m_btDir * (THumAction.HA.ActStruck.frame + THumAction.HA.ActStruck.skip);
                m_nEndFrame = m_nStartFrame + THumAction.HA.ActStruck.frame - 1;
                m_dwFrameTime = m_dwStruckFrameTime;
                m_dwStartTime = MShare.GetTickCount();
                Shift(m_btDir, 0, 0, 1);
                m_dwGenAnicountTime = MShare.GetTickCount();
                m_nCurBubbleStruck = 0;
                break;
            case Grobal2.SM_NOWDEATH:
                m_nStartFrame = THumAction.HA.ActDie.start +
                                m_btDir * (THumAction.HA.ActDie.frame + THumAction.HA.ActDie.skip);
                m_nEndFrame = m_nStartFrame + THumAction.HA.ActDie.frame - 1;
                m_dwFrameTime = THumAction.HA.ActDie.ftime;
                m_dwStartTime = MShare.GetTickCount();
                break;
        }
    }

    public override void DefaultMotion()
    {
        int MaxIdx;
        int frame;
        base.DefaultMotion();
        if (m_boUseCboLib)
        {
            if (m_btEffect == 50)
            {
                if (m_nCurrentFrame <= 536)
                    if (MShare.GetTickCount() - m_dwFrameTick > 100)
                    {
                        if (m_nFrame < 19)
                            m_nFrame++;
                        else
                            m_nFrame = 0;
                        m_dwFrameTick = MShare.GetTickCount();
                    }
            }
            else if (m_btEffect != 0)
            {
                MaxIdx = 0;
                frame = 0;
                switch (m_nCurrentAction)
                {
                    case Grobal2.SM_SPELL:
                        switch (m_CurMagic.EffectNumber)
                        {
                            case 104:
                                frame = 6;
                                MaxIdx = 640;
                                break;
                            case 112:
                                frame = 6;
                                MaxIdx = 720;
                                break;
                            case 106:
                                frame = 8;
                                MaxIdx = 800;
                                break;
                            case 107:
                                frame = 13;
                                MaxIdx = 1040;
                                break;
                            case 108:
                                frame = 6;
                                MaxIdx = 1200;
                                break;
                            case 109:
                                frame = 12;
                                MaxIdx = 1440;
                                break;
                            case 110:
                                frame = 12;
                                MaxIdx = 1600;
                                break;
                            case 111:
                                frame = 14;
                                MaxIdx = 1760;
                                break;
                            case 105:
                                // 112
                                frame = 10;
                                MaxIdx = 880;
                                break;
                        }

                        break;
                }

                // dscreen.AddChatBoardString(inttostr(m_nCboHumWinOffSet), clBlue, clWhite);
                if (m_nCurrentFrame < MaxIdx)
                    if (MShare.GetTickCount() - m_dwFrameTick > MShare.HUMWINEFFECTTICK)
                    {
                        if (m_nFrame < frame - 1)
                            m_nFrame++;
                        else
                            m_nFrame = 0;
                        m_dwFrameTick = MShare.GetTickCount();
                    }
            }
            else
            {
                if (m_btEffect == 50)
                {
                    if (m_nCurrentFrame <= 536)
                        if (MShare.GetTickCount() - m_dwFrameTick > 100)
                        {
                            if (m_nFrame < 19)
                                m_nFrame++;
                            else
                                m_nFrame = 0;
                            m_dwFrameTick = MShare.GetTickCount();
                        }
                }
                else if (m_btEffect != 0)
                {
                    if (m_nCurrentFrame < 64)
                        if (MShare.GetTickCount() - m_dwFrameTick > MShare.HUMWINEFFECTTICK)
                        {
                            // Blue
                            if (m_nFrame < 7)
                                m_nFrame++;
                            else
                                m_nFrame = 0;
                            m_dwFrameTick = MShare.GetTickCount();
                        }
                }
            }
        }
    }

    public override int GetDefaultFrame(bool wmode)
    {
        int result;
        int cf;
        if (m_boDeath)
        {
            result = THumAction.HA.ActDie.start + m_btDir * (THumAction.HA.ActDie.frame + THumAction.HA.ActDie.skip) +
                     (THumAction.HA.ActDie.frame - 1);
        }
        else if (wmode)
        {
            result = THumAction.HA.ActWarMode.start +
                     m_btDir * (THumAction.HA.ActWarMode.frame + THumAction.HA.ActWarMode.skip);
        }
        else
        {
            m_nDefFrameCount = THumAction.HA.ActStand.frame;
            if (m_nCurrentDefFrame < 0)
                cf = 0;
            else if (m_nCurrentDefFrame >= THumAction.HA.ActStand.frame)
                cf = 0;
            else
                cf = m_nCurrentDefFrame;
            result = THumAction.HA.ActStand.start +
                     m_btDir * (THumAction.HA.ActStand.frame + THumAction.HA.ActStand.skip) + cf;
        }

        return result;
    }

    public override void RunFrameAction(int frame)
    {
        //m_boHideWeapon = false;
        //if (m_boSSkill)
        //{
        //    if (frame == 1)
        //    {
        //        m_boSSkill = false;
        //    }
        //}
        //else if (this.m_nCurrentAction == Grobal2.SM_HEAVYHIT)
        //{
        //    if ((frame == 5) && this.m_boDigFragment)
        //    {
        //        this.m_boDigFragment = false;
        //        var __event = ClMain.EventMan.GetEvent(this.m_nCurrX, this.m_nCurrY, Grobal2.ET_PILESTONES);
        //        if (__event != null)
        //        {
        //            __event.m_nEventParam = __event.m_nEventParam + 1;
        //        }
        //    }
        //}
        //else if (this.m_nCurrentAction == Grobal2.SM_SITDOWN)
        //{
        //    if ((frame == 5) && this.m_boDigFragment)
        //    {
        //        this.m_boDigFragment = false;
        //        var __event = ClMain.EventMan.GetEvent(this.m_nCurrX, this.m_nCurrY, Grobal2.ET_PILESTONES);
        //        if (__event != null)
        //        {
        //            __event.m_nEventParam = __event.m_nEventParam + 1;
        //        }
        //    }
        //}
        //else if (this.m_nCurrentAction == Grobal2.SM_THROW)
        //{
        //    if ((frame == 3) && this.m_boThrow)
        //    {
        //        this.m_boThrow = false;
        //    }
        //    if (frame >= 3)
        //    {
        //        m_boHideWeapon = true;
        //    }
        //}
    }

    public void DoWeaponBreakEffect()
    {
        m_boWeaponEffect = true;
        m_nCurWeaponEffect = 0;
    }

    //public bool Run_MagicTimeOut()
    //{
    //    bool result;
    //    if (this == MShare.g_MySelf)
    //    {
    //        result = MShare.GetTickCount() - this.m_dwWaitMagicRequest > 1800;
    //    }
    //    else
    //    {
    //        result = MShare.GetTickCount() - this.m_dwWaitMagicRequest > 850 + (byte)bss * 50;
    //    }
    //    if (!bss && result)
    //    {
    //        this.m_CurMagic.ServerMagicCode = 0;
    //    }
    //    return result;
    //}

    public override void Run()
    {
        int off;
        int prv;
        int dwFrameTimetime;
        bool bss;
        bool sskill;
        bool fAddNewMagic;
        if (MShare.GetTickCount() - m_dwGenAnicountTime > 120)
        {
            m_dwGenAnicountTime = MShare.GetTickCount();
            m_nGenAniCount++;
            if (m_nGenAniCount > 100000) m_nGenAniCount = 0;
            m_nCurBubbleStruck++;
        }

        if (m_boWeaponEffect)
            if (MShare.GetTickCount() - m_dwWeaponpEffectTime > 120)
            {
                m_dwWeaponpEffectTime = MShare.GetTickCount();
                m_nCurWeaponEffect++;
                if (m_nCurWeaponEffect >= Actor.MAXWPEFFECTFRAME) m_boWeaponEffect = false;
            }

        if (new ArrayList(new[] { 5, 9, 11, 13, 39 }).Contains(m_nCurrentAction)) return;
        m_boMsgMuch = this != MShare.g_MySelf && m_MsgList.Count >= 2;
        bss = new ArrayList(new[] { 105, 109 }).Contains(m_CurMagic.EffectNumber);
        off = m_nCurrentFrame - m_nStartFrame;
        RunFrameAction(off);
        prv = m_nCurrentFrame;
        if (m_nCurrentAction != 0)
        {
            if (m_nCurrentFrame < m_nStartFrame || m_nCurrentFrame > m_nEndFrame) m_nCurrentFrame = m_nStartFrame;
            if (m_boMsgMuch)
            {
                if (m_boUseCboLib)
                {
                    if (m_btIsHero == 1)
                        dwFrameTimetime = HUtil32.Round(m_dwFrameTime / 1.50);
                    else
                        dwFrameTimetime = HUtil32.Round(m_dwFrameTime / 1.55);
                }
                else
                {
                    dwFrameTimetime = HUtil32.Round(m_dwFrameTime / 1.7);
                }
            }
            else if (this != MShare.g_MySelf && m_boUseMagic)
            {
                if (m_boUseCboLib)
                {
                    if (m_btIsHero == 1)
                        dwFrameTimetime = HUtil32.Round(m_dwFrameTime / 1.28);
                    else
                        dwFrameTimetime = HUtil32.Round(m_dwFrameTime / 1.32);
                }
                else
                {
                    dwFrameTimetime = HUtil32.Round(m_dwFrameTime / 1.38);
                }
            }
            else
            {
                dwFrameTimetime = m_dwFrameTime;
            }

            if (MShare.g_boSpeedRate)
                dwFrameTimetime = HUtil32._MAX(0, dwFrameTimetime - HUtil32._MIN(10, MShare.g_MoveSpeedRate));
            if (MShare.GetTickCount() - m_dwStartTime > dwFrameTimetime)
            {
                if (m_nCurrentFrame < m_nEndFrame)
                {
                    if (m_boUseMagic)
                    {
                        // 魔法效果...
                        if (m_nCurEffFrame == m_nSpellFrame - 2 || Run_MagicTimeOut())
                        {
                            if (m_CurMagic.ServerMagicCode >= 0 || Run_MagicTimeOut())
                            {
                                m_nCurrentFrame++;
                                m_nCurEffFrame++;
                                m_dwStartTime = MShare.GetTickCount();
                            }
                        }
                        else
                        {
                            if (m_nCurrentFrame < m_nEndFrame - 1) m_nCurrentFrame++;
                            m_nCurEffFrame++;
                            m_dwStartTime = MShare.GetTickCount();
                        }
                    }
                    else
                    {
                        m_nCurrentFrame++;
                        m_dwStartTime = MShare.GetTickCount();
                    }

                    ReadyNextAction();
                }
                else
                {
                    if (this == MShare.g_MySelf)
                    {
                        if (robotClient.ServerAcceptNextAction())
                        {
                            ActionEnded();
                            m_nCurrentAction = 0;
                            m_boUseMagic = false;
                            m_boUseCboLib = false;
                        }
                    }
                    else
                    {
                        ActionEnded();
                        m_nCurrentAction = 0;
                        m_boUseMagic = false;
                        m_boUseCboLib = false;
                    }

                    m_boHitEffect = false;
                    if (m_boSmiteLongHit == 1)
                    {
                        m_boSmiteLongHit = 2;
                        CalcActorWinFrame();
                        m_nStartFrame = THumAction.HA.ActSmiteLongHit2.start + m_btDir *
                            (THumAction.HA.ActSmiteLongHit2.frame + THumAction.HA.ActSmiteLongHit2.skip);
                        m_nEndFrame = m_nStartFrame + THumAction.HA.ActSmiteLongHit2.frame - 1;
                        m_dwFrameTime = THumAction.HA.ActSmiteLongHit2.ftime;
                        m_dwStartTime = MShare.GetTickCount();
                        m_nMaxTick = THumAction.HA.ActSmiteLongHit2.usetick;
                        m_nCurTick = 0;
                        Shift(m_btDir, 0, 0, 1);
                        m_boWarMode = true;
                        m_dwWarModeTime = MShare.GetTickCount();
                        m_boUseCboLib = true;
                        m_boHitEffect = true;
                    }
                }

                if (m_boUseMagic)
                {
                    if (bss && m_CurMagic.ServerMagicCode > 0)
                    {
                        sskill = false;
                        switch (m_CurMagic.EffectNumber)
                        {
                            case 105:
                            case 106:
                                sskill = m_nCurEffFrame == 7;
                                break;
                            case 107:
                            case 109:
                                sskill = m_nCurEffFrame == 9;
                                break;
                            case 110:
                                sskill = new ArrayList(new[] { 6, 8, 10 }).Contains(m_nCurEffFrame);
                                break;
                            case 111:
                                sskill = m_nCurEffFrame == 10;
                                break;
                                // 116, 117: sskill := m_nCurEffFrame = 6;
                        }

                        if (sskill)
                            //TUseMagicInfo _wvar1 = this.m_CurMagic;
                            //ClMain.g_PlayScene.NewMagic(this, _wvar1.ServerMagicCode, _wvar1.EffectNumber, this.m_nCurrX, this.m_nCurrY, _wvar1.targx, _wvar1.targy, _wvar1.target, _wvar1.EffectType, _wvar1.Recusion, _wvar1.anitime, ref boFly, _wvar1.magfirelv, _wvar1.Poison);
                            m_boNewMagic = false;
                    }

                    switch (m_CurMagic.EffectNumber)
                    {
                        case 127:
                            fAddNewMagic = m_nCurEffFrame == 7;
                            break;
                        default:
                            fAddNewMagic = m_nCurEffFrame == m_nSpellFrame - 1;
                            break;
                    }

                    if (fAddNewMagic)
                    {
                        if (m_CurMagic.ServerMagicCode > 0 && (!bss || m_boNewMagic))
                        {
                            //TUseMagicInfo _wvar2 = this.m_CurMagic;
                            //ClMain.g_PlayScene.NewMagic(this, _wvar2.ServerMagicCode, _wvar2.EffectNumber, this.m_nCurrX, this.m_nCurrY, _wvar2.targx, _wvar2.targy, _wvar2.target, _wvar2.EffectType, _wvar2.Recusion, _wvar2.anitime, ref boFly, _wvar2.magfirelv, _wvar2.Poison);
                        }

                        if (this == MShare.g_MySelf) MShare.g_dwLatestSpellTick = MShare.GetTickCount();
                        m_CurMagic.ServerMagicCode = 0;
                    }
                }
            }

            if (m_btRace == 0)
                m_nCurrentDefFrame = 0;
            else
                m_nCurrentDefFrame = -10;
            m_dwDefFrameTime = MShare.GetTickCount();
        }
        else if (MShare.GetTickCount() - m_dwSmoothMoveTime > 200)
        {
            if (MShare.GetTickCount() - m_dwDefFrameTime > 500)
            {
                m_dwDefFrameTime = MShare.GetTickCount();
                m_nCurrentDefFrame++;
                if (m_nCurrentDefFrame >= m_nDefFrameCount) m_nCurrentDefFrame = 0;
            }

            DefaultMotion();
        }

        if (prv != m_nCurrentFrame) m_dwLoadSurfaceTime = MShare.GetTickCount();
    }

    public override int light()
    {
        int result;
        int L;
        L = m_nChrLight;
        if (L < m_nMagLight)
            if (m_boUseMagic || m_boHitEffect)
                L = m_nMagLight;
        result = L;
        return result;
    }
}

public class TStallMgr
{
    public TClientStallInfo mBlock;
    public bool OnSale;
}

public class TClientStallInfo
{
    public int ItemCount;
    public TClientItem[] Items = new TClientItem[10];
    public string StallName;
}