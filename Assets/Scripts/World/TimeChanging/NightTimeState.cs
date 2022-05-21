using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class NightTimeState : TimeBaseState
{
    

    private MorningTimeState morningTimeState;
    private float timeLeft;

    void Start()
    {
        morningTimeState = GetComponent<MorningTimeState>();
        sunLight = GetComponent<Light2D>();
        timeLeft = 300f;
    }

    public override TimeBaseState TimeAction()
    {
        timeLeft -= Time.deltaTime;

        if (timeLeft < 0f)
        {
            timeLeft = 300f;
            return morningTimeState;
        }

        if(timeLeft < 60f)
        {
            ChangeSkyToMorning();
        }

        return this;
    }

    private void ChangeSkyToMorning()
    {
        sunLight.color = Color.Lerp(morningSky, nightSky, timeLeft / 60f);
    }
}
