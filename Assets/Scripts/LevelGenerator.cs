using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelGenerator : MonoBehaviour 
{
    public int rooms = 10;
    public float minDistance = 6;
    public float maxDistance = 10;
    public float minRoomSize = 2;
    public float maxRoomSize = 5.5f;
    public float minSideCoef  = 0.2f;
    public float maxSideCoef = 0.8f;
    int currentRooms;
	// Use this for initialization
	void Start () 
    {
        currentRooms = 10;
        //lastOctagon.center = 
       // while (currentRooms > 0)
       // {
            Octagon o = new Octagon(Vector2.zero, Random.Range(minRoomSize, maxRoomSize), Random.Range(minSideCoef, maxSideCoef));
            o.transform.parent = transform;
            Octagon z = new Octagon(new Vector2(10,10), Random.Range(minRoomSize, maxRoomSize), Random.Range(minSideCoef, maxSideCoef));
            z.transform.parent = transform;
            bool boolean = true;
            while (boolean == true)
            {
                boolean = DestroyInLine(o.transform.position, z.transform.position);
            }

       // }
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}

    bool DestroyInLine(Vector2 a, Vector2 b)
    {
        RaycastHit2D hit = Physics2D.Raycast(a, b);
        if (hit != null)
        {
            Destroy(hit.collider.gameObject);
            return true;
        }
        else
        {
            return false;
        }
    }

    void Octagon(Vector2 center, float radius, float sideCoef, string goingIn, string goingOut)
    {
        GameObject o = new GameObject();
        o.name = "Octagon";
        o.transform.position = center;
        o.transform.parent = transform;
 
        Line(o, center + new Vector2(-radius * sideCoef, radius), center + new Vector2(radius * sideCoef, radius), "top"); //top
        Line(o, center + new Vector2(radius * sideCoef, radius), center + new Vector2(radius, radius * sideCoef), "topright"); //topright
        Line(o, center + new Vector2(radius, radius * sideCoef), center + new Vector2(radius, -radius * sideCoef), "right"); //right
        Line(o, center + new Vector2(radius, -radius * sideCoef), center + new Vector2(radius * sideCoef, -radius), "bottomright"); //bottomright
        Line(o, center + new Vector2(-radius * sideCoef, -radius), center + new Vector2(radius * sideCoef, -radius), "bottom"); //bottom
        Line(o, center + new Vector2(-radius, -radius * sideCoef), center + new Vector2(-radius * sideCoef, -radius), "bottomleft"); //bottomleft
        Line(o, center + new Vector2(-radius, -radius * sideCoef), center + new Vector2(-radius, radius * sideCoef), "left"); //left
        Line(o, center + new Vector2(-radius * sideCoef, radius), center + new Vector2(-radius, radius * sideCoef), "topLeft"); //topleft

    }

    void Line (GameObject g, Vector2 a, Vector2 b, string name)
    {
        GameObject l = new GameObject();
        l.name = name;
        l.transform.parent = g.transform;
        l.transform.position = Vector3.zero;
        l.AddComponent<EdgeCollider2D>();
        Vector2[] points = new Vector2[2];
        points[0] = a;
        points[1] = b;
        l.GetComponent<EdgeCollider2D>().points = points;
    }

}
