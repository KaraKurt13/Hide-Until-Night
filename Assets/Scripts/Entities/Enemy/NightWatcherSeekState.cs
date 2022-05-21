using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NightWatcherSeekState : EnemyAIState
{
    NightWatcherIdleState idleState;
    NightWatcherChaseState chaseState;

    [SerializeField] bool playerIsNear;
    [SerializeField] bool seekingStarted;
    [SerializeField] bool playerEscaped;
    private void Start()
    {
        seekingStarted = false;
        playerEscaped = false;
        idleState = GetComponent<NightWatcherIdleState>();
        chaseState = GetComponent<NightWatcherChaseState>();
        playerIsNear = false;
    }
    public override EnemyAIState EnemyAction()
    {
        if (playerEscaped == true)
        {
            StopCoroutine(SeekingProgress());
            this.enabled = false;
            seekingStarted = false;
            playerEscaped = false;
            playerIsNear = false;


            idleState.enabled = true;

            return idleState;
        }

        if (seekingStarted == false)
        {
            Debug.Log("seek");
            StartCoroutine(SeekingProgress());

        }

        if (playerIsNear == true)
        {
            StopCoroutine(SeekingProgress());
            playerIsNear = false;
            seekingStarted = false;
            playerEscaped = false;

            this.enabled = false;
            chaseState.enabled = true;

            return chaseState;
        }

        

        

        return this;
    }

    IEnumerator  SeekingProgress()
    {
        seekingStarted = true;
        yield return new WaitForSeconds(7);
        playerEscaped = true;
        seekingStarted = false;
        playerIsNear = false;
        
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "PlayerEnemyTrigger" && playerIsNear==false)
        {
            destination.target = collision.transform;
            playerIsNear = true;
        }
    }

}
