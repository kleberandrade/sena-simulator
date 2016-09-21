using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Net.Sockets;
using System.Net;

namespace UDPCarSimulator
{
    class CompassSensor
    {
        private string name = string.Empty;
        public int Port { get; set; }
        public bool IsDebug { get; set; }
        public double north;

        public CompassSensor(string name)
        {
            this.IsDebug = false;
            this.name = name.ToUpper();
            this.north = 0.0;
        }

        public void Run()
        {
            Thread sick = new Thread(new ThreadStart(ReceiveData));
            sick.Name = name;
            sick.Start();

            if (IsDebug)
            {
                Thread sickDebug = new Thread(new ThreadStart(Debug));
                sickDebug.Start();
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
                    north = double.Parse(resultInfo[0], System.Globalization.NumberStyles.AllowDecimalPoint, System.Globalization.NumberFormatInfo.InvariantInfo);
                }
            }
        }

        private void Debug()
        {
            while (true)
            {
                Console.WriteLine("COMPASS " + name + " >> " + north);
            }
        }
    }
}
