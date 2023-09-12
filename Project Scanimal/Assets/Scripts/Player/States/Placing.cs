using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using static PlayerStateManager;

public class Placing : MonoBehaviour
{
    #region Events
    public delegate void StateChange();
    public static event StateChange OnStateChange;
    #endregion

    private ItemSO Helditem;
    private GameObject objectToPlace;
    private GameObject tempObject;
    [SerializeField]
    private GameObject Player;
    private bool stateEnabled;
    private bool spawnTempObject;
    private Renderer[] renderers;
    private PlacedItemsManager placedItemsManager;
    public GameObject TerrainHolder;

    private void OnEnable()
    {
        PlayerStateManager.OnStateChange += EnableDisableState;
        SwpieDetection.OnTap += StartTap;
    }

    private void OnDisable()
    {
        PlayerStateManager.OnStateChange -= EnableDisableState;
        SwpieDetection.OnTap -= StartTap;
    }

    private void Start()
    {
        stateEnabled = false;
        objectToPlace = null;
        spawnTempObject = false;
        placedItemsManager = GameObject.Find("Terrain Generator").GetComponent<PlacedItemsManager>();
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
            Destroy(tempObject);
            stateEnabled = false;
            spawnTempObject = false;
        }
    }

    private void PlaceMode()
    {
        if (spawnTempObject == false)
        {
            spawnTempObject = true;
            tempObject = Instantiate(objectToPlace, new Vector3(transform.position.x, 0, transform.position.z), Quaternion.identity);
            GetMaterials();
        }
        else if (spawnTempObject == true)
        {
            tempObject.transform.position = new Vector3(Player.transform.position.x, 0, Player.transform.position.z);
            if (CanPlace())
            {
                TurnGreen();
            }
            else
            { 
                TurnRed();
            }
        }
    }

    private bool CanPlace()
    {
        var worldController = tempObject.GetComponent<WorldOverlayController>();
        if (worldController.colliding == true)
        {
            return false;
        }
        else if (worldController.colliding == false)
        {
            return true;
        }
        return false;
    }

    private void GetMaterials()
    {
        // Get all the materials attached to the object
        renderers = tempObject.GetComponentsInChildren<Renderer>();
    }

    private void TurnRed()
    {
        foreach (Renderer renderer in renderers)
        {
            foreach (Material material in renderer.materials)
            {
                // Change the material color to red
                material.color = Color.red;
                // Make the material semi-transparent
                Color newColor = material.color;
                material.color = newColor;
            }
        }
    }

    private void TurnGreen()
    {
        foreach (Renderer renderer in renderers)
        {
            foreach (Material material in renderer.materials)
            {
                // Change the material color to red
                material.color = Color.green;
                // Make the material semi-transparent
                Color newColor = material.color;
                material.color = newColor;
            }
        }
    }

    private void StartTap()
    {
        if (stateEnabled)
        {
            if (CanPlace())
            {
               StartCoroutine(SpawnObject());
            }
        }
    }

    IEnumerator SpawnObject()
    {
        GameObject PlacedObject = objectToPlace.gameObject;
        PlacedObject = Instantiate(objectToPlace, new Vector3(tempObject.transform.position.x, 0, tempObject.transform.position.z), Quaternion.identity);
        PlacedObject.transform.parent = TerrainHolder.transform;
        PlacedObject.name = objectToPlace.name;
        InventoryManager.Instance.GetSelectedItem(true);
        yield return new WaitForSeconds(0.1f);
        OnStateChange?.Invoke();
        StopCoroutine(SpawnObject());
    }
}
