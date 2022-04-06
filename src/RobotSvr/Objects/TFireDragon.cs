using System.Collections;
using SystemModule;

namespace RobotSvr
{
    public class TFireDragon : TSkeletonArcherMon
    {
        public TFireDragon(RobotClient robotClient) : base(robotClient)
        {
        }

        public override void CalcActorFrame()
        {
            TMonsterAction pm;
            m_btDir = 0;
            m_nCurrentFrame = -1;
            m_nBodyOffset = Actor.GetOffset(m_wAppearance);
            pm = Actor.GetRaceByPM(m_btRace, m_wAppearance);
            if (pm == null) return;
            switch (m_nCurrentAction)
            {
                case Grobal2.SM_TURN:
                    m_dwStartTime = MShare.GetTickCount();
                    m_nDefFrameCount = pm.ActStand.frame;
                    Shift(m_btDir, 0, 0, 1);
                    if (m_btRace == 120)
                    {
                        switch (m_nTempState)
                        {
                            case 1:
                                m_nStartFrame = 0;
                                break;
                            case 2:
                                m_nStartFrame = 80;
                                break;
                            case 3:
                                m_nStartFrame = 160;
                                break;
                            case 4:
                                m_nStartFrame = 240;
                                break;
                            case 5:
                                m_nStartFrame = 320;
                                break;
                        }

                        m_boWarMode = true;
                        m_dwFrameTime = 150;
                        m_nEndFrame = m_nStartFrame + 19;
                        m_dwStartTime = MShare.GetTickCount();
                        m_nDefFrameCount = 20;
                        m_boUseEffect = true;
                        m_dwEffectStartTime = MShare.GetTickCount();
                        m_dwEffectFrameTime = 150;
                    }

                    break;
                case Grobal2.SM_DIGUP:
                    Shift(0, 0, 0, 1);
                    m_nStartFrame = 0;
                    m_nEndFrame = 9;
                    m_dwFrameTime = 300;
                    m_dwStartTime = MShare.GetTickCount();
                    break;
                // Modify the A .. B: Grobal2.SM_LIGHTING, Grobal2.SM_LIGHTING_1 .. Grobal2.SM_LIGHTING_3
                case Grobal2.SM_LIGHTING:
                    if (m_btRace == 120)
                    {
                        m_nStartFrame = 0;
                        m_nEndFrame = 19;
                        m_dwFrameTime = 150;
                        m_dwStartTime = MShare.GetTickCount();
                        m_boUseEffect = true;
                        m_nEffectFrame = 0;
                        m_nEffectStart = 0;
                        m_nEffectEnd = 19;
                        m_dwEffectStartTime = MShare.GetTickCount();
                        m_dwEffectFrameTime = 150;
                        m_nCurEffFrame = 0;
                        m_boUseMagic = true;
                        m_dwWarModeTime = MShare.GetTickCount();
                        Shift(m_btDir, 0, 0, 1);
                        if (m_btRace == 120)
                        {
                            switch (m_nTempState)
                            {
                                case 1:
                                    m_nStartFrame = 20;
                                    break;
                                case 2:
                                    m_nStartFrame = 100;
                                    break;
                                case 3:
                                    m_nStartFrame = 180;
                                    break;
                                case 4:
                                    m_nStartFrame = 260;
                                    break;
                                case 5:
                                    m_nStartFrame = 340;
                                    break;
                            }

                            m_nEndFrame = m_nStartFrame + 9;
                            m_dwFrameTime = 150;
                            m_boUseEffect = true;
                            m_dwEffectStartTime = MShare.GetTickCount();
                            m_dwEffectFrameTime = 150;
                        }
                    }

                    break;
                case Grobal2.SM_HIT:
                    if (m_btRace != 120)
                    {
                        m_nStartFrame = 0;
                        m_nEndFrame = 19;
                        m_dwFrameTime = 150;
                        m_dwStartTime = MShare.GetTickCount();
                        m_boUseEffect = true;
                        m_nEffectStart = 0;
                        m_nEffectFrame = 0;
                        m_nEffectEnd = 19;
                        m_dwEffectStartTime = MShare.GetTickCount();
                        m_dwEffectFrameTime = 150;
                        m_nCurEffFrame = 0;
                        m_boUseMagic = true;
                        m_dwWarModeTime = MShare.GetTickCount();
                        Shift(m_btDir, 0, 0, 1);
                    }

                    break;
                case Grobal2.SM_STRUCK:
                    if (m_btRace != 120)
                    {
                        m_nStartFrame = 0;
                        m_nEndFrame = 9;
                        m_dwFrameTime = 300;
                        m_dwStartTime = MShare.GetTickCount();
                    }

                    break;
                // Modify the A .. B: 81 .. 83
                case 81:
                    m_nStartFrame = 0;
                    m_nEndFrame = 5;
                    m_dwFrameTime = 150;
                    m_dwStartTime = MShare.GetTickCount();
                    m_boUseEffect = true;
                    m_nEffectStart = 0;
                    m_nEffectFrame = 0;
                    m_nEffectEnd = 10;
                    m_dwEffectStartTime = MShare.GetTickCount();
                    m_dwEffectFrameTime = 150;
                    m_nCurEffFrame = 0;
                    m_boUseMagic = true;
                    m_dwWarModeTime = MShare.GetTickCount();
                    Shift(m_btDir, 0, 0, 1);
                    break;
                case Grobal2.SM_DEATH:
                    if (m_btRace != 120)
                    {
                        m_nCurrentFrame = 0;
                        m_nStartFrame = 80;
                        m_nEndFrame = 81;
                        m_boUseEffect = false;
                        m_boDelActionAfterFinished = true;
                    }

                    break;
                case Grobal2.SM_NOWDEATH:
                    if (m_btRace == 120)
                    {
                        m_nStartFrame = pm.ActDie.start;
                        m_nEndFrame = m_nStartFrame + pm.ActDie.frame - 1;
                        m_dwFrameTime = pm.ActDie.ftime;
                        m_dwStartTime = MShare.GetTickCount();
                        m_boUseEffect = true;
                        m_nEffectFrame = 420;
                        m_nEffectStart = 420;
                        m_dwFrameTime = 150;
                        m_nEndFrame = m_nStartFrame + 17;
                        m_dwStartTime = MShare.GetTickCount();
                        m_boUseEffect = true;
                        m_dwEffectStartTime = MShare.GetTickCount();
                        m_dwEffectFrameTime = 150;
                    }
                    else
                    {
                        m_nCurrentFrame = 0;
                        m_nStartFrame = 80;
                        m_nEndFrame = 81;
                        m_nCurrentFrame = 0;
                        m_boUseEffect = false;
                        m_boDelActionAfterFinished = true;
                    }

                    break;
            }

            if (new ArrayList(new[] { 118, 119, 120 }).Contains(m_btRace)) m_boUseEffect = true;
        }

