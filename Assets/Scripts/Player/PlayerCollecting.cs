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
    [SerializeField] List<CollectibleResource> avaibleResources;
    [SerializeField] CollectibleResource resourceToCollect;

    public delegate void CollectingResource(string collectingType);
    public static event CollectingResource collectingStarted;
    public static event CollectingResource collectingEnded;

    private void Start()
    {
        playerAnimationController = GetComponent<PlayerAnimation>();
        playerStats = GetComponent<PlayerStats>();
        avaibleResources = new List<CollectibleResource>();
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
        avaibleResources.Add(resource);
    }

    private void ForbidCollectResource(CollectibleResource resource)
    {
        avaibleResources.Remove(resource);

        if (avaibleResources.Count==0)
        {
            collectingIsAvaible = false;
        }
            
            StopResourceCollection();            
    }

    private void StartResourceCollection()
    {
        collectingIsInProgress = true;
        collectingPosition = transform.position;
        CalculateNearestResoruce();
        SetCollectingSpeed();
        collectingStarted(resourceToCollect.type.ToString());
    }

    private void CalculateNearestResoruce()
    {
        float minDistance=10f;

        foreach(CollectibleResource resource in avaibleResources)
        {
            if(Vector3.Distance(collectingPosition,resource.transform.position)<minDistance)
            {
                resourceToCollect = resource;
                minDistance = Vector3.Distance(collectingPosition, resource.transform.position);
            }
        }
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
