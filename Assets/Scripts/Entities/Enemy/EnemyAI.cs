using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    EnemyAIState currentEnemyState;

    private void Start()
    {
       
    }

    void Update()
    {
        EnemyAIAction();
    }

    private void EnemyAIAction()
    {
        EnemyAIState nextEnemyState = currentEnemyState?.TreeGrowing();

        if (nextGrowingState != null)
        {
            ChangeGrowingState(nextGrowingState);
        }
    }

    private void ChangeGrowingState(TreeGrowingState nextGrowingState)
    {
        currentEnemyState = nextGrowingState;
    }
}
