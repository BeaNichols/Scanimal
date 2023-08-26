using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropDownMenu : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> DropDownItems = new List<GameObject>();

    private bool isStateOne = false;

    private void Start()
    {
        // Get the transform of the parent object
        Transform parentTransform = transform; // Replace 'transform' with your parent object's transform

        // Loop through each child transform
        foreach (Transform childTransform in parentTransform)
        {
            // Add the child game object to the list
            DropDownItems.Add(childTransform.gameObject);
        }

        foreach (GameObject UiObject in DropDownItems)
        { 
            UiObject.SetActive(false);
        }
    }

    public void ToggleAction()
    {
        if (isStateOne)
        {
            // Perform the second action (opposite of the first)
            OnClickDisable();
            isStateOne = false;
        }
        else
        {
            // Perform the first action
            OnClickEnable();
            isStateOne = true;
        }
    }

    public void OnClickEnable()
    {
        foreach (GameObject UiObject in DropDownItems)
        {
            UiObject.SetActive(true);
        }
    }

    public void OnClickDisable() 
    {
        foreach (GameObject UiObject in DropDownItems)
        {
            UiObject.SetActive(false);
        }
    }

}
