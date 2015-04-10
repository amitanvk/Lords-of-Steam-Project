using UnityEngine;
using System.Collections;

public class TitleStateMachineMenu : MonoBehaviour {
	public Transform[] menuOptions;
	public int selectedLevel;
	public string Vertical;
	public string Horizontal;
	public string Select;
	bool canMove;
	public Camera mainCamera;
	bool creditsMenu;
	public GameObject returner;
	public float rotateSpeed;
	public float scrollSpeed;

	Vector3 newRot = new Vector3 (0, 180, 0);
	Vector3 oldRot = Vector3.zero;
	Quaternion newQ = Quaternion.identity;
	Quaternion oldQ = Quaternion.identity;
	Quaternion cusQ = Quaternion.identity;
	public Transform MainCameraPoint;
	public Transform CustomizerCameraPoint;

	public Transform creditsTitle;
	public Transform creditsBody;
	bool customMenu;

	public AudioClip movingSelector;
	public AudioClip selectNoise;
	public GameObject chassis;
	public GameObject[] customMenuOptions;
	public bool leftArmCustomizer;

	// Use this for initialization
	void Start () {
		returner.SetActive (false);
		selectedLevel = 0;
		canMove = true;
		creditsMenu = false;
		customMenu = false;
		newQ.eulerAngles = newRot;
		oldQ.eulerAngles = oldRot;
		cusQ = CustomizerCameraPoint.transform.rotation;
		customMenuOff ();
		leftArmCustomizer = true;
	}
	
	// Update is called once per frame
	void FixedUpdate () {


		if (creditsMenu && !customMenu) 
		{
			mainCamera.transform.rotation = Quaternion.Slerp(mainCamera.transform.rotation, newQ, Time.deltaTime * rotateSpeed);
			credits ();
		}
		else if (!creditsMenu && customMenu) 
		{
			customizer();
		}
		else
		{
			mainCamera.transform.position = Vector3.Slerp(mainCamera.transform.position,MainCameraPoint.transform.position,Time.deltaTime * rotateSpeed);
			mainCamera.transform.rotation = Quaternion.Slerp(mainCamera.transform.rotation, oldQ, Time.deltaTime * rotateSpeed);
			menuController ();
			selectMover ();
			selector ();
		}

		if (Input.GetButtonDown (Select)) {
			GetComponent<AudioSource>().PlayOneShot(selectNoise);
				}
	}

	void menuController() {

		if(Input.GetAxisRaw(Vertical) > 0 && canMove)
		{
			if (selectedLevel > 0) 
			{
				GetComponent<AudioSource>().PlayOneShot(movingSelector);
				canMove = false;
				selectedLevel = selectedLevel - 1;
				Debug.Log ("Selected Option: " + selectedLevel);
			}
		}
		if(Input.GetAxisRaw(Vertical) < 0 && canMove)
		{
			if (selectedLevel < menuOptions.Length - 1) 
			{
				GetComponent<AudioSource>().PlayOneShot(movingSelector);
				canMove = false;
				selectedLevel = selectedLevel + 1;
				Debug.Log ("Selected Option: " + selectedLevel);
			}
		}
		if (Input.GetAxisRaw (Vertical) == 0) {
			canMove = true;
				}
	}

	void selectMover()
	{

		Vector3 newPos = menuOptions [selectedLevel].position;
		newPos.z += 1;
		transform.position = newPos;

	}

	void selector()
	{

		if (Input.GetButtonDown (Select)) 
		{
			Debug.Log ("select");
			//JOIN SESSION
			if (selectedLevel == 0) {
				Application.LoadLevel(1);
			}
			//CHAR SELCT
			if (selectedLevel == 1) {
				customMenu = true;
				customMenuOn();
				StartCoroutine(moveToCustomizer());
				Debug.Log(canMove);
			}
			//TUTORIAL
			if (selectedLevel == 2) {
				Application.LoadLevel(2);
			}
			//CREDITS
			if (selectedLevel == 3) {
				creditsMenu = true;
				Debug.Log("creditsMenu = " + creditsMenu);

			}
			//EXIT
			if (selectedLevel == 4) {
				Debug.Log("Quit");
				Application.Quit();
			}

		}
	}

	void credits()
	{

		Vector3 creditsPos = new Vector3 (0, Input.GetAxis (Vertical)*scrollSpeed, 0);
		creditsTitle.transform.position += creditsPos;
		creditsBody.transform.position += creditsPos;

		if(mainCamera.transform.rotation.eulerAngles.y < 181)
		{
			returner.SetActive (true);
			if (Input.GetButtonDown (Select)) 
			{
				creditsMenu = false;

				Debug.Log("creditsMenu = " + creditsMenu);
				returner.SetActive (false);
			}
		}


	}

