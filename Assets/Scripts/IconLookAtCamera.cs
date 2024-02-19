using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IconLookAtCamera : MonoBehaviour
{
    [SerializeField] private BillboardType billboardType;
    [SerializeField] private bool lockX;
    [SerializeField] private bool lockY;
    [SerializeField] private bool lockZ;

    private Vector3 initRotation;
    public enum BillboardType { LookAtCamera, CameraForward };

    private void Awake()
    {
        initRotation = transform.rotation.eulerAngles;
    }

    private void LateUpdate()
    {
        switch (billboardType)
        {
            case BillboardType.LookAtCamera:
                transform.LookAt(Camera.main.transform.position, Vector3.up); 
                break;
            case BillboardType.CameraForward:
                transform.forward = Camera.main.transform.forward;
                break;
            default:
                break;
        }

        Vector3 rotation = transform.rotation.eulerAngles;
        if (lockX) { rotation.x = initRotation.x;}
        if (lockY) { rotation.y = initRotation.y;}
        if (lockZ) { rotation.z = initRotation.z;}
        transform.rotation = Quaternion.Euler(rotation);
    }
}
