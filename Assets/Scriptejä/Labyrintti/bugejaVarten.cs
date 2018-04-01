using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bugejaVarten : MonoBehaviour {


	void Update () {
		Debug.Log (Camera.main.ScreenToWorldPoint (transform.position));
	}
}
