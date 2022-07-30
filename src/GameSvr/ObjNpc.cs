using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using SystemModule;

namespace GameSvr
{
    public class TUpgradeInfo
    {
        public string UserName;
        public TUserItem uitem;
        public byte updc;
        public byte upsc;
        public byte upmc;
        public byte durapoint;
        public DateTime readydate;
        public long readycount;
    } 

    public struct TQuestRequire
    {
        public int RandomCount;
        public ushort CheckIndex;
        public byte CheckValue;
    }

    public struct TQuestActionInfo
    {
        public int ActIdent;
        public string ActParam;
        public int ActParamVal;
        public string ActTag;
        public int ActTagVal;
        public string ActExtra;
        public int ActExtraVal;
        public string ActParam4;
        public int ActParamVal4;
        public string ActParam5;
        public int ActParamVal5;
        public string ActParam6;
        public int ActParamVal6;
        public string ActParam7;
        public int ActParamVal7;
    }

    public struct TQuestConditionInfo
    {
        public int IfIdent;
        public string IfParam;
        public int IfParamVal;
        public string IfTag;
        public int IfTagVal;
    }

    public class TSayingProcedure
    {
        public ArrayList ConditionList;
        public ArrayList ActionList;
        public string Saying;
        public ArrayList ElseActionList;
        public string ElseSaying;
        public ArrayList AvailableCommands;
    }

    public class TSayingRecord
    {
        public string Title;
        public ArrayList Procs;
    } 

    public class TQuestRecord
    {
        public bool BoRequire;
        public int LocalNumber;
        public TQuestRequire[] QuestRequireArr;
        public ArrayList SayingList;
    } 

    public class TNormNpc : TAnimal
    {
        public byte NpcFace = 0;
        public ArrayList Sayings = null;
        public string DefineDirectory = String.Empty;
        public bool BoInvisible = false;
        public bool BoUseMapFileName = false;
        public string NpcBaseDir = String.Empty;
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
            SoundStartTime = GetTickCount;
            MemorialCount = 0;
        }

        ~TNormNpc()
        {
            for (var i = 0; i < Sayings.Count; i++)
            {
                Dispose(Sayings[i] as TQuestRecord);
            }
            Sayings.Free();
            base.Destroy();
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
            // ¾ÆÀÌÅÛ Á¦Á¶
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
            // »õ·ÎÃß°¡(sonmg)
            if (lwstr.IndexOf("@makeetc") > 0)
            {
                CanMakeItem = true;
            }
            // »õ·ÎÃß°¡(sonmg)
            // À§Å¹»óÁ¡
            if (lwstr.IndexOf("@market_") > 0)
            {
                CanItemMarket = true;
            }
            // ¹®ÆÄÀå¿ø
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
            // ¹®ÆÄÀå¿ø(°ü¸®)
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
            // Àå¿ø²Ù¹Ì±â
            if (lwstr.IndexOf("@ga_decoitem_buy") > 0)
            {
                CanBuyDecoItem = true;
            }
            if (lwstr.IndexOf("@ga_decomon_count") > 0)
            {
                CanBuyDecoItem = true;
            }
        }

