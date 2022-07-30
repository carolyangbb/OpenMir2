using System;
using System.Collections;
using SystemModule;

namespace GameSvr
{
    public class TGuildAgit
    {
        public int GuildAgitNumber = 0;
        // 厘盔锅龋(1-base)
        public string GuildName = String.Empty;
        // 巩颇疙
        public string GuildMasterName = String.Empty;
        // 巩林疙
        public string GuildMasterNameSecond = String.Empty;
        // 滴锅掳 巩林疙
        public string RegistrationTime = String.Empty;
        // 殿废老矫
        public int ContractPeriod = 0;
        // 拌距扁埃(老)
        public int GuildAgitTotalGold = 0;
        // 厘盔 扁何陛
        public int ForSaleFlag = 0;
        // 魄概 敲贰弊(1,0)
        public int ForSaleMoney = 0;
        // 魄概 陛咀
        public int ForSaleWait = 0;
        // 备涝 蜡抗扁埃(老)
        public string ForSaleGuildName = String.Empty;
        // 备涝 抗沥 巩颇疙
        public string ForSaleGuildMasterName = String.Empty;
        // 备涝 抗沥 巩林疙
        public string ForSaleTime = String.Empty;

        public TGuildAgit() : base()
        {
            InitGuildAgitRecord();
        }

        ~TGuildAgit()
        {
            base.Destroy();
        }
        private void InitGuildAgitRecord()
        {
            if (this == null)
            {
                return;
            }
            GuildAgitNumber = -1;
            GuildName = "";
            GuildMasterName = "";
            GuildMasterNameSecond = "";
            RegistrationTime = "";
            ContractPeriod = -1;
            GuildAgitTotalGold = 0;
            ForSaleFlag = 0;
            ForSaleMoney = 0;
            ForSaleWait = 0;
            ForSaleGuildName = "";
            ForSaleGuildMasterName = "";
            ForSaleTime = "";
        }

        public void SetGuildAgitRecord(int agitnumber, string gname, string mastername, string regtime, int period, int agittotalgold)
        {
            TGuild aguild;
            if (this == null)
            {
                return;
            }
            GuildAgitNumber = agitnumber;
            GuildName = gname;
            GuildMasterName = mastername;
            GuildMasterNameSecond = "";
            RegistrationTime = regtime;
            ContractPeriod = period;
            GuildAgitTotalGold = agittotalgold;
            // 滴锅掳 巩林 掘绢 坷扁.
            aguild = svMain.GuildMan.GetGuild(gname);
            if (aguild != null)
            {
                GuildMasterNameSecond = aguild.GetAnotherGuildMaster();
            }
        }

        public void SetGuildAgitForSaleRecord(int saleflag, int salemoney, int salewait, string salegname, string salemastername, string saletime)
        {
            if (this == null)
            {
                return;
            }
            ForSaleFlag = saleflag;
            ForSaleMoney = salemoney;
            ForSaleWait = salewait;
            ForSaleGuildName = salegname;
            ForSaleGuildMasterName = salemastername;
            ForSaleTime = saletime;
        }

        public bool AddGuildAgitRecord(int nextnumber, string gname, string mastername, string secondmastername)
        {
            bool result;
            DateTime RegDateTime;
            // RegDate, RegTime : TDateTime;
            // Year, Month, Day : Word;
            // Hour, Min, Sec, MSec : Word;
            result = true;
            if (this == null)
            {
                result = false;
                return result;
            }
            RegDateTime = DateTime.Now;
            RegistrationTime = ConvertDateTimeToString(RegDateTime);
            GuildAgitNumber = nextnumber;
            GuildName = gname;
            GuildMasterName = mastername;
            GuildMasterNameSecond = secondmastername;
            ContractPeriod = Guild.GUILDAGIT_DAYUNIT;
            // 7老(扁夯窜困)
            // 厘盔 扁何陛
            GuildAgitTotalGold = 0;
            return result;
        }

        public DateTime GetGuildAgitRemainDateTime()
        {
            DateTime result;
            DateTime regdatetime;
            // nowdatetime, nowdate, nowtime : TDateTime;
            DateTime enddatetime;
            DateTime remaindatetime;
            // str, data: string;
            // Year, Month, Day : Word;
            // Hour, Min, Sec, MSec : Word;
            // RemainSeconds : integer;
            result = -100;
            if (this == null)
            {
                return result;
            }
            regdatetime = ConvertStringToDatetime(RegistrationTime);
            // 付皑老 拌魂
            // {$IFNDEF UNDEF_DEBUG}   //sonmg
            // enddatetime := regdatetime + ContractPeriod;
            // {$ELSE}
            enddatetime = regdatetime + (ContractPeriod / 60 / 24);
            // {$ENDIF}
            // 巢篮矫埃 = 付皑老 - 泅犁矫阿.
            remaindatetime = enddatetime - DateTime.Now;
            result = remaindatetime;
            return result;
        }

        public void ExpulsionMembers()
        {
            ArrayList list;
            TCreature cret;
            int i;
            int j;
            int ix;
            int iy;
            TEnvirnoment env;
            if (this == null)
            {
                return;
            }
            try
            {
                list = new ArrayList();
                // 厘盔 甘 傈眉 谅钎甫 八祸窍咯 阿 谅钎俊 乐绰
                // 葛电 蜡历甸阑 瘤沥等 厘家肺 碍力 捞悼矫挪促.
                for (i = 0; i <= 3; i++)
                {
                    env = svMain.GrobalEnvir.GetEnvir(svMain.GuildAgitMan.GuildAgitMapName[i] + GuildAgitNumber.ToString());
                    if (env != null)
                    {
                        for (ix = 0; ix < env.MapWidth; ix++)
                        {
                            for (iy = 0; iy < env.MapHeight; iy++)
                            {
                                list.Clear();
                                env.GetAllCreature(ix, iy, true, list);
                                for (j = 0; j < list.Count; j++)
                                {
                                    cret = (TCreature)list[j];
                                    if (cret != null)
                                    {
                                        if (cret.RaceServer == Grobal2.RC_USERHUMAN)
                                        {
                                            // 眠规.
                                            cret.SendRefMsg(Grobal2.RM_SPACEMOVE_HIDE, 0, 0, 0, 0, "");
                                            // cret.SpaceMove (GuildAgitMan.ReturnMapName, GuildAgitMan.ReturnX, GuildAgitMan.ReturnY, 0); //傍埃捞悼
                                            cret.UserSpaceMove(cret.HomeMap, cret.HomeX.ToString(), cret.HomeY.ToString());
                                            cret.SysMsg("Your guild has been expelled due to rent term expiration.", 0);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                list.Free();
            }
            catch
            {
                svMain.MainOutMessage("[Exception]TGuildAgit.ExpulsionMembers");
            }
        }

        public bool IsExpired()
        {
            bool result;
            result = false;
            if (this == null)
            {
                result = true;
                return result;
            }
            // {$IFNDEF UNDEF_DEBUG}   //sonmg
            // if GetGuildAgitRemainDateTime <= -GUILDAGIT_DAYUNIT then begin
            // {$ELSE}
            if (GetGuildAgitRemainDateTime() <= -(Guild.GUILDAGIT_DAYUNIT / 60 / 24))
            {
                // {$ENDIF}
                result = true;
            }
            return result;
        }

        public int GetCurrentDelayStatus()
        {
            int result;
            DateTime RemainDateTime;
            // 沥惑惑怕
            result = 1;
            if (this == null)
            {
                result = -1;
                return result;
            }
            RemainDateTime = GetGuildAgitRemainDateTime();
            if (RemainDateTime < 0)
            {
                // 楷眉惑怕
                result = 0;
                // {$IFNDEF UNDEF_DEBUG}   //sonmg
                // if RemainDateTime <= -GUILDAGIT_DAYUNIT then begin
                // {$ELSE}
                if (RemainDateTime <= -(Guild.GUILDAGIT_DAYUNIT / 60 / 24))
                {
                    // {$ENDIF}
                    // 秦力惑怕
                    result = -1;
                }
            }
            return result;
        }

        public DateTime GetGuildAgitRegDateTime()
        {
            DateTime result;
            DateTime regdatetime;
            result = -100;
            if (this == null)
            {
                return result;
            }
            regdatetime = ConvertStringToDatetime(RegistrationTime);
            result = regdatetime;
            return result;
        }

        public DateTime ConvertStringToDatetime(string datestring)
        {
            DateTime result;
            DateTime regdatetime;
            DateTime regdate;
            DateTime regtime;
            string str;
            string data = string.Empty;
            short Year;
            short Month;
            short Day;
            short Hour;
            short Min;
            short Sec;
            short MSec;
            result = -100;
            try
            {
                str = datestring;
                str = HUtil32.GetValidStr3(str, ref data, new string[] { ".", ":" });
                Year = (short)HUtil32.Str_ToInt(data, 0);
                str = HUtil32.GetValidStr3(str, ref data, new string[] { ".", ":" });
                Month = (short)HUtil32.Str_ToInt(data, 0);
                str = HUtil32.GetValidStr3(str, ref data, new string[] { ".", ":" });
                Day = (short)HUtil32.Str_ToInt(data, 0);
                str = HUtil32.GetValidStr3(str, ref data, new string[] { ".", ":" });
                Hour = (short)HUtil32.Str_ToInt(data, 0);
                str = HUtil32.GetValidStr3(str, ref data, new string[] { ".", ":" });
                Min = (short)HUtil32.Str_ToInt(data, 0);
                str = HUtil32.GetValidStr3(str, ref data, new string[] { ".", ":" });
                Sec = (short)HUtil32.Str_ToInt(data, 0);
                str = HUtil32.GetValidStr3(str, ref data, new string[] { ".", ":" });
                MSec = (short)HUtil32.Str_ToInt(data, 0);
                regdate = Convert.ToInt64(new DateTime(Year, Month, Day));
                regtime = new DateTime(0, 0, 0, Hour, Min, Sec, MSec);
                regdatetime = regdate + regtime;
                result = regdatetime;
            }
            catch
            {
                svMain.MainOutMessage("[Exception]TGuildAgit.ConvertStringToDatetime");
            }
            return result;
        }

        public string ConvertDateTimeToString(DateTime datetime)
        {
            string result;
            short Year;
            short Month;
            short Day;
            short Hour;
            short Min;
            short Sec;
            short MSec;
            result = "";
            try
            {
                Year = (short)datetime.Year;
                Month = (short)datetime.Month;
                Day = (short)datetime.Day;
                Hour = (short)datetime.Hour;
                Min = (short)datetime.Minute;
                Sec = (short)datetime.Second;
                MSec = (short)datetime.Millisecond;
                result = Year.ToString() + "." + Month.ToString() + "." + Day.ToString() + "." + Hour.ToString() + ":" + Min.ToString() + ":" + Sec.ToString();
            }
            catch
            {
                svMain.MainOutMessage("[Exception]TGuildAgit.ConvertDateTimeToString");
            }
            return result;
        }

        public bool IsForSale()
        {
            bool result;
            result = false;
            // 敲贰弊啊 1捞绊, 备涝脚没巩颇啊 绝栏搁 TRUE
            if ((ForSaleFlag == 1) && (ForSaleGuildName == ""))
            {
                result = true;
            }
            return result;
        }

        public bool IsSoldOut()
        {
            bool result;
            result = false;
            // 敲贰弊啊 0捞绊, 备涝脚没巩颇啊 乐栏搁 芭贰 己荤等 厘盔
            if ((ForSaleFlag == 0) && (ForSaleGuildName != ""))
            {
                result = true;
            }
            return result;
        }

        public bool IsSoldOutExpired()
        {
            bool result;
            DateTime SaleDateTime;
            result = false;
            // 芭贰啊 己荤等 厘盔吝俊辑
            if (IsSoldOut())
            {
                // 泅犁 矫埃捞 魄概矫埃 + ForSaleWait 焊促 瘤车栏搁 Expired
                SaleDateTime = ConvertStringToDatetime(ForSaleTime);
                // {$IFNDEF UNDEF_DEBUG}   //sonmg
                // if Now > SaleDateTime + ForSaleWait then begin
                // {$ELSE}
                if (DateTime.Now > SaleDateTime + (ForSaleWait / 24 / 60))
                {
                    // {$ENDIF}
                    result = true;
                }
            }
            return result;
        }

        public void ResetForSaleFields()
        {
            ForSaleFlag = 0;
            ForSaleMoney = 0;
            ForSaleWait = 0;
            ForSaleGuildName = "";
            ForSaleGuildMasterName = "";
            ForSaleTime = "";
        }

        public bool ChangeGuildAgitMaster()
        {
            bool result;
            DateTime RemainDateTime;
            result = false;
            if ((ForSaleGuildName == "") || (ForSaleGuildMasterName == ""))
            {
                return result;
            }
            // 捞傈 厘盔狼 巢篮 措咯扁埃
            RemainDateTime = GetGuildAgitRemainDateTime();
            // 厘盔 巩颇甫 货 巩颇肺 背眉
            GuildName = ForSaleGuildName;
            // 厘盔 巩林甫 货 巩林肺 背眉
            GuildMasterName = ForSaleGuildMasterName;
            GuildMasterNameSecond = "";
            // 殿废老 荐沥(泅犁矫阿)
            RegistrationTime = ConvertDateTimeToString(DateTime.Now);
            // 措咯扁埃 檬扁拳
            // ContractPeriod := GUILDAGIT_DAYUNIT;
            // 措咯扁埃 : 7老 捞惑 巢疽栏搁 7老肺 檬扁拳, 7老 捞窍捞搁 弊措肺 蜡瘤(?)
            if (RemainDateTime <= 0)
            {
                ContractPeriod = 0;
                // end else if RemainDateTime >= GUILDAGIT_DAYUNIT then begin
                // ContractPeriod := GUILDAGIT_DAYUNIT;
            }
            else
            {
                // 巢篮 扁埃 馆棵覆
                ContractPeriod = HUtil32.MathRound(RemainDateTime.ToOADate());
            }
            // 厘盔 扁何陛(檬扁拳? or 弊措肺 牢拌?)
            GuildAgitTotalGold = GuildAgitTotalGold;
            // 0;
            result = true;
            return result;
        }

        public string UpdateGuildMaster()
        {
            string result = String.Empty;
            TGuild aguild;
            // 巩林甫 泅犁 巩林肺 弥脚拳矫挪促.
            if (GuildName != "")
            {
                aguild = svMain.GuildMan.GetGuild(GuildName);
                if (aguild != null)
                {
                    GuildMasterName = aguild.GetGuildMaster();
                    GuildMasterNameSecond = aguild.GetAnotherGuildMaster();
                }
            }
            return result;
        }

    }
}

