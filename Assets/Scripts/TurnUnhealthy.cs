using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnUnhealthy : MonoBehaviour
{
    // Start is called before the first frame update
    // Blends between two materials

    public Material material1;
    public Material material2;
    float duration = 2.0f;
    Renderer rend;

    bool allowTrans = false;

    void Start()
    {
        rend = this.GetComponent<Renderer>();

        // At start, use the first material
        rend.material = material1;
    }

    void Update()
    {
        if (allowTrans)
        {
           rend.material.Lerp(material1, material2, duration * Time.deltaTime);
        }
        // ping-pong between the materials over the duration
    }

    public void startTransition()
    {
        allowTrans = true;
    }

}
