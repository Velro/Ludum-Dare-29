using UnityEngine;
using System.Collections;

[System.Serializable]
public class Hall
{
    public GameObject gameObject;
    public Transform transform;
    GameObject[] walls;
    float wallsPerUnit = 1;
    float minScale = 1f;
    float maxScale = 1.3f;


    public Hall(Vector2[] a, Vector2[] b, GameObject[] walls)
    {
        gameObject = new GameObject();
        gameObject.name = "Hall";
        this.walls = walls;
        transform = gameObject.transform;
        Line(gameObject, a[1], b[0], "HallA");
        Line(gameObject, a[0], b[1], "HallB");
        RaycastHit2D hit = Physics2D.Linecast(Vector2.Lerp(a[0], a[1], 0.5f),Vector2.Lerp(b[0], b[1], 0.5f));
        if (hit.collider != null)
        {
            hit.collider.gameObject.SetActive(false);
            RaycastHit2D hit2 = Physics2D.Linecast(Vector2.Lerp(a[0], a[1], 0.5f), Vector2.Lerp(b[0], b[1], 0.5f));
            hit2.collider.gameObject.SetActive(false);
            Line(gameObject, a[0], b[0], "HallA");
            Line(gameObject, a[1], b[1], "HallB");
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
        wa.AddComponent<MeshFilter>();
        wa.AddComponent<MeshRenderer>();
        int rando = (int) Mathf.Floor(Random.Range(0, walls.Length));
        GameObject modelWall = walls[rando];
        MeshFilter f = modelWall.GetComponent<MeshFilter>();
        MeshRenderer r = modelWall.GetComponent<MeshRenderer>();
        wa.GetComponent<MeshFilter>().mesh = f.mesh;
        wa.GetComponent<MeshRenderer>().material = r.material;
        wa.transform.position = position;
        wa.transform.localScale = new Vector3(Random.Range(minScale, maxScale), Random.Range(minScale, maxScale), Random.Range(minScale, maxScale));
        return wa;
    }
}
