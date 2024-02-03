using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    CharacterController characterController;
    public Camera mainCamera;

    float yVelocity = 0;

    public float movementSpeed = 10;
    public float jumpHeight = 10;
    public float mouseSens = 2;

    public bool canRotate = true;

    void Start()
    {
        characterController = gameObject.GetComponent<CharacterController>();

        Cursor.lockState = CursorLockMode.Locked;
    }

    void UpdateMovement()
    {
        if (characterController.isGrounded && yVelocity <= 0)
        {
            yVelocity = 0;
        }
        if (Input.GetButton("Jump") && characterController.isGrounded)
        {
            yVelocity += jumpHeight;
        }

        // I usually use -20 as the gravity value since -9.81 feels very floaty
        yVelocity += -20 * Time.deltaTime;

        Vector3 move = new Vector3(Input.GetAxis("Horizontal") * movementSpeed, yVelocity, Input.GetAxis("Vertical") * movementSpeed);
        characterController.Move(transform.rotation * move * Time.deltaTime);
    }

    void UpdateRotation()
    {
        if (canRotate)
        {
            float rotY = mouseSens * Input.GetAxis("Mouse X");
            float rotX = -mouseSens * Input.GetAxis("Mouse Y");

            transform.Rotate(new Vector3(0, rotY, 0));

            mainCamera.transform.localEulerAngles = new Vector3(mainCamera.transform.localEulerAngles.x + rotX, 0, 0);
        }
    }

    void ProcessInputs()
    {

    }

    void Update()
    {
        UpdateMovement();
        UpdateRotation();

        ProcessInputs();
    }
}
