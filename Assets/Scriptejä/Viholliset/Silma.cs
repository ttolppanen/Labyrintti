using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Silma : MonoBehaviour {

	public float maxNopeus;
	public float kiihtyvyys;
	public float tasausVoima;
	public float korjausKiihtyvyys;
	public GameObject silmanNakoAlue;
	public Sprite kuollutSprite;
	List<List<List<int>>> data = new List<List<List<int>>>();
	List<List<GameObject>> dataPalikat = new List<List<GameObject>>();
	List<Vector3> suuntaMahdollisuudet = new List<Vector3> ();
	Rigidbody2D rb;
	int x;
	int mistaTultiinX;
	int mistaTultiinY;
	int y;
	public Vector3 suunta;
	Vector2 mistaTultiinSuunta;
	public bool jahti;
	bool pitaakoKorjata = false;

	void Awake () {
		data = GameObject.Find ("Luo Labyrintti").GetComponent<LuoLabyrintti>().labyrintinData;
		dataPalikat = GameObject.Find ("Luo Labyrintti").GetComponent<LuoLabyrintti>().labyrintinPalaset;
		rb = GetComponent<Rigidbody2D> ();
		x = Mathf.FloorToInt (transform.position.x / 10);
		y = Mathf.FloorToInt (transform.position.y / 10);
		suunta = Vector3.zero;
	}

	void FixedUpdate (){
		int kartassaX = Mathf.FloorToInt (transform.position.x / 10);
		int kartassaY = Mathf.FloorToInt (transform.position.y / 10);
		int maanPaikkaKartassaX = kartassaX * 10 + 5;
		int maanPaikkaKartassaY = kartassaY * 10 + 5;
		if (gameObject.GetComponent<Elama>().kuollut){
			gameObject.GetComponent<SpriteRenderer> ().sprite = kuollutSprite;
			transform.Find ("SilmänNäköAlue").gameObject.SetActive(false);
			transform.Find ("Spotlight").gameObject.SetActive(false);
			gameObject.GetComponent<CircleCollider2D> ().enabled = false;
			rb.constraints = RigidbodyConstraints2D.FreezeRotation;
			return;
		}
		if (pitaakoKorjata){
			Vector2 palautusSuunta = new Vector3(maanPaikkaKartassaX, maanPaikkaKartassaY, 0) - transform.position;
			rb.AddForce (palautusSuunta.normalized * korjausKiihtyvyys);
			if (palautusSuunta.magnitude < 0.01f) {
				transform.position = new Vector3 (maanPaikkaKartassaX, maanPaikkaKartassaY, transform.position.z);
				pitaakoKorjata = false;
			}
			return;
		}
		if (rb.velocity.magnitude == 0 || ((kartassaX != x || kartassaY != y) && (Mathf.Abs (transform.position.x - (kartassaX * 10 + 5)) < 1f && Mathf.Abs (transform.position.y - (kartassaY * 10 + 5)) < 1f))){
			mistaTultiinX = x;
			mistaTultiinY = y;
			x = kartassaX;
			y = kartassaY;
			mistaTultiinSuunta = new Vector2 (x - mistaTultiinX, y - mistaTultiinY);
			if (data [y] [x] [0] == 0 && mistaTultiinSuunta != Vector2.left) {
				suuntaMahdollisuudet.Add (Vector3.right);
			}
			if (data [y] [x] [1] == 0 && mistaTultiinSuunta != Vector2.down) {
				suuntaMahdollisuudet.Add (Vector3.up);
			}
			if (data [y] [x] [2] == 0 && mistaTultiinSuunta != Vector2.right) {
				suuntaMahdollisuudet.Add (Vector3.left);
			}
			if (data [y] [x] [3] == 0 && mistaTultiinSuunta != Vector2.up) {
				suuntaMahdollisuudet.Add (Vector3.down);
			}
			if (suuntaMahdollisuudet.Count == 0){
				suunta = -mistaTultiinSuunta;
			}
			else {
				suunta = suuntaMahdollisuudet [Random.Range (0, suuntaMahdollisuudet.Count)];
				suuntaMahdollisuudet.Clear ();
			}
		}
		if (jahti){return;}
		if (rb.velocity.magnitude < maxNopeus) {
			rb.AddForce (suunta * kiihtyvyys);
		}
		Vector3 maaPalikanOikeaPaikka = new Vector3 (maanPaikkaKartassaX, maanPaikkaKartassaY, 0);
		if (suunta == Vector3.right || suunta == Vector3.left){
			rb.AddForce (new Vector2(0, (maaPalikanOikeaPaikka - transform.position).normalized.y * tasausVoima));
		}
		if (suunta == Vector3.up || suunta == Vector3.down){
			rb.AddForce (new Vector2((maaPalikanOikeaPaikka - transform.position).normalized.x * tasausVoima, 0));
		}
	}
	void Update (){
		if (jahti){return;}

		if (suunta == Vector3.right) {
			transform.rotation =  Quaternion.Euler(new Vector3 (0, 0, 90));
		}
		else if (suunta == Vector3.up) {
			transform.rotation =  Quaternion.Euler(new Vector3 (0, 0, 180));
		}
		else if (suunta == Vector3.left) {
			transform.rotation =  Quaternion.Euler(new Vector3 (0, 0, 270));
		}
		else if (suunta == Vector3.down) {
			transform.rotation = Quaternion.Euler(new Vector3 (0, 0, 0));
		}
	}
	void OnCollisionEnter2D(Collision2D seina){
		if(seina != null && seina.gameObject.tag == "Muuri" && !jahti){
			pitaakoKorjata = true;
		}
	}
}