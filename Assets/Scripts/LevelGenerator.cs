using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelGenerator : MonoBehaviour 
{
    public int roomsInCritPath = 10;
    public float minDistance = 6;
    public float maxDistance = 10;
    public float minRoomSize = 2;
    public float maxRoomSize = 5.5f;
    public float minSideCoef  = 0.2f;
    public float maxSideCoef = 0.8f;
    int currentRooms;

    public GameObject[] walls;
    public GameObject tick;
	// Use this for initialization
	void Start () 
    {
        currentRooms = roomsInCritPath;
        Octagon o = new Octagon(Vector2.zero, Random.Range(minRoomSize, maxRoomSize), Random.Range(minSideCoef, maxSideCoef), walls);
        o.transform.parent = transform;
        while (currentRooms > 0)
        {
            Octagon z = new Octagon(new Vector2(o.transform.position.x, o.transform.position.y) + NextRoomLocation(), Random.Range(minRoomSize, maxRoomSize), Random.Range(minSideCoef, maxSideCoef), walls);
            z.transform.parent = transform;
            Path(new Vector2(o.transform.position.x, o.transform.position.y), new Vector2(z.transform.position.x, z.transform.position.y));
            if (Random.value > 0.2)
            {
                int rando = (int)Mathf.Floor(Random.Range(0, 3));
                //left
                if (rando == 0)
                {
                    Octagon deadend = new Octagon(new Vector2(z.transform.position.x, z.transform.position.y) + new Vector2(-Random.Range(minDistance, maxDistance), 0), 
                                                  Random.Range(minRoomSize, maxRoomSize), 
                                                  Random.Range(minSideCoef, maxSideCoef), walls);
                    deadend.transform.parent = transform;
                    Path(new Vector2(z.transform.position.x, z.transform.position.y), new Vector2(deadend.transform.position.x, deadend.transform.position.y));
                }
                //right
                if (rando == 1)
                {
                    Octagon deadend = new Octagon(new Vector2(z.transform.position.x, z.transform.position.y) + new Vector2(Random.Range(minDistance, maxDistance), 0),
                              Random.Range(minRoomSize, maxRoomSize),
                              Random.Range(minSideCoef, maxSideCoef), walls);
                    deadend.transform.parent = transform;
                    Path(new Vector2(z.transform.position.x, z.transform.position.y), new Vector2(deadend.transform.position.x, deadend.transform.position.y));
                }
            }
            o = z;
            currentRooms--;
        }
        Mesh mesh = new Mesh();

        GameObject t = Instantiate(tick, Vector3.zero, Quaternion.identity) as GameObject;
        t.SetActive(true);
	}

    Vector2 NextRoomLocation()
    {
        Vector2 loc = new Vector2();
        int direction = (int)Mathf.Floor(Random.Range(0, 3));
        float distance = Random.Range(minDistance, maxDistance);
        //up
        if (direction == 0)
        {
            loc = new Vector2(0, distance);
        }
        //up-right
        else if (direction == 1)
        {
            
            loc = new Vector2(distance, distance);
        }
        //up-left
        else if (direction == 2)
        {
            loc = new Vector2(-distance, distance);
        }
        return loc;
    }

    void Path (Vector2 a, Vector2 b)
    {
        
        Vector2[] pointsA = DestroyInLine(a, b);
        Vector2[] pointsB = DestroyInLine(a, b);
        Hall hall = new Hall(pointsA, pointsB, walls);
        hall.transform.parent = transform;
    }

    Vector2[] DestroyInLine(Vector2 a, Vector2 b)
    {
       // Debug.Log(a);
        RaycastHit2D hit = Physics2D.Linecast(a,b);
        Vector2[] points = new Vector2[2];
        if (hit.collider != null)
        {
            points = hit.collider.gameObject.GetComponent<EdgeCollider2D>().points;
            Destroy(hit.collider.gameObject);
        }
        else
        {
            Debug.Log("couldn't find gameObject " + hit.point);
        }

        return points;
    }
}
