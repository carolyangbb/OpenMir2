using System;
using System.Collections;
using SystemModule;

namespace GameSvr
{
    public class TPushedMon : TATMonster
    {
        private int DeathCount = 0;
        public int AttackWide = 0;

        public TPushedMon() : base()
        {
            this.Light = 3;
            this.SearchRate = 2500 + ((long)new System.Random(1500).Next());
            AttackWide = 1;
        }

        public override void Initialize()
        {
            this.PushedCount = 0;
            if (AttackWide == 1)
            {
                DeathCount = 5;
            }
            else
            {
                DeathCount = 7;
            }
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
            wide = AttackWide;
            this.Dir = M2Share.GetNextDirection(this.CX, this.CY, target.CX, target.CY);
            this.SendRefMsg(Grobal2.RM_LIGHTING, this.Dir, this.CX, this.CY, target.ActorId, "");
            TAbility _wvar1 = this.WAbil;
            pwr = this.GetAttackPower(_wvar1.Lobyte(_wvar1.DC), (short)HiByte(_wvar1.DC) - _wvar1.Lobyte(_wvar1.DC));
            if (pwr <= 0)
            {
                return;
            }
            rlist = new ArrayList();
            this.GetMapCreatures(this.PEnvir, this.CX, this.CY, wide, rlist);
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

        public override void Run()
        {
            if (!this.Death)
            {
                if (this.PushedCount >= DeathCount)
                {
                    // 磷澜
                    this.Die();
                }
            }
            base.Run();
        }

        public override void RunMsg(TMessageInfo msg)
        {
            switch (msg.Ident)
            {
                case Grobal2.RM_STRUCK:
                    this.WAbil.HP = this.WAbil.MaxHP;
                    return;
                    break;
                case Grobal2.RM_REFMESSAGE:
                    if (((int)msg.sender) == Grobal2.RM_STRUCK)
                    {
                        this.WAbil.HP = this.WAbil.MaxHP;
                        return;
                    }
                    break;
            }
            base.RunMsg(msg);
        }

        public override void Struck(TCreature hiter)
        {
            this.WAbil.HP = this.WAbil.MaxHP;
        }

        protected override bool AttackTarget()
        {
            bool result;
            byte targdir;
            int TargX;
            int TargY;
            bool Flag;
            result = false;
            if (this.TargetCret != null)
            {
                if (GetCurrentTime - this.HitTime > this.GetNextHitTime())
                {
                    this.HitTime = GetCurrentTime;
                    if (AttackWide == 1)
                    {
                        Flag = this.TargetInAttackRange(this.TargetCret, ref targdir);
                    }
                    else
                    {
                        Flag = this.TargetInSpitRange(this.TargetCret, ref targdir);
                    }
                    if (Flag)
                    {
                        Attack(this.TargetCret, targdir);
                    }
                    else
                    {
                        if (this.TargetCret.MapName == this.MapName)
                        {
                            if ((Math.Abs(this.CX - this.TargetCret.CX) <= 11) && (Math.Abs(this.CY - this.TargetCret.CY) <= 11))
                            {
                                TargX = new System.Random(2 * AttackWide + 1).Next() - AttackWide;
                                TargY = new System.Random(2 * AttackWide + 1).Next() - AttackWide;
                                if ((TargX < AttackWide) && (TargY < AttackWide))
                                {
                                    TargX = -AttackWide;
                                }
                                TargX = TargX + this.TargetCret.CX;
                                TargY = TargY + this.TargetCret.CY;
                                this.SetTargetXY(TargX, TargY);
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