using System;
using SystemModule;

namespace GameSvr
{
    public class TStickMonster : TAnimal
    {
        protected bool RunDone = false;
        protected int DigupRange = 0;
        protected int DigdownRange = 0;

        public TStickMonster() : base()
        {
            RunDone = false;
            this.ViewRange = 7;
            this.RunNextTick = 250;
            this.SearchRate = 2500 + ((long)new System.Random(1500).Next());
            this.SearchTime = HUtil32.GetTickCount();
            this.RaceServer = Grobal2.RC_KILLINGHERB;
            DigupRange = 4;
            DigdownRange = 4;
            this.HideMode = true;
            this.StickMode = true;
            this.BoAnimal = true;
        }

        protected virtual bool AttackTarget()
        {
            byte targdir = 0;
            bool result = false;
            if (this.TargetCret != null)
            {
                if (this.TargetInAttackRange(this.TargetCret, ref targdir))
                {
                    if (GetCurrentTime - this.HitTime > this.GetNextHitTime())
                    {
                        this.HitTime = GetCurrentTime;
                        this.TargetFocusTime = HUtil32.GetTickCount();
                        this.Attack(this.TargetCret, targdir);
                    }
                    result = true;
                }
                else
                {
                    if (this.TargetCret.MapName == this.MapName)
                    {
                        this.SetTargetXY(this.TargetCret.CX, this.TargetCret.CY);
                    }
                    else
                    {
                        this.LoseTarget();
                    }
                }
            }
            return result;
        }

        protected virtual void ComeOut()
        {
            this.HideMode = false;
            this.SendRefMsg(Grobal2.RM_DIGUP, this.Dir, this.CX, this.CY, 0, "");
        }

        protected virtual void ComeDown()
        {
            this.SendRefMsg(Grobal2.RM_DIGDOWN, 0, this.CX, this.CY, 0, "");
            try
            {
                for (var i = 0; i < this.VisibleActors.Count; i++)
                {
                    Dispose(this.VisibleActors[i]);
                }
                this.VisibleActors.Clear();
            }
            catch
            {
                M2Share.MainOutMessage("[Exception] TStickMonster VisbleActors Dispose(..)");
            }
            this.HideMode = true;
        }

        protected void CheckComeOut()
        {
            TCreature cret;
            for (var i = 0; i < this.VisibleActors.Count; i++)
            {
                cret = (TCreature)this.VisibleActors[i].cret;
                if ((!cret.Death) && this.IsProperTarget(cret) && (!cret.BoHumHideMode || this.BoViewFixedHide))
                {
                    if ((Math.Abs(this.CX - cret.CX) <= DigupRange) && (Math.Abs(this.CY - cret.CY) <= DigupRange))
                    {
                        ComeOut();
                        break;
                    }
                }
            }
        }

        public override void RunMsg(TMessageInfo msg)
        {
            base.RunMsg(msg);
        }

        public override void Run()
        {
            bool boidle;
            if (this.IsMoveAble())
            {
                if (GetCurrentTime - this.WalkTime > this.GetNextWalkTime())
                {
                    this.WalkTime = GetCurrentTime;
                    if (this.HideMode)
                    {
                        CheckComeOut();
                    }
                    else
                    {
                        if (GetCurrentTime - this.HitTime > this.GetNextHitTime())
                        {
                            this.MonsterNormalAttack();
                        }
                        boidle = false;
                        if (this.TargetCret != null)
                        {
                            if ((Math.Abs(this.TargetCret.CX - this.CX) > DigdownRange) || (Math.Abs(this.TargetCret.CY - this.CY) > DigdownRange))
                            {
                                boidle = true;
                            }
                        }
                        else
                        {
                            boidle = true;
                        }
                        if (boidle)
                        {
                            ComeDown();
                        }
                        else if (AttackTarget())
                        {
                            base.Run();
                            return;
                        }
                    }
                }
            }
            base.Run();
        }
    }
}

