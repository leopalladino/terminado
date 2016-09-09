using UnityEngine;
using System.Collections;

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
	}
	
	// Update is called once per frame
	void Update () {
		if (ConfirmSaveHT) {
			string auxSTR = getStats ();
			saveProgress (auxSTR);
			ConfirmSaveHT = false;
		}
	}

	public void saveStats(string stats)
	{
		strSaveStats = PlayerPrefs.GetString("Save1", stats);
		statsSTR = stats.Split (';');
	}
	public void saveProgress(string progress)
	{
		PlayerPrefs.SetString("Save1",progress); 
		Debug.Log (progress);
	}
	public string getStats()
	{	
		return PlayerPrefs.GetString("Save1"); 
	}
	public void setFirstTime()
	{
		PlayerPrefs.SetString("Save2", "yes"); 
	}
	public void setStats()
	{

		strSetStats = getStats ();
		saveStats (strSetStats);
		for (int i = 0; i < statsSTR.Length; i++) {
			switch (i) {
			case 0:
				PS.HPLegit = int.Parse (statsSTR [0]);
				PS.HPNoNull = PS.HPLegit;
				PS.currentHP = PS.HPLevels[PS.HPLegit];
				PS.theH.playerMaxHealth = PS.currentHP;
				if (PS.isFirstTime) {
					PS.theH.playerCurrentHealth = PS.theH.playerMaxHealth;
				}
				break;
			case 1:
				PS.ATQLegir = int.Parse(statsSTR [1]);
				PS.ATQNoNull = PS.ATQLegir;
				PS.currentAttack = PS.attacksLevels[PS.ATQLegir];
				break;
			case 2:
				PS.STAMINALegit = int.Parse (statsSTR [2]);
				PS.STAMINANoNull = PS.STAMINALegit;
				PS.theS.playerMaxStamina = PS.staminaLevels [PS.STAMINALegit];
				if (PS.isFirstTime) {
					PS.theS.playerCurrentStamina = PS.theS.playerMaxStamina; 
				}
				break;
			case 3:
				PS.RESLegit = int.Parse(statsSTR [3]);
				PS.RESNoNull= PS.RESLegit;
				PS.currentDefense = PS.defenseLevels[PS.STAMINALegit];
				break;
			case 4:
				PS.LUCKLegit = int.Parse(statsSTR [4]);
				PS.currentLuck = PS.luckLevels[PS.LUCKLegit];
				break;
			case 5:
				PS.currentLevel = int.Parse (statsSTR [5]);
				PS.falselevel = PS.currentLevel;
				break;
			case 6:
				PS.currentExp = int.Parse(statsSTR [6]);
				break;
			case 7:
				GM.gold = int.Parse (statsSTR [7]);
				break;
			case 8:
				PS.Points = int.Parse (statsSTR [8]);
				PS.FalsePoints = PS.Points;
				break;
			}
		}
	}
}
