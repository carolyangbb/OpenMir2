using System;
using SystemModule;

namespace GameSvr
{
    public class TCloneMon : TATMonster
    {
        private bool bofirst = false;
        private long NextMPSpendTime = 0;
        private long MPSpendTickTime = 0;

        public TCloneMon() : base()
        {
            bofirst = true;
            this.HideMode = false;
            this.RaceServer = Grobal2.RC_CLONE;
            this.ViewRange = 10;
        }
        
        public override void RecalcAbilitys()
        {
            BeforeRecalcAbility();
            base.RecalcAbilitys();
        }

        protected void BeforeRecalcAbility()
        {
            switch (this.SlaveMakeLevel)
            {
                case 1:
                    this.Abil.MC = MakeWord(10, 22);
                    break;
                case 2:
                    this.Abil.MC = MakeWord(13, 25);
                    break;
                case 3:
                    this.Abil.MC = MakeWord(15, 30);
                    break;
                default:
                    this.Abil.MC = MakeWord(9, 20);
                    break;
            }
            this.AddAbil.HP = 0;
        }

        protected void AfterRecalcAbility()
        {
            this.NextHitTime = 3300 - (this.SlaveMakeLevel * 300);
            this.NextWalkTime = 500;
            this.WalkTime = GetCurrentTime + 2000;
            NextMPSpendTime  =  HUtil32.GetTickCount();
            MPSpendTickTime = 600 * 30;
            if (this.Master != null)
            {
                this.WAbil.MaxHP = this.Master.WAbil.MaxHP;
                this.WAbil.HP = this.Master.WAbil.HP;
                this.WAbil.AC = MakeWord(LoByte(this.Master.Abil.AC) * 2 / 3, HiByte(this.Master.Abil.AC) * 2 / 3);
                this.WAbil.MAC = MakeWord(LoByte(this.Master.Abil.MAC) * 2 / 3, HiByte(this.Master.Abil.MAC) * 2 / 3);
            }
        }

        protected void ResetLevel()
        {
            // 贸澜父檬扁拳登绰何盒...
        }

        // 馆靛矫 target <> nil
        public int RangeAttackTo_GetPower1(int power, int trainrate)
        {
            return HUtil32.MathRound((10 + trainrate * 0.9) * (power / 100));
        }

        public int RangeAttackTo_CalcMagicPow()
        {
            int result;
            result = 8 + new System.Random(20).Next();
            return result;
        }

        protected void RangeAttackTo(TCreature targ)
        {
            int pwr;
            if (targ == null)
            {
                return;
            }
            if (this.IsProperTarget(targ))
            {
                if (targ.AntiMagic <= new System.Random(50).Next())
                {
                    pwr = this.GetAttackPower(RangeAttackTo_GetPower1(RangeAttackTo_CalcMagicPow(), 0) + LoByte(this.WAbil.MC), HiByte(this.WAbil.MC) - LoByte(this.WAbil.MC) + 1);
                    if (targ.LifeAttrib == Grobal2.LA_UNDEAD)
                    {
                        pwr = HUtil32.MathRound(pwr * 1.5);
                    }
                    this.SendDelayMsg(this, Grobal2.RM_DELAYMAGIC, (ushort)pwr, HUtil32.MakeLong(targ.CX, targ.CY), 2, targ.ActorId, "", 600);
                    this.SendRefMsg(Grobal2.RM_MAGICFIRE, 0, MakeWord(7, 9), HUtil32.MakeLong(targ.CX, targ.CY), targ.ActorId, "");
                }
            }
        }

        protected override bool AttackTarget()
        {
            bool result = false;
            if ((this.TargetCret != null) && (this.Master != null) && (this.TargetCret != this.Master))
            {
                if (GetCurrentTime - this.HitTime > this.GetNextHitTime())
                {
                    this.HitTime = GetCurrentTime;
                    if ((Math.Abs(this.CX - this.TargetCret.CX) <= this.ViewRange) && (Math.Abs(this.CY - this.TargetCret.CY) <= this.ViewRange) && (!this.TargetCret.Death))
                    {
                        if (this.IsProperTarget(this.TargetCret))
                        {
                            // 付过 霖厚悼累阑 刚历 焊晨
                            this.SendRefMsg(Grobal2.RM_SPELL, 9, this.TargetCret.CX, this.TargetCret.CY, 11, "");
                            // 碍拜捞固瘤
                            RangeAttackTo(this.TargetCret);
                            result = true;
                        }
                    }
                }
            }
            this.BoLoseTargetMoment = true;
            return result;
        }

