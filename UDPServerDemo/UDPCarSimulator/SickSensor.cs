using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Net.Sockets;
using System.Net;

namespace UDPCarSimulator
{
    class SickSensor
    {
        private const int DATA_LENGTH = 181;
        private string name = string.Empty;
        public int Port { get; set; }
        public bool IsDebug { get; set; }
        public double[] distances;

        public SickSensor(string name)
        {
            this.IsDebug = false;
            this.name = name.ToUpper();
            this.distances = new double[DATA_LENGTH];
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

                    for (int i = 0; i < resultInfo.Length; i++)
                    {
                        distances[i] = double.Parse(resultInfo[i], System.Globalization.NumberStyles.AllowDecimalPoint, System.Globalization.NumberFormatInfo.InvariantInfo);
                    }
                }
            }
        }

        private void Debug()
        {
            while (true)
            {
                Console.Write("SICK " + name + " >>");
                foreach (double d in distances)
                {
                    Console.Write(" " + d);
                }
               Console.Read();
                Console.WriteLine();

                
            }
        }
    }
}
