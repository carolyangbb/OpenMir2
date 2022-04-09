using SystemModule;

namespace RobotSvr
{
    public class TCentipedeKingMon : TKillingHerb
    {
        private readonly int ax = 0;
        private readonly int ay = 0;
        private bool BoUseDieEffect;

        public TCentipedeKingMon(RobotClient robotClient) : base(robotClient)
        {

        }

        public override void Run()
        {
            if (m_nCurrentAction == Grobal2.SM_WALK || m_nCurrentAction == Grobal2.SM_BACKSTEP ||
                m_nCurrentAction == Grobal2.SM_HORSERUN || m_nCurrentAction == Grobal2.SM_RUN) return;
            if (BoUseDieEffect)
                if (m_nCurrentFrame - m_nStartFrame >= 5)
                {
                    BoUseDieEffect = false;
                    m_boUseEffect = true;
                    m_dwEffectStartTime = MShare.GetTickCount();
                    m_nEffectFrame = 0;
                }

            if (m_boUseEffect)
                if (MShare.GetTickCount() - m_dwEffectStartTime > m_dwEffectFrameTime)
                {
                    m_dwEffectStartTime = MShare.GetTickCount();
                    if (m_nEffectFrame < m_nEffectEnd)
                        m_nEffectFrame++;
                    else
                        m_boUseEffect = false;
                }

            base.Run();
        }
    }
}