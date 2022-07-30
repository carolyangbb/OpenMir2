using System;
using System.Collections;
using SystemModule;

namespace GameSvr
{
    public class TStickBlockMonster : TStickMonster
    {
        private bool DontAttack = false;
        private bool BoCallFollower = false;
        private bool BoTransparent = false;
        private bool FirstComeOut = false;
        private bool SecondMovement = false;
        private bool FirstStruck = false;
        private TCreature Caller = null;
        private long ComeoutTime = 0;
        private long TargetDisappearTime = 0;
        private readonly ArrayList childlist = null;
        private TCreature OldTargetCret = null;

        public TStickBlockMonster() : base()
        {
            this.ViewRange = 7;
            this.DigupRange = 4;
            this.DigdownRange = 4;
            BoCallFollower = true;
            BoTransparent = false;
            FirstComeOut = true;
            SecondMovement = false;
            DontAttack = true;
            childlist = new ArrayList();
            this.RaceServer = Grobal2.RC_STICKBLOCK;
            FirstStruck = false;
            ComeoutTime = 0;
            TargetDisappearTime = 0;
            Caller = null;
            OldTargetCret = null;
            this.BoAnimal = false;
            // 戒府瘤 臼档废...

        }
        //@ Destructor  Destroy()
        ~TStickBlockMonster()
        {
            childlist.Free();
            base.Destroy();
        }
        protected bool FindTarget()
        {
            bool result;
            int i;
            TCreature cret;
            result = false;
            for (i = 0; i < this.VisibleActors.Count; i++)
            {
                cret = (TCreature)this.VisibleActors[i].cret;
                if ((!cret.Death) && this.IsProperTarget(cret))
                {
                    if ((Math.Abs(this.CX - cret.CX) <= this.ViewRange) && (Math.Abs(this.CY - cret.CY) <= this.ViewRange))
                    {
                        if (cret.RaceServer == Grobal2.RC_USERHUMAN)
                        {
                            // 鸥百 瘤沥
                            this.TargetCret = cret;
                            OldTargetCret = this.TargetCret;
                            result = true;
                            break;
                        }
                    }
                }
            }
            return result;
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
            wide = 0;
            this.Dir = M2Share.GetNextDirection(this.CX, this.CY, target.CX, target.CY);
            this.SendRefMsg(Grobal2.RM_HIT, this.Dir, this.CX, this.CY, target.ActorId, "");
            TAbility _wvar1 = this.WAbil;
            pwr = this.GetAttackPower(HUtil32.LoByte(_wvar1.DC), HiByte(_wvar1.DC) - HUtil32.LoByte(_wvar1.DC));
            if (pwr <= 0)
            {
                return;
            }
            rlist = new ArrayList();
            this.GetMapCreatures(this.PEnvir, target.CX, target.CY, wide, rlist);
            for (i = 0; i < rlist.Count; i++)
            {
                cret = (TCreature)rlist[i];
                if (this.IsProperTarget(cret))
                {
                    this.SelectTarget(cret);
                    cret.SendRefMsg(Grobal2.RM_NORMALEFFECT, 0, cret.CX, cret.CY, Grobal2.NE_SOULSTONE_HIT, "");
                    cret.SendDelayMsg(this, Grobal2.RM_MAGSTRUCK, 0, pwr, 0, 0, "", 600);
                }
            }
            rlist.Free();
        }

        protected override bool AttackTarget()
        {
            bool result;
            byte targdir=0;
            result = false;
            if (DontAttack)
            {
                return result;
            }
            if (FindTarget())
            {
                // 贸澜 鸥百阑 官操瘤 臼澜.
                if (OldTargetCret != this.TargetCret)
                {
                    this.TargetCret = OldTargetCret;
                }
                if (this.TargetCret != null)
                {
                    if (this.TargetInAttackRange(this.TargetCret, ref targdir))
                    {
                        if (GetCurrentTime - this.HitTime > this.GetNextHitTime())
                        {
                            this.HitTime = GetCurrentTime;
                            if (new System.Random(8).Next() == 0)
                            {
                                // 矫埃,檬
                                this.TargetCret.MakePoison(Grobal2.POISON_STONE, 5, 0);
                            }
                            else
                            {
                                Attack(this.TargetCret, targdir);
                            }
                            result = true;
                        }
                    }
                }
            }
            return result;
        }