        public override void Run()
        {
            int plus;
            int finalplus;
            plus = 0;
            try
            {
                if (bofirst)
                {
                    bofirst = false;
                    this.Dir = 5;
                    this.HideMode = false;
                    this.SendRefMsg(Grobal2.RM_DIGUP, this.Dir, this.CX, this.CY, 0, "");
                    RecalcAbilitys();
                    AfterRecalcAbility();
                    // 瘤盔 付过矫 掉饭捞 滚弊 荐沥
                    ResetLevel();
                }
                if (this.Death)
                {
                    // 盒脚篮 矫眉啊 绝促.
                    if (HUtil32.GetTickCount() - this.DeathTime > 1500)
                    {
                        this.MakeGhost(8);
                    }
                }
                else
                {
                    if ((!this.BoDisapear) && (!this.BoGhost) && (this.WAbil.HP > 0) && (this.Master != null) && (!this.Master.BoGhost) && (!this.Master.Death))
                    {
                        // 付仿捞 雀汗登瘤 臼霸 茄促.(sonmg 2005/02/15)
                        this.Master.SpellTick = 0;
                        // 盒脚俊霸 林牢狼 眉仿阑 焊碍秦霖促.(sonmg 2005/03/09)
                        this.WAbil.HP = this.Master.WAbil.HP;
                        if (this.Master.WAbil.MP < 200)
                        {
                            // MP 啊 200 焊促 累栏搁 磊悼荤扼咙
                            this.Master.SysMsg("Your clone is destroyed due to lack of MP.", 0);
                            this.BoDisapear = true;
                            this.WAbil.HP = 0;
                        }
                        if (HUtil32.GetTickCount() >= NextMPSpendTime + MPSpendTickTime)
                        {
                            NextMPSpendTime  =  HUtil32.GetTickCount();
                            // 170
                            if (this.Master.WAbil.MP >= 200)
                            {
                                // Self.WAbil.HP := Master.WAbil.HP;
                                // Master.WAbil.MP := Master.WAbil.MP - ( 1 + SlaveMakeLevel div 2 );
                                // ----------------------------------------------------------
                                // 盒脚捞 家券登菌阑 版快 付仿 瞒绰 傍侥 喊档 利侩
                                // 某腐狼 付仿 傍侥捞 函版登搁 捞 何盒档 函版登绢具 窃.
                                plus = this.Master.WAbil.MaxMP / 18 + 1;
                                finalplus = -((1 + this.SlaveMakeLevel / 2) * 64) + plus + (plus * this.Master.SpellRecover / 10);
                                // 歹秦瘤绰 蔼捞 剧荐牢瘤 澜荐牢瘤俊 蝶扼 备盒
                                if (finalplus >= 0)
                                {
                                    if (this.Master.WAbil.MP + finalplus > this.Master.WAbil.MaxMP)
                                    {
                                        this.Master.WAbil.MP = this.Master.WAbil.MaxMP;
                                    }
                                    else
                                    {
                                        this.Master.WAbil.MP = (ushort)(this.Master.WAbil.MP + finalplus);
                                    }
                                }
                                else
                                {
                                    if (this.Master.WAbil.MP < -finalplus)
                                    {
                                        this.Master.WAbil.MP = 0;
                                    }
                                    else
                                    {
                                        this.Master.WAbil.MP = (ushort)(this.Master.WAbil.MP + finalplus);
                                    }
                                }
                            }
                            this.Master.HealthSpellChanged();
                        }
                    }
                    else
                    {
                        this.WAbil.HP = 0;
                    }
                }
                base.Run();
            }
            catch
            {
                svMain.MainOutMessage("EXCEPT TCLONE");
            }
        }
    } 
}

