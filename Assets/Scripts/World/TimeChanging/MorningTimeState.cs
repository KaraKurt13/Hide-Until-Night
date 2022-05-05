using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class MorningTimeState : TimeBaseState
{
    private DayTimeState dayTimeState;
    private float timeLeft;


    void Start()
    {
        dayTimeState = GetComponent<DayTimeState>();
        sunLight = GetComponent<Light2D>();
        timeLeft = 60f;
    }

    void Update()
    {
        
    }

    public override TimeBaseState TimeAction()
    {
        timeLeft -= Time.deltaTime;

        if(timeLeft<0f)
        {
            timeLeft = 60f;
            return dayTimeState;
        }

        ChangeSkyToDay();

        return this;
    }

    private void ChangeSkyToDay()
    {
        sunLight.color = Color.Lerp(daySky, morningSky, timeLeft / 60f);
    }
}
