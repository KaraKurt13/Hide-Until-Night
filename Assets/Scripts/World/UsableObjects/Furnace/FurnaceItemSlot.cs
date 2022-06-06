using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FurnaceItemSlot : MonoBehaviour
{
    [SerializeField] private Image itemImage;
    [SerializeField] private Text itemAmountText;
    [SerializeField] private Button itemAddButton;
    private Item slotItem;
    public Item.ItemUsage slotItemType;
    private int slotItemAmount;

    public delegate void FurnaceItemClicked(Item item, Item.ItemUsage type);
    public static event FurnaceItemClicked addItemToFurnace;

    public delegate void FurnaceItemSlotIsEmpty(Item.ItemUsage type);
    public static event FurnaceItemSlotIsEmpty slotIsEmtpy;


    public void UpdateFurnaceItemSlot(Item item, int itemAmount, Item.ItemUsage itemType)
    {


        slotItem = item;
        slotItemAmount = itemAmount;
        slotItemType = itemType;

        itemImage.sprite = item.itemImage;
        itemAmountText.text = itemAmount.ToString();
        itemAddButton.onClick.AddListener(delegate { AddItemToFurnace();});
    }

    public void AddItemToFurnace()
    {
        addItemToFurnace(slotItem,slotItemType);
        slotItemAmount--;
        if(slotItemAmount==0)
        {
            slotIsEmtpy(slotItemType);
            Destroy(this.gameObject);
            return;
        }

        itemAmountText.text = slotItemAmount.ToString();
    }



}
