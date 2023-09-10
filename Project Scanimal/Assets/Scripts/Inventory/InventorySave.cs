using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySave
{
    public string itemSoName;

    public int ItemAmount;

    public InventorySave(string name, int amount)
    {
        itemSoName = name;
        ItemAmount = amount;
    }

}
