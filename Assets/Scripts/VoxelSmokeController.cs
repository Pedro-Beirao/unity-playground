using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoxelSmokeController : MonoBehaviour
{
    Voxelizer voxelizer;
    Rigidbody rb;

    float maxTime = 5;

    bool colliding = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        voxelizer = GameObject.Find("VoxelizedScene").GetComponent<Voxelizer>();
    }

    // Update is called once per frame
    void Update()
    {
        maxTime -= Time.deltaTime;

        if ((maxTime <= 0) || ( (rb.velocity == Vector3.zero) && colliding))
        {

            voxelizer.StartSmoke(transform.position);
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        colliding = true;
    }
}
