using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunRotation : MonoBehaviour
{
    public Transform rightHand;

    private Quaternion initialObjectRotation;
    private Quaternion initialControllerRotation;

    private bool set = false;
    // Update is called once per frame
    void Update()
    {
        if (set == false)
        {
            initialObjectRotation = transform.rotation;
            initialControllerRotation = rightHand.rotation;
            set = true;
        }

        Quaternion controllerAngularDifference = initialControllerRotation * rightHand.rotation;
        transform.rotation = controllerAngularDifference * initialObjectRotation;

    }
}
