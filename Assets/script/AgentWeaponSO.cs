using Inventory.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


[CreateAssetMenu(fileName = "New agentWeapont")]
public class AgentWeaponSO : ScriptableObject
{
    [SerializeField]
    private EquippableItemSO weapon;
    [SerializeField]
    private InventorySO inventoryData;
    [SerializeField]
    private List<ItemParameter> parametersToModify, itemCurrentState;

    public event Action<ItemParameter> OnSetWeapon;
    public void SetWeapon(EquippableItemSO weaponItemSO, List<ItemParameter> itemState)
    {

        if(weapon != null)
        {

            inventoryData.AddItem(weapon, 1, itemCurrentState);

        }

        weapon = weaponItemSO;
        itemCurrentState = new List<ItemParameter>(itemState);
        ModifyParameters();

    }

    private void ModifyParameters()
    {

        foreach(ItemParameter parameter in parametersToModify)
        {

            if (itemCurrentState.Contains(parameter))
            {

                int index = itemCurrentState.IndexOf(parameter);
                float newValue = itemCurrentState[index].Values + parameter.Values;
                itemCurrentState[index] = new ItemParameter { _ItemParameter = parameter._ItemParameter, Values = newValue };

            }

        }

    }

}
