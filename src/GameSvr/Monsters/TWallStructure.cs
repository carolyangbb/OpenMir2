using SystemModule;

namespace GameSvr
{
    public class TWallStructure : TGuardUnit
    {
        public long BrokenTime = 0;
        public bool BoBlockPos = false;
        // ---------------------------------------------------------------------
        // 己寒,
        //Constructor  Create()
        public TWallStructure() : base()
        {
            this.BoAnimal = false;
            this.StickMode = true;
            BoBlockPos = false;
            this.AntiPoison = 200;
            // HideMode := TRUE;

        }
        public override void Initialize()
        {
            this.Dir = 0;
            // 檬扁惑怕
            base.Initialize();
        }

        // 货肺 绊媚咙
        public void RepairStructure()
        {
            int newdir;
            if (this.WAbil.HP > 0)
            {
                newdir = 3 - HUtil32.MathRound(this.WAbil.HP / this.WAbil.MaxHP * 3);
            }
            else
            {
                newdir = 4;
            }
            if (!(newdir >= 0 && newdir <= 4))
            {
                newdir = 0;
            }
            this.Dir = (byte)newdir;
            this.SendRefMsg(Grobal2.RM_ALIVE, this.Dir, this.CX, this.CY, 0, "");
        }

        public override void Die()
        {
            base.Die();
            BrokenTime = HUtil32.GetTickCount();
        }

        public override void Run()
        {
            int newdir;
            if (this.Death)
            {
                this.DeathTime = HUtil32.GetTickCount();
                // 绝绢瘤瘤 臼绰促.
                if (BoBlockPos)
                {
                    this.PEnvir.GetMarkMovement(this.CX, this.CY, true);
                    // 捞悼 啊瓷窍霸
                    BoBlockPos = false;
                }
            }
            else
            {
                this.HealthTick = 0;
                // 眉仿捞 促矫 瞒瘤 臼绰促.
                if (!BoBlockPos)
                {
                    this.PEnvir.GetMarkMovement(this.CX, this.CY, false);
                    // 捞悼 给窍霸
                    BoBlockPos = true;
                }
            }
            if (this.WAbil.HP > 0)
            {
                newdir = 3 - HUtil32.MathRound(this.WAbil.HP / this.WAbil.MaxHP * 3);
            }
            else
            {
                newdir = 4;
            }
            if ((newdir != this.Dir) && (newdir < 5))
            {
                // 规氢 0,1,2,3,4
                this.Dir = (byte)newdir;
                this.SendRefMsg(Grobal2.RM_DIGUP, this.Dir, this.CX, this.CY, 0, "");
                // 何辑瘤绰 局聪皋捞记
            }
            base.Run();
        }

    }
}