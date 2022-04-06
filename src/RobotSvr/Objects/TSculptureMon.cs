using SystemModule;

namespace RobotSvr;

public class TSculptureMon : TSkeletonOma
{
    private int firedir;

    public TSculptureMon(RobotClient robotClient) : base(robotClient)
    {
    }

    public override void CalcActorFrame()
    {
        TMonsterAction pm;
        m_nCurrentFrame = -1;
        m_nBodyOffset = Actor.GetOffset(m_wAppearance);
        pm = Actor.GetRaceByPM(m_btRace, m_wAppearance);
        if (pm == null) return;
        m_boUseEffect = false;
        switch (m_nCurrentAction)
        {
            case Grobal2.SM_TURN:
                if ((m_nState & Grobal2.STATE_STONE_MODE) != 0)
                {
                    if (m_btRace == 48 || m_btRace == 49)
                        m_nStartFrame = pm.ActDeath.start;
                    else
                        m_nStartFrame = pm.ActDeath.start + m_btDir * (pm.ActDeath.frame + pm.ActDeath.skip);
                    m_nEndFrame = m_nStartFrame;
                    m_dwFrameTime = pm.ActDeath.ftime;
                    m_dwStartTime = MShare.GetTickCount();
                    m_nDefFrameCount = pm.ActDeath.frame;
                }
                else
                {
                    m_nStartFrame = pm.ActStand.start + m_btDir * (pm.ActStand.frame + pm.ActStand.skip);
                    m_nEndFrame = m_nStartFrame + pm.ActStand.frame - 1;
                    m_dwFrameTime = pm.ActStand.ftime;
                    m_dwStartTime = MShare.GetTickCount();
                    m_nDefFrameCount = pm.ActStand.frame;
                }

                Shift(m_btDir, 0, 0, 1);
                break;
            case Grobal2.SM_WALK:
            case Grobal2.SM_BACKSTEP:
                m_nStartFrame = pm.ActWalk.start + m_btDir * (pm.ActWalk.frame + pm.ActWalk.skip);
                m_nEndFrame = m_nStartFrame + pm.ActWalk.frame - 1;
                m_dwFrameTime = pm.ActWalk.ftime;
                m_dwStartTime = MShare.GetTickCount();
                m_nMaxTick = pm.ActWalk.usetick;
                m_nCurTick = 0;
                // WarMode := FALSE;
                m_nMoveStep = 1;
                if (m_nCurrentAction == Grobal2.SM_WALK)
                    Shift(m_btDir, m_nMoveStep, 0, m_nEndFrame - m_nStartFrame + 1);
                else
                    // sm_backstep
                    Shift(ClFunc.GetBack(m_btDir), m_nMoveStep, 0, m_nEndFrame - m_nStartFrame + 1);
                break;
            case Grobal2.SM_DIGUP:
                // //叭扁 绝澜, SM_DIGUP, 规氢 绝澜.
                if (m_btRace == 48 || m_btRace == 49)
                    m_nStartFrame = pm.ActDeath.start;
                else
                    m_nStartFrame = pm.ActDeath.start + m_btDir * (pm.ActDeath.frame + pm.ActDeath.skip);
                m_nEndFrame = m_nStartFrame + pm.ActDeath.frame - 1;
                m_dwFrameTime = pm.ActDeath.ftime;
                m_dwStartTime = MShare.GetTickCount();
                // WarMode := FALSE;
                Shift(m_btDir, 0, 0, 1);
                break;
            case Grobal2.SM_HIT:
                m_nStartFrame = pm.ActAttack.start + m_btDir * (pm.ActAttack.frame + pm.ActAttack.skip);
                m_nEndFrame = m_nStartFrame + pm.ActAttack.frame - 1;
                m_dwFrameTime = pm.ActAttack.ftime;
                m_dwStartTime = MShare.GetTickCount();
                if (m_btRace == 49)
                {
                    m_boUseEffect = true;
                    firedir = m_btDir;
                    m_nEffectFrame = 0;
                    // startframe;
                    m_nEffectStart = 0;
                    // startframe;
                    m_nEffectEnd = m_nEffectStart + 8;
                    m_dwEffectStartTime = MShare.GetTickCount();
                    m_dwEffectFrameTime = m_dwFrameTime;
                }

                Shift(m_btDir, 0, 0, 1);
                break;
            case Grobal2.SM_STRUCK:
                m_nStartFrame = pm.ActStruck.start + m_btDir * (pm.ActStruck.frame + pm.ActStruck.skip);
                m_nEndFrame = m_nStartFrame + pm.ActStruck.frame - 1;
                m_dwFrameTime = m_dwStruckFrameTime;
                // pm.ActStruck.ftime;
                m_dwStartTime = MShare.GetTickCount();
                break;
            case Grobal2.SM_DEATH:
                m_nStartFrame = pm.ActDie.start + m_btDir * (pm.ActDie.frame + pm.ActDie.skip);
                m_nEndFrame = m_nStartFrame + pm.ActDie.frame - 1;
                m_nStartFrame = m_nEndFrame;
                m_dwFrameTime = pm.ActDie.ftime;
                m_dwStartTime = MShare.GetTickCount();
                break;
            case Grobal2.SM_NOWDEATH:
                m_nStartFrame = pm.ActDie.start + m_btDir * (pm.ActDie.frame + pm.ActDie.skip);
                m_nEndFrame = m_nStartFrame + pm.ActDie.frame - 1;
                m_dwFrameTime = pm.ActDie.ftime;
                m_dwStartTime = MShare.GetTickCount();
                break;
        }
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
            result = pm.ActDie.start + m_btDir * (pm.ActDie.frame + pm.ActDie.skip) + (pm.ActDie.frame - 1);
        }
        else
        {
            if ((m_nState & Grobal2.STATE_STONE_MODE) != 0)
            {
                switch (m_btRace)
                {
                    case 47:
                        result = pm.ActDeath.start + m_btDir * (pm.ActDeath.frame + pm.ActDeath.skip);
                        break;
                    case 48:
                    case 49:
                        result = pm.ActDeath.start;
                        break;
                }
            }
            else
            {
                m_nDefFrameCount = pm.ActStand.frame;
                if (m_nCurrentDefFrame < 0)
                    cf = 0;
                else if (m_nCurrentDefFrame >= pm.ActStand.frame)
                    cf = 0;
                else
                    cf = m_nCurrentDefFrame;
                result = pm.ActStand.start + m_btDir * (pm.ActStand.frame + pm.ActStand.skip) + cf;
            }
        }

        return result;
    }

    public override void Run()
    {
        long m_dwEffectFrameTimetime;
        if (m_nCurrentAction == Grobal2.SM_WALK || m_nCurrentAction == Grobal2.SM_BACKSTEP ||
            m_nCurrentAction == Grobal2.SM_RUN || m_nCurrentAction == Grobal2.SM_HORSERUN) return;
        if (m_boUseEffect)
        {
            m_dwEffectFrameTimetime = m_dwEffectFrameTime;
            if (MShare.GetTickCount() - m_dwEffectStartTime > m_dwEffectFrameTimetime)
            {
                m_dwEffectStartTime = MShare.GetTickCount();
                if (m_nEffectFrame < m_nEffectEnd)
                    m_nEffectFrame++;
                else
                    m_boUseEffect = false;
            }
        }

        base.Run();
    }
}