using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour, IBeginDragHandler, IDragHandler , IEndDragHandler
{
    [HideInInspector]
    public ItemSO item;
    [HideInInspector]
    public Transform parentAfterDrag;
    [HideInInspector]
    public int count = 1;

    [Header("UI")]
    public Image image;
    public Text countText;

    public void InitiliseItem(ItemSO newItem)
    { 
        item = newItem;
        image.sprite= newItem.image;
        RefreshCount();
    }

    public void RefreshCount()
    { 
        countText.text = count.ToString();
        bool textActive = count > 1;
        countText.gameObject.SetActive(textActive);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        parentAfterDrag = transform.parent;
        transform.SetParent(transform.root);
        transform.SetAsLastSibling();
        image.raycastTarget = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.SetParent(parentAfterDrag);
        image.raycastTarget = true;
    }
}
