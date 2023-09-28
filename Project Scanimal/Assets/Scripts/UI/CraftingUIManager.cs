using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PlayerStateManager;

public class CraftingUIManager : MonoBehaviour
{
    #region events
    public delegate void CloseInteract();
    public static event CloseInteract OnCloseInteract;
    #endregion

    public void OnClickDestroy()
    {
        OnCloseInteract?.Invoke();

        Destroy(this.gameObject);
    }
}
