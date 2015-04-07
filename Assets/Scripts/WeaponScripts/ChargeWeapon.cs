using UnityEngine;
using System.Collections;

public class ChargeWeapon : Weapon {

	public float minCharge; // seconds charged to launch
	public float maxCharge; // longest charge
	public bool isCharging = false;
	private bool canCharge = true;
	public float charge = 0.0f;
	public float costDampen = 0.5f;
	public float cost;
	public float dist; //how far shots go
	public float angle = 0.5f;
	void Start () {
		cost = enCost;
	}

	void Update () 
	{
		Inputs ();
	}

	public override void fire()
	{

	}
	void Inputs(){
		if (Input.GetButton ("Fire11") && canCharge == true && charge < maxCharge && GetComponentInParent<MechMain> ().Energy >= cost) {
			isCharging = true;
		} else {
			isCharging = false;
			
		}
		if (isCharging == true && charge < maxCharge) {
			charge += Time.deltaTime*3;
			cost = (cost + enCost) * costDampen;
		}
		if (charge < minCharge && !isCharging && canCharge == false) {
			charge = 0.0f;
			cost = enCost;
		} else if (charge >= minCharge && !isCharging && canCharge == true) {
			StartCoroutine (fire (charge));
			charge = 0.0f;
			cost = enCost;
		} else if (charge >= maxCharge-0.1f && canCharge == true) {
			StartCoroutine (fire (charge));
			charge = 0.0f;
			cost = enCost;
		} else {
			
		}
	}
	[RPC]
	IEnumerator fire(float charge){
		float dam = charge * damage;
		GameObject clone;
		clone = PhotonNetwork.Instantiate(bullet.name, spawn.position, spawn.rotation,0) as GameObject; // spawn.rotation can be replaced with a Quaternian
		clone.GetComponent<SimpleBullet>().Damage = damage;
		clone.GetComponent<Rigidbody>().AddForce(transform.forward * (((dist * charge * charge) * 30) % 10000), ForceMode.Acceleration);
		clone.GetComponent<Rigidbody>().AddForce (transform.up * (charge) * angle, ForceMode.Acceleration);
		canCharge = false;
		yield return new WaitForSeconds (0.9f);
		canCharge = true;
		charge = 0.0f;
		cost = enCost;

		
	}
}
