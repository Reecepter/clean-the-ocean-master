using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SWS;

public class SplineFishDeath : MonoBehaviour
{
    public splineMove fishSpline;
    private Animator anim;
    private BasicMover mover;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        mover = GetComponent<BasicMover>();
    }

    public void BeginDeath()
    {
        StartCoroutine(Death());
    }

    public IEnumerator Death()
    {
        fishSpline.speed = 0.5f;
        anim.SetBool("isDying", true);
        yield return new WaitForSeconds(2f);
        fishSpline.Stop();
        mover.enabled = true;
    }
}
