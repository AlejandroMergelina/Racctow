using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIInventoryPage : MonoBehaviour
{

    [SerializeField]
    private UIInventoryItem itemPrefab;

    [SerializeField]
    private RectTransform contentPanel;

    [SerializeField]
    private UIInventoryDescription itemDescription;

    [SerializeField]
    private MoseFollower moseFollower;

    private List<UIInventoryItem> listOfUIItems = new List<UIInventoryItem>();

    private int currentlyDraggedItemIndex = -1;

    public event Action<int> OnDescriptionRequested, OnItemActionRequested, OnStartDragging;

    public event Action<int, int> OnSapItems;

    private void Awake()
    {

        Hide();
        moseFollower.Toogle(false);
        itemDescription.ResetDescription();

    }

    public void InitializeInventoryUI(int inventorysize)
    {

        for(int i = 0; i < inventorysize; i++)
        {

            UIInventoryItem uiItem = Instantiate(itemPrefab, Vector3.zero, Quaternion.identity);
            uiItem.transform.SetParent(contentPanel);
            listOfUIItems.Add(uiItem);
            uiItem.OnItemClicked += HandleItemSelection;
            uiItem.OnItemBeingDrag += HandleBeginDrag;
            uiItem.OnItemDroppedOn += HandleSwap;
            uiItem.OnItemEndDrag += HandleEndDrag;
            uiItem.OnRightMouseBtnClick += HandleShowItemActions;

        }

    }

    public void UpdateData(int itemIndex,Sprite itemImage, int itemQuantity)
    {

        if(listOfUIItems.Count > itemIndex)
        {

            listOfUIItems[itemIndex].SetData(itemImage, itemQuantity);
        }

    }

    private void HandleShowItemActions(UIInventoryItem InventoryItemUI)
    {

    }

    private void HandleEndDrag(UIInventoryItem InventoryItemUI)
    {

        moseFollower.Toogle(false);

    }

    private void HandleSwap(UIInventoryItem InventoryItemUI)
    {
        int index = listOfUIItems.IndexOf(InventoryItemUI);
        if (index == -1)
        {
            moseFollower.Toogle(false);
            currentlyDraggedItemIndex = -1;
            return;

        }

        //Operador ternario. (if-else en una línea)
        //listOfUIItems[currentlyDraggedItemIndex].SetData(index == 0? image : image2, quantity);
        //listOfUIItems[index].SetData(currentlyDraggedItemIndex == 0? image : image2, quantity);
        moseFollower.Toogle(false);
        currentlyDraggedItemIndex = -1;
    }

    private void HandleBeginDrag(UIInventoryItem InventoryItemUI)
    {
        int index = listOfUIItems.IndexOf(InventoryItemUI);
        if (index == -1)
            return;
        currentlyDraggedItemIndex = index;

        moseFollower.Toogle(true);
        //moseFollower.SetData(index == 0? image : image2, quantity);
        
    }

    private void HandleItemSelection(UIInventoryItem InventoryItemUI)
    {

        //itemDescription.SetDescription(image, titel, description);
        listOfUIItems[0].Selected();

    }

    public void Show()
    {

        gameObject.SetActive(true);
        itemDescription.ResetDescription();

        //listOfUIItems[0].SetData(image, quantity);
        //listOfUIItems[1].SetData(image2, quantity);

    }

    public void Hide()
    {

        gameObject.SetActive(false);

    }

}
