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
        //rend.material = material1;
        material1 = rend.material;
        StartCoroutine(LerpMaterial());

    }

    void Update()
    {
            //float lerp = Mathf.PingPong(Time.time, duration) / duration;
            //rend.material.Lerp(material1, material2, Time.deltaTime);
        // ping-pong between the materials over the duration
    }

    IEnumerator LerpMaterial()
    {
        float time = 0;
        while (time < duration)
        {
            material1.Lerp(material1, material2, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        rend.material = material2;
    }


}
