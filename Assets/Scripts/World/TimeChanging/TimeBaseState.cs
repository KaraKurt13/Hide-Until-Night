using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public abstract class TimeBaseState : MonoBehaviour
{
    public abstract TimeBaseState TimeAction();

    [HideInInspector] public Light2D sunLight;
    [HideInInspector] public static Color eveningSky = new Color(0.990566f, 0.6112491f, 0.135502f);
    [HideInInspector] public static Color daySky = new Color(1f,1f,1f);
    [HideInInspector] public static Color nightSky = new Color(0f, 0f, 0f);
    [HideInInspector] public static Color morningSky= new Color(1f, 0.6414298f,0f);
    
}
