using System;
using System.Collections;
using SystemModule;

namespace GameSvr
{
    public class TFoxTaoist : TATMonster
    {
        private bool BoRecallComplete = false;
        // ---------------------------------------------------------------------------
        // TFoxTaoist    厚岿咯快(档荤)
        //Constructor  Create()
        public TFoxTaoist() : base()
        {
            this.SearchRate = 2500 + ((long)new System.Random(1500).Next());
        }
        public override void Initialize()
        {
            BoRecallComplete = false;
            this.ViewRange = 7;
            base.Initialize();
        }

        public override void Attack(TCreature target, byte dir)
        {
            int pwr;
            this.Dir = dir;
            TAbility _wvar1 = this.WAbil;
            pwr = this.GetAttackPower(HUtil32.LoByte(_wvar1.DC), HiByte(_wvar1.DC) - HUtil32.LoByte(_wvar1.DC));
            if (pwr <= 0)
            {
                return;
            }
            this.SendRefMsg(Grobal2.RM_HIT, this.Dir, this.CX, this.CY, target.ActorId, "");
            if (this.IsProperTarget(target))
            {
                target.StruckDamage(pwr, this);
                // wparam
                // lparam1
                // lparam2
                // hiter
                target.SendDelayMsg(Grobal2.RM_STRUCK, Grobal2.RM_REFMESSAGE, (ushort)pwr, target.WAbil.HP, target.WAbil.MaxHP, this.ActorId, "", 500);
            }
        }

        public void RangeAttack(TCreature targ)
        {
            int pwr;
            int sec;
            int skilllevel;
            if (targ == null)
            {
                return;
            }
            sec = 60;
            pwr = 70;
            skilllevel = 3;
            this.MagMakeCurseArea(targ.CX, targ.CY, 2, sec, pwr, skilllevel, false);
            this.Dir = M2Share.GetNextDirection(this.CX, this.CY, targ.CX, targ.CY);
            this.SendRefMsg(Grobal2.RM_LIGHTING_1, this.Dir, this.CX, this.CY, targ.ActorId, "");
        }

        public void RangeAttack2(TCreature targ)
        {
            // 馆靛矫 target <> nil
            int i;
            int pwr;
            int dam;
            short sx = 0;
            short sy = 0;
            short tx = 0;
            short ty = 0;
            ArrayList list;
            TCreature cret;
            if (targ == null)
            {
                return;
            }
            this.Dir = M2Share.GetNextDirection(this.CX, this.CY, targ.CX, targ.CY);
            this.SendRefMsg(Grobal2.RM_LIGHTING, this.Dir, this.CX, this.CY, targ.ActorId, "");
            if (M2Share.GetNextPosition(this.PEnvir, this.CX, this.CY, this.Dir, 1, ref sx, ref sy))
            {
                M2Share.GetNextPosition(this.PEnvir, this.CX, this.CY, this.Dir, 9, ref tx, ref ty);
                TAbility _wvar1 = this.WAbil;
                pwr = HUtil32._MAX(0, HUtil32.LoByte(_wvar1.DC) + new System.Random(HiByte(_wvar1.DC) - HUtil32.LoByte(_wvar1.DC) + 1).Next());
                list = new ArrayList();
                this.PEnvir.GetAllCreature(targ.CX, targ.CY, true, list);
                for (i = 0; i < list.Count; i++)
                {
                    cret = (TCreature)list[i];
                    if (this.IsProperTarget(cret))
                    {
                        dam = cret.GetMagStruckDamage(this, pwr);
                        if (dam > 0)
                        {
                            cret.StruckDamage(dam, this);
                            cret.SendDelayMsg(Grobal2.RM_STRUCK, Grobal2.RM_REFMESSAGE, (ushort)dam, cret.WAbil.HP, cret.WAbil.MaxHP, this.ActorId, "", 800);
                        }
                    }
                }
                list.Free();
            }
        }

