using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

[Serializable]
public class PlayerData {

	public string PlayerName {get;set;}

	public int ButtonNumberForLoad {get;set;}

	public int Gold {get;set;}

	public int Clase {get;set;}

	public PlayerStats PS {get;set;}

	public int HPLegit{get;set;}
	public int ATQLegir{get;set;}
	public int STAMINALegit{get;set;}
	public int RESLegit{get;set;}
	public int LUCKLegit{get;set;}
	public int falselevel{get;set;}
	public int currentExp{get;set;}

	public int Points{get;set;}

	public int HPNoNull{get;set;}
	public int ATQNoNull{get;set;}
	public int STAMINANoNull{get;set;}
	public int RESNoNull{get;set;}
	public int LUCKNoNull{get;set;}

	public PlayerData(){
		//PS = new PlayerStats();
	}
	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}
}
