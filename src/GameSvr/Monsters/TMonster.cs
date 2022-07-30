using System;
using SystemModule;

namespace GameSvr
{
    public class TMonster : TAnimal
    {
        private long thinktime = 0;
        protected bool RunDone = false;
        protected bool DupMode = false;

        public TMonster() : base()
        {
            DupMode = false;
            RunDone = false;
            thinktime  =  HUtil32.GetTickCount();
            this.ViewRange = 5;
            this.RunNextTick = 250;
            this.SearchRate = 3000 + ((long)new System.Random(2000).Next());
            this.SearchTime  =  HUtil32.GetTickCount();
            this.RaceServer = Grobal2.RC_MONSTER;
        }

        public TCreature MakeClone(string mname, TCreature src)
        {
            TCreature result = null;
            TCreature mon = svMain.UserEngine.AddCreatureSysop(src.PEnvir.MapName, src.CX, src.CY, mname);
            if (mon != null)
            {
                mon.Master = src.Master;
                mon.MasterRoyaltyTime = src.MasterRoyaltyTime;
                mon.SlaveMakeLevel = src.SlaveMakeLevel;
                mon.SlaveExpLevel = src.SlaveExpLevel;
                mon.RecalcAbilitys();
                mon.ChangeNameColor();
                if (src.Master != null)
                {
                    src.Master.SlaveList.Add(mon);
                }
                mon.WAbil = src.WAbil;
                //Move(src.StatusArr, mon.StatusArr, sizeof(short) * Grobal2.STATUSARR_SIZE);
                //Move(src.StatusValue, mon.StatusValue, sizeof(byte) * Grobal2.STATUSARR_SIZE);
                mon.TargetCret = src.TargetCret;
                mon.TargetFocusTime = src.TargetFocusTime;
                mon.LastHiter = src.LastHiter;
                mon.LastHitTime = src.LastHitTime;
                mon.Dir = src.Dir;
                result = mon;
            }
            return result;
        }

        public override void RunMsg(TMessageInfo msg)
        {
            base.RunMsg(msg);
        }

        public bool Think()
        {
            bool result;
            int oldx;
            int oldy;
            result = false;
            if (HUtil32.GetTickCount() - thinktime > 3000)
            {
                thinktime  =  HUtil32.GetTickCount();
                if (this.PEnvir.GetDupCount(this.CX, this.CY) >= 2)
                {
                    DupMode = true;
                }
                if (!this.IsProperTarget(this.TargetCret))
                {
                    this.TargetCret = null;
                }
            }
            if (DupMode && (!this.BoDontMove))
            {
                oldx = this.CX;
                oldy = this.CY;
                this.WalkTo((byte)new System.Random(8).Next(), false);
                if ((oldx != this.CX) || (oldy != this.CY))
                {
                    DupMode = false;
                    result = true;
                }
            }
            return result;
        }

