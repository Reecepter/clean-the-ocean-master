using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Tweenmove : MonoBehaviour
{
    public Transform startPos;
    public Transform EndPos;
    public float time;
    private bool isStarted = false;
    // Start is called before the first frame update
    void Start()
    {
        transform.localPosition = startPos.localPosition;
        transform.LookAt(EndPos.position);

    }

    // Update is called once per frame
    void Update()
    {
        if (!isStarted) {
            isStarted = true;
            transform.DOLocalMove(EndPos.localPosition, time);
        }

    }
}
