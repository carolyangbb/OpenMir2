using System;
using SystemModule;

namespace GameSvr
{
    public class TArcherMaster : TATMonster
    {
        public TArcherMaster() : base()
        {
            this.ViewRange = 12;
        }

        private void ShotArrow(TCreature targ)
        {
            if (targ == null)
            {
                return;
            }
            if ((!targ.Death) && this.IsProperTarget(targ))
            {
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
        }

        protected override bool AttackTarget()
        {
            if (GetCurrentTime - this.HitTime > this.GetNextHitTime())
            {
                this.HitTime = GetCurrentTime;
                ShotArrow(this.TargetCret);
            }
            return true;
        }

        public override void Run()
        {
            int dis = 9999;
            TCreature nearcret = null;
            if (!this.RunDone && this.IsMoveAble())
            {
                if (HUtil32.GetTickCount() - this.SearchEnemyTime > 5000)
                {
                    this.SearchEnemyTime  =  HUtil32.GetTickCount();
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
                        this.SelectTarget(nearcret);
                    }
                }
                AttackTarget();
                if (GetCurrentTime - this.WalkTime > this.GetNextWalkTime())
                {
                    if (this.TargetCret != null)
                    {
                        if ((Math.Abs(this.CX - this.TargetCret.CX) <= 4) && (Math.Abs(this.CY - this.TargetCret.CY) <= 4))
                        {
                            if ((Math.Abs(this.CX - this.TargetCret.CX) <= 2) && (Math.Abs(this.CY - this.TargetCret.CY) <= 2))
                            {
                                if (new System.Random(3).Next() == 0)
                                {
                                    M2Share.GetBackPosition(this, ref this.TargetX, ref this.TargetY);
                                    if (this.TargetX != -1)
                                    {
                                        this.GotoTargetXY();
                                    }
                                }
                            }
                        }
                    }
                }
            }
            base.Run();
        }
    }
}