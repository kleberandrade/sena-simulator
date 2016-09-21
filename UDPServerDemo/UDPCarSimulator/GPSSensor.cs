using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace UDPCarSimulator
{
    class GPSSensor
    {
        
        public int Port { get; set; }
        public bool IsDebug { get; set; }
        public double position;


         public GPSSensor()
        {
            this.IsDebug = false;
            this.position = 0.0;
        }



        public void Run()
        {
            Thread gps = new Thread(new ThreadStart(ReceiveData));
            gps.Start();

            if (IsDebug)
            {
                Thread gpsDebug = new Thread(new ThreadStart(Debug));
                 gpsDebug.Start();
            }
        }

        private void ReceiveData()
        {
            using (UdpClient server = new UdpClient(Port))
            {
                IPEndPoint sender = new IPEndPoint(IPAddress.Any, Port);
                while (true)
                {
                    byte[] data = new byte[1024];
                    data = server.Receive(ref sender);
                    string[] resultInfo = Encoding.UTF8.GetString(data, 0, data.Length).TrimEnd(' ').Split(' ');
                    position = double.Parse(resultInfo[0]);
                }
            }
        }

        private void Debug()
        {
            while (true)
            {
                Console.WriteLine("GPS " + " >> " + position);
            }
        }
    
    
    
    
    }
}
