using SystemModule;

namespace GameSvr
{
    public class TSkeletonSoldier : TATMonster
    {
        // 2003/02/11 秦榜捍荤
        //Constructor  Create()
        public TSkeletonSoldier() : base()
        {
        }
        public void RangeAttack(byte dir)
        {
            int i;
            int k;
            int mx;
            int my;
            int dam;
            TCreature cret;
            int pwr;
            this.Dir = dir;
            TAbility _wvar1 = this.WAbil;
            dam = HUtil32.LoByte(_wvar1.DC) + new System.Random(HiByte(_wvar1.DC) - HUtil32.LoByte(_wvar1.DC) + 1).Next();
            if (dam <= 0)
            {
                return;
            }
            this.SendRefMsg(Grobal2.RM_HIT, this.Dir, this.CX, this.CY, 0, "");
            TAbility _wvar2 = this.WAbil;
            pwr = this.GetAttackPower(HUtil32.LoByte(_wvar2.DC), HiByte(_wvar2.DC) - HUtil32.LoByte(_wvar2.DC));
            for (i = 0; i <= 4; i++)
            {
                for (k = 0; k <= 4; k++)
                {
                    if (M2Share.SpitMap[dir, i, k] == 1)
                    {
                        mx = this.CX - 2 + k;
                        my = this.CY - 2 + i;
                        cret = (TCreature)this.PEnvir.GetCreature(mx, my, true);
                        if ((cret != null) && (cret != this))
                        {
                            if (this.IsProperTarget(cret))
                            {
                                if (new System.Random(cret.SpeedPoint).Next() < this.AccuracyPoint)
                                {
                                    this.HitHit2(cret, 0, pwr, true);
                                }
                            }
                        }
                    }
                }
            }
        }

        protected override bool AttackTarget()
        {
            bool result;
            byte targdir=0;
            result = false;
            if (this.TargetCret != null)
            {
                if (this.TargetInSpitRange(this.TargetCret, ref targdir))
                {
                    if (GetCurrentTime - this.HitTime > this.GetNextHitTime())
                    {
                        this.HitTime = GetCurrentTime;
                        this.TargetFocusTime  =  HUtil32.GetTickCount();
                        RangeAttack(targdir);
                        this.BreakHolySeize();
                    }
                    result = true;
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
            return result;
        }

    }
}