using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpBar : MonoBehaviour {

	GameObject ukko;
	float maxHp;
	float hp;
	Elama hpScripti;

	void Awake () {
		ukko = GameObject.Find ("Ukko");
		hpScripti = ukko.GetComponent<Elama>();
	}

	void Update () {
		hp = hpScripti.HP;
		maxHp = hpScripti.maxHP;
		transform.localScale = new Vector3 (hp / maxHp, 1, 1);
	}
}
