using UnityEngine;
using System.Collections;

public class Pincers : MonoBehaviour {

    public float chompDuration = 0.2f;
    float lastTimeChomped;
    public float damage;
    public GameObject pincerLeft;
    public GameObject pincerRight;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D (Collider2D other)
    {
        if (Time.time < lastTimeChomped + chompDuration)
        {
            other.gameObject.SendMessage("Damage", damage, SendMessageOptions.DontRequireReceiver);
            print("hit");
        }
    }

    public void Chomp ()
    {
        pincerLeft.GetComponent<Animation>().Play("PincerLeft");
        pincerRight.GetComponent<Animation>().Play("PincerRight");
        lastTimeChomped = Time.time;
    }
}
