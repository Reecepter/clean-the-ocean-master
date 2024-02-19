using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class SceneTransit : MonoBehaviour
{
    public Material fadeMaterial = null;
    public SpriteRenderer credit_SR;
    public GameObject boxForFade;
    // Start is called before the first frame update
    void Start()
    {
        FadeIn();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void loadScene(int index) {
        StartCoroutine(Fade(1));
    }

    IEnumerator Fade(int index) {
        boxForFade.SetActive(true);
        Tween myTween = fadeMaterial.DOFade(1f, 1.5f);
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(index);
    }

    public void FadeIn()
    {
        Tween myTween = fadeMaterial.DOFade(0f, 1.5f);
        //boxForFade.SetActive(false);
    }

    public void FadeOut()
    {
        Tween myTween = fadeMaterial.DOFade(1f, 1.5f);
    }

    public void showCredit() {
        credit_SR.DOFade(1f, 1.5f);
    }

}
