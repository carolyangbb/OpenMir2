using System.Collections;
using SystemModule;

namespace GameSvr
{
    public class TBeeQueen : TAnimal
    {
        // childcount: integer;
        private readonly ArrayList childlist = null;
        // --------------------------------------------------------------
        // 国烹
        //Constructor  Create()
        public TBeeQueen() : base()
        {
            this.ViewRange = 9;
            this.RunNextTick = 250;
            this.SearchRate = 2500 + ((long)new System.Random(1500).Next());
            this.SearchTime  =  HUtil32.GetTickCount();
            this.StickMode = true;
            childlist = new ArrayList();
        }

        protected void MakeChildBee()
        {
            if (childlist.Count < 15)
            {
                this.SendRefMsg(Grobal2.RM_HIT, this.Dir, this.CX, this.CY, 0, "");
                this.SendDelayMsg(this, Grobal2.RM_ZEN_BEE, 0, 0, 0, 0, "", 500);
            }
        }

        public override void RunMsg(TMessageInfo msg)
        {
            string monname;
            TCreature mon;
            switch (msg.Ident)
            {
                case Grobal2.RM_ZEN_BEE:
                    monname = svMain.__Bee;
                    mon = svMain.UserEngine.AddCreatureSysop(this.PEnvir.MapName, this.CX, this.CY, monname);
                    if (mon != null)
                    {
                        mon.SelectTarget(this.TargetCret);
                        childlist.Add(mon);
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
                        // 惑加罐篮 run 俊辑 HitTime 犁汲沥窃.
                        this.HitTime  =  HUtil32.GetTickCount();
                        this.MonsterNormalAttack();
                        if (this.TargetCret != null)
                        {
                            MakeChildBee();
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