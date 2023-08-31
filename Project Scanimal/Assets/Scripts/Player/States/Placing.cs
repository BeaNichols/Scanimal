using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Placing : MonoBehaviour
{
    private ItemSO Helditem;
    private GameObject objectToPlace;
    private GameObject tempObject;
    [SerializeField]
    private GameObject Player;
    private bool stateEnabled;
    private bool spawnTempObject;

    private void OnEnable()
    {
        PlayerStateManager.OnStateChange += EnableDisableState;
    }

    private void OnDisable()
    {
        PlayerStateManager.OnStateChange -= EnableDisableState;
    }

    private void Start()
    {
        stateEnabled = false;
        objectToPlace = null;
        spawnTempObject = false;
    }

    private void Update()
    {
        if (stateEnabled)
        {
            PlaceMode();
        }
    }

    private void EnableDisableState(PlayerStateManager.PlayerState currentState)
    {
        if (currentState == PlayerStateManager.PlayerState.building)
        {
            stateEnabled = true;
            Helditem = InventoryManager.Instance.GetSelectedItem(false);
            objectToPlace = Helditem.PlaceableItem;
            
        }
        else
        {
            stateEnabled = false;
            spawnTempObject = false;
        }
    }

    private void PlaceMode()
    {
        if (spawnTempObject == false)
        {
            tempObject = Instantiate(objectToPlace, new Vector3(transform.position.x, 0, transform.position.z), Quaternion.identity);
            spawnTempObject = true;
        }
        else if (spawnTempObject == true)
        {
            tempObject.transform.position = new Vector3(Player.transform.position.x, 0, Player.transform.position.z);
        }


    }

    
}
