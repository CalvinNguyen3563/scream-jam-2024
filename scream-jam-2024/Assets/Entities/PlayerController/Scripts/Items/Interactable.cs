using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Interactable 
{
    public enum itemState
    {
        inHand,
        interactable
    }
    Item GetItemInfo();

    void PerformSpecialAction();

    itemState GetItemState();
    void DestroyItem();
}
