using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WorldOverlayController : MonoBehaviour
{
    private Canvas worldCanvas;
    public ItemSO ItemDrop;

    private BoxCollider boxCollider;
    public bool colliding;

    private void OnEnable()
    {
        Breaking.OnObjectChanged += DisableCanvas;
    }

    private void OnDisable()
    {
        Breaking.OnObjectChanged -= DisableCanvas;
    }

    private void Start()
    {
        worldCanvas = GetComponentInChildren<Canvas>();
        worldCanvas.gameObject.SetActive(false);
        boxCollider = GetComponent<BoxCollider>();
        colliding = false;
    }

    public void EnableCanvas()
    {
        worldCanvas.gameObject.SetActive(true);
    }

    public void DisableCanvas() 
    {
        worldCanvas.gameObject.SetActive(false);
    }

    void OnCollisionEnter(Collision collisionInfo)
    {
        if (collisionInfo.gameObject.tag == "Item")
        { 
            colliding = true;
        }
    }

    void OnCollisionExit(Collision collisionInfo)
    {
        if (collisionInfo.gameObject.tag == "Item")
        {
            colliding = false;
        }
    }
}
