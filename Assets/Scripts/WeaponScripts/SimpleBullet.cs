using UnityEngine;
using System.Collections;

public class SimpleBullet : Photon.MonoBehaviour {

	// Use this for initialization
	//private bool active = false;
	public float timer = 0.0f;
	public float lifeSpan = 10.0f;
	public float damage = 1.0f;
	public float fallRate = .1f; //the bullet's own gravity
	//public ParticleSystem shot = null;

	public bool active;

	public float Damage{
		get{
			return damage;
		}
		set
		{
			damage = value;
		}
	}

	
	void OnEnable () {
		active = true;
		timer = lifeSpan;

		
	}
	
	// Update is called once per frame
	void Update () {
		timer -= Time.deltaTime;
//		if (shot != null) {
//			particleSystems();
//		}
		if (timer <= 0) {
			gameObject.DestroyAPS();
		}

	}
//	[RPC]
//	public void particleSystems()
//	{
//
//			//spawn.GetComponent<ParticleSystem> ().Play ();
//			shot.GetComponent<ParticleSystem> ().Play ();
//
//		
//	}
//	[RPC]
//	public void stopParticle(){
//		//spawn.GetComponent<ParticleSystem>().Stop();
//		shot.GetComponent<ParticleSystem>().Stop();
//
//		
//	}
	void FixedUpdate(){
//		GetComponent<Rigidbody>().AddForce((transform.forward * GetComponent<Weapon>().veloc), ForceMode.Acceleration);
//		GetComponent<Rigidbody>().AddForce((transform.right * (Random.Range(-1.0f,1.0f)) * GetComponent<Weapon>().innac), ForceMode.Acceleration);
//		GetComponent<Rigidbody>().AddForce((transform.up * (Random.Range(-1.0f,1.0f))  * GetComponent<Weapon>().innac), ForceMode.Acceleration);
		if (fallRate > 0 || fallRate < 0) {
			gameObject.GetComponent<Rigidbody>().AddForce (-Vector3.up * fallRate);
		}
	}
	void OnDrawGizmos(){
		Gizmos.color = Color.red;
		Gizmos.DrawWireCube(transform.position, new Vector3(3, 3, 3));
	}
	void OnDrawGizmosSelected() {
		Gizmos.color = Color.yellow;
		Gizmos.DrawWireCube(transform.position, new Vector3(7, 7, 7));
	}

	//[RPC]
	void OnTriggerStay(Collider other) {
		if ( other.tag == "mech2") {
			Debug.Log("mech 2 hit");
			other.GetComponentInParent<MechMain>().Health -= damage;
			PoolingSystem.DestroyAPS(gameObject);
		}
		if ((other.tag == "mech1")) {
			Debug.Log("mech1 hit");
			other.GetComponentInParent<MechMain>().Health -= damage;
			PoolingSystem.DestroyAPS(gameObject);
		}
		if ((other.tag == "Mech")) {
			if (GetComponent<PhotonView> ().isMine) {
					Debug.Log ("Mech hit");
					//this.collider.
					other.GetComponentInParent<PhotonView> ().RPC ("TakeDamage", PhotonTargets.AllBuffered, damage);
//				if (shot != null) {
//					stopParticle();
//				}
					gameObject.DestroyAPS ();
			}
		} 
		else {
			//PoolingSystem.DestroyAPS (gameObject);
		}
		//active = false;
	}
}
