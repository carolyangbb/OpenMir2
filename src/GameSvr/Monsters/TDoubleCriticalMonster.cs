using SystemModule;

namespace GameSvr
{
    public class TDoubleCriticalMonster : TATMonster
    {
        public int criticalpoint = 0;

        public TDoubleCriticalMonster() : base()
        {
            criticalpoint = 0;
        }

        public void DoubleCriticalAttack(int dam, byte dir)
        {
            int i;
            int k;
            int mx;
            int my;
            TCreature cret;
            this.Dir = dir;
            if (dam <= 0)
            {
                return;
            }
            this.SendRefMsg(Grobal2.RM_LIGHTING, this.Dir, this.CX, this.CY, 0, "");
            for (i = 0; i <= 4; i++)
            {
                for (k = 0; k <= 4; k++)
                {
                    if (M2Share.SpitMap[dir, i, k] == 1)
                    {
                        mx = this.CX - 2 + k;
                        my = this.CY - 2 + i;
                        cret = (TCreature)this.PEnvir.GetCreature(mx, my, true);
                        if ((cret != null) && (cret != this))
                        {
                            if (this.IsProperTarget(cret))
                            {
                                if (new System.Random(cret.SpeedPoint).Next() < this.AccuracyPoint)
                                {
                                    dam = cret.GetMagStruckDamage(this, dam);
                                    if (dam > 0)
                                    {
                                        cret.StruckDamage(dam, this);
                                        cret.SendDelayMsg(Grobal2.RM_STRUCK, Grobal2.RM_REFMESSAGE, (short)dam, cret.WAbil.HP, cret.WAbil.MaxHP, this.ActorId, "", 300);
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        public override void Attack(TCreature target, byte dir)
        {
            int pwr;
            TAbility _wvar1 = this.WAbil;
            pwr = this.GetAttackPower(HUtil32.LoByte(_wvar1.DC), HiByte(_wvar1.DC) - HUtil32.LoByte(_wvar1.DC));
            criticalpoint++;
            if ((criticalpoint > 5) || (new System.Random(10).Next() == 0))
            {
                criticalpoint = 0;
                pwr = HUtil32.MathRound(pwr * (this.Abil.MaxMP / 10));
                DoubleCriticalAttack(pwr, dir);
            }
            else
            {
                this.HitHit2(target, 0, pwr, true);
            }
        }
    }
}