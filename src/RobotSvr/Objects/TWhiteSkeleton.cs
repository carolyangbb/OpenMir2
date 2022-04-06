using SystemModule;

namespace RobotSvr;

public class TWhiteSkeleton : TSkeletonOma
{
    public TWhiteSkeleton(RobotClient robotClient) : base(robotClient)
    {
    }

    public override void CalcActorFrame()
    {
        base.CalcActorFrame();
        m_boUseMagic = false;
        m_nCurrentFrame = -1;
        m_nHitEffectNumber = 0;
        m_nBodyOffset = Actor.GetOffset(m_wAppearance);
        m_Action = Actor.GetRaceByPM(m_btRace, m_wAppearance);
        if (m_Action == null) return;
        switch (m_nCurrentAction)
        {
            case Grobal2.SM_POWERHIT:
                m_nStartFrame = m_Action.ActAttack.start +
                                m_btDir * (m_Action.ActAttack.frame + m_Action.ActAttack.skip);
                m_nEndFrame = m_nStartFrame + m_Action.ActAttack.frame - 1;
                m_dwFrameTime = m_Action.ActAttack.ftime;
                m_dwStartTime = MShare.GetTickCount();
                m_dwWarModeTime = MShare.GetTickCount();
                if (m_nCurrentAction == Grobal2.SM_POWERHIT)
                {
                    m_boHitEffect = true;
                    m_nMagLight = 2;
                    m_nHitEffectNumber = 1;
                    switch (m_btRace)
                    {
                        case 91:
                            m_nHitEffectNumber += 101;
                            break;
                        case 92:
                            m_nHitEffectNumber += 201;
                            break;
                        case 93:
                            m_nHitEffectNumber += 301;
                            break;
                    }
                }

                Shift(m_btDir, 0, 0, 1);
                break;
        }
    }
}