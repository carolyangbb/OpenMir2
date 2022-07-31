using System;
using System.Collections;
using SystemModule;

namespace GameSvr
{
    public class TScultureMonster : TMonster
    {
        // ---------------------------------------------------------------------------
        // 籍惑阁飘舰: 堪家厘焙, 堪家措厘
        //Constructor  Create()
        public TScultureMonster() : base()
        {
            this.SearchRate = 1500 + ((long)new System.Random(1500).Next());
            this.ViewRange = 7;
            this.BoStoneMode = true;
            // 贸澜俊绰 倒肺 被绢廉 乐澜...
            this.CharStatusEx = Grobal2.STATE_STONE_MODE;
            this.BoDontMove = true;
            this.MeltArea = 2;
        }
        public void MeltStone()
        {
            this.CharStatusEx = 0;
            this.CharStatus = this.GetCharStatus();
            this.SendRefMsg(Grobal2.RM_DIGUP, this.Dir, this.CX, this.CY, 0, "");
            // 踌绰 局聪皋捞记
            this.BoStoneMode = false;
            this.BoDontMove = false;
        }

        public void MeltStoneAll()
        {
            int i;
            TCreature cret;
            ArrayList rlist;
            MeltStone();
            rlist = new ArrayList();
            this.GetMapCreatures(this.PEnvir, this.CX, this.CY, 7, rlist);
            for (i = 0; i < rlist.Count; i++)
            {
                cret = (TCreature)rlist[i];
                if (cret.BoStoneMode)
                {
                    if (cret is TScultureMonster)
                    {
                        (cret as TScultureMonster).MeltStone();
                    }
                }
            }
            rlist.Free();
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
                    if (this.BoStoneMode)
                    {
                        // 酒流 葛嚼阑 唱鸥郴瘤 臼疽澜.
                        for (i = 0; i < this.VisibleActors.Count; i++)
                        {
                            cret = (TCreature)this.VisibleActors[i].cret;
                            if ((!cret.Death) && this.IsProperTarget(cret) && (!cret.BoHumHideMode || this.BoViewFixedHide))
                            {
                                if ((Math.Abs(this.CX - cret.CX) <= this.MeltArea) && (Math.Abs(this.CY - cret.CY) <= this.MeltArea))
                                {
                                    MeltStoneAll();
                                    // 籍惑惑怕俊辑 踌绰促, 林狼悼丰甸档 窃膊 踌绰促.
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
                            this.SearchEnemyTime = HUtil32.GetTickCount();
                            this.MonsterNormalAttack();
                        }
                    }
                }
            }
            base.Run();
        }

    }
}