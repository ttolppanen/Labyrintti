using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PortinAvaus : MonoBehaviour {

	public string avain; //Avain tulee paikan koordinaateista
	List<List<List<int>>> labyrintinData = new List<List<List<int>>>();

	void Awake(){
		avain = Mathf.FloorToInt (transform.position.x / 10).ToString()	+ Mathf.FloorToInt (transform.position.y / 10).ToString();
		labyrintinData = GameObject.Find ("Luo Labyrintti").GetComponent<LuoLabyrintti> ().labyrintinData;
	}

	void OnCollisionEnter2D(Collision2D col){
		if (col != null && col.gameObject.tag == "Ukko"){
			if (col.gameObject.GetComponent<UkonDataa>().avaimet.Contains(avain)){
				col.gameObject.GetComponent<UkonDataa> ().avaimet.Remove (avain);
				labyrintinData[Mathf.FloorToInt (transform.position.y / 10)][ Mathf.FloorToInt (transform.position.x / 10)][Convert.ToInt32(gameObject.name.Replace("PorttiPalikka",""))] = 0;
				Destroy (gameObject);
			}
		}
	}
}
