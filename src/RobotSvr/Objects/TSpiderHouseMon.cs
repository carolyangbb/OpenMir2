namespace RobotSvr
{
    public class TSpiderHouseMon : TKillingHerb
    {
        public TSpiderHouseMon(RobotClient robotClient) : base(robotClient)
        {
        }

        public override void CalcActorFrame()
        {
            this.m_btDir = 0;
            base.CalcActorFrame();
        }
    }
}

