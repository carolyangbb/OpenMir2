using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using SystemModule;

namespace GameSvr
{
    public class TGuildAgitManager
    {
        public int GuildAgitFileVersion = 0;
        public string ReturnMapName = String.Empty;
        public int ReturnX = 0;
        public int ReturnY = 0;
        public int EntranceX = 0;
        public int EntranceY = 0;
        public string[] GuildAgitMapName;
        public IList<TGuildAgit> GuildAgitList = null;
        public IList<TAgitDecoItem> AgitDecoMonList = null;
        public int[] AgitDecoMonCount;

        public TGuildAgitManager() : base()
        {
            GuildAgitFileVersion = 0;
            GuildAgitList = new List<TGuildAgit>();
            AgitDecoMonList = new List<TAgitDecoItem>();
            GuildAgitMapName[0] = "GA0";
            GuildAgitMapName[1] = "GA1";
            GuildAgitMapName[2] = "GA2";
            GuildAgitMapName[3] = "GA3";
            ReturnMapName = "0";
            ReturnX = 333;
            ReturnY = 276;
            EntranceX = 119;
            EntranceY = 122;
            for (var i = 1; i <= Guild.MAXGUILDAGITCOUNT; i++)
            {
                AgitDecoMonCount[i] = 0;
            }
        }

        public void ClearGuildAgitList()
        {
            int i;
            for (i = 0; i < GuildAgitList.Count; i++)
            {
                (GuildAgitList[i] as TGuildAgit).Free();
            }
            GuildAgitList.Clear();
        }

        public void LoadGuildAgitList()
        {
            ArrayList strlist;
            int i;
            string str;
            string data = string.Empty;
            TGuildAgit guildagit;
            int agitnumber;
            int period;
            string gname;
            string mastername;
            string regtime;
            int saleflag;
            int salemoney;
            int salewait;
            string salegname;
            string salemastername;
            string saletime;
            int agittotalgold;
            agittotalgold = 0;
            if (File.Exists(svMain.GuildAgitFile))
            {
                strlist = new ArrayList();
                strlist.LoadFromFile(svMain.GuildAgitFile);
                for (i = 0; i < strlist.Count; i++)
                {
                    str = (string)strlist[i];
                    if (i == 0)
                    {
                        str = str.Trim();
                        if (str[0] == '[')
                        {
                            str = HUtil32.GetValidStr3(str, ref data, new string[] { "=", "[", " ", ",", "\09" });
                            if (data.ToUpper() == "VERSION")
                            {
                                str = HUtil32.GetValidStr3(str, ref data, new string[] { "=", "[", "]", " ", ",", "\09" });
                                GuildAgitFileVersion = HUtil32.Str_ToInt(data, 0);
                                continue;
                            }
                        }
                    }
                    if (str == "")
                    {
                        continue;
                    }
                    if (str[0] == ';')
                    {
                        continue;
                    }
                    str = HUtil32.GetValidStr3(str, ref data, new string[] { " ", ",", "\09" });
                    agitnumber = HUtil32.Str_ToInt(data, 0);
                    if (data == "")
                    {
                        continue;
                    }
                    str = HUtil32.GetValidStr3(str, ref data, new string[] { " ", ",", "\09" });
                    gname = data;
                    str = HUtil32.GetValidStr3(str, ref data, new string[] { " ", ",", "\09" });
                    mastername = data;
                    str = HUtil32.GetValidStr3(str, ref data, new string[] { " ", ",", "\09" });
                    regtime = data;
                    str = HUtil32.GetValidStr3(str, ref data, new string[] { " ", ",", "\09" });
                    period = HUtil32.Str_ToInt(data, 0);
                    // 厘盔 扁何陛
                    if (GuildAgitFileVersion == 1)
                    {
                        str = HUtil32.GetValidStr3(str, ref data, new string[] { " ", ",", "\09" });
                        agittotalgold = HUtil32.Str_ToInt(data, 0);
                    }
                    if (data == "")
                    {
                        svMain.MainOutMessage("GuildAgitList " + i.ToString() + "th Line is error.");
                        continue;
                        // 厘盔 沥焊 何练.
                    }
                    // 备涝巩颇沥焊
                    str = HUtil32.GetValidStr3(str, ref data, new string[] { " ", ",", "\09" });
                    saleflag = HUtil32.Str_ToInt(data, 0);
                    str = HUtil32.GetValidStr3(str, ref data, new string[] { " ", ",", "\09" });
                    salemoney = HUtil32.Str_ToInt(data, -1);
                    str = HUtil32.GetValidStr3(str, ref data, new string[] { " ", ",", "\09" });
                    salewait = HUtil32.Str_ToInt(data, 0);
                    str = HUtil32.GetValidStr3(str, ref data, new string[] { " ", ",", "\09" });
                    salegname = data;
                    str = HUtil32.GetValidStr3(str, ref data, new string[] { " ", ",", "\09" });
                    salemastername = data;
                    str = HUtil32.GetValidStr3(str, ref data, new string[] { " ", ",", "\09" });
                    saletime = data;
                    if (agitnumber > -1)
                    {
                        guildagit = new TGuildAgit();
                        if (guildagit != null)
                        {
                            guildagit.SetGuildAgitRecord(agitnumber, gname, mastername, regtime, period, agittotalgold);
                            guildagit.SetGuildAgitForSaleRecord(saleflag, salemoney, salewait, salegname, salemastername, saletime);
                            GuildAgitList.Add(guildagit);
                        }
                    }
                }
                strlist.Free();
                svMain.MainOutMessage(GuildAgitList.Count.ToString() + " guildagits are loaded.");
                // 矫累且 锭 滚傈 眉农 0捞搁 1肺 棵府绊 货肺 归诀 棺 历厘窃
                if (GuildAgitFileVersion <= 0)
                {
                    svMain.GuildAgitMan.SaveGuildAgitList(true);
                }
            }
            else
            {
                svMain.MainOutMessage("No guildagit file loaded...");
            }
        }

