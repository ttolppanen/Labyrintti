using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiekkaScripti : MonoBehaviour {

	public int vahinko; 
	public int hajonta;
	public float jaadytysAika;

	GameObject ukko;
	EsineenOminaisuudet om;

	void Awake(){
		om = GetComponent<EsineenOminaisuudet> ();
		om.nimi = "Sword";
		vahinko = vahinko - hajonta + Random.Range (0, hajonta * 2);
		om.kuvaus = "Damage: " + vahinko.ToString () + "\n" + "Nice sword that you can use to swing at stuff.";
		ukko = GameObject.FindGameObjectWithTag ("Ukko");
	}

	void OnTriggerEnter2D(Collider2D vihollinen){
		if (vihollinen.tag == "Vihollinen" && tag != "EsineMaassa"){
			vihollinen.gameObject.GetComponent<Elama> ().OtaVahinkoa (vahinko * ukko.GetComponent<Levelit>().strenght, jaadytysAika);
		}
	}
}