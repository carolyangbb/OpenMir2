using System;
using SystemModule;

namespace GameSvr
{
    public class TCastleManager : TMerchant
    {
        public TCastleManager() : base()
        {

        }

        public override void CheckNpcSayCommand(TUserHuman hum, ref string source, string tag)
        {
            string str;
            base.CheckNpcSayCommand(hum, ref source, tag);
            if (tag == "$CASTLEGOLD")
            {
                source = this.ChangeNpcSayTag(source, "<$CASTLEGOLD>", svMain.UserCastle.TotalGold.ToString());
            }
            if (tag == "$TODAYINCOME")
            {
                source = this.ChangeNpcSayTag(source, "<$TODAYINCOME>", svMain.UserCastle.TodayIncome.ToString());
            }
            if (tag == "$CASTLEDOORSTATE")
            {
                TCastleDoor _wvar1 = (TCastleDoor)svMain.UserCastle.MainDoor.UnitObj;
                if (_wvar1.Death)
                {
                    str = "已经损坏了。";
                }
                else if (_wvar1.BoOpenState)
                {
                    str = "已开启";
                }
                else
                {
                    str = "已关闭";
                }
                source = this.ChangeNpcSayTag(source, "<$CASTLEDOORSTATE>", str);
            }
            if (tag == "$REPAIRDOORGOLD")
            {
                source = this.ChangeNpcSayTag(source, "<$REPAIRDOORGOLD>", ObjNpc.CASTLEMAINDOORREPAREGOLD.ToString());
            }
            if (tag == "$REPAIRWALLGOLD")
            {
                source = this.ChangeNpcSayTag(source, "<$REPAIRWALLGOLD>", ObjNpc.CASTLECOREWALLREPAREGOLD.ToString());
            }
            if (tag == "$GUARDFEE")
            {
                source = this.ChangeNpcSayTag(source, "<$GUARDFEE>", ObjNpc.CASTLEGUARDEMPLOYFEE.ToString());
            }
            if (tag == "$ARCHERFEE")
            {
                source = this.ChangeNpcSayTag(source, "<$ARCHERFEE>", ObjNpc.CASTLEARCHEREMPLOYFEE.ToString());
            }
            if (tag == "$GUARDRULE")
            {
            }
        }

        private void RepaireCastlesMainDoor(TUserHuman hum)
        {
            if (svMain.UserCastle.TotalGold >= ObjNpc.CASTLEMAINDOORREPAREGOLD)
            {
                if (svMain.UserCastle.RepairCastleDoor())
                {
                    svMain.UserCastle.TotalGold = svMain.UserCastle.TotalGold - ObjNpc.CASTLEMAINDOORREPAREGOLD;
                    hum.SysMsg("修理完成。", 1);
                }
                else
                {
                    hum.SysMsg("您现在无法修理。", 0);
                }
            }
            else
            {
                hum.SysMsg("城内资金不足。", 0);
            }
        }

        private void RepaireCoreCastleWall(int wall, TUserHuman hum)
        {
            int n;
            if (svMain.UserCastle.TotalGold >= ObjNpc.CASTLECOREWALLREPAREGOLD)
            {
                n = svMain.UserCastle.RepairCoreCastleWall(wall);
                if (n == 1)
                {
                    svMain.UserCastle.TotalGold = svMain.UserCastle.TotalGold - ObjNpc.CASTLECOREWALLREPAREGOLD;
                    hum.SysMsg("修理完成。", 1);
                }
                else
                {
                    hum.SysMsg("您现在无法修理。", 0);
                }
            }
            else
            {
                hum.SysMsg("城内资金不足。", 0);
            }
        }

