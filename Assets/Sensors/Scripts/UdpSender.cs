using UnityEngine;
using System.Collections;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

public class UdpSender : MonoBehaviour {

    public int port = 12324;
    public string ipAddress = "127.0.0.1";
    public float frequence = 0.0f;

    private string data = string.Empty;
    private byte[] buffer;
    private Thread sender;

    private bool isSending = true;

    public void SetData(string data)
    {
        this.data = data;
    }

    void Sender()
    {
        using (UdpClient client = new UdpClient(ipAddress, port)){
            while (isSending)
            {
                buffer = Encoding.ASCII.GetBytes(data);
                client.Send(buffer, buffer.Length);
                Thread.Sleep((int)(1.0f / frequence * 1000));
            }
        }
    }

    IEnumerator SenderStart()
    {
        ThreadStart senderStart = new ThreadStart(Sender);
        sender = new Thread(senderStart);
        sender.IsBackground = true;
        sender.Start();
        yield return null;
    }

    void Start()
    {
        isSending = true;
        StartCoroutine("SenderStart");
    }

    void OnDestroy()
    {
        Exit();
    }

    void Exit()
    {
        isSending = false;
        if (sender.IsAlive)
            sender.Abort();

    }

    void OnApplicationQuit()
    {
        Exit();
    }
}