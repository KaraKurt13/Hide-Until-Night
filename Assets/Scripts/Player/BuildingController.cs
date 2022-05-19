using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingController : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "UnbuildedTile")
        {
            collision.GetComponent<UnbuildedTile>().enabled = true;
        }

        if (collision.tag == "DestructionTile")
        {
            collision.GetComponent<DestructionTile>().enabled = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "UnbuildedTile")
        {
            collision.GetComponent<UnbuildedTile>().enabled = false;
        }

        if (collision.tag == "DestructionTile")
        {
            collision.GetComponent<DestructionTile>().enabled = false;
        }
    }
}
