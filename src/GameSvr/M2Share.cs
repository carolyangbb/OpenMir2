using System;
using System.Collections;
using SystemModule;

namespace GameSvr
{
    public struct TMonInfo
    {
        public string Name;
        public int Race;
    }

    public struct TSaveRcd
    {
        public string uid;
        public string uname;
        public int certify;
        public int savefail;
        public long savetime;
        public TUserHuman hum;
        public FDBRecord rcd;
    }

    public struct TReadyUserInfo
    {
        public string UserId;
        public string UserName;
        public string UserAddress;
        public bool StartNew;
        public int Certification;
        public int ApprovalMode;
        public int AvailableMode;
        public int ClientVersion;
        public int LoginClientVersion;
        public int ClientCheckSum;
        public int Shandle;
        public ushort UserGateIndex;
        public int GateIndex;
        public long ReadyStartTime;
        public bool Closed;
    }

    public struct TChangeUserInfo
    {
        public string CommandWho;
        public string UserName;
        public int ChangeGold;
    }

    public struct TUserOpenInfo
    {
        public string Name;
        public FDBRecord rcd;
        public TReadyUserInfo readyinfo;
    }

    public struct THolySeizeInfo
    {
        // 搬拌
        public Object[] earr;
        public ArrayList seizelist;
        public long OpenTime;
        public long SeizeTime;
    }
}

namespace GameSvr
{
    public class M2Share
    {
        public static string GateClass = "Config";
        public static TGameConfig g_GameConfig = new TGameConfig() { true, true, true, false, true, true, true, false, false, true };

