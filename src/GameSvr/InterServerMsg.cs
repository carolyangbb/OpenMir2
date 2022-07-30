using System;
using System.IO;
using System.Net.Sockets;
using System.Windows.Forms;
using SystemModule;

namespace GameSvr
{
    public class TFrmSrvMsg
    {
        private TServerMsgInfo PCur = null;
        public TUserHuman WorkHum = null;
        public TServerMsgInfo[] ServerArr;

        public TFrmSrvMsg()
        {
 
        }

        public void FormCreate(System.Object Sender, System.EventArgs _e1)
        {
            FillChar(ServerArr, sizeof(TServerMsgInfo) * InterServerMsg.MAXSERVER, '\0');
            WorkHum = new TUserHuman();
        }

        public void FormDestroy(Object Sender)
        {
            WorkHum.Free();
        }

        public void Initialize()
        {
            this.Active = false;
            MsgServer.Port = svMain.MsgServerPort;
            this.Active = true;
        }

        public void MsgServerClientConnect(Object Sender, Socket Socket)
        {
            int i;
            for (i = 0; i < InterServerMsg.MAXSERVER; i++)
            {
                if (ServerArr[i].Socket == null)
                {
                    ServerArr[i].Socket = Socket;
                    ServerArr[i].SocData = "";
                    // ServerArr[i].SendData := '';
                    Socket.Data = i as object;
                    break;
                }
            }
        }

        public void MsgServerClientDisconnect(Object Sender, Socket Socket)
        {
            int i;
            for (i = 0; i < InterServerMsg.MAXSERVER; i++)
            {
                if (ServerArr[i].Socket == Socket)
                {
                    ServerArr[i].Socket = null;
                    ServerArr[i].SocData = "";
                    // ServerArr[i].SendData := '';
                    break;
                }
            }
        }

        public void MsgServerClientError(Object Sender, Socket Socket, TErrorEvent ErrorEvent, ref int ErrorCode)
        {
            ErrorCode = 0;
            Socket.Close;
        }

        public void MsgServerClientRead(Object Sender, Socket Socket)
        {
            int idx;
            idx = (int)Socket.Data;
            if (idx >= 0 && idx <= InterServerMsg.MAXSERVER - 1)
            {
                if (ServerArr[idx].Socket == Socket)
                {
                    ServerArr[idx].SocData = ServerArr[idx].SocData + Socket.ReceiveText;
                }
            }
        }

        // -------------------------------------------------------------------
        public void SendSocket(Socket Socket, string str)
        {
            if (Socket.Connected)
            {
                Socket.SendText("(" + str + ")");
            }
        }

        public void SendServerSocket(string msgstr)
        {
            int i;
            for (i = 0; i < InterServerMsg.MAXSERVER; i++)
            {
                if (ServerArr[i].Socket != null)
                {
                    SendSocket(ServerArr[i].Socket, msgstr);
                }
            }
        }

        public void DecodeSocStr_SendOtherServer(string msgstr)
        {
            int i;
            for (i = 0; i < InterServerMsg.MAXSERVER; i++)
            {
                if (ServerArr[i].Socket != null)
                {
                    if (ServerArr[i].Socket != ps.Socket)
                    {
                        SendSocket(ServerArr[i].Socket, msgstr);
                    }
                }
            }
        }

