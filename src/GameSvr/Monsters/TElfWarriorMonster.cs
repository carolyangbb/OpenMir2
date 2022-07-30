using SystemModule;

namespace GameSvr
{
    public class TElfWarriorMonster : TSpitSpider
    {
        private bool bofirst = false;
        private long changefacetime = 0;

        public TElfWarriorMonster() : base()
        {
            this.ViewRange = 6;
            this.HideMode = true;
            bofirst = true;
            this.BoUsePoison = false;
        }

        public override void RecalcAbilitys()
        {
            base.RecalcAbilitys();
        }

        public void ResetElfMon()
        {
            this.NextHitTime = 1500 - (this.SlaveMakeLevel * 100);
            this.NextWalkTime = 500 - (this.SlaveMakeLevel * 50);
            this.WalkTime = GetCurrentTime + 2000;
        }

        public void AppearNow()
        {
            bofirst = false;
            this.HideMode = false;
            this.SendRefMsg(Grobal2.RM_DIGUP, this.Dir, this.CX, this.CY, 0, "");
            RecalcAbilitys();
            ResetElfMon();
            this.WalkTime = this.WalkTime + 800;
            changefacetime  =  HUtil32.GetTickCount();
        }

        public override void Run()
        {
            TCreature cret;
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
                    this.MakeGhost(2);
                }
            }
            else
            {
                bool bochangeface = true;
                if (this.TargetCret != null)
                {
                    bochangeface = false;
                }
                if (this.Master != null)
                {
                    if ((this.Master.TargetCret != null) || (this.Master.LastHiter != null))
                    {
                        bochangeface = false;
                    }
                }
                if (bochangeface)
                {
                    if (HUtil32.GetTickCount() - changefacetime > 60 * 1000)
                    {
                        cret = this.MakeClone(svMain.__ShinSu, this);
                        if (cret != null)
                        {
                            this.SendRefMsg(Grobal2.RM_DIGDOWN, 0, this.CX, this.CY, 0, "");
                            this.SendRefMsg(Grobal2.RM_CHANGEFACE, 0, this.ActorId, (int)cret, 0, "");
                            if (cret is TElfMonster)
                            {
                                (cret as TElfMonster).AppearNow();
                            }
                            this.Master = null;
                            this.KickException();
                        }
                    }
                }
                else
                {
                    changefacetime  =  HUtil32.GetTickCount();
                }
            }
            base.Run();
        }
    }
}