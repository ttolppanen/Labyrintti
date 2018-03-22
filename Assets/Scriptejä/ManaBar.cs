using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaBar : MonoBehaviour {

	GameObject ukko;
	float maxMana;
	float mana;
	Mana manaScripti;

	void Awake () {
		ukko = GameObject.Find ("Ukko");
		manaScripti = ukko.GetComponent<Mana>();
		maxMana = manaScripti.maxMana;
	}

	void Update () {
		mana = manaScripti.mana;
		transform.localScale = new Vector3 (mana / maxMana, 1, 1);
	}
}
