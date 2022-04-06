using SystemModule;

namespace RobotSvr
{
    public class TStoneMonster : TSkeletonArcherMon
    {
        public TStoneMonster(RobotClient robotClient) : base(robotClient)
        {
            m_boUseEffect = false;
            m_boNowDeath = false;
        }

        public override void CalcActorFrame()
        {
            TMonsterAction pm;
            m_boUseMagic = false;
            m_nCurrentFrame = -1;
            m_nBodyOffset = Actor.GetOffset(m_wAppearance);
            pm = Actor.GetRaceByPM(m_btRace, m_wAppearance);
            if (pm == null) return;
            m_btDir = 0;
            switch (m_nCurrentAction)
            {
                case Grobal2.SM_TURN:
                    m_nStartFrame = pm.ActStand.start;
                    m_nEndFrame = m_nStartFrame + pm.ActStand.frame - 1;
                    m_dwFrameTime = pm.ActStand.ftime;
                    m_dwStartTime = MShare.GetTickCount();
                    m_nDefFrameCount = pm.ActStand.frame;
                    if (!m_boUseEffect)
                    {
                        m_boUseEffect = true;
                        m_nEffectFrame = m_nStartFrame;
                        m_nEffectStart = m_nStartFrame;
                        m_nEffectEnd = m_nEndFrame;
                        m_dwEffectStartTime = MShare.GetTickCount();
                        m_dwEffectFrameTime = 300;
                    }

                    break;
                case Grobal2.SM_HIT:
                    m_nStartFrame = pm.ActAttack.start;
                    m_nEndFrame = m_nStartFrame + pm.ActAttack.frame - 1;
                    m_dwFrameTime = pm.ActAttack.ftime;
                    m_dwStartTime = MShare.GetTickCount();
                    m_dwWarModeTime = MShare.GetTickCount();
                    if (!m_boUseEffect)
                    {
                        m_boUseEffect = true;
                        m_nEffectFrame = m_nStartFrame;
                        m_nEffectStart = m_nStartFrame;
                        m_nEffectEnd = m_nStartFrame + 25;
                        m_dwEffectStartTime = MShare.GetTickCount();
                        m_dwEffectFrameTime = 150;
                    }

                    break;
                case Grobal2.SM_STRUCK:
                    m_nStartFrame = pm.ActStruck.start;
                    m_nEndFrame = m_nStartFrame + pm.ActStruck.frame - 1;
                    m_dwFrameTime = pm.ActStruck.ftime;
                    m_dwStartTime = MShare.GetTickCount();
                    break;
                case Grobal2.SM_DEATH:
                    m_nStartFrame = pm.ActDie.start;
                    m_nEndFrame = m_nStartFrame + pm.ActDie.frame - 1;
                    m_dwFrameTime = pm.ActDie.ftime;
                    m_dwStartTime = MShare.GetTickCount();
                    break;
                case Grobal2.SM_NOWDEATH:
                    m_nStartFrame = pm.ActDie.start;
                    m_nEndFrame = m_nStartFrame + pm.ActDie.frame - 1;
                    m_dwFrameTime = pm.ActDie.ftime;
                    m_dwStartTime = MShare.GetTickCount();
                    m_boNowDeath = true;
                    m_nEffectFrame = m_nStartFrame;
                    m_nEffectStart = m_nStartFrame;
                    m_nEffectEnd = m_nStartFrame + 19;
                    m_dwEffectStartTime = MShare.GetTickCount();
                    m_dwEffectFrameTime = 80;
                    break;
            }
        }

        public override void Run()
        {
            int prv;
            long m_dwEffectFrameTimetime;
            long m_dwFrameTimetime;
            if (m_nCurrentAction == Grobal2.SM_WALK || m_nCurrentAction == Grobal2.SM_BACKSTEP ||
                m_nCurrentAction == Grobal2.SM_RUN || m_nCurrentAction == Grobal2.SM_HORSERUN) return;
            m_boMsgMuch = false;
            if (m_MsgList.Count >= MShare.MSGMUCH) m_boMsgMuch = true;
            RunFrameAction(m_nCurrentFrame - m_nStartFrame);
            if (m_boUseEffect || m_boNowDeath)
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
                        m_boUseEffect = false;
                        m_boNowDeath = false;
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
                    }

                    m_nCurrentDefFrame = 0;
                    m_dwDefFrameTime = MShare.GetTickCount();
                }
            }
            else
            {
                if (MShare.GetTickCount() - m_dwSmoothMoveTime > 200)
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

            if (prv != m_nCurrentFrame || prv != m_nEffectFrame) m_dwLoadSurfaceTime = MShare.GetTickCount();
        }
    }
}