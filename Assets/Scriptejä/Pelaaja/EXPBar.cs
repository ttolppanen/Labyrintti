using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EXPBar : MonoBehaviour {

	Slider slideri;
	public GameObject ukko;
	Levelit lvlData;

	void Awake () {
		slideri = GetComponent<Slider> ();
		lvlData = ukko.GetComponent<Levelit> ();
	}
	

	void Update () {
		float maxEXP = lvlData.lvlUpRaja;
		float nytEXP = lvlData.experience;
		slideri.maxValue = maxEXP;
		slideri.value = nytEXP;
	}
}
