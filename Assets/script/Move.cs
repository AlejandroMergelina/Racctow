using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class Move : MonoBehaviour
{
    [SerializeField]
    private InputManager inputManager;

    [SerializeField]
    private CharacterController controller;
    [SerializeField]
    private Transform cam;

    [SerializeField]
    private float speed;

    [SerializeField]
    private float turnSmoothTime;

    private float turnSmoothVelocity;

    private Vector3 movementDirection;


    private void OnEnable()
    {
        inputManager.OnMoveAction += OnMoveChanged;
    }

    private void OnMoveChanged(Vector2 obj)
    {
        Debug.Log("fsdfds");
        movementDirection = new Vector3(obj.x, 0, obj.y);
    }

    void Update()
    {
        //OnMove(inputManager.GetMoveValue());
        OnMove();

    }

    private void OnMove()
    {

        //Vector3 direction = new Vector3(input.x, 0, input.y);

        if (movementDirection.magnitude >= 0.1f)
        {

            float targetAngle = Mathf.Atan2(movementDirection.x, movementDirection.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0, angle, 0);

            Vector3 moveDir = Quaternion.Euler(0, targetAngle, 0) * Vector3.forward;
            controller.Move(moveDir.normalized * speed * Time.deltaTime);

        }

    }

}
