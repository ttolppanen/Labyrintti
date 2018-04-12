﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiekkaScripti : MonoBehaviour {

	public float vahinko; 
	public float jaadytysAika;

	GameObject ukko;
	EsineenOminaisuudet om;

	void Awake(){
		om = GetComponent<EsineenOminaisuudet> ();
		om.nimi = "Sword";
		vahinko = vahinko + Random.Range (0, 60);
		om.kuvaus = "Damage: " + vahinko.ToString () + "\n" + "Nice sword that you can use to swing at stuff.";
		ukko = GameObject.FindGameObjectWithTag ("Ukko");
	}

	void OnTriggerEnter2D(Collider2D vihollinen){
		if (vihollinen.tag == "Vihollinen"){
			vihollinen.gameObject.GetComponent<Elama> ().OtaVahinkoa (vahinko * ukko.GetComponent<Levelit>().strenght, jaadytysAika);
		}
	}
}