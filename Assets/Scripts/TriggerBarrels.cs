using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using SWS;

public class TriggerBarrels : MonoBehaviour
{
    bool canDropBarrel = true;
    public splineMove[] barrelArray = new splineMove[7];
    int barrelIndex;
    float dropTime = 0f;

    public InputActionReference barrelTrigger;
    public GameObject dropUI;
    public AudioClip readyAudio;
    public AudioClip triggerAudio;
    private AudioSource audSource;
    private void OnEnable()
    {
        barrelIndex = 0;
        barrelTrigger.action.performed += DropBarrel;
        audSource = GetComponent<AudioSource>();
    }
    //void Start()
    //{
    //    barrelIndex = 0;
    //}

    // Update is called once per frame
    void Update()
    {
        if (dropTime > 0f)
        {
            dropTime -= Time.deltaTime;
            if (dropUI.activeSelf == true)
            {
                dropUI.SetActive(false);
            }
        }
        else if (dropTime <= 0f)
        {
            if(barrelIndex < barrelArray.Length)
            {
                if (dropUI.activeSelf == false)
                {
                    dropUI.SetActive(true);
                }
                if (canDropBarrel == false)
                {
                    canDropBarrel = true;
                    audSource.PlayOneShot(readyAudio);
                }
            } 
        }
    }
    private void DropBarrel(InputAction.CallbackContext context)
    {
        if(canDropBarrel == true && barrelIndex < barrelArray.Length)
        {
            barrelArray[barrelIndex].StartMove();
            barrelIndex++;
            canDropBarrel = false;
            dropTime = 10f;
            audSource.PlayOneShot(triggerAudio);
        }
    }

    private void OnDisable()
    {
        barrelTrigger.action.performed -= DropBarrel;
    }
}
