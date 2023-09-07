using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class connectObjects : MonoBehaviour
{
    LineRenderer lr;
    public Transform startPoint;
    public Transform endPoint;
    public Text distancetext;
    // Start is called before the first frame update
    void Start()
    {
        lr = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        lr.SetPositions(new Vector3[] { startPoint.position, endPoint.position });
        float tmp = Vector3.Distance(startPoint.position, endPoint.position);
        distancetext.text = Mathf.FloorToInt(tmp).ToString() + " m";
    }
}
