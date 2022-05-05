using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class PlayerVision : MonoBehaviour
{
    [SerializeField] private float basicPlayerVisionRadius;
    [SerializeField] private float grassPlayerVisionRadius;
    private Light2D playerVision;

    void Start()
    {
        playerVision = GetComponent<Light2D>();
        RestoreBasicPlayerVision();
    }

    private void RestoreBasicPlayerVision()
    {
        playerVision.pointLightOuterRadius = basicPlayerVisionRadius;
    }

    private void ReducePlayerVisionByGrass()
    {
        playerVision.pointLightOuterRadius = grassPlayerVisionRadius;
    }

    private void OnEnable()
    {
        TallGrassHiding.playerHidingInGrass += ReducePlayerVisionByGrass;
        TallGrassHiding.playerNoLongerHidingInGrass += RestoreBasicPlayerVision;
    }
}
