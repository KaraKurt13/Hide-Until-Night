using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Item : ScriptableObject
{
    public string itemName;
    public string itemDescription;
    public Sprite itemImage;
    public ItemType itemType;
    public ItemUsage[] itemUsage;
    public bool isStackable;
    public bool isPlaceable;

    public virtual void UseItem()
    {

    }

    public enum ItemType
    {
        Resource,
        Food,
        Equipment
    }

    public enum ItemUsage
    {
        Melting,
        Fuel,
    }



}
