using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Reppu : MonoBehaviour {

	public GameObject[,] reppu = new GameObject[5, 4];
	List<GameObject[]> kuvat = new List<GameObject[]> ();
	public GameObject[] kuvat1 = new GameObject[4];
	public GameObject[] kuvat2 = new GameObject[4];
	public GameObject[] kuvat3 = new GameObject[4];
	public GameObject[] kuvat4 = new GameObject[4];
	public GameObject[] kuvat5 = new GameObject[4];

	public GameObject kasi; //Käsi
	public Texture tyhjaKuva;

	void Awake(){
		kuvat.Add (kuvat1);
		kuvat.Add (kuvat2);
		kuvat.Add (kuvat3);
		kuvat.Add (kuvat4);
		kuvat.Add (kuvat5);
		//reppu [4, 2] = kasi.transform.GetChild (0).gameObject;
	}

	void Update () {
		//UI
		for(int y = 0; y < 5; y++){
			for (int x = 0; x < 4; x++) {
				if (reppu [y, x] != null) {
					kuvat [y][x].GetComponent<RawImage> ().texture = reppu [y, x].GetComponent<SpriteRenderer> ().sprite.texture;
				} else {
					kuvat [y][x].GetComponent<RawImage> ().texture = tyhjaKuva;
				}
			}
		}
		GameObject esineKadessa = kasi.transform.childCount > 0 ? kasi.transform.GetChild (0).gameObject : null;
		/*if(reppu[4, 2] == null && esineKadessa != null){
			Destroy (esineKadessa.gameObject);
		} 
		else if(reppu[4, 2] != esineKadessa){
			Destroy (esineKadessa);
			GameObject aseKateen = Instantiate(reppu [4, 2], kasi.transform);
			aseKateen.transform.position = kasi.transform.position;
			aseKateen.GetComponent<MiekkaScripti> ().vahinko = reppu [4, 2].GetComponent<MiekkaScripti> ().vahinko;
		}*/
		if(reppu[4, 2] == null && esineKadessa != null){
			esineKadessa.transform.position = new Vector3(-10, -10, 0);
			esineKadessa.transform.parent = null;
			esineKadessa = null;
		} 
		else if(reppu[4, 2] != esineKadessa){
			if (esineKadessa != null) {
				esineKadessa.transform.position = new Vector3 (-10, -10, 0);
				esineKadessa.transform.parent = null;
			}
			esineKadessa = reppu [4, 2];
			esineKadessa.transform.parent = kasi.transform;
			esineKadessa.transform.position = kasi.transform.position;
			esineKadessa.transform.rotation = kasi.transform.rotation;
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