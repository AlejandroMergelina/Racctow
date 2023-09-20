using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCharacter : Character
{
    public event Action OnAttack;
    private bool mayDodge = false;

    private bool mainDodge;

    [SerializeField]
    private float maxDodgeTime;
    private float currentDodgeTime;

    protected override void Start()
    {
        base.Start();
        currentDodgeTime = maxDodgeTime;
    }

    private void Update()
    {
        if (mainDodge)
        {

            currentDodgeTime -= Time.deltaTime;

        }
        if(currentDodgeTime <= 0)
        {
            mainDodge = false;
            currentDodgeTime = maxDodgeTime;

        }
        if(Input.GetKeyDown(KeyCode.Space) && mayDodge)
        {

            mainDodge= true;

        }
        

    }

    public override IEnumerator Attack(Character it)
    {
        MainCharacter1 i = it as MainCharacter1;



        i.SetDodge(mainDodge);

        //OnAttack?.Invoke();
        yield return new WaitForSeconds(3f);//cambiar por el tiempo de la animacion

        mainDodge = true;
        if (i.GetDodge() == false)
            it.TakeDamage(3);

        print(it.GetHP());

        mainDodge = false;

    }


}
