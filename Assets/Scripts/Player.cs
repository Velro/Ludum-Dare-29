using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour 
{
    public float health = 9;
    public float maxHealth = 10;
    public float healthLossEveryXSeconds = 10f;
    public float invincibleTimeAfterHit = 1f;

    public float speed = 0.1f;
    public Vector3 moveTarget;

    private float lastHealth = 0;
    private float ltHealthLostToTime = 0;
    private float ltHealthLostToHit = 0;
    private float ltBloodDrop;
    public float bloodDropsEveryXSeconds = 0.2f;
    GameObject bloodParent;
    public GameObject groundBloodDrop;

    public float flashDuration;

    public GameObject light;

    Color originalColor;

    void Start ()
    {
        bloodParent = GameObject.Find("BloodParent");
        originalColor = gameObject.GetComponentInChildren<SpriteRenderer>().material.color;
        //defaultMat = gameObject.GetComponentInChildren<MeshRenderer>().material;
    }

	// Update is called once per frame
	void Update () 
    {
        //rigidbody2D.
        Vector3 input = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"),0) * speed;
        moveTarget = input;
        if (input != Vector3.zero)
        {
           rigidbody2D.velocity = input;
        }
        else
        {
            rigidbody2D.velocity = Vector3.zero;
        }

        if (health > maxHealth)
        {
            health = maxHealth;
        }

        if (Time.time < ltHealthLostToHit)
        {
            health = lastHealth;
            StartCoroutine(Flash( flashDuration));
        }

        if (Time.time > ltHealthLostToTime + ltHealthLostToTime)
        {
            health--;
            ltHealthLostToTime = Time.time;
        }

        if (health < lastHealth)
        {
            ltHealthLostToHit = Time.time;
            lastHealth = health;
        }

        if (Time.time > ltBloodDrop + bloodDropsEveryXSeconds)
        {
            GameObject g = Instantiate(groundBloodDrop, new Vector3(transform.position.x, transform.position.y, 0.1f), transform.rotation) as GameObject;
            g.transform.parent = bloodParent.transform;
            ltBloodDrop = Time.time;
        }

        Vector3 mouseScreen = Input.mousePosition;
        Vector3 mouse = Camera.main.ScreenToWorldPoint(mouseScreen);
        transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(mouse.y - transform.position.y, mouse.x - transform.position.x) * Mathf.Rad2Deg - 90);

        if (health <= 0)
        {
            Lose();
        }
	}

    void Lose()
    {

    }

    IEnumerator Flash( float duration)
    {
        gameObject.GetComponentInChildren<SpriteRenderer>().material.color = Color.red;
        yield return new WaitForSeconds(duration);
        gameObject.GetComponentInChildren<SpriteRenderer>().material.color = originalColor;
    }
}
