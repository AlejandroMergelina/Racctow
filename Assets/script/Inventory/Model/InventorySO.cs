using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class InventorySO : ScriptableObject
{

    [SerializeField]
    private List<InventoryItem> inventoryItems;

    [SerializeField]
    private int size = 10;
    public int Size { get => size; set => size = value; }

    public void Initialize()
    {
        inventoryItems = new List<InventoryItem>();
        for (int i = 0; i < size; i++)
        {
            inventoryItems.Add(InventoryItem.GetEmptyItem());
        }
    }

    public void AddItem(ItemSO item, int quantity)
    {

        for (int i = 0; i < inventoryItems.Count; i++)
        {
            if (inventoryItems[i].IsEmty)
            {

                inventoryItems[i] = new InventoryItem
                {

                    Item = item,
                    Quantity = quantity

                };

            }
        }
        
    }
    public Dictionary<int, InventoryItem> GetCurrentIventoryState()
    {
        Dictionary<int, InventoryItem> returnValue = new Dictionary<int, InventoryItem>();
        for (int i = 0; i < inventoryItems.Count; i++)
        {

            if (inventoryItems[i].IsEmty)
                continue;
            returnValue[i] = inventoryItems[i];
        }
        return returnValue;
    }

}
[SerializeField]
public struct InventoryItem
{

    private int quantity;
    public int Quantity { get => quantity; set => quantity = value; }
    private ItemSO item;
    public ItemSO Item { get => item; set => item = value; }

    private bool isEmty => item == null;
    public bool IsEmty { get => isEmty;}

    public InventoryItem ChangeQuantity(int newQuuantity)
    {
        return new InventoryItem
        {
            item = this.item,
            quantity = newQuuantity

        };

    }

    public static InventoryItem GetEmptyItem() => new InventoryItem()
    {

        item = null,
        quantity = 0,

    };

}