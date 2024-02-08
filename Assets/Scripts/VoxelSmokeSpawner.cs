using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoxelSmokeSpawner : MonoBehaviour
{
    public GameObject voxelSmokePrefab;
    public Transform voxelSmokeSpawnPosition;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            GameObject obj = Instantiate(voxelSmokePrefab, voxelSmokeSpawnPosition.position, Quaternion.identity);
            Rigidbody objRb = obj.GetComponent<Rigidbody>();

            objRb.AddRelativeForce(Camera.main.transform.forward * 20, ForceMode.Impulse);
        }
    }
}
