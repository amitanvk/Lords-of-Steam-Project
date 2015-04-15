using UnityEngine;
using System.Collections;

public class NetworkLogic : MonoBehaviour {



	
//	// 3D HUD
//	public Transform health3DText;
//	public Transform energy3DText;
//	public Transform recharge3DText;
//	public Transform lives3DText;
	public GUIStyle myStyle;


	public int lives = 3;
	public Camera standbyCamera;
	public SpawnSpot[] spawnSpots;
	public bool online = true;
	private bool connected = false;

	private GameObject myPlayer;
	public GameObject prefab;
	public bool On = false;
	public float respawnTimer = 0.0f;
	public float spawnWait = 1.5f;

	void Start () {
		spawnSpots = GameObject.FindObjectsOfType<SpawnSpot>();
		Connect ();
	}

	void Connect (){
		if (online) {
			PhotonNetwork.ConnectUsingSettings ("Lords-Of-Steam-v1.0");
			connected = true;
		} else {
			PhotonNetwork.offlineMode = true;
			OnJoinedLobby();
					
		}
	}
	void Update(){
		if (connected == false) {
			Connect ();
		}
		if (connected == true) {
			if (respawnTimer > 0) {
				respawnTimer -= Time.deltaTime;
				if (respawnTimer <= 0) {
					SpawnMyPlayer ();
				}
			}
		}


		//UpdateGUI ();
	}
	void OnGUI(){
		if (myPlayer != null) {
			GUILayout.Label ("Health: " + Mathf.Max (0, (int)myPlayer.GetComponent<MechMain> ().Health) + "                      Recharge: " + Mathf.Max (0, (int)myPlayer.GetComponent<MechMain> ().EnWait), myStyle);
			GUILayout.Label ("Energy: " + Mathf.Max (0, (int)myPlayer.GetComponent<MechMain> ().Energy) + "                      Lives: " + lives, myStyle);

		} else {
			GUILayout.Label (PhotonNetwork.connectionStateDetailed.ToString (), myStyle);
			GUILayout.Label ("");
			GUILayout.Label ("");
			GUILayout.Label ("");
			GUILayout.Label ("");
			GUILayout.Label ("SPAWNING IN...  " + respawnTimer, myStyle);

		}
	}
//	void UpdateGUI()
//	{				
//		if (myPlayer!= null) {
//			health3DText.GetComponent<TextMesh> ().text = "Health: " + myPlayer.GetComponent<MechMain>().Health + "%";
//			
//			energy3DText.GetComponent<TextMesh> ().text = "Energy: " + myPlayer.GetComponent<MechMain>().Energy + "%";
//			recharge3DText.GetComponent<TextMesh> ().text = "Recharge: " + myPlayer.GetComponent<MechMain>().EnWait;
//			lives3DText.GetComponent<TextMesh>().text = "Lives: " + lives;
//			
//		}
//	}
	void OnJoinedLobby(){
		Debug.Log ("?!?!?!");
		PhotonNetwork.JoinRandomRoom ();
	}
	void OnJoinedRoom(){
		Debug.Log ("OnJoinedRoom");
		SpawnMyPlayer();
		//respawnTimer = spawnWait;
	}
	void OnPhotonRandomJoinFailed() {
		Debug.Log ("OnPhotonRandomJoinFailed");
		PhotonNetwork.CreateRoom( null );
	}
	void SpawnMyPlayer(){

		if (lives > 0) {
			lives -= 1;
			SpawnSpot mySpawnSpot = spawnSpots [Random.Range (0, spawnSpots.Length)];

			myPlayer = (GameObject)PhotonNetwork.Instantiate ("MechNetwork", mySpawnSpot.transform.position, mySpawnSpot.transform.rotation, 0);

			standbyCamera.GetComponent<AudioListener> ().enabled = false;
			standbyCamera.enabled = false;
			myPlayer.GetComponent<MechMain> ().enabled = true;
			myPlayer.transform.FindChild ("Main Camera").gameObject.SetActive (true);
		} else {
			if(GameObject.FindObjectOfType<NetworkLogic>().online)
				GameObject.FindObjectOfType<NetworkLogic>().Disconnect();
			else
				GameObject.FindObjectOfType<NetworkLogic>().LeaveRoom();
			Application.LoadLevel(0);
		}
			
	}






	public void Disconnect()
	{
		PhotonNetwork.Disconnect ();
	}
	public void LeaveRoom()
	{
		PhotonNetwork.LeaveRoom ();
	}
}
