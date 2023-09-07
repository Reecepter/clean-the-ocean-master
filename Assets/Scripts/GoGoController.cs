using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoGoController : MonoBehaviour
{
    public GameObject leftHand;
    public GameObject rightHand;

    private GameObject leftController;
    private GameObject rightController;

    void Awake()
    {
        // If the controllers are null will try to set everything up. Otherwise will run.
        if (leftController == null && rightController == null)
        {

            // Adding variables to shadow scripts
            GoGoShadow shadowLeft;
            if ((shadowLeft = leftHand.GetComponent<GoGoShadow>()) != null)
            {
            }
        }
    }
}
