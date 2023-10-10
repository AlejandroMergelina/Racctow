using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.EventSystems;

public class UIInventoryItem : MonoBehaviour
{

    [SerializeField]
    private Image ItemImage;
    [SerializeField]
    private TMP_Text quantityTxt;

    [SerializeField]
    private Image borderImagen;

    public event Action<UIInventoryItem> OnItemClicked, OnItemDroppedOn,OnItemBeingDrag, OnItemEndDrag, OnRightMouseBtnClick;

    private bool emty = true;

    public void Awake()
    {

        ResetData();
        Deselect();

    }
        
    public void ResetData()
    {

        ItemImage.gameObject.SetActive(false);

    }

    public void Deselect()
    {

        borderImagen.enabled = false;

    }

    public void SetData(Sprite sprite, int quantity)
    {

        ItemImage.gameObject.SetActive(true);
        ItemImage.sprite = sprite;
        quantityTxt.text = quantity + "";
        emty = false;

    }

    public void Selected()
    {

        borderImagen.enabled = true;

    }

    public void OnBegingDrag()
    {

        if (emty)
            return;

        OnItemBeingDrag?.Invoke(this);

    }

    public void OnDrop()
    {

        OnItemDroppedOn?.Invoke(this);

    }

    public void OnEndDrag()
    {

        OnItemEndDrag?.Invoke(this);

    }

    public void OnPointerClick(BaseEventData data)
    {
        if (emty)
            return;

        PointerEventData pointerData = (PointerEventData)data;
        if(pointerData.button == PointerEventData.InputButton.Right)
        {

            OnRightMouseBtnClick?.Invoke(this);

        }
        else
        {

            OnItemClicked?.Invoke(this);

        }

    }

}
