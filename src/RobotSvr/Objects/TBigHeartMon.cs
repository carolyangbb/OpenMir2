namespace RobotSvr;

public class TBigHeartMon : TKillingHerb
{
    public TBigHeartMon(RobotClient robotClient) : base(robotClient)
    {
    }

    public override void CalcActorFrame()
    {
        m_btDir = 0;
        base.CalcActorFrame();
    }
}