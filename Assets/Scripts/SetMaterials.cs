using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetMaterials : MonoBehaviour
{
    public Material highlight;
    public Material highlight1;
    public Material TransparntMat;

    public bool isHovered;
    public bool isGrabbed;
    public bool isoverLapped;
    private MeshRenderer mr;
    private Material[] originalMat;
    // Start is called before the first frame update
    void Start()
    {
        mr = GetComponent<MeshRenderer>();
        if (mr == null)
        {
            mr = transform.GetChild(0).GetComponent<MeshRenderer>();
        }
        originalMat = mr.materials;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void changeMat()
    {
        if (highlight != null)
            mr.material = highlight;
    }

    public void changeMat1()
    {
        if(highlight1 != null)
        mr.material = highlight1;
    }

    public void restoreMat()
    {
        mr.materials = originalMat;
    }

    public void turnTransparent()
    {
        if(TransparntMat != null)
        mr.material = TransparntMat;
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.tag == "gogoTargets")
    //    {
    //        isoverLapped = true;
    //    }
    //}

    //private void OnTriggerExit(Collider other)
    //{
    //    if (other.tag == "gogoTargets")
    //    {
    //        isoverLapped = false;
    //    }
    //}
}
