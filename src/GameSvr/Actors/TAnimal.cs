using System;
using SystemModule;

namespace GameSvr
{
    public class TAnimal : TCreature
    {
        public short TargetX = 0;
        public short TargetY = 0;
        public bool BoRunAwayMode = false;
        public long RunAwayStart = 0;
        public int RunAwayTime = 0;

        public TAnimal() : base()
        {
            TargetX = -1;
            this.FindPathRate = 1000 + new System.Random(4).Next() * 500;
            this.FindpathTime  =  HUtil32.GetTickCount();
            this.RaceServer = Grobal2.RC_ANIMAL;
            this.HitTime = GetCurrentTime - new System.Random(3000).Next();
            this.WalkTime = GetCurrentTime - new System.Random(3000).Next();
            this.SearchEnemyTime  =  HUtil32.GetTickCount();
            BoRunAwayMode = false;
            RunAwayStart  =  HUtil32.GetTickCount();
            RunAwayTime = 0;
        }

        public override void RunMsg(TMessageInfo msg)
        {
            switch (msg.Ident)
            {
                case Grobal2.RM_STRUCK:
                    if ((msg.sender == this) && (msg.lParam3 != 0))
                    {
                        this.SetLastHiter(msg.lParam3 as TCreature);
                        Struck(msg.lParam3 as TCreature);
                        this.BreakHolySeize();
                        if ((this.Master != null) && ((msg.lParam3 as TCreature) != this.Master))
                        {
                            if ((msg.lParam3 as TCreature).RaceServer == Grobal2.RC_USERHUMAN)
                            {
                                this.Master.AddPkHiter(msg.lParam3 as TCreature);
                            }
                        }
                    }
                    break;
                default:
                    base.RunMsg(msg);
                    break;
            }
        }

        public override void Run()
        {
            base.Run();
        }

        public virtual void Attack(TCreature target, byte dir)
        {
            this.HitHit(target, Grobal2.HM_HIT, dir);
        }

        public virtual void Struck(TCreature hiter)
        {
            byte targdir=0;
            this.StruckTime  =  HUtil32.GetTickCount();
            if (hiter != null)
            {
                if ((this.TargetCret == null) || (!this.TargetInAttackRange(this.TargetCret, ref targdir)) || (new System.Random(6).Next() == 0))
                {
                    if (this.IsProperTarget(hiter))
                    {
                        this.SelectTarget(hiter);
                    }
                }
            }
            if (this.BoAnimal)
            {
                this.MeatQuality = this.MeatQuality - new System.Random(300).Next();
                if (this.MeatQuality < 0)
                {
                    this.MeatQuality = 0;
                }
            }
            if (this.Abil.Level < M2Share.MAXKINGLEVEL - 1)
            {
                this.HitTime = this.HitTime + (150 - _MIN(130, this.Abil.Level * 4));
            }
        }

        public override void LoseTarget()
        {
            base.LoseTarget();
            TargetX = -1;
            TargetY = -1;
        }

        public TCreature GetNearMonster()
        {
            TCreature result;
            int i;
            int d;
            int dis;
            TCreature cret;
            TCreature nearcret;
            result = null;
            nearcret = null;
            dis = 999;
            for (i = 0; i < this.VisibleActors.Count; i++)
            {
                cret = this.VisibleActors[i].cret as TCreature;
                if ((!cret.Death) && this.IsProperTarget(cret) && (!cret.BoHumHideMode || this.BoViewFixedHide))
                {
                    d = Math.Abs(this.CX - cret.CX) + Math.Abs(this.CY - cret.CY);
                    if (d < dis)
                    {
                        dis = d;
                        nearcret = cret;
                    }
                }
            }
            result = nearcret;
            return result;
        }

