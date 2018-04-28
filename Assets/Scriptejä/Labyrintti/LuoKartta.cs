using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LuoKartta : MonoBehaviour {

	public int kartanPikseliMaara;
	public Color kolmionVari;
	public Color nelionVari;
	public Color ympyranVari;

	public float skaalaus;
	List<List<GameObject>> kartanPalaset = new List<List<GameObject>>();
	List<List<List<int>>> kartanData = new List<List<List<int>>>();
	List<List<int>> maaMerkit = new List<List<int>> ();
	int labyrintinKoko;
	public bool onkoKarttaLuotu = false;

	void Update () {
		if(!onkoKarttaLuotu && GameObject.Find ("Luo Labyrintti").GetComponent<LuoLabyrintti> ().labyrintinData != null){
			onkoKarttaLuotu = true;
			kartanData = GameObject.Find ("Luo Labyrintti").GetComponent<LuoLabyrintti> ().labyrintinData;
			labyrintinKoko = GameObject.Find ("Luo Labyrintti").GetComponent<LuoLabyrintti> ().labyrintinKoko;
			maaMerkit = GameObject.Find ("Luo Labyrintti").GetComponent<LuoLabyrintti> ().maaMerkit;

			Texture2D tekstuuri = new Texture2D (labyrintinKoko * kartanPikseliMaara, labyrintinKoko * kartanPikseliMaara); //Ennen oli 6 kartanpikselien paikalla, vakiona pikselit oli 6, ja skaalaus 8
			GetComponent<RawImage> ().texture = tekstuuri;
			Color vari = Color.black;

			for (int y = 0; y < labyrintinKoko; y++) {
				for (int x = 0; x < labyrintinKoko; x++) { 
					for (int iy = 0; iy < kartanPikseliMaara; iy++) {
						for (int ix = 0; ix < kartanPikseliMaara; ix++) {
							tekstuuri.SetPixel (x * kartanPikseliMaara + ix, y * kartanPikseliMaara + iy, Color.clear);
						}
					}
				}
			}
			for (int y = 0; y < labyrintinKoko; y++) { // Ilmeisesti kulma palat?
				for (int x = 0; x < labyrintinKoko; x++) { 
					for (int iy = 0; iy < 2; iy++) {
						for (int ix = 0; ix < 2; ix++) { 
							tekstuuri.SetPixel (x * kartanPikseliMaara + ix * (kartanPikseliMaara - 1), y * kartanPikseliMaara + iy * (kartanPikseliMaara - 1), vari);
						}
					}
					if (kartanData [y] [x] [0] == 1) {//Ilmeisesti muurin seinät?
						for(int i = 1; i < (kartanPikseliMaara - 1); i++){
							tekstuuri.SetPixel (x * kartanPikseliMaara + (kartanPikseliMaara - 1), y * kartanPikseliMaara + i, vari);
						}
					}
					if (kartanData [y] [x] [1] == 1) {
						for(int i = 1; i < (kartanPikseliMaara - 1); i++){
							tekstuuri.SetPixel (x * kartanPikseliMaara + i, y * kartanPikseliMaara + (kartanPikseliMaara - 1), vari);
						}
					}
					if (kartanData [y] [x] [2] == 1) {
						for(int i = 1; i < (kartanPikseliMaara - 1); i++){
							tekstuuri.SetPixel (x * kartanPikseliMaara, y * kartanPikseliMaara + i, vari);
						}
					}
					if (kartanData [y] [x] [3] == 1) {
						for(int i = 1; i < (kartanPikseliMaara - 1); i++){
							tekstuuri.SetPixel (x * kartanPikseliMaara + i, y * kartanPikseliMaara, vari);
						}
					}
				}
			}

			//Maamerkit
			for (int y = 0; y < labyrintinKoko; y++) {
				for (int x = 0; x < labyrintinKoko; x++) {
					if(maaMerkit[y][x] == 3){
						teeKolmio (tekstuuri, new Vector2(x * kartanPikseliMaara, y * kartanPikseliMaara), kolmionVari);
					}
					else if(maaMerkit[y][x] == 2){
						teeNelio(tekstuuri, new Vector2(x * kartanPikseliMaara, y * kartanPikseliMaara), nelionVari);
					}
					else if(maaMerkit[y][x] == 1){
						teeYmpyra(tekstuuri, new Vector2(x * kartanPikseliMaara, y * kartanPikseliMaara), ympyranVari);
					}
				}
			}

			tekstuuri.filterMode = FilterMode.Point;
			tekstuuri.Apply ();
			//Sprite uusiSprite = Sprite.Create (tekstuuri, new Rect (0.0f, 0.0f, tekstuuri.width, tekstuuri.height), new Vector2 (0, 0), labyrintinKoko * kartanPikseliMaara / skaalaus); //Scaalaus, 8 on leveys tai pituus :&&
			GetComponent<RawImage> ().texture = tekstuuri;
		}
	}

	void teeKolmio(Texture2D tex, Vector2 paikka, Color vari){
		Vector2 keskiPaikka = new Vector2 (paikka.x + Mathf.RoundToInt(kartanPikseliMaara / 2), paikka.y + Mathf.RoundToInt(kartanPikseliMaara / 2));
		for (int iy = -1; iy < 2; iy++) {
			for(int ix = iy - 1; ix < Mathf.Abs(iy - 1) + 1; ix++){
				tex.SetPixel (Mathf.RoundToInt(keskiPaikka.x) + ix, Mathf.RoundToInt(keskiPaikka.y) + iy, vari);
			}
		}
	}

	void teeNelio(Texture2D tex, Vector2 paikka, Color vari){
		Vector2 keskiPaikka = new Vector2 (paikka.x + Mathf.RoundToInt(kartanPikseliMaara / 2), paikka.y + Mathf.RoundToInt(kartanPikseliMaara / 2));
		for (int iy = -2; iy < 3; iy++) {
			for(int ix = -2; ix < 3; ix++){
				tex.SetPixel (Mathf.RoundToInt(keskiPaikka.x) + ix, Mathf.RoundToInt(keskiPaikka.y) + iy, vari);
			}
		}
	}

	void teeYmpyra(Texture2D tex, Vector2 paikka, Color vari){
		Vector2 keskiPaikka = new Vector2 (paikka.x + Mathf.RoundToInt(kartanPikseliMaara / 2), paikka.y + Mathf.RoundToInt(kartanPikseliMaara / 2));
		for (int iy = -2; iy < 3; iy++) {
			if (Mathf.Abs (iy) == 2) {
				for (int ix = -1; ix < 2; ix++) {
					tex.SetPixel (Mathf.RoundToInt (keskiPaikka.x) + ix, Mathf.RoundToInt (keskiPaikka.y) + iy, vari);
				}
			} 
			else {
				for (int ix = -2; ix < 3; ix++) {
					tex.SetPixel (Mathf.RoundToInt (keskiPaikka.x) + ix, Mathf.RoundToInt (keskiPaikka.y) + iy, vari);
				}
			}
		}
	}
}