using System;
using SystemModule;

namespace GameSvr
{
    public class TExplosionSpider : TMonster
    {
        public long maketime = 0;

        public TExplosionSpider() : base()
        {
            this.ViewRange = 5;
            this.RunNextTick = 250;
            this.SearchRate = 2500 + ((long)new System.Random(1500).Next());
            this.SearchTime = 0;
            maketime = HUtil32.GetTickCount();
        }

        public void DoSelfExplosion()
        {
            this.WAbil.HP = 0;
            TAbility _wvar1 = this.WAbil;
            int pwr = HUtil32._MAX(0, HUtil32.LoByte(_wvar1.DC) + new System.Random(HiByte(_wvar1.DC) - HUtil32.LoByte(_wvar1.DC) + 1).Next());
            for (var i = 0; i < this.VisibleActors.Count; i++)
            {
                TCreature cret = (TCreature)this.VisibleActors[i].cret;
                if ((Math.Abs(cret.CX - this.CX) <= 1) && (Math.Abs(cret.CY - this.CY) <= 1))
                {
                    if ((!cret.Death) && this.IsProperTarget(cret))
                    {
                        int dam = 0;
                        dam = dam + cret.GetHitStruckDamage(this, pwr / 2);
                        dam = dam + cret.GetMagStruckDamage(this, pwr / 2);
                        if (dam > 0)
                        {
                            cret.StruckDamage(dam, this);
                            cret.SendDelayMsg(Grobal2.RM_STRUCK, Grobal2.RM_REFMESSAGE, (short)dam, cret.WAbil.HP, cret.WAbil.MaxHP, this.ActorId, "", 700);
                        }
                    }
                }
            }
        }

        protected override bool AttackTarget()
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
                        DoSelfExplosion();
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

        public override void Run()
        {
            if ((!this.Death) && (!this.BoGhost))
            {
                if (HUtil32.GetTickCount() - maketime > 1 * 60 * 1000)
                {
                    maketime = HUtil32.GetTickCount();
                    DoSelfExplosion();
                }
            }
            base.Run();
        }
    }
}