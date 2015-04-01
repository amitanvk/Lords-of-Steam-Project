using UnityEngine;
using System.Collections;

public class NetworkCharacter : Photon.MonoBehaviour {

	Vector3 realPosition = Vector3.zero;
	Quaternion realRotation = Quaternion.identity;
	bool gotFirstUpdate = false;
	public float SmoothingDelay = 5;


	void Start () {
	
	}

	// Update is called once per frame

	void Update () {
		if( photonView.isMine ) {
			// Do nothing
		}
		else {
			transform.position = Vector3.Lerp(transform.position, realPosition, Time.deltaTime * this.SmoothingDelay);
			transform.rotation = Quaternion.Lerp(transform.rotation, realRotation, Time.deltaTime * this.SmoothingDelay);
		}
	}
	public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) {		
		if(stream.isWriting) {
			// This is OUR player. We need to send our actual position to the network.
			
			stream.SendNext(transform.position);
			stream.SendNext(transform.rotation);

		}
		else {
			// This is someone else's player.					
			realPosition = (Vector3)stream.ReceiveNext();
			realRotation = (Quaternion)stream.ReceiveNext();



			
		}
		
	}
}
