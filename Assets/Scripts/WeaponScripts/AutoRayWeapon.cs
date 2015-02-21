using UnityEngine;
using System.Collections;

public class AutoRayWeapon : AutoWeapon {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Inputs();
	}
	void Inputs(){
		if (Input.GetButton ("Fire11") && GetComponentInParent<MechMain> ().Energy >= enCost && canFire == true) {
				isFiring = true;
		} else {
				isFiring = false;
		}

		if (isFiring == true) {
			counter += Time.deltaTime;
			if (rof < counter) {
				GetComponentInParent<MechMain>().Energy -=2;
				RaycastHit hit;
				LayerMask layerMask = 1 << 2;
				layerMask = ~layerMask;
				Debug.DrawRay(spawn.transform.position, spawn.transform.forward * vel, Color.red);
				if(Physics.Raycast (spawn.position, spawn.transform.forward , out hit,vel,layerMask))
				{

					Debug.Log(hit.collider.tag);
					if(hit.collider.tag == "mech2")
					{
						Debug.Log("mech2 hit");
						hit.collider.gameObject.GetComponentInParent<MechMain>().Health -= 0.5f;
					}
				}
			}

		}
	}

}
