using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDestruction : MonoBehaviour
{
    [SerializeField] private List<DestructionTile> avaibleDestructionTiles;
    [SerializeField] private DestructionTile tileToDestruct;
    [SerializeField] private Transform playerPosition;
    private Vector3Int previousDestuctionPosition;
    private Vector3Int destructionPosition;
    private Vector3 playerStartingPosition;
    private bool destructionIsAvaible;
    private bool destructionIsInProgress;
    private bool spriteIsFlipped;
    public float destructionSpeed;

    public delegate void Destruction();
    public static event Destruction destructionStarted;
    public static event Destruction destructionEnded;

    void Start()
    {
        DestructionTile.destructionIsAvaible += AllowDestruction;
        DestructionTile.destructionIsUnavaible += ForbidDestruction;
        destructionSpeed = 15f;
        destructionIsAvaible = false;
        spriteIsFlipped = false;

    }

    // Update is called once per frame
    void Update()
    {
        if(destructionIsAvaible&&!destructionIsInProgress&&Input.GetButtonDown("DestructionProcess"))
        {
            StartDestruction();
        }

        if(destructionIsInProgress)
        {
            CheckForMoving();
            CheckForDestructionPossibility();
            DestructTile();
        }
    }

    private void StartDestruction()
    {
        destructionIsInProgress = true;
        playerStartingPosition = playerPosition.position;
        CalculateNearestTileDestruction();

        if (playerStartingPosition.x > tileToDestruct.gameObject.transform.position.x)
        {
            spriteIsFlipped = true;
            FlipSprite();
        }

        destructionStarted();
    }

    private void CalculateNearestTileDestruction()
    {
        float minDistance = 10f;
        foreach (DestructionTile tile in avaibleDestructionTiles)
        {
            if (Vector2.Distance(playerStartingPosition, tile.gameObject.transform.position) < minDistance)
            {
                tileToDestruct = tile;
                minDistance = Vector2.Distance(playerStartingPosition, tile.transform.position);
            }
        }
    }

    private void FlipSprite()
    {
        Vector3 flipScale = transform.localScale;
        flipScale.x *= -1;
        playerPosition.localScale = flipScale;
    }

    private void CheckForMoving()
    {
        if(playerStartingPosition!=playerPosition.position)
        {
            StopDestruction();
        }
    }

    private void CheckForDestructionPossibility()
    {
        if(destructionIsAvaible==false)
        {
            StopDestruction();
        }
    }

    private void StopDestruction()
    {
        if(spriteIsFlipped)
        {
            spriteIsFlipped = false;
            FlipSprite();
        }

        destructionIsInProgress = false;
        destructionEnded();
    }

    private void DestructTile()
    {
        tileToDestruct.progressOfDestruction -= destructionSpeed * Time.deltaTime;
    }

    private void AllowDestruction(DestructionTile destructionTile)
    {
        avaibleDestructionTiles.Add(destructionTile);
        destructionIsAvaible = true;
    }

    private void ForbidDestruction(DestructionTile destructionTile)
    {
        avaibleDestructionTiles.Remove(destructionTile);
        if(avaibleDestructionTiles.Count==0)
        {
            destructionIsAvaible = false;
        }
        

    }
}
