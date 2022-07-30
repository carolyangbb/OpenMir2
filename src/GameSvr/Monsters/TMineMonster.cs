using System;
using SystemModule;

namespace GameSvr
{
    public class TMineMonster : TAnimal
    {
        protected bool RunDone = false;
        protected int DigupRange = 0;
        protected int DigdownRange = 0;
        // Mine Monster -----------------------------------------------------------------
        //Constructor  Create()
        public TMineMonster() : base()
        {
            RunDone = false;
            this.ViewRange = 7;
            this.RunNextTick = 250;
            this.SearchRate = 2500 + ((long)new System.Random(1500).Next());
            this.SearchTime = GetTickCount;
            this.RaceServer = Grobal2.RC_MINE;
            DigupRange = 4;
            DigdownRange = 4;
            this.HideMode = true;
            this.StickMode = true;
            this.BoAnimal = false;
            // 戒搁 侥牢檬蕾, 侥牢檬凯概啊 唱咳.

        }
        //@ Destructor  Destroy()
        ~TMineMonster()
        {
            base.Destroy();
        }
        protected bool AttackTarget()
        {
            bool result;
            this.WAbil.HP = 0;
            result = true;
            return result;
        }

        protected void ComeOut()
        {
            this.HideMode = false;
            this.SendRefMsg(Grobal2.RM_DIGUP, this.Dir, this.CX, this.CY, 0, "");
        }

        protected void CheckComeOut()
        {
            int i;
            TCreature cret;
            for (i = 0; i < this.VisibleActors.Count; i++)
            {
                cret = (TCreature)this.VisibleActors[i].cret;
                if ((!cret.Death) && this.IsProperTarget(cret) && (!cret.BoHumHideMode || this.BoViewFixedHide))
                {
                    if ((Math.Abs(this.CX - cret.CX) <= DigupRange) && (Math.Abs(this.CY - cret.CY) <= DigupRange))
                    {
                        ComeOut();
                        // 观栏肺 唱坷促. 焊牢促.
                        break;
                    }
                }
            }
        }

        public override void RunMsg(TMessageInfo msg)
        {
            base.RunMsg(msg);
        }

        public override void Run()
        {
            if (this.IsMoveAble())
            {
                if (GetCurrentTime - this.WalkTime > this.GetNextWalkTime())
                {
                    this.WalkTime = GetCurrentTime;
                    if (this.HideMode)
                    {
                        // 酒流 葛嚼阑 唱鸥郴瘤 臼疽澜.
                        CheckComeOut();
                    }
                    else
                    {
                        if (GetCurrentTime - this.HitTime > this.GetNextHitTime())
                        {
                            // 惑加罐篮 run 俊辑 HitTime 犁汲沥窃.
                            // /HitTime := GetTickCount; //酒贰 AttackTarget俊辑 窃.
                            if (AttackTarget())
                            {
                                base.Run();
                                return;
                            }
                        }
                    }
                }
            }
            base.Run();
        }

    }
}