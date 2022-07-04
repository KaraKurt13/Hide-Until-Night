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

    private Coroutine seeking;

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
        if (seekingStarted == false)
        {
            Debug.Log("seek");
            seeking = StartCoroutine(SeekingProcess());

        }

        if (playerIsNear == true)
        {
            StopCoroutine(seeking);
            SetBooleansDefault();
            this.enabled = false;
            chaseState.enabled = true;
            return chaseState;
        }

        if (playerEscaped == true)
        {
            SetBooleansDefault();
            idleState.enabled = true;
            this.enabled = false;
            return idleState;
        }     
        return this;
    }

    private void SetBooleansDefault()
    {
        playerIsNear = false;
        seekingStarted = false;
        playerEscaped = false;
    }

    IEnumerator  SeekingProcess()
    {
        seekingStarted = true;
        yield return new WaitForSeconds(7);
        if (!playerIsNear)
        {
            playerEscaped = true;
        }
        seekingStarted = false;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "PlayerEnemyTrigger" && playerIsNear==false && enabled)
        {
            destination.target = collision.transform;
            playerIsNear = true;
        }
    }

}
