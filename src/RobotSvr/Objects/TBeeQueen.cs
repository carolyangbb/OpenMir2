using SystemModule;

namespace RobotSvr;

public class TBeeQueen : TActor
{
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
                // //规氢捞 绝澜...
                m_nStartFrame = pm.ActStand.start;
                // + Dir * (pm.ActStand.frame + pm.ActStand.skip);
                m_nEndFrame = m_nStartFrame + pm.ActStand.frame - 1;
                m_dwFrameTime = pm.ActStand.ftime;
                m_dwStartTime = MShare.GetTickCount();
                m_nDefFrameCount = pm.ActStand.frame;
                Shift(m_btDir, 0, 0, 1);
                break;
            case Grobal2.SM_HIT:
                m_nStartFrame = pm.ActAttack.start;
                // + Dir * (pm.ActAttack.frame + pm.ActAttack.skip);
                m_nEndFrame = m_nStartFrame + pm.ActAttack.frame - 1;
                m_dwFrameTime = pm.ActAttack.ftime;
                m_dwStartTime = MShare.GetTickCount();
                // WarMode := TRUE;
                m_dwWarModeTime = MShare.GetTickCount();
                Shift(m_btDir, 0, 0, 1);
                break;
            case Grobal2.SM_STRUCK:
                m_nStartFrame = pm.ActStruck.start;
                // + Dir * (pm.ActStruck.frame + pm.ActStruck.skip);
                m_nEndFrame = m_nStartFrame + pm.ActStruck.frame - 1;
                m_dwFrameTime = m_dwStruckFrameTime;
                // pm.ActStruck.ftime;
                m_dwStartTime = MShare.GetTickCount();
                break;
            case Grobal2.SM_DEATH:
                m_nStartFrame = pm.ActDie.start;
                // + Dir * (pm.ActDie.frame + pm.ActDie.skip);
                m_nEndFrame = m_nStartFrame + pm.ActDie.frame - 1;
                m_nStartFrame = m_nEndFrame;
                m_dwFrameTime = pm.ActDie.ftime;
                m_dwStartTime = MShare.GetTickCount();
                break;
            case Grobal2.SM_NOWDEATH:
                m_nStartFrame = pm.ActDie.start;
                // + Dir * (pm.ActDie.frame + pm.ActDie.skip);
                m_nEndFrame = m_nStartFrame + pm.ActDie.frame - 1;
                m_dwFrameTime = pm.ActDie.ftime;
                m_dwStartTime = MShare.GetTickCount();
                break;
        }
    }

    public override int GetDefaultFrame(bool wmode)
    {
        int result;
        TMonsterAction pm;
        int cf;
        result = 0;
        // jacky
        pm = Actor.GetRaceByPM(m_btRace, m_wAppearance);
        if (pm == null) return result;
        if (m_boDeath)
        {
            result = pm.ActDie.start + (pm.ActDie.frame - 1);
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
            // 规氢捞 绝澜..
        }

        return result;
    }

    public TBeeQueen(RobotClient robotClient) : base(robotClient)
    {
    }
}