using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Item")]
public class ItemSO : ScriptableObject
{
    public Sprite image;
    public ItemType type;
    public ActionType actionType;
    public bool stackable = true;

    public enum ItemType
    { 
        building,
        Tool,
        item
        
    }

    public enum ActionType
    { 
        Place,
        Break
    }
}
