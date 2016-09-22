using UnityEngine;
using System.Collections;

public class LoadNewArea : MonoBehaviour {
    public string LevelToLoad;
	public PlayerStats PS;
 
	// Use this for initialization
	void Start () {
		PS = FindObjectOfType<PlayerStats>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    IEnumerator OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
			
            ScreenFader sf = GameObject.FindGameObjectWithTag("Fader").GetComponent<ScreenFader>();

			GameObject.Find("Player").GetComponent<Animator>().enabled = false;
			GameObject.Find("Player").GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
            //RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
            yield return StartCoroutine(sf.FadeToBlack());
            Application.LoadLevel(LevelToLoad);
            yield return StartCoroutine(sf.FadeToClear());
			GameObject.Find("Player").GetComponent<Animator>().enabled = true;
			GameObject.Find("Player").GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
			GameObject.Find("Player").GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;

        }
    }

}
