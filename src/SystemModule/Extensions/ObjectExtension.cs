namespace GameSvr
{
    public static class ObjectExtension
    {
        public static void Free(this object obj)
        {
            if (obj != null)
            {
                obj = null;
            }
        }
    }
}

