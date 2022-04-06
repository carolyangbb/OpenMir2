using SystemModule;

namespace RobotSvr;

public class TCentipedeKingMon : TKillingHerb
{
    private readonly int ax = 0;
    private readonly int ay = 0;
    private bool BoUseDieEffect;

    public TCentipedeKingMon(RobotClient robotClient) : base(robotClient)
    {
    }

    public override void CalcActorFrame()
    {
        TMonsterAction pm;
        m_boUseMagic = false;
        m_nCurrentFrame = -1;
        m_nBodyOffset = Actor.GetOffset(m_wAppearance);
        pm = Actor.GetRaceByPM(m_btRace, m_wAppearance);
        if (pm == null) return;
        switch (m_nCurrentAction)
        {
            case Grobal2.SM_TURN:
                m_btDir = 0;
                base.CalcActorFrame();
                break;
            case Grobal2.SM_HIT:
                m_btDir = 0;
                m_nStartFrame = pm.ActCritical.start + m_btDir * (pm.ActCritical.frame + pm.ActCritical.skip);
                m_nEndFrame = m_nStartFrame + pm.ActCritical.frame - 1;
                m_dwFrameTime = pm.ActCritical.ftime;
                m_dwStartTime = MShare.GetTickCount();
                BoUseDieEffect = true;
                m_nEffectFrame = 0;
                m_nEffectStart = 0;
                m_nEffectEnd = m_nEffectStart + 9;
                m_dwEffectFrameTime = 62;
                Shift(m_btDir, 0, 0, 1);
                break;
            case Grobal2.SM_DIGDOWN:
                base.CalcActorFrame();
                break;
            default:
                m_btDir = 0;
                base.CalcActorFrame();
                break;
        }
    }

    public override void Run()
    {
        if (m_nCurrentAction == Grobal2.SM_WALK || m_nCurrentAction == Grobal2.SM_BACKSTEP ||
            m_nCurrentAction == Grobal2.SM_HORSERUN || m_nCurrentAction == Grobal2.SM_RUN) return;
        if (BoUseDieEffect)
            if (m_nCurrentFrame - m_nStartFrame >= 5)
            {
                BoUseDieEffect = false;
                m_boUseEffect = true;
                m_dwEffectStartTime = MShare.GetTickCount();
                m_nEffectFrame = 0;
            }

        if (m_boUseEffect)
            if (MShare.GetTickCount() - m_dwEffectStartTime > m_dwEffectFrameTime)
            {
                m_dwEffectStartTime = MShare.GetTickCount();
                if (m_nEffectFrame < m_nEffectEnd)
                    m_nEffectFrame++;
                else
                    m_boUseEffect = false;
            }

        base.Run();
    }
}