using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;

    public ItemSO[] startItem;
    public int MaxStack = 5;
    public InventorySlot[] inventorSlots;
    public GameObject inventoryItemPrefab;

    private int selectedSlot = -1;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        ChangeSelectedSlot(0);
        foreach (var item in startItem)
        { 
            AddItem(item);
        }
    }

    private void Update()
    {
        //implement changing selected slot 
    }

    void ChangeSelectedSlot(int newValue)
    {
        if (selectedSlot >= 0)
        {
            inventorSlots[selectedSlot].Deselect();
        }

        inventorSlots[newValue].Select();
        selectedSlot = newValue;
    }

    public bool AddItem(ItemSO item)
    {
        //check if item can be stacked 
        for (int i = 0; i < inventorSlots.Length; i++)
        {
            InventorySlot slot = inventorSlots[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            if (itemInSlot != null && itemInSlot.item == item && itemInSlot.count < MaxStack && itemInSlot.item.stackable)
            {
                itemInSlot.count++;
                itemInSlot.RefreshCount();
                return true;
            }
        }
        //check got empty slot
        for (int i = 0; i < inventorSlots.Length; i++)
        {
            InventorySlot slot = inventorSlots[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            if (itemInSlot == null)
            {
                SpawnNewItem(item, slot);
                return true;
            }
        }
        return false;
    }

    public void SpawnNewItem(ItemSO item, InventorySlot slot)
    { 
        GameObject newItemObject = Instantiate(inventoryItemPrefab, slot.transform);
        InventoryItem inventoryItem  =  newItemObject.GetComponentInChildren<InventoryItem>();
        inventoryItem.InitiliseItem(item);
    }

    //ToDo change Tools to not Break
    public ItemSO GetSelectedItem(bool use)
    {
        InventorySlot slot = inventorSlots[selectedSlot];
        InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
        if (itemInSlot != null)
        { 
            ItemSO item = itemInSlot.item;
            if(use)
            {
                itemInSlot.count--;
                if (itemInSlot.count <= 0)
                {
                    Destroy(itemInSlot.gameObject);
                }
                else 
                {
                    itemInSlot.RefreshCount();
                }
            }
            return item;
        }
        return null;
    }
}
