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
    bool spriteIsFlipped;
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
        spriteIsFlipped = false;
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
            CheckForCollectingPossibility();
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

    private void CheckForCollectingPossibility()
    {
        if(collectingIsAvaible==false)
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
                        
    }

    private void StartResourceCollection()
    {
        collectingIsInProgress = true;
        collectingPosition = transform.position;
        CalculateNearestResoruce();
        SetCollectingSpeed();

        if(collectingPosition.x>resourceToCollect.gameObject.transform.position.x)
        {
            spriteIsFlipped = true;
            FlipSprite();
        }

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
        if(spriteIsFlipped)
        {
            spriteIsFlipped = false;
            FlipSprite();
        }
        collectingIsInProgress = false;
        collectingEnded(null);
    }

    private void FlipSprite()
    {
            Vector3 flipScale = transform.localScale;
            flipScale.x *= -1;
            transform.localScale = flipScale;
    }


    private void CollectResource()
    {
        resourceToCollect.progressOfCollecting -= collectingSpeed * Time.deltaTime;
    }
}
