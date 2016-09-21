using UnityEngine;
using System.Collections;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

public class UdpReceiver : MonoBehaviour {
	
	public int port = 12301;
    private byte[] buffer;
    private Thread receiver;
    public string Data { get; set; }
    private bool isReceiving = true;
    private UdpClient server;

    IEnumerator ReceiverStart()
    {
        receiver = new Thread(new ThreadStart(Receiver));
        receiver.IsBackground = true;
        receiver.Start();
        yield return null;
    }

    void Receiver()
    {  
        IPEndPoint receiver = new IPEndPoint(IPAddress.Any, port);
        while (isReceiving)
        {
            byte[] buffer = new byte[1024];
            buffer = server.Receive(ref receiver);
            Data = Encoding.ASCII.GetString(buffer, 0, buffer.Length);
        }
        Debug.Log("Receiver finalizado!");
    }

    void Start()
    {
        server = new UdpClient(port);
        isReceiving = true;
        Data = string.Empty;
        StartCoroutine("ReceiverStart");
    }

    void Exit()
    {
        if (server != null)
        {
            server.Close();
        }
        isReceiving = false;
        if (receiver != null)
        {
            if (receiver.IsAlive)
            {
                receiver.Abort();
            }
        }
    }

    void OnDestroy()
    {
        Exit();
    }

    void OnApplicationQuit()
    {
        Exit();
    }

}
