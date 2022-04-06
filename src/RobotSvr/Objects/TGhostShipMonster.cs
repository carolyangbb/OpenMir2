using SystemModule;

namespace RobotSvr;

public class TGhostShipMonster : TActor
{
    protected int ax = 0;
    protected int ax2 = 0;
    protected int ay = 0;
    protected int ay2 = 0;
    public bool FFireBall;
    protected byte firedir = 0;
    public bool FLighting;

    public TGhostShipMonster(RobotClient robotClient) : base(robotClient)
    {
        m_boUseEffect = false;
        FFireBall = false;
        FLighting = false;
    }

    public override void CalcActorFrame()
    {
        TMonsterAction pm;
        m_boUseMagic = false;
        m_boUseEffect = false;
        m_boHitEffect = false;
        m_boReverseFrame = false;
        m_nCurrentFrame = -1;
        m_nHitEffectNumber = 0;
        m_nBodyOffset = Actor.GetOffset(m_wAppearance);
        pm = Actor.GetRaceByPM(m_btRace, m_wAppearance);
        if (pm == null) return;
        switch (m_nCurrentAction)
        {
            case Grobal2.SM_HIT:
                m_nStartFrame = m_Action.ActAttack.start +
                                m_btDir * (m_Action.ActAttack.frame + m_Action.ActAttack.skip);
                m_nEndFrame = m_nStartFrame + m_Action.ActAttack.frame - 1;
                m_dwFrameTime = m_Action.ActAttack.ftime;
                m_dwStartTime = MShare.GetTickCount();
                m_boWarMode = true;
                m_dwWarModeTime = MShare.GetTickCount();
                if (m_wAppearance == 354)
                {
                    m_boHitEffect = true;
                    m_nMagLight = 2;
                    m_nHitEffectNumber = 3;
                    m_nHitEffectNumber += 101;
                }

                if (m_wAppearance == 815)
                {
                    m_boHitEffect = true;
                    m_nMagLight = 2;
                    m_nHitEffectNumber = 3;
                    m_nHitEffectNumber += 301;
                }

                Shift(m_btDir, 0, 0, 1);
                break;
            case Grobal2.SM_LIGHTING:
                switch (m_wAppearance)
                {
                    case 354:
                    case 356:
                    case 359:
                    case 813:
                    case 815:
                        m_nStartFrame = m_Action.ActCritical.start +
                                        m_btDir * (m_Action.ActCritical.frame + m_Action.ActCritical.skip);
                        m_nEndFrame = m_nStartFrame + m_Action.ActCritical.frame - 1;
                        m_dwFrameTime = m_Action.ActCritical.ftime;
                        m_dwStartTime = MShare.GetTickCount();
                        m_dwWarModeTime = MShare.GetTickCount();
                        if (m_wAppearance == 354 || m_wAppearance == 815) m_boSmiteWideHit2 = true;
                        break;
                    default:
                        base.CalcActorFrame();
                        break;
                }
                break;
            case Grobal2.SM_DIGUP:
                switch (m_wAppearance)
                {
                    case 351:
                    case 827:
                        m_nStartFrame = pm.ActDeath.start + m_btDir * (pm.ActDeath.frame + pm.ActDeath.skip);
                        break;
                    default:
                        base.CalcActorFrame();
                        break;
                }
                m_nEndFrame = m_nStartFrame + pm.ActDeath.frame - 1;
                m_dwFrameTime = pm.ActDeath.ftime;
                m_dwStartTime = MShare.GetTickCount();
                Shift(m_btDir, 0, 0, 1);
                break;
            case Grobal2.SM_SPELL:
                if (m_CurMagic.MagicSerial == 23)
                {
                    m_nStartFrame = m_Action.ActCritical.start +
                                    m_btDir * (m_Action.ActCritical.frame + m_Action.ActCritical.skip);
                    m_nEndFrame = m_nStartFrame + m_Action.ActCritical.frame - 1;
                    m_dwFrameTime = m_Action.ActCritical.ftime;
                    m_dwStartTime = MShare.GetTickCount();
                    m_dwWarModeTime = MShare.GetTickCount();
                }
                else
                {
                    m_nStartFrame = pm.ActAttack.start + m_btDir * (pm.ActAttack.frame + pm.ActAttack.skip);
                    m_nEndFrame = m_nStartFrame + pm.ActAttack.frame - 1;
                    m_dwFrameTime = pm.ActAttack.ftime;
                    m_dwStartTime = MShare.GetTickCount();
                }

                m_nCurEffFrame = 0;
                m_boUseMagic = true;
                m_nMagLight = 2;
                m_nSpellFrame = pm.ActAttack.frame;
                m_dwWaitMagicRequest = MShare.GetTickCount();
                m_boWarMode = true;
                m_dwWarModeTime = MShare.GetTickCount();
                Shift(m_btDir, 0, 0, 1);
                break;
            case Grobal2.SM_FLYAXE:
                m_nStartFrame = pm.ActAttack.start + m_btDir * (pm.ActAttack.frame + pm.ActAttack.skip);
                m_nEndFrame = m_nStartFrame + pm.ActAttack.frame - 1;
                m_dwFrameTime = pm.ActAttack.ftime;
                m_dwStartTime = MShare.GetTickCount();
                m_dwWarModeTime = MShare.GetTickCount();
                Shift(m_btDir, 0, 0, 1);
                break;
            default:
                base.CalcActorFrame();
                break;
        }
    }

