namespace RobotSvr;

public class TMineMon : TKillingHerb
{
    public TMineMon(RobotClient robotClient) : base(robotClient)
    {
    }

    public override void CalcActorFrame()
    {
        base.CalcActorFrame();
    }


    public override int GetDefaultFrame(bool wmode)
    {
        var result = 0;
        return result;
    }
}