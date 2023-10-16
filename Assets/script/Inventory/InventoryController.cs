using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Inventory.UI;
using Inventory.Model;
using System.Text;
using System;

namespace Inventory
{
    public class InventoryController : MonoBehaviour
    {
        [SerializeField]
        private UIInventoryPage inventoryUI;

        [SerializeField]
        private InventorySO inventoryData;

        [SerializeField]
        private List<InventoryItem> initialItems = new List<InventoryItem>();

        private void Start()
        {
            PrepareUI();
            PrepareInventoryData();
        }

        private void PrepareInventoryData()
        {
            inventoryData.Initialize();
            inventoryData.OnInventoryUpdated += UpdateIventoryUI;
            foreach (InventoryItem item in initialItems)
            {

                if (item.IsEmpty)
                    continue;
                inventoryData.AddItem(item);

            }
        }

        private void UpdateIventoryUI(Dictionary<int, InventoryItem> inventoryState)
        {

            inventoryUI.ResetAllItems();
            foreach (KeyValuePair<int, InventoryItem> item in inventoryState)
            {

                inventoryUI.UpdateData(item.Key, item.Value.Item.ItemImage, item.Value.Quantity);

            }
            
        }

        private void PrepareUI()
        {
            inventoryUI.InitializeInventoryUI(inventoryData.Size);
            inventoryUI.OnDescriptionRequested += HandleDescriptionRequest;
            inventoryUI.OnSwapItems += HandleSawmpItem;
            inventoryUI.OnStartDragging += HandleDragging;
            inventoryUI.OnItemActionRequested += HandleItemActionRequest;
        }

        private void HandleItemActionRequest(int itemIndex)
        {
            
            InventoryItem inventoryItem = inventoryData.GetItemAt(itemIndex);
            if(inventoryItem.IsEmpty)
                return;

            IItemAction itemAction = inventoryItem.Item as IItemAction;
            if(itemAction != null)
            {

                
                inventoryUI.ShowItemAction(itemIndex);
                inventoryUI.AddAction(itemAction.ActionName, () => PerformAction(itemIndex));

            }

            IDestroyableItem destroyableItem = inventoryItem.Item as IDestroyableItem;
            if (destroyableItem != null)
            {

                inventoryUI.AddAction("Drop", () => DropItem(itemIndex, inventoryItem.Quantity));

            }

        }

        private void DropItem(int itemIndex, int quantity)
        {
            inventoryData.RemoveItem(itemIndex, quantity);
            inventoryUI.ResetSelection();
        }

        public void PerformAction(int itemIndex)
        {


            InventoryItem inventoryItem = inventoryData.GetItemAt(itemIndex);
            if (inventoryItem.IsEmpty)
                return;

            IDestroyableItem destroyableItem = inventoryItem.Item as IDestroyableItem;
            if (destroyableItem != null)
            {

                inventoryData.RemoveItem(itemIndex, 1);

            }

            IItemAction itemAction = inventoryItem.Item as IItemAction;
            if (itemAction != null)
            {

                itemAction.PerformAction(gameObject/*aqui ira el jugador seleccionado*/, inventoryItem.ItemState);
                if (inventoryData.GetItemAt(itemIndex).IsEmpty)
                    inventoryUI.ResetSelection();

            }


        }

        private void HandleDragging(int itemIndex)
        {

            InventoryItem inventoryItem = inventoryData.GetItemAt(itemIndex);
            if (inventoryItem.IsEmpty)
                return;
            inventoryUI.CreateDraggedItem(inventoryItem.Item.ItemImage, inventoryItem.Quantity);

        }

        private void HandleSawmpItem(int itemIndex_1, int itemIndex_2)
        {

            inventoryData.SwapItems(itemIndex_1, itemIndex_2);

        }

        private void HandleDescriptionRequest(int itemIndex)
        {

            InventoryItem inventoryItem = inventoryData.GetItemAt(itemIndex);
            if (inventoryItem.IsEmpty)
            {

                inventoryUI.ResetSelection();
                return;

            }

            ItemSO item = inventoryItem.Item;
            string description = PrepareDescription(inventoryItem);
            inventoryUI.UpdateDescription(itemIndex, item.ItemImage, item.Name, description);

        }

        private string PrepareDescription(InventoryItem inventoryItem)
        {

            StringBuilder sb = new StringBuilder();
            sb.Append(inventoryItem.Item.Description);
            sb.AppendLine();
            for(int i = 0; i < inventoryItem.ItemState.Count; i++)
            {

                sb.Append($"{inventoryItem.ItemState[i]._ItemParameter.ParameterName}" + $":{inventoryItem.ItemState[i].Values} / {inventoryItem.Item.DefaultParameterList[i].Values}");
                sb.AppendLine();
            }
            return sb.ToString();

        }

        // Implementar en el nuevo sisitema de input de unity solo para cuando no este en combate
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.I))
            {
                if (inventoryUI.isActiveAndEnabled == false)
                {
                    inventoryUI.Show();
                    foreach (KeyValuePair<int, InventoryItem> item in inventoryData.GetCurrentIventoryState())
                    {

                        inventoryUI.UpdateData(item.Key, item.Value.Item.ItemImage, item.Value.Quantity);

                    }
                }
                else
                {

                    inventoryUI.Hide();

                }
            }
        }

        public void OppeInventoty()
        {

            inventoryUI.Show();
            foreach (KeyValuePair<int, InventoryItem> item in inventoryData.GetCurrentIventoryState())
            {

                inventoryUI.UpdateData(item.Key, item.Value.Item.ItemImage, item.Value.Quantity);

            }

        }

    }
}