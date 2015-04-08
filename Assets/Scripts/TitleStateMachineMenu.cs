using UnityEngine;
using System.Collections;

public class TitleStateMachineMenu : MonoBehaviour {
	public Transform[] menuOptions;
	public int selectedLevel;
	public string Vertical;
	public string Select;
	bool canMove;
	public Camera mainCamera;
	bool creditsMenu;
	public float rotateSpeed;
	public float scrollSpeed;

	Vector3 newPos = new Vector3 (0, 180, 0);
	Vector3 oldPos = Vector3.zero;
	Quaternion newQ = Quaternion.identity;
	Quaternion oldQ = Quaternion.identity;

	public Transform creditsTitle;
	public Transform creditsBody;


	// Use this for initialization
	void Start () {
		selectedLevel = 0;
		canMove = true;
		creditsMenu = false;

		newQ.eulerAngles = newPos;
		oldQ.eulerAngles = oldPos;
	}
	
	// Update is called once per frame
	void FixedUpdate () {


		if (creditsMenu) 
		{
			mainCamera.transform.rotation = Quaternion.Slerp(mainCamera.transform.rotation, newQ, Time.deltaTime * rotateSpeed);
			credits ();
		}
		else
		{
			mainCamera.transform.rotation = Quaternion.Slerp(mainCamera.transform.rotation, oldQ, Time.deltaTime * rotateSpeed);
			menuController ();
			selectMover ();
			selector ();
		}
	}

	void menuController() {

		if(Input.GetAxisRaw(Vertical) > 0 && canMove)
		{
			if (selectedLevel > 0) 
			{
				canMove = false;
				selectedLevel = selectedLevel - 1;
				Debug.Log ("Selected Option: " + selectedLevel);
			}
		}
		if(Input.GetAxisRaw(Vertical) < 0 && canMove)
		{
			if (selectedLevel < menuOptions.Length - 1) 
			{
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

		if (Input.GetButtonDown (Select) && mainCamera.transform.rotation.eulerAngles.y < 181) 
		{
			creditsMenu = false;
			Debug.Log("creditsMenu = " + creditsMenu);
		}


	}
}
