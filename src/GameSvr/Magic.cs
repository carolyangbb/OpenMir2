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
                                // ¼ö·ÃÁ¤µµ¿¡ µû¶ó¼­
                                if (user.IsProperTarget(cret))
                                {
                                    push = 1 + _MAX(0, pushlevel - 1) + new System.Random(2).Next();
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
                                            target.WAbil.HP = (ushort)(target.WAbil.HP / 10);
                                        }
                                        target.Master = user;
                                        target.MasterRoyaltyTime = GetTickCount + ((long)20 + shocklevel * 20 + new System.Random(user.Abil.Level * 2).Next()) * 60 * 1000;
                                        target.SlaveMakeLevel = (byte)shocklevel;
                                        if (target.SlaveLifeTime == 0)
                                        {
                                            target.SlaveLifeTime = GetTickCount;
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
                    ((TAnimal)target).RunAwayStart = GetTickCount;
                    ((TAnimal)target).RunAwayTime = 10 * 1000;
                }
                user.SelectTarget(target);
                if ((target.Abil.Level < (_MIN(user.Abil.Level, 51) - 1 + new System.Random(4).Next())) && (target.Abil.Level < M2Share.MAXKINGLEVEL - 1))
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
                    // ¾Æ°øÇà¹ý »ç¿ëºÒ°¡(sonmg)
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
                            phs = M2Share.THolySeizeInfo = new THolySeizeInfo();
                            //FillChar(phs, sizeof(THolySeizeInfo), '\0');
                            phs.seizelist = new ArrayList();
                            phs.OpenTime = GetTickCount;
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
                    svMain.EventMan.AddEvent(__event);
                    phs.earr[0] = __event;
                    __event = new THolyCurtainEvent(user.PEnvir, x + 1, y - 2, Grobal2.ET_HOLYCURTAIN, htime * 1000);
                    svMain.EventMan.AddEvent(__event);
                    phs.earr[1] = __event;
                    __event = new THolyCurtainEvent(user.PEnvir, x - 2, y - 1, Grobal2.ET_HOLYCURTAIN, htime * 1000);
                    svMain.EventMan.AddEvent(__event);
                    phs.earr[2] = __event;
                    __event = new THolyCurtainEvent(user.PEnvir, x + 2, y - 1, Grobal2.ET_HOLYCURTAIN, htime * 1000);
                    svMain.EventMan.AddEvent(__event);
                    phs.earr[3] = __event;
                    __event = new THolyCurtainEvent(user.PEnvir, x - 2, y + 1, Grobal2.ET_HOLYCURTAIN, htime * 1000);
                    svMain.EventMan.AddEvent(__event);
                    phs.earr[4] = __event;
                    __event = new THolyCurtainEvent(user.PEnvir, x + 2, y + 1, Grobal2.ET_HOLYCURTAIN, htime * 1000);
                    svMain.EventMan.AddEvent(__event);
                    phs.earr[5] = __event;
                    __event = new THolyCurtainEvent(user.PEnvir, x - 1, y + 2, Grobal2.ET_HOLYCURTAIN, htime * 1000);
                    svMain.EventMan.AddEvent(__event);
                    phs.earr[6] = __event;
                    __event = new THolyCurtainEvent(user.PEnvir, x + 1, y + 2, Grobal2.ET_HOLYCURTAIN, htime * 1000);
                    svMain.EventMan.AddEvent(__event);
                    phs.earr[7] = __event;
                    svMain.UserEngine.HolySeizeList.Add(phs);
                    // °á°èÃß°¡
                }
                else
                {
                    if (phs != null)
                    {
                        phs.seizelist.Free();
                        Dispose(phs);
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
                svMain.EventMan.AddEvent(__event);
            }
            if (user.PEnvir.GetEvent(x - 1, y) == null)
            {
                __event = new TFireBurnEvent(user, x - 1, y, Grobal2.ET_FIRE, htime * 1000, dam);
                svMain.EventMan.AddEvent(__event);
            }
            if (user.PEnvir.GetEvent(x, y) == null)
            {
                __event = new TFireBurnEvent(user, x, y, Grobal2.ET_FIRE, htime * 1000, dam);
                svMain.EventMan.AddEvent(__event);
            }
            if (user.PEnvir.GetEvent(x + 1, y) == null)
            {
                __event = new TFireBurnEvent(user, x + 1, y, Grobal2.ET_FIRE, htime * 1000, dam);
                svMain.EventMan.AddEvent(__event);
            }
            if (user.PEnvir.GetEvent(x, y + 1) == null)
            {
                __event = new TFireBurnEvent(user, x, y + 1, Grobal2.ET_FIRE, htime * 1000, dam);
                svMain.EventMan.AddEvent(__event);
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
                    // ¾ðµ¥µå °è¿­ ¸ó½ºÅÍ¿¡°Ô °ø°Ý·ÂÀÌ ÀÖÀ½
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

        // ¼úÀÚ°¡ Àº½ÅÀÌ µÈ´Ù. ³ª¸¦ °ø°ÝÇÏ°í ÀÖ´Â ¸÷µéÀÌ ³ª¸¦ ¸ô¶óº»´Ù.
        // ¼úÀÚ°¡ ¶§¸®¸é °ø°ÝÇÔ.
        // ¼úÀÚ°¡ ÀÌµ¿ÇÏ¸é ÇØÁ¦µÈ´Ù.
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
            // ÀÌ¹Ì Åõ¸í
            rlist = new ArrayList();
            user.GetMapCreatures(user.PEnvir, user.CX, user.CY, 9, rlist);
            for (i = 0; i < rlist.Count; i++)
            {
                cret = (TCreature)rlist[i];
                if (cret.RaceServer >= Grobal2.RC_ANIMAL)
                {
                    if (cret.TargetCret == user)
                    {
                        // °¡±îÀÌ ÀÖ´Â³ðÀº 1/2 Àº ³Ñ¾î°£´Ù.
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
            // ÇÑ ÀÚ¸®¿¡¼­¸¸ Àº½Å°¡´É, ÀÌµ¿ÇÏ¸é Àº½ÅÀÌ Ç®¸°´Ù.
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
                        // Åõ¸íÀÌ ¾Æ´Ñ »óÅÂ
                        cret.SendDelayMsg(cret, Grobal2.RM_TRANSPARENT, 0, htime, 0, 0, "", 800);
                        // if MagMakePrivateTransparent (cret, htime) then
                        result = true;
                    }
                }
            }
            rlist.Free();
            return result;
        }

        // 2003/07/15 ½Å±Ô¹«°ø
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
                    target.SendDelayMsg((TCreature)Grobal2.RM_STRUCK, Grobal2.RM_REFMESSAGE, dam, target.WAbil.HP, target.WAbil.MaxHP, user.ActorId, "", 200);
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
            // ¹üÀ§ÀÇ ÁÜ½ÉÀÌ µÇ´Â ÁÂÇ¥ º¯°æ
            xx = user.CX;
            yy = user.CY;
            f1x = xx;
            f1y = yy;
            f2x = xx;
            f2y = yy;
            switch (user.Dir)
            {
                case 0:
                    // °­ÇÏ°Ô Å¸°ÝÀÌ µé¾î°¡´Â ÁÂÇ¥ ¼³Á¤
                    f1x = xx;
                    f1y = yy - 1;
                    f2x = xx;
                    f2y = yy - 2;
                    // Áß¾ÓÁÂÇ¥ ¼³Á¤
                    yy = yy - 2;
                    user.GetMapCreatures(user.PEnvir, xx, yy, 1, rlist);
                    break;
                case 1:
                    // °­ÇÏ°Ô Å¸°ÝÀÌ µé¾î°¡´Â ÁÂÇ¥ ¼³Á¤
                    f1x = xx + 1;
                    f1y = yy - 1;
                    f2x = xx + 2;
                    f2y = yy - 2;
                    // Áß¾ÓÁÂÇ¥ ¼³Á¤
                    // xx := xx + 1;
                    // yy := yy - 1;
                    xx = xx + 2;
                    yy = yy - 2;
                    user.GetObliqueMapCreatures(user.PEnvir, xx, yy, 1, user.Dir, rlist);
                    break;
                case 2:
                    // °­ÇÏ°Ô Å¸°ÝÀÌ µé¾î°¡´Â ÁÂÇ¥ ¼³Á¤
                    f1x = xx + 1;
                    f1y = yy;
                    f2x = xx + 2;
                    f2y = yy;
                    // Áß¾ÓÁÂÇ¥ ¼³Á¤
                    xx = xx + 2;
                    user.GetMapCreatures(user.PEnvir, xx, yy, 1, rlist);
                    break;
                case 3:
                    // °­ÇÏ°Ô Å¸°ÝÀÌ µé¾î°¡´Â ÁÂÇ¥ ¼³Á¤
                    f1x = xx + 1;
                    f1y = yy + 1;
                    f2x = xx + 2;
                    f2y = yy + 2;
                    // Áß¾ÓÁÂÇ¥ ¼³Á¤
                    // xx := xx + 1;
                    // yy := yy + 1;
                    xx = xx + 2;
                    yy = yy + 2;
                    user.GetObliqueMapCreatures(user.PEnvir, xx, yy, 1, user.Dir, rlist);
                    break;
                case 4:
                    // °­ÇÏ°Ô Å¸°ÝÀÌ µé¾î°¡´Â ÁÂÇ¥ ¼³Á¤
                    f1x = xx;
                    f1y = yy + 1;
                    f2x = xx;
                    f2y = yy + 2;
                    // Áß¾ÓÁÂÇ¥ ¼³Á¤
                    yy = yy + 2;
                    user.GetMapCreatures(user.PEnvir, xx, yy, 1, rlist);
                    break;
                case 5:
                    // °­ÇÏ°Ô Å¸°ÝÀÌ µé¾î°¡´Â ÁÂÇ¥ ¼³Á¤
                    f1x = xx - 1;
                    f1y = yy + 1;
                    f2x = xx - 2;
                    f2y = yy + 2;
                    // Áß¾ÓÁÂÇ¥ ¼³Á¤
                    // xx := xx - 1;
                    // yy := yy + 1;
                    xx = xx - 2;
                    yy = yy + 2;
                    user.GetObliqueMapCreatures(user.PEnvir, xx, yy, 1, user.Dir, rlist);
                    break;
                case 6:
                    // °­ÇÏ°Ô Å¸°ÝÀÌ µé¾î°¡´Â ÁÂÇ¥ ¼³Á¤
                    f1x = xx - 1;
                    f1y = yy;
                    f2x = xx - 2;
                    f2y = yy;
                    // Áß¾ÓÁÂÇ¥ ¼³Á¤
                    xx = xx - 2;
                    user.GetMapCreatures(user.PEnvir, xx, yy, 1, rlist);
                    break;
                case 7:
                    // °­ÇÏ°Ô Å¸°ÝÀÌ µé¾î°¡´Â ÁÂÇ¥ ¼³Á¤
                    f1x = xx - 1;
                    f1y = yy - 1;
                    f2x = xx - 2;
                    f2y = yy - 2;
                    // Áß¾ÓÁÂÇ¥ ¼³Á¤
                    // xx := xx - 1;
                    // yy := yy - 1;
                    xx = xx - 2;
                    yy = yy - 2;
                    user.GetObliqueMapCreatures(user.PEnvir, xx, yy, 1, user.Dir, rlist);
                    break;
            }
            // ¸ð¼ÇÀ» ÃëÇÏ°Ô ÇÑ´ÙÀ½¿¡
            // user.HitMotion( RM_HIT, user.Dir, user.CX, user.CY);
            // user.SendRefMsg( RM_WINDCUT , user.Dir , user.CX , user.CY , 0, '');
            // Àåºñ Çà¿îÄ¡·Î Å©¸®Æ¼ÄÃ È®·ü °áÁ¤
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
                    // Å¸°ÝÀÌ °­ÇÏ°Ô µé¾î°¡¾ßµÉ ³Ñ°ú ¾àÇÏ°Ô µé¾î°¡¾ßµÉ ³Ñ ÆÇ´Ü.
                    if (((cret.CX == f1x) && (cret.CY == f1y)) || ((cret.CX == f2x) && (cret.CY == f2y)))
                    {
                        isnear = true;
                    }
                    else
                    {
                        isnear = false;
                    }
                    // Å©¸®Æ¼ÄÃ µ¥¹ÌÁö
                    if (CriticalDamage)
                    {
                        DcRandom = HiByte(user.WAbil.DC);
                    }
                    else
                    {
                        DcRandom = new System.Random(HiByte(user.WAbil.DC)).Next();
                    }
                    // Àü¹æ 1*2ÀÇ ¹üÀ§:
                    // ((1.2+0.3*(Lv_S+(LV/20)) * Random(Dcmax)/3+DC
                    // ±× ¿Ü ¹üÀ§: ((0.8+0.2*(Lv_S+LV/20)) * Random(Dcmax)/3+DC
                    // Å¸°ÝÄ¡°¡ ´Ù¸¥°Ô Àû¿ëµÊ
                    if (isnear)
                    {
                        pwr = (12 + 3 * (skilllevel + user.Abil.Level / 20)) * DcRandom / 30 + Lobyte(user.WAbil.DC);
                    }
                    else
                    {
                        pwr = (8 + 2 * (skilllevel + user.Abil.Level / 20)) * DcRandom / 30 + Lobyte(user.WAbil.DC);
                    }
                    // Å©¸®Æ¼ÄÃ µ¥¹ÌÁö
                    if (CriticalDamage)
                    {
                        pwr = pwr * 2;
                    }
                    if (pwr > 0)
                    {
                        // Å×½ºÆ®¿ë
                        // 
                        // if isnear then
                        // cret.MakePoison( POISON_STONE , 2 ,1 )
                        // else
                        // cret.MakePoison( POISON_SLOW , 2 ,1 );
                        WindCutHit(user, cret, pwr, 0);
                        result = true;
                    }
                }
            }
            rlist.Free();
            return result;
        }

        // 2004/06/23 ½Å±Ô¹«°ø
        // Æ÷½Â°Ë
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
                // »ç¶÷ÇÑÅ× ¾µ ¼ö ¾øÀ½.
                if (target.RaceServer == Grobal2.RC_USERHUMAN)
                {
                    return result;
                }
                // ¿òÁ÷ÀÌÁö ¾Ê´Â ¸ó½ºÅÍ´Â ²ø¾î¿Ã ¼ö ¾øÀ½(2004/12/01)
                if (target.StickMode)
                {
                    return result;
                }
                // ³Ê¹« °¡±îÀÌ ÀÖÀ¸¸é ±â¼úÀ» ¾µ ¼ö ¾øÀ½.
                if ((Math.Abs(user.CX - target.CX) < 3) && (Math.Abs(user.CY - target.CY) < 3))
                {
                    user.SysMsg("Target is too close.", 0);
                    return result;
                }
                else if ((Math.Abs(user.CX - target.CX) > 7) && (Math.Abs(user.CY - target.CY) > 7))
                {
                    // user.SysMsg('»ó´ë°¡ ³Ê¹« ¸Ö¸® ÀÖ½À´Ï´Ù.', 0);
                    return result;
                }
                // ¸Ö¸®ÀÖ´Â ÀûÀ» ²ø¾î´ç±ä´Ù.
                user.Dir = M2Share.GetNextDirectionNew(user.CX, user.CY, target.CX, target.CY);
                // user.SendRefMsg (RM_LIGHTING, user.Dir, user.CX, user.CY, Integer(target), '');
                // ¹æÇâº° ²ø¾î´ç±â´Â °Å¸® Á¶Àý
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
                    rushDist = _MAX(0, _MIN(Math.Abs(user.CX - target.CX) - 2, Math.Abs(user.CY - target.CY) - 2));
                }
                if (user.IsProperTarget(target))
                {
                    // ½ÃÃ¼°¡ ¾Æ´Ï°í ²¿ºÀ¸÷ÀÌ ¾Æ´Ï¾î¾ß ÇÔ.
                    // if (not target.Death) and (target.Master = nil) then begin
                    // ½ÃÃ¼°¡ ¾Æ´Ï¾î¾ß ÇÔ. ²¿ºÀ¸÷Àº °¡´ÉÇÏ°Ô ¼öÁ¤(sonmg 2005/11/2)
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
                                // Á÷¼±¿¡ ÀÖ´Â³Ñ¸¸ ¶¯±ä´Ù.(1Ä­¾¿ ÁÂ¿ì·Î ÀÖ´Â ³Ñµµ ¶¯±ä´Ù)
                                if ((user.CX == target.CX) || (user.CY == target.CY) || (Math.Abs(user.CX - target.CX) == Math.Abs(user.CY - target.CY)) || (user.CX + 1 == target.CX) || (user.CY + 1 == target.CY) || (Math.Abs(Math.Abs(user.CX - target.CX) - Math.Abs(user.CY - target.CY)) == 1) || (user.CX - 1 == target.CX) || (user.CY - 1 == target.CY))
                                {
                                    rush = rushDist;
                                    target.CharDrawingRush(rushdir, rush, false);
                                    if (target.RaceServer != Grobal2.RC_USERHUMAN)
                                    {
                                        Dur = HUtil32.MathRound((skilllevel + 1) * 1.6) + _MAX(1, 10 - target.SpeedPoint);
                                    }
                                    else
                                    {
                                        Dur = HUtil32.MathRound((skilllevel + 1) * 0.8) + _MAX(1, 10 - target.SpeedPoint);
                                        if (user.RaceServer == Grobal2.RC_USERHUMAN)
                                        {
                                            // Á¤´ç¹æ¾î¸¦ À§ÇÑ ±â·Ï..
                                            target.AddPkHiter(user);
                                        }
                                    }
                                    target.MakePoison(Grobal2.POISON_STONE, Dur, 1);
                                    target.SendRefMsg(Grobal2.RM_LOOPNORMALEFFECT, (ushort)target.ActorId, 1500, 0, Grobal2.NE_MONCAPTURE, "");
                                    result = true;
                                }
                                else
                                {
                                    // user.SysMsg('²ø¾î ´ç±æ ¼ö ¾ø´Â À§Ä¡¿¡ ÀÖ½À´Ï´Ù.', 0);
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
            if (HiByte(pw) > Lobyte(pw))
            {
                result = Lobyte(pw) + new System.Random(HiByte(pw) - Lobyte(pw) + 1).Next();
            }
            else
            {
                result = Lobyte(pw);
            }
            return result;
        }

        public int MagBlindEye_GetPower(int pw)
        {
            int result;
            // ¼ö·Ã 0 ´Ü°è¿¡¼­´Â 1/4ÀÇ ÆÄ¿öÀÓ
            result = HUtil32.MathRound(pw / (pum.pDef.MaxTrainLevel + 1) * (pum.Level + 1)) + pum.pDef.DefMinPower + new System.Random(pum.pDef.DefMaxPower - pum.pDef.DefMinPower).Next();
            return result;
        }

        public int MagBlindEye_GetPower13(int pw)
        {
            int result;
            // ¼ö·Ã 0 ´Ü°è¿¡µµ 1/3ÀÇ ÆÄ¿ö°¡ ³²
            double p1;
            double p2;
            p1 = pw / 3;
            p2 = pw - p1;
            result = HUtil32.MathRound(p1 + p2 / (pum.pDef.MaxTrainLevel + 1) * (pum.Level + 1) + pum.pDef.DefMinPower + new System.Random(pum.pDef.DefMaxPower - pum.pDef.DefMinPower).Next());
            return result;
        }

        public bool MagBlindEye(TCreature user, TCreature target, TUserMagic pum)
        {
            bool result;
            int pwr;
            int levelgap;
            // flag : Boolean;
            result = false;
            // flag := FALSE;
            if (user.IsProperTarget(target))
            {
                // ¸ó½ºÅÍ¿¡¸¸ °É¸².
                if (target.RaceServer >= Grobal2.RC_ANIMAL)
                {
                    // ½ºÅ³Á¤µµ¿¡ µû¶ó ¼º°ø¿©ºÎ°¡ °áÁ¤
                    levelgap = user.Abil.Level - target.Abil.Level;
                    if ((20 - (pum.Level + 1) * 2) <= new System.Random(MagBlindEye_GetRPow((short)user.WAbil.SC) + (user.Abil.Level / 5) + (levelgap * 2)).Next())
                    {
                        // (»ó´ë¹æ ·¹º§ < ½ÃÀüÀÚ ·¹º§+1+Random(3)) and (»ó´ë¹æ·¹º§ < 55)
                        if ((target.Abil.Level < user.Abil.Level + 1 + new System.Random(3).Next()) && (target.Abil.Level < 55))
                        {
                            if (target.BoGoodCrazyMode == false)
                            {
                                // Áßº¹ÇØ¼­ °É¸®Áö ¾Ê´Â´Ù.
                                // pwr = ÆøÁÖ½Ã°£
                                pwr = MagBlindEye_GetPower13(10) + HUtil32.MathRound(MagBlindEye_GetRPow((short)user.WAbil.SC) / 3);
                                pwr = pwr + new System.Random(20).Next();
                                target.TargetCret = null;
                                target.MakeGoodCrazyMode(pwr);
                                // target.SendRefMsg (RM_LOOPNORMALEFFECT, integer(target), pwr * 1000, 0, NE_BLINDEFFECT, '');
                                // flag := TRUE;
                                // user.SysMsg('¶°µµ´Â ¿µÈ¥À» ºùÀÇ½ÃÄ×½À´Ï´Ù.', 1);
                            }
                        }
                        result = true;
                    }
                    user.SelectTarget(target);
                }
            }
            // if flag = FALSE then
            // user.SysMsg('¶°µµ´Â ¿µÈ¥À» ºùÀÇ½ÃÅ°Áö ¸øÇß½À´Ï´Ù.', 0);

            return result;
        }

        public byte SpellNow_GetRPow(short pw)
        {
            byte result;
            if (HiByte(pw) > Lobyte(pw))
            {
                result = Lobyte(pw) + new System.Random(HiByte(pw) - Lobyte(pw) + 1).Next();
            }
            else
            {
                result = Lobyte(pw);
            }
            return result;
        }

        public int SpellNow_GetPower(int pw)
        {
            int result;
            // ¼ö·Ã 0 ´Ü°è¿¡¼­´Â 1/4ÀÇ ÆÄ¿öÀÓ
            result = HUtil32.MathRound(pw / (pum.pDef.MaxTrainLevel + 1) * (pum.Level + 1)) + pum.pDef.DefMinPower + new System.Random(pum.pDef.DefMaxPower - pum.pDef.DefMinPower).Next();
            return result;
        }

        public int SpellNow_GetPower13(int pw)
        {
            int result;
            // ¼ö·Ã 0 ´Ü°è¿¡µµ 1/3ÀÇ ÆÄ¿ö°¡ ³²
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
            // 2003/03/15 COPARK ¾ÆÀÌÅÛ ÀÎº¥Åä¸® È®Àå
            if (user.UseItems[Grobal2.U_BUJUK].Index > 0)
            {
                // U_ARMRINGL->U_BUJUK
                pstd = svMain.UserEngine.GetStdItem(user.UseItems[Grobal2.U_BUJUK].Index);
                if (pstd != null)
                {
                    if ((pstd.StdMode == 25) && (pstd.Shape == 5))
                    {
                        // ºÎÀû
                        if (HUtil32.MathRound(user.UseItems[Grobal2.U_BUJUK].Dura / 100) >= (count - 1))
                        {
                            result = 1;
                        }
                    }
                }
            }
            if ((user.UseItems[Grobal2.U_ARMRINGL].Index > 0) && (result == 0))
            {
                pstd = svMain.UserEngine.GetStdItem(user.UseItems[Grobal2.U_ARMRINGL].Index);
                if (pstd != null)
                {
                    if ((pstd.StdMode == 25) && (pstd.Shape == 5))
                    {
                        // ºÎÀû
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
            // 2003/03/15 COPARK ¾ÆÀÌÅÛ ÀÎº¥Åä¸® È®Àå
            // ´Ù ¾´ ºÎÀûÀº »ç¶óÁø´Ù.
            if (user.UseItems[Grobal2.U_BUJUK].Index > 0)
            {
                if (user.UseItems[Grobal2.U_BUJUK].Dura < 100)
                {
                    // U_ARMRINGL->U_BUJUK
                    user.UseItems[Grobal2.U_BUJUK].Dura = 0;
                    // ´Ù ¾´ ºÎÀûÀº »ç¶óÁø´Ù.
                    if (user.RaceServer == Grobal2.RC_USERHUMAN)
                    {
                        hum = (TUserHuman)user;
                        hum.SendDelItem(user.UseItems[Grobal2.U_BUJUK]);
                        // Å¬¶óÀÌ¾ðÆ®¿¡ ¾ø¾îÁø°Å º¸³¿
                        hum.SysMsg("ÄãµÄ»¤Éí·ûÒÑ¾­ºÄ¾¡¡£", 0);
                        // ºÎÀû ´Ù ´â¾ÒÀ» ¶§ ¸Þ½ÃÁö(2004/11/15)
                    }
                    user.UseItems[Grobal2.U_BUJUK].Index = 0;
                }
            }
            if (user.UseItems[Grobal2.U_ARMRINGL].Index > 0)
            {
                pstd = svMain.UserEngine.GetStdItem(user.UseItems[Grobal2.U_ARMRINGL].Index);
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
                                hum.SysMsg("ÄãµÄ»¤Éí·ûÒÑ¾­ºÄ¾¡¡£", 0);
                            }
                            user.UseItems[Grobal2.U_ARMRINGL].Index = 0;
                        }
                    }
                }
            }
        }

        public bool SpellNow(TCreature user, TUserMagic pum, int xx, int yy, TCreature target, int spell)
        {
            bool result;
            int sx;
            int sy;
            int ndir;
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
            // ¸¶¹ý ÁØºñµ¿ÀÛÀ» ¸ÕÀú º¸³¿
            user.SendRefMsg(Grobal2.RM_SPELL, pum.pDef.Effect, xx, yy, pum.pDef.MagicId, "");
            if (target != null)
            {
                if (target.Death)
                {
                    // Å¸°ÙÀÌ Á×Àº°æ¿ì.....
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
                    // È­¿°Àå
                    // ±Ý°­È­¿°Àå
                    if (user.MagCanHitTarget(user.CX, user.CY, target))
                    {
                        if (user.IsProperTarget(target))
                        {
                            if ((target.AntiMagic <= new System.Random(50).Next()) && (Math.Abs(target.CX - xx) <= 1) && (Math.Abs(target.CY - yy) <= 1))
                            {
                                pwr = user.GetAttackPower(SpellNow_GetPower(MPow(pum)) + user.Lobyte(user.WAbil.MC), (short)HiByte(user.WAbil.MC) - user.Lobyte(user.WAbil.MC) + 1);
                                user.SendDelayMsg(user, Grobal2.RM_DELAYMAGIC, (ushort)pwr, HUtil32.MakeLong(xx, yy), 2, target.ActorId, "", 600);
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
                        pwr = user.GetAttackPower(SpellNow_GetPower(MPow(pum)) + user.Lobyte(user.WAbil.MC), (short)HiByte(user.WAbil.MC) - user.Lobyte(user.WAbil.MC) + 1);
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
                        pwr = user.GetAttackPower(SpellNow_GetPower(MPow(pum)) + user.Lobyte(user.WAbil.MC), (short)HiByte(user.WAbil.MC) - user.Lobyte(user.WAbil.MC) + 1);
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
                            pwr = user.GetAttackPower(SpellNow_GetPower(MPow(pum)) + user.Lobyte(user.WAbil.MC), (short)HiByte(user.WAbil.MC) - user.Lobyte(user.WAbil.MC) + 1);
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
                            user.SendDelayMsg(user, Grobal2.RM_DELAYMAGIC, (ushort)pwr, HUtil32.MakeLong(xx, yy), 2, target.ActorId, "", 600);
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
                    // ·ÚÈ¥°Ý
                    if (user.IsProperTarget(target))
                    {
                        if (MagLightingShock(user, target, xx, yy, pum.Level))
                        {
                            train = true;
                        }
                    }
                    break;
                case 32:
                    // »çÀÚÀ±È¸
                    if (user.IsProperTarget(target))
                    {
                        if (MagTurnUndead(user, target, xx, yy, pum.Level))
                        {
                            train = true;
                        }
                    }
                    break;
                case 21:
                    // ¾Æ°øÇà¹ý
                    user.SendRefMsg(Grobal2.RM_MAGICFIRE, 0, MakeWord(pum.pDef.EffectType, pum.pDef.Effect), HUtil32.MakeLong(xx, yy), target.ActorId, "");
                    needfire = false;
                    if (MagLightingSpaceMove(user, pum.Level))
                    {
                        train = true;
                    }
                    break;
                case 22:
                    // Áö¿°¼ú
                    if (MagMakeFireCross(user, user.GetAttackPower(SpellNow_GetPower(MPow(pum)) + Lobyte(user.WAbil.MC), (short)HiByte(user.WAbil.MC) - Lobyte(user.WAbil.MC) + 1), SpellNow_GetPower(10) + SpellNow_GetRPow((short)user.WAbil.MC) / 2, xx, yy) > 0)
                    {
                        train = true;
                    }
                    break;
                case 23:
                    // Æø¿­ÆÄ
                    if (MagBigExplosion(user, user.GetAttackPower(SpellNow_GetPower(MPow(pum)) + Lobyte(user.WAbil.MC), (short)HiByte(user.WAbil.MC) - Lobyte(user.WAbil.MC) + 1), xx, yy, 1))
                    {
                        train = true;
                    }
                    break;
                case 45:
                    // È­·æ±â¿°
                    // Random(0.8+(0.5*(Lv_S+1)))*Mcmax)+(1.2*Lv_S)*Mc
                    pwr = (new System.Random(8 + (5 * (pum.Level + 1))).Next() * HiByte(user.WAbil.MC) + 12 * pum.Level * user.Lobyte(user.WAbil.MC)) / 10;
                    if (MagDragonFire(user, pwr, pum.Level))
                    {
                        train = true;
                    }
                    break;
                case 33:
                    // ºù¼³Ç³
                    if (MagBigExplosion(user, user.GetAttackPower(SpellNow_GetPower(MPow(pum)) + Lobyte(user.WAbil.MC), (short)HiByte(user.WAbil.MC) - Lobyte(user.WAbil.MC) + 1), xx, yy, 1))
                    {
                        train = true;
                    }
                    break;
                case 24:
                    // ·Ú¼³È­
                    if (MagElecBlizzard(user, user.GetAttackPower(SpellNow_GetPower(MPow(pum)) + Lobyte(user.WAbil.MC), (short)HiByte(user.WAbil.MC) - Lobyte(user.WAbil.MC) + 1)))
                    {
                        train = true;
                    }
                    break;
                case 31:
                    // ÁÖ¼úÀÇ¸·
                    if (user.MagBubbleDefenceUp(pum.Level, SpellNow_GetPower(15 + SpellNow_GetRPow((short)user.WAbil.MC))))
                    {
                        train = true;
                    }
                    break;
                case 2:
                    // È¸º¹¼ú
                    if (target == null)
                    {
                        target = user;
                        xx = user.CX;
                        yy = user.CY;
                    }
                    if (user.IsProperFriend(target))
                    {
                        pwr = user.GetAttackPower(SpellNow_GetPower(MPow(pum)) + user.Lobyte(user.WAbil.SC) * 2, ((short)HiByte(user.WAbil.SC) - user.Lobyte(user.WAbil.SC)) * 2 + 1);
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
                    // ´ëÈ¸º¹¼ú
                    pwr = user.GetAttackPower(SpellNow_GetPower(MPow(pum)) + user.Lobyte(user.WAbil.SC) * 2, ((short)HiByte(user.WAbil.SC) - user.Lobyte(user.WAbil.SC)) * 2 + 1);
                    if (MagBigHealing(user, pwr, xx, yy))
                    {
                        train = true;
                    }
                    break;
                case 6:
                    // ¾Ï¿¬¼ú
                    nofire = true;
                    bhasitem = 0;
                    pstd = null;
                    if (user.IsProperTarget(target))
                    {
                        // ¾Ï¿¬¼úÀº µ¶°¡·çÁÖ¸Ó´Ï°¡ ÀÖ¾î¾ß ÇÑ´Ù.
                        // 2003/03/15 COPARK ¾ÆÀÌÅÛ ÀÎº¥Åä¸® È®Àå
                        if (user.UseItems[Grobal2.U_BUJUK].Index > 0)
                        {
                            // U_ARMRINGL->U_BUJUK
                            pstd = svMain.UserEngine.GetStdItem(user.UseItems[Grobal2.U_BUJUK].Index);
                            if (pstd != null)
                            {
                                if ((pstd.StdMode == 25) && (pstd.Shape <= 2))
                                {
                                    // 25:µ¶ÁÖ¸Ó´Ï
                                    if (user.UseItems[Grobal2.U_BUJUK].Dura >= 100)
                                    {
                                        user.UseItems[Grobal2.U_BUJUK].Dura = (ushort)(user.UseItems[Grobal2.U_BUJUK].Dura - 100);
                                        // ³»±¸¼º º¯°æÀº ¾Ë¸²
                                        user.SendMsg(user, Grobal2.RM_DURACHANGE, Grobal2.U_BUJUK, user.UseItems[Grobal2.U_BUJUK].Dura, user.UseItems[Grobal2.U_BUJUK].DuraMax, 0, "");
                                        bhasitem = 1;
                                    }
                                }
                            }
                        }
                        if ((user.UseItems[Grobal2.U_ARMRINGL].Index > 0) && (bhasitem == 0))
                        {
                            pstd = svMain.UserEngine.GetStdItem(user.UseItems[Grobal2.U_ARMRINGL].Index);
                            if (pstd != null)
                            {
                                if ((pstd.StdMode == 25) && (pstd.Shape <= 2))
                                {
                                    // 25:µ¶ÁÖ¸Ó´Ï
                                    if (user.UseItems[Grobal2.U_ARMRINGL].Dura >= 100)
                                    {
                                        user.UseItems[Grobal2.U_ARMRINGL].Dura = (ushort)(user.UseItems[Grobal2.U_ARMRINGL].Dura - 100);
                                        // ³»±¸¼º º¯°æÀº ¾Ë¸²
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
                                // ½ºÅ³Á¤µµ¿¡ µû¶ó ¼º°ø¿©ºÎ°¡ °áÁ¤
                                if (6 >= new System.Random(7 + target.AntiPoison).Next())
                                {
                                    switch (pstd.Shape)
                                    {
                                        case 1:
                                            // È¸»öµ¶°¡·ç: Áßµ¶
                                            // sec = Áßµ¶½Ã°£  60ÃÊ + ¾ËÆÄ
                                            sec = SpellNow_GetPower13(30) + 2 * SpellNow_GetRPow((short)user.WAbil.SC);
                                            // pwr := pum.Level;
                                            pwr = pum.Level + _MAX(0, _MIN(3, _MAX(0, HiByte(user.WAbil.SC) - 30) * 15 / 100));
                                            // wparam
                                            target.SendDelayMsg(user, Grobal2.RM_MAKEPOISON, Grobal2.POISON_DECHEALTH, sec, user.ActorId, pwr, "", 1000);
                                            break;
                                        case 2:
                                            // È²»öµ¶°¡·ç: ¹æ¾î·Â°¨¼Ò
                                            // ³ìµ¶À» °Ç »ó´ë°¡ ´©±¸ÀÎÁö ¸ð¸¥ »óÅÂ¿¡¼­ »¡µ¶À» °É¸é ³ìµ¶Àº »ç¶óÁø´Ù.(sonmg 2004/12/27)
                                            if ((target.LastHiter == null) && (target.StatusArr[Grobal2.POISON_DECHEALTH] > 0))
                                            {
                                                target.StatusArr[Grobal2.POISON_DECHEALTH] = 0;
                                            }
                                            // sec = Áßµ¶½Ã°£ 40ÃÊ + ¾ËÆÄ
                                            sec = SpellNow_GetPower13(40) + 2 * SpellNow_GetRPow((short)user.WAbil.SC);
                                            // (Lobyte(user.WAbil.SC) + Random(ShortInt(HiByte(user.WAbil.SC)-Lobyte(user.WAbil.SC)) + 1));
                                            // pwr := 2{pum.Level};
                                            pwr = _MAX(2, _MIN(5, HiByte(user.WAbil.SC) / 10));
                                            // wparam
                                            target.SendDelayMsg(user, Grobal2.RM_MAKEPOISON, Grobal2.POISON_DAMAGEARMOR, sec, user.ActorId, pwr, "", 1000);
                                            break;
                                    }
                                    // -----------------------------------------
                                    // °¥ÇÇ Ã¼Å©(sonmg 2005/11/28)
                                    if ((target.RaceServer == Grobal2.RC_USERHUMAN) && (user.RaceServer == Grobal2.RC_USERHUMAN))
                                    {
                                        // Á¤´ç¹æ¾î¸¦ À§ÇÑ ±â·Ï..
                                        target.AddPkHiter(user);
                                        target.SetLastHiter(user);
                                    }
                                    else if (target.Master != null)
                                    {
                                        // Áßµ¶µÈ ¼ÒÈ¯¸÷ÀÇ ÁÖÀÎÀÌ ¶§¸°»ç¶÷ÀÌ ¾Æ´Ï¸é
                                        if (target.Master != user)
                                        {
                                            // Á¤´ç¹æ¾î¸¦ À§ÇÑ ±â·Ï..
                                            target.AddPkHiter(user);
                                            target.SetLastHiter(user);
                                        }
                                    }
                                    // -----------------------------------------
                                    // »ç¶÷,¸ó½ºÅÍ¿¡°Ô °É¾úÀ»¶§ ¼ö·ÃµÈ´Ù.
                                    if ((target.RaceServer == Grobal2.RC_USERHUMAN) || (target.RaceServer >= Grobal2.RC_ANIMAL))
                                    {
                                        train = true;
                                    }
                                }
                                user.SelectTarget(target);
                                nofire = false;
                            }
                        }
                        // ´Ù ¾´ ¾àÀº »ç¶óÁø´Ù.
                        if (bhasitem == 1)
                        {
                            if (user.UseItems[Grobal2.U_BUJUK].Dura < 100)
                            {
                                user.UseItems[Grobal2.U_BUJUK].Dura = 0;
                                // ´Ù ¾´¾àÀº »ç¶óÁø´Ù.
                                if (user.RaceServer == Grobal2.RC_USERHUMAN)
                                {
                                    hum = (TUserHuman)user;
                                    hum.SendDelItem(user.UseItems[Grobal2.U_BUJUK]);
                                    // Å¬¶óÀÌ¾ðÆ®¿¡ ¾ø¾îÁø°Å º¸³¿
                                    hum.SysMsg("The Poison item has been exhausted.", 0);
                                    // µ¶°¡·ç ´Ù ´â¾ÒÀ» ¶§ ¸Þ½ÃÁö(2004/11/15)
                                }
                                user.UseItems[Grobal2.U_BUJUK].Index = 0;
                            }
                        }
                        if ((user.UseItems[Grobal2.U_ARMRINGL].Index > 0) && (bhasitem == 2))
                        {
                            pstd = svMain.UserEngine.GetStdItem(user.UseItems[Grobal2.U_ARMRINGL].Index);
                            if (pstd != null)
                            {
                                if (pstd.StdMode == 25)
                                {
                                    // 25:µ¶ÁÖ¸Ó´Ï
                                    if (user.UseItems[Grobal2.U_ARMRINGL].Dura < 100)
                                    {
                                        user.UseItems[Grobal2.U_ARMRINGL].Dura = 0;
                                        // ´Ù ¾´¾àÀº »ç¶óÁø´Ù.
                                        if (user.RaceServer == Grobal2.RC_USERHUMAN)
                                        {
                                            hum = (TUserHuman)user;
                                            hum.SendDelItem(user.UseItems[Grobal2.U_ARMRINGL]);
                                            // Å¬¶óÀÌ¾ðÆ®¿¡ ¾ø¾îÁø°Å º¸³¿
                                            hum.SysMsg("The Poison item has been exhausted.", 0);
                                            // µ¶°¡·ç ´Ù ´â¾ÒÀ» ¶§ ¸Þ½ÃÁö(2004/11/15)
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
                    // 2003/03/15 ½Å±Ô¹«°ø Ãß°¡
                    // ¹«±ØÁø±â
                    // Æø»ì°è(µµ»ç)
                    // Ç×¸¶Áø¹ý
                    // ´ëÁö¿øÈ£
                    // °á°è
                    // ¹é°ñ¼ÒÈ¯¼ú
                    // Àº½Å¼ú
                    // ´ëÀº½Å¼ú
                    // Ãµ³à¼ÒÈ¯(Á¤È¥¼ÒÈ¯-¿ù·É)
                    // ÀúÁÖ¼ú
                    // ¸Í¾È¼ú(¹ÌÈ¥¼ú)
                    nofire = true;
                    bujuckcount = 1;
                    try
                    {
                        switch (pum.pDef.MagicId)
                        {
                            case 41:
                                // Ãµ³à¼ÒÈ¯(Á¤È¥¼ÒÈ¯-¿ù·É)ÀÏ°æ¿ì¿¡´Â 5°³ ¼Òºñ
                                TempCret = user.GetExistSlave(svMain.__AngelMob);
                                if (TempCret != null)
                                {
                                    TempCret.ForceMoveToMaster = true;
                                    return result;
                                }
                                bujuckcount = 5;
                                break;
                            case 17:
                                TempCret = user.GetExistSlave(svMain.__WhiteSkeleton);
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
                        svMain.MainOutMessage("EXCEPTION BUJUCK CALC");
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
                            // 2003/03/15 COPARK ¾ÆÀÌÅÛ ÀÎº¥Åä¸® È®Àå
                            if (user.UseItems[Grobal2.U_BUJUK].Dura >= (bujuckcount * 100))
                            {
                                // U_ARMRINGL->U_BUJUK
                                user.UseItems[Grobal2.U_BUJUK].Dura = (ushort)(user.UseItems[Grobal2.U_BUJUK].Dura - (bujuckcount * 100));
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
                                user.UseItems[Grobal2.U_ARMRINGL].Dura = (ushort)(user.UseItems[Grobal2.U_ARMRINGL].Dura - (bujuckcount * 100));
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
                                // Æø»ì°è
                                if (user.MagCanHitTarget(user.CX, user.CY, target))
                                {
                                    if (user.IsProperTarget(target))
                                    {
                                        if ((target.AntiMagic <= new System.Random(50).Next()) && (Math.Abs(target.CX - xx) <= 1) && (Math.Abs(target.CY - yy) <= 1))
                                        {
                                            // ÆÄ¿ö
                                            pwr = user.GetAttackPower(SpellNow_GetPower(MPow(pum)) + user.Lobyte(user.WAbil.SC), (short)HiByte(user.WAbil.SC) - user.Lobyte(user.WAbil.SC) + 1);
                                            // Å¸°Ù ¸ÂÀ½, ÈÄ¿¡ È¿°ú³ªÅ¸³²
                                            // target.SendDelayMsg (user, RM_MAGSTRUCK, 0, pwr, 0, 0, '', 1200 + _MAX(Abs(CX-xx),Abs(CY-yy)) * 50 );
                                            // user.SelectTarget (target);
                                            user.SendDelayMsg(user, Grobal2.RM_DELAYMAGIC, (ushort)pwr, HUtil32.MakeLong(xx, yy), 2, target.ActorId, "", 1200);
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
                                // Ç×¸¶Áø¹ý
                                pwr = user.GetAttackPower(SpellNow_GetPower13(60) + 5 * Lobyte(user.WAbil.SC), 5 * ((short)HiByte(user.WAbil.SC) - Lobyte(user.WAbil.SC) + 1));
                                // ÃÊ
                                if (user.MagMakeDefenceArea(xx, yy, 3, pwr, true) > 0)
                                {
                                    train = true;
                                }
                                break;
                            case 46:
                                // ÀúÁÖ¼ú
                                sec = (((pum.Level + 1) * 24) + HiByte(user.WAbil.SC) + user.Abil.Level) / 2;
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
                                // 2003/03/15 ½Å±Ô¹«°ø Ãß°¡
                                // ¹«±ØÁø±â
                                sec = user.GetAttackPower(SpellNow_GetPower13(60) + 5 * Lobyte(user.WAbil.SC), 5 * ((short)HiByte(user.WAbil.SC) - Lobyte(user.WAbil.SC) + 1));
                                // Áõ°¡µÇ´Â ÆÄ±«·Â
                                pwr = ((HiByte(user.WAbil.SC) - 1) / 5) + 1;
                                if (pwr > 8)
                                {
                                    pwr = 8;
                                }
                                // ÃÊ
                                if (user.MagDcUp(sec, pwr))
                                {
                                    train = true;
                                }
                                // Å¸ÄÏÀÇ ´É·ÂÄ¡¸¦ ¿Ã·ÁÁÜ(sonmg 2005/06/07)
                                if ((target != null) && (target.RaceServer == Grobal2.RC_USERHUMAN))
                                {
                                    // ÃÊ
                                    if (target.MagDcUp(sec, pwr))
                                    {
                                        target.SendRefMsg(Grobal2.RM_LOOPNORMALEFFECT, (ushort)target.ActorId, 0, 0, Grobal2.NE_BIGFORCE, "");
                                        train = true;
                                    }
                                }
                                break;
                            case 15:
                                // ´ëÁö¿øÈ£
                                pwr = user.GetAttackPower(SpellNow_GetPower13(60) + 5 * Lobyte(user.WAbil.SC), 5 * ((short)HiByte(user.WAbil.SC) - Lobyte(user.WAbil.SC) + 1));
                                // ÃÊ
                                if (user.MagMakeDefenceArea(xx, yy, 3, pwr, false) > 0)
                                {
                                    train = true;
                                }
                                break;
                            case 16:
                                // °á°è
                                // Lobyte(user.WAbil.SC),
                                if (MagMakeHolyCurtain(user, SpellNow_GetPower13(40) + 3 * SpellNow_GetRPow((short)user.WAbil.SC), xx, yy) > 0)
                                {
                                    train = true;
                                }
                                break;
                            case 17:
                                // ¹é°ñ¼ÒÈ¯¼ú
                                try
                                {
                                    TempCret = user.GetExistSlave(svMain.__WhiteSkeleton);
                                    if (TempCret == null)
                                    {
                                        if ((user.GetExistSlave(svMain.__ShinSu) == null) && (user.GetExistSlave(svMain.__ShinSu1) == null))
                                        {
                                            if (user.MakeSlave(svMain.__WhiteSkeleton, pum.Level, 1, 10 * 24 * 60 * 60) != null)
                                            {
                                                train = true;
                                            }
                                        }
                                    }
                                }
                                catch
                                {
                                    svMain.MainOutMessage("EXCEPT WHITE SKELETON");
                                }
                                break;
                            case 18:
                                // Àº½Å
                                if (MagMakePrivateTransparent(user, SpellNow_GetPower13(30) + 3 * SpellNow_GetRPow((short)user.WAbil.SC)))
                                {
                                    train = true;
                                }
                                break;
                            case 19:
                                // ´ëÀº½Å
                                if (MagMakeGroupTransparent(user, xx, yy, SpellNow_GetPower13(30) + 3 * SpellNow_GetRPow((short)user.WAbil.SC)))
                                {
                                    train = true;
                                }
                                break;
                            case 41:
                                // Ãµ³à¼ÒÈ¯¼ú(Á¤È¥¼ÒÈ¯¼ú)
                                try
                                {
                                    TempCret = user.GetExistSlave(svMain.__AngelMob);
                                    if (TempCret == null)
                                    {
                                        // Ãµ³à(¿ù·É)°¡ ¾ø´Ù.
                                        if (user.MakeSlave(svMain.__AngelMob, pum.Level, 1, 10 * 24 * 60 * 60) != null)
                                        {
                                            train = true;
                                        }
                                    }
                                }
                                catch
                                {
                                    svMain.MainOutMessage("EXCEPT ALGEL ");
                                }
                                break;
                            case 49:
                                // ¸Í¾È¼ú(¹ÌÈ¥¼ú)
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
                    // ½Å¼ö¼ÒÈ¯
                    nofire = true;
                    try
                    {
                        TempCret = user.GetExistSlave(svMain.__ShinSu);
                        if (TempCret == null)
                        {
                            TempCret = user.GetExistSlave(svMain.__ShinSu1);
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
                                // 2003/03/15 COPARK ¾ÆÀÌÅÛ ÀÎº¥Åä¸® È®Àå
                                if (user.UseItems[Grobal2.U_BUJUK].Dura >= 500)
                                {
                                    // U_ARMRINGL->U_BUJUK
                                    user.UseItems[Grobal2.U_BUJUK].Dura = (ushort)(user.UseItems[Grobal2.U_BUJUK].Dura - 500);
                                }
                                else
                                {
                                    user.UseItems[Grobal2.U_BUJUK].Dura = 0;
                                }
                                // ³»±¸¼º º¯°æÀº ¾Ë¸²
                                user.SendMsg(user, Grobal2.RM_DURACHANGE, Grobal2.U_BUJUK, user.UseItems[Grobal2.U_BUJUK].Dura, user.UseItems[Grobal2.U_BUJUK].DuraMax, 0, "");
                            }
                            if (bhasitem == 2)
                            {
                                if (user.UseItems[Grobal2.U_ARMRINGL].Dura >= 500)
                                {
                                    user.UseItems[Grobal2.U_ARMRINGL].Dura = (ushort)(user.UseItems[Grobal2.U_ARMRINGL].Dura - 500);
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
                                    // ½Å¼ö¼ÒÈ¯
                                    if (user.GetExistSlave(svMain.__WhiteSkeleton) == null)
                                    {
                                        if (user.MakeSlave(svMain.__ShinSu, pum.Level, 1, 10 * 24 * 60 * 60) != null)
                                        {
                                            train = true;
                                        }
                                    }
                                    break;
                            }
                            nofire = false;
                            SpellNow_UseBujuk(user);
                            // ¹ö±× ¼öÁ¤(2004/09/01 sonmg)
                        }
                    }
                    catch
                    {
                        svMain.MainOutMessage("EXCEPT SHINSU");
                    }
                    break;
                case 42:
                    // ºÐ½Å¼ÒÈ¯
                    try
                    {
                        TempCret = user.GetExistSlave(svMain.__CloneMob);
                        if (TempCret != null)
                        {
                            // ºÐ½ÅÇÑ³ÑÀÌ ÀÖ´Ù.
                            TempCret.BoDisapear = true;
                            // MP ´Ù½Ã ´õÇØÁÖÀÚ
                            user.WAbil.MP = (ushort)(user.WAbil.MP + spell);
                        }
                        else
                        {
                            // ºÐ½ÅÇÑ³ÑÀÌ ¾ø´Ù.
                            TempCret = user.MakeSlave(svMain.__CloneMob, pum.Level, 5, 10 * 24 * 60 * 60);
                            if (TempCret != null)
                            {
                                user.SendRefMsg(Grobal2.RM_NORMALEFFECT, 0, TempCret.CX, TempCret.CY, Grobal2.NE_CLONESHOW, "");
                                train = true;
                            }
                        }
                    }
                    catch
                    {
                        svMain.MainOutMessage("EXCET CLONE");
                    }
                    break;
                case 28:
                    // Å½±âÆÄ¿¬
                    if (target != null)
                    {
                        if (!target.BoOpenHealth)
                        {
                            if (new System.Random(6).Next() <= 3 + pum.Level)
                            {
                                target.OpenHealthStart = GetTickCount;
                                target.OpenHealthTime = SpellNow_GetPower13(30 + SpellNow_GetRPow((short)user.WAbil.SC) * 2) * 1000;
                                target.SendDelayMsg(target, Grobal2.RM_DOOPENHEALTH, 0, 0, 0, 0, "", 1500);
                                train = true;
                            }
                        }
                    }
                    break;
                case 39:
                    // 2003/07/15 ½Å±Ô¹«°ø
                    // °áºùÀå
                    if (user.MagCanHitTarget(user.CX, user.CY, target))
                    {
                        if (user.IsProperTarget(target))
                        {
                            if ((target.AntiMagic <= new System.Random(50).Next()) && (Math.Abs(target.CX - xx) <= 1) && (Math.Abs(target.CY - yy) <= 1))
                            {
                                // °áºùÀå °ø½Ä ¼öÁ¤(sonmg 2004/10/20)
                                // Dur := (Round (0.4+pum.Level*0.2) * (Lobyte(WAbil.MC) + HiByte(WAbil.MC)));
                                Dur = HUtil32.MathRound(0.4 + pum.Level * 0.2) * (user.Lobyte(user.WAbil.MC) + new System.Random(HiByte(user.WAbil.MC)).Next() + (HiByte(user.WAbil.MC) / 2));
                                pwr = pum.pDef.MinPower + Dur;
                                user.SendDelayMsg(user, Grobal2.RM_DELAYMAGIC, (ushort)pwr, HUtil32.MakeLong(xx, yy), 2, target.ActorId, "", 600);
                                // »óÅÂÀÌ»ó...µÐÈ­ÆÇÁ¤
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
                    // Á¤È­¼ú
                    if (MagMakePrivateClean(user, xx, yy, pum.Level * 15 + 45))
                    {
                        train = true;
                    }
                    break;
                case 43:
                    // »çÀÚÈÄ
                    try
                    {
                        // nofire := true;   //sonmg(2004/05/19)
                        user._Attack(Grobal2.HM_STONEHIT, null);
                    }
                    catch
                    {
                        svMain.MainOutMessage("EXCEPTION STONEHIT");
                    }
                    train = false;
                    break;
                case 44:
                    // °øÆÄ¼¶
                    if (MagWindCut(user, pum.Level))
                    {
                        train = true;
                    }
                    break;
                case 47:
                    // Æ÷½Â°Ë
                    if (MagPullMon(user, target, pum.Level))
                    {
                        train = true;
                    }
                    break;
                case 48:
                    // ÈíÇ÷¼ú
                    if (user.IsProperTarget(target))
                    {
                        if (target.AntiMagic <= new System.Random(50).Next())
                        {
                            pwr = user.GetAttackPower(SpellNow_GetPower(MPow(pum)) + user.Lobyte(user.WAbil.MC), (short)HiByte(user.WAbil.MC) - user.Lobyte(user.WAbil.MC) + 1);
                            // user.SelectTarget (target);
                            if ((target.LifeAttrib != Grobal2.LA_UNDEAD) && (target.RaceServer != Grobal2.RC_USERHUMAN))
                            {
                                pwr = HUtil32.MathRound(pwr * 1.2);
                            }
                            user.SendDelayMsg(user, Grobal2.RM_DELAYMAGIC, (ushort)pwr, HUtil32.MakeLong(xx, yy), 2, target.ActorId, "", 0);
                            if (target.RaceServer >= Grobal2.RC_ANIMAL)
                            {
                                // ¸÷¿¡°Ô »ç¿ëÇÑ °æ¿ì »¯¾î¿À´Â HP·®.
                                user.IncHealth = (byte)((pwr * (pum.Level + 1) * 10 + new System.Random(20).Next()) / 100);
                                train = true;
                            }
                            else
                            {
                                // »ç¶÷¿¡°Ô »ç¿ëÇÑ °æ¿ì »¯¾î¿À´Â HP·®.
                                user.IncHealth = (pwr * (pum.Level + 1) * 10 + new System.Random(10).Next()) / 100 * _MAX(0, 1 - target.AntiMagic / 25);
                            }
                            // user.SendRefMsg (RM_LOOPNORMALEFFECT, integer(user), 0, 0, NE_BLOODSUCK, '');
                            user.SendDelayMsg(user, Grobal2.RM_LOOPNORMALEFFECT, (ushort)user.ActorId, 0, 0, Grobal2.NE_BLOODSUCK, "", 1000);
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
                    user.SendRefMsg(Grobal2.RM_MAGICFIRE, 0, MakeWord(pum.pDef.EffectType, pum.pDef.Effect), HUtil32.MakeLong(xx, yy), target.ActorId, "");
                }
                // 2003/03/15 ½Å±Ô¹«°ø Ãß°¡
                // ¸ÂÀº »ó´ë
                if ((pum.Level < 3) && train)
                {
                    if (user.Abil.Level >= pum.pDef.NeedLevel[pum.Level])
                    {
                        // ¼ö·Ã·¹º§¿¡ µµ´ÞÇÑ °æ¿ì
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

    } // end TMagicManager

}