        // -------------------------------------------------------------------
        private void DecodeSocStr(TServerMsgInfo ps)
        {
            string BufStr;
            string str;
            string snumstr;
            string head;
            string body= string.Empty;
            int ident;
            int snum;
            if (ps.SocData.IndexOf(")") <= 0)
            {
                return;
            }
            try
            {
                BufStr = ps.SocData;
                ps.SocData = "";
                while (BufStr.IndexOf(")") > 0)
                {
                    BufStr = ArrestStringEx(BufStr, "(", ")", str);
                    if (str != "")
                    {
                        DecodeSocStr_SendOtherServer(str);
                        body = HUtil32.GetValidStr3(str, ref head, new string[] { "/" });
                        body = HUtil32.GetValidStr3(body, ref snumstr, new string[] { "/" });
                        ident = HUtil32.Str_ToInt(head, 0);
                        snum = HUtil32.Str_ToInt(EDcode.DecodeString(snumstr), -1);
                        switch (ident)
                        {
                            case Grobal2.ISM_USERSERVERCHANGE:
                                MsgGetUserServerChange(snum, body);
                                break;
                            case Grobal2.ISM_CHANGESERVERRECIEVEOK:
                                MsgGetUserChangeServerRecieveOk(snum, body);
                                break;
                            case Grobal2.ISM_USERLOGON:
                                MsgGetUserLogon(snum, body);
                                break;
                            case Grobal2.ISM_USERLOGOUT:
                                MsgGetUserLogout(snum, body);
                                break;
                            case Grobal2.ISM_WHISPER:
                                MsgGetWhisper(snum, body);
                                break;
                            case Grobal2.ISM_GMWHISPER:
                                MsgGetGMWhisper(snum, body);
                                break;
                            case Grobal2.ISM_LM_WHISPER:
                                MsgGetLoverWhisper(snum, body);
                                break;
                            case Grobal2.ISM_SYSOPMSG:
                                MsgGetSysopMsg(snum, body);
                                break;
                            case Grobal2.ISM_ADDGUILD:
                                MsgGetAddGuild(snum, body);
                                break;
                            case Grobal2.ISM_DELGUILD:
                                MsgGetDelGuild(snum, body);
                                break;
                            case Grobal2.ISM_RELOADGUILD:
                                MsgGetReloadGuild(snum, body);
                                break;
                            case Grobal2.ISM_GUILDMSG:
                                MsgGetGuildMsg(snum, body);
                                break;
                            case Grobal2.ISM_GUILDWAR:
                                MsgGetGuildWarInfo(snum, body);
                                break;
                            case Grobal2.ISM_CHATPROHIBITION:
                                MsgGetChatProhibition(snum, body);
                                break;
                            case Grobal2.ISM_CHATPROHIBITIONCANCEL:
                                MsgGetChatProhibitionCancel(snum, body);
                                break;
                            case Grobal2.ISM_CHANGECASTLEOWNER:
                                MsgGetChangeCastleOwner(snum, body);
                                break;
                            case Grobal2.ISM_RELOADCASTLEINFO:
                                MsgGetReloadCastleAttackers(snum);
                                break;
                            case Grobal2.ISM_RELOADADMIN:
                                MsgGetReloadAdmin();
                                break;
                            case Grobal2.ISM_MARKETOPEN:
                                MsgGetMarketOpen(true);
                                break;
                            case Grobal2.ISM_MARKETCLOSE:
                                MsgGetMarketOpen(false);
                                break;
                            case Grobal2.ISM_RELOADCHATLOG:
                                // 2003/08/28 채팅로그
                                MsgGetReloadChatLog();
                                break;
                            case Grobal2.ISM_LM_DELETE:
                                MsgGetRelationshipDelete(snum, body);
                                break;
                            case Grobal2.ISM_USER_INFO:
                            case Grobal2.ISM_FRIEND_INFO:
                            case Grobal2.ISM_FRIEND_DELETE:
                            case Grobal2.ISM_FRIEND_OPEN:
                            case Grobal2.ISM_FRIEND_CLOSE:
                            case Grobal2.ISM_FRIEND_RESULT:
                            case Grobal2.ISM_TAG_SEND:
                            case Grobal2.ISM_TAG_RESULT:
                                // 친구 쪽지 관련 내부 서버 메세지
                                MsgGetUserMgr(snum, body, ident);
                                break;
                            case Grobal2.ISM_RELOADMAKEITEMLIST:
                                // 제조 재료 목록 리로드(sonmg)
                                MsgGetReloadMakeItemList();
                                break;
                            case Grobal2.ISM_GUILDMEMBER_RECALL:
                                // 문원소환.
                                MsgGetGuildMemberRecall(snum, body);
                                break;
                            case Grobal2.ISM_RELOADGUILDAGIT:
                                MsgGetReloadGuildAgit(snum, body);
                                break;
                            case Grobal2.ISM_LM_LOGIN:
                                // 연인
                                MsgGetLoverLogin(snum, body);
                                break;
                            case Grobal2.ISM_LM_LOGOUT:
                                MsgGetLoverLogout(snum, body);
                                break;
                            case Grobal2.ISM_LM_LOGIN_REPLY:
                                MsgGetLoverLoginReply(snum, body);
                                break;
                            case Grobal2.ISM_LM_KILLED_MSG:
                                MsgGetLoverKilledMsg(snum, body);
                                break;
                            case Grobal2.ISM_RECALL:
                                // 소환
                                MsgGetRecall(snum, body);
                                break;
                            case Grobal2.ISM_REQUEST_RECALL:
                                MsgGetRequestRecall(snum, body);
                                break;
                            case Grobal2.ISM_REQUEST_LOVERRECALL:
                                MsgGetRequestLoverRecall(snum, body);
                                break;
                        }
                    }
                    else
                    {
                        break;
                    }
                }
                ps.SocData = BufStr + ps.SocData;
            }
            catch
            {
                svMain.MainOutMessage("[Exception] FrmIdSoc.DecodeSocStr");
            }
        }

