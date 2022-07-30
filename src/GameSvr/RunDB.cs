using System;
using System.Threading;
using SystemModule;

namespace GameSvr
{
    public class RunDB
    {
        public static int LatestDBSendMsg = 0;
        public static int LatestDBError = 0;
        public static bool g_DbUse = false;

        public static void MakeDefMsg(ref TDefaultMessage DMsg, ushort msg, int llong, ushort aparam, ushort atag, ushort nseries)
        {
            DMsg.Ident = msg;
            DMsg.Recog = llong;
            DMsg.Param = aparam;
            DMsg.Tag = atag;
            DMsg.Series = nseries;
        }

        public static void FDBLoadHuman(FDBRecord prcd, ref TUserHuman hum)
        {
            int i;
            TUserItem pu;
            TUserMagic pum;
            TDefMagic pdefm;
            var _wvar1 = prcd.Block.DBHuman;
            hum.UserName = _wvar1.UserName;
            hum.MapName = _wvar1.MapName;
            hum.CX = _wvar1.CX;
            hum.CY = _wvar1.CY;
            hum.Dir = _wvar1.Dir;
            hum.Hair = _wvar1.Hair;
            hum.Sex = _wvar1.Sex;
            hum.Job = _wvar1.Job;
            hum.Gold = _wvar1.Gold;
            hum.GameGold = _wvar1.GameGold;
            hum.Abil.Level = _wvar1.Abil_Level;
            hum.Abil.HP = _wvar1.Abil_HP;
            hum.Abil.MP = _wvar1.Abil_MP;
            hum.Abil.Exp = (int)_wvar1.Abil_Exp;
            hum.WAbil.Level = _wvar1.Abil_Level;
            hum.WAbil.HP = _wvar1.Abil_HP;
            hum.WAbil.MP = _wvar1.Abil_MP;
            hum.WAbil.Exp = (int)_wvar1.Abil_Exp;
            Move(_wvar1.StatusArr, hum.StatusArr, sizeof(short) * Grobal2.STATUSARR_SIZE);
            hum.HomeMap = _wvar1.HomeMap;
            hum.HomeX = _wvar1.HomeX;
            hum.HomeY = _wvar1.HomeY;
            hum.PlayerKillingPoint = _wvar1.PkPoint;
            if (_wvar1.AllowParty > 0)
            {
                hum.AllowGroup = true;
            }
            else
            {
                hum.AllowGroup = false;
            }
            hum.FreeGulityCount = _wvar1.FreeGulityCount;
            hum.HumAttackMode = _wvar1.AttackMode;
            hum.IncHealth = _wvar1.IncHealth;
            hum.IncSpell = _wvar1.IncSpell;
            hum.IncHealing = _wvar1.IncHealing;
            hum.FightZoneDieCount = _wvar1.FightZoneDie;
            hum.UserId = _wvar1.UserId;
            hum.DBVersion = _wvar1.DBVersion;
            hum.BonusApply = _wvar1.BonusApply;
#if FOR_ABIL_POINT
                        hum.BonusAbil = _wvar1.BonusAbil;
                        hum.CurBonusAbil = _wvar1.CurBonusAbil;
                        hum.BonusPoint = _wvar1.BonusPoint;
#endif
            hum.CGHIUseTime = (ushort)_wvar1.CGHIUseTime;
            hum.BodyLuck = _wvar1.SolveDouble(_wvar1.BodyLuck);
            hum.BoEnableRecall = _wvar1.BoEnableGRecall;
            Move(_wvar1.QuestOpenIndex, hum.QuestIndexOpenStates, Grobal2.MAXQUESTINDEXBYTE);
            Move(_wvar1.QuestFinIndex, hum.QuestIndexFinStates, Grobal2.MAXQUESTINDEXBYTE);
            Move(_wvar1.Quest, hum.QuestStates, Grobal2.MAXQUESTBYTE);
            hum.NotReadTag = _wvar1.HorseRace;
            hum.SecondsCard = _wvar1.SecondsCard;
            hum.HairColorR = _wvar1.HairColorR;
            hum.HairColorG = _wvar1.HairColorG;
            hum.HairColorB = _wvar1.HairColorB;
            if ((hum.HairColorR & 0x01) == 0)
            {
                hum.BoEnableAgitRecall = false;
            }
            else
            {
                hum.BoEnableAgitRecall = true;
            }
            var _wvar2 = prcd.Block.DBBagItem;
            hum.UseItems[Grobal2.U_DRESS] = _wvar2.uDress;
            hum.UseItems[Grobal2.U_WEAPON] = _wvar2.uWeapon;
            hum.UseItems[Grobal2.U_RIGHTHAND] = _wvar2.uRightHand;
            hum.UseItems[Grobal2.U_NECKLACE] = _wvar2.uNecklace;
            hum.UseItems[Grobal2.U_HELMET] = _wvar2.uHelmet;
            hum.UseItems[Grobal2.U_ARMRINGL] = _wvar2.uArmRingL;
            hum.UseItems[Grobal2.U_ARMRINGR] = _wvar2.uArmRingR;
            hum.UseItems[Grobal2.U_RINGL] = _wvar2.uRingL;
            hum.UseItems[Grobal2.U_RINGR] = _wvar2.uRingR;
            hum.UseItems[Grobal2.U_BUJUK] = _wvar2.uBujuk;
            hum.UseItems[Grobal2.U_BELT] = _wvar2.uBelt;
            hum.UseItems[Grobal2.U_BOOTS] = _wvar2.uBoots;
            hum.UseItems[Grobal2.U_CHARM] = _wvar2.uCharm;
            for (i = 0; i < Grobal2.MAXBAGITEM; i++)
            {
                if (_wvar2.Bags[i].Index > 0)
                {
                    pu = new TUserItem();
                    pu = _wvar2.Bags[i];
                    hum.ItemList.Add(pu);
                }
            }
            object _wvar3 = prcd.Block.DBUseMagic;
            for (i = 0; i < Grobal2.MAXUSERMAGIC; i++)
            {
                if (_wvar3.Magics[i].MagicId > 0)
                {
                    pdefm = svMain.UserEngine.GetDefMagicFromId(_wvar3.Magics[i].MagicId);
                    if (pdefm != null)
                    {
                        pum = new TUserMagic();
                        pum.pDef = pdefm;
                        pum.MagicId = _wvar3.Magics[i].MagicId;
                        pum.Level = _wvar3.Magics[i].Level;
                        pum.Key = _wvar3.Magics[i].Key;
                        pum.CurTrain = _wvar3.Magics[i].CurTrain;
                        hum.MagicList.Add(pum);
                    }
                }
                else
                {
                    break;
                }
            }
            object _wvar4 = prcd.Block.DBSaveItem;
            for (i = 0; i < Grobal2.MAXSAVEITEM; i++)
            {
                if (_wvar4.Items[i].Index > 0)
                {
                    pu = new TUserItem();
                    pu = _wvar4.Items[i];
                    hum.SaveItems.Add(pu);
                }
            }
        }

