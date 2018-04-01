using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TuliPallonScripti : MonoBehaviour {

	Rigidbody2D rb;
	public float tuliPallonNopeus;
	public float tuliPallonTyontoVoima;
	public float vahinko;
	public float castAika;
	public float idleVoima;
	public float elinAika;
	public float mananVahentumisKerroin;
	float aika;
	GameObject ukko;

	void Awake () {
		rb = GetComponent<Rigidbody2D> ();
		ukko = GameObject.Find ("Ukko");
		Vector2 suunta = Camera.main.ScreenToWorldPoint (Input.mousePosition) - ukko.transform.position;
		rb.AddForce (suunta.normalized * idleVoima, ForceMode2D.Impulse);
		aika = 0;
	}
	void Update(){
		aika += Time.deltaTime;
		ukko.GetComponent<Mana> ().mana -= Time.deltaTime * mananVahentumisKerroin;
		if (aika > castAika){
			Vector2 suunta = Camera.main.ScreenToWorldPoint (Input.mousePosition) - transform.position;
			rb.AddForce (suunta.normalized * tuliPallonNopeus, ForceMode2D.Impulse);
		}
	}
	void OnTriggerEnter2D(Collider2D kohde){
		if (kohde.tag == "Vihollinen"){
			Vector2 valiSuunta = kohde.transform.position - ukko.transform.position;
			kohde.gameObject.GetComponent<Rigidbody2D> ().AddForce (valiSuunta.normalized * Mathf.Pow(rb.velocity.magnitude, 1.2f) * tuliPallonTyontoVoima, ForceMode2D.Impulse);
			kohde.gameObject.GetComponent<Elama> ().OtaVahinkoa (vahinko * Mathf.Pow(rb.velocity.magnitude, 1.2f), 0);
		}
		if (kohde.gameObject.tag != "Silmän näkökenttä"){
			Destroy (gameObject);
		}
	}
}
