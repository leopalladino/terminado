using UnityEngine;
using System.Collections;

public class EnemyHealth : MonoBehaviour {
    public int MaxHealth;
    public int CurrentHealth;
    public float waitToReload;
    private bool reloading;
	public GameObject canvas;
	public GameObject item;

	private PlayerStats thePS;
	private SaveGame theSG;
	public int xpToGive;

    // Use this for initialization
    void Start()
    {

        CurrentHealth = MaxHealth;
		thePS = FindObjectOfType <PlayerStats>();
		theSG= FindObjectOfType <SaveGame>();
    }


    // Update is called once per frame
    void Update()
    {
        if (CurrentHealth <= 0)
        {
			Instantiate(item, transform.position, transform.rotation);
			thePS.AddExperience (xpToGive);
			this.transform.parent.gameObject.SetActive(false);
			thePS.statsForSG = thePS.HPLegit + ";" + thePS.ATQLegir + ";" + thePS.STAMINALegit + ";" + thePS.RESLegit + ";" + thePS.LUCKLegit + ";" + thePS.falselevel + ";" + thePS.currentExp + ";" + thePS.GM.gold + ";" + thePS.Points;
			theSG.saveProgress (thePS.statsForSG);
			theSG.saveStats(thePS.statsForSG);


            /*waitToReload -= Time.deltaTime;
            if (waitToReload < 1)
            {
                Application.LoadLevel(Application.loadedLevel);
            }
            if (waitToReload < 0)
            {
                gameObject.SetActive(true);
            }
        */

        }
    }
    public void HurtEnemy(int damageToGive)
    {
        CurrentHealth -= damageToGive;
    }
    public void SetMaxHealth()
    {
        CurrentHealth = CurrentHealth;
    }
}
