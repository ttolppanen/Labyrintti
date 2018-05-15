using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vesi : MonoBehaviour {

	public Material mat;
	public Material nmMat;
	public Material saroMat;
	public Texture alkuNM;
	public float aaltoAika;
	public float uudenPisteenRaja;
	RenderTexture nmText;
	Vector3 viimeUkonPaikka;
	Vector4[] pisteet = new Vector4[30];

	void Awake () {
		nmText = new RenderTexture (100, 100, 32, RenderTextureFormat.ARGB32);
		viimeUkonPaikka = transform.position + new Vector3 (2.5f, 2.5f, 2.5f);
		for(int i = 0; i < pisteet.Length; i++){
			pisteet[i] = new Vector4 (0, 0, 0, 1);
		}
	}

	void Update () {
		lisaaAikaa (Time.deltaTime);
		nmMat.SetVectorArray ("pisteet", pisteet);
		Graphics.Blit (alkuNM, nmText, nmMat);
		Graphics.Blit (nmText, saroMat);
		mat.SetTexture ("_BumpMap", nmText);
	}



	void OnTriggerEnter2D(Collider2D coll){
		if(coll.tag == "Ukko"){
			viimeUkonPaikka = coll.transform.position;
			lisaaPisteisiin (viimeUkonPaikka);
		}
	}
	void OnTriggerStay2D(Collider2D coll){
		if(coll.tag == "Ukko"){
			if((coll.transform.position - viimeUkonPaikka).magnitude > uudenPisteenRaja){
				viimeUkonPaikka = coll.transform.position;
				lisaaPisteisiin (viimeUkonPaikka);
			}
		}
	}
	void OnTriggerLeave2D(Collider2D coll){
		if(coll.tag == "Ukko"){
			viimeUkonPaikka = transform.position + new Vector3 (2.5f, 2.5f, 2.5f);
		}
	}



	void lisaaPisteisiin(Vector4 piste){
		piste = new Vector4(viimeUkonPaikka.x, viimeUkonPaikka.y, 0, 0);
		piste = (piste - new Vector4(transform.position.x, transform.position.y, 0, 0)) / 5.0f * transform.localScale.x;
		for(int i = 0; i < pisteet.Length; i++){
			if(pisteet[i].w == 1){
				pisteet [i] = piste;
				break;
			}
		}
	}
	void lisaaAikaa(float lisattavaAika){
		for(int i = 0; i < pisteet.Length; i++){
			if (pisteet [i].w != 1) {
				pisteet [i].z += lisattavaAika;
				if(pisteet[i].z > aaltoAika){
					pisteet [i].w = 1;
				}
			} 
		}
	}
}
