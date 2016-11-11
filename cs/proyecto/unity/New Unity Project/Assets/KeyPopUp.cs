using UnityEngine;
using System.Collections;

public class KeyPopUp : MonoBehaviour {
	public bool isinzone;
	private UIManager UIMan;
	private DestroyElement destroy;
	private SpawnPositionManager SPW;
	private  GameObject dPopUp;
	private bool HadPopUp;
	private PlayerStats PS;
	public string leveltoload;
	// Use this for initialization
	void Start () {
		HadPopUp = false;
		PS = FindObjectOfType<PlayerStats>();
		UIMan = FindObjectOfType<UIManager>();
		SPW = FindObjectOfType<SpawnPositionManager>();
	}

	// Update is called once per frame
	void Update () {
		if (isinzone) {

			if (HadPopUp == false) {

				UIMan.PopUp(Resources.Load ("PopUp") as GameObject);
				HadPopUp = true;
			}

			if (Input.GetKeyUp(KeyCode.E))
			{
				DestroyElement[] hinges = FindObjectsOfType(typeof(DestroyElement)) as DestroyElement[];
				foreach (DestroyElement hinge in hinges) {
					hinge.Destroy ();
				}
				HadPopUp = true;
				Application.LoadLevel (leveltoload);
			}
		}
	}

	void OnTriggerStay2D (Collider2D other)
	{
		if (other.name == "Player") 
		{
			isinzone = true;
			Debug.Log ("sad");
		}
	}
	void OnTriggerExit2D (Collider2D other)
	{
		if (other.name == "Player") 
		{
			DestroyElement[] hinges = FindObjectsOfType(typeof(DestroyElement)) as DestroyElement[];
			foreach (DestroyElement hinge in hinges) {
				hinge.Destroy ();
			}
			isinzone = false;
		}
	}
}
