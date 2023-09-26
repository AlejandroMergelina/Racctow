using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCharacter1 : Character
{
    [SerializeField]
    private Vector3 centerOfPunch;
    [SerializeField]
    private float radius;
    [SerializeField]
    private LayerMask enemyMask;
    [SerializeField]
    private Vector3 distanceToEnemy;

    [SerializeField]
    private float atackTime;
    private float currentatAckTime;

    private Vector3 initialPosition;

    private float cooldDownDodge;
    private bool canAtack;

    protected override void Start()
    {
        base.Start();
        initialPosition = transform.position;
    }

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape) && cooldDownDodge <= 0/* y puede esquivar*/)
        {

            //esquivar

        }
        else if (Input.GetKeyDown(KeyCode.Escape) && canAtack)
        {

            ComfirmAtack();

        }

        if (canAtack)
        {

            currentatAckTime += Time.deltaTime;

            if(currentatAckTime >= atackTime)
            {

                canAtack = false;
                currentatAckTime = 0;

            }

        }

    }

    public override void Attack(Character it)
    {

        transform.position = it.transform.position - distanceToEnemy;

        canAtack= true;

        print(it.GetHP());

        transform.position = initialPosition;

    }

    void ComfirmAtack()
    {

        canAtack = false;

        Collider[] enemy = Physics.OverlapSphere(centerOfPunch, radius, enemyMask);

        enemy[1].GetComponent<EnemyCharacter>().TakeDamage(power);

    }

}
