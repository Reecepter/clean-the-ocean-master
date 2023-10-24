using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SWS;
using UnityEngine.InputSystem;
using System;

public class MoveStopStart : MonoBehaviour
{
    public splineMove playerSpline;
    public InputActionReference lTriggerInputActionRefence;
    private bool pathPaused = false;

    // Start is called before the first frame update
    void OnEnable()
    {
        lTriggerInputActionRefence.action.performed += PauseMovement;
    }

    private void PauseMovement(InputAction.CallbackContext context)
    {
        if (pathPaused == false)
        {
            playerSpline.Pause();
            pathPaused = true;
        }
        else
        {
            playerSpline.Resume();
            pathPaused = false;
        }
    }

    private void OnDestroy()
    {
        lTriggerInputActionRefence.action.performed -= PauseMovement;
    }

}
