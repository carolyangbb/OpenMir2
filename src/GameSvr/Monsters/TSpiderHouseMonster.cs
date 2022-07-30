using System.Collections;
using SystemModule;

namespace GameSvr
{
    public class TSpiderHouseMonster : TAnimal
    {
        private readonly ArrayList childlist = null;

        public TSpiderHouseMonster() : base()
        {
            this.ViewRange = 9;
            this.RunNextTick = 250;
            this.SearchRate = 2500 + ((long)new System.Random(1500).Next());
            this.SearchTime  =  HUtil32.GetTickCount();
            this.StickMode = true;
            childlist = new ArrayList();
        }

        protected void MakeChildSpider()
        {
            if (childlist.Count < 15)
            {
                this.SendRefMsg(Grobal2.RM_HIT, this.Dir, this.CX, this.CY, 0, "");
                this.SendDelayMsg(this, Grobal2.RM_ZEN_BEE, 0, 0, 0, 0, "", 500);
            }
        }

        public override void RunMsg(TMessageInfo msg)
        {
            int nx=0;
            int ny=0;
            string monname;
            TCreature mon;
            switch (msg.Ident)
            {
                case Grobal2.RM_ZEN_BEE:
                    monname = svMain.__Spider;
                    // 气林
                    // 芭固狼 规氢俊 蝶扼辑 货尝 芭固狼 困摹啊 炼沥
                    nx = this.CX;
                    ny = this.CY + 1;
                    if (this.PEnvir.CanWalk(nx, ny, true))
                    {
                        mon = svMain.UserEngine.AddCreatureSysop(this.PEnvir.MapName, nx, ny, monname);
                        if (mon != null)
                        {
                            mon.SelectTarget(this.TargetCret);
                            childlist.Add(mon);
                        }
                    }
                    break;
            }
            base.RunMsg(msg);
        }

        public override void Run()
        {
            int i;
            if (this.IsMoveAble())
            {
                if (GetCurrentTime - this.WalkTime > this.GetNextWalkTime())
                {
                    this.WalkTime = GetCurrentTime;
                    if (GetCurrentTime - this.HitTime > this.GetNextHitTime())
                    {
                        this.HitTime  =  HUtil32.GetTickCount();
                        this.MonsterNormalAttack();
                        if (this.TargetCret != null)
                        {
                            MakeChildSpider();
                        }
                    }
                    for (i = childlist.Count - 1; i >= 0; i--)
                    {
                        if (((TCreature)childlist[i]).Death || ((TCreature)childlist[i]).BoGhost)
                        {
                            childlist.RemoveAt(i);
                        }
                    }
                }
            }
            base.Run();
        }

    }
}