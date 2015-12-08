using System;
using System.Net;
using System.Net.Sockets;

namespace SocketListener
{
    class Program
    {
        private static Socket _socket;
        private static byte[] _buffer = new byte[4096];

        static void Main(string[] args)
        {
            String endpointIp = "192.168.1.2";

            _socket = new Socket(AddressFamily.InterNetwork, SocketType.Raw, ProtocolType.IP);

            // Bind socket
            _socket.Bind(new IPEndPoint(IPAddress.Parse(endpointIp), 0));

            // Socket options
            _socket.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.HeaderIncluded, true);

            // http://stackoverflow.com/a/9440355
            byte[] optionIn = new byte[4] { 1, 0, 0, 0 };
            byte[] optionOut = new byte[4] { 1, 0, 0, 0 }; //Capture outgoing packets
            _socket.IOControl(IOControlCode.ReceiveAll, optionIn, optionOut);

            //Start receiving the packets asynchronously
            _socket.BeginReceive(_buffer, 0, _buffer.Length, SocketFlags.None, new AsyncCallback(OnReceiveData), null);

            Console.WriteLine("Press Enter to exit");
            Console.ReadLine();
        }


        private static void OnReceiveData(IAsyncResult ar)
        {
            int received = _socket.EndReceive(ar);

            ParseData(_buffer, received);

            _buffer = new byte[4096];

            //Another call to BeginReceive so that we continue to receive the incoming packets
            _socket.BeginReceive(_buffer, 0, _buffer.Length, SocketFlags.None, new AsyncCallback(OnReceiveData), null);
        }

        private static void ParseData(byte[] byteData, int nReceived)
        {
            //Since all protocol packets are encapsulated in the IP datagram
            //so we start by parsing the IP header and see what protocol data
            //is being carried by it
            IPHeader ipHeader = new IPHeader(byteData, nReceived);

            //Now according to the protocol being carried by the IP datagram we parse 
            //the data field of the datagram
            switch (ipHeader.ProtocolType)
            {
                case Protocol.ICMP:

                    Icmp icmp = new Icmp(ipHeader.Data, ipHeader.MessageLength);

                    Console.WriteLine("ICMP : Type={0}, Code={1}",
                        icmp.Type,
                        icmp.Code);

                    break;

                case Protocol.TCP:

                    TCPHeader tcpHeader = new TCPHeader(ipHeader.Data, ipHeader.MessageLength);

                    //If the port is equal to 53 then the underlying protocol is DNS
                    //Note: DNS can use either TCP or UDP thats why the check is done twice
                    if (tcpHeader.DestinationPort == "53" || tcpHeader.SourcePort == "53")
                    {
                    }

                    Console.WriteLine("TCP : Flags={0}, HeaderLength={1}, MessageLength={2}, SequenceNumber={3}, SourcePort={4}, DestinationPort={5}",
                        tcpHeader.Flags,
                        tcpHeader.HeaderLength,
                        tcpHeader.MessageLength,
                        tcpHeader.SequenceNumber,
                        tcpHeader.SourcePort,
                        tcpHeader.DestinationPort);

                    break;

                case Protocol.UDP:

                    UDPHeader udpHeader = new UDPHeader(ipHeader.Data, (int)ipHeader.MessageLength);

                    //If the port is equal to 53 then the underlying protocol is DNS
                    //Note: DNS can use either TCP or UDP thats why the check is done twice
                    if (udpHeader.DestinationPort == "53" || udpHeader.SourcePort == "53")
                    {

                    }

                    Console.WriteLine("UDP : SourcePort={0}, DestinationPort={1}, Length={2},",
                        udpHeader.SourcePort,
                        udpHeader.DestinationPort,
                        udpHeader.Length);

                    break;

                case Protocol.Unknown:
                    break;
            }
        }
    }
}
