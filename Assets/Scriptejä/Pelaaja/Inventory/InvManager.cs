using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvManager : MonoBehaviour {

	public GameObject kartta;
	public GameObject inventory;

	public void karttaPaalle(){
		kartta.SetActive (true);
		inventory.SetActive (false);
	}

	public void inventoryPaalle(){
		kartta.SetActive (false);
		inventory.SetActive (true);
	}

}
