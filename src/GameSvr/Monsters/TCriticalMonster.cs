using SystemModule;

namespace GameSvr
{
    public class TCriticalMonster : TATMonster
    {
        public int criticalpoint = 0;

        public TCriticalMonster() : base()
        {
            criticalpoint = 0;
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
                this.HitHitEx2(target, Grobal2.RM_LIGHTING, 0, pwr, true);
            }
            else
            {
                this.HitHit2(target, 0, pwr, true);
            }
        }
    }
}