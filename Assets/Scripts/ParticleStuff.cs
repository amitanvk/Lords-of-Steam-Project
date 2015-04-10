using UnityEngine;
using System.Collections;

public class ParticleStuff : MonoBehaviour {
	public ParticleSystem shot;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		particleSystems ();
	}
	[RPC]
	public void particleSystems()
	{
		//if (isFiring) {
			//spawn.GetComponent<ParticleSystem> ().Play ();
			shot.Play ();
			//smoke.GetComponent<ParticleSystem> ().Play ();
		//}
		
	}
	[RPC]
	public void stopParticle(){
		//spawn.GetComponent<ParticleSystem>().Stop();
		shot.Stop();
		//smoke.GetComponent<ParticleSystem>().Stop();
		
	}

}
