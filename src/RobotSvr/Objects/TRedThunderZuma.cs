using SystemModule;

namespace RobotSvr;

public class TRedThunderZuma : TGasKuDeGi
{
    public bool boCasted;

    public TRedThunderZuma(RobotClient robotClient) : base(robotClient)
    {
        boCasted = false;
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
            case Grobal2.SM_TURN:
                if ((m_nState & Grobal2.STATE_STONE_MODE) != 0)
                {
                    m_nStartFrame = pm.ActDeath.start + m_btDir * (pm.ActDeath.frame + pm.ActDeath.skip);
                    m_nEndFrame = m_nStartFrame;
                    m_dwFrameTime = pm.ActDeath.ftime;
                    m_dwStartTime = MShare.GetTickCount();
                    m_nDefFrameCount = pm.ActDeath.frame;
                }
                else
                {
                    base.CalcActorFrame();
                }

                break;
            case Grobal2.SM_LIGHTING:
                m_nStartFrame = pm.ActCritical.start + m_btDir * (pm.ActCritical.frame + pm.ActCritical.skip);
                m_nEndFrame = m_nStartFrame + pm.ActCritical.frame - 1;
                m_dwFrameTime = pm.ActCritical.ftime;
                m_dwStartTime = MShare.GetTickCount();
                m_dwWarModeTime = MShare.GetTickCount();
                firedir = m_btDir;
                Shift(m_btDir, 0, 0, 1);
                m_boUseEffect = true;
                m_nEffectStart = 0;
                m_nEffectFrame = 0;
                m_nEffectEnd = 6;
                m_dwEffectStartTime = MShare.GetTickCount();
                m_dwEffectFrameTime = 150;
                m_nCurEffFrame = 0;
                boCasted = true;
                break;
            case Grobal2.SM_DIGUP:
                m_nStartFrame = pm.ActDeath.start + m_btDir * (pm.ActDeath.frame + pm.ActDeath.skip);
                m_nEndFrame = m_nStartFrame + pm.ActDeath.frame - 1;
                m_dwFrameTime = pm.ActDeath.ftime;
                m_dwStartTime = MShare.GetTickCount();
                m_dwWarModeTime = MShare.GetTickCount();
                Shift(m_btDir, 0, 0, 1);
                break;
            default:
                base.CalcActorFrame();
                break;
        }
    }

    public override void Run()
    {
        bool bofly;
        if (m_nCurrentFrame - m_nStartFrame == 2)
        {
            if (m_nCurrentAction == Grobal2.SM_LIGHTING)
            {
                if (boCasted)
                {
                    boCasted = false;
                    //ClMain.g_PlayScene.NewMagic(this, 80, 80, m_nCurrX, m_nCurrY, m_nTargetX, m_nTargetY,m_nTargetRecog, magiceff.TMagicType.mtRedThunder, false, 30, ref bofly);
                }
            }
        }
        base.Run();
    }

    public override int GetDefaultFrame(bool wmode)
    {
        int result;
        TMonsterAction pm;
        pm = Actor.GetRaceByPM(m_btRace, m_wAppearance);
        if ((m_nState & Grobal2.STATE_STONE_MODE) != 0)
            result = pm.ActDeath.start + m_btDir * (pm.ActDeath.frame + pm.ActDeath.skip);
        else
            result = base.GetDefaultFrame(wmode);
        return result;
    }
}