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

    [SerializeField]
    private Sprite image;
    public Sprite Image { get => image; set => image = value; }
    [SerializeField]
    private int quantity;
    public int Quantity { get => quantity; set => quantity = value; }
    [SerializeField]
    private string description;
    [SerializeField]
    private string titel;
    public string Titel { get => titel; set => titel = value; }
    public string Description { get => description; set => description = value; }

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

    private void HandleShowItemActions(UIInventoryItem obj)
    {

    }

    private void HandleEndDrag(UIInventoryItem obj)
    {

        moseFollower.Toogle(false);

    }

    private void HandleSwap(UIInventoryItem obj)
    {

    }

    private void HandleBeginDrag(UIInventoryItem obj)
    {

        moseFollower.Toogle(true);
        moseFollower.SetData(image, quantity);
        
    }

    private void HandleItemSelection(UIInventoryItem obj)
    {

        itemDescription.SetDescription(image, titel, description);
        listOfUIItems[0].Selected();

    }

    public void Show()
    {

        gameObject.SetActive(true);
        itemDescription.ResetDescription();

        listOfUIItems[0].SetData(image, quantity);

    }

    public void Hide()
    {

        gameObject.SetActive(false);

    }

}
