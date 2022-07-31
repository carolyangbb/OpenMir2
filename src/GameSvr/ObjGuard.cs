using SystemModule;

namespace GameSvr
{
    public class TSuperGuard : TNormNpc
    {
        public TSuperGuard() : base()
        {
            this.RaceServer = Grobal2.RC_DOORGUARD;
            this.ViewRange = 7;
            this.Light = 2;
        }

        public override void RunMsg(TMessageInfo msg)
        {
            base.RunMsg(msg);
        }

        public bool AttackTarget()
        {
            bool result = false;
            if (this.TargetCret.PEnvir == this.PEnvir)
            {
                if (GetCurrentTime - this.HitTime > this.GetNextHitTime())
                {
                    this.HitTime = GetCurrentTime;
                    this.TargetFocusTime = HUtil32.GetTickCount();
                    short ox = CX;
                    short oy = CY;
                    byte olddir = this.Dir;
                    M2Share.GetBackPosition(this.TargetCret, ref this.CX, ref this.CY);
                    this.Dir = M2Share.GetNextDirection(this.CX, this.CY, this.TargetCret.CX, this.TargetCret.CY);
                    this.SendRefMsg(Grobal2.RM_HIT, this.Dir, this.CX, this.CY, 0, "");
                    this._Attack(Grobal2.HM_HIT, this.TargetCret);
                    this.TargetCret.SetLastHiter(this);
                    this.TargetCret.ExpHiter = null;
                    this.CX = ox;
                    this.CY = oy;
                    this.Dir = olddir;
                    this.Turn(this.Dir);
                    this.BreakHolySeize();
                }
                result = true;
            }
            else
            {
                this.LoseTarget();
            }
            return result;
        }

        public override void Run()
        {
            TCreature cret;
            if (GetCurrentTime - this.HitTime > this.GetNextHitTime())
            {
                for (var i = 0; i < this.VisibleActors.Count; i++)
                {
                    cret = (TCreature)this.VisibleActors[i].cret;
                    if ((!cret.Death) && ((cret.PKLevel() >= 2) || ((cret.RaceServer >= Grobal2.RC_MONSTER) && (!cret.BoHasMission))))
                    {
                        this.SelectTarget(cret);
                        break;
                    }
                }
            }
            if (this.TargetCret != null)
            {
                AttackTarget();
            }
            base.Run();
        }
    }
}
