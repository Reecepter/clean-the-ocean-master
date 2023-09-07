using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using SWS; 
public class WIM_Grab : MonoBehaviour
{
    public GameObject shadowObject;
    public GameObject Hand;
    public GrabbingObject go;
    public LineRenderer lr;
    public splineMove _sM;
    private AudioSource _audioSource;
    private bool isGrabbing;
    // Start is called before the first frame update
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isGrabbing) {
            lr.SetPositions(new Vector3[] { shadowObject.transform.position, Hand.transform.position });
        }
    }

    public void moveToHand() {
        //Debug.Log(shadowObject.transform.name);
        //if (shadowObject.transform.GetComponent<BasicMover>() == null)
        //    return;

        shadowObject.transform.GetComponent<BasicMover>().enabled = false;
        StartCoroutine(SummonToHand());
        
    }

    IEnumerator SummonToHand() {
        Debug.Log(shadowObject.transform.name);
        lr.enabled = true;
        isGrabbing = true;
        _audioSource.Play();
        //Tween myTween = shadowObject.transform.DOMove(Hand.transform.position + _sM.transform.forward.normalized * _sM.speed * 2f, 2f);
        Tween myTween = shadowObject.transform.DOMove(Hand.transform.position, 2f);
        yield return myTween.WaitForCompletion();
        _audioSource.Stop();
        shadowObject.GetComponent<BoxCollider>().isTrigger = true;
        lr.enabled = false;
        go.ReleaseObject();
        //transform.GetChild(0).gameObject.SetActive(false);
        go.pickUpObject(shadowObject);
        isGrabbing = false;
        Destroy(gameObject);
    }
}
