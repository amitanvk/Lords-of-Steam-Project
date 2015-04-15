using UnityEngine;
using System.Collections;

public class AutoRayWeapon : AutoWeapon {
	public float coneAngle = 1.04f;
	// Use this for initialization
	private Quaternion coneRandomRotation;
	public GameObject go;

	void Start () {
		pooling = PoolingSystem.Instance;
		counter = Time.deltaTime;
	}
	void Update(){

		coneRandomRotation = Quaternion.Euler (Random.Range (-coneAngle, coneAngle), Random.Range (-coneAngle, coneAngle), 0);
		// Update is called once per frame
	}
	public override void fire(){

		if (isFiring == true) {
			counter += Time.deltaTime;

			if (rof < counter) {
				GetComponent<AudioSource> ().Play ();


					
				RaycastHit hitInfo = new RaycastHit();	


				//GameObject go = pooling.InstantiateAPS(bullet.name, spawn.transform.position, spawn.transform.rotation * coneRandomRotation) as GameObject; 



				Ray ray = new Ray(spawn.transform.position, go.transform.forward);
					//hitInfo = RaycastHit();
				Physics.Raycast (ray, out hitInfo);


				GetComponentInParent<MechMain>().Energy -= enCost;




				//gunEffect(spawn.position, go.transform.forward * vel);
				//GetComponent<PhotonView>().RPC ("gunEffect", PhotonTargets.All, spawn.transform.position, hitInfo.point, vel);
				gunEffect(spawn.transform.position, ray.GetPoint(vel));
				LayerMask layerMask = 1 << 2;
				layerMask = ~layerMask;
				Debug.DrawRay(spawn.transform.position, spawn.transform.forward * vel, Color.red);
				if(Physics.Raycast (spawn.transform.position, spawn.transform.forward , out hitInfo,vel,layerMask))
				{

					Debug.Log(hitInfo.collider.tag);
					if(hitInfo.collider.tag == "Mech")
					{
						Debug.Log("mech hit");
						hitInfo.collider.gameObject.GetComponentInParent<MechMain>().Health -= damage;
					}
				}
			}
			else 
			{
				GetComponent<AudioSource> ().Stop();
			}

		}
	}
	[RPC]
	public void gunEffect(Vector3 startPos, Vector3 endPos){

		GameObject shotFX = pooling.InstantiateAPS(bullet.name, startPos, Quaternion.LookRotation( endPos - startPos ) )as GameObject;
		go.SetActive(true);

		LineRenderer lr = shotFX.GetComponent<LineRenderer>();
		if(lr != null) {
			lr.SetPosition(0, startPos);
			lr.SetPosition(1, endPos);
		}
	}

}
