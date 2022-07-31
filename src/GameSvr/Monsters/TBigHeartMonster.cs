using System;
using SystemModule;

namespace GameSvr
{
    public class TBigHeartMonster : TAnimal
    {
        public TBigHeartMonster() : base()
        {
            this.ViewRange = 16;
            this.BoAnimal = false;
        }

        protected bool AttackTarget()
        {
            bool result;
            int i;
            int pwr;
            TCreature cret;
            result = false;
            if (GetCurrentTime - this.HitTime > this.GetNextHitTime())
            {
                this.HitTime = GetCurrentTime;
                this.HitMotion(Grobal2.RM_HIT, this.Dir, this.CX, this.CY);
                TAbility _wvar1 = this.WAbil;
                pwr = HUtil32._MAX(0, HUtil32.LoByte(_wvar1.DC) + new System.Random(HiByte(_wvar1.DC) - HUtil32.LoByte(_wvar1.DC) + 1).Next());
                for (i = 0; i < this.VisibleActors.Count; i++)
                {
                    cret = (TCreature)this.VisibleActors[i].cret;
                    if ((!cret.Death) && this.IsProperTarget(cret))
                    {
                        if ((Math.Abs(this.CX - cret.CX) <= this.ViewRange) && (Math.Abs(this.CY - cret.CY) <= this.ViewRange))
                        {
                            this.SendDelayMsg(this, Grobal2.RM_DELAYMAGIC, (short)pwr, HUtil32.MakeLong(cret.CX, cret.CY), 1, cret.ActorId, "", 200);
                            this.SendRefMsg(Grobal2.RM_NORMALEFFECT, 0, cret.CX, cret.CY, Grobal2.NE_HEARTPALP, "");
                        }
                    }
                }
                result = true;
            }
            return result;
        }

        public override void Run()
        {
            if (this.IsMoveAble())
            {
                if (this.VisibleActors.Count > 0)
                {
                    AttackTarget();
                }
            }
            base.Run();
        }
    }
}