        public static void FDBMakeHumRcd(TUserHuman hum, FDBRecord prcd)
        {
            int i;
            TUseMagicInfo umi;
            var _wvar1 = prcd.Block.DBHuman;
            //StrPCopy(_wvar1.UserName, hum.UserName);
            //StrPCopy(_wvar1.MapName, hum.MapName);
            _wvar1.CX = hum.CX;
            _wvar1.CY = hum.CY;
            _wvar1.Dir = hum.Dir;
            _wvar1.Hair = hum.Hair;
            _wvar1.Sex = hum.Sex;
            _wvar1.Job = hum.Job;
            _wvar1.Gold = hum.Gold;
            _wvar1.GameGold = hum.GameGold;
            _wvar1.Abil_Level = hum.Abil.Level;
            _wvar1.Abil_Exp = hum.Abil.Exp;
            _wvar1.Abil_HP = hum.WAbil.HP;
            _wvar1.Abil_MP = hum.WAbil.MP;
            Move(hum.StatusArr, _wvar1.StatusArr, sizeof(short) * Grobal2.STATUSARR_SIZE);
            StrPCopy(_wvar1.HomeMap, hum.HomeMap);
            _wvar1.HomeX = hum.HomeX;
            _wvar1.HomeY = hum.HomeY;
            _wvar1.PKPoint = hum.PlayerKillingPoint;
            if (hum.AllowGroup)
            {
                _wvar1.AllowParty = 1;
            }
            else
            {
                _wvar1.AllowParty = 0;
            }
            _wvar1.FreeGulityCount = hum.FreeGulityCount;
            _wvar1.AttackMode = hum.HumAttackMode;
            _wvar1.IncHealth = hum.IncHealth;
            _wvar1.IncSpell = hum.IncSpell;
            _wvar1.IncHealing = hum.IncHealing;
            _wvar1.FightZoneDie = hum.FightZoneDieCount;
            StrPCopy(_wvar1.UserId, hum.UserId);
            _wvar1.DBVersion = (byte)hum.DBVersion;
            _wvar1.BonusApply = hum.BonusApply;
#if FOR_ABIL_POINT
                        _wvar1.BonusAbil = hum.BonusAbil;
                        _wvar1.CurBonusAbil = hum.CurBonusAbil;
                        _wvar1.BonusPoint = hum.BonusPoint;
#endif
            _wvar1.CGHIUseTime = (short)hum.CGHIUseTime;
            _wvar1.BodyLuck = _wvar1.PackDouble(hum.BodyLuck);
            _wvar1.BoEnableGRecall = hum.BoEnableRecall;
            //Move(hum.QuestIndexOpenStates, _wvar1.QuestOpenIndex, Grobal2.MAXQUESTINDEXBYTE);
            //Move(hum.QuestIndexFinStates, _wvar1.QuestFinIndex, Grobal2.MAXQUESTINDEXBYTE);
            //Move(hum.QuestStates, _wvar1.Quest, Grobal2.MAXQUESTBYTE);
            _wvar1.HorseRace = (byte)hum.NotReadTag;
            _wvar1.SecondsCard = hum.SecondsCard;
            _wvar1.HairColorR = hum.HairColorR;
            _wvar1.HairColorG = hum.HairColorG;
            _wvar1.HairColorB = hum.HairColorB;
            if (hum.BoEnableAgitRecall == false)
            {
                _wvar1.HairColorR = _wvar1.HairColorR && ~0x01;
            }
            else
            {
                _wvar1.HairColorR = _wvar1.HairColorR || 0x01;
            }
            var _wvar2 = prcd.Block.DBBagItem;
            _wvar2.uDress = hum.UseItems[Grobal2.U_DRESS];
            _wvar2.uWeapon = hum.UseItems[Grobal2.U_WEAPON];
            _wvar2.uRightHand = hum.UseItems[Grobal2.U_RIGHTHAND];
            _wvar2.uNecklace = hum.UseItems[Grobal2.U_NECKLACE];
            _wvar2.uHelmet = hum.UseItems[Grobal2.U_HELMET];
            _wvar2.uArmRingL = hum.UseItems[Grobal2.U_ARMRINGL];
            _wvar2.uArmRingR = hum.UseItems[Grobal2.U_ARMRINGR];
            _wvar2.uRingL = hum.UseItems[Grobal2.U_RINGL];
            _wvar2.uRingR = hum.UseItems[Grobal2.U_RINGR];
            _wvar2.uBujuk = hum.UseItems[Grobal2.U_BUJUK];
            _wvar2.uBelt = hum.UseItems[Grobal2.U_BELT];
            _wvar2.uBoots = hum.UseItems[Grobal2.U_BOOTS];
            _wvar2.uCharm = hum.UseItems[Grobal2.U_CHARM];
            for (i = 0; i < hum.ItemList.Count; i++)
            {
                if (i < Grobal2.MAXBAGITEM)
                {
                    _wvar2.Bags[i] = hum.ItemList[i];
                }
                else
                {
                    break;
                }
            }
            for (i = 0; i < Grobal2.MAXUSERMAGIC; i++)
            {
                if (i >= hum.MagicList.Count)
                {
                    break;
                }
                umi.MagicId = (short)((TUserMagic)hum.MagicList[i]).MagicId;
                umi.Level = ((TUserMagic)hum.MagicList[i]).Level;
                umi.Key = ((TUserMagic)hum.MagicList[i]).Key;
                umi.CurTrain = ((TUserMagic)hum.MagicList[i]).CurTrain;
                prcd.Block.DBUseMagic.Magics[i] = umi;
            }
            object _wvar3 = prcd.Block.DBSaveItem;
            for (i = 0; i < hum.SaveItems.Count; i++)
            {
                if (i < Grobal2.MAXSAVEITEM)
                {
                    _wvar3.Items[i] = (TUserItem)hum.SaveItems[i];
                }
            }
        }

