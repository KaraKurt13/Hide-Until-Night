using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;

public class DestructionTile : MonoBehaviour
{
    public float progressOfDestruction;
    [SerializeField] private Slider destructionProgressUI;
    [SerializeField] private GameObject destructionStatusUI;

    public delegate void Destruction(DestructionTile destructionTile);
    public static event Destruction destructionIsAvaible;
    public static event Destruction destructionIsUnavaible;


    public Tile tileToDestruct;
    public Tilemap tilemapToDestruct;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateDestructionStatus();
        if(progressOfDestruction<=0)
        {
            DestructTile();
            destructionIsUnavaible(this);
            Destroy(this.gameObject);
        }
    }



    private void UpdateDestructionStatus()
    {
        destructionProgressUI.value = progressOfDestruction;
    }

    private void DestructTile()
    {
        Vector3Int tilePosition = tilemapToDestruct.WorldToCell(transform.position);
        tilemapToDestruct.SetTile(tilePosition, null);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "PlayerBuildingControllerTrigger")
        {
            destructionIsAvaible(this);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "PlayerBuildingControllerTrigger")
        {
            destructionIsUnavaible(this);
        }

    }

    private void OnEnable()
    {
        destructionStatusUI.SetActive(true);
    }

    private void OnDisable()
    {
        destructionStatusUI.SetActive(false);
    }

}
