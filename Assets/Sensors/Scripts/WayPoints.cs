using UnityEngine;
using System.Collections;
using System.Threading;
using System.Text;
using System.Net.Sockets;

[RequireComponent(typeof(UdpSender))]
public class WayPoints : MonoBehaviour {

    private static float x = 0.0f, y = 0.0f, z = 0.0f;
    public float frequence = 0.0f;
    private UdpClient udpclient;
    //private float[] position = {x,y,z};
    public Transform car;
    private Thread t,t2;
    private string dataPosX = string.Empty;
    private string dataPosZ = string.Empty;
    private byte[] buffer, buffer2;
    private bool arrived;
	// Use this for initialization
	
    void Start () {

        arrived = false;
       
       x = this.transform.localPosition.x;
       y = this.transform.localPosition.y;
       z = this.transform.localPosition.z;

      // udpclient = new UdpClient("127.0.0.1", 12340);

       dataPosX = x.ToString();
       dataPosZ = z.ToString();

       StartCoroutine("SendMessages");
	}



    
    void OnTriggerEnter(Collider hit){

        if (hit.gameObject)
        {
            arrived = true;
            Debug.Log("Cheguei");
            StopAllCoroutines();
            
            

        }
        else {
            arrived = false;
            //Debug.Log("não cheguei");
        
        }
    }

   
   void SenderPosX()
    {
        
        while (true)
        {
            // Debug.Log("não cheguei"); 
            buffer = Encoding.ASCII.GetBytes(dataPosX);
            udpclient.Send(buffer, buffer.Length);
            Thread.Sleep((int)(1.0f / frequence * 1000));
           
        }
    }

   void SenderPosZ()
   {

       while (true)
       {
           buffer2 = Encoding.ASCII.GetBytes(dataPosZ);
           udpclient.Send(buffer2, buffer2.Length);
           Thread.Sleep((int)(1.0f / frequence * 1000));
          // Debug.Log("não cheguei");
       }
   }


    IEnumerator SendMessages()
    {
        t = new Thread(new ThreadStart(SenderPosX));
        t.IsBackground = true;
        t.Start();
        
        t2 = new Thread(new ThreadStart(SenderPosZ));
        t2.IsBackground = true;
        t2.Start();
        yield return null;
    }

    void Update() {

        if (arrived.Equals(false))
        {
            Debug.Log("não cheguei");
            SendMessage("SetData", dataPosX);
            SendMessage("SetData", dataPosZ);
        }
        else {


            arrived = true;
        }
        
    }


}
