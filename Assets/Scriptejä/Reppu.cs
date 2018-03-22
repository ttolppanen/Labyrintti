﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Reppu : MonoBehaviour {

	GameObject[,] reppu = new GameObject[4, 4];
	List<GameObject[]> kuvat = new List<GameObject[]> ();
	public GameObject[] kuvat1 = new GameObject[4];
	public GameObject[] kuvat2 = new GameObject[4];
	public GameObject[] kuvat3 = new GameObject[4];
	public GameObject[] kuvat4 = new GameObject[4];

	public GameObject kasi; //Käsi
	public Texture tyhjaKuva;

	void Awake(){
		kuvat.Add (kuvat1);
		kuvat.Add (kuvat2);
		kuvat.Add (kuvat3);	
		kuvat.Add (kuvat4);	
	}

	void Update () {
		//Ase repusta käteen
		GameObject aseKadessa = kasi.transform.GetChild (0).gameObject;
		/*for(int i = 0; i < 9; i++){ Vanha siirtäminen?
			if(Input.GetKeyDown((i + 1).ToString()) && reppu[i] != null && reppu[i].name.Substring(0, 3) != aseKadessa.name.Substring(0, 3)){
				Transform esineRepussa = reppu [i].transform;
				esineRepussa.position = kasi.transform.position;
				esineRepussa.transform.rotation = kasi.transform.rotation;
				esineRepussa.parent = kasi.transform;
				reppu [y, x] = aseKadessa;
				aseKadessa.transform.parent = null;
				aseKadessa.transform.position = new Vector3 (-10, -10, 0);
				break;
			}
		}	*/

		//UI
		for(int y = 0; y < 4; y++){
			for (int x = 0; x < 4; x++) {
				if (reppu [y, x] != null) {
					kuvat [y][x].GetComponent<RawImage> ().texture = reppu [y, x].GetComponent<SpriteRenderer> ().sprite.texture;
				} else {
					kuvat [y][x].GetComponent<RawImage> ().texture = tyhjaKuva;
				}
			}
		}
	}

	void OnTriggerEnter2D(Collider2D coll){
		if (coll != null && coll.tag == "AseMaassa"){
			for(int y = 0; y < 4; y++){
				for (int x = 0; x < 4; x++) {
					if (reppu [y, x] == null) {
						coll.gameObject.tag = "AseKadessa";
						coll.transform.position = new Vector3 (-10, -10, 0);
						reppu [y, x] = coll.gameObject;
						coll.transform.parent = kuvat [y] [x].transform;
						return;
					}
				}
			}
		}
	}
}