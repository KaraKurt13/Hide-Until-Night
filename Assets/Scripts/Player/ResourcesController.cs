using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourcesController : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag=="CollectibleResource")
        {
            collision.GetComponent<CollectibleResource>().enabled = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "CollectibleResource")
        {
            collision.GetComponent<CollectibleResource>().enabled = false;
        }
    }
}