        public static string g_sStartMarryManMsg = "[%n]: %s 与 %d 的婚礼现在开始..";
        public static string g_sStartMarryWoManMsg = "[%n]: %d 与 %s 的婚礼现在开始..";
        public static string g_sStartMarryManAskQuestionMsg = "[%n]: %s 你愿意娶 %d 小姐为妻，并照顾她一生一世吗？";
        public static string g_sStartMarryWoManAskQuestionMsg = "[%n]: %d 你愿意娶 %s 小姐为妻，并照顾她一生一世吗？";
        public static string g_sMarryManAnswerQuestionMsg = "[%s]: 我愿意，%d 小姐我会尽我一生的时间来照顾您，让您过上快乐美满的日子的";
        public static string g_sMarryManAskQuestionMsg = "[%n]: %d 你愿意嫁给 %s 先生为妻，并照顾他一生一世吗？";
        public static string g_sMarryWoManAnswerQuestionMsg = "[%s]: 我愿意，%d 先生我愿意让你来照顾我，保护我";
        public static string g_sMarryWoManGetMarryMsg = "[%n]: 我宣布 %d 先生与 %s 小姐正式成为合法夫妻";
        public static string g_sMarryWoManDenyMsg = "[%s]: %d 你这个好色之徒，谁会愿意嫁给你呀，癞蛤蟆想吃天鹅肉";
        public static string g_sMarryWoManCancelMsg = "[%n]: 真是可惜，二个人这个时候才翻脸，你们培养好感情后再来找我吧";
        public static string g_sfUnMarryManLoginMsg = "你的老婆%d已经强行与你脱离了夫妻关系了";
        public static string g_sfUnMarryWoManLoginMsg = "你的老公%d已经强行与你脱离了夫妻关系了";
        public static string g_sManLoginDearOnlineSelfMsg = "你的老婆%d当前位于%m(%x:%y)";
        public static string g_sManLoginDearOnlineDearMsg = "你的老公%s在:%m(%x:%y)上线了";
        public static string g_sWoManLoginDearOnlineSelfMsg = "你的老公当前位于%m(%x:%y)";
        public static string g_sWoManLoginDearOnlineDearMsg = "你的老婆%s在:%m(%x:%y) 上线了";
        public static string g_sManLoginDearNotOnlineMsg = "你的老婆现在不在线";
        public static string g_sWoManLoginDearNotOnlineMsg = "你的老公现在不在线";
        public static string g_sManLongOutDearOnlineMsg = "你的老公在:%m(%x:%y)下线了";
        public static string g_sWoManLongOutDearOnlineMsg = "你的老婆在:%m(%x:%y)下线了";
        public static string g_sYouAreNotMarryedMsg = "你都没结婚查什么？";
        public static string g_sYourWifeNotOnlineMsg = "你的老婆还没有上线";
        public static string g_sYourHusbandNotOnlineMsg = "你的老公还没有上线";
        public static string g_sYourWifeNowLocateMsg = "你的老婆现在位于:";
        public static string g_sYourHusbandSearchLocateMsg = "你的老公正在找你，他现在位于:";
        public static string g_sYourHusbandNowLocateMsg = "你的老公现在位于:";
        public static string g_sYourWifeSearchLocateMsg = "你的老婆正在找你，他现在位于:";
        public static string g_sfUnMasterLoginMsg = "你的一个徒弟已经背判师门了";
        public static string g_sfUnMasterListLoginMsg = "你的师父%d已经将你逐出师门了";
        public static string g_sMasterListOnlineSelfMsg = "你的师父%d当前位于%m(%x:%y)";
        public static string g_sMasterListOnlineMasterMsg = "你的徒弟%s在:%m(%x:%y)上线了";
        public static string g_sMasterOnlineSelfMsg = "你的徒弟当前位于%m(%x:%y)";
        public static string g_sMasterOnlineMasterListMsg = "你的师父%s在:%m(%x:%y) 上线了";
        public static string g_sMasterLongOutMasterListOnlineMsg = "你的师父在:%m(%x:%y)下线了";
        public static string g_sMasterListLongOutMasterOnlineMsg = "你的徒弟%s在:%m(%x:%y)下线了";
        public static string g_sMasterListNotOnlineMsg = "你的师父现不在线";
        public static string g_sMasterNotOnlineMsg = "你的徒弟现不在线";
        public static string g_sYouAreNotMasterMsg = "你都没师徒关系查什么？";
        public static string g_sYourMasterNotOnlineMsg = "你的师父还没有上线";
        public static string g_sYourMasterListNotOnlineMsg = "你的徒弟还没有上线";
        public static string g_sYourMasterNowLocateMsg = "你的师父现在位于:";
        public static string g_sYourMasterListSearchLocateMsg = "你的徒弟正在找你，他现在位于:";
        public static string g_sYourMasterListNowLocateMsg = "你的徒弟现在位于:";
        public static string g_sYourMasterSearchLocateMsg = "你的师父正在找你，他现在位于:";
        public static string g_sYourMasterListUnMasterOKMsg = "你的徒弟%d已经圆满出师了";
        public static string g_sYouAreUnMasterOKMsg = "你已经出师了";
        public static string g_sUnMasterLoginMsg = "你的一个徒弟已经圆满出师了";
        public static string g_sNPCSayUnMasterOKMsg = "[%n]: 我宣布%d与%s正式脱离师徒关系";
        public static string g_sNPCSayForceUnMasterMsg = "[%n]: 我宣布%s与%d已经正式脱离师徒关系";
        public static bool boSecondCardSystem = false;
        public static int g_nExpErienceLevel = 7;
        public static string BADMANHOMEMAP = "3";
        public static int BADMANSTARTX = 845;
        public static int BADMANSTARTY = 674;
        public static string RECHARGINGMAP = "kaiqu";
        // 充值地图名称
        public static bool boSafeZoneStall = false;
        public const int MAXKINGLEVEL = 61;
        public const int MAXLEVEL = 101;
        public static long[] NEEDEXPS = { 100, 200, 300, 400, 600, 900, 1200, 1700, 2500, 6000, 8000, 10000, 15000, 30000, 40000, 50000, 70000, 100000, 120000, 140000, 250000, 300000, 350000, 400000, 500000, 700000, 1000000, 1400000, 1800000, 2000000, 2400000, 2800000, 3200000, 3600000, 4000000, 4800000, 5600000, 8200000, 9000000, 12000000, 16000000, 30000000, 50000000, 80000000, 120000000, 160000000, 200000000, 250000000, 300000000, 350000000, 400000000, 480000000, 560000000, 640000000, 740000000, 840000000, 950000000, 1070000000, 1200000000, 1500000000, 1500000000, 1600000000, 1600000000, 1700000000, 1700000000, 1800000000, 1800000000, 1900000000, 1900000000, 2000000000, 2000000000, 2000000000, 2000000000, 2000000000, 2000000000, 2000000000, 2000000000, 2000000000, 2000000000, 2000000000, 2000000000, 2000000000, 2000000000, 2000000000, 2000000000, 2000000000, 2000000000, 2000000000, 2000000000, 2000000000, 2100000000, 2100000000, 2100000000, 2100000000, 2100000000, 2100000000, 2100000000, 2100000000, 2100000000, 2100000000, 2140000000 };
        public const int ADJ_LEVEL = 20;
        public static TNakedAbility WarriorBonus = new TNakedAbility() { DC = 17, MC = 20, SC = 20, AC = 20, MAC = 20, HP = 1, MP = 3, Hit = 20, Speed = 35 };
        public static TNakedAbility WizzardBonus = new TNakedAbility() { DC = 17, MC = 25, SC = 30, AC = 20, MAC = 15, HP = 2, MP = 1, Hit = 25, Speed = 35 };
        public static TNakedAbility PriestBonus = new TNakedAbility() { DC = 20, MC = 30, SC = 17, AC = 20, MAC = 15, HP = 2, MP = 1, Hit = 30, Speed = 30 };
        public static byte[,,] SpitMap = { { { 0, 0, 1, 0, 0 }, { 0, 0, 1, 0, 0 }, { 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0 } }, { { 0, 0, 0, 0, 1 }, { 0, 0, 0, 1, 0 }, { 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0 } }, { { 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0 }, { 0, 0, 0, 1, 1 }, { 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0 } }, { { 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0 }, { 0, 0, 0, 1, 0 }, { 0, 0, 0, 0, 1 } }, { { 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0 }, { 0, 0, 1, 0, 0 }, { 0, 0, 1, 0, 0 } }, { { 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0 }, { 0, 1, 0, 0, 0 }, { 1, 0, 0, 0, 0 } }, { { 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0 }, { 1, 1, 0, 0, 0 }, { 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0 } }, { { 1, 0, 0, 0, 0 }, { 0, 1, 0, 0, 0 }, { 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0 } } };
        public static byte[,,] CrossMap = { { { 0, 1, 1, 1, 0 }, { 0, 0, 1, 0, 0 }, { 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0 } }, { { 0, 0, 0, 1, 1 }, { 0, 0, 0, 1, 1 }, { 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0 } }, { { 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 1 }, { 0, 0, 0, 1, 1 }, { 0, 0, 0, 0, 1 }, { 0, 0, 0, 0, 0 } }, { { 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0 }, { 0, 0, 0, 1, 1 }, { 0, 0, 0, 1, 1 } }, { { 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0 }, { 0, 0, 1, 0, 0 }, { 0, 1, 1, 1, 0 } }, { { 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0 }, { 1, 1, 0, 0, 0 }, { 1, 1, 0, 0, 0 } }, { { 0, 0, 0, 0, 0 }, { 1, 0, 0, 0, 0 }, { 1, 1, 0, 0, 0 }, { 1, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0 } }, { { 1, 1, 0, 0, 0 }, { 1, 1, 0, 0, 0 }, { 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0 } } };
        
