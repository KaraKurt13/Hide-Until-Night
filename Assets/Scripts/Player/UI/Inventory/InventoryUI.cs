using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private GameObject inventoryUI;
    [SerializeField] private PlayerInventory playerInventoryInfo;

    public delegate void InventoryUsage();
    public static event InventoryUsage inventoryOpened;
    public static event InventoryUsage inventoryClosed;

    public InventorySlot[] inventorySlots;
    private bool inventoryIsAvaible;

    private void Start()
    {
        inventorySlots = this.GetComponentsInChildren<InventorySlot>(true);
        inventoryIsAvaible = true;
        
    }
    void Update()
    {
        if (Input.GetButtonDown("Inventory")&&inventoryIsAvaible)
        {
            switch(inventoryUI.activeSelf)
            {
                case true:
                    {
                        CloseInventory();
                        break;
                    }
                case false:
                    {
                        OpenInventory();
                        break;
                    }
            }
        }
    }

    private void OpenInventory()
    {
        inventoryUI.SetActive(true);
        UpdateInventory();
        inventoryOpened();
    }

    private void CloseInventory()
    {
        inventoryUI.SetActive(false);
        inventoryClosed();
    }

    private void UpdateInventory()
    {
        int counter = 0;
       foreach(InventorySlot slot in inventorySlots)
        {
            if(playerInventoryInfo.items.Count>counter)
            {
                slot.UpdateInventorySlot(playerInventoryInfo.items[counter], playerInventoryInfo.itemsAmount[counter]);
            }
            else
            {
                slot.UpdateInventorySlot(null, 0);
            }
            counter++;
        }
    }

    private void DisableInventoryByCollecting(string type)
    {
        if(inventoryUI.activeSelf==true)
        {
            CloseInventory();
        }
        inventoryIsAvaible = false;

    }

    private void EnableInventoryByCollecting(string type)
    {
        inventoryIsAvaible = true;
    }

    private void OnEnable()
    {
        PlayerInventory.inventoryChanged += UpdateInventory;
        PlayerCollecting.collectingStarted += DisableInventoryByCollecting;
        PlayerCollecting.collectingEnded += EnableInventoryByCollecting;
    }
    
}
