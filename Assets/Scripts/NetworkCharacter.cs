using UnityEngine;
using System.Collections;

public class NetworkCharacter : Photon.MonoBehaviour {

	Vector3 realPosition = Vector3.zero;
	Quaternion realRotation = Quaternion.identity;
	double LastNetworkDataRecievedTime;
	float speed = 0.0f;
	public bool gotFirstUpdate = false;
	public float SmoothingDelay = 2;


	void Start () {

	}

	// Update is called once per frame

	void FixedUpdate () {
		if( photonView.isMine ) {
			// Do nothing
		}
		else {
			UpdateNetworkedPosition ();

			//transform.position = Vector3.Lerp(transform.position, realPosition, Time.deltaTime * this.SmoothingDelay);

			transform.rotation = Quaternion.Lerp(transform.rotation, realRotation, Time.deltaTime * this.SmoothingDelay);
		}

	}
	void UpdateNetworkedPosition(){
		float pingInSeconds = (float)PhotonNetwork.GetPing () * 0.001f;
		float timeSinceLastUpdate = (float)(PhotonNetwork.time - LastNetworkDataRecievedTime);
		float totalTimePassed = pingInSeconds + timeSinceLastUpdate;
		Vector3 exterpolatedTargetPosition = realPosition + transform.forward * speed * totalTimePassed;
		Vector3 newPos = Vector3.MoveTowards(transform.position, exterpolatedTargetPosition, speed * Time.deltaTime);
		//transform.position = Vector3.MoveTowards(transform.position, exterpolatedTargetPosition, speed * Time.deltaTime);
		if (Vector3.Distance (transform.position, exterpolatedTargetPosition) > 2.0f) {
			newPos = exterpolatedTargetPosition;
			//newPos = Vector3.Lerp(newPos, exterpolatedTargetPosition,this.SmoothingDelay);
		}
//		else if (Vector3.Distance (transform.position, exterpolatedTargetPosition) < 4.0f) {
//			newPos = realPosition;		
//		}
			
		newPos.y = Mathf.Clamp (Time.time * 1.5f, newPos.y , newPos.y+1 );
		//transform.rotation = Quaternion.Lerp(transform.rotation, realRotation, Time.deltaTime * this.SmoothingDelay);
		//transform.position = newPos;
		transform.position = Vector3.Lerp (transform.position, newPos, Time.deltaTime * 6.0f);
	}
//	public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) {		
//		if(stream.isWriting) {
//			// This is OUR player. We need to send our actual position to the network.
//			
//			stream.SendNext(transform.position);
//			stream.SendNext(transform.rotation);
//
//		}
//		else {
//			// This is someone else's player.					
//			realPosition = (Vector3)stream.ReceiveNext();
//			realRotation = (Quaternion)stream.ReceiveNext();
//		
//		}
//		
//	}
	public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info){
		if (stream.isWriting == true) {						
			stream.SendNext (transform.position);						
			stream.SendNext (transform.rotation);
			speed = GetComponent<Rigidbody> ().velocity.magnitude;
			stream.SendNext (speed);

				
		}
		else {
			realPosition = (Vector3)stream.ReceiveNext();
			realRotation = (Quaternion)stream.ReceiveNext();
			speed = (float)stream.ReceiveNext();//GetComponent<Rigidbody> ().velocity.magnitude;
			LastNetworkDataRecievedTime = info.timestamp;
			if(gotFirstUpdate == false) {
				transform.position = realPosition;
				transform.rotation = realRotation;
				gotFirstUpdate = true;
			}

		}
	}
}
