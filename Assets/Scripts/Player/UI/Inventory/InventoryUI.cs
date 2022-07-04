using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private GameObject inventoryUI;
    [SerializeField] private PlayerInventory playerInventoryInfo;
    [SerializeField] private Transform playerPosition;

    public delegate void InventoryUsage();
    public static event InventoryUsage inventoryOpened;
    public static event InventoryUsage inventoryClosed;

    public InventorySlot[] inventorySlots;
    private bool inventoryIsAvaible;

    public void DropItem(Item item)
    {
        Vector2 dropPosition;

        RaycastHit2D hit = Physics2D.Raycast(playerPosition.transform.position, -playerPosition.transform.up, 0.2f);
        if(hit.collider!=null)
        {
            dropPosition = new Vector2(hit.point.x, hit.point.y+0.02f);
            playerInventoryInfo.DropItem(item, dropPosition);
            return;
        }

        dropPosition = new Vector2(playerPosition.position.x, playerPosition.position.y - 0.2f);

        playerInventoryInfo.DropItem(item,dropPosition);
    }

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
        InventoryItemDescription.itemDropped += DropItem;
    }
    
}
