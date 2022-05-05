using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Transform playerTransform;
    private PlayerAnimation playerAnimationController;
    private Rigidbody2D playerRigidbody;
    
    private Vector2 moveDirection;
    public float moveSpeed=0.4f;
    public bool movementIsAvaible;



    void Start()
    {
        playerAnimationController = GetComponent<PlayerAnimation>();
        playerTransform = GetComponent<Transform>();
        playerRigidbody = GetComponent<Rigidbody2D>();
        movementIsAvaible = true;

        moveSpeed =0.4f;
    }

    void Update()
    {
        GetMovementDirection();
        playerAnimationController.MovementAnimation(moveDirection);
    }

    private void GetMovementDirection()
    {
        moveDirection.x = Input.GetAxisRaw("Horizontal");
        moveDirection.y = Input.GetAxisRaw("Vertical");
    }
    private void FixedUpdate()
    {
        if(movementIsAvaible)
        {
            MovePlayer();
        }
    }

    private void MovePlayer()
    {
        playerRigidbody.MovePosition(playerRigidbody.position + moveDirection * moveSpeed * Time.deltaTime);
    }

    private void ReduceMovementSpeedByGrass()
    {
        moveSpeed = 0.2f;
    }

    private void RestoreBasicMovementSpeed()
    {
        moveSpeed = 0.4f;
    }

    public void EnablePlayerMovement()
    {
        movementIsAvaible = true;
    }

    public void DisablePlayerMovement()
    {
        movementIsAvaible = false;
    }

    private void OnEnable()
    {
        InventoryUI.inventoryOpened += DisablePlayerMovement;
        InventoryUI.inventoryClosed += EnablePlayerMovement;
        TallGrassHiding.playerHidingInGrass += ReduceMovementSpeedByGrass;
        TallGrassHiding.playerNoLongerHidingInGrass += RestoreBasicMovementSpeed;
    }

}
