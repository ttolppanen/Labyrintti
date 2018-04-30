using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Varustus : MonoBehaviour {

	public float defence;

	EsineenOminaisuudet om;

	void Awake(){
		defence += Random.Range (0, 6);
		om = GetComponent<EsineenOminaisuudet> ();
		om.kuvaus = "Defence: " + defence + "\n" + om.kuvaus;
	}
		
}