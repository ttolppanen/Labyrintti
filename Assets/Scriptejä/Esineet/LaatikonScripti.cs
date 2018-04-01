using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaatikonScripti : MonoBehaviour {

	public GameObject[] spawnattavatEsineet = new GameObject[1];
	public float todennakoisyysSpawnata;

	void Update () {
		if(GetComponent<Elama> ().kuollut){
			if(todennakoisyysSpawnata > Random.Range(0f, 1f)){
				Instantiate (spawnattavatEsineet[Random.Range(0, spawnattavatEsineet.Length)] , transform.position, Quaternion.identity);
			}
			Destroy (transform.parent.gameObject);
		}
	}
}
