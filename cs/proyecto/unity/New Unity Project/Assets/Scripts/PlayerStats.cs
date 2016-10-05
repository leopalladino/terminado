using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

[Serializable]
public class PlayerStats : MonoBehaviour {
	public int currentLevel;
	public int currentLevelFalse;
	public int currentExp;
	public int[] toLevelUp;

	public int[] HPLevels;
	public int[] attacksLevels;
	public int[] staminaLevels;
	public int[] defenseLevels;
	public int[] luckLevels;
	public float[] speedLevels;

	public bool hadwarp = false;

	public int HPNoNull;
	public int ATQNoNull;
	public int STAMINANoNull;
	public int RESNoNull;
	public int LUCKNoNull;
	public float SPEEDNoNull;

	public bool PSAuxliarBool;

	public int HPLegit;
	public int ATQLegir;
	public int STAMINALegit;
	public int RESLegit;
	public int LUCKLegit;
	public float SPEEDLegit;

	public int SkillPoints;
	public int currentHP;
	public int currentStamina;
	public int currentAttack;
	public int currentDefense;
	public int currentLuck;
	public float currentSpeed;

	private PlayerMovement PV;
	public Health theH;
	public Stamina theS;
	public int Points; 
	public int FalsePoints; 
	public GameObject LV;
	public GameObject ConfirmS;

	public Input Field;
	public bool HadConfirm;
	public SpawnPositionManager SPM;
	public GoldManager GM;
	private SaveGame SG;
	private bool isActive;
	public bool isFirstTime;

	public Text puntos;

	public int falselevel;
	public bool isReset;

	private int[] Count;
	private int[] FalseCount;
	public int limit;
	private bool aux = false;
	public bool SConfirm;
	public bool auxSetStats = false;
	public string statsForSG;
	public bool hastoexit = true;
	private GameObject statsButton;
	private string auxNombre;

	private Text PlayerNameText;

	private bool itis = false;
	// Use this for initialization
	void Start () {
		statsButton = GameObject.Find ("statsButton");
		SConfirm = false;
	
		limit = 8;

		isReset = false;
		falselevel = 1;
		currentLevel = 1;
		currentLevelFalse = 1;
		isFirstTime = true;
		PV = FindObjectOfType<PlayerMovement>();
		GM = FindObjectOfType<GoldManager>();
		HadConfirm = true;
		isActive = false;

		HPNoNull = 1;
		ATQNoNull = 1;
		STAMINANoNull = 1;
		RESNoNull = 1;
		LUCKNoNull = 1;
		SPEEDNoNull = 1;

		HPLegit = 1;
		ATQLegir = 1;
		STAMINALegit = 1;
		RESLegit = 1;
		LUCKLegit = 1;
		SPEEDLegit = 1;
	
		PSAuxliarBool = true;

		currentSpeed = 8;
		//LV.SetActive (false); ESTO SE TIEWNE QUE ACTIVAR CUANDO LEVEL UP
		//if (Application.loadedLevelName != "escena2") {
			currentStamina = staminaLevels [currentLevel];
			currentHP = HPLevels [currentLevel];
			currentAttack = attacksLevels [currentLevel];
			currentDefense = defenseLevels [currentLevel];
		//}
		/*Points = 0; //tENGO QUE SACAR ESTO
		FalsePoints = 0;*/
		theH = FindObjectOfType <Health>();
		theS = FindObjectOfType <Stamina>();
		SPM = FindObjectOfType <SpawnPositionManager>();
		SG = FindObjectOfType <SaveGame>();
	
		Count = new int[6];
		for (int i = 0; i < Count.Length; i++) {
			Count [i] = 0;
		}

		FalseCount = new int[6];
		for (int i = 0; i < FalseCount.Length; i++) {
			FalseCount [i] = 0;
		}
			




			loadStats ();
		PlayerNameText = GameObject.Find ("PlayerName").GetComponent<Text>();
		LV.SetActive (false);

	}
	