        private void HireCastleGuard(string numstr, TUserHuman hum)
        {
            int gnum;
            if (svMain.UserCastle.TotalGold >= ObjNpc.CASTLEGUARDEMPLOYFEE)
            {
                gnum = HUtil32.Str_ToInt(numstr, 0) - 1;
                if (gnum >= 0 && gnum <= CastleDef.MAXGUARD - 1)
                {
                    if (svMain.UserCastle.Guards[gnum].UnitObj == null)
                    {
                        if (!svMain.UserCastle.BoCastleUnderAttack)
                        {
                            TDefenseUnit _wvar1 = svMain.UserCastle.Guards[gnum];
                            _wvar1.UnitObj = svMain.UserEngine.AddCreatureSysop(svMain.UserCastle.CastleMapName, _wvar1.X, _wvar1.Y, _wvar1.UnitName);
                            if (_wvar1.UnitObj != null)
                            {
                                svMain.UserCastle.TotalGold = svMain.UserCastle.TotalGold - ObjNpc.CASTLEGUARDEMPLOYFEE;
                                hum.SysMsg("雇用卫士。", 1);
                            }
                        }
                        else
                        {
                            hum.SysMsg("现在无法雇用。", 0);
                        }
                    }
                    else
                    {
                        if (!svMain.UserCastle.Guards[gnum].UnitObj.Death)
                        {
                            hum.SysMsg("那里已经有卫士了。", 0);
                        }
                        else
                        {
                            hum.SysMsg("现在无法雇用。", 0);
                        }
                    }
                }
                else
                {
                    hum.SysMsg("错误的命令。", 0);
                }
            }
            else
            {
                hum.SysMsg("城内资金不足。", 0);
            }
        }

        private void HireCastleArcher(string numstr, TUserHuman hum)
        {
            int gnum;
            if (svMain.UserCastle.TotalGold >= ObjNpc.CASTLEARCHEREMPLOYFEE)
            {
                gnum = HUtil32.Str_ToInt(numstr, 0) - 1;
                if (gnum >= 0 && gnum <= CastleDef.MAXARCHER - 1)
                {
                    if (svMain.UserCastle.Archers[gnum].UnitObj == null)
                    {
                        if (!svMain.UserCastle.BoCastleUnderAttack)
                        {
                            TDefenseUnit _wvar1 = svMain.UserCastle.Archers[gnum];
                            _wvar1.UnitObj = svMain.UserEngine.AddCreatureSysop(svMain.UserCastle.CastleMapName, _wvar1.X, _wvar1.Y, _wvar1.UnitName);
                            if (_wvar1.UnitObj != null)
                            {
                                svMain.UserCastle.TotalGold = svMain.UserCastle.TotalGold - ObjNpc.CASTLEARCHEREMPLOYFEE;
                                ((TGuardUnit)_wvar1.UnitObj).Castle = svMain.UserCastle;
                                ((TGuardUnit)_wvar1.UnitObj).OriginX = _wvar1.X;
                                ((TGuardUnit)_wvar1.UnitObj).OriginY = _wvar1.Y;
                                ((TGuardUnit)_wvar1.UnitObj).OriginDir = 3;
                                hum.SysMsg("雇用了弓箭手。", 1);
                            }
                        }
                        else
                        {
                            hum.SysMsg("现在无法雇用。", 0);
                        }
                    }
                    else
                    {
                        if (!svMain.UserCastle.Archers[gnum].UnitObj.Death)
                        {
                            hum.SysMsg("那里已经有卫士了。", 0);
                        }
                        else
                        {
                            hum.SysMsg("现在无法雇用。", 0);
                        }
                    }
                }
                else
                {
                    hum.SysMsg("错误的命令", 0);
                }
            }
            else
            {
                hum.SysMsg("城内资金不足", 0);
            }
        }

        public override void UserCall(TCreature caller)
        {
            if (svMain.UserCastle.IsOurCastle(caller.MyGuild))
            {
                base.UserCall(caller);
            }
        }

