using SystemModule;

namespace RobotSvr;

public class TKillingHerb : TActor
{
    public TKillingHerb(RobotClient robotClient) : base(robotClient)
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
                m_nStartFrame = pm.ActStand.start;
                m_nEndFrame = m_nStartFrame + pm.ActStand.frame - 1;
                m_dwFrameTime = pm.ActStand.ftime;
                m_dwStartTime = MShare.GetTickCount();
                m_nDefFrameCount = pm.ActStand.frame;
                Shift(m_btDir, 0, 0, 1);
                break;
            case Grobal2.SM_DIGUP:
                m_nStartFrame = pm.ActWalk.start;
                m_nEndFrame = m_nStartFrame + pm.ActWalk.frame - 1;
                m_dwFrameTime = pm.ActWalk.ftime;
                m_dwStartTime = MShare.GetTickCount();
                m_nMaxTick = pm.ActWalk.usetick;
                m_nCurTick = 0;
                m_nMoveStep = 1;
                Shift(m_btDir, 0, 0, 1);
                break;
            case Grobal2.SM_HIT:
                m_nStartFrame = pm.ActAttack.start + m_btDir * (pm.ActAttack.frame + pm.ActAttack.skip);
                m_nEndFrame = m_nStartFrame + pm.ActAttack.frame - 1;
                m_dwFrameTime = pm.ActAttack.ftime;
                m_dwStartTime = MShare.GetTickCount();
                m_dwWarModeTime = MShare.GetTickCount();
                Shift(m_btDir, 0, 0, 1);
                break;
            case Grobal2.SM_STRUCK:
                m_nStartFrame = pm.ActStruck.start + m_btDir * (pm.ActStruck.frame + pm.ActStruck.skip);
                m_nEndFrame = m_nStartFrame + pm.ActStruck.frame - 1;
                m_dwFrameTime = m_dwStruckFrameTime;
                m_dwStartTime = MShare.GetTickCount();
                break;
            case Grobal2.SM_DEATH:
                m_nStartFrame = pm.ActDie.start + m_btDir * (pm.ActDie.frame + pm.ActDie.skip);
                m_nEndFrame = m_nStartFrame + pm.ActDie.frame - 1;
                m_nStartFrame = m_nEndFrame;
                m_dwFrameTime = pm.ActDie.ftime;
                m_dwStartTime = MShare.GetTickCount();
                break;
            case Grobal2.SM_NOWDEATH:
                m_nStartFrame = pm.ActDie.start + m_btDir * (pm.ActDie.frame + pm.ActDie.skip);
                m_nEndFrame = m_nStartFrame + pm.ActDie.frame - 1;
                m_dwFrameTime = pm.ActDie.ftime;
                m_dwStartTime = MShare.GetTickCount();
                break;
            case Grobal2.SM_DIGDOWN:
                m_nStartFrame = pm.ActDeath.start;
                m_nEndFrame = m_nStartFrame + pm.ActDeath.frame - 1;
                m_dwFrameTime = pm.ActDeath.ftime;
                m_dwStartTime = MShare.GetTickCount();
                m_boDelActionAfterFinished = true;
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
            if (m_boSkeleton)
                result = pm.ActDeath.start;
            else
                result = pm.ActDie.start + m_btDir * (pm.ActDie.frame + pm.ActDie.skip) + (pm.ActDie.frame - 1);
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
            result = pm.ActStand.start + cf;
        }

        return result;
    }
}