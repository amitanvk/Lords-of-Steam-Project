using UnityEngine;
using System.Collections;

public class AutoWeapon : Weapon {
	public float rof; //rate of fire (how many seconds between each bullet)
	public int magSize; // the most bullets that can be shot before a pause
	protected int bNum; //remembers how many bullets we have
	public float reload = 0.3f; // time inbetween firing
	public float inaccuracy = 5.0f; //how inaccurate the weapon is
	protected bool isFiring = false;
	protected bool canFire = true;
	protected float counter = 0; // used with rof (rate of fire)
	public float vel; // the velocity
	// Use this for initialization
	void Start () {
		bNum = magSize;
		counter = Time.deltaTime;
	}
	
	// Update is called once per frame
	void Update () {
		Inputs();
	}
	void Inputs(){
		if (Input.GetButton (shoot) && GetComponentInParent<MechMain> ().energy >= enCost && canFire == true) {
			isFiring = true;
		} else {
			isFiring = false;
		}
		
		if (isFiring == true) {
			counter += Time.deltaTime;
			if (rof < counter) {
				GameObject clone;
				clone = Instantiate (bullet, spawn.position, spawn.rotation)as GameObject;
				clone.SendMessage("Damage", damage);
				clone.rigidbody.AddForce((clone.transform.forward * vel), ForceMode.Acceleration);
				clone.rigidbody.AddForce((clone.transform.right * (Random.Range(-1.0f,1.0f)) * inaccuracy), ForceMode.Acceleration);
				clone.rigidbody.AddForce((clone.transform.up * (Random.Range(-1.0f,1.0f))  * inaccuracy), ForceMode.Acceleration);
				GetComponentInParent<MechMain> ().energy -= enCost;
				bNum--;
				counter = 0;
				if (bNum <= 0 || !isFiring) {
					StartCoroutine (wait ());
				}
			}
			
		}
	}
	IEnumerator wait(){					
		canFire = false;
		isFiring = false;
		yield return new WaitForSeconds(reload);
		canFire = true;
		bNum = magSize;
	}
}
