using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlacedItemsManager : MonoBehaviour
{
    public List<PlacedItems> placedItems;
    public List<PlacedItems> savedPlacedItems;
    public GameObject itemHolder;

    private void Start()
    {
        placedItems = new List<PlacedItems>();
    }

    public void SaveItems()
    {
        placedItems.Clear();
        foreach (Transform child in itemHolder.transform)
        {
            PlacedItems itemData = new PlacedItems(child.name, child.transform.position.x, child.transform.position.y, child.transform.position.z);
            placedItems.Add(itemData);
        }
    }

    public void LoadItems()
    {
        foreach (PlacedItems itemData in savedPlacedItems)
        {
            GameObject prefab = Resources.Load<GameObject>("Prefabs/" + itemData.prefabName);
            var placed = Instantiate(prefab, new Vector3(itemData.pos[0], itemData.pos[1], itemData.pos[2]),Quaternion.identity);
            placed.transform.parent = itemHolder.transform;
            placed.name = itemData.prefabName;
        }
    }
}
