using UnityEngine;
using System.Collections;

public class SpawnSpot : MonoBehaviour {

	public int id = 0;
	void OnDrawGizmos(){
		Gizmos.color = Color.red;
		Gizmos.DrawWireCube(transform.position, new Vector3(7, 7, 7));
	}
	void OnDrawGizmosSelected() {
		Gizmos.color = Color.yellow;
		Gizmos.DrawWireCube(transform.position, new Vector3(15, 15, 15));
	}
}
