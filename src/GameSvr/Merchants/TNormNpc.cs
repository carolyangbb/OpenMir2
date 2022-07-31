using System;
using System.Collections;
using System.IO;
using SystemModule;

namespace GameSvr
{
    public class TNormNpc : TAnimal
    {
        public byte NpcFace = 0;
        public ArrayList Sayings = null;
        public string DefineDirectory = string.Empty;
        public bool BoInvisible = false;
        public bool BoUseMapFileName = false;
        public string NpcBaseDir = string.Empty;
        public bool CanSell = false;
        public bool CanBuy = false;
        public bool CanStorage = false;
        public bool CanGetBack = false;
        public bool CanRepair = false;
        public bool CanMakeDrug = false;
        public bool CanUpgrade = false;
        public bool CanMakeItem = false;
        public bool CanItemMarket = false;
        public bool CanSpecialRepair = false;
        public bool CanTotalRepair = false;
        public bool CanAgitUsage = false;
        public bool CanAgitManage = false;
        public bool CanBuyDecoItem = false;
        public bool CanDoingEtc = false;
        public bool BoSoundPlaying = false;
        public long SoundStartTime = 0;
        public long MemorialCount = 0;

        public TNormNpc() : base()
        {
            this.NeverDie = true;
            this.RaceServer = Grobal2.RC_NPC;
            this.Light = 2;
            this.AntiPoison = 99;
            Sayings = new ArrayList();
            this.StickMode = true;
            DefineDirectory = "";
            BoInvisible = false;
            BoUseMapFileName = true;
            CanSell = false;
            CanBuy = false;
            CanStorage = false;
            CanGetBack = false;
            CanRepair = false;
            CanMakeDrug = false;
            CanUpgrade = false;
            CanMakeItem = false;
            CanItemMarket = false;
            CanSpecialRepair = false;
            CanTotalRepair = false;
            CanAgitUsage = false;
            CanAgitManage = false;
            CanBuyDecoItem = false;
            CanDoingEtc = false;
            BoSoundPlaying = false;
            SoundStartTime = HUtil32.GetTickCount();
            MemorialCount = 0;
        }

        public override void RunMsg(TMessageInfo msg)
        {
            base.RunMsg(msg);
        }

        public override void Run()
        {
            base.Run();
        }

        public void ActivateNpcUtilitys(string saystr)
        {
            string lwstr = saystr.ToLower();
            if (lwstr.IndexOf("@buy") > 0)
            {
                CanBuy = true;
            }
            if (lwstr.IndexOf("@sell") > 0)
            {
                CanSell = true;
            }
            if (lwstr.IndexOf("@storage") > 0)
            {
                CanStorage = true;
            }
            if (lwstr.IndexOf("@getback") > 0)
            {
                CanGetBack = true;
            }
            if (lwstr.IndexOf("@repair") > 0)
            {
                CanRepair = true;
            }
            if (lwstr.IndexOf("@makedrug") > 0)
            {
                CanMakeDrug = true;
            }
            if (lwstr.IndexOf("@upgradenow") > 0)
            {
                CanUpgrade = true;
            }
            if (lwstr.IndexOf("@s_repair") > 0)
            {
                CanSpecialRepair = true;
            }
            if (lwstr.IndexOf("@t_repair") > 0)
            {
                CanTotalRepair = true;
            }
            // 酒捞袍 力炼
            if (lwstr.IndexOf("@makefood") > 0)
            {
                CanMakeItem = true;
            }
            if (lwstr.IndexOf("@makepotion") > 0)
            {
                CanMakeItem = true;
            }
            if (lwstr.IndexOf("@makegem") > 0)
            {
                CanMakeItem = true;
            }
            if (lwstr.IndexOf("@makeitem") > 0)
            {
                CanMakeItem = true;
            }
            if (lwstr.IndexOf("@makestuff") > 0)
            {
                CanMakeItem = true;
            }
            // 货肺眠啊(sonmg)
            if (lwstr.IndexOf("@makeetc") > 0)
            {
                CanMakeItem = true;
            }
            // 货肺眠啊(sonmg)
            // 困殴惑痢
            if (lwstr.IndexOf("@market_") > 0)
            {
                CanItemMarket = true;
            }
            // 巩颇厘盔
            if (lwstr.IndexOf("@agitreg") > 0)
            {
                CanAgitUsage = true;
            }
            if (lwstr.IndexOf("@agitmove") > 0)
            {
                CanAgitUsage = true;
            }
            if (lwstr.IndexOf("@agitbuy") > 0)
            {
                CanAgitUsage = true;
            }
            if (lwstr.IndexOf("@agittrade") > 0)
            {
                CanAgitUsage = true;
            }
            // 巩颇厘盔(包府)
            if (lwstr.IndexOf("@agitextend") > 0)
            {
                CanAgitManage = true;
            }
            if (lwstr.IndexOf("@agitremain") > 0)
            {
                CanAgitManage = true;
            }
            if (lwstr.IndexOf("@@agitonerecall") > 0)
            {
                CanAgitManage = true;
            }
            if (lwstr.IndexOf("@agitrecall") > 0)
            {
                CanAgitManage = true;
            }
            if (lwstr.IndexOf("@@agitforsale") > 0)
            {
                CanAgitManage = true;
            }
            if (lwstr.IndexOf("@agitforsalecancel") > 0)
            {
                CanAgitManage = true;
            }
            if (lwstr.IndexOf("@gaboardlist") > 0)
            {
                CanAgitManage = true;
            }
            if (lwstr.IndexOf("@@guildagitdonate") > 0)
            {
                CanAgitManage = true;
            }
            if (lwstr.IndexOf("@viewdonation") > 0)
            {
                CanAgitManage = true;
            }
            // 厘盔操固扁
            if (lwstr.IndexOf("@ga_decoitem_buy") > 0)
            {
                CanBuyDecoItem = true;
            }
            if (lwstr.IndexOf("@ga_decomon_count") > 0)
            {
                CanBuyDecoItem = true;
            }
        }

        public void NpcSay(TCreature target, string str)
        {
            str = HUtil32.ReplaceChar(str, '\\', (char)0xa);
            target.SendMsg(this, Grobal2.RM_MERCHANTSAY, 0, 0, 0, 0, this.UserName + "/" + str);
        }

        public string ChangeNpcSayTag(string src, string orgstr, string chstr)
        {
            string result;
            int n;
            string src1;
            string src2;
            n = src.IndexOf(orgstr);
            if (n > 0)
            {
                src1 = src.Substring(1 - 1, n - 1);
                src2 = src.Substring(n + orgstr.Length - 1, src.Length);
                result = src1 + chstr + src2;
            }
            else
            {
                result = src;
            }
            return result;
        }

        public virtual void CheckNpcSayCommand(TUserHuman hum, ref string source, string tag)
        {
            string data = string.Empty;
            string str2 = string.Empty;
            int n;
            if (tag == "$OWNERGUILD")
            {
                data = M2Share.UserCastle.OwnerGuildName;
                if (data == "")
                {
                    data = "GameManagerconsultation";
                }
                source = ChangeNpcSayTag(source, "<$OWNERGUILD>", data);
            }
            if (tag == "$LORD")
            {
                if (M2Share.UserCastle.OwnerGuild != null)
                {
                    data = M2Share.UserCastle.OwnerGuild.GetGuildMaster();
                }
                else
                {
                    data = "预备";
                }
                source = ChangeNpcSayTag(source, "<$LORD>", data);
            }
            if (tag == "$GUILDWARFEE")
            {
                source = ChangeNpcSayTag(source, "<$GUILDWARFEE>", ObjNpc.GUILDWARFEE.ToString());
            }
            if (tag == "$CASTLEWARDATE")
            {
                if (!M2Share.UserCastle.BoCastleUnderAttack)
                {
                    data = M2Share.UserCastle.GetNextWarDateTimeStr();
                    if (data != "")
                    {
                        source = ChangeNpcSayTag(source, "<$CASTLEWARDATE>", data);
                    }
                    else
                    {
                        source = "短期内没有攻城战。\\ \\<返回/@main> ";
                    }
                }
                else
                {
                    source = "正在攻城中！\\ \\<返回/@main>";
                }
                source = HUtil32.ReplaceChar(source, '\\', (char)0xa);
            }
            if (tag == "$LISTOFWAR")
            {
                data = M2Share.UserCastle.GetListOfWars();
                // 葛电 傍己 老沥
                if (data != "")
                {
                    source = ChangeNpcSayTag(source, "<$LISTOFWAR>", data);
                }
                else
                {
                    source = "我们未确定时间...\\ \\<返回/@main>";
                }
                source = HUtil32.ReplaceChar(source, '\\', (char)0xa);
            }
            if (tag == "$USERNAME")
            {
                source = ChangeNpcSayTag(source, "<$USERNAME>", hum.UserName);
            }
            if (tag == "$GAMEGOLD")
            {
                source = ChangeNpcSayTag(source, "<$GAMEGOLD>", hum.GameGold.ToString());
            }
            if (tag == "$SECONDSCARD")
            {
                source = ChangeNpcSayTag(source, "<$SECONDSCARD>", hum.SecondsCard.ToString());
            }
            if (tag == "$PKTIME")
            {
                source = ChangeNpcSayTag(source, "<$PKTIME>", hum.GetPKTimeMin());
            }
            // 咯包 焊包 俺荐
            if (tag == "$SAVEITEM")
            {
                source = ChangeNpcSayTag(source, "<$SAVEITEM>", hum.SaveItems.Count.ToString());
            }
            if (tag == "$REMAINSAVEITEM")
            {
                source = ChangeNpcSayTag(source, "<$REMAINSAVEITEM>", (ObjBase.MAXSAVELIMIT - hum.SaveItems.Count).ToString());
            }
            if (tag == "$MAXSAVEITEM")
            {
                source = ChangeNpcSayTag(source, "<$MAXSAVEITEM>", ObjBase.MAXSAVELIMIT.ToString());
            }
            // 巩颇 厘盔.
            if (tag == "$GUILDAGITREGFEE")
            {
                source = ChangeNpcSayTag(source, "<$GUILDAGITREGFEE>", M2Share.GetGoldStr(Guild.GUILDAGITREGFEE));
            }
            if (tag == "$GUILDAGITEXTENDFEE")
            {
                source = ChangeNpcSayTag(source, "<$GUILDAGITEXTENDFEE>", M2Share.GetGoldStr(Guild.GUILDAGITEXTENDFEE));
            }
            if (tag == "$GUILDAGITMAXGOLD")
            {
                source = ChangeNpcSayTag(source, "<$GUILDAGITMAXGOLD>", M2Share.GetGoldStr(Guild.GUILDAGITMAXGOLD));
            }
            // 厘盔操固扁.
            if (tag == "$AGITGUILDNAME")
            {
                source = ChangeNpcSayTag(source, "<$AGITGUILDNAME>", hum.GetGuildNameHereAgit());
            }
            if (tag == "$AGITGUILDMASTER")
            {
                source = ChangeNpcSayTag(source, "<$AGITGUILDMASTER>", hum.GetGuildMasterNameHereAgit());
            }
            if (tag == "$MEMORIALCOUNT")
            {
                source = ChangeNpcSayTag(source, "<$MEMORIALCOUNT>", MemorialCount.ToString());
            }
            if (tag == "$MASTERINFO1")
            {
                source = ChangeNpcSayTag(source, "<$MASTERINFO1>", hum.m_MasterRanking[0].sMasterName);
            }
            if (tag == "$MASTERINFO2")
            {
                source = ChangeNpcSayTag(source, "<$MASTERINFO2>", hum.m_MasterRanking[1].sMasterName);
            }
            if (tag == "$MASTERINFO3")
            {
                source = ChangeNpcSayTag(source, "<$MASTERINFO3>", hum.m_MasterRanking[2].sMasterName);
            }
            if (tag == "$MASTERINFO4")
            {
                source = ChangeNpcSayTag(source, "<$MASTERINFO4>", hum.m_MasterRanking[3].sMasterName);
            }
            if (tag == "$MASTERINFO5")
            {
                source = ChangeNpcSayTag(source, "<$MASTERINFO5>", hum.m_MasterRanking[4].sMasterName);
            }
            if (HUtil32.CompareLStr(tag, "$STR(", 5))
            {
                HUtil32.ArrestStringEx(tag, "(", ")", ref str2);
                n = ObjNpc.GetPP(str2);
                if (n >= 0)
                {
                    switch (n)
                    {
                        // Modify the A .. B: 0 .. 9
                        case 0:
                            source = ChangeNpcSayTag(source, "<" + tag + ">", hum.QuestParams[n].ToString());
                            break;
                        // Modify the A .. B: 100 .. 109
                        case 100:
                            source = ChangeNpcSayTag(source, "<" + tag + ">", M2Share.GrobalQuestParams[n - 100].ToString());
                            break;
                        // Modify the A .. B: 200 .. 209
                        case 200:
                            source = ChangeNpcSayTag(source, "<" + tag + ">", hum.DiceParams[n - 200].ToString());
                            break;
                        // Modify the A .. B: 300 .. 309
                        case 300:
                            source = ChangeNpcSayTag(source, "<" + tag + ">", this.PEnvir.MapQuestParams[n - 300].ToString());
                            break;
                    }
                }
            }
        }

        // 6-11
        public bool NpcSayTitle_CheckNameAndDeleteFromFileList(string uname, string listfile)
        {
            bool result;
            int i;
            string str = String.Empty;
            ArrayList strlist;
            result = false;
            listfile = M2Share.EnvirDir + listfile;
            if (File.Exists(listfile))
            {
                strlist = new ArrayList();
                try
                {
                    ObjNpc.ReadStrings(listfile, strlist);
                }
                catch
                {
                    M2Share.MainOutMessage("loading fail.... => " + listfile);
                }
                for (i = 0; i < strlist.Count; i++)
                {
                    //str = strlist[i].Trim();
                    if (str == uname)
                    {
                        strlist.Remove(i);
                        result = true;
                        break;
                    }
                }
                try
                {
                    ObjNpc.WriteStrings(listfile, strlist);
                }
                catch
                {
                    M2Share.MainOutMessage("saving fail.... => " + listfile);
                }
                strlist.Free();
            }
            else
            {
                M2Share.MainOutMessage("file not found => " + listfile);
            }
            return result;
        }

        // 6-11
        public bool NpcSayTitle_CheckNameFromFileList(string uname, string listfile)
        {
            bool result;
            int i;
            string str = String.Empty;
            ArrayList strlist;
            result = false;
            listfile = M2Share.EnvirDir + listfile;
            if (File.Exists(listfile))
            {
                strlist = new ArrayList();
                try
                {
                    ObjNpc.ReadStrings(listfile, strlist);
                }
                catch
                {
                    M2Share.MainOutMessage("loading fail.... => " + listfile);
                }
                for (i = 0; i < strlist.Count; i++)
                {
                    //str = strlist[i].Trim();
                    if (str == uname)
                    {
                        result = true;
                        break;
                    }
                }
                strlist.Free();
            }
            else
            {
                M2Share.MainOutMessage("file not found => " + listfile);
            }
            return result;
        }

        public void NpcSayTitle_AddNameFromFileList(string uname, string listfile)
        {
            ArrayList strlist;
            listfile = M2Share.EnvirDir + listfile;
            strlist = new ArrayList();
            if (File.Exists(listfile))
            {
                try
                {
                    ObjNpc.ReadStrings(listfile, strlist);
                }
                catch
                {
                    M2Share.MainOutMessage("loading fail.... => " + listfile);
                }
            }
            strlist.Add(uname);
            try
            {
                ObjNpc.WriteStrings(listfile, strlist);
            }
            catch
            {
                M2Share.MainOutMessage("saving fail.... => " + listfile);
            }
            strlist.Free();
        }

        public void NpcSayTitle_DeleteNameFromFileList(string uname, string listfile)
        {
            int i;
            string str = String.Empty;
            ArrayList strlist;
            listfile = M2Share.EnvirDir + listfile;
            if (File.Exists(listfile))
            {
                strlist = new ArrayList();
                try
                {
                    ObjNpc.ReadStrings(listfile, strlist);
                }
                catch
                {
                    M2Share.MainOutMessage("loading fail.... => " + listfile);
                }
                for (i = 0; i < strlist.Count; i++)
                {
                    //str = strlist[i].Trim();
                    if (str == uname)
                    {
                        strlist.Remove(i);
                        break;
                    }
                }
                try
                {
                    ObjNpc.WriteStrings(listfile, strlist);
                }
                catch
                {
                    M2Share.MainOutMessage("saving fail.... => " + listfile);
                }
                strlist.Free();
            }
            else
            {
                M2Share.MainOutMessage("file not found => " + listfile);
            }
        }

