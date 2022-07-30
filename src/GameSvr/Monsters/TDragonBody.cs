using System;
using SystemModule;

namespace GameSvr
{
    public class TDragonBody : TATMonster
    {
        private bool bofirst = false;

        public TDragonBody() : base()
        {
            bofirst = true;
            this.HideMode = true;
            this.RaceServer = Grobal2.RC_DRAGONBODY;
            this.ViewRange = 0;
            this.BoWalkWaitMode = true;
            this.BoDontMove = true;
        }
        
        public override void RecalcAbilitys()
        {
            base.RecalcAbilitys();
            ResetLevel();
        }

        protected void ResetLevel()
        {
            
        }

        protected override bool AttackTarget()
        {
            bool result;
            result = false;
            return result;
        }

        protected void RangeAttack(TCreature targ)
        {
        }

        public override void Struck(TCreature hiter)
        {
            if (hiter != null)
            {
                if ((Math.Abs(hiter.CX - this.CX) <= 8) && (Math.Abs(hiter.CY - this.CY) <= 8))
                {
                    this.SendMsg(this, Grobal2.RM_DRAGON_EXP, 0, new System.Random(3).Next() + 1, 0, 0, "");
                }
            }
            base.Struck(hiter);
        }

        public override void Run()
        {
            if (bofirst)
            {
                bofirst = false;
                this.Dir = 5;
                this.HideMode = false;
                this.SendRefMsg(Grobal2.RM_DIGUP, this.Dir, this.CX, this.CY, 0, "");
                ResetLevel();
            }
            base.Run();
        }

    }
}