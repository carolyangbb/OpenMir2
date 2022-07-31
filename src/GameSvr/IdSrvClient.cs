using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using SystemModule;

namespace GameSvr
{
    public class TFrmIDSoc
    {
        private IList<TAdmission> AdmissionList = null;
        private ArrayList ShareIPList = null;
        private long LoginServerCheckTime = 0;
        public string ServerAddress = string.Empty;
        public int ServerPort = 0;
        public string IDSocStr = string.Empty;

        public TFrmIDSoc()
        {

        }

        public void FormCreate(object Sender, System.EventArgs _e1)
        {
            //IDSocket.Address = "";
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
            LoginServerCheckTime = HUtil32.GetTickCount();
        }

        public void FormDestroy(object Sender)
        {

        }

        public void LoadShareIPList()
        {

        }

        public void Timer1Timer(object Sender, System.EventArgs _e1)
        {
            //if (IDSocket.Address != "")
            //{
            //    if (!IDSocket.Active)
            //    {
            //        IDSocket.Active = true;
            //    }
            //}
        }

        public void IDSocketConnect(object Sender, Socket Socket)
        {
        }

        public void IDSocketDisconnect(object Sender, Socket Socket)
        {
        }

        public void IDSocketError(object Sender, Socket Socket, ref int ErrorCode)
        {
            ErrorCode = 0;
            Socket.Close();
        }

        public void IDSocketRead(object Sender, Socket Socket)
        {
            try
            {
                M2Share.csShare.Enter();
                //IDSocStr = IDSocStr + Socket.ReceiveText;
            }
            finally
            {
                M2Share.csShare.Leave();
            }
        }

        public void Initialize()
        {
            /*this.Active = false;
            IDSocket.Address = ServerAddress;
            IDSocket.Port = ServerPort;
            this.Active = true;*/
        }

        public void SendUserClose(string uid, int cert)
        {
            /*if (IDSocket.Socket.Connected)
            {
                IDSocket.Socket.SendText("(" + Grobal2.ISM_USERCLOSED.ToString() + "/" + uid + "/" + cert.ToString() + ")");
            }*/
        }

        public void SendGameTimeOfTimeCardUser(string uid, int gametime_min)
        {

        }

        public void SendUserShiftToVentureServer(string svname, string uid, int cert)
        {
            /*if (IDSocket.Socket.Connected)
            {
                IDSocket.Socket.SendText("(" + Grobal2.ISM_SHIFTVENTURESERVER.ToString() + "/" + svname + "/" + uid + "/" + cert.ToString() + ")");
            }*/
        }

        public void SendUserCount(int ucount)
        {
            /*if (IDSocket.Socket.Connected)
            {
                IDSocket.Socket.SendText("(" + Grobal2.ISM_USERCOUNT.ToString() + "/" + svMain.ServerName + "/" + svMain.ServerIndex.ToString() + "/" + ucount.ToString() + ")");
            }*/
        }

        public void DecodeSocStr()
        {
            string BufStr = string.Empty;
            string str = string.Empty;
            string head = string.Empty;
            string body = string.Empty;
            int ident = 0;
            try
            {
                M2Share.csShare.Enter();
                if (IDSocStr.IndexOf(")") <= 0)
                {
                    return;
                }
                BufStr = IDSocStr;
                IDSocStr = "";
            }
            finally
            {
                M2Share.csShare.Leave();
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
                                if (M2Share.BoVentureServer)
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
                    M2Share.csShare.Enter();
                    IDSocStr = BufStr + IDSocStr;
                }
                finally
                {
                    M2Share.csShare.Leave();
                }
            }
            catch
            {
                M2Share.MainOutMessage("[Exception] FrmIdSoc.DecodeSocStr");
            }
        }

