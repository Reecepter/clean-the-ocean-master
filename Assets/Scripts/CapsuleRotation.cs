using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.InputSystem;
//using UnityEditor.ShaderGraph.Internal;

public class CapsuleRotation : MonoBehaviour
{
    public GameObject subBody;
    public InputActionReference rightHandTurn;
    public InputActionReference leftHandTurn;

    Vector2 thumbstickR;
    Vector3 thumbstickL;
    public float sensitivityRot = 0.75f;
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

    void RotationControl()
    {
        if (thumbstickR.x > 0.6f || thumbstickR.x < -0.6f)
        {
            subBody.transform.Rotate(0, thumbstickR.x * sensitivityRot, 0);
        }
        if (thumbstickL.y > 0.6f || thumbstickL.y < -0.6f)
        {
            subBody.transform.Rotate(thumbstickL.y * sensitivityRot, 0, 0);
        }
    }

    private void OnDisable()
    {
        rightHandTurn.action.performed -= Turned;
        leftHandTurn.action.performed -= Turned2;
    }
}
