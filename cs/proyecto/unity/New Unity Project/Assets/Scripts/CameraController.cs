using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

    public Transform target;
    public float m_speed;
    Camera mycam;
    private static bool cameraExists;

	// Use this for initialization
	void Start () {
		target = GameObject.Find ("Player").GetComponent<Transform> ();
        mycam = GetComponent<Camera>();

    }

    // Update is called once per frame
    void Update () {
        mycam.orthographicSize = (Screen.height / 50);

        if (target)
        {
            transform.position = Vector3.Lerp(transform.position, target.position, m_speed) + new Vector3(0,0,-10);
        }
	}
}
