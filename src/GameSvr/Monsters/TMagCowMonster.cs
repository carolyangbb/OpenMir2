using SystemModule;

namespace GameSvr
{
    public class TMagCowMonster : TATMonster
    {
        public TMagCowMonster() : base()
        {
            this.SearchRate = 1500 + ((long)new System.Random(1500).Next());
        }

        public void MagicAttack(byte dir)
        {
            int dam;
            TCreature cret;
            this.Dir = dir;
            TAbility _wvar1 = this.WAbil;
            dam = HUtil32.LoByte(_wvar1.DC) + new System.Random(HiByte(_wvar1.DC) - HUtil32.LoByte(_wvar1.DC) + 1).Next();
            if (dam <= 0)
            {
                return;
            }
            this.SendRefMsg(Grobal2.RM_HIT, this.Dir, this.CX, this.CY, 0, "");
            cret = this.GetFrontCret();
            if (cret != null)
            {
                if (this.IsProperTarget(cret))
                {
                    if (cret.AntiMagic <= new System.Random(50).Next())
                    {
                        dam = cret.GetMagStruckDamage(this, dam);
                        if (dam > 0)
                        {
                            cret.StruckDamage(dam, this);
                            cret.SendDelayMsg(Grobal2.RM_STRUCK, Grobal2.RM_REFMESSAGE, (ushort)dam, cret.WAbil.HP, cret.WAbil.MaxHP, this.ActorId, "", 300);
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
                        MagicAttack(targdir);
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