        public int GetBonusPoint_adjlowlv(int lv)
        {
            int result;
            if (lv <= 25)
            {
                result = HUtil32.MathRound(26 - lv);
            }
            else
            {
                result = 0;
            }
            return result;
        }

        public static int GetBonusPoint(int job, int lv)
        {
            int result;
            result = 0;
            switch (job)
            {
                case 0:
                    if (lv >= ADJ_LEVEL + 1)
                    {
                        // Result := Round(5 + adjlowlv(lv) + lv * 0.7)
                        result = HUtil32.MathRound(20 + lv / 10 * 5);
                    }
                    else
                    {
                        result = 0;
                    }
                    break;
                case 1:
                    if (lv >= ADJ_LEVEL + 1)
                    {
                        // Result := Round(8 + adjlowlv(lv) + lv * 1.2)
                        result = HUtil32.MathRound(27 + lv / 10 * 8);
                    }
                    else
                    {
                        result = 0;
                    }
                    break;
                case 2:
                    if (lv >= ADJ_LEVEL + 1)
                    {
                        // Result := Round(8 + adjlowlv(lv) + lv * 1.4)
                        result = HUtil32.MathRound(28 + lv / 10 * 9);
                    }
                    else
                    {
                        result = 0;
                    }
                    break;
            }
            return result;
        }

        public static int GetLevelBonusSum(int job, int lv)
        {
            int result;
            int i;
            result = 0;
            for (i = 2; i <= lv; i++)
            {
                result = result + GetBonusPoint(job, i);
            }
            return result;
        }

        public static int GetBack(int dir)
        {
            int result;
            result = Grobal2.DR_UP;
            switch (dir)
            {
                case Grobal2.DR_UP:
                    result = Grobal2.DR_DOWN;
                    break;
                case Grobal2.DR_DOWN:
                    result = Grobal2.DR_UP;
                    break;
                case Grobal2.DR_LEFT:
                    result = Grobal2.DR_RIGHT;
                    break;
                case Grobal2.DR_RIGHT:
                    result = Grobal2.DR_LEFT;
                    break;
                case Grobal2.DR_UPLEFT:
                    result = Grobal2.DR_DOWNRIGHT;
                    break;
                case Grobal2.DR_UPRIGHT:
                    result = Grobal2.DR_DOWNLEFT;
                    break;
                case Grobal2.DR_DOWNLEFT:
                    result = Grobal2.DR_UPRIGHT;
                    break;
                case Grobal2.DR_DOWNRIGHT:
                    result = Grobal2.DR_UPLEFT;
                    break;
            }
            return result;
        }

