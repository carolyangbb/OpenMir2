using System;
using System.Collections;
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
                M2Share.fuLock.Enter();
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
                M2Share.fuLock.Leave();
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
            pu.UserGateIndex = (short)userremotegateindex;
            pu.GateIndex = gateindex;
            pu.ReadyStartTime = HUtil32.GetTickCount();
            pu.Closed = false;
            try
            {
                M2Share.fuLock.Enter();
                ReadyUsers.Add(pu);
            }
            finally
            {
                M2Share.fuLock.Leave();
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
                M2Share.fuLock.Enter();
                ChangeUsers.Add(pc);
            }
            finally
            {
                M2Share.fuLock.Leave();
            }
        }

        public bool IsFinished()
        {
            bool result;
            result = false;
            try
            {
                M2Share.fuLock.Enter();
                if (SavePlayers.Count == 0)
                {
                    result = true;
                }
            }
            finally
            {
                M2Share.fuLock.Leave();
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
                M2Share.fuLock.Enter();
                if (SavePlayers.Count >= 1000)
                {
                    result = true;
                }
            }
            finally
            {
                M2Share.fuLock.Leave();
            }
            return result;
        }

        public int AddSavePlayer(TSaveRcd psr)
        {
            int result = -1;
            try
            {
                M2Share.fuLock.Enter();
                SavePlayers.Add(psr);
                result = SavePlayers.Count;
            }
            finally
            {
                M2Share.fuLock.Leave();
            }
            return result;
        }

        public void AddDBData(string Data)
        {
            try
            {
                M2Share.fuLock.Enter();
                fDBDatas.Add(Data);
            }
            finally
            {
                M2Share.fuLock.Leave();
            }
        }

        public bool IsDoingSave(string chrname)
        {
            bool result;
            int i;
            result = false;
            try
            {
                M2Share.fuLock.Enter();
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
                M2Share.fuLock.Leave();
            }
            return result;
        }

        private bool OpenUserCharactor(TReadyUserInfo pu)
        {
            TUserOpenInfo pui;
            FDBRecord rcd = null;
            if (!RunDB.LoadHumanCharacter(pu.UserId, pu.UserName, pu.UserAddress, pu.Certification, ref rcd))
            {
                M2Share.fuOpenLock.Enter();
                try
                {
                    M2Share.RunSocket.SendForcedClose(pu.GateIndex, pu.Shandle);
                }
                finally
                {
                    M2Share.fuOpenLock.Leave();
                }
                return false;
            }
            pui = new TUserOpenInfo();
            pui.Name = pu.UserName;
            pui.readyinfo = pu;
            pui.rcd = rcd;
            M2Share.UserEngine.AddNewUser(pui);
            return true;
        }

        private bool OpenChangeSaveUserInfo(TChangeUserInfo pc)
        {
            FDBRecord rcd = null;
            bool result = false;
            if (RunDB.LoadHumanCharacter("1", pc.UserName, "1", 1, ref rcd))
            {
                if ((rcd.Block.DBHuman.Gold + pc.ChangeGold > 0) && (rcd.Block.DBHuman.Gold + pc.ChangeGold < ObjBase.MAXGOLD))
                {
                    rcd.Block.DBHuman.Gold = rcd.Block.DBHuman.Gold + pc.ChangeGold;
                    if (RunDB.SaveHumanCharacter("1", pc.UserName, 1, rcd))
                    {
                        M2Share.UserEngine.ChangeAndSaveOk(pc);
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
            totaltime = HUtil32.GetTickCount();
            loadlist = null;
            savelist = null;
            chglist = null;
            datalist = null;
            try
            {
                M2Share.fuLock.Enter();
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
                M2Share.fuLock.Leave();
            }
            if (savelist != null)
            {
                // n := 0;
                listtime = HUtil32.GetTickCount();
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
                                M2Share.MainOutMessage("[Warning] SavePlayers was deleted because of timeover... " + p.uname);
                            }
                            try
                            {
                                M2Share.fuLock.Enter();
                                try
                                {
                                    if (p.hum != null)
                                    {
                                        p.hum.BoSaveOk = true;
                                    }
                                }
                                catch
                                {
                                    M2Share.MainOutMessage("NOT BoSaveOK ... ");
                                }
                                for (k = 0; k < SavePlayers.Count; k++)
                                {
                                    //if (SavePlayers[k] == p)
                                    //{
                                    //    SavePlayers.RemoveAt(k);
                                    //    Dispose(p);
                                    //    break;
                                    //}
                                }
                            }
                            finally
                            {
                                M2Share.fuLock.Leave();
                            }
                        }
                        else
                        {
                            p.savetime = HUtil32.GetTickCount();
                            p.savefail++;
                        }
                    }
                }
                savelist.Free();
            }
            if (loadlist != null)
            {
                listtime = HUtil32.GetTickCount();
                listcount = loadlist.Count;
                for (i = 0; i < loadlist.Count; i++)
                {
                    // load human here...
                    // <rundb>
                    FrnEngn.g_DbUse = true;
                    pu = (TReadyUserInfo)loadlist[i];
                    if (!OpenUserCharactor(pu))
                    {
                        M2Share.fuCloseLock.Enter();
                        try
                        {
                            M2Share.RunSocket.CloseUser(pu.GateIndex, pu.Shandle);
                        }
                        finally
                        {
                            M2Share.fuCloseLock.Leave();
                        }
                    }
                    Dispose(loadlist[i]);
                }
                FrnEngn.g_DbUse = false;
                loadlist.Free();
            }
            if (chglist != null)
            {
                listtime = HUtil32.GetTickCount();
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
                listtime = HUtil32.GetTickCount();
                listcount = datalist.Count;
                for (i = 0; i < datalist.Count; i++)
                {
                    M2Share.fuLock.Enter();
                    try
                    {
                        RunDB.SendNonBlockDatas((string)datalist[i]);
                    }
                    finally
                    {
                        M2Share.fuLock.Leave();
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
                    M2Share.MirDayTime = 2;
                    break;
                case 4:
                case 15:
                    // 历翅
                    M2Share.MirDayTime = 0;
                    break;
                // 货寒
                // Modify the A .. B: 0 .. 3, 12 .. 14
                case 0:
                case 12:
                    M2Share.MirDayTime = 3;
                    break;
                default:
                    // 广
                    M2Share.MirDayTime = 1;
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
                    M2Share.MainOutMessage("[FrnEngn] raise exception1..");
                }
                try
                {
                    ProcessEtc();
                }
                catch
                {
                    M2Share.MainOutMessage("[FrnEngn] raise exception2..");
                }
                //this.Sleep(1);
                //if (this.Terminated)
                //{
                //    return;
                //}
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

