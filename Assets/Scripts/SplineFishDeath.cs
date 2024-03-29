using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SWS;

public class SplineFishDeath : MonoBehaviour
{
    public splineMove fishSpline;
    public bool isRotating;
    public bool hasAudio;
    public AudioSource aud = null;

    private Animator anim;
    private BasicMover mover;
    private bool isDeathStarted = false; // Flag to check if death has started
    private float targetHeight = 0.5f; // Height above terrain to move to
    private float moveSpeed = 0.5f; // Speed of movement
    private float lerpDuration = 6f;

    void Start()
    {
        anim = GetComponent<Animator>();
        mover = GetComponent<BasicMover>();
    }

    public void BeginDeath()
    {
        if (!isDeathStarted)
        {
            StartCoroutine(Death());
            isDeathStarted = true; // Set death started flag to true
        }
    }

    public IEnumerator Death()
    {
        // Set up death animation and effects
        anim.SetBool("isDying", true);
        fishSpline.speed = 0.5f;
        yield return new WaitForSeconds(2f);

        // Stop spline movement
        fishSpline.Stop();

        if (isRotating)
        {
            StartCoroutine(RotateAfterDeath());
        }
        // Move towards target height above terrain
        Vector3 targetPosition = CalculateTargetPositionAboveTerrain();
        while (transform.position.y != targetPosition.y)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            yield return null;
        }
        if (hasAudio && aud != null)
        {
            aud.Stop();
        }
        mover.enabled = true;
    }

    // Calculate the target position above terrain using a raycast
    private Vector3 CalculateTargetPositionAboveTerrain()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, -Vector3.up, out hit, Mathf.Infinity))
        {
            Vector3 targetPosition = hit.point + Vector3.up * targetHeight;
            //Debug.Log(targetPosition);
            return targetPosition;
        }
        else
        {
            //Debug.Log("RaycastFailed");
            // If raycast fails, return the current position with targetHeight added
            return transform.position + Vector3.up * targetHeight;
        }
    }

    private IEnumerator RotateAfterDeath()
    {
        float timeElapsed = 0;
        Quaternion startRotation = transform.rotation;
        Quaternion targetRotation = transform.rotation * Quaternion.Euler(0, 0, 180);

        while (timeElapsed < lerpDuration)
        {
            transform.rotation = Quaternion.Slerp(startRotation, targetRotation, timeElapsed / lerpDuration);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        transform.rotation = targetRotation;
    }
}
