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
    public float sensitivityRot = 0.75f;

    //private float currentRotation = 0;
    float desiredRotation = 0;
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
        thumbstickR = rightHandTurn.action.ReadValue<Vector2>();
        thumbstickL = leftHandTurn.action.ReadValue<Vector2>();
        RotationControl();
        
    }

    //void RotationControl()
    //{
    //    if (thumbstickR.x > 0.6f || thumbstickR.x < -0.6f)
    //    {
    //        subBody.transform.Rotate(0, thumbstickR.x * sensitivityRot, 0);
    //    }
    //    if (thumbstickL.y > 0.6f || thumbstickL.y < -0.6f)
    //    {
    //        // Calculate the desired rotation based on thumbstick input
    //        float desiredRotation = thumbstickL.y * sensitivityRot;

    //        // Clamp the rotation to stay within the range of -90 to 90 degrees
    //        float clampedRotation = Mathf.Clamp(desiredRotation, -90f, 90f);
    //        Debug.Log(clampedRotation);

    //        // Apply the clamped rotation to the object along the X-axis
    //        subBody.transform.localrotation = Quaternion.Euler(clampedRotation, 0, 0);
    //    }
    //}
    //void RotationControl()
    //{
    //    if (thumbstickR.x > 0.6f || thumbstickR.x < -0.6f)
    //    {
    //        subBody.transform.Rotate(0, thumbstickR.x * sensitivityRot, 0);
    //    }

    //    if (thumbstickL.y > 0.6f || thumbstickL.y < -0.6f)
    //    {
    //        // Calculate the desired rotation based on thumbstick input
    //        float desiredRotation = thumbstickL.y * sensitivityRot;

    //        // Get the current local rotation around the X-axis
    //        float currentRotationX = subBody.transform.localRotation.eulerAngles.x;

    //        // Define the maximum allowed rotation (adjust the value accordingly)
    //        float maxRotation = 45f;

    //        // Calculate the potential new rotation without applying it
    //        float newRotation = currentRotationX + desiredRotation;

    //        // Ensure the new rotation stays within the range of -maxRotation to maxRotation
    //        float clampedRotation = Mathf.Clamp(newRotation, -maxRotation, maxRotation);

    //        // Check if the new rotation crosses the 0-degree boundary
    //        if (Mathf.Abs(newRotation) < Mathf.Abs(currentRotationX))
    //        {
    //            // If it does, set the clamped rotation to 0 to avoid jumping
    //            clampedRotation = 0f;
    //        }

    //        Debug.Log(clampedRotation);

    //        // Apply the clamped rotation to the object along the X-axis
    //        subBody.transform.localRotation = Quaternion.Euler(clampedRotation, 0, 0);
    //    }
    //}
    //void RotationControl()
    //{
    //    if (thumbstickR.x > 0.6f || thumbstickR.x < -0.6f)
    //    {
    //        subBody.transform.Rotate(0, thumbstickR.x * sensitivityRot, 0);
    //    }

    //    if (thumbstickL.y > 0.6f || thumbstickL.y < -0.6f)
    //    {
    //        // Calculate the desired rotation based on thumbstick input
    //        float desiredRotation = thumbstickL.y * sensitivityRot;

    //        // Get the current local rotation around the X-axis
    //        Quaternion currentRotation = subBody.transform.localRotation;

    //        // Define the maximum allowed rotation in either direction (adjust the value accordingly)
    //        float maxRotation = 45f;

    //        // Calculate the target rotation based on the thumbstick input
    //        Quaternion targetRotation = Quaternion.Euler(Mathf.Clamp(currentRotation.eulerAngles.x + desiredRotation, -maxRotation, maxRotation), 0, 0);

    //        // Smoothly interpolate between the current and target rotation
    //        float rotationSpeed = 5f; // Adjust the speed of rotation
    //        subBody.transform.localRotation = Quaternion.Lerp(currentRotation, targetRotation, Time.deltaTime * rotationSpeed);
    //    }
    //}
    void RotationControl()
    {
        if (thumbstickR.x > 0.6f || thumbstickR.x < -0.6f)
        {
            subBody.transform.Rotate(0, thumbstickR.x * sensitivityRot, 0);
        }

        if (thumbstickL.y > 0.6f || thumbstickL.y < -0.6f)
        {
            // Calculate the desired rotation based on thumbstick input
            float desiredRotation = thumbstickL.y * sensitivityRot;

            // Get the current local rotation around the X-axis
            Vector3 currentRotation = subBody.transform.localRotation.eulerAngles;

            // Define the maximum allowed rotation in either direction (adjust the value accordingly)
            float maxRotation = 45f;

            // Calculate the target rotation based on the thumbstick input
            float targetRotationX = Mathf.Clamp(currentRotation.x + desiredRotation, -maxRotation, maxRotation);

            // Set the new rotation
            subBody.transform.localRotation = Quaternion.Euler(targetRotationX, currentRotation.y, currentRotation.z);
        }
    }



    private void OnDisable()
    {
        rightHandTurn.action.performed -= Turned;
        leftHandTurn.action.performed -= Turned2;
    }
}