        // 다른 서버로 서버 이동을 함.
        // Decode(servernumber) + '/' + Decode(filename)(사용자 정보가 기록된 파일)
        public void MsgGetUserServerChange(int snum, string body)
        {
            string ufilename;
            int i;
            int fhandle;
            int checksum;
            int filechecksum;
            TServerShiftUserInfo psui;
            if (svMain.ShareBaseDir != svMain.ShareBaseDirCopy)
            {
                svMain.ShareBaseDir = svMain.ShareBaseDirCopy;
            }
            ufilename = EDcode.DecodeString(body);
            psui = null;
            if (svMain.ServerIndex == snum)
            {
                // 내 서버의 메세지인지
                try
                {
                    fhandle = File.Open(svMain.ShareBaseDir + ufilename, (FileMode)FileAccess.Read | FileShare.ReadWrite);
                    if (fhandle > 0)
                    {
                        psui = new TServerShiftUserInfo();
                        FileRead(fhandle, psui, sizeof(TServerShiftUserInfo));
                        FileRead(fhandle, filechecksum, sizeof(int));
                        fhandle.Close();
                    }
                    File.Delete(svMain.ShareBaseDir + ufilename);
                    checksum = 0;
                    for (i = 0; i < sizeof(TServerShiftUserInfo); i++)
                    {
                        checksum = checksum + ((byte)psui + i);
                    }
                    if (checksum == filechecksum)
                    {
                        svMain.UserEngine.AddServerWaitUser(psui);
                        svMain.UserEngine.SendInterServerMsg(Grobal2.ISM_CHANGESERVERRECIEVEOK.ToString() + "/" + EDcode.EncodeString(svMain.ServerIndex.ToString()) + "/" + EDcode.EncodeString(ufilename));
                    }
                    else
                    {
                        this.Dispose(psui);
                    }
                }
                catch
                {
                    svMain.MainOutMessage("[Exception] MsgGetUserServerChange.." + svMain.ShareBaseDir + ufilename);
                }
            }
        }

        public void MsgGetUserChangeServerRecieveOk(int snum, string body)
        {
            string ufilename = EDcode.DecodeString(body);
            svMain.UserEngine.GetISMChangeServerReceive(ufilename);
        }

        public void MsgGetUserLogon(int snum, string body)
        {
            string uname = EDcode.DecodeString(body);
            svMain.UserEngine.OtherServerUserLogon(snum, uname);
        }

        public void MsgGetUserLogout(int snum, string body)
        {
            string uname = EDcode.DecodeString(body);
            svMain.UserEngine.OtherServerUserLogout(snum, uname);
        }

        public void MsgGetWhisper(int snum, string body)
        {
            string uname = string.Empty;
            if (snum == svMain.ServerIndex)
            {
                string str = EDcode.DecodeString(body);
                str = HUtil32.GetValidStr3(str, ref uname, new string[] { "/" });
                TUserHuman hum = svMain.UserEngine.GetUserHuman(uname);
                if (hum != null)
                {
                    // 운영자 귓속말 차단
                    if (!hum.bStealth)
                    {
                        hum.WhisperRe(str, false);
                    }
                }
            }
        }

