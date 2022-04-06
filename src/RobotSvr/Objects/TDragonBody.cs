using System;
using SystemModule;

namespace RobotSvr;

public class TDragonBody : TKillingHerb
{
    public TDragonBody(RobotClient robotClient) : base(robotClient)
    {
    }

    public override void CalcActorFrame()
    {
        TMonsterAction pm;
        m_btDir = 0;
        m_boUseMagic = false;
        m_nCurrentFrame = -1;
        m_nBodyOffset = Actor.GetOffset(m_wAppearance);
        pm = Actor.GetRaceByPM(m_btRace, m_wAppearance);
        if (pm == null) return;
        switch (m_nCurrentAction)
        {
            case Grobal2.SM_DIGUP:
                m_nMaxTick = pm.ActWalk.ftime;
                m_nCurTick = 0;
                m_nMoveStep = 1;
                Shift(m_btDir, 0, 0, 1);
                break;
            case Grobal2.SM_HIT:
                AttackEff();
                break;
        }

        m_nStartFrame = 0;
        m_nEndFrame = 1;
        m_dwFrameTime = 400;
        m_dwStartTime = MShare.GetTickCount();
    }

    private void AttackEff()
    {
        int n8;
        int nc;
        int iCount;
        n8 = m_nCurrX;
        nc = m_nCurrY;
        iCount = new Random(5).Next();
        //for (i = 0; i <= iCount; i++)
        //{
        //    n10 = (new System.Random(4)).Next();
        //    n14 = (new System.Random(8)).Next();
        //    n18 = (new System.Random(8)).Next();
        //    switch (n10)
        //    {
        //        case 0:
        //            ClMain.g_PlayScene.NewMagic(this, 80, 80, this.m_nCurrX, this.m_nCurrY, n8 - n14 - 2, nc + n18 + 1, 0, magiceff.TMagicType.mtRedThunder, false, 30, ref bo11);
        //            break;
        //        case 1:
        //            ClMain.g_PlayScene.NewMagic(this, 80, 80, this.m_nCurrX, this.m_nCurrY, n8 - n14, nc + n18, 0, magiceff.TMagicType.mtRedThunder, false, 30, ref bo11);
        //            break;
        //        case 2:
        //            ClMain.g_PlayScene.NewMagic(this, 80, 80, this.m_nCurrX, this.m_nCurrY, n8 - n14, nc + n18 + 1, 0, magiceff.TMagicType.mtRedThunder, false, 30, ref bo11);
        //            break;
        //        case 3:
        //            ClMain.g_PlayScene.NewMagic(this, 80, 80, this.m_nCurrX, this.m_nCurrY, n8 - n14 - 2, nc + n18, 0, magiceff.TMagicType.mtRedThunder, false, 30, ref bo11);
        //            break;
        //    }
        //}
    }
}