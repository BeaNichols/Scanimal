using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateManager : MonoBehaviour
{
    public enum PlayerState
    { 
        building,
        breaking,
        standard
    }
    [SerializeField]
    public PlayerState currentState;

    public delegate void StateChange(PlayerState currentState);
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
        currentState = PlayerState.standard;
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

        OnStateChange?.Invoke(currentState);
    }
}