        public void MsgGetGMWhisper(int snum, string body)
        {
            string uname = string.Empty;
            if (snum == svMain.ServerIndex)
            {
                string str = EDcode.DecodeString(body);
                str = HUtil32.GetValidStr3(str, ref uname, new string[] { "/" });
                TUserHuman hum = svMain.UserEngine.GetUserHuman(uname);
                if (hum != null)
                {
                    if (!hum.bStealth)
                    {
                        hum.WhisperRe(str, true);
                    }
                }
            }
        }

        public void MsgGetLoverWhisper(int snum, string body)
        {
            string str;
            string uname = string.Empty;
            TUserHuman hum;
            if (snum == svMain.ServerIndex)
            {
                str = EDcode.DecodeString(body);
                str = HUtil32.GetValidStr3(str, ref uname, new string[] { "/" });
                hum = svMain.UserEngine.GetUserHuman(uname);
                if (hum != null)
                {
                    // 운영자 귓속말 차단
                    if (!hum.bStealth)
                    {
                        hum.LoverWhisperRe(str);
                    }
                }
            }
        }

        public void MsgGetRelationshipDelete(int snum, string body)
        {
            string str;
            string uname = string.Empty;
            string reqType = string.Empty;
            TUserHuman hum;
            if (snum == svMain.ServerIndex)
            {
                str = EDcode.DecodeString(body);
                str = HUtil32.GetValidStr3(str, ref uname, new string[] { "/" });
                str = HUtil32.GetValidStr3(str, ref reqType, new string[] { "/" });
                hum = svMain.UserEngine.GetUserHuman(uname);
                if (hum != null)
                {
                    hum.RelationShipDeleteOther(HUtil32.Str_ToInt(reqType, 0), uname);
                }
            }
        }

        public void MsgGetSysopMsg(int snum, string body)
        {
            svMain.UserEngine.SysMsgAll(EDcode.DecodeString(body));
        }

        public void MsgGetAddGuild(int snum, string body)
        {
            string gname;
            string mname;
            body = EDcode.DecodeString(body);
            mname = HUtil32.GetValidStr3(body, ref gname, new string[] { "/" });
            svMain.GuildMan.AddGuild(gname, mname);
        }

        public void MsgGetDelGuild(int snum, string body)
        {
            string gname;
            gname = EDcode.DecodeString(body);
            // 장원 반환 후 문파삭제.
            // GuildAgitMan.DelGuildAgit( gname );
            svMain.GuildMan.DelGuild(gname);
        }

        public void MsgGetReloadGuild(int snum, string body)
        {
            string gname;
            TGuild g;
            gname = EDcode.DecodeString(body);
            if (snum == 0)
            {
                // 0번 서버에서 보낸거....
                g = svMain.GuildMan.GetGuild(gname);
                if (g != null)
                {
                    g.LoadGuild();
                    svMain.UserEngine.GuildMemberReLogin(g);
                }
            }
            else
            {
                // 다른 서버에서 보냄
                if (svMain.ServerIndex != snum)
                {
                    g = svMain.GuildMan.GetGuild(gname);
                    if (g != null)
                    {
                        g.LoadGuildFile(gname + "." + snum.ToString());
                        svMain.UserEngine.GuildMemberReLogin(g);
                        g.SaveGuild();
                    }
                }
            }
        }

        public void MsgGetGuildMsg(int snum, string body)
        {
            string str;
            string gname;
            TGuild g;
            str = EDcode.DecodeString(body);
            str = HUtil32.GetValidStr3(str, ref gname, new string[] { "/" });
            if (gname != "")
            {
                g = svMain.GuildMan.GetGuild(gname);
                if (g != null)
                {
                    g.GuildMsg(str);
                }
            }
        }

