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
    private Stad power;
    public Stad Power { get => power;set => power = value; }

    [SerializeField]
    private Stad defense;
    public Stad Defense { get => defense; set => defense = value;}

    [SerializeField]
    private Stad speed;
    public Stad Speed { get => speed; set => speed = value;}
    
    [SerializeField]
    private int iD;
    public int ID { get => iD; set => iD = value; }

    [SerializeField]
    private AgentWeaponSO agentWeapon;
    public AgentWeaponSO AgentWeapon { get => agentWeapon; set => agentWeapon = value; }

    public void HealHP(int val)
    {
        if (maxHP > currentHP + val)
            currentHP += val;
        else if (maxHP <= currentHP + val)
            currentHP = maxHP;

    }

}
