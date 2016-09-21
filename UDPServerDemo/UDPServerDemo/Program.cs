using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace UDPServerDemo
{
    class Program
    {
        static UdpClient server;
        static int port = 12323;

        static void Main(string[] args)
        {
            server = new UdpClient(port);
            Console.WriteLine("UDP Server");
            while (true)
            {
                receber();
            }
        }

        static void receber()
        {
            IPEndPoint sender = new IPEndPoint(IPAddress.Any, port);
            byte[] data = new byte[1024];
            data = server.Receive(ref sender);
            String info = Encoding.ASCII.GetString(data, 0, data.Length);
            Console.WriteLine(">> " + info);
        }
    }
}