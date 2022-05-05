using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TallGrassHiding : MonoBehaviour
{
    public delegate void TallGrassAction();
    public static event TallGrassAction playerHidingInGrass;
    public static event TallGrassAction playerNoLongerHidingInGrass;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("PlayerGrassTrigger"))
        {
            playerHidingInGrass();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerGrassTrigger"))
        {
            playerNoLongerHidingInGrass();
        }
    }
}
