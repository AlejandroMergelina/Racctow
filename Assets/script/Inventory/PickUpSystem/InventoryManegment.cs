using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Inventory.Model
{
    [CreateAssetMenu]
    public class InventoryManegment : ScriptableObject
    {
        [SerializeField]
        InventorySO inventory;

        [SerializeField]
        ItemSO[] items;
    

        public void PickUpItem(string name, int quantity)
        {

            foreach (ItemSO item in items)
            {

                if(item.Name == name)
                {

                    inventory.AddItem(item, quantity);

                }
                else
                {

                    Debug.Log("no existe");

                }

            }

        }

    }
}