        // procedure ArrangeSayStrings;
        public void NpcSay(TCreature target, string str)
        {
            // Á¡Â÷ ¾È ¾²ÀÓ... ÇÏµåÄÚµù ÇÏÁö ¾Ê´Â °ÍÀÌ ÁÁÀ½
            str = ReplaceChar(str, "\\", (char)0xa);
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
            string str2 = String.Empty;
            int n;
            if (tag == "$OWNERGUILD")
            {
                data = svMain.UserCastle.OwnerGuildName;
                if (data == "")
                {
                    data = "GameManagerconsultation";
                }
                source = ChangeNpcSayTag(source, "<$OWNERGUILD>", data);
            }
            if (tag == "$LORD")
            {
                if (svMain.UserCastle.OwnerGuild != null)
                {
                    data = svMain.UserCastle.OwnerGuild.GetGuildMaster();
                }
                else
                {
                    data = "Ô¤±¸";
                }
                source = ChangeNpcSayTag(source, "<$LORD>", data);
            }
            if (tag == "$GUILDWARFEE")
            {
                source = ChangeNpcSayTag(source, "<$GUILDWARFEE>", ObjNpc.GUILDWARFEE.ToString());
            }
            if (tag == "$CASTLEWARDATE")
            {
                if (!svMain.UserCastle.BoCastleUnderAttack)
                {
                    data = svMain.UserCastle.GetNextWarDateTimeStr();
                    if (data != "")
                    {
                        source = ChangeNpcSayTag(source, "<$CASTLEWARDATE>", data);
                    }
                    else
                    {
                        source = "¶ÌÆÚÄÚÃ»ÓÐ¹¥³ÇÕ½¡£\\ \\<·µ»Ø/@main> ";
                    }
                }
                else
                {
                    source = "ÕýÔÚ¹¥³ÇÖÐ£¡\\ \\<·µ»Ø/@main>";
                }
                source = ReplaceChar(source, "\\", (char)0xa);
            }
            if (tag == "$LISTOFWAR")
            {
                data = svMain.UserCastle.GetListOfWars();
                // ¸ðµç °ø¼º ÀÏÁ¤
                if (data != "")
                {
                    source = ChangeNpcSayTag(source, "<$LISTOFWAR>", data);
                }
                else
                {
                    source = "ÎÒÃÇÎ´È·¶¨Ê±¼ä...\\ \\<·µ»Ø/@main>";
                }
                source = ReplaceChar(source, "\\", (char)0xa);
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
            // ¿©°ü º¸°ü °³¼ö
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
            // ¹®ÆÄ Àå¿ø.
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
            // Àå¿ø²Ù¹Ì±â.
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
                            source = ChangeNpcSayTag(source, "<" + tag + ">", svMain.GrobalQuestParams[n - 100].ToString());
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
            string str;
            ArrayList strlist;
            result = false;
            listfile = svMain.EnvirDir + listfile;
            if (File.Exists(listfile))
            {
                strlist = new ArrayList();
                try
                {
                    ObjNpc.ReadStrings(listfile, strlist);
                }
                catch
                {
                    svMain.MainOutMessage("loading fail.... => " + listfile);
                }
                for (i = 0; i < strlist.Count; i++)
                {
                    str = strlist[i].Trim();
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
                    svMain.MainOutMessage("saving fail.... => " + listfile);
                }
                strlist.Free();
            }
            else
            {
                svMain.MainOutMessage("file not found => " + listfile);
            }
            return result;
        }

        // 6-11
        public bool NpcSayTitle_CheckNameFromFileList(string uname, string listfile)
        {
            bool result;
            int i;
            string str;
            ArrayList strlist;
            result = false;
            listfile = svMain.EnvirDir + listfile;
            if (File.Exists(listfile))
            {
                strlist = new ArrayList();
                try
                {
                    ObjNpc.ReadStrings(listfile, strlist);
                }
                catch
                {
                    svMain.MainOutMessage("loading fail.... => " + listfile);
                }
                for (i = 0; i < strlist.Count; i++)
                {
                    str = strlist[i].Trim();
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
                svMain.MainOutMessage("file not found => " + listfile);
            }
            return result;
        }

        // 6-11
        public void NpcSayTitle_AddNameFromFileList(string uname, string listfile)
        {
            ArrayList strlist;
            listfile = svMain.EnvirDir + listfile;
            strlist = new ArrayList();
            if (File.Exists(listfile))
            {
                try
                {
                    ObjNpc.ReadStrings(listfile, strlist);
                }
                catch
                {
                    svMain.MainOutMessage("loading fail.... => " + listfile);
                }
            }
            // flag := FALSE;
            // for i:=0 to strlist.Count-1 do begin
            // str := Trim(strlist[i]);
            // if str = uname then begin
            // flag := TRUE;
            // break;
            // end;
            // end;
            // if not flag then   //º¹¼ö·Î Ãß°¡ ¾ÈÇÔ
            strlist.Add(uname);
            try
            {
                ObjNpc.WriteStrings(listfile, strlist);
            }
            catch
            {
                svMain.MainOutMessage("saving fail.... => " + listfile);
            }
            strlist.Free();
        }

        // 6-11
        public void NpcSayTitle_DeleteNameFromFileList(string uname, string listfile)
        {
            int i;
            string str;
            ArrayList strlist;
            listfile = svMain.EnvirDir + listfile;
            if (File.Exists(listfile))
            {
                strlist = new ArrayList();
                try
                {
                    ObjNpc.ReadStrings(listfile, strlist);
                }
                catch
                {
                    svMain.MainOutMessage("loading fail.... => " + listfile);
                }
                for (i = 0; i < strlist.Count; i++)
                {
                    str = strlist[i].Trim();
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
                    svMain.MainOutMessage("saving fail.... => " + listfile);
                }
                strlist.Free();
            }
            else
            {
                svMain.MainOutMessage("file not found => " + listfile);
            }
        }

        public bool NpcSayTitle_CheckQuestCondition(TCreature who, TQuestRecord pq)
        {
            bool result;
            int i;
            result = true;
            if (pq.BoRequire)
            {
                for (i = 0; i < ObjNpc.MAXREQUIRE; i++)
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

        public TUserItem NpcSayTitle_FindItemFromState(TCreature who, string iname, int count)
        {
            TUserItem result;
            int n = 0;
            result = null;
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

        public bool NpcSayTitle_CheckSayingCondition(TCreature who, ArrayList clist)
        {
            bool result;
            int i;
            int k;
            int m;
            int n;
            int param;
            int tag;
            int count;
            int durasum;
            int duratop;
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
                            // ¿ä±¸°¡ ³²ÀÚ
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
                            if (svMain.MirDayTime != 0)
                            {
                                result = false;
                            }
                        }
                        if (pqc.IfParam.ToLower().CompareTo("DAY".ToLower()) == 0)
                        {
                            if (svMain.MirDayTime != 1)
                            {
                                result = false;
                            }
                        }
                        if (pqc.IfParam.ToLower().CompareTo("SUNSET".ToLower()) == 0)
                        {
                            if (svMain.MirDayTime != 2)
                            {
                                result = false;
                            }
                        }
                        if (pqc.IfParam.ToLower().CompareTo("NIGHT".ToLower()) == 0)
                        {
                            if (svMain.MirDayTime != 3)
                            {
                                result = false;
                            }
                        }
                        break;
                    case Grobal2.QI_DAYOFWEEK:
                        if (HUtil32.CompareLStr(pqc.IfParam, "Sun", 3))
                        {
                            if (DateTime.Today.DayOfWeek != 1)
                            {
                                result = false;
                            }
                        }
                        if (HUtil32.CompareLStr(pqc.IfParam, "Mon", 3))
                        {
                            if (DateTime.Today.DayOfWeek != 2)
                            {
                                result = false;
                            }
                        }
                        if (HUtil32.CompareLStr(pqc.IfParam, "Tue", 3))
                        {
                            if (DateTime.Today.DayOfWeek != 3)
                            {
                                result = false;
                            }
                        }
                        if (HUtil32.CompareLStr(pqc.IfParam, "Wed", 3))
                        {
                            if (DateTime.Today.DayOfWeek != 4)
                            {
                                result = false;
                            }
                        }
                        if (HUtil32.CompareLStr(pqc.IfParam, "Thu", 3))
                        {
                            if (DateTime.Today.DayOfWeek != 5)
                            {
                                result = false;
                            }
                        }
                        if (HUtil32.CompareLStr(pqc.IfParam, "Fri", 3))
                        {
                            if (DateTime.Today.DayOfWeek != 6)
                            {
                                result = false;
                            }
                        }
                        if (HUtil32.CompareLStr(pqc.IfParam, "Sat", 3))
                        {
                            if (DateTime.Today.DayOfWeek != 7)
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
                        pcheckitem = who.FindItemNameEx(pqc.IfParam, ref count, ref durasum, ref duratop);
                        if (count < pqc.IfTagVal)
                        {
                            result = false;
                        }
                        break;
                    case Grobal2.QI_CHECKITEMW:
                        pcheckitem = NpcSayTitle_FindItemFromState(pqc.IfParam, pqc.IfTagVal);
                        if (pcheckitem == null)
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
                        if (latesttakeitem != pqc.IfParam)
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
                        pcheckitem = who.FindItemNameEx(pqc.IfParam, ref count, ref durasum, ref duratop);
                        if (HUtil32.MathRound(duratop / 1000) < pqc.IfTagVal)
                        {
                            result = false;
                        }
                        break;
                    case Grobal2.QI_CHECKDURAEVA:
                        pcheckitem = who.FindItemNameEx(pqc.IfParam, ref count, ref durasum, ref duratop);
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
                        penv = svMain.GrobalEnvir.GetEnvir(pqc.IfParam);
                        if (penv != null)
                        {
                            if (svMain.UserEngine.GetMapMons(penv, null) < pqc.IfTagVal)
                            {
                                result = false;
                            }
                        }
                        break;
                    case Grobal2.QI_CHECKMON_NORECALLMOB_MAP:
                        penv = svMain.GrobalEnvir.GetEnvir(pqc.IfParam);
                        if (penv != null)
                        {
                            if (svMain.UserEngine.GetMapMonsNoRecallMob(penv, null) < pqc.IfTagVal)
                            {
                                result = false;
                            }
                        }
                        break;
                    case Grobal2.QI_CHECKMON_AREA:
                        break;
                    case Grobal2.QI_CHECKHUM:
                        if (svMain.UserEngine.GetHumCount(pqc.IfParam) < pqc.IfTagVal)
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
                                ps = svMain.UserEngine.GetStdItemFromName(pqc.IfParam);
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
                        // Event Grade
                        // ------------------------------------------------------------
                        // IfParamVal : ÀÌº¥Æ® ¾ÆÀÌÅÛ µî±Þ
                        // IfTagVal   : Á¸Àç ¾ÆÀÌÅÛ °³¼ö(Ä«¿îÆ® ¾ÆÀÌÅÛÀº ÇÏ³ª·Î °£ÁÖ)
                        // ÁöÁ¤ µî±ÞÀÇ ¾ÆÀÌÅÛÀ» ÁöÁ¤ °³¼ö ÀÌ»ó °¡Áö°í ÀÖ´ÂÁö °Ë»çÇØ¼­
                        // ÀÖÀ¸¸é TRUE, ¾øÀ¸¸é FALSE¸¦ ¸®ÅÏÇÔ.
                        // ------------------------------------------------------------
                        if (who.FindItemEventGrade(pqc.IfParamVal, pqc.IfTagVal) == false)
                        {
                            result = false;
                        }
                        break;
                    case Grobal2.QI_CHECKBAGREMAIN:
                        // °¡¹æÃ¢¿¡ Parameter °³¼ö¸¸Å­ ³²¾Æ ÀÖ´ÂÁö
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
                                    equalvar = svMain.GrobalQuestParams[m - 100];
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
                                    // Àü¿ªº¯¼ö
                                    if (svMain.GrobalQuestParams[n - 100] != equalvar)
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
                                    // ¸ÊÁö¿ªº¯¼ö
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
                                    // Àü¿ªº¯¼ö
                                    if (svMain.GrobalQuestParams[n - 100] != pqc.IfTagVal)
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
                                    // ¸ÊÁö¿ªº¯¼ö
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
                                    if (svMain.GrobalQuestParams[n - 100] <= pqc.IfTagVal)
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
                                    if (svMain.GrobalQuestParams[n - 100] >= pqc.IfTagVal)
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
                        // ÀÚ½ÅÀÌ GroupOwnerÀÎÁö °Ë»ç.
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
                        // ÀÚ½ÅÀÌ Ã¼ÇèÆÇ À¯ÀúÀÎÁö °Ë»çÇÑ´Ù.
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
                            hum = svMain.UserEngine.GetUserHuman(((TUserHuman)who).fLover.GetLoverName);
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
                            hum = svMain.UserEngine.GetUserHuman(((TUserHuman)who).fLover.GetLoverName);
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
                            if (HUtil32.Str_ToInt(((TUserHuman)who).fLover.GetLoverDays, 0) < param)
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
                                // ÀúÁÖ°¡ ºÙ¾îÀÖÀ» ¶§
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
                            hum = svMain.UserEngine.GetUserHuman(who.GroupMembers[k]);
                            if (hum != null)
                            {
                                // °°Àº ¸Ê¿¡ ÀÖ´ÂÁö Ã¼Å©
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
                        // ÁÖº¯¿¡ ¿¬ÀÎ ¸ÎÀº »ç¶÷ÀÌ ÀÖ´ÂÁö Ã¼Å©
                        flag = false;
                        list = new ArrayList();
                        // ¸ÊÄù½ºÆ®¸¦ À§ÇØ PEnvir ´ë½Å¿¡ who.PEnvir¸¦ »ç¿ëÇÑ´Ù.
                        who.PEnvir.GetCreatureInRange(who.CX, who.CY, pqc.IfParamVal, true, list);
                        for (k = 0; k < list.Count; k++)
                        {
                            cret = (TCreature)list[k];
                            if ((cret != null) && (cret.RaceServer == Grobal2.RC_USERHUMAN))
                            {
                                hum = (TUserHuman)cret;
                                if ((hum.fLover != null) && (hum.fLover.GetLoverName != "") && (hum != who))
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
                        if (((TUserHuman)who).fLover.GetLoverName == "")
                        {
                            result = false;
                        }
                        break;
                    case Grobal2.QI_CHECKPOSEMARRY:
                        cret = ((TUserHuman)who).GetFrontCret();
                        if ((cret != null) && (cret.RaceServer == Grobal2.RC_USERHUMAN))
                        {
                            if (((TUserHuman)cret).fLover.GetLoverName == "")
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
                        else if (pqc.IfParam.ToLower().CompareTo("ÄÐ".ToLower()) == 0)
                        {
                            btSex = 0;
                        }
                        else if (pqc.IfParam.ToLower().CompareTo("WOMAN".ToLower()) == 0)
                        {
                            btSex = 1;
                        }
                        else if (pqc.IfParam.ToLower().CompareTo("Å®".ToLower()) == 0)
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
                                    // ÒªÇóÏàÍ¬ÐÔ±ð
                                    if (cret.Sex != who.Sex)
                                    {
                                        result = true;
                                    }
                                    break;
                                default:
                                    // ÒªÇó²»Í¬ÐÔ±ð
                                    result = true;
                                    break;
                                    // ÎÞ²ÎÊýÊ±²»ÅÐ±ðÐÔ±ð
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
                        // ¼ì²âÊÇ·ñ°ÝÊ¦
                        result = false;
                        if ((who.m_sMasterName != "") && (!who.m_boMaster))
                        {
                            result = true;
                        }
                        break;
                    case Grobal2.QI_CHECKISMASTER:
                        // ¼ì²âÊÇ²»ÊÇ±ðÈËÊ¦¸µ
                        result = false;
                        if (who.m_boMaster)
                        {
                            result = true;
                        }
                        break;
                    case Grobal2.QI_CHECKPOSEMASTER:
                        // ¼ì²âÊÇ·ñÊÇ±ðÈËÍ½µÜ
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
            int i;
            for (i = 0; i < Sayings.Count; i++)
            {
                if ((Sayings[i] as TQuestRecord).LocalNumber == num)
                {
                    ((TUserHuman)who).CurQuest as TQuestRecord = Sayings[i] as TQuestRecord;
                    ((TUserHuman)who).CurQuestNpc = this;
                    NpcSayTitle(who, "@main");
                    break;
                }
            }
        }

        public void NpcSayTitle_GotoSay(string saystr)
        {
            NpcSayTitle(who, saystr);
        }

        public void NpcSayTitle_ActionOfMarry(TUserHuman who, TQuestActionInfo pqa)
        {
            TUserHuman PoseHuman;
            string sSayMsg;
            string msgstr;
            string data = string.Empty;
            string str;
            int listCount;
            if (who.fLover.GetLoverName != "")
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
                            svMain.UserEngine.SendBroadCastMsg(sSayMsg, 0);
                            sSayMsg = M2Share.g_sStartMarryManAskQuestionMsg.Replace("%n", this.UserName);
                            sSayMsg = sSayMsg.Replace("%s", who.UserName);
                            sSayMsg = sSayMsg.Replace("%d", PoseHuman.UserName);
                            svMain.UserEngine.SendBroadCastMsg(sSayMsg, 0);
                        }
                        else if ((who.Sex == 1) && (PoseHuman.Sex == 0))
                        {
                            sSayMsg = M2Share.g_sStartMarryWoManMsg.Replace("%n", this.UserName);
                            sSayMsg = sSayMsg.Replace("%s", who.UserName);
                            sSayMsg = sSayMsg.Replace("%d", PoseHuman.UserName);
                            svMain.UserEngine.SendBroadCastMsg(sSayMsg, 0);
                            sSayMsg = M2Share.g_sStartMarryWoManAskQuestionMsg.Replace("%n", this.UserName);
                            sSayMsg = sSayMsg.Replace("%s", who.UserName);
                            sSayMsg = sSayMsg.Replace("%d", PoseHuman.UserName);
                            svMain.UserEngine.SendBroadCastMsg(sSayMsg, 0);
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
                        svMain.UserEngine.SendBroadCastMsg(sSayMsg, 0);
                        sSayMsg = M2Share.g_sMarryManAskQuestionMsg.Replace("%n", this.UserName);
                        sSayMsg = sSayMsg.Replace("%s", who.UserName);
                        sSayMsg = sSayMsg.Replace("%d", PoseHuman.UserName);
                        svMain.UserEngine.SendBroadCastMsg(sSayMsg, 0);
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
                            svMain.UserEngine.SendBroadCastMsg(sSayMsg, 0);
                            sSayMsg = M2Share.g_sMarryWoManGetMarryMsg.Replace("%n", this.UserName);
                            sSayMsg = sSayMsg.Replace("%s", who.UserName);
                            sSayMsg = sSayMsg.Replace("%d", PoseHuman.UserName);
                            svMain.UserEngine.SendBroadCastMsg(sSayMsg, 0);
                            NpcSayTitle(who, "@EndMarry");
                            NpcSayTitle(PoseHuman, "@EndMarry");
                            who.m_boStartMarry = false;
                            PoseHuman.m_boStartMarry = false;
                            data = "";
                            who.fLover.ReqSequence = Grobal2.RsReq_None;
                            who.fLover.Add(who.UserName, PoseHuman.UserName, Grobal2.RsState_Lover, PoseHuman.WAbil.Level, PoseHuman.Sex, data, "");
                            msgstr = who.fLover.GetListmsg(Grobal2.RsState_Lover, listCount);
                            who.SendDefMessage(Grobal2.SM_LM_LIST, 0, listCount, 0, 0, msgstr);
                            who.SendDefMessage(Grobal2.SM_LM_RESULT, 0, Grobal2.RsState_Lover, Grobal2.RsError_SuccessJoin, 0, PoseHuman.UserName);
                            PoseHuman.fLover.ReqSequence = Grobal2.RsReq_None;
                            PoseHuman.fLover.Add(PoseHuman.UserName, who.UserName, Grobal2.RsState_Lover, who.WAbil.Level, who.Sex, data, "");
                            msgstr = PoseHuman.fLover.GetListmsg(Grobal2.RsState_Lover, listCount);
                            PoseHuman.SendDefMessage(Grobal2.SM_LM_LIST, 0, listCount, 0, 0, msgstr);
                            PoseHuman.SendDefMessage(Grobal2.SM_LM_RESULT, 0, Grobal2.RsState_Lover, Grobal2.RsError_SuccessJoined, 0, who.UserName);
                            who.SendMsg(who, Grobal2.RM_LM_DBADD, 0, 0, 0, 0, PoseHuman.UserName + ":" + 10.ToString() + ":" + data + "/");
                            str = "¹§Ï²£¡\"" + who.UserName + "\"Óë\"" + PoseHuman.UserName + "\"³ÉÎªÇéÂÂ¡£";
                            svMain.UserEngine.CryCry(Grobal2.RM_SYSMSG_PINK, this.PEnvir, this.CX, this.CY, 300, ":)" + str);
                            svMain.AddUserLog("47\09" + this.MapName + "\09" + this.CX.ToString() + "\09" + this.CY.ToString() + "\09" + who.UserName + "\09" + "0\09" + "0\09" + "0\09" + PoseHuman.UserName);
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
                            svMain.UserEngine.SendBroadCastMsg(sSayMsg, 0);
                            sSayMsg = M2Share.g_sMarryWoManCancelMsg.Replace("%n", this.UserName);
                            sSayMsg = sSayMsg.Replace("%s", who.UserName);
                            sSayMsg = sSayMsg.Replace("%d", PoseHuman.UserName);
                            svMain.UserEngine.SendBroadCastMsg(sSayMsg, 0);
                        }
                    }
                }
                return;
            }
        }

        public void NpcSayTitle_ActionOfUnMarry(TUserHuman who, TQuestActionInfo pqa)
        {
            TUserHuman PoseHuman;
            int svidx=0;
            if (who.fLover.GetLoverName == "")
            {
                NpcSayTitle(who, "@ExeMarryFail");
                // Äã¶¼Ã»½á¹ý»é£¬ÅÜÀ´×öÊ²Ã´£¿ \ \
                return;
            }
            PoseHuman = (TUserHuman)who.GetFrontCret();
            if (PoseHuman == null)
            {
                NpcSayTitle(who, "@UnMarryCheckDir");
                // ¼ì²â»éÒöÊÇ·ñ¶ÔÃæ
            }
            if (PoseHuman != null)
            {
                if (pqa.ActParam == "")
                {
                    if (PoseHuman.RaceServer != Grobal2.RC_USERHUMAN)
                    {
                        NpcSayTitle(who, "@UnMarryTypeErr");
                        // ¶ÔÃæ²»ÊÇÈËÎï
                        return;
                    }
                    if (PoseHuman.GetFrontCret() == who)
                    {
                        if (who.fLover.GetLoverName == PoseHuman.UserName)
                        {
                            NpcSayTitle(who, "@StartUnMarry");
                            // ¿ªÊ¼Àë»é
                            NpcSayTitle(PoseHuman, "@StartUnMarry");
                            return;
                        }
                    }
                }
            }
            // sREQUESTUNMARRY
            if (pqa.ActParam.ToLower().CompareTo("REQUESTUNMARRY".ToLower()) == 0)
            {
                // È·¶¨Àë»é
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
                                // HP, MP º¯°æ(50%)
                                who.WAbil.HP = (ushort)_MAX(1, who.WAbil.HP / 2);
                                who.WAbil.MP = (ushort)_MAX(1, who.WAbil.MP / 2);
                                this.SysMsg("ÇéÂÂ¹ØÏµÆÆÁÑÁË£¬½«Ôì³É×ÔÉíµÄ³å»÷¡£", 0);
                                svMain.AddUserLog("47\09" + this.MapName + "\09" + this.CX.ToString() + "\09" + this.CY.ToString() + "\09" + this.UserName + "\09" + "0\09" + "0\09" + "2\09" + PoseHuman.UserName);
                                who.UserNameChanged();
                                if (PoseHuman != null)
                                {
                                    if (PoseHuman.RelationShipDeleteOther(Grobal2.RsState_Lover, who.UserName))
                                    {
                                        PoseHuman.MakePoison(Grobal2.POISON_SLOW, 3, 1);
                                        // HP, MP º¯°æ(50%)
                                        PoseHuman.WAbil.HP = (ushort)_MAX(1, PoseHuman.WAbil.HP / 2);
                                        PoseHuman.WAbil.MP = (ushort)_MAX(1, PoseHuman.WAbil.MP / 2);
                                        PoseHuman.SysMsg("ÇéÂÂ¹ØÏµÆÆÁÑÁË£¬½«Ôì³É×ÔÉíµÄ³å»÷¡£", 0);
                                        svMain.AddUserLog("47\09" + PoseHuman.MapName + "\09" + PoseHuman.CX.ToString() + "\09" + PoseHuman.CY.ToString() + "\09" + PoseHuman.UserName + "\09" + "0\09" + "0\09" + "2\09" + who.UserName);
                                    }
                                    PoseHuman.UserNameChanged();
                                }
                                else
                                {
                                    if (svMain.UserEngine.FindOtherServerUser(PoseHuman.UserName, ref svidx))
                                    {
                                        svMain.UserEngine.SendInterMsg(Grobal2.ISM_LM_DELETE, svidx, PoseHuman.UserName + "/" + this.UserName + "/" + Grobal2.RsState_Lover.ToString());
                                    }
                                }
                            }
                            else
                            {
                                who.SendDefMessage(Grobal2.SM_LM_RESULT, Grobal2.RsState_Lover, Grobal2.RsError_DontDelete, 0, 0, PoseHuman.UserName);
                            }
                            NpcSayTitle(who, "@UnMarryEnd");
                            NpcSayTitle(PoseHuman, "@UnMarryEnd");// Àë»é½áÊøÓï
                        }
                        else
                        {
                            NpcSayTitle(who, "@WateUnMarry");
                        }
                        // ÇëÇóÀë»éºóÏÔÊ¾µÄÐÅÏ¢
                    }
                    return;
                }
                else
                {
                    // Ç¿ÐÐÀë»é
                    if (pqa.ActTag.ToLower().CompareTo("FORCE".ToLower()) == 0)
                    {
                        PoseHuman = svMain.UserEngine.GetUserHuman(who.fLover.GetLoverName);
                        if (PoseHuman != null)
                        {
                            if (who.RelationShipDeleteOther(Grobal2.RsState_Lover, PoseHuman.UserName))
                            {
                                this.MakePoison(Grobal2.POISON_SLOW, 3, 1);
                                // HP, MP º¯°æ(50%)
                                who.WAbil.HP = (ushort)_MAX(1, who.WAbil.HP / 2);
                                who.WAbil.MP = (ushort)_MAX(1, who.WAbil.MP / 2);
                                this.SysMsg("ÇéÂÂ¹ØÏµÆÆÁÑÁË£¬½«Ôì³É×ÔÉíµÄ³å»÷¡£", 0);
                                svMain.AddUserLog("47\09" + this.MapName + "\09" + this.CX.ToString() + "\09" + this.CY.ToString() + "\09" + this.UserName + "\09" + "0\09" + "0\09" + "2\09" + PoseHuman.UserName);
                                who.UserNameChanged();
                                if (PoseHuman != null)
                                {
                                    if (PoseHuman.RelationShipDeleteOther(Grobal2.RsState_Lover, who.UserName))
                                    {
                                        PoseHuman.MakePoison(Grobal2.POISON_SLOW, 3, 1);
                                        // HP, MP º¯°æ(50%)
                                        PoseHuman.WAbil.HP = (ushort)_MAX(1, PoseHuman.WAbil.HP / 2);
                                        PoseHuman.WAbil.MP = (ushort)_MAX(1, PoseHuman.WAbil.MP / 2);
                                        PoseHuman.SysMsg("ÇéÂÂ¹ØÏµÆÆÁÑÁË£¬½«Ôì³É×ÔÉíµÄ³å»÷¡£", 0);
                                        svMain.AddUserLog("47\09" + PoseHuman.MapName + "\09" + PoseHuman.CX.ToString() + "\09" + PoseHuman.CY.ToString() + "\09" + PoseHuman.UserName + "\09" + "0\09" + "0\09" + "2\09" + who.UserName);
                                    }
                                    PoseHuman.UserNameChanged();
                                }
                                else
                                {
                                    if (svMain.UserEngine.FindOtherServerUser(PoseHuman.UserName, ref svidx))
                                    {
                                        svMain.UserEngine.SendInterMsg(Grobal2.ISM_LM_DELETE, svidx, PoseHuman.UserName + "/" + who.UserName + "/" + Grobal2.RsState_Lover.ToString());
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
                            if (who.RelationShipDeleteOther(Grobal2.RsState_Lover, who.fLover.GetLoverName))
                            {
                                this.MakePoison(Grobal2.POISON_SLOW, 3, 1);
                                // HP, MP º¯°æ(50%)
                                who.WAbil.HP = (ushort)_MAX(1, who.WAbil.HP / 2);
                                who.WAbil.MP = (ushort)_MAX(1, who.WAbil.MP / 2);
                                this.SysMsg("ÇéÂÂ¹ØÏµÆÆÁÑÁË£¬½«Ôì³É×ÔÉíµÄ³å»÷¡£", 0);
                                svMain.AddUserLog("47\09" + this.MapName + "\09" + this.CX.ToString() + "\09" + this.CY.ToString() + "\09" + this.UserName + "\09" + "0\09" + "0\09" + "2\09" + who.UserName);
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
                            who.m_nMasterCount++;// Í½µÜÊýÁ¿¼Ó1
                            who.m_boStartMaster = false;
                            PoseHuman.m_boStartMaster = false;
                            if (who.m_sMasterName == "")
                            {
                                who.m_sMasterName = PoseHuman.UserName;// Í½µÜÃû×Ö
                                who.m_boMaster = true;// ÎªÊ¦¸µ
                            }
                            PoseHuman.m_sMasterName = who.UserName;// Ê¦¸µÃû×Ö
                            PoseHuman.m_boMaster = false;// ÎªÍ½µÜ
                            who.SendMsg(who, Grobal2.RM_MA_DBADD, 0, 0, 0, 0, PoseHuman.UserName + "1/" + BoolToInt(who.m_boMaster).ToString() + "/" + who.m_nMasterCount.ToString());// ·¢ËÍµÄÊ¦¸µÊý¾Ý
                            PoseHuman.SendMsg(PoseHuman, Grobal2.RM_MA_DBADD, 0, 0, 0, 0, who.UserName + "1/" + BoolToInt(PoseHuman.m_boMaster).ToString() + "/" + PoseHuman.m_nMasterRanking.ToString());// ·¢ËÍµÄÍ½µÜÊý¾Ý
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
                            who.SendMsg(who, Grobal2.RM_MA_DBDELETE, 0, 0, 0, 0, PoseHuman.UserName + "1/" + BoolToInt(who.m_boMaster).ToString() + "/" + who.m_nMasterCount.ToString());
                            PoseHuman.SendMsg(PoseHuman, Grobal2.RM_MA_DBDELETE, 0, 0, 0, 0, who.UserName + "1/" + BoolToInt(PoseHuman.m_boMaster).ToString() + "/" + PoseHuman.m_nMasterRanking.ToString());
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
                    // Ç¿ÐÐ³öÊ¦
                    if (pqa.ActTag.ToLower().CompareTo("FORCE".ToLower()) == 0)
                    {
                        ForceIndex = Convert.ToInt32(pqa.ActExtra);
                        if (ForceIndex < 0)
                        {
                            PoseHuman = svMain.UserEngine.GetUserHuman(who.m_sMasterName);
                            if (PoseHuman != null)
                            {
                                PoseHuman.m_nMasterCount -= 1;
                                who.SendMsg(who, Grobal2.RM_MA_DBDELETE, 0, 0, 0, 0, PoseHuman.UserName + "1/" + BoolToInt(who.m_boMaster).ToString() + "/" + who.m_nMasterCount.ToString());
                                PoseHuman.SendMsg(PoseHuman, Grobal2.RM_MA_DBDELETE, 0, 0, 0, 0, who.UserName + "1/" + BoolToInt(PoseHuman.m_boMaster).ToString() + "/" + PoseHuman.m_nMasterRanking.ToString());
                                PoseHuman.m_sMasterName = "";
                                who.SendMsg(who, Grobal2.RM_LM_DBMATLIST, 0, 0, 0, 0, "");
                                PoseHuman.CheckMaster();
                                PoseHuman.UserNameChanged();
                            }
                            else
                            {
                                who.SendMsg(who, Grobal2.RM_MA_DBDELETE, 0, 0, 0, 0, who.m_sMasterName + "1/" + BoolToInt(who.m_boMaster).ToString() + "/" + 4.ToString());
                            }
                            who.m_sMasterName = "";
                            NpcSayTitle(who, "@UnMasterEnd");
                            who.UserNameChanged();
                        }
                        else
                        {
                            TempMasterName = who.m_MasterRanking[ForceIndex - 1].sMasterName;
                            PoseHuman = svMain.UserEngine.GetUserHuman(TempMasterName);
                            if (PoseHuman != null)
                            {
                                PoseHuman.m_nMasterCount -= 1;
                                who.SendMsg(who, Grobal2.RM_MA_DBDELETE, 0, 0, 0, 0, PoseHuman.UserName + "1/" + BoolToInt(who.m_boMaster).ToString() + "/" + who.m_nMasterCount.ToString());
                                PoseHuman.SendMsg(PoseHuman, Grobal2.RM_MA_DBDELETE, 0, 0, 0, 0, who.UserName + "1/" + BoolToInt(PoseHuman.m_boMaster).ToString() + "/" + PoseHuman.m_nMasterRanking.ToString());
                                PoseHuman.m_sMasterName = "";
                                who.SendMsg(who, Grobal2.RM_LM_DBMATLIST, 0, 0, 0, 0, "");
                                PoseHuman.CheckMaster();
                                PoseHuman.UserNameChanged();
                            }
                            else
                            {
                                who.SendMsg(who, Grobal2.RM_MA_DBDELETE, 0, 0, 0, 0, TempMasterName + "1/" + BoolToInt(who.m_boMaster).ToString() + "/" + 4.ToString());
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
            if (!(nIndex >= Grobal2.TIconInfo.GetLowerBound(0) && nIndex <= Grobal2.TIconInfo.GetUpperBound(0)))
            {
                // ScriptActionError(who, '', pqa, sSC_SETHUMICON);
                return;
            }
            who.m_IconInfo[nIndex].wStart = (ushort)Convert.ToInt32(pqa.ActTag);
            who.m_IconInfo[nIndex].btFrame = (byte)Convert.ToInt32(pqa.ActExtra);
            who.m_IconInfo[nIndex].wFrameTime = (ushort)Convert.ToInt32(pqa.ActParam4);
            who.m_IconInfo[nIndex].bo01 = (byte)Convert.ToInt32(pqa.ActParam5);
            who.m_IconInfo[nIndex].nx = Convert.ToInt32(pqa.ActParam6);
            who.m_IconInfo[nIndex].ny = Convert.ToInt32(pqa.ActParam7);
            who.RefIconInfo();
        }

        public void NpcSayTitle_TakeItemFromUser(TCreature who, string iname, int count)
        {
            int i;
            TUserItem pu;
            TStdItem ps;
            pu = null;
            ps = null;
            if (iname.ToLower().CompareTo("½ð±Ò".ToLower()) == 0)
            {
                who.DecGold(count);
                who.GoldChanged();
                latesttakeitem = "½ð±Ò";
                // ÆÇ¸Å ¿Í °°ÀÌ¾¸
                // '±ÝÀü'
                svMain.AddUserLog("10\09" + who.MapName + "\09" + who.CX.ToString() + "\09" + who.CY.ToString() + "\09" + who.UserName + "\09" + Envir.NAME_OF_GOLD + "\09" + count.ToString() + "\09" + "1\09" + this.UserName);
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
                    ps = svMain.UserEngine.GetStdItem(pu.Index);
                    if (ps != null)
                    {
                        if (ps.Name.ToLower().CompareTo(iname.ToLower()) == 0)
                        {
                            // Ä«¿îÆ®¾ÆÀÌÅÛ (sonmg 2005/03/15)
                            if (ps.OverlapItem >= 1)
                            {
                                // ·Î±×³²±è
                                // ÆÇ¸Å ¿Í °°ÀÌ¾¸(Ä«¿îÆ®¾ÆÀÌÅÛ)
                                // count
                                svMain.AddUserLog("10\09" + who.MapName + "\09" + who.CX.ToString() + "\09" + who.CY.ToString() + "\09" + who.UserName + "\09" + pu.Index.ToString() + "\09" + count.ToString() + "\09" + "1\09" + this.UserName);
                                // -------------------------(sonmg 2005/03/15)
                                if (pu.Dura >= count)
                                {
                                    pu.Dura = (ushort)(pu.Dura - count);
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
                                // -------------------------
                                // TUserHuman(who).SendDelItem (pu^);    // ÀÌ»óÇÏ´Ù.
                                latesttakeitem = svMain.UserEngine.GetStdItemName(pu.Index);
                                // Dispose (pu);
                                // who.ItemList.Delete (i);
                                // Dec (count);
                                break;
                            }
                            else
                            {
                                // -----------------------------------------------------------
                                // ºÎÀûÀÌ¸é °³¼ö¸¦ Ã¼Å©ÇØ¼­ ¸ðÀÚ¸£¸é ´ÙÀ½ ¾ÆÀÌÅÛÀ¸·Î ³Ñ¾î°¨.
                                if (iname == svMain.GetUnbindItemName(ObjBase.SHAPE_AMULET_BUNCH))
                                {
                                    if (pu.Dura < pu.DuraMax)
                                    {
                                        continue;
                                    }
                                }
                                // -----------------------------------------------------------
                                // ·Î±×³²±è
                                // ÆÇ¸Å ¿Í °°ÀÌ¾¸
                                // count
                                svMain.AddUserLog("10\09" + who.MapName + "\09" + who.CX.ToString() + "\09" + who.CY.ToString() + "\09" + who.UserName + "\09" + iname + "\09" + pu.MakeIndex.ToString() + "\09" + "1\09" + this.UserName);
                                ((TUserHuman)who).SendDelItem(pu);
                                // ÀÌ»óÇÏ´Ù.
                                latesttakeitem = svMain.UserEngine.GetStdItemName(pu.Index);
                                Dispose(pu);
                                who.ItemList.RemoveAt(i);
                                count -= 1;
                            }
                        }
                    }
                }
            }
        }

        // ÁöÁ¤ µî±Þ ÀÌÇÏÀÇ ¾ÆÀÌÅÛÀ» ¸ðµÎ °¡Á®¿Â´Ù.
        public void NpcSayTitle_TakeEventGradeItemFromUser(TCreature who, int grade)
        {
            int i;
            TUserItem pu;
            TStdItem ps;
            pu = null;
            ps = null;
            for (i = who.ItemList.Count - 1; i >= 0; i--)
            {
                pu = who.ItemList[i];
                if (pu != null)
                {
                    ps = svMain.UserEngine.GetStdItem(pu.Index);
                    if (ps != null)
                    {
                        if (ps.EffType2 == Grobal2.EFFTYPE2_EVENT_GRADE)
                        {
                            if (ps.EffValue2 <= grade)
                            {
                                // ·Î±×³²±è
                                // ÆÇ¸Å ¿Í °°ÀÌ¾¸
                                // count
                                svMain.AddUserLog("10\09" + who.MapName + "\09" + who.CX.ToString() + "\09" + who.CY.ToString() + "\09" + who.UserName + "\09" + ps.Name + "\09" + pu.MakeIndex.ToString() + "\09" + "1\09" + this.UserName);
                                ((TUserHuman)who).SendDelItem(pu);
                                // ÀÌ»óÇÏ´Ù.
                                latesttakeitem = svMain.UserEngine.GetStdItemName(pu.Index);
                                Dispose(pu);
                                who.ItemList.RemoveAt(i);
                            }
                        }
                    }
                }
            }
        }

        public void NpcSayTitle_TakeWItemFromUser(TCreature who, string iname, int count)
        {
            int i;
            if (HUtil32.CompareLStr(iname, "[NECKLACE]", 4))
            {
                if (who.UseItems[Grobal2.U_NECKLACE].Index > 0)
                {
                    ((TUserHuman)who).SendDelItem(who.UseItems[Grobal2.U_NECKLACE]);
                    latesttakeitem = svMain.UserEngine.GetStdItemName(who.UseItems[Grobal2.U_NECKLACE].Index);
                    who.UseItems[Grobal2.U_NECKLACE].Index = 0;
                }
                return;
            }
            if (HUtil32.CompareLStr(iname, "[RING]", 4))
            {
                if (who.UseItems[Grobal2.U_RINGL].Index > 0)
                {
                    ((TUserHuman)who).SendDelItem(who.UseItems[Grobal2.U_RINGL]);
                    latesttakeitem = svMain.UserEngine.GetStdItemName(who.UseItems[Grobal2.U_RINGL].Index);
                    who.UseItems[Grobal2.U_RINGL].Index = 0;
                    return;
                }
                if (who.UseItems[Grobal2.U_RINGR].Index > 0)
                {
                    ((TUserHuman)who).SendDelItem(who.UseItems[Grobal2.U_RINGR]);
                    latesttakeitem = svMain.UserEngine.GetStdItemName(who.UseItems[Grobal2.U_RINGR].Index);
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
                    latesttakeitem = svMain.UserEngine.GetStdItemName(who.UseItems[Grobal2.U_ARMRINGL].Index);
                    who.UseItems[Grobal2.U_ARMRINGL].Index = 0;
                    return;
                }
                if (who.UseItems[Grobal2.U_ARMRINGR].Index > 0)
                {
                    ((TUserHuman)who).SendDelItem(who.UseItems[Grobal2.U_ARMRINGR]);
                    latesttakeitem = svMain.UserEngine.GetStdItemName(who.UseItems[Grobal2.U_ARMRINGR].Index);
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
                    latesttakeitem = svMain.UserEngine.GetStdItemName(who.UseItems[Grobal2.U_WEAPON].Index);
                    who.UseItems[Grobal2.U_WEAPON].Index = 0;
                }
                return;
            }
            if (HUtil32.CompareLStr(iname, "[HELMET]", 4))
            {
                if (who.UseItems[Grobal2.U_HELMET].Index > 0)
                {
                    ((TUserHuman)who).SendDelItem(who.UseItems[Grobal2.U_HELMET]);
                    latesttakeitem = svMain.UserEngine.GetStdItemName(who.UseItems[Grobal2.U_HELMET].Index);
                    who.UseItems[Grobal2.U_HELMET].Index = 0;
                }
                return;
            }
            // 2003/03/15 COPARK ¾ÆÀÌÅÛ ÀÎº¥Åä¸® È®Àå
            if (HUtil32.CompareLStr(iname, "[BUJUK]", 4))
            {
                if (who.UseItems[Grobal2.U_BUJUK].Index > 0)
                {
                    ((TUserHuman)who).SendDelItem(who.UseItems[Grobal2.U_BUJUK]);
                    latesttakeitem = svMain.UserEngine.GetStdItemName(who.UseItems[Grobal2.U_BUJUK].Index);
                    who.UseItems[Grobal2.U_BUJUK].Index = 0;
                }
                return;
            }
            if (HUtil32.CompareLStr(iname, "[BELT]", 4))
            {
                if (who.UseItems[Grobal2.U_BELT].Index > 0)
                {
                    ((TUserHuman)who).SendDelItem(who.UseItems[Grobal2.U_BELT]);
                    latesttakeitem = svMain.UserEngine.GetStdItemName(who.UseItems[Grobal2.U_BELT].Index);
                    who.UseItems[Grobal2.U_BELT].Index = 0;
                }
                return;
            }
            if (HUtil32.CompareLStr(iname, "[BOOTS]", 4))
            {
                if (who.UseItems[Grobal2.U_BOOTS].Index > 0)
                {
                    ((TUserHuman)who).SendDelItem(who.UseItems[Grobal2.U_BOOTS]);
                    latesttakeitem = svMain.UserEngine.GetStdItemName(who.UseItems[Grobal2.U_BOOTS].Index);
                    who.UseItems[Grobal2.U_BOOTS].Index = 0;
                }
                return;
            }
            if (HUtil32.CompareLStr(iname, "[CHARM]", 4))
            {
                if (who.UseItems[Grobal2.U_CHARM].Index > 0)
                {
                    ((TUserHuman)who).SendDelItem(who.UseItems[Grobal2.U_CHARM]);
                    latesttakeitem = svMain.UserEngine.GetStdItemName(who.UseItems[Grobal2.U_CHARM].Index);
                    who.UseItems[Grobal2.U_CHARM].Index = 0;
                }
                return;
            }
            // 2003/03/15 COPARK ¾ÆÀÌÅÛ ÀÎº¥Åä¸® È®Àå
            for (i = 0; i <= 12; i++)
            {
                // 8->12
                if (count <= 0)
                {
                    break;
                }
                if (who.UseItems[i].Index > 0)
                {
                    if (svMain.UserEngine.GetStdItemName(who.UseItems[i].Index).ToLower().CompareTo(iname.ToLower()) == 0)
                    {
                        ((TUserHuman)who).SendDelItem(who.UseItems[i]);
                        latesttakeitem = svMain.UserEngine.GetStdItemName(who.UseItems[i].Index);
                        who.UseItems[i].Index = 0;
                        count -= 1;
                    }
                }
            }
        }

        public void NpcSayTitle_GiveItemToUser(TCreature receivewho, string iname, int count)
        {
            int i;
            int idx;
            TStdItem pstd;
            TStdItem pstd2;
            TUserItem pu;
            int wg;
            if (iname.ToLower().CompareTo("½ð±Ò".ToLower()) == 0)
            {
                receivewho.IncGold(count);
                receivewho.GoldChanged();
                // ±¸ÀÔ ¿Í °°ÀÌ¾¸
                // '±ÝÀü'
                // count
                svMain.AddUserLog("9\09" + receivewho.MapName + "\09" + receivewho.CX.ToString() + "\09" + receivewho.CY.ToString() + "\09" + receivewho.UserName + "\09" + Envir.NAME_OF_GOLD + "\09" + count.ToString() + "\09" + "1\09" + this.UserName);
            }
            else
            {
                idx = 0;
                idx = svMain.UserEngine.GetStdItemIndex(iname);
                pstd = svMain.UserEngine.GetStdItem(idx);
                if ((idx > 0) && (pstd != null))
                {
                    for (i = 0; i < count; i++)
                    {
                        // Ä«¿îÆ®¾ÆÀÌÅÛ
                        if (pstd.OverlapItem >= 1)
                        {
                            if (receivewho.UserCounterItemAdd(pstd.StdMode, pstd.Looks, count, iname, false))
                            {
                                // ±¸ÀÔ ¿Í °°ÀÌ¾¸(Ä«¿îÆ®¾ÆÀÌÅÛ)
                                svMain.AddUserLog("9\09" + receivewho.MapName + "\09" + receivewho.CX.ToString() + "\09" + receivewho.CY.ToString() + "\09" + receivewho.UserName + "\09" + idx.ToString() + "\09" + count.ToString() + "\09" + "1\09" + this.UserName);
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
                        // and (wg <= receivewho.WAbil.MaxWeight)
                        if (receivewho.CanAddItem())
                        {
                            pu = new TUserItem();
                            if (svMain.UserEngine.CopyToUserItemFromName(iname, ref pu))
                            {
                                // gadget:Ä«¿îÆ®¾ÆÀÌÅÛ
                                if (pstd.OverlapItem >= 1)
                                {
                                    pu.Dura = (ushort)count;
                                }
                                receivewho.ItemList.Add(pu);
                                ((TUserHuman)receivewho).SendAddItem(pu);
                                // ·Î±×³²±è
                                // ±¸ÀÔ ¿Í °°ÀÌ¾¸
                                svMain.AddUserLog("9\09" + receivewho.MapName + "\09" + receivewho.CX.ToString() + "\09" + receivewho.CY.ToString() + "\09" + receivewho.UserName + "\09" + iname + "\09" + pu.MakeIndex.ToString() + "\09" + "1\09" + this.UserName);
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
                            if (svMain.UserEngine.CopyToUserItemFromName(iname, ref pu))
                            {
                                pstd2 = svMain.UserEngine.GetStdItem(svMain.UserEngine.GetStdItemIndex(iname));
                                if (pstd2 != null)
                                {
                                    if (pstd2.OverlapItem >= 1)
                                    {
                                        pu.Dura = (ushort)count;
                                        // gadget:Ä«¿îÆ®¾ÆÀÌÅÛ
                                    }
                                    // ·Î±×³²±è
                                    // ±¸ÀÔ ¿Í °°ÀÌ¾¸
                                    svMain.AddUserLog("9\09" + receivewho.MapName + "\09" + receivewho.CX.ToString() + "\09" + receivewho.CY.ToString() + "\09" + receivewho.UserName + "\09" + iname + "\09" + pu.MakeIndex.ToString() + "\09" + "1\09" + this.UserName);
                                    receivewho.DropItemDown(pu, 3, false, receivewho, null, 2);
                                    // ÀÏÁ¤½Ã°£µ¿¾È ´Ù¸¥ »ç¶÷ÀÌ ¸ø ÁÝ°Ô
                                }
                                // if pstd2 <> nil then
                            }
                            if (pu != null)
                            {
                                Dispose(pu);
                            }
                            // Memory Leak sonmg
                        }
                    }
                }
            }
        }

        public bool NpcSayTitle_DoActionList(TCreature who, ArrayList alist)
        {
            bool result;
            int i;
            int k;
            int n;
            int n1;
            int n2;
            int ixx;
            int iyy;
            int param;
            int tag;
            int iparam1;
            int iparam2;
            int iparam3;
            int iparam4;
            string sparam1 = String.Empty;
            string sparam2 = String.Empty;
            string sparam3 = String.Empty;
            string sparam4 = String.Empty;
            TQuestActionInfo pqa;
            ArrayList list;
            TUserHuman hum;
            TEnvirnoment envir;
            iparam2 = 0;
            iparam3 = 0;
            result = true;
            for (i = 0; i < alist.Count; i++)
            {
                pqa = alist[i] as TQuestActionInfo;
                switch (pqa.ActIdent)
                {
                    case Grobal2.QA_SET:
                        param = HUtil32.Str_ToInt(pqa.ActParam, 0);
                        // if param > 100 then begin
                        tag = HUtil32.Str_ToInt(pqa.ActTag, 0);
                        who.SetQuestMark(param, tag);
                        break;
                    case Grobal2.QA_OPENUNIT:
                        // end;
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
                        NpcSayTitle_TakeItemFromUser(pqa.ActParam, pqa.ActTagVal);
                        break;
                    case Grobal2.QA_TAKEW:
                        NpcSayTitle_TakeWItemFromUser(pqa.ActParam, pqa.ActTagVal);
                        break;
                    case Grobal2.QA_GIVE:
                        NpcSayTitle_GiveItemToUser(who, pqa.ActParam, pqa.ActTagVal);
                        break;
                    case Grobal2.QA_CLOSE:
                        // ´ëÈ­Ã¢À» ´ÝÀ½
                        who.SendMsg(this, Grobal2.RM_MERCHANTDLGCLOSE, 0, this.ActorId, 0, 0, "");
                        break;
                    case Grobal2.QA_CLOSENOINVEN:
                        // ´ëÈ­Ã¢À» ´ÝÀ½(ÀÎº¥Ã¢Àº °Çµå¸®Áö ¾ÊÀ½)
                        who.SendMsg(this, Grobal2.RM_MERCHANTDLGCLOSE, 0, this.ActorId, 1, 0, "");
                        break;
                    case Grobal2.QA_RESET:
                        // if pqa.ActParamVal > 100 then  //100¹Ì¸¸Àº ¸®·¿ÇÒ ¼ö ¾ø´Ù
                        for (k = 0; k < pqa.ActTagVal; k++)
                        {
                            who.SetQuestMark(pqa.ActParamVal + k, 0);
                        }
                        break;
                    case Grobal2.QA_RESETUNIT:
                        break;
                    case Grobal2.QA_MAPMOVE:
                        // for k:=0 to pqa.ActTagVal-1 do
                        // if pqa.ActParamVal + k <= 100 then  //100ÀÌÇÏ¸¸ ¸®¼Â µÊ
                        // who.SetQuestMark (pqa.ActParamVal + k, 0);
                        // ÀÚÀ¯ÀÌµ¿ ½ÃÄÑÁÜ
                        who.SendRefMsg(Grobal2.RM_SPACEMOVE_HIDE, 0, 0, 0, 0, "");
                        who.SpaceMove(pqa.ActParam, (short)pqa.ActTagVal, (short)pqa.ActExtraVal, 0);
                        bosaynow = true;
                        break;
                    case Grobal2.QA_MAPRANDOM:
                        who.SendRefMsg(Grobal2.RM_SPACEMOVE_HIDE, 0, 0, 0, 0, "");
                        who.RandomSpaceMove(pqa.ActParam, 0);
                        bosaynow = true;
                        break;
                    case Grobal2.QA_BREAK:
                        result = false;
                        break;
                    case Grobal2.QA_TIMERECALL:
                        // break;
                        ((TUserHuman)who).BoTimeRecall = true;
                        ((TUserHuman)who).TimeRecallMap = ((TUserHuman)who).MapName;
                        ((TUserHuman)who).TimeRecallX = ((TUserHuman)who).CX;
                        ((TUserHuman)who).TimeRecallY = ((TUserHuman)who).CY;
                        ((TUserHuman)who).TimeRecallEnd = GetTickCount + pqa.ActParamVal * 60 * 1000;
                        break;
                    case Grobal2.QA_TIMERECALLGROUP:
                        for (k = 0; k < who.GroupMembers.Count; k++)
                        {
                            hum = svMain.UserEngine.GetUserHuman(who.GroupMembers[k]);
                            if (hum != null)
                            {
                                hum.BoTimeRecall = false;
                                // °³ÀÎ TimeRecallÀº ÇØÁ¦
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
                        if (pcheckitem != null)
                        {
                            who.DeletePItemAndSend(pcheckitem);
                        }
                        break;
                    case Grobal2.QA_MONGEN:
                        for (k = 0; k < pqa.ActTagVal; k++)
                        {
                            ixx = iparam2 - pqa.ActExtraVal + new System.Random(pqa.ActExtraVal * 2 + 1).Next();
                            iyy = iparam3 - pqa.ActExtraVal + new System.Random(pqa.ActExtraVal * 2 + 1).Next();
                            svMain.UserEngine.AddCreatureSysop(sparam1, ixx, iyy, pqa.ActParam);
                        }
                        break;
                    case Grobal2.QA_MONCLEAR:
                        list = new ArrayList();
                        svMain.UserEngine.GetMapMons(svMain.GrobalEnvir.GetEnvir(pqa.ActParam), list);
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
                                    svMain.GrobalQuestParams[n - 100] = pqa.ActTagVal;
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
                                        svMain.GrobalQuestParams[n - 100] = svMain.GrobalQuestParams[n - 100] + pqa.ActTagVal;
                                    }
                                    else
                                    {
                                        svMain.GrobalQuestParams[n - 100] = svMain.GrobalQuestParams[n - 100] + 1;
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
                                        svMain.GrobalQuestParams[n - 100] = svMain.GrobalQuestParams[n - 100] - pqa.ActTagVal;
                                    }
                                    else
                                    {
                                        svMain.GrobalQuestParams[n - 100] = svMain.GrobalQuestParams[n - 100] - 1;
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
                                    n1 = svMain.GrobalQuestParams[n - 100];
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
                                    n2 = svMain.GrobalQuestParams[n - 100];
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
                                    svMain.GrobalQuestParams[9] = svMain.GrobalQuestParams[9] + n1 + n2;
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
                                    svMain.GrobalQuestParams[n - 100] = new System.Random(pqa.ActTagVal).Next();
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
                        // ºÎ¸¦ »ç¶÷
                        envir = svMain.GrobalEnvir.GetEnvir(pqa.ActParam);
                        if (envir != null)
                        {
                            list = new ArrayList();
                            svMain.UserEngine.GetAreaUsers(envir, 0, 0, 1000, list);
                            if (list.Count > 0)
                            {
                                // ÇÑ¸í¸¸ ¼±ÅÃ
                                hum = (TUserHuman)list[0];
                                if (hum != null)
                                {
                                    hum.RandomSpaceMove(this.MapName, 0);
                                }
                            }
                            list.Free();
                        }
                        // ³ªµµ ÀÌµ¿
                        who.RandomSpaceMove(pqa.ActParam, 0);
                        break;
                    case Grobal2.QA_RECALLMAP:
                        // ºÎ¸¦ »ç¶÷
                        envir = svMain.GrobalEnvir.GetEnvir(pqa.ActParam);
                        if (envir != null)
                        {
                            list = new ArrayList();
                            svMain.UserEngine.GetAreaUsers(envir, 0, 0, 1000, list);
                            for (k = 0; k < list.Count; k++)
                            {
                                hum = (TUserHuman)list[k];
                                if (hum != null)
                                {
                                    hum.RandomSpaceMove(this.MapName, 0);
                                }
                                // 22¸í Á¦ÇÑ
                                if (k > 20)
                                {
                                    break;
                                }
                            }
                            list.Free();
                        }
                        break;
                    case Grobal2.QA_BATCHDELAY:
                        batchdelay = pqa.ActParamVal * 1000;
                        break;
                    case Grobal2.QA_ADDBATCH:
                        batchlist.Add(pqa.ActParam, batchdelay as Object);
                        break;
                    case Grobal2.QA_BATCHMOVE:
                        for (k = 0; k < batchlist.Count; k++)
                        {
                            who.SendDelayMsg(this, Grobal2.RM_RANDOMSPACEMOVE, 0, 0, 0, 0, batchlist[k], previousbatchdelay + ((int)batchlist.Values[k]));
                            previousbatchdelay = previousbatchdelay + ((int)batchlist.Values[k]);
                        }
                        break;
                    case Grobal2.QA_PLAYDICE:
                        who.SendMsg(this, Grobal2.RM_PLAYDICE, (ushort)pqa.ActParamVal, HUtil32.MakeLong(MakeWord(((TUserHuman)who).DiceParams[0], ((TUserHuman)who).DiceParams[1]), MakeWord(((TUserHuman)who).DiceParams[2], ((TUserHuman)who).DiceParams[3])), HUtil32.MakeLong(MakeWord(((TUserHuman)who).DiceParams[4], ((TUserHuman)who).DiceParams[5]), MakeWord(((TUserHuman)who).DiceParams[6], ((TUserHuman)who).DiceParams[7])), HUtil32.MakeLong(MakeWord(((TUserHuman)who).DiceParams[8], ((TUserHuman)who).DiceParams[9]), 0), pqa.ActTag);
                        bosaynow = true;
                        break;
                    case Grobal2.QA_PLAYROCK:
                        who.SendMsg(this, Grobal2.RM_PLAYROCK, (ushort)pqa.ActParamVal, HUtil32.MakeLong(MakeWord(((TUserHuman)who).DiceParams[0], ((TUserHuman)who).DiceParams[1]), MakeWord(((TUserHuman)who).DiceParams[2], ((TUserHuman)who).DiceParams[3])), HUtil32.MakeLong(MakeWord(((TUserHuman)who).DiceParams[4], ((TUserHuman)who).DiceParams[5]), MakeWord(((TUserHuman)who).DiceParams[6], ((TUserHuman)who).DiceParams[7])), HUtil32.MakeLong(MakeWord(((TUserHuman)who).DiceParams[8], ((TUserHuman)who).DiceParams[9]), 0), pqa.ActTag);
                        bosaynow = true;
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
                        NpcSayTitle_TakeEventGradeItemFromUser(pqa.ActParamVal);
                        break;
                    case Grobal2.QA_GOTOQUEST:
                        NpcSayTitle_GotoQuest(pqa.ActParamVal);
                        break;
                    case Grobal2.QA_ENDQUEST:
                        ((TUserHuman)who).CurQuest = null;
                        break;
                    case Grobal2.QA_GOTO:
                        NpcSayTitle_GotoSay(pqa.ActParam);
                        break;
                    case Grobal2.QA_SOUND:
                        who.SendMsg(this, Grobal2.RM_SOUND, 0, HUtil32.Str_ToInt(pqa.ActParam, 0), 0, 0, "");
                        break;
                    case Grobal2.QA_SOUNDALL:
                        // ½Ã°£ÀÌ Áö³ª¸é ÇÃ·¡±× Reset
                        // 25ÃÊ
                        if (GetTickCount - SoundStartTime > 25 * 1000)
                        {
                            SoundStartTime = GetTickCount;
                            BoSoundPlaying = false;
                        }
                        if (!BoSoundPlaying)
                        {
                            BoSoundPlaying = true;
                            // »ç¿îµå ÇÃ·¹ÀÌ ¿äÃ»
                            this.SendRefMsg(Grobal2.RM_DIGUP, this.Dir, this.CX, this.CY, 0, "");
                        }
                        break;
                    case Grobal2.QA_CHANGEGENDER:
                        ((TUserHuman)who).CmdChangeSex();
                        if (who.Sex == 1)
                        {
                            who.BoxMsg("´ÓÄÐÈË±äÐÔµ½Å®ÈË£»\\ÄãÐèÒª·µ»ØÈËÎïÑ¡Ôñ½çÃæÖØÐÂÁ¬½Ó²ÅÄÜ¿´µ½ÄãµÄÐÂÃæ¿×¡£", 1);
                            who.SysMsg("´ÓÄÐÈË±äÐÔµ½Å®ÈË£»\\ÄãÐèÒª·µ»ØÈËÎïÑ¡Ôñ½çÃæÖØÐÂÁ¬½Ó²ÅÄÜ¿´µ½ÄãµÄÐÂÃæ¿×¡£", 1);
                        }
                        else
                        {
                            who.BoxMsg("´ÓÅ®ÈË±äÐÔµ½ÄÐÈË£»\\ÄãÐèÒª·µ»ØÈËÎïÑ¡Ôñ½çÃæÖØÐÂÁ¬½Ó²ÅÄÜ¿´µ½ÄãµÄÐÂÃæ¿×¡£", 1);
                            who.SysMsg("´ÓÅ®ÈË±äÐÔµ½ÄÐÈË£»\\ÄãÐèÒª·µ»ØÈËÎïÑ¡Ôñ½çÃæÖØÐÂÁ¬½Ó²ÅÄÜ¿´µ½ÄãµÄÐÂÃæ¿×¡£", 1);
                        }
                        break;
                    case Grobal2.QA_KICK:
                        ((TUserHuman)who).EmergencyClose = true;
                        break;
                    case Grobal2.QA_MOVEALLMAP:
                        param = pqa.ActTagVal;
                        // ÀÌµ¿ ½ÃÅ³ »ç¶÷
                        envir = svMain.GrobalEnvir.GetEnvir(this.MapName);
                        if (envir != null)
                        {
                            list = new ArrayList();
                            svMain.UserEngine.GetAreaUsers(envir, 0, 0, 1000, list);
                            for (k = 0; k < list.Count; k++)
                            {
                                hum = (TUserHuman)list[k];
                                if (hum != null)
                                {
                                    hum.RandomSpaceMove(pqa.ActParam, 0);
                                }
                                // param¸í Á¦ÇÑ
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
                            // ±×·ìÀÌ ¾ø°í ÀÚ±â È¥ÀÚ¸¸ ÀÖÀ» ¶§
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
                            // ÀÚ½ÅÀÌ ±×·ìÂ¯
                            for (k = 0; k < who.GroupMembers.Count; k++)
                            {
                                // ÀÚ½Å Æ÷ÇÔ.
                                hum = svMain.UserEngine.GetUserHuman(who.GroupMembers[k]);
                                if (hum != null)
                                {
                                    // ÇöÀç ¸Ê¿¡ ÀÖ´Â »ç¶÷¸¸ ÁöÁ¤ ¸ÊÀ¸·Î ÀÌµ¿.
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
                        n = (int)((GetTickCount - who.CGHIstart) / 1000);
                        who.CGHIstart = who.CGHIstart + ((long)n * 1000);
                        if (who.CGHIUseTime > n)
                        {
                            who.CGHIUseTime = (ushort)(who.CGHIUseTime - n);
                        }
                        else
                        {
                            who.CGHIUseTime = 0;
                        }
                        if (who.CGHIUseTime == 0)
                        {
                            if (who.GroupOwner == who)
                            {
                                // ÀÚ½ÅÀÌ ±×·ìÂ¯
                                for (k = 1; k < who.GroupMembers.Count; k++)
                                {
                                    // ÀÚ½Å »©°í
                                    // ÁöÁ¤ÇÑ ¸Ê¿¡ ÀÖ´Â »ç¶÷¸¸ ¼ÒÈ¯
                                    ((TUserHuman)who).CmdRecallMan(who.GroupMembers[k], pqa.ActParam);
                                }
                                who.CGHIstart = GetTickCount;
                                who.CGHIUseTime = 10;
                                // 10ÃÊ °£°Ý
                            }
                        }
                        else
                        {
                            who.SysMsg(who.CGHIUseTime.ToString() + "ÃëºóÔÙÊ¹ÓÃ¡£", 0);
                        }
                        break;
                    case Grobal2.QA_WEAPONUPGRADE:
                        if ((pqa.ActParam == "DC") || (pqa.ActParam == "¹¥»÷"))
                        {
                            ((TUserHuman)who).CmdRefineWeapon(pqa.ActTagVal, 0, 0, 0);
                        }
                        else if ((pqa.ActParam == "MC") || (pqa.ActParam == "Ä§·¨"))
                        {
                            ((TUserHuman)who).CmdRefineWeapon(0, pqa.ActTagVal, 0, 0);
                        }
                        else if ((pqa.ActParam == "SC") || (pqa.ActParam == "µÀÊõ"))
                        {
                            ((TUserHuman)who).CmdRefineWeapon(0, 0, pqa.ActTagVal, 0);
                        }
                        else if ((pqa.ActParam == "ACC") || (pqa.ActParam == "×¼È·"))
                        {
                            ((TUserHuman)who).CmdRefineWeapon(0, 0, 0, pqa.ActTagVal);
                        }
                        break;
                    case Grobal2.QA_SETALLINMAP:
                        param = HUtil32.Str_ToInt(pqa.ActParam, 0);
                        tag = HUtil32.Str_ToInt(pqa.ActTag, 0);
                        // ¸Ê¿¡ ÀÖ´Â ¸ðµç »ç¶÷
                        envir = svMain.GrobalEnvir.GetEnvir(this.MapName);
                        if (envir != null)
                        {
                            list = new ArrayList();
                            svMain.UserEngine.GetAreaUsers(envir, 0, 0, 1000, list);
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
                        // ¿¬ÀÎ ¾ÕÀ¸·Î ÀÌµ¿
                        if (((TUserHuman)who).fLover != null)
                        {
                            if (((TUserHuman)who).fLover.GetLoverName != "")
                            {
                                ((TUserHuman)who).CmdCharSpaceMove(((TUserHuman)who).fLover.GetLoverName);
                            }
                        }
                        break;
                    case Grobal2.QA_BREAKLOVER:
                        // ¿¬ÀÎ °ü°è ÀÏ¹æ ÇØÁ¦
                        ((TUserHuman)who).CmdBreakLoverRelation();
                        break;
                    case Grobal2.QA_DECDONATION:
                        // Àå¿ø±âºÎ±Ý
                        ((TUserHuman)who).DecGuildAgitDonation(pqa.ActParamVal);
                        break;
                    case Grobal2.QA_SHOWEFFECT:
                        // Àå¿øÀÌÆåÆ®
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
                                // ÀÌÆåÆ® Á¾·ù
                                who.SendRefMsg(Grobal2.RM_LOOPNORMALEFFECT, (ushort)this.ActorId, tag, 0, Grobal2.NE_JW_EFFECT1, "");
                                break;
                            default:
                                who.SendRefMsg(Grobal2.RM_LOOPNORMALEFFECT, (ushort)this.ActorId, tag, 0, Grobal2.NE_JW_EFFECT1, "");
                                break;
                        }
                        break;
                    case Grobal2.QA_MONGENAROUND:
                        // Ä³¸¯ ÁÖÀ§¿¡ ¸ó½ºÅÍ Á¨
                        for (ixx = who.CX - 2; ixx <= who.CX + 2; ixx++)
                        {
                            for (iyy = who.CY - 2; iyy <= who.CY + 2; iyy++)
                            {
                                // sparam1 : map
                                if (sparam1 == "")
                                {
                                    sparam1 = who.MapName;
                                }
                                if (((Math.Abs(who.CX - ixx) == 2) || (Math.Abs(who.CY - iyy) == 2)) && (Math.Abs(who.CX - ixx) % 2 == 0) && (Math.Abs(who.CY - iyy) % 2 == 0))
                                {
                                    // ¸ÊÄù½ºÆ®¸¦ À§ÇØ PEnvir ´ë½Å¿¡ who.PEnvir¸¦ »ç¿ëÇÑ´Ù.
                                    if (who.PEnvir.CanWalk(ixx, iyy, false))
                                    {
                                        // map
                                        svMain.UserEngine.AddCreatureSysop(sparam1, ixx, iyy, pqa.ActParam);
                                        // mon-name
                                    }
                                }
                            }
                        }
                        break;
                    case Grobal2.QA_RECALLMOB:
                        // ¸÷ÀÌ¸§
                        // ¸¶¸®¼ö
                        ((TUserHuman)who).CmdCallMakeSlaveMonster(pqa.ActParam, pqa.ActTag, 3, 0);
                        break;
                    case Grobal2.QA_SETLOVERFLAG:
                        if (((TUserHuman)who).fLover != null)
                        {
                            hum = svMain.UserEngine.GetUserHuman(((TUserHuman)who).fLover.GetLoverName);
                            if (hum != null)
                            {
                                param = HUtil32.Str_ToInt(pqa.ActParam, 0);
                                // if param > 100 then begin
                                tag = HUtil32.Str_ToInt(pqa.ActTag, 0);
                                hum.SetQuestMark(param, tag);
                                // end;
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
                            hum = svMain.UserEngine.GetUserHuman(((TUserHuman)who).fLover.GetLoverName);
                            if (hum != null)
                            {
                                if ((Math.Abs(hum.CX - who.CX) <= 7) && (Math.Abs(hum.CY - who.CY) <= 7))
                                {
                                    NpcSayTitle_GiveItemToUser(hum, pqa.ActParam, pqa.ActTagVal);
                                }
                                else
                                {
                                    who.SysMsg("ÄãµÄÇéÂÂ²»ÔÚÕâÀï¡£", 0);
                                }
                            }
                            else
                            {
                                who.SysMsg("ÎÞ·¨ÕÒµ½ÄãµÄÇéÂÂ¡£", 0);
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
                        who.SysMsg(Format("[ÕË»§ÐÅÏ¢]µ±Ç°ÕË»§³äÖµÊ£ÓàÊ±¼ä%dÃë", new int[] { ((TUserHuman)who).SecondsCard }), 0);
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
            int k;
            string tag = String.Empty;
            string rst = String.Empty;
            rst = str;
            for (k = 0; k <= 100; k++)
            {
                if (CharCount(rst, ">") >= 1)
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

        public void NpcSayTitle(TCreature who, string title)
        {
            ArrayList batchlist;
            bool bosaynow;
            int i;
            int j;
            int m;
            string str;
            TQuestRecord pquest;
            TQuestRecord pqr;
            TSayingRecord psay;
            TSayingProcedure psayproc;
            pquest = null;
            psayproc = null;
            batchlist = new ArrayList();
            if (((TUserHuman)who).CurQuestNpc != this)
            {
                ((TUserHuman)who).CurQuestNpc = null;
                ((TUserHuman)who).CurQuest = null;
                FillChar(((TUserHuman)who).QuestParams, sizeof(int) * 10, '\0');
            }
            if (title.ToLower().CompareTo("@main".ToLower()) == 0)
            {
                for (i = 0; i < Sayings.Count; i++)
                {
                    pqr = Sayings[i] as TQuestRecord;
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
                        // Äù½ºÆ®ÀÇ Á¶°ÇÀ» °Ë»ç ÇÑ´Ù.
                        if (NpcSayTitle_CheckQuestCondition(Sayings[i] as TQuestRecord))
                        {
                            pquest = Sayings[i] as TQuestRecord;
                            ((TUserHuman)who).CurQuest = pquest;
                            ((TUserHuman)who).CurQuestNpc = this;
                        }
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
                            // 2003-09-08 nil °Ë»ç
                            bosaynow = false;
                            if (NpcSayTitle_CheckSayingCondition(psayproc.ConditionList))
                            {
                                // Á¶°Ç ÂüÀÎ °æ¿ì, ´ëÈ­
                                str = str + psayproc.Saying;
                                // Á¶°Ç ÂüÀÎ °æ¿ì, ¾×¼Ç
                                if (!NpcSayTitle_DoActionList(psayproc.ActionList))
                                {
                                    break;
                                }
                                if (bosaynow)
                                {
                                    NpcSayTitle_NpcSayProc(str, true);
                                    ((TUserHuman)who).CurSayProc = psayproc;
                                }
                            }
                            else
                            {
                                // Á¶°Ç °ÅÁþÀÎ °æ¿ì, ´ëÈ­
                                str = str + psayproc.ElseSaying;
                                // Á¶°Ç °ÅÁþÀÎ °æ¿ì, ¾×¼Ç
                                if (!NpcSayTitle_DoActionList(psayproc.ElseActionList))
                                {
                                    break;
                                }
                                if (bosaynow)
                                {
                                    NpcSayTitle_NpcSayProc(str, true);
                                    ((TUserHuman)who).CurSayProc = psayproc;
                                }
                            }
                        }
                        if (str != "")
                        {
                            NpcSayTitle_NpcSayProc(str, false);
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

        // »óÀÎÀÌ ÇÒ ¼ö ÀÖ´Â ±â´É Á¦¾î,  ÆÇ¸Å, ±¸ÀÔ, ¸Ã±â±â µî...
        public virtual void UserCall(TCreature caller)
        {
        }

        public virtual void UserSelect(TCreature whocret, string selstr)
        {

        }

    }

    public class TMerchant : TNormNpc
    {
        // ÆÇ¸Å¸¸ ÇÏ´Â »óÀÎ
        public string MarketName = String.Empty;
        public byte MarketType = 0;
        // RepairItem: byte;  //0:¾ÈÇÔ,  1:ÇÔ
        // StorageItem: byte;
        public int PriceRate = 0;
        // ¹°°¡, 100:º¸Åë, 100º¸´Ù Å©¸é ºñ½Î´Ù.
        public bool NoSeal = false;
        public bool BoCastleManage = false;
        public bool BoHiddenNpc = false;
        public int fSaveToFileCount = 0;
        public int CreateIndex = 0;
        private long checkrefilltime = 0;
        private long checkverifytime = 0;
        public ArrayList DealGoods = null;
        public ArrayList ProductList = null;
        public IList<IList<TUserItem>> GoodsList = null;
        public ArrayList PriceList = null;
        public IList<TUpgradeInfo> UpgradingList = null;

        public TMerchant() : base()
        {
            this.RaceImage = Grobal2.RCC_MERCHANT;
            this.Appearance = 0;
            PriceRate = 100;
            NoSeal = false;
            BoCastleManage = false;
            BoHiddenNpc = false;
            DealGoods = new ArrayList();
            ProductList = new ArrayList();
            GoodsList = new List<IList<TUserItem>>();
            PriceList = new ArrayList();
            UpgradingList = new List<TUpgradeInfo>();
            checkrefilltime = GetTickCount;
            checkverifytime = GetTickCount;
            fSaveToFileCount = 0;
            CreateIndex = 0;
        }

        ~TMerchant()
        {
            int i;
            int k;
            ArrayList list;
            for (i = 0; i < ProductList.Count; i++)
            {
                Dispose((TMarketProduct)ProductList[i]);
            }
            ProductList.Free();
            for (i = 0; i < GoodsList.Count; i++)
            {
                list = GoodsList[i] as ArrayList;
                for (k = 0; k < list.Count; k++)
                {
                    Dispose((TUserItem)list[k]);
                }
                list.Free();
            }
            GoodsList.Free();
            for (i = 0; i < PriceList.Count; i++)
            {
                Dispose((TPricesInfo)PriceList[i]);
            }
            PriceList.Free();
            for (i = 0; i < UpgradingList.Count; i++)
            {
                Dispose(UpgradingList[i]);
            }
            UpgradingList.Free();
            base.Destroy();
        }
        public void ClearMerchantInfos()
        {
            int i;
            for (i = 0; i < ProductList.Count; i++)
            {
                Dispose((TMarketProduct)ProductList[i]);
            }
            ProductList.Clear();
            DealGoods.Clear();
            // inherited
            this.ClearNpcInfos();
            // °øÅëÀ¸·Î »ç¿ë

        }

        public void LoadMerchantInfos()
        {
            this.NpcBaseDir = LocalDB.MARKETDEFDIR;
            LocalDB.FrmDB.LoadMarketDef(this, LocalDB.MARKETDEFDIR, MarketName + "-" + this.MapName, true);
            // ArrangeSayStrings;
            // ///////////*****************
            // for i:=0 to SayStrings.Count-1 do begin
            // if CompareText(SayStrings[i], '@makedrug') = 0 then begin
            // NoSeal := TRUE;  //¹°°ÇÀ» ¸¸µå´Â °÷¿¡¼­´Â ÆÇ¸Å´Â ¾ÈÇÔ.
            // break;
            // end;
            // end;

        }

        public void LoadMarketSavedGoods()
        {
            LocalDB.FrmDB.LoadMarketSavedGoods(this, MarketName + "-" + this.MapName);
            LocalDB.FrmDB.LoadMarketPrices(this, MarketName + "-" + this.MapName);
            LoadUpgradeItemList();
        }

        private IList<TUserItem> GetGoodsList(int gindex)
        {
            IList<TUserItem> result = null;
            IList<TUserItem> l;
            if (gindex > 0)
            {
                try
                {
                    for (var i = 0; i < GoodsList.Count; i++)
                    {
                        l = GoodsList[i];
                        if (l.Count > 0)
                        {
                            if (l[0].Index == gindex)
                            {
                                result = l;
                                break;
                            }
                        }
                    }
                }
                catch
                {
                }
            }
            return result;
        }

        public void PriceUp(int index)
        {
            for (var i = 0; i < PriceList.Count; i++)
            {
                if (((TPricesInfo)PriceList[i]).Index == index)
                {
                    int price = ((TPricesInfo)PriceList[i]).SellPrice;
                    if (price < HUtil32.MathRound(price * 1.1))
                    {
                        price = HUtil32.MathRound(price * 1.1);
                    }
                    else
                    {
                        price = price + 1;
                    }
                    return;
                }
            }
            TStdItem pstd = svMain.UserEngine.GetStdItem(index);
            if (pstd != null)
            {
                NewPrice(index, HUtil32.MathRound(pstd.Price * 1.1));
            }
        }

        public void PriceDown(int index)
        {
            decimal price;
            for (var i = 0; i < PriceList.Count; i++)
            {
                if (((TPricesInfo)PriceList[i]).Index == index)
                {
                    price = ((TPricesInfo)PriceList[i]).SellPrice;
                    if (price > (decimal)HUtil32.MathRound(price / 1.1))
                    {
                        price = (decimal)HUtil32.MathRound(price / 1.1);
                    }
                    else
                    {
                        price = price - 1;
                    }
                    price = _MAX(2, (int)price);
                    return;
                }
            }
            TStdItem pstd = svMain.UserEngine.GetStdItem(index);
            if (pstd != null)
            {
                NewPrice(index, HUtil32.MathRound(pstd.Price * 1.1));
            }
        }

        public void NewPrice(int index, decimal price)
        {
            TPricesInfo pi = new TPricesInfo();
            pi.Index = (ushort)index;
            pi.SellPrice = (int)price;
            PriceList.Add(pi);
            LocalDB.FrmDB.WriteMarketPrices(this, MarketName + "-" + this.MapName);
        }

        // ¹°°ÇÀÇ ´ëÇ¥ °¡°Ý
        public int GetPrice(int index)
        {
            int price = -2;
            for (var i = 0; i < PriceList.Count; i++)
            {
                if (((TPricesInfo)PriceList[i]).Index == index)
                {
                    price = ((TPricesInfo)PriceList[i]).SellPrice;
                    break;
                }
            }
            if (price < 0)
            {
                TStdItem pstd = svMain.UserEngine.GetStdItem(index);
                if ((pstd != null) && IsDealingItem(pstd.StdMode, pstd.Shape))
                {
                    price = pstd.Price;
                }
            }
            return price;
        }

        private int GetGoodsPrice(TUserItem uitem)
        {
            int result;
            int i;
            int price;
            int upg;
            double dam;
            TStdItem pstd;
            price = GetPrice(uitem.Index);
            if (price > 0)
            {
                pstd = svMain.UserEngine.GetStdItem(uitem.Index);
                if (pstd != null)
                {
                    if ((pstd.OverlapItem < 1) && (pstd.StdMode > 4) && (pstd.DuraMax > 0) && (uitem.DuraMax > 0) && (pstd.StdMode != 8))
                    {
                        if (pstd.StdMode == 40)
                        {
                            if (uitem.Dura <= uitem.DuraMax)
                            {
                                dam = price / 2 / uitem.DuraMax * (uitem.DuraMax - uitem.Dura);
                                price = _MAX(2, HUtil32.MathRound(price - dam));
                            }
                            else
                            {
                                price = price + HUtil32.MathRound((uitem.Dura - uitem.DuraMax) * price / uitem.DuraMax * 2);
                            }
                        }
                        if (pstd.StdMode == 43)
                        {
                            if (uitem.DuraMax < 10000)
                            {
                                uitem.DuraMax = 10000;
                            }
                            if (uitem.Dura <= uitem.DuraMax)
                            {
                                dam = price / 2 / uitem.DuraMax * (uitem.DuraMax - uitem.Dura);
                                price = _MAX(2, HUtil32.MathRound(price - dam));
                            }
                            else
                            {
                                price = price + HUtil32.MathRound((uitem.Dura - uitem.DuraMax) * (price / uitem.DuraMax * 1.3));
                            }
                        }
                        if ((pstd.OverlapItem < 1) && (pstd.StdMode > 4))
                        {
                            upg = 0;
                            for (i = 0; i <= 7; i++)
                            {
                                if ((pstd.StdMode == 5) || (pstd.StdMode == 6))
                                {
                                    if ((i == 4) || (i == 9))
                                    {
                                        continue;
                                    }
                                    if (i == 6)
                                    {
                                        if (uitem.Desc[i] > 10)
                                        {
                                            upg = upg + (uitem.Desc[i] - 10) * 2;
                                        }
                                        continue;
                                    }
                                    upg = upg + uitem.Desc[i];
                                }
                                else
                                {
                                    upg = upg + uitem.Desc[i];
                                }
                            }
                            if (upg > 0)
                            {
                                price = price + price / 5 * upg;
                            }
                            price = HUtil32.MathRound(price / pstd.DuraMax * uitem.DuraMax);
                            dam = price / 2 / uitem.DuraMax * (uitem.DuraMax - uitem.Dura);
                            price = _MAX(2, HUtil32.MathRound(price - dam));
                        }
                    }
                }
            }
            result = price;
            return result;
        }

        private int GetSellPrice(TUserHuman whocret, int price)
        {
            int result;
            int prate;
            if (BoCastleManage)
            {
                if (svMain.UserCastle.IsOurCastle((TGuild)whocret.MyGuild))
                {
                    prate = _MAX(60, HUtil32.MathRound(PriceRate * 0.8));
                    result = HUtil32.MathRound(price / 100 * prate);
                }
                else
                {
                    result = HUtil32.MathRound(price / 100 * PriceRate);
                }
            }
            else
            {
                result = HUtil32.MathRound(price / 100 * PriceRate);
            }
            return result;
        }

        private decimal GetBuyPrice(int price)
        {
            return HUtil32.MathRound(price / 2);
        }

        public bool IsDealingItem(int stdmode, int shape)
        {
            bool result;
            int i;
            int _stdmode;
            int _shape;
            string str1 = String.Empty;
            string str2 = String.Empty;
            result = false;
            for (i = 0; i < DealGoods.Count; i++)
            {
                str2 = HUtil32.GetValidStr3((string)DealGoods[i], ref str1, new string[] { ",", " " });
                _stdmode = HUtil32.Str_ToInt(str1, -1);
                _shape = HUtil32.Str_ToInt(str2, -1);
                if (_stdmode == stdmode)
                {
                    if (_shape != -1)
                    {
                        if (_shape == shape)
                        {
                            result = true;
                            break;
                        }
                    }
                    else
                    {
                        result = true;
                        break;
                    }
                }
            }
            return result;
        }

        public void RefillGoods_RefillNow(ref IList<TUserItem> list, string itemname, int fcount)
        {
            if (list == null)
            {
                list = new List<TUserItem>();
                GoodsList.Add(list);
            }
            for (var i = 0; i < fcount; i++)
            {
                TUserItem pu = new TUserItem();
                if (svMain.UserEngine.CopyToUserItemFromName(itemname, ref pu))
                {
                    TStdItem ps = svMain.UserEngine.GetStdItem(pu.Index);
                    if (ps != null)
                    {
                        if (ps.OverlapItem >= 1)
                        {
                            pu.Dura = 1;
                        }
                        list.Insert(0, pu);
                    }
                    else
                    {
                        Dispose(pu);
                    }
                }
                else
                {
                    Dispose(pu);
                }
            }
        }

        public void RefillGoods_WasteNow(ref IList<TUserItem> list, int wcount)
        {
            for (var i = list.Count - 1; i >= 0; i--)
            {
                if (wcount <= 0)
                {
                    break;
                }
                try
                {
                    Dispose(list[i]);
                }
                finally
                {
                    list.RemoveAt(i);
                }
                wcount -= 1;
            }
        }

        private void RefillGoods()
        {
            int i;
            int j;
            int k;
            int stock;
            int gindex;
            TMarketProduct pp;
            IList<TUserItem> list;
            IList<TUserItem> l;
            bool flag;
            int step;
            bool ItemChanged;
            ItemChanged = false;
            i = 0;
            step = 0;
            try
            {
                step = 0;
                for (i = 0; i < ProductList.Count; i++)
                {
                    step = 1;
                    pp = (TMarketProduct)ProductList[i];
                    if (GetTickCount - pp.ZenTime > ((long)pp.ZenHour) * 60 * 1000)
                    {
                        step = 3;
                        pp.ZenTime = GetTickCount;
                        gindex = svMain.UserEngine.GetStdItemIndex(pp.GoodsName);
                        // ÀÌ¸§À¸·Î ¾ÆÀÌÅÛ ÀÎµ¦½º¸¦ ¾ò¾î¿È
                        if (gindex >= 0)
                        {
                            step = 4;
                            list = null;
                            list = GetGoodsList(gindex);
                            stock = 0;
                            if (list != null)
                            {
                                stock = list.Count;
                            }
                            if (stock < pp.Count)
                            {
                                // ¹°°ÇÀÌ ºÎÁ·
                                step = 5;
                                PriceUp(gindex);
                                RefillGoods_RefillNow(ref list, pp.GoodsName, pp.Count - stock);
                                // »õ·Î Ãß°¡´Â ¾Õ¿¡¼­ ºÎÅÍ
                                ItemChanged = true;
                                // ÀúÀå
                                LocalDB.FrmDB.WriteMarketSavedGoods(this, MarketName + "-" + this.MapName);
                                LocalDB.FrmDB.WriteMarketPrices(this, MarketName + "-" + this.MapName);
                                step = 6;
                            }
                            if (stock > pp.Count)
                            {
                                // ¹°°ÇÀÌ ³²¾Æ µ·´Ù. ¹ö¸°´Ù.
                                step = 7;
                                // ///PriceDown (gindex);
                                RefillGoods_WasteNow(ref list, stock - pp.Count);
                                // µÚ¿¡¼­ ºÎÅÍ ¹ö¸²
                                ItemChanged = true;
                                // ÀúÀå
                                LocalDB.FrmDB.WriteMarketSavedGoods(this, MarketName + "-" + this.MapName);
                                LocalDB.FrmDB.WriteMarketPrices(this, MarketName + "-" + this.MapName);
                                step = 8;
                            }
                        }
                    }
                }
                if (ItemChanged)
                {
                    // 10 ¹ø¿¡ ÇÑ¹ø¾¿ ÀúÀåÀ» ÇÏ°ÔÇÑ´Ù 5ºÐ x 10 = 50 ºÐ¿¡ ÇÑ¹ø¾¿ ÀúÀåµÊ
                    if (fSaveToFileCount >= 10)
                    {
                        LocalDB.FrmDB.WriteMarketSavedGoods(this, MarketName + "-" + this.MapName);
                        LocalDB.FrmDB.WriteMarketPrices(this, MarketName + "-" + this.MapName);
                        fSaveToFileCount = 0;
                    }
                    else
                    {
                        fSaveToFileCount++;
                    }
                }
                // ÀÌ»óÁ¡¿¡¼­ ³ªÁö´Â ¾ÊÁö¸¸ »çµéÀÎ ¹°°ÇÁß¿¡¼­ 1000°³ ÀÌ»óÀÌ¸é ¹ö¸°´Ù.
                // ÀÌ »óÁ¡¿¡¼­ ³ª´Â °ÍÀº 5000°³ ÀÌ»ó ¹ö¸²
                for (j = 0; j < GoodsList.Count; j++)
                {
                    step = 9;
                    l = GoodsList[j] as ArrayList;
                    step = 10;
                    if (l.Count > 1000)
                    {
                        // ÀÌ »óÁ¡¿¡¼­ ³ª´Â°ÍÀº Á¦°ÅÇÏÁö ¾ÊÀ½.
                        flag = false;
                        for (k = 0; k < ProductList.Count; k++)
                        {
                            step = 11;
                            pp = (TMarketProduct)ProductList[k];
                            gindex = svMain.UserEngine.GetStdItemIndex(pp.GoodsName);
                            // ÀÌ¸§À¸·Î ¾ÆÀÌÅÛ ÀÎµ¦½º¸¦ ¾ò¾î¿È
                            if (((TUserItem)l[0]).Index == gindex)
                            {
                                step = 12;
                                flag = true;
                                break;
                            }
                        }
                        step = 13;
                        if (!flag)
                        {
                            RefillGoods_WasteNow(ref l, l.Count - 1000);
                            // µÚ¿¡¼­ ºÎÅÍ ¹ö¸²
                        }
                        else
                        {
                            RefillGoods_WasteNow(ref l, l.Count - 5000);
                        }
                        // µÚ¿¡¼­ ºÎÅÍ ¹ö¸²
                    }
                }
            }
            catch
            {
                svMain.MainOutMessage("Merchant RefillGoods Exception..Step=(" + step.ToString() + ")");
            }
        }

        public override void CheckNpcSayCommand(TUserHuman hum, ref string source, string tag)
        {
            base.CheckNpcSayCommand(hum, ref source, tag);
            if (tag == "$PRICERATE")
            {
                source = this.ChangeNpcSayTag(source, "<$PRICERATE>", PriceRate.ToString());
            }
            if (tag == "$UPGRADEWEAPONFEE")
            {
                source = this.ChangeNpcSayTag(source, "<$UPGRADEWEAPONFEE>", ObjNpc.UPGRADEWEAPONFEE.ToString());
            }
            if (tag == "$USERWEAPON")
            {
                if (hum.UseItems[Grobal2.U_WEAPON].Index != 0)
                {
                    source = this.ChangeNpcSayTag(source, "<$USERWEAPON>", svMain.UserEngine.GetStdItemName(hum.UseItems[Grobal2.U_WEAPON].Index));
                }
                else
                {
                    source = this.ChangeNpcSayTag(source, "<$USERWEAPON>", "Weapon");
                }
            }
        }

        public override void UserCall(TCreature caller)
        {
            this.NpcSayTitle(caller, "@main");
        }

        private void SaveUpgradeItemList()
        {
            try
            {
                LocalDB.FrmDB.WriteMarketUpgradeInfos(this.UserName, UpgradingList);
            }
            catch
            {
                svMain.MainOutMessage("Failure in saving upgradinglist - " + this.UserName);
            }
        }

        private void LoadUpgradeItemList()
        {
            for (var i = 0; i < UpgradingList.Count; i++)
            {
                Dispose(UpgradingList[i]);
            }
            UpgradingList.Clear();
            try
            {
                LocalDB.FrmDB.LoadMarketUpgradeInfos(this.UserName, UpgradingList);
            }
            catch
            {
                svMain.MainOutMessage("Failure in loading upgradinglist - " + this.UserName);
            }
        }

        private void VerifyUpgradeList()
        {
            int i;
            int old;
            TUpgradeInfo pup;
            double realdate;
            old = 0;
            for (i = UpgradingList.Count - 1; i >= 0; i--)
            {
                pup = UpgradingList[i];
                // TO PDS: CHeck Null..
                if (pup != null)
                {
                    realdate = ((double)DateTime.Today) - ((double)pup.readydate);
                    try
                    {
                        // Round ½Ã¿¡ ¼ýÀÚ ÄÁ¹öÆÃ ¿¡·¯ ¹ß»ý PDS
                        old = HUtil32.MathRound(realdate);
                    }
                    catch (Exception)
                    {
                        old = 0;
                    }
                    if (old >= 8)
                    {
                        // 7+1ÀÏ ÀÌ»ó Áö³­ °Í
                        Dispose(pup);
                        UpgradingList.RemoveAt(i);
                    }
                }
                else
                {
                    svMain.MainOutMessage("pup Is Nil... ");
                }
            }
        }

        public void UserSelectUpgradeWeapon_PrepareWeaponUpgrade(IList<TUserItem> ilist, ref byte adc, ref byte asc, ref byte amc, ref byte dura)
        {
            int i;
            int k;
            int d;
            int s;
            int m;
            int dctop;
            int dcsec;
            int sctop;
            int scsec;
            int mctop;
            int mcsec;
            int durasum;
            int duracount;
            TStdItem ps;
            ArrayList dellist;
            ArrayList sumlist;
            TStdItem std;
            dctop = 0;
            dcsec = 0;
            sctop = 0;
            scsec = 0;
            mctop = 0;
            mcsec = 0;
            durasum = 0;
            duracount = 0;
            dellist = null;
            sumlist = new ArrayList();
            for (i = ilist.Count - 1; i >= 0; i--)
            {
                if (svMain.UserEngine.GetStdItemName(ilist[i].Index) == svMain.__BlackStone)
                {
                    sumlist.Add(HUtil32.MathRound(ilist[i].Dura / 1000));
                    // durasum := durasum +
                    // Inc (duracount);
                    if (dellist == null)
                    {
                        dellist = new ArrayList();
                    }
                    dellist.Add(svMain.__BlackStone, ilist[i].MakeIndex as Object);
                    Dispose(ilist[i]);
                    ilist.RemoveAt(i);
                }
                else
                {
                    if (M2Share.IsUpgradeWeaponStuff(ilist[i].Index))
                    {
                        ps = svMain.UserEngine.GetStdItem(ilist[i].Index);
                        if (ps != null)
                        {
                            std = ps;
                            svMain.ItemMan.GetUpgradeStdItem(ilist[i], ref std);
                            d = 0;
                            s = 0;
                            m = 0;
                            switch (std.StdMode)
                            {
                                case 19:
                                case 20:
                                case 21:
                                    // ¸ñ°ÉÀÌ
                                    d = Lobyte(std.DC) + HiByte(std.DC);
                                    s = Lobyte(std.SC) + HiByte(std.SC);
                                    m = Lobyte(std.MC) + HiByte(std.MC);
                                    break;
                                case 22:
                                case 23:
                                    // ¹ÝÁö
                                    d = Lobyte(std.DC) + HiByte(std.DC);
                                    s = Lobyte(std.SC) + HiByte(std.SC);
                                    m = Lobyte(std.MC) + HiByte(std.MC);
                                    break;
                                case 24:
                                case 26:
                                    // ÆÈÂî
                                    d = Lobyte(std.DC) + HiByte(std.DC) + 1;
                                    s = Lobyte(std.SC) + HiByte(std.SC) + 1;
                                    m = Lobyte(std.MC) + HiByte(std.MC) + 1;
                                    break;
                            }
                            if (dctop < d)
                            {
                                dcsec = dctop;
                                dctop = d;
                            }
                            else if (dcsec < d)
                            {
                                dcsec = d;
                            }
                            if (sctop < s)
                            {
                                scsec = sctop;
                                sctop = s;
                            }
                            else if (scsec < s)
                            {
                                scsec = s;
                            }
                            if (mctop < m)
                            {
                                mcsec = mctop;
                                mctop = m;
                            }
                            else if (mcsec < m)
                            {
                                mcsec = m;
                            }
                            if (dellist == null)
                            {
                                dellist = new ArrayList();
                            }
                            dellist.Add(ps.Name, ilist[i].MakeIndex as Object);
                            // ·Î±×³²±è
                            // ¾÷Àç_
                            svMain.AddUserLog("26\09" + hum.MapName + "\09" + hum.CX.ToString() + "\09" + hum.CY.ToString() + "\09" + hum.UserName + "\09" + svMain.UserEngine.GetStdItemName(ilist[i].Index) + "\09" + ilist[i].MakeIndex.ToString() + "\09" + "1\09" + this.ItemOptionToStr(ilist[i].Desc));
                            Dispose(ilist[i]);
                            ilist.RemoveAt(i);
                        }
                    }
                }
            }
            for (i = 0; i < sumlist.Count; i++)
            {
                for (k = sumlist.Count - 1; k > i; k--)
                {
                    if (((int)sumlist[k]) > ((int)sumlist[k - 1]))
                    {
                        sumlist.Exchange(k, k - 1);
                    }
                }
            }
            for (i = 0; i < sumlist.Count; i++)
            {
                durasum = durasum + ((int)sumlist[i]);
                duracount++;
                if (duracount >= 5)
                {
                    break;
                }
            }
            // ³»±¸ Æò±Õ, 5°³ ±îÁö ¸¹ÀÌ ³ÖÀ¸¸é ¾îµåº¥Å×Áö
            dura = (byte)HUtil32.MathRound(Convert.ToDouble(_MIN(5, duracount) + durasum / duracount / 5 * _MIN(5, duracount)));
            adc = (byte)(dctop + dctop / 5 + dcsec / 3);
            // ÆÄ±« 5ÀÌ»ó °¡ÁßÄ¡
            asc = (byte)(sctop + sctop / 5 + scsec / 3);
            amc = (byte)(mctop + mctop / 5 + mcsec / 3);
            if (dellist != null)
            {
                hum.SendMsg(hum, Grobal2.RM_DELITEMS, 0, (int)dellist, 0, 0, "");
                // dellist ´Â RM_DELITEMS ¿¡¼­ FREE µÈ´Ù.
            }
            if (sumlist != null)
            {
                sumlist.Free();
            }
        }

        public void UserSelectUpgradeWeapon(TUserHuman hum)
        {
            int i;
            bool flag;
            TUpgradeInfo pup;
            TStdItem pstd;
            flag = false;
            // µé°í ÀÖ´Â ¹«±âÀÇ ¾÷±×·¹ÀÌµå¸¦ ¸Ã±ä´Ù.
            for (i = 0; i < UpgradingList.Count; i++)
            {
                if (hum.UserName == UpgradingList[i].UserName)
                {
                    this.NpcSayTitle(hum, "~@upgradenow_ing");
                    return;
                }
            }
            if (hum.UseItems[Grobal2.U_WEAPON].Index != 0)
            {
                // --------------------------------------
                // À¯´ÏÅ©¾ÆÀÌÅÛÀº Á¦·Ã ¸ø¸Ã±â°Ô...
                pstd = svMain.UserEngine.GetStdItem(hum.UseItems[Grobal2.U_WEAPON].Index);
                if (pstd != null)
                {
                    if (pstd.UniqueItem == 1)
                    {
                        hum.BoxMsg("Ï¡ÓÐÎïÆ·²»ÄÜ±»Éý¼¶¡£", 0);
                        return;
                    }
                }
                // --------------------------------------
                if (hum.Gold >= ObjNpc.UPGRADEWEAPONFEE)
                {
                    // µ·ÀÌ ÀÖ´ÂÁö
                    if (hum.FindItemName(svMain.__BlackStone) != null)
                    {
                        // ÈæÃ¶À» °¡Áö°í ÀÖ´ÂÁö
                        hum.DecGold(ObjNpc.UPGRADEWEAPONFEE);
                        if (BoCastleManage)
                        {
                            // 5%ÀÇ ¼¼±ÝÀÌ °ÈÈù´Ù.
                            svMain.UserCastle.PayTax(ObjNpc.UPGRADEWEAPONFEE);
                        }
                        hum.GoldChanged();
                        // °¡¹æ¿¡ ÀÖ´Â ¾ÆÀÌÅÛÀ» ¸ù¶¥ ³Ö´Â´Ù.
                        pup = new TUpgradeInfo();
                        pup.UserName = hum.UserName;
                        pup.uitem = hum.UseItems[Grobal2.U_WEAPON];
                        // ·Î±×³²±è
                        // ¾÷¸Â_ +
                        svMain.AddUserLog("25\09" + hum.MapName + "\09" + hum.CX.ToString() + "\09" + hum.CY.ToString() + "\09" + hum.UserName + "\09" + svMain.UserEngine.GetStdItemName(hum.UseItems[Grobal2.U_WEAPON].Index) + "\09" + hum.UseItems[Grobal2.U_WEAPON].MakeIndex.ToString() + "\09" + "1\09" + this.ItemOptionToStr(this.UseItems[Grobal2.U_WEAPON].Desc));
                        hum.SendDelItem(hum.UseItems[Grobal2.U_WEAPON]);
                        // Å¬¶óÀÌ¾ðÆ®¿¡ ¾ø¾îÁø°Å º¸³¿
                        hum.UseItems[Grobal2.U_WEAPON].Index = 0;
                        hum.RecalcAbilitys();
                        hum.FeatureChanged();
                        hum.SendMsg(hum, Grobal2.RM_ABILITY, 0, 0, 0, 0, "");
                        // hum.SendMsg (hum, RM_SUBABILITY, 0, 0, 0, 0, '');
                        UserSelectUpgradeWeapon_PrepareWeaponUpgrade(hum.ItemList, ref pup.updc, ref pup.upsc, ref pup.upmc, ref pup.durapoint);
                        pup.readydate = DateTime.Now;
                        pup.readycount = GetTickCount;
                        UpgradingList.Add(pup);
                        SaveUpgradeItemList();
                        flag = true;
                    }
                }
            }
            if (flag)
            {
                this.NpcSayTitle(hum, "~@upgradenow_ok");
            }
            else
            {
                this.NpcSayTitle(hum, "~@upgradenow_fail");
            }
        }

        public void UserSelectGetBackUpgrade(TUserHuman hum)
        {
            int i;
            int per;
            int state;
            int rand;
            TUpgradeInfo pup;
            TUserItem pu;
            state = 0;
            pup = null;
            if (hum.CanAddItem())
            {
                for (i = 0; i < UpgradingList.Count; i++)
                {
                    if (hum.UserName == UpgradingList[i].UserName)
                    {
                        state = 1;
                        if ((GetTickCount - UpgradingList[i].readycount > 60 * 60 * 1000) || (hum.UserDegree >= Grobal2.UD_ADMIN))
                        {
                            pup = UpgradingList[i];
                            UpgradingList.RemoveAt(i);
                            SaveUpgradeItemList();
                            state = 2;
                            break;
                        }
                    }
                }
                if (pup != null)
                {
                    switch (pup.durapoint)
                    {
                        // ³»±¸ °áÁ¤
                        // Modify the A .. B: 0 .. 8
                        case 0:
                            // n := _MAX(3000, pup.uitem.DuraMax div 2);
                            if (pup.uitem.DuraMax > 3000)
                            {
                                pup.uitem.DuraMax = (ushort)(pup.uitem.DuraMax - 3000);
                            }
                            else
                            {
                                pup.uitem.DuraMax = (ushort)(pup.uitem.DuraMax / 2);
                            }
                            if (pup.uitem.Dura > pup.uitem.DuraMax)
                            {
                                pup.uitem.Dura = pup.uitem.DuraMax;
                            }
                            break;
                        // Modify the A .. B: 9 .. 15
                        case 9:
                            if (new System.Random(pup.durapoint).Next() < 6)
                            {
                                pup.uitem.DuraMax = (ushort)_MAX(0, pup.uitem.DuraMax - 1000);
                            }
                            break;
                        // DURAMAX¼öÁ¤
                        // 16..19
                        // Modify the A .. B: 18 .. 255
                        case 18:
                            switch (new System.Random(pup.durapoint - 18).Next())
                            {
                                // Modify the A .. B: 1 .. 4
                                case 1:
                                    pup.uitem.DuraMax = (ushort)(pup.uitem.DuraMax + 1000);
                                    break;
                                // Modify the A .. B: 5 .. 7
                                case 5:
                                    pup.uitem.DuraMax = (ushort)(pup.uitem.DuraMax + 2000);
                                    break;
                                // Modify the A .. B: 8 .. 255
                                case 8:
                                    pup.uitem.DuraMax = (ushort)(pup.uitem.DuraMax + 4000);
                                    break;
                            }
                            break;
                    }
                    if ((pup.updc == pup.upmc) && (pup.upmc == pup.upsc))
                    {
                        rand = new System.Random(3).Next();
                    }
                    else
                    {
                        rand = -1;
                    }
                    // ´É·ÂÄ¡
                    if ((pup.updc >= pup.upmc) && (pup.updc >= pup.upsc) || (rand == 0))
                    {
                        // ÆÄ±«¾÷
                        // ¹«±âÀÇ Çà¿îµµ °ü·Ã ÀÖÀ½
                        // Çà¿î
                        per = _MIN(85, 10 + _MIN(11, pup.updc) * 7 + pup.uitem.Desc[3] - pup.uitem.Desc[4] + hum.BodyLuckLevel);
                        if (new System.Random(100).Next() < per)
                        {
                            pup.uitem.Desc[10] = 10;
                            if ((per > 63) && (new System.Random(30).Next() == 0))
                            {
                                pup.uitem.Desc[10] = 11;
                            }
                            if ((per > 79) && (new System.Random(200).Next() == 0))
                            {
                                pup.uitem.Desc[10] = 12;
                            }
                        }
                        else
                        {
                            pup.uitem.Desc[10] = 1;
                        }
                    }
                    if ((pup.upmc >= pup.updc) && (pup.upmc >= pup.upsc) || (rand == 1))
                    {
                        // ¸¶·Â¾÷
                        // ¹«±âÀÇ Çà¿îµµ °ü·Ã ÀÖÀ½
                        per = _MIN(85, 10 + _MIN(11, pup.upmc) * 7 + pup.uitem.Desc[3] - pup.uitem.Desc[4] + hum.BodyLuckLevel);
                        if (new System.Random(100).Next() < per)
                        {
                            pup.uitem.Desc[10] = 20;
                            if ((per > 63) && (new System.Random(30).Next() == 0))
                            {
                                pup.uitem.Desc[10] = 21;
                            }
                            if ((per > 79) && (new System.Random(200).Next() == 0))
                            {
                                pup.uitem.Desc[10] = 22;
                            }
                        }
                        else
                        {
                            pup.uitem.Desc[10] = 1;
                        }
                    }
                    if ((pup.upsc >= pup.upmc) && (pup.upsc >= pup.updc) || (rand == 2))
                    {
                        // µµ·Â¾÷
                        // ¹«±âÀÇ Çà¿îµµ °ü·Ã ÀÖÀ½
                        per = _MIN(85, 10 + _MIN(11, pup.upsc) * 7 + pup.uitem.Desc[3] - pup.uitem.Desc[4] + hum.BodyLuckLevel);
                        if (new System.Random(100).Next() < per)
                        {
                            pup.uitem.Desc[10] = 30;
                            if ((per > 63) && (new System.Random(30).Next() == 0))
                            {
                                pup.uitem.Desc[10] = 31;
                            }
                            if ((per > 79) && (new System.Random(200).Next() == 0))
                            {
                                pup.uitem.Desc[10] = 32;
                            }
                        }
                        else
                        {
                            pup.uitem.Desc[10] = 1;
                        }
                    }
                    pu = new TUserItem();
                    pu = pup.uitem;
                    Dispose(pup);
                    // ·Î±×³²±è
                    // ¾÷Ã£_ +
                    svMain.AddUserLog("24\09" + hum.MapName + "\09" + hum.CX.ToString() + "\09" + hum.CY.ToString() + "\09" + hum.UserName + "\09" + svMain.UserEngine.GetStdItemName(pu.Index) + "\09" + pu.MakeIndex.ToString() + "\09" + "1\09" + this.ItemOptionToStr(pu.Desc));
                    hum.AddItem(pu);
                    hum.SendAddItem(pu);
                }
                switch (state)
                {
                    case 2:
                        this.NpcSayTitle(hum, "~@getbackupgnow_ok");
                        break;
                    case 1:
                        // ¿Ï¼º
                        this.NpcSayTitle(hum, "~@getbackupgnow_ing");
                        break;
                    case 0:
                        // ÀÛ¾÷Áß
                        this.NpcSayTitle(hum, "~@getbackupgnow_fail");
                        break;
                }
            }
            else
            {
#if KOREA
                hum.SysMsg("´õ ÀÌ»ó µé ¼ö ¾ø½À´Ï´Ù.", 0);
#else
                hum.SysMsg("You cannot carry any more.", 0);
#endif
                this.NpcSayTitle(hum, "@exit");
            }
        }

        public void SendGoodsEntry(TCreature who, int ltop)
        {
            TClientGoods cg;
            IList<TUserItem> goods;
            TStdItem pstd;
            TUserItem pu;
            string data = "";
            int count = 0;
            for (var i = ltop; i < GoodsList.Count; i++)
            {
                goods = GoodsList[i];
                pu = goods[0];
                pstd = svMain.UserEngine.GetStdItem(pu.Index);
                if (pstd != null)
                {
                    cg.Name = pstd.Name;
                    cg.Price = GetSellPrice((TUserHuman)who, GetPrice(pu.Index));
                    cg.Stock = goods.Count;
                    if ((pstd.StdMode <= 4) || (pstd.StdMode == 42) || (pstd.StdMode == 31))
                    {
                        cg.SubMenu = 0;
                    }
                    else
                    {
                        cg.SubMenu = 1;
                    }
                    if (pstd.OverlapItem >= 1)
                    {
                        cg.SubMenu = 2;
                    }
                    data = data + cg.Name + "/" + cg.SubMenu.ToString() + "/" + cg.Price.ToString() + "/" + cg.Stock.ToString() + "/";
                    count++;
                }
            }
            who.SendMsg(this, Grobal2.RM_SENDGOODSLIST, 0, this.ActorId, count, 0, data);
        }

        public void SendSellGoods(TCreature who)
        {
            who.SendMsg(this, Grobal2.RM_SENDUSERSELL, 0, this.ActorId, 0, 0, "");
        }

        public void SendRepairGoods(TCreature who)
        {
            who.SendMsg(this, Grobal2.RM_SENDUSERREPAIR, 0, this.ActorId, 0, 0, "");
        }

        public void SendSpecialRepairGoods(TCreature who)
        {
            // Æ¯¼ö¼ö¸®ÇÏ±â ¸Þ´º
            string str;
            // if specialrepair > 0 then begin
            str = "ÄãÕâ¼Ò»ï£¡ÄãÌ«ÐÒÔËÁË...ÎÒÕýºÃÓÐËùÐè²ÄÁÏ¿É×öÌØÊâÐÞ²¹¡£\\" + "µ«¼Û¸ñÂï...ÊÇÍ¨³£µÄÈý±¶¡£\\ \\ " + " <·µ»Ø/@main> ";
            str = ReplaceChar(str, "\\", (char)0xa);
            this.NpcSay(who, str);
            who.SendMsg(this, Grobal2.RM_SENDUSERSPECIALREPAIR, 0, this.ActorId, 0, 0, "");
            // end else begin
            // {$IFDEF KOREA}
            // NpcSay (who, '¶Ô²»Æð£¬ÎÒÃÇÓÃÀ´×öÌØÊâÐÞ²¹µÄ²ÄÁÏÒÑ¾­Ã»ÁË£¬\ ' +
            // 'µÈºòÒ»¶ÎÊ±¼äºó£¬Äú¿ÉÒÔÔÙµÃµ½ÕâÖÖ²ÄÁÏ¡£\ ' +
            // ' \µ«ÊÇÄãÐèÒªÔÙµÈÒ»»á¶ù¡£<·µ»Ø/@main> ');
            // {$ELSE}
            // NpcSay (who, 'Sorry, but we ran out of material for special repairs\' +
            // 'Sorry but we have no materials for repairs, Please wait for a moment\' +
            // ' \ <back/@main>');
            // {$ENDIF}
            // whocret.LatestNpcCmd := '@repair';
            // end;

        }

        // Æ¯¼ö¼ö¸®ÇÏ±â ¸Þ´º
        public void SendStorageItemMenu(TCreature who)
        {
            who.SendMsg(this, Grobal2.RM_SENDUSERSTORAGEITEM, 0, this.ActorId, 0, 0, "");
        }

        public void SendStorageItemList(TCreature who)
        {
            who.SendMsg(this, Grobal2.RM_SENDUSERSTORAGEITEMLIST, 0, this.ActorId, 0, 0, "");
        }

        public void SendMakeDrugItemList(TCreature who)
        {
            const int MAKEPRICE = 100;
            int i;
            int j;
            string data = string.Empty;
            TClientGoods cg;
            TUserItem pu;
            TStdItem pstd;
            ArrayList L;
            string sMakeItemName = String.Empty;
            string sMakePrice;
            data = "";
            for (i = 0; i < GoodsList.Count; i++)
            {
                L = (ArrayList)GoodsList[i];
                pu = (TUserItem)L[0];
                pstd = svMain.UserEngine.GetStdItem(pu.Index);
                if (pstd != null)
                {
                    cg.Name = pstd.Name;
                    cg.Price = MAKEPRICE;
                    // GetSellPrice (GetPrice (pu.Index));//¾à¸¸µå´Â ºñ¿ë
                    for (j = 0; j < svMain.MakeItemList.Count; j++)
                    {
                        sMakePrice = HUtil32.GetValidStr3(svMain.MakeItemList[j], ref sMakeItemName, new string[] { ":" });
                        if (cg.Name == sMakeItemName)
                        {
                            cg.Price = HUtil32.Str_ToInt(sMakePrice, 0);
                            break;
                        }
                    }
                    cg.Stock = 1;
                    // l.Count;
                    cg.SubMenu = 0;
                    // ½Ã¾à,À½½Ä,µµ±¸·ù...
                    data = data + cg.Name + "/" + cg.SubMenu.ToString() + "/" + cg.Price.ToString() + "/" + cg.Stock.ToString() + "/";
                }
            }
            if (data != "")
            {
                who.SendMsg(this, Grobal2.RM_SENDUSERMAKEDRUGITEMLIST, 0, this.ActorId, 0, 0, data);
            }
        }

        // /////////////////////////////////////////////////////
        public void SendMakeFoodList(TCreature who)
        {
            const int MAKEPRICE = 100;
            int i;
            int j;
            string data = string.Empty;
            TClientGoods cg;
            TUserItem pu;
            TStdItem pstd;
            ArrayList L;
            string sMakeItemName = String.Empty;
            string sMakePrice;
            data = "";
            for (i = 0; i < GoodsList.Count; i++)
            {
                // if i >= 12 then // MAKE FOOD
                // break;
                if (i >= HUtil32.Str_ToInt((string)svMain.MakeItemIndexList[1], 0) - HUtil32.Str_ToInt((string)svMain.MakeItemIndexList[0], 0))
                {
                    // MAKE FOOD
                    break;
                }
                L = (ArrayList)GoodsList[i];
                pu = (TUserItem)L[0];
                pstd = svMain.UserEngine.GetStdItem(pu.Index);
                if (pstd != null)
                {
                    cg.Name = pstd.Name;
                    cg.Price = MAKEPRICE;
                    // GetSellPrice (GetPrice (pu.Index));//¾à¸¸µå´Â ºñ¿ë
                    for (j = 0; j < svMain.MakeItemList.Count; j++)
                    {
                        sMakePrice = HUtil32.GetValidStr3(svMain.MakeItemList[j], ref sMakeItemName, new string[] { ":" });
                        if (cg.Name == sMakeItemName)
                        {
                            cg.Price = HUtil32.Str_ToInt(sMakePrice, 0);
                            break;
                        }
                    }
                    cg.Stock = 1;
                    // l.Count;
                    cg.SubMenu = 0;
                    // ½Ã¾à,À½½Ä,µµ±¸·ù...
                    data = data + cg.Name + "/" + cg.SubMenu.ToString() + "/" + cg.Price.ToString() + "/" + cg.Stock.ToString() + "/";
                }
            }
            if (data != "")
            {
                // ¸ðµå
                who.SendMsg(this, Grobal2.RM_SENDUSERMAKEITEMLIST, 0, this.ActorId, 1, 0, data);
            }
        }

        // /////////////////////////////////////////////////////
        public void SendMakePotionList(TCreature who)
        {
            const int MAKEPRICE = 100;
            int i;
            int j;
            string data = string.Empty;
            TClientGoods cg;
            TUserItem pu;
            TStdItem pstd;
            ArrayList L;
            string sMakeItemName = String.Empty;
            string sMakePrice;
            data = "";
            for (i = HUtil32.Str_ToInt((string)svMain.MakeItemIndexList[1], 0) - HUtil32.Str_ToInt((string)svMain.MakeItemIndexList[0], 0); i < GoodsList.Count; i++)
            {
                // if i >= 16 then // MAKE POTION
                // break;
                if (i >= HUtil32.Str_ToInt((string)svMain.MakeItemIndexList[2], 0) - HUtil32.Str_ToInt((string)svMain.MakeItemIndexList[0], 0))
                {
                    // MAKE FOOD
                    break;
                }
                L = (ArrayList)GoodsList[i];
                pu = (TUserItem)L[0];
                pstd = svMain.UserEngine.GetStdItem(pu.Index);
                if (pstd != null)
                {
                    cg.Name = pstd.Name;
                    cg.Price = MAKEPRICE;
                    // GetSellPrice (GetPrice (pu.Index));//¾à¸¸µå´Â ºñ¿ë
                    for (j = 0; j < svMain.MakeItemList.Count; j++)
                    {
                        sMakePrice = HUtil32.GetValidStr3(svMain.MakeItemList[j], ref sMakeItemName, new string[] { ":" });
                        if (cg.Name == sMakeItemName)
                        {
                            cg.Price = HUtil32.Str_ToInt(sMakePrice, 0);
                            break;
                        }
                    }
                    cg.Stock = 1;
                    // l.Count;
                    cg.SubMenu = 0;
                    // ½Ã¾à,À½½Ä,µµ±¸·ù...
                    data = data + cg.Name + "/" + cg.SubMenu.ToString() + "/" + cg.Price.ToString() + "/" + cg.Stock.ToString() + "/";
                }
            }
            if (data != "")
            {
                // ¸ðµå
                who.SendMsg(this, Grobal2.RM_SENDUSERMAKEITEMLIST, 0, this.ActorId, 2, 0, data);
            }
        }

        // /////////////////////////////////////////////////////
        public void SendMakeGemList(TCreature who)
        {
            const int MAKEPRICE = 100;
            int i;
            int j;
            string data = string.Empty;
            TClientGoods cg;
            TUserItem pu;
            TStdItem pstd;
            ArrayList L;
            string sMakeItemName = String.Empty;
            string sMakePrice;
            data = "";
            for (i = HUtil32.Str_ToInt((string)svMain.MakeItemIndexList[2], 0) - HUtil32.Str_ToInt((string)svMain.MakeItemIndexList[0], 0); i < GoodsList.Count; i++)
            {
                // if i >= 29 then // MAKE GEM
                // break;
                if (i >= HUtil32.Str_ToInt((string)svMain.MakeItemIndexList[3], 0) - HUtil32.Str_ToInt((string)svMain.MakeItemIndexList[0], 0))
                {
                    // MAKE FOOD
                    break;
                }
                L = (ArrayList)GoodsList[i];
                pu = (TUserItem)L[0];
                pstd = svMain.UserEngine.GetStdItem(pu.Index);
                if (pstd != null)
                {
                    cg.Name = pstd.Name;
                    cg.Price = MAKEPRICE;
                    // GetSellPrice (GetPrice (pu.Index));//¾à¸¸µå´Â ºñ¿ë
                    for (j = 0; j < svMain.MakeItemList.Count; j++)
                    {
                        sMakePrice = HUtil32.GetValidStr3(svMain.MakeItemList[j], ref sMakeItemName, new string[] { ":" });
                        if (cg.Name == sMakeItemName)
                        {
                            cg.Price = HUtil32.Str_ToInt(sMakePrice, 0);
                            break;
                        }
                    }
                    cg.Stock = 1;
                    // l.Count;
                    cg.SubMenu = 0;
                    // ½Ã¾à,À½½Ä,µµ±¸·ù...
                    data = data + cg.Name + "/" + cg.SubMenu.ToString() + "/" + cg.Price.ToString() + "/" + cg.Stock.ToString() + "/";
                }
            }
            if (data != "")
            {
                who.SendMsg(this, Grobal2.RM_SENDUSERMAKEITEMLIST, 0, this.ActorId, 3, 0, data);
            }
        }

        // /////////////////////////////////////////////////////
        public void SendMakeItemList(TCreature who)
        {
            const int MAKEPRICE = 100;
            int i;
            int j;
            string data = string.Empty;
            TClientGoods cg;
            TUserItem pu;
            TStdItem pstd;
            ArrayList L;
            string sMakeItemName = String.Empty;
            string sMakePrice;
            data = "";
            for (i = HUtil32.Str_ToInt((string)svMain.MakeItemIndexList[3], 0) - HUtil32.Str_ToInt((string)svMain.MakeItemIndexList[0], 0); i < GoodsList.Count; i++)
            {
                if (i >= HUtil32.Str_ToInt((string)svMain.MakeItemIndexList[4], 0) - HUtil32.Str_ToInt((string)svMain.MakeItemIndexList[0], 0))
                {
                    break;
                }
                L = (ArrayList)GoodsList[i];
                pu = (TUserItem)L[0];
                pstd = svMain.UserEngine.GetStdItem(pu.Index);
                if (pstd != null)
                {
                    cg.Name = pstd.Name;
                    cg.Price = MAKEPRICE;
                    for (j = 0; j < svMain.MakeItemList.Count; j++)
                    {
                        sMakePrice = HUtil32.GetValidStr3(svMain.MakeItemList[j], ref sMakeItemName, new string[] { ":" });
                        if (cg.Name == sMakeItemName)
                        {
                            cg.Price = HUtil32.Str_ToInt(sMakePrice, 0);
                            break;
                        }
                    }
                    cg.Stock = 1;
                    cg.SubMenu = 0;
                    data = data + cg.Name + "/" + cg.SubMenu.ToString() + "/" + cg.Price.ToString() + "/" + cg.Stock.ToString() + "/";
                }
            }
            if (data != "")
            {
                who.SendMsg(this, Grobal2.RM_SENDUSERMAKEITEMLIST, 0, this.ActorId, 4, 0, data);
            }
        }

        // /////////////////////////////////////////////////////
        public void SendMakeStuffList(TCreature who)
        {
            const int MAKEPRICE = 100;
            int i;
            int j;
            string data = string.Empty;
            TClientGoods cg;
            TUserItem pu;
            TStdItem pstd;
            ArrayList L;
            string sMakeItemName = String.Empty;
            string sMakePrice;
            data = "";
            for (i = HUtil32.Str_ToInt((string)svMain.MakeItemIndexList[4], 0) - HUtil32.Str_ToInt((string)svMain.MakeItemIndexList[0], 0); i < GoodsList.Count; i++)
            {
                if (i >= HUtil32.Str_ToInt((string)svMain.MakeItemIndexList[5], 0) - HUtil32.Str_ToInt((string)svMain.MakeItemIndexList[0], 0))
                {
                    // MAKE STUFF
                    break;
                }
                L = (ArrayList)GoodsList[i];
                pu = (TUserItem)L[0];
                pstd = svMain.UserEngine.GetStdItem(pu.Index);
                if (pstd != null)
                {
                    cg.Name = pstd.Name;
                    cg.Price = MAKEPRICE;
                    // GetSellPrice (GetPrice (pu.Index));//¾à¸¸µå´Â ºñ¿ë
                    for (j = 0; j < svMain.MakeItemList.Count; j++)
                    {
                        sMakePrice = HUtil32.GetValidStr3(svMain.MakeItemList[j], ref sMakeItemName, new string[] { ":" });
                        if (cg.Name == sMakeItemName)
                        {
                            cg.Price = HUtil32.Str_ToInt(sMakePrice, 0);
                            break;
                        }
                    }
                    cg.Stock = 1;
                    // l.Count;
                    cg.SubMenu = 0;
                    // ½Ã¾à,À½½Ä,µµ±¸·ù...
                    data = data + cg.Name + "/" + cg.SubMenu.ToString() + "/" + cg.Price.ToString() + "/" + cg.Stock.ToString() + "/";
                }
            }
            if (data != "")
            {
                // ¸ðµå
                who.SendMsg(this, Grobal2.RM_SENDUSERMAKEITEMLIST, 0, this.ActorId, 5, 0, data);
            }
        }

        // /////////////////////////////////////////////////////
        public void SendMakeEtcList(TCreature who)
        {
            const int MAKEPRICE = 100;
            int i;
            int j;
            string data = string.Empty;
            TClientGoods cg;
            TUserItem pu;
            TStdItem pstd;
            ArrayList L;
            string sMakeItemName = String.Empty;
            string sMakePrice;
            data = "";
            for (i = HUtil32.Str_ToInt((string)svMain.MakeItemIndexList[5], 0) - HUtil32.Str_ToInt((string)svMain.MakeItemIndexList[0], 0); i < GoodsList.Count; i++)
            {
                L = (ArrayList)GoodsList[i];
                pu = (TUserItem)L[0];
                pstd = svMain.UserEngine.GetStdItem(pu.Index);
                if (pstd != null)
                {
                    cg.Name = pstd.Name;
                    cg.Price = MAKEPRICE;
                    // GetSellPrice (GetPrice (pu.Index));//¾à¸¸µå´Â ºñ¿ë
                    for (j = 0; j < svMain.MakeItemList.Count; j++)
                    {
                        sMakePrice = HUtil32.GetValidStr3(svMain.MakeItemList[j], ref sMakeItemName, new string[] { ":" });
                        if (cg.Name == sMakeItemName)
                        {
                            cg.Price = HUtil32.Str_ToInt(sMakePrice, 0);
                            break;
                        }
                    }
                    cg.Stock = 1;
                    // l.Count;
                    cg.SubMenu = 0;
                    // ½Ã¾à,À½½Ä,µµ±¸·ù...
                    data = data + cg.Name + "/" + cg.SubMenu.ToString() + "/" + cg.Price.ToString() + "/" + cg.Stock.ToString() + "/";
                }
            }
            if (data != "")
            {
                // ¸ðµå
                who.SendMsg(this, Grobal2.RM_SENDUSERMAKEITEMLIST, 0, this.ActorId, 6, 0, data);
            }
        }

        // ------------------------------------------------------------------------
        // /////////////////////////////////////////////////////////////
        public override void UserSelect(TCreature whocret, string selstr)
        {
            string sel = String.Empty;
            string body= string.Empty;
            try
            {
                // »çºÏ¼º¾È¿¡ ÀÖ´Â »óÁ¡Àº °ø¼ºÀü Áß¿¡´Â ¹°°ÇÀ» ÆÈÁö ¾Ê´Â´Ù.
                if ((BoCastleManage && svMain.UserCastle.BoCastleUnderAttack) || whocret.Death)
                {
                }
                else
                {
                    body = HUtil32.GetValidStr3(selstr, ref sel, new char[] { '\r' });
                    if (sel != "")
                    {
                        if (sel[0] == '@')
                        {
                            while (true)
                            {
                                whocret.LatestNpcCmd = sel;
                                if (this.CanSpecialRepair)
                                {
                                    if (sel.ToLower().CompareTo("@s_repair".ToLower()) == 0)
                                    {
                                        SendSpecialRepairGoods(whocret);
                                        break;
                                    }
                                }
                                if (this.CanTotalRepair)
                                {
                                    if (sel.ToLower().CompareTo("@t_repair".ToLower()) == 0)
                                    {
                                        SendSpecialRepairGoods(whocret);
                                        break;
                                    }
                                }
                                this.NpcSayTitle(whocret, sel);
                                if (this.CanBuy)
                                {
                                    if (sel.ToLower().CompareTo("@buy".ToLower()) == 0)
                                    {
                                        SendGoodsEntry(whocret, 0);
                                        break;
                                    }
                                }
                                if (this.CanSell)
                                {
                                    if (sel.ToLower().CompareTo("@sell".ToLower()) == 0)
                                    {
                                        SendSellGoods(whocret);
                                        break;
                                    }
                                }
                                if (this.CanRepair)
                                {
                                    if (sel.ToLower().CompareTo("@repair".ToLower()) == 0)
                                    {
                                        SendRepairGoods(whocret);
                                        break;
                                    }
                                }
                                if (this.CanMakeDrug)
                                {
                                    if (sel.ToLower().CompareTo("@makedrug".ToLower()) == 0)
                                    {
                                        SendMakeDrugItemList(whocret);
                                        break;
                                    }
                                }
                                if (sel.ToLower().CompareTo("@prices".ToLower()) == 0)
                                {
                                    break;
                                }
                                if (this.CanStorage)
                                {
                                    if (sel.ToLower().CompareTo("@storage".ToLower()) == 0)
                                    {
                                        SendStorageItemMenu(whocret);
                                        break;
                                    }
                                }
                                if (this.CanGetBack)
                                {
                                    if (sel.ToLower().CompareTo("@getback".ToLower()) == 0)
                                    {
                                        SendStorageItemList(whocret);
                                        break;
                                    }
                                }
                                if (this.CanUpgrade)
                                {
                                    if (sel.ToLower().CompareTo("@upgradenow".ToLower()) == 0)
                                    {
                                        UserSelectUpgradeWeapon((TUserHuman)whocret);
                                        break;
                                    }
                                    if (sel.ToLower().CompareTo("@getbackupgnow".ToLower()) == 0)
                                    {
                                        UserSelectGetBackUpgrade((TUserHuman)whocret);
                                        break;
                                    }
                                }
                                if (this.CanMakeItem)
                                {
                                    if (sel.ToLower().CompareTo("@makefood".ToLower()) == 0)
                                    {
                                        SendMakeFoodList(whocret);
                                        break;
                                    }
                                    if (sel.ToLower().CompareTo("@makepotion".ToLower()) == 0)
                                    {
                                        SendMakePotionList(whocret);
                                        break;
                                    }
                                    if (sel.ToLower().CompareTo("@makegem".ToLower()) == 0)
                                    {
                                        SendMakeGemList(whocret);
                                        break;
                                    }
                                    if (sel.ToLower().CompareTo("@makeitem".ToLower()) == 0)
                                    {
                                        SendMakeItemList(whocret);
                                        break;
                                    }
                                    if (sel.ToLower().CompareTo("@makestuff".ToLower()) == 0)
                                    {
                                        SendMakeStuffList(whocret);
                                        break;
                                    }
                                    if (sel.ToLower().CompareTo("@makeetc".ToLower()) == 0)
                                    {
                                        SendMakeEtcList(whocret);
                                        break;
                                    }
                                }
                                if (this.CanItemMarket && (whocret != null) && (whocret.RaceServer == Grobal2.RC_USERHUMAN))
                                {
                                    if (sel.ToLower().CompareTo("@market_0".ToLower()) == 0)
                                    {
                                        SendUserMarket((TUserHuman)whocret, Grobal2.USERMARKET_TYPE_ALL, Grobal2.USERMARKET_MODE_BUY);
                                        break;
                                    }
                                    if (sel.ToLower().CompareTo("@market_1".ToLower()) == 0)
                                    {
                                        SendUserMarket((TUserHuman)whocret, Grobal2.USERMARKET_TYPE_WEAPON, Grobal2.USERMARKET_MODE_BUY);
                                        break;
                                    }
                                    if (sel.ToLower().CompareTo("@market_2".ToLower()) == 0)
                                    {
                                        SendUserMarket((TUserHuman)whocret, Grobal2.USERMARKET_TYPE_NECKLACE, Grobal2.USERMARKET_MODE_BUY);
                                        break;
                                    }
                                    if (sel.ToLower().CompareTo("@market_3".ToLower()) == 0)
                                    {
                                        SendUserMarket((TUserHuman)whocret, Grobal2.USERMARKET_TYPE_RING, Grobal2.USERMARKET_MODE_BUY);
                                        break;
                                    }
                                    if (sel.ToLower().CompareTo("@market_4".ToLower()) == 0)
                                    {
                                        SendUserMarket((TUserHuman)whocret, Grobal2.USERMARKET_TYPE_BRACELET, Grobal2.USERMARKET_MODE_BUY);
                                        break;
                                    }
                                    if (sel.ToLower().CompareTo("@market_5".ToLower()) == 0)
                                    {
                                        SendUserMarket((TUserHuman)whocret, Grobal2.USERMARKET_TYPE_CHARM, Grobal2.USERMARKET_MODE_BUY);
                                        break;
                                    }
                                    if (sel.ToLower().CompareTo("@market_6".ToLower()) == 0)
                                    {
                                        SendUserMarket((TUserHuman)whocret, Grobal2.USERMARKET_TYPE_HELMET, Grobal2.USERMARKET_MODE_BUY);
                                        break;
                                    }
                                    if (sel.ToLower().CompareTo("@market_7".ToLower()) == 0)
                                    {
                                        SendUserMarket((TUserHuman)whocret, Grobal2.USERMARKET_TYPE_BELT, Grobal2.USERMARKET_MODE_BUY);
                                        break;
                                    }
                                    if (sel.ToLower().CompareTo("@market_8".ToLower()) == 0)
                                    {
                                        SendUserMarket((TUserHuman)whocret, Grobal2.USERMARKET_TYPE_SHOES, Grobal2.USERMARKET_MODE_BUY);
                                        break;
                                    }
                                    if (sel.ToLower().CompareTo("@market_9".ToLower()) == 0)
                                    {
                                        SendUserMarket((TUserHuman)whocret, Grobal2.USERMARKET_TYPE_ARMOR, Grobal2.USERMARKET_MODE_BUY);
                                        break;
                                    }
                                    if (sel.ToLower().CompareTo("@market_10".ToLower()) == 0)
                                    {
                                        SendUserMarket((TUserHuman)whocret, Grobal2.USERMARKET_TYPE_DRINK, Grobal2.USERMARKET_MODE_BUY);
                                        break;
                                    }
                                    if (sel.ToLower().CompareTo("@market_11".ToLower()) == 0)
                                    {
                                        SendUserMarket((TUserHuman)whocret, Grobal2.USERMARKET_TYPE_JEWEL, Grobal2.USERMARKET_MODE_BUY);
                                        break;
                                    }
                                    if (sel.ToLower().CompareTo("@market_12".ToLower()) == 0)
                                    {
                                        SendUserMarket((TUserHuman)whocret, Grobal2.USERMARKET_TYPE_BOOK, Grobal2.USERMARKET_MODE_BUY);
                                        break;
                                    }
                                    if (sel.ToLower().CompareTo("@market_13".ToLower()) == 0)
                                    {
                                        SendUserMarket((TUserHuman)whocret, Grobal2.USERMARKET_TYPE_MINERAL, Grobal2.USERMARKET_MODE_BUY);
                                        break;
                                    }
                                    if (sel.ToLower().CompareTo("@market_14".ToLower()) == 0)
                                    {
                                        SendUserMarket((TUserHuman)whocret, Grobal2.USERMARKET_TYPE_QUEST, Grobal2.USERMARKET_MODE_BUY);
                                        break;
                                    }
                                    if (sel.ToLower().CompareTo("@market_15".ToLower()) == 0)
                                    {
                                        SendUserMarket((TUserHuman)whocret, Grobal2.USERMARKET_TYPE_ETC, Grobal2.USERMARKET_MODE_BUY);
                                        break;
                                    }
                                    if (sel.ToLower().CompareTo("@market_100".ToLower()) == 0)
                                    {
                                        SendUserMarket((TUserHuman)whocret, Grobal2.USERMARKET_TYPE_SET, Grobal2.USERMARKET_MODE_BUY);
                                        break;
                                    }
                                    if (sel.ToLower().CompareTo("@market_200".ToLower()) == 0)
                                    {
                                        SendUserMarket((TUserHuman)whocret, Grobal2.USERMARKET_TYPE_MINE, Grobal2.USERMARKET_MODE_INQUIRY);
                                        break;
                                    }
                                    if (sel.ToLower().CompareTo("@market_sell".ToLower()) == 0)
                                    {
                                        SendUserMarket((TUserHuman)whocret, Grobal2.USERMARKET_TYPE_ALL, Grobal2.USERMARKET_MODE_SELL);
                                        break;
                                    }
                                }
                                // ¹®ÆÄ Àå¿ø
                                // (ServerIndex = 0) and
                                if (this.CanAgitUsage && (whocret != null))
                                {
                                    if (sel.ToLower().CompareTo("@agitreg".ToLower()) == 0)
                                    {
                                        ((TUserHuman)whocret).CmdGuildAgitRegistration();
                                    }
                                    if (sel.ToLower().CompareTo("@agitmove".ToLower()) == 0)
                                    {
                                        ((TUserHuman)whocret).CmdGuildAgitAutoMove();
                                    }
                                    if (sel.ToLower().CompareTo("@agitbuy".ToLower()) == 0)
                                    {
                                        ((TUserHuman)whocret).CmdGuildAgitBuy(1);
                                    }
                                    if (sel.ToLower().CompareTo("@agittrade".ToLower()) == 0)
                                    {
                                        ((TUserHuman)whocret).BoGuildAgitDealTry = true;
                                        ((TUserHuman)whocret).CmdTryGuildAgitTrade();
                                    }
                                }
                                if (this.CanAgitManage && (whocret != null))
                                {
                                    if (sel.ToLower().CompareTo("@agitextend".ToLower()) == 0)
                                    {
                                        ((TUserHuman)whocret).CmdGuildAgitExtendTime(1);
                                    }
                                    if (sel.ToLower().CompareTo("@agitremain".ToLower()) == 0)
                                    {
                                        ((TUserHuman)whocret).CmdGuildAgitRemainTime();
                                    }
                                    if (sel.ToLower().CompareTo("@@agitonerecall".ToLower()) == 0)
                                    {
                                        ((TUserHuman)whocret).CmdGuildAgitRecall(body, false);
                                    }
                                    if (sel.ToLower().CompareTo("@agitrecall".ToLower()) == 0)
                                    {
                                        ((TUserHuman)whocret).CmdGuildAgitRecall("", true);
                                    }
                                    if (sel.ToLower().CompareTo("@@agitforsale".ToLower()) == 0)
                                    {
                                        ((TUserHuman)whocret).CmdGuildAgitSale(body);
                                    }
                                    if (sel.ToLower().CompareTo("@agitforsalecancel".ToLower()) == 0)
                                    {
                                        ((TUserHuman)whocret).CmdGuildAgitSaleCancel();
                                    }
                                    if (sel.ToLower().CompareTo("@gaboardlist".ToLower()) == 0)
                                    {
                                        ((TUserHuman)whocret).CmdGaBoardList(1);
                                    }
                                    if (sel.ToLower().CompareTo("@@guildagitdonate".ToLower()) == 0)
                                    {
                                        ((TUserHuman)whocret).CmdGuildAgitDonate(body);
                                    }
                                    if (sel.ToLower().CompareTo("@viewdonation".ToLower()) == 0)
                                    {
                                        ((TUserHuman)whocret).CmdGuildAgitViewDonation();
                                    }
                                }
                                if (this.CanBuyDecoItem && (whocret != null))
                                {
                                    if (sel.ToLower().CompareTo("@ga_decoitem_buy".ToLower()) == 0)
                                    {
                                        SendDecoItemListShow(whocret);
                                    }
                                    if (sel.ToLower().CompareTo("@ga_decomon_count".ToLower()) == 0)
                                    {
                                        ((TUserHuman)whocret).CmdAgitDecoMonCountHere();
                                    }
                                }
                                if (sel.ToLower().CompareTo("@exit".ToLower()) == 0)
                                {
                                    whocret.SendMsg(this, Grobal2.RM_MERCHANTDLGCLOSE, 0, this.ActorId, 0, 0, "");
                                    break;
                                }
                                break;
                            }
                        }
                    }
                }
            }
            catch
            {
                svMain.MainOutMessage("[Exception] TMerchant.UserSelect... ");
            }
        }

        public void SayMakeItemMaterials(TCreature whocret, string selstr)
        {
            string rmsg = "@";
            rmsg = rmsg + selstr;
            this.NpcSayTitle(whocret, rmsg);
        }

        public void QueryPrice(TCreature whocret, TUserItem uitem)
        {
            int buyprice;
            buyprice = (int)GetBuyPrice(GetGoodsPrice(uitem));
            // ±¸ÀÔ °¡°ÝÀ» ¾Ë·ÁÁÜ
            if (buyprice >= 0)
            {
                whocret.SendMsg(this, Grobal2.RM_SENDBUYPRICE, 0, buyprice, 0, 0, "");
            }
            else
            {
                whocret.SendMsg(this, Grobal2.RM_SENDBUYPRICE, 0, 0, 0, 0, "");
            }
            // ¾øÀ½..

        }

        public bool AddGoods(TUserItem uitem)
        {
            bool result;
            TUserItem pu;
            IList<TUserItem> list;
            TStdItem pstd;
            if (uitem.DuraMax > 0)
            {
                // ³»±¸¼ºÀÌ 0ÀÎ°ÍÀº ¼Õ½Ç Ã³¸®ÇÑ´Ù. (¾²·¡±â ¹æÁö)
                list = GetGoodsList(uitem.Index);
                if (list == null)
                {
                    list = new List<TUserItem>();
                    GoodsList.Add(list);
                }
                pu = new TUserItem();
                // 2003/06/12 »ç¿ëÀÚ°¡ ÆÈÀº ¹°°ÇÀÇ ³»±¸¼ºÀº ÃÖ´ë³»±¸·Î ¼öÁ¤ÇÏ¿©
                // ½Ñ °¡°Ý¿¡ µÇ»ì¼ö ¾øµµ·Ï ¼öÁ¤
                pstd = svMain.UserEngine.GetStdItem(uitem.Index);
                if (pstd != null)
                {
                    // Àâ»óÀÎÀÇÈ¶ºÒ,µ¶°¡·çÀÇ ³»±¸¸¦ ÃÖ´ë·Î ¼öÁ¤ÇÏÁö ¾Ê´Â´Ù(sonmg 2004/07/16)
                    // or (pstd.StdMode = 25)
                    if ((pstd.StdMode == 0) || (pstd.StdMode == 31) || ((pstd.StdMode == 3) && ((pstd.Shape == 1) || (pstd.Shape == 2) || (pstd.Shape == 3) || (pstd.Shape == 5) || (pstd.Shape == 9))) || ((pstd.StdMode == 30) && (pstd.Shape == 0)))
                    {
                        uitem.Dura = uitem.DuraMax;
                    }
                }
                pu = uitem;
                list.Insert(0, pu);
            }
            result = true;
            return result;
        }

        public bool UserSellItem_CanSell(TUserItem pu)
        {
            bool result;
            TStdItem pstd;
            result = true;
            pstd = svMain.UserEngine.GetStdItem(pu.Index);
            if (pstd != null)
            {
                if ((pstd.StdMode == 25) || (pstd.StdMode == 30))
                {
                    if (pu.Dura < 4000)
                    {
                        result = false;
                    }
                }
                else if (pstd.StdMode == 8)
                {
                    // ÃÊ´ëÀåÀº ÆÈ ¼ö ¾ø´Ù.
                    result = false;
                }
            }
            return result;
        }

        public bool UserSellItem(TCreature whocret, TUserItem uitem)
        {
            bool result;
            int buyprice;
            TStdItem pstd;
            result = false;
            buyprice = (int)GetBuyPrice(GetGoodsPrice(uitem));
            // ¹°°Ç ±¸ÀÔ °¡°Ý
            if ((buyprice >= 0) && !NoSeal && UserSellItem_CanSell(uitem))
            {
                // »ç¿ëÀÚ°¡ ¹°°ÇÀ» ÆÈÀ½. »óÇ° ±¸ÀÔµµ ¾ÈÇÔ
                if (whocret.IncGold(buyprice))
                {
                    // »çºÏ¼º¾ÈÀÇ »óÁ¡ÀÎ °æ¿ì
                    if (BoCastleManage)
                    {
                        // 5%ÀÇ ¼¼±ÝÀÌ °ÈÈù´Ù.
                        svMain.UserCastle.PayTax(buyprice);
                    }
                    whocret.SendMsg(this, Grobal2.RM_USERSELLITEM_OK, 0, whocret.Gold, 0, 0, "");
                    // »óÇ°¿¡ Ãß°¡
                    AddGoods(uitem);
                    // ·Î±×³²±è
                    pstd = svMain.UserEngine.GetStdItem(uitem.Index);
                    if ((pstd != null) && (!M2Share.IsCheapStuff(pstd.StdMode)))
                    {
                        // ÆÇ¸Å_ +
                        svMain.AddUserLog("10\09" + whocret.MapName + "\09" + whocret.CX.ToString() + "\09" + whocret.CY.ToString() + "\09" + whocret.UserName + "\09" + svMain.UserEngine.GetStdItemName(uitem.Index) + "\09" + uitem.MakeIndex.ToString() + "\09" + "1\09" + this.UserName);
                    }
                    result = true;
                }
                else
                {
                    // µ·ÀÌ ³Ê¹« ¸¹À½.
                    whocret.SendMsg(this, Grobal2.RM_USERSELLITEM_FAIL, 0, 0, 1, 0, "");
                }
            }
            else
            {
                // Ãë±Þ ¾ÈÇÔ
                whocret.SendMsg(this, Grobal2.RM_USERSELLITEM_FAIL, 0, 0, 0, 0, "");
            }
            return result;
        }

        // Ä«¿îÆ® ¾ÆÀÌÅÛ
        public bool UserCountSellItem(TCreature whocret, TUserItem uitem, int sellcnt)
        {
            bool result;
            int remain;
            int buyprice;
            TStdItem pstd;
            result = false;
            buyprice = -1;
            pstd = svMain.UserEngine.GetStdItem(uitem.Index);
            if (pstd != null)
            {
                if (IsDealingItem(pstd.StdMode, pstd.Shape))
                {
                    buyprice = (int)(GetBuyPrice(GetGoodsPrice(uitem)) * sellcnt);
                }
                // ¹°°Ç ±¸ÀÔ °¡°Ý
            }
            remain = uitem.Dura - sellcnt;
            if ((buyprice >= 0) && !NoSeal && (remain >= 0))
            {
                // »ç¿ëÀÚ°¡ ¹°°ÇÀ» ÆÈÀ½. »óÇ° ±¸ÀÔµµ ¾ÈÇÔ
                if (whocret.IncGold(buyprice))
                {
                    // »çºÏ¼º¾ÈÀÇ »óÁ¡ÀÎ °æ¿ì
                    if (BoCastleManage)
                    {
                        // 5%ÀÇ ¼¼±ÝÀÌ °ÈÈù´Ù.
                        svMain.UserCastle.PayTax(buyprice);
                    }
                    whocret.SendMsg(this, Grobal2.RM_USERSELLCOUNTITEM_OK, 0, whocret.Gold, remain, sellcnt, "");
                    // »óÇ°¿¡ Ãß°¡
                    // AddGoods (uitem);
                    // ·Î±×³²±è
                    // ÆÇ¸Å_ +
                    svMain.AddUserLog("10\09" + whocret.MapName + "\09" + whocret.CX.ToString() + "\09" + whocret.CY.ToString() + "\09" + whocret.UserName + "\09" + svMain.UserEngine.GetStdItemName(uitem.Index) + "\09" + uitem.MakeIndex.ToString() + "\09" + "1\09" + this.UserName);
                    result = true;
                }
                else
                {
                    // µ·ÀÌ ³Ê¹« ¸¹À½.
                    whocret.SendMsg(this, Grobal2.RM_USERSELLCOUNTITEM_FAIL, 0, 0, 0, 0, "");
                }
            }
            else
            {
                // Ãë±Þ ¾ÈÇÔ
                whocret.SendMsg(this, Grobal2.RM_USERSELLCOUNTITEM_FAIL, 0, 0, 0, 0, "");
            }
            return result;
        }

        public void QueryRepairCost(TCreature whocret, TUserItem uitem)
        {
            int price;
            int cost;
            price = GetSellPrice((TUserHuman)whocret, GetGoodsPrice(uitem));
            // ÆÇ¸Å°¡°ÝÀ¸·Î È¯»êÇÔ.
            if (price > 0)
            {
                if ((whocret.LatestNpcCmd == "@s_repair") || (whocret.LatestNpcCmd == "@t_repair"))
                {
                    // Æ¯¼ö¼ö¸®
                    price = price * 3;
                    // if specialrepair > 0 then
                    // else whocret.LatestNpcCmd := '@fail_s_repair';     //Æ¯¼ö¼ö¸® Àç·á ºÎÁ·..
                }
                if (uitem.DuraMax > 0)
                {
                    // DURAMAX¼öÁ¤
                    cost = HUtil32.MathRound(price / 3 / uitem.DuraMax * _MAX(0, uitem.DuraMax - uitem.Dura));
                }
                else
                {
                    cost = 0;
                }
                // price;
                whocret.SendMsg(this, Grobal2.RM_SENDREPAIRCOST, 0, cost, 0, 0, "");
            }
            else
            {
                whocret.SendMsg(this, Grobal2.RM_SENDREPAIRCOST, 0, -1, 0, 0, "");
            }
            // ¾øÀ½..

        }

        public bool UserRepairItem(TCreature whocret, TUserItem puitem)
        {
            bool result;
            int price;
            int cost;
            TStdItem pstd;
            int repair_type;
            string str;
            result = false;
            repair_type = 0;
            if (whocret.LatestNpcCmd == "@fail_s_repair")
            {
                // Æ¯¼ö¼ö¸® ¸øÇÔ.
                str = "¶Ô²»Æð£¬ÎÒÃÇ¸ÕÓÃÍêÁËÌØÊâÐÞ²¹µÄ²ÄÁÏ...\\ " + " \\ \\<·µ»Ø/@main> ";
                str = ReplaceChar(str, "\\", (char)0xa);
                this.NpcSay(whocret, str);
                whocret.SendMsg(this, Grobal2.RM_USERREPAIRITEM_FAIL, 0, 0, 0, 0, "");
                return result;
            }
            pstd = svMain.UserEngine.GetStdItem(puitem.Index);
            if (pstd == null)
            {
                return result;
            }
            price = GetSellPrice((TUserHuman)whocret, GetGoodsPrice(puitem));
            if (this.CanSpecialRepair && (whocret.LatestNpcCmd == "@s_repair"))
            {
                // Æ¯¼ö¼ö¸®
                price = price * 3;
                if ((pstd.StdMode != 5) && (pstd.StdMode != 6))
                {
                    svMain.MainOutMessage("Special Repair(X): " + whocret.UserName + " - " + pstd.Name);
                    whocret.SendMsg(this, Grobal2.RM_USERREPAIRITEM_FAIL, 0, 0, 0, 0, "");
                    return result;
                    // gadget:¹«±â°¡ ¾Æ´Ï¸é Æ¯¼ö¼ö¸® ¾øÀ½.
                }
                else
                {
                    svMain.MainOutMessage("Repair: " + whocret.UserName + "(" + whocret.MapName + ":" + whocret.CX.ToString() + "," + whocret.CY.ToString() + ")" + " - " + pstd.Name);
                }
            }
            if (this.CanTotalRepair && (whocret.LatestNpcCmd == "@t_repair"))
            {
                // Àý´ë¼ö¸®
                price = price * 3;
                switch (pstd.StdMode)
                {
                    case 5:
                    case 6:
                    case 10:
                    case 11:
                    case 15:
                    case 19:
                    case 20:
                    case 21:
                    case 22:
                    case 23:
                    case 24:
                    case 26:
                    case 52:
                    case 54:
                        // Àý´ë¼ö¸® ÀÌº¥Æ® 2003-06-26
                        svMain.MainOutMessage("Perfect Repair: " + whocret.UserName + "(" + whocret.MapName + ":" + whocret.CX.ToString() + "," + whocret.CY.ToString() + ")" + " - " + pstd.Name);
                        break;
                    default:
                        svMain.MainOutMessage("Perfect Repair(X): " + whocret.UserName + " - " + pstd.Name);
                        whocret.SendMsg(this, Grobal2.RM_USERREPAIRITEM_FAIL, 0, 0, 0, 0, "");
                        return result;
                        break;
                        // pds:ÀÌº¥Æ® Àý´ë¼ö¸®
                }
            }
            // À¯´ÏÅ©¾ÆÀÌÅÛ ÇÊµå°¡ 3ÀÌ¸é ¼ö¸®ºÒ°¡.
            // or -> and (sonmg's bug 2003/12/03)
            if ((price > 0) && (pstd.StdMode != 43) && (pstd.UniqueItem != 3))
            {
                // Ãë±ÞÇÏÁö ¾Ê´Â °ÍÀº ¼ö¸® ¾ÈµÊ
                if (puitem.DuraMax > 0)
                {
                    // DURAMAX¼öÁ¤
                    cost = HUtil32.MathRound(price / 3 / puitem.DuraMax * _MAX(0, puitem.DuraMax - puitem.Dura));
                }
                else
                {
                    cost = 0;
                }
                // price;
                if ((cost > 0) && whocret.DecGold(cost))
                {
                    // »çºÏ¼º¾ÈÀÇ »óÁ¡ÀÎ °æ¿ì
                    if (BoCastleManage)
                    {
                        // 5%ÀÇ ¼¼±ÝÀÌ °ÈÈù´Ù.
                        svMain.UserCastle.PayTax(cost);
                    }
                    if ((this.CanSpecialRepair && (whocret.LatestNpcCmd == "@s_repair")) || (this.CanTotalRepair && (whocret.LatestNpcCmd == "@t_repair")))
                    {
                        // Æ¯¼ö¼ö¸®
                        // Dec (specialrepair);
                        // Æ¯¼ö¼ö¸®´Â ³»±¸°¡ ¾àÇØÁöÁö ¾ÊÀ½
                        // puitem.DuraMax := puitem.DuraMax - _MAX(0, puitem.DuraMax-puitem.Dura) div 100;  //DURAMAX¼öÁ¤
                        puitem.Dura = puitem.DuraMax;
                        whocret.SendMsg(this, Grobal2.RM_USERREPAIRITEM_OK, 0, whocret.Gold, puitem.Dura, puitem.DuraMax, "");
                        str = "¿Ïº®ÇÏ°Ô ¼ö¸®µÇ¾ú³×...\\ÇëºÃºÃÊ¹ÓÃËü¡£\\ \\<·µ»Ø/@main> ";
                        str = ReplaceChar(str, "\\", (char)0xa);
                        this.NpcSay(whocret, str);
                        repair_type = 2;
                    }
                    else
                    {
                        // ÀÏ¹Ý ¼ö¸®, ³»±¸¼ºÀÌ ¸¹ÀÌ ¾àÇØÁü
                        puitem.DuraMax = (ushort)(puitem.DuraMax - _MAX(0, puitem.DuraMax - puitem.Dura) / 30);
                        // DURAMAX¼öÁ¤
                        puitem.Dura = puitem.DuraMax;
                        whocret.SendMsg(this, Grobal2.RM_USERREPAIRITEM_OK, 0, whocret.Gold, puitem.Dura, puitem.DuraMax, "");
                        this.NpcSayTitle(whocret, "~@repair");
                        repair_type = 1;
                    }
                    result = true;
                    // ¼ö¸® ·Î±× ³²±è
                    // ¼ö¸®_ +
                    svMain.AddUserLog("36\09" + whocret.MapName + "\09" + cost.ToString() + "\09" + whocret.Gold.ToString() + "\09" + whocret.UserName + "\09" + puitem.DuraMax.ToString() + "\09" + puitem.MakeIndex.ToString() + "\09" + repair_type.ToString() + "\09" + "0");
                }
                else
                {
                    // µ·ÀÌ ¾øÀ½
                    whocret.SendMsg(this, Grobal2.RM_USERREPAIRITEM_FAIL, 0, 0, 0, 0, "");
                }
            }
            else
            {
                whocret.SendMsg(this, Grobal2.RM_USERREPAIRITEM_FAIL, 0, 0, 0, 0, "");
            }
            return result;
        }

        public void UserBuyItem(TUserHuman whocret, string itmname, int serverindex, int BuyCount)
        {
            int i;
            int k;
            int sellprice;
            int rcode;
            ArrayList list;
            TStdItem pstd;
            TUserItem pu;
            bool done;
            int CheckWeight;
            bool InviteResult;
            done = false;
            InviteResult = true;
            rcode = 1;
            // »óÇ°ÀÌ ´Ù ÆÈ·È½À´Ï´Ù.
            for (i = 0; i < GoodsList.Count; i++)
            {
                if (done)
                {
                    break;
                }
                if (NoSeal)
                {
                    break;
                }
                // ¹°°ÇÀ» ¾ÈÆÄ´Â °¡°Ô
                list = (ArrayList)GoodsList[i];
                pu = (TUserItem)list[0];
                pstd = svMain.UserEngine.GetStdItem(pu.Index);
                if (pstd != null)
                {
                    // Ä«¿îÆ®¾ÆÀÌÅÛ
                    if (pstd.OverlapItem == 1)
                    {
                        CheckWeight = pstd.Weight + pstd.Weight * (BuyCount / 10);
                    }
                    else if (pstd.OverlapItem >= 2)
                    {
                        CheckWeight = pstd.Weight * BuyCount;
                    }
                    else
                    {
                        CheckWeight = pstd.Weight;
                    }
                    if (whocret.IsAddWeightAvailable(CheckWeight))
                    {
                        if (pstd.Name == itmname)
                        {
                            for (k = 0; k < list.Count; k++)
                            {
                                // »ç¿ëÀÚ°¡ ¹°°ÇÀ» »ç°¨
                                pu = (TUserItem)list[k];
                                if ((pstd.StdMode <= 4) || (pstd.StdMode == 42) || (pstd.StdMode == 31) || (pu.MakeIndex == serverindex) || (pstd.OverlapItem >= 1))
                                {
                                    // µ·À» ÃæºÐÀÌ °¡Áö°í ÀÖ¾î¾ßÇÔ.
                                    // if pstd.StdMode <= 4 then sellprice := GetPrice (pu.Index) //´ëÇ¥°¡°Ý
                                    sellprice = GetSellPrice(whocret, GetGoodsPrice(pu)) * BuyCount;
                                    // °³º° °¡°Ý
                                    if ((whocret.Gold >= sellprice) && (sellprice > 0))
                                    {
                                        if (pstd.OverlapItem >= 1)
                                        {
                                            pu.Dura = (ushort)_MIN(1000, BuyCount);
                                        }
                                        // 2003/03/04 »óÁ¡ Á¨ Å¸ÀÓ Á¶Á¤ 1ºÐ -> 1½Ã°£<- ÀÌ ¸®¸¶Å©·Î ÄÚµùµÇ¾î ÀÖ´Â Å×½ºÆ® ¼­¹ö ÄÚµå
                                        // 
                                        // // 2003/03/04 ¾à¹°, Àü¼­·ù, È¶ºÒ, ¾à¹­À½, ºÎÀû Á¾·ù´Â »õ·Î ¸¸µé¾î º¸³»ÁØ´Ù
                                        // if(pstd.StdMode = 0) or //(pstd.StdMode = 25) or //µ¶°¡·ç Á¦¿Ü
                                        // ((pstd.StdMode = 3) and ((pstd.Shape = 1) or (pstd.Shape = 2) or (pstd.Shape = 3) or (pstd.Shape = 5) or (pstd.Shape = 9))) or
                                        // ((pstd.StdMode = 30) and (pstd.Shape = 0)) or (pstd.StdMode = 31) then begin
                                        // iname := pstd.Name;
                                        // new(pu);
                                        // if UserEngine.CopyToUserItemFromName(iname, pu^) then begin
                                        // if whocret.AddItem(pu) then begin
                                        // //                                 whocret.Gold := whocret.Gold - sellprice;
                                        // whocret.DecGold( sellprice );
                                        // whocret.SendAddItem(pu^);
                                        // //»çºÏ¼º¾ÈÀÇ »óÁ¡ÀÎ °æ¿ì
                                        // if BoCastleManage then  //5%ÀÇ ¼¼±ÝÀÌ °ÈÈù´Ù.
                                        // UserCastle.PayTax (sellprice);
                                        // //·Î±×³²±è
                                        // AddUserLog ('9'#9 + //±¸ÀÔ_
                                        // whocret.MapName + ''#9 +
                                        // IntToStr(whocret.CX) + ''#9 +
                                        // IntToStr(whocret.CY) + ''#9 +
                                        // whocret.UserName + ''#9 +
                                        // UserEngine.GetStdItemName (pu.Index) + ''#9 +
                                        // IntToStr(pu.MakeIndex) + ''#9 +
                                        // '1'#9 +
                                        // UserName);
                                        // rcode := 0;
                                        // end else begin
                                        // Dispose(pu);
                                        // rcode := 2;
                                        // end;
                                        // end else begin
                                        // Dispose(pu);
                                        // rcode := 2;
                                        // end;
                                        // end else begin
                                        // Ä«¿îÆ® ¾ÆÀÌÅÛ
                                        if (pstd.OverlapItem >= 1)
                                        {
                                            if (whocret.UserCounterItemAdd(pstd.StdMode, pstd.Looks, BuyCount, pstd.Name, false))
                                            {
                                                // whocret.Gold := whocret.Gold - sellprice;
                                                whocret.DecGold(sellprice);
                                                // Dispose(list[k]);    //¸·¾Æº¸ÀÚ...
                                                list.RemoveAt(k);
                                                if (list.Count == 0)
                                                {
                                                    list.Free();
                                                    GoodsList.RemoveAt(i);
                                                }
                                                whocret.WeightChanged();
                                                rcode = 0;
                                                done = true;
                                                break;
                                            }
                                        }
                                        InviteResult = true;
                                        // ÃÊ´ëÀå ¼ÂÆÃ.
                                        if ((pstd.StdMode == 8) && (pstd.Shape == ObjBase.SHAPE_OF_INVITATION))
                                        {
                                            InviteResult = whocret.GuildAgitInvitationItemSet(pu);
                                            if (!InviteResult)
                                            {
                                                whocret.SysMsg("ÔÚÄãµÄÃÅÅÉ×¯Ô°Äã¿ÉÒÔµÃµ½Ò»¸öÑûÇëº¯¡£", 0);
                                            }
                                        }
                                        if (InviteResult)
                                        {
                                            if (whocret.AddItem(pu))
                                            {
                                                // whocret.Gold := whocret.Gold - sellprice;
                                                whocret.DecGold(sellprice);
                                                // »çºÏ¼º¾ÈÀÇ »óÁ¡ÀÎ °æ¿ì
                                                if (BoCastleManage)
                                                {
                                                    // 5%ÀÇ ¼¼±ÝÀÌ °ÈÈù´Ù.
                                                    svMain.UserCastle.PayTax(sellprice);
                                                }
                                                whocret.SendAddItem(pu);
                                                // »ç±â ¼º°ø
                                                // ·Î±×³²±è
                                                // ±¸ÀÔ_
                                                svMain.AddUserLog("9\09" + whocret.MapName + "\09" + whocret.CX.ToString() + "\09" + whocret.CY.ToString() + "\09" + whocret.UserName + "\09" + svMain.UserEngine.GetStdItemName(pu.Index) + "\09" + pu.MakeIndex.ToString() + "\09" + "1\09" + this.UserName);
                                                list.RemoveAt(k);
                                                if (list.Count == 0)
                                                {
                                                    list.Free();
                                                    GoodsList.RemoveAt(i);
                                                }
                                                rcode = 0;
                                            }
                                            else
                                            {
                                                rcode = 2;
                                            }
                                        }
                                        else
                                        {
                                            // ÃÊ´ëÀåÀ» »ì ¼ö ¾øÀ¸¸é ºüÁ®³ª°¨.
                                            return;
                                        }
                                        // 2003/03/04 ¾à¹°, Àü¼­·ù, È¶ºÒ, ¾à¹­À½, ºÎÀû Á¾·ù´Â »õ·Î ¸¸µé¾î º¸³»ÁØ´Ù
                                        // end;
                                    }
                                    else
                                    {
                                        rcode = 3;
                                    }
                                    // µ·ÀÌ ºÎÁ·ÇÔ.
                                    done = true;
                                    break;
                                }
                            }
                        }
                    }
                    else
                    {
                        rcode = 2;
                        // ´õ ÀÌ»ó µé ¼ö ¾øÀ½.
                    }
                }
            }
            if (rcode == 0)
            {
                // ÆÈ¸° ¾ÆÀÌÅÛ
                whocret.SendMsg(this, Grobal2.RM_BUYITEM_SUCCESS, 0, whocret.Gold, serverindex, 0, "");
            }
            else
            {
                whocret.SendMsg(this, Grobal2.RM_BUYITEM_FAIL, 0, rcode, 0, 0, "");
            }
        }

        public void UserWantDetailItems(TCreature whocret, string itmname, int menuindex)
        {
            int i;
            int k;
            int count;
            string data = string.Empty;
            ArrayList list;
            TStdItem pstd;
            TStdItem std;
            TUserItem pu;
            TClientItem citem=null;
            count = 0;
            for (i = 0; i < GoodsList.Count; i++)
            {
                list = (ArrayList)GoodsList[i];
                pu = (TUserItem)list[0];
                pstd = svMain.UserEngine.GetStdItem(pu.Index);
                if (pstd != null)
                {
                    if (pstd.Name == itmname)
                    {
                        if (menuindex > list.Count - 1)
                        {
                            menuindex = _MAX(0, list.Count - 10);
                        }
                        for (k = menuindex; k < list.Count; k++)
                        {
                            pu = (TUserItem)list[k];
                            // citem.S := pstd^;
                            std = pstd;
                            svMain.ItemMan.GetUpgradeStdItem(pu, ref std);
                            //Move(std, citem.S, sizeof(TStdItem));
                            //FillChar(citem.Desc, sizeof(citem.Desc), '\0');
                            citem.Dura = pu.Dura;
                            citem.DuraMax = (ushort)GetSellPrice((TUserHuman)whocret, GetGoodsPrice(pu));
                            citem.MakeIndex = pu.MakeIndex;
                            //FillChar(citem.Desc, sizeof(citem.Desc), '\0');
                            //Move(pu.Desc, citem.Desc, sizeof(pu.Desc));
                            data = data + EDcode.EncodeBuffer(citem) + "/";
                            count++;
                            if (count >= 10)
                            {
                                break;
                            }
                        }
                        break;
                    }
                }
            }
            whocret.SendMsg(this, Grobal2.RM_SENDDETAILGOODSLIST, 0, this.ActorId, count, menuindex, data);
        }

        public int UserMakeNewItem_CheckCondition(TUserHuman hum, string itemname, ref int iPrice)
        {
            int result;
            ArrayList list;
            int k;
            int i;
            int sourcecount;
            string sourcename;
            int condition;
            ArrayList dellist;
            TUserItem pu;
            TStdItem ps;
            condition = ObjNpc.COND_FAILURE;
            list = M2Share.GetMakeItemCondition(itemname, ref iPrice);
            if (hum.Gold < iPrice)
            {
                result = ObjNpc.COND_NOMONEY;
                return result;
            }
            if (list != null)
            {
                condition = ObjNpc.COND_SUCCESS;
                for (k = 0; k < list.Count; k++)
                {
                    sourcename = (string)list[k];
                    sourcecount = (int)list.Values[k];
                    for (i = 0; i < hum.ItemList.Count; i++)
                    {
                        // ³» °¡¹æ¿¡ ¾ÆÀÌÅÛÀÌ ÀÖ´ÂÁö °Ë»ç
                        pu = hum.ItemList[i];
                        if (sourcename == svMain.UserEngine.GetStdItemName(pu.Index))
                        {
                            ps = svMain.UserEngine.GetStdItem(pu.Index);
                            if (ps != null)
                            {
                                // Ä«¿îÆ® ¾ÆÀÌÅÛ.
                                if (ps.OverlapItem >= 1)
                                {
                                    sourcecount = sourcecount - _MIN(pu.Dura, sourcecount);
                                }
                                else
                                {
                                    sourcecount -= 1;
                                }
                                // °¹¼ö °Ë»ç..
                            }
                        }
                    }
                    if (sourcecount > 0)
                    {
                        condition = ObjNpc.COND_FAILURE;
                        // °¹¼ö ¹Ì´ÞÀÌ¸é Á¶°Ç ¾È¸ÂÀ½°£ÁÖ.
                        break;
                    }
                }
                if (condition == ObjNpc.COND_SUCCESS)
                {
                    // Á¶°ÇÀÌ ¸ÂÀ¸¸é Àç·á´Â »ç¶óÁø´Ù.
                    dellist = null;
                    for (k = 0; k < list.Count; k++)
                    {
                        sourcename = (string)list[k];
                        sourcecount = (int)list.Values[k];
                        for (i = hum.ItemList.Count - 1; i >= 0; i--)
                        {
                            pu = hum.ItemList[i];
                            if (sourcecount > 0)
                            {
                                if (sourcename == svMain.UserEngine.GetStdItemName(pu.Index))
                                {
                                    ps = svMain.UserEngine.GetStdItem(pu.Index);
                                    if (ps != null)
                                    {
                                        // Ä«¿îÆ® ¾ÆÀÌÅÛ.
                                        if (ps.OverlapItem >= 1)
                                        {
                                            if (pu.Dura < ((int)list.Values[k]))
                                            {
                                                pu.Dura = 0;
                                            }
                                            else
                                            {
                                                pu.Dura = pu.Dura - ((int)list.Values[k]);
                                            }
                                            if (pu.Dura > 0)
                                            {
                                                hum.SendMsg(this, Grobal2.RM_COUNTERITEMCHANGE, 0, pu.MakeIndex, pu.Dura, 0, ps.Name);
                                                continue;
                                            }
                                        }
                                        // ÀÏ¹Ý ¾ÆÀÌÅÛ ¶Ç´Â Ä«¿îÆ® ¾ÆÀÌÅÛ »èÁ¦
                                        if (dellist == null)
                                        {
                                            dellist = new ArrayList();
                                        }
                                        dellist.Add(sourcename, hum.ItemList[i].MakeIndex as Object);
                                        Dispose(hum.ItemList[i]);
                                        hum.ItemList.RemoveAt(i);
                                        sourcecount -= 1;
                                    }
                                }
                            }
                            else
                            {
                                break;
                            }
                        }
                    }
                    if (dellist != null)
                    {
                        // dellistÀº  RM_DELITEMS¿¡¼­ FreeµÊ.
                        hum.SendMsg(this, Grobal2.RM_DELITEMS, 0, (int)dellist, 0, 0, "");
                    }
                }
            }
            result = condition;
            return result;
        }

        // Áö¼® or ½Å¼®
        // ---------------//
        // ////////////////////////////////////////
        public void UserMakeNewItem(TUserHuman whocret, string itmname)
        {
            const int MAKEPRICE = 100;
            int i;
            int rcode;
            bool done;
            ArrayList list;
            TUserItem pu;
            TUserItem newpu;
            TStdItem pstd;
            int iMakePrice;
            int iCheckResult;
            iMakePrice = MAKEPRICE;
            done = false;
            rcode = 1;
            for (i = 0; i < GoodsList.Count; i++)
            {
                if (done)
                {
                    break;
                }
                list = (ArrayList)GoodsList[i];
                pu = (TUserItem)list[0];
                pstd = svMain.UserEngine.GetStdItem(pu.Index);
                if (pstd != null)
                {
                    if (pstd.Name == itmname)
                    {
                        // ¾ÆÀÌÅÛ ¸¸µå´Â ºñ¿ëµµ ÇÔ²² Ã¼Å©ÇÑ´Ù.
                        iCheckResult = UserMakeNewItem_CheckCondition(whocret, itmname, ref iMakePrice);
                        if (iCheckResult != ObjNpc.COND_NOMONEY)
                        {
                            if (iCheckResult == ObjNpc.COND_SUCCESS)
                            {
                                newpu = new TUserItem();
                                svMain.UserEngine.CopyToUserItemFromName(itmname, ref newpu);
                                if (whocret.AddItem(newpu))
                                {
                                    // whocret.Gold := whocret.Gold - iMakePrice;
                                    whocret.DecGold(iMakePrice);
                                    whocret.SendAddItem(newpu);
                                    // ¸¸µé±â ¼º°ø...
                                    // ·Î±×³²±è
                                    // Á¦ÀÛ_
                                    svMain.AddUserLog("2\09" + whocret.MapName + "\09" + whocret.CX.ToString() + "\09" + whocret.CY.ToString() + "\09" + whocret.UserName + "\09" + svMain.UserEngine.GetStdItemName(newpu.Index) + "\09" + newpu.MakeIndex.ToString() + "\09" + "1\09" + this.UserName);
                                    rcode = 0;
                                }
                                else
                                {
                                    Dispose(newpu);
                                    rcode = 2;
                                }
                            }
                            else
                            {
                                rcode = 4;
                            }
                        }
                        else
                        {
                            rcode = 3;
                        }
                    }
                }
            }
            if (rcode == 0)
            {
                whocret.SendMsg(this, Grobal2.RM_MAKEDRUG_SUCCESS, 0, whocret.Gold, 0, 0, "");
            }
            else
            {
                whocret.SendMsg(this, Grobal2.RM_MAKEDRUG_FAIL, 0, rcode, 0, 0, "");
            }
        }

        // ¾ÆÀÌÅÛ Á¦Á¶ ÇÁ·Î½ÃÁ®.
        public void UserManufactureItem(TUserHuman whocret, string itmname)
        {
            const int MAKEPRICE = 100;
            int i;
            int j;
            int rcode;
            bool done;
            ArrayList list;
            TUserItem pu;
            TUserItem newpu;
            TStdItem pstd;
            string sMakeItemName = String.Empty;
            string[] sItemMakeIndex = new string[ObjNpc.MAX_SOURCECNT + 1];
            string[] sItemName = new string[ObjNpc.MAX_SOURCECNT + 1];
            string[] sItemCount = new string[ObjNpc.MAX_SOURCECNT + 1];
            int iCheckResult;
            int iMakePrice;
            int iMakeCount;
            iMakePrice = MAKEPRICE;
            try
            {
                itmname = HUtil32.GetValidStr3(itmname, ref sMakeItemName, new string[] { "/" });
                for (i = 1; i <= ObjNpc.MAX_SOURCECNT; i++)
                {
                    itmname = HUtil32.GetValidStr3(itmname, ref sItemMakeIndex[i], new string[] { ":" });
                    itmname = HUtil32.GetValidStr3(itmname, ref sItemName[i], new string[] { ":" });
                    itmname = HUtil32.GetValidStr3(itmname, ref sItemCount[i], new string[] { "/" });
                }
                // /////////////////////////////////////////
#if DEBUG
                // sonmg
                whocret.SysMsg(sMakeItemName, 0);
                for (i = 1; i <= ObjNpc.MAX_SOURCECNT; i ++ )
                {
                    whocret.SysMsg(sItemMakeIndex[i] + sItemName[i] + sItemCount[i], 0);
                }
#endif
                // /////////////////////////////////////////
                done = false;
                rcode = 1;
                for (i = 0; i < GoodsList.Count; i++)
                {
                    if (done)
                    {
                        break;
                    }
                    list = (ArrayList)GoodsList[i];
                    pu = (TUserItem)list[0];
                    pstd = svMain.UserEngine.GetStdItem(pu.Index);
                    if (pstd != null)
                    {
                        if (pstd.Name == sMakeItemName)
                        {
                            // ¾ÆÀÌÅÛ ¸¸µå´Â ºñ¿ëµµ ÇÔ²² Ã¼Å©ÇÑ´Ù.
                            iCheckResult = CheckMakeItemCondition(whocret, sMakeItemName, sItemMakeIndex, sItemName, sItemCount, ref iMakePrice, ref iMakeCount);
                            if (iCheckResult != ObjNpc.COND_NOMONEY)
                            {
                                if (iCheckResult == ObjNpc.COND_SUCCESS)
                                {
                                    for (j = 0; j < iMakeCount; j++)
                                    {
                                        newpu = new TUserItem();
                                        svMain.UserEngine.CopyToUserItemFromName(sMakeItemName, ref newpu);
                                        if (whocret.AddItem(newpu))
                                        {
                                            // whocret.Gold := whocret.Gold - iMakePrice;
                                            whocret.DecGold(iMakePrice);
                                            whocret.SendAddItem(newpu);
                                            // ¸¸µé±â ¼º°ø...
                                            // Á¦Á¶ ¼º°ø ·Î±×
                                            // 
                                            // MainOutMessage( '[Manufacture] ' + whocret.UserName + ' ' + UserEngine.GetStdItemName (newpu.Index) + '(' + IntToStr(newpu.MakeIndex) + ') '
                                            // + '=> »èÁ¦µÈ Àç·á:' + sItemName[1] + ', ' + sItemName[2]
                                            // + ', ' + sItemName[3] + ', ' + sItemName[4]
                                            // + ', ' + sItemName[5] + ', ' + sItemName[6] );
                                            // ·Î±×³²±è
                                            // Á¦ÀÛ_
                                            svMain.AddUserLog("2\09" + whocret.MapName + "\09" + whocret.CX.ToString() + "\09" + whocret.CY.ToString() + "\09" + whocret.UserName + "\09" + svMain.UserEngine.GetStdItemName(newpu.Index) + "\09" + newpu.MakeIndex.ToString() + "\09" + "1\09" + this.UserName);
                                            rcode = 0;
                                        }
                                        else
                                        {
                                            Dispose(newpu);
                                            rcode = 2;
                                        }
                                    }
                                }
                                else if (iCheckResult == ObjNpc.COND_GEMFAIL)
                                {
                                    // º¸¿Á Á¦Á¶ ½ÇÆÐ½Ã¿¡µµ µ·Àº ºüÁ® ³ª°£´Ù.
                                    // whocret.Gold := whocret.Gold - iMakePrice;
                                    whocret.DecGold(iMakePrice);
                                    whocret.GoldChanged();
                                    // ·Î±×³²±è
                                    // Á¦ÀÛ_½ÇÆÐ
                                    svMain.AddUserLog("2\09" + whocret.MapName + "\09" + whocret.CX.ToString() + "\09" + whocret.CY.ToString() + "\09" + whocret.UserName + "\09" + "FAIL\09" + "0\09" + "1\09" + this.UserName);
                                    rcode = 5;
                                }
                                else if (iCheckResult == ObjNpc.COND_MINERALFAIL)
                                {
                                    rcode = 6;
                                }
                                else if (iCheckResult == ObjNpc.COND_BAGFULL)
                                {
                                    rcode = 7;
                                }
                                else
                                {
                                    rcode = 4;
                                }
                            }
                            else
                            {
                                rcode = 3;
                            }
                        }
                    }
                }
                if (rcode == 0)
                {
                    whocret.SendMsg(this, Grobal2.RM_MAKEDRUG_SUCCESS, 0, whocret.Gold, 0, 0, "");
                }
                else
                {
                    whocret.SendMsg(this, Grobal2.RM_MAKEDRUG_FAIL, 0, rcode, 0, 0, "");
                }
            }
            catch
            {
                svMain.MainOutMessage("[Exception] TMerchant.UserManufactureItem");
            }
        }

        // //////////////////////////////////////////////////////////
        // ¼öÈ£¼®ÀÇ µî±ÞÀ» ¾ò¾î³»´Â ÇÔ¼ö.
        public int GetGradeOfGuardStoneByName(string strGuardStone)
        {
            int result = ObjNpc.GSG_ERROR;
            if (HUtil32.CompareBackLStr(strGuardStone, "(Ð¡)", 3) == true)
            {
                result = ObjNpc.GSG_SMALL;
            }
            else if (HUtil32.CompareBackLStr(strGuardStone, "(ÖÐ)", 3) == true)
            {
                result = ObjNpc.GSG_MEDIUM;
            }
            else if (HUtil32.CompareBackLStr(strGuardStone, "(´ó)", 3) == true)
            {
                result = ObjNpc.GSG_LARGE;
            }
            else if ((HUtil32.CompareBackLStr(strGuardStone, "(ÌØ)", 4) == true) || (HUtil32.CompareBackLStr(strGuardStone, "ÌØ´ó", 2) == true))
            {
                result = ObjNpc.GSG_GREATLARGE;
            }
            else if (HUtil32.CompareBackLStr(strGuardStone, "Ê¯Í·", 5) == true)
            {
                result = ObjNpc.GSG_SUPERIOR;
            }
            else
            {
                result = ObjNpc.GSG_ERROR;
            }
            return result;
        }

        // //////////////////////////////////////////////////////////
        // Á¦Á¶´ë»ó¿¡ ÇÊ¿äÇÑ ¸ñ·Ï°ú Àü¼Û¹ÞÀº ¸ñ·ÏÀ» ºñ±³ÇÏ¿©
        // Á¶°Ç¿¡ ¸Â´ÂÁö ¾Æ´ÑÁö Ã¼Å©ÇÏ°í ¾ÆÀÌÅÛÀ» »èÁ¦ÇÏ´Â ÇÔ¼ö.
        public int CheckMakeItemCondition(TUserHuman hum, string itemname, string[] sItemMakeIndex, string[] sItemName, string[] sItemCount, ref int iPrice, ref int iMakeCount)
        {
            int result;
            IList<string> list;
            int k;
            int i;
            int j;
            int icnt;
            int sourcecount;
            int counteritmcount;
            int itemp;
            int sourcemindex;
            string sourcename;
            int condition;
            ArrayList dellist;
            TUserItem pu;
            TStdItem ps;
            int iGuardStoneGrade;
            int iProbability;
            double fTemporary;
            int iRequiredGuardStoneGrade;
            int iSumOutfitAbil;
            int iOutfitGrade;
            // »õ·Î¿î List
            string[] sNewName = new string[ObjNpc.MAX_SOURCECNT - 1 + 1];
            string[] sNewCount = new string[ObjNpc.MAX_SOURCECNT - 1 + 1];
            string[] sNewMIndex = new string[ObjNpc.MAX_SOURCECNT - 1 + 1];
            int[] iListDoubleCount = new int[ObjNpc.MAX_SOURCECNT - 1 + 1];
            int checkcount;
            bool bCheckMIndex;
            // ½ºÅ©¸³Æ® ¹®ÀÚ¿­ Á¤ÀÇ
            string strPendant;
            string strGuardStone;
            string strGuardStone15;
            string strGuardStoneXLHigher;
            string delitemname;
            strPendant = "";
            strGuardStone = "";
            strGuardStone15 = "";
            strGuardStoneXLHigher = "";
            strPendant = "<µõ×¹>";
            strGuardStone = "<ÊØ»¤Ê¯>";
            strGuardStone15 = "<ÊØ»¤Ê¯15>";
            strGuardStoneXLHigher = "<ÊØ»¤Ê¯(ÌØ´ó)ºÜ¸ß>";
            iProbability = 0;
            fTemporary = 0;
            condition = ObjNpc.COND_FAILURE;
            iRequiredGuardStoneGrade = 0;
            // ¼öÈ£¼® Ãß°¡ È®·ü µî±Þ
            iOutfitGrade = 0;
            // Àå½Å±¸ Ãß°¡ È®·ü µî±Þ
            iSumOutfitAbil = 0;
            iGuardStoneGrade = ObjNpc.GSG_ERROR;
            list = M2Share.GetMakeItemCondition(itemname, ref iPrice);
            // °¡¹æÃ¢ ¿©ºÐÀÇ °ø°£ È®ÀÎ
            if (hum.CanAddItem() == false)
            {
                result = ObjNpc.COND_BAGFULL;
                hum.SysMsg("ÄãµÄ°ü¸¤ÒÑÂú¡£", 0);
                return result;
            }
            if (list != null)
            {
                // Àü¼Û¹®ÀÚ¿­ ÀÎÀÚ¼öº¸´Ù Å©¸é ¾ÈµÊ.
                if (list.Count > ObjNpc.MAX_SOURCECNT)
                {
                    svMain.MainOutMessage("[Caution!] list.Count Overflow in TMerchant.UserManufactureItem");
                }
                condition = ObjNpc.COND_SUCCESS;
                // º¸¿Á Å¸ÀÔ °Ë»ç(¼öÈ£¼®ÀÇ Á¾·ù·Î °Ë»ç sonmg)
                for (j = 0; j < list.Count; j++)
                {
                    if (list[j].ToUpper() == strGuardStone)
                    {
                        iRequiredGuardStoneGrade = 1;
                        // Å¸ÀÔ A
                    }
                    else if (list[j].ToUpper() == strGuardStoneXLHigher)
                    {
                        iRequiredGuardStoneGrade = 2;
                        // Å¸ÀÔ B
                    }
                    else if (list[j].ToUpper() == strGuardStone15)
                    {
                        iRequiredGuardStoneGrade = 3;
                        // Å¸ÀÔ C (¼öÈ£¼®Àº ÀÏ¹Ý, ±¤¼®¼øµµ 15ÀÌ»ó)
                    }
                }
                // ------ Àç·á °Ë»ç ------//
                // 1.Àü¼Û¹®ÀÚ¿­ÀÌ °¡¹æÃ¢¿¡ ¸ðµÎ ÀÖ´ÂÁö(Name°ú MakeIndex) °Ë»ç
                // ÀÖÀ¸¸é List¿¡¼­ ÇØ´ç ¾ÆÀÌÅÛ ÀÌ¸§°ú MakeIndex ¾÷µ¥ÀÌÆ®(StdModeÂüÁ¶)
                for (k = 0; k < ObjNpc.MAX_SOURCECNT; k++)
                {
                    // Àü¼Û¹®ÀÚ¿­
                    sourcemindex = HUtil32.Str_ToInt(sItemMakeIndex[k], 0);
                    sourcename = sItemName[k];
                    sourcecount = HUtil32.Str_ToInt(sItemCount[k], 0);
                    for (i = 0; i < hum.ItemList.Count; i++)
                    {
                        pu = hum.ItemList[i];
                        // ¾ÆÀÌÅÛ ÀÌ¸§ ºñ±³
                        if (sItemName[k] == svMain.UserEngine.GetStdItemName(pu.Index))
                        {
                            ps = svMain.UserEngine.GetStdItem(pu.Index);
                            if (ps != null)
                            {
                                // Ä«¿îÆ® ¾ÆÀÌÅÛ.
                                if (ps.OverlapItem >= 1)
                                {
                                    if (pu.Dura < sourcecount)
                                    {
                                        sourcecount = sourcecount - pu.Dura;
                                    }
                                    else
                                    {
                                        itemp = sourcecount;
                                        sourcecount = _MAX(0, itemp - pu.Dura);
                                    }
                                    if (sourcecount <= 0)
                                    {
                                        for (j = 0; j < list.Count; j++)
                                        {
                                            if (list[j] == sourcename)
                                            {
                                                sNewMIndex[j] = sItemMakeIndex[k];
                                                sNewName[j] = sourcename;
                                                sNewCount[j] = sItemCount[k];
                                            }
                                        }
                                        break;
                                    }
                                }
                                else
                                {
                                    // MakeIndex ºñ±³
                                    if (sourcemindex == pu.MakeIndex)
                                    {
                                        for (j = 0; j < list.Count; j++)
                                        {
                                            if (list[j] == sourcename)
                                            {
                                                // °°Àº ¾ÆÀÌÅÛÀÌ µÎ°³ ÀÌ»ó ÀÖÀ» °æ¿ì Ä«¿îÆ® Áõ°¡
                                                if (sNewName[j] == sourcename)
                                                {
                                                    sNewCount[j] = (HUtil32.Str_ToInt(sNewCount[j], 0) + 1).ToString();
                                                }
                                                else
                                                {
                                                    sNewCount[j] = sItemCount[k];
                                                    sNewMIndex[j] = sItemMakeIndex[k];
                                                }
                                                sNewName[j] = sourcename;
                                            }
                                        }
                                        // <Àå½Å±¸> <¼öÈ£¼®> <¼öÈ£¼®(Æ¯)ÀÌ»ó> ¾ÆÀÌÅÛÀÌ ÀÖÀ¸¸é
                                        // Àü¼Û¹®ÀÚ¿­¿¡ ÀÖ´Â ¾ÆÀÌÅÛÀ» list¿¡ µî·ÏÇÑ´Ù.
                                        if (new ArrayList(new int[] { 19, 20, 21, 22, 23, 24, 26 }).Contains(ps.StdMode))
                                        {
                                            for (j = 0; j < list.Count; j++)
                                            {
                                                if (list[j].ToUpper() == strPendant)
                                                {
                                                    sNewMIndex[j] = sItemMakeIndex[k];
                                                    sNewName[j] = sourcename;
                                                    sNewCount[j] = sItemCount[k];
                                                    // Àå½Å±¸ ÆÄ±«,¸¶·Â,µµ·Â ÃÑÇÕ¿¡ µû¶ó µî±Þ °áÁ¤...
                                                    iSumOutfitAbil = HiByte(ps.DC) + HiByte(ps.MC) + HiByte(ps.SC);
                                                    if (new ArrayList(new int[] { 22, 23 }).Contains(ps.StdMode))
                                                    {
                                                        // ¹ÝÁö
                                                        if (iSumOutfitAbil <= 3)
                                                        {
                                                            // °¡±º
                                                            iOutfitGrade = 0;
                                                        }
                                                        else if (iSumOutfitAbil == 4)
                                                        {
                                                            // ³ª±º
                                                            iOutfitGrade = 1;
                                                        }
                                                        else
                                                        {
                                                            iOutfitGrade = 2;
                                                        }
                                                        // ´Ù±º
                                                    }
                                                    else if (new ArrayList(new int[] { 24, 26 }).Contains(ps.StdMode))
                                                    {
                                                        // ÆÈÂî
                                                        if (HiByte(ps.DC) > 0)
                                                        {
                                                            // ÆÄ±« ºÙÀº ÆÈÂî
                                                            if (iSumOutfitAbil == 1)
                                                            {
                                                                // °¡±º
                                                                iOutfitGrade = 0;
                                                            }
                                                            else if (iSumOutfitAbil == 2)
                                                            {
                                                                // ³ª±º
                                                                iOutfitGrade = 1;
                                                            }
                                                            else
                                                            {
                                                                iOutfitGrade = 2;
                                                            }
                                                            // ´Ù±º
                                                        }
                                                        else
                                                        {
                                                            if (iSumOutfitAbil == 1)
                                                            {
                                                                // °¡±º
                                                                iOutfitGrade = 0;
                                                            }
                                                            else if (new ArrayList(new int[] { 2, 3 }).Contains(iSumOutfitAbil))
                                                            {
                                                                // ³ª±º
                                                                iOutfitGrade = 1;
                                                            }
                                                            else
                                                            {
                                                                iOutfitGrade = 2;
                                                            }
                                                            // ´Ù±º
                                                        }
                                                    }
                                                    else
                                                    {
                                                        // ¸ñ°ÉÀÌ
                                                        if (iSumOutfitAbil <= 3)
                                                        {
                                                            // °¡±º
                                                            iOutfitGrade = 0;
                                                        }
                                                        else if (new ArrayList(new int[] { 4, 5 }).Contains(iSumOutfitAbil))
                                                        {
                                                            // ³ª±º
                                                            iOutfitGrade = 1;
                                                        }
                                                        else
                                                        {
                                                            iOutfitGrade = 2;
                                                        }
                                                        // ´Ù±º
                                                    }
                                                }
                                            }
                                        }
                                        if (ps.StdMode == 53)
                                        {
                                            // ¼öÈ£¼® µî±ÞÀ» ¾ò¾î³½´Ù.
                                            iGuardStoneGrade = GetGradeOfGuardStoneByName(sourcename);
                                            for (j = 0; j < list.Count; j++)
                                            {
                                                if (iGuardStoneGrade < ObjNpc.GSG_GREATLARGE)
                                                {
                                                    if ((list[j].ToUpper() == strGuardStone) || (list[j].ToUpper() == strGuardStone15))
                                                    {
                                                        sNewMIndex[j] = sItemMakeIndex[k];
                                                        sNewName[j] = sourcename;
                                                        sNewCount[j] = sItemCount[k];
                                                    }
                                                }
                                                else if (iGuardStoneGrade >= ObjNpc.GSG_GREATLARGE)
                                                {
                                                    if ((list[j].ToUpper() == strGuardStone) || (list[j].ToUpper() == strGuardStone15) || (list[j].ToUpper() == strGuardStoneXLHigher))
                                                    {
                                                        sNewMIndex[j] = sItemMakeIndex[k];
                                                        sNewName[j] = sourcename;
                                                        sNewCount[j] = sItemCount[k];
                                                    }
                                                }
                                                else
                                                {
                                                    // ¼öÈ£¼® ÀÌ¸§ÀÌ ÀÌ»óÇÏ´Ù¸é Error : È®ÀÎÇØ ºÁ¾ßÇÔ.
                                                    svMain.MainOutMessage("[Caution!] TMerchant.UserManufactureItem iGuardStoneGrade = GSG_ERROR");
                                                }
                                            }
                                        }
                                        // ------ ±¤¼® ¼øµµ °Ë»ç ------//
                                        if (ps.StdMode == 43)
                                        {
                                            // ±¤¼®
                                            if (iRequiredGuardStoneGrade == 1)
                                            {
                                                // Å¸ÀÔ A
                                                if (pu.Dura < 11500)
                                                {
                                                    // ¼øµµ 12
                                                    condition = ObjNpc.COND_MINERALFAIL;
                                                }
                                            }
                                            else if (iRequiredGuardStoneGrade == 2)
                                            {
                                                // Å¸ÀÔ B
                                                if (pu.Dura < 14500)
                                                {
                                                    // ¼øµµ 15
                                                    condition = ObjNpc.COND_MINERALFAIL;
                                                }
                                            }
                                            else if (iRequiredGuardStoneGrade == 3)
                                            {
                                                // Å¸ÀÔ C
                                                if (pu.Dura < 14500)
                                                {
                                                    // ¼øµµ 15
                                                    condition = ObjNpc.COND_MINERALFAIL;
                                                }
                                            }
                                        }
                                        sourcecount -= 1;
                                        // °¹¼ö °¨¼Ò..
                                    }
                                }
                            }
                            // if ps <> nil then
                        }
                    }
                    if (sourcecount > 0)
                    {
                        condition = ObjNpc.COND_FAILURE;
                        // °¹¼ö ¹Ì´ÞÀÌ¸é Á¶°Ç ¾È¸ÂÀ½°£ÁÖ.
                        break;
                    }
                }
#if DEBUG
                for (k = 0; k < list.Count; k ++ )
                {
                    hum.SysMsg(sNewMIndex[k] + " " + sNewName[k] + " " + sNewCount[k], 2);
                }
#endif
                // 2.»õ·Î¿î List°¡ listÀÇ Á¶°Ç¿¡ ¸¸Á·ÇÏ´ÂÁö °Ë»ç
                // ¸î °³±îÁö ¸¸µé ¼ö ÀÖ´ÂÁö È®ÀÎ
                if ((condition == ObjNpc.COND_SUCCESS) || (condition == ObjNpc.COND_MINERALFAIL))
                {
                    checkcount = list.Count;
                    for (k = 0; k < list.Count; k++)
                    {
                        // »õ·Î¿î List
                        sourcename = sNewName[k];
                        sourcecount = HUtil32.Str_ToInt(sNewCount[k], 0);
                        if ((sourcename == list[k]) && (sourcecount >= ((int)list.Values[k])))
                        {
                            iListDoubleCount[k] = sourcecount / ((int)list.Values[k]);
                            checkcount -= 1;
                        }
                        else if (((list[k].ToUpper() == strPendant) || (list[k].ToUpper() == strGuardStone) || (list[k].ToUpper() == strGuardStone15) || (list[k].ToUpper() == strGuardStoneXLHigher)) && (sourcecount >= ((int)list.Values[k])))
                        {
                            iListDoubleCount[k] = sourcecount / ((int)list.Values[k]);
                            checkcount -= 1;
                        }
                    }
                    if (checkcount > 0)
                    {
                        condition = ObjNpc.COND_FAILURE;
                    }
                    // °¹¼ö ¹Ì´ÞÀÌ¸é Á¶°Ç ¾È¸ÂÀ½°£ÁÖ.
                }
                // ------ Àç·á »èÁ¦ ------//
                // °¡¹æÃ¢¿¡¼­ »õ·Î¿î List¿¡ ÇØ´çÇÏ´Â ¾ÆÀÌÅÛÀ» »èÁ¦ÇÑ´Ù.
                // ¸¸µé ¼ö ÀÖ´Â °³¼ö¸¸Å­ »èÁ¦ÇÏ°í ³ª¸ÓÁö´Â »èÁ¦ÇÏÁö ¾ÊÀ½...
                if (condition == ObjNpc.COND_SUCCESS)
                {
                    // ------ ¸¸µé ¼ö ÀÖ´Â °³¼ö °è»ê(ÃÖ¼Ò°ª) -----//
                    iMakeCount = iListDoubleCount[0];
                    for (k = 0; k < list.Count; k++)
                    {
                        if (iMakeCount > iListDoubleCount[k])
                        {
                            iMakeCount = iListDoubleCount[k];
                        }
                        // hum.SysMsg(IntToStr(iListDoubleCount[k]), 1);
                    }
                    // hum.SysMsg('¸¸µå´Â ¾ÆÀÌÅÛ °³¼ö : ' + IntToStr(iMakeCount), 2);
                    // ÇÊ¿äÇÑ ±ÝÀüÀÌ ÀÖ´ÂÁö È®ÀÎ
                    if (hum.Gold < iPrice * iMakeCount)
                    {
                        result = ObjNpc.COND_NOMONEY;
                        return result;
                    }
                    // °¡¹æÃ¢ ¿©ºÐÀÌ ÀÖ´ÂÁö È®ÀÎ (sonmg 2004/02/21)
                    if (hum.ItemList.Count + iMakeCount > Grobal2.MAXBAGITEM)
                    {
                        result = ObjNpc.COND_BAGFULL;
#if KOREA
                        hum.SysMsg("°¡¹æÀÌ °¡µæ Â÷¼­ Á¦Á¶¸¦ ÇÒ ¼ö ¾ø½À´Ï´Ù.", 0);
#else
                        hum.SysMsg("Your bag is full.", 0);
#endif
                        return result;
                    }
                    dellist = null;
                    for (j = 0; j < iMakeCount; j++)
                    {
                        for (k = 0; k < list.Count; k++)
                        {
                            // »õ·Î¿î List
                            sourcemindex = HUtil32.Str_ToInt(sNewMIndex[k], 0);
                            sourcename = sNewName[k];
                            // Ä«¿îÆ®´Â listÀÇ Ä«¿îÆ®¸¸Å­ »èÁ¦(ÇÊ¿äÇÑ ¸¸Å­¸¸ »èÁ¦)
                            sourcecount = (int)list.Values[k];
                            counteritmcount = (int)list.Values[k];
                            for (i = hum.ItemList.Count - 1; i >= 0; i--)
                            {
                                pu = hum.ItemList[i];
                                if (sourcecount > 0)
                                {
                                    if (sourcename == svMain.UserEngine.GetStdItemName(pu.Index))
                                    {
                                        ps = svMain.UserEngine.GetStdItem(pu.Index);
                                        if (ps != null)
                                        {
                                            // Ä«¿îÆ® ¾ÆÀÌÅÛ.
                                            if (ps.OverlapItem >= 1)
                                            {
                                                if (pu.Dura < counteritmcount)
                                                {
                                                    counteritmcount = counteritmcount - pu.Dura;
                                                    pu.Dura = 0;
                                                }
                                                else
                                                {
                                                    itemp = counteritmcount;
                                                    counteritmcount = _MAX(0, itemp - pu.Dura);
                                                    pu.Dura = (ushort)(pu.Dura - itemp);
                                                }
                                                if (pu.Dura > 0)
                                                {
                                                    hum.SendMsg(this, Grobal2.RM_COUNTERITEMCHANGE, 0, pu.MakeIndex, pu.Dura, 0, ps.Name);
                                                    continue;
                                                }
                                            }
                                            else
                                            {
                                                // MakeIndex ºñ±³
                                                if (pu.MakeIndex != HUtil32.Str_ToInt(sNewMIndex[k], 0))
                                                {
                                                    bCheckMIndex = false;
                                                    for (icnt = 0; icnt < ObjNpc.MAX_SOURCECNT; icnt++)
                                                    {
                                                        if (pu.MakeIndex == HUtil32.Str_ToInt(sItemMakeIndex[icnt], 0))
                                                        {
                                                            bCheckMIndex = true;
                                                            break;
                                                        }
                                                    }
                                                    if (bCheckMIndex == false)
                                                    {
                                                        continue;
                                                    }
                                                }
                                            }
                                            // ÀÏ¹Ý ¾ÆÀÌÅÛ ¶Ç´Â Ä«¿îÆ® ¾ÆÀÌÅÛ »èÁ¦
                                            if (dellist == null)
                                            {
                                                dellist = new ArrayList();
                                            }
                                            delitemname = svMain.UserEngine.GetStdItemName(pu.Index);
                                            dellist.Add(delitemname, hum.ItemList[i].MakeIndex as Object);
                                            // ·Î±×³²±è
                                            // Á¦Á¶»è_
                                            svMain.AddUserLog("44\09" + hum.MapName + "\09" + hum.CX.ToString() + "\09" + hum.CY.ToString() + "\09" + hum.UserName + "\09" + delitemname + "\09" + hum.ItemList[i].MakeIndex.ToString() + "\09" + "1\09" + this.UserName);
                                            Dispose(hum.ItemList[i]);
                                            hum.ItemList.RemoveAt(i);
                                            sourcecount -= 1;
                                        }
                                        // if ps <> nil then
                                    }
                                }
                                else
                                {
                                    break;
                                }
                            }
                        }
                    }
                    if (dellist != null)
                    {
                        // dellistÀº  RM_DELITEMS¿¡¼­ FreeµÊ.
                        hum.SendMsg(this, Grobal2.RM_DELITEMS, 0, (int)dellist, 0, 0, "");
                    }
                    // °øÅë(1Â÷) º¸¿Á Á¦Á¶ È®·ü Àû¿ë...
                    if (iRequiredGuardStoneGrade > 0)
                    {
                        fTemporary = (hum.BodyLuck - hum.PlayerKillingPoint) / 250;
                        if (iRequiredGuardStoneGrade == 1)
                        {
                            iProbability = 50;
                        }
                        else if (iRequiredGuardStoneGrade == 2)
                        {
                            iProbability = 50;
                        }
                        else if (iRequiredGuardStoneGrade == 3)
                        {
                            iProbability = 50;
                        }
                        if (fTemporary >= 100)
                        {
                            iProbability = iProbability + 5;
                        }
                        else if ((fTemporary < 100) && (fTemporary >= 50))
                        {
                            iProbability = iProbability + 3;
                        }
                        switch (iGuardStoneGrade)
                        {
                            case ObjNpc.GSG_SMALL:
                                // ¼öÈ£¼®º° Ãß°¡ È®·ü Àû¿ë (sonmg 2003/12/19)
                                iProbability = iProbability + 5;
                                break;
                            case ObjNpc.GSG_MEDIUM:
                                iProbability = iProbability + 10;
                                break;
                            case ObjNpc.GSG_LARGE:
                                iProbability = iProbability + 15;
                                break;
                            case ObjNpc.GSG_GREATLARGE:
                                iProbability = iProbability + 30;
                                break;
                            case ObjNpc.GSG_SUPERIOR:
                                iProbability = iProbability + 50;
                                break;
                        }
                    }
                    // 2Â÷ º¸¿Á Á¦Á¶ È®·ü Àû¿ë...
                    if ((iRequiredGuardStoneGrade == 1) || (iRequiredGuardStoneGrade == 3))
                    {
                        // º¸¿Á Å¸ÀÔA Á¦Á¶ È®·ü Àû¿ë...
                        if (iOutfitGrade == 0)
                        {
                            iProbability = iProbability + 10;
                        }
                        else if (iOutfitGrade == 1)
                        {
                            iProbability = iProbability + 20;
                        }
                        else if (iOutfitGrade == 2)
                        {
                            iProbability = iProbability + 40;
                        }
#if DEBUG
                        // test
                        hum.SysMsg("BodyLuck:" + Convert.ToString(hum.BodyLuck) + " - PKPoint:" + Convert.ToString(hum.PlayerKillingPoint) + " = " + Convert.ToString(fTemporary) + ", iProbability:" + (iProbability).ToString() + ", DC/MC/SC SUM :" + (iSumOutfitAbil).ToString(), 0);
#endif
                        if (new System.Random(100).Next() < iProbability)
                        {
                            condition = ObjNpc.COND_SUCCESS;
                        }
                        else
                        {
                            condition = ObjNpc.COND_GEMFAIL;
                        }
                    }
                    else if (iRequiredGuardStoneGrade == 2)
                    {
                        // º¸¿Á Å¸ÀÔB Á¦Á¶ È®·ü Àû¿ë...
                        // 2Â÷ È®·ü ¾øÀ½.
#if DEBUG
                        // test
                        hum.SysMsg("BodyLuck:" + Convert.ToString(hum.BodyLuck) + " - PKPoint:" + Convert.ToString(hum.PlayerKillingPoint) + " = " + Convert.ToString(fTemporary) + ", iProbability:" + (iProbability).ToString(), 0);
#endif
                        if (new System.Random(100).Next() < iProbability)
                        {
                            condition = ObjNpc.COND_SUCCESS;
                        }
                        else
                        {
                            condition = ObjNpc.COND_GEMFAIL;
                        }
                    }
                }
            }
            // 
            // if condition = COND_GEMFAIL then begin
            // // º¸¿Á Á¦Á¶ ½ÇÆÐ ·Î±×
            // MainOutMessage( '[Jewel Manufacture Failure] ' + hum.UserName + ' ' + itemname + ' '
            // + '=> »èÁ¦µÈ Àç·á:' + sNewName[0] + ', ' + sNewName[1]
            // + ', ' + sNewName[2] + ', ' + sNewName[3]
            // + ', ' + sNewName[4] + ', ' + sNewName[5] + ' '
            // + 'BodyÇà¿î:' + FloatToStr(hum.BodyLuck)
            // + ' - PKÆ÷ÀÎÆ®:' + FloatToStr(hum.PlayerKillingPoint)
            // + ' / 250 = ' + FloatToStr(fTemporary) + ', Prob.Manufacture Gem:' + IntToStr(iProbability) );
            // end;
            if (condition == ObjNpc.COND_SUCCESS)
            {
                // Á¦Á¶ ¼º°ø ·Î±× -> Ãà¼Ò
                svMain.MainOutMessage("[Manufacture Success] " + hum.UserName + " " + itemname + "(" + iMakeCount.ToString() + ")" + " " + "=> Deleted Items:" + sNewName[0] + ", " + sNewName[1] + ", " + sNewName[2] + ", " + sNewName[3] + ", " + sNewName[4] + ", " + sNewName[5] + " " + "BodyLuck:" + Convert.ToString(hum.BodyLuck) + " - PKPoint:" + Convert.ToString(hum.PlayerKillingPoint) + " / 250 = " + Convert.ToString(fTemporary) + ", Prob.Manufacture Gem:" + iProbability.ToString());
            }
            result = condition;
            return result;
        }

        // ------------------------------------------------------------------------
        // À§Å¹»óÁ¡
        // À§Å¹»óÁ¡
        // Mode : 0 = ÀüÃ¼ , 1~13 Á¾·ùº° , 100 = ¼ÂÆ®¾ÆÀÌÅÛ , 200 = ÀÚ°¡ÀÚ½ÅÀÌ ¿Ã¸°°Å
        public void SendUserMarket(TUserHuman hum, int ItemType, int UserMode)
        {
            switch (UserMode)
            {
                case Grobal2.USERMARKET_MODE_BUY:
                case Grobal2.USERMARKET_MODE_INQUIRY:
                    // »ç´Â¸ðµå
                    // Á¶È¸¸ðµå
                    hum.RequireLoadUserMarket(svMain.ServerName + "_" + this.UserName, ItemType, UserMode, "", "");
                    break;
                case Grobal2.USERMARKET_MODE_SELL:
                    // ÆÇ¸Å¸ðµå
                    hum.SendUserMarketSellReady(this);
                    break;
                    // NPC ¸¦ ³Ñ°ÜÁØ´Ù.
            }
        }

        public override void RunMsg(TMessageInfo msg)
        {
            base.RunMsg(msg);
        }

        public override void Run()
        {
            int flag;
            long dwCurrentTick;
            long dwDelayTick;
            flag = 0;
            try
            {
                // --------------------------------
                // Merchant ºÎÇÏ ºÐ»ê ÄÚµå(sonmg 2005/02/01)
                dwCurrentTick = GetTickCount;
                dwDelayTick = CreateIndex * 500;
                if (dwCurrentTick < dwDelayTick)
                {
                    dwDelayTick = 0;
                }
                // --------------------------------
                if (dwCurrentTick - checkrefilltime > 5 * 60 * 1000 + dwDelayTick)
                {
                    checkrefilltime = dwCurrentTick - dwDelayTick;
                    flag = 1;
                    RefillGoods();
                    flag = 2;
                }
                // 10 * 60
                if (dwCurrentTick - checkverifytime > 601 * 1000)
                {
                    checkverifytime = dwCurrentTick;
                    flag = 3;
                    VerifyUpgradeList();
                    flag = 4;
                }
                if (new System.Random(50).Next() == 0)
                {
                    this.Turn((byte)new System.Random(8).Next());
                }
                else if (new System.Random(80).Next() == 0)
                {
                    this.SendRefMsg(Grobal2.RM_HIT, this.Dir, this.CX, this.CY, 0, "");
                }
                if (BoCastleManage && svMain.UserCastle.BoCastleUnderAttack)
                {
                    flag = 5;
                    // ÀüÀïÁß¿¡ »çºÏ¼º¾ÈÀÇ »óÁ¡Àº ¹®À» ´Ý´Â´Ù.
                    if (!this.HideMode)
                    {
                        this.SendRefMsg(Grobal2.RM_DISAPPEAR, 0, 0, 0, 0, "");
                        this.HideMode = true;
                    }
                    flag = 6;
                }
                else
                {
                    if (!BoHiddenNpc)
                    {
                        // Æò»ó½Ã
                        if (this.HideMode)
                        {
                            this.HideMode = false;
                            this.SendRefMsg(Grobal2.RM_HIT, this.Dir, this.CX, this.CY, 0, "");
                        }
                    }
                }
            }
            catch
            {
                svMain.MainOutMessage("[Exception] Merchant.Run (" + flag.ToString() + ") " + MarketName + "-" + this.MapName);
            }
            base.Run();
        }

        // ------------------------------------------------------------------------
        // Àå¿ø²Ù¹Ì±â
        // Àå¿ø²Ù¹Ì±â ¾ÆÀÌÅÛ ¸ñ·Ï º¸³»±â
        public void SendDecoItemListShow(TCreature who)
        {
            int count;
            string data = string.Empty;
            data = "";
            count = 0;
            who.SendMsg(this, Grobal2.RM_DECOITEM_LISTSHOW, 0, this.ActorId, count, 0, data);
        }

    } // end TMerchant

    public class TGuildOfficial : TNormNpc
    {

        public TGuildOfficial() : base()
        {
            this.RaceImage = Grobal2.RCC_MERCHANT;
            // »óÀÎ
            this.Appearance = 8;
        }

        ~TGuildOfficial()
        {
            base.Destroy();
        }

        public override void UserCall(TCreature caller)
        {
            this.NpcSayTitle(caller, "@main");
        }

        private int UserBuildGuildNow(TUserHuman hum, string gname)
        {
            int result;
            TUserItem pu;
            result = 0;
            // ¹®ÆÄ¸¦ ¸¸µé ÀÚ°ÝÀÌ ÀÖ´ÂÁö °Ë»ç
            // ¹®ÆÄ¿¡ °¡ÀÔÇÑ ÀûÀÌ ¾ø°í
            // µ·100¸¸¿ø, ¿ì¸é±Í¿ÕÀÇ »Ô
            gname = gname.Trim();
            pu = null;
            if (gname == "")
            {
                result = -4;
            }
            if (hum.MyGuild == null)
            {
                if (hum.Gold >= svMain.BUILDGUILDFEE)
                {
                    pu = hum.FindItemName(svMain.__WomaHorn);
                    if (pu != null)
                    {
                        // Á¶°Ç ¼º¸³
                    }
                    else
                    {
                        result = -3;
                    }
                    // Á¶°Ç ¾ÆÀÌÅÛÀÌ ¾øÀ½
                }
                else
                {
                    result = -2;
                }
                // µ·ÀÌ ºÎÁ·
            }
            else
            {
                result = -1;
            }
            // ÀÌ¹Ì ¹®ÆÄ¿¡ °¡ÀÔµÇ¾î ÀÖÀ½.
            // ¹®ÆÄ¸¦ ¸¸µç´Ù.
            if (result == 0)
            {
                if (svMain.GuildMan.AddGuild(gname, hum.UserName))
                {
                    svMain.UserEngine.SendInterMsg(Grobal2.ISM_ADDGUILD, svMain.ServerIndex, gname + "/" + hum.UserName);
                    hum.SendDelItem(pu);
                    // Å¬¶óÀÌ¾ðÆ®¿¡ º¸³¿
                    hum.DelItem(pu.MakeIndex, svMain.__WomaHorn);
                    hum.DecGold(svMain.BUILDGUILDFEE);
                    hum.GoldChanged();
                    // ¹®ÆÄÁ¤º¸¸¦ ´Ù½Ã ÀÐ´Â´Ù.
                    hum.MyGuild = svMain.GuildMan.GetGuildFromMemberName(hum.UserName);
                    if (hum.MyGuild != null)
                    {
                        // ±æµå¿¡ °¡ÀÔµÇ¾î ÀÖ´Â °æ¿ì
                        hum.GuildRankName = ((TGuild)hum.MyGuild).MemberLogin(hum, ref hum.GuildRank);
                        // hum.SendMsg (self, RM_CHANGEGUILDNAME, 0, 0, 0, 0, '');
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
            if (svMain.GuildMan.GetGuild(gname) != null)
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
                    hum.SysMsg("È±ÉÙ½ð±Ò¡£", 0);
                }
            }
            else
            {
                hum.SysMsg(gname + " ÕÒ²»µ½ÃÅÅÉ¡£", 0);
            }
            result = 1;
            return result;
        }

        private int UserFreeGuild(TUserHuman hum)
        {
            int result;
            result = 1;
            return result;
        }

        private void UserDonateGold(TUserHuman hum)
        {
            hum.SendMsg(this, Grobal2.RM_DONATE_FAIL, 0, 0, 0, 0, "");
        }

        private void UserRequestCastleWar(TUserHuman hum)
        {
            TUserItem pu;
            if (hum.IsGuildMaster() && (!svMain.UserCastle.IsOurCastle((TGuild)hum.MyGuild)))
            {
                pu = hum.FindItemName(svMain.__ZumaPiece);
                if (pu != null)
                {
                    if (svMain.UserCastle.ProposeCastleWar((TGuild)hum.MyGuild))
                    {
                        hum.SendDelItem(pu);
                        hum.DelItem(pu.MakeIndex, svMain.__ZumaPiece);
                        svMain.AddUserLog("10\09" + hum.MapName + "\09" + hum.CX.ToString() + "\09" + hum.CY.ToString() + "\09" + hum.UserName + "\09" + svMain.__ZumaPiece + "\09" + "0" + "\09" + "1\09" + this.UserName);
                        this.NpcSayTitle(hum, "~@request_ok");
                    }
                    else
                    {
                        hum.SysMsg("Äú´Ë¿ÌÎÞ·¨ÇëÇó¡£", 0);
                    }
                }
                else
                {
                    hum.SysMsg("ÄúÃ»ÓÐ×æÂêÍ·Ïñ¡£", 0);
                }
            }
            else
            {
                // ¹®ÆÄ°¡ ¾ø°Å³ª, ¼ºÀÇ ÁÖÀÎ¹®ÆÄ°¡ ½ÅÃ»ÇÑ °æ¿ì
                hum.SysMsg("ÄúµÄÇëÇó±»È¡Ïû¡£", 0);
            }
            hum.SendMsg(this, Grobal2.RM_MENU_OK, 0, 0, 0, 0, "");
        }

        public override void UserSelect(TCreature whocret, string selstr)
        {
            string sel = String.Empty;
            string body= string.Empty;
            try
            {
                if (selstr != "")
                {
                    if (selstr[1] == "@")
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
                svMain.MainOutMessage("[Exception] TGuildOfficial.UserSelect... ");
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

    } // end TGuildOfficial

    public class TTrainer : TNormNpc
    {
        private long strucktime = 0;
        private int damagesum = 0;
        private int struckcount = 0;
        // -------------------------------------------------------
        //Constructor  Create()
        public TTrainer() : base()
        {
            strucktime = GetTickCount;
            damagesum = 0;
            struckcount = 0;
        }
        public override void RunMsg(TMessageInfo msg)
        {
            switch (msg.Ident)
            {
                case Grobal2.RM_REFMESSAGE:
                    if ((((int)msg.sender) == Grobal2.RM_STRUCK) && (msg.wParam != 0))
                    {
                        damagesum = damagesum + msg.wParam;
                        strucktime = GetTickCount;
                        struckcount++;
                        this.Say("ÆÆ»µÁ¦Îª" + msg.wParam.ToString() + "£¬Æ½¾ùÖµÎª" + (damagesum / struckcount).ToString() + "¡£");
                    }
                    break;
                case Grobal2.RM_MAGSTRUCK:
                    // RM_STRUCK:
                    // begin
                    // if (msg.Sender = self) and (msg.lParam3 <> 0) then begin
                    // damagesum := damagesum + msg.wParam;
                    // strucktime := GetTickCount;
                    // Inc (struckcount);
                    // {$IFDEF KOREA} Say ('ÆÄ±«·ÂÀº ' + IntToStr(msg.wParam) + ', Æò±ÕÀº ' + IntToStr(damagesum div struckcount));
                    // {$ELSE}        Say ('Destructive power is ' + IntToStr(msg.wParam) + ', Average  is ' + IntToStr(damagesum div struckcount));
                    // {$ENDIF}
                    // end;
                    // end;
                    // (msg.Sender = self) and
                    if (msg.lParam1 != 0)
                    {
                        damagesum = damagesum + msg.lParam1;
                        strucktime = GetTickCount;
                        struckcount++;
                        this.Say("×ÜÆÆ»µÁ¦Îª" + msg.lParam1.ToString() + "£¬Æ½¾ùÆÆ»µÁ¦Îª" + (damagesum / struckcount).ToString() + "¡£");
                    }
                    break;
            }
        }

        public override void Run()
        {
            if (struckcount > 0)
            {
                if (GetTickCount - strucktime > 3 * 1000)
                {
                    this.Say("×ÜÆÆ»µÁ¦Îª" + damagesum.ToString() + "£¬Æ½¾ùÆÆ»µÁ¦Îª" + (damagesum / struckcount).ToString() + "¡£");
                    struckcount = 0;
                    damagesum = 0;
                }
            }
            base.Run();
        }

    } // end TTrainer

    public class TCastleManager : TMerchant
    {
        // TCastleManager, (»çºÏ¼º¿¡¸¸ ÇØ´çµÊ)
        // ¹®¿øµé¿¡°Ô¸¸ Å¬¸¯ÀÌ µÇ°Ô ÇÏ°í, ¼ºÁÖ¿¡°Ô¸¸ µ· ÀÔ±Ý, Ãâ±ÝÀ»
        // ÇÒ ¼ö ÀÖ°Ô ÇÑ´Ù.
        //Constructor  Create()
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
                    str = "ÒÑ¾­Ëð»µÁË¡£";
                }
                else if (_wvar1.BoOpenState)
                {
                    str = "ÒÑ¿ªÆô";
                }
                else
                {
                    str = "ÒÑ¹Ø±Õ";
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
                    hum.SysMsg("ÐÞÀíÍê³É¡£", 1);
                }
                else
                {
                    hum.SysMsg("ÄúÏÖÔÚÎÞ·¨ÐÞÀí¡£", 0);
                }
            }
            else
            {
                hum.SysMsg("³ÇÄÚ×Ê½ð²»×ã¡£", 0);
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
                    hum.SysMsg("ÐÞÀíÍê³É¡£", 1);
                }
                else
                {
                    hum.SysMsg("ÄúÏÖÔÚÎÞ·¨ÐÞÀí¡£", 0);
                }
            }
            else
            {
                hum.SysMsg("³ÇÄÚ×Ê½ð²»×ã¡£", 0);
            }
        }

        private void HireCastleGuard(string numstr, TUserHuman hum)
        {
            int gnum;
            if (svMain.UserCastle.TotalGold >= ObjNpc.CASTLEGUARDEMPLOYFEE)
            {
                gnum = HUtil32.Str_ToInt(numstr, 0) - 1;
                if (gnum >= 0 && gnum <= Castle.Units.Castle.MAXGUARD - 1)
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
                                // TGuardUnit(UnitObj).OriginX := X;
                                // TGuardUnit(UnitObj).OriginY := Y;
                                // TGuardUnit(UnitObj).OriginDir := 3;
                                hum.SysMsg("¹ÍÓÃÎÀÊ¿¡£", 1);
                            }
                        }
                        else
                        {
                            hum.SysMsg("ÏÖÔÚÎÞ·¨¹ÍÓÃ¡£", 0);
                        }
                    }
                    else
                    {
                        if (!svMain.UserCastle.Guards[gnum].UnitObj.Death)
                        {
                            hum.SysMsg("ÄÇÀïÒÑ¾­ÓÐÎÀÊ¿ÁË¡£", 0);
                        }
                        else
                        {
                            hum.SysMsg("ÏÖÔÚÎÞ·¨¹ÍÓÃ¡£", 0);
                        }
                    }
                }
                else
                {
                    hum.SysMsg("´íÎóµÄÃüÁî¡£", 0);
                }
            }
            else
            {
                hum.SysMsg("³ÇÄÚ×Ê½ð²»×ã¡£", 0);
            }
        }

        private void HireCastleArcher(string numstr, TUserHuman hum)
        {
            int gnum;
            if (svMain.UserCastle.TotalGold >= ObjNpc.CASTLEARCHEREMPLOYFEE)
            {
                gnum = HUtil32.Str_ToInt(numstr, 0) - 1;
                if (gnum >= 0 && gnum <= Units.Castle.MAXARCHER - 1)
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
                                hum.SysMsg("¹ÍÓÃÁË¹­¼ýÊÖ¡£", 1);
                            }
                        }
                        else
                        {
                            hum.SysMsg("ÏÖÔÚÎÞ·¨¹ÍÓÃ¡£", 0);
                        }
                    }
                    else
                    {
                        if (!svMain.UserCastle.Archers[gnum].UnitObj.Death)
                        {
                            hum.SysMsg("ÄÇÀïÒÑ¾­ÓÐÎÀÊ¿ÁË¡£", 0);
                        }
                        else
                        {
                            hum.SysMsg("ÏÖÔÚÎÞ·¨¹ÍÓÃ¡£", 0);
                        }
                    }
                }
                else
                {
                    hum.SysMsg("´íÎóµÄÃüÁî", 0);
                }
            }
            else
            {
                hum.SysMsg("³ÇÄÚ×Ê½ð²»×ã", 0);
            }
        }

        public override void UserCall(TCreature caller)
        {
            if (svMain.UserCastle.IsOurCastle((TGuild)caller.MyGuild))
            {
                base.UserCall(caller);
            }
        }

        public override void UserSelect(TCreature whocret, string selstr)
        {
            string body= string.Empty;
            string sel = String.Empty;
            string rmsg = String.Empty;
            try
            {
                if (selstr != "")
                {
                    if (selstr[1] == "@")
                    {
                        body = HUtil32.GetValidStr3(selstr, ref sel, new char[] { '\r' });
                        rmsg = "";
                        while (true)
                        {
                            whocret.LatestNpcCmd = selstr;
                            this.NpcSayTitle(whocret, sel);
                            // »çºÏ¼º¿¡¼­ÀÇ ¹®ÁÖ¸í·É
                            if (svMain.UserCastle.IsOurCastle((TGuild)whocret.MyGuild) && whocret.IsGuildMaster())
                            {
                                if (sel.ToLower().CompareTo("@@withdrawal".ToLower()) == 0)
                                {
                                    switch (svMain.UserCastle.GetBackCastleGold((TUserHuman)whocret, Math.Abs(HUtil32.Str_ToInt(body, 0))))
                                    {
                                        case -1:
                                            rmsg = svMain.UserCastle.OwnerGuildName + "Ö»ÓÐÒÔÏÂÃÅÅÉµÄÃÅÖ÷²ÅÄÜÊ¹ÓÃ£º";
                                            break;
                                        case -2:
                                            rmsg = "¸Ã³ÇÄÚÃ»ÓÐÕâÃ´¶à½ð±Ò¡£";
                                            break;
                                        case -3:
                                            rmsg = "ÄúÎÞ·¨Ð¯´ø¸ü¶àµÄ¶«Î÷¡£";
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
                                            rmsg = svMain.UserCastle.OwnerGuildName + "Ö»ÓÐÒÔÏÂÃÅÅÉµÄÃÅÖ÷²ÅÄÜÊ¹ÓÃ£º";
                                            break;
                                        case -2:
                                            rmsg = "ÄúÃ»ÓÐÄÇÃ´¶à½ð±Ò¡£";
                                            break;
                                        case -3:
                                            rmsg = "ÄúÒÑ¾­´ïµ½ÔÚ³ÇÄÚ´æ·ÅÎïÆ·µÄÏÞÖÆÁË¡£";
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
                                    // UserSelect (whocret, '@hirearchers');
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
                                whocret.SendMsg(this, Grobal2.RM_MENU_OK, 0, this.ActorId, 0, 0, "ÄúÃ»ÓÐÊ¹ÓÃÈ¨ÏÞ¡£");
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

    } // end TCastleManager

    // ÀÏÁ¤ ¹üÀ§¾È¿¡ ¿À¸é ³ªÅ¸³ª´Â NPC(sonmg)
    public class THiddenNpc : TMerchant
    {
        protected bool RunDone = false;
        protected int DigupRange = 0;
        protected int DigdownRange = 0;
        // ---------------------------HiddenNpc-----------------------------
        //Constructor  Create()
        public THiddenNpc() : base()
        {
            RunDone = false;
            this.ViewRange = 7;
            this.RunNextTick = 250;
            this.SearchRate = 2500 + ((long)new System.Random(1500).Next());
            this.SearchTime = GetTickCount;
            DigupRange = 4;
            DigdownRange = 4;
            this.HideMode = true;
            this.StickMode = true;
            this.BoHiddenNpc = true;
        }
        //@ Destructor  Destroy()
        ~THiddenNpc()
        {
            base.Destroy();
        }
        protected void ComeOut()
        {
            this.HideMode = false;
            // SendRefMsg (RM_DIGUP, Dir, CX, CY, 0, '');
            this.SendRefMsg(Grobal2.RM_HIT, this.Dir, this.CX, this.CY, 0, "");
        }

        protected void ComeDown()
        {
            int i;
            // SendRefMsg (RM_DIGDOWN, {Dir}0, CX, CY, 0, '');
            this.SendRefMsg(Grobal2.RM_DISAPPEAR, 0, 0, 0, 0, "");
            try
            {
                for (i = 0; i < this.VisibleActors.Count; i++)
                {
                    Dispose(this.VisibleActors[i]);
                }
                this.VisibleActors.Clear();
            }
            catch
            {
                svMain.MainOutMessage("[Exception] THiddenNpc VisbleActors Dispose(..)");
            }
            this.HideMode = true;
        }

        protected void CheckComeOut()
        {
            int i;
            TCreature cret;
            for (i = 0; i < this.VisibleActors.Count; i++)
            {
                cret = (TCreature)this.VisibleActors[i].cret;
                if ((!cret.Death) && this.IsProperTarget(cret) && (!cret.BoHumHideMode || this.BoViewFixedHide))
                {
                    if ((Math.Abs(this.CX - cret.CX) <= DigupRange) && (Math.Abs(this.CY - cret.CY) <= DigupRange))
                    {
                        ComeOut();
                        // ¹ÛÀ¸·Î ³ª¿À´Ù. º¸ÀÎ´Ù.
                        break;
                    }
                }
            }
        }

        public override void RunMsg(TMessageInfo msg)
        {
            base.RunMsg(msg);
        }

        public override void Run()
        {
            bool boidle;
            TCreature nearcret;
            nearcret = null;
            // if (not BoGhost) and (not Death) and
            // (StatusArr[POISON_STONE] = 0) and (StatusArr[POISON_ICE] = 0) and
            // (StatusArr[POISON_STUN] = 0) then begin
            if (this.IsMoveAble())
            {
                if (GetCurrentTime - this.WalkTime > this.GetNextWalkTime())
                {
                    this.WalkTime = GetCurrentTime;
                    if (this.HideMode)
                    {
                        // ¾ÆÁ÷ ¸ð½ÀÀ» ³ªÅ¸³»Áö ¾Ê¾ÒÀ½.
                        CheckComeOut();
                    }
                    else
                    {
                        if (GetCurrentTime - this.HitTime > this.GetNextHitTime())
                        {
                            // »ó¼Ó¹ÞÀº run ¿¡¼­ HitTime Àç¼³Á¤ÇÔ.
                            // HitTime := GetTickCount; //¾Æ·¡ AttackTarget¿¡¼­ ÇÔ.
                            nearcret = this.GetNearMonster();
                        }
                        boidle = false;
                        if (nearcret != null)
                        {
                            if ((Math.Abs(nearcret.CX - this.CX) > DigdownRange) || (Math.Abs(nearcret.CY - this.CY) > DigdownRange))
                            {
                                boidle = true;
                            }
                        }
                        else
                        {
                            boidle = true;
                        }
                        if (boidle)
                        {
                            ComeDown();
                            // ´Ù½Ã µé¾î°£´Ù.
                        }
                    }
                }
            }
            base.Run();
        }

    } // end THiddenNpc

}

namespace GameSvr
{
    public class ObjNpc
    {
        public const int GUILDWARFEE = 30000;
        public const int CASTLEMAINDOORREPAREGOLD = 1500000;
        public const int CASTLECOREWALLREPAREGOLD = 400000;
        public const int CASTLEARCHEREMPLOYFEE = 250000;
        public const int CASTLEGUARDEMPLOYFEE = 250000;
        public const int UPGRADEWEAPONFEE = 10000;
        public const int MAXREQUIRE = 10;
        public const int MAX_SOURCECNT = 6;
        public const int COND_FAILURE = 0;
        public const int COND_GEMFAIL = 1;
        public const int COND_SUCCESS = 2;
        public const int COND_MINERALFAIL = 3;
        public const int COND_NOMONEY = 4;
        public const int COND_BAGFULL = 5;
        public const int GSG_ERROR = 0;
        public const int GSG_SMALL = 1;
        public const int GSG_MEDIUM = 2;
        public const int GSG_LARGE = 3;
        public const int GSG_GREATLARGE = 4;
        public const int GSG_SUPERIOR = 5;

        public static int GetPP(string str)
        {
            int result;
            int n;
            result = -1;
            if (str.Length == 2)
            {
                if (Char.ToUpper(str[1]) == "P")
                {
                    n = HUtil32.Str_ToInt(str[2], -1);
                    if (n >= 0 && n <= 9)
                    {
                        result = n;
                    }
                }
                if (Char.ToUpper(str[1]) == "G")
                {
                    n = HUtil32.Str_ToInt(str[2], -1);
                    if (n >= 0 && n <= 9)
                    {
                        result = 100 + n;
                    }
                }
                if (Char.ToUpper(str[1]) == "D")
                {
                    n = HUtil32.Str_ToInt(str[2], -1);
                    if (n >= 0 && n <= 9)
                    {
                        result = 200 + n;
                    }
                }
                if (Char.ToUpper(str[1]) == "M")
                {
                    n = HUtil32.Str_ToInt(str[2], -1);
                    if (n >= 0 && n <= 9)
                    {
                        result = 300 + n;
                    }
                }
            }
            return result;
        }

        public static void ReadStrings(string flname, ArrayList strlist)
        {
            System.IO.FileInfo f;
            string str;
            strlist.Clear();
            f = new FileInfo(flname);
            FileMode = 0;
            // Set file access to read only
            StreamReader _R_0 = f.OpenText();
            while (!(_R_0.BaseStream.Position >= _R_0.BaseStream.Length))
            {
                str = _R_0.ReadLine();
                strlist.Add(str);
            }
            _R_0.Close();
            // IOResult

        }

        public static void WriteStrings(string flname, ArrayList strlist)
        {
            System.IO.FileInfo f;
            int i;
            f = new FileInfo(flname);
            // FileMode := 2;  {Set file access to read only }
            StreamWriter _W_0 = f.CreateText();
            for (i = 0; i < strlist.Count; i++)
            {
                _W_0.WriteLine(strlist[i]);
            }
            _W_0.Close();
            // IOResult

        }

    } // end ObjNpc

}

