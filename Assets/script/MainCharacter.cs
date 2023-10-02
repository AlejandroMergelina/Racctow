using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCharacter : Character
{
    [SerializeField]
    private Vector3 distanceToEnemy;
    protected Vector3 initialPosition;
    private Vector3 targetPosition;
    protected Vector3 start, end;

    private float cooldDownDodge;
    protected bool canAttack, canMove;
    //private bool goToEnemy;

    [SerializeField]
    protected Animator animator;

    protected override void Start()
    {
        base.Start();
        initialPosition = transform.position;
    }

    protected virtual void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && cooldDownDodge <= 0/* y puede esquivar*/)
        {

            //esquivar

        }

        if (canMove)
        {

            Move(start, end);

        }
    }

    public override void Attack(Character it)
    {
        print("hola");
        targetPosition = end = it.transform.position - distanceToEnemy;
        start = initialPosition;

        canMove = true;
        animator.SetBool("move", canMove);

    }

    void Move(Vector3 start, Vector3 end)
    {

        Vector3 direction = end - start;

        transform.position += direction.normalized * Time.deltaTime;

        if (transform.position.x >= end.x && Mathf.Sign(direction.x) == 1)
        {

            canMove = false;
            animator.SetBool("move", canMove);
            canAttack = true;
            /*if (transform.position.x >= targetPosition.x)
            {

                

            }*/

        }
        else if (transform.position.x <= end.x && Mathf.Sign(direction.x) == -1)
        {

            canMove = false;
            animator.SetBool("move", canMove);



        }


    }


}
