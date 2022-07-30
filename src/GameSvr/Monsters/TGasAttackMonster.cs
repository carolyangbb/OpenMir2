using SystemModule;

namespace GameSvr
{
    public class TGasAttackMonster : TATMonster
    {
        public TGasAttackMonster() : base()
        {
            this.SearchRate = 1500 + ((long)new System.Random(1500).Next());
            this.BoAnimal = true;
        }

        public virtual TCreature GasAttack(byte dir)
        {
            TCreature result;
            int dam;
            TCreature cret;
            result = null;
            this.Dir = dir;
            TAbility _wvar1 = this.WAbil;
            dam = _wvar1.Lobyte(_wvar1.DC) + new System.Random((short)HiByte(_wvar1.DC) - _wvar1.Lobyte(_wvar1.DC) + 1).Next();
            if (dam <= 0)
            {
                return result;
            }
            this.SendRefMsg(Grobal2.RM_HIT, this.Dir, this.CX, this.CY, 0, "");
            cret = this.GetFrontCret();
            if (cret != null)
            {
                if (this.IsProperTarget(cret))
                {
                    if (new System.Random(cret.SpeedPoint).Next() < this.AccuracyPoint)
                    {
                        dam = cret.GetMagStruckDamage(this, dam);
                        if (dam > 0)
                        {
                            cret.StruckDamage(dam, this);
                            cret.SendDelayMsg((TCreature)Grobal2.RM_STRUCK, Grobal2.RM_REFMESSAGE, dam, cret.WAbil.HP, cret.WAbil.MaxHP, this.ActorId, "", 300);
                            if (this.RaceServer == Grobal2.RC_TOXICGHOST)
                            {
                                if (new System.Random(20 + cret.AntiPoison).Next() == 0)
                                {
                                    cret.MakePoison(Grobal2.POISON_DECHEALTH, 30, 1);
                                }
                            }
                            else
                            {
                                if (new System.Random(20 + cret.AntiPoison).Next() == 0)
                                {
                                    cret.MakePoison(Grobal2.POISON_STONE, 5, 0);
                                }
                            }
                            result = cret;
                        }
                    }
                }
            }
            return result;
        }

        protected override bool AttackTarget()
        {
            bool result;
            byte targdir;
            result = false;
            if (this.TargetCret != null)
            {
                if (this.TargetInAttackRange(this.TargetCret, ref targdir))
                {
                    if (GetCurrentTime - this.HitTime > this.GetNextHitTime())
                    {
                        this.HitTime = GetCurrentTime;
                        this.TargetFocusTime = GetTickCount;
                        GasAttack(targdir);
                        this.BreakHolySeize();
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

    }
}