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
        // ���
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

        public static string g_sStartMarryManMsg = "[%n]: %s �� %d �Ļ������ڿ�ʼ..";
        public static string g_sStartMarryWoManMsg = "[%n]: %d �� %s �Ļ������ڿ�ʼ..";
        public static string g_sStartMarryManAskQuestionMsg = "[%n]: %s ��Ը��Ȣ %d С��Ϊ�ޣ����չ���һ��һ����";
        public static string g_sStartMarryWoManAskQuestionMsg = "[%n]: %d ��Ը��Ȣ %s С��Ϊ�ޣ����չ���һ��һ����";
        public static string g_sMarryManAnswerQuestionMsg = "[%s]: ��Ը�⣬%d С���һᾡ��һ����ʱ�����չ������������Ͽ������������ӵ�";
        public static string g_sMarryManAskQuestionMsg = "[%n]: %d ��Ը��޸� %s ����Ϊ�ޣ����չ���һ��һ����";
        public static string g_sMarryWoManAnswerQuestionMsg = "[%s]: ��Ը�⣬%d ������Ը���������չ��ң�������";
        public static string g_sMarryWoManGetMarryMsg = "[%n]: ������ %d ������ %s С����ʽ��Ϊ�Ϸ�����";
        public static string g_sMarryWoManDenyMsg = "[%s]: %d �������ɫ֮ͽ��˭��Ը��޸���ѽ�������������";
        public static string g_sMarryWoManCancelMsg = "[%n]: ���ǿ�ϧ�����������ʱ��ŷ��������������ø�����������Ұ�";
        public static string g_sfUnMarryManLoginMsg = "�������%d�Ѿ�ǿ�����������˷��޹�ϵ��";
        public static string g_sfUnMarryWoManLoginMsg = "����Ϲ�%d�Ѿ�ǿ�����������˷��޹�ϵ��";
        public static string g_sManLoginDearOnlineSelfMsg = "�������%d��ǰλ��%m(%x:%y)";
        public static string g_sManLoginDearOnlineDearMsg = "����Ϲ�%s��:%m(%x:%y)������";
        public static string g_sWoManLoginDearOnlineSelfMsg = "����Ϲ���ǰλ��%m(%x:%y)";
        public static string g_sWoManLoginDearOnlineDearMsg = "�������%s��:%m(%x:%y) ������";
        public static string g_sManLoginDearNotOnlineMsg = "����������ڲ�����";
        public static string g_sWoManLoginDearNotOnlineMsg = "����Ϲ����ڲ�����";
        public static string g_sManLongOutDearOnlineMsg = "����Ϲ���:%m(%x:%y)������";
        public static string g_sWoManLongOutDearOnlineMsg = "���������:%m(%x:%y)������";
        public static string g_sYouAreNotMarryedMsg = "�㶼û����ʲô��";
        public static string g_sYourWifeNotOnlineMsg = "������Ż�û������";
        public static string g_sYourHusbandNotOnlineMsg = "����Ϲ���û������";
        public static string g_sYourWifeNowLocateMsg = "�����������λ��:";
        public static string g_sYourHusbandSearchLocateMsg = "����Ϲ��������㣬������λ��:";
        public static string g_sYourHusbandNowLocateMsg = "����Ϲ�����λ��:";
        public static string g_sYourWifeSearchLocateMsg = "��������������㣬������λ��:";
        public static string g_sfUnMasterLoginMsg = "���һ��ͽ���Ѿ�����ʦ����";
        public static string g_sfUnMasterListLoginMsg = "���ʦ��%d�Ѿ��������ʦ����";
        public static string g_sMasterListOnlineSelfMsg = "���ʦ��%d��ǰλ��%m(%x:%y)";
        public static string g_sMasterListOnlineMasterMsg = "���ͽ��%s��:%m(%x:%y)������";
        public static string g_sMasterOnlineSelfMsg = "���ͽ�ܵ�ǰλ��%m(%x:%y)";
        public static string g_sMasterOnlineMasterListMsg = "���ʦ��%s��:%m(%x:%y) ������";
        public static string g_sMasterLongOutMasterListOnlineMsg = "���ʦ����:%m(%x:%y)������";
        public static string g_sMasterListLongOutMasterOnlineMsg = "���ͽ��%s��:%m(%x:%y)������";
        public static string g_sMasterListNotOnlineMsg = "���ʦ���ֲ�����";
        public static string g_sMasterNotOnlineMsg = "���ͽ���ֲ�����";
        public static string g_sYouAreNotMasterMsg = "�㶼ûʦͽ��ϵ��ʲô��";
        public static string g_sYourMasterNotOnlineMsg = "���ʦ����û������";
        public static string g_sYourMasterListNotOnlineMsg = "���ͽ�ܻ�û������";
        public static string g_sYourMasterNowLocateMsg = "���ʦ������λ��:";
        public static string g_sYourMasterListSearchLocateMsg = "���ͽ���������㣬������λ��:";
        public static string g_sYourMasterListNowLocateMsg = "���ͽ������λ��:";
        public static string g_sYourMasterSearchLocateMsg = "���ʦ���������㣬������λ��:";
        public static string g_sYourMasterListUnMasterOKMsg = "���ͽ��%d�Ѿ�Բ����ʦ��";
        public static string g_sYouAreUnMasterOKMsg = "���Ѿ���ʦ��";
        public static string g_sUnMasterLoginMsg = "���һ��ͽ���Ѿ�Բ����ʦ��";
        public static string g_sNPCSayUnMasterOKMsg = "[%n]: ������%d��%s��ʽ����ʦͽ��ϵ";
        public static string g_sNPCSayForceUnMasterMsg = "[%n]: ������%s��%d�Ѿ���ʽ����ʦͽ��ϵ";
        public static bool boSecondCardSystem = false;
        public static int g_nExpErienceLevel = 7;
        public static string BADMANHOMEMAP = "3";
        public static int BADMANSTARTX = 845;
        public static int BADMANSTARTY = 674;
        public static string RECHARGINGMAP = "kaiqu";
        // ��ֵ��ͼ����
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
                        // ���� ���ڿ�..
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
                        // �к�, �ķз���
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
                    // ���..
                    if ((pstd.StdMode == 24) || (pstd.StdMode == 26))
                    {
                        result = true;
                    }
                    break;
                case Grobal2.U_ARMRINGL:
                    // ����, ����/������..
                    if ((pstd.StdMode == 24) || (pstd.StdMode == 25) || (pstd.StdMode == 26))
                    {
                        result = true;
                    }
                    break;
                case Grobal2.U_BUJUK:
                    // 2003/03/15 ������ �κ��丮 Ȯ��
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

        // ���� �޸� ���̴� �Լ�.
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

        // ���� �޸� ���̴� �Լ�.
        public static void LoadConfig()
        {
            //FileStream Conf;
            //string sConfigFileName;
            //int nInteger;
            //string sString;
            //sConfigFileName = ".\\Config.ini";
            //Conf = new FileStream(sConfigFileName);
            //nInteger = Conf.ReadInteger(GateClass, "WhisperRecord", -1);// ��Ϸ˽��
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
            //// ��ֵ��ͼ
            //if (sString == "")
            //{
            //    Conf.WriteString(GateClass, "RECHARGINGMAP", "kaiqu");
            //}
            //g_GameConfig.boWhisperRecord = Conf.ReadBool(GateClass, "WhisperRecord", g_GameConfig.boWhisperRecord);
            //// ��Ϸ˽��
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
            //RECHARGINGMAP = Conf.ReadString(GateClass, "RECHARGINGMAP", RECHARGINGMAP);// ��ֵ
            //Conf.Free();
        }

        public static void SaveConfig()
        {
            //FileStream Conf;
            //string sConfigFileName;
            //sConfigFileName = ".\\Config.ini";
            //Conf = new FileStream(sConfigFileName);
            //Conf.WriteBool(GateClass, "WhisperRecord", g_GameConfig.boWhisperRecord);
            //// ��Ϸ˽��
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
            //Conf.WriteString(GateClass, "RECHARGINGMAP", RECHARGINGMAP);// ��ֵ��ͼ
            //Conf.Free();
        }
    }
}

