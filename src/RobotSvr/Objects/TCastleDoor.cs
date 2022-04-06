using SystemModule;

namespace RobotSvr
{
    public class TCastleDoor : TActor
    {
        private readonly int ax = 0;
        private readonly int ay = 0;
        private readonly int oldunitx = 0;
        private readonly int oldunity = 0;
        public bool BoDoorOpen;

        public TCastleDoor(RobotClient robotClient) : base(robotClient)
        {
            m_btDir = 0;
            m_nDownDrawLevel = 1;
        }

        private void ApplyDoorState(TDoorState dstate)
        {
            //bool bowalk;
            //ClMain.Map.MarkCanWalk(this.m_nCurrX, this.m_nCurrY - 2, true);
            //ClMain.Map.MarkCanWalk(this.m_nCurrX + 1, this.m_nCurrY - 1, true);
            //ClMain.Map.MarkCanWalk(this.m_nCurrX + 1, this.m_nCurrY - 2, true);
            //if (dstate == TDoorState.dsClose)
            //{
            //    bowalk = false;
            //}
            //else
            //{
            //    bowalk = true;
            //}
            //ClMain.Map.MarkCanWalk(this.m_nCurrX, this.m_nCurrY, bowalk);
            //ClMain.Map.MarkCanWalk(this.m_nCurrX, this.m_nCurrY - 1, bowalk);
            //ClMain.Map.MarkCanWalk(this.m_nCurrX, this.m_nCurrY - 2, bowalk);
            //ClMain.Map.MarkCanWalk(this.m_nCurrX + 1, this.m_nCurrY - 1, bowalk);
            //ClMain.Map.MarkCanWalk(this.m_nCurrX + 1, this.m_nCurrY - 2, bowalk);
            //ClMain.Map.MarkCanWalk(this.m_nCurrX - 1, this.m_nCurrY - 1, bowalk);
            //ClMain.Map.MarkCanWalk(this.m_nCurrX - 1, this.m_nCurrY, bowalk);
            //ClMain.Map.MarkCanWalk(this.m_nCurrX - 1, this.m_nCurrY + 1, bowalk);
            //ClMain.Map.MarkCanWalk(this.m_nCurrX - 2, this.m_nCurrY, bowalk);
            //if (dstate == TDoorState.dsOpen)
            //{
            //    ClMain.Map.MarkCanWalk(this.m_nCurrX, this.m_nCurrY - 2, false);
            //    ClMain.Map.MarkCanWalk(this.m_nCurrX + 1, this.m_nCurrY - 1, false);
            //    ClMain.Map.MarkCanWalk(this.m_nCurrX + 1, this.m_nCurrY - 2, false);
            //}
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
            switch (m_nCurrentAction)
            {
                case Grobal2.SM_NOWDEATH:
                    m_nStartFrame = pm.ActDie.start;
                    m_nEndFrame = m_nStartFrame + pm.ActDie.frame - 1;
                    m_dwFrameTime = pm.ActDie.ftime;
                    m_dwStartTime = MShare.GetTickCount();
                    Shift(m_btDir, 0, 0, 1);
                    m_boUseEffect = true;
                    ApplyDoorState(TDoorState.dsBroken);
                    break;
                case Grobal2.SM_STRUCK:
                    m_nStartFrame = pm.ActStruck.start + m_btDir * (pm.ActStruck.frame + pm.ActStruck.skip);
                    m_nEndFrame = m_nStartFrame + pm.ActStruck.frame - 1;
                    m_dwFrameTime = pm.ActStand.ftime;
                    m_dwStartTime = MShare.GetTickCount();
                    Shift(m_btDir, 0, 0, 1);
                    break;
                case Grobal2.SM_DIGUP:
                    m_nStartFrame = pm.ActAttack.start;
                    m_nEndFrame = m_nStartFrame + pm.ActAttack.frame - 1;
                    m_dwFrameTime = pm.ActAttack.ftime;
                    m_dwStartTime = MShare.GetTickCount();
                    ApplyDoorState(TDoorState.dsOpen);
                    break;
                case Grobal2.SM_DIGDOWN:
                    m_nStartFrame = pm.ActCritical.start;
                    m_nEndFrame = m_nStartFrame + pm.ActCritical.frame - 1;
                    m_dwFrameTime = pm.ActCritical.ftime;
                    m_dwStartTime = MShare.GetTickCount();
                    BoDoorOpen = false;
                    m_boHoldPlace = true;
                    ApplyDoorState(TDoorState.dsClose);
                    break;
                case Grobal2.SM_DEATH:
                    m_nStartFrame = pm.ActDie.start + pm.ActDie.frame - 1;
                    m_nEndFrame = m_nStartFrame;
                    m_nDefFrameCount = 0;
                    ApplyDoorState(TDoorState.dsBroken);
                    break;
                default:
                    if (m_btDir < 3)
                    {
                        m_nStartFrame = pm.ActStand.start + m_btDir * (pm.ActStand.frame + pm.ActStand.skip);
                        m_nEndFrame = m_nStartFrame;
                        m_dwFrameTime = pm.ActStand.ftime;
                        m_dwStartTime = MShare.GetTickCount();
                        m_nDefFrameCount = 0;
                        Shift(m_btDir, 0, 0, 1);
                        BoDoorOpen = false;
                        m_boHoldPlace = true;
                        ApplyDoorState(TDoorState.dsClose);
                    }
                    else
                    {
                        m_nStartFrame = pm.ActCritical.start;
                        m_nEndFrame = m_nStartFrame;
                        m_nDefFrameCount = 0;
                        BoDoorOpen = true;
                        m_boHoldPlace = false;
                        ApplyDoorState(TDoorState.dsOpen);
                    }

                    break;
            }
        }

        public override int GetDefaultFrame(bool wmode)
        {
            var result = 0;
            m_nBodyOffset = Actor.GetOffset(m_wAppearance);
            var pm = Actor.GetRaceByPM(m_btRace, m_wAppearance);
            if (pm == null) return result;
            if (m_boDeath)
            {
                result = pm.ActDie.start + pm.ActDie.frame - 1;
                m_nDownDrawLevel = 2;
            }
            else
            {
                if (BoDoorOpen)
                {
                    m_nDownDrawLevel = 2;
                    result = pm.ActCritical.start;
                }
                else
                {
                    m_nDownDrawLevel = 1;
                    result = pm.ActStand.start + m_btDir * (pm.ActStand.frame + pm.ActStand.skip);
                }
            }

            return result;
        }

        public override void ActionEnded()
        {
            if (m_nCurrentAction == Grobal2.SM_DIGUP)
            {
                BoDoorOpen = true;
                m_boHoldPlace = false;
            }
        }

        public override void Run()
        {
            //if ((ClMain.Map.m_nCurUnitX != oldunitx) || (ClMain.Map.m_nCurUnitY != oldunity))
            //{
            //    if (this.m_boDeath)
            //    {
            //        ApplyDoorState(TDoorState.dsBroken);
            //    }
            //    else if (BoDoorOpen)
            //    {
            //        ApplyDoorState(TDoorState.dsOpen);
            //    }
            //    else
            //    {
            //        ApplyDoorState(TDoorState.dsClose);
            //    }
            //}
            //oldunitx = ClMain.Map.m_nCurUnitX;
            //oldunity = ClMain.Map.m_nCurUnitY;
            base.Run();
        }
    }
}