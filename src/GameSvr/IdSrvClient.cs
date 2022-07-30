using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using SystemModule;

namespace GameSvr
{
    public class TFrmIDSoc
    {
        private IList<TAdmission> AdmissionList = null;
        private ArrayList ShareIPList = null;
        private long LoginServerCheckTime = 0;
        public string ServerAddress = String.Empty;
        public int ServerPort = 0;
        public string IDSocStr = String.Empty;

        public TFrmIDSoc()
        {

        }

        public void FormCreate(System.Object Sender, System.EventArgs _e1)
        {
            FileStream ini;
            string fname = ".\\!setup.txt";
            IDSocket.Address = "";
            //if (File.Exists(fname))
            //{
            //    ini = new FileStream(fname);
            //    if (ini != null)
            //    {
            //        ServerAddress = ini.ReadString("Server", "IDSAddr", "210.121.143.204");
            //        ServerPort = ini.ReadInteger("Server", "IDSPort", 5600);
            //    }
            //    ini.Free();
            //}
            //else
            //{
            //    MessageBox.Show(fname + " not found");
            //}
            AdmissionList = new List<TAdmission>();
            ShareIPList = new ArrayList();
            LoadShareIPList();
            LoginServerCheckTime = GetTickCount;
        }

        public void FormDestroy(Object Sender)
        {
           
        }

        public void LoadShareIPList()
        {

        }

        public void Timer1Timer(System.Object Sender, System.EventArgs _e1)
        {
            //if (IDSocket.Address != "")
            //{
            //    if (!IDSocket.Active)
            //    {
            //        IDSocket.Active = true;
            //    }
            //}
        }

        public void IDSocketConnect(Object Sender, Socket Socket)
        {
        }

        public void IDSocketDisconnect(Object Sender, Socket Socket)
        {
        }

        public void IDSocketError(Object Sender, Socket Socket, TErrorEvent ErrorEvent, ref int ErrorCode)
        {
            ErrorCode = 0;
            Socket.Close;
        }

        public void IDSocketRead(Object Sender, Socket Socket)
        {
            try
            {
                svMain.csShare.Enter();
                IDSocStr = IDSocStr + Socket.ReceiveText;
            }
            finally
            {
                svMain.csShare.Leave();
            }
        }

        public void Initialize()
        {
            this.Active = false;
            IDSocket.Address = ServerAddress;
            IDSocket.Port = ServerPort;
            this.Active = true;
        }

        // 皋牢 胶贰靛俊辑父 荤侩秦具 窃.
        public void SendUserClose(string uid, int cert)
        {
            if (IDSocket.Socket.Connected)
            {
                IDSocket.Socket.SendText("(" + Grobal2.ISM_USERCLOSED.ToString() + "/" + uid + "/" + cert.ToString() + ")");
            }
        }

        public void SendGameTimeOfTimeCardUser(string uid, int gametime_min)
        {

        }

        public void SendUserShiftToVentureServer(string svname, string uid, int cert)
        {
            if (IDSocket.Socket.Connected)
            {
                IDSocket.Socket.SendText("(" + Grobal2.ISM_SHIFTVENTURESERVER.ToString() + "/" + svname + "/" + uid + "/" + cert.ToString() + ")");
            }
        }

        public void SendUserCount(int ucount)
        {
            if (IDSocket.Socket.Connected)
            {
                IDSocket.Socket.SendText("(" + Grobal2.ISM_USERCOUNT.ToString() + "/" + svMain.ServerName + "/" + svMain.ServerIndex.ToString() + "/" + ucount.ToString() + ")");
            }
        }

