using SystemModule;

namespace RobotSvr;

public class TDragonStatue : TSkeletonArcherMon
{
    public TDragonStatue(RobotClient robotClient) : base(robotClient)
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
            case Grobal2.SM_DIGUP:
                Shift(0, 0, 0, 1);
                m_nStartFrame = 0;
                m_nEndFrame = 0;
                m_dwFrameTime = 100;
                m_dwStartTime = MShare.GetTickCount();
                break;
            case Grobal2.SM_LIGHTING:
                m_nStartFrame = 0;
                m_nEndFrame = 9;
                m_dwFrameTime = 100;
                m_dwStartTime = MShare.GetTickCount();
                m_boUseEffect = true;
                m_nEffectStart = 0;
                m_nEffectFrame = 0;
                m_nEffectEnd = 9;
                m_dwEffectStartTime = MShare.GetTickCount();
                m_dwEffectFrameTime = 100;
                break;
        }
    }

    public override void Run()
    {
        int prv;
        long dwEffectFrameTime;
        long m_dwFrameTimetime;
        bool bofly;
        m_btDir = 0;
        if (m_nCurrentAction == Grobal2.SM_WALK || m_nCurrentAction == Grobal2.SM_BACKSTEP ||
            m_nCurrentAction == Grobal2.SM_RUN || m_nCurrentAction == Grobal2.SM_HORSERUN) return;
        m_boMsgMuch = false;
        if (m_MsgList.Count >= MShare.MSGMUCH) m_boMsgMuch = true;
        if (m_boUseEffect)
        {
            if (m_boMsgMuch)
                dwEffectFrameTime = HUtil32.Round(m_dwEffectFrameTime * 2 / 3);
            else
                dwEffectFrameTime = m_dwEffectFrameTime;
            if (MShare.GetTickCount() - m_dwEffectStartTime > dwEffectFrameTime)
            {
                m_dwEffectStartTime = MShare.GetTickCount();
                if (m_nEffectFrame < m_nEffectEnd)
                    m_nEffectFrame++;
                else
                    m_boUseEffect = false;
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

                if (m_nCurrentAction == Grobal2.SM_LIGHTING && m_nCurrentFrame == 4)
                    robotClient.g_PlayScene.NewMagic(this, 90, 90, m_nCurrX, m_nCurrY, m_nTargetX, m_nTargetY, 0,
                        magiceff.TMagicType.mtExplosion, false, 30, ref bofly);
            }
            m_nCurrentDefFrame = 0;
            m_dwDefFrameTime = MShare.GetTickCount();
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

        if (prv != m_nCurrentFrame)
        {
            m_dwLoadSurfaceTime = MShare.GetTickCount();
        }
    }
}