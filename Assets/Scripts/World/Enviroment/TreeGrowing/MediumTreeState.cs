using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MediumTreeState : TreeGrowingState
{
    private BigTreeState bigTreeState;
    public float timeToGrow = 300f;

    private void Start()
    {
        bigTreeState = GetComponent<BigTreeState>();
    }
    public override TreeGrowingState TreeGrowing()
    {
        timeToGrow -= Time.deltaTime;

        if (timeToGrow<=0)
        {
            return bigTreeState;
        }

        return this;
    }
}
