using UnityEngine;
using System.Collections;

public class DamageText : MonoBehaviour {

	public float fadeTime=1.0f;
	float startTime=0;

	
	void Start () {
		startTime=Time.time;
	}
	
	
	void Update () {
		
		transform.Translate(0,Time.deltaTime*1.0f,0);
		transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.LookRotation(-transform.position,GameObject.Find ("MechNetwork").transform.position),9f*Time.deltaTime);
		
		float newAlpha=1.0f-(Time.time-startTime)/fadeTime;
		GetComponent<TextMesh>().color=new Color(1,0,0,newAlpha);
		GetComponent<TextMesh>().color=new Color(1,0,0,1.0f);
		
		if (newAlpha<=0)
		{
			gameObject.DestroyAPS();
		}
	}
}
