using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEnemyTrigger : MonoBehaviour
{
    [SerializeField] private float basicTriggerRadius;
    [SerializeField] private float grassTriggerRadius;
    private CircleCollider2D playerEnemyTrigger; 

    private void Start()
    {
        playerEnemyTrigger = GetComponent<CircleCollider2D>();
        RestoreBasicEnemyTrigger();
    }

    private void RestoreBasicEnemyTrigger()
    {
        playerEnemyTrigger.radius = basicTriggerRadius;
    }

    private void ReduceEnemeyTriggerByGrass()
    {
        playerEnemyTrigger.radius = grassTriggerRadius;
    }

    private void OnEnable()
    {
        TallGrassHiding.playerHidingInGrass += ReduceEnemeyTriggerByGrass;
        TallGrassHiding.playerNoLongerHidingInGrass += RestoreBasicEnemyTrigger;
    }

}
