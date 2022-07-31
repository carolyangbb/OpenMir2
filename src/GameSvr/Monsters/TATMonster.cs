using SystemModule;

namespace GameSvr
{
    public class TATMonster : TMonster
    {
        public TATMonster() : base()
        {
            this.SearchRate = 1500 + ((long)new System.Random(1500).Next());
        }

        public override void Run()
        {
            if (!this.RunDone && this.IsMoveAble())
            {
                if ((HUtil32.GetTickCount() - this.SearchEnemyTime > 8000) || ((HUtil32.GetTickCount() - this.SearchEnemyTime > 1000) && (this.TargetCret == null)))
                {
                    this.SearchEnemyTime = HUtil32.GetTickCount();
                    this.MonsterNormalAttack();
                }
            }
            base.Run();
        }
    }
}