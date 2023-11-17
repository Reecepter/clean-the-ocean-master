using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SWS;
using UnityEngine.Events;

public class PauseShipTrigger : MonoBehaviour
{
    //public splineMove playerSM;
    [SerializeField]
    private UnityEvent _PauseShipAllowDrop;
    private bool allowShipPause = true;
    // Start is called before the first frame update

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Player") && allowShipPause == true)
        {
            Debug.Log("Player ship paused");
            allowShipPause = false;
            Debug.Log("Trigger: "+ this.gameObject.name + " can no longer pause ship");
            _PauseShipAllowDrop?.Invoke();
        }
    }
}
