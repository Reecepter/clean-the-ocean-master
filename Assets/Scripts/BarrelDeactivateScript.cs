using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BarrelDeactivateScript : MonoBehaviour
{
    //public GameObject areaToDisable;
    //public GameObject areaToEnable;
    [SerializeField]
    private UnityEvent _startAnimationForArea;
    // Start is called before the first frame update
    //void Start()
    //{
        
    //}

    private void OnDisable()
    {
        //if (areaToDisable != null && areaToEnable != null) {  
        //    areaToDisable.SetActive(false); 
        //    areaToEnable.SetActive(true);
        //}
        _startAnimationForArea?.Invoke();
        BarrelCount.barrelsCleared++;
    }
}
