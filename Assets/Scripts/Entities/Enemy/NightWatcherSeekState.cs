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

    private IEnumerator coroutine;

    private void Start()
    {
        seekingStarted = false;
        playerEscaped = false;
        idleState = GetComponent<NightWatcherIdleState>();
        chaseState = GetComponent<NightWatcherChaseState>();
        coroutine = SeekingProgress();
        playerIsNear = false;
    }
    public override EnemyAIState EnemyAction()
    {
        if (playerIsNear == true)
        {
            StopCoroutine(coroutine);
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


        if (seekingStarted == false)
        {
            Debug.Log("seek");
            StartCoroutine(coroutine);

        }

        
        return this;
    }

    private void SetBooleansDefault()
    {
        playerIsNear = false;
        seekingStarted = false;
        playerEscaped = false;
    }

    IEnumerator  SeekingProgress()
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
