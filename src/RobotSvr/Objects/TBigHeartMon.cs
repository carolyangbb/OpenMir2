namespace RobotSvr
{
    public class TBigHeartMon : TKillingHerb
    {
        public override void CalcActorFrame()
        {
            this.m_btDir = 0;
            base.CalcActorFrame();
        }
    }
}

