using UnityEngine;
using System.Collections;

public class LoadNewArea : MonoBehaviour {
    public string LevelToLoad;
	public PlayerStats PS;
	private bool ButtonPressed;
	private  GameObject dPopUp;
	private GameObject Canvas;
	// Use this for initialization
	void Start () {
		Canvas = GameObject.Find ("Canvas");
		dPopUp = Resources.Load ("PopUp") as GameObject;
		dPopUp.transform.parent = Canvas.transform;
		dPopUp.SetActive (false);
		ButtonPressed = false;
		PS = FindObjectOfType<PlayerStats>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    IEnumerator OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
			if (ButtonPressed) {
				dPopUp.SetActive (true);
				dPopUp.transform.SetParent(Canvas.transform);
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

	void OnTriggerStay2D (Collider2D other)
	{
		if (other is BoxCollider2D && other.name == "Player") 
		{
			dPopUp.SetActive (true);
			if (Input.GetKeyDown(KeyCode.E)) {
				ButtonPressed = true;
			}

		}
	}
	void OnTriggerExit2D (Collider2D other)
	{
		
		if (other is BoxCollider2D && other.name == "Player") 
		{
			dPopUp.SetActive (false);
			if (Input.GetKeyDown(KeyCode.E)) {
				ButtonPressed = true;
			}

		}
	}
}
