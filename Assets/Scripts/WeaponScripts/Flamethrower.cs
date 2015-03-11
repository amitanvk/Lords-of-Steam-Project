using UnityEngine;
using System.Collections;

public class Flamethrower : Weapon {
	public Transform smoke;

	void Update()
	{
		particleSystems ();
	}
	public override void fire(){

		if (isFiring == true) 
		{
			Parent.Energy -= enCost;

		}
	}

	void particleSystems()
	{
		if (isFiring) 
		{
			spawn.GetComponent<ParticleSystem>().Play();
			smoke.GetComponent<ParticleSystem>().Play();
		} 
		else 
		{
			spawn.GetComponent<ParticleSystem>().Stop();
			smoke.GetComponent<ParticleSystem>().Stop();
		}
	}

	void OnTriggerStay(Collider other)
	{
		if (isFiring == true) {
			if (other.tag == "mech2") 
			{
				Debug.Log ("mech 2 hit");
				other.GetComponentInParent<MechMain> ().Health -= damage;
			}
			if ((other.tag == "mech1")) {
				Debug.Log ("mech1 hit");
				other.GetComponentInParent<MechMain> ().Health -= damage;
			}
		}
	}
}
