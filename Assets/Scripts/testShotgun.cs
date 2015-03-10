using UnityEngine;
using System.Collections;

public class testShotgun : MonoBehaviour {

	public bool active;
	public GameObject parentObject;
	ParticleSystem particle;
	public AudioClip shotgun;
	public string fireButton;
	// Use this for initialization
	void Start () {
		active = false;
		particle = GetComponentInParent<ParticleSystem> ();
	}
	
	// Update is called once per frame
	void Update () {


		if (Input.GetButtonDown (fireButton) && !active && GetComponentInParent<MechMain>().Energy > 10) {
			if(!audio.isPlaying)
			{
				audio.PlayOneShot(shotgun);
			}
			particle.Emit (10);
			GetComponentInParent<MechMain>().Energy -=10;
			StartCoroutine (fire ());
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
			other.GetComponentInParent<MechMain>().Health -= 10;

				}
		if (active && (other.tag == "mech1")) {
			Debug.Log("mech1 hit");
			active = false;
			other.GetComponentInParent<MechMain>().Health -= 10;
			
		}
	}
}
