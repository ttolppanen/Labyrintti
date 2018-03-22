using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KukanScripti : MonoBehaviour {

	void Awake () {
		if (Random.Range(0f, 1f) > 0.01f){
			Destroy (gameObject);
		}
	}
}