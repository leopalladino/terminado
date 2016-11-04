using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PotionManager : MonoBehaviour {
	public int Potions;
	public int PotionsInUsage;
	public bool isPotion; 
	private int aux;
	public Health H;
	public GoldManager GM;
	public Text cant;
	public AudioSource AS;
	// Use this for initialization
	void Start () {
		AS = GameObject.Find ("PotSound").GetComponent<AudioSource>();
		H = FindObjectOfType<Health> (); 
		GM = FindObjectOfType<GoldManager> ();
		cant = GameObject.Find ("Potions(PotionsManager) - Text").GetComponent<Text> ();
	}
	
	// Update is called once per frame
	void Update () {
		cant.text = "Pociones: " + Potions;
		if (H.playerCurrentHealth == H.playerMaxHealth) {
			isPotion = false;
			aux = 0;
		}
		if (aux > 20) {
			isPotion = false;
			aux = 0;
		}
		if (isPotion) {
			H.playerCurrentHealth += 1;
			aux += 1;
		}
	}

	public void AddPotion(int cant)
	{
		Potions += cant;
		if (GM.gold >= 50 ) {
			GM.gold -= 50;
		} else {
			Debug.Log ("No tenes suficiente dinero");
		}

	}
	public void PotionEffect()
	{
		if (Potions > 0 && H.playerCurrentHealth != H.playerMaxHealth) {
			AS.Play ();
			isPotion = true;
			Potions--;
		}

	}
}