        public bool NpcSayTitle_CheckQuestCondition(TCreature who, TQuestRecord pq)
        {
            bool result = true;
            if (pq.BoRequire)
            {
                for (var i = 0; i < ObjNpc.MAXREQUIRE; i++)
                {
                    if (pq.QuestRequireArr[i].RandomCount > 0)
                    {
                        if (new System.Random(pq.QuestRequireArr[i].RandomCount).Next() != 0)
                        {
                            result = false;
                            break;
                        }
                    }
                    if (who.GetQuestMark(pq.QuestRequireArr[i].CheckIndex) != pq.QuestRequireArr[i].CheckValue)
                    {
                        result = false;
                        break;
                    }
                }
            }
            return result;
        }

        public TUserItem NpcSayTitle_FindItemFromState(TCreature who, string iname, int count = 0)
        {
            int n = 0;
            TUserItem result = null;
            if (HUtil32.CompareLStr(iname, "[NECKLACE]", 4))
            {
                if (who.UseItems[Grobal2.U_NECKLACE].Index > 0)
                {
                    result = who.UseItems[Grobal2.U_NECKLACE];
                }
                return result;
            }
            if (HUtil32.CompareLStr(iname, "[RING]", 4))
            {
                if (who.UseItems[Grobal2.U_RINGL].Index > 0)
                {
                    result = who.UseItems[Grobal2.U_RINGL];
                }
                if (who.UseItems[Grobal2.U_RINGR].Index > 0)
                {
                    result = who.UseItems[Grobal2.U_RINGR];
                }
                return result;
            }
            if (HUtil32.CompareLStr(iname, "[ARMRING]", 4))
            {
                if (who.UseItems[Grobal2.U_ARMRINGL].Index > 0)
                {
                    result = who.UseItems[Grobal2.U_ARMRINGL];
                }
                if (who.UseItems[Grobal2.U_ARMRINGR].Index > 0)
                {
                    result = who.UseItems[Grobal2.U_ARMRINGR];
                }
                return result;
            }
            if (HUtil32.CompareLStr(iname, "[WEAPON]", 4))
            {
                if (who.UseItems[Grobal2.U_WEAPON].Index > 0)
                {
                    result = who.UseItems[Grobal2.U_WEAPON];
                }
                return result;
            }
            if (HUtil32.CompareLStr(iname, "[HELMET]", 4))
            {
                if (who.UseItems[Grobal2.U_HELMET].Index > 0)
                {
                    result = who.UseItems[Grobal2.U_HELMET];
                }
                return result;
            }
            if (HUtil32.CompareLStr(iname, "[BUJUK]", 4))
            {
                if (who.UseItems[Grobal2.U_BUJUK].Index > 0)
                {
                    result = who.UseItems[Grobal2.U_BUJUK];
                }
                return result;
            }
            if (HUtil32.CompareLStr(iname, "[BELT]", 4))
            {
                if (who.UseItems[Grobal2.U_BELT].Index > 0)
                {
                    result = who.UseItems[Grobal2.U_BELT];
                }
                return result;
            }
            if (HUtil32.CompareLStr(iname, "[BOOTS]", 4))
            {
                if (who.UseItems[Grobal2.U_BOOTS].Index > 0)
                {
                    result = who.UseItems[Grobal2.U_BOOTS];
                }
                return result;
            }
            if (HUtil32.CompareLStr(iname, "[CHARM]", 4))
            {
                if (who.UseItems[Grobal2.U_CHARM].Index > 0)
                {
                    result = who.UseItems[Grobal2.U_CHARM];
                }
                return result;
            }
            result = who.FindItemWear(iname, ref n);
            if (n < count)
            {
                result = null;
            }
            return result;
        }

