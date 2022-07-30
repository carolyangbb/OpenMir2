using System;
using System.Collections;
using SystemModule;

namespace GameSvr
{
    public class TFoxWizard : TATMonster
    {
        private long WarpTime = 0;
        // ---------------------------------------------------------------------------
        // TFoxWizard    厚岿咯快(贱荤)
        //Constructor  Create()
        public TFoxWizard() : base()
        {
            this.SearchRate = 2500 + ((long)new System.Random(1500).Next());
        }
        public override void Initialize()
        {
            WarpTime = GetTickCount;
            this.ViewRange = 7;
            base.Initialize();
        }

        public override void Attack(TCreature target, byte dir)
        {
            int i;
            int wide;
            ArrayList rlist;
            TCreature cret;
            int pwr;
            if (target == null)
            {
                return;
            }
            wide = 0;
            this.Dir = M2Share.GetNextDirection(this.CX, this.CY, target.CX, target.CY);
            this.SendRefMsg(Grobal2.RM_LIGHTING, this.Dir, this.CX, this.CY, target.ActorId, "");
            TAbility _wvar1 = this.WAbil;
            pwr = this.GetAttackPower(_wvar1.Lobyte(_wvar1.DC), (short)HiByte(_wvar1.DC) - _wvar1.Lobyte(_wvar1.DC));
            if (pwr <= 0)
            {
                return;
            }
            rlist = new ArrayList();
            this.GetMapCreatures(this.PEnvir, target.CX, target.CY, wide, rlist);
            for (i = 0; i < rlist.Count; i++)
            {
                cret = (TCreature)rlist[i];
                if (this.IsProperTarget(cret))
                {
                    this.SelectTarget(cret);
                    cret.SendDelayMsg(this, Grobal2.RM_MAGSTRUCK, 0, pwr, 0, 0, "", 600);
                }
            }
            rlist.Free();
        }

        public void RangeAttack(TCreature targ)
        {
            // 馆靛矫 target <> nil
            int i;
            int pwr;
            int dam;
            int sx;
            int sy;
            int tx;
            int ty;
            ArrayList list;
            TCreature cret;
            if (targ == null)
            {
                return;
            }
            this.Dir = M2Share.GetNextDirection(this.CX, this.CY, targ.CX, targ.CY);
            this.SendRefMsg(Grobal2.RM_LIGHTING_1, this.Dir, this.CX, this.CY, targ.ActorId, "");
            if (M2Share.GetNextPosition(this.PEnvir, this.CX, this.CY, this.Dir, 1, ref sx, ref sy))
            {
                M2Share.GetNextPosition(this.PEnvir, this.CX, this.CY, this.Dir, 9, ref tx, ref ty);
                TAbility _wvar1 = this.WAbil;
                pwr = _wvar1._MAX(0, _wvar1.Lobyte(_wvar1.DC) + new System.Random((short)HiByte(_wvar1.DC) - _wvar1.Lobyte(_wvar1.DC) + 1).Next());
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
                            // wparam
                            // lparam1
                            // lparam2
                            // hiter
                            cret.SendDelayMsg((TCreature)Grobal2.RM_STRUCK, Grobal2.RM_REFMESSAGE, dam, cret.WAbil.HP, cret.WAbil.MaxHP, this.ActorId, "", 800);
                        }
                    }
                }
                list.Free();
            }
        }

        public override void Run()
        {
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

        public override void RunMsg(TMessageInfo msg)
        {
            switch (msg.Ident)
            {
                case Grobal2.RM_REFMESSAGE:
                    if (((int)msg.sender) == Grobal2.RM_STRUCK)
                    {
                        if (new System.Random(100).Next() < 30)
                        {
                            // 2檬 掉饭捞
                            if ((GetTickCount - WarpTime > 2000) && (!this.Death))
                            {
                                WarpTime = GetTickCount;
                                // 况橇
                                this.SendRefMsg(Grobal2.RM_NORMALEFFECT, 0, this.CX, this.CY, Grobal2.NE_FOX_MOVEHIDE, "");
                                this.RandomSpaceMoveInRange(2, 4, 4);
                                this.SendRefMsg(Grobal2.RM_NORMALEFFECT, 0, this.CX, this.CY, Grobal2.NE_FOX_MOVESHOW, "");
                            }
                        }
                    }
                    break;
            }
            base.RunMsg(msg);
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
                        if (new System.Random(10).Next() < 7)
                        {
                            // 碍拜
                            Attack(this.TargetCret, this.Dir);
                            result = true;
                        }
                        else if (new System.Random(10).Next() < 6)
                        {
                            // 气凯颇
                            RangeAttack(this.TargetCret);
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
                            // <!!林狼> TargetCret := nil肺 官柴
                        }
                    }
                }
            }
            return result;
        }

    }
}