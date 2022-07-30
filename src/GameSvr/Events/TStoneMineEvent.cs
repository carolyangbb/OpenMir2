using SystemModule;

namespace GameSvr
{
    public class TStoneMineEvent : TEvent
    {
        public int MineCount = 0;
        public int MineFillCount = 0;
        public long RefillTime = 0;

        public TStoneMineEvent(TEnvirnoment penv, int ax, int ay, int etype) : base(penv, ax, ay, etype, 0, false)
        {
            AddToMap();
            this.FVisible = false;
            MineCount = new System.Random(200).Next();
            RefillTime = GettickCount;
            this.Active = false;
            MineFillCount = new System.Random(80).Next();
        }

        public void Refill()
        {
            MineCount = MineFillCount;
            RefillTime = GettickCount;
        }

        public override void AddToMap()
        {
            if (null == this.PEnvir.AddToMapMineEvnet(this.X, this.Y, Grobal2.OS_EVENTOBJECT, this))
            {
                this.IsAddToMap = false;
            }
            else
            {
                this.IsAddToMap = true;
            }
        }

    }

}

