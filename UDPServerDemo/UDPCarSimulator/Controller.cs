using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.Threading;
// script de controle via rede
namespace UDPCarSimulator
{
    
    class Controller
    {
        #region [ Ports ]
        private const int SENDER_PORT = 12301;
        private const int VEHICLE_INFO_PORT = 12300;
        private const int FRONTAL_SICK_PORT = 12320;
        private const int BACK_SICK_PORT = 12321;
        private const int TOP_SICK_PORT = 12322;
        private const int COMPASS_PORT = 12323;
        private const int GPS_PORT = 12324;
        private const string ipServer = "127.0.0.1";

        #endregion

        #region [ Sensors ]
        private SickSensor frontSick;
        private SickSensor topSick;
        private SickSensor backSick;
        private CompassSensor compassSensor;
        private VehicleInfo vehicleInfo;
        private GPSSensor gpsSensor;
        #endregion

        #region [ Constructor ]
        public Controller()
        {
            Receiver();
            Thread tSend = new Thread(new ThreadStart(Sender));
            tSend.Start();
            
        }
        #endregion

        #region [ Receiver ]
        private void Receiver()
        {
            frontSick = new SickSensor("Front");
            frontSick.Port = FRONTAL_SICK_PORT;
            frontSick.IsDebug = true;
            frontSick.Run();

            topSick = new SickSensor("Top");
            topSick.Port = TOP_SICK_PORT;
            topSick.IsDebug = false;
            topSick.Run();

            backSick = new SickSensor("Back");
            backSick.Port = BACK_SICK_PORT;
            backSick.IsDebug = false;
            backSick.Run();

            compassSensor = new CompassSensor("Compass");
            compassSensor.Port = COMPASS_PORT;
            compassSensor.IsDebug = false;
            compassSensor.Run();

            vehicleInfo = new VehicleInfo();
            vehicleInfo.Port = VEHICLE_INFO_PORT;
            vehicleInfo.IsDebug = false;
            vehicleInfo.Run();

            gpsSensor = new GPSSensor();
            gpsSensor.Port = GPS_PORT;
            gpsSensor.IsDebug = false;
            gpsSensor.Run();
        }
        #endregion

        #region [ Sender ]
        private void Sender()
        {
            using (UdpClient client = new UdpClient(ipServer, SENDER_PORT))
            {
                while (true)
                {
                    byte[] data = new byte[1024];
                    data = Encoding.ASCII.GetBytes("");
                    client.Send(data, data.Length);
                }
            }
        }
        #endregion

        #region [ Run Controller ]
        public void Run(){
            while (true)
            {

               // Console.WriteLine("Tomando decisões");
                
            }
        }
        #endregion
    }
}
