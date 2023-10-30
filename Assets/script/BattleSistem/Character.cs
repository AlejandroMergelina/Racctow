using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour
{

    protected bool stoon;

    [SerializeField]
    protected CharacterSO characterData;


    protected virtual void Start()
    {

        characterData.CurrentHP = characterData.MaxHP;

    }

    public void HealHP(int val)
    {
        characterData.HealHP(val);

    }

    public abstract void Attack(Character it);



    public void TakeDamage(int dmg)
    {

        characterData.CurrentHP -= dmg;

    }

    protected abstract void FinishAnimationAtack();
    
    public string GetName()
    {
        return characterData.Name;
    }

    public int GetMaxHP()
    {
        return characterData.MaxHP;
    }



    public int GetHP()
    {

        return characterData.CurrentHP;

    }



    public int GetPower()
    {

        return characterData.Power;

    }
    public void SetPower(int power)
    {
        characterData.Power = power;
    }



    public int GetDefense()
    {

        return characterData.Defense;

    }
    public void SetDefense(int defense)
    {

        characterData.Defense = defense;

    }



    public int GetSpeed()
    {

        return characterData.Speed;

    }
    public void Setspeed(int speed)
    {

        characterData.Speed = speed;

    }

    public int GetID()
    {

        return characterData.ID;

    }
    public void SetID(int iD)
    {
        characterData.ID = iD;

    }

}
