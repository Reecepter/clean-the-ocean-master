using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class WIM : MonoBehaviour
{
    [SerializeField]
    private InputActionReference m_ActionReference;
    public InputActionReference actionReference { get => m_ActionReference; set => m_ActionReference = value; }

    private ControllerState _contState;
    private ControllerState _lastcontState;
    private bool _showWim;

    public GrabbingObject go;
    public GameObject GoGoObjects;
    public GameObject pointer;
    public float distanceThreshold;
    public static bool isSummoning;
    // Start is called before the first frame update

    public enum ControllerState
    {
        MENU_UP, MENU_DOWN, NONE
    }

    Type lastActiveType = null;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (go.objectInHand != null && go.objectInHand.GetComponent<WIM_Grab>()!= null && !isSummoning) {
            if (Vector3.Distance(go.objectInHand.transform.position, transform.position) > distanceThreshold) {
                go.objectInHand.GetComponent<WIM_Grab>().moveToHand();
                isSummoning = true;
            }
        }
    }

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
                _contState = value ? ControllerState.MENU_DOWN : ControllerState.NONE;
            }
            else if (typeToUse == typeof(float))
            {
                lastActiveType = typeof(float);
                float value = actionReference.action.ReadValue<float>();
                _lastcontState = _contState;
                _contState = (value > .5f) ? ControllerState.MENU_DOWN : ControllerState.NONE;
            }
        }

        //if (_lastcontState != _contState) {
        //    if (_contState == ControllerState.MENU_DOWN) {
        //        toggleShowWim();
        //    }

        //}
    }
    //public void toggleShowWim()
    //{
    //    _showWim = !_showWim;
    //    this.gameObject.transform.GetChild(0).gameObject.SetActive(_showWim);
    //    pointer.SetActive(_showWim);
    //    GoGoObjects.SetActive(!_showWim);
    //    GoGoObjects.GetComponentInChildren<GoGo_Transparency>().AllOpaque();
    //}

}
