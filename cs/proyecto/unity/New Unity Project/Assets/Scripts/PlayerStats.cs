using UnityEngine;
using UnityEngine.UI;
using System.Collections;

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

	public Rigidbody2D TheRB2D;
	private PlayerMovement PV;
	public Health theH;
	public Stamina theS;
	public int Points; 
	public int FalsePoints; 
	public GameObject LV;
	public GameObject ConfirmS;

	public Input Field;
	public bool HadConfirm;
	private SpawnPositionManager SPM;
	public GoldManager GM;
	private SaveGame SG;
	private bool isActive;
	public bool isFirstTime;

	public Text puntos;
	private bool LevelEarn;

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
	// Use this for initialization
	void Start () {
		SConfirm = false;

		limit = 8;
		statsButton = GameObject.Find ("statsButton");
		isReset = false;
		falselevel = 1;
		LevelEarn = false;
		currentLevel = 1;
		currentLevelFalse = 1;
		isFirstTime = true;
		PV = FindObjectOfType<PlayerMovement>();
		GM = FindObjectOfType<GoldManager>();
		HadConfirm = true;
		isActive = false;

		HPNoNull = 0;
		ATQNoNull = 0;
		STAMINANoNull = 0;
		RESNoNull = 0;
		LUCKNoNull = 0;
		SPEEDNoNull = 0;

		HPLegit = 0;
		ATQLegir = 0;
		STAMINALegit = 0;
		RESLegit = 0;
		LUCKLegit = 0;
		SPEEDLegit = 0;
	
		PSAuxliarBool = true;

		currentSpeed = 8;
		//LV.SetActive (false); ESTO SE TIEWNE QUE ACTIVAR CUANDO LEVEL UP
		if (Application.loadedLevelName != "escena2") {
			currentStamina = staminaLevels [currentLevel];
			currentHP = HPLevels [currentLevel];
			currentAttack = attacksLevels [currentLevel];
			currentDefense = defenseLevels [currentLevel];
		}
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
		statsForSG = SG.getStats ();
		if (statsForSG != "") {
			SG.saveProgress (statsForSG);
			//SG.setStats ();
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (Application.loadedLevelName != "escena2") {
			puntos.text = "Puntos restantes: " + Points;
		
		}
	
		if (Application.loadedLevelName != "escena2") {
			if (currentExp >= toLevelUp[falselevel]) 
			{
				falselevel++;
				Points++;
				FalsePoints = Points;

				statsForSG = HPLegit + ";" + ATQLegir + ";" + STAMINALegit + ";" + RESLegit + ";" + LUCKLegit + ";" + falselevel + ";" + currentExp + ";" + GM.gold + ";" + Points;
				SG.saveProgress (statsForSG);
				SG.saveStats(statsForSG);
			}

			if (SPM.win && !isActive && HadConfirm && PSAuxliarBool && !SPM.wantcontinue && hastoexit) {
				if (statsButton.GetComponent<Image>().color == Color.white) {
					statsButton.GetComponent<Image>().color = Color.red;
				}
				LevelUp ();
				currentLevel = falselevel;
				PSAuxliarBool = false;
				HadConfirm = false;
				isActive = true;
				if (isFirstTime) {
					HP (HPLegit, null);
					HPNoNull++;
					HPLegit++;
					STAMINA (STAMINALegit, null);
					STAMINANoNull++;
					STAMINALegit++;
					RES (RESLegit, null);
					RESNoNull++;
					RESLegit++;
					ATQ (ATQLegir, null);
					ATQNoNull++;
					ATQLegir++;
					//			SPEED (0, null);
					LUCK (LUCKLegit, null);
					LUCKNoNull++;
					LUCKLegit++;
					isFirstTime = false;
					SG.setStats ();
				}
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
		Debug.Log (currentExp);
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
					statsForSG = HPLegit + ";" + ATQLegir + ";" + STAMINALegit + ";" + RESLegit + ";" + LUCKLegit + ";" + falselevel + ";" + currentExp + ";" + GM.gold + ";" + Points;
			SG.saveProgress (statsForSG);
			SG.saveStats(statsForSG);
			SG.setStats ();
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
			HP (HPLegit, null);
			STAMINA (STAMINALegit, null);
			RES (RESLegit, null);
			ATQ (ATQLegir, null);
//			SPEED (SPEEDLegit, null);
			LUCK (LUCKLegit, null);
			isFirstTime = false;



		}
	}
	public void OnOffLevelUpGO(){
		if (!LV.activeSelf) {
			statsButton.GetComponent<Image> ().color = Color.white;
			currentLevel = falselevel;
			LV.SetActive (true);
			PSAuxliarBool = false;
			HadConfirm = false;
			isActive = true;
			if (isFirstTime) {
				HP (HPLegit, null);
				HPNoNull++;
				HPLegit++;
				STAMINA (STAMINALegit, null);
				STAMINANoNull++;
				STAMINALegit++;
				RES (RESLegit, null);
				RESNoNull++;
				RESLegit++;
				ATQ (ATQLegir, null);
				ATQNoNull++;
				ATQLegir++;
				//			SPEED (0, null);
				LUCK (LUCKLegit, null);
				LUCKNoNull++;
				LUCKLegit++;
				isFirstTime = false;
				SG.setStats ();
			}
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
			isFirstTime = true;
			hastoexit = false;
			for (int i = 0; i < Count.Length; i++) {
				Count[i] = FalseCount[i];
			}
			HP (HPLegit, null);
			STAMINA (STAMINALegit, null);
			RES (RESLegit, null);
			ATQ (ATQLegir, null);
			//			SPEED (SPEEDLegit, null);
			LUCK (LUCKLegit, null);
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
	public void HP(int points, Text text)
	{
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
			text = GameObject.Find ("HPText").GetComponent<Text>();
			currentHP = HPLevels[points];
			theH.playerMaxHealth = currentHP;
			text.text = "" +  currentHP;
			//HPNoNull++;
		}
	/*	if (isReset) {
			text = GameObject.Find ("HPText").GetComponent<Text>();
			currentHP = HPLevels[points];
			theH.playerMaxHealth = currentHP;
			text.text = "" +  currentHP;

		}*/
	}
	}
	public void STAMINA(int points, Text text)
	{
		if (Count[2] != limit) {
		if (!isFirstTime) {
			if (Points >  0) {
		text = GameObject.Find ("STAText").GetComponent<Text>();
		theS.playerMaxStamina = staminaLevels[points];
		Points--;
		text.text ="" +  theS.playerMaxStamina;
		STAMINANoNull++;
		Count [2]++;
			}	
		} else {
			text = GameObject.Find ("STAText").GetComponent<Text>();
			theS.playerMaxStamina = staminaLevels[points];
			text.text ="" +  theS.playerMaxStamina;
			//STAMINANoNull++;
		}
	}
		}

	public void RES(int points, Text text)
	{
		if (Count[3] != limit) {
		if (!isFirstTime) {
			if (Points >  0) {
		text = GameObject.Find ("RESText").GetComponent<Text>();
		currentDefense = defenseLevels[points];
		Points--;
		text.text = "" + currentDefense;
		RESNoNull++;
		Count [3]++;
			} 
		} else {
			text = GameObject.Find ("RESText").GetComponent<Text>();
			currentDefense = defenseLevels[points];
			text.text = "" + currentDefense;
		//	RESNoNull++;
		}
	}
	}

	public void ATQ(int points, Text text)
	{
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
			text = GameObject.Find ("ATQText").GetComponent<Text>();
			currentAttack = attacksLevels[points];
			text.text = "" + currentAttack;
		//	ATQNoNull++;
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

	public void LUCK(int points, Text text)
	{
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
			text = GameObject.Find ("LUCKText").GetComponent<Text>();
			currentLuck = luckLevels[points];
			text.text = "" + currentLuck;
		//	LUCKNoNull++;
		}
		}
	}

	public void INCREASE(string parent)
	{
		switch (parent) {
		case "HP":
			HP (HPNoNull + 1, null);
			break;
		case "STAMINA":
			STAMINA (STAMINANoNull + 1, null);
			break;
		case "RES":
			RES (RESNoNull + 1, null);
			break;
		case "ATQ":
			ATQ (ATQNoNull + 1, null);
			break;
		case "SPEED":
			//SPEED (SPEEDNoNull + 1, null);
			break;
		case "LUCK":
			LUCK (LUCKNoNull + 1, null);
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
}
