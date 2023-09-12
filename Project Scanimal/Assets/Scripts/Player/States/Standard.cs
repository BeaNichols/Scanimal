using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Breaking;

public class Standard : MonoBehaviour
{
    [SerializeField]
    private float hitRange = 0.5f;
    [SerializeField]
    private float rayThickness = 1f;

    private bool stateEnabled;
    private GameObject currentObject;
    private Interact interaction;

    public GameObject interactCanvas;

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
    }

    private void Update()
    {
        if (stateEnabled)
        {
            Interact();
        }
    }

    private void EnableDisableState(PlayerStateManager.PlayerState currentState)
    {
        if (currentState == PlayerStateManager.PlayerState.standard)
        {
            stateEnabled = true;

        }
        else
        {
            stateEnabled = false;
        }
    }

    private bool Raycast()
    {
        RaycastHit hit;
        if (Physics.SphereCast(transform.position, rayThickness, transform.TransformDirection(Vector3.forward), out hit, hitRange))
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

    private void StartTap()
    {
        if (stateEnabled)
        {
            if (CanInteract())
            {
                
            }
        }
    }

    private void Interact()
    {
        if (Raycast())
        {
            if (currentObject.GetComponent<Interact>() != null)
            {
                interaction = currentObject.GetComponent<Interact>();
                if (interaction != null)
                {
                    interactCanvas.SetActive(true);
                }
            }
        }
        else
        {
            if (interaction != null)
            {
                interactCanvas.SetActive(false);
            }
        }
    }

    private bool CanInteract()
    {

        return false;
    }
}
