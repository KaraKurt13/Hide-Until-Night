using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollectibleResource : MonoBehaviour
{
    public CollectingType type;
    public float progressOfCollecting;
    [SerializeField] private Item[] itemsDrop;
    [SerializeField] private int[] itemsDropAmount;

    [SerializeField] private Item[] itemsRareDrop;
    [SerializeField] private int[] itemsRareDropAmount;
    [SerializeField] private float[] itemsRareDropChance;

    public delegate void Collecting(CollectibleResource collectibleObject);
    public static event Collecting collectingIsAvaible;
    public static event Collecting collectingIsUnavaible;

    [SerializeField] private GameObject ResourceUI;
    [SerializeField] private Slider collectingStatusUI;
    [SerializeField] private GameObject dropPrefab;

    private void Start()
    {
        collectingStatusUI.maxValue = progressOfCollecting;
    }
    private void Update()
    {
        UpdateCollectingStatus();

        if(progressOfCollecting<=0)
        {
            collectingIsUnavaible(this);
            DropItems();
            Destroy(this.gameObject);
        }
    }

    private void UpdateCollectingStatus()
    {
        collectingStatusUI.value = progressOfCollecting;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag=="PlayerCollectibleResourceTrigger")
        {
            collectingIsAvaible(this);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "PlayerCollectibleResourceTrigger")
        {
            collectingIsUnavaible(this);
        }
    }

    private void OnEnable()
    {
        ResourceUI.SetActive(true);
    }

    private void OnDisable()
    {
        ResourceUI.SetActive(false);
    }

    private void DropItems()
    {
        CalculateDrop();

        if (itemsRareDrop.Length != 0)
        {
            CalculateRareDrop();
        }
    }

    private void CalculateDrop()
    {
        int counter = 0;
        foreach (Item item in itemsDrop)
        {
            GameObject dropItemObject = new GameObject();
            dropItemObject = dropPrefab;
            dropItemObject.transform.position = transform.position;
            dropItemObject.GetComponent<WorldItem>().item = item;
            for (int i = 0; i < itemsDropAmount[counter]; i++)
            {
                Instantiate(dropItemObject, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
            }
            counter++;
        }
    }

    private void CalculateRareDrop()
    {
        int counter = 0;
        foreach(Item rareItem in itemsRareDrop)
        {
            GameObject dropItemObject = new GameObject();
            dropItemObject = dropPrefab;
            dropItemObject.transform.position = transform.position;
            dropItemObject.GetComponent<WorldItem>().item = rareItem;


            for (int i = 0; i < itemsRareDropAmount[counter]; i++)
            {
                if (Random.value < itemsRareDropChance[counter])
                {
                    Instantiate(dropItemObject, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
                }
                
            }
            counter++;

            
        }
    }

    public enum CollectingType
    {
        Mining,
        Lumbering,
        Gathering,
    }
}
