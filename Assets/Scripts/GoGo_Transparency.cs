using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.OpenXR.Input;
using UnityEngine.InputSystem;

public class GoGo_Transparency : MonoBehaviour
{
    public GameObject leftController;
    public GameObject rightController;
    public Transform mainCam;
    public float offset;
    public float radius;
    public InputActionReference controllerActionLeft;
    public InputActionReference controllerActionRight;
    private RaycastHit[] hits;
    private RaycastHit[] hits1;
    private GameObject[] targetObjects;
    private int countL;
    private int countR;
    private int lastcountL;
    private int lastcountR;
    // Start is called before the first frame update
    void Start()
    {
        targetObjects = GameObject.FindGameObjectsWithTag("gogoTargets");
    }

    // Update is called once per frame
    void Update()
    {
        countL = 0;
        countR = 0;

        hits = Physics.SphereCastAll(mainCam.position, radius, leftController.transform.position - mainCam.position, Vector3.Distance(mainCam.position, leftController.transform.position) - offset);

        hits1 = Physics.SphereCastAll(mainCam.position, radius, rightController.transform.position - mainCam.position, Vector3.Distance(mainCam.position, rightController.transform.position) - offset);

        for (int i = 0; i < targetObjects.Length; i++)
        {
            if (targetObjects[i] != null && !targetObjects[i].transform.GetComponent<SetMaterials>().isHovered && !targetObjects[i].transform.GetComponent<SetMaterials>().isGrabbed)
                targetObjects[i].transform.GetComponent<SetMaterials>().restoreMat();
        }

        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[i].transform.tag !="trash" && hits[i].transform.GetComponent<SetMaterials>() != null && !hits[i].transform.GetComponent<SetMaterials>().isGrabbed)
            {
                hits[i].transform.GetComponent<SetMaterials>().turnTransparent();
                countL++;
            }
        }

        for (int i = 0; i < hits1.Length; i++)
        {
            if (hits1[i].transform.tag != "trash" && hits1[i].transform.GetComponent<SetMaterials>() != null && !hits1[i].transform.GetComponent<SetMaterials>().isGrabbed)
            {
                hits1[i].transform.GetComponent<SetMaterials>().turnTransparent();
                countR++;
            }
        }

        if (countL > lastcountL) {
            OpenXRInput.SendHapticImpulse(controllerActionLeft, .3f, .1f, UnityEngine.InputSystem.XR.XRController.leftHand);
        }

        if (countR > lastcountR)
        {
            OpenXRInput.SendHapticImpulse(controllerActionRight, .3f, .1f, UnityEngine.InputSystem.XR.XRController.rightHand);
        }

        lastcountL = countL;
        lastcountR = countR;

    }

    public void setAllKinetmatic()
    {
        for (int i = 0; i < targetObjects.Length; i++)
        {
            if (targetObjects[i] != null && !targetObjects[i].GetComponent<SetMaterials>().isGrabbed)
            {
                targetObjects[i].GetComponent<BoxCollider>().isTrigger = true;
            }
        }
    }

    public void disableAllKinetmatic()
    {
        for (int i = 0; i < targetObjects.Length; i++)
        {
            if (targetObjects[i] != null && !targetObjects[i].GetComponent<SetMaterials>().isoverLapped)
            {
                targetObjects[i].GetComponent<BoxCollider>().isTrigger = false;
            }
        }
    }

    public void AllOpaque() {
        if (targetObjects == null)
            return;

        for (int i = 0; i < targetObjects.Length; i++)
        {
            if (targetObjects[i] != null)
                targetObjects[i].transform.GetComponent<SetMaterials>().restoreMat();
        }
    }
}