	// Update is called once per frame
	void Update () {
		/*
		 if (hadwarp) {
			statsButton = GameObject.Find ("statsButton");
			LV = GameObject.FindGameObjectWithTag ("LevelUp");	
			PV = FindObjectOfType<PlayerMovement>();
			GM = FindObjectOfType<GoldManager>();
			SPM = FindObjectOfType<SpawnPositionManager>();
			theH = FindObjectOfType <Health>();
			theS = FindObjectOfType <Stamina>();
			SPM = FindObjectOfType <SpawnPositionManager>();
			SG = FindObjectOfType <SaveGame>();
			loadStats ();
			hadwarp = false;
		}
		*/

		if (Application.loadedLevelName != "escena2" && LV.activeSelf) {
			puntos.text = "Puntos restantes: " + Points;
			PlayerNameText.text = auxNombre;
		}
	
		if (Application.loadedLevelName != "escena2") {
			if (currentExp >= toLevelUp[falselevel]) 
			{
				if (statsButton.GetComponent<Image>().color == Color.white) {
					statsButton.GetComponent<Image>().color = Color.red;
				}
				falselevel++;
				Points++;
				FalsePoints = Points;

				saveStats();

			}
			if (SPM.win && !isActive && HadConfirm && PSAuxliarBool && !SPM.wantcontinue && hastoexit) {
				LevelUp ();
				currentLevel = falselevel;
				PSAuxliarBool = false;
				HadConfirm = false;
				isActive = true;
			}
		}
		if (Input.GetKeyDown(KeyCode.P)) {
			OnOffLevelUpGO ();
		}

		if (Application.loadedLevelName != "escena2") {
			if (LV.activeSelf) {
				aux = true;

				SPM.continuar.SetActive (false);
				Time.timeScale = 0;
			} else {

				//ESTO INTERFIERE CON LA VERDADERA PAUSA
				if (HadConfirm && aux) {
					Time.timeScale = 1;
					if (SPM.win) {
						SPM.continuar.SetActive (true);
					}
					aux= false;
				}

			}
		}
	}

