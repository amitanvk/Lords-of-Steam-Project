using UnityEngine;
using System.Collections;

public class MechMain : MonoBehaviour {

	[SerializeField]
	private AudioClip steam;
	
	[SerializeField]
	private GameObject enemyMech;
	
	[SerializeField]
	private GUIText healthMeter;
	[SerializeField]
	private GUIText energyMeter;
	
	[SerializeField]
	private Transform healthBar;
	[SerializeField]
	private Transform energyBar;

	[SerializeField]
	private float healthMax;
	[SerializeField]
	private float energyMax;
	[SerializeField]
	private float healthRegen;
	[SerializeField]
	private float energyRegen;
	private float health;
	public float Health {
		get {
			return health;
		}
		set {
			health = value;
		}
	}
	
	private float energy;
	public float Energy {
		get {
			return energy;
		}
		set {
			energy = value;
		}
	}

	
	[SerializeField]
	private float moveForce;
	[SerializeField]
	private float dashForce;
	[SerializeField]
	private float defaultForce;
	[SerializeField]
	private float hoverForce;
	[SerializeField]
	private float rotationSpeed;
	[SerializeField]
	private float jumpForce;
	
	[SerializeField]
	private string Vertical;
	[SerializeField]
	private string Horizontal;
	[SerializeField]
	private string dash;
	[SerializeField]
	private string locker;
	[SerializeField]
	private string jump;

	private bool lockOn;
	private bool dashOn;
	private bool overHeat;
	private bool jumping;
	public bool isFiring;

	[SerializeField]
	private Transform leftWeaponAttachPoint;
	[SerializeField]
	private Transform rightWeaponAttachPoint;

	[SerializeField]
	private GameObject leftWeaponPrefab;
	[SerializeField]
	private GameObject rightWeaponPrefab;

	// Use this for initialization
	void Start () {
		health = 100;
		energy = 1;
		lockOn = false;
		dashOn = false;	
		overHeat = false;

		health = healthMax;
		energy = energyMax;

		defaultForce = moveForce;

		Attach (leftWeaponPrefab, leftWeaponAttachPoint,-1);
		Attach (rightWeaponPrefab, rightWeaponAttachPoint,1);

	}

	GameObject Attach(GameObject prefab, Transform location, float dir) {
		GameObject instance = (GameObject)Instantiate (prefab);
		instance.transform.parent = location;
		Vector3 temp = new Vector3 (dir,1f,1f);
		instance.transform.localScale = temp;
		instance.transform.localPosition = Vector3.zero;
		instance.transform.localRotation = Quaternion.identity;
		return instance;
	}
	
	// Update is called once per frame
	void Update () {
		UpdateGUI ();
	}

	void FixedUpdate() {
		Regen ();
		Control ();
		State ();
	}


	// CONTROLLER METHOD 
	void Control()
	{
		Quaternion AddRot = Quaternion.identity;
		float yaw = 0;

		if(Input.GetButtonDown(locker))
		{
			lockOn = !lockOn;
		}
		if (lockOn) 
		{
			transform.LookAt (enemyMech.transform.position);
		}
		if(Input.GetButton(dash) && energy > 0.0f) {
			moveForce = dashForce;
			dashOn = true;
			if(!audio.isPlaying)
			{
				audio.PlayOneShot(steam);
			}
			energy -= energyRegen;
		} else {
			dashOn = false;
			moveForce = defaultForce;
		}

		if (Input.GetButton (jump) && energy > 0.0f) 
		{
			jumping = true;
			rigidbody.AddForce (transform.up * jumpForce, ForceMode.Acceleration);
			energy -= energyRegen;
			if(!audio.isPlaying)
			{
				audio.PlayOneShot(steam);
			}
		} 
		else 
		{
			jumping = false;
		}

		if(lockOn)
		{
			rigidbody.AddForce (Input.GetAxisRaw (Horizontal) * transform.right * moveForce,ForceMode.Acceleration);
		}
		else
		{
			yaw = Input.GetAxisRaw(Horizontal) * (Time.fixedDeltaTime * rotationSpeed);
			AddRot.eulerAngles = new Vector3(0, yaw,0);
		}
		rigidbody.rotation *= AddRot;
		rigidbody.AddForce (Input.GetAxisRaw (Vertical) * transform.forward * moveForce,ForceMode.Acceleration);
	}

	void Regen()
	{
		if (!isFiring && !dashOn && !jumping && energy < energyMax)
		{
			energy += energyRegen;
		}
	}

	void State()
	{
		if (health <= 0) {
			Destroy(gameObject);
		}
	}

	void UpdateGUI()
	{
		const float scaling = 1.45f;
		float healthDisplay = Mathf.Clamp (health, 0f, healthMax);
		float energyDisplay = Mathf.Clamp (energy, 0f, energyMax);
		Vector3 tempHealth = healthBar.localScale;
		Vector3 tempEnergy = energyBar.localScale;
		tempHealth.x = (health / healthMax) * scaling;
		tempEnergy.x = (energy / energyMax) * scaling;
		healthMeter.text = "Health: " + health;
		energyMeter.text = "Energy: " + energyDisplay;
		energyBar.localScale = tempEnergy;
		healthBar.localScale = tempHealth;
	}
	//TRIGGER METHODS
	void OnTriggerStay(Collider other)
	{
		if (other.tag == "firetrap") 
		{
			health -= 0.25f;
		}
	}
}
