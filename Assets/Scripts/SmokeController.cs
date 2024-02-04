using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SmokeController : MonoBehaviour
{
    public int particleLimit = 100;
    public float elipseHRadius = 10;
    public float elipseVRadius = 7;
    bool filledElipse = false;

    Mesh mesh;

    public HashSet<Vector3> smokedPositions = new HashSet<Vector3>();
    public HashSet<Vector3> smokingPositions = new HashSet<Vector3>();

    public GameObject smokeParticle;

    Vector3[] directions = { Vector3.forward, Vector3.back, Vector3.left, Vector3.right, Vector3.up, Vector3.down};

    // Start is called before the first frame update
    void Start()
    {
        smokingPositions.Add(transform.position);

        // Pre-calculating the value we'll need
        elipseHRadius = Mathf.Pow(elipseHRadius, 2);
        elipseVRadius = Mathf.Pow(elipseVRadius, 2);

        mesh = GetComponent<Mesh>();
    }

    bool IsInsideElipse(Vector3 position)
    {
        return (((Mathf.Pow(position.x - transform.position.x, 2) / elipseHRadius) + (Mathf.Pow(position.y - transform.position.y, 2) / elipseVRadius) + (Mathf.Pow(position.z - transform.position.z, 2) / elipseHRadius)) <= 1);
    }

    Vector3[] meshVertices =
    {
        new Vector3 (0, 0, 0),
        new Vector3 (1, 0, 0),
        new Vector3 (1, 1, 0),
        new Vector3 (0, 1, 0),
        new Vector3 (0, 1, 1),
        new Vector3 (1, 1, 1),
        new Vector3 (1, 0, 1),
        new Vector3 (0, 0, 1),
    };

    int[] meshTriangles =
    {
        0, 2, 1, //face front
		0, 3, 2,
        2, 3, 4, //face top
		2, 4, 5,
        1, 2, 5, //face right
		1, 5, 6,
        0, 7, 4, //face left
		0, 4, 3,
        5, 4, 7, //face back
		5, 7, 6,
        0, 6, 7, //face bottom
		0, 1, 6
    };

    Mesh CreateCube()
    {
        Mesh mesh = new Mesh();
        mesh.vertices = meshVertices;
        mesh.triangles = meshTriangles;
        mesh.Optimize();
        mesh.RecalculateNormals();

        return mesh;
    }

    // Update is called once per frame
    void Update()
    {
        HashSet<Vector3> newSmokingPositions = new HashSet<Vector3>();

        foreach (Vector3 position in smokingPositions)
        {
            Instantiate(smokeParticle, position, Quaternion.identity, transform);
            smokedPositions.Add(position);
            particleLimit--;

            if (particleLimit <= 0)
            {
                break;
            }

            if (!IsInsideElipse(position))
            {
                continue;
            }

            foreach (Vector3 dir in directions)
            {
                if (!Physics.Raycast(position, dir, 0.5f) && !smokedPositions.Contains(position + dir * 0.5f))
                {
                    newSmokingPositions.Add(position + dir * 0.5f);
                }
            }
        }

        smokingPositions = newSmokingPositions;
    }
}
