using UnityEngine;
using System.Collections;

public class NetworkLogic : MonoBehaviour {

	public int lives = 3;
	public Camera standbyCamera;
	public SpawnSpot[] spawnSpots;
	public bool online = true;
	private bool connected = false;
	[SerializeField]
	private Transform hud;
	[SerializeField]
	private Transform myhud;
	private GameObject myPlayer;
	public GameObject prefab;
	public bool On = false;
	public float respawnTimer = 0.0f;

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
		if (respawnTimer > 0) {
			respawnTimer -= Time.deltaTime;
			if(respawnTimer<=0){
				SpawnMyPlayer();
			}
		}
	}
	void OnGUI(){
		if (myPlayer != null) {
			GUILayout.Label (PhotonNetwork.connectionStateDetailed.ToString () + "          H: " + myPlayer.GetComponent<MechMain>().Health +"      E:" + 
			                 myPlayer.GetComponent<MechMain>().Energy + "       recharge:" + myPlayer.GetComponent<MechMain>().EnWait);				
				} else 
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
		if (lives > 0) {
			lives -= 1;
			SpawnSpot mySpawnSpot = spawnSpots [Random.Range (0, spawnSpots.Length)];
			if (hud != null) {
				//myhud = (Transform)PhotonNetwork.Instantiate("HUD2", mySpawnSpot.transform.position, mySpawnSpot.transform.rotation,0);
			}
			myPlayer = (GameObject)PhotonNetwork.Instantiate ("MechNetwork", mySpawnSpot.transform.position, mySpawnSpot.transform.rotation, 0);

			standbyCamera.GetComponent<AudioListener> ().enabled = false;
			standbyCamera.enabled = false;
			myPlayer.GetComponent<MechMain> ().enabled = true;

			myPlayer.transform.FindChild ("Main Camera").gameObject.SetActive (true);
		} else {
			Application.LoadLevel(0);
		}
	}

	void SpawnOffline(){
		SpawnSpot mySpawnSpot = spawnSpots [Random.Range (0, spawnSpots.Length)];
		if (hud != null) {
			//myhud = (Transform)PhotonNetwork.Instantiate("HUD2", mySpawnSpot.transform.position, mySpawnSpot.transform.rotation,0);
		}
		myPlayer = (GameObject)Instantiate(prefab, mySpawnSpot.transform.position, mySpawnSpot.transform.rotation);
		
		standbyCamera.GetComponent<AudioListener> ().enabled = false;
		standbyCamera.enabled = false;
		myPlayer.GetComponent<MechMain>().enabled = true;
		
		myPlayer.transform.FindChild ("Main Camera").gameObject.SetActive (true);
		On = true;	
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
