using System;
using SystemModule;

namespace RobotSvr
{
    public class TSkeletonArcherMon : TArcherMon
    {
        public bool m_boNowDeath = false;
        public int n264 = 0;
        public int n268 = 0;

        public override void CalcActorFrame()
        {
            base.CalcActorFrame();
            if ((this.m_nCurrentAction == Grobal2.SM_NOWDEATH) && (this.m_btRace != 72))
            {
                m_boNowDeath = true;
            }
        }

        public override void Run()
        {
            long m_dwFrameTimetime;
            if (this.m_boMsgMuch)
            {
                m_dwFrameTimetime = Math.Round(this.m_dwFrameTime * 2 / 3);
            }
            else
            {
                m_dwFrameTimetime = this.m_dwFrameTime;
            }
            if (this.m_nCurrentAction != 0)
            {
                if ((MShare.GetTickCount() - this.m_dwStartTime) > m_dwFrameTimetime)
                {
                    if (this.m_nCurrentFrame < this.m_nEndFrame)
                    {
                    }
                    else
                    {
                        this.m_nCurrentAction = 0;
                        m_boNowDeath = false;
                    }
                }
            }
            base.Run();
        }

    }
}