        public override void UserSelect(TCreature whocret, string selstr)
        {
            string body = string.Empty;
            string sel = String.Empty;
            string rmsg = String.Empty;
            try
            {
                if (selstr != "")
                {
                    if (selstr[0] == '@')
                    {
                        body = HUtil32.GetValidStr3(selstr, ref sel, new char[] { '\r' });
                        rmsg = "";
                        while (true)
                        {
                            whocret.LatestNpcCmd = selstr;
                            this.NpcSayTitle(whocret, sel);
                            if (svMain.UserCastle.IsOurCastle(whocret.MyGuild) && whocret.IsGuildMaster())
                            {
                                if (sel.ToLower().CompareTo("@@withdrawal".ToLower()) == 0)
                                {
                                    switch (svMain.UserCastle.GetBackCastleGold((TUserHuman)whocret, Math.Abs(HUtil32.Str_ToInt(body, 0))))
                                    {
                                        case -1:
                                            rmsg = svMain.UserCastle.OwnerGuildName + "只有以下门派的门主才能使用：";
                                            break;
                                        case -2:
                                            rmsg = "该城内没有这么多金币。";
                                            break;
                                        case -3:
                                            rmsg = "您无法携带更多的东西。";
                                            break;
                                        case 1:
                                            UserSelect(whocret, "@main");
                                            break;
                                    }
                                    whocret.SendMsg(this, Grobal2.RM_MENU_OK, 0, this.ActorId, 0, 0, rmsg);
                                    break;
                                }
                                if (sel.ToLower().CompareTo("@@receipts".ToLower()) == 0)
                                {
                                    switch (svMain.UserCastle.TakeInCastleGold((TUserHuman)whocret, Math.Abs(HUtil32.Str_ToInt(body, 0))))
                                    {
                                        case -1:
                                            rmsg = svMain.UserCastle.OwnerGuildName + "只有以下门派的门主才能使用：";
                                            break;
                                        case -2:
                                            rmsg = "您没有那么多金币。";
                                            break;
                                        case -3:
                                            rmsg = "您已经达到在城内存放物品的限制了。";
                                            break;
                                        case 1:
                                            UserSelect(whocret, "@main");
                                            break;
                                    }
                                    whocret.SendMsg(this, Grobal2.RM_MENU_OK, 0, this.ActorId, 0, 0, rmsg);
                                    break;
                                }
                                if (sel.ToLower().CompareTo("@openmaindoor".ToLower()) == 0)
                                {
                                    svMain.UserCastle.ActivateMainDoor(false);
                                    break;
                                }
                                if (sel.ToLower().CompareTo("@closemaindoor".ToLower()) == 0)
                                {
                                    svMain.UserCastle.ActivateMainDoor(true);
                                    break;
                                }
                                if (sel.ToLower().CompareTo("@repairdoornow".ToLower()) == 0)
                                {
                                    RepaireCastlesMainDoor((TUserHuman)whocret);
                                    UserSelect(whocret, "@main");
                                    break;
                                }
                                if (sel.ToLower().CompareTo("@repairwallnow1".ToLower()) == 0)
                                {
                                    RepaireCoreCastleWall(1, (TUserHuman)whocret);
                                    UserSelect(whocret, "@main");
                                    break;
                                }
                                if (sel.ToLower().CompareTo("@repairwallnow2".ToLower()) == 0)
                                {
                                    RepaireCoreCastleWall(2, (TUserHuman)whocret);
                                    UserSelect(whocret, "@main");
                                    break;
                                }
                                if (sel.ToLower().CompareTo("@repairwallnow3".ToLower()) == 0)
                                {
                                    RepaireCoreCastleWall(3, (TUserHuman)whocret);
                                    UserSelect(whocret, "@main");
                                    break;
                                }
                                if (HUtil32.CompareLStr(sel, "@hireguardnow", 13))
                                {
                                    HireCastleGuard(sel.Substring(14 - 1, sel.Length), (TUserHuman)whocret);
                                    UserSelect(whocret, "@hireguards");
                                }
                                if (HUtil32.CompareLStr(sel, "@hirearchernow", 14))
                                {
                                    HireCastleArcher(sel.Substring(15 - 1, sel.Length), (TUserHuman)whocret);
                                    whocret.SendMsg(this, Grobal2.RM_MENU_OK, 0, this.ActorId, 0, 0, "");
                                }
                                if (sel.ToLower().CompareTo("@exit".ToLower()) == 0)
                                {
                                    whocret.SendMsg(this, Grobal2.RM_MERCHANTDLGCLOSE, 0, this.ActorId, 0, 0, "");
                                    break;
                                }
                            }
                            else
                            {
                                whocret.SendMsg(this, Grobal2.RM_MENU_OK, 0, this.ActorId, 0, 0, "您没有使用权限。");
                            }
                            break;
                        }
                    }
                }
            }
            catch
            {
                svMain.MainOutMessage("[Exception] TMerchant.UserSelect... ");
            }
        }
    }
}

