using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WorldOverlayController : MonoBehaviour
{
    private Canvas worldCanvas;

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
    }

    public void EnableCanvas()
    {
        worldCanvas.gameObject.SetActive(true);
    }

    public void DisableCanvas() 
    {
        worldCanvas.gameObject.SetActive(false);
    }
}