        public void SaveGuildAgitList(bool bBackup)
        {
            ArrayList strlist;
            int i;
            if (svMain.ServerIndex == 0)
            {
                if (bBackup)
                {
                    //FileCopy(svMain.GuildAgitFile, svMain.GuildAgitFile + ConvertDatetimeToFileName(DateTime.Now));
                }
                strlist = new ArrayList();
                if (GuildAgitFileVersion <= 0)
                {
                    GuildAgitFileVersion = 1;
                }
                strlist.Add("[VERSION=" + GuildAgitFileVersion.ToString() + "]");
                strlist.Add(";Territory no.|Guild name|Guild master|Reg Date|Remain days|Territory gold|On sale|Sale gold|Sale remain days|Buy Guild name|Buy Guild master|Conclusion date");
                if (GuildAgitFileVersion <= 0)
                {
                    for (i = 0; i < GuildAgitList.Count; i++)
                    {
                        strlist.Add((GuildAgitList[i] as TGuildAgit).GuildAgitNumber.ToString() + "\09" + (GuildAgitList[i] as TGuildAgit).GuildName + "\09" + (GuildAgitList[i] as TGuildAgit).GuildMasterName + "\09" + (GuildAgitList[i] as TGuildAgit).RegistrationTime + "\09" + (GuildAgitList[i] as TGuildAgit).ContractPeriod.ToString() + "\09" + (GuildAgitList[i] as TGuildAgit).ForSaleFlag.ToString() + "\09" + (GuildAgitList[i] as TGuildAgit).ForSaleMoney.ToString() + "\09" + (GuildAgitList[i] as TGuildAgit).ForSaleWait.ToString() + "\09" + (GuildAgitList[i] as TGuildAgit).ForSaleGuildName + "\09" + (GuildAgitList[i] as TGuildAgit).ForSaleGuildMasterName + "\09" + (GuildAgitList[i] as TGuildAgit).ForSaleTime);
                    }
                }
                else
                {
                    for (i = 0; i < GuildAgitList.Count; i++)
                    {
                        strlist.Add((GuildAgitList[i] as TGuildAgit).GuildAgitNumber.ToString() + "\09" + (GuildAgitList[i] as TGuildAgit).GuildName + "\09" + (GuildAgitList[i] as TGuildAgit).GuildMasterName + "\09" + (GuildAgitList[i] as TGuildAgit).RegistrationTime + "\09" + (GuildAgitList[i] as TGuildAgit).ContractPeriod.ToString() + "\09" + (GuildAgitList[i] as TGuildAgit).GuildAgitTotalGold.ToString() + "\09" + (GuildAgitList[i] as TGuildAgit).ForSaleFlag.ToString() + "\09" + (GuildAgitList[i] as TGuildAgit).ForSaleMoney.ToString() + "\09" + (GuildAgitList[i] as TGuildAgit).ForSaleWait.ToString() + "\09" + (GuildAgitList[i] as TGuildAgit).ForSaleGuildName + "\09" + (GuildAgitList[i] as TGuildAgit).ForSaleGuildMasterName + "\09" + (GuildAgitList[i] as TGuildAgit).ForSaleTime);
                    }
                }
                try
                {
                    strlist.SaveToFile(svMain.GuildAgitFile);
                }
                catch
                {
                    svMain.MainOutMessage(svMain.GuildAgitFile + " Saving error...");
                }
                strlist.Free();
            }
        }

        public TGuildAgit GetGuildAgit(string gname)
        {
            TGuildAgit result = null;
            for (var i = 0; i < GuildAgitList.Count; i++)
            {
                if ((GuildAgitList[i] as TGuildAgit).GuildName == gname)
                {
                    result = GuildAgitList[i] as TGuildAgit;
                    break;
                }
            }
            return result;
        }

        // 府畔蔼 : 厘盔锅龋 (-1 捞搁 Error)
        public int AddGuildAgit(string gname, string mastername, string secondmastername)
        {
            int result;
            TGuildAgit guildagit;
            int nextnumber;
            result = -1;
            // if IsValidFileName (gname) then begin   //傍归漂荐巩磊 瞒窜扁瓷 秦力(2004/11/05)
            if (GetGuildAgit(gname) == null)
            {
                // 巩颇厘盔 俺荐力茄.
                if (GuildAgitList.Count >= svMain.GuildAgitMaxNumber)
                {
                    return result;
                }
                guildagit = new TGuildAgit();
                nextnumber = GetNextGuildAgitNumber();
                if (guildagit.AddGuildAgitRecord(nextnumber, gname, mastername, secondmastername))
                {
                    GuildAgitList.Add(guildagit);
                    SaveGuildAgitList(false);
                    result = nextnumber;
                }
            }
            // end;

            return result;
        }

