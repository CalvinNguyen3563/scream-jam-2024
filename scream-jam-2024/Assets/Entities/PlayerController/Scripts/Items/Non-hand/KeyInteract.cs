using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyInteract : MonoBehaviour, Interactable
{

    public Interactable.itemState state = Interactable.itemState.interactable;

    public AudioSource src;
    public AudioClip clip;
    private void Awake()
    {
    }

    private void Start()
    {
       src = WorldGameObjectStorage.Instance.playerSrc;
    }
    public Item GetItemInfo()
    {
        return null;
    }

    public void PerformSpecialAction()
    {
        ++PlayerInventoryUIManager.Instance.keyCount;
        src.PlayOneShot(clip);
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