        public static bool GetFrontPosition(TCreature cret, ref short newx, ref short newy)
        {
            bool result;
            TEnvirnoment penv;
            penv = cret.PEnvir;
            newx = cret.CX;
            newy = cret.CY;
            switch (cret.Dir)
            {
                case Grobal2.DR_UP:
                    if (newy > 0)
                    {
                        newy = (short)(newy - 1);
                    }
                    break;
                case Grobal2.DR_DOWN:
                    if (newy < penv.MapHeight - 1)
                    {
                        newy = (short)(newy + 1);
                    }
                    break;
                case Grobal2.DR_LEFT:
                    if (newx > 0)
                    {
                        newx = (short)(newx - 1);
                    }
                    break;
                case Grobal2.DR_RIGHT:
                    if (newx < penv.MapWidth - 1)
                    {
                        newx = (short)(newx + 1);
                    }
                    break;
                case Grobal2.DR_UPLEFT:
                    if ((newx > 0) && (newy > 0))
                    {
                        newx = (short)(newx - 1);
                        newy = (short)(newy - 1);
                    }
                    break;
                case Grobal2.DR_UPRIGHT:
                    if ((newx > 0) && (newy < penv.MapHeight - 1))
                    {
                        newx = (short)(newx + 1);
                        newy = (short)(newy - 1);
                    }
                    break;
                case Grobal2.DR_DOWNLEFT:
                    if ((newx < penv.MapWidth - 1) && (newy > 0))
                    {
                        newx = (short)(newx - 1);
                        newy = (short)(newy + 1);
                    }
                    break;
                case Grobal2.DR_DOWNRIGHT:
                    if ((newx < penv.MapWidth - 1) && (newy < penv.MapHeight - 1))
                    {
                        newx = (short)(newx + 1);
                        newy = (short)(newy + 1);
                    }
                    break;
            }
            result = true;
            return result;
        }

        public static bool GetBackPosition(TCreature cret, ref short newx, ref short newy)
        {
            bool result;
            TEnvirnoment penv;
            penv = cret.PEnvir;
            newx = cret.CX;
            newy = cret.CY;
            switch (cret.Dir)
            {
                case Grobal2.DR_UP:
                    if (newy < penv.MapHeight - 1)
                    {
                        newy = (short)(newy + 1);
                    }
                    break;
                case Grobal2.DR_DOWN:
                    if (newy > 0)
                    {
                        newy = (short)(newy - 1);
                    }
                    break;
                case Grobal2.DR_LEFT:
                    if (newx < penv.MapWidth - 1)
                    {
                        newx = (short)(newx + 1);
                    }
                    break;
                case Grobal2.DR_RIGHT:
                    if (newx > 0)
                    {
                        newx = (short)(newx - 1);
                    }
                    break;
                case Grobal2.DR_UPLEFT:
                    if ((newx < penv.MapWidth - 1) && (newy < penv.MapHeight - 1))
                    {
                        newx = (short)(newx + 1);
                        newy = (short)(newy + 1);
                    }
                    break;
                case Grobal2.DR_UPRIGHT:
                    if ((newx < penv.MapWidth - 1) && (newy > 0))
                    {
                        newx = (short)(newx - 1);
                        newy = (short)(newy + 1);
                    }
                    break;
                case Grobal2.DR_DOWNLEFT:
                    if ((newx > 0) && (newy < penv.MapHeight - 1))
                    {
                        newx = (short)(newx + 1);
                        newy = (short)(newy - 1);
                    }
                    break;
                case Grobal2.DR_DOWNRIGHT:
                    if ((newx > 0) && (newy > 0))
                    {
                        newx = (short)(newx - 1);
                        newy = (short)(newy - 1);
                    }
                    break;
            }
            result = true;
            return result;
        }

        public static bool GetNextPosition(TEnvirnoment penv, short sx, short sy, byte dir, int dis, ref short newx, ref short newy)
        {
            bool result;
            newx = sx;
            newy = sy;
            switch (dir)
            {
                case Grobal2.DR_UP:
                    if (newy > (dis - 1))
                    {
                        newy = (short)(newy - dis);
                    }
                    break;
                case Grobal2.DR_DOWN:
                    if (newy < penv.MapHeight - dis)
                    {
                        newy = (short)(newy + dis);
                    }
                    break;
                case Grobal2.DR_LEFT:
                    if (newx > (dis - 1))
                    {
                        newx = (short)(newx - dis);
                    }
                    break;
                case Grobal2.DR_RIGHT:
                    if (newx < penv.MapWidth - dis)
                    {
                        newx = (short)(newx + dis);
                    }
                    break;
                case Grobal2.DR_UPLEFT:
                    if ((newx > dis - 1) && (newy > dis - 1))
                    {
                        newx = (short)(newx - dis);
                        newy = (short)(newy - dis);
                    }
                    break;
                case Grobal2.DR_UPRIGHT:
                    if ((newx > dis - 1) && (newy < penv.MapHeight - dis))
                    {
                        newx = (short)(newx + dis);
                        newy = (short)(newy - dis);
                    }
                    break;
                case Grobal2.DR_DOWNLEFT:
                    if ((newx < penv.MapWidth - dis) && (newy > dis - 1))
                    {
                        newx = (short)(newx - dis);
                        newy = (short)(newy + dis);
                    }
                    break;
                case Grobal2.DR_DOWNRIGHT:
                    if ((newx < penv.MapWidth - dis) && (newy < penv.MapHeight - dis))
                    {
                        newx = (short)(newx + dis);
                        newy = (short)(newy + dis);
                    }
                    break;
            }
            if ((sx == newx) && (sy == newy))
            {
                result = false;
            }
            else
            {
                result = true;
            }
            return result;
        }

