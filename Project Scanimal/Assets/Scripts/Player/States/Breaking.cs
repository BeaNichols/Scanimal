using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;


public class Breaking : MonoBehaviour
{
    #region Events
    public delegate void ObjectChanged();
    public static event ObjectChanged OnObjectChanged;
    #endregion

    [SerializeField]
    private float hitRange = 0.5f;
    [SerializeField]
    private float rayThickness = 1f;
    [SerializeField]
    private ItemSO item;

    private bool stateEnabled;
    private GameObject currentObject;
    private GameObject inputManager;
    private int hitAmount;
    private bool Swiped = false;


    private void OnEnable()
    {
        PlayerStateManager.OnStateChange += EnableDisableState;
        SwpieDetection.OnSwipe += IsSwiping;
    }

    private void OnDisable()
    {
        PlayerStateManager.OnStateChange -= EnableDisableState;
        SwpieDetection.OnSwipe -= IsSwiping;
    }

    private void Start()
    {
        inputManager = GameObject.Find("InputManager");
        inputManager.SetActive(false);
        stateEnabled = false;
        currentObject = null;
        hitAmount = 0;
    }

    private void Update()
    {
        if (stateEnabled)
        {
            BreakMode();
        }
    }

    private void EnableDisableState(PlayerStateManager.PlayerState currentState)
    {
        if (currentState == PlayerStateManager.PlayerState.breaking)
        {
            stateEnabled = true;
            inputManager.SetActive(true);
        }
        else
        {
            stateEnabled = false;
            if (inputManager.activeSelf == true)
            {
                inputManager.SetActive(false);
            }
        }
    }

    private void BreakMode()
    {
        if (Raycast())
        {
            var worldController = currentObject.GetComponent<WorldOverlayController>();
            if (worldController != null) 
            {
                worldController.EnableCanvas();
                StartBreak(currentObject);
            }
        }
        else
        {
            hitAmount = 0;
            OnObjectChanged?.Invoke();
        }
    }

    private bool Raycast()
    {
        RaycastHit hit;
        if (Physics.SphereCast(transform.position,rayThickness, transform.TransformDirection(Vector3.forward), out hit, hitRange))
        {
            currentObject = hit.transform.gameObject;
            return true;
        }
        else
        {
            return false;
        }
    }

    private void OnDrawGizmos()
    {
        if (stateEnabled == true)
        {
            RaycastHit hit;
            if (Physics.SphereCast(transform.position, rayThickness, transform.TransformDirection(Vector3.forward), out hit, hitRange))
            {
                Gizmos.color = Color.green;
                Vector3 sphereCastMidpoint = transform.position + (transform.forward * hit.distance);
                Gizmos.DrawWireSphere(sphereCastMidpoint, rayThickness);
                Gizmos.DrawSphere(hit.point, 0.1f);
                Debug.DrawLine(transform.position, sphereCastMidpoint, Color.green);
                currentObject = hit.transform.gameObject;
            }
            else
            {
                Gizmos.color = Color.red;
                Vector3 sphereCastMidpoint = transform.position + (transform.forward * (hitRange - rayThickness));
                Gizmos.DrawWireSphere(sphereCastMidpoint, rayThickness);
                Debug.DrawLine(transform.position, sphereCastMidpoint, Color.red);
            }
        }
    }

    private void StartBreak(GameObject currentObject)
    {
        if (Swiped)
        {
            hitAmount++;
            Swiped = false;
        }
        else if (hitAmount >= 3)
        {
            Destroy(currentObject);
            InventoryManager.Instance.AddItem(item);
        }
    }

    private void IsSwiping()
    {
        if (SwpieDetection.Instance.currentDirection == SwpieDetection.swipeDirection.left)
        {
            Swiped = true;
            SwpieDetection.Instance.currentDirection = SwpieDetection.swipeDirection.non;
        }
        else if (SwpieDetection.Instance.currentDirection == SwpieDetection.swipeDirection.right)
        {
            Swiped = true;
            SwpieDetection.Instance.currentDirection = SwpieDetection.swipeDirection.non;
        }
        else
        { Swiped = false; }
    }
}
