using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class InventorySlot : MonoBehaviour
{
    //public Item item;
    //public int itemAmount;

    [SerializeField] private Image itemImage;
    [SerializeField] private Text itemAmountText;

    public void UpdateInventorySlot(Item item,int itemAmount)
    {
        if (item==null)
        {
            itemImage.enabled = false;
            itemAmountText.enabled = false;
            return;
        }

        itemImage.enabled = true;
        itemAmountText.enabled = true;

        itemImage.sprite = item.itemImage;
        itemAmountText.text = itemAmount.ToString();
    }
}