        public void DecodeSocStr()
        {
            string BufStr = String.Empty;
            string str = String.Empty;
            string head = String.Empty;
            string body = String.Empty;
            int ident;
            try
            {
                svMain.csShare.Enter();
                if (IDSocStr.IndexOf(")") <= 0)
                {
                    return;
                }
                BufStr = IDSocStr;
                IDSocStr = "";
            }
            finally
            {
                svMain.csShare.Leave();
            }
            try
            {
                while (BufStr.IndexOf(")") > 0)
                {
                    BufStr = HUtil32.ArrestStringEx(BufStr, "(", ")", ref str);
                    if (str != "")
                    {
                        body = HUtil32.GetValidStr3(str, ref head, new string[] { "/" });
                        ident = HUtil32.Str_ToInt(head, 0);
                        switch (ident)
                        {
                            case Grobal2.ISM_PASSWDSUCCESS:
                                GetPasswdSuccess(body);
                                break;
                            case Grobal2.ISM_CANCELADMISSION:
                                GetCancelAdmission(body);
                                break;
                            case Grobal2.ISM_SHIFTVENTURESERVER:
                                if (svMain.BoVentureServer)
                                {
                                    // 葛氰辑滚肺 捞悼秦咳
                                }
                                break;
                            case Grobal2.ISM_TOTALUSERCOUNT:
                                GetTotalUserCount(body);
                                break;
                            case Grobal2.ISM_ACCOUNTEXPIRED:
                                GetAccountExpired(body);
                                break;
                        }
                    }
                    else
                    {
                        break;
                    }
                }
                try
                {
                    svMain.csShare.Enter();
                    IDSocStr = BufStr + IDSocStr;
                }
                finally
                {
                    svMain.csShare.Leave();
                }
            }
            catch
            {
                svMain.MainOutMessage("[Exception] FrmIdSoc.DecodeSocStr");
            }
        }

        private void GetPasswdSuccess(string body)
        {
            string uid = String.Empty;
            string certstr = String.Empty;
            string paystr = String.Empty;
            string uaddr = String.Empty;
            string availablestr = String.Empty;
            string cversion = String.Empty;
            int avail;
            try
            {
                body = HUtil32.GetValidStr3(body, ref uid, new char[] { (char)0xa });
                body = HUtil32.GetValidStr3(body, ref certstr, new char[] { (char)0xa });
                body = HUtil32.GetValidStr3(body, ref paystr, new char[] { (char)0xa });
                body = HUtil32.GetValidStr3(body, ref availablestr, new char[] { (char)0xa });
                body = HUtil32.GetValidStr3(body, ref uaddr, new char[] { (char)0xa });
                body = HUtil32.GetValidStr3(body, ref cversion, new char[] { (char)0xa });
                avail = HUtil32.Str_ToInt(availablestr, 0);
                AddAdmission(uid, uaddr, HUtil32.Str_ToInt(certstr, 0), HUtil32.Str_ToInt(paystr, 0), avail, HUtil32.Str_ToInt(cversion, 0));
            }
            catch
            {
                svMain.MainOutMessage("[Exception] FrmIdSoc.GetPasswdSuccess");
            }
        }

        private void GetCancelAdmission(string body)
        {
            string uid;
            string certstr;
            int cert;
            try
            {
                certstr = HUtil32.GetValidStr3(body, uid, new string[] { "/" });
                cert = HUtil32.Str_ToInt(certstr, 0);
                DelAdmission(cert);
            }
            catch
            {
                svMain.MainOutMessage("[Exception] FrmIdSoc.GetCancelAdmission");
            }
        }

        private void AddAdmission(string uid, string uaddr, int certify, int paymode, int available, int clversion)
        {
            TAdmission pa;
            pa = new TAdmission();
            pa.usrid = uid;
            pa.uaddr = uaddr;
            pa.Certification = certify;
            pa.PayMode = paymode;
            pa.AvailableMode = available;
            pa.ClientVersion = clversion;
            try
            {
                svMain.csShare.Enter();
                AdmissionList.Add(pa);
            }
            finally
            {
                svMain.csShare.Leave();
            }
        }

        private void DelAdmission(int certify)
        {
            int i;
            string kickid;
            kickid = "";
            try
            {
                svMain.csShare.Enter();
                for (i = 0; i < AdmissionList.Count; i++)
                {
                    if ((AdmissionList[i] as TAdmission).Certification == certify)
                    {
                        kickid = (AdmissionList[i] as TAdmission).usrid;
                        this.Dispose(AdmissionList[i] as TAdmission);
                        AdmissionList.RemoveAt(i);
                        break;
                    }
                }
            }
            finally
            {
                svMain.csShare.Leave();
            }
            if (kickid != "")
            {
                svMain.csDelShare.Enter();
                try
                {
                    svMain.RunSocket.CloseUserId(kickid, certify);
                }
                finally
                {
                    svMain.csDelShare.Leave();
                }
            }
        }

