using System;
using System.Collections;
using SystemModule;

namespace GameSvr
{
    public class TMagicManager
    {
        public TMagicManager() : base()
        {
        }

        public bool IsSwordSkill(int mid)
        {
            bool result = false;
            switch (mid)
            {
                case 3:
                case 4:
                case 7:
                case 12:
                case 25:
                case 26:
                case 27:
                case 34:
                case 38:
                    result = true;
                    break;
            }
            return result;
        }

        public int MPow(TUserMagic pum)
        {
            return pum.pDef.MinPower + new System.Random(pum.pDef.MaxPower - pum.pDef.MinPower).Next();
        }

        public int MagPushAround(TCreature user, int pushlevel)
        {
            int result;
            int i;
            int ndir;
            int levelgap;
            int push;
            TCreature cret;
            result = 0;
            for (i = 0; i < user.VisibleActors.Count; i++)
            {
                cret = (TCreature)user.VisibleActors[i].cret;
                if ((Math.Abs(user.CX - cret.CX) <= 1) && (Math.Abs(user.CY - cret.CY) <= 1))
                {
                    if ((!cret.Death) && (cret != user))
                    {
                        if ((user.Abil.Level > cret.Abil.Level) && (!cret.StickMode))
                        {
                            levelgap = user.Abil.Level - cret.Abil.Level;
                            if (new System.Random(20).Next() < 6 + pushlevel * 3 + levelgap)
                            {
                                // 荐访沥档俊 蝶扼辑
                                if (user.IsProperTarget(cret))
                                {
                                    push = 1 + HUtil32._MAX(0, pushlevel - 1) + new System.Random(2).Next();
                                    ndir = M2Share.GetNextDirection(user.CX, user.CY, cret.CX, cret.CY);
                                    cret.CharPushed(ndir, push);
                                    cret.PushedCount++;
                                    result++;
                                }
                            }
                        }
                    }
                }
            }
            return result;
        }

        public bool MagDragonFire(TCreature user, int pwr, int skilllevel)
        {
            bool result;
            int i;
            ArrayList rlist;
            TCreature cret;
            int wide;
            result = false;
            rlist = new ArrayList();
            wide = 2;
            user.GetMapCreatures(user.PEnvir, user.CX, user.CY, wide, rlist);
            for (i = 0; i < rlist.Count; i++)
            {
                cret = (TCreature)rlist[i];
                if (user.IsProperTarget(cret))
                {
                    user.SelectTarget(cret);
                    cret.SendMsg(user, Grobal2.RM_MAGSTRUCK, 0, pwr, 0, 0, "");
                    result = true;
                }
            }
            rlist.Free();
            return result;
        }

        // 0..3
        public bool MagLightingShock(TCreature user, TCreature target, int x, int y, int shocklevel)
        {
            bool result;
            int ran;
            result = false;
            if ((target.RaceServer != Grobal2.RC_USERHUMAN) && (new System.Random(4 - shocklevel).Next() == 0))
            {
                target.TargetCret = null;
                if (target.Master == user)
                {
                    target.MakeHolySeize((10 + shocklevel * 5) * 1000);
                    result = true;
                    return result;
                }
                if (new System.Random(2).Next() == 0)
                {
                    if (target.Abil.Level <= user.Abil.Level + 2)
                    {
                        if (new System.Random(3).Next() == 0)
                        {
                            if ((10 + target.Abil.Level) < new System.Random(20 + user.Abil.Level + shocklevel * 5).Next())
                            {
                                if (user.EnableRecallMob(target, shocklevel))
                                {
                                    ran = target.WAbil.MaxHP / 100;
                                    if (target.RaceServer == Grobal2.RC_GHOST_TIGER)
                                    {
                                        ran = ran / 4;
                                    }
                                    if (ran <= 2)
                                    {
                                        ran = 2;
                                    }
                                    else
                                    {
                                        ran = ran + ran;
                                    }
                                    if ((target.Master != user) && (new System.Random(ran).Next() == 0))
                                    {
                                        target.BreakCrazyMode();
                                        if (target.Master != null)
                                        {
                                            target.WAbil.HP = (short)(target.WAbil.HP / 10);
                                        }
                                        target.Master = user;
                                        target.MasterRoyaltyTime = HUtil32.GetTickCount() + ((long)20 + shocklevel * 20 + new System.Random(user.Abil.Level * 2).Next()) * 60 * 1000;
                                        target.SlaveMakeLevel = (byte)shocklevel;
                                        if (target.SlaveLifeTime == 0)
                                        {
                                            target.SlaveLifeTime = HUtil32.GetTickCount();
                                        }
                                        target.BreakHolySeize();
                                        if (target.NextWalkTime > 1500 - (shocklevel * 200))
                                        {
                                            target.NextWalkTime = 1500 - (shocklevel * 200);
                                        }
                                        if (target.NextHitTime > 2000 - (shocklevel * 200))
                                        {
                                            target.NextHitTime = 2000 - (shocklevel * 200);
                                        }
                                        target.UserNameChanged();
                                        user.SlaveList.Add(target);
                                    }
                                    else
                                    {
                                        if (new System.Random(20).Next() == 0)
                                        {
                                            target.WAbil.HP = 0;
                                        }
                                    }
                                }
                                else
                                {
                                    if ((target.LifeAttrib == Grobal2.LA_UNDEAD) && (new System.Random(2).Next() == 0))
                                    {
                                        target.WAbil.HP = 0;
                                    }
                                }
                            }
                            else
                            {
                                if ((target.LifeAttrib != Grobal2.LA_UNDEAD) && (new System.Random(2).Next() == 0))
                                {
                                    target.MakeCrazyMode(10 + new System.Random(20).Next());
                                }
                            }
                        }
                        else
                        {
                            if (target.LifeAttrib != Grobal2.LA_UNDEAD)
                            {
                                target.MakeCrazyMode(10 + new System.Random(20).Next());
                            }
                        }
                    }
                }
                else
                {
                    target.MakeHolySeize((10 + shocklevel * 5) * 1000);
                }
                result = true;
            }
            else if (new System.Random(2).Next() == 0)
            {
                result = true;
            }
            return result;
        }

        public bool MagTurnUndead(TCreature user, TCreature target, int x, int y, int mlevel)
        {
            bool result;
            int lvgap;
            result = false;
            if ((!target.NeverDie) && (target.LifeAttrib == Grobal2.LA_UNDEAD))
            {
                ((TAnimal)target).Struck(user);
                if (target.TargetCret == null)
                {
                    ((TAnimal)target).BoRunAwayMode = true;
                    ((TAnimal)target).RunAwayStart = HUtil32.GetTickCount();
                    ((TAnimal)target).RunAwayTime = 10 * 1000;
                }
                user.SelectTarget(target);
                if ((target.Abil.Level < (HUtil32._MIN(user.Abil.Level, 51) - 1 + new System.Random(4).Next())) && (target.Abil.Level < M2Share.MAXKINGLEVEL - 1))
                {
                    lvgap = user.Abil.Level - target.Abil.Level;
                    if (new System.Random(100).Next() < (15 + mlevel * 7 + lvgap))
                    {
                        target.SetLastHiter(user);
                        target.WAbil.HP = 0;
                        result = true;
                    }
                }
            }
            return result;
        }

        public bool MagLightingSpaceMove(TCreature user, int slevel)
        {
            bool result;
            TEnvirnoment oldenvir;
            TUserHuman hum;
            result = false;
            if (!user.BoTaiwanEventUser)
            {
                if (!(user.PEnvir.NoEscapeMove || user.PEnvir.NoTeleportMove))
                {
                    // 酒傍青过 荤侩阂啊(sonmg)
                    if (new System.Random(11).Next() < 4 + slevel * 2)
                    {
                        user.SendRefMsg(Grobal2.RM_SPACEMOVE_HIDE2, 0, 0, 0, 0, "");
                        if (user is TUserHuman)
                        {
                            oldenvir = user.PEnvir;
                            ((TUserHuman)user).RandomSpaceMove(user.HomeMap, 1);
                            if (oldenvir != user.PEnvir)
                            {
                                if (user.RaceServer == Grobal2.RC_USERHUMAN)
                                {
                                    hum = (TUserHuman)user;
                                    hum.BoTimeRecall = false;
                                    hum.BoTimeRecallGroup = false;
                                }
                            }
                        }
                        result = true;
                    }
                }
            }
            return result;
        }

