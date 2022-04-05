using System;
using System.Collections;
using SystemModule;

namespace RobotSvr
{
    public class TFireDragon : TSkeletonArcherMon
    {
        public override void CalcActorFrame()
        {
            TMonsterAction pm;
            this.m_btDir = 0;
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
                    this.m_dwStartTime = MShare.GetTickCount();
                    this.m_nDefFrameCount = pm.ActStand.frame;
                    this.Shift(this.m_btDir, 0, 0, 1);
                    if (this.m_btRace == 120)
                    {
                        switch (this.m_nTempState)
                        {
                            case 1:
                                this.m_nStartFrame = 0;
                                break;
                            case 2:
                                this.m_nStartFrame = 80;
                                break;
                            case 3:
                                this.m_nStartFrame = 160;
                                break;
                            case 4:
                                this.m_nStartFrame = 240;
                                break;
                            case 5:
                                this.m_nStartFrame = 320;
                                break;
                        }
                        this.m_boWarMode = true;
                        this.m_dwFrameTime = 150;
                        this.m_nEndFrame = this.m_nStartFrame + 19;
                        this.m_dwStartTime = MShare.GetTickCount();
                        this.m_nDefFrameCount = 20;
                        this.m_boUseEffect = true;
                        this.m_dwEffectStartTime = MShare.GetTickCount();
                        this.m_dwEffectFrameTime = 150;
                    }
                    break;
                case Grobal2.SM_DIGUP:
                    this.Shift(0, 0, 0, 1);
                    this.m_nStartFrame = 0;
                    this.m_nEndFrame = 9;
                    this.m_dwFrameTime = 300;
                    this.m_dwStartTime = MShare.GetTickCount();
                    break;
                // Modify the A .. B: Grobal2.SM_LIGHTING, Grobal2.SM_LIGHTING_1 .. Grobal2.SM_LIGHTING_3
                case Grobal2.SM_LIGHTING:
                    if (this.m_btRace == 120)
                    {
                        this.m_nStartFrame = 0;
                        this.m_nEndFrame = 19;
                        this.m_dwFrameTime = 150;
                        this.m_dwStartTime = MShare.GetTickCount();
                        this.m_boUseEffect = true;
                        this.m_nEffectFrame = 0;
                        this.m_nEffectStart = 0;
                        this.m_nEffectEnd = 19;
                        this.m_dwEffectStartTime = MShare.GetTickCount();
                        this.m_dwEffectFrameTime = 150;
                        this.m_nCurEffFrame = 0;
                        this.m_boUseMagic = true;
                        this.m_dwWarModeTime = MShare.GetTickCount();
                        this.Shift(this.m_btDir, 0, 0, 1);
                        if (this.m_btRace == 120)
                        {
                            switch (this.m_nTempState)
                            {
                                case 1:
                                    this.m_nStartFrame = 20;
                                    break;
                                case 2:
                                    this.m_nStartFrame = 100;
                                    break;
                                case 3:
                                    this.m_nStartFrame = 180;
                                    break;
                                case 4:
                                    this.m_nStartFrame = 260;
                                    break;
                                case 5:
                                    this.m_nStartFrame = 340;
                                    break;
                            }
                            this.m_nEndFrame = this.m_nStartFrame + 9;
                            this.m_dwFrameTime = 150;
                            this.m_boUseEffect = true;
                            this.m_dwEffectStartTime = MShare.GetTickCount();
                            this.m_dwEffectFrameTime = 150;
                        }
                    }
                    break;
                case Grobal2.SM_HIT:
                    if (this.m_btRace != 120)
                    {
                        this.m_nStartFrame = 0;
                        this.m_nEndFrame = 19;
                        this.m_dwFrameTime = 150;
                        this.m_dwStartTime = MShare.GetTickCount();
                        this.m_boUseEffect = true;
                        this.m_nEffectStart = 0;
                        this.m_nEffectFrame = 0;
                        this.m_nEffectEnd = 19;
                        this.m_dwEffectStartTime = MShare.GetTickCount();
                        this.m_dwEffectFrameTime = 150;
                        this.m_nCurEffFrame = 0;
                        this.m_boUseMagic = true;
                        this.m_dwWarModeTime = MShare.GetTickCount();
                        this.Shift(this.m_btDir, 0, 0, 1);
                    }
                    break;
                case Grobal2.SM_STRUCK:
                    if (this.m_btRace != 120)
                    {
                        this.m_nStartFrame = 0;
                        this.m_nEndFrame = 9;
                        this.m_dwFrameTime = 300;
                        this.m_dwStartTime = MShare.GetTickCount();
                    }
                    break;
                // Modify the A .. B: 81 .. 83
                case 81:
                    this.m_nStartFrame = 0;
                    this.m_nEndFrame = 5;
                    this.m_dwFrameTime = 150;
                    this.m_dwStartTime = MShare.GetTickCount();
                    this.m_boUseEffect = true;
                    this.m_nEffectStart = 0;
                    this.m_nEffectFrame = 0;
                    this.m_nEffectEnd = 10;
                    this.m_dwEffectStartTime = MShare.GetTickCount();
                    this.m_dwEffectFrameTime = 150;
                    this.m_nCurEffFrame = 0;
                    this.m_boUseMagic = true;
                    this.m_dwWarModeTime = MShare.GetTickCount();
                    this.Shift(this.m_btDir, 0, 0, 1);
                    break;
                case Grobal2.SM_DEATH:
                    if (this.m_btRace != 120)
                    {
                        this.m_nCurrentFrame = 0;
                        this.m_nStartFrame = 80;
                        this.m_nEndFrame = 81;
                        this.m_boUseEffect = false;
                        this.m_boDelActionAfterFinished = true;
                    }
                    break;
                case Grobal2.SM_NOWDEATH:
                    if (this.m_btRace == 120)
                    {
                        this.m_nStartFrame = pm.ActDie.start;
                        this.m_nEndFrame = this.m_nStartFrame + pm.ActDie.frame - 1;
                        this.m_dwFrameTime = pm.ActDie.ftime;
                        this.m_dwStartTime = MShare.GetTickCount();
                        this.m_boUseEffect = true;
                        this.m_nEffectFrame = 420;
                        this.m_nEffectStart = 420;
                        this.m_dwFrameTime = 150;
                        this.m_nEndFrame = this.m_nStartFrame + 17;
                        this.m_dwStartTime = MShare.GetTickCount();
                        this.m_boUseEffect = true;
                        this.m_dwEffectStartTime = MShare.GetTickCount();
                        this.m_dwEffectFrameTime = 150;
                    }
                    else
                    {
                        this.m_nCurrentFrame = 0;
                        this.m_nStartFrame = 80;
                        this.m_nEndFrame = 81;
                        this.m_nCurrentFrame = 0;
                        this.m_boUseEffect = false;
                        this.m_boDelActionAfterFinished = true;
                    }
                    break;
            }
            if (new ArrayList(new int[] { 118, 119, 120 }).Contains(this.m_btRace))
            {
                this.m_boUseEffect = true;
            }
        }

