using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEditor.Build.Content;
using UnityEngine;

public class Interact : MonoBehaviour
{
    private enum InteractionType
    { 
        Crafting,
        Mail
    }

    [SerializeField]
    private InteractionType currentInteractionType;

    [SerializeField]
    private GameObject craftingCanvasObject;

    [SerializeField]
    private GameObject mailCanvasObject;

    public void Interaction()
    {
        switch (currentInteractionType)
        {
            case InteractionType.Crafting:
                Instantiate(craftingCanvasObject);
                break;
            case InteractionType.Mail:
                Instantiate(mailCanvasObject);
                break;
        }
    }
}
