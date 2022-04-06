using System.IO;

namespace SystemModule
{
    public class TClientMagic : Packets
    {
        public char Key;
        public byte Level;
        public int CurTrain;
        public TMagic Def;

        public TClientMagic()
        {
            Def = new TMagic();
        }

        protected override void ReadPacket(BinaryReader reader)
        {
            Key = reader.ReadChar();
            Level = reader.ReadByte();
            reader.ReadByte();
            reader.ReadByte();
            CurTrain = reader.ReadInt32();
            Def = ToPacket<TMagic>(reader);
        }

        protected override void WritePacket(BinaryWriter writer)
        {
            writer.Write(Key);
            writer.Write(Level);
            writer.Write((byte)0);
            writer.Write((byte)0);
            writer.Write(CurTrain);
            writer.Write(Def.GetBuffer());
        }
    }
}
