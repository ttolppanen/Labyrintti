using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KarttaSeuraus: MonoBehaviour {

	GameObject ukko;
	public float xSiirto;
	public float ySiirto;
	
	void Awake(){
		ukko = GameObject.Find ("Ukko");
	}
	void Update () {
		transform.position = new Vector3(ukko.transform.position.x + xSiirto, ukko.transform.position.y + ySiirto, 1);	
	}
}
