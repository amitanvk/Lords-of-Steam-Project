﻿using UnityEngine;
using System.Collections;

public class ShotgunWeapon : Weapon {
	public float rof; //rate of fire (how many seconds between each bullet)
	public int magSize; // the most bullets that can be shot before a pause
	protected int bNum; //remembers how many bullets we have
	public float reload = 0.3f; // time inbetween firing
	public float inaccuracy = 5.0f; //how inaccurate the weapon is
	protected bool canFire = true;
	protected float counter = 0; // used with rof (rate of fire)
	public float vel; // the velocity
	public Transform[] spawnArray;
	public float[] scatterx;
	public float[] scattery;
	// Use this for initialization
	void Start () {
		bNum = magSize;
		counter = Time.deltaTime;
	}

	public override void fire()
	{

		if (isFiring == true) {
			for(int i = 0; i < spawnArray.Length; i++)
			{
				GameObject clone;
				clone = Instantiate (bullet, spawnArray[i].position, spawnArray[i].rotation)as GameObject;
				clone.SendMessage("Damage", damage);
				clone.GetComponent<Rigidbody>().AddForce((clone.transform.forward * vel), ForceMode.Acceleration);
				clone.GetComponent<Rigidbody>().AddForce((clone.transform.right * scatterx[i] * inaccuracy), ForceMode.Acceleration);
				clone.GetComponent<Rigidbody>().AddForce((clone.transform.up * scattery[i]  * inaccuracy), ForceMode.Acceleration);
				Parent.Energy -= enCost;
				bNum--;
				counter = 0;
				if (bNum <= 0 || !isFiring) {
					StartCoroutine (wait ());
				}
			}
			
		}
	}
	IEnumerator wait(){					
		canFire = false;
		isFiring = false;
		yield return new WaitForSeconds(reload);
		canFire = true;
		bNum = magSize;
	}
}