using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Loppu : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D coll){
		if(coll.tag == "Ukko"){
			GameObject pelaaja = coll.transform.root.gameObject;
			DontDestroyOnLoad (pelaaja);
			GameObject ukko = GameObject.FindGameObjectWithTag ("Ukko").gameObject;
			GameObject kartta = GameObject.FindGameObjectWithTag ("Pelaaja").transform.Find ("InventoryCanvas").Find ("Koko inventory").Find ("Kartta").gameObject;
			kartta.GetComponent<LuoKartta> ().onkoKarttaLuotu = false;
			ukko.transform.position = new Vector3 (5, 5, ukko.transform.position.z);
			SceneManager.LoadScene ("toinen");
		}
	}
}
