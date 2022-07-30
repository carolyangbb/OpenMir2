using System;
using System.Collections;
using SystemModule;

namespace GameSvr
{
    public class TDragon : TATMonster
    {
        private bool bofirst = false;
        private readonly ArrayList ChildList = null;
        // ==============================================================================
        //Constructor  Create()
        public TDragon() : base()
        {
            bofirst = true;
            this.HideMode = true;
            this.RaceServer = Grobal2.RC_FIREDRAGON;
            this.ViewRange = 12;
            this.BoWalkWaitMode = true;
            this.BoDontMove = true;
            ChildList = new ArrayList();
        }
        //@ Destructor  Destroy()
        ~TDragon()
        {
            TCreature mon;
            int i;
            if (ChildList != null)
            {
                for (i = ChildList.Count - 1; i >= 0; i--)
                {
                    mon = (TCreature)ChildList[0];
                    mon.WAbil.HP = 0;
                    ChildList.RemoveAt(0);
                }
                ChildList.Free();
            }
            base.Destroy();
        }
        public override void RecalcAbilitys()
        {
            base.RecalcAbilitys();
            ResetLevel();
        }

        protected void ResetLevel()
        {
            int[,] bodypos = { { 0, -5 }, { 1, -5 }, { -1, -4 }, { 0, -4 }, { 1, -4 }, { 2, -4 }, { -2, -3 }, { -1, -3 }, { 0, -3 }, { 1, -3 }, { 2, -3 }, { -3, -2 }, { -2, -2 }, { -1, -3 }, { 0, -2 }, { 1, -2 }, { 2, -2 }, { -3, -1 }, { -2, -1 }, { -1, -1 }, { 0, -1 }, { 1, -1 }, { 2, -1 }, { -3, 0 }, { -2, 0 }, { -1, 0 }, { 0, 0 }, { 1, 0 }, { 2, 0 }, { 3, 0 }, { -2, 1 }, { -1, 1 }, { 0, 1 }, { 1, 1 }, { 2, 1 }, { 3, 1 }, { -1, 2 }, { 0, 2 }, { 1, 2 }, { 2, 2 }, { 0, 3 }, { 1, 3 } };
            TCreature mon;
            int i;
            if (this.PEnvir != null)
            {
                for (i = 0; i <= 41; i++)
                {
                    if ((bodypos[i][0] != 0) || (bodypos[i][1] != 0))
                    {
                        // 颇玫付锋个捞 00
                        mon = svMain.UserEngine.AddCreatureSysop(this.PEnvir.MapName, this.CX + bodypos[i][0], this.CY + bodypos[i][1], "00");
                        if (mon != null)
                        {
                            ChildList.Add(mon);
                        }
                    }
                    // if  i <> cx
                }
                // for i
            }
        }

        // 馆靛矫 target <> nil
        public int RangeAttack_MPow(TUserMagic pum)
        {
            int result;
            result = pum.pDef.MinPower + new System.Random(pum.pDef.MaxPower - pum.pDef.MinPower).Next();
            return result;
        }

        protected void RangeAttack(TCreature targ)
        {
            int i;
            int pwr;
            int dam;
            int TempDir;
            int ix;
            int iy;
            int ixf;
            int iyf;
            int ixt;
            int iyt;
            TCreature cret;
            ArrayList list;
            if (targ == null)
            {
                return;
            }
            TempDir = M2Share.GetNextDirection(this.CX, this.CY, targ.CX, targ.CY);
            switch (TempDir)
            {
                case 0:
                case 1:
                case 6:
                case 7:
                    this.SendRefMsg(Grobal2.RM_DRAGON_FIRE3, (ushort)TempDir, this.CX, this.CY, targ.ActorId, "");
                    break;
                case 5:
                    this.SendRefMsg(Grobal2.RM_DRAGON_FIRE2, (ushort)TempDir, this.CX, this.CY, targ.ActorId, "");
                    break;
                case 2:
                case 3:
                case 4:
                    this.SendRefMsg(Grobal2.RM_DRAGON_FIRE1, (ushort)TempDir, this.CX, this.CY, targ.ActorId, "");
                    break;
            }
            TAbility _wvar1 = this.WAbil;
            pwr = new System.Random(HiByte(this.WAbil.DC)).Next() + _wvar1.Lobyte(this.WAbil.DC) + new System.Random(_wvar1.Lobyte(this.WAbil.MC)).Next();
            pwr = pwr * (new System.Random(2).Next() + 1);
            if (targ.LifeAttrib == Grobal2.LA_UNDEAD)
            {
                pwr = HUtil32.MathRound(pwr * 1.5);
            }
            ixf = _MAX(0, targ.CX - 2);
            ixt = _MIN(this.PEnvir.MapWidth - 1, targ.CX + 2);
            iyf = _MAX(0, targ.CY - 2);
            iyt = _MIN(this.PEnvir.MapHeight - 1, targ.CY + 2);
            for (ix = ixf; ix <= ixt; ix++)
            {
                for (iy = iyf; iy <= iyt; iy++)
                {
                    list = new ArrayList();
                    this.PEnvir.GetAllCreature(ix, iy, true, list);
                    for (i = 0; i < list.Count; i++)
                    {
                        cret = (TCreature)list[i];
                        if (this.IsProperTarget(cret))
                        {
                            dam = cret.GetMagStruckDamage(this, pwr);
                            if (cret.LifeAttrib == Grobal2.LA_UNDEAD)
                            {
                                pwr = HUtil32.MathRound(pwr * 1.5);
                            }
                            if (dam > 0)
                            {
                                cret.StruckDamage(dam, this);
                                // wparam
                                // lparam1
                                // lparam2
                                // hiter
                                cret.SendDelayMsg((TCreature)Grobal2.RM_STRUCK, Grobal2.RM_REFMESSAGE, dam, cret.WAbil.HP, cret.WAbil.MaxHP, this.ActorId, "", 600 + _MAX(Math.Abs(this.CX - cret.CX), Math.Abs(this.CY - cret.CY)) * 70);
                            }
                        }
                    }
                    list.Free();
                }
            }
        }

