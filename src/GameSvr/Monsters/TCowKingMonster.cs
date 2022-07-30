using SystemModule;

namespace GameSvr
{
    public class TCowKingMonster : TATMonster
    {
        private long JumpTime = 0;
        // 鉴埃捞悼阑 茄促.
        private bool CrazyReadyMode = false;
        private bool CrazyKingMode = false;
        private int CrazyCount = 0;
        private long crazyready = 0;
        private long crazytime = 0;
        private int oldhittime = 0;
        private int oldwalktime = 0;
        // ---------------------------------------------------------------------------
        // TCowKingMonster    快搁蓖空
        //Constructor  Create()
        public TCowKingMonster() : base()
        {
            this.SearchRate = 500 + ((long)new System.Random(1500).Next());
            JumpTime  =  HUtil32.GetTickCount();
            this.RushMode = true;
            // 付过俊 嘎酒档 倒柳茄促.
            CrazyCount = 0;
            CrazyReadyMode = false;
            CrazyKingMode = false;
        }
        public override void Attack(TCreature target, byte dir)
        {
            int pwr;
            TAbility _wvar1 = this.WAbil;
            pwr = this.GetAttackPower(_wvar1.Lobyte(_wvar1.DC), (short)HiByte(_wvar1.DC) - _wvar1.Lobyte(_wvar1.DC));
            // inherited
            this.HitHit2(target, pwr / 2, pwr / 2, true);
        }

        public override void Initialize()
        {
            oldhittime = this.NextHitTime;
            oldwalktime = this.NextWalkTime;
            base.Initialize();
        }

        public override void Run()
        {
            short nx = 0;
            short ny = 0;
            int old;
            if (!this.Death && !this.RunDone && !this.BoGhost)
            {
                if (HUtil32.GetTickCount() - JumpTime > 30 * 1000)
                {
                    JumpTime  =  HUtil32.GetTickCount();
                    if ((this.TargetCret != null) && (this.SiegeLockCount() >= 5))
                    {
                        M2Share.GetBackPosition(this.TargetCret, ref nx, ref ny);
                        if (this.PEnvir.CanWalk(nx, ny, false))
                        {
                            this.SpaceMove(this.PEnvir.MapName, nx, ny, 0);
                        }
                        else
                        {
                            this.RandomSpaceMove(this.PEnvir.MapName, 0);
                        }
                        return;
                    }
                }
                old = CrazyCount;
                CrazyCount = 7 - this.WAbil.HP / (this.WAbil.MaxHP / 7);
                if ((CrazyCount >= 2) && (CrazyCount != old))
                {
                    CrazyReadyMode = true;
                    crazyready  =  HUtil32.GetTickCount();
                }
                if (CrazyReadyMode)
                {
                    if (HUtil32.GetTickCount() - crazyready < 8 * 1000)
                    {
                        this.NextHitTime = 10000;
                    }
                    else
                    {
                        CrazyReadyMode = false;
                        CrazyKingMode = true;
                        crazytime  =  HUtil32.GetTickCount();
                    }
                }
                if (CrazyKingMode)
                {
                    if (HUtil32.GetTickCount() - crazytime < 8 * 1000)
                    {
                        this.NextHitTime = 500;
                        this.NextWalkTime = 400;
                    }
                    else
                    {
                        CrazyKingMode = false;
                        this.NextHitTime = oldhittime;
                        this.NextWalkTime = oldwalktime;
                    }
                }
            }
            base.Run();
        }
    }
}