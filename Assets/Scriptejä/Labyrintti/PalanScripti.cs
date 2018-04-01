using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PalanScripti : MonoBehaviour {

	int mapinKoko = 10;
	public GameObject maali;
	GameObject muuriPalikka;

	void Awake (){
		muuriPalikka = gameObject.transform.Find ("MuuriPalikka0").gameObject;
		mapinKoko = GameObject.Find ("Luo Labyrintti").GetComponent<LuoLabyrintti>().labyrintinKoko;
	}

	void Update (){
		int palikanX = Mathf.FloorToInt (transform.position.x / 10);
		int palikanY = Mathf.FloorToInt (transform.position.y / 10);
		if (palikanX == mapinKoko - 1 && palikanY == mapinKoko - 1 && muuriPalikka.activeSelf){
			muuriPalikka.SetActive (false);
			Instantiate (maali, transform.position + new Vector3(5, 5, 3), Quaternion.identity);
		}
	}
}