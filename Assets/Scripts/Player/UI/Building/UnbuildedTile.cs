using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;
public class UnbuildedTile : MonoBehaviour
{
    public SpriteRenderer tileSpriteRenderer;
    public float progressOfBuilding;
    public Tilemap tilemapToBuild;
    private Tile tileToBuild;
    
    public delegate void Building(UnbuildedTile unbuildedTile);
    public static event Building buildingIsAvaible;
    public static event Building buildingIsUnavaible;


    [SerializeField] private Slider buildingProgressUI;
    [SerializeField] private GameObject buildingStatusUI;

    private void Start()
    {
        
    }


    private void Update()
    {
        UpdateBuildingStatus();

        if (progressOfBuilding <= 0)
        {
            PlaceCompletedTile();
            buildingIsUnavaible(this);
            Destroy(this.gameObject);
            
        }
    }

    private void PlaceCompletedTile()
    {
        tileToBuild = new Tile();
        tileToBuild.sprite = tileSpriteRenderer.sprite;
        tilemapToBuild.SetTile(tilemapToBuild.WorldToCell(transform.position), tileToBuild);
    }

    private void UpdateBuildingStatus()
    {
        buildingProgressUI.value = progressOfBuilding;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "PlayerBuildingControllerTrigger")
        {
            buildingIsAvaible(this);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "PlayerBuildingControllerTrigger")
        {
            buildingIsUnavaible(this);
        }

    }

    private void OnEnable()
    {
        buildingStatusUI.SetActive(true);
    }

    private void OnDisable()
    {
        buildingStatusUI.SetActive(false);
    }
}