        public static byte GetNextDirection(int sx, int sy, int dx, int dy)
        {
            byte result;
            int flagx;
            int flagy;
            result = Grobal2.DR_DOWN;
            if (sx < dx)
            {
                flagx = 1;
            }
            else if (sx == dx)
            {
                flagx = 0;
            }
            else
            {
                flagx = -1;
            }
            if (Math.Abs(sy - dy) > 2)
            {
                if ((sx >= dx - 1) && (sx <= dx + 1))
                {
                    flagx = 0;
                }
            }
            if (sy < dy)
            {
                flagy = 1;
            }
            else if (sy == dy)
            {
                flagy = 0;
            }
            else
            {
                flagy = -1;
            }
            if (Math.Abs(sx - dx) > 2)
            {
                if ((sy > dy - 1) && (sy <= dy + 1))
                {
                    flagy = 0;
                }
            }
            if ((flagx == 0) && (flagy == -1))
            {
                result = Grobal2.DR_UP;
            }
            if ((flagx == 1) && (flagy == -1))
            {
                result = Grobal2.DR_UPRIGHT;
            }
            if ((flagx == 1) && (flagy == 0))
            {
                result = Grobal2.DR_RIGHT;
            }
            if ((flagx == 1) && (flagy == 1))
            {
                result = Grobal2.DR_DOWNRIGHT;
            }
            if ((flagx == 0) && (flagy == 1))
            {
                result = Grobal2.DR_DOWN;
            }
            if ((flagx == -1) && (flagy == 1))
            {
                result = Grobal2.DR_DOWNLEFT;
            }
            if ((flagx == -1) && (flagy == 0))
            {
                result = Grobal2.DR_LEFT;
            }
            if ((flagx == -1) && (flagy == -1))
            {
                result = Grobal2.DR_UPLEFT;
            }
            return result;
        }

        public static byte GetNextDirectionNew(int sx, int sy, int dx, int dy)
        {
            byte result;
            int flagx;
            int flagy;
            result = Grobal2.DR_DOWN;
            if (sx < dx)
            {
                flagx = 1;
            }
            else if (sx == dx)
            {
                flagx = 0;
            }
            else
            {
                flagx = -1;
            }
            if (Math.Abs(sy - dy) > 2)
            {
                if ((sx >= dx - 1) && (sx <= dx + 1))
                {
                    flagx = 0;
                }
            }
            if (sy < dy)
            {
                flagy = 1;
            }
            else if (sy == dy)
            {
                flagy = 0;
            }
            else
            {
                flagy = -1;
            }
            if (Math.Abs(sx - dx) > 2)
            {
                if ((sy >= dy - 1) && (sy <= dy + 1))
                {
                    flagy = 0;
                }
            }
            if ((flagx == 0) && (flagy == -1))
            {
                result = Grobal2.DR_UP;
            }
            if ((flagx == 1) && (flagy == -1))
            {
                result = Grobal2.DR_UPRIGHT;
            }
            if ((flagx == 1) && (flagy == 0))
            {
                result = Grobal2.DR_RIGHT;
            }
            if ((flagx == 1) && (flagy == 1))
            {
                result = Grobal2.DR_DOWNRIGHT;
            }
            if ((flagx == 0) && (flagy == 1))
            {
                result = Grobal2.DR_DOWN;
            }
            if ((flagx == -1) && (flagy == 1))
            {
                result = Grobal2.DR_DOWNLEFT;
            }
            if ((flagx == -1) && (flagy == 0))
            {
                result = Grobal2.DR_LEFT;
            }
            if ((flagx == -1) && (flagy == -1))
            {
                result = Grobal2.DR_UPLEFT;
            }
            return result;
        }

        public static ushort GetHpMpRate(TCreature cret)
        {
            ushort result;
            byte hrate;
            byte srate;
            if ((cret.Abil.MaxHP != 0) && (cret.Abil.MaxMP != 0))
            {
                result = HUtil32.MakeWord(HUtil32.MathRound(cret.Abil.HP / cret.Abil.MaxHP * 100), HUtil32.MathRound(cret.Abil.MP / cret.Abil.MaxMP * 100));
            }
            else
            {
                if (cret.Abil.MaxHP == 0)
                {
                    hrate = 0;
                }
                else
                {
                    hrate = (byte)HUtil32.MathRound(cret.Abil.HP / cret.Abil.MaxHP * 100);
                }
                if (cret.Abil.MaxMP == 0)
                {
                    srate = 0;
                }
                else
                {
                    srate = (byte)HUtil32.MathRound(cret.Abil.MP / cret.Abil.MaxMP * 100);
                }
                result = HUtil32.MakeWord(hrate, srate);
            }
            return result;
        }

