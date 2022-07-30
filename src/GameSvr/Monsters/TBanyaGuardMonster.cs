using System;
using System.Collections;
using SystemModule;

namespace GameSvr
{
    public class TBanyaGuardMonster : TSkeletonKingMonster
    {
        public TBanyaGuardMonster() : base()
        {
            this.ChainShotCount = 6;
            this.BoCallFollower = false;
        }

        public override void Attack(TCreature target, byte dir)
        {
            TAbility _wvar1 = this.WAbil;
            int pwr = this.GetAttackPower(_wvar1.Lobyte(_wvar1.DC), (short)HiByte(_wvar1.DC) - _wvar1.Lobyte(_wvar1.DC));
            this.HitHit2(target, 0, pwr, true);
        }

        public override void RangeAttack(TCreature targ)
        {
            int i;
            int pwr;
            int dam;
            short sx = 0;
            short sy = 0;
            short tx = 0;
            short ty = 0;
            ArrayList list;
            TCreature cret;
            if (targ == null)
            {
                return;
            }
            this.Dir = M2Share.GetNextDirection(this.CX, this.CY, targ.CX, targ.CY);
            this.SendRefMsg(Grobal2.RM_LIGHTING, this.Dir, this.CX, this.CY, targ.ActorId, "");
            if (M2Share.GetNextPosition(this.PEnvir, this.CX, this.CY, this.Dir, 1, ref sx, ref sy))
            {
                M2Share.GetNextPosition(this.PEnvir, this.CX, this.CY, this.Dir, 9, ref tx, ref ty);
                TAbility _wvar1 = this.WAbil;
                pwr = _wvar1._MAX(0, _wvar1.Lobyte(_wvar1.DC) + new System.Random((short)HiByte(_wvar1.DC) - _wvar1.Lobyte(_wvar1.DC) + 1).Next());
                list = new ArrayList();
                this.PEnvir.GetAllCreature(targ.CX, targ.CY, true, list);
                for (i = 0; i < list.Count; i++)
                {
                    cret = (TCreature)list[i];
                    if (this.IsProperTarget(cret))
                    {
                        dam = cret.GetMagStruckDamage(this, pwr);
                        if (dam > 0)
                        {
                            cret.StruckDamage(dam, this);
                            cret.SendDelayMsg((TCreature)Grobal2.RM_STRUCK, Grobal2.RM_REFMESSAGE, dam, cret.WAbil.HP, cret.WAbil.MaxHP, this.ActorId, "", 800);
                        }
                    }
                }
                list.Free();
            }
        }

        protected override bool AttackTarget()
        {
            byte targdir=0;
            bool result = false;
            if (this.TargetCret != null)
            {
                if (GetCurrentTime - this.HitTime > this.GetNextHitTime())
                {
                    this.HitTime = GetCurrentTime;
                    if ((Math.Abs(this.CX - this.TargetCret.CX) <= 7) && (Math.Abs(this.CY - this.TargetCret.CY) <= 7))
                    {
                        if (this.TargetInAttackRange(this.TargetCret, ref targdir) && (new System.Random(3).Next() != 0))
                        {
                            this.TargetFocusTime  =  HUtil32.GetTickCount();
                            Attack(this.TargetCret, targdir);
                            result = true;
                        }
                        else
                        {
                            if (this.ChainShot < this.ChainShotCount - 1)
                            {
                                this.ChainShot++;
                                this.TargetFocusTime  =  HUtil32.GetTickCount();
                                RangeAttack(this.TargetCret);
                            }
                            else
                            {
                                if (new System.Random(5).Next() == 0)
                                {
                                    this.ChainShot = 0;
                                }
                            }
                            result = true;
                        }
                    }
                    else
                    {
                        if (this.TargetCret.MapName == this.MapName)
                        {
                            if ((Math.Abs(this.CX - this.TargetCret.CX) <= 11) && (Math.Abs(this.CY - this.TargetCret.CY) <= 11))
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