﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiekkaScripti : MonoBehaviour {

	public float vahinko; 
	public float jaadytysAika;

	EsineenOminaisuudet om;

	void Awake(){
		om = GetComponent<EsineenOminaisuudet> ();
		om.nimi = "Miekka";
		vahinko = vahinko + Random.Range (0, 5);
		om.vahinko = vahinko.ToString ();
		om.kuvaus = "Hieno miekka millä voi vaikka huitoa.";
	}

	void OnTriggerEnter2D(Collider2D vihollinen){
		if (vihollinen.tag == "Vihollinen"){
			vihollinen.gameObject.GetComponent<Elama> ().OtaVahinkoa (vahinko, jaadytysAika);
		}
	}
}