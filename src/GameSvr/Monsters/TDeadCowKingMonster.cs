using System;
using System.Collections;
using SystemModule;

namespace GameSvr
{
    public class TDeadCowKingMonster : TSkeletonKingMonster
    {
        public TDeadCowKingMonster() : base()
        {
            this.ChainShotCount = 6;
            this.BoCallFollower = false;
        }
        
        public override void Attack(TCreature target, byte dir)
        {
            int pwr;
            int i;
            int ix;
            int iy;
            int ixf;
            int ixt;
            int iyf;
            int iyt;
            int dam;
            ArrayList list;
            TCreature cret;
            this.Dir = M2Share.GetNextDirection(this.CX, this.CY, target.CX, target.CY);
            TAbility _wvar1 = this.WAbil;
            pwr = this.GetAttackPower(_wvar1.Lobyte(_wvar1.DC), (short)HiByte(_wvar1.DC) - _wvar1.Lobyte(_wvar1.DC));
            ixf = _MAX(0, this.CX - 1);
            ixt = _MIN(this.PEnvir.MapWidth - 1, this.CX + 1);
            iyf = _MAX(0, this.CY - 1);
            iyt = _MIN(this.PEnvir.MapHeight - 1, this.CY + 1);
            for (ix = ixf; ix <= ixt; ix++)
            {
                for (iy = iyf; iy <= iyt; iy++)
                {
                    list = new ArrayList();
                    this.PEnvir.GetAllCreature(ix, iy, true, list);
                    for (i = 0; i < list.Count; i++)
                    {
                        cret = (TCreature)list[i];
                        if (this.IsProperTarget(cret))
                        {
                            dam = cret.GetMagStruckDamage(this, pwr);
                            if (dam > 0)
                            {
                                cret.StruckDamage(dam, this);
                                cret.SendDelayMsg((TCreature)Grobal2.RM_STRUCK, Grobal2.RM_REFMESSAGE, dam, cret.WAbil.HP, cret.WAbil.MaxHP, this.ActorId, "", 200);
                            }
                        }
                    }
                    list.Free();
                }
            }
            this.SendRefMsg(Grobal2.RM_HIT, this.Dir, this.CX, this.CY, target.ActorId, "");
        }

        public override void RangeAttack(TCreature targ)
        {
            int i;
            int ix;
            int iy;
            int ixf;
            int ixt;
            int iyf;
            int iyt;
            int pwr;
            int dam;
            short sx =0;
            short sy =0;
            short tx =0;
            short ty =0;
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
                ixf = _MAX(0, targ.CX - 2);
                ixt = _MIN(this.PEnvir.MapWidth - 1, targ.CX + 2);
                iyf = _MAX(0, targ.CY - 2);
                iyt = _MIN(this.PEnvir.MapHeight - 1, targ.CY + 2);
                for (ix = ixf; ix <= ixt; ix++)
                {
                    for (iy = iyf; iy <= iyt; iy++)
                    {
                        list = new ArrayList();
                        this.PEnvir.GetAllCreature(ix, iy, true, list);
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
            }
        }

        protected override bool AttackTarget()
        {
            bool result;
            byte targdir=0;
            result = false;
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