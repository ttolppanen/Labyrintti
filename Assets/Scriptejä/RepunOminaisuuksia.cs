using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepunOminaisuuksia : MonoBehaviour {

	public float palikanKoko;
	public bool onkoKannossa;
	public Vector2 repunAlareuna;

	void Awake(){
		onkoKannossa = false;
		repunAlareuna = new Vector2 (transform.position.x - palikanKoko / 2, transform.position.y - palikanKoko / 2);
	}


}