        public bool GetAdmission_IsShareIP(string ip)
        {
            bool result;
            int i;
            result = false;
            for (i = 0; i < ShareIPList.Count; i++)
            {
                if (ShareIPList[i] == ip)
                {
                    result = true;
                    break;
                }
            }
            return result;
        }

        // 0: 铰牢 绝澜
        // 1: 眉氰魄
        // 2: 沥侥 荤侩磊
        public int GetAdmission(string uid, string ipaddr, int cert, ref int availmode, ref int clversion)
        {
            int result;
            int i;
            result = 0;
            availmode = 0;
            try
            {
                try
                {
                    svMain.csShare.Enter();
                    for (i = 0; i < AdmissionList.Count; i++)
                    {
                        if ((AdmissionList[i] as TAdmission).Certification == cert)
                        {
                            switch ((AdmissionList[i] as TAdmission).PayMode)
                            {
                                case 2:
                                    result = 3;
                                    break;
                                case 1:
                                    result = 2;
                                    break;
                                case 0:
                                    result = 1;
                                    break;
                            }
                            availmode = (AdmissionList[i] as TAdmission).AvailableMode;
                            clversion = (AdmissionList[i] as TAdmission).ClientVersion;
                            break;
                        }
                    }
                }
                finally
                {
                    svMain.csShare.Leave();
                }
            }
            catch
            {
                svMain.MainOutMessage("[RunSock->FrmIdSoc] GetAdmission exception");
            }
            return result;
        }

        private void GetTotalUserCount(string body)
        {
            svMain.TotalUserCount = HUtil32.Str_ToInt(body, 0);
        }

        private void GetAccountExpired(string body)
        {
            string uid = string.Empty;
            string certstr = string.Empty;
            int cert;
            try
            {
                certstr = HUtil32.GetValidStr3(body, ref uid, new string[] { "/" });
                cert = HUtil32.Str_ToInt(certstr, 0);
                if (!svMain.BoTestServer && !svMain.BoServiceMode)
                {
                    svMain.UserEngine.AccountExpired(uid);
                    DelAdmission(cert);
                }
            }
            catch
            {
                svMain.MainOutMessage("[Exception] FrmIdSoc.GetCancelAdmission");
            }
        }

        private void GetUsageInformation(string body)
        {
            string scurmon = String.Empty;
            string stotalmon = String.Empty;
            string slastmon = String.Empty;
            string sgross = String.Empty;
            string sgrosscount = String.Empty;
            body = HUtil32.GetValidStr3(body, ref scurmon, new string[] { "/" });
            body = HUtil32.GetValidStr3(body, ref stotalmon, new string[] { "/" });
            body = HUtil32.GetValidStr3(body, ref slastmon, new string[] { "/" });
            body = HUtil32.GetValidStr3(body, ref sgross, new string[] { "/" });
            body = HUtil32.GetValidStr3(body, ref sgrosscount, new string[] { "/" });
            svMain.CurrentMonthlyCard = HUtil32.Str_ToInt(scurmon, 0);
            svMain.TotalTimeCardUsage = HUtil32.Str_ToInt(stotalmon, 0);
            svMain.LastMonthTotalTimeCardUsage = HUtil32.Str_ToInt(slastmon, 0);
            svMain.GrossTimeCardUsage = HUtil32.Str_ToInt(sgross, 0);
            svMain.GrossResetCount = HUtil32.Str_ToInt(sgrosscount, 0);
        }
    }

    public struct TAdmission
    {
        public string[] usrid;
        public string[] uaddr;
        public int Certification;
        public int PayMode;
        public int AvailableMode;
        public int ClientVersion;
    }
}

namespace GameSvr
{
    public class IdSrvClient
    {
        public static TFrmIDSoc FrmIDSoc = null;
    } 
}

