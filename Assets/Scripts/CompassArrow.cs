using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompassArrow : MonoBehaviour
{
    public GameObject[] objectiveLocations;
    public float rotSpeed = 3.0f;
    private int i = 0;
    //private Vector3 v;
    private Transform target;

    private void Start()
    {
        target = objectiveLocations[i].transform;
        //rt = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        //Vector3 dir = transform.InverseTransformDirection(target.position - transform.position);
        //float angle = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg;
        //transform.eulerAngles = new Vector3(0, 0, angle);

        //var targetPosLocal = Camera.main.transform.InverseTransformPoint(target.position);
        //var targetAngle = -Mathf.Atan2(targetPosLocal.x, targetPosLocal.y) * Mathf.Rad2Deg + 180;
        //transform.eulerAngles = new Vector3(0, 0, targetAngle);
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(target.position - transform.position),
            rotSpeed * Time.deltaTime);
    }

    public void UpdateTarget()
    {
        i++;
        target = objectiveLocations[i].transform;
    }
}
