using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class MainMenuManager : MonoBehaviour {
	private GameObject CNPJ;
	private GameObject LPJ;
	private bool boolCNPJ;
	private bool boolLPJ;

	private GameObject Menu;
	private GameObject OutFromLN;

	public Text PJName1;
	public Text PJName2;
	public Text PJName3;
	// Use this for initialization
	void Start () {
		OutFromLN = GameObject.Find ("Quit - L/N");

		Menu=	GameObject.Find ("Menu");

		CNPJ=	GameObject.Find ("Create New PJ");
		CNPJ.SetActive (false);
		boolCNPJ = false;

		LPJ = GameObject.Find ("Load PJ");
		LPJ.SetActive (false);
		boolLPJ = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void NewGame(int Level)
    {
        Application.LoadLevel(Level);
    }
    public void Quit()
    {
        Application.Quit();
    }
	public void SuperNewGame()
	{
		boolCNPJ = !boolCNPJ;
		Debug.Log (boolCNPJ);
		if (boolCNPJ) {
			CNPJ.SetActive (true);
			OutFromLN.SetActive (true);
			Menu.SetActive (false);
		} else {
			CNPJ.SetActive (false);
			OutFromLN.SetActive (false);
			Menu.SetActive (true);
		}

	}
	public void SuperLoadGame()
	{
		boolLPJ = !boolLPJ;
		if (boolLPJ) {
			LPJ.SetActive (true);
			OutFromLN.SetActive (true);
			Menu.SetActive (false);
		} else {
			LPJ.SetActive (false);
			OutFromLN.SetActive (false);
			Menu.SetActive (true);
		}

	}

	public void Out()
	{
		Menu.SetActive (true);
		LPJ.SetActive (false);
		boolLPJ = false;
		CNPJ.SetActive (false);
		boolCNPJ = false;
		OutFromLN.SetActive (false);
	}
	public void CreatePJ(int ButtonNumber)
	{
		if (File.Exists(Application.persistentDataPath + "/player" + ButtonNumber + ".dat")) {
		} else {
		Debug.Log ("Se guardaron datos");
		BinaryFormatter bf = new BinaryFormatter ();
			FileStream file = File.Create (Application.persistentDataPath + "/player" + ButtonNumber + ".dat");
			Debug.Log (Application.persistentDataPath + "/player" + ButtonNumber + ".dat");
		PlayerData _PlayerData = new PlayerData();
		
		_PlayerData.PS = new PlayerStats ();

		_PlayerData.ButtonNumberForLoad = ButtonNumber;
		
			switch (ButtonNumber) {
			case 1:
				_PlayerData.PlayerName = PJName1.text;
				break;
			case 2:
				_PlayerData.PlayerName = PJName2.text;
				break;
			case 3:
				_PlayerData.PlayerName = PJName3.text;
				break;
			}
		_PlayerData.PS.HPLegit = 1;
		_PlayerData.PS.ATQLegir = 1;
		_PlayerData.PS.STAMINALegit = 1;
		_PlayerData.PS.RESLegit = 1;
		_PlayerData.PS.LUCKLegit = 1;
		_PlayerData.PS.falselevel= 1;
		_PlayerData.PS.currentExp= 0;
		_PlayerData.Gold = 0;
		_PlayerData.PS.Points= 0;

		_PlayerData.PS.HPNoNull = 1;
		_PlayerData.PS.ATQNoNull = 1;
		_PlayerData.PS.STAMINANoNull = 1;
		_PlayerData.PS.RESNoNull = 1;
		_PlayerData.PS.LUCKNoNull = 1;

		bf.Serialize (file,_PlayerData);
		file.Close ();

		Application.LoadLevel("escena2");
		}
	}
	public void LoadPJ(int ButtonNumber)
	{
		if (File.Exists(Application.persistentDataPath + "/player" + ButtonNumber + ".dat")) {
			Application.LoadLevel("escena2");
			PlayerPrefs.SetInt ("partida", ButtonNumber);
		} else {
			Debug.Log ("No hay partida");
		}
	}
}
