using System;
using System.Collections;
using SystemModule;

namespace GameSvr
{
    public class TDragonStatue : TATMonster
    {
        private bool bofirst = false;

        public TDragonStatue() : base()
        {
            bofirst = true;
            this.HideMode = true;
            this.RaceServer = Grobal2.RC_DRAGONSTATUE;
            this.ViewRange = 12;
            this.BoWalkWaitMode = true;
            this.BoDontMove = true;
        }
        
        public override void RecalcAbilitys()
        {
            base.RecalcAbilitys();
            ResetLevel();
        }

        protected void ResetLevel()
        {
            
        }

        public int RangeAttack_MPow(TUserMagic pum)
        {
            return pum.pDef.MinPower + new System.Random(pum.pDef.MaxPower - pum.pDef.MinPower).Next();
        }

        protected void RangeAttack(TCreature targ)
        {
            int i;
            int dam;
            TCreature cret;
            ArrayList list;
            if (targ == null)
            {
                return;
            }
            this.SendRefMsg(Grobal2.RM_LIGHTING, this.Dir, this.CX, this.CY, targ.ActorId, "");
            TAbility _wvar1 = this.WAbil;
            var pwr = new System.Random(HiByte(this.WAbil.DC)).Next() + HUtil32.LoByte(this.WAbil.DC) + new System.Random(HUtil32.LoByte(this.WAbil.MC)).Next();
            var ixf = _MAX(0, targ.CX - 2);
            var ixt = _MIN(this.PEnvir.MapWidth - 1, targ.CX + 2);
            var iyf = _MAX(0, targ.CY - 2);
            var iyt = _MIN(this.PEnvir.MapHeight - 1, targ.CY + 2);
            for (var ix = ixf; ix <= ixt; ix++)
            {
                for (var iy = iyf; iy <= iyt; iy++)
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
                                cret.SendDelayMsg(Grobal2.RM_STRUCK, Grobal2.RM_REFMESSAGE, (ushort)dam, cret.WAbil.HP, cret.WAbil.MaxHP, this.ActorId, "", 600 + _MAX(Math.Abs(this.CX - cret.CX), Math.Abs(this.CY - cret.CY)) * 50);
                            }
                        }
                    }
                    list.Free();
                }
            }
        }

        protected override bool AttackTarget()
        {
            bool result = false;
            if ((this.TargetCret != null) && (this.TargetCret != this.Master))
            {
                if (GetCurrentTime - this.HitTime > this.GetNextHitTime())
                {
                    this.HitTime = GetCurrentTime;
                    if ((Math.Abs(this.CX - this.TargetCret.CX) <= this.ViewRange) && (Math.Abs(this.CY - this.TargetCret.CY) <= this.ViewRange) && (!this.TargetCret.Death) && (!this.TargetCret.BoGhost))
                    {
                        RangeAttack(this.TargetCret);
                        result = true;
                    }
                    this.LoseTarget();
                }
            }
            return result;
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