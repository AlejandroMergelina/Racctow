using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(menuName = "InputManager")]
public class InputManager : ScriptableObject
{
    Controles controles;

    public event Action OnRotateCameraAction;

    public event Action OnActionP1Action;
    public event Action OnActionP2Action;

    private String[] actionMaps = { "MoveOut", "CombatMode", "Menu" };

    public string[] ActionMaps { get => actionMaps;}

    private void OnEnable()
    {
        //Constructor.
        controles = new Controles();

        controles.MoveOut.Enable();

        controles.MoveOut.RotateCamera.started += OnRotateCamera;

        //Suscripciones a eventos del ActionPnº.
        controles.CombatMode.ActionP1.started += OnActionP1;
        controles.CombatMode.ActionP2.started += OnActionP2;


    }

    private void OnRotateCamera(InputAction.CallbackContext obj)
    {
        OnRotateCameraAction?.Invoke();
    }

    private void OnActionP2(InputAction.CallbackContext obj)
    {
        OnActionP2Action?.Invoke();

    }

    private void OnActionP1(InputAction.CallbackContext obj)
    {
        OnActionP1Action?.Invoke();
    }

    public Vector2 GetMoveValue()
    {

        return controles.MoveOut.Move.ReadValue<Vector2>();

    }

    public float GetCameraRotateValue()
    {

        return controles.MoveOut.RotateCamera.ReadValue<float>();

    }

    public void SwichActionMap(string ActionMap)
    {
        if (ActionMap == this.ActionMaps[0])
            controles.MoveOut.Enable();
        else
            controles.MoveOut.Disable();

        if (ActionMap == this.ActionMaps[1])
            controles.CombatMode.Enable();
        else
            controles.CombatMode.Disable();

        if (ActionMap == this.ActionMaps[2])
            controles.Menu.Enable();
        else
            controles.Menu.Disable();
    }

}
