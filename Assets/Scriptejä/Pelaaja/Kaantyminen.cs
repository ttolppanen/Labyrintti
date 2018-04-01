using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kaantyminen : MonoBehaviour {



	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 suuntaHiireen = Camera.main.ScreenToWorldPoint (Input.mousePosition) - transform.position;
		float suuntaKulma = Mathf.Atan2 (suuntaHiireen.y, suuntaHiireen.x);
		transform.rotation = Quaternion.Euler (0, 0, suuntaKulma * Mathf.Rad2Deg - 90);
	}
}
