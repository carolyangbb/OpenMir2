using System;
using System.Collections;
using SystemModule;

namespace GameSvr
{
    public class TFoxWizard : TATMonster
    {
        private long WarpTime = 0;

        public TFoxWizard() : base()
        {
            this.SearchRate = 2500 + ((long)new System.Random(1500).Next());
        }

        public override void Initialize()
        {
            WarpTime = HUtil32.GetTickCount();
            this.ViewRange = 7;
            base.Initialize();
        }

        public override void Attack(TCreature target, byte dir)
        {
            if (target == null)
            {
                return;
            }
            int wide = 0;
            this.Dir = M2Share.GetNextDirection(this.CX, this.CY, target.CX, target.CY);
            this.SendRefMsg(Grobal2.RM_LIGHTING, this.Dir, this.CX, this.CY, target.ActorId, "");
            TAbility _wvar1 = this.WAbil;
            int pwr = this.GetAttackPower(HUtil32.LoByte(_wvar1.DC), HiByte(_wvar1.DC) - HUtil32.LoByte(_wvar1.DC));
            if (pwr <= 0)
            {
                return;
            }
            ArrayList rlist = new ArrayList();
            this.GetMapCreatures(this.PEnvir, target.CX, target.CY, wide, rlist);
            for (var i = 0; i < rlist.Count; i++)
            {
                TCreature cret = (TCreature)rlist[i];
                if (this.IsProperTarget(cret))
                {
                    this.SelectTarget(cret);
                    cret.SendDelayMsg(this, Grobal2.RM_MAGSTRUCK, 0, pwr, 0, 0, "", 600);
                }
            }
            rlist.Free();
        }

        public void RangeAttack(TCreature targ)
        {
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
            this.SendRefMsg(Grobal2.RM_LIGHTING_1, this.Dir, this.CX, this.CY, targ.ActorId, "");
            if (M2Share.GetNextPosition(this.PEnvir, this.CX, this.CY, this.Dir, 1, ref sx, ref sy))
            {
                M2Share.GetNextPosition(this.PEnvir, this.CX, this.CY, this.Dir, 9, ref tx, ref ty);
                TAbility _wvar1 = this.WAbil;
                int pwr = HUtil32._MAX(0, HUtil32.LoByte(_wvar1.DC) + new System.Random(HiByte(_wvar1.DC) - HUtil32.LoByte(_wvar1.DC) + 1).Next());
                list = new ArrayList();
                this.PEnvir.GetAllCreature(targ.CX, targ.CY, true, list);
                for (var i = 0; i < list.Count; i++)
                {
                    cret = (TCreature)list[i];
                    if (this.IsProperTarget(cret))
                    {
                        int dam = cret.GetMagStruckDamage(this, pwr);
                        if (dam > 0)
                        {
                            cret.StruckDamage(dam, this);
                            cret.SendDelayMsg(Grobal2.RM_STRUCK, Grobal2.RM_REFMESSAGE, (short)dam, cret.WAbil.HP, cret.WAbil.MaxHP, this.ActorId, "", 800);
                        }
                    }
                }
                list.Free();
            }
        }

        public override void Run()
        {
            if (!this.RunDone && this.IsMoveAble())
            {
                if (GetCurrentTime - this.WalkTime > this.GetNextWalkTime())
                {
                    if (this.TargetCret != null)
                    {
                        if ((Math.Abs(this.CX - this.TargetCret.CX) <= 5) && (Math.Abs(this.CY - this.TargetCret.CY) <= 5))
                        {
                            if ((Math.Abs(this.CX - this.TargetCret.CX) <= 2) && (Math.Abs(this.CY - this.TargetCret.CY) <= 2))
                            {
                                if (new System.Random(3).Next() == 0)
                                {
                                    M2Share.GetBackPosition(this, ref this.TargetX, ref this.TargetY);
                                }
                            }
                            else
                            {
                                M2Share.GetBackPosition(this, ref this.TargetX, ref this.TargetY);
                            }
                        }
                    }
                }
            }
            base.Run();
        }

        public override void RunMsg(TMessageInfo msg)
        {
            switch (msg.Ident)
            {
                case Grobal2.RM_REFMESSAGE:
                    if (((int)msg.sender) == Grobal2.RM_STRUCK)
                    {
                        if (new System.Random(100).Next() < 30)
                        {
                            if ((HUtil32.GetTickCount() - WarpTime > 2000) && (!this.Death))
                            {
                                WarpTime = HUtil32.GetTickCount();
                                this.SendRefMsg(Grobal2.RM_NORMALEFFECT, 0, this.CX, this.CY, Grobal2.NE_FOX_MOVEHIDE, "");
                                this.RandomSpaceMoveInRange(2, 4, 4);
                                this.SendRefMsg(Grobal2.RM_NORMALEFFECT, 0, this.CX, this.CY, Grobal2.NE_FOX_MOVESHOW, "");
                            }
                        }
                    }
                    break;
            }
            base.RunMsg(msg);
        }

        protected override bool AttackTarget()
        {
            bool result = false;
            if (this.TargetCret != null)
            {
                if (GetCurrentTime - this.HitTime > this.GetNextHitTime())
                {
                    this.HitTime = GetCurrentTime;
                    if ((Math.Abs(this.CX - this.TargetCret.CX) <= 7) && (Math.Abs(this.CY - this.TargetCret.CY) <= 7))
                    {
                        if (new System.Random(10).Next() < 7)
                        {
                            Attack(this.TargetCret, this.Dir);
                            result = true;
                        }
                        else if (new System.Random(10).Next() < 6)
                        {
                            RangeAttack(this.TargetCret);
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