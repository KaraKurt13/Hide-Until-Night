using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallTreeState : TreeGrowingState
{
    public float timeToGrow = 300f;
    private MediumTreeState mediumTreeState;

    private void Start()
    {
        mediumTreeState = GetComponent<MediumTreeState>();
    }
    public override TreeGrowingState TreeGrowing()
    {
        timeToGrow -= Time.deltaTime;

        if (timeToGrow <= 0)
        {
            return mediumTreeState;
        }

        return this;

    }
}
