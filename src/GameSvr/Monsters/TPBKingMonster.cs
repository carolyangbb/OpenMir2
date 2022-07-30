using System;
using System.Collections;
using SystemModule;

namespace GameSvr
{
    public class TPBKingMonster : TDeadCowKingMonster
    {
        // 颇炔付脚 =====================================================================
        //Constructor  Create()
        public TPBKingMonster() : base()
        {
            this.ChainShotCount = 3;
            this.ViewRange = 12;
        }
        public override void Run()
        {
            // 颇炔付脚阑 甘啊厘磊府肺 单府备 啊辑磷捞绰芭 规瘤
            if (this.PEnvir != null)
            {
                // 甘狼 寇胞局 困摹秦 乐促搁. 埃窜茄 拌魂捞骨肺 拌加 积阿窍霸 秦档等促.
                // 颇炔付脚捞 乐绰 66 甘篮 300 x 300 甘捞促.
                if ((this.CX < 50) || (this.CX > this.PEnvir.MapWidth - 70) || (this.CY < 40) || (this.CY > this.PEnvir.MapHeight - 70))
                {
                    // 鸥百捞 乐栏搁 绝浚饶俊
                    this.LoseTarget();
                    // 郴何 救率栏肺 捞悼... 10鸥老 救率俊辑 唱鸥唱霸 窍磊. 版拌何盒篮 救亮澜
                    this.SpaceMove(this.PEnvir.MapName, (short)(new System.Random(this.PEnvir.MapWidth - 140).Next() + 60), (short)(new System.Random(this.PEnvir.MapHeight - 130).Next() + 50), 1);
                }
            }
            // 扁粮 角青阑 茄促.
            base.Run();
        }

