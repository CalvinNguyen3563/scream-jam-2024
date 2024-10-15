using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Interactable 
{
    Item GetItemInfo();
    void DestroyItem();
}
