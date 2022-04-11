using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Channels;
using System.Threading.Tasks;
using SystemModule;

namespace RobotSvr
{
    public static class ClientManager
    {
        private static readonly ConcurrentDictionary<string, RobotClient> _Clients;
        private static IList<AutoPlayRunTime> AutoList;
        private static int g_dwProcessTimeMin = 0;
        private static int g_dwProcessTimeMax = 0;
        private static int g_nPosition = 0;
        private static int dwRunTick = 0;
        private static int AutoRunTick = 0;
        private static readonly Channel<RecvicePacket> _reviceMsgList;
        private static readonly object _lock = new object();

        static ClientManager()
        {
            _Clients = new ConcurrentDictionary<string, RobotClient>();
            _reviceMsgList = Channel.CreateUnbounded<RecvicePacket>();
            AutoList = new List<AutoPlayRunTime>();
        }

        public static void Start()
        {
            Task.Run(() =>
            {
                Task.Factory.StartNew(ProcessReviceMessage);
            });
        }

        private static async Task ProcessReviceMessage()
        {
            while (await _reviceMsgList.Reader.WaitToReadAsync())
            {
                if (_reviceMsgList.Reader.TryRead(out var message))
                {
                    try
                    {
                        if (_Clients.ContainsKey(message.SessionId))
                        {
                            _Clients[message.SessionId].ProcessPacket(message.ReviceData);
                        }
                    }
                    catch (System.Exception ex)
                    {
                        System.Console.WriteLine(ex.StackTrace);
                    }
                }
            }
        }

        public static void AddPacket(string sessionId, string reviceData)
        {
            var clientPacket = new RecvicePacket();
            clientPacket.SessionId = sessionId;
            clientPacket.ReviceData = reviceData;
            _reviceMsgList.Writer.TryWrite(clientPacket);
        }

        public static void AddClient(string sessionId, RobotClient objClient)
        {
            AutoList.Add(new AutoPlayRunTime()
            {
                SessionId = sessionId,
                RunTick = HUtil32.GetTickCount()
            });
            _Clients.TryAdd(sessionId, objClient);
        }

        public static void DelClient(string sessionId)
        {
            AutoPlayRunTime findSession = null;
            foreach (var item in AutoList)
            {
                if (item.SessionId == sessionId)
                {
                    findSession = item;
                    break;
                }
            }
            if (findSession != null)
            {
                AutoList.Remove(findSession);
            }
            _Clients.TryRemove(sessionId, out RobotClient robotClient);
        }

        public static void Run()
        {
            dwRunTick = HUtil32.GetTickCount();
            var boProcessLimit = false;
            var ClientList = _Clients.Values.ToList();
            for (var i = g_nPosition; i < ClientList.Count; i++)
            {
                ClientList[i].Run();
                if (((HUtil32.GetTickCount() - dwRunTick) > 20))
                {
                    g_nPosition = i;
                    boProcessLimit = true;
                    break;
                }
            }
            if (!boProcessLimit)
            {
                g_nPosition = 0;
            }
            g_dwProcessTimeMin = HUtil32.GetTickCount() - dwRunTick;
            if (g_dwProcessTimeMin > g_dwProcessTimeMax)
            {
                g_dwProcessTimeMax = g_dwProcessTimeMin;
            }
            RunAutoPlay(ClientList);
        }

        private static void RunAutoPlay(IList<RobotClient> ClientList)
        {
            AutoRunTick = HUtil32.GetTickCount();
            if (AutoList.Count > 0)
            {
                for (var i = 0; i < AutoList.Count; i++)
                {
                    if ((AutoRunTick - AutoList[i].RunTick) > 600)
                    {
                        AutoList[i].RunTick = HUtil32.GetTickCount();
                        ClientList[i].RunAutoPlay();
                    }
                }
            }
        }
    }

    public struct RecvicePacket
    {
        public string SessionId;
        public string ReviceData;
    }

    public class AutoPlayRunTime
    {
        public string SessionId;
        public int RunTick;
    }
}