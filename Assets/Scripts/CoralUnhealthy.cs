using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CoralUnhealthy : MonoBehaviour
{
    float duration = 6f;
    public Color healthyColor = Color.white;
    public Color unhealthyColor;
    Renderer rend;

    // Start is called before the first frame update
    void Start()
    {
        rend = this.GetComponent<Renderer>();
        Scene sceneCurrent = SceneManager.GetActiveScene();
        if (sceneCurrent.name.Equals("_mainScene_unhealthy")){
            this.GetComponent<Renderer>().material.color = unhealthyColor;
        }
        else
        {
            rend.material.color = healthyColor;
        }
    }
    public void StartUnhealthy()
    {
        StartCoroutine(TurnCoralUnhealthy());
    }
    public IEnumerator TurnCoralUnhealthy()
    {
        float time = 0f;
        rend.material.color = healthyColor;
        
        while (time < duration)
        {
            Color.Lerp(healthyColor, unhealthyColor, time / (duration));
            time += Time.deltaTime;
            
            yield return null;
        }
        rend.material.color = unhealthyColor;
    }
    public void StartHealthy()
    {
        StartCoroutine(TurnCoralHealthy());
    }
    public IEnumerator TurnCoralHealthy()
    {
        float time = 0f;
        rend.material.color = unhealthyColor;

        while (time < duration)
        {
            Color.Lerp(unhealthyColor, healthyColor, time / (duration));
            time += Time.deltaTime;

            yield return null;
        }
        rend.material.color = healthyColor;
    }
}
