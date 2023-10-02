using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCharacter1 : MainCharacter
{
    [SerializeField]
    private Transform centerOfPunch;
    [SerializeField]
    private float radius;
    [SerializeField]
    private LayerMask enemyMask;
            
    protected override void Update()
    {
        base.Update();

        if (Input.GetKeyDown(KeyCode.E) && canAttack)
        {
            print("entro");
            animator.SetBool("attack", true);
            canAttack = false;
            
        }
        
    }

    void ComfirmAtack()
    {

        Collider[] enemy = Physics.OverlapSphere(centerOfPunch.position, radius, enemyMask);
        
        foreach(Collider _enemy in enemy)
        {
            
            _enemy.GetComponent<EnemyCharacter>().TakeDamage(power);
            print(_enemy.GetComponent<EnemyCharacter>().GetHP());
        }

        canMove = true;
        animator.SetBool("move", canMove);
        animator.SetBool("attack", canAttack);

        end = initialPosition;
        start = transform.position;

    }
    
}
