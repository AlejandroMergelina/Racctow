using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class CharacterHealthModifireSO : CharacterStatModifierSO
{
    public override void AffectCharacter(GameObject character, float val)
    {
        
        Character ch = character.GetComponent<Character>();
        if(ch != null)
            ch.HealHP((int)val);
    }
}
