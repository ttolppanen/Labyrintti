using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmpuvaSilma : MonoBehaviour {

	public GameObject ampuvaSilma;
	public GameObject ammus;
	public float ampumisCooldown;
	public float ampumisVoima;
	float ampumisAika;
	bool seurataan;
	Vector3 ampumisSuunta;
	GameObject ukko;
	VihollistenLiikkuminen SeurausScripti;

	void Awake() {
		SeurausScripti = ampuvaSilma.GetComponent<VihollistenLiikkuminen> ();
		seurataan = false;
	}

	void Update () {
		if(ukko == null){//Tämä returni saattaa rikkoa?
			return;
		}
		RaycastHit2D[] osumat = Physics2D.RaycastAll (transform.position, ukko.transform.position - transform.position, 50);
		foreach(RaycastHit2D osuma in osumat){
			if(osuma.collider.gameObject != gameObject && osuma.collider.gameObject != ampuvaSilma && osuma.collider.tag != "Ammus" && osuma.collider.tag != "AseKadessa" && osuma.collider.tag != "AseMaassa"){
				if (osuma.collider.tag != "Ukko") {
					Debug.Log (osuma.collider.name);
					SeurausScripti.jahti = false;
					seurataan = false;
				}
				break;
			}
		}
		if(!SeurausScripti.jahti){
			return;
		}

		ampumisSuunta = ukko.transform.position - transform.position;
		ampumisSuunta = new Vector3 (ampumisSuunta.x, ampumisSuunta.y, 0);

		if (!ampuvaSilma.GetComponent<Elama>().ollaankoJaadyksissa){
			ampuvaSilma.transform.rotation = Quaternion.Euler (new Vector3(0, 0,Mathf.Atan2(ampumisSuunta.y, ampumisSuunta.x) * Mathf.Rad2Deg));
		}
		if (ampumisAika < 0){
			ampumisAika = ampumisCooldown;
			GameObject ammuttuAmmus = Instantiate (ammus, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
			ammuttuAmmus.transform.rotation = Quaternion.Euler (new Vector3(0, 0,Mathf.Atan2(ampumisSuunta.y, ampumisSuunta.x) * Mathf.Rad2Deg));
			ammuttuAmmus.GetComponent<Rigidbody2D> ().AddForce (ampumisSuunta.normalized * ampumisVoima, ForceMode2D.Impulse);
		}
		ampumisAika -= Time.deltaTime;
	}

	void OnTriggerStay2D(Collider2D col){
		if (col != null && col.gameObject.tag == "Ukko" && !seurataan){
			ukko = col.gameObject;
			ampumisSuunta = col.transform.position - transform.position;
			RaycastHit2D[] osumat = Physics2D.RaycastAll (transform.position, ampumisSuunta, 50);
			foreach(RaycastHit2D osuma in osumat){
				if(osuma.collider.gameObject != gameObject && osuma.collider.gameObject != ampuvaSilma && osuma.collider.tag != "Ammus" && osuma.collider.tag != "AseKadessa" && osuma.collider.tag != "AseMaassa"){
					if(osuma.collider.tag == "Ukko"){
						SeurausScripti.jahti = true;
						ampumisSuunta = new Vector3 (ampumisSuunta.x, ampumisSuunta.y, 0);
						seurataan = true;
						ampumisAika = ampumisCooldown;
					}
					break;
				}
			}
		}
	}
}