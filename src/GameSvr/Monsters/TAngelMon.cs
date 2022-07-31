using System;
using SystemModule;

namespace GameSvr
{
    public class TAngelMon : TATMonster
    {
        private bool bofirst = false;

        public TAngelMon() : base()
        {
            bofirst = true;
            this.HideMode = true;
            this.RaceServer = Grobal2.RC_ANGEL;
            this.ViewRange = 10;
        }

        public override void RecalcAbilitys()
        {
            BeforeRecalcAbility();
            base.RecalcAbilitys();
        }

        protected void BeforeRecalcAbility()
        {
            switch (this.SlaveMakeLevel)
            {
                case 1:
                    this.Abil.MaxHP = 200;
                    this.Abil.AC = MakeWord(4, 5);
                    this.Abil.MC = MakeWord(11, 20);
                    break;
                case 2:
                    this.Abil.MaxHP = 300;
                    this.Abil.AC = MakeWord(5, 6);
                    this.Abil.MC = MakeWord(13, 23);
                    break;
                case 3:
                    this.Abil.MaxHP = 450;
                    this.Abil.AC = MakeWord(6, 9);
                    this.Abil.MC = MakeWord(18, 28);
                    break;
                default:
                    this.Abil.MaxHP = 150;
                    this.Abil.AC = MakeWord(4, 4);
                    this.Abil.MC = MakeWord(10, 18);
                    break;
            }
            this.Abil.MAC = MakeWord(4, 4);
            this.AddAbil.HP = 0;
        }

        protected void AfterRecalcAbility()
        {
            this.NextHitTime = 3100 - (this.SlaveMakeLevel * 300);
            this.NextWalkTime = 600 - (this.SlaveMakeLevel * 50);
            this.WalkTime = GetCurrentTime + 2000;
        }

        protected void ResetLevel()
        {
            this.WAbil.HP = this.WAbil.MaxHP;
        }

        public int RangeAttackTo_GetPower1(int power, int trainrate)
        {
            return HUtil32.MathRound((10 + trainrate * 0.9) * (power / 100));
        }

        public int RangeAttackTo_CalcMagicPower()
        {
            int result;
            result = 8 + new System.Random(20).Next();
            return result;
        }

        protected void RangeAttackTo(TCreature targ)
        {
            int pwr;
            int dam;
            if (targ == null)
            {
                return;
            }
            if (this.IsProperTarget(targ))
            {
                this.Dir = M2Share.GetNextDirection(this.CX, this.CY, targ.CX, targ.CY);
                this.SendRefMsg(Grobal2.RM_LIGHTING, this.Dir, this.CX, this.CY, targ.ActorId, "");
                pwr = this.GetAttackPower(RangeAttackTo_GetPower1(RangeAttackTo_CalcMagicPower(), 0) + LoByte(this.WAbil.MC), HiByte(this.WAbil.MC) - LoByte(this.WAbil.MC) + 1);
                if (targ.LifeAttrib == Grobal2.LA_UNDEAD)
                {
                    pwr = HUtil32.MathRound(pwr * 1.5);
                }
                dam = targ.GetMagStruckDamage(this, pwr);
                if (dam > 0)
                {
                    targ.StruckDamage(dam, this);
                    targ.SendDelayMsg(Grobal2.RM_STRUCK, Grobal2.RM_REFMESSAGE, (short)dam, targ.WAbil.HP, targ.WAbil.MaxHP, this.ActorId, "", 800);
                }
            }
        }

        protected override bool AttackTarget()
        {
            bool result;
            result = false;
            if ((this.TargetCret != null) && (this.Master != null) && (this.TargetCret != this.Master))
            {
                if (GetCurrentTime - this.HitTime > this.GetNextHitTime())
                {
                    this.HitTime = GetCurrentTime;
                    if ((Math.Abs(this.CX - this.TargetCret.CX) <= this.ViewRange) && (Math.Abs(this.CY - this.TargetCret.CY) <= this.ViewRange) && (!this.TargetCret.Death))
                    {
                        RangeAttackTo(this.TargetCret);
                        result = true;
                    }
                }
            }
            // LoseTarget;
            this.BoLoseTargetMoment = true;
            // sonmg

            return result;
        }

        public override void Run()
        {
            try
            {
                if (bofirst)
                {
                    bofirst = false;
                    this.Dir = 5;
                    this.HideMode = false;
                    this.SendRefMsg(Grobal2.RM_DIGUP, this.Dir, this.CX, this.CY, 0, "");
                    RecalcAbilitys();
                    AfterRecalcAbility();
                    // 瘤盔 付过矫 掉饭捞 滚弊 荐沥
                    ResetLevel();
                }
                base.Run();
            }
            catch
            {
                M2Share.MainOutMessage("EXCEPTION TANGEL");
            }
        }

    }
}