using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mana : MonoBehaviour {

	public float maxMana;
	public float mananKasvu;
	public float mana;

	void Awake () {
		mana = maxMana;
	}

	void Update () {
		if (mana < maxMana){
			mana += Time.deltaTime * mananKasvu;
			if (mana > maxMana) {
				mana = maxMana;
			}
		}
		if (mana <= 0) {
			mana = 0;
		}
	}
}
