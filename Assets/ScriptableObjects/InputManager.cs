using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(menuName = "InputManager")]
public class InputManager : ScriptableObject
{
    Controles controles;

    public event Action OnActionP1Action;
    public event Action OnActionP2Action;

    private void OnEnable()
    {
        //Constructor.
        controles = new Controles();

        controles.Gameplay.Enable();

        //Suscripciones a eventos.
        controles.Gameplay.ActionP1.started += OnActionP1;
        controles.Gameplay.ActionP2.started += OnActionP2;
    }

    private void OnActionP2(InputAction.CallbackContext obj)
    {
        OnActionP2Action?.Invoke();
    }

    private void OnActionP1(InputAction.CallbackContext obj)
    {
        OnActionP1Action?.Invoke();
    }
}
