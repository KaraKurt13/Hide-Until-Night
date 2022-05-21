using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NightWatcherEnemyAI : MonoBehaviour
{
    EnemyAIState currentEnemyState;

    private void Start()
    {
        //currentEnemyState = GetComponent<NightWatcherStatueState>();
        currentEnemyState = GetComponent<NightWatcherIdleState>();
    }

    void Update()
    {
        EnemyAIAction();
    }

    private void EnemyAIAction()
    {
        EnemyAIState nextEnemyState = currentEnemyState?.EnemyAction();
        Debug.Log(currentEnemyState + " - " + nextEnemyState);
        if (nextEnemyState != null)
        {
            ChangeGrowingState(nextEnemyState);
        }
    }

    private void ChangeGrowingState(EnemyAIState nextEnemyState)
    {
        currentEnemyState = nextEnemyState;
    }
}
