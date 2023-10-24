using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class CharacterHealthModifireSO : CharacterStatModifierSO
{
    public override void AffectCharacter(CharacterSO character, float val)
    {
        
        if(character != null)
            character.HealHP((int)val);
    }
}
