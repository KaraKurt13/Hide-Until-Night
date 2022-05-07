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

    private void Start()
    {

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
        UpdateBuildingUI();
    }

    private void CloseBuilding()
    {
        buildingUI.SetActive(false);
        playerBuilding.enabled = false;
        ClearBuildingSlots();
    }

    private void UpdateBuildingUI()
    {
        foreach(BuildingTile tileContainer in tiles)
        {
            foreach(Sprite tileSprite in tileContainer.tileVariations)
            {
                AddNewBuildingSlot(tileSprite, tileContainer.type.ToString());
            }
        }
    }

    private void AddNewBuildingSlot(Sprite tileSprite,string tileType)
    {
        GameObject newSlot = Instantiate(buildingSlotPrefab, buildingContainer.transform);
        newSlot.GetComponent<BuildingSlot>().tileSprite = tileSprite;
        newSlot.GetComponent<BuildingSlot>().tileType = tileType;
    }

    private void ClearBuildingSlots()
    {
        foreach(Transform child in buildingContainer.transform)
        {
            Destroy(child.gameObject);
        }
    }



   
}
