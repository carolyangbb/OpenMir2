using SystemModule;

namespace GameSvr
{
    public class TGasMothMonster : TGasAttackMonster
    {

        public TGasMothMonster() : base()
        {
            this.ViewRange = 7;
        }

        public override TCreature GasAttack(byte dir)
        {
            TCreature result;
            TCreature cret = base.GasAttack(dir);
            if (cret != null)
            {
                if (new System.Random(3).Next() == 0)
                {
                    if (cret.BoHumHideMode)
                    {
                        cret.StatusArr[Grobal2.STATE_TRANSPARENT] = 1;
                    }
                }
            }
            result = cret;
            return result;
        }

        public override void Run()
        {
            if (!this.RunDone && this.IsMoveAble())
            {
                if ((GetTickCount - this.SearchEnemyTime > 8000) || ((GetTickCount - this.SearchEnemyTime > 1000) && (this.TargetCret == null)))
                {
                    this.SearchEnemyTime = GetTickCount;
                    this.MonsterDetecterAttack();
                }
            }
            base.Run();
        }

    }
}