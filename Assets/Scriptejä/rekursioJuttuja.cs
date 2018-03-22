using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rekursioJuttuja : MonoBehaviour {

	public GameObject avain;
	public int porttienMaara;
	List<List<List<int>>> kartanData = new List<List<List<int>>>();
	List<List<int>> karttaTarkistettavistaPisteista = new List<List<int>> ();
	List<Vector2> valmisLista;
	LuoLabyrintti labyrintinScripti;
	int labyrintinKoko;

	void Awake () {
		labyrintinScripti = GameObject.Find ("Luo Labyrintti").GetComponent<LuoLabyrintti> ();
		kartanData = labyrintinScripti.labyrintinData;
		labyrintinKoko = GameObject.Find ("Luo Labyrintti").GetComponent<LuoLabyrintti> ().labyrintinKoko;
		for (int y = 0; y < labyrintinKoko; y++) {
			karttaTarkistettavistaPisteista.Add (new List<int> ());
			for (int x = 0; x < labyrintinKoko; x++) { 
				karttaTarkistettavistaPisteista [y].Add (1);
			}
		}
		LapaistaanKentta (0, 0, new List<Vector2> ()); //Valmis lista
		for (int i = 0; i < porttienMaara; i++){
			int portinIndeksi = Mathf.RoundToInt(valmisLista.Count * (1 + i)/ (porttienMaara + 1));
			Vector2 portinPaikka = valmisLista[portinIndeksi];
			Vector2 aiempiPaikka = valmisLista [portinIndeksi - 1];
			Vector2 mihinPortti = portinPaikka - aiempiPaikka;
			kartanData[Mathf.RoundToInt(aiempiPaikka.y)][Mathf.RoundToInt(aiempiPaikka.x)][OikeaSeinaLukkoon(mihinPortti)] = 2;
			List<Vector2> avaimenPaikat = HaetaanAvainRuudut(Mathf.RoundToInt(aiempiPaikka.x), Mathf.RoundToInt(aiempiPaikka.y), new List<Vector2>());
			Vector2 avaimenPaikka = avaimenPaikat[Random.Range(0, avaimenPaikat.Count)];
			avain = Instantiate(avain, new Vector3(avaimenPaikka.x * 10 + 5, avaimenPaikka.y * 10 + 5, 4f), Quaternion.identity);
			avain.name = aiempiPaikka.x.ToString() + aiempiPaikka.y.ToString();
		}
	}

	void LapaistaanKentta (int x, int y, List<Vector2> tarkistusLista){
		if (x == labyrintinKoko - 1 && y == labyrintinKoko - 1){
			valmisLista = tarkistusLista;
			return;
		}
		tarkistusLista.Add (new Vector2(x, y));
		karttaTarkistettavistaPisteista[y][x] = 2;
		if (kartanData[y][x][0] == 0 && x + 1 < labyrintinKoko && karttaTarkistettavistaPisteista[y][x + 1] != 2){
			LapaistaanKentta (x + 1, y, new List<Vector2>(tarkistusLista));
		}
		if (kartanData[y][x][1] == 0&& y + 1 < labyrintinKoko && karttaTarkistettavistaPisteista[y + 1][x] != 2){
			LapaistaanKentta (x, y + 1, new List<Vector2>(tarkistusLista));
		}
		if (kartanData[y][x][2] == 0&& x - 1 >= 0 && karttaTarkistettavistaPisteista[y][x - 1] != 2){
			LapaistaanKentta (x - 1, y, new List<Vector2>(tarkistusLista));
		}
		if (kartanData[y][x][3] == 0 && y - 1 >= 0 && karttaTarkistettavistaPisteista[y - 1][x] != 2){
			LapaistaanKentta (x, y - 1, new List<Vector2>(tarkistusLista));
		}
	}
	List<Vector2> HaetaanAvainRuudut (int x, int y, List<Vector2> tarkistusLista){
		tarkistusLista.Add (new Vector2(x, y));
		karttaTarkistettavistaPisteista[y][x] = 3;
		if (kartanData[y][x][0] == 0 && x + 1 < labyrintinKoko && karttaTarkistettavistaPisteista[y][x + 1] != 3){
			HaetaanAvainRuudut (x + 1, y, tarkistusLista);
		}
		if (kartanData[y][x][1] == 0 && y + 1 < labyrintinKoko && karttaTarkistettavistaPisteista[y + 1][x] != 3){
			HaetaanAvainRuudut (x, y + 1, tarkistusLista);
		}
		if (kartanData[y][x][2] == 0 && x - 1 >= 0 && karttaTarkistettavistaPisteista[y][x - 1] != 3){
			HaetaanAvainRuudut(x - 1, y, tarkistusLista);
		}
		if (kartanData[y][x][3] == 0 && y - 1 >= 0 && karttaTarkistettavistaPisteista[y - 1][x] != 3){
			HaetaanAvainRuudut (x, y - 1, tarkistusLista);
		}
		return tarkistusLista;
	}
	int OikeaSeinaLukkoon (Vector2 paikka){//MATEMAATTISESTI OIKEA PORTIN PAIKKA LOLOLOL K^X + C MISSÄ K = 0.4142 ja C -0.4142 tai 0.5858
		return Mathf.RoundToInt((Mathf.Pow(0.4142f, paikka.x) - 0.4142f) * Mathf.Pow(paikka.x, 2) + (Mathf.Pow(0.4142f, paikka.y) + 0.5858f) * Mathf.Pow(paikka.y, 2));	
	}
}