        public TFireDragon() : base()
        {
 
        }

        public void LightningTimerTimer(Object Sender)
        {
            //int tx;
            //int ty;
            //int kx;
            //int ky;
            //bool bofly;
            //if (this.m_btRace == 120)
            //{
            //    if (LightningTimer.Tag == 0)
            //    {
            //        LightningTimer.Tag = LightningTimer.Tag + 1;
            //        LightningTimer.Interval = 10;
            //        return;
            //    }
            //    tx = MShare.g_MySelf.m_nCurrX;
            //    ty = MShare.g_MySelf.m_nCurrY;
            //    kx = new System.Random(7).Next();
            //    ky = new System.Random(5).Next();
            //    if (LightningTimer.Tag == 0)
            //    {
            //        ClMain.g_PlayScene.NewMagic(this, Grobal2.MAGIC_SOULBALL_ATT3_1, Grobal2.MAGIC_SOULBALL_ATT3_1, this.m_nCurrX, this.m_nCurrY, tx, ty, 0, magiceff.TMagicType.mtThunder, false, 30, ref bofly);
            //        ClMain.g_PlayScene.NewMagic(this, Grobal2.MAGIC_SOULBALL_ATT3_2, Grobal2.MAGIC_SOULBALL_ATT3_2, this.m_nCurrX, this.m_nCurrY, tx - 2, ty, 0, magiceff.TMagicType.mtThunder, false, 30, ref bofly);
            //        ClMain.g_PlayScene.NewMagic(this, Grobal2.MAGIC_SOULBALL_ATT3_3, Grobal2.MAGIC_SOULBALL_ATT3_3, this.m_nCurrX, this.m_nCurrY, tx, ty - 2, 0, magiceff.TMagicType.mtThunder, false, 30, ref bofly);
            //        ClMain.g_PlayScene.NewMagic(this, Grobal2.MAGIC_SOULBALL_ATT3_4, Grobal2.MAGIC_SOULBALL_ATT3_4, this.m_nCurrX, this.m_nCurrY, tx - kx, ty - ky, 0, magiceff.TMagicType.mtThunder, false, 30, ref bofly);
            //        LightningTimer.Interval = 500;
            //    }
            //    else if (LightningTimer.Tag == 2)
            //    {
            //        ClMain.g_PlayScene.NewMagic(this, Grobal2.MAGIC_SOULBALL_ATT3_1, Grobal2.MAGIC_SOULBALL_ATT3_1, this.m_nCurrX, this.m_nCurrY, tx - 2, ty - 2, 0, magiceff.TMagicType.mtThunder, false, 30, ref bofly);
            //        ClMain.g_PlayScene.NewMagic(this, Grobal2.MAGIC_SOULBALL_ATT3_2, Grobal2.MAGIC_SOULBALL_ATT3_2, this.m_nCurrX, this.m_nCurrY, tx + 2, ty - 2, 0, magiceff.TMagicType.mtThunder, false, 30, ref bofly);
            //        ClMain.g_PlayScene.NewMagic(this, Grobal2.MAGIC_SOULBALL_ATT3_3, Grobal2.MAGIC_SOULBALL_ATT3_3, this.m_nCurrX, this.m_nCurrY, tx + kx, ty, 0, magiceff.TMagicType.mtThunder, false, 30, ref bofly);
            //        ClMain.g_PlayScene.NewMagic(this, Grobal2.MAGIC_SOULBALL_ATT3_4, Grobal2.MAGIC_SOULBALL_ATT3_4, this.m_nCurrX, this.m_nCurrY, tx - kx, ty, 0, magiceff.TMagicType.mtThunder, false, 30, ref bofly);
            //    }
            //    ClMain.g_PlayScene.NewMagic(this, Grobal2.MAGIC_SOULBALL_ATT3_5, Grobal2.MAGIC_SOULBALL_ATT3_5, this.m_nCurrX, this.m_nCurrY, tx + kx, ty - ky, 0, magiceff.TMagicType.mtThunder, false, 30, ref bofly);
            //    ClMain.g_PlayScene.NewMagic(this, Grobal2.MAGIC_SOULBALL_ATT3_1, Grobal2.MAGIC_SOULBALL_ATT3_1, this.m_nCurrX, this.m_nCurrY, tx - kx - 2, ty + ky, 0, magiceff.TMagicType.mtThunder, false, 30, ref bofly);
            //    ClMain.g_PlayScene.NewMagic(this, Grobal2.MAGIC_SOULBALL_ATT3_2, Grobal2.MAGIC_SOULBALL_ATT3_2, this.m_nCurrX, this.m_nCurrY, tx - kx, ty - ky, 0, magiceff.TMagicType.mtThunder, false, 30, ref bofly);
            //    ClMain.g_PlayScene.NewMagic(this, Grobal2.MAGIC_SOULBALL_ATT3_3, Grobal2.MAGIC_SOULBALL_ATT3_3, this.m_nCurrX, this.m_nCurrY, tx + kx + 2, ty + ky, 0, magiceff.TMagicType.mtThunder, false, 30, ref bofly);
            //    ClMain.g_PlayScene.NewMagic(this, Grobal2.MAGIC_SOULBALL_ATT3_4, Grobal2.MAGIC_SOULBALL_ATT3_4, this.m_nCurrX, this.m_nCurrY, tx + kx, ty, 0, magiceff.TMagicType.mtThunder, false, 30, ref bofly);
            //    ClMain.g_PlayScene.NewMagic(this, Grobal2.MAGIC_SOULBALL_ATT3_5, Grobal2.MAGIC_SOULBALL_ATT3_5, this.m_nCurrX, this.m_nCurrY, tx - kx, ty, 0, magiceff.TMagicType.mtThunder, false, 30, ref bofly);
            //    LightningTimer.Interval = LightningTimer.Interval + 100;
            //    LightningTimer.Tag = LightningTimer.Tag + 1;
            //    if (LightningTimer.Tag > 7)
            //    {
            //        LightningTimer.Interval = 10;
            //        LightningTimer.Tag = 0;
            //        LightningTimer.Enabled = false;
            //    }
            //}
        }

