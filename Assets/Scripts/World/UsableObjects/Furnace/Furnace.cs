using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Furnace : MonoBehaviour
{
    public delegate void FurnaceActivation(Furnace furnaceScript);
    public static event FurnaceActivation furnaceActivated;

    public delegate void FurnaceChanged();
    public static event FurnaceChanged furnaceChanged;

    private Coroutine meltingProcess;

    public Item itemIn;
    public int itemInAmount;
    public Item itemOut;
    public int itemOutAmount;
    public Item itemFuel;
    public int itemFuelAmount;

    public bool meltingIsInProgress = false;

    private  const float MELTING_SPEED=5f;
    [SerializeField] private MeltingRecipes meltingRecipes;

    public void AddItemToFurnaceIn(Item item)
    {
        if(itemIn==null)
        {
            itemIn = item;
            itemInAmount = 1;
            StopMelting();
            return;
        }

        itemInAmount++;

        StopMelting();
    }

    public void AddItemToFurnaceFuel(Item item)
    {
        if (itemFuel == null)
        {
            itemFuel = item;
            itemFuelAmount = 1;
            StopMelting();
            return;
        }

        itemFuelAmount++;

        StopMelting();
    }

    public void RemoveItemFromFurnaceIn()
    {
        if (itemIn == null)
        {
            StopMelting();
            return;
        }

        itemInAmount--;
        if(itemInAmount==0)
        {
            itemIn = null;
        }

        StopMelting();

    }

    public void RemoveItemFromFurnaceFuel()
    {
        if (itemFuel == null)
        {
            StopMelting();
            return;

        }

        itemFuelAmount--;
        if(itemFuelAmount==0)
        {
            itemFuel = null;
        }

        StopMelting();

    }

    public void RemoveItemsFromFurnaceOut()
    {
        if (itemOut == null)
        {
            return;
        }

        itemOutAmount=0;
        itemOut = null;
    }

    public void StartMelting()
    {
        if(MeltingIsPossible())
        {
            meltingProcess = StartCoroutine(Melting());
            furnaceChanged();

        }

    }

    public void StopMelting()
    {
        if(meltingProcess==null)
        {
            return;
        }
        StopCoroutine(meltingProcess);
        meltingIsInProgress = false;
        furnaceChanged();
    }

    private bool MeltingIsPossible()
    {
        if(itemIn!=null && itemFuel!=null && !meltingIsInProgress && (itemOut==null || itemOut==meltingRecipes.itemOut[meltingRecipes.itemIn.IndexOf(itemIn)]))
        {
            return true;
        }

        return false;
    }

    public IEnumerator Melting()
    {
        Debug.Log("Start!");
        meltingIsInProgress = true;
        yield return new WaitForSeconds(MELTING_SPEED);
        Debug.Log("MELTING!");
        
        
        meltingIsInProgress = false;

        
        AddMeltedItem();
        if (MeltingIsPossible())
        {
            meltingProcess = StartCoroutine(Melting());
        }

        furnaceChanged();

    }

    public void AddMeltedItem()
    {
        if (itemOut == null)
        {
            itemOut = meltingRecipes.itemOut[meltingRecipes.itemIn.IndexOf(itemIn)];
            itemOutAmount = 1;
            RemoveItemFromFurnaceIn();
            RemoveItemFromFurnaceFuel();
            furnaceChanged();
            return;
        }

        RemoveItemFromFurnaceIn();
        RemoveItemFromFurnaceFuel();
        
        itemOutAmount++;
        furnaceChanged();
    }

    private void OnMouseDown()
    {
        furnaceActivated(this);
    }

}
