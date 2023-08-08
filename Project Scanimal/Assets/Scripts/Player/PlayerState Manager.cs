using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateManager : MonoBehaviour
{
    private enum PlayerState
    { 
        building,
        breaking,
        standard
    }
    [SerializeField]
    private PlayerState currentState;

    public delegate void StateChange();
    public static event StateChange OnStateChange;

    private void OnEnable()
    {
        InventoryManager.OnSelectionChanged += CheckCurrentItem;
    }

    private void OnDisable()
    {
        InventoryManager.OnSelectionChanged -= CheckCurrentItem;
    }

    private void Start()
    {
        currentState = PlayerState.breaking;
    }

    private void CheckCurrentItem()
    {
        ItemSO item = InventoryManager.Instance.GetSelectedItem(false);

        if (item != null)
        {
            switch (item.type)
            {
                case ItemSO.ItemType.Tool:
                    currentState = PlayerState.breaking;
                    break;
                case ItemSO.ItemType.building:
                    currentState = PlayerState.building;
                    break;
                case ItemSO.ItemType.item:
                    currentState = PlayerState.standard;
                    break;
            }
        }
        else 
        {
            currentState = PlayerState.standard;
        }
    }
}
