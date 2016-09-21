using UnityEngine;
using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class SaveGame : MonoBehaviour {
	public bool ConfirmSavePS;
	public bool ConfirmSaveHT;

	public GoldManager GM;
	public PlayerStats PS;
	private string strSaveStats;
	private string strSetStats;
	public string[] statsSTR;

	// Use this for initialization
	void Start () {
	//	statsSTR = new int[4] {0, 0, 0, 0, 0};
		strSetStats = "";
		ConfirmSaveHT = false;
		ConfirmSavePS = false;
		PS = FindObjectOfType<PlayerStats>();
		GM = FindObjectOfType<GoldManager>();
		PlayerData _PlayerData = new PlayerData();
	}
	
	// Update is called once per frame
	void Update () {
		if (ConfirmSaveHT) {
			//string auxSTR = getStats ();
			//saveProgress (auxSTR);
			ConfirmSaveHT = false;
		}
	}

	public void saveStats(int HP, int ATQ, int STAMINA, int RES, int LUCK, int falseLevel, int Exp, int Gold, int Points)
	{
		Debug.Log ("Se guardaron datos");
		BinaryFormatter bf = new BinaryFormatter ();
		FileStream file = File.Open (Application.persistentDataPath + "/playerInfo.dat", FileMode.Open);
		PlayerData _PlayerData = new PlayerData();
		_PlayerData.HPLegit = HP;
		_PlayerData.ATQLegir = ATQ;
		_PlayerData.STAMINALegit = STAMINA;
		_PlayerData.RESLegit = RES;
		_PlayerData.LUCKLegit = LUCK;
		_PlayerData.falselevel= falseLevel;
		_PlayerData.currentExp= Exp;
//		PlayerData.GM.gold = Gold;
		_PlayerData.Points= Points;

		_PlayerData.HPNoNull = HP;
		_PlayerData.ATQNoNull = ATQ;
		_PlayerData.STAMINANoNull = STAMINA;
		_PlayerData.RESNoNull = RES;
		_PlayerData.LUCKNoNull = LUCK;

		bf.Serialize (file,_PlayerData);
		file.Close ();
	}
	public void loadStats(int HP, int ATQ, int STAMINA, int RES, int LUCK, int falseLevel, int Exp, int Gold, int Points)
	{
		Debug.Log ("Se cargaron datos");
		BinaryFormatter bf = new BinaryFormatter ();
		FileStream file = File.Open (Application.persistentDataPath + "/playerInfo.dat", FileMode.Open);
		PlayerData _PlayerData = new PlayerData();
		_PlayerData = (PlayerData)bf.Deserialize(file);
		file.Close ();
		PS.HPLegit = _PlayerData.HPLegit;
		PS.ATQLegir = _PlayerData.ATQLegir;
		PS.STAMINALegit = _PlayerData.STAMINALegit;
		PS.RESLegit = _PlayerData.RESLegit;
		PS.LUCKLegit = _PlayerData.LUCKLegit;
		PS.falselevel= _PlayerData.falselevel;
		PS.currentExp= _PlayerData.currentExp;
	//	PS.GM.gold = PlayerData.GM.gold;
		PS.Points= _PlayerData.Points;

		PS.HPNoNull = _PlayerData.HPNoNull;
		PS.ATQNoNull = _PlayerData.ATQNoNull;
		PS.STAMINANoNull = _PlayerData.STAMINANoNull;
		PS.RESNoNull = _PlayerData.RESNoNull;
		PS.LUCKNoNull = _PlayerData.LUCKNoNull;


		PS.currentHP = PS.HPLevels[PS.HPLegit];
		PS.theH.playerMaxHealth = PS.currentHP;
		PS.theS.playerMaxStamina = PS.staminaLevels [PS.STAMINALegit];
		PS.currentDefense = PS.defenseLevels[PS.STAMINALegit];
		PS.currentLuck = PS.luckLevels[PS.LUCKLegit];
		PS.falselevel = PS.currentLevel;
		PS.FalsePoints = PS.Points;

		if (PS.isFirstTime) {
			PS.theS.playerCurrentStamina = PS.theS.playerMaxStamina; 
			PS.theH.playerCurrentHealth = PS.theH.playerMaxHealth;
		}

	}

	[Serializable]
 public class PlayerData
	{
		public int HPLegit;
		public int ATQLegir;
		public int STAMINALegit;
		public int RESLegit;
		public int LUCKLegit;
		public int falselevel;
		public int currentExp;
		//	PS.GM.gold = PlayerData.GM.gold;
		public int Points;

		public int HPNoNull;
		public int ATQNoNull;
		public int STAMINANoNull;
		public int RESNoNull;
		public int LUCKNoNull;
	}
}
