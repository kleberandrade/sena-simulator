using UnityEngine;
using System.Collections;

[RequireComponent(typeof(UdpSender))]
/*public class GPS : MonoBehaviour
{
    private GameObject gpsGO = null;
    private CompassSensor gpsRef = null;
    private double easting;
    private double northing;
    private int zone;

    private double lat;
    private double lon;
    private ArrayList latlon_meters;

    public double updateFreq = 1.0;
    private double timer = 0.0;
    private bool useLatLon = true;


    // Use this for initialization
    void Start()
    {
        gpsGO = GameObject.Find("GPS Reference");
        gpsRef = (CompassSensor)gpsGO.GetComponent("gpsReferenceName");
        latlon_meters = FindMetersPerLat((float)gpsRef.GetNorth);
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        //easting = gpsRef.easting + gpsGO.transform.InverseTransformPoint(transform.position).x; //Calc current easting
        //northing = gpsRef.northing + gpsGO.transform.InverseTransformPoint(transform.position).z; //Calc current northing

        //lat = gpsRef.lat + (gpsGO.transform.InverseTransformPoint(transform.position).z) / (double)latlon_meters[0]; //Calc current lat
        //lon = gpsRef.lon + (gpsGO.transform.InverseTransformPoint(transform.position).x) / (double)latlon_meters[1]; //Calc current lon

        latlon_meters = FindMetersPerLat((float)lat); //Update meters calc based on new coords

        string sendString = "";
        if (useLatLon == false) sendString += easting + "E  " + northing + "N";
        else sendString += string.Format("{0:0#.#####}N {1:0#.#####}W", lat, lon);


        if (timer > (1.0 / updateFreq))
        {
            Send(sendString);
            timer = 0;
        }
        //		Debug.Log(sendString);
    }

    

    void Send(string data)
    {
        //if (Server.Connected())
        //{
          //  string sendString = gameObject.transform.root.name + "," + gameObject.name + "," + data + "\n";
            //Server.PutMessage(sendString);
        //}
    }


    //This function is a modified version of the JavaScript found at http://www.csgnetwork.com/degreelenllavcalc.html (C) CSGNetwork.COM and Computer Support Group	

    ArrayList FindMetersPerLat(float lat) // Compute lengths of degrees
    {
        // Set up "Constants"
        double m1 = 111132.92;		// latitude calculation term 1
        double m2 = -559.82;		// latitude calculation term 2
        double m3 = 1.175;			// latitude calculation term 3
        double m4 = -0.0023;		// latitude calculation term 4
        double p1 = 111412.84;		// longitude calculation term 1
        double p2 = -93.5;			// longitude calculation term 2
        double p3 = 0.118;			// longitude calculation term 3
        double latlen = 0.0;
        double lonlen = 0.0;
        // Convert latitude to radians

        //lat = Mathf.Deg2Rad(lat);
        lat = (float)((lat * Mathf.PI) / 180.0);

        // Calculate the length of a degree of latitude and longitude in meters

        latlen = m1 + (m2 * Mathf.Cos(2 * lat)) + (m3 * Mathf.Cos(4 * lat)) + (m4 * Mathf.Cos(6 * lat));

        lonlen = (p1 * Mathf.Cos(lat)) + (p2 * Mathf.Cos(3 * lat)) + (p3 * Mathf.Cos(5 * lat));

        ArrayList retval = new ArrayList();
        retval.Add(latlen);
        retval.Add(lonlen);

        return retval;
    }


}*/


public class GPS : MonoBehaviour
{

    private float longitude;
    private float latitude;
    private float north;
    public CompassSensor cps;
    private ArrayList latlon_meters;

    public double GetLAT {

        get { return lat; }
    
    }