        public void MonsterNormalAttack()
        {
            int i;
            int d;
            int dis;
            TCreature cret;
            TCreature nearcret;
            nearcret = null;
            dis = 999;
            for (i = 0; i < this.VisibleActors.Count; i++)
            {
                cret = this.VisibleActors[i].cret as TCreature;
                if ((!cret.Death) && this.IsProperTarget(cret) && (!cret.BoHumHideMode || this.BoViewFixedHide))
                {
                    d = Math.Abs(this.CX - cret.CX) + Math.Abs(this.CY - cret.CY);
                    if (d < dis)
                    {
                        dis = d;
                        nearcret = cret;
                    }
                }
            }
            if (nearcret != null)
            {
                this.SelectTarget(nearcret);
            }
        }

        public void MonsterDetecterAttack()
        {
            int i;
            int d;
            int dis;
            TCreature cret;
            TCreature nearcret;
            nearcret = null;
            dis = 999;
            for (i = 0; i < this.VisibleActors.Count; i++)
            {
                cret = this.VisibleActors[i].cret as TCreature;
                if ((!cret.Death) && this.IsProperTarget(cret))
                {
                    d = Math.Abs(this.CX - cret.CX) + Math.Abs(this.CY - cret.CY);
                    if (d < dis)
                    {
                        dis = d;
                        nearcret = cret;
                    }
                }
            }
            if (nearcret != null)
            {
                this.SelectTarget(nearcret);
            }
        }

        public void SetTargetXY(int x, int y)
        {
            TargetX = (short)x;
            TargetY = (short)y;
        }

        public void GotoTargetXY()
        {
            int wantdir;
            int i;
            int targx;
            int targy;
            int oldx;
            int oldy;
            int rand;
            if (this.BoDontMove)
            {
                return;
            }
            if ((this.CX != TargetX) || (this.CY != TargetY))
            {
                targx = TargetX;
                targy = TargetY;
                this.FindpathTime = GetCurrentTime;
                wantdir = Grobal2.DR_DOWN;
                while (true)
                {
                    if (targx > this.CX)
                    {
                        wantdir = Grobal2.DR_RIGHT;
                        if (targy > this.CY)
                        {
                            wantdir = Grobal2.DR_DOWNRIGHT;
                        }
                        if (targy < this.CY)
                        {
                            wantdir = Grobal2.DR_UPRIGHT;
                        }
                        break;
                    }
                    if (targx < this.CX)
                    {
                        wantdir = Grobal2.DR_LEFT;
                        if (targy > this.CY)
                        {
                            wantdir = Grobal2.DR_DOWNLEFT;
                        }
                        if (targy < this.CY)
                        {
                            wantdir = Grobal2.DR_UPLEFT;
                        }
                        break;
                    }
                    if (targy > this.CY)
                    {
                        wantdir = Grobal2.DR_DOWN;
                        break;
                    }
                    if (targy < this.CY)
                    {
                        wantdir = Grobal2.DR_UP;
                        break;
                    }
                    break;
                }
                oldx = this.CX;
                oldy = this.CY;
                this.WalkTo((byte)wantdir, false);
                rand = new System.Random(3).Next();
                for (i = 1; i <= 7; i++)
                {
                    if ((oldx == this.CX) && (oldy == this.CY))
                    {
                        // 菊捞 阜囚 乐澜
                        if (rand != 0)
                        {
                            wantdir++;
                        }
                        else if (wantdir > 0)
                        {
                            wantdir -= 1;
                        }
                        else
                        {
                            wantdir = 7;
                        }
                        if (wantdir > 7)
                        {
                            wantdir = 0;
                        }
                        this.WalkTo((byte)wantdir, false);
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }

        public void Wondering()
        {
            if (this.BoDontMove)
            {
                return;
            }
            if (new System.Random(20).Next() == 0)
            {
                if (new System.Random(4).Next() == 1)
                {
                    this.Turn((byte)new System.Random(8).Next());
                }
                else
                {
                    this.WalkTo(this.Dir, false);
                }
            }
        }
    }
}