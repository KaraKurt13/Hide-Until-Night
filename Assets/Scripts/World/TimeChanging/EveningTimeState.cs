using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class EveningTimeState : TimeBaseState
{
    public delegate void NightTime();
    public static event NightTime nightHasBegun;

    private NightTimeState nightTimeState;
    private float timeLeft;


    void Start()
    {
        nightTimeState = GetComponent<NightTimeState>();
        sunLight = GetComponent<Light2D>();
        timeLeft = 120f;
    }

    public override TimeBaseState TimeAction()
    {
        timeLeft -= Time.deltaTime;

        if (timeLeft < 0f)
        {
            nightHasBegun();
            timeLeft = 120f;
            return nightTimeState;
        }

        ChangeSkyToNight();

        return this;
    }

    private void ChangeSkyToNight()
    {
        sunLight.color = Color.Lerp(nightSky, eveningSky, timeLeft / 120f);
    }

}
