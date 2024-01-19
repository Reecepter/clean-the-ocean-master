using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PcCamera : MonoBehaviour
{
    //[SerializeField] float minLookDistance = 25f;
    //[SerializeField] Transform cameraOffset;

    //public float mouseSens = 20f;

    //float xRotation = 0f;

    //private void Start()
    //{
    //    Cursor.lockState = CursorLockMode.Locked;
    //}

    //private void Update()
    //{
    //    float mouseX = Input.GetAxis("Mouse X") * mouseSens * Time.deltaTime;
    //    float mouseY = Input.GetAxis("Mouse X") * mouseSens * Time.deltaTime;

    //    xRotation -= mouseY;
    //    xRotation = Mathf.Clamp(xRotation, -90f, minLookDistance);

    //    transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    //    cameraOffset.Rotate(Vector3.up * mouseX);
    //}
    public float sensitivity = 2.0f; // Adjust the mouse sensitivity

    private float rotationX = 0f;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // Get mouse input
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        // Rotate the player horizontally based on mouse X movement
        transform.Rotate(Vector3.up * mouseX * sensitivity);

        // Calculate the vertical rotation using quaternions
        rotationX -= mouseY * sensitivity;
        rotationX = Mathf.Clamp(rotationX, -90f, 90f);

        // Apply the rotation to the camera
        transform.localRotation = Quaternion.Euler(rotationX, transform.localEulerAngles.y, 0f);
    }

}
