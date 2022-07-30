using System;
using System.Net.Sockets;
using SystemModule;

namespace GameSvr
{
    public class TFrmMsgClient
    {
        private long start = 0;
        private string SocData = String.Empty;
        public TFrmMsgClient()
        {

        }

        public void Initialize()
        {
            this.Active = false;
            MsgClient.Address = svMain.MsgServerAddress;
            MsgClient.Port = svMain.MsgServerPort;
            start = GetTickCount;
        }

        public void MsgClientConnect(Object Sender, Socket Socket)
        {
            SocData = "";
        }

        public void MsgClientDisconnect(Object Sender, Socket Socket)
        {
        }

        public void MsgClientError(Object Sender, Socket Socket, TErrorEvent ErrorEvent, ref int ErrorCode)
        {
            ErrorCode = 0;
            Socket.Close;
        }

        public void MsgClientRead(Object Sender, Socket Socket)
        {
            SocData = SocData + Socket.ReceiveText;
        }

        public void SendSocket(string str)
        {
            if (MsgClient.Socket.Connected)
            {
                MsgClient.Socket.SendText("(" + str + ")");
            }
        }

        private void DecodeSocStr()
        {
            string BufStr;
            string str;
            string head;
            string body= string.Empty;
            string snumstr;
            int ident;
            int snum;
            if (SocData.IndexOf(")") <= 0)
            {
                return;
            }
            try
            {
                BufStr = SocData;
                SocData = "";
                while (BufStr.IndexOf(")") > 0)
                {
                    BufStr = ArrestStringEx(BufStr, "(", ")", str);
                    if (str != "")
                    {
                        body = GetValidStr3(str, head, new string[] { "/" });
                        body = GetValidStr3(body, snumstr, new string[] { "/" });
                        ident = HUtil32.Str_ToInt(head, 0);
                        snum = HUtil32.Str_ToInt(EDcode.DecodeString(snumstr), -1);
                        switch (ident)
                        {
                            case Grobal2.ISM_USERSERVERCHANGE:
                                InterServerMsg.FrmSrvMsg.MsgGetUserServerChange(snum, body);
                                break;
                            case Grobal2.ISM_CHANGESERVERRECIEVEOK:
                                InterServerMsg.FrmSrvMsg.MsgGetUserChangeServerRecieveOk(snum, body);
                                break;
                            case Grobal2.ISM_USERLOGON:
                                InterServerMsg.FrmSrvMsg.MsgGetUserLogon(snum, body);
                                break;
                            case Grobal2.ISM_USERLOGOUT:
                                InterServerMsg.FrmSrvMsg.MsgGetUserLogout(snum, body);
                                break;
                            case Grobal2.ISM_WHISPER:
                                InterServerMsg.FrmSrvMsg.MsgGetWhisper(snum, body);
                                break;
                            case Grobal2.ISM_GMWHISPER:
                                InterServerMsg.FrmSrvMsg.MsgGetGMWhisper(snum, body);
                                break;
                            case Grobal2.ISM_LM_WHISPER:
                                InterServerMsg.FrmSrvMsg.MsgGetLoverWhisper(snum, body);
                                break;
                            case Grobal2.ISM_SYSOPMSG:
                                InterServerMsg.FrmSrvMsg.MsgGetSysopMsg(snum, body);
                                break;
                            case Grobal2.ISM_ADDGUILD:
                                InterServerMsg.FrmSrvMsg.MsgGetAddGuild(snum, body);
                                break;
                            case Grobal2.ISM_DELGUILD:
                                InterServerMsg.FrmSrvMsg.MsgGetDelGuild(snum, body);
                                break;
                            case Grobal2.ISM_RELOADGUILD:
                                InterServerMsg.FrmSrvMsg.MsgGetReloadGuild(snum, body);
                                break;
                            case Grobal2.ISM_GUILDMSG:
                                InterServerMsg.FrmSrvMsg.MsgGetGuildMsg(snum, body);
                                break;
                            case Grobal2.ISM_GUILDWAR:
                                InterServerMsg.FrmSrvMsg.MsgGetGuildWarInfo(snum, body);
                                break;
                            case Grobal2.ISM_CHATPROHIBITION:
                                InterServerMsg.FrmSrvMsg.MsgGetChatProhibition(snum, body);
                                break;
                            case Grobal2.ISM_CHATPROHIBITIONCANCEL:
                                InterServerMsg.FrmSrvMsg.MsgGetChatProhibitionCancel(snum, body);
                                break;
                            case Grobal2.ISM_CHANGECASTLEOWNER:
                                InterServerMsg.FrmSrvMsg.MsgGetChangeCastleOwner(snum, body);
                                break;
                            case Grobal2.ISM_RELOADCASTLEINFO:
                                InterServerMsg.FrmSrvMsg.MsgGetReloadCastleAttackers(snum);
                                break;
                            case Grobal2.ISM_RELOADADMIN:
                                InterServerMsg.FrmSrvMsg.MsgGetReloadAdmin();
                                break;
                            case Grobal2.ISM_MARKETOPEN:
                                InterServerMsg.FrmSrvMsg.MsgGetMarketOpen(true);
                                break;
                            case Grobal2.ISM_MARKETCLOSE:
                                InterServerMsg.FrmSrvMsg.MsgGetMarketOpen(false);
                                break;
                            case Grobal2.ISM_RELOADCHATLOG:
                                InterServerMsg.FrmSrvMsg.MsgGetReloadChatLog();
                                break;
                            case Grobal2.ISM_LM_DELETE:
                                InterServerMsg.FrmSrvMsg.MsgGetRelationshipDelete(snum, body);
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
                                InterServerMsg.FrmSrvMsg.MsgGetUserMgr(snum, body, ident);
                                break;
                            case Grobal2.ISM_RELOADMAKEITEMLIST:
                                // 제조 재료 목록 리로드(sonmg)
                                InterServerMsg.FrmSrvMsg.MsgGetReloadMakeItemList();
                                break;
                            case Grobal2.ISM_GUILDMEMBER_RECALL:
                                // 문원소환.
                                InterServerMsg.FrmSrvMsg.MsgGetGuildMemberRecall(snum, body);
                                break;
                            case Grobal2.ISM_RELOADGUILDAGIT:
                                InterServerMsg.FrmSrvMsg.MsgGetReloadGuildAgit(snum, body);
                                break;
                            case Grobal2.ISM_LM_LOGIN:
                                // 연인
                                InterServerMsg.FrmSrvMsg.MsgGetLoverLogin(snum, body);
                                break;
                            case Grobal2.ISM_LM_LOGOUT:
                                InterServerMsg.FrmSrvMsg.MsgGetLoverLogout(snum, body);
                                break;
                            case Grobal2.ISM_LM_LOGIN_REPLY:
                                InterServerMsg.FrmSrvMsg.MsgGetLoverLoginReply(snum, body);
                                break;
                            case Grobal2.ISM_LM_KILLED_MSG:
                                InterServerMsg.FrmSrvMsg.MsgGetLoverKilledMsg(snum, body);
                                break;
                            case Grobal2.ISM_RECALL:
                                // 소환
                                InterServerMsg.FrmSrvMsg.MsgGetRecall(snum, body);
                                break;
                            case Grobal2.ISM_REQUEST_RECALL:
                                InterServerMsg.FrmSrvMsg.MsgGetRequestRecall(snum, body);
                                break;
                            case Grobal2.ISM_REQUEST_LOVERRECALL:
                                InterServerMsg.FrmSrvMsg.MsgGetRequestLoverRecall(snum, body);
                                break;
                        }
                    }
                    else
                    {
                        break;
                    }
                }
                SocData = BufStr + SocData;
            }
            catch
            {
                svMain.MainOutMessage("[Exception] FrmIdSoc.DecodeSocStr");
            }
        }

        public void Run()
        {
            try
            {
                if (!MsgClient.Socket.Connected)
                {
                    if (GetTickCount - start > 20 * 1000)
                    {
                        start = GetTickCount;
                        MsgClient.Active = true;
                    }
                }
                DecodeSocStr();
            }
            catch
            {
                svMain.MainOutMessage("EXCEPT TFrmClient.Run");
            }
        }
    }
}

namespace GameSvr
{
    public class InterMsgClient
    {
        public static TFrmMsgClient FrmMsgClient = null;
    }
}