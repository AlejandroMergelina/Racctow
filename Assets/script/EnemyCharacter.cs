using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCharacter : Character
{
    public override void Attack(Character it)
    {



        it.TakeDamage(3);
        print(it.GetHP());

    }
}
