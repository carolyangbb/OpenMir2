using SystemModule;

namespace RobotSvr;

public class TTiger : TActor
{
    protected int ax = 0;
    protected int ay = 0;
    protected byte firedir;

    public TTiger(RobotClient robotClient) : base(robotClient)
    {
        m_boUseEffect = false;
    }

    public override void CalcActorFrame()
    {
        TMonsterAction pm;
        m_nCurrentFrame = -1;
        m_boReverseFrame = false;
        m_boUseEffect = false;
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
                m_dwWarModeTime = MShare.GetTickCount();
                Shift(m_btDir, 0, 0, 1);
                m_boUseEffect = true;
                m_dwEffectStartTime = MShare.GetTickCount();
                firedir = m_btDir;
                m_nEffectFrame = m_nStartFrame;
                m_nEffectStart = m_nStartFrame;
                m_nEffectEnd = m_nEndFrame;
                m_dwEffectStartTime = MShare.GetTickCount();
                m_dwEffectFrameTime = m_dwFrameTime;
                break;
            case Grobal2.SM_LIGHTING:
                m_nStartFrame = pm.ActCritical.start + m_btDir * (pm.ActCritical.frame + pm.ActCritical.skip);
                m_nEndFrame = m_nStartFrame + pm.ActCritical.frame - 1;
                m_dwFrameTime = pm.ActCritical.ftime;
                m_dwStartTime = MShare.GetTickCount();
                m_dwWarModeTime = MShare.GetTickCount();
                Shift(m_btDir, 0, 0, 1);
                m_boUseEffect = true;
                m_dwEffectStartTime = MShare.GetTickCount();
                firedir = m_btDir;
                m_nEffectFrame = m_nStartFrame;
                m_nEffectStart = m_nStartFrame;
                m_nEffectEnd = m_nEndFrame;
                m_dwEffectStartTime = MShare.GetTickCount();
                m_dwEffectFrameTime = m_dwFrameTime;
                break;
            default:
                base.CalcActorFrame();
                break;
        }
    }

    public override void Run()
    {
        long m_dwEffectframetimetime;
        if (m_nCurrentAction == Grobal2.SM_WALK || m_nCurrentAction == Grobal2.SM_BACKSTEP ||
            m_nCurrentAction == Grobal2.SM_RUN || m_nCurrentAction == Grobal2.SM_HORSERUN) return;
        if (m_boUseEffect)
        {
            m_dwEffectframetimetime = m_dwEffectFrameTime;
            if (MShare.GetTickCount() - m_dwEffectStartTime > m_dwEffectframetimetime)
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