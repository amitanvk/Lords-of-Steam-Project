using UnityEngine;
using System.Collections;

public class testShotgun : MonoBehaviour {

	public bool active;
	public GameObject parentObject;
	ParticleSystem particle;
	// Use this for initialization
	void Start () {
		active = false;
		particle = GetComponentInParent<ParticleSystem> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (parentObject.tag == "mech1") 
		{

			if (Input.GetButtonDown ("Fire11") && !active && GetComponentInParent<MechMain>().energy > 10) {
				particle.Emit (10);
				GetComponentInParent<MechMain>().energy -=10;
				StartCoroutine (fire ());
			}
		}
		if (parentObject.tag == "mech2") 
		{
			
			if (Input.GetButtonDown ("Fire12") && !active && GetComponentInParent<MechMain>().energy > 10) {
				particle.Emit (10);
				GetComponentInParent<MechMain>().energy -=10;
				StartCoroutine (fire ());
			}
		}
	}
	IEnumerator fire()
	{
		active = true;
		yield return new WaitForSeconds(0.15f);
		active = false;

	}

	void OnTriggerStay(Collider other)
	{
		if (active && (other.tag == "mech2")) {
			Debug.Log("mech 2 hit");
			active = false;
			other.GetComponentInParent<MechMain>().health -= 10;

				}
		if (active && (other.tag == "mech1")) {
			Debug.Log("mech1 hit");
			active = false;
			other.GetComponentInParent<MechMain>().health -= 10;
			
		}
	}
}
