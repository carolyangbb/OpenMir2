using SystemModule;

namespace GameSvr
{
    public class TGuardUnit : TAnimal
    {
        public int OriginX = 0;
        public int OriginY = 0;
        public int OriginDir = 0;

        public override void Struck(TCreature hiter)
        {
            base.Struck(hiter);
            if (this.Castle != null)
            {
                hiter.BoCrimeforCastle = true;
                hiter.CrimeforCastleTime = HUtil32.GetTickCount();
            }
        }

        public override bool IsProperTarget(TCreature target)
        {
            bool result;
            result = false;
            if (this.Castle != null)
            {
                if (this.LastHiter == target)
                {
                    result = true;
                }
                if (target.BoCrimeforCastle)
                {
                    if (HUtil32.GetTickCount() - target.CrimeforCastleTime < 2 * 60 * 1000)
                    {
                        result = true;
                    }
                    else
                    {
                        target.BoCrimeforCastle = false;
                    }
                    if (target.Castle != null)
                    {
                        target.BoCrimeforCastle = false;
                        result = false;
                    }
                }
                if (((TUserCastle)this.Castle).BoCastleUnderAttack)
                {
                    result = true;
                }
                if (((TUserCastle)this.Castle).OwnerGuild != null)
                {
                    if (target.Master == null)
                    {
                        if (((((TUserCastle)this.Castle).OwnerGuild == target.MyGuild) || ((TUserCastle)this.Castle).OwnerGuild.IsAllyGuild(target.MyGuild)) && (this.LastHiter != target))
                        {
                            result = false;
                        }
                    }
                    else
                    {
                        if (((((TUserCastle)this.Castle).OwnerGuild == target.Master.MyGuild) || ((TUserCastle)this.Castle).OwnerGuild.IsAllyGuild(target.Master.MyGuild)) && (this.LastHiter != target.Master) && (this.LastHiter != target))
                        {
                            result = false;
                        }
                    }
                }
                if (target.BoSysopMode || target.BoStoneMode || (target.RaceServer >= Grobal2.RC_NPC) && (target.RaceServer < Grobal2.RC_ANIMAL) || (target == this) || (target.Castle == this.Castle))
                {
                    result = false;
                }
            }
            else
            {
                if (this.LastHiter == target)
                {
                    result = true;
                }
                if (target.TargetCret != null)
                {
                    if (target.TargetCret.RaceServer == Grobal2.RC_ARCHERGUARD)
                    {
                        result = true;
                    }
                }
                if (target.PKLevel() >= 2)
                {
                    result = true;
                }
                if (target.BoSysopMode || target.BoStoneMode || (target == this))
                {
                    result = false;
                }
            }
            return result;
        }
    }
}