        public override void Attack(TCreature target, byte dir)
        {
            int i;
            int ix;
            int iy;
            int ix2;
            int iy2;
            int levelgap;
            int push;
            int ixf;
            int ixt;
            int iyf;
            int iyt;
            int pwr;
            int dam;
            ArrayList list;
            TCreature cret;
            this.Dir = M2Share.GetNextDirection(this.CX, this.CY, target.CX, target.CY);
            TAbility _wvar1 = this.WAbil;
            pwr = this.GetAttackPower(_wvar1.Lobyte(_wvar1.DC), (short)HiByte(_wvar1.DC) - _wvar1.Lobyte(_wvar1.DC));
            ixf = _MAX(0, this.CX - 2);
            ixt = _MIN(this.PEnvir.MapWidth - 1, this.CX + 2);
            iyf = _MAX(0, this.CY - 2);
            iyt = _MIN(this.PEnvir.MapHeight - 1, this.CY + 2);
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
                            if (dam > 0)
                            {
                                cret.StruckDamage(dam, this);
                                // wparam
                                // lparam1
                                // lparam2
                                // hiter
                                cret.SendDelayMsg((TCreature)Grobal2.RM_STRUCK, Grobal2.RM_REFMESSAGE, dam, cret.WAbil.HP, cret.WAbil.MaxHP, this.ActorId, "", 200);
                                if (new System.Random(10).Next() == 0)
                                {
                                    cret.MakePoison(Grobal2.POISON_STONE, 5, 0);
                                }
                            }
                        }
                    }
                    list.Free();
                }
            }
            this.SendRefMsg(Grobal2.RM_HIT, this.Dir, this.CX, this.CY, target.ActorId, "");
            // 剐绢尘 规氢 犬牢
            ix = 0;
            iy = 0;
            ix2 = 0;
            iy2 = 0;
            switch (this.Dir)
            {
                case 0:
                    ix = this.CX;
                    iy = _MAX(0, this.CY - 1);
                    ix2 = this.CX;
                    iy2 = _MAX(0, this.CY - 2);
                    break;
                case 1:
                    ix = _MIN(this.PEnvir.MapWidth - 1, this.CX + 1);
                    iy = _MAX(0, this.CY - 1);
                    ix2 = _MIN(this.PEnvir.MapWidth - 1, this.CX + 2);
                    iy2 = _MAX(0, this.CY - 2);
                    break;
                case 2:
                    ix = _MIN(this.PEnvir.MapWidth - 1, this.CX + 1);
                    iy = this.CY;
                    ix2 = _MIN(this.PEnvir.MapWidth - 1, this.CX + 2);
                    iy2 = this.CY;
                    break;
                case 3:
                    ix = _MIN(this.PEnvir.MapWidth - 1, this.CX + 1);
                    iy = _MIN(this.PEnvir.MapHeight - 1, this.CY + 1);
                    ix2 = _MIN(this.PEnvir.MapWidth - 1, this.CX + 2);
                    iy2 = _MIN(this.PEnvir.MapHeight - 1, this.CY + 2);
                    break;
                case 4:
                    ix = this.CX;
                    iy = _MIN(this.PEnvir.MapHeight - 1, this.CY + 1);
                    ix2 = this.CX;
                    iy2 = _MIN(this.PEnvir.MapHeight - 1, this.CY + 2);
                    break;
                case 5:
                    ix = _MAX(0, this.CX - 1);
                    iy = _MIN(this.PEnvir.MapHeight - 1, this.CY + 1);
                    ix2 = _MAX(0, this.CX - 2);
                    iy2 = _MIN(this.PEnvir.MapHeight - 1, this.CY + 2);
                    break;
                case 6:
                    ix = _MAX(0, this.CX - 1);
                    iy = this.CY;
                    ix2 = _MAX(0, this.CX - 2);
                    iy2 = this.CY;
                    break;
                case 7:
                    ix = _MAX(0, this.CX - 1);
                    iy = _MAX(0, this.CY - 1);
                    ix2 = _MAX(0, this.CX - 2);
                    iy2 = _MAX(0, this.CY - 2);
                    break;
            }
            list = new ArrayList();
            list.Clear();
            this.PEnvir.GetAllCreature(ix, iy, true, list);
            // MainOutMessage ('[TPBKingMonster] ix,iy,Count=' + IntToStr(ix)+'/'+IntToStr(iy)+'/'+IntToStr(list.Count));
            for (i = 0; i < list.Count; i++)
            {
                cret = (TCreature)list[i];
                if (this.IsProperTarget(cret))
                {
                    if ((!cret.Death) && ((cret.RaceServer == Grobal2.RC_USERHUMAN) || (cret.Master != null)))
                    {
                        levelgap = 60 - cret.Abil.Level;
                        if (new System.Random(20).Next() < 4 + levelgap)
                        {
                            push = 3 + new System.Random(3).Next();
                            cret.CharPushed(this.Dir, push);
                        }
                    }
                }
            }
            list.Free();
            list = new ArrayList();
            this.PEnvir.GetAllCreature(ix2, iy2, true, list);
            // MainOutMessage ('[TPBKingMonster] ix2,iy2,Count=' + IntToStr(ix2)+'/'+IntToStr(iy2)+'/'+IntToStr(list.Count));
            for (i = 0; i < list.Count; i++)
            {
                cret = (TCreature)list[i];
                if (this.IsProperTarget(cret))
                {
                    if ((!cret.Death) && ((cret.RaceServer == Grobal2.RC_USERHUMAN) || (cret.Master != null)))
                    {
                        levelgap = 60 - cret.Abil.Level;
                        if (new System.Random(20).Next() < 4 + levelgap)
                        {
                            push = 3 + new System.Random(3).Next();
                            cret.CharPushed(this.Dir, push);
                        }
                    }
                }
            }
            list.Free();
        }

        public override void RangeAttack(TCreature targ)
        {
            int dam;
            TCreature cret;
            base.RangeAttack(targ);
            for (var i = 0; i < this.VisibleActors.Count; i++)
            {
                cret = (TCreature)this.VisibleActors[i].cret;
                if (this.IsProperTarget(cret))
                {
                    if ((cret.RaceServer == Grobal2.RC_USERHUMAN) || (cret.Master != null))
                    {
                        dam = cret.WAbil.HP / 4;
                        cret.DamageHealth(dam, 0);
                        cret.SendDelayMsg((TCreature)Grobal2.RM_STRUCK, Grobal2.RM_REFMESSAGE, dam, cret.WAbil.HP, cret.WAbil.MaxHP, this.ActorId, "", 800);
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
                if (GetCurrentTime - this.HitTime > this.GetNextHitTime())
                {
                    this.HitTime = GetCurrentTime;
                    if ((Math.Abs(this.CX - this.TargetCret.CX) <= 12) && (Math.Abs(this.CY - this.TargetCret.CY) <= 12))
                    {
                        if (this.TargetInSpitRange(this.TargetCret, ref targdir) && (new System.Random(3).Next() != 0))
                        {
                            this.TargetFocusTime  =  HUtil32.GetTickCount();
                            Attack(this.TargetCret, targdir);
                            try
                            {
                                if ((new System.Random(3).Next() == 0) && (this.VisibleActors.Count > 0))
                                {
                                    this.TargetCret = (TCreature)this.VisibleActors[new System.Random(this.VisibleActors.Count).Next()].cret;
                                    if (this.TargetCret != null)
                                    {
                                        this.SetTargetXY(this.TargetCret.CX, this.TargetCret.CY);
                                    }
                                }
                            }
                            catch
                            {
                                svMain.MainOutMessage("[Exception] TPBKingMonster.AttackTarget fail target change 1");
                            }
                            result = true;
                        }
                        else
                        {
                            if (this.ChainShot < this.ChainShotCount - 1)
                            {
                                this.ChainShot++;
                                this.TargetFocusTime  =  HUtil32.GetTickCount();
                                RangeAttack(this.TargetCret);
                            }
                            else
                            {
                                if (new System.Random(5).Next() == 0)
                                {
                                    this.ChainShot = 0;
                                }
                                // 3檬
                                try
                                {
                                    if ((GetCurrentTime > (3000 + this.TargetFocusTime)) && (this.VisibleActors.Count > 0))
                                    {
                                        this.TargetCret = (TCreature)this.VisibleActors[new System.Random(this.VisibleActors.Count).Next()].cret;
                                        if (this.TargetCret != null)
                                        {
                                            this.SetTargetXY(this.TargetCret.CX, this.TargetCret.CY);
                                            this.TargetFocusTime  =  HUtil32.GetTickCount();
                                        }
                                    }
                                }
                                catch
                                {
                                    svMain.MainOutMessage("[Exception] TPBKingMonster.AttackTarget fail target change 2");
                                }
                            }
                            result = true;
                        }
                    }
                    else
                    {
                        if (this.TargetCret.MapName == this.MapName)
                        {
                            if ((Math.Abs(this.CX - this.TargetCret.CX) <= 11) && (Math.Abs(this.CY - this.TargetCret.CY) <= 11))
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

    }
}