using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TreeGrowingState : MonoBehaviour
{
    public abstract TreeGrowingState TreeGrowing();
    public int newWoodAmount;
    public Sprite treeSprite;
}