        public void MsgGetGuildWarInfo(int snum, string body)
        {
            string str;
            string gname;
            string warguildname;
            string starttime;
            string remaintime;
            TGuild g;
            TGuild warguild;
            TGuildWarInfo pgw;
            int i;
            long currenttick;
            if (snum == 0)
            {
                str = EDcode.DecodeString(body);
                str = HUtil32.GetValidStr3(str, ref gname, new string[] { "/" });
                str = HUtil32.GetValidStr3(str, ref warguildname, new string[] { "/" });
                str = HUtil32.GetValidStr3(str, ref starttime, new string[] { "/" });
                remaintime = str;
                if ((gname != "") && (warguildname != ""))
                {
                    g = svMain.GuildMan.GetGuild(gname);
                    warguild = svMain.GuildMan.GetGuild(warguildname);
                    if ((g != null) && (warguild != null))
                    {
                        currenttick = GetTickCount;
                        if (svMain.ServerTickDifference == 0)
                        {
                            svMain.ServerTickDifference = Convert.ToInt64(starttime) - currenttick;
                        }
                        pgw = null;
                        for (i = 0; i < g.KillGuilds.Count; i++)
                        {
                            if (((TGuildWarInfo)g.KillGuilds.Values[i]).WarGuild == warguild)
                            {
                                pgw = (TGuildWarInfo)g.KillGuilds.Values[i];
                                if (pgw != null)
                                {
                                    pgw.WarGuild = warguild;
                                    pgw.WarStartTime = Convert.ToInt64(starttime) - svMain.ServerTickDifference;
                                    pgw.WarRemain = Convert.ToInt64(remaintime);
                                    break;
                                }
                            }
                        }
                        if (pgw == null)
                        {
                            pgw = new TGuildWarInfo();
                            pgw.WarGuild = warguild;
                            pgw.WarStartTime = Convert.ToInt64(starttime) - svMain.ServerTickDifference;
                            pgw.WarRemain = Convert.ToInt64(remaintime);
                            g.KillGuilds.Add(warguild.GuildName, pgw as Object);
                        }
                        g.MemberNameChanged();
                        g.GuildInfoChange();
                    }
                }
            }
        }

        public void MsgGetChatProhibition(int snum, string body)
        {
            string str;
            string whostr;
            string minstr;
            str = EDcode.DecodeString(body);
            str = HUtil32.GetValidStr3(str, ref whostr, new string[] { "/" });
            str = HUtil32.GetValidStr3(str, ref minstr, new string[] { "/" });
            if (whostr != "")
            {
                WorkHum.CmdAddShutUpList(whostr, minstr, false);
            }
        }

        public void MsgGetChatProhibitionCancel(int snum, string body)
        {
            string whostr;
            whostr = EDcode.DecodeString(body);
            if (whostr != "")
            {
                WorkHum.CmdDelShutUpList(whostr, false);
            }
        }

        public void MsgGetChangeCastleOwner(int snum, string body)
        {
            string gldstr;
            gldstr = EDcode.DecodeString(body);
            WorkHum.CmdChangeUserCastleOwner(gldstr, false);
        }

        public void MsgGetReloadCastleAttackers(int snum)
        {
            svMain.UserCastle.LoadAttackerList();
        }

        public void MsgGetReloadAdmin()
        {
            LocalDB.FrmDB.LoadAdminFiles();
        }

        public void MsgGetReloadChatLog()
        {
            LocalDB.FrmDB.LoadChatLogFiles();
        }

        public void MsgGetUserMgr(int snum, string body, int Ident_)
        {
            string username;
            string msgbody;
            string str;
            str = EDcode.DecodeString(body);
            msgbody = HUtil32.GetValidStr3(str, ref username, new string[] { "/" });
            svMain.UserMgrEngine.OnExternInterMsg(snum, Ident_, username, msgbody);
        }

        // 제조 재료 목록 리로드(sonmg)
        // 제조 재료 목록 리로드(sonmg)
        public void MsgGetReloadMakeItemList()
        {
            LocalDB.FrmDB.LoadMakeItemList();
        }

