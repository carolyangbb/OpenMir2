using SystemModule;

namespace RobotSvr;

public class TWallStructure : TActor
{
    private const int V = 0;
    private readonly int ax = 0;
    private readonly int ay = 0;
    private readonly int bx = 0;
    private readonly int by = 0;
    private bool bomarkpos;
    private int deathframe;

    public TWallStructure(RobotClient robotClient) : base(robotClient)
    {
        m_btDir = 0;
        bomarkpos = false;
    }

    public override void CalcActorFrame()
    {
        TMonsterAction pm;
        m_boUseEffect = false;
        m_nCurrentFrame = -1;
        m_nBodyOffset = Actor.GetOffset(m_wAppearance);
        pm = Actor.GetRaceByPM(m_btRace, m_wAppearance);
        if (pm == null) return;
        m_sUserName = " ";
        deathframe = 0;
        m_boUseEffect = false;
        switch (m_nCurrentAction)
        {
            case Grobal2.SM_NOWDEATH:
                m_nStartFrame = pm.ActDie.start;
                m_nEndFrame = m_nStartFrame + pm.ActDie.frame - 1;
                m_dwFrameTime = pm.ActDie.ftime;
                m_dwStartTime = MShare.GetTickCount();
                deathframe = pm.ActStand.start + m_btDir;
                Shift(m_btDir, 0, 0, 1);
                m_boUseEffect = true;
                break;
            case Grobal2.SM_DEATH:
                m_nStartFrame = pm.ActDie.start + pm.ActDie.frame - 1;
                m_nEndFrame = m_nStartFrame;
                m_nDefFrameCount = 0;
                break;
            case Grobal2.SM_DIGUP:
                m_nStartFrame = pm.ActDie.start;
                m_nEndFrame = m_nStartFrame + pm.ActDie.frame - 1;
                m_dwFrameTime = pm.ActDie.ftime;
                m_dwStartTime = MShare.GetTickCount();
                deathframe = pm.ActStand.start + m_btDir;
                m_boUseEffect = true;
                break;
            default:
                m_nStartFrame = pm.ActStand.start + m_btDir;
                m_nEndFrame = m_nStartFrame;
                m_dwFrameTime = pm.ActStand.ftime;
                m_dwStartTime = MShare.GetTickCount();
                m_nDefFrameCount = 0;
                Shift(m_btDir, 0, 0, 1);
                m_boHoldPlace = true;
                break;
        }
    }

    public override int GetDefaultFrame(bool wmode)
    {
        var result = V;
        m_nBodyOffset = Actor.GetOffset(m_wAppearance);
        var pm = Actor.GetRaceByPM(m_btRace, m_wAppearance);
        if (pm == null) return result;
        result = pm.ActStand.start + m_btDir;
        return result;
    }

    public override void Run()
    {
        if (m_boDeath)
        {
            if (bomarkpos)
            {
                robotClient.Map.MarkCanWalk(m_nCurrX, m_nCurrY, true);
                bomarkpos = false;
            }
        }
        else
        {
            if (!bomarkpos)
            {
                robotClient.Map.MarkCanWalk(m_nCurrX, m_nCurrY, false);
                bomarkpos = true;
            }
        }

        robotClient.g_PlayScene.SetActorDrawLevel(this, 0);
        base.Run();
    }
}