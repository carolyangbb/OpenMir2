using SystemModule;

namespace GameSvr
{
    public class TThornDarkMonster : TDualAxeMonster
    {
        // ----------------------------------------------------
        // TThornDarkMonster
        //Constructor  Create()
        public TThornDarkMonster() : base()
        {
            this.ChainShotCount = 3;
            this.RaceServer = Grobal2.RC_THORNDARK;
        }
    }
}