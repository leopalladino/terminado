using UnityEngine;
using System.Collections;

public class LoadStats : MonoBehaviour {
	private SaveGame SG;
	private bool had;
	// Use this for initialization
	void Start () {
	SG = FindObjectOfType <SaveGame>();
		had = true;
	}
	
	// Update is called once per frame
	void Update () {
		if (had) {
		//	SG.setStats ();
			had = false;
		}
	}
}
