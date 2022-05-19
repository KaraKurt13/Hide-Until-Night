using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SproutTreeState : TreeGrowingState
{
    public float timeToGrow=300f;
    private SmallTreeState smallTreeState;

    private void Start()
    {
        smallTreeState = GetComponent<SmallTreeState>();
    }
    public override TreeGrowingState TreeGrowing()
    {
        timeToGrow -= Time.deltaTime;

        if (timeToGrow <= 0)
        {
            return smallTreeState;
        }

        return this;
    }
}
