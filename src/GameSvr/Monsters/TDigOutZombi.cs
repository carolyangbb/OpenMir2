using System;
using SystemModule;

namespace GameSvr
{
    public class TDigOutZombi : TMonster
    {
        public TDigOutZombi() : base()
        {
            this.RunDone = false;
            this.ViewRange = 7;
            this.SearchRate = 2500 + ((long)new System.Random(1500).Next());
            this.SearchTime  =  HUtil32.GetTickCount();
            this.RaceServer = Grobal2.RC_DIGOUTZOMBI;
            this.HideMode = true;
        }

        protected void ComeOut()
        {
            TEvent __event = new TEvent(this.PEnvir, this.CX, this.CY, Grobal2.ET_DIGOUTZOMBI, 5 * 60 * 1000, true);
            if ((__event != null) && (__event.IsAddToMap == true))
            {
                svMain.EventMan.AddEvent(__event);
                this.HideMode = false;
                this.SendRefMsg(Grobal2.RM_DIGUP, this.Dir, this.CX, this.CY, __event.EventId, "");
                return;
            }
            if (__event != null)
            {
                __event.Free();
            }
        }

        public override void Run()
        {
            TCreature cret;
            if (this.IsMoveAble())
            {
                if (GetCurrentTime - this.WalkTime > this.GetNextWalkTime())
                {
                    if (this.HideMode)
                    {
                        for (var i = 0; i < this.VisibleActors.Count; i++)
                        {
                            cret = (TCreature)this.VisibleActors[i].cret;
                            if ((!cret.Death) && this.IsProperTarget(cret) && (!cret.BoHumHideMode || this.BoViewFixedHide))
                            {
                                if ((Math.Abs(this.CX - cret.CX) <= 3) && (Math.Abs(this.CY - cret.CY) <= 3))
                                {
                                    ComeOut();
                                    this.WalkTime = GetCurrentTime + 1000;
                                    break;
                                }
                            }
                        }
                    }
                    else
                    {
                        if ((HUtil32.GetTickCount() - this.SearchEnemyTime > 8000) || ((HUtil32.GetTickCount() - this.SearchEnemyTime > 1000) && (this.TargetCret == null)))
                        {
                            this.SearchEnemyTime  =  HUtil32.GetTickCount();
                            this.MonsterNormalAttack();
                        }
                    }
                }
            }
            base.Run();
        }
    }
}