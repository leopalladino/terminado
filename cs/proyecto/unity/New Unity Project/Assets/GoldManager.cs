using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GoldManager : MonoBehaviour {
	public int gold;
	public Text goldTXT;
	private SaveGame SG;
	private PlayerStats PS;
	// Use this for initialization
	void Start () {
		gold = 0;
		SG = FindObjectOfType<SaveGame>();
		PS = FindObjectOfType<PlayerStats>();
	}
	
	// Update is called once per frame
	void Update () {
		goldTXT.text = gold + " monedas"; 
	}
	public void AddGold(int quanty)
	{
		gold += quanty;
		PS.statsForSG = PS.HPLegit + ";" + PS.ATQLegir + ";" + PS.STAMINALegit + ";" + PS.RESLegit + ";" + PS.LUCKLegit + ";" + PS.falselevel + ";" + PS.currentExp + ";" + gold + ";" + PS.Points;
		SG.saveProgress (PS.statsForSG);
		SG.saveStats(PS.statsForSG);
		SG.setStats ();
	}
	public void RestGold(int quanty)
	{
		gold -= quanty;
	}
}