        public static bool IsTakeOnAvailable(int useindex, TStdItem pstd)
        {
            bool result;
            result = false;
            if (pstd == null)
            {
                return result;
            }
            switch (useindex)
            {
                case Grobal2.U_DRESS:
                    if (pstd.StdMode >= 10 && pstd.StdMode <= 11)
                    {
                        // 巢磊 咯磊渴..
                        result = true;
                    }
                    break;
                case Grobal2.U_WEAPON:
                    if ((pstd.StdMode == 5) || (pstd.StdMode == 6))
                    {
                        result = true;
                    }
                    break;
                case Grobal2.U_RIGHTHAND:
                    if (pstd.StdMode == 30)
                    {
                        // 眯阂, 颇沸伐橇
                        result = true;
                    }
                    break;
                case Grobal2.U_NECKLACE:
                    if ((pstd.StdMode == 19) || (pstd.StdMode == 20) || (pstd.StdMode == 21))
                    {
                        result = true;
                    }
                    break;
                case Grobal2.U_HELMET:
                    if (pstd.StdMode == 15)
                    {
                        result = true;
                    }
                    break;
                case Grobal2.U_RINGL:
                case Grobal2.U_RINGR:
                    if ((pstd.StdMode == 22) || (pstd.StdMode == 23))
                    {
                        result = true;
                    }
                    break;
                case Grobal2.U_ARMRINGR:
                    // 迫骂父..
                    if ((pstd.StdMode == 24) || (pstd.StdMode == 26))
                    {
                        result = true;
                    }
                    break;
                case Grobal2.U_ARMRINGL:
                    // 迫骂, 何利/刀啊风..
                    if ((pstd.StdMode == 24) || (pstd.StdMode == 25) || (pstd.StdMode == 26))
                    {
                        result = true;
                    }
                    break;
                case Grobal2.U_BUJUK:
                    // 2003/03/15 酒捞袍 牢亥配府 犬厘
                    if (pstd.StdMode == 25)
                    {
                        result = true;
                    }
                    break;
                case Grobal2.U_BELT:
                    if (pstd.StdMode == 54)
                    {
                        result = true;
                    }
                    break;
                case Grobal2.U_BOOTS:
                    if (pstd.StdMode == 52)
                    {
                        result = true;
                    }
                    break;
                case Grobal2.U_CHARM:
                    if (pstd.StdMode == 53)
                    {
                        result = true;
                    }
                    break;
            }
            return result;
        }

        public static bool IsDCItem(int uindex)
        {
            bool result;
            TStdItem pstd;
            pstd = svMain.UserEngine.GetStdItem(uindex);
            if (new ArrayList(new int[] { 5, 6, 10, 11, 15, 19, 20, 21, 22, 23, 24, 26, 52, 53, 54 }).Contains(pstd.StdMode))
            {
                result = true;
            }
            else
            {
                result = false;
            }
            return result;
        }

        public static bool IsUpgradeWeaponStuff(int uindex)
        {
            bool result;
            TStdItem pstd;
            pstd = svMain.UserEngine.GetStdItem(uindex);
            // ,52,53,54 sonmg
            if (new ArrayList(new int[] { 19, 20, 21, 22, 23, 24, 26 }).Contains(pstd.StdMode))
            {
                result = true;
            }
            else
            {
                result = false;
            }
            return result;
        }

        public static ArrayList GetMakeItemCondition(string itemname, ref int iPrice)
        {
            string sMakeItemName = string.Empty;
            string sMakeItemPrice = string.Empty;
            ArrayList result = null;
            for (var i = 0; i < svMain.MakeItemList.Count; i++)
            {
                sMakeItemPrice = HUtil32.GetValidStr3(svMain.MakeItemList[i], ref sMakeItemName, new string[] { ":" });
                if (sMakeItemName == itemname)
                {
                    result = svMain.MakeItemList.Values[i] as ArrayList;
                    iPrice = HUtil32.Str_ToInt(sMakeItemPrice, 0);
                    break;
                }
            }
            return result;
        }

        public static byte GetTurnDir(int dir, int rotatecount)
        {
            byte result;
            result = (byte)((dir + rotatecount) % 8);
            return result;
        }

        public static bool IsCheapStuff(int stdmode)
        {
            bool result;
            if (stdmode >= 0 && stdmode <= 2)
            {
                result = true;
            }
            else
            {
                result = false;
            }
            return result;
        }

        public static string GetGoldStr(int gold)
        {
            string result;
            int i;
            int n;
            string str;
            str = gold.ToString();
            n = 0;
            result = "";
            for (i = str.Length; i >= 1; i--)
            {
                if (n == 3)
                {
                    result = str[i] + "," + result;
                    n = 1;
                }
                else
                {
                    result = str[i] + result;
                    n++;
                }
            }
            return result;
        }

        // 捣俊 霓付 嘿捞绰 窃荐.
        public static string GetStrGoldStr(string strgold)
        {
            string result;
            int i;
            int n;
            // str: string;
            // str := IntToStr (gold);
            n = 0;
            result = "";
            for (i = strgold.Length; i >= 1; i--)
            {
                if (n == 3)
                {
                    result = strgold[i] + "," + result;
                    n = 1;
                }
                else
                {
                    result = strgold[i] + result;
                    n++;
                }
            }
            return result;
        }

