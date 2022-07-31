using System;
using System.Collections;
using SystemModule;

namespace GameSvr
{
    public class TFoxBead : TATMonster
    {
        protected bool RunDone = false;
        public long TargetTime = 0;
        public long RangeAttackTime = 0;
        public TCreature OldTargetCret = null;
        public int OrgNextHitTime = 0;
        public long sectick = 0;

        public TFoxBead() : base()
        {
            RunDone = false;
            this.ViewRange = 16;
            this.RunNextTick = 250;
            this.SearchRate = 1500 + ((long)new System.Random(1500).Next());
            this.SearchTime = HUtil32.GetTickCount();
            this.HideMode = false;
            this.StickMode = true;
            this.BoDontMove = true;
            this.BodyState = 1;
            OrgNextHitTime = this.NextHitTime;
            sectick = HUtil32.GetTickCount();
        }

        public override void Run()
        {
            if (HUtil32.GetTickCount() - sectick > 3000)
            {
                sectick = HUtil32.GetTickCount();
                if ((!this.Death) && (!this.BoGhost))
                {
                    if (this.WAbil.HP >= this.WAbil.MaxHP * 4 / 5)
                    {
                        if (this.BodyState != 1)
                        {
                            this.BodyState = 1;
#if DEBUG
                            //svMain.UserEngine.CryCry(Grobal2.RM_CRY, this.PEnvir, this.CX, this.CY, 10000, " State(" + (this.BodyState).ToString() + ") : " + this.TargetCret.UserName);
                            // test
#endif
                            this.WAbil.DC = MakeWord(_MIN(255, LoByte(this.Abil.DC)), _MIN(255, HiByte(this.Abil.DC)));
                            this.WAbil.AC = MakeWord(_MIN(255, LoByte(this.Abil.AC)), _MIN(255, HiByte(this.Abil.AC)));
                            this.WAbil.MAC = MakeWord(_MIN(255, LoByte(this.Abil.MAC)), _MIN(255, HiByte(this.Abil.MAC)));
                            this.SendRefMsg(Grobal2.RM_FOXSTATE, this.Dir, this.CX, this.CY, this.BodyState, this.UserName);
                        }
                    }
                    else if (this.WAbil.HP >= this.WAbil.MaxHP * 3 / 5)
                    {
                        if (this.BodyState != 2)
                        {
                            this.BodyState = 2;
#if DEBUG
                            //svMain.UserEngine.CryCry(Grobal2.RM_CRY, this.PEnvir, this.CX, this.CY, 10000, " State(" + (this.BodyState).ToString() + ") : " + this.TargetCret.UserName);
#endif
                            this.WAbil.DC = MakeWord(_MIN(255, LoByte(this.Abil.DC)), _MIN(255, HiByte(this.Abil.DC) + HiByte(this.Abil.DC) / 10));
                            this.WAbil.AC = MakeWord(_MIN(255, LoByte(this.Abil.AC)), _MIN(255, HiByte(this.Abil.AC) + HiByte(this.Abil.AC) * 2 / 10));
                            this.WAbil.MAC = MakeWord(_MIN(255, LoByte(this.Abil.MAC)), _MIN(255, HiByte(this.Abil.MAC) + HiByte(this.Abil.MAC) * 2 / 10));
                            this.SendRefMsg(Grobal2.RM_FOXSTATE, this.Dir, this.CX, this.CY, this.BodyState, this.UserName);
                        }
                    }
                    else if (this.WAbil.HP >= this.WAbil.MaxHP * 2 / 5)
                    {
                        if (this.BodyState != 3)
                        {
                            this.BodyState = 3;
#if DEBUG
                            //svMain.UserEngine.CryCry(Grobal2.RM_CRY, this.PEnvir, this.CX, this.CY, 10000, " State(" + (this.BodyState).ToString() + ") : " + this.TargetCret.UserName);
#endif
                            this.WAbil.DC = MakeWord(_MIN(255, LoByte(this.Abil.DC)), _MIN(255, HiByte(this.Abil.DC) + HiByte(this.Abil.DC) * 2 / 10));
                            this.WAbil.AC = MakeWord(_MIN(255, LoByte(this.Abil.AC)), _MIN(255, HiByte(this.Abil.AC) + HiByte(this.Abil.AC) * 4 / 10));
                            this.WAbil.MAC = MakeWord(_MIN(255, LoByte(this.Abil.MAC)), _MIN(255, HiByte(this.Abil.MAC) + HiByte(this.Abil.MAC) * 4 / 10));
                            this.SendRefMsg(Grobal2.RM_FOXSTATE, this.Dir, this.CX, this.CY, this.BodyState, this.UserName);
                        }
                    }
                    else if (this.WAbil.HP >= this.WAbil.MaxHP * 1 / 5)
                    {
                        if (this.BodyState != 4)
                        {
                            this.BodyState = 4;
#if DEBUG
                            //svMain.UserEngine.CryCry(Grobal2.RM_CRY, this.PEnvir, this.CX, this.CY, 10000, " State(" + (this.BodyState).ToString() + ") : " + this.TargetCret.UserName);
#endif
                            this.WAbil.DC = MakeWord(_MIN(255, LoByte(this.Abil.DC)), _MIN(255, HiByte(this.Abil.DC) + HiByte(this.Abil.DC) * 3 / 10));
                            this.WAbil.AC = MakeWord(_MIN(255, LoByte(this.Abil.AC)), _MIN(255, HiByte(this.Abil.AC) + HiByte(this.Abil.AC) * 6 / 10));
                            this.WAbil.MAC = MakeWord(_MIN(255, LoByte(this.Abil.MAC)), _MIN(255, HiByte(this.Abil.MAC) + HiByte(this.Abil.MAC) * 4 / 10));
                            this.SendRefMsg(Grobal2.RM_FOXSTATE, this.Dir, this.CX, this.CY, this.BodyState, this.UserName);
                        }
                    }
                    else
                    {
                        if (this.BodyState != 5)
                        {
                            this.BodyState = 5;
#if DEBUG
                            //svMain.UserEngine.CryCry(Grobal2.RM_CRY, this.PEnvir, this.CX, this.CY, 10000, " State(" + (this.BodyState).ToString() + ") : " + this.TargetCret.UserName);
#endif
                            this.WAbil.DC = MakeWord(_MIN(255, LoByte(this.Abil.DC)), _MIN(255, HiByte(this.Abil.DC) + HiByte(this.Abil.DC) * 4 / 10));
                            this.WAbil.AC = MakeWord(_MIN(255, LoByte(this.Abil.AC)), _MIN(255, HiByte(this.Abil.AC) + HiByte(this.Abil.AC) * 8 / 10));
                            this.WAbil.MAC = MakeWord(_MIN(255, LoByte(this.Abil.MAC)), _MIN(255, HiByte(this.Abil.MAC) + HiByte(this.Abil.MAC) * 4 / 10));
                            this.SendRefMsg(Grobal2.RM_FOXSTATE, this.Dir, this.CX, this.CY, this.BodyState, this.UserName);
                        }
                    }
                }
            }
            base.Run();
        }

