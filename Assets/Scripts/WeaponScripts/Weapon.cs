using UnityEngine;
using System.Collections;

public abstract class Weapon : MonoBehaviour {

	public float damage;
	public Transform spawn; // from where we will shoot
	public GameObject bullet;// what will be shot
	public float enCost; //how much energy is used
	//public float kb; //knockback... will cover this later 
	public Transform attachPoint;
	public bool isFiring = false;
	private MechMain parent;

	public MechMain Parent {
		get {
			return parent;
		}
		set {
			parent = value;
		}
	}



	public abstract void fire ();

}
