using UnityEngine;
using System.Collections;
using System.Text;
using System.Collections.Generic;
using System.Threading;

[RequireComponent(typeof(UdpSender))]
public class Scanner : MonoBehaviour {
	public float range = 80;
	public float angle = 180;
	public float angularResolution = 1;
    public float frequence = 75;
	public bool hasNoise = true;
	public float systemError = 0.04f;
	
	public Color color = Color.blue;
	public bool showDebugRay = true;
	private int numLines;
	private RaycastHit hit;

    private float distance;
    private float error;
    private StringBuilder builder;
	
	void Start() {
		numLines = (int)(angle / angularResolution) + 1;
	}
	
	void FixedUpdate() {
		range = Mathf.Clamp(range, 0.01f, range);
		angle = Mathf.Clamp(angle, 0.0f, angle);
		angularResolution = Mathf.Clamp(angularResolution, 0.01f, angle * 0.5f);
        numLines = (int)(angle / angularResolution) + 1;
    }

    void Update() 
    {
        builder = new StringBuilder();
        for (int index = 0; index < numLines; index++)
        {
            error = (hasNoise ? Noise() : 0.0f);
            distance = range + error;

            Vector3 ray = transform.rotation * Quaternion.AngleAxis(angle * 0.5f + (-1 * index * angularResolution), Vector3.up) * Vector3.forward;
            if (Physics.Raycast(transform.position, ray, out hit, range))
                distance = hit.distance + error;

            if (showDebugRay)
                Debug.DrawRay(transform.position, ray * distance, color);

            builder.AppendFormat("{0:0.000} ", distance);
        }
        SendMessage("SetData", builder.ToString());
	}

	float Noise() {
		return Random.Range(-systemError, systemError);
	}
}
