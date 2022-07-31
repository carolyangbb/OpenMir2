using System;
using System.Collections.Generic;
using SystemModule;

namespace GameSvr
{
    public class RelationshipDef
    {
        public const int MAX_LOVERCOUNT = 1;
        public const int MAX_MASTERCOUNT = 1;
        public const int MAX_PUPILCOUNT = 5;
    }

    public class TRelationShipInfo
    {
        public string Ownner
        {
            get
            {
                return FOwnner;
            }
            set
            {
                FOwnner = value;
            }
        }
        public string Name
        {
            get
            {
                return FName;
            }
            set
            {
                FName = value;
            }
        }
        public byte State
        {
            get
            {
                return FState;
            }
            set
            {
                FState = value;
            }
        }
        public byte Level
        {
            get
            {
                return FLevel;
            }
            set
            {
                FLevel = value;
            }
        }
        public byte Sex
        {
            get
            {
                return FSex;
            }
            set
            {
                FSex = value;
            }
        }
        public string Date
        {
            get
            {
                return FDate;
            }
            set
            {
                FDate = value;
            }
        }
        public string ServerDate
        {
            get
            {
                return FServerDate;
            }
            set
            {
                FServerDate = value;
            }
        }
        public string MapInfo
        {
            get
            {
                return FMapInfo;
            }
            set
            {
                FMapInfo = value;
            }
        }
        private string FOwnner = string.Empty;
        private string FName = string.Empty;
        public byte FState = 0;
        public byte FLevel = 0;
        private byte FSex = 0;
        private string FDate = string.Empty;
        private string FServerDate = string.Empty;
        private string FMapInfo = string.Empty;

        public TRelationShipInfo() : base()
        {
            FOwnner = "";
            FName = "";
            FState = 0;
            FLevel = 0;
            FSex = 0;
            FDate = GetNowDate();
            FMapInfo = "";
            FServerDate = GetNowDate();
        }

        ~TRelationShipInfo()
        {

        }

        public string GetNowDate()
        {
            return DateTime.Now.ToString("yymmddhhnn");
        }
    }

    public class TRelationShipMgr
    {
        public int ReqSequence
        {
            get
            {
                return GetReqSequence();
            }
            set
            {
                SetReqSequence(value);
            }
        }
        public bool EnableJoinLover
        {
            get
            {
                return FEnableJoinLover;
            }
        }
        private readonly IList<TRelationShipInfo> FItems = null;
        private bool FEnableJoinLover = false;
        private int FReqSequence = 0;
        private long FCancelTime = 0;
        private int FLoverCount = 0;
        private int FMasterCount = 0;
        private int FPupilCount = 0;

        public TRelationShipMgr() : base()
        {
            FItems = new List<TRelationShipInfo>();
            FEnableJoinLover = false;
            FReqSequence = Grobal2.RsReq_None;
            FCancelTime = 0;
            FLoverCount = 0;
            FMasterCount = 0;
            FPupilCount = 0;
        }

        ~TRelationShipMgr()
        {
            RemoveAll();
            // FItems.Free;
        }

        private void RemoveAll()
        {
            TRelationShipInfo Info;
            for (var i = 0; i < FItems.Count; i++)
            {
                Info = FItems[i];
                if ((Info != null))
                {
                    Info.Free();
                    Info = null;
                }
            }
            FItems.Clear();
        }

        private int GetReqSequence()
        {
            if ((FCancelTime == 0) || ((HUtil32.GetTickCount() - FCancelTime) <= Grobal2.MAX_WAITTIME))
            {

            }
            else
            {
                FReqSequence = Grobal2.RsReq_None;
            }
            return FReqSequence;
        }

        private void SetReqSequence(int Sequence)
        {
            if ((FCancelTime == 0) || ((HUtil32.GetTickCount() - FCancelTime) <= Grobal2.MAX_WAITTIME))
            {
                FReqSequence = Sequence;
            }
            else
            {
                FReqSequence = Grobal2.RsReq_None;
            }
            FCancelTime = HUtil32.GetTickCount();
        }

        private string GetDayStr(string datestr, string delimeter)
        {
            string result = "";
            if (datestr.Length >= 6)
            {
                result = "20" + datestr[1] + datestr[2] + delimeter + datestr[3] + datestr[4] + delimeter + datestr[5] + datestr[6];
            }
            return result;
        }