        // 문원소환.
        // 문원소환.
        public void MsgGetGuildMemberRecall(int snum, string body)
        {
            TUserHuman hum;
            int dx=0;
            int dy=0;
            string dxstr;
            string dystr;
            string str;
            string uname = string.Empty;
            if (snum == svMain.ServerIndex)
            {
                str = EDcode.DecodeString(body);
                str = HUtil32.GetValidStr3(str, ref uname, new string[] { "/" });
                str = HUtil32.GetValidStr3(str, ref dxstr, new string[] { "/" });
                str = HUtil32.GetValidStr3(str, ref dystr, new string[] { "/" });
                dx = HUtil32.Str_ToInt(dxstr, 0);
                dy = HUtil32.Str_ToInt(dystr, 0);
                hum = svMain.UserEngine.GetUserHuman(uname);
                if (hum != null)
                {
                    if (hum.BoEnableAgitRecall)
                    {
                        hum.SysMsg("The guild master has recalled " + hum.UserName + ".", 0);
                        hum.SendRefMsg(Grobal2.RM_SPACEMOVE_HIDE, 0, 0, 0, 0, "");
                        hum.SpaceMove(str, (short)dx, (short)dy, 0);
                    }
                }
            }
        }

        public void MsgGetReloadGuildAgit(int snum, string body)
        {
            svMain.GuildAgitMan.ClearGuildAgitList();
            svMain.GuildAgitMan.LoadGuildAgitList();
        }

        // 연인
        public void MsgGetLoverLogin(int snum, string body)
        {
            TUserHuman humlover;
            int svidx=0;
            string str;
            string uname = string.Empty;
            string lovername;
            if (snum == svMain.ServerIndex)
            {
                str = EDcode.DecodeString(body);
                str = HUtil32.GetValidStr3(str, ref uname, new string[] { "/" });
                str = HUtil32.GetValidStr3(str, ref lovername, new string[] { "/" });
                humlover = svMain.UserEngine.GetUserHuman(lovername);
                if (humlover != null)
                {
                    humlover.SysMsg(uname + " has entered " + str + ".", 6);
                    // 연인 로그인 메시지를 받으면 나의 위치 정보를 알려줌
                    if (svMain.UserEngine.FindOtherServerUser(uname, ref svidx))
                    {
                        svMain.UserEngine.SendInterMsg(Grobal2.ISM_LM_LOGIN_REPLY, svidx, lovername + "/" + uname + "/" + humlover.PEnvir.MapTitle);
                    }
                }
            }
        }

        public void MsgGetLoverLogout(int snum, string body)
        {
            TUserHuman hum;
            string str;
            string uname = string.Empty;
            string lovername;
            if (snum == svMain.ServerIndex)
            {
                str = EDcode.DecodeString(body);
                str = HUtil32.GetValidStr3(str, ref uname, new string[] { "/" });
                lovername = str;
                hum = svMain.UserEngine.GetUserHuman(lovername);
                if (hum != null)
                {
                    hum.SysMsg(uname + " has exited from the game.", 5);
                }
            }
        }

        public void MsgGetLoverLoginReply(int snum, string body)
        {
            TUserHuman humlover;
            string str;
            string uname = string.Empty;
            string lovername;
            if (snum == svMain.ServerIndex)
            {
                str = EDcode.DecodeString(body);
                str = HUtil32.GetValidStr3(str, ref uname, new string[] { "/" });
                str = HUtil32.GetValidStr3(str, ref lovername, new string[] { "/" });
                humlover = svMain.UserEngine.GetUserHuman(lovername);
                if (humlover != null)
                {
                    humlover.SysMsg(uname + " is currently in " + str + ".", 6);
                }
            }
        }

        public void MsgGetLoverKilledMsg(int snum, string body)
        {
            TUserHuman hum;
            string str;
            string uname = string.Empty;
            if (snum == svMain.ServerIndex)
            {
                str = EDcode.DecodeString(body);
                str = HUtil32.GetValidStr3(str, ref uname, new string[] { "/" });
                hum = svMain.UserEngine.GetUserHuman(uname);
                if (hum != null)
                {
                    hum.SysMsg(str, 0);
                }
            }
        }

