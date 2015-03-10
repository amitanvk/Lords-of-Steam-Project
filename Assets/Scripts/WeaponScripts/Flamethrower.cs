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
			spawn.particleSystem.Play();
			smoke.particleSystem.Play();
		} 
		else 
		{
			spawn.particleSystem.Stop();
			smoke.particleSystem.Stop();
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
