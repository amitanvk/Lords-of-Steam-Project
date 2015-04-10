using UnityEngine;
using System.Collections;

public class Flamethrower : Weapon {
	public Transform smoke;
	AudioSource source;
	void Start()
	{
		source = GetComponent<AudioSource> ();
	}
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
			if(!source.isPlaying)
			{
				source.Play();
			}
			spawn.GetComponent<ParticleSystem>().Play();
			smoke.GetComponent<ParticleSystem>().Play();
		} 
		else 
		{
			source.Stop();
			spawn.GetComponent<ParticleSystem>().Stop();
			smoke.GetComponent<ParticleSystem>().Stop();
		}
	}

	void OnTriggerStay(Collider other)
	{
		if (isFiring == true) {
			if (other.tag == "mech2" && other.GetComponentInParent<MechMain>() != null) 
			{
				Debug.Log ("mech 2 hit");
				other.GetComponentInParent<MechMain> ().Health -= damage;
			}
			if ((other.tag == "mech1") && other.GetComponentInParent<MechMain>() != null) {
				Debug.Log ("mech1 hit");
				other.GetComponentInParent<MechMain> ().Health -= damage;
			}
		}
	}
}