	public void AddExperience(int experienceToAdd)
	{

		currentExp += experienceToAdd;
    }
	public void LevelUp()
	{
		currentLevelFalse++;

	}
	public void Confirm()
	{
		//tiene que avisar que tiene puntos por gastar
		if (FalsePoints > 0) { //esto va en otro lado
			
		
		if (SConfirm) {
			
		
		if (!HadConfirm) {
			HadConfirm = true;
			isActive = false;
			/*
			HPLegit = currentHP;
			ATQLegir = currentAttack;
			STAMINALegit = currentStamina;
			RESLegit = currentDefense;
			LUCKLegit = currentLuck;
			*/
			HPLegit= HPNoNull;
			ATQLegir= ATQNoNull;
			STAMINALegit=STAMINANoNull;
			RESLegit= RESNoNull;
			LUCKLegit=LUCKNoNull;
			SPEEDLegit= SPEEDNoNull;
			//SPEEDLegit = currentSpeed;
			isActive = false;
			FalsePoints = Points; //ESTO CAUSA PROBLEMAS SI O SI
	
			for (int i = 0; i < FalseCount.Length; i++) {
				FalseCount[i] = Count[i];
			}
			SConfirm = false;
			ConfirmS.SetActive(false);
			LV.SetActive (false);
			SG.ConfirmSavePS = true;
					saveStats();
			
		}
		}
		}
	}
	public void Reset()
	{
		if (!HadConfirm) {
			
			Points = FalsePoints;

			currentHP = HPLevels[HPLegit];
			currentAttack = attacksLevels[ATQLegir];
			currentStamina = staminaLevels[STAMINALegit];
			currentDefense= defenseLevels[RESLegit];
			currentLuck=luckLevels[LUCKLegit];

			HPNoNull = HPLegit;
			ATQNoNull = ATQLegir;
			STAMINANoNull = STAMINALegit;
			RESNoNull = RESLegit;
			LUCKNoNull = LUCKLegit;
			SPEEDNoNull = SPEEDLegit;
			//currentSpeed= SPEEDLegit;
			isFirstTime = true;
			for (int i = 0; i < Count.Length; i++) {
				Count[i] = FalseCount[i];
			}
			HP (HPLegit);
			STAMINA (STAMINALegit);
			RES (RESLegit);
			ATQ (ATQLegir);
//			SPEED (SPEEDLegit, null);
			LUCK (LUCKLegit);
			isFirstTime = false;
		}
	}
	public void OnOffLevelUpGO(){

		if (!LV.activeSelf) {
			statsButton.GetComponent<Image> ().color = Color.white;
			currentLevel = falselevel;
			LV.SetActive (true);
			if (itis) {
				itis = false;
				HP (HPLegit);
				HPNoNull++;
				HPLegit++;
				STAMINA (STAMINALegit);
				STAMINANoNull++;
				STAMINALegit++;
				RES (RESLegit);
				RESNoNull++;
				RESLegit++;
				ATQ (ATQLegir);
				ATQNoNull++;
				ATQLegir++;
				//			SPEED (0, null);
				LUCK (LUCKLegit);
				LUCKNoNull++;
				LUCKLegit++;
				isFirstTime = false;
				saveStats ();
			}
			PSAuxliarBool = false;
			HadConfirm = false;
			isActive = true;

			Points = FalsePoints;
			currentHP = HPLevels[HPLegit];
			currentAttack = attacksLevels[ATQLegir];
			currentStamina = staminaLevels[STAMINALegit];
			currentDefense= defenseLevels[RESLegit];
			currentLuck=luckLevels[LUCKLegit];

			HPNoNull = HPLegit;
			ATQNoNull = ATQLegir;
			STAMINANoNull = STAMINALegit;
			RESNoNull = RESLegit;
			LUCKNoNull = LUCKLegit;
			SPEEDNoNull = SPEEDLegit;
			//currentSpeed= SPEEDLegit;

			for (int i = 0; i < Count.Length; i++) {
				Count[i] = FalseCount[i];
			}
			isFirstTime = true;
			HP (HPLegit);
			STAMINA (STAMINALegit);
			RES (RESLegit);
			//ATQ (ATQLegir);
			currentAttack = attacksLevels[ATQLegir];
			GameObject.Find ("ATQText").GetComponent<Text>().text = "" + currentAttack;
			//			SPEED (SPEEDLegit, null);
			LUCK (LUCKLegit);
			isFirstTime = false;
		} else {
			Exit ();
		}
	}
	public void Exit()
	{
		if (!HadConfirm) {
			//(SPM.win && !isActive && HadConfirm && PSAuxliarBool && !SPM.wantcontinue) 

			PSAuxliarBool = true;
			HadConfirm = true;
			isActive = false;
			Points = FalsePoints;
			HPNoNull = HPLegit;
			ATQNoNull = ATQLegir;
			STAMINANoNull=STAMINALegit;
			RESNoNull=RESLegit ;
			LUCKNoNull=LUCKLegit;
			SPEEDNoNull= SPEEDLegit;
			/*
			isFirstTime = true;
			if (!hadwarp) {
				hastoexit = false;
			}
			for (int i = 0; i < Count.Length; i++) {
				Count[i] = FalseCount[i];
			}
			HP (HPLegit);
			STAMINA (STAMINALegit);
			RES (RESLegit);
			ATQ (ATQLegir);
			//			SPEED (SPEEDLegit, null);
			LUCK (LUCKLegit);
			*/
			isFirstTime = false;
			//SPEEDLegit = currentSpeed;
			isActive = false;
			FalsePoints = Points; //ESTO CAUSA PROBLEMAS SI O SI

			for (int i = 0; i < FalseCount.Length; i++) {
				FalseCount[i] = Count[i];
			}
			SConfirm = false;
			ConfirmS.SetActive(false);
			LV.SetActive (false);
		}
	}
	public void HP(int points)
	{
		Text text;
		if (Count[1] != limit) {
		if (!isFirstTime) {
			if (Points >  0) {
		text = GameObject.Find ("HPText").GetComponent<Text>();
		currentHP = HPLevels[points];
		theH.playerMaxHealth = currentHP;
		Points--;
		text.text = "" +  currentHP;
		HPNoNull++;
		Count [1]++;
							}
		} else {
			currentHP = HPLevels[points];
			theH.playerMaxHealth = currentHP;
			GameObject.Find ("HPText").GetComponent<Text>().text = "" +  currentHP;
			
		}
	}
	}
	public void ATQ(int points)
	{
		Text text;
		if (Count[4] != limit) {
			if (!isFirstTime) {
				if (Points >  0) {
					text = GameObject.Find ("ATQText").GetComponent<Text>();
					currentAttack = attacksLevels[points];
					Points--;
					text.text = "" + currentAttack;
					ATQNoNull++;
					Count [4]++;
				}
			} else {
				currentAttack = attacksLevels[points];
				GameObject.Find ("ATQText").GetComponent<Text>().text = "" + currentAttack;

			}
		}

	}
	public void STAMINA(int points)
	{
		if (Count[2] != limit) {
		if (!isFirstTime) {
			if (Points >  0) {
		theS.playerMaxStamina = staminaLevels[points];
		Points--;
					GameObject.Find ("STAText").GetComponent<Text>().text ="" +  theS.playerMaxStamina;
		STAMINANoNull++;
		Count [2]++;
			}	
		} else {
			theS.playerMaxStamina = staminaLevels[points];
			GameObject.Find ("STAText").GetComponent<Text>().text ="" +  theS.playerMaxStamina;
		
		}
	}
		}

	public void RES(int points)
	{
		Text text;
		if (Count[3] != limit) {
		if (!isFirstTime) {
			if (Points >  0) {
		currentDefense = defenseLevels[points];
		Points--;
		GameObject.Find ("RESText").GetComponent<Text>().text = "" + currentDefense;
		RESNoNull++;
		Count [3]++;
			} 
		} else {
			currentDefense = defenseLevels[points];
			GameObject.Find ("RESText").GetComponent<Text>().text = "" + currentDefense;
		
		}
	}
	}



	public void SPEED(int points, Text text)
	{
		//currentSpeed = speedLevels[points];
		Points--;
		//PV.moveSpeed = currentSpeed;
		//text.text = "" + currentSpeed;
	}