    public override void RunFrameAction(int frame)
    {
        //TNormalDrawEffect neff;
        //if (this.m_nCurrentAction == Grobal2.SM_LIGHTING)
        //{
        //    if ((frame == 5) && this.m_boSmiteWideHit2)
        //    {
        //        this.m_boSmiteWideHit2 = false;
        //        neff = new TNormalDrawEffect(this.m_nCurrX, this.m_nCurrY, WMFile.Units.WMFile.g_WMagic2Images, 1391, 14, 75, true);
        //        if (neff != null)
        //        {
        //            ClMain.g_PlayScene.m_EffectList.Add(neff);
        //        }
        //        if (this.m_wAppearance == 354)
        //        {
        //            ClMain.g_ShakeScreen.SetScrShake_X(4);
        //            ClMain.g_ShakeScreen.SetScrShake_Y(3);
        //        }
        //        if (this.m_wAppearance == 815)
        //        {
        //            ClMain.g_ShakeScreen.SetScrShake_X(7);
        //            ClMain.g_ShakeScreen.SetScrShake_Y(5);
        //        }
        //    }
        //}
    }

    public override int GetDefaultFrame(bool wmode)
    {
        int result;
        int cf;
        TMonsterAction pm;
        result = 0;
        pm = Actor.GetRaceByPM(m_btRace, m_wAppearance);
        if (pm == null) return result;
        if (m_boDeath)
        {
            if (m_boSkeleton)
                result = pm.ActDeath.start;
            else
                result = pm.ActDie.start + m_btDir * (pm.ActDie.frame + pm.ActDie.skip) + (pm.ActDie.frame - 1);
        }
        else
        {
            m_nDefFrameCount = pm.ActStand.frame;
            if (m_nCurrentDefFrame < 0)
                cf = 0;
            else if (m_nCurrentDefFrame >= m_nDefFrameCount)
                cf = 0;
            else
                cf = m_nCurrentDefFrame;
            result = pm.ActStand.start + m_btDir * (pm.ActStand.frame + pm.ActStand.skip) + cf;
        }

        return result;
    }

    public override void Run()
    {
        int prv;
        long dwFrameTimetime;
        bool bofly;
        if (m_nCurrentAction == Grobal2.SM_WALK || m_nCurrentAction == Grobal2.SM_BACKSTEP || m_nCurrentAction == Grobal2.SM_RUN || m_nCurrentAction == Grobal2.SM_HORSERUN || m_nCurrentAction == Grobal2.SM_RUSH || m_nCurrentAction == Grobal2.SM_RUSHKUNG)
        {
            return;
        }
        m_boMsgMuch = false;
        if (m_MsgList.Count >= 2) m_boMsgMuch = true;
        RunFrameAction(m_nCurrentFrame - m_nStartFrame);
        prv = m_nCurrentFrame;
        if (m_nCurrentAction != 0)
        {
            if (m_nCurrentFrame < m_nStartFrame || m_nCurrentFrame > m_nEndFrame) m_nCurrentFrame = m_nStartFrame;
            if (m_boMsgMuch)
                dwFrameTimetime = HUtil32.Round(m_dwFrameTime * 2 / 3);
            else
                dwFrameTimetime = m_dwFrameTime;
            if (MShare.GetTickCount() - m_dwStartTime > dwFrameTimetime)
            {
                if (m_nCurrentFrame < m_nEndFrame)
                {
                    if (m_boUseMagic)
                    {
                        if (m_nCurEffFrame == m_nSpellFrame - 2)
                        {
                            if (m_CurMagic.ServerMagicCode >= 0)
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
                }
                else
                {
                    if (m_boDelActionAfterFinished) m_boDelActor = true;
                    ActionEnded();
                    m_nCurrentAction = 0;
                    m_boUseMagic = false;
                    m_boUseEffect = false;
                    m_boHitEffect = false;
                }
                if (m_boUseMagic)
                {
                    if (m_nCurEffFrame == m_nSpellFrame - 1)
                    {
                        if (m_CurMagic.ServerMagicCode > 0)
                        {
                            var _wvar1 = m_CurMagic;
                            //robotClient.g_PlayScene.NewMagic(this, _wvar1.ServerMagicCode, _wvar1.EffectNumber, this.m_nCurrX, this.m_nCurrY, _wvar1.targx, _wvar1.targy, _wvar1.target, _wvar1.EffectType, _wvar1.Recusion, _wvar1.anitime, ref bofly, _wvar1.magfirelv);
                        }
                        m_CurMagic.ServerMagicCode = 0;
                    }
                }
            }
            m_nCurrentDefFrame = 0;
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
}