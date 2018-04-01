using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiekkaScripti : MonoBehaviour {

	public float vahinko; 
	public float jaadytysAika;

	EsineenOminaisuudet om;

	void Awake(){
		om = GetComponent<EsineenOminaisuudet> ();
		om.nimi = "Sword";
		vahinko = vahinko + Random.Range (0, 30);
		om.vahinko = "Damage: " + vahinko.ToString ();
		om.kuvaus = "Nice sword that you can use to swing at stuff.";
	}

	void OnTriggerEnter2D(Collider2D vihollinen){
		if (vihollinen.tag == "Vihollinen"){
			vihollinen.gameObject.GetComponent<Elama> ().OtaVahinkoa (vahinko, jaadytysAika);
		}
	}
}