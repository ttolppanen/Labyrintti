using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UkonDataa : MonoBehaviour {

	public List<string> avaimet = new List<string>();
	public AudioClip avainAani;

	void OnTriggerEnter2D(Collider2D col){
		if (col != null && col.gameObject.tag == "Avain"){
			avaimet.Add (col.gameObject.name);
			GetComponent<AudioSource> ().clip = avainAani;
			GetComponent<AudioSource> ().Play ();
			Destroy (col.gameObject);
		}
	}
}