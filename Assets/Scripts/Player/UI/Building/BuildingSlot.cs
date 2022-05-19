using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingSlot : MonoBehaviour
{
    public delegate void TileChoice(Sprite newTileSprite,BuildingTile tileInfo);
    public static event TileChoice NewTileSelected;

    [SerializeField] private Image tileImageObject;
    public Button tileImageButton;
    
    public Sprite tileSprite;
    public BuildingTile tileInfo;

    private void Start()
    {
        UpdateBuildingSlot();
        
    }

    public void UpdateBuildingSlot()
    {
        tileImageObject.sprite = tileSprite;
        tileImageButton.onClick.AddListener(delegate { NewTileSelected(tileSprite,tileInfo); });
    }

}