        public bool DelGuildAgit(string gname)
        {
            bool result;
            TGuildAgit guildagit;
            int i;
            result = false;
            for (i = 0; i < GuildAgitList.Count; i++)
            {
                guildagit = GuildAgitList[i] as TGuildAgit;
                if (guildagit != null)
                {
                    if (guildagit.GuildName == gname)
                    {
                        // 眠规.
                        guildagit.ExpulsionMembers();
                        // Guild绰 老何矾 皋葛府甫 秦力窍瘤 臼澜(why?)
                        guildagit.Free();
                        // Free救茄 版快 皋葛府 穿利捞 积变促...
                        GuildAgitList.RemoveAt(i);
                        SaveGuildAgitList(true);
                        result = true;
                        break;
                    }
                }
            }
            return result;
        }

        public int GetNextGuildAgitNumber()
        {
            int result;
            int i;
            int j;
            result = -1;
            if (GuildAgitList.Count == 0)
            {
                result = svMain.GuildAgitStartNumber;
                return result;
            }
            for (j = svMain.GuildAgitStartNumber; j <= svMain.GuildAgitMaxNumber; j++)
            {
                for (i = 0; i < GuildAgitList.Count; i++)
                {
                    if ((GuildAgitList[i] as TGuildAgit).GuildAgitNumber == j)
                    {
                        break;
                    }
                }
                // 锅龋啊 绝栏搁
                if (i == GuildAgitList.Count)
                {
                    result = j;
                    return result;
                }
            }
            return result;
        }

        public bool CheckGuildAgitTimeOut(int gaCount)
        {
            bool result;
            int i;
            bool SaveFlag;
            int agitnumber;
            TGuildAgit guildagit;
            result = false;
            SaveFlag = false;
            agitnumber = -1;
            guildagit = null;
            for (i = GuildAgitList.Count - 1; i >= 0; i--)
            {
                guildagit = GuildAgitList[i] as TGuildAgit;
                if (gaCount == 0)
                {
                    guildagit.UpdateGuildMaster();
                }
                if (guildagit.GetCurrentDelayStatus() <= 0)
                {
                    if (guildagit.GuildAgitTotalGold >= Guild.GUILDAGITEXTENDFEE)
                    {
                        agitnumber = svMain.GuildAgitMan.ExtendTime(1, guildagit.GuildName);
                        if (agitnumber > -1)
                        {
                            guildagit.GuildAgitTotalGold = guildagit.GuildAgitTotalGold - Guild.GUILDAGITEXTENDFEE;
                            svMain.AddUserLog("38\09" + "GUILDAGIT" + "\09" + "0" + "\09" + "0" + "\09" + "AUTOEXTEND" + "\09" + guildagit.GuildName + "\09" + agitnumber.ToString() + "\09" + "1\09" + Guild.GUILDAGITEXTENDFEE.ToString());
                            SaveFlag = true;
                            svMain.UserEngine.SendInterMsg(Grobal2.ISM_RELOADGUILDAGIT, svMain.ServerIndex, "");
                        }
                    }
                }
                if (guildagit.IsSoldOutExpired())
                {
                    guildagit.ChangeGuildAgitMaster();
                    guildagit.ResetForSaleFields();
                    guildagit.ExpulsionMembers();
                    SaveFlag = true;
                    result = true;
                }
                if (guildagit.IsExpired())
                {
                    guildagit.ExpulsionMembers();
                    guildagit.Free();
                    GuildAgitList.RemoveAt(i);
                    SaveFlag = true;
                    result = true;
                }
            }
            if (SaveFlag)
            {
                SaveGuildAgitList(true);
            }
            return result;
        }

        public int ExtendTime(int count, string gname)
        {
            int result;
            const int LIMIT_DAY = 300;
            TGuildAgit guildagit;
            DateTime RegDateTime;
            DateTime NowDateTime;
            bool BackupFlag;
            result = -1;
            BackupFlag = false;
            if ((count < 1) || (count > 100))
            {
                return result;
            }
            guildagit = GetGuildAgit(gname);
            if (guildagit != null)
            {
                RegDateTime = guildagit.GetGuildAgitRegDateTime();
                NowDateTime = DateTime.Now;
                if (NowDateTime - RegDateTime > LIMIT_DAY)
                {
                    if (guildagit.ContractPeriod > LIMIT_DAY)
                    {
                        RegDateTime = RegDateTime + LIMIT_DAY;
                        guildagit.RegistrationTime = guildagit.ConvertDateTimeToString(RegDateTime);
                        guildagit.ContractPeriod = guildagit.ContractPeriod - LIMIT_DAY;
                        BackupFlag = true;
                    }
                    else
                    {
                        return result;
                    }
                }
                else
                {
                    if (guildagit.ContractPeriod + (count * Guild.GUILDAGIT_DAYUNIT) > 365)
                    {
                        return result;
                    }
                }
                guildagit.ContractPeriod = guildagit.ContractPeriod + (count * Guild.GUILDAGIT_DAYUNIT);
                SaveGuildAgitList(BackupFlag);
                result = guildagit.GuildAgitNumber;
            }
            return result;
        }

        public DateTime GetRemainDateTime(string gname)
        {
            TGuildAgit guildagit;
            DateTime result = -100;
            guildagit = GetGuildAgit(gname);
            if (guildagit != null)
            {
                result = guildagit.GetGuildAgitRemainDateTime();
            }
            return result;
        }

        public int GetTradingRemainDate(string gname)
        {
            int result = -100;
            TGuildAgit guildagit = GetGuildAgit(gname);
            if (guildagit != null)
            {
                result = HUtil32.MathRound(guildagit.GetGuildAgitRemainDateTime().ToOADate());
            }
            return result;
        }

