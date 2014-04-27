﻿using UnityEngine;
using System.Collections;

[System.Serializable]
public class Octagon
{
    public Transform transform;
    public GameObject gameObject;
    float wallsPerUnit = 2;
    GameObject wall;
    GameObject levelGen;

    public Octagon(Vector2 center, float radius, float sideCoef, GameObject wall)
    {
        levelGen = GameObject.Find(".LevelGen");
        gameObject = new GameObject();
        gameObject.name = "Octagon";
        gameObject.transform.position = center;
        transform = gameObject.transform;
        this.wall = wall;

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
        wa.AddComponent<SpriteRenderer>();
        wa.GetComponent<SpriteRenderer>().sprite = wall.GetComponent<SpriteRenderer>().sprite;
        wa.transform.position = position;
        return wa;
    }
}
