using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NightWatcherChaseState : EnemyAIState
{
    [SerializeField] Transform enemyTarget;
    [SerializeField] bool playerIsNoLongerAround;
    [SerializeField] bool playerIsInAttackRadius;

    NightWatcherSeekState seekState;
    NightWatcherAttackState attackState;

    private void Start()
    {
        playerIsNoLongerAround = false;
        playerIsInAttackRadius = false;
        seekState = GetComponent<NightWatcherSeekState>();
        attackState = GetComponent<NightWatcherAttackState>();

    }
    public override EnemyAIState EnemyAction()
    {
        if(playerIsNoLongerAround==true)
        {
            playerIsNoLongerAround = false;
            destination.target = enemyTarget;
            this.enabled = false;
            seekState.enabled = true;   
            return seekState;
        }

        if(playerIsInAttackRadius)
        {
            return attackState;
        }


        return this;
    }

    private Vector2 PlayerLastPosition()
    {
        return destination.target.position;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "PlayerEnemyTrigger" && playerIsNoLongerAround == false && enabled)
        {
            enemyTarget.position = PlayerLastPosition();
            playerIsNoLongerAround = true;
        }
    }


}
