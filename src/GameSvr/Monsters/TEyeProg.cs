using System;
using SystemModule;

namespace GameSvr
{
    public class TEyeProg : TATMonster
    {
        // ==============================================================================
        //Constructor  Create()
        public TEyeProg() : base()
        {
            this.SearchRate = 1500 + ((long)new System.Random(1500).Next());
            this.ViewRange = 11;
        }
        protected void RangeAttack(TCreature targ)
        {
            int levelgap;
            int rush;
            int rushdir;
            int rushDist;
            // 钢府乐绰 利阑 缠绢寸变促.
            this.Dir = M2Share.GetNextDirection(this.CX, this.CY, targ.CX, targ.CY);
            this.SendRefMsg(Grobal2.RM_LIGHTING, this.Dir, this.CX, this.CY, targ.ActorId, "");
            rushdir = (this.Dir + 4) % 8;
            rushDist = _MIN(Math.Abs(this.CX - targ.CX), Math.Abs(this.CY - targ.CY));
            if (this.IsProperTarget(targ))
            {
                if ((!targ.Death) && ((targ.RaceServer == Grobal2.RC_USERHUMAN) || (targ.Master != null)))
                {
                    levelgap = (targ.AntiMagic * 5) + HiByte(targ.WAbil.AC) / 2;
                    if (new System.Random(40).Next() > levelgap)
                    {
                        // 流急俊 乐绰逞父 动变促.
                        if ((this.CX == targ.CX) || (this.CY == targ.CY) || (Math.Abs(this.CX - targ.CX) == Math.Abs(this.CY - targ.CY)))
                        {
                            rush = rushDist;
                            targ.CharRushRush((byte)rushdir, rush, false);
                        }
                        targ.MakePoison(Grobal2.POISON_DECHEALTH, 30, new System.Random(10).Next() + 5);
                    }
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
                    if ((Math.Abs(this.CX - this.TargetCret.CX) <= 5) && (Math.Abs(this.CY - this.TargetCret.CY) <= 5))
                    {
                        if (this.TargetInAttackRange(this.TargetCret, ref targdir))
                        {
                            this.TargetFocusTime = GetTickCount;
                            this.Attack(this.TargetCret, targdir);
                            result = true;
                        }
                        else
                        {
                            if (new System.Random(2).Next() == 0)
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