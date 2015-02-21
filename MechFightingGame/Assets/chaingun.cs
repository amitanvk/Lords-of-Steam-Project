using UnityEngine;
using System.Collections;

public class chaingun : MonoBehaviour {

	// Use this for initialization
	public GameObject parentObject;
	public AudioClip chainGun;
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		if(parentObject.tag == "mech1" && Input.GetButton("Fire21"))
		{
				if(GetComponentInParent<MechMain>().energy > 0)
				{
				if(!audio.isPlaying)
				{
					audio.PlayOneShot(chainGun);
				}
				GetComponentInParent<MechMain>().energy -=2;
				particleSystem.Emit(1);
				RaycastHit hit;
				LayerMask layerMask = 1 << 2;
				layerMask = ~layerMask;
				if(Physics.Raycast (transform.position, transform.forward, out hit,100.0F,layerMask))
				{
					Debug.Log(hit.collider.tag);
					if(hit.collider.tag == "mech2")
					{
						Debug.Log("mech2 hit");
						hit.collider.gameObject.GetComponentInParent<MechMain>().health -= 0.5f;
					}
				}
			}

		}

		if(parentObject.tag == "mech2" && Input.GetButton("Fire22"))
		{
			if(GetComponentInParent<MechMain>().energy > 0)
			{
				if(!audio.isPlaying)
				{
					audio.PlayOneShot(chainGun);
				}
				GetComponentInParent<MechMain>().energy -=2;
				particleSystem.Emit(1);
				RaycastHit hit;
				LayerMask layerMask = 1 << 2;
				layerMask = ~layerMask;
				if(Physics.Raycast (transform.position, transform.forward, out hit,100.0F,layerMask))
				{
					Debug.Log(hit.collider.tag);
					if(hit.collider.tag == "mech1")
					{
						Debug.Log("mech1 hit");
						hit.collider.gameObject.GetComponentInParent<MechMain>().health -= 0.5f;
					}
				}
			}
			
		}


		/**if(Input.GetButtonUp("Fire21"))
		{
			particleSystem.Stop();
		}
		**/
	}
}