        public override void Run()
        {
            TCreature cret;
            cret = null;
            if (!BoRecallComplete)
            {
                if (this.WAbil.HP <= this.WAbil.MaxHP / 2)
                {
                    this.SendRefMsg(Grobal2.RM_LIGHTING_2, this.Dir, this.CX, this.CY, this.ActorId.TargetCret, "");
                    // 家券
                    // '厚岿孺龋'
                    cret = svMain.UserEngine.AddCreatureSysop(this.PEnvir.MapName, this.CX + 1, this.CY, "BlackFoxFolks");
                    if (cret != null)
                    {
                        cret.SendRefMsg(Grobal2.RM_NORMALEFFECT, 0, cret.CX, cret.CY, Grobal2.NE_FOX_MOVESHOW, "");
                    }
                    // '厚岿孺龋'
                    cret = svMain.UserEngine.AddCreatureSysop(this.PEnvir.MapName, this.CX - 1, this.CY, "BlackFoxFolks");
                    if (cret != null)
                    {
                        cret.SendRefMsg(Grobal2.RM_NORMALEFFECT, 0, cret.CX, cret.CY, Grobal2.NE_FOX_MOVESHOW, "");
                    }
                    // '厚岿利龋'
                    cret = svMain.UserEngine.AddCreatureSysop(this.PEnvir.MapName, this.CX, this.CY + 1, "RedFoxFolks");
                    if (cret != null)
                    {
                        cret.SendRefMsg(Grobal2.RM_NORMALEFFECT, 0, cret.CX, cret.CY, Grobal2.NE_FOX_MOVESHOW, "");
                    }
                    // '厚岿利龋'
                    cret = svMain.UserEngine.AddCreatureSysop(this.PEnvir.MapName, this.CX, this.CY - 1, "RedFoxFolks");
                    if (cret != null)
                    {
                        cret.SendRefMsg(Grobal2.RM_NORMALEFFECT, 0, cret.CX, cret.CY, Grobal2.NE_FOX_MOVESHOW, "");
                    }
                    // UserEngine.CryCry (RM_CRY, PEnvir, CX, CY, 10000, '家券 : ' + TargetCret.UserName);//test
                    BoRecallComplete = true;
                }
            }
            if (!this.RunDone && this.IsMoveAble())
            {
                if (GetCurrentTime - this.WalkTime > this.GetNextWalkTime())
                {
                    // 惑加罐篮 run俊辑 WalkTime 犁汲沥窃
                    if (this.TargetCret != null)
                    {
                        if ((Math.Abs(this.CX - this.TargetCret.CX) <= 5) && (Math.Abs(this.CY - this.TargetCret.CY) <= 5))
                        {
                            if ((Math.Abs(this.CX - this.TargetCret.CX) <= 2) && (Math.Abs(this.CY - this.TargetCret.CY) <= 2))
                            {
                                // 呈公 啊鳖快搁, 肋 档噶 救皑.
                                if (new System.Random(3).Next() == 0)
                                {
                                    // 档噶皑.
                                    M2Share.GetBackPosition(this, ref this.TargetX, ref this.TargetY);
                                }
                            }
                            else
                            {
                                // 档噶皑.
                                M2Share.GetBackPosition(this, ref this.TargetX, ref this.TargetY);
                            }
                        }
                    }
                }
            }
            base.Run();
        }

        protected override bool AttackTarget()
        {
            bool result;
            byte targdir=0;
            result = false;
            if (this.TargetCret != null)
            {
                if (GetCurrentTime - this.HitTime > this.GetNextHitTime())
                {
                    this.HitTime = GetCurrentTime;
                    if ((Math.Abs(this.CX - this.TargetCret.CX) <= 7) && (Math.Abs(this.CY - this.TargetCret.CY) <= 7))
                    {
                        if (this.TargetInAttackRange(this.TargetCret, ref targdir) && (new System.Random(10).Next() < 8))
                        {
                            this.TargetFocusTime  =  HUtil32.GetTickCount();
                            Attack(this.TargetCret, targdir);
                            result = true;
                        }
                        else
                        {
                            if ((Math.Abs(this.CX - this.TargetCret.CX) <= 6) && (Math.Abs(this.CY - this.TargetCret.CY) <= 6))
                            {
                                if (new System.Random(10).Next() < 7)
                                {
                                    // 咯快_气混拌
                                    RangeAttack2(this.TargetCret);
                                    result = true;
                                }
                                else if (new System.Random(10).Next() < 6)
                                {
                                    // 咯快_历林贱
                                    RangeAttack(this.TargetCret);
                                    result = true;
                                }
                            }
                            else
                            {
                                if (new System.Random(10).Next() < 6)
                                {
                                    // 咯快_历林贱
                                    RangeAttack(this.TargetCret);
                                    result = true;
                                }
                            }
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
                            // <!!林狼> TargetCret := nil肺 官柴
                        }
                    }
                }
            }
            return result;
        }

    }
}