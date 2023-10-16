using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Inventory.Model
{
    [CreateAssetMenu]
    public class EdibleItemSO : ItemSO, IDestroyableItem, IItemAction
    {

        [SerializeField]
        private List<ModifierData> modifiersDatas = new List<ModifierData>();

        public string ActionName => "Consume";

        public bool PerformAction(GameObject character, List<ItemParameter> itemState = null)
        {
            foreach(ModifierData data in modifiersDatas)
            {

                data.StatModifier.AffectCharacter(character, data.Value);

            }
            return true;
        }
    }

    public interface IDestroyableItem
    {



    }

    public interface IItemAction
    {
        public string ActionName { get;}

        bool PerformAction(GameObject character, List<ItemParameter> itemState);
    }

    [Serializable]
    public class ModifierData
    {
        [SerializeField]
        private CharacterStatModifierSO statModifier;
        public CharacterStatModifierSO StatModifier { get => statModifier; set => statModifier = value; }
        [SerializeField]
        private float value;
        public float Value { get => value; set => this.value = value; }
    }

}