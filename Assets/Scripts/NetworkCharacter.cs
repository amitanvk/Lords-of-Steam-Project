using UnityEngine;
using System.Collections;

public class NetworkCharacter : Photon.MonoBehaviour {

	Vector3 realPosition = Vector3.zero;
	Quaternion realRotation = Quaternion.identity;
	bool gotFirstUpdate = false;


	void Start () {
	
	}

	// Update is called once per frame

	void Update () {
		if( photonView.isMine ) {
			// Do nothing
		}
		else {
			transform.position = Vector3.Slerp(transform.position, realPosition, 0.1f);
			transform.rotation = Quaternion.Slerp(transform.rotation, realRotation, 0.1f);
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
