using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SilmanSeuraus : MonoBehaviour {

	public GameObject silma;
	Rigidbody2D rb;
	public float seurausSade;
	public float seurausKiihtyvyys;
	public float nopeusLisays = 20f;
	public float syoksySade;
	public float syoksyVoima;
	public float syoksyAika;
	bool seurataan;
	float aika;
	Vector3 seurausSuunta;
	GameObject ukko;
	VihollistenLiikkuminen silmanScripti;//vanha nimi
	List<List<GameObject>> dataPalikat = new List<List<GameObject>>();

	void Awake () {
		seurataan = false;
		silmanScripti = silma.GetComponent<VihollistenLiikkuminen> ();
		rb = silma.GetComponent<Rigidbody2D> ();
		seurausSuunta = Vector3.zero;
		dataPalikat = GameObject.Find ("Luo Labyrintti").GetComponent<LuoLabyrintti>().labyrintinPalaset;
		aika = 0;
	}
	void Update(){
		aika += Time.deltaTime;
		if (silmanScripti.jahti && !silma.GetComponent<Elama>().ollaankoJaadyksissa){
			silma.transform.rotation = Quaternion.Euler (new Vector3(0, 0,Mathf.Atan2(seurausSuunta.y, seurausSuunta.x) * Mathf.Rad2Deg));
		}
	}
	void FixedUpdate () {
		if (ukko == null || !silmanScripti.jahti){return;}

		if (muutaVectori2((transform.position - ukko.transform.position)).magnitude > seurausSade){
			seurataan = false;
		}
		if ((muutaVectori2(ukko.transform.position - transform.position)).magnitude < syoksySade && aika > syoksyAika){
			Vector2 syoksySuunta = ukko.transform.position - transform.position;
			rb.AddForce (syoksySuunta.normalized * syoksyVoima, ForceMode2D.Impulse);
			aika = 0;
		}
		if (seurataan) {
			RaycastHit2D[] osumat = Physics2D.RaycastAll (transform.position, ukko.transform.position - transform.position);
			silma.GetComponent<CircleCollider2D> ().enabled = true;
			if (osumat[1].collider.gameObject == ukko.gameObject) {
				seurataan = true;
				seurausSuunta = ukko.transform.position - transform.position;
				seurausSuunta = new Vector3 (seurausSuunta.x, seurausSuunta.y, 0).normalized;
			}
			if (rb.velocity.magnitude < (silmanScripti.maxNopeus + nopeusLisays)){
				rb.AddForce (seurausSuunta * seurausKiihtyvyys);
			}
		} 
		else if (silmanScripti.jahti) {
			int kartassaX = Mathf.FloorToInt (silma.transform.position.x / 10);
			int kartassaY = Mathf.FloorToInt (silma.transform.position.y / 10);
			Vector3 palautusSuunta = dataPalikat [kartassaY] [kartassaX].transform.position - transform.position;
			palautusSuunta = new Vector3 (palautusSuunta.x + 5, palautusSuunta.y + 5, 0);
			rb.AddForce (palautusSuunta.normalized * seurausKiihtyvyys);
			if (palautusSuunta.magnitude < 0.01f){
				silma.transform.position = new Vector3 (dataPalikat [kartassaY] [kartassaX].transform.position.x + 5, dataPalikat [kartassaY] [kartassaX].transform.position.y + 5,silma.transform.position.z);
				silmanScripti.jahti = false;
			}
		}
	}
	void OnTriggerEnter2D(Collider2D col){
		if (col != null && col.gameObject.tag == "Ukko" && !seurataan){
			ukko = col.gameObject;
			seurausSuunta = col.transform.position - transform.position;
			RaycastHit2D[] osumat = Physics2D.RaycastAll (transform.position, seurausSuunta);
			if (osumat[1].collider.gameObject.tag == "Ukko") {
				seurataan = true;
				silmanScripti.jahti = true;
				seurausSuunta = new Vector3 (seurausSuunta.x, seurausSuunta.y, 0).normalized;
			} 
		}
	}
	Vector2 muutaVectori2 (Vector3 vektori){
		Vector2 valiVektori = vektori;
		return valiVektori;
	}
}