using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TuliPallo : MonoBehaviour {

	public GameObject tuliPallo;
	public float castAika;
	public float manaMaksu;
	float aika = 0;
	Mana manaScripti;

	void Awake (){
		manaScripti = GetComponent<Mana>();
	}

	void Update () {
		if (Input.GetMouseButtonDown(1) && aika > castAika && manaScripti.mana > manaMaksu){
			Instantiate (tuliPallo, transform.position, Quaternion.identity);
			aika = 0;
			manaScripti.mana -= manaMaksu;
		}
		aika += Time.deltaTime;
	}
}