        private void AttackEff()
        {
            int n8;
            int nc;
            int n10;
            int n14;
            int n18;
            bool bofly;
            int i;
            int iCount;
            if (this.m_boDeath)
            {
                return;
            }
            n8 = this.m_nCurrX;
            nc = this.m_nCurrY;
            //iCount = new System.Random(4).Next();
            //for (i = 0; i <= iCount; i++)
            //{
            //    n10 = new System.Random(4).Next();
            //    n14 = new System.Random(8).Next();
            //    n18 = new System.Random(8).Next();
            //    switch (n10)
            //    {
            //        case 0:
            //            ClMain.g_PlayScene.NewMagic(this, 80, 80, this.m_nCurrX, this.m_nCurrY, n8 - n14 - 2, nc + n18 + 1, 0, magiceff.TMagicType.mtRedThunder, false, 30, ref bofly);
            //            break;
            //        case 1:
            //            ClMain.g_PlayScene.NewMagic(this, 80, 80, this.m_nCurrX, this.m_nCurrY, n8 - n14, nc + n18, 0, magiceff.TMagicType.mtRedThunder, false, 30, ref bofly);
            //            break;
            //        case 2:
            //            ClMain.g_PlayScene.NewMagic(this, 80, 80, this.m_nCurrX, this.m_nCurrY, n8 - n14, nc + n18 + 1, 0, magiceff.TMagicType.mtRedThunder, false, 30, ref bofly);
            //            break;
            //        case 3:
            //            ClMain.g_PlayScene.NewMagic(this, 80, 80, this.m_nCurrX, this.m_nCurrY, n8 - n14 - 2, nc + n18, 0, magiceff.TMagicType.mtRedThunder, false, 30, ref bofly);
            //            break;
            //    }
            //    // PlaySound(8206);
            //}
        }

