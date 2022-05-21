using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


public class WorldGenerator : MonoBehaviour
{
    [SerializeField] GameObject[] treeObjects;
    [SerializeField] GameObject[] bushObjects;
    [SerializeField] GameObject[] stoneObjects;
    [SerializeField] GameObject[] tallGrassClusterObjects;

    [SerializeField] Tilemap wallsTileMap;
    [SerializeField] Tilemap floorTileMap;

    [SerializeField] List<TileBase> possibleTreeSpawnTiles;
    [SerializeField] List<TileBase> possibleBushSpawnTiles;
    [SerializeField] List<TileBase> possibleGrassSpawnTiles;

    static int MIN_TREES_AMOUNT = 20;
    static int MAX_TREES_AMOUNT = 50;

    static int MIN_STONES_AMOUNT = 15;
    static int MAX_STONES_AMOUNT = 25;

    static int MIN_BUSHES_AMOUNT = 10;
    static int MAX_BUSHES_AMOUNT = 20;

    static int MIN_GRASS_AMOUNT = 4;
    static int MAX_GRASS_AMOUNT = 8;

    static float GENERATION_RADIUS = 2f;

    Vector3 randomPosition;

    void Start()
    {
        SetRandomSeed();
        GenerateTrees();
        GenerateStones();
        GenerateBushes();
        GenerateTallGrassClusters();
    }

    void GenerateTrees()
    {
        int amount = Random.Range(MIN_TREES_AMOUNT, MAX_TREES_AMOUNT);

        for(int i=0;i<amount;i++)
        {

            int randomTreeID = Random.Range(0, treeObjects.Length - 1);

            bool placingIsPossible=false;
            while (!placingIsPossible)
            {
                randomPosition = Random.insideUnitCircle * GENERATION_RADIUS;
                placingIsPossible = TreePlacingIsPossible(randomPosition);
            }

            GameObject treeObject = Instantiate(treeObjects[randomTreeID],randomPosition, Quaternion.identity);
        }
    }

    bool TreePlacingIsPossible(Vector2 treePosition)
    {
        Vector3Int gridPosition = wallsTileMap.WorldToCell(treePosition);
 
        if(wallsTileMap.GetTile(gridPosition)==null&&possibleTreeSpawnTiles.Contains(floorTileMap.GetTile(gridPosition)))
        {
            return true;
        }
        else
        {
            return false;
        }
        
    }

    void GenerateStones()
    {
        int amount = Random.Range(MIN_STONES_AMOUNT,MAX_STONES_AMOUNT);

        for (int i = 0; i < amount; i++)
        {

            

            int randomStoneID = Random.Range(0, stoneObjects.Length - 1);

            bool placingIsPossible = false;
            while (!placingIsPossible)
            {
                randomPosition = Random.insideUnitCircle * 2f;
                placingIsPossible = StonePlacingIsPossible(randomPosition);
            }

            GameObject stoneObject = Instantiate(stoneObjects[randomStoneID], randomPosition, Quaternion.identity);
        }
    }

    bool StonePlacingIsPossible(Vector2 stonePosition)
    {
        Vector3Int gridPosition = wallsTileMap.WorldToCell(stonePosition);
        
        if (wallsTileMap.GetTile(gridPosition) == null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    void GenerateBushes()
    {
        int amount = Random.Range(MIN_BUSHES_AMOUNT,MAX_BUSHES_AMOUNT);

        for (int i = 0; i < amount; i++)
        {

            int randomBushID = Random.Range(0, bushObjects.Length - 1);

            bool placingIsPossible = false;
            while (!placingIsPossible)
            {
                randomPosition = Random.insideUnitCircle * 2f;
                placingIsPossible = BushPlacingIsPossible(randomPosition);
            }

            GameObject bushObject = Instantiate(bushObjects[randomBushID], randomPosition, Quaternion.identity);
        }
    }

    bool BushPlacingIsPossible(Vector2 bushPosition)
    {
        Vector3Int gridPosition = wallsTileMap.WorldToCell(bushPosition);

        if (wallsTileMap.GetTile(gridPosition) == null&&possibleBushSpawnTiles.Contains(floorTileMap.GetTile(gridPosition)))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    void GenerateTallGrassClusters()
    {
        int amount = Random.Range(MIN_GRASS_AMOUNT, MAX_GRASS_AMOUNT);

        for (int i = 0; i < amount; i++)
        {

            int randomGrassID = Random.Range(0, tallGrassClusterObjects.Length - 1);

            bool placingIsPossible = false;
            while (!placingIsPossible)
            {
                randomPosition = Random.insideUnitCircle * 2f;
                placingIsPossible = TallGrassPlacingIsPossible(randomPosition);
            }
            randomPosition.z = -0.2f;

            GameObject grassObject = Instantiate(tallGrassClusterObjects[randomGrassID], randomPosition, Quaternion.identity);
        }
    }

    bool TallGrassPlacingIsPossible(Vector2 grassPosition)
    {
        Vector3Int gridPosition = wallsTileMap.WorldToCell(grassPosition);

        if (wallsTileMap.GetTile(gridPosition) == null && possibleGrassSpawnTiles.Contains(floorTileMap.GetTile(gridPosition)))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    void SetRandomSeed()
    {
        Random.InitState(System.DateTime.Now.Millisecond);
    }
}
