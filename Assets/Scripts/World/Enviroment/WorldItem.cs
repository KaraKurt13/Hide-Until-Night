using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldItem : MonoBehaviour
{
    public Item item;
    private bool playerIsNear;

    private void Start()
    {
        GetComponent<SpriteRenderer>().sprite = item.itemImage;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.tag == "Player")
        {
            List<Item> playerItems = collision.GetComponent<PlayerStats>().inventory.items;

            if ((playerItems.Contains(item)&&item.isStackable)||playerItems.Count<12)
            {
                collision.GetComponent<PlayerStats>().inventory.AddItem(item, 1);
                Destroy(this.gameObject);
            }
        }
    }

}
