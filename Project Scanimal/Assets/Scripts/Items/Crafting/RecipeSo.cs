using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/ItemRecipe")]
public class RecipeSo : ScriptableObject
{
    public ItemSO itemToCraft;

    public List<ItemSO> items;
    public List<int> Amount;
}
