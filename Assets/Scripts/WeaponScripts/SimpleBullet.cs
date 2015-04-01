﻿using UnityEngine;
using System.Collections;

public class SimpleBullet : MonoBehaviour {

	// Use this for initialization
	//private bool active = false;
	public float timer = 0.0f;
	public float lifeSpan = 10.0f;
	public float damage = 5.0f;
	public float fallRate = .1f; //the bullet's own gravity
	public bool active;

	void Awake(){
		//active = true;
	}
	void Start () {
		active = true;
		timer = lifeSpan;

	}
	
	// Update is called once per frame
	void Update () {
		timer -= Time.deltaTime;
		if (timer <= 0) {
			DestroyImmediate(gameObject);
		}

	}
	void FixedUpdate(){
		if (fallRate > 0 || fallRate < 0) {
			gameObject.GetComponent<Rigidbody>().AddForce (-Vector3.up * fallRate);
		}
	}
	void Damage(float dam){
		this.damage = dam;
	}
	[RPC]
	void OnTriggerStay(Collider other) {
		if ( other.tag == "mech2") {
			Debug.Log("mech 2 hit");
			other.GetComponentInParent<MechMain>().Health -= damage;
			Destroy(gameObject);
		}
		if ((other.tag == "mech1")) {
			Debug.Log("mech1 hit");
			other.GetComponentInParent<MechMain>().Health -= damage;
			Destroy(gameObject);
		}
		if ((other.tag == "Mech")) {
			Debug.Log("Mech hit");
			other.GetComponentInParent<MechMain>().Health -= damage;
			Destroy(gameObject);
		}
		//active = false;
	}
}
