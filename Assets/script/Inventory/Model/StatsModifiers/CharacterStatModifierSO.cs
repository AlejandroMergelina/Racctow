using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterStatModifierSO : ScriptableObject
{
    [SerializeField]
    private Stads stad2Change;
    public Stads Stad2Change { get => stad2Change;}

    public abstract void AffectCharacter(CharacterSO character, int val);
    public abstract void RemubeModifiers(CharacterSO character, int val);
}
