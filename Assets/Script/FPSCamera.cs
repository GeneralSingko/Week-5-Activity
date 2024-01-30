using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSCamera : MonoBehaviour
{
    public Transform player;
    public float mouseSensitivity = 2f;

    private Quaternion initialRotation;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = true;

        initialRotation = transform.localRotation;
    }

    private void Update()
    {
        HandleRotationInput();

        Quaternion cameraRotation = Quaternion.Euler(rotationX, 0f, 0f);
        transform.localRotation = initialRotation * cameraRotation;

        Quaternion playerRotation = Quaternion.Euler(0f, Input.GetAxis("Mouse X") * mouseSensitivity, 0f);
        player.Rotate(playerRotation.eulerAngles);

        Cursor.visible = true;
    }

    private void HandleRotationInput()
    {
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        rotationX -= mouseY;
        rotationX = Mathf.Clamp(rotationX, -90f, 90f);
    }

    private float rotationX = 0f;

}
