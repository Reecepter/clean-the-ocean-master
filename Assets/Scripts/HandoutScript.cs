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
    private void Start()
    {
        contMoveProvider = moveProvider.GetComponent<ActionBasedContinuousMoveProvider>();
        contMoveProvider.moveSpeed = 0;
    }
    void Awake()
    {
        closeInputActionReference.action.performed += OnClose;
        //contMoveProvider.enabled = false;
    }

    private void OnClose(InputAction.CallbackContext context)
    {
        _CloseHandout?.Invoke();
        //contMoveProvider.enabled = true;
        this.gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        closeInputActionReference.action.performed -= OnClose;
        //contMoveProvider.enabled = true;
    }

}
