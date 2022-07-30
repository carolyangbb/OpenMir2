namespace GameSvr
{
    public class THolyCurtainEvent : TEvent
    {
        // ----------------------------------------------------------
        //Constructor  Create( penv,  ax,  ay,  etype,  etime)
        public THolyCurtainEvent(TEnvirnoment penv, int ax, int ay, int etype, int etime) : base(penv, ax, ay, etype, etime, true)
        {
        }
    } // end THolyCurtainEvent

}

