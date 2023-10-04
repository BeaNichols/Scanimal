using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftButton : MonoBehaviour
{
    private RecipeSo itemToCraft;

    private void OnEnable()
    {
        RecipeButton.OnSwitchItem += SetCurrentItem;
    }

    private void OnDisable()
    {
        RecipeButton.OnSwitchItem -= SetCurrentItem;
    }

    private void SetCurrentItem(RecipeSo currentItem)
    { 
        itemToCraft = currentItem;
    }

    public void OnClickCraft()
    {
        InventoryManager.Instance.AddItem(itemToCraft.itemToCraft);
    }
}
