using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DayNightChange : MonoBehaviour
{
    private float timeLeftForChange;

    TimeBaseState currentTimeState;


    private void Start()
    {
        currentTimeState = GetComponent<DayTimeState>();
    }

    void Update()
    {
        RunTimeMachine();
    }

    private void RunTimeMachine()
    {
        TimeBaseState nextTimeState = currentTimeState?.TimeAction();

        if (nextTimeState!=null)
        {
            ChangeTime(nextTimeState);
        }
    }

    private void ChangeTime(TimeBaseState nextTimeState)
    {
        currentTimeState = nextTimeState;
    }
}
