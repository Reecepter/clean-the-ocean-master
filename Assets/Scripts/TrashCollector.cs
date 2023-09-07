using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TrashCollector : MonoBehaviour
{
    public GrabbingObject goL;
    public GrabbingObject goR;
    public Transform trahCan;
    public AudioSource _audioSource;

    ScoreManager sm;

    // Start is called before the first frame update
    void Start()
    {
        sm = GameObject.Find("ScoreManager").GetComponent<ScoreManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "trashcan") {
            if (goL && goL.objectInHand)
            {
                goL.ReleaseObject();
                StartCoroutine(ObsorbTrash());
            }
            else if (goR && goR.objectInHand)
            {
                goR.ReleaseObject();
                StartCoroutine(ObsorbTrash());
            }
        }
    }

    IEnumerator ObsorbTrash() {
        transform.DOMove(trahCan.position, .5f);
        Tween mytween = transform.DOScale(Vector3.zero, .5f);
        _audioSource.Play();
        yield return mytween.WaitForCompletion();
        Destroy(gameObject);
        sm.AddTrashCollected();
    }
}
