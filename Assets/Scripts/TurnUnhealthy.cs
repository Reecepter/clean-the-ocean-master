using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnUnhealthy : MonoBehaviour
{
    // Start is called before the first frame update
    // Blends between two materials

    //public Material material1;
    //public Material material2;
    float duration = 6.0f;
    //Renderer rend;

    float unhealthyStrength;


    //bool allowTrans = false;

    //void Start()
    //{
    //    //rend = this.GetComponent<Renderer>();

    //    // At start, use the first material
    //    //rend.material = material1;
    //    //material1 = rend.material;
    //    StartCoroutine(LerpMaterial());

    //}

    void Update()
    {
            //float lerp = Mathf.PingPong(Time.time, duration) / duration;
            //rend.material.Lerp(material1, material2, Time.deltaTime);
        // ping-pong between the materials over the duration
    }
    public void StartUnhealthy()
    {
        StartCoroutine(LerpMaterial());
    }
    public void StartHealthy()
    {
        StartCoroutine(LerpMaterialBack());
    }

    public IEnumerator LerpMaterial()
    {
        float time = 0;

        Material seaweedShader = GetComponent<Renderer>().material;

        while (time < duration)
        {
            time += Time.deltaTime;

            unhealthyStrength = Mathf.Lerp(0, 1, time / duration);
            seaweedShader.SetFloat("_LerpAmount", unhealthyStrength);
            
            yield return null;
        }
    }
    public IEnumerator LerpMaterialBack()
    {
        float time = 0;

        Material seaweedShader = GetComponent<Renderer>().material;

        while (time < duration)
        {
            time += Time.deltaTime;

            unhealthyStrength = Mathf.Lerp(1, 0, time / duration);
            seaweedShader.SetFloat("_LerpAmount", unhealthyStrength);

            yield return null;
        }
    }


}
