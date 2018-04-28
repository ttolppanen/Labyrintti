using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Levelit : MonoBehaviour {

	public GameObject ukko;

	public int level;
	public float experience;

	public int strenght;
	public int speed;
	public int intelligence;
	public int hp;

	public float lvlUpRajaNosto;
	public float lvlUpRaja;

	void Update(){
		if(experience >= lvlUpRaja){
			level += 1;
			experience -= lvlUpRaja;
			lvlUpRaja += lvlUpRajaNosto;
			lisaaStatseja ();
			ukko.GetComponent<Elama> ().maxHP = hp;
			ukko.GetComponent<Elama> ().HP = hp;
		}
	}

	void lisaaStatseja(){
		strenght += Random.Range (1, 4);
		speed += Random.Range (1, 4);
		intelligence += Random.Range (1, 4);
		hp += Random.Range (5, 25);
	}
}
