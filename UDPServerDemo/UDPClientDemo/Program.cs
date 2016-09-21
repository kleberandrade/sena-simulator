using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace UDPClientDemo
{
    class Program
    {
        static string msg;
        static byte[] data;
        static UdpClient client;

        static void Main(string[] args)
        {
            client = new UdpClient("192.168.200.245", 12321);
            Console.WriteLine("UDP Client");
            while (true)
            {
                Console.WriteLine(">> ");
                msg = Console.ReadLine();
                enviar();
            }
        }

        static void enviar()
        {
            data = Encoding.ASCII.GetBytes(msg);
            client.Send(data, data.Length);

        }
    }
}


/*

static void Main(string[] args)
        {
            Header();
            UdpClient client = new UdpClient();
            client.ExclusiveAddressUse = false;
            client.AllowNatTraversal(true);
            IPEndPoint localEp = new IPEndPoint(IPAddress.Any, 2222);


            client.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
            client.ExclusiveAddressUse = false;

            client.Client.Bind(localEp);

            IPAddress multicastaddress = IPAddress.Parse("239.0.0.222");
            client.JoinMulticastGroup(multicastaddress);

            Console.WriteLine("Listening this will never quit so you will need to ctrl-c it");

            while (true)
            {
                Byte[] data = client.Receive(ref localEp);
                string strData = Encoding.Unicode.GetString(data);
                Console.WriteLine(strData);
            }
        }
*/