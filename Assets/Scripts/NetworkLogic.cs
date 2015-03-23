using UnityEngine;
using System.Collections;

public class NetworkLogic : MonoBehaviour {

	public Camera standbyCamera;
	public SpawnSpot[] spawnSpots;

	void Start () {
		spawnSpots = GameObject.FindObjectsOfType<SpawnSpot>();
		Connect ();
	}
	
	// Update is called once per frame
	void Connect (){
		PhotonNetwork.ConnectUsingSettings("Lords-Of-Steam-v1.0");

	}
	void OnGUI(){
		GUILayout.Label (PhotonNetwork.connectionStateDetailed.ToString ());
	}
	void OnJoinedLobby(){
		Debug.Log ("?!?!?!");
		PhotonNetwork.JoinRandomRoom ();
	}
	void OnJoinedRoom(){
		Debug.Log ("OnJoinedRoom");
		SpawnMyPlayer();
	}
	void OnPhotonRandomJoinFailed() {
		Debug.Log ("OnPhotonRandomJoinFailed");
		PhotonNetwork.CreateRoom( null );
	}
	void SpawnMyPlayer(){
		SpawnSpot mySpawnSpot = spawnSpots [Random.Range (0, spawnSpots.Length)];
		GameObject myPlayer = (GameObject)PhotonNetwork.Instantiate("MechNetwork", mySpawnSpot.transform.position, mySpawnSpot.transform.rotation,0);
		standbyCamera.enabled = false;
		myPlayer.GetComponent<MechMain>().enabled = true;
		//myPlayer.GetComponent<NetworkCharacter>().enabled = true;
		myPlayer.transform.FindChild ("Main Camera").gameObject.SetActive (true);

	}
}