        public override void Run()
        {
            int prv;
            long m_dwEffectFrameTimetime;
            long m_dwFrameTimetime;
            bool bofly;
            if ((this.m_btRace != 120) && this.m_boDeath)
            {
                return;
            }
            if ((this.m_nCurrentAction == Grobal2.SM_WALK) || (this.m_nCurrentAction == Grobal2.SM_BACKSTEP) || (this.m_nCurrentAction == Grobal2.SM_RUN) || (this.m_nCurrentAction == Grobal2.SM_HORSERUN))
            {
                return;
            }
            this.m_boMsgMuch = false;
            if (this.m_MsgList.Count >= MShare.MSGMUCH)
            {
                this.m_boMsgMuch = true;
            }
            if (this.m_boRunSound)
            {
                this.m_boRunSound = false;
            }
            if (this.m_boUseEffect)
            {
                if (this.m_boMsgMuch)
                {
                    m_dwEffectFrameTimetime = HUtil32.Round(this.m_dwEffectFrameTime * 2 / 3);
                }
                else
                {
                    m_dwEffectFrameTimetime = this.m_dwEffectFrameTime;
                }
                if (MShare.GetTickCount() - this.m_dwEffectStartTime > m_dwEffectFrameTimetime)
                {
                    this.m_dwEffectStartTime = MShare.GetTickCount();
                    if (this.m_nEffectFrame < this.m_nEffectEnd)
                    {
                        this.m_nEffectFrame++;
                    }
                    else
                    {
                        if (new ArrayList(new int[] { 118, 119, 120 }).Contains(this.m_btRace))
                        {
                            if (this.m_boDeath)
                            {
                                this.m_boUseEffect = false;
                            }
                            else
                            {
                                this.m_boUseEffect = true;
                            }
                        }
                        else
                        {
                            this.m_boUseEffect = false;
                        }
                    }
                }
            }
            prv = this.m_nCurrentFrame;
            if (this.m_nCurrentAction != 0)
            {
                if ((this.m_nCurrentFrame < this.m_nStartFrame) || (this.m_nCurrentFrame > this.m_nEndFrame))
                {
                    this.m_nCurrentFrame = this.m_nStartFrame;
                }
                if (this.m_boMsgMuch)
                {
                    m_dwFrameTimetime = HUtil32.Round(this.m_dwFrameTime * 2 / 3);
                }
                else
                {
                    m_dwFrameTimetime = this.m_dwFrameTime;
                }
                if (MShare.GetTickCount() - this.m_dwStartTime > m_dwFrameTimetime)
                {
                    if (this.m_nCurrentFrame < this.m_nEndFrame)
                    {
                        this.m_nCurrentFrame++;
                        this.m_dwStartTime = MShare.GetTickCount();
                    }
                    else
                    {
                        this.m_nCurrentAction = 0;
                        this.m_boUseEffect = false;
                        this.m_boNowDeath = false;
                    }
                    if (this.m_nCurrentAction == Grobal2.SM_HIT)
                    {
                        AttackEff();
                    }
                    else if (this.m_btRace == 120)
                    {
                        //if ((this.m_nCurrentAction == Grobal2.SM_LIGHTING) && (this.m_nCurrentFrame - this.m_nStartFrame == 1))
                        //{
                        //    ClMain.g_PlayScene.NewMagic(this, Grobal2.MAGIC_SOULBALL_ATT1, Grobal2.MAGIC_SOULBALL_ATT1, this.m_nCurrX, this.m_nCurrY, this.m_nCurrX, this.m_nCurrY, this.m_nRecogId, magiceff.TMagicType.mtGroundEffect, false, 30, ref bofly);
                        //}
                        //else if ((this.m_nCurrentAction == Grobal2.SM_LIGHTING_1) && (this.m_nCurrentFrame - this.m_nStartFrame == 1))
                        //{
                        //    ClMain.g_PlayScene.NewMagic(this, Grobal2.MAGIC_SOULBALL_ATT2, Grobal2.MAGIC_SOULBALL_ATT2, this.m_nCurrX, this.m_nCurrY, this.m_nTargetX, this.m_nTargetY, this.m_nTargetRecog, magiceff.TMagicType.mtThunder, false, 30, ref bofly);
                        //}
                        //else if ((this.m_nCurrentAction == Grobal2.SM_LIGHTING_2) && (this.m_nCurrentFrame - this.m_nStartFrame == 1))
                        //{
                        //    if (!LightningTimer.Enabled)
                        //    {
                        //        LightningTimer.Enabled = true;
                        //    }
                        //}
                    }
                    else if ((this.m_nCurrentAction == 81) || (this.m_nCurrentAction == 82) || (this.m_nCurrentAction == 83))
                    {
                        if ((this.m_nCurrentFrame - this.m_nStartFrame) == 4)
                        {
                            //ClMain.g_PlayScene.NewMagic(this, this.m_nCurrentAction, this.m_nCurrentAction, this.m_nCurrX, this.m_nCurrY, this.m_nTargetX, this.m_nTargetY, this.m_nTargetRecog, magiceff.TMagicType.mtFly, true, 30, ref bofly);
                        }
                    }
                }
                this.m_nCurrentDefFrame = 0;
                this.m_dwDefFrameTime = MShare.GetTickCount();
            }
            else
            {
                if (this.m_btRace == 120)
                {
                    if (MShare.GetTickCount() - this.m_dwDefFrameTime > 150)
                    {
                        this.m_dwDefFrameTime = MShare.GetTickCount();
                        this.m_nCurrentDefFrame++;
                        if (this.m_nCurrentDefFrame >= this.m_nDefFrameCount)
                        {
                            this.m_nCurrentDefFrame = 0;
                        }
                    }
                    this.DefaultMotion();
                }
                else if ((MShare.GetTickCount() - this.m_dwSmoothMoveTime) > 200)
                {
                    if (MShare.GetTickCount() - this.m_dwDefFrameTime > 500)
                    {
                        this.m_dwDefFrameTime = MShare.GetTickCount();
                        this.m_nCurrentDefFrame++;
                        if (this.m_nCurrentDefFrame >= this.m_nDefFrameCount)
                        {
                            this.m_nCurrentDefFrame = 0;
                        }
                    }
                    this.DefaultMotion();
                }
            }
            if (prv != this.m_nCurrentFrame)
            {
                this.m_dwLoadSurfaceTime = MShare.GetTickCount();
            }
        }
    }
}

