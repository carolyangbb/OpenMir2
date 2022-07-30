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
        // ---------------------------------------------------------------------------
        // TFoxWarrior    厚岿咯快(傈荤)
        //Constructor  Create()
        public TFoxWarrior() : base()
        {
            this.SearchRate = 2500 + ((long)new System.Random(1500).Next());
            CrazyKingMode = false;
            CriticalMode = false;
        }
        public override void Initialize()
        {
            CrazyTime  =  HUtil32.GetTickCount();
            oldhittime = this.NextHitTime;
            oldwalktime = this.NextWalkTime;
            this.ViewRange = 7;
            base.Initialize();
        }

        public override void Attack(TCreature target, byte dir)
        {
            int i;
            int k;
            int mx;
            int my;
            TCreature cret;
            int pwr;
            this.Dir = dir;
            TAbility _wvar1 = this.WAbil;
            pwr = this.GetAttackPower(_wvar1.Lobyte(_wvar1.DC), (short)HiByte(_wvar1.DC) - _wvar1.Lobyte(_wvar1.DC));
            if (pwr <= 0)
            {
                return;
            }
            if (CriticalMode)
            {
                pwr = pwr * 2;
                this.SendRefMsg(Grobal2.RM_LIGHTING, this.Dir, this.CX, this.CY, target.ActorId, "");
#if DEBUG
            // UserEngine.CryCry (RM_CRY, PEnvir, CX, CY, 10000, ' Critical Attack : ' + TargetCret.UserName);//test
#endif
            }
            else
            {
                this.SendRefMsg(Grobal2.RM_HIT, this.Dir, this.CX, this.CY, target.ActorId, "");
#if DEBUG
            // UserEngine.CryCry (RM_CRY, PEnvir, CX, CY, 10000, ' Normal Attack : ' + TargetCret.UserName);//test
#endif
            }
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
                                // 嘎绰瘤 搬沥
                                if (new System.Random(cret.SpeedPoint).Next() < this.AccuracyPoint)
                                {
                                    cret.StruckDamage(pwr, this);
                                    // wparam
                                    // lparam1
                                    // lparam2
                                    // hiter
                                    cret.SendDelayMsg((TCreature)Grobal2.RM_STRUCK, Grobal2.RM_REFMESSAGE, pwr, cret.WAbil.HP, cret.WAbil.MaxHP, this.ActorId, "", 500);
                                    // {inherited} HitHit2 (cret, pwr, 0, TRUE);
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
                    // 气林
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
                        CrazyTime  =  HUtil32.GetTickCount();
#if DEBUG
                    // UserEngine.CryCry (RM_CRY, PEnvir, CX, CY, 10000, ' CrazyKingMode : ' + TargetCret.UserName);//test
#endif
                    }
                }
            }
            base.Run();
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
                    // <!!林狼> TargetCret := nil肺 官柴
                }
            }
            return result;
        }

    }
}