        public int MagMakeHolyCurtain(TCreature user, int htime, int x, int y)
        {
            int result;
            TEvent __event;
            int i;
            ArrayList rlist;
            TCreature cret;
            THolySeizeInfo phs;
            result = 0;
            if (user.PEnvir.CanWalk(x, y, true))
            {
                rlist = new ArrayList();
                phs = null;
                user.GetMapCreatures(user.PEnvir, x, y, 1, rlist);
                for (i = 0; i < rlist.Count; i++)
                {
                    cret = (TCreature)rlist[i];
                    if ((cret.RaceServer >= Grobal2.RC_ANIMAL) && (cret.Abil.Level < (user.Abil.Level - 1 + new System.Random(4).Next())) && (cret.Abil.Level < M2Share.MAXKINGLEVEL - 1) && (cret.Master == null))
                    {
                        cret.MakeHolySeize(htime * 1000);
                        if (phs == null)
                        {
                            //phs = M2Share.THolySeizeInfo = new THolySeizeInfo();
                            //FillChar(phs, sizeof(THolySeizeInfo), '\0');
                            phs.seizelist = new ArrayList();
                            phs.OpenTime = HUtil32.GetTickCount();
                            phs.SeizeTime = htime * 1000;
                        }
                        phs.seizelist.Add(cret);
                        result++;
                    }
                    else
                    {
                        result = 0;
                        break;
                    }
                }
                rlist.Free();
                if ((result > 0) && (phs != null))
                {
                    __event = new THolyCurtainEvent(user.PEnvir, x - 1, y - 2, Grobal2.ET_HOLYCURTAIN, htime * 1000);
                    M2Share.EventMan.AddEvent(__event);
                    phs.earr[0] = __event;
                    __event = new THolyCurtainEvent(user.PEnvir, x + 1, y - 2, Grobal2.ET_HOLYCURTAIN, htime * 1000);
                    M2Share.EventMan.AddEvent(__event);
                    phs.earr[1] = __event;
                    __event = new THolyCurtainEvent(user.PEnvir, x - 2, y - 1, Grobal2.ET_HOLYCURTAIN, htime * 1000);
                    M2Share.EventMan.AddEvent(__event);
                    phs.earr[2] = __event;
                    __event = new THolyCurtainEvent(user.PEnvir, x + 2, y - 1, Grobal2.ET_HOLYCURTAIN, htime * 1000);
                    M2Share.EventMan.AddEvent(__event);
                    phs.earr[3] = __event;
                    __event = new THolyCurtainEvent(user.PEnvir, x - 2, y + 1, Grobal2.ET_HOLYCURTAIN, htime * 1000);
                    M2Share.EventMan.AddEvent(__event);
                    phs.earr[4] = __event;
                    __event = new THolyCurtainEvent(user.PEnvir, x + 2, y + 1, Grobal2.ET_HOLYCURTAIN, htime * 1000);
                    M2Share.EventMan.AddEvent(__event);
                    phs.earr[5] = __event;
                    __event = new THolyCurtainEvent(user.PEnvir, x - 1, y + 2, Grobal2.ET_HOLYCURTAIN, htime * 1000);
                    M2Share.EventMan.AddEvent(__event);
                    phs.earr[6] = __event;
                    __event = new THolyCurtainEvent(user.PEnvir, x + 1, y + 2, Grobal2.ET_HOLYCURTAIN, htime * 1000);
                    M2Share.EventMan.AddEvent(__event);
                    phs.earr[7] = __event;
                    M2Share.UserEngine.HolySeizeList.Add(phs);
                    // 搬拌眠啊
                }
                else
                {
                    if (phs != null)
                    {
                        phs.seizelist.Free();
                        //Dispose(phs);
                    }
                }
            }
            return result;
        }

        public int MagMakeFireCross(TCreature user, int dam, int htime, int x, int y)
        {
            int result;
            TEvent __event;
            if (user.PEnvir.GetEvent(x, y - 1) == null)
            {
                __event = new TFireBurnEvent(user, x, y - 1, Grobal2.ET_FIRE, htime * 1000, dam);
                M2Share.EventMan.AddEvent(__event);
            }
            if (user.PEnvir.GetEvent(x - 1, y) == null)
            {
                __event = new TFireBurnEvent(user, x - 1, y, Grobal2.ET_FIRE, htime * 1000, dam);
                M2Share.EventMan.AddEvent(__event);
            }
            if (user.PEnvir.GetEvent(x, y) == null)
            {
                __event = new TFireBurnEvent(user, x, y, Grobal2.ET_FIRE, htime * 1000, dam);
                M2Share.EventMan.AddEvent(__event);
            }
            if (user.PEnvir.GetEvent(x + 1, y) == null)
            {
                __event = new TFireBurnEvent(user, x + 1, y, Grobal2.ET_FIRE, htime * 1000, dam);
                M2Share.EventMan.AddEvent(__event);
            }
            if (user.PEnvir.GetEvent(x, y + 1) == null)
            {
                __event = new TFireBurnEvent(user, x, y + 1, Grobal2.ET_FIRE, htime * 1000, dam);
                M2Share.EventMan.AddEvent(__event);
            }
            result = 1;
            return result;
        }

        public bool MagBigHealing(TCreature user, int pwr, int x, int y)
        {
            bool result;
            int i;
            ArrayList rlist;
            TCreature cret;
            result = false;
            rlist = new ArrayList();
            user.GetMapCreatures(user.PEnvir, x, y, 1, rlist);
            for (i = 0; i < rlist.Count; i++)
            {
                cret = (TCreature)rlist[i];
                if (user.IsProperFriend(cret))
                {
                    if (cret.WAbil.HP < cret.WAbil.MaxHP)
                    {
                        cret.SendDelayMsg(user, Grobal2.RM_MAGHEALING, 0, pwr, 0, 0, "", 800);
                        result = true;
                    }
                    if (user.BoAbilSeeHealGauge)
                    {
                        user.SendMsg(cret, Grobal2.RM_INSTANCEHEALGUAGE, 0, 0, 0, 0, "");
                    }
                }
            }
            rlist.Free();
            return result;
        }

        public bool MagBigExplosion(TCreature user, int pwr, int x, int y, int wide)
        {
            bool result;
            int i;
            ArrayList rlist;
            TCreature cret;
            result = false;
            rlist = new ArrayList();
            user.GetMapCreatures(user.PEnvir, x, y, wide, rlist);
            for (i = 0; i < rlist.Count; i++)
            {
                cret = (TCreature)rlist[i];
                if (user.IsProperTarget(cret))
                {
                    user.SelectTarget(cret);
                    cret.SendMsg(user, Grobal2.RM_MAGSTRUCK, 0, pwr, 0, 0, "");
                    result = true;
                }
            }
            rlist.Free();
            return result;
        }

        public bool MagElecBlizzard(TCreature user, int pwr)
        {
            bool result;
            int i;
            int acpwr;
            ArrayList rlist;
            TCreature cret;
            result = false;
            rlist = new ArrayList();
            user.GetMapCreatures(user.PEnvir, user.CX, user.CY, 2, rlist);
            for (i = 0; i < rlist.Count; i++)
            {
                cret = (TCreature)rlist[i];
                if (cret.LifeAttrib != Grobal2.LA_UNDEAD)
                {
                    // 攫单靛 拌凯 阁胶磐俊霸 傍拜仿捞 乐澜
                    acpwr = pwr / 10;
                }
                else
                {
                    acpwr = pwr;
                }
                if (user.IsProperTarget(cret))
                {
                    cret.SendMsg(user, Grobal2.RM_MAGSTRUCK, 0, acpwr, 0, 0, "");
                    result = true;
                }
            }
            rlist.Free();
            return result;
        }

        // 贱磊啊 篮脚捞 等促. 唱甫 傍拜窍绊 乐绰 各甸捞 唱甫 隔扼夯促.
        // 贱磊啊 锭府搁 傍拜窃.
        // 贱磊啊 捞悼窍搁 秦力等促.
        public bool MagMakePrivateTransparent(TCreature user, int htime)
        {
            bool result;
            int i;
            ArrayList rlist;
            TCreature cret;
            result = false;
            if (user.StatusArr[Grobal2.STATE_TRANSPARENT] > 0)
            {
                return result;
            }
            // 捞固 捧疙
            rlist = new ArrayList();
            user.GetMapCreatures(user.PEnvir, user.CX, user.CY, 9, rlist);
            for (i = 0; i < rlist.Count; i++)
            {
                cret = (TCreature)rlist[i];
                if (cret.RaceServer >= Grobal2.RC_ANIMAL)
                {
                    if (cret.TargetCret == user)
                    {
                        // 啊鳖捞 乐绰仇篮 1/2 篮 逞绢埃促.
                        if ((Math.Abs(cret.CX - user.CX) > 1) || (Math.Abs(cret.CY - user.CY) > 1) || (new System.Random(2).Next() == 0))
                        {
                            cret.TargetCret = null;
                        }
                    }
                }
            }
            rlist.Free();
            user.StatusArr[Grobal2.STATE_TRANSPARENT] = (ushort)htime;
            user.CharStatus = user.GetCharStatus();
            user.CharStatusChanged();
            user.BoHumHideMode = true;
            user.BoFixedHideMode = true;
            // 茄 磊府俊辑父 篮脚啊瓷, 捞悼窍搁 篮脚捞 钱赴促.
            result = true;
            return result;
        }

        public bool MagMakeGroupTransparent(TCreature user, int x, int y, int htime)
        {
            bool result;
            int i;
            ArrayList rlist;
            TCreature cret;
            result = false;
            rlist = new ArrayList();
            user.GetMapCreatures(user.PEnvir, x, y, 1, rlist);
            for (i = 0; i < rlist.Count; i++)
            {
                cret = (TCreature)rlist[i];
                // if cret.RaceServer = RC_USERHUMAN then begin
                if (user.IsProperFriend(cret))
                {
                    // . or (cret.Master <> nil) then begin  //
                    if (cret.StatusArr[Grobal2.STATE_TRANSPARENT] == 0)
                    {
                        // 捧疙捞 酒囱 惑怕
                        cret.SendDelayMsg(cret, Grobal2.RM_TRANSPARENT, 0, htime, 0, 0, "", 800);
                        // if MagMakePrivateTransparent (cret, htime) then
                        result = true;
                    }
                }
            }
            rlist.Free();
            return result;
        }

        // 2003/07/15 脚痹公傍
        public bool MagMakePrivateClean(TCreature user, int x, int y, int pct)
        {
            bool result;
            int i;
            ArrayList rlist;
            TCreature cret;
            result = false;
            if (new System.Random(100).Next() > pct)
            {
                return result;
            }
            rlist = new ArrayList();
            user.GetMapCreatures(user.PEnvir, x, y, 0, rlist);
            for (i = 0; i < rlist.Count; i++)
            {
                cret = (TCreature)rlist[i];
                if (user.IsProperFriend(cret))
                {
                    cret.StatusArr[Grobal2.POISON_DECHEALTH] = 0;
                    cret.StatusArr[Grobal2.POISON_DAMAGEARMOR] = 0;
                    cret.StatusArr[Grobal2.POISON_ICE] = 0;
                    cret.StatusArr[Grobal2.POISON_STUN] = 0;
                    cret.StatusArr[Grobal2.POISON_DONTMOVE] = 0;
                    cret.StatusArr[Grobal2.POISON_STONE] = 0;
                    cret.StatusArr[Grobal2.POISON_SLOW] = 0;
                    cret.CharStatus = cret.GetCharStatus();
                    cret.CharStatusChanged();
                    result = true;
                }
            }
            rlist.Free();
            return result;
        }

