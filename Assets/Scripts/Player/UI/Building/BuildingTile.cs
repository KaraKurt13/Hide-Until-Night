using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName ="New Building Tile",menuName ="Building/Build")]
public class BuildingTile : ScriptableObject
{
    public string tileName;
    public Sprite[] tileVariations;
    public tileBuildingType type;
    
    public enum tileBuildingType
    {
        Walls,
        Floor,
    }

}
