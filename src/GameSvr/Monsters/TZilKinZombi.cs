using SystemModule;

namespace GameSvr
{
    public class TZilKinZombi : TATMonster
    {
        private long deathstart = 0;
        private int LifeCount = 0;
        // 巢篮 犁积
        private long RelifeTime = 0;
        // ---------------------------------------------------------------------------
        // 磷菌促 柄绢唱绰 粱厚
        //Constructor  Create()
        public TZilKinZombi() : base()
        {
            this.ViewRange = 6;
            this.SearchRate = 2500 + ((long)new System.Random(1500).Next());
            this.SearchTime = GetTickCount;
            this.RaceServer = Grobal2.RC_ZILKINZOMBI;
            LifeCount = 0;
            if (new System.Random(3).Next() == 0)
            {
                LifeCount = 1 + new System.Random(3).Next();
            }
        }
        public override void Die()
        {
            base.Die();
            if (LifeCount > 0)
            {
                deathstart = GetTickCount;
                RelifeTime = (4 + new System.Random(20).Next()) * 1000;
            }
            LifeCount -= 1;
        }

        public override void Run()
        {
            // 漂喊茄 版快扼 IsMoveAble 轿侩且荐 绝澜
            if (this.Death && (!this.BoGhost) && (LifeCount >= 0) && (this.StatusArr[Grobal2.POISON_STONE] == 0) && (this.StatusArr[Grobal2.POISON_ICE] == 0) && (this.StatusArr[Grobal2.POISON_STUN] == 0) && (this.StatusArr[Grobal2.POISON_DONTMOVE] == 0))
            {
                // 磷菌澜, 绊胶飘惑怕绰 酒丛
                if (this.VisibleActors.Count > 0)
                {
                    if (GetTickCount - deathstart >= RelifeTime)
                    {
                        this.Abil.MaxHP = (ushort)(this.Abil.MaxHP / 2);
                        this.FightExp = this.FightExp / 2;
                        this.Abil.HP = this.Abil.MaxHP;
                        this.WAbil.HP = this.Abil.MaxHP;
                        this.Alive();
                        this.WalkTime = GetCurrentTime + 1000;
                    }
                }
            }
            base.Run();
        }

    }
}