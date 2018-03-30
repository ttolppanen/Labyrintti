using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UusiVarjo : MonoBehaviour {

	GameObject ukko;
	public float piirtoEtaisyys;
	public GameObject emo; //Vanha nimi, siis se missä on colliderit.
	public bool pitaakoTehdaVarjot = true;
	Material materiaali;
	Renderer rendereri;
	public GameObject varjonReuna;
	Vector4 reunaPisteet; //0 ja 1, eka reuna, 2 ja 3 toinen.
	Vector2 osumaPiste;
	float asteRadiaaneina;
	float alkuKorkeus;

	void Awake () {
		ukko = GameObject.FindGameObjectWithTag ("Ukko");
		reunaPisteet = new Vector4 (789, 0, 789, 0);
		asteRadiaaneina = 2 * Mathf.PI / 360;
		alkuKorkeus = emo.transform.position.z;
		rendereri = GetComponent<Renderer>();
		materiaali = rendereri.material;
		piirtoEtaisyys = 13;
	}

	void Update () {
		if((ukko.transform.position - transform.position).magnitude > piirtoEtaisyys){
			materiaali.SetInt ("_TehdaankoVarjot", 0);
			return;
		}
		else{
			materiaali.SetInt ("_TehdaankoVarjot", 1);
		}
		Vector2 alkuSuunta = transform.position - ukko.transform.position;

		//Muut jutut että toimii
		float etaisyys = Mathf.Pow (Mathf.Pow (alkuSuunta.x, 2) + Mathf.Pow (alkuSuunta.y, 2), 0.5f);
		emo.transform.position = new Vector3 (emo.transform.position.x, emo.transform.position.y, alkuKorkeus + etaisyys / 100);
		transform.position = new Vector3(emo.transform.position.x, emo.transform.position.y, emo.transform.position.z + 0.001f);

		if (!pitaakoTehdaVarjot){
			return;
		}
		//itse reunojen tarkistus
		float alkuKulma = Mathf.Atan2 (alkuSuunta.y, alkuSuunta.x);
		float kulma = alkuKulma;
		while(true){
			bool osuttiinko = false;
			Vector2 suunta = new Vector2 (Mathf.Cos(kulma), Mathf.Sin(kulma));
			RaycastHit2D[] osumat = Physics2D.RaycastAll (ukko.transform.position, suunta, 20);
			foreach(RaycastHit2D osuma in osumat){
				if (osuma.collider.gameObject == emo){
					osuttiinko = true;
					osumaPiste = new Vector2 ((osuma.point.x - varjonReuna.transform.position.x) / 20, (osuma.point.y - varjonReuna.transform.position.y) / 20);
					break;
				}
			}
			if(!osuttiinko){
				reunaPisteet = new Vector4 (osumaPiste.x, osumaPiste.y, 789, 0);
				break;
			}
			kulma += asteRadiaaneina;
		}
		kulma = alkuKulma;
		while(true){
			bool osuttiinko = false;
			Vector2 suunta = new Vector2 (Mathf.Cos(kulma), Mathf.Sin(kulma));
			RaycastHit2D[] osumat = Physics2D.RaycastAll (ukko.transform.position, suunta, 20);
			foreach(RaycastHit2D osuma in osumat){
				if (osuma.collider.gameObject == emo){
					osuttiinko = true;
					osumaPiste = new Vector2 ((osuma.point.x - varjonReuna.transform.position.x) / 20, (osuma.point.y - varjonReuna.transform.position.y) / 20);
					break;
				}
			}
			if(!osuttiinko){
				reunaPisteet = new Vector4 (reunaPisteet.x, reunaPisteet.y, osumaPiste.x, osumaPiste.y);
				break;
			}
			kulma -= asteRadiaaneina;
		}
		materiaali.SetVector("_ReunaPisteet", reunaPisteet);
		materiaali.SetVector("_UkonPaikka", new Vector4((ukko.transform.position.x - varjonReuna.transform.position.x) / 20, (ukko.transform.position.y - varjonReuna.transform.position.y) / 20, 0, 0));

	}
}
