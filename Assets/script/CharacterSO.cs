using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class CharacterSO : ScriptableObject
{
    [SerializeField]
    private string _name;
    public string Name { get => _name;}

    [SerializeField]
    private int maxHP;
    public int MaxHP { get => maxHP;}

    [SerializeField]
    private int currentHP;
    public int CurrentHP { get => currentHP;set => currentHP = value; }

    [SerializeField]
    private int power;
    public int Power { get => power;set => power = value; }

    [SerializeField]
    private int defense;
    public int Defense { get => defense; set => defense = value;}

    [SerializeField]
    private int speed;
    public int Speed { get => speed; set => speed = value;}
    
    [SerializeField]
    private int iD;
    public int ID { get => iD; set => iD = value; }

    public void HealHP(int val)
    {
        if (maxHP > currentHP + val)
            currentHP += val;
        else if (maxHP <= currentHP + val)
            currentHP = maxHP;

    }

}
