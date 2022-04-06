using SystemModule;

namespace RobotSvr;

public class TFrostTiger : TSkeletonOma
{
    public bool boActive;
    public bool boCasted;

    public TFrostTiger(RobotClient robotClient) : base(robotClient)
    {
        boActive = false;
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
            case Grobal2.SM_LIGHTING:
                m_nStartFrame = pm.ActCritical.start + m_btDir * (pm.ActCritical.frame + pm.ActCritical.skip);
                m_nEndFrame = m_nStartFrame + pm.ActCritical.frame - 1;
                m_dwFrameTime = pm.ActCritical.ftime;
                m_dwStartTime = MShare.GetTickCount();
                m_dwWarModeTime = MShare.GetTickCount();
                boCasted = true;
                Shift(m_btDir, 0, 0, 1);
                break;
            case Grobal2.SM_DIGDOWN:
                m_nStartFrame = pm.ActDeath.start + m_btDir * (pm.ActDeath.frame + pm.ActDeath.skip);
                m_nEndFrame = m_nStartFrame + pm.ActDeath.frame - 1;
                m_dwFrameTime = pm.ActDeath.ftime;
                m_dwStartTime = MShare.GetTickCount();
                m_dwWarModeTime = MShare.GetTickCount();
                boActive = false;
                break;
            case Grobal2.SM_DIGUP:
                boActive = true;
                break;
            case Grobal2.SM_WALK:
                boActive = true;
                base.CalcActorFrame();
                break;
            default:
                base.CalcActorFrame();
                break;
        }
    }

    public override void Run()
    {
        if (m_nCurrentAction == Grobal2.SM_LIGHTING && boCasted)
            boCasted = false;
        //ClMain.g_PlayScene.NewMagic(this, 1, 39, this.m_nCurrX, this.m_nCurrY, this.m_nTargetX, this.m_nTargetY, this.m_nTargetRecog, magiceff.TMagicType.mtFly, false, 30, ref bofly);
        base.Run();
    }

    public override int GetDefaultFrame(bool wmode)
    {
        int result;
        int cf;
        TMonsterAction pm;
        result = 0;
        if (boActive == false)
        {
            pm = Actor.GetRaceByPM(m_btRace, m_wAppearance);
            if (pm == null) return result;
            if (m_boDeath)
            {
                base.GetDefaultFrame(wmode);
                return result;
            }

            m_nDefFrameCount = pm.ActDeath.frame;
            if (m_nCurrentDefFrame < 0)
                cf = 0;
            else if (m_nCurrentDefFrame >= pm.ActDeath.frame)
                cf = 0;
            else
                cf = m_nCurrentDefFrame;
            result = pm.ActDeath.start + m_btDir * (pm.ActDeath.frame + pm.ActDeath.skip) + cf;
        }
        else
        {
            result = base.GetDefaultFrame(wmode);
        }

        return result;
    }
}