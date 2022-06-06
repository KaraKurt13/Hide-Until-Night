using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FurnaceUI : MonoBehaviour
{
    [SerializeField] GameObject furnaceUI;
    [SerializeField] GameObject furnaceItemInUI;
    [SerializeField] GameObject furnaceItemOutUI;
    [SerializeField] GameObject furnaceItemFuelUI;
    [SerializeField] Text furnaceItemInAmount;
    [SerializeField] Text furnaceItemOutAmount;
    [SerializeField] Text furnaceItemFuelAmount;

    [SerializeField] Button furnaceItemInButton;
    [SerializeField] Button furnaceItemOutButton;
    [SerializeField] Button furnaceItemFuelButton;
    [SerializeField] Button furnaceMeltingButton;
    [SerializeField] Text furnaceMeltingButtonText;

    [SerializeField] GameObject furnaceInventory;
    [SerializeField] GameObject furnaceItemSlotsContainer;
    [SerializeField] GameObject furnaceItemSlotPrefab;

    [SerializeField] PlayerInventory inventoryInfo;

    private Furnace furnaceInfo;

    private void Start()
    {
        Furnace.furnaceActivated += ActivateFurnaceUI;
        Furnace.furnaceChanged += SetFurnaceInformation;
        FurnaceItemSlot.addItemToFurnace += AddItemToFurnaceUI;
        FurnaceItemSlot.slotIsEmtpy += UpdateFurnaceInventoryUI;
        
    }


    private void ActivateFurnaceUI(Furnace furnace)
    {
        furnaceInfo = furnace;
        SetFurnaceInformation();
        furnaceUI.SetActive(true);
        
    }

    private void DisableFurnaceUI()
    {
        furnaceUI.SetActive(false);
        furnaceInventory.SetActive(false);
        ChangeFurnaceUIButtonsToDefault();
    }

    private void SetFurnaceInformation()
    {
        SetItemIn();
        SetItemOut();
        SetItemFuel();
        SetFurnaceMeltingButton();
    }

    private void SetItemIn()
    {
        if(furnaceInfo.itemIn==null)
        {
            furnaceItemInUI.GetComponent<Image>().sprite = null;
            furnaceItemInAmount.text = null;
            return;
        }

        furnaceItemInUI.GetComponent<Image>().sprite = furnaceInfo.itemIn.itemImage;
        furnaceItemInAmount.text = furnaceInfo.itemInAmount.ToString();
    }
    
    private void SetItemOut()
    {
        if (furnaceInfo.itemOut == null)
        {
            furnaceItemOutUI.GetComponent<Image>().sprite = null;
            furnaceItemOutAmount.text = null;
            return;
        }

        furnaceItemOutUI.GetComponent<Image>().sprite = furnaceInfo.itemOut.itemImage;
        furnaceItemOutAmount.text = furnaceInfo.itemOutAmount.ToString();

    }

    private void SetItemFuel()
    {
        if (furnaceInfo.itemFuel == null)
        {
            furnaceItemFuelUI.GetComponent<Image>().sprite = null;
            furnaceItemFuelAmount.text = null;
            return;
        }
            
        furnaceItemFuelUI.GetComponent<Image>().sprite = furnaceInfo.itemFuel.itemImage;
        furnaceItemFuelAmount.text = furnaceInfo.itemFuelAmount.ToString();
    }

    private void SetFurnaceMeltingButton()
    {
        if(furnaceInfo.meltingIsInProgress)
        {
            furnaceMeltingButton.onClick.RemoveAllListeners();
            furnaceMeltingButton.onClick.AddListener(delegate { furnaceInfo.StopMelting(); });
            furnaceMeltingButtonText.text = "Stop Melting";
            return;
        }

        furnaceMeltingButton.onClick.RemoveAllListeners();
        furnaceMeltingButton.onClick.AddListener(delegate { furnaceInfo.StartMelting(); });
        furnaceMeltingButtonText.text = "Start Melting";
    }

    public void ShowFurnaceMeltableItems()
    {
        ChangeFurnaceUIButtonToItemRemoval(Item.ItemUsage.Melting);

        ClearFurnaceInventoryContainer();

        furnaceInventory.SetActive(true);

        int counter = 0;
        foreach(Item item in inventoryInfo.items)
        {
            foreach(Item.ItemUsage itemUsage in item.itemUsage)
            {
                if(itemUsage==Item.ItemUsage.Melting)
                {
                    GameObject furnaceItemSlot = Instantiate(furnaceItemSlotPrefab, furnaceItemSlotsContainer.transform);
                    furnaceItemSlot.GetComponent<FurnaceItemSlot>().UpdateFurnaceItemSlot(item, inventoryInfo.itemsAmount[counter], Item.ItemUsage.Melting);
                    break;
                }
            }
            counter++;
        }
    }

    private void RemoveMeltableItemFromFurnace()
    {
        if(furnaceInfo.itemIn==null)
        {
            return;
        }

        inventoryInfo.AddItem(furnaceInfo.itemIn, 1);

        furnaceInfo.RemoveItemFromFurnaceIn();

        UpdateFurnaceInventoryUI(Item.ItemUsage.Melting);
        SetFurnaceInformation();
    }

    private void RemoveFuelItemFromFurnace()
    {
        if (furnaceInfo.itemFuel == null)
        {
            return;
        }

        inventoryInfo.AddItem(furnaceInfo.itemFuel, 1);

        furnaceInfo.RemoveItemFromFurnaceFuel();

        UpdateFurnaceInventoryUI(Item.ItemUsage.Fuel);
        SetFurnaceInformation();

    }

    public void RemoveOutItemFromFurnace()
    {
        if (furnaceInfo.itemOut == null)
        {
            return;
        }

        inventoryInfo.AddItem(furnaceInfo.itemOut, furnaceInfo.itemOutAmount);

        furnaceInfo.RemoveItemsFromFurnaceOut();
        SetFurnaceInformation();

        //UPDATE BOTH INVENTORIES

    }
    public void ShowFurnaceFuelItems()
    {
        ChangeFurnaceUIButtonToItemRemoval(Item.ItemUsage.Fuel);

        ClearFurnaceInventoryContainer();

        furnaceInventory.SetActive(true);

        int counter = 0;
        foreach (Item item in inventoryInfo.items)
        {
            foreach (Item.ItemUsage itemUsage in item.itemUsage)
            {
                if (itemUsage == Item.ItemUsage.Fuel)
                {
                    GameObject furnaceItemSlot = Instantiate(furnaceItemSlotPrefab, furnaceItemSlotsContainer.transform);
                    furnaceItemSlot.GetComponent<FurnaceItemSlot>().UpdateFurnaceItemSlot(item, inventoryInfo.itemsAmount[counter], Item.ItemUsage.Fuel);
                    break;
                }
            }
            counter++;
        }

    }

    private void ClearFurnaceInventoryContainer()
    {
        foreach(Transform child in furnaceItemSlotsContainer.transform)
        {
            Destroy(child.gameObject);
        }
    }

    private void UpdateFurnaceInventoryUI(Item.ItemUsage inventoryType)
    {
        switch (inventoryType)
        {
            case Item.ItemUsage.Fuel:
                {
                    ShowFurnaceFuelItems();
                    break;
                }

            case Item.ItemUsage.Melting:
                {
                    ShowFurnaceMeltableItems();
                    break;
                }
        }

    }
    private void AddItemToFurnaceUI(Item item, Item.ItemUsage type)
    {

        switch (type)
        {
            case Item.ItemUsage.Fuel:
                {
                    if(furnaceInfo.itemFuel!=item&&furnaceInfo.itemFuel!=null)
                    {
                        Debug.Log(furnaceInfo.itemFuelAmount);
                        inventoryInfo.AddItem(furnaceInfo.itemFuel, furnaceInfo.itemFuelAmount);
                        furnaceInfo.itemFuelAmount = 0;
                        furnaceInfo.itemFuel = null;
                        
                    }
                    
                    furnaceInfo.AddItemToFurnaceFuel(item);
                    break;
                }

            case Item.ItemUsage.Melting:
                {
                    if (furnaceInfo.itemIn != item&&furnaceInfo.itemIn!=null)
                    {
                        inventoryInfo.AddItem(furnaceInfo.itemIn, furnaceInfo.itemInAmount);
                        furnaceInfo.itemInAmount = 0;
                        furnaceInfo.itemIn = null;
                    }

                    furnaceInfo.AddItemToFurnaceIn(item);
                    break;
                }
        }

        inventoryInfo.RemoveItem(item, 1);
        UpdateFurnaceInventoryUI(type);
        SetFurnaceInformation();

    }

    private void RemoveItemFromFurnaceUI(Item item, Item.ItemUsage type)
    {

    }


    private void Update()
    {
        if (furnaceUI.activeSelf == true)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                DisableFurnaceUI();
            }
        }
        
    }

    private void ChangeFurnaceUIButtonToItemRemoval(Item.ItemUsage furnaceButtonType)
    {
        switch (furnaceButtonType)
        {
            case Item.ItemUsage.Melting:
                {
                    furnaceItemInButton.onClick.RemoveAllListeners();
                    furnaceItemInButton.onClick.AddListener(delegate { RemoveMeltableItemFromFurnace(); });
                    furnaceItemFuelButton.onClick.RemoveAllListeners();
                    furnaceItemFuelButton.onClick.AddListener(delegate { ShowFurnaceFuelItems(); });
                    break;
                }
            case Item.ItemUsage.Fuel:
                {
                    furnaceItemInButton.onClick.RemoveAllListeners();
                    furnaceItemInButton.onClick.AddListener(delegate { ShowFurnaceMeltableItems(); });
                    furnaceItemFuelButton.onClick.RemoveAllListeners();
                    furnaceItemFuelButton.onClick.AddListener(delegate { RemoveFuelItemFromFurnace(); });
                    break;
                }
        }

    }

    private void ChangeFurnaceUIButtonsToDefault()
    {
        furnaceItemInButton.onClick.RemoveAllListeners();
        furnaceItemInButton.onClick.AddListener(delegate { ShowFurnaceMeltableItems(); });
        furnaceItemFuelButton.onClick.RemoveAllListeners();
        furnaceItemFuelButton.onClick.AddListener(delegate { ShowFurnaceFuelItems(); });

    }

}