        private string GetDayNow(string datestr, string serverdatestr)
        {
            string result = "0";
            try
            {
                //string str = GetDayStr(datestr, "-");
                //str = HUtil32.GetValidStr3(str, ref strtemp, new string[] { "-" });
                //cYear = ((short)Convert.ToInt32(strtemp));
                //str = HUtil32.GetValidStr3(str, ref strtemp, new string[] { "-" });
                //cMon = ((short)Convert.ToInt32(strtemp));
                //cDay = ((short)Convert.ToInt32(str));
                //cHour = 0;
                //cMin = 0;
                //cSec = 0;
                //cMSec = 0;
                //exdate = Convert.ToInt64(new DateTime(cYear, cMon, cDay));
                //extime = new DateTime(0, 0, 0, cHour, cMin, cSec, cMSec);
                //exdatetime = exdate + extime + 1;
                //str = GetDayStr(serverdatestr, "-");
                //str = HUtil32.GetValidStr3(str, ref strtemp, new string[] { "-" });
                //cYear = ((short)Convert.ToInt32(strtemp));
                //str = HUtil32.GetValidStr3(str, ref strtemp, new string[] { "-" });
                //cMon = ((short)Convert.ToInt32(strtemp));
                //cDay = ((short)Convert.ToInt32(str));
                //cHour = 0;
                //cMin = 0;
                //cSec = 0;
                //cMSec = 0;
                //exdate = Convert.ToInt64(new DateTime(cYear, cMon, cDay));
                //extime = new DateTime(0, 0, 0, cHour, cMin, cSec, cMSec);
                //exdatetime2 = exdate + extime + 1;
                //result = (Convert.ToInt64(exdatetime2 - exdatetime) + 1).ToString();
            }
            catch
            {
                result = "0";
            }
            return result;
        }

        public int GetEnableJoin(int ReqType)
        {
            int result;
            result = 3;
            switch (ReqType)
            {
                case Grobal2.RsState_Lover:
                    if (!FEnableJoinLover)
                    {
                        result = 1;
                        return result;
                    }
                    if (FLoverCount < RelationshipDef.MAX_LOVERCOUNT)
                    {
                        result = 0;
                        return result;
                    }
                    else
                    {
                        result = 2;
                        return result;
                    }
                    break;
                case Grobal2.RsState_Master:
                    if (FPupilCount < RelationshipDef.MAX_PUPILCOUNT)
                    {
                        result = 0;
                        return result;
                    }
                    else
                    {
                        result = 1;
                        return result;
                    }
                    break;
                case Grobal2.RsState_Pupil:
                    if (FMasterCount < RelationshipDef.MAX_MASTERCOUNT)
                    {
                        result = 0;
                        return result;
                    }
                    else
                    {
                        result = 1;
                        return result;
                    }
                    break;
            }
            return result;
        }

        public bool GetEnableJoinReq(int ReqType)
        {
            bool result;
            result = false;
            switch (ReqType)
            {
                case Grobal2.RsState_Lover:
                    if (FEnableJoinLover && (FLoverCount < RelationshipDef.MAX_LOVERCOUNT))
                    {
                        result = true;
                    }
                    break;
            }
            return result;
        }

        public void SetEnable(int ReqType, int enable)
        {
            switch (ReqType)
            {
                case Grobal2.RsState_Lover:
                    if (enable == 1)
                    {
                        FEnableJoinLover = true;
                    }
                    else
                    {
                        FEnableJoinLover = false;
                    }
                    break;
            }
        }

        public int GetEnable(int ReqType)
        {
            int result;
            result = 0;
            switch (ReqType)
            {
                case Grobal2.RsState_Lover:
                    if (FEnableJoinLover)
                    {
                        result = 1;
                    }
                    else
                    {
                        result = 0;
                    }
                    break;
            }
            return result;
        }

        public string GetListMsg(int ReqType, ref int ListCount)
        {
            string result;
            int i;
            TRelationShipInfo Info;
            string msg;
            ListCount = 0;
            msg = "";
            for (i = 0; i < FItems.Count; i++)
            {
                Info = FItems[i];
                if (Info.FState == ReqType)
                {
                    msg = msg + (Info.FState).ToString() + ":" + Info.Name + ":" + (Info.Level).ToString() + ":" + (Info.Sex).ToString() + ":" + Info.Date + ":" + Info.ServerDate + ":" + Info.MapInfo + "/";
                    ListCount++;
                }
            }
            result = msg;
            return result;
        }

