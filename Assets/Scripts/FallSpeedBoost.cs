using UnityEngine;
using System.Collections;

public class FallSpeedBoost : MonoBehaviour {
	[SerializeField]
	private float fallSpeed = 3.0f;
	private Rigidbody obj;

	public float speed;
	public float FallSpeed {
		get{
			return fallSpeed;
		}
	}

	void Start(){
		obj = this.GetComponent<Rigidbody> ();

	}
	void Update () {
		speed = obj.velocity.y;
		if (GetComponent<MechMain> ().Jumping == false || GetComponent<MechMain> ().Energy <= 0) {
						obj.AddForce (Vector3.down * Mathf.Lerp (fallSpeed/3.5f, fallSpeed, 0.2f), ForceMode.Acceleration);
				
		} else {

			if(speed < fallSpeed/1.5f){
				obj.AddForce (Vector3.up * (Mathf.Abs (speed)/fallSpeed *1.5f), ForceMode.Acceleration);
			}
		
		}

	}
}
