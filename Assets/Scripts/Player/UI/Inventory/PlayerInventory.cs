using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class PlayerInventory : ScriptableObject
{
    public List<Item> items;
    public List<int> itemsAmount;


    [SerializeField] private GameObject worldItemPrefab;
    [SerializeField] private Transform playerPosition;

    public delegate void InventoryChanged();
    public static event InventoryChanged inventoryChanged;
    

    public void AddItem(Item item,int amount)
    {
        int itemID = items.IndexOf(item);

        if (items.Contains(item) && item.isStackable)
        {
            itemsAmount[itemID] += amount;
        }
        else
        {
            items.Add(item);
            itemsAmount.Add(amount);           
        }

        inventoryChanged();
        
    }

    public void RemoveItem(Item item, int amount)
    {
        int itemID = items.LastIndexOf(item);
        if(!item.isStackable || itemsAmount[itemID]-amount==0)
        {
            items.RemoveAt(itemID);
            itemsAmount.RemoveAt(itemID);
        }
        else
        {
            itemsAmount[itemID] -= amount;
        }

        inventoryChanged();

    }

    public void DropItem(Item item, Vector2 dropPosition)
    {

        worldItemPrefab.transform.position = dropPosition;
        worldItemPrefab.GetComponent<WorldItem>().item = item;
        for(int i=0;i<itemsAmount[items.IndexOf(item)];i++)
        {
            Instantiate(worldItemPrefab, dropPosition, Quaternion.identity);
        }
    }
    
    private void OnDisable()
    {
        items.Clear();
        itemsAmount.Clear();
    }


}
