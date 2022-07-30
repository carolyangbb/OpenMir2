using System;
using System.Collections;
using SystemModule;

namespace GameSvr
{
    public class TScultureKingMonster : TMonster
    {
        private int DangerLevel = 0;
        private readonly ArrayList childlist = null;
        public bool BoCallFollower = false;

        public TScultureKingMonster() : base()
        {
            this.SearchRate = 1500 + ((long)new System.Random(1500).Next());
            this.ViewRange = 8;
            this.BoStoneMode = true;
            this.CharStatusEx = Grobal2.STATE_STONE_MODE;
            this.Dir = 5;
            DangerLevel = 5;
            childlist = new ArrayList();
            BoCallFollower = true;
        }

        public void MeltStone()
        {
            TEvent __event;
            __event = new TEvent(this.PEnvir, this.CX, this.CY, Grobal2.ET_SCULPEICE, 5 * 60 * 1000, true);
            if ((__event != null) && (__event.IsAddToMap == true))
            {
                this.CharStatusEx = 0;
                this.CharStatus = this.GetCharStatus();
                this.SendRefMsg(Grobal2.RM_DIGUP, this.Dir, this.CX, this.CY, 0, "");
                this.BoStoneMode = false;
                svMain.EventMan.AddEvent(__event);
                return;
            }
            if (__event != null)
            {
                __event.Free();
            }
        }

        public virtual void CallFollower()
        {
            const int MAX_FOLLOWERS = 4;
            int i;
            int count;
            int nx = 0;
            int ny = 0;
            string monname;
            TCreature mon;
            string[] followers = new string[MAX_FOLLOWERS - 1 + 1];
            count = 6 + new System.Random(6).Next();
            M2Share.GetFrontPosition(this, ref nx, ref ny);
            followers[0] = svMain.__ZumaMonster1;
            followers[1] = svMain.__ZumaMonster2;
            followers[2] = svMain.__ZumaMonster3;
            followers[3] = svMain.__ZumaMonster4;
            for (i = 1; i <= count; i++)
            {
                if (childlist.Count < 30)
                {
                    monname = followers[new System.Random(MAX_FOLLOWERS).Next()];
                    mon = svMain.UserEngine.AddCreatureSysop(this.MapName, nx, ny, monname);
                    if (mon != null)
                    {
                        childlist.Add(mon);
                    }
                }
            }
        }

        public override void Attack(TCreature target, byte dir)
        {
            int pwr;
            TAbility _wvar1 = this.WAbil;
            pwr = this.GetAttackPower(HUtil32.LoByte(_wvar1.DC), HiByte(_wvar1.DC) - HUtil32.LoByte(_wvar1.DC));
            // inherited
            this.HitHit2(target, 0, pwr, true);
        }

        public override void Run()
        {
            int i;
            TCreature cret;
            if (this.IsMoveAble())
            {
                if (GetCurrentTime - this.WalkTime > this.GetNextWalkTime())
                {
                    if (this.BoStoneMode)
                    {
                        for (i = 0; i < this.VisibleActors.Count; i++)
                        {
                            cret = (TCreature)this.VisibleActors[i].cret;
                            if ((!cret.Death) && this.IsProperTarget(cret) && (!cret.BoHumHideMode || this.BoViewFixedHide))
                            {
                                if ((Math.Abs(this.CX - cret.CX) <= 2) && (Math.Abs(this.CY - cret.CY) <= 2))
                                {
                                    MeltStone();
                                    this.WalkTime = GetCurrentTime + 2000;
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
                        if (BoCallFollower)
                        {
                            if (((this.WAbil.HP / this.WAbil.MaxHP * 5) < DangerLevel) && (DangerLevel > 0))
                            {
                                DangerLevel -= 1;
                                CallFollower();
                            }
                            if (this.WAbil.HP == this.WAbil.MaxHP)
                            {
                                DangerLevel = 5;
                            }
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