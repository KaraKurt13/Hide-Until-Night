using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Resource Item",menuName ="Items/Resourse")]

public class ResourceItem : Item
{
    private void Awake()
    {
        itemType = ItemType.Resource;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
