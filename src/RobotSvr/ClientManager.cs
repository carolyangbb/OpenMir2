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
        private static IList<RobotClient> ClientList;
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
                    if (_Clients.ContainsKey(message.SessionId))
                    {
                        _Clients[message.SessionId].ProcessPacket(message.ReviceData);
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
            _Clients.TryAdd(sessionId, objClient);
        }

        public static void DelClient(string sessionId)
        {
            _Clients.TryRemove(sessionId, out RobotClient robotClient);
        }

        public static void Run()
        {
            dwRunTick = HUtil32.GetTickCount();
            var boProcessLimit = false;
            ClientList = _Clients.Values.ToList();
            for (var i = g_nPosition; i < _Clients.Count; i++)
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
            RunAutoPlay();
        }

        private static void RunAutoPlay()
        {
            AutoRunTick = HUtil32.GetTickCount();
            var boProcessLimit = false;
            if (ClientList.Count > 0)
            {
                for (var i = g_nPosition; i < _Clients.Count; i++)
                {
                    ClientList[i].RunAutoPlay();
                    if (((HUtil32.GetTickCount() - AutoRunTick) > 200))
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
            }
        }
    }

    public struct RecvicePacket
    {
        public string SessionId;
        public string ReviceData;
    }
}