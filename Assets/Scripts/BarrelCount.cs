using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BarrelCount : MonoBehaviour
{
    public static int barrelsCleared = 0;
    [SerializeField]
    private UnityEvent _halfBarrelsCleared;
    [SerializeField]
    private UnityEvent _BarrelsCleared;

    private bool allowHalfFog = true;
    private bool allowFullFog = true;
    void Start()
    {
        ResetBarrelCount();
    }

    // Update is called once per frame
    void Update()
    {
        if(barrelsCleared == 3 && allowHalfFog == true)
        {
            allowHalfFog = false;
            _halfBarrelsCleared?.Invoke();
        }
        if(barrelsCleared == 6 && allowFullFog == true)
        {
            allowFullFog = false;
            _BarrelsCleared?.Invoke();
        }
    }

    private static void ResetBarrelCount()
    {
        barrelsCleared = 0;
    } 
}
