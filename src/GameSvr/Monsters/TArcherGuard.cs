using System;
using SystemModule;

namespace GameSvr
{
    public class TArcherGuard : TGuardUnit
    {
        // --------------------------------------------------------------
        // TArcherGuard
        //Constructor  Create()
        public TArcherGuard() : base()
        {
            this.ViewRange = 12;
            this.WantRefMsg = true;
            this.Castle = null;
            this.OriginDir = -1;
            this.RaceServer = Grobal2.RC_ARCHERGUARD;
        }
        // 馆靛矫 target <> nil
        private void ShotArrow(TCreature targ)
        {
            int dam;
            if (targ == null)
            {
                return;
            }
            this.Dir = M2Share.GetNextDirection(this.CX, this.CY, targ.CX, targ.CY);
            TAbility _wvar1 = this.WAbil;
            dam = _wvar1.Lobyte(_wvar1.DC) + new System.Random((short)HiByte(_wvar1.DC) - _wvar1.Lobyte(_wvar1.DC) + 1).Next();
            if (dam > 0)
            {
                // armor := (Lobyte(targ.WAbil.AC) + Random(ShortInt(HiByte(targ.WAbil.AC)-Lobyte(targ.WAbil.AC)) + 1));
                // dam := dam - armor;
                // if dam <= 0 then
                // if dam > -10 then dam := 1;
                dam = targ.GetHitStruckDamage(this, dam);
            }
            if (dam > 0)
            {
                targ.SetLastHiter(this);
                targ.ExpHiter = null;
                // 版氰摹甫
                targ.StruckDamage(dam, this);
                // wparam
                // lparam1
                // lparam2
                // hiter
                targ.SendDelayMsg((TCreature)Grobal2.RM_STRUCK, Grobal2.RM_REFMESSAGE, dam, targ.WAbil.HP, targ.WAbil.MaxHP, this.ActorId, "", 600 + _MAX(Math.Abs(this.CX - targ.CX), Math.Abs(this.CY - targ.CY)) * 50);
            }
            this.SendRefMsg(Grobal2.RM_FLYAXE, this.Dir, this.CX, this.CY, targ.ActorId, "");
        }

        public override void Run()
        {
            int i;
            int d;
            int dis;
            TCreature cret;
            TCreature nearcret;
            dis = 9999;
            nearcret = null;
            // if not Death and not BoGhost and
            // (StatusArr[POISON_STONE] = 0) and (StatusArr[POISON_ICE] = 0) and
            // (StatusArr[POISON_STUN] = 0) then begin
            if (this.IsMoveAble())
            {
                if (GetCurrentTime - this.WalkTime > this.GetNextWalkTime())
                {
                    this.WalkTime = GetCurrentTime;
                    for (i = 0; i < this.VisibleActors.Count; i++)
                    {
                        cret = (TCreature)this.VisibleActors[i].cret;
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
                    else
                    {
                        this.LoseTarget();
                    }
                }
                if (this.TargetCret != null)
                {
                    if (GetCurrentTime - this.HitTime > this.GetNextHitTime())
                    {
                        this.HitTime = GetCurrentTime;
                        ShotArrow(this.TargetCret);
                    }
                }
                else
                {
                    if (this.OriginDir >= 0)
                    {
                        if (this.OriginDir != this.Dir)
                        {
                            this.Turn((byte)this.OriginDir);
                        }
                    }
                }
            }
            base.Run();
        }

    }
}