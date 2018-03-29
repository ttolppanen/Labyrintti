using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepunPalikka : MonoBehaviour {

	float palikanKoko;
	public GameObject repunOminaisuudet;
	public GameObject ukko;
	GameObject[,] esineReppu = new GameObject[4, 4];
	RepunOminaisuuksia rOm;
	Vector2 repunAlareuna;
	bool olenKannossa;
	GameObject minunKuva;

	void Awake(){
		rOm = repunOminaisuudet.GetComponent<RepunOminaisuuksia> ();
		palikanKoko = rOm.palikanKoko;
		olenKannossa = false;
		repunAlareuna = rOm.repunAlareuna;
		esineReppu = ukko.GetComponent<Reppu> ().reppu;
	}

	void Update(){
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
				Debug.Log (hit.transform.tag);
				if(hit.transform.tag == "RepunPalikka" && hit.transform != transform){
					//Kuvat
					/*Transform uudenPaikanEsine = hit.transform.GetChild (0);
					*/Transform pudotusPaikka = hit.transform;
					minunKuva.transform.position = transform.position;
					/*minunKuva.transform.parent = pudotusPaikka;
					minunKuva.transform.position = uudenPaikanEsine.parent.transform.position;
					uudenPaikanEsine.parent = transform;*/
					//Oikeat esineet
					string uusiIndeksi = pudotusPaikka.name.Substring(0, 2);
					string tamaIndeksi = gameObject.name.Substring(0, 2);
					GameObject uusiItemi = esineReppu [int.Parse(uusiIndeksi.Substring (0, 1)), int.Parse(uusiIndeksi.Substring (1, 1))];
					esineReppu [int.Parse (uusiIndeksi.Substring (0, 1)), int.Parse (uusiIndeksi.Substring (1, 1))] = esineReppu [int.Parse (tamaIndeksi.Substring (0, 1)), int.Parse (tamaIndeksi.Substring (1, 1))];
					esineReppu [int.Parse (tamaIndeksi.Substring (0, 1)), int.Parse (tamaIndeksi.Substring (1, 1))] = uusiItemi;
					ukko.GetComponent<Reppu> ().reppu = esineReppu;
					loytyko = true;
					rOm.onkoKannossa = false;
					olenKannossa = false;
					break;
				}
				/*else if(hit.transform.tag == "EsineetPäällä" && hit.transform != transform && hit.transform.name == "Käsi"){
					Transform pudotusPaikka = hit.transform;
					minunKuva.transform.position = transform.position;
				}*/
			}
			if(!loytyko){
				minunKuva.transform.position = transform.position;
				rOm.onkoKannossa = false;
				olenKannossa = false;
			}
		}
	}
	bool ollaankoSisalla(){
		Vector2 oikeaPaikka = new Vector2 (Camera.main.ScreenToWorldPoint(transform.position).x - palikanKoko / 2, Camera.main.ScreenToWorldPoint(transform.position).y - palikanKoko / 2);
		Vector3 hiirenPaikka = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		if(hiirenPaikka.x > oikeaPaikka.x && hiirenPaikka.x < oikeaPaikka.x + palikanKoko && hiirenPaikka.y > oikeaPaikka.y && hiirenPaikka.y < oikeaPaikka.y + palikanKoko){
			return true;
		}
		return false;
	}
}

