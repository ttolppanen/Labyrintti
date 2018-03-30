using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Liikkuminen : MonoBehaviour {

	public float liikkumisKiihtyvyys;
	public float maxNopeus;
	public float syoksyVoima;
	public float syoksyCooldown;
	public GameObject inventory;
	float syoksyAika;
	public AudioClip miekkaLyonti;
	Rigidbody2D rb;
	GameObject kartta = null;
	Animator animaatio;

	void Awake () {
		rb = GetComponent<Rigidbody2D> ();
		animaatio = GetComponent<Animator> ();
	}
	void Update() {
		if (Input.GetKeyDown (KeyCode.Tab)) {
			inventory.SetActive (true);
		}
		if (Input.GetKeyUp (KeyCode.Tab)) {
			inventory.SetActive (false);
		}
		if (Input.GetKeyDown ("r")) {
			Application.LoadLevel (Application.loadedLevel);
		}
		if (Input.GetMouseButtonDown (0)) {
			animaatio.SetTrigger ("Lyonti");
			GetComponent<AudioSource> ().clip = miekkaLyonti;
			GetComponent<AudioSource> ().Play ();
		}
		if (Input.GetKeyDown(KeyCode.Space) && syoksyAika < 0){
			syoksyAika = syoksyCooldown;
			Vector2 syoksySuunta = Vector2.zero;
			if (Input.GetKey("d")){
				syoksySuunta += new Vector2(1, 0);
			}
			if (Input.GetKey("w")){
				syoksySuunta += new Vector2(0, 1);
			}
			if (Input.GetKey("a")){
				syoksySuunta += new Vector2(-1, 0);
			}
			if (Input.GetKey("s")){
				syoksySuunta += new Vector2(0, -1);
			}
			syoksySuunta *= 1 / syoksySuunta.magnitude;
			rb.AddForce (syoksySuunta * syoksyVoima, ForceMode2D.Impulse);
		}
		syoksyAika -= Time.deltaTime;
	}

	void FixedUpdate () {
		float nopeusX = rb.velocity.x;
		float nopeusY = rb.velocity.y;
		if (GetComponent<Elama>().HP <= 0){
			Scene scene = SceneManager.GetActiveScene(); 
			SceneManager.LoadScene(scene.name);
		}
		if (Input.GetKey("w") && maxNopeus > nopeusY){
			rb.AddForce (Vector2.up * liikkumisKiihtyvyys);
		}
		if (Input.GetKey("d") && maxNopeus > nopeusX){
			rb.AddForce (Vector2.right * liikkumisKiihtyvyys);
		}
		if (Input.GetKey("a") && maxNopeus > Mathf.Abs(nopeusX)){
			rb.AddForce (Vector2.left * liikkumisKiihtyvyys);
		}
		if (Input.GetKey("s") && maxNopeus > Mathf.Abs(nopeusY)){
			rb.AddForce (Vector2.down * liikkumisKiihtyvyys);
		}
	}
}
