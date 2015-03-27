using UnityEngine;
using System.Collections;

public class PlatformOrbiter : MonoBehaviour {

	public float angle;
	public Transform point;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 vector = new Vector3 (0, 1, 0);
		transform.RotateAround (point.position, vector, angle);
	}
}
