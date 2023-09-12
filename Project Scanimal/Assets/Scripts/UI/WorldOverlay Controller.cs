using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WorldOverlayController : MonoBehaviour
{
    [SerializeField]
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
        if (worldCanvas == null)
        {
            return;
        }
        else
        {
            worldCanvas.gameObject.SetActive(false);
        }
    }

    void OnCollisionEnter(Collision collisionInfo)
    {
        if (collisionInfo.gameObject.tag == "Item" || collisionInfo.gameObject.tag == "Tree" || collisionInfo.gameObject.tag == "Rock" || collisionInfo.gameObject.tag == "Flower")
        { 
            colliding = true;
        }
    }

    void OnCollisionExit(Collision collisionInfo)
    {
        if (collisionInfo.gameObject.tag == "Item" || collisionInfo.gameObject.tag == "Tree" || collisionInfo.gameObject.tag == "Rock" || collisionInfo.gameObject.tag == "Flower")
        {
            colliding = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Item" || other.gameObject.tag == "Tree" || other.gameObject.tag == "Rock" || other.gameObject.tag == "Flower")
        {
            colliding = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Item" || other.gameObject.tag == "Tree" || other.gameObject.tag == "Rock" || other.gameObject.tag == "Flower")
        {
            colliding = false;
        }
    }
}
