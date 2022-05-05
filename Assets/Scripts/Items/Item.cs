using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Item : ScriptableObject
{
    public string itemName;
    public Sprite itemImage;
    public ItemType itemType;
    public bool isStackable;

    public virtual void UseItem()
    {

    }

    public enum ItemType
    {
        Resource,
        Food,
        Equipment
    }


}
