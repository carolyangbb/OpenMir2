using SystemModule;

namespace GameSvr
{
    public class TGuildOfficial : TNormNpc
    {

        public TGuildOfficial() : base()
        {
            this.RaceImage = Grobal2.RCC_MERCHANT;
            this.Appearance = 8;
        }

        public override void UserCall(TCreature caller)
        {
            this.NpcSayTitle(caller, "@main");
        }

        private int UserBuildGuildNow(TUserHuman hum, string gname)
        {
            int result = 0;
            gname = gname.Trim();
            TUserItem pu = null;
            if (gname == "")
            {
                result = -4;
            }
            if (hum.MyGuild == null)
            {
                if (hum.Gold >= M2Share.BUILDGUILDFEE)
                {
                    pu = hum.FindItemName(M2Share.__WomaHorn);
                    if (pu != null)
                    {

                    }
                    else
                    {
                        result = -3;
                    }
                }
                else
                {
                    result = -2;
                }
            }
            else
            {
                result = -1;
            }
            if (result == 0)
            {
                if (M2Share.GuildMan.AddGuild(gname, hum.UserName))
                {
                    M2Share.UserEngine.SendInterMsg(Grobal2.ISM_ADDGUILD, M2Share.ServerIndex, gname + "/" + hum.UserName);
                    hum.SendDelItem(pu);
                    hum.DelItem(pu.MakeIndex, M2Share.__WomaHorn);
                    hum.DecGold(M2Share.BUILDGUILDFEE);
                    hum.GoldChanged();
                    hum.MyGuild = M2Share.GuildMan.GetGuildFromMemberName(hum.UserName);
                    if (hum.MyGuild != null)
                    {
                        hum.GuildRankName = hum.MyGuild.MemberLogin(hum, ref hum.GuildRank);
                    }
                }
                else
                {
                    result = -4;
                }
            }
            switch (result)
            {
                case 0:
                    hum.SendMsg(this, Grobal2.RM_BUILDGUILD_OK, 0, 0, 0, 0, "");
                    break;
                default:
                    hum.SendMsg(this, Grobal2.RM_BUILDGUILD_FAIL, 0, result, 0, 0, "");
                    break;
            }
            return result;
        }

        private int UserDeclareGuildWarNow(TUserHuman hum, string gname)
        {
            int result;
            if (M2Share.GuildMan.GetGuild(gname) != null)
            {
                if (hum.Gold >= ObjNpc.GUILDWARFEE)
                {
                    if (hum.GuildDeclareWar(gname) == true)
                    {
                        hum.DecGold(ObjNpc.GUILDWARFEE);
                        hum.GoldChanged();
                    }
                }
                else
                {
                    hum.SysMsg("缺少金币。", 0);
                }
            }
            else
            {
                hum.SysMsg(gname + " 找不到门派。", 0);
            }
            result = 1;
            return result;
        }

        private int UserFreeGuild(TUserHuman hum)
        {
            return 1;
        }

        private void UserDonateGold(TUserHuman hum)
        {
            hum.SendMsg(this, Grobal2.RM_DONATE_FAIL, 0, 0, 0, 0, "");
        }

        private void UserRequestCastleWar(TUserHuman hum)
        {
            TUserItem pu;
            if (hum.IsGuildMaster() && (!M2Share.UserCastle.IsOurCastle(hum.MyGuild)))
            {
                pu = hum.FindItemName(M2Share.__ZumaPiece);
                if (pu != null)
                {
                    if (M2Share.UserCastle.ProposeCastleWar(hum.MyGuild))
                    {
                        hum.SendDelItem(pu);
                        hum.DelItem(pu.MakeIndex, M2Share.__ZumaPiece);
                        M2Share.AddUserLog("10\09" + hum.MapName + "\09" + hum.CX.ToString() + "\09" + hum.CY.ToString() + "\09" + hum.UserName + "\09" + M2Share.__ZumaPiece + "\09" + "0" + "\09" + "1\09" + this.UserName);
                        this.NpcSayTitle(hum, "~@request_ok");
                    }
                    else
                    {
                        hum.SysMsg("您此刻无法请求。", 0);
                    }
                }
                else
                {
                    hum.SysMsg("您没有祖玛头像。", 0);
                }
            }
            else
            {
                hum.SysMsg("您的请求被取消。", 0);
            }
            hum.SendMsg(this, Grobal2.RM_MENU_OK, 0, 0, 0, 0, "");
        }

        public override void UserSelect(TCreature whocret, string selstr)
        {
            string sel = string.Empty;
            string body = string.Empty;
            try
            {
                if (selstr != "")
                {
                    if (selstr[0] == '@')
                    {
                        body = HUtil32.GetValidStr3(selstr, ref sel, new char[] { '\r' });
                        this.NpcSayTitle(whocret, sel);
                        if (sel.ToLower().CompareTo("@@buildguildnow".ToLower()) == 0)
                        {
                            UserBuildGuildNow((TUserHuman)whocret, body);
                        }
                        if (sel.ToLower().CompareTo("@@guildwar".ToLower()) == 0)
                        {
                            UserDeclareGuildWarNow((TUserHuman)whocret, body);
                        }
                        if (sel.ToLower().CompareTo("@@donate".ToLower()) == 0)
                        {
                            UserDonateGold((TUserHuman)whocret);
                        }
                        if (sel.ToLower().CompareTo("@requestcastlewarnow".ToLower()) == 0)
                        {
                            UserRequestCastleWar((TUserHuman)whocret);
                        }
                        if (sel.ToLower().CompareTo("@exit".ToLower()) == 0)
                        {
                            whocret.SendMsg(this, Grobal2.RM_MERCHANTDLGCLOSE, 0, this.ActorId, 0, 0, "");
                        }
                    }
                }
            }
            catch
            {
                M2Share.MainOutMessage("[Exception] TGuildOfficial.UserSelect... ");
            }
        }

        public override void Run()
        {
            if (new System.Random(40).Next() == 0)
            {
                this.Turn((byte)new System.Random(8).Next());
            }
            else if (new System.Random(30).Next() == 0)
            {
                this.SendRefMsg(Grobal2.RM_HIT, this.Dir, this.CX, this.CY, 0, "");
            }
            base.Run();
        }
    }
}

