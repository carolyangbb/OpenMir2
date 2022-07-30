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
        // ---------------------------------------------------------------------------
        // TFoxBead    厚岿玫林
        //Constructor  Create()
        public TFoxBead() : base()
        {
            RunDone = false;
            this.ViewRange = 16;
            this.RunNextTick = 250;
            this.SearchRate = 1500 + ((long)new System.Random(1500).Next());
            this.SearchTime  =  HUtil32.GetTickCount();
            this.HideMode = false;
            this.StickMode = true;
            this.BoDontMove = true;
            this.BodyState = 1;
            OrgNextHitTime = this.NextHitTime;
            sectick  =  HUtil32.GetTickCount();
        }
        public override void Run()
        {
            if (HUtil32.GetTickCount() - sectick > 3000)
            {
                sectick  =  HUtil32.GetTickCount();
                if ((!this.Death) && (!this.BoGhost))
                {
                    if (this.WAbil.HP >= this.WAbil.MaxHP * 4 / 5)
                    {
                        if (this.BodyState != 1)
                        {
                            this.BodyState = 1;
#if DEBUG
                            svMain.UserEngine.CryCry(Grobal2.RM_CRY, this.PEnvir, this.CX, this.CY, 10000, " State(" + (this.BodyState).ToString() + ") : " + this.TargetCret.UserName);
                            // test
#endif
                            this.WAbil.DC = MakeWord(_MIN(255, Lobyte(this.Abil.DC)), _MIN(255, HiByte(this.Abil.DC)));
                            this.WAbil.AC = MakeWord(_MIN(255, Lobyte(this.Abil.AC)), _MIN(255, HiByte(this.Abil.AC)));
                            this.WAbil.MAC = MakeWord(_MIN(255, Lobyte(this.Abil.MAC)), _MIN(255, HiByte(this.Abil.MAC)));
                            this.SendRefMsg(Grobal2.RM_FOXSTATE, this.Dir, this.CX, this.CY, this.BodyState, this.UserName);
                            // NextHitTime := OrgNextHitTime;
                        }
                    }
                    else if (this.WAbil.HP >= this.WAbil.MaxHP * 3 / 5)
                    {
                        if (this.BodyState != 2)
                        {
                            this.BodyState = 2;
#if DEBUG
                            svMain.UserEngine.CryCry(Grobal2.RM_CRY, this.PEnvir, this.CX, this.CY, 10000, " State(" + (this.BodyState).ToString() + ") : " + this.TargetCret.UserName);
                            // test
#endif
                            this.WAbil.DC = MakeWord(_MIN(255, Lobyte(this.Abil.DC)), _MIN(255, HiByte(this.Abil.DC) + HiByte(this.Abil.DC) / 10));
                            this.WAbil.AC = MakeWord(_MIN(255, Lobyte(this.Abil.AC)), _MIN(255, HiByte(this.Abil.AC) + HiByte(this.Abil.AC) * 2 / 10));
                            this.WAbil.MAC = MakeWord(_MIN(255, Lobyte(this.Abil.MAC)), _MIN(255, HiByte(this.Abil.MAC) + HiByte(this.Abil.MAC) * 2 / 10));
                            this.SendRefMsg(Grobal2.RM_FOXSTATE, this.Dir, this.CX, this.CY, this.BodyState, this.UserName);
                            // NextHitTime := OrgNextHitTime;
                        }
                    }
                    else if (this.WAbil.HP >= this.WAbil.MaxHP * 2 / 5)
                    {
                        if (this.BodyState != 3)
                        {
                            this.BodyState = 3;
#if DEBUG
                            svMain.UserEngine.CryCry(Grobal2.RM_CRY, this.PEnvir, this.CX, this.CY, 10000, " State(" + (this.BodyState).ToString() + ") : " + this.TargetCret.UserName);
                            // test
#endif
                            this.WAbil.DC = MakeWord(_MIN(255, Lobyte(this.Abil.DC)), _MIN(255, HiByte(this.Abil.DC) + HiByte(this.Abil.DC) * 2 / 10));
                            this.WAbil.AC = MakeWord(_MIN(255, Lobyte(this.Abil.AC)), _MIN(255, HiByte(this.Abil.AC) + HiByte(this.Abil.AC) * 4 / 10));
                            this.WAbil.MAC = MakeWord(_MIN(255, Lobyte(this.Abil.MAC)), _MIN(255, HiByte(this.Abil.MAC) + HiByte(this.Abil.MAC) * 4 / 10));
                            this.SendRefMsg(Grobal2.RM_FOXSTATE, this.Dir, this.CX, this.CY, this.BodyState, this.UserName);
                            // NextHitTime := OrgNextHitTime * 9 div 10;
                        }
                    }
                    else if (this.WAbil.HP >= this.WAbil.MaxHP * 1 / 5)
                    {
                        if (this.BodyState != 4)
                        {
                            this.BodyState = 4;
#if DEBUG
                            svMain.UserEngine.CryCry(Grobal2.RM_CRY, this.PEnvir, this.CX, this.CY, 10000, " State(" + (this.BodyState).ToString() + ") : " + this.TargetCret.UserName);
                            // test
#endif
                            this.WAbil.DC = MakeWord(_MIN(255, Lobyte(this.Abil.DC)), _MIN(255, HiByte(this.Abil.DC) + HiByte(this.Abil.DC) * 3 / 10));
                            this.WAbil.AC = MakeWord(_MIN(255, Lobyte(this.Abil.AC)), _MIN(255, HiByte(this.Abil.AC) + HiByte(this.Abil.AC) * 6 / 10));
                            this.WAbil.MAC = MakeWord(_MIN(255, Lobyte(this.Abil.MAC)), _MIN(255, HiByte(this.Abil.MAC) + HiByte(this.Abil.MAC) * 4 / 10));
                            this.SendRefMsg(Grobal2.RM_FOXSTATE, this.Dir, this.CX, this.CY, this.BodyState, this.UserName);
                            // NextHitTime := OrgNextHitTime * 8 div 10;
                        }
                    }
                    else
                    {
                        if (this.BodyState != 5)
                        {
                            this.BodyState = 5;
#if DEBUG
                            svMain.UserEngine.CryCry(Grobal2.RM_CRY, this.PEnvir, this.CX, this.CY, 10000, " State(" + (this.BodyState).ToString() + ") : " + this.TargetCret.UserName);
                            // test
#endif
                            this.WAbil.DC = MakeWord(_MIN(255, Lobyte(this.Abil.DC)), _MIN(255, HiByte(this.Abil.DC) + HiByte(this.Abil.DC) * 4 / 10));
                            this.WAbil.AC = MakeWord(_MIN(255, Lobyte(this.Abil.AC)), _MIN(255, HiByte(this.Abil.AC) + HiByte(this.Abil.AC) * 8 / 10));
                            this.WAbil.MAC = MakeWord(_MIN(255, Lobyte(this.Abil.MAC)), _MIN(255, HiByte(this.Abil.MAC) + HiByte(this.Abil.MAC) * 4 / 10));
                            this.SendRefMsg(Grobal2.RM_FOXSTATE, this.Dir, this.CX, this.CY, this.BodyState, this.UserName);
                            // NextHitTime := OrgNextHitTime * 7 div 10;
                        }
                    }
                }
            }
            // 扁粮 角青阑 茄促.
            base.Run();
        }

        protected override bool AttackTarget()
        {
            bool result;
            int i;
            int nx=0;
            int ny=0;
            TCreature cret;
            ArrayList rlist;
            result = false;
            rlist = null;
            cret = null;
            if (this.TargetCret != null)
            {
                if (GetCurrentTime - this.HitTime > this.GetNextHitTime())
                {
                    this.HitTime = GetCurrentTime;
                    if ((Math.Abs(this.CX - this.TargetCret.CX) <= this.ViewRange) && (Math.Abs(this.CY - this.TargetCret.CY) <= this.ViewRange))
                    {
                        // 家券(10%)
                        if (new System.Random(10).Next() == 0)
                        {
                            // 家券 厚岿玫林 Effect
                            this.SendRefMsg(Grobal2.RM_NORMALEFFECT, 0, this.CX, this.CY, Grobal2.NE_KINGSTONE_RECALL_1, "");
                            rlist = new ArrayList();
                            this.GetMapCreatures(this.PEnvir, this.CX, this.CY, 30, rlist);
                            for (i = 0; i < rlist.Count; i++)
                            {
                                cret = (TCreature)rlist[i];
                                if ((!cret.Death) && this.IsProperTarget(cret))
                                {
                                    // 老沥 裹困 观俊 乐绰 荤恩父
                                    if ((cret.RaceServer == Grobal2.RC_USERHUMAN) && ((Math.Abs(this.CX - cret.CX) > 3) || (Math.Abs(this.CY - cret.CY) > 3)))
                                    {
                                        // 家券茄促.
                                        if (new System.Random(3).Next() < 2)
                                        {
                                            // 家券 某腐 Effect
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
                                            // 家券 某腐 Effect
                                            cret.SendRefMsg(Grobal2.RM_NORMALEFFECT, 0, cret.CX, cret.CY, Grobal2.NE_KINGSTONE_RECALL_2, "");
                                        }
                                    }
                                }
                            }
                            rlist.Free();
#if DEBUG
                            svMain.UserEngine.CryCry(Grobal2.RM_CRY, this.PEnvir, this.CX, this.CY, 10000, " 家券 : " + this.TargetCret.UserName);
                            // test
#endif
                            result = true;
                        }
                        else if (new System.Random(100).Next() < 40)
                        {
                            // 檬鞘混 傍拜(35%)
#if DEBUG
                            svMain.UserEngine.CryCry(Grobal2.RM_CRY, this.PEnvir, this.CX, this.CY, 10000, " 檬鞘混 : " + this.TargetCret.UserName);
                            // test
#endif
                            RangeAttack2(this.TargetCret);
                            result = true;
                        }
                        else if (new System.Random(10).Next() < 4)
                        {
                            // 吝缴傍拜(%)
#if DEBUG
                            svMain.UserEngine.CryCry(Grobal2.RM_CRY, this.PEnvir, this.CX, this.CY, 10000, " 吝缴傍拜 : " + this.TargetCret.UserName);
                            // test
#endif
                            Attack(this.TargetCret, this.Dir);
                            result = true;
                        }
                        else
                        {
                            // 盔芭府傍拜(%)
#if DEBUG
                            svMain.UserEngine.CryCry(Grobal2.RM_CRY, this.PEnvir, this.CX, this.CY, 10000, " 盔芭府裹困 : " + this.TargetCret.UserName);
                            // test
#endif
                            RangeAttack(this.TargetCret);
                            result = true;
                        }
                        // 促弗 鸥百 拱祸
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
                            // <!!林狼> TargetCret := nil肺 官柴
                        }
                    }
                }
            }
            return result;
        }

        public bool FindTarget()
        {
            bool result;
            int i;
            TCreature cret;
            result = false;
            for (i = 0; i < this.VisibleActors.Count; i++)
            {
                cret = (TCreature)this.VisibleActors[i].cret;
                if ((!cret.Death) && this.IsProperTarget(cret))
                {
                    if ((Math.Abs(this.CX - cret.CX) <= this.ViewRange) && (Math.Abs(this.CY - cret.CY) <= this.ViewRange))
                    {
                        if (cret.RaceServer == Grobal2.RC_USERHUMAN)
                        {
                            if (this.TargetCret == null)
                            {
                                // 鸥百 瘤沥
                                this.TargetCret = cret;
                            }
                            else
                            {
                                // 绊沥 鸥百 规瘤.
                                if (new System.Random(100).Next() < 50)
                                {
                                    continue;
                                }
                                // 鸥百 瘤沥
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

        // 5x5 盔芭府 裹困 傍拜
        public void RangeAttack(TCreature targ)
        {
            // 馆靛矫 target <> nil
            int i;
            int pwr;
            int dam;
           short sx=0;
           short sy=0;
           short tx=0;
            short ty =0;
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
                pwr = _wvar1._MAX(0, _wvar1.Lobyte(_wvar1.DC) + _wvar1._MIN(255, new System.Random((short)HiByte(_wvar1.DC) - _wvar1.Lobyte(_wvar1.DC) + 1).Next()));
                pwr = pwr + new System.Random(_wvar1.Lobyte(_wvar1.MC)).Next();
                pwr = pwr * 2;
                list = new ArrayList();
                this.PEnvir.GetCreatureInRange(targ.CX, targ.CY, 2, true, list);
                for (i = 0; i < list.Count; i++)
                {
                    cret = (TCreature)list[i];
                    if (this.IsProperTarget(cret))
                    {
                        dam = cret.GetMagStruckDamage(this, pwr);
                        if (dam > 0)
                        {
                            cret.StruckDamage(dam, this);
                            // wparam
                            // lparam1
                            // lparam2
                            // hiter
                            cret.SendDelayMsg((TCreature)Grobal2.RM_STRUCK, Grobal2.RM_REFMESSAGE, dam, cret.WAbil.HP, cret.WAbil.MaxHP, this.ActorId, "", 800);
                        }
                    }
                }
                list.Free();
            }
        }

        // 檬鞘混 傍拜
        public void RangeAttack2(TCreature targ)
        {
            // 馆靛矫 target <> nil
            int i;
            int pwr;
            int dam;
            TCreature cret;
            int sec;
            int skilllevel;
            if (targ == null)
            {
                return;
            }
            // 檬鞘混 Effect
            this.SendRefMsg(Grobal2.RM_LIGHTING_2, this.Dir, this.CX, this.CY, this.ActorId, "");
            TAbility _wvar1 = this.WAbil;
            pwr = this.GetAttackPower(_wvar1.Lobyte(_wvar1.DC), _wvar1._MIN(255, (short)HiByte(_wvar1.DC) - _wvar1.Lobyte(_wvar1.DC)));
            pwr = pwr * 2;
            // 矫具郴 葛电 某腐/家券各 吝刀
            for (i = 0; i < this.VisibleActors.Count; i++)
            {
                cret = (TCreature)this.VisibleActors[i].cret;
                if ((!cret.Death) && this.IsProperTarget(cret))
                {
                    if ((cret.RaceServer == Grobal2.RC_USERHUMAN) || (cret.Master != null))
                    {
                        // 历林贱 肚绰 付厚啊 等促.
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
                                sec = 60;
                                pwr = 70;
                                skilllevel = 3;
                                this.MagMakeCurseArea(targ.CX, targ.CY, 2, sec, pwr, skilllevel, false);
                            }
                        }
                        dam = cret.GetMagStruckDamage(this, pwr);
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

        // 吝缴 裹困 傍拜
        public override void Attack(TCreature target, byte dir)
        {
            int i;
            int dam;
            int wide;
            ArrayList rlist;
            TCreature cret;
            int pwr;
            if (target == null)
            {
                return;
            }
            wide = 3;
            this.Dir = M2Share.GetNextDirection(this.CX, this.CY, target.CX, target.CY);
            this.SendRefMsg(Grobal2.RM_LIGHTING, this.Dir, this.CX, this.CY, target.ActorId, "");
            TAbility _wvar1 = this.WAbil;
            pwr = this.GetAttackPower(_wvar1.Lobyte(_wvar1.DC), _wvar1._MIN(255, (short)HiByte(_wvar1.DC) - _wvar1.Lobyte(_wvar1.DC)));
            pwr = pwr + new System.Random(_wvar1.Lobyte(_wvar1.MC)).Next();
            pwr = pwr * 2;
            if (pwr <= 0)
            {
                return;
            }
            rlist = new ArrayList();
            this.GetMapCreatures(this.PEnvir, this.CX, this.CY, wide, rlist);
            for (i = 0; i < rlist.Count; i++)
            {
                cret = (TCreature)rlist[i];
                if (this.IsProperTarget(cret))
                {
                    this.SelectTarget(cret);
                    // 3锅 楷加 鸥拜
                    dam = cret.GetMagStruckDamage(this, pwr);
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
            int k;
            ArrayList list;
            list = new ArrayList();
            svMain.UserEngine.GetMapMons(this.PEnvir, list);
            for (k = 0; k < list.Count; k++)
            {
                ((TCreature)list[k]).NeverDie = false;
                // TCreature(list[k]).BoNoItem := TRUE;
                ((TCreature)list[k]).WAbil.HP = 0;
                // 葛滴 磷牢促.
            }
            list.Free();
            base.Die();
        }

    }
}