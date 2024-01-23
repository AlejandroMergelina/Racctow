using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Inventory.Model
{
    [CreateAssetMenu]
    public class EquippableItemSO : ItemSO, IDestroyableItem, IItemAction
    {
        public string ActionName => "Equip";

        public bool PerformAction(CharacterSO character, List<ItemParameter> itemState = null)
        {
            AgentWeaponSO weaponSystem = character.AgentWeapon;
            if (weaponSystem != null)
            {

                weaponSystem.SetWeapon(this, itemState == null ? DefaultParameterList : itemState);

                foreach (ModifierData data in modifiersDatas)
                {

                    data.StatModifier.AffectCharacter(character, data.Value);

                }

                return true;
            }
            return false;
        }
    }
}