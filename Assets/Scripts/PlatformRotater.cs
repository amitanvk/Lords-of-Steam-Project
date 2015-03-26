using UnityEngine;
using System.Collections;

public class PlatformRotater : MonoBehaviour {

	// Use this for initialization
	public float speed;
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 vector = new Vector3 (0, 1, 0);
		transform.Rotate (vector, speed);
	}
}
