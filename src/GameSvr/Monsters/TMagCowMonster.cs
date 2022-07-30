using SystemModule;

namespace GameSvr
{
    public class TMagCowMonster : TATMonster
    {
        // TMagCowMonster   付过筋绰 快搁蓖
        //Constructor  Create()
        public TMagCowMonster() : base()
        {
            this.SearchRate = 1500 + ((long)new System.Random(1500).Next());
        }
        public void MagicAttack(byte dir)
        {
            int dam;
            TCreature cret;
            this.Dir = dir;
            TAbility _wvar1 = this.WAbil;
            dam = _wvar1.Lobyte(_wvar1.DC) + new System.Random((short)HiByte(_wvar1.DC) - _wvar1.Lobyte(_wvar1.DC) + 1).Next();
            if (dam <= 0)
            {
                return;
            }
            this.SendRefMsg(Grobal2.RM_HIT, this.Dir, this.CX, this.CY, 0, "");
            cret = this.GetFrontCret();
            if (cret != null)
            {
                if (this.IsProperTarget(cret))
                {
                    // .RaceServer = RC_USERHUMAN then begin //荤恩父 傍拜窃
                    // 嘎绰瘤 搬沥 (付过 雀乔肺 搬沥)
                    if (cret.AntiMagic <= new System.Random(50).Next())
                    {
                        // 付过规绢仿俊 瓤苞 乐澜.
                        // armor := (Lobyte(cret.WAbil.MAC) + Random(ShortInt(HiByte(cret.WAbil.MAC)-Lobyte(cret.WAbil.MAC)) + 1));
                        // dam := dam - armor;
                        // if dam <= 0 then
                        // if dam > -10 then dam := 1;
                        dam = cret.GetMagStruckDamage(this, dam);
                        if (dam > 0)
                        {
                            cret.StruckDamage(dam, this);
                            // wparam
                            // lparam1
                            // lparam2
                            // hiter
                            cret.SendDelayMsg((TCreature)Grobal2.RM_STRUCK, Grobal2.RM_REFMESSAGE, dam, cret.WAbil.HP, cret.WAbil.MaxHP, this.ActorId, "", 300);
                        }
                    }
                }
            }
        }

        protected override bool AttackTarget()
        {
            bool result;
            byte targdir;
            result = false;
            if (this.TargetCret != null)
            {
                if (this.TargetInAttackRange(this.TargetCret, ref targdir))
                {
                    if (GetCurrentTime - this.HitTime > this.GetNextHitTime())
                    {
                        this.HitTime = GetCurrentTime;
                        this.TargetFocusTime = GetTickCount;
                        MagicAttack(targdir);
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
                    // <!!林狼> TargetCret := nil肺 官柴
                }
            }
            return result;
        }

    }
}