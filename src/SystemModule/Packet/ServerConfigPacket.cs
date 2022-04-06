using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SystemModule.Packet
{
    public class ServerConfigPacket : Packets
    {
        public bool AutoSay;
        public byte[] Reserved;

        protected override void ReadPacket(BinaryReader reader)
        {
            throw new NotImplementedException();
        }

        protected override void WritePacket(BinaryWriter writer)
        {
            throw new NotImplementedException();
        }
    }
}