        public bool GetInfo(string Name_, ref TRelationShipInfo Info_)
        {
            bool result;
            int i;
            TRelationShipInfo Info;
            result = false;
            Info_ = null;
            for (i = 0; i < FItems.Count; i++)
            {
                Info = FItems[i];
                if ((Info != null) && (Info.Name == Name_))
                {
                    Info_ = Info;
                    result = true;
                    return result;
                }
            }
            return result;
        }

        public bool Find(string Name_)
        {
            TRelationShipInfo Info = null;
            bool result = GetInfo(Name_, ref Info);
            return result;
        }

        public bool Add(string Ownner_, string Other_, byte State_, byte Level_, byte Sex_, ref string Date_, string MapInfo_)
        {
            bool result;
            TRelationShipInfo Info;
            result = false;
            if ((Ownner_ == "") || (Other_ == "") || (Level_ == 0))
            {
                return result;
            }
            Info = null;
            if (!Find(Other_))
            {
                Info = new TRelationShipInfo();
                Info.Ownner = Ownner_;
                Info.Name = Other_;
                Info.State = State_;
                Info.Level = Level_;
                Info.Sex = Sex_;
                if (Date_ != "")
                {
                    Info.Date = Date_;
                }
                else
                {
                    Date_ = Info.Date;
                }
                Info.MapInfo = MapInfo_;
                FItems.Add(Info);
                switch (State_)
                {
                    case Grobal2.RsState_Lover:
                        FLoverCount++;
                        break;
                    case Grobal2.RsState_Master:
                        FMasterCount++;
                        break;
                    case Grobal2.RsState_Pupil:
                        FPupilCount++;
                        break;
                }
                result = true;
            }
            return result;
        }

        public bool Delete(string Name_)
        {
            TRelationShipInfo Info;
            bool result = false;
            for (var i = 0; i < FItems.Count; i++)
            {
                Info = FItems[i];
                if ((Info != null) && (Info.Name == Name_))
                {
                    switch (Info.State)
                    {
                        case Grobal2.RsState_Lover:
                            FLoverCount -= 1;
                            break;
                        case Grobal2.RsState_Master:
                            FMasterCount -= 1;
                            break;
                        case Grobal2.RsState_Pupil:
                            FPupilCount -= 1;
                            break;
                    }
                    Info.Free();
                    Info = null;
                    FItems.RemoveAt(i);
                    result = true;
                    return result;
                }
            }
            return result;
        }

        public bool ChangeLevel(string Name_, byte Level_)
        {
            bool result = false;
            if (Level_ > 0)
            {
                TRelationShipInfo Info = null;
                if (GetInfo(Name_, ref Info))
                {
                    if (Info != null)
                    {
                        Info.Level = Level_;
                        result = true;
                    }
                }
            }
            return result;
        }

        public int GetLoverCount()
        {
            return FLoverCount;
        }

        public int GetMasterCount()
        {
            return FMasterCount;
        }

        public int GetPupilCount()
        {
            return FPupilCount;
        }

        public string GetLoverName()
        {
            TRelationShipInfo Info;
            string result = "";
            for (var i = 0; i < FItems.Count; i++)
            {
                Info = FItems[i];
                if ((Info != null))
                {
                    result = Info.Name;
                    return result;
                }
            }
            return result;
        }

        public string GetMasterName()
        {
            string result = null;
            TRelationShipInfo Info;
            for (var i = 0; i < FItems.Count; i++)
            {
                Info = FItems[i];
                if ((Info != null) && (Info.FState == Grobal2.RsState_Master))
                {
                    result = Info.Name;
                    return result;
                }
            }
            return result;
        }

        public string GetPupilName(int idx)
        {
            TRelationShipInfo Info;
            string result = "";
            for (var i = 0; i < FItems.Count; i++)
            {
                if (i == idx)
                {
                    Info = FItems[i];
                    if ((Info != null))
                    {
                        result = Info.Name;
                        break;
                    }
                }
            }
            return result;
        }

        public bool GetPupilName(string useName)
        {
            TRelationShipInfo Info;
            bool result = false;
            for (var i = 0; i < FItems.Count; i++)
            {
                Info = FItems[i];
                if (useName.ToLower().Equals(Info.Name.ToLower()))
                {
                    result = true;
                    break;
                }
            }
            return result;
        }

        public string GetLoverDays()
        {
            TRelationShipInfo Info;
            string result = "";
            for (var i = 0; i < FItems.Count; i++)
            {
                Info = FItems[i];
                if ((Info != null))
                {
                    result = GetDayNow(Info.Date, Info.ServerDate);
                    return result;
                }
            }
            return result;
        }

    }
}
