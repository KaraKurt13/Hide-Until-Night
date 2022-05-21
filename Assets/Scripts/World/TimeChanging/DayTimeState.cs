using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class DayTimeState : TimeBaseState
{
    public delegate void EveningTime();
    public static event EveningTime eveningHasBegun;

    private EveningTimeState eveningTimeState;
    private float timeLeft;
    
    void Start()
    {
        eveningTimeState = GetComponent<EveningTimeState>();
        sunLight = GetComponent<Light2D>();
        timeLeft = 240f;
    }


    public override TimeBaseState TimeAction()
    {
        timeLeft -= Time.deltaTime;

        if (timeLeft < 0f)
        {
            eveningHasBegun();
            timeLeft = 240f;
            return eveningTimeState;
        }

        if (timeLeft < 60f)
        {
            ChangeSkyToEvening();
        }

        return this;
    }

    private void ChangeSkyToEvening()
    {
        sunLight.color = Color.Lerp(eveningSky, daySky, timeLeft / 60f);
    }

}
