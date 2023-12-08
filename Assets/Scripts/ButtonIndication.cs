using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonIndication : MonoBehaviour
{
    float blinkStrength = 0f;
    public float lerpSpeed = 1f;
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(blinkInputs());
    }
    private void OnEnable()
    {
        StartCoroutine(blinkInputs());
    }
    private IEnumerator blinkInputs()
    {
        float time = 0;

        Material buttonShader = GetComponent<Renderer>().material;

        while (this.enabled)
        {
            //time += Time.deltaTime;

            blinkStrength = Mathf.PingPong(time += (Time.deltaTime * lerpSpeed / 1), 1f);
            buttonShader.SetFloat("_LerpAmount", blinkStrength);

            yield return new WaitForEndOfFrame();
        }

        buttonShader.SetFloat("_LerpAmount", 0);
    }
}
