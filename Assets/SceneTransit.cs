using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class SceneTransit : MonoBehaviour
{
    public Material fadeMaterial = null;
    public SpriteRenderer credit_SR;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void loadScene(int index) {
        StartCoroutine(Fade(1));
    }

    IEnumerator Fade(int index) {
        Tween myTween = fadeMaterial.DOFade(1f, 1.5f);
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(index);
    }

    public void FadeIn()
    {
        Tween myTween = fadeMaterial.DOFade(0f, 1.5f);
    }

    public void FadeOut()
    {
        Tween myTween = fadeMaterial.DOFade(1f, 1.5f);
    }

    public void showCredit() {
        credit_SR.DOFade(1f, 1.5f);
    }

}