        public bool IsDelayed(string gname)
        {
            bool result = false;
            TGuildAgit guildagit = GetGuildAgit(gname);
            if (guildagit != null)
            {
                if (guildagit.GetCurrentDelayStatus() == 0)
                {
                    result = true;
                }
            }
            return result;
        }

        public string ConvertDatetimeToFileName(DateTime datetime)
        {
            short Year = (short)datetime.Year;
            short Month = (short)datetime.Month;
            short Day = (short)datetime.Day;
            short Hour = (short)datetime.Hour;
            short Min = (short)datetime.Minute;
            short Sec = (short)datetime.Second;
            short MSec = (short)datetime.Millisecond;
            return ".bak." + Year.ToString() + "_" + Month.ToString() + "_" + Day.ToString() + "_" + Hour.ToString() + "_" + Min.ToString() + "_" + Sec.ToString() + ".txt";
        }

        public void GetGuildAgitSaleList(ref ArrayList salelist)
        {
            DateTime RemainDateTime;
            string RemainDay;
            salelist = null;
            string strStatus = "";
            string AnotherGuildMaster = "";
            for (var i = 0; i < GuildAgitList.Count; i++)
            {
                TGuildAgit guildagit = GuildAgitList[i] as TGuildAgit;
                strStatus = "Normal";
                if (guildagit.IsForSale())
                {
                    strStatus = "On Sale";
                }
                else if (guildagit.IsSoldOut())
                {
                    strStatus = "Conclusion";
                }
                if (salelist == null)
                {
                    salelist = new ArrayList();
                }
                RemainDateTime = guildagit.GetGuildAgitRemainDateTime();
                if (RemainDateTime <= 0)
                {
                    RemainDay = "Arrear";
                }
                else
                {
                    RemainDateTime = Convert.ToInt64(RemainDateTime * 10);
                    RemainDateTime = RemainDateTime / 10;
                    RemainDateTime = Guild.GUILDAGIT_DAYUNIT - RemainDateTime;
#if !DEBUG
                    RemainDay = Convert.ToString(RemainDateTime) + "Day(s)";
#else
                    RemainDay = Convert.ToString(RemainDateTime) + "Min.";
#endif
                }
                string strtemp = guildagit.GuildAgitNumber.ToString();
                if (strtemp.Length == 1)
                {
                    strtemp = "0" + strtemp;
                }
                TGuild guild = svMain.GuildMan.GetGuild(guildagit.GuildName);
                if (guild != null)
                {
                    AnotherGuildMaster = guildagit.GuildMasterNameSecond;
                }
                if (AnotherGuildMaster == "")
                {
                    AnotherGuildMaster = "---";
                }
                salelist.Add(strtemp + "/" + guildagit.GuildName + "/" + guildagit.GuildMasterName + "/" + AnotherGuildMaster + "/" + guildagit.ForSaleMoney.ToString() + "/" + strStatus);
            }
            if (salelist != null)
            {
                salelist.Sort();
            }
        }

        public bool IsGuildAgitExpired(string gname)
        {
            TGuildAgit guildagit;
            bool result = false;
            for (var i = 0; i < GuildAgitList.Count; i++)
            {
                guildagit = GuildAgitList[i] as TGuildAgit;
                if (guildagit != null)
                {
                    if (guildagit.GuildName == gname)
                    {
                        if (guildagit.IsExpired())
                        {
                            result = true;
                            break;
                        }
                    }
                }
            }
            return result;
        }

        public bool GuildAgitTradeOk(string gname, string new_gname, string new_mastername)
        {
            bool result = false;
            if ((gname == "") || (new_gname == "") || (new_mastername == ""))
            {
                return result;
            }
            for (var i = 0; i < GuildAgitList.Count; i++)
            {
                TGuildAgit guildagit = GuildAgitList[i] as TGuildAgit;
                if (guildagit != null)
                {
                    if ((guildagit.GuildName == gname) && (guildagit.ForSaleFlag == 1))
                    {
                        guildagit.ForSaleFlag = 0;
                        guildagit.ForSaleWait = Guild.GUILDAGIT_SALEWAIT_DAYUNIT;
                        guildagit.ForSaleGuildName = new_gname;
                        guildagit.ForSaleGuildMasterName = new_mastername;
                        guildagit.ForSaleTime = guildagit.ConvertDateTimeToString(DateTime.Now);
                        SaveGuildAgitList(true);
                        result = true;
                        break;
                    }
                }
            }
            return result;
        }

        public bool IsExistInForSaleGuild(string gname)
        {
            TGuildAgit guildagit;
            bool result = false;
            for (var i = 0; i < GuildAgitList.Count; i++)
            {
                guildagit = GuildAgitList[i] as TGuildAgit;
                if (guildagit != null)
                {
                    if (guildagit.ForSaleGuildName == gname)
                    {
                        result = true;
                        break;
                    }
                }
            }
            return result;
        }

        public string GetGuildNameFromAgitNum(int AgitNum)
        {
            string result;
            int i;
            TGuildAgit guildagit;
            result = "";
            if (AgitNum > -1)
            {
                for (i = 0; i < GuildAgitList.Count; i++)
                {
                    guildagit = GuildAgitList[i] as TGuildAgit;
                    if (guildagit != null)
                    {
                        if (guildagit.GuildAgitNumber == AgitNum)
                        {
                            result = guildagit.GuildName;
                            break;
                        }
                    }
                }
            }
            return result;
        }