    public double GetLON {


        get { return lon; }
    }
    
    
    /*
    // Use this for initialization
    void Start () {

        north = cps.GetNorth;
        latlon_meters = FindMetersPerLat(latitude);
    }


    ArrayList FindMetersPerLat(float lat) // Armazena os valores dos angulos
    {
        // Set up "Constants"
        float m1 = 111132.92f;		// latitude calculation term 1
        float m2 = -559.82f;		// latitude calculation term 2
        float m3 = 1.175f;			// latitude calculation term 3
        float m4 = -0.0023f;		// latitude calculation term 4

        float p1 = 111412.84f;		// longitude calculation term 1
        float p2 = -93.5f;			// longitude calculation term 2
        float p3 = 0.118f;			// longitude calculation term 3
        float latlen = 0.0f;
        float lonlen = 0.0f;
        // Convert latitude to radians

        //lat = Mathf.Deg2Rad(lat);
        lat = (float)((lat * Mathf.PI) / 180.0);

        // Calculate the length of a degree of latitude and longitude in meters

        latlen = m1 + (m2 * Mathf.Cos(2 * lat)) + (m3 * Mathf.Cos(4 * lat)) + (m4 * Mathf.Cos(6 * lat));

        lonlen = (p1 * Mathf.Cos(lat)) + (p2 * Mathf.Cos(3 * lat)) + (p3 * Mathf.Cos(5 * lat));

        ArrayList retval = new ArrayList();
        retval.Add(latlen);
        retval.Add(lonlen);

        return retval;
    }




	
    // Update is called once per frame
    void Update () {

     
        latitude = latitude + (transform.InverseTransformPoint(transform.position).z) / (float)latlon_meters[0];
        longitude = longitude + (transform.InverseTransformPoint(transform.position).x) / (float)latlon_meters[1];
        latlon_meters = FindMetersPerLat(latitude);

        for (int i = 0; i < latlon_meters.Count; i++)
        {
            Debug.Log("Latitude Longitude: " + latlon_meters[i]);
        }
        SendMessage("SetData", latlon_meters.ToString());
    
    
    
    }
}*/ // termino do asterisco barra




    //  private GameObject gpsGO = null;
    // private GPSCoord gpsRef = null;
    private double easting;
    private double northing;
    private int zone;
    private string sendString = "";
    private double lat;
    private double lon;
    //   private ArrayList latlon_meters;

   // public double updateFreq = 1.0;
    private double timer = 0.0;
    private bool useLatLon = true;


    // Use this for initialization
    void Start()
    {

        latlon_meters = FindMetersPerLat(latitude);
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        //easting = gpsRef.easting + transform.InverseTransformPoint(transform.position).x; //Calc current easting
        northing = cps.GetNorth + transform.InverseTransformPoint(transform.position).z; //Calc current northing

        lat = latitude + (transform.InverseTransformPoint(transform.position).z) / (double)latlon_meters[0]; //Calc current lat
        lon = longitude + (transform.InverseTransformPoint(transform.position).x) / (double)latlon_meters[1]; //Calc current lon

        latlon_meters = FindMetersPerLat((float)lat); //Update meters calc based on new coords

        SendMessage("SetData", lat.ToString());
       
        if (useLatLon == false) sendString += easting + "E  " + northing + "N";
        else sendString += string.Format("{0:0#.#####}N {1:0#.#####}W", lat, lon);

                    

      //  if (timer > (1.0 / updateFreq))
       // {
         //   Send(sendString);
           // timer = 0;
       // }
        		//Debug.Log("LAT: " + lat + ", " + "LON: " + lon);
               
    }

    //  void Send(string dataPosX){
    //    if(Server.Connected()){
    //      string sendString = gameObject.transform.root.name + "," + gameObject.name + "," + dataPosX + "\n";
    //    Server.PutMessage(sendString);
    //}
    //}


    //This function is a modified version of the JavaScript found at http://www.csgnetwork.com/degreelenllavcalc.html (C) CSGNetwork.COM and Computer Support Group	

    ArrayList FindMetersPerLat(float lat) // Compute lengths of degrees
    {
        // Set up "Constants"
        double m1 = 111132.92;		// latitude calculation term 1
        double m2 = -559.82;		// latitude calculation term 2
        double m3 = 1.175;			// latitude calculation term 3
        double m4 = -0.0023;		// latitude calculation term 4
        double p1 = 111412.84;		// longitude calculation term 1
        double p2 = -93.5;			// longitude calculation term 2
        double p3 = 0.118;			// longitude calculation term 3
        double latlen = 0.0;
        double lonlen = 0.0;
        // Convert latitude to radians

        //lat = Mathf.Deg2Rad(lat);
        lat = (float)((lat * Mathf.PI) / 180.0);

        // Calculate the length of a degree of latitude and longitude in meters

        latlen = m1 + (m2 * Mathf.Cos(2 * lat)) + (m3 * Mathf.Cos(4 * lat)) + (m4 * Mathf.Cos(6 * lat));

        lonlen = (p1 * Mathf.Cos(lat)) + (p2 * Mathf.Cos(3 * lat)) + (p3 * Mathf.Cos(5 * lat));

        ArrayList retval = new ArrayList();
        retval.Add(latlen);
        retval.Add(lonlen);

        return retval;
    }
}
 
 
 
 
 
 
 
 