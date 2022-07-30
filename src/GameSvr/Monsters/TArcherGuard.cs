using System;
using SystemModule;

namespace GameSvr
{
    public class TArcherGuard : TGuardUnit
    {
        public TArcherGuard() : base()
        {
            this.ViewRange = 12;
            this.WantRefMsg = true;
            this.Castle = null;
            this.OriginDir = -1;
            this.RaceServer = Grobal2.RC_ARCHERGUARD;
        }

        private void ShotArrow(TCreature targ)
        {
            if (targ == null)
            {
                return;
            }
            this.Dir = M2Share.GetNextDirection(this.CX, this.CY, targ.CX, targ.CY);
            TAbility _wvar1 = this.WAbil;
            int dam = HUtil32.LoByte(_wvar1.DC) + new System.Random(HiByte(_wvar1.DC) - HUtil32.LoByte(_wvar1.DC) + 1).Next();
            if (dam > 0)
            {
                dam = targ.GetHitStruckDamage(this, dam);
            }
            if (dam > 0)
            {
                targ.SetLastHiter(this);
                targ.ExpHiter = null;
                targ.StruckDamage(dam, this);
                targ.SendDelayMsg(Grobal2.RM_STRUCK, Grobal2.RM_REFMESSAGE, dam, targ.WAbil.HP, targ.WAbil.MaxHP, this.ActorId, "", 600 + _MAX(Math.Abs(this.CX - targ.CX), Math.Abs(this.CY - targ.CY)) * 50);
            }
            this.SendRefMsg(Grobal2.RM_FLYAXE, this.Dir, this.CX, this.CY, targ.ActorId, "");
        }

        public override void Run()
        {
            int dis = 9999;
            TCreature nearcret = null;
            if (this.IsMoveAble())
            {
                if (GetCurrentTime - this.WalkTime > this.GetNextWalkTime())
                {
                    this.WalkTime = GetCurrentTime;
                    for (var i = 0; i < this.VisibleActors.Count; i++)
                    {
                        TCreature cret = (TCreature)this.VisibleActors[i].cret;
                        if ((!cret.Death) && this.IsProperTarget(cret))
                        {
                            var d = Math.Abs(this.CX - cret.CX) + Math.Abs(this.CY - cret.CY);
                            if (d < dis)
                            {
                                dis = d;
                                nearcret = cret;
                            }
                        }
                    }
                    if (nearcret != null)
                    {
                        this.SelectTarget(nearcret);
                    }
                    else
                    {
                        this.LoseTarget();
                    }
                }
                if (this.TargetCret != null)
                {
                    if (GetCurrentTime - this.HitTime > this.GetNextHitTime())
                    {
                        this.HitTime = GetCurrentTime;
                        ShotArrow(this.TargetCret);
                    }
                }
                else
                {
                    if (this.OriginDir >= 0)
                    {
                        if (this.OriginDir != this.Dir)
                        {
                            this.Turn((byte)this.OriginDir);
                        }
                    }
                }
            }
            base.Run();
        }

    }
}