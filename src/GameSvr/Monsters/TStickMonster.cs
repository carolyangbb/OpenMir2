using System;
using SystemModule;

namespace GameSvr
{
    public class TStickMonster : TAnimal
    {
        protected bool RunDone = false;
        protected int DigupRange = 0;
        protected int DigdownRange = 0;

        public TStickMonster() : base()
        {
            RunDone = false;
            this.ViewRange = 7;
            this.RunNextTick = 250;
            this.SearchRate = 2500 + ((long)new System.Random(1500).Next());
            this.SearchTime = GetTickCount;
            this.RaceServer = Grobal2.RC_KILLINGHERB;
            DigupRange = 4;
            DigdownRange = 4;
            this.HideMode = true;
            this.StickMode = true;
            this.BoAnimal = true;
        }
        
        ~TStickMonster()
        {
            base.Destroy();
        }
        
        protected virtual bool AttackTarget()
        {
            bool result;
            byte targdir;
            result = false;
            if (this.TargetCret != null)
            {
                if (this.TargetInAttackRange(this.TargetCret, ref targdir))
                {
                    if (GetCurrentTime - this.HitTime > this.GetNextHitTime())
                    {
                        this.HitTime = GetCurrentTime;
                        this.TargetFocusTime = GetTickCount;
                        this.Attack(this.TargetCret, targdir);
                    }
                    result = true;
                }
                else
                {
                    if (this.TargetCret.MapName == this.MapName)
                    {
                        this.SetTargetXY(this.TargetCret.CX, this.TargetCret.CY);
                    }
                    else
                    {
                        this.LoseTarget();
                    }
                    // <!!林狼> TargetCret := nil肺 官柴
                }
            }
            return result;
        }

        protected virtual void ComeOut()
        {
            this.HideMode = false;
            this.SendRefMsg(Grobal2.RM_DIGUP, this.Dir, this.CX, this.CY, 0, "");
        }

        protected virtual void ComeDown()
        {
            int i;
            // Dir
            this.SendRefMsg(Grobal2.RM_DIGDOWN, 0, this.CX, this.CY, 0, "");
            try
            {
                for (i = 0; i < this.VisibleActors.Count; i++)
                {
                    Dispose(this.VisibleActors[i]);
                }
                this.VisibleActors.Clear();
            }
            catch
            {
                svMain.MainOutMessage("[Exception] TStickMonster VisbleActors Dispose(..)");
            }
            this.HideMode = true;
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
            bool boidle;
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
                            this.MonsterNormalAttack();
                        }
                        boidle = false;
                        if (this.TargetCret != null)
                        {
                            if ((Math.Abs(this.TargetCret.CX - this.CX) > DigdownRange) || (Math.Abs(this.TargetCret.CY - this.CY) > DigdownRange))
                            {
                                boidle = true;
                            }
                        }
                        else
                        {
                            boidle = true;
                        }
                        if (boidle)
                        {
                            // 促矫 甸绢埃促.
                            ComeDown();
                        }
                        else if (AttackTarget())
                        {
                            base.Run();
                            return;
                        }
                    }
                }
            }
            base.Run();
        }

    } // end TStickMonster

    // end TBeeQueen

    // end TCentipedeKingMonster

    // end TBigHeartMonster

    // end TBamTreeMonster

    // end TSpiderHouseMonster

    // end TExplosionSpider

    // 版厚, 己巩, 泵荐
    // end TGuardUnit

    // end TArcherGuard

    // end TArcherMaster

    // end TArcherPolice

    // end TCastleDoor

    // end TWallStructure

    // end TSoccerBall

    // end TMineMonster

    // 龋去籍
    // end TStickBlockMonster

    // end TDoorState

}

