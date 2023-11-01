using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.InputSystem;
using UnityEditor.ShaderGraph.Internal;

public class CapsuleRotation : MonoBehaviour
{
    public GameObject subBody;
    public InputActionReference rightHandTurn;

    Vector2 thumbstickR;
    public float sensitivityRot = 0.5f;
    // Start is called before the first frame update
    void OnEnable()
    {
        rightHandTurn.action.performed += Turned;
    }

    private void Turned(InputAction.CallbackContext obj)
    {

    }

    // Update is called once per frame
    void Update()
    {
        thumbstickR = rightHandTurn.action.ReadValue<Vector2>();
        RotationControl();
        
    }

    void RotationControl()
    {
        if (thumbstickR.x > 0.6f || thumbstickR.x < -0.6f)
        {
            subBody.transform.Rotate(0, thumbstickR.x * sensitivityRot, 0);
        }
        if (thumbstickR.y > 0.6f || thumbstickR.y < -0.6f)
        {
            subBody.transform.Rotate(thumbstickR.y * sensitivityRot, 0, 0);
        }
    }

    private void OnDisable()
    {
        rightHandTurn.action.performed -= Turned;
    }
}