        // 捣俊 霓付 嘿捞绰 窃荐.
        public static void LoadConfig()
        {
            //FileStream Conf;
            //string sConfigFileName;
            //int nInteger;
            //string sString;
            //sConfigFileName = ".\\Config.ini";
            //Conf = new FileStream(sConfigFileName);
            //nInteger = Conf.ReadInteger(GateClass, "WhisperRecord", -1);// 游戏私聊
            //if (nInteger == -1)
            //{
            //    Conf.WriteBool(GateClass, "WhisperRecord", g_GameConfig.boWhisperRecord);
            //}
            //nInteger = Conf.ReadInteger(GateClass, "NoFog", -1);
            //if (nInteger == -1)
            //{
            //    Conf.WriteBool(GateClass, "NoFog", g_GameConfig.boNoFog);
            //}
            //nInteger = Conf.ReadInteger(GateClass, "StallSystem", -1);
            //if (nInteger == -1)
            //{
            //    Conf.WriteBool(GateClass, "StallSystem", g_GameConfig.boStallSystem);
            //}
            //nInteger = Conf.ReadInteger(GateClass, "SafeZoneStall", -1);
            //if (nInteger == -1)
            //{
            //    Conf.WriteBool(GateClass, "SafeZoneStall", boSafeZoneStall);
            //}
            //nInteger = Conf.ReadInteger(GateClass, "ShowHpBar", -1);
            //if (nInteger == -1)
            //{
            //    Conf.WriteBool(GateClass, "ShowHpBar", g_GameConfig.boShowHpBar);
            //}
            //nInteger = Conf.ReadInteger(GateClass, "ShowHpNumber", -1);
            //if (nInteger == -1)
            //{
            //    Conf.WriteBool(GateClass, "ShowHpNumber", g_GameConfig.boShowHpNumber);
            //}
            //nInteger = Conf.ReadInteger(GateClass, "NoStruck", -1);
            //if (nInteger == -1)
            //{
            //    Conf.WriteBool(GateClass, "NoStruck", g_GameConfig.boNoStruck);
            //}
            //nInteger = Conf.ReadInteger(GateClass, "FastMove", -1);
            //if (nInteger == -1)
            //{
            //    Conf.WriteBool(GateClass, "FastMove", g_GameConfig.boFastMove);
            //}
            //nInteger = Conf.ReadInteger(GateClass, "ShowFriend", -1);
            //if (nInteger == -1)
            //{
            //    Conf.WriteBool(GateClass, "ShowFriend", g_GameConfig.boShowFriend);
            //}
            //nInteger = Conf.ReadInteger(GateClass, "ShowRelationship", -1);
            //if (nInteger == -1)
            //{
            //    Conf.WriteBool(GateClass, "ShowRelationship", g_GameConfig.boShowRelationship);
            //}
            //nInteger = Conf.ReadInteger(GateClass, "ShowMail", -1);
            //if (nInteger == -1)
            //{
            //    Conf.WriteBool(GateClass, "ShowMail", g_GameConfig.boShowMail);
            //}
            //nInteger = Conf.ReadInteger(GateClass, "ShowRecharging", -1);
            //if (nInteger == -1)
            //{
            //    Conf.WriteBool(GateClass, "ShowRecharging", g_GameConfig.boShowRecharging);
            //}
            //nInteger = Conf.ReadInteger(GateClass, "ShowHelp", -1);
            //if (nInteger == -1)
            //{
            //    Conf.WriteBool(GateClass, "ShowHelp", g_GameConfig.boShowHelp);
            //}
            //nInteger = Conf.ReadInteger(GateClass, "SecondCardSystem", -1);
            //if (nInteger == -1)
            //{
            //    Conf.WriteBool(GateClass, "SecondCardSystem", boSecondCardSystem);
            //}
            //nInteger = Conf.ReadInteger(GateClass, "ExpErienceLevel", -1);
            //if (nInteger == -1)
            //{
            //    Conf.WriteInteger(GateClass, "ExpErienceLevel", g_nExpErienceLevel);
            //}
            //sString = Conf.ReadString(GateClass, "BadManHomeMap", "");
            //if (sString == "")
            //{
            //    Conf.WriteString(GateClass, "BadManHomeMap", "3");
            //}
            //nInteger = Conf.ReadInteger(GateClass, "BadManStartX", -1);
            //if (nInteger == -1)
            //{
            //    Conf.WriteInteger(GateClass, "BadManStartX", BADMANSTARTX);
            //}
            //nInteger = Conf.ReadInteger(GateClass, "BadManStartY", -1);
            //if (nInteger == -1)
            //{
            //    Conf.WriteInteger(GateClass, "BadManStartY", BADMANSTARTY);
            //}
            //sString = Conf.ReadString(GateClass, "RECHARGINGMAP", "");
            //// 充值地图
            //if (sString == "")
            //{
            //    Conf.WriteString(GateClass, "RECHARGINGMAP", "kaiqu");
            //}
            //g_GameConfig.boWhisperRecord = Conf.ReadBool(GateClass, "WhisperRecord", g_GameConfig.boWhisperRecord);
            //// 游戏私聊
            //g_GameConfig.boNoFog = Conf.ReadBool(GateClass, "NoFog", g_GameConfig.boNoFog);
            //g_GameConfig.boStallSystem = Conf.ReadBool(GateClass, "StallSystem", g_GameConfig.boStallSystem);
            //boSafeZoneStall = Conf.ReadBool(GateClass, "SafeZoneStall", boSafeZoneStall);
            //g_GameConfig.boShowHpBar = Conf.ReadBool(GateClass, "ShowHpBar", g_GameConfig.boShowHpBar);
            //g_GameConfig.boShowHpNumber = Conf.ReadBool(GateClass, "ShowHpNumber", g_GameConfig.boShowHpNumber);
            //g_GameConfig.boNoStruck = Conf.ReadBool(GateClass, "NoStruck", g_GameConfig.boNoStruck);
            //g_GameConfig.boFastMove = Conf.ReadBool(GateClass, "FastMove", g_GameConfig.boFastMove);
            //g_GameConfig.boShowFriend = Conf.ReadBool(GateClass, "ShowFriend", g_GameConfig.boShowFriend);
            //g_GameConfig.boShowRelationship = Conf.ReadBool(GateClass, "ShowRelationship", g_GameConfig.boShowRelationship);
            //g_GameConfig.boShowMail = Conf.ReadBool(GateClass, "ShowMail", g_GameConfig.boShowMail);
            //g_GameConfig.boShowRecharging = Conf.ReadBool(GateClass, "ShowRecharging", g_GameConfig.boShowRecharging);
            //g_GameConfig.boShowHelp = Conf.ReadBool(GateClass, "ShowHelp", g_GameConfig.boShowHelp);
            //boSecondCardSystem = Conf.ReadBool(GateClass, "SecondCardSystem", boSecondCardSystem);
            //g_nExpErienceLevel = Conf.ReadInteger(GateClass, "ExpErienceLevel", g_nExpErienceLevel);
            //BADMANHOMEMAP = Conf.ReadString(GateClass, "BadManHomeMap", BADMANHOMEMAP);
            //BADMANSTARTX = Conf.ReadInteger(GateClass, "BadManStartX", BADMANSTARTX);
            //BADMANSTARTY = Conf.ReadInteger(GateClass, "BadManStartY", BADMANSTARTY);
            //g_GameConfig.boGamepath = Conf.ReadBool(GateClass, "Gamepath", g_GameConfig.boGamepath);
            //RECHARGINGMAP = Conf.ReadString(GateClass, "RECHARGINGMAP", RECHARGINGMAP);// 充值
            //Conf.Free();
        }

