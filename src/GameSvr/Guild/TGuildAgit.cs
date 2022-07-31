using System;
using System.Collections;
using SystemModule;

namespace GameSvr
{
    public class TGuildAgit
    {
        public int GuildAgitNumber = 0;
        public string GuildName = string.Empty;
        public string GuildMasterName = string.Empty;
        public string GuildMasterNameSecond = string.Empty;
        public string RegistrationTime = string.Empty;
        public int ContractPeriod = 0;
        public int GuildAgitTotalGold = 0;
        public int ForSaleFlag = 0;
        public int ForSaleMoney = 0;
        public int ForSaleWait = 0;
        public string ForSaleGuildName = string.Empty;
        public string ForSaleGuildMasterName = string.Empty;
        public string ForSaleTime = string.Empty;

        public TGuildAgit() : base()
        {
            InitGuildAgitRecord();
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
            TGuild aguild = M2Share.GuildMan.GetGuild(gname);
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
            DateTime RegDateTime;
            bool result = true;
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
            GuildAgitTotalGold = 0;
            return result;
        }

        public DateTime GetGuildAgitRemainDateTime()
        {
            DateTime regdatetime;
            DateTime result = DateTime.Now;
            if (this == null)
            {
                return result;
            }
            regdatetime = ConvertStringToDatetime(RegistrationTime);
            //enddatetime = regdatetime + (ContractPeriod / 60 / 24);
            //remaindatetime = enddatetime - DateTime.Now;
            //result = remaindatetime;
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
                for (i = 0; i <= 3; i++)
                {
                    env = M2Share.GrobalEnvir.GetEnvir(M2Share.GuildAgitMan.GuildAgitMapName[i] + GuildAgitNumber.ToString());
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
                                            cret.SendRefMsg(Grobal2.RM_SPACEMOVE_HIDE, 0, 0, 0, 0, "");
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
                M2Share.MainOutMessage("[Exception]TGuildAgit.ExpulsionMembers");
            }
        }

        public bool IsExpired()
        {
            bool result = false;
            if (this == null)
            {
                result = true;
                return result;
            }
            //if (GetGuildAgitRemainDateTime() <= -(Guild.GUILDAGIT_DAYUNIT / 60 / 24))
            //{
            //    result = true;
            //}
            return result;
        }

        public int GetCurrentDelayStatus()
        {
            int result = 1;
            if (this == null)
            {
                result = -1;
                return result;
            }
            DateTime RemainDateTime = GetGuildAgitRemainDateTime();
            //if (RemainDateTime < 0)
            //{
            //    result = 0;
            //    if (RemainDateTime <= -(Guild.GUILDAGIT_DAYUNIT / 60 / 24))
            //    {
            //        result = -1;
            //    }
            //}
            return result;
        }

        public DateTime GetGuildAgitRegDateTime()
        {
            DateTime result = DateTime.Now;
            DateTime regdatetime;
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
            DateTime result = DateTime.Now;
            DateTime regdatetime = DateTime.Now;
            string str;
            string data = string.Empty;
            short Year;
            short Month;
            short Day;
            short Hour;
            short Min;
            short Sec;
            short MSec;
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
                //regdate = Convert.ToInt64(new DateTime(Year, Month, Day));
                //regtime = new DateTime(0, 0, 0, Hour, Min, Sec, MSec);
                //regdatetime = regdate + regtime;
                result = regdatetime;
            }
            catch
            {
                M2Share.MainOutMessage("[Exception]TGuildAgit.ConvertStringToDatetime");
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
                M2Share.MainOutMessage("[Exception]TGuildAgit.ConvertDateTimeToString");
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
            if (IsSoldOut())
            {
                SaleDateTime = ConvertStringToDatetime(ForSaleTime);
                //if (DateTime.Now > SaleDateTime + (ForSaleWait / 24 / 60))
                //{
                //    result = true;
                //}
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
            RemainDateTime = GetGuildAgitRemainDateTime();
            GuildName = ForSaleGuildName;
            GuildMasterName = ForSaleGuildMasterName;
            GuildMasterNameSecond = "";
            RegistrationTime = ConvertDateTimeToString(DateTime.Now);
            //if (RemainDateTime <= 0)
            //{
            //    ContractPeriod = 0;
            //}
            //else
            //{
            //    ContractPeriod = HUtil32.MathRound(RemainDateTime.ToOADate());
            //}
            GuildAgitTotalGold = GuildAgitTotalGold;
            result = true;
            return result;
        }

        public string UpdateGuildMaster()
        {
            string result = string.Empty;
            TGuild aguild;
            if (GuildName != "")
            {
                aguild = M2Share.GuildMan.GetGuild(GuildName);
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