        public void WindCutHit(TCreature user, TCreature target, int hitPwr, int magpwr)
        {
            int dam;
            if (user.IsProperTarget(target))
            {
                dam = 0;
                dam = dam + target.GetHitStruckDamage(user, hitPwr);
                dam = dam + target.GetMagStruckDamage(user, magpwr);
                if (dam > 0)
                {
                    target.StruckDamage(dam, user);
                    target.SendDelayMsg(Grobal2.RM_STRUCK, Grobal2.RM_REFMESSAGE, (short)dam, target.WAbil.HP, target.WAbil.MaxHP, user.ActorId, "", 200);
                }
            }
        }

        public bool MagWindCut(TCreature user, int skilllevel)
        {
            bool result;
            int i;
            ArrayList rlist;
            TCreature cret;
            int pwr;
            bool isnear;
            int xx;
            int yy;
            int f1x;
            int f1y;
            int f2x;
            int f2y;
            bool CriticalDamage;
            int DcRandom;
            result = false;
            pwr = 0;
            isnear = false;
            rlist = new ArrayList();
            // 裹困狼 淋缴捞 登绰 谅钎 函版
            xx = user.CX;
            yy = user.CY;
            f1x = xx;
            f1y = yy;
            f2x = xx;
            f2y = yy;
            switch (user.Dir)
            {
                case 0:
                    // 碍窍霸 鸥拜捞 甸绢啊绰 谅钎 汲沥
                    f1x = xx;
                    f1y = yy - 1;
                    f2x = xx;
                    f2y = yy - 2;
                    // 吝居谅钎 汲沥
                    yy = yy - 2;
                    user.GetMapCreatures(user.PEnvir, xx, yy, 1, rlist);
                    break;
                case 1:
                    // 碍窍霸 鸥拜捞 甸绢啊绰 谅钎 汲沥
                    f1x = xx + 1;
                    f1y = yy - 1;
                    f2x = xx + 2;
                    f2y = yy - 2;
                    // 吝居谅钎 汲沥
                    // xx := xx + 1;
                    // yy := yy - 1;
                    xx = xx + 2;
                    yy = yy - 2;
                    user.GetObliqueMapCreatures(user.PEnvir, xx, yy, 1, user.Dir, rlist);
                    break;
                case 2:
                    // 碍窍霸 鸥拜捞 甸绢啊绰 谅钎 汲沥
                    f1x = xx + 1;
                    f1y = yy;
                    f2x = xx + 2;
                    f2y = yy;
                    // 吝居谅钎 汲沥
                    xx = xx + 2;
                    user.GetMapCreatures(user.PEnvir, xx, yy, 1, rlist);
                    break;
                case 3:
                    // 碍窍霸 鸥拜捞 甸绢啊绰 谅钎 汲沥
                    f1x = xx + 1;
                    f1y = yy + 1;
                    f2x = xx + 2;
                    f2y = yy + 2;
                    // 吝居谅钎 汲沥
                    // xx := xx + 1;
                    // yy := yy + 1;
                    xx = xx + 2;
                    yy = yy + 2;
                    user.GetObliqueMapCreatures(user.PEnvir, xx, yy, 1, user.Dir, rlist);
                    break;
                case 4:
                    // 碍窍霸 鸥拜捞 甸绢啊绰 谅钎 汲沥
                    f1x = xx;
                    f1y = yy + 1;
                    f2x = xx;
                    f2y = yy + 2;
                    // 吝居谅钎 汲沥
                    yy = yy + 2;
                    user.GetMapCreatures(user.PEnvir, xx, yy, 1, rlist);
                    break;
                case 5:
                    // 碍窍霸 鸥拜捞 甸绢啊绰 谅钎 汲沥
                    f1x = xx - 1;
                    f1y = yy + 1;
                    f2x = xx - 2;
                    f2y = yy + 2;
                    // 吝居谅钎 汲沥
                    // xx := xx - 1;
                    // yy := yy + 1;
                    xx = xx - 2;
                    yy = yy + 2;
                    user.GetObliqueMapCreatures(user.PEnvir, xx, yy, 1, user.Dir, rlist);
                    break;
                case 6:
                    // 碍窍霸 鸥拜捞 甸绢啊绰 谅钎 汲沥
                    f1x = xx - 1;
                    f1y = yy;
                    f2x = xx - 2;
                    f2y = yy;
                    // 吝居谅钎 汲沥
                    xx = xx - 2;
                    user.GetMapCreatures(user.PEnvir, xx, yy, 1, rlist);
                    break;
                case 7:
                    // 碍窍霸 鸥拜捞 甸绢啊绰 谅钎 汲沥
                    f1x = xx - 1;
                    f1y = yy - 1;
                    f2x = xx - 2;
                    f2y = yy - 2;
                    // 吝居谅钎 汲沥
                    // xx := xx - 1;
                    // yy := yy - 1;
                    xx = xx - 2;
                    yy = yy - 2;
                    user.GetObliqueMapCreatures(user.PEnvir, xx, yy, 1, user.Dir, rlist);
                    break;
            }
            // 葛记阑 秒窍霸 茄促澜俊
            // user.HitMotion( RM_HIT, user.Dir, user.CX, user.CY);
            // user.SendRefMsg( RM_WINDCUT , user.Dir , user.CX , user.CY , 0, '');
            // 厘厚 青款摹肺 农府萍拿 犬伏 搬沥
            CriticalDamage = false;
            if (new System.Random(100).Next() <= (1 + user.UseItems[Grobal2.U_WEAPON].Desc[3] - user.UseItems[Grobal2.U_WEAPON].Desc[4]))
            {
                CriticalDamage = true;
            }
            for (i = 0; i < rlist.Count; i++)
            {
                cret = (TCreature)rlist[i];
                if (user.IsProperTarget(cret) && (!cret.Death) && (!cret.BoGhost))
                {
                    // 鸥拜捞 碍窍霸 甸绢啊具瞪 逞苞 距窍霸 甸绢啊具瞪 逞 魄窜.
                    if (((cret.CX == f1x) && (cret.CY == f1y)) || ((cret.CX == f2x) && (cret.CY == f2y)))
                    {
                        isnear = true;
                    }
                    else
                    {
                        isnear = false;
                    }
                    // 农府萍拿 单固瘤
                    if (CriticalDamage)
                    {
                        DcRandom = HUtil32.HiByte(user.WAbil.DC);
                    }
                    else
                    {
                        DcRandom = new System.Random(HUtil32.HiByte(user.WAbil.DC)).Next();
                    }
                    // 傈规 1*2狼 裹困:
                    // ((1.2+0.3*(Lv_S+(LV/20)) * Random(Dcmax)/3+DC
                    // 弊 寇 裹困: ((0.8+0.2*(Lv_S+LV/20)) * Random(Dcmax)/3+DC
                    // 鸥拜摹啊 促弗霸 利侩凳
                    if (isnear)
                    {
                        pwr = (12 + 3 * (skilllevel + user.Abil.Level / 20)) * DcRandom / 30 + HUtil32.LoByte(user.WAbil.DC);
                    }
                    else
                    {
                        pwr = (8 + 2 * (skilllevel + user.Abil.Level / 20)) * DcRandom / 30 + HUtil32.LoByte(user.WAbil.DC);
                    }
                    // 农府萍拿 单固瘤
                    if (CriticalDamage)
                    {
                        pwr = pwr * 2;
                    }
                    if (pwr > 0)
                    {
                        WindCutHit(user, cret, pwr, 0);
                        result = true;
                    }
                }
            }
            rlist.Free();
            return result;
        }

