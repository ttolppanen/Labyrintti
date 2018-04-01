using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotioninScripti : MonoBehaviour {

	EsineenOminaisuudet om;
	public int hpParannus;

	void Awake(){
		om = GetComponent<EsineenOminaisuudet> ();
		om.nimi = "Potion";
		om.kuvaus = "Heals for " + hpParannus.ToString() + " health points.";
	}

	void kaytaEsine(){
		Elama elamaScripti = GameObject.Find ("Ukko").GetComponent<Elama>();
		if (elamaScripti.HP + hpParannus > elamaScripti.maxHP) {
			elamaScripti.HP = elamaScripti.maxHP;
		} else {
			elamaScripti.HP += hpParannus;
		}
		Destroy (gameObject);
	}
}