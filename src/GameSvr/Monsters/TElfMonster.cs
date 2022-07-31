using SystemModule;

namespace GameSvr
{
    public class TElfMonster : TMonster
    {
        private bool bofirst = false;

        public TElfMonster() : base()
        {
            this.ViewRange = 6;
            this.HideMode = true;
            this.NoAttackMode = true;
            bofirst = true;
        }

        public override void RecalcAbilitys()
        {
            base.RecalcAbilitys();
            ResetElfMon();
        }

        public void ResetElfMon()
        {
            this.NextWalkTime = 500 - (this.SlaveMakeLevel * 50);
            this.WalkTime = GetCurrentTime + 2000;
        }

        public void AppearNow()
        {
            bofirst = false;
            this.HideMode = false;
            RecalcAbilitys();
            this.WalkTime = this.WalkTime + 800;
        }

        public override void Run()
        {
            if (bofirst)
            {
                bofirst = false;
                this.HideMode = false;
                this.SendRefMsg(Grobal2.RM_DIGUP, this.Dir, this.CX, this.CY, 0, "");
                ResetElfMon();
            }
            if (this.Death)
            {
                if (HUtil32.GetTickCount() - this.DeathTime > 2 * 1000)
                {
                    this.MakeGhost(1);
                }
            }
            else
            {
                bool bochangeface = false;
                if (this.TargetCret != null)
                {
                    bochangeface = true;
                }
                if (this.Master != null)
                {
                    if ((this.Master.TargetCret != null) || (this.Master.LastHiter != null))
                    {
                        bochangeface = true;
                    }
                }
                if (bochangeface)
                {
                    TCreature cret = this.MakeClone(M2Share.__ShinSu1, this);
                    if (cret != null)
                    {
                        if (cret is TElfWarriorMonster)
                        {
                            (cret as TElfWarriorMonster).AppearNow();
                        }
                        this.Master = null;
                        this.KickException();
                    }
                }
            }
            base.Run();
        }
    }
}