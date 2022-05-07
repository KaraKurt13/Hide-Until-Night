using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerBuilding : MonoBehaviour
{
    [SerializeField] private Grid mainGrid;
    [SerializeField] private Tilemap buildingTileMap;
    [SerializeField] private Tilemap wallTileMap;
    [SerializeField] private Tilemap floorTileMap;

    private Vector3Int previousBuildingPosition;
    private Vector3Int buildingPosition;
    private Tile buildingTile;
    private BuildingTile tileInfo;
    private string tileType;

    private const float MIN_BUILDING_RADUIS = 0.15f;
    private const float MAX_BUILDING_RADIUS = 0.3f;


    void Start()
    {
        BuildingSlot.NewTileSelected += ChangeBuildingTile;
        buildingTile = new Tile();
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0)&&buildingTile.sprite!=null&&BuildingIsPossible())
        {
            PlaceTile();
        }
        previousBuildingPosition = buildingPosition;
        buildingPosition = buildingTileMap.WorldToCell(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        buildingPosition.z = 0;

        if(previousBuildingPosition!=buildingPosition)
        {
            buildingTileMap.SetTile(previousBuildingPosition, null);
        }
        buildingTileMap.SetTile(buildingPosition, buildingTile);
    }

    private bool BuildingIsPossible()
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
    public void SetBuildingTileSprite()
    {
        buildingTile.sprite = tileInfo.tileVariations[0];
    }

    private void ChangeBuildingTile(Sprite tileSprite,string newTileType)
    {
        buildingTile.sprite = tileSprite;
        tileType = newTileType;
    }

    private void PlaceTile()
    {
        switch(tileType)
        {
            case "Walls":
                {
                    wallTileMap.SetTile(buildingPosition, buildingTile);
                    break;
                }
            case "Floor":
                {
                    floorTileMap.SetTile(buildingPosition, buildingTile);
                    break;
                }
           
        }
    }

}