        public string GetGuildMasterNameFromAgitNum(int AgitNum)
        {
            string result;
            int i;
            TGuildAgit guildagit;
            result = "";
            if (AgitNum > -1)
            {
                for (i = 0; i < GuildAgitList.Count; i++)
                {
                    guildagit = GuildAgitList[i] as TGuildAgit;
                    if (guildagit != null)
                    {
                        if (guildagit.GuildAgitNumber == AgitNum)
                        {
                            result = guildagit.GuildMasterName;
                            break;
                        }
                    }
                }
            }
            return result;
        }

        public int MakeAgitDecoMon()
        {
            TAgitDecoItem pitem;
            int result = 0;
            int count = 0;
            for (var i = 0; i < AgitDecoMonList.Count; i++)
            {
                pitem = (TAgitDecoItem)AgitDecoMonList[i];
                if (pitem != null)
                {
                    if (MakeDecoItemToMap(pitem.MapName, pitem.Name, pitem.Looks, pitem.Dura, pitem.x, pitem.y) > 0)
                    {
                        count++;
                    }
                }
            }
            result = count;
            return result;
        }

        public int MakeDecoItemToMap(string DropMapName, string ItemName, int LookIndex, int Durability, int dx, int dy)
        {
            TStdItem ps;
            TUserItem newpu;
            TMapItem pmi;
            TMapItem pr;
            TEnvirnoment dropenvir;
            string pricestr = string.Empty;
            int result = 0;
            if (svMain.DecoItemList.Count <= 0)
            {
                return result;
            }
            try
            {
                ps = svMain.UserEngine.GetStdItemFromName(ObjBase.NAME_OF_DECOITEM);
                if (ps != null)
                {
                    newpu = new TUserItem();
                    if (svMain.UserEngine.CopyToUserItemFromName(ps.Name, ref newpu))
                    {
                        newpu.Dura = (ushort)LookIndex;
                        newpu.DuraMax = (ushort)Durability;
                        pmi = new TMapItem();
                        pmi.UserItem = newpu;
                        if ((ps.StdMode == ObjBase.STDMODE_OF_DECOITEM) && (ps.Shape == ObjBase.SHAPE_OF_DECOITEM))
                        {
                            pmi.Name = GetDecoItemName(LookIndex, ref pricestr) + "[" + HUtil32.MathRound(Durability / 1000).ToString() + "]" + "/" + "1";
                            pmi.Count = 1;
                            pmi.Looks = (ushort)LookIndex;
                            pmi.AniCount = ps.AniCount;
                            pmi.Reserved = 0;
                            pmi.Ownership = null;
                            pmi.Droptime = GetTickCount;
                            pmi.Droper = null;
                        }
                        dropenvir = svMain.GrobalEnvir.GetEnvir(DropMapName);
                        pr = (TMapItem)dropenvir.AddToMap(dx, dy, Grobal2.OS_ITEMOBJECT, pmi);
                        if (pr == pmi)
                        {
                            result = pmi.UserItem.MakeIndex;
                            svMain.MainOutMessage("[DecoItemGen] " + pmi.Name + "(" + dx.ToString() + "," + dy.ToString() + ")");
                        }
                        else
                        {
                            Dispose(pmi);
                        }
                    }
                    if (newpu != null)
                    {
                        Dispose(newpu);
                    }
                }
            }
            catch
            {
                svMain.MainOutMessage("[Exception] TGuildAgitManager.MakeDecoItemToMap");
            }
            return result;
        }

        public bool DeleteAgitDecoMon(string mapname, short x, short y)
        {
            bool result;
            int i;
            TAgitDecoItem tempitem;
            int agitnum;
            result = false;
            try
            {
                for (i = 0; i < AgitDecoMonList.Count; i++)
                {
                    tempitem = (TAgitDecoItem)AgitDecoMonList[i];
                    if (tempitem != null)
                    {
                        if ((tempitem.MapName == mapname) && (tempitem.x == x) && (tempitem.y == y))
                        {
                            AgitDecoMonList.RemoveAt(i);
                            agitnum = GetGuildAgitNumFromMapName(mapname);
                            if ((agitnum > 0) && (agitnum <= Guild.MAXGUILDAGITCOUNT))
                            {
                                DecAgitDecoMonCountByAgitNum(agitnum);
                            }
                            result = true;
                            break;
                        }
                    }
                }
            }
            catch
            {
                svMain.MainOutMessage("[Exception] TGuildAgitManager.DeleteAgitDecoMon");
            }
            return result;
        }

        // DecoMonList俊 坷宏璃飘甫 眠啊茄促.
        public bool AddAgitDecoMon(TAgitDecoItem item)
        {
            bool result;
            TAgitDecoItem pitem;
            result = false;
            try
            {
                pitem = new TAgitDecoItem();
                pitem = item;
                // 捞抚捞 绝绰 酒捞袍篮 荤扼柳 酒捞袍烙...
                AgitDecoMonList.Add(pitem);
                result = true;
            }
            catch
            {
                svMain.MainOutMessage("[Exception] TGuildAgitManager.AddAgitDecoMon");
            }
            return result;
        }

        // DecoMonList俊 酒捞袍 郴侩阑 诀单捞飘茄促.
        public bool UpdateDuraAgitDecoMon(string mapname, short x, short y, short dura)
        {
            bool result;
            int i;
            TAgitDecoItem tempitem;
            result = false;
            try
            {
                for (i = 0; i < AgitDecoMonList.Count; i++)
                {
                    tempitem = (TAgitDecoItem)AgitDecoMonList[i];
                    if (tempitem != null)
                    {
                        if ((tempitem.MapName == mapname) && (tempitem.x == x) && (tempitem.y == y))
                        {
                            tempitem.Dura = (ushort)dura;
                            result = true;
                            break;
                        }
                    }
                }
            }
            catch
            {
                svMain.MainOutMessage("[Exception] TGuildAgitManager.UpdateDuraAgitDecoMon");
            }
            return result;
        }

