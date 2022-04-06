using System.Collections;
using SystemModule;

namespace RobotSvr;

public class TBanyaGuardMon : TSkeletonArcherMon
{
    public TBanyaGuardMon(RobotClient robotClient) : base(robotClient)
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
            case Grobal2.SM_HIT:
                if (m_btRace >= 117 && m_btRace <= 119)
                    m_nStartFrame = pm.ActAttack.start;
                else
                    m_nStartFrame = pm.ActAttack.start + m_btDir * (pm.ActAttack.frame + pm.ActAttack.skip);
                m_nEndFrame = m_nStartFrame + pm.ActAttack.frame - 1;
                m_dwFrameTime = pm.ActAttack.ftime;
                m_dwStartTime = MShare.GetTickCount();
                m_dwWarModeTime = MShare.GetTickCount();
                Shift(m_btDir, 0, 0, 1);
                if (!new ArrayList(new[] {27, 28, 111}).Contains(m_btRace))
                {
                    m_boUseEffect = true;
                    m_nEffectFrame = m_nStartFrame;
                    m_nEffectStart = m_nStartFrame;
                    m_nEffectEnd = m_nEndFrame;
                    m_dwEffectStartTime = MShare.GetTickCount();
                    m_dwEffectFrameTime = m_dwFrameTime;
                }

                if (new ArrayList(new[] {113, 114, 115}).Contains(m_btRace)) m_boUseEffect = false;
                break;
            case Grobal2.SM_LIGHTING:
                if (m_btRace >= 117 && m_btRace <= 119)
                    m_nStartFrame = pm.ActCritical.start;
                else
                    m_nStartFrame = pm.ActCritical.start + m_btDir * (pm.ActCritical.frame + pm.ActCritical.skip);
                m_nEndFrame = m_nStartFrame + pm.ActCritical.frame - 1;
                m_dwFrameTime = pm.ActCritical.ftime;
                m_dwStartTime = MShare.GetTickCount();
                m_nCurEffFrame = 0;
                m_boUseMagic = true;
                m_dwWarModeTime = MShare.GetTickCount();
                Shift(m_btDir, 0, 0, 1);
                if (new ArrayList(new[] {71, 72, 111}).Contains(m_btRace))
                {
                    m_boUseEffect = true;
                    m_nEffectFrame = m_nStartFrame;
                    m_nEffectStart = m_nStartFrame;
                    m_nEffectEnd = m_nEndFrame;
                    m_dwEffectStartTime = MShare.GetTickCount();
                    m_dwEffectFrameTime = m_dwFrameTime;
                }
                else if (new ArrayList(new[] {27, 28}).Contains(m_btRace))
                {
                    m_boUseEffect = true;
                    m_nEffectFrame = m_nStartFrame;
                    m_nEffectStart = m_nStartFrame;
                    m_nEffectEnd = m_nEndFrame;
                    if (m_btRace == 28) m_nEffectEnd = m_nEndFrame - 4;
                    m_dwEffectStartTime = MShare.GetTickCount();
                    m_dwEffectFrameTime = m_dwFrameTime;
                }
                else if (new ArrayList(new[] {113, 114}).Contains(m_btRace))
                {
                    m_nStartFrame = pm.ActAttack.start + m_btDir * (pm.ActAttack.frame + pm.ActAttack.skip);
                    m_nEndFrame = m_nStartFrame + pm.ActAttack.frame - 1;
                    m_dwFrameTime = pm.ActAttack.ftime;
                    if (m_btRace == 113)
                    {
                        m_boUseEffect = true;
                        m_nEffectFrame = 350 + m_btDir * 10;
                        m_nEffectStart = m_nEffectFrame;
                        m_nEffectEnd = m_nEffectFrame + 5;
                        m_dwEffectStartTime = MShare.GetTickCount();
                        m_dwEffectFrameTime = m_dwFrameTime;
                    }
                }
                else if (m_btRace == 117)
                {
                    m_boUseEffect = true;
                    m_nEffectFrame = m_nStartFrame + 15;
                    m_nEffectStart = m_nEffectFrame;
                    m_nEffectEnd = m_nEffectFrame + 3;
                    m_dwEffectStartTime = MShare.GetTickCount();
                    m_dwEffectFrameTime = m_dwFrameTime;
                }
                else if (new ArrayList(new[] {118, 119}).Contains(m_btRace))
                {
                    m_boUseEffect = true;
                    m_boUseMagic = false;
                    m_dwEffectStartTime = MShare.GetTickCount();
                    m_dwEffectFrameTime = m_dwFrameTime;
                    if (m_btRace == 118)
                    {
                    }

                    m_nEffectFrame = 2750;
                    m_nEffectStart = 2750;
                    m_nEffectEnd = 2750 + 9;
                    if (m_btRace == 119)
                    {
                        m_nEffectFrame = 2860;
                        m_nEffectStart = 2860;
                        m_nEffectEnd = 2860 + 9;
                    }
                }

                break;
            default:
                base.CalcActorFrame();
                break;
        }
    }
}