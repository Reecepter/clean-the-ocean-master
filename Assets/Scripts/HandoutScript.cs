using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

public class HandoutScript : MonoBehaviour
{
    public InputActionReference closeInputActionReference;
    public UnityEvent _CloseHandout;
    public GameObject moveProvider;
    private ActionBasedContinuousMoveProvider contMoveProvider;
    // Start is called before the first frame update
    void OnEnable()
    {
        closeInputActionReference.action.performed += OnClose;
        contMoveProvider = moveProvider.GetComponent<ActionBasedContinuousMoveProvider>();
        contMoveProvider.enabled = false;
    }

    private void OnClose(InputAction.CallbackContext context)
    {
        _CloseHandout?.Invoke();
        this.gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        closeInputActionReference.action.performed -= OnClose;
        contMoveProvider.enabled = true;
    }

}
