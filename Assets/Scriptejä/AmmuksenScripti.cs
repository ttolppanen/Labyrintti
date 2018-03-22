using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmuksenScripti : MonoBehaviour {

	public float vahinko;

	void OnTriggerEnter2D(Collider2D col){
		if(col != null && col.tag == "Ukko"){
			col.gameObject.GetComponent<Elama> ().OtaVahinkoa (vahinko, 0);
			Destroy (gameObject);
		}
	}
}