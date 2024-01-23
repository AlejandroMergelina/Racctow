using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class CharacterPowerStadModifierSO : CharacterStatModifierSO
{

    public override void AffectCharacter(CharacterSO character, int val)
    {

        if (character != null)
            character.Power.AddModifier(val);
    }

}
