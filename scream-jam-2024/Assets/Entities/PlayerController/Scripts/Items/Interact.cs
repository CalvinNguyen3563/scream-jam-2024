using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact : MonoBehaviour, Interactable
{
    public Item item;
    public Interactable.itemState state = Interactable.itemState.inHand;

    public Item GetItemInfo()
    {
        return item;
    }

    public void PerformSpecialAction()
    {
        Debug.LogError("No special action");
        return;
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



