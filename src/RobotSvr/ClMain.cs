using Microsoft.Win32;
using System;
using System.Collections;
using System.Drawing;
using System.IO;
using System.Resources;
using SystemModule;

namespace RobotSvr
{
    public class TfrmMain
    {
        public static bool g_MoveBusy = false;
        public static bool g_PathBusy = false;
        public static int g_MoveStep = 0;
        public static int g_MoveErr = 0;
        public static long g_MoveErrTick = 0;
        public static long g_dwSendCDCheckTick = 0;
        public static TfrmMain frmMain = null;
        public static bool g_boShowMemoLog = false;
        public static int g_boShowRecog = 0;
        public static DrawScreen DScreen = null;
        public static IntroScene IntroScene = null;
        public static LoginScene LoginScene = null;
        public static SelectChrScene SelectChrScene = null;
        public static PlayScene g_PlayScene = null;
        public static ShakeScreen g_ShakeScreen = null;
        public static LoginNotice LoginNoticeScene = null;
        public static ClEventManager EventMan = null;
        public static TMap Map = null;
        public static TOneClickMode OneClickMode;
        // m_boPasswordIntputStatus: Boolean = False;
        public static TActor ShowMsgActor = null;
        public static long g_dwOverSpaceWarningTick = 0;
        public static char activebuf = "*";
        public const bool CHECKPACKED = true;
        public const bool CONFIGTEST = !CHECKPACKED;
        public const bool boNeedPatch = true;
        public const string NEARESTPALETTEINDEXFILE = ".\\Data\\npal.idx";
        public const string NEARESTPALETTEINDEXFILE_16 = ".\\Data\\npal-16.idx";
        public const string MonImageDir = ".\\Data\\";
        public const string NpcImageDir = ".\\Data\\";
        public const string ItemImageDir = ".\\Data\\";
        public const string WeaponImageDir = ".\\Data\\Weapon";
        public const string HumImageDir = ".\\Data\\Hum";
        public const string g_Debugflname = ".\\!Debug.txt";
        public const string g_sChgWindowMsg = "ALT + ENTER 切换窗口模式";
        private bool FPrintScreenNow = false;
        private bool FExchgScreen = false;
        // FHardwareSwitch: Boolean;
        private string SocStr = String.Empty;
        private string BufferStr = String.Empty;
        // testSocStr, testBufferStr: string;
        private TTimerCommand TimerCmd;
        private string MakeNewId = String.Empty;
        private long ActionLockTime = 0;
        private short ActionKey = 0;
        private long m_dwMouseDownTime = 0;
        private bool m_boMouseUpEnAble = false;
        private ClientPacket WaitingMsg = null;
        private string WaitingStr = String.Empty;
        private string WhisperName = String.Empty;
        private long m_dwProcUseMagicTick = 0;
        public bool ActionFailLock = false;
        public long ActionFailLockTime = 0;
        public long LastHitTick = 0;
        public string LoginID = String.Empty;
        public string LoginPasswd = String.Empty;
        public string m_sCharName = String.Empty;
        public string m_sHeroCharName = String.Empty;
        public int Certification = 0;
        public int m_nEatRetIdx = 0;
        public bool ActionLock = false;
        public bool m_boSupplyItem = false;
        public ArrayList NpcImageList = null;
        public ArrayList ItemImageList = null;
        public ArrayList WeaponImageList = null;
        public ArrayList HumImageList = null;
        public long m_dwDuraWarningTick = 0;
        public long dwIPTick = 0;
        public long dwhIPTick = 0;
        public object FEndeBuffer = null;
        public AnsiChar[] FTempBuffer;
        public AnsiChar[] FSendBuffer;
        public long DXRETime = 0;
        public long FDDrawHandle = 0;
        public bool FboDisplayChange = false;
        public int FHotKeyId = 0;
        public bool boSizeMove = false;

        public void FormCreate(System.Object Sender, System.EventArgs _e1)
        {
            string flname;
            int i;
            int n;
            ResourceManager Res;
            RegistryKey Reg;
            long MODULE;
            string REGPathStr;
            bool boBITDEPTH;
            try
            {
                boBITDEPTH = false;
                REGPathStr = "";
                // 设置分辨率大小                      //分辨率修改标记
                for (i = MShare.g_FSResolutionWidth.GetLowerBound(0); i <= MShare.g_FSResolutionWidth.GetUpperBound(0); i++)
                {
                    if (i == MShare.g_FScreenMode)
                    {
                        MShare.g_FScreenWidth = MShare.g_FSResolutionWidth[i];
                        MShare.g_FScreenHeight = MShare.g_FSResolutionHeight[i];
                        break;
                    }
                }
                // 当设置坐标超过限定值的时候初始化
                if (MShare.g_FScreenMode > MShare.g_FSResolutionWidth.GetUpperBound(0))
                {
                    MShare.g_FScreenWidth = MShare.g_FSResolutionWidth[0];
                    MShare.g_FScreenHeight = MShare.g_FSResolutionHeight[0];
                }
                MShare.InitScreenConfig();
                // 加载屏幕相关数值
                MShare.ScreenChanged();
                // 调整绘制地图范围大小
                MShare.g_APPathList = new ArrayList();
                MShare.g_ShowItemList = new THStringList();
                Grobal2.InitIPNeedExps();
                FDDrawHandle = 0;
                boSizeMove = false;
                DScreen = new DrawScreen();
                IntroScene = new IntroScene();
                LoginScene = new LoginScene();
                SelectChrScene = new SelectChrScene();
                g_PlayScene = new PlayScene();
                g_ShakeScreen = new ShakeScreen();
                LoginNoticeScene = new LoginNotice();
                NpcImageList = new ArrayList();
                ItemImageList = new ArrayList();
                WeaponImageList = new ArrayList();
                HumImageList = new ArrayList();
                Map = new TMap();
                MShare.g_DropedItemList = new ArrayList();
                MShare.g_MagicList = new ArrayList();
                MShare.g_IPMagicList = new ArrayList();
                MShare.g_HeroMagicList = new ArrayList();
                MShare.g_HeroIPMagicList = new ArrayList();
                for (n = MShare.g_ShopListArr.GetLowerBound(0); n <= MShare.g_ShopListArr.GetUpperBound(0); n++)
                {
                    MShare.g_ShopListArr[n] = new ArrayList();
                }
                MShare.g_FreeActorList = new ArrayList();
                EventMan = new TClEventManager();
                MShare.g_ChangeFaceReadyList = new ArrayList();
                MShare.g_ServerList = new ArrayList();
                MShare.g_SendSayList = new ArrayList();
                if (MShare.g_MySelf != null)
                {
                    MShare.g_MySelf.m_HeroObject = null;
                    MShare.g_MySelf.m_SlaveObject.Clear();
                    MShare.g_MySelf = null;
                }
                MShare.InitClientItems();
                MShare.g_DetectItemMineID = 0;
                MShare.g_BAFirstShape = -1;
                MShare.g_BuildAcusesSuc = -1;
                MShare.g_BuildAcusesStep = 0;
                MShare.g_BuildAcusesProc = 0;
                MShare.g_BuildAcusesRate = 0;
                MShare.g_SaveItemList = new ArrayList();
                MShare.g_MenuItemList = new ArrayList();
                MShare.g_DetectItem.Item.Name = "";
                MShare.g_WaitingUseItem.Item.Item.Name = "";
                MShare.g_WaitingDetectItem.Item.Item.Name = "";
                MShare.g_WaitingStallItem.Item.Item.Name = "";
                MShare.g_OpenBoxItem.Item.Item.Name = "";
                MShare.g_EatingItem.Item.Name = "";
                FPrintScreenNow = false;
                FExchgScreen = false;
                MShare.g_nLastMapMusic = -1;
                MShare.g_nTargetX = -1;
                MShare.g_nTargetY = -1;
                MShare.g_TargetCret = null;
                MShare.g_FocusCret = null;
                MShare.g_FocusItem = null;
                MShare.g_MagicTarget = null;
                MShare.g_nDebugCount = 0;
                MShare.g_nDebugCount1 = 0;
                MShare.g_nDebugCount2 = 0;
                MShare.g_nTestSendCount = 0;
                MShare.g_nTestReceiveCount = 0;
                MShare.g_boServerChanging = false;
                MShare.g_boBagLoaded = false;
                MShare.g_nHeroBagSize = 10;
                MShare.g_boAutoDig = false;
                MShare.g_boAutoSit = false;
                MShare.g_dwLatestClientTime2 = 0;
                MShare.g_dwFirstClientTime = 0;
                MShare.g_dwFirstServerTime = 0;
                MShare.g_dwFirstClientTimerTime = 0;
                MShare.g_dwLatestClientTimerTime = 0;
                MShare.g_dwFirstClientGetTime = 0;
                MShare.g_dwLatestClientGetTime = 0;
                MShare.g_nTimeFakeDetectCount = 0;
                MShare.g_nTimeFakeDetectTimer = 0;
                MShare.g_nTimeFakeDetectSum = 0;
                MShare.g_nDayBright = 3;
                MShare.g_nAreaStateValue = 0;
                MShare.g_ConnectionStep = TConnectionStep.cnsIntro;
                MShare.g_boSendLogin = false;
                MShare.g_boServerConnected = false;
                MShare.g_SoftClosed = false;
                SocStr = "";
                ActionFailLock = false;
                MShare.g_boMapMoving = false;
                MShare.g_boMapMovingWait = false;
                MShare.g_boCheckBadMapMode = false;
                MShare.g_boCheckSpeedHackDisplay = false;
                MShare.g_boViewMiniMap = true;
                MShare.g_nDupSelection = 0;
                MShare.g_dwLastAttackTick = MShare.GetTickCount();
                MShare.g_dwLastMoveTick = MShare.GetTickCount();
                MShare.g_dwLatestSpellTick = MShare.GetTickCount();
                MShare.g_dwAutoPickupTick = MShare.GetTickCount();
                MShare.g_boFirstTime = true;
                MShare.g_boItemMoving = false;
                MShare.g_boDoFadeIn = false;
                MShare.g_boDoFadeOut = false;
                MShare.g_boDoFastFadeOut = false;
                MShare.g_boNextTimePowerHit = false;
                MShare.g_boCanLongHit = false;
                MShare.g_boCanWideHit = false;
                MShare.g_boCanCrsHit = false;
                MShare.g_boNextTimeFireHit = false;
                MShare.g_boCanSLonHit = false;
                MShare.g_boNextTimeTwinHit = false;
                MShare.g_boNextTimePursueHit = false;
                MShare.g_boNextTimeRushHit = false;
                MShare.g_boNextTimeSmiteHit = false;
                MShare.g_boNextTimeSmiteLongHit = false;
                MShare.g_boNextTimeSmiteLongHit3 = false;
                MShare.g_boNextTimeSmiteLongHit2 = false;
                MShare.g_boNextTimeSmiteWideHit = false;
                MShare.g_boNextTimeSmiteWideHit2 = false;
                MShare.g_boNoDarkness = false;
                MShare.g_boQueryPrice = false;
                MShare.g_sSellPriceStr = "";
                MShare.g_boAllowGroup = false;
                MShare.g_GroupMembers = new THStringList();
                MShare.g_SeriesSkillSelList = new ArrayList();
                MShare.g_hSeriesSkillSelList = new ArrayList();
                MShare.g_ItemDesc = new THStringList();
                MShare.LoadItemDesc();
                MShare.LoadItemFilter();
                MShare.LoadMapDesc();// 加载迷你地图提示
                OneClickMode = TOneClickMode.toNone;
                m_dwMouseDownTime = 0;
                m_boMouseUpEnAble = true;
                MaketSystem.Units.MaketSystem.g_Market = new TMarketItemManager();
            }
            catch
            {
                DebugOutStr("[Exception]: TfrmMain.FormCreate");
            }
        }

        public void AppLogout()
        {
            if (MShare.g_boQueryExit)
            {
                return;
            }
            MShare.g_boQueryExit = true;
            try
            {
                SendClientMessage(Grobal2.CM_SOFTCLOSE, 0, 0, 0, 0);
                CloseAllWindows();
                g_PlayScene.ClearActors();
                MShare.g_SoftClosed = true;
                ActiveCmdTimer(MShare.TTimerCommand.tcSoftClose);
                SaveBagsData();
                MShare.SaveItemFilter();
            }
            finally
            {
                MShare.g_boQueryExit = false;
            }
        }

        public void AppExit()
        {
            if (MShare.g_boQueryExit)
            {
                return;
            }
            MShare.g_boQueryExit = true;
            try
            {
                SendClientMessage(Grobal2.CM_SOFTCLOSE, 0, 0, 0, 0);
                SaveBagsData();
                MShare.SaveItemFilter();
                frmMain.Close();
            }
            finally
            {
                MShare.g_boQueryExit = false;
            }
        }

        public void LoadBagsData()
        {
            if (MShare.g_boBagLoaded)
            {
                ClFunc.Loadbagsdat(".\\Config\\" + MShare.g_sServerName + "." + m_sCharName + ".itm-plus", MShare.g_ItemArr);
            }
            MShare.g_boBagLoaded = false;
        }

        public void SaveBagsData()
        {
            if (MShare.g_boBagLoaded)
            {
                ClFunc.FillBagStallItem(0);
                ClFunc.Savebagsdat(".\\Config\\" + MShare.g_sServerName + "." + m_sCharName + ".itm-plus", MShare.g_ItemArr);
            }
            MShare.g_boBagLoaded = false;
        }

        public string PrintScreenNow_IntToStr2(int n)
        {
            string result;
            if (n < 10)
            {
                result = "0" + (n).ToString();
            }
            else
            {
                result = (n).ToString();
            }
            return result;
        }

        private void ProcessMagic()
        {
            int nSX;
            int nSY;
            int tdir;
            int targid;
            int targx;
            int targy;
            TUseMagicInfo pmag;
            if ((g_PlayScene.ProcMagic.NTargetX < 0) || (MShare.g_MySelf == null))
            {
                return;
            }
            if (MShare.GetTickCount() - g_PlayScene.ProcMagic.dwTick > 5000)
            {
                g_PlayScene.ProcMagic.dwTick = MShare.GetTickCount();
                g_PlayScene.ProcMagic.NTargetX = -1;
                return;
            }
            if (MShare.GetTickCount() - m_dwProcUseMagicTick > 28)
            {
                m_dwProcUseMagicTick = MShare.GetTickCount();
                if (g_PlayScene.ProcMagic.FUnLockMagic)
                {
                    targid = 0;
                    targx = g_PlayScene.ProcMagic.NTargetX;
                    targy = g_PlayScene.ProcMagic.NTargetY;
                }
                else if ((g_PlayScene.ProcMagic.XTarget != null) && !g_PlayScene.ProcMagic.XTarget.m_boDeath)
                {
                    targid = g_PlayScene.ProcMagic.XTarget.m_nRecogId;
                    targx = g_PlayScene.ProcMagic.XTarget.m_nCurrX;
                    targy = g_PlayScene.ProcMagic.XTarget.m_nCurrY;
                }
                else
                {
                    g_PlayScene.ProcMagic.NTargetX = -1;
                    return;
                }
                nSX = Math.Abs(MShare.g_MySelf.m_nCurrX - targx);
                nSY = Math.Abs(MShare.g_MySelf.m_nCurrY - targy);
                if ((nSX <= MShare.g_nMagicRange) && (nSY <= MShare.g_nMagicRange))
                {
                    if (g_PlayScene.ProcMagic.FContinue || (CanNextAction() && ServerAcceptNextAction()))
                    {
                        MShare.g_dwLatestSpellTick = MShare.GetTickCount();
                        tdir = ClFunc.GetFlyDirection(MShare.g_MySelf.m_nCurrX, MShare.g_MySelf.m_nCurrY, targx, targy);
                        pmag = new TUseMagicInfo();
                        pmag.EffectNumber = g_PlayScene.ProcMagic.XMagic.Def.btEffect;
                        pmag.MagicSerial = g_PlayScene.ProcMagic.XMagic.Def.wMagicID;
                        pmag.ServerMagicCode = 0;
                        MShare.g_dwMagicDelayTime = 200 + g_PlayScene.ProcMagic.XMagic.Def.dwDelayTime;
                        MShare.g_dwMagicPKDelayTime = 0;
                        if ((MShare.g_MagicTarget != null))
                        {
                            if (MShare.g_MagicTarget.m_btRace == 0)
                            {
                                MShare.g_dwMagicPKDelayTime = 300 + (new System.Random(1100)).Next();
                            }
                        }
                        MShare.g_MySelf.SendMsg(Grobal2.CM_SPELL, targx, targy, tdir, (int)pmag, targid, "", 0);
                        g_PlayScene.ProcMagic.NTargetX = -1;
                    }
                }
                else
                {
                    MShare.g_ChrAction = TChrAction.caRun;
                    MShare.g_nTargetX = targx;
                    MShare.g_nTargetY = targy;
                }
            }
        }

        // function CalcBufferCRC(Buffer: PChar; nSize: Integer): Cardinal;
        // var
        // I: Integer;
        // Int: ^Integer;
        // nCrc: Cardinal;
        // begin
        // Int := Pointer(Buffer);
        // nCrc := 0;
        // for I := 0 to nSize div 4 - 1 do begin
        // nCrc := nCrc xor Int^;
        // Int := Pointer(Integer(Int) + 4);
        // end;
        // Result := nCrc;
        // end;
        private void ProcessKeyMessages()
        {
            if (ActionKey == 0)
            {
                return;
            }
            if ((MShare.g_MySelf != null) && MShare.g_MySelf.m_StallMgr.OnSale)
            {
                return;
            }
            switch (ActionKey)
            {
                case VK_F1:
                case VK_F2:
                case VK_F3:
                case VK_F4:
                case VK_F5:
                case VK_F6:
                case VK_F7:
                case VK_F8:
                    if (MShare.g_MySelf.m_btHorse == 0)
                    {
                        UseMagic(MShare.g_nMouseX, MShare.g_nMouseY, GetMagicByKey(((char)(ActionKey - VK_F1) + (byte)"1")));
                    }
                    ActionKey = 0;
                    MShare.g_nTargetX = -1;
                    return;
                    break;
                // Modify the A .. B: 12 .. 19
                case 12:
                    if (MShare.g_MySelf.m_btHorse == 0)
                    {
                        UseMagic(MShare.g_nMouseX, MShare.g_nMouseY, GetMagicByKey(((char)(ActionKey - 12) + (byte)"1" + (byte)0x14)));
                    }
                    ActionKey = 0;
                    MShare.g_nTargetX = -1;
                    return;
                    break;
            }
        }

        private void ProcessActionMessages()
        {
            int mx;
            int my;
            int dx;
            int dy;
            int crun;
            byte ndir;
            byte adir;
            byte mdir;
            bool bowalk;
            bool bostop;
            if (MShare.g_MySelf == null)
            {
                return;
            }
            if ((MShare.g_nTargetX >= 0) && CanNextAction() && ServerAcceptNextAction())
            {
                // ///////////////////////////////////////////////
                if (MShare.g_boOpenAutoPlay && (MShare.g_APMapPath != null) && (MShare.g_APStep >= 0) && (0 < MShare.g_APMapPath.GetUpperBound(0)))
                {
                    if ((Math.Abs(MShare.g_APMapPath[MShare.g_APStep].X - MShare.g_MySelf.m_nCurrX) <= 3) && (Math.Abs(MShare.g_APMapPath[MShare.g_APStep].X - MShare.g_MySelf.m_nCurrY) <= 3))
                    {
                        if (MShare.g_APMapPath.GetUpperBound(0) >= 2)
                        {
                            // 3点以上
                            if (MShare.g_APStep >= MShare.g_APMapPath.GetUpperBound(0))
                            {
                                // 当前点在终点...
                                // 终点 <-> 起点 距离过远...
                                if ((Math.Abs(MShare.g_APMapPath[MShare.g_APMapPath.GetUpperBound(0)].X - MShare.g_APMapPath[0].X) >= 36) || (Math.Abs(MShare.g_APMapPath[MShare.g_APMapPath.GetUpperBound(0)].X - MShare.g_APMapPath[0].X) >= 36))
                                {
                                    MShare.g_APGoBack = true;
                                    // 原路返回
                                    MShare.g_APLastPoint = MShare.g_APMapPath[MShare.g_APStep];
                                    MShare.g_APStep -= 1;
                                }
                                else
                                {
                                    // 循环到起点...
                                    MShare.g_APGoBack = false;
                                    MShare.g_APLastPoint = MShare.g_APMapPath[MShare.g_APStep];
                                    MShare.g_APStep = 0;
                                }
                            }
                            else
                            {
                                if (MShare.g_APGoBack)
                                {
                                    // 原路返回
                                    MShare.g_APLastPoint = MShare.g_APMapPath[MShare.g_APStep];
                                    MShare.g_APStep -= 1;
                                    if (MShare.g_APStep <= 0)
                                    {
                                        // 已回到起点
                                        MShare.g_APStep = 0;
                                        MShare.g_APGoBack = false;
                                    }
                                }
                                else
                                {
                                    // 循环...
                                    MShare.g_APLastPoint = MShare.g_APMapPath[MShare.g_APStep];
                                    MShare.g_APStep++;
                                }
                            }
                        }
                        else
                        {
                            // 2点,循环...
                            MShare.g_APLastPoint = MShare.g_APMapPath[MShare.g_APStep];
                            MShare.g_APStep++;
                            if (MShare.g_APStep > MShare.g_APMapPath.GetUpperBound(0))
                            {
                                MShare.g_APStep = 0;
                            }
                        }
                    }
                }
                if ((MShare.g_nTargetX != MShare.g_MySelf.m_nCurrX) || (MShare.g_nTargetY != MShare.g_MySelf.m_nCurrY))
                {
                    if ((MShare.g_MySelf.m_nTagX > 0) && (MShare.g_MySelf.m_nTagY > 0))
                    {
                        if (g_MoveBusy)
                        {
                            if (MShare.GetTickCount() - g_MoveErrTick > 60)
                            {
                                g_MoveErrTick = MShare.GetTickCount();
                                g_MoveErr++;
                            }
                        }
                        else
                        {
                            g_MoveErr = 0;
                        }
                        if ((g_MoveErr > 10))
                        {
                            g_MoveErr = 0;
                            g_MoveBusy = false;
                            TimerAutoMove.Enabled = false;
                            if ((MShare.g_MySelf.m_nTagX > 0) && (MShare.g_MySelf.m_nTagY > 0))
                            {
                                if (!g_PathBusy)
                                {
                                    g_PathBusy = true;
                                    try
                                    {
                                        Map.ReLoadMapData();
                                        MapUnit.g_MapPath = Map.FindPath(MShare.g_MySelf.m_nCurrX, MShare.g_MySelf.m_nCurrY, MShare.g_MySelf.m_nTagX, MShare.g_MySelf.m_nTagY, 0);
                                        if (MapUnit.g_MapPath != null)
                                        {
                                            g_MoveStep = 1;
                                            TimerAutoMove.Enabled = true;
                                        }
                                        else
                                        {
                                            MShare.g_MySelf.m_nTagX = 0;
                                            MShare.g_MySelf.m_nTagY = 0;
                                            DScreen.AddChatBoardString("自动移动出错，停止移动", GetRGB(5), Color.White);
                                        }
                                    }
                                    finally
                                    {
                                        g_PathBusy = false;
                                    }
                                }
                            }
                        }
                    }
                TTTT:
                    mx = MShare.g_MySelf.m_nCurrX;
                    my = MShare.g_MySelf.m_nCurrY;
                    dx = MShare.g_nTargetX;
                    dy = MShare.g_nTargetY;
                    ndir = ClFunc.GetNextDirection(mx, my, dx, dy);
                    switch (MShare.g_ChrAction)
                    {
                        case TChrAction.caWalk:
                        LB_WALK:
                            crun = MShare.g_MySelf.CanWalk();
                            if (IsUnLockAction() && (crun > 0))
                            {
                                ClFunc.GetNextPosXY(ndir, ref mx, ref my);
                                bostop = false;
                                if (!g_PlayScene.CanWalk(mx, my))
                                {
                                    if (MShare.g_boOpenAutoPlay && MShare.g_boAPAutoMove && (MShare.g_APPathList.Count > 0))
                                    {
                                        HeroActor.Init_Queue2();
                                        MShare.g_nTargetX = -1;
                                    }
                                    bowalk = false;
                                    adir = 0;
                                    if (!bowalk)
                                    {
                                        mx = MShare.g_MySelf.m_nCurrX;
                                        my = MShare.g_MySelf.m_nCurrY;
                                        ClFunc.GetNextPosXY(ndir, ref mx, ref my);
                                        if (CheckDoorAction(mx, my))
                                        {
                                            bostop = true;
                                        }
                                    }
                                    // not
                                    if (!bostop && (g_PlayScene.CrashMan(mx, my) || !Map.CanMove(mx, my)))
                                    {
                                        mx = MShare.g_MySelf.m_nCurrX;
                                        my = MShare.g_MySelf.m_nCurrY;
                                        adir = ClFunc.PrivDir(ndir);
                                        ClFunc.GetNextPosXY(adir, ref mx, ref my);
                                        if (!Map.CanMove(mx, my))
                                        {
                                            mx = MShare.g_MySelf.m_nCurrX;
                                            my = MShare.g_MySelf.m_nCurrY;
                                            adir = ClFunc.NextDir(ndir);
                                            ClFunc.GetNextPosXY(adir, ref mx, ref my);
                                            if (Map.CanMove(mx, my))
                                            {
                                                bowalk = true;
                                            }
                                        }
                                        else
                                        {
                                            bowalk = true;
                                        }
                                    }
                                    if (bowalk)
                                    {
                                        MShare.g_MySelf.UpdateMsg(Grobal2.CM_WALK, mx, my, adir, 0, 0, "", 0);
                                        MShare.g_dwLastMoveTick = MShare.GetTickCount();
                                        if (MShare.g_nOverAPZone > 0)
                                        {
                                            MShare.g_nOverAPZone -= 1;
                                        }
                                    }
                                    else
                                    {
                                        mdir = ClFunc.GetNextDirection(MShare.g_MySelf.m_nCurrX, MShare.g_MySelf.m_nCurrY, dx, dy);
                                        if (mdir != MShare.g_MySelf.m_btDir)
                                        {
                                            MShare.g_MySelf.SendMsg(Grobal2.CM_TURN, MShare.g_MySelf.m_nCurrX, MShare.g_MySelf.m_nCurrY, mdir, 0, 0, "", 0);
                                        }
                                        MShare.g_nTargetX = -1;
                                    }
                                }
                                else
                                {
                                    MShare.g_MySelf.UpdateMsg(Grobal2.CM_WALK, mx, my, ndir, 0, 0, "", 0);
                                    MShare.g_dwLastMoveTick = MShare.GetTickCount();
                                }
                            }
                            else
                            {
                                MShare.g_nTargetX = -1;
                            }
                            break;
                        case TChrAction.caRun:
                            // 免助跑
                            if (MShare.g_boCanStartRun || (MShare.g_nRunReadyCount >= 1))
                            {
                                crun = MShare.g_MySelf.CanRun();
                                // 骑马开始
                                if ((MShare.g_MySelf.m_btHorse != 0) && (ClFunc.GetDistance(mx, my, dx, dy) >= 3) && (crun > 0) && IsUnLockAction())
                                {
                                    ClFunc.GetNextHorseRunXY(ndir, ref mx, ref my);
                                    if (g_PlayScene.CanRun(MShare.g_MySelf.m_nCurrX, MShare.g_MySelf.m_nCurrY, mx, my))
                                    {
                                        MShare.g_MySelf.UpdateMsg(Grobal2.CM_HORSERUN, mx, my, ndir, 0, 0, "", 0);
                                        MShare.g_dwLastMoveTick = MShare.GetTickCount();
                                        if (MShare.g_nOverAPZone > 0)
                                        {
                                            MShare.g_nOverAPZone -= 1;
                                        }
                                    }
                                    else
                                    {
                                        // 如果跑失败则跳回去走
                                        MShare.g_ChrAction = TChrAction.caWalk;
                                        goto TTTT;
                                    }
                                }
                                else
                                {
                                    if ((ClFunc.GetDistance(mx, my, dx, dy) >= 2) && (crun > 0))
                                    {
                                        if (IsUnLockAction())
                                        {
                                            ClFunc.GetNextRunXY(ndir, ref mx, ref my);
                                            if (g_PlayScene.CanRun(MShare.g_MySelf.m_nCurrX, MShare.g_MySelf.m_nCurrY, mx, my))
                                            {
                                                MShare.g_MySelf.UpdateMsg(Grobal2.CM_RUN, mx, my, ndir, 0, 0, "", 0);
                                                MShare.g_dwLastMoveTick = MShare.GetTickCount();
                                                if (MShare.g_nOverAPZone > 0)
                                                {
                                                    MShare.g_nOverAPZone -= 1;
                                                }
                                            }
                                            else
                                            {
                                                // 如果跑失败则跳回去走
                                                MShare.g_ChrAction = TChrAction.caWalk;
                                                goto TTTT;
                                            }
                                        }
                                        else
                                        {
                                            MShare.g_nTargetX = -1;
                                        }
                                    }
                                    else
                                    {
                                        mdir = ClFunc.GetNextDirection(MShare.g_MySelf.m_nCurrX, MShare.g_MySelf.m_nCurrY, dx, dy);
                                        if (mdir != MShare.g_MySelf.m_btDir)
                                        {
                                            MShare.g_MySelf.SendMsg(Grobal2.CM_TURN, MShare.g_MySelf.m_nCurrX, MShare.g_MySelf.m_nCurrY, mdir, 0, 0, "", 0);
                                        }
                                        MShare.g_nTargetX = -1;
                                        goto LB_WALK;
                                    }
                                }
                            }
                            else
                            {
                                MShare.g_nRunReadyCount++;
                                goto LB_WALK;
                            }
                            break;
                    }
                }
                else if (MShare.g_boOpenAutoPlay && MShare.g_boAPAutoMove && (MShare.g_AutoPicupItem != null))
                {
                    frmMain.SendPickup();
                    MShare.g_sAPStr = "拾取物品";
                    if (MShare.g_boAPAutoMove && (MShare.g_APPathList.Count > 0))
                    {
                        HeroActor.Init_Queue2();
                        MShare.g_nTargetX = -1;
                    }
                }
            }
            MShare.g_nTargetX = -1;
        MMMM:
            if (MShare.g_MySelf.RealActionMsg.Ident > 0)
            {
                if (MShare.g_MySelf.RealActionMsg.Ident == Grobal2.CM_SPELL)
                {
                    SendSpellMsg(MShare.g_MySelf.RealActionMsg.Ident, MShare.g_MySelf.RealActionMsg.X, MShare.g_MySelf.RealActionMsg.Y, MShare.g_MySelf.RealActionMsg.Dir, MShare.g_MySelf.RealActionMsg.State);
                }
                else
                {
                    SendActMsg(MShare.g_MySelf.RealActionMsg.Ident, MShare.g_MySelf.RealActionMsg.X, MShare.g_MySelf.RealActionMsg.Y, MShare.g_MySelf.RealActionMsg.Dir);
                }
                MShare.g_MySelf.RealActionMsg.Ident = 0;
                if (MShare.g_nMDlgX != -1)
                {
                    if ((Math.Abs(MShare.g_nMDlgX - MShare.g_MySelf.m_nCurrX) >= 8) || (Math.Abs(MShare.g_nMDlgY - MShare.g_MySelf.m_nCurrY) >= 8))
                    {
                        MShare.g_nMDlgX = -1;
                    }
                }
                if (MShare.g_nStallX != -1)
                {
                    if ((Math.Abs(MShare.g_nStallX - MShare.g_MySelf.m_nCurrX) >= 8) || (Math.Abs(MShare.g_nStallY - MShare.g_MySelf.m_nCurrY) >= 8))
                    {
                        MShare.g_nStallX = -1;
                    }
                }
            }
        }

        public void SwitchMiniMap()
        {
            int i;
            string szMapTitle;
            TMapDescInfo pMapDescInfo;
            if (!MShare.g_boViewMiniMap)
            {
                if (MShare.GetTickCount() > MShare.g_dwQueryMsgTick)
                {
                    MShare.g_dwQueryMsgTick = MShare.GetTickCount() + 3000;
                    frmMain.SendWantMiniMap();
                    MShare.g_nViewMinMapLv = 1;
                }
            }
            else
            {
                if (MShare.g_nViewMinMapLv >= 2)
                {
                    MShare.g_nViewMinMapLv = 0;
                    MShare.g_boViewMiniMap = false;
                }
                else
                {
                    MShare.g_nViewMinMapLv++;
                }
            }
            MShare.g_xCurMapDescList.Clear();
            for (i = 0; i < MShare.g_xMapDescList.Count; i++)
            {
                szMapTitle = MShare.g_xMapDescList[i];
                pMapDescInfo = ((TMapDescInfo)(MShare.g_xMapDescList.Values[i]));
                if (((MShare.g_xMapDescList[i]).ToLower().CompareTo((MShare.g_sMapTitle).ToLower()) == 0) && (((pMapDescInfo.nFullMap == MShare.g_nViewMinMapLv) && (pMapDescInfo.nFullMap == 1)) || ((MShare.g_nViewMinMapLv != 1) && (pMapDescInfo.nFullMap == 0))))
                {
                    MShare.g_xCurMapDescList.Add(MShare.g_xMapDescList[i], ((pMapDescInfo) as Object));
                }
            }
        }

        public void FormKeyDown(System.Object Sender, System.Windows.Forms.KeyEventArgs _e1)
        {
            short M;
            short k;
            short MD;
            int wc;
            FileStream ini;
            if ((MShare.g_MySelf == null) || (DScreen.CurrentScene != g_PlayScene))
            {
                return;
            }
            switch (Key)
            {
                case (short)"Z":
                    if (new ArrayList(Shift).Contains(System.Windows.Forms.Keys.Control))
                    {
                        MShare.g_gcAss[0] = !MShare.g_gcAss[0];
                        frmMain.TimerAutoPlay.Enabled = MShare.g_gcAss[0];
                        if (frmMain.TimerAutoPlay.Enabled)
                        {
                            MShare.g_APTagget = null;
                            MShare.g_AutoPicupItem = null;
                            MShare.g_nAPStatus = -1;
                            MShare.g_nTargetX = -1;
                            MShare.g_APGoBack = false;
                            DScreen.AddChatBoardString("[挂机] 开始自动挂机...", Color.White, Color.Red);
                            SaveWayPoint();
                            if ((MShare.g_APMapPath != null))
                            {
                                MShare.g_APStep = 0;
                                MShare.g_APLastPoint.X = -1;
                                GetNearPoint();
                            }
                        }
                        else
                        {
                            DScreen.AddChatBoardString("[挂机] 停止自动挂机...", Color.White, Color.Red);
                        }
                        return;
                    }
                    break;
                case (short)"X":
                    if (MShare.g_MySelf == null)
                    {
                        return;
                    }
                    if (MShare.g_boOpenAutoPlay)
                    {
                        MShare.g_gcAss[0] = !MShare.g_gcAss[0];
                        frmMain.TimerAutoPlay.Enabled = MShare.g_gcAss[0];
                        if (frmMain.TimerAutoPlay.Enabled)
                        {
                            MShare.g_APTagget = null;
                            MShare.g_AutoPicupItem = null;
                            MShare.g_nAPStatus = -1;
                            MShare.g_nTargetX = -1;
                            MShare.g_APGoBack = false;
                            DScreen.AddChatBoardString("[挂机] 开始自动挂机...", Color.White, Color.Red);
                            SaveWayPoint();
                            if ((MShare.g_APMapPath != null))
                            {
                                MShare.g_APStep = 0;
                                MShare.g_APLastPoint.X = -1;
                                GetNearPoint();
                            }
                            if ((MShare.g_MySelf.m_HeroObject == null))
                            {
                                FrmDlg.m_dwUnRecallHeroTick = MShare.GetTickCount() - 58000;
                            }
                        }
                        else
                        {
                            DScreen.AddChatBoardString("[挂机] 停止自动挂机...", Color.White, Color.Red);
                        }
                        return;
                    }
                    break;
            }
        }

        public TClientMagic GetMagicByKey(char Key)
        {
            TClientMagic result;
            int i;
            TClientMagic pm;
            result = null;
            for (i = 0; i < MShare.g_MagicList.Count; i++)
            {
                pm = ((TClientMagic)(MShare.g_MagicList[i]));
                if (pm.Key == Key)
                {
                    result = pm;
                    break;
                }
            }
            return result;
        }

        public void UseMagic(int tx, int ty, TClientMagic pcm, bool boReacll, bool boContinue)
        {
            bool boSeriesSkill;
            int defSpellSpend;
            int tdir;
            int targx;
            int targy;
            int targid;
            TUseMagicInfo pmag;
            short SpellSpend;
            bool fUnLockMagic;
            if ((MShare.g_MySelf != null) && MShare.g_MySelf.m_StallMgr.OnSale)
            {
                return;
            }
            if (pcm == null)
            {
                return;
            }
            if (pcm.Def.wMagicID == 0)
            {
                return;
            }
            SpellSpend = Math.Round(pcm.Def.wSpell / (pcm.Def.btTrainLv + 1) * (pcm.Level + 1)) + pcm.Def.btDefSpell;
            if (pcm.Def.wMagicID == 114)
            {
                if (MShare.g_boSkill_114_MP)
                {
                    boSeriesSkill = false;
                }
                else
                {
                    boSeriesSkill = true;
                }
            }
            else if (new ArrayList(new int[] { 68, 78 }).Contains(pcm.Def.wMagicID))
            {
                if (MShare.g_boSkill_68_MP)
                {
                    boSeriesSkill = false;
                }
                else
                {
                    boSeriesSkill = true;
                }
            }
            else
            {
                boSeriesSkill = pcm.Def.wMagicID >= 100 && pcm.Def.wMagicID <= 111;
            }
            if (boSeriesSkill)
            {
                defSpellSpend = MShare.g_MySelf.m_nIPower;
            }
            else
            {
                defSpellSpend = MShare.g_MySelf.m_Abil.MP;
            }
            if ((SpellSpend <= defSpellSpend))
            {
                if (pcm.Def.btEffectType == 0)
                {
                    if (new ArrayList(new int[] { 68, 78 }).Contains(pcm.Def.wMagicID))
                    {
                        boContinue = true;
                        goto labSpell;
                    }
                    if (pcm.Def.wMagicID == 26)
                    {
                        // 烈火时间间隔
                        if (MShare.g_boNextTimeFireHit || (MShare.GetTickCount() - MShare.g_dwLatestFireHitTick <= 10 * 1000))
                        {
                            return;
                        }
                    }
                    if (pcm.Def.wMagicID == 66)
                    {
                        if (MShare.g_boCanSLonHit || (MShare.GetTickCount() - MShare.g_dwLatestSLonHitTick <= 8 * 1000))
                        {
                            return;
                        }
                    }
                    if (pcm.Def.wMagicID == 43)
                    {
                        if (MShare.g_boNextTimeTwinHit || (MShare.GetTickCount() - MShare.g_dwLatestTwinHitTick <= 15 * 1000))
                        {
                            return;
                        }
                    }
                    if (pcm.Def.wMagicID == 56)
                    {
                        if (MShare.g_boNextTimePursueHit || (MShare.GetTickCount() - MShare.g_dwLatestPursueHitTick <= 10 * 1000))
                        {
                            return;
                        }
                    }
                    if ((new ArrayList(new int[] { 27 }).Contains(pcm.Def.wMagicID)))
                    {
                        // 野蛮时间间隔
                        if ((MShare.GetTickCount() - MShare.g_dwLatestRushRushTick <= 3 * 1000))
                        {
                            return;
                        }
                    }
                    // /////////////////////////////////////////////
                    if (pcm.Def.wMagicID == 100)
                    {
                        if (boContinue || (CanNextAction() && ServerAcceptNextAction() && CanNextHit()))
                        {
                        }
                        else
                        {
                            return;
                        }
                    }
                    if (pcm.Def.wMagicID == 101)
                    {
                        if (MShare.g_boNextTimeSmiteHit || (MShare.GetTickCount() - MShare.g_dwLatestSmiteHitTick <= 1 * 100))
                        {
                            return;
                        }
                    }
                    if (pcm.Def.wMagicID == 102)
                    {
                        if (MShare.g_boNextTimeSmiteLongHit || (MShare.GetTickCount() - MShare.g_dwLatestSmiteLongHitTick <= 1 * 100))
                        {
                            return;
                        }
                    }
                    if (pcm.Def.wMagicID == 103)
                    {
                        if (MShare.g_boNextTimeSmiteWideHit || (MShare.GetTickCount() - MShare.g_dwLatestSmiteWideHitTick <= 1 * 100))
                        {
                            return;
                        }
                    }
                    if (pcm.Def.wMagicID == 113)
                    {
                        if (MShare.g_boNextTimeSmiteLongHit2 || (MShare.GetTickCount() - MShare.g_dwLatestSmiteLongHitTick2 <= 10 * 1000))
                        {
                            return;
                        }
                    }
                    if (pcm.Def.wMagicID == 114)
                    {
                        if (MShare.g_boNextTimeSmiteWideHit2 || (MShare.GetTickCount() - MShare.g_dwLatestSmiteWideHitTick2 <= 2 * 1000))
                        {
                            return;
                        }
                    }
                    if (pcm.Def.wMagicID == 115)
                    {
                        if (MShare.g_boNextTimeSmiteLongHit3 || (MShare.GetTickCount() - MShare.g_dwLatestSmiteLongHitTick3 <= 2 * 1000))
                        {
                            return;
                        }
                    }
                    if (MShare.g_boSpeedRate)
                    {
                        if (boContinue || (MShare.GetTickCount() - MShare.g_dwLatestSpellTick > MShare.g_dwSpellTime - ((long)MShare.g_MagSpeedRate) * 20))
                        {
                            MShare.g_dwLatestSpellTick = MShare.GetTickCount();
                            MShare.g_dwMagicDelayTime = 0;
                            // x
                            SendSpellMsg(Grobal2.CM_SPELL, MShare.g_MySelf.m_btDir, 0, pcm.Def.wMagicID, 0, false);
                        }
                    }
                    else
                    {
                        if (boContinue || (MShare.GetTickCount() - MShare.g_dwLatestSpellTick > MShare.g_dwSpellTime))
                        {
                            MShare.g_dwLatestSpellTick = MShare.GetTickCount();
                            MShare.g_dwMagicDelayTime = 0;
                            // x
                            SendSpellMsg(Grobal2.CM_SPELL, MShare.g_MySelf.m_btDir, 0, pcm.Def.wMagicID, 0, false);
                        }
                    }
                    // g_MySelf.SendMsg(CM_SPELL, targx, targy, tdir, Integer(pmag), targid, '', 0);
                    // DScreen.AddChatBoardString(Format('%d:%d', [pcm.Def.wMagicID, SpellSpend]), GetRGB(5), clWhite);
                }
                else
                {
                labSpell:
                    fUnLockMagic = (new ArrayList(new int[] { 2, 9, 10, 14, 21, 33, 37, 41, 46, 50, 58, 70, 72, 75 }).Contains(pcm.Def.wMagicID));
                    if (fUnLockMagic)
                    {
                        MShare.g_MagicTarget = MShare.g_FocusCret;
                    }
                    else
                    {
                        if (MShare.g_boMagicLock && (g_PlayScene.IsValidActor(MShare.g_FocusCret) && !MShare.g_FocusCret.m_boDeath))
                        {
                            MShare.g_MagicLockActor = MShare.g_FocusCret;
                        }
                        MShare.g_MagicTarget = MShare.g_MagicLockActor;
                    }
                    if (MShare.g_MagicTarget != null)
                    {
                        if (!MShare.g_boMagicLock || MShare.g_MagicTarget.m_boDeath || (MShare.g_MagicTarget.m_btRace == Grobal2.RCC_MERCHANT) || !g_PlayScene.IsValidActor(MShare.g_MagicTarget))
                        {
                            MShare.g_MagicTarget = null;
                            MShare.g_MagicLockActor = null;
                        }
                    }
                    if ((MShare.g_MagicTarget != null) && (MShare.g_MagicTarget is THumActor))
                    {
                        if (((THumActor)(MShare.g_MagicTarget)).m_StallMgr.OnSale)
                        {
                            MShare.g_MagicTarget = null;
                            MShare.g_MagicLockActor = null;
                        }
                    }
                    SmartChangePoison(pcm);
                    if (MShare.g_MagicTarget == null)
                    {
                        MShare.g_nCurrentMagic = 888;
                        if (boReacll)
                        {
                            targx = tx;
                            targy = ty;
                        }
                        targid = 0;
                    }
                    else
                    {
                        if (!boReacll)
                        {
                            targx = MShare.g_MagicTarget.m_nCurrX;
                            targy = MShare.g_MagicTarget.m_nCurrY;
                        }
                        else
                        {
                            targx = tx;
                            targy = ty;
                        }
                        targid = MShare.g_MagicTarget.m_nRecogId;
                    }
                    if ((Math.Abs(MShare.g_MySelf.m_nCurrX - targx) > MShare.g_nMagicRange) || (Math.Abs(MShare.g_MySelf.m_nCurrY - targy) > MShare.g_nMagicRange))
                    {
                        if (MShare.g_gcTec[14] && (fUnLockMagic || (targid != 0)))
                        {
                            g_PlayScene.ProcMagic.NTargetX = targx;
                            g_PlayScene.ProcMagic.NTargetY = targy;
                            g_PlayScene.ProcMagic.XMagic = pcm;
                            g_PlayScene.ProcMagic.XTarget = MShare.g_MagicLockActor;
                            g_PlayScene.ProcMagic.FReacll = boReacll;
                            g_PlayScene.ProcMagic.FContinue = boContinue;
                            g_PlayScene.ProcMagic.FUnLockMagic = fUnLockMagic;
                            g_PlayScene.ProcMagic.DwTick = MShare.GetTickCount();
                        }
                        else
                        {
                            if (MShare.GetTickCount() - g_dwOverSpaceWarningTick > 1000)
                            {
                                g_dwOverSpaceWarningTick = MShare.GetTickCount();
                                DScreen.AddSysMsg("目标太远了，施展魔法失败！！！");
                            }
                            g_PlayScene.ProcMagic.NTargetX = -1;
                        }
                        return;
                    }
                    g_PlayScene.ProcMagic.NTargetX = -1;
                    if (boContinue || (CanNextAction() && ServerAcceptNextAction()))
                    {
                        MShare.g_dwLatestSpellTick = MShare.GetTickCount();
                        tdir = ClFunc.GetFlyDirection(MShare.g_MySelf.m_nCurrX, MShare.g_MySelf.m_nCurrY, targx, targy);
                        pmag = new TUseMagicInfo();
                        pmag.EffectNumber = pcm.Def.btEffect;
                        pmag.MagicSerial = pcm.Def.wMagicID;
                        pmag.ServerMagicCode = 0;
                        MShare.g_dwMagicDelayTime = 200 + pcm.Def.dwDelayTime;
                        MShare.g_dwMagicPKDelayTime = 0;
                        if ((MShare.g_MagicTarget != null))
                        {
                            if (MShare.g_MagicTarget.m_btRace == 0)
                            {
                                MShare.g_dwMagicPKDelayTime = 300 + (new System.Random(1100)).Next();
                            }
                            // blue
                        }
                        MShare.g_MySelf.SendMsg(Grobal2.CM_SPELL, targx, targy, tdir, (int)pmag, targid, "", 0);
                    }
                }
            }
            else
            {
                if (boSeriesSkill)
                {
                    if (MShare.GetTickCount() - MShare.g_IPointLessHintTick > 5000)
                    {
                        MShare.g_IPointLessHintTick = MShare.GetTickCount();
                        DScreen.AddSysMsg(Format("需要 %d 内力值才能释放 %s", new string[] { SpellSpend, pcm.Def.sMagicName }));
                    }
                }
                else if (MShare.GetTickCount() - MShare.g_MPLessHintTick > 1000)
                {
                    MShare.g_MPLessHintTick = MShare.GetTickCount();
                    DScreen.AddSysMsg(Format("需要 %d 魔法值才能释放 %s", new string[] { SpellSpend, pcm.Def.sMagicName }));
                }
            }
        }

        private void UseMagicSpell(int who, int effnum, int targetx, int targety, int magic_id)
        {
            TActor Actor;
            int adir;
            TUseMagicInfo UseMagic;
            Actor = g_PlayScene.FindActor(who);
            if (Actor != null)
            {
                adir = ClFunc.GetFlyDirection(Actor.m_nCurrX, Actor.m_nCurrY, targetx, targety);
                UseMagic = new TUseMagicInfo();
                UseMagic.EffectNumber = effnum % 255;
                UseMagic.ServerMagicCode = 0;
                UseMagic.MagicSerial = magic_id % 300;
                Actor.SendMsg(Grobal2.SM_SPELL, effnum / 255, magic_id / 300, adir, (int)UseMagic, 0, "", 0);
                MShare.g_nSpellCount++;
            }
            else
            {
                MShare.g_nSpellFailCount++;
            }
        }

        private void UseMagicFire(int who, int efftype, int effnum, int targetx, int targety, int target, int maglv)
        {
            TActor Actor;
            int Sound;
            Sound = 0;
            Actor = g_PlayScene.FindActor(who);
            if (Actor != null)
            {
                Actor.SendMsg(Grobal2.SM_MAGICFIRE, target, efftype, effnum, targetx, targety, (maglv).ToString(), Sound);
                if (MShare.g_nFireCount < MShare.g_nSpellCount)
                {
                    MShare.g_nFireCount++;
                }
            }
            MShare.g_MagicTarget = null;
        }

        private void UseMagicFireFail(int who)
        {
            TActor Actor;
            Actor = g_PlayScene.FindActor(who);
            if (Actor != null)
            {
                Actor.SendMsg(Grobal2.SM_MAGICFIRE_FAIL, 0, 0, 0, 0, 0, "", 0);
            }
            MShare.g_MagicTarget = null;
        }

        public void ActorAutoEat(THumActor Actor)
        {
            if (!Actor.m_boDeath)
            {
                ActorCheckHealth(false);
                if (MShare.g_EatingItem.Item.Name == "")
                {
                    if (MShare.IsPersentSpc(Actor.m_Abil.HP, Actor.m_Abil.MaxHP))
                    {
                        ActorCheckHealth(true);
                    }
                }
            }
        }

        public void ActorCheckHealth(bool bNeedSP)
        {
            int i;
            int nCount;
            int MaxHP;
            int MaxMP;
            int MaxSP;
            int hidx;
            int midx;
            int sidx;
            int bidx;
            int uhidx;
            int umidx;
            int usidx;
            int ubidx;
            bool bHint;
            bool bEatOK;
            bool bEatSp;
            nCount = 0;
            hidx = -1;
            midx = -1;
            sidx = -1;
            bidx = -1;
            uhidx = -1;
            umidx = -1;
            usidx = -1;
            ubidx = -1;
            MaxHP = Int32.MaxValue / 2 - 1;
            MaxMP = Int32.MaxValue / 2 - 1;
            MaxSP = Int32.MaxValue / 2 - 1;
            for (i = Grobal2.MAXBAGITEM - (1 + 0); i >= 0; i--)
            {
                if ((MShare.g_ItemArr[i].Item.Name != "") && (MShare.g_ItemArr[i].Item.NeedIdentify < 4))
                {
                    switch (MShare.g_ItemArr[i].Item.ItemtdMode)
                    {
                        case 00:
                            switch (MShare.g_ItemArr[i].Item.Itemhape)
                            {
                                case 0:
                                    // 普通药
                                    if (MShare.g_gcProtect[0] && (MShare.g_ItemArr[i].Item.AC > 0) && (MShare.g_ItemArr[i].Item.AC < MaxHP))
                                    {
                                        MaxHP = MShare.g_ItemArr[i].Item.AC;
                                        hidx = i;
                                    }
                                    if (MShare.g_gcProtect[1] && (MShare.g_ItemArr[i].Item.MAC > 0) && (MShare.g_ItemArr[i].Item.MAC < MaxMP))
                                    {
                                        MaxMP = MShare.g_ItemArr[i].Item.MAC;
                                        midx = i;
                                    }
                                    break;
                                case 1:
                                    // 速效药
                                    if (MShare.g_gcProtect[3] && (MShare.g_ItemArr[i].Item.AC > 0) && (MShare.g_ItemArr[i].Item.AC < MaxSP))
                                    {
                                        MaxSP = MShare.g_ItemArr[i].Item.AC;
                                        sidx = i;
                                    }
                                    break;
                            }
                            break;
                        case 2:
                        case 3:
                            if (MShare.g_gcProtect[5])
                            {
                                if ((MShare.g_ItemArr[i].Item.Name).ToLower().CompareTo((MShare.g_sRenewBooks[MShare.g_gnProtectPercent[6]]).ToLower()) == 0)
                                {
                                    bidx = i;
                                }
                            }
                            break;
                        case 31:
                            switch (MShare.g_ItemArr[i].Item.AniCount)
                            {
                                case 1:
                                    if (MShare.g_gcProtect[0])
                                    {
                                        uhidx = i;
                                    }
                                    break;
                                case 2:
                                    if (MShare.g_gcProtect[1])
                                    {
                                        umidx = i;
                                    }
                                    break;
                                case 3:
                                    if (MShare.g_gcProtect[3])
                                    {
                                        usidx = i;
                                    }
                                    break;
                                default:
                                    if (MShare.g_gcProtect[5] && ((MShare.g_ItemArr[i].Item.Name).ToLower().CompareTo((MShare.g_sRenewBooks[MShare.g_gnProtectPercent[6]] + "包").ToLower()) == 0))
                                    {
                                        ubidx = i;
                                    }
                                    break;
                            }
                            break;
                    }
                }
                else
                {
                    nCount++;
                }
            }
            bHint = false;
            bEatSp = false;
            bEatOK = false;
            if (MShare.GetTickCount() - MShare.g_MySelf.m_dwMsgHint > 15 * 1000)
            {
                MShare.g_MySelf.m_dwMsgHint = MShare.GetTickCount();
                bHint = true;
            }
            if (!bNeedSP)
            {
                if (MShare.g_gcProtect[0] && MShare.IsPersentHP(MShare.g_MySelf.m_Abil.HP, MShare.g_MySelf.m_Abil.MaxHP))
                {
                    if (MShare.GetTickCount() - MShare.g_MySelf.m_dwHealthHP > ((long)MShare.g_gnProtectTime[0]))
                    {
                        MShare.g_MySelf.m_dwHealthHP = MShare.GetTickCount();
                        if (hidx > -1)
                        {
                            EatItem(hidx);
                            bEatOK = true;
                        }
                        else if ((nCount > 4) && (uhidx > -1))
                        {
                            EatItem(uhidx);
                            bEatOK = true;
                        }
                        else
                        {
                            bEatSp = true;
                            if (bHint)
                            {
                                DScreen.AddSysMsgCenter("你的金创药已经用完！", Color.Lime, Color.Black, 10);
                            }
                            bEatOK = false;
                        }
                    }
                }
            }
            if (!bNeedSP)
            {
                if (MShare.g_gcProtect[1] && MShare.IsPersentMP(MShare.g_MySelf.m_Abil.MP, MShare.g_MySelf.m_Abil.MaxMP))
                {
                    if (MShare.GetTickCount() - MShare.g_MySelf.m_dwHealthMP > ((long)MShare.g_gnProtectTime[1]))
                    {
                        MShare.g_MySelf.m_dwHealthMP = MShare.GetTickCount();
                        if (midx > -1)
                        {
                            EatItem(midx);
                            bEatOK = true;
                        }
                        else if ((nCount > 4) && (umidx > -1))
                        {
                            EatItem(umidx);
                            bEatOK = true;
                        }
                        else
                        {
                            if (MShare.g_gcProtect[11])
                            {
                                bEatSp = true;
                            }
                            if (bHint)
                            {
                                DScreen.AddSysMsgCenter("你的魔法药已经用完！", Color.Lime, Color.Black, 10);
                            }
                            bEatOK = false;
                        }
                    }
                }
            }
            if (!bEatOK)
            {
                if (MShare.g_gcProtect[3] && (bNeedSP || bEatSp || (MShare.g_gcProtect[11] && MShare.IsPersentSpc(MShare.g_MySelf.m_Abil.MP, MShare.g_MySelf.m_Abil.MaxMP))))
                {
                    if (MShare.GetTickCount() - MShare.g_MySelf.m_dwHealthSP > ((long)MShare.g_gnProtectTime[3]))
                    {
                        MShare.g_MySelf.m_dwHealthSP = MShare.GetTickCount();
                        if (sidx > -1)
                        {
                            Ea.Item.Itemidx);
                        }
                        else if ((nCount > 4) && (usidx > -1))
                        {
                            EatItem(usidx);
                        }
                        else if (bHint)
                        {
                            DScreen.AddSysMsgCenter("你的特殊药品已经用完！", Color.Lime, Color.Black, 10);
                        }
                    }
                }
            }
            if (MShare.g_gcProtect[5] && MShare.IsPersentBook(MShare.g_MySelf.m_Abil.HP, MShare.g_MySelf.m_Abil.MaxHP))
            {
                if (MShare.GetTickCount() - MShare.g_MySelf.m_dwHealthBK > ((long)MShare.g_gnProtectTime[5]))
                {
                    MShare.g_MySelf.m_dwHealthBK = MShare.GetTickCount();
                    if (bidx > -1)
                    {
                        EatItem(bidx);
                    }
                    else if ((nCount > 4) && (ubidx > -1))
                    {
                        EatItem(ubidx);
                    }
                    else if (bHint)
                    {
                        DScreen.AddSysMsgCenter("你的" + MShare.g_sRenewBooks[MShare.g_gnProtectPercent[6]] + "已经用完！", Color.Lime, Color.Black, 10);
                    }
                }
            }
        }

        public void AutoSupplyBeltItem(int nType, int idx, string sItem)
        {
            int i;
            if ((idx >= 0 && idx <= 5) && (sItem != ""))
            {
                if (MShare.g_ItemArr[idx].Item.Name == "")
                {
                    for (i = MShare.MAXBAGITEMCL - 1; i >= 6; i--)
                    {
                        if (MShare.g_ItemArr[i].Item.Name == sItem)
                        {
                            MShare.g_ItemArr[idx] = MShare.g_ItemArr[i];
                            MShare.g_ItemArr[i].Item.Name = "";
                            return;
                        }
                    }
                    AutoUnBindItem(nType, sItem);
                }
            }
        }

        public void AutoSupplyBagItem(int nType, string sItem)
        {
            int i;
            for (i = MShare.MAXBAGITEMCL - 1; i >= 6; i--)
            {
                if (MShare.g_ItemArr[i].Item.Name == sItem)
                {
                    return;
                }
            }
            AutoUnBindItem(nType, sItem);
        }

        /// <summary>
        /// 自动解包物品
        /// </summary>
        /// <param name="nType"></param>
        /// <param name="sItem"></param>
        public void AutoUnBindItem(int nType, string sItem)
        {
            int i;
            int n;
            int idx;
            bool boUnBindAble;
            bool boIsUnBindItem;
            if ((sItem != "") && (nType != 0))
            {
                boIsUnBindItem = false;
                for (i = MShare.g_UnBindItems.GetLowerBound(0); i <= MShare.g_UnBindItems.GetUpperBound(0); i++)
                {
                    if (sItem == MShare.g_UnBindItems[i])
                    {
                        boIsUnBindItem = true;
                        break;
                    }
                }
                if (!boIsUnBindItem)
                {
                    return;
                }
                n = 0;
                boUnBindAble = false;
                for (i = 0; i <= MShare.MAXBAGITEMCL - 1 - 6; i++)
                {
                    if (MShare.g_ItemArr[i].Item.Name == "")
                    {
                        n++;
                        if (n >= 5)
                        {
                            boUnBindAble = true;
                            break;
                        }
                    }
                }
                if (!boUnBindAble)
                {
                    return;
                }
                idx = -1;
                for (i = MShare.MAXBAGITEMCL - 1; i >= 6; i--)
                {
                    if (MShare.g_ItemArr[i].Item.ItemtdMode == 31)
                    {
                        if (MShare.g_ItemArr[i].Item.Name != "")
                        {
                            if (MShare.g_ItemArr[i].Item.Itemhape == nType)
                            {
                                idx = i;
                                break;
                            }
                        }
                    }
                }
                if (idx > -1)
                {
                    SendEat(MShare.g_ItemArr[idx].MakeIndex, "", MShare.g_ItemArr[idx].Item.ItemtdMode);
                    if ((MShare.g_ItemArr[idx].Dura > 1))
                    {
                        MShare.g_ItemArr[idx].Dura = MShare.g_ItemArr[idx].Dura - 1;
                        MShare.g_EatingItem = MShare.g_ItemArr[idx];
                        m_nEatRetIdx = -1;
                    }
                    else
                    {
                        MShare.g_ItemArr[idx].Dura = MShare.g_ItemArr[idx].Dura - 1;
                        MShare.g_EatingItem = MShare.g_ItemArr[idx];
                        MShare.g_ItemArr[idx].Item.Name = "";
                        m_nEatRetIdx = -1;
                    }
                }
            }
        }

        public bool EatItemName(string Str)
        {
            bool result;
            int i;
            result = false;
            if ((Str == "小退") && (MShare.g_MySelf.m_nHiterCode > 0))
            {
                AppLogout();
                return result;
            }
            if ((Str == "大退") && (MShare.g_MySelf.m_nHiterCode > 0))
            {
                DScreen.ClearHint();
                return result;
            }
            for (i = 0; i < MShare.MAXBAGITEMCL; i++)
            {
                if ((MShare.g_ItemArr[i].Item.Name == Str) && (MShare.g_ItemArr[i].Item.NeedIdentify < 4))
                {
                    EatItem(i);
                    result = true;
                    return result;
                }
            }
            return result;
        }

        public void EatItem(int idx)
        {
            int i;
            int where;
            bool takeon;
            bool eatable;
            eatable = false;
            takeon = false;
            where = -1;
            if (idx >= 0 && idx <= MShare.MAXBAGITEMCL - 1)
            {
                if ((MShare.g_EatingItem.Item.Name != "") && (MShare.GetTickCount() - MShare.g_dwEatTime > 5 * 1000))
                {
                    MShare.g_EatingItem.Item.Name = "";
                }
                if ((MShare.g_EatingItem.Item.Name == "") && (MShare.g_ItemArr[idx].Item.Name != "") && (MShare.g_ItemArr[idx].Item.NeedIdentify < 4))
                {
                    if ((MShare.g_ItemArr[idx].Item.ItemtdMode <= 3) || (MShare.g_ItemArr[idx].Item.ItemtdMode == 31))
                    {
                        if ((MShare.g_ItemArr[idx].Dura > 1))
                        {
                            MShare.g_ItemArr[idx].Dura = MShare.g_ItemArr[idx].Dura - 1;
                            MShare.g_EatingItem = MShare.g_ItemArr[idx];
                            MShare.g_ItemArr[idx].Item.Name = "";
                            eatable = true;
                        }
                        else
                        {
                            MShare.g_EatingItem = MShare.g_ItemArr[idx];
                            MShare.g_ItemArr[idx].Item.Name = "";
                            eatable = true;
                        }
                    }
                    else
                    {
                        if ((MShare.g_ItemArr[idx].Item.Overlap >= 1))
                        {
                            if ((MShare.g_ItemArr[idx].Dura > 1))
                            {
                                frmMain.SendDismantleItem(MShare.g_ItemArr[idx].Item.Name, MShare.g_ItemArr[idx].MakeIndex, 1, 0);
                                MShare.g_dwEatTime = MShare.GetTickCount();
                                return;
                            }
                            else
                            {
                                goto lab1;
                            }
                        }
                        else
                        {
                        lab1:
                            if ((MShare.g_ItemArr[idx].Item.ItemtdMode == 46) && (MShare.g_ItemArr[idx].Item.Itemhape >= 2 && MShare.g_ItemArr[idx].Item.Itemhape <= 6))
                            {
                                if (!MShare.g_RareBoxWindow.m_boKeyAvail && (MShare.g_OpenBoxItem.Item.Item.Name == "") && !FrmDlg.DWBoxBKGnd.Visible)
                                {
                                    MShare.g_OpenBoxItem.Index = idx;
                                    MShare.g_OpenBoxItem.Item = MShare.g_ItemArr[idx];
                                    MShare.g_ItemArr[idx].Item.Name = "";
                                    FrmDlg.DWBoxBKGnd.Visible = true;
                                }
                                return;
                            }
                            if ((MShare.g_ItemArr[idx].Item.ItemtdMode == 41) && (new ArrayList(new int[] { 10, 30 }).Contains(MShare.g_ItemArr[idx].Item.Itemhape)) && (MShare.g_BuildAcusesStep != 1) && FrmDlg.DWBuildAcus.Visible && (new ArrayList(new int[] { 1, 2 }).Contains(FrmDlg.DWBuildAcus.tag)))
                            {
                                for (i = 0; i <= 7; i++)
                                {
                                    if (MShare.g_BuildAcuses[i].Item.Item.Name == "")
                                    {
                                        if (((MShare.g_ItemArr[idx].Item.Itemhape >= 30 && MShare.g_ItemArr[idx].Item.Itemhape <= 34) && (i >= 5 && i <= 7)) || ((MShare.g_ItemArr[idx].Item.Itemhape >= 10 && MShare.g_ItemArr[idx].Item.Itemhape <= 14) && (i >= 0 && i <= 4)))
                                        {
                                            break;
                                        }
                                    }
                                }
                                if (i >= 0 && i <= 7)
                                {
                                    MShare.g_boItemMoving = true;
                                    MShare.g_MovingItem.Index = idx;
                                    MShare.g_MovingItem.Item = MShare.g_ItemArr[idx];
                                    MShare.g_ItemArr[idx].Item.Name = "";
                                }
                                //switch (i)
                                //{
                                //    case 0:
                                //        FrmDlg.DBAcus1Click(FrmDlg.DBAcus1, 0, 0);
                                //        break;
                                //    case 1:
                                //        FrmDlg.DBAcus1Click(FrmDlg.DBAcus2, 0, 0);
                                //        break;
                                //    case 2:
                                //        FrmDlg.DBAcus1Click(FrmDlg.DBAcus3, 0, 0);
                                //        break;
                                //    case 3:
                                //        FrmDlg.DBAcus1Click(FrmDlg.DBAcus4, 0, 0);
                                //        break;
                                //    case 4:
                                //        FrmDlg.DBAcus1Click(FrmDlg.DBAcus5, 0, 0);
                                //        break;
                                //    case 5:
                                //        FrmDlg.DBAcus1Click(FrmDlg.DBCharm1, 0, 0);
                                //        break;
                                //    case 6:
                                //        FrmDlg.DBAcus1Click(FrmDlg.DBCharm2, 0, 0);
                                //        break;
                                //    case 7:
                                //        FrmDlg.DBAcus1Click(FrmDlg.DBCharm3, 0, 0);
                                //        break;
                                //}
                                return;
                            }
                        }
                        where = ClFunc.GetTakeOnPosition(MShare.g_ItemArr[idx].Item, MShare.g_UseItems, true);
                        if (where >= 0 && where <= Grobal2.U_FASHION)
                        {
                            // takeon...
                            takeon = true;
                            MShare.g_EatingItem = MShare.g_ItemArr[idx];
                            MShare.g_ItemArr[idx].Item.Name = "";
                        }
                        // end;
                    }
                }
            }
            else if ((idx == -1) && MShare.g_boItemMoving)
            {
                if ((MShare.g_MovingItem.Item.Item.ItemtdMode <= 4) || (MShare.g_MovingItem.Item.Item.ItemtdMode == 31) && (MShare.g_MovingItem.Item.Item.NeedIdentify < 4))
                {
                    if (((MShare.g_MovingItem.Item.Item.ItemtdMode <= 3) || (MShare.g_MovingItem.Item.Item.ItemtdMode == 31)) && (MShare.g_MovingItem.Item.Item.Overlap >= 1) && (MShare.g_MovingItem.Item.Dura > 1))
                    {
                        MShare.g_MovingItem.Item.Dura = MShare.g_MovingItem.Item.Dura - 1;
                        MShare.g_boItemMoving = false;
                        MShare.g_EatingItem = MShare.g_MovingItem.Item;
                        MShare.g_MovingItem.Item.Item.Name = "";
                    }
                    else
                    {
                        MShare.g_boItemMoving = false;
                        MShare.g_EatingItem = MShare.g_MovingItem.Item;
                        MShare.g_MovingItem.Item.Item.Name = "";
                    }
                    if ((MShare.g_EatingItem.Item.ItemtdMode == 4) && (MShare.g_EatingItem.Item.Itemhape < 50))
                    {
                        //if (System.Windows.Forms.DialogResult.Yes != DebugOutStr("是否确认开始练习 \"" + MShare.g_EatingItem.Item.Name + "\"？", new object[] { MessageBoxButtons.YesNo, MessageBoxButtons.YesNo }))
                        //{
                        //    ClFunc.AddItemBag(MShare.g_EatingItem);
                        //    return;
                        //}
                    }
                    idx = frmMain.m_nEatRetIdx;
                    eatable = true;
                }
                else
                {
                    if ((MShare.g_MovingItem.Item.Item.Overlap >= 1))
                    {
                        if ((MShare.g_MovingItem.Item.Dura > 1))
                        {
                            frmMain.SendDismantleItem(MShare.g_MovingItem.Item.Item.Name, MShare.g_MovingItem.Item.MakeIndex, 1, 0);
                            MShare.g_dwEatTime = MShare.GetTickCount();
                            return;
                        }
                        else
                        {
                            goto lab2;
                        }
                    }
                    else
                    {
                    lab2:
                        if ((MShare.g_MovingItem.Item.Item.ItemtdMode == 46) && (MShare.g_MovingItem.Item.Item.Itemhape >= 2 && MShare.g_MovingItem.Item.Item.Itemhape <= 6))
                        {
                            if (!MShare.g_RareBoxWindow.m_boKeyAvail && (MShare.g_OpenBoxItem.Item.Item.Name == "") && !FrmDlg.DWBoxBKGnd.Visible)
                            {
                                MShare.g_OpenBoxItem.Index = frmMain.m_nEatRetIdx;
                                MShare.g_OpenBoxItem.Item = MShare.g_MovingItem.Item;
                                MShare.g_boItemMoving = false;
                                MShare.g_MovingItem.Item.Item.Name = "";
                                FrmDlg.DWBoxBKGnd.Visible = true;
                            }
                            return;
                        }
                        if ((MShare.g_MovingItem.Item.Item.ItemtdMode == 41) && (new ArrayList(new int[] { 10, 30 }).Contains(MShare.g_MovingItem.Item.Item.Itemhape)) && (MShare.g_BuildAcusesStep != 1) && FrmDlg.DWBuildAcus.Visible && (new ArrayList(new int[] { 1, 2 }).Contains(FrmDlg.DWBuildAcus.tag)))
                        {
                            for (i = 0; i <= 7; i++)
                            {
                                if (MShare.g_BuildAcuses[i].Item.Item.Name == "")
                                {
                                    if (((MShare.g_MovingItem.Item.Item.Itemhape >= 30 && MShare.g_MovingItem.Item.Item.Itemhape <= 34) && (i >= 5 && i <= 7)) || ((MShare.g_MovingItem.Item.Item.Itemhape >= 10 && MShare.g_MovingItem.Item.Item.Itemhape <= 14) && (i >= 0 && i <= 4)))
                                    {
                                        break;
                                    }
                                }
                            }
                            switch (i)
                            {
                                //case 0:
                                //    FrmDlg.DBAcus1Click(FrmDlg.DBAcus1, 0, 0);
                                //    break;
                                //case 1:
                                //    FrmDlg.DBAcus1Click(FrmDlg.DBAcus2, 0, 0);
                                //    break;
                                //case 2:
                                //    FrmDlg.DBAcus1Click(FrmDlg.DBAcus3, 0, 0);
                                //    break;
                                //case 3:
                                //    FrmDlg.DBAcus1Click(FrmDlg.DBAcus4, 0, 0);
                                //    break;
                                //case 4:
                                //    FrmDlg.DBAcus1Click(FrmDlg.DBAcus5, 0, 0);
                                //    break;
                                //case 5:
                                //    FrmDlg.DBAcus1Click(FrmDlg.DBCharm1, 0, 0);
                                //    break;
                                //case 6:
                                //    FrmDlg.DBAcus1Click(FrmDlg.DBCharm2, 0, 0);
                                //    break;
                                //case 7:
                                //    FrmDlg.DBAcus1Click(FrmDlg.DBCharm3, 0, 0);
                                //    break;
                            }
                            return;
                        }
                    }
                    where = ClFunc.GetTakeOnPosition(MShare.g_MovingItem.Item.Item, MShare.g_UseItems, true);
                    if (where >= 0 && where <= Grobal2.U_FASHION)
                    {
                        takeon = true;
                        MShare.g_boItemMoving = false;
                        MShare.g_EatingItem = MShare.g_MovingItem.Item;
                        MShare.g_MovingItem.Item.Item.Name = "";
                        idx = frmMain.m_nEatRetIdx;
                    }
                    else
                    {

                    }
                }
            }
            if (eatable)
            {
                m_nEatRetIdx = idx;
                m_boSupplyItem = true;
                MShare.g_dwEatTime = MShare.GetTickCount();
                SendEat(MShare.g_EatingItem.MakeIndex, MShare.g_EatingItem.Item.Name, MShare.g_EatingItem.Item.ItemtdMode);
            }
            else if (takeon)
            {
                m_nEatRetIdx = idx;
                MShare.g_dwEatTime = MShare.GetTickCount();
                MShare.g_WaitingUseItem.Item = MShare.g_EatingItem;
                MShare.g_WaitingUseItem.Index = where;
                SendTakeOnItem(where, MShare.g_EatingItem.MakeIndex, MShare.g_EatingItem.Item.Name);
                MShare.g_EatingItem.Item.Name = "";
            }
        }

        public bool TargetInSwordLongAttackRange(int ndir)
        {
            bool result;
            int nX = 0;
            int nY = 0;
            TActor Actor;
            if (MShare.g_gcTec[0])
            {
                result = true;
                return result;
            }
            result = false;
            ClFunc.GetFrontPosition(MShare.g_MySelf.m_nCurrX, MShare.g_MySelf.m_nCurrY, ndir, ref nX, ref nY);
            ClFunc.GetFrontPosition(nX, nY, ndir, ref nX, ref nY);
            if ((Math.Abs(MShare.g_MySelf.m_nCurrX - nX) == 2) || (Math.Abs(MShare.g_MySelf.m_nCurrY - nY) == 2))
            {
                Actor = g_PlayScene.FindActorXY(nX, nY);
                if (Actor != null)
                {
                    if (!Actor.m_boDeath)
                    {
                        result = true;
                    }
                }
            }
            return result;
        }

        public bool TargetInSwordLongAttackRange2(int sx, int sy, int dx, int dy)
        {
            bool result;
            result = false;
            if ((Math.Abs(sx - dx) == 2) && (Math.Abs(sy - dy) == 0))
            {
                result = true;
                return result;
            }
            if ((Math.Abs(sx - dx) == 0) && (Math.Abs(sy - dy) == 2))
            {
                result = true;
                return result;
            }
            if ((Math.Abs(sx - dx) == 2) && (Math.Abs(sy - dy) == 2))
            {
                result = true;
                return result;
            }
            return result;
        }

        public bool TargetInSwordWideAttackRange(int ndir)
        {
            bool result;
            int nX = 0;
            int nY = 0;
            int rx = 0;
            int ry = 0;
            int mdir;
            TActor Actor;
            TActor ractor;
            result = false;
            ClFunc.GetFrontPosition(MShare.g_MySelf.m_nCurrX, MShare.g_MySelf.m_nCurrY, ndir, ref nX, ref nY);
            Actor = g_PlayScene.FindActorXY(nX, nY);
            mdir = (ndir + 1) % 8;
            ClFunc.GetFrontPosition(MShare.g_MySelf.m_nCurrX, MShare.g_MySelf.m_nCurrY, mdir, ref rx, ref ry);
            ractor = g_PlayScene.FindActorXY(rx, ry);
            if (ractor == null)
            {
                mdir = (ndir + 2) % 8;
                ClFunc.GetFrontPosition(MShare.g_MySelf.m_nCurrX, MShare.g_MySelf.m_nCurrY, mdir, ref rx, ref ry);
                ractor = g_PlayScene.FindActorXY(rx, ry);
            }
            if (ractor == null)
            {
                mdir = (ndir + 7) % 8;
                ClFunc.GetFrontPosition(MShare.g_MySelf.m_nCurrX, MShare.g_MySelf.m_nCurrY, mdir, ref rx, ref ry);
                ractor = g_PlayScene.FindActorXY(rx, ry);
            }
            if ((Actor != null) && (ractor != null))
            {
                if (!Actor.m_boDeath && !ractor.m_boDeath)
                {
                    result = true;
                }
            }
            return result;
        }

        public bool TargetInSwordLongAttackRangeX(int ndir)
        {
            bool result;
            int nC;
            int nX = 0;
            int nY = 0;
            TActor Actor;
            result = false;
            nC = 1;
            while (true)
            {
                if (GetNextPosition(MShare.g_MySelf.m_nCurrX, MShare.g_MySelf.m_nCurrY, ndir, nC, ref nX, ref nY))
                {
                    Actor = g_PlayScene.FindActorXY(nX, nY);
                    if ((Actor != null) && !Actor.m_boDeath)
                    {
                        result = true;
                        break;
                    }
                }
                nC++;
                if (nC >= 5)
                {
                    break;
                }
            }
            return result;
        }

        public bool TargetInSwordLongAttackRangeA(int ndir)
        {
            bool result;
            int nC = 0;
            int nX = 0;
            int nY = 0;
            TActor Actor;
            result = false;
            nC = 1;
            while (true)
            {
                if (GetNextPosition(MShare.g_MySelf.m_nCurrX, MShare.g_MySelf.m_nCurrY, ndir, nC, ref nX, ref nY))
                {
                    Actor = g_PlayScene.FindActorXY(nX, nY);
                    if ((Actor != null) && !Actor.m_boDeath)
                    {
                        result = true;
                        break;
                    }
                }
                nC++;
                if (nC >= 4)
                {
                    break;
                }
            }
            return result;
        }

        public bool TargetInSwordCrsAttackRange(int ndir)
        {
            bool result;
            int nX = 0;
            int nY = 0;
            int rx = 0;
            int ry = 0;
            int mdir;
            TActor Actor;
            TActor ractor;
            result = false;
            ClFunc.GetFrontPosition(MShare.g_MySelf.m_nCurrX, MShare.g_MySelf.m_nCurrY, ndir, ref nX, ref nY);
            Actor = g_PlayScene.FindActorXY(nX, nY);
            mdir = (ndir + 1) % 8;
            ClFunc.GetFrontPosition(MShare.g_MySelf.m_nCurrX, MShare.g_MySelf.m_nCurrY, mdir, ref rx, ref ry);
            ractor = g_PlayScene.FindActorXY(rx, ry);
            if (ractor == null)
            {
                mdir = (ndir + 2) % 8;
                ClFunc.GetFrontPosition(MShare.g_MySelf.m_nCurrX, MShare.g_MySelf.m_nCurrY, mdir, ref rx, ref ry);
                ractor = g_PlayScene.FindActorXY(rx, ry);
            }
            if (ractor == null)
            {
                mdir = (ndir + 7) % 8;
                ClFunc.GetFrontPosition(MShare.g_MySelf.m_nCurrX, MShare.g_MySelf.m_nCurrY, mdir, ref rx, ref ry);
                ractor = g_PlayScene.FindActorXY(rx, ry);
            }
            if ((Actor != null) && (ractor != null))
            {
                if (!Actor.m_boDeath && !ractor.m_boDeath)
                {
                    result = true;
                }
            }
            return result;
        }

        public bool AttackTarget(TActor target)
        {
            bool result;
            int tdir;
            int dx;
            int dy;
            int nHitMsg;
            result = false;
            nHitMsg = Grobal2.CM_HIT;
            if (MShare.g_UseItems[Grobal2.U_WEAPON].s.StdMode == 6)
            {
                nHitMsg = Grobal2.CM_HEAVYHIT;
            }
            tdir = ClFunc.GetNextDirection(MShare.g_MySelf.m_nCurrX, MShare.g_MySelf.m_nCurrY, target.m_nCurrX, target.m_nCurrY);
            if ((Math.Abs(MShare.g_MySelf.m_nCurrX - target.m_nCurrX) <= 1) && (Math.Abs(MShare.g_MySelf.m_nCurrY - target.m_nCurrY) <= 1) && (!target.m_boDeath))
            {
                if (TimerAutoPlay.Enabled)
                {
                    MShare.g_boAPAutoMove = false;
                    if (MShare.g_APTagget != null)
                    {
                        MShare.g_sAPStr = Format("[挂机] 怪物目标：%s (%d,%d) 正在使用普通攻击", new int[] { MShare.g_APTagget.m_sUserName, MShare.g_APTagget.m_nCurrX, MShare.g_APTagget.m_nCurrY });
                    }
                }
                if (CanNextAction() && ServerAcceptNextAction())
                {
                    if (CanNextHit(false) || MShare.g_NextSeriesSkill)
                    {
                        MShare.g_NextSeriesSkill = false;
                        if (MShare.g_boNextTimeSmiteHit && (MShare.g_MySelf.m_Abil.MP >= 7))
                        {
                            MShare.g_boNextTimeSmiteHit = false;
                            nHitMsg = Grobal2.CM_SMITEHIT;
                            MShare.g_MySelf.SendMsg(nHitMsg, MShare.g_MySelf.m_nCurrX, MShare.g_MySelf.m_nCurrY, tdir, 0, 0, "", 0);
                            goto lab;
                        }
                        else if (MShare.g_boNextTimeSmiteLongHit && (MShare.g_MySelf.m_Abil.MP >= 7))
                        {
                            MShare.g_boNextTimeSmiteLongHit = false;
                            nHitMsg = Grobal2.CM_SMITELONGHIT;
                            MShare.g_MySelf.SendMsg(nHitMsg, MShare.g_MySelf.m_nCurrX, MShare.g_MySelf.m_nCurrY, tdir, 0, 0, "", 0);
                            goto lab;
                        }
                        else if (MShare.g_boNextTimeSmiteWideHit && (MShare.g_MySelf.m_Abil.MP >= 7))
                        {
                            MShare.g_boNextTimeSmiteWideHit = false;
                            nHitMsg = Grobal2.CM_SMITEWIDEHIT;
                            MShare.g_MySelf.SendMsg(nHitMsg, MShare.g_MySelf.m_nCurrX, MShare.g_MySelf.m_nCurrY, tdir, 0, 0, "", 0);
                            goto lab;
                        }
                        else if (MShare.g_boNextTimeSmiteLongHit2 && (MShare.g_MySelf.m_Abil.MP >= 7))
                        {
                            MShare.g_boNextTimeSmiteLongHit2 = false;
                            nHitMsg = Grobal2.CM_SMITELONGHIT2;
                            MShare.g_MySelf.SendMsg(nHitMsg, MShare.g_MySelf.m_nCurrX, MShare.g_MySelf.m_nCurrY, tdir, 0, 0, "", 0);
                            goto lab;
                        }
                    }
                    if (CanNextHit())
                    {
                        if (MShare.g_boNextTimeSmiteWideHit2 && (MShare.g_MySelf.m_Abil.MP >= 1))
                        {
                            MShare.g_boNextTimeSmiteWideHit2 = false;
                            nHitMsg = Grobal2.CM_SMITEWIDEHIT2;
                        }
                        else if (MShare.g_boNextTimeSmiteLongHit3 && (MShare.g_MySelf.m_Abil.MP >= 1))
                        {
                            MShare.g_boNextTimeSmiteLongHit3 = false;
                            nHitMsg = Grobal2.CM_SMITELONGHIT3;
                        }
                        else if (MShare.g_boNextTimeTwinHit && (MShare.g_MySelf.m_Abil.MP >= 10))
                        {
                            MShare.g_boNextTimeTwinHit = false;
                            nHitMsg = Grobal2.CM_TWNHIT;
                        }
                        else if (MShare.g_boNextTimePursueHit && (MShare.g_MySelf.m_Abil.MP >= 7))
                        {
                            MShare.g_boNextTimePursueHit = false;
                            nHitMsg = Grobal2.CM_PURSUEHIT;
                        }
                        else if (MShare.g_boNextTimeFireHit && (MShare.g_MySelf.m_Abil.MP >= 7))
                        {
                            MShare.g_boNextTimeFireHit = false;
                            nHitMsg = Grobal2.CM_FIREHIT;
                        }
                        else if (MShare.g_boCanSLonHit && (MShare.g_MySelf.m_Abil.MP >= 7))
                        {
                            MShare.g_boCanSLonHit = false;
                            nHitMsg = Grobal2.CM_HERO_LONGHIT2;
                        }
                        else if (MShare.g_boNextTimePowerHit)
                        {
                            MShare.g_boNextTimePowerHit = false;
                            nHitMsg = Grobal2.CM_POWERHIT;
                        }
                        else if (MShare.g_boCanSquHit && (MShare.g_MySelf.m_Abil.MP >= 3) && (MShare.g_nSquHitPoint > 0))
                        {
                            nHitMsg = Grobal2.CM_SQUHIT;
                        }
                        else if ((MShare.g_MySelf.m_Abil.MP >= 3) && (MShare.g_boCanWideHit || (MShare.g_gcTec[1] && (GetMagicByID(25) != null) && TargetInSwordWideAttackRange(tdir))))
                        {
                            nHitMsg = Grobal2.CM_WIDEHIT;
                        }
                        else if (MShare.g_boCanCrsHit && (MShare.g_MySelf.m_Abil.MP >= 6))
                        {
                            nHitMsg = Grobal2.CM_CRSHIT;
                        }
                        else if (MShare.g_boCanLongHit && (TargetInSwordLongAttackRange(tdir)))
                        {
                            nHitMsg = Grobal2.CM_LONGHIT;
                        }
                        MShare.g_MySelf.SendMsg(nHitMsg, MShare.g_MySelf.m_nCurrX, MShare.g_MySelf.m_nCurrY, tdir, 0, 0, "", 0);
                    }
                lab:
                }
                result = true;
                MShare.g_dwLastAttackTick = MShare.GetTickCount();
            }
            else
            {
                if (MShare.g_boNextTimeSmiteWideHit2 && (MShare.g_MySelf.m_Abil.MP >= 1))
                {
                    if (CanNextAction() && ServerAcceptNextAction() && CanNextHit())
                    {
                        if ((Math.Abs(MShare.g_MySelf.m_nCurrX - target.m_nCurrX) <= 5) && (Math.Abs(MShare.g_MySelf.m_nCurrY - target.m_nCurrY) <= 5))
                        {
                            MShare.g_boNextTimeSmiteWideHit2 = false;
                            nHitMsg = Grobal2.CM_SMITEWIDEHIT2;
                            MShare.g_MySelf.SendMsg(nHitMsg, MShare.g_MySelf.m_nCurrX, MShare.g_MySelf.m_nCurrY, tdir, 0, 0, "", 0);
                            MShare.g_dwLatestSmiteWideHitTick2 = MShare.GetTickCount();
                            return result;
                        }
                    }
                }
                if (MShare.g_boNextTimeSmiteLongHit3 && (MShare.g_MySelf.m_Abil.MP >= 1) && TargetInSwordLongAttackRangeA(tdir))
                {
                    if (CanNextAction() && ServerAcceptNextAction() && CanNextHit())
                    {
                        MShare.g_boNextTimeSmiteLongHit3 = false;
                        nHitMsg = Grobal2.CM_SMITELONGHIT3;
                        MShare.g_MySelf.SendMsg(nHitMsg, MShare.g_MySelf.m_nCurrX, MShare.g_MySelf.m_nCurrY, tdir, 0, 0, "", 0);
                        MShare.g_dwLastAttackTick = MShare.GetTickCount();
                        MShare.g_dwLatestSmiteLongHitTick3 = MShare.GetTickCount();
                        return result;
                    }
                }
                if (MShare.g_boNextTimeSmiteLongHit && (MShare.g_MySelf.m_Abil.MP >= 7) && TargetInSwordLongAttackRangeA(tdir))
                {
                    if (CanNextAction() && ServerAcceptNextAction() && CanNextHit())
                    {
                        MShare.g_boNextTimeSmiteLongHit = false;
                        nHitMsg = Grobal2.CM_SMITELONGHIT;
                        MShare.g_MySelf.SendMsg(nHitMsg, MShare.g_MySelf.m_nCurrX, MShare.g_MySelf.m_nCurrY, tdir, 0, 0, "", 0);
                        MShare.g_dwLastAttackTick = MShare.GetTickCount();
                        return result;
                    }
                }
                if (MShare.g_boNextTimeSmiteLongHit2 && (MShare.g_MySelf.m_Abil.MP >= 7) && TargetInSwordLongAttackRangeX(tdir))
                {
                    if (CanNextAction() && ServerAcceptNextAction() && CanNextHit())
                    {
                        MShare.g_boNextTimeSmiteLongHit2 = false;
                        nHitMsg = Grobal2.CM_SMITELONGHIT2;
                        MShare.g_MySelf.SendMsg(nHitMsg, MShare.g_MySelf.m_nCurrX, MShare.g_MySelf.m_nCurrY, tdir, 0, 0, "", 0);
                        MShare.g_dwLastAttackTick = MShare.GetTickCount();
                        return result;
                    }
                }
                if (MShare.g_boNextTimePursueHit && (MShare.g_MySelf.m_Abil.MP >= 7) && TargetInSwordLongAttackRangeX(tdir))
                {
                    if (CanNextAction() && ServerAcceptNextAction() && CanNextHit())
                    {
                        MShare.g_boNextTimePursueHit = false;
                        nHitMsg = Grobal2.CM_PURSUEHIT;
                        MShare.g_MySelf.SendMsg(nHitMsg, MShare.g_MySelf.m_nCurrX, MShare.g_MySelf.m_nCurrY, tdir, 0, 0, "", 0);
                        MShare.g_dwLastAttackTick = MShare.GetTickCount();
                        return result;
                    }
                }
                if (MShare.g_boCanSLonHit && (MShare.g_MySelf.m_Abil.MP >= 7) && TargetInSwordLongAttackRangeX(tdir))
                {
                    if (CanNextAction() && ServerAcceptNextAction() && CanNextHit())
                    {
                        MShare.g_boCanSLonHit = false;
                        nHitMsg = Grobal2.CM_HERO_LONGHIT2;
                        MShare.g_MySelf.SendMsg(nHitMsg, MShare.g_MySelf.m_nCurrX, MShare.g_MySelf.m_nCurrY, tdir, 0, 0, "", 0);
                        MShare.g_dwLastAttackTick = MShare.GetTickCount();
                        return result;
                    }
                }
                if (MShare.g_boCanLongHit && (MShare.g_MySelf.m_btJob == 0) && (!target.m_boDeath) && MShare.g_boAutoLongAttack && MShare.g_gcTec[10] && (MShare.g_MagicArr[0][12] != null) && TargetInSwordLongAttackRange2(MShare.g_MySelf.m_nCurrX, MShare.g_MySelf.m_nCurrY, target.m_nCurrX, target.m_nCurrY))
                {
                    if (CanNextAction() && ServerAcceptNextAction() && CanNextHit())
                    {
                        nHitMsg = Grobal2.CM_LONGHIT;
                        MShare.g_MySelf.SendMsg(nHitMsg, MShare.g_MySelf.m_nCurrX, MShare.g_MySelf.m_nCurrY, tdir, 0, 0, "", 0);
                        MShare.g_dwLastAttackTick = MShare.GetTickCount();
                    }
                    else if (MShare.g_boAutoLongAttack && MShare.g_gcTec[10] && TimerAutoPlay.Enabled)
                    {
                        // 走刺杀位
                        result = true;
                        return result;
                    }
                }
                else
                {
                    dx = MShare.g_MySelf.m_nCurrX;
                    dy = MShare.g_MySelf.m_nCurrY;
                    if ((MShare.g_MySelf.m_btJob == 0) && MShare.g_boAutoLongAttack && MShare.g_gcTec[10] && (MShare.g_MagicArr[0][12] != null))
                    {
                        ClFunc.GetNextHitPosition(target.m_nCurrX, target.m_nCurrY, ref dx, ref dy);
                        if (!g_PlayScene.CanWalk(dx, dy))
                        {
                            ClFunc.GetBackPosition(target.m_nCurrX, target.m_nCurrY, tdir, ref dx, ref dy);
                        }
                    }
                    else
                    {
                        ClFunc.GetBackPosition(target.m_nCurrX, target.m_nCurrY, tdir, ref dx, ref dy);
                    }
                    MShare.g_nTargetX = dx;
                    MShare.g_nTargetY = dy;
                    MShare.g_ChrAction = TChrAction.caRun;
                    // end;
                }
                if (TimerAutoPlay.Enabled)
                {
                    MShare.g_boAPAutoMove = true;
                    if (target != null)
                    {
                        MShare.g_sAPStr = Format("[挂机] 怪物目标：%s (%d,%d) 正在跑向", new int[] { target.m_sUserName, target.m_nCurrX, target.m_nCurrY });
                    }
                }
            }
            return result;
        }

        private void _DXDrawMouseDown(Object Sender, MouseButtons Button, Keys Shift, int X, int Y, bool boClick)
        {
            int i;
            int tdir;
            int nX = 0;
            int nY = 0;
            int nHitMsg;
            int sel;
            TActor target;
            string itemnames;
            Point P;
            Rectangle RC;
            double rx;
            double ry;
            TActor Actor;
            string szMapTitle;
            TMapDescInfo pMapDescInfo;
            ActionKey = 0;
            MShare.g_nMouseX = X;
            MShare.g_nMouseY = Y;
            if (boClick)
            {
                if ((Button == System.Windows.Forms.MouseButtons.Right) && (MShare.g_OpenBoxItem.Item.Item.Name != "") && !MShare.g_RareBoxWindow.m_boRareBoxShow && (FrmDlg.DWBoxBKGnd.Visible))
                {
                    ClFunc.AddItemBag(MShare.g_OpenBoxItem.Item, MShare.g_OpenBoxItem.Index);
                    DScreen.AddSysMsg(MShare.g_OpenBoxItem.Item.Item.Name + "被发现");
                    MShare.g_OpenBoxItem.Item.Item.Name = "";
                    FrmDlg.DWBoxBKGnd.Visible = false;
                }
                if ((Button == System.Windows.Forms.MouseButtons.Right) && MShare.g_boItemMoving)
                {
                    // 当前是否在移动物品
                    FrmDlg.CancelItemMoving;
                    return;
                }
            }
            if ((MShare.g_MySelf == null) || (DScreen.CurrentScene != g_PlayScene))
            {
                return;
            }
            if ((new ArrayList(Shift).Contains(System.Windows.Forms.Keys.RButton)))
            {
                // 鼠标右键
                if (boClick)
                {
                    g_PlayScene.ProcMagic.NTargetX = -1;
                    MShare.g_boAutoDig = false;
                    MShare.g_boAutoSit = false;
                    if (TimerAutoMove.Enabled)
                    {
                        if ((new ArrayList(Shift).Contains(System.Windows.Forms.Keys.RButton)) || (new ArrayList(Shift).Contains(System.Windows.Forms.Keys.LButton)))
                        {
                            MShare.g_MySelf.m_nTagX = 0;
                            MShare.g_MySelf.m_nTagY = 0;
                            TimerAutoMove.Enabled = false;
                            MapUnit.g_MapPath = new Point[0];
                            MapUnit.g_MapPath = null;
                            DScreen.AddChatBoardString("停止自动移动", GetRGB(5), Color.White);
                        }
                    }
                    if (Shift == new System.Windows.Forms.Keys[] { System.Windows.Forms.Keys.RButton })
                    {
                        MShare.g_nDupSelection++;
                    }
                    target = g_PlayScene.GetAttackFocusCharacter(X, Y, MShare.g_nDupSelection, ref sel, false);
                    if (MShare.g_nDupSelection != sel)
                    {
                        MShare.g_nDupSelection = 0;
                    }
                    if (target != null)
                    {
                        if (new ArrayList(Shift).Contains(System.Windows.Forms.Keys.Control))
                        {
                            if (MShare.GetTickCount() - MShare.g_dwLastMoveTick > 500)
                            {
                                if ((target.m_btRace == 0))
                                {
                                    SendClientMessage(Grobal2.CM_QUERYUSERSTATE, target.m_nRecogId, target.m_nCurrX, target.m_nCurrY, 0);
                                    return;
                                }
                            }
                        }
                        if (new ArrayList(Shift).Contains(System.Windows.Forms.Keys.Alt))
                        {
                            if (MShare.GetTickCount() - MShare.g_dwLastMoveTick > 500)
                            {
                                if ((target.m_btRace == 0))
                                {
                                    return;
                                }
                            }
                        }
                    }
                    else
                    {
                        MShare.g_nDupSelection = 0;
                    }
                    MShare.g_FocusItem2 = g_PlayScene.GetDropItems(X, Y, ref itemnames);
                    if (MShare.g_FocusItem2 != null)
                    {
                        if (itemnames[itemnames.Length] == '\\')
                        {
                            itemnames = itemnames.Substring(1 - 1, itemnames.Length - 1);
                        }
                        if (new ArrayList(Shift).Contains(System.Windows.Forms.Keys.Alt))
                        {
                            // get user name to chat
                            if (MShare.GetTickCount() - MShare.g_dwLastMoveTick > 500)
                            {
                                DScreen.AddChatBoardString(itemnames, Color.Blue, Color.White);
                                return;
                            }
                        }
                    }
                }
                if ((Math.Abs(MShare.g_MySelf.m_nCurrX - MShare.g_nMouseCurrX) <= 1) && (Math.Abs(MShare.g_MySelf.m_nCurrY - MShare.g_nMouseCurrY) <= 1))
                {
                    // 目标座标
                    if (boClick)
                    {
                        tdir = ClFunc.GetNextDirection(MShare.g_MySelf.m_nCurrX, MShare.g_MySelf.m_nCurrY, MShare.g_nMouseCurrX, MShare.g_nMouseCurrY);
                        if (CanNextAction() && ServerAcceptNextAction())
                        {
                            MShare.g_MySelf.SendMsg(Grobal2.CM_TURN, MShare.g_MySelf.m_nCurrX, MShare.g_MySelf.m_nCurrY, tdir, 0, 0, "", 0);
                        }
                    }
                }
                else
                {
                    MShare.g_ChrAction = TChrAction.caRun;
                    MShare.g_nTargetX = MShare.g_nMouseCurrX;
                    MShare.g_nTargetY = MShare.g_nMouseCurrY;
                    return;
                }
            }
            if (new ArrayList(Shift).Contains(System.Windows.Forms.Keys.LButton))
            {
                // 鼠标左键
                g_PlayScene.ProcMagic.NTargetX = -1;
                MShare.g_boAutoDig = false;
                MShare.g_boAutoSit = false;
                if (TimerAutoMove.Enabled)
                {
                    MShare.g_MySelf.m_nTagX = 0;
                    MShare.g_MySelf.m_nTagY = 0;
                    TimerAutoMove.Enabled = false;
                    MapUnit.g_MapPath = new Point[0];
                    MapUnit.g_MapPath = null;
                    DScreen.AddChatBoardString("停止自动移动", GetRGB(5), Color.White);
                }
                target = g_PlayScene.GetAttackFocusCharacter(X, Y, MShare.g_nDupSelection, ref sel, true);
                g_PlayScene.CXYfromMouseXY(X, Y, ref MShare.g_nMouseCurrX, ref MShare.g_nMouseCurrY);
                MShare.g_TargetCret = null;
                if ((MShare.g_UseItems[Grobal2.U_WEAPON].s.Name != "") && (target == null) && (MShare.g_MySelf.m_btHorse == 0))
                {
                    if (MShare.g_UseItems[Grobal2.U_WEAPON].s.Shape == 19)
                    {
                        tdir = ClFunc.GetNextDirection(MShare.g_MySelf.m_nCurrX, MShare.g_MySelf.m_nCurrY, MShare.g_nMouseCurrX, MShare.g_nMouseCurrY);
                        ClFunc.GetFrontPosition(MShare.g_MySelf.m_nCurrX, MShare.g_MySelf.m_nCurrY, tdir, ref nX, ref nY);
                        if (!Map.CanMove(nX, nY) || (new ArrayList(Shift).Contains(System.Windows.Forms.Keys.Shift)))
                        {
                            if (CanNextAction() && ServerAcceptNextAction() && CanNextHit())
                            {
                                MShare.g_MySelf.SendMsg(Grobal2.CM_HEAVYHIT, MShare.g_MySelf.m_nCurrX, MShare.g_MySelf.m_nCurrY, tdir, 0, 0, "", 0);
                            }
                            MShare.g_boAutoDig = true;
                            return;
                        }
                    }
                }
                if ((new ArrayList(Shift).Contains(System.Windows.Forms.Keys.Alt)) && (MShare.g_MySelf.m_btHorse == 0))
                {
                    tdir = ClFunc.GetNextDirection(MShare.g_MySelf.m_nCurrX, MShare.g_MySelf.m_nCurrY, MShare.g_nMouseCurrX, MShare.g_nMouseCurrY);
                    if (CanNextAction() && ServerAcceptNextAction())
                    {
                        target = g_PlayScene.ButchAnimal(MShare.g_nMouseCurrX, MShare.g_nMouseCurrY);
                        if (target != null)
                        {
                            SendButchAnimal(MShare.g_nMouseCurrX, MShare.g_nMouseCurrY, tdir, target.m_nRecogId);
                            MShare.g_MySelf.SendMsg(Grobal2.CM_SITDOWN, MShare.g_MySelf.m_nCurrX, MShare.g_MySelf.m_nCurrY, tdir, 0, 0, "", 0);
                            MShare.g_boAutoSit = true;
                            return;
                        }
                        else
                        {
                            SendButchAnimal(MShare.g_nMouseCurrX, MShare.g_nMouseCurrY, tdir, MShare.g_DetectItemMineID);
                            MShare.g_MySelf.SendMsg(Grobal2.CM_SITDOWN, MShare.g_MySelf.m_nCurrX, MShare.g_MySelf.m_nCurrY, tdir, 0, 0, "", 0);
                            MShare.g_boAutoSit = true;
                            return;
                        }
                        // g_MySelf.SendMsg(CM_SITDOWN, g_MySelf.m_nCurrX, g_MySelf.m_nCurrY, tdir, 0, 0, '', 0);
                    }
                    MShare.g_nTargetX = -1;
                }
                else
                {
                    if ((target != null) || (new ArrayList(Shift).Contains(System.Windows.Forms.Keys.Shift)))
                    {
                        MShare.g_nTargetX = -1;
                        if (target != null)
                        {
                            if (MShare.GetTickCount() - MShare.g_dwLastMoveTick > 500)
                            {
                                if (boClick && (target is THumActor) && ((THumActor)(target)).m_StallMgr.OnSale)
                                {
                                    // SendClientMessage(CM_CLICKNPC, target.m_nRecogId, 0, 0, 0);
                                    // NPC大窗口相关 Development 2019-01-14
                                    SendClientMessage(Grobal2.CM_CLICKNPC, target.m_nRecogId, 0, 0, HUtil32.BoolToInt(FrmDlg.DMerchantBigDlg.Visible));
                                    MShare.g_dwLastMoveTick = MShare.GetTickCount();
                                    return;
                                }
                                if (boClick && (target.m_btRace == Grobal2.RCC_MERCHANT))
                                {
                                    SendClientMessage(Grobal2.CM_CLICKNPC, target.m_nRecogId, 0, 0, HUtil32.BoolToInt(FrmDlg.DMerchantBigDlg.Visible));
                                    MShare.g_dwLastMoveTick = MShare.GetTickCount();
                                    return;
                                }
                            }
                            if (boClick && !target.m_boDeath && (MShare.g_MySelf.m_btHorse == 0) && (!(target is THumActor) || !((THumActor)(target)).m_StallMgr.OnSale))
                            {
                                MShare.g_TargetCret = target;
                                if (((target.m_btRace != 0) && (target.m_btRace != Grobal2.RCC_MERCHANT) && (target.m_sUserName.IndexOf("(") == 0)) || (MShare.g_gcGeneral[2]) || (new ArrayList(Shift).Contains(System.Windows.Forms.Keys.Shift)))
                                {
                                    AttackTarget(target);
                                }
                            }
                        }
                        else
                        {
                            // 骑马不允许操作
                            if ((MShare.g_MySelf.m_btHorse == 0))
                            {
                                tdir = ClFunc.GetNextDirection(MShare.g_MySelf.m_nCurrX, MShare.g_MySelf.m_nCurrY, MShare.g_nMouseCurrX, MShare.g_nMouseCurrY);
                                if (CanNextAction() && ServerAcceptNextAction() && CanNextHit())
                                {
                                    if (MShare.g_boNextTimeSmiteWideHit2 && (MShare.g_MySelf.m_Abil.MP >= 1))
                                    {
                                        MShare.g_boNextTimeSmiteWideHit2 = false;
                                        nHitMsg = Grobal2.CM_SMITEWIDEHIT2;
                                    }
                                    else if (MShare.g_boNextTimeSmiteLongHit3 && (MShare.g_MySelf.m_Abil.MP >= 1) && TargetInSwordLongAttackRangeA(tdir))
                                    {
                                        MShare.g_boNextTimeSmiteLongHit3 = false;
                                        nHitMsg = Grobal2.CM_SMITELONGHIT3;
                                    }
                                    else if (MShare.g_boNextTimeSmiteLongHit && (MShare.g_MySelf.m_Abil.MP >= 7) && TargetInSwordLongAttackRangeA(tdir))
                                    {
                                        MShare.g_boNextTimeSmiteLongHit = false;
                                        nHitMsg = Grobal2.CM_SMITELONGHIT;
                                    }
                                    else if (MShare.g_boNextTimeSmiteLongHit2 && (MShare.g_MySelf.m_Abil.MP >= 7) && TargetInSwordLongAttackRangeX(tdir))
                                    {
                                        MShare.g_boNextTimeSmiteLongHit2 = false;
                                        nHitMsg = Grobal2.CM_SMITELONGHIT2;
                                    }
                                    else if (MShare.g_boNextTimePursueHit && (MShare.g_MySelf.m_Abil.MP >= 7) && TargetInSwordLongAttackRangeX(tdir))
                                    {
                                        MShare.g_boNextTimePursueHit = false;
                                        nHitMsg = Grobal2.CM_PURSUEHIT;
                                    }
                                    else if (MShare.g_boCanSLonHit && (MShare.g_MySelf.m_Abil.MP >= 7) && TargetInSwordLongAttackRangeX(tdir))
                                    {
                                        MShare.g_boCanSLonHit = false;
                                        nHitMsg = Grobal2.CM_HERO_LONGHIT2;
                                    }
                                    else
                                    {
                                        if (MShare.g_boCanWideHit && (MShare.g_MySelf.m_Abil.MP >= 3) && (TargetInSwordWideAttackRange(tdir)))
                                        {
                                            nHitMsg = Grobal2.CM_WIDEHIT;
                                        }
                                        else if (MShare.g_boCanLongHit && (TargetInSwordLongAttackRange(tdir)))
                                        {
                                            nHitMsg = Grobal2.CM_LONGHIT;
                                        }
                                        else
                                        {
                                            nHitMsg = Grobal2.CM_HIT + (new System.Random(3)).Next();
                                        }
                                        if (MShare.g_boCanSquHit && (MShare.g_MySelf.m_Abil.MP >= 3) && (MShare.g_nSquHitPoint > 0))
                                        {
                                            if (MShare.g_boCanRunAllInWarZone)
                                            {
                                                ClFunc.GetFrontPosition(MShare.g_MySelf.m_nCurrX, MShare.g_MySelf.m_nCurrY, tdir, ref nX, ref nY);
                                                Actor = g_PlayScene.FindActorXY(nX, nY);
                                                if ((Actor != null) && !Actor.m_boDeath)
                                                {
                                                    nHitMsg = Grobal2.CM_SQUHIT;
                                                }
                                            }
                                            else
                                            {
                                                nHitMsg = Grobal2.CM_SQUHIT;
                                            }
                                        }
                                    }
                                    MShare.g_MySelf.SendMsg(nHitMsg, MShare.g_MySelf.m_nCurrX, MShare.g_MySelf.m_nCurrY, tdir, 0, 0, "", 0);
                                    // CheckSpeedCount();
                                }
                                MShare.g_dwLastAttackTick = MShare.GetTickCount();
                            }
                        }
                    }
                    else
                    {
                        if ((MShare.g_nMouseCurrX == MShare.g_MySelf.m_nCurrX) && (MShare.g_nMouseCurrY == MShare.g_MySelf.m_nCurrY))
                        {
                            if (boClick)
                            {
                                // tdir := GetNextDirection(g_MySelf.m_nCurrX, g_MySelf.m_nCurrY, g_nMouseCurrX, g_nMouseCurrY);
                                if (CanNextAction() && ServerAcceptNextAction())
                                {
                                    SendPickup();
                                }
                            }
                        }
                        else if (MShare.GetTickCount() - MShare.g_dwLastAttackTick > 900)
                        {
                            // 最后攻击操作停留指定时间才能移动
                            if (new ArrayList(Shift).Contains(System.Windows.Forms.Keys.Control))
                            {
                                MShare.g_ChrAction = TChrAction.caRun;
                            }
                            else
                            {
                                MShare.g_ChrAction = TChrAction.caWalk;
                            }
                            // if not TimerAutoPlay.Enabled then begin
                            MShare.g_nTargetX = MShare.g_nMouseCurrX;
                            MShare.g_nTargetY = MShare.g_nMouseCurrY;
                            // end;
                        }
                    }
                }
            }
        }

        private bool CheckDoorAction(int dx, int dy)
        {
            bool result;
            int door;
            result = false;
            door = Map.GetDoor(dx, dy);
            if (door > 0)
            {
                if (!Map.IsDoorOpen(dx, dy))
                {
                    SendClientMessage(Grobal2.CM_OPENDOOR, door, dx, dy, 0);
                    result = true;
                }
            }
            return result;
        }

        public void MouseTimerTimer(System.Object Sender, System.EventArgs _e1)
        {
            int i;
            int ii;
            int fixidx;
            Point pt;
            TActor target;
            if ((MShare.g_gcGeneral[1] || MShare.g_gcGeneral[9]) && (MShare.GetTickCount() - m_dwDuraWarningTick > 60 * 1000))
            {
                m_dwDuraWarningTick = MShare.GetTickCount();
                if ((MShare.g_MySelf != null) && !MShare.g_MySelf.m_boDeath)
                {
                    for (i = MShare.g_UseItems.GetUpperBound(0); i >= MShare.g_UseItems.GetLowerBound(0); i--)
                    {
                        if ((MShare.g_UseItems[i].s.Name != ""))
                        {
                            if (new ArrayList(new int[] { 7, 25 }).Contains(MShare.g_UseItems[i].s.StdMode))
                            {
                                continue;
                            }
                            if (MShare.g_UseItems[i].Dura < 1500)
                            {
                                if (MShare.g_gcGeneral[1])
                                {
                                    DScreen.AddSysMsgCenter(Format("你的[%s]持久已到底限，请及时修理！", new string[] { MShare.g_UseItems[i].s.Name }), Color.Lime, Color.Black, 10);
                                }
                                if (MShare.g_gcGeneral[9])
                                {
                                    fixidx = -1;
                                    for (ii = Grobal2.MAXBAGITEM - (1 + 0); ii >= 0; ii--)
                                    {
                                        if ((MShare.g_ItemArr[ii].s.NeedIdentify < 4) && (MShare.g_ItemArr[ii].s.Name != "") && (MShare.g_ItemArr[ii].s.StdMode == 2) && (MShare.g_ItemArr[ii].s.Shape == 9) && (MShare.g_ItemArr[ii].Dura > 0))
                                        {
                                            fixidx = ii;
                                            break;
                                        }
                                    }
                                    if (fixidx > -1)
                                    {
                                        EatItem(fixidx);
                                    }
                                    else
                                    {
                                        DScreen.AddSysMsgCenter(Format("你的修复神水已经用完，请及时补充！", new string[] { MShare.g_UseItems[i].s.Name }), Color.Lime, Color.Black, 10);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            if ((MShare.g_MySelf != null) && !MShare.g_MySelf.m_boDeath && (MShare.g_MySelf.m_nIPowerLvl > 5) && (MShare.g_MySelf.m_nIPower < 30) && (MShare.GetTickCount() - dwIPTick > 30 * 1000))
            {
                dwIPTick = MShare.GetTickCount();
                fixidx = -1;
                for (ii = Grobal2.MAXBAGITEM - (1 + 0); ii >= 0; ii--)
                {
                    if ((MShare.g_ItemArr[ii].s.NeedIdentify < 4) && (MShare.g_ItemArr[ii].s.Name != "") && (MShare.g_ItemArr[ii].s.StdMode == 2) && (MShare.g_ItemArr[ii].s.Shape == 13) && (MShare.g_ItemArr[ii].DuraMax > 0))
                    {
                        fixidx = ii;
                        break;
                    }
                }
                if (fixidx > -1)
                {
                    EatItem(fixidx);
                }
            }
            if (MShare.g_TargetCret != null)
            {
                if (ActionKey > 0)
                {
                    // ProcessKeyMessages;
                }
                else
                {
                    if (!MShare.g_TargetCret.m_boDeath && g_PlayScene.IsValidActor(MShare.g_TargetCret) && (!(MShare.g_TargetCret is THumActor) || !((THumActor)(MShare.g_TargetCret)).m_StallMgr.OnSale))
                    {
                        FillChar(keyvalue, sizeof(TKeyBoardState), '\0');
                        if (GetKeyboardState(keyvalue))
                        {
                            Shift = new object[] { };
                            if (((keyvalue[VK_SHIFT] && 0x80) != 0) || (MShare.g_gcGeneral[2]))
                            {
                                Shift = Shift + new System.Windows.Forms.Keys[] { System.Windows.Forms.Keys.Shift };
                            }
                            if (((MShare.g_TargetCret.m_btRace != 0) && (MShare.g_TargetCret.m_btRace != Grobal2.RCC_MERCHANT) && (MShare.g_TargetCret.m_sUserName.IndexOf("(") == 0)) || ((new ArrayList(Shift).Contains(System.Windows.Forms.Keys.Shift)) && (!FrmDlg.DEditChat.Visible)))
                            {
                                AttackTarget(MShare.g_TargetCret);
                            }
                        }
                    }
                    else
                    {
                        MShare.g_TargetCret = null;
                    }
                }
            }
            if ((MShare.g_MySelf != null) && (MShare.g_boAutoDig || MShare.g_boAutoSit))
            {
                if (CanNextAction() && ServerAcceptNextAction() && (MShare.g_boAutoSit || CanNextHit()))
                {
                    if (MShare.g_boAutoDig)
                    {
                        MShare.g_MySelf.SendMsg(Grobal2.CM_HIT + 1, MShare.g_MySelf.m_nCurrX, MShare.g_MySelf.m_nCurrY, MShare.g_MySelf.m_btDir, 0, 0, "", 0);
                    }
                    if (MShare.g_boAutoSit)
                    {
                        target = g_PlayScene.ButchAnimal(MShare.g_nMouseCurrX, MShare.g_nMouseCurrY);
                        if (target != null)
                        {
                            ii = ClFunc.GetNextDirection(MShare.g_MySelf.m_nCurrX, MShare.g_MySelf.m_nCurrY, MShare.g_nMouseCurrX, MShare.g_nMouseCurrY);
                            SendButchAnimal(MShare.g_nMouseCurrX, MShare.g_nMouseCurrY, ii, target.m_nRecogId);
                            MShare.g_MySelf.SendMsg(Grobal2.CM_SITDOWN, MShare.g_MySelf.m_nCurrX, MShare.g_MySelf.m_nCurrY, ii, 0, 0, "", 0);
                        }
                        else
                        {
                            ii = ClFunc.GetNextDirection(MShare.g_MySelf.m_nCurrX, MShare.g_MySelf.m_nCurrY, MShare.g_nMouseCurrX, MShare.g_nMouseCurrY);
                            SendButchAnimal(MShare.g_nMouseCurrX, MShare.g_nMouseCurrY, ii, MShare.g_DetectItemMineID);
                            MShare.g_MySelf.SendMsg(Grobal2.CM_SITDOWN, MShare.g_MySelf.m_nCurrX, MShare.g_MySelf.m_nCurrY, ii, 0, 0, "", 0);
                        }
                    }
                }
            }
            // 动自捡取
            if (MShare.g_boAutoPickUp && (MShare.g_MySelf != null) && (((MShare.GetTickCount() - MShare.g_dwAutoPickupTick) > MShare.g_dwAutoPickupTime)))
            {
                MShare.g_dwAutoPickupTick = MShare.GetTickCount();
                AutoPickUpItem();
            }
        }

        private void AutoPickUpItem()
        {
            TDropItem DropItem;
            if (ServerAcceptNextAction())
            {
                DropItem = g_PlayScene.GetXYDropItems(MShare.g_MySelf.m_nCurrX, MShare.g_MySelf.m_nCurrY);
                if (DropItem != null)
                {
                    if (MShare.g_boPickUpAll || DropItem.boPickUp)
                    {
                        SendPickup();
                    }
                }
            }
        }

        public void WaitMsgTimerTimer(System.Object Sender, System.EventArgs _e1)
        {
            if (MShare.g_MySelf == null)
            {
                return;
            }
            if (MShare.g_MySelf.ActionFinished())
            {
                WaitMsgTimer.Enabled = false;
                switch (WaitingMsg.Ident)
                {
                    case Grobal2.SM_CHANGEMAP:
                        MShare.g_boMapMovingWait = false;
                        MShare.g_boMapMoving = false;
                        if (MShare.g_nMDlgX != -1)
                        {
                            FrmDlg.CloseMDlg;
                            MShare.g_nMDlgX = -1;
                        }
                        if (MShare.g_nStallX != -1)
                        {
                            MShare.g_nStallX = -1;
                            FrmDlg.DBUserStallCloseClick(null, 0, 0);
                        }
                        ClearDropItems();
                        g_PlayScene.CleanObjects();
                        MShare.g_sMapTitle = "";
                        g_PlayScene.SendMsg(Grobal2.SM_CHANGEMAP, 0, WaitingMsg.Param, WaitingMsg.Tag, WaitingMsg.Series, 0, 0, WaitingStr);
                        MShare.g_MySelf.CleanCharMapSetting(WaitingMsg.Param, WaitingMsg.Tag);
                        MShare.g_nTargetX = -1;
                        MShare.g_TargetCret = null;
                        MShare.g_FocusCret = null;
                        break;
                }
            }
        }

        public void SelChrWaitTimerTimer(System.Object Sender, System.EventArgs _e1)
        {
            SelChrWaitTimer.Enabled = false;
            CmdTimer.Interval = 500;
            SendQueryChr();
        }

        public void ActiveCmdTimer(TTimerCommand cmd)
        {
            TimerCmd = cmd;
            CmdTimer.Enabled = true;
        }

        public void CmdTimerTimer(System.Object Sender, System.EventArgs _e1)
        {
            CmdTimer.Enabled = false;
            CmdTimer.Interval = 500;
            switch (TimerCmd)
            {
                case MShare.TTimerCommand.tcSoftClose:
                    CSocket.Socket.Close;
                    while (true)
                    {
                        if (!CSocket.Socket.Connected)
                        {
                            CmdTimer.Interval = 100;
                            ActiveCmdTimer(MShare.TTimerCommand.tcReSelConnect);
                            break;
                        }
                        Application.ProcessMessages;
                        if (Application.Terminated)
                        {
                            break;
                        }
                        WaitAndPass(10);
                    }
                    break;
                case MShare.TTimerCommand.tcReSelConnect:
                    ResetGameVariables();
                    this.Active = false;
                    while (true)
                    {
                        if (!CSocket.Socket.Connected)
                        {
                            DScreen.ChangeScene(IntroScn.TSceneType.stSelectChr);
                            if (!MShare.g_boDoFadeOut && !MShare.g_boDoFadeIn)
                            {
                                MShare.g_boDoFadeIn = true;
                                MShare.g_nFadeIndex = 0;
                            }
                            MShare.g_ConnectionStep = TConnectionStep.cnsReSelChr;
                            MShare.g_boQuerySelChar = true;
                            CSocket.Address = MShare.g_sSelChrAddr;
                            CSocket.Port = MShare.g_nSelChrPort;
                            this.Active = true;
                            break;
                        }
                        if (Application.Terminated)
                        {
                            break;
                        }
                        WaitAndPass(10);
                    }
                    break;
                case MShare.TTimerCommand.tcFastQueryChr:
                    SendQueryChr();
                    break;
            }
        }

        private void CloseAllWindows()
        {
            //SaveWayPoint();
            MShare.g_gcAss[0] = false;
            MShare.g_APTagget = null;
            MShare.g_AutoPicupItem = null;
            MShare.g_nAPStatus = -1;
            MShare.g_nTargetX = -1;
            MShare.g_boCanRunSafeZone = true;
            MShare.g_nEatIteminvTime = 200;
            MShare.g_SendSayListIdx = 0;
            MShare.g_SendSayList.Clear();
            MShare.ResetSeriesSkillVar();
            if (MShare.g_nMDlgX != -1)
            {
                MShare.g_nMDlgX = -1;
            }
            if (MShare.g_nStallX != -1)
            {
                MShare.g_nStallX = -1;
            }
            MShare.g_boItemMoving = false;
        }

        private void ClearDropItems()
        {
            int i;
            for (i = 0; i < MShare.g_DropedItemList.Count; i++)
            {
                this.Dispose(((TDropItem)(MShare.g_DropedItemList[i])));
            }
            MShare.g_DropedItemList.Clear();
        }

        private void ResetGameVariables()
        {
            int i;
            int ii;
            ArrayList List;
            CloseAllWindows();
            ClearDropItems();
            if (MShare.g_RareBoxWindow != null)
            {
                MShare.g_RareBoxWindow.Initialize();
            }
            for (i = Low(FrmDlg.m_MissionList); i <= High(FrmDlg.m_MissionList); i++)
            {
                List = FrmDlg.m_MissionList[i];
                for (ii = 0; ii < List.Count; ii++)
                {
                    this.Dispose(((TClientMission)(List[ii])));
                }
                List.Clear();
            }
            for (i = 0; i < MShare.g_MagicList.Count; i++)
            {
                this.Dispose(((TClientMagic)(MShare.g_MagicList[i])));
            }
            MShare.g_MagicList.Clear();
            for (i = 0; i < MShare.g_IPMagicList.Count; i++)
            {
                this.Dispose(((TClientMagic)(MShare.g_IPMagicList[i])));
            }
            MShare.g_IPMagicList.Clear();
            for (i = 0; i < MShare.g_HeroMagicList.Count; i++)
            {
                this.Dispose(((TClientMagic)(MShare.g_HeroMagicList[i])));
            }
            MShare.g_HeroMagicList.Clear();
            for (i = 0; i < MShare.g_HeroIPMagicList.Count; i++)
            {
                this.Dispose(((TClientMagic)(MShare.g_HeroIPMagicList[i])));
            }
            MShare.g_HeroIPMagicList.Clear();
            for (i = MShare.g_ShopListArr.GetLowerBound(0); i <= MShare.g_ShopListArr.GetUpperBound(0); i++)
            {
                List = MShare.g_ShopListArr[i];
                for (ii = 0; ii < List.Count; ii++)
                {
                    this.Dispose(((TShopItem)(List[ii])));
                }
                List.Clear();
            }
            MShare.g_boItemMoving = false;
            MShare.g_DetectItem.Item.Name = "";
            MShare.g_WaitingUseItem.Item.Item.Name = "";
            MShare.g_WaitingStallItem.Item.Item.Name = "";
            MShare.g_WaitingDetectItem.Item.Item.Name = "";
            MShare.g_OpenBoxItem.Item.Item.Name = "";
            MShare.g_EatingItem.Item.Name = "";
            MShare.g_nLastMapMusic = -1;
            MShare.g_nTargetX = -1;
            MShare.g_TargetCret = null;
            MShare.g_FocusCret = null;
            MShare.g_MagicTarget = null;
            ActionLock = false;
            m_boSupplyItem = false;
            m_nEatRetIdx = -1;
            MShare.g_GroupMembers.Clear();
            MShare.g_sGuildRankName = "";
            MShare.g_sGuildName = "";
            MShare.g_boMapMoving = false;
            WaitMsgTimer.Enabled = false;
            MShare.g_boMapMovingWait = false;
            MShare.g_boNextTimePowerHit = false;
            MShare.g_boCanLongHit = false;
            MShare.g_boCanWideHit = false;
            MShare.g_boCanCrsHit = false;
            MShare.g_boCanSquHit = false;
            MShare.g_boNextTimeFireHit = false;
            MShare.g_boCanSLonHit = false;
            MShare.g_boNextTimeTwinHit = false;
            MShare.g_boNextTimePursueHit = false;
            MShare.g_boNextTimeSmiteHit = false;
            MShare.g_boNextTimeRushHit = false;
            MShare.g_boNextTimeSmiteLongHit = false;
            MShare.g_boNextTimeSmiteLongHit3 = false;
            MShare.g_boNextTimeSmiteLongHit2 = false;
            MShare.g_boNextTimeSmiteWideHit = false;
            MShare.g_boNextTimeSmiteWideHit2 = false;
            MShare.InitClientItems();
            MShare.g_DetectItemMineID = 0;
            MShare.g_BAFirstShape = -1;
            MShare.g_BuildAcusesSuc = -1;
            MShare.g_BuildAcusesStep = 0;
            MShare.g_BuildAcusesProc = 0;
            MShare.g_BuildAcusesRate = 0;
            TSelectChrScene _wvar1 = SelectChrScene;
            FillChar(_wvar1.ChrArr, sizeof(TSelChar) * 2, '\0');
            _wvar1.ChrArr[0].FreezeState = true;
            _wvar1.ChrArr[1].FreezeState = true;
            g_PlayScene.ClearActors();
            ClearDropItems();
            EventMan.ClearEvents();
            g_PlayScene.CleanObjects();
            MaketSystem.Units.MaketSystem.g_Market.Clear();
        }

        private void ChangeServerClearGameVariables()
        {
            int i;
            int ii;
            ArrayList List;
            CloseAllWindows();
            ClearDropItems();
            for (i = Low(FrmDlg.m_MissionList); i <= High(FrmDlg.m_MissionList); i++)
            {
                List = FrmDlg.m_MissionList[i];
                for (ii = 0; ii < List.Count; ii++)
                {
                    this.Dispose(((TClientMission)(List[ii])));
                }
                List.Clear();
            }
            for (i = 0; i < MShare.g_MagicList.Count; i++)
            {
                this.Dispose(((TClientMagic)(MShare.g_MagicList[i])));
            }
            MShare.g_MagicList.Clear();
            for (i = 0; i < MShare.g_IPMagicList.Count; i++)
            {
                this.Dispose(((TClientMagic)(MShare.g_IPMagicList[i])));
            }
            MShare.g_IPMagicList.Clear();
            FillChar(MShare.g_MagicArr, sizeof(MShare.g_MagicArr), 0);
            for (i = 0; i < MShare.g_HeroMagicList.Count; i++)
            {
                this.Dispose(((TClientMagic)(MShare.g_HeroMagicList[i])));
            }
            MShare.g_HeroMagicList.Clear();
            for (i = 0; i < MShare.g_HeroIPMagicList.Count; i++)
            {
                this.Dispose(((TClientMagic)(MShare.g_HeroIPMagicList[i])));
            }
            MShare.g_HeroIPMagicList.Clear();
            for (i = MShare.g_ShopListArr.GetLowerBound(0); i <= MShare.g_ShopListArr.GetUpperBound(0); i++)
            {
                List = MShare.g_ShopListArr[i];
                for (ii = 0; ii < List.Count; ii++)
                {
                    this.Dispose(((TShopItem)(List[ii])));
                }
                List.Clear();
            }
            MShare.g_boItemMoving = false;
            MShare.g_DetectItem.Item.Name = "";
            MShare.g_WaitingUseItem.Item.Item.Name = "";
            MShare.g_WaitingStallItem.Item.Item.Name = "";
            MShare.g_WaitingDetectItem.Item.Item.Name = "";
            MShare.g_OpenBoxItem.Item.Item.Name = "";
            MShare.g_EatingItem.Item.Name = "";
            MShare.g_nLastMapMusic = -1;
            MShare.g_nTargetX = -1;
            MShare.g_TargetCret = null;
            MShare.g_FocusCret = null;
            MShare.g_MagicTarget = null;
            ActionLock = false;
            m_boSupplyItem = false;
            m_nEatRetIdx = -1;
            MShare.g_GroupMembers.Clear();
            MShare.g_sGuildRankName = "";
            MShare.g_sGuildName = "";
            MShare.g_boMapMoving = false;
            WaitMsgTimer.Enabled = false;
            MShare.g_boMapMovingWait = false;
            MShare.g_boNextTimePowerHit = false;
            MShare.g_boCanLongHit = false;
            MShare.g_boCanWideHit = false;
            MShare.g_boCanCrsHit = false;
            MShare.g_boCanSquHit = false;
            MShare.g_boNextTimeFireHit = false;
            MShare.g_boCanSLonHit = false;
            MShare.g_boNextTimeTwinHit = false;
            MShare.g_boNextTimePursueHit = false;
            MShare.g_boNextTimeSmiteHit = false;
            MShare.g_boNextTimeRushHit = false;
            MShare.g_boNextTimeSmiteLongHit = false;
            MShare.g_boNextTimeSmiteLongHit3 = false;
            MShare.g_boNextTimeSmiteLongHit2 = false;
            MShare.g_boNextTimeSmiteWideHit = false;
            MShare.g_boNextTimeSmiteWideHit2 = false;
            ClearDropItems();
            EventMan.ClearEvents();
            g_PlayScene.CleanObjects();
        }

        public void CSocketConnect(Object Sender, TCustomWinSocket Socket)
        {
            MShare.g_boServerConnected = true;
            // cnsIntro
            if (MShare.g_ConnectionStep == TConnectionStep.cnsLogin)
            {
                DScreen.ChangeScene(IntroScn.TSceneType.stLogin);
                if (!MShare.g_boDoFadeOut && !MShare.g_boDoFadeIn)
                {
                    // g_boDoFadeOut := True;
                    MShare.g_boDoFadeIn = true;
                    MShare.g_nFadeIndex = 0;
                }
            }
            if (MShare.g_ConnectionStep == TConnectionStep.cnsSelChr)
            {
                LoginScene.OpenLoginDoor();
                SelChrWaitTimer.Interval = 300;
                SelChrWaitTimer.Enabled = true;
            }
            if (MShare.g_ConnectionStep == TConnectionStep.cnsReSelChr)
            {
                while (true)
                {
                    if (CSocket.Socket.Connected)
                    {
                        CmdTimer.Interval = 150;
                        ActiveCmdTimer(MShare.TTimerCommand.tcFastQueryChr);
                        break;
                    }
                    Application.ProcessMessages;
                    if (Application.Terminated)
                    {
                        break;
                    }
                    WaitAndPass(10);
                }
            }
            if (MShare.g_ConnectionStep == TConnectionStep.cnsPlay)
            {
                if (!MShare.g_boServerChanging)
                {
                    ClFunc.ClearBag();
                    // HeroClearBag();
                    DScreen.ClearChatBoard();
                    DScreen.ChangeScene(IntroScn.TSceneType.stLoginNotice);
                    if (!MShare.g_boDoFadeOut && !MShare.g_boDoFadeIn)
                    {
                        // 由暗变亮，进入登录公告界面。
                        MShare.g_boDoFadeIn = true;
                        MShare.g_nFadeIndex = 0;
                    }
                }
                else
                {
                    ChangeServerClearGameVariables();
                }
                SendRunLogin();
            }
            SocStr = "";
            BufferStr = "";
            TimerPacket.Enabled = true;
        }

        public void CSocketDisconnect(Object Sender, TCustomWinSocket Socket)
        {
            MShare.g_boServerConnected = false;
            if (MShare.g_SoftClosed)
            {
                MShare.g_SoftClosed = false;
                ActiveCmdTimer(MShare.TTimerCommand.tcReSelConnect);
            }
            // (g_ConnectionStep = cnsIntro)
            else if ((DScreen.CurrentScene == LoginScene) && !MShare.g_boSendLogin)
            {
                DebugOutStr("游戏连接已关闭...");
                // Close;
            }
            TimerPacket.Enabled = false;
        }

        public void CSocketError(Object Sender, TCustomWinSocket Socket, TErrorEvent ErrorEvent, ref int ErrorCode)
        {
            ErrorCode = 0;
            Socket.Close;
        }

        public void CSocketRead(Object Sender, TCustomWinSocket Socket)
        {
            int n;
            string data;
            string data2;
            data = Socket.ReceiveText;
            if (data != "")
            {
                n = data.IndexOf("*");
                if (n > 0)
                {
                    data2 = data.Substring(1 - 1, n - 1);
                    data = data2 + data.Substring(n + 1 - 1, data.Length);
                    // CSocket.Socket.SendText('*');
                    CSocket.Socket.SendBuf(activebuf, 1);
                }
                SocStr = SocStr + data;
            }
        }

        public void SendSocket(string sendstr)
        {
            if (CSocket.Socket.Connected)
            {
                // Code,
                CSocket.Socket.SendText(Format(("#1%s!" as string), new string[] { sendstr }));
            }
        }

        public void SendClientMessage(int msg, int Recog, int param, int tag, int series)
        {
            ClientPacket dMsg;
            dMsg = Grobal2.MakeDefaultMsg(msg, Recog, param, tag, series);
            SendSocket(EDcode.EncodeMessage(dMsg));
        }

        public void SendShoping(string sItemName)
        {
            ClientPacket msg;
            msg = Grobal2.MakeDefaultMsg(Grobal2.CM_BUYSHOPITEM, 0, 0, 0, 0);
            SendSocket(EDcode.EncodeMessage(msg) + EDcode.EncodeString(sItemName));
        }

        public void SendPresend(string sPlayer, string sItemName)
        {
            ClientPacket msg;
            msg = Grobal2.MakeDefaultMsg(Grobal2.CM_SHOPPRESEND, MShare.g_MySelf.m_nRecogId, 0, 0, 0);
            SendSocket(EDcode.EncodeMessage(msg) + EDcode.EncodeString(sPlayer + "/" + sItemName));
        }

        // 发送账号与密码
        public void SendLogin(string uid, string passwd)
        {
            ClientPacket msg;
            LoginID = uid;
            LoginPasswd = passwd;
            msg = Grobal2.MakeDefaultMsg(Grobal2.CM_IDPASSWORD, 0, 0, 0, 0);
            SendSocket(EDcode.EncodeMessage(msg) + EDcode.EncodeString(uid + "/" + passwd));
            // 20090309 增加服务名
            MShare.g_boSendLogin = true;
        }

        public void SendNewAccount(TUserEntry ue, TUserEntryAdd ua)
        {
            string ss;
            int iLen;
            ClientPacket msg;
            MakeNewId = ue.sAccount;
            msg = Grobal2.MakeDefaultMsg(Grobal2.CM_ADDNEWUSER, 0, 0, 0, 0);
            Move(msg, FTempBuffer[0], 12);
            Move(ue, FTempBuffer[12], sizeof(TUserEntry));
            Move(ua, FTempBuffer[12 + sizeof(TUserEntry)], sizeof(TUserEntryAdd));
            iLen = EDcode.EncodeBuf((int)FTempBuffer, sizeof(TUserEntry) + sizeof(TUserEntryAdd) + 12, (int)FSendBuffer);
            ss.Length = iLen;
            Move(FSendBuffer[0], ss[1], iLen);
            SendSocket(ss);
        }

        public void SendBuildAcus(TClientRefineItem[] cr)
        {
            ClientPacket DefMsg;
            DefMsg = Grobal2.MakeDefaultMsg(Grobal2.CM_BUILDACUS, 0, 0, 0, 0);
            SendSocket(EDcode.EncodeMessage(DefMsg) + EDcode.EncodeBuffer(cr, sizeof(Grobal2.TClientRefineItem)));
        }

        public void SendSelectServer(string svname)
        {
            string ss;
            int iLen;
            ClientPacket msg;
            string sAnsiChar;
            sAnsiChar = (svname as string);
            msg = Grobal2.MakeDefaultMsg(Grobal2.CM_SELECTSERVER, 0, 0, 0, 0);
            Move(msg, FTempBuffer[0], 12);
            iLen = sAnsiChar.Length;
            Move(sAnsiChar[1], FTempBuffer[12], iLen);
            iLen = EDcode.EncodeBuf((int)FTempBuffer, iLen + 12, (int)FSendBuffer);
            ss.Length = iLen;
            Move(FSendBuffer[0], ss[1], iLen);
            SendSocket(ss);
        }

        public void SendChgPw(string ID, string passwd, string newpasswd)
        {
            string S;
            string ss;
            int iLen;
            ClientPacket msg;
            msg = Grobal2.MakeDefaultMsg(Grobal2.CM_CHANGEPASSWORD, 0, 0, 0, 0);
            Move(msg, FTempBuffer[0], 12);
            S = (ID + "\09" + passwd + "\09" + newpasswd as string);
            iLen = S.Length;
            Move(S[1], FTempBuffer[12], iLen);
            iLen = EDcode.EncodeBuf((int)FTempBuffer, iLen + 12, (int)FSendBuffer);
            ss.Length = iLen;
            Move(FSendBuffer[0], ss[1], iLen);
            SendSocket(ss);
        }

        public void SendNewChr(string uid, string uname, string shair, string sjob, string ssex)
        {
            ClientPacket msg;
            msg = Grobal2.MakeDefaultMsg(Grobal2.CM_NEWCHR, 0, 0, 0, 0);
            SendSocket(EDcode.EncodeMessage(msg) + EDcode.EncodeString(uid + "/" + uname + "/" + shair + "/" + sjob + "/" + ssex));
        }

        public void SendQueryChr()
        {
            ClientPacket msg;
            msg = Grobal2.MakeDefaultMsg(Grobal2.CM_QUERYCHR, 0, 0, 0, 0);
            SendSocket(EDcode.EncodeMessage(msg) + EDcode.EncodeString(LoginID + "/" + (Certification).ToString()));
        }

        public void SendDelChr(string chrname)
        {
            ClientPacket msg;
            msg = Grobal2.MakeDefaultMsg(Grobal2.CM_DELCHR, 0, 0, 0, 0);
            SendSocket(EDcode.EncodeMessage(msg) + EDcode.EncodeString(chrname));
        }

        public void SendSelChr(string chrname)
        {
            ClientPacket msg;
            m_sCharName = chrname;
            msg = Grobal2.MakeDefaultMsg(Grobal2.CM_SELCHR, 0, 0, 0, 0);
            SendSocket(EDcode.EncodeMessage(msg) + EDcode.EncodeString(LoginID + "/" + chrname));
        }

        // 发送登录消息
        public void SendRunLogin()
        {
            string sSendMsg;
            THardwareHeader HardwareHeader;
            int KeyLen;
            int KeyPos;
            int offset;
            string Dest;
            int SrcPos;
            int SrcAsc;
            int Range;
            string Src;
            string Key;
            HardwareHeader.dwMagicCode = 0x13F13F13;
            try
            {
                Src = uSMBIOS.GetHWID().Trim();
                HardwareHeader.xMd5Digest = MD5.MD5String(Src);
                Dest = "";
                Key = "legendsoft";
                KeyLen = Key.Length;
                KeyPos = 0;
                Range = 256;
                Randomize;
                offset = (new System.Random(Range)).Next();
                Dest = Format("%1.2x", new int[] { offset });
                for (SrcPos = 0; SrcPos < sizeof(HardwareHeader); SrcPos++)
                {
                    SrcAsc = ((int)((HardwareHeader as string)[SrcPos]) + offset) % 255;
                    if (KeyPos < KeyLen)
                    {
                        KeyPos = KeyPos + 1;
                    }
                    else
                    {
                        KeyPos = 1;
                    }
                    SrcAsc = SrcAsc ^ (int)(Key[KeyPos]);
                    Dest = Dest + Format("%1.2x", new int[] { SrcAsc });
                    offset = SrcAsc;
                }
            }
            catch
            {
            }
            sSendMsg = Format("**%s/%s/%d/%d/%d/%s", new string[] { LoginID, m_sCharName, Certification, MShare.CLIENT_VERSION_NUMBER, MShare.RUNLOGINCODE, Dest });
            SendSocket(EDcode.EncodeString(sSendMsg));
        }

        public void SendSay(string Str)
        {
            string sx;
            string sy;
            string param;
            int X;
            int Y;
            ClientPacket msg;
            const string sam = "/move";
            if (Str != "")
            {
                if (HUtil32.CompareLStr(Str, "/cmd", "/cmd".Length))
                {
                    ProcessCommand(Str);
                    return;
                }
                if (HUtil32.CompareLStr(Str, sam, sam.Length))
                {
                    param = Str.Substring(sam.Length + 1 - 1, Str.Length - sam.Length);
                    if (param != "")
                    {
                        sy = HUtil32.GetValidStr3(param, ref sx, new string[] { " ", ":", ",", "\09" });
                        if ((sx != "") && (sy != ""))
                        {
                            X = Convert.ToInt32(sx);
                            Y = Convert.ToInt32(sy);
                            if ((X > 0) && (Y > 0))
                            {
                                MShare.g_MySelf.m_nTagX = X;
                                MShare.g_MySelf.m_nTagY = Y;
                                if (!g_PathBusy)
                                {
                                    g_PathBusy = true;
                                    try
                                    {
                                        Map.LoadMapData();
                                        MapUnit.g_MapPath = Map.FindPath(MShare.g_MySelf.m_nCurrX, MShare.g_MySelf.m_nCurrY, MShare.g_MySelf.m_nTagX, MShare.g_MySelf.m_nTagY, 0);
                                        if (MapUnit.g_MapPath != null)
                                        {
                                            g_MoveStep = 1;
                                            TimerAutoMove.Enabled = true;
                                            DScreen.AddChatBoardString(Format("自动移动至坐标(%d:%d)，点击鼠标任意键停止……", new int[] { MShare.g_MySelf.m_nTagX, MShare.g_MySelf.m_nTagY }), GetRGB(5), Color.White);
                                        }
                                        else
                                        {
                                            TimerAutoMove.Enabled = false;
                                            DScreen.AddChatBoardString(Format("自动移动坐标点(%d:%d)不可到达", new int[] { MShare.g_MySelf.m_nTagX, MShare.g_MySelf.m_nTagY }), GetRGB(5), Color.White);
                                            MShare.g_MySelf.m_nTagX = 0;
                                            MShare.g_MySelf.m_nTagY = 0;
                                        }
                                    }
                                    finally
                                    {
                                        g_PathBusy = false;
                                    }
                                }
                            }
                        }
                    }
                    return;
                }
                if (Str == "/debug check")
                {
                    g_boShowMemoLog = !g_boShowMemoLog;
                    g_PlayScene.MemoLog.Clear();
                    g_PlayScene.MemoLog.Visible = g_boShowMemoLog;
                    return;
                }
                msg = Grobal2.MakeDefaultMsg(Grobal2.CM_SAY, 0, 0, 0, 0);
                SendSocket(EDcode.EncodeMessage(msg) + EDcode.EncodeString(Str));
                if (Str[0] == '/')
                {
                    DScreen.AddChatBoardString(Str, GetRGB(180), Color.White);
                    HUtil32.GetValidStr3(Str.Substring(2 - 1, Str.Length - 1), ref WhisperName, new string[] { " " });
                }
            }
        }

        public void SendActMsg(int ident, int X, int Y, int dir)
        {
            ClientPacket msg;
            msg = Grobal2.MakeDefaultMsg(ident, HUtil32.Makelong(X, Y), 0, dir, 0);
            SendSocket(EDcode.EncodeMessage(msg));
            ActionLock = true;
            ActionLockTime = MShare.GetTickCount();
        }

        public void SendSpellMsg(int ident, int X, int Y, int dir, int target, bool bLock)
        {
            ClientPacket msg;
            msg = Grobal2.MakeDefaultMsg(ident, HUtil32.Makelong(X, Y), HUtil32.LoWord(target), dir, HUtil32.HiWord(target));
            SendSocket(EDcode.EncodeMessage(msg));
            if (!bLock)
            {
                return;
            }
            ActionLock = true;
            ActionLockTime = MShare.GetTickCount();
        }

        public void SendQueryUserName(int targetid, int X, int Y)
        {
            ClientPacket msg;
            msg = Grobal2.MakeDefaultMsg(Grobal2.CM_QUERYUSERNAME, targetid, X, Y, 0);
            SendSocket(EDcode.EncodeMessage(msg));
        }

        public void SendItemSumCount(int OrgItemIndex, int ExItemIndex, int hero, string StrOrgItem, string StrExItem)
        {
            ClientPacket msg;
            msg = Grobal2.MakeDefaultMsg(Grobal2.CM_ITEMSUMCOUNT, OrgItemIndex, HUtil32.LoWord(ExItemIndex), HUtil32.HiWord(ExItemIndex), hero);
            SendSocket(EDcode.EncodeMessage(msg) + EDcode.EncodeString(StrOrgItem + "/" + StrExItem));
        }

        public void SendDro.Item.Itemtring Name, int itemserverindex, int dropcnt)
        {
            ClientPacket msg;
            msg = Grobal2.MakeDefaultMsg(Grobal2.CM_DROPITEM, itemserverindex, dropcnt, 0, 0);
            SendSocket(EDcode.EncodeMessage(msg) + EDcode.EncodeString(Name));
        }

        public void SendHeroDro.Item.Itemtring Name, int itemserverindex, int dropcnt)
        {
            ClientPacket msg;
            msg = Grobal2.MakeDefaultMsg(Grobal2.CM_HERODROPITEM, itemserverindex, dropcnt, 0, 0);
            SendSocket(EDcode.EncodeMessage(msg) + EDcode.EncodeString(Name));
        }

        public void SendDismantl.Item.Itemtring Name, int itemserverindex, int dropcnt, int hero)
        {
            ClientPacket msg;
            msg = Grobal2.MakeDefaultMsg(Grobal2.CM_DISMANTLEITEM, itemserverindex, dropcnt, hero, 0);
            SendSocket(EDcode.EncodeMessage(msg) + EDcode.EncodeString(Name));
        }

        public void SendPickup()
        {
            ClientPacket msg;
            msg = Grobal2.MakeDefaultMsg(Grobal2.CM_PICKUP, 0, MShare.g_MySelf.m_nCurrX, MShare.g_MySelf.m_nCurrY, 0);
            SendSocket(EDcode.EncodeMessage(msg));
        }

        public void SendHeroSetTarget()
        {
            ClientPacket msg;
            if ((MShare.g_MySelf.m_HeroObject != null) && (MShare.g_FocusCret != null))
            {
                msg = Grobal2.MakeDefaultMsg(Grobal2.CM_HEROSETTARGET, MShare.g_FocusCret.m_nRecogId, MShare.g_FocusCret.m_nCurrX, MShare.g_FocusCret.m_nCurrY, 0);
                SendSocket(EDcode.EncodeMessage(msg));
            }
        }

        public void SendHeroSetGuard()
        {
            ClientPacket msg;
            msg = Grobal2.MakeDefaultMsg(Grobal2.CM_HEROSETTARGET, 0, MShare.g_nMouseCurrX, MShare.g_nMouseCurrY, 0);
            SendSocket(EDcode.EncodeMessage(msg));
        }

        public void SendHeroJoinAttack()
        {
            ClientPacket msg;
            if (MShare.g_SeriesSkillFire)
            {
                return;
            }
            msg = Grobal2.MakeDefaultMsg(Grobal2.CM_HERORJOINTATTACK, 0, 0, 0, 0);
            SendSocket(EDcode.EncodeMessage(msg));
        }

        public void SendOpenBox(TOpenBoxItem OpenBoxItem)
        {
            ClientPacket msg;
            msg = Grobal2.MakeDefaultMsg(Grobal2.CM_OPENBOX, 0, 0, 0, 0);
            SendSocket(EDcode.EncodeMessage(msg) + EDcode.EncodeBuffer(OpenBoxItem, sizeof(TOpenBoxItem)));
        }

        public void SendSetSeriesSkill(int Index, int magid, int hero)
        {
            ClientPacket msg;
            msg = Grobal2.MakeDefaultMsg(Grobal2.CM_SETSERIESSKILL, Index, magid, 0, hero);
            SendSocket(EDcode.EncodeMessage(msg));
        }

        public void SendTakeOnItem(byte where, int itmindex, string itmname)
        {
            ClientPacket msg;
            msg = Grobal2.MakeDefaultMsg(Grobal2.CM_TAKEONITEM, itmindex, where, 0, 0);
            SendSocket(EDcode.EncodeMessage(msg) + EDcode.EncodeString(itmname));
        }

        public void HeroSendTakeOnItem(byte where, int itmindex, string itmname)
        {
            ClientPacket msg;
            msg = Grobal2.MakeDefaultMsg(Grobal2.CM_HEROTAKEONITEM, itmindex, where, 0, 0);
            SendSocket(EDcode.EncodeMessage(msg) + EDcode.EncodeString(itmname));
        }

        public void SendTakeOffItem(byte where, int itmindex, string itmname)
        {
            ClientPacket msg;
            msg = Grobal2.MakeDefaultMsg(Grobal2.CM_TAKEOFFITEM, itmindex, where, 0, 0);
            SendSocket(EDcode.EncodeMessage(msg) + EDcode.EncodeString(itmname));
        }

        public void HeroSendTakeOffItem(byte where, int itmindex, string itmname)
        {
            ClientPacket msg;
            msg = Grobal2.MakeDefaultMsg(Grobal2.CM_HEROTAKEOFFITEM, itmindex, where, 0, 0);
            SendSocket(EDcode.EncodeMessage(msg) + EDcode.EncodeString(itmname));
        }

        public void SendEat(int itmindex, string itmname, int nUnBindItem)
        {
            ClientPacket msg;
            msg = Grobal2.MakeDefaultMsg(Grobal2.CM_EAT, itmindex, 0, 0, nUnBindItem);
            SendSocket(EDcode.EncodeMessage(msg));
        }

        public void SendHeroEat(int itmindex, string itmname, int nType, int nUnBindItem)
        {
            ClientPacket msg;
            msg = Grobal2.MakeDefaultMsg(Grobal2.CM_HEROEAT, itmindex, nType, 0, nUnBindItem);
            SendSocket(EDcode.EncodeMessage(msg));
        }

        public void SendButchAnimal(int X, int Y, int dir, int actorid)
        {
            ClientPacket msg;
            msg = Grobal2.MakeDefaultMsg(Grobal2.CM_BUTCH, actorid, X, Y, dir);
            SendSocket(EDcode.EncodeMessage(msg));
        }

        public void SendMagicKeyChange(int magid, char keych)
        {
            ClientPacket msg;
            msg = Grobal2.MakeDefaultMsg(Grobal2.CM_MAGICKEYCHANGE, magid, (byte)keych, 0, 0);
            SendSocket(EDcode.EncodeMessage(msg));
        }

        public void SendHeroMagicKeyChange(int magid, char keych)
        {
            ClientPacket msg;
            msg = Grobal2.MakeDefaultMsg(Grobal2.CM_HEROMAGICKEYCHANGE, magid, (byte)keych, 0, 0);
            SendSocket(EDcode.EncodeMessage(msg));
        }

        public void SendMerchantDlgSelect(int merchant, string rstr)
        {
            const string sam = "@_automove ";
            int X;
            int Y;
            ClientPacket msg;
            string param;
            string sx;
            string sy;
            string sM;
            FrmDlg.CancelItemMoving;
            if (rstr.Length >= 2)
            {
                if (HUtil32.CompareLStr(rstr, sam, sam.Length))
                {
                    param = rstr.Substring(sam.Length + 1 - 1, rstr.Length - sam.Length);
                    if (param != "")
                    {
                        param = HUtil32.GetValidStr3(param, ref sx, new string[] { " ", ":", ",", "\09" });
                        sM = HUtil32.GetValidStr3(param, ref sy, new string[] { " ", ":", ",", "\09" });
                        if ((sx != "") && (sy != ""))
                        {
                            if ((sM != "") && ((MShare.g_sMapTitle).ToLower().CompareTo((sM).ToLower()) != 0))
                            {
                                // 自动移动
                                DScreen.AddChatBoardString(Format("到达 %s 之后才能使用自动走路", new string[] { sM }), Color.Blue, Color.White);
                                return;
                            }
                            X = Convert.ToInt32(sx);
                            Y = Convert.ToInt32(sy);
                            if ((X > 0) && (Y > 0))
                            {
                                MShare.g_MySelf.m_nTagX = X;
                                MShare.g_MySelf.m_nTagY = Y;
                                if (!g_PathBusy)
                                {
                                    g_PathBusy = true;
                                    try
                                    {
                                        Map.LoadMapData();
                                        MapUnit.g_MapPath = Map.FindPath(MShare.g_MySelf.m_nCurrX, MShare.g_MySelf.m_nCurrY, MShare.g_MySelf.m_nTagX, MShare.g_MySelf.m_nTagY, 0);
                                        if (MapUnit.g_MapPath != null)
                                        {
                                            g_MoveStep = 1;
                                            TimerAutoMove.Enabled = true;
                                            DScreen.AddChatBoardString(Format("自动移动至坐标(%d:%d)，点击鼠标任意键停止……", new int[] { MShare.g_MySelf.m_nTagX, MShare.g_MySelf.m_nTagY }), GetRGB(5), Color.White);
                                        }
                                        else
                                        {
                                            TimerAutoMove.Enabled = false;
                                            DScreen.AddChatBoardString(Format("自动移动坐标点(%d:%d)不可到达", new int[] { MShare.g_MySelf.m_nTagX, MShare.g_MySelf.m_nTagY }), GetRGB(5), Color.White);
                                            MShare.g_MySelf.m_nTagX = 0;
                                            MShare.g_MySelf.m_nTagY = 0;
                                        }
                                    }
                                    finally
                                    {
                                        g_PathBusy = false;
                                    }
                                }
                            }
                        }
                    }
                    return;
                }
                //if ((rstr[1] == "@") && (rstr[2] == "@"))
                //{
                //    if ((rstr).ToLower().CompareTo(("@@buildguildnow").ToLower()) == 0)
                //    {
                //        DebugOutStr("请输入行会名称：", new object[] { MessageBoxButtons.OK, MessageBoxButtons.AbortRetryIgnore });
                //    }
                //    else if ((rstr).ToLower().CompareTo(("@@ybbuylf").ToLower()) == 0)
                //    {
                //        DebugOutStr("请输入要兑换的灵符数量(1~1000)：", new object[] { MessageBoxButtons.OK, MessageBoxButtons.AbortRetryIgnore });
                //    }
                //    else if ((rstr).ToLower().CompareTo(("@@givezhh").ToLower()) == 0)
                //    {
                //        DebugOutStr("请输入你想赠给的角色名字以及数量，中间用空格分隔：", new object[] { MessageBoxButtons.OK, MessageBoxButtons.AbortRetryIgnore });
                //    }
                //    else if ((rstr).ToLower().CompareTo(("@@kachu_M2").ToLower()) == 0)
                //    {
                //        DebugOutStr("请输入你要开除的徒弟的角色名（若含有英文字符请区分大小写）：", new object[] { MessageBoxButtons.OK, MessageBoxButtons.AbortRetryIgnore });
                //    }
                //    else if ((rstr).ToLower().CompareTo(("@@sdmarry").ToLower()) == 0)
                //    {
                //        DebugOutStr("请输入你求婚对象的角色名（若含有英文字符请区分大小写）：", new object[] { MessageBoxButtons.OK, MessageBoxButtons.AbortRetryIgnore });
                //    }
                //    else if ((rstr).ToLower().CompareTo(("@@dealybme").ToLower()) == 0)
                //    {
                //        FrmDlg.QueryYbSell();
                //    }
                //    else if ((rstr).ToLower().CompareTo(("@@BuHero").ToLower()) == 0)
                //    {
                //        DebugOutStr("请输入英雄的名字：", new object[] { MessageBoxButtons.OK, MessageBoxButtons.AbortRetryIgnore });
                //    }
                //    else if ((rstr).ToLower().CompareTo(("@@BuHeroEx").ToLower()) == 0)
                //    {
                //        DebugOutStr("请输入英雄的名字：", new object[] { MessageBoxButtons.OK, MessageBoxButtons.AbortRetryIgnore });
                //    }
                //    else if ((rstr).ToLower().CompareTo(("@@BuHeroEx").ToLower()) == 0)
                //    {
                //        DebugOutStr("请输入英雄的名字：", new object[] { MessageBoxButtons.OK, MessageBoxButtons.AbortRetryIgnore });
                //    }
                //    else if ((rstr).ToLower().CompareTo(("@@TreasureIdentify").ToLower()) == 0)
                //    {
                //        FrmDlg.OpenDBTI();
                //    }
                //    else if ((rstr).ToLower().CompareTo(("@@ExchangeBook").ToLower()) == 0)
                //    {
                //        FrmDlg.ShowMDlg(0, "", "请把你要换成卷轴碎片的装备放在下面的物品栏中，我会帮你计算\\你可以换取多少个卷轴碎片。\\ \\<返回/@back>\\<关闭/@exit>");
                //        frmMain.ClientGetSendUserExchgBook(MShare.g_nCurMerchant);
                //    }
                //    else if ((rstr).ToLower().CompareTo(("@@SecretProperty").ToLower()) == 0)
                //    {
                //        FrmDlg.OpenDBSP();
                //    }
                //    else
                //    {
                //        DebugOutStr("请输入信息：", new object[] { MessageBoxButtons.OK, MessageBoxButtons.AbortRetryIgnore });
                //    }
                //    param = FrmDlg.DlgEditText.Trim();
                //    rstr = rstr + '\r' + param;
                //}
                //if ((rstr[1] == "@"))
                //{
                //    if ((rstr).ToLower().CompareTo(("@closewin").ToLower()) == 0)
                //    {
                //        FrmDlg.CloseMDlg;
                //    }
                //    else if ((rstr).ToLower().CompareTo(("@buildacus").ToLower()) == 0)
                //    {
                //        FrmDlg.OpenBuildAcusWin();
                //    }
                //}
            }
            msg = Grobal2.MakeDefaultMsg(Grobal2.CM_MERCHANTDLGSELECT, merchant, 0, 0, 0);
            SendSocket(EDcode.EncodeMessage(msg) + EDcode.EncodeString(rstr));
        }

        public void SendQueryPrice(int merchant, int itemindex, string itemname)
        {
            ClientPacket msg;
            msg = Grobal2.MakeDefaultMsg(Grobal2.CM_MERCHANTQUERYSELLPRICE, merchant, HUtil32.LoWord(itemindex), HUtil32.HiWord(itemindex), 0);
            SendSocket(EDcode.EncodeMessage(msg) + EDcode.EncodeString(itemname));
        }

        public void SendQueryExchgBook(int merchant, int itemindex, string itemname)
        {
            ClientPacket msg;
            msg = Grobal2.MakeDefaultMsg(Grobal2.CM_MERCHANTQUERYEXCHGBOOK, merchant, HUtil32.LoWord(itemindex), HUtil32.HiWord(itemindex), 0);
            SendSocket(EDcode.EncodeMessage(msg) + EDcode.EncodeString(itemname));
        }

        public void SendQueryRepairCost(int merchant, int itemindex, string itemname)
        {
            ClientPacket msg;
            msg = Grobal2.MakeDefaultMsg(Grobal2.CM_MERCHANTQUERYREPAIRCOST, merchant, HUtil32.LoWord(itemindex), HUtil32.HiWord(itemindex), 0);
            SendSocket(EDcode.EncodeMessage(msg) + EDcode.EncodeString(itemname));
        }

        public void SendSellItem(int merchant, int itemindex, string itemname, short count)
        {
            ClientPacket msg;
            msg = Grobal2.MakeDefaultMsg(Grobal2.CM_USERSELLITEM, merchant, HUtil32.LoWord(itemindex), HUtil32.HiWord(itemindex), count);
            SendSocket(EDcode.EncodeMessage(msg) + EDcode.EncodeString(itemname));
        }

        public void SendExchgBook(int merchant, int itemindex, string itemname, short count)
        {
            ClientPacket msg;
            // DScreen.AddChatBoardString(Format('%d:%d', [merchant, itemindex]), GetRGB(5), clWhite);
            msg = Grobal2.MakeDefaultMsg(Grobal2.CM_ExchangeBook, merchant, HUtil32.LoWord(itemindex), HUtil32.HiWord(itemindex), count);
            SendSocket(EDcode.EncodeMessage(msg) + EDcode.EncodeString(itemname));
        }

        public void SendRepairItem(int merchant, int itemindex, string itemname)
        {
            ClientPacket msg;
            msg = Grobal2.MakeDefaultMsg(Grobal2.CM_USERREPAIRITEM, merchant, HUtil32.LoWord(itemindex), HUtil32.HiWord(itemindex), 0);
            SendSocket(EDcode.EncodeMessage(msg) + EDcode.EncodeString(itemname));
        }

        public void SendStorageItem(int merchant, int itemindex, string itemname, short count)
        {
            ClientPacket msg;
            msg = Grobal2.MakeDefaultMsg(Grobal2.CM_USERSTORAGEITEM, merchant, HUtil32.LoWord(itemindex), HUtil32.HiWord(itemindex), count);
            SendSocket(EDcode.EncodeMessage(msg) + EDcode.EncodeString(itemname));
        }

        public void SendBindItem(int merchant, int itemindex, string itemname, short idx)
        {
            ClientPacket msg;
            msg = Grobal2.MakeDefaultMsg(Grobal2.CM_QUERYBINDITEM, merchant, HUtil32.LoWord(itemindex), HUtil32.HiWord(itemindex), idx);
            SendSocket(EDcode.EncodeMessage(msg) + EDcode.EncodeString(itemname));
        }

        public void SendSelectItem(int merchant, int itemindex, string itemname)
        {
            ClientPacket msg;
            msg = Grobal2.MakeDefaultMsg(Grobal2.CM_ITEMDLGSELECT, merchant, HUtil32.LoWord(itemindex), HUtil32.HiWord(itemindex), 0);
            SendSocket(EDcode.EncodeMessage(msg) + EDcode.EncodeString(itemname));
        }

        public void SendMaketSellItem(int merchant, int itemindex, string price, short count)
        {
            ClientPacket msg;
            msg = Grobal2.MakeDefaultMsg(Grobal2.CM_MARKET_SELL, merchant, HUtil32.LoWord(itemindex), HUtil32.HiWord(itemindex), count);
            SendSocket(EDcode.EncodeMessage(msg) + EDcode.EncodeString(price));
        }

        public void SendGetDetailItem(int merchant, int menuindex, string itemname)
        {
            ClientPacket msg;
            msg = Grobal2.MakeDefaultMsg(Grobal2.CM_USERGETDETAILITEM, merchant, menuindex, 0, 0);
            SendSocket(EDcode.EncodeMessage(msg) + EDcode.EncodeString(itemname));
        }

        public void SendGetMarketPageList(int merchant, int pagetype, string itemname)
        {
            // Market System..
            ClientPacket msg;
            msg = Grobal2.MakeDefaultMsg(Grobal2.CM_MARKET_LIST, merchant, pagetype, 0, 0);
            SendSocket(EDcode.EncodeMessage(msg) + EDcode.EncodeString(itemname));
        }

        public void SendBuyMarket(int merchant, int sellindex)
        {
            // Market System..
            ClientPacket msg;
            msg = Grobal2.MakeDefaultMsg(Grobal2.CM_MARKET_BUY, merchant, HUtil32.LoWord(sellindex), HUtil32.HiWord(sellindex), 0);
            SendSocket(EDcode.EncodeMessage(msg));
        }

        public void SendCancelMarket(int merchant, int sellindex)
        {
            // Market System..
            ClientPacket msg;
            msg = Grobal2.MakeDefaultMsg(Grobal2.CM_MARKET_CANCEL, merchant, HUtil32.LoWord(sellindex), HUtil32.HiWord(sellindex), 0);
            SendSocket(EDcode.EncodeMessage(msg));
        }

        // procedure TfrmMain.SendCaptchaRes(res: string);
        // var msg: TDefaultMessage;
        // begin
        // msg := MakeDefaultMsg(CM_CAPTCHAR, 0, 0, 0, 0);
        // SendSocket(EncodeMessage(msg) + EncodeString(res));
        // end;
        public void SendGetPayMarket(int merchant, int sellindex)
        {
            // Market System..
            ClientPacket msg;
            msg = Grobal2.MakeDefaultMsg(Grobal2.CM_MARKET_GETPAY, merchant, HUtil32.LoWord(sellindex), HUtil32.HiWord(sellindex), 0);
            SendSocket(EDcode.EncodeMessage(msg));
        }

        public void SendMarketClose()
        {
            // Market System..
            ClientPacket msg;
            msg = Grobal2.MakeDefaultMsg(Grobal2.CM_MARKET_CLOSE, 0, 0, 0, 0);
            SendSocket(EDcode.EncodeMessage(msg));
        }

        public void SendBuyItem(int merchant, int itemserverindex, string itemname, short conut)
        {
            ClientPacket msg;
            msg = Grobal2.MakeDefaultMsg(Grobal2.CM_USERBUYITEM, merchant, HUtil32.LoWord(itemserverindex), HUtil32.HiWord(itemserverindex), conut);
            SendSocket(EDcode.EncodeMessage(msg) + EDcode.EncodeString(itemname));
        }

        public void SendTakeBackStorageItem(int merchant, int itemserverindex, string itemname, short count)
        {
            ClientPacket msg;
            msg = Grobal2.MakeDefaultMsg(Grobal2.CM_USERTAKEBACKSTORAGEITEM, merchant, HUtil32.LoWord(itemserverindex), HUtil32.HiWord(itemserverindex), count);
            SendSocket(EDcode.EncodeMessage(msg) + EDcode.EncodeString(itemname));
        }

        public void SendMakeDrugItem(int merchant, string itemname)
        {
            ClientPacket msg;
            msg = Grobal2.MakeDefaultMsg(Grobal2.CM_USERMAKEDRUGITEM, merchant, 0, 0, 0);
            SendSocket(EDcode.EncodeMessage(msg) + EDcode.EncodeString(itemname));
        }

        public void SendDropGold(int dropgold)
        {
            ClientPacket msg;
            msg = Grobal2.MakeDefaultMsg(Grobal2.CM_DROPGOLD, dropgold, 0, 0, 0);
            SendSocket(EDcode.EncodeMessage(msg));
        }

        public void SendGroupMode(bool onoff)
        {
            ClientPacket msg;
            if (onoff)
            {
                // on
                msg = Grobal2.MakeDefaultMsg(Grobal2.CM_GROUPMODE, 0, 1, 0, 0);
            }
            else
            {
                msg = Grobal2.MakeDefaultMsg(Grobal2.CM_GROUPMODE, 0, 0, 0, 0);
            }
            // off
            SendSocket(EDcode.EncodeMessage(msg));
        }

        public void SendCreateGroup(string withwho)
        {
            ClientPacket msg;
            if (withwho != "")
            {
                msg = Grobal2.MakeDefaultMsg(Grobal2.CM_CREATEGROUP, 0, 0, 0, 0);
                SendSocket(EDcode.EncodeMessage(msg) + EDcode.EncodeString(withwho));
            }
        }

        public void SendWantMiniMap()
        {
            ClientPacket msg;
            msg = Grobal2.MakeDefaultMsg(Grobal2.CM_WANTMINIMAP, 0, 0, 0, 0);
            SendSocket(EDcode.EncodeMessage(msg));
        }

        public void SendGuildDlg()
        {
            ClientPacket msg;
            msg = Grobal2.MakeDefaultMsg(Grobal2.CM_OPENGUILDDLG, 0, 0, 0, 0);
            SendSocket(EDcode.EncodeMessage(msg));
        }

        public void SendDealTry()
        {
            ClientPacket msg;
            string who;
            msg = Grobal2.MakeDefaultMsg(Grobal2.CM_DEALTRY, 0, 0, 0, 0);
            SendSocket(EDcode.EncodeMessage(msg) + EDcode.EncodeString(who));
        }

        public void SendCancelDeal()
        {
            ClientPacket msg;
            msg = Grobal2.MakeDefaultMsg(Grobal2.CM_DEALCANCEL, 0, 0, 0, 0);
            SendSocket(EDcode.EncodeMessage(msg));
        }

        public void SendAddDealItem(TClientItem ci)
        {
            ClientPacket msg;
            msg = Grobal2.MakeDefaultMsg(Grobal2.CM_DEALADDITEM, ci.MakeIndex, 0, 0, ci.Dura);
            SendSocket(EDcode.EncodeMessage(msg) + EDcode.EncodeString(ci.s.Name));
        }

        public void SendDelDealItem(TClientItem ci)
        {
            ClientPacket msg;
            msg = Grobal2.MakeDefaultMsg(Grobal2.CM_DEALDELITEM, ci.MakeIndex, 0, 0, ci.Dura);
            SendSocket(EDcode.EncodeMessage(msg) + EDcode.EncodeString(ci.s.Name));
        }

        public void SendChangeDealGold(int gold)
        {
            ClientPacket msg;
            msg = Grobal2.MakeDefaultMsg(Grobal2.CM_DEALCHGGOLD, gold, 0, 0, 0);
            SendSocket(EDcode.EncodeMessage(msg));
        }

        public void SendDealEnd()
        {
            ClientPacket msg;
            msg = Grobal2.MakeDefaultMsg(Grobal2.CM_DEALEND, 0, 0, 0, 0);
            SendSocket(EDcode.EncodeMessage(msg));
        }

        public void SendAddGroupMember(string withwho)
        {
            ClientPacket msg;
            if (withwho != "")
            {
                msg = Grobal2.MakeDefaultMsg(Grobal2.CM_ADDGROUPMEMBER, 0, 0, 0, 0);
                SendSocket(EDcode.EncodeMessage(msg) + EDcode.EncodeString(withwho));
            }
        }

        public void SendDelGroupMember(string withwho)
        {
            ClientPacket msg;
            if (withwho != "")
            {
                msg = Grobal2.MakeDefaultMsg(Grobal2.CM_DELGROUPMEMBER, 0, 0, 0, 0);
                SendSocket(EDcode.EncodeMessage(msg) + EDcode.EncodeString(withwho));
            }
        }

        public void SendGuildHome()
        {
            ClientPacket msg;
            msg = Grobal2.MakeDefaultMsg(Grobal2.CM_GUILDHOME, 0, 0, 0, 0);
            SendSocket(EDcode.EncodeMessage(msg));
        }

        public void SendGuildMemberList()
        {
            ClientPacket msg;
            msg = Grobal2.MakeDefaultMsg(Grobal2.CM_GUILDMEMBERLIST, 0, 0, 0, 0);
            SendSocket(EDcode.EncodeMessage(msg));
        }

        public void SendGuildAddMem(string who)
        {
            ClientPacket msg;
            if (who.Trim() != "")
            {
                msg = Grobal2.MakeDefaultMsg(Grobal2.CM_GUILDADDMEMBER, 0, 0, 0, 0);
                SendSocket(EDcode.EncodeMessage(msg) + EDcode.EncodeString(who));
            }
        }

        public void SendGuildDelMem(string who)
        {
            ClientPacket msg;
            if (who.Trim() != "")
            {
                msg = Grobal2.MakeDefaultMsg(Grobal2.CM_GUILDDELMEMBER, 0, 0, 0, 0);
                SendSocket(EDcode.EncodeMessage(msg) + EDcode.EncodeString(who));
            }
        }

        public void SendGuildUpdateNotice(string notices)
        {
            ClientPacket msg;
            // zip
            msg = Grobal2.MakeDefaultMsg(Grobal2.CM_GUILDUPDATENOTICE, 0, 0, 0, 0);
            SendSocket(EDcode.EncodeMessage(msg) + EDcode.EncodeString(notices));
        }

        public void SendGuildUpdateGrade(string rankinfo)
        {
            ClientPacket msg;
            // zip
            msg = Grobal2.MakeDefaultMsg(Grobal2.CM_GUILDUPDATERANKINFO, 0, 0, 0, 0);
            SendSocket(EDcode.EncodeMessage(msg) + EDcode.EncodeString(rankinfo));
        }

        public void SendSpeedHackUser()
        {
            ClientPacket msg;
            msg = Grobal2.MakeDefaultMsg(Grobal2.CM_SPEEDHACKUSER, 0, 0, 0, 0);
            SendSocket(EDcode.EncodeMessage(msg));
        }

        public void SendAdjustBonus(int remain, TNakedAbility babil)
        {
            ClientPacket msg;
            msg = Grobal2.MakeDefaultMsg(Grobal2.CM_ADJUST_BONUS, remain, 0, 0, 0);
            SendSocket(EDcode.EncodeMessage(msg) + EDcode.EncodeBuffer(babil, sizeof(TNakedAbility)));
        }

        // procedure TfrmMain.SendPowerBlock();
        // var
        // msg: TDefaultMessage;
        // begin
        // msg := MakeDefaultMsg(CM_POWERBLOCK, 0, 0, 0, 0);
        // SendSocket(EncodeMessage(msg) + EncodeBuffer(@g_PowerBlock, SizeOf(TPowerBlock)));
        // end;
        public void SendFireSerieSkill()
        {
            ClientPacket msg;
            if ((MShare.g_MySelf == null))
            {
                return;
            }
            if (MShare.g_SeriesSkillFire)
            {
                return;
            }
            if (MShare.g_MySelf.m_boUseCboLib)
            {
                return;
            }
            if (MShare.g_MySelf.m_nIPower < 5)
            {
                if (MShare.GetTickCount() - MShare.g_IPointLessHintTick > 10000)
                {
                    MShare.g_IPointLessHintTick = MShare.GetTickCount();
                    DScreen.AddSysMsg("内力值不足...");
                }
                return;
            }
            if ((MShare.g_MySelf.m_nState & 0x04000000 == 0) && (MShare.g_MySelf.m_nState & 0x02000000 == 0))
            {
                if (MShare.GetTickCount() - MShare.g_SendFireSerieSkillTick > 1000)
                {
                    MShare.g_SendFireSerieSkillTick = MShare.GetTickCount();
                    msg = Grobal2.MakeDefaultMsg(Grobal2.CM_FIRESERIESSKILL, MShare.g_MySelf.m_nRecogId, 0, 0, 0);
                    SendSocket(EDcode.EncodeMessage(msg));
                }
            }
        }

        public bool ServerAcceptNextAction()
        {
            bool result;
            if ((MShare.g_MySelf != null) && MShare.g_MySelf.m_StallMgr.OnSale)
            {
                result = false;
                return result;
            }
            result = true;
            if (ActionLock)
            {
                if (MShare.GetTickCount() - ActionLockTime > 5 * 1000)
                {
                    ActionLock = false;
                    // Dec (WarningLevel);
                }
                result = false;
            }
            return result;
        }

        public bool CanNextAction()
        {
            bool result;
            if ((MShare.g_MySelf != null) && MShare.g_MySelf.m_StallMgr.OnSale)
            {
                result = false;
                return result;
            }
            if (!MShare.g_MySelf.m_boUseCboLib && (MShare.g_MySelf.IsIdle()) && (MShare.g_MySelf.m_nState & 0x04000000 == 0) && (MShare.g_MySelf.m_nState & 0x02000000 == 0) && (MShare.GetTickCount() - MShare.g_dwDizzyDelayStart > MShare.g_dwDizzyDelayTime))
            {
                result = true;
            }
            else
            {
                result = false;
            }
            return result;
        }

        /// <summary>
        /// 是否可以攻击，控制攻击速度
        /// </summary>
        /// <param name="settime"></param>
        /// <returns></returns>
        public bool CanNextHit(bool settime = false)
        {
            bool result;
            int NextHitTime;
            int LevelFastTime;
            if ((MShare.g_MySelf != null) && MShare.g_MySelf.m_StallMgr.OnSale)
            {
                result = false;
                return result;
            }
            LevelFastTime = HUtil32._MIN(370, (MShare.g_MySelf.m_Abil.Level * 14));
            LevelFastTime = HUtil32._MIN(800, LevelFastTime + MShare.g_MySelf.m_nHitSpeed * MShare.g_nItemSpeed);
            if (MShare.g_boSpeedRate)
            {
                if (MShare.g_MySelf.m_boAttackSlow)
                {
                    // 腕力超过时，减慢攻击速度
                    NextHitTime = MShare.g_nHitTime - LevelFastTime + 1500 - MShare.g_HitSpeedRate * 20;
                }
                else
                {
                    NextHitTime = MShare.g_nHitTime - LevelFastTime - MShare.g_HitSpeedRate * 20;
                }
            }
            else
            {
                if (MShare.g_MySelf.m_boAttackSlow)
                {
                    NextHitTime = MShare.g_nHitTime - LevelFastTime + 1500;
                }
                else
                {
                    NextHitTime = MShare.g_nHitTime - LevelFastTime;
                }
            }
            if (NextHitTime < 0)
            {
                NextHitTime = 0;
            }
            if (MShare.GetTickCount() - LastHitTick > (long)NextHitTime)
            {
                if (settime)
                {
                    LastHitTick = MShare.GetTickCount();
                }
                result = true;
            }
            else
            {
                result = false;
            }
            return result;
        }

        private void ActionFailed()
        {
            MShare.g_nTargetX = -1;
            MShare.g_nTargetY = -1;
            MShare.g_MySelf.m_boUseCboLib = false;
            ActionFailLock = true;
            ActionFailLockTime = MShare.GetTickCount();
            MShare.g_MySelf.MoveFail();
        }

        public bool IsUnLockAction()
        {
            bool result;
            // Action, adir: Integer
            if (ActionFailLock)
            {
                // 如果操作被锁定，则在指定时间后解锁
                if (((int)MShare.GetTickCount() - ActionFailLockTime) >= 1000)
                {
                    // blue 1000
                    ActionFailLock = false;
                }
            }
            if (ActionFailLock || (MShare.g_boMapMoving) || (MShare.g_boServerChanging))
            {
                result = false;
            }
            else
            {
                result = true;
            }
            return result;
        }

        public bool IsGroupMember(string uname)
        {
            bool result;
            result = MShare.g_GroupMembers.IndexOf(uname) >= 0;
            return result;
        }

        private void CheckSpeedHack(long rtime)
        {
            // var
            // cltime, svtime: Integer;
            // Str: string;
            return;
            // if g_dwFirstServerTime > 0 then begin
            // if (GetTickCount - g_dwFirstClientTime) > 1 * 60 * 60 * 1000 then begin
            // g_dwFirstServerTime := rtime;
            // g_dwFirstClientTime := GetTickCount;
            // end;
            // cltime := GetTickCount - g_dwFirstClientTime;
            // svtime := rtime - g_dwFirstServerTime + 3000;
            // if cltime > svtime then begin
            // Inc(g_nTimeFakeDetectCount);
            // if g_nTimeFakeDetectCount > 6 then begin
            // Str := 'Bad';
            // DebugOutStr('系统不稳定或网络状态极差，游戏被中止\' + '如有问题请联系游戏管理员', [mbOk]);
            // frmMain.Close;
            // end;
            // end else begin
            // Str := 'Good';
            // g_nTimeFakeDetectCount := 0;
            // end;
            // if g_boCheckSpeedHackDisplay then
            // DScreen.AddSysMsg(IntToStr(svtime) + ' - ' + IntToStr(cltime) + ' = ' + IntToStr(svtime - cltime) + ' ' + Str);
            // end else begin
            // g_dwFirstServerTime := rtime;
            // g_dwFirstClientTime := GetTickCount;
            // end;

        }

        private void RecalcAutoMovePath()
        {
            if ((MShare.g_MySelf.m_nTagX > 0) && (MShare.g_MySelf.m_nTagY > 0))
            {
                if (!g_PathBusy)
                {
                    g_PathBusy = true;
                    try
                    {
                        Map.ReLoadMapData();
                        MapUnit.g_MapPath = Map.FindPath(MShare.g_MySelf.m_nCurrX, MShare.g_MySelf.m_nCurrY, MShare.g_MySelf.m_nTagX, MShare.g_MySelf.m_nTagY, 0);
                        // g_MapPath := Map.FindPath(g_MySelf.m_nCurrX, g_MySelf.m_nCurrY);
                        if (MapUnit.g_MapPath != null)
                        {
                            g_MoveStep = 1;
                            TimerAutoMove.Enabled = true;
                        }
                        else
                        {
                            MShare.g_MySelf.m_nTagX = 0;
                            MShare.g_MySelf.m_nTagY = 0;
                            TimerAutoMove.Enabled = false;
                            DScreen.AddChatBoardString(Format("自动移动目标(%d:%d)被占据，不可到达", new int[] { MShare.g_MySelf.m_nTagX, MShare.g_MySelf.m_nTagY }), GetRGB(5), Color.White);
                        }
                    }
                    finally
                    {
                        g_PathBusy = false;
                    }
                }
            }
        }

        public string DecodeMessagePacket_ExtractUserName(string line)
        {
            string result;
            string uname = string.Empty;
            HUtil32.GetValidStr3(line, ref line, new string[] { "(", "!", "*", "/", ")" });
            HUtil32.GetValidStr3(line, ref uname, new string[] { " ", "=", ":" });
            if (uname != "")
            {
                if ((uname[0] == '/') || (uname[0] == '(') || (uname[0] == ' ') || (uname[0] == '['))
                {
                    uname = "";
                }
            }
            result = uname;
            return result;
        }

        private void DecodeMessagePacket(string datablock, int btPacket)
        {
            string head;
            string body;
            string body2;
            string tagstr;
            string data;
            string rdstr;
            string Str;
            string Str2;
            string str3;
            ClientPacket msg;
            ClientPacket msg2;
            ClientPacket DefMsg;
            TShortMessage sMsg;
            TMessageBodyW mbw;
            TCharDesc desc;
            TMessageBodyWL wl;
            short featureEx;
            int L;
            int i;
            int j;
            int n;
            int BLKSize;
            int param;
            int Sound;
            int cltime;
            int svtime;
            bool tempb;
            TActor Actor;
            TActor Actor2;
            TClEvent __event;
            string edBuf;
            int nad;
            int nFuncPos;
            TMagicEff meff;
            long ipExp;
            TGList List;
            TClientMission pMission;
            int MsgResult;
            TStallInfo StallInfo;
            TServerConfig svrcfg;
            if ((btPacket == 0) && (datablock[1] == "+"))
            {
                ProcessActMsg(datablock);
                return;
            }
            if (datablock.Length < Grobal2.DEFBLOCKSIZE)
            {
                return;
            }
            body = "";
            if (datablock.Length > Grobal2.DEFBLOCKSIZE)
            {
                body = datablock.Substring(Grobal2.DEFBLOCKSIZE + 1 - 1, datablock.Length - Grobal2.DEFBLOCKSIZE);
            }
            if (btPacket == 0)
            {
                head = datablock.Substring(1 - 1, Grobal2.DEFBLOCKSIZE);
                msg = EDcode.DecodeMessage(head);
            }
            else
            {
                body = body2;
            }
            if (MShare.g_MySelf == null)
            {
                switch (msg.Ident)
                {
                    case Grobal2.SM_GETBACKDELCHR:
                        switch (msg.Recog)
                        {
                            case 1:
                                SendQueryChr();
                                break;
                            case 2:
                                DebugOutStr("[失败] 角色并未被删除");
                                break;
                            case 3:
                                DebugOutStr("[失败] 你最多只能为一个帐号设置2个角色");
                                break;
                            case 4:
                                DebugOutStr("[失败] 没有找到被删除的角色");
                                break;
                            case 5:
                                DebugOutStr("[失败] 角色已被删除");
                                break;
                            default:
                                DebugOutStr("[失败] 角色数据读取失败，请稍候再试");
                                break;
                        }
                        break;
                    case Grobal2.SM_QUERYDELCHR:
                        if (msg.Recog == 1)
                        {
                            ClientGetDelCharList(msg.Series, body);
                        }
                        else
                        {
                            DebugOutStr("[失败] 没有找到被删除的角色");
                        }
                        break;
                    case Grobal2.SM_NEWID_SUCCESS:
                        DebugOutStr("您的帐号创建成功。\\" + "请妥善保管您的帐号和密码，\\并且不要因任何原因把帐号和密码告诉任何其他人。\\" + "如果忘记了密码,\\你可以通过我们的主页重新找回。");
                        break;
                    case Grobal2.SM_NEWID_FAIL:
                        switch (msg.Recog)
                        {
                            case 0:
                                DebugOutStr("帐号 \"" + MakeNewId + "\" 已被其他的玩家使用了。\\" + "请选择其它帐号名注册");
                                LoginScene.NewIdRetry(false);
                                break;
                            case -2:
                                DebugOutStr("此帐号名被禁止使用！");
                                break;
                            default:
                                DebugOutStr("帐号创建失败，请确认帐号是否包括空格、及非法字符！Code: " + (msg.Recog).ToString());
                                break;
                        }
                        break;
                    case Grobal2.SM_PASSWD_FAIL:
                        switch (msg.Recog)
                        {
                            case -1:
                                DebugOutStr("密码错误！");
                                break;
                            case -2:
                                DebugOutStr("密码输入错误超过3次，此帐号被暂时锁定，请稍候再登录！");
                                break;
                            case -3:
                                DebugOutStr("此帐号已经登录或被异常锁定，请稍候再登录！");
                                break;
                            case -4:
                                DebugOutStr("这个帐号访问失败！\\请使用其他帐号登录，\\或者申请付费注册");
                                break;
                            case -5:
                                DebugOutStr("这个帐号被锁定！");
                                break;
                            case -6:
                                DebugOutStr("请使用专用登陆器登陆游戏！");
                                break;
                            default:
                                DebugOutStr("此帐号不存在，或出现未知错误！");
                                break;
                        }
                        LoginScene.PassWdFail();
                        break;
                    case Grobal2.SM_NEEDUPDATE_ACCOUNT:
                        // //拌沥 沥焊甫 促矫 涝仿窍扼.
                        ClientGetNeedUpdateAccount(body);
                        break;
                    case Grobal2.SM_UPDATEID_SUCCESS:
                        DebugOutStr("您的帐号信息更新成功。\\" + "请妥善保管您的帐号和密码。\\" + "并且不要因任何原因把帐号和密码告诉任何其他人。\\" + "如果忘记了密码，你可以通过我们的主页重新找回。");
                        ClientGetSelectServer();
                        break;
                    case Grobal2.SM_UPDATEID_FAIL:
                        DebugOutStr("更新帐号失败！");
                        ClientGetSelectServer();
                        break;
                    case Grobal2.SM_PASSOK_SELECTSERVER:
                        ClientGetPasswordOK(msg, body);
                        break;
                    case Grobal2.SM_SELECTSERVER_OK:
                        ClientGetPasswdSuccess(body);
                        break;
                    case Grobal2.SM_QUERYCHR:
                        ClientGetReceiveChrs(body);
                        break;
                    case Grobal2.SM_QUERYCHR_FAIL:
                        MShare.g_boDoFastFadeOut = false;
                        MShare.g_boDoFadeIn = false;
                        MShare.g_boDoFadeOut = false;
                        DebugOutStr("服务器认证失败！");
                        this.Close();
                        break;
                    case Grobal2.SM_NEWCHR_SUCCESS:
                        SendQueryChr();
                        break;
                    case Grobal2.SM_NEWCHR_FAIL:
                        switch (msg.Recog)
                        {
                            case 0:
                                DebugOutStr("[错误信息] 输入的角色名称包含非法字符！ 错误代码 = 0");
                                break;
                            case 2:
                                DebugOutStr("[错误信息] 创建角色名称已被其他人使用！ 错误代码 = 2");
                                break;
                            case 3:
                                DebugOutStr("[错误信息] 您只能创建二个游戏角色！ 错误代码 = 3");
                                break;
                            case 4:
                                DebugOutStr("[错误信息] 创建角色时出现错误！ 错误代码 = 4");
                                break;
                            default:
                                DebugOutStr("[错误信息] 创建角色时出现未知错误！");
                                break;
                        }
                        break;
                    case Grobal2.SM_CHGPASSWD_SUCCESS:
                        DebugOutStr("密码修改成功");
                        break;
                    case Grobal2.SM_CHGPASSWD_FAIL:
                        switch (msg.Recog)
                        {
                            case -1:
                                DebugOutStr("输入的原始密码不正确！");
                                break;
                            case -2:
                                DebugOutStr("此帐号被锁定！");
                                break;
                            default:
                                DebugOutStr("输入的新密码长度小于四位！");
                                break;
                        }
                        break;
                    case Grobal2.SM_DELCHR_SUCCESS:
                        SendQueryChr();
                        break;
                    case Grobal2.SM_DELCHR_FAIL:
                        DebugOutStr("[错误信息] 删除游戏角色时出现错误！");
                        break;
                    case Grobal2.SM_STARTPLAY:
                        ClientGetStartPlay(body);
                        return;
                        break;
                    case Grobal2.SM_STARTFAIL:
                        MShare.g_boDoFastFadeOut = false;
                        DebugOutStr("此服务器满员！");
                        ClientGetSelectServer();
                        return;
                        break;
                    case Grobal2.SM_VERSION_FAIL:
                        MShare.g_boDoFastFadeOut = false;
                        DebugOutStr("游戏程序版本不正确，请下载最新版本游戏程序！");
                        this.Close();
                        return;
                        break;
                    case Grobal2.SM_OVERCLIENTCOUNT:
                        // 123456
                        MShare.g_boDoFastFadeOut = false;
                        DebugOutStr("客户端开启数量过多，连接被断开！！！");
                        this.Close();
                        return;
                        break;
                    case Grobal2.SM_CDVERSION_FAIL:
                        MShare.g_boDoFastFadeOut = false;
                        this.Close();
                        return;
                        break;
                    case Grobal2.SM_OUTOFCONNECTION:
                    case Grobal2.SM_NEWMAP:
                    case Grobal2.SM_LOGON:
                    case Grobal2.SM_RECONNECT:
                    case Grobal2.SM_SENDNOTICE:
                    case Grobal2.SM_DLGMSG:
                        break;
                    case Grobal2.SM_SENDTITLES:
                        ClientGetServerTitles(msg.Recog, body);
                        break;
                    case Grobal2.SM_MYTITLES:
                        ClientGetMyTitles(msg.Recog, body);
                        break;
                    default:
                        return;
                        break;
                }
            }
            if (MShare.g_boMapMoving)
            {
                if (msg.Ident == Grobal2.SM_CHANGEMAP)
                {
                    WaitingMsg = msg;
                    WaitingStr = EDcode.DeCodeString(body);
                    MShare.g_boMapMovingWait = true;
                    WaitMsgTimer.Enabled = true;
                }
                return;
            }
            switch (msg.Ident)
            {
                case Grobal2.SM_PLAYERCONFIG:
                    switch (msg.Recog)
                    {
                        case -1:
                            DScreen.AddChatBoardString("切换时装外显操作太快了！", Color.White, Color.Red);
                            break;
                    }
                    if (msg.Tag != 0)
                    {
                        FrmDlg.CheckBox_hShowFashion.Checked = HUtil32.LoWord(msg.Series) != 0;
                    }
                    else
                    {
                        FrmDlg.CheckBox_ShowFashion.Checked = HUtil32.LoWord(msg.Series) != 0;
                    }
                    if (msg.Recog > 0)
                    {
                        if (msg.Tag != 0)
                        {
                            if (FrmDlg.CheckBox_hShowFashion.Checked)
                            {
                                DScreen.AddChatBoardString("[英雄] 开启 时装外显！", GetRGB(219), Color.White);
                            }
                            else
                            {
                                DScreen.AddChatBoardString("[英雄] 关闭 时装外显！", GetRGB(219), Color.White);
                            }
                        }
                        else
                        {
                            if (FrmDlg.CheckBox_ShowFashion.Checked)
                            {
                                DScreen.AddChatBoardString("开启 时装外显！", GetRGB(219), Color.White);
                            }
                            else
                            {
                                DScreen.AddChatBoardString("关闭 时装外显！", GetRGB(219), Color.White);
                            }
                        }
                    }
                    break;
                case Grobal2.SM_NEWMAP:
                    MShare.g_sMapTitle = "";
                    Str = EDcode.DeCodeString(body);
                    g_PlayScene.SendMsg(Grobal2.SM_NEWMAP, 0, msg.Param, msg.Tag, msg.Series, 0, 0, Str);
                    break;
                case Grobal2.SM_LOGON:
                    MShare.g_dwFirstServerTime = 0;
                    MShare.g_dwFirstClientTime = 0;
                    EDcode.DecodeBuffer(body, wl);
                    g_PlayScene.SendMsg(Grobal2.SM_LOGON, msg.Recog, msg.Param, msg.Tag, msg.Series, wl.lParam1, wl.lParam2, "");
                    DScreen.ChangeScene(IntroScn.TSceneType.stPlayGame);
                    SendClientMessage(Grobal2.CM_WANTVIEWRANGE, HUtil32.Makelong(MShare.g_TileMapOffSetX, MShare.g_TileMapOffSetY), 0, 0, 0);
                    if (!MShare.g_boDoFadeOut && !MShare.g_boDoFadeIn)
                    {
                        MShare.g_boDoFadeIn = true;
                        MShare.g_nFadeIndex = 10;
                    }
                    SendClientMessage(Grobal2.CM_QUERYBAGITEMS, 1, 0, 0, 0);
                    if (msg.LoByte(msg.HUtil32.LoWord(wl.lTag1)) == 1)
                    {
                        MShare.g_boAllowGroup = true;
                    }
                    else
                    {
                        MShare.g_boAllowGroup = false;
                    }
                    MShare.g_boServerChanging = false;
                    if (MShare.g_wAvailIDDay > 0)
                    {
                        DScreen.AddChatBoardString("您当前通过包月帐号充值", GetRGB(219), Color.White);
                    }
                    else if (MShare.g_wAvailIPDay > 0)
                    {
                        DScreen.AddChatBoardString("您当前通过包月IP 充值", GetRGB(219), Color.White);
                    }
                    else if (MShare.g_wAvailIPHour > 0)
                    {
                        DScreen.AddChatBoardString("您当前通过计时IP 充值", GetRGB(219), Color.White);
                    }
                    else if (MShare.g_wAvailIDHour > 0)
                    {
                        DScreen.AddChatBoardString("您当前通过计时帐号充值", GetRGB(219), Color.White);
                    }
                    MShare.LoadUserConfig(m_sCharName);
                    MShare.LoadItemFilter2();
                    SendClientMessage(Grobal2.CM_HIDEDEATHBODY, MShare.g_MySelf.m_nRecogId, ((int)MShare.g_gcGeneral[8]), 0, 0);
                    break;
                case Grobal2.SM_SERVERCONFIG:
                    ClientGetServerConfig(msg, body);
                    break;
                case Grobal2.SM_SERVERCONFIG2:
                    EDcode.DecodeBuffer(EDcode.DeCodeString(body), svrcfg, sizeof(svrcfg));
                    MShare.g_boAutoSay = svrcfg.AutoSay;
                    MShare.g_boMutiHero = svrcfg.Reserved[0] != 0;
                    MShare.g_boSkill_114_MP = svrcfg.Reserved[1] != 0;
                    MShare.g_boSkill_68_MP = svrcfg.Reserved[2] != 0;
                    FrmDlg.DBAotoSay.Visible = MShare.g_boAutoSay;
                    if (msg.Series > 200)
                    {
                        MShare.g_nEatIteminvTime = msg.Series;
                    }
                    MShare.g_boForceNotViewFog = HiByte(msg.Param) != 0;
                    MShare.g_boOpenStallSystem = LoByte(msg.Param) != 0;
                    MShare.g_boAutoLongAttack = true;
                    MShare.g_boHero = true;
                    if (!MShare.g_boHero)
                    {
                        if (MShare.g_BuildBotTex == 0)
                        {
                            MShare.g_BuildBotTex = 1;
                        }
                    }
                    if (!MShare.g_boAutoLongAttack)
                    {
                        MShare.g_gcTec[10] = false;
                    }
                    break;
                case Grobal2.SM_SERVERCONFIG3:
#if CONFIGTEST
                    MShare.g_boSpeedRate = true;
                    MShare.g_boSpeedRateShow = MShare.g_boSpeedRate;
#else
                    if ((LoByte(msg.Series) > 0))
                    {
                        MShare.g_boSpeedRate = true;
                        MShare.g_boSpeedRateShow = false;
                        MShare.g_HitSpeedRate = HUtil32._MIN(68, LoByte(msg.Param));
                        MShare.g_MagSpeedRate = HUtil32._MIN(68, HiByte(msg.Param));
                        MShare.g_MoveSpeedRate = HUtil32._MIN(68, LoByte(msg.Tag));
                    }
                    break;
                case Grobal2.SM_RUNHUMAN:
#endif
                    MShare.g_boCanRunHuman = msg.Recog != 0;
                    break;
                case Grobal2.SM_INSAFEZONEFLAG:
                    MShare.g_boCanRunSafeZone = msg.Recog != 0;
                    break;
                case Grobal2.SM_RECONNECT:
                    ClientGetReconnect(body);
                    break;
                case Grobal2.SM_TIMECHECK_MSG:
                    CheckSpeedHack(msg.Recog);
                    break;
                case Grobal2.SM_AREASTATE:
                    MShare.g_nAreaStateValue = msg.Recog;
                    break;
                case Grobal2.SM_MAPDESCRIPTION:
                    ClientGetMapDescription(msg, body);
                    break;
                case Grobal2.SM_GAMEGOLDNAME:
                    ClientGetGameGoldName(msg, body);
                    break;
                case Grobal2.SM_ADJUST_BONUS:
                    ClientGetAdjustBonus(msg.Recog, body);
                    break;
                case Grobal2.SM_MYSTATUS:
                    MShare.g_nMyHungryState = msg.Param;
                    break;
                case Grobal2.SM_TURN:
                    n = HUtil32.GetCodeMsgSize(sizeof(TCharDesc) * 4 / 3);
                    if (body.Length > n)
                    {
                        body2 = body.Substring(n + 1 - 1, body.Length);
                        data = EDcode.DeCodeString(body2);
                        body2 = body.Substring(1 - 1, n);
                        Str = HUtil32.GetValidStr3(data, ref data, new string[] { "/" });
                    }
                    else
                    {
                        body2 = body;
                        data = "";
                    }
                    EDcode.DecodeBuffer(body2, desc, sizeof(TCharDesc));
                    // x
                    // y
                    // dir + light
                    g_PlayScene.SendMsg(Grobal2.SM_TURN, msg.Recog, msg.Param, msg.Tag, msg.Series, desc.Feature, desc.Status, "", desc.StatusEx);
                    if (data != "")
                    {
                        Actor = g_PlayScene.FindActor(msg.Recog);
                        if (Actor != null)
                        {
                            Actor.m_sDescUserName = HUtil32.GetValidStr3(data, ref Actor.m_sUserName, new string[] { "\\" });
                            Actor.m_sUserNameOffSet = HGECanvas.Units.HGECanvas.g_DXCanvas.TextWidth(Actor.m_sUserName) / 2;
                            if (Actor.m_sUserName.IndexOf("(") != 0)
                            {
                                HUtil32.ArrestStringEx(Actor.m_sUserName, "(", ")", ref data);
                                // DScreen.AddChatBoardString(data, clWhite, clRed);
                                if (data == MShare.g_MySelf.m_sUserName)
                                {
                                    j = 0;
                                    for (i = 0; i < MShare.g_MySelf.m_SlaveObject.Count; i++)
                                    {
                                        if (((TActor)(MShare.g_MySelf.m_SlaveObject[i])) == Actor)
                                        {
                                            j = 1;
                                            break;
                                        }
                                    }
                                    if (j == 0)
                                    {
                                        MShare.g_MySelf.m_SlaveObject.Add(Actor);
                                    }
                                    // if g_MySelf.m_SlaveObject <> Actor then
                                    // g_MySelf.m_SlaveObject := Actor;
                                }
                            }
                            Actor.m_btNameColor = HUtil32.Str_ToInt(Str, 0);
                            if (Actor.m_btRace == Grobal2.RCC_MERCHANT)
                            {
                                Actor.m_nNameColor = Color.Lime;
                            }
                            else
                            {
                                Actor.m_nNameColor = GetRGB(Actor.m_btNameColor);
                            }
                        }
                    }
                    break;
                case Grobal2.SM_FOXSTATE:
                    ClientGetFoxState(msg, body);
                    break;
                case Grobal2.SM_BACKSTEP:
                    n = HUtil32.GetCodeMsgSize(sizeof(TCharDesc) * 4 / 3);
                    if (body.Length > n)
                    {
                        body2 = body.Substring(n + 1 - 1, body.Length);
                        data = EDcode.DeCodeString(body2);
                        body2 = body.Substring(1 - 1, n);
                        Str = HUtil32.GetValidStr3(data, ref data, new string[] { "/" });
                    }
                    else
                    {
                        body2 = body;
                        data = "";
                    }
                    EDcode.DecodeBuffer(body2, desc, sizeof(TCharDesc));
                    // x
                    // y
                    // dir + light
                    g_PlayScene.SendMsg(Grobal2.SM_BACKSTEP, msg.Recog, msg.Param, msg.Tag, msg.Series, desc.Feature, desc.Status, "", desc.StatusEx);
                    if (data != "")
                    {
                        Actor = g_PlayScene.FindActor(msg.Recog);
                        if (Actor != null)
                        {
                            Actor.m_sDescUserName = HUtil32.GetValidStr3(data, ref Actor.m_sUserName, new string[] { "\\" });
                            Actor.m_sUserNameOffSet = HGECanvas.Units.HGECanvas.g_DXCanvas.TextWidth(Actor.m_sUserName) / 2;
                            Actor.m_btNameColor = HUtil32.Str_ToInt(Str, 0);
                            if (Actor.m_btRace == Grobal2.RCC_MERCHANT)
                            {
                                Actor.m_nNameColor = Color.Lime;
                            }
                            else
                            {
                                Actor.m_nNameColor = GetRGB(Actor.m_btNameColor);
                            }
                        }
                    }
                    break;
                case Grobal2.SM_SPACEMOVE_HIDE:
                case Grobal2.SM_SPACEMOVE_HIDE2:
                    if (msg.Recog != MShare.g_MySelf.m_nRecogId)
                    {
                        // x
                        // y
                        g_PlayScene.SendMsg(msg.Ident, msg.Recog, msg.Param, msg.Tag, 0, 0, 0, "");
                    }
                    break;
                case Grobal2.SM_HEROLOGIN:
                    Actor = g_PlayScene.FindActor(msg.Recog);
                    if (Actor != null)
                    {
                        if (Actor.m_btIsHero != 1)
                        {
                            Actor.m_btIsHero = 2;
                        }
                    }
                    DrawEffectHum(84, msg.Param, msg.Tag);
                    break;
                case Grobal2.SM_HEROLOGOUT:
                    DrawEffectHum(85, msg.Param, msg.Tag);
                    break;
                case Grobal2.SM_SPACEMOVE_SHOW:
                case Grobal2.SM_SPACEMOVE_SHOW2:
                    n = HUtil32.GetCodeMsgSize(sizeof(TCharDesc) * 4 / 3);
                    if (body.Length > n)
                    {
                        body2 = body.Substring(n + 1 - 1, body.Length);
                        data = EDcode.DeCodeString(body2);
                        body2 = body.Substring(1 - 1, n);
                        Str = HUtil32.GetValidStr3(data, ref data, new string[] { "/" });
                    }
                    else
                    {
                        body2 = body;
                        data = "";
                    }
                    // DScreen.AddChatBoardString(body, clWhite, clRed);
                    EDcode.DecodeBuffer(body2, desc, sizeof(TCharDesc));
                    if ((msg.Recog != MShare.g_MySelf.m_nRecogId))
                    {
                        g_PlayScene.NewActor(msg.Recog, msg.Param, msg.Tag, msg.Series, desc.Feature, desc.Status);
                    }
                    // x
                    // y
                    // dir + light
                    g_PlayScene.SendMsg(msg.Ident, msg.Recog, msg.Param, msg.Tag, msg.Series, desc.Feature, desc.Status, "", desc.StatusEx);
                    if (data != "")
                    {
                        Actor = g_PlayScene.FindActor(msg.Recog);
                        if (Actor != null)
                        {
                            Actor.m_sDescUserName = HUtil32.GetValidStr3(data, ref Actor.m_sUserName, new string[] { "\\" });
                            Actor.m_sUserNameOffSet = HGECanvas.Units.HGECanvas.g_DXCanvas.TextWidth(Actor.m_sUserName) / 2;
                            Actor.m_btNameColor = HUtil32.Str_ToInt(Str, 0);
                            if (Actor.m_btRace == Grobal2.RCC_MERCHANT)
                            {
                                Actor.m_nNameColor = Color.Lime;
                            }
                            else
                            {
                                Actor.m_nNameColor = GetRGB(Actor.m_btNameColor);
                            }
                        }
                    }
                    break;
                case Grobal2.SM_RUSH:
                case Grobal2.SM_RUSHEX:
                case Grobal2.SM_RUSHKUNG:
                    EDcode.DecodeBuffer(body, desc, sizeof(TCharDesc));
                    if (msg.Recog == MShare.g_MySelf.m_nRecogId)
                    {
                        // x
                        // y
                        // dir+light
                        g_PlayScene.SendMsg(msg.Ident, msg.Recog, msg.Param, msg.Tag, msg.Series, desc.Feature, desc.Status, "", desc.StatusEx);
                    }
                    else
                    {
                        // x
                        // y
                        // dir+light
                        g_PlayScene.SendMsg(msg.Ident, msg.Recog, msg.Param, msg.Tag, msg.Series, desc.Feature, desc.Status, "", desc.StatusEx);
                    }
                    if ((msg.Ident == Grobal2.SM_RUSH))
                    {
                        MShare.g_dwLatestRushRushTick = MShare.GetTickCount();
                    }
                    break;
                case Grobal2.SM_WALK:
                case Grobal2.SM_RUN:
                case Grobal2.SM_HORSERUN:
                    EDcode.DecodeBuffer(body, desc, sizeof(TCharDesc));
                    if (msg.Recog != MShare.g_MySelf.m_nRecogId)
                    {
                        // x
                        // y
                        // dir+light
                        g_PlayScene.SendMsg(msg.Ident, msg.Recog, msg.Param, msg.Tag, msg.Series, desc.Feature, desc.Status, "", desc.StatusEx);
                    }
                    break;
                case Grobal2.SM_CHANGELIGHT:
                    Actor = g_PlayScene.FindActor(msg.Recog);
                    if (Actor != null)
                    {
                        Actor.m_nChrLight = msg.Param;
                    }
                    break;
                case Grobal2.SM_LAMPCHANGEDURA:
                    if (MShare.g_UseItems[Grobal2.U_RIGHTHAND].s.Name != "")
                    {
                        MShare.g_UseItems[Grobal2.U_RIGHTHAND].Dura = msg.Recog;
                    }
                    break;
                case Grobal2.SM_HEROLAMPCHANGEDURA:
                    if (MShare.g_HeroUseItems[Grobal2.U_RIGHTHAND].s.Name != "")
                    {
                        MShare.g_HeroUseItems[Grobal2.U_RIGHTHAND].Dura = msg.Recog;
                    }
                    break;
                case Grobal2.SM_MOVEFAIL:
                    ActionFailed();
                    ActionLock = false;
                    frmMain.RecalcAutoMovePath();
                    EDcode.DecodeBuffer(body, desc, sizeof(TCharDesc));
                    ActionFailLock = false;
                    // x
                    // y
                    // dir
                    g_PlayScene.SendMsg(Grobal2.SM_TURN, msg.Recog, msg.Param, msg.Tag, msg.Series, desc.Feature, desc.Status, "", desc.StatusEx);
                    break;
                case Grobal2.SM_BUTCH:// 挖肉动作封包
                    EDcode.DecodeBuffer(body, desc);
                    if (msg.Recog != MShare.g_MySelf.m_nRecogId)
                    {
                        Actor = g_PlayScene.FindActor(msg.Recog);
                        if (Actor != null)
                        {
                            Actor.SendMsg(Grobal2.SM_SITDOWN, msg.Param, msg.Tag, msg.Series, 0, 0, "", 0);
                        }
                    }
                    break;
                case Grobal2.SM_SITDOWN:// 蹲下动作封包
                    EDcode.DecodeBuffer(body, desc);
                    if (msg.Recog != MShare.g_MySelf.m_nRecogId)
                    {
                        Actor = g_PlayScene.FindActor(msg.Recog);
                        if (Actor != null)
                        {
                            Actor.SendMsg(Grobal2.SM_SITDOWN, msg.Param, msg.Tag, msg.Series, 0, 0, "", 0);
                        }
                    }
                    break;
                case Grobal2.SM_HIT:
                case Grobal2.SM_HEAVYHIT:
                case Grobal2.SM_POWERHIT:
                case Grobal2.SM_LONGHIT:
                case Grobal2.SM_CRSHIT:
                case Grobal2.SM_WIDEHIT:
                case Grobal2.SM_BIGHIT:
                case Grobal2.SM_FIREHIT:
                    if (msg.Recog != MShare.g_MySelf.m_nRecogId)
                    {
                        Actor = g_PlayScene.FindActor(msg.Recog);
                        if (Actor != null)
                        {
                            Actor.SendMsg(msg.Ident, msg.Param, msg.Tag, msg.Series, 0, 0, body, 0);
                            if (msg.Ident == Grobal2.SM_HEAVYHIT)
                            {
                                if (body != "")
                                {
                                    Actor.m_boDigFragment = true;
                                }
                            }
                        }
                    }
                    break;
                case Grobal2.SM_FLYAXE:
                    EDcode.DecodeBuffer(body, mbw);
                    Actor = g_PlayScene.FindActor(msg.Recog);
                    if (Actor != null)
                    {
                        Actor.SendMsg(msg.Ident, msg.Param, msg.Tag, msg.Series, 0, 0, "", 0);
                        Actor.m_nTargetX = mbw.param1;
                        Actor.m_nTargetY = mbw.param2;
                        Actor.m_nTargetRecog = HUtil32.Makelong(mbw.Tag1, mbw.Tag2);
                    }
                    break;
                // Modify the A .. B: Grobal2.SM_LIGHTING, Grobal2.SM_LIGHTING_1 .. Grobal2.SM_LIGHTING_3
                case Grobal2.SM_LIGHTING:
                case Grobal2.SM_LIGHTING_1:
                    EDcode.DecodeBuffer(body, wl);
                    Actor = g_PlayScene.FindActor(msg.Recog);
                    if (Actor != null)
                    {
                        // x
                        // y
                        // dir
                        Actor.SendMsg(msg.Ident, msg.Param, msg.Tag, msg.Series, 0, 0, "", 0);
                        Actor.m_nTargetX = wl.lParam1;
                        // x 带瘤绰 格钎
                        Actor.m_nTargetY = wl.lParam2;
                        // y
                        Actor.m_nTargetRecog = wl.lTag1;
                        Actor.m_nMagicNum = wl.lTag2;
                        // 付过 锅龋
                    }
                    break;
                case Grobal2.SM_SPELL:
                    // who
                    // effectnum
                    // tx
                    // y
                    UseMagicSpell(msg.Recog, msg.Series, msg.Param, msg.Tag, HUtil32.Str_ToInt(body, 0));
                    break;
                case Grobal2.SM_MAGICFIRE:
                    EDcode.DecodeBuffer(body, desc, sizeof(TCharDesc));
                    // who
                    // efftype
                    // effnum
                    // tx
                    // y
                    // taget
                    // lv
                    UseMagicFire(msg.Recog, LoByte(msg.Series), HiByte(msg.Series), msg.Param, msg.Tag, desc.Feature, desc.Status);
                    break;
                case Grobal2.SM_MAGICFIRE_FAIL:
                    // who
                    UseMagicFireFail(msg.Recog);
                    break;
                case Grobal2.SM_OUTOFCONNECTION:
                    MShare.g_boDoFastFadeOut = false;
                    MShare.g_boDoFadeIn = false;
                    MShare.g_boDoFadeOut = false;
                    DebugOutStr("服务器连接被强行中断。\\连接时间可能超过限制");
                    this.Close();
                    break;
                case Grobal2.SM_DEATH:
                case Grobal2.SM_NOWDEATH:
                    EDcode.DecodeBuffer(body, desc, sizeof(TCharDesc));
                    Actor = g_PlayScene.FindActor(msg.Recog);
                    if (Actor != null)
                    {
                        // x
                        // y
                        // dir
                        Actor.SendMsg(msg.Ident, msg.Param, msg.Tag, msg.Series, desc.Feature, desc.Status, "", 0);
                        Actor.m_Abil.HP = 0;
                        Actor.m_nIPower = -1;
                    }
                    else
                    {
                        // x
                        // y
                        // dir
                        g_PlayScene.SendMsg(Grobal2.SM_DEATH, msg.Recog, msg.Param, msg.Tag, msg.Series, desc.Feature, desc.Status, "", desc.StatusEx);
                    }
                    break;
                case Grobal2.SM_SKELETON:
                    EDcode.DecodeBuffer(body, desc, sizeof(TCharDesc));
                    // HP
                    // maxHP
                    // damage
                    g_PlayScene.SendMsg(Grobal2.SM_SKELETON, msg.Recog, msg.Param, msg.Tag, msg.Series, desc.Feature, desc.Status, "", desc.StatusEx);
                    break;
                case Grobal2.SM_ALIVE:
                    EDcode.DecodeBuffer(body, desc, sizeof(TCharDesc));
                    // HP
                    // maxHP
                    // damage
                    g_PlayScene.SendMsg(Grobal2.SM_ALIVE, msg.Recog, msg.Param, msg.Tag, msg.Series, desc.Feature, desc.Status, "", desc.StatusEx);
                    break;
                case Grobal2.SM_ABILITY:
                    MShare.g_MySelf.m_nGold = msg.Recog;
                    MShare.g_MySelf.m_btJob = LoByte(msg.Param);
                    MShare.g_MySelf.m_nIPowerLvl = HiByte(msg.Param);
                    MShare.g_MySelf.m_nGameGold = HUtil32.Makelong(msg.Tag, msg.Series);
                    EDcode.DecodeBuffer(body, MShare.g_MySelf.m_Abil, sizeof(TAbility));
                    break;
                case Grobal2.SM_SUBABILITY:
                    MShare.g_nMyHitPoint = LoByte(msg.Param);
                    MShare.g_nMySpeedPoint = HiByte(msg.Param);
                    MShare.g_nMyAntiPoison = LoByte(msg.Tag);
                    MShare.g_nMyPoisonRecover = HiByte(msg.Tag);
                    MShare.g_nMyHealthRecover = LoByte(msg.Series);
                    MShare.g_nMySpellRecover = HiByte(msg.Series);
                    MShare.g_nMyAntiMagic = LoByte(HUtil32.LoWord(msg.Recog));
                    MShare.g_nMyIPowerRecover = HiByte(HUtil32.LoWord(msg.Recog));
                    MShare.g_nMyAddDamage = LoByte(HUtil32.HiWord(msg.Recog));
                    MShare.g_nMyDecDamage = HiByte(HUtil32.HiWord(msg.Recog));
                    break;
                case Grobal2.SM_REFDIAMOND:
                    // g_nMyIPowerRecover := HUtil32.HiWord(LongWord(msg.Recog));
                    MShare.g_MySelf.m_nGameDiamd = (msg.Recog);
                    MShare.g_MySelf.m_nGameGird = (msg.Param);
                    break;
                case Grobal2.SM_DAYCHANGING:
                    MShare.g_nDayBright = msg.Param;
                    break;
                case Grobal2.SM_INTERNALPOWER:
                    // {$IF VIEWFOG}
                    // DarkLevel := msg.tag;
                    // {$ELSE}
                    // DarkLevel := 0;
                    // {$IFEND VIEWFOG}
                    // if g_boForceNotViewFog then
                    // DarkLevel := 0;
                    // if DarkLevel = 0 then
                    // g_boViewFog := False
                    // else
                    // g_boViewFog := True;
                    Actor = g_PlayScene.FindActor(msg.Recog);
                    if (Actor != null)
                    {
                        Actor.m_nIPower = msg.Param;
                    }
                    break;
                case Grobal2.SM_WINEXP:
                    MShare.g_MySelf.m_Abil.Exp = msg.Recog;
                    if (!MShare.g_gcGeneral[3] || (HUtil32.Makelong(msg.Param, msg.Tag) > MShare.g_MaxExpFilter))
                    {
                        DScreen.AddSysMsgBottom(Format("经验值 +%d", new long[] { ((long)HUtil32.Makelong(msg.Param, msg.Tag)) }));
                    }
                    break;
                case Grobal2.SM_HEROWINEXP:
                    // DScreen.AddChatBoardString(Format('%d经验值增加', [LongWord(HUtil32.Makelong(msg.param, msg.tag))]), clWhite, clRed);
                    if (MShare.g_MySelf.m_HeroObject != null)
                    {
                        MShare.g_MySelf.m_HeroObject.m_Abil.Exp = msg.Recog;
                        if (!MShare.g_gcGeneral[3] || (HUtil32.Makelong(msg.Param, msg.Tag) > MShare.g_MaxExpFilter))
                        {
                            DScreen.AddSysMsgBottom(Format("(英雄)经验值 +%d", new long[] { ((long)HUtil32.Makelong(msg.Param, msg.Tag)) }));
                        }
                        // DScreen.AddChatBoardString(Format('%d英雄经验值增加', [LongWord(HUtil32.Makelong(msg.param, msg.tag))]), clWhite, clRed);
                    }
                    break;
                case Grobal2.SM_WINNIMBUSEXP:
                    if (msg.Recog > 0)
                    {
                        DScreen.AddSysMsgBottom(Format("当前灵气值 %d", new int[] { msg.Recog }));
                    }
                    break;
                case Grobal2.SM_HEROWINNIMBUSEXP:
                    if (msg.Recog > 0)
                    {
                        DScreen.AddSysMsgBottom(Format("(英雄)当前灵气值 %d", new int[] { msg.Recog }));
                    }
                    break;
                case Grobal2.SM_WINIPEXP:
                    MShare.g_MySelf.m_nIPowerExp = msg.Recog;
                    ipExp = ((long)HUtil32.Makelong(msg.Param, msg.Tag));
                    if (ipExp > 0)
                    {
                        DScreen.AddSysMsgBottom(Format("%d点内功经验增加", new long[] { ipExp }));
                    }
                    if (msg.Series >= 3 && msg.Series <= 28)
                    {
                        MShare.g_nMagicRange = msg.Series;
                    }
                    break;
                case Grobal2.SM_HEROWINIPEXP:
                    if (MShare.g_MySelf.m_HeroObject != null)
                    {
                        MShare.g_MySelf.m_HeroObject.m_nIPowerExp = msg.Recog;
                        ipExp = ((long)HUtil32.Makelong(msg.Param, msg.Tag));
                        if (ipExp > 0)
                        {
                            DScreen.AddSysMsgBottom(Format("(英雄)%d点内功经验增加", new long[] { ipExp }));
                        }
                    }
                    break;
                case Grobal2.SM_LEVELUP:
                    MShare.g_MySelf.m_Abil.Level = msg.Param;
                    DScreen.AddSysMsg("您的等级已升级！");
                    break;
                case Grobal2.SM_HEALTHSPELLCHANGED:
                    Actor = g_PlayScene.FindActor(msg.Recog);
                    if (Actor != null)
                    {
                        Actor.m_Abil.HP = msg.Param;
                        Actor.m_Abil.MP = msg.Tag;
                        Actor.m_Abil.MaxHP = msg.Series;
                    }
                    break;
                case Grobal2.SM_STRUCK:
                    EDcode.DecodeBuffer(body, wl);
                    Actor = g_PlayScene.FindActor(msg.Recog);
                    if (Actor != null)
                    {
                        if (MShare.g_gcGeneral[13] && (msg.Series > 0))
                        {
                            Actor.GetMoveHPShow(msg.Series);
                        }
                        if (Actor != MShare.g_MySelf)
                        {
                            if (Actor.CanCancelAction())
                            {
                                Actor.CancelAction();
                            }
                        }
                        if ((Actor != MShare.g_MySelf) && (Actor != MShare.g_MySelf.m_HeroObject))
                        {
                            if ((Actor.m_btRace != 0) || !MShare.g_gcGeneral[15])
                            {
                                Actor.UpdateMsg(Grobal2.SM_STRUCK, wl.lTag2, 0, msg.Series, wl.lParam1, wl.lParam2, "", wl.lTag1);
                            }
                        }
                        Actor.m_Abil.HP = msg.Param;
                        Actor.m_Abil.MaxHP = msg.Tag;
                        if (MShare.g_boOpenAutoPlay && TimerAutoPlay.Enabled)
                        {
                            // 0613 自己受人攻击,小退
                            Actor2 = g_PlayScene.FindActor(wl.lTag1);
                            if ((Actor2 == null) || ((Actor2.m_btRace != 0) && (Actor2.m_btIsHero != 1)))
                            {
                                return;
                            }
                            if ((MShare.g_MySelf != null))
                            {
                                if ((Actor == MShare.g_MySelf.m_HeroObject))
                                {
                                    // 英雄受人攻击
                                    FrmDlg.ClientCallHero();
                                }
                                else if ((Actor == MShare.g_MySelf))
                                {
                                    // 自己受人攻击,小退
                                    MShare.g_nAPReLogon = 1;
                                    // 保存状态
                                    MShare.g_nAPrlRecallHero = (MShare.g_MySelf.m_HeroObject != null);
                                    MShare.g_nOverAPZone2 = MShare.g_nOverAPZone;
                                    MShare.g_APGoBack2 = MShare.g_APGoBack;
                                    if (MShare.g_APMapPath != null)
                                    {
                                        MShare.g_APMapPath2 = new Point[MShare.g_APMapPath.GetUpperBound(0) + 1];
                                        for (i = 0; i <= MShare.g_APMapPath.GetUpperBound(0); i++)
                                        {
                                            MShare.g_APMapPath2[i] = MShare.g_APMapPath[i];
                                        }
                                    }
                                    MShare.g_APLastPoint2 = MShare.g_APLastPoint;
                                    MShare.g_APStep2 = MShare.g_APStep;
                                    AppLogout();
                                    // SaveBagsData();
                                }
                            }
                        }
                    }
                    break;
                case Grobal2.SM_CHANGEFACE:
                    Actor = g_PlayScene.FindActor(msg.Recog);
                    if (Actor != null)
                    {
                        EDcode.DecodeBuffer(body, desc, sizeof(TCharDesc));
                        Actor.m_nWaitForRecogId = HUtil32.Makelong(msg.Param, msg.Tag);
                        Actor.m_nWaitForFeature = desc.Feature;
                        Actor.m_nWaitForStatus = desc.Status;
                        ClFunc.AddChangeFace(Actor.m_nWaitForRecogId);
                    }
                    break;
                case Grobal2.SM_PASSWORD:
                    break;
                case Grobal2.SM_OPENHEALTH:
                    // PlayScene.EdChat.PasswordChar:='*';
                    // SetInputStatus();
                    Actor = g_PlayScene.FindActor(msg.Recog);
                    if (Actor != null)
                    {
                        if (Actor != MShare.g_MySelf)
                        {
#if HIGHHP
                            EDcode.DecodeBuffer(body, sMsg, sizeof(TShortMessage));
                                                        Actor.m_Abil.HP = ((double)HUtil32.Makelong(msg.Param, msg.Tag));
                                                        Actor.m_Abil.MaxHP = ((double)HUtil32.Makelong(sMsg.Ident, sMsg.wMsg));
#else
                            Actor.m_Abil.HP = msg.Param;
                            Actor.m_Abil.MaxHP = msg.Tag;
#endif
                        }
                        Actor.m_boOpenHealth = true;
                        // actor.OpenHealthTime := 999999999;
                        // actor.OpenHealthStart := GetTickCount;
                    }
                    break;
                case Grobal2.SM_CLOSEHEALTH:
                    Actor = g_PlayScene.FindActor(msg.Recog);
                    if (Actor != null)
                    {
                        Actor.m_boOpenHealth = false;
                    }
                    break;
                case Grobal2.SM_INSTANCEHEALGUAGE:
                    Actor = g_PlayScene.FindActor(msg.Recog);
                    if (Actor != null)
                    {
#if HIGHHP
                        EDcode.DecodeBuffer(body, sMsg, sizeof(TShortMessage));
                                                Actor.m_Abil.HP = ((double)HUtil32.Makelong(msg.Param, msg.Tag));
                                                Actor.m_Abil.MaxHP = ((double)HUtil32.Makelong(sMsg.Ident, sMsg.wMsg));
#else
                        Actor.m_Abil.HP = msg.Param;
                        Actor.m_Abil.MaxHP = msg.Tag;
#endif
                        Actor.m_noInstanceOpenHealth = true;
                        Actor.m_dwOpenHealthTime = 2 * 1000;
                        Actor.m_dwOpenHealthStart = MShare.GetTickCount();
                    }
                    break;
                case Grobal2.SM_BREAKWEAPON:
                    Actor = g_PlayScene.FindActor(msg.Recog);
                    if (Actor != null)
                    {
                        if (Actor is THumActor)
                        {
                            ((THumActor)(Actor)).DoWeaponBreakEffect();
                        }
                    }
                    break;
                case Grobal2.SM_HEAR:
                case Grobal2.SM_CRY:
                case Grobal2.SM_GROUPMESSAGE:
                case Grobal2.SM_GUILDMESSAGE:
                case Grobal2.SM_WHISPER:
                case Grobal2.SM_SYSMESSAGE:
                    if ((msg.Ident == Grobal2.SM_HEAR) && (FrmDlg.DBRefuseSay.tag != 0))
                    {
                        return;
                    }
                    if ((msg.Ident == Grobal2.SM_CRY) && (FrmDlg.DBRefuseCry.tag != 0))
                    {
                        return;
                    }
                    if ((msg.Ident == Grobal2.SM_WHISPER) && (FrmDlg.DBRefuseWhisper.tag != 0))
                    {
                        return;
                    }
                    if ((msg.Ident == Grobal2.SM_GUILDMESSAGE) && (FrmDlg.DBRefuseGuild.tag != 0))
                    {
                        return;
                    }
                    Str = EDcode.DeCodeString(body);
                    if (msg.Tag > 0)
                    {
                        DScreen.AddSysMsgCenter(Str, GetRGB(LoByte(msg.Param)), GetRGB(HiByte(msg.Param)), msg.Tag);
                        return;
                    }
                    if (FrmDlg.m_BlockList.count > 0)
                    {
                        Str2 = DecodeMessagePacket_ExtractUserName(Str);
                        nFuncPos = FrmDlg.m_BlockList.IndexOf(Str2);
                        if (nFuncPos >= 0)
                        {
                            return;
                        }
                    }
                    if (msg.Ident == Grobal2.SM_WHISPER)
                    {
                        HUtil32.GetValidStr3(Str, ref str3, new string[] { " ", "=", ">" });
                        if (FrmDlg.m_FriendsList.IndexOf(str3) > -1)
                        {
                            DScreen.AddChatBoardString(Str, Color.White, GetRGB(253));
                        }
                        else
                        {
                            DScreen.AddChatBoardString(Str, GetRGB(LoByte(msg.Param)), GetRGB(HiByte(msg.Param)));
                        }
                        FrmDlg.m_xChatRecordList.Add(Format("[%s] %s", new string[] { DateTime.Now.ToString(), Str }));
                        if (FrmDlg.m_xChatRecordList.count > 5000)
                        {
                            FrmDlg.m_xChatRecordList.Delete(0);
                        }
                    }
                    else
                    {
                        DScreen.AddChatBoardString(Str, GetRGB(LoByte(msg.Param)), GetRGB(HiByte(msg.Param)));
                    }
                    if (msg.Ident == Grobal2.SM_GUILDMESSAGE)
                    {
                        FrmDlg.AddGuildChat(Str);
                    }
                    else if (msg.Ident == Grobal2.SM_HEAR)
                    {
                        Actor = g_PlayScene.FindActor(msg.Recog);
                        if (Actor != null)
                        {
                            Actor.Say(Str);
                        }
                    }
                    break;
                case Grobal2.SM_ATTACKMODE:
                    switch (msg.Param)
                    {
                        case Grobal2.HAM_ALL:
                            MShare.g_sAttackMode = MShare.sAttackModeOfAll;
                            break;
                        case Grobal2.HAM_PEACE:
                            MShare.g_sAttackMode = MShare.sAttackModeOfPeaceful;
                            break;
                        case Grobal2.HAM_DEAR:
                            MShare.g_sAttackMode = MShare.sAttackModeOfDear;
                            break;
                        case Grobal2.HAM_MASTER:
                            MShare.g_sAttackMode = MShare.sAttackModeOfMaster;
                            break;
                        case Grobal2.HAM_GROUP:
                            MShare.g_sAttackMode = MShare.sAttackModeOfGroup;
                            break;
                        case Grobal2.HAM_GUILD:
                            MShare.g_sAttackMode = MShare.sAttackModeOfGuild;
                            break;
                        case Grobal2.HAM_PKATTACK:
                            MShare.g_sAttackMode = MShare.sAttackModeOfRedWhite;
                            break;
                    }
                    break;
                case Grobal2.SM_USERNAME:
                    Str = EDcode.DeCodeString(body);
                    Actor = g_PlayScene.FindActor(msg.Recog);
                    if (Actor != null)
                    {
                        Actor.m_sDescUserName = HUtil32.GetValidStr3(Str, ref Actor.m_sUserName, new string[] { "\\" });
                        Actor.m_sUserNameOffSet = HGECanvas.Units.HGECanvas.g_DXCanvas.TextWidth(Actor.m_sUserName) / 2;
                        Actor.m_btNameColor = msg.Param;
                        if (Actor.m_btRace == Grobal2.RCC_MERCHANT)
                        {
                            Actor.m_nNameColor = Color.Lime;
                        }
                        else
                        {
                            Actor.m_nNameColor = GetRGB(msg.Param);
                        }
                        if (msg.Tag >= 1 && msg.Tag <= 5)
                        {
                            Actor.m_btAttribute = msg.Tag;
                        }
                    }
                    break;
                case Grobal2.SM_CHANGENAMECOLOR:
                    Actor = g_PlayScene.FindActor(msg.Recog);
                    if (Actor != null)
                    {
                        Actor.m_btNameColor = msg.Param;
                        if (Actor.m_btRace == Grobal2.RCC_MERCHANT)
                        {
                            Actor.m_nNameColor = Color.Lime;
                        }
                        else
                        {
                            Actor.m_nNameColor = GetRGB(msg.Param);
                        }
                    }
                    break;
                case Grobal2.SM_HIDE:
                case Grobal2.SM_GHOST:
                case Grobal2.SM_DISAPPEAR:
                    if (MShare.g_MySelf.m_nRecogId != msg.Recog)
                    {
                        // x
                        // y
                        g_PlayScene.SendMsg(Grobal2.SM_HIDE, msg.Recog, msg.Param, msg.Tag, msg.Series, 0, 0, "");
                    }
                    break;
                case Grobal2.SM_DIGUP:
                    EDcode.DecodeBuffer(body, wl);
                    Actor = g_PlayScene.FindActor(msg.Recog);
                    if (Actor == null)
                    {
                        Actor = g_PlayScene.NewActor(msg.Recog, msg.Param, msg.Tag, msg.Series, wl.lParam1, wl.lParam2);
                    }
                    Actor.m_nCurrentEvent = wl.lTag1;
                    // x
                    // y
                    // dir + light
                    Actor.SendMsg(Grobal2.SM_DIGUP, msg.Param, msg.Tag, msg.Series, wl.lParam1, wl.lParam2, "", 0);
                    break;
                case Grobal2.SM_HEROSTATE:
                    if (MShare.g_MySelf != null)
                    {
                        EDcode.DecodeBuffer(body, wl);
                        MShare.g_MySelf.m_HeroObject = g_PlayScene.NewActor(msg.Recog, msg.Param, msg.Tag, msg.Series, wl.lParam1, wl.lParam2);
                        MShare.g_MySelf.m_HeroObject.m_btIsHero = 1;
                        FrmDlg.DWHeroStatus.Visible = true;
                    }
                    break;
                case Grobal2.SM_HEROABILITY:
                    if ((MShare.g_MySelf != null) && (MShare.g_MySelf.m_HeroObject != null))
                    {
                        MShare.g_MySelf.m_HeroObject.m_nGold = msg.Recog;
                        MShare.g_MySelf.m_HeroObject.m_btJob = LoByte(msg.Param);
                        MShare.g_MySelf.m_HeroObject.m_nIPowerLvl = HiByte(msg.Param);
                        MShare.g_MySelf.m_HeroObject.m_wGloryPoint = msg.Series;
                        EDcode.DecodeBuffer(body, MShare.g_MySelf.m_HeroObject.m_Abil, sizeof(TAbility));
                    }
                    break;
                case Grobal2.SM_HEROSUBABILITY:
                    MShare.g_nHeroHitPoint = LoByte(msg.Param);
                    MShare.g_nHeroSpeedPoint = HiByte(msg.Param);
                    MShare.g_nHeroAntiPoison = LoByte(msg.Tag);
                    MShare.g_nHeroPoisonRecover = HiByte(msg.Tag);
                    MShare.g_nHeroHealthRecover = LoByte(msg.Series);
                    MShare.g_nHeroSpellRecover = HiByte(msg.Series);
                    MShare.g_nHeroAntiMagic = LoByte(HUtil32.LoWord(msg.Recog));
                    MShare.g_nHeroIPowerRecover = HiByte(HUtil32.LoWord(msg.Recog));
                    MShare.g_nHeroAddDamage = LoByte(HUtil32.HiWord(msg.Recog));
                    MShare.g_nHeroDecDamage = HiByte(HUtil32.HiWord(msg.Recog));
                    break;
                case Grobal2.SM_HEROSTATEDISPEAR:
                    // g_nHeroIPowerRecover := HUtil32.HiWord(msg.Recog);
                    if (MShare.g_MySelf != null)
                    {
                        if (msg.Recog == 0)
                        {
                            SaveBagsData();
                            ClFunc.HeroClearBag();
                            MShare.g_MySelf.m_HeroObject = null;
                            FrmDlg.CloseHeroWindows;
                        }
                        else
                        {
                            for (i = 0; i < MShare.g_MySelf.m_SlaveObject.Count; i++)
                            {
                                if (((TActor)(MShare.g_MySelf.m_SlaveObject[i])).m_nRecogId == msg.Recog)
                                {
                                    MShare.g_MySelf.m_SlaveObject.RemoveAt(i);
                                    break;
                                }
                            }
                            // g_MySelf.m_SlaveObject := nil;
                        }
                    }
                    break;
                case Grobal2.SM_HERONAME:
                    Str = EDcode.DeCodeString(body);
                    if ((Str != "") && (MShare.g_MySelf.m_HeroObject != null))
                    {
                        MShare.g_MySelf.m_HeroObject.m_sDescUserName = HUtil32.GetValidStr3(Str, ref MShare.g_MySelf.m_HeroObject.m_sUserName, new string[] { "\\" });
                        MShare.g_MySelf.m_HeroObject.m_sUserNameOffSet = HGECanvas.Units.HGECanvas.g_DXCanvas.TextWidth(MShare.g_MySelf.m_HeroObject.m_sUserName) / 2;
                        MShare.g_MySelf.m_HeroObject.m_btNameColor = msg.Param;
                        MShare.g_MySelf.m_HeroObject.m_nNameColor = GetRGB(msg.Param);
                        m_sHeroCharName = MShare.g_MySelf.m_HeroObject.m_sUserName;
                    }
                    break;
                case Grobal2.SM_HEROLOYALTY:
                    Str = EDcode.DeCodeString(body);
                    if (MShare.g_MySelf.m_HeroObject != null)
                    {
                        if (Str != "")
                        {
                            MShare.g_MySelf.m_HeroObject.m_sLoyaly = Str;
                        }
                        else
                        {
                            MShare.g_MySelf.m_HeroObject.m_sLoyaly = "50.00%";
                        }
                    }
                    break;
                case Grobal2.SM_DIGDOWN:
                    // x
                    // y
                    g_PlayScene.SendMsg(Grobal2.SM_DIGDOWN, msg.Recog, msg.Param, msg.Tag, 0, 0, 0, "");
                    break;
                case Grobal2.SM_SHOWEVENT:
                    EDcode.DecodeBuffer(body, sMsg, sizeof(TShortMessage));
                    // x
                    // y
                    // e-type
                    __event = new TClEvent(msg.Recog, HUtil32.LoWord(msg.Tag), msg.Series, msg.Param);
                    __event.m_nDir = 0;
                    __event.m_nEventParam = sMsg.Ident;
                    __event.m_nEventLevel = sMsg.wMsg;
                    EventMan.AddEvent(__event);
                    break;
                case Grobal2.SM_HIDEEVENT:
                    EventMan.DelEventById(msg.Recog);
                    break;
                case Grobal2.SM_ADDITEM:
                    ClientGetAddItem(msg.Series, body);
                    break;
                case Grobal2.SM_HEROADDITEM:
                    ClientHeroGetAddItem(body);
                    break;
                case Grobal2.SM_BAGITEMS:
                    ClientGetBagItmes(body);
                    break;
                case Grobal2.SM_HEROBAGITEMS:
                    ClientHeroGetBagItmes(body, msg.Series);
                    break;
                case Grobal2.SM_COUNTERITEMCHANGE:
                    if (!MShare.g_boDealEnd)
                    {
                        MShare.g_dwDealActionTick = MShare.GetTickCount();
                    }
                    ClFunc.ChangeItemCount(msg.Recog, msg.Param, msg.Tag, EDcode.DeCodeString(body));
                    break;
                case Grobal2.SM_HEROCOUNTERITEMCHANGE:
                    if (!MShare.g_boDealEnd)
                    {
                        MShare.g_dwDealActionTick = MShare.GetTickCount();
                    }
                    ClFunc.HEroChangeItemCount(msg.Recog, msg.Param, msg.Tag, EDcode.DeCodeString(body));
                    break;
                case Grobal2.SM_UPDATEITEM:
                    ClientGetUpdateItem(body);
                    break;
                case Grobal2.SM_HEROUPDATEITEM:
                    ClientHeroGetUpdateItem(body);
                    break;
                case Grobal2.SM_DELITEM:
                    ClientGetDelItem(body);
                    break;
                case Grobal2.SM_HERODELITEM:
                    ClientHeroGetDelItem(body);
                    break;
                case Grobal2.SM_DELITEMS:
                    ClientGetDelItems(body, msg.Param);
                    break;
                case Grobal2.SM_HERODELITEMS:
                    ClientHeroGetDelItems(body, msg.Param);
                    break;
                case Grobal2.SM_DRO.Item.ItemUCCESS:
                    ClFunc.DelDropItem(EDcode.DeCodeString(body), msg.Recog);
                    break;
                case Grobal2.SM_DROPITEM_FAIL:
                    ClientGetDropItemFail(EDcode.DeCodeString(body), msg.Recog);
                    break;
                case Grobal2.SM_HERODRO.Item.ItemUCCESS:
                    ClFunc.DelDropItem(EDcode.DeCodeString(body), msg.Recog);
                    break;
                case Grobal2.SM_HERODROPITEM_FAIL:
                    ClientHeroGetDropItemFail(EDcode.DeCodeString(body), msg.Recog);
                    break;
                case Grobal2.SM_ITEMSHOW:
                    // x
                    // y
                    // looks
                    ClientGetShowItem(msg.Recog, msg.Param, msg.Tag, msg.Series, EDcode.DeCodeString(body));
                    break;
                case Grobal2.SM_ITEMHIDE:
                    ClientGetHideItem(msg.Recog, msg.Param, msg.Tag);
                    break;
                case Grobal2.SM_OPENDOOR_OK:
                    Map.OpenDoor(msg.Param, msg.Tag);
                    break;
                case Grobal2.SM_OPENDOOR_LOCK:
                    DScreen.AddSysMsg("此门被锁定");
                    break;
                case Grobal2.SM_CLOSEDOOR:
                    Map.CloseDoor(msg.Param, msg.Tag);
                    break;
                case Grobal2.SM_ADDITEMTOHEROBAG:
                    if (MShare.g_WaitingUseItem.Item.Item.Name != "")
                    {
                        ClFunc.DelStallItem(MShare.g_WaitingUseItem.Item);
                        ClFunc.HeroAddItemBag(MShare.g_WaitingUseItem.Item);
                        MShare.g_WaitingUseItem.Item.Item.Name = "";
                    }
                    break;
                case Grobal2.SM_HEROEXCHGBAGITEM_FAIL:
                    if (MShare.g_WaitingUseItem.Item.Item.Name != "")
                    {
                        if (msg.Recog == 0)
                        {
                            ClFunc.AddItemBag(MShare.g_WaitingUseItem.Item);
                            MShare.g_WaitingUseItem.Item.Item.Name = "";
                            DScreen.AddChatBoardString("(英雄) 背包已满，请整理后进行操作", Color.White, Color.Red);
                        }
                        else if (msg.Recog == 1)
                        {
                            ClFunc.HeroAddItemBag(MShare.g_WaitingUseItem.Item);
                            MShare.g_WaitingUseItem.Item.Item.Name = "";
                            DScreen.AddChatBoardString("你的背包已满，请整理后进行操作", Color.White, Color.Red);
                        }
                        else if (msg.Recog == 2)
                        {
                            ClFunc.AddItemBag(MShare.g_WaitingUseItem.Item);
                            DScreen.AddChatBoardString(MShare.g_WaitingUseItem.Item.Item.Name + "不能放到英雄包裹中", Color.White, Color.Red);
                            MShare.g_WaitingUseItem.Item.Item.Name = "";
                        }
                    }
                    break;
                case Grobal2.SM_GETITEMFROMHEROBAG:
                    if (MShare.g_WaitingUseItem.Item.Item.Name != "")
                    {
                        ClFunc.AddItemBag(MShare.g_WaitingUseItem.Item);
                        MShare.g_WaitingUseItem.Item.Item.Name = "";
                    }
                    break;
                case Grobal2.SM_TAKEON_OK:
                    MShare.g_MySelf.m_nFeature = msg.Recog;
                    MShare.g_MySelf.FeatureChanged();
                    if (MShare.g_WaitingUseItem.Item.Item.Name != "")
                    {
                        if (MShare.g_WaitingUseItem.Index >= 0 && MShare.g_WaitingUseItem.Index <= Grobal2.U_FASHION)
                        {
                            MShare.g_UseItems[MShare.g_WaitingUseItem.Index] = MShare.g_WaitingUseItem.Item;
                        }
                        MShare.g_WaitingUseItem.Item.Item.Name = "";
                    }
                    break;
                case Grobal2.SM_TAKEON_FAIL:
                    if (MShare.g_WaitingUseItem.Item.Item.Name != "")
                    {
                        ClFunc.AddItemBag(MShare.g_WaitingUseItem.Item);
                        MShare.g_WaitingUseItem.Item.Item.Name = "";
                    }
                    break;
                case Grobal2.SM_HEROTAKEON_OK:
                    if (MShare.g_WaitingUseItem.Item.Item.Name != "")
                    {
                        if (MShare.g_WaitingUseItem.Index >= 0 && MShare.g_WaitingUseItem.Index <= Grobal2.U_FASHION)
                        {
                            MShare.g_HeroUseItems[MShare.g_WaitingUseItem.Index] = MShare.g_WaitingUseItem.Item;
                        }
                        MShare.g_WaitingUseItem.Item.Item.Name = "";
                    }
                    break;
                case Grobal2.SM_HEROTAKEON_FAIL:
                    if (MShare.g_WaitingUseItem.Item.Item.Name != "")
                    {
                        ClFunc.HeroAddItemBag(MShare.g_WaitingUseItem.Item);
                        MShare.g_WaitingUseItem.Item.Item.Name = "";
                    }
                    break;
                case Grobal2.SM_TAKEOFF_OK:
                    MShare.g_MySelf.m_nFeature = msg.Recog;
                    MShare.g_MySelf.FeatureChanged();
                    // AddItemBag(g_WaitingUseItem.Item);
                    MShare.g_WaitingUseItem.Item.Item.Name = "";
                    break;
                case Grobal2.SM_HEROTAKEOFF_OK:
                    if (MShare.g_WaitingUseItem.Item.Item.Name != "")
                    {
                        ClFunc.HeroAddItemBag(MShare.g_WaitingUseItem.Item);
                        MShare.g_WaitingUseItem.Item.Item.Name = "";
                    }
                    break;
                case Grobal2.SM_TAKEOFF_FAIL:
                    if (MShare.g_WaitingUseItem.Item.Item.Name != "")
                    {
                        if (MShare.g_WaitingUseItem.Index < 0)
                        {
                            n = -(MShare.g_WaitingUseItem.Index + 1);
                            MShare.g_UseItems[n] = MShare.g_WaitingUseItem.Item;
                        }
                        MShare.g_WaitingUseItem.Item.Item.Name = "";
                    }
                    break;
                case Grobal2.SM_HEROTAKEOFF_FAIL:
                    if (MShare.g_WaitingUseItem.Item.Item.Name != "")
                    {
                        if ((MShare.g_WaitingUseItem.Index < (0 - MShare.HERO_MIIDX_OFFSET)) && (MShare.g_WaitingUseItem.Index >= -((Grobal2.U_FASHION + 1) + MShare.HERO_MIIDX_OFFSET)))
                        {
                            n = -(MShare.g_WaitingUseItem.Index + 1 + MShare.HERO_MIIDX_OFFSET);
                            MShare.g_HeroUseItems[n] = MShare.g_WaitingUseItem.Item;
                        }
                        MShare.g_WaitingUseItem.Item.Item.Name = "";
                    }
                    break;
                case Grobal2.SM_QUERYREFINEITEM:
                    FrmDlg.DWRefine.Visible = true;
                    break;
                case Grobal2.SM_SENDUSEITEMS:
                    ClientGetSendUseItems(body);
                    break;
                case Grobal2.SM_HEROUSEITEMS:
                    ClientGetSendHeroUseItems(body);
                    break;
                case Grobal2.SM_WEIGHTCHANGED:
                    MShare.g_MySelf.m_Abil.Weight = msg.Recog;
                    MShare.g_MySelf.m_Abil.WearWeight = msg.Param;
                    MShare.g_MySelf.m_Abil.HandWeight = msg.Tag;
                    break;
                case Grobal2.SM_GOLDCHANGED:
                    if (msg.Recog > MShare.g_MySelf.m_nGold)
                    {
                        DScreen.AddSysMsg("获得 " + (msg.Recog - MShare.g_MySelf.m_nGold).ToString() + MShare.g_sGoldName);
                    }
                    MShare.g_MySelf.m_nGold = msg.Recog;
                    MShare.g_MySelf.m_nGameGold = HUtil32.Makelong(msg.Param, msg.Tag);
                    break;
                case Grobal2.SM_FEATURECHANGED:
                    g_PlayScene.SendMsg(msg.Ident, msg.Recog, 0, 0, 0, HUtil32.Makelong(msg.Param, msg.Tag), HUtil32.Makelong(msg.Series, 0), body);
                    break;
                case Grobal2.SM_APPRCHANGED:
                case Grobal2.SM_CHARSTATUSCHANGED:
                    if (body != "")
                    {
                        g_PlayScene.SendMsg(msg.Ident, msg.Recog, 0, 0, 0, HUtil32.Makelong(msg.Param, msg.Tag), msg.Series, EDcode.DeCodeString(body));
                    }
                    else
                    {
                        g_PlayScene.SendMsg(msg.Ident, msg.Recog, 0, 0, 0, HUtil32.Makelong(msg.Param, msg.Tag), msg.Series, "");
                    }
                    break;
                case Grobal2.SM_CLEAROBJECTS:
                    // PlayScene.CleanObjects;
                    MShare.g_boMapMoving = true;
                    break;
                case Grobal2.SM_EAT_OK:
                    if (msg.Recog != 0)
                    {
                        Str = "";
                        if (msg.Recog != MShare.g_EatingItem.MakeIndex)
                        {
                            for (i = MShare.MAXBAGITEMCL - 1; i >= 0; i--)
                            {
                                if (MShare.g_ItemArr[i].Item.Name != "")
                                {
                                    if (MShare.g_ItemArr[i].MakeIndex == MShare.g_EatingItem.MakeIndex)
                                    {
                                        ClFunc.DelStallItem(MShare.g_ItemArr[i]);
                                        Str = MShare.g_ItemArr[i].Item.Name;
                                        MShare.g_ItemArr[i].Item.Name = "";
                                        break;
                                    }
                                }
                            }
                        }
                        if (Str == "")
                        {
                            Str = MShare.g_EatingItem.Item.Name;
                            if (m_boSupplyItem)
                            {
                                if (m_nEatRetIdx >= 0 && m_nEatRetIdx <= 5)
                                {
                                    AutoSupplyBeltItem(MShare.g_EatingItem.Item.AniCount, m_nEatRetIdx, Str);
                                }
                                else
                                {
                                    AutoSupplyBagItem(MShare.g_EatingItem.Item.AniCount, Str);
                                }
                                m_boSupplyItem = false;
                            }
                        }
                        MShare.g_EatingItem.Item.Name = "";
                        ClFunc.ArrangeItembag();
                        m_nEatRetIdx = -1;
                    }
                    break;
                case Grobal2.SM_HEROEAT_OK:
                    MShare.g_EatingItem.Item.Name = "";
                    ClFunc.ArrangeHeroItembag();
                    break;
                case Grobal2.SM_EAT_FAIL:
                    if (msg.Recog == MShare.g_EatingItem.MakeIndex)
                    {
                        // DScreen.AddChatBoardString(g_EatingItem.Item.Name + ' ' + IntToStr(msg.tag), clRed, clWhite);
                        if (msg.Tag > 0)
                        {
                            MShare.g_EatingItem.Dura = msg.Tag;
                        }
                        ClFunc.AddItemBag(MShare.g_EatingItem, m_nEatRetIdx);
                        MShare.g_EatingItem.Item.Name = "";
                        m_nEatRetIdx = -1;
                    }
                    m_boSupplyItem = false;
                    switch (msg.Series)
                    {
                        case 1:
                            DScreen.AddChatBoardString("[失败] 你的金币不足，不能释放积灵珠！", Color.Red, Color.White);
                            break;
                        case 2:
                            DScreen.AddChatBoardString("[失败] 你的元宝不足，不能释放积灵珠！", Color.Red, Color.White);
                            break;
                        case 3:
                            DScreen.AddChatBoardString("[失败] 你的金刚石不足，不能释放积灵珠！", Color.Red, Color.White);
                            break;
                        case 4:
                            DScreen.AddChatBoardString("[失败] 你的灵符不足，不能释放积灵珠！", Color.Red, Color.White);
                            break;
                    }
                    break;
                case Grobal2.SM_HEROEAT_FAIL:
                    if (msg.Tag > 0)
                    {
                        MShare.g_EatingItem.Dura = msg.Tag;
                    }
                    ClFunc.HeroAddItemBag(MShare.g_EatingItem);
                    MShare.g_EatingItem.Item.Name = "";
                    switch (msg.Series)
                    {
                        case 1:
                            DScreen.AddChatBoardString("[失败] 你的金币不足，英雄不能释放积灵珠！", Color.Red, Color.White);
                            break;
                        case 2:
                            DScreen.AddChatBoardString("[失败] 你的元宝不足，英雄不能释放积灵珠！", Color.Red, Color.White);
                            break;
                        case 3:
                            DScreen.AddChatBoardString("[失败] 你的金刚石不足，英雄不能释放积灵珠！", Color.Red, Color.White);
                            break;
                        case 4:
                            DScreen.AddChatBoardString("[失败] 你的灵符不足，英雄不能释放积灵珠！", Color.Red, Color.White);
                            break;
                    }
                    break;
                case Grobal2.SM_OFFERITEM:
                case Grobal2.SM_SPECOFFERITEM:
                    if (body != "")
                    {
                        ClientGetShopItems(body, msg.Param);
                    }
                    break;
                case Grobal2.SM_ADDMAGIC:
                    if (body != "")
                    {
                        ClientGetAddMagic(body);
                    }
                    break;
                case Grobal2.SM_HEROADDMAGIC:
                    if (body != "")
                    {
                        ClientHeroGetAddMagic(body);
                    }
                    break;
                case Grobal2.SM_SENDMYMAGIC:
                    if (body != "")
                    {
                        ClientGetMyMagics(body);
                    }
                    break;
                case Grobal2.SM_HEROMYMAGICS:
                    if (body != "")
                    {
                        ClientGetHeroMagics(body);
                    }
                    break;
                case Grobal2.SM_DELMAGIC:
                    ClientGetDelMagic(msg.Recog, msg.Param);
                    break;
                case Grobal2.SM_CONVERTMAGIC:
                    ClientConvertMagic(msg.Recog, msg.Param, msg.Tag, msg.Series, body);
                    break;
                case Grobal2.SM_HCONVERTMAGIC:
                    hClientConvertMagic(msg.Recog, msg.Param, msg.Tag, msg.Series, body);
                    break;
                case Grobal2.SM_HERODELMAGIC:
                    ClientHeroGetDelMagic(msg.Recog, msg.Param);
                    break;
                case Grobal2.SM_MAGIC_LVEXP:
                    // magid
                    // lv
                    ClientGetMagicLvExp(msg.Recog, msg.Param, HUtil32.Makelong(msg.Tag, msg.Series));
                    break;
                case Grobal2.SM_MAGIC_MAXLV:
                    // magid
                    // Maxlv
                    ClientGetMagicMaxLv(msg.Recog, msg.Param, msg.Series);
                    break;
                case Grobal2.SM_HEROMAGIC_LVEXP:
                    // magid
                    // lv
                    ClientHeroGetMagicLvExp(msg.Recog, msg.Param, HUtil32.Makelong(msg.Tag, msg.Series));
                    break;
                case Grobal2.SM_DURACHANGE:
                    // useitem index
                    ClientGetDuraChange(msg.Param, msg.Recog, HUtil32.Makelong(msg.Tag, msg.Series));
                    break;
                case Grobal2.SM_HERODURACHANGE:
                    // useitem index
                    ClientHeroGetDuraChange(msg.Param, msg.Recog, HUtil32.Makelong(msg.Tag, msg.Series));
                    break;
                case Grobal2.SM_HEROPOWERUP:
                    if (MShare.g_MySelf.m_HeroObject != null)
                    {
                        MShare.g_MySelf.m_HeroObject.m_nHeroEnergyType = msg.Param;
                        MShare.g_MySelf.m_HeroObject.m_nHeroEnergy = msg.Recog;
                        MShare.g_MySelf.m_HeroObject.m_nMaxHeroEnergy = msg.Tag;
                        if (msg.Param == 1)
                        {
                        }
                    }
                    break;
                case Grobal2.SM_MERCHANTSAY:
                    ClientGetMerchantSay(msg.Recog, msg.Param, EDcode.DeCodeString(body));
                    break;
                case Grobal2.SM_MERCHANTDLGCLOSE:
                    FrmDlg.CloseMDlg;
                    break;
                case Grobal2.SM_SENDGOODSLIST:
                    ClientGetSendGoodsList(msg.Recog, msg.Param, body);
                    break;
                case Grobal2.SM_SENDUSERMAKEDRUGITEMLIST:
                    ClientGetSendMakeDrugList(msg.Recog, body);
                    break;
                case Grobal2.SM_SENDUSERSELL:
                    ClientGetSendUserSell(msg.Recog);
                    break;
                case Grobal2.SM_SENDUSERREPAIR:
                    ClientGetSendUserRepair(msg.Recog);
                    break;
                case Grobal2.SM_SENDBUYPRICE:
                    if (MShare.g_SellDlgItem.Item.Name != "")
                    {
                        if (msg.Recog > 0)
                        {
                            if (MShare.g_SellDlgItem.Item.Overlap > 0)
                            {
                                MShare.g_sSellPriceStr = (msg.Recog * MShare.g_SellDlgItem.Dura).ToString() + MShare.g_sGoldName;
                            }
                            else
                            {
                                MShare.g_sSellPriceStr = (msg.Recog).ToString() + MShare.g_sGoldName;
                            }
                        }
                        else
                        {
                            // 金币'
                            MShare.g_sSellPriceStr = "???? " + MShare.g_sGoldName;
                        }
                    }
                    break;
                case Grobal2.SM_SENDBOOKCNT:
                    if (MShare.g_SellDlgItem.Item.Name != "")
                    {
                        if (msg.Recog > 0)
                        {
                            if (MShare.g_SellDlgItem.Item.Overlap > 0)
                            {
                                MShare.g_sSellPriceStr = "换: " + (msg.Recog * MShare.g_SellDlgItem.Dura).ToString() + "卷轴碎片";
                            }
                            else
                            {
                                MShare.g_sSellPriceStr = "换: " + (msg.Recog).ToString() + "卷轴碎片";
                            }
                        }
                        else
                        {
                            switch (msg.Recog)
                            {
                                case 00:
                                    MShare.g_sSellPriceStr = "不可以兑换";
                                    break;
                                case -1:
                                    MShare.g_sSellPriceStr = "不是装备类";
                                    break;
                                case -2:
                                    MShare.g_sSellPriceStr = "装备级别太低";
                                    break;
                                case -3:
                                    MShare.g_sSellPriceStr = "装备级别太高";
                                    break;
                            }
                        }
                    }
                    break;
                case Grobal2.SM_USERSELLITEM_OK:
                    FrmDlg.LastestClickTime = MShare.GetTickCount();
                    MShare.g_MySelf.m_nGold = msg.Recog;
                    MShare.g_SellDlgItemSellWait.Item.Item.Name = "";
                    break;
                case Grobal2.SM_USERSELLITEM_FAIL:
                    FrmDlg.LastestClickTime = MShare.GetTickCount();
                    ClFunc.AddItemBag(MShare.g_SellDlgItemSellWait.Item);
                    MShare.g_SellDlgItemSellWait.Item.Item.Name = "";
                    DebugOutStr("此物品不能出售");
                    break;
                case Grobal2.SM_USEREXCHGITEM_FAIL:
                    FrmDlg.LastestClickTime = MShare.GetTickCount();
                    ClFunc.AddItemBag(MShare.g_SellDlgItemSellWait.Item);
                    MShare.g_SellDlgItemSellWait.Item.Item.Name = "";
                    switch (msg.Recog)
                    {
                        case -1:
                            DebugOutStr("[失败] 摆摊中，不能操作");
                            break;
                        case -2:
                            DebugOutStr("[失败] 物品已经绑定他人");
                            break;
                        case -3:
                            DebugOutStr("[失败] 禁止出售的物品，也不能更换卷轴碎片");
                            break;
                        case -4:
                            DebugOutStr("[失败] 更换卷轴碎片失败");
                            break;
                        case -5:
                            DebugOutStr("[失败] 该不是装备类");
                            break;
                        case -6:
                            DebugOutStr("[失败] 该装备级别太低");
                            break;
                        case -7:
                            DebugOutStr("[失败] 该装备级别太高");
                            break;
                    }
                    break;
                case Grobal2.SM_USERSELLCOUNTITEM_OK:
                    FrmDlg.LastestClickTime = MShare.GetTickCount();
                    MShare.g_MySelf.m_nGold = msg.Recog;
                    ClFunc.SellItemProg(msg.Param, msg.Tag);
                    MShare.g_SellDlgItemSellWait.Item.Item.Name = "";
                    break;
                case Grobal2.SM_USERSELLCOUNTITEM_FAIL:
                    FrmDlg.LastestClickTime = MShare.GetTickCount();
                    ClFunc.AddItemBag(MShare.g_SellDlgItemSellWait.Item);
                    MShare.g_SellDlgItemSellWait.Item.Item.Name = "";
                    DebugOutStr("此物品不能出售");
                    break;
                case Grobal2.SM_SENDREPAIRCOST:
                    if (MShare.g_SellDlgItem.Item.Name != "")
                    {
                        if (msg.Recog >= 0)
                        {
                            // 金币
                            MShare.g_sSellPriceStr = (msg.Recog).ToString() + " " + MShare.g_sGoldName;
                        }
                        else
                        {
                            // 金币
                            MShare.g_sSellPriceStr = "???? " + MShare.g_sGoldName;
                        }
                    }
                    break;
                case Grobal2.SM_USERREPAIRITEM_OK:
                    if (MShare.g_SellDlgItemSellWait.Item.Item.Name != "")
                    {
                        FrmDlg.LastestClickTime = MShare.GetTickCount();
                        MShare.g_MySelf.m_nGold = msg.Recog;
                        MShare.g_SellDlgItemSellWait.Item.Dura = msg.Param;
                        MShare.g_SellDlgItemSellWait.Item.DuraMax = msg.Tag;
                        ClFunc.AddItemBag(MShare.g_SellDlgItemSellWait.Item);
                        MShare.g_SellDlgItemSellWait.Item.Item.Name = "";
                    }
                    break;
                case Grobal2.SM_USERREPAIRITEM_FAIL:
                    FrmDlg.LastestClickTime = MShare.GetTickCount();
                    ClFunc.AddItemBag(MShare.g_SellDlgItemSellWait.Item);
                    MShare.g_SellDlgItemSellWait.Item.Item.Name = "";
                    DebugOutStr("您不能修理此物品");
                    break;
                case Grobal2.SM_ITEMSUMCOUNT_FAIL:
                    if (MShare.g_WaitingUseItem.Item.Item.Name != "")
                    {
                        if (msg.Series != 0)
                        {
                            if (msg.Recog == 0)
                            {
                                ClFunc.HeroAddItemBag(MShare.g_WaitingUseItem.Item);
                                MShare.g_WaitingUseItem.Item.Item.Name = "";
                                DScreen.AddChatBoardString("(英雄)重叠失败,物品最高数量是 " + (Grobal2.MAX_OVERLAPITEM).ToString(), Color.White, Color.Red);
                            }
                            else
                            {
                                MShare.g_WaitingUseItem.Item.Dura = msg.Param;
                                ClFunc.HeroAddItemBag(MShare.g_WaitingUseItem.Item);
                                MShare.g_WaitingUseItem.Item.Item.Name = "";
                            }
                        }
                        else
                        {
                            if (msg.Recog == 0)
                            {
                                ClFunc.AddItemBag(MShare.g_WaitingUseItem.Item, MShare.g_WaitingUseItem.Index);
                                MShare.g_WaitingUseItem.Item.Item.Name = "";
                                DScreen.AddChatBoardString("重叠失败,物品最高数量是 " + (Grobal2.MAX_OVERLAPITEM).ToString(), Color.White, Color.Red);
                            }
                            else
                            {
                                MShare.g_WaitingUseItem.Item.Dura = msg.Param;
                                ClFunc.AddItemBag(MShare.g_WaitingUseItem.Item, MShare.g_WaitingUseItem.Index);
                                MShare.g_WaitingUseItem.Item.Item.Name = "";
                            }
                        }
                    }
                    break;
                case Grobal2.SM_STORAGE_OK:
                case Grobal2.SM_STORAGE_FULL:
                case Grobal2.SM_STORAGE_FAIL:
                    FrmDlg.LastestClickTime = MShare.GetTickCount();
                    if (msg.Ident != Grobal2.SM_STORAGE_OK)
                    {
                        if (msg.Ident == Grobal2.SM_STORAGE_FULL)
                        {
                            DebugOutStr("您的个人仓库已经满了，不能再保管任何东西了");
                        }
                        else
                        {
                            if (msg.Recog == 2)
                            {
                                DebugOutStr("寄存物品失败,同类单个物品最高重叠数量是 " + (Grobal2.MAX_OVERLAPITEM).ToString());
                            }
                            else if (msg.Recog == 3)
                            {
                                MShare.g_SellDlgItemSellWait.Item.Dura = MShare.g_SellDlgItemSellWait.Item.Dura - msg.Param;
                                DScreen.AddChatBoardString(Format("成功寄存 %s %d个", new short[] { MShare.g_SellDlgItemSellWait.Item.Item.Name, msg.Param }), Color.Blue, Color.White);
                            }
                            else
                            {
                                DebugOutStr("您不能寄存物品");
                            }
                        }
                        ClFunc.AddItemBag(MShare.g_SellDlgItemSellWait.Item);
                    }
                    else
                    {
                    }
                    MShare.g_SellDlgItemSellWait.Item.Item.Name = "";
                    break;
                case Grobal2.SM_SAVEITEMLIST:
                    ClientGetSaveItemList(msg.Recog, body);
                    break;
                case Grobal2.SM_TAKEBACKSTORAGEITEM_OK:
                case Grobal2.SM_TAKEBACKSTORAGEITEM_FAIL:
                case Grobal2.SM_TAKEBACKSTORAGEITEM_FULLBAG:
                    FrmDlg.LastestClickTime = MShare.GetTickCount();
                    if (msg.Ident != Grobal2.SM_TAKEBACKSTORAGEITEM_OK)
                    {
                        if (msg.Ident == Grobal2.SM_TAKEBACKSTORAGEITEM_FULLBAG)
                        {
                            DebugOutStr("您无法携带更多物品了");
                        }
                        else
                        {
                            DebugOutStr("您无法取回物品");
                        }
                    }
                    else
                    {
                        FrmDlg.DelStorageItem(msg.Recog, msg.Param);
                    }
                    break;
                case Grobal2.SM_BU.Item.ItemUCCESS:
                    FrmDlg.LastestClickTime = MShare.GetTickCount();
                    MShare.g_MySelf.m_nGold = msg.Recog;
                    FrmDlg.SoldOutGoods(HUtil32.Makelong(msg.Param, msg.Tag));
                    break;
                case Grobal2.SM_BUYITEM_FAIL:
                    FrmDlg.LastestClickTime = MShare.GetTickCount();
                    switch (msg.Recog)
                    {
                        case 1:
                            DebugOutStr("此物品被卖出");
                            break;
                        case 2:
                            DebugOutStr("您无法携带更多物品了");
                            break;
                        case 3:
                            DebugOutStr("您没有足够的钱来购买此物品");
                            break;
                    }
                    break;
                case Grobal2.SM_MAKEDRUG_SUCCESS:
                    FrmDlg.LastestClickTime = MShare.GetTickCount();
                    MShare.g_MySelf.m_nGold = msg.Recog;
                    DebugOutStr("您要的物品已经搞定了");
                    break;
                case Grobal2.SM_MAKEDRUG_FAIL:
                    FrmDlg.LastestClickTime = MShare.GetTickCount();
                    switch (msg.Recog)
                    {
                        case 1:
                            DebugOutStr("未知错误");
                            break;
                        case 2:
                            DebugOutStr("发生了错误");
                            break;
                        case 3:
                            // '金币'
                            DebugOutStr(MShare.g_sGoldName + "不足");
                            break;
                        case 4:
                            DebugOutStr("你缺乏所必需的物品");
                            break;
                    }
                    break;
                case Grobal2.SM_NORMALEFFECT:
                    // type
                    // x
                    // y
                    DrawEffectHum(msg.Series, msg.Param, msg.Tag);
                    break;
                case Grobal2.SM_SENDDETAILGOODSLIST:
                    ClientGetSendDetailGoodsList(msg.Recog, msg.Param, msg.Tag, body);
                    break;
                case Grobal2.SM_TEST:
                    MShare.g_nTestReceiveCount++;
                    break;
                case Grobal2.SM_SENDNOTICE:
                    ClientGetSendNotice(body);
                    break;
                case Grobal2.SM_POSTIONMOVE:
                    ClientGetPositionMove(msg, body);
                    break;
                case Grobal2.SM_GROUPMODECHANGED:
                    if (msg.Param > 0)
                    {
                        MShare.g_boAllowGroup = true;
                        DScreen.AddChatBoardString("[开启组队开关]", GetRGB(219), Color.White);
                    }
                    else
                    {
                        MShare.g_boAllowGroup = false;
                        DScreen.AddChatBoardString("[关闭组队开关]", GetRGB(219), Color.White);
                    }
                    MShare.g_dwChangeGroupModeTick = MShare.GetTickCount();
                    break;
                case Grobal2.SM_CREATEGROUP_OK:
                    MShare.g_dwChangeGroupModeTick = MShare.GetTickCount();
                    MShare.g_boAllowGroup = true;
                    break;
                case Grobal2.SM_CREATEGROUP_FAIL:
                    MShare.g_dwChangeGroupModeTick = MShare.GetTickCount();
                    switch (msg.Recog)
                    {
                        case -1:
                            DebugOutStr("编组还未成立或者你还不够等级创建！");
                            break;
                        case -2:
                            DebugOutStr("输入的人物名称不正确！");
                            break;
                        case -3:
                            DebugOutStr("您想邀请加入编组的人已经加入了其它组！");
                            break;
                        case -4:
                            DebugOutStr("对方不允许编组！");
                            break;
                    }
                    break;
                case Grobal2.SM_GROUPADDMEM_OK:
                    MShare.g_dwChangeGroupModeTick = MShare.GetTickCount();
                    break;
                case Grobal2.SM_GROUPADDMEM_FAIL:
                    // GroupMembers.Add (DeCodeString(body));
                    MShare.g_dwChangeGroupModeTick = MShare.GetTickCount();
                    switch (msg.Recog)
                    {
                        case -1:
                            DebugOutStr("编组还未成立或者你还不够等级创建！");
                            break;
                        case -2:
                            DebugOutStr("输入的人物名称不正确！");
                            break;
                        case -3:
                            DebugOutStr("已经加入编组！");
                            break;
                        case -4:
                            DebugOutStr("对方不允许编组！");
                            break;
                        case -5:
                            DebugOutStr("您想邀请加入编组的人已经加入了其它组！");
                            break;
                    }
                    break;
                case Grobal2.SM_GROUPDELMEM_OK:
                    MShare.g_dwChangeGroupModeTick = MShare.GetTickCount();
                    break;
                case Grobal2.SM_GROUPDELMEM_FAIL:
                    MShare.g_dwChangeGroupModeTick = MShare.GetTickCount();
                    switch (msg.Recog)
                    {
                        case -1:
                            DebugOutStr("编组还未成立或者您还不够等级创建");
                            break;
                        case -2:
                            DebugOutStr("输入的人物名称不正确！");
                            break;
                        case -3:
                            DebugOutStr("此人不在本组中！");
                            break;
                    }
                    break;
                case Grobal2.SM_GROUPCANCEL:
                    MShare.g_GroupMembers.Clear();
                    break;
                case Grobal2.SM_GROUPMEMBERS:
                    ClientGetGroupMembers(EDcode.DeCodeString(body));
                    break;
                case Grobal2.SM_OPENGUILDDLG:
                    MShare.g_dwQueryMsgTick = MShare.GetTickCount();
                    ClientGetOpenGuildDlg(body);
                    break;
                case Grobal2.SM_SENDGUILDMEMBERLIST:
                    MShare.g_dwQueryMsgTick = MShare.GetTickCount();
                    ClientGetSendGuildMemberList(body);
                    break;
                case Grobal2.SM_OPENGUILDDLG_FAIL:
                    MShare.g_dwQueryMsgTick = MShare.GetTickCount();
                    DebugOutStr("您还没有加入行会！");
                    break;
                case Grobal2.SM_DEALTRY_FAIL:
                    MShare.g_dwQueryMsgTick = MShare.GetTickCount();
                    DebugOutStr("只有二人面对面才能进行交易");
                    break;
                case Grobal2.SM_DEALMENU:
                    MShare.g_dwQueryMsgTick = MShare.GetTickCount();
                    MShare.g_sDealWho = EDcode.DeCodeString(body);
                    FrmDlg.OpenDealDlg;
                    break;
                case Grobal2.SM_DEALCANCEL:
                    ClFunc.MoveDealItemToBag();
                    if (MShare.g_DealDlgItem.Item.Name != "")
                    {
                        ClFunc.AddItemBag(MShare.g_DealDlgItem);
                        MShare.g_DealDlgItem.Item.Name = "";
                    }
                    if (MShare.g_nDealGold > 0)
                    {
                        MShare.g_MySelf.m_nGold = MShare.g_MySelf.m_nGold + MShare.g_nDealGold;
                        MShare.g_nDealGold = 0;
                    }
                    FrmDlg.CloseDealDlg;
                    break;
                case Grobal2.SM_DEALADDITEM_OK:
                    MShare.g_dwDealActionTick = MShare.GetTickCount();
                    if (MShare.g_DealDlgItem.Item.Name != "")
                    {
                        ClFunc.ResultDealItem(MShare.g_DealDlgItem, msg.Recog, msg.Param);
                        MShare.g_DealDlgItem.Item.Name = "";
                    }
                    break;
                case Grobal2.SM_DEALADDITEM_FAIL:
                    MShare.g_dwDealActionTick = MShare.GetTickCount();
                    if (MShare.g_DealDlgItem.Item.Name != "")
                    {
                        ClFunc.AddItemBag(MShare.g_DealDlgItem);
                        MShare.g_DealDlgItem.Item.Name = "";
                    }
                    if (msg.Recog != 0)
                    {
                        DScreen.AddChatBoardString("重叠失败,物品最高数量是 " + (Grobal2.MAX_OVERLAPITEM).ToString(), Color.White, Color.Red);
                    }
                    break;
                case Grobal2.SM_DEALDELITEM_OK:
                    MShare.g_dwDealActionTick = MShare.GetTickCount();
                    if (MShare.g_DealDlgItem.Item.Name != "")
                    {
                        MShare.g_DealDlgItem.Item.Name = "";
                    }
                    break;
                case Grobal2.SM_DEALDELITEM_FAIL:
                    MShare.g_dwDealActionTick = MShare.GetTickCount();
                    if (MShare.g_DealDlgItem.Item.Name != "")
                    {
                        ClFunc.AddDealItem(MShare.g_DealDlgItem);
                        MShare.g_DealDlgItem.Item.Name = "";
                    }
                    FrmDlg.CancelItemMoving;
                    break;
                case Grobal2.SM_DEALREMOTEADDITEM:
                    ClientGetDealRemoteAddItem(body);
                    break;
                case Grobal2.SM_DEALREMOTEDELITEM:
                    ClientGetDealRemoteDelItem(body);
                    break;
                case Grobal2.SM_DEALCHGGOLD_OK:
                    MShare.g_nDealGold = msg.Recog;
                    MShare.g_MySelf.m_nGold = HUtil32.Makelong(msg.Param, msg.Tag);
                    MShare.g_dwDealActionTick = MShare.GetTickCount();
                    break;
                case Grobal2.SM_DEALCHGGOLD_FAIL:
                    MShare.g_nDealGold = msg.Recog;
                    MShare.g_MySelf.m_nGold = HUtil32.Makelong(msg.Param, msg.Tag);
                    MShare.g_dwDealActionTick = MShare.GetTickCount();
                    break;
                case Grobal2.SM_DEALREMOTECHGGOLD:
                    MShare.g_nDealRemoteGold = msg.Recog;
                    break;
                case Grobal2.SM_DEALSUCCESS:
                    FrmDlg.CloseDealDlg;
                    break;
                case Grobal2.SM_SENDUSERSTORAGEITEM:
                    ClientGetSendUserStorage(msg.Recog);
                    break;
                case Grobal2.SM_READMINIMAP_OK:
                    MShare.g_dwQueryMsgTick = MShare.GetTickCount();
                    ClientGetReadMiniMap(msg.Param);
                    break;
                case Grobal2.SM_READMINIMAP_FAIL:
                    MShare.g_dwQueryMsgTick = MShare.GetTickCount();
                    DScreen.AddChatBoardString("没有小地图", Color.White, Color.Red);
                    MShare.g_nMiniMapIndex = -1;
                    break;
                case Grobal2.SM_CHANGEGUILDNAME:
                    ClientGetChangeGuildName(EDcode.DeCodeString(body));
                    break;
                case Grobal2.SM_SENDUSERSTATE:
                    ClientGetSendUserState(body);
                    break;
                case Grobal2.SM_GUILDADDMEMBER_OK:
                    SendGuildMemberList();
                    break;
                case Grobal2.SM_GUILDADDMEMBER_FAIL:
                    switch (msg.Recog)
                    {
                        case 1:
                            DebugOutStr("你没有权利使用这个命令");
                            break;
                        case 2:
                            DebugOutStr("想加入进来的成员应该来面对掌门人");
                            break;
                        case 3:
                            DebugOutStr("对方已经加入我们的行会");
                            break;
                        case 4:
                            DebugOutStr("对方已经加入其他行会");
                            break;
                        case 5:
                            DebugOutStr("对方不允许加入行会");
                            break;
                    }
                    break;
                case Grobal2.SM_GUILDDELMEMBER_OK:
                    SendGuildMemberList();
                    break;
                case Grobal2.SM_GUILDDELMEMBER_FAIL:
                    switch (msg.Recog)
                    {
                        case 1:
                            DebugOutStr("不能使用命令！");
                            break;
                        case 2:
                            DebugOutStr("此人非本行会成员！");
                            break;
                        case 3:
                            DebugOutStr("行会掌门人不能开除自己！");
                            break;
                        case 4:
                            DebugOutStr("不能使用命令！");
                            break;
                    }
                    break;
                case Grobal2.SM_GUILDRANKUPDATE_FAIL:
                    switch (msg.Recog)
                    {
                        case -2:
                            DebugOutStr("[提示信息] 掌门人位置不能为空");
                            break;
                        case -3:
                            DebugOutStr("[提示信息] 新的行会掌门人已经被传位");
                            break;
                        case -4:
                            DebugOutStr("[提示信息] 一个行会最多只能有二个掌门人");
                            break;
                        case -5:
                            DebugOutStr("[提示信息] 掌门人位置不能为空");
                            break;
                        case -6:
                            DebugOutStr("[提示信息] 不能添加成员/删除成员");
                            break;
                        case -7:
                            DebugOutStr("[提示信息] 职位重复或者出错");
                            break;
                    }
                    break;
                case Grobal2.SM_GUILDMAKEALLY_OK:
                case Grobal2.SM_GUILDMAKEALLY_FAIL:
                    switch (msg.Recog)
                    {
                        case -1:
                            DebugOutStr("您无此权限！");
                            break;
                        case -2:
                            DebugOutStr("结盟失败！");
                            break;
                        case -3:
                            DebugOutStr("行会结盟必须双方掌门人面对面！");
                            break;
                        case -4:
                            DebugOutStr("对方行会掌门人不允许结盟！");
                            break;
                    }
                    break;
                case Grobal2.SM_GUILDBREAKALLY_OK:
                case Grobal2.SM_GUILDBREAKALLY_FAIL:
                    switch (msg.Recog)
                    {
                        case -1:
                            DebugOutStr("解除结盟！");
                            break;
                        case -2:
                            DebugOutStr("此行会不是您行会的结盟行会！");
                            break;
                        case -3:
                            DebugOutStr("没有此行会！");
                            break;
                    }
                    break;
                case Grobal2.SM_BUILDGUILD_OK:
                    FrmDlg.LastestClickTime = MShare.GetTickCount();
                    DebugOutStr("行会建立成功");
                    break;
                case Grobal2.SM_BUILDGUILD_FAIL:
                    FrmDlg.LastestClickTime = MShare.GetTickCount();
                    switch (msg.Recog)
                    {
                        case -1:
                            DebugOutStr("您已经加入其它行会");
                            break;
                        case -2:
                            DebugOutStr("缺少创建费用");
                            break;
                        case -3:
                            DebugOutStr("你没有准备好需要的全部物品");
                            break;
                        default:
                            DebugOutStr("创建行会失败！！！");
                            break;
                    }
                    break;
                case Grobal2.SM_MENU_OK:
                    FrmDlg.LastestClickTime = MShare.GetTickCount();
                    if (body != "")
                    {
                        DebugOutStr(EDcode.DeCodeString(body));
                    }
                    break;
                case Grobal2.SM_DLGMSG:
                    if (body != "")
                    {
                        DebugOutStr(EDcode.DeCodeString(body));
                    }
                    break;
                case Grobal2.SM_DONATE_OK:
                    FrmDlg.LastestClickTime = MShare.GetTickCount();
                    break;
                case Grobal2.SM_DONATE_FAIL:
                    FrmDlg.LastestClickTime = MShare.GetTickCount();
                    break;
                case Grobal2.SM_PLAYDICE:
                    n = HUtil32.GetCodeMsgSize(sizeof(TMessageBodyWL) * 4 / 3);
                    body2 = body.Substring(n + 1 - 1, body.Length);
                    data = EDcode.DeCodeString(body2);
                    body2 = body.Substring(1 - 1, n);
                    EDcode.DecodeBuffer(body2, wl);
                    FrmDlg.m_nDiceCount = msg.Param;
                    // QuestActionInfo.nParam1
                    FrmDlg.m_Dice[0].nDicePoint = LoByte(HUtil32.LoWord(wl.lParam1));
                    // UserHuman.m_DyVal[0]
                    FrmDlg.m_Dice[1].nDicePoint = HiByte(HUtil32.LoWord(wl.lParam1));
                    // UserHuman.m_DyVal[0]
                    FrmDlg.m_Dice[2].nDicePoint = LoByte(HUtil32.HiWord(wl.lParam1));
                    // UserHuman.m_DyVal[0]
                    FrmDlg.m_Dice[3].nDicePoint = HiByte(HUtil32.HiWord(wl.lParam1));
                    // UserHuman.m_DyVal[0]
                    FrmDlg.m_Dice[4].nDicePoint = LoByte(HUtil32.LoWord(wl.lParam2));
                    // UserHuman.m_DyVal[0]
                    FrmDlg.m_Dice[5].nDicePoint = HiByte(HUtil32.LoWord(wl.lParam2));
                    // UserHuman.m_DyVal[0]
                    FrmDlg.m_Dice[6].nDicePoint = LoByte(HUtil32.HiWord(wl.lParam2));
                    // UserHuman.m_DyVal[0]
                    FrmDlg.m_Dice[7].nDicePoint = HiByte(HUtil32.HiWord(wl.lParam2));
                    // UserHuman.m_DyVal[0]
                    FrmDlg.m_Dice[8].nDicePoint = LoByte(HUtil32.LoWord(wl.lTag1));
                    // UserHuman.m_DyVal[0]
                    FrmDlg.m_Dice[9].nDicePoint = HiByte(HUtil32.LoWord(wl.lTag1));
                    // UserHuman.m_DyVal[0]
                    FrmDlg.DialogSize = 0;
                    DebugOutStr("", new object[] { });
                    SendMerchantDlgSelect(msg.Recog, data);
                    break;
                case Grobal2.SM_PASSWORDSTATUS:
                    ClientGetPasswordStatus(msg, body);
                    break;
                case Grobal2.SM_MARKET_LIST:
                    // SM_GETREGINFO: ClientGetRegInfo(@Msg,Body);
                    MaketSystem.Units.MaketSystem.g_Market.OnMsgWriteData(msg, body);
                    FrmDlg.ShowItemMarketDlg;
                    break;
                case Grobal2.SM_MARKET_RESULT:
                    switch (msg.Param)
                    {
                        case Grobal2.UMRESULT_SUCCESS:
                            break;
                        case Grobal2.UMResult_Fail:
                            // Market System..
                            DebugOutStr("[失败]: 使用交易市场出错, 请告知管理员.");
                            break;
                        case Grobal2.UMResult_ReadFail:
                            DebugOutStr("[失败]: 读取寄售物品列表出错, 请告知管理员.");
                            break;
                        case Grobal2.UMResult_WriteFail:
                            DebugOutStr("[失败]: 存储寄售物品出错, 请告知管理员.");
                            break;
                        case Grobal2.UMResult_ReadyToSell:
                            ClientGetSendUserMaketSell(msg.Recog);
                            break;
                        case Grobal2.UMResult_OverSellCount:
                            DebugOutStr("[失败]: 寄售物品超过限制. 最多可以寄售 " + (Grobal2.MARKET_MAX_SELL_COUNT).ToString() + " 个物品.");
                            break;
                        case Grobal2.UMResult_LessMoney:
                            FrmDlg.LastestClickTime = MShare.GetTickCount();
                            if (MShare.g_SellDlgItemSellWait.Item.Item.Name != "")
                            {
                                ClFunc.AddItemBag(MShare.g_SellDlgItemSellWait.Item);
                            }
                            MShare.g_SellDlgItemSellWait.Item.Item.Name = "";
                            DebugOutStr("[失败]: 你携带的金币不足以支付寄售的费用.");
                            break;
                        case Grobal2.UMResult_LessLevel:
                            DebugOutStr("[失败]: 需要 " + (Grobal2.MARKET_ALLOW_LEVEL).ToString() + " 级以上才能使用交易市场.");
                            break;
                        case Grobal2.UMResult_MaxBagItemCount:
                            DebugOutStr("[失败]: 背包空位不足.");
                            break;
                        case Grobal2.UMResult_NoItem:
                            DebugOutStr("[失败]: 物品不存在.");
                            break;
                        case Grobal2.UMResult_DontSell:
                            FrmDlg.LastestClickTime = MShare.GetTickCount();
                            ClFunc.AddItemBag(MShare.g_SellDlgItemSellWait.Item);
                            MShare.g_SellDlgItemSellWait.Item.Item.Name = "";
                            DebugOutStr("[失败]: 该物品不能寄售.");
                            break;
                        case Grobal2.UMResult_DontBuy:
                            DebugOutStr("[失败]: 不能购买自己的物品.");
                            break;
                        case Grobal2.UMResult_MarketNotReady:
                            // UMResult_DontGetMoney: ;
                            DebugOutStr("[失败]: 交易市场未准备就绪.");
                            break;
                        case Grobal2.UMResult_LessTrustMoney:
                            FrmDlg.LastestClickTime = MShare.GetTickCount();
                            if (MShare.g_SellDlgItemSellWait.Item.Item.Name != "")
                            {
                                ClFunc.AddItemBag(MShare.g_SellDlgItemSellWait.Item);
                            }
                            MShare.g_SellDlgItemSellWait.Item.Item.Name = "";
                            DebugOutStr("[失败]: 寄售物品价格至少 " + (Grobal2.MARKET_CHARGE_MONEY).ToString() + " 金币.");
                            break;
                        case Grobal2.UMResult_MaxTrustMoney:
                            DebugOutStr("[失败]: 寄售物品价格不能大于 " + (Grobal2.MARKET_MAX_TRUST_MONEY).ToString() + " 金币.");
                            break;
                        case Grobal2.UMResult_CancelFail:
                            DebugOutStr("[失败]: 该物品不属于你.");
                            break;
                        case Grobal2.UMResult_OverMoney:
                            DebugOutStr("[失败]: 达到金币存放的限额.");
                            break;
                        case Grobal2.UMResult_SellOK:
                            FrmDlg.DSellDlg.Visible = false;
                            FrmDlg.LastestClickTime = MShare.GetTickCount();
                            MShare.g_SellDlgItemSellWait.Item.Item.Name = "";
                            break;
                        case Grobal2.UMResult_BuyOK:
                            break;
                        case Grobal2.UMResult_CancelOK:
                            break;
                        case Grobal2.UMResult_GetPayOK:
                            break;
                        default:
                            break;
                    }
                    break;
                case Grobal2.SM_OPENBIGDIALOGBOX:
                    // 打开NPC大对话框  Development 2019-01-14
                    MShare.g_nCurMerchantFaceIdx = msg.Recog;
                    FrmDlg.ResetMenuDlg;
                    FrmDlg.CloseMDlg;
                    FrmDlg.DMerchantBigDlg.Floating = true;
                    try
                    {
                        FrmDlg.DMerchantBigDlg.Visible = true;
                    }
                    finally
                    {
                        FrmDlg.DMerchantBigDlg.Floating = false;
                    }
                    break;
                default:
                    break;
                    // if g_MySelf = nil then Exit;
                    // g_PlayScene.MemoLog.Lines.Add('Ident: ' + IntToStr(Msg.ident));
                    // g_PlayScene.MemoLog.Lines.Add('Recog: ' + IntToStr(Msg.Recog));
                    // g_PlayScene.MemoLog.Lines.Add('Param: ' + IntToStr(Msg.param));
                    // g_PlayScene.MemoLog.Lines.Add('Tag: ' + IntToStr(Msg.tag));
                    // g_PlayScene.MemoLog.Lines.Add('Series: ' + IntToStr(Msg.series));
            }
        }

        private void ClientGetPasswdSuccess(string body)
        {
            string Str;
            string runaddr;
            string runport;
            string certifystr;
            Str = EDcode.DeCodeString(body);
            Str = HUtil32.GetValidStr3(Str, ref runaddr, new string[] { "/" });
            Str = HUtil32.GetValidStr3(Str, ref runport, new string[] { "/" });
            Str = HUtil32.GetValidStr3(Str, ref certifystr, new string[] { "/" });
            Certification = HUtil32.Str_ToInt(certifystr, 0);
            CSocket.Active = false;
            CSocket.Host = "";
            CSocket.Port = 0;
            FrmDlg.DWinSelServer.Visible = false;
            MShare.g_sSelChrAddr = runaddr;
            MShare.g_nSelChrPort = HUtil32.Str_ToInt(runport, 0);
            while (true)
            {
                if (!CSocket.Socket.Connected)
                {
                    MShare.g_ConnectionStep = TConnectionStep.cnsSelChr;
                    MShare.g_boQuerySelChar = true;
                    MShare.g_sSelChrAddr = runaddr;
                    MShare.g_nSelChrPort = HUtil32.Str_ToInt(runport, 0);
                    CSocket.Address = MShare.g_sSelChrAddr;
                    CSocket.Port = MShare.g_nSelChrPort;
                    this.Active = true;
                    break;
                }
                Application.ProcessMessages;
                if (Application.Terminated)
                {
                    break;
                }
                WaitAndPass(10);
            }
        }

        private void ClientGetPasswordOK(ClientPacket msg, string sBody)
        {
            int i;
            string sServerName;
            string sServerStatus;
            int nCount;
            sBody = EDcode.DeCodeString(sBody);
            nCount = HUtil32._MIN(6, msg.Series);
            MShare.g_ServerList.Clear();
            for (i = 0; i < nCount; i++)
            {
                sBody = HUtil32.GetValidStr3(sBody, ref sServerName, new string[] { "/" });
                sBody = HUtil32.GetValidStr3(sBody, ref sServerStatus, new string[] { "/" });
                MShare.g_ServerList.Add(sServerName, ((HUtil32.Str_ToInt(sServerStatus, 0)) as Object));
            }
            MShare.g_wAvailIDDay = HUtil32.LoWord(msg.Recog);
            MShare.g_wAvailIDHour = HUtil32.HiWord(msg.Recog);
            MShare.g_wAvailIPDay = msg.Param;
            MShare.g_wAvailIPHour = msg.Tag;
            if ((MShare.g_wAvailIDHour % 60) > 0)
            {
                // 付费时长大于60
                DebugOutStr("个人帐户的期限: 剩余 " + (MShare.g_wAvailIDHour / 60).ToString() + " 小时 " + (MShare.g_wAvailIDHour % 60).ToString() + " 分钟.");
            }
            else if (MShare.g_wAvailIDHour > 0)
            {
                DebugOutStr("个人帐户的期限: 剩余 " + (MShare.g_wAvailIDHour).ToString() + " 分钟.");
            }
            ClientGetSelectServer();
        }

        private void ClientGetSelectServer()
        {
            LoginScene.HideLoginBox();
            FrmDlg.ShowSelectServer;
        }

        private void ClientGetNeedUpdateAccount(string body)
        {
            TUserEntry ue;
            EDcode.DecodeBuffer(body, ue, sizeof(TUserEntry));
            LoginScene.UpdateAccountInfos(ue);
        }

        private void ClientGetReceiveChrs(string body)
        {
            int i;
            int select;
            string Str;
            string uname;
            string sjob;
            string shair;
            string slevel;
            string ssex;
            if (MShare.g_boOpenAutoPlay && (MShare.g_nAPReLogon == 1))
            {
                MShare.g_nAPReLogon = 2;
                MShare.g_nAPReLogonWaitTick = MShare.GetTickCount();
                MShare.g_nAPReLogonWaitTime = 5000 + (new System.Random(10)).Next() * 1000;
            }
            SelectChrScene.ClearChrs();
            Str = EDcode.DeCodeString(body);
            for (i = 0; i <= 1; i++)
            {
                Str = HUtil32.GetValidStr3(Str, ref uname, new string[] { "/" });
                Str = HUtil32.GetValidStr3(Str, ref sjob, new string[] { "/" });
                Str = HUtil32.GetValidStr3(Str, ref shair, new string[] { "/" });
                Str = HUtil32.GetValidStr3(Str, ref slevel, new string[] { "/" });
                Str = HUtil32.GetValidStr3(Str, ref ssex, new string[] { "/" });
                select = 0;
                if ((uname != "") && (slevel != "") && (ssex != ""))
                {
                    if (uname[1] == "*")
                    {
                        select = i;
                        uname = uname.Substring(2 - 1, uname.Length - 1);
                    }
                    SelectChrScene.AddChr(uname, HUtil32.Str_ToInt(sjob, 0), HUtil32.Str_ToInt(shair, 0), HUtil32.Str_ToInt(slevel, 0), HUtil32.Str_ToInt(ssex, 0));
                }
                TSelectChrScene _wvar1 = SelectChrScene;
                if (select == 0)
                {
                    _wvar1.ChrArr[0].FreezeState = false;
                    _wvar1.ChrArr[0].Selected = true;
                    _wvar1.ChrArr[1].FreezeState = true;
                    _wvar1.ChrArr[1].Selected = false;
                }
                else
                {
                    _wvar1.ChrArr[0].FreezeState = true;
                    _wvar1.ChrArr[0].Selected = false;
                    _wvar1.ChrArr[1].FreezeState = false;
                    _wvar1.ChrArr[1].Selected = true;
                }
            }
        }

        private void ClientGetStartPlay(string body)
        {
            string Str;
            string addr;
            string sport;
            Str = EDcode.DeCodeString(body);
            sport = HUtil32.GetValidStr3(Str, ref addr, new string[] { "/" });
            MShare.g_nRunServerPort = HUtil32.Str_ToInt(sport, 0);
            MShare.g_sRunServerAddr = addr;
            CSocket.Active = false;
            CSocket.Host = "";
            CSocket.Port = 0;
            while (true)
            {
                if (!CSocket.Socket.Connected)
                {
                    MShare.g_ConnectionStep = TConnectionStep.cnsPlay;
                    CSocket.Address = MShare.g_sRunServerAddr;
                    CSocket.Port = MShare.g_nRunServerPort;
                    this.Active = true;
                    SocStr = "";
                    BufferStr = "";
                    break;
                }
                Application.ProcessMessages;
                if (Application.Terminated)
                {
                    break;
                }
                WaitAndPass(10);
            }
        }

        private void ClientGetReconnect(string body)
        {
            string Str;
            string addr;
            string sport;
            Str = EDcode.DeCodeString(body);
            sport = HUtil32.GetValidStr3(Str, ref addr, new string[] { "/" });
            SaveBagsData();
            MShare.g_boServerChanging = true;
            CSocket.Active = false;
            CSocket.Host = "";
            CSocket.Port = 0;
            while (true)
            {
                if (!CSocket.Socket.Connected)
                {
                    MShare.g_ConnectionStep = TConnectionStep.cnsPlay;
                    CSocket.Address = addr;
                    CSocket.Port = HUtil32.Str_ToInt(sport, 0);
                    this.Active = true;
                    SocStr = "";
                    BufferStr = "";
                    break;
                }
                Application.ProcessMessages;
                if (Application.Terminated)
                {
                    break;
                }
                WaitAndPass(10);
            }
        }

        private void ClientGetMapDescription(ClientPacket msg, string sBody)
        {
            string sTitle;
            sBody = EDcode.DeCodeString(sBody);
            sTitle = sBody;
            MShare.g_sMapTitle = sTitle;
            LoadWayPoint();
            if (!MShare.g_gcGeneral[11])
            {
                MShare.g_nLastMapMusic = msg.Recog;
            }
            else
            {
                if (msg.Recog == -1)
                {
                    MShare.g_nLastMapMusic = -1;
                }
                if (MShare.g_nLastMapMusic != msg.Recog)
                {
                    MShare.g_nLastMapMusic = msg.Recog;
                }
            }
        }

        private void ClientGetGameGoldName(ClientPacket msg, string sBody)
        {
            string sData;
            if (sBody != "")
            {
                sBody = EDcode.DeCodeString(sBody);
                sBody = HUtil32.GetValidStr3(sBody, ref sData, new char[] { '\r' });
                MShare.g_sGameGoldName = sData;
                MShare.g_sGamePointName = sBody;
            }
            MShare.g_MySelf.m_nGameGold = msg.Recog;
            MShare.g_MySelf.m_nGamePoint = HUtil32.Makelong(msg.Param, msg.Tag);
        }

        private void ClientGetAdjustBonus(int bonus, string body)
        {
            string str1;
            string Str2;
            string str3;
            MShare.g_nBonusPoint = bonus;
            body = HUtil32.GetValidStr3(body, ref str1, new string[] { "/" });
            str3 = HUtil32.GetValidStr3(body, ref Str2, new string[] { "/" });
            EDcode.DecodeBuffer(str1, MShare.g_BonusTick, sizeof(TNakedAbility));
            EDcode.DecodeBuffer(Str2, MShare.g_BonusAbil, sizeof(TNakedAbility));
            EDcode.DecodeBuffer(str3, MShare.g_NakedAbil, sizeof(TNakedAbility));
            FillChar(MShare.g_BonusAbilChg, sizeof(TNakedAbility), '\0');
        }

        private void ClientGetAddItem(int Hint, string body)
        {
            TClientItem cu;
            if (body != "")
            {
                EDcode.DecodeBuffer(body, cu, sizeof(TClientItem));
                ClFunc.AddItemBag(cu);
                if (Hint != 0)
                {
                    DScreen.AddSysMsg(cu.s.Name + " 被发现");
                }
            }
        }

        private void ClientHeroGetAd.Item.Itemtring body)
        {
            TClientItem cu;
            if (body != "")
            {
                EDcode.DecodeBuffer(body, cu, sizeof(TClientItem));
                ClFunc.HeroAddItemBag(cu);
                DScreen.AddSysMsg(cu.s.Name + " 在英雄包裹内被发现");
            }
        }

        private void ClientGetUpdat.Item.Itemtring body)
        {
            int i;
            TClientItem cu;
            if (body != "")
            {
                EDcode.DecodeBuffer(body, cu, sizeof(TClientItem));
                ClFunc.UpdateItemBag(cu);
                for (i = MShare.g_UseItems.GetLowerBound(0); i <= MShare.g_UseItems.GetUpperBound(0); i++)
                {
                    if ((MShare.g_UseItems[i].s.Name == cu.s.Name) && (MShare.g_UseItems[i].MakeIndex == cu.MakeIndex))
                    {
                        MShare.g_UseItems[i] = cu;
                    }
                }
                if ((MShare.g_SellDlgItem.Item.Name != "") && (MShare.g_SellDlgItem.MakeIndex == cu.MakeIndex))
                {
                    MShare.g_SellDlgItem = cu;
                }
                for (i = 0; i <= 1; i++)
                {
                    if ((MShare.g_TIItems[i].Item.MakeIndex == cu.MakeIndex) && (MShare.g_TIItems[i].Item.Item.Name != ""))
                    {
                        MShare.g_TIItems[i].Item = cu;
                        if (i == 0)
                        {
                            MShare.GetTIHintString1(1, MShare.g_TIItems[0].Item);
                        }
                    }
                }
                MShare.AutoPutOntiBooks();
                for (i = 0; i <= 1; i++)
                {
                    if ((MShare.g_spItems[i].Item.MakeIndex == cu.MakeIndex) && (MShare.g_spItems[i].Item.Item.Name != ""))
                    {
                        MShare.g_spItems[i].Item = cu;
                    }
                }
            }
        }

        private void ClientHeroGetUpdat.Item.Itemtring body)
        {
            int i;
            TClientItem cu;
            if (body != "")
            {
                EDcode.DecodeBuffer(body, cu, sizeof(TClientItem));
                ClFunc.HeroUpdateItemBag(cu);
                for (i = MShare.g_HeroUseItems.GetLowerBound(0); i <= MShare.g_HeroUseItems.GetUpperBound(0); i++)
                {
                    if ((MShare.g_HeroUseItems[i].s.Name == cu.s.Name) && (MShare.g_HeroUseItems[i].MakeIndex == cu.MakeIndex))
                    {
                        MShare.g_HeroUseItems[i] = cu;
                    }
                }
            }
        }

        private void ClientGetDe.Item.Itemtring body)
        {
            int i;
            TClientItem cu;
            if (body != "")
            {
                EDcode.DecodeBuffer(body, cu, sizeof(TClientItem));
                ClFunc.DelItemBag(cu.s.Name, cu.MakeIndex);
                for (i = MShare.g_UseItems.GetLowerBound(0); i <= MShare.g_UseItems.GetUpperBound(0); i++)
                {
                    if ((MShare.g_UseItems[i].s.Name == cu.s.Name) && (MShare.g_UseItems[i].MakeIndex == cu.MakeIndex))
                    {
                        MShare.g_UseItems[i].s.Name = "";
                    }
                }
                for (i = 0; i <= 1; i++)
                {
                    if ((MShare.g_TIItems[i].Item.MakeIndex == cu.MakeIndex))
                    {
                        MShare.g_TIItems[i].Item.Item.Name = "";
                        if (i == 0)
                        {
                            MShare.GetTIHintString1(0);
                        }
                    }
                }
                for (i = 0; i <= 1; i++)
                {
                    if ((MShare.g_spItems[i].Item.MakeIndex == cu.MakeIndex))
                    {
                        MShare.g_spItems[i].Item.Item.Name = "";
                    }
                }
            }
        }

        private void ClientGetDelItems(string body, short wOnlyBag)
        {
            int i;
            int iindex;
            string Str;
            string iname;
            TClientItem cu;
            body = EDcode.DeCodeString(body);
            while (body != "")
            {
                body = HUtil32.GetValidStr3(body, ref iname, new string[] { "/" });
                body = HUtil32.GetValidStr3(body, ref Str, new string[] { "/" });
                if ((iname != "") && (Str != ""))
                {
                    iindex = HUtil32.Str_ToInt(Str, 0);
                    ClFunc.DelItemBag(iname, iindex);
                    if (wOnlyBag == 0)
                    {
                        for (i = MShare.g_UseItems.GetLowerBound(0); i <= MShare.g_UseItems.GetUpperBound(0); i++)
                        {
                            if ((MShare.g_UseItems[i].s.Name == iname) && (MShare.g_UseItems[i].MakeIndex == iindex))
                            {
                                MShare.g_UseItems[i].s.Name = "";
                                break;
                            }
                        }
                    }
                    for (i = 0; i <= 1; i++)
                    {
                        if ((MShare.g_TIItems[i].Item.MakeIndex == cu.MakeIndex))
                        {
                            MShare.g_TIItems[i].Item.Item.Name = "";
                            if (i == 0)
                            {
                                MShare.GetTIHintString1(0);
                            }
                        }
                    }
                    for (i = 0; i <= 1; i++)
                    {
                        if ((MShare.g_spItems[i].Item.MakeIndex == cu.MakeIndex))
                        {
                            MShare.g_spItems[i].Item.Item.Name = "";
                        }
                    }
                }
                else
                {
                    break;
                }
            }
        }

        public bool ClientGetBagItmes_CompareItemArr()
        {
            bool result;
            int i;
            int j;
            bool flag;
            flag = true;
            for (i = 0; i < MShare.MAXBAGITEMCL; i++)
            {
                if (ItemSaveArr[i].s.Name != "")
                {
                    flag = false;
                    for (j = 0; j < MShare.MAXBAGITEMCL; j++)
                    {
                        if ((MShare.g_ItemArr[j].s.Name == ItemSaveArr[i].s.Name) && (MShare.g_ItemArr[j].MakeIndex == ItemSaveArr[i].MakeIndex))
                        {
                            if ((MShare.g_ItemArr[j].Dura == ItemSaveArr[i].Dura) && (MShare.g_ItemArr[j].DuraMax == ItemSaveArr[i].DuraMax))
                            {
                                flag = true;
                            }
                            break;
                        }
                    }
                    if (!flag)
                    {
                        break;
                    }
                }
            }
            if (flag)
            {
                for (i = 0; i < MShare.MAXBAGITEMCL; i++)
                {
                    if (MShare.g_ItemArr[i].Item.Name != "")
                    {
                        flag = false;
                        for (j = 0; j < MShare.MAXBAGITEMCL; j++)
                        {
                            if ((MShare.g_ItemArr[i].Item.Name == ItemSaveArr[j].s.Name) && (MShare.g_ItemArr[i].MakeIndex == ItemSaveArr[j].MakeIndex))
                            {
                                if ((MShare.g_ItemArr[i].Dura == ItemSaveArr[j].Dura) && (MShare.g_ItemArr[i].DuraMax == ItemSaveArr[j].DuraMax))
                                {
                                    flag = true;
                                }
                                break;
                            }
                        }
                        if (!flag)
                        {
                            break;
                        }
                    }
                }
            }
            result = flag;
            return result;
        }

        private void ClientGetBagItmes(string body)
        {
            int i;
            int k;
            string Str;
            TClientItem cu;
            TClientItem[] ItemSaveArr = new TClientItem[MShare.MAXBAGITEMCL - 1 + 1];
            MShare.g_SellDlgItem.Item.Name = "";
            //FillChar(MShare.g_RefineItems, sizeof(TMovingItem) * 3, '\0');
            //FillChar(MShare.g_BuildAcuses, sizeof(MShare.g_BuildAcuses), '\0');
            //FillChar(MShare.g_ItemArr, sizeof(TClientItem) * MShare.MAXBAGITEMCL, '\0');
            //FillChar(MShare.g_TIItems, sizeof(MShare.g_TIItems), '\0');
            //FillChar(MShare.g_spItems, sizeof(MShare.g_spItems), '\0');
            if ((MShare.g_MovingItem.Item.Item.Name != "") && (MShare.IsBagItem(MShare.g_MovingItem.Index)))
            {
                MShare.g_MovingItem.Item.Item.Name = "";
                MShare.g_boItemMoving = false;
            }
            while (true)
            {
                if (body == "")
                {
                    break;
                }
                body = HUtil32.GetValidStr3(body, ref Str, new string[] { "/" });
                EDcode.DecodeBuffer(Str, cu, sizeof(TClientItem));
                ClFunc.AddItemBag(cu);
            }
            FillChar(ItemSaveArr, sizeof(TClientItem) * MShare.MAXBAGITEMCL, '\0');
            ClFunc.Loadbagsdat(".\\Config\\" + MShare.g_sServerName + "." + m_sCharName + ".itm-plus", ItemSaveArr);
            if (ClientGetBagItmes_CompareItemArr())
            {
                Move(ItemSaveArr, MShare.g_ItemArr, sizeof(TClientItem) * MShare.MAXBAGITEMCL);
            }
            ClFunc.ArrangeItembag();
            MShare.g_boBagLoaded = true;
            if (MShare.g_MySelf != null)
            {
                if (!MShare.g_MySelf.m_StallMgr.OnSale)
                {
                    for (i = 0; i <= 9; i++)
                    {
                        if (MShare.g_MySelf.m_StallMgr.mBlock.Items[i].s.Name != "")
                        {
                            ClFunc.UpdateBagStallItem(MShare.g_MySelf.m_StallMgr.mBlock.Items[i], 4);
                        }
                    }
                }
                else
                {
                    for (i = 0; i <= 9; i++)
                    {
                        if (MShare.g_MySelf.m_StallMgr.mBlock.Items[i].s.Name != "")
                        {
                            ClFunc.UpdateBagStallItem(MShare.g_MySelf.m_StallMgr.mBlock.Items[i], 5);
                        }
                    }
                }
            }
            if (MShare.g_boOpenAutoPlay && (MShare.g_nAPReLogon == 4))
            {
                // 0613
                MShare.g_nAPReLogon = 0;
                MShare.g_nOverAPZone = MShare.g_nOverAPZone2;
                MShare.g_APGoBack = MShare.g_APGoBack2;
                if (MShare.g_APMapPath2 != null)
                {
                    MShare.g_APMapPath = new Point[MShare.g_APMapPath2.GetUpperBound(0) + 1];
                    for (k = 0; k <= MShare.g_APMapPath2.GetUpperBound(0); k++)
                    {
                        MShare.g_APMapPath[k] = MShare.g_APMapPath2[k];
                    }
                }
                MShare.g_APLastPoint = MShare.g_APLastPoint2;
                MShare.g_APStep = MShare.g_APStep2;
                MShare.g_gcAss[0] = true;
                MShare.g_APTagget = null;
                MShare.g_AutoPicupItem = null;
                MShare.g_nAPStatus = -1;
                MShare.g_nTargetX = -1;
                MShare.g_APGoBack2 = false;
                MShare.g_APMapPath2 = null;
                GetNearPoint();
                frmMain.TimerAutoPlay.Enabled = MShare.g_gcAss[0];
                DScreen.AddChatBoardString("[挂机] 开始自动挂机...", Color.White, Color.Red);
                SaveWayPoint();
                if (MShare.g_nAPrlRecallHero || (MShare.g_MySelf.m_HeroObject == null))
                {
                    // := (g_MySelf.m_HeroObject <> nil);
                    FrmDlg.m_dwUnRecallHeroTick = MShare.GetTickCount() - 58000;
                }
            }
        }

        private void ClientGetDropItemFail(string iname, int sindex)
        {
            TClientItem pc;
            pc = ClFunc.GetDropItem(iname, sindex);
            if (pc != null)
            {
                ClFunc.AddItemBag(pc);
                ClFunc.DelDropItem(iname, sindex);
            }
        }

        private void ClientGetShowItem(int itemid, int X, int Y, int looks, string itmname)
        {
            int i;
            TDropItem DropItem;
            TCItemRule P;
            for (i = 0; i < MShare.g_DropedItemList.Count; i++)
            {
                if (((TDropItem)(MShare.g_DropedItemList[i])).id == itemid)
                {
                    return;
                }
            }
            DropItem = new TDropItem();
            DropItem.id = itemid;
            DropItem.X = X;
            DropItem.Y = Y;
            DropItem.looks = looks;
            DropItem.Name = itmname;
            DropItem.Width = HGECanvas.Units.HGECanvas.g_DXCanvas.TextWidth(itmname);
            DropItem.Height = HGECanvas.Units.HGECanvas.g_DXCanvas.TextHeight(itmname);
            HUtil32.GetValidStr3(DropItem.Name, ref itmname, new string[] { "\\" });
            DropItem.FlashTime = MShare.GetTickCount() - ((long)(new System.Random(3000)).Next());
            DropItem.BoFlash = false;
            DropItem.boNonSuch = false;
            DropItem.boShowName = MShare.g_ShowItemList.IndexOf(itmname) < 0;
            // True;
            DropItem.boPickUp = DropItem.boShowName;
            if (MShare.g_gcAss[5])
            {
                DropItem.boNonSuch = false;
                DropItem.boPickUp = false;
                DropItem.boShowName = false;
                i = MShare.g_APPickUpList.IndexOf(itmname);
                if (i >= 0)
                {
                    DropItem.boNonSuch = ((int)MShare.g_APPickUpList.Values[i]) != 0;
                    DropItem.boPickUp = true;
                    if (!DropItem.boNonSuch)
                    {
                        DropItem.boShowName = true;
                    }
                }
            }
            else
            {
                P = ((TCItemRule)(MShare.g_ItemsFilter_All.GetValues(itmname)));
                if (P != null)
                {
                    DropItem.boNonSuch = P.rare;
                    DropItem.boPickUp = P.pick;
                    DropItem.boShowName = P.Show;
                }
            }
            // DropItem.boShowName := g_ShowItemList.IndexOf(itmname) < 0;
            // if g_gcAss[5] then DropItem.boShowName := g_APPickUpList.IndexOf(itmname) >= 0;
            MShare.g_DropedItemList.Add(DropItem);
        }

        private void ClientGetHideItem(int itemid, int X, int Y)
        {
            int i;
            TDropItem DropItem;
            for (i = 0; i < MShare.g_DropedItemList.Count; i++)
            {
                DropItem = MShare.g_DropedItemList[i];
                if (DropItem.id == itemid)
                {
                    this.Dispose(DropItem);
                    MShare.g_DropedItemList.RemoveAt(i);
                    break;
                }
            }
        }

        private void ClientGetSendUseItems(string body)
        {
            int Index;
            string Str;
            string data;
            TClientItem cu;
            FillChar(MShare.g_UseItems, sizeof(TClientItem) * 14, '\0');
            while (true)
            {
                if (body == "")
                {
                    break;
                }
                body = HUtil32.GetValidStr3(body, ref Str, new string[] { "/" });
                body = HUtil32.GetValidStr3(body, ref data, new string[] { "/" });
                Index = HUtil32.Str_ToInt(Str, -1);
                if (Index >= 0 && Index <= Grobal2.U_FASHION)
                {
                    EDcode.DecodeBuffer(data, cu, sizeof(TClientItem));
                    MShare.g_UseItems[Index] = cu;
                }
            }
        }

        private void ClientGetSendHeroUseItems(string body)
        {
            int Index;
            string Str;
            string data;
            TClientItem cu;
            FillChar(MShare.g_HeroUseItems, sizeof(TClientItem) * 14, '\0');
            while (true)
            {
                if (body == "")
                {
                    break;
                }
                body = HUtil32.GetValidStr3(body, ref Str, new string[] { "/" });
                body = HUtil32.GetValidStr3(body, ref data, new string[] { "/" });
                Index = HUtil32.Str_ToInt(Str, -1);
                if (Index >= 0 && Index <= Grobal2.U_FASHION)
                {
                    EDcode.DecodeBuffer(data, cu, sizeof(TClientItem));
                    MShare.g_HeroUseItems[Index] = cu;
                }
            }
        }

        public int ClientGetAddMagic_ListSortCompareLevel(object Item1, object Item2)
        {
            int result;
            result = 1;
            if (((int)((TClientMagic)(Item1)).Def.TrainLevel[0]) < ((int)((TClientMagic)(Item2)).Def.TrainLevel[0]))
            {
                result = -1;
            }
            else if (((int)((TClientMagic)(Item1)).Def.TrainLevel[0]) == ((int)((TClientMagic)(Item2)).Def.TrainLevel[0]))
            {
                result = 0;
            }
            return result;
        }

        private void ClientGetAddMagic(string body)
        {
            int i;
            TClientMagic pcm;
            pcm = new TClientMagic();
            EDcode.DecodeBuffer(body, pcm, sizeof(TClientMagic));
            MShare.g_MagicArr[pcm.Def.btClass][pcm.Def.wMagicID] = pcm;
            if (pcm.Def.btClass == 0)
            {
                MShare.g_MagicList.Add(pcm);
            }
            else
            {
                MShare.g_IPMagicList.Add(pcm);
            }
            for (i = 0; i < MShare.g_MagicList.Count; i++)
            {
                if (((TClientMagic)(MShare.g_MagicList[i])).Def.wMagicID == 67)
                {
                    MShare.g_MagicList.Move(i, 0);
                    break;
                }
            }
        }

        private void ClientGetDelMagic(int magid, int btclass)
        {
            int i;
            if (btclass == 0)
            {
                for (i = MShare.g_MagicList.Count - 1; i >= 0; i--)
                {
                    if (((TClientMagic)(MShare.g_MagicList[i])).Def.wMagicID == magid)
                    {
                        this.Dispose(((TClientMagic)(MShare.g_MagicList[i])));
                        MShare.g_MagicList.RemoveAt(i);
                        break;
                    }
                }
            }
            else
            {
                for (i = MShare.g_IPMagicList.Count - 1; i >= 0; i--)
                {
                    if (((TClientMagic)(MShare.g_IPMagicList[i])).Def.wMagicID == magid)
                    {
                        this.Dispose(((TClientMagic)(MShare.g_IPMagicList[i])));
                        MShare.g_IPMagicList.RemoveAt(i);
                        break;
                    }
                }
            }
            MShare.g_MagicArr[btclass][magid] = null;
        }

        public int ClientConvertMagic_ListSortCompareLevel(object Item1, object Item2)
        {
            int result;
            result = 1;
            if (((int)((TClientMagic)(Item1)).Def.TrainLevel[0]) < ((int)((TClientMagic)(Item2)).Def.TrainLevel[0]))
            {
                result = -1;
            }
            else if (((int)((TClientMagic)(Item1)).Def.TrainLevel[0]) == ((int)((TClientMagic)(Item2)).Def.TrainLevel[0]))
            {
                result = 0;
            }
            return result;
        }

        private void ClientConvertMagic(int t1, int t2, int id1, int id2, string S)
        {
            int i;
            TClientMagic cm;
            TClientMagic pcm;
            EDcode.DecodeBuffer(S, cm, sizeof(TClientMagic));
            if (t1 == 0)
            {
                for (i = MShare.g_MagicList2.Count - 1; i >= 0; i--)
                {
                    pcm = ((TClientMagic)(MShare.g_MagicList2[i]));
                    if (pcm.Def.wMagicID == id1)
                    {
                        pcm = cm;
                        if (t1 == t2)
                        {
                            // update
                            MShare.g_MagicArr[t1][id1] = null;
                            MShare.g_MagicArr[t1][id2] = pcm;
                        }
                        else
                        {
                            // convert
                            MShare.g_MagicList2.RemoveAt(i);
                            MShare.g_MagicList2.Sort(ClientConvertMagic_ListSortCompareLevel);
                            MShare.g_IPMagicList.Add(pcm);
                            MShare.g_MagicArr[t1][id1] = null;
                            MShare.g_MagicArr[t2][id2] = pcm;
                        }
                        break;
                    }
                }
                for (i = MShare.g_MagicList.Count - 1; i >= 0; i--)
                {
                    pcm = ((TClientMagic)(MShare.g_MagicList[i]));
                    if (pcm.Def.wMagicID == id1)
                    {
                        pcm = cm;
                        if (t1 == t2)
                        {
                            MShare.g_MagicArr[t1][id1] = null;
                            MShare.g_MagicArr[t1][id2] = pcm;
                        }
                        else
                        {
                            MShare.g_MagicList.RemoveAt(i);
                            MShare.g_IPMagicList.Add(pcm);
                            MShare.g_MagicArr[t1][id1] = null;
                            MShare.g_MagicArr[t2][id2] = pcm;
                        }
                        break;
                    }
                }
            }
            else
            {
                for (i = MShare.g_IPMagicList.Count - 1; i >= 0; i--)
                {
                    pcm = ((TClientMagic)(MShare.g_IPMagicList[i]));
                    if (pcm.Def.wMagicID == id1)
                    {
                        pcm = cm;
                        if (t1 == t2)
                        {
                            // update
                            MShare.g_MagicArr[t1][id1] = null;
                            MShare.g_MagicArr[t1][id2] = pcm;
                        }
                        else
                        {
                            // convert
                            MShare.g_IPMagicList.RemoveAt(i);
                            MShare.g_MagicList.Add(pcm);
                        }
                        MShare.g_MagicArr[t1][id1] = null;
                        MShare.g_MagicArr[t2][id2] = pcm;
                    }
                    break;
                }
            }
        }

        public int hClientConvertMagic_ListSortCompareLevel(object Item1, object Item2)
        {
            int result;
            result = 1;
            if (((int)((TClientMagic)(Item1)).Def.TrainLevel[0]) < ((int)((TClientMagic)(Item2)).Def.TrainLevel[0]))
            {
                result = -1;
            }
            else if (((int)((TClientMagic)(Item1)).Def.TrainLevel[0]) == ((int)((TClientMagic)(Item2)).Def.TrainLevel[0]))
            {
                result = 0;
            }
            return result;
        }

        private void hClientConvertMagic(int t1, int t2, int id1, int id2, string S)
        {
            int i;
            TClientMagic pcm;
            TClientMagic cm;
            // DScreen.AddChatBoardString(format('%d %d %d %d %s', [t1, t2, id1, id2, S]), clWhite, clRed);
            EDcode.DecodeBuffer(S, cm, sizeof(TClientMagic));
            if (t1 == 0)
            {
                for (i = MShare.g_hMagicList2.Count - 1; i >= 0; i--)
                {
                    pcm = ((TClientMagic)(MShare.g_hMagicList2[i]));
                    if (pcm.Def.wMagicID == id1)
                    {
                        // pcm.Def.btclass := t2;
                        // pcm.Def.wMagicID := id2;
                        // pcm.Def.sMagicName := S;
                        pcm = cm;
                        // DScreen.AddChatBoardString('1111111', clWhite, clRed);
                        if (t1 == t2)
                        {
                            // update
                            // g_MagicArr[t1][id1] := nil;
                            // g_MagicArr[t1][id2] := pcm;
                        }
                        else
                        {
                            // convert
                            MShare.g_hMagicList2.RemoveAt(i);
                            MShare.g_hMagicList2.Sort(hClientConvertMagic_ListSortCompareLevel);
                            MShare.g_HeroIPMagicList.Add(pcm);
                            // g_MagicArr[t1][id1] := nil;
                            // g_MagicArr[t2][id2] := pcm;
                        }
                        break;
                    }
                }
                for (i = MShare.g_HeroMagicList.Count - 1; i >= 0; i--)
                {
                    pcm = ((TClientMagic)(MShare.g_HeroMagicList[i]));
                    if (pcm.Def.wMagicID == id1)
                    {
                        // pcm.Def.btclass := t2;
                        // pcm.Def.wMagicID := id2;
                        // pcm.Def.sMagicName := S;
                        pcm = cm;
                        if (t1 == t2)
                        {
                            // g_MagicArr[t1][id1] := nil;
                            // g_MagicArr[t1][id2] := pcm;
                        }
                        else
                        {
                            MShare.g_HeroMagicList.RemoveAt(i);
                            MShare.g_HeroIPMagicList.Add(pcm);
                            // g_MagicArr[t1][id1] := nil;
                            // g_MagicArr[t2][id2] := pcm;
                        }
                        break;
                    }
                }
            }
            else
            {
                for (i = MShare.g_HeroIPMagicList.Count - 1; i >= 0; i--)
                {
                    pcm = ((TClientMagic)(MShare.g_HeroIPMagicList[i]));
                    if (pcm.Def.wMagicID == id1)
                    {
                        pcm = cm;
                        if (t1 == t2)
                        {

                        }
                        else
                        {
                            // convert
                            MShare.g_HeroIPMagicList.RemoveAt(i);
                            MShare.g_HeroMagicList.Add(pcm);
                        }
                    }
                    break;
                }
            }
        }

        public int ClientGetMyMagics_ListSortCompareLevel(object Item1, object Item2)
        {
            int result;
            result = 1;
            if (((int)((TClientMagic)(Item1)).Def.TrainLevel[0]) < ((int)((TClientMagic)(Item2)).Def.TrainLevel[0]))
            {
                result = -1;
            }
            else if (((int)((TClientMagic)(Item1)).Def.TrainLevel[0]) == ((int)((TClientMagic)(Item2)).Def.TrainLevel[0]))
            {
                result = 0;
            }
            return result;
        }

        private void ClientGetMyMagics(string body)
        {
            int i;
            string data;
            TClientMagic pcm;
            for (i = 0; i < MShare.g_MagicList.Count; i++)
            {
                this.Dispose(((TClientMagic)(MShare.g_MagicList[i])));
            }
            MShare.g_MagicList.Clear();
            for (i = 0; i < MShare.g_IPMagicList.Count; i++)
            {
                this.Dispose(((TClientMagic)(MShare.g_IPMagicList[i])));
            }
            MShare.g_IPMagicList.Clear();
            FillChar(MShare.g_MagicArr, sizeof(MShare.g_MagicArr), 0);
            while (true)
            {
                if (body == "")
                {
                    break;
                }
                body = HUtil32.GetValidStr3(body, ref data, new string[] { "/" });
                if (data != "")
                {
                    pcm = new TClientMagic();
                    EDcode.DecodeBuffer(data, pcm, sizeof(TClientMagic));
                    if (pcm.Def.btClass == 0)
                    {
                        MShare.g_MagicList.Add(pcm);
                    }
                    else
                    {
                        MShare.g_IPMagicList.Add(pcm);
                    }
                    MShare.g_MagicArr[pcm.Def.btClass][pcm.Def.wMagicID] = pcm;
                }
                else
                {
                    break;
                }
            }
            for (i = 0; i < MShare.g_MagicList.Count; i++)
            {
                if (((TClientMagic)(MShare.g_MagicList[i])).Def.wMagicID == 67)
                {
                    MShare.g_MagicList.Move(i, 0);
                    break;
                }
            }
        }

        private void ClientGetShopItems(string body, int __Int)
        {
            string data;
            TShopItem pSi;
            MShare.g_btSellType = __Int;
            while (true)
            {
                if (body == "")
                {
                    break;
                }
                body = HUtil32.GetValidStr3(body, ref data, new string[] { "/" });
                if (data != "")
                {
                    pSi = new TShopItem();
                    EDcode.DecodeBuffer(data, pSi, sizeof(TShopItem));
                    MShare.g_ShopListArr[pSi.btClass].Add(pSi);
                }
                else
                {
                    break;
                }
            }
        }

        private void ClientGetMagicLvExp(int magid, int maglv, int magtrain)
        {
            int nType;
            TClientMagic pcm;
            nType = HUtil32.HiWord(magid);
            magid = HUtil32.LoWord(magid);
            pcm = MShare.g_MagicArr[nType][magid];
            if (pcm != null)
            {
                pcm.Level = maglv;
                pcm.CurTrain = magtrain;
            }
        }

        private void ClientGetMagicMaxLv(int magid, int magMaxlv, int hero)
        {
            // , nType
            int i;
            TClientMagic pcm;
            // nType := HUtil32.HiWord(magid);
            magid = HUtil32.LoWord(magid);
            if (hero == 0)
            {
                pcm = MShare.g_MagicArr[0][magid];
                if ((magid <= 0) || (magid >= 255))
                {
                    return;
                }
                if (pcm != null)
                {
                    pcm.Def.btTrainLv = magMaxlv;
                }
            }
            else
            {
                for (i = MShare.g_hMagicList2.Count - 1; i >= 0; i--)
                {
                    if (((TClientMagic)(MShare.g_hMagicList2[i])).Def.wMagicID == magid)
                    {
                        ((TClientMagic)(MShare.g_hMagicList2[i])).Def.btTrainLv = magMaxlv;
                        return;
                    }
                }
                for (i = MShare.g_HeroMagicList.Count - 1; i >= 0; i--)
                {
                    if (((TClientMagic)(MShare.g_HeroMagicList[i])).Def.wMagicID == magid)
                    {
                        ((TClientMagic)(MShare.g_HeroMagicList[i])).Def.btTrainLv = magMaxlv;
                        break;
                    }
                }
            }
        }

        private void ClientGetDuraChange(int uidx, int newdura, int newduramax)
        {
            if (uidx >= 0 && uidx <= Grobal2.U_FASHION)
            {
                if (MShare.g_UseItems[uidx].s.Name != "")
                {
                    MShare.g_UseItems[uidx].Dura = newdura;
                    MShare.g_UseItems[uidx].DuraMax = newduramax;
                }
            }
        }

        private void ClientGetMerchantSay(int merchant, int face, string saying)
        {
            string npcname;
            MShare.g_nMDlgX = MShare.g_MySelf.m_nCurrX;
            MShare.g_nMDlgY = MShare.g_MySelf.m_nCurrY;
            if (MShare.g_nCurMerchant != merchant)
            {
                MShare.g_nCurMerchant = merchant;
                FrmDlg.ResetMenuDlg;
                FrmDlg.CloseMDlg;
            }
            saying = HUtil32.GetValidStr3(saying, ref npcname, new string[] { "/" });
            FrmDlg.ShowMDlg(face, npcname, saying);
        }

        private void ClientGetSendGoodsList(int merchant, int count, string body)
        {
            string gname;
            string gsub;
            string gprice;
            string gstock;
            TClientGoods pcg;
            FrmDlg.ResetMenuDlg;
            MShare.g_nCurMerchant = merchant;
            body = EDcode.DeCodeString(body);
            while (body != "")
            {
                body = HUtil32.GetValidStr3(body, ref gname, new string[] { "/" });
                body = HUtil32.GetValidStr3(body, ref gsub, new string[] { "/" });
                body = HUtil32.GetValidStr3(body, ref gprice, new string[] { "/" });
                body = HUtil32.GetValidStr3(body, ref gstock, new string[] { "/" });
                if ((gname != "") && (gprice != "") && (gstock != ""))
                {
                    pcg = new TClientGoods();
                    pcg.Name = gname;
                    pcg.SubMenu = HUtil32.Str_ToInt(gsub, 0);
                    pcg.Price = HUtil32.Str_ToInt(gprice, 0);
                    pcg.Stock = HUtil32.Str_ToInt(gstock, 0);
                    pcg.Grade = -1;
                    FrmDlg.MenuList.Add(pcg);
                }
                else
                {
                    break;
                }
            }
            FrmDlg.FrmDlg.ShowShopMenuDlg(FrmDlg.dmBuy);
            FrmDlg.FrmDlg.CurDetailItem = "";
        }

        private void ClientGetDelCharList(int count, string body)
        {
            string gname;
            string gjob;
            string gsex;
            string glevel;
            TDelChar pcg;
            FrmDlg.ResetDelCharMenuDlg;
            body = EDcode.DeCodeString(body);
            while (body != "")
            {
                body = HUtil32.GetValidStr3(body, ref gname, new string[] { "/" });
                body = HUtil32.GetValidStr3(body, ref gjob, new string[] { "/" });
                body = HUtil32.GetValidStr3(body, ref gsex, new string[] { "/" });
                body = HUtil32.GetValidStr3(body, ref glevel, new string[] { "/" });
                body = HUtil32.GetValidStr3(body, ref gsex, new string[] { "/" });
                if ((gname != "") && (glevel != "") && (gsex != ""))
                {
                    pcg = new TDelChar();
                    pcg.sCharName = gname;
                    pcg.nLevel = HUtil32.Str_ToInt(glevel, 1);
                    pcg.btJob = HUtil32.Str_ToInt(gjob, 0);
                    pcg.btSex = HUtil32.Str_ToInt(gsex, 0);
                    FrmDlg.m_DelCharList.Add(pcg);
                }
                else
                {
                    break;
                }
            }
            FrmDlg.FrmDlg.ShowDelCharInfoDlg;
        }

        private void ClientGetSendMakeDrugList(int merchant, string body)
        {
            string gname;
            string gsub;
            string gprice;
            string gstock;
            TClientGoods pcg;
            FrmDlg.ResetMenuDlg;
            MShare.g_nCurMerchant = merchant;
            // clear shop menu list
            // deocde body received from server
            body = EDcode.DeCodeString(body);
            while (body != "")
            {
                body = HUtil32.GetValidStr3(body, ref gname, new string[] { "/" });
                body = HUtil32.GetValidStr3(body, ref gsub, new string[] { "/" });
                body = HUtil32.GetValidStr3(body, ref gprice, new string[] { "/" });
                body = HUtil32.GetValidStr3(body, ref gstock, new string[] { "/" });
                if ((gname != "") && (gprice != "") && (gstock != ""))
                {
                    pcg = new TClientGoods();
                    pcg.Name = gname;
                    pcg.SubMenu = HUtil32.Str_ToInt(gsub, 0);
                    pcg.Price = HUtil32.Str_ToInt(gprice, 0);
                    pcg.Stock = HUtil32.Str_ToInt(gstock, 0);
                    pcg.Grade = -1;
                    FrmDlg.MenuList.Add(pcg);
                }
                else
                {
                    break;
                }
            }
            FrmDlg.FrmDlg.ShowShopMenuDlg(FrmDlg.dmMakeDrug);
            FrmDlg.FrmDlg.CurDetailItem = "";
            FrmDlg.FrmDlg.BoMakeDrugMenu = true;
        }

        private void ClientGetSendUserSell(int merchant)
        {
            FrmDlg.CloseDSellDlg;
            MShare.g_nCurMerchant = merchant;
            FrmDlg.SpotDlgMode = dmSell;
            FrmDlg.ShowShopSellDlg;
        }

        private void ClientGetSendUserExchgBook(int merchant)
        {
            FrmDlg.CloseDSellDlg;
            MShare.g_nCurMerchant = merchant;
            FrmDlg.SpotDlgMode = dmExchangeBook;
            FrmDlg.ShowShopSellDlg;
        }

        private void ClientGetSendItemDlg(int merchant, string Str)
        {
            FrmDlg.CloseDSellDlg;
            MShare.g_nCurMerchant = merchant;
            FrmDlg.SpotDlgStr = Str;
            FrmDlg.SpotDlgMode = dmItemDlg;
            FrmDlg.ShowShopSellDlg;
        }

        private void ClientGetSendBindItem(int merchant)
        {
            FrmDlg.CloseDSellDlg;
            MShare.g_nCurMerchant = merchant;
            FrmDlg.SpotDlgMode = dmBindItem;
            FrmDlg.ShowShopSellDlg;
        }

        private void ClientGetSendUnBindItem(int merchant)
        {
            FrmDlg.CloseDSellDlg;
            MShare.g_nCurMerchant = merchant;
            FrmDlg.SpotDlgMode = dmUnBindItem;
            FrmDlg.ShowShopSellDlg;
        }

        private void ClientGetSendUserRepair(int merchant)
        {
            FrmDlg.CloseDSellDlg;
            MShare.g_nCurMerchant = merchant;
            FrmDlg.SpotDlgMode = dmRepair;
            FrmDlg.ShowShopSellDlg;
        }

        private void ClientGetSendUserStorage(int merchant)
        {
            FrmDlg.CloseDSellDlg;
            MShare.g_nCurMerchant = merchant;
            FrmDlg.SpotDlgMode = dmStorage;
            FrmDlg.ShowShopSellDlg;
        }

        private void ClientGetSendUserMaketSell(int merchant)
        {
            FrmDlg.CloseDSellDlg;
            MShare.g_nCurMerchant = merchant;
            FrmDlg.SpotDlgMode = dmMaketSell;
            FrmDlg.ShowShopSellDlg;
            FrmDlg.DItemMarketCloseClick(null, 0, 0);
        }

        private void ClientGetSaveItemList(int merchant, string bodystr)
        {
            int i;
            string data;
            TClientItem pc;
            TClientGoods pcg;
            FrmDlg.ResetMenuDlg;
            for (i = 0; i < MShare.g_SaveItemList.Count; i++)
            {
                this.Dispose(((TClientItem)(MShare.g_SaveItemList[i])));
            }
            MShare.g_SaveItemList.Clear();
            while (true)
            {
                if (bodystr == "")
                {
                    break;
                }
                bodystr = HUtil32.GetValidStr3(bodystr, ref data, new string[] { "/" });
                if (data != "")
                {
                    pc = new TClientItem();
                    EDcode.DecodeBuffer(data, pc, sizeof(TClientItem));
                    MShare.g_SaveItemList.Add(pc);
                }
                else
                {
                    break;
                }
            }
            MShare.g_nCurMerchant = merchant;
            // deocde body received from server
            for (i = 0; i < MShare.g_SaveItemList.Count; i++)
            {
                pcg = new TClientGoods();
                pcg.Name = ((TClientItem)(MShare.g_SaveItemList[i])).s.Name;
                pcg.SubMenu = 0;
                pcg.Price = ((TClientItem)(MShare.g_SaveItemList[i])).MakeIndex;
                pcg.Stock = Math.Round(((TClientItem)(MShare.g_SaveItemList[i])).Dura / 1000);
                pcg.Grade = Math.Round(((TClientItem)(MShare.g_SaveItemList[i])).DuraMax / 1000);
                FrmDlg.MenuList.Add(pcg);
            }
            FrmDlg.FrmDlg.ShowShopMenuDlg(FrmDlg.dmGetSave);
            FrmDlg.FrmDlg.BoStorageMenu = true;
        }

        private void ClientGetSendDetailGoodsList(int merchant, int count, int topline, string bodystr)
        {
            int i;
            string data;
            TClientGoods pcg;
            TClientItem pc;
            FrmDlg.ResetMenuDlg;
            MShare.g_nCurMerchant = merchant;
            bodystr = EDcode.DeCodeString(bodystr);
            while (true)
            {
                if (bodystr == "")
                {
                    break;
                }
                bodystr = HUtil32.GetValidStr3(bodystr, ref data, new string[] { "/" });
                if (data != "")
                {
                    pc = new TClientItem();
                    EDcode.DecodeBuffer(data, pc, sizeof(TClientItem));
                    MShare.g_MenuItemList.Add(pc);
                }
                else
                {
                    break;
                }
            }
            // clear shop menu list
            for (i = 0; i < MShare.g_MenuItemList.Count; i++)
            {
                pcg = new TClientGoods();
                pcg.Name = ((TClientItem)(MShare.g_MenuItemList[i])).s.Name;
                pcg.SubMenu = 0;
                pcg.Price = ((TClientItem)(MShare.g_MenuItemList[i])).DuraMax;
                pcg.Stock = ((TClientItem)(MShare.g_MenuItemList[i])).MakeIndex;
                pcg.Grade = Math.Round(((TClientItem)(MShare.g_MenuItemList[i])).Dura / 1000);
                FrmDlg.MenuList.Add(pcg);
            }
            FrmDlg.FrmDlg.ShowShopMenuDlg(FrmDlg.dmDetailMenu);
            FrmDlg.FrmDlg.BoDetailMenu = true;
            FrmDlg.FrmDlg.MenuTopLine = topline;
        }

        private void ClientGetSendNotice(string body)
        {
            string data;
            string msgstr;
            MShare.g_boDoFastFadeOut = false;
            if (MShare.g_boOpenAutoPlay && (MShare.g_nAPReLogon == 3))
            {
                MShare.g_nAPReLogon = 4;
                SendClientMessage(Grobal2.CM_LOGINNOTICEOK, 0, 0, 0, MShare.CLIENTTYPE);
                return;
            }
            msgstr = "";
            body = EDcode.DeCodeString(body);
            while (true)
            {
                if (body == "")
                {
                    break;
                }
                body = HUtil32.GetValidStr3(body, ref data, new char[] { '' });
                msgstr = msgstr + data + "\\";
            }
            FrmDlg.DialogSize = 2;
            if (DebugOutStr(msgstr) == System.Windows.Forms.DialogResult.OK)
            {
                SendClientMessage(Grobal2.CM_LOGINNOTICEOK, 0, 0, 0, MShare.CLIENTTYPE);
            }
        }

        private void ClientGetGroupMembers(string bodystr)
        {
            string memb;
            MShare.g_GroupMembers.Clear();
            while (true)
            {
                if (bodystr == "")
                {
                    break;
                }
                bodystr = HUtil32.GetValidStr3(bodystr, ref memb, new string[] { "/" });
                if (memb != "")
                {
                    MShare.g_GroupMembers.Add(memb);
                }
                else
                {
                    break;
                }
            }
        }

        private void ClientGetOpenGuildDlg(string bodystr)
        {
            string Str;
            string data;
            string linestr;
            string s1;
            int pstep;
            if (g_boShowMemoLog)
            {
                g_PlayScene.MemoLog.Lines.Add("ClientGetOpenGuildDlg");
            }
            Str = EDcode.DeCodeString(bodystr);
            Str = HUtil32.GetValidStr3(Str, ref FrmDlg.Guild, new char[] { '\r' });
            Str = HUtil32.GetValidStr3(Str, ref FrmDlg.GuildFlag, new char[] { '\r' });
            Str = HUtil32.GetValidStr3(Str, ref data, new char[] { '\r' });
            if (data == "1")
            {
                FrmDlg.GuildCommanderMode = true;
            }
            else
            {
                FrmDlg.GuildCommanderMode = false;
            }
            FrmDlg.GuildStrs.Clear;
            FrmDlg.GuildNotice.Clear;
            pstep = 0;
            while (true)
            {
                if (Str == "")
                {
                    break;
                }
                Str = HUtil32.GetValidStr3(Str, ref data, new char[] { '\r' });
                if (data == "<Notice>")
                {
                    FrmDlg.GuildStrs.AddObject((char)7 + "行会公告", ((Color.White) as Object));
                    FrmDlg.GuildStrs.Add(" ");
                    pstep = 1;
                    continue;
                }
                if (data == "<KillGuilds>")
                {
                    FrmDlg.GuildStrs.Add(" ");
                    FrmDlg.GuildStrs.AddObject((char)7 + "敌对行会", ((Color.White) as Object));
                    FrmDlg.GuildStrs.Add(" ");
                    pstep = 2;
                    linestr = "";
                    continue;
                }
                if (data == "<AllyGuilds>")
                {
                    if (linestr != "")
                    {
                        FrmDlg.GuildStrs.Add(linestr);
                    }
                    linestr = "";
                    FrmDlg.GuildStrs.Add(" ");
                    FrmDlg.GuildStrs.AddObject((char)7 + "联盟行会", ((Color.White) as Object));
                    FrmDlg.GuildStrs.Add(" ");
                    pstep = 3;
                    continue;
                }
                if (pstep == 1)
                {
                    FrmDlg.GuildNotice.Add(data);
                }
                if (data != "")
                {
                    if (data[1] == "<")
                    {
                        HUtil32.ArrestStringEx(data, "<", ">", ref s1);
                        if (s1 != "")
                        {
                            FrmDlg.GuildStrs.Add(" ");
                            FrmDlg.GuildStrs.AddObject((char)7 + s1, ((Color.White) as Object));
                            FrmDlg.GuildStrs.Add(" ");
                            continue;
                        }
                    }
                }
                if ((pstep == 2) || (pstep == 3))
                {
                    if (linestr.Length > 80)
                    {
                        FrmDlg.GuildStrs.Add(linestr);
                        linestr = "";
                    }
                    else
                    {
                        linestr = linestr + ClFunc.fmstr(data, 18);
                    }
                    continue;
                }
                FrmDlg.GuildStrs.Add(data);
            }
            if (linestr != "")
            {
                FrmDlg.GuildStrs.Add(linestr);
            }
            FrmDlg.ShowGuildDlg;
        }

        private void ClientGetSendGuildMemberList(string body)
        {
            string Str;
            string data;
            string rankname;
            string members;
            int rank;
            Str = EDcode.DeCodeString(body);
            FrmDlg.GuildStrs.Clear;
            FrmDlg.GuildMembers.Clear;
            rank = 0;
            while (true)
            {
                if (Str == "")
                {
                    break;
                }
                Str = HUtil32.GetValidStr3(Str, ref data, new string[] { "/" });
                if (data != "")
                {
                    if (data[1] == "#")
                    {
                        rank = HUtil32.Str_ToInt(data.Substring(2 - 1, data.Length - 1), 0);
                        continue;
                    }
                    if (data[1] == "*")
                    {
                        if (members != "")
                        {
                            FrmDlg.GuildStrs.Add(members);
                        }
                        rankname = data.Substring(2 - 1, data.Length - 1);
                        members = "";
                        FrmDlg.GuildStrs.Add(" ");
                        if (FrmDlg.GuildCommanderMode)
                        {
                            FrmDlg.GuildStrs.AddObject(ClFunc.fmstr("(" + (rank).ToString() + ")", 3) + "<" + rankname + ">", ((Color.White) as Object));
                        }
                        else
                        {
                            FrmDlg.GuildStrs.AddObject("<" + rankname + ">", ((Color.White) as Object));
                        }
                        FrmDlg.GuildMembers.Add("#" + (rank).ToString() + " <" + rankname + ">");
                        continue;
                    }
                    if (members.Length > 80)
                    {
                        FrmDlg.GuildStrs.Add(members);
                        members = "";
                    }
                    members = members + ClFunc.fmstr(data, 18);
                    FrmDlg.GuildMembers.Add(data);
                }
            }
            if (members != "")
            {
                FrmDlg.GuildStrs.Add(members);
            }
        }

        public void MinTimerTimer(System.Object Sender, System.EventArgs _e1)
        {
            int i;
            for (i = 0; i < g_PlayScene.m_ActorList.Count; i++)
            {
                if (IsGroupMember(((TActor)(g_PlayScene.m_ActorList[i])).m_sUserName))
                {
                    ((TActor)(g_PlayScene.m_ActorList[i])).m_boGrouped = true;
                }
                else
                {
                    ((TActor)(g_PlayScene.m_ActorList[i])).m_boGrouped = false;
                }
            }
            for (i = MShare.g_FreeActorList.Count - 1; i >= 0; i--)
            {
                if (MShare.GetTickCount() - ((TActor)(MShare.g_FreeActorList[i])).m_dwDeleteTime > 60 * 1000)
                {
                    ((TActor)(MShare.g_FreeActorList[i])).Free;
                    MShare.g_FreeActorList.RemoveAt(i);
                }
            }
        }

        public void CheckHackTimerTimer(Object Sender)
        {
            // const
            // busy: Boolean = False;
            // var
            // ahour, amin, asec, amsec: Word;
            // tCount, timertime: LongWord;

        }

        private void ClientGetDealRemoteAd.Item.Itemtring body)
        {
            TClientItem ci;
            if (body != "")
            {
                EDcode.DecodeBuffer(body, ci, sizeof(TClientItem));
                ClFunc.AddDealRemoteItem(ci);
            }
        }

        private void ClientGetDealRemoteDe.Item.Itemtring body)
        {
            TClientItem ci;
            if (body != "")
            {
                EDcode.DecodeBuffer(body, ci, sizeof(TClientItem));
                ClFunc.DelDealRemoteItem(ci);
            }
        }

        private void ClientGetReadMiniMap(int mapindex)
        {
            int i;
            string szMapTitle;
            TMapDescInfo pMapDescInfo;
            if (MShare.g_nApMiniMap)
            {
                MShare.g_nApMiniMap = false;
                if (mapindex >= 1)
                {
                    MShare.g_nMiniMapIndex = mapindex - 1;
                }
            }
            else
            {
                if (mapindex >= 1)
                {
                    MShare.g_boViewMiniMap = true;
                    MShare.g_nMiniMapIndex = mapindex - 1;
                }
            }
            // 123456
            MShare.g_xCurMapDescList.Clear();
            for (i = 0; i < MShare.g_xMapDescList.Count; i++)
            {
                szMapTitle = MShare.g_xMapDescList[i];
                pMapDescInfo = ((TMapDescInfo)(MShare.g_xMapDescList.Values[i]));
                if (((MShare.g_xMapDescList[i]).ToLower().CompareTo((MShare.g_sMapTitle).ToLower()) == 0) && (((pMapDescInfo.nFullMap == MShare.g_nViewMinMapLv) && (pMapDescInfo.nFullMap == 1)) || ((MShare.g_nViewMinMapLv != 1) && (pMapDescInfo.nFullMap == 0))))
                {
                    MShare.g_xCurMapDescList.Add(MShare.g_xMapDescList[i], ((pMapDescInfo) as Object));
                }
            }
        }

        private void ClientGetChangeGuildName(string body)
        {
            string Str;
            Str = HUtil32.GetValidStr3(body, ref MShare.g_sGuildName, new string[] { "/" });
            MShare.g_sGuildRankName = Str.Trim();
        }

        private void ClientGetSendUserState(string body)
        {
            int i;
            int ii;
            TUserStateInfo UserState;
            THumTitle[] Titles;
            FillChar(UserState, sizeof(TUserStateInfo), '\0');
            EDcode.DecodeBuffer(body, UserState, sizeof(TUserStateInfo));
            UserState.NameColor = GetRGB(UserState.NameColor);
#if !UI_0508
            ii = 0;
            FillChar(Titles, sizeof(Titles), 0);
            for (i = Titles.GetLowerBound(0); i <= Titles.GetUpperBound(0); i++)
            {
                if (UserState.Titles[i].Index > 0)
                {
                    Titles[ii] = UserState.Titles[i];
                    ii++;
                }
            }
            if (ii > 0)
            {
                UserState.Titles = Titles;
            }
#endif
            FrmDlg.OpenUserState(UserState);
        }

        private void DrawEffectHum(int nType, int nX, int nY)
        {
            TMagicEff Effect;
            bool boFly;
            Effect = null;
            switch (nType)
            {
                case 0:
                    break;
                case 1:
                    Effect = new TNormalDrawEffect(nX, nY, WMFile.Units.WMFile.g_WMons[14], 410, 6, 120, false);
                    break;
                case 2:
                    // 赤月恶魔 地钉
                    Effect = new TNormalDrawEffect(nX, nY, WMFile.Units.WMFile.g_WMagic2Images, 670, 10, 150, true);
                    break;
                case 3:
                    Effect = new TNormalDrawEffect(nX, nY, WMFile.Units.WMFile.g_WMagic2Images, 690, 10, 150, true);
                    break;
                case 4:
                    g_PlayScene.NewMagic(null, 70, 70, nX, nY, nX, nY, 0, magiceff.TMagicType.mtRedGroundThunder, false, 60, ref boFly);
                    break;
                case 5:
                    g_PlayScene.NewMagic(null, 71, 71, nX, nY, nX, nY, 0, magiceff.TMagicType.mtRedThunder, false, 60, ref boFly);
                    break;
                case 6:
                    g_PlayScene.NewMagic(null, 72, 72, nX, nY, nX, nY, 0, magiceff.TMagicType.mtLava, false, 60, ref boFly);
                    break;
                case 7:
                    g_PlayScene.NewMagic(null, 73, 73, nX, nY, nX, nY, 0, magiceff.TMagicType.mtSpurt, false, 60, ref boFly);
                    break;
                case 8:
                    // Effect := THeroCharEffect.Create(g_Wui, 1210, 12, 120, Actor);
                    Effect = new TNormalDrawEffect(nX, nY, WMFile.Units.WMFile.g_wui, 1210, 12, 120, true);
                    break;
                // Modify the A .. B: 41 .. 43
                case 41:
                    Effect = new TNormalDrawEffect(nX, nY, WMFile.Units.WMFile.g_wui, 1170 + 10 * (nType - 41), 6, 220, true);
                    break;
                // Modify the A .. B: 75 .. 83
                case 75:
                    Effect = new TNormalDrawEffect(nX, nY, WMFile.Units.WMFile.g_WMagic3Images, (nType - 75) * 20, 20, 150, true);
                    if (nType >= 78)
                    {
                    }
                    break;
                case 84:
                    Effect = new TNormalDrawEffect(nX, nY, WMFile.Units.WMFile.g_WEffectImg, 800, 10, 100, true);
                    break;
                case 85:
                    Effect = new TNormalDrawEffect(nX, nY, WMFile.Units.WMFile.g_WEffectImg, 810, 10, 100, true);
                    break;
            }
            if (Effect != null)
            {
                Effect.MagOwner = MShare.g_MySelf;
                g_PlayScene.m_EffectList.Add(Effect);
            }
        }

        private void DrawEffectHumEx(int nID, int nType, int tag)
        {
            TMagicEff Effect;
            TActor Actor;
            Actor = g_PlayScene.FindActor(nID);
            if (Actor == null)
            {
                return;
            }
            Effect = null;
            switch (nType)
            {
                case 1:
                    // 0: ;
                    Effect = new THeroCharEffect(WMFile.Units.WMFile.g_WMagic4Images, 170, 5, 120, Actor);
                    break;
                case 2:
                    Effect = new THeroCharEffect(WMFile.Units.WMFile.g_WMagic4Images, 520, 16, 120, Actor);
                    break;
                case 3:
                    Effect = new THeroCharEffect(WMFile.Units.WMFile.g_WMagic4Images, 820, 10, 120, Actor);
                    break;
                case 4:
                    Effect = new THeroCharEffect(WMFile.Units.WMFile.g_WMagic4Images, 600, 6, 120, Actor);
                    break;
                case 5:
                    Effect = new THeroCharEffect(WMFile.Units.WMFile.g_WMagic4Images, 260, 8, 120, Actor);
                    break;
                case 6:
                    Effect = new THeroCharEffect(WMFile.Units.WMFile.g_WMagic4Images, 420, 16, 120, Actor);
                    break;
                case 7:
                    Effect = new THeroCharEffect(WMFile.Units.WMFile.g_WMagic4Images, 180, 6, 120, Actor);
                    break;
                case 8:
                    Effect = new THeroCharEffect(WMFile.Units.WMFile.g_WMagic4Images, 180, 6, 120, Actor);
                    break;
                case 9:
                    Effect = new THeroCharEffect(WMFile.Units.WMFile.g_WPrguse2, 00, 25, 120, Actor);
                    break;
                case 10:
                    Effect = new THeroCharEffect(WMFile.Units.WMFile.g_WPrguse2, 30, 25, 120, Actor);
                    break;
                case 11:
                    Effect = new THeroCharEffect(WMFile.Units.WMFile.g_WMagic5Images, 790, 10, 60, Actor);
                    break;
                case 13:
                    Effect = new THeroCharEffect(WMFile.Units.WMFile.g_WMagic6Images, 470, 5, 120, Actor);
                    break;
                case 14:
                    Effect = new THeroCharEffect(WMFile.Units.WMFile.g_WPrguse2, 110, 15, 80, Actor);
                    break;
                case 15:
                    Effect = new THeroCharEffect(WMFile.Units.WMFile.g_WMagic6Images, 480, 6, 120, Actor);
                    break;
                case 16:
                    Effect = new THeroCharEffect(WMFile.Units.WMFile.g_WMagic6Images, 490, 8, 120, Actor);
                    break;
                case 17:
                    Effect = new THeroCharEffect(WMFile.Units.WMFile.g_WMons[24], 3740, 10, 500, Actor);
                    break;
                case 18:
                    Effect = new THeroCharEffect(WMFile.Units.WMFile.g_cboEffect, 4060, 37, 50, Actor);
                    break;
                case 19:
                    Effect = new THeroCharEffect(WMFile.Units.WMFile.g_cboEffect, 4100, 33, 55, Actor);
                    break;
                case 20:
                    Effect = new THeroCharEffect(WMFile.Units.WMFile.g_cboEffect, 4140, 30, 60, Actor);
                    break;
                case 21:
                    Effect = new THeroCharEffect(WMFile.Units.WMFile.g_cboEffect, 4180, 06, 120, Actor);
                    break;
                case 22:
                    Effect = new THeroCharEffect(WMFile.Units.WMFile.g_cboEffect, 4190, 04, 120, Actor);
                    break;
                case 23:
                    Effect = new THeroCharEffect(WMFile.Units.WMFile.g_WPrguse2, 640, 10, 120, Actor);
                    break;
                case 24:
                    Effect = new THeroCharEffect(WMFile.Units.WMFile.g_WPrguse2, 650, 15, 095, Actor);
                    break;
                case 25:
                    Effect = new THeroCharEffect(WMFile.Units.WMFile.g_WPrguse2, 670, 18, 090, Actor);
                    break;
                case 26:
                    Effect = new THeroCharEffect(WMFile.Units.WMFile.g_WPrguse2, 690, 17, 090, Actor);
                    break;
                case 27:
                    Effect = new THeroCharEffect(WMFile.Units.WMFile.g_WPrguse2, 710, 19, 088, Actor);
                    break;
                case 28:
                    Effect = new THeroCharEffect(WMFile.Units.WMFile.g_WPrguse2, 630, 06, 120, Actor);
                    break;
                case 29:
                    Actor.StruckShowDamage((tag).ToString());
                    break;
                case 30:
                    Effect = new THeroCharEffect(WMFile.Units.WMFile.g_WMagic8Images2, 2460, 06, 100, Actor);
                    break;
                case 31:
                    Effect = new THeroCharEffect(WMFile.Units.WMFile.g_wui, 1210, 12, 120, Actor);
                    // Effect := TNormalDrawEffect.Create(nX, nY, g_WMon14Img, 410, 6, 120, True);
                    break;
                case 32:
                    Effect = new THeroCharEffect(WMFile.Units.WMFile.g_wui, 1222, 12, 120, Actor);
                    break;
                // Modify the A .. B: 33 .. 40
                case 33:
                    Effect = new THeroCharEffect(WMFile.Units.WMFile.g_wui, 1080 + 10 * (nType - 33), 6, 220, Actor);
                    break;
                // Modify the A .. B: 41 .. 43
                case 41:
                    Effect = new THeroCharEffect(WMFile.Units.WMFile.g_wui, 1170 + 10 * (nType - 41), 6, 220, Actor);
                    break;
            }
            if (Effect != null)
            {
                // Effect.MagOwner := g_MySelf;
                g_PlayScene.m_EffectList.Add(Effect);
            }
        }

        public void SelectChr(string sChrName)
        {
            // PlayScene.EdChrNamet.Text := sChrName;

        }

        public bool GetNpcImg(short wAppr, ref TWMBaseImages WMImage)
        {
            bool result;
            int i;
            result = false;
            for (i = 0; i < NpcImageList.Count; i++)
            {
                WMImage = ((TWMBaseImages)(NpcImageList[i]));
                if (WMImage.Appr == wAppr)
                {
                    result = true;
                    return result;
                }
            }
            return result;
        }

        private void ClientGetPasswordStatus(ClientPacket msg, string body)
        {
        }

        public void SendPassword(string sPassword, int nIdent)
        {
            ClientPacket DefMsg;
            DefMsg = Grobal2.MakeDefaultMsg(Grobal2.CM_PASSWORD, 0, nIdent, 0, 0);
            SendSocket(EDcode.EncodeMessage(DefMsg) + EDcode.EncodeString(sPassword));
        }

        public void SendRefineItems(TClientRefineItem[] cr)
        {
            ClientPacket DefMsg;
            DefMsg = Grobal2.MakeDefaultMsg(Grobal2.CM_REFINEITEM, 0, 0, 0, 0);
            SendSocket(EDcode.EncodeMessage(DefMsg) + EDcode.EncodeBuffer(cr, sizeof(Grobal2.TClientRefineItem)));
        }

        public void SendStallInfo(TClientStallItems cr, int cnt)
        {
            ClientPacket DefMsg;
            DefMsg = Grobal2.MakeDefaultMsg(Grobal2.CM_OPENSTALL, MShare.g_MySelf.m_nRecogId, 0, 0, cnt);
            SendSocket(EDcode.EncodeMessage(DefMsg) + EDcode.EncodeBuffer(cr, sizeof(TClientStallItems)));
        }

        public void SendGateTick()
        {
        }

        public void SendGetbackDelCharName(string sName)
        {
            ClientPacket DefMsg;
            DefMsg = Grobal2.MakeDefaultMsg(Grobal2.CM_GETBACKDELCHR, 0, 0, 0, 0);
            SendSocket(EDcode.EncodeMessage(DefMsg) + EDcode.EncodeString(sName));
        }

        public void SendHeroItemToMasterBag(int nMakeIdx, string sItemName)
        {
            ClientPacket DefMsg;
            DefMsg = Grobal2.MakeDefaultMsg(Grobal2.CM_HEROADDITEMTOPLAYER, nMakeIdx, 0, 0, 0);
            SendSocket(EDcode.EncodeMessage(DefMsg) + EDcode.EncodeString(sItemName));
        }

        public void SendMasterItemToHeroBag(int nMakeIdx, string sItemName)
        {
            ClientPacket DefMsg;
            DefMsg = Grobal2.MakeDefaultMsg(Grobal2.CM_PLAYERADDITEMTOHERO, nMakeIdx, 0, 0, 0);
            SendSocket(EDcode.EncodeMessage(DefMsg) + EDcode.EncodeString(sItemName));
        }

        private void ClientGetServerConfig(ClientPacket msg, string sBody)
        {
            TClientConf ClientConf;
#if CONFIGTEST
            MShare.g_boOpenAutoPlay = true;
            MShare.g_boSpeedRate = true;
            MShare.g_boSpeedRateShow = MShare.g_boSpeedRate;
#else
            MShare.g_boOpenAutoPlay = LoByte(HUtil32.LoWord(msg.Recog)) == 1;
            MShare.g_boSpeedRate = msg.Series != 0;
            MShare.g_boSpeedRateShow = MShare.g_boSpeedRate;
#endif // CONFIGTEST
            MShare.g_boCanRunMon = HiByte(HUtil32.LoWord(msg.Recog)) == 1;
            MShare.g_boCanRunNpc = LoByte(HUtil32.HiWord(msg.Recog)) == 1;
            MShare.g_boCanRunAllInWarZone = HiByte(HUtil32.HiWord(msg.Recog)) == 1;
            sBody = EDcode.DeCodeString(sBody);
            EDcode.DecodeBuffer(sBody, ClientConf, sizeof(ClientConf));
            MShare.g_boCanRunHuman = ClientConf.boRUNHUMAN;
            MShare.g_boCanRunMon = ClientConf.boRUNMON;
            MShare.g_boCanRunNpc = ClientConf.boRunNpc;
            MShare.g_boCanRunAllInWarZone = ClientConf.boWarRunAll;
            MShare.g_DeathColorEffect = ((TColorEffect)(HUtil32._MIN(8, ClientConf.btDieColor)));
            // 死亡颜色  Development 2018-12-29
            // g_nHitTime := ClientConf.wHitIime;
            // g_dwSpellTime := ClientConf.wSpellTime;
            // g_nItemSpeed := ClientConf.btItemSpeed;
            MShare.g_boCanStartRun = ClientConf.boCanStartRun;
            MShare.g_boParalyCanRun = ClientConf.boParalyCanRun;
            MShare.g_boParalyCanWalk = ClientConf.boParalyCanWalk;
            MShare.g_boParalyCanHit = ClientConf.boParalyCanHit;
            MShare.g_boParalyCanSpell = ClientConf.boParalyCanSpell;
            MShare.g_boShowRedHPLable = ClientConf.boShowRedHPLable;
            MShare.g_boShowHPNumber = ClientConf.boShowHPNumber;
            MShare.g_boShowJobLevel = ClientConf.boShowJobLevel;
            MShare.g_boDuraAlert = ClientConf.boDuraAlert;
            MShare.g_boMagicLock = ClientConf.boMagicLock;
            // g_boAutoPuckUpItem := ClientConf.boAutoPuckUpItem;

        }

        public void OpenConfigDlg(bool boStatus)
        {
            // Form2.ParentWindow := frmMain.Handle;
            // Form2.show;

        }

        public void ProcessCommand(string sData)
        {
            string sCmd;
            string sParam1;
            string sParam2;
            string sParam3;
            string sParam4;
            string sParam5;
            sData = HUtil32.GetValidStr3(sData, ref sCmd, new string[] { " ", ":", "\09" });
            sData = HUtil32.GetValidStr3(sData, ref sCmd, new string[] { " ", ":", "\09" });
            sData = HUtil32.GetValidStr3(sData, ref sParam1, new string[] { " ", ":", "\09" });
            sData = HUtil32.GetValidStr3(sData, ref sParam2, new string[] { " ", ":", "\09" });
            sData = HUtil32.GetValidStr3(sData, ref sParam3, new string[] { " ", ":", "\09" });
            sData = HUtil32.GetValidStr3(sData, ref sParam4, new string[] { " ", ":", "\09" });
            sData = HUtil32.GetValidStr3(sData, ref sParam5, new string[] { " ", ":", "\09" });
            if ((sCmd).ToLower().CompareTo(("ShowHumanMsg").ToLower()) == 0)
            {
                CmdShowHumanMsg(sParam1, sParam2, sParam3, sParam4, sParam5);
            }
        }

        // procedure SetInputStatus();
        private void CmdShowHumanMsg(string sParam1, string sParam2, string sParam3, string sParam4, string sParam5)
        {
            string sHumanName;
            sHumanName = sParam1;
            if ((sHumanName != "") && (sHumanName[1] == "C"))
            {
                g_PlayScene.MemoLog.Clear();
                return;
            }
            if (sHumanName != "")
            {
                ShowMsgActor = g_PlayScene.FindActor(sHumanName);
                if (ShowMsgActor == null)
                {
                    DScreen.AddChatBoardString(Format("%s没找到", new string[] { sHumanName }), Color.White, Color.Red);
                    return;
                }
            }
            g_boShowMemoLog = !g_boShowMemoLog;
            g_PlayScene.MemoLog.Clear();
            g_PlayScene.MemoLog.Visible = g_boShowMemoLog;
        }

        // Note: the original parameters are Object Sender, ref bool CanClose
        public void FormCloseQuery(System.Object Sender, System.ComponentModel.CancelEventArgs _e1)
        {
            CanClose = false;
            if (!MShare.g_DlgInitialize)
            {
                if (MShare.g_MySelf != null)
                {
                    SaveBagsData();
                    MShare.SaveItemFilter();
                }
                CanClose = true;
            }
        }

        public TClientMagic GetMagicByID(int magid)
        {
            TClientMagic result;
            result = null;
            if ((magid <= 0) || (magid >= 255))
            {
                return result;
            }
            result = MShare.g_MagicArr[0][magid];
            return result;
        }

        public TClientMagic HeroGetMagicByID(int magid)
        {
            TClientMagic result;
            int i;
            result = null;
            for (i = MShare.g_HeroMagicList.Count - 1; i >= 0; i--)
            {
                if (((TClientMagic)(MShare.g_HeroMagicList[i])).Def.wMagicID == magid)
                {
                    result = ((TClientMagic)(MShare.g_HeroMagicList[i]));
                    break;
                }
            }
            return result;
        }

        public void TimerHeroActorTimer(System.Object Sender, System.EventArgs _e1)
        {
            if ((MShare.g_MySelf != null))
            {
                if (!MShare.g_boMapMoving && !MShare.g_boServerChanging)
                {
                    if (MShare.GetTickCount() - MShare.g_MySelf.m_dwAutoTecTick > 100)
                    {
                        MShare.g_MySelf.m_dwAutoTecTick = MShare.GetTickCount();
                        ActorAutoEat(MShare.g_MySelf);
                    }
                }
                if (MShare.g_MySelf.m_HeroObject != null)
                {
                    if (MShare.GetTickCount() - MShare.g_MySelf.m_dwAutoTecHeroTick > 100)
                    {
                        MShare.g_MySelf.m_dwAutoTecHeroTick = MShare.GetTickCount();
                        HeroActorAutoEat(MShare.g_MySelf);
                    }
                }
            }
        }

        public void TimerPacketTimer(System.Object Sender, System.EventArgs _e1)
        {
            string data;
            BufferStr = BufferStr + SocStr;
            SocStr = "";
            if (BufferStr != "")
            {
                while (BufferStr.Length >= 2)
                {
                    if (MShare.g_boMapMovingWait)
                    {
                        break;
                    }
                    if (BufferStr.IndexOf("!") <= 0)
                    {
                        break;
                    }
                    BufferStr = HUtil32.ArrestStringEx(BufferStr, "#", "!", ref data);
                    if (data != "")
                    {
                        DecodeMessagePacket(data, 0);
                        // DScreen.AddChatBoardString(data, clWhite, clBlue);
                    }
                    else
                    {
                        break;
                    }
                }
            }
            // and (CanNextAction and ServerAcceptNextAction and CanNextHit())
            if (MShare.g_SeriesSkillFire_100 && (MShare.g_MySelf != null) && MShare.g_MySelf.ActionFinished())
            {
                MShare.g_SeriesSkillFire_100 = false;
                MShare.g_nCurrentMagic2 = 1;
                MShare.g_nCurrentMagic = 888;
                UseMagic(MShare.g_nMouseX, MShare.g_nMouseY, MShare.g_MagicArr[0][MShare.g_SeriesSkillArr[0]], false, true);
            }
            if (MShare.g_boQueryPrice)
            {
                if (MShare.GetTickCount() - MShare.g_dwQueryPriceTime > 500)
                {
                    MShare.g_boQueryPrice = false;
                    switch (FrmDlg.SpotDlgMode)
                    {
                        case dmSell:
                            SendQueryPrice(MShare.g_nCurMerchant, MShare.g_SellDlgItem.MakeIndex, MShare.g_SellDlgItem.Item.Name);
                            break;
                        case dmRepair:
                            SendQueryRepairCost(MShare.g_nCurMerchant, MShare.g_SellDlgItem.MakeIndex, MShare.g_SellDlgItem.Item.Name);
                            break;
                        case dmExchangeBook:
                            SendQueryExchgBook(MShare.g_nCurMerchant, MShare.g_SellDlgItem.MakeIndex, MShare.g_SellDlgItem.Item.Name);
                            break;
                    }
                }
            }
            if (MShare.g_nBonusPoint > 0)
            {
                if (!FrmDlg.DBotPlusAbil.Visible)
                {
                    FrmDlg.DBotPlusAbil.Visible = true;
                }
            }
            else
            {
                if (FrmDlg.DBotPlusAbil.Visible)
                {
                    FrmDlg.DBotPlusAbil.Visible = false;
                }
            }
        }

        public void SmartChangePoison(TClientMagic pcm)
        {
            string Str;
            string cStr;
            int i;
            if (MShare.g_MySelf == null)
            {
                return;
            }
            MShare.g_MySelf.m_btPoisonDecHealth = 0;
            if (new ArrayList(new int[] { 13, 30, 43, 55, 57 }).Contains(pcm.Def.wMagicID))
            {
                Str = "符";
                cStr = "符";
            }
            else if (new ArrayList(new int[] { 6, 38 }).Contains(pcm.Def.wMagicID))
            {
                if ((MShare.g_MagicTarget != null))
                {
                    Str = "药";
                    MShare.g_boExchgPoison = !MShare.g_boExchgPoison;
                    if (MShare.g_boExchgPoison)
                    {
                        // dec health
                        MShare.g_MySelf.m_btPoisonDecHealth = 1;
                        cStr = "灰";
                    }
                    else
                    {
                        MShare.g_MySelf.m_btPoisonDecHealth = 2;
                        cStr = "黄";
                    }
                }
                else
                {
                    return;
                }
            }
            else
            {
                return;
            }
            // g_boCheckPoison := True;
            if ((MShare.g_UseItems[Grobal2.U_BUJUK].s.StdMode == 25) && (MShare.g_UseItems[Grobal2.U_BUJUK].s.Shape != 6) && (MShare.g_UseItems[Grobal2.U_BUJUK].s.Name.IndexOf(cStr) > 0))
            {
                // g_boCheckPoison := False;
                return;
            }
            MShare.g_boCheckTakeOffPoison = false;
            MShare.g_WaitingUseItem.Index = Grobal2.U_BUJUK;
            for (i = 6; i < MShare.MAXBAGITEMCL; i++)
            {
                if ((MShare.g_ItemArr[i].Item.NeedIdentify < 4) && (MShare.g_ItemArr[i].Item.ItemtdMode == 25) && (MShare.g_ItemArr[i].Item.Itemhape != 6) && (MShare.g_ItemArr[i].Item.Name.IndexOf(Str) > 0) && (MShare.g_ItemArr[i].Item.Name.IndexOf(cStr) > 0))
                {
                    MShare.g_WaitingUseItem.Item = MShare.g_ItemArr[i];
                    MShare.g_ItemArr[i].Item.Name = "";
                    MShare.g_boCheckTakeOffPoison = true;
                    SendTakeOnItem(MShare.g_WaitingUseItem.Index, MShare.g_WaitingUseItem.Item.MakeIndex, MShare.g_WaitingUseItem.Item.Item.Name);
                    ClFunc.ArrangeItembag();
                    return;
                }
            }
            if (Str == "符")
            {
                DScreen.AddChatBoardString("你的[护身符]已经用完", Color.White, Color.Blue);
            }
            else if (MShare.g_boExchgPoison)
            {
                DScreen.AddChatBoardString("你的[灰色药粉]已经用完", Color.White, Color.Blue);
            }
            else
            {
                DScreen.AddChatBoardString("你的[黄色药粉]已经用完", Color.White, Color.Blue);
            }
        }

        private void ClientOpenBook(ClientPacket msg, string sBody)
        {
            if (sBody != "")
            {
                MShare.g_sBookLabel = sBody;
            }
            else
            {
                MShare.g_sBookLabel = "";
            }
            MShare.g_nBookPath = msg.Param;
            MShare.g_nBookPage = msg.Tag;
            MShare.g_HillMerchant = msg.Recog;
            if (MShare.g_nBookPath > 0)
            {
                FrmDlg.DWBookBkgnd.Visible = true;
            }
        }

        public bool IsRegisteredHotKey(double HotKey)
        {
            bool result;
            result = false;
            if (!FrmDlg.DWGameConfig.Visible)
            {
                return result;
            }
            if (FrmDlg.DxEditSSkill.HotKey == HotKey)
            {
                result = true;
                return result;
            }
            if (FrmDlg.DEHeroCallHero.HotKey == HotKey)
            {
                result = true;
                return result;
            }
            if (FrmDlg.DEHeroSetTarget.HotKey == HotKey)
            {
                result = true;
                return result;
            }
            if (FrmDlg.DEHeroUnionHit.HotKey == HotKey)
            {
                result = true;
                return result;
            }
            if (FrmDlg.DEHeroSetAttackState.HotKey == HotKey)
            {
                result = true;
                return result;
            }
            if (FrmDlg.DEHeroSetGuard.HotKey == HotKey)
            {
                result = true;
                return result;
            }
            if (FrmDlg.DESwitchAttackMode.HotKey == HotKey)
            {
                result = true;
                return result;
            }
            if (FrmDlg.DESwitchMiniMap.HotKey == HotKey)
            {
                result = true;
                return result;
            }
            return result;
        }

        public void TimerAutoMagicTimer(System.Object Sender, System.EventArgs _e1)
        {
            TClientMagic pcm;
            int nspeed;
            if ((MShare.g_MySelf != null) && MShare.g_MySelf.m_StallMgr.OnSale)
            {
                return;
            }
            if ((MShare.g_MySelf != null) && MShare.g_boAutoSay && (FrmDlg.DBAotoSay.tag == 0) && (MShare.g_MySelf.m_sAutoSayMsg != ""))
            {
                if (MShare.GetTickCount() - FrmDlg.m_sAutoSayMsgTick > 30 * 1000)
                {
                    FrmDlg.m_sAutoSayMsgTick = MShare.GetTickCount();
                    SendSay(MShare.g_MySelf.m_sAutoSayMsg);
                }
            }
            if ((MShare.g_MySelf != null) && IsUnLockAction())
            {
                if (CanNextAction() && ServerAcceptNextAction())
                {
                    nspeed = 0;
                    if (MShare.g_boSpeedRate)
                    {
                        nspeed = MShare.g_MagSpeedRate * 20;
                    }
                    if ((MShare.GetTickCount() - MShare.g_dwLatestSpellTick > (MShare.g_dwSpellTime + MShare.g_dwMagicDelayTime - (long)nspeed)))
                    {
                        // and (g_MySelf.m_nState and $00000100 = 0)
                        if (MShare.g_gcTec[4] && (MShare.g_MySelf.m_nState & 0x00100000 == 0))
                        {
                            if (MShare.g_MagicArr[0][31] != null)
                            {
                                frmMain.UseMagic(MShare.SCREENWIDTH / 2, MShare.SCREENHEIGHT / 2, MShare.g_MagicArr[0][31]);
                                return;
                            }
                        }
                        switch (MShare.g_MySelf.m_btJob)
                        {
                            case 0:
                                if (MShare.g_gcTec[3] && !MShare.g_boNextTimePursueHit)
                                {
                                    pcm = GetMagicByID(56);
                                    if (pcm != null)
                                    {
                                        UseMagic(MShare.SCREENWIDTH / 2, MShare.SCREENHEIGHT / 2, pcm);
                                        return;
                                    }
                                }
                                if (MShare.g_gcTec[11] && !MShare.g_boNextTimeSmiteLongHit2)
                                {
                                    pcm = GetMagicByID(113);
                                    if (pcm != null)
                                    {
                                        UseMagic(MShare.SCREENWIDTH / 2, MShare.SCREENHEIGHT / 2, pcm);
                                        return;
                                    }
                                }
                                if (MShare.g_gcTec[2] && !MShare.g_boNextTimeFireHit)
                                {
                                    pcm = GetMagicByID(26);
                                    if (pcm != null)
                                    {
                                        UseMagic(MShare.SCREENWIDTH / 2, MShare.SCREENHEIGHT / 2, pcm);
                                        return;
                                    }
                                }
                                if (MShare.g_gcTec[13] && !MShare.g_boCanSLonHit)
                                {
                                    pcm = GetMagicByID(66);
                                    if (pcm != null)
                                    {
                                        UseMagic(MShare.SCREENWIDTH / 2, MShare.SCREENHEIGHT / 2, pcm);
                                        return;
                                    }
                                }
                                if (MShare.g_gcTec[9] && !MShare.g_boNextTimeTwinHit)
                                {
                                    pcm = GetMagicByID(43);
                                    if (pcm != null)
                                    {
                                        UseMagic(MShare.SCREENWIDTH / 2, MShare.SCREENHEIGHT / 2, pcm);
                                        return;
                                    }
                                }
                                break;
                            case 2:
                                // 1: begin
                                // if g_gcTec[4] and (g_MySelf.m_nState and $00100000 = 0) then begin
                                // pcm := GetMagicByID(31);
                                // if pcm <> nil then
                                // UseMagic(SCREENWIDTH div 2, SCREENHEIGHT div 2, pcm);
                                // end;
                                // end;
                                if (MShare.g_gcTec[6] && (MShare.g_MySelf.m_nState & 0x00800000 == 0))
                                {
                                    pcm = GetMagicByID(18);
                                    if (pcm != null)
                                    {
                                        UseMagic(MShare.SCREENWIDTH / 2, MShare.SCREENHEIGHT / 2, pcm);
                                    }
                                }
                                break;
                        }
                        if (MShare.g_gcTec[7] && (MShare.GetTickCount() - MShare.g_MySelf.m_dwPracticeTick > ((long)HUtil32._MAX(500, MShare.g_gnTecTime[8]))))
                        {
                            MShare.g_MySelf.m_dwPracticeTick = MShare.GetTickCount();
                            pcm = GetMagicByID(MShare.g_gnTecPracticeKey);
                            if (pcm != null)
                            {
                                UseMagic(MShare.g_nMouseX, MShare.g_nMouseY, pcm);
                            }
                        }
                    }
                }
            }
        }

        public int DirToDX(int Direction, int tdir)
        {
            int result;
            if (Direction == -1)
            {
                Direction = 7;
            }
            switch (Direction)
            {
                case 0:
                case 4:
                    result = 0;
                    break;
                // Modify the A .. B: 1 .. 3
                case 1:
                    result = 1 * tdir;
                    break;
                default:
                    result = -1 * tdir;
                    break;
            }
            return result;
        }

        public int DirToDY(int Direction, int tdir)
        {
            int result;
            if (Direction == -1)
            {
                Direction = 7;
            }
            switch (Direction)
            {
                case 2:
                case 6:
                    result = 0;
                    break;
                // Modify the A .. B: 3 .. 5
                case 3:
                    result = 1 * tdir;
                    break;
                default:
                    result = -1 * tdir;
                    break;
            }
            return result;
        }

        public void TimerAutoMoveTimer(System.Object Sender, System.EventArgs _e1)
        {
            int ndir;
            int X1;
            int Y1;
            int X2;
            int Y2;
            int X3;
            int Y3;
            bool boCanRun;
            if ((MShare.g_MySelf == null) || (Map.m_MapBuf == null) || (!CSocket.Active))
            {
                return;
            }
            if (MapUnit.g_MapPath != null)
            {
                if (((MShare.g_MySelf.m_nCurrX == MShare.g_MySelf.m_nTagX) && (MShare.g_MySelf.m_nCurrY == MShare.g_MySelf.m_nTagY)))
                {
                    TimerAutoMove.Enabled = false;
                    DScreen.AddChatBoardString("已经到达终点", GetRGB(5), Color.White);
                    MapUnit.g_MapPath = new Point[0];
                    MapUnit.g_MapPath = null;
                    MShare.g_MySelf.m_nTagX = 0;
                    MShare.g_MySelf.m_nTagY = 0;
                }
                if (CanNextAction() && ServerAcceptNextAction() && IsUnLockAction())
                {
                    if (g_MoveStep <= MapUnit.g_MapPath.GetUpperBound(0))
                    {
                        MShare.g_nTargetX = MapUnit.g_MapPath[g_MoveStep].X;
                        MShare.g_nTargetY = MapUnit.g_MapPath[g_MoveStep].X;
                        while ((Math.Abs(MShare.g_MySelf.m_nCurrX - MShare.g_nTargetX) <= 1) && (Math.Abs(MShare.g_MySelf.m_nCurrY - MShare.g_nTargetY) <= 1))
                        {
                            boCanRun = false;
                            if (g_MoveStep + 1 <= MapUnit.g_MapPath.GetUpperBound(0))
                            {
                                X1 = MShare.g_MySelf.m_nCurrX;
                                Y1 = MShare.g_MySelf.m_nCurrY;
                                X2 = MapUnit.g_MapPath[(g_MoveStep + 1)].X;
                                Y2 = MapUnit.g_MapPath[(g_MoveStep + 1)].X;
                                ndir = ClFunc.GetNextDirection(X1, Y1, X2, Y2);
                                ClFunc.GetNextPosXY(ndir, ref X1, ref Y1);
                                X3 = MShare.g_MySelf.m_nCurrX;
                                Y3 = MShare.g_MySelf.m_nCurrY;
                                ClFunc.GetNextRunXY(ndir, ref X3, ref Y3);
                                if ((MapUnit.g_MapPath[(g_MoveStep + 1)].X == X3) && (MapUnit.g_MapPath[(g_MoveStep + 1)].X == Y3))
                                {
                                    boCanRun = true;
                                }
                            }
                            if (boCanRun && Map.CanMove(X1, Y1) && !g_PlayScene.CrashMan(X1, Y1))
                            {
                                g_MoveStep++;
                                MShare.g_nTargetX = MapUnit.g_MapPath[g_MoveStep].X;
                                MShare.g_nTargetY = MapUnit.g_MapPath[g_MoveStep].X;
                                if (g_MoveStep >= MapUnit.g_MapPath.GetUpperBound(0))
                                {
                                    break;
                                }
                            }
                            else
                            {
                                MShare.g_nTargetX = MapUnit.g_MapPath[g_MoveStep].X;
                                MShare.g_nTargetY = MapUnit.g_MapPath[g_MoveStep].X;
                                break;
                            }
                        }
                        if ((Math.Abs(MShare.g_MySelf.m_nCurrX - MShare.g_MySelf.m_nTagX) <= 1) && (Math.Abs(MShare.g_MySelf.m_nCurrY - MShare.g_MySelf.m_nTagY) <= 1))
                        {
                            MShare.g_nTargetX = MShare.g_MySelf.m_nTagX;
                            MShare.g_nTargetY = MShare.g_MySelf.m_nTagY;
                        }
                        if ((Math.Abs(MShare.g_MySelf.m_nCurrX - MShare.g_nTargetX) <= 1) && (Math.Abs(MShare.g_MySelf.m_nCurrY - MShare.g_nTargetY) <= 1))
                        {
                            // 目标座标
                            MShare.g_ChrAction = TChrAction.caWalk;
                            g_MoveBusy = true;
                        }
                        else
                        {
                            if (MShare.g_MySelf.CanRun() > 0)
                            {
                                MShare.g_ChrAction = TChrAction.caRun;
                                g_MoveBusy = true;
                            }
                            else
                            {
                                MShare.g_ChrAction = TChrAction.caWalk;
                                g_MoveBusy = true;
                            }
                        }
                    }
                }
            }
        }

        public void TimerAutoPlayTimer_randomtag()
        {
            int i;
            i = 0;
            b = false;
            ndir = MShare.g_MySelf.m_btDir;
            if ((new System.Random(28)).Next() == 0)
            {
                ndir = (new System.Random(8)).Next();
            }
            while (i < 16)
            {
                if (!GetNextPosition(MShare.g_MySelf.m_nCurrX, MShare.g_MySelf.m_nCurrY, ndir, 2, ref MShare.g_nTargetX, ref MShare.g_nTargetY))
                {
                    ClFunc.GetFrontPosition(MShare.g_MySelf.m_nCurrX, MShare.g_MySelf.m_nCurrY, ndir, ref MShare.g_nTargetX, ref MShare.g_nTargetY);
                    if (!g_PlayScene.CanWalk(MShare.g_nTargetX, MShare.g_nTargetY))
                    {
                        MShare.g_MySelf.SendMsg(Grobal2.CM_TURN, MShare.g_MySelf.m_nCurrX, MShare.g_MySelf.m_nCurrY, (new System.Random(8)).Next(), 0, 0, "", 0);
                        i++;
                    }
                    else
                    {
                        b = true;
                        break;
                    }
                }
                else
                {
                    if (g_PlayScene.CanWalk(MShare.g_nTargetX, MShare.g_nTargetY))
                    {
                        b = true;
                        break;
                    }
                    else
                    {
                        ndir = (new System.Random(8)).Next();
                        i++;
                    }
                }
            }
        }

        public void TimerAutoPlayTimer(System.Object Sender, System.EventArgs _e1)
        {
            TFindNode T;
            int ndir;
            int X1;
            int Y1;
            bool b;
            MShare.g_sAPStr = "";
            MShare.g_boAPAutoMove = false;
            if (MShare.g_MySelf == null)
            {
                return;
            }
            if (!MShare.g_boOpenAutoPlay)
            {
                return;
            }
            if (MShare.g_MySelf.m_boDeath)
            {
                MShare.g_gcAss[0] = false;
                MShare.g_APTagget = null;
                MShare.g_AutoPicupItem = null;
                MShare.g_nAPStatus = -1;
                MShare.g_nTargetX = -1;
                frmMain.TimerAutoPlay.Enabled = MShare.g_gcAss[0];
                WaitAndPass(2000);
                DScreen.ClearHint();
                Application.Terminate;
                return;
            }
            if ((MShare.g_MySelf.m_HeroObject != null))
            {
                if (MShare.GetTickCount() - FrmDlg.m_dwUnRecallHeroTick > 3000)
                {
                    if ((MShare.g_MySelf.m_HeroObject.m_Abil.HP != 0) && (Math.Round((MShare.g_MySelf.m_HeroObject.m_Abil.HP / MShare.g_MySelf.m_HeroObject.m_Abil.MaxHP) * 100) < 20))
                    {
                        FrmDlg.m_dwUnRecallHeroTick = MShare.GetTickCount();
                        MShare.g_pbRecallHero = true;
                        FrmDlg.ClientCallHero();
                    }
                }
            }
            else
            {
                if (MShare.GetTickCount() - FrmDlg.m_dwUnRecallHeroTick > 62000)
                {
                    if (HeroActor.TargetHumCount(MShare.g_MySelf) <= 3)
                    {
                        FrmDlg.m_dwUnRecallHeroTick = MShare.GetTickCount();
                        MShare.g_pbRecallHero = true;
                        FrmDlg.ClientCallHero();
                    }
                }
            }
            MShare.g_AutoPicupItem = null;
            switch (HeroActor.GetAutoPalyStation())
            {
                case 0:
                    if (!EatItemName("回城卷") && !EatItemName("回城卷包") && !EatItemName("盟重传送石") && !EatItemName("比奇传送石"))
                    {
                        DScreen.AddChatBoardString("[挂机] 你的回城卷已用完,已挂机停止!!!", Color.White, Color.Red);
                    }
                    else
                    {
                        DScreen.AddChatBoardString("[挂机] 回城并挂机停止!!!", Color.White, Color.Red);
                    }
                    MShare.g_gcAss[0] = false;
                    MShare.g_APTagget = null;
                    MShare.g_AutoPicupItem = null;
                    MShare.g_nAPStatus = -1;
                    MShare.g_nTargetX = -1;
                    frmMain.TimerAutoPlay.Enabled = MShare.g_gcAss[0];
                    MShare.g_boAPAutoMove = true;
                    break;
                case 1:// 此时为该怪物首次被发现，自动寻找路径
                    if (HeroActor.HeroAttackTagget(MShare.g_APTagget))
                    {
                        return;
                    }
                    if (MShare.g_APTagget != null)
                    {
                        MShare.g_nTargetX = MShare.g_APTagget.m_nCurrX;
                        MShare.g_nTargetY = MShare.g_APTagget.m_nCurrY;
                        HeroActor.AP_findpath(MShare.g_MySelf.m_nCurrX, MShare.g_MySelf.m_nCurrY, MShare.g_APTagget.m_nCurrX, MShare.g_APTagget.m_nCurrY);
                    }
                    MShare.g_nTargetX = -1;
                    MShare.g_nAPStatus = 1;
                    MShare.g_boAPAutoMove = true;
                    break;
                case 2:// 此时该物品为首次发现，自动寻找路径
                    if ((MShare.g_AutoPicupItem != null) && ((MShare.g_nAPStatus != 2) || (MShare.g_APPathList.Count == 0)))
                    {
                        MShare.g_nTargetX = MShare.g_AutoPicupItem.X;
                        MShare.g_nTargetY = MShare.g_AutoPicupItem.Y;
                        HeroActor.AP_findpath(MShare.g_MySelf.m_nCurrX, MShare.g_MySelf.m_nCurrY, MShare.g_nTargetX, MShare.g_nTargetY);
                        MShare.g_nTargetX = -1;
                        MShare.g_sAPStr = Format("[挂机] 物品目标：%s(%d,%d) 正在去拾取", new int[] { MShare.g_AutoPicupItem.Name, MShare.g_AutoPicupItem.X, MShare.g_AutoPicupItem.Y });
                    }
                    else if ((MShare.g_AutoPicupItem != null))
                    {
                        MShare.g_nTargetX = MShare.g_AutoPicupItem.X;
                        MShare.g_nTargetY = MShare.g_AutoPicupItem.Y;
                        HeroActor.AP_findpath(MShare.g_MySelf.m_nCurrX, MShare.g_MySelf.m_nCurrY, MShare.g_nTargetX, MShare.g_nTargetY);
                        MShare.g_nTargetX = -1;
                        MShare.g_sAPStr = Format("[挂机] 物品目标：%s(%d,%d) 正在去拾取", new int[] { MShare.g_AutoPicupItem.Name, MShare.g_AutoPicupItem.X, MShare.g_AutoPicupItem.Y });
                    }
                    MShare.g_nAPStatus = 2;
                    MShare.g_boAPAutoMove = true;
                    break;
                case 3:
                    if ((MShare.g_APMapPath != null) && (MShare.g_APStep >= 0) && (MShare.g_APStep <= MShare.g_APMapPath.GetUpperBound(0)))
                    {
                        if ((MShare.g_APMapPath.GetUpperBound(0) > 0))
                        {
                            MShare.g_nTargetX = MShare.g_APMapPath[MShare.g_APStep].X;
                            MShare.g_nTargetY = MShare.g_APMapPath[MShare.g_APStep].X;
                            HeroActor.AP_findpath(MShare.g_MySelf.m_nCurrX, MShare.g_MySelf.m_nCurrY, MShare.g_nTargetX, MShare.g_nTargetY);
                            MShare.g_sAPStr = Format("[挂机] 循路搜寻目标(%d,%d)", new int[] { MShare.g_nTargetX, MShare.g_nTargetY });
                            MShare.g_nTargetX = -1;
                        }
                        else
                        {
                            if ((MShare.g_nTargetX == -1) || (MShare.g_APPathList.Count == 0))
                            {
                                if (b)
                                {
                                    HeroActor.AP_findpath(MShare.g_MySelf.m_nCurrX, MShare.g_MySelf.m_nCurrY, MShare.g_nTargetX, MShare.g_nTargetY);
                                }
                                // memory leak !!!
                                MShare.g_sAPStr = Format("[挂机] 定点随机搜寻目标(%d,%d)", new int[] { MShare.g_APMapPath[MShare.g_APStep].X, MShare.g_APMapPath[MShare.g_APStep].X });
                                MShare.g_nTargetX = -1;
                            }
                        }
                    }
                    else if ((MShare.g_nTargetX == -1) || (MShare.g_APPathList.Count == 0))
                    {
                        TimerAutoPlayTimer_randomtag();
                        if (b)
                        {
                            HeroActor.AP_findpath(MShare.g_MySelf.m_nCurrX, MShare.g_MySelf.m_nCurrY, MShare.g_nTargetX, MShare.g_nTargetY);
                        }
                        MShare.g_sAPStr = "[挂机] 随机搜寻目标...";
                        MShare.g_nTargetX = -1;
                    }
                    MShare.g_nAPStatus = 3;
                    MShare.g_boAPAutoMove = true;
                    break;
                case 4:
                    if ((MShare.g_APMapPath != null) && (MShare.g_APStep >= 0) && (MShare.g_APStep <= MShare.g_APMapPath.GetUpperBound(0)))
                    {
                        if ((MShare.g_APLastPoint.X >= 0))
                        {
                            MShare.g_nTargetX = MShare.g_APLastPoint.X;
                            MShare.g_nTargetY = MShare.g_APLastPoint.X;
                            HeroActor.AP_findpath(MShare.g_MySelf.m_nCurrX, MShare.g_MySelf.m_nCurrY, MShare.g_nTargetX, MShare.g_nTargetY);
                        }
                        else
                        {
                            MShare.g_nTargetX = MShare.g_APMapPath[MShare.g_APStep].X;
                            MShare.g_nTargetY = MShare.g_APMapPath[MShare.g_APStep].X;
                            HeroActor.AP_findpath(MShare.g_MySelf.m_nCurrX, MShare.g_MySelf.m_nCurrY, MShare.g_nTargetX, MShare.g_nTargetY);
                        }
                        MShare.g_sAPStr = Format("[挂机] 超出搜寻范围,返回(%d,%d)", new int[] { MShare.g_nTargetX, MShare.g_nTargetY });
                        MShare.g_nTargetX = -1;
                    }
                    else if ((MShare.g_nTargetX == -1) || (MShare.g_APPathList.Count == 0))
                    {
                        TimerAutoPlayTimer_randomtag();
                        if (b)
                        {
                            HeroActor.AP_findpath(MShare.g_MySelf.m_nCurrX, MShare.g_MySelf.m_nCurrY, MShare.g_nTargetX, MShare.g_nTargetY);
                        }
                        MShare.g_sAPStr = Format("[挂机] 超出搜寻范围,随机搜寻目标(%d,%d)", new int[] { MShare.g_nTargetX, MShare.g_nTargetY });
                        MShare.g_nTargetX = -1;
                    }
                    MShare.g_nAPStatus = 3;
                    MShare.g_boAPAutoMove = true;
                    break;
            }
            if ((MShare.g_APPathList.Count > 0) && ((MShare.g_nTargetX == -1) || ((MShare.g_nTargetX == MShare.g_MySelf.m_nCurrX) && (MShare.g_nTargetY == MShare.g_MySelf.m_nCurrY))))
            {
                T = ((TFindNode)(MShare.g_APPathList[0]));
                MShare.g_nTargetX = T.X;
                MShare.g_nTargetY = T.Y;
                if (MShare.g_nAPStatus >= 1 && MShare.g_nAPStatus <= 4)
                {
                    if ((Math.Abs(MShare.g_MySelf.m_nCurrX - MShare.g_nTargetX) <= 1) && (Math.Abs(MShare.g_MySelf.m_nCurrY - MShare.g_nTargetY) <= 1))
                    {
                        if (g_PlayScene.CanWalk(MShare.g_nTargetX, MShare.g_nTargetY))
                        {
                            if ((MShare.g_nAPStatus == 2) && (MShare.g_AutoPicupItem != null))
                            {
                                if ((Math.Abs(MShare.g_MySelf.m_nCurrX - MShare.g_AutoPicupItem.X) > 1) || (Math.Abs(MShare.g_MySelf.m_nCurrY - MShare.g_AutoPicupItem.Y) > 1))
                                {
                                    goto AAAA;
                                }
                                else
                                {
                                    this.Dispose(T);
                                    MShare.g_APPathList.RemoveAt(0);
                                    return;
                                }
                            }
                            else
                            {
                            AAAA:
                                if ((MShare.g_APPathList.Count > 2))
                                {
                                    ndir = ClFunc.GetNextDirection(MShare.g_MySelf.m_nCurrX, MShare.g_MySelf.m_nCurrY, ((TFindNode)(MShare.g_APPathList[2])).X, ((TFindNode)(MShare.g_APPathList[2])).Y);
                                }
                                else
                                {
                                    ndir = ClFunc.GetNextDirection(MShare.g_MySelf.m_nCurrX, MShare.g_MySelf.m_nCurrY, MShare.g_nTargetX, MShare.g_nTargetY);
                                }
                                X1 = MShare.g_MySelf.m_nCurrX;
                                Y1 = MShare.g_MySelf.m_nCurrY;
                                ClFunc.GetNextRunXY(ndir, ref X1, ref Y1);
                                if (Map.CanMove(X1, Y1))
                                {
                                    if (g_PlayScene.CrashMan(X1, Y1))
                                    {
                                        MShare.g_nTargetX = T.X;
                                        MShare.g_nTargetY = T.Y;
                                        MShare.g_ChrAction = TChrAction.caWalk;
                                    }
                                    else
                                    {
                                        MShare.g_nTargetX = X1;
                                        MShare.g_nTargetY = Y1;
                                        MShare.g_ChrAction = TChrAction.caRun;
                                    }
                                }
                            }
                        }
                    }
                }
                this.Dispose(T);
                MShare.g_APPathList.RemoveAt(0);
            }
            if (MShare.g_boAPAutoMove && (MShare.g_APPathList.Count > 0))
            {
                HeroActor.Init_Queue2();
            }
        }

        private void ClientGetServerTitles(int Len, string S)
        {
            int i;
            int len2;
            TClientStdItem pClientItem;
            TClientStdItem[] aaa;
            object P;
            object p2;
            int cnt;
            for (i = 0; i < MShare.g_TitlesList.Count; i++)
            {
                this.Dispose(((TClientStdItem)(MShare.g_TitlesList[i])));
            }
            MShare.g_TitlesList.Clear();
            if (Len == 0)
            {
                return;
            }
            GetMem(P, Len);
            EDcode.DecodeBuffer2(S, (P as string), Len);
            FVCLUnZip.ZLibDecompressBuffer(P, Len, p2, len2);
            cnt = len2 / sizeof(TClientStdItem);
            aaa = new TClientStdItem[cnt];
            Move((p2 as string), aaa[0], len2);
            FreeMem(P);
            FreeMem(p2);
            for (i = aaa.GetLowerBound(0); i <= aaa.GetUpperBound(0); i++)
            {
                pClientItem = new TClientStdItem();
                pClientItem = aaa[i];
                MShare.g_TitlesList.Add(pClientItem);
                // DScreen.AddChatBoardString(pClientItem.Name, clWhite, clRed);
            }
        }

        public void InitSuiteStrs(int Len, string S)
        {
            int i;
            int len2;
            TClientSuiteItems[] aaa;
            object P;
            object p2;
            int cnt;
            TClientSuiteItems SuiteItems;
            const string fn = ".\\Data\\SuiteStrs.dat";
            for (i = 0; i < MShare.g_SuiteItemsList.Count; i++)
            {
                this.Dispose(((TClientSuiteItems)(MShare.g_SuiteItemsList[i])));
            }
            MShare.g_SuiteItemsList.Clear();
            if (Len == 0)
            {
                return;
            }
            GetMem(P, Len);
            EDcode.DecodeBuffer2(S, (P as string), Len);
            FVCLUnZip.ZLibDecompressBuffer(P, Len, p2, len2);
            FreeMem(P);
            cnt = len2 / sizeof(TClientSuiteItems);
            aaa = new TClientSuiteItems[cnt];
            Move((p2 as string), aaa[0], len2);
            FreeMem(p2);
            for (i = aaa.GetLowerBound(0); i <= aaa.GetUpperBound(0); i++)
            {
                SuiteItems = new TClientSuiteItems();
                SuiteItems = aaa[i];
                MShare.g_SuiteItemsList.Add(SuiteItems);
            }
        }

        private void ClientGetFoxState(ClientPacket msg, string Buff)
        {
            int i;
            string NameCol;
            string data;
            string Buff2;
            TCharDesc desc;
            TActor Actor;
            i = HUtil32.GetCodeMsgSize(sizeof(TCharDesc) * 4 / 3);
            if (Buff.Length > i)
            {
                Buff2 = Buff.Substring(i + 1 - 1, Buff.Length);
                data = EDcode.DeCodeString(Buff2);
                Buff2 = Buff.Substring(1 - 1, i);
                NameCol = HUtil32.GetValidStr3(data, ref data, new string[] { "/" });
            }
            else
            {
                Buff2 = Buff;
                data = "";
            }
            EDcode.DecodeBuffer(Buff2, desc, sizeof(TCharDesc));
            // x
            // y
            // dir + light
            g_PlayScene.SendMsg(Grobal2.SM_TURN, msg.Recog, msg.Param, msg.Tag, msg.Series, desc.Feature, desc.Status, "", desc.StatusEx);
            if (data != "")
            {
                Actor = g_PlayScene.FindActor(msg.Recog);
                if (Actor != null)
                {
                    Actor.m_sDescUserName = HUtil32.GetValidStr3(data, ref Actor.m_sUserName, new string[] { "\\" });
                    Actor.m_sUserNameOffSet = HGECanvas.Units.HGECanvas.g_DXCanvas.TextWidth(Actor.m_sUserName) / 2;
                    Actor.m_btNameColor = HUtil32.Str_ToInt(NameCol, 0);
                    if (Actor.m_btRace == Grobal2.RCC_MERCHANT)
                    {
                        Actor.m_nNameColor = Color.Lime;
                    }
                    else
                    {
                        Actor.m_nNameColor = GetRGB(Actor.m_btNameColor);
                    }
                    Actor.m_nTempState = HiByte(msg.Series);
                }
            }
        }

        private void ClientGetPositionMove(ClientPacket msg, string Buff)
        {
            TActor pActor;
            bool fFlay;
            TPostionMoveMessage psMessage;
            pActor = g_PlayScene.FindActor(msg.Recog);
            if (pActor != null)
            {
                EDcode.DecodeBuffer(Buff, psMessage, sizeof(psMessage));
                pActor.m_fHideMode = true;
                g_PlayScene.NewMagic(pActor, 0075, 0075, pActor.m_nCurrX, pActor.m_nCurrY, msg.Param, msg.Tag, msg.Recog, magiceff.TMagicType.mtExploBujauk, false, 60, ref fFlay);
                if (msg.Recog == MShare.g_MySelf.m_nRecogId)
                {
                    pActor.SendMsg(Grobal2.SM_TURN, msg.Param, msg.Tag, LoByte(msg.Series), psMessage.lFeature, psMessage.nStatus, psMessage.szBuff, 0, 300);
                }
                else
                {
                    pActor.SendMsg(Grobal2.SM_TURN, msg.Param, msg.Tag, LoByte(msg.Series), psMessage.lFeature, psMessage.nStatus, psMessage.szBuff, 0, 300);
                }
            }
        }

        private void ProcessActMsg(string datablock)
        {
            string data;
            string tagstr;
            int cltime;
            int svtime;
            long rtime;
            TMagicEff meff;
            if ((datablock[2] == "G") && (datablock[3] == "D") && (datablock[4] == "/"))
            {
                data = datablock.Substring(2 - 1, datablock.Length - 1);
                data = HUtil32.GetValidStr3(data, ref tagstr, new string[] { "/" });
                if (data != "")
                {
                    rtime = ((long)HUtil32.Str_ToInt(data, 0));
                    if (rtime <= 0)
                    {
                        return;
                    }
                    if (MShare.g_rtime == rtime)
                    {
                        return;
                    }
                    MShare.g_rtime = rtime;
                    ActionLock = false;
                    g_MoveBusy = false;
                    g_MoveErr = 0;
                    if (TimerAutoMove.Enabled)
                    {
                        g_MoveStep++;
                    }
                    if (MShare.g_dwFirstServerTime > 0)
                    {
                        if ((MShare.GetTickCount() - MShare.g_dwFirstClientTime) > 10 * 60 * 1000)
                        {
                            MShare.g_dwFirstServerTime = rtime;
                            MShare.g_dwFirstClientTime = MShare.GetTickCount();
                        }
                        cltime = MShare.GetTickCount() - MShare.g_dwFirstClientTime;
                        svtime = rtime - MShare.g_dwFirstServerTime;
                        // DScreen.AddChatBoardString('[速度检测] 时间差：' + IntToStr(cltime - svtime), GetRGB(219), clWhite);
                        if (cltime > svtime + 4500)
                        {
                            MShare.g_nTimeFakeDetectCount++;
                            if (MShare.g_nTimeFakeDetectCount >= 3)
                            {
                                DebugOutStr("系统不稳定或网络状态极差，游戏被中止！如有问题请联系游戏管理员！");
                                DScreen.Finalize();
                                g_PlayScene.Finalize();
                                LoginNoticeScene.Finalize();
                            }
                        }
                        else
                        {
                            if (Math.Abs(cltime - svtime) < 20)
                            {
                                MShare.g_nTimeFakeDetectCount = 0;
                            }
                            else if (Math.Abs(cltime - svtime) < 40)
                            {
                                if (MShare.g_nTimeFakeDetectCount > 1)
                                {
                                    MShare.g_nTimeFakeDetectCount -= 2;
                                }
                            }
                            else if (Math.Abs(cltime - svtime) < 80)
                            {
                                if (MShare.g_nTimeFakeDetectCount > 0)
                                {
                                    MShare.g_nTimeFakeDetectCount -= 1;
                                }
                            }
                        }
                    }
                    else
                    {
                        MShare.g_dwFirstServerTime = rtime;
                        MShare.g_dwFirstClientTime = MShare.GetTickCount();
                    }
                }
                return;
            }
            else
            {
                tagstr = datablock.Substring(2 - 1, datablock.Length - 1);
            }
            if (tagstr == "DIG")
            {
                MShare.g_MySelf.m_boDigFragment = true;
            }
            else if (tagstr == "PWR")
            {
                MShare.g_boNextTimePowerHit = true;
            }
            else if (tagstr == "LNG")
            {
                MShare.g_boCanLongHit = true;
            }
            else if (tagstr == "ULNG")
            {
                MShare.g_boCanLongHit = false;
            }
            else if (tagstr == "WID")
            {
                MShare.g_boCanWideHit = true;
            }
            else if (tagstr == "UWID")
            {
                MShare.g_boCanWideHit = false;
            }
            else if (tagstr == "STN")
            {
                MShare.g_boCanStnHit = true;
            }
            else if (tagstr == "USTN")
            {
                MShare.g_boCanStnHit = false;
            }
            else if (tagstr == "CRS")
            {
                MShare.g_boCanCrsHit = true;
                DScreen.AddChatBoardString("双龙斩开启", GetRGB(219), Color.White);
            }
            else if (tagstr == "UCRS")
            {
                MShare.g_boCanCrsHit = false;
                DScreen.AddChatBoardString("双龙斩关闭", GetRGB(219), Color.White);
            }
            else if (tagstr == "TWN")
            {
                MShare.g_boNextTimeTwinHit = true;
                MShare.g_dwLatestTwinHitTick = MShare.GetTickCount();
                DScreen.AddChatBoardString("召集雷电力量成功", GetRGB(219), Color.White);
                meff = new TCharEffect(210, 6, MShare.g_MySelf);
                meff.NextFrameTime = 80;
                meff.ImgLib = WMFile.Units.WMFile.g_WMagic2Images;
                g_PlayScene.m_EffectList.Add(meff);
            }
            else if (tagstr == "UTWN")
            {
                MShare.g_boNextTimeTwinHit = false;
                DScreen.AddChatBoardString("雷电力量消失", GetRGB(219), Color.White);
            }
            else if (tagstr == "SQU")
            {
                MShare.g_boCanSquHit = true;
                DScreen.AddChatBoardString("[龙影剑法] 开启", GetRGB(219), Color.White);
            }
            else if (tagstr == "FIR")
            {
                MShare.g_boNextTimeFireHit = true;
                MShare.g_dwLatestFireHitTick = MShare.GetTickCount();
            }
            else if (tagstr == "PUR")
            {
                MShare.g_boNextTimePursueHit = true;
                MShare.g_dwLatestPursueHitTick = MShare.GetTickCount();
            }
            else if (tagstr == "RSH")
            {
                MShare.g_boNextTimeRushHit = true;
                MShare.g_dwLatestRushHitTick = MShare.GetTickCount();
            }
            else if (tagstr == "SMI")
            {
                MShare.g_boNextTimeSmiteHit = true;
                MShare.g_dwLatestSmiteHitTick = MShare.GetTickCount();
            }
            else if (tagstr == "SMIL3")
            {
                MShare.g_boNextTimeSmiteLongHit3 = true;
                MShare.g_dwLatestSmiteLongHitTick3 = MShare.GetTickCount();
                DScreen.AddChatBoardString("[血魂一击] 已准备...", GetRGB(219), Color.White);
            }
            else if (tagstr == "SMIL")
            {
                MShare.g_boNextTimeSmiteLongHit = true;
                MShare.g_dwLatestSmiteLongHitTick = MShare.GetTickCount();
            }
            else if (tagstr == "SMIL2")
            {
                MShare.g_boNextTimeSmiteLongHit2 = true;
                MShare.g_dwLatestSmiteLongHitTick2 = MShare.GetTickCount();
                DScreen.AddChatBoardString("[断空斩] 已准备...", GetRGB(219), Color.White);
            }
            else if (tagstr == "SMIW")
            {
                MShare.g_boNextTimeSmiteWideHit = true;
                MShare.g_dwLatestSmiteWideHitTick = MShare.GetTickCount();
            }
            else if (tagstr == "SMIW2")
            {
                MShare.g_boNextTimeSmiteWideHit2 = true;
                MShare.g_dwLatestSmiteWideHitTick2 = MShare.GetTickCount();
                DScreen.AddChatBoardString("[倚天辟地] 已准备", Color.Blue, Color.White);
            }
            else if (tagstr == "MDS")
            {
                DScreen.AddChatBoardString("[美杜莎之瞳] 技能可施展", Color.Blue, Color.White);
                meff = new TCharEffect(1110, 10, MShare.g_MySelf);
                meff.NextFrameTime = 80;
                meff.ImgLib = WMFile.Units.WMFile.g_WMagic2Images;
                g_PlayScene.m_EffectList.Add(meff);
            }
            else if (tagstr == "UFIR")
            {
                MShare.g_boNextTimeFireHit = false;
            }
            else if (tagstr == "UPUR")
            {
                MShare.g_boNextTimePursueHit = false;
            }
            else if (tagstr == "USMI")
            {
                MShare.g_boNextTimeSmiteHit = false;
            }
            else if (tagstr == "URSH")
            {
                MShare.g_boNextTimeRushHit = false;
            }
            else if (tagstr == "USMIL")
            {
                MShare.g_boNextTimeSmiteLongHit = false;
            }
            else if (tagstr == "USML3")
            {
                MShare.g_boNextTimeSmiteLongHit3 = false;
            }
            else if (tagstr == "USML2")
            {
                MShare.g_boNextTimeSmiteLongHit2 = false;
                // DScreen.AddChatBoardString('[断空斩] 力量消失...', clWhite, clRed);
            }
            else if (tagstr == "USMIW")
            {
                MShare.g_boNextTimeSmiteWideHit = false;
            }
            else if (tagstr == "USMIW2")
            {
                MShare.g_boNextTimeSmiteWideHit2 = false;
            }
            else if (tagstr == "USQU")
            {
                MShare.g_boCanSquHit = false;
                DScreen.AddChatBoardString("[龙影剑法] 关闭", GetRGB(219), Color.White);
            }
            else if (tagstr == "SLON")
            {
                MShare.g_boCanSLonHit = true;
                MShare.g_dwLatestSLonHitTick = MShare.GetTickCount();
                DScreen.AddChatBoardString("[开天斩] 力量凝聚...", GetRGB(219), Color.White);
            }
            else if (tagstr == "USLON")
            {
                MShare.g_boCanSLonHit = false;
                DScreen.AddChatBoardString("[开天斩] 力量消失", Color.White, Color.Red);
            }
        }

        private void ClientGetMyTitles(int nHero, string Buff)
        {
            int i;
            string data;
            THumTitle ht;
            if (nHero != 0)
            {
                FillChar(MShare.g_hTitles, sizeof(MShare.g_hTitles), 0);
            }
            else
            {
                FillChar(MShare.g_Titles, sizeof(MShare.g_Titles), 0);
            }
            i = 0;
            while (true)
            {
                if (Buff == "")
                {
                    break;
                }
                Buff = HUtil32.GetValidStr3(Buff, ref data, new string[] { "/" });
                if (data != "")
                {
                    EDcode.DecodeBuffer(data, ht, sizeof(THumTitle));
                    if (nHero != 0)
                    {
                        MShare.g_hTitles[i] = ht;
                    }
                    else
                    {
                        MShare.g_Titles[i] = ht;
                    }
                    i++;
                    if (i > Grobal2.THumTitle.GetUpperBound(0))
                    {
                        break;
                    }
                }
                else
                {
                    break;
                }
            }
        }

        public void TimerRunTimer(System.Object Sender, System.EventArgs _e1)
        {
            AppOnIdle();
        }


        // 鼠标样子等素材
        public static void DebugOutStr(string msg)
        {
            Console.WriteLine(msg);
        }

        public static void WaitAndPass(long msec)
        {
            long Start = MShare.GetTickCount();
            while (MShare.GetTickCount() - Start < msec)
            {
                // Application.ProcessMessages();
            }
        }

        public static int GetRGB(byte c256)
        {
            return 0;
        }

        public static void GetNearPoint()
        {
            int i;
            int nC;
            int n10;
            int n14;
            if ((MShare.g_APMapPath != null) && (MShare.g_APMapPath.GetUpperBound(0) > 0))
            {
                n14 = 0;
                MShare.g_APLastPoint.X = -1;
                n10 = 999;
                for (i = MShare.g_APMapPath.GetLowerBound(0); i <= MShare.g_APMapPath.GetUpperBound(0); i++)
                {
                    nC = Math.Abs(MShare.g_APMapPath[i].X - MShare.g_MySelf.m_nCurrX) + Math.Abs(MShare.g_APMapPath[i].X - MShare.g_MySelf.m_nCurrY);
                    if (nC < n10)
                    {
                        n10 = nC;
                        n14 = i;
                    }
                }
                MShare.g_APStep = n14;
            }
        }

        public static bool GetNextPosition(int sx, int sy, int ndir, int nFlag, ref int snx, ref int sny)
        {
            bool result;
            snx = sx;
            sny = sy;
            switch (ndir)
            {
                case Grobal2.DR_UP:
                    if (sny > nFlag - 1)
                    {
                        sny -= nFlag;
                    }
                    break;
                case Grobal2.DR_DOWN:
                    if (sny < (Map.m_MapHeader.wHeight - nFlag))
                    {
                        sny += nFlag;
                    }
                    break;
                case Grobal2.DR_LEFT:
                    if (snx > nFlag - 1)
                    {
                        snx -= nFlag;
                    }
                    break;
                case Grobal2.DR_RIGHT:
                    if (snx < (Map.m_MapHeader.wWidth - nFlag))
                    {
                        snx += nFlag;
                    }
                    break;
                case Grobal2.DR_UPLEFT:
                    if ((snx > nFlag - 1) && (sny > nFlag - 1))
                    {
                        snx -= nFlag;
                        sny -= nFlag;
                    }
                    break;
                case Grobal2.DR_UPRIGHT:
                    if ((snx > nFlag - 1) && (sny < (Map.m_MapHeader.wHeight - nFlag)))
                    {
                        snx += nFlag;
                        sny -= nFlag;
                    }
                    break;
                case Grobal2.DR_DOWNLEFT:
                    if ((snx < (Map.m_MapHeader.wWidth - nFlag)) && (sny > nFlag - 1))
                    {
                        snx -= nFlag;
                        sny += nFlag;
                    }
                    break;
                case Grobal2.DR_DOWNRIGHT:
                    if ((snx < (Map.m_MapHeader.wWidth - nFlag)) && (sny < (Map.m_MapHeader.wHeight - nFlag)))
                    {
                        snx += nFlag;
                        sny += nFlag;
                    }
                    break;
            }
            if ((snx == sx) && (sny == sy))
            {
                result = false;
            }
            else
            {
                result = true;
            }
            return result;
        }

        public static int CheckMagPassThrough(int sx, int sy, int tx, int ty, int ndir)
        {
            int result;
            int i;
            int tCount;
            TActor Actor;
            tCount = 0;
            for (i = 0; i <= 12; i++)
            {
                Actor = g_PlayScene.FindActorXY(sx, sy);
                if (Actor != null)
                {
                    if (HeroActor.IsProperTarget(Actor))
                    {
                        tCount++;
                    }
                }
                if (!((Math.Abs(sx - tx) <= 0) && (Math.Abs(sy - ty) <= 0)))
                {
                    ndir = ClFunc.GetNextDirection(sx, sy, tx, ty);
                    if (!GetNextPosition(sx, sy, ndir, 1, ref sx, ref sy))
                    {
                        break;
                    }
                }
                else
                {
                    break;
                }
            }
            result = tCount;
            return result;
        }

        public static bool GetAdvPosition(TActor TargetCret, ref int nX, ref int nY)
        {
            bool result;
            byte btDir;
            // boTagWarr: Boolean;
            result = false;
            // boTagWarr := (m_btJob <> 0) and (m_TargetCret.m_btRaceServer in [RC_PLAYOBJECT, RC_HERO]) and (m_TargetCret.m_btJob = 0);
            // if not boTagWarr then begin
            // GetBackPositionEx(nX, nY);
            // Exit;
            // end;
            nX = MShare.g_MySelf.m_nCurrX;
            nY = MShare.g_MySelf.m_nCurrY;
            btDir = ClFunc.GetNextDirection(MShare.g_MySelf.m_nCurrX, MShare.g_MySelf.m_nCurrY, TargetCret.m_nCurrX, TargetCret.m_nCurrY);
            Randomize;
            THumActor _wvar1 = MShare.g_MySelf;
            switch (btDir)
            {
                case Grobal2.DR_UP:
                    if ((new System.Random(2)).Next() == 0)
                    {
                        nY += 2;
                        // if not g_PlayScene.CanWalk(nX, nY) then Dec(nY);
                        nX += 2;
                        // if not g_PlayScene.CanWalk(nX, nY) then Dec(nX);
                    }
                    else
                    {
                        nY += 2;
                        // if not g_PlayScene.CanWalk(nX, nY) then Dec(nY);
                        nX -= 2;
                        // if not g_PlayScene.CanWalk(nX, nY) then Inc(nX);
                    }
                    if (!g_PlayScene.CanWalk(nX, nY))
                    {
                        nY = _wvar1.m_nCurrY + 2;
                    }
                    break;
                case Grobal2.DR_DOWN:
                    if ((new System.Random(2)).Next() == 0)
                    {
                        nY -= 2;
                        // if not g_PlayScene.CanWalk(nX, nY) then Inc(nY);
                        nX += 2;
                        // if not g_PlayScene.CanWalk(nX, nY) then Dec(nX);
                    }
                    else
                    {
                        nY -= 2;
                        // if not g_PlayScene.CanWalk(nX, nY) then Inc(nY);
                        nX -= 2;
                        // if not g_PlayScene.CanWalk(nX, nY) then Inc(nX);
                    }
                    if (!g_PlayScene.CanWalk(nX, nY))
                    {
                        nY = _wvar1.m_nCurrY - 2;
                    }
                    break;
                case Grobal2.DR_LEFT:
                    if ((new System.Random(2)).Next() == 0)
                    {
                        nX += 2;
                        // if not g_PlayScene.CanWalk(nX, nY) then Dec(nX);
                        nY += 2;
                        // if not g_PlayScene.CanWalk(nX, nY) then Dec(nY);
                    }
                    else
                    {
                        nX += 2;
                        // if not g_PlayScene.CanWalk(nX, nY) then Dec(nX);
                        nY -= 2;
                        // if not g_PlayScene.CanWalk(nX, nY) then Inc(nY);
                    }
                    if (!g_PlayScene.CanWalk(nX, nY))
                    {
                        nX = _wvar1.m_nCurrX + 2;
                    }
                    break;
                case Grobal2.DR_RIGHT:
                    if ((new System.Random(2)).Next() == 0)
                    {
                        nX -= 2;
                        // if not g_PlayScene.CanWalk(nX, nY) then Inc(nX);
                        nY += 2;
                        // if not g_PlayScene.CanWalk(nX, nY) then Dec(nY);
                    }
                    else
                    {
                        nX -= 2;
                        // if not g_PlayScene.CanWalk(nX, nY) then Inc(nX);
                        nY -= 2;
                        // if not g_PlayScene.CanWalk(nX, nY) then Inc(nY);
                    }
                    if (!g_PlayScene.CanWalk(nX, nY))
                    {
                        nX = _wvar1.m_nCurrX - 2;
                    }
                    break;
                case Grobal2.DR_UPLEFT:
                    if ((new System.Random(2)).Next() == 0)
                    {
                        nX += 2;
                        // if not g_PlayScene.CanWalk(nX, nY) then Dec(nX);
                    }
                    else
                    {
                        nY += 2;
                        // if not g_PlayScene.CanWalk(nX, nY) then
                        nY -= 1;
                    }
                    if (!g_PlayScene.CanWalk(nX, nY))
                    {
                        nX = _wvar1.m_nCurrX + 2;
                        nY = _wvar1.m_nCurrY + 2;
                    }
                    break;
                case Grobal2.DR_UPRIGHT:
                    if ((new System.Random(2)).Next() == 0)
                    {
                        nY += 2;
                        // if not g_PlayScene.CanWalk(nX, nY) then
                        nY -= 1;
                    }
                    else
                    {
                        nX -= 2;
                        // if not g_PlayScene.CanWalk(nX, nY) then
                        nX++;
                    }
                    if (!g_PlayScene.CanWalk(nX, nY))
                    {
                        nX = _wvar1.m_nCurrX - 2;
                        nY = _wvar1.m_nCurrY + 2;
                    }
                    break;
                case Grobal2.DR_DOWNLEFT:
                    if ((new System.Random(2)).Next() == 0)
                    {
                        nX += 2;
                        // if not g_PlayScene.CanWalk(nX, nY) then Dec(nX);
                    }
                    else
                    {
                        nY -= 2;
                        // if not g_PlayScene.CanWalk(nX, nY) then Inc(nY);
                    }
                    if (!g_PlayScene.CanWalk(nX, nY))
                    {
                        nX = _wvar1.m_nCurrX + 2;
                        nY = _wvar1.m_nCurrY - 2;
                    }
                    break;
                case Grobal2.DR_DOWNRIGHT:
                    if ((new System.Random(2)).Next() == 0)
                    {
                        nX -= 2;
                        // if not g_PlayScene.CanWalk(nX, nY) then Inc(nX);
                    }
                    else
                    {
                        nY -= 2;
                        // if not g_PlayScene.CanWalk(nX, nY) then Inc(nY);
                    }
                    if (!g_PlayScene.CanWalk(nX, nY))
                    {
                        nX = _wvar1.m_nCurrX - 2;
                        nY = _wvar1.m_nCurrY - 2;
                    }
                    break;
            }
            return result;
        }

        public static void SaveWayPoint()
        {
            int i;
            string S;
            FileStream ini;
            if (MShare.g_APMapPath != null)
            {
                try
                {
                    ini = new FileStream(".\\Config\\" + MShare.g_sServerName + "." + MShare.g_MySelf.m_sUserName + ".WayPoint.txt");
                    S = "";
                    for (i = MShare.g_APMapPath.GetLowerBound(0); i <= MShare.g_APMapPath.GetUpperBound(0); i++)
                    {
                        S = S + Format("%d,%d ", new int[] { MShare.g_APMapPath[i].X, MShare.g_APMapPath[i].X });
                    }
                    ini.WriteString(MShare.g_sMapTitle, "WayPoint", S);
                    ini.Free;
                }
                catch
                {
                }
            }
            else
            {
                if (MShare.g_MySelf != null)
                {
                    try
                    {
                        if (!Directory.Exists(".\\Config\\"))
                        {
                            Directory.CreateDirectory(".\\Config\\");
                        }
                        ini = new FileStream(".\\Config\\" + MShare.g_sServerName + "." + MShare.g_MySelf.m_sUserName + ".WayPoint.txt");
                        ini.WriteString(MShare.g_sMapTitle, "WayPoint", "");
                        ini.Free;
                    }
                    catch
                    {
                    }
                }
            }
        }

        public static void LoadWayPoint()
        {
            string X;
            string Y;
            string S;
            string ss;
            FileStream ini;
            MShare.g_APMapPath = null;
            ini = new FileStream(".\\Config\\" + MShare.g_sServerName + "." + MShare.g_MySelf.m_sUserName + ".WayPoint.txt");
            S = ini.ReadString(MShare.g_sMapTitle, "WayPoint", "");
            while (true)
            {
                if (S == "")
                {
                    break;
                }
                S = HUtil32.GetValidStr3(S, ref ss, new string[] { " " });
                if (ss != "")
                {
                    Y = HUtil32.GetValidStr3(ss, ref X, new string[] { "," });
                    if (MShare.g_APMapPath == null)
                    {
                        MShare.g_APMapPath = new Point[1];
                        MShare.g_APMapPath[0].X = Convert.ToInt32(X);
                        MShare.g_APMapPath[0].X = Convert.ToInt32(Y);
                    }
                    else
                    {
                        MShare.g_APMapPath = new Point[MShare.g_APMapPath.GetUpperBound(0) + 2];
                        MShare.g_APMapPath[MShare.g_APMapPath.GetUpperBound(0)].X = Convert.ToInt32(X);
                        MShare.g_APMapPath[MShare.g_APMapPath.GetUpperBound(0)].X = Convert.ToInt32(Y);
                    }
                }
            }
            ini.Free;
        }

        public static int NotifyCallback(int NotifyType, int NotifyData, object pCallbackContext)
        {
            int result;
            DebugOutStr(Format("(Notify) Type: %d, Data: %.8x", new double[] { NotifyType, (double)NotifyData }));
            result = 0;
            return result;
        }

        public static int GetMagicLv(TActor Actor, int magid)
        {
            int result;
            int i;
            result = 0;
            if ((Actor == null))
            {
                return result;
            }
            if ((magid <= 0) || (magid >= 255))
            {
                return result;
            }
            if (Actor.m_btIsHero == 1)
            {
                for (i = MShare.g_HeroMagicList.Count - 1; i >= 0; i--)
                {
                    if (((TClientMagic)(MShare.g_HeroMagicList[i])).Def.wMagicID == magid)
                    {
                        result = ((TClientMagic)(MShare.g_HeroMagicList[i])).Level;
                        break;
                    }
                }
            }
            else
            {
                if (MShare.g_MagicArr[0][magid] != null)
                {
                    result = MShare.g_MagicArr[0][magid].level;
                }
            }
            return result;
        }
    }

    public enum TOneClickMode
    {
        toNone,
        toKornetWorld
    }
}

