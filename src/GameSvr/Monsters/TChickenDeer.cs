using System;
using SystemModule;

namespace GameSvr
{
    public class TChickenDeer : TMonster
    {
        // ----------------------------------------------------------------------
        //Constructor  Create()
        public TChickenDeer() : base()
        {
            this.ViewRange = 5;
        }
        public override void Run()
        {
            int i;
            int d;
            int dis;
            int ndir;
            TCreature cret;
            TCreature nearcret;
            dis = 9999;
            nearcret = null;
            // if not Death and not RunDone and not BoGhost and
            // (StatusArr[POISON_STONE] = 0) and (StatusArr[POISON_ICE] = 0) and
            // (StatusArr[POISON_STUN] = 0) then begin
            if (!this.RunDone && this.IsMoveAble())
            {
                if (GetCurrentTime - this.WalkTime > this.GetNextWalkTime())
                {
                    // 惑加罐篮 run 俊辑 WalkTime 犁汲沥窃.
                    for (i = 0; i < this.VisibleActors.Count; i++)
                    {
                        cret = (TCreature)this.VisibleActors[i].cret;
                        if ((!cret.Death) && this.IsProperTarget(cret) && (!cret.BoHumHideMode || this.BoViewFixedHide))
                        {
                            d = Math.Abs(this.CX - cret.CX) + Math.Abs(this.CY - cret.CY);
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
                        // 崔酒唱绰 葛靛
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
                        // 惑加罐篮 run俊辑 WalkTime 犁汲沥窃
                        if ((Math.Abs(this.CX - this.TargetCret.CX) <= 6) && (Math.Abs(this.CY - this.TargetCret.CY) <= 6))
                        {
                            // 档噶皑.
                            ndir = M2Share.GetNextDirection(this.TargetCret.CX, this.TargetCret.CY, this.CX, this.CY);
                            M2Share.GetNextPosition(this.PEnvir, this.TargetCret.CX, this.TargetCret.CY, ndir, 5, ref (int)this.TargetX, ref (int)this.TargetY);
                        }
                    }
                }
            }
            base.Run();
        }

    } // end TChickenDeer
}