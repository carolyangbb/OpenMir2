namespace RobotSvr
{
    public struct SelChar
    {
        public bool Valid;
        public TUserCharacterInfo UserChr;
        public bool Selected;
        public bool FreezeState;
        public bool Freezing;
        public long StartTime;
    }

    public class TUserCharacterInfo
    {
        public string Name;
        public byte Job;
        public byte hair;
        public ushort Level;
        public byte Sex;
    }
}