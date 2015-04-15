using UnityEngine;
using System.Collections;

public class AutoWeapon : Weapon {
	public float rof; //rate of fire (how many seconds between each bullet)
	public float reload = 0.3f; // time inbetween firing
	public float inaccuracy = 5.0f; //how inaccurate the weapon is
	protected float counter = 0; // used with rof (rate of fire)
	public float vel; // the velocity
	//public PoolingSystem poolingSystem;
	public float innac {
		get {
			return inaccuracy;
		}
	}
	public float veloc {
		get {
			return vel;
		}
	}

//	void Awake() {
//		pooling = GetComponentInParent<MechMain>().pool;
//	}
	// Use this for initialization
	void Start () {
		counter = Time.deltaTime;
		pooling = PoolingSystem.Instance;
	}
	
	// Update is called once per frame
	
	public override void fire()
	{

		if (isFiring == true) {
			counter += Time.deltaTime;
			if (rof < counter) {
				GameObject clone;
				//clone = PhotonNetwork.Instantiate (bullet.name, spawn.position, spawn.rotation,0)as GameObject;
				//if (!GetComponent<AudioSource> ().isPlaying)
				GetComponent<AudioSource> ().Play ();
				clone = pooling.InstantiateAPS (bullet.name, spawn.position, spawn.rotation)as GameObject;
				if (clone == null) {
					return;
				}
				clone.SetActive (true);
				clone.GetComponent<SimpleBullet> ().Damage = this.damage;
				clone.GetComponent<Rigidbody> ().AddForce (((transform.forward * vel)), ForceMode.Acceleration);
				clone.GetComponent<Rigidbody> ().AddForce ((transform.right * (Random.Range (-1.0f, 1.0f)) * inaccuracy), ForceMode.Acceleration);
				clone.GetComponent<Rigidbody> ().AddForce ((transform.up * (Random.Range (-1.0f, 1.0f)) * inaccuracy), ForceMode.Acceleration);
				Parent.Energy -= enCost;
				counter = 0;
			}
		} 
		else 
		{
			GetComponent<AudioSource> ().Stop();
		}
	}

}
