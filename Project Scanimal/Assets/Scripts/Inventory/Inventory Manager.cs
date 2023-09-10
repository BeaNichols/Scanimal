using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(-1)]
public class InventoryManager : MonoBehaviour
{
    #region Events
    public delegate void SelectionChanged();
    public static event SelectionChanged OnSelectionChanged;
    #endregion

    public static InventoryManager Instance;

    public ItemSO[] startItem;
    public int MaxStack = 5;
    public InventorySlot[] inventorSlots;
    public GameObject inventoryItemPrefab;

    public List<InventorySave> invItems;
    public List<InventorySave> savedInvItems;

    private int selectedSlot = -1;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    { 
        invItems = new List<InventorySave>();
        savedInvItems = new List<InventorySave>();
        foreach (var item in startItem)
        {
            AddItem(item);
        }
    }

    public void ChangeSelectedSlot(int newValue)
    {
        if (selectedSlot >= 0)
        {
            inventorSlots[selectedSlot].Deselect();
        }

        inventorSlots[newValue].Select();
        selectedSlot = newValue;
        OnSelectionChanged?.Invoke();
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

    private void WipeInventory()
    {
        for (int i = 0; i < inventorSlots.Length; i++)
        {
            InventorySlot slot = inventorSlots[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            if (itemInSlot != null)
            {
                ItemSO item = itemInSlot.item;
                Destroy(itemInSlot.gameObject);
            }
        }
    }

    public void SaveInv()
    {
        for (int i = 0; i < inventorSlots.Length; i++)
        {
            InventorySlot slot = inventorSlots[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            if (itemInSlot != null)
            {
                ItemSO itemobject = itemInSlot.item;
                InventorySave invData = new InventorySave(itemobject.name.ToString(), itemInSlot.count);
                invItems.Add(invData);
            }
        }
    }

    public void LoadInv()
    {
        WipeInventory();
        foreach (InventorySave invItem in savedInvItems)
        {
            if (invItem.ItemAmount > 1)
            {
                for (int i = 0; i < invItem.ItemAmount; i++)
                {
                    ItemSO currentItem = Resources.Load<ItemSO>("ItemSO/" + invItem.itemSoName);
                    AddItem(currentItem);
                }
            }
            else
            {
                ItemSO currentItem = Resources.Load<ItemSO>("ItemSO/" + invItem.itemSoName);
                AddItem(currentItem);
            }
        }
    }
}