        // 2004/06/23 脚痹公傍
        // 器铰八
        public bool MagPullMon(TCreature user, TCreature target, int skilllevel)
        {
            bool result;
            int levelgap;
            int rush;
            int rushdir;
            int rushDist;
            int Dur;
            bool SuccessFlag;
            result = false;
            SuccessFlag = false;
            if (user == null)
            {
                return result;
            }
            if (target != null)
            {
                // 荤恩茄抛 镜 荐 绝澜.
                if (target.RaceServer == Grobal2.RC_USERHUMAN)
                {
                    return result;
                }
                // 框流捞瘤 臼绰 阁胶磐绰 缠绢棵 荐 绝澜(2004/12/01)
                if (target.StickMode)
                {
                    return result;
                }
                // 呈公 啊鳖捞 乐栏搁 扁贱阑 镜 荐 绝澜.
                if ((Math.Abs(user.CX - target.CX) < 3) && (Math.Abs(user.CY - target.CY) < 3))
                {
                    user.SysMsg("Target is too close.", 0);
                    return result;
                }
                else if ((Math.Abs(user.CX - target.CX) > 7) && (Math.Abs(user.CY - target.CY) > 7))
                {
                    // user.SysMsg('惑措啊 呈公 钢府 乐嚼聪促.', 0);
                    return result;
                }
                // 钢府乐绰 利阑 缠绢寸变促.
                user.Dir = M2Share.GetNextDirectionNew(user.CX, user.CY, target.CX, target.CY);
                // user.SendRefMsg (RM_LIGHTING, user.Dir, user.CX, user.CY, Integer(target), '');
                // 规氢喊 缠绢寸扁绰 芭府 炼例
                rushdir = (user.Dir + 4) % 8;
                if (new ArrayList(new int[] { 0, 4 }).Contains(rushdir))
                {
                    rushDist = Math.Abs(user.CY - target.CY) - 3;
                }
                else if (new ArrayList(new int[] { 2, 6 }).Contains(rushdir))
                {
                    rushDist = Math.Abs(user.CX - target.CX) - 3;
                }
                else
                {
                    rushDist = HUtil32._MAX(0, HUtil32._MIN(Math.Abs(user.CX - target.CX) - 2, Math.Abs(user.CY - target.CY) - 2));
                }
                if (user.IsProperTarget(target))
                {
                    // 矫眉啊 酒聪绊 部豪各捞 酒聪绢具 窃.
                    // if (not target.Death) and (target.Master = nil) then begin
                    // 矫眉啊 酒聪绢具 窃. 部豪各篮 啊瓷窍霸 荐沥(sonmg 2005/11/2)
                    if (!target.Death)
                    {
                        if ((target.Abil.Level < user.Abil.Level + 5 + new System.Random(8).Next()) && (target.Abil.Level < 60))
                        {
                            levelgap = user.Abil.Level - target.Abil.Level;
                            if (target.RaceServer == Grobal2.RC_USERHUMAN)
                            {
                                if (new System.Random(30).Next() < ((skilllevel + 1) * 2) + levelgap + 4)
                                {
                                    SuccessFlag = true;
                                }
                            }
                            else if (target.RaceServer >= Grobal2.RC_ANIMAL)
                            {
                                if (new System.Random(30).Next() < ((skilllevel + 1) * 3) + levelgap + 9)
                                {
                                    SuccessFlag = true;
                                }
                            }
                            if (SuccessFlag)
                            {
                                // 流急俊 乐绰逞父 动变促.(1沫究 谅快肺 乐绰 逞档 动变促)
                                if ((user.CX == target.CX) || (user.CY == target.CY) || (Math.Abs(user.CX - target.CX) == Math.Abs(user.CY - target.CY)) || (user.CX + 1 == target.CX) || (user.CY + 1 == target.CY) || (Math.Abs(Math.Abs(user.CX - target.CX) - Math.Abs(user.CY - target.CY)) == 1) || (user.CX - 1 == target.CX) || (user.CY - 1 == target.CY))
                                {
                                    rush = rushDist;
                                    target.CharDrawingRush(rushdir, rush, false);
                                    if (target.RaceServer != Grobal2.RC_USERHUMAN)
                                    {
                                        Dur = HUtil32.MathRound((skilllevel + 1) * 1.6) + HUtil32._MAX(1, 10 - target.SpeedPoint);
                                    }
                                    else
                                    {
                                        Dur = HUtil32.MathRound((skilllevel + 1) * 0.8) + HUtil32._MAX(1, 10 - target.SpeedPoint);
                                        if (user.RaceServer == Grobal2.RC_USERHUMAN)
                                        {
                                            // 沥寸规绢甫 困茄 扁废..
                                            target.AddPkHiter(user);
                                        }
                                    }
                                    target.MakePoison(Grobal2.POISON_STONE, Dur, 1);
                                    target.SendRefMsg(Grobal2.RM_LOOPNORMALEFFECT, (short)target.ActorId, 1500, 0, Grobal2.NE_MONCAPTURE, "");
                                    result = true;
                                }
                                else
                                {
                                    // user.SysMsg('缠绢 寸辨 荐 绝绰 困摹俊 乐嚼聪促.', 0);
                                }
                            }
                        }
                    }
                }
            }
            return result;
        }

        public byte MagBlindEye_GetRPow(short pw)
        {
            byte result;
            if (HUtil32.HiByte(pw) > HUtil32.LoByte(pw))
            {
                result = (byte)(HUtil32.LoByte(pw) + new System.Random(HUtil32.HiByte(pw) - HUtil32.LoByte(pw) + 1).Next());
            }
            else
            {
                result = HUtil32.LoByte(pw);
            }
            return result;
        }

        public int MagBlindEye_GetPower(TUserMagic pum, int pw)
        {
            int result;
            // 荐访 0 窜拌俊辑绰 1/4狼 颇况烙
            result = HUtil32.MathRound(pw / (pum.pDef.MaxTrainLevel + 1) * (pum.Level + 1)) + pum.pDef.DefMinPower + new System.Random(pum.pDef.DefMaxPower - pum.pDef.DefMinPower).Next();
            return result;
        }

        public int MagBlindEye_GetPower13(TUserMagic pum, int pw)
        {
            int result;
            // 荐访 0 窜拌俊档 1/3狼 颇况啊 巢
            double p1;
            double p2;
            p1 = pw / 3;
            p2 = pw - p1;
            result = HUtil32.MathRound(p1 + p2 / (pum.pDef.MaxTrainLevel + 1) * (pum.Level + 1) + pum.pDef.DefMinPower + new System.Random(pum.pDef.DefMaxPower - pum.pDef.DefMinPower).Next());
            return result;
        }

        public bool MagBlindEye(TCreature user, TCreature target, TUserMagic pum)
        {
            int pwr;
            int levelgap;
            bool result = false;
            if (user.IsProperTarget(target))
            {
                if (target.RaceServer >= Grobal2.RC_ANIMAL)
                {
                    levelgap = user.Abil.Level - target.Abil.Level;
                    if ((20 - (pum.Level + 1) * 2) <= new System.Random(MagBlindEye_GetRPow(user.WAbil.SC) + (user.Abil.Level / 5) + (levelgap * 2)).Next())
                    {
                        if ((target.Abil.Level < user.Abil.Level + 1 + new System.Random(3).Next()) && (target.Abil.Level < 55))
                        {
                            if (target.BoGoodCrazyMode == false)
                            {
                                pwr = MagBlindEye_GetPower13(pum, 10) + HUtil32.MathRound(MagBlindEye_GetRPow(user.WAbil.SC) / 3);
                                pwr = pwr + new System.Random(20).Next();
                                target.TargetCret = null;
                                target.MakeGoodCrazyMode(pwr);
                            }
                        }
                        result = true;
                    }
                    user.SelectTarget(target);
                }
            }
            return result;
        }

        public byte SpellNow_GetRPow(short pw)
        {
            byte result;
            if (HUtil32.HiByte(pw) > HUtil32.LoByte(pw))
            {
                result = (byte)(HUtil32.LoByte(pw) + new System.Random(HUtil32.HiByte(pw) - HUtil32.LoByte(pw) + 1).Next());
            }
            else
            {
                result = HUtil32.LoByte(pw);
            }
            return result;
        }

        public int SpellNow_GetPower(TUserMagic pum, int pw)
        {
            int result;
            // 荐访 0 窜拌俊辑绰 1/4狼 颇况烙
            result = HUtil32.MathRound(pw / (pum.pDef.MaxTrainLevel + 1) * (pum.Level + 1)) + pum.pDef.DefMinPower + new System.Random(pum.pDef.DefMaxPower - pum.pDef.DefMinPower).Next();
            return result;
        }

        public int SpellNow_GetPower13(TUserMagic pum, int pw)
        {
            int result;
            // 荐访 0 窜拌俊档 1/3狼 颇况啊 巢
            double p1;
            double p2;
            p1 = pw / 3;
            p2 = pw - p1;
            result = HUtil32.MathRound(p1 + p2 / (pum.pDef.MaxTrainLevel + 1) * (pum.Level + 1) + pum.pDef.DefMinPower + new System.Random(pum.pDef.DefMaxPower - pum.pDef.DefMinPower).Next());
            return result;
        }

        public int SpellNow_CanUseBujuk(TCreature user, int count)
        {
            int result;
            TStdItem pstd;
            result = 0;
            // 2003/03/15 COPARK 酒捞袍 牢亥配府 犬厘
            if (user.UseItems[Grobal2.U_BUJUK].Index > 0)
            {
                // U_ARMRINGL->U_BUJUK
                pstd = M2Share.UserEngine.GetStdItem(user.UseItems[Grobal2.U_BUJUK].Index);
                if (pstd != null)
                {
                    if ((pstd.StdMode == 25) && (pstd.Shape == 5))
                    {
                        // 何利
                        if (HUtil32.MathRound(user.UseItems[Grobal2.U_BUJUK].Dura / 100) >= (count - 1))
                        {
                            result = 1;
                        }
                    }
                }
            }
            if ((user.UseItems[Grobal2.U_ARMRINGL].Index > 0) && (result == 0))
            {
                pstd = M2Share.UserEngine.GetStdItem(user.UseItems[Grobal2.U_ARMRINGL].Index);
                if (pstd != null)
                {
                    if ((pstd.StdMode == 25) && (pstd.Shape == 5))
                    {
                        // 何利
                        if (HUtil32.MathRound(user.UseItems[Grobal2.U_ARMRINGL].Dura / 100) >= (count - 1))
                        {
                            result = 2;
                        }
                    }
                }
            }
            return result;
        }

        public void SpellNow_UseBujuk(TCreature user)
        {
            TUserHuman hum;
            TStdItem pstd;
            // 2003/03/15 COPARK 酒捞袍 牢亥配府 犬厘
            // 促 敬 何利篮 荤扼柳促.
            if (user.UseItems[Grobal2.U_BUJUK].Index > 0)
            {
                if (user.UseItems[Grobal2.U_BUJUK].Dura < 100)
                {
                    // U_ARMRINGL->U_BUJUK
                    user.UseItems[Grobal2.U_BUJUK].Dura = 0;
                    // 促 敬 何利篮 荤扼柳促.
                    if (user.RaceServer == Grobal2.RC_USERHUMAN)
                    {
                        hum = (TUserHuman)user;
                        hum.SendDelItem(user.UseItems[Grobal2.U_BUJUK]);
                        // 努扼捞攫飘俊 绝绢柳芭 焊晨
                        hum.SysMsg("你的护身符已经耗尽。", 0);
                        // 何利 促 粹疽阑 锭 皋矫瘤(2004/11/15)
                    }
                    user.UseItems[Grobal2.U_BUJUK].Index = 0;
                }
            }
            if (user.UseItems[Grobal2.U_ARMRINGL].Index > 0)
            {
                pstd = M2Share.UserEngine.GetStdItem(user.UseItems[Grobal2.U_ARMRINGL].Index);
                if (pstd != null)
                {
                    if (pstd.StdMode == 25)
                    {
                        if (user.UseItems[Grobal2.U_ARMRINGL].Dura < 100)
                        {
                            user.UseItems[Grobal2.U_ARMRINGL].Dura = 0;
                            if (user.RaceServer == Grobal2.RC_USERHUMAN)
                            {
                                hum = (TUserHuman)user;
                                hum.SendDelItem(user.UseItems[Grobal2.U_ARMRINGL]);
                                hum.SysMsg("你的护身符已经耗尽。", 0);
                            }
                            user.UseItems[Grobal2.U_ARMRINGL].Index = 0;
                        }
                    }
                }
            }
        }

