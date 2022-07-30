namespace GameSvr
{
    public class TCowMonster : TATMonster
    {
        // ---------------------------------------------------------------------------
        // 快搁蓖
        //Constructor  Create()
        public TCowMonster() : base()
        {
            this.SearchRate = 1500 + ((long)new System.Random(1500).Next());
        }
    }
}