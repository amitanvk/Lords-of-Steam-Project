using UnityEngine;
using System.Collections;

public class DeathFloor : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnTriggerEnter(Collider other)
	{
		if ((other.tag == "Mech")) {

				other.GetComponentInParent<PhotonView> ().RPC ("TakeDamage", PhotonTargets.AllBuffered, 120.0f);
				//				if (shot != null) {
				//					stopParticle();
				//				}

			}

	}
}
