using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCharacter1 : Character
{

    bool dodge = false;

    public bool GetDodge()
    {

        return dodge;
    }

    public void SetDodge(bool dodge)
    {

        this.dodge = dodge;

    }

    public override IEnumerator Attack(Character it)
    {

        EnemyCharacter enemy = it as EnemyCharacter;

        yield return new WaitForSeconds(0f);//cambiar por el tiempo de la animacion

        it.TakeDamage(3);

        print(it.GetHP());

    }

}
