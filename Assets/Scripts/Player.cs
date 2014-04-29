using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour 
{
    public float health = 9;
    private float maxHealth = 11;
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
    public GameObject legs;
    public GameObject body;
    public GameObject pincers;

    public Sprite health11;
    public Sprite health10;
    public Sprite health9;
    public Sprite health8;
    public Sprite health7;
    public Sprite health6;
    public Sprite health5;
    public Sprite health4;
    public Sprite health3;
    public Sprite health2;
    public Sprite health1;
    public Sprite health0;

    

    Color originalColor;

    void Start ()
    {
        bloodParent = GameObject.Find("BloodParent");
        originalColor = gameObject.GetComponentInChildren<SpriteRenderer>().material.color;
        //defaultMat = gameObject.GetComponentInChildren<MeshRenderer>().material;
    }

    void FixedUpdate ()
    {
        Vector3 input = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0) * speed;
        moveTarget = input;
        if (input != Vector3.zero)
        {
            rigidbody2D.velocity = input;
            legs.GetComponent<Animator>().SetBool("isMoving", true);
        }
        else
        {
            rigidbody2D.velocity = Vector3.zero;
            legs.GetComponent<Animator>().SetBool("isMoving", false);
        }
    }

	// Update is called once per frame
	void Update () 
    {
        //rigidbody2D.


        if (health > maxHealth)
        {
            health = maxHealth;
        }

        if (Time.time < ltHealthLostToHit)
        {
            health = lastHealth;
            StartCoroutine(Flash( flashDuration));
        }

        if (Time.time > ltHealthLostToTime + healthLossEveryXSeconds)
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

        HealthValueToSprite();
        if (health <= 0)
        {
            Lose();
        }

        if (Input.GetButtonDown("Fire1"))
        {
            pincers.GetComponent<Pincers>().Chomp();
        }
	}


    void HealthValueToSprite ()
    {
        if (health == 11)
            body.GetComponent<SpriteRenderer>().sprite = health11;
        if (health == 10)
            body.GetComponent<SpriteRenderer>().sprite = health10;
        if (health == 9)
            body.GetComponent<SpriteRenderer>().sprite = health9;
        if (health == 8)
            body.GetComponent<SpriteRenderer>().sprite = health8;
        if (health == 7)
            body.GetComponent<SpriteRenderer>().sprite = health7;
        if (health == 6)
            body.GetComponent<SpriteRenderer>().sprite = health6;
        if (health == 5)
            body.GetComponent<SpriteRenderer>().sprite = health5;
        if (health == 4)
            body.GetComponent<SpriteRenderer>().sprite = health4;
        if (health == 3)
            body.GetComponent<SpriteRenderer>().sprite = health3;
        if (health == 2)
            body.GetComponent<SpriteRenderer>().sprite = health2;
        if (health == 1)
            body.GetComponent<SpriteRenderer>().sprite = health1;
        if (health == 0)
            body.GetComponent<SpriteRenderer>().sprite = health0;
    }

    public void Damage (float damage)
    {
        health -= damage;
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
