using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapaPelaaja : MonoBehaviour {

	public float vahinko;
	public float tyontoVoima;

	void OnCollisionEnter2D(Collision2D pelaaja) {
		if (pelaaja.gameObject != null && pelaaja.gameObject.tag == "Ukko"){
			pelaaja.gameObject.GetComponent<Elama> ().OtaVahinkoa (vahinko, 0);
			Vector2 valiSuunta = pelaaja.transform.position - transform.position;
			pelaaja.gameObject.GetComponent<Rigidbody2D> ().AddForce (valiSuunta.normalized * tyontoVoima, ForceMode2D.Impulse);
		}
	}
}