        public static bool LoadHumanCharacter(string uid, string usrname, string useraddr, int certify, ref FDBRecord rcd)
        {
            bool result = false;
            //FillChar(rcd, sizeof(FDBRecord), '\0');
            if (LoadHumanRcd(uid, usrname, useraddr, certify, ref rcd))
            {
                if ((rcd.Block.DBHuman.UserName == usrname) && ((rcd.Block.DBHuman.UserId == "") || (rcd.Block.DBHuman.UserId.ToLower().CompareTo(uid.ToLower()) == 0)))
                {
                    result = true;
                }
            }
            svMain.MirUserLoadCount++;
            return result;
        }

        public static bool SaveHumanCharacter(string uid, string usrname, int certify, FDBRecord rcd)
        {
            bool result = SaveHumanRcd(uid, usrname, certify, rcd);
            svMain.MirUserSaveCount++;
            return result;
        }

        public static void SendNonBlockDatas(string data)
        {
            SendRDBSocket(0, data);
        }

        public static void SendRDBSocket(int certify, string data)
        {
            string cc;
            string str;
            short len;
            int cert;
            string ansidata;
            if (svMain.DBConnected())
            {
                try
                {
                    svMain.csSocLock.Enter();
                    svMain.RDBSocData = "";
                }
                finally
                {
                    svMain.csSocLock.Leave();
                }
                ansidata = data;
                len = (short)(ansidata.Length + 6);
                cert = HUtil32.MakeLong(certify ^ 0xaa, len);
                cc = EDcode.EncodeBuffer(cert, sizeof(int));
                str = "#" + certify.ToString() + "/" + ansidata + cc + "!";
                svMain.ReadyDBReceive = true;
                svMain.FrmMain.DBSocket.Socket.SendText(str);
            }
        }