	void customizer()
	{
		Vector3 leftArm = customMenuOptions[3].transform.position;
		Vector3 rightArm = customMenuOptions[4].transform.position;
		Vector3 CSPOS = customMenuOptions [0].transform.position;
		// 0 = chaingun
		// 1 = flamethrower
		// 2 = shtogun
		int leftSelect = chassis.GetComponent<WeaponVisualizer> ().leftSelected;
		int rightSelect = chassis.GetComponent<WeaponVisualizer> ().rightSelected;


		if (leftSelect == 0)
						customMenuOptions [3].GetComponent<TextMesh> ().text = "Chaingun";
		if (leftSelect == 1)
			customMenuOptions [3].GetComponent<TextMesh> ().text = "Flamethrower";
		if (leftSelect == 2)
			customMenuOptions [3].GetComponent<TextMesh> ().text = "Shotgun";

		if (rightSelect == 0)
			customMenuOptions [4].GetComponent<TextMesh> ().text = "Chaingun";
		if (rightSelect == 1)
			customMenuOptions [4].GetComponent<TextMesh> ().text = "Flamethrower";
		if (rightSelect == 2)
			customMenuOptions [4].GetComponent<TextMesh> ().text = "Shotgun";

		Debug.Log("Horizontal: " + Input.GetAxis(Horizontal));
		if (leftArmCustomizer) 
		{
			Debug.Log ("moving left arm");
			customMenuOptions[0].transform.position = new Vector3(leftArm.x,leftArm.y,leftArm.z+0.5f);

			if(Input.GetAxis(Horizontal) > 0 && canMove)
			{
				leftArmCustomizer = false;
				canMove = false;
			}
			if(Input.GetAxis(Vertical) < 0 && canMove)
			{
				canMove = false;
				if(chassis.GetComponent<WeaponVisualizer>().leftSelected > 0)
				{
					chassis.GetComponent<WeaponVisualizer>().leftSelected-=1;
				}

			}
			if(Input.GetAxis(Vertical) > 0 && canMove)
			{
				canMove = false;
				if(chassis.GetComponent<WeaponVisualizer>().leftSelected < 2)
				{
					chassis.GetComponent<WeaponVisualizer>().leftSelected+=1;
				}

			}
			if (Input.GetAxis (Vertical) == 0 && Input.GetAxis (Horizontal) == 0) 
			{
				canMove = true;
			}
		}
		else
		{
			Debug.Log ("moving right arm");
			customMenuOptions[0].transform.position = new Vector3(rightArm.x,rightArm.y,rightArm.z+0.05f);

			if(Input.GetAxis(Horizontal) < 0 && canMove)
			{
				leftArmCustomizer = true;
				canMove = false;
			}
			if(Input.GetAxis(Vertical) < 0 && canMove)
			{
				canMove = false;
				if(chassis.GetComponent<WeaponVisualizer>().rightSelected > 0)
				{
					chassis.GetComponent<WeaponVisualizer>().rightSelected-=1;
				}
				
			}
			if(Input.GetAxis(Vertical) > 0 && canMove)
			{
				canMove = false;
				if(chassis.GetComponent<WeaponVisualizer>().rightSelected < 2)
				{
					chassis.GetComponent<WeaponVisualizer>().rightSelected+=1;
				}
				
			}
			if (Input.GetAxis (Vertical) == 0 && Input.GetAxis (Horizontal) == 0) 
			{
				canMove = true;
			}
		}

		if (Input.GetButtonDown (Select)) {
			customMenu = false;
			customMenuOff();
			StartCoroutine(moveBack());
			Debug.Log("Customizer menu: " + customMenu);
				}

	}

	IEnumerator moveToCustomizer()
	{
		float t = 0;
		while (t <= 1f) 
		{
			mainCamera.transform.position = Vector3.Lerp(MainCameraPoint.position,CustomizerCameraPoint.position,t);
			mainCamera.transform.rotation = Quaternion.Slerp(mainCamera.transform.rotation, cusQ, t);
			yield return null;
			t += Time.deltaTime*rotateSpeed;
		}
	}
	IEnumerator moveBack()
	{
		float t = 1;
		while (t >= 0f) 
		{
			mainCamera.transform.position = Vector3.Lerp(MainCameraPoint.position,CustomizerCameraPoint.position,t);
			mainCamera.transform.rotation = Quaternion.Slerp(mainCamera.transform.rotation, cusQ, t);
			yield return null;
			t -= Time.deltaTime*rotateSpeed;
		}
	}

	void customMenuOn()
	{
		for (int i = 0; i < customMenuOptions.Length; i++) 
		{
			customMenuOptions[i].SetActive(true);
		}
	}

	void customMenuOff()
	{
		for (int i = 0; i < customMenuOptions.Length; i++) 
		{
			customMenuOptions[i].SetActive(false);
		}
	}

}