	public void LUCK(int points)
	{
		Text text;
				if (Count[5] != limit) {
		if (!isFirstTime) {
			if (Points > 0) {
		text = GameObject.Find ("LUCKText").GetComponent<Text>();
		currentLuck = luckLevels[points];
		Points--;
		text.text = "" + currentLuck;
		LUCKNoNull++;
		Count [5]++;
			} 
		} else {
			currentLuck = luckLevels[points];
				GameObject.Find ("LUCKText").GetComponent<Text>().text = "" + currentLuck;

		}
		}
	}

	public void INCREASE(string parent)
	{
		switch (parent) {
		case "HP":
			HP (HPNoNull + 1);
			break;
		case "STAMINA":
			STAMINA (STAMINANoNull + 1);
			break;
		case "RES":
			RES (RESNoNull + 1);
			break;
		case "ATQ":
			ATQ (ATQNoNull + 1);
			break;
		case "SPEED":
			//SPEED (SPEEDNoNull + 1, null);
			break;
		case "LUCK":
			LUCK (LUCKNoNull + 1);
			break;

		}
	}

	public void SuperConfirm()
	{
		SConfirm = true;
		ConfirmS.SetActive(true);
	}
	public void NoSuperConfirm()
	{
		ConfirmS.SetActive(false);
	}

	/*
	 public PlayerStats BringStats( PlayerData)
	{
		_PlayerData = this;
		return _PlayerData;
	}
	*/
	public void saveStats()
	{
		Debug.Log ("Se guardaron datos");
		BinaryFormatter bf = new BinaryFormatter ();

		PlayerData _PlayerData = new PlayerData();

		_PlayerData.HPLegit = HPLegit;
		_PlayerData.ATQLegir = ATQLegir;
		_PlayerData.STAMINALegit = STAMINALegit;
		_PlayerData.RESLegit = RESLegit;
		_PlayerData.LUCKLegit = LUCKLegit;
		_PlayerData.falselevel= falselevel;
		_PlayerData.currentExp= currentExp;

		_PlayerData.Points= Points;

		_PlayerData.HPNoNull = HPNoNull;
		_PlayerData.ATQNoNull = ATQNoNull;
		_PlayerData.STAMINANoNull = STAMINANoNull;
		_PlayerData.RESNoNull = RESNoNull;
		_PlayerData.LUCKNoNull = LUCKNoNull;
		_PlayerData.Gold = GM.gold;

		string path = "/player" + Convert.ToString (PlayerPrefs.GetInt ("partida")) + ".dat";
		FileStream file = File.OpenWrite(Application.persistentDataPath + path);

		try {
			bf.Serialize (file,_PlayerData);
		}
		finally {
			file.Close ();
		}
	}
	public void loadStats()
	{
		string path = "/player" + Convert.ToString (PlayerPrefs.GetInt ("partida")) + ".dat";

		if (File.Exists(Application.persistentDataPath + path)) {
					Debug.Log ("Se cargaron datos");
		BinaryFormatter bf = new BinaryFormatter ();
		FileStream file = File.OpenRead (Application.persistentDataPath + path);
		PlayerData _PlayerData = (PlayerData)bf.Deserialize (file) ;
			Debug.Log (_PlayerData.PlayerName);
		file.Close ();
		HPLegit = _PlayerData.HPLegit;
		ATQLegir = _PlayerData.ATQLegir;
		STAMINALegit = _PlayerData.STAMINALegit;
		RESLegit = _PlayerData.RESLegit;
		LUCKLegit = _PlayerData.LUCKLegit;
		falselevel= _PlayerData.falselevel;
		currentExp= _PlayerData.currentExp;
		GM.gold = _PlayerData.Gold;
			auxNombre = _PlayerData.PlayerName;
		Points= _PlayerData.Points;

		HPNoNull = HPLegit;
		ATQNoNull = ATQLegir;
		STAMINANoNull = STAMINALegit;
		RESNoNull = RESLegit;
		LUCKNoNull = LUCKLegit;

		HPNoNull = _PlayerData.HPNoNull;
		ATQNoNull = _PlayerData.ATQNoNull;
		STAMINANoNull = _PlayerData.STAMINANoNull;
		RESNoNull = _PlayerData.RESNoNull;
		LUCKNoNull = _PlayerData.LUCKNoNull;


		currentHP = HPLevels[HPLegit];
		currentAttack = attacksLevels[ATQLegir];
		theH.playerMaxHealth = currentHP;
		theS.playerMaxStamina = staminaLevels [STAMINALegit];
		currentDefense = defenseLevels[STAMINALegit];
		currentLuck = luckLevels[LUCKLegit];
		currentLevel = falselevel;
		FalsePoints = Points;

		if (isFirstTime) {
			theS.playerCurrentStamina = theS.playerMaxStamina; 
			theH.playerCurrentHealth = theH.playerMaxHealth;
		}
	}
	}
}
