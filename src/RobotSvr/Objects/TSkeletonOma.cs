using System.Collections;
using SystemModule;

namespace RobotSvr;

public class TSkeletonOma : TActor
{
    protected int ax = 0;
    protected int ay = 0;

    public TSkeletonOma(RobotClient robotClient) : base(robotClient)
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
            case Grobal2.SM_TURN:
                if (m_btRace >= 117 && m_btRace <= 119)
                    m_nStartFrame = pm.ActStand.start;
                else
                    m_nStartFrame = pm.ActStand.start + m_btDir * (pm.ActStand.frame + pm.ActStand.skip);
                m_nEndFrame = m_nStartFrame + pm.ActStand.frame - 1;
                m_dwFrameTime = pm.ActStand.ftime;
                m_dwStartTime = MShare.GetTickCount();
                m_nDefFrameCount = pm.ActStand.frame;
                Shift(m_btDir, 0, 0, 1);
                if (new ArrayList(new[] { 118, 119 }).Contains(m_btRace))
                {
                    m_boUseEffect = true;
                    m_dwEffectStartTime = MShare.GetTickCount();
                }

                break;
            case Grobal2.SM_WALK:
            case Grobal2.SM_BACKSTEP:
                if (m_btRace >= 117 && m_btRace <= 119)
                    m_nStartFrame = pm.ActWalk.start;
                else
                    m_nStartFrame = pm.ActWalk.start + m_btDir * (pm.ActWalk.frame + pm.ActWalk.skip);
                m_nEndFrame = m_nStartFrame + pm.ActWalk.frame - 1;
                m_dwFrameTime = pm.ActWalk.ftime;
                m_dwStartTime = MShare.GetTickCount();
                m_nMaxTick = pm.ActWalk.usetick;
                m_nCurTick = 0;
                // WarMode := FALSE;
                m_nMoveStep = 1;
                if (new ArrayList(new[] { 118, 119 }).Contains(m_btRace))
                {
                    m_boUseEffect = true;
                    m_dwEffectStartTime = MShare.GetTickCount();
                }

                if (m_nCurrentAction == Grobal2.SM_WALK)
                    Shift(m_btDir, m_nMoveStep, 0, m_nEndFrame - m_nStartFrame + 1);
                else
                    // sm_backstep
                    Shift(ClFunc.GetBack(m_btDir), m_nMoveStep, 0, m_nEndFrame - m_nStartFrame + 1);
                break;
            case Grobal2.SM_DIGUP:
                switch (m_btRace)
                {
                    // Modify the A .. B: 23, 91 .. 93
                    case 23:
                    case 91:
                        m_nStartFrame = pm.ActDeath.start;
                        break;
                    default:
                        m_nStartFrame = pm.ActDeath.start + m_btDir * (pm.ActDeath.frame + pm.ActDeath.skip);
                        break;
                }

                m_nEndFrame = m_nStartFrame + pm.ActDeath.frame - 1;
                m_dwFrameTime = pm.ActDeath.ftime;
                m_dwStartTime = MShare.GetTickCount();
                Shift(m_btDir, 0, 0, 1);
                break;
            case Grobal2.SM_DIGDOWN:
                if (m_btRace == 55)
                {
                    m_nStartFrame = pm.ActCritical.start + m_btDir * (pm.ActCritical.frame + pm.ActCritical.skip);
                    m_nEndFrame = m_nStartFrame + pm.ActCritical.frame - 1;
                    m_dwFrameTime = pm.ActCritical.ftime;
                    m_dwStartTime = MShare.GetTickCount();
                    m_boReverseFrame = true;
                    Shift(m_btDir, 0, 0, 1);
                }

                break;
            case Grobal2.SM_HIT:
            case Grobal2.SM_FLYAXE:
            case Grobal2.SM_LIGHTING:
                m_nStartFrame = pm.ActAttack.start + m_btDir * (pm.ActAttack.frame + pm.ActAttack.skip);
                m_nEndFrame = m_nStartFrame + pm.ActAttack.frame - 1;
                m_dwFrameTime = pm.ActAttack.ftime;
                m_dwStartTime = MShare.GetTickCount();
                // WarMode := TRUE;
                m_dwWarModeTime = MShare.GetTickCount();
                if (m_btRace == 16 || m_btRace == 54) m_boUseEffect = true;
                Shift(m_btDir, 0, 0, 1);
                if (new ArrayList(new[] { 118, 119 }).Contains(m_btRace))
                {
                    m_boUseEffect = true;
                    m_dwEffectStartTime = MShare.GetTickCount();
                }

