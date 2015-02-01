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
	public float dashForce;
	public float defaultForce;
	public float hoverForce;
	public float rotationSpeed;

	public string Vertical;
	public string Horizontal;
	public string dash;
	public string locker;


	public bool lockOn;
	public bool dashOn;
	public bool overHeat;


	// Use this for initialization
	void Start () {
		health = 100;
		energy = 1;
		lockOn = false;
		dashOn = false;
		overHeat = false;

		defaultForce = moveForce;
	}

	// Update is called once per frame
	void Update () {


		regen ();
		updateGUI ();
		control ();
		state();
	}


	void regen()
	{

		if (!dashOn && energy < energyMax)
		{
			energy += energyRegen;
		}

	}

	void updateGUI() {
		healthMeter.text = "Health: " + health;
		if (energy > 0.0f)
						energyMeter.text = "Energy: " + energy;
				else
						energyMeter.text = "Energy: " + 0.0f;

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


			if(Input.GetButton(dash) && energy > 0.0f) {

				moveForce = dashForce;
				dashOn = true;

				energy -= energyRegen;

			} else {
				dashOn = false;
				moveForce = defaultForce;

			}
			if(Input.GetButtonDown(locker))
			{
				lockOn = !lockOn;
			}
			rigidbody.AddForce (Input.GetAxisRaw(Vertical) * transform.forward * moveForce);
			if(lockOn)
			{
				rigidbody.AddForce (Input.GetAxisRaw(Horizontal) * transform.right * moveForce);
			}
			else
			{
				yaw = Input.GetAxisRaw(Horizontal) * (Time.fixedDeltaTime * rotationSpeed);
				AddRot.eulerAngles = new Vector3(0, yaw,0);
				rigidbody.rotation *= AddRot;
			}

			rigidbody.rotation *= AddRot;
			}




	void attack()
	{


	}
	public void takeDamage(float damage) {
		health -= damage;


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
