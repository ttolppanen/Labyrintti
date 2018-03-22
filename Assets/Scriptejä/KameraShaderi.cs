using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]

public class KameraShaderi : MonoBehaviour {

	public Material materiaali;
	public GameObject ukko;
	public GameObject kameranReuna;
	float sateenKulma = 0;
	int objNum = 0;
	List<Vector4> listaReunoista = new List<Vector4>();
	Collider2D osuma;
	Collider2D uusiOsuma;
	Vector2 sateenSuunta;
	RaycastHit2D vanhaSade;

	void Start () {
		Vector2 sateenSuunta = new Vector2 (Mathf.Cos ((sateenKulma + 90) * Mathf.Deg2Rad), Mathf.Sin ((sateenKulma + 90) * Mathf.Deg2Rad));
		RaycastHit2D sade = Physics2D.Raycast (ukko.transform.position, sateenSuunta, 15, LayerMask.GetMask ("Varjot"));
		osuma = sade.collider;
		vanhaSade = sade;
	}

	void Update(){
		while(sateenKulma < 360){
			sateenKulma += 1f;
			sateenSuunta = new Vector2 (Mathf.Cos((sateenKulma + 90) * Mathf.Deg2Rad), Mathf.Sin((sateenKulma + 90) * Mathf.Deg2Rad));
			RaycastHit2D uusiSade = Physics2D.Raycast (ukko.transform.position, sateenSuunta, 15, LayerMask.GetMask("Varjot"));
			uusiOsuma = uusiSade.collider;
			if (uusiOsuma != osuma){
				if (uusiSade.point.x != 0 && uusiSade.point.y != 0){
					Vector2 oikeaPaikka = new Vector2((uusiSade.point.x - kameranReuna.transform.position.x) / 23, (uusiSade.point.y - kameranReuna.transform.position.y) / 13);
					listaReunoista.Add (new Vector4(oikeaPaikka.x, oikeaPaikka.y, sateenKulma, objNum));
				}
				else{
					Vector2 oikeaPaikka = new Vector2((vanhaSade.point.x - kameranReuna.transform.position.x) / 23, (vanhaSade.point.y - kameranReuna.transform.position.y) / 13);
					listaReunoista.Add (new Vector4(oikeaPaikka.x, oikeaPaikka.y, sateenKulma, objNum));
				}
				if (listaReunoista.Count < 2 || listaReunoista[listaReunoista.Count - 1] != listaReunoista[listaReunoista.Count - 2]){
					objNum += 1;
				}
			}
			osuma = uusiOsuma;
			vanhaSade = uusiSade;
		}
		if (listaReunoista.Count != 0){//turhia bugejaa zzzz
			float loppuIndeksi = listaReunoista[listaReunoista.Count - 1].w;
			for (int i = 0; i < listaReunoista.Count; i++){
				if (listaReunoista[i].w == loppuIndeksi){
					listaReunoista [i] = new Vector4(listaReunoista [i].x, listaReunoista [i].y, listaReunoista [i].z, 0);
				}	
			}
		}
		materiaali.SetFloat("_ListanPituus", listaReunoista.Count);
		if (listaReunoista.Count < 20){//turhia bugejaa zzzz
			for(int i = 0; i < 20 - listaReunoista.Count; i++){
				listaReunoista.Add (Vector4.zero);
			}
		}
		materiaali.SetFloat("_ListanPituus", listaReunoista.Count);
		materiaali.SetVectorArray ("reunaPisteet", listaReunoista);
		listaReunoista.Clear ();
		sateenKulma = 0;
		objNum = 0;
	}

	void OnRenderImage(RenderTexture src, RenderTexture dest){
		Graphics.Blit (src, dest, materiaali);
	}
}
