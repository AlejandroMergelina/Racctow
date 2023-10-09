using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class UIInventoryItem : MonoBehaviour
{

    [SerializeField]
    private Image ItemImage;
    [SerializeField]
    private TMP_Text quantityTxt;

    [SerializeField]
    private Image borderImagen;

    public event Action<UIInventoryItem> OnItemClicked, OnItemDroppedOn,OnTtemEndDrag, OnItemEndDrag, OnRightMouseBtnClick;

    private bool emty = true;
    
}
