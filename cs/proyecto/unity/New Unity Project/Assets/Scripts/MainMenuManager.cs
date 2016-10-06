using UnityEngine;
using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour {
	private GameObject CNPJ;
	private GameObject LPJ;
	private bool boolCNPJ;
	private bool boolLPJ;

	private GameObject Menu;
	private GameObject OutFromLN;
	private GameObject CreateMenu;
	private GameObject Slots;

	public Text CreatePJName1;
	public Text CreatePJName2;
	public Text CreatePJName3;

	public Text ViewPJName1;
	public Text ViewPJName2;
	public Text ViewPJName3;

	public Text LoadPJName1;
	public Text LoadPJName2;
	public Text LoadPJName3;

	public Button CreateBtn1;
	public Button CreateBtn2;
	public Button CreateBtn3;

	public InputField CreateField1;
	public InputField CreateField2;
	public InputField CreateField3;

	public Toggle Toggle1;
	public Toggle Toggle2;
	public Toggle Toggle3;
	// Use this for initialization
	void Start () {
		Toggle1 = GameObject.Find ("Toggle1").GetComponent<Toggle>();
		Toggle2 = GameObject.Find ("Toggle2").GetComponent<Toggle>();
		Toggle3 = GameObject.Find ("Toggle3").GetComponent<Toggle>();

		CreateBtn1 = GameObject.Find ("CrearBTN1").GetComponent<Button>();
		CreateBtn2 = GameObject.Find ("CrearBTN2").GetComponent<Button>();
		CreateBtn3 = GameObject.Find ("CrearBTN3").GetComponent<Button>();

		CreateField1 = GameObject.Find ("NameIF1").GetComponent<InputField>();
		CreateField2 = GameObject.Find ("NameIF2").GetComponent<InputField>();
		CreateField3 = GameObject.Find ("NameIF3").GetComponent<InputField>();

		ViewPJName1 = GameObject.Find ("Placeholder1").GetComponent<Text>();
		ViewPJName2 = GameObject.Find ("Placeholder2").GetComponent<Text>();
		ViewPJName3 = GameObject.Find ("Placeholder3").GetComponent<Text>();

		Slots = GameObject.Find ("Slots");
		OutFromLN = GameObject.Find ("Quit - L/N");
		CreateMenu =	GameObject.Find ("Creation Menu");
		Menu=	GameObject.Find ("Menu");

		CNPJ=	GameObject.Find ("Create New PJ");
		CNPJ.SetActive (false);
		boolCNPJ = false;
		CreateMenu.SetActive (false);
		LPJ = GameObject.Find ("Load PJ");
		LPJ.SetActive (false);
		boolLPJ = false;

		if (File.Exists (Application.persistentDataPath + "/player" + 1 + ".dat") == false && File.Exists (Application.persistentDataPath + "/player" + 2 + ".dat") == false && File.Exists (Application.persistentDataPath + "/player" + 3 + ".dat") == false) {
			GameObject.Find ("Continue").GetComponent<Button> ().interactable = false;
		}

		if (File.Exists (Application.persistentDataPath + "/player" + 1 + ".dat")) {
			BinaryFormatter bf = new BinaryFormatter ();
			FileStream file = File.OpenRead (Application.persistentDataPath +  "/player" + 1 + ".dat");
			PlayerData _PlayerData = (PlayerData)bf.Deserialize (file);
			file.Close ();
			LoadPJName1.text = _PlayerData.PlayerName;
			CreatePJName1.text = _PlayerData.PlayerName;
			CreateBtn1.interactable = false;
			CreateBtn1.GetComponentInChildren<Text> ().text = "Ocupado";
			CreateField1.interactable = false;
			ViewPJName1.text = _PlayerData.PlayerName;
		}
		if (File.Exists (Application.persistentDataPath + "/player" + 2 + ".dat")) {
			BinaryFormatter bf = new BinaryFormatter ();
			FileStream file = File.OpenRead (Application.persistentDataPath +  "/player" + 2 + ".dat");
			PlayerData _PlayerData = (PlayerData)bf.Deserialize (file);
			file.Close ();
			LoadPJName2.text = _PlayerData.PlayerName;
			CreatePJName2.text = _PlayerData.PlayerName;
			CreateBtn2.interactable = false;
			CreateBtn2.GetComponentInChildren<Text> ().text = "Ocupado";
			CreateField2.interactable = false;
			ViewPJName2.text = _PlayerData.PlayerName;
		}

		if (File.Exists (Application.persistentDataPath + "/player" + 3 + ".dat")) {
			BinaryFormatter bf = new BinaryFormatter ();
			FileStream file = File.OpenRead (Application.persistentDataPath +  "/player" + 3 + ".dat");
			PlayerData _PlayerData = (PlayerData)bf.Deserialize (file);
			file.Close ();
			LoadPJName3.text = _PlayerData.PlayerName;
			CreatePJName3.text = _PlayerData.PlayerName;
			CreateBtn3.interactable = false;
			CreateBtn3.GetComponentInChildren<Text> ().text = "Ocupado";
			CreateField3.interactable = false;
			ViewPJName3.text = _PlayerData.PlayerName;
		}
	}
	void Update () {
		if (Toggle1.isOn) {
			Toggle2.isOn = false;
			Toggle3.isOn = false;
		}
		if (Toggle2.isOn) {
			Toggle1.isOn = false;
			Toggle3.isOn = false;
		}
		if (Toggle3.isOn) {
			Toggle1.isOn = false;
			Toggle2.isOn = false;
		}
	}

    public void NewGame(int Level)
    {
        Application.LoadLevel(Level);
    }
	public void DeleteGame(int game)
	{
		File.Delete (Application.persistentDataPath +  "/player" + game + ".dat");
		//Button Continue = GameObject.Find ("Continue").GetComponent<Button> ();
		//Continue.interactable = false;
		switch (game) {
		case 1:
			CreatePJName1.text = "";
			CreateBtn1.interactable = true;
			CreateBtn1.GetComponentInChildren<Text> ().text = "Crear";
			CreateField1.interactable = true;
			ViewPJName1.text = "Nombre";
			LoadPJName1.text = "Empty";
			break;
		case 2:
			CreatePJName2.text = "";
			CreateBtn2.interactable = true;
			CreateBtn2.GetComponentInChildren<Text> ().text = "Crear";
			CreateField2.interactable = true;
			ViewPJName2.text = "Nombre";
			LoadPJName2.text = "Empty";
			break;
		case 3:
			CreatePJName3.text = "";
			CreateBtn3.interactable = true;
			CreateBtn3.GetComponentInChildren<Text> ().text = "Crear";
			CreateField3.interactable = true;
			ViewPJName3.text = "Nombre";
			LoadPJName3.text = "Empty";
			break;
		}
	}
    public void Quit()
    {
        Application.Quit();
    }
	public void Continue()
	{
		Application.LoadLevel("escena2");
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
		Slots.SetActive (true);
		Menu.SetActive (true);
		LPJ.SetActive (false);
		boolLPJ = false;
		CNPJ.SetActive (false);
		boolCNPJ = false;
		OutFromLN.SetActive (false);
	}
	public void CreatePJ()
	{
		if (!Toggle1.isOn && !Toggle2.isOn && !Toggle3.isOn) {
			Debug.Log ("lol");	
		} else {
		if (File.Exists(Application.persistentDataPath + "/player" + PlayerPrefs.GetInt ("partida") + ".dat")) {
		} else {
		Debug.Log ("Se guardaron datos");
		BinaryFormatter bf = new BinaryFormatter ();
			FileStream file = File.Create (Application.persistentDataPath + "/player" + PlayerPrefs.GetInt ("partida") + ".dat");
			Debug.Log (Application.persistentDataPath + "/player" + PlayerPrefs.GetInt ("partida") + ".dat");
		PlayerData _PlayerData = new PlayerData();
		_PlayerData.ButtonNumberForLoad = PlayerPrefs.GetInt ("partida");
			switch (PlayerPrefs.GetInt ("partida")) {
			case 1:
				if (CreatePJName1.text != "") {
					_PlayerData.PlayerName = CreatePJName1.text;
				} else {
					Debug.Log ("Debes llenar el espacio Nombre.");
				}
				break;
			case 2:
				if (CreatePJName2.text != "") {
					_PlayerData.PlayerName = CreatePJName2.text;
				} else {
					Debug.Log ("Debes llenar el espacio Nombre.");
				}
				break;
			case 3:
				if (CreatePJName3.text != "") {
					_PlayerData.PlayerName = CreatePJName3.text;
				} else {
					Debug.Log ("Debes llenar el espacio Nombre.");
				}
				break;
			}
			if (Toggle1.isOn) {
				_PlayerData.Clase = 1;
			}
			if (Toggle2.isOn) {
				_PlayerData.Clase = 2;
			}
			if (Toggle3.isOn) {
				_PlayerData.Clase = 3;
			}

		_PlayerData.HPLegit = 1;
		_PlayerData.ATQLegir = 1;
		_PlayerData.STAMINALegit = 1;
		_PlayerData.RESLegit = 1;
		_PlayerData.LUCKLegit = 1;
		_PlayerData.falselevel= 1;
		_PlayerData.currentExp= 0;
		_PlayerData.Gold = 0;
		_PlayerData.Points= 0;

		_PlayerData.HPNoNull = 1;
		_PlayerData.ATQNoNull = 1;
		_PlayerData.STAMINANoNull = 1;
		_PlayerData.RESNoNull = 1;
		_PlayerData.LUCKNoNull = 1;

				switch (_PlayerData.Clase) {
				case 1:
					_PlayerData.CurrentWeapon = "Standar";
					_PlayerData.CurrentArmor = "isStandard";
					break;
				case 2:
					_PlayerData.CurrentWeapon = "FireAxe";
					_PlayerData.CurrentArmor = "isStandard";
					break;
				case 3:
					_PlayerData.CurrentWeapon = "StandardBow";
					_PlayerData.CurrentArmor = "isStandard";
					break;
				}
				Debug.Log (_PlayerData.Clase);
		bf.Serialize (file,_PlayerData);
		file.Close ();

		Application.LoadLevel("escena2");
			}
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
	public void SuperCreateNewGame(int ButtonNumber)
	{
		
		CreateMenu.SetActive (true);
		Slots.SetActive (false);
		PlayerPrefs.SetInt ("partida", ButtonNumber);	

	}
}