        public static bool RunDBWaitMsg(int cert, ref int ident, ref int recog, ref string rmsg, long waittime)
        {
            bool result;
            long start;
            int len;
            int v;
            short w1;
            short w2;
            string str;
            string data = string.Empty;
            string certify = string.Empty;
            string cc;
            TDefaultMessage msg= new TDefaultMessage();
            string head;
            string body= string.Empty;
            bool flag;
            int waitcnt;
            int EndPosition;
            flag = false;
            result = false;
            start = GetTickCount;
            waitcnt = 0;
            str = "";
            while (true)
            {
                waitcnt = 1;
                if (GetTickCount - start > waittime)
                {
                    LatestDBError = LatestDBSendMsg;
                    break;
                }
                waitcnt = 2;
                try
                {
                    svMain.csSocLock.Enter();
                    str = str + svMain.RDBSocData;
                    svMain.RDBSocData = "";
                }
                finally
                {
                    svMain.csSocLock.Leave();
                }
                waitcnt = 3;
                EndPosition = str.IndexOf("!");
                if (EndPosition > 0)
                {
                    data = "";
                    str = ArrestStringEx(str, "#", "!", data);
                    waitcnt = 4;
                    if (data != "")
                    {
                        data = GetValidStr3(data, certify, new string[] { "/" });
                        if (HUtil32.Str_ToInt(certify, 0) == 0)
                        {
                            continue;
                        }
                        else
                        {
                            waitcnt = 5;
                            len = data.Length;
                            if ((len >= Grobal2.DEFBLOCKSIZE) && (HUtil32.Str_ToInt(certify, 0) == cert))
                            {
                                w1 = (short)(HUtil32.Str_ToInt(certify, 0) ^ 0xaa);
                                w2 = (short)len;
                                v = HUtil32.MakeLong(w1, w2);
                                cc = EDcode.EncodeBuffer(v, sizeof(int));
                                waitcnt = 6;
                                if (CompareBackLStr(data, cc, cc.Length))
                                {
                                    if (len == Grobal2.DEFBLOCKSIZE)
                                    {
                                        head = data;
                                        body = "";
                                    }
                                    else
                                    {
                                        head = data.Substring(1 - 1, Grobal2.DEFBLOCKSIZE);
                                        body = data.Substring(Grobal2.DEFBLOCKSIZE + 1 - 1, data.Length - Grobal2.DEFBLOCKSIZE - 6);
                                    }
                                    msg = DecodeMessage(head);
                                    ident = msg.Ident;
                                    recog = msg.Recog;
                                    rmsg = body;
                                    flag = true;
                                    result = true;
                                    break;
                                }
                                else
                                {
                                    svMain.RunFailCount++;
                                }
                            }
                            else
                            {
                                waitcnt = 7;
                                svMain.RunFailCount++;
                            }
                        }
                    }
                    waitcnt = 8;
                }
                Thread.CurrentThread.Sleep(0);
            }
            if (!flag)
            {
                svMain.MainOutMessage("[RunDB] DB Wait Error (" + waittime.ToString() + ")" + DateTime.Now.ToString() + ":" + waitcnt.ToString());
            }
            if (GetTickCount - start > svMain.CurrentDBloadingTime)
            {
                svMain.CurrentDBloadingTime = GetTickCount - start;
            }
            svMain.ReadyDBReceive = false;
            return result;
        }

