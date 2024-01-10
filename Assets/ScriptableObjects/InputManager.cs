using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

enum actionMaps { moveOut, CombatMode, Menu }

[CreateAssetMenu(menuName = "InputManager")]
public class InputManager : ScriptableObject
{
    Controles controles;

    public event Action OnRotateCameraAction;
    public event Action<Vector2> OnMoveAction;
    public event Action OnInteractAction;


    public event Action OnActionP1Action;
    public event Action OnActionP2Action;

    private string[] actionMaps = { "MoveOut", "CombatMode", "Menu" };

    public string[] ActionMaps { get => actionMaps;}

    private void OnEnable()
    {
        //Constructor.
        controles = new Controles();

        controles.MoveOut.Enable();

        controles.MoveOut.RotateCamera.started += OnRotateCamera;
        controles.MoveOut.Move.performed += OnMoveFer;
        controles.MoveOut.Move.canceled += OnCancelledMove;
        controles.MoveOut.Inreract.started += OnInteract;

        //Suscripciones a eventos del ActionPnº.
        controles.CombatMode.ActionP1.started += OnActionP1;
        controles.CombatMode.ActionP2.started += OnActionP2;


    }

    private void OnInteract(InputAction.CallbackContext obj)
    {
        OnInteractAction?.Invoke();
    }

    private void OnCancelledMove(InputAction.CallbackContext obj)
    {
        OnMoveAction?.Invoke(Vector2.zero);
    }

    private void OnMoveFer(InputAction.CallbackContext obj)
    {
        OnMoveAction?.Invoke(obj.ReadValue<Vector2>());
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

    //public Vector2 GetMoveValue()
    //{
    //    //OnActionP1Action?.Invoke();
    //    //return controles.MoveOut.Move.ReadValue<Vector2>();

    //}

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
