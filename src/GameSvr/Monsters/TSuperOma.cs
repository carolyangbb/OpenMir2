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

        public TSuperOma() : base()
        {
            RecentAttackTime = (int)GetTickCount;
            TeleInterval = 10;
            criticalpoint = 0;
            TargetTime = HUtil32.GetTickCount();
            OldTargetCret = null;
        }

        protected override bool AttackTarget()
        {
            byte targdir = 0;
            short nx = 0;
            short ny = 0;
            bool result = false;
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
                        this.TargetFocusTime = HUtil32.GetTickCount();
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
                            M2Share.GetFrontPosition(this.TargetCret, ref nx, ref ny);
                            this.SpaceMove(this.PEnvir.MapName, nx, ny, 0);
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
                    }
                }
            }
            return result;
        }

        public override void Attack(TCreature target, byte dir)
        {
            TAbility _wvar1 = this.WAbil;
            int pwr = this.GetAttackPower(HUtil32.LoByte(_wvar1.DC), HiByte(_wvar1.DC) - HUtil32.LoByte(_wvar1.DC));
            criticalpoint++;
            if ((criticalpoint > 3) || (new System.Random(20).Next() == 0))
            {
                criticalpoint = 0;
                pwr = HUtil32.MathRound(pwr * 3);
                this.HitHitEx2(target, Grobal2.RM_LIGHTING, 0, pwr, true);
            }
            else
            {
                this.HitHit2(target, 0, pwr, true);
            }
        }
    }
}