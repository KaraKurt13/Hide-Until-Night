using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class NightWatcherIdleState : EnemyAIState
{
    private const float WALKING_RADIUS = 0.5f;
    private const float WALKING_SPEED = 0.25f;
    private const float CHASING_SPEED = 0.5f;
    private Vector2 randomWalkingPosition;
    [SerializeField] private bool enemyIsWalking;
    [SerializeField] private bool playerIsInSight;
    private Transform playerTransform;
    private NightWatcherChaseState chaseState;

    private void Start()
    {
        playerIsInSight = false;
        enemyIsWalking = false;
        chaseState = GetComponent<NightWatcherChaseState>();
    }
    public override EnemyAIState EnemyAction()
    {

        if(playerIsInSight == true)
        {
            StopCoroutine(Walking());
            enemyIsWalking = false;
            playerIsInSight = false;
            destination.target = playerTransform;

            this.enabled = false;
            chaseState.enabled = true;
            return chaseState;
        }

        if (enemyIsWalking == false)
        {
            GetRandomPositionForWalking();
            StartCoroutine(Walking());
        }

        return this;
    }

    private void GetRandomPositionForWalking()
    {
        randomWalkingPosition = Random.insideUnitCircle * WALKING_RADIUS;

       /*bool pointCanBeReached = false;
        while (pointCanBeReached == false)
        {
            randomWalkingPosition = Random.insideUnitCircle * WALKING_RADIUS;
            if (PathUtilities.IsPathPossible(AstarPath.active.GetNearest(transform.position).node, AstarPath.active.GetNearest(randomWalkingPosition).node))
            {
                pointCanBeReached = true;
            }
        }*/

        
    }

    IEnumerator Walking()
    {
        enemyIsWalking = true;
        destination.target.localPosition = randomWalkingPosition;
        yield return new WaitForSeconds(Random.Range(5f,10f));
        enemyIsWalking = false;
       
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "PlayerEnemyTrigger")
        {
            playerTransform = collision.transform;
            Debug.Log("!!!!!!!");
            playerIsInSight = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "PlayerEnemyTrigger")
        {
            playerTransform = collision.transform;
            playerIsInSight = false;
        }
    }
}
