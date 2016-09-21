using UnityEngine;
using System.Collections;

[RequireComponent(typeof(UdpSender))]
public class CompassSensor : MonoBehaviour {

    public string gpsReferenceName = "GPS Reference";
    public bool hasNoise = true;
    public float systemError = 1.0f;

    private float heading;
    private float north = 0.0f;
    private Transform gpsReference;
   

	void Start () {
        Initialization();    
    }

    public float GetNorth { 
    
         get { return this.heading; }
    
    }

    void OnEnable()
    {
        Initialization();
    }

    void Initialization()
    {
        GameObject gps = GameObject.Find(gpsReferenceName);

        if (!gps){
            gps = new GameObject(gpsReferenceName);
            gps.transform.position = Vector3.zero;
            gps.transform.rotation = Quaternion.identity;
        }

        gpsReference = gps.transform;
        north = gpsReference.rotation.eulerAngles.y;
	}
	
	void Update () {
        heading = transform.rotation.eulerAngles.y - north + (hasNoise ? Noise() : 0.0f);
        if (heading < 0.0f)
            heading += 360;
        if (heading > 360.0f)
            heading -= 360.0f;
        SendMessage("SetData", heading.ToString());
	}

    float Noise()
    {
        return Random.Range(-systemError, systemError);
    }
}

