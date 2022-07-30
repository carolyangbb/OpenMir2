using System;
using System.Collections;
using SystemModule;

namespace GameSvr
{
    public class TFoxPillar : TATMonster
    {
        protected bool RunDone = false;

        public TFoxPillar() : base()
        {
            RunDone = false;
            this.ViewRange = 12;
            this.RunNextTick = 250;
            this.SearchRate = 2500 + ((long)new System.Random(1500).Next());
            this.SearchTime = GetTickCount;
            this.HideMode = false;
            this.StickMode = true;
            this.BoDontMove = true;
            this.NeverDie = true;
        }

        protected override bool AttackTarget()
        {
            bool result;
            result = false;
            if (FindTarget())
            {
                if (GetCurrentTime - this.HitTime > this.GetNextHitTime())
                {
                    this.HitTime = GetCurrentTime;
                    if ((Math.Abs(this.CX - this.TargetCret.CX) <= this.ViewRange) && (Math.Abs(this.CY - this.TargetCret.CY) <= this.ViewRange))
                    {
                        if (new System.Random(5).Next() == 0)
                        {
                            RangeAttack(this.TargetCret);
                            Attack(this.TargetCret, this.Dir);
                            result = true;
                        }
                        else if (new System.Random(4).Next() < 2)
                        {
                            RangeAttack(this.TargetCret);
                            result = true;
                        }
                        else
                        {
                            Attack(this.TargetCret, this.Dir);
                            result = true;
                        }
                    }
                    else
                    {
                        if (this.TargetCret.MapName == this.MapName)
                        {
                            if ((Math.Abs(this.CX - this.TargetCret.CX) <= this.ViewRange) && (Math.Abs(this.CY - this.TargetCret.CY) <= this.ViewRange))
                            {
                                this.SetTargetXY(this.TargetCret.CX, this.TargetCret.CY);
                            }
                        }
                        else
                        {
                            this.LoseTarget();
                        }
                    }
                }
            }
            return result;
        }

        protected bool FindTarget()
        {
            bool result;
            int i;
            TCreature cret;
            result = false;
            for (i = 0; i < this.VisibleActors.Count; i++)
            {
                cret = (TCreature)this.VisibleActors[i].cret;
                if ((!cret.Death) && this.IsProperTarget(cret))
                {
                    if ((Math.Abs(this.CX - cret.CX) <= this.ViewRange) && (Math.Abs(this.CY - cret.CY) <= this.ViewRange))
                    {
                        if (cret.RaceServer == Grobal2.RC_USERHUMAN)
                        {
                            if (this.TargetCret == null)
                            {
                                this.TargetCret = cret;
                            }
                            else
                            {
                                if (new System.Random(2).Next() == 0)
                                {
                                    continue;
                                }
                                this.TargetCret = cret;
                            }
                            result = true;
                            break;
                        }
                    }
                }
            }
            return result;
        }

        public void RangeAttack(TCreature targ)
        {
            int levelgap;
            int rushdir;
            int rushDist;
            if (targ == null)
            {
                return;
            }
            // 钢府乐绰 利阑 缠绢寸变促.
            this.Dir = M2Share.GetNextDirection(this.CX, this.CY, targ.CX, targ.CY);
            this.SendRefMsg(Grobal2.RM_LIGHTING_1, this.Dir, this.CX, this.CY, targ.ActorId, "");
            rushdir = (this.Dir + 4) % 8;
            rushDist = _MAX(0, _MAX(Math.Abs(this.CX - targ.CX), Math.Abs(this.CY - targ.CY)) - 3);
            if (this.IsProperTarget(targ))
            {
                if (!((Math.Abs(this.CX - targ.CX) <= 2) && (Math.Abs(this.CY - targ.CY) <= 2)))
                {
                    if ((!targ.Death) && ((targ.RaceServer == Grobal2.RC_USERHUMAN) || (targ.Master != null)))
                    {
                        levelgap = (targ.AntiMagic * 5) + HiByte(targ.WAbil.AC) / 2;
                        if (new System.Random(50).Next() > levelgap)
                        {
                            if ((Math.Abs(this.CX - targ.CX) <= 12) && (Math.Abs(this.CY - targ.CY) <= 12))
                            {
                                targ.SendRefMsg(Grobal2.RM_LOOPNORMALEFFECT, (ushort)targ.ActorId, 1000, 0, Grobal2.NE_SIDESTONE_PULL, "");
                                targ.CharRushRush((byte)rushdir, rushDist, false);
                            }
                        }
                    }
                }
            }
        }

        public override void Attack(TCreature target, byte dir)
        {
            int i;
            int wide;
            ArrayList rlist;
            TCreature cret;
            int pwr;
            if (target == null)
            {
                return;
            }
            wide = 2;
            this.Dir = M2Share.GetNextDirection(this.CX, this.CY, target.CX, target.CY);
            this.SendRefMsg(Grobal2.RM_LIGHTING, this.Dir, this.CX, this.CY, target.ActorId, "");
            TAbility _wvar1 = this.WAbil;
            pwr = this.GetAttackPower(_wvar1.Lobyte(_wvar1.DC), (short)HiByte(_wvar1.DC) - _wvar1.Lobyte(_wvar1.DC));
            if (pwr <= 0)
            {
                return;
            }
            rlist = new ArrayList();
            this.GetMapCreatures(this.PEnvir, this.CX, this.CY, wide, rlist);
            for (i = 0; i < rlist.Count; i++)
            {
                cret = (TCreature)rlist[i];
                if (this.IsProperTarget(cret))
                {
                    this.SelectTarget(cret);
                    cret.SendDelayMsg(this, Grobal2.RM_MAGSTRUCK, 0, pwr, 0, 0, "", 600);
                }
            }
            rlist.Free();
        }
    }
}