        public void LightningTimerTimer(object Sender)
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
            if (m_boDeath) return;
            n8 = m_nCurrX;
            nc = m_nCurrY;
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
            if (m_btRace != 120 && m_boDeath) return;
            if (m_nCurrentAction == Grobal2.SM_WALK || m_nCurrentAction == Grobal2.SM_BACKSTEP ||
                m_nCurrentAction == Grobal2.SM_RUN || m_nCurrentAction == Grobal2.SM_HORSERUN) return;
            m_boMsgMuch = false;
            if (m_MsgList.Count >= MShare.MSGMUCH) m_boMsgMuch = true;
            if (m_boRunSound) m_boRunSound = false;
            if (m_boUseEffect)
            {
                if (m_boMsgMuch)
                    m_dwEffectFrameTimetime = HUtil32.Round(m_dwEffectFrameTime * 2 / 3);
                else
                    m_dwEffectFrameTimetime = m_dwEffectFrameTime;
                if (MShare.GetTickCount() - m_dwEffectStartTime > m_dwEffectFrameTimetime)
                {
                    m_dwEffectStartTime = MShare.GetTickCount();
                    if (m_nEffectFrame < m_nEffectEnd)
                    {
                        m_nEffectFrame++;
                    }
                    else
                    {
                        if (new ArrayList(new[] { 118, 119, 120 }).Contains(m_btRace))
                        {
                            if (m_boDeath)
                                m_boUseEffect = false;
                            else
                                m_boUseEffect = true;
                        }
                        else
                        {
                            m_boUseEffect = false;
                        }
                    }
                }
            }

            prv = m_nCurrentFrame;
            if (m_nCurrentAction != 0)
            {
                if (m_nCurrentFrame < m_nStartFrame || m_nCurrentFrame > m_nEndFrame) m_nCurrentFrame = m_nStartFrame;
                if (m_boMsgMuch)
                    m_dwFrameTimetime = HUtil32.Round(m_dwFrameTime * 2 / 3);
                else
                    m_dwFrameTimetime = m_dwFrameTime;
                if (MShare.GetTickCount() - m_dwStartTime > m_dwFrameTimetime)
                {
                    if (m_nCurrentFrame < m_nEndFrame)
                    {
                        m_nCurrentFrame++;
                        m_dwStartTime = MShare.GetTickCount();
                    }
                    else
                    {
                        m_nCurrentAction = 0;
                        m_boUseEffect = false;
                        m_boNowDeath = false;
                    }

                    if (m_nCurrentAction == Grobal2.SM_HIT)
                    {
                        AttackEff();
                    }
                    else if (m_btRace == 120)
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
                    else if (m_nCurrentAction == 81 || m_nCurrentAction == 82 || m_nCurrentAction == 83)
                    {
                        if (m_nCurrentFrame - m_nStartFrame == 4)
                        {
                            //ClMain.g_PlayScene.NewMagic(this, this.m_nCurrentAction, this.m_nCurrentAction, this.m_nCurrX, this.m_nCurrY, this.m_nTargetX, this.m_nTargetY, this.m_nTargetRecog, magiceff.TMagicType.mtFly, true, 30, ref bofly);
                        }
                    }
                }

                m_nCurrentDefFrame = 0;
                m_dwDefFrameTime = MShare.GetTickCount();
            }
            else
            {
                if (m_btRace == 120)
                {
                    if (MShare.GetTickCount() - m_dwDefFrameTime > 150)
                    {
                        m_dwDefFrameTime = MShare.GetTickCount();
                        m_nCurrentDefFrame++;
                        if (m_nCurrentDefFrame >= m_nDefFrameCount) m_nCurrentDefFrame = 0;
                    }

                    DefaultMotion();
                }
                else if (MShare.GetTickCount() - m_dwSmoothMoveTime > 200)
                {
                    if (MShare.GetTickCount() - m_dwDefFrameTime > 500)
                    {
                        m_dwDefFrameTime = MShare.GetTickCount();
                        m_nCurrentDefFrame++;
                        if (m_nCurrentDefFrame >= m_nDefFrameCount) m_nCurrentDefFrame = 0;
                    }

                    DefaultMotion();
                }
            }

            if (prv != m_nCurrentFrame) m_dwLoadSurfaceTime = MShare.GetTickCount();
        }
    }
}