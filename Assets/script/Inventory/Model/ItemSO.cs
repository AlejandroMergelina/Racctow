using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu]
public class ItemSO : ScriptableObject
{

    [field: SerializeField]
    private bool isStackable;
    public bool IsStackable { get => isStackable; set => isStackable = value; }

    public int ID => GetInstanceID();
    
    [field: SerializeField]
    private int maxStackSize = 1;
    public int MaxStackSize1 { get => maxStackSize; set => maxStackSize = value; }

    [field: SerializeField]
    private string _name;
    public string Name { get => _name; set => _name = value; }
    
    [field: SerializeField]
    [field: TextArea]
    private string description;
    public string Description { get => description; set => description = value; }

    [field: SerializeField]
    private Sprite itemImage;
    public Sprite ItemImage { get => itemImage; set => itemImage = value; }
}