        // 소환
        // 소환
        public void MsgGetRecall(int snum, string body)
        {
            TUserHuman hum;
            int dx=0;
            int dy=0;
            string dxstr;
            string dystr;
            string str;
            string uname = string.Empty;
            if (snum == svMain.ServerIndex)
            {
                str = EDcode.DecodeString(body);
                str = HUtil32.GetValidStr3(str, ref uname, new string[] { "/" });
                str = HUtil32.GetValidStr3(str, ref dxstr, new string[] { "/" });
                str = HUtil32.GetValidStr3(str, ref dystr, new string[] { "/" });
                dx = HUtil32.Str_ToInt(dxstr, 0);
                dy = HUtil32.Str_ToInt(dystr, 0);
                hum = svMain.UserEngine.GetUserHuman(uname);
                if (hum != null)
                {
                    hum.SendRefMsg(Grobal2.RM_SPACEMOVE_HIDE, 0, 0, 0, 0, "");
                    hum.SpaceMove(str, (short)dx, (short)dy, 0);
                    // 공간이동
                }
            }
        }

        public void MsgGetRequestRecall(int snum, string body)
        {
            TUserHuman hum;
            string str;
            string uname = string.Empty;
            if (snum == svMain.ServerIndex)
            {
                str = EDcode.DecodeString(body);
                str = HUtil32.GetValidStr3(str, ref uname, new string[] { "/" });
                hum = svMain.UserEngine.GetUserHuman(uname);
                if (hum != null)
                {
                    hum.CmdRecallMan(str, "");
                }
            }
        }

        public void MsgGetRequestLoverRecall(int snum, string body)
        {
            TUserHuman hum;
            string str;
            string uname = string.Empty;
            if (snum == svMain.ServerIndex)
            {
                str = EDcode.DecodeString(body);
                str = HUtil32.GetValidStr3(str, ref uname, new string[] { "/" });
                hum = svMain.UserEngine.GetUserHuman(uname);
                if (hum != null)
                {
                    if (!hum.PEnvir.NoRecall)
                    {
                        hum.CmdRecallMan(str, "");
                    }
                }
            }
        }

        // 위탁판매
        public void MsgGetMarketOpen(bool WantOpen)
        {
            SqlEngn.Units.SqlEngn.SqlEngine.Open(WantOpen);
        }

        // -------------------------------------------------------------------
        public void Run()
        {
            int i;
            TServerMsgInfo ps;
            try
            {
                for (i = 0; i < InterServerMsg.MAXSERVER; i++)
                {
                    if (ServerArr[i].Socket != null)
                    {
                        ps = ServerArr[i];
                        DecodeSocStr(ps);
                    }
                }
            }
            catch
            {
                svMain.MainOutMessage("EXCEPT TFrmSrvMsg.Run");
            }
        }

    } // end TFrmSrvMsg

    public struct TServerShiftUserInfo
    {
        public string UserName;
        public FDBRecord rcd;
        public int Certification;
        public string GroupOwner;
        public string GroupMembers;
        // 그룹원은 최대 9명
        public bool BoHearCry;
        public bool BoHearWhisper;
        public bool BoHearGuildMsg;
        public bool BoSysopMode;
        public bool BoSuperviserMode;
        public string[] WhisperBlockNames;
        // 귓속말 차단 캐릭터
        public bool BoSlaveRelax;
        // 부하몹의 휴식상태 (sonmg 2005/01/21)
        public TSlaveInfo[] Slaves;
        public long waittime;
        // timeout은 30초
        public byte[] StatusValue;
        // 상승 능력치 값 추가(sonmg 2005/06/03)
        public byte[] ExtraAbil;
        // 상승 능력치 값
        public long[] ExtraAbilTimes;
    } // end TServerShiftUserInfo

    public struct TServerMsgInfo
    {
        public Socket Socket;
        public string SocData;
    } // end TServerMsgInfo

}

namespace GameSvr
{
    public class InterServerMsg
    {
        public static TFrmSrvMsg FrmSrvMsg = null;
        public const int MAXSERVER = 10;
    } // end InterServerMsg

}

