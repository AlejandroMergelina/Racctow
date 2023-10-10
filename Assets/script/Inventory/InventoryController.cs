using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    [SerializeField]
    private UIInventoryPage inventoryUI;

    private int inventorySize = 10;

    public int GetInventorySize()
    {
        return inventorySize;
    }

    public void SetInventorySize(int inventaorySize)
    {

        this.inventorySize = inventaorySize;

    }

    private void Start()
    {
        inventoryUI.InitializeInventoryUI(inventorySize);
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.I))
        {
            if(inventoryUI.isActiveAndEnabled == false)
            {
                inventoryUI.Show();
            }
            else
            {

                inventoryUI.Hide();

            }
        }
    }
}
