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

    bool canMerge = true;
    // Update is called once per frame
    void Update()
    {
        HashSet<Vector3> newSmokingPositions = new HashSet<Vector3>();

        foreach (Vector3 position in smokingPositions)
        {
            // Would be better to just create meshes instead of new gameobjects
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

        // Merge meshes to greatly improve performance
        if (smokingPositions.Count <= 0 && canMerge)
        {
            Vector3 parentPos = transform.position;
            transform.position = Vector3.zero;

            canMerge = false;


            MeshFilter[] meshFilters = GetComponentsInChildren<MeshFilter>();
            CombineInstance[] combine = new CombineInstance[meshFilters.Length];

            int i = 0;
            while (i < meshFilters.Length)
            {
                combine[i].mesh = meshFilters[i].sharedMesh;
                combine[i].transform = meshFilters[i].transform.localToWorldMatrix;
                meshFilters[i].gameObject.SetActive(false);

                i++;
            }

            Mesh mesh = new Mesh();
            mesh.indexFormat = UnityEngine.Rendering.IndexFormat.UInt32;
            mesh.CombineMeshes(combine);
            transform.GetComponent<MeshFilter>().sharedMesh = mesh;
            transform.gameObject.SetActive(true);

            transform.position = parentPos;
        }
    }
}
