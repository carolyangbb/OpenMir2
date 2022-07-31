using System;
using SystemModule;

namespace GameSvr
{
    public class THiddenNpc : TMerchant
    {
        protected bool RunDone = false;
        protected int DigupRange = 0;
        protected int DigdownRange = 0;

        public THiddenNpc() : base()
        {
            RunDone = false;
            this.ViewRange = 7;
            this.RunNextTick = 250;
            this.SearchRate = 2500 + ((long)new System.Random(1500).Next());
            this.SearchTime = HUtil32.GetTickCount();
            DigupRange = 4;
            DigdownRange = 4;
            this.HideMode = true;
            this.StickMode = true;
            this.BoHiddenNpc = true;
        }

        protected void ComeOut()
        {
            this.HideMode = false;
            this.SendRefMsg(Grobal2.RM_HIT, this.Dir, this.CX, this.CY, 0, "");
        }

        protected void ComeDown()
        {
            this.SendRefMsg(Grobal2.RM_DISAPPEAR, 0, 0, 0, 0, "");
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
                M2Share.MainOutMessage("[Exception] THiddenNpc VisbleActors Dispose(..)");
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
            TCreature nearcret = null;
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
                            nearcret = this.GetNearMonster();
                        }
                        boidle = false;
                        if (nearcret != null)
                        {
                            if ((Math.Abs(nearcret.CX - this.CX) > DigdownRange) || (Math.Abs(nearcret.CY - this.CY) > DigdownRange))
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
                    }
                }
            }
            base.Run();
        }
    }
}

