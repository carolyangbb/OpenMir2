using System.Threading;

namespace GameSvr
{
    public static class MonitorExtension
    {
        public static void Enter(this object obj)
        {
            Monitor.Enter(obj);
        }

        public static void Leave(this object obj)
        { 
            Monitor.Exit(obj);
        }
    }
}

