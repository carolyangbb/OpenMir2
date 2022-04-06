using SystemModule;

namespace RobotSvr
{
    public class TCentipedeKingMon : TKillingHerb
    {
        private bool BoUseDieEffect = false;
        private readonly int ax = 0;
        private readonly int ay = 0;

        public override void CalcActorFrame()
        {
            TMonsterAction pm;
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
                case Grobal2.SM_TURN:
                    this.m_btDir = 0;
                    base.CalcActorFrame();
                    break;
                case Grobal2.SM_HIT:
                    this.m_btDir = 0;
                    this.m_nStartFrame = pm.ActCritical.start + this.m_btDir * (pm.ActCritical.frame + pm.ActCritical.skip);
                    this.m_nEndFrame = this.m_nStartFrame + pm.ActCritical.frame - 1;
                    this.m_dwFrameTime = pm.ActCritical.ftime;
                    this.m_dwStartTime = MShare.GetTickCount();
                    BoUseDieEffect = true;
                    this.m_nEffectFrame = 0;
                    this.m_nEffectStart = 0;
                    this.m_nEffectEnd = this.m_nEffectStart + 9;
                    this.m_dwEffectFrameTime = 62;
                    this.Shift(this.m_btDir, 0, 0, 1);
                    break;
                case Grobal2.SM_DIGDOWN:
                    base.CalcActorFrame();
                    break;
                default:
                    this.m_btDir = 0;
                    base.CalcActorFrame();
                    break;
            }
        }

        public override void Run()
        {
            if ((this.m_nCurrentAction == Grobal2.SM_WALK) || (this.m_nCurrentAction == Grobal2.SM_BACKSTEP) || (this.m_nCurrentAction == Grobal2.SM_HORSERUN) || (this.m_nCurrentAction == Grobal2.SM_RUN))
            {
                return;
            }
            if (BoUseDieEffect)
            {
                if ((this.m_nCurrentFrame - this.m_nStartFrame) >= 5)
                {
                    BoUseDieEffect = false;
                    this.m_boUseEffect = true;
                    this.m_dwEffectStartTime = MShare.GetTickCount();
                    this.m_nEffectFrame = 0;
                }
            }
            if (this.m_boUseEffect)
            {
                if ((MShare.GetTickCount() - this.m_dwEffectStartTime) > this.m_dwEffectFrameTime)
                {
                    this.m_dwEffectStartTime = MShare.GetTickCount();
                    if (this.m_nEffectFrame < this.m_nEffectEnd)
                    {
                        this.m_nEffectFrame++;
                    }
                    else
                    {
                        this.m_boUseEffect = false;
                    }
                }
            }
            base.Run();
        }

    }
}

