using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokeSpawner : MonoBehaviour
{
    public GameObject smokePrefab;
    public GameObject spawnedSmoke;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (spawnedSmoke != null)
            {
                Destroy(spawnedSmoke);
            }

            RaycastHit hit;
            if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit))
            {
                spawnedSmoke = Instantiate(smokePrefab, hit.point - (Camera.main.transform.forward * 0.1f), Quaternion.identity);
            }
        }
    }
}
