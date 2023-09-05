using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using static PlayerStateManager;

public class LootDrops : MonoBehaviour
{
    [SerializeField]
    private int[] dropRate = new int[4];
    [SerializeField]
    private LootTable currentLootTable;

    public TextMeshProUGUI TextHeader;

    private enum Rarity 
    {
        Common,
        Rare,
        Epic,
        Legendary
    }
    private Rarity rarityDropped;

    private void OnEnable()
    {
        BarcodeHandler.OnScanPass += DropItem;
    }

    private void OnDisable()
    {
        BarcodeHandler.OnScanPass -= DropItem;
    }

    private void DropItem()
    {
        rarityDropped = CalculateDropRarity();
        TextHeader.text = rarityDropped.ToString();
        InventoryManager.Instance.AddItem(ItemToDrop());

    }

    private Rarity CalculateDropRarity()
    {
        float RandomDropChance = Random.Range(1, 101);
        if (RandomDropChance <= dropRate[3])
        {
            return Rarity.Legendary;
        }
        else if (RandomDropChance <= dropRate[2])
        {
            return Rarity.Epic;
        }
        else if (RandomDropChance <= dropRate[1])
        {
            return Rarity.Rare;
        }
        else if (RandomDropChance <= dropRate[0])
        {
            return Rarity.Common;
        }
        return Rarity.Common;
    }

    private ItemSO ItemToDrop()
    {
        int DropLength = 0;
        var SelectedLootTable = currentLootTable.CommonItems;
        switch (rarityDropped)
        {
            case Rarity.Common:
                DropLength = currentLootTable.CommonItems.Length;
                SelectedLootTable = currentLootTable.CommonItems;
                break;
            case Rarity.Rare:
                DropLength = currentLootTable.RareItems.Length;
                SelectedLootTable = currentLootTable.RareItems;
                break;
            case Rarity.Epic:
                DropLength = currentLootTable.EpicItems.Length;
                SelectedLootTable = currentLootTable.EpicItems;
                break;
            case Rarity.Legendary:
                DropLength = currentLootTable.LegendaryItems.Length;
                SelectedLootTable = currentLootTable.LegendaryItems;
                break;
        }
        float RandomDropChance = Random.Range(0, DropLength);
        for (int i = 0; i < DropLength; i++)
        {
            if (i == RandomDropChance)
            {
                return SelectedLootTable[i];
            }
            
        }
        return null;
    }



}
