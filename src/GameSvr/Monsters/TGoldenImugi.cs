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
        // 炔陛捞公扁(何锋陛荤) =====================================================================
        //Constructor  Create()
        public TGoldenImugi() : base()
        {
            this.ViewRange = 12;
            TwinGenDelay = 100;
            // 3檬窜困
            sectick  =  HUtil32.GetTickCount();
            // DontBagItemDrop := TRUE;
            // DontBagGoldDrop := TRUE;
            this.BoNoItem = true;
            FirstCheck = true;
            DontAttack = true;
            DontAttackCheck = false;
            AttackState = false;
            InitialState = false;
            ChildMobRecalled = false;
            FinalWarp = false;
            RevivalTime  =  HUtil32.GetTickCount();
            WarpTime  =  HUtil32.GetTickCount();
            TargetTime  =  HUtil32.GetTickCount();
            RangeAttackTime  =  HUtil32.GetTickCount();
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
            int nx=0;
            int ny=0;
            TCreature cret;
            cret = null;
            int snakecount = 0;
            if (HUtil32.GetTickCount() - sectick > 3000)
            {
                this.BreakHolySeize();
                int imugicount = 0;
                sectick  =  HUtil32.GetTickCount();
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
                                    // 捞 何盒篮 滴锅掳 捞公扁父 积阿窍绰 何盒.
                                    if ((imugicount == 2) && (cret != this))
                                    {
                                        // 老沥 裹困 捞惑 冻绢廉 乐栏搁 娄捞公扁 磊府肺 捞悼茄促.
                                        if ((Math.Abs(cret.CX - this.CX) >= 10) || (Math.Abs(cret.CY - this.CY) >= 10))
                                        {
                                            // 郴啊 WarpTime捞 坷贰灯栏搁 郴啊 况橇茄促.
                                            if (this.WarpTime < (cret as TGoldenImugi).WarpTime)
                                            {
                                                // 况橇 NormalEffect
                                                this.SendRefMsg(Grobal2.RM_NORMALEFFECT, 0, this.CX, this.CY, Grobal2.NE_SN_MOVEHIDE, "");
                                                this.SpaceMove(cret.PEnvir.MapName, cret.CX, cret.CY, 0);
                                                WarpTime  =  HUtil32.GetTickCount();
                                                this.SendRefMsg(Grobal2.RM_NORMALEFFECT, 0, this.CX, this.CY, Grobal2.NE_SN_MOVESHOW, "");
                                            }
                                            else
                                            {
                                                cret.SendRefMsg(Grobal2.RM_NORMALEFFECT, 0, cret.CX, cret.CY, Grobal2.NE_SN_MOVEHIDE, "");
                                                cret.SpaceMove(this.PEnvir.MapName, this.CX, this.CY, 0);
                                                (cret as TGoldenImugi).WarpTime  =  HUtil32.GetTickCount();
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
                                if ((!cret.Death) && (cret.UserName == svMain.__WhiteSnake))
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
                        cret = svMain.UserEngine.AddCreatureSysop(this.PEnvir.MapName, _MIN(this.CX + 2, this.PEnvir.MapWidth - 1), this.CY, svMain.__GoldenImugi);
                        if (cret != null)
                        {
                            if (!DontAttack)
                            {
                                svMain.UserEngine.CryCry(Grobal2.RM_CRY, this.PEnvir, this.CX, this.CY, 10000, " -" + svMain.__GoldenImugi + " has recalled its clone.");
                            }
                            RevivalTime  =  HUtil32.GetTickCount();
                            this.SendRefMsg(Grobal2.RM_LIGHTING, this.Dir, this.CX, this.CY, this.ActorId, "");
                            cret.SendRefMsg(Grobal2.RM_NORMALEFFECT, 0, cret.CX, cret.CY, Grobal2.NE_SN_RELIVE, "");
                            if (!DontAttack)
                            {
                                cret.WAbil.HP = (ushort)(cret.WAbil.MaxHP / 3 * 2);
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
                    svMain.UserEngine.AddCreatureSysop(this.PEnvir.MapName, nx, ny, svMain.__WhiteSnake);
                    svMain.UserEngine.AddCreatureSysop(this.PEnvir.MapName, nx, ny, svMain.__WhiteSnake);
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
                    WarpTime  =  HUtil32.GetTickCount();
                    this.SendRefMsg(Grobal2.RM_NORMALEFFECT, 0, this.CX, this.CY, Grobal2.NE_SN_MOVESHOW, "");
                    FinalWarp = true;
                }
            }
            base.Run();
        }

        public override void Attack(TCreature targ, byte dir)
        {
            int i;
            int k;
            int mx;
            int my;
            int dam;
            TCreature cret;
            int pwr;
            // targ绰 静捞瘤 臼澜
            this.Dir = dir;
            TAbility _wvar1 = this.WAbil;
            dam = HUtil32.LoByte(_wvar1.DC) + new System.Random(HiByte(_wvar1.DC) - HUtil32.LoByte(_wvar1.DC) + 1).Next();
            if (dam <= 0)
            {
                return;
            }
            this.SendRefMsg(Grobal2.RM_HIT, this.Dir, this.CX, this.CY, 0, "");
            TAbility _wvar2 = this.WAbil;
            pwr = this.GetAttackPower(HUtil32.LoByte(_wvar2.DC), (short)HiByte(_wvar2.DC) - HUtil32.LoByte(_wvar2.DC));
            for (i = 0; i <= 4; i++)
            {
                for (k = 0; k <= 4; k++)
                {
                    if (M2Share.SpitMap[dir, i, k] == 1)
                    {
                        mx = this.CX - 2 + k;
                        my = this.CY - 2 + i;
                        cret = (TCreature)this.PEnvir.GetCreature(mx, my, true);
                        if ((cret != null) && (cret != this))
                        {
                            if (this.IsProperTarget(cret))
                            {
                                // cret.RaceServer = RC_USERHUMAN then begin
                                // 嘎绰瘤 搬沥
                                if (new System.Random(cret.SpeedPoint).Next() < this.AccuracyPoint)
                                {
                                    // inherited
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
            // 馆靛矫 target <> nil
            int i;
            int pwr;
            int dam;
            short sx=0;
            short sy=0;
            short tx=0;
            short ty =0;
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
                for (i = 0; i < list.Count; i++)
                {
                    cret = (TCreature)list[i];
                    if (this.IsProperTarget(cret))
                    {
                        dam = cret.GetMagStruckDamage(this, pwr);
                        if (dam > 0)
                        {
                            cret.StruckDamage(dam, this);
                            cret.SendDelayMsg(Grobal2.RM_STRUCK, Grobal2.RM_REFMESSAGE, dam, cret.WAbil.HP, cret.WAbil.MaxHP, this.ActorId, "", 800);
                        }
                    }
                }
                list.Free();
            }
        }

        public void RangeAttack2(TCreature targ)
        {
            // 馆靛矫 target <> nil
            int i;
            TCreature cret;
            if (targ == null)
            {
                return;
            }
            // 刀救俺 NormalEffect
            this.SendRefMsg(Grobal2.RM_LIGHTING, this.Dir, this.CX, this.CY, this.ActorId, "");
            this.SendRefMsg(Grobal2.RM_NORMALEFFECT, 0, this.CX, this.CY, Grobal2.NE_POISONFOG, "");
            // 矫具郴 葛电 某腐/家券各 吝刀
            for (i = 0; i < this.VisibleActors.Count; i++)
            {
                cret = (TCreature)this.VisibleActors[i].cret;
                if ((!cret.Death) && this.IsProperTarget(cret))
                {
                    if ((cret.RaceServer == Grobal2.RC_USERHUMAN) || (cret.Master != null))
                    {
                        // 规绢仿捞 皑家窍绰 刀俊 吝刀 等促.
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
            bool result;
            byte targdir=0;
            TCreature cret;
            result = false;
            cret = null;
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
                            this.TargetFocusTime  =  HUtil32.GetTickCount();
                            this.Dir = M2Share.GetNextDirection(this.CX, this.CY, this.TargetCret.CX, this.TargetCret.CY);
                            Attack(this.TargetCret, targdir);
                            // UserEngine.CryCry (RM_CRY, PEnvir, CX, CY, 10000, ' Attack : ' + TargetCret.UserName);//test
                            result = true;
                        }
                        else
                        {
                            if (GetCurrentTime < (8000 + TargetTime))
                            {
                                this.TargetFocusTime  =  HUtil32.GetTickCount();
                                if ((GetCurrentTime < (30000 + RangeAttackTime)) && (new System.Random(10).Next() < 8))
                                {
                                    RangeAttack(this.TargetCret);
                                    // UserEngine.CryCry (RM_CRY, PEnvir, CX, CY, 10000, ' RangeAttack : ' + TargetCret.UserName);//test
                                }
                                else
                                {
                                    RangeAttack2(this.TargetCret);
                                    RangeAttackTime  =  HUtil32.GetTickCount();
                                    // UserEngine.CryCry (RM_CRY, PEnvir, CX, CY, 10000, ' RangeAttack (2)');//test
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
                                                // UserEngine.CryCry (RM_CRY, PEnvir, CX, CY, 10000, ' Targeting : ' + TargetCret.UserName);//test
                                                this.SetTargetXY(this.TargetCret.CX, this.TargetCret.CY);
                                                this.TargetFocusTime  =  HUtil32.GetTickCount();
                                                TargetTime  =  HUtil32.GetTickCount();
                                            }
                                        }
                                    }
                                }
                                catch
                                {
                                    svMain.MainOutMessage("[Exception] TGoldenImugi.AttackTarget fail target change 3");
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
                            // <!!林狼> TargetCret := nil肺 官柴
                        }
                    }
                }
            }
            return result;
        }

        public override void Struck(TCreature hiter)
        {
            // 嘎栏搁 傍拜葛靛肺 函版
            DontAttack = false;
        }

        public override void Die()
        {
            int ix;
            int iy;
            TCreature cret;
            int imugicount;
            imugicount = 0;
            // 郴啊 付瘤阜 捞公扁捞搁 酒捞袍阑 冻焙促.
            if (this.PEnvir != null)
            {
                for (ix = 0; ix < this.PEnvir.MapWidth; ix++)
                {
                    for (iy = 0; iy < this.PEnvir.MapHeight; iy++)
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