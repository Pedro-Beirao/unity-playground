using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    public float fireDelay = 2;
    float currentDelay = 0;

    public GameObject bulletPrefab;
    public Transform gunModel;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        currentDelay -= Time.deltaTime;

        if ((currentDelay <= 0))
        {
            if (Input.GetKey(KeyCode.Mouse0))
            {
                Instantiate(bulletPrefab, gunModel.position, gunModel.rotation);

                currentDelay = fireDelay;
            }
        }
    }
}