        public int LoadAgitDecoMon()
        {
            int result;
            int i;
            string str = string.Empty;
            string flname = string.Empty;
            string decomonname = string.Empty;
            string map = string.Empty;
            string indexstr = string.Empty;
            string xstr = string.Empty;
            string ystr = string.Empty;
            string maker = string.Empty;
            string durastr = string.Empty;
            ArrayList strlist;
            TAgitDecoItem item;
            TAgitDecoItem pitem;
            flname = svMain.GuildBaseDir + Guild.AGITDECOMONFILE;
            if (File.Exists(flname))
            {
                strlist = new ArrayList();
                strlist.LoadFromFile(flname);
                for (i = 0; i < strlist.Count; i++)
                {
                    str = strlist[i].Trim();
                    if (str == "")
                    {
                        continue;
                    }
                    if (str[1] == ";")
                    {
                        continue;
                    }
                    str = HUtil32.GetValidStr3(str, ref decomonname, new string[] { " ", "\09" });
                    str = HUtil32.GetValidStr3(str, ref indexstr, new string[] { " ", "\09" });
                    str = HUtil32.GetValidStr3(str, ref map, new string[] { " ", "\09" });
                    str = HUtil32.GetValidStr3(str, ref xstr, new string[] { " ", "\09" });
                    str = HUtil32.GetValidStr3(str, ref ystr, new string[] { " ", "\09" });
                    str = HUtil32.GetValidStrCap(str, ref maker, new string[] { " ", "\09" });
                    str = HUtil32.GetValidStr3(str, ref durastr, new string[] { " ", "\09" });
                    item.Name = decomonname.Substring(0 - 1, 20);
                    item.Looks = (ushort)HUtil32.Str_ToInt(indexstr, 0);
                    item.MapName = map.Substring(0 - 1, 14);
                    item.x = (ushort)HUtil32.Str_ToInt(xstr, 0);
                    item.y = (ushort)HUtil32.Str_ToInt(ystr, 0);
                    item.Maker = maker.Substring(0 - 1, 14);
                    item.Dura = (ushort)HUtil32.Str_ToInt(durastr, 0);
                    pitem = new TAgitDecoItem();
                    pitem = item;
                    // 捞抚捞 绝绰 酒捞袍篮 荤扼柳 酒捞袍烙...
                    AgitDecoMonList.Add(pitem);
                }
                strlist.Free();
            }
            result = 1;
            return result;
        }

        public void SaveAgitDecoMonList()
        {
            ArrayList strlist;
            int i;
            if (svMain.ServerIndex == 0)
            {
                // 付胶磐 辑滚父 历厘阑 茄促.
                strlist = new ArrayList();
                for (i = 0; i < AgitDecoMonList.Count; i++)
                {
                    strlist.Add(((TAgitDecoItem)AgitDecoMonList[i]).Name + "\09" + ((TAgitDecoItem)AgitDecoMonList[i]).Looks.ToString() + "\09" + ((TAgitDecoItem)AgitDecoMonList[i]).MapName + "\09" + ((TAgitDecoItem)AgitDecoMonList[i]).x.ToString() + "\09" + ((TAgitDecoItem)AgitDecoMonList[i]).y.ToString() + "\09" + ((TAgitDecoItem)AgitDecoMonList[i]).Maker + "\09" + ((TAgitDecoItem)AgitDecoMonList[i]).Dura.ToString());
                }
                try
                {
                    strlist.SaveToFile(svMain.GuildBaseDir + Guild.AGITDECOMONFILE);
                }
                catch
                {
                    svMain.MainOutMessage(Guild.AGITDECOMONFILE + " 历厘 坷幅...");
                }
                strlist.Free();
            }
        }

        public string GetDecoItemName(int index, ref string price)
        {
            string name = string.Empty;
            string result = string.Empty;
            string str = string.Empty;
            if (index < 0)
            {
                return result;
            }
            if (svMain.DecoItemList.Count <= index)
            {
                return result;
            }
            str = (string)svMain.DecoItemList[index];
            price = HUtil32.GetValidStr3(str, ref name, new string[] { "/" });
            result = name;
            return result;
        }

        public int GetAgitDecoMonCount(int agitnum)
        {
            int result;
            result = -1;
            if (agitnum <= 0)
            {
                return result;
            }
            if (agitnum > Guild.MAXGUILDAGITCOUNT)
            {
                return result;
            }
            result = AgitDecoMonCount[agitnum];
            return result;
        }

        public void IncAgitDecoMonCount(string gname)
        {
            int agitnum;
            TGuildAgit guildagit;
            guildagit = GetGuildAgit(gname);
            if (guildagit != null)
            {
                agitnum = guildagit.GuildAgitNumber;
                if ((agitnum > 0) && (agitnum <= Guild.MAXGUILDAGITCOUNT))
                {
                    AgitDecoMonCount[agitnum] = AgitDecoMonCount[agitnum] + 1;
                }
            }
        }

        public void IncAgitDecoMonCountByAgitNum(int agitnum)
        {
            if ((agitnum > 0) && (agitnum <= Guild.MAXGUILDAGITCOUNT))
            {
                AgitDecoMonCount[agitnum] = AgitDecoMonCount[agitnum] + 1;
            }
        }

