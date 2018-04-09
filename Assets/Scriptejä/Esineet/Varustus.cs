using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Varustus : MonoBehaviour {

	public float defence;

	EsineenOminaisuudet om;

	void Awake(){
		om = GetComponent<EsineenOminaisuudet> ();
		om.nimi = "Helmet";
		om.kuvaus = "Defence: " + defence.ToString () + "\n" + "Nice helmet that you can use to put your head in.";
	}
		
}