using UnityEngine;
using System.Collections;

public class Maggot : MonoBehaviour 
{
    public float health;
    public float collisionDamage;
    public float speed;
    public float aggroRange;
    public float disengageDistance;
    public bool gaveHealth = false;
    bool shifted = false;

    Vector3 startLocation;
    bool aggroed = false;

    GameObject player;
    public Sprite deadSprite;
    GameObject levelGen;

    float waitSeconds = 3f;

	// Use this for initialization
	void Start () 
    {
        player = GameObject.Find("tick(Clone)");
        levelGen = GameObject.Find(".LevelGen");
	}
	


    void FixedUpdate ()
    {
        if (aggroed)
        {
            Vector2 direction = new Vector2(player.transform.position.x, player.transform.position.y) - new Vector2(transform.position.x, transform.position.y);
            rigidbody2D.velocity = direction.normalized * speed;
            GetComponent<Animator>().SetBool("isMoving", true);
        }
        else
        {
            rigidbody2D.velocity = Vector2.zero;
            GetComponent<Animator>().SetBool("isMoving", false);
        }


    }

	// Update is called once per frame
	void Update () 
    {
        if (Time.time > waitSeconds && !shifted)
        {
            transform.position = new Vector3(transform.position.x - 1000, transform.position.y, 0);
            shifted = true;
        }
        if (Vector3.Distance(player.transform.position, transform.position) < aggroRange && health > 0)
        {
            aggroed = true;
        }
        
        if (aggroed)
        {
            transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(player.transform.position.y - transform.position.y, player.transform.position.x - transform.position.x) * Mathf.Rad2Deg - 90);
        }


        if (health < 0)
            health = 0;

        if (health == 0)
        {
            GetComponent<Animator>().SetBool("isMoving", false);
            aggroed = false;
            gameObject.GetComponent<SpriteRenderer>().sprite = deadSprite;
            rigidbody2D.velocity = Vector2.zero;
            GetComponent<Animator>().enabled = false;
            if (!gaveHealth)
            {
                player.GetComponent<Player>().health += 1;
                gaveHealth = true;
                levelGen.GetComponent<LevelGenerator>().numberOfEnemies--;
            }
            
        }

	}

    public void Damage(float damage)
    {
        health -= damage;
    }

    void OnCollisionEnter2D (Collision2D other)
    {
        other.gameObject.SendMessage("Damage", collisionDamage, SendMessageOptions.DontRequireReceiver);
    }
}
