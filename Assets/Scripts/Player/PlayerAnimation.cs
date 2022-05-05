using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator playerAnimator;

    void Start()
    {
        playerAnimator = GetComponent<Animator>();
    }

    public void MovementAnimation(Vector2 moveDirection)
    {
        if (moveDirection.x == 0 && moveDirection.y == 0)
        {
            playerAnimator.SetFloat("RightOrLeft", 0);
            playerAnimator.SetFloat("UpOrDown", 0);
            playerAnimator.SetBool("PlayerNotMoving", true);
            return;
        }
        else
        {
            playerAnimator.SetBool("PlayerNotMoving", false);
        }

        if (moveDirection.y!=0 && moveDirection.y!=0)
        {
            playerAnimator.SetFloat("RightOrLeft", 0);
            playerAnimator.SetFloat("UpOrDown", moveDirection.y);
        }
        else
        {
            playerAnimator.SetFloat("RightOrLeft", moveDirection.x);
            playerAnimator.SetFloat("UpOrDown", moveDirection.y);
        }
        
    }

    private void ActivateInventoryAnimation()
    {
        playerAnimator.SetBool("InventoryIsActivated", true);
    }

    private void DisableInventoryAnimation()
    {
       
        playerAnimator.SetBool("InventoryIsActivated", false);
    }

    private void ActivateCollectingAnimation(string collectingType)
    {

        switch (collectingType)
        {
            case "Mining":
                {
                    playerAnimator.SetInteger("CollectingType", 0);
                    break;
                }
            case "Lumbering":
                {
                    playerAnimator.SetInteger("CollectingType", 1);
                    break;
                }
            case "Gathering":
                {
                    playerAnimator.SetInteger("CollectingType", 2);
                    break;
                }
        }

        playerAnimator.SetBool("CollectingIsInProgress", true);

    }

    private void DisableeCollectingAnimation(string collectingType)
    {
        playerAnimator.SetBool("CollectingIsInProgress", false);
    }

    private void OnEnable()
    {
        InventoryUI.inventoryOpened += ActivateInventoryAnimation;
        InventoryUI.inventoryClosed += DisableInventoryAnimation;
        PlayerCollecting.collectingStarted += ActivateCollectingAnimation;
        PlayerCollecting.collectingEnded += DisableeCollectingAnimation;
    }

}
