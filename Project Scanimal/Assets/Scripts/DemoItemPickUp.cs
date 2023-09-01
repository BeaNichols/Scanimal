using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoItemPickUp : MonoBehaviour
{
    public InventoryManager inventoryManager;
    public ItemSO[] itemsToPickup;

    public void PickUpItem(int id)
    {
       bool result = inventoryManager.AddItem(itemsToPickup[id]);
        if (result)
        {
            //Debug.Log("Item Added");
        }
        else
        {
            //Debug.Log("Item Not Added");
        }
    }
}
