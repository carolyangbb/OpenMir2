namespace GameSvr
{
    public class TATMonster : TMonster
    {
        // ------------------- TATMonster -------------------
        //Constructor  Create()
        public TATMonster() : base()
        {
            this.SearchRate = 1500 + ((long)new System.Random(1500).Next());
        }
        //@ Destructor  Destroy()
        ~TATMonster()
        {
            base.Destroy();
        }
        public override void Run()
        {
            // 啊厘 啊鳖款 仇何磐 傍拜茄促.
            // if not Death and not RunDone and not BoGhost and
            // (StatusArr[POISON_STONE] = 0) and (StatusArr[POISON_ICE] = 0) and
            // (StatusArr[POISON_STUN] = 0) then begin
            if (!this.RunDone && this.IsMoveAble())
            {
                if ((GetTickCount - this.SearchEnemyTime > 8000) || ((GetTickCount - this.SearchEnemyTime > 1000) && (this.TargetCret == null)))
                {
                    this.SearchEnemyTime = GetTickCount;
                    this.MonsterNormalAttack();
                }
            }
            base.Run();
        }

    } // end TATMonster
}