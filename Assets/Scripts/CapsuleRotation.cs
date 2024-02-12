using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.InputSystem;
using DG.Tweening;
//using UnityEditor.ShaderGraph.Internal;

public class CapsuleRotation : MonoBehaviour
{
    public GameObject subBody;
    public InputActionReference rightHandTurn;
    public InputActionReference leftHandTurn;

    Vector2 thumbstickR;
    Vector2 thumbstickL;
    public float rotationSpeed = 5f;
    public float maxVerticalAngle = 60f;
    public float minVerticalAngle = -60f;

    //private float currentRotation = 0;
    //float desiredRotation = 0;
    // Start is called before the first frame update
    void OnEnable()
    {
        rightHandTurn.action.performed += Turned;
        leftHandTurn.action.performed += Turned2;
    }

    private void Turned(InputAction.CallbackContext obj)
    {

    }
    private void Turned2(InputAction.CallbackContext obj)
    {

    }
    // Update is called once per frame
    void Update()
    {
        //thumbstickR = rightHandTurn.action.ReadValue<Vector2>();
        //thumbstickL = leftHandTurn.action.ReadValue<Vector2>();
        //RotationControl();
        RotateObjectOnInput();
        
    }
    //void RotationControl()
    //{
    //    if (thumbstickR.x > 0.6f || thumbstickR.x < -0.6f)
    //    {
    //        subBody.transform.Rotate(0, thumbstickR.x * rotationSpeed, 0);
    //    }

    //    if (thumbstickL.y > 0.6f || thumbstickL.y < -0.6f)
    //    {
    //        // Calculate the desired rotation based on thumbstick input
    //        float desiredRotation = thumbstickL.y * rotationSpeed;

    //        // Get the current local rotation around the X-axis
    //        Vector3 currentRotation = subBody.transform.localRotation.eulerAngles;

    //        // Define the maximum allowed rotation in either direction (adjust the value accordingly)
    //        float maxRotation = 45f;

    //        // Calculate the target rotation based on the thumbstick input
    //        float targetRotationX = Mathf.Clamp(currentRotation.x + desiredRotation, -maxRotation, maxRotation);

    //        // Set the new rotation
    //        subBody.transform.localRotation = Quaternion.Euler(targetRotationX, currentRotation.y, currentRotation.z);
    //    }
    //}

    //private void RotateObjectOnInput()
    //{
    //    thumbstickR = rightHandTurn.action.ReadValue<Vector2>();
    //    thumbstickL = leftHandTurn.action.ReadValue<Vector2>();
    //    float verticalRotation = Mathf.Clamp(thumbstickL.y, -90f, 180f);
    //    Vector3 rotationAmount = new Vector3(-verticalRotation, thumbstickR.x, 0f) * rotationSpeed * Time.deltaTime;
    //    subBody.transform.Rotate(rotationAmount, Space.Self);
    //}

    private void OnDisable()
    {
        rightHandTurn.action.performed -= Turned;
        leftHandTurn.action.performed -= Turned2;
    }

    private void RotateObjectOnInput()
    {
        thumbstickR = rightHandTurn.action.ReadValue<Vector2>();
        thumbstickL = leftHandTurn.action.ReadValue<Vector2>();

        // Calculate rotation amount based on thumbstick input
        Vector3 rotationAmount = new Vector3(-thumbstickL.y, thumbstickR.x, 0f) * rotationSpeed * Time.deltaTime;

        // Apply rotation
        subBody.transform.Rotate(rotationAmount, Space.Self);

        // Clamp vertical rotation
        Vector3 currentRotation = subBody.transform.localRotation.eulerAngles;
        currentRotation.x = ClampAngle(currentRotation.x, minVerticalAngle, maxVerticalAngle);
        subBody.transform.localRotation = Quaternion.Euler(currentRotation);
    }

    private float ClampAngle(float angle, float min, float max)
    {
        // Normalize angle to be between -180 and 180 degrees
        angle %= 360f;
        if (angle > 180f)
            angle -= 360f;
        else if (angle < -180f)
            angle += 360f;

        // Clamp angle between min and max
        return Mathf.Clamp(angle, min, max);
    }
}
