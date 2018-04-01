using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Varjot : MonoBehaviour {

	public Material materiaali;
	public GameObject ukko;
	public GameObject kameranReuna;
	public float tarkistusSateenPituus;
	float sateenKulma = 0;
	int objNum = 0;
	List<Vector4> listaReunoista = new List<Vector4>();
	Collider2D osuma;
	Collider2D uusiOsuma;
	Collider2D ekaOsuma;
	Vector2 sateenSuunta;
	RaycastHit2D vanhaSade;

	void Update(){
		sateenSuunta = new Vector2 (Mathf.Cos ((sateenKulma ) * Mathf.Deg2Rad), Mathf.Sin ((sateenKulma ) * Mathf.Deg2Rad));
		RaycastHit2D sade = Physics2D.Raycast (ukko.transform.position, sateenSuunta, tarkistusSateenPituus, LayerMask.GetMask ("Varjot"));
		osuma = sade.collider;
		ekaOsuma = sade.collider;
		vanhaSade = sade;
		while(sateenKulma < 360){
			sateenKulma += 10f;
			sateenSuunta = new Vector2 (Mathf.Cos((sateenKulma) * Mathf.Deg2Rad), Mathf.Sin((sateenKulma) * Mathf.Deg2Rad));
			RaycastHit2D uusiSade = Physics2D.Raycast (ukko.transform.position, sateenSuunta, tarkistusSateenPituus, LayerMask.GetMask("Varjot"));
			uusiOsuma = uusiSade.collider;
			if(uusiOsuma != osuma){
				if (uusiOsuma != null) {
					if (osuma != null) {
						Vector2 oikeaPaikka = new Vector2 ((vanhaSade.point.x - kameranReuna.transform.position.x) / 23, (vanhaSade.point.y - kameranReuna.transform.position.y) / 13);
						listaReunoista.Add (new Vector4 (oikeaPaikka.x, oikeaPaikka.y, sateenKulma, 1));
						oikeaPaikka = new Vector2 ((uusiSade.point.x - kameranReuna.transform.position.x) / 23, (uusiSade.point.y - kameranReuna.transform.position.y) / 13);
						listaReunoista.Add (new Vector4 (oikeaPaikka.x, oikeaPaikka.y, sateenKulma, 1));
					} 
					else {
						Vector2 oikeaPaikka = new Vector2 ((uusiSade.point.x - kameranReuna.transform.position.x) / 23, (uusiSade.point.y - kameranReuna.transform.position.y) / 13);
						listaReunoista.Add (new Vector4 (oikeaPaikka.x, oikeaPaikka.y, sateenKulma, 0));
						listaReunoista.Add (new Vector4 (oikeaPaikka.x, oikeaPaikka.y, sateenKulma, 1));
					}
				} else {
					Vector2 oikeaPaikka = new Vector2 ((vanhaSade.point.x - kameranReuna.transform.position.x) / 23, (vanhaSade.point.y - kameranReuna.transform.position.y) / 13);
					listaReunoista.Add (new Vector4 (oikeaPaikka.x, oikeaPaikka.y, sateenKulma, 1));
					listaReunoista.Add (new Vector4 (oikeaPaikka.x, oikeaPaikka.y, sateenKulma, 0));
				}
			}
			/*if(uusiOsuma != osuma){
				if (uusiOsuma != null) {
					if (osuma != null) {
						Vector2 oikeaPaikka = new Vector2 ((vanhaSade.point.x - kameranReuna.transform.position.x) / 23, (vanhaSade.point.y - kameranReuna.transform.position.y) / 13);
						listaReunoista.Add (new Vector4 (oikeaPaikka.x, oikeaPaikka.y, sateenKulma, objNum));
						oikeaPaikka = new Vector2 ((uusiSade.point.x - kameranReuna.transform.position.x) / 23, (uusiSade.point.y - kameranReuna.transform.position.y) / 13);
						listaReunoista.Add (new Vector4 (oikeaPaikka.x, oikeaPaikka.y, sateenKulma, objNum));
					} 
					else {
						Vector2 oikeaPaikka = new Vector2 ((uusiSade.point.x - kameranReuna.transform.position.x) / 23, (uusiSade.point.y - kameranReuna.transform.position.y) / 13);
						objNum += 1;
						listaReunoista.Add (new Vector4 (oikeaPaikka.x, oikeaPaikka.y, sateenKulma, objNum));
					}
				} else {
					Vector2 oikeaPaikka = new Vector2 ((vanhaSade.point.x - kameranReuna.transform.position.x) / 23, (vanhaSade.point.y - kameranReuna.transform.position.y) / 13);
					listaReunoista.Add (new Vector4 (oikeaPaikka.x, oikeaPaikka.y, sateenKulma, objNum));
				}
			}*/
			osuma = uusiOsuma;
			vanhaSade = uusiSade;
		}
		/*if (listaReunoista.Count != 0 && osuma == ekaOsuma && osuma != null){//turhia bugejaa zzzz
			listaReunoista[listaReunoista.Count - 1] = new Vector4(listaReunoista [listaReunoista.Count - 1].x, listaReunoista [listaReunoista.Count - 1].y, listaReunoista [listaReunoista.Count - 1].z, 0);
			/*float loppuIndeksi = listaReunoista [listaReunoista.Count - 1].w;
			for(int i = 0; i < listaReunoista.Count; i++){
				if (listaReunoista[i].w == loppuIndeksi){
					listaReunoista[i] = new Vector4(listaReunoista [i].x, listaReunoista [i].y, listaReunoista [i].z, 0);
				}
			}
		}*/
		materiaali.SetFloat("_ListanPituus", listaReunoista.Count);
		if (listaReunoista.Count < 100){//turhia bugejaa zzzz
			for(int i = 0; i < 100 - listaReunoista.Count; i++){
				listaReunoista.Add (Vector4.zero);
			}
		}
		materiaali.SetVector("_Paikka", new Vector4((ukko.transform.position.x - kameranReuna.transform.position.x) / 23, (ukko.transform.position.y - kameranReuna.transform.position.y) / 13, 0, 0));
		Debug.Log ((ukko.transform.position.x - kameranReuna.transform.position.x) / 23);
		materiaali.SetVectorArray ("reunaPisteet", listaReunoista);
		listaReunoista.Clear ();
		sateenKulma = 0;
		objNum = 0;
	}

	/*if (uusiOsuma != osuma){
				if (uusiSade.point.x != 0 && uusiSade.point.y != 0){
					Vector2 oikeaPaikka = new Vector2((uusiSade.point.x - kameranReuna.transform.position.x) / 23, (uusiSade.point.y - kameranReuna.transform.position.y) / 13);
					listaReunoista.Add (new Vector4(oikeaPaikka.x, oikeaPaikka.y, sateenKulma, objNum));
				}
				else{
					Vector2 oikeaPaikka = new Vector2((vanhaSade.point.x - kameranReuna.transform.position.x) / 23, (vanhaSade.point.y - kameranReuna.transform.position.y) / 13);
					listaReunoista.Add (new Vector4(oikeaPaikka.x, oikeaPaikka.y, sateenKulma, objNum));
				}
				if (osuma == null){
					objNum += 1;
				}
			}*/
}
