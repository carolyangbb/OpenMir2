using System;
using SystemModule;

namespace GameSvr
{
    public class TSuperOma : TATMonster
    {
        public int RecentAttackTime = 0;
        public int TeleInterval = 0;
        public int criticalpoint = 0;
        public long TargetTime = 0;
        public TCreature OldTargetCret = null;
        // ==============================================================================
        // 荐欺坷付
        //Constructor  Create()
        public TSuperOma() : base()
        {
            RecentAttackTime = (int)GetTickCount;
            TeleInterval = 10;
            // sec
            criticalpoint = 0;
            TargetTime  =  HUtil32.GetTickCount();
            OldTargetCret = null;
        }
        protected override bool AttackTarget()
        {
            bool result;
            byte targdir=0;
            int nx=0;
            int ny=0;
            result = false;
            if (GetCurrentTime < ((long)new System.Random(3000).Next() + 4000 + TargetTime))
            {
                if (OldTargetCret != null)
                {
                    this.TargetCret = OldTargetCret;
                }
            }
            if (this.TargetCret != null)
            {
                OldTargetCret = this.TargetCret;
                if (this.TargetInAttackRange(this.TargetCret, ref targdir))
                {
                    if (GetCurrentTime - this.HitTime > this.GetNextHitTime())
                    {
                        this.HitTime = GetCurrentTime;
                        this.TargetFocusTime  =  HUtil32.GetTickCount();
                        RecentAttackTime = (int)GetTickCount;
                        Attack(this.TargetCret, targdir);
                        this.BreakHolySeize();
                    }
                    result = true;
                }
                else
                {
                    if (GetCurrentTime - RecentAttackTime > (TeleInterval + new System.Random(5).Next()) * 1000)
                    {
                        if (new System.Random(2).Next() == 0)
                        {
                            // 鸥百狼 菊栏肺
                            M2Share.GetFrontPosition(this.TargetCret, ref nx, ref ny);
                            // 炮饭器飘
                            this.SpaceMove(this.PEnvir.MapName, (short)nx, (short)ny, 0);
                            RecentAttackTime = (int)GetTickCount;
                        }
                    }
                    else
                    {
                        if (this.TargetCret.MapName == this.MapName)
                        {
                            this.SetTargetXY(this.TargetCret.CX, this.TargetCret.CY);
                        }
                        else
                        {
                            this.LoseTarget();
                        }
                        // <!!林狼> TargetCret := nil肺 官柴
                    }
                }
            }
            return result;
        }

        public override void Attack(TCreature target, byte dir)
        {
            int pwr;
            TAbility _wvar1 = this.WAbil;
            pwr = this.GetAttackPower(HUtil32.LoByte(_wvar1.DC), HiByte(_wvar1.DC) - HUtil32.LoByte(_wvar1.DC));
            criticalpoint++;
            // 付过 鸥拜栏肺 登绢乐澜.
            if ((criticalpoint > 3) || (new System.Random(20).Next() == 0))
            {
                criticalpoint = 0;
                pwr = HUtil32.MathRound(pwr * 3);
                // inherited
                this.HitHitEx2(target, Grobal2.RM_LIGHTING, 0, pwr, true);
            }
            else
            {
                // inherited
                this.HitHit2(target, 0, pwr, true);
            }
        }

    }
}