        public static void SaveConfig()
        {
            //FileStream Conf;
            //string sConfigFileName;
            //sConfigFileName = ".\\Config.ini";
            //Conf = new FileStream(sConfigFileName);
            //Conf.WriteBool(GateClass, "WhisperRecord", g_GameConfig.boWhisperRecord);
            //// 游戏私聊
            //Conf.WriteBool(GateClass, "NoFog", g_GameConfig.boNoFog);
            //Conf.WriteBool(GateClass, "StallSystem", g_GameConfig.boStallSystem);
            //Conf.WriteBool(GateClass, "SafeZoneStall", boSafeZoneStall);
            //Conf.WriteBool(GateClass, "ShowHpBar", g_GameConfig.boShowHpBar);
            //Conf.WriteBool(GateClass, "ShowHpNumber", g_GameConfig.boShowHpNumber);
            //Conf.WriteBool(GateClass, "NoStruck", g_GameConfig.boNoStruck);
            //Conf.WriteBool(GateClass, "FastMove", g_GameConfig.boFastMove);
            //Conf.WriteBool(GateClass, "ShowFriend", g_GameConfig.boShowFriend);
            //Conf.WriteBool(GateClass, "ShowRelationship", g_GameConfig.boShowRelationship);
            //Conf.WriteBool(GateClass, "ShowMail", g_GameConfig.boShowMail);
            //Conf.WriteBool(GateClass, "ShowRecharging", g_GameConfig.boShowRecharging);
            //Conf.WriteBool(GateClass, "ShowHelp", g_GameConfig.boShowHelp);
            //Conf.WriteBool(GateClass, "SecondCardSystem", boSecondCardSystem);
            //Conf.WriteInteger(GateClass, "ExpErienceLevel", g_nExpErienceLevel);
            //Conf.WriteString(GateClass, "BadManHomeMap", BADMANHOMEMAP);
            //Conf.WriteInteger(GateClass, "BadManStartX", BADMANSTARTX);
            //Conf.WriteInteger(GateClass, "BadManStartY", BADMANSTARTY);
            //Conf.WriteBool(GateClass, "Gamepath", g_GameConfig.boGamepath);
            //Conf.WriteString(GateClass, "RECHARGINGMAP", RECHARGINGMAP);// 充值地图
            //Conf.Free();
        }
    }
}

