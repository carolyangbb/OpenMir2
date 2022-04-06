namespace RobotSvr
{
    public class PlayScn
    {
        public static long GO = 0;
        public static long GW = 0;
        public static int GProcDrawChrPos = 0;
        public static long GProcDrawChr = 0;
        public static int GNEffectAction = 0;
        public const int LongheightImage = 35;
        public const int Flashbase = 410;
        public const int Soffx = 0;
        public const int Soffy = 0;
        public const int Lmx = 30;
        public const int Lmy = 26;
        public const int Maxlight = 5;

        public static bool IsMySlaveObject(TActor atc)
        {
            bool result;
            int i;
            result = false;
            if (MShare.g_MySelf == null)
            {
                return result;
            }
            for (i = 0; i < MShare.g_MySelf.m_SlaveObject.Count; i++)
            {
                if (atc == MShare.g_MySelf.m_SlaveObject[i])
                {
                    result = true;
                    break;
                }
            }
            return result;
        }
    }
}

