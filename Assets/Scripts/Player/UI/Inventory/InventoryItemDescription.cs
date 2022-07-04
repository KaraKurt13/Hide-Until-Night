using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItemDescription : MonoBehaviour
{
    [SerializeField] private GameObject descriptionUI;
    [SerializeField] private Text itemName;
    [SerializeField] private Text itemDescription;
    [SerializeField] private Image itemImage;
    [SerializeField] private Button placingButton;
    [SerializeField] private Button dropButton;

    private Item inventoryItem;

    public delegate void ItemDropping(Item itemToDrop);
    public static event ItemDropping itemDropped;
    
    private void ShowItemDescription(Item item)
    {
        SetInventoryItem(item);
        SetItemDescription();
        descriptionUI.SetActive(true);
    }

    private void HideItemDescription()
    {
        descriptionUI.SetActive(false);
    }

    private void SetInventoryItem(Item newItem)
    {
        inventoryItem = newItem;
    }

    private void SetItemDescription()
    {
        itemName.text = inventoryItem.itemName;
        itemDescription.text = inventoryItem.itemDescription;
        itemImage.sprite = inventoryItem.itemImage;

        SetItemPlacingButton();
        SetItemDropButton();
    }

    private void SetItemPlacingButton()
    {
        if (inventoryItem.isPlaceable)
        {
            placingButton.gameObject.SetActive(true);
            return;
        }

        placingButton.gameObject.SetActive(false);
    }

    private void SetItemDropButton()
    {
        dropButton.onClick.RemoveAllListeners();
        dropButton.onClick.AddListener(delegate { itemDropped(inventoryItem); });
    }

    private void Start()
    {
        InventorySlot.slotClicked += ShowItemDescription;
        InventoryUI.inventoryClosed += HideItemDescription;
    }
}
