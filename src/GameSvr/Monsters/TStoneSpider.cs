using System;
using SystemModule;

namespace GameSvr
{
    public class TStoneSpider : TATMonster
    {

        public TStoneSpider() : base()
        {
            this.SearchRate = 1500 + ((long)new System.Random(1500).Next());
            this.ViewRange = 11;
        }

        protected void RangeAttack(TCreature targ)
        {
            int i;
            int pwr;
            int dam;
            int sx;
            int sy;
            int tx;
            int ty;
            TCreature cret;
            int ndir;
            // 汾牢厘 静磊
            this.Dir = M2Share.GetNextDirection(this.CX, this.CY, targ.CX, targ.CY);
            this.SendRefMsg(Grobal2.RM_LIGHTING, this.Dir, this.CX, this.CY, targ.ActorId, "");
            TAbility _wvar1 = this.WAbil;
            pwr = _wvar1._MAX(0, _wvar1.Lobyte(_wvar1.DC) + new System.Random((short)HiByte(_wvar1.DC) - _wvar1.Lobyte(_wvar1.DC) + 1).Next());
            ndir = this.Dir;
            M2Share.GetNextPosition(this.PEnvir, this.CX, this.CY, ndir, 1, ref sx, ref sy);
            M2Share.GetNextPosition(this.PEnvir, this.CX, this.CY, ndir, 8, ref tx, ref ty);
            for (i = 0; i <= 12; i++)
            {
                cret = (TCreature)this.PEnvir.GetCreature(sx, sy, true);
                if (cret != null)
                {
                    if (this.IsProperTarget(cret))
                    {
                        if (new System.Random(18).Next() > (cret.AntiMagic * 3))
                        {
                            // 付过 雀乔啊 乐澜
                            dam = cret.GetMagStruckDamage(this, pwr);
                            cret.SendDelayMsg(this, Grobal2.RM_MAGSTRUCK, 0, dam, 0, 0, "", 600);
                        }
                    }
                }
                if (!((Math.Abs(sx - tx) <= 0) && (Math.Abs(sy - ty) <= 0)))
                {
                    ndir = M2Share.GetNextDirection(sx, sy, tx, ty);
                    if (!M2Share.GetNextPosition(this.PEnvir, sx, sy, ndir, 1, ref sx, ref sy))
                    {
                        break;
                    }
                }
                else
                {
                    break;
                }
            }
        }

        protected override bool AttackTarget()
        {
            bool result;
            byte targdir;
            result = false;
            // 辟立秦 老阑锭俊绰 辟立 塞 傍拜阑
            // 盔芭府 老锭绰 盔芭府 付过傍拜阑 茄促.
            if (this.TargetCret != null)
            {
                if (GetCurrentTime - this.HitTime > this.GetNextHitTime())
                {
                    this.HitTime = GetCurrentTime;
                    if ((Math.Abs(this.CX - this.TargetCret.CX) <= this.ViewRange) && (Math.Abs(this.CY - this.TargetCret.CY) <= this.ViewRange))
                    {
                        if (this.TargetInAttackRange(this.TargetCret, ref targdir))
                        {
                            this.TargetFocusTime = GetTickCount;
                            this.Attack(this.TargetCret, targdir);
                            // 辟立老锭 刀 吧府霸...
                            if (new System.Random(3).Next() == 0)
                            {
                                this.TargetCret.MakePoison(Grobal2.POISON_DECHEALTH, 30, new System.Random(10).Next() + 5);
                            }
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
                            // <!!林狼> TargetCret := nil肺 官柴
                        }
                    }
                }
            }
            return result;
        }

    }
}