        // 馆靛矫 target <> nil
        public int AttackAll_MPow(TUserMagic pum)
        {
            int result;
            result = pum.pDef.MinPower + new System.Random(pum.pDef.MaxPower - pum.pDef.MinPower).Next();
            return result;
        }

        protected void AttackAll(TCreature Targ)
        {
            int ix;
            int iy;
            int ixf;
            int ixt;
            int iyf;
            int iyt;
            int dam;
            int pwr;
            int i;
            ArrayList list;
            TCreature cret;
            if (Targ == null)
            {
                return;
            }
            this.SendRefMsg(Grobal2.RM_LIGHTING, this.Dir, this.CX, this.CY, this.ActorId, "");
            TAbility _wvar1 = this.WAbil;
            pwr = new System.Random(HiByte(this.WAbil.DC)).Next() + _wvar1.Lobyte(this.WAbil.DC) + new System.Random(_wvar1.Lobyte(this.WAbil.MC)).Next();
            pwr = pwr * (new System.Random(5).Next() + 1);
            ixf = _MAX(0, Targ.CX - 10);
            ixt = _MIN(this.PEnvir.MapWidth - 1, Targ.CX + 10);
            iyf = _MAX(0, Targ.CY - 10);
            iyt = _MIN(this.PEnvir.MapHeight - 1, Targ.CY + 10);
            for (ix = ixf; ix <= ixt; ix++)
            {
                for (iy = iyf; iy <= iyt; iy++)
                {
                    list = new ArrayList();
                    this.PEnvir.GetAllCreature(ix, iy, true, list);
                    for (i = 0; i < list.Count; i++)
                    {
                        cret = (TCreature)list[i];
                        if (this.IsProperTarget(cret))
                        {
                            dam = cret.GetMagStruckDamage(this, pwr);
                            if (cret.LifeAttrib == Grobal2.LA_UNDEAD)
                            {
                                pwr = HUtil32.MathRound(pwr * 1.5);
                            }
                            if (dam > 0)
                            {
                                cret.StruckDamage(dam, this);
                                // wparam
                                // lparam1
                                // lparam2
                                // hiter
                                cret.SendDelayMsg((TCreature)Grobal2.RM_STRUCK, Grobal2.RM_REFMESSAGE, dam, cret.WAbil.HP, cret.WAbil.MaxHP, this.ActorId, "", 600);
                            }
                        }
                    }
                    list.Free();
                }
            }
        }

        protected override bool AttackTarget()
        {
            bool result;
            result = false;
            if ((this.TargetCret != null) && (this.TargetCret != this.Master))
            {
                if (GetCurrentTime - this.HitTime > this.GetNextHitTime())
                {
                    this.HitTime = GetCurrentTime;
                    if ((Math.Abs(this.CX - this.TargetCret.CX) <= this.ViewRange) && (Math.Abs(this.CY - this.TargetCret.CY) <= this.ViewRange) && (!this.TargetCret.Death) && (!this.TargetCret.BoGhost))
                    {
                        if (new System.Random(5).Next() == 0)
                        {
                            // 葛滴傍拜
                            AttackAll(this.TargetCret);
                        }
                        else
                        {
                            RangeAttack(this.TargetCret);
                        }
                        result = true;
                    }
                    this.LoseTarget();
                    // <!!林狼> TargetCret := nil肺 官柴
                }
            }
            return result;
        }

        public override void Struck(TCreature hiter)
        {
            base.Struck(hiter);
            if (hiter != null)
            {
                // 颇玫付锋篮 裹困啊 8 捞窍老版快俊父 版氰摹甫 刘啊矫挪促.
                // 钢府辑 惧火窍霸 傍拜窍绰 贱荤 规瘤
                if ((Math.Abs(hiter.CX - this.CX) <= 8) && (Math.Abs(hiter.CY - this.CY) <= 8))
                {
                    this.SendMsg(this, Grobal2.RM_DRAGON_EXP, 0, new System.Random(3).Next() + 1, 0, 0, "");
                }
            }
        }

        public override void Run()
        {
            if (bofirst)
            {
                bofirst = false;
                this.Dir = 5;
                this.HideMode = false;
                this.SendRefMsg(Grobal2.RM_DIGUP, this.Dir, this.CX, this.CY, 0, "");
                ResetLevel();
            }
            base.Run();
        }

    }
}