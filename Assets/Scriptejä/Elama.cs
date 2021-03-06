﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Elama : MonoBehaviour {

	public float HP;
	public float maxHP;
	public bool kuollut;
	public GameObject dmgTeksti;
	public GameObject veri;
	GameObject ukko;
	public AudioClip osumisAani;
	public bool ollaankoJaadyksissa;
	public float dmgTekstiOdotusAika;
	float dmgTekstiAika;
	float jaadytysAika;
	float tonaisyKerroin;
	string vahinkoTeksti;
	AudioSource aanet;
	Rigidbody2D rb;

	void Awake () {
		kuollut = false;
		aanet = GetComponent<AudioSource> ();
		rb = GetComponent<Rigidbody2D> ();
		jaadytysAika = -1;
		ollaankoJaadyksissa = false;
		maxHP = HP;
		ukko = GameObject.FindGameObjectWithTag ("Ukko");
		tonaisyKerroin = 1f;
	}

	void Update(){
		if (jaadytysAika > 0) {
			rb.constraints = RigidbodyConstraints2D.FreezeAll;
			jaadytysAika -= Time.deltaTime;
			ollaankoJaadyksissa = true;
		} 
		else {
			rb.constraints = RigidbodyConstraints2D.None;
			ollaankoJaadyksissa = false;
		}

		//Dmg teksti
		if(dmgTeksti == null){
			return;
		}
		if (dmgTekstiAika > 0) {
			dmgTeksti.SetActive (true);
			dmgTeksti.GetComponent<Text> ().text = vahinkoTeksti;
			dmgTekstiAika -= Time.deltaTime;
		} else {
			dmgTeksti.SetActive (false);
		}
	}

	public void OtaVahinkoa(float vahinko, float aika){
		OlionOminaisuudet oliOm = GetComponent<OlionOminaisuudet> ();
		HP -= Mathf.CeilToInt(vahinko * (1 - oliOm.defence / 100)) ;
		aanet.clip = osumisAani;
		aanet.Play ();
		jaadytysAika = aika;
		dmgTekstiAika = dmgTekstiOdotusAika;
		if(tag != "Ukko"){
			dmgTeksti.transform.position = transform.position + new Vector3 (0, 1, -1);
		}
		vahinkoTeksti = Mathf.Round(vahinko).ToString ();
		veri.GetComponent<ParticleSystem> ().Play ();
		Vector3 tonaisySuunta = new Vector3(transform.position.x, transform.position.y, 0) - new Vector3(ukko.transform.position.x, ukko.transform.position.y, 0);
		rb.AddForce (tonaisyKerroin * Mathf.Sqrt(vahinko) * tonaisySuunta, ForceMode2D.Impulse);
		if (HP <= 0){
			kuollut = true;
			ukko.GetComponent<Levelit> ().experience += GetComponent<OlionOminaisuudet> ().expAnto;
		}
	}
}