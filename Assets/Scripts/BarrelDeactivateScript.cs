using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelDeactivateScript : MonoBehaviour
{
    public GameObject areaToDisable;
    public GameObject areaToEnable;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnDisable()
    {
        if (areaToDisable != null && areaToEnable != null) {  
            areaToDisable.SetActive(false); 
            areaToEnable.SetActive(true);
        }
    }
}
