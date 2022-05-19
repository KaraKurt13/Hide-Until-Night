using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingUI : MonoBehaviour
{
    [SerializeField] private GameObject buildingUI;
    [SerializeField] private GameObject buildingContainer;
    [SerializeField] private GameObject buildingSlotPrefab;
    [SerializeField] private BuildingTile[] tiles;
    [SerializeField] private PlayerBuilding playerBuilding;
    [SerializeField] private PlayerDestruction playerDestruction;

    [SerializeField] private GameObject requiredItemPrefab;
    [SerializeField] private GameObject requiredItemsContainer;

    private void Start()
    {
        BuildingSlot.NewTileSelected += ShowRequiredItemsForBuilding;
    }
    void Update()
    {
        if(Input.GetButtonDown("Building"))
        {
            switch (buildingUI.activeSelf)
            {
                case true:
                    {
                        CloseBuilding();
                        break;
                    }
                case false:
                    {
                        OpenBuilding();
                        break;
                    }
            }
        }
    }

    private void OpenBuilding()
    {
        buildingUI.SetActive(true);
        playerBuilding.enabled = true;
        playerDestruction.enabled = true;
        UpdateBuildingUI();
    }

    private void CloseBuilding()
    {
        buildingUI.SetActive(false);
        playerBuilding.RemoveVirtualTile();
        playerBuilding.enabled = false;
        playerDestruction.enabled = false;
        ClearBuildingSlots();
        ClearRequiredItemsForBuilding();
        
    }

    private void UpdateBuildingUI()
    {
        foreach(BuildingTile tileContainer in tiles)
        {
            foreach(Sprite tileSprite in tileContainer.tileVariations)
            {
                AddNewBuildingSlot(tileSprite, tileContainer);
            }
        }
    }

    private void AddNewBuildingSlot(Sprite tileSprite,BuildingTile tileInfo)
    {
        GameObject newSlot = Instantiate(buildingSlotPrefab, buildingContainer.transform);
        BuildingSlot buildingSlot = newSlot.GetComponent<BuildingSlot>();
        buildingSlot.tileSprite = tileSprite;
        buildingSlot.tileInfo=tileInfo;
    }

    private void ClearBuildingSlots()
    {
        foreach(Transform child in buildingContainer.transform)
        {
            Destroy(child.gameObject);
        }
    }

    private void ShowRequiredItemsForBuilding(Sprite tileSprite,BuildingTile tileInfo)
    {
        ClearRequiredItemsForBuilding();
        int counter = 0;
        foreach (Item requiredItem in tileInfo.itemsNeeded)
        {
            
            GameObject requiredItemObject = Instantiate(requiredItemPrefab, requiredItemsContainer.transform);
            BuildingRequiredItemUI requiredItemScript = requiredItemObject.GetComponent<BuildingRequiredItemUI>();

            requiredItemScript.requiredItemSprite = requiredItem.itemImage;
            requiredItemScript.requiredItemAmount = tileInfo.itemAmountNeeded[counter];
            counter++;
        }
    }

    private void ClearRequiredItemsForBuilding()
    {
        foreach(Transform oldRequiredItem in requiredItemsContainer.transform)
        {
            Destroy(oldRequiredItem.gameObject);
        }
    }





}
