using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VihollistenLiikkuminen : MonoBehaviour {

	List<Vector2> suunnat = new List<Vector2>();
	Vector2 menoSuunta;
	Vector2 viimeTarkistusPiste;
	Vector2 paikka;
	Rigidbody2D rb;
	float korjausSade;
	public float kiihtyvyys;
	public float maxNopeus;
	public float tarkistusAlueenSade;
	public float korjausVoima;
	public bool jahti;
	public Sprite kuollutSprite;
	LuoLabyrintti labScr; //labyrintin scripti

	void Awake () {
		menoSuunta = Vector2.zero;
		rb = GetComponent<Rigidbody2D> ();
		labScr = GameObject.Find ("Luo Labyrintti").GetComponent<LuoLabyrintti>();
		korjausSade = Random.Range(0, 2f);
	}

	void FixedUpdate () {
		if (gameObject.GetComponent<Elama>().kuollut){
			gameObject.GetComponent<SpriteRenderer> ().sprite = kuollutSprite;
			transform.Find ("SilmänNäköAlue").gameObject.SetActive(false);
			transform.Find ("Spotlight").gameObject.SetActive(false);
			gameObject.GetComponent<CircleCollider2D> ().enabled = false;
			rb.constraints = RigidbodyConstraints2D.FreezeRotation;
			return;
		}
		if (jahti){
			return;
		}
		paikka = new Vector2(Mathf.FloorToInt (transform.position.x / 10), Mathf.FloorToInt (transform.position.y / 10)); //Koordinaatit
		int x = Mathf.RoundToInt(paikka.x);
		int y = Mathf.RoundToInt(paikka.y);
		if (paikka != viimeTarkistusPiste && (menoSuunta == Vector2.zero || labScr.labyrintinData[y][x][Mathf.RoundToInt(Mathf.Abs((menoSuunta.y - 2) * menoSuunta.y) + Mathf.Abs((menoSuunta.x - 1) * menoSuunta.x))] != 0 || OllaankoKeskella())) { //Physics2D.Raycast (transform.position, menoSuunta, 5f, LayerMask.GetMask ("Muuri")).collider != null
			for (int i = 0; i < 4; i++) {
				Vector2 sateenSuunta = new Vector2 (Mathf.Cos (Mathf.PI * i / 2), Mathf.Sin (Mathf.PI * i / 2));
				if (labScr.labyrintinData[y][x][i] == 0 && sateenSuunta != -menoSuunta) {
					suunnat.Add (sateenSuunta);
				}
			}
			if (suunnat.Count != 0) {
				menoSuunta = suunnat [Random.Range (0, suunnat.Count)];
			} else {
				menoSuunta = -menoSuunta;
			}
			viimeTarkistusPiste = new Vector2 (paikka.x, paikka.y);
			suunnat.Clear();
		}
		if (rb.velocity.magnitude < maxNopeus){
			rb.AddForce (menoSuunta * kiihtyvyys);
		}
		//Korjataan keskelle
		if (Mathf.RoundToInt(menoSuunta.x) != 0) {
			if (transform.position.y > y * 10 + 5 - korjausSade){
				rb.AddForce (Vector2.down * korjausVoima);
			}
			else if(transform.position.y < y * 10 + 5 + korjausSade){
				rb.AddForce (Vector2.up * korjausVoima);
			}
		} else {
			if (transform.position.x > x * 10 + 5 - korjausSade){
				rb.AddForce (Vector2.left * korjausVoima);
			}
			else if(transform.position.x < x * 10 + 5 + korjausSade){
				rb.AddForce (Vector2.right * korjausVoima);
			}
		}

		transform.rotation =  Quaternion.Euler(new Vector3 (0, 0, Mathf.Atan2(menoSuunta.y, menoSuunta.x) * Mathf.Rad2Deg));
	}

	bool OllaankoKeskella (){
		if ((transform.position.x < paikka.x * 10 + 5 + tarkistusAlueenSade && transform.position.x > paikka.x * 10 + 5 - tarkistusAlueenSade) && (transform.position.y < paikka.y * 10 + 5 + tarkistusAlueenSade && transform.position.y > paikka.y * 10 + 5 - tarkistusAlueenSade)) {
			return true;
		} 
		else {
			return false;
		}
	}
}