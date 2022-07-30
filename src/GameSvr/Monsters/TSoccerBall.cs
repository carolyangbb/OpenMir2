namespace GameSvr
{
    public class TSoccerBall : TAnimal
    {
        public int GoPower = 0;

        public TSoccerBall() : base()
        {
            this.BoAnimal = false;
            this.NeverDie = true;
            GoPower = 0;
            this.TargetX = -1;
        }

        public override void Struck(TCreature hiter)
        {
            int nx=0;
            int ny=0;
            if (hiter != null)
            {
                this.Dir = hiter.Dir;
                GoPower = GoPower + 4 + new System.Random(4).Next();
                GoPower = _MIN(20, GoPower);
                M2Share.GetNextPosition(this.PEnvir, this.CX, this.CY, this.Dir, GoPower, ref nx, ref ny);
                this.TargetX = (short)nx;
                this.TargetY = (short)ny;
            }
        }

        public override void Run()
        {
            int nx=0;
            int ny=0;
            bool bohigh;
            bohigh = false;
            if (GoPower > 0)
            {
                if (M2Share.GetNextPosition(this.PEnvir, this.CX, this.CY, this.Dir, 1, ref nx, ref ny))
                {
                    if (!this.PEnvir.CanWalk(nx, ny, bohigh))
                    {
                        switch (this.Dir)
                        {
                            case 0:
                                this.Dir = 4;
                                break;
                            case 1:
                                this.Dir = 7;
                                break;
                            case 2:
                                this.Dir = 6;
                                break;
                            case 3:
                                this.Dir = 5;
                                break;
                            case 4:
                                this.Dir = 0;
                                break;
                            case 5:
                                this.Dir = 3;
                                break;
                            case 6:
                                this.Dir = 2;
                                break;
                            case 7:
                                this.Dir = 1;
                                break;
                        }
                        M2Share.GetNextPosition(this.PEnvir, this.CX, this.CY, this.Dir, GoPower, ref nx, ref ny);
                        this.TargetX = (short)nx;
                        this.TargetY = (short)ny;
                    }
                }
            }
            else
            {
                this.TargetX = -1;
            }
            if (this.TargetX != -1)
            {
                this.GotoTargetXY();
                if ((this.TargetX == this.CX) && (this.TargetY == this.CY))
                {
                    GoPower = 0;
                }
            }
            base.Run();
        }
    }
}