        protected virtual bool AttackTarget()
        {
            byte targdir = 0;
            bool result = false;
            if (this.TargetCret != null)
            {
                if ((!this.TargetCret.Death) && this.IsProperTarget(this.TargetCret))
                {
                    if (this.TargetInAttackRange(this.TargetCret, ref targdir))
                    {
                        if (GetCurrentTime - this.HitTime > this.GetNextHitTime())
                        {
                            this.HitTime = GetCurrentTime;
                            this.TargetFocusTime  =  HUtil32.GetTickCount();
                            this.Attack(this.TargetCret, targdir);
                            this.BreakHolySeize();
                        }
                        result = true;
                    }
                    else
                    {
                        if (this.TargetCret.MapName == this.MapName)
                        {
                            this.SetTargetXY(this.TargetCret.CX, this.TargetCret.CY);
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

        public override void Run()
        {
            short bx = 0;
            short by = 0;
            if (!this.HideMode && !this.BoStoneMode && this.IsMoveAble())
            {
                if (Think())
                {
                    base.Run();
                    return;
                }
                if (this.BoWalkWaitMode)
                {
                    if (((int)GetTickCount - this.WalkWaitCurTime) > this.WalkWaitTime)
                    {
                        this.BoWalkWaitMode = false;
                    }
                }
                if (!this.BoWalkWaitMode && (GetCurrentTime - this.WalkTime > this.GetNextWalkTime()))
                {
                    this.WalkTime = GetCurrentTime;
                    this.WalkCurStep++;
                    if (this.WalkCurStep > this.WalkStep)
                    {
                        this.WalkCurStep = 0;
                        this.BoWalkWaitMode = true;
                        this.WalkWaitCurTime  =  HUtil32.GetTickCount();
                    }
                    if (!this.BoRunAwayMode)
                    {
                        if (!this.NoAttackMode)
                        {
                            if (this.TargetCret != null)
                            {
                                if (AttackTarget())
                                {
                                    if (this.Master != null)
                                    {
                                        // 傍拜吝俊 林牢捞 碍力肺 何福搁
                                        if (this.ForceMoveToMaster)
                                        {
                                            this.ForceMoveToMaster = false;
                                            M2Share.GetBackPosition(this.Master, ref bx, ref by);
                                            this.TargetX = bx;
                                            this.TargetY = by;
                                            this.SpaceMove(this.Master.PEnvir.MapName, this.TargetX, this.TargetY, 1);
                                        }
                                    }
                                    // ---------------------
                                    base.Run();
                                    return;
                                }
                            }
                            else
                            {
                                this.TargetX = -1;
                                if (this.BoHasMission)
                                {
                                    this.TargetX = (short)this.Mission_X;
                                    this.TargetY = (short)this.Mission_Y;
                                }
                            }
                        }
                        if (this.Master != null)
                        {
                            // 家券荐啊 傍拜 吝老 锭绰 恐 Master甫 牢侥且 荐 绝阑鳖? AttackTarget促澜俊 瞒窜登扁 锭巩...
                            if ((this.TargetCret == null) || this.BoLoseTargetMoment)
                            {
                                // 林牢捞 乐栏搁 林牢阑 蝶扼埃促.
                                this.BoLoseTargetMoment = false;
                                M2Share.GetBackPosition(this.Master, ref bx, ref by);
                                // 林牢狼 第肺 皑
                                if ((Math.Abs(this.TargetX - bx) > 1) || (Math.Abs(this.TargetY - bx) > 1))
                                {
                                    this.TargetX = bx;
                                    this.TargetY = by;
                                    if ((Math.Abs(this.CX - bx) <= 2) && (Math.Abs(this.CY - by) <= 2))
                                    {
                                        if (this.PEnvir.GetCreature(bx, by, true) != null)
                                        {
                                            this.TargetX = this.CX;
                                            // 歹 捞惑 框流捞瘤 臼绰促.
                                            this.TargetY = this.CY;
                                        }
                                    }
                                }
                            }
                            // 林牢苞 呈公 冻绢廉 乐栏搁...
                            if (this.ForceMoveToMaster || ((!this.Master.BoSlaveRelax) && ((this.PEnvir != this.Master.PEnvir) || (Math.Abs(this.CX - this.Master.CX) > 20) || (Math.Abs(this.CY - this.Master.CY) > 20))))
                            {
                                this.ForceMoveToMaster = false;
                                // -------------(sonmg 2004/12/24)
                                M2Share.GetBackPosition(this.Master, ref bx, ref by);
                                // 林牢狼 第肺 皑
                                this.TargetX = bx;
                                this.TargetY = by;
                                // -------------
                                this.SpaceMove(this.Master.PEnvir.MapName, this.TargetX, this.TargetY, 1);
                            }
                        }
                    }
                    else
                    {
                        // 档噶啊绰 葛靛捞搁 TargetX, TargetY肺 档噶皑...
                        if (this.RunAwayTime > 0)
                        {
                            // 矫埃 力茄捞 乐澜
                            if (HUtil32.GetTickCount() - this.RunAwayStart > RunAwayTime)
                            {
                                this.BoRunAwayMode = false;
                                this.RunAwayTime = 0;
                            }
                        }
                    }
                    if (this.Master != null)
                    {
                        if (this.Master.BoSlaveRelax)
                        {
                            // 林牢捞 绒侥窍扼绊 窃...
                            base.Run();
                            return;
                        }
                    }
                    if (this.TargetX != -1)
                    {
                        // 啊具且 镑捞 乐澜
                        this.GotoTargetXY();
                    }
                    else
                    {
                        // 2003/03/18 矫具郴俊 酒公档 绝栏搁 硅雀窍瘤 臼澜
                        if ((this.TargetCret == null) && ((this.RefObjCount > 0) || this.HideMode))
                        {
                            // if (TargetCret = nil) then
                            this.Wondering();
                        }
                        // 硅雀窃
                    }
                }
            }
            base.Run();
        }

        public override void RecalcAbilitys()
        {
            bool[] cghi = new bool[3 + 1];
            TAbility temp;
            bool oldhmode;
            this.AddAbil = new TAddAbility();//FillChar(this.AddAbil, sizeof(TAddAbility), 0);
            temp = this.WAbil;
            this.WAbil = this.Abil;
            this.WAbil.HP = temp.HP;
            this.WAbil.MP = temp.MP;
            this.WAbil.Weight = 0;
            this.WAbil.WearWeight = 0;
            this.WAbil.HandWeight = 0;
            this.AntiPoison = 0;
            // 扁夯 2%(sonmg)
            this.PoisonRecover = 0;
            this.HealthRecover = 0;
            this.SpellRecover = 0;
            this.AntiMagic = 1;
            // 扁夯 10% => 2%
            this.Luck = 0;
            this.HitSpeed = 0;
            oldhmode = this.BoHumHideMode;
            this.BoHumHideMode = false;
            // 漂荐茄 瓷仿
            this.BoAbilSpaceMove = false;
            this.BoAbilMakeStone = false;
            this.BoAbilRevival = false;
            this.BoAddMagicFireball = false;
            this.BoAddMagicHealing = false;
            this.BoAbilAngerEnergy = false;
            this.BoMagicShield = false;
            this.BoAbilSuperStrength = false;
            this.BoFastTraining = false;
            this.BoAbilSearch = false;
            if (this.BoFixedHideMode && (this.StatusArr[Grobal2.STATE_TRANSPARENT] > 0))
            {
                // 篮脚贱
                this.BoHumHideMode = true;
            }
            if (this.BoHumHideMode)
            {
                if (!oldhmode)
                {
                    this.CharStatus = this.GetCharStatus();
                    this.CharStatusChanged();
                }
            }
            else
            {
                if (oldhmode)
                {
                    this.StatusArr[Grobal2.STATE_TRANSPARENT] = 0;
                    this.CharStatus = this.GetCharStatus();
                    this.CharStatusChanged();
                }
            }
            this.RecalcHitSpeed();
            this.SpeedPoint = (byte)(this.SpeedPoint + this.AddAbil.SPEED);
            this.AccuracyPoint = (byte)(this.AccuracyPoint + this.AddAbil.HIT);
            this.AntiPoison = (byte)(this.AntiPoison + this.AddAbil.AntiPoison);
            this.PoisonRecover = (byte)(this.PoisonRecover + this.AddAbil.PoisonRecover);
            this.HealthRecover = (byte)(this.HealthRecover + this.AddAbil.HealthRecover);
            this.SpellRecover = (byte)(this.SpellRecover + this.AddAbil.SpellRecover);
            this.AntiMagic = (byte)(this.AntiMagic + this.AddAbil.AntiMagic);
            this.Luck = this.Luck + this.AddAbil.Luck;
            this.Luck = this.Luck - this.AddAbil.UnLuck;
            this.HitSpeed = this.AddAbil.HitSpeed;
            this.WAbil.MaxHP = (ushort)(this.Abil.MaxHP + this.AddAbil.HP);
            this.WAbil.MaxMP = (ushort)(this.Abil.MaxMP + this.AddAbil.MP);
            this.WAbil.AC = MakeWord(LoByte(this.AddAbil.AC) + LoByte(this.Abil.AC), HiByte(this.AddAbil.AC) + HiByte(this.Abil.AC));
            this.WAbil.MAC = MakeWord(LoByte(this.AddAbil.MAC) + LoByte(this.Abil.MAC), HiByte(this.AddAbil.MAC) + HiByte(this.Abil.MAC));
            this.WAbil.DC = MakeWord(LoByte(this.AddAbil.DC) + LoByte(this.Abil.DC), HiByte(this.AddAbil.DC) + HiByte(this.Abil.DC));
            this.WAbil.MC = MakeWord(LoByte(this.AddAbil.MC) + LoByte(this.Abil.MC), HiByte(this.AddAbil.MC) + HiByte(this.Abil.MC));
            this.WAbil.SC = MakeWord(LoByte(this.AddAbil.SC) + LoByte(this.Abil.SC), HiByte(this.AddAbil.SC) + HiByte(this.Abil.SC));
            if (this.StatusArr[Grobal2.STATE_DEFENCEUP] > 0)
            {
                this.WAbil.AC = MakeWord(LoByte(this.WAbil.AC), _MIN(255, HiByte(this.WAbil.AC) + (this.Abil.Level / 7) + this.StatusValue[Grobal2.STATE_DEFENCEUP]));
            }
            if (this.StatusArr[Grobal2.STATE_MAGDEFENCEUP] > 0)
            {
                this.WAbil.MAC = MakeWord(LoByte(this.WAbil.MAC), _MIN(255, HiByte(this.WAbil.MAC) + (this.Abil.Level / 7) + this.StatusValue[Grobal2.STATE_MAGDEFENCEUP]));
            }
            if (this.ExtraAbil[Grobal2.EABIL_DCUP] > 0)
            {
                this.WAbil.DC = MakeWord(LoByte(this.WAbil.DC), HiByte(this.WAbil.DC) + this.ExtraAbil[Grobal2.EABIL_DCUP]);
            }
            if (this.ExtraAbil[Grobal2.EABIL_MCUP] > 0)
            {
                this.WAbil.MC = MakeWord(LoByte(this.WAbil.MC), HiByte(this.WAbil.MC) + this.ExtraAbil[Grobal2.EABIL_MCUP]);
            }
            if (this.ExtraAbil[Grobal2.EABIL_SCUP] > 0)
            {
                this.WAbil.SC = MakeWord(LoByte(this.WAbil.SC), HiByte(this.WAbil.SC) + this.ExtraAbil[Grobal2.EABIL_SCUP]);
            }
            if (this.ExtraAbil[Grobal2.EABIL_HITSPEEDUP] > 0)
            {
                this.HitSpeed = (ushort)(this.HitSpeed + this.ExtraAbil[Grobal2.EABIL_HITSPEEDUP]);
            }
            if (this.ExtraAbil[Grobal2.EABIL_HPUP] > 0)
            {
                this.WAbil.MaxHP = (ushort)(this.WAbil.MaxHP + this.ExtraAbil[Grobal2.EABIL_HPUP]);
            }
            if (this.ExtraAbil[Grobal2.EABIL_MPUP] > 0)
            {
                this.WAbil.MaxMP = (ushort)(this.WAbil.MaxMP + this.ExtraAbil[Grobal2.EABIL_MPUP]);
            }
            if (this.RaceServer >= Grobal2.RC_ANIMAL)
            {
                this.ApplySlaveLevelAbilitys();
            }
        }
    } 
}