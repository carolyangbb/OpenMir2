namespace RobotSvr
{
    public class TDragonBody : TKillingHerb
    {
        public override void CalcActorFrame()
        {
            TMonsterAction pm;
            this.m_btDir = 0;
            this.m_boUseMagic = false;
            this.m_nCurrentFrame = -1;
            this.m_nBodyOffset = Actor.GetOffset(this.m_wAppearance);
            pm = Actor.GetRaceByPM(this.m_btRace, this.m_wAppearance);
            if (pm == null)
            {
                return;
            }
            switch (this.m_nCurrentAction)
            {
                case Grobal2.SM_DIGUP:
                    this.m_nMaxTick = pm.ActWalk.ftime;
                    this.m_nCurTick = 0;
                    this.m_nMoveStep = 1;
                    this.Shift(this.m_btDir, 0, 0, 1);
                    break;
                case Grobal2.SM_HIT:
                    AttackEff();
                    break;
            }
            this.m_nStartFrame = 0;
            this.m_nEndFrame = 1;
            this.m_dwFrameTime = 400;
            this.m_dwStartTime = MShare.GetTickCount();
        }

        private void AttackEff()
        {
            int n8;
            int nc;
            int iCount;
            n8 = this.m_nCurrX;
            nc = this.m_nCurrY;
            iCount = new System.Random(5).Next();
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
}

