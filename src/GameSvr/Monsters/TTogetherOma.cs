using System.Collections;
using SystemModule;

namespace GameSvr
{
    public class TTogetherOma : TATMonster
    {
        public int RecentAttackTime = 0;
        public long TargetTime = 0;
        public TCreature OldTargetCret = null;
        public int SameRaceCount = 0;
        // ==============================================================================
        // 苟摹搁 碍秦瘤绰 坷付
        //Constructor  Create()
        public TTogetherOma() : base()
        {
            RecentAttackTime = (int)GetTickCount;
            TargetTime = HUtil32.GetTickCount();
            OldTargetCret = null;
            SameRaceCount = 0;
        }
        public override void Initialize()
        {
            base.Initialize();
            this.PlusPoisonFactor = 200;
            // 刀俊 200%狼 单固瘤甫 涝澜.

        }

        protected override bool AttackTarget()
        {
            bool result;
            byte targdir = 0;
            result = false;
            if (GetCurrentTime < ((long)new System.Random(3000).Next() + 4000 + TargetTime))
            {
                if (OldTargetCret != null)
                {
                    this.TargetCret = OldTargetCret;
                }
            }
            if (this.TargetCret != null)
            {
                OldTargetCret = this.TargetCret;
                if (this.TargetInAttackRange(this.TargetCret, ref targdir))
                {
                    if (GetCurrentTime - this.HitTime > this.GetNextHitTime())
                    {
                        this.HitTime = GetCurrentTime;
                        this.TargetFocusTime = HUtil32.GetTickCount();
                        RecentAttackTime = (int)GetTickCount;
                        Attack(this.TargetCret, targdir);
                        this.BreakHolySeize();
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

        public override void Attack(TCreature target, byte dir)
        {
            int i;
            ArrayList rlist;
            TCreature cret;
            int pwr;
            int wide;
            int CriticalFact;
            int DCFact;
            if (target == null)
            {
                return;
            }
            DCFact = 0;
            CriticalFact = 0;
            SameRaceCount = 0;
            wide = 3;
            rlist = new ArrayList();
            this.GetMapCreatures(this.PEnvir, this.CX, this.CY, wide, rlist);
            for (i = 0; i < rlist.Count; i++)
            {
                cret = (TCreature)rlist[i];
                if (!cret.BoGhost && !cret.Death)
                {
                    if (cret.RaceServer == this.RaceServer)
                    {
                        SameRaceCount++;
                    }
                }
            }
            rlist.Free();
            SameRaceCount = _MIN(30, SameRaceCount);
            DCFact = SameRaceCount * 3;
            TAbility _wvar1 = this.WAbil;
            pwr = this.GetAttackPower(HUtil32._MIN(255, HUtil32.LoByte(_wvar1.DC) + DCFact), (short)HUtil32._MIN(255, HiByte(_wvar1.DC) + DCFact) - HUtil32._MIN(255, HUtil32.LoByte(_wvar1.DC) + DCFact));
            CriticalFact = SameRaceCount;
            if (new System.Random(100).Next() < 1 + CriticalFact)
            {
                pwr = pwr + LoByte(target.WAbil.AC) + new System.Random(HiByte(target.WAbil.AC) - LoByte(target.WAbil.AC) + 1).Next();
                this.HitHitEx2(target, Grobal2.RM_LIGHTING, pwr, 0, true);
            }
            else
            {
                this.HitHit2(target, pwr, 0, true);
            }
        }

    }
}