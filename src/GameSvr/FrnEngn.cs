using System;
using System.Collections;
using System.Threading;
using SystemModule;

namespace GameSvr
{
    public class TFrontEngine
    {
        private readonly ArrayList ReadyUsers = null;
        private readonly ArrayList SavePlayers = null;
        private readonly ArrayList ChangeUsers = null;
        private readonly ArrayList fDBDatas = null;

        public TFrontEngine()
        {
            ReadyUsers = new ArrayList();
            SavePlayers = new ArrayList();
            ChangeUsers = new ArrayList();
            fDBDatas = new ArrayList();
        }

        public void UserSocketHasClosed(int gateindex, int uhandle)
        {
            TReadyUserInfo pu;
            try
            {
                svMain.fuLock.Enter();
                for (var i = 0; i < ReadyUsers.Count; i++)
                {
                    pu = (TReadyUserInfo)ReadyUsers[i];
                    if ((pu.GateIndex == gateindex) && (pu.Shandle == uhandle))
                    {
                        Dispose(pu);
                        ReadyUsers.RemoveAt(i);
                        break;
                    }
                }
            }
            finally
            {
                svMain.fuLock.Leave();
            }
        }

        public void LoadPlayer(string uid, string chrname, string uaddr, bool startnew, int certify, int certmode, int availmode, int clversion, int loginclversion, int clientchecksum, int shandle, int userremotegateindex, int gateindex)
        {
            TReadyUserInfo pu;
            pu = new TReadyUserInfo();
            pu.UserId = uid;
            pu.UserName = chrname;
            pu.UserAddress = uaddr;
            pu.StartNew = startnew;
            pu.Certification = certify;
            pu.ClientVersion = clversion;
            pu.LoginClientVersion = loginclversion;
            pu.ClientCheckSum = clientchecksum;
            pu.ApprovalMode = certmode;
            pu.AvailableMode = availmode;
            pu.Shandle = shandle;
            pu.UserGateIndex = (ushort)userremotegateindex;
            pu.GateIndex = gateindex;
            pu.ReadyStartTime  =  HUtil32.GetTickCount();
            pu.Closed = false;
            try
            {
                svMain.fuLock.Enter();
                ReadyUsers.Add(pu);
            }
            finally
            {
                svMain.fuLock.Leave();
            }
        }

        public void ChangeUserInfos(string cmdr, string chrname, int chggold)
        {
            TChangeUserInfo pc;
            pc = new TChangeUserInfo();
            pc.CommandWho = cmdr;
            pc.UserName = chrname;
            pc.ChangeGold = chggold;
            try
            {
                svMain.fuLock.Enter();
                ChangeUsers.Add(pc);
            }
            finally
            {
                svMain.fuLock.Leave();
            }
        }

        public bool IsFinished()
        {
            bool result;
            result = false;
            try
            {
                svMain.fuLock.Enter();
                if (SavePlayers.Count == 0)
                {
                    result = true;
                }
            }
            finally
            {
                svMain.fuLock.Leave();
            }
            return result;
        }

        // 葛电 老阑 辆丰 沁绰啊.
        public bool HasServerHeavyLoad()
        {
            bool result;
            result = false;
            try
            {
                svMain.fuLock.Enter();
                if (SavePlayers.Count >= 1000)
                {
                    result = true;
                }
            }
            finally
            {
                svMain.fuLock.Leave();
            }
            return result;
        }

        public int AddSavePlayer(TSaveRcd psr)
        {
            int result = -1;
            try
            {
                svMain.fuLock.Enter();
                SavePlayers.Add(psr);
                result = SavePlayers.Count;
            }
            finally
            {
                svMain.fuLock.Leave();
            }
            return result;
        }

        public void AddDBData(string Data)
        {
            try
            {
                svMain.fuLock.Enter();
                fDBDatas.Add(Data);
            }
            finally
            {
                svMain.fuLock.Leave();
            }
        }

        public bool IsDoingSave(string chrname)
        {
            bool result;
            int i;
            result = false;
            try
            {
                svMain.fuLock.Enter();
                for (i = 0; i < SavePlayers.Count; i++)
                {
                    if (((TSaveRcd)SavePlayers[i]).uname == chrname)
                    {
                        result = true;
                        break;
                    }
                }
            }
            finally
            {
                svMain.fuLock.Leave();
            }
            return result;
        }

