using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EsineenOminaisuudet : MonoBehaviour {

	public GameObject tekstiPalikka;

	public string nimi;
	public string vahinko;
	public string kuvaus;
	public bool onkoAse;
	public bool onkoKaytettava;

	public GameObject luoTekstiPalikka(Vector3 paikka, Transform emo){
		GameObject palikka = Instantiate (tekstiPalikka, emo);
		palikka.transform.position = paikka;
		Text teksti = palikka.transform.GetChild (0).GetChild (0).GetComponent<Text> ();
		if (onkoAse) {
			teksti.text = nimi + "\n" + vahinko + "\n\n" + kuvaus;
		} 
		else {
			teksti.text = nimi + "\n\n" + kuvaus;
		}
		return palikka;
	}
}