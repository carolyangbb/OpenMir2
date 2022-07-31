using SystemModule;

namespace GameSvr
{
    public class TSpitSpider : TATMonster
    {
        public bool BoUsePoison = false;
        // ---------------------------------------------------------------------------
        // TSpitSpider (魔柜绰 芭固)
        //Constructor  Create()
        public TSpitSpider() : base()
        {
            this.SearchRate = 1500 + ((long)new System.Random(1500).Next());
            this.BoAnimal = true;
            // 戒搁 魔芭固捞弧捞 唱咳
            BoUsePoison = true;
        }
        // 魔柜绰 阁胶磐狼 傍拜
        // 阁胶磐父 荤侩窃
        public void SpitAttack(byte dir)
        {
            int i;
            int k;
            int mx;
            int my;
            int dam;
            TCreature cret;
            this.Dir = dir;
            TAbility _wvar1 = this.WAbil;
            dam = HUtil32.LoByte(_wvar1.DC) + new System.Random(HiByte(_wvar1.DC) - HUtil32.LoByte(_wvar1.DC) + 1).Next();
            if (dam <= 0)
            {
                return;
            }
            this.SendRefMsg(Grobal2.RM_HIT, this.Dir, this.CX, this.CY, 0, "");
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
                                    // 魔芭固 魔篮 付过规绢仿俊 瓤苞 乐澜.
                                    // armor := (LoByte(cret.WAbil.MAC) + Random(ShortInt(HiByte(cret.WAbil.MAC)-LoByte(cret.WAbil.MAC)) + 1));
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
                                        cret.SendDelayMsg(Grobal2.RM_STRUCK, Grobal2.RM_REFMESSAGE, (ushort)dam, cret.WAbil.HP, cret.WAbil.MaxHP, this.ActorId, "", 300);
                                        if (BoUsePoison)
                                        {
                                            // 眉仿捞 皑家窍绰 刀俊 吝刀 等促.
                                            if (new System.Random(20 + cret.AntiPoison).Next() == 0)
                                            {
                                                cret.MakePoison(Grobal2.POISON_DECHEALTH, 30, 1);
                                            }
                                            // 眉仿捞 皑家
                                            // if Random(2) = 0 then
                                            // cret.MakePoison (POISON_STONE, 5);   //付厚
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        protected override bool AttackTarget()
        {
            bool result;
            byte targdir=0;
            result = false;
            if (this.TargetCret != null)
            {
                if (this.TargetInSpitRange(this.TargetCret, ref targdir))
                {
                    if (GetCurrentTime - this.HitTime > this.GetNextHitTime())
                    {
                        this.HitTime = GetCurrentTime;
                        this.TargetFocusTime  =  HUtil32.GetTickCount();
                        SpitAttack(targdir);
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

    } // end TSpitSpider
}