using UnityEngine;
using System.Collections;

public class NetworkLogic : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Connect ();
	}
	
	// Update is called once per frame
	void Connect (){
		PhotonNetwork.ConnectUsingSettings("Lords-Of-Steam-v1.0");
	}
	void OnGui(){
		GUILayout.Label (PhotonNetwork.connectionStateDetailed.ToString ());
	}
	void OnJoinedLobby(){
		Debug.Log ("?!?!?!");
		PhotonNetwork.JoinRandomRoom ();
	}
}
