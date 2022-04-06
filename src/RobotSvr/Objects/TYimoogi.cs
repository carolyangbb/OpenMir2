using SystemModule;

namespace RobotSvr;

public class TYimoogi : TGasKuDeGi
{
    public TYimoogi(RobotClient robotClient) : base(robotClient)
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
            case Grobal2.SM_FLYAXE:
                m_nStartFrame = pm.ActCritical.start + m_btDir * (pm.ActCritical.frame + pm.ActCritical.skip);
                m_nEndFrame = m_nStartFrame + pm.ActCritical.frame - 1;
                m_dwFrameTime = pm.ActCritical.ftime;
                m_dwStartTime = MShare.GetTickCount();
                m_dwWarModeTime = MShare.GetTickCount();
                Shift(m_btDir, 0, 0, 1);
                m_boUseEffect = true;
                m_nEffectStart = 0;
                m_nEffectFrame = 0;
                m_nEffectEnd = 6;
                m_dwEffectStartTime = MShare.GetTickCount();
                m_dwEffectFrameTime = pm.ActCritical.ftime;
                m_nCurEffFrame = 0;
                break;
            case Grobal2.SM_LIGHTING:
                m_nStartFrame = pm.ActAttack.start + m_btDir * (pm.ActAttack.frame + pm.ActAttack.skip);
                m_nEndFrame = m_nStartFrame + pm.ActAttack.frame - 1;
                m_dwFrameTime = pm.ActAttack.ftime;
                m_dwStartTime = MShare.GetTickCount();
                m_dwWarModeTime = MShare.GetTickCount();
                Shift(m_btDir, 0, 0, 1);
                m_boUseEffect = true;
                m_nEffectStart = 0;
                m_nEffectFrame = 0;
                m_nEffectEnd = 2;
                m_dwEffectStartTime = MShare.GetTickCount();
                m_dwEffectFrameTime = pm.ActAttack.ftime;
                m_nCurEffFrame = 0;
                break;
            case Grobal2.SM_HIT:
                base.CalcActorFrame();
                m_boUseEffect = false;
                break;
            case Grobal2.SM_NOWDEATH:
                m_nStartFrame = pm.ActDie.start;
                m_nEndFrame = m_nStartFrame + pm.ActDie.frame - 1;
                m_dwFrameTime = pm.ActDie.ftime;
                m_dwStartTime = MShare.GetTickCount();
                m_boUseEffect = true;
                m_nEffectFrame = 0;
                m_nEffectStart = 0;
                m_nEffectEnd = 10;
                m_dwEffectStartTime = MShare.GetTickCount();
                m_dwEffectFrameTime = 100;
                break;
            default:
                base.CalcActorFrame();
                break;
        }
    }
}