        private bool OpenUserCharactor(TReadyUserInfo pu)
        {
            bool result;
            TUserOpenInfo pui;
            FDBRecord rcd;
            result = false;
            if (!RunDB.LoadHumanCharacter(pu.UserId, pu.UserName, pu.UserAddress, pu.Certification, ref rcd))
            {
                // 努扼捞攫飘俊 俊矾 皋技瘤甫 焊辰促.
                svMain.fuOpenLock.Enter();
                try
                {
                    svMain.RunSocket.SendForcedClose(pu.GateIndex, pu.Shandle);
                }
                finally
                {
                    svMain.fuOpenLock.Leave();
                }
                return result;
            }
            pui = new TUserOpenInfo();
            pui.Name = pu.UserName;
            pui.readyinfo = pu;
            pui.rcd = rcd;
            svMain.UserEngine.AddNewUser(pui);
            result = true;
            return result;
        }

        private bool OpenChangeSaveUserInfo(TChangeUserInfo pc)
        {
            bool result;
            FDBRecord rcd;
            result = false;
            if (RunDB.LoadHumanCharacter("1", pc.UserName, "1", 1, ref rcd))
            {
                if ((rcd.Block.DBHuman.Gold + pc.ChangeGold > 0) && (rcd.Block.DBHuman.Gold + pc.ChangeGold < ObjBase.MAXGOLD))
                {
                    rcd.Block.DBHuman.Gold = rcd.Block.DBHuman.Gold + pc.ChangeGold;
                    if (RunDB.SaveHumanCharacter("1", pc.UserName, 1, rcd))
                    {
                        svMain.UserEngine.ChangeAndSaveOk(pc);
                        // 己傍沁澜阑 舅赴促.
                        result = true;
                    }
                }
            }
            return result;
        }

