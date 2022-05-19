using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigTreeState : TreeGrowingState
{
    public override TreeGrowingState TreeGrowing()
    {
        GetComponent<TreeGrowing>().enabled = false;
        return this;
    }
}
