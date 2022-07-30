using System;
using SystemModule;

namespace GameSvr
{
    public class TDualAxeMonster : TMonster
    {
        protected bool RunDone = false;
        protected int ChainShot = 0;
        protected int ChainShotCount = 0;

        public TDualAxeMonster() : base()
        {
            RunDone = false;
            this.ViewRange = 5;
            this.RunNextTick = 250;
            this.SearchRate = 3000;
            ChainShot = 0;
            ChainShotCount = 2;
            this.SearchTime  =  HUtil32.GetTickCount();
            this.RaceServer = Grobal2.RC_DUALAXESKELETON;
        }

        protected void FlyAxeAttack(TCreature targ)
        {
            // 馆靛矫 target <> nil
            int dam;
            if (targ == null)
            {
                return;
            }
            if (this.PEnvir.CanFly(this.CX, this.CY, targ.CX, targ.CY))
            {
                // 档尝啊 朝酒哎荐 乐绰瘤.
                this.Dir = M2Share.GetNextDirection(this.CX, this.CY, targ.CX, targ.CY);
                TAbility _wvar1 = this.WAbil;
                dam = HUtil32.LoByte(_wvar1.DC) + new System.Random(HiByte(_wvar1.DC) - HUtil32.LoByte(_wvar1.DC) + 1).Next();
                if (dam > 0)
                {
                    // armor := (LoByte(targ.WAbil.AC) + Random(ShortInt(HiByte(targ.WAbil.AC)-LoByte(targ.WAbil.AC)) + 1));
                    // dam := dam - armor;
                    // if dam <= 0 then
                    // if dam > -10 then dam := 1;
                    dam = targ.GetHitStruckDamage(this, dam);
                }
                if (dam > 0)
                {
                    targ.StruckDamage(dam, this);
                    // wparam
                    // lparam1
                    // lparam2
                    // hiter
                    targ.SendDelayMsg(Grobal2.RM_STRUCK, Grobal2.RM_REFMESSAGE, dam, targ.WAbil.HP, targ.WAbil.MaxHP, this.ActorId, "", 600 + _MAX(Math.Abs(this.CX - targ.CX), Math.Abs(this.CY - targ.CY)) * 50);
                }
                this.SendRefMsg(Grobal2.RM_FLYAXE, this.Dir, this.CX, this.CY, targ.ActorId, "");
            }
        }

        protected override bool AttackTarget()
        {
            bool result;
            result = false;
            if (this.TargetCret != null)
            {
                if (GetCurrentTime - this.HitTime > this.GetNextHitTime())
                {
                    this.HitTime = GetCurrentTime;
                    if ((Math.Abs(this.CX - this.TargetCret.CX) <= 7) && (Math.Abs(this.CY - this.TargetCret.CY) <= 7))
                    {
                        if (ChainShot < ChainShotCount - 1)
                        {
                            ChainShot++;
                            this.TargetFocusTime  =  HUtil32.GetTickCount();
                            FlyAxeAttack(this.TargetCret);
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
                    else
                    {
                        if (this.TargetCret.MapName == this.MapName)
                        {
                            if ((Math.Abs(this.CX - this.TargetCret.CX) <= 11) && (Math.Abs(this.CY - this.TargetCret.CY) <= 11))
                            {
                                // 距埃 冻绢柳 版快 蝶扼埃促.
                                this.SetTargetXY(this.TargetCret.CX, this.TargetCret.CY);
                            }
                        }
                        else
                        {
                            this.LoseTarget();
                        }
                        // <!!林狼> TargetCret := nil肺 官柴
                    }
                }
            }
            return result;
        }

        public override void Run()
        {
            int i;
            int dis;
            int d;
            TCreature cret;
            TCreature nearcret;
            dis = 9999;
            nearcret = null;
            // if not Death and not RunDone and not BoGhost and
            // (StatusArr[POISON_STONE] = 0) and (StatusArr[POISON_ICE] = 0) and
            // (StatusArr[POISON_STUN] = 0) then begin
            if (!RunDone && this.IsMoveAble())
            {
                if (HUtil32.GetTickCount() - this.SearchEnemyTime > 5000)
                {
                    this.SearchEnemyTime  =  HUtil32.GetTickCount();
                    // 惑加罐篮 run 俊辑 HitTime 犁汲沥窃.
                    for (i = 0; i < this.VisibleActors.Count; i++)
                    {
                        cret = (TCreature)this.VisibleActors[i].cret;
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
                if (GetCurrentTime - this.WalkTime > this.GetNextWalkTime())
                {
                    if (this.TargetCret != null)
                    {
                        if ((Math.Abs(this.CX - this.TargetCret.CX) <= 4) && (Math.Abs(this.CY - this.TargetCret.CY) <= 4))
                        {
                            if ((Math.Abs(this.CX - this.TargetCret.CX) <= 2) && (Math.Abs(this.CY - this.TargetCret.CY) <= 2))
                            {
                                if (new System.Random(5).Next() == 0)
                                {
                                    M2Share.GetBackPosition(this, ref this.TargetX, ref this.TargetY);
                                }
                            }
                            else
                            {
                                M2Share.GetBackPosition(this, ref this.TargetX, ref this.TargetY);
                            }
                        }
                    }
                }
            }
            base.Run();
        }
    }
    
}

