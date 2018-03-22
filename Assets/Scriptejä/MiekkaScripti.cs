using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiekkaScripti : MonoBehaviour {

	public float vahinko; 
	public float jaadytysAika;

	void OnTriggerEnter2D(Collider2D vihollinen){
		if (vihollinen.tag == "Vihollinen"){
			vihollinen.gameObject.GetComponent<Elama> ().OtaVahinkoa (vahinko * Random.Range(0.8f, 1.2f), jaadytysAika);
		}
	}
}