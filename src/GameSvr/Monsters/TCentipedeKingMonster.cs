using System;
using SystemModule;

namespace GameSvr
{
    public class TCentipedeKingMonster : TStickMonster
    {
        private long appeartime = 0;

        public TCentipedeKingMonster() : base()
        {
            this.ViewRange = 6;
            this.DigupRange = 4;
            this.DigdownRange = 6;
            this.BoAnimal = false;
            appeartime = HUtil32.GetTickCount();
        }
        protected bool FindTarget()
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
                        result = true;
                        break;
                    }
                }
            }
            return result;
        }

        protected override bool AttackTarget()
        {
            bool result;
            int i;
            int pwr;
            TCreature cret;
            result = false;
            if (FindTarget())
            {
                if (GetCurrentTime - this.HitTime > this.GetNextHitTime())
                {
                    this.HitTime = GetCurrentTime;
                    this.HitMotion(Grobal2.RM_HIT, this.Dir, this.CX, this.CY);
                    TAbility _wvar1 = this.WAbil;
                    pwr = HUtil32._MAX(0, HUtil32.LoByte(_wvar1.DC) + new System.Random(HiByte(_wvar1.DC) - HUtil32.LoByte(_wvar1.DC) + 1).Next());
                    for (i = 0; i < this.VisibleActors.Count; i++)
                    {
                        cret = (TCreature)this.VisibleActors[i].cret;
                        if ((!cret.Death) && this.IsProperTarget(cret))
                        {
                            if ((Math.Abs(this.CX - cret.CX) <= this.ViewRange) && (Math.Abs(this.CY - cret.CY) <= this.ViewRange))
                            {
                                this.TargetFocusTime = HUtil32.GetTickCount();
                                this.SendDelayMsg(this, Grobal2.RM_DELAYMAGIC, (short)pwr, HUtil32.MakeLong(cret.CX, cret.CY), 2, cret.ActorId, "", 600);
                                if (new System.Random(4).Next() == 0)
                                {
                                    if (new System.Random(3).Next() != 0)
                                    {
                                        cret.MakePoison(Grobal2.POISON_DECHEALTH, 60, 3);
                                    }
                                    else
                                    {
                                        cret.MakePoison(Grobal2.POISON_STONE, 5, 0);
                                    }
                                }
                                this.TargetCret = cret;
                            }
                        }
                    }
                }
                result = true;
            }
            return result;
        }

        protected override void ComeOut()
        {
            base.ComeOut();
            this.WAbil.HP = this.WAbil.MaxHP;
        }

        public override void Run()
        {
            int i;
            TCreature cret;
            if (this.IsMoveAble())
            {
                if (GetCurrentTime - this.WalkTime > this.NextWalkTime)
                {
                    this.WalkTime = GetCurrentTime;
                    if (this.HideMode)
                    {
                        // 酒流 葛嚼阑 唱鸥郴瘤 臼疽澜.
                        if (HUtil32.GetTickCount() - appeartime > 10 * 1000)
                        {
                            for (i = 0; i < this.VisibleActors.Count; i++)
                            {
                                cret = (TCreature)this.VisibleActors[i].cret;
                                if ((!cret.Death) && this.IsProperTarget(cret) && (!cret.BoHumHideMode || this.BoViewFixedHide))
                                {
                                    if ((Math.Abs(this.CX - cret.CX) <= this.DigupRange) && (Math.Abs(this.CY - cret.CY) <= this.DigupRange))
                                    {
                                        ComeOut();
                                        // 观栏肺 唱坷促. 焊牢促.
                                        appeartime = HUtil32.GetTickCount();
                                        break;
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        if (HUtil32.GetTickCount() - appeartime > 3 * 1000)
                        {
                            if (AttackTarget())
                            {
                                base.Run();
                                return;
                            }
                            else
                            {
                                // 利捞 绝澜
                                if (HUtil32.GetTickCount() - appeartime > 10 * 1000)
                                {
                                    this.ComeDown();
                                    appeartime = HUtil32.GetTickCount();
                                }
                            }
                        }
                    }
                }
            }
            base.Run();
        }

    }
}