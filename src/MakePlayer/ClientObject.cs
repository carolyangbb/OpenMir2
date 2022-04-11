namespace MakePlayer
{
    public struct TUserCharacterInfo
    {
        public string sName;
        public byte btJob;
        public byte btHair;
        public ushort wLevel;
        public byte btSex;
    }

    public struct TSelChar
    {
        public bool boValid;
        public TUserCharacterInfo UserChr;
    }

    public enum TConnectionStep
    {
        cnsConnect,
        cnsNewAccount,
        cnsQueryServer,
        cnsSelServer,
        cnsLogin,
        cnsNewChr,
        cnsQueryChr,
        cnsSelChr,
        cnsReSelChr,
        cnsPlay
    }

    public enum TConnectionStatus
    {
        cns_Success,
        cns_Failure
    }
}

