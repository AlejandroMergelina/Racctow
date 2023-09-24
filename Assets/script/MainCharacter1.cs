using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCharacter1 : Character
{
    [SerializeField]
    private Vector3 center;
    [SerializeField]
    float radius;
    [SerializeField]
    LayerMask enemyMask;

    private float cooldDownDodge;

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape) && cooldDownDodge <= 0)
        {

            //esquivar

        }



    }

    public override void Attack(Character it)
    {

        transform.position = it.transform.position - new Vector3(0f,0f,0f);

        Collider[] spawners = Physics.OverlapSphere(center,radius,enemyMask);

        print(it.GetHP());

    }

}
