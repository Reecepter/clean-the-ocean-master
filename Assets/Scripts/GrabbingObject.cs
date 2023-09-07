using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;
using System;
using UnityEngine.XR.OpenXR.Input;

public class GrabbingObject : MonoBehaviour
{
    [SerializeField]
    private InputActionReference m_ActionReference;
    public enum controllerHand {left, right};
    public controllerHand TargetHand;
    public InputActionReference actionReference { get => m_ActionReference; set => m_ActionReference = value; }
    public InputActionReference controllerAction;
    public GoGo_Transparency ggt;
    public GameObject collidingObject;
    public GameObject objectInHand;
    // Start is called before the first frame update
    public UnityEvent selectedObject; // Invoked when an object is selected

    public UnityEvent hovered; // Invoked when an object is hovered by technique
    public UnityEvent unHovered; // Invoked when an object is no longer hovered by the technique

    public LayerMask interactionLayers;

    public AudioSource _audioSource1;
    public AudioSource _audioSource2;

    private ControllerState _contState;
    private ControllerState _lastcontState;

    void Start()
    {

    }

    Type lastActiveType = null;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (actionReference != null && actionReference.action != null && actionReference.action.enabled && actionReference.action.controls.Count > 0)
        {

            Type typeToUse = null;

            if (actionReference.action.activeControl != null)
            {
                typeToUse = actionReference.action.activeControl.valueType;
            }
            else
            {
                typeToUse = lastActiveType;
            }

            //Debug.Log(typeToUse);

            if (typeToUse == typeof(bool))
            {
                lastActiveType = typeof(bool);
                bool value = actionReference.action.ReadValue<bool>();
                _lastcontState = _contState;
                _contState = value ? ControllerState.TRIGGER_DOWN : ControllerState.NONE;
            }
            else if (typeToUse == typeof(float))
            {
                lastActiveType = typeof(float);
                float value = actionReference.action.ReadValue<float>();
                _lastcontState = _contState;
                _contState = (value > .5f) ? ControllerState.TRIGGER_DOWN : ControllerState.NONE;
            }
        }

        if (_lastcontState != _contState)
        {
            if (_contState == ControllerState.TRIGGER_DOWN)
            {
                if (collidingObject && interactionLayers == (interactionLayers | (1 << collidingObject.gameObject.layer)))
                {
                    selectedObject.Invoke();
                    print("selected " + collidingObject);
                    //selection = collidingObject;
                    pickUpObject();
                }
            }
            else if (_contState == ControllerState.NONE)
            {
                if (objectInHand)
                {
                    ReleaseObject();
                }
            }
        }
    }

    public enum ControllerState
    {
        TRIGGER_UP, TRIGGER_DOWN, NONE
    }


    private void OnTriggerEnter(Collider other)
    {
        SetCollidingObject(other);
        if (interactionLayers == (interactionLayers | (1 << other.gameObject.layer)) && objectInHand == null)
        {
            hovered.Invoke();
        }
        if (objectInHand == null)
        {
            if (other.GetComponent<SetMaterials>() != null)
            {
                other.GetComponent<SetMaterials>().isHovered = true;
                other.GetComponent<SetMaterials>().changeMat();
                //haptic
                Debug.Log("Entered!");
            }
            if (other.GetComponent<Outline>() != null)
            {
                other.GetComponent<Outline>().enabled = true;
                if (TargetHand == controllerHand.left)
                {
                    OpenXRInput.SendHapticImpulse(controllerAction, .3f, .1f, UnityEngine.InputSystem.XR.XRController.leftHand); //Left Hand Haptic Impulse
                }
                else {
                    OpenXRInput.SendHapticImpulse(controllerAction, .3f, .1f, UnityEngine.InputSystem.XR.XRController.rightHand);
                }
                if (!_audioSource2.isPlaying)
                    _audioSource2.Play();
                //haptic
            }

            if (other.GetComponent<WIM_Grab>() != null && other.GetComponent<WIM_Grab>().shadowObject != null)
            {
                other.GetComponent<WIM_Grab>().shadowObject.GetComponent<Outline>().enabled = true;
            }
        }
    }

    public void OnTriggerStay(Collider other)
    {
        SetCollidingObject(other);
    }

    public void OnTriggerExit(Collider other)
    {
        if (!collidingObject)
        {
            return;
        }
        if (interactionLayers == (interactionLayers | (1 << other.gameObject.layer)))
        {
            unHovered.Invoke();
        }
        //if (objectInHand == null)
        //{
            if (other.GetComponent<SetMaterials>() != null)
            {
                other.GetComponent<SetMaterials>().isHovered = false;
                other.GetComponent<SetMaterials>().restoreMat();
                Debug.Log("Released!");
            }
            if (other.GetComponent<Outline>() != null)
            {
                other.GetComponent<Outline>().enabled = false;
                //haptic
            }

            if (other.GetComponent<WIM_Grab>() != null && other.GetComponent<WIM_Grab>().shadowObject != null)
            {
                other.GetComponent<WIM_Grab>().shadowObject.GetComponent<Outline>().enabled = false;
            }
        //}


        collidingObject = null;
    }

    private void SetCollidingObject(Collider col)
    {

        if (collidingObject)
        {
            return;
        }

        collidingObject = col.gameObject;
    }

    private void pickUpObject()
    {
        objectInHand = collidingObject;

        collidingObject = null;

        var joint = AddFixedJoint();
        joint.connectedBody = objectInHand.GetComponent<Rigidbody>();
        if (objectInHand.GetComponent<SetMaterials>() != null)
        {
            objectInHand.GetComponent<SetMaterials>().isGrabbed = true;
            objectInHand.GetComponent<SetMaterials>().changeMat1();
            objectInHand.GetComponent<BoxCollider>().isTrigger = true;
            if (!_audioSource1.isPlaying)
                _audioSource1.Play();
        }
        if (ggt != null)
            ggt.setAllKinetmatic();
    }

    public void pickUpObject(GameObject target)
    {
        objectInHand = target;

        collidingObject = null;

        var joint = AddFixedJoint();
        joint.connectedBody = objectInHand.GetComponent<Rigidbody>();
        if (objectInHand.GetComponent<SetMaterials>() != null)
        {
            objectInHand.GetComponent<SetMaterials>().isGrabbed = true;
            objectInHand.GetComponent<SetMaterials>().changeMat1();
        }
        

        if (ggt != null)
            ggt.setAllKinetmatic();
    }

    private FixedJoint AddFixedJoint()
    {
        FixedJoint fx = gameObject.AddComponent<FixedJoint>();
        fx.breakForce = 20000;
        fx.breakTorque = 20000;
        return fx;
    }

    public void ReleaseObject()
    {

        if (GetComponent<FixedJoint>())
        {
            if (objectInHand.GetComponent<SetMaterials>() != null)
            {
                GetComponent<FixedJoint>().connectedBody.GetComponent<SetMaterials>().isGrabbed = false;
                GetComponent<FixedJoint>().connectedBody.GetComponent<SetMaterials>().restoreMat();
                objectInHand.GetComponent<BoxCollider>().isTrigger = false;
            }
            GetComponent<FixedJoint>().connectedBody = null;
            Destroy(GetComponent<FixedJoint>());
        }

        objectInHand = null;
        WIM.isSummoning = false;
        if (ggt != null)
            ggt.disableAllKinetmatic();
    }

}
