using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class HandControllerScript : MonoBehaviour
{
    [SerializeField] InputActionReference gripInputAction;
    [SerializeField] InputActionReference triggerInputAction;

    Animator handAnimator;

    private void Awake()
    {
        handAnimator = GetComponent<Animator>();
    }


    private void OnEnable()
    {
        /*Quando viene dato l'input su una determinata 
         azione, richiamiamo il delegate performed e 
        gli assegniamo il metodo che vogliamo richiamare*/
        gripInputAction.action.performed += GripPressed;
        triggerInputAction.action.performed += TriggerPressed;

        gripInputAction.action.canceled += ResetGripAnimation;
        triggerInputAction.action.canceled += ResetTriggerAnimation;

    }

    private void ResetGripAnimation(InputAction.CallbackContext obj)
    {    
        handAnimator.SetFloat("Grip", 0f);
    }

    private void ResetTriggerAnimation(InputAction.CallbackContext obj)
    {
        handAnimator.SetFloat("Trigger", 0f);
    }

    private void TriggerPressed(InputAction.CallbackContext obj)
    {
        handAnimator.SetFloat("Trigger", obj.ReadValue<float>());
    }

    private void GripPressed(InputAction.CallbackContext obj)
    {
        handAnimator.SetFloat("Grip", obj.ReadValue<float>());
    }

    private void OnDisable()
    {
        gripInputAction.action.performed -= GripPressed;
        triggerInputAction.action.performed -= TriggerPressed;

        gripInputAction.action.canceled -= ResetGripAnimation;
        triggerInputAction.action.canceled -= ResetTriggerAnimation;
    }
}