        public bool NpcSayTitle_CheckSayingCondition(TCreature who, NpcSayParams sayParams, ArrayList clist)
        {
            bool result;
            int i;
            int k;
            int m;
            int n;
            int param;
            int tag;
            int count = 0;
            int durasum = 0;
            int duratop = 0;
            int ahour;
            int amin;
            int asec;
            int amsec;
            TQuestConditionInfo pqc;
            TStdItem ps;
            TEnvirnoment penv;
            TUserHuman hum;
            int WarriorCount;
            int WizardCount;
            int TaoistCount;
            int equalvar;
            string CheckMap;
            TCreature cret;
            ArrayList list;
            bool flag;
            int btSex;
            int nLevel;
            char cMethod;
            result = true;
            for (i = 0; i < clist.Count; i++)
            {
                pqc = clist[i] as TQuestConditionInfo;
                switch (pqc.IfIdent)
                {
                    case Grobal2.QI_CHECK:
                        param = HUtil32.Str_ToInt(pqc.IfParam, 0);
                        tag = HUtil32.Str_ToInt(pqc.IfTag, 0);
                        n = who.GetQuestMark(param);
                        if (n == 0)
                        {
                            if (tag != 0)
                            {
                                result = false;
                            }
                        }
                        else if (tag == 0)
                        {
                            result = false;
                        }
                        break;
                    case Grobal2.QI_CHECKOPENUNIT:
                        param = HUtil32.Str_ToInt(pqc.IfParam, 0);
                        tag = HUtil32.Str_ToInt(pqc.IfTag, 0);
                        n = who.GetQuestOpenIndexMark(param);
                        if (n == 0)
                        {
                            if (tag != 0)
                            {
                                result = false;
                            }
                        }
                        else if (tag == 0)
                        {
                            result = false;
                        }
                        break;
                    case Grobal2.QI_CHECKUNIT:
                        param = HUtil32.Str_ToInt(pqc.IfParam, 0);
                        tag = HUtil32.Str_ToInt(pqc.IfTag, 0);
                        n = who.GetQuestFinIndexMark(param);
                        if (n == 0)
                        {
                            if (tag != 0)
                            {
                                result = false;
                            }
                        }
                        else if (tag == 0)
                        {
                            result = false;
                        }
                        break;
                    case Grobal2.QI_RANDOM:
                        if (new System.Random(pqc.IfParamVal).Next() != 0)
                        {
                            result = false;
                        }
                        break;
                    case Grobal2.QI_GENDER:
                        if (pqc.IfParam.ToLower().CompareTo("MAN".ToLower()) == 0)
                        {
                            // 夸备啊 巢磊
                            if (who.Sex != 0)
                            {
                                result = false;
                            }
                        }
                        else
                        {
                            if (who.Sex != 1)
                            {
                                result = false;
                            }
                        }
                        break;
                    case Grobal2.QI_DAYTIME:
                        if (pqc.IfParam.ToLower().CompareTo("SUNRAISE".ToLower()) == 0)
                        {
                            if (M2Share.MirDayTime != 0)
                            {
                                result = false;
                            }
                        }
                        if (pqc.IfParam.ToLower().CompareTo("DAY".ToLower()) == 0)
                        {
                            if (M2Share.MirDayTime != 1)
                            {
                                result = false;
                            }
                        }
                        if (pqc.IfParam.ToLower().CompareTo("SUNSET".ToLower()) == 0)
                        {
                            if (M2Share.MirDayTime != 2)
                            {
                                result = false;
                            }
                        }
                        if (pqc.IfParam.ToLower().CompareTo("NIGHT".ToLower()) == 0)
                        {
                            if (M2Share.MirDayTime != 3)
                            {
                                result = false;
                            }
                        }
                        break;
                    case Grobal2.QI_DAYOFWEEK:
                        if (HUtil32.CompareLStr(pqc.IfParam, "Sun", 3))
                        {
                            if (DateTime.Today.DayOfWeek != DayOfWeek.Monday)
                            {
                                result = false;
                            }
                        }
                        if (HUtil32.CompareLStr(pqc.IfParam, "Mon", 3))
                        {
                            if (DateTime.Today.DayOfWeek != DayOfWeek.Tuesday)
                            {
                                result = false;
                            }
                        }
                        if (HUtil32.CompareLStr(pqc.IfParam, "Tue", 3))
                        {
                            if (DateTime.Today.DayOfWeek != DayOfWeek.Wednesday)
                            {
                                result = false;
                            }
                        }
                        if (HUtil32.CompareLStr(pqc.IfParam, "Wed", 3))
                        {
                            if (DateTime.Today.DayOfWeek != DayOfWeek.Thursday)
                            {
                                result = false;
                            }
                        }
                        if (HUtil32.CompareLStr(pqc.IfParam, "Thu", 3))
                        {
                            if (DateTime.Today.DayOfWeek != DayOfWeek.Friday)
                            {
                                result = false;
                            }
                        }
                        if (HUtil32.CompareLStr(pqc.IfParam, "Fri", 3))
                        {
                            if (DateTime.Today.DayOfWeek != DayOfWeek.Saturday)
                            {
                                result = false;
                            }
                        }
                        if (HUtil32.CompareLStr(pqc.IfParam, "Sat", 3))
                        {
                            if (DateTime.Today.DayOfWeek != DayOfWeek.Sunday)
                            {
                                result = false;
                            }
                        }
                        break;
                    case Grobal2.QI_TIMEHOUR:
                        if ((pqc.IfParamVal != 0) && (pqc.IfTagVal == 0))
                        {
                            pqc.IfTagVal = pqc.IfParamVal;
                        }
                        ahour = DateTime.Now.Hour;
                        amin = DateTime.Now.Minute;
                        asec = DateTime.Now.Second;
                        amsec = DateTime.Now.Millisecond;
                        if (!((ahour >= pqc.IfParamVal) && (ahour <= pqc.IfTagVal)))
                        {
                            result = false;
                        }
                        break;
                    case Grobal2.QI_TIMEMIN:
                        if ((pqc.IfParamVal != 0) && (pqc.IfTagVal == 0))
                        {
                            pqc.IfTagVal = pqc.IfParamVal;
                        }
                        ahour = DateTime.Now.Hour;
                        amin = DateTime.Now.Minute;
                        asec = DateTime.Now.Second;
                        amsec = DateTime.Now.Millisecond;
                        if (!((amin >= pqc.IfParamVal) && (amin <= pqc.IfTagVal)))
                        {
                            result = false;
                        }
                        break;
                    case Grobal2.QI_CHECKITEM:
                        sayParams.pcheckitem = who.FindItemNameEx(pqc.IfParam, ref count, ref durasum, ref duratop);
                        if (count < pqc.IfTagVal)
                        {
                            result = false;
                        }
                        break;
                    case Grobal2.QI_CHECKITEMW:
                        //sayParams.pcheckitem = NpcSayTitle_FindItemFromState(pqc.IfParam, pqc.IfTagVal);
                        if (sayParams.pcheckitem == null)
                        {
                            result = false;
                        }
                        break;
                    case Grobal2.QI_CHECKGOLD:
                        if (who.Gold < pqc.IfParamVal)
                        {
                            result = false;
                        }
                        break;
                    case Grobal2.QI_ISTAKEITEM:
                        if (sayParams.latesttakeitem != pqc.IfParam)
                        {
                            result = false;
                        }
                        break;
                    case Grobal2.QI_CHECKLEVEL:
                        if (who.Abil.Level < pqc.IfParamVal)
                        {
                            result = false;
                        }
                        break;
                    case Grobal2.QI_CHECKJOB:
                        if (HUtil32.CompareLStr(pqc.IfParam, "Warrior", 3))
                        {
                            if (who.Job != 0)
                            {
                                result = false;
                            }
                        }
                        if (HUtil32.CompareLStr(pqc.IfParam, "Wizard", 3))
                        {
                            if (who.Job != 1)
                            {
                                result = false;
                            }
                        }
                        if (HUtil32.CompareLStr(pqc.IfParam, "Taoist", 3))
                        {
                            if (who.Job != 2)
                            {
                                result = false;
                            }
                        }
                        break;
                    case Grobal2.QI_CHECKDURA:
                        //pcheckitem = who.FindItemNameEx(pqc.IfParam, ref count, ref durasum, ref duratop);
                        if (HUtil32.MathRound(duratop / 1000) < pqc.IfTagVal)
                        {
                            result = false;
                        }
                        break;
                    case Grobal2.QI_CHECKDURAEVA:
                        //pcheckitem = who.FindItemNameEx(pqc.IfParam, ref count, ref durasum, ref duratop);
                        if (count > 0)
                        {
                            if (HUtil32.MathRound(durasum / count / 1000) < pqc.IfTagVal)
                            {
                                result = false;
                            }
                        }
                        else
                        {
                            result = false;
                        }
                        break;
                    case Grobal2.QI_CHECKPKPOINT:
                        if (who.PKLevel() < pqc.IfParamVal)
                        {
                            result = false;
                        }
                        break;
                    case Grobal2.QI_CHECKLUCKYPOINT:
                        if (who.BodyLuckLevel < pqc.IfParamVal)
                        {
                            result = false;
                        }
                        break;
                    case Grobal2.QI_CHECKMON_MAP:
                        penv = M2Share.GrobalEnvir.GetEnvir(pqc.IfParam);
                        if (penv != null)
                        {
                            if (M2Share.UserEngine.GetMapMons(penv, null) < pqc.IfTagVal)
                            {
                                result = false;
                            }
                        }
                        break;
                    case Grobal2.QI_CHECKMON_NORECALLMOB_MAP:
                        penv = M2Share.GrobalEnvir.GetEnvir(pqc.IfParam);
                        if (penv != null)
                        {
                            if (M2Share.UserEngine.GetMapMonsNoRecallMob(penv, null) < pqc.IfTagVal)
                            {
                                result = false;
                            }
                        }
                        break;
                    case Grobal2.QI_CHECKMON_AREA:
                        break;
                    case Grobal2.QI_CHECKHUM:
                        if (M2Share.UserEngine.GetHumCount(pqc.IfParam) < pqc.IfTagVal)
                        {
                            result = false;
                        }
                        break;
                    case Grobal2.QI_CHECKBAGGAGE:
                        if (who.CanAddItem())
                        {
                            if (pqc.IfParam != "")
                            {
                                result = false;
                                ps = M2Share.UserEngine.GetStdItemFromName(pqc.IfParam);
                                if (ps != null)
                                {
                                    if (who.IsAddWeightAvailable(ps.Weight))
                                    {
                                        result = true;
                                    }
                                }
                            }
                        }
                        else
                        {
                            result = false;
                        }
                        break;
                    case Grobal2.QI_CHECKANDDELETENAMELIST:
                        // 6-11
                        // filename
                        if (!NpcSayTitle_CheckNameAndDeleteFromFileList(who.UserName, NpcBaseDir + pqc.IfParam))
                        {
                            result = false;
                        }
                        break;
                    case Grobal2.QI_CHECKANDDELETEIDLIST:
                        // 6-11
                        hum = (TUserHuman)who;
                        // filename
                        if (!NpcSayTitle_CheckNameAndDeleteFromFileList(hum.UserId, NpcBaseDir + pqc.IfParam))
                        {
                            result = false;
                        }
                        break;
                    case Grobal2.QI_CHECKNAMELIST:
                        // 6-11
                        // filename
                        if (!NpcSayTitle_CheckNameFromFileList(who.UserName, NpcBaseDir + pqc.IfParam))
                        {
                            result = false;
                        }
                        break;
                    case Grobal2.QI_IFGETDAILYQUEST:
                        // *dq
                        if (who.GetDailyQuest() != 0)
                        {
                            result = false;
                        }
                        break;
                    case Grobal2.QI_RANDOMEX:
                        // *dq
                        if (new System.Random(pqc.IfTagVal).Next() >= pqc.IfParamVal)
                        {
                            result = false;
                        }
                        break;
                    case Grobal2.QI_CHECKDAILYQUEST:
                        // *dq
                        if (who.GetDailyQuest() != pqc.IfParamVal)
                        {
                            result = false;
                        }
                        break;
                    case Grobal2.QI_CHECKGRADEITEM:
                        if (who.FindItemEventGrade(pqc.IfParamVal, pqc.IfTagVal) == false)
                        {
                            result = false;
                        }
                        break;
                    case Grobal2.QI_CHECKBAGREMAIN:
                        if (who.CanAddItem())
                        {
                            if (pqc.IfParamVal > 0)
                            {
                                result = false;
                                if ((Grobal2.MAXBAGITEM - who.ItemList.Count) >= pqc.IfParamVal)
                                {
                                    result = true;
                                }
                            }
                        }
                        else
                        {
                            result = false;
                        }
                        break;
                    case Grobal2.QI_EQUALVAR:
                        equalvar = 0;
                        n = ObjNpc.GetPP(pqc.IfParam);
                        m = ObjNpc.GetPP(pqc.IfTag);
                        if (m >= 0)
                        {
                            switch (m)
                            {
                                // Modify the A .. B: 0 .. 9
                                case 0:
                                    equalvar = ((TUserHuman)who).QuestParams[m];
                                    break;
                                // Modify the A .. B: 100 .. 109
                                case 100:
                                    equalvar = M2Share.GrobalQuestParams[m - 100];
                                    break;
                                // Modify the A .. B: 200 .. 209
                                case 200:
                                    equalvar = ((TUserHuman)who).DiceParams[m - 200];
                                    break;
                                // Modify the A .. B: 300 .. 309
                                case 300:
                                    equalvar = this.PEnvir.MapQuestParams[m - 300];
                                    break;
                            }
                        }
                        if (n >= 0)
                        {
                            switch (n)
                            {
                                // Modify the A .. B: 0 .. 9
                                case 0:
                                    if (((TUserHuman)who).QuestParams[n] != equalvar)
                                    {
                                        result = false;
                                    }
                                    break;
                                // Modify the A .. B: 100 .. 109
                                case 100:
                                    // 傈开函荐
                                    if (M2Share.GrobalQuestParams[n - 100] != equalvar)
                                    {
                                        result = false;
                                    }
                                    break;
                                // Modify the A .. B: 200 .. 209
                                case 200:
                                    if (((TUserHuman)who).DiceParams[n - 200] != equalvar)
                                    {
                                        result = false;
                                    }
                                    break;
                                // Modify the A .. B: 300 .. 309
                                case 300:
                                    // 甘瘤开函荐
                                    if (this.PEnvir.MapQuestParams[n - 300] != equalvar)
                                    {
                                        result = false;
                                    }
                                    break;
                            }
                        }
                        else
                        {
                            result = false;
                        }
                        break;
                    case Grobal2.QI_EQUAL:
                        n = ObjNpc.GetPP(pqc.IfParam);
                        if (n >= 0)
                        {
                            switch (n)
                            {
                                // Modify the A .. B: 0 .. 9
                                case 0:
                                    if (((TUserHuman)who).QuestParams[n] != pqc.IfTagVal)
                                    {
                                        result = false;
                                    }
                                    break;
                                // Modify the A .. B: 100 .. 109
                                case 100:
                                    // 傈开函荐
                                    if (M2Share.GrobalQuestParams[n - 100] != pqc.IfTagVal)
                                    {
                                        result = false;
                                    }
                                    break;
                                // Modify the A .. B: 200 .. 209
                                case 200:
                                    if (((TUserHuman)who).DiceParams[n - 200] != pqc.IfTagVal)
                                    {
                                        result = false;
                                    }
                                    break;
                                // Modify the A .. B: 300 .. 309
                                case 300:
                                    // 甘瘤开函荐
                                    if (this.PEnvir.MapQuestParams[n - 300] != pqc.IfTagVal)
                                    {
                                        result = false;
                                    }
                                    break;
                            }
                        }
                        else
                        {
                            result = false;
                        }
                        break;
                    case Grobal2.QI_LARGE:
                        n = ObjNpc.GetPP(pqc.IfParam);
                        if (n >= 0)
                        {
                            switch (n)
                            {
                                // Modify the A .. B: 0 .. 9
                                case 0:
                                    if (((TUserHuman)who).QuestParams[n] <= pqc.IfTagVal)
                                    {
                                        result = false;
                                    }
                                    break;
                                // Modify the A .. B: 100 .. 109
                                case 100:
                                    if (M2Share.GrobalQuestParams[n - 100] <= pqc.IfTagVal)
                                    {
                                        result = false;
                                    }
                                    break;
                                // Modify the A .. B: 200 .. 209
                                case 200:
                                    if (((TUserHuman)who).DiceParams[n - 200] <= pqc.IfTagVal)
                                    {
                                        result = false;
                                    }
                                    break;
                                // Modify the A .. B: 300 .. 309
                                case 300:
                                    if (this.PEnvir.MapQuestParams[n - 300] <= pqc.IfTagVal)
                                    {
                                        result = false;
                                    }
                                    break;
                            }
                        }
                        else
                        {
                            result = false;
                        }
                        break;
                    case Grobal2.QI_SMALL:
                        n = ObjNpc.GetPP(pqc.IfParam);
                        if (n >= 0)
                        {
                            switch (n)
                            {
                                // Modify the A .. B: 0 .. 9
                                case 0:
                                    if (((TUserHuman)who).QuestParams[n] >= pqc.IfTagVal)
                                    {
                                        result = false;
                                    }
                                    break;
                                // Modify the A .. B: 100 .. 109
                                case 100:
                                    if (M2Share.GrobalQuestParams[n - 100] >= pqc.IfTagVal)
                                    {
                                        result = false;
                                    }
                                    break;
                                // Modify the A .. B: 200 .. 209
                                case 200:
                                    if (((TUserHuman)who).DiceParams[n - 200] >= pqc.IfTagVal)
                                    {
                                        result = false;
                                    }
                                    break;
                                // Modify the A .. B: 300 .. 309
                                case 300:
                                    if (this.PEnvir.MapQuestParams[n - 300] >= pqc.IfTagVal)
                                    {
                                        result = false;
                                    }
                                    break;
                            }
                        }
                        else
                        {
                            result = false;
                        }
                        break;
                    case Grobal2.QI_ISGROUPOWNER:
                        // 磊脚捞 GroupOwner牢瘤 八荤.
                        result = false;
                        if (who != null)
                        {
                            if (who.GroupOwner != null)
                            {
                                if (who.GroupOwner == who)
                                {
                                    result = true;
                                }
                            }
                        }
                        break;
                    case Grobal2.QI_ISEXPUSER:
                        // 磊脚捞 眉氰魄 蜡历牢瘤 八荤茄促.
                        result = false;
                        hum = (TUserHuman)who;
                        if (hum != null)
                        {
                            if (hum.ApprovalMode == 1)
                            {
                                result = true;
                            }
                        }
                        break;
                    case Grobal2.QI_CHECKLOVERFLAG:
                        if (((TUserHuman)who).fLover != null)
                        {
                            hum = M2Share.UserEngine.GetUserHuman(((TUserHuman)who).fLover.GetLoverName());
                            if (hum != null)
                            {
                                param = HUtil32.Str_ToInt(pqc.IfParam, 0);
                                tag = HUtil32.Str_ToInt(pqc.IfTag, 0);
                                n = hum.GetQuestMark(param);
                                if (n == 0)
                                {
                                    if (tag != 0)
                                    {
                                        result = false;
                                    }
                                }
                                else if (tag == 0)
                                {
                                    result = false;
                                }
                            }
                            else
                            {
                                result = false;
                            }
                        }
                        else
                        {
                            result = false;
                        }
                        break;
                    case Grobal2.QI_CHECKLOVERRANGE:
                        if (((TUserHuman)who).fLover != null)
                        {
                            hum = M2Share.UserEngine.GetUserHuman(((TUserHuman)who).fLover.GetLoverName());
                            if (hum != null)
                            {
                                param = HUtil32.Str_ToInt(pqc.IfParam, 0);
                                if (!((Math.Abs(who.CX - hum.CX) <= param) && (Math.Abs(who.CY - hum.CY) <= param)))
                                {
                                    result = false;
                                }
                            }
                            else
                            {
                                result = false;
                            }
                        }
                        else
                        {
                            result = false;
                        }
                        break;
                    case Grobal2.QI_CHECKLOVERDAY:
                        if (((TUserHuman)who).fLover != null)
                        {
                            param = HUtil32.Str_ToInt(pqc.IfParam, 0);
                            // MainOutMessage('GetLoverDays(3) : ' + TUserHuman(who).fLover.GetLoverDays);
                            if (HUtil32.Str_ToInt(((TUserHuman)who).fLover.GetLoverDays(), 0) < param)
                            {
                                result = false;
                            }
                        }
                        else
                        {
                            result = false;
                        }
                        break;
                    case Grobal2.QI_CHECKDONATION:
                        if (((TUserHuman)who).GetGuildAgitDonation() < pqc.IfParamVal)
                        {
                            result = false;
                        }
                        break;
                    case Grobal2.QI_ISGUILDMASTER:
                        if (!who.IsGuildMaster())
                        {
                            result = false;
                        }
                        break;
                    case Grobal2.QI_CHECKWEAPONBADLUCK:
                        if (who.UseItems[Grobal2.U_WEAPON].Index != 0)
                        {
                            if (!(who.UseItems[Grobal2.U_WEAPON].Desc[4] - who.UseItems[Grobal2.U_WEAPON].Desc[3] > 0))
                            {
                                // 历林啊 嘿绢乐阑 锭
                                result = false;
                            }
                        }
                        break;
                    case Grobal2.QI_CHECKCHILDMOB:
                        if (who.GetExistSlave(pqc.IfParam) == null)
                        {
                            result = false;
                        }
                        break;
                    case Grobal2.QI_CHECKGROUPJOBBALANCE:
                        WarriorCount = 0;
                        WizardCount = 0;
                        TaoistCount = 0;
                        CheckMap = "";
                        for (k = 0; k < who.GroupMembers.Count; k++)
                        {
                            hum = M2Share.UserEngine.GetUserHuman(who.GroupMembers[k].UserName);
                            if (hum != null)
                            {
                                // 鞍篮 甘俊 乐绰瘤 眉农
                                if (CheckMap == "")
                                {
                                    CheckMap = hum.MapName;
                                }
                                else
                                {
                                    if (CheckMap != hum.MapName)
                                    {
                                        result = false;
                                    }
                                }
                                if (hum.Job == 0)
                                {
                                    WarriorCount++;
                                }
                                else if (hum.Job == 1)
                                {
                                    WizardCount++;
                                }
                                else if (hum.Job == 2)
                                {
                                    TaoistCount++;
                                }
                            }
                        }
                        if (!((WarriorCount == pqc.IfParamVal) && (WizardCount == pqc.IfParamVal) && (TaoistCount == pqc.IfParamVal)))
                        {
                            result = false;
                        }
                        break;
                    case Grobal2.QI_CHECKRANGEONELOVER:
                        // 林函俊 楷牢 肝篮 荤恩捞 乐绰瘤 眉农
                        flag = false;
                        list = new ArrayList();
                        // 甘涅胶飘甫 困秦 PEnvir 措脚俊 who.PEnvir甫 荤侩茄促.
                        who.PEnvir.GetCreatureInRange(who.CX, who.CY, pqc.IfParamVal, true, list);
                        for (k = 0; k < list.Count; k++)
                        {
                            cret = (TCreature)list[k];
                            if ((cret != null) && (cret.RaceServer == Grobal2.RC_USERHUMAN))
                            {
                                hum = (TUserHuman)cret;
                                if ((hum.fLover != null) && (hum.fLover.GetLoverName() != "") && (hum != who))
                                {
                                    flag = true;
                                    break;
                                }
                            }
                        }
                        list.Free();
                        if (!flag)
                        {
                            result = false;
                        }
                        break;
                    case Grobal2.QI_ISNEWHUMAN:
                        result = ((TUserHuman)who).FirstTimeConnection;
                        break;
                    case Grobal2.QI_ISSYSOP:
                        if (!(who.UserDegree >= 4))
                        {
                            result = false;
                        }
                        break;
                    case Grobal2.QI_ISADMIN:
                        if (!(who.UserDegree >= 8))
                        {
                            result = false;
                        }
                        break;
                    case Grobal2.QI_CHECKGAMEGOLD:
                        if (((TUserHuman)who).GameGold < pqc.IfParamVal)
                        {
                            result = false;
                        }
                        break;
                    case Grobal2.QI_CHECKMARRY:
                        if (((TUserHuman)who).fLover.GetLoverName() == "")
                        {
                            result = false;
                        }
                        break;
                    case Grobal2.QI_CHECKPOSEMARRY:
                        cret = ((TUserHuman)who).GetFrontCret();
                        if ((cret != null) && (cret.RaceServer == Grobal2.RC_USERHUMAN))
                        {
                            if (((TUserHuman)cret).fLover.GetLoverName() == "")
                            {
                                result = false;
                            }
                        }
                        break;
                    case Grobal2.QI_CHECKPOSEGENDER:
                        btSex = 0;
                        if (pqc.IfParam.ToLower().CompareTo("MAN".ToLower()) == 0)
                        {
                            btSex = 0;
                        }
                        else if (pqc.IfParam.ToLower().CompareTo("男".ToLower()) == 0)
                        {
                            btSex = 0;
                        }
                        else if (pqc.IfParam.ToLower().CompareTo("WOMAN".ToLower()) == 0)
                        {
                            btSex = 1;
                        }
                        else if (pqc.IfParam.ToLower().CompareTo("女".ToLower()) == 0)
                        {
                            btSex = 1;
                        }
                        cret = ((TUserHuman)who).GetFrontCret();
                        if ((cret != null) && (cret.RaceServer == Grobal2.RC_USERHUMAN))
                        {
                            if (cret.Sex != btSex)
                            {
                                result = false;
                            }
                        }
                        break;
                    case Grobal2.QI_CHECKPOSEDIR:
                        result = false;
                        cret = ((TUserHuman)who).GetFrontCret();
                        if ((cret != null) && (cret.GetFrontCret() == who) && (cret.RaceServer == Grobal2.RC_USERHUMAN))
                        {
                            switch (pqc.IfParamVal)
                            {
                                case 1:
                                    if (cret.Sex == who.Sex)
                                    {
                                        result = true;
                                    }
                                    break;
                                case 2:
                                    // 要求相同性别
                                    if (cret.Sex != who.Sex)
                                    {
                                        result = true;
                                    }
                                    break;
                                default:
                                    // 要求不同性别
                                    result = true;
                                    break;
                                    // 无参数时不判别性别
                            }
                        }
                        break;
                    case Grobal2.QI_CHECKPOSELEVEL:
                        result = false;
                        nLevel = HUtil32.Str_ToInt(pqc.IfTag, -1);
                        if (nLevel < 0)
                        {
                            // ScriptConditionError(PlayObject, QuestConditionInfo, sSC_CHECKPOSELEVEL);
                            return result;
                        }
                        cMethod = pqc.IfParam[1];
                        cret = ((TUserHuman)who).GetFrontCret();
                        if ((cret != null) && (cret.RaceServer == Grobal2.RC_USERHUMAN))
                        {
                            switch (cMethod)
                            {
                                case '=':
                                    if (cret.Abil.Level == nLevel)
                                    {
                                        result = true;
                                    }
                                    break;
                                case '>':
                                    if (cret.Abil.Level > nLevel)
                                    {
                                        result = true;
                                    }
                                    break;
                                case '<':
                                    if (cret.Abil.Level < nLevel)
                                    {
                                        result = true;
                                    }
                                    break;
                                default:
                                    if (cret.Abil.Level >= nLevel)
                                    {
                                        result = true;
                                    }
                                    break;
                            }
                        }
                        break;
                    case Grobal2.QI_CHECKMASTER:
                        // 检测是否拜师
                        result = false;
                        if ((who.m_sMasterName != "") && (!who.m_boMaster))
                        {
                            result = true;
                        }
                        break;
                    case Grobal2.QI_CHECKISMASTER:
                        // 检测是不是别人师傅
                        result = false;
                        if (who.m_boMaster)
                        {
                            result = true;
                        }
                        break;
                    case Grobal2.QI_CHECKPOSEMASTER:
                        // 检测是否是别人徒弟
                        result = false;
                        cret = who.GetFrontCret();
                        if ((cret != null) && (cret.RaceServer == Grobal2.RC_USERHUMAN))
                        {
                            if ((((TUserHuman)cret).m_sMasterName != "") && !((TUserHuman)cret).m_boMaster)
                            {
                                result = true;
                            }
                        }
                        break;
                    case Grobal2.QI_HAVEMASTER:
                        result = false;
                        if (who.m_boMaster)
                        {
                            result = true;
                        }
                        break;
                    case Grobal2.QI_CHECKMASTERCOUNT:
                        result = false;
                        nLevel = HUtil32.Str_ToInt(pqc.IfTag, -1);
                        if (!who.m_boMaster)
                        {
                            if (nLevel < 0)
                            {
                                cret = who.GetFrontCret();
                                if ((cret != null) && (cret.RaceServer == Grobal2.RC_USERHUMAN))
                                {
                                    if (((TUserHuman)cret).m_nMasterCount > 4)
                                    {
                                        result = true;
                                    }
                                }
                            }
                            else
                            {
                                cret = who.GetFrontCret();
                                if ((cret != null) && (cret.RaceServer == Grobal2.RC_USERHUMAN))
                                {
                                    cMethod = pqc.IfParam[1];
                                    switch (cMethod)
                                    {
                                        case '=':
                                            if (((TUserHuman)cret).m_nMasterCount == nLevel)
                                            {
                                                result = true;
                                            }
                                            break;
                                        case '>':
                                            if (((TUserHuman)cret).m_nMasterCount > nLevel)
                                            {
                                                result = true;
                                            }
                                            break;
                                        case '<':
                                            if (((TUserHuman)cret).m_nMasterCount < nLevel)
                                            {
                                                result = true;
                                            }
                                            break;
                                        default:
                                            if (((TUserHuman)cret).m_nMasterCount >= nLevel)
                                            {
                                                result = true;
                                            }
                                            break;
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (nLevel < 0)
                            {
                                if (who.m_nMasterCount > 4)
                                {
                                    result = true;
                                }
                            }
                            else
                            {
                                cMethod = pqc.IfParam[1];
                                if (who.RaceServer == Grobal2.RC_USERHUMAN)
                                {
                                    switch (cMethod)
                                    {
                                        case '=':
                                            if (who.m_nMasterCount == nLevel)
                                            {
                                                result = true;
                                            }
                                            break;
                                        case '>':
                                            if (who.m_nMasterCount > nLevel)
                                            {
                                                result = true;
                                            }
                                            break;
                                        case '<':
                                            if (who.m_nMasterCount < nLevel)
                                            {
                                                result = true;
                                            }
                                            break;
                                        default:
                                            if (who.m_nMasterCount >= nLevel)
                                            {
                                                result = true;
                                            }
                                            break;
                                    }
                                }
                            }
                        }
                        break;
                }
            }
            return result;
        }

        public void NpcSayTitle_GotoQuest(TCreature who, int num)
        {
            for (var i = 0; i < Sayings.Count; i++)
            {
                if ((Sayings[i] as TQuestRecord).LocalNumber == num)
                {
                    ((TUserHuman)who).CurQuest = Sayings[i] as TQuestRecord;
                    ((TUserHuman)who).CurQuestNpc = this;
                    NpcSayTitle(who, "@main");
                    break;
                }
            }
        }

        public void NpcSayTitle_GotoSay(TCreature who, string saystr)
        {
            NpcSayTitle(who, saystr);
        }

        public void NpcSayTitle_ActionOfMarry(TUserHuman who, TQuestActionInfo pqa)
        {
            TUserHuman PoseHuman;
            string sSayMsg = string.Empty;
            string msgstr = string.Empty;
            string data = string.Empty;
            string str = string.Empty;
            int listCount = 0;
            if (who.fLover.GetLoverName() != "")
            {
                return;
            }
            PoseHuman = (TUserHuman)who.GetFrontCret();
            if (PoseHuman == null)
            {
                NpcSayTitle(who, "@MarryCheckDir");
                return;
            }
            if (pqa.ActParam == "")
            {
                if (PoseHuman.RaceServer != Grobal2.RC_USERHUMAN)
                {
                    NpcSayTitle(who, "@HumanTypeErr");
                    return;
                }
                if (PoseHuman.GetFrontCret() == who)
                {
                    if (who.Sex != PoseHuman.Sex)
                    {
                        NpcSayTitle(who, "@StartMarry");
                        NpcSayTitle(PoseHuman, "@StartMarry");
                        if ((who.Sex == 0) && (PoseHuman.Sex == 1))
                        {
                            sSayMsg = M2Share.g_sStartMarryManMsg.Replace("%n", this.UserName);
                            sSayMsg = sSayMsg.Replace("%s", who.UserName);
                            sSayMsg = sSayMsg.Replace("%d", PoseHuman.UserName);
                            M2Share.UserEngine.SendBroadCastMsg(sSayMsg, 0);
                            sSayMsg = M2Share.g_sStartMarryManAskQuestionMsg.Replace("%n", this.UserName);
                            sSayMsg = sSayMsg.Replace("%s", who.UserName);
                            sSayMsg = sSayMsg.Replace("%d", PoseHuman.UserName);
                            M2Share.UserEngine.SendBroadCastMsg(sSayMsg, 0);
                        }
                        else if ((who.Sex == 1) && (PoseHuman.Sex == 0))
                        {
                            sSayMsg = M2Share.g_sStartMarryWoManMsg.Replace("%n", this.UserName);
                            sSayMsg = sSayMsg.Replace("%s", who.UserName);
                            sSayMsg = sSayMsg.Replace("%d", PoseHuman.UserName);
                            M2Share.UserEngine.SendBroadCastMsg(sSayMsg, 0);
                            sSayMsg = M2Share.g_sStartMarryWoManAskQuestionMsg.Replace("%n", this.UserName);
                            sSayMsg = sSayMsg.Replace("%s", who.UserName);
                            sSayMsg = sSayMsg.Replace("%d", PoseHuman.UserName);
                            M2Share.UserEngine.SendBroadCastMsg(sSayMsg, 0);
                        }
                        who.m_boStartMarry = true;
                        PoseHuman.m_boStartMarry = true;
                    }
                    else
                    {
                        NpcSayTitle(PoseHuman, "@MarrySexErr");
                        NpcSayTitle(who, "@MarrySexErr");
                    }
                }
                else
                {
                    NpcSayTitle(who, "@MarryDirErr");
                    NpcSayTitle(PoseHuman, "@MarryCheckDir");
                }
                return;
            }
            // sREQUESTMARRY
            if (pqa.ActParam.ToLower().CompareTo("REQUESTMARRY".ToLower()) == 0)
            {
                if (who.m_boStartMarry && PoseHuman.m_boStartMarry)
                {
                    if ((who.Sex == 0) && (PoseHuman.Sex == 1))
                    {
                        sSayMsg = M2Share.g_sMarryManAnswerQuestionMsg.Replace("%n", this.UserName);
                        sSayMsg = sSayMsg.Replace("%s", who.UserName);
                        sSayMsg = sSayMsg.Replace("%d", PoseHuman.UserName);
                        M2Share.UserEngine.SendBroadCastMsg(sSayMsg, 0);
                        sSayMsg = M2Share.g_sMarryManAskQuestionMsg.Replace("%n", this.UserName);
                        sSayMsg = sSayMsg.Replace("%s", who.UserName);
                        sSayMsg = sSayMsg.Replace("%d", PoseHuman.UserName);
                        M2Share.UserEngine.SendBroadCastMsg(sSayMsg, 0);
                        NpcSayTitle(who, "@WateMarry");
                        NpcSayTitle(PoseHuman, "@RevMarry");
                    }
                }
                return;
            }
            // sRESPONSEMARRY
            if (pqa.ActParam.ToLower().CompareTo("RESPONSEMARRY".ToLower()) == 0)
            {
                if ((who.Sex == 1) && (PoseHuman.Sex == 0))
                {
                    if (pqa.ActTag.ToLower().CompareTo("OK".ToLower()) == 0)
                    {
                        if (who.m_boStartMarry && PoseHuman.m_boStartMarry)
                        {
                            sSayMsg = M2Share.g_sMarryWoManAnswerQuestionMsg.Replace("%n", this.UserName);
                            sSayMsg = sSayMsg.Replace("%s", who.UserName);
                            sSayMsg = sSayMsg.Replace("%d", PoseHuman.UserName);
                            M2Share.UserEngine.SendBroadCastMsg(sSayMsg, 0);
                            sSayMsg = M2Share.g_sMarryWoManGetMarryMsg.Replace("%n", this.UserName);
                            sSayMsg = sSayMsg.Replace("%s", who.UserName);
                            sSayMsg = sSayMsg.Replace("%d", PoseHuman.UserName);
                            M2Share.UserEngine.SendBroadCastMsg(sSayMsg, 0);
                            NpcSayTitle(who, "@EndMarry");
                            NpcSayTitle(PoseHuman, "@EndMarry");
                            who.m_boStartMarry = false;
                            PoseHuman.m_boStartMarry = false;
                            data = "";
                            who.fLover.ReqSequence = Grobal2.RsReq_None;
                            who.fLover.Add(who.UserName, PoseHuman.UserName, Grobal2.RsState_Lover, PoseHuman.WAbil.Level, PoseHuman.Sex, ref data, "");
                            msgstr = who.fLover.GetListMsg(Grobal2.RsState_Lover, ref listCount);
                            who.SendDefMessage(Grobal2.SM_LM_LIST, 0, listCount, 0, 0, msgstr);
                            who.SendDefMessage(Grobal2.SM_LM_RESULT, 0, Grobal2.RsState_Lover, Grobal2.RsError_SuccessJoin, 0, PoseHuman.UserName);
                            PoseHuman.fLover.ReqSequence = Grobal2.RsReq_None;
                            PoseHuman.fLover.Add(PoseHuman.UserName, who.UserName, Grobal2.RsState_Lover, who.WAbil.Level, who.Sex, ref data, "");
                            msgstr = PoseHuman.fLover.GetListMsg(Grobal2.RsState_Lover, ref listCount);
                            PoseHuman.SendDefMessage(Grobal2.SM_LM_LIST, 0, listCount, 0, 0, msgstr);
                            PoseHuman.SendDefMessage(Grobal2.SM_LM_RESULT, 0, Grobal2.RsState_Lover, Grobal2.RsError_SuccessJoined, 0, who.UserName);
                            who.SendMsg(who, Grobal2.RM_LM_DBADD, 0, 0, 0, 0, PoseHuman.UserName + ":" + 10.ToString() + ":" + data + "/");
                            str = "恭喜！\"" + who.UserName + "\"与\"" + PoseHuman.UserName + "\"成为情侣。";
                            M2Share.UserEngine.CryCry(Grobal2.RM_SYSMSG_PINK, this.PEnvir, this.CX, this.CY, 300, ":)" + str);
                            M2Share.AddUserLog("47\09" + this.MapName + "\09" + this.CX.ToString() + "\09" + this.CY.ToString() + "\09" + who.UserName + "\09" + "0\09" + "0\09" + "0\09" + PoseHuman.UserName);
                            who.UserNameChanged();
                            PoseHuman.UserNameChanged();
                        }
                    }
                    else
                    {
                        if (who.m_boStartMarry && PoseHuman.m_boStartMarry)
                        {
                            NpcSayTitle(who, "@EndMarryFail");
                            NpcSayTitle(PoseHuman, "@EndMarryFail");
                            who.m_boStartMarry = false;
                            PoseHuman.m_boStartMarry = false;
                            sSayMsg = M2Share.g_sMarryWoManDenyMsg.Replace("%n", this.UserName);
                            sSayMsg = sSayMsg.Replace("%s", who.UserName);
                            sSayMsg = sSayMsg.Replace("%d", PoseHuman.UserName);
                            M2Share.UserEngine.SendBroadCastMsg(sSayMsg, 0);
                            sSayMsg = M2Share.g_sMarryWoManCancelMsg.Replace("%n", this.UserName);
                            sSayMsg = sSayMsg.Replace("%s", who.UserName);
                            sSayMsg = sSayMsg.Replace("%d", PoseHuman.UserName);
                            M2Share.UserEngine.SendBroadCastMsg(sSayMsg, 0);
                        }
                    }
                }
                return;
            }
        }

        public void NpcSayTitle_ActionOfUnMarry(TUserHuman who, TQuestActionInfo pqa)
        {
            TUserHuman PoseHuman;
            int svidx = 0;
            if (who.fLover.GetLoverName() == "")
            {
                NpcSayTitle(who, "@ExeMarryFail");
                // 你都没结过婚，跑来做什么？ \ \
                return;
            }
            PoseHuman = (TUserHuman)who.GetFrontCret();
            if (PoseHuman == null)
            {
                NpcSayTitle(who, "@UnMarryCheckDir");
                // 检测婚姻是否对面
            }
            if (PoseHuman != null)
            {
                if (pqa.ActParam == "")
                {
                    if (PoseHuman.RaceServer != Grobal2.RC_USERHUMAN)
                    {
                        NpcSayTitle(who, "@UnMarryTypeErr");
                        // 对面不是人物
                        return;
                    }
                    if (PoseHuman.GetFrontCret() == who)
                    {
                        if (who.fLover.GetLoverName() == PoseHuman.UserName)
                        {
                            NpcSayTitle(who, "@StartUnMarry");
                            // 开始离婚
                            NpcSayTitle(PoseHuman, "@StartUnMarry");
                            return;
                        }
                    }
                }
            }
            // sREQUESTUNMARRY
            if (pqa.ActParam.ToLower().CompareTo("REQUESTUNMARRY".ToLower()) == 0)
            {
                // 确定离婚
                if (pqa.ActTag == "")
                {
                    if (PoseHuman != null)
                    {
                        who.m_boStartUnMarry = true;
                        if (who.m_boStartUnMarry && PoseHuman.m_boStartUnMarry)
                        {
                            if (who.RelationShipDeleteOther(Grobal2.RsState_Lover, PoseHuman.UserName))
                            {
                                this.MakePoison(Grobal2.POISON_SLOW, 3, 1);
                                // HP, MP 函版(50%)
                                who.WAbil.HP = (short)_MAX(1, who.WAbil.HP / 2);
                                who.WAbil.MP = (short)_MAX(1, who.WAbil.MP / 2);
                                this.SysMsg("情侣关系破裂了，将造成自身的冲击。", 0);
                                M2Share.AddUserLog("47\09" + this.MapName + "\09" + this.CX.ToString() + "\09" + this.CY.ToString() + "\09" + this.UserName + "\09" + "0\09" + "0\09" + "2\09" + PoseHuman.UserName);
                                who.UserNameChanged();
                                if (PoseHuman != null)
                                {
                                    if (PoseHuman.RelationShipDeleteOther(Grobal2.RsState_Lover, who.UserName))
                                    {
                                        PoseHuman.MakePoison(Grobal2.POISON_SLOW, 3, 1);
                                        // HP, MP 函版(50%)
                                        PoseHuman.WAbil.HP = (short)_MAX(1, PoseHuman.WAbil.HP / 2);
                                        PoseHuman.WAbil.MP = (short)_MAX(1, PoseHuman.WAbil.MP / 2);
                                        PoseHuman.SysMsg("情侣关系破裂了，将造成自身的冲击。", 0);
                                        M2Share.AddUserLog("47\09" + PoseHuman.MapName + "\09" + PoseHuman.CX.ToString() + "\09" + PoseHuman.CY.ToString() + "\09" + PoseHuman.UserName + "\09" + "0\09" + "0\09" + "2\09" + who.UserName);
                                    }
                                    PoseHuman.UserNameChanged();
                                }
                                else
                                {
                                    if (M2Share.UserEngine.FindOtherServerUser(PoseHuman.UserName, ref svidx))
                                    {
                                        M2Share.UserEngine.SendInterMsg(Grobal2.ISM_LM_DELETE, svidx, PoseHuman.UserName + "/" + this.UserName + "/" + Grobal2.RsState_Lover.ToString());
                                    }
                                }
                            }
                            else
                            {
                                who.SendDefMessage(Grobal2.SM_LM_RESULT, Grobal2.RsState_Lover, Grobal2.RsError_DontDelete, 0, 0, PoseHuman.UserName);
                            }
                            NpcSayTitle(who, "@UnMarryEnd");
                            NpcSayTitle(PoseHuman, "@UnMarryEnd");// 离婚结束语
                        }
                        else
                        {
                            NpcSayTitle(who, "@WateUnMarry");
                        }
                        // 请求离婚后显示的信息
                    }
                    return;
                }
                else
                {
                    // 强行离婚
                    if (pqa.ActTag.ToLower().CompareTo("FORCE".ToLower()) == 0)
                    {
                        PoseHuman = M2Share.UserEngine.GetUserHuman(who.fLover.GetLoverName());
                        if (PoseHuman != null)
                        {
                            if (who.RelationShipDeleteOther(Grobal2.RsState_Lover, PoseHuman.UserName))
                            {
                                this.MakePoison(Grobal2.POISON_SLOW, 3, 1);
                                // HP, MP 函版(50%)
                                who.WAbil.HP = (short)_MAX(1, who.WAbil.HP / 2);
                                who.WAbil.MP = (short)_MAX(1, who.WAbil.MP / 2);
                                this.SysMsg("情侣关系破裂了，将造成自身的冲击。", 0);
                                M2Share.AddUserLog("47\09" + this.MapName + "\09" + this.CX.ToString() + "\09" + this.CY.ToString() + "\09" + this.UserName + "\09" + "0\09" + "0\09" + "2\09" + PoseHuman.UserName);
                                who.UserNameChanged();
                                if (PoseHuman != null)
                                {
                                    if (PoseHuman.RelationShipDeleteOther(Grobal2.RsState_Lover, who.UserName))
                                    {
                                        PoseHuman.MakePoison(Grobal2.POISON_SLOW, 3, 1);
                                        // HP, MP 函版(50%)
                                        PoseHuman.WAbil.HP = (short)_MAX(1, PoseHuman.WAbil.HP / 2);
                                        PoseHuman.WAbil.MP = (short)_MAX(1, PoseHuman.WAbil.MP / 2);
                                        PoseHuman.SysMsg("情侣关系破裂了，将造成自身的冲击。", 0);
                                        M2Share.AddUserLog("47\09" + PoseHuman.MapName + "\09" + PoseHuman.CX.ToString() + "\09" + PoseHuman.CY.ToString() + "\09" + PoseHuman.UserName + "\09" + "0\09" + "0\09" + "2\09" + who.UserName);
                                    }
                                    PoseHuman.UserNameChanged();
                                }
                                else
                                {
                                    if (M2Share.UserEngine.FindOtherServerUser(PoseHuman.UserName, ref svidx))
                                    {
                                        M2Share.UserEngine.SendInterMsg(Grobal2.ISM_LM_DELETE, svidx, PoseHuman.UserName + "/" + who.UserName + "/" + Grobal2.RsState_Lover.ToString());
                                    }
                                }
                            }
                            else
                            {
                                who.SendDefMessage(Grobal2.SM_LM_RESULT, Grobal2.RsState_Lover, Grobal2.RsError_DontDelete, 0, 0, PoseHuman.UserName);
                            }
                        }
                        else
                        {
                            if (who.RelationShipDeleteOther(Grobal2.RsState_Lover, who.fLover.GetLoverName()))
                            {
                                this.MakePoison(Grobal2.POISON_SLOW, 3, 1);
                                // HP, MP 函版(50%)
                                who.WAbil.HP = (short)_MAX(1, who.WAbil.HP / 2);
                                who.WAbil.MP = (short)_MAX(1, who.WAbil.MP / 2);
                                this.SysMsg("情侣关系破裂了，将造成自身的冲击。", 0);
                                M2Share.AddUserLog("47\09" + this.MapName + "\09" + this.CX.ToString() + "\09" + this.CY.ToString() + "\09" + this.UserName + "\09" + "0\09" + "0\09" + "2\09" + who.UserName);
                                who.UserNameChanged();
                            }
                        }
                        NpcSayTitle(who, "@UnMarryEnd");
                    }
                    return;
                }
            }
        }

        public void NpcSayTitle_ActionOfMaster(TUserHuman who, TQuestActionInfo pqa)
        {
            TUserHuman PoseHuman;
            if ((who.m_sMasterName != "") && (who.m_nMasterCount > 4))
            {
                return;
            }
            PoseHuman = (TUserHuman)who.GetFrontCret();
            if (PoseHuman == null)
            {
                NpcSayTitle(who, "@MasterCheckDir");
                return;
            }
            if (pqa.ActParam == "")
            {
                if (PoseHuman.RaceServer != Grobal2.RC_USERHUMAN)
                {
                    NpcSayTitle(who, "@HumanTypeErr");
                    return;
                }
                if (PoseHuman.GetFrontCret() == who)
                {
                    NpcSayTitle(who, "@StartGetMaster");
                    NpcSayTitle(PoseHuman, "@StartMaster");
                    who.m_boStartMaster = true;
                    PoseHuman.m_boStartMaster = true;
                }
                else
                {
                    NpcSayTitle(who, "@MasterDirErr");
                    NpcSayTitle(PoseHuman, "@MasterCheckDir");
                }
                return;
            }
            if (pqa.ActParam.ToLower().CompareTo("REQUESTMASTER".ToLower()) == 0)
            {
                if (who.m_boStartMaster && PoseHuman.m_boStartMaster)
                {
                    who.m_PoseBaseObject = PoseHuman;
                    PoseHuman.m_PoseBaseObject = who;
                    NpcSayTitle(who, "@WateMaster");
                    NpcSayTitle(PoseHuman, "@RevMaster");
                }
                return;
            }
            if (pqa.ActParam.ToLower().CompareTo("RESPONSEMASTER".ToLower()) == 0)
            {
                if (pqa.ActTag.ToLower().CompareTo("OK".ToLower()) == 0)
                {
                    if ((who.m_PoseBaseObject == PoseHuman) && (PoseHuman.m_PoseBaseObject == who))
                    {
                        if (who.m_boStartMaster && PoseHuman.m_boStartMaster)
                        {
                            NpcSayTitle(who, "@EndMaster");
                            NpcSayTitle(PoseHuman, "@EndMaster");
                            who.m_nMasterCount++;// 徒弟数量加1
                            who.m_boStartMaster = false;
                            PoseHuman.m_boStartMaster = false;
                            if (who.m_sMasterName == "")
                            {
                                who.m_sMasterName = PoseHuman.UserName;// 徒弟名字
                                who.m_boMaster = true;// 为师傅
                            }
                            PoseHuman.m_sMasterName = who.UserName;// 师傅名字
                            PoseHuman.m_boMaster = false;// 为徒弟
                            who.SendMsg(who, Grobal2.RM_MA_DBADD, 0, 0, 0, 0, PoseHuman.UserName + "1/" + HUtil32.BoolToIntStr(who.m_boMaster).ToString() + "/" + who.m_nMasterCount.ToString());// 发送的师傅数据
                            PoseHuman.SendMsg(PoseHuman, Grobal2.RM_MA_DBADD, 0, 0, 0, 0, who.UserName + "1/" + HUtil32.BoolToIntStr(PoseHuman.m_boMaster).ToString() + "/" + PoseHuman.m_nMasterRanking.ToString());// 发送的徒弟数据
                            who.SendMsg(who, Grobal2.RM_LM_DBMATLIST, 0, 0, 0, 0, "");
                            PoseHuman.SendDelayMsg(PoseHuman, Grobal2.RM_LM_REFMATLIST, 0, 0, 0, 0, "", 1500);
                        }
                    }
                }
                else
                {
                    if (who.m_boStartMaster && PoseHuman.m_boStartMaster)
                    {
                        NpcSayTitle(who, "@EndMasterFail");
                        NpcSayTitle(PoseHuman, "@EndMasterFail");
                        who.m_boStartMaster = false;
                        PoseHuman.m_boStartMaster = false;
                    }
                }
                return;
            }
        }

        public void NpcSayTitle_ActionOfunMaster(TUserHuman who, TQuestActionInfo pqa)
        {
            TUserHuman PoseHuman;
            int ForceIndex;
            string TempMasterName;
            if ((who.m_sMasterName == "") && !who.m_boMaster)
            {
                NpcSayTitle(who, "@ExeMasterFail");
                return;
            }
            PoseHuman = (TUserHuman)who.GetFrontCret();
            if (PoseHuman == null)
            {
                NpcSayTitle(who, "@UnMasterCheckDir");
            }
            if (PoseHuman != null)
            {
                if (pqa.ActParam == "")
                {
                    if (PoseHuman.RaceServer != Grobal2.RC_USERHUMAN)
                    {
                        NpcSayTitle(who, "@UnMasterTypeErr");
                        return;
                    }
                    if (PoseHuman.GetFrontCret() == who)
                    {
                        if (who.m_sMasterName == PoseHuman.UserName)
                        {
                            if (who.m_boMaster)
                            {
                                NpcSayTitle(who, "@UnIsMaster");
                                return;
                            }
                            if (who.m_sMasterName != PoseHuman.UserName)
                            {
                                NpcSayTitle(who, "@UnMasterError");
                                return;
                            }
                            NpcSayTitle(who, "@StartUnMaster");
                            NpcSayTitle(PoseHuman, "@WateUnMaster");
                            return;
                        }
                    }
                }
            }
            // sREQUESTUNMARRY
            if (pqa.ActParam.ToLower().CompareTo("REQUESTUNMASTER".ToLower()) == 0)
            {
                if (pqa.ActTag == "")
                {
                    if (PoseHuman != null)
                    {
                        who.m_boStartUnMaster = true;
                        if (who.m_boStartUnMaster && PoseHuman.m_boStartUnMaster)
                        {
                            who.m_nMasterCount -= 1;
                            who.SendMsg(who, Grobal2.RM_MA_DBDELETE, 0, 0, 0, 0, PoseHuman.UserName + "1/" + HUtil32.BoolToIntStr(who.m_boMaster).ToString() + "/" + who.m_nMasterCount.ToString());
                            PoseHuman.SendMsg(PoseHuman, Grobal2.RM_MA_DBDELETE, 0, 0, 0, 0, who.UserName + "1/" + HUtil32.BoolToIntStr(PoseHuman.m_boMaster).ToString() + "/" + PoseHuman.m_nMasterRanking.ToString());
                            who.m_sMasterName = "";
                            PoseHuman.m_sMasterName = "";
                            who.m_boStartUnMaster = false;
                            PoseHuman.m_boStartUnMaster = false;
                            who.SendMsg(who, Grobal2.RM_LM_DBMATLIST, 0, 0, 0, 0, "");
                            // who.UserNameChanged;
                            PoseHuman.CheckMaster();
                            PoseHuman.UserNameChanged();
                            NpcSayTitle(who, "@UnMasterEnd");
                            NpcSayTitle(PoseHuman, "@UnMasterEnd");
                        }
                        else
                        {
                            NpcSayTitle(who, "@WateUnMaster");
                            NpcSayTitle(PoseHuman, "@RevUnMaster");
                        }
                    }
                    return;
                }
                else
                {
                    // 强行出师
                    if (pqa.ActTag.ToLower().CompareTo("FORCE".ToLower()) == 0)
                    {
                        ForceIndex = Convert.ToInt32(pqa.ActExtra);
                        if (ForceIndex < 0)
                        {
                            PoseHuman = M2Share.UserEngine.GetUserHuman(who.m_sMasterName);
                            if (PoseHuman != null)
                            {
                                PoseHuman.m_nMasterCount -= 1;
                                who.SendMsg(who, Grobal2.RM_MA_DBDELETE, 0, 0, 0, 0, PoseHuman.UserName + "1/" + HUtil32.BoolToIntStr(who.m_boMaster).ToString() + "/" + who.m_nMasterCount.ToString());
                                PoseHuman.SendMsg(PoseHuman, Grobal2.RM_MA_DBDELETE, 0, 0, 0, 0, who.UserName + "1/" + HUtil32.BoolToIntStr(PoseHuman.m_boMaster).ToString() + "/" + PoseHuman.m_nMasterRanking.ToString());
                                PoseHuman.m_sMasterName = "";
                                who.SendMsg(who, Grobal2.RM_LM_DBMATLIST, 0, 0, 0, 0, "");
                                PoseHuman.CheckMaster();
                                PoseHuman.UserNameChanged();
                            }
                            else
                            {
                                who.SendMsg(who, Grobal2.RM_MA_DBDELETE, 0, 0, 0, 0, who.m_sMasterName + "1/" + HUtil32.BoolToIntStr(who.m_boMaster).ToString() + "/" + 4.ToString());
                            }
                            who.m_sMasterName = "";
                            NpcSayTitle(who, "@UnMasterEnd");
                            who.UserNameChanged();
                        }
                        else
                        {
                            TempMasterName = who.m_MasterRanking[ForceIndex - 1].sMasterName;
                            PoseHuman = M2Share.UserEngine.GetUserHuman(TempMasterName);
                            if (PoseHuman != null)
                            {
                                PoseHuman.m_nMasterCount -= 1;
                                who.SendMsg(who, Grobal2.RM_MA_DBDELETE, 0, 0, 0, 0, PoseHuman.UserName + "1/" + HUtil32.BoolToIntStr(who.m_boMaster).ToString() + "/" + who.m_nMasterCount.ToString());
                                PoseHuman.SendMsg(PoseHuman, Grobal2.RM_MA_DBDELETE, 0, 0, 0, 0, who.UserName + "1/" + HUtil32.BoolToIntStr(PoseHuman.m_boMaster).ToString() + "/" + PoseHuman.m_nMasterRanking.ToString());
                                PoseHuman.m_sMasterName = "";
                                who.SendMsg(who, Grobal2.RM_LM_DBMATLIST, 0, 0, 0, 0, "");
                                PoseHuman.CheckMaster();
                                PoseHuman.UserNameChanged();
                            }
                            else
                            {
                                who.SendMsg(who, Grobal2.RM_MA_DBDELETE, 0, 0, 0, 0, TempMasterName + "1/" + HUtil32.BoolToIntStr(who.m_boMaster).ToString() + "/" + 4.ToString());
                                who.SendMsg(who, Grobal2.RM_LM_DBMATLIST, 0, 0, 0, 0, "");
                            }
                            who.m_sMasterName = "";
                            NpcSayTitle(who, "@UnMasterEnd");
                            who.UserNameChanged();
                        }
                    }
                    return;
                }
            }
        }

        public void NpcSayTitle_ActionOfSetHumIcon(TUserHuman who, TQuestActionInfo pqa)
        {
            int nIndex;
            nIndex = Convert.ToInt32(pqa.ActParam);
            //if (!(nIndex >= Grobal2.TIconInfo.GetLowerBound(0) && nIndex <= Grobal2.TIconInfo.GetUpperBound(0)))
            //{
            //    // ScriptActionError(who, '', pqa, sSC_SETHUMICON);
            //    return;
            //}
            who.m_IconInfo[nIndex].wStart = (short)Convert.ToInt32(pqa.ActTag);
            who.m_IconInfo[nIndex].btFrame = (byte)Convert.ToInt32(pqa.ActExtra);
            who.m_IconInfo[nIndex].wFrameTime = (short)Convert.ToInt32(pqa.ActParam4);
            who.m_IconInfo[nIndex].bo01 = (byte)Convert.ToInt32(pqa.ActParam5);
            who.m_IconInfo[nIndex].nx = Convert.ToInt32(pqa.ActParam6);
            who.m_IconInfo[nIndex].ny = Convert.ToInt32(pqa.ActParam7);
            who.RefIconInfo();
        }

        public void NpcSayTitle_TakeItemFromUser(TCreature who, ref NpcSayParams sayParams, string iname, int count)
        {
            int i;
            TUserItem pu;
            TStdItem ps;
            pu = null;
            ps = null;
            if (iname.ToLower().CompareTo("金币".ToLower()) == 0)
            {
                who.DecGold(count);
                who.GoldChanged();
                sayParams.latesttakeitem = "金币";
                M2Share.AddUserLog("10\09" + who.MapName + "\09" + who.CX.ToString() + "\09" + who.CY.ToString() + "\09" + who.UserName + "\09" + Envir.NAME_OF_GOLD + "\09" + count.ToString() + "\09" + "1\09" + this.UserName);
            }
            else
            {
                for (i = who.ItemList.Count - 1; i >= 0; i--)
                {
                    if (count <= 0)
                    {
                        break;
                    }
                    pu = who.ItemList[i];
                    ps = M2Share.UserEngine.GetStdItem(pu.Index);
                    if (ps != null)
                    {
                        if (ps.Name.ToLower().CompareTo(iname.ToLower()) == 0)
                        {
                            if (ps.OverlapItem >= 1)
                            {
                                M2Share.AddUserLog("10\09" + who.MapName + "\09" + who.CX.ToString() + "\09" + who.CY.ToString() + "\09" + who.UserName + "\09" + pu.Index.ToString() + "\09" + count.ToString() + "\09" + "1\09" + this.UserName);
                                if (pu.Dura >= count)
                                {
                                    pu.Dura = (short)(pu.Dura - count);
                                    if (pu.Dura <= 0)
                                    {
                                        if (who.RaceServer == Grobal2.RC_USERHUMAN)
                                        {
                                            ((TUserHuman)who).SendDelItem(pu);
                                        }
                                        Dispose(pu);
                                        who.ItemList.RemoveAt(i);
                                    }
                                    else
                                    {
                                        who.SendMsg(this, Grobal2.RM_COUNTERITEMCHANGE, 0, pu.MakeIndex, pu.Dura, 0, ps.Name);
                                    }
                                }
                                else
                                {
                                    if (who.RaceServer == Grobal2.RC_USERHUMAN)
                                    {
                                        ((TUserHuman)who).SendDelItem(pu);
                                    }
                                    Dispose(pu);
                                    who.ItemList.RemoveAt(i);
                                }
                                sayParams.latesttakeitem = M2Share.UserEngine.GetStdItemName(pu.Index);
                                break;
                            }
                            else
                            {
                                if (iname == M2Share.GetUnbindItemName(ObjBase.SHAPE_AMULET_BUNCH))
                                {
                                    if (pu.Dura < pu.DuraMax)
                                    {
                                        continue;
                                    }
                                }
                                M2Share.AddUserLog("10\09" + who.MapName + "\09" + who.CX.ToString() + "\09" + who.CY.ToString() + "\09" + who.UserName + "\09" + iname + "\09" + pu.MakeIndex.ToString() + "\09" + "1\09" + this.UserName);
                                ((TUserHuman)who).SendDelItem(pu);
                                sayParams.latesttakeitem = M2Share.UserEngine.GetStdItemName(pu.Index);
                                Dispose(pu);
                                who.ItemList.RemoveAt(i);
                                count -= 1;
                            }
                        }
                    }
                }
            }
        }

        public void NpcSayTitle_TakeEventGradeItemFromUser(TCreature who, ref NpcSayParams sayParams, int grade)
        {
            TUserItem pu = null;
            TStdItem ps = null;
            for (var i = who.ItemList.Count - 1; i >= 0; i--)
            {
                pu = who.ItemList[i];
                if (pu != null)
                {
                    ps = M2Share.UserEngine.GetStdItem(pu.Index);
                    if (ps != null)
                    {
                        if (ps.EffType2 == Grobal2.EFFTYPE2_EVENT_GRADE)
                        {
                            if (ps.EffValue2 <= grade)
                            {
                                M2Share.AddUserLog("10\09" + who.MapName + "\09" + who.CX.ToString() + "\09" + who.CY.ToString() + "\09" + who.UserName + "\09" + ps.Name + "\09" + pu.MakeIndex.ToString() + "\09" + "1\09" + this.UserName);
                                ((TUserHuman)who).SendDelItem(pu);
                                sayParams.latesttakeitem = M2Share.UserEngine.GetStdItemName(pu.Index);
                                Dispose(pu);
                                who.ItemList.RemoveAt(i);
                            }
                        }
                    }
                }
            }
        }

        public void NpcSayTitle_TakeWItemFromUser(TCreature who, ref NpcSayParams sayParams, string iname, int count = 0)
        {
            if (HUtil32.CompareLStr(iname, "[NECKLACE]", 4))
            {
                if (who.UseItems[Grobal2.U_NECKLACE].Index > 0)
                {
                    ((TUserHuman)who).SendDelItem(who.UseItems[Grobal2.U_NECKLACE]);
                    sayParams.latesttakeitem = M2Share.UserEngine.GetStdItemName(who.UseItems[Grobal2.U_NECKLACE].Index);
                    who.UseItems[Grobal2.U_NECKLACE].Index = 0;
                }
                return;
            }
            if (HUtil32.CompareLStr(iname, "[RING]", 4))
            {
                if (who.UseItems[Grobal2.U_RINGL].Index > 0)
                {
                    ((TUserHuman)who).SendDelItem(who.UseItems[Grobal2.U_RINGL]);
                    sayParams.latesttakeitem = M2Share.UserEngine.GetStdItemName(who.UseItems[Grobal2.U_RINGL].Index);
                    who.UseItems[Grobal2.U_RINGL].Index = 0;
                    return;
                }
                if (who.UseItems[Grobal2.U_RINGR].Index > 0)
                {
                    ((TUserHuman)who).SendDelItem(who.UseItems[Grobal2.U_RINGR]);
                    sayParams.latesttakeitem = M2Share.UserEngine.GetStdItemName(who.UseItems[Grobal2.U_RINGR].Index);
                    who.UseItems[Grobal2.U_RINGR].Index = 0;
                    return;
                }
                return;
            }
            if (HUtil32.CompareLStr(iname, "[ARMRING]", 4))
            {
                if (who.UseItems[Grobal2.U_ARMRINGL].Index > 0)
                {
                    ((TUserHuman)who).SendDelItem(who.UseItems[Grobal2.U_ARMRINGL]);
                    sayParams.latesttakeitem = M2Share.UserEngine.GetStdItemName(who.UseItems[Grobal2.U_ARMRINGL].Index);
                    who.UseItems[Grobal2.U_ARMRINGL].Index = 0;
                    return;
                }
                if (who.UseItems[Grobal2.U_ARMRINGR].Index > 0)
                {
                    ((TUserHuman)who).SendDelItem(who.UseItems[Grobal2.U_ARMRINGR]);
                    sayParams.latesttakeitem = M2Share.UserEngine.GetStdItemName(who.UseItems[Grobal2.U_ARMRINGR].Index);
                    who.UseItems[Grobal2.U_ARMRINGR].Index = 0;
                    return;
                }
                return;
            }
            if (HUtil32.CompareLStr(iname, "[WEAPON]", 4))
            {
                if (who.UseItems[Grobal2.U_WEAPON].Index > 0)
                {
                    ((TUserHuman)who).SendDelItem(who.UseItems[Grobal2.U_WEAPON]);
                    sayParams.latesttakeitem = M2Share.UserEngine.GetStdItemName(who.UseItems[Grobal2.U_WEAPON].Index);
                    who.UseItems[Grobal2.U_WEAPON].Index = 0;
                }
                return;
            }
            if (HUtil32.CompareLStr(iname, "[HELMET]", 4))
            {
                if (who.UseItems[Grobal2.U_HELMET].Index > 0)
                {
                    ((TUserHuman)who).SendDelItem(who.UseItems[Grobal2.U_HELMET]);
                    sayParams.latesttakeitem = M2Share.UserEngine.GetStdItemName(who.UseItems[Grobal2.U_HELMET].Index);
                    who.UseItems[Grobal2.U_HELMET].Index = 0;
                }
                return;
            }
            if (HUtil32.CompareLStr(iname, "[BUJUK]", 4))
            {
                if (who.UseItems[Grobal2.U_BUJUK].Index > 0)
                {
                    ((TUserHuman)who).SendDelItem(who.UseItems[Grobal2.U_BUJUK]);
                    sayParams.latesttakeitem = M2Share.UserEngine.GetStdItemName(who.UseItems[Grobal2.U_BUJUK].Index);
                    who.UseItems[Grobal2.U_BUJUK].Index = 0;
                }
                return;
            }
            if (HUtil32.CompareLStr(iname, "[BELT]", 4))
            {
                if (who.UseItems[Grobal2.U_BELT].Index > 0)
                {
                    ((TUserHuman)who).SendDelItem(who.UseItems[Grobal2.U_BELT]);
                    sayParams.latesttakeitem = M2Share.UserEngine.GetStdItemName(who.UseItems[Grobal2.U_BELT].Index);
                    who.UseItems[Grobal2.U_BELT].Index = 0;
                }
                return;
            }
            if (HUtil32.CompareLStr(iname, "[BOOTS]", 4))
            {
                if (who.UseItems[Grobal2.U_BOOTS].Index > 0)
                {
                    ((TUserHuman)who).SendDelItem(who.UseItems[Grobal2.U_BOOTS]);
                    sayParams.latesttakeitem = M2Share.UserEngine.GetStdItemName(who.UseItems[Grobal2.U_BOOTS].Index);
                    who.UseItems[Grobal2.U_BOOTS].Index = 0;
                }
                return;
            }
            if (HUtil32.CompareLStr(iname, "[CHARM]", 4))
            {
                if (who.UseItems[Grobal2.U_CHARM].Index > 0)
                {
                    ((TUserHuman)who).SendDelItem(who.UseItems[Grobal2.U_CHARM]);
                    sayParams.latesttakeitem = M2Share.UserEngine.GetStdItemName(who.UseItems[Grobal2.U_CHARM].Index);
                    who.UseItems[Grobal2.U_CHARM].Index = 0;
                }
                return;
            }
            for (var i = 0; i <= 12; i++)
            {
                if (count <= 0)
                {
                    break;
                }
                if (who.UseItems[i].Index > 0)
                {
                    if (M2Share.UserEngine.GetStdItemName(who.UseItems[i].Index).ToLower().CompareTo(iname.ToLower()) == 0)
                    {
                        ((TUserHuman)who).SendDelItem(who.UseItems[i]);
                        sayParams.latesttakeitem = M2Share.UserEngine.GetStdItemName(who.UseItems[i].Index);
                        who.UseItems[i].Index = 0;
                        count -= 1;
                    }
                }
            }
        }

        public void NpcSayTitle_GiveItemToUser(TCreature receivewho, string iname, int count)
        {
            int idx;
            TStdItem pstd;
            TStdItem pstd2;
            TUserItem pu;
            int wg;
            if (iname.ToLower().CompareTo("金币".ToLower()) == 0)
            {
                receivewho.IncGold(count);
                receivewho.GoldChanged();
                M2Share.AddUserLog("9\09" + receivewho.MapName + "\09" + receivewho.CX.ToString() + "\09" + receivewho.CY.ToString() + "\09" + receivewho.UserName + "\09" + Envir.NAME_OF_GOLD + "\09" + count.ToString() + "\09" + "1\09" + this.UserName);
            }
            else
            {
                idx = 0;
                idx = M2Share.UserEngine.GetStdItemIndex(iname);
                pstd = M2Share.UserEngine.GetStdItem(idx);
                if ((idx > 0) && (pstd != null))
                {
                    for (var i = 0; i < count; i++)
                    {
                        if (pstd.OverlapItem >= 1)
                        {
                            if (receivewho.UserCounterItemAdd(pstd.StdMode, pstd.Looks, count, iname, false))
                            {
                                M2Share.AddUserLog("9\09" + receivewho.MapName + "\09" + receivewho.CX.ToString() + "\09" + receivewho.CY.ToString() + "\09" + receivewho.UserName + "\09" + idx.ToString() + "\09" + count.ToString() + "\09" + "1\09" + this.UserName);
                                receivewho.WeightChanged();
                                return;
                            }
                        }
                        if (pstd.OverlapItem == 1)
                        {
                            wg = receivewho.WAbil.Weight + (count / 10);
                        }
                        else if (pstd.OverlapItem >= 2)
                        {
                            wg = receivewho.WAbil.Weight + (pstd.Weight * count);
                        }
                        else
                        {
                            wg = receivewho.WAbil.Weight + pstd.Weight;
                        }
                        if (receivewho.CanAddItem())
                        {
                            pu = new TUserItem();
                            if (M2Share.UserEngine.CopyToUserItemFromName(iname, ref pu))
                            {
                                if (pstd.OverlapItem >= 1)
                                {
                                    pu.Dura = (short)count;
                                }
                                receivewho.ItemList.Add(pu);
                                ((TUserHuman)receivewho).SendAddItem(pu);
                                M2Share.AddUserLog("9\09" + receivewho.MapName + "\09" + receivewho.CX.ToString() + "\09" + receivewho.CY.ToString() + "\09" + receivewho.UserName + "\09" + iname + "\09" + pu.MakeIndex.ToString() + "\09" + "1\09" + this.UserName);
                                receivewho.WeightChanged();
                                if (pstd.OverlapItem >= 1)
                                {
                                    break;
                                }
                            }
                            else
                            {
                                Dispose(pu);
                            }
                        }
                        else
                        {
                            pu = new TUserItem();
                            if (M2Share.UserEngine.CopyToUserItemFromName(iname, ref pu))
                            {
                                pstd2 = M2Share.UserEngine.GetStdItem(M2Share.UserEngine.GetStdItemIndex(iname));
                                if (pstd2 != null)
                                {
                                    if (pstd2.OverlapItem >= 1)
                                    {
                                        pu.Dura = (short)count;
                                    }
                                    M2Share.AddUserLog("9\09" + receivewho.MapName + "\09" + receivewho.CX.ToString() + "\09" + receivewho.CY.ToString() + "\09" + receivewho.UserName + "\09" + iname + "\09" + pu.MakeIndex.ToString() + "\09" + "1\09" + this.UserName);
                                    receivewho.DropItemDown(pu, 3, false, receivewho, null, 2);
                                }
                            }
                            if (pu != null)
                            {
                                Dispose(pu);
                            }
                        }
                    }
                }
            }
        }

        public bool NpcSayTitle_DoActionList(TCreature who, ref NpcSayParams sayParams, ArrayList alist)
        {
            bool result;
            int k;
            int n;
            int n1;
            int n2;
            int param;
            int tag;
            int iparam1;
            int iparam2;
            int iparam3;
            int iparam4;
            string sparam1 = string.Empty;
            string sparam2 = string.Empty;
            string sparam3 = string.Empty;
            string sparam4 = string.Empty;
            TQuestActionInfo pqa;
            ArrayList list;
            TUserHuman hum;
            TEnvirnoment envir;
            iparam2 = 0;
            iparam3 = 0;
            result = true;
            for (var i = 0; i < alist.Count; i++)
            {
                pqa = alist[i] as TQuestActionInfo;
                switch (pqa.ActIdent)
                {
                    case Grobal2.QA_SET:
                        param = HUtil32.Str_ToInt(pqa.ActParam, 0);
                        tag = HUtil32.Str_ToInt(pqa.ActTag, 0);
                        who.SetQuestMark(param, tag);
                        break;
                    case Grobal2.QA_OPENUNIT:
                        param = HUtil32.Str_ToInt(pqa.ActParam, 0);
                        tag = HUtil32.Str_ToInt(pqa.ActTag, 0);
                        who.SetQuestOpenIndexMark(param, tag);
                        break;
                    case Grobal2.QA_SETUNIT:
                        param = HUtil32.Str_ToInt(pqa.ActParam, 0);
                        tag = HUtil32.Str_ToInt(pqa.ActTag, 0);
                        who.SetQuestFinIndexMark(param, tag);
                        break;
                    case Grobal2.QA_TAKE:
                        // NpcSayTitle_TakeItemFromUser(pqa.ActParam, pqa.ActTagVal);
                        break;
                    case Grobal2.QA_TAKEW:
                        //  NpcSayTitle_TakeWItemFromUser(pqa.ActParam, pqa.ActTagVal);
                        break;
                    case Grobal2.QA_GIVE:
                        NpcSayTitle_GiveItemToUser(who, pqa.ActParam, pqa.ActTagVal);
                        break;
                    case Grobal2.QA_CLOSE:
                        who.SendMsg(this, Grobal2.RM_MERCHANTDLGCLOSE, 0, this.ActorId, 0, 0, "");
                        break;
                    case Grobal2.QA_CLOSENOINVEN:
                        who.SendMsg(this, Grobal2.RM_MERCHANTDLGCLOSE, 0, this.ActorId, 1, 0, "");
                        break;
                    case Grobal2.QA_RESET:
                        for (k = 0; k < pqa.ActTagVal; k++)
                        {
                            who.SetQuestMark(pqa.ActParamVal + k, 0);
                        }
                        break;
                    case Grobal2.QA_RESETUNIT:
                        break;
                    case Grobal2.QA_MAPMOVE:
                        who.SendRefMsg(Grobal2.RM_SPACEMOVE_HIDE, 0, 0, 0, 0, "");
                        who.SpaceMove(pqa.ActParam, (short)pqa.ActTagVal, (short)pqa.ActExtraVal, 0);
                        sayParams.bosaynow = true;
                        break;
                    case Grobal2.QA_MAPRANDOM:
                        who.SendRefMsg(Grobal2.RM_SPACEMOVE_HIDE, 0, 0, 0, 0, "");
                        who.RandomSpaceMove(pqa.ActParam, 0);
                        sayParams.bosaynow = true;
                        break;
                    case Grobal2.QA_BREAK:
                        result = false;
                        break;
                    case Grobal2.QA_TIMERECALL:
                        ((TUserHuman)who).BoTimeRecall = true;
                        ((TUserHuman)who).TimeRecallMap = ((TUserHuman)who).MapName;
                        ((TUserHuman)who).TimeRecallX = ((TUserHuman)who).CX;
                        ((TUserHuman)who).TimeRecallY = ((TUserHuman)who).CY;
                        ((TUserHuman)who).TimeRecallEnd = GetTickCount + pqa.ActParamVal * 60 * 1000;
                        break;
                    case Grobal2.QA_TIMERECALLGROUP:
                        for (k = 0; k < who.GroupMembers.Count; k++)
                        {
                            hum = M2Share.UserEngine.GetUserHuman(who.GroupMembers[k].UserName);
                            if (hum != null)
                            {
                                hum.BoTimeRecall = false;
                                hum.BoTimeRecallGroup = true;
                                hum.TimeRecallMap = hum.MapName;
                                hum.TimeRecallX = hum.CX;
                                hum.TimeRecallY = hum.CY;
                                hum.TimeRecallEnd = GetTickCount + pqa.ActParamVal * 60 * 1000;
                            }
                        }
                        break;
                    case Grobal2.QA_BREAKTIMERECALL:
                        ((TUserHuman)who).BoTimeRecall = false;
                        ((TUserHuman)who).BoTimeRecallGroup = false;
                        break;
                    case Grobal2.QA_PARAM1:
                        iparam1 = pqa.ActParamVal;
                        sparam1 = pqa.ActParam;
                        break;
                    case Grobal2.QA_PARAM2:
                        iparam2 = pqa.ActParamVal;
                        sparam2 = pqa.ActParam;
                        break;
                    case Grobal2.QA_PARAM3:
                        iparam3 = pqa.ActParamVal;
                        sparam3 = pqa.ActParam;
                        break;
                    case Grobal2.QA_PARAM4:
                        iparam4 = pqa.ActParamVal;
                        sparam4 = pqa.ActParam;
                        break;
                    case Grobal2.QA_TAKECHECKITEM:
                        if (sayParams.pcheckitem != null)
                        {
                            who.DeletePItemAndSend(sayParams.pcheckitem);
                        }
                        break;
                    case Grobal2.QA_MONGEN:
                        for (k = 0; k < pqa.ActTagVal; k++)
                        {
                            var ixx = (short)(iparam2 - pqa.ActExtraVal + new System.Random(pqa.ActExtraVal * 2 + 1).Next());
                            var iyy = (short)(iparam3 - pqa.ActExtraVal + new System.Random(pqa.ActExtraVal * 2 + 1).Next());
                            M2Share.UserEngine.AddCreatureSysop(sparam1, ixx, iyy, pqa.ActParam);
                        }
                        break;
                    case Grobal2.QA_MONCLEAR:
                        list = new ArrayList();
                        M2Share.UserEngine.GetMapMons(M2Share.GrobalEnvir.GetEnvir(pqa.ActParam), list);
                        for (k = 0; k < list.Count; k++)
                        {
                            ((TCreature)list[k]).BoNoItem = true;
                            ((TCreature)list[k]).WAbil.HP = 0;
                        }
                        list.Free();
                        break;
                    case Grobal2.QA_MOV:
                        n = ObjNpc.GetPP(pqa.ActParam);
                        if (n >= 0)
                        {
                            switch (n)
                            {
                                // Modify the A .. B: 0 .. 9
                                case 0:
                                    ((TUserHuman)who).QuestParams[n] = pqa.ActTagVal;
                                    break;
                                // Modify the A .. B: 100 .. 109
                                case 100:
                                    M2Share.GrobalQuestParams[n - 100] = pqa.ActTagVal;
                                    break;
                                // Modify the A .. B: 200 .. 209
                                case 200:
                                    ((TUserHuman)who).DiceParams[n - 200] = pqa.ActTagVal;
                                    break;
                                // Modify the A .. B: 300 .. 309
                                case 300:
                                    this.PEnvir.MapQuestParams[n - 300] = pqa.ActTagVal;
                                    break;
                            }
                        }
                        break;
                    case Grobal2.QA_INC:
                        n = ObjNpc.GetPP(pqa.ActParam);
                        if (n >= 0)
                        {
                            switch (n)
                            {
                                // Modify the A .. B: 0 .. 9
                                case 0:
                                    if (pqa.ActTagVal > 1)
                                    {
                                        ((TUserHuman)who).QuestParams[n] = ((TUserHuman)who).QuestParams[n] + pqa.ActTagVal;
                                    }
                                    else
                                    {
                                        ((TUserHuman)who).QuestParams[n] = ((TUserHuman)who).QuestParams[n] + 1;
                                    }
                                    break;
                                // Modify the A .. B: 100 .. 109
                                case 100:
                                    if (pqa.ActTagVal > 1)
                                    {
                                        M2Share.GrobalQuestParams[n - 100] = M2Share.GrobalQuestParams[n - 100] + pqa.ActTagVal;
                                    }
                                    else
                                    {
                                        M2Share.GrobalQuestParams[n - 100] = M2Share.GrobalQuestParams[n - 100] + 1;
                                    }
                                    break;
                                // Modify the A .. B: 200 .. 209
                                case 200:
                                    if (pqa.ActTagVal > 1)
                                    {
                                        ((TUserHuman)who).DiceParams[n - 200] = ((TUserHuman)who).DiceParams[n - 200] + pqa.ActTagVal;
                                    }
                                    else
                                    {
                                        ((TUserHuman)who).DiceParams[n - 200] = ((TUserHuman)who).DiceParams[n - 200] + 1;
                                    }
                                    break;
                                // Modify the A .. B: 300 .. 309
                                case 300:
                                    if (pqa.ActTagVal > 1)
                                    {
                                        this.PEnvir.MapQuestParams[n - 300] = this.PEnvir.MapQuestParams[n - 300] + pqa.ActTagVal;
                                    }
                                    else
                                    {
                                        this.PEnvir.MapQuestParams[n - 300] = this.PEnvir.MapQuestParams[n - 300] + 1;
                                    }
                                    break;
                            }
                        }
                        break;
                    case Grobal2.QA_DEC:
                        n = ObjNpc.GetPP(pqa.ActParam);
                        if (n >= 0)
                        {
                            switch (n)
                            {
                                // Modify the A .. B: 0 .. 9
                                case 0:
                                    if (pqa.ActTagVal > 1)
                                    {
                                        ((TUserHuman)who).QuestParams[n] = ((TUserHuman)who).QuestParams[n] - pqa.ActTagVal;
                                    }
                                    else
                                    {
                                        ((TUserHuman)who).QuestParams[n] = ((TUserHuman)who).QuestParams[n] - 1;
                                    }
                                    break;
                                // Modify the A .. B: 100 .. 109
                                case 100:
                                    if (pqa.ActTagVal > 1)
                                    {
                                        M2Share.GrobalQuestParams[n - 100] = M2Share.GrobalQuestParams[n - 100] - pqa.ActTagVal;
                                    }
                                    else
                                    {
                                        M2Share.GrobalQuestParams[n - 100] = M2Share.GrobalQuestParams[n - 100] - 1;
                                    }
                                    break;
                                // Modify the A .. B: 200 .. 209
                                case 200:
                                    if (pqa.ActTagVal > 1)
                                    {
                                        ((TUserHuman)who).DiceParams[n - 200] = ((TUserHuman)who).DiceParams[n - 200] - pqa.ActTagVal;
                                    }
                                    else
                                    {
                                        ((TUserHuman)who).DiceParams[n - 200] = ((TUserHuman)who).DiceParams[n - 200] - 1;
                                    }
                                    break;
                                // Modify the A .. B: 300 .. 309
                                case 300:
                                    if (pqa.ActTagVal > 1)
                                    {
                                        this.PEnvir.MapQuestParams[n - 300] = this.PEnvir.MapQuestParams[n - 300] - pqa.ActTagVal;
                                    }
                                    else
                                    {
                                        this.PEnvir.MapQuestParams[n - 300] = this.PEnvir.MapQuestParams[n - 300] - 1;
                                    }
                                    break;
                            }
                        }
                        break;
                    case Grobal2.QA_SUM:
                        n1 = 0;
                        n = ObjNpc.GetPP(pqa.ActParam);
                        if (n >= 0)
                        {
                            switch (n)
                            {
                                // Modify the A .. B: 0 .. 9
                                case 0:
                                    n1 = ((TUserHuman)who).QuestParams[n];
                                    break;
                                // Modify the A .. B: 100 .. 109
                                case 100:
                                    n1 = M2Share.GrobalQuestParams[n - 100];
                                    break;
                                // Modify the A .. B: 200 .. 209
                                case 200:
                                    n1 = ((TUserHuman)who).DiceParams[n - 200];
                                    break;
                                // Modify the A .. B: 300 .. 309
                                case 300:
                                    n1 = this.PEnvir.MapQuestParams[n - 300];
                                    break;
                            }
                        }
                        n2 = 0;
                        n = ObjNpc.GetPP(pqa.ActTag);
                        if (n >= 0)
                        {
                            switch (n)
                            {
                                // Modify the A .. B: 0 .. 9
                                case 0:
                                    n2 = ((TUserHuman)who).QuestParams[n];
                                    break;
                                // Modify the A .. B: 100 .. 109
                                case 100:
                                    n2 = M2Share.GrobalQuestParams[n - 100];
                                    break;
                                // Modify the A .. B: 200 .. 209
                                case 200:
                                    n2 = ((TUserHuman)who).DiceParams[n - 200];
                                    break;
                                // Modify the A .. B: 300 .. 309
                                case 300:
                                    n2 = this.PEnvir.MapQuestParams[n - 300];
                                    break;
                            }
                        }
                        n = ObjNpc.GetPP(pqa.ActParam);
                        if (n >= 0)
                        {
                            switch (n)
                            {
                                // Modify the A .. B: 0 .. 9
                                case 0:
                                    ((TUserHuman)who).QuestParams[9] = ((TUserHuman)who).QuestParams[9] + n1 + n2;
                                    break;
                                // Modify the A .. B: 100 .. 109
                                case 100:
                                    M2Share.GrobalQuestParams[9] = M2Share.GrobalQuestParams[9] + n1 + n2;
                                    break;
                                // Modify the A .. B: 200 .. 209
                                case 200:
                                    ((TUserHuman)who).DiceParams[9] = ((TUserHuman)who).DiceParams[9] + n1 + n2;
                                    break;
                                // Modify the A .. B: 300 .. 309
                                case 300:
                                    this.PEnvir.MapQuestParams[9] = this.PEnvir.MapQuestParams[9] + n1 + n2;
                                    break;
                            }
                        }
                        break;
                    case Grobal2.QA_MOVRANDOM:
                        // MOVR
                        n = ObjNpc.GetPP(pqa.ActParam);
                        if (n >= 0)
                        {
                            switch (n)
                            {
                                // Modify the A .. B: 0 .. 9
                                case 0:
                                    ((TUserHuman)who).QuestParams[n] = new System.Random(pqa.ActTagVal).Next();
                                    break;
                                // Modify the A .. B: 100 .. 109
                                case 100:
                                    M2Share.GrobalQuestParams[n - 100] = new System.Random(pqa.ActTagVal).Next();
                                    break;
                                // Modify the A .. B: 200 .. 209
                                case 200:
                                    ((TUserHuman)who).DiceParams[n - 200] = new System.Random(pqa.ActTagVal).Next();
                                    break;
                                // Modify the A .. B: 300 .. 309
                                case 300:
                                    this.PEnvir.MapQuestParams[n - 300] = new System.Random(pqa.ActTagVal).Next();
                                    break;
                            }
                        }
                        break;
                    case Grobal2.QA_EXCHANGEMAP:
                        // 何甫 荤恩
                        envir = M2Share.GrobalEnvir.GetEnvir(pqa.ActParam);
                        if (envir != null)
                        {
                            list = new ArrayList();
                            M2Share.UserEngine.GetAreaUsers(envir, 0, 0, 1000, list);
                            if (list.Count > 0)
                            {
                                // 茄疙父 急琶
                                hum = (TUserHuman)list[0];
                                if (hum != null)
                                {
                                    hum.RandomSpaceMove(this.MapName, 0);
                                }
                            }
                            list.Free();
                        }
                        // 唱档 捞悼
                        who.RandomSpaceMove(pqa.ActParam, 0);
                        break;
                    case Grobal2.QA_RECALLMAP:
                        // 何甫 荤恩
                        envir = M2Share.GrobalEnvir.GetEnvir(pqa.ActParam);
                        if (envir != null)
                        {
                            list = new ArrayList();
                            M2Share.UserEngine.GetAreaUsers(envir, 0, 0, 1000, list);
                            for (k = 0; k < list.Count; k++)
                            {
                                hum = (TUserHuman)list[k];
                                if (hum != null)
                                {
                                    hum.RandomSpaceMove(this.MapName, 0);
                                }
                                if (k > 20)
                                {
                                    break;
                                }
                            }
                            list.Free();
                        }
                        break;
                    case Grobal2.QA_BATCHDELAY:
                        sayParams.batchdelay = pqa.ActParamVal * 1000;
                        break;
                    case Grobal2.QA_ADDBATCH:
                        //sayParams.batchlist.Add(pqa.ActParam, sayParams.batchdelay);
                        break;
                    case Grobal2.QA_BATCHMOVE:
                        //for (k = 0; k < sayParams.batchlist.Count; k++)
                        //{
                        //    who.SendDelayMsg(this, Grobal2.RM_RANDOMSPACEMOVE, 0, 0, 0, 0, sayParams.batchlist[k], previousbatchdelay + ((int)sayParams.batchlist.Values[k]));
                        //    previousbatchdelay = previousbatchdelay + ((int)sayParams.batchlist.Values[k]);
                        //}
                        break;
                    case Grobal2.QA_PLAYDICE:
                        who.SendMsg(this, Grobal2.RM_PLAYDICE, (short)pqa.ActParamVal, HUtil32.MakeLong(MakeWord(((TUserHuman)who).DiceParams[0], ((TUserHuman)who).DiceParams[1]), MakeWord(((TUserHuman)who).DiceParams[2], ((TUserHuman)who).DiceParams[3])), HUtil32.MakeLong(MakeWord(((TUserHuman)who).DiceParams[4], ((TUserHuman)who).DiceParams[5]), MakeWord(((TUserHuman)who).DiceParams[6], ((TUserHuman)who).DiceParams[7])), HUtil32.MakeLong(MakeWord(((TUserHuman)who).DiceParams[8], ((TUserHuman)who).DiceParams[9]), 0), pqa.ActTag);
                        sayParams.bosaynow = true;
                        break;
                    case Grobal2.QA_PLAYROCK:
                        who.SendMsg(this, Grobal2.RM_PLAYROCK, (short)pqa.ActParamVal, HUtil32.MakeLong(MakeWord(((TUserHuman)who).DiceParams[0], ((TUserHuman)who).DiceParams[1]), MakeWord(((TUserHuman)who).DiceParams[2], ((TUserHuman)who).DiceParams[3])), HUtil32.MakeLong(MakeWord(((TUserHuman)who).DiceParams[4], ((TUserHuman)who).DiceParams[5]), MakeWord(((TUserHuman)who).DiceParams[6], ((TUserHuman)who).DiceParams[7])), HUtil32.MakeLong(MakeWord(((TUserHuman)who).DiceParams[8], ((TUserHuman)who).DiceParams[9]), 0), pqa.ActTag);
                        sayParams.bosaynow = true;
                        break;
                    case Grobal2.QA_ADDNAMELIST:
                        NpcSayTitle_AddNameFromFileList(who.UserName, NpcBaseDir + pqa.ActParam);
                        break;
                    case Grobal2.QA_DELETENAMELIST:
                        NpcSayTitle_DeleteNameFromFileList(who.UserName, NpcBaseDir + pqa.ActParam);
                        break;
                    case Grobal2.QA_RANDOMSETDAILYQUEST:
                        who.SetDailyQuest(pqa.ActParamVal + new System.Random(pqa.ActTagVal - pqa.ActParamVal + 1).Next());
                        break;
                    case Grobal2.QA_SETDAILYQUEST:
                        who.SetDailyQuest(pqa.ActParamVal);
                        break;
                    case Grobal2.QA_TAKEGRADEITEM:
                        // sayParams.NpcSayTitle_TakeEventGradeItemFromUser(pqa.ActParamVal);
                        break;
                    case Grobal2.QA_GOTOQUEST:
                        // sayParams.NpcSayTitle_GotoQuest(pqa.ActParamVal);
                        break;
                    case Grobal2.QA_ENDQUEST:
                        ((TUserHuman)who).CurQuest = null;
                        break;
                    case Grobal2.QA_GOTO:
                        NpcSayTitle_GotoSay(who, pqa.ActParam);
                        break;
                    case Grobal2.QA_SOUND:
                        who.SendMsg(this, Grobal2.RM_SOUND, 0, HUtil32.Str_ToInt(pqa.ActParam, 0), 0, 0, "");
                        break;
                    case Grobal2.QA_SOUNDALL:
                        if (HUtil32.GetTickCount() - SoundStartTime > 25 * 1000)
                        {
                            SoundStartTime = HUtil32.GetTickCount();
                            BoSoundPlaying = false;
                        }
                        if (!BoSoundPlaying)
                        {
                            BoSoundPlaying = true;
                            this.SendRefMsg(Grobal2.RM_DIGUP, this.Dir, this.CX, this.CY, 0, "");
                        }
                        break;
                    case Grobal2.QA_CHANGEGENDER:
                        ((TUserHuman)who).CmdChangeSex();
                        if (who.Sex == 1)
                        {
                            who.BoxMsg("从男人变性到女人；\\你需要返回人物选择界面重新连接才能看到你的新面孔。", 1);
                            who.SysMsg("从男人变性到女人；\\你需要返回人物选择界面重新连接才能看到你的新面孔。", 1);
                        }
                        else
                        {
                            who.BoxMsg("从女人变性到男人；\\你需要返回人物选择界面重新连接才能看到你的新面孔。", 1);
                            who.SysMsg("从女人变性到男人；\\你需要返回人物选择界面重新连接才能看到你的新面孔。", 1);
                        }
                        break;
                    case Grobal2.QA_KICK:
                        ((TUserHuman)who).EmergencyClose = true;
                        break;
                    case Grobal2.QA_MOVEALLMAP:
                        param = pqa.ActTagVal;
                        // 捞悼 矫懦 荤恩
                        envir = M2Share.GrobalEnvir.GetEnvir(this.MapName);
                        if (envir != null)
                        {
                            list = new ArrayList();
                            M2Share.UserEngine.GetAreaUsers(envir, 0, 0, 1000, list);
                            for (k = 0; k < list.Count; k++)
                            {
                                hum = (TUserHuman)list[k];
                                if (hum != null)
                                {
                                    hum.RandomSpaceMove(pqa.ActParam, 0);
                                }
                                if (k >= (param - 1))
                                {
                                    break;
                                }
                            }
                            list.Free();
                        }
                        break;
                    case Grobal2.QA_MOVEALLMAPGROUP:
                        if (who.GroupOwner == null)
                        {
                            if ((pqa.ActTag == "") && (pqa.ActExtra == ""))
                            {
                                who.RandomSpaceMove(pqa.ActParam, 0);
                            }
                            else
                            {
                                who.SpaceMove(pqa.ActParam, (short)pqa.ActTagVal, (short)pqa.ActExtraVal, 0);
                            }
                        }
                        else if (who.GroupOwner == who)
                        {
                            for (k = 0; k < who.GroupMembers.Count; k++)
                            {
                                hum = M2Share.UserEngine.GetUserHuman(who.GroupMembers[k].UserName);
                                if (hum != null)
                                {
                                    if (hum.MapName == this.MapName)
                                    {
                                        if ((pqa.ActTag == "") && (pqa.ActExtra == ""))
                                        {
                                            hum.RandomSpaceMove(pqa.ActParam, 0);
                                        }
                                        else
                                        {
                                            hum.SpaceMove(pqa.ActParam, (short)pqa.ActTagVal, (short)pqa.ActExtraVal, 0);
                                        }
                                    }
                                }
                            }
                        }
                        break;
                    case Grobal2.QA_RECALLMAPGROUP:
                        n = (int)((HUtil32.GetTickCount() - who.CGHIstart) / 1000);
                        who.CGHIstart = who.CGHIstart + ((long)n * 1000);
                        if (who.CGHIUseTime > n)
                        {
                            who.CGHIUseTime = (short)(who.CGHIUseTime - n);
                        }
                        else
                        {
                            who.CGHIUseTime = 0;
                        }
                        if (who.CGHIUseTime == 0)
                        {
                            if (who.GroupOwner == who)
                            {
                                for (k = 1; k < who.GroupMembers.Count; k++)
                                {
                                    ((TUserHuman)who).CmdRecallMan(who.GroupMembers[k].UserName, pqa.ActParam);
                                }
                                who.CGHIstart = HUtil32.GetTickCount();
                                who.CGHIUseTime = 10;
                            }
                        }
                        else
                        {
                            who.SysMsg(who.CGHIUseTime.ToString() + "秒后再使用。", 0);
                        }
                        break;
                    case Grobal2.QA_WEAPONUPGRADE:
                        if ((pqa.ActParam == "DC") || (pqa.ActParam == "攻击"))
                        {
                            ((TUserHuman)who).CmdRefineWeapon(pqa.ActTagVal, 0, 0, 0);
                        }
                        else if ((pqa.ActParam == "MC") || (pqa.ActParam == "魔法"))
                        {
                            ((TUserHuman)who).CmdRefineWeapon(0, pqa.ActTagVal, 0, 0);
                        }
                        else if ((pqa.ActParam == "SC") || (pqa.ActParam == "道术"))
                        {
                            ((TUserHuman)who).CmdRefineWeapon(0, 0, pqa.ActTagVal, 0);
                        }
                        else if ((pqa.ActParam == "ACC") || (pqa.ActParam == "准确"))
                        {
                            ((TUserHuman)who).CmdRefineWeapon(0, 0, 0, pqa.ActTagVal);
                        }
                        break;
                    case Grobal2.QA_SETALLINMAP:
                        param = HUtil32.Str_ToInt(pqa.ActParam, 0);
                        tag = HUtil32.Str_ToInt(pqa.ActTag, 0);
                        // 甘俊 乐绰 葛电 荤恩
                        envir = M2Share.GrobalEnvir.GetEnvir(this.MapName);
                        if (envir != null)
                        {
                            list = new ArrayList();
                            M2Share.UserEngine.GetAreaUsers(envir, 0, 0, 1000, list);
                            for (k = 0; k < list.Count; k++)
                            {
                                hum = (TUserHuman)list[k];
                                if (hum != null)
                                {
                                    hum.SetQuestMark(param, tag);
                                }
                            }
                            list.Free();
                        }
                        break;
                    case Grobal2.QA_INCPKPOINT:
                        param = HUtil32.Str_ToInt(pqa.ActParam, 0);
                        ((TUserHuman)who).IncPKPoint(param);
                        break;
                    case Grobal2.QA_DECPKPOINT:
                        param = HUtil32.Str_ToInt(pqa.ActParam, 0);
                        ((TUserHuman)who).DecPKPoint(param);
                        break;
                    case Grobal2.QA_MOVETOLOVER:
                        // 楷牢 菊栏肺 捞悼
                        if (((TUserHuman)who).fLover != null)
                        {
                            if (((TUserHuman)who).fLover.GetLoverName() != "")
                            {
                                ((TUserHuman)who).CmdCharSpaceMove(((TUserHuman)who).fLover.GetLoverName());
                            }
                        }
                        break;
                    case Grobal2.QA_BREAKLOVER:
                        // 楷牢 包拌 老规 秦力
                        ((TUserHuman)who).CmdBreakLoverRelation();
                        break;
                    case Grobal2.QA_DECDONATION:
                        // 厘盔扁何陛
                        ((TUserHuman)who).DecGuildAgitDonation(pqa.ActParamVal);
                        break;
                    case Grobal2.QA_SHOWEFFECT:
                        // 厘盔捞棋飘
                        if (pqa.ActTagVal > 0)
                        {
                            tag = pqa.ActTagVal * 1000;
                        }
                        else
                        {
                            tag = 60000;
                        }
                        switch (pqa.ActParamVal)
                        {
                            case 1:
                                // 捞棋飘 辆幅
                                who.SendRefMsg(Grobal2.RM_LOOPNORMALEFFECT, (short)this.ActorId, tag, 0, Grobal2.NE_JW_EFFECT1, "");
                                break;
                            default:
                                who.SendRefMsg(Grobal2.RM_LOOPNORMALEFFECT, (short)this.ActorId, tag, 0, Grobal2.NE_JW_EFFECT1, "");
                                break;
                        }
                        break;
                    case Grobal2.QA_MONGENAROUND:
                        for (var ixx = who.CX - 2; ixx <= who.CX + 2; ixx++)
                        {
                            for (var iyy = who.CY - 2; iyy <= who.CY + 2; iyy++)
                            {
                                if (sparam1 == "")
                                {
                                    sparam1 = who.MapName;
                                }
                                if (((Math.Abs(who.CX - ixx) == 2) || (Math.Abs(who.CY - iyy) == 2)) && (Math.Abs(who.CX - ixx) % 2 == 0) && (Math.Abs(who.CY - iyy) % 2 == 0))
                                {
                                    if (who.PEnvir.CanWalk(ixx, iyy, false))
                                    {
                                        M2Share.UserEngine.AddCreatureSysop(sparam1, (short)ixx, (short)iyy, pqa.ActParam);
                                    }
                                }
                            }
                        }
                        break;
                    case Grobal2.QA_RECALLMOB:
                        ((TUserHuman)who).CmdCallMakeSlaveMonster(pqa.ActParam, pqa.ActTag, 3, 0);
                        break;
                    case Grobal2.QA_SETLOVERFLAG:
                        if (((TUserHuman)who).fLover != null)
                        {
                            hum = M2Share.UserEngine.GetUserHuman(((TUserHuman)who).fLover.GetLoverName());
                            if (hum != null)
                            {
                                param = HUtil32.Str_ToInt(pqa.ActParam, 0);
                                tag = HUtil32.Str_ToInt(pqa.ActTag, 0);
                                hum.SetQuestMark(param, tag);
                            }
                        }
                        break;
                    case Grobal2.QA_GUILDSECESSION:
                        if (who.RaceServer == Grobal2.RC_USERHUMAN)
                        {
                            ((TUserHuman)who).GuildSecession();
                        }
                        break;
                    case Grobal2.QA_GIVETOLOVER:
                        if (((TUserHuman)who).fLover != null)
                        {
                            hum = M2Share.UserEngine.GetUserHuman(((TUserHuman)who).fLover.GetLoverName());
                            if (hum != null)
                            {
                                if ((Math.Abs(hum.CX - who.CX) <= 7) && (Math.Abs(hum.CY - who.CY) <= 7))
                                {
                                    NpcSayTitle_GiveItemToUser(hum, pqa.ActParam, pqa.ActTagVal);
                                }
                                else
                                {
                                    who.SysMsg("你的情侣不在这里。", 0);
                                }
                            }
                            else
                            {
                                who.SysMsg("无法找到你的情侣。", 0);
                            }
                        }
                        break;
                    case Grobal2.QA_INCMEMORIALCOUNT:
                        MemorialCount = MemorialCount + pqa.ActParamVal;
                        break;
                    case Grobal2.QA_DECMEMORIALCOUNT:
                        MemorialCount = MemorialCount - pqa.ActParamVal;
                        break;
                    case Grobal2.QA_SAVEMEMORIALCOUNT:
                        WriteMemorialCount();
                        break;
                    case Grobal2.QA_SECONDSCARD:
                        param = HUtil32.Str_ToInt(pqa.ActParam, 0);
                        ((TUserHuman)who).SecondsCard += param * 86400;
                        who.SysMsg(string.Format("[账户信息]当前账户充值剩余时间%d秒", new int[] { ((TUserHuman)who).SecondsCard }), 0);
                        break;
                    case Grobal2.QA_MARRY:
                        NpcSayTitle_ActionOfMarry((TUserHuman)who, pqa);
                        break;
                    case Grobal2.QA_UNMARRY:
                        NpcSayTitle_ActionOfUnMarry((TUserHuman)who, pqa);
                        break;
                    case Grobal2.QA_MASTER:
                        NpcSayTitle_ActionOfMaster((TUserHuman)who, pqa);
                        break;
                    case Grobal2.QA_UNMASTER:
                        NpcSayTitle_ActionOfunMaster((TUserHuman)who, pqa);
                        break;
                    case Grobal2.QA_SETHUMICON:
                        NpcSayTitle_ActionOfSetHumIcon((TUserHuman)who, pqa);
                        break;
                }
            }
            return result;
        }

        public void NpcSayTitle_NpcSayProc(TCreature who, string str, bool fast)
        {
            string tag = string.Empty;
            string rst = string.Empty;
            rst = str;
            for (var k = 0; k <= 100; k++)
            {
                if (HUtil32.CharCount(rst, '>') >= 1)
                {
                    rst = HUtil32.ArrestStringEx(rst, "<", ">", ref tag);
                    CheckNpcSayCommand((TUserHuman)who, ref str, tag);
                }
                else
                {
                    break;
                }
            }
            if (fast)
            {
                who.SendFastMsg(this, Grobal2.RM_MERCHANTSAY, 0, 0, 0, 0, this.UserName + "/" + str);
            }
            else
            {
                who.SendMsg(this, Grobal2.RM_MERCHANTSAY, 0, 0, 0, 0, this.UserName + "/" + str);
            }
        }

        public class NpcSayParams
        {
            public string latesttakeitem;
            public TUserItem pcheckitem;
            public ArrayList batchlist;
            public bool bosaynow;
            public int batchdelay;
        }

        public void NpcSayTitle(TCreature who, string title)
        {
            int i;
            int j;
            int m;
            string str;
            TSayingRecord psay;
            TQuestRecord pquest = null;
            TSayingProcedure psayproc = null;
            ArrayList batchlist = new ArrayList();
            if (((TUserHuman)who).CurQuestNpc != this)
            {
                ((TUserHuman)who).CurQuestNpc = null;
                ((TUserHuman)who).CurQuest = null;
                //FillChar(((TUserHuman)who).QuestParams, sizeof(int) * 10, '\0');
            }
            if (title.ToLower().CompareTo("@main".ToLower()) == 0)
            {
                for (i = 0; i < Sayings.Count; i++)
                {
                    TQuestRecord pqr = Sayings[i] as TQuestRecord;
                    for (j = 0; j < pqr.SayingList.Count; j++)
                    {
                        psay = (TSayingRecord)pqr.SayingList[j];
                        if (psay.Title.ToLower().CompareTo(title.ToLower()) == 0)
                        {
                            pquest = pqr;
                            ((TUserHuman)who).CurQuest = pquest;
                            ((TUserHuman)who).CurQuestNpc = this;
                            break;
                        }
                    }
                    if (pquest != null)
                    {
                        break;
                    }
                }
            }
            if (pquest == null)
            {
                if (((TUserHuman)who).CurQuest != null)
                {
                    for (i = Sayings.Count - 1; i >= 0; i--)
                    {
                        if (((TUserHuman)who).CurQuest == (Sayings[i] as TQuestRecord))
                        {
                            pquest = Sayings[i] as TQuestRecord;
                        }
                    }
                }
                if (pquest == null)
                {
                    for (i = Sayings.Count - 1; i >= 0; i--)
                    {
                        //if (NpcSayTitle_CheckQuestCondition(Sayings[i] as TQuestRecord))
                        //{
                        //    pquest = Sayings[i] as TQuestRecord;
                        //    ((TUserHuman)who).CurQuest = pquest;
                        //    ((TUserHuman)who).CurQuestNpc = this;
                        //}
                    }
                }
            }
            if (pquest != null)
            {
                for (j = 0; j < pquest.SayingList.Count; j++)
                {
                    psay = pquest.SayingList[j] as TSayingRecord;
                    if (psay.Title.ToLower().CompareTo(title.ToLower()) == 0)
                    {
                        str = "";
                        for (m = 0; m < psay.Procs.Count; m++)
                        {
                            psayproc = psay.Procs[m] as TSayingProcedure;
                            if (psayproc == null)
                            {
                                continue;
                            }
                            //if (NpcSayTitle_CheckSayingCondition(who, psayproc.ConditionList))
                            //{
                            //    str = str + psayproc.Saying;
                            //    if (!NpcSayTitle_DoActionList(who, psayproc.ActionList))
                            //    {
                            //        break;
                            //    }
                            //    if (bosaynow)
                            //    {
                            //        NpcSayTitle_NpcSayProc(who, str, true);
                            //        ((TUserHuman)who).CurSayProc = psayproc;
                            //    }
                            //}
                            //else
                            //{
                            //    str = str + psayproc.ElseSaying;
                            //    if (!NpcSayTitle_DoActionList(who, psayproc.ElseActionList))
                            //    {
                            //        break;
                            //    }
                            //    if (bosaynow)
                            //    {
                            //        NpcSayTitle_NpcSayProc(who, str, true);
                            //        ((TUserHuman)who).CurSayProc = psayproc;
                            //    }
                            //}
                        }
                        if (str != "")
                        {
                            NpcSayTitle_NpcSayProc(who, str, false);
                            ((TUserHuman)who).CurSayProc = psayproc;
                        }
                        break;
                    }
                }
            }
            batchlist.Free();
        }

        public void ClearNpcInfos()
        {
            int i;
            int j;
            int k;
            int m;
            TQuestRecord pqr;
            TSayingRecord psay;
            TSayingProcedure psayproc;
            for (i = 0; i < Sayings.Count; i++)
            {
                pqr = Sayings[i] as TQuestRecord;
                for (j = 0; j < pqr.SayingList.Count; j++)
                {
                    psay = (TSayingRecord)pqr.SayingList[j];
                    for (k = 0; k < psay.Procs.Count; k++)
                    {
                        psayproc = psay.Procs[k] as TSayingProcedure;
                        for (m = 0; m < psayproc.ConditionList.Count; m++)
                        {
                            Dispose(psayproc.ConditionList[m] as TQuestConditionInfo);
                        }
                        for (m = 0; m < psayproc.ActionList.Count; m++)
                        {
                            Dispose(psayproc.ActionList[m] as TQuestActionInfo);
                        }
                        for (m = 0; m < psayproc.ElseActionList.Count; m++)
                        {
                            Dispose(psayproc.ElseActionList[m] as TQuestActionInfo);
                        }
                        psayproc.ConditionList.Free();
                        psayproc.ActionList.Free();
                        psayproc.ElseActionList.Free();
                        Dispose(psayproc);
                    }
                    psay.Procs.Free();
                    Dispose(psay);
                }
                pqr.SayingList.Free();
                Dispose(pqr);
            }
            Sayings.Clear();
        }

        public void LoadNpcInfos()
        {
            if (BoUseMapFileName)
            {
                NpcBaseDir = LocalDB.NPCDEFDIR;
                LocalDB.FrmDB.LoadNpcDef(this, DefineDirectory, this.UserName + "-" + this.MapName);
            }
            else
            {
                NpcBaseDir = DefineDirectory;
                LocalDB.FrmDB.LoadNpcDef(this, DefineDirectory, this.UserName);
            }
            // ArrangeSayStrings;

        }

        public void LoadMemorialCount()
        {
            LocalDB.FrmDB.LoadMemorialCount(this, this.UserName + "-" + this.MapName);
        }

        public void WriteMemorialCount()
        {
            LocalDB.FrmDB.WriteMemorialCount(this, this.UserName + "-" + this.MapName);
        }

        // 惑牢捞 且 荐 乐绰 扁瓷 力绢,  魄概, 备涝, 该扁扁 殿...
        public virtual void UserCall(TCreature caller)
        {
        }

        public virtual void UserSelect(TCreature whocret, string selstr)
        {

        }

    }
}

