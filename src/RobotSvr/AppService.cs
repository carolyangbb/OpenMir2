using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;
using SystemModule;

namespace RobotSvr
{
    public class AppService : BackgroundService
    {
        /// <summary>
        /// 服务器名称
        /// </summary>
        private static string g_sServerName = string.Empty;
        /// <summary>
        /// 游戏服务器IP地址
        /// </summary>
        private static string g_sGameIPaddr = string.Empty;
        /// <summary>
        /// 服务器端口号
        /// </summary>
        private static int g_nGamePort = 0;
        /// <summary>
        /// 账号前缀
        /// </summary>
        private static string g_sAccount = string.Empty;
        /// <summary>
        /// 同时登录人数
        /// </summary>
        private static int g_nChrCount = 1;
        /// <summary>
        /// 登录总人数
        /// </summary>
        private static int g_nTotalChrCount = 1;
        /// <summary>
        /// 是否创建帐号
        /// </summary>
        private static bool g_boNewAccount = false;
        /// <summary>
        /// 登录序号
        /// </summary>
        private static int g_nLoginIndex = 0;
        /// <summary>
        /// 登录间隔
        /// </summary>
        private static long g_dwLogonTick = 0;

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            MShare.g_sGameIPaddr = g_sGameIPaddr;
            MShare.g_nGamePort = g_nGamePort;
            ClientManager.Start();
            Start();
            return Task.CompletedTask;
        }

        private void Start()
        {
            while (true)
            {
                if (g_nTotalChrCount > 0)
                {
                    if (((HUtil32.GetTickCount() - g_dwLogonTick) > 1000 * g_nChrCount))
                    {
                        g_dwLogonTick = HUtil32.GetTickCount();
                        if (g_nTotalChrCount >= g_nChrCount)
                        {
                            g_nTotalChrCount -= g_nChrCount;
                        }
                        else
                        {
                            g_nTotalChrCount = 0;
                        }
                        for (var i = 0; i < g_nChrCount; i++)
                        {
                            var playClient = new RobotClient();
                            playClient.SessionId = Guid.NewGuid().ToString("N");
                            playClient.m_boNewAccount = g_boNewAccount;
                            playClient.LoginID = string.Concat(g_sAccount, g_nLoginIndex);
                            playClient.LoginPasswd = playClient.LoginID;
                            playClient.m_sCharName = playClient.LoginID;
                            playClient.m_sServerName = g_sServerName;
                            playClient.m_dwConnectTick = HUtil32.GetTickCount() + (i + 1) * 3000;
                            ClientManager.AddClient(playClient.SessionId, playClient);
                            g_nLoginIndex++;
                        }
                    }
                }
                ClientManager.Run();
                Thread.Sleep(TimeSpan.FromMilliseconds(50));
            }
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            g_sServerName = "热血传奇";
            g_sGameIPaddr = "10.10.0.110";
            g_nGamePort = 7000;
            g_boNewAccount = false;
            g_nChrCount = HUtil32._MIN(g_nChrCount, g_nTotalChrCount);
            g_dwLogonTick = HUtil32.GetTickCount() - 1000 * g_nChrCount;
            g_sAccount = "rotbot";
            return base.StartAsync(cancellationToken);
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            return base.StopAsync(cancellationToken);
        }
    }
}