                break;
            case Grobal2.SM_STRUCK:
                if (m_btRace >= 117 && m_btRace <= 119)
                    m_nStartFrame = pm.ActStruck.start;
                else
                    m_nStartFrame = pm.ActStruck.start + m_btDir * (pm.ActStruck.frame + pm.ActStruck.skip);
                m_nEndFrame = m_nStartFrame + pm.ActStruck.frame - 1;
                m_dwFrameTime = m_dwStruckFrameTime;
                // pm.ActStruck.ftime;
                m_dwStartTime = MShare.GetTickCount();
                break;
            case Grobal2.SM_DEATH:
                if (m_btRace >= 117 && m_btRace <= 119)
                    m_nStartFrame = pm.ActDie.start;
                else
                    m_nStartFrame = pm.ActDie.start + m_btDir * (pm.ActDie.frame + pm.ActDie.skip);
                m_nEndFrame = m_nStartFrame + pm.ActDie.frame - 1;
                m_nStartFrame = m_nEndFrame;
                m_dwFrameTime = pm.ActDie.ftime;
                m_dwStartTime = MShare.GetTickCount();
                break;
            case Grobal2.SM_NOWDEATH:
                if (m_btRace >= 117 && m_btRace <= 119)
                    m_nStartFrame = pm.ActDie.start;
                else
                    m_nStartFrame = pm.ActDie.start + m_btDir * (pm.ActDie.frame + pm.ActDie.skip);
                m_nEndFrame = m_nStartFrame + pm.ActDie.frame - 1;
                m_dwFrameTime = pm.ActDie.ftime;
                m_dwStartTime = MShare.GetTickCount();
                if (m_btRace != 22) m_boUseEffect = true;
                break;
            case Grobal2.SM_SKELETON:
                m_nStartFrame = pm.ActDeath.start;
                m_nEndFrame = m_nStartFrame + pm.ActDeath.frame - 1;
                m_dwFrameTime = pm.ActDeath.ftime;
                m_dwStartTime = MShare.GetTickCount();
                break;
            case Grobal2.SM_ALIVE:
                if (new ArrayList(new[] { 117 }).Contains(m_btRace))
                    m_nStartFrame = pm.ActDeath.start;
                else
                    m_nStartFrame = pm.ActDeath.start + m_btDir * (pm.ActDeath.frame + pm.ActDeath.skip);
                m_nEndFrame = m_nStartFrame + pm.ActDeath.frame - 1;
                m_dwFrameTime = pm.ActDeath.ftime;
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
            if (new ArrayList(new[] { 30, 151 }).Contains(m_wAppearance)) m_nDownDrawLevel = 1;
            if (m_boSkeleton)
            {
                result = pm.ActDeath.start;
            }
            else if (m_btRace == 120)
            {
                result = 417;
                m_boUseEffect = false;
            }
            else if (m_btRace >= 117 && m_btRace <= 119)
            {
                result = pm.ActDie.start + (pm.ActDie.frame - 1);
                m_boUseEffect = false;
            }
            else
            {
                result = pm.ActDie.start + m_btDir * (pm.ActDie.frame + pm.ActDie.skip) + (pm.ActDie.frame - 1);
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
            if (m_btRace == 120)
            {
                switch (m_nTempState)
                {
                    case 1:
                        m_nStartFrame = 0;
                        break;
                    case 2:
                        m_nStartFrame = 80;
                        break;
                    case 3:
                        m_nStartFrame = 160;
                        break;
                    case 4:
                        m_nStartFrame = 240;
                        break;
                    case 5:
                        m_nStartFrame = 320;
                        break;
                }

                result = m_nStartFrame + cf;
            }
            else if (m_btRace >= 117 && m_btRace <= 119)
            {
                result = pm.ActStand.start + cf;
            }
            else
            {
                result = pm.ActStand.start + m_btDir * (pm.ActStand.frame + pm.ActStand.skip) + cf;
            }

            if (new ArrayList(new[] { 118, 119 }).Contains(m_btRace))
            {
                m_boUseEffect = true;
                m_dwEffectStartTime = MShare.GetTickCount();
                m_dwEffectFrameTime = pm.ActStand.ftime;
            }

            if (m_btRace == 118)
            {
                m_nEffectFrame = 2730 + m_nCurrentFrame;
            }
            else if (m_btRace == 119)
            {
                m_nEffectFrame = 2840 + m_nCurrentFrame;
            }
            else if (m_btRace == 120)
            {
                m_boUseEffect = true;
                m_dwEffectStartTime = MShare.GetTickCount();
                m_dwEffectFrameTime = pm.ActStand.ftime;
                m_nEffectFrame = 2940 + m_nCurrentFrame;
            }
        }

        return result;
    }

    public override void Run()
    {
        int prv;
        long m_dwFrameTimetime;
        if (m_nCurrentAction == Grobal2.SM_WALK || m_nCurrentAction == Grobal2.SM_BACKSTEP ||
            m_nCurrentAction == Grobal2.SM_RUN || m_nCurrentAction == Grobal2.SM_HORSERUN) return;
        m_boMsgMuch = false;
        if (m_MsgList.Count >= MShare.MSGMUCH) m_boMsgMuch = true;
        RunFrameAction(m_nCurrentFrame - m_nStartFrame);
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
                    // 悼累捞 场巢.
                    m_nCurrentAction = 0;
                    // 悼累 肯丰
                    m_boUseEffect = false;
                }
            }

            m_nCurrentDefFrame = 0;
            m_dwDefFrameTime = MShare.GetTickCount();
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