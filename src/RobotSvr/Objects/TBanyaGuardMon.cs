using System.Collections;
using SystemModule;

namespace RobotSvr
{
    public class TBanyaGuardMon : TSkeletonArcherMon
    {
        public override void CalcActorFrame()
        {
            this.m_nCurrentFrame = -1;
            this.m_nBodyOffset = Actor.GetOffset(this.m_wAppearance);
            TMonsterAction pm = Actor.GetRaceByPM(this.m_btRace, this.m_wAppearance);
            if (pm == null)
            {
                return;
            }
            switch (this.m_nCurrentAction)
            {
                case Grobal2.SM_HIT:
                    if (this.m_btRace >= 117 && this.m_btRace <= 119)
                    {
                        this.m_nStartFrame = pm.ActAttack.start;
                    }
                    else
                    {
                        this.m_nStartFrame = pm.ActAttack.start + this.m_btDir * (pm.ActAttack.frame + pm.ActAttack.skip);
                    }
                    this.m_nEndFrame = this.m_nStartFrame + pm.ActAttack.frame - 1;
                    this.m_dwFrameTime = pm.ActAttack.ftime;
                    this.m_dwStartTime = MShare.GetTickCount();
                    this.m_dwWarModeTime = MShare.GetTickCount();
                    this.Shift(this.m_btDir, 0, 0, 1);
                    if (!new ArrayList(new int[] { 27, 28, 111 }).Contains(this.m_btRace))
                    {
                        this.m_boUseEffect = true;
                        this.m_nEffectFrame = this.m_nStartFrame;
                        this.m_nEffectStart = this.m_nStartFrame;
                        this.m_nEffectEnd = this.m_nEndFrame;
                        this.m_dwEffectStartTime = MShare.GetTickCount();
                        this.m_dwEffectFrameTime = this.m_dwFrameTime;
                    }
                    if (new ArrayList(new int[] { 113, 114, 115 }).Contains(this.m_btRace))
                    {
                        this.m_boUseEffect = false;
                    }
                    break;
                case Grobal2.SM_LIGHTING:
                    if (this.m_btRace >= 117 && this.m_btRace <= 119)
                    {
                        this.m_nStartFrame = pm.ActCritical.start;
                    }
                    else
                    {
                        this.m_nStartFrame = pm.ActCritical.start + this.m_btDir * (pm.ActCritical.frame + pm.ActCritical.skip);
                    }
                    this.m_nEndFrame = this.m_nStartFrame + pm.ActCritical.frame - 1;
                    this.m_dwFrameTime = pm.ActCritical.ftime;
                    this.m_dwStartTime = MShare.GetTickCount();
                    this.m_nCurEffFrame = 0;
                    this.m_boUseMagic = true;
                    this.m_dwWarModeTime = MShare.GetTickCount();
                    this.Shift(this.m_btDir, 0, 0, 1);
                    if (new ArrayList(new int[] { 71, 72, 111 }).Contains(this.m_btRace))
                    {
                        this.m_boUseEffect = true;
                        this.m_nEffectFrame = this.m_nStartFrame;
                        this.m_nEffectStart = this.m_nStartFrame;
                        this.m_nEffectEnd = this.m_nEndFrame;
                        this.m_dwEffectStartTime = MShare.GetTickCount();
                        this.m_dwEffectFrameTime = this.m_dwFrameTime;
                    }
                    else if (new ArrayList(new int[] { 27, 28 }).Contains(this.m_btRace))
                    {
                        this.m_boUseEffect = true;
                        this.m_nEffectFrame = this.m_nStartFrame;
                        this.m_nEffectStart = this.m_nStartFrame;
                        this.m_nEffectEnd = this.m_nEndFrame;
                        if (this.m_btRace == 28)
                        {
                            this.m_nEffectEnd = this.m_nEndFrame - 4;
                        }
                        this.m_dwEffectStartTime = MShare.GetTickCount();
                        this.m_dwEffectFrameTime = this.m_dwFrameTime;
                    }
                    else if (new ArrayList(new int[] { 113, 114 }).Contains(this.m_btRace))
                    {
                        this.m_nStartFrame = pm.ActAttack.start + this.m_btDir * (pm.ActAttack.frame + pm.ActAttack.skip);
                        this.m_nEndFrame = this.m_nStartFrame + pm.ActAttack.frame - 1;
                        this.m_dwFrameTime = pm.ActAttack.ftime;
                        if (this.m_btRace == 113)
                        {
                            this.m_boUseEffect = true;
                            this.m_nEffectFrame = 350 + this.m_btDir * 10;
                            this.m_nEffectStart = this.m_nEffectFrame;
                            this.m_nEffectEnd = this.m_nEffectFrame + 5;
                            this.m_dwEffectStartTime = MShare.GetTickCount();
                            this.m_dwEffectFrameTime = this.m_dwFrameTime;
                        }
                    }
                    else if (this.m_btRace == 117)
                    {
                        this.m_boUseEffect = true;
                        this.m_nEffectFrame = this.m_nStartFrame + 15;
                        this.m_nEffectStart = this.m_nEffectFrame;
                        this.m_nEffectEnd = this.m_nEffectFrame + 3;
                        this.m_dwEffectStartTime = MShare.GetTickCount();
                        this.m_dwEffectFrameTime = this.m_dwFrameTime;
                    }
                    else if (new ArrayList(new int[] { 118, 119 }).Contains(this.m_btRace))
                    {
                        this.m_boUseEffect = true;
                        this.m_boUseMagic = false;
                        this.m_dwEffectStartTime = MShare.GetTickCount();
                        this.m_dwEffectFrameTime = this.m_dwFrameTime;
                        if (this.m_btRace == 118)
                        {
                        }
                        this.m_nEffectFrame = 2750;
                        this.m_nEffectStart = 2750;
                        this.m_nEffectEnd = 2750 + 9;
                        if (this.m_btRace == 119)
                        {
                            this.m_nEffectFrame = 2860;
                            this.m_nEffectStart = 2860;
                            this.m_nEffectEnd = 2860 + 9;
                        }
                    }
                    break;
                default:
                    base.CalcActorFrame();
                    break;
            }
        }
    }
}