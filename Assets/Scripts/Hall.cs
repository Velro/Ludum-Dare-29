using UnityEngine;
using System.Collections;

[System.Serializable]
public class Hall
{
    public GameObject gameObject;
    public Transform transform;
    GameObject wall;
    float wallsPerUnit = 2;
    GameObject levelGen;


    public Hall(Vector2[] a, Vector2[] b, GameObject wall)
    {
        levelGen = GameObject.Find(".LevelGen");
        gameObject = new GameObject();
        gameObject.name = "Hall";
        this.wall = wall;
        transform = gameObject.transform;
        if (Mathf.Abs(a[0].x) - Mathf.Abs(b[1].x) == Mathf.Abs(a[0].x) - Mathf.Abs(b[1].x))
        {
            Line(gameObject, a[0], b[0], "HallA");
            Line(gameObject, a[1], b[1], "HallB");
//           Debug.Log("one to one");
        } 
        else if (Mathf.Abs(a[0].x) - Mathf.Abs(b[0].x) == Mathf.Abs(a[1].x) - Mathf.Abs(b[1].x))
        {
            Line(gameObject, a[0], b[1], "HallA");
            Line(gameObject, a[1], b[0], "HallB");
//            Debug.Log("crisscross");
        }
    }
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void Line(GameObject g, Vector2 a, Vector2 b, string name)
    {
        levelGen.GetComponent<LevelGenerator>().verts.Add(new Vector3(a.x, a.y, 0));
        levelGen.GetComponent<LevelGenerator>().verts.Add(new Vector3(b.x, b.y, 0));
        GameObject l = new GameObject();
        l.name = name;
        l.transform.parent = g.transform;
        l.transform.position = Vector3.zero;
        l.AddComponent<EdgeCollider2D>();
        Vector2[] points = new Vector2[2];
        points[0] = a;
        points[1] = b;
        l.GetComponent<EdgeCollider2D>().points = points;
        float numberOfWalls = Vector3.Distance(a, b) * wallsPerUnit;
        float spacing = 1 / numberOfWalls;
        float currentPlace = 0;
        for (int i = 0; i < numberOfWalls; i++)
        {
            Vector3 nextPos = Vector3.Lerp(a, b, currentPlace);
            GameObject thisWall = Wall(nextPos);
            thisWall.transform.parent = l.transform;
            if (Mathf.Abs(Mathf.Abs(a.x) - Mathf.Abs(b.x)) > 5 && Mathf.Abs(Mathf.Abs(a.y) - Mathf.Abs(b.y)) > 5)
            {
                thisWall.transform.rotation = Quaternion.Euler(0, 0, 45);
            }
            currentPlace += spacing;
        }
    }

    GameObject Wall(Vector3 position)
    {
        GameObject wa = new GameObject("Wall");
        wa.AddComponent<SpriteRenderer>();
        wa.GetComponent<SpriteRenderer>().sprite = wall.GetComponent<SpriteRenderer>().sprite;
        wa.transform.position = position;
        return wa;
    }
}
