namespace GameSvr
{
    public class TBamTreeMonster : TAnimal
    {
        public int StruckCount = 0;
        public int DeathStruckCount = 0;
        // --------------------------------------------------------------
        // 广唱公
        //Constructor  Create()
        public TBamTreeMonster() : base()
        {
            this.BoAnimal = false;
            StruckCount = 0;
            DeathStruckCount = 0;
            // HP;;;

        }
        public override void Struck(TCreature hiter)
        {
            base.Struck(hiter);
            StruckCount++;
        }

        public override void Run()
        {
            if (DeathStruckCount == 0)
            {
                DeathStruckCount = this.WAbil.MaxHP;
            }
            this.WAbil.HP = this.WAbil.MaxHP;
            if (StruckCount >= DeathStruckCount)
            {
                this.WAbil.HP = 0;
            }
            base.Run();
        }

    }
}