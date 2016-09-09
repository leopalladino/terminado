using UnityEngine;
using System.Collections;

public class ShopManager : MonoBehaviour {
	
	public int Cost1;
	public int Cost2;

	public int LevelOfSword;
	public bool waterIsOn;
	public bool thunderIsON;
	public int LevelOfAxe;
	public int LevelOfBow;

	public bool [] skills;

	public Animator anim;

	public GameObject weapon;

	public Sprite StandarSword;
	public Sprite SwordAgua;
	public Sprite SwordAguaCargada;
	public GoldManager gold;
	public GameObject hover;
	private bool canChange = false;
	public string weaponInUsage;

	//public GameObject SwordRayo;

	// Use this for initialization
	void Start () {
		gold = FindObjectOfType<GoldManager> ();
		hover.SetActive(false);
		waterIsOn = false;
		thunderIsON = false;
	//	anim = GameObject.Find ("sword").GetComponent<Animator> ();
		weapon.GetComponent<SpriteRenderer> ().sprite = null;
	}
	
	// Update is called once per frame
	void Update () {
		if (LevelOfSword == 1) {
			anim.SetBool ("Standar", true);
			anim.SetBool ("Water", false);
			anim.SetBool ("Thunder", false);
			anim.SetBool ("FireSword", false);
			anim.SetBool ("ThunderAxe", false);
			anim.SetBool ("FireAxe", false);
			anim.SetBool ("StandardBow", false);
			anim.SetBool ("WaterAxe", false);
			anim.SetBool ("BloodAxe", false);


		}
		if (LevelOfSword == 2) {
			anim.SetBool ("Standar", false);
			anim.SetBool ("Water", true);
			anim.SetBool ("Thunder", false);
			anim.SetBool ("FireSword", false);
			anim.SetBool ("ThunderAxe", false);
			anim.SetBool ("FireAxe", false);
			anim.SetBool ("StandardBow", false);
			anim.SetBool ("WaterAxe", false);
			anim.SetBool ("BloodAxe", false);
			if (waterIsOn) {
				anim.SetBool ("IsWaterON", true);
			} else {
				anim.SetBool ("IsWaterON", false);
			}
		}
		if (LevelOfSword == 3) {
			anim.SetBool ("Standar", false);
			anim.SetBool ("Water", false);
			anim.SetBool ("Thunder", true);
			anim.SetBool ("FireSword", false);
			anim.SetBool ("ThunderAxe", false);
			anim.SetBool ("FireAxe", false);
			anim.SetBool ("StandardBow", false);
			anim.SetBool ("WaterAxe", false);
			anim.SetBool ("BloodAxe", false);

			if (thunderIsON) {
				anim.SetBool ("IsThunderON", true);
			} else {
				anim.SetBool ("IsThunderON", false);
			}
		}
		if (canChange) {
			canChange = false;
			anim.SetBool ("Standar", false);
			anim.SetBool ("Water", false);
			anim.SetBool ("Thunder", false);
			anim.SetBool ("FireSword", false);
			anim.SetBool ("ThunderAxe", false);
			anim.SetBool ("FireAxe", false);
			anim.SetBool ("StandardBow", false);
			anim.SetBool ("WaterAxe", false);
			anim.SetBool ("BloodAxe", false);
			anim.SetBool (weaponInUsage, true);
		}
	}

	public void ChangeWeapon1()
	{
		if (LevelOfSword == 3) {
			Debug.Log ("Ya tenes esta arma");
	
		}
		else {
			if (gold.gold >= 50 ) {
				LevelOfSword = 3;
				gold.gold -= 50;
			}
		}
	}
	public void ChangeWeapon2()
	{
		if (LevelOfSword == 2) {
			Debug.Log ("Ya tenes esta arma");

		}
		else {
			if (gold.gold >= 100 ) {
				LevelOfSword = 2;
				gold.gold -= 100;
			}
		}
	}

	public void catchNameOfWeapon(string nameofweapon)
	{
		if (gold.gold >= 100 ) {
			
			weaponInUsage = nameofweapon;
		canChange = true;
		
		LevelOfSword = 0;
			gold.gold -= 100;
		}
	}


}
