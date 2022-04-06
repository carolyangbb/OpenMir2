using SystemModule;

namespace RobotSvr;

public class TCrystalSpider : TGasKuDeGi
{
    public TCrystalSpider(RobotClient robotClient) : base(robotClient)
    {
    }

    public override void CalcActorFrame()
    {
        m_nCurrentFrame = -1;
        m_nBodyOffset = Actor.GetOffset(m_wAppearance);
        var pm = Actor.GetRaceByPM(m_btRace, m_wAppearance);
        if (pm == null) return;
        switch (m_nCurrentAction)
        {
            case Grobal2.SM_LIGHTING:
                m_nStartFrame = pm.ActCritical.start + m_btDir * (pm.ActCritical.frame + pm.ActCritical.skip);
                m_nEndFrame = m_nStartFrame + pm.ActCritical.frame - 1;
                m_dwFrameTime = pm.ActCritical.ftime;
                m_dwStartTime = MShare.GetTickCount();
                m_dwWarModeTime = MShare.GetTickCount();
                Shift(m_btDir, 0, 0, 1);
                m_boUseEffect = true;
                m_nEffectStart = 0;
                m_nEffectFrame = 0;
                m_nEffectEnd = 10;
                m_dwEffectStartTime = MShare.GetTickCount();
                m_dwEffectFrameTime = 50;
                m_nCurEffFrame = 0;
                break;
            case Grobal2.SM_HIT:
                base.CalcActorFrame();
                m_boUseEffect = false;
                break;
            default:
                base.CalcActorFrame();
                break;
        }
    }
}