        public void DecAgitDecoMonCount(string gname)
        {
            int agitnum;
            TGuildAgit guildagit;
            guildagit = GetGuildAgit(gname);
            if (guildagit != null)
            {
                agitnum = guildagit.GuildAgitNumber;
                if ((agitnum > 0) && (agitnum <= Guild.MAXGUILDAGITCOUNT))
                {
                    AgitDecoMonCount[agitnum] = HUtil32._MAX(0, AgitDecoMonCount[agitnum] - 1);
                }
            }
        }

        public void DecAgitDecoMonCountByAgitNum(int agitnum)
        {
            if ((agitnum > 0) && (agitnum <= Guild.MAXGUILDAGITCOUNT))
            {
                AgitDecoMonCount[agitnum] = HUtil32._MAX(0, AgitDecoMonCount[agitnum] - 1);
            }
        }

        public void ArrangeEachAgitDecoMonCount()
        {
            int i;
            int agitnum;
            TAgitDecoItem pitem;
            for (i = 0; i < AgitDecoMonList.Count; i++)
            {
                pitem = (TAgitDecoItem)AgitDecoMonList[i];
                if (pitem != null)
                {
                    agitnum = GetGuildAgitNumFromMapName(pitem.MapName);
                    if ((agitnum > 0) && (agitnum <= Guild.MAXGUILDAGITCOUNT))
                    {
                        IncAgitDecoMonCountByAgitNum(agitnum);
                    }
                }
            }
        }

        // 厘盔操固扁 酒捞袍 汲摹 俺荐 檬苞 咯何 八荤
        public bool IsAvailableDecoMonCount(string gname)
        {
            bool result;
            TGuildAgit guildagit;
            int decomoncount;
            result = false;
            decomoncount = -1;
            // 厘盔 醚 俺荐 力茄
            if (AgitDecoMonList.Count >= 1000)
            {
                return result;
            }
            guildagit = GetGuildAgit(gname);
            if (guildagit != null)
            {
                decomoncount = GetAgitDecoMonCount(guildagit.GuildAgitNumber);
                if ((decomoncount < 0) || (decomoncount >= Guild.MAXCOUNT_DECOMON_PER_AGIT))
                {
                    return result;
                }
                result = true;
            }
            return result;
        }

        public bool IsMatchDecoItemInOutdoor(int index, string mapname)
        {
            bool result;
            int kind;
            int floor;
            result = false;
            kind = -1;
            floor = -1;
            if (svMain.DecoItemList == null)
            {
                return result;
            }
            if ((index < 0) || (index >= svMain.DecoItemList.Count))
            {
                return result;
            }
            // 1:角郴, 2:角寇, 3:笛促
            kind = Hiword((int)svMain.DecoItemList.Values[index]);
            floor = GetGuildAgitMapFloor(mapname);
            if ((kind == 1) && (floor == 2))
            {
                result = true;
            }
            if ((kind == 2) && (floor == 0))
            {
                result = true;
            }
            if ((kind == 3) && ((floor == 2) || (floor == 0)))
            {
                result = true;
            }
            return result;
        }

        public int GetGuildAgitMapFloor(string mapname)
        {
            int result = -1;
            if (mapname.Length < 3)
            {
                return result;
            }
            return HUtil32.Str_ToInt(mapname[3], -1);
        }

        public int GetGuildAgitNumFromMapName(string gmapname)
        {
            int result;
            string agitnumstr;
            int mapnamelen;
            int agitnum;
            result = -1;
            mapnamelen = gmapname.Length;
            if (mapnamelen <= 3)
            {
                return result;
            }
            agitnumstr = gmapname.Substring(4 - 1, mapnamelen - 3);
            agitnum = HUtil32.Str_ToInt(agitnumstr, -1);
            if ((agitnum > 0) && (agitnum <= Guild.MAXGUILDAGITCOUNT))
            {
                result = agitnum;
            }
            return result;
        }

