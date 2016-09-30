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

	public PlayerStats PS {get;set;}

	public PlayerData(){
		PS = new PlayerStats();
	}
	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}
}
