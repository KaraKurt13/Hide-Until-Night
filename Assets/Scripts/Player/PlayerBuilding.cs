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
    [SerializeField] private Transform playerTransform;

    private Vector3Int previousBuildingPosition;
    private Vector3Int buildingPosition;
    private Tile buildingTile;
    private BuildingTile tileInfo;
    private string tileType;
    private bool buildingIsAvaible;

    private const float MIN_BUILDING_RADUIS=1f;


    void Start()
    {
        BuildingSlot.NewTileSelected += ChangeBuildingTile;
        buildingIsAvaible = true;
        buildingTile = new Tile();
    }

    void Update()
    {
        Debug.Log(Vector3.Distance(playerTransform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition)));

        if(Input.GetMouseButtonDown(0)&&buildingTile.sprite!=null&&buildingIsAvaible)
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

    public void SetBuildingTileSprite()
    {
        buildingTile.sprite = tileInfo.tileVariations[0];
    }

    private void ChangeBuildingTile(Sprite tileSprite,string newTileType)
    {
        Debug.Log(tileSprite);
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