        protected override void ComeOut()
        {
            base.ComeOut();
            if (BoCallFollower)
            {
                if (FindTarget())
                {
                    if (this.TargetCret != null)
                    {
                        if (FirstComeOut)
                        {
                            FirstComeOut = false;
                            SecondMovement = true;
                            // 矫埃,檬
                            this.TargetCret.MakePoison(Grobal2.POISON_STONE, 5, 0);
                        }
                    }
                }
            }
        }

        protected override void ComeDown()
        {
            // 贸府 救窃.
        }

        public void CallFollower()
        {
            const int MAX_FOLLOWERS = 2;
            int nx=0;
            int ny=0;
            int dx=0;
            int dy=0;
            string monname;
            TCreature mon;
            string[] followers = new string[MAX_FOLLOWERS - 1 + 1];
            if (this.TargetCret != null)
            {
                nx = this.TargetCret.CX;
                ny = this.TargetCret.CY;
                // 哩矫懦 阁胶磐捞抚
                followers[0] = this.UserName;
                followers[1] = "11";
                // '龋去籍00';   //捞固瘤 救焊烙
                for (dx = -1; dx <= 1; dx++)
                {
                    for (dy = -1; dy <= 1; dy++)
                    {
                        if (((nx + dx == this.CX) && (ny + dy == this.CY)) || ((nx + dx == this.TargetCret.CX) && (ny + dy == this.TargetCret.CY)))
                        {
                            continue;
                        }
                        // 措阿急
                        if (Math.Abs(dx) == Math.Abs(dy))
                        {
                            monname = followers[1];
                        }
                        else
                        {
                            monname = followers[0];
                        }
                        if (this.PEnvir.CanWalk(nx + dx, ny + dy, false))
                        {
                            mon = svMain.UserEngine.AddCreatureSysop(this.MapName, nx + dx, ny + dy, monname);
                            if (mon != null)
                            {
                                // 鞍篮 辆幅狼 阁胶磐捞搁
                                if (mon.RaceServer == Grobal2.RC_STICKBLOCK)
                                {
                                    (mon as TStickBlockMonster).BoCallFollower = false;
                                    // 捧疙阁胶磐 汲沥
                                    if (mon.UserName == followers[1])
                                    {
                                        (mon as TStickBlockMonster).BoTransparent = true;
                                    }
                                    (mon as TStickBlockMonster).ComeOut();
                                    (mon as TStickBlockMonster).Caller = this;
                                    childlist.Add(mon);
                                }
                            }
                        }
                    }
                }
                this.ComeOut();
            }
        }

        public override void Die()
        {
            base.Die();
            if (BoCallFollower)
            {
                for (var i = childlist.Count - 1; i >= 0; i--)
                {
                    if (!((TCreature)childlist[i]).Death)
                    {
                        ((TCreature)childlist[i]).LastHiter = null;
                        ((TCreature)childlist[i]).ExpHiter = null;
                        ((TCreature)childlist[i]).BoNoItem = true;
                        ((TCreature)childlist[i]).Die();
                    }
                }
            }
        }

        public override void RunMsg(TMessageInfo msg)
        {
            int i;
            bool check;
            TCreature hiter;
            hiter = null;
            switch (msg.Ident)
            {
                case Grobal2.RM_REFMESSAGE:
                    if (((int)msg.sender) == Grobal2.RM_STRUCK)
                    {
                        check = false;
                        hiter = (TCreature)msg.lParam3;
                        if ((hiter != null) && (hiter.RaceServer == Grobal2.RC_USERHUMAN))
                        {
                            // 皋牢 各捞 嘎疽阑 锭 follower甸 吝俊 刚历 嘎篮 逞捞 绝栏搁 官肺 磷澜
                            if (BoCallFollower)
                            {
                                for (i = 0; i < childlist.Count; i++)
                                {
                                    if ((childlist[i] as TStickBlockMonster).FirstStruck)
                                    {
                                        check = true;
                                    }
                                }
                                if (!check && DontAttack)
                                {
                                    if (!this.Death)
                                    {
                                        FirstStruck = true;
                                        Die();
                                    }
                                }
                                DontAttack = false;
                            }
                            else
                            {
                                this.WAbil.HP = this.WAbil.MaxHP;
                                if (!BoTransparent)
                                {
                                    // follower啊 嘎疽阑 锭 皋牢 各捞 刚历 嘎瘤 臼疽栏搁 FirstStruck TRUE肺 悸泼
                                    // (汾去拜栏肺 部寂瘤绰啊?)
                                    if ((Caller != null) && (Caller.RaceServer == Grobal2.RC_STICKBLOCK) && !(Caller as TStickBlockMonster).FirstStruck)
                                    {
                                        FirstStruck = true;
                                        // 皋牢 各 傍拜 葛靛肺
                                        (Caller as TStickBlockMonster).DontAttack = false;
                                    }
                                }
                            }
                        }
                    }
                    break;
                case Grobal2.RM_MAGSTRUCK:
                    if (BoCallFollower)
                    {
                        DontAttack = false;
                    }
                    else
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
            // 
            // if not BoCallFollower then begin
            // WAbil.HP := WAbil.MaxHP;
            // exit;
            // end else begin
            // DontAttack := FALSE;
            // end;
            base.Struck(hiter);
        }

        public override void Run()
        {
            int nx=0;
            int ny=0;
            byte targdir=0;
            // 贸澜 鸥百阑 官操瘤 臼澜.
            if (OldTargetCret != this.TargetCret)
            {
                this.TargetCret = OldTargetCret;
            }
            if (GetCurrentTime - this.WalkTime > this.GetNextWalkTime())
            {
                if (BoCallFollower && (!this.Death))
                {
                    if ((Caller != null) && (Caller.RaceServer == Grobal2.RC_STICKBLOCK) && !(Caller as TStickBlockMonster).DontAttack)
                    {
                        DontAttack = false;
                    }
                    if (this.TargetCret != null)
                    {
                        if (!FirstComeOut && SecondMovement)
                        {
                            SecondMovement = false;
                            switch (new System.Random(4).Next())
                            {
                                case 0:
                                    nx = 0;
                                    ny = -1;
                                    break;
                                case 1:
                                    nx = 0;
                                    ny = 1;
                                    break;
                                case 2:
                                    nx = -1;
                                    ny = 0;
                                    break;
                                default:
                                    nx = 1;
                                    ny = 0;
                                    break;
                            }
                            this.SpaceMove(this.MapName, (short)(this.TargetCret.CX + nx), (short)(this.TargetCret.CY + ny), 2);
                            CallFollower();
                            // 何窍甸阑 阂矾晨
                            ComeoutTime  =  HUtil32.GetTickCount();
                        }
                        // 矫埃捞 儒福搁
                        if ((ComeoutTime != 0) && (HUtil32.GetTickCount() - ComeoutTime > 10000))
                        {
                            // 皋牢 各 傍拜 葛靛肺
                            if (DontAttack)
                            {
                                DontAttack = false;
                            }
                        }
                    }
                    if (DontAttack == false)
                    {
                        if (this.TargetCret != null)
                        {
                            if (!this.TargetInAttackRange(this.TargetCret, ref targdir))
                            {
                                if (TargetDisappearTime == 0)
                                {
                                    if ((ComeoutTime != 0) && (HUtil32.GetTickCount() - ComeoutTime > 15000))
                                    {
                                        TargetDisappearTime  =  HUtil32.GetTickCount();
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (TargetDisappearTime == 0)
                            {
                                if ((ComeoutTime != 0) && (HUtil32.GetTickCount() - ComeoutTime > 15000))
                                {
                                    TargetDisappearTime  =  HUtil32.GetTickCount();
                                }
                            }
                        }
                        if ((TargetDisappearTime != 0) && (HUtil32.GetTickCount() - TargetDisappearTime > 10000))
                        {
                            if (!this.Death)
                            {
                                FirstStruck = true;
                                Die();
                            }
                        }
                    }
                }
            }
            base.Run();
        }

    }
}