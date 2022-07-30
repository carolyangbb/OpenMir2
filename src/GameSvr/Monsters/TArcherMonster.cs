using SystemModule;

namespace GameSvr
{
    public class TArcherMonster : TDualAxeMonster
    {
        // ----------------------------------------------------
        // TArcherMonster
        //Constructor  Create()
        public TArcherMonster() : base()
        {
            this.ChainShotCount = 6;
            this.RaceServer = Grobal2.RC_ARCHERMON;
        }
    }
}