using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateManager : MonoBehaviour
{
    #region Events
    public delegate void StateChange(PlayerState currentState);
    public static event StateChange OnStateChange;
    #endregion

    public enum PlayerState
    { 
        building,
        breaking,
        standard
    }
    [SerializeField]
    public PlayerState currentState;
    private GameObject inputManager;

    private void OnEnable()
    {
        InventoryManager.OnSelectionChanged += CheckCurrentItem;
        Placing.OnStateChange += CheckCurrentItem;
    }

    private void OnDisable()
    {
        InventoryManager.OnSelectionChanged -= CheckCurrentItem;
        Placing.OnStateChange -= CheckCurrentItem;
    }

    private void Start()
    {
        inputManager = GameObject.Find("InputManager");
        inputManager.SetActive(false);
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
                    inputManager.SetActive(true);
                    break;
                case ItemSO.ItemType.building:
                    currentState = PlayerState.building;
                    inputManager.SetActive(true);
                    break;
                case ItemSO.ItemType.item:
                    currentState = PlayerState.standard;
                    inputManager.SetActive(false);
                    break;
            }
        }
        else 
        {
            currentState = PlayerState.standard;
            inputManager.SetActive(false);
        }

        OnStateChange?.Invoke(currentState);
    }
}
