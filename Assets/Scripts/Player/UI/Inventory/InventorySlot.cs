
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class InventorySlot : MonoBehaviour
{
    //public Item item;
    //public int itemAmount;

    public delegate void SlotEvent(Item item);
    public static event SlotEvent slotClicked;

    [SerializeField] private Image itemImage;
    [SerializeField] private Text itemAmountText;
    [SerializeField] private Button itemButton;

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

        itemButton.onClick.RemoveAllListeners();
        itemButton.onClick.AddListener(delegate { slotClicked(item); });
    }

    
}
