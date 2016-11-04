using UnityEngine;
using System.Collections;

public class GoldAdd : MonoBehaviour {
	private GameObject goldie;
	public int quanty; 
	public GameObject damageBurst;
	public AudioSource AS;
	// Use this for initialization
	void Start () {
		goldie = GameObject.Find ("Player");
		AS = GameObject.Find ("GoldSound").GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.name == "Player" && other is PolygonCollider2D) {
			AS.Play ();
			Instantiate(damageBurst,transform.position, transform.rotation);
			var clone = (GameObject )Instantiate(Resources.Load("DamageNumbers"), transform.position, Quaternion.Euler(Vector3.zero));
			clone.GetComponent<FloatingNumbers>().damageNumber = "$" + quanty;
			Instantiate(damageBurst, transform.position, transform.rotation);
		goldie.gameObject.GetComponent<GoldManager> ().AddGold(quanty);
		Destroy(this.gameObject);
		}
	}
}