        public static bool LoadHumanRcd(string uid, string uname, string useraddr, int certify, ref FDBRecord rcd)
        {
            bool result;
            int ident;
            int recog;
            int cer;
            string body= string.Empty;
            string str;
            string runame;
            TDefaultMessage Def;
            TLoadHuman lhuman;
            cer = svMain.GetCertifyNumber();
            MakeDefMsg(ref Def, Grobal2.DB_LOADHUMANRCD, 0, 0, 0, 0);
            lhuman.CertifyCode = certify;
            str = EDcodeEncodeMessage(Def) + EDcode.EncodeBuffer(lhuman, sizeof(TLoadHuman));
            LatestDBSendMsg = Grobal2.DB_LOADHUMANRCD;
            SendRDBSocket(cer, str);
            if (RunDBWaitMsg(cer, ref ident, ref recog, ref body, 5000))
            {
                result = false;
                if (ident == Grobal2.DBR_LOADHUMANRCD)
                {
                    if (recog == 1)
                    {
                        body = GetValidStr3(body, str, new string[] { "/" });
                        runame = EDcode.DecodeString(str);
                        if (runame == uname)
                        {
                            DecodeBuffer(body, rcd, sizeof(FDBRecord));
                            result = true;
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
            return result;
        }

        public static bool SaveHumanRcd(string uid, string uname, int certify, FDBRecord rcd)
        {
            int ident;
            int recog;
            string body= string.Empty;
            TDefaultMessage Def;
            int cer = svMain.GetCertifyNumber();
            bool result = false;
            LatestDBSendMsg = Grobal2.DB_SAVEHUMANRCD;
            MakeDefMsg(ref Def, Grobal2.DB_SAVEHUMANRCD, certify, 0, 0, 0);
            SendRDBSocket(cer, EDcodeEncodeMessage(Def) + EDcode.EncodeString(uid) + "/" + EDcode.EncodeString(uname) + "/" + EDcode.EncodeBuffer(rcd, sizeof(FDBRecord)));
            if (RunDBWaitMsg(cer, ref ident, ref recog, ref body, 4999))
            {
                if (ident == Grobal2.DBR_SAVEHUMANRCD)
                {
                    if (recog == 1)
                    {
                        result = true;
                    }
                }
            }
            return result;
        }

        public static void SendCloseUser(string uid, int certify)
        {
            if (svMain.ServerClosing)
            {
                return;
            }
            string body= string.Empty;
            int ident;
            int recog;
            int cer = svMain.GetCertifyNumber();
            string scert = certify.ToString();
            TDefaultMessage Def;
            MakeDefMsg(ref Def, Grobal2.DB_RUNCLOSEUSER, 0, 0, 0, 0);
            LatestDBSendMsg = Grobal2.DB_RUNCLOSEUSER;
            SendRDBSocket(cer, EDcode.EncodeMessage(Def) + EDcode.EncodeString(uid + "/" + scert));
            RunDBWaitMsg(cer, ref ident, ref recog, ref body, 2000);
        }

        public static void SendChangeServer(string uid, string chrname, int certify)
        {
            string body= string.Empty;
            int ident;
            int recog;
            int cer;
            TDefaultMessage Def;
            cer = svMain.GetCertifyNumber();
            MakeDefMsg(ref Def, Grobal2.DB_CHANGESERVER, certify, 0, 0, 0);
            LatestDBSendMsg = Grobal2.DB_CHANGESERVER;
            SendRDBSocket(cer, EDcode.EncodeMessage(Def) + EDcode.EncodeString(uid + "/" + chrname));
            RunDBWaitMsg(cer, ref ident, ref recog, ref body, 2000);
        }
    }
}
