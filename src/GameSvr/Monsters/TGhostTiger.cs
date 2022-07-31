using System;
using System.Collections;
using SystemModule;

namespace GameSvr
{
    public class TGhostTiger : TMonster
    {
        private long LastHideTime = 0;
        private long LastSitDownTime = 0;
        private bool fSitDown = false;
        private bool fHide = false;
        private bool fEnableSitDown = false;

        public TGhostTiger() : base()
        {
            this.SearchRate = 1500 + ((long)new System.Random(1500).Next());
            LastHideTime = GetTickCount + 10000;
            LastSitDownTime = GetTickCount + 10000;
            fSitDown = false;
            fHide = false;
            fEnableSitDown = false;
            this.ViewRange = 11;
        }

        protected void RangeAttack(TCreature targ)
        {
            TCreature cret;
            this.Dir = M2Share.GetNextDirection(this.CX, this.CY, targ.CX, targ.CY);
            this.SendRefMsg(Grobal2.RM_LIGHTING, this.Dir, this.CX, this.CY, targ.ActorId, "");
            TAbility _wvar1 = this.WAbil;
            int pwr = HUtil32._MAX(0, HUtil32.LoByte(_wvar1.DC) + new System.Random(HiByte(_wvar1.DC) - HUtil32.LoByte(_wvar1.DC) + 1).Next());
            ArrayList list = new ArrayList();
            this.GetMapCreatures(this.PEnvir, targ.CX, targ.CY, 1, list);
            for (var i = 0; i < list.Count; i++)
            {
                cret = (TCreature)list[i];
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
                            int slowtime = dam / 10;
                            if (slowtime > 0)
                            {
                                cret.MakePoison(Grobal2.POISON_SLOW, slowtime, 1);
                            }
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
                            if (fSitDown == false)
                            {
                                LastSitDownTime = GetTickCount + 10000;
                            }
                            result = true;
                        }
                        else
                        {
                            if (new System.Random(3).Next() == 0)
                            {
                                RangeAttack(this.TargetCret);
                                if (fSitDown == false)
                                {
                                    LastSitDownTime = GetTickCount + 10000;
                                }
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

        public override void Run()
        {
            if (HUtil32.GetTickCount() >= LastHideTime)
            {
                if (fHide)
                {
                    this.StatusArr[Grobal2.STATE_TRANSPARENT] = 0;
                    LastHideTime = GetTickCount + new System.Random(3000).Next() + 9000;
                    fHide = false;
                }
                else
                {
                    if ((!this.BoGhost) && (!this.Death))
                    {
                        this.StatusArr[Grobal2.STATE_TRANSPARENT] = 60000;
                        LastHideTime = GetTickCount + new System.Random(3000).Next() + 9000;
                        fHide = true;
                    }
                }
                this.CharStatus = this.GetCharStatus();
                this.CharStatusChanged();
            }
            fEnableSitDown = false;
            if (this.Master != null)
            {
                if (this.Master.BoSlaveRelax)
                {
                    fEnableSitDown = true;
                }
                if (!this.RunDone && this.IsMoveAble())
                {
                    if ((HUtil32.GetTickCount() - this.SearchEnemyTime > 8000) || ((HUtil32.GetTickCount() - this.SearchEnemyTime > 1000) && (this.TargetCret == null)))
                    {
                        this.SearchEnemyTime = HUtil32.GetTickCount();
                        this.MonsterNormalAttack();
                    }
                }
            }
            else
            {
                if (this.TargetX == -1)
                {
                    fEnableSitDown = true;
                }
            }
            if (this.BoGhost || this.Death)
            {
                fEnableSitDown = false;
            }
            if ((fEnableSitDown || fSitDown) && (LastSitDownTime < GetTickCount))
            {
                if (fSitDown)
                {
                    this.SendRefMsg(Grobal2.RM_TURN, this.Dir, this.CX, this.CY, 0, "");
                    LastSitDownTime = GetTickCount + new System.Random(5000).Next() + 15000;
                    fSitDown = false;
                    this.BoDontMove = false;
                }
                else
                {
                    this.SendRefMsg(Grobal2.RM_DIGDOWN, this.Dir, this.CX, this.CY, 0, "");
                    LastSitDownTime = GetTickCount + new System.Random(3000).Next() + 9000;
                    fSitDown = true;
                    this.BoDontMove = true;
                }
            }
            base.Run();
        }
    }
}