using UnityEngine;
using System.Collections;

public class MechMain : MonoBehaviour {

	public GameObject enemyMech;

	public GUIText healthMeter;
	public GUIText energyMeter;

	public float health;
	public float energy;
	public float healthMax;
	public float energyMax;
	public float healthRegen;
	public float energyRegen;

	public float moveForce;
	public float hoverForce;
	public float rotationSpeed;

	public bool lockOn;

	// Use this for initialization
	void Start () {
		health = 100;
		energy = 1;
		lockOn = false;
	}
	
	// Update is called once per frame
	void Update () {


		regen ();
		control ();
		state ();
	}


	void regen()
	{

		if (energy < energyMax) 
		{
			energy += energyRegen;
		}
		healthMeter.text = "Health: " + health;
		energyMeter.text = "Energy: " + energy;
	}

	void control()
	{
		Quaternion AddRot = Quaternion.identity;
		float yaw = 0;
		if (lockOn) 
		{
			transform.LookAt (enemyMech.transform.position);
		}

		rigidbody.AddForce (Vector3.up * hoverForce);

		if (gameObject.tag == "mech1") 
		{
			if(Input.GetButtonDown("lock1"))
			{
				lockOn = !lockOn;
			}
			rigidbody.AddForce (Input.GetAxisRaw ("Vertical1") * transform.forward * moveForce);
			if(lockOn)
			{
				rigidbody.AddForce (Input.GetAxisRaw ("Horizontal1") * transform.right * moveForce);
			}
			else
			{
				yaw = Input.GetAxisRaw("Horizontal1") * (Time.fixedDeltaTime * rotationSpeed);
				AddRot.eulerAngles = new Vector3(0, yaw,0);
				rigidbody.rotation *= AddRot;
			}
		}
		if (gameObject.tag == "mech2") 
		{
			if(Input.GetButtonDown("lock2"))
			{
				lockOn = !lockOn;
			}
			rigidbody.AddForce (Input.GetAxisRaw ("Vertical2") * -1 * transform.forward * moveForce);
			if(lockOn)
			{
				rigidbody.AddForce (Input.GetAxisRaw ("Horizontal2") * transform.right * moveForce);
			}
			else
			{
				yaw = Input.GetAxisRaw("Horizontal2") * (Time.fixedDeltaTime * rotationSpeed);
				AddRot.eulerAngles = new Vector3(0, yaw,0);
				rigidbody.rotation *= AddRot;
			}
		}

	}

	void attack()
	{


	}

	void state()
	{
		if (health <= 0) {
			Destroy(gameObject);
				}

	}
	void OnTriggerStay(Collider other)
	{
		if (other.tag == "firetrap") 
		{
			health -= 0.25f;
		}
	}
}
