namespace RobotSvr
{
    public class PlayScn
    {
        public static bool IsMySlaveObject(TActor atc)
        {
            var result = false;
            if (MShare.g_MySelf == null)
            {
                return result;
            }
            for (var i = 0; i < MShare.g_MySelf.m_SlaveObject.Count; i++)
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

