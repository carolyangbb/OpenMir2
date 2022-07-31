using System;
using System.Collections;
using SystemModule;

namespace GameSvr
{
    public class TFoxTaoist : TATMonster
    {
        private bool BoRecallComplete = false;

        public TFoxTaoist() : base()
        {
            this.SearchRate = 2500 + ((long)new System.Random(1500).Next());
        }

        public override void Initialize()
        {
            BoRecallComplete = false;
            this.ViewRange = 7;
            base.Initialize();
        }

        public override void Attack(TCreature target, byte dir)
        {
            this.Dir = dir;
            TAbility _wvar1 = this.WAbil;
            int pwr = this.GetAttackPower(HUtil32.LoByte(_wvar1.DC), HiByte(_wvar1.DC) - HUtil32.LoByte(_wvar1.DC));
            if (pwr <= 0)
            {
                return;
            }
            this.SendRefMsg(Grobal2.RM_HIT, this.Dir, this.CX, this.CY, target.ActorId, "");
            if (this.IsProperTarget(target))
            {
                target.StruckDamage(pwr, this);
                target.SendDelayMsg(Grobal2.RM_STRUCK, Grobal2.RM_REFMESSAGE, (short)pwr, target.WAbil.HP, target.WAbil.MaxHP, this.ActorId, "", 500);
            }
        }

        public void RangeAttack(TCreature targ)
        {
            if (targ == null)
            {
                return;
            }
            int sec = 60;
            int pwr = 70;
            int skilllevel = 3;
            this.MagMakeCurseArea(targ.CX, targ.CY, 2, sec, pwr, skilllevel, false);
            this.Dir = M2Share.GetNextDirection(this.CX, this.CY, targ.CX, targ.CY);
            this.SendRefMsg(Grobal2.RM_LIGHTING_1, this.Dir, this.CX, this.CY, targ.ActorId, "");
        }

        public void RangeAttack2(TCreature targ)
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
            this.SendRefMsg(Grobal2.RM_LIGHTING, this.Dir, this.CX, this.CY, targ.ActorId, "");
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
            TCreature cret = null;
            if (!BoRecallComplete)
            {
                if (this.WAbil.HP <= this.WAbil.MaxHP / 2)
                {
                    this.SendRefMsg(Grobal2.RM_LIGHTING_2, this.Dir, this.CX, this.CY, this.TargetCret.ActorId, "");
                    cret = M2Share.UserEngine.AddCreatureSysop(this.PEnvir.MapName, (short)(this.CX + 1), this.CY, "BlackFoxFolks");
                    if (cret != null)
                    {
                        cret.SendRefMsg(Grobal2.RM_NORMALEFFECT, 0, cret.CX, cret.CY, Grobal2.NE_FOX_MOVESHOW, "");
                    }
                    cret = M2Share.UserEngine.AddCreatureSysop(this.PEnvir.MapName, (short)(this.CX - 1), this.CY, "BlackFoxFolks");
                    if (cret != null)
                    {
                        cret.SendRefMsg(Grobal2.RM_NORMALEFFECT, 0, cret.CX, cret.CY, Grobal2.NE_FOX_MOVESHOW, "");
                    }
                    cret = M2Share.UserEngine.AddCreatureSysop(this.PEnvir.MapName, this.CX, (short)(this.CY + 1), "RedFoxFolks");
                    if (cret != null)
                    {
                        cret.SendRefMsg(Grobal2.RM_NORMALEFFECT, 0, cret.CX, cret.CY, Grobal2.NE_FOX_MOVESHOW, "");
                    }
                    cret = M2Share.UserEngine.AddCreatureSysop(this.PEnvir.MapName, this.CX, (short)(this.CY - 1), "RedFoxFolks");
                    if (cret != null)
                    {
                        cret.SendRefMsg(Grobal2.RM_NORMALEFFECT, 0, cret.CX, cret.CY, Grobal2.NE_FOX_MOVESHOW, "");
                    }
                    BoRecallComplete = true;
                }
            }
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

        protected override bool AttackTarget()
        {
            byte targdir = 0;
            bool result = false;
            if (this.TargetCret != null)
            {
                if (GetCurrentTime - this.HitTime > this.GetNextHitTime())
                {
                    this.HitTime = GetCurrentTime;
                    if ((Math.Abs(this.CX - this.TargetCret.CX) <= 7) && (Math.Abs(this.CY - this.TargetCret.CY) <= 7))
                    {
                        if (this.TargetInAttackRange(this.TargetCret, ref targdir) && (new System.Random(10).Next() < 8))
                        {
                            this.TargetFocusTime = HUtil32.GetTickCount();
                            Attack(this.TargetCret, targdir);
                            result = true;
                        }
                        else
                        {
                            if ((Math.Abs(this.CX - this.TargetCret.CX) <= 6) && (Math.Abs(this.CY - this.TargetCret.CY) <= 6))
                            {
                                if (new System.Random(10).Next() < 7)
                                {
                                    RangeAttack2(this.TargetCret);
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
                                if (new System.Random(10).Next() < 6)
                                {
                                    RangeAttack(this.TargetCret);
                                    result = true;
                                }
                            }
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