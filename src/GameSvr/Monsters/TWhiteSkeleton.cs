using SystemModule;

namespace GameSvr
{
    public class TWhiteSkeleton : TATMonster
    {
        private bool bofirst = false;
        // ---------------------------------------------------------------------------
        // 归榜:  家券荐
        //Constructor  Create()
        public TWhiteSkeleton() : base()
        {
            bofirst = true;
            this.HideMode = true;
            this.RaceServer = Grobal2.RC_WHITESKELETON;
            this.ViewRange = 6;
        }
        public override void RecalcAbilitys()
        {
            base.RecalcAbilitys();
            // ResetSkeleton;
            // ApplySlaveLevelAbilitys;

        }

        public void ResetSkeleton()
        {
            this.NextHitTime = 3000 - (this.SlaveMakeLevel * 600);
            this.NextWalkTime = 1200 - (this.SlaveMakeLevel * 250);
            // WAbil.DC := MakeWord(LoByte(WAbil.DC), HiByte(WAbil.DC) + SlaveMakeLevel);
            // WAbil.MaxHP := WAbil.MaxHP + SlaveMakeLevel * 5;
            // WAbil.HP := WAbil.MaxHP;
            // AccuracyPoint := 11 + SlaveMakeLevel;
            this.WalkTime = GetCurrentTime + 2000;
        }

        public override void Run()
        {
            if (bofirst)
            {
                bofirst = false;
                this.Dir = 5;
                this.HideMode = false;
                this.SendRefMsg(Grobal2.RM_DIGUP, this.Dir, this.CX, this.CY, 0, "");
                ResetSkeleton();
            }
            base.Run();
        }

    }
}