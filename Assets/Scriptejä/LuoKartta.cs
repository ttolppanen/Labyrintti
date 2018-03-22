using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LuoKartta : MonoBehaviour {

	List<List<GameObject>> kartanPalaset = new List<List<GameObject>>();
	List<List<List<int>>> kartanData = new List<List<List<int>>>();
	int labyrintinKoko;

	void Awake () {
		kartanData = GameObject.Find ("Luo Labyrintti").GetComponent<LuoLabyrintti> ().labyrintinData;
		labyrintinKoko = GameObject.Find ("Luo Labyrintti").GetComponent<LuoLabyrintti> ().labyrintinKoko;

		Texture2D tekstuuri = new Texture2D (labyrintinKoko * 6, labyrintinKoko * 6);
		GetComponent<SpriteRenderer> ().material.mainTexture = tekstuuri;
		Color vari = Color.black;
		for (int y = 0; y < labyrintinKoko; y++) {
			for (int x = 0; x < labyrintinKoko; x++) { 
				for (int iy = 0; iy < 6; iy++) {
					for (int ix = 0; ix < 6; ix++) {
						tekstuuri.SetPixel (x * 6 + ix, y * 6 + iy, Color.clear);
					}
				}
			}
		}
		for (int y = 0; y < labyrintinKoko; y++) {
			for (int x = 0; x < labyrintinKoko; x++) { 
				for (int iy = 0; iy < 2; iy++) {
					for (int ix = 0; ix < 2; ix++) {
						tekstuuri.SetPixel (x * 6 + ix * 5, y * 6 + iy * 5, vari);
					}
				}
				if (kartanData [y] [x] [0] == 1) {
					tekstuuri.SetPixel (x * 6 + 5, y * 6 + 1, vari);
					tekstuuri.SetPixel (x * 6 + 5, y * 6 + 2, vari);
					tekstuuri.SetPixel (x * 6 + 5, y * 6 + 3, vari);
					tekstuuri.SetPixel (x * 6 + 5, y * 6 + 4, vari);
				}
				if (kartanData [y] [x] [1] == 1) {
					tekstuuri.SetPixel (x * 6 + 1, y * 6 + 5, vari);
					tekstuuri.SetPixel (x * 6 + 2, y * 6 + 5, vari);
					tekstuuri.SetPixel (x * 6 + 3, y * 6 + 5, vari);
					tekstuuri.SetPixel (x * 6 + 4, y * 6 + 5, vari);
				}
				if (kartanData [y] [x] [2] == 1) {
					tekstuuri.SetPixel (x * 6, y * 6 + 1, vari);
					tekstuuri.SetPixel (x * 6, y * 6 + 2, vari);
					tekstuuri.SetPixel (x * 6, y * 6 + 3, vari);
					tekstuuri.SetPixel (x * 6, y * 6 + 4, vari);
				}
				if (kartanData [y] [x] [3] == 1) {
					tekstuuri.SetPixel (x * 6 + 1, y * 6, vari);
					tekstuuri.SetPixel (x * 6 + 2, y * 6, vari);
					tekstuuri.SetPixel (x * 6 + 3, y * 6, vari);
					tekstuuri.SetPixel (x * 6 + 4, y * 6, vari);
				}
			}
		}
		tekstuuri.filterMode = FilterMode.Point;
		tekstuuri.Apply ();
		Sprite uusiSprite = Sprite.Create (tekstuuri, new Rect (0.0f, 0.0f, tekstuuri.width, tekstuuri.height), new Vector2 (0, 0), labyrintinKoko * 6 / 8f); //Scaalaus, 8 on leveys tai pituus :&&
		GetComponent<SpriteRenderer> ().sprite = uusiSprite;
	}
}