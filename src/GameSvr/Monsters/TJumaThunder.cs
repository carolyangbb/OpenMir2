using System;
using System.Collections;
using SystemModule;

namespace GameSvr
{
    public class TJumaThunder : TScultureMonster
    {
        public TJumaThunder() : base()
        {
            this.ViewRange = 11;
            this.MeltArea = 5;
        }

        protected void RangeAttack(TCreature targ)
        {
            this.Dir = M2Share.GetNextDirection(this.CX, this.CY, targ.CX, targ.CY);
            this.SendRefMsg(Grobal2.RM_LIGHTING, this.Dir, this.CX, this.CY, targ.ActorId, "");
            TAbility _wvar1 = this.WAbil;
            int pwr = HUtil32._MAX(0, HUtil32.LoByte(_wvar1.DC) + new System.Random(HiByte(_wvar1.DC) - HUtil32.LoByte(_wvar1.DC) + 1).Next());
            ArrayList list = new ArrayList();
            this.GetMapCreatures(this.PEnvir, targ.CX, targ.CY, 1, list);
            for (var i = 0; i < list.Count; i++)
            {
                TCreature cret = (TCreature)list[i];
                if (this.IsProperTarget(cret))
                {
                    if (new System.Random(18).Next() > (cret.AntiMagic * 3))
                    {
                        int dam = cret.GetMagStruckDamage(this, pwr);
                        if (cret != targ)
                        {
                            dam = dam / 2;
                        }
                        if (dam > 0)
                        {
                            cret.StruckDamage(dam, this);
                            cret.SendDelayMsg(Grobal2.RM_STRUCK, Grobal2.RM_REFMESSAGE, (short)dam, cret.WAbil.HP, cret.WAbil.MaxHP, this.ActorId, "", 800);
                        }
                    }
                }
            }
            list.Free();
        }

        protected override bool AttackTarget()
        {
            byte targdir = 0;
            bool result = false;
            if (this.TargetCret != null)
            {
                if (GetCurrentTime - this.HitTime > this.GetNextHitTime())
                {
                    this.HitTime = GetCurrentTime;
                    if ((Math.Abs(this.CX - this.TargetCret.CX) <= this.ViewRange) && (Math.Abs(this.CY - this.TargetCret.CY) <= this.ViewRange))
                    {
                        if (this.TargetInAttackRange(this.TargetCret, ref targdir))
                        {
                            this.TargetFocusTime = HUtil32.GetTickCount();
                            this.Attack(this.TargetCret, targdir);
                            result = true;
                        }
                        else
                        {
                            if (new System.Random(3).Next() == 0)
                            {
                                RangeAttack(this.TargetCret);
                                result = true;
                            }
                            else
                            {
                                result = base.AttackTarget();
                            }
                        }
                    }
                    else
                    {
                        if (this.TargetCret.MapName == this.MapName)
                        {
                            if ((Math.Abs(this.CX - this.TargetCret.CX) <= this.ViewRange) && (Math.Abs(this.CY - this.TargetCret.CY) <= this.ViewRange))
                            {
                                this.SetTargetXY(this.TargetCret.CX, this.TargetCret.CY);
                            }
                        }
                        else
                        {
                            this.LoseTarget();
                        }
                    }
                }
            }
            return result;
        }

    }
}