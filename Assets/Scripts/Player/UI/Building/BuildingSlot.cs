using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingSlot : MonoBehaviour
{
    public delegate void TileChoice(Sprite newTileSprite, string newTileType);
    public static event TileChoice NewTileSelected;

    [SerializeField] private Image tileImageObject;
    public Button tileImageButton;
    
    public Sprite tileSprite;
    public string tileType;

    private void Start()
    {
        UpdateBuildingSlot();
        
    }

    public void UpdateBuildingSlot()
    {
        tileImageObject.sprite = tileSprite;
        tileImageButton.onClick.AddListener(delegate { NewTileSelected(tileSprite, tileType); });
    }

}
