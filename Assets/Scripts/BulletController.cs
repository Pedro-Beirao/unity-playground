using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float life = 10;
    public float speed = 0.4f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void UpdateLife()
    {
        life -= Time.deltaTime;

        if (life <= 0)
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        UpdateLife();
    }

    void FixedUpdate()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.forward, out hit, speed))
        {
            if (!hit.transform.CompareTag("Player"))
            {
                Destroy(gameObject);
            }
        }

        transform.Translate(Vector3.forward * (speed - 0.1f));
    }
}
