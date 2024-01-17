using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PcCamera : MonoBehaviour
{
    public InputActionReference mouseDelta;

    private float mouseSensitivity = 100f;
    private Vector2 mouseLook;
    private float xRot = 0f;

    public Transform playerbody;

    private void Awake()
    {
        //playerbody = transform.parent;

        Cursor.lockState = CursorLockMode.Locked;
    }
    private void OnEnable()
    {
        mouseDelta.action.Enable();
    }
    private void OnDisable()
    {
        mouseDelta.action.Disable();
    }
    private void Update()
    {
        Look();
    }
    private void Look()
    {
        mouseLook = mouseDelta.action.ReadValue<Vector2>();

        float mouseX = mouseLook.x * mouseSensitivity * Time.deltaTime;
        float mouseY = mouseLook.y * mouseSensitivity * Time.deltaTime;

        xRot -= mouseY;
        xRot = Mathf.Clamp(xRot, -90f, 90f);


        transform.localRotation = Quaternion.Euler(xRot, 0, 0);
        playerbody.Rotate(Vector3.up * mouseX);
    }
}
