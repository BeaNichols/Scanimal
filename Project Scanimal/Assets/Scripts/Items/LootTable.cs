using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/LootTable")]
public class LootTable : ScriptableObject
{
    public ItemSO[] CommonItems;
    public ItemSO[] RareItems;
    public ItemSO[] EpicItems;
    public ItemSO[] LegendaryItems;
}
