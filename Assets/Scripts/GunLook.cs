using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunLook : MonoBehaviour
{
    public Transform target;
    public float speed;

    //private float maxYRot = 90f;
    //private float minYRot = -180f;

    void Update()
    {

        Vector3 targetDir = target.position - transform.position;
        //targetDir.y = 0;
        float step = speed * Time.deltaTime;

        Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, step, 0.0F);
        Debug.DrawRay(transform.position, newDir, Color.red);

        //Quaternion targetRot = Quaternion.LookRotation(newDir);
        transform.rotation = Quaternion.LookRotation(newDir);
        //transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, speed * Time.deltaTime);
        //LimitRot();
    }

    //private void LimitRot()
    //{
    //    Vector3 gunsEulerAngles = transform.rotation.eulerAngles;

    //    gunsEulerAngles.y = (gunsEulerAngles.y > 180) ? gunsEulerAngles.y = 360 : gunsEulerAngles.y;
    //    gunsEulerAngles.y = Mathf.Clamp(gunsEulerAngles.y, minYRot, maxYRot);

    //    transform.rotation = Quaternion.Euler(gunsEulerAngles);
    //}

}
