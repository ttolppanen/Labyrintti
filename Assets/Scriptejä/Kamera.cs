using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kamera : MonoBehaviour {

	public GameObject ukko;

	void Update () {
		transform.position = new Vector3(ukko.transform.position.x, ukko.transform.position.y, -10);
	}
}
