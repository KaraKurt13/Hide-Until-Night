using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeGrowing : MonoBehaviour
{
    TreeGrowingState currentGrowingState;
    SpriteRenderer treeSprite;

    private void Start()
    {
        treeSprite = GetComponent<SpriteRenderer>();

        switch (Random.Range(1,4))
        {
            case 1:
                {
                    currentGrowingState = GetComponent<SmallTreeState>();
                    break;
                }
            case 2:
                {
                    currentGrowingState = GetComponent<MediumTreeState>();
                    break;
                }
            case 3:
                {
                    currentGrowingState = GetComponent<BigTreeState>();
                    break;
                }
        }
        SetNewWoodDropAmount(currentGrowingState.newWoodAmount);
        treeSprite.sprite = currentGrowingState.treeSprite;
    }

    void Update()
    {
        RunTreeGrowingMachine();
    }

    private void RunTreeGrowingMachine()
    {
        TreeGrowingState nextGrowingState = currentGrowingState?.TreeGrowing();

        if (nextGrowingState != null)
        {
            ChangeGrowingState(nextGrowingState);
        }
    }

    private void ChangeGrowingState(TreeGrowingState nextGrowingState)
    {
        if(currentGrowingState!=nextGrowingState)
        {
            ChangeTreeSprite(nextGrowingState.treeSprite);
            SetNewWoodDropAmount(nextGrowingState.newWoodAmount);
        }
        currentGrowingState = nextGrowingState;

        
    }

    private void ChangeTreeSprite(Sprite newTreeSprite)
    {
        treeSprite.sprite = newTreeSprite;
    }

    private void SetNewWoodDropAmount(int newWoodDropAmount)
    {
        Item[] treeDrop = GetComponent<CollectibleResource>().itemsDrop;
        int[] treeDropAmount = GetComponent<CollectibleResource>().itemsDropAmount;
        int counter = 0;
        foreach (Item drop in treeDrop)
        {
            if(drop.itemName=="Wood Log")
            {
                treeDropAmount[counter] = newWoodDropAmount;
                break;
            }
            counter++;
        }
    }
}
