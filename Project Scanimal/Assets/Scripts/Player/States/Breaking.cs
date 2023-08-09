using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class Breaking : MonoBehaviour
{
    private bool stateEnabled;
    [SerializeField]
    private float hitRange = 0.5f;
    [SerializeField]
    private float rayThickness = 1f;

    private GameObject currentObject;

    public delegate void ObjectChanged();
    public static event ObjectChanged OnObjectChanged;

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
        currentObject = null;
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
        }
        else
        {
            stateEnabled = false;
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
            }
        }
        else
        {
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
                //Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
                Gizmos.color = Color.green;
                Vector3 sphereCastMidpoint = transform.position + (transform.forward * hit.distance);
                Gizmos.DrawWireSphere(sphereCastMidpoint, rayThickness);
                Gizmos.DrawSphere(hit.point, 0.1f);
                Debug.DrawLine(transform.position, sphereCastMidpoint, Color.green);
                currentObject = hit.transform.gameObject;
                //return true;
            }
            else
            {
                //Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hitRange, Color.white);
                Gizmos.color = Color.red;
                Vector3 sphereCastMidpoint = transform.position + (transform.forward * (hitRange - rayThickness));
                Gizmos.DrawWireSphere(sphereCastMidpoint, rayThickness);
                Debug.DrawLine(transform.position, sphereCastMidpoint, Color.red);
                //return false;
            }
        }
    }
}
