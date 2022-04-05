namespace RobotSvr
{
    public struct SelChar
    {
        public bool Valid;
        public TUserCharacterInfo UserChr;
        public bool Selected;
        public bool FreezeState;
        public bool Unfreezing;
        public bool Freezing;
        public int AniIndex;
        public int DarkLevel;
        public int EffIndex;
        public long StartTime;
        public long Moretime;
        public long Startefftime;
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