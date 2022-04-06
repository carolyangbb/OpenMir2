using SystemModule;

namespace RobotSvr;

public class TSkeletonKingMon : TGasKuDeGi
{
    public TSkeletonKingMon(RobotClient robotClient) : base(robotClient)
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
            case Grobal2.SM_BACKSTEP:
            case Grobal2.SM_WALK:
                m_nStartFrame = pm.ActWalk.start + m_btDir * (pm.ActWalk.frame + pm.ActWalk.skip);
                m_nEndFrame = m_nStartFrame + pm.ActWalk.frame - 1;
                m_dwFrameTime = pm.ActWalk.ftime;
                m_dwStartTime = MShare.GetTickCount();
                m_nEffectFrame = pm.ActWalk.start;
                m_nEffectStart = pm.ActWalk.start;
                m_nEffectEnd = pm.ActWalk.start + pm.ActWalk.frame - 1;
                m_dwEffectStartTime = MShare.GetTickCount();
                m_dwEffectFrameTime = m_dwFrameTime;
                m_boUseEffect = true;
                m_nMaxTick = pm.ActWalk.usetick;
                m_nCurTick = 0;
                m_nMoveStep = 1;
                if (m_nCurrentAction == Grobal2.SM_WALK)
                    Shift(m_btDir, m_nMoveStep, 0, m_nEndFrame - m_nStartFrame + 1);
                else
                    Shift(ClFunc.GetBack(m_btDir), m_nMoveStep, 0, m_nEndFrame - m_nStartFrame + 1);
                break;
            case Grobal2.SM_HIT:
                m_nStartFrame = pm.ActAttack.start + m_btDir * (pm.ActAttack.frame + pm.ActAttack.skip);
                m_nEndFrame = m_nStartFrame + pm.ActAttack.frame - 1;
                m_dwFrameTime = pm.ActAttack.ftime;
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
            case Grobal2.SM_FLYAXE:
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
            case Grobal2.SM_LIGHTING:
                m_nStartFrame = 80 + pm.ActAttack.start + m_btDir * (pm.ActAttack.frame + pm.ActAttack.skip);
                m_nEndFrame = m_nStartFrame + pm.ActAttack.frame - 1;
                m_dwFrameTime = pm.ActAttack.ftime;
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
            case Grobal2.SM_STRUCK:
                m_nStartFrame = pm.ActStruck.start + m_btDir * (pm.ActStruck.frame + pm.ActStruck.skip);
                m_nEndFrame = m_nStartFrame + pm.ActStruck.frame - 1;
                m_dwFrameTime = pm.ActStruck.ftime;
                m_dwStartTime = MShare.GetTickCount();
                m_nEffectFrame = pm.ActStruck.start;
                m_nEffectStart = pm.ActStruck.start;
                m_nEffectEnd = pm.ActStruck.start + pm.ActStruck.frame - 1;
                m_dwEffectStartTime = MShare.GetTickCount();
                m_dwEffectFrameTime = m_dwFrameTime;
                m_boUseEffect = true;
                break;
            case Grobal2.SM_NOWDEATH:
                m_nStartFrame = pm.ActDie.start + m_btDir * (pm.ActDie.frame + pm.ActDie.skip);
                m_nEndFrame = m_nStartFrame + pm.ActDie.frame - 1;
                m_dwFrameTime = pm.ActDie.ftime;
                m_dwStartTime = MShare.GetTickCount();
                m_nEffectFrame = pm.ActDie.start;
                m_nEffectStart = pm.ActDie.start;
                m_nEffectEnd = pm.ActDie.start + pm.ActDie.frame - 1;
                m_dwEffectStartTime = MShare.GetTickCount();
                m_dwEffectFrameTime = m_dwFrameTime;
                m_boUseEffect = true;
                break;
            default:
                base.CalcActorFrame();
                break;
        }
    }

    public override void Run()
    {
        int prv;
        long m_dwEffectFrameTimetime;
        long m_dwFrameTimetime;
        if (m_nCurrentAction == Grobal2.SM_WALK || m_nCurrentAction == Grobal2.SM_BACKSTEP ||
            m_nCurrentAction == Grobal2.SM_RUN || m_nCurrentAction == Grobal2.SM_HORSERUN) return;
        m_boMsgMuch = false;
        if (m_MsgList.Count >= MShare.MSGMUCH) m_boMsgMuch = true;
        RunFrameAction(m_nCurrentFrame - m_nStartFrame);
        if (m_boUseEffect)
        {
            if (m_boMsgMuch)
                m_dwEffectFrameTimetime = HUtil32.Round(m_dwEffectFrameTime * 2 / 3);
            else
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

        prv = m_nCurrentFrame;
        if (m_nCurrentAction != 0)
        {
            if (m_nCurrentFrame < m_nStartFrame || m_nCurrentFrame > m_nEndFrame) m_nCurrentFrame = m_nStartFrame;
            if (m_boMsgMuch)
                m_dwFrameTimetime = HUtil32.Round(m_dwFrameTime * 2 / 3);
            else
                m_dwFrameTimetime = m_dwFrameTime;
            if (MShare.GetTickCount() - m_dwStartTime > m_dwFrameTimetime)
            {
                if (m_nCurrentFrame < m_nEndFrame)
                {
                    m_nCurrentFrame++;
                    m_dwStartTime = MShare.GetTickCount();
                }
                else
                {
                    m_nCurrentAction = 0;
                    m_boUseEffect = false;
                    BoUseDieEffect = false;
                }

                m_nCurrentDefFrame = 0;
                m_dwDefFrameTime = MShare.GetTickCount();
            }
        }
        else
        {
            if (MShare.GetTickCount() - m_dwSmoothMoveTime > 200)
            {
                if (MShare.GetTickCount() - m_dwDefFrameTime > 500)
                {
                    m_dwDefFrameTime = MShare.GetTickCount();
                    m_nCurrentDefFrame++;
                    if (m_nCurrentDefFrame >= m_nDefFrameCount) m_nCurrentDefFrame = 0;
                }

                DefaultMotion();
            }
        }

        if (prv != m_nCurrentFrame) m_dwLoadSurfaceTime = MShare.GetTickCount();
    }
}