using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateBack : MonoBehaviour
{
    public GameObject rotateReference;
    Vector3 turnDirection;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TurnBackToRoute()
    {
        turnDirection = rotateReference.transform.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(turnDirection);
        //transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 0.5f);
        StartCoroutine(RotateReset(transform.rotation, rotation));
    }
    IEnumerator RotateReset(Quaternion start, Quaternion end)
    {
        float t = 0f;
        while (t < 2f)
        {
            transform.rotation = Quaternion.Slerp(start, end, t / 2f);
            yield return null;
            t += Time.deltaTime;
        }
        transform.rotation = end;
    }
}
