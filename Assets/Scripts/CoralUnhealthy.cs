using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CoralUnhealthy : MonoBehaviour
{
    float duration = 6f;
    public Material material1;
    public Material material2;
    Renderer rend;

    // Start is called before the first frame update
    void Start()
    {
        rend = this.GetComponent<Renderer>();
        Scene sceneCurrent = SceneManager.GetActiveScene();
        if (sceneCurrent.name.Equals("_mainScene_unhealthy")){
            this.GetComponent<Renderer>().material = material2;
        }
        else
        {
            rend.material = material1;
        }
    }
    public void StartUnhealthy()
    {
        StartCoroutine(TurnCoralUnhealthy());
    }
    public IEnumerator TurnCoralUnhealthy()
    {
        float time = 0;
        //rend.material = material1;
        while (time < duration)
        {
            rend.material.Lerp(rend.material, material2, time / duration);
            time += Time.deltaTime;
            
            yield return null;
        }
        rend.material = material2;
    }
}
