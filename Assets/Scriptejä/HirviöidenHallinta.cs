using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HirviöidenHallinta : MonoBehaviour {

	public GameObject[] hallittavat = new GameObject[2];
	public float sammutusEtaisyys;
	GameObject ukko;

	void Awake () {
		ukko = GameObject.Find ("Ukko");
	}

	void Update () {
		float etaisyys = (new Vector2(ukko.transform.position.x, ukko.transform.position.y) - new Vector2(hallittavat[0].transform.position.x, hallittavat[0].transform.position.y)).magnitude;
		if(etaisyys > sammutusEtaisyys && hallittavat[0].activeSelf){
			for(int i = 0; i < hallittavat.Length; i++){
				hallittavat[i].SetActive (false);
			}
		}
		else if(etaisyys < sammutusEtaisyys && !hallittavat[0].activeSelf){
			for(int i = 0; i < hallittavat.Length; i++){
				hallittavat[i].SetActive (true);
			}
		}
	}
}
