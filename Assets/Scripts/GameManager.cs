using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.Playables;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private InputActionReference m_ActionReference;
    public InputActionReference actionReference { get => m_ActionReference; set => m_ActionReference = value; }
    public Transform playerPos;
    public Transform playerRig;
    public PlayableDirector PD;
    private ControllerState _contState;
    private ControllerState _lastcontState;
    private IEnumerator temp;


    public GameObject gogoObj;
    public GameObject gogohandLObj;
    public GameObject gogohandRObj;
    public GameObject wimObj;
    public GameObject wimPtrObj;
    public GameObject selIndicatorLine;

    public GameObject textL, textR;

    public GameObject GoGoInst, GoGoVideo;
    public GameObject WIMInst, WIMVideo;
    public ScoreManager sm;

    public static int currentstate = 0;
    // Start is called before the first frame update
    void Start()
    {
        _contState = ControllerState.NONE;
        _lastcontState = ControllerState.NONE;

        if (playerPos != null) {
            playerRig.position = playerPos.position;
        }
    }

    public enum ControllerState
    {
        TRIGGER_UP, TRIGGER_DOWN, NONE
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

        if (_lastcontState != _contState && Time.timeSinceLevelLoad > 3f)
        {
            if (_contState == ControllerState.TRIGGER_DOWN)
            {
                //SceneManager.LoadScene((SceneManager.GetActiveScene().buildIndex + 1) % 2);
                //if(SceneManager.GetActiveScene().buildIndex == 0)
                //    SceneManager.LoadScene((SceneManager.GetActiveScene().buildIndex + 1));
            }
        }
    }

    public void DisableAllInteractions()
    {
        gogoObj.SetActive(false);
        gogohandLObj.SetActive(false);
        gogohandRObj.SetActive(false);
        wimObj.SetActive(false);
        wimPtrObj.SetActive(false);
        textL.SetActive(false);
        textR.SetActive(false);
        selIndicatorLine.SetActive(false);
    }

    public void EnableGogo()
    {
        sm.targetText = " / 5";
        sm.setTargetText();
        currentstate = 1;
        gogoObj.SetActive(true);
        gogohandLObj.SetActive(true);
        gogohandRObj.SetActive(true);
        textL.SetActive(true);
        textR.SetActive(true);
        selIndicatorLine.SetActive(true);
    }

    public void EnableWim()
    {
        sm.targetText = " / " + (sm._trashCollected + 5);
        sm.setTargetText();
        currentstate = 2;
        wimObj.SetActive(true);
        wimPtrObj.SetActive(true);
        selIndicatorLine.SetActive(true);
    }

    public void waitForCollection(float waitDelay) {

        temp = waitCoroutine(waitDelay);
        StartCoroutine(temp);

    }

    IEnumerator waitCoroutine(float time) {
        yield return new WaitForSeconds(time);
        PD.Resume();
    }

    public void stopWaiting() {
        StopCoroutine(temp);
    }

    public void enableWIMInst() {
        GoGoInst.SetActive(false);
        GoGoVideo.SetActive(false);
        WIMInst.SetActive(true);
        WIMVideo.SetActive(true);
    }
}
