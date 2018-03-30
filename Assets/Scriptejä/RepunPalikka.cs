using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RepunPalikka : MonoBehaviour {

	float palikanKoko;
	public GameObject repunOminaisuudet;
	public GameObject ukko;
	GameObject[,] esineReppu = new GameObject[4, 4];
	RepunOminaisuuksia rOm;
	Vector2 repunAlareuna;
	bool olenKannossa;
	GameObject minunKuva;
	GameObject tekstiPalikka;
	bool siirraKuvaTakaisin; //Siirto tässä framessa, mutta piirto ens framessa, joten tämä on ratkaisu :D
	bool tekstiPalikkaOnLuotu;

	void Awake(){
		rOm = repunOminaisuudet.GetComponent<RepunOminaisuuksia> ();
		palikanKoko = rOm.palikanKoko;
		olenKannossa = false;
		repunAlareuna = rOm.repunAlareuna;
		esineReppu = ukko.GetComponent<Reppu> ().reppu;
	}

	void Update(){
		if(siirraKuvaTakaisin){
			siirraKuvaTakaisin = false;
			minunKuva.transform.position = transform.position;
		}
		minunKuva = transform.GetChild (0).gameObject;
		if(Input.GetMouseButtonDown(0) && !rOm.onkoKannossa && ollaankoSisalla()){
			olenKannossa = true;
			rOm.onkoKannossa = true;
		}
		if(olenKannossa){
			minunKuva.transform.position = Input.mousePosition;
		}
		if(Input.GetMouseButtonUp(0) && olenKannossa){
			RaycastHit2D[] osumat = Physics2D.RaycastAll (Input.mousePosition, Camera.main.ScreenPointToRay(Input.mousePosition).direction);
			bool loytyko = false;
			foreach(RaycastHit2D hit in osumat){
				if(hit.transform.tag == "RepunPalikka" && hit.transform != transform){
					siirraKuvaTakaisin = true;
					Transform pudotusPaikka = hit.transform;
					string uusiIndeksi = pudotusPaikka.name.Substring(0, 2);
					string tamaIndeksi = gameObject.name.Substring(0, 2);
					GameObject uusiItemi = esineReppu [int.Parse(uusiIndeksi.Substring (0, 1)), int.Parse(uusiIndeksi.Substring (1, 1))];
					esineReppu [int.Parse (uusiIndeksi.Substring (0, 1)), int.Parse (uusiIndeksi.Substring (1, 1))] = esineReppu [int.Parse (tamaIndeksi.Substring (0, 1)), int.Parse (tamaIndeksi.Substring (1, 1))];
					esineReppu [int.Parse (tamaIndeksi.Substring (0, 1)), int.Parse (tamaIndeksi.Substring (1, 1))] = uusiItemi;
					loytyko = true;
					rOm.onkoKannossa = false;
					olenKannossa = false;
					break;
				}
			}
			if(!loytyko){
				minunKuva.transform.position = transform.position;
				rOm.onkoKannossa = false;
				olenKannossa = false;
			}
		}
		//Teksti boxi
		if(ollaankoSisalla() && !tekstiPalikkaOnLuotu && esineReppu [int.Parse (gameObject.name.Substring (0, 1)), int.Parse (gameObject.name.Substring (1, 1))] != null){
			tekstiPalikkaOnLuotu = true;
			tekstiPalikka = esineReppu [int.Parse (gameObject.name.Substring (0, 1)), int.Parse (gameObject.name.Substring (1, 1))].GetComponent<EsineenOminaisuudet> ().luoTekstiPalikka (transform.position, transform);

		}
		if(!ollaankoSisalla() && tekstiPalikka != null){
			tekstiPalikkaOnLuotu = false;
			Destroy (tekstiPalikka);
		}
	}

	bool ollaankoSisalla(){
		Vector2 oikeaPaikka = new Vector2 (transform.position.x - palikanKoko / 2, transform.position.y - palikanKoko / 2);
		Vector3 hiirenPaikka = Input.mousePosition;
		if(hiirenPaikka.x > oikeaPaikka.x && hiirenPaikka.x < oikeaPaikka.x + palikanKoko && hiirenPaikka.y > oikeaPaikka.y && hiirenPaikka.y < oikeaPaikka.y + palikanKoko){
			return true;
		}
		return false;
	}
}