        private void GetPasswdSuccess(string body)
        {
            string uid = string.Empty;
            string certstr = string.Empty;
            string paystr = string.Empty;
            string uaddr = string.Empty;
            string availablestr = string.Empty;
            string cversion = string.Empty;
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
                M2Share.MainOutMessage("[Exception] FrmIdSoc.GetPasswdSuccess");
            }
        }

        private void GetCancelAdmission(string body)
        {
            string uid = string.Empty;
            string certstr;
            int cert;
            try
            {
                certstr = HUtil32.GetValidStr3(body, ref uid, new string[] { "/" });
                cert = HUtil32.Str_ToInt(certstr, 0);
                DelAdmission(cert);
            }
            catch
            {
                M2Share.MainOutMessage("[Exception] FrmIdSoc.GetCancelAdmission");
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
                M2Share.csShare.Enter();
                AdmissionList.Add(pa);
            }
            finally
            {
                M2Share.csShare.Leave();
            }
        }

        private void DelAdmission(int certify)
        {
            int i;
            string kickid;
            kickid = "";
            try
            {
                M2Share.csShare.Enter();
                for (i = 0; i < AdmissionList.Count; i++)
                {
                    if (AdmissionList[i].Certification == certify)
                    {
                        kickid = AdmissionList[i].usrid;
                        //this.Dispose(AdmissionList[i] as TAdmission);
                        AdmissionList.RemoveAt(i);
                        break;
                    }
                }
            }
            finally
            {
                M2Share.csShare.Leave();
            }
            if (kickid != "")
            {
                M2Share.csDelShare.Enter();
                try
                {
                    M2Share.RunSocket.CloseUserId(kickid, certify);
                }
                finally
                {
                    M2Share.csDelShare.Leave();
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
                    M2Share.csShare.Enter();
                    for (i = 0; i < AdmissionList.Count; i++)
                    {
                        if (AdmissionList[i].Certification == cert)
                        {
                            switch (AdmissionList[i].PayMode)
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
                            availmode = AdmissionList[i].AvailableMode;
                            clversion = AdmissionList[i].ClientVersion;
                            break;
                        }
                    }
                }
                finally
                {
                    M2Share.csShare.Leave();
                }
            }
            catch
            {
                M2Share.MainOutMessage("[RunSock->FrmIdSoc] GetAdmission exception");
            }
            return result;
        }

        private void GetTotalUserCount(string body)
        {
            M2Share.TotalUserCount = HUtil32.Str_ToInt(body, 0);
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
                if (!M2Share.BoTestServer && !M2Share.BoServiceMode)
                {
                    M2Share.UserEngine.AccountExpired(uid);
                    DelAdmission(cert);
                }
            }
            catch
            {
                M2Share.MainOutMessage("[Exception] FrmIdSoc.GetCancelAdmission");
            }
        }

        private void GetUsageInformation(string body)
        {
            string scurmon = string.Empty;
            string stotalmon = string.Empty;
            string slastmon = string.Empty;
            string sgross = string.Empty;
            string sgrosscount = string.Empty;
            body = HUtil32.GetValidStr3(body, ref scurmon, new string[] { "/" });
            body = HUtil32.GetValidStr3(body, ref stotalmon, new string[] { "/" });
            body = HUtil32.GetValidStr3(body, ref slastmon, new string[] { "/" });
            body = HUtil32.GetValidStr3(body, ref sgross, new string[] { "/" });
            body = HUtil32.GetValidStr3(body, ref sgrosscount, new string[] { "/" });
            M2Share.CurrentMonthlyCard = HUtil32.Str_ToInt(scurmon, 0);
            M2Share.TotalTimeCardUsage = HUtil32.Str_ToInt(stotalmon, 0);
            M2Share.LastMonthTotalTimeCardUsage = HUtil32.Str_ToInt(slastmon, 0);
            M2Share.GrossTimeCardUsage = HUtil32.Str_ToInt(sgross, 0);
            M2Share.GrossResetCount = HUtil32.Str_ToInt(sgrosscount, 0);
        }
    }

    public class TAdmission
    {
        public string usrid;
        public string uaddr;
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

