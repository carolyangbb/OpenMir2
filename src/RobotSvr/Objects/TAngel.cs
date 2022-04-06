using SystemModule;

namespace RobotSvr;

public class TAngel : THumActor
{
    protected int ax = 0;
    protected int ay = 0;

    public TAngel(RobotClient robotClient) : base(robotClient)
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
                m_nStartFrame = pm.ActStand.start + m_btDir * (pm.ActStand.frame + pm.ActStand.skip);
                m_nEndFrame = m_nStartFrame + pm.ActStand.frame - 1;
                m_dwFrameTime = pm.ActStand.ftime;
                m_dwStartTime = MShare.GetTickCount();
                m_nDefFrameCount = pm.ActStand.frame;
                Shift(m_btDir, 0, 0, 1);
                break;
            case Grobal2.SM_WALK:
                m_nStartFrame = pm.ActWalk.start + m_btDir * (pm.ActWalk.frame + pm.ActWalk.skip);
                m_nEndFrame = m_nStartFrame + pm.ActWalk.frame - 1;
                m_dwFrameTime = pm.ActWalk.ftime;
                m_dwStartTime = MShare.GetTickCount();
                m_nMaxTick = pm.ActWalk.usetick;
                m_nCurTick = 0;
                m_nMoveStep = 1;
                Shift(m_btDir, m_nMoveStep, 0, m_nEndFrame - m_nStartFrame + 1);
                break;
            case Grobal2.SM_DIGUP:
                if (m_wAppearance == 330 || m_wAppearance == 336)
                {
                    m_nStartFrame = 4;
                    m_nEndFrame = m_nStartFrame + 6 - 1;
                    m_dwFrameTime = 120;
                    m_dwStartTime = MShare.GetTickCount();
                    Shift(m_btDir, 0, 0, 1);
                }
                else
                {
                    m_nStartFrame = pm.ActDeath.start;
                    m_nEndFrame = m_nStartFrame + pm.ActDeath.frame - 1;
                    m_dwFrameTime = pm.ActDeath.ftime;
                    m_dwStartTime = MShare.GetTickCount();
                    Shift(m_btDir, 0, 0, 1);
                }

                break;
            case Grobal2.SM_SPELL:
                m_nStartFrame = pm.ActAttack.start + m_btDir * (pm.ActAttack.frame + pm.ActAttack.skip);
                m_nEndFrame = m_nStartFrame + pm.ActAttack.frame - 1;
                m_dwFrameTime = pm.ActAttack.ftime;
                m_dwStartTime = MShare.GetTickCount();
                m_nCurEffFrame = 0;
                m_boUseMagic = true;
                m_nMagLight = 2;
                m_nSpellFrame = pm.ActCritical.frame;
                m_dwWaitMagicRequest = MShare.GetTickCount();
                m_boWarMode = true;
                m_dwWarModeTime = MShare.GetTickCount();
                Shift(m_btDir, 0, 0, 1);
                break;
            case Grobal2.SM_HIT:
            case Grobal2.SM_FLYAXE:
            case Grobal2.SM_LIGHTING:
                m_nStartFrame = pm.ActAttack.start + m_btDir * (pm.ActAttack.frame + pm.ActAttack.skip);
                m_nEndFrame = m_nStartFrame + pm.ActAttack.frame - 1;
                m_dwFrameTime = pm.ActAttack.ftime;
                m_dwStartTime = MShare.GetTickCount();
                m_dwWarModeTime = MShare.GetTickCount();
                m_boUseEffect = true;
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
                if (m_btRace != 22) m_boUseEffect = true;
                break;
            case Grobal2.SM_ALIVE:
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
            result = pm.ActStand.start + m_btDir * (pm.ActStand.frame + pm.ActStand.skip) + cf;
        }

        return result;
    }
}