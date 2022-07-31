using System;
using System.Collections;
using SystemModule;

namespace GameSvr
{
    public class TGoldenImugi : TATMonster
    {
        public bool DontAttack = false;
        public bool DontAttackCheck = false;
        public bool AttackState = false;
        public bool InitialState = false;
        public bool ChildMobRecalled = false;
        public bool FinalWarp = false;
        public bool FirstCheck = false;
        public int TwinGenDelay = 0;
        public long sectick = 0;
        public long RevivalTime = 0;
        public long WarpTime = 0;
        public long TargetTime = 0;
        public long RangeAttackTime = 0;
        public TCreature OldTargetCret = null;

        public TGoldenImugi() : base()
        {
            this.ViewRange = 12;
            TwinGenDelay = 100;
            sectick = HUtil32.GetTickCount();
            this.BoNoItem = true;
            FirstCheck = true;
            DontAttack = true;
            DontAttackCheck = false;
            AttackState = false;
            InitialState = false;
            ChildMobRecalled = false;
            FinalWarp = false;
            RevivalTime = HUtil32.GetTickCount();
            WarpTime = HUtil32.GetTickCount();
            TargetTime = HUtil32.GetTickCount();
            RangeAttackTime = HUtil32.GetTickCount();
            OldTargetCret = null;
        }

        public override void RunMsg(TMessageInfo msg)
        {
            switch (msg.Ident)
            {
                case Grobal2.RM_MAKEPOISON:
                    DontAttack = false;
                    break;
            }
            base.RunMsg(msg);
        }

