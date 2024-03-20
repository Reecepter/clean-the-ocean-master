using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompassArrow : MonoBehaviour
{
    public GameObject[] objectiveLocations;
    public float rotSpeed = 3.0f;
    private int i = 0;
    private Transform target;

    private void Start()
    {
        target = objectiveLocations[i].transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(target.position - transform.position),
            rotSpeed * Time.deltaTime);
    }

    public void UpdateTarget()
    {
        i++;
        target = objectiveLocations[i].transform;
    }
}
