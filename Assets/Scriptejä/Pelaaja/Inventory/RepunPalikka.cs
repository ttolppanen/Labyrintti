﻿using System.Collections;
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
					int[] uudetIndeksit = new int[]{ int.Parse(pudotusPaikka.name.Substring (0, 1)), int.Parse(pudotusPaikka.name.Substring (1, 1)) };
					int[] tamanIndeksit = new int[]{ int.Parse(gameObject.name.Substring (0, 1)), int.Parse(gameObject.name.Substring (1, 1)) };
					GameObject uusiItemi = esineReppu [uudetIndeksit[0], uudetIndeksit[1]];
					EsineenOminaisuudet tamaItemiOm = (esineReppu [tamanIndeksit[0], tamanIndeksit[1]] != null) ? esineReppu [tamanIndeksit[0], tamanIndeksit[1]].GetComponent<EsineenOminaisuudet> () : null;
					EsineenOminaisuudet uusiItemiOm = (uusiItemi != null) ? uusiItemi.GetComponent<EsineenOminaisuudet> () : null;

					//Minne siirretään tarkistetaan
					if (uudetIndeksit[0] == 4 && tamaItemiOm != null){
						if(uudetIndeksit[1] == 2 && !tamaItemiOm.onkoAse){
							break;
						}
						if(uudetIndeksit[1] == 0 && !tamaItemiOm.onkoKypara){
							break;
						}
						if(uudetIndeksit[1] == 1 && !tamaItemiOm.onkoHaarniska){
							break;
						}
						if(uudetIndeksit[1] == 3 && !tamaItemiOm.onkoHousut){
							break;
						}
					}

					//Mistä siirretään tarkistetaan
					if (tamanIndeksit[0] == 4 && uusiItemiOm != null){
						if(tamanIndeksit[1] == 2 && !uusiItemiOm.onkoAse){
							break;
						}
						if(tamanIndeksit[1] == 0 && !uusiItemiOm.onkoKypara){
							break;
						}
						if(tamanIndeksit[1] == 1 && !uusiItemiOm.onkoHaarniska){
							break;
						}
						if(tamanIndeksit[1] == 3 && !uusiItemiOm.onkoHousut){
							break;
						}
					}
						
					esineReppu [uudetIndeksit[0], uudetIndeksit[1]] = esineReppu [tamanIndeksit[0], tamanIndeksit[1]];
					esineReppu [tamanIndeksit[0], tamanIndeksit[1]] = uusiItemi;
					loytyko = true;
					rOm.onkoKannossa = false;
					olenKannossa = false;
					break;
				}
			}
			if (!loytyko) {
				minunKuva.transform.position = transform.position;
				rOm.onkoKannossa = false;
				olenKannossa = false;
				if (osumat.Length == 0) {
					GameObject esineRepussa = esineReppu [int.Parse (gameObject.name.Substring (0, 1)), int.Parse (gameObject.name.Substring (1, 1))];
					esineRepussa.transform.position = new Vector3 (ukko.transform.position.x, ukko.transform.position.y, esineRepussa.transform.position.z) + new Vector3 (0, 2, 0);
					esineRepussa.transform.parent = null;
					esineRepussa.tag = "EsineMaassa";
					esineReppu [int.Parse (gameObject.name.Substring (0, 1)), int.Parse (gameObject.name.Substring (1, 1))] = null;
				}
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
		//Esineiden käyttö
		if(ollaankoSisalla() && Input.GetMouseButtonDown(1) && esineReppu [int.Parse (gameObject.name.Substring (0, 1)), int.Parse (gameObject.name.Substring (1, 1))].GetComponent<EsineenOminaisuudet>().onkoKaytettava){
			esineReppu [int.Parse (gameObject.name.Substring (0, 1)), int.Parse (gameObject.name.Substring (1, 1))].SendMessage ("kaytaEsine");
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

