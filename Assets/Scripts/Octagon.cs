using UnityEngine;
using System.Collections;

[System.Serializable]
public class Octagon
{
    public Transform transform;
    public GameObject gameObject;

    public Octagon(Vector2 center, float radius, float sideCoef)
    {
        gameObject = new GameObject();
        gameObject.name = "Octagon";
        gameObject.transform.position = center;
        transform = gameObject.transform;

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
    }
}
