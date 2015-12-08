using System.IO;

namespace SocketListener
{
    public class Icmp
    {
        private readonly byte _icmpType;
        private readonly byte _icmpSubType;

        public Icmp(byte[] byBuffer, int nReceived)
        {
            // https://en.wikipedia.org/wiki/Internet_Control_Message_Protocol

            MemoryStream memoryStream = new MemoryStream(byBuffer, 0, nReceived);
            BinaryReader binaryReader = new BinaryReader(memoryStream);

            _icmpType = binaryReader.ReadByte();
            _icmpSubType = binaryReader.ReadByte();
        }

        public byte Code { get { return _icmpSubType; } }

        public IcmpType Type
        {
            get
            {
                return (IcmpType)_icmpType;
            }
        }
    }
}
