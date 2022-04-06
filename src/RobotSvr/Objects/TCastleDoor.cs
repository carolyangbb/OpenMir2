using SystemModule;

namespace RobotSvr
{
    public class TCastleDoor : TActor
    {
        private readonly int ax = 0;
        private readonly int ay = 0;
        private readonly int oldunitx = 0;
        private readonly int oldunity = 0;
        public bool BoDoorOpen = false;

        public TCastleDoor() : base()
        {
            this.m_btDir = 0;
            this.m_nDownDrawLevel = 1;
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
            this.m_boUseEffect = false;
            this.m_nCurrentFrame = -1;
            this.m_nBodyOffset = Actor.GetOffset(this.m_wAppearance);
            pm = Actor.GetRaceByPM(this.m_btRace, this.m_wAppearance);
            if (pm == null)
            {
                return;
            }
            this.m_sUserName = " ";
            switch (this.m_nCurrentAction)
            {
                case Grobal2.SM_NOWDEATH:
                    this.m_nStartFrame = pm.ActDie.start;
                    this.m_nEndFrame = this.m_nStartFrame + pm.ActDie.frame - 1;
                    this.m_dwFrameTime = pm.ActDie.ftime;
                    this.m_dwStartTime = MShare.GetTickCount();
                    this.Shift(this.m_btDir, 0, 0, 1);
                    this.m_boUseEffect = true;
                    ApplyDoorState(TDoorState.dsBroken);
                    break;
                case Grobal2.SM_STRUCK:
                    this.m_nStartFrame = pm.ActStruck.start + this.m_btDir * (pm.ActStruck.frame + pm.ActStruck.skip);
                    this.m_nEndFrame = this.m_nStartFrame + pm.ActStruck.frame - 1;
                    this.m_dwFrameTime = pm.ActStand.ftime;
                    this.m_dwStartTime = MShare.GetTickCount();
                    this.Shift(this.m_btDir, 0, 0, 1);
                    break;
                case Grobal2.SM_DIGUP:
                    this.m_nStartFrame = pm.ActAttack.start;
                    this.m_nEndFrame = this.m_nStartFrame + pm.ActAttack.frame - 1;
                    this.m_dwFrameTime = pm.ActAttack.ftime;
                    this.m_dwStartTime = MShare.GetTickCount();
                    ApplyDoorState(TDoorState.dsOpen);
                    break;
                case Grobal2.SM_DIGDOWN:
                    this.m_nStartFrame = pm.ActCritical.start;
                    this.m_nEndFrame = this.m_nStartFrame + pm.ActCritical.frame - 1;
                    this.m_dwFrameTime = pm.ActCritical.ftime;
                    this.m_dwStartTime = MShare.GetTickCount();
                    BoDoorOpen = false;
                    this.m_boHoldPlace = true;
                    ApplyDoorState(TDoorState.dsClose);
                    break;
                case Grobal2.SM_DEATH:
                    this.m_nStartFrame = pm.ActDie.start + pm.ActDie.frame - 1;
                    this.m_nEndFrame = this.m_nStartFrame;
                    this.m_nDefFrameCount = 0;
                    ApplyDoorState(TDoorState.dsBroken);
                    break;
                default:
                    if (this.m_btDir < 3)
                    {
                        this.m_nStartFrame = pm.ActStand.start + this.m_btDir * (pm.ActStand.frame + pm.ActStand.skip);
                        this.m_nEndFrame = this.m_nStartFrame;
                        this.m_dwFrameTime = pm.ActStand.ftime;
                        this.m_dwStartTime = MShare.GetTickCount();
                        this.m_nDefFrameCount = 0;
                        this.Shift(this.m_btDir, 0, 0, 1);
                        BoDoorOpen = false;
                        this.m_boHoldPlace = true;
                        ApplyDoorState(TDoorState.dsClose);
                    }
                    else
                    {
                        this.m_nStartFrame = pm.ActCritical.start;
                        this.m_nEndFrame = this.m_nStartFrame;
                        this.m_nDefFrameCount = 0;
                        BoDoorOpen = true;
                        this.m_boHoldPlace = false;
                        ApplyDoorState(TDoorState.dsOpen);
                    }
                    break;
            }
        }

        public override int GetDefaultFrame(bool wmode)
        {
            int result = 0;
            this.m_nBodyOffset = Actor.GetOffset(this.m_wAppearance);
            TMonsterAction pm = Actor.GetRaceByPM(this.m_btRace, this.m_wAppearance);
            if (pm == null)
            {
                return result;
            }
            if (this.m_boDeath)
            {
                result = pm.ActDie.start + pm.ActDie.frame - 1;
                this.m_nDownDrawLevel = 2;
            }
            else
            {
                if (BoDoorOpen)
                {
                    this.m_nDownDrawLevel = 2;
                    result = pm.ActCritical.start;
                }
                else
                {
                    this.m_nDownDrawLevel = 1;
                    result = pm.ActStand.start + this.m_btDir * (pm.ActStand.frame + pm.ActStand.skip);
                }
            }
            return result;
        }

        public override void ActionEnded()
        {
            if (this.m_nCurrentAction == Grobal2.SM_DIGUP)
            {
                BoDoorOpen = true;
                this.m_boHoldPlace = false;
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

