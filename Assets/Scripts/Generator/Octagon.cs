using UnityEngine;
using System.Collections;

[System.Serializable]
public class Octagon
{
    public Transform transform;
    public GameObject gameObject;
    float wallsPerUnit = 1;
    GameObject[] walls;
    float minScale = 1f;
    float maxScale = 1.3f;

    public Octagon(Vector2 center, float radius, float sideCoef, GameObject[] walls)
    {
        gameObject = new GameObject();
        gameObject.name = "Octagon";
        gameObject.transform.position = center;
        transform = gameObject.transform;
        this.walls = walls;

        Line(gameObject, center + new Vector2(-radius * sideCoef, radius), center + new Vector2(radius * sideCoef, radius), "top"); //top
        Line(gameObject, center + new Vector2(radius * sideCoef, radius), center + new Vector2(radius, radius * sideCoef), "topright"); //topright
        Line(gameObject, center + new Vector2(radius, radius * sideCoef), center + new Vector2(radius, -radius * sideCoef), "right"); //right
        Line(gameObject, center + new Vector2(radius, -radius * sideCoef), center + new Vector2(radius * sideCoef, -radius), "bottomright"); //bottomright
        Line(gameObject, center + new Vector2(-radius * sideCoef, -radius), center + new Vector2(radius * sideCoef, -radius), "bottom"); //bottom
        Line(gameObject, center + new Vector2(-radius, -radius * sideCoef), center + new Vector2(-radius * sideCoef, -radius), "bottomleft"); //bottomleft
        Line(gameObject, center + new Vector2(-radius, -radius * sideCoef), center + new Vector2(-radius, radius * sideCoef), "left"); //left
        Line(gameObject, center + new Vector2(-radius * sideCoef, radius), center + new Vector2(-radius, radius * sideCoef), "topLeft"); //topleft

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
            if (a.x != b.x && a.y != b.y)
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
        int rando = (int)Mathf.Floor(Random.Range(0, walls.Length));
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