        public override void Run()
        {
            byte ndir;
            short nx = 0;
            short ny = 0;
            TCreature cret;
            cret = null;
            int snakecount = 0;
            if (HUtil32.GetTickCount() - sectick > 3000)
            {
                this.BreakHolySeize();
                int imugicount = 0;
                sectick = HUtil32.GetTickCount();
                if (this.PEnvir != null)
                {
                    for (var ix = 0; ix < this.PEnvir.MapWidth; ix++)
                    {
                        for (var iy = 0; iy < this.PEnvir.MapHeight; iy++)
                        {
                            cret = (TCreature)this.PEnvir.GetCreature(ix, iy, true);
                            if (cret != null)
                            {
                                if ((!cret.Death) && (cret.RaceServer == Grobal2.RC_GOLDENIMUGI))
                                {
                                    if (!this.Death)
                                    {
                                        if (DontAttackCheck)
                                        {
                                            (cret as TGoldenImugi).DontAttack = false;
                                        }
                                        else if (DontAttack == false)
                                        {
                                            DontAttackCheck = true;
                                        }
                                    }
                                    imugicount++;
                                    if (imugicount > 2)
                                    {
                                        cret.MakeGhost(8);
                                    }
                                    if ((imugicount == 2) && (cret != this))
                                    {
                                        if ((Math.Abs(cret.CX - this.CX) >= 10) || (Math.Abs(cret.CY - this.CY) >= 10))
                                        {
                                            if (this.WarpTime < (cret as TGoldenImugi).WarpTime)
                                            {
                                                this.SendRefMsg(Grobal2.RM_NORMALEFFECT, 0, this.CX, this.CY, Grobal2.NE_SN_MOVEHIDE, "");
                                                this.SpaceMove(cret.PEnvir.MapName, cret.CX, cret.CY, 0);
                                                WarpTime = HUtil32.GetTickCount();
                                                this.SendRefMsg(Grobal2.RM_NORMALEFFECT, 0, this.CX, this.CY, Grobal2.NE_SN_MOVESHOW, "");
                                            }
                                            else
                                            {
                                                cret.SendRefMsg(Grobal2.RM_NORMALEFFECT, 0, cret.CX, cret.CY, Grobal2.NE_SN_MOVEHIDE, "");
                                                cret.SpaceMove(this.PEnvir.MapName, this.CX, this.CY, 0);
                                                (cret as TGoldenImugi).WarpTime = HUtil32.GetTickCount();
                                                cret.SendRefMsg(Grobal2.RM_NORMALEFFECT, 0, cret.CX, cret.CY, Grobal2.NE_SN_MOVESHOW, "");
                                            }
                                        }
                                        if ((Math.Abs(cret.CX - this.CX) <= 2) && (Math.Abs(cret.CY - this.CY) <= 2))
                                        {
                                            if (new System.Random(3).Next() == 0)
                                            {
                                                ndir = M2Share.GetNextDirection(cret.CX, cret.CY, this.CX, this.CY);
                                                M2Share.GetNextPosition(this.PEnvir, cret.CX, cret.CY, ndir, 5, ref this.TargetX, ref this.TargetY);
                                            }
                                        }
                                    }
                                }
                                if ((!cret.Death) && (cret.UserName == M2Share.__WhiteSnake))
                                {
                                    snakecount++;
                                }
                            }
                        }
                    }
                }
                if (FirstCheck)
                {
                    FirstCheck = false;
                    TwinGenDelay = 1;
                }
                if (imugicount == 1)
                {
                    if (TwinGenDelay <= 0)
                    {
                        cret = M2Share.UserEngine.AddCreatureSysop(this.PEnvir.MapName, (short)_MIN(this.CX + 2, this.PEnvir.MapWidth - 1), this.CY, M2Share.__GoldenImugi);
                        if (cret != null)
                        {
                            if (!DontAttack)
                            {
                                M2Share.UserEngine.CryCry(Grobal2.RM_CRY, this.PEnvir, this.CX, this.CY, 10000, " -" + M2Share.__GoldenImugi + " has recalled its clone.");
                            }
                            RevivalTime = HUtil32.GetTickCount();
                            this.SendRefMsg(Grobal2.RM_LIGHTING, this.Dir, this.CX, this.CY, this.ActorId, "");
                            cret.SendRefMsg(Grobal2.RM_NORMALEFFECT, 0, cret.CX, cret.CY, Grobal2.NE_SN_RELIVE, "");
                            if (!DontAttack)
                            {
                                cret.WAbil.HP = (short)(cret.WAbil.MaxHP / 3 * 2);
                            }
                            if (DontAttack)
                            {
                                InitialState = false;
                            }
                        }
                        TwinGenDelay = 100;
                    }
                    TwinGenDelay -= 1;
                    if (this.Death)
                    {
                        this.MakeGhost(8);
                    }
                }
                else if (imugicount >= 2)
                {
                    FirstCheck = false;
                    TwinGenDelay = 100;
                }
            }
            if (DontAttack == false)
            {
                if (AttackState == false)
                {
                    this.SendRefMsg(Grobal2.RM_TURN, this.Dir, this.CX, this.CY, 0, "");
                    AttackState = true;
                    this.BoDontMove = false;
                }
            }
            else
            {
                if (InitialState == false)
                {
                    this.SendRefMsg(Grobal2.RM_DIGDOWN, this.Dir, this.CX, this.CY, 0, "");
                    InitialState = true;
                    this.BoDontMove = true;
                }
            }
            if (snakecount > 0)
            {
                this.AddAbil.HealthRecover = (ushort)(snakecount * 2);
                this.HealthRecover = (byte)this.AddAbil.HealthRecover;
            }
            else
            {
                this.AddAbil.HealthRecover = 0;
                this.HealthRecover = (byte)this.AddAbil.HealthRecover;
            }
            if (this.WAbil.HP <= this.WAbil.MaxHP / 2)
            {
                if (!ChildMobRecalled)
                {
                    M2Share.GetFrontPosition(this, ref nx, ref ny);
                    M2Share.UserEngine.AddCreatureSysop(this.PEnvir.MapName, nx, ny, M2Share.__WhiteSnake);
                    M2Share.UserEngine.AddCreatureSysop(this.PEnvir.MapName, nx, ny, M2Share.__WhiteSnake);
                    ChildMobRecalled = true;
                }
            }
            if (this.WAbil.HP <= this.WAbil.MaxHP / 10)
            {
                if (!FinalWarp)
                {
                    this.MagDefenceUp(60, 20);
                    this.MagMagDefenceUp(60, 20);
                    this.LoseTarget();
                    this.SendRefMsg(Grobal2.RM_NORMALEFFECT, 0, this.CX, this.CY, Grobal2.NE_SN_MOVEHIDE, "");
                    this.RandomSpaceMoveInRange(0, 30, 80);
                    WarpTime = HUtil32.GetTickCount();
                    this.SendRefMsg(Grobal2.RM_NORMALEFFECT, 0, this.CX, this.CY, Grobal2.NE_SN_MOVESHOW, "");
                    FinalWarp = true;
                }
            }
            base.Run();
        }

