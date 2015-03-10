using UnityEngine;
using System.Collections;

public class AutoWeapon : Weapon {
	public float rof; //rate of fire (how many seconds between each bullet)
	public float reload = 0.3f; // time inbetween firing
	public float inaccuracy = 5.0f; //how inaccurate the weapon is
	protected float counter = 0; // used with rof (rate of fire)
	public float vel; // the velocity
	// Use this for initialization
	void Start () {
		counter = Time.deltaTime;
	}
	
	// Update is called once per frame


	public override void fire()
	{

		if (isFiring == true) {
			counter += Time.deltaTime;
			if (rof < counter) {
				GameObject clone;
				clone = Instantiate (bullet, spawn.position, spawn.rotation)as GameObject;
				clone.SendMessage("Damage", damage);
				clone.rigidbody.AddForce((clone.transform.forward * vel), ForceMode.Acceleration);
				clone.rigidbody.AddForce((clone.transform.right * (Random.Range(-1.0f,1.0f)) * inaccuracy), ForceMode.Acceleration);
				clone.rigidbody.AddForce((clone.transform.up * (Random.Range(-1.0f,1.0f))  * inaccuracy), ForceMode.Acceleration);
				Parent.Energy -= enCost;
				counter = 0;
			}
		}
	}

}
