using UnityEngine;
using System.Collections;

public class FallSpeedBoost : MonoBehaviour {
	[SerializeField]
	private float fallSpeed = 3.0f;

	void Start(){

	}
	void Update () {
		GetComponent<Rigidbody>().AddForce(Vector3.down*fallSpeed);
	}
}
