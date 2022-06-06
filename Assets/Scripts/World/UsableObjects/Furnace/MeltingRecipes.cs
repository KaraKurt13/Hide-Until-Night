using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class MeltingRecipes : ScriptableObject
{
    public List<Item> itemIn;
    public List<Item> itemOut;
}
