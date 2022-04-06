using SystemModule;

namespace RobotSvr;

public class TExplosionSpider : TGasKuDeGi
{
    public TExplosionSpider(RobotClient robotClient) : base(robotClient)
    {
    }

    public override void CalcActorFrame()
    {
        base.CalcActorFrame();
        switch (m_nCurrentAction)
        {
            case Grobal2.SM_HIT:
                m_boUseEffect = false;
                break;
            case Grobal2.SM_NOWDEATH:
                m_nEffectStart = m_nStartFrame;
                m_nEffectFrame = m_nStartFrame;
                m_dwEffectStartTime = MShare.GetTickCount();
                m_dwEffectFrameTime = m_dwFrameTime;
                m_nEffectEnd = m_nEndFrame;
                m_boUseEffect = true;
                break;
        }
    }
}