using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTrigger : MonoBehaviour
{
    private CircleCollider2D enemyTrigger;
    public float enemyTriggerBasicRadius=0.5f;
    private const float enemyGrassHidingTriggerRadius=0.1f;

    void Start()
    {
        enemyTrigger = GetComponent<CircleCollider2D>();
        enemyTrigger.radius = enemyTriggerBasicRadius;
    }

    private void PlayerHidingInGrassTriggerRadiusChange()
    {
        enemyTrigger.radius = enemyGrassHidingTriggerRadius;
    }

    private void ResetTriggerRadius()
    {
        enemyTrigger.radius = enemyTriggerBasicRadius;
    }

    private void OnEnable()
    {
        TallGrassHiding.playerHidingInGrass += PlayerHidingInGrassTriggerRadiusChange;
        TallGrassHiding.playerNoLongerHidingInGrass += ResetTriggerRadius;
    }

    private void OnDisable()
    {
        TallGrassHiding.playerHidingInGrass -= PlayerHidingInGrassTriggerRadiusChange;
        TallGrassHiding.playerNoLongerHidingInGrass -= ResetTriggerRadius;
    }
}
