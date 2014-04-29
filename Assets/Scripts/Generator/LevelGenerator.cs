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
    [System.NonSerialized]
    public float numberOfEnemies = 0;
    [System.NonSerialized]
    public float numberOfVessels = 0;
    public float chanceOf1MaggotInRoom = 0.5f;
    public float chanceOf2MaggotsInRoom = 0.8f;
    public float chanceOf1BloodVessel = 0.4f;
    public float chanceOf2BloodVessel = 0.2f;
    int currentRooms;

    public GameObject[] walls;
    public GameObject tick;
    public GameObject maggot;
    public GameObject bloodVessel;
    public GameObject controls;

	// Use this for initialization
	void Awake () 
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

                if (rando > chanceOf2MaggotsInRoom)
                {
                    Instantiate (maggot,new Vector3(z.transform.position.x + Random.Range(-1,1)+1000, z.transform.position.y + Random.Range(-1,1), 0), Quaternion.Euler(0,0,Random.Range(0,360)));
                    Instantiate(maggot, new Vector3(z.transform.position.x + Random.Range(-1, 1) + 1000, z.transform.position.y + Random.Range(-1, 1), 0), Quaternion.Euler(0, 0, Random.Range(0, 360)));
                    numberOfEnemies += 2;
                }
                else if (rando > chanceOf1MaggotInRoom)
                {
                    Instantiate(maggot, new Vector3(z.transform.position.x + Random.Range(-1, 1) + 1000, z.transform.position.y + Random.Range(-1, 1), 0), Quaternion.Euler(0, 0, Random.Range(0, 360)));
                    numberOfEnemies++;
                }

                if (rando < chanceOf2BloodVessel)
                {
                    Instantiate(bloodVessel, new Vector3(z.transform.position.x + Random.Range(-1, 1) + 1000, z.transform.position.y + Random.Range(-1, 1), 0), Quaternion.Euler(0, 0, Random.Range(0, 360)));
                    Instantiate(bloodVessel, new Vector3(z.transform.position.x + Random.Range(-1, 1) + 1000, z.transform.position.y + Random.Range(-1, 1), 0), Quaternion.Euler(0, 0, Random.Range(0, 360)));
                    numberOfVessels += 2;
                }
                else if (rando < chanceOf1BloodVessel)
                {
                    Instantiate(bloodVessel, new Vector3(z.transform.position.x + Random.Range(-1, 1) + 1000, z.transform.position.y + Random.Range(-1, 1), 0), Quaternion.Euler(0, 0, Random.Range(0, 360)));
                    numberOfVessels++;
                }
            }
            o = z;
            currentRooms--;
        }
        Mesh mesh = new Mesh();

        GameObject t = Instantiate(tick, Vector3.zero, Quaternion.identity) as GameObject;
        GameObject controlsGameObj = Instantiate(controls, Vector3.zero, Quaternion.identity) as GameObject;
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
        Vector2 hallCenter = Vector2.Lerp(Vector2.Lerp(pointsA[0], pointsA[1], 0.5f), Vector2.Lerp(pointsB[0], pointsB[1], 0.5f), 0.5f);
        float rando = Random.value;
        if (rando < chanceOf2BloodVessel)
        {
            Instantiate(bloodVessel, new Vector3(hallCenter.x + Random.Range(-1, 1) + 1000, hallCenter.y + Random.Range(-1, 1), 0), Quaternion.Euler(0, 0, Random.Range(0, 360)));
            Instantiate(bloodVessel, new Vector3(hallCenter.x + Random.Range(-1, 1) + 1000, hallCenter.y + Random.Range(-1, 1), 0), Quaternion.Euler(0, 0, Random.Range(0, 360)));
            numberOfVessels += 2;
        }
        else if (rando < chanceOf1BloodVessel)
        {
            Instantiate(bloodVessel, new Vector3(hallCenter.x + Random.Range(-1, 1) + 1000, hallCenter.y + Random.Range(-1, 1), 0), Quaternion.Euler(0, 0, Random.Range(0, 360)));
            numberOfVessels++;
        }
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
