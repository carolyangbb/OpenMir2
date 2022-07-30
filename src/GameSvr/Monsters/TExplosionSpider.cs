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
            maketime  =  HUtil32.GetTickCount();
        }
        
        public void DoSelfExplosion()
        {
            int i;
            int pwr;
            int dam;
            TCreature cret;
            this.WAbil.HP = 0;
            TAbility _wvar1 = this.WAbil;
            pwr = _wvar1._MAX(0, _wvar1.Lobyte(_wvar1.DC) + new System.Random((short)HiByte(_wvar1.DC) - _wvar1.Lobyte(_wvar1.DC) + 1).Next());
            for (i = 0; i < this.VisibleActors.Count; i++)
            {
                cret = (TCreature)this.VisibleActors[i].cret;
                if ((Math.Abs(cret.CX - this.CX) <= 1) && (Math.Abs(cret.CY - this.CY) <= 1))
                {
                    if ((!cret.Death) && this.IsProperTarget(cret))
                    {
                        dam = 0;
                        dam = dam + cret.GetHitStruckDamage(this, pwr / 2);
                        dam = dam + cret.GetMagStruckDamage(this, pwr / 2);
                        if (dam > 0)
                        {
                            cret.StruckDamage(dam, this);
                            // wparam
                            // lparam1
                            // lparam2
                            // hiter
                            cret.SendDelayMsg((TCreature)Grobal2.RM_STRUCK, Grobal2.RM_REFMESSAGE, dam, cret.WAbil.HP, cret.WAbil.MaxHP, this.ActorId, "", 700);
                        }
                    }
                }
            }
        }

        protected override bool AttackTarget()
        {
            bool result;
            byte targdir=0;
            result = false;
            if (this.TargetCret != null)
            {
                if (this.TargetInAttackRange(this.TargetCret, ref targdir))
                {
                    if (GetCurrentTime - this.HitTime > this.GetNextHitTime())
                    {
                        this.HitTime = GetCurrentTime;
                        this.TargetFocusTime  =  HUtil32.GetTickCount();
                        // 磊气....
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
                    // <!!林狼> TargetCret := nil肺 官柴
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
                    // 磊气
                    maketime  =  HUtil32.GetTickCount();
                    DoSelfExplosion();
                }
            }
            base.Run();
        }

    }
}