        public bool SpellNow(TCreature user, TUserMagic pum, short xx, short yy, TCreature target, int spell)
        {
            bool result;
            short sx = 0;
            short sy = 0;
            byte ndir;
            int pwr;
            int sec;
            int MoC;
            int Dur;
            int Gap;
            bool train;
            bool nofire;
            bool needfire;
            int bhasitem;
            int bujuckcount;
            TStdItem pstd;
            TUserHuman hum;
            TCreature TempCret;
            result = false;
            if (IsSwordSkill(pum.MagicId))
            {
                return result;
            }
            // 付过 霖厚悼累阑 刚历 焊晨
            user.SendRefMsg(Grobal2.RM_SPELL, pum.pDef.Effect, xx, yy, pum.pDef.MagicId, "");
            if (target != null)
            {
                if (target.Death)
                {
                    target = null;
                }
            }
            train = false;
            nofire = false;
            needfire = true;
            pwr = 0;
            switch (pum.pDef.MagicId)
            {
                case 1:
                case 5:
                    if (user.MagCanHitTarget(user.CX, user.CY, target))
                    {
                        if (user.IsProperTarget(target))
                        {
                            if ((target.AntiMagic <= new System.Random(50).Next()) && (Math.Abs(target.CX - xx) <= 1) && (Math.Abs(target.CY - yy) <= 1))
                            {
                                pwr = user.GetAttackPower(SpellNow_GetPower(pum, MPow(pum)) + HUtil32.LoByte(user.WAbil.MC), HUtil32.HiByte(user.WAbil.MC) - HUtil32.LoByte(user.WAbil.MC) + 1);
                                user.SendDelayMsg(user, Grobal2.RM_DELAYMAGIC, (short)pwr, HUtil32.MakeLong(xx, yy), 2, target.ActorId, "", 600);
                                if (target.RaceServer >= Grobal2.RC_ANIMAL)
                                {
                                    train = true;
                                }
                            }
                            else
                            {
                                target = null;
                            }
                        }
                        else
                        {
                            target = null;
                        }
                    }
                    else
                    {
                        target = null;
                    }
                    break;
                case 37:
                case 8:
                    if (MagPushAround(user, pum.Level) > 0)
                    {
                        train = true;
                    }
                    break;
                case 9:
                    ndir = M2Share.GetNextDirection(user.CX, user.CY, xx, yy);
                    if (M2Share.GetNextPosition(user.PEnvir, user.CX, user.CY, ndir, 1, ref sx, ref sy))
                    {
                        M2Share.GetNextPosition(user.PEnvir, user.CX, user.CY, ndir, 5, ref xx, ref yy);
                        pwr = user.GetAttackPower(SpellNow_GetPower(pum, MPow(pum)) + HUtil32.LoByte(user.WAbil.MC), HUtil32.HiByte(user.WAbil.MC) - HUtil32.LoByte(user.WAbil.MC) + 1);
                        if (user.MagPassThroughMagic(sx, sy, xx, yy, ndir, pwr, false) > 0)
                        {
                            train = true;
                        }
                    }
                    break;
                case 10:
                    ndir = M2Share.GetNextDirection(user.CX, user.CY, xx, yy);
                    if (M2Share.GetNextPosition(user.PEnvir, user.CX, user.CY, ndir, 1, ref sx, ref sy))
                    {
                        M2Share.GetNextPosition(user.PEnvir, user.CX, user.CY, ndir, 8, ref xx, ref yy);
                        pwr = user.GetAttackPower(SpellNow_GetPower(pum, MPow(pum)) + HUtil32.LoByte(user.WAbil.MC), HUtil32.HiByte(user.WAbil.MC) - HUtil32.LoByte(user.WAbil.MC) + 1);
                        if (user.MagPassThroughMagic(sx, sy, xx, yy, ndir, pwr, true) > 0)
                        {
                            train = true;
                        }
                    }
                    break;
                case 35:
                case 11:
                    if (user.IsProperTarget(target))
                    {
                        if (target.AntiMagic <= new System.Random(50).Next())
                        {
                            pwr = user.GetAttackPower(SpellNow_GetPower(pum, MPow(pum)) + HUtil32.LoByte(user.WAbil.MC), HUtil32.HiByte(user.WAbil.MC) - HUtil32.LoByte(user.WAbil.MC) + 1);
                            if (pum.pDef.MagicId == 11)
                            {
                                if (target.LifeAttrib == Grobal2.LA_UNDEAD)
                                {
                                    pwr = HUtil32.MathRound(pwr * 1.5);
                                }
                            }
                            else
                            {
                                if ((target.LifeAttrib != Grobal2.LA_UNDEAD) && (target.RaceServer != Grobal2.RC_USERHUMAN))
                                {
                                    pwr = HUtil32.MathRound(pwr * 1.2);
                                }
                            }
                            user.SendDelayMsg(user, Grobal2.RM_DELAYMAGIC, (short)pwr, HUtil32.MakeLong(xx, yy), 2, target.ActorId, "", 600);
                            if (target.RaceServer >= Grobal2.RC_ANIMAL)
                            {
                                train = true;
                            }
                        }
                        else
                        {
                            target = null;
                        }
                    }
                    else
                    {
                        target = null;
                    }
                    break;
                case 20:
                    // 汾去拜
                    if (user.IsProperTarget(target))
                    {
                        if (MagLightingShock(user, target, xx, yy, pum.Level))
                        {
                            train = true;
                        }
                    }
                    break;
                case 32:
                    // 荤磊辣雀
                    if (user.IsProperTarget(target))
                    {
                        if (MagTurnUndead(user, target, xx, yy, pum.Level))
                        {
                            train = true;
                        }
                    }
                    break;
                case 21:
                    // 酒傍青过
                    user.SendRefMsg(Grobal2.RM_MAGICFIRE, 0, HUtil32.MakeWord(pum.pDef.EffectType, pum.pDef.Effect), HUtil32.MakeLong(xx, yy), target.ActorId, "");
                    needfire = false;
                    if (MagLightingSpaceMove(user, pum.Level))
                    {
                        train = true;
                    }
                    break;
                case 22:
                    // 瘤堪贱
                    if (MagMakeFireCross(user, user.GetAttackPower(SpellNow_GetPower(pum, MPow(pum)) + HUtil32.LoByte(user.WAbil.MC), HUtil32.HiByte(user.WAbil.MC) - HUtil32.LoByte(user.WAbil.MC) + 1), SpellNow_GetPower(pum, 10) + SpellNow_GetRPow(user.WAbil.MC) / 2, xx, yy) > 0)
                    {
                        train = true;
                    }
                    break;
                case 23:
                    // 气凯颇
                    if (MagBigExplosion(user, user.GetAttackPower(SpellNow_GetPower(pum, MPow(pum)) + HUtil32.LoByte(user.WAbil.MC), HUtil32.HiByte(user.WAbil.MC) - HUtil32.LoByte(user.WAbil.MC) + 1), xx, yy, 1))
                    {
                        train = true;
                    }
                    break;
                case 45:
                    // 拳锋扁堪
                    // Random(0.8+(0.5*(Lv_S+1)))*Mcmax)+(1.2*Lv_S)*Mc
                    pwr = (new System.Random(8 + (5 * (pum.Level + 1))).Next() * HUtil32.HiByte(user.WAbil.MC) + 12 * pum.Level * HUtil32.LoByte(user.WAbil.MC)) / 10;
                    if (MagDragonFire(user, pwr, pum.Level))
                    {
                        train = true;
                    }
                    break;
                case 33:
                    // 葫汲浅
                    if (MagBigExplosion(user, user.GetAttackPower(SpellNow_GetPower(pum, MPow(pum)) + HUtil32.LoByte(user.WAbil.MC), HUtil32.HiByte(user.WAbil.MC) - HUtil32.LoByte(user.WAbil.MC) + 1), xx, yy, 1))
                    {
                        train = true;
                    }
                    break;
                case 24:
                    // 汾汲拳
                    if (MagElecBlizzard(user, user.GetAttackPower(SpellNow_GetPower(pum, MPow(pum)) + HUtil32.LoByte(user.WAbil.MC), HUtil32.HiByte(user.WAbil.MC) - HUtil32.LoByte(user.WAbil.MC) + 1)))
                    {
                        train = true;
                    }
                    break;
                case 31:
                    // 林贱狼阜
                    if (user.MagBubbleDefenceUp(pum.Level, SpellNow_GetPower(pum, 15 + SpellNow_GetRPow(user.WAbil.MC))))
                    {
                        train = true;
                    }
                    break;
                case 2:
                    // 雀汗贱
                    if (target == null)
                    {
                        target = user;
                        xx = user.CX;
                        yy = user.CY;
                    }
                    if (user.IsProperFriend(target))
                    {
                        pwr = user.GetAttackPower(SpellNow_GetPower(pum, MPow(pum)) + HUtil32.LoByte(user.WAbil.SC) * 2, (HUtil32.HiByte(user.WAbil.SC) - HUtil32.LoByte(user.WAbil.SC)) * 2 + 1);
                        if (target.WAbil.HP < target.WAbil.MaxHP)
                        {
                            target.SendDelayMsg(user, Grobal2.RM_MAGHEALING, 0, pwr, 0, 0, "", 800);
                            train = true;
                        }
                        if (user.BoAbilSeeHealGauge)
                        {
                            user.SendMsg(target, Grobal2.RM_INSTANCEHEALGUAGE, 0, 0, 0, 0, "");
                        }
                    }
                    break;
                case 29:
                    // 措雀汗贱
                    pwr = user.GetAttackPower(SpellNow_GetPower(pum, MPow(pum)) + HUtil32.LoByte(user.WAbil.SC) * 2, (HUtil32.HiByte(user.WAbil.SC) - HUtil32.LoByte(user.WAbil.SC)) * 2 + 1);
                    if (MagBigHealing(user, pwr, xx, yy))
                    {
                        train = true;
                    }
                    break;
                case 6:
                    // 鞠楷贱
                    nofire = true;
                    bhasitem = 0;
                    pstd = null;
                    if (user.IsProperTarget(target))
                    {
                        // 鞠楷贱篮 刀啊风林赣聪啊 乐绢具 茄促.
                        // 2003/03/15 COPARK 酒捞袍 牢亥配府 犬厘
                        if (user.UseItems[Grobal2.U_BUJUK].Index > 0)
                        {
                            // U_ARMRINGL->U_BUJUK
                            pstd = M2Share.UserEngine.GetStdItem(user.UseItems[Grobal2.U_BUJUK].Index);
                            if (pstd != null)
                            {
                                if ((pstd.StdMode == 25) && (pstd.Shape <= 2))
                                {
                                    // 25:刀林赣聪
                                    if (user.UseItems[Grobal2.U_BUJUK].Dura >= 100)
                                    {
                                        user.UseItems[Grobal2.U_BUJUK].Dura = (short)(user.UseItems[Grobal2.U_BUJUK].Dura - 100);
                                        // 郴备己 函版篮 舅覆
                                        user.SendMsg(user, Grobal2.RM_DURACHANGE, Grobal2.U_BUJUK, user.UseItems[Grobal2.U_BUJUK].Dura, user.UseItems[Grobal2.U_BUJUK].DuraMax, 0, "");
                                        bhasitem = 1;
                                    }
                                }
                            }
                        }
                        if ((user.UseItems[Grobal2.U_ARMRINGL].Index > 0) && (bhasitem == 0))
                        {
                            pstd = M2Share.UserEngine.GetStdItem(user.UseItems[Grobal2.U_ARMRINGL].Index);
                            if (pstd != null)
                            {
                                if ((pstd.StdMode == 25) && (pstd.Shape <= 2))
                                {
                                    // 25:刀林赣聪
                                    if (user.UseItems[Grobal2.U_ARMRINGL].Dura >= 100)
                                    {
                                        user.UseItems[Grobal2.U_ARMRINGL].Dura = (short)(user.UseItems[Grobal2.U_ARMRINGL].Dura - 100);
                                        // 郴备己 函版篮 舅覆
                                        user.SendMsg(user, Grobal2.RM_DURACHANGE, Grobal2.U_ARMRINGL, user.UseItems[Grobal2.U_ARMRINGL].Dura, user.UseItems[Grobal2.U_ARMRINGL].DuraMax, 0, "");
                                        bhasitem = 2;
                                    }
                                }
                            }
                        }
                        if (pstd != null)
                        {
                            if (bhasitem > 0)
                            {
                                // 胶懦沥档俊 蝶扼 己傍咯何啊 搬沥
                                if (6 >= new System.Random(7 + target.AntiPoison).Next())
                                {
                                    switch (pstd.Shape)
                                    {
                                        case 1:
                                            // 雀祸刀啊风: 吝刀
                                            // sec = 吝刀矫埃  60檬 + 舅颇
                                            sec = SpellNow_GetPower13(pum, 30) + 2 * SpellNow_GetRPow(user.WAbil.SC);
                                            // pwr := pum.Level;
                                            pwr = pum.Level + HUtil32._MAX(0, HUtil32._MIN(3, HUtil32._MAX(0, HUtil32.HiByte(user.WAbil.SC) - 30) * 15 / 100));
                                            // wparam
                                            target.SendDelayMsg(user, Grobal2.RM_MAKEPOISON, Grobal2.POISON_DECHEALTH, sec, user.ActorId, pwr, "", 1000);
                                            break;
                                        case 2:
                                            // 炔祸刀啊风: 规绢仿皑家
                                            // 踌刀阑 扒 惑措啊 穿备牢瘤 葛弗 惑怕俊辑 弧刀阑 吧搁 踌刀篮 荤扼柳促.(sonmg 2004/12/27)
                                            if ((target.LastHiter == null) && (target.StatusArr[Grobal2.POISON_DECHEALTH] > 0))
                                            {
                                                target.StatusArr[Grobal2.POISON_DECHEALTH] = 0;
                                            }
                                            // sec = 吝刀矫埃 40檬 + 舅颇
                                            sec = SpellNow_GetPower13(pum, 40) + 2 * SpellNow_GetRPow(user.WAbil.SC);
                                            // (HUtil32.LoByte(user.WAbil.SC) + Random(ShortInt(HUtil32.HiByte(user.WAbil.SC)-HUtil32.LoByte(user.WAbil.SC)) + 1));
                                            // pwr := 2{pum.Level};
                                            pwr = HUtil32._MAX(2, HUtil32._MIN(5, HUtil32.HiByte(user.WAbil.SC) / 10));
                                            // wparam
                                            target.SendDelayMsg(user, Grobal2.RM_MAKEPOISON, Grobal2.POISON_DAMAGEARMOR, sec, user.ActorId, pwr, "", 1000);
                                            break;
                                    }
                                    // -----------------------------------------
                                    // 哎乔 眉农(sonmg 2005/11/28)
                                    if ((target.RaceServer == Grobal2.RC_USERHUMAN) && (user.RaceServer == Grobal2.RC_USERHUMAN))
                                    {
                                        // 沥寸规绢甫 困茄 扁废..
                                        target.AddPkHiter(user);
                                        target.SetLastHiter(user);
                                    }
                                    else if (target.Master != null)
                                    {
                                        // 吝刀等 家券各狼 林牢捞 锭赴荤恩捞 酒聪搁
                                        if (target.Master != user)
                                        {
                                            // 沥寸规绢甫 困茄 扁废..
                                            target.AddPkHiter(user);
                                            target.SetLastHiter(user);
                                        }
                                    }
                                    // -----------------------------------------
                                    // 荤恩,阁胶磐俊霸 吧菌阑锭 荐访等促.
                                    if ((target.RaceServer == Grobal2.RC_USERHUMAN) || (target.RaceServer >= Grobal2.RC_ANIMAL))
                                    {
                                        train = true;
                                    }
                                }
                                user.SelectTarget(target);
                                nofire = false;
                            }
                        }
                        // 促 敬 距篮 荤扼柳促.
                        if (bhasitem == 1)
                        {
                            if (user.UseItems[Grobal2.U_BUJUK].Dura < 100)
                            {
                                user.UseItems[Grobal2.U_BUJUK].Dura = 0;
                                // 促 敬距篮 荤扼柳促.
                                if (user.RaceServer == Grobal2.RC_USERHUMAN)
                                {
                                    hum = (TUserHuman)user;
                                    hum.SendDelItem(user.UseItems[Grobal2.U_BUJUK]);
                                    // 努扼捞攫飘俊 绝绢柳芭 焊晨
                                    hum.SysMsg("The Poison item has been exhausted.", 0);
                                    // 刀啊风 促 粹疽阑 锭 皋矫瘤(2004/11/15)
                                }
                                user.UseItems[Grobal2.U_BUJUK].Index = 0;
                            }
                        }
                        if ((user.UseItems[Grobal2.U_ARMRINGL].Index > 0) && (bhasitem == 2))
                        {
                            pstd = M2Share.UserEngine.GetStdItem(user.UseItems[Grobal2.U_ARMRINGL].Index);
                            if (pstd != null)
                            {
                                if (pstd.StdMode == 25)
                                {
                                    // 25:刀林赣聪
                                    if (user.UseItems[Grobal2.U_ARMRINGL].Dura < 100)
                                    {
                                        user.UseItems[Grobal2.U_ARMRINGL].Dura = 0;
                                        // 促 敬距篮 荤扼柳促.
                                        if (user.RaceServer == Grobal2.RC_USERHUMAN)
                                        {
                                            hum = (TUserHuman)user;
                                            hum.SendDelItem(user.UseItems[Grobal2.U_ARMRINGL]);
                                            // 努扼捞攫飘俊 绝绢柳芭 焊晨
                                            hum.SysMsg("The Poison item has been exhausted.", 0);
                                            // 刀啊风 促 粹疽阑 锭 皋矫瘤(2004/11/15)
                                        }
                                        user.UseItems[Grobal2.U_ARMRINGL].Index = 0;
                                    }
                                }
                            }
                        }
                    }
                    break;
                case 36:
                case 13:
                case 14:
                case 15:
                case 16:
                case 17:
                case 18:
                case 19:
                case 41:
                case 46:
                case 49:
                    // 2003/03/15 脚痹公傍 眠啊
                    // 公必柳扁
                    // 气混拌(档荤)
                    // 亲付柳过
                    // 措瘤盔龋
                    // 搬拌
                    // 归榜家券贱
                    // 篮脚贱
                    // 措篮脚贱
                    // 玫赤家券(沥去家券-岿飞)
                    // 历林贱
                    // 竿救贱(固去贱)
                    nofire = true;
                    bujuckcount = 1;
                    try
                    {
                        switch (pum.pDef.MagicId)
                        {
                            case 41:
                                // 玫赤家券(沥去家券-岿飞)老版快俊绰 5俺 家厚
                                TempCret = user.GetExistSlave(M2Share.__AngelMob);
                                if (TempCret != null)
                                {
                                    TempCret.ForceMoveToMaster = true;
                                    return result;
                                }
                                bujuckcount = 5;
                                break;
                            case 17:
                                TempCret = user.GetExistSlave(M2Share.__WhiteSkeleton);
                                if (TempCret != null)
                                {
                                    TempCret.ForceMoveToMaster = true;
                                    return result;
                                }
                                bujuckcount = 1;
                                break;
                            default:
                                bujuckcount = 1;
                                break;
                        }
                    }
                    catch
                    {
                        M2Share.MainOutMessage("EXCEPTION BUJUCK CALC");
                    }
                    bhasitem = 0;
                    if (bujuckcount > 0)
                    {
                        bhasitem = SpellNow_CanUseBujuk(user, bujuckcount);
                    }
                    if (bhasitem > 0)
                    {
                        if (bhasitem == 1)
                        {
                            // 2003/03/15 COPARK 酒捞袍 牢亥配府 犬厘
                            if (user.UseItems[Grobal2.U_BUJUK].Dura >= (bujuckcount * 100))
                            {
                                // U_ARMRINGL->U_BUJUK
                                user.UseItems[Grobal2.U_BUJUK].Dura = (short)(user.UseItems[Grobal2.U_BUJUK].Dura - (bujuckcount * 100));
                            }
                            else
                            {
                                user.UseItems[Grobal2.U_BUJUK].Dura = 0;
                            }
                            user.SendMsg(user, Grobal2.RM_DURACHANGE, Grobal2.U_BUJUK, user.UseItems[Grobal2.U_BUJUK].Dura, user.UseItems[Grobal2.U_BUJUK].DuraMax, 0, "");
                        }
                        if (bhasitem == 2)
                        {
                            if (user.UseItems[Grobal2.U_ARMRINGL].Dura >= (bujuckcount * 100))
                            {
                                user.UseItems[Grobal2.U_ARMRINGL].Dura = (short)(user.UseItems[Grobal2.U_ARMRINGL].Dura - (bujuckcount * 100));
                            }
                            else
                            {
                                user.UseItems[Grobal2.U_ARMRINGL].Dura = 0;
                            }
                            user.SendMsg(user, Grobal2.RM_DURACHANGE, Grobal2.U_ARMRINGL, user.UseItems[Grobal2.U_ARMRINGL].Dura, user.UseItems[Grobal2.U_ARMRINGL].DuraMax, 0, "");
                        }
                        switch (pum.pDef.MagicId)
                        {
                            case 13:
                                // 气混拌
                                if (user.MagCanHitTarget(user.CX, user.CY, target))
                                {
                                    if (user.IsProperTarget(target))
                                    {
                                        if ((target.AntiMagic <= new System.Random(50).Next()) && (Math.Abs(target.CX - xx) <= 1) && (Math.Abs(target.CY - yy) <= 1))
                                        {
                                            // 颇况
                                            pwr = user.GetAttackPower(SpellNow_GetPower(pum, MPow(pum)) + HUtil32.LoByte(user.WAbil.SC), HUtil32.HiByte(user.WAbil.SC) - HUtil32.LoByte(user.WAbil.SC) + 1);
                                            // 鸥百 嘎澜, 饶俊 瓤苞唱鸥巢
                                            // target.SendDelayMsg (user, RM_MAGSTRUCK, 0, pwr, 0, 0, '', 1200 + HUtil32._MAX(Abs(CX-xx),Abs(CY-yy)) * 50 );
                                            // user.SelectTarget (target);
                                            user.SendDelayMsg(user, Grobal2.RM_DELAYMAGIC, (short)pwr, HUtil32.MakeLong(xx, yy), 2, target.ActorId, "", 1200);
                                            if (target.RaceServer >= Grobal2.RC_ANIMAL)
                                            {
                                                train = true;
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    target = null;
                                }
                                break;
                            case 14:
                                // 亲付柳过
                                pwr = user.GetAttackPower(SpellNow_GetPower13(pum, 60) + 5 * HUtil32.LoByte(user.WAbil.SC), 5 * (HUtil32.HiByte(user.WAbil.SC) - HUtil32.LoByte(user.WAbil.SC) + 1));
                                // 檬
                                if (user.MagMakeDefenceArea(xx, yy, 3, pwr, true) > 0)
                                {
                                    train = true;
                                }
                                break;
                            case 46:
                                // 历林贱
                                sec = (((pum.Level + 1) * 24) + HUtil32.HiByte(user.WAbil.SC) + user.Abil.Level) / 2;
                                switch (pum.Level)
                                {
                                    case 0:
                                        pwr = 93;
                                        break;
                                    case 1:
                                        pwr = 88;
                                        break;
                                    case 2:
                                        pwr = 82;
                                        break;
                                    case 3:
                                        pwr = 75;
                                        break;
                                }
                                if (user.MagMakeCurseArea(xx, yy, 2, sec, pwr, pum.Level, true) > 0)
                                {
                                    train = true;
                                }
                                break;
                            case 36:
                                // 2003/03/15 脚痹公傍 眠啊
                                // 公必柳扁
                                sec = user.GetAttackPower(SpellNow_GetPower13(pum, 60) + 5 * HUtil32.LoByte(user.WAbil.SC), 5 * (HUtil32.HiByte(user.WAbil.SC) - HUtil32.LoByte(user.WAbil.SC) + 1));
                                // 刘啊登绰 颇鲍仿
                                pwr = ((HUtil32.HiByte(user.WAbil.SC) - 1) / 5) + 1;
                                if (pwr > 8)
                                {
                                    pwr = 8;
                                }
                                // 檬
                                if (user.MagDcUp(sec, pwr))
                                {
                                    train = true;
                                }
                                // 鸥南狼 瓷仿摹甫 棵妨淋(sonmg 2005/06/07)
                                if ((target != null) && (target.RaceServer == Grobal2.RC_USERHUMAN))
                                {
                                    // 檬
                                    if (target.MagDcUp(sec, pwr))
                                    {
                                        target.SendRefMsg(Grobal2.RM_LOOPNORMALEFFECT, (short)target.ActorId, 0, 0, Grobal2.NE_BIGFORCE, "");
                                        train = true;
                                    }
                                }
                                break;
                            case 15:
                                // 措瘤盔龋
                                pwr = user.GetAttackPower(SpellNow_GetPower13(pum, 60) + 5 * HUtil32.LoByte(user.WAbil.SC), 5 * (HUtil32.HiByte(user.WAbil.SC) - HUtil32.LoByte(user.WAbil.SC) + 1));
                                // 檬
                                if (user.MagMakeDefenceArea(xx, yy, 3, pwr, false) > 0)
                                {
                                    train = true;
                                }
                                break;
                            case 16:
                                // 搬拌
                                // HUtil32.LoByte(user.WAbil.SC),
                                if (MagMakeHolyCurtain(user, SpellNow_GetPower13(pum, 40) + 3 * SpellNow_GetRPow(user.WAbil.SC), xx, yy) > 0)
                                {
                                    train = true;
                                }
                                break;
                            case 17:
                                // 归榜家券贱
                                try
                                {
                                    TempCret = user.GetExistSlave(M2Share.__WhiteSkeleton);
                                    if (TempCret == null)
                                    {
                                        if ((user.GetExistSlave(M2Share.__ShinSu) == null) && (user.GetExistSlave(M2Share.__ShinSu1) == null))
                                        {
                                            if (user.MakeSlave(M2Share.__WhiteSkeleton, pum.Level, 1, 10 * 24 * 60 * 60) != null)
                                            {
                                                train = true;
                                            }
                                        }
                                    }
                                }
                                catch
                                {
                                    M2Share.MainOutMessage("EXCEPT WHITE SKELETON");
                                }
                                break;
                            case 18:
                                // 篮脚
                                if (MagMakePrivateTransparent(user, SpellNow_GetPower13(pum, 30) + 3 * SpellNow_GetRPow(user.WAbil.SC)))
                                {
                                    train = true;
                                }
                                break;
                            case 19:
                                // 措篮脚
                                if (MagMakeGroupTransparent(user, xx, yy, SpellNow_GetPower13(pum, 30) + 3 * SpellNow_GetRPow(user.WAbil.SC)))
                                {
                                    train = true;
                                }
                                break;
                            case 41:
                                // 玫赤家券贱(沥去家券贱)
                                try
                                {
                                    TempCret = user.GetExistSlave(M2Share.__AngelMob);
                                    if (TempCret == null)
                                    {
                                        // 玫赤(岿飞)啊 绝促.
                                        if (user.MakeSlave(M2Share.__AngelMob, pum.Level, 1, 10 * 24 * 60 * 60) != null)
                                        {
                                            train = true;
                                        }
                                    }
                                }
                                catch
                                {
                                    M2Share.MainOutMessage("EXCEPT ALGEL ");
                                }
                                break;
                            case 49:
                                // 竿救贱(固去贱)
                                if (MagBlindEye(user, target, pum))
                                {
                                    train = true;
                                }
                                break;
                        }
                        nofire = false;
                        SpellNow_UseBujuk(user);
                    }
                    break;
                case 30:
                    // 脚荐家券
                    nofire = true;
                    try
                    {
                        TempCret = user.GetExistSlave(M2Share.__ShinSu);
                        if (TempCret == null)
                        {
                            TempCret = user.GetExistSlave(M2Share.__ShinSu1);
                        }
                        if (TempCret != null)
                        {
                            TempCret.ForceMoveToMaster = true;
                            return result;
                        }
                        bhasitem = SpellNow_CanUseBujuk(user, 5);
                        if (bhasitem > 0)
                        {
                            if (bhasitem == 1)
                            {
                                // 2003/03/15 COPARK 酒捞袍 牢亥配府 犬厘
                                if (user.UseItems[Grobal2.U_BUJUK].Dura >= 500)
                                {
                                    // U_ARMRINGL->U_BUJUK
                                    user.UseItems[Grobal2.U_BUJUK].Dura = (short)(user.UseItems[Grobal2.U_BUJUK].Dura - 500);
                                }
                                else
                                {
                                    user.UseItems[Grobal2.U_BUJUK].Dura = 0;
                                }
                                // 郴备己 函版篮 舅覆
                                user.SendMsg(user, Grobal2.RM_DURACHANGE, Grobal2.U_BUJUK, user.UseItems[Grobal2.U_BUJUK].Dura, user.UseItems[Grobal2.U_BUJUK].DuraMax, 0, "");
                            }
                            if (bhasitem == 2)
                            {
                                if (user.UseItems[Grobal2.U_ARMRINGL].Dura >= 500)
                                {
                                    user.UseItems[Grobal2.U_ARMRINGL].Dura = (short)(user.UseItems[Grobal2.U_ARMRINGL].Dura - 500);
                                }
                                else
                                {
                                    user.UseItems[Grobal2.U_ARMRINGL].Dura = 0;
                                }
                                user.SendMsg(user, Grobal2.RM_DURACHANGE, Grobal2.U_ARMRINGL, user.UseItems[Grobal2.U_ARMRINGL].Dura, user.UseItems[Grobal2.U_ARMRINGL].DuraMax, 0, "");
                            }
                            switch (pum.pDef.MagicId)
                            {
                                case 30:
                                    // 脚荐家券
                                    if (user.GetExistSlave(M2Share.__WhiteSkeleton) == null)
                                    {
                                        if (user.MakeSlave(M2Share.__ShinSu, pum.Level, 1, 10 * 24 * 60 * 60) != null)
                                        {
                                            train = true;
                                        }
                                    }
                                    break;
                            }
                            nofire = false;
                            SpellNow_UseBujuk(user);
                            // 滚弊 荐沥(2004/09/01 sonmg)
                        }
                    }
                    catch
                    {
                        M2Share.MainOutMessage("EXCEPT SHINSU");
                    }
                    break;
                case 42:
                    // 盒脚家券
                    try
                    {
                        TempCret = user.GetExistSlave(M2Share.__CloneMob);
                        if (TempCret != null)
                        {
                            // 盒脚茄逞捞 乐促.
                            TempCret.BoDisapear = true;
                            // MP 促矫 歹秦林磊
                            user.WAbil.MP = (short)(user.WAbil.MP + spell);
                        }
                        else
                        {
                            // 盒脚茄逞捞 绝促.
                            TempCret = user.MakeSlave(M2Share.__CloneMob, pum.Level, 5, 10 * 24 * 60 * 60);
                            if (TempCret != null)
                            {
                                user.SendRefMsg(Grobal2.RM_NORMALEFFECT, 0, TempCret.CX, TempCret.CY, Grobal2.NE_CLONESHOW, "");
                                train = true;
                            }
                        }
                    }
                    catch
                    {
                        M2Share.MainOutMessage("EXCET CLONE");
                    }
                    break;
                case 28:
                    // 沤扁颇楷
                    if (target != null)
                    {
                        if (!target.BoOpenHealth)
                        {
                            if (new System.Random(6).Next() <= 3 + pum.Level)
                            {
                                target.OpenHealthStart = HUtil32.GetTickCount();
                                target.OpenHealthTime = SpellNow_GetPower13(pum, 30 + SpellNow_GetRPow(user.WAbil.SC) * 2) * 1000;
                                target.SendDelayMsg(target, Grobal2.RM_DOOPENHEALTH, 0, 0, 0, 0, "", 1500);
                                train = true;
                            }
                        }
                    }
                    break;
                case 39:
                    // 2003/07/15 脚痹公傍
                    // 搬葫厘
                    if (user.MagCanHitTarget(user.CX, user.CY, target))
                    {
                        if (user.IsProperTarget(target))
                        {
                            if ((target.AntiMagic <= new System.Random(50).Next()) && (Math.Abs(target.CX - xx) <= 1) && (Math.Abs(target.CY - yy) <= 1))
                            {
                                // 搬葫厘 傍侥 荐沥(sonmg 2004/10/20)
                                // Dur := (Round (0.4+pum.Level*0.2) * (HUtil32.LoByte(WAbil.MC) + HUtil32.HiByte(WAbil.MC)));
                                Dur = HUtil32.MathRound(0.4 + pum.Level * 0.2) * (HUtil32.LoByte(user.WAbil.MC) + new System.Random(HUtil32.HiByte(user.WAbil.MC)).Next() + (HUtil32.HiByte(user.WAbil.MC) / 2));
                                pwr = pum.pDef.MinPower + Dur;
                                user.SendDelayMsg(user, Grobal2.RM_DELAYMAGIC, (short)pwr, HUtil32.MakeLong(xx, yy), 2, target.ActorId, "", 600);
                                // 惑怕捞惑...敌拳魄沥
                                if ((target.Abil.Level < 60) && (target.StatusArr[Grobal2.POISON_SLOW] == 0) && (target.StatusArr[Grobal2.POISON_ICE] == 0) && (new System.Random(50).Next() > target.AntiMagic))
                                {
                                    // 100->50
                                    MoC = 1;
                                    Gap = target.Abil.Level - user.Abil.Level;
                                    if (Gap > 10)
                                    {
                                        Gap = 10;
                                    }
                                    if (Gap < -10)
                                    {
                                        Gap = -10;
                                    }
                                    if (target.RaceServer == Grobal2.RC_USERHUMAN)
                                    {
                                        MoC = 2;
                                    }
                                    if (new System.Random(100).Next() < (20 + (pum.Level - Gap) / MoC))
                                    {
                                        Dur = (900 * pum.Level + 3300) / 1000;
                                        if ((MoC == 1) && (new System.Random(10).Next() == 0))
                                        {
                                            target.MakePoison(Grobal2.POISON_ICE, Dur, 1);
                                        }
                                        else if ((MoC == 2) && (new System.Random(100).Next() == 0))
                                        {
                                            target.MakePoison(Grobal2.POISON_ICE, Dur, 1);
                                        }
                                        else
                                        {
                                            target.MakePoison(Grobal2.POISON_SLOW, Dur + 1, 1);
                                        }
                                    }
                                }
                                if (target.RaceServer >= Grobal2.RC_ANIMAL)
                                {
                                    train = true;
                                }
                            }
                            else
                            {
                                target = null;
                            }
                        }
                        else
                        {
                            target = null;
                        }
                    }
                    else
                    {
                        target = null;
                    }
                    break;
                case 40:
                    // 沥拳贱
                    if (MagMakePrivateClean(user, xx, yy, pum.Level * 15 + 45))
                    {
                        train = true;
                    }
                    break;
                case 43:
                    // 荤磊饶
                    try
                    {
                        // nofire := true;   //sonmg(2004/05/19)
                        user._Attack(Grobal2.HM_STONEHIT, null);
                    }
                    catch
                    {
                        M2Share.MainOutMessage("EXCEPTION STONEHIT");
                    }
                    train = false;
                    break;
                case 44:
                    // 傍颇级
                    if (MagWindCut(user, pum.Level))
                    {
                        train = true;
                    }
                    break;
                case 47:
                    // 器铰八
                    if (MagPullMon(user, target, pum.Level))
                    {
                        train = true;
                    }
                    break;
                case 48:
                    // 软趋贱
                    if (user.IsProperTarget(target))
                    {
                        if (target.AntiMagic <= new System.Random(50).Next())
                        {
                            pwr = user.GetAttackPower(SpellNow_GetPower(pum, MPow(pum)) + HUtil32.LoByte(user.WAbil.MC), HUtil32.HiByte(user.WAbil.MC) - HUtil32.LoByte(user.WAbil.MC) + 1);
                            if ((target.LifeAttrib != Grobal2.LA_UNDEAD) && (target.RaceServer != Grobal2.RC_USERHUMAN))
                            {
                                pwr = HUtil32.MathRound(pwr * 1.2);
                            }
                            user.SendDelayMsg(user, Grobal2.RM_DELAYMAGIC, (short)pwr, HUtil32.MakeLong(xx, yy), 2, target.ActorId, "", 0);
                            if (target.RaceServer >= Grobal2.RC_ANIMAL)
                            {
                                user.IncHealth = (byte)((pwr * (pum.Level + 1) * 10 + new System.Random(20).Next()) / 100);
                                train = true;
                            }
                            else
                            {
                                user.IncHealth = (byte)((pwr * (pum.Level + 1) * 10 + new System.Random(10).Next()) / 100 * HUtil32._MAX(0, 1 - target.AntiMagic / 25));
                            }
                            user.SendDelayMsg(user, Grobal2.RM_LOOPNORMALEFFECT, (short)user.ActorId, 0, 0, Grobal2.NE_BLOODSUCK, "", 1000);
                        }
                        else
                        {
                            target = null;
                        }
                    }
                    else
                    {
                        target = null;
                    }
                    break;
            }
            if (!nofire)
            {
                if (needfire)
                {
                    user.SendRefMsg(Grobal2.RM_MAGICFIRE, 0, HUtil32.MakeWord(pum.pDef.EffectType, pum.pDef.Effect), HUtil32.MakeLong(xx, yy), target.ActorId, "");
                }
                if ((pum.Level < 3) && train)
                {
                    if (user.Abil.Level >= pum.pDef.NeedLevel[pum.Level])
                    {
                        user.TrainSkill(pum, 1 + new System.Random(3).Next());
                        if (!user.CheckMagicLevelup(pum))
                        {
                            user.SendDelayMsg(user, Grobal2.RM_MAGIC_LVEXP, 0, pum.pDef.MagicId, pum.Level, pum.CurTrain, "", 1000);
                        }
                    }
                }
                result = true;
            }
            return result;
        }
    }
}
