using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NightWatcherStatueState : EnemyAIState
{
    NightWatcherIdleState idleState;
    bool nightHasBegun;

    private void Start()
    {
        nightHasBegun = false;
        //destination.enabled = false;
        EveningTimeState.nightHasBegun += NightHasBegun;
        idleState = GetComponent<NightWatcherIdleState>();
    }
    public override EnemyAIState EnemyAction()
    {
        if(nightHasBegun)
        {
            destination.enabled = true;
            nightHasBegun = false;

            this.enabled = false;
            idleState.enabled=true;
            return idleState;
        }

        return this;
    }

    private void NightHasBegun()
    {
        nightHasBegun=true;
    }
}
