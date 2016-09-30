using UnityEngine;
using System.Collections;

public class MainMenuManager : MonoBehaviour {
	private GameObject CNPJ;
	private GameObject LPJ;
	private bool boolCNPJ;
	private bool boolLPJ;

	private GameObject Menu;
	private GameObject OutFromLN;
	// Use this for initialization
	void Start () {
		OutFromLN = GameObject.Find ("Quit - L/N");

		Menu=	GameObject.Find ("Menu");

		CNPJ=	GameObject.Find ("Create New PJ");
		CNPJ.SetActive (false);
		boolCNPJ = false;

		LPJ = GameObject.Find ("Load PJ");
		LPJ.SetActive (false);
		boolLPJ = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void NewGame(int Level)
    {
        Application.LoadLevel(Level);
    }
    public void Quit()
    {
        Application.Quit();
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
		Menu.SetActive (true);
		LPJ.SetActive (false);
		boolLPJ = false;
		CNPJ.SetActive (false);
		boolCNPJ = false;
		OutFromLN.SetActive (false);
	}
}
