using UnityEngine;
using System.Collections;

public class tutorialText : MonoBehaviour {

	public float range;
	GameObject mech;
	TextMesh text;
	bool showText;
	string tutorial; 
	public float distance;
	// Use this for initialization
	void Start () {
		showText = true;
		text = GetComponent<TextMesh> ();
		tutorial = text.text;
		showText = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (mech == null) 
		{
			mech = GameObject.FindGameObjectWithTag ("Mech");
		}
		else 
		{
			getDistance();
			if(showText)
			{
				transform.LookAt(mech.transform.position);
				transform.Rotate(0f,180f,0);
				Quaternion angleFixer = Quaternion.identity;
				Vector3 angleFix = new Vector3(0,transform.rotation.eulerAngles.y,transform.rotation.eulerAngles.z);
				angleFixer.eulerAngles = angleFix;
				transform.rotation = angleFixer;
				text.text = tutorial;
			}
			else
			{
				text.text = "";
			}
		}
	}

	void getDistance()
	{
		distance = Vector3.Distance (transform.position, mech.transform.position);
		if(distance<range)
				showText = true;
		else
			showText = false;
	}
}
