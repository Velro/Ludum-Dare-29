using UnityEngine;
using System.Collections;

public class AssignManually : MonoBehaviour {

    public Material spriteDiffuse;

    void Awake()
    {
        renderer.material = spriteDiffuse;
    }
	// Use this for initialization
	void Start () {
        renderer.material = spriteDiffuse;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
