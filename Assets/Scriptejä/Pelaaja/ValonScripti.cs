using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ValonScripti : MonoBehaviour {

	public GameObject valonPaikka;
	public float flickkausAika;
	public float flickkausMinVoimakkuus;
	public float lerppausKerroin;
	float flickkausMaxVoimakkuus;
	float flickataanko = 0;
	Light valo;

	void Awake () {
		valo = GetComponent<Light> ();
		flickkausMaxVoimakkuus = valo.intensity;
	}

	void FixedUpdate () {
		if (flickataanko > flickkausAika) {
			valo.intensity = Random.Range (flickkausMinVoimakkuus, flickkausMaxVoimakkuus);
			flickataanko = 0;
		} 
		else {
			flickataanko += Time.deltaTime;
		}
		transform.position = Vector3.Lerp (transform.position, valonPaikka.transform.position, lerppausKerroin);
	}
}
