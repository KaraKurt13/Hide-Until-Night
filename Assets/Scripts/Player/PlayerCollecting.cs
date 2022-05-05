using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollecting : MonoBehaviour
{
    private PlayerAnimation playerAnimationController;
    private PlayerStats playerStats;
    private Vector3 collectingPosition;
    bool collectingIsAvaible;
    bool collectingIsInProgress;
    float collectingSpeed;
    [SerializeField] CollectibleResource resourceToCollect;

    public delegate void CollectingResource(string collectingType);
    public static event CollectingResource collectingStarted;
    public static event CollectingResource collectingEnded;

    private void Start()
    {
        playerAnimationController = GetComponent<PlayerAnimation>();
        playerStats = GetComponent<PlayerStats>();
        CollectibleResource.collectingIsAvaible += AllowCollectResource;
        CollectibleResource.collectingIsUnavaible += ForbidCollectResource;
        collectingIsAvaible = false;
    }

    private void Update()
    {
        if(collectingIsAvaible && !collectingIsInProgress && Input.GetButtonDown("Interaction"))
        {
            StartResourceCollection();
        }

        if(collectingIsInProgress)
        {
            CheckForMoving();
            CollectResource();
        }
    }

    private void CheckForMoving()
    {
        if(transform.position!=collectingPosition)
        {
            StopResourceCollection();
        }
    }

    private void AllowCollectResource(CollectibleResource resource)
    {
        collectingIsAvaible = true;
        resourceToCollect = resource;
    }

    private void ForbidCollectResource(CollectibleResource resource)
    {
            collectingIsAvaible = false;
            StopResourceCollection();
            resourceToCollect = null;
        
    }

    private void StartResourceCollection()
    {
        collectingIsInProgress = true;
        collectingPosition = transform.position;
        SetCollectingSpeed();
        collectingStarted(resourceToCollect.type.ToString());
    }

    private void SetCollectingSpeed()
    {
        switch(resourceToCollect.type.ToString())
        {
            case "Mining":
                {
                    collectingSpeed = playerStats.miningSpeed;
                    break;
                }
            case "Lumbering":
                {
                    collectingSpeed = playerStats.lumberingSpeed;
                    break;
                }
            case "Gathering":
                {
                    collectingSpeed = playerStats.gatheringSpeed;
                    break;
                }
        }
    }

    private void StopResourceCollection()
    {
        collectingIsInProgress = false;
        collectingEnded(null);
    }

    private void CollectResource()
    {
        resourceToCollect.progressOfCollecting -= collectingSpeed * Time.deltaTime;
    }
}
