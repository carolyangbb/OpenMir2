using System;
using SystemModule;

namespace GameSvr
{
    public class TDigOutZombi : TMonster
    {
        // ---------------------------------------------------------------------------
        // 顶颇绊 唱坷绰 粱厚
        //Constructor  Create()
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
            TEvent __event;
            __event = new TEvent(this.PEnvir, this.CX, this.CY, Grobal2.ET_DIGOUTZOMBI, 5 * 60 * 1000, true);
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
            int i;
            TCreature cret;
            // if (not BoGhost) and (not Death) and
            // (StatusArr[POISON_STONE] = 0) and (StatusArr[POISON_ICE] = 0) and
            // (StatusArr[POISON_STUN] = 0) then begin
            if (this.IsMoveAble())
            {
                if (GetCurrentTime - this.WalkTime > this.GetNextWalkTime())
                {
                    // WalkTime : =  HUtil32.GetTickCount();  惑加罐篮 run俊辑 犁汲沥窃
                    if (this.HideMode)
                    {
                        // 酒流 葛嚼阑 唱鸥郴瘤 臼疽澜.
                        for (i = 0; i < this.VisibleActors.Count; i++)
                        {
                            cret = (TCreature)this.VisibleActors[i].cret;
                            if ((!cret.Death) && this.IsProperTarget(cret) && (!cret.BoHumHideMode || this.BoViewFixedHide))
                            {
                                if ((Math.Abs(this.CX - cret.CX) <= 3) && (Math.Abs(this.CY - cret.CY) <= 3))
                                {
                                    ComeOut();
                                    // 观栏肺 唱坷促. 焊牢促.
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