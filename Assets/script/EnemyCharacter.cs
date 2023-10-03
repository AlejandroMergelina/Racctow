using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyCharacter : Character
{
    [SerializeField]
    private Transform centerOfPunch;
    [SerializeField]
    private float radius;
    [SerializeField]
    private LayerMask enemyMask;

    [SerializeField]
    private Vector3 distanceToEnemy;
    protected Vector3 initialPosition;
    
    protected Vector3 start, end;

    private bool canMove;
    [SerializeField]
    Animator animator;

    public override void Attack(Character it)
    {
        end = it.transform.position - distanceToEnemy;
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
  
        }
        else if (transform.position.x <= end.x && Mathf.Sign(direction.x) == -1)
        {

            canMove = false;
            animator.SetBool("move", canMove);



        }


    }

    void ComfirmAtack()
    {

        Collider[] enemy = Physics.OverlapSphere(centerOfPunch.position, radius, enemyMask);

        foreach (Collider _enemy in enemy)
        {

            _enemy.GetComponent<EnemyCharacter>().TakeDamage(power);
            print(_enemy.GetComponent<EnemyCharacter>().GetHP());
        }

        canMove = true;
        animator.SetBool("move", canMove);
        animator.SetBool("attack", false);

        end = initialPosition;
        start = transform.position;

    }
}