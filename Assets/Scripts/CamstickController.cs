using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamstickController : MonoBehaviour
{
    PlayerController playerController;
    LayerMask playerLayerMask;

    public GameObject cam;
    public Transform raycastStart;
    public Transform stickJoint;

    public float mouseSens = 2;
    public float stickRange = 1;


    // Start is called before the first frame update
    void Start()
    {
        playerController = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        playerLayerMask = LayerMask.NameToLayer("Player");
    }

    void UpdateRotation()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            playerController.canRotate = false;

            float rotY = mouseSens * Input.GetAxis("Mouse X");
            float rotX = -mouseSens * Input.GetAxis("Mouse Y");

            cam.transform.eulerAngles = new Vector3(cam.transform.eulerAngles.x + rotX, cam.transform.eulerAngles.y + rotY, 0);
        }
        else
        {
            playerController.canRotate = true;

            cam.transform.eulerAngles = new Vector3(cam.transform.eulerAngles.x, cam.transform.eulerAngles.y, 0);
        }
    }

    void UpdatePosition()
    {
        RaycastHit hit;
        if (Physics.Raycast(raycastStart.position, raycastStart.forward, out hit, stickRange, playerLayerMask))
        {
            stickJoint.transform.position = hit.point - (raycastStart.forward / 10);
        }
        else
        {
            stickJoint.transform.position = raycastStart.position + raycastStart.forward * stickRange - (raycastStart.forward / 10);
        }
    }

    private void LateUpdate()
    {
        stickJoint.LookAt(raycastStart);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateRotation();
        UpdatePosition();
    }
}