        protected void ProcessReadyPlayers()
        {
            int i;
            int k;
            TReadyUserInfo pu;
            ArrayList loadlist;
            ArrayList savelist;
            ArrayList chglist;
            ArrayList datalist;
            TSaveRcd p;
            TChangeUserInfo pc;
            long listtime;
            int listcount;
            long totaltime;
            totaltime  =  HUtil32.GetTickCount();
            loadlist = null;
            savelist = null;
            chglist = null;
            datalist = null;
            try
            {
                svMain.fuLock.Enter();
                if (SavePlayers.Count > 0)
                {
                    savelist = new ArrayList();
                    for (i = 0; i < SavePlayers.Count; i++)
                    {
                        // thread 面倒阑 乔窍扁 困秦辑 汗荤夯阑 父电促.
                        savelist.Add(SavePlayers[i]);
                    }
                }
                if (ReadyUsers.Count > 0)
                {
                    loadlist = new ArrayList();
                    for (i = 0; i < ReadyUsers.Count; i++)
                    {
                        pu = (TReadyUserInfo)ReadyUsers[i];
                        loadlist.Add(pu);
                    }
                    ReadyUsers.Clear();
                }
                if (ChangeUsers.Count > 0)
                {
                    chglist = new ArrayList();
                    for (i = 0; i < ChangeUsers.Count; i++)
                    {
                        pc = (TChangeUserInfo)ChangeUsers[i];
                        chglist.Add(pc);
                    }
                    ChangeUsers.Clear();
                }
                if (fDBDatas.Count > 0)
                {
                    datalist = new ArrayList();
                    for (i = 0; i < fDBDatas.Count; i++)
                    {
                        datalist.Add(fDBDatas[i]);
                    }
                    fDBDatas.Clear();
                }
            }
            finally
            {
                svMain.fuLock.Leave();
            }
            if (savelist != null)
            {
                // n := 0;
                listtime  =  HUtil32.GetTickCount();
                listcount = savelist.Count;
                for (i = 0; i < savelist.Count; i++)
                {
                    p = (TSaveRcd)savelist[i];
                    // 历厘 角菩 饶 0.5檬俊 茄锅究父 历厘 夸没
                    if (HUtil32.GetTickCount() - p.savetime > 500)
                    {
                        if (RunDB.SaveHumanCharacter(p.uid, p.uname, p.certify, p.rcd) || (p.savefail > 20))
                        {
                            if (p.savefail > 20)
                            {
                                svMain.MainOutMessage("[Warning] SavePlayers was deleted because of timeover... " + p.uname);
                            }
                            try
                            {
                                svMain.fuLock.Enter();
                                try
                                {
                                    if (p.hum != null)
                                    {
                                        p.hum.BoSaveOk = true;
                                    }
                                    // 静饭靛 林狼
                                }
                                catch
                                {
                                    svMain.MainOutMessage("NOT BoSaveOK ... ");
                                }
                                for (k = 0; k < SavePlayers.Count; k++)
                                {
                                    // 历厘 己傍茄 巴父 瘤款促.
                                    if (SavePlayers[k] == p)
                                    {
                                        SavePlayers.RemoveAt(k);
                                        Dispose(p);
                                        break;
                                    }
                                }
                            }
                            finally
                            {
                                svMain.fuLock.Leave();
                            }
                        }
                        else
                        {
                            p.savetime  =  HUtil32.GetTickCount();
                            p.savefail++;
                        }
                    }
                }
                savelist.Free();
            }
            if (loadlist != null)
            {
                listtime  =  HUtil32.GetTickCount();
                listcount = loadlist.Count;
                for (i = 0; i < loadlist.Count; i++)
                {
                    // load human here...
                    // <rundb>
                    FrnEngn.g_DbUse = true;
                    pu = (TReadyUserInfo)loadlist[i];
                    if (!OpenUserCharactor(pu))
                    {
                        svMain.fuCloseLock.Enter();
                        try
                        {
                            svMain.RunSocket.CloseUser(pu.GateIndex, pu.Shandle);
                        }
                        finally
                        {
                            svMain.fuCloseLock.Leave();
                        }
                    }
                    Dispose(loadlist[i]);
                }
                FrnEngn.g_DbUse = false;
                loadlist.Free();
            }
            if (chglist != null)
            {
                listtime  =  HUtil32.GetTickCount();
                listcount = chglist.Count;
                for (i = 0; i < chglist.Count; i++)
                {
                    pc = (TChangeUserInfo)chglist[i];
                    OpenChangeSaveUserInfo(pc);
                    Dispose(pc);
                }
                chglist.Free();
            }
            if (datalist != null)
            {
                listtime  =  HUtil32.GetTickCount();
                listcount = datalist.Count;
                for (i = 0; i < datalist.Count; i++)
                {
                    svMain.fuLock.Enter();
                    try
                    {
                        RunDB.SendNonBlockDatas((string)datalist[i]);
                    }
                    finally
                    {
                        svMain.fuLock.Leave();
                    }
                }
                datalist.Clear();
                datalist.Free();
            }
        }

        protected void ProcessEtc()
        {
            short ahour;
            short amin;
            short asec;
            short amsec;
            ahour = (short)DateTime.Now.Hour;
            amin = (short)DateTime.Now.Minute;
            asec = (short)DateTime.Now.Second;
            amsec = (short)DateTime.Now.Millisecond;
            switch (ahour)
            {
                case 23:
                case 11:
                    // 0: 货寒, 1: 撤, 2: 历翅 3: 广
                    svMain.MirDayTime = 2;
                    break;
                case 4:
                case 15:
                    // 历翅
                    svMain.MirDayTime = 0;
                    break;
                // 货寒
                // Modify the A .. B: 0 .. 3, 12 .. 14
                case 0:
                case 12:
                    svMain.MirDayTime = 3;
                    break;
                default:
                    // 广
                    svMain.MirDayTime = 1;
                    break;
                    // 撤
            }
        }

        public void Execute()
        {
            while (true)
            {
                try
                {
                    ProcessReadyPlayers();
                }
                catch
                {
                    svMain.MainOutMessage("[FrnEngn] raise exception1..");
                }
                try
                {
                    ProcessEtc();
                }
                catch
                {
                    svMain.MainOutMessage("[FrnEngn] raise exception2..");
                }
                this.Sleep(1);
                if (this.Terminated)
                {
                    return;
                }
            }
        }

        public void Dispose(object obj)
        {
            if (obj != null)
            {
                obj = null;
            }
        }
    }
}

namespace GameSvr
{
    public class FrnEngn
    {
        public static bool g_DbUse = false;
    }
}

