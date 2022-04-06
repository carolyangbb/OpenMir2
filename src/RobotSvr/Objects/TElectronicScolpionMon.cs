using SystemModule;

namespace RobotSvr;

public class TElectronicScolpionMon : TGasKuDeGi
{
    public TElectronicScolpionMon(RobotClient robotClient) : base(robotClient)
    {
    }

    public override void CalcActorFrame()
    {
        TMonsterAction pm;
        m_nCurrentFrame = -1;
        m_nBodyOffset = Actor.GetOffset(m_wAppearance);
        pm = Actor.GetRaceByPM(m_btRace, m_wAppearance);
        if (pm == null) return;
        switch (m_nCurrentAction)
        {
            case Grobal2.SM_HIT:
                m_nStartFrame = pm.ActAttack.start + m_btDir * (pm.ActAttack.frame + pm.ActAttack.skip);
                m_nEndFrame = m_nStartFrame + pm.ActAttack.frame - 1;
                m_dwFrameTime = pm.ActAttack.ftime;
                m_dwStartTime = MShare.GetTickCount();
                m_dwWarModeTime = MShare.GetTickCount();
                Shift(m_btDir, 0, 0, 1);
                break;
            case Grobal2.SM_LIGHTING:
                m_nStartFrame = pm.ActCritical.start + m_btDir * (pm.ActCritical.frame + pm.ActCritical.skip);
                m_nEndFrame = m_nStartFrame + pm.ActCritical.frame - 1;
                m_dwFrameTime = pm.ActCritical.ftime;
                m_dwStartTime = MShare.GetTickCount();
                m_dwWarModeTime = MShare.GetTickCount();
                Shift(m_btDir, 0, 0, 1);
                m_boUseEffect = true;
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
}