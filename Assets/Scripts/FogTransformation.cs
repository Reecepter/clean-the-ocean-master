using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FogTransformation : MonoBehaviour
{
    float duration = 20f;

    //float healthyDensity = 0.03f;
    //float unhealthyDensity = 0.015f;

    //Color32 healthyColor = new Color32(35, 82, 106, 255);
    //Color32 unhealthyColor = new Color32(57, 176, 138, 255);

    Color32 colorToChange;
    float densityToChange;
    public Color startColor;
    public Color midwayColor;
    //float midwayDensity;

    public Color endValueColor;
    //public float endValueDensity;
    // Start is called before the first frame update
    void Start()
    {
        //midwayColor = Color.Lerp(healthyColor, unhealthyColor, 0.5f);
        Color colorToChange = RenderSettings.fogColor;
        float densityToChange = RenderSettings.fogDensity;
    }

    public void StartLerpHalfway()
    {
        StartCoroutine(LerpHalfway(midwayColor));
    }
    public void StartLerpToEnd()
    {
        StartCoroutine(LerpToEnd(endValueColor));
    }

    IEnumerator LerpHalfway(Color endColor)
    {
        float time = 0f;
        //Color startColor = colorToChange;
        //float startDensity = densityToChange;
        RenderSettings.fogColor = startColor;
        //RenderSettings.fogDensity = startDensity;

        while (time < duration)
        {
            RenderSettings.fogColor = Color.Lerp(startColor, endColor, time / (duration));
            //RenderSettings.fogDensity = Mathf.Lerp(startDensity, endDensity, time / (duration));
            time += Time.deltaTime;
            yield return null;
        }
        midwayColor = RenderSettings.fogColor;
        //midwayDensity = RenderSettings.fogDensity;
    }
    IEnumerator LerpToEnd(Color endColor)
    {
        float time = 0f;
        Color startColor = midwayColor;
        //float startDensity = midwayDensity;

        while (time < duration)
        {
            RenderSettings.fogColor = Color.Lerp(startColor, endColor, time / duration);
            //RenderSettings.fogDensity = Mathf.Lerp(startDensity, endDensity, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
    }
}
