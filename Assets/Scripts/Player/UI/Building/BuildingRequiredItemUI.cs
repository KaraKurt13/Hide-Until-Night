using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BuildingRequiredItemUI : MonoBehaviour
{
    public Sprite requiredItemSprite;
    public int requiredItemAmount;
    [SerializeField] private Text requiredItemAmountUI;
    [SerializeField] private Image requiredItemImageUI;

    void Start()
    {
        SetRequiredItemInfo();
    }

    private void SetRequiredItemInfo()
    {
        requiredItemAmountUI.text = requiredItemAmount.ToString();
        requiredItemImageUI.sprite = requiredItemSprite;
    }
}
