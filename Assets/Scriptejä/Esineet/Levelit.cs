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
	float lvlUpRaja;

	void Update(){
		if(experience > lvlUpRaja){
			experience -= lvlUpRaja;
			lvlUpRaja += lvlUpRajaNosto;
			lisaaStatseja ();
			ukko.GetComponent<Elama> ().maxHP = hp;
			ukko.GetComponent<Elama> ().HP = hp;
		}
	}
	void lisaaStatseja(){
		
	}
}
