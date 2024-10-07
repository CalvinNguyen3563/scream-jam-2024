using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorchInteract : MonoBehaviour, Interactable
{
    public Item item;

    public Item GetItemInfo()
    {
        return item;
    }

    public void DestroyItem()
    {
        Destroy(gameObject.transform.root.gameObject);
    }
}