        // 厘盔操固扁 酒捞袍 郴备 皑家矫糯.
        public bool DecreaseDecoMonDurability()
        {
            bool result;
            ArrayList list;
            int i;
            int ix;
            int iy;
            TEnvirnoment env;
            TMapInfo pm = null;
            TMapItem pmapitem;
            bool inrange;
            int k;
            byte ObjShape;
            int agitnum;
            string pricestr;
            TStdItem ps;
            // (郴备 7,000 : 1矫埃俊 40究 皑家 => 老林老捞搁 6,720皑家)
            const int DEC_DURA = 40;
            result = false;
            // 傈眉 厘盔阑 八荤窍咯
            // 甘俊 乐绰 酒捞袍捞 厘盔操固扁 酒捞袍捞搁 郴备 皑家矫挪促.
            // 府胶飘俊 诀单捞飘窍绊
            // 府胶飘甫 历厘茄促.
            try
            {
                list = new ArrayList();
                // 厘盔 甘 傈眉 谅钎甫 八祸窍咯 阿 谅钎俊 乐绰 操固扁 酒捞袍狼 郴备甫 皑家矫挪促.
                for (i = 0; i <= 3; i++)
                {
                    for (agitnum = 1; agitnum <= svMain.GuildAgitMaxNumber; agitnum++)
                    {
                        env = svMain.GrobalEnvir.GetEnvir(GuildAgitMapName[i] + agitnum.ToString());
                        if (env != null)
                        {
                            for (ix = 0; ix < env.MapWidth; ix++)
                            {
                                for (iy = 0; iy < env.MapHeight; iy++)
                                {
                                    // ------------------------------------------------------------
                                    // 厘盔操固扁 酒捞袍捞 乐栏搁 郴备 皑家
                                    inrange = env.GetMapXY(ix, iy, ref pm);
                                    if (inrange)
                                    {
                                        if (pm.OBJList != null)
                                        {
                                            k = 0;
                                            while (true)
                                            {
                                                if (k >= pm.OBJList.Count)
                                                {
                                                    break;
                                                }
                                                // -1 do begin //downto 0 do begin
                                                if (pm.OBJList[k] != null)
                                                {
                                                    // Check Object wrong Memory 2003-09-15 PDS
                                                    try
                                                    {
                                                        // 皋葛府俊辑 俊矾啊 乐栏搁 劳剂记 吧府备
                                                        ObjShape = ((TAThing)pm.OBJList[k]).Shape;
                                                    }
                                                    catch
                                                    {
                                                        // 坷宏璃飘俊辑 哗滚府磊.
                                                        svMain.MainOutMessage("[TGuildAgit.ExpulsionMembers] DELOBJ-WRONG MEMORY:" + env.MapName + "," + ix.ToString() + "," + iy.ToString());
                                                        pm.OBJList.RemoveAt(k);
                                                        continue;
                                                    }

                                                    if (((TAThing)pm.OBJList[k]).Shape == Grobal2.OS_ITEMOBJECT)
                                                    {
                                                        pmapitem = (TMapItem)((TAThing)pm.OBJList[k]).AObject;
                                                        // UpdateVisibleItems (i, j, pmapitem);
                                                        // 厘盔操固扁 酒捞袍牢瘤 犬牢
                                                        ps = svMain.UserEngine.GetStdItem(pmapitem.UserItem.Index);
                                                        if (ps != null)
                                                        {
                                                            if ((ps.StdMode == ObjBase.STDMODE_OF_DECOITEM) && (ps.Shape == ObjBase.SHAPE_OF_DECOITEM))
                                                            {
                                                                // 郴备啊 DEC_DURA焊促 农搁 DEC_DURA甫 皑家矫挪促.
                                                                if (pmapitem.UserItem.DuraMax >= DEC_DURA)
                                                                {
                                                                    pmapitem.UserItem.DuraMax = (ushort)(pmapitem.UserItem.DuraMax - DEC_DURA);
                                                                    pmapitem.Name = GetDecoItemName(pmapitem.Looks, ref pricestr) + "[" + HUtil32.MathRound(pmapitem.UserItem.DuraMax / 1000).ToString() + "]" + "/" + "1";
                                                                    // 郴备甫 皑家矫虐绊 历厘茄促.
                                                                    if (UpdateDuraAgitDecoMon(env.MapName, (short)ix, (short)iy, (short)pmapitem.UserItem.DuraMax))
                                                                    {
                                                                        SaveAgitDecoMonList();
                                                                    }
                                                                }
                                                                else
                                                                {
                                                                    pmapitem.UserItem.DuraMax = 0;
                                                                }
                                                                // 郴备啊 0捞窍捞搁 绝矩促.
                                                                if (pmapitem.UserItem.DuraMax <= 0)
                                                                {
                                                                    // 府胶飘俊辑 昏力窍绊 历厘茄促.
                                                                    if (DeleteAgitDecoMon(env.MapName, (short)ix, (short)iy))
                                                                    {
                                                                        SaveAgitDecoMonList();
                                                                    }
                                                                    // 滚赴瘤 1矫埃捞 瘤抄扒 绝矩促. -PDS 肋给瞪 啊瓷己 乐澜
                                                                    // Dispose (PTMapItem (PTAThing (pm.ObjList[k]).AObject));
                                                                    Dispose((TAThing)pm.OBJList[k]);
                                                                    pm.OBJList.RemoveAt(k);
                                                                    if (pm.OBJList.Count <= 0)
                                                                    {
                                                                        pm.OBJList.Free();
                                                                        pm.OBJList = null;
                                                                        break;
                                                                    }
                                                                    continue;
                                                                }
                                                                if ((pmapitem.Ownership != null) || (pmapitem.Droper != null))
                                                                {
                                                                    if (GetTickCount - pmapitem.Droptime > ObjBase.ANTI_MUKJA_DELAY)
                                                                    {
                                                                        pmapitem.Ownership = null;
                                                                        pmapitem.Droper = null;
                                                                    }
                                                                    else
                                                                    {
                                                                        // {林狼} 冈磊 焊龋 矫埃捞 5盒(磷篮 某腐 free 蜡抗矫埃)阑 檬苞窍搁
                                                                        // 捞 何盒俊辑 滚弊啊 惯积茄促.
                                                                        if (pmapitem.Ownership != null)
                                                                        {
                                                                            if (((TCreature)pmapitem.Ownership).BoGhost)
                                                                            {
                                                                                pmapitem.Ownership = null;
                                                                            }
                                                                        }
                                                                        if (pmapitem.Droper != null)
                                                                        {
                                                                            if (((TCreature)pmapitem.Droper).BoGhost)
                                                                            {
                                                                                pmapitem.Droper = null;
                                                                            }
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                                k++;
                                            }
                                        }
                                    }
                                    // ------------------------------------------------------------
                                }
                            }
                        }
                    }
                }
                list.Free();
            }
            catch
            {
                svMain.MainOutMessage("[Exception]TGuildAgitManager.DecreaseDecoMonDurability");
            }
            result = true;
            return result;
        }

        public void Dispose(object obj)
        { 
            
        }
    } 
}

