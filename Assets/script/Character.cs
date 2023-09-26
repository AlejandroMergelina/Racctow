using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
    [SerializeField]
    protected string _name;

    [SerializeField]
    protected int maxHP;
    [SerializeField]
    protected int currentHP;
    [SerializeField]
    protected int power;
    [SerializeField]
    protected int defense;
    [SerializeField]
    protected int speed;

    protected bool stoon;


    protected virtual void Start()
    {

        currentHP = maxHP;

    }

    public string GetName()
    {

        return _name;

    }
    public void SetName(string name)
    {

        this._name = name;

    }

    public int GetMaxHP()
    {

        return maxHP;

    }
    public void SetMaxHP(int maxHP)
    {

        this.maxHP = maxHP;

    }

    public int GetHP()
    {

        return currentHP;

    }
    public void SetHP(int currentHP)
    {

        this.currentHP = currentHP;

    }

    public int GetPower()
    {

        return power;

    }
    public void SetPower(int power)
    {

        this.power = power;

    }

    public int GetDefense()
    {

        return defense;

    }
    public void SetDefense(int defense)
    {

        this.defense = defense;

    }

    public int GetSpeed()
    {

        return speed;

    }
    public void Setspeed(int speed)
    {

        this.speed = speed;

    }

    public abstract void Attack(Character it);

    public void TakeDamage(int dmg)
    {

        currentHP -= dmg;

    }

}
