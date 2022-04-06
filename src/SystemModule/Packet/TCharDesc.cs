using System.IO;

namespace SystemModule
{
    public class TCharDesc : Packets
    {
        public int Feature;
        public int Status;

        protected override void ReadPacket(BinaryReader reader)
        {
            Feature = reader.ReadInt32();
            Status = reader.ReadInt32();
        }

        protected override void WritePacket(BinaryWriter writer)
        {
            writer.Write(Feature);
            writer.Write(Status);
        }
    }
}

