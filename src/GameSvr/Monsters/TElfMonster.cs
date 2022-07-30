using SystemModule;

namespace GameSvr
{
    public class TElfMonster : TMonster
    {
        private bool bofirst = false;
        // ---------------------------------------------------------------------------
        // 脚荐 (函脚 傈)
        //Constructor  Create()
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
            // NextHitTime := 3000 - (SlaveMakeLevel * 600);  //傍拜 救窃
            this.NextWalkTime = 500 - (this.SlaveMakeLevel * 50);
            this.WalkTime = GetCurrentTime + 2000;
        }

        public void AppearNow()
        {
            bofirst = false;
            this.HideMode = false;
            // SendRefMsg (RM_TURN, Dir, CX, CY, 0, '');
            // Appear;
            // ResetElfMon;
            RecalcAbilitys();
            this.WalkTime = this.WalkTime + 800;
            // 函脚饶 距埃 掉饭捞 乐澜

        }

        public override void Run()
        {
            TCreature cret;
            bool bochangeface;
            if (bofirst)
            {
                bofirst = false;
                this.HideMode = false;
                this.SendRefMsg(Grobal2.RM_DIGUP, this.Dir, this.CX, this.CY, 0, "");
                ResetElfMon();
            }
            if (this.Death)
            {
                // 脚荐绰 矫眉啊 绝促.
                if (GetTickCount - this.DeathTime > 2 * 1000)
                {
                    this.MakeGhost(1);
                }
            }
            else
            {
                bochangeface = false;
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
                    // 傍拜 措惑捞 乐绰 版快->函脚
                    cret = this.MakeClone(svMain.__ShinSu1, this);
                    if (cret != null)
                    {
                        // SendRefMsg (RM_CHANGEFACE, 0, integer(self), integer(cret), 0, '');
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