        public override void Attack(TCreature targ, byte dir)
        {
            TCreature cret;
            this.Dir = dir;
            TAbility _wvar1 = this.WAbil;
            int dam = HUtil32.LoByte(_wvar1.DC) + new System.Random(HiByte(_wvar1.DC) - HUtil32.LoByte(_wvar1.DC) + 1).Next();
            if (dam <= 0)
            {
                return;
            }
            this.SendRefMsg(Grobal2.RM_HIT, this.Dir, this.CX, this.CY, 0, "");
            TAbility _wvar2 = this.WAbil;
            int pwr = this.GetAttackPower(HUtil32.LoByte(_wvar2.DC), HiByte(_wvar2.DC) - HUtil32.LoByte(_wvar2.DC));
            for (var i = 0; i <= 4; i++)
            {
                for (var k = 0; k <= 4; k++)
                {
                    if (M2Share.SpitMap[dir, i, k] == 1)
                    {
                        int mx = this.CX - 2 + k;
                        int my = this.CY - 2 + i;
                        cret = (TCreature)this.PEnvir.GetCreature(mx, my, true);
                        if ((cret != null) && (cret != this))
                        {
                            if (this.IsProperTarget(cret))
                            {
                                if (new System.Random(cret.SpeedPoint).Next() < this.AccuracyPoint)
                                {
                                    this.HitHit2(cret, 0, pwr, true);
                                }
                            }
                        }
                    }
                }
            }
        }

        public void RangeAttack(TCreature targ)
        {
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
            this.SendRefMsg(Grobal2.RM_LIGHTING_1, this.Dir, this.CX, this.CY, targ.ActorId, "");
            if (M2Share.GetNextPosition(this.PEnvir, this.CX, this.CY, this.Dir, 1, ref sx, ref sy))
            {
                M2Share.GetNextPosition(this.PEnvir, this.CX, this.CY, this.Dir, 9, ref tx, ref ty);
                TAbility _wvar1 = this.WAbil;
                pwr = HUtil32._MAX(0, HUtil32.LoByte(_wvar1.DC) + new System.Random(HiByte(_wvar1.DC) - HUtil32.LoByte(_wvar1.DC) + 1).Next());
                list = new ArrayList();
                this.PEnvir.GetCreatureInRange(targ.CX, targ.CY, 1, true, list);
                for (var i = 0; i < list.Count; i++)
                {
                    cret = (TCreature)list[i];
                    if (this.IsProperTarget(cret))
                    {
                        dam = cret.GetMagStruckDamage(this, pwr);
                        if (dam > 0)
                        {
                            cret.StruckDamage(dam, this);
                            cret.SendDelayMsg(Grobal2.RM_STRUCK, Grobal2.RM_REFMESSAGE, (short)dam, cret.WAbil.HP, cret.WAbil.MaxHP, this.ActorId, "", 800);
                        }
                    }
                }
                list.Free();
            }
        }

        public void RangeAttack2(TCreature targ)
        {
            if (targ == null)
            {
                return;
            }
            this.SendRefMsg(Grobal2.RM_LIGHTING, this.Dir, this.CX, this.CY, this.ActorId, "");
            this.SendRefMsg(Grobal2.RM_NORMALEFFECT, 0, this.CX, this.CY, Grobal2.NE_POISONFOG, "");
            for (var i = 0; i < this.VisibleActors.Count; i++)
            {
                TCreature cret = (TCreature)this.VisibleActors[i].cret;
                if ((!cret.Death) && this.IsProperTarget(cret))
                {
                    if ((cret.RaceServer == Grobal2.RC_USERHUMAN) || (cret.Master != null))
                    {
                        if (new System.Random(2 + cret.AntiPoison).Next() == 0)
                        {
                            cret.MakePoison(Grobal2.POISON_DAMAGEARMOR, 60, 5);
                        }
                    }
                }
            }
        }

        protected override bool AttackTarget()
        {
            byte targdir = 0;
            bool result = false;
            TCreature cret = null;
            if (DontAttack)
            {
                this.LoseTarget();
                return result;
            }
            if (GetCurrentTime < ((long)new System.Random(3000).Next() + 4000 + TargetTime))
            {
                if (OldTargetCret != null)
                {
                    this.TargetCret = OldTargetCret;
                }
            }
            if (this.TargetCret != null)
            {
                if (GetCurrentTime - this.HitTime > this.GetNextHitTime())
                {
                    this.HitTime = GetCurrentTime;
                    if ((Math.Abs(this.CX - this.TargetCret.CX) <= 11) && (Math.Abs(this.CY - this.TargetCret.CY) <= 11))
                    {
                        if ((this.TargetInSpitRange(this.TargetCret, ref targdir) && (new System.Random(3).Next() < 2)) || (HUtil32.GetTickCount() - RevivalTime < 3000))
                        {
                            this.TargetFocusTime = HUtil32.GetTickCount();
                            this.Dir = M2Share.GetNextDirection(this.CX, this.CY, this.TargetCret.CX, this.TargetCret.CY);
                            Attack(this.TargetCret, targdir);
                            result = true;
                        }
                        else
                        {
                            if (GetCurrentTime < (8000 + TargetTime))
                            {
                                this.TargetFocusTime = HUtil32.GetTickCount();
                                if ((GetCurrentTime < (30000 + RangeAttackTime)) && (new System.Random(10).Next() < 8))
                                {
                                    RangeAttack(this.TargetCret);
                                }
                                else
                                {
                                    RangeAttack2(this.TargetCret);
                                    RangeAttackTime = HUtil32.GetTickCount();
                                }
                            }
                            else
                            {
                                try
                                {
                                    if (this.VisibleActors.Count > 0)
                                    {
                                        cret = (TCreature)this.VisibleActors[new System.Random(this.VisibleActors.Count).Next()].cret;
                                        if (cret != null)
                                        {
                                            if (!cret.Death)
                                            {
                                                this.TargetCret = cret;
                                                OldTargetCret = this.TargetCret;
                                                this.SetTargetXY(this.TargetCret.CX, this.TargetCret.CY);
                                                this.TargetFocusTime = HUtil32.GetTickCount();
                                                TargetTime = HUtil32.GetTickCount();
                                            }
                                        }
                                    }
                                }
                                catch
                                {
                                    M2Share.MainOutMessage("[Exception] TGoldenImugi.AttackTarget fail target change 3");
                                }
                            }
                            result = true;
                        }
                    }
                    else
                    {
                        if (this.TargetCret.MapName == this.MapName)
                        {
                            if ((Math.Abs(this.CX - this.TargetCret.CX) <= this.ViewRange) && (Math.Abs(this.CY - this.TargetCret.CY) <= this.ViewRange))
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

        public override void Struck(TCreature hiter)
        {
            DontAttack = false;
        }

        public override void Die()
        {
            TCreature cret;
            int imugicount = 0;
            if (this.PEnvir != null)
            {
                for (var ix = 0; ix < this.PEnvir.MapWidth; ix++)
                {
                    for (var iy = 0; iy < this.PEnvir.MapHeight; iy++)
                    {
                        cret = (TCreature)this.PEnvir.GetCreature(ix, iy, true);
                        if (cret != null)
                        {
                            if ((!cret.Death) && (cret.RaceServer == Grobal2.RC_GOLDENIMUGI))
                            {
                                imugicount++;
                            }
                        }
                    }
                }
            }
            if (imugicount == 1)
            {
                this.BoNoItem = false;
            }
            base.Die();
        }
    }
}