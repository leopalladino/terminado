using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GoldManager : MonoBehaviour {
	public int gold;
	public Text goldTXT;
	private SaveGame SG;
	// Use this for initialization
	void Start () {
		gold = 0;
		SG = FindObjectOfType<SaveGame>();
		goldTXT = GameObject.Find ("Gold(GoldManager) - Text").GetComponent<Text> ();
	}
	
	// Update is called once per frame
	void Update () {
		goldTXT.text = gold + " monedas"; 
	}
	public void AddGold(int quanty)
	{
		gold += quanty;
	}
	public void RestGold(int quanty)
	{
		gold -= quanty;
	}
}
