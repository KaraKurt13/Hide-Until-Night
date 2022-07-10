using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerBuilding : MonoBehaviour
{
    [SerializeField] private Grid mainGrid;
    [SerializeField] private Tilemap virtualBuildingTileMap;
    [SerializeField] private Tilemap buildedWalls;
    [SerializeField] private Tilemap buildedFloor;
    [SerializeField] private Tilemap wallTileMap;
    [SerializeField] private Tilemap floorTileMap;
    [SerializeField] private PlayerInventory playerInventory;
    [SerializeField] private BuildingTile tileBuildingInfo;
    [SerializeField] private GameObject unbuildedTilePrefab;
    [SerializeField] private GameObject destructionTilePrefab;
    [SerializeField] private PlayerAnimation playerAnimationController;
    [SerializeField] private Transform playerPosition;
    [SerializeField] private float buildingSpeed;

    private bool buildingIsAvaible;
    private bool buildingIsInProgress;
    private bool spriteIsFlipped;

    [SerializeField] private List<UnbuildedTile> avaibleBuildingTiles;
    [SerializeField] private UnbuildedTile tileToBuild;

    private Vector3Int previousBuildingPosition;
    private Vector3Int buildingPosition;
    private Vector3 playerStartingPosition;
    private Tile virtualTile;

    public delegate void Building();
    public static event Building buildingStarted;
    public static event Building buildingEnded;

    private string tileType;

    private const float MIN_BUILDING_RADUIS = 0.15f;
    private const float MAX_BUILDING_RADIUS = 0.3f;

    void Start()
    {

        BuildingSlot.NewTileSelected += ChangeBuildingTile;
        UnbuildedTile.buildingIsAvaible += AllowBuilding;
        UnbuildedTile.buildingIsUnavaible += ForbidBuilding;

        buildingIsAvaible = false;
        buildingIsInProgress = false;
        spriteIsFlipped = false;
        avaibleBuildingTiles = new List<UnbuildedTile>();
        virtualTile = new Tile();
    }

    void Update()
    {
        if (buildingIsAvaible && !buildingIsInProgress && Input.GetButtonDown("BuildingProcess"))
        {
            StartBuilding();
        }

        if (buildingIsInProgress)
        {
            CheckForMoving();
            CheckForBuildingPossibility();
            BuildTile();
        }

        if (virtualTile.sprite != null && tileBuildingInfo.tileName=="DestructionTile")
        {
            SetVirtualDestructionTile();
            SetTilePosition();

            if (Input.GetMouseButtonDown(0)&&PlacingDestructionTileIsPossible())
            {
                    PlaceDestructionTile();
            }

            return;
        }

        SetTilePosition();
        SetVirtualBuildingTile();

        if (Input.GetMouseButtonDown(0) && virtualTile.sprite != null && BuildingDistanceIsInAvaibleRange() && PlayerHaveEnoughResources()&&PlacingTileIsPossible())
        {
                PlaceTile();   
        }
        
        
    }

    private void SetVirtualDestructionTile()
    {
        virtualBuildingTileMap.SetTile(previousBuildingPosition, null);
        virtualBuildingTileMap.SetTile(buildingPosition, virtualTile);
    }
    private void SetVirtualBuildingTile()
    {
        SetTileAvailabilityColor();
        virtualBuildingTileMap.SetTile(previousBuildingPosition, null);
        virtualBuildingTileMap.SetTile(buildingPosition, virtualTile);
    }
    
    private void StartBuilding()
    {
        buildingIsInProgress = true;
        playerStartingPosition = playerPosition.position;
        CalculateNearestBuilding();

        if (playerStartingPosition.x > tileToBuild.gameObject.transform.position.x)
        {
            spriteIsFlipped = true;
            FlipSprite();
        }
        buildingStarted();
    }

    private void FlipSprite()
    {
        Vector3 flipScale = transform.localScale;
        flipScale.x *= -1;
        //playerPosition.localScale = flipScale;
    }

    private void CalculateNearestBuilding()
    {
        float minDistance = 10f;
        foreach (UnbuildedTile tile in avaibleBuildingTiles)
        {
            if (Vector2.Distance(playerStartingPosition, tile.gameObject.transform.position) < minDistance)
            {
                tileToBuild = tile;
                minDistance = Vector2.Distance(playerStartingPosition, tile.transform.position);
            }
        }
        
    }

    private void CheckForMoving()
    {
        if(playerStartingPosition!=playerPosition.position)
        {
            StopBuilding();
        }
    }

    private void CheckForBuildingPossibility()
    {
        if (buildingIsAvaible == false)
        {
            StopBuilding();
        }

    }

    private void BuildTile()
    {
        tileToBuild.progressOfBuilding -= buildingSpeed*Time.deltaTime;
    }
    private void StopBuilding()
    {
        if(spriteIsFlipped)
        {
            spriteIsFlipped = false;
            FlipSprite();
            
        }
        buildingIsInProgress = false;
        buildingEnded();
    }

    

    private void SetTileAvailabilityColor()
    {

        if (BuildingDistanceIsInAvaibleRange() == false || PlayerHaveEnoughResources() == false)
        {
            virtualTile.color = Color.red;
        }
        else
        {
            virtualTile.color = Color.white;
        }

    }
    private void SetTilePosition()
    {
        previousBuildingPosition = buildingPosition;
        buildingPosition = virtualBuildingTileMap.WorldToCell(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        buildingPosition.z = 0;
        
    }

    private bool BuildingDistanceIsInAvaibleRange()
    {
        float buildingRange= Vector2.Distance(transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition));

        if (buildingRange < MAX_BUILDING_RADIUS && buildingRange > MIN_BUILDING_RADUIS)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private bool PlayerHaveEnoughResources()
    {
        int counter = 0;
        if(tileBuildingInfo == null)
        {
            return false;
        }
        foreach(Item neededItem in tileBuildingInfo.itemsNeeded)
        {
            if(!playerInventory.items.Contains(neededItem))
            {
                return false;
            }         
            else
            {
                int itemPos = playerInventory.items.IndexOf(neededItem);
                if (!(playerInventory.itemsAmount[itemPos] >= tileBuildingInfo.itemAmountNeeded[counter]))
                {
                    return false;
                }
            }
            counter++;
        }

        return true;
    }

    private void ChangeBuildingTile(Sprite newTileSprite,BuildingTile tileInfo)
    {
        virtualTile.sprite = newTileSprite;
        tileBuildingInfo = tileInfo;
    }

    private void PlaceTile()
    {
        RemoveRequiredItems();
        GameObject unbuildedTile = Instantiate(unbuildedTilePrefab,virtualBuildingTileMap.GetCellCenterWorld(buildingPosition),Quaternion.identity);
        UnbuildedTile unbuildedScript = unbuildedTile.GetComponent<UnbuildedTile>();
        unbuildedScript.tileSpriteRenderer.sprite = virtualTile.sprite;
        unbuildedScript.tileSpriteRenderer.color = new Color(1f, 1f, 1f, 0.5f);

        switch(tileBuildingInfo.type.ToString())
        {
            case "Walls":
                {
                    unbuildedScript.tilemapToBuild = buildedWalls;
                    break;
                }
            case "Floor":
                {
                    unbuildedScript.tilemapToBuild = buildedFloor;
                    break;
                }
           
        }
    }

    private bool PlacingTileIsPossible()
    {
        switch (tileBuildingInfo.type.ToString())
        {
            case "Walls":
                {
                    if (buildedWalls.GetTile(buildingPosition) != null || wallTileMap.GetTile(buildingPosition) != null)
                    {
                        return false;
                    }

                    break;
                }
            case "Floor":
                {
                    if (buildedFloor.GetTile(buildingPosition) != null || floorTileMap.GetTile(buildingPosition) != null)
                    {
                        return false;
                    }
                    break;
                }
                
        }

        return true;
    }

    private bool PlacingDestructionTileIsPossible()
    {
        switch (tileBuildingInfo.type.ToString())
        {
            case "Walls":
                {
                    if(buildedWalls.GetTile(buildingPosition)==null)
                    {
                        return false;
                        
                    }

                    break;
                }
            case "Floor":
                {
                    if (buildedFloor.GetTile(buildingPosition) == null)
                    {
                        return false;
                    }
                    break;
                } 
        }

        return true;

    }

    private void PlaceDestructionTile()
    {
        GameObject destructionTileObject = Instantiate(destructionTilePrefab, virtualBuildingTileMap.GetCellCenterWorld(buildingPosition), Quaternion.identity);

        switch (tileBuildingInfo.type.ToString())
        {
            case "Walls":
                {
                    destructionTileObject.GetComponent<DestructionTile>().tilemapToDestruct = buildedWalls;
                    break;
                }
            case "Floor":
                {
                    destructionTileObject.GetComponent<DestructionTile>().tilemapToDestruct = buildedFloor;
                    break;
                }

        }

    }

    

    private void RemoveRequiredItems()
    {
        int counter = 0;
        foreach(Item requiredItem in tileBuildingInfo.itemsNeeded)
        {
            playerInventory.RemoveItem(requiredItem, tileBuildingInfo.itemAmountNeeded[counter]);
            counter++;
        }
    }

    public void RemoveVirtualTile()
    {
        virtualBuildingTileMap.ClearAllTiles();
        virtualTile.sprite = null;
        tileBuildingInfo = null;
    }

    private void AllowBuilding(UnbuildedTile newTile)
    {
        buildingIsAvaible = true;
        avaibleBuildingTiles.Add(newTile);
    }

    private void ForbidBuilding(UnbuildedTile oldTile)
    {
        avaibleBuildingTiles.Remove(oldTile);
        if(avaibleBuildingTiles.Count==0)
        {
            buildingIsAvaible = false;
        }
    }

}
