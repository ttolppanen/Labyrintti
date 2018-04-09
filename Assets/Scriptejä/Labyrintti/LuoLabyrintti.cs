using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LuoLabyrintti : MonoBehaviour {

	public float iteminSpawnausTodennakoisyys;
	public GameObject[] spawnattavatItemit = new GameObject[1];
	public float maaMerkinSpawnausTodennakoisyys;
	public Sprite[] maaMerkkienSpritet = new Sprite[3]; //0 on ympyrä, 1 neliä, 2 kolmio
	public Material[] maaMerkkienMateriaalit = new Material[3]; //0 on ympyrä, 1 neliä, 2 kolmio

	public float tnKaksiPalaa;
	public float tnKolmePalaa;
	public float tnNeljaPalaa;
	public GameObject labyrintinPala;
	public GameObject portti;
	public GameObject maali;
	public GameObject[] hirviot = new GameObject[2];
	public GameObject loydaLoppu;
	public int labyrintinKoko;
	public int silmienSpawnausMaara;
	public GameObject ukko;
	public GameObject kartta;
	public List<List<GameObject>> labyrintinPalaset = new List<List<GameObject>>();
	public List<List<List<int>>> labyrintinData = new List<List<List<int>>>(); //0 = auki, 1 = muuri, 2 = portti
	List<List<int>> karttaTarkistettavistaPisteista = new List<List<int>>(); // 1 = tarkistettava, 2 = tarkistettu
	List<List<int>> tarkistettavatPisteet = new List<List<int>>();
	List<List<int>> loydetaanTarkistettavatPisteet = new List<List<int>>();
	List<List<bool>> itemienSpawnaus = new List<List<bool>>();
	public List<List<int>> maaMerkit = new List<List<int>>(); //0 ei mitään, 1 ympyrä, 2 neliö, 3 kolmio


	void Awake () {
		//Luodaan listat
		/*for (int y = 0; y < labyrintinKoko; y++) {
			labyrintinPalaset.Add (new List<GameObject>());
			for (int x = 0; x < labyrintinKoko; x++) { 
				labyrintinPalaset[y].Add(Instantiate (labyrintinPala, new Vector3 (x * 10, y * 10, 0), Quaternion.identity));
			}
		}*/
		for (int y = 0; y < labyrintinKoko; y++) {
			labyrintinPalaset.Add (new List<GameObject>());
			for (int x = 0; x < labyrintinKoko; x++) { 
				labyrintinPalaset[y].Add(new GameObject("tyhja"));
			}
		}
		for (int y = 0; y < labyrintinKoko; y++) {
			labyrintinData.Add (new List<List<int>>());
			for (int x = 0; x < labyrintinKoko; x++) { 
				labyrintinData[y].Add(new List<int> {0, 0, 0, 0});
			}
		}
		for (int y = 0; y < labyrintinKoko; y++) {
			karttaTarkistettavistaPisteista.Add (new List<int> ());
			for (int x = 0; x < labyrintinKoko; x++) { 
				karttaTarkistettavistaPisteista [y].Add (1);
			}
		}
		for (int y = 0; y < labyrintinKoko; y++) {
			itemienSpawnaus.Add (new List<bool> ());
			for (int x = 0; x < labyrintinKoko; x++) { 
				if(iteminSpawnausTodennakoisyys > Random.Range(0f, 1f)){
					itemienSpawnaus [y].Add (true);
				}
				else{
					itemienSpawnaus [y].Add (false);
				}
			}
		}
		for (int y = 0; y < labyrintinKoko; y++) {
			maaMerkit.Add (new List<int> ());
			for (int x = 0; x < labyrintinKoko; x++) { 
				if(maaMerkinSpawnausTodennakoisyys > Random.Range(0f, 1f)){
					maaMerkit [y].Add (Random.Range(1, 4));
				}
				else{
					maaMerkit [y].Add (0);
				}
			}
		}
		
		//Värkätään dataa
		int yPaikka = 0;
		int xPaikka = 0;
		tarkistettavatPisteet.Add (new List<int>{labyrintinKoko - 1, labyrintinKoko - 1});

		//Randomi generoi
		foreach (List<List<int>> xLista in labyrintinData){
			foreach (List<int> sisaData in xLista){
				int aukkojenKohdeMaara = LuoOikeaPala ();
				while (true){
					int aukkojenMaara = LaskeAukot (sisaData);
					if (aukkojenMaara <= aukkojenKohdeMaara){
						break;
					}
					int randomiMuuri = Random.Range (0, 4);
					sisaData[randomiMuuri] = 1;
				
					//Tehdään paksut reunat
					if (randomiMuuri == 0 && xPaikka != labyrintinKoko - 1){
						labyrintinData[yPaikka][xPaikka + 1][2] = 1;
					}
					if (randomiMuuri == 1 && yPaikka != labyrintinKoko - 1){
						labyrintinData[yPaikka + 1][xPaikka][3] = 1;
					}
					if (randomiMuuri == 2 && xPaikka != 0){
						labyrintinData[yPaikka][xPaikka - 1][0] = 1;
					}
					if (randomiMuuri == 3 && yPaikka != 0) {
						labyrintinData [yPaikka - 1] [xPaikka] [1] = 1;
					}
				}
				xPaikka += 1;
			}
			yPaikka += 1;
			xPaikka = 0;
		}

		//Ulkoreunat
		for (int y = 0; y < labyrintinKoko; y++){
			for (int x = 0; x < labyrintinKoko; x++){
				if (x == 0){
					labyrintinData [y][x][2] = 1;
				}
				if (x == labyrintinKoko - 1){
					labyrintinData [y][x][0] = 1;
				}
				if (y == 0){
					labyrintinData [y][x][3] = 1;
				}
				if (y == labyrintinKoko - 1){
					labyrintinData [y][x][1] = 1;
				}
			}
		}

		//Tehdään ratkaistava
		while (true){
			bool onkoTarkistettaviaRuutuja = false; //Onko valmis?
			foreach (List<int> xLista in karttaTarkistettavistaPisteista){
				foreach (int sisaData in xLista){
					if (sisaData == 1){
						onkoTarkistettaviaRuutuja = true;
					}
				}
			}
			if (!onkoTarkistettaviaRuutuja) {
				break;
			}
			else {
				if (tarkistettavatPisteet.Count == 0) {//Jos ei ole mitä tarkistaa, niin pitää korjata jonkin piste
					while (true) {
						bool loytyi = false;
						int x = Random.Range (0, labyrintinKoko);
						int y = Random.Range (0, labyrintinKoko);
						for (int i = 0; i < 2; i++) {
							if (x - 1 + i * 2 >= 0 && x - 1 + i * 2 < labyrintinKoko) {
								if (karttaTarkistettavistaPisteista[y][x] == 2 && karttaTarkistettavistaPisteista [y] [x - 1 + i * 2] == 1) {
									loytyi = true;
									labyrintinData [y] [x] [-2 * i + 2] = 0;
									labyrintinData [y] [x - 1 + i * 2] [2 * i] = 0;
								}
							}
						}
						for (int i = 0; i < 2; i++) {
							if (y - 1 + i * 2 >= 0 && y - 1 + i * 2 < labyrintinKoko && !loytyi) {
								if (karttaTarkistettavistaPisteista[y][x] == 2 && karttaTarkistettavistaPisteista [y - 1 + i * 2] [x] == 1) {
									loytyi = true;
									labyrintinData [y] [x] [-2 * i + 3] = 0;
									labyrintinData [y - 1 + i * 2] [x] [2 * i + 1] = 0;
								}
							}
						}
						if (loytyi) {
							tarkistettavatPisteet.Add (new List<int>{x, y});
							karttaTarkistettavistaPisteista [y] [x] = 1;
							break;
						}
					}
				}
				else {
					int x = tarkistettavatPisteet [tarkistettavatPisteet.Count - 1] [0];
					int y = tarkistettavatPisteet [tarkistettavatPisteet.Count - 1] [1];
					tarkistettavatPisteet.RemoveAt (tarkistettavatPisteet.Count - 1);
					for (int i = 0; i < 2; i++) {
						if (x - 1 + i * 2 >= 0 && x - 1 + i * 2 < labyrintinKoko) {
							if (karttaTarkistettavistaPisteista [y] [x - 1 + i * 2] == 1 && ((i == 0 && labyrintinData [y] [x] [2] == 0) || (i == 1 && labyrintinData [y] [x] [0] == 0))) {
								tarkistettavatPisteet.Add (new List<int>{x - 1 + i * 2, y});
							}
						}
					}
					for (int i = 0; i < 2; i++) {
						if (y - 1 + i * 2 >= 0 && y - 1 + i * 2 < labyrintinKoko) {
							if (karttaTarkistettavistaPisteista [y - 1 + i * 2] [x] == 1 && ((i == 0 && labyrintinData [y] [x] [3] == 0) || (i == 1 && labyrintinData [y] [x] [1] == 0))) {
								tarkistettavatPisteet.Add (new List<int>{x, y - 1 + i * 2});
							}
						}
					}
					karttaTarkistettavistaPisteista [y] [x] = 2;
				} 
			}
			//Katsotaan onko yhtään 1 tarkistettavassa listassa
				//ON Katsotaan onko tarkistettava lista tyhjä
					//JOO Selataan listaa kunnes löytyy piste jonka vieressä on tarkistamaton piste ja seinä välissä, otetaan seinä pois ja lisätään tarkistettavalle listalle 
				//EI Listasta tarkistettava piste
				//Onko avoimia paikkoja vieressä?
					//JOO lisätään avoimet paikat tarkistettavalle listalle ja merkataan paikka tarkistetuksi
				//EI tehdä mitään
			//EI valmis
		}
		//Instantiate (kartta, new Vector3(ukko.transform.position.x, ukko.transform.position.y, 0), Quaternion.identity);
		Instantiate (loydaLoppu, Vector3.zero, Quaternion.identity);
		for (int i = 0; i < silmienSpawnausMaara; i++){
			Instantiate (hirviot[Random.Range(0, hirviot.Length)], new Vector3(Random.Range(0, labyrintinKoko) * 10 + 5, Random.Range(0, labyrintinKoko) * 10 + 5, 2.5f), Quaternion.identity);
		}
	}
	
	void Update () {
		int dataPaikka = 0;
		int ukonX = Mathf.FloorToInt (ukko.transform.position.x / 10);
		int ukonY = Mathf.FloorToInt (ukko.transform.position.y / 10);
		for (int iy = 0; iy < 5; iy++) {
			for (int ix = 0; ix < 5; ix++) { 
				if ((ukonY - 2 + iy >= 0 && ukonY - 2 + iy < labyrintinKoko && ukonX - 2 + ix >= 0 && ukonX - 2 + ix < labyrintinKoko) && labyrintinPalaset[ukonY - 2 + iy][ukonX - 2 + ix].name == "tyhja"){
					labyrintinPalaset[ukonY - 2 + iy][ukonX - 2 + ix] = Instantiate (labyrintinPala, new Vector3 ((ukonX - 2 + ix) * 10, (ukonY - 2 + iy) * 10, 1), Quaternion.identity);
					List<int> sisaData = labyrintinData [ukonY - 2 + iy] [ukonX - 2 + ix];
					foreach(int dataArvo in sisaData){
						labyrintinPalaset [ukonY - 2 + iy] [ukonX - 2 + ix].transform.Find ("MuuriPalikka" + dataPaikka.ToString()).gameObject.SetActive (dataArvo == 1 ? true : false);
						labyrintinPalaset [ukonY - 2 + iy] [ukonX - 2 + ix].transform.Find ("PorttiPalikka" + dataPaikka.ToString()).gameObject.SetActive (dataArvo == 2 ? true : false);
						dataPaikka += 1;

					}
					if (itemienSpawnaus [ukonY - 2 + iy] [ukonX - 2 + ix]) {
						itemienSpawnaus [ukonY - 2 + iy] [ukonX - 2 + ix] = false;
						Instantiate (spawnattavatItemit[Random.Range(0, spawnattavatItemit.Length)], new Vector3((ukonX - 2 + ix) * 10 + Random.Range(3f, 7f), (ukonY - 2 + iy) * 10 + Random.Range(3f, 7f), 4), Quaternion.Euler(0, 0, Random.Range(0, 360f)));
					}
					dataPaikka = 0;
					int maaMerkki = maaMerkit [ukonY - 2 + iy] [ukonX - 2 + ix];
					if(maaMerkki != 0){
						GameObject itseMaa = labyrintinPalaset [ukonY - 2 + iy] [ukonX - 2 + ix].transform.Find ("Maa").gameObject;
						if (maaMerkki == 1) {
							itseMaa.GetComponent<SpriteRenderer> ().sprite = maaMerkkienSpritet [0];
							itseMaa.GetComponent<Renderer> ().material = maaMerkkienMateriaalit [0];
						} else if (maaMerkki == 2) {
							itseMaa.GetComponent<SpriteRenderer> ().sprite = maaMerkkienSpritet [1];
							itseMaa.GetComponent<Renderer> ().material = maaMerkkienMateriaalit [1];
						} 
						else {
							itseMaa.GetComponent<SpriteRenderer> ().sprite = maaMerkkienSpritet [2];
							itseMaa.GetComponent<Renderer> ().material = maaMerkkienMateriaalit [2];
						}
					}
				}
			}
		}
		for (int iy = 0; iy < 2; iy++) {
			for (int ix = 0; ix < 7; ix++) {
				if ((ukonY - 3 + iy * 6 >= 0 && ukonY - 3 + iy * 6 < labyrintinKoko && ukonX - 3 + ix >= 0 && ukonX - 3 + ix < labyrintinKoko) && labyrintinPalaset[ukonY - 3 + iy * 6][ukonX - 3 + ix].name != "tyhja"){
					GameObject tuhottava = labyrintinPalaset [ukonY - 3 + iy * 6] [ukonX - 3 + ix];
					labyrintinPalaset [ukonY - 3 + iy * 6] [ukonX - 3 + ix] = new GameObject ("tyhja");
					Destroy (tuhottava);
				}
			}
		}
		for (int iy = 0; iy < 7; iy++) {
			for (int ix = 0; ix < 2; ix++) {
				if ((ukonY - 3 + iy >= 0 && ukonY - 3 + iy < labyrintinKoko && ukonX - 3 + ix * 6 >= 0 && ukonX - 3 + ix * 6 < labyrintinKoko) && labyrintinPalaset[ukonY - 3 + iy][ukonX - 3 + ix * 6].name != "tyhja"){
					GameObject tuhottava = labyrintinPalaset [ukonY - 3 + iy] [ukonX - 3 + ix * 6];
					labyrintinPalaset [ukonY - 3 + iy] [ukonX - 3 + ix * 6] = new GameObject ("tyhja");
					Destroy (tuhottava);
				}
			}
		}
	}

	int LuoOikeaPala () {
		float randomiLukuTassa = Random.Range (0f, 1f);
		if (randomiLukuTassa > tnKaksiPalaa) {
			return 2;
		} else if (randomiLukuTassa > tnKolmePalaa) {
			return 3;
		} else if (randomiLukuTassa > tnNeljaPalaa) {
			return 4;
		} 
		else {
			return 1;
		}
	}	
	int LaskeAukot(List<int> lista){
		int aukkojenMaara = 0;
		foreach (int dataArvo in lista) {
			if (dataArvo == 0) {
				aukkojenMaara += 1;
			}
		}
		return aukkojenMaara;
	}
	int LaskePisteetYmparilla(List<List<int>> lista, int x, int y){
		int tarkistetutPisteetYmparilla = 0;
		for (int i = 0; i < 2; i++){
			if (x - 1 + i * 2 >= 0 && x - 1 + i * 2 < labyrintinKoko) {
				if (karttaTarkistettavistaPisteista [y] [x - 1 + i * 2] == 1 && ((i == 0 && labyrintinData[y][x - 1][2] == 0) || i == 1 && labyrintinData[y][x + 1][0] == 0)) {
					tarkistetutPisteetYmparilla += 1;
				}
			}
		}
		for (int i = 0; i < 2; i++){
			if (y - 1 + i * 2 >= 0 && y - 1 + i * 2 < labyrintinKoko){
				if (karttaTarkistettavistaPisteista[y - 1 + i * 2][x] == 2 && ((i == 0 && labyrintinData[y - 1][x][3] == 0) || i == 1 && labyrintinData[y + 1][x][1] == 0)){
					tarkistetutPisteetYmparilla += 1;
				}
			}
		}
		return tarkistetutPisteetYmparilla;
	}
	Vector3 LabyrintinPalanKeskiPaikka(int x, int y){
		return labyrintinPalaset [y] [x].transform.position + new Vector3 (5, 5, 0);
	}
}