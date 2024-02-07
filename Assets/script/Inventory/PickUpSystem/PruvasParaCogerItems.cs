using Inventory.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//cambiar a scriptable y hacer array de Objetos
public class PruvasParaCogerItems : MonoBehaviour
{

    [SerializeField]
    private InventorySO inventoryData;

    [SerializeField]
    private ItemSO item1;
    [SerializeField]
    private int quantity1;

    [SerializeField]
    private ItemSO item2;
    [SerializeField]
    private int quantity2;

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {

            inventoryData.AddItem(item1, quantity1);

        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {

            inventoryData.AddItem(item2, quantity2);

        }

    }
}
