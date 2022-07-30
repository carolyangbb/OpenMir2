using System;
using SystemModule;

namespace GameSvr
{
    public class TSkeletonKingMonster : TScultureKingMonster
    {
        public bool RunDone = false;
        public int ChainShot = 0;
        public int ChainShotCount = 0;

        public TSkeletonKingMonster() : base()
        {
            ChainShotCount = 6;
            this.BoStoneMode = false;
            this.CharStatusEx = 0;
            this.CharStatus = this.GetCharStatus();
        }

        public override void CallFollower()
        {
            const int MAX_SKELFOLLOWERS = 3;
            int i;
            int count;
            int nx = 0;
            int ny = 0;
            string monname;
            TCreature mon;
            string[] followers = new string[MAX_SKELFOLLOWERS - 1 + 1];
            this.SendRefMsg(Grobal2.RM_LIGHTING, this.Dir, this.CX, this.CY, 0, "");
            count = 4 + new System.Random(4).Next();
            M2Share.GetFrontPosition(this, ref nx, ref ny);
            followers[0] = "BoneCaptain";
            followers[1] = "BoneArcher";
            followers[2] = "BoneSpearman";
            for (i = 1; i <= count; i++)
            {
                if (this.childlist.Count < 20)
                {
                    monname = followers[new System.Random(MAX_SKELFOLLOWERS).Next()];
                    mon = svMain.UserEngine.AddCreatureSysop(this.MapName, nx, ny, monname);
                    if (mon != null)
                    {
                        this.childlist.Add(mon);
                    }
                }
            }
        }

        public override void Attack(TCreature target, byte dir)
        {
            int pwr;
            TAbility _wvar1 = this.WAbil;
            pwr = this.GetAttackPower(_wvar1.Lobyte(_wvar1.DC), (short)HiByte(_wvar1.DC) - _wvar1.Lobyte(_wvar1.DC));
            // inherited
            this.HitHit2(target, 0, pwr, true);
        }

        public override void Run()
        {
            base.Run();
        }

        public virtual void RangeAttack(TCreature targ)
        {
            int dam;
            if (targ == null)
            {
                return;
            }
            if (this.PEnvir.CanFly(this.CX, this.CY, targ.CX, targ.CY))
            {
                this.Dir = M2Share.GetNextDirection(this.CX, this.CY, targ.CX, targ.CY);
                TAbility _wvar1 = this.WAbil;
                dam = _wvar1._MAX(0, _wvar1.Lobyte(_wvar1.DC) + new System.Random((short)HiByte(_wvar1.DC) - _wvar1.Lobyte(_wvar1.DC) + 1).Next());
                if (dam > 0)
                {
                    dam = targ.GetHitStruckDamage(this, dam);
                }
                if (dam > 0)
                {
                    targ.StruckDamage(dam, this);
                    targ.SendDelayMsg((TCreature)Grobal2.RM_STRUCK, Grobal2.RM_REFMESSAGE, dam, targ.WAbil.HP, targ.WAbil.MaxHP, this.ActorId, "", 600 + _MAX(Math.Abs(this.CX - targ.CX), Math.Abs(this.CY - targ.CY)) * 50);
                }
                this.SendRefMsg(Grobal2.RM_FLYAXE, this.Dir, this.CX, this.CY, targ.ActorId, "");
            }
        }

        protected override bool AttackTarget()
        {
            byte targdir=0;
            bool result = false;
            if (this.TargetCret != null)
            {
                if (GetCurrentTime - this.HitTime > this.GetNextHitTime())
                {
                    this.HitTime = GetCurrentTime;
                    if ((Math.Abs(this.CX - this.TargetCret.CX) <= 7) && (Math.Abs(this.CY - this.TargetCret.CY) <= 7))
                    {
                        if (this.TargetInAttackRange(this.TargetCret, ref targdir))
                        {
                            this.TargetFocusTime  =  HUtil32.GetTickCount();
                            Attack(this.TargetCret, targdir);
                            result = true;
                        }
                        else
                        {
                            if (ChainShot < ChainShotCount - 1)
                            {
                                ChainShot++;
                                this.TargetFocusTime  =  HUtil32.GetTickCount();
                                RangeAttack(this.TargetCret);
                            }
                            else
                            {
                                if (new System.Random(5).Next() == 0)
                                {
                                    ChainShot = 0;
                                }
                            }
                            result = true;
                        }
                    }
                    else
                    {
                        if (this.TargetCret.MapName == this.MapName)
                        {
                            if ((Math.Abs(this.CX - this.TargetCret.CX) <= 11) && (Math.Abs(this.CY - this.TargetCret.CY) <= 11))
                            {
                                this.SetTargetXY(this.TargetCret.CX, this.TargetCret.CY);
                            }
                        }
                        else
                        {
                            this.LoseTarget();
                        }
                    }
                }
            }
            return result;
        }
    }
}