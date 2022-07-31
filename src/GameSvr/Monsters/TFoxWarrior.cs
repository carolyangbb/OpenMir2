using SystemModule;

namespace GameSvr
{
    public class TFoxWarrior : TATMonster
    {
        private bool CrazyKingMode = false;
        private bool CriticalMode = false;
        private long CrazyTime = 0;
        private int oldhittime = 0;
        private int oldwalktime = 0;

        public TFoxWarrior() : base()
        {
            this.SearchRate = 2500 + ((long)new System.Random(1500).Next());
            CrazyKingMode = false;
            CriticalMode = false;
        }

        public override void Initialize()
        {
            CrazyTime = HUtil32.GetTickCount();
            oldhittime = this.NextHitTime;
            oldwalktime = this.NextWalkTime;
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
            if (CriticalMode)
            {
                pwr = pwr * 2;
                this.SendRefMsg(Grobal2.RM_LIGHTING, this.Dir, this.CX, this.CY, target.ActorId, "");
            }
            else
            {
                this.SendRefMsg(Grobal2.RM_HIT, this.Dir, this.CX, this.CY, target.ActorId, "");
            }
            for (var i = 0; i <= 4; i++)
            {
                for (var k = 0; k <= 4; k++)
                {
                    if (M2Share.SpitMap[dir, i, k] == 1)
                    {
                        int mx = this.CX - 2 + k;
                        int my = this.CY - 2 + i;
                        TCreature cret = (TCreature)this.PEnvir.GetCreature(mx, my, true);
                        if ((cret != null) && (cret != this))
                        {
                            if (this.IsProperTarget(cret))
                            {
                                if (new System.Random(cret.SpeedPoint).Next() < this.AccuracyPoint)
                                {
                                    cret.StruckDamage(pwr, this);
                                    cret.SendDelayMsg(Grobal2.RM_STRUCK, Grobal2.RM_REFMESSAGE, (short)pwr, cret.WAbil.HP, cret.WAbil.MaxHP, this.ActorId, "", 500);
                                }
                            }
                        }
                    }
                }
            }
        }

        public override void Run()
        {
            if (!this.Death && !this.BoGhost)
            {
                if (CrazyKingMode)
                {
                    if (HUtil32.GetTickCount() - CrazyTime < 60 * 1000)
                    {
                        this.NextHitTime = oldhittime * 2 / 5;
                        this.NextWalkTime = oldwalktime * 1 / 2;
                    }
                    else
                    {
                        CrazyKingMode = false;
                        this.NextHitTime = oldhittime;
                        this.NextWalkTime = oldwalktime;
                    }
                }
                else
                {
                    if (this.WAbil.HP < this.WAbil.MaxHP / 4)
                    {
                        CrazyKingMode = true;
                        CrazyTime = HUtil32.GetTickCount();
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
                if (this.TargetInSpitRange(this.TargetCret, ref targdir))
                {
                    if (GetCurrentTime - this.HitTime > this.GetNextHitTime())
                    {
                        this.HitTime = GetCurrentTime;
                        this.TargetFocusTime = HUtil32.GetTickCount();
                        if (new System.Random(100).Next() < 20)
                        {
                            CriticalMode = true;
                        }
                        else
                        {
                            CriticalMode = false;
                        }
                        Attack(this.TargetCret, targdir);
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
                }
            }
            return result;
        }
    }
}