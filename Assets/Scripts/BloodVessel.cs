using UnityEngine;
using System.Collections;

public class BloodVessel : MonoBehaviour {

    float waitSeconds =3f;
    bool shifted = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Time.time > waitSeconds && !shifted)
        {
            transform.position = new Vector3(transform.position.x - 1000, transform.position.y, 0);
            shifted = true;
        }
	}
}
