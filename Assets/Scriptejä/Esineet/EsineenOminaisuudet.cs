using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EsineenOminaisuudet : MonoBehaviour {

	public GameObject tekstiPalikka;

	public string nimi;
	public string kuvaus;
	public bool onkoAse;
	public bool onkoKaytettava;
	public bool onkoPaalleLaitettava;
<<<<<<< HEAD
	public bool onkoKypara;
	public bool onkoHaarniska;
	public bool onkoHousut;
=======
>>>>>>> f4a311b71bf15d25b89f13debbab2e40fc84e57b

	public GameObject luoTekstiPalikka(Vector3 paikka, Transform emo){
		GameObject palikka = Instantiate (tekstiPalikka, emo);
		palikka.transform.position = paikka;
		Text teksti = palikka.transform.GetChild (0).GetChild (0).GetComponent<Text> ();
		teksti.text = nimi + "\n\n" + kuvaus;
		return palikka;
	}
}