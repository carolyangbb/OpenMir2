using System;

namespace GameSvr
{
    public class TChickenDeer : TMonster
    {
        public TChickenDeer() : base()
        {
            this.ViewRange = 5;
        }

        public override void Run()
        {
            int dis = 9999;
            TCreature nearcret = null;
            if (!this.RunDone && this.IsMoveAble())
            {
                if (GetCurrentTime - this.WalkTime > this.GetNextWalkTime())
                {
                    for (var i = 0; i < this.VisibleActors.Count; i++)
                    {
                        TCreature cret = (TCreature)this.VisibleActors[i].cret;
                        if ((!cret.Death) && this.IsProperTarget(cret) && (!cret.BoHumHideMode || this.BoViewFixedHide))
                        {
                            int d = Math.Abs(this.CX - cret.CX) + Math.Abs(this.CY - cret.CY);
                            if (d < dis)
                            {
                                dis = d;
                                nearcret = cret;
                            }
                        }
                    }
                    if (nearcret != null)
                    {
                        this.BoRunAwayMode = true;
                        this.TargetCret = nearcret;
                    }
                    else
                    {
                        this.BoRunAwayMode = false;
                        this.TargetCret = null;
                    }
                }
                if (this.BoRunAwayMode && (this.TargetCret != null))
                {
                    if (GetCurrentTime - this.WalkTime > this.GetNextWalkTime())
                    {
                        if ((Math.Abs(this.CX - this.TargetCret.CX) <= 6) && (Math.Abs(this.CY - this.TargetCret.CY) <= 6))
                        {
                            byte ndir = M2Share.GetNextDirection(this.TargetCret.CX, this.TargetCret.CY, this.CX, this.CY);
                            M2Share.GetNextPosition(this.PEnvir, this.TargetCret.CX, this.TargetCret.CY, ndir, 5, ref this.TargetX, ref this.TargetY);
                        }
                    }
                }
            }
            base.Run();
        }
    }
}