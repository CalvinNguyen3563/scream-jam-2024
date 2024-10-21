using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyInteract : MonoBehaviour, Interactable
{

    public Interactable.itemState state = Interactable.itemState.interactable;
    private void Awake()
    {
    }

    public Item GetItemInfo()
    {
        return null;
    }

    public void PerformSpecialAction()
    {
        ++PlayerInventoryUIManager.Instance.keyCount;
        DestroyItem();
    }

    public Interactable.itemState GetItemState()
    {
        return state;
    }
    public void DestroyItem()
    {
        Destroy(gameObject.transform.parent.gameObject);
    }
}
