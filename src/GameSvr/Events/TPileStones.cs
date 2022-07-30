namespace GameSvr
{
    public class TPileStones : TEvent
    {
        // ----------------------------------------------------------
        //Constructor  Create( penv,  ax,  ay,  etype,  etime,  bovisible)
        public TPileStones(TEnvirnoment penv, int ax, int ay, int etype, int etime, bool bovisible) : base(penv, ax, ay, etype, etime, true)
        {
            this.EventParam = 1;
        }
        public void EnlargePile()
        {
            if (this.EventParam < 5)
            {
                this.EventParam++;
            }
        }

    } // end TPileStones

}

