using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Net.Sockets;
using System.Net;
// classe de recebimento das informações dos sensores do veículo
namespace UDPCarSimulator
{
    class VehicleInfo
    {
        public int Port { get; set; }
        private char[] gear = { 'R', 'N', '1', '2', '3', '4', '5', '6' };
        private int currentGear = 0;

        public int SteerAngle { get; set; }
        public int Velocity { get; set; }
        public bool IsDebug { get; set; }
        
        public char Cambio
        {
            get { return gear[currentGear]; }
        }

        public void Run()
        {
            Thread car = new Thread(new ThreadStart(ReceiveData));
            car.Name = "CarInfo";
            car.Start();

            if (IsDebug)
            {
                Thread carDebug = new Thread(new ThreadStart(Debug));
                carDebug.Start();
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
                    Velocity = int.Parse(resultInfo[0]);
                    SteerAngle = int.Parse(resultInfo[1]);
                    currentGear = int.Parse(resultInfo[2]);
                }
            }
        }

        private void Debug()
        {
            while (true)
            {
                Console.WriteLine("VEHICLE");
                Console.WriteLine("Velocity :" + Velocity );
                Console.WriteLine("SteerAngle: " + SteerAngle);
                Console.WriteLine("Cambio: " + Cambio);
                Console.WriteLine();
            }
        }
    }
}