        protected override bool AttackTarget()
        {
            int nx = 0;
            int ny = 0;
            bool result = false;
            ArrayList rlist = null;
            TCreature cret = null;
            if (this.TargetCret != null)
            {
                if (GetCurrentTime - this.HitTime > this.GetNextHitTime())
                {
                    this.HitTime = GetCurrentTime;
                    if ((Math.Abs(this.CX - this.TargetCret.CX) <= this.ViewRange) && (Math.Abs(this.CY - this.TargetCret.CY) <= this.ViewRange))
                    {
                        if (new System.Random(10).Next() == 0)
                        {
                            this.SendRefMsg(Grobal2.RM_NORMALEFFECT, 0, this.CX, this.CY, Grobal2.NE_KINGSTONE_RECALL_1, "");
                            rlist = new ArrayList();
                            this.GetMapCreatures(this.PEnvir, this.CX, this.CY, 30, rlist);
                            for (var i = 0; i < rlist.Count; i++)
                            {
                                cret = (TCreature)rlist[i];
                                if ((!cret.Death) && this.IsProperTarget(cret))
                                {
                                    if ((cret.RaceServer == Grobal2.RC_USERHUMAN) && ((Math.Abs(this.CX - cret.CX) > 3) || (Math.Abs(this.CY - cret.CY) > 3)))
                                    {
                                        if (new System.Random(3).Next() < 2)
                                        {
                                            cret.SendRefMsg(Grobal2.RM_NORMALEFFECT, 0, cret.CX, cret.CY, Grobal2.NE_KINGSTONE_RECALL_2, "");
                                            if (new System.Random(2).Next() == 0)
                                            {
                                                nx = this.CX + new System.Random(3).Next() + 1;
                                                ny = this.CY + new System.Random(3).Next() + 1;
                                            }
                                            else
                                            {
                                                nx = this.CX - new System.Random(3).Next() - 1;
                                                ny = this.CY - new System.Random(3).Next() - 1;
                                            }
                                            cret.SpaceMove(this.PEnvir.MapName, (short)nx, (short)ny, 2);
                                            cret.SendRefMsg(Grobal2.RM_NORMALEFFECT, 0, cret.CX, cret.CY, Grobal2.NE_KINGSTONE_RECALL_2, "");
                                        }
                                    }
                                }
                            }
                            rlist.Free();
#if DEBUG
                            //svMain.UserEngine.CryCry(Grobal2.RM_CRY, this.PEnvir, this.CX, this.CY, 10000, " 家券 : " + this.TargetCret.UserName);
#endif
                            result = true;
                        }
                        else if (new System.Random(100).Next() < 40)
                        {
#if DEBUG
                            //svMain.UserEngine.CryCry(Grobal2.RM_CRY, this.PEnvir, this.CX, this.CY, 10000, " 檬鞘混 : " + this.TargetCret.UserName);
#endif
                            RangeAttack2(this.TargetCret);
                            result = true;
                        }
                        else if (new System.Random(10).Next() < 4)
                        {
#if DEBUG
                            //svMain.UserEngine.CryCry(Grobal2.RM_CRY, this.PEnvir, this.CX, this.CY, 10000, " 吝缴傍拜 : " + this.TargetCret.UserName);
#endif
                            Attack(this.TargetCret, this.Dir);
                            result = true;
                        }
                        else
                        {
#if DEBUG
                            //svMain.UserEngine.CryCry(Grobal2.RM_CRY, this.PEnvir, this.CX, this.CY, 10000, " 盔芭府裹困 : " + this.TargetCret.UserName);
#endif
                            RangeAttack(this.TargetCret);
                            result = true;
                        }
                        if (new System.Random(10).Next() < 4)
                        {
                            FindTarget();
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

        public bool FindTarget()
        {
            bool result = false;
            for (var i = 0; i < this.VisibleActors.Count; i++)
            {
                TCreature cret = (TCreature)this.VisibleActors[i].cret;
                if ((!cret.Death) && this.IsProperTarget(cret))
                {
                    if ((Math.Abs(this.CX - cret.CX) <= this.ViewRange) && (Math.Abs(this.CY - cret.CY) <= this.ViewRange))
                    {
                        if (cret.RaceServer == Grobal2.RC_USERHUMAN)
                        {
                            if (this.TargetCret == null)
                            {
                                this.TargetCret = cret;
                            }
                            else
                            {
                                if (new System.Random(100).Next() < 50)
                                {
                                    continue;
                                }
                                this.TargetCret = cret;
                            }
                            result = true;
                            break;
                        }
                    }
                }
            }
            return result;
        }

        public void RangeAttack(TCreature targ)
        {
            short sx = 0;
            short sy = 0;
            short tx = 0;
            short ty = 0;
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
                int pwr = HUtil32._MAX(0, HUtil32.LoByte(_wvar1.DC) + HUtil32._MIN(255, new System.Random(HiByte(_wvar1.DC) - HUtil32.LoByte(_wvar1.DC) + 1).Next()));
                pwr = pwr + new System.Random(HUtil32.LoByte(_wvar1.MC)).Next();
                pwr = pwr * 2;
                ArrayList list = new ArrayList();
                this.PEnvir.GetCreatureInRange(targ.CX, targ.CY, 2, true, list);
                for (var i = 0; i < list.Count; i++)
                {
                    TCreature cret = (TCreature)list[i];
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

        public void RangeAttack2(TCreature targ)
        {
            if (targ == null)
            {
                return;
            }
            this.SendRefMsg(Grobal2.RM_LIGHTING_2, this.Dir, this.CX, this.CY, this.ActorId, "");
            TAbility _wvar1 = this.WAbil;
            int pwr = this.GetAttackPower(HUtil32.LoByte(_wvar1.DC), HUtil32._MIN(255, HiByte(_wvar1.DC) - HUtil32.LoByte(_wvar1.DC)));
            pwr = pwr * 2;
            for (var i = 0; i < this.VisibleActors.Count; i++)
            {
                TCreature cret = (TCreature)this.VisibleActors[i].cret;
                if ((!cret.Death) && this.IsProperTarget(cret))
                {
                    if ((cret.RaceServer == Grobal2.RC_USERHUMAN) || (cret.Master != null))
                    {
                        if (new System.Random(10).Next() < 2)
                        {
                            if (new System.Random(2 + cret.AntiPoison).Next() == 0)
                            {
                                cret.MakePoison(Grobal2.POISON_STONE, 5, 5);
                            }
                        }
                        else
                        {
                            if (new System.Random(2 + cret.AntiPoison).Next() == 0)
                            {
                                int sec = 60;
                                pwr = 70;
                                int skilllevel = 3;
                                this.MagMakeCurseArea(targ.CX, targ.CY, 2, sec, pwr, skilllevel, false);
                            }
                        }
                        int dam = cret.GetMagStruckDamage(this, pwr);
                        if (dam > 0)
                        {
                            cret.SendDelayMsg(this, Grobal2.RM_MAGSTRUCK, 0, dam, 0, 0, "", 1500);
                        }
                        dam = cret.GetMagStruckDamage(this, pwr);
                        if (dam > 0)
                        {
                            cret.SendDelayMsg(this, Grobal2.RM_MAGSTRUCK, 0, dam, 0, 0, "", 2000);
                        }
                    }
                }
            }
        }

        public override void Attack(TCreature target, byte dir)
        {
            if (target == null)
            {
                return;
            }
            int wide = 3;
            this.Dir = M2Share.GetNextDirection(this.CX, this.CY, target.CX, target.CY);
            this.SendRefMsg(Grobal2.RM_LIGHTING, this.Dir, this.CX, this.CY, target.ActorId, "");
            TAbility _wvar1 = this.WAbil;
            int pwr = this.GetAttackPower(HUtil32.LoByte(_wvar1.DC), HUtil32._MIN(255, HiByte(_wvar1.DC) - HUtil32.LoByte(_wvar1.DC)));
            pwr = pwr + new System.Random(HUtil32.LoByte(_wvar1.MC)).Next();
            pwr = pwr * 2;
            if (pwr <= 0)
            {
                return;
            }
            ArrayList rlist = new ArrayList();
            this.GetMapCreatures(this.PEnvir, this.CX, this.CY, wide, rlist);
            for (var i = 0; i < rlist.Count; i++)
            {
                TCreature cret = (TCreature)rlist[i];
                if (this.IsProperTarget(cret))
                {
                    this.SelectTarget(cret);
                    int dam = cret.GetMagStruckDamage(this, pwr);
                    if (dam > 0)
                    {
                        cret.SendDelayMsg(this, Grobal2.RM_MAGSTRUCK, 0, dam, 0, 0, "", 300);
                    }
                    dam = cret.GetMagStruckDamage(this, pwr);
                    if (dam > 0)
                    {
                        cret.SendDelayMsg(this, Grobal2.RM_MAGSTRUCK, 0, dam, 0, 0, "", 600);
                    }
                    dam = cret.GetMagStruckDamage(this, pwr);
                    if (dam > 0)
                    {
                        cret.SendDelayMsg(this, Grobal2.RM_MAGSTRUCK, 0, dam, 0, 0, "", 900);
                    }
                }
            }
            rlist.Free();
        }

        public override void Die()
        {
            ArrayList list = new ArrayList();
            M2Share.UserEngine.GetMapMons(this.PEnvir, list);
            for (var k = 0; k < list.Count; k++)
            {
                ((TCreature)list[k]).NeverDie = false;
                ((TCreature)list[k]).WAbil.HP = 0;
            }
            list.Free();
            base.Die();
        }
    }
}