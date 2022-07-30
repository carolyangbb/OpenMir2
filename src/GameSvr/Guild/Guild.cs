using System;
using System.Collections;
using SystemModule;

namespace GameSvr
{
    public struct TGuildRank
    {
        public int Rank;
        public string RankName;
        public ArrayList MemList;
    }

    public struct TGuildWarInfo
    {
        public TGuild WarGuild;
        public long WarStartTime;
        public long WarRemain;
    }

    public class TGuildAgit
    {
        public int GuildAgitNumber = 0;
        // 장원번호(1-base)
        public string GuildName = String.Empty;
        // 문파명
        public string GuildMasterName = String.Empty;
        // 문주명
        public string GuildMasterNameSecond = String.Empty;
        // 두번째 문주명
        public string RegistrationTime = String.Empty;
        // 등록일시
        public int ContractPeriod = 0;
        // 계약기간(일)
        public int GuildAgitTotalGold = 0;
        // 장원 기부금
        public int ForSaleFlag = 0;
        // 판매 플래그(1,0)
        public int ForSaleMoney = 0;
        // 판매 금액
        public int ForSaleWait = 0;
        // 구입 유예기간(일)
        public string ForSaleGuildName = String.Empty;
        // 구입 예정 문파명
        public string ForSaleGuildMasterName = String.Empty;
        // 구입 예정 문주명
        public string ForSaleTime = String.Empty;
        // -------------------------------------------------------------------
        // TGuildAgit
        //Constructor  Create()
        public TGuildAgit() : base()
        {
            InitGuildAgitRecord();
        }
        //@ Destructor  Destroy()
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
            // 두번째 문주 얻어 오기.
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
            // 7일(기본단위)
            // 장원 기부금
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
            // 마감일 계산
            // {$IFNDEF UNDEF_DEBUG}   //sonmg
            // enddatetime := regdatetime + ContractPeriod;
            // {$ELSE}
            enddatetime = regdatetime + (ContractPeriod / 60 / 24);
            // {$ENDIF}
            // 남은시간 = 마감일 - 현재시각.
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
                // 장원 맵 전체 좌표를 검색하여 각 좌표에 있는
                // 모든 유저들을 지정된 장소로 강제 이동시킨다.
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
                                            // 추방.
                                            cret.SendRefMsg(Grobal2.RM_SPACEMOVE_HIDE, 0, 0, 0, 0, "");
                                            // cret.SpaceMove (GuildAgitMan.ReturnMapName, GuildAgitMan.ReturnX, GuildAgitMan.ReturnY, 0); //공간이동
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
            // 정상상태
            result = 1;
            if (this == null)
            {
                result = -1;
                return result;
            }
            RemainDateTime = GetGuildAgitRemainDateTime();
            if (RemainDateTime < 0)
            {
                // 연체상태
                result = 0;
                // {$IFNDEF UNDEF_DEBUG}   //sonmg
                // if RemainDateTime <= -GUILDAGIT_DAYUNIT then begin
                // {$ELSE}
                if (RemainDateTime <= -(Guild.GUILDAGIT_DAYUNIT / 60 / 24))
                {
                    // {$ENDIF}
                    // 해제상태
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
            // 플래그가 1이고, 구입신청문파가 없으면 TRUE
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
            // 플래그가 0이고, 구입신청문파가 있으면 거래 성사된 장원
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
            // 거래가 성사된 장원중에서
            if (IsSoldOut())
            {
                // 현재 시간이 판매시간 + ForSaleWait 보다 지났으면 Expired
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
            // 이전 장원의 남은 대여기간
            RemainDateTime = GetGuildAgitRemainDateTime();
            // 장원 문파를 새 문파로 교체
            GuildName = ForSaleGuildName;
            // 장원 문주를 새 문주로 교체
            GuildMasterName = ForSaleGuildMasterName;
            GuildMasterNameSecond = "";
            // 등록일 수정(현재시각)
            RegistrationTime = ConvertDateTimeToString(DateTime.Now);
            // 대여기간 초기화
            // ContractPeriod := GUILDAGIT_DAYUNIT;
            // 대여기간 : 7일 이상 남았으면 7일로 초기화, 7일 이하이면 그대로 유지(?)
            if (RemainDateTime <= 0)
            {
                ContractPeriod = 0;
                // end else if RemainDateTime >= GUILDAGIT_DAYUNIT then begin
                // ContractPeriod := GUILDAGIT_DAYUNIT;
            }
            else
            {
                // 남은 기간 반올림
                ContractPeriod = HUtil32.MathRound(RemainDateTime.ToOADate());
            }
            // 장원 기부금(초기화? or 그대로 인계?)
            GuildAgitTotalGold = GuildAgitTotalGold;
            // 0;
            result = true;
            return result;
        }

        public string UpdateGuildMaster()
        {
            string result = String.Empty;
            TGuild aguild;
            // 문주를 현재 문주로 최신화시킨다.
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

namespace GameSvr
{
    public class Guild
    {
        public const int DEFRANK = 99;
        // 문파의 가장 낮은 등급
        public const int GUILDAGIT_DAYUNIT = 7;
        // 7일
        public const int GUILDAGIT_SALEWAIT_DAYUNIT = 1;
        // 1일
        public const int MAXGUILDAGITCOUNT = 100;
        public const int GABOARD_NOTICE_LINE = 3;
        public const int GABOARD_COUNT_PER_PAGE = 10;
        // 장원게시판 1페이지당 라인수.
        public const int GABOARD_MAX_ARTICLE_COUNT = 73;
        // 장원게시판 최대 게시물 수.
        public const string AGITDECOMONFILE = "AgitDecoMon.txt";
        // 장원꾸미기 설치 아이템 리스트 저장.
        public const int MAXCOUNT_DECOMON_PER_AGIT = 50;
        // 꾸미기 아이템 장원당 최대 설치 개수
        public const int GUILDAGITMAXGOLD = 100000000;
        // 문파 장원
        public const int GUILDAGITREGFEE = 10000000;
        public const int GUILDAGITEXTENDFEE = 1000000;
    } // end Guild

}

