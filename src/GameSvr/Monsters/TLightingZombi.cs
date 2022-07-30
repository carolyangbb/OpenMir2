using System;
using SystemModule;

namespace GameSvr
{
    public class TLightingZombi : TMonster
    {
        public TLightingZombi() : base()
        {
            this.SearchRate = 1500 + ((long)new System.Random(1500).Next());
        }

        public void LightingAttack(int dir)
        {
            int sx;
            int sy;
            int tx;
            int ty;
            int pwr;
            this.Dir = (byte)dir;
            this.SendRefMsg(Grobal2.RM_LIGHTING, 1, this.CX, this.CY, this.ActorId.TargetCret, "");
            if (M2Share.GetNextPosition(this.PEnvir, this.CX, this.CY, dir, 1, ref sx, ref sy))
            {
                M2Share.GetNextPosition(this.PEnvir, this.CX, this.CY, dir, 9, ref tx, ref ty);
                TAbility _wvar1 = this.WAbil;
                pwr = _wvar1._MAX(0, _wvar1.Lobyte(_wvar1.DC) + new System.Random((short)HiByte(_wvar1.DC) - _wvar1.Lobyte(_wvar1.DC) + 1).Next());
                this.MagPassThroughMagic(sx, sy, tx, ty, dir, pwr, true);
            }
            this.BreakHolySeize();
        }

        public override void Run()
        {
            int targdir;
            if (!this.RunDone && this.IsMoveAble())
            {
                if ((GetTickCount - this.SearchEnemyTime > 8000) || ((GetTickCount - this.SearchEnemyTime > 1000) && (this.TargetCret == null)))
                {
                    this.SearchEnemyTime = GetTickCount;
                    this.MonsterNormalAttack();
                }
                if (GetCurrentTime - this.WalkTime > this.GetNextWalkTime())
                {
                    if (this.TargetCret != null)
                    {
                        if ((Math.Abs(this.CX - this.TargetCret.CX) <= 4) && (Math.Abs(this.CY - this.TargetCret.CY) <= 4))
                        {
                            if ((Math.Abs(this.CX - this.TargetCret.CX) <= 2) && (Math.Abs(this.CY - this.TargetCret.CY) <= 2))
                            {
                                if (new System.Random(3).Next() != 0)
                                {
                                    base.Run();
                                    return;
                                }
                            }
                            M2Share.GetBackPosition(this, ref this.TargetX, ref this.TargetY);
                        }
                    }
                }
                if (this.TargetCret != null)
                {
                    if ((Math.Abs(this.CX - this.TargetCret.CX) < 6) && (Math.Abs(this.CY - this.TargetCret.CY) < 6))
                    {
                        if (GetCurrentTime - this.HitTime > this.GetNextHitTime())
                        {
                            this.HitTime = GetCurrentTime;
                            targdir = M2Share.GetNextDirection(this.CX, this.CY, this.TargetCret.CX, this.TargetCret.CY);
                            LightingAttack(targdir);
                        }
                    }
                }
            }
            base.Run();
        }
    }
}