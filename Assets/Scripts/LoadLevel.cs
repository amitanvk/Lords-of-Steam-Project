using UnityEngine;
using System.Collections;

public class LoadLevel : MonoBehaviour {
	[SerializeField]
	private int level = 0;
	[SerializeField]
	private bool go = false;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void load () {
		Application.LoadLevel (level);
				
	}
}
