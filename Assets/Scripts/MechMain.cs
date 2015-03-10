using UnityEngine;
using System.Collections;

public class MechMain : MonoBehaviour {


	[SerializeField]
	private GameObject enemyMech;


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
	[SerializeField]
	private string FireLeft;
	[SerializeField]
	private string FireRight;

	private bool lockOn;
	private bool dashOn;
	private bool overHeat;
	private bool jumping;
	private bool leftFiring;
	private bool rightFiring;

	[SerializeField]
	private Transform leftWeaponAttachPoint;
	[SerializeField]
	private Transform rightWeaponAttachPoint;

	[SerializeField]
	private Weapon leftWeaponPrefab;
	[SerializeField]
	private Weapon rightWeaponPrefab;

	private Weapon leftWeapon;
	private Weapon rightWeapon;

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

		leftWeapon = (Weapon) Instantiate (leftWeaponPrefab);
		rightWeapon = (Weapon) Instantiate (rightWeaponPrefab);

		leftWeapon.Parent = this;
		rightWeapon.Parent = this;

		Attach (leftWeapon, leftWeaponAttachPoint,-1);
		Attach (rightWeapon, rightWeaponAttachPoint,1);

	}

	void Attach(Weapon prefab, Transform location, float dir) {
		Transform weaponAttachPoint = prefab.attachPoint;
		Transform prefabTransform = prefab.transform;
		prefabTransform.parent = null;
		weaponAttachPoint.parent = null;
		prefabTransform.parent = weaponAttachPoint;
		weaponAttachPoint.parent = location;
		Vector3 temp = new Vector3 (dir,1f,1f);
		weaponAttachPoint.localScale = temp;
		weaponAttachPoint.localPosition = Vector3.zero;
		weaponAttachPoint.localRotation = Quaternion.identity;
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
		
			energy -= energyRegen;
		} else {
			dashOn = false;
			moveForce = defaultForce;
		}
		//WEAPON FIRING
		if (Input.GetButton (FireLeft) && energy >= leftWeapon.enCost) 
		{
			leftFiring = true;
			leftWeapon.isFiring = true;
			leftWeapon.fire();
		} 
		else 
		{
			leftFiring = false;
			leftWeapon.isFiring = false;
		}
		if (Input.GetButton (FireRight) && energy > rightWeapon.enCost) 
		{
			rightFiring = true;
			rightWeapon.isFiring = true;
			rightWeapon.fire ();
		} 
		else 
		{
			rightFiring = false;
			rightWeapon.isFiring = false;

		}
		// END WEAPON FIRING
		if (Input.GetButton (jump) && energy > 0.0f) 
		{
			jumping = true;
			rigidbody.AddForce (transform.up * jumpForce, ForceMode.Acceleration);
			energy -= energyRegen;

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
		if (!(leftFiring || rightFiring) && !dashOn && !jumping && energy < energyMax)
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
