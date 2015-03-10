using UnityEngine;
using System.Collections;

public class Menu : MonoBehaviour {

	public Transform[] menuArray;
	public Transform select;
	public int selected = 0;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 position = new Vector3 (menuArray [selected].position.x, menuArray [selected].position.y, .47f);
		select.position = position;

		if(Input.GetButtonDown("M1Horizontal"))
		{
			if(Input.GetAxis("M1Horizontal") > 0)
			{
				if(selected < menuArray.Length-1)
				{
					selected++;
				}
			}
			else
			{
				if(selected > 0)
				{
					selected--;
				}
			}
		}

		if (Input.GetButtonDown ("M1Jump")) 
		{
			if(selected == 0) Application.LoadLevel("DemoArena0");
		}

	}
}
