using SystemModule;

namespace RobotSvr
{
    public class TWhiteSkeleton : TSkeletonOma
    {
        public override void CalcActorFrame()
        {
            base.CalcActorFrame();
            this.m_boUseMagic = false;
            this.m_nCurrentFrame = -1;
            this.m_nHitEffectNumber = 0;
            this.m_nBodyOffset = Actor.GetOffset(this.m_wAppearance);
            this.m_Action = Actor.GetRaceByPM(this.m_btRace, this.m_wAppearance);
            if (this.m_Action == null)
            {
                return;
            }
            switch (this.m_nCurrentAction)
            {
                case Grobal2.SM_POWERHIT:
                    this.m_nStartFrame = this.m_Action.ActAttack.start + this.m_btDir * (this.m_Action.ActAttack.frame + this.m_Action.ActAttack.skip);
                    this.m_nEndFrame = this.m_nStartFrame + this.m_Action.ActAttack.frame - 1;
                    this.m_dwFrameTime = this.m_Action.ActAttack.ftime;
                    this.m_dwStartTime = MShare.GetTickCount();
                    this.m_dwWarModeTime = MShare.GetTickCount();
                    if ((this.m_nCurrentAction == Grobal2.SM_POWERHIT))
                    {
                        this.m_boHitEffect = true;
                        this.m_nMagLight = 2;
                        this.m_nHitEffectNumber = 1;
                        switch (this.m_btRace)
                        {
                            case 91:
                                this.m_nHitEffectNumber += 101;
                                break;
                            case 92:
                                this.m_nHitEffectNumber += 201;
                                break;
                            case 93:
                                this.m_nHitEffectNumber += 301;
                                break;
                        }
                    }
                    this.Shift(this.m_btDir, 0, 0, 1);
                    break;
            }
        }
    }
}