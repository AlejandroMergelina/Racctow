using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCharacter1 : Character
{
    [SerializeField]
    private Vector3 centerOfPunch;
    [SerializeField]
    float radius;
    [SerializeField]
    private LayerMask enemyMask;
    [SerializeField]
    private Vector3 distanceToEnemy;

    private Vector3 initialPosition;

    private float cooldDownDodge;

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

        else if (Input.GetKeyDown(KeyCode.Escape) /* y puede atacar*/)
        {

            ComfirmAtack();

        }

    }

    public override void Attack(Character it)
    {

        transform.position = it.transform.position - distanceToEnemy;

        print(it.GetHP());

        transform.position = initialPosition;

    }

    void ComfirmAtack()
    {

        Collider[] enemy = Physics.OverlapSphere(centerOfPunch, radius, enemyMask);

        enemy[1].GetComponent<EnemyCharacter>().